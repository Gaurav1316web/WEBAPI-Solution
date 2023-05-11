Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRiceMixingEntry
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
        Me.txtto_loc_name = New common.Controls.MyLabel
        Me.lblMasterItem = New common.Controls.MyLabel
        Me.txtfrm_loc_code = New common.UserControls.txtFinder
        Me.txtfrm_loc_name = New common.Controls.MyLabel
        Me.lblCode = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.txtmixing_uom = New common.UserControls.txtFinder
        Me.lblBuildQty = New common.Controls.MyLabel
        Me.txtCode = New common.UserControls.txtNavigator
        Me.txtcharge = New common.MyNumBox
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
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnsaveLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.btndeletelayout = New Telerik.WinControls.UI.RadMenuItem
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
        CType(Me.txtto_loc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfrm_loc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcharge, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(917, 456)
        Me.SplitContainer1.SplitterDistance = 417
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(1, 1)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(915, 415)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(894, 367)
        Me.RadPageViewPage1.Text = "Mixing Entry"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtto_loc_name)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMasterItem)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtfrm_loc_code)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtfrm_loc_name)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtmixing_uom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtcharge)
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
        Me.SplitContainer2.Size = New System.Drawing.Size(894, 367)
        Me.SplitContainer2.SplitterDistance = 133
        Me.SplitContainer2.TabIndex = 24
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(18, 82)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel1.TabIndex = 27
        Me.MyLabel1.Text = "To Location"
        '
        'txtto_loc_code
        '
        Me.txtto_loc_code.Location = New System.Drawing.Point(115, 82)
        Me.txtto_loc_code.MendatroryField = True
        Me.txtto_loc_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtto_loc_code.MyLinkLable1 = Me.MyLabel1
        Me.txtto_loc_code.MyLinkLable2 = Me.txtto_loc_name
        Me.txtto_loc_code.MyReadOnly = False
        Me.txtto_loc_code.MyShowMasterFormButton = False
        Me.txtto_loc_code.Name = "txtto_loc_code"
        Me.txtto_loc_code.Size = New System.Drawing.Size(129, 19)
        Me.txtto_loc_code.TabIndex = 4
        Me.txtto_loc_code.Value = ""
        '
        'txtto_loc_name
        '
        Me.txtto_loc_name.AutoSize = False
        Me.txtto_loc_name.BorderVisible = True
        Me.txtto_loc_name.Location = New System.Drawing.Point(248, 82)
        Me.txtto_loc_name.Name = "txtto_loc_name"
        Me.txtto_loc_name.Size = New System.Drawing.Size(336, 19)
        Me.txtto_loc_name.TabIndex = 28
        Me.txtto_loc_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.txtfrm_loc_code.Location = New System.Drawing.Point(115, 59)
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
        Me.txtfrm_loc_name.Location = New System.Drawing.Point(248, 59)
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
        Me.lblCode.Size = New System.Drawing.Size(74, 16)
        Me.lblCode.TabIndex = 13
        Me.lblCode.Text = "Mixing Code"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(785, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 14
        '
        'txtmixing_uom
        '
        Me.txtmixing_uom.Location = New System.Drawing.Point(248, 105)
        Me.txtmixing_uom.MendatroryField = True
        Me.txtmixing_uom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmixing_uom.MyLinkLable1 = Me.lblBuildQty
        Me.txtmixing_uom.MyLinkLable2 = Nothing
        Me.txtmixing_uom.MyReadOnly = False
        Me.txtmixing_uom.MyShowMasterFormButton = False
        Me.txtmixing_uom.Name = "txtmixing_uom"
        Me.txtmixing_uom.Size = New System.Drawing.Size(106, 20)
        Me.txtmixing_uom.TabIndex = 6
        Me.txtmixing_uom.Value = ""
        '
        'lblBuildQty
        '
        Me.lblBuildQty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblBuildQty.Location = New System.Drawing.Point(18, 107)
        Me.lblBuildQty.Name = "lblBuildQty"
        Me.lblBuildQty.Size = New System.Drawing.Size(79, 16)
        Me.lblBuildQty.TabIndex = 21
        Me.lblBuildQty.Text = "Mixing Charge"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(115, 12)
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
        'txtcharge
        '
        Me.txtcharge.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtcharge.DecimalPlaces = 0
        Me.txtcharge.Location = New System.Drawing.Point(115, 105)
        Me.txtcharge.MendatroryField = True
        Me.txtcharge.MyLinkLable1 = Me.lblBuildQty
        Me.txtcharge.MyLinkLable2 = Nothing
        Me.txtcharge.Name = "txtcharge"
        Me.txtcharge.Size = New System.Drawing.Size(129, 20)
        Me.txtcharge.TabIndex = 5
        Me.txtcharge.Text = "0"
        Me.txtcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtcharge.Value = 0
        '
        'btnNew
        '
        Me.btnNew.Image = My.Resources._new
        Me.btnNew.Location = New System.Drawing.Point(438, 13)
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
        Me.lblBomDate.Location = New System.Drawing.Point(458, 16)
        Me.lblBomDate.Name = "lblBomDate"
        Me.lblBomDate.Size = New System.Drawing.Size(30, 16)
        Me.lblBomDate.TabIndex = 19
        Me.lblBomDate.Text = "Date"
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(115, 36)
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
        Me.dtpDate.Location = New System.Drawing.Point(492, 14)
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
        Me.RadGroupBox1.Size = New System.Drawing.Size(892, 228)
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
        Me.gv.Size = New System.Drawing.Size(888, 208)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(894, 367)
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
        Me.RadGroupBox2.Size = New System.Drawing.Size(894, 367)
        Me.RadGroupBox2.TabIndex = 24
        '
        'gv_finish
        '
        Me.gv_finish.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_finish.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gv_finish.MasterTemplate.AllowDragToGroup = False
        Me.gv_finish.MasterTemplate.EnableFiltering = True
        Me.gv_finish.MasterTemplate.EnableGrouping = False
        Me.gv_finish.Name = "gv_finish"
        Me.gv_finish.ShowGroupPanel = False
        Me.gv_finish.Size = New System.Drawing.Size(890, 347)
        Me.gv_finish.TabIndex = 0
        Me.gv_finish.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.UcAttachment1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(894, 367)
        Me.RadPageViewPage2.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(894, 367)
        Me.UcAttachment1.TabIndex = 8
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(837, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(152, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(4, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 22)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(68, 22)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(917, 20)
        Me.RadMenu1.TabIndex = 5
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnsaveLayout, Me.btndeletelayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnsaveLayout
        '
        Me.btnsaveLayout.AccessibleDescription = "Save Layout"
        Me.btnsaveLayout.AccessibleName = "Save Layout"
        Me.btnsaveLayout.Name = "btnsaveLayout"
        Me.btnsaveLayout.Text = "Save Layout"
        Me.btnsaveLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btndeletelayout
        '
        Me.btndeletelayout.AccessibleDescription = "Delete Layout"
        Me.btndeletelayout.AccessibleName = "Delete Layout"
        Me.btndeletelayout.Name = "btndeletelayout"
        Me.btndeletelayout.Text = "Delete Layout"
        Me.btndeletelayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmRiceMixingEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 476)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmRiceMixingEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmRiceMixingEntry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
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
        CType(Me.txtto_loc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfrm_loc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBuildQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcharge, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblBomDesc As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblBomDate As common.Controls.MyLabel
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtcharge As common.MyNumBox
    Friend WithEvents lblBuildQty As common.Controls.MyLabel
    Friend WithEvents txtmixing_uom As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_finish As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtto_loc_code As common.UserControls.txtFinder
    Friend WithEvents txtto_loc_name As common.Controls.MyLabel
    Friend WithEvents lblMasterItem As common.Controls.MyLabel
    Friend WithEvents txtfrm_loc_code As common.UserControls.txtFinder
    Friend WithEvents txtfrm_loc_name As common.Controls.MyLabel
    Friend WithEvents btnsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btndeletelayout As Telerik.WinControls.UI.RadMenuItem
End Class

