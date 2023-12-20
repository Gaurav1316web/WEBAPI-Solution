<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCrateJaliReport
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.ddlReportType = New common.Controls.MyComboBox()
        Me.lbltype = New common.Controls.MyLabel()
        Me.pnlActiveInActiveCustomer = New System.Windows.Forms.Panel()
        Me.chkActive = New System.Windows.Forms.RadioButton()
        Me.chkAll = New System.Windows.Forms.RadioButton()
        Me.chkInactive = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkCan = New common.Controls.MyCheckBox()
        Me.chkCrate = New common.Controls.MyCheckBox()
        Me.chkcustomerWithDateWise = New common.Controls.MyCheckBox()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.chkCustomerWise = New common.Controls.MyCheckBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtVehicle = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPDFWithFormat = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExcelGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDFGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlActiveInActiveCustomer.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkCan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcustomerWithDateWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPDFWithFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPDFWithFormat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(819, 468)
        Me.SplitContainer1.SplitterDistance = 425
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(819, 405)
        Me.RadPageView1.TabIndex = 10
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.ddlReportType)
        Me.RadPageViewPage1.Controls.Add(Me.lbltype)
        Me.RadPageViewPage1.Controls.Add(Me.pnlActiveInActiveCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.chkcustomerWithDateWise)
        Me.RadPageViewPage1.Controls.Add(Me.txtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.chkCustomerWise)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicle)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(798, 357)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'ddlReportType
        '
        Me.ddlReportType.AutoCompleteDisplayMember = Nothing
        Me.ddlReportType.AutoCompleteValueMember = Nothing
        Me.ddlReportType.CalculationExpression = Nothing
        Me.ddlReportType.DropDownAnimationEnabled = True
        Me.ddlReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlReportType.FieldCode = Nothing
        Me.ddlReportType.FieldDesc = Nothing
        Me.ddlReportType.FieldMaxLength = 0
        Me.ddlReportType.FieldName = Nothing
        Me.ddlReportType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlReportType.isCalculatedField = False
        Me.ddlReportType.IsSourceFromTable = False
        Me.ddlReportType.IsSourceFromValueList = False
        Me.ddlReportType.IsUnique = False
        Me.ddlReportType.Location = New System.Drawing.Point(95, 4)
        Me.ddlReportType.MendatroryField = False
        Me.ddlReportType.MyLinkLable1 = Nothing
        Me.ddlReportType.MyLinkLable2 = Nothing
        Me.ddlReportType.Name = "ddlReportType"
        Me.ddlReportType.ReferenceFieldDesc = Nothing
        Me.ddlReportType.ReferenceFieldName = Nothing
        Me.ddlReportType.ReferenceTableName = Nothing
        Me.ddlReportType.Size = New System.Drawing.Size(180, 18)
        Me.ddlReportType.TabIndex = 418
        '
        'lbltype
        '
        Me.lbltype.FieldName = Nothing
        Me.lbltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltype.Location = New System.Drawing.Point(16, 3)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(69, 16)
        Me.lbltype.TabIndex = 417
        Me.lbltype.Text = "Report Type"
        '
        'pnlActiveInActiveCustomer
        '
        Me.pnlActiveInActiveCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkActive)
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkAll)
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkInactive)
        Me.pnlActiveInActiveCustomer.Location = New System.Drawing.Point(457, 71)
        Me.pnlActiveInActiveCustomer.Name = "pnlActiveInActiveCustomer"
        Me.pnlActiveInActiveCustomer.Size = New System.Drawing.Size(121, 67)
        Me.pnlActiveInActiveCustomer.TabIndex = 416
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.Location = New System.Drawing.Point(3, 23)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(107, 17)
        Me.chkActive.TabIndex = 27
        Me.chkActive.TabStop = True
        Me.chkActive.Text = "Active Customer"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(3, 3)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(95, 17)
        Me.chkAll.TabIndex = 26
        Me.chkAll.Text = "All Customers"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkInactive
        '
        Me.chkInactive.AutoSize = True
        Me.chkInactive.Location = New System.Drawing.Point(3, 43)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(116, 17)
        Me.chkInactive.TabIndex = 25
        Me.chkInactive.Text = "Inactive Customer"
        Me.chkInactive.UseVisualStyleBackColor = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkCan)
        Me.RadGroupBox1.Controls.Add(Me.chkCrate)
        Me.RadGroupBox1.HeaderText = "Show Value"
        Me.RadGroupBox1.Location = New System.Drawing.Point(580, 86)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(128, 52)
        Me.RadGroupBox1.TabIndex = 415
        Me.RadGroupBox1.Text = "Show Value"
        '
        'chkCan
        '
        Me.chkCan.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCan.Location = New System.Drawing.Point(68, 24)
        Me.chkCan.MyLinkLable1 = Nothing
        Me.chkCan.MyLinkLable2 = Nothing
        Me.chkCan.Name = "chkCan"
        Me.chkCan.Size = New System.Drawing.Size(39, 18)
        Me.chkCan.TabIndex = 390
        Me.chkCan.Tag1 = Nothing
        Me.chkCan.Text = "Can"
        Me.chkCan.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkCrate
        '
        Me.chkCrate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCrate.Location = New System.Drawing.Point(9, 24)
        Me.chkCrate.MyLinkLable1 = Nothing
        Me.chkCrate.MyLinkLable2 = Nothing
        Me.chkCrate.Name = "chkCrate"
        Me.chkCrate.Size = New System.Drawing.Size(47, 18)
        Me.chkCrate.TabIndex = 389
        Me.chkCrate.Tag1 = Nothing
        Me.chkCrate.Text = "Crate"
        Me.chkCrate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkcustomerWithDateWise
        '
        Me.chkcustomerWithDateWise.Location = New System.Drawing.Point(559, 28)
        Me.chkcustomerWithDateWise.MyLinkLable1 = Nothing
        Me.chkcustomerWithDateWise.MyLinkLable2 = Nothing
        Me.chkcustomerWithDateWise.Name = "chkcustomerWithDateWise"
        Me.chkcustomerWithDateWise.Size = New System.Drawing.Size(149, 18)
        Me.chkcustomerWithDateWise.TabIndex = 414
        Me.chkcustomerWithDateWise.Tag1 = Nothing
        Me.chkcustomerWithDateWise.Text = "Customer With Date Wise"
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(107, 120)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(344, 19)
        Me.txtRoute.TabIndex = 413
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(16, 120)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel3.TabIndex = 412
        Me.MyLabel3.Text = "Route"
        '
        'chkCustomerWise
        '
        Me.chkCustomerWise.Location = New System.Drawing.Point(457, 28)
        Me.chkCustomerWise.MyLinkLable1 = Nothing
        Me.chkCustomerWise.MyLinkLable2 = Nothing
        Me.chkCustomerWise.Name = "chkCustomerWise"
        Me.chkCustomerWise.Size = New System.Drawing.Size(96, 18)
        Me.chkCustomerWise.TabIndex = 388
        Me.chkCustomerWise.Tag1 = Nothing
        Me.chkCustomerWise.Text = "Customer Wise"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(16, 97)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel2.TabIndex = 387
        Me.MyLabel2.Text = "Customer"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(107, 96)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.MyLabel2
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomer.TabIndex = 386
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(16, 76)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel1.TabIndex = 385
        Me.MyLabel1.Text = "Vehicle"
        '
        'txtVehicle
        '
        Me.txtVehicle.arrDispalyMember = Nothing
        Me.txtVehicle.arrValueMember = Nothing
        Me.txtVehicle.Location = New System.Drawing.Point(107, 75)
        Me.txtVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.MyLinkLable1 = Me.MyLabel1
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.MyNullText = "All"
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.Size = New System.Drawing.Size(344, 19)
        Me.txtVehicle.TabIndex = 384
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 28)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(435, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(154, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(181, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(78, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(78, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(798, 357)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.EnableFiltering = True
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(798, 357)
        Me.Gv1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(819, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
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
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(372, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(71, 22)
        Me.btnPrint.TabIndex = 152
        Me.btnPrint.Text = "Print"
        '
        'btnPDFWithFormat
        '
        Me.btnPDFWithFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPDFWithFormat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPDFWithFormat.Location = New System.Drawing.Point(269, 9)
        Me.btnPDFWithFormat.Name = "btnPDFWithFormat"
        Me.btnPDFWithFormat.Size = New System.Drawing.Size(95, 22)
        Me.btnPDFWithFormat.TabIndex = 151
        Me.btnPDFWithFormat.Text = "PDF With Format"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.PDF, Me.ExcelGrid, Me.PDFGrid})
        Me.RadSplitButton1.Location = New System.Drawing.Point(166, 9)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 150
        Me.RadSplitButton1.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'PDF
        '
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'ExcelGrid
        '
        Me.ExcelGrid.AccessibleDescription = "ExcelGrid"
        Me.ExcelGrid.AccessibleName = "ExcelGrid"
        Me.ExcelGrid.Name = "ExcelGrid"
        Me.ExcelGrid.Text = "Excel(Grid)"
        Me.ExcelGrid.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'PDFGrid
        '
        Me.PDFGrid.AccessibleDescription = "PDFGrid"
        Me.PDFGrid.AccessibleName = "PDFGrid"
        Me.PDFGrid.Name = "PDFGrid"
        Me.PDFGrid.Text = "PDF(Grid)"
        Me.PDFGrid.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(727, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 148
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(8, 9)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 146
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(87, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 147
        Me.btnReset.Text = "Reset"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(256, 142)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(195, 18)
        Me.lblLocationName.TabIndex = 421
        Me.lblLocationName.TextWrap = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(16, 142)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 420
        Me.RadLabel15.Text = "Location"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(107, 142)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.RadLabel15
        Me.fndLocation.MyLinkLable2 = Me.lblLocationName
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(143, 18)
        Me.fndLocation.TabIndex = 419
        Me.fndLocation.Value = ""
        '
        'FrmCrateJaliReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 468)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCrateJaliReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Crate Jali Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlActiveInActiveCustomer.ResumeLayout(False)
        Me.pnlActiveInActiveCustomer.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkCan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcustomerWithDateWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPDFWithFormat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtVehicle As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkCustomerWise As common.Controls.MyCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkcustomerWithDateWise As common.Controls.MyCheckBox
    Friend WithEvents ExcelGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDFGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkCrate As common.Controls.MyCheckBox
    Friend WithEvents chkCan As common.Controls.MyCheckBox
    Friend WithEvents pnlActiveInActiveCustomer As Panel
    Friend WithEvents chkActive As RadioButton
    Friend WithEvents chkAll As RadioButton
    Friend WithEvents chkInactive As RadioButton
    Friend WithEvents btnPDFWithFormat As RadButton
    Friend WithEvents ddlReportType As common.Controls.MyComboBox
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
End Class

