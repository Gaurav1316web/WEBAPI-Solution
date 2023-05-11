<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPJCEmployeeMaster
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.lblCustBillingRate = New common.Controls.MyLabel
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.chkApplyToAllCust = New Telerik.WinControls.UI.RadCheckBox
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.RadTextBox1 = New Telerik.WinControls.UI.RadTextBox
        Me.lblEmailId = New common.Controls.MyLabel
        Me.txtEmailId = New common.Controls.MyTextBox
        Me.lblEarningCode = New common.Controls.MyLabel
        Me.txtEarningCode = New common.Controls.MyTextBox
        Me.lblUserName = New common.Controls.MyTextBox
        Me.txtUnitCost = New common.MyNumBox
        Me.lblUnitCost = New common.Controls.MyLabel
        Me.txtBillingRate = New common.MyNumBox
        Me.lblBillingRate = New common.Controls.MyLabel
        Me.fndUser = New common.UserControls.txtFinder
        Me.lblUser = New common.Controls.MyLabel
        Me.lblTaskCode = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.lblEmployeeName = New common.Controls.MyLabel
        Me.txtEmpName = New common.Controls.MyTextBox
        Me.txtCode = New common.UserControls.txtNavigator
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ucCustomFields
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblCustBillingRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyToAllCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmailId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmailId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEarningCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEarningCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUnitCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnitCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBillingRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillingRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaskCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(798, 484)
        Me.SplitContainer1.SplitterDistance = 448
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(798, 448)
        Me.RadPageView1.TabIndex = 216
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblCustBillingRate)
        Me.RadPageViewPage1.Controls.Add(Me.gv1)
        Me.RadPageViewPage1.Controls.Add(Me.chkApplyToAllCust)
        Me.RadPageViewPage1.Controls.Add(Me.chkInactive)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadTextBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblEmailId)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmailId)
        Me.RadPageViewPage1.Controls.Add(Me.lblEarningCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtEarningCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblUserName)
        Me.RadPageViewPage1.Controls.Add(Me.txtUnitCost)
        Me.RadPageViewPage1.Controls.Add(Me.lblUnitCost)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillingRate)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillingRate)
        Me.RadPageViewPage1.Controls.Add(Me.fndUser)
        Me.RadPageViewPage1.Controls.Add(Me.lblUser)
        Me.RadPageViewPage1.Controls.Add(Me.lblTaskCode)
        Me.RadPageViewPage1.Controls.Add(Me.btnnew)
        Me.RadPageViewPage1.Controls.Add(Me.lblEmployeeName)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmpName)
        Me.RadPageViewPage1.Controls.Add(Me.txtCode)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(777, 400)
        Me.RadPageViewPage1.Text = "Employee"
        '
        'lblCustBillingRate
        '
        Me.lblCustBillingRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustBillingRate.Location = New System.Drawing.Point(5, 127)
        Me.lblCustBillingRate.Name = "lblCustBillingRate"
        Me.lblCustBillingRate.Size = New System.Drawing.Size(105, 16)
        Me.lblCustBillingRate.TabIndex = 241
        Me.lblCustBillingRate.Text = "Customerwise Rate"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(114, 127)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AutoGenerateColumns = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.Size = New System.Drawing.Size(630, 123)
        Me.gv1.TabIndex = 7
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'chkApplyToAllCust
        '
        Me.chkApplyToAllCust.Location = New System.Drawing.Point(285, 103)
        Me.chkApplyToAllCust.Name = "chkApplyToAllCust"
        Me.chkApplyToAllCust.Size = New System.Drawing.Size(137, 18)
        Me.chkApplyToAllCust.TabIndex = 6
        Me.chkApplyToAllCust.Text = "Apply To All Customers"
        Me.chkApplyToAllCust.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkInactive
        '
        Me.chkInactive.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkInactive.Location = New System.Drawing.Point(114, 369)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 18)
        Me.chkInactive.TabIndex = 12
        Me.chkInactive.Text = "Inactive"
        Me.chkInactive.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 303)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel1.TabIndex = 237
        Me.MyLabel1.Text = "Comments"
        '
        'RadTextBox1
        '
        Me.RadTextBox1.AutoSize = False
        Me.RadTextBox1.Location = New System.Drawing.Point(114, 302)
        Me.RadTextBox1.Multiline = True
        Me.RadTextBox1.Name = "RadTextBox1"
        Me.RadTextBox1.Size = New System.Drawing.Size(387, 61)
        Me.RadTextBox1.TabIndex = 11
        '
        'lblEmailId
        '
        Me.lblEmailId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmailId.Location = New System.Drawing.Point(6, 281)
        Me.lblEmailId.Name = "lblEmailId"
        Me.lblEmailId.Size = New System.Drawing.Size(47, 16)
        Me.lblEmailId.TabIndex = 235
        Me.lblEmailId.Text = "Email Id"
        '
        'txtEmailId
        '
        Me.txtEmailId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailId.Location = New System.Drawing.Point(114, 279)
        Me.txtEmailId.MaxLength = 50
        Me.txtEmailId.MendatroryField = False
        Me.txtEmailId.MyLinkLable1 = Nothing
        Me.txtEmailId.MyLinkLable2 = Nothing
        Me.txtEmailId.Name = "txtEmailId"
        '
        '
        '
        Me.txtEmailId.RootElement.StretchVertically = True
        Me.txtEmailId.Size = New System.Drawing.Size(219, 20)
        Me.txtEmailId.TabIndex = 10
        '
        'lblEarningCode
        '
        Me.lblEarningCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEarningCode.Location = New System.Drawing.Point(4, 59)
        Me.lblEarningCode.Name = "lblEarningCode"
        Me.lblEarningCode.Size = New System.Drawing.Size(75, 16)
        Me.lblEarningCode.TabIndex = 233
        Me.lblEarningCode.Text = "Earning Code"
        '
        'txtEarningCode
        '
        Me.txtEarningCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEarningCode.Location = New System.Drawing.Point(114, 57)
        Me.txtEarningCode.MaxLength = 50
        Me.txtEarningCode.MendatroryField = False
        Me.txtEarningCode.MyLinkLable1 = Nothing
        Me.txtEarningCode.MyLinkLable2 = Nothing
        Me.txtEarningCode.Name = "txtEarningCode"
        '
        '
        '
        Me.txtEarningCode.RootElement.StretchVertically = True
        Me.txtEarningCode.Size = New System.Drawing.Size(219, 20)
        Me.txtEarningCode.TabIndex = 3
        '
        'lblUserName
        '
        Me.lblUserName.Location = New System.Drawing.Point(285, 256)
        Me.lblUserName.MendatroryField = False
        Me.lblUserName.MyLinkLable1 = Nothing
        Me.lblUserName.MyLinkLable2 = Nothing
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.ReadOnly = True
        Me.lblUserName.Size = New System.Drawing.Size(216, 20)
        Me.lblUserName.TabIndex = 9
        Me.lblUserName.TabStop = False
        '
        'txtUnitCost
        '
        Me.txtUnitCost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtUnitCost.DecimalPlaces = 6
        Me.txtUnitCost.Location = New System.Drawing.Point(114, 79)
        Me.txtUnitCost.MaxLength = 18
        Me.txtUnitCost.MendatroryField = True
        Me.txtUnitCost.MyLinkLable1 = Me.lblUnitCost
        Me.txtUnitCost.MyLinkLable2 = Nothing
        Me.txtUnitCost.Name = "txtUnitCost"
        Me.txtUnitCost.Size = New System.Drawing.Size(165, 20)
        Me.txtUnitCost.TabIndex = 4
        Me.txtUnitCost.Text = "0"
        Me.txtUnitCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtUnitCost.Value = 0
        '
        'lblUnitCost
        '
        Me.lblUnitCost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitCost.Location = New System.Drawing.Point(4, 79)
        Me.lblUnitCost.Name = "lblUnitCost"
        Me.lblUnitCost.Size = New System.Drawing.Size(53, 16)
        Me.lblUnitCost.TabIndex = 226
        Me.lblUnitCost.Text = "Unit Cost"
        '
        'txtBillingRate
        '
        Me.txtBillingRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBillingRate.DecimalPlaces = 6
        Me.txtBillingRate.Location = New System.Drawing.Point(114, 102)
        Me.txtBillingRate.MaxLength = 18
        Me.txtBillingRate.MendatroryField = True
        Me.txtBillingRate.MyLinkLable1 = Me.lblBillingRate
        Me.txtBillingRate.MyLinkLable2 = Nothing
        Me.txtBillingRate.Name = "txtBillingRate"
        Me.txtBillingRate.Size = New System.Drawing.Size(165, 20)
        Me.txtBillingRate.TabIndex = 5
        Me.txtBillingRate.Text = "0"
        Me.txtBillingRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBillingRate.Value = 0
        '
        'lblBillingRate
        '
        Me.lblBillingRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillingRate.Location = New System.Drawing.Point(5, 104)
        Me.lblBillingRate.Name = "lblBillingRate"
        Me.lblBillingRate.Size = New System.Drawing.Size(63, 16)
        Me.lblBillingRate.TabIndex = 224
        Me.lblBillingRate.Text = "Billing Rate"
        '
        'fndUser
        '
        Me.fndUser.Location = New System.Drawing.Point(114, 257)
        Me.fndUser.MendatroryField = True
        Me.fndUser.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndUser.MyLinkLable1 = Me.lblUser
        Me.fndUser.MyLinkLable2 = Nothing
        Me.fndUser.MyReadOnly = False
        Me.fndUser.Name = "fndUser"
        Me.fndUser.Size = New System.Drawing.Size(165, 19)
        Me.fndUser.TabIndex = 8
        Me.fndUser.Value = ""
        '
        'lblUser
        '
        Me.lblUser.Location = New System.Drawing.Point(6, 257)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(29, 18)
        Me.lblUser.TabIndex = 222
        Me.lblUser.Text = "User"
        '
        'lblTaskCode
        '
        Me.lblTaskCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaskCode.Location = New System.Drawing.Point(3, 13)
        Me.lblTaskCode.Name = "lblTaskCode"
        Me.lblTaskCode.Size = New System.Drawing.Size(87, 16)
        Me.lblTaskCode.TabIndex = 37
        Me.lblTaskCode.Text = "Employee Code"
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(318, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 1
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeName.Location = New System.Drawing.Point(3, 36)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(90, 16)
        Me.lblEmployeeName.TabIndex = 36
        Me.lblEmployeeName.Text = "Employee Name"
        '
        'txtEmpName
        '
        Me.txtEmpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpName.Location = New System.Drawing.Point(114, 34)
        Me.txtEmpName.MaxLength = 50
        Me.txtEmpName.MendatroryField = True
        Me.txtEmpName.MyLinkLable1 = Nothing
        Me.txtEmpName.MyLinkLable2 = Nothing
        Me.txtEmpName.Name = "txtEmpName"
        '
        '
        '
        Me.txtEmpName.RootElement.StretchVertically = True
        Me.txtEmpName.Size = New System.Drawing.Size(387, 20)
        Me.txtEmpName.TabIndex = 2
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(114, 11)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.TabStop = False
        Me.txtCode.Value = ""
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(571, 192)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(571, 192)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(17, 10)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(96, 10)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(721, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(798, 20)
        Me.RadMenu1.TabIndex = 43
        Me.RadMenu1.Text = "RadMenu1"
        '
        'rdmenufile
        '
        Me.rdmenufile.AccessibleDescription = "RadMenuItem1"
        Me.rdmenufile.AccessibleName = "RadMenuItem1"
        Me.rdmenufile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Import"
        Me.RadMenuItem1.AccessibleName = "Import"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Import"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export"
        Me.RadMenuItem2.AccessibleName = "Export"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmPJCEmployeeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(798, 504)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmPJCEmployeeMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Employee Master"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblCustBillingRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyToAllCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmailId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmailId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEarningCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEarningCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUnitCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnitCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBillingRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillingRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaskCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblTaskCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtEmpName As common.Controls.MyTextBox
    Friend WithEvents lblEmployeeName As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndUser As common.UserControls.txtFinder
    Friend WithEvents lblUser As common.Controls.MyLabel
    Friend WithEvents txtBillingRate As common.MyNumBox
    Friend WithEvents lblBillingRate As common.Controls.MyLabel
    Friend WithEvents txtUnitCost As common.MyNumBox
    Friend WithEvents lblUnitCost As common.Controls.MyLabel
    Friend WithEvents lblUserName As common.Controls.MyTextBox
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents lblEarningCode As common.Controls.MyLabel
    Friend WithEvents txtEarningCode As common.Controls.MyTextBox
    Friend WithEvents lblEmailId As common.Controls.MyLabel
    Friend WithEvents txtEmailId As common.Controls.MyTextBox
    Friend WithEvents RadTextBox1 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents chkInactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkApplyToAllCust As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblCustBillingRate As common.Controls.MyLabel
End Class

