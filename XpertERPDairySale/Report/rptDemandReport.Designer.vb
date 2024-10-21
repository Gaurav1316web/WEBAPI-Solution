<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptDemandReport
    'Inherits System.Windows.Forms.Form
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkBookingWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.pnlMilkPouch = New System.Windows.Forms.Panel()
        Me.chkProduct = New Telerik.WinControls.UI.RadCheckBox()
        Me.rbtnAsPerBooking = New System.Windows.Forms.RadioButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.cboShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.rdbCreate = New System.Windows.Forms.RadioButton()
        Me.rdbLtr = New System.Windows.Forms.RadioButton()
        Me.chkMilkPouch = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.TxtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.chkFirstAndSecondSpell = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFilterByCreatedDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSaleInvoiceWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.rbtnDateWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRouteSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkGatePass = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRouteBoothWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkDayWiseSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFirstAndSecondSpellAbstract = New Telerik.WinControls.UI.RadCheckBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkBookingWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMilkPouch.SuspendLayout()
        CType(Me.chkProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMilkPouch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFirstAndSecondSpell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFilterByCreatedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSaleInvoiceWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDateWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkGatePass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteBoothWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDayWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFirstAndSecondSpellAbstract, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 402
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 402)
        Me.RadPageView1.TabIndex = 8
        Me.RadPageView1.TabStop = False
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnDateWise)
        Me.RadPageViewPage1.Controls.Add(Me.chkRouteSummary)
        Me.RadPageViewPage1.Controls.Add(Me.chkFirstAndSecondSpell)
        Me.RadPageViewPage1.Controls.Add(Me.chkFilterByCreatedDate)
        Me.RadPageViewPage1.Controls.Add(Me.chkSummary)
        Me.RadPageViewPage1.Controls.Add(Me.chkSaleInvoiceWise)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.chkBookingWise)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.pnlMilkPouch)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 354)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'chkBookingWise
        '
        Me.chkBookingWise.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBookingWise.Location = New System.Drawing.Point(14, 66)
        Me.chkBookingWise.Name = "chkBookingWise"
        Me.chkBookingWise.Size = New System.Drawing.Size(89, 18)
        Me.chkBookingWise.TabIndex = 431
        Me.chkBookingWise.Text = "Booking Wise"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(9, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox3.TabIndex = 430
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
        Me.ToDate.Size = New System.Drawing.Size(81, 20)
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
        Me.fromDate.Size = New System.Drawing.Size(81, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'pnlMilkPouch
        '
        Me.pnlMilkPouch.Controls.Add(Me.chkProduct)
        Me.pnlMilkPouch.Controls.Add(Me.rbtnAsPerBooking)
        Me.pnlMilkPouch.Controls.Add(Me.MyLabel7)
        Me.pnlMilkPouch.Controls.Add(Me.cboShift)
        Me.pnlMilkPouch.Controls.Add(Me.rdbCreate)
        Me.pnlMilkPouch.Controls.Add(Me.rdbLtr)
        Me.pnlMilkPouch.Controls.Add(Me.chkMilkPouch)
        Me.pnlMilkPouch.Location = New System.Drawing.Point(355, 3)
        Me.pnlMilkPouch.Name = "pnlMilkPouch"
        Me.pnlMilkPouch.Size = New System.Drawing.Size(301, 50)
        Me.pnlMilkPouch.TabIndex = 429
        '
        'chkProduct
        '
        Me.chkProduct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkProduct.Location = New System.Drawing.Point(3, 27)
        Me.chkProduct.Name = "chkProduct"
        Me.chkProduct.Size = New System.Drawing.Size(59, 18)
        Me.chkProduct.TabIndex = 432
        Me.chkProduct.Text = "Product"
        '
        'rbtnAsPerBooking
        '
        Me.rbtnAsPerBooking.AutoSize = True
        Me.rbtnAsPerBooking.Location = New System.Drawing.Point(185, 6)
        Me.rbtnAsPerBooking.Name = "rbtnAsPerBooking"
        Me.rbtnAsPerBooking.Size = New System.Drawing.Size(102, 17)
        Me.rbtnAsPerBooking.TabIndex = 430
        Me.rbtnAsPerBooking.Text = "As Per Booking"
        Me.rbtnAsPerBooking.UseVisualStyleBackColor = True
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(87, 26)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel7.TabIndex = 429
        Me.MyLabel7.Text = "Shift"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.DropDownAnimationEnabled = True
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Both"
        RadListDataItem2.Text = "Morning"
        RadListDataItem3.Text = "Evening"
        RadListDataItem4.Text = "Shift Wise"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Items.Add(RadListDataItem3)
        Me.cboShift.Items.Add(RadListDataItem4)
        Me.cboShift.Location = New System.Drawing.Point(122, 25)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.Size = New System.Drawing.Size(91, 20)
        Me.cboShift.TabIndex = 429
        '
        'rdbCreate
        '
        Me.rdbCreate.AutoSize = True
        Me.rdbCreate.Checked = True
        Me.rdbCreate.Location = New System.Drawing.Point(127, 6)
        Me.rdbCreate.Name = "rdbCreate"
        Me.rdbCreate.Size = New System.Drawing.Size(52, 17)
        Me.rdbCreate.TabIndex = 1
        Me.rdbCreate.TabStop = True
        Me.rdbCreate.Text = "Crate"
        Me.rdbCreate.UseVisualStyleBackColor = True
        '
        'rdbLtr
        '
        Me.rdbLtr.AutoSize = True
        Me.rdbLtr.Location = New System.Drawing.Point(85, 6)
        Me.rdbLtr.Name = "rdbLtr"
        Me.rdbLtr.Size = New System.Drawing.Size(38, 17)
        Me.rdbLtr.TabIndex = 0
        Me.rdbLtr.Text = "Ltr"
        Me.rdbLtr.UseVisualStyleBackColor = True
        '
        'chkMilkPouch
        '
        Me.chkMilkPouch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkMilkPouch.Location = New System.Drawing.Point(1, 4)
        Me.chkMilkPouch.Name = "chkMilkPouch"
        Me.chkMilkPouch.Size = New System.Drawing.Size(76, 18)
        Me.chkMilkPouch.TabIndex = 426
        Me.chkMilkPouch.Text = "Milk Pouch"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 355)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 355)
        Me.gv1.TabIndex = 3
        Me.gv1.VarID = ""
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(19, 10)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 148
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(93, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 149
        Me.btnReset.Text = "Reset"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(100, 103)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Nothing
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(332, 19)
        Me.txtCustomer.TabIndex = 432
        '
        'TxtRoute
        '
        Me.TxtRoute.arrDispalyMember = Nothing
        Me.TxtRoute.arrValueMember = Nothing
        Me.TxtRoute.Location = New System.Drawing.Point(100, 128)
        Me.TxtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoute.MyLinkLable1 = Nothing
        Me.TxtRoute.MyLinkLable2 = Nothing
        Me.TxtRoute.MyNullText = "All"
        Me.TxtRoute.Name = "TxtRoute"
        Me.TxtRoute.Size = New System.Drawing.Size(332, 19)
        Me.TxtRoute.TabIndex = 433
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(14, 102)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 434
        Me.lblCustomer.Text = "Customer"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(14, 129)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel10.TabIndex = 435
        Me.MyLabel10.Text = "Route"
        '
        'chkFirstAndSecondSpell
        '
        Me.chkFirstAndSecondSpell.Location = New System.Drawing.Point(404, 196)
        Me.chkFirstAndSecondSpell.Name = "chkFirstAndSecondSpell"
        Me.chkFirstAndSecondSpell.Size = New System.Drawing.Size(232, 18)
        Me.chkFirstAndSecondSpell.TabIndex = 440
        Me.chkFirstAndSecondSpell.Text = "First And Second Spell Card Sale Summary"
        Me.chkFirstAndSecondSpell.Visible = False
        '
        'chkFilterByCreatedDate
        '
        Me.chkFilterByCreatedDate.Location = New System.Drawing.Point(390, 141)
        Me.chkFilterByCreatedDate.Name = "chkFilterByCreatedDate"
        Me.chkFilterByCreatedDate.Size = New System.Drawing.Size(129, 18)
        Me.chkFilterByCreatedDate.TabIndex = 439
        Me.chkFilterByCreatedDate.Text = "Filter By Created Date"
        Me.chkFilterByCreatedDate.Visible = False
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(404, 172)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 438
        Me.chkSummary.Text = "Summary"
        Me.chkSummary.Visible = False
        '
        'chkSaleInvoiceWise
        '
        Me.chkSaleInvoiceWise.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSaleInvoiceWise.Location = New System.Drawing.Point(143, 196)
        Me.chkSaleInvoiceWise.Name = "chkSaleInvoiceWise"
        Me.chkSaleInvoiceWise.Size = New System.Drawing.Size(106, 18)
        Me.chkSaleInvoiceWise.TabIndex = 437
        Me.chkSaleInvoiceWise.Text = "Sale Invoice Wise"
        Me.chkSaleInvoiceWise.Visible = False
        '
        'rbtnDateWise
        '
        Me.rbtnDateWise.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnDateWise.Location = New System.Drawing.Point(425, 168)
        Me.rbtnDateWise.Name = "rbtnDateWise"
        Me.rbtnDateWise.Size = New System.Drawing.Size(71, 18)
        Me.rbtnDateWise.TabIndex = 442
        Me.rbtnDateWise.Text = "Date Wise"
        Me.rbtnDateWise.Visible = False
        '
        'chkRouteSummary
        '
        Me.chkRouteSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRouteSummary.Location = New System.Drawing.Point(283, 168)
        Me.chkRouteSummary.Name = "chkRouteSummary"
        Me.chkRouteSummary.Size = New System.Drawing.Size(126, 18)
        Me.chkRouteSummary.TabIndex = 441
        Me.chkRouteSummary.Text = "Route Summary Print"
        Me.chkRouteSummary.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkGatePass)
        Me.RadGroupBox1.Controls.Add(Me.chkRouteBoothWise)
        Me.RadGroupBox1.Controls.Add(Me.ChkDayWiseSummary)
        Me.RadGroupBox1.Controls.Add(Me.chkFirstAndSecondSpellAbstract)
        Me.RadGroupBox1.HeaderText = "RadGroupBox1"
        Me.RadGroupBox1.Location = New System.Drawing.Point(259, 231)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(345, 86)
        Me.RadGroupBox1.TabIndex = 443
        Me.RadGroupBox1.Text = "RadGroupBox1"
        Me.RadGroupBox1.Visible = False
        '
        'chkGatePass
        '
        Me.chkGatePass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkGatePass.Enabled = False
        Me.chkGatePass.Location = New System.Drawing.Point(5, 21)
        Me.chkGatePass.Name = "chkGatePass"
        Me.chkGatePass.Size = New System.Drawing.Size(95, 18)
        Me.chkGatePass.TabIndex = 425
        Me.chkGatePass.Text = "Only Gate Pass"
        Me.chkGatePass.Visible = False
        '
        'chkRouteBoothWise
        '
        Me.chkRouteBoothWise.Location = New System.Drawing.Point(5, 36)
        Me.chkRouteBoothWise.Name = "chkRouteBoothWise"
        Me.chkRouteBoothWise.Size = New System.Drawing.Size(114, 18)
        Me.chkRouteBoothWise.TabIndex = 422
        Me.chkRouteBoothWise.Text = "Route/Booth Wise "
        Me.chkRouteBoothWise.Visible = False
        '
        'ChkDayWiseSummary
        '
        Me.ChkDayWiseSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChkDayWiseSummary.Location = New System.Drawing.Point(5, 51)
        Me.ChkDayWiseSummary.Name = "ChkDayWiseSummary"
        Me.ChkDayWiseSummary.Size = New System.Drawing.Size(117, 18)
        Me.ChkDayWiseSummary.TabIndex = 423
        Me.ChkDayWiseSummary.Text = "Day Wise Summary"
        Me.ChkDayWiseSummary.Visible = False
        '
        'chkFirstAndSecondSpellAbstract
        '
        Me.chkFirstAndSecondSpellAbstract.Location = New System.Drawing.Point(5, 66)
        Me.chkFirstAndSecondSpellAbstract.Name = "chkFirstAndSecondSpellAbstract"
        Me.chkFirstAndSecondSpellAbstract.Size = New System.Drawing.Size(226, 18)
        Me.chkFirstAndSecondSpellAbstract.TabIndex = 424
        Me.chkFirstAndSecondSpellAbstract.Text = "First And Second Spell Card Sale Abstract"
        Me.chkFirstAndSecondSpellAbstract.Visible = False
        '
        'rptDemandReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptDemandReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptDemandReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkBookingWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMilkPouch.ResumeLayout(False)
        Me.pnlMilkPouch.PerformLayout()
        CType(Me.chkProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMilkPouch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFirstAndSecondSpell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFilterByCreatedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSaleInvoiceWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDateWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkGatePass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteBoothWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDayWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFirstAndSecondSpellAbstract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents pnlMilkPouch As Panel
    Friend WithEvents rbtnAsPerBooking As RadioButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents cboShift As RadDropDownList
    Friend WithEvents rdbCreate As RadioButton
    Friend WithEvents rdbLtr As RadioButton
    Friend WithEvents chkMilkPouch As RadCheckBox
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents chkBookingWise As RadCheckBox
    Friend WithEvents chkProduct As RadCheckBox
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents TxtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents chkFirstAndSecondSpell As RadCheckBox
    Friend WithEvents chkFilterByCreatedDate As RadCheckBox
    Friend WithEvents chkSummary As RadCheckBox
    Friend WithEvents chkSaleInvoiceWise As RadCheckBox
    Friend WithEvents rbtnDateWise As RadCheckBox
    Friend WithEvents chkRouteSummary As RadCheckBox
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents chkGatePass As RadCheckBox
    Friend WithEvents chkRouteBoothWise As RadCheckBox
    Friend WithEvents ChkDayWiseSummary As RadCheckBox
    Friend WithEvents chkFirstAndSecondSpellAbstract As RadCheckBox
End Class
