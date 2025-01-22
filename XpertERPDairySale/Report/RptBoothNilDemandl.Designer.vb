<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptBoothNilDemandl
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbCustomerStatus = New System.Windows.Forms.RadioButton()
        Me.rdbInActive = New System.Windows.Forms.RadioButton()
        Me.rdbActive = New System.Windows.Forms.RadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtrouteNo = New common.UserControls.txtMultiSelectFinder()
        Me.monthlyDate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbnEvening = New System.Windows.Forms.RadioButton()
        Me.rbnmorning = New System.Windows.Forms.RadioButton()
        Me.gbDocStatus = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbDemandBoth = New System.Windows.Forms.RadioButton()
        Me.rdbProduct = New System.Windows.Forms.RadioButton()
        Me.rdbMilk = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbRangeWise = New System.Windows.Forms.RadioButton()
        Me.rdbMonth = New System.Windows.Forms.RadioButton()
        Me.rdbDay = New System.Windows.Forms.RadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.rbtnIceCream = New System.Windows.Forms.RadioButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.monthlyDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gbDocStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDocStatus.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 412
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 412)
        Me.RadPageView1.TabIndex = 12
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtrouteNo)
        Me.RadPageViewPage1.Controls.Add(Me.monthlyDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.gbDocStatus)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 364)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rdbCustomerStatus)
        Me.RadGroupBox2.Controls.Add(Me.rdbInActive)
        Me.RadGroupBox2.Controls.Add(Me.rdbActive)
        Me.RadGroupBox2.HeaderText = "Customer Status"
        Me.RadGroupBox2.Location = New System.Drawing.Point(563, 20)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(194, 45)
        Me.RadGroupBox2.TabIndex = 403
        Me.RadGroupBox2.Text = "Customer Status"
        '
        'rdbCustomerStatus
        '
        Me.rdbCustomerStatus.AutoSize = True
        Me.rdbCustomerStatus.Location = New System.Drawing.Point(137, 16)
        Me.rdbCustomerStatus.Name = "rdbCustomerStatus"
        Me.rdbCustomerStatus.Size = New System.Drawing.Size(49, 17)
        Me.rdbCustomerStatus.TabIndex = 6
        Me.rdbCustomerStatus.Text = "Both"
        Me.rdbCustomerStatus.UseVisualStyleBackColor = True
        '
        'rdbInActive
        '
        Me.rdbInActive.AutoSize = True
        Me.rdbInActive.Location = New System.Drawing.Point(66, 16)
        Me.rdbInActive.Name = "rdbInActive"
        Me.rdbInActive.Size = New System.Drawing.Size(65, 17)
        Me.rdbInActive.TabIndex = 5
        Me.rdbInActive.Text = "InActive"
        Me.rdbInActive.UseVisualStyleBackColor = True
        '
        'rdbActive
        '
        Me.rdbActive.AutoSize = True
        Me.rdbActive.Checked = True
        Me.rdbActive.Location = New System.Drawing.Point(13, 16)
        Me.rdbActive.Name = "rdbActive"
        Me.rdbActive.Size = New System.Drawing.Size(55, 17)
        Me.rdbActive.TabIndex = 4
        Me.rdbActive.TabStop = True
        Me.rdbActive.Text = "Active"
        Me.rdbActive.UseVisualStyleBackColor = True
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(3, 78)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel1.TabIndex = 405
        Me.MyLabel1.Text = "Route No."
        '
        'txtrouteNo
        '
        Me.txtrouteNo.arrDispalyMember = Nothing
        Me.txtrouteNo.arrValueMember = Nothing
        Me.txtrouteNo.Location = New System.Drawing.Point(61, 78)
        Me.txtrouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrouteNo.MyLinkLable1 = Nothing
        Me.txtrouteNo.MyLinkLable2 = Nothing
        Me.txtrouteNo.MyNullText = "All"
        Me.txtrouteNo.Name = "txtrouteNo"
        Me.txtrouteNo.Size = New System.Drawing.Size(243, 19)
        Me.txtrouteNo.TabIndex = 404
        '
        'monthlyDate
        '
        Me.monthlyDate.CalculationExpression = Nothing
        Me.monthlyDate.CustomFormat = "MMM-yyyy"
        Me.monthlyDate.FieldCode = Nothing
        Me.monthlyDate.FieldDesc = Nothing
        Me.monthlyDate.FieldMaxLength = 0
        Me.monthlyDate.FieldName = Nothing
        Me.monthlyDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.monthlyDate.isCalculatedField = False
        Me.monthlyDate.IsSourceFromTable = False
        Me.monthlyDate.IsSourceFromValueList = False
        Me.monthlyDate.IsUnique = False
        Me.monthlyDate.Location = New System.Drawing.Point(72, 48)
        Me.monthlyDate.MendatroryField = False
        Me.monthlyDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.monthlyDate.MyLinkLable1 = Nothing
        Me.monthlyDate.MyLinkLable2 = Nothing
        Me.monthlyDate.Name = "monthlyDate"
        Me.monthlyDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.monthlyDate.ReferenceFieldDesc = Nothing
        Me.monthlyDate.ReferenceFieldName = Nothing
        Me.monthlyDate.ReferenceTableName = Nothing
        Me.monthlyDate.Size = New System.Drawing.Size(96, 20)
        Me.monthlyDate.TabIndex = 59
        Me.monthlyDate.TabStop = False
        Me.monthlyDate.Text = "Dec-2011"
        Me.monthlyDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbnEvening)
        Me.RadGroupBox1.Controls.Add(Me.rbnmorning)
        Me.RadGroupBox1.HeaderText = "Shift"
        Me.RadGroupBox1.Location = New System.Drawing.Point(310, 68)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(173, 40)
        Me.RadGroupBox1.TabIndex = 403
        Me.RadGroupBox1.Text = "Shift"
        '
        'rbnEvening
        '
        Me.rbnEvening.AutoSize = True
        Me.rbnEvening.Location = New System.Drawing.Point(91, 16)
        Me.rbnEvening.Name = "rbnEvening"
        Me.rbnEvening.Size = New System.Drawing.Size(66, 17)
        Me.rbnEvening.TabIndex = 5
        Me.rbnEvening.Text = "Evening"
        Me.rbnEvening.UseVisualStyleBackColor = True
        '
        'rbnmorning
        '
        Me.rbnmorning.AutoSize = True
        Me.rbnmorning.Checked = True
        Me.rbnmorning.Location = New System.Drawing.Point(13, 16)
        Me.rbnmorning.Name = "rbnmorning"
        Me.rbnmorning.Size = New System.Drawing.Size(70, 17)
        Me.rbnmorning.TabIndex = 4
        Me.rbnmorning.TabStop = True
        Me.rbnmorning.Text = "Morning"
        Me.rbnmorning.UseVisualStyleBackColor = True
        '
        'gbDocStatus
        '
        Me.gbDocStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbDocStatus.Controls.Add(Me.rbtnIceCream)
        Me.gbDocStatus.Controls.Add(Me.rdbDemandBoth)
        Me.gbDocStatus.Controls.Add(Me.rdbProduct)
        Me.gbDocStatus.Controls.Add(Me.rdbMilk)
        Me.gbDocStatus.HeaderText = "Demand"
        Me.gbDocStatus.Location = New System.Drawing.Point(310, 20)
        Me.gbDocStatus.Name = "gbDocStatus"
        Me.gbDocStatus.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbDocStatus.Size = New System.Drawing.Size(247, 45)
        Me.gbDocStatus.TabIndex = 402
        Me.gbDocStatus.Text = "Demand"
        '
        'rdbDemandBoth
        '
        Me.rdbDemandBoth.AutoSize = True
        Me.rdbDemandBoth.Location = New System.Drawing.Point(137, 16)
        Me.rdbDemandBoth.Name = "rdbDemandBoth"
        Me.rdbDemandBoth.Size = New System.Drawing.Size(49, 17)
        Me.rdbDemandBoth.TabIndex = 6
        Me.rdbDemandBoth.Text = "Both"
        Me.rdbDemandBoth.UseVisualStyleBackColor = True
        '
        'rdbProduct
        '
        Me.rdbProduct.AutoSize = True
        Me.rdbProduct.Location = New System.Drawing.Point(66, 16)
        Me.rdbProduct.Name = "rdbProduct"
        Me.rdbProduct.Size = New System.Drawing.Size(65, 17)
        Me.rdbProduct.TabIndex = 5
        Me.rdbProduct.Text = "Product"
        Me.rdbProduct.UseVisualStyleBackColor = True
        '
        'rdbMilk
        '
        Me.rdbMilk.AutoSize = True
        Me.rdbMilk.Checked = True
        Me.rdbMilk.Location = New System.Drawing.Point(13, 16)
        Me.rdbMilk.Name = "rdbMilk"
        Me.rdbMilk.Size = New System.Drawing.Size(47, 17)
        Me.rdbMilk.TabIndex = 4
        Me.rdbMilk.TabStop = True
        Me.rdbMilk.Text = "Milk"
        Me.rdbMilk.UseVisualStyleBackColor = True
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbRangeWise)
        Me.RadGroupBox3.Controls.Add(Me.rdbMonth)
        Me.RadGroupBox3.Controls.Add(Me.rdbDay)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(288, 63)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Range"
        '
        'rdbRangeWise
        '
        Me.rdbRangeWise.AutoSize = True
        Me.rdbRangeWise.Location = New System.Drawing.Point(90, 18)
        Me.rdbRangeWise.Name = "rdbRangeWise"
        Me.rdbRangeWise.Size = New System.Drawing.Size(84, 17)
        Me.rdbRangeWise.TabIndex = 6
        Me.rdbRangeWise.Text = "Range wise"
        Me.rdbRangeWise.UseVisualStyleBackColor = True
        '
        'rdbMonth
        '
        Me.rdbMonth.AutoSize = True
        Me.rdbMonth.Location = New System.Drawing.Point(180, 18)
        Me.rdbMonth.Name = "rdbMonth"
        Me.rdbMonth.Size = New System.Drawing.Size(94, 17)
        Me.rdbMonth.TabIndex = 5
        Me.rdbMonth.Text = "Monthly wise"
        Me.rdbMonth.UseVisualStyleBackColor = True
        '
        'rdbDay
        '
        Me.rdbDay.AutoSize = True
        Me.rdbDay.Checked = True
        Me.rdbDay.Location = New System.Drawing.Point(14, 18)
        Me.rdbDay.Name = "rdbDay"
        Me.rdbDay.Size = New System.Drawing.Size(70, 17)
        Me.rdbDay.TabIndex = 4
        Me.rdbDay.TabStop = True
        Me.rdbDay.Text = "Day wise"
        Me.rdbDay.UseVisualStyleBackColor = True
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(151, 38)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(20, 38)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(176, 37)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(56, 38)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(89, 20)
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
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(671, 278)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'RadSplitExp
        '
        Me.RadSplitExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitExp.Location = New System.Drawing.Point(167, 6)
        Me.RadSplitExp.Name = "RadSplitExp"
        Me.RadSplitExp.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitExp.TabIndex = 159
        Me.RadSplitExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        Me.rmiPDF.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(699, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 158
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(13, 6)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 156
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(90, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 157
        Me.btnReset.Text = "Reset"
        '
        'rbtnIceCream
        '
        Me.rbtnIceCream.AutoSize = True
        Me.rbtnIceCream.Location = New System.Drawing.Point(137, 16)
        Me.rbtnIceCream.Name = "rbtnIceCream"
        Me.rbtnIceCream.Size = New System.Drawing.Size(74, 17)
        Me.rbtnIceCream.TabIndex = 7
        Me.rbtnIceCream.TabStop = True
        Me.rbtnIceCream.Text = "Ice Cream"
        Me.rbtnIceCream.UseVisualStyleBackColor = True
        Me.rbtnIceCream.Visible = False
        '
        'RptBoothNilDemandl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptBoothNilDemandl"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptBoothNilDemandl"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.monthlyDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.gbDocStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDocStatus.ResumeLayout(False)
        Me.gbDocStatus.PerformLayout()
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
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents gbDocStatus As RadGroupBox
    Friend WithEvents rdbDemandBoth As RadioButton
    Friend WithEvents rdbProduct As RadioButton
    Friend WithEvents rdbMilk As RadioButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents rdbRangeWise As RadioButton
    Friend WithEvents rdbMonth As RadioButton
    Friend WithEvents rdbDay As RadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitExp As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbnEvening As RadioButton
    Friend WithEvents rbnmorning As RadioButton
    Friend WithEvents monthlyDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtrouteNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rdbCustomerStatus As RadioButton
    Friend WithEvents rdbInActive As RadioButton
    Friend WithEvents rdbActive As RadioButton
    Friend WithEvents rbtnIceCream As RadioButton
End Class
