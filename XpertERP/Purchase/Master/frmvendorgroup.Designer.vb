<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorGroup
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
        Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rdmenuexit = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chk_istdsapplicable = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndgroupcode = New common.UserControls.txtNavigator()
        Me.rdlblgroupcode = New common.Controls.MyLabel()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.rdtxtgroupdesc = New common.Controls.MyTextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndSubGroup = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtSubGroup = New common.Controls.MyTextBox()
        Me.fndtaxgroup = New common.UserControls.txtFinder()
        Me.rdlbltaxgroup = New common.Controls.MyLabel()
        Me.fndpayamentcode = New common.UserControls.txtFinder()
        Me.rdlblpayamentcode = New common.Controls.MyLabel()
        Me.fndbankcode = New common.UserControls.txtFinder()
        Me.rdlblbankcode = New common.Controls.MyLabel()
        Me.fndtermscode = New common.UserControls.txtFinder()
        Me.rdlbltermscode = New common.Controls.MyLabel()
        Me.fndaccountset = New common.UserControls.txtFinder()
        Me.rdlblaccountset = New common.Controls.MyLabel()
        Me.rdtxttaxgroup = New common.Controls.MyTextBox()
        Me.rdtxtpayametcode = New common.Controls.MyTextBox()
        Me.rdtxtbankcode = New common.Controls.MyTextBox()
        Me.rdtxttermscode = New common.Controls.MyTextBox()
        Me.rdtxtaccountset = New common.Controls.MyTextBox()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.chkDefaultVSP = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chk_istdsapplicable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblgroupcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtgroupdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltaxgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblpayamentcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltermscode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblaccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxttaxgroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtpayametcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxttermscode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtaccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenu1
        '
        Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
        Me.rdmenu1.Name = "rdmenu1"
        Me.rdmenu1.Size = New System.Drawing.Size(744, 20)
        Me.rdmenu1.TabIndex = 0
        Me.rdmenu1.Text = "rdmenu"
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
        Me.rdmenuexport.AccessibleDescription = "Export"
        Me.rdmenuexport.AccessibleName = "Export"
        Me.rdmenuexport.Name = "rdmenuexport"
        Me.rdmenuexport.Text = "Export"
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
        Me.RadGroupBox1.Controls.Add(Me.chkDefaultVSP)
        Me.RadGroupBox1.Controls.Add(Me.chk_istdsapplicable)
        Me.RadGroupBox1.Controls.Add(Me.fndgroupcode)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnreset)
        Me.RadGroupBox1.Controls.Add(Me.rdtxtgroupdesc)
        Me.RadGroupBox1.Controls.Add(Me.rdlblgroupcode)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(744, 249)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chk_istdsapplicable
        '
        Me.chk_istdsapplicable.Location = New System.Drawing.Point(512, 13)
        Me.chk_istdsapplicable.Name = "chk_istdsapplicable"
        Me.chk_istdsapplicable.Size = New System.Drawing.Size(107, 18)
        Me.chk_istdsapplicable.TabIndex = 4
        Me.chk_istdsapplicable.Text = "Is TDS Applicable"
        '
        'fndgroupcode
        '
        Me.fndgroupcode.FieldName = Nothing
        Me.fndgroupcode.Location = New System.Drawing.Point(108, 11)
        Me.fndgroupcode.MendatroryField = False
        Me.fndgroupcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndgroupcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndgroupcode.MyLinkLable1 = Me.rdlblgroupcode
        Me.fndgroupcode.MyLinkLable2 = Nothing
        Me.fndgroupcode.MyMaxLength = 12
        Me.fndgroupcode.MyReadOnly = False
        Me.fndgroupcode.Name = "fndgroupcode"
        Me.fndgroupcode.Size = New System.Drawing.Size(180, 21)
        Me.fndgroupcode.TabIndex = 0
        Me.fndgroupcode.Value = ""
        '
        'rdlblgroupcode
        '
        Me.rdlblgroupcode.FieldName = Nothing
        Me.rdlblgroupcode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rdlblgroupcode.Location = New System.Drawing.Point(20, 12)
        Me.rdlblgroupcode.Name = "rdlblgroupcode"
        Me.rdlblgroupcode.Size = New System.Drawing.Size(67, 18)
        Me.rdlblgroupcode.TabIndex = 3
        Me.rdlblgroupcode.Text = "Group Code"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnreset.Location = New System.Drawing.Point(294, 12)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(18, 20)
        Me.rdbtnreset.TabIndex = 0
        '
        'rdtxtgroupdesc
        '
        Me.rdtxtgroupdesc.CalculationExpression = Nothing
        Me.rdtxtgroupdesc.FieldCode = Nothing
        Me.rdtxtgroupdesc.FieldDesc = Nothing
        Me.rdtxtgroupdesc.FieldMaxLength = 0
        Me.rdtxtgroupdesc.FieldName = Nothing
        Me.rdtxtgroupdesc.isCalculatedField = False
        Me.rdtxtgroupdesc.IsSourceFromTable = False
        Me.rdtxtgroupdesc.IsSourceFromValueList = False
        Me.rdtxtgroupdesc.IsUnique = False
        Me.rdtxtgroupdesc.Location = New System.Drawing.Point(318, 12)
        Me.rdtxtgroupdesc.MaxLength = 100
        Me.rdtxtgroupdesc.MendatroryField = False
        Me.rdtxtgroupdesc.MyLinkLable1 = Me.rdlblgroupcode
        Me.rdtxtgroupdesc.MyLinkLable2 = Nothing
        Me.rdtxtgroupdesc.Name = "rdtxtgroupdesc"
        Me.rdtxtgroupdesc.ReferenceFieldDesc = Nothing
        Me.rdtxtgroupdesc.ReferenceFieldName = Nothing
        Me.rdtxtgroupdesc.ReferenceTableName = Nothing
        Me.rdtxtgroupdesc.Size = New System.Drawing.Size(178, 20)
        Me.rdtxtgroupdesc.TabIndex = 1
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndSubGroup)
        Me.RadGroupBox2.Controls.Add(Me.txtSubGroup)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.fndtaxgroup)
        Me.RadGroupBox2.Controls.Add(Me.fndpayamentcode)
        Me.RadGroupBox2.Controls.Add(Me.fndbankcode)
        Me.RadGroupBox2.Controls.Add(Me.fndtermscode)
        Me.RadGroupBox2.Controls.Add(Me.fndaccountset)
        Me.RadGroupBox2.Controls.Add(Me.rdtxttaxgroup)
        Me.RadGroupBox2.Controls.Add(Me.rdtxtpayametcode)
        Me.RadGroupBox2.Controls.Add(Me.rdtxtbankcode)
        Me.RadGroupBox2.Controls.Add(Me.rdtxttermscode)
        Me.RadGroupBox2.Controls.Add(Me.rdlbltaxgroup)
        Me.RadGroupBox2.Controls.Add(Me.rdlblpayamentcode)
        Me.RadGroupBox2.Controls.Add(Me.rdlblbankcode)
        Me.RadGroupBox2.Controls.Add(Me.rdlbltermscode)
        Me.RadGroupBox2.Controls.Add(Me.rdtxtaccountset)
        Me.RadGroupBox2.Controls.Add(Me.rdlblaccountset)
        Me.RadGroupBox2.HeaderText = "Group"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 38)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(496, 181)
        Me.RadGroupBox2.TabIndex = 2
        Me.RadGroupBox2.Text = "Group"
        '
        'fndSubGroup
        '
        Me.fndSubGroup.CalculationExpression = Nothing
        Me.fndSubGroup.FieldCode = Nothing
        Me.fndSubGroup.FieldDesc = Nothing
        Me.fndSubGroup.FieldMaxLength = 0
        Me.fndSubGroup.FieldName = Nothing
        Me.fndSubGroup.isCalculatedField = False
        Me.fndSubGroup.IsSourceFromTable = False
        Me.fndSubGroup.IsSourceFromValueList = False
        Me.fndSubGroup.IsUnique = False
        Me.fndSubGroup.Location = New System.Drawing.Point(104, 151)
        Me.fndSubGroup.MendatroryField = True
        Me.fndSubGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSubGroup.MyLinkLable1 = Me.MyLabel1
        Me.fndSubGroup.MyLinkLable2 = Nothing
        Me.fndSubGroup.MyReadOnly = False
        Me.fndSubGroup.MyShowMasterFormButton = False
        Me.fndSubGroup.Name = "fndSubGroup"
        Me.fndSubGroup.ReferenceFieldDesc = Nothing
        Me.fndSubGroup.ReferenceFieldName = Nothing
        Me.fndSubGroup.ReferenceTableName = Nothing
        Me.fndSubGroup.Size = New System.Drawing.Size(126, 19)
        Me.fndSubGroup.TabIndex = 15
        Me.fndSubGroup.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(13, 151)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel1.TabIndex = 17
        Me.MyLabel1.Text = "Sub Group"
        '
        'txtSubGroup
        '
        Me.txtSubGroup.CalculationExpression = Nothing
        Me.txtSubGroup.FieldCode = Nothing
        Me.txtSubGroup.FieldDesc = Nothing
        Me.txtSubGroup.FieldMaxLength = 0
        Me.txtSubGroup.FieldName = Nothing
        Me.txtSubGroup.isCalculatedField = False
        Me.txtSubGroup.IsSourceFromTable = False
        Me.txtSubGroup.IsSourceFromValueList = False
        Me.txtSubGroup.IsUnique = False
        Me.txtSubGroup.Location = New System.Drawing.Point(247, 151)
        Me.txtSubGroup.MaxLength = 50
        Me.txtSubGroup.MendatroryField = False
        Me.txtSubGroup.MyLinkLable1 = Me.MyLabel1
        Me.txtSubGroup.MyLinkLable2 = Nothing
        Me.txtSubGroup.Name = "txtSubGroup"
        Me.txtSubGroup.ReferenceFieldDesc = Nothing
        Me.txtSubGroup.ReferenceFieldName = Nothing
        Me.txtSubGroup.ReferenceTableName = Nothing
        Me.txtSubGroup.Size = New System.Drawing.Size(236, 20)
        Me.txtSubGroup.TabIndex = 16
        Me.txtSubGroup.TabStop = False
        '
        'fndtaxgroup
        '
        Me.fndtaxgroup.CalculationExpression = Nothing
        Me.fndtaxgroup.FieldCode = Nothing
        Me.fndtaxgroup.FieldDesc = Nothing
        Me.fndtaxgroup.FieldMaxLength = 0
        Me.fndtaxgroup.FieldName = Nothing
        Me.fndtaxgroup.isCalculatedField = False
        Me.fndtaxgroup.IsSourceFromTable = False
        Me.fndtaxgroup.IsSourceFromValueList = False
        Me.fndtaxgroup.IsUnique = False
        Me.fndtaxgroup.Location = New System.Drawing.Point(104, 125)
        Me.fndtaxgroup.MendatroryField = True
        Me.fndtaxgroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndtaxgroup.MyLinkLable1 = Me.rdlbltaxgroup
        Me.fndtaxgroup.MyLinkLable2 = Nothing
        Me.fndtaxgroup.MyReadOnly = False
        Me.fndtaxgroup.MyShowMasterFormButton = False
        Me.fndtaxgroup.Name = "fndtaxgroup"
        Me.fndtaxgroup.ReferenceFieldDesc = Nothing
        Me.fndtaxgroup.ReferenceFieldName = Nothing
        Me.fndtaxgroup.ReferenceTableName = Nothing
        Me.fndtaxgroup.Size = New System.Drawing.Size(126, 19)
        Me.fndtaxgroup.TabIndex = 4
        Me.fndtaxgroup.Value = ""
        '
        'rdlbltaxgroup
        '
        Me.rdlbltaxgroup.FieldName = Nothing
        Me.rdlbltaxgroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.rdlbltaxgroup.Location = New System.Drawing.Point(13, 125)
        Me.rdlbltaxgroup.Name = "rdlbltaxgroup"
        Me.rdlbltaxgroup.Size = New System.Drawing.Size(60, 16)
        Me.rdlbltaxgroup.TabIndex = 10
        Me.rdlbltaxgroup.Text = "Tax Group"
        '
        'fndpayamentcode
        '
        Me.fndpayamentcode.CalculationExpression = Nothing
        Me.fndpayamentcode.FieldCode = Nothing
        Me.fndpayamentcode.FieldDesc = Nothing
        Me.fndpayamentcode.FieldMaxLength = 0
        Me.fndpayamentcode.FieldName = Nothing
        Me.fndpayamentcode.isCalculatedField = False
        Me.fndpayamentcode.IsSourceFromTable = False
        Me.fndpayamentcode.IsSourceFromValueList = False
        Me.fndpayamentcode.IsUnique = False
        Me.fndpayamentcode.Location = New System.Drawing.Point(104, 99)
        Me.fndpayamentcode.MendatroryField = True
        Me.fndpayamentcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndpayamentcode.MyLinkLable1 = Me.rdlblpayamentcode
        Me.fndpayamentcode.MyLinkLable2 = Nothing
        Me.fndpayamentcode.MyReadOnly = False
        Me.fndpayamentcode.MyShowMasterFormButton = False
        Me.fndpayamentcode.Name = "fndpayamentcode"
        Me.fndpayamentcode.ReferenceFieldDesc = Nothing
        Me.fndpayamentcode.ReferenceFieldName = Nothing
        Me.fndpayamentcode.ReferenceTableName = Nothing
        Me.fndpayamentcode.Size = New System.Drawing.Size(126, 19)
        Me.fndpayamentcode.TabIndex = 3
        Me.fndpayamentcode.Value = ""
        '
        'rdlblpayamentcode
        '
        Me.rdlblpayamentcode.FieldName = Nothing
        Me.rdlblpayamentcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.rdlblpayamentcode.Location = New System.Drawing.Point(13, 99)
        Me.rdlblpayamentcode.Name = "rdlblpayamentcode"
        Me.rdlblpayamentcode.Size = New System.Drawing.Size(81, 16)
        Me.rdlblpayamentcode.TabIndex = 11
        Me.rdlblpayamentcode.Text = "Payment Code"
        '
        'fndbankcode
        '
        Me.fndbankcode.CalculationExpression = Nothing
        Me.fndbankcode.FieldCode = Nothing
        Me.fndbankcode.FieldDesc = Nothing
        Me.fndbankcode.FieldMaxLength = 0
        Me.fndbankcode.FieldName = Nothing
        Me.fndbankcode.isCalculatedField = False
        Me.fndbankcode.IsSourceFromTable = False
        Me.fndbankcode.IsSourceFromValueList = False
        Me.fndbankcode.IsUnique = False
        Me.fndbankcode.Location = New System.Drawing.Point(104, 73)
        Me.fndbankcode.MendatroryField = True
        Me.fndbankcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndbankcode.MyLinkLable1 = Me.rdlblbankcode
        Me.fndbankcode.MyLinkLable2 = Nothing
        Me.fndbankcode.MyReadOnly = False
        Me.fndbankcode.MyShowMasterFormButton = False
        Me.fndbankcode.Name = "fndbankcode"
        Me.fndbankcode.ReferenceFieldDesc = Nothing
        Me.fndbankcode.ReferenceFieldName = Nothing
        Me.fndbankcode.ReferenceTableName = Nothing
        Me.fndbankcode.Size = New System.Drawing.Size(126, 19)
        Me.fndbankcode.TabIndex = 2
        Me.fndbankcode.Value = ""
        '
        'rdlblbankcode
        '
        Me.rdlblbankcode.FieldName = Nothing
        Me.rdlblbankcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.rdlblbankcode.Location = New System.Drawing.Point(13, 73)
        Me.rdlblbankcode.Name = "rdlblbankcode"
        Me.rdlblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.rdlblbankcode.TabIndex = 12
        Me.rdlblbankcode.Text = "Bank Code"
        '
        'fndtermscode
        '
        Me.fndtermscode.CalculationExpression = Nothing
        Me.fndtermscode.FieldCode = Nothing
        Me.fndtermscode.FieldDesc = Nothing
        Me.fndtermscode.FieldMaxLength = 0
        Me.fndtermscode.FieldName = Nothing
        Me.fndtermscode.isCalculatedField = False
        Me.fndtermscode.IsSourceFromTable = False
        Me.fndtermscode.IsSourceFromValueList = False
        Me.fndtermscode.IsUnique = False
        Me.fndtermscode.Location = New System.Drawing.Point(104, 47)
        Me.fndtermscode.MendatroryField = True
        Me.fndtermscode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndtermscode.MyLinkLable1 = Me.rdlbltermscode
        Me.fndtermscode.MyLinkLable2 = Nothing
        Me.fndtermscode.MyReadOnly = False
        Me.fndtermscode.MyShowMasterFormButton = False
        Me.fndtermscode.Name = "fndtermscode"
        Me.fndtermscode.ReferenceFieldDesc = Nothing
        Me.fndtermscode.ReferenceFieldName = Nothing
        Me.fndtermscode.ReferenceTableName = Nothing
        Me.fndtermscode.Size = New System.Drawing.Size(126, 19)
        Me.fndtermscode.TabIndex = 1
        Me.fndtermscode.Value = ""
        '
        'rdlbltermscode
        '
        Me.rdlbltermscode.FieldName = Nothing
        Me.rdlbltermscode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.rdlbltermscode.Location = New System.Drawing.Point(13, 47)
        Me.rdlbltermscode.Name = "rdlbltermscode"
        Me.rdlbltermscode.Size = New System.Drawing.Size(68, 16)
        Me.rdlbltermscode.TabIndex = 13
        Me.rdlbltermscode.Text = "Terms Code"
        '
        'fndaccountset
        '
        Me.fndaccountset.CalculationExpression = Nothing
        Me.fndaccountset.FieldCode = Nothing
        Me.fndaccountset.FieldDesc = Nothing
        Me.fndaccountset.FieldMaxLength = 0
        Me.fndaccountset.FieldName = Nothing
        Me.fndaccountset.isCalculatedField = False
        Me.fndaccountset.IsSourceFromTable = False
        Me.fndaccountset.IsSourceFromValueList = False
        Me.fndaccountset.IsUnique = False
        Me.fndaccountset.Location = New System.Drawing.Point(104, 21)
        Me.fndaccountset.MendatroryField = True
        Me.fndaccountset.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndaccountset.MyLinkLable1 = Me.rdlblaccountset
        Me.fndaccountset.MyLinkLable2 = Nothing
        Me.fndaccountset.MyReadOnly = False
        Me.fndaccountset.MyShowMasterFormButton = False
        Me.fndaccountset.Name = "fndaccountset"
        Me.fndaccountset.ReferenceFieldDesc = Nothing
        Me.fndaccountset.ReferenceFieldName = Nothing
        Me.fndaccountset.ReferenceTableName = Nothing
        Me.fndaccountset.Size = New System.Drawing.Size(126, 19)
        Me.fndaccountset.TabIndex = 0
        Me.fndaccountset.Value = ""
        '
        'rdlblaccountset
        '
        Me.rdlblaccountset.FieldName = Nothing
        Me.rdlblaccountset.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rdlblaccountset.Location = New System.Drawing.Point(13, 23)
        Me.rdlblaccountset.Name = "rdlblaccountset"
        Me.rdlblaccountset.Size = New System.Drawing.Size(69, 18)
        Me.rdlblaccountset.TabIndex = 14
        Me.rdlblaccountset.Text = "Account Set "
        '
        'rdtxttaxgroup
        '
        Me.rdtxttaxgroup.CalculationExpression = Nothing
        Me.rdtxttaxgroup.FieldCode = Nothing
        Me.rdtxttaxgroup.FieldDesc = Nothing
        Me.rdtxttaxgroup.FieldMaxLength = 0
        Me.rdtxttaxgroup.FieldName = Nothing
        Me.rdtxttaxgroup.isCalculatedField = False
        Me.rdtxttaxgroup.IsSourceFromTable = False
        Me.rdtxttaxgroup.IsSourceFromValueList = False
        Me.rdtxttaxgroup.IsUnique = False
        Me.rdtxttaxgroup.Location = New System.Drawing.Point(247, 125)
        Me.rdtxttaxgroup.MaxLength = 50
        Me.rdtxttaxgroup.MendatroryField = False
        Me.rdtxttaxgroup.MyLinkLable1 = Me.rdlbltaxgroup
        Me.rdtxttaxgroup.MyLinkLable2 = Nothing
        Me.rdtxttaxgroup.Name = "rdtxttaxgroup"
        Me.rdtxttaxgroup.ReferenceFieldDesc = Nothing
        Me.rdtxttaxgroup.ReferenceFieldName = Nothing
        Me.rdtxttaxgroup.ReferenceTableName = Nothing
        Me.rdtxttaxgroup.Size = New System.Drawing.Size(236, 20)
        Me.rdtxttaxgroup.TabIndex = 5
        Me.rdtxttaxgroup.TabStop = False
        '
        'rdtxtpayametcode
        '
        Me.rdtxtpayametcode.CalculationExpression = Nothing
        Me.rdtxtpayametcode.FieldCode = Nothing
        Me.rdtxtpayametcode.FieldDesc = Nothing
        Me.rdtxtpayametcode.FieldMaxLength = 0
        Me.rdtxtpayametcode.FieldName = Nothing
        Me.rdtxtpayametcode.isCalculatedField = False
        Me.rdtxtpayametcode.IsSourceFromTable = False
        Me.rdtxtpayametcode.IsSourceFromValueList = False
        Me.rdtxtpayametcode.IsUnique = False
        Me.rdtxtpayametcode.Location = New System.Drawing.Point(247, 99)
        Me.rdtxtpayametcode.MaxLength = 50
        Me.rdtxtpayametcode.MendatroryField = False
        Me.rdtxtpayametcode.MyLinkLable1 = Me.rdlblpayamentcode
        Me.rdtxtpayametcode.MyLinkLable2 = Nothing
        Me.rdtxtpayametcode.Name = "rdtxtpayametcode"
        Me.rdtxtpayametcode.ReferenceFieldDesc = Nothing
        Me.rdtxtpayametcode.ReferenceFieldName = Nothing
        Me.rdtxtpayametcode.ReferenceTableName = Nothing
        Me.rdtxtpayametcode.Size = New System.Drawing.Size(236, 20)
        Me.rdtxtpayametcode.TabIndex = 6
        Me.rdtxtpayametcode.TabStop = False
        '
        'rdtxtbankcode
        '
        Me.rdtxtbankcode.CalculationExpression = Nothing
        Me.rdtxtbankcode.FieldCode = Nothing
        Me.rdtxtbankcode.FieldDesc = Nothing
        Me.rdtxtbankcode.FieldMaxLength = 0
        Me.rdtxtbankcode.FieldName = Nothing
        Me.rdtxtbankcode.isCalculatedField = False
        Me.rdtxtbankcode.IsSourceFromTable = False
        Me.rdtxtbankcode.IsSourceFromValueList = False
        Me.rdtxtbankcode.IsUnique = False
        Me.rdtxtbankcode.Location = New System.Drawing.Point(247, 73)
        Me.rdtxtbankcode.MaxLength = 60
        Me.rdtxtbankcode.MendatroryField = False
        Me.rdtxtbankcode.MyLinkLable1 = Me.rdlblbankcode
        Me.rdtxtbankcode.MyLinkLable2 = Nothing
        Me.rdtxtbankcode.Name = "rdtxtbankcode"
        Me.rdtxtbankcode.ReferenceFieldDesc = Nothing
        Me.rdtxtbankcode.ReferenceFieldName = Nothing
        Me.rdtxtbankcode.ReferenceTableName = Nothing
        Me.rdtxtbankcode.Size = New System.Drawing.Size(236, 20)
        Me.rdtxtbankcode.TabIndex = 7
        Me.rdtxtbankcode.TabStop = False
        '
        'rdtxttermscode
        '
        Me.rdtxttermscode.CalculationExpression = Nothing
        Me.rdtxttermscode.FieldCode = Nothing
        Me.rdtxttermscode.FieldDesc = Nothing
        Me.rdtxttermscode.FieldMaxLength = 0
        Me.rdtxttermscode.FieldName = Nothing
        Me.rdtxttermscode.isCalculatedField = False
        Me.rdtxttermscode.IsSourceFromTable = False
        Me.rdtxttermscode.IsSourceFromValueList = False
        Me.rdtxttermscode.IsUnique = False
        Me.rdtxttermscode.Location = New System.Drawing.Point(247, 47)
        Me.rdtxttermscode.MaxLength = 50
        Me.rdtxttermscode.MendatroryField = False
        Me.rdtxttermscode.MyLinkLable1 = Me.rdlbltermscode
        Me.rdtxttermscode.MyLinkLable2 = Nothing
        Me.rdtxttermscode.Name = "rdtxttermscode"
        Me.rdtxttermscode.ReferenceFieldDesc = Nothing
        Me.rdtxttermscode.ReferenceFieldName = Nothing
        Me.rdtxttermscode.ReferenceTableName = Nothing
        Me.rdtxttermscode.Size = New System.Drawing.Size(236, 20)
        Me.rdtxttermscode.TabIndex = 8
        Me.rdtxttermscode.TabStop = False
        '
        'rdtxtaccountset
        '
        Me.rdtxtaccountset.CalculationExpression = Nothing
        Me.rdtxtaccountset.FieldCode = Nothing
        Me.rdtxtaccountset.FieldDesc = Nothing
        Me.rdtxtaccountset.FieldMaxLength = 0
        Me.rdtxtaccountset.FieldName = Nothing
        Me.rdtxtaccountset.isCalculatedField = False
        Me.rdtxtaccountset.IsSourceFromTable = False
        Me.rdtxtaccountset.IsSourceFromValueList = False
        Me.rdtxtaccountset.IsUnique = False
        Me.rdtxtaccountset.Location = New System.Drawing.Point(247, 21)
        Me.rdtxtaccountset.MaxLength = 100
        Me.rdtxtaccountset.MendatroryField = False
        Me.rdtxtaccountset.MyLinkLable1 = Me.rdlblaccountset
        Me.rdtxtaccountset.MyLinkLable2 = Nothing
        Me.rdtxtaccountset.Name = "rdtxtaccountset"
        Me.rdtxtaccountset.ReferenceFieldDesc = Nothing
        Me.rdtxtaccountset.ReferenceFieldName = Nothing
        Me.rdtxtaccountset.ReferenceTableName = Nothing
        Me.rdtxtaccountset.Size = New System.Drawing.Size(236, 20)
        Me.rdtxtaccountset.TabIndex = 9
        Me.rdtxtaccountset.TabStop = False
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(670, 5)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(71, 7)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Location = New System.Drawing.Point(2, 7)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(744, 282)
        Me.SplitContainer1.SplitterDistance = 249
        Me.SplitContainer1.TabIndex = 2
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(143, 7)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "&History"
        '
        'chkDefaultVSP
        '
        Me.chkDefaultVSP.Location = New System.Drawing.Point(625, 13)
        Me.chkDefaultVSP.Name = "chkDefaultVSP"
        Me.chkDefaultVSP.Size = New System.Drawing.Size(79, 18)
        Me.chkDefaultVSP.TabIndex = 5
        Me.chkDefaultVSP.Text = "Default Secretary"
        '
        'frmVendorGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 302)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "frmVendorGroup"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendors Group"
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chk_istdsapplicable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblgroupcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtgroupdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltaxgroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblpayamentcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltermscode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblaccountset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxttaxgroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtpayametcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxttermscode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtaccountset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultVSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdmenuexit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdtxtgroupdesc As common.Controls.MyTextBox
    Friend WithEvents rdtxtaccountset As common.Controls.MyTextBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdtxttaxgroup As common.Controls.MyTextBox
    Friend WithEvents rdtxtpayametcode As common.Controls.MyTextBox
    Friend WithEvents rdtxtbankcode As common.Controls.MyTextBox
    Friend WithEvents rdtxttermscode As common.Controls.MyTextBox
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdlblgroupcode As common.Controls.MyLabel
    Friend WithEvents rdlblaccountset As common.Controls.MyLabel
    Friend WithEvents rdlbltermscode As common.Controls.MyLabel
    Friend WithEvents rdlbltaxgroup As common.Controls.MyLabel
    Friend WithEvents rdlblpayamentcode As common.Controls.MyLabel
    Friend WithEvents rdlblbankcode As common.Controls.MyLabel
    Friend WithEvents fndgroupcode As common.UserControls.txtNavigator
    Friend WithEvents fndaccountset As common.UserControls.txtFinder
    Friend WithEvents fndtermscode As common.UserControls.txtFinder
    Friend WithEvents fndbankcode As common.UserControls.txtFinder
    Friend WithEvents fndpayamentcode As common.UserControls.txtFinder
    Friend WithEvents fndtaxgroup As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chk_istdsapplicable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndSubGroup As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtSubGroup As common.Controls.MyTextBox
    Friend WithEvents chkDefaultVSP As Telerik.WinControls.UI.RadCheckBox
End Class

