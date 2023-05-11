<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemCommissionSummary
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.Customer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgcategory = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkcategoryselect = New common.Controls.MyRadioButton
        Me.chkcategoryall = New common.Controls.MyRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvRoute = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkrotesel = New common.Controls.MyRadioButton
        Me.chkAllroute = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdoSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.rdoDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.ddlhier = New common.Controls.MyComboBox
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvemployee = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkempselect = New common.Controls.MyRadioButton
        Me.chkempall = New common.Controls.MyRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbtnSelectCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnAllCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Customer.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkrotesel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllroute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdoSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdoDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlhier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkempselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkempall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Customer)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.ddlhier)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(899, 571)
        Me.RadGroupBox1.TabIndex = 0
        '
        'Customer
        '
        Me.Customer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Customer.Controls.Add(Me.cbgcategory)
        Me.Customer.Controls.Add(Me.Panel5)
        Me.Customer.HeaderText = "Customer Type"
        Me.Customer.Location = New System.Drawing.Point(13, 406)
        Me.Customer.Name = "Customer"
        Me.Customer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Customer.Size = New System.Drawing.Size(431, 155)
        Me.Customer.TabIndex = 320
        Me.Customer.Text = "Customer Type"
        '
        'cbgcategory
        '
        Me.cbgcategory.CheckedValue = Nothing
        Me.cbgcategory.DataSource = Nothing
        Me.cbgcategory.DisplayMember = "Name"
        Me.cbgcategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgcategory.Location = New System.Drawing.Point(10, 40)
        Me.cbgcategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgcategory.MyShowHeadrText = False
        Me.cbgcategory.Name = "cbgcategory"
        Me.cbgcategory.Size = New System.Drawing.Size(411, 105)
        Me.cbgcategory.TabIndex = 1
        Me.cbgcategory.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkcategoryselect)
        Me.Panel5.Controls.Add(Me.chkcategoryall)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(411, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkcategoryselect
        '
        Me.chkcategoryselect.Location = New System.Drawing.Point(202, 1)
        Me.chkcategoryselect.MyLinkLable1 = Nothing
        Me.chkcategoryselect.MyLinkLable2 = Nothing
        Me.chkcategoryselect.Name = "chkcategoryselect"
        Me.chkcategoryselect.Size = New System.Drawing.Size(50, 18)
        Me.chkcategoryselect.TabIndex = 1
        Me.chkcategoryselect.Text = "Select"
        '
        'chkcategoryall
        '
        Me.chkcategoryall.Location = New System.Drawing.Point(139, 1)
        Me.chkcategoryall.MyLinkLable1 = Nothing
        Me.chkcategoryall.MyLinkLable2 = Nothing
        Me.chkcategoryall.Name = "chkcategoryall"
        Me.chkcategoryall.Size = New System.Drawing.Size(33, 18)
        Me.chkcategoryall.TabIndex = 0
        Me.chkcategoryall.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cgvRoute)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Route"
        Me.RadGroupBox6.Location = New System.Drawing.Point(455, 231)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(431, 169)
        Me.RadGroupBox6.TabIndex = 310
        Me.RadGroupBox6.Text = "Route"
        '
        'cgvRoute
        '
        Me.cgvRoute.CheckedValue = Nothing
        Me.cgvRoute.DataSource = Nothing
        Me.cgvRoute.DisplayMember = "Name"
        Me.cgvRoute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvRoute.Location = New System.Drawing.Point(10, 40)
        Me.cgvRoute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvRoute.MyShowHeadrText = False
        Me.cgvRoute.Name = "cgvRoute"
        Me.cgvRoute.Size = New System.Drawing.Size(411, 119)
        Me.cgvRoute.TabIndex = 2
        Me.cgvRoute.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkrotesel)
        Me.Panel4.Controls.Add(Me.chkAllroute)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(411, 20)
        Me.Panel4.TabIndex = 1
        '
        'chkrotesel
        '
        Me.chkrotesel.Location = New System.Drawing.Point(189, 1)
        Me.chkrotesel.MyLinkLable1 = Nothing
        Me.chkrotesel.MyLinkLable2 = Nothing
        Me.chkrotesel.Name = "chkrotesel"
        Me.chkrotesel.Size = New System.Drawing.Size(50, 18)
        Me.chkrotesel.TabIndex = 2
        Me.chkrotesel.Text = "Select"
        '
        'chkAllroute
        '
        Me.chkAllroute.Location = New System.Drawing.Point(140, 1)
        Me.chkAllroute.MyLinkLable1 = Nothing
        Me.chkAllroute.MyLinkLable2 = Nothing
        Me.chkAllroute.Name = "chkAllroute"
        Me.chkAllroute.Size = New System.Drawing.Size(33, 18)
        Me.chkAllroute.TabIndex = 1
        Me.chkAllroute.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdoSummary)
        Me.RadGroupBox3.Controls.Add(Me.rdoDetail)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 30)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(226, 28)
        Me.RadGroupBox3.TabIndex = 319
        '
        'rdoSummary
        '
        Me.rdoSummary.Location = New System.Drawing.Point(138, 3)
        Me.rdoSummary.Name = "rdoSummary"
        Me.rdoSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdoSummary.TabIndex = 1
        Me.rdoSummary.Text = "Summary"
        '
        'rdoDetail
        '
        Me.rdoDetail.Location = New System.Drawing.Point(27, 3)
        Me.rdoDetail.Name = "rdoDetail"
        Me.rdoDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdoDetail.TabIndex = 0
        Me.rdoDetail.Text = "Detail"
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(280, 33)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(54, 18)
        Me.RadLabel3.TabIndex = 318
        Me.RadLabel3.Text = "Hierarchy"
        '
        'ddlhier
        '
        Me.ddlhier.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "HOS"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "TDM"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "ADC"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "CE"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "Sales Man"
        RadListDataItem5.TextWrap = True
        Me.ddlhier.Items.Add(RadListDataItem1)
        Me.ddlhier.Items.Add(RadListDataItem2)
        Me.ddlhier.Items.Add(RadListDataItem3)
        Me.ddlhier.Items.Add(RadListDataItem4)
        Me.ddlhier.Items.Add(RadListDataItem5)
        Me.ddlhier.Location = New System.Drawing.Point(363, 33)
        Me.ddlhier.MendatroryField = False
        Me.ddlhier.MyLinkLable1 = Nothing
        Me.ddlhier.MyLinkLable2 = Nothing
        Me.ddlhier.Name = "ddlhier"
        Me.ddlhier.Size = New System.Drawing.Size(137, 20)
        Me.ddlhier.TabIndex = 317
        Me.ddlhier.Text = "HOS"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cgvemployee)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Employee"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 57)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(431, 160)
        Me.RadGroupBox2.TabIndex = 314
        Me.RadGroupBox2.Text = "Employee"
        '
        'cgvemployee
        '
        Me.cgvemployee.CheckedValue = Nothing
        Me.cgvemployee.DataSource = Nothing
        Me.cgvemployee.DisplayMember = "Name"
        Me.cgvemployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvemployee.Location = New System.Drawing.Point(10, 40)
        Me.cgvemployee.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvemployee.MyShowHeadrText = False
        Me.cgvemployee.Name = "cgvemployee"
        Me.cgvemployee.Size = New System.Drawing.Size(411, 110)
        Me.cgvemployee.TabIndex = 2
        Me.cgvemployee.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkempselect)
        Me.Panel2.Controls.Add(Me.chkempall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(411, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkempselect
        '
        Me.chkempselect.Location = New System.Drawing.Point(192, 1)
        Me.chkempselect.MyLinkLable1 = Nothing
        Me.chkempselect.MyLinkLable2 = Nothing
        Me.chkempselect.Name = "chkempselect"
        Me.chkempselect.Size = New System.Drawing.Size(50, 18)
        Me.chkempselect.TabIndex = 2
        Me.chkempselect.Text = "Select"
        '
        'chkempall
        '
        Me.chkempall.Location = New System.Drawing.Point(144, 1)
        Me.chkempall.MyLinkLable1 = Nothing
        Me.chkempall.MyLinkLable2 = Nothing
        Me.chkempall.Name = "chkempall"
        Me.chkempall.Size = New System.Drawing.Size(33, 18)
        Me.chkempall.TabIndex = 1
        Me.chkempall.Text = "All"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(363, 7)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(137, 20)
        Me.txtToDate.TabIndex = 311
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(94, 6)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(145, 20)
        Me.txtFromDate.TabIndex = 310
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(280, 6)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 312
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(18, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 313
        Me.RadLabel1.Text = "From Date"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(457, 59)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(431, 159)
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
        Me.cbgLocation.Size = New System.Drawing.Size(411, 109)
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
        Me.Panel3.Size = New System.Drawing.Size(411, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(189, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(140, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gvDB)
        Me.RadGroupBox4.Controls.Add(Me.Panel1)
        Me.RadGroupBox4.HeaderText = "Company"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 224)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(431, 176)
        Me.RadGroupBox4.TabIndex = 303
        Me.RadGroupBox4.Text = "Company"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 46)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(411, 120)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnSelectCompany)
        Me.Panel1.Controls.Add(Me.rbtnAllCompany)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(411, 26)
        Me.Panel1.TabIndex = 0
        '
        'rbtnSelectCompany
        '
        Me.rbtnSelectCompany.Location = New System.Drawing.Point(191, 5)
        Me.rbtnSelectCompany.Name = "rbtnSelectCompany"
        Me.rbtnSelectCompany.Size = New System.Drawing.Size(50, 18)
        Me.rbtnSelectCompany.TabIndex = 1
        Me.rbtnSelectCompany.Text = "Select"
        '
        'rbtnAllCompany
        '
        Me.rbtnAllCompany.Location = New System.Drawing.Point(146, 5)
        Me.rbtnAllCompany.Name = "rbtnAllCompany"
        Me.rbtnAllCompany.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAllCompany.TabIndex = 0
        Me.rbtnAllCompany.Text = "All"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(832, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(12, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 316
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(87, 10)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 315
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(924, 620)
        Me.SplitContainer1.SplitterDistance = 580
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmItemCommissionSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 620)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmItemCommissionSummary"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Commission Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Customer.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkrotesel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllroute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdoSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdoDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlhier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkempselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkempall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnSelectCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnAllCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvemployee As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkempselect As common.Controls.MyRadioButton
    Friend WithEvents chkempall As common.Controls.MyRadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ddlhier As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdoSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdoDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkrotesel As common.Controls.MyRadioButton
    Friend WithEvents chkAllroute As common.Controls.MyRadioButton
    Friend WithEvents Customer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgcategory As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkcategoryselect As common.Controls.MyRadioButton
    Friend WithEvents chkcategoryall As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

