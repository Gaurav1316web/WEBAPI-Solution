Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptNoSales
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvtemplate = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chktempselect = New common.Controls.MyRadioButton
        Me.chktempall = New common.Controls.MyRadioButton
        Me.ddlVisiType = New common.Controls.MyComboBox
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.Customer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgcategory = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkcategoryselect = New common.Controls.MyRadioButton
        Me.chkcategoryall = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgRoute = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkRouteSelect = New common.Controls.MyRadioButton
        Me.chkRouteAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.rbtnDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlVisiType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Customer.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.ddlVisiType)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox1.Controls.Add(Me.Customer)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = "  "
        Me.RadGroupBox1.Location = New System.Drawing.Point(18, 19)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(826, 451)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "  "
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cgvtemplate)
        Me.RadGroupBox5.Controls.Add(Me.Panel5)
        Me.RadGroupBox5.HeaderText = "Template"
        Me.RadGroupBox5.Location = New System.Drawing.Point(417, 265)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(404, 167)
        Me.RadGroupBox5.TabIndex = 308
        Me.RadGroupBox5.Text = "Template"
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
        Me.cgvtemplate.Size = New System.Drawing.Size(384, 117)
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
        Me.Panel5.Size = New System.Drawing.Size(384, 20)
        Me.Panel5.TabIndex = 1
        '
        'chktempselect
        '
        Me.chktempselect.Location = New System.Drawing.Point(203, 1)
        Me.chktempselect.MyLinkLable1 = Nothing
        Me.chktempselect.MyLinkLable2 = Nothing
        Me.chktempselect.Name = "chktempselect"
        Me.chktempselect.Size = New System.Drawing.Size(50, 18)
        Me.chktempselect.TabIndex = 2
        Me.chktempselect.Text = "Select"
        '
        'chktempall
        '
        Me.chktempall.Location = New System.Drawing.Point(148, 2)
        Me.chktempall.MyLinkLable1 = Nothing
        Me.chktempall.MyLinkLable2 = Nothing
        Me.chktempall.Name = "chktempall"
        Me.chktempall.Size = New System.Drawing.Size(33, 18)
        Me.chktempall.TabIndex = 1
        Me.chktempall.Text = "All"
        '
        'ddlVisiType
        '
        Me.ddlVisiType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Both"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "With Visi"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Without Visi"
        RadListDataItem3.TextWrap = True
        Me.ddlVisiType.Items.Add(RadListDataItem1)
        Me.ddlVisiType.Items.Add(RadListDataItem2)
        Me.ddlVisiType.Items.Add(RadListDataItem3)
        Me.ddlVisiType.Location = New System.Drawing.Point(606, 18)
        Me.ddlVisiType.MendatroryField = False
        Me.ddlVisiType.MyLinkLable1 = Nothing
        Me.ddlVisiType.MyLinkLable2 = Nothing
        Me.ddlVisiType.Name = "ddlVisiType"
        Me.ddlVisiType.Size = New System.Drawing.Size(116, 20)
        Me.ddlVisiType.TabIndex = 306
        '
        'RadLabel8
        '
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(537, 21)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(56, 16)
        Me.RadLabel8.TabIndex = 307
        Me.RadLabel8.Text = "Visi Type "
        '
        'Customer
        '
        Me.Customer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Customer.Controls.Add(Me.cbgcategory)
        Me.Customer.Controls.Add(Me.Panel4)
        Me.Customer.HeaderText = "Customer Type"
        Me.Customer.Location = New System.Drawing.Point(7, 72)
        Me.Customer.Name = "Customer"
        Me.Customer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Customer.Size = New System.Drawing.Size(404, 179)
        Me.Customer.TabIndex = 305
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
        Me.cbgcategory.Size = New System.Drawing.Size(384, 129)
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
        Me.Panel4.Size = New System.Drawing.Size(384, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkcategoryselect
        '
        Me.chkcategoryselect.Location = New System.Drawing.Point(210, 1)
        Me.chkcategoryselect.MyLinkLable1 = Nothing
        Me.chkcategoryselect.MyLinkLable2 = Nothing
        Me.chkcategoryselect.Name = "chkcategoryselect"
        Me.chkcategoryselect.Size = New System.Drawing.Size(50, 18)
        Me.chkcategoryselect.TabIndex = 1
        Me.chkcategoryselect.Text = "Select"
        '
        'chkcategoryall
        '
        Me.chkcategoryall.Location = New System.Drawing.Point(156, 1)
        Me.chkcategoryall.MyLinkLable1 = Nothing
        Me.chkcategoryall.MyLinkLable2 = Nothing
        Me.chkcategoryall.Name = "chkcategoryall"
        Me.chkcategoryall.Size = New System.Drawing.Size(33, 18)
        Me.chkcategoryall.TabIndex = 0
        Me.chkcategoryall.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgRoute)
        Me.RadGroupBox4.Controls.Add(Me.Panel1)
        Me.RadGroupBox4.HeaderText = "Route"
        Me.RadGroupBox4.Location = New System.Drawing.Point(7, 257)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(404, 175)
        Me.RadGroupBox4.TabIndex = 304
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
        Me.cbgRoute.Size = New System.Drawing.Size(384, 125)
        Me.cbgRoute.TabIndex = 1
        Me.cbgRoute.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkRouteSelect)
        Me.Panel1.Controls.Add(Me.chkRouteAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(384, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkRouteSelect
        '
        Me.chkRouteSelect.Location = New System.Drawing.Point(202, 1)
        Me.chkRouteSelect.MyLinkLable1 = Nothing
        Me.chkRouteSelect.MyLinkLable2 = Nothing
        Me.chkRouteSelect.Name = "chkRouteSelect"
        Me.chkRouteSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkRouteSelect.TabIndex = 1
        Me.chkRouteSelect.Text = "Select"
        '
        'chkRouteAll
        '
        Me.chkRouteAll.Location = New System.Drawing.Point(151, 1)
        Me.chkRouteAll.MyLinkLable1 = Nothing
        Me.chkRouteAll.MyLinkLable2 = Nothing
        Me.chkRouteAll.Name = "chkRouteAll"
        Me.chkRouteAll.Size = New System.Drawing.Size(33, 18)
        Me.chkRouteAll.TabIndex = 0
        Me.chkRouteAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Customer"
        Me.RadGroupBox3.Location = New System.Drawing.Point(417, 72)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(404, 179)
        Me.RadGroupBox3.TabIndex = 303
        Me.RadGroupBox3.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(384, 129)
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
        Me.Panel2.Size = New System.Drawing.Size(384, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(205, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(154, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnDetail)
        Me.RadGroupBox2.Controls.Add(Me.rbtnSummary)
        Me.RadGroupBox2.HeaderText = "Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 9)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(163, 43)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "Type"
        '
        'rbtnDetail
        '
        Me.rbtnDetail.Location = New System.Drawing.Point(13, 12)
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 0
        Me.rbtnDetail.TabStop = True
        Me.rbtnDetail.Text = "Detail"
        Me.rbtnDetail.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnSummary
        '
        Me.rbtnSummary.Location = New System.Drawing.Point(86, 12)
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 1
        Me.rbtnSummary.Text = "Summary"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(380, 21)
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
        Me.txtFromDate.Location = New System.Drawing.Point(241, 21)
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
        Me.RadLabel2.Location = New System.Drawing.Point(329, 21)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(176, 21)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(776, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(25, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(99, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 5
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
        Me.SplitContainer1.Size = New System.Drawing.Size(860, 516)
        Me.SplitContainer1.SplitterDistance = 477
        Me.SplitContainer1.TabIndex = 1
        '
        'frmRptNoSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(860, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmRptNoSales"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "No Sales Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlVisiType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Customer.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkcategoryselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcategoryall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkRouteSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rbtnDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgRoute As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkRouteSelect As common.Controls.MyRadioButton
    Friend WithEvents chkRouteAll As common.Controls.MyRadioButton
    Friend WithEvents Customer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgcategory As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkcategoryselect As common.Controls.MyRadioButton
    Friend WithEvents chkcategoryall As common.Controls.MyRadioButton
    Friend WithEvents ddlVisiType As common.Controls.MyComboBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvtemplate As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chktempselect As common.Controls.MyRadioButton
    Friend WithEvents chktempall As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

