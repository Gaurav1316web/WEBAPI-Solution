<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptInventoryMovement
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbItemSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdbOthers = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbRaw = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbFinish = New Telerik.WinControls.UI.RadRadioButton
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgmrp = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkmrpselect = New common.Controls.MyRadioButton
        Me.chkmrpall = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkIemAll = New common.Controls.MyRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.Export = New Telerik.WinControls.UI.RadMenuItem
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rdbItemSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.rdbOthers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbRaw, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFinish, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkmrpselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkmrpall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(655, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(12, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 5
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(86, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = ">>>"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 46)
        '
        '
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(471, 100)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(740, 540)
        Me.SplitContainer1.SplitterDistance = 501
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(740, 501)
        Me.RadPageView1.TabIndex = 25
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(719, 453)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(671, 447)
        Me.RadGroupBox1.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbItemSummary)
        Me.GroupBox1.Controls.Add(Me.rdbDetail)
        Me.GroupBox1.Controls.Add(Me.rdbSummary)
        Me.GroupBox1.Controls.Add(Me.RadButton2)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(304, 45)
        Me.GroupBox1.TabIndex = 109
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select"
        '
        'rdbItemSummary
        '
        Me.rdbItemSummary.Location = New System.Drawing.Point(97, 21)
        Me.rdbItemSummary.Name = "rdbItemSummary"
        Me.rdbItemSummary.Size = New System.Drawing.Size(93, 18)
        Me.rdbItemSummary.TabIndex = 105
        Me.rdbItemSummary.Text = "Item Summary"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(212, 21)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 106
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(10, 21)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 104
        Me.rdbSummary.Text = "Summary"
        '
        'RadButton2
        '
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(703, 20)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(17, 19)
        Me.RadButton2.TabIndex = 41
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdbOthers)
        Me.GroupBox2.Controls.Add(Me.rdbRaw)
        Me.GroupBox2.Controls.Add(Me.rdbFinish)
        Me.GroupBox2.Controls.Add(Me.RadButton1)
        Me.GroupBox2.Location = New System.Drawing.Point(327, 1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(304, 45)
        Me.GroupBox2.TabIndex = 108
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Select"
        '
        'rdbOthers
        '
        Me.rdbOthers.Location = New System.Drawing.Point(204, 20)
        Me.rdbOthers.Name = "rdbOthers"
        Me.rdbOthers.Size = New System.Drawing.Size(54, 18)
        Me.rdbOthers.TabIndex = 106
        Me.rdbOthers.Text = "Others"
        '
        'rdbRaw
        '
        Me.rdbRaw.Location = New System.Drawing.Point(128, 20)
        Me.rdbRaw.Name = "rdbRaw"
        Me.rdbRaw.Size = New System.Drawing.Size(44, 18)
        Me.rdbRaw.TabIndex = 105
        Me.rdbRaw.Text = "Raw "
        '
        'rdbFinish
        '
        Me.rdbFinish.Location = New System.Drawing.Point(10, 20)
        Me.rdbFinish.Name = "rdbFinish"
        Me.rdbFinish.Size = New System.Drawing.Size(97, 18)
        Me.rdbFinish.TabIndex = 104
        Me.rdbFinish.Text = "Finished Goods"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(703, 20)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(17, 19)
        Me.RadButton1.TabIndex = 41
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgmrp)
        Me.RadGroupBox3.Controls.Add(Me.Panel4)
        Me.RadGroupBox3.HeaderText = "MRP"
        Me.RadGroupBox3.Location = New System.Drawing.Point(336, 86)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(327, 169)
        Me.RadGroupBox3.TabIndex = 312
        Me.RadGroupBox3.Text = "MRP"
        '
        'cbgmrp
        '
        Me.cbgmrp.CheckedValue = Nothing
        Me.cbgmrp.DataSource = Nothing
        Me.cbgmrp.DisplayMember = "Name"
        Me.cbgmrp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgmrp.Location = New System.Drawing.Point(10, 40)
        Me.cbgmrp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgmrp.MyShowHeadrText = False
        Me.cbgmrp.Name = "cbgmrp"
        Me.cbgmrp.Size = New System.Drawing.Size(307, 119)
        Me.cbgmrp.TabIndex = 1
        Me.cbgmrp.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkmrpselect)
        Me.Panel4.Controls.Add(Me.chkmrpall)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(307, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkmrpselect
        '
        Me.chkmrpselect.Location = New System.Drawing.Point(161, 1)
        Me.chkmrpselect.MyLinkLable1 = Nothing
        Me.chkmrpselect.MyLinkLable2 = Nothing
        Me.chkmrpselect.Name = "chkmrpselect"
        Me.chkmrpselect.Size = New System.Drawing.Size(50, 18)
        Me.chkmrpselect.TabIndex = 1
        Me.chkmrpselect.Text = "Select"
        '
        'chkmrpall
        '
        Me.chkmrpall.Location = New System.Drawing.Point(110, 1)
        Me.chkmrpall.MyLinkLable1 = Nothing
        Me.chkmrpall.MyLinkLable2 = Nothing
        Me.chkmrpall.Name = "chkmrpall"
        Me.chkmrpall.Size = New System.Drawing.Size(33, 18)
        Me.chkmrpall.TabIndex = 0
        Me.chkmrpall.TabStop = True
        Me.chkmrpall.Text = "All"
        Me.chkmrpall.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgItem)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Item"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 274)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(325, 160)
        Me.RadGroupBox2.TabIndex = 311
        Me.RadGroupBox2.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(305, 110)
        Me.cbgItem.TabIndex = 2
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkItemSelect)
        Me.Panel1.Controls.Add(Me.chkIemAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(305, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(148, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 2
        Me.chkItemSelect.Text = "Select"
        '
        'chkIemAll
        '
        Me.chkIemAll.Location = New System.Drawing.Point(99, 1)
        Me.chkIemAll.MyLinkLable1 = Nothing
        Me.chkIemAll.MyLinkLable2 = Nothing
        Me.chkIemAll.Name = "chkIemAll"
        Me.chkIemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkIemAll.TabIndex = 1
        Me.chkIemAll.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(5, 86)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(325, 169)
        Me.RadGroupBox5.TabIndex = 309
        Me.RadGroupBox5.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(305, 119)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocSelect)
        Me.Panel3.Controls.Add(Me.chkLocAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(305, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(150, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(101, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(227, 58)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(70, 60)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(165, 60)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 9
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(5, 62)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 10
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(719, 453)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(719, 453)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Export, Me.PDF})
        Me.btnExport.Location = New System.Drawing.Point(160, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 127
        Me.btnExport.Text = "Export"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Image = Global.ERP.My.Resources.Resources.MSE
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        Me.Export.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        Me.PDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmRptInventoryMovement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(740, 540)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmRptInventoryMovement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Inventory Movement Report"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rdbItemSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.rdbOthers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbRaw, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFinish, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkmrpselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkmrpall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadRadioButton1 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbOthers As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbRaw As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbFinish As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgmrp As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkmrpselect As common.Controls.MyRadioButton
    Friend WithEvents chkmrpall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkIemAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rdbItemSummary As Telerik.WinControls.UI.RadRadioButton
End Class

