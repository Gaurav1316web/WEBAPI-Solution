<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptDataEntryTracingReport
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkMarketing = New Telerik.WinControls.UI.RadCheckBox()
        Me.ddlCustomerCategory = New Telerik.WinControls.UI.RadDropDownList()
        Me.SalesType = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtn_LMS = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtn_Marketing = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnPosted = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnUnposted = New Telerik.WinControls.UI.RadRadioButton()
        Me.ddlReportType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblType = New common.Controls.MyLabel()
        Me.TxtMultiCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkMarketing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCustomerCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SalesType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SalesType.SuspendLayout()
        CType(Me.rbtn_LMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtn_Marketing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.btnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnposted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(815, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.AccessibleDescription = "Save Layout"
        Me.rmsaveLayout.AccessibleName = "Save Layout"
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(815, 376)
        Me.SplitContainer1.SplitterDistance = 326
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(815, 326)
        Me.RadPageView1.TabIndex = 11
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkMarketing)
        Me.RadPageViewPage1.Controls.Add(Me.ddlCustomerCategory)
        Me.RadPageViewPage1.Controls.Add(Me.SalesType)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.Controls.Add(Me.ddlReportType)
        Me.RadPageViewPage1.Controls.Add(Me.lblType)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiCustomerCategory)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(794, 278)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'chkMarketing
        '
        Me.chkMarketing.Location = New System.Drawing.Point(441, 47)
        Me.chkMarketing.Name = "chkMarketing"
        Me.chkMarketing.Size = New System.Drawing.Size(71, 18)
        Me.chkMarketing.TabIndex = 427
        Me.chkMarketing.Text = "Marketing"
        '
        'ddlCustomerCategory
        '
        Me.ddlCustomerCategory.AutoCompleteDisplayMember = Nothing
        Me.ddlCustomerCategory.AutoCompleteValueMember = Nothing
        Me.ddlCustomerCategory.Location = New System.Drawing.Point(124, 94)
        Me.ddlCustomerCategory.Name = "ddlCustomerCategory"
        Me.ddlCustomerCategory.Size = New System.Drawing.Size(171, 20)
        Me.ddlCustomerCategory.TabIndex = 426
        '
        'SalesType
        '
        Me.SalesType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.SalesType.Controls.Add(Me.rbtn_LMS)
        Me.SalesType.Controls.Add(Me.rbtn_Marketing)
        Me.SalesType.HeaderText = "Sales Type"
        Me.SalesType.Location = New System.Drawing.Point(536, 4)
        Me.SalesType.Name = "SalesType"
        Me.SalesType.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.SalesType.Size = New System.Drawing.Size(145, 37)
        Me.SalesType.TabIndex = 425
        Me.SalesType.Text = "Sales Type"
        Me.SalesType.Visible = False
        '
        'rbtn_LMS
        '
        Me.rbtn_LMS.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtn_LMS.Location = New System.Drawing.Point(8, 10)
        Me.rbtn_LMS.Name = "rbtn_LMS"
        Me.rbtn_LMS.Size = New System.Drawing.Size(42, 18)
        Me.rbtn_LMS.TabIndex = 306
        Me.rbtn_LMS.Text = "LMS"
        Me.rbtn_LMS.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtn_Marketing
        '
        Me.rbtn_Marketing.Location = New System.Drawing.Point(58, 10)
        Me.rbtn_Marketing.Name = "rbtn_Marketing"
        Me.rbtn_Marketing.Size = New System.Drawing.Size(71, 18)
        Me.rbtn_Marketing.TabIndex = 307
        Me.rbtn_Marketing.TabStop = False
        Me.rbtn_Marketing.Text = "Marketing"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(15, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(272, 41)
        Me.RadGroupBox3.TabIndex = 424
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(130, 16)
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
        Me.ToDate.Location = New System.Drawing.Point(157, 15)
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
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.btnAll)
        Me.RadGroupBox6.Controls.Add(Me.btnPosted)
        Me.RadGroupBox6.Controls.Add(Me.btnUnposted)
        Me.RadGroupBox6.HeaderText = "Status"
        Me.RadGroupBox6.Location = New System.Drawing.Point(293, 4)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(228, 37)
        Me.RadGroupBox6.TabIndex = 423
        Me.RadGroupBox6.Text = "Status"
        '
        'btnAll
        '
        Me.btnAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.btnAll.Location = New System.Drawing.Point(13, 11)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(33, 18)
        Me.btnAll.TabIndex = 308
        Me.btnAll.Text = "All"
        Me.btnAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'btnPosted
        '
        Me.btnPosted.Location = New System.Drawing.Point(73, 10)
        Me.btnPosted.Name = "btnPosted"
        Me.btnPosted.Size = New System.Drawing.Size(54, 18)
        Me.btnPosted.TabIndex = 306
        Me.btnPosted.TabStop = False
        Me.btnPosted.Text = "Posted"
        '
        'btnUnposted
        '
        Me.btnUnposted.Location = New System.Drawing.Point(148, 10)
        Me.btnUnposted.Name = "btnUnposted"
        Me.btnUnposted.Size = New System.Drawing.Size(69, 18)
        Me.btnUnposted.TabIndex = 307
        Me.btnUnposted.TabStop = False
        Me.btnUnposted.Text = "Unposted"
        '
        'ddlReportType
        '
        Me.ddlReportType.AutoCompleteDisplayMember = Nothing
        Me.ddlReportType.AutoCompleteValueMember = Nothing
        Me.ddlReportType.Location = New System.Drawing.Point(124, 45)
        Me.ddlReportType.Name = "ddlReportType"
        Me.ddlReportType.Size = New System.Drawing.Size(171, 20)
        Me.ddlReportType.TabIndex = 422
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Location = New System.Drawing.Point(15, 47)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(67, 18)
        Me.lblType.TabIndex = 421
        Me.lblType.Text = "Report Type"
        '
        'TxtMultiCustomerCategory
        '
        Me.TxtMultiCustomerCategory.arrDispalyMember = Nothing
        Me.TxtMultiCustomerCategory.arrValueMember = Nothing
        Me.TxtMultiCustomerCategory.Location = New System.Drawing.Point(124, 154)
        Me.TxtMultiCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCustomerCategory.MyLinkLable1 = Nothing
        Me.TxtMultiCustomerCategory.MyLinkLable2 = Nothing
        Me.TxtMultiCustomerCategory.MyNullText = "All"
        Me.TxtMultiCustomerCategory.Name = "TxtMultiCustomerCategory"
        Me.TxtMultiCustomerCategory.Size = New System.Drawing.Size(331, 19)
        Me.TxtMultiCustomerCategory.TabIndex = 420
        Me.TxtMultiCustomerCategory.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(15, 94)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel6.TabIndex = 419
        Me.MyLabel6.Text = "Customer Category"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(124, 69)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(332, 19)
        Me.txtLocation.TabIndex = 412
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(15, 70)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel4.TabIndex = 411
        Me.MyLabel4.Text = "Location"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(671, 278)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Gv1.ForeColor = System.Drawing.Color.Black
        Me.Gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(671, 278)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(165, 15)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(83, 22)
        Me.btnExp.TabIndex = 157
        Me.btnExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(722, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(14, 15)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(88, 15)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'rptDataEntryTracingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 396)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptDataEntryTracingReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Payment Summary"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkMarketing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCustomerCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SalesType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SalesType.ResumeLayout(False)
        Me.SalesType.PerformLayout()
        CType(Me.rbtn_LMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtn_Marketing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.btnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnposted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents TxtMultiCustomerCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents ddlReportType As RadDropDownList
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents btnAll As RadRadioButton
    Friend WithEvents btnPosted As RadRadioButton
    Friend WithEvents btnUnposted As RadRadioButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents SalesType As RadGroupBox
    Friend WithEvents rbtn_LMS As RadRadioButton
    Friend WithEvents rbtn_Marketing As RadRadioButton
    Friend WithEvents ddlCustomerCategory As RadDropDownList
    Friend WithEvents chkMarketing As RadCheckBox
End Class

