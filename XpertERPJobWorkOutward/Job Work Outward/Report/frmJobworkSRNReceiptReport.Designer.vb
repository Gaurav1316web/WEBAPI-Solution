<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJobworkSRNReceiptReport
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvLocation = New common.UserControls.MyRadGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rbtnLocationSelect = New common.Controls.MyRadioButton()
        Me.rbtnLocationAll = New common.Controls.MyRadioButton()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtCustGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(858, 486)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustGroup)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtItem)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendor)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(837, 438)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gvLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 51)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(388, 223)
        Me.RadGroupBox2.TabIndex = 350
        Me.RadGroupBox2.Text = "Location"
        '
        'gvLocation
        '
        Me.gvLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLocation.Location = New System.Drawing.Point(10, 40)
        '
        'gvLocation
        '
        Me.gvLocation.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvLocation.Name = "gvLocation"
        Me.gvLocation.ShowHeaderCellButtons = True
        Me.gvLocation.Size = New System.Drawing.Size(368, 173)
        Me.gvLocation.TabIndex = 3
        Me.gvLocation.Text = "RadGridView1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.SplitContainer2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(368, 20)
        Me.Panel4.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnLocationSelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnLocationAll)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton5)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton4)
        Me.SplitContainer2.Size = New System.Drawing.Size(368, 20)
        Me.SplitContainer2.SplitterDistance = 181
        Me.SplitContainer2.TabIndex = 0
        '
        'rbtnLocationSelect
        '
        Me.rbtnLocationSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationSelect.Location = New System.Drawing.Point(94, 1)
        Me.rbtnLocationSelect.MyLinkLable1 = Nothing
        Me.rbtnLocationSelect.MyLinkLable2 = Nothing
        Me.rbtnLocationSelect.Name = "rbtnLocationSelect"
        Me.rbtnLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnLocationSelect.TabIndex = 2
        Me.rbtnLocationSelect.Text = "Select"
        '
        'rbtnLocationAll
        '
        Me.rbtnLocationAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationAll.Location = New System.Drawing.Point(55, 1)
        Me.rbtnLocationAll.MyLinkLable1 = Nothing
        Me.rbtnLocationAll.MyLinkLable2 = Nothing
        Me.rbtnLocationAll.Name = "rbtnLocationAll"
        Me.rbtnLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnLocationAll.TabIndex = 1
        Me.rbtnLocationAll.Text = "All"
        '
        'RadButton5
        '
        Me.RadButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton5.Location = New System.Drawing.Point(93, 2)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(86, 15)
        Me.RadButton5.TabIndex = 3
        Me.RadButton5.Text = "Unselect All"
        '
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Location = New System.Drawing.Point(4, 2)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(86, 15)
        Me.RadButton4.TabIndex = 2
        Me.RadButton4.Text = "Select All"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(414, 98)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel7.TabIndex = 349
        Me.MyLabel7.Text = "Vendor Group"
        '
        'txtCustGroup
        '
        Me.txtCustGroup.arrDispalyMember = Nothing
        Me.txtCustGroup.arrValueMember = Nothing
        Me.txtCustGroup.Location = New System.Drawing.Point(500, 98)
        Me.txtCustGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustGroup.MyLinkLable1 = Me.MyLabel7
        Me.txtCustGroup.MyLinkLable2 = Nothing
        Me.txtCustGroup.MyNullText = "All"
        Me.txtCustGroup.Name = "txtCustGroup"
        Me.txtCustGroup.Size = New System.Drawing.Size(329, 19)
        Me.txtCustGroup.TabIndex = 348
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(414, 75)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel4.TabIndex = 347
        Me.MyLabel4.Text = "Item"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(500, 74)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.MyLabel4
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(328, 19)
        Me.txtItem.TabIndex = 346
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(414, 123)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(43, 18)
        Me.MyLabel8.TabIndex = 345
        Me.MyLabel8.Text = "Vendor"
        '
        'txtVendor
        '
        Me.txtVendor.arrDispalyMember = Nothing
        Me.txtVendor.arrValueMember = Nothing
        Me.txtVendor.Location = New System.Drawing.Point(500, 122)
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.MyLabel8
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyNullText = "All"
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(329, 19)
        Me.txtVendor.TabIndex = 344
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbDetail)
        Me.RadGroupBox3.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(391, 42)
        Me.RadGroupBox3.TabIndex = 341
        Me.RadGroupBox3.Text = "Date Range"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(322, 16)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 5
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbSummary.Location = New System.Drawing.Point(250, 16)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 4
        Me.rdbSummary.Text = "Summary"
        Me.rdbSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
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
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(632, 457)
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
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(632, 457)
        Me.gv.TabIndex = 6
        Me.gv.Text = "RadGridView1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(858, 516)
        Me.SplitContainer1.SplitterDistance = 486
        Me.SplitContainer1.TabIndex = 1
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(791, 1)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(67, 24)
        Me.RadButton2.TabIndex = 15
        Me.RadButton2.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(79, 1)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(95, 24)
        Me.btnReset.TabIndex = 13
        Me.btnReset.Text = "Reset"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(174, 1)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 24)
        Me.btnExport.TabIndex = 12
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel"
        Me.RadMenuItem1.AccessibleName = "Excel"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF"
        Me.RadMenuItem2.AccessibleName = "PDF"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(1, 1)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(78, 24)
        Me.btnRefresh.TabIndex = 11
        Me.btnRefresh.Text = ">>>"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(858, 20)
        Me.rdmenufile.TabIndex = 71
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
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
        'frmJobworkSRNReceiptReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(858, 536)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmJobworkSRNReceiptReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Jobwork SRN Receipt Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtCustGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvLocation As common.UserControls.MyRadGridView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Protected WithEvents rbtnLocationSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadButton5 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
End Class

