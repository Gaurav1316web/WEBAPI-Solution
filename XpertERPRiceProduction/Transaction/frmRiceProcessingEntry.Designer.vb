Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRiceProcessingEntry
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtto_loc_code = New common.UserControls.txtFinder
        Me.txtto_loc_desc = New common.Controls.MyLabel
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.txteffective_cost = New common.Controls.MyLabel
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txttotal_cost = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.txtadmin_cost = New common.Controls.MyLabel
        Me.txtadmin_charge = New common.MyNumBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtprocess_cost = New common.Controls.MyLabel
        Me.lblMasterItem = New common.Controls.MyLabel
        Me.txtfrm_loc_code = New common.UserControls.txtFinder
        Me.txtfrm_loc_name = New common.Controls.MyLabel
        Me.lblCode = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.txtCode = New common.UserControls.txtNavigator
        Me.txtprocess_charge = New common.MyNumBox
        Me.lblBuildQty = New common.Controls.MyLabel
        Me.btnNew = New Telerik.WinControls.UI.RadButton
        Me.lblBomDesc = New common.Controls.MyLabel
        Me.lblBomDate = New common.Controls.MyLabel
        Me.txtDescription = New common.Controls.MyTextBox
        Me.dtpDate = New common.Controls.MyDateTimePicker
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv = New common.UserControls.MyRadGridView
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv_finish = New common.UserControls.MyRadGridView
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnsavelayout = New Telerik.WinControls.UI.RadMenuItem
        Me.btndeletelayout = New Telerik.WinControls.UI.RadMenuItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtto_loc_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txteffective_cost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotal_cost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadmin_cost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtadmin_charge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtprocess_cost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfrm_loc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtprocess_charge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv_finish, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_finish.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(901, 473)
        Me.SplitContainer1.SplitterDistance = 434
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView1.Size = New System.Drawing.Size(901, 414)
        Me.RadPageView1.TabIndex = 7
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(98.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(880, 366)
        Me.RadPageViewPage1.Text = "Processing Entry"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtto_loc_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtto_loc_desc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txteffective_cost)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txttotal_cost)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtadmin_cost)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtadmin_charge)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtprocess_cost)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMasterItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtfrm_loc_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtfrm_loc_name)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtprocess_charge)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBuildQty)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBomDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(1)
        Me.SplitContainer2.Size = New System.Drawing.Size(880, 366)
        Me.SplitContainer2.SplitterDistance = 174
        Me.SplitContainer2.TabIndex = 24
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(18, 81)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel1.TabIndex = 37
        Me.MyLabel1.Text = "To Location"
        '
        'txtto_loc_code
        '
        Me.txtto_loc_code.Location = New System.Drawing.Point(129, 81)
        Me.txtto_loc_code.MendatroryField = True
        Me.txtto_loc_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtto_loc_code.MyLinkLable1 = Me.MyLabel1
        Me.txtto_loc_code.MyLinkLable2 = Me.txtto_loc_desc
        Me.txtto_loc_code.MyReadOnly = False
        Me.txtto_loc_code.MyShowMasterFormButton = False
        Me.txtto_loc_code.Name = "txtto_loc_code"
        Me.txtto_loc_code.Size = New System.Drawing.Size(129, 19)
        Me.txtto_loc_code.TabIndex = 4
        Me.txtto_loc_code.Value = ""
        '
        'txtto_loc_desc
        '
        Me.txtto_loc_desc.AutoSize = False
        Me.txtto_loc_desc.BorderVisible = True
        Me.txtto_loc_desc.Location = New System.Drawing.Point(262, 81)
        Me.txtto_loc_desc.Name = "txtto_loc_desc"
        Me.txtto_loc_desc.Size = New System.Drawing.Size(336, 19)
        Me.txtto_loc_desc.TabIndex = 38
        Me.txtto_loc_desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(350, 151)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel8.TabIndex = 35
        Me.MyLabel8.Text = "Effective Cost"
        '
        'txteffective_cost
        '
        Me.txteffective_cost.AutoSize = False
        Me.txteffective_cost.BorderVisible = True
        Me.txteffective_cost.Location = New System.Drawing.Point(446, 148)
        Me.txteffective_cost.Name = "txteffective_cost"
        Me.txteffective_cost.Size = New System.Drawing.Size(152, 19)
        Me.txteffective_cost.TabIndex = 34
        Me.txteffective_cost.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(18, 151)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel6.TabIndex = 33
        Me.MyLabel6.Text = "Total Cost"
        '
        'txttotal_cost
        '
        Me.txttotal_cost.AutoSize = False
        Me.txttotal_cost.BorderVisible = True
        Me.txttotal_cost.Location = New System.Drawing.Point(129, 150)
        Me.txttotal_cost.Name = "txttotal_cost"
        Me.txttotal_cost.Size = New System.Drawing.Size(129, 19)
        Me.txttotal_cost.TabIndex = 32
        Me.txttotal_cost.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(350, 128)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel3.TabIndex = 31
        Me.MyLabel3.Text = "Admin Cost"
        '
        'txtadmin_cost
        '
        Me.txtadmin_cost.AutoSize = False
        Me.txtadmin_cost.BorderVisible = True
        Me.txtadmin_cost.Location = New System.Drawing.Point(446, 125)
        Me.txtadmin_cost.Name = "txtadmin_cost"
        Me.txtadmin_cost.Size = New System.Drawing.Size(152, 19)
        Me.txtadmin_cost.TabIndex = 30
        Me.txtadmin_cost.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtadmin_charge
        '
        Me.txtadmin_charge.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtadmin_charge.DecimalPlaces = 0
        Me.txtadmin_charge.Location = New System.Drawing.Point(129, 127)
        Me.txtadmin_charge.MendatroryField = True
        Me.txtadmin_charge.MyLinkLable1 = Me.MyLabel5
        Me.txtadmin_charge.MyLinkLable2 = Me.txtadmin_cost
        Me.txtadmin_charge.Name = "txtadmin_charge"
        Me.txtadmin_charge.Size = New System.Drawing.Size(129, 20)
        Me.txtadmin_charge.TabIndex = 6
        Me.txtadmin_charge.Text = "0"
        Me.txtadmin_charge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtadmin_charge.Value = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(18, 129)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel5.TabIndex = 29
        Me.MyLabel5.Text = "Admin Charge"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(350, 104)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel2.TabIndex = 27
        Me.MyLabel2.Text = "Processing Cost"
        '
        'txtprocess_cost
        '
        Me.txtprocess_cost.AutoSize = False
        Me.txtprocess_cost.BorderVisible = True
        Me.txtprocess_cost.Location = New System.Drawing.Point(446, 101)
        Me.txtprocess_cost.Name = "txtprocess_cost"
        Me.txtprocess_cost.Size = New System.Drawing.Size(152, 19)
        Me.txtprocess_cost.TabIndex = 26
        Me.txtprocess_cost.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMasterItem
        '
        Me.lblMasterItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMasterItem.Location = New System.Drawing.Point(18, 59)
        Me.lblMasterItem.Name = "lblMasterItem"
        Me.lblMasterItem.Size = New System.Drawing.Size(78, 18)
        Me.lblMasterItem.TabIndex = 24
        Me.lblMasterItem.Text = "From Location"
        '
        'txtfrm_loc_code
        '
        Me.txtfrm_loc_code.Location = New System.Drawing.Point(129, 59)
        Me.txtfrm_loc_code.MendatroryField = True
        Me.txtfrm_loc_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfrm_loc_code.MyLinkLable1 = Me.lblMasterItem
        Me.txtfrm_loc_code.MyLinkLable2 = Me.txtfrm_loc_name
        Me.txtfrm_loc_code.MyReadOnly = False
        Me.txtfrm_loc_code.MyShowMasterFormButton = False
        Me.txtfrm_loc_code.Name = "txtfrm_loc_code"
        Me.txtfrm_loc_code.Size = New System.Drawing.Size(129, 19)
        Me.txtfrm_loc_code.TabIndex = 3
        Me.txtfrm_loc_code.Value = ""
        '
        'txtfrm_loc_name
        '
        Me.txtfrm_loc_name.AutoSize = False
        Me.txtfrm_loc_name.BorderVisible = True
        Me.txtfrm_loc_name.Location = New System.Drawing.Point(262, 59)
        Me.txtfrm_loc_name.Name = "txtfrm_loc_name"
        Me.txtfrm_loc_name.Size = New System.Drawing.Size(336, 19)
        Me.txtfrm_loc_name.TabIndex = 25
        Me.txtfrm_loc_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(17, 14)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(82, 16)
        Me.lblCode.TabIndex = 13
        Me.lblCode.Text = "Process Code"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(771, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 14
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(129, 12)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(322, 21)
        Me.txtCode.TabIndex = 9
        Me.txtCode.Value = ""
        '
        'txtprocess_charge
        '
        Me.txtprocess_charge.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtprocess_charge.DecimalPlaces = 0
        Me.txtprocess_charge.Location = New System.Drawing.Point(129, 103)
        Me.txtprocess_charge.MendatroryField = True
        Me.txtprocess_charge.MyLinkLable1 = Me.lblBuildQty
        Me.txtprocess_charge.MyLinkLable2 = Me.txtprocess_cost
        Me.txtprocess_charge.Name = "txtprocess_charge"
        Me.txtprocess_charge.Size = New System.Drawing.Size(129, 20)
        Me.txtprocess_charge.TabIndex = 5
        Me.txtprocess_charge.Text = "0"
        Me.txtprocess_charge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtprocess_charge.Value = 0
        '
        'lblBuildQty
        '
        Me.lblBuildQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBuildQty.Location = New System.Drawing.Point(18, 105)
        Me.lblBuildQty.Name = "lblBuildQty"
        Me.lblBuildQty.Size = New System.Drawing.Size(103, 16)
        Me.lblBuildQty.TabIndex = 21
        Me.lblBuildQty.Text = "Processing Charge"
        '
        'btnNew
        '
        Me.btnNew.Image = My.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(452, 13)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'lblBomDesc
        '
        Me.lblBomDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDesc.Location = New System.Drawing.Point(18, 37)
        Me.lblBomDesc.Name = "lblBomDesc"
        Me.lblBomDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblBomDesc.TabIndex = 12
        Me.lblBomDesc.Text = "Description"
        '
        'lblBomDate
        '
        Me.lblBomDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBomDate.Location = New System.Drawing.Point(472, 16)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBomDate.TabIndex = 19
        Me.lblBomDate.Text = "Date"
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(129, 36)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblBomDesc
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(469, 20)
        Me.txtDescription.TabIndex = 2
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(506, 14)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.lblBomDate
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(92, 18)
        Me.dtpDate.TabIndex = 1
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011"
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Mixing Item(s)"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(878, 186)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Mixing Item(s)"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(2, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(874, 166)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage3.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(880, 366)
        Me.RadPageViewPage3.Text = "Produced Item"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv_finish)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(880, 342)
        Me.RadGroupBox2.TabIndex = 24
        '
        'gv_finish
        '
        Me.gv_finish.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_finish.Location = New System.Drawing.Point(2, 18)
        '
        'gv_finish
        '
        Me.gv_finish.MasterTemplate.AllowDragToGroup = False
        Me.gv_finish.MasterTemplate.EnableFiltering = True
        Me.gv_finish.MasterTemplate.EnableGrouping = False
        Me.gv_finish.Name = "gv_finish"
        Me.gv_finish.ShowGroupPanel = False
        Me.gv_finish.Size = New System.Drawing.Size(876, 322)
        Me.gv_finish.TabIndex = 0
        Me.gv_finish.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(880, 366)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(880, 366)
        Me.UcAttachment1.TabIndex = 8
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(901, 20)
        Me.RadMenu1.TabIndex = 6
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnsavelayout, Me.btndeletelayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnsavelayout
        '
        Me.btnsavelayout.AccessibleDescription = "Save LayOut"
        Me.btnsavelayout.AccessibleName = "Save LayOut"
        Me.btnsavelayout.Name = "btnsavelayout"
        Me.btnsavelayout.Text = "Save LayOut"
        Me.btnsavelayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btndeletelayout
        '
        Me.btndeletelayout.AccessibleDescription = "Delete LayOut"
        Me.btndeletelayout.AccessibleName = "Delete LayOut"
        Me.btndeletelayout.Name = "btndeletelayout"
        Me.btndeletelayout.Text = "Delete LayOut"
        Me.btndeletelayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(823, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(155, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.MyLabel4)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 342)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(880, 24)
        Me.RadPanel1.TabIndex = 1
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.ForeColor = System.Drawing.Color.MediumBlue
        Me.MyLabel4.Location = New System.Drawing.Point(466, 4)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(411, 18)
        Me.MyLabel4.TabIndex = 0
        Me.MyLabel4.Text = "Click Shift+Control+C for auto calculation.(Set any one item cost to 0(zero))"
        Me.MyLabel4.TextAlignment = System.Drawing.ContentAlignment.TopRight
        '
        'FrmRiceProcessingEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRiceProcessingEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmRiceProcessingEntry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtto_loc_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txteffective_cost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotal_cost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadmin_cost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtadmin_charge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtprocess_cost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfrm_loc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtprocess_charge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBomDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv_finish.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_finish, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents txtfrm_loc_code As common.UserControls.txtFinder
    Friend WithEvents txtfrm_loc_name As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblBuildQty As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents txtprocess_charge As common.MyNumBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBomDesc As common.Controls.MyLabel
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_finish As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents btnsavelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btndeletelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtprocess_cost As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtadmin_cost As common.Controls.MyLabel
    Friend WithEvents txtadmin_charge As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txteffective_cost As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txttotal_cost As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtto_loc_code As common.UserControls.txtFinder
    Friend WithEvents txtto_loc_desc As common.Controls.MyLabel
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

