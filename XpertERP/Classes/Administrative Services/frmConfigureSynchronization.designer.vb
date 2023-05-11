<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigureSynchronization
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfigureSynchronization))
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtLocName = New common.Controls.MyTextBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.fndLoc = New common.UserControls.txtFinder()
        Me.chkLapseUnAvailed = New Telerik.WinControls.UI.RadCheckBox()
        Me.dtpStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
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
        Me.btnTestServer = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem10 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem9 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem8 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem7 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.gvTargetTables = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.gvSourceTables = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtPrevConfDate = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLapseUnAvailed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
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
        CType(Me.btnTestServer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenuItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTargetTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSourceTables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrevConfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(396, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnSave.Location = New System.Drawing.Point(3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(85, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Configure"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.AccessibleDescription = "File"
        Me.RadMenuItemExport.AccessibleName = "File"
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "File"
        Me.RadMenuItem2.AccessibleName = "File"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "File"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnTestServer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(476, 484)
        Me.SplitContainer1.SplitterDistance = 453
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtPrevConfDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtLocName)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.fndLoc)
        Me.RadGroupBox1.Controls.Add(Me.chkLapseUnAvailed)
        Me.RadGroupBox1.Controls.Add(Me.dtpStartDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.HeaderText = "Configuration Settings"
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 211)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(399, 162)
        Me.RadGroupBox1.TabIndex = 30
        Me.RadGroupBox1.Text = "Configuration Settings"
        '
        'txtLocName
        '
        Me.txtLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLocName.CalculationExpression = Nothing
        Me.txtLocName.Enabled = False
        Me.txtLocName.FieldCode = Nothing
        Me.txtLocName.FieldDesc = Nothing
        Me.txtLocName.FieldMaxLength = 0
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.isCalculatedField = False
        Me.txtLocName.IsSourceFromTable = False
        Me.txtLocName.IsSourceFromValueList = False
        Me.txtLocName.IsUnique = False
        Me.txtLocName.Location = New System.Drawing.Point(145, 36)
        Me.txtLocName.MendatroryField = False
        Me.txtLocName.MyLinkLable1 = Nothing
        Me.txtLocName.MyLinkLable2 = Nothing
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.ReferenceFieldDesc = Nothing
        Me.txtLocName.ReferenceFieldName = Nothing
        Me.txtLocName.ReferenceTableName = Nothing
        Me.txtLocName.Size = New System.Drawing.Size(241, 20)
        Me.txtLocName.TabIndex = 282
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(14, 18)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(32, 16)
        Me.lblLocation.TabIndex = 283
        Me.lblLocation.Text = "MCC"
        '
        'fndLoc
        '
        Me.fndLoc.CalculationExpression = Nothing
        Me.fndLoc.FieldCode = Nothing
        Me.fndLoc.FieldDesc = Nothing
        Me.fndLoc.FieldMaxLength = 0
        Me.fndLoc.FieldName = Nothing
        Me.fndLoc.isCalculatedField = False
        Me.fndLoc.IsSourceFromTable = False
        Me.fndLoc.IsSourceFromValueList = False
        Me.fndLoc.IsUnique = False
        Me.fndLoc.Location = New System.Drawing.Point(145, 15)
        Me.fndLoc.MendatroryField = True
        Me.fndLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoc.MyLinkLable1 = Me.lblLocation
        Me.fndLoc.MyLinkLable2 = Nothing
        Me.fndLoc.MyReadOnly = False
        Me.fndLoc.MyShowMasterFormButton = False
        Me.fndLoc.Name = "fndLoc"
        Me.fndLoc.ReferenceFieldDesc = Nothing
        Me.fndLoc.ReferenceFieldName = Nothing
        Me.fndLoc.ReferenceTableName = Nothing
        Me.fndLoc.Size = New System.Drawing.Size(210, 19)
        Me.fndLoc.TabIndex = 281
        Me.fndLoc.Value = ""
        '
        'chkLapseUnAvailed
        '
        Me.chkLapseUnAvailed.Location = New System.Drawing.Point(145, 104)
        Me.chkLapseUnAvailed.Name = "chkLapseUnAvailed"
        Me.chkLapseUnAvailed.Size = New System.Drawing.Size(138, 18)
        Me.chkLapseUnAvailed.TabIndex = 30
        Me.chkLapseUnAvailed.Text = "Manualy Update Tables"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CalculationExpression = Nothing
        Me.dtpStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpStartDate.FieldCode = Nothing
        Me.dtpStartDate.FieldDesc = Nothing
        Me.dtpStartDate.FieldMaxLength = 0
        Me.dtpStartDate.FieldName = Nothing
        Me.dtpStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.isCalculatedField = False
        Me.dtpStartDate.IsSourceFromTable = False
        Me.dtpStartDate.IsSourceFromValueList = False
        Me.dtpStartDate.IsUnique = False
        Me.dtpStartDate.Location = New System.Drawing.Point(146, 80)
        Me.dtpStartDate.MendatroryField = True
        Me.dtpStartDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.MyLinkLable1 = Me.MyLabel2
        Me.dtpStartDate.MyLinkLable2 = Nothing
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.NullDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.ReferenceFieldDesc = Nothing
        Me.dtpStartDate.ReferenceFieldName = Nothing
        Me.dtpStartDate.ReferenceTableName = Nothing
        Me.dtpStartDate.Size = New System.Drawing.Size(142, 18)
        Me.dtpStartDate.TabIndex = 28
        Me.dtpStartDate.TabStop = False
        Me.dtpStartDate.Text = "07/06/2016"
        Me.dtpStartDate.Value = New Date(2016, 6, 7, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(13, 80)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel2.TabIndex = 29
        Me.MyLabel2.Text = "Start Date"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Location = New System.Drawing.Point(12, 89)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel16.TabIndex = 19
        '
        'RadButton1
        '
        Me.RadButton1.Image = CType(resources.GetObject("RadButton1.Image"), System.Drawing.Image)
        Me.RadButton1.Location = New System.Drawing.Point(352, -422)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(14, 20)
        Me.RadButton1.TabIndex = 17
        Me.RadButton1.Text = " "
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(399, 202)
        Me.RadGroupBox2.TabIndex = 11
        Me.RadGroupBox2.Text = "Server Settings"
        '
        'txtServerRemarks
        '
        Me.txtServerRemarks.CalculationExpression = Nothing
        Me.txtServerRemarks.Enabled = False
        Me.txtServerRemarks.FieldCode = Nothing
        Me.txtServerRemarks.FieldDesc = Nothing
        Me.txtServerRemarks.FieldMaxLength = 0
        Me.txtServerRemarks.FieldName = Nothing
        Me.txtServerRemarks.isCalculatedField = False
        Me.txtServerRemarks.IsSourceFromTable = False
        Me.txtServerRemarks.IsSourceFromValueList = False
        Me.txtServerRemarks.IsUnique = False
        Me.txtServerRemarks.Location = New System.Drawing.Point(146, 149)
        Me.txtServerRemarks.MaxLength = 50
        Me.txtServerRemarks.MendatroryField = False
        Me.txtServerRemarks.MyLinkLable1 = Me.MyLabel5
        Me.txtServerRemarks.MyLinkLable2 = Nothing
        Me.txtServerRemarks.Name = "txtServerRemarks"
        Me.txtServerRemarks.ReferenceFieldDesc = Nothing
        Me.txtServerRemarks.ReferenceFieldName = Nothing
        Me.txtServerRemarks.ReferenceTableName = Nothing
        Me.txtServerRemarks.Size = New System.Drawing.Size(212, 20)
        Me.txtServerRemarks.TabIndex = 7
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(16, 151)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel5.TabIndex = 28
        Me.MyLabel5.Text = "Remarks"
        '
        'txtServerPassword
        '
        Me.txtServerPassword.CalculationExpression = Nothing
        Me.txtServerPassword.Enabled = False
        Me.txtServerPassword.FieldCode = Nothing
        Me.txtServerPassword.FieldDesc = Nothing
        Me.txtServerPassword.FieldMaxLength = 0
        Me.txtServerPassword.FieldName = Nothing
        Me.txtServerPassword.isCalculatedField = False
        Me.txtServerPassword.IsSourceFromTable = False
        Me.txtServerPassword.IsSourceFromValueList = False
        Me.txtServerPassword.IsUnique = False
        Me.txtServerPassword.Location = New System.Drawing.Point(146, 123)
        Me.txtServerPassword.MaxLength = 50
        Me.txtServerPassword.MendatroryField = False
        Me.txtServerPassword.MyLinkLable1 = Me.MyLabel6
        Me.txtServerPassword.MyLinkLable2 = Nothing
        Me.txtServerPassword.Name = "txtServerPassword"
        Me.txtServerPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtServerPassword.ReferenceFieldDesc = Nothing
        Me.txtServerPassword.ReferenceFieldName = Nothing
        Me.txtServerPassword.ReferenceTableName = Nothing
        Me.txtServerPassword.Size = New System.Drawing.Size(212, 20)
        Me.txtServerPassword.TabIndex = 6
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(16, 125)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel6.TabIndex = 25
        Me.MyLabel6.Text = "Password"
        '
        'txtServerUserId
        '
        Me.txtServerUserId.CalculationExpression = Nothing
        Me.txtServerUserId.Enabled = False
        Me.txtServerUserId.FieldCode = Nothing
        Me.txtServerUserId.FieldDesc = Nothing
        Me.txtServerUserId.FieldMaxLength = 0
        Me.txtServerUserId.FieldName = Nothing
        Me.txtServerUserId.isCalculatedField = False
        Me.txtServerUserId.IsSourceFromTable = False
        Me.txtServerUserId.IsSourceFromValueList = False
        Me.txtServerUserId.IsUnique = False
        Me.txtServerUserId.Location = New System.Drawing.Point(146, 98)
        Me.txtServerUserId.MaxLength = 50
        Me.txtServerUserId.MendatroryField = False
        Me.txtServerUserId.MyLinkLable1 = Me.MyLabel7
        Me.txtServerUserId.MyLinkLable2 = Nothing
        Me.txtServerUserId.Name = "txtServerUserId"
        Me.txtServerUserId.ReferenceFieldDesc = Nothing
        Me.txtServerUserId.ReferenceFieldName = Nothing
        Me.txtServerUserId.ReferenceTableName = Nothing
        Me.txtServerUserId.Size = New System.Drawing.Size(212, 20)
        Me.txtServerUserId.TabIndex = 5
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(14, 100)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(41, 18)
        Me.MyLabel7.TabIndex = 24
        Me.MyLabel7.Text = "User Id"
        '
        'txtServerSchema
        '
        Me.txtServerSchema.CalculationExpression = Nothing
        Me.txtServerSchema.Enabled = False
        Me.txtServerSchema.FieldCode = Nothing
        Me.txtServerSchema.FieldDesc = Nothing
        Me.txtServerSchema.FieldMaxLength = 0
        Me.txtServerSchema.FieldName = Nothing
        Me.txtServerSchema.isCalculatedField = False
        Me.txtServerSchema.IsSourceFromTable = False
        Me.txtServerSchema.IsSourceFromValueList = False
        Me.txtServerSchema.IsUnique = False
        Me.txtServerSchema.Location = New System.Drawing.Point(146, 73)
        Me.txtServerSchema.MaxLength = 50
        Me.txtServerSchema.MendatroryField = False
        Me.txtServerSchema.MyLinkLable1 = Me.MyLabel8
        Me.txtServerSchema.MyLinkLable2 = Nothing
        Me.txtServerSchema.Name = "txtServerSchema"
        Me.txtServerSchema.ReferenceFieldDesc = Nothing
        Me.txtServerSchema.ReferenceFieldName = Nothing
        Me.txtServerSchema.ReferenceTableName = Nothing
        Me.txtServerSchema.Size = New System.Drawing.Size(212, 20)
        Me.txtServerSchema.TabIndex = 4
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(14, 75)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel8.TabIndex = 21
        Me.MyLabel8.Text = "Schema Name"
        '
        'txtServerDBName
        '
        Me.txtServerDBName.CalculationExpression = Nothing
        Me.txtServerDBName.Enabled = False
        Me.txtServerDBName.FieldCode = Nothing
        Me.txtServerDBName.FieldDesc = Nothing
        Me.txtServerDBName.FieldMaxLength = 0
        Me.txtServerDBName.FieldName = Nothing
        Me.txtServerDBName.isCalculatedField = False
        Me.txtServerDBName.IsSourceFromTable = False
        Me.txtServerDBName.IsSourceFromValueList = False
        Me.txtServerDBName.IsUnique = False
        Me.txtServerDBName.Location = New System.Drawing.Point(146, 48)
        Me.txtServerDBName.MaxLength = 50
        Me.txtServerDBName.MendatroryField = False
        Me.txtServerDBName.MyLinkLable1 = Me.MyLabel9
        Me.txtServerDBName.MyLinkLable2 = Nothing
        Me.txtServerDBName.Name = "txtServerDBName"
        Me.txtServerDBName.ReferenceFieldDesc = Nothing
        Me.txtServerDBName.ReferenceFieldName = Nothing
        Me.txtServerDBName.ReferenceTableName = Nothing
        Me.txtServerDBName.Size = New System.Drawing.Size(212, 20)
        Me.txtServerDBName.TabIndex = 3
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(13, 50)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(86, 18)
        Me.MyLabel9.TabIndex = 20
        Me.MyLabel9.Text = "Database Name"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(12, 89)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel10.TabIndex = 19
        '
        'RadButton2
        '
        Me.RadButton2.Image = CType(resources.GetObject("RadButton2.Image"), System.Drawing.Image)
        Me.RadButton2.Location = New System.Drawing.Point(352, -422)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(14, 20)
        Me.RadButton2.TabIndex = 17
        Me.RadButton2.Text = " "
        '
        'txtServerNameIP
        '
        Me.txtServerNameIP.CalculationExpression = Nothing
        Me.txtServerNameIP.Enabled = False
        Me.txtServerNameIP.FieldCode = Nothing
        Me.txtServerNameIP.FieldDesc = Nothing
        Me.txtServerNameIP.FieldMaxLength = 0
        Me.txtServerNameIP.FieldName = Nothing
        Me.txtServerNameIP.isCalculatedField = False
        Me.txtServerNameIP.IsSourceFromTable = False
        Me.txtServerNameIP.IsSourceFromValueList = False
        Me.txtServerNameIP.IsUnique = False
        Me.txtServerNameIP.Location = New System.Drawing.Point(146, 23)
        Me.txtServerNameIP.MaxLength = 50
        Me.txtServerNameIP.MendatroryField = False
        Me.txtServerNameIP.MyLinkLable1 = Me.MyLabel11
        Me.txtServerNameIP.MyLinkLable2 = Nothing
        Me.txtServerNameIP.Name = "txtServerNameIP"
        Me.txtServerNameIP.ReferenceFieldDesc = Nothing
        Me.txtServerNameIP.ReferenceFieldName = Nothing
        Me.txtServerNameIP.ReferenceTableName = Nothing
        Me.txtServerNameIP.Size = New System.Drawing.Size(212, 20)
        Me.txtServerNameIP.TabIndex = 2
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(13, 25)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(127, 18)
        Me.MyLabel11.TabIndex = 1
        Me.MyLabel11.Text = "Server Name/IP Address"
        '
        'btnTestServer
        '
        Me.btnTestServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTestServer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTestServer.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnTestServer.Location = New System.Drawing.Point(94, 6)
        Me.btnTestServer.Name = "btnTestServer"
        Me.btnTestServer.Size = New System.Drawing.Size(137, 18)
        Me.btnTestServer.TabIndex = 4
        Me.btnTestServer.Text = "Test Server Connection"
        '
        'RadMenuItem10
        '
        Me.RadMenuItem10.AnimationEnabled = True
        Me.RadMenuItem10.AnimationFrames = 1
        Me.RadMenuItem10.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem10.AutoSize = True
        Me.RadMenuItem10.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem10.DropShadow = True
        Me.RadMenuItem10.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem10.EnableAeroEffects = False
        Me.RadMenuItem10.FadeAnimationFrames = 10
        Me.RadMenuItem10.FadeAnimationSpeed = 10
        Me.RadMenuItem10.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem10.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem10.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem10.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem10.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem10.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem10.Name = "RadMenuItem10"
        Me.RadMenuItem10.Opacity = 1.0!
        Me.RadMenuItem10.ProcessKeyboard = False
        Me.RadMenuItem10.RollOverItemSelection = True
        Me.RadMenuItem10.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem10.TabIndex = 0
        Me.RadMenuItem10.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem10.Visible = False
        '
        'RadMenuItem9
        '
        Me.RadMenuItem9.AnimationEnabled = True
        Me.RadMenuItem9.AnimationFrames = 1
        Me.RadMenuItem9.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem9.AutoSize = True
        Me.RadMenuItem9.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem9.DropShadow = True
        Me.RadMenuItem9.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem9.EnableAeroEffects = False
        Me.RadMenuItem9.FadeAnimationFrames = 10
        Me.RadMenuItem9.FadeAnimationSpeed = 10
        Me.RadMenuItem9.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem9.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem9.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem9.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem9.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem9.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem9.Name = "RadMenuItem9"
        Me.RadMenuItem9.Opacity = 1.0!
        Me.RadMenuItem9.ProcessKeyboard = False
        Me.RadMenuItem9.RollOverItemSelection = True
        Me.RadMenuItem9.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem9.TabIndex = 0
        Me.RadMenuItem9.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem9.Visible = False
        '
        'RadMenuItem8
        '
        Me.RadMenuItem8.AnimationEnabled = True
        Me.RadMenuItem8.AnimationFrames = 1
        Me.RadMenuItem8.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem8.AutoSize = True
        Me.RadMenuItem8.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem8.DropShadow = True
        Me.RadMenuItem8.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem8.EnableAeroEffects = False
        Me.RadMenuItem8.FadeAnimationFrames = 10
        Me.RadMenuItem8.FadeAnimationSpeed = 10
        Me.RadMenuItem8.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem8.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem8.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem8.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem8.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem8.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem8.Name = "RadMenuItem8"
        Me.RadMenuItem8.Opacity = 1.0!
        Me.RadMenuItem8.ProcessKeyboard = False
        Me.RadMenuItem8.RollOverItemSelection = True
        Me.RadMenuItem8.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem8.TabIndex = 0
        Me.RadMenuItem8.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem8.Visible = False
        '
        'RadMenuItem7
        '
        Me.RadMenuItem7.AnimationEnabled = True
        Me.RadMenuItem7.AnimationFrames = 1
        Me.RadMenuItem7.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem7.AutoSize = True
        Me.RadMenuItem7.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem7.DropShadow = True
        Me.RadMenuItem7.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem7.EnableAeroEffects = False
        Me.RadMenuItem7.FadeAnimationFrames = 10
        Me.RadMenuItem7.FadeAnimationSpeed = 10
        Me.RadMenuItem7.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem7.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem7.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem7.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem7.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem7.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem7.Name = "RadMenuItem7"
        Me.RadMenuItem7.Opacity = 1.0!
        Me.RadMenuItem7.ProcessKeyboard = False
        Me.RadMenuItem7.RollOverItemSelection = True
        Me.RadMenuItem7.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem7.TabIndex = 0
        Me.RadMenuItem7.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem7.Visible = False
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AnimationEnabled = True
        Me.RadMenuItem6.AnimationFrames = 1
        Me.RadMenuItem6.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem6.AutoSize = True
        Me.RadMenuItem6.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem6.DropShadow = True
        Me.RadMenuItem6.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem6.EnableAeroEffects = False
        Me.RadMenuItem6.FadeAnimationFrames = 10
        Me.RadMenuItem6.FadeAnimationSpeed = 10
        Me.RadMenuItem6.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem6.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem6.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem6.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem6.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem6.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Opacity = 1.0!
        Me.RadMenuItem6.ProcessKeyboard = False
        Me.RadMenuItem6.RollOverItemSelection = True
        Me.RadMenuItem6.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem6.TabIndex = 0
        Me.RadMenuItem6.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem6.Visible = False
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AnimationEnabled = True
        Me.RadMenuItem5.AnimationFrames = 1
        Me.RadMenuItem5.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem5.AutoSize = True
        Me.RadMenuItem5.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem5.DropShadow = True
        Me.RadMenuItem5.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem5.EnableAeroEffects = False
        Me.RadMenuItem5.FadeAnimationFrames = 10
        Me.RadMenuItem5.FadeAnimationSpeed = 10
        Me.RadMenuItem5.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem5.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem5.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem5.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem5.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem5.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Opacity = 1.0!
        Me.RadMenuItem5.ProcessKeyboard = False
        Me.RadMenuItem5.RollOverItemSelection = True
        Me.RadMenuItem5.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem5.TabIndex = 0
        Me.RadMenuItem5.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem5.Visible = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AnimationEnabled = True
        Me.RadMenuItem4.AnimationFrames = 1
        Me.RadMenuItem4.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem4.AutoSize = True
        Me.RadMenuItem4.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem4.DropShadow = True
        Me.RadMenuItem4.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem4.EnableAeroEffects = False
        Me.RadMenuItem4.FadeAnimationFrames = 10
        Me.RadMenuItem4.FadeAnimationSpeed = 10
        Me.RadMenuItem4.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem4.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem4.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem4.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem4.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem4.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Opacity = 1.0!
        Me.RadMenuItem4.ProcessKeyboard = False
        Me.RadMenuItem4.RollOverItemSelection = True
        Me.RadMenuItem4.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem4.TabIndex = 0
        Me.RadMenuItem4.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem4.Visible = False
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AnimationEnabled = True
        Me.RadMenuItem3.AnimationFrames = 1
        Me.RadMenuItem3.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.Fade
        Me.RadMenuItem3.AutoSize = True
        Me.RadMenuItem3.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem3.DropShadow = True
        Me.RadMenuItem3.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem3.EnableAeroEffects = False
        Me.RadMenuItem3.FadeAnimationFrames = 10
        Me.RadMenuItem3.FadeAnimationSpeed = 10
        Me.RadMenuItem3.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem3.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem3.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem3.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem3.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem3.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Opacity = 1.0!
        Me.RadMenuItem3.ProcessKeyboard = False
        Me.RadMenuItem3.RollOverItemSelection = True
        Me.RadMenuItem3.Size = New System.Drawing.Size(0, 0)
        Me.RadMenuItem3.TabIndex = 0
        Me.RadMenuItem3.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem3.Visible = False
        '
        'gvTargetTables
        '
        Me.gvTargetTables.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvTargetTables.AllowAddNewRow = False
        Me.gvTargetTables.AutoGenerateColumns = False
        Me.gvTargetTables.EnableCustomFiltering = True
        Me.gvTargetTables.EnableFiltering = True
        Me.gvTargetTables.EnableGrouping = False
        Me.gvTargetTables.ShowHeaderCellButtons = True
        '
        'gvSourceTables
        '
        Me.gvSourceTables.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvSourceTables.AllowAddNewRow = False
        Me.gvSourceTables.AutoGenerateColumns = False
        Me.gvSourceTables.EnableCustomFiltering = True
        Me.gvSourceTables.EnableFiltering = True
        Me.gvSourceTables.EnableGrouping = False
        Me.gvSourceTables.ShowHeaderCellButtons = True
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(13, 60)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(79, 18)
        Me.MyLabel1.TabIndex = 285
        Me.MyLabel1.Text = "Last Start Date"
        '
        'txtPrevConfDate
        '
        Me.txtPrevConfDate.CalculationExpression = Nothing
        Me.txtPrevConfDate.FieldCode = Nothing
        Me.txtPrevConfDate.FieldDesc = Nothing
        Me.txtPrevConfDate.FieldMaxLength = 0
        Me.txtPrevConfDate.FieldName = Nothing
        Me.txtPrevConfDate.isCalculatedField = False
        Me.txtPrevConfDate.IsSourceFromTable = False
        Me.txtPrevConfDate.IsSourceFromValueList = False
        Me.txtPrevConfDate.IsUnique = False
        Me.txtPrevConfDate.Location = New System.Drawing.Point(146, 58)
        Me.txtPrevConfDate.MaxLength = 50
        Me.txtPrevConfDate.MendatroryField = False
        Me.txtPrevConfDate.MyLinkLable1 = Me.MyLabel11
        Me.txtPrevConfDate.MyLinkLable2 = Nothing
        Me.txtPrevConfDate.Name = "txtPrevConfDate"
        Me.txtPrevConfDate.ReferenceFieldDesc = Nothing
        Me.txtPrevConfDate.ReferenceFieldName = Nothing
        Me.txtPrevConfDate.ReferenceTableName = Nothing
        Me.txtPrevConfDate.Size = New System.Drawing.Size(212, 20)
        Me.txtPrevConfDate.TabIndex = 286
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(14, 40)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel3.TabIndex = 287
        Me.MyLabel3.Text = "MCC Name"
        '
        'frmConfigureSynchronization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 484)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmConfigureSynchronization"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Configure MCC Data Configuration"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLapseUnAvailed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
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
        CType(Me.btnTestServer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenuItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTargetTables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSourceTables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrevConfDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnTestServer As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem10 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem9 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem8 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem7 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
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
    Friend WithEvents gvTargetTables As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents gvSourceTables As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents dtpStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkLapseUnAvailed As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtLocName As common.Controls.MyTextBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents fndLoc As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtPrevConfDate As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

