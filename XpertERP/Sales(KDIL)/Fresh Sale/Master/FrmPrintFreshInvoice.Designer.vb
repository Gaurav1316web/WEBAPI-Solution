<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrintFreshInvoice
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtLocation1Mult = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation1 = New common.Controls.MyLabel()
        Me.txtCustomerCategoryMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerCategory = New common.Controls.MyLabel()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtCustomerMult = New common.UserControls.txtMultiSelectFinder()
        Me.gbLocation = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnQuickExport = New Telerik.WinControls.UI.RadButton()
        Me.btnUnSelect = New Telerik.WinControls.UI.RadButton()
        Me.BtnPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.TxtMultiCustomerGroup = New common.UserControls.txtMultiSelectFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbLocation.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnQuickExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnSelect)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1105, 411)
        Me.SplitContainer1.SplitterDistance = 376
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1105, 356)
        Me.RadPageView1.TabIndex = 4
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiCustomerGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerGroup)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation1Mult)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation1)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerCategoryMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerCategory)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerMult)
        Me.RadPageViewPage1.Controls.Add(Me.gbLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1084, 308)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.Location = New System.Drawing.Point(3, 108)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(89, 18)
        Me.lblCustomerGroup.TabIndex = 19
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtLocation1Mult
        '
        Me.txtLocation1Mult.arrDispalyMember = Nothing
        Me.txtLocation1Mult.arrValueMember = Nothing
        Me.txtLocation1Mult.Location = New System.Drawing.Point(112, 60)
        Me.txtLocation1Mult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation1Mult.MyLinkLable1 = Nothing
        Me.txtLocation1Mult.MyLinkLable2 = Nothing
        Me.txtLocation1Mult.MyNullText = "All"
        Me.txtLocation1Mult.Name = "txtLocation1Mult"
        Me.txtLocation1Mult.Size = New System.Drawing.Size(294, 19)
        Me.txtLocation1Mult.TabIndex = 18
        '
        'lblLocation1
        '
        Me.lblLocation1.Location = New System.Drawing.Point(3, 60)
        Me.lblLocation1.Name = "lblLocation1"
        Me.lblLocation1.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation1.TabIndex = 17
        Me.lblLocation1.Text = "Location"
        '
        'txtCustomerCategoryMult
        '
        Me.txtCustomerCategoryMult.arrDispalyMember = Nothing
        Me.txtCustomerCategoryMult.arrValueMember = Nothing
        Me.txtCustomerCategoryMult.Location = New System.Drawing.Point(132, 172)
        Me.txtCustomerCategoryMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCategoryMult.MyLinkLable1 = Nothing
        Me.txtCustomerCategoryMult.MyLinkLable2 = Nothing
        Me.txtCustomerCategoryMult.MyNullText = "All"
        Me.txtCustomerCategoryMult.Name = "txtCustomerCategoryMult"
        Me.txtCustomerCategoryMult.Size = New System.Drawing.Size(294, 19)
        Me.txtCustomerCategoryMult.TabIndex = 16
        Me.txtCustomerCategoryMult.Visible = False
        '
        'lblCustomerCategory
        '
        Me.lblCustomerCategory.Location = New System.Drawing.Point(23, 172)
        Me.lblCustomerCategory.Name = "lblCustomerCategory"
        Me.lblCustomerCategory.Size = New System.Drawing.Size(103, 18)
        Me.lblCustomerCategory.TabIndex = 15
        Me.lblCustomerCategory.Text = "Customer Category"
        Me.lblCustomerCategory.Visible = False
        '
        'lblCustomer
        '
        Me.lblCustomer.Location = New System.Drawing.Point(3, 84)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 14
        Me.lblCustomer.Text = "Customer"
        '
        'txtCustomerMult
        '
        Me.txtCustomerMult.arrDispalyMember = Nothing
        Me.txtCustomerMult.arrValueMember = Nothing
        Me.txtCustomerMult.Location = New System.Drawing.Point(112, 84)
        Me.txtCustomerMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerMult.MyLinkLable1 = Nothing
        Me.txtCustomerMult.MyLinkLable2 = Nothing
        Me.txtCustomerMult.MyNullText = "All"
        Me.txtCustomerMult.Name = "txtCustomerMult"
        Me.txtCustomerMult.Size = New System.Drawing.Size(294, 19)
        Me.txtCustomerMult.TabIndex = 2
        '
        'gbLocation
        '
        Me.gbLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbLocation.Controls.Add(Me.cbgLocation)
        Me.gbLocation.Controls.Add(Me.Panel2)
        Me.gbLocation.HeaderText = "Location"
        Me.gbLocation.Location = New System.Drawing.Point(942, 14)
        Me.gbLocation.Name = "gbLocation"
        Me.gbLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbLocation.Size = New System.Drawing.Size(89, 64)
        Me.gbLocation.TabIndex = 1
        Me.gbLocation.Text = "Location"
        Me.gbLocation.Visible = False
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
        Me.cbgLocation.Size = New System.Drawing.Size(69, 14)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.TabStop = False
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(69, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(172, 1)
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
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblfromDate)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(403, 44)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(291, 11)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(82, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(240, 13)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.Location = New System.Drawing.Point(9, 12)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1084, 308)
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
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowFilteringRow = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1084, 308)
        Me.gv.TabIndex = 3
        Me.gv.Text = "gv"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1105, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "RadMenuItem2"
        Me.rmSaveLayout.AccessibleName = "RadMenuItem2"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'btnQuickExport
        '
        Me.btnQuickExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnQuickExport.Location = New System.Drawing.Point(428, 5)
        Me.btnQuickExport.Name = "btnQuickExport"
        Me.btnQuickExport.Size = New System.Drawing.Size(80, 23)
        Me.btnQuickExport.TabIndex = 24
        Me.btnQuickExport.Text = "Quick Export"
        '
        'btnUnSelect
        '
        Me.btnUnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnSelect.Location = New System.Drawing.Point(241, 5)
        Me.btnUnSelect.Name = "btnUnSelect"
        Me.btnUnSelect.Size = New System.Drawing.Size(80, 23)
        Me.btnUnSelect.TabIndex = 23
        Me.btnUnSelect.Text = "UnSelect All"
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(161, 5)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(71, 22)
        Me.BtnPrint.TabIndex = 16
        Me.BtnPrint.Text = "Print"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel})
        Me.RadSplitButton1.Location = New System.Drawing.Point(327, 6)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 21)
        Me.RadSplitButton1.TabIndex = 14
        Me.RadSplitButton1.Text = "Export"
        Me.RadSplitButton1.Visible = False
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "Excel"
        Me.rmExcel.AccessibleName = "Excel"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(84, 5)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(71, 22)
        Me.BtnReset.TabIndex = 13
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1012, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 15
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(7, 4)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 12
        Me.btnGo.Text = ">>>"
        '
        'TxtMultiCustomerGroup
        '
        Me.TxtMultiCustomerGroup.arrDispalyMember = Nothing
        Me.TxtMultiCustomerGroup.arrValueMember = Nothing
        Me.TxtMultiCustomerGroup.Location = New System.Drawing.Point(112, 109)
        Me.TxtMultiCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCustomerGroup.MyLinkLable1 = Nothing
        Me.TxtMultiCustomerGroup.MyLinkLable2 = Nothing
        Me.TxtMultiCustomerGroup.MyNullText = "All"
        Me.TxtMultiCustomerGroup.Name = "TxtMultiCustomerGroup"
        Me.TxtMultiCustomerGroup.Size = New System.Drawing.Size(294, 19)
        Me.TxtMultiCustomerGroup.TabIndex = 20
        '
        'FrmPrintFreshInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 411)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmPrintFreshInvoice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmPrintFreshInvoice"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbLocation.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gbLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents BtnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnUnSelect As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnQuickExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustomerMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtCustomerCategoryMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerCategory As common.Controls.MyLabel
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtLocation1Mult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation1 As common.Controls.MyLabel
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents TxtMultiCustomerGroup As common.UserControls.txtMultiSelectFinder
End Class

