Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVisi_Install_Pullout_Report
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
        Me.chkSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.chkDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.gbVisi = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVisi = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkVisiSelect = New common.Controls.MyRadioButton
        Me.chkVisiAll = New common.Controls.MyRadioButton
        Me.gbCustomer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyCheckBoxGrid4 = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.MyRadioButton7 = New common.Controls.MyRadioButton
        Me.MyRadioButton8 = New common.Controls.MyRadioButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyCheckBoxGrid5 = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.MyRadioButton9 = New common.Controls.MyRadioButton
        Me.MyRadioButton10 = New common.Controls.MyRadioButton
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyCheckBoxGrid6 = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.MyRadioButton11 = New common.Controls.MyRadioButton
        Me.MyRadioButton12 = New common.Controls.MyRadioButton
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.gbLocation = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLoc = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.GV1 = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbVisi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVisi.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkVisiSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVisiAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCustomer.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.MyRadioButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.MyRadioButton9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.MyRadioButton11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbLocation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(840, 490)
        Me.SplitContainer1.SplitterDistance = 460
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(840, 460)
        Me.RadPageView1.TabIndex = 117
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.chkSummary)
        Me.RadPageViewPage1.Controls.Add(Me.chkDetail)
        Me.RadPageViewPage1.Controls.Add(Me.gbVisi)
        Me.RadPageViewPage1.Controls.Add(Me.gbCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.gbLocation)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(819, 412)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(377, 20)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 118
        Me.chkSummary.Text = "Summary"
        '
        'chkDetail
        '
        Me.chkDetail.Location = New System.Drawing.Point(305, 20)
        Me.chkDetail.Name = "chkDetail"
        Me.chkDetail.Size = New System.Drawing.Size(49, 18)
        Me.chkDetail.TabIndex = 117
        Me.chkDetail.Text = "Detail"
        '
        'gbVisi
        '
        Me.gbVisi.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVisi.Controls.Add(Me.cbgVisi)
        Me.gbVisi.Controls.Add(Me.Panel1)
        Me.gbVisi.HeaderText = "Visi"
        Me.gbVisi.Location = New System.Drawing.Point(3, 54)
        Me.gbVisi.Name = "gbVisi"
        Me.gbVisi.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbVisi.Size = New System.Drawing.Size(403, 174)
        Me.gbVisi.TabIndex = 116
        Me.gbVisi.Text = "Visi"
        '
        'cbgVisi
        '
        Me.cbgVisi.CheckedValue = Nothing
        Me.cbgVisi.DataSource = Nothing
        Me.cbgVisi.DisplayMember = "Name"
        Me.cbgVisi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVisi.Location = New System.Drawing.Point(10, 40)
        Me.cbgVisi.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVisi.MyShowHeadrText = False
        Me.cbgVisi.Name = "cbgVisi"
        Me.cbgVisi.Size = New System.Drawing.Size(383, 124)
        Me.cbgVisi.TabIndex = 1
        Me.cbgVisi.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkVisiSelect)
        Me.Panel1.Controls.Add(Me.chkVisiAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(383, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkVisiSelect
        '
        Me.chkVisiSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkVisiSelect.MyLinkLable1 = Nothing
        Me.chkVisiSelect.MyLinkLable2 = Nothing
        Me.chkVisiSelect.Name = "chkVisiSelect"
        Me.chkVisiSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVisiSelect.TabIndex = 1
        Me.chkVisiSelect.Text = "Select"
        '
        'chkVisiAll
        '
        Me.chkVisiAll.Location = New System.Drawing.Point(114, 1)
        Me.chkVisiAll.MyLinkLable1 = Nothing
        Me.chkVisiAll.MyLinkLable2 = Nothing
        Me.chkVisiAll.Name = "chkVisiAll"
        Me.chkVisiAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVisiAll.TabIndex = 0
        Me.chkVisiAll.Text = "All"
        '
        'gbCustomer
        '
        Me.gbCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbCustomer.Controls.Add(Me.cbgCustomer)
        Me.gbCustomer.Controls.Add(Me.Panel8)
        Me.gbCustomer.HeaderText = "Customer"
        Me.gbCustomer.Location = New System.Drawing.Point(411, 54)
        Me.gbCustomer.Name = "gbCustomer"
        Me.gbCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbCustomer.Size = New System.Drawing.Size(403, 174)
        Me.gbCustomer.TabIndex = 115
        Me.gbCustomer.Text = "Customer"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(383, 124)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chkCustomerSelect)
        Me.Panel8.Controls.Add(Me.chkCustomerAll)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(383, 20)
        Me.Panel8.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(114, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox3.Controls.Add(Me.RadGroupBox7)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 6)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(258, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Select Date"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.MyCheckBoxGrid4)
        Me.RadGroupBox5.Controls.Add(Me.Panel5)
        Me.RadGroupBox5.HeaderText = "RadGroupBox5"
        Me.RadGroupBox5.Location = New System.Drawing.Point(398, 316)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(330, 171)
        Me.RadGroupBox5.TabIndex = 115
        Me.RadGroupBox5.Text = "RadGroupBox5"
        '
        'MyCheckBoxGrid4
        '
        Me.MyCheckBoxGrid4.CheckedValue = Nothing
        Me.MyCheckBoxGrid4.DataSource = Nothing
        Me.MyCheckBoxGrid4.DisplayMember = "Name"
        Me.MyCheckBoxGrid4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyCheckBoxGrid4.Location = New System.Drawing.Point(10, 40)
        Me.MyCheckBoxGrid4.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.MyCheckBoxGrid4.MyShowHeadrText = False
        Me.MyCheckBoxGrid4.Name = "MyCheckBoxGrid4"
        Me.MyCheckBoxGrid4.Size = New System.Drawing.Size(310, 121)
        Me.MyCheckBoxGrid4.TabIndex = 1
        Me.MyCheckBoxGrid4.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.MyRadioButton7)
        Me.Panel5.Controls.Add(Me.MyRadioButton8)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(310, 20)
        Me.Panel5.TabIndex = 0
        '
        'MyRadioButton7
        '
        Me.MyRadioButton7.Location = New System.Drawing.Point(165, 1)
        Me.MyRadioButton7.MyLinkLable1 = Nothing
        Me.MyRadioButton7.MyLinkLable2 = Nothing
        Me.MyRadioButton7.Name = "MyRadioButton7"
        Me.MyRadioButton7.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton7.TabIndex = 1
        Me.MyRadioButton7.Text = "Select"
        '
        'MyRadioButton8
        '
        Me.MyRadioButton8.Location = New System.Drawing.Point(114, 1)
        Me.MyRadioButton8.MyLinkLable1 = Nothing
        Me.MyRadioButton8.MyLinkLable2 = Nothing
        Me.MyRadioButton8.Name = "MyRadioButton8"
        Me.MyRadioButton8.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton8.TabIndex = 0
        Me.MyRadioButton8.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(141, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.MyCheckBoxGrid5)
        Me.RadGroupBox6.Controls.Add(Me.Panel6)
        Me.RadGroupBox6.HeaderText = "RadGroupBox6"
        Me.RadGroupBox6.Location = New System.Drawing.Point(399, 139)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(330, 171)
        Me.RadGroupBox6.TabIndex = 114
        Me.RadGroupBox6.Text = "RadGroupBox6"
        '
        'MyCheckBoxGrid5
        '
        Me.MyCheckBoxGrid5.CheckedValue = Nothing
        Me.MyCheckBoxGrid5.DataSource = Nothing
        Me.MyCheckBoxGrid5.DisplayMember = "Name"
        Me.MyCheckBoxGrid5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyCheckBoxGrid5.Location = New System.Drawing.Point(10, 40)
        Me.MyCheckBoxGrid5.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.MyCheckBoxGrid5.MyShowHeadrText = False
        Me.MyCheckBoxGrid5.Name = "MyCheckBoxGrid5"
        Me.MyCheckBoxGrid5.Size = New System.Drawing.Size(310, 121)
        Me.MyCheckBoxGrid5.TabIndex = 1
        Me.MyCheckBoxGrid5.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.MyRadioButton9)
        Me.Panel6.Controls.Add(Me.MyRadioButton10)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(310, 20)
        Me.Panel6.TabIndex = 0
        '
        'MyRadioButton9
        '
        Me.MyRadioButton9.Location = New System.Drawing.Point(165, 1)
        Me.MyRadioButton9.MyLinkLable1 = Nothing
        Me.MyRadioButton9.MyLinkLable2 = Nothing
        Me.MyRadioButton9.Name = "MyRadioButton9"
        Me.MyRadioButton9.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton9.TabIndex = 1
        Me.MyRadioButton9.Text = "Select"
        '
        'MyRadioButton10
        '
        Me.MyRadioButton10.Location = New System.Drawing.Point(114, 1)
        Me.MyRadioButton10.MyLinkLable1 = Nothing
        Me.MyRadioButton10.MyLinkLable2 = Nothing
        Me.MyRadioButton10.Name = "MyRadioButton10"
        Me.MyRadioButton10.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton10.TabIndex = 0
        Me.MyRadioButton10.Text = "All"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.MyCheckBoxGrid6)
        Me.RadGroupBox7.Controls.Add(Me.Panel7)
        Me.RadGroupBox7.HeaderText = "RadGroupBox7"
        Me.RadGroupBox7.Location = New System.Drawing.Point(399, -35)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(330, 171)
        Me.RadGroupBox7.TabIndex = 113
        Me.RadGroupBox7.Text = "RadGroupBox7"
        '
        'MyCheckBoxGrid6
        '
        Me.MyCheckBoxGrid6.CheckedValue = Nothing
        Me.MyCheckBoxGrid6.DataSource = Nothing
        Me.MyCheckBoxGrid6.DisplayMember = "Name"
        Me.MyCheckBoxGrid6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyCheckBoxGrid6.Location = New System.Drawing.Point(10, 40)
        Me.MyCheckBoxGrid6.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.MyCheckBoxGrid6.MyShowHeadrText = False
        Me.MyCheckBoxGrid6.Name = "MyCheckBoxGrid6"
        Me.MyCheckBoxGrid6.Size = New System.Drawing.Size(310, 121)
        Me.MyCheckBoxGrid6.TabIndex = 1
        Me.MyCheckBoxGrid6.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.MyRadioButton11)
        Me.Panel7.Controls.Add(Me.MyRadioButton12)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(310, 20)
        Me.Panel7.TabIndex = 0
        '
        'MyRadioButton11
        '
        Me.MyRadioButton11.Location = New System.Drawing.Point(165, 1)
        Me.MyRadioButton11.MyLinkLable1 = Nothing
        Me.MyRadioButton11.MyLinkLable2 = Nothing
        Me.MyRadioButton11.Name = "MyRadioButton11"
        Me.MyRadioButton11.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton11.TabIndex = 1
        Me.MyRadioButton11.Text = "Select"
        '
        'MyRadioButton12
        '
        Me.MyRadioButton12.Location = New System.Drawing.Point(114, 1)
        Me.MyRadioButton12.MyLinkLable1 = Nothing
        Me.MyRadioButton12.MyLinkLable2 = Nothing
        Me.MyRadioButton12.Name = "MyRadioButton12"
        Me.MyRadioButton12.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton12.TabIndex = 0
        Me.MyRadioButton12.Text = "All"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011 2:29 AM"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011 2:29 AM"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'gbLocation
        '
        Me.gbLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbLocation.Controls.Add(Me.cbgLoc)
        Me.gbLocation.Controls.Add(Me.Panel3)
        Me.gbLocation.HeaderText = "Location"
        Me.gbLocation.Location = New System.Drawing.Point(3, 233)
        Me.gbLocation.Name = "gbLocation"
        Me.gbLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbLocation.Size = New System.Drawing.Size(403, 174)
        Me.gbLocation.TabIndex = 110
        Me.gbLocation.Text = "Location"
        '
        'cbgLoc
        '
        Me.cbgLoc.CheckedValue = Nothing
        Me.cbgLoc.DataSource = Nothing
        Me.cbgLoc.DisplayMember = "Name"
        Me.cbgLoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgLoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLoc.MyShowHeadrText = False
        Me.cbgLoc.Name = "cbgLoc"
        Me.cbgLoc.Size = New System.Drawing.Size(383, 124)
        Me.cbgLoc.TabIndex = 1
        Me.cbgLoc.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocationSelect)
        Me.Panel3.Controls.Add(Me.chkLocationAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(383, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(165, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(114, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(819, 412)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.AllowAddNewRow = False
        Me.GV1.MasterTemplate.AllowEditRow = False
        Me.GV1.Name = "GV1"
        Me.GV1.ShowGroupPanel = False
        Me.GV1.Size = New System.Drawing.Size(819, 412)
        Me.GV1.TabIndex = 0
        Me.GV1.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(763, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 18)
        Me.btnClose.TabIndex = 130
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(154, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 129
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel"
        Me.RadMenuItem1.AccessibleName = "Excel"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF"
        Me.RadMenuItem2.AccessibleName = "PDF"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(78, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 127
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 128
        Me.btnRefresh.Text = ">>>"
        '
        'FrmVisi_Install_Pullout_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(840, 490)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVisi_Install_Pullout_Report"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Market Equipment Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbVisi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVisi.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkVisiSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVisiAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCustomer.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.MyRadioButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.MyRadioButton9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.MyRadioButton11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbLocation.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gbCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyCheckBoxGrid4 As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton7 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton8 As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyCheckBoxGrid5 As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton9 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton10 As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyCheckBoxGrid6 As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents MyRadioButton11 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton12 As common.Controls.MyRadioButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents gbLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoc As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents gbVisi As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVisi As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkVisiSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVisiAll As common.Controls.MyRadioButton
    Friend WithEvents chkSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
End Class

