<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerListRpt
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgZone = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkZoneSelect = New common.Controls.MyRadioButton()
        Me.chkZoneAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgRoute = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkRouteSelect = New common.Controls.MyRadioButton()
        Me.chkRouteAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustGrp = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkCustGrpSelect = New common.Controls.MyRadioButton()
        Me.chkCustGrpAll = New common.Controls.MyRadioButton()
        Me.chkSettlementPending = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkAll = New System.Windows.Forms.RadioButton()
        Me.chkActive = New System.Windows.Forms.RadioButton()
        Me.chkInactive = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgcustomer = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkcustomerSelect = New common.Controls.MyRadioButton()
        Me.chkcustomerAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkZoneSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkZoneAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCustGrpSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustGrpAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSettlementPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkcustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.BottomLeft
        Me.btnReset.Location = New System.Drawing.Point(106, 2)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(93, 22)
        Me.btnReset.TabIndex = 11
        Me.btnReset.Text = "&Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.ImageAlignment = System.Drawing.ContentAlignment.BottomLeft
        Me.btnPrint.Location = New System.Drawing.Point(199, 2)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(93, 22)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "&Print"
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(833, 611)
        Me.SplitContainer1.SplitterDistance = 582
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(833, 582)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.TabStop = False
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.chkSettlementPending)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(812, 534)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgZone)
        Me.RadGroupBox1.Controls.Add(Me.Panel3)
        Me.RadGroupBox1.HeaderText = "Zone"
        Me.RadGroupBox1.Location = New System.Drawing.Point(404, 301)
        Me.RadGroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox1.Size = New System.Drawing.Size(385, 238)
        Me.RadGroupBox1.TabIndex = 35
        Me.RadGroupBox1.Text = "Zone"
        '
        'cbgZone
        '
        Me.cbgZone.CheckedValue = Nothing
        Me.cbgZone.DataSource = Nothing
        Me.cbgZone.DisplayMember = "Name"
        Me.cbgZone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgZone.Location = New System.Drawing.Point(13, 45)
        Me.cbgZone.Margin = New System.Windows.Forms.Padding(4)
        Me.cbgZone.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgZone.MyShowHeadrText = False
        Me.cbgZone.Name = "cbgZone"
        Me.cbgZone.Size = New System.Drawing.Size(359, 181)
        Me.cbgZone.TabIndex = 1
        Me.cbgZone.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkZoneSelect)
        Me.Panel3.Controls.Add(Me.chkZoneAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(13, 25)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(359, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkZoneSelect
        '
        Me.chkZoneSelect.Location = New System.Drawing.Point(169, 1)
        Me.chkZoneSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.chkZoneSelect.MyLinkLable1 = Nothing
        Me.chkZoneSelect.MyLinkLable2 = Nothing
        Me.chkZoneSelect.Name = "chkZoneSelect"
        Me.chkZoneSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkZoneSelect.TabIndex = 1
        Me.chkZoneSelect.Text = "Select"
        '
        'chkZoneAll
        '
        Me.chkZoneAll.Location = New System.Drawing.Point(131, 1)
        Me.chkZoneAll.Margin = New System.Windows.Forms.Padding(4)
        Me.chkZoneAll.MyLinkLable1 = Nothing
        Me.chkZoneAll.MyLinkLable2 = Nothing
        Me.chkZoneAll.Name = "chkZoneAll"
        Me.chkZoneAll.Size = New System.Drawing.Size(33, 18)
        Me.chkZoneAll.TabIndex = 0
        Me.chkZoneAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgRoute)
        Me.RadGroupBox4.Controls.Add(Me.Panel1)
        Me.RadGroupBox4.HeaderText = "Route"
        Me.RadGroupBox4.Location = New System.Drawing.Point(11, 301)
        Me.RadGroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox4.Size = New System.Drawing.Size(385, 238)
        Me.RadGroupBox4.TabIndex = 34
        Me.RadGroupBox4.Text = "Route"
        '
        'cbgRoute
        '
        Me.cbgRoute.CheckedValue = Nothing
        Me.cbgRoute.DataSource = Nothing
        Me.cbgRoute.DisplayMember = "Name"
        Me.cbgRoute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRoute.Location = New System.Drawing.Point(13, 45)
        Me.cbgRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.cbgRoute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRoute.MyShowHeadrText = False
        Me.cbgRoute.Name = "cbgRoute"
        Me.cbgRoute.Size = New System.Drawing.Size(359, 181)
        Me.cbgRoute.TabIndex = 1
        Me.cbgRoute.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkRouteSelect)
        Me.Panel1.Controls.Add(Me.chkRouteAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(13, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(359, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkRouteSelect
        '
        Me.chkRouteSelect.Location = New System.Drawing.Point(173, 1)
        Me.chkRouteSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.chkRouteSelect.MyLinkLable1 = Nothing
        Me.chkRouteSelect.MyLinkLable2 = Nothing
        Me.chkRouteSelect.Name = "chkRouteSelect"
        Me.chkRouteSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkRouteSelect.TabIndex = 1
        Me.chkRouteSelect.Text = "Select"
        '
        'chkRouteAll
        '
        Me.chkRouteAll.Location = New System.Drawing.Point(132, 1)
        Me.chkRouteAll.Margin = New System.Windows.Forms.Padding(4)
        Me.chkRouteAll.MyLinkLable1 = Nothing
        Me.chkRouteAll.MyLinkLable2 = Nothing
        Me.chkRouteAll.Name = "chkRouteAll"
        Me.chkRouteAll.Size = New System.Drawing.Size(33, 18)
        Me.chkRouteAll.TabIndex = 0
        Me.chkRouteAll.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgCustGrp)
        Me.RadGroupBox5.Controls.Add(Me.Panel4)
        Me.RadGroupBox5.HeaderText = "Customer Group"
        Me.RadGroupBox5.Location = New System.Drawing.Point(404, 55)
        Me.RadGroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox5.Size = New System.Drawing.Size(385, 238)
        Me.RadGroupBox5.TabIndex = 33
        Me.RadGroupBox5.Text = "Customer Group"
        '
        'cbgCustGrp
        '
        Me.cbgCustGrp.CheckedValue = Nothing
        Me.cbgCustGrp.DataSource = Nothing
        Me.cbgCustGrp.DisplayMember = "Name"
        Me.cbgCustGrp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustGrp.Location = New System.Drawing.Point(13, 45)
        Me.cbgCustGrp.Margin = New System.Windows.Forms.Padding(4)
        Me.cbgCustGrp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustGrp.MyShowHeadrText = False
        Me.cbgCustGrp.Name = "cbgCustGrp"
        Me.cbgCustGrp.Size = New System.Drawing.Size(359, 181)
        Me.cbgCustGrp.TabIndex = 1
        Me.cbgCustGrp.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkCustGrpSelect)
        Me.Panel4.Controls.Add(Me.chkCustGrpAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(13, 25)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(359, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkCustGrpSelect
        '
        Me.chkCustGrpSelect.Location = New System.Drawing.Point(169, 1)
        Me.chkCustGrpSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCustGrpSelect.MyLinkLable1 = Nothing
        Me.chkCustGrpSelect.MyLinkLable2 = Nothing
        Me.chkCustGrpSelect.Name = "chkCustGrpSelect"
        Me.chkCustGrpSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustGrpSelect.TabIndex = 1
        Me.chkCustGrpSelect.Text = "Select"
        '
        'chkCustGrpAll
        '
        Me.chkCustGrpAll.Location = New System.Drawing.Point(131, 1)
        Me.chkCustGrpAll.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCustGrpAll.MyLinkLable1 = Nothing
        Me.chkCustGrpAll.MyLinkLable2 = Nothing
        Me.chkCustGrpAll.Name = "chkCustGrpAll"
        Me.chkCustGrpAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustGrpAll.TabIndex = 0
        Me.chkCustGrpAll.Text = "All"
        '
        'chkSettlementPending
        '
        Me.chkSettlementPending.Location = New System.Drawing.Point(386, 22)
        Me.chkSettlementPending.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSettlementPending.Name = "chkSettlementPending"
        Me.chkSettlementPending.Size = New System.Drawing.Size(178, 18)
        Me.chkSettlementPending.TabIndex = 32
        Me.chkSettlementPending.Text = "Full && Final Settlement Pending"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkAll)
        Me.RadGroupBox3.Controls.Add(Me.chkActive)
        Me.RadGroupBox3.Controls.Add(Me.chkInactive)
        Me.RadGroupBox3.HeaderText = "Customer Type"
        Me.RadGroupBox3.Location = New System.Drawing.Point(15, 9)
        Me.RadGroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox3.Size = New System.Drawing.Size(363, 44)
        Me.RadGroupBox3.TabIndex = 30
        Me.RadGroupBox3.Text = "Customer Type"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(245, 14)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(95, 17)
        Me.chkAll.TabIndex = 29
        Me.chkAll.Text = "All Customers"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Location = New System.Drawing.Point(9, 14)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(107, 17)
        Me.chkActive.TabIndex = 27
        Me.chkActive.Text = "Active Customer"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'chkInactive
        '
        Me.chkInactive.AutoSize = True
        Me.chkInactive.Location = New System.Drawing.Point(122, 14)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(116, 17)
        Me.chkInactive.TabIndex = 28
        Me.chkInactive.Text = "Inactive Customer"
        Me.chkInactive.UseVisualStyleBackColor = True
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgcustomer)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Customers"
        Me.RadGroupBox2.Location = New System.Drawing.Point(11, 55)
        Me.RadGroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox2.Size = New System.Drawing.Size(385, 238)
        Me.RadGroupBox2.TabIndex = 4
        Me.RadGroupBox2.Text = "Customers"
        '
        'cbgcustomer
        '
        Me.cbgcustomer.CheckedValue = Nothing
        Me.cbgcustomer.DataSource = Nothing
        Me.cbgcustomer.DisplayMember = "Name"
        Me.cbgcustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgcustomer.Location = New System.Drawing.Point(13, 45)
        Me.cbgcustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.cbgcustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgcustomer.MyShowHeadrText = False
        Me.cbgcustomer.Name = "cbgcustomer"
        Me.cbgcustomer.Size = New System.Drawing.Size(359, 181)
        Me.cbgcustomer.TabIndex = 2
        Me.cbgcustomer.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkcustomerSelect)
        Me.Panel2.Controls.Add(Me.chkcustomerAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(13, 25)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(359, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkcustomerSelect
        '
        Me.chkcustomerSelect.Location = New System.Drawing.Point(173, 1)
        Me.chkcustomerSelect.Margin = New System.Windows.Forms.Padding(4)
        Me.chkcustomerSelect.MyLinkLable1 = Nothing
        Me.chkcustomerSelect.MyLinkLable2 = Nothing
        Me.chkcustomerSelect.Name = "chkcustomerSelect"
        Me.chkcustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkcustomerSelect.TabIndex = 2
        Me.chkcustomerSelect.Text = "Select"
        '
        'chkcustomerAll
        '
        Me.chkcustomerAll.Location = New System.Drawing.Point(132, 1)
        Me.chkcustomerAll.Margin = New System.Windows.Forms.Padding(4)
        Me.chkcustomerAll.MyLinkLable1 = Nothing
        Me.chkcustomerAll.MyLinkLable2 = Nothing
        Me.chkcustomerAll.Name = "chkcustomerAll"
        Me.chkcustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkcustomerAll.TabIndex = 1
        Me.chkcustomerAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(812, 534)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(812, 534)
        Me.gv.TabIndex = 4
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageAlignment = System.Drawing.ContentAlignment.BottomLeft
        Me.btnClose.Location = New System.Drawing.Point(733, 1)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(93, 22)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "Close"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.ImageAlignment = System.Drawing.ContentAlignment.BottomLeft
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadSplitButton1.Location = New System.Drawing.Point(292, 2)
        Me.RadSplitButton1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(93, 22)
        Me.RadSplitButton1.TabIndex = 14
        Me.RadSplitButton1.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.ImageAlignment = System.Drawing.ContentAlignment.BottomLeft
        Me.btnGo.Location = New System.Drawing.Point(13, 2)
        Me.btnGo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(93, 22)
        Me.btnGo.TabIndex = 13
        Me.btnGo.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(833, 20)
        Me.RadMenu1.TabIndex = 64
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'frmCustomerListRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 631)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmCustomerListRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer List Report"
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkZoneSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkZoneAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCustGrpSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustGrpAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSettlementPending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkcustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkAll As System.Windows.Forms.RadioButton
    Friend WithEvents chkActive As System.Windows.Forms.RadioButton
    Friend WithEvents chkInactive As System.Windows.Forms.RadioButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustGrp As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkCustGrpSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustGrpAll As common.Controls.MyRadioButton
    Friend WithEvents chkSettlementPending As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgcustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkcustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkcustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkRouteSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRouteAll As common.Controls.MyRadioButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents cbgZone As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As Panel
    Friend WithEvents chkZoneSelect As common.Controls.MyRadioButton
    Friend WithEvents chkZoneAll As common.Controls.MyRadioButton
    Friend WithEvents btnClose As RadButton
End Class

