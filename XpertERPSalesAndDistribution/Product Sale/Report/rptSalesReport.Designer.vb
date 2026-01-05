<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptSalesReport
    'Inherits System.Windows.Forms.Form
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbSaleTransfer = New common.Controls.MyRadioButton()
        Me.rdbStockTransfer = New common.Controls.MyRadioButton()
        Me.rdbSaleReturn = New common.Controls.MyRadioButton()
        Me.rdbSale = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbInvoice = New common.Controls.MyRadioButton()
        Me.rdbDispatch = New common.Controls.MyRadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbfgsfg = New common.Controls.MyRadioButton()
        Me.rdbSFG = New common.Controls.MyRadioButton()
        Me.rdbFG = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAll = New common.Controls.MyRadioButton()
        Me.rdbUnposted = New common.Controls.MyRadioButton()
        Me.rdbPosted = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbnCustgroup = New System.Windows.Forms.RadioButton()
        Me.rbnPricegroup = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
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
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rdbInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDispatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.rdbfgsfg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbUnposted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 405
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
        Me.RadPageView1.Size = New System.Drawing.Size(800, 405)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.Label3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 357)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(78, 70)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(191, 19)
        Me.txtLocation.TabIndex = 63
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rdbSaleTransfer)
        Me.RadGroupBox6.Controls.Add(Me.rdbStockTransfer)
        Me.RadGroupBox6.Controls.Add(Me.rdbSaleReturn)
        Me.RadGroupBox6.Controls.Add(Me.rdbSale)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(23, 239)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(366, 42)
        Me.RadGroupBox6.TabIndex = 62
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
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rdbInvoice)
        Me.RadGroupBox4.Controls.Add(Me.rdbDispatch)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(23, 191)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox4.TabIndex = 61
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
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.rdbfgsfg)
        Me.RadGroupBox5.Controls.Add(Me.rdbSFG)
        Me.RadGroupBox5.Controls.Add(Me.rdbFG)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(23, 143)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox5.TabIndex = 60
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
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbAll)
        Me.RadGroupBox3.Controls.Add(Me.rdbUnposted)
        Me.RadGroupBox3.Controls.Add(Me.rdbPosted)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(23, 95)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox3.TabIndex = 58
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
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbnCustgroup)
        Me.RadGroupBox2.Controls.Add(Me.rbnPricegroup)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(381, 16)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(219, 36)
        Me.RadGroupBox2.TabIndex = 45
        '
        'rbnCustgroup
        '
        Me.rbnCustgroup.AutoSize = True
        Me.rbnCustgroup.Location = New System.Drawing.Point(103, 9)
        Me.rbnCustgroup.Name = "rbnCustgroup"
        Me.rbnCustgroup.Size = New System.Drawing.Size(110, 17)
        Me.rbnCustgroup.TabIndex = 46
        Me.rbnCustgroup.TabStop = True
        Me.rbnCustgroup.Text = "Customer Group"
        Me.rbnCustgroup.UseVisualStyleBackColor = True
        '
        'rbnPricegroup
        '
        Me.rbnPricegroup.AutoSize = True
        Me.rbnPricegroup.Location = New System.Drawing.Point(14, 9)
        Me.rbnPricegroup.Name = "rbnPricegroup"
        Me.rbnPricegroup.Size = New System.Drawing.Size(85, 17)
        Me.rbnPricegroup.TabIndex = 0
        Me.rbnPricegroup.TabStop = True
        Me.rbnPricegroup.Text = "Price Group"
        Me.rbnPricegroup.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Location"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(23, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(352, 52)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(238, 16)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 5
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-07-2023"
        Me.txtToDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(186, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(71, 16)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 3
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-07-2023"
        Me.txtFromDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 357)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gv1.MyExportFilePath = ""
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(779, 357)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(240, 12)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(62, 17)
        Me.btnPrint.TabIndex = 47
        Me.btnPrint.Text = "Print"
        '
        'btnSplitExport
        '
        Me.btnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnSplitExport.Location = New System.Drawing.Point(143, 12)
        Me.btnSplitExport.Name = "btnSplitExport"
        Me.btnSplitExport.Size = New System.Drawing.Size(95, 17)
        Me.btnSplitExport.TabIndex = 46
        Me.btnSplitExport.Text = "Export"
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
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(79, 12)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(62, 17)
        Me.BtnReset.TabIndex = 44
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(709, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(71, 17)
        Me.btnclose.TabIndex = 45
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(20, 12)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(57, 17)
        Me.btnGo.TabIndex = 43
        Me.btnGo.Text = ">>"
        '
        'rptSalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptSalesReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptSalesReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.rdbSaleTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbStockTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSaleReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rdbInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDispatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.rdbfgsfg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbUnposted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Label1 As Label
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents btnSplitExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents BtnReset As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbnCustgroup As RadioButton
    Friend WithEvents rbnPricegroup As RadioButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents rdbAll As common.Controls.MyRadioButton
    Friend WithEvents rdbUnposted As common.Controls.MyRadioButton
    Friend WithEvents rdbPosted As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents rdbfgsfg As common.Controls.MyRadioButton
    Friend WithEvents rdbSFG As common.Controls.MyRadioButton
    Friend WithEvents rdbFG As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents rdbInvoice As common.Controls.MyRadioButton
    Friend WithEvents rdbDispatch As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents rdbSaleTransfer As common.Controls.MyRadioButton
    Friend WithEvents rdbStockTransfer As common.Controls.MyRadioButton
    Friend WithEvents rdbSaleReturn As common.Controls.MyRadioButton
    Friend WithEvents rdbSale As common.Controls.MyRadioButton
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
End Class
