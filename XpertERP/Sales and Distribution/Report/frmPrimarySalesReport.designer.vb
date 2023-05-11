<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrimarySalesReport
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grpCompany = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rbtnSelectCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnAllCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkCustomerClass = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkClassSelect = New common.Controls.MyRadioButton
        Me.chkClassAll = New common.Controls.MyRadioButton
        Me.rdbPack = New Telerik.WinControls.UI.RadRadioButton
        Me.gbRoot = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgRoute = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkRouteSelect = New common.Controls.MyRadioButton
        Me.chkRouteAll = New common.Controls.MyRadioButton
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.rdbFlavour = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSku = New Telerik.WinControls.UI.RadRadioButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.dtpFdate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdbAccount = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbShip = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.GroupBox1.SuspendLayout()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCompany.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkClassSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkClassAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbRoot.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.rdbAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbShip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.grpCompany)
        Me.GroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.GroupBox1.Controls.Add(Me.rdbPack)
        Me.GroupBox1.Controls.Add(Me.gbRoot)
        Me.GroupBox1.Controls.Add(Me.Locationgb)
        Me.GroupBox1.Controls.Add(Me.rdbFlavour)
        Me.GroupBox1.Controls.Add(Me.rdbSku)
        Me.GroupBox1.Controls.Add(Me.btnReset)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 78)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(732, 400)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'grpCompany
        '
        Me.grpCompany.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCompany.Controls.Add(Me.gvDB)
        Me.grpCompany.Controls.Add(Me.Panel4)
        Me.grpCompany.HeaderText = "Company"
        Me.grpCompany.Location = New System.Drawing.Point(6, 46)
        Me.grpCompany.Name = "grpCompany"
        Me.grpCompany.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCompany.Size = New System.Drawing.Size(351, 172)
        Me.grpCompany.TabIndex = 105
        Me.grpCompany.Text = "Company"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 40)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.MasterTemplate.EnableFiltering = True
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(331, 122)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbtnSelectCompany)
        Me.Panel4.Controls.Add(Me.rbtnAllCompany)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(331, 20)
        Me.Panel4.TabIndex = 0
        '
        'rbtnSelectCompany
        '
        Me.rbtnSelectCompany.Location = New System.Drawing.Point(135, 1)
        Me.rbtnSelectCompany.Name = "rbtnSelectCompany"
        Me.rbtnSelectCompany.Size = New System.Drawing.Size(57, 16)
        Me.rbtnSelectCompany.TabIndex = 1
        Me.rbtnSelectCompany.Text = "Select"
        '
        'rbtnAllCompany
        '
        Me.rbtnAllCompany.Location = New System.Drawing.Point(68, 1)
        Me.rbtnAllCompany.Name = "rbtnAllCompany"
        Me.rbtnAllCompany.Size = New System.Drawing.Size(37, 16)
        Me.rbtnAllCompany.TabIndex = 0
        Me.rbtnAllCompany.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.chkCustomerClass)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "Customer Class"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 219)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(357, 167)
        Me.RadGroupBox2.TabIndex = 104
        Me.RadGroupBox2.Text = "Customer Class"
        '
        'chkCustomerClass
        '
        Me.chkCustomerClass.CheckedValue = Nothing
        Me.chkCustomerClass.DataSource = Nothing
        Me.chkCustomerClass.DisplayMember = "Name"
        Me.chkCustomerClass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkCustomerClass.Location = New System.Drawing.Point(10, 40)
        Me.chkCustomerClass.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.chkCustomerClass.MyShowHeadrText = False
        Me.chkCustomerClass.Name = "chkCustomerClass"
        Me.chkCustomerClass.Size = New System.Drawing.Size(337, 117)
        Me.chkCustomerClass.TabIndex = 1
        Me.chkCustomerClass.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkClassSelect)
        Me.Panel3.Controls.Add(Me.chkClassAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(337, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkClassSelect
        '
        Me.chkClassSelect.Location = New System.Drawing.Point(135, 1)
        Me.chkClassSelect.MyLinkLable1 = Nothing
        Me.chkClassSelect.MyLinkLable2 = Nothing
        Me.chkClassSelect.Name = "chkClassSelect"
        Me.chkClassSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkClassSelect.TabIndex = 1
        Me.chkClassSelect.Text = "Select"
        '
        'chkClassAll
        '
        Me.chkClassAll.Location = New System.Drawing.Point(66, 1)
        Me.chkClassAll.MyLinkLable1 = Nothing
        Me.chkClassAll.MyLinkLable2 = Nothing
        Me.chkClassAll.Name = "chkClassAll"
        Me.chkClassAll.Size = New System.Drawing.Size(45, 18)
        Me.chkClassAll.TabIndex = 0
        Me.chkClassAll.Text = "All"
        '
        'rdbPack
        '
        Me.rdbPack.Location = New System.Drawing.Point(104, 20)
        Me.rdbPack.Name = "rdbPack"
        Me.rdbPack.Size = New System.Drawing.Size(81, 18)
        Me.rdbPack.TabIndex = 103
        Me.rdbPack.Text = "Pack Wise"
        '
        'gbRoot
        '
        Me.gbRoot.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbRoot.Controls.Add(Me.cbgRoute)
        Me.gbRoot.Controls.Add(Me.Panel1)
        Me.gbRoot.HeaderText = "Route"
        Me.gbRoot.Location = New System.Drawing.Point(369, 219)
        Me.gbRoot.Name = "gbRoot"
        Me.gbRoot.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbRoot.Size = New System.Drawing.Size(357, 167)
        Me.gbRoot.TabIndex = 49
        Me.gbRoot.Text = "Route"
        '
        'cbgRoute
        '
        Me.cbgRoute.CheckedValue = Nothing
        Me.cbgRoute.DataSource = Nothing
        Me.cbgRoute.DisplayMember = "Name"
        Me.cbgRoute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRoute.Location = New System.Drawing.Point(10, 40)
        Me.cbgRoute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRoute.MyShowHeadrText = False
        Me.cbgRoute.Name = "cbgRoute"
        Me.cbgRoute.Size = New System.Drawing.Size(337, 117)
        Me.cbgRoute.TabIndex = 1
        Me.cbgRoute.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkRouteSelect)
        Me.Panel1.Controls.Add(Me.chkRouteAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(337, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkRouteSelect
        '
        Me.chkRouteSelect.Location = New System.Drawing.Point(135, 1)
        Me.chkRouteSelect.MyLinkLable1 = Nothing
        Me.chkRouteSelect.MyLinkLable2 = Nothing
        Me.chkRouteSelect.Name = "chkRouteSelect"
        Me.chkRouteSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkRouteSelect.TabIndex = 1
        Me.chkRouteSelect.Text = "Select"
        '
        'chkRouteAll
        '
        Me.chkRouteAll.Location = New System.Drawing.Point(68, 1)
        Me.chkRouteAll.MyLinkLable1 = Nothing
        Me.chkRouteAll.MyLinkLable2 = Nothing
        Me.chkRouteAll.Name = "chkRouteAll"
        Me.chkRouteAll.Size = New System.Drawing.Size(45, 18)
        Me.chkRouteAll.TabIndex = 0
        Me.chkRouteAll.Text = "All"
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLocation)
        Me.Locationgb.Controls.Add(Me.Panel2)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(369, 46)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(357, 172)
        Me.Locationgb.TabIndex = 48
        Me.Locationgb.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(337, 122)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(337, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(135, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(71, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(66, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(45, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'rdbFlavour
        '
        Me.rdbFlavour.Location = New System.Drawing.Point(207, 20)
        Me.rdbFlavour.Name = "rdbFlavour"
        Me.rdbFlavour.Size = New System.Drawing.Size(110, 18)
        Me.rdbFlavour.TabIndex = 103
        Me.rdbFlavour.Text = "Flavour Wise"
        '
        'rdbSku
        '
        Me.rdbSku.Location = New System.Drawing.Point(15, 20)
        Me.rdbSku.Name = "rdbSku"
        Me.rdbSku.Size = New System.Drawing.Size(110, 18)
        Me.rdbSku.TabIndex = 102
        Me.rdbSku.Text = "SKU Wise"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(365, 20)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 41
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(13, 4)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(78, 22)
        Me.btnprint.TabIndex = 100
        Me.btnprint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(704, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 101
        Me.btnClose.Text = "Close"
        '
        'dtpFdate
        '
        Me.dtpFdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpFdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFdate.Location = New System.Drawing.Point(254, 21)
        Me.dtpFdate.MendatroryField = False
        Me.dtpFdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.MyLinkLable1 = Nothing
        Me.dtpFdate.MyLinkLable2 = Nothing
        Me.dtpFdate.Name = "dtpFdate"
        Me.dtpFdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.Size = New System.Drawing.Size(90, 18)
        Me.dtpFdate.TabIndex = 98
        Me.dtpFdate.TabStop = False
        Me.dtpFdate.Text = "18/05/2011 02:11 PM"
        Me.dtpFdate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'RadLabel2
        '
        Me.RadLabel2.BackColor = System.Drawing.Color.Transparent
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(218, 21)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel2.TabIndex = 97
        Me.RadLabel2.Text = "Date"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadLabel2)
        Me.GroupBox2.Controls.Add(Me.rdbDetail)
        Me.GroupBox2.Controls.Add(Me.dtpFdate)
        Me.GroupBox2.Controls.Add(Me.rdbSummary)
        Me.GroupBox2.Location = New System.Drawing.Point(378, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(363, 45)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Select"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(101, 21)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(64, 18)
        Me.rdbDetail.TabIndex = 103
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(15, 21)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(80, 18)
        Me.rdbSummary.TabIndex = 103
        Me.rdbSummary.Text = "Summary"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdbAccount)
        Me.GroupBox3.Controls.Add(Me.rdbShip)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(185, 45)
        Me.GroupBox3.TabIndex = 104
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Primary Sale"
        '
        'rdbAccount
        '
        Me.rdbAccount.Location = New System.Drawing.Point(104, 21)
        Me.rdbAccount.Name = "rdbAccount"
        Me.rdbAccount.Size = New System.Drawing.Size(64, 18)
        Me.rdbAccount.TabIndex = 103
        Me.rdbAccount.Text = "Account"
        '
        'rdbShip
        '
        Me.rdbShip.Location = New System.Drawing.Point(15, 21)
        Me.rdbShip.Name = "rdbShip"
        Me.rdbShip.Size = New System.Drawing.Size(80, 18)
        Me.rdbShip.TabIndex = 103
        Me.rdbShip.Text = "Shipping"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.GroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(760, 494)
        Me.RadGroupBox1.TabIndex = 5
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(785, 546)
        Me.SplitContainer1.SplitterDistance = 515
        Me.SplitContainer1.TabIndex = 6
        '
        'FrmPrimarySalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 546)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPrimarySalesReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Primary Sales Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCompany.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkClassSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkClassAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbRoot.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.rdbAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbShip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbPack As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbRoot As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkRouteSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRouteAll As common.Controls.MyRadioButton
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents rdbFlavour As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSku As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpFdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkCustomerClass As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkClassSelect As common.Controls.MyRadioButton
    Friend WithEvents chkClassAll As common.Controls.MyRadioButton
    Friend WithEvents grpCompany As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbtnSelectCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnAllCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbAccount As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbShip As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

