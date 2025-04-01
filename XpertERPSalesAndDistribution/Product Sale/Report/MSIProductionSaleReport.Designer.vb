<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MSIProductionSaleReport
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Prdncreallchk = New common.Controls.MyRadioButton()
        Me.RePrdntchk = New common.Controls.MyRadioButton()
        Me.Productionchk = New common.Controls.MyRadioButton()
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
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.FromDate = New common.Controls.MyDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.Prdncreallchk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RePrdntchk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Productionchk, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.FromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 413)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.Label3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 365)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.Prdncreallchk)
        Me.RadGroupBox7.Controls.Add(Me.RePrdntchk)
        Me.RadGroupBox7.Controls.Add(Me.Productionchk)
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(16, 292)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox7.TabIndex = 60
        '
        'Prdncreallchk
        '
        Me.Prdncreallchk.Location = New System.Drawing.Point(202, 11)
        Me.Prdncreallchk.MyLinkLable1 = Nothing
        Me.Prdncreallchk.MyLinkLable2 = Nothing
        Me.Prdncreallchk.Name = "Prdncreallchk"
        Me.Prdncreallchk.Size = New System.Drawing.Size(33, 18)
        Me.Prdncreallchk.TabIndex = 3
        Me.Prdncreallchk.TabStop = False
        Me.Prdncreallchk.Text = "All"
        '
        'RePrdntchk
        '
        Me.RePrdntchk.Location = New System.Drawing.Point(97, 11)
        Me.RePrdntchk.MyLinkLable1 = Nothing
        Me.RePrdntchk.MyLinkLable2 = Nothing
        Me.RePrdntchk.Name = "RePrdntchk"
        Me.RePrdntchk.Size = New System.Drawing.Size(92, 18)
        Me.RePrdntchk.TabIndex = 2
        Me.RePrdntchk.TabStop = False
        Me.RePrdntchk.Text = "Re-Production"
        '
        'Productionchk
        '
        Me.Productionchk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Productionchk.Location = New System.Drawing.Point(16, 11)
        Me.Productionchk.MyLinkLable1 = Nothing
        Me.Productionchk.MyLinkLable2 = Nothing
        Me.Productionchk.Name = "Productionchk"
        Me.Productionchk.Size = New System.Drawing.Size(75, 18)
        Me.Productionchk.TabIndex = 1
        Me.Productionchk.Text = "Production"
        Me.Productionchk.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rdbSaleTransfer)
        Me.RadGroupBox6.Controls.Add(Me.rdbStockTransfer)
        Me.RadGroupBox6.Controls.Add(Me.rdbSaleReturn)
        Me.RadGroupBox6.Controls.Add(Me.rdbSale)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(16, 244)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(366, 42)
        Me.RadGroupBox6.TabIndex = 59
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
        Me.RadGroupBox5.Location = New System.Drawing.Point(16, 196)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox5.TabIndex = 58
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
        Me.RadGroupBox4.Location = New System.Drawing.Point(16, 148)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox4.TabIndex = 57
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(16, 100)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox2.TabIndex = 56
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
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(179, 76)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(83, 18)
        Me.lblLocation.TabIndex = 47
        Me.lblLocation.TextWrap = False
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(76, 76)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(99, 18)
        Me.txtLocation.TabIndex = 46
        Me.txtLocation.Value = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Location"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.FromDate)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(16, 15)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(246, 46)
        Me.RadGroupBox1.TabIndex = 44
        '
        'FromDate
        '
        Me.FromDate.CalculationExpression = Nothing
        Me.FromDate.CustomFormat = "dd-MM-yyyy"
        Me.FromDate.FieldCode = Nothing
        Me.FromDate.FieldDesc = Nothing
        Me.FromDate.FieldMaxLength = 0
        Me.FromDate.FieldName = Nothing
        Me.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.FromDate.isCalculatedField = False
        Me.FromDate.IsSourceFromTable = False
        Me.FromDate.IsSourceFromValueList = False
        Me.FromDate.IsUnique = False
        Me.FromDate.Location = New System.Drawing.Point(46, 13)
        Me.FromDate.MendatroryField = False
        Me.FromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.FromDate.MyLinkLable1 = Nothing
        Me.FromDate.MyLinkLable2 = Nothing
        Me.FromDate.Name = "FromDate"
        Me.FromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.FromDate.ReferenceFieldDesc = Nothing
        Me.FromDate.ReferenceFieldName = Nothing
        Me.FromDate.ReferenceTableName = Nothing
        Me.FromDate.Size = New System.Drawing.Size(82, 20)
        Me.FromDate.TabIndex = 8
        Me.FromDate.TabStop = False
        Me.FromDate.Text = "13-07-2023"
        Me.FromDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Date"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(76, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(67, 17)
        Me.btnPrint.TabIndex = 60
        Me.btnPrint.Text = "Print"
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(12, 8)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(62, 17)
        Me.BtnReset.TabIndex = 58
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(710, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(71, 17)
        Me.btnclose.TabIndex = 59
        Me.btnclose.Text = "Close"
        '
        'MSIProductionSaleReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "MSIProductionSaleReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MSIProductionSaleReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.Prdncreallchk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RePrdntchk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Productionchk, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.FromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents Label3 As Label
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents FromDate As common.Controls.MyDateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents BtnReset As RadButton
    Friend WithEvents btnclose As RadButton
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
    Friend WithEvents rdbSaleTransfer As common.Controls.MyRadioButton
    Friend WithEvents rdbStockTransfer As common.Controls.MyRadioButton
    Friend WithEvents rdbSaleReturn As common.Controls.MyRadioButton
    Friend WithEvents rdbSale As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox7 As RadGroupBox
    Friend WithEvents Prdncreallchk As common.Controls.MyRadioButton
    Friend WithEvents RePrdntchk As common.Controls.MyRadioButton
    Friend WithEvents Productionchk As common.Controls.MyRadioButton
End Class
