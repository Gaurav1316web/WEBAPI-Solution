<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReplicationSetting
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
    '<System.Diagnostics.DebuggerStepThrough()> _
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvTargetTables As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtServerRetypePassword As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtServerRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtServerPassword As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtServerUserId As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtServerSchema As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtServerDBName As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtServerNameIP As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDefSnapshotFolder As common.Controls.MyTextBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDistributorName As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents btnSaveSub As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSavePub As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveDist As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReplicationSetting))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageDistributor = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtDefSnapshotFolder = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.txtDistributorName = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.pagePublication = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtDatabaseName = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyTextBox6 = New common.Controls.MyTextBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.txtPublisherName = New common.Controls.MyTextBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.gvTargetTables = New common.UserControls.MyRadGridView()
        Me.pageSubscription = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtServerRetypePassword = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtServerRemarks = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtServerPassword = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtServerUserId = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtServerSchema = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtServerDBName = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.txtServerNameIP = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.btnSaveSub = New Telerik.WinControls.UI.RadButton()
        Me.btnSavePub = New Telerik.WinControls.UI.RadButton()
        Me.btnSaveDist = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageDistributor.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtDefSnapshotFolder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistributorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pagePublication.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txtDatabaseName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyTextBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPublisherName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTargetTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTargetTables.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageSubscription.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtServerRetypePassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerUserId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerSchema, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerDBName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerNameIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveSub, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSavePub, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveDist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveSub)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSavePub)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveDist)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(978, 490)
        Me.SplitContainer1.SplitterDistance = 459
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.pageDistributor)
        Me.RadPageView1.Controls.Add(Me.pagePublication)
        Me.RadPageView1.Controls.Add(Me.pageSubscription)
        Me.RadPageView1.Location = New System.Drawing.Point(11, 12)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageDistributor
        Me.RadPageView1.Size = New System.Drawing.Size(955, 434)
        Me.RadPageView1.TabIndex = 18
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageDistributor
        '
        Me.pageDistributor.Controls.Add(Me.RadGroupBox1)
        Me.pageDistributor.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.pageDistributor.Location = New System.Drawing.Point(10, 37)
        Me.pageDistributor.Name = "pageDistributor"
        Me.pageDistributor.Size = New System.Drawing.Size(934, 386)
        Me.pageDistributor.Text = "Distribution"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtDefSnapshotFolder)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.txtDistributorName)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.HeaderText = "Settings"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(513, 85)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Settings"
        '
        'txtDefSnapshotFolder
        '
        Me.txtDefSnapshotFolder.Location = New System.Drawing.Point(146, 48)
        Me.txtDefSnapshotFolder.MaxLength = 50
        Me.txtDefSnapshotFolder.MendatroryField = False
        Me.txtDefSnapshotFolder.MyLinkLable1 = Me.RadLabel3
        Me.txtDefSnapshotFolder.MyLinkLable2 = Nothing
        Me.txtDefSnapshotFolder.Name = "txtDefSnapshotFolder"
        Me.txtDefSnapshotFolder.Size = New System.Drawing.Size(349, 20)
        Me.txtDefSnapshotFolder.TabIndex = 3
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(13, 48)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(127, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Default Snapshot Folder"
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(12, 89)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(2, 2)
        Me.RadLabel4.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'txtDistributorName
        '
        Me.txtDistributorName.Location = New System.Drawing.Point(146, 23)
        Me.txtDistributorName.MaxLength = 50
        Me.txtDistributorName.MendatroryField = False
        Me.txtDistributorName.MyLinkLable1 = Me.RadLabel2
        Me.txtDistributorName.MyLinkLable2 = Nothing
        Me.txtDistributorName.Name = "txtDistributorName"
        Me.txtDistributorName.Size = New System.Drawing.Size(349, 20)
        Me.txtDistributorName.TabIndex = 2
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(13, 23)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(93, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "Distributor Name"
        '
        'pagePublication
        '
        Me.pagePublication.Controls.Add(Me.RadGroupBox3)
        Me.pagePublication.Controls.Add(Me.RadGroupBox4)
        Me.pagePublication.ItemSize = New System.Drawing.SizeF(72.0!, 28.0!)
        Me.pagePublication.Location = New System.Drawing.Point(10, 37)
        Me.pagePublication.Name = "pagePublication"
        Me.pagePublication.Size = New System.Drawing.Size(934, 386)
        Me.pagePublication.Text = "Publication"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txtDatabaseName)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox3.Controls.Add(Me.MyTextBox6)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox3.Controls.Add(Me.RadButton3)
        Me.RadGroupBox3.Controls.Add(Me.txtPublisherName)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox3.HeaderText = "Settings"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(918, 111)
        Me.RadGroupBox3.TabIndex = 18
        Me.RadGroupBox3.Text = "Settings"
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.Location = New System.Drawing.Point(104, 72)
        Me.txtDatabaseName.MaxLength = 50
        Me.txtDatabaseName.MendatroryField = False
        Me.txtDatabaseName.MyLinkLable1 = Me.MyLabel14
        Me.txtDatabaseName.MyLinkLable2 = Nothing
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.Size = New System.Drawing.Size(447, 20)
        Me.txtDatabaseName.TabIndex = 21
        '
        'MyLabel14
        '
        Me.MyLabel14.Location = New System.Drawing.Point(13, 74)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(86, 18)
        Me.MyLabel14.TabIndex = 22
        Me.MyLabel14.Text = "Database Name"
        '
        'MyTextBox6
        '
        Me.MyTextBox6.Location = New System.Drawing.Point(104, 46)
        Me.MyTextBox6.MaxLength = 50
        Me.MyTextBox6.MendatroryField = False
        Me.MyTextBox6.MyLinkLable1 = Me.MyLabel19
        Me.MyTextBox6.MyLinkLable2 = Nothing
        Me.MyTextBox6.Name = "MyTextBox6"
        Me.MyTextBox6.Size = New System.Drawing.Size(447, 20)
        Me.MyTextBox6.TabIndex = 3
        '
        'MyLabel19
        '
        Me.MyLabel19.Location = New System.Drawing.Point(13, 48)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel19.TabIndex = 20
        Me.MyLabel19.Text = "Description"
        '
        'MyLabel20
        '
        Me.MyLabel20.Location = New System.Drawing.Point(12, 89)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel20.TabIndex = 19
        '
        'RadButton3
        '
        Me.RadButton3.Location = New System.Drawing.Point(352, -422)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(14, 20)
        Me.RadButton3.TabIndex = 17
        Me.RadButton3.Text = " "
        '
        'txtPublisherName
        '
        Me.txtPublisherName.Location = New System.Drawing.Point(104, 21)
        Me.txtPublisherName.MaxLength = 50
        Me.txtPublisherName.MendatroryField = False
        Me.txtPublisherName.MyLinkLable1 = Me.MyLabel21
        Me.txtPublisherName.MyLinkLable2 = Nothing
        Me.txtPublisherName.Name = "txtPublisherName"
        Me.txtPublisherName.Size = New System.Drawing.Size(447, 20)
        Me.txtPublisherName.TabIndex = 2
        '
        'MyLabel21
        '
        Me.MyLabel21.Location = New System.Drawing.Point(13, 23)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel21.TabIndex = 1
        Me.MyLabel21.Text = "Publisher Name"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox4.Controls.Add(Me.RadButton4)
        Me.RadGroupBox4.Controls.Add(Me.gvTargetTables)
        Me.RadGroupBox4.HeaderText = "Publication Tables"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 120)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(928, 263)
        Me.RadGroupBox4.TabIndex = 17
        Me.RadGroupBox4.Text = "Publication Tables"
        '
        'RadButton4
        '
        Me.RadButton4.Image = CType(resources.GetObject("RadButton4.Image"), System.Drawing.Image)
        Me.RadButton4.Location = New System.Drawing.Point(352, -422)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(14, 20)
        Me.RadButton4.TabIndex = 17
        Me.RadButton4.Text = " "
        '
        'gvTargetTables
        '
        Me.gvTargetTables.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvTargetTables.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTargetTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvTargetTables.EnableCustomFiltering = True
        Me.gvTargetTables.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvTargetTables.ForeColor = System.Drawing.Color.Black
        Me.gvTargetTables.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTargetTables.Location = New System.Drawing.Point(10, 20)
        '
        'gvTargetTables
        '
        Me.gvTargetTables.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvTargetTables.MasterTemplate.AllowAddNewRow = False
        Me.gvTargetTables.MasterTemplate.AutoGenerateColumns = False
        Me.gvTargetTables.MasterTemplate.EnableCustomFiltering = True
        Me.gvTargetTables.MasterTemplate.EnableFiltering = True
        Me.gvTargetTables.MasterTemplate.EnableGrouping = False
        Me.gvTargetTables.Name = "gvTargetTables"
        Me.gvTargetTables.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTargetTables.Size = New System.Drawing.Size(908, 233)
        Me.gvTargetTables.TabIndex = 13
        Me.gvTargetTables.TabStop = False
        '
        'pageSubscription
        '
        Me.pageSubscription.Controls.Add(Me.RadGroupBox2)
        Me.pageSubscription.ItemSize = New System.Drawing.SizeF(78.0!, 28.0!)
        Me.pageSubscription.Location = New System.Drawing.Point(10, 37)
        Me.pageSubscription.Name = "pageSubscription"
        Me.pageSubscription.Size = New System.Drawing.Size(934, 386)
        Me.pageSubscription.Text = "Subscription"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtServerRetypePassword)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox2.Controls.Add(Me.txtServerRemarks)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.txtServerPassword)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.txtServerUserId)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox2.Controls.Add(Me.txtServerSchema)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox2.Controls.Add(Me.txtServerDBName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox2.Controls.Add(Me.RadButton2)
        Me.RadGroupBox2.Controls.Add(Me.txtServerNameIP)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox2.HeaderText = "Server Settings"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(399, 206)
        Me.RadGroupBox2.TabIndex = 11
        Me.RadGroupBox2.Text = "Server Settings"
        '
        'txtServerRetypePassword
        '
        Me.txtServerRetypePassword.Location = New System.Drawing.Point(146, 147)
        Me.txtServerRetypePassword.MaxLength = 50
        Me.txtServerRetypePassword.MendatroryField = False
        Me.txtServerRetypePassword.MyLinkLable1 = Me.MyLabel12
        Me.txtServerRetypePassword.MyLinkLable2 = Nothing
        Me.txtServerRetypePassword.Name = "txtServerRetypePassword"
        Me.txtServerRetypePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtServerRetypePassword.Size = New System.Drawing.Size(212, 20)
        Me.txtServerRetypePassword.TabIndex = 29
        '
        'MyLabel12
        '
        Me.MyLabel12.Location = New System.Drawing.Point(16, 149)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(91, 18)
        Me.MyLabel12.TabIndex = 30
        Me.MyLabel12.Text = "Retype Password"
        '
        'txtServerRemarks
        '
        Me.txtServerRemarks.Location = New System.Drawing.Point(146, 171)
        Me.txtServerRemarks.MaxLength = 50
        Me.txtServerRemarks.MendatroryField = False
        Me.txtServerRemarks.MyLinkLable1 = Me.MyLabel5
        Me.txtServerRemarks.MyLinkLable2 = Nothing
        Me.txtServerRemarks.Name = "txtServerRemarks"
        Me.txtServerRemarks.Size = New System.Drawing.Size(212, 20)
        Me.txtServerRemarks.TabIndex = 7
        '
        'MyLabel5
        '
        Me.MyLabel5.Location = New System.Drawing.Point(16, 173)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel5.TabIndex = 28
        Me.MyLabel5.Text = "Remarks"
        '
        'txtServerPassword
        '
        Me.txtServerPassword.Location = New System.Drawing.Point(146, 123)
        Me.txtServerPassword.MaxLength = 50
        Me.txtServerPassword.MendatroryField = False
        Me.txtServerPassword.MyLinkLable1 = Me.MyLabel6
        Me.txtServerPassword.MyLinkLable2 = Nothing
        Me.txtServerPassword.Name = "txtServerPassword"
        Me.txtServerPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtServerPassword.Size = New System.Drawing.Size(212, 20)
        Me.txtServerPassword.TabIndex = 6
        '
        'MyLabel6
        '
        Me.MyLabel6.Location = New System.Drawing.Point(16, 125)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel6.TabIndex = 25
        Me.MyLabel6.Text = "Password"
        '
        'txtServerUserId
        '
        Me.txtServerUserId.Location = New System.Drawing.Point(146, 98)
        Me.txtServerUserId.MaxLength = 50
        Me.txtServerUserId.MendatroryField = False
        Me.txtServerUserId.MyLinkLable1 = Me.MyLabel7
        Me.txtServerUserId.MyLinkLable2 = Nothing
        Me.txtServerUserId.Name = "txtServerUserId"
        Me.txtServerUserId.Size = New System.Drawing.Size(212, 20)
        Me.txtServerUserId.TabIndex = 5
        '
        'MyLabel7
        '
        Me.MyLabel7.Location = New System.Drawing.Point(14, 100)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(41, 18)
        Me.MyLabel7.TabIndex = 24
        Me.MyLabel7.Text = "User Id"
        '
        'txtServerSchema
        '
        Me.txtServerSchema.Location = New System.Drawing.Point(146, 73)
        Me.txtServerSchema.MaxLength = 50
        Me.txtServerSchema.MendatroryField = False
        Me.txtServerSchema.MyLinkLable1 = Me.MyLabel8
        Me.txtServerSchema.MyLinkLable2 = Nothing
        Me.txtServerSchema.Name = "txtServerSchema"
        Me.txtServerSchema.Size = New System.Drawing.Size(212, 20)
        Me.txtServerSchema.TabIndex = 4
        '
        'MyLabel8
        '
        Me.MyLabel8.Location = New System.Drawing.Point(14, 75)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel8.TabIndex = 21
        Me.MyLabel8.Text = "Schema Name"
        '
        'txtServerDBName
        '
        Me.txtServerDBName.Location = New System.Drawing.Point(146, 48)
        Me.txtServerDBName.MaxLength = 50
        Me.txtServerDBName.MendatroryField = False
        Me.txtServerDBName.MyLinkLable1 = Me.MyLabel9
        Me.txtServerDBName.MyLinkLable2 = Nothing
        Me.txtServerDBName.Name = "txtServerDBName"
        Me.txtServerDBName.Size = New System.Drawing.Size(212, 20)
        Me.txtServerDBName.TabIndex = 3
        '
        'MyLabel9
        '
        Me.MyLabel9.Location = New System.Drawing.Point(13, 50)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(86, 18)
        Me.MyLabel9.TabIndex = 20
        Me.MyLabel9.Text = "Database Name"
        '
        'MyLabel10
        '
        Me.MyLabel10.Location = New System.Drawing.Point(12, 89)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel10.TabIndex = 19
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(352, -422)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(14, 20)
        Me.RadButton2.TabIndex = 17
        Me.RadButton2.Text = " "
        '
        'txtServerNameIP
        '
        Me.txtServerNameIP.Location = New System.Drawing.Point(146, 23)
        Me.txtServerNameIP.MaxLength = 50
        Me.txtServerNameIP.MendatroryField = False
        Me.txtServerNameIP.MyLinkLable1 = Me.MyLabel11
        Me.txtServerNameIP.MyLinkLable2 = Nothing
        Me.txtServerNameIP.Name = "txtServerNameIP"
        Me.txtServerNameIP.Size = New System.Drawing.Size(212, 20)
        Me.txtServerNameIP.TabIndex = 2
        '
        'MyLabel11
        '
        Me.MyLabel11.Location = New System.Drawing.Point(13, 25)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(127, 18)
        Me.MyLabel11.TabIndex = 1
        Me.MyLabel11.Text = "Server Name/IP Address"
        '
        'btnSaveSub
        '
        Me.btnSaveSub.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveSub.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveSub.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSaveSub.Location = New System.Drawing.Point(258, 5)
        Me.btnSaveSub.Name = "btnSaveSub"
        Me.btnSaveSub.Size = New System.Drawing.Size(137, 18)
        Me.btnSaveSub.TabIndex = 5
        Me.btnSaveSub.Text = "Save Subscription"
        '
        'btnSavePub
        '
        Me.btnSavePub.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSavePub.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSavePub.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSavePub.Location = New System.Drawing.Point(136, 5)
        Me.btnSavePub.Name = "btnSavePub"
        Me.btnSavePub.Size = New System.Drawing.Size(116, 18)
        Me.btnSavePub.TabIndex = 4
        Me.btnSavePub.Text = "Save Publication"
        '
        'btnSaveDist
        '
        Me.btnSaveDist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveDist.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveDist.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSaveDist.Location = New System.Drawing.Point(13, 5)
        Me.btnSaveDist.Name = "btnSaveDist"
        Me.btnSaveDist.Size = New System.Drawing.Size(117, 18)
        Me.btnSaveDist.TabIndex = 3
        Me.btnSaveDist.Text = "Save Distribution"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(898, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'frmReplicationSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(978, 490)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmReplicationSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageDistributor.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtDefSnapshotFolder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistributorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pagePublication.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txtDatabaseName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyTextBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPublisherName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTargetTables.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTargetTables, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageSubscription.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtServerRetypePassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerUserId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerSchema, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerDBName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerNameIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveSub, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSavePub, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveDist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageDistributor As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pagePublication As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageSubscription As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyTextBox6 As common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtPublisherName As common.Controls.MyTextBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents txtDatabaseName As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel

End Class

