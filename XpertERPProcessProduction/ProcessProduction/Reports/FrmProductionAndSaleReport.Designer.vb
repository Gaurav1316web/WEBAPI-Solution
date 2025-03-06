<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProductionAndSaleReport
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbSaleTransfer = New common.Controls.MyRadioButton()
        Me.rdbStockTransfer = New common.Controls.MyRadioButton()
        Me.rdbSaleReturn = New common.Controls.MyRadioButton()
        Me.rdbSale = New common.Controls.MyRadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbfgsfg = New common.Controls.MyRadioButton()
        Me.rdbSFG = New common.Controls.MyRadioButton()
        Me.rdbFG = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbInvoice = New common.Controls.MyRadioButton()
        Me.rdbDispatch = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAll = New common.Controls.MyRadioButton()
        Me.rdbUnposted = New common.Controls.MyRadioButton()
        Me.rdbPosted = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbWeekly = New common.Controls.MyRadioButton()
        Me.rdbDaily = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lbltoDate = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.BtnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.rdbSaleTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbStockTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSaleReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.rdbfgsfg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rdbInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDispatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbUnposted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbWeekly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.lbltoDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.btnExport.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1078, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer3.Size = New System.Drawing.Size(1078, 512)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 1
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Size = New System.Drawing.Size(1078, 483)
        Me.SplitContainer1.SplitterDistance = 440
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
        Me.RadPageView1.Size = New System.Drawing.Size(1078, 440)
        Me.RadPageView1.TabIndex = 14
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1057, 392)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rdbSaleTransfer)
        Me.RadGroupBox6.Controls.Add(Me.rdbStockTransfer)
        Me.RadGroupBox6.Controls.Add(Me.rdbSaleReturn)
        Me.RadGroupBox6.Controls.Add(Me.rdbSale)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(16, 249)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(366, 42)
        Me.RadGroupBox6.TabIndex = 58
        '
        'rdbSaleTransfer
        '
        Me.rdbSaleTransfer.Location = New System.Drawing.Point(246, 11)
        Me.rdbSaleTransfer.MyLinkLable1 = Nothing
        Me.rdbSaleTransfer.MyLinkLable2 = Nothing
        Me.rdbSaleTransfer.Name = "rdbSaleTransfer"
        Me.rdbSaleTransfer.Size = New System.Drawing.Size(108, 18)
        Me.rdbSaleTransfer.TabIndex = 4
        Me.rdbSaleTransfer.TabStop = False
        Me.rdbSaleTransfer.Text = "All(Sale+Transfer)"
        '
        'rdbStockTransfer
        '
        Me.rdbStockTransfer.Location = New System.Drawing.Point(149, 11)
        Me.rdbStockTransfer.MyLinkLable1 = Nothing
        Me.rdbStockTransfer.MyLinkLable2 = Nothing
        Me.rdbStockTransfer.Name = "rdbStockTransfer"
        Me.rdbStockTransfer.Size = New System.Drawing.Size(91, 18)
        Me.rdbStockTransfer.TabIndex = 3
        Me.rdbStockTransfer.TabStop = False
        Me.rdbStockTransfer.Text = "Stock Transfer"
        '
        'rdbSaleReturn
        '
        Me.rdbSaleReturn.Location = New System.Drawing.Point(66, 11)
        Me.rdbSaleReturn.MyLinkLable1 = Nothing
        Me.rdbSaleReturn.MyLinkLable2 = Nothing
        Me.rdbSaleReturn.Name = "rdbSaleReturn"
        Me.rdbSaleReturn.Size = New System.Drawing.Size(77, 18)
        Me.rdbSaleReturn.TabIndex = 2
        Me.rdbSaleReturn.TabStop = False
        Me.rdbSaleReturn.Text = "Sale Return"
        '
        'rdbSale
        '
        Me.rdbSale.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbSale.Location = New System.Drawing.Point(16, 11)
        Me.rdbSale.MyLinkLable1 = Nothing
        Me.rdbSale.MyLinkLable2 = Nothing
        Me.rdbSale.Name = "rdbSale"
        Me.rdbSale.Size = New System.Drawing.Size(41, 18)
        Me.rdbSale.TabIndex = 1
        Me.rdbSale.Text = "Sale"
        Me.rdbSale.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.rdbfgsfg)
        Me.RadGroupBox5.Controls.Add(Me.rdbSFG)
        Me.RadGroupBox5.Controls.Add(Me.rdbFG)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(16, 201)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox5.TabIndex = 57
        '
        'rdbfgsfg
        '
        Me.rdbfgsfg.Location = New System.Drawing.Point(170, 11)
        Me.rdbfgsfg.MyLinkLable1 = Nothing
        Me.rdbfgsfg.MyLinkLable2 = Nothing
        Me.rdbfgsfg.Name = "rdbfgsfg"
        Me.rdbfgsfg.Size = New System.Drawing.Size(33, 18)
        Me.rdbfgsfg.TabIndex = 3
        Me.rdbfgsfg.TabStop = False
        Me.rdbfgsfg.Text = "All"
        '
        'rdbSFG
        '
        Me.rdbSFG.Location = New System.Drawing.Point(81, 11)
        Me.rdbSFG.MyLinkLable1 = Nothing
        Me.rdbSFG.MyLinkLable2 = Nothing
        Me.rdbSFG.Name = "rdbSFG"
        Me.rdbSFG.Size = New System.Drawing.Size(39, 18)
        Me.rdbSFG.TabIndex = 2
        Me.rdbSFG.TabStop = False
        Me.rdbSFG.Text = "SFG"
        '
        'rdbFG
        '
        Me.rdbFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbFG.Location = New System.Drawing.Point(16, 11)
        Me.rdbFG.MyLinkLable1 = Nothing
        Me.rdbFG.MyLinkLable2 = Nothing
        Me.rdbFG.Name = "rdbFG"
        Me.rdbFG.Size = New System.Drawing.Size(33, 18)
        Me.rdbFG.TabIndex = 1
        Me.rdbFG.Text = "FG"
        Me.rdbFG.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rdbInvoice)
        Me.RadGroupBox4.Controls.Add(Me.rdbDispatch)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(16, 153)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox4.TabIndex = 56
        '
        'rdbInvoice
        '
        Me.rdbInvoice.Location = New System.Drawing.Point(135, 11)
        Me.rdbInvoice.MyLinkLable1 = Nothing
        Me.rdbInvoice.MyLinkLable2 = Nothing
        Me.rdbInvoice.Name = "rdbInvoice"
        Me.rdbInvoice.Size = New System.Drawing.Size(56, 18)
        Me.rdbInvoice.TabIndex = 2
        Me.rdbInvoice.TabStop = False
        Me.rdbInvoice.Text = "Invoice"
        '
        'rdbDispatch
        '
        Me.rdbDispatch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbDispatch.Location = New System.Drawing.Point(16, 11)
        Me.rdbDispatch.MyLinkLable1 = Nothing
        Me.rdbDispatch.MyLinkLable2 = Nothing
        Me.rdbDispatch.Name = "rdbDispatch"
        Me.rdbDispatch.Size = New System.Drawing.Size(107, 18)
        Me.rdbDispatch.TabIndex = 1
        Me.rdbDispatch.Text = "Challan(Dispatch)"
        Me.rdbDispatch.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rdbAll)
        Me.RadGroupBox2.Controls.Add(Me.rdbUnposted)
        Me.RadGroupBox2.Controls.Add(Me.rdbPosted)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(16, 105)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox2.TabIndex = 55
        '
        'rdbAll
        '
        Me.rdbAll.Location = New System.Drawing.Point(170, 11)
        Me.rdbAll.MyLinkLable1 = Nothing
        Me.rdbAll.MyLinkLable2 = Nothing
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(33, 18)
        Me.rdbAll.TabIndex = 3
        Me.rdbAll.TabStop = False
        Me.rdbAll.Text = "All"
        '
        'rdbUnposted
        '
        Me.rdbUnposted.Location = New System.Drawing.Point(92, 11)
        Me.rdbUnposted.MyLinkLable1 = Nothing
        Me.rdbUnposted.MyLinkLable2 = Nothing
        Me.rdbUnposted.Name = "rdbUnposted"
        Me.rdbUnposted.Size = New System.Drawing.Size(69, 18)
        Me.rdbUnposted.TabIndex = 2
        Me.rdbUnposted.TabStop = False
        Me.rdbUnposted.Text = "Unposted"
        '
        'rdbPosted
        '
        Me.rdbPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbPosted.Location = New System.Drawing.Point(16, 11)
        Me.rdbPosted.MyLinkLable1 = Nothing
        Me.rdbPosted.MyLinkLable2 = Nothing
        Me.rdbPosted.Name = "rdbPosted"
        Me.rdbPosted.Size = New System.Drawing.Size(54, 18)
        Me.rdbPosted.TabIndex = 1
        Me.rdbPosted.Text = "Posted"
        Me.rdbPosted.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rdbWeekly)
        Me.RadGroupBox1.Controls.Add(Me.rdbDaily)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(16, 57)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(174, 42)
        Me.RadGroupBox1.TabIndex = 54
        '
        'rdbWeekly
        '
        Me.rdbWeekly.Location = New System.Drawing.Point(92, 11)
        Me.rdbWeekly.MyLinkLable1 = Nothing
        Me.rdbWeekly.MyLinkLable2 = Nothing
        Me.rdbWeekly.Name = "rdbWeekly"
        Me.rdbWeekly.Size = New System.Drawing.Size(56, 18)
        Me.rdbWeekly.TabIndex = 2
        Me.rdbWeekly.TabStop = False
        Me.rdbWeekly.Text = "Weekly"
        '
        'rdbDaily
        '
        Me.rdbDaily.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbDaily.Location = New System.Drawing.Point(16, 11)
        Me.rdbDaily.MyLinkLable1 = Nothing
        Me.rdbDaily.MyLinkLable2 = Nothing
        Me.rdbDaily.Name = "rdbDaily"
        Me.rdbDaily.Size = New System.Drawing.Size(45, 18)
        Me.rdbDaily.TabIndex = 1
        Me.rdbDaily.Text = "Daily"
        Me.rdbDaily.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.lbltoDate)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(350, 42)
        Me.RadGroupBox3.TabIndex = 53
        '
        'lbltoDate
        '
        Me.lbltoDate.FieldName = Nothing
        Me.lbltoDate.Location = New System.Drawing.Point(186, 12)
        Me.lbltoDate.Name = "lbltoDate"
        Me.lbltoDate.Size = New System.Drawing.Size(45, 18)
        Me.lbltoDate.TabIndex = 4
        Me.lbltoDate.Text = "To Date"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(236, 11)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(107, 20)
        Me.ToDate.TabIndex = 3
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From Date"
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(67, 11)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(107, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1057, 392)
        Me.RadPageViewPage2.Text = "Detail"
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
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1057, 392)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(256, 8)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(71, 22)
        Me.BtnPrint.TabIndex = 168
        Me.BtnPrint.Text = "Print"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Controls.Add(Me.RadButton1)
        Me.btnExport.Controls.Add(Me.RadButton2)
        Me.btnExport.Controls.Add(Me.RadButton3)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(167, 8)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(83, 22)
        Me.btnExport.TabIndex = 167
        Me.btnExport.Text = "Export"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(343, 0)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(84, 22)
        Me.RadButton1.TabIndex = 166
        Me.RadButton1.Text = "Close"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(-268, 0)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(71, 22)
        Me.RadButton2.TabIndex = 165
        Me.RadButton2.Text = "Reset"
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton3.Location = New System.Drawing.Point(-345, 0)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(71, 22)
        Me.RadButton3.TabIndex = 164
        Me.RadButton3.Text = ">>>"
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
        Me.btnClose.Location = New System.Drawing.Point(982, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 166
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(91, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 165
        Me.btnReset.Text = "Reset"
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Location = New System.Drawing.Point(14, 8)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(71, 22)
        Me.btnReport.TabIndex = 164
        Me.btnReport.Text = ">>>"
        '
        'FrmProductionAndSaleReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1078, 512)
        Me.Controls.Add(Me.SplitContainer3)
        Me.Name = "FrmProductionAndSaleReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Production And Sale Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.rdbSaleTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbStockTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSaleReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.rdbfgsfg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rdbInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDispatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbUnposted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbWeekly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.lbltoDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.btnExport.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents lbltoDate As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnReport As RadButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents rdbDaily As common.Controls.MyRadioButton
    Friend WithEvents rdbWeekly As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rdbAll As common.Controls.MyRadioButton
    Friend WithEvents rdbUnposted As common.Controls.MyRadioButton
    Friend WithEvents rdbPosted As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents rdbInvoice As common.Controls.MyRadioButton
    Friend WithEvents rdbDispatch As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents rdbfgsfg As common.Controls.MyRadioButton
    Friend WithEvents rdbSFG As common.Controls.MyRadioButton
    Friend WithEvents rdbFG As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents rdbStockTransfer As common.Controls.MyRadioButton
    Friend WithEvents rdbSaleReturn As common.Controls.MyRadioButton
    Friend WithEvents rdbSale As common.Controls.MyRadioButton
    Friend WithEvents rdbSaleTransfer As common.Controls.MyRadioButton
    Friend WithEvents BtnPrint As RadButton
End Class

