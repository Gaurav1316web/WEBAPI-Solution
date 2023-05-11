<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRptSales
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
        Me.grpCustomerType = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgcategory = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkcategoryselect = New common.Controls.MyRadioButton
        Me.chkcategoryall = New common.Controls.MyRadioButton
        Me.grpLocation = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.grpCustomer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.grpCompany = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbtnSelectCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnAllCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.rbtnItemWise = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnInvoiceWise = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnCustomerWise = New Telerik.WinControls.UI.RadRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.chkIC = New common.Controls.MyCheckBox
        Me.chkIncludeTransfer = New common.Controls.MyCheckBox
        Me.grpTemplate = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvtemplate = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chktempselect = New common.Controls.MyRadioButton
        Me.chktempall = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvReport = New common.UserControls.MyRadGridView
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel
        Me.pnlAdminSetting = New System.Windows.Forms.Panel
        Me.pnlSaleRegister = New System.Windows.Forms.Panel
        Me.rbtnVAT = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnCST = New Telerik.WinControls.UI.RadRadioButton
        Me.chkMismatch = New Telerik.WinControls.UI.RadCheckBox
        Me.chkReconcile = New Telerik.WinControls.UI.RadCheckBox
        Me.btnExportToExcel = New Telerik.WinControls.UI.RadButton
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        CType(Me.grpCustomerType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomerType.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLocation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomer.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCompany.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnInvoiceWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCustomerWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkIC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTemplate.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        Me.pnlAdminSetting.SuspendLayout()
        Me.pnlSaleRegister.SuspendLayout()
        CType(Me.rbtnVAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpCustomerType
        '
        Me.grpCustomerType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomerType.Controls.Add(Me.cbgcategory)
        Me.grpCustomerType.Controls.Add(Me.Panel4)
        Me.grpCustomerType.HeaderText = "Customer Type"
        Me.grpCustomerType.Location = New System.Drawing.Point(443, 113)
        Me.grpCustomerType.Name = "grpCustomerType"
        Me.grpCustomerType.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomerType.Size = New System.Drawing.Size(414, 169)
        Me.grpCustomerType.TabIndex = 47
        Me.grpCustomerType.Text = "Customer Type"
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
        Me.cbgcategory.Size = New System.Drawing.Size(394, 119)
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
        Me.Panel4.Size = New System.Drawing.Size(394, 20)
        Me.Panel4.TabIndex = 0
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
        'grpLocation
        '
        Me.grpLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpLocation.Controls.Add(Me.cbgLocation)
        Me.grpLocation.Controls.Add(Me.Panel3)
        Me.grpLocation.HeaderText = "Location"
        Me.grpLocation.Location = New System.Drawing.Point(3, 222)
        Me.grpLocation.Name = "grpLocation"
        Me.grpLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpLocation.Size = New System.Drawing.Size(424, 169)
        Me.grpLocation.TabIndex = 308
        Me.grpLocation.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(404, 119)
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
        Me.Panel3.Size = New System.Drawing.Size(404, 20)
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
        'grpCustomer
        '
        Me.grpCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomer.Controls.Add(Me.cbgCustomer)
        Me.grpCustomer.Controls.Add(Me.Panel2)
        Me.grpCustomer.HeaderText = "Customer"
        Me.grpCustomer.Location = New System.Drawing.Point(3, 45)
        Me.grpCustomer.Name = "grpCustomer"
        Me.grpCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomer.Size = New System.Drawing.Size(424, 169)
        Me.grpCustomer.TabIndex = 303
        Me.grpCustomer.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(404, 119)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkCustomerSelect)
        Me.Panel2.Controls.Add(Me.chkCustomerAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(404, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(191, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(140, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'grpCompany
        '
        Me.grpCompany.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCompany.Controls.Add(Me.gvDB)
        Me.grpCompany.Controls.Add(Me.Panel1)
        Me.grpCompany.HeaderText = "Company"
        Me.grpCompany.Location = New System.Drawing.Point(433, 287)
        Me.grpCompany.Name = "grpCompany"
        Me.grpCompany.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCompany.Size = New System.Drawing.Size(424, 262)
        Me.grpCompany.TabIndex = 302
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
        Me.gvDB.Size = New System.Drawing.Size(404, 206)
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
        Me.Panel1.Size = New System.Drawing.Size(404, 26)
        Me.Panel1.TabIndex = 0
        '
        'rbtnSelectCompany
        '
        Me.rbtnSelectCompany.Location = New System.Drawing.Point(192, 5)
        Me.rbtnSelectCompany.Name = "rbtnSelectCompany"
        Me.rbtnSelectCompany.Size = New System.Drawing.Size(50, 18)
        Me.rbtnSelectCompany.TabIndex = 1
        Me.rbtnSelectCompany.Text = "Select"
        '
        'rbtnAllCompany
        '
        Me.rbtnAllCompany.Location = New System.Drawing.Point(149, 5)
        Me.rbtnAllCompany.Name = "rbtnAllCompany"
        Me.rbtnAllCompany.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAllCompany.TabIndex = 0
        Me.rbtnAllCompany.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnItemWise)
        Me.RadGroupBox2.Controls.Add(Me.rbtnInvoiceWise)
        Me.RadGroupBox2.Controls.Add(Me.rbtnCustomerWise)
        Me.RadGroupBox2.HeaderText = "Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(283, 37)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Type"
        '
        'rbtnItemWise
        '
        Me.rbtnItemWise.Location = New System.Drawing.Point(13, 12)
        Me.rbtnItemWise.Name = "rbtnItemWise"
        Me.rbtnItemWise.Size = New System.Drawing.Size(70, 18)
        Me.rbtnItemWise.TabIndex = 1
        Me.rbtnItemWise.TabStop = True
        Me.rbtnItemWise.Text = "Item Wise"
        Me.rbtnItemWise.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnInvoiceWise
        '
        Me.rbtnInvoiceWise.Location = New System.Drawing.Point(88, 12)
        Me.rbtnInvoiceWise.Name = "rbtnInvoiceWise"
        Me.rbtnInvoiceWise.Size = New System.Drawing.Size(83, 18)
        Me.rbtnInvoiceWise.TabIndex = 0
        Me.rbtnInvoiceWise.Text = "Invoice Wise"
        '
        'rbtnCustomerWise
        '
        Me.rbtnCustomerWise.Location = New System.Drawing.Point(174, 12)
        Me.rbtnCustomerWise.Name = "rbtnCustomerWise"
        Me.rbtnCustomerWise.Size = New System.Drawing.Size(96, 18)
        Me.rbtnCustomerWise.TabIndex = 1
        Me.rbtnCustomerWise.Text = "Customer Wise"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(519, 15)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(375, 15)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 3
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(468, 15)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(309, 15)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(808, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(77, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(157, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 19)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(884, 602)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkIC)
        Me.RadPageViewPage1.Controls.Add(Me.chkIncludeTransfer)
        Me.RadPageViewPage1.Controls.Add(Me.grpTemplate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.grpCustomerType)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.grpLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.grpCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.grpCompany)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(863, 554)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'chkIC
        '
        Me.chkIC.Location = New System.Drawing.Point(733, 17)
        Me.chkIC.MyLinkLable1 = Nothing
        Me.chkIC.MyLinkLable2 = Nothing
        Me.chkIC.Name = "chkIC"
        Me.chkIC.Size = New System.Drawing.Size(131, 18)
        Me.chkIC.TabIndex = 310
        Me.chkIC.Tag1 = Nothing
        Me.chkIC.Text = "Include InterCompany"
        '
        'chkIncludeTransfer
        '
        Me.chkIncludeTransfer.Location = New System.Drawing.Point(608, 16)
        Me.chkIncludeTransfer.MyLinkLable1 = Nothing
        Me.chkIncludeTransfer.MyLinkLable2 = Nothing
        Me.chkIncludeTransfer.Name = "chkIncludeTransfer"
        Me.chkIncludeTransfer.Size = New System.Drawing.Size(100, 18)
        Me.chkIncludeTransfer.TabIndex = 309
        Me.chkIncludeTransfer.Tag1 = Nothing
        Me.chkIncludeTransfer.Text = "Include Transfer"
        '
        'grpTemplate
        '
        Me.grpTemplate.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpTemplate.Controls.Add(Me.cgvtemplate)
        Me.grpTemplate.Controls.Add(Me.Panel5)
        Me.grpTemplate.HeaderText = "Template"
        Me.grpTemplate.Location = New System.Drawing.Point(3, 394)
        Me.grpTemplate.Name = "grpTemplate"
        Me.grpTemplate.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpTemplate.Size = New System.Drawing.Size(424, 157)
        Me.grpTemplate.TabIndex = 53
        Me.grpTemplate.Text = "Template"
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
        Me.cgvtemplate.Size = New System.Drawing.Size(404, 107)
        Me.cgvtemplate.TabIndex = 2
        Me.cgvtemplate.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chktempselect)
        Me.Panel5.Controls.Add(Me.chktempall)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(404, 20)
        Me.Panel5.TabIndex = 1
        '
        'chktempselect
        '
        Me.chktempselect.Location = New System.Drawing.Point(190, 1)
        Me.chktempselect.MyLinkLable1 = Nothing
        Me.chktempselect.MyLinkLable2 = Nothing
        Me.chktempselect.Name = "chktempselect"
        Me.chktempselect.Size = New System.Drawing.Size(50, 18)
        Me.chktempselect.TabIndex = 2
        Me.chktempselect.Text = "Select"
        '
        'chktempall
        '
        Me.chktempall.Location = New System.Drawing.Point(134, 2)
        Me.chktempall.MyLinkLable1 = Nothing
        Me.chktempall.MyLinkLable2 = Nothing
        Me.chktempall.Name = "chktempall"
        Me.chktempall.Size = New System.Drawing.Size(33, 18)
        Me.chktempall.TabIndex = 1
        Me.chktempall.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvReport)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(863, 554)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvReport
        '
        Me.gvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvReport.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvReport.MasterTemplate.AllowAddNewRow = False
        Me.gvReport.MasterTemplate.AllowEditRow = False
        Me.gvReport.Name = "gvReport"
        Me.gvReport.ShowGroupPanel = False
        Me.gvReport.Size = New System.Drawing.Size(863, 554)
        Me.gvReport.TabIndex = 0
        Me.gvReport.Text = "RadGridView1"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.pnlAdminSetting)
        Me.RadPanel1.Controls.Add(Me.btnExportToExcel)
        Me.RadPanel1.Controls.Add(Me.btnRefresh)
        Me.RadPanel1.Controls.Add(Me.btnPrint)
        Me.RadPanel1.Controls.Add(Me.btnClose)
        Me.RadPanel1.Controls.Add(Me.btnReset)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 625)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(885, 27)
        Me.RadPanel1.TabIndex = 2
        '
        'pnlAdminSetting
        '
        Me.pnlAdminSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlAdminSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminSetting.Controls.Add(Me.pnlSaleRegister)
        Me.pnlAdminSetting.Controls.Add(Me.chkMismatch)
        Me.pnlAdminSetting.Controls.Add(Me.chkReconcile)
        Me.pnlAdminSetting.Location = New System.Drawing.Point(274, 4)
        Me.pnlAdminSetting.Name = "pnlAdminSetting"
        Me.pnlAdminSetting.Size = New System.Drawing.Size(269, 21)
        Me.pnlAdminSetting.TabIndex = 331
        Me.pnlAdminSetting.Visible = False
        '
        'pnlSaleRegister
        '
        Me.pnlSaleRegister.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSaleRegister.Controls.Add(Me.rbtnVAT)
        Me.pnlSaleRegister.Controls.Add(Me.rbtnCST)
        Me.pnlSaleRegister.Location = New System.Drawing.Point(162, 0)
        Me.pnlSaleRegister.Name = "pnlSaleRegister"
        Me.pnlSaleRegister.Size = New System.Drawing.Size(103, 19)
        Me.pnlSaleRegister.TabIndex = 313
        '
        'rbtnVAT
        '
        Me.rbtnVAT.Location = New System.Drawing.Point(6, 0)
        Me.rbtnVAT.Name = "rbtnVAT"
        Me.rbtnVAT.Size = New System.Drawing.Size(40, 18)
        Me.rbtnVAT.TabIndex = 0
        Me.rbtnVAT.TabStop = True
        Me.rbtnVAT.Text = "VAT"
        Me.rbtnVAT.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnCST
        '
        Me.rbtnCST.Location = New System.Drawing.Point(55, 0)
        Me.rbtnCST.Name = "rbtnCST"
        Me.rbtnCST.Size = New System.Drawing.Size(39, 18)
        Me.rbtnCST.TabIndex = 1
        Me.rbtnCST.Text = "CST"
        '
        'chkMismatch
        '
        Me.chkMismatch.Location = New System.Drawing.Point(74, 0)
        Me.chkMismatch.Name = "chkMismatch"
        Me.chkMismatch.Size = New System.Drawing.Size(81, 18)
        Me.chkMismatch.TabIndex = 19
        Me.chkMismatch.Text = "Mismatched"
        Me.chkMismatch.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkReconcile
        '
        Me.chkReconcile.Location = New System.Drawing.Point(4, 0)
        Me.chkReconcile.Name = "chkReconcile"
        Me.chkReconcile.Size = New System.Drawing.Size(68, 18)
        Me.chkReconcile.TabIndex = 18
        Me.chkReconcile.Text = "Reconcile"
        '
        'btnExportToExcel
        '
        Me.btnExportToExcel.Location = New System.Drawing.Point(549, 6)
        Me.btnExportToExcel.Name = "btnExportToExcel"
        Me.btnExportToExcel.Size = New System.Drawing.Size(118, 18)
        Me.btnExportToExcel.TabIndex = 9
        Me.btnExportToExcel.Text = "Export To Excel"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(4, 6)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(885, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'FrmRptSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(885, 652)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Name = "FrmRptSales"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Register"
        CType(Me.grpCustomerType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomerType.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLocation.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomer.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCompany.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnInvoiceWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCustomerWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkIC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTemplate.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.pnlAdminSetting.ResumeLayout(False)
        Me.pnlAdminSetting.PerformLayout()
        Me.pnlSaleRegister.ResumeLayout(False)
        Me.pnlSaleRegister.PerformLayout()
        CType(Me.rbtnVAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportToExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnInvoiceWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnCustomerWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents grpCompany As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnSelectCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnAllCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents grpCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents grpLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents grpCustomerType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgcategory As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkcategoryselect As common.Controls.MyRadioButton
    Friend WithEvents chkcategoryall As common.Controls.MyRadioButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportToExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents grpTemplate As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvtemplate As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chktempselect As common.Controls.MyRadioButton
    Friend WithEvents chktempall As common.Controls.MyRadioButton
    Friend WithEvents gvReport As common.UserControls.MyRadGridView
    Friend WithEvents chkIncludeTransfer As common.Controls.MyCheckBox
    Friend WithEvents rbtnItemWise As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents pnlSaleRegister As System.Windows.Forms.Panel
    Friend WithEvents rbtnVAT As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnCST As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents pnlAdminSetting As System.Windows.Forms.Panel
    Friend WithEvents chkMismatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkReconcile As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIC As common.Controls.MyCheckBox
End Class

