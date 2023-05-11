<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFarmerServiceOrderWithRate
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFarmerServiceOrderWithRate))
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyNumBox1 = New common.MyNumBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyNumBox2 = New common.MyNumBox()
        Me.lblFarmerId = New common.Controls.MyLabel()
        Me.txtFarmerId = New common.UserControls.txtFinder()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblServiceProviderName = New common.Controls.MyLabel()
        Me.txtServiceProviderName = New common.UserControls.txtFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblServiceProviderType = New common.Controls.MyLabel()
        Me.txtServiceProviderType = New common.UserControls.txtFinder()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.dgvitem = New common.UserControls.MyRadGridView()
        Me.dtpServiceOrderDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtServiceOrder = New common.UserControls.txtNavigator()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtHeadOffice = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblStaff = New common.Controls.MyLabel()
        Me.txtStaff = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.lblPMC = New common.Controls.MyLabel()
        Me.txtPMC = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblBranch = New common.Controls.MyLabel()
        Me.txtBranch = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblArea = New common.Controls.MyLabel()
        Me.txtArea = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblRegion = New common.Controls.MyLabel()
        Me.txtRegion = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblZone = New common.Controls.MyLabel()
        Me.txtZone = New common.UserControls.txtFinder()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyNumBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyNumBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFarmerId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblServiceProviderName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblServiceProviderType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvitem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvitem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpServiceOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.txtHeadOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStaff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRegion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(1076, 20)
        Me.rdmenufile.TabIndex = 1
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1076, 517)
        Me.SplitContainer1.SplitterDistance = 488
        Me.SplitContainer1.TabIndex = 2
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(1076, 488)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyNumBox1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.MyNumBox2)
        Me.RadPageViewPage1.Controls.Add(Me.lblFarmerId)
        Me.RadPageViewPage1.Controls.Add(Me.txtFarmerId)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.lblServiceProviderName)
        Me.RadPageViewPage1.Controls.Add(Me.txtServiceProviderName)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.lblServiceProviderType)
        Me.RadPageViewPage1.Controls.Add(Me.txtServiceProviderType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.dgvitem)
        Me.RadPageViewPage1.Controls.Add(Me.dtpServiceOrderDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.btnNew)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtServiceOrder)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(88.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1055, 440)
        Me.RadPageViewPage1.Text = "Service Details"
        '
        'MyNumBox1
        '
        Me.MyNumBox1.BackColor = System.Drawing.Color.White
        Me.MyNumBox1.CalculationExpression = Nothing
        Me.MyNumBox1.DecimalPlaces = 2
        Me.MyNumBox1.FieldCode = Nothing
        Me.MyNumBox1.FieldDesc = Nothing
        Me.MyNumBox1.FieldMaxLength = 0
        Me.MyNumBox1.FieldName = Nothing
        Me.MyNumBox1.isCalculatedField = False
        Me.MyNumBox1.IsSourceFromTable = False
        Me.MyNumBox1.IsSourceFromValueList = False
        Me.MyNumBox1.IsUnique = False
        Me.MyNumBox1.Location = New System.Drawing.Point(856, 56)
        Me.MyNumBox1.MendatroryField = False
        Me.MyNumBox1.MyLinkLable1 = Nothing
        Me.MyNumBox1.MyLinkLable2 = Nothing
        Me.MyNumBox1.Name = "MyNumBox1"
        Me.MyNumBox1.ReferenceFieldDesc = Nothing
        Me.MyNumBox1.ReferenceFieldName = Nothing
        Me.MyNumBox1.ReferenceTableName = Nothing
        Me.MyNumBox1.Size = New System.Drawing.Size(141, 20)
        Me.MyNumBox1.TabIndex = 304
        Me.MyNumBox1.Text = "0"
        Me.MyNumBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MyNumBox1.Value = 0.0R
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(776, 58)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel13.TabIndex = 303
        Me.MyLabel13.Text = "Paid Amount"
        '
        'MyNumBox2
        '
        Me.MyNumBox2.BackColor = System.Drawing.Color.White
        Me.MyNumBox2.CalculationExpression = Nothing
        Me.MyNumBox2.DecimalPlaces = 2
        Me.MyNumBox2.FieldCode = Nothing
        Me.MyNumBox2.FieldDesc = Nothing
        Me.MyNumBox2.FieldMaxLength = 0
        Me.MyNumBox2.FieldName = Nothing
        Me.MyNumBox2.isCalculatedField = False
        Me.MyNumBox2.IsSourceFromTable = False
        Me.MyNumBox2.IsSourceFromValueList = False
        Me.MyNumBox2.IsUnique = False
        Me.MyNumBox2.Location = New System.Drawing.Point(856, 34)
        Me.MyNumBox2.MendatroryField = False
        Me.MyNumBox2.MyLinkLable1 = Nothing
        Me.MyNumBox2.MyLinkLable2 = Nothing
        Me.MyNumBox2.Name = "MyNumBox2"
        Me.MyNumBox2.ReadOnly = True
        Me.MyNumBox2.ReferenceFieldDesc = Nothing
        Me.MyNumBox2.ReferenceFieldName = Nothing
        Me.MyNumBox2.ReferenceTableName = Nothing
        Me.MyNumBox2.Size = New System.Drawing.Size(140, 20)
        Me.MyNumBox2.TabIndex = 302
        Me.MyNumBox2.Text = "0"
        Me.MyNumBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.MyNumBox2.Value = 0.0R
        '
        'lblFarmerId
        '
        Me.lblFarmerId.AutoSize = False
        Me.lblFarmerId.BorderVisible = True
        Me.lblFarmerId.FieldName = Nothing
        Me.lblFarmerId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFarmerId.Location = New System.Drawing.Point(453, 74)
        Me.lblFarmerId.Name = "lblFarmerId"
        Me.lblFarmerId.Size = New System.Drawing.Size(300, 18)
        Me.lblFarmerId.TabIndex = 300
        Me.lblFarmerId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFarmerId.TextWrap = False
        '
        'txtFarmerId
        '
        Me.txtFarmerId.CalculationExpression = Nothing
        Me.txtFarmerId.FieldCode = Nothing
        Me.txtFarmerId.FieldDesc = Nothing
        Me.txtFarmerId.FieldMaxLength = 0
        Me.txtFarmerId.FieldName = Nothing
        Me.txtFarmerId.isCalculatedField = False
        Me.txtFarmerId.IsSourceFromTable = False
        Me.txtFarmerId.IsSourceFromValueList = False
        Me.txtFarmerId.IsUnique = False
        Me.txtFarmerId.Location = New System.Drawing.Point(127, 74)
        Me.txtFarmerId.MendatroryField = True
        Me.txtFarmerId.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFarmerId.MyLinkLable1 = Nothing
        Me.txtFarmerId.MyLinkLable2 = Nothing
        Me.txtFarmerId.MyReadOnly = False
        Me.txtFarmerId.MyShowMasterFormButton = False
        Me.txtFarmerId.Name = "txtFarmerId"
        Me.txtFarmerId.ReferenceFieldDesc = Nothing
        Me.txtFarmerId.ReferenceFieldName = Nothing
        Me.txtFarmerId.ReferenceTableName = Nothing
        Me.txtFarmerId.Size = New System.Drawing.Size(323, 18)
        Me.txtFarmerId.TabIndex = 299
        Me.txtFarmerId.Value = ""
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(776, 36)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel15.TabIndex = 301
        Me.MyLabel15.Text = "Bill Amount"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 75)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel3.TabIndex = 298
        Me.MyLabel3.Text = "Farmer Id"
        '
        'lblServiceProviderName
        '
        Me.lblServiceProviderName.AutoSize = False
        Me.lblServiceProviderName.BorderVisible = True
        Me.lblServiceProviderName.FieldName = Nothing
        Me.lblServiceProviderName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceProviderName.Location = New System.Drawing.Point(453, 53)
        Me.lblServiceProviderName.Name = "lblServiceProviderName"
        Me.lblServiceProviderName.Size = New System.Drawing.Size(299, 18)
        Me.lblServiceProviderName.TabIndex = 297
        Me.lblServiceProviderName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblServiceProviderName.TextWrap = False
        '
        'txtServiceProviderName
        '
        Me.txtServiceProviderName.CalculationExpression = Nothing
        Me.txtServiceProviderName.FieldCode = Nothing
        Me.txtServiceProviderName.FieldDesc = Nothing
        Me.txtServiceProviderName.FieldMaxLength = 0
        Me.txtServiceProviderName.FieldName = Nothing
        Me.txtServiceProviderName.isCalculatedField = False
        Me.txtServiceProviderName.IsSourceFromTable = False
        Me.txtServiceProviderName.IsSourceFromValueList = False
        Me.txtServiceProviderName.IsUnique = False
        Me.txtServiceProviderName.Location = New System.Drawing.Point(127, 53)
        Me.txtServiceProviderName.MendatroryField = True
        Me.txtServiceProviderName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceProviderName.MyLinkLable1 = Nothing
        Me.txtServiceProviderName.MyLinkLable2 = Nothing
        Me.txtServiceProviderName.MyReadOnly = False
        Me.txtServiceProviderName.MyShowMasterFormButton = False
        Me.txtServiceProviderName.Name = "txtServiceProviderName"
        Me.txtServiceProviderName.ReferenceFieldDesc = Nothing
        Me.txtServiceProviderName.ReferenceFieldName = Nothing
        Me.txtServiceProviderName.ReferenceTableName = Nothing
        Me.txtServiceProviderName.Size = New System.Drawing.Size(323, 18)
        Me.txtServiceProviderName.TabIndex = 296
        Me.txtServiceProviderName.Value = ""
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(3, 55)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(123, 16)
        Me.MyLabel14.TabIndex = 295
        Me.MyLabel14.Text = "Service Provider Name"
        '
        'lblServiceProviderType
        '
        Me.lblServiceProviderType.AutoSize = False
        Me.lblServiceProviderType.BorderVisible = True
        Me.lblServiceProviderType.FieldName = Nothing
        Me.lblServiceProviderType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceProviderType.Location = New System.Drawing.Point(453, 33)
        Me.lblServiceProviderType.Name = "lblServiceProviderType"
        Me.lblServiceProviderType.Size = New System.Drawing.Size(299, 18)
        Me.lblServiceProviderType.TabIndex = 294
        Me.lblServiceProviderType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblServiceProviderType.TextWrap = False
        '
        'txtServiceProviderType
        '
        Me.txtServiceProviderType.CalculationExpression = Nothing
        Me.txtServiceProviderType.FieldCode = Nothing
        Me.txtServiceProviderType.FieldDesc = Nothing
        Me.txtServiceProviderType.FieldMaxLength = 0
        Me.txtServiceProviderType.FieldName = Nothing
        Me.txtServiceProviderType.isCalculatedField = False
        Me.txtServiceProviderType.IsSourceFromTable = False
        Me.txtServiceProviderType.IsSourceFromValueList = False
        Me.txtServiceProviderType.IsUnique = False
        Me.txtServiceProviderType.Location = New System.Drawing.Point(127, 33)
        Me.txtServiceProviderType.MendatroryField = True
        Me.txtServiceProviderType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceProviderType.MyLinkLable1 = Nothing
        Me.txtServiceProviderType.MyLinkLable2 = Nothing
        Me.txtServiceProviderType.MyReadOnly = False
        Me.txtServiceProviderType.MyShowMasterFormButton = False
        Me.txtServiceProviderType.Name = "txtServiceProviderType"
        Me.txtServiceProviderType.ReferenceFieldDesc = Nothing
        Me.txtServiceProviderType.ReferenceFieldName = Nothing
        Me.txtServiceProviderType.ReferenceTableName = Nothing
        Me.txtServiceProviderType.Size = New System.Drawing.Size(323, 18)
        Me.txtServiceProviderType.TabIndex = 293
        Me.txtServiceProviderType.Value = ""
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(3, 35)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel12.TabIndex = 292
        Me.MyLabel12.Text = "Service Provider Type"
        '
        'dgvitem
        '
        Me.dgvitem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvitem.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvitem.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvitem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvitem.ForeColor = System.Drawing.Color.Black
        Me.dgvitem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvitem.Location = New System.Drawing.Point(0, 108)
        '
        'dgvitem
        '
        Me.dgvitem.MasterTemplate.EnableFiltering = True
        Me.dgvitem.MasterTemplate.EnableGrouping = False
        Me.dgvitem.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvitem.Name = "dgvitem"
        Me.dgvitem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvitem.ShowHeaderCellButtons = True
        Me.dgvitem.Size = New System.Drawing.Size(1052, 332)
        Me.dgvitem.TabIndex = 291
        Me.dgvitem.TabStop = False
        Me.dgvitem.Text = "RadGridView1"
        '
        'dtpServiceOrderDate
        '
        Me.dtpServiceOrderDate.CalculationExpression = Nothing
        Me.dtpServiceOrderDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpServiceOrderDate.FieldCode = Nothing
        Me.dtpServiceOrderDate.FieldDesc = Nothing
        Me.dtpServiceOrderDate.FieldMaxLength = 0
        Me.dtpServiceOrderDate.FieldName = Nothing
        Me.dtpServiceOrderDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpServiceOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpServiceOrderDate.isCalculatedField = False
        Me.dtpServiceOrderDate.IsSourceFromTable = False
        Me.dtpServiceOrderDate.IsSourceFromValueList = False
        Me.dtpServiceOrderDate.IsUnique = False
        Me.dtpServiceOrderDate.Location = New System.Drawing.Point(513, 9)
        Me.dtpServiceOrderDate.MendatroryField = False
        Me.dtpServiceOrderDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpServiceOrderDate.MyLinkLable1 = Me.RadLabel4
        Me.dtpServiceOrderDate.MyLinkLable2 = Nothing
        Me.dtpServiceOrderDate.Name = "dtpServiceOrderDate"
        Me.dtpServiceOrderDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpServiceOrderDate.ReferenceFieldDesc = Nothing
        Me.dtpServiceOrderDate.ReferenceFieldName = Nothing
        Me.dtpServiceOrderDate.ReferenceTableName = Nothing
        Me.dtpServiceOrderDate.Size = New System.Drawing.Size(81, 18)
        Me.dtpServiceOrderDate.TabIndex = 287
        Me.dtpServiceOrderDate.TabStop = False
        Me.dtpServiceOrderDate.Text = "13/06/2011"
        Me.dtpServiceOrderDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(479, 12)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 284
        Me.RadLabel4.Text = "Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 12)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel2.TabIndex = 286
        Me.MyLabel2.Text = "Service Order No"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(452, 9)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 283
        Me.btnNew.Text = " "
        '
        'txtServiceOrder
        '
        Me.txtServiceOrder.FieldName = Nothing
        Me.txtServiceOrder.Location = New System.Drawing.Point(127, 8)
        Me.txtServiceOrder.MendatroryField = True
        Me.txtServiceOrder.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtServiceOrder.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtServiceOrder.MyLinkLable1 = Nothing
        Me.txtServiceOrder.MyLinkLable2 = Nothing
        Me.txtServiceOrder.MyMaxLength = 16
        Me.txtServiceOrder.MyReadOnly = False
        Me.txtServiceOrder.Name = "txtServiceOrder"
        Me.txtServiceOrder.Size = New System.Drawing.Size(323, 21)
        Me.txtServiceOrder.TabIndex = 285
        Me.txtServiceOrder.Value = ""
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtHeadOffice)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage2.Controls.Add(Me.lblStaff)
        Me.RadPageViewPage2.Controls.Add(Me.txtStaff)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage2.Controls.Add(Me.lblPMC)
        Me.RadPageViewPage2.Controls.Add(Me.txtPMC)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage2.Controls.Add(Me.lblMCC)
        Me.RadPageViewPage2.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage2.Controls.Add(Me.lblBranch)
        Me.RadPageViewPage2.Controls.Add(Me.txtBranch)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage2.Controls.Add(Me.lblArea)
        Me.RadPageViewPage2.Controls.Add(Me.txtArea)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage2.Controls.Add(Me.lblRegion)
        Me.RadPageViewPage2.Controls.Add(Me.txtRegion)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage2.Controls.Add(Me.lblZone)
        Me.RadPageViewPage2.Controls.Add(Me.txtZone)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(88.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1055, 440)
        Me.RadPageViewPage2.Text = "Farmer Details"
        '
        'txtHeadOffice
        '
        Me.txtHeadOffice.AutoSize = False
        Me.txtHeadOffice.CalculationExpression = Nothing
        Me.txtHeadOffice.FieldCode = Nothing
        Me.txtHeadOffice.FieldDesc = Nothing
        Me.txtHeadOffice.FieldMaxLength = 0
        Me.txtHeadOffice.FieldName = Nothing
        Me.txtHeadOffice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadOffice.isCalculatedField = False
        Me.txtHeadOffice.IsSourceFromTable = False
        Me.txtHeadOffice.IsSourceFromValueList = False
        Me.txtHeadOffice.IsUnique = False
        Me.txtHeadOffice.Location = New System.Drawing.Point(95, 10)
        Me.txtHeadOffice.MaxLength = 50
        Me.txtHeadOffice.MendatroryField = False
        Me.txtHeadOffice.Multiline = True
        Me.txtHeadOffice.MyLinkLable1 = Nothing
        Me.txtHeadOffice.MyLinkLable2 = Nothing
        Me.txtHeadOffice.Name = "txtHeadOffice"
        Me.txtHeadOffice.ReferenceFieldDesc = Nothing
        Me.txtHeadOffice.ReferenceFieldName = Nothing
        Me.txtHeadOffice.ReferenceTableName = Nothing
        Me.txtHeadOffice.Size = New System.Drawing.Size(597, 21)
        Me.txtHeadOffice.TabIndex = 316
        Me.txtHeadOffice.Text = " "
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(14, 13)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel11.TabIndex = 315
        Me.MyLabel11.Text = "Head Office"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(14, 166)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel9.TabIndex = 314
        Me.MyLabel9.Text = "Staff"
        '
        'lblStaff
        '
        Me.lblStaff.AutoSize = False
        Me.lblStaff.BorderVisible = True
        Me.lblStaff.FieldName = Nothing
        Me.lblStaff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStaff.Location = New System.Drawing.Point(322, 166)
        Me.lblStaff.Name = "lblStaff"
        Me.lblStaff.Size = New System.Drawing.Size(368, 18)
        Me.lblStaff.TabIndex = 313
        Me.lblStaff.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblStaff.TextWrap = False
        '
        'txtStaff
        '
        Me.txtStaff.CalculationExpression = Nothing
        Me.txtStaff.FieldCode = Nothing
        Me.txtStaff.FieldDesc = Nothing
        Me.txtStaff.FieldMaxLength = 0
        Me.txtStaff.FieldName = Nothing
        Me.txtStaff.isCalculatedField = False
        Me.txtStaff.IsSourceFromTable = False
        Me.txtStaff.IsSourceFromValueList = False
        Me.txtStaff.IsUnique = False
        Me.txtStaff.Location = New System.Drawing.Point(93, 166)
        Me.txtStaff.MendatroryField = False
        Me.txtStaff.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStaff.MyLinkLable1 = Nothing
        Me.txtStaff.MyLinkLable2 = Me.lblStaff
        Me.txtStaff.MyReadOnly = False
        Me.txtStaff.MyShowMasterFormButton = False
        Me.txtStaff.Name = "txtStaff"
        Me.txtStaff.ReferenceFieldDesc = Nothing
        Me.txtStaff.ReferenceFieldName = Nothing
        Me.txtStaff.ReferenceTableName = Nothing
        Me.txtStaff.Size = New System.Drawing.Size(223, 18)
        Me.txtStaff.TabIndex = 312
        Me.txtStaff.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(14, 122)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel8.TabIndex = 311
        Me.MyLabel8.Text = "PMC"
        '
        'lblPMC
        '
        Me.lblPMC.AutoSize = False
        Me.lblPMC.BorderVisible = True
        Me.lblPMC.FieldName = Nothing
        Me.lblPMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPMC.Location = New System.Drawing.Point(322, 122)
        Me.lblPMC.Name = "lblPMC"
        Me.lblPMC.Size = New System.Drawing.Size(368, 18)
        Me.lblPMC.TabIndex = 310
        Me.lblPMC.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPMC.TextWrap = False
        '
        'txtPMC
        '
        Me.txtPMC.CalculationExpression = Nothing
        Me.txtPMC.FieldCode = Nothing
        Me.txtPMC.FieldDesc = Nothing
        Me.txtPMC.FieldMaxLength = 0
        Me.txtPMC.FieldName = Nothing
        Me.txtPMC.isCalculatedField = False
        Me.txtPMC.IsSourceFromTable = False
        Me.txtPMC.IsSourceFromValueList = False
        Me.txtPMC.IsUnique = False
        Me.txtPMC.Location = New System.Drawing.Point(93, 122)
        Me.txtPMC.MendatroryField = False
        Me.txtPMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMC.MyLinkLable1 = Nothing
        Me.txtPMC.MyLinkLable2 = Me.lblPMC
        Me.txtPMC.MyReadOnly = False
        Me.txtPMC.MyShowMasterFormButton = False
        Me.txtPMC.Name = "txtPMC"
        Me.txtPMC.ReferenceFieldDesc = Nothing
        Me.txtPMC.ReferenceFieldName = Nothing
        Me.txtPMC.ReferenceTableName = Nothing
        Me.txtPMC.Size = New System.Drawing.Size(223, 18)
        Me.txtPMC.TabIndex = 309
        Me.txtPMC.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(14, 144)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel7.TabIndex = 308
        Me.MyLabel7.Text = "MCC"
        '
        'lblMCC
        '
        Me.lblMCC.AutoSize = False
        Me.lblMCC.BorderVisible = True
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC.Location = New System.Drawing.Point(322, 144)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(368, 18)
        Me.lblMCC.TabIndex = 307
        Me.lblMCC.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMCC.TextWrap = False
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(93, 144)
        Me.txtMCC.MendatroryField = False
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Me.lblMCC
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(223, 18)
        Me.txtMCC.TabIndex = 306
        Me.txtMCC.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(14, 101)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel6.TabIndex = 305
        Me.MyLabel6.Text = "Branch"
        '
        'lblBranch
        '
        Me.lblBranch.AutoSize = False
        Me.lblBranch.BorderVisible = True
        Me.lblBranch.FieldName = Nothing
        Me.lblBranch.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranch.Location = New System.Drawing.Point(322, 101)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(368, 18)
        Me.lblBranch.TabIndex = 304
        Me.lblBranch.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBranch.TextWrap = False
        '
        'txtBranch
        '
        Me.txtBranch.CalculationExpression = Nothing
        Me.txtBranch.FieldCode = Nothing
        Me.txtBranch.FieldDesc = Nothing
        Me.txtBranch.FieldMaxLength = 0
        Me.txtBranch.FieldName = Nothing
        Me.txtBranch.isCalculatedField = False
        Me.txtBranch.IsSourceFromTable = False
        Me.txtBranch.IsSourceFromValueList = False
        Me.txtBranch.IsUnique = False
        Me.txtBranch.Location = New System.Drawing.Point(93, 101)
        Me.txtBranch.MendatroryField = False
        Me.txtBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.MyLinkLable1 = Nothing
        Me.txtBranch.MyLinkLable2 = Me.lblBranch
        Me.txtBranch.MyReadOnly = False
        Me.txtBranch.MyShowMasterFormButton = False
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.ReferenceFieldDesc = Nothing
        Me.txtBranch.ReferenceFieldName = Nothing
        Me.txtBranch.ReferenceTableName = Nothing
        Me.txtBranch.Size = New System.Drawing.Size(223, 18)
        Me.txtBranch.TabIndex = 303
        Me.txtBranch.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(14, 79)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel5.TabIndex = 302
        Me.MyLabel5.Text = "Area"
        '
        'lblArea
        '
        Me.lblArea.AutoSize = False
        Me.lblArea.BorderVisible = True
        Me.lblArea.FieldName = Nothing
        Me.lblArea.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArea.Location = New System.Drawing.Point(322, 79)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(368, 18)
        Me.lblArea.TabIndex = 301
        Me.lblArea.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblArea.TextWrap = False
        '
        'txtArea
        '
        Me.txtArea.CalculationExpression = Nothing
        Me.txtArea.FieldCode = Nothing
        Me.txtArea.FieldDesc = Nothing
        Me.txtArea.FieldMaxLength = 0
        Me.txtArea.FieldName = Nothing
        Me.txtArea.isCalculatedField = False
        Me.txtArea.IsSourceFromTable = False
        Me.txtArea.IsSourceFromValueList = False
        Me.txtArea.IsUnique = False
        Me.txtArea.Location = New System.Drawing.Point(93, 79)
        Me.txtArea.MendatroryField = False
        Me.txtArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArea.MyLinkLable1 = Nothing
        Me.txtArea.MyLinkLable2 = Me.lblArea
        Me.txtArea.MyReadOnly = False
        Me.txtArea.MyShowMasterFormButton = False
        Me.txtArea.Name = "txtArea"
        Me.txtArea.ReferenceFieldDesc = Nothing
        Me.txtArea.ReferenceFieldName = Nothing
        Me.txtArea.ReferenceTableName = Nothing
        Me.txtArea.Size = New System.Drawing.Size(223, 18)
        Me.txtArea.TabIndex = 300
        Me.txtArea.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(14, 57)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel1.TabIndex = 299
        Me.MyLabel1.Text = "Region"
        '
        'lblRegion
        '
        Me.lblRegion.AutoSize = False
        Me.lblRegion.BorderVisible = True
        Me.lblRegion.FieldName = Nothing
        Me.lblRegion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegion.Location = New System.Drawing.Point(322, 57)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(368, 18)
        Me.lblRegion.TabIndex = 298
        Me.lblRegion.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRegion.TextWrap = False
        '
        'txtRegion
        '
        Me.txtRegion.CalculationExpression = Nothing
        Me.txtRegion.FieldCode = Nothing
        Me.txtRegion.FieldDesc = Nothing
        Me.txtRegion.FieldMaxLength = 0
        Me.txtRegion.FieldName = Nothing
        Me.txtRegion.isCalculatedField = False
        Me.txtRegion.IsSourceFromTable = False
        Me.txtRegion.IsSourceFromValueList = False
        Me.txtRegion.IsUnique = False
        Me.txtRegion.Location = New System.Drawing.Point(93, 57)
        Me.txtRegion.MendatroryField = False
        Me.txtRegion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegion.MyLinkLable1 = Nothing
        Me.txtRegion.MyLinkLable2 = Me.lblRegion
        Me.txtRegion.MyReadOnly = False
        Me.txtRegion.MyShowMasterFormButton = False
        Me.txtRegion.Name = "txtRegion"
        Me.txtRegion.ReferenceFieldDesc = Nothing
        Me.txtRegion.ReferenceFieldName = Nothing
        Me.txtRegion.ReferenceTableName = Nothing
        Me.txtRegion.Size = New System.Drawing.Size(223, 18)
        Me.txtRegion.TabIndex = 297
        Me.txtRegion.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(14, 35)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel4.TabIndex = 296
        Me.MyLabel4.Text = "Zone"
        '
        'lblZone
        '
        Me.lblZone.AutoSize = False
        Me.lblZone.BorderVisible = True
        Me.lblZone.FieldName = Nothing
        Me.lblZone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZone.Location = New System.Drawing.Point(322, 35)
        Me.lblZone.Name = "lblZone"
        Me.lblZone.Size = New System.Drawing.Size(368, 18)
        Me.lblZone.TabIndex = 295
        Me.lblZone.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblZone.TextWrap = False
        '
        'txtZone
        '
        Me.txtZone.CalculationExpression = Nothing
        Me.txtZone.FieldCode = Nothing
        Me.txtZone.FieldDesc = Nothing
        Me.txtZone.FieldMaxLength = 0
        Me.txtZone.FieldName = Nothing
        Me.txtZone.isCalculatedField = False
        Me.txtZone.IsSourceFromTable = False
        Me.txtZone.IsSourceFromValueList = False
        Me.txtZone.IsUnique = False
        Me.txtZone.Location = New System.Drawing.Point(93, 35)
        Me.txtZone.MendatroryField = False
        Me.txtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZone.MyLinkLable1 = Nothing
        Me.txtZone.MyLinkLable2 = Me.lblZone
        Me.txtZone.MyReadOnly = False
        Me.txtZone.MyShowMasterFormButton = False
        Me.txtZone.Name = "txtZone"
        Me.txtZone.ReferenceFieldDesc = Nothing
        Me.txtZone.ReferenceFieldName = Nothing
        Me.txtZone.ReferenceTableName = Nothing
        Me.txtZone.Size = New System.Drawing.Size(223, 18)
        Me.txtZone.TabIndex = 294
        Me.txtZone.Value = ""
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(218, 2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(66, 21)
        Me.RadButton1.TabIndex = 9
        Me.RadButton1.Text = "Post"
        Me.RadButton1.Visible = False
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(4, 1)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1008, 1)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 8
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(76, 2)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 7
        Me.btndelete.Text = "Delete"
        '
        'FrmFarmerServiceOrderWithRate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1076, 537)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "FrmFarmerServiceOrderWithRate"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Farmer Service Order With Rate"
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyNumBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyNumBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFarmerId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblServiceProviderName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblServiceProviderType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvitem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvitem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpServiceOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.txtHeadOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStaff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRegion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblZone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dtpServiceOrderDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtServiceOrder As common.UserControls.txtNavigator
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblZone As common.Controls.MyLabel
    Friend WithEvents txtZone As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblPMC As common.Controls.MyLabel
    Friend WithEvents txtPMC As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents lblBranch As common.Controls.MyLabel
    Friend WithEvents txtBranch As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblArea As common.Controls.MyLabel
    Friend WithEvents txtArea As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblRegion As common.Controls.MyLabel
    Friend WithEvents txtRegion As common.UserControls.txtFinder
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblStaff As common.Controls.MyLabel
    Friend WithEvents txtStaff As common.UserControls.txtFinder
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents dgvitem As common.UserControls.MyRadGridView
    Friend WithEvents lblServiceProviderName As common.Controls.MyLabel
    Friend WithEvents txtServiceProviderName As common.UserControls.txtFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents lblServiceProviderType As common.Controls.MyLabel
    Friend WithEvents txtServiceProviderType As common.UserControls.txtFinder
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblFarmerId As common.Controls.MyLabel
    Friend WithEvents txtFarmerId As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtHeadOffice As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyNumBox1 As common.MyNumBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyNumBox2 As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
End Class

