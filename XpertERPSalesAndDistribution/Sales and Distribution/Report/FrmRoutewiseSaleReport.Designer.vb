Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRoutewiseSaleReport
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
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvtemplate = New common.MyCheckBoxGrid
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.chktempselect = New common.Controls.MyRadioButton
        Me.chktempall = New common.Controls.MyRadioButton
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.dtptyear = New common.Controls.MyDateTimePicker
        Me.dtpfyear = New common.Controls.MyDateTimePicker
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgroute = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkSelectRoute = New common.Controls.MyRadioButton
        Me.chkAllRoute = New common.Controls.MyRadioButton
        Me.grpCompany = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rbtnSelectCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnAllCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.Customer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgcategory = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkcategoryselect = New common.Controls.MyRadioButton
        Me.chkcategoryall = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.cg = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkselectcustomer = New common.Controls.MyRadioButton
        Me.chkallcustomer = New common.Controls.MyRadioButton
        Me.lblconversion = New common.Controls.MyLabel
        Me.ddlconversion = New common.Controls.MyComboBox
        Me.dtptodate = New common.Controls.MyDateTimePicker
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.ddlcategory = New common.Controls.MyComboBox
        Me.dtpFromdate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkSelectRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCompany.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Customer.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cg.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblconversion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlconversion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlcategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox9)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.dtptyear)
        Me.RadGroupBox1.Controls.Add(Me.dtpfyear)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.grpCompany)
        Me.RadGroupBox1.Controls.Add(Me.Customer)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.cg)
        Me.RadGroupBox1.Controls.Add(Me.lblconversion)
        Me.RadGroupBox1.Controls.Add(Me.ddlconversion)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.ddlcategory)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 14)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(764, 572)
        Me.RadGroupBox1.TabIndex = 1
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.cgvtemplate)
        Me.RadGroupBox9.Controls.Add(Me.Panel8)
        Me.RadGroupBox9.HeaderText = "Template"
        Me.RadGroupBox9.Location = New System.Drawing.Point(392, 404)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(365, 154)
        Me.RadGroupBox9.TabIndex = 306
        Me.RadGroupBox9.Text = "Template"
        '
        'cgvtemplate
        '
        Me.cgvtemplate.CheckedValue = Nothing
        Me.cgvtemplate.DataSource = Nothing
        Me.cgvtemplate.DisplayMember = "Name"
        Me.cgvtemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvtemplate.Location = New System.Drawing.Point(10, 40)
        Me.cgvtemplate.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvtemplate.MyShowHeadrText = False
        Me.cgvtemplate.Name = "cgvtemplate"
        Me.cgvtemplate.Size = New System.Drawing.Size(345, 104)
        Me.cgvtemplate.TabIndex = 2
        Me.cgvtemplate.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chktempselect)
        Me.Panel8.Controls.Add(Me.chktempall)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(345, 20)
        Me.Panel8.TabIndex = 1
        '
        'chktempselect
        '
        Me.chktempselect.Location = New System.Drawing.Point(152, 1)
        Me.chktempselect.MyLinkLable1 = Nothing
        Me.chktempselect.MyLinkLable2 = Nothing
        Me.chktempselect.Name = "chktempselect"
        Me.chktempselect.Size = New System.Drawing.Size(50, 18)
        Me.chktempselect.TabIndex = 2
        Me.chktempselect.Text = "Select"
        '
        'chktempall
        '
        Me.chktempall.Location = New System.Drawing.Point(96, 2)
        Me.chktempall.MyLinkLable1 = Nothing
        Me.chktempall.MyLinkLable2 = Nothing
        Me.chktempall.Name = "chktempall"
        Me.chktempall.Size = New System.Drawing.Size(33, 18)
        Me.chktempall.TabIndex = 1
        Me.chktempall.Text = "All"
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(257, 33)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(44, 18)
        Me.MyLabel3.TabIndex = 319
        Me.MyLabel3.Text = "To Year"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(15, 33)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(57, 18)
        Me.MyLabel2.TabIndex = 320
        Me.MyLabel2.Text = "From Year"
        '
        'dtptyear
        '
        Me.dtptyear.CustomFormat = "yyyy"
        Me.dtptyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptyear.Location = New System.Drawing.Point(327, 31)
        Me.dtptyear.MendatroryField = False
        Me.dtptyear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptyear.MyLinkLable1 = Nothing
        Me.dtptyear.MyLinkLable2 = Nothing
        Me.dtptyear.Name = "dtptyear"
        Me.dtptyear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptyear.Size = New System.Drawing.Size(122, 20)
        Me.dtptyear.TabIndex = 318
        Me.dtptyear.TabStop = False
        Me.dtptyear.Text = "2011"
        Me.dtptyear.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'dtpfyear
        '
        Me.dtpfyear.CustomFormat = "yyyy"
        Me.dtpfyear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfyear.Location = New System.Drawing.Point(105, 31)
        Me.dtpfyear.MendatroryField = False
        Me.dtpfyear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfyear.MyLinkLable1 = Nothing
        Me.dtpfyear.MyLinkLable2 = Nothing
        Me.dtpfyear.Name = "dtpfyear"
        Me.dtpfyear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfyear.Size = New System.Drawing.Size(133, 20)
        Me.dtpfyear.TabIndex = 317
        Me.dtpfyear.TabStop = False
        Me.dtpfyear.Text = "2011"
        Me.dtpfyear.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgroute)
        Me.RadGroupBox4.Controls.Add(Me.Panel6)
        Me.RadGroupBox4.HeaderText = "Route"
        Me.RadGroupBox4.Location = New System.Drawing.Point(392, 224)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(365, 162)
        Me.RadGroupBox4.TabIndex = 19
        Me.RadGroupBox4.Text = "Route"
        '
        'cbgroute
        '
        Me.cbgroute.CheckedValue = Nothing
        Me.cbgroute.DataSource = Nothing
        Me.cbgroute.DisplayMember = "Name"
        Me.cbgroute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgroute.Location = New System.Drawing.Point(10, 40)
        Me.cbgroute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgroute.MyShowHeadrText = False
        Me.cbgroute.Name = "cbgroute"
        Me.cbgroute.Size = New System.Drawing.Size(345, 112)
        Me.cbgroute.TabIndex = 2
        Me.cbgroute.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkSelectRoute)
        Me.Panel6.Controls.Add(Me.chkAllRoute)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(345, 20)
        Me.Panel6.TabIndex = 1
        '
        'chkSelectRoute
        '
        Me.chkSelectRoute.Location = New System.Drawing.Point(128, 1)
        Me.chkSelectRoute.MyLinkLable1 = Nothing
        Me.chkSelectRoute.MyLinkLable2 = Nothing
        Me.chkSelectRoute.Name = "chkSelectRoute"
        Me.chkSelectRoute.Size = New System.Drawing.Size(50, 18)
        Me.chkSelectRoute.TabIndex = 2
        Me.chkSelectRoute.Text = "Select"
        '
        'chkAllRoute
        '
        Me.chkAllRoute.Location = New System.Drawing.Point(73, 1)
        Me.chkAllRoute.MyLinkLable1 = Nothing
        Me.chkAllRoute.MyLinkLable2 = Nothing
        Me.chkAllRoute.Name = "chkAllRoute"
        Me.chkAllRoute.Size = New System.Drawing.Size(33, 18)
        Me.chkAllRoute.TabIndex = 1
        Me.chkAllRoute.Text = "All"
        '
        'grpCompany
        '
        Me.grpCompany.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCompany.Controls.Add(Me.gvDB)
        Me.grpCompany.Controls.Add(Me.Panel3)
        Me.grpCompany.HeaderText = "Company"
        Me.grpCompany.Location = New System.Drawing.Point(5, 62)
        Me.grpCompany.Name = "grpCompany"
        Me.grpCompany.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCompany.Size = New System.Drawing.Size(381, 153)
        Me.grpCompany.TabIndex = 49
        Me.grpCompany.Text = "Company"
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
        Me.gvDB.Size = New System.Drawing.Size(361, 97)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnSelectCompany)
        Me.Panel3.Controls.Add(Me.rbtnAllCompany)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(361, 26)
        Me.Panel3.TabIndex = 0
        '
        'rbtnSelectCompany
        '
        Me.rbtnSelectCompany.Location = New System.Drawing.Point(134, 4)
        Me.rbtnSelectCompany.Name = "rbtnSelectCompany"
        Me.rbtnSelectCompany.Size = New System.Drawing.Size(50, 18)
        Me.rbtnSelectCompany.TabIndex = 1
        Me.rbtnSelectCompany.Text = "Select"
        '
        'rbtnAllCompany
        '
        Me.rbtnAllCompany.Location = New System.Drawing.Point(90, 4)
        Me.rbtnAllCompany.Name = "rbtnAllCompany"
        Me.rbtnAllCompany.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAllCompany.TabIndex = 0
        Me.rbtnAllCompany.Text = "All"
        '
        'Customer
        '
        Me.Customer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Customer.Controls.Add(Me.cbgcategory)
        Me.Customer.Controls.Add(Me.Panel4)
        Me.Customer.HeaderText = "Customer Type"
        Me.Customer.Location = New System.Drawing.Point(5, 222)
        Me.Customer.Name = "Customer"
        Me.Customer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Customer.Size = New System.Drawing.Size(381, 164)
        Me.Customer.TabIndex = 48
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
        Me.cbgcategory.Size = New System.Drawing.Size(361, 114)
        Me.cbgcategory.TabIndex = 1
        Me.cbgcategory.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkcategoryselect)
        Me.Panel4.Controls.Add(Me.chkcategoryall)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(361, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkcategoryselect
        '
        Me.chkcategoryselect.Location = New System.Drawing.Point(140, 1)
        Me.chkcategoryselect.MyLinkLable1 = Nothing
        Me.chkcategoryselect.MyLinkLable2 = Nothing
        Me.chkcategoryselect.Name = "chkcategoryselect"
        Me.chkcategoryselect.Size = New System.Drawing.Size(50, 18)
        Me.chkcategoryselect.TabIndex = 1
        Me.chkcategoryselect.Text = "Select"
        '
        'chkcategoryall
        '
        Me.chkcategoryall.Location = New System.Drawing.Point(88, 1)
        Me.chkcategoryall.MyLinkLable1 = Nothing
        Me.chkcategoryall.MyLinkLable2 = Nothing
        Me.chkcategoryall.Name = "chkcategoryall"
        Me.chkcategoryall.Size = New System.Drawing.Size(33, 18)
        Me.chkcategoryall.TabIndex = 0
        Me.chkcategoryall.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 392)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(373, 166)
        Me.RadGroupBox3.TabIndex = 40
        Me.RadGroupBox3.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(353, 116)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocSelect)
        Me.Panel2.Controls.Add(Me.chkLocAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(353, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(128, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(73, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'cg
        '
        Me.cg.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cg.Controls.Add(Me.cbgCustomer)
        Me.cg.Controls.Add(Me.Panel1)
        Me.cg.HeaderText = "Customer"
        Me.cg.Location = New System.Drawing.Point(392, 62)
        Me.cg.Name = "cg"
        Me.cg.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cg.Size = New System.Drawing.Size(365, 155)
        Me.cg.TabIndex = 38
        Me.cg.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(345, 105)
        Me.cbgCustomer.TabIndex = 2
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkselectcustomer)
        Me.Panel1.Controls.Add(Me.chkallcustomer)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(345, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkselectcustomer
        '
        Me.chkselectcustomer.Location = New System.Drawing.Point(116, 1)
        Me.chkselectcustomer.MyLinkLable1 = Nothing
        Me.chkselectcustomer.MyLinkLable2 = Nothing
        Me.chkselectcustomer.Name = "chkselectcustomer"
        Me.chkselectcustomer.Size = New System.Drawing.Size(50, 18)
        Me.chkselectcustomer.TabIndex = 2
        Me.chkselectcustomer.Text = "Select"
        '
        'chkallcustomer
        '
        Me.chkallcustomer.Location = New System.Drawing.Point(60, 1)
        Me.chkallcustomer.MyLinkLable1 = Nothing
        Me.chkallcustomer.MyLinkLable2 = Nothing
        Me.chkallcustomer.Name = "chkallcustomer"
        Me.chkallcustomer.Size = New System.Drawing.Size(33, 18)
        Me.chkallcustomer.TabIndex = 1
        Me.chkallcustomer.Text = "All"
        '
        'lblconversion
        '
        Me.lblconversion.Location = New System.Drawing.Point(495, 38)
        Me.lblconversion.Name = "lblconversion"
        Me.lblconversion.Size = New System.Drawing.Size(62, 18)
        Me.lblconversion.TabIndex = 27
        Me.lblconversion.Text = "Conversion"
        '
        'ddlconversion
        '
        RadListDataItem1.Text = "Converted"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "RAW"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "80z"
        RadListDataItem3.TextWrap = True
        Me.ddlconversion.Items.Add(RadListDataItem1)
        Me.ddlconversion.Items.Add(RadListDataItem2)
        Me.ddlconversion.Items.Add(RadListDataItem3)
        Me.ddlconversion.Location = New System.Drawing.Point(570, 38)
        Me.ddlconversion.MendatroryField = False
        Me.ddlconversion.MyLinkLable1 = Nothing
        Me.ddlconversion.MyLinkLable2 = Nothing
        Me.ddlconversion.Name = "ddlconversion"
        Me.ddlconversion.Size = New System.Drawing.Size(124, 20)
        Me.ddlconversion.TabIndex = 26
        '
        'dtptodate
        '
        Me.dtptodate.CustomFormat = "MMM"
        Me.dtptodate.Location = New System.Drawing.Point(327, 5)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(122, 20)
        Me.dtptodate.TabIndex = 25
        Me.dtptodate.TabStop = False
        Me.dtptodate.Value = New Date(1753, 1, 1, 0, 0, 0, 0)
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(495, 7)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(54, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Hierarchy"
        '
        'ddlcategory
        '
        RadListDataItem4.Text = "HOS"
        RadListDataItem4.TextWrap = True
        RadListDataItem5.Text = "TDM"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "ADC"
        RadListDataItem6.TextWrap = True
        RadListDataItem7.Text = "CE"
        RadListDataItem7.TextWrap = True
        RadListDataItem8.Text = "SalesMan"
        RadListDataItem8.TextWrap = True
        Me.ddlcategory.Items.Add(RadListDataItem4)
        Me.ddlcategory.Items.Add(RadListDataItem5)
        Me.ddlcategory.Items.Add(RadListDataItem6)
        Me.ddlcategory.Items.Add(RadListDataItem7)
        Me.ddlcategory.Items.Add(RadListDataItem8)
        Me.ddlcategory.Location = New System.Drawing.Point(570, 7)
        Me.ddlcategory.MendatroryField = False
        Me.ddlcategory.MyLinkLable1 = Nothing
        Me.ddlcategory.MyLinkLable2 = Nothing
        Me.ddlcategory.Name = "ddlcategory"
        Me.ddlcategory.Size = New System.Drawing.Size(124, 20)
        Me.ddlcategory.TabIndex = 18
        '
        'dtpFromdate
        '
        Me.dtpFromdate.CustomFormat = "MMM"
        Me.dtpFromdate.Location = New System.Drawing.Point(105, 7)
        Me.dtpFromdate.MendatroryField = False
        Me.dtpFromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.MyLinkLable1 = Nothing
        Me.dtpFromdate.MyLinkLable2 = Nothing
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.Size = New System.Drawing.Size(133, 20)
        Me.dtpFromdate.TabIndex = 14
        Me.dtpFromdate.TabStop = False
        Me.dtpFromdate.Value = New Date(1753, 1, 1, 0, 0, 0, 0)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(257, 7)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 18)
        Me.RadLabel2.TabIndex = 16
        Me.RadLabel2.Text = "To Month"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(69, 18)
        Me.RadLabel1.TabIndex = 17
        Me.RadLabel1.Text = "From Month"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(709, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 24
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(18, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 23
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(94, 9)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 22
        Me.btnprint.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(793, 644)
        Me.SplitContainer1.SplitterDistance = 601
        Me.SplitContainer1.TabIndex = 2
        '
        'FrmRoutewiseSaleReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(793, 644)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRoutewiseSaleReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Routewise Monthly Sale Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkSelectRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCompany.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Customer.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cg.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblconversion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlconversion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlcategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgroute As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkSelectRoute As common.Controls.MyRadioButton
    Friend WithEvents chkAllRoute As common.Controls.MyRadioButton
    Friend WithEvents grpCompany As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbtnSelectCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnAllCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents cg As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkselectcustomer As common.Controls.MyRadioButton
    Friend WithEvents chkallcustomer As common.Controls.MyRadioButton
    Friend WithEvents lblconversion As common.Controls.MyLabel
    Friend WithEvents ddlconversion As common.Controls.MyComboBox
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents ddlcategory As common.Controls.MyComboBox
    Friend WithEvents dtpFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtptyear As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfyear As common.Controls.MyDateTimePicker
    Friend WithEvents Customer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgcategory As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkcategoryselect As common.Controls.MyRadioButton
    Friend WithEvents chkcategoryall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvtemplate As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chktempselect As common.Controls.MyRadioButton
    Friend WithEvents chktempall As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

