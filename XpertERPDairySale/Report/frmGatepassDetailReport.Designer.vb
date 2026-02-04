Imports common
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGatepassDetailReport
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBothShift = New System.Windows.Forms.RadioButton()
        Me.rbtnEvening = New System.Windows.Forms.RadioButton()
        Me.rbtnMorning = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnIceCream = New System.Windows.Forms.RadioButton()
        Me.rbtnBoth = New System.Windows.Forms.RadioButton()
        Me.rbtnProduct = New System.Windows.Forms.RadioButton()
        Me.rbtnMilk = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnSupplyDate = New System.Windows.Forms.RadioButton()
        Me.rbtnGatepassDate = New System.Windows.Forms.RadioButton()
        Me.txtMultRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.ddlSubCategory = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnWithIndiCust = New System.Windows.Forms.RadioButton()
        Me.rbtnOnlyIndiCust = New System.Windows.Forms.RadioButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdmenufile)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 381)
        Me.SplitContainer1.SplitterDistance = 340
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 320)
        Me.RadPageView1.TabIndex = 73
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.ddlSubCategory)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 272)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rbtnBothShift)
        Me.RadGroupBox4.Controls.Add(Me.rbtnEvening)
        Me.RadGroupBox4.Controls.Add(Me.rbtnMorning)
        Me.RadGroupBox4.HeaderText = "Shift"
        Me.RadGroupBox4.Location = New System.Drawing.Point(21, 101)
        Me.RadGroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox4.Size = New System.Drawing.Size(327, 45)
        Me.RadGroupBox4.TabIndex = 362
        Me.RadGroupBox4.Text = "Shift"
        '
        'rbtnBothShift
        '
        Me.rbtnBothShift.AutoSize = True
        Me.rbtnBothShift.Location = New System.Drawing.Point(229, 16)
        Me.rbtnBothShift.Name = "rbtnBothShift"
        Me.rbtnBothShift.Size = New System.Drawing.Size(49, 17)
        Me.rbtnBothShift.TabIndex = 2
        Me.rbtnBothShift.TabStop = True
        Me.rbtnBothShift.Text = "Both"
        Me.rbtnBothShift.UseVisualStyleBackColor = True
        '
        'rbtnEvening
        '
        Me.rbtnEvening.AutoSize = True
        Me.rbtnEvening.Location = New System.Drawing.Point(129, 16)
        Me.rbtnEvening.Name = "rbtnEvening"
        Me.rbtnEvening.Size = New System.Drawing.Size(66, 17)
        Me.rbtnEvening.TabIndex = 1
        Me.rbtnEvening.TabStop = True
        Me.rbtnEvening.Text = "Evening"
        Me.rbtnEvening.UseVisualStyleBackColor = True
        '
        'rbtnMorning
        '
        Me.rbtnMorning.AutoSize = True
        Me.rbtnMorning.Location = New System.Drawing.Point(30, 16)
        Me.rbtnMorning.Name = "rbtnMorning"
        Me.rbtnMorning.Size = New System.Drawing.Size(70, 17)
        Me.rbtnMorning.TabIndex = 0
        Me.rbtnMorning.TabStop = True
        Me.rbtnMorning.Text = "Morning"
        Me.rbtnMorning.UseVisualStyleBackColor = True
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnIceCream)
        Me.RadGroupBox2.Controls.Add(Me.rbtnBoth)
        Me.RadGroupBox2.Controls.Add(Me.rbtnProduct)
        Me.RadGroupBox2.Controls.Add(Me.rbtnMilk)
        Me.RadGroupBox2.HeaderText = "Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(21, 150)
        Me.RadGroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox2.Size = New System.Drawing.Size(327, 45)
        Me.RadGroupBox2.TabIndex = 361
        Me.RadGroupBox2.Text = "Type"
        '
        'rbtnIceCream
        '
        Me.rbtnIceCream.AutoSize = True
        Me.rbtnIceCream.Location = New System.Drawing.Point(229, 16)
        Me.rbtnIceCream.Name = "rbtnIceCream"
        Me.rbtnIceCream.Size = New System.Drawing.Size(74, 17)
        Me.rbtnIceCream.TabIndex = 5
        Me.rbtnIceCream.TabStop = True
        Me.rbtnIceCream.Text = "Ice Cream"
        Me.rbtnIceCream.UseVisualStyleBackColor = True
        Me.rbtnIceCream.Visible = False
        '
        'rbtnBoth
        '
        Me.rbtnBoth.AutoSize = True
        Me.rbtnBoth.Location = New System.Drawing.Point(229, 16)
        Me.rbtnBoth.Name = "rbtnBoth"
        Me.rbtnBoth.Size = New System.Drawing.Size(49, 17)
        Me.rbtnBoth.TabIndex = 4
        Me.rbtnBoth.TabStop = True
        Me.rbtnBoth.Text = "Both"
        Me.rbtnBoth.UseVisualStyleBackColor = True
        '
        'rbtnProduct
        '
        Me.rbtnProduct.AutoSize = True
        Me.rbtnProduct.Location = New System.Drawing.Point(129, 16)
        Me.rbtnProduct.Name = "rbtnProduct"
        Me.rbtnProduct.Size = New System.Drawing.Size(65, 17)
        Me.rbtnProduct.TabIndex = 3
        Me.rbtnProduct.TabStop = True
        Me.rbtnProduct.Text = "Product"
        Me.rbtnProduct.UseVisualStyleBackColor = True
        '
        'rbtnMilk
        '
        Me.rbtnMilk.AutoSize = True
        Me.rbtnMilk.Location = New System.Drawing.Point(30, 16)
        Me.rbtnMilk.Name = "rbtnMilk"
        Me.rbtnMilk.Size = New System.Drawing.Size(47, 17)
        Me.rbtnMilk.TabIndex = 2
        Me.rbtnMilk.TabStop = True
        Me.rbtnMilk.Text = "Milk"
        Me.rbtnMilk.UseVisualStyleBackColor = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnSupplyDate)
        Me.RadGroupBox1.Controls.Add(Me.rbtnGatepassDate)
        Me.RadGroupBox1.HeaderText = "Select Date"
        Me.RadGroupBox1.Location = New System.Drawing.Point(21, 3)
        Me.RadGroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox1.Size = New System.Drawing.Size(327, 45)
        Me.RadGroupBox1.TabIndex = 360
        Me.RadGroupBox1.Text = "Select Date"
        '
        'rbtnSupplyDate
        '
        Me.rbtnSupplyDate.AutoSize = True
        Me.rbtnSupplyDate.Location = New System.Drawing.Point(181, 16)
        Me.rbtnSupplyDate.Name = "rbtnSupplyDate"
        Me.rbtnSupplyDate.Size = New System.Drawing.Size(87, 17)
        Me.rbtnSupplyDate.TabIndex = 1
        Me.rbtnSupplyDate.TabStop = True
        Me.rbtnSupplyDate.Text = "Supply Date"
        Me.rbtnSupplyDate.UseVisualStyleBackColor = True
        '
        'rbtnGatepassDate
        '
        Me.rbtnGatepassDate.AutoSize = True
        Me.rbtnGatepassDate.Location = New System.Drawing.Point(32, 16)
        Me.rbtnGatepassDate.Name = "rbtnGatepassDate"
        Me.rbtnGatepassDate.Size = New System.Drawing.Size(99, 17)
        Me.rbtnGatepassDate.TabIndex = 0
        Me.rbtnGatepassDate.TabStop = True
        Me.rbtnGatepassDate.Text = "Gatepass Date"
        Me.rbtnGatepassDate.UseVisualStyleBackColor = True
        '
        'txtMultRoute
        '
        Me.txtMultRoute.arrDispalyMember = Nothing
        Me.txtMultRoute.arrValueMember = Nothing
        Me.txtMultRoute.Location = New System.Drawing.Point(80, 203)
        Me.txtMultRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMultRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultRoute.MyLinkLable1 = Me.MyLabel10
        Me.txtMultRoute.MyLinkLable2 = Nothing
        Me.txtMultRoute.MyNullText = "All"
        Me.txtMultRoute.Name = "txtMultRoute"
        Me.txtMultRoute.Size = New System.Drawing.Size(268, 19)
        Me.txtMultRoute.TabIndex = 358
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(28, 203)
        Me.MyLabel10.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel10.TabIndex = 359
        Me.MyLabel10.Text = "Route"
        '
        'ddlSubCategory
        '
        Me.ddlSubCategory.AutoCompleteDisplayMember = Nothing
        Me.ddlSubCategory.AutoCompleteValueMember = Nothing
        Me.ddlSubCategory.DropDownAnimationEnabled = True
        RadListDataItem1.Text = "Both"
        RadListDataItem2.Text = "Sale Invoice"
        RadListDataItem3.Text = "Sale Return"
        Me.ddlSubCategory.Items.Add(RadListDataItem1)
        Me.ddlSubCategory.Items.Add(RadListDataItem2)
        Me.ddlSubCategory.Items.Add(RadListDataItem3)
        Me.ddlSubCategory.Location = New System.Drawing.Point(1098, 94)
        Me.ddlSubCategory.Margin = New System.Windows.Forms.Padding(4)
        Me.ddlSubCategory.Name = "ddlSubCategory"
        Me.ddlSubCategory.Size = New System.Drawing.Size(161, 20)
        Me.ddlSubCategory.TabIndex = 344
        Me.ddlSubCategory.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(21, 51)
        Me.RadGroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox3.Size = New System.Drawing.Size(327, 45)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(173, 15)
        Me.RadLabel2.Margin = New System.Windows.Forms.Padding(4)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(7, 15)
        Me.RadLabel1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(209, 14)
        Me.ToDate.Margin = New System.Windows.Forms.Padding(4)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(104, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(59, 14)
        Me.fromDate.Margin = New System.Windows.Forms.Padding(4)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(104, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 268)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        Me.Gv1.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.AllowDragToGroup = False
        Me.Gv1.MasterTemplate.AllowEditRow = False
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(779, 268)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Margin = New System.Windows.Forms.Padding(4)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(800, 20)
        Me.rdmenufile.TabIndex = 74
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        Me.rdmenufile1.UseCompatibleTextRendering = False
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        Me.rmSaveLayout.UseCompatibleTextRendering = False
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        Me.rmDeleteLayout.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(719, 8)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 22)
        Me.btnClose.TabIndex = 163
        Me.btnClose.Text = "Close"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadSplitButton1.Location = New System.Drawing.Point(150, 8)
        Me.RadSplitButton1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(84, 22)
        Me.RadSplitButton1.TabIndex = 162
        Me.RadSplitButton1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Excel"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(12, 8)
        Me.btnGo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 22)
        Me.btnGo.TabIndex = 161
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(81, 8)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 22)
        Me.btnReset.TabIndex = 160
        Me.btnReset.Text = "Reset"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.rbtnWithIndiCust)
        Me.RadGroupBox5.Controls.Add(Me.rbtnOnlyIndiCust)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(356, 7)
        Me.RadGroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox5.Size = New System.Drawing.Size(342, 41)
        Me.RadGroupBox5.TabIndex = 363
        '
        'rbtnWithIndiCust
        '
        Me.rbtnWithIndiCust.AutoSize = True
        Me.rbtnWithIndiCust.Location = New System.Drawing.Point(170, 12)
        Me.rbtnWithIndiCust.Name = "rbtnWithIndiCust"
        Me.rbtnWithIndiCust.Size = New System.Drawing.Size(156, 17)
        Me.rbtnWithIndiCust.TabIndex = 1
        Me.rbtnWithIndiCust.TabStop = True
        Me.rbtnWithIndiCust.Text = "With Individual Customer"
        Me.rbtnWithIndiCust.UseVisualStyleBackColor = True
        '
        'rbtnOnlyIndiCust
        '
        Me.rbtnOnlyIndiCust.AutoSize = True
        Me.rbtnOnlyIndiCust.Checked = True
        Me.rbtnOnlyIndiCust.Location = New System.Drawing.Point(9, 12)
        Me.rbtnOnlyIndiCust.Name = "rbtnOnlyIndiCust"
        Me.rbtnOnlyIndiCust.Size = New System.Drawing.Size(155, 17)
        Me.rbtnOnlyIndiCust.TabIndex = 0
        Me.rbtnOnlyIndiCust.TabStop = True
        Me.rbtnOnlyIndiCust.Text = "Only Individual Customer"
        Me.rbtnOnlyIndiCust.UseVisualStyleBackColor = True
        '
        'frmGatepassDetailReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 381)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmGatepassDetailReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmGatepassDetailReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents txtMultRoute As UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel10 As Controls.MyLabel
    Friend WithEvents ddlSubCategory As RadDropDownList
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As Controls.MyLabel
    Friend WithEvents RadLabel1 As Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnGatepassDate As RadioButton
    Friend WithEvents rbtnSupplyDate As RadioButton
    Friend WithEvents rbtnBoth As RadioButton
    Friend WithEvents rbtnProduct As RadioButton
    Friend WithEvents rbtnMilk As RadioButton
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents rbtnEvening As RadioButton
    Friend WithEvents rbtnMorning As RadioButton
    Friend WithEvents rbtnBothShift As RadioButton
    Friend WithEvents rdmenufile As RadMenu
    Friend WithEvents rdmenufile1 As RadMenuItem
    Friend WithEvents rmSaveLayout As RadMenuItem
    Friend WithEvents rmDeleteLayout As RadMenuItem
    Friend WithEvents rbtnIceCream As RadioButton
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents rbtnWithIndiCust As RadioButton
    Friend WithEvents rbtnOnlyIndiCust As RadioButton
End Class
