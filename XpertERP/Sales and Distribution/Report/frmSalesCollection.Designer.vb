<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesCollection
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cboCustomerClass = New common.Controls.MyComboBox
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocatioAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgRoute = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkRouteSelect = New common.Controls.MyRadioButton
        Me.chkRouteAll = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgInvoiceNo = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkInvoiceSelect = New common.Controls.MyRadioButton
        Me.chkInvoiceAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgSalesPerson = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chlSalesSelect = New common.Controls.MyRadioButton
        Me.ChkSalesAll = New common.Controls.MyRadioButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCompany = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnCompanySelect = New common.Controls.MyRadioButton
        Me.rbtnCompanyAll = New common.Controls.MyRadioButton
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.cboCustomerClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocatioAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkInvoiceSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInvoiceAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chlSalesSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSalesAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cboCustomerClass)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(365, 23)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(346, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Select Date"
        '
        'cboCustomerClass
        '
        Me.cboCustomerClass.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCustomerClass.Location = New System.Drawing.Point(351, 12)
        Me.cboCustomerClass.MendatroryField = False
        Me.cboCustomerClass.MyLinkLable1 = Nothing
        Me.cboCustomerClass.MyLinkLable2 = Nothing
        Me.cboCustomerClass.Name = "cboCustomerClass"
        Me.cboCustomerClass.Size = New System.Drawing.Size(11, 20)
        Me.cboCustomerClass.TabIndex = 84
        Me.cboCustomerClass.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(141, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
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
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel2)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(13, 242)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(346, 159)
        Me.RadGroupBox5.TabIndex = 109
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
        Me.cbgLocation.Size = New System.Drawing.Size(326, 109)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocatioAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(326, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(178, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocatioAll
        '
        Me.chkLocatioAll.Location = New System.Drawing.Point(127, 1)
        Me.chkLocatioAll.MyLinkLable1 = Nothing
        Me.chkLocatioAll.MyLinkLable2 = Nothing
        Me.chkLocatioAll.Name = "chkLocatioAll"
        Me.chkLocatioAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocatioAll.TabIndex = 0
        Me.chkLocatioAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgRoute)
        Me.RadGroupBox4.Controls.Add(Me.Panel4)
        Me.RadGroupBox4.HeaderText = "Route"
        Me.RadGroupBox4.Location = New System.Drawing.Point(365, 253)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(346, 159)
        Me.RadGroupBox4.TabIndex = 110
        Me.RadGroupBox4.Text = "Route"
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
        Me.cbgRoute.Size = New System.Drawing.Size(326, 109)
        Me.cbgRoute.TabIndex = 1
        Me.cbgRoute.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkRouteSelect)
        Me.Panel4.Controls.Add(Me.chkRouteAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(326, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkRouteSelect
        '
        Me.chkRouteSelect.Location = New System.Drawing.Point(178, 1)
        Me.chkRouteSelect.MyLinkLable1 = Nothing
        Me.chkRouteSelect.MyLinkLable2 = Nothing
        Me.chkRouteSelect.Name = "chkRouteSelect"
        Me.chkRouteSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkRouteSelect.TabIndex = 1
        Me.chkRouteSelect.Text = "Select"
        '
        'chkRouteAll
        '
        Me.chkRouteAll.Location = New System.Drawing.Point(127, 1)
        Me.chkRouteAll.MyLinkLable1 = Nothing
        Me.chkRouteAll.MyLinkLable2 = Nothing
        Me.chkRouteAll.Name = "chkRouteAll"
        Me.chkRouteAll.Size = New System.Drawing.Size(33, 18)
        Me.chkRouteAll.TabIndex = 0
        Me.chkRouteAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgInvoiceNo)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Invoice No"
        Me.RadGroupBox1.Location = New System.Drawing.Point(365, 77)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(346, 159)
        Me.RadGroupBox1.TabIndex = 110
        Me.RadGroupBox1.Text = "Invoice No"
        '
        'cbgInvoiceNo
        '
        Me.cbgInvoiceNo.CheckedValue = Nothing
        Me.cbgInvoiceNo.DataSource = Nothing
        Me.cbgInvoiceNo.DisplayMember = "Name"
        Me.cbgInvoiceNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgInvoiceNo.Location = New System.Drawing.Point(10, 40)
        Me.cbgInvoiceNo.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgInvoiceNo.MyShowHeadrText = False
        Me.cbgInvoiceNo.Name = "cbgInvoiceNo"
        Me.cbgInvoiceNo.Size = New System.Drawing.Size(326, 109)
        Me.cbgInvoiceNo.TabIndex = 1
        Me.cbgInvoiceNo.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkInvoiceSelect)
        Me.Panel1.Controls.Add(Me.chkInvoiceAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(326, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkInvoiceSelect
        '
        Me.chkInvoiceSelect.Location = New System.Drawing.Point(178, 1)
        Me.chkInvoiceSelect.MyLinkLable1 = Nothing
        Me.chkInvoiceSelect.MyLinkLable2 = Nothing
        Me.chkInvoiceSelect.Name = "chkInvoiceSelect"
        Me.chkInvoiceSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkInvoiceSelect.TabIndex = 1
        Me.chkInvoiceSelect.Text = "Select"
        '
        'chkInvoiceAll
        '
        Me.chkInvoiceAll.Location = New System.Drawing.Point(127, 1)
        Me.chkInvoiceAll.MyLinkLable1 = Nothing
        Me.chkInvoiceAll.MyLinkLable2 = Nothing
        Me.chkInvoiceAll.Name = "chkInvoiceAll"
        Me.chkInvoiceAll.Size = New System.Drawing.Size(33, 18)
        Me.chkInvoiceAll.TabIndex = 0
        Me.chkInvoiceAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgSalesPerson)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "SalesPerson"
        Me.RadGroupBox2.Location = New System.Drawing.Point(11, 77)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(346, 159)
        Me.RadGroupBox2.TabIndex = 110
        Me.RadGroupBox2.Text = "SalesPerson"
        '
        'cbgSalesPerson
        '
        Me.cbgSalesPerson.CheckedValue = Nothing
        Me.cbgSalesPerson.DataSource = Nothing
        Me.cbgSalesPerson.DisplayMember = "Name"
        Me.cbgSalesPerson.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSalesPerson.Location = New System.Drawing.Point(10, 40)
        Me.cbgSalesPerson.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSalesPerson.MyShowHeadrText = False
        Me.cbgSalesPerson.Name = "cbgSalesPerson"
        Me.cbgSalesPerson.Size = New System.Drawing.Size(326, 109)
        Me.cbgSalesPerson.TabIndex = 1
        Me.cbgSalesPerson.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chlSalesSelect)
        Me.Panel3.Controls.Add(Me.ChkSalesAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(326, 20)
        Me.Panel3.TabIndex = 0
        '
        'chlSalesSelect
        '
        Me.chlSalesSelect.Location = New System.Drawing.Point(178, 1)
        Me.chlSalesSelect.MyLinkLable1 = Nothing
        Me.chlSalesSelect.MyLinkLable2 = Nothing
        Me.chlSalesSelect.Name = "chlSalesSelect"
        Me.chlSalesSelect.Size = New System.Drawing.Size(50, 18)
        Me.chlSalesSelect.TabIndex = 1
        Me.chlSalesSelect.Text = "Select"
        '
        'ChkSalesAll
        '
        Me.ChkSalesAll.Location = New System.Drawing.Point(127, 1)
        Me.ChkSalesAll.MyLinkLable1 = Nothing
        Me.ChkSalesAll.MyLinkLable2 = Nothing
        Me.ChkSalesAll.Name = "ChkSalesAll"
        Me.ChkSalesAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkSalesAll.TabIndex = 0
        Me.ChkSalesAll.Text = "All"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(13, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 113
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(672, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 112
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(85, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 111
        Me.btnPrint.Text = "Print"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.rdbDetail)
        Me.RadGroupBox7.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox7.HeaderText = "Select"
        Me.RadGroupBox7.Location = New System.Drawing.Point(13, 21)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(346, 42)
        Me.RadGroupBox7.TabIndex = 109
        Me.RadGroupBox7.Text = "Select"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(151, 14)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 105
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(9, 14)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 105
        Me.rdbSummary.Text = "Summary"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgCompany)
        Me.RadGroupBox6.Controls.Add(Me.Panel7)
        Me.RadGroupBox6.HeaderText = "Company"
        Me.RadGroupBox6.Location = New System.Drawing.Point(11, 407)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(348, 141)
        Me.RadGroupBox6.TabIndex = 114
        Me.RadGroupBox6.Text = "Company"
        '
        'cbgCompany
        '
        Me.cbgCompany.CheckedValue = Nothing
        Me.cbgCompany.DataSource = Nothing
        Me.cbgCompany.DisplayMember = "Name"
        Me.cbgCompany.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCompany.Location = New System.Drawing.Point(10, 40)
        Me.cbgCompany.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCompany.MyShowHeadrText = False
        Me.cbgCompany.Name = "cbgCompany"
        Me.cbgCompany.Size = New System.Drawing.Size(328, 91)
        Me.cbgCompany.TabIndex = 1
        Me.cbgCompany.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbtnCompanySelect)
        Me.Panel7.Controls.Add(Me.rbtnCompanyAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(328, 20)
        Me.Panel7.TabIndex = 0
        '
        'rbtnCompanySelect
        '
        Me.rbtnCompanySelect.Location = New System.Drawing.Point(163, 1)
        Me.rbtnCompanySelect.MyLinkLable1 = Nothing
        Me.rbtnCompanySelect.MyLinkLable2 = Nothing
        Me.rbtnCompanySelect.Name = "rbtnCompanySelect"
        Me.rbtnCompanySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCompanySelect.TabIndex = 1
        Me.rbtnCompanySelect.Text = "Select"
        '
        'rbtnCompanyAll
        '
        Me.rbtnCompanyAll.Location = New System.Drawing.Point(109, 1)
        Me.rbtnCompanyAll.MyLinkLable1 = Nothing
        Me.rbtnCompanyAll.MyLinkLable2 = Nothing
        Me.rbtnCompanyAll.Name = "rbtnCompanyAll"
        Me.rbtnCompanyAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCompanyAll.TabIndex = 0
        Me.rbtnCompanyAll.Text = "All"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox7)
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox8.HeaderText = ""
        Me.RadGroupBox8.Location = New System.Drawing.Point(13, 13)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(727, 567)
        Me.RadGroupBox8.TabIndex = 115
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox8)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(754, 616)
        Me.SplitContainer1.SplitterDistance = 587
        Me.SplitContainer1.TabIndex = 116
        '
        'FrmSalesCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 616)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSalesCollection"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Collection"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.cboCustomerClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocatioAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkInvoiceSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInvoiceAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chlSalesSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSalesAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cboCustomerClass As common.Controls.MyComboBox
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocatioAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkRouteSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRouteAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgInvoiceNo As common.MyCheckBoxGrid
    Friend WithEvents chkInvoiceSelect As common.Controls.MyRadioButton
    Friend WithEvents chkInvoiceAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSalesPerson As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chlSalesSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkSalesAll As common.Controls.MyRadioButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCompany As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCompanySelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCompanyAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

