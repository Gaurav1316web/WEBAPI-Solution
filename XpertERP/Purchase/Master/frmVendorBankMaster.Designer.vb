<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorBankMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVendorBankMaster))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.txtIFSCCode = New common.Controls.MyTextBox()
        Me.lblIFSCCode = New common.Controls.MyLabel()
        Me.txtBranchName = New common.Controls.MyTextBox()
        Me.lblBranchName = New common.Controls.MyLabel()
        Me.txtBranchCode = New common.Controls.MyTextBox()
        Me.lblBranchCode = New common.Controls.MyLabel()
        Me.txtcityName = New common.Controls.MyLabel()
        Me.txtstateName = New common.Controls.MyLabel()
        Me.txtcountryName = New common.Controls.MyLabel()
        Me.fndCountry = New common.UserControls.txtFinder()
        Me.lblCountry = New common.Controls.MyLabel()
        Me.fndstate = New common.UserControls.txtFinder()
        Me.lblState = New common.Controls.MyLabel()
        Me.fndCity = New common.UserControls.txtFinder()
        Me.lblCity = New common.Controls.MyLabel()
        Me.txtAdd2 = New common.Controls.MyTextBox()
        Me.lblAddress = New common.Controls.MyLabel()
        Me.txtAdd1 = New common.Controls.MyTextBox()
        Me.txtAdd3 = New common.Controls.MyTextBox()
        Me.txtBankName = New common.Controls.MyTextBox()
        Me.lblBankName = New common.Controls.MyLabel()
        Me.lblBankCode = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.fndBankCode = New common.UserControls.txtNavigator()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDImportBankDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDImportBranchDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDExportBankDetails = New Telerik.WinControls.UI.RadMenuItem()
        Me.RDExportBranchDetails = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIFSCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBranchCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtstateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcountryName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(703, 491)
        Me.SplitContainer1.SplitterDistance = 453
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(703, 433)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtIFSCCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblIFSCCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtBranchName)
        Me.RadPageViewPage1.Controls.Add(Me.lblBranchName)
        Me.RadPageViewPage1.Controls.Add(Me.txtBranchCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblBranchCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtcityName)
        Me.RadPageViewPage1.Controls.Add(Me.txtstateName)
        Me.RadPageViewPage1.Controls.Add(Me.txtcountryName)
        Me.RadPageViewPage1.Controls.Add(Me.fndCountry)
        Me.RadPageViewPage1.Controls.Add(Me.lblCountry)
        Me.RadPageViewPage1.Controls.Add(Me.fndstate)
        Me.RadPageViewPage1.Controls.Add(Me.fndCity)
        Me.RadPageViewPage1.Controls.Add(Me.lblCity)
        Me.RadPageViewPage1.Controls.Add(Me.lblState)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd2)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd1)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdd3)
        Me.RadPageViewPage1.Controls.Add(Me.txtBankName)
        Me.RadPageViewPage1.Controls.Add(Me.lblAddress)
        Me.RadPageViewPage1.Controls.Add(Me.lblBankName)
        Me.RadPageViewPage1.Controls.Add(Me.lblBankCode)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.fndBankCode)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(682, 385)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Branch Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(1, 253)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(678, 130)
        Me.RadGroupBox2.TabIndex = 1373
        Me.RadGroupBox2.Text = "Branch Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(658, 100)
        Me.gv1.TabIndex = 13
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'txtIFSCCode
        '
        Me.txtIFSCCode.CalculationExpression = Nothing
        Me.txtIFSCCode.FieldCode = Nothing
        Me.txtIFSCCode.FieldDesc = Nothing
        Me.txtIFSCCode.FieldMaxLength = 0
        Me.txtIFSCCode.FieldName = Nothing
        Me.txtIFSCCode.isCalculatedField = False
        Me.txtIFSCCode.IsSourceFromTable = False
        Me.txtIFSCCode.IsSourceFromValueList = False
        Me.txtIFSCCode.IsUnique = False
        Me.txtIFSCCode.Location = New System.Drawing.Point(603, 229)
        Me.txtIFSCCode.MaxLength = 150
        Me.txtIFSCCode.MendatroryField = False
        Me.txtIFSCCode.MyLinkLable1 = Me.lblIFSCCode
        Me.txtIFSCCode.MyLinkLable2 = Nothing
        Me.txtIFSCCode.Name = "txtIFSCCode"
        Me.txtIFSCCode.ReferenceFieldDesc = Nothing
        Me.txtIFSCCode.ReferenceFieldName = Nothing
        Me.txtIFSCCode.ReferenceTableName = Nothing
        Me.txtIFSCCode.Size = New System.Drawing.Size(53, 20)
        Me.txtIFSCCode.TabIndex = 1371
        Me.txtIFSCCode.Visible = False
        '
        'lblIFSCCode
        '
        Me.lblIFSCCode.FieldName = Nothing
        Me.lblIFSCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIFSCCode.Location = New System.Drawing.Point(492, 233)
        Me.lblIFSCCode.Name = "lblIFSCCode"
        Me.lblIFSCCode.Size = New System.Drawing.Size(62, 16)
        Me.lblIFSCCode.TabIndex = 1372
        Me.lblIFSCCode.Text = "IFSC Code"
        Me.lblIFSCCode.Visible = False
        '
        'txtBranchName
        '
        Me.txtBranchName.CalculationExpression = Nothing
        Me.txtBranchName.FieldCode = Nothing
        Me.txtBranchName.FieldDesc = Nothing
        Me.txtBranchName.FieldMaxLength = 0
        Me.txtBranchName.FieldName = Nothing
        Me.txtBranchName.isCalculatedField = False
        Me.txtBranchName.IsSourceFromTable = False
        Me.txtBranchName.IsSourceFromValueList = False
        Me.txtBranchName.IsUnique = False
        Me.txtBranchName.Location = New System.Drawing.Point(560, 228)
        Me.txtBranchName.MaxLength = 150
        Me.txtBranchName.MendatroryField = False
        Me.txtBranchName.MyLinkLable1 = Me.lblBranchName
        Me.txtBranchName.MyLinkLable2 = Nothing
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.ReferenceFieldDesc = Nothing
        Me.txtBranchName.ReferenceFieldName = Nothing
        Me.txtBranchName.ReferenceTableName = Nothing
        Me.txtBranchName.Size = New System.Drawing.Size(29, 20)
        Me.txtBranchName.TabIndex = 1369
        Me.txtBranchName.Visible = False
        '
        'lblBranchName
        '
        Me.lblBranchName.FieldName = Nothing
        Me.lblBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranchName.Location = New System.Drawing.Point(589, 208)
        Me.lblBranchName.Name = "lblBranchName"
        Me.lblBranchName.Size = New System.Drawing.Size(75, 16)
        Me.lblBranchName.TabIndex = 1370
        Me.lblBranchName.Text = "Branch Name"
        Me.lblBranchName.Visible = False
        '
        'txtBranchCode
        '
        Me.txtBranchCode.CalculationExpression = Nothing
        Me.txtBranchCode.FieldCode = Nothing
        Me.txtBranchCode.FieldDesc = Nothing
        Me.txtBranchCode.FieldMaxLength = 0
        Me.txtBranchCode.FieldName = Nothing
        Me.txtBranchCode.isCalculatedField = False
        Me.txtBranchCode.IsSourceFromTable = False
        Me.txtBranchCode.IsSourceFromValueList = False
        Me.txtBranchCode.IsUnique = False
        Me.txtBranchCode.Location = New System.Drawing.Point(435, 226)
        Me.txtBranchCode.MaxLength = 150
        Me.txtBranchCode.MendatroryField = False
        Me.txtBranchCode.MyLinkLable1 = Me.lblBranchCode
        Me.txtBranchCode.MyLinkLable2 = Nothing
        Me.txtBranchCode.Name = "txtBranchCode"
        Me.txtBranchCode.ReferenceFieldDesc = Nothing
        Me.txtBranchCode.ReferenceFieldName = Nothing
        Me.txtBranchCode.ReferenceTableName = Nothing
        Me.txtBranchCode.Size = New System.Drawing.Size(30, 20)
        Me.txtBranchCode.TabIndex = 1367
        Me.txtBranchCode.Visible = False
        '
        'lblBranchCode
        '
        Me.lblBranchCode.FieldName = Nothing
        Me.lblBranchCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranchCode.Location = New System.Drawing.Point(482, 211)
        Me.lblBranchCode.Name = "lblBranchCode"
        Me.lblBranchCode.Size = New System.Drawing.Size(72, 16)
        Me.lblBranchCode.TabIndex = 1368
        Me.lblBranchCode.Text = "Branch Code"
        Me.lblBranchCode.Visible = False
        '
        'txtcityName
        '
        Me.txtcityName.AutoSize = False
        Me.txtcityName.BorderVisible = True
        Me.txtcityName.FieldName = Nothing
        Me.txtcityName.Location = New System.Drawing.Point(264, 182)
        Me.txtcityName.Name = "txtcityName"
        Me.txtcityName.Size = New System.Drawing.Size(411, 19)
        Me.txtcityName.TabIndex = 1366
        Me.txtcityName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtstateName
        '
        Me.txtstateName.AutoSize = False
        Me.txtstateName.BorderVisible = True
        Me.txtstateName.FieldName = Nothing
        Me.txtstateName.Location = New System.Drawing.Point(264, 159)
        Me.txtstateName.Name = "txtstateName"
        Me.txtstateName.Size = New System.Drawing.Size(411, 19)
        Me.txtstateName.TabIndex = 1366
        Me.txtstateName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcountryName
        '
        Me.txtcountryName.AutoSize = False
        Me.txtcountryName.BorderVisible = True
        Me.txtcountryName.FieldName = Nothing
        Me.txtcountryName.Location = New System.Drawing.Point(264, 136)
        Me.txtcountryName.Name = "txtcountryName"
        Me.txtcountryName.Size = New System.Drawing.Size(411, 19)
        Me.txtcountryName.TabIndex = 1365
        Me.txtcountryName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndCountry
        '
        Me.fndCountry.CalculationExpression = Nothing
        Me.fndCountry.FieldCode = Nothing
        Me.fndCountry.FieldDesc = Nothing
        Me.fndCountry.FieldMaxLength = 0
        Me.fndCountry.FieldName = Nothing
        Me.fndCountry.isCalculatedField = False
        Me.fndCountry.IsSourceFromTable = False
        Me.fndCountry.IsSourceFromValueList = False
        Me.fndCountry.IsUnique = False
        Me.fndCountry.Location = New System.Drawing.Point(118, 136)
        Me.fndCountry.MendatroryField = False
        Me.fndCountry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCountry.MyLinkLable1 = Me.lblCountry
        Me.fndCountry.MyLinkLable2 = Me.txtcountryName
        Me.fndCountry.MyReadOnly = False
        Me.fndCountry.MyShowMasterFormButton = False
        Me.fndCountry.Name = "fndCountry"
        Me.fndCountry.ReferenceFieldDesc = Nothing
        Me.fndCountry.ReferenceFieldName = Nothing
        Me.fndCountry.ReferenceTableName = Nothing
        Me.fndCountry.Size = New System.Drawing.Size(143, 19)
        Me.fndCountry.TabIndex = 6
        Me.fndCountry.Value = ""
        '
        'lblCountry
        '
        Me.lblCountry.FieldName = Nothing
        Me.lblCountry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountry.Location = New System.Drawing.Point(3, 139)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(46, 16)
        Me.lblCountry.TabIndex = 1364
        Me.lblCountry.Text = "Country"
        '
        'fndstate
        '
        Me.fndstate.CalculationExpression = Nothing
        Me.fndstate.FieldCode = Nothing
        Me.fndstate.FieldDesc = Nothing
        Me.fndstate.FieldMaxLength = 0
        Me.fndstate.FieldName = Nothing
        Me.fndstate.isCalculatedField = False
        Me.fndstate.IsSourceFromTable = False
        Me.fndstate.IsSourceFromValueList = False
        Me.fndstate.IsUnique = False
        Me.fndstate.Location = New System.Drawing.Point(118, 159)
        Me.fndstate.MendatroryField = False
        Me.fndstate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndstate.MyLinkLable1 = Me.lblState
        Me.fndstate.MyLinkLable2 = Me.txtstateName
        Me.fndstate.MyReadOnly = False
        Me.fndstate.MyShowMasterFormButton = False
        Me.fndstate.Name = "fndstate"
        Me.fndstate.ReferenceFieldDesc = Nothing
        Me.fndstate.ReferenceFieldName = Nothing
        Me.fndstate.ReferenceTableName = Nothing
        Me.fndstate.Size = New System.Drawing.Size(143, 19)
        Me.fndstate.TabIndex = 7
        Me.fndstate.Value = ""
        '
        'lblState
        '
        Me.lblState.FieldName = Nothing
        Me.lblState.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.Location = New System.Drawing.Point(3, 162)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(33, 16)
        Me.lblState.TabIndex = 1363
        Me.lblState.Text = "State"
        '
        'fndCity
        '
        Me.fndCity.CalculationExpression = Nothing
        Me.fndCity.FieldCode = Nothing
        Me.fndCity.FieldDesc = Nothing
        Me.fndCity.FieldMaxLength = 0
        Me.fndCity.FieldName = Nothing
        Me.fndCity.isCalculatedField = False
        Me.fndCity.IsSourceFromTable = False
        Me.fndCity.IsSourceFromValueList = False
        Me.fndCity.IsUnique = False
        Me.fndCity.Location = New System.Drawing.Point(118, 182)
        Me.fndCity.MendatroryField = False
        Me.fndCity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCity.MyLinkLable1 = Me.lblCity
        Me.fndCity.MyLinkLable2 = Me.txtcityName
        Me.fndCity.MyReadOnly = False
        Me.fndCity.MyShowMasterFormButton = False
        Me.fndCity.Name = "fndCity"
        Me.fndCity.ReferenceFieldDesc = Nothing
        Me.fndCity.ReferenceFieldName = Nothing
        Me.fndCity.ReferenceTableName = Nothing
        Me.fndCity.Size = New System.Drawing.Size(143, 19)
        Me.fndCity.TabIndex = 8
        Me.fndCity.Value = ""
        '
        'lblCity
        '
        Me.lblCity.FieldName = Nothing
        Me.lblCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.Location = New System.Drawing.Point(3, 185)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(26, 16)
        Me.lblCity.TabIndex = 1362
        Me.lblCity.Text = "City"
        '
        'txtAdd2
        '
        Me.txtAdd2.CalculationExpression = Nothing
        Me.txtAdd2.FieldCode = Nothing
        Me.txtAdd2.FieldDesc = Nothing
        Me.txtAdd2.FieldMaxLength = 0
        Me.txtAdd2.FieldName = Nothing
        Me.txtAdd2.isCalculatedField = False
        Me.txtAdd2.IsSourceFromTable = False
        Me.txtAdd2.IsSourceFromValueList = False
        Me.txtAdd2.IsUnique = False
        Me.txtAdd2.Location = New System.Drawing.Point(118, 88)
        Me.txtAdd2.MaxLength = 75
        Me.txtAdd2.MendatroryField = False
        Me.txtAdd2.MyLinkLable1 = Me.lblAddress
        Me.txtAdd2.MyLinkLable2 = Nothing
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.ReferenceFieldDesc = Nothing
        Me.txtAdd2.ReferenceFieldName = Nothing
        Me.txtAdd2.ReferenceTableName = Nothing
        Me.txtAdd2.Size = New System.Drawing.Size(557, 20)
        Me.txtAdd2.TabIndex = 4
        '
        'lblAddress
        '
        Me.lblAddress.FieldName = Nothing
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(3, 64)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblAddress.TabIndex = 44
        Me.lblAddress.Text = "Address"
        '
        'txtAdd1
        '
        Me.txtAdd1.CalculationExpression = Nothing
        Me.txtAdd1.FieldCode = Nothing
        Me.txtAdd1.FieldDesc = Nothing
        Me.txtAdd1.FieldMaxLength = 0
        Me.txtAdd1.FieldName = Nothing
        Me.txtAdd1.isCalculatedField = False
        Me.txtAdd1.IsSourceFromTable = False
        Me.txtAdd1.IsSourceFromValueList = False
        Me.txtAdd1.IsUnique = False
        Me.txtAdd1.Location = New System.Drawing.Point(118, 65)
        Me.txtAdd1.MaxLength = 150
        Me.txtAdd1.MendatroryField = False
        Me.txtAdd1.MyLinkLable1 = Me.lblAddress
        Me.txtAdd1.MyLinkLable2 = Nothing
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.ReferenceFieldDesc = Nothing
        Me.txtAdd1.ReferenceFieldName = Nothing
        Me.txtAdd1.ReferenceTableName = Nothing
        Me.txtAdd1.Size = New System.Drawing.Size(557, 20)
        Me.txtAdd1.TabIndex = 3
        '
        'txtAdd3
        '
        Me.txtAdd3.CalculationExpression = Nothing
        Me.txtAdd3.FieldCode = Nothing
        Me.txtAdd3.FieldDesc = Nothing
        Me.txtAdd3.FieldMaxLength = 0
        Me.txtAdd3.FieldName = Nothing
        Me.txtAdd3.isCalculatedField = False
        Me.txtAdd3.IsSourceFromTable = False
        Me.txtAdd3.IsSourceFromValueList = False
        Me.txtAdd3.IsUnique = False
        Me.txtAdd3.Location = New System.Drawing.Point(118, 112)
        Me.txtAdd3.MaxLength = 75
        Me.txtAdd3.MendatroryField = False
        Me.txtAdd3.MyLinkLable1 = Me.lblAddress
        Me.txtAdd3.MyLinkLable2 = Nothing
        Me.txtAdd3.Name = "txtAdd3"
        Me.txtAdd3.ReferenceFieldDesc = Nothing
        Me.txtAdd3.ReferenceFieldName = Nothing
        Me.txtAdd3.ReferenceTableName = Nothing
        Me.txtAdd3.Size = New System.Drawing.Size(557, 20)
        Me.txtAdd3.TabIndex = 5
        '
        'txtBankName
        '
        Me.txtBankName.CalculationExpression = Nothing
        Me.txtBankName.FieldCode = Nothing
        Me.txtBankName.FieldDesc = Nothing
        Me.txtBankName.FieldMaxLength = 0
        Me.txtBankName.FieldName = Nothing
        Me.txtBankName.isCalculatedField = False
        Me.txtBankName.IsSourceFromTable = False
        Me.txtBankName.IsSourceFromValueList = False
        Me.txtBankName.IsUnique = False
        Me.txtBankName.Location = New System.Drawing.Point(118, 41)
        Me.txtBankName.MaxLength = 200
        Me.txtBankName.MendatroryField = True
        Me.txtBankName.MyLinkLable1 = Me.lblBankName
        Me.txtBankName.MyLinkLable2 = Nothing
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReferenceFieldDesc = Nothing
        Me.txtBankName.ReferenceFieldName = Nothing
        Me.txtBankName.ReferenceTableName = Nothing
        Me.txtBankName.Size = New System.Drawing.Size(557, 20)
        Me.txtBankName.TabIndex = 2
        '
        'lblBankName
        '
        Me.lblBankName.FieldName = Nothing
        Me.lblBankName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(3, 42)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(65, 16)
        Me.lblBankName.TabIndex = 43
        Me.lblBankName.Text = "Bank Name"
        '
        'lblBankCode
        '
        Me.lblBankCode.FieldName = Nothing
        Me.lblBankCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankCode.Location = New System.Drawing.Point(3, 18)
        Me.lblBankCode.Name = "lblBankCode"
        Me.lblBankCode.Size = New System.Drawing.Size(62, 16)
        Me.lblBankCode.TabIndex = 41
        Me.lblBankCode.Text = "Bank Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(415, 17)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'fndBankCode
        '
        Me.fndBankCode.FieldName = Nothing
        Me.fndBankCode.Location = New System.Drawing.Point(118, 16)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndBankCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndBankCode.MyLinkLable1 = Me.lblBankCode
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyMaxLength = 30
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.Size = New System.Drawing.Size(292, 21)
        Me.fndBankCode.TabIndex = 40
        Me.fndBankCode.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(809, 356)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(809, 356)
        Me.UcAttachment1.TabIndex = 6
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(703, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImport, Me.rmExport})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "Import"
        Me.rmImport.AccessibleName = "Import"
        Me.rmImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDImportBankDetail, Me.RDImportBranchDetail})
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'RDImportBankDetail
        '
        Me.RDImportBankDetail.AccessibleDescription = "RadMenuItem1"
        Me.RDImportBankDetail.AccessibleName = "RadMenuItem1"
        Me.RDImportBankDetail.Name = "RDImportBankDetail"
        Me.RDImportBankDetail.Text = "Import Bank Details"
        '
        'RDImportBranchDetail
        '
        Me.RDImportBranchDetail.AccessibleDescription = "RadMenuItem2"
        Me.RDImportBranchDetail.AccessibleName = "RadMenuItem2"
        Me.RDImportBranchDetail.Name = "RDImportBranchDetail"
        Me.RDImportBranchDetail.Text = "Import Branch Details"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "Export"
        Me.rmExport.AccessibleName = "Export"
        Me.rmExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RDExportBankDetails, Me.RDExportBranchDetails})
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'RDExportBankDetails
        '
        Me.RDExportBankDetails.AccessibleDescription = "RadMenuItem3"
        Me.RDExportBankDetails.AccessibleName = "RadMenuItem3"
        Me.RDExportBankDetails.Name = "RDExportBankDetails"
        Me.RDExportBankDetails.Text = "Export Bank Details"
        '
        'RDExportBranchDetails
        '
        Me.RDExportBranchDetails.AccessibleDescription = "RadMenuItem4"
        Me.RDExportBranchDetails.AccessibleName = "RadMenuItem4"
        Me.RDExportBranchDetails.Name = "RDExportBranchDetails"
        Me.RDExportBranchDetails.Text = "Export Branch Details"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(618, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(85, 8)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(6, 8)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmVendorBankMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 491)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVendorBankMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmVendorBankMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIFSCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBranchCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtstateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcountryName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdd3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtcityName As common.Controls.MyLabel
    Friend WithEvents txtstateName As common.Controls.MyLabel
    Friend WithEvents txtcountryName As common.Controls.MyLabel
    Friend WithEvents fndCountry As common.UserControls.txtFinder
    Friend WithEvents lblCountry As common.Controls.MyLabel
    Friend WithEvents fndstate As common.UserControls.txtFinder
    Friend WithEvents lblState As common.Controls.MyLabel
    Friend WithEvents fndCity As common.UserControls.txtFinder
    Friend WithEvents lblCity As common.Controls.MyLabel
    Friend WithEvents txtAdd2 As common.Controls.MyTextBox
    Friend WithEvents lblAddress As common.Controls.MyLabel
    Friend WithEvents txtAdd1 As common.Controls.MyTextBox
    Friend WithEvents txtAdd3 As common.Controls.MyTextBox
    Friend WithEvents txtBankName As common.Controls.MyTextBox
    Friend WithEvents lblBankName As common.Controls.MyLabel
    Friend WithEvents lblBankCode As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndBankCode As common.UserControls.txtNavigator
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtBranchName As common.Controls.MyTextBox
    Friend WithEvents lblBranchName As common.Controls.MyLabel
    Friend WithEvents txtBranchCode As common.Controls.MyTextBox
    Friend WithEvents lblBranchCode As common.Controls.MyLabel
    Friend WithEvents txtIFSCCode As common.Controls.MyTextBox
    Friend WithEvents lblIFSCCode As common.Controls.MyLabel
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RDImportBankDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDImportBranchDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDExportBankDetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RDExportBranchDetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
End Class

