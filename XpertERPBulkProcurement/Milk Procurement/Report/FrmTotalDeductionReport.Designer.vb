<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTotalDeductionReport
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnAliasNameWise = New common.Controls.MyRadioButton()
        Me.rdbArea = New common.Controls.MyRadioButton()
        Me.MyRadioButton1 = New common.Controls.MyRadioButton()
        Me.rdbBMC = New common.Controls.MyRadioButton()
        Me.rdbBMCDCS = New common.Controls.MyRadioButton()
        Me.rdbDCS = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnMonthly = New System.Windows.Forms.RadioButton()
        Me.rbtnCycleWise = New System.Windows.Forms.RadioButton()
        Me.rbtnDate = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblDCS = New common.Controls.MyLabel()
        Me.TxtDeduction = New common.UserControls.txtMultiSelectFinder()
        Me.txtDCS = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblBMC = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtReportType = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rbtnAliasNameWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbBMCDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 407
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
        Me.RadPageView1.Size = New System.Drawing.Size(800, 387)
        Me.RadPageView1.TabIndex = 25
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 339)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadGroupBox3)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox2)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.MyLabel1)
        Me.RadPanel1.Controls.Add(Me.txtReportType)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(779, 339)
        Me.RadPanel1.TabIndex = 15
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rbtnAliasNameWise)
        Me.RadGroupBox3.Controls.Add(Me.rdbArea)
        Me.RadGroupBox3.Controls.Add(Me.MyRadioButton1)
        Me.RadGroupBox3.Controls.Add(Me.rdbBMC)
        Me.RadGroupBox3.Controls.Add(Me.rdbBMCDCS)
        Me.RadGroupBox3.Controls.Add(Me.rdbDCS)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(391, 46)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(350, 49)
        Me.RadGroupBox3.TabIndex = 448
        '
        'rbtnAliasNameWise
        '
        Me.rbtnAliasNameWise.Location = New System.Drawing.Point(15, 25)
        Me.rbtnAliasNameWise.MyLinkLable1 = Nothing
        Me.rbtnAliasNameWise.MyLinkLable2 = Nothing
        Me.rbtnAliasNameWise.Name = "rbtnAliasNameWise"
        Me.rbtnAliasNameWise.Size = New System.Drawing.Size(104, 18)
        Me.rbtnAliasNameWise.TabIndex = 6
        Me.rbtnAliasNameWise.TabStop = False
        Me.rbtnAliasNameWise.Text = "Alias Name Wise"
        Me.rbtnAliasNameWise.Visible = False
        '
        'rdbArea
        '
        Me.rdbArea.Location = New System.Drawing.Point(152, 5)
        Me.rdbArea.MyLinkLable1 = Nothing
        Me.rdbArea.MyLinkLable2 = Nothing
        Me.rdbArea.Name = "rdbArea"
        Me.rdbArea.Size = New System.Drawing.Size(43, 18)
        Me.rdbArea.TabIndex = 5
        Me.rdbArea.TabStop = False
        Me.rdbArea.Text = "Area"
        '
        'MyRadioButton1
        '
        Me.MyRadioButton1.Location = New System.Drawing.Point(183, 26)
        Me.MyRadioButton1.MyLinkLable1 = Nothing
        Me.MyRadioButton1.MyLinkLable2 = Nothing
        Me.MyRadioButton1.Name = "MyRadioButton1"
        Me.MyRadioButton1.Size = New System.Drawing.Size(110, 18)
        Me.MyRadioButton1.TabIndex = 4
        Me.MyRadioButton1.TabStop = False
        Me.MyRadioButton1.Text = "Month Cycle Wise"
        Me.MyRadioButton1.Visible = False
        '
        'rdbBMC
        '
        Me.rdbBMC.Location = New System.Drawing.Point(69, 5)
        Me.rdbBMC.MyLinkLable1 = Nothing
        Me.rdbBMC.MyLinkLable2 = Nothing
        Me.rdbBMC.Name = "rdbBMC"
        Me.rdbBMC.Size = New System.Drawing.Size(44, 18)
        Me.rdbBMC.TabIndex = 3
        Me.rdbBMC.TabStop = False
        Me.rdbBMC.Text = "BMC"
        '
        'rdbBMCDCS
        '
        Me.rdbBMCDCS.Location = New System.Drawing.Point(108, 28)
        Me.rdbBMCDCS.MyLinkLable1 = Nothing
        Me.rdbBMCDCS.MyLinkLable2 = Nothing
        Me.rdbBMCDCS.Name = "rdbBMCDCS"
        Me.rdbBMCDCS.Size = New System.Drawing.Size(69, 18)
        Me.rdbBMCDCS.TabIndex = 2
        Me.rdbBMCDCS.TabStop = False
        Me.rdbBMCDCS.Text = "BMC/DCS"
        Me.rdbBMCDCS.Visible = False
        '
        'rdbDCS
        '
        Me.rdbDCS.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbDCS.Location = New System.Drawing.Point(5, 5)
        Me.rdbDCS.MyLinkLable1 = Nothing
        Me.rdbDCS.MyLinkLable2 = Nothing
        Me.rdbDCS.Name = "rdbDCS"
        Me.rdbDCS.Size = New System.Drawing.Size(41, 18)
        Me.rdbDCS.TabIndex = 1
        Me.rdbDCS.Text = "DCS"
        Me.rdbDCS.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnMonthly)
        Me.RadGroupBox2.Controls.Add(Me.rbtnCycleWise)
        Me.RadGroupBox2.Controls.Add(Me.rbtnDate)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(391, 13)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(248, 27)
        Me.RadGroupBox2.TabIndex = 447
        '
        'rbtnMonthly
        '
        Me.rbtnMonthly.AutoSize = True
        Me.rbtnMonthly.Location = New System.Drawing.Point(152, 5)
        Me.rbtnMonthly.Name = "rbtnMonthly"
        Me.rbtnMonthly.Size = New System.Drawing.Size(89, 17)
        Me.rbtnMonthly.TabIndex = 440
        Me.rbtnMonthly.Text = "Month-Wise"
        Me.rbtnMonthly.UseVisualStyleBackColor = True
        '
        'rbtnCycleWise
        '
        Me.rbtnCycleWise.AutoSize = True
        Me.rbtnCycleWise.Location = New System.Drawing.Point(69, 5)
        Me.rbtnCycleWise.Name = "rbtnCycleWise"
        Me.rbtnCycleWise.Size = New System.Drawing.Size(80, 17)
        Me.rbtnCycleWise.TabIndex = 439
        Me.rbtnCycleWise.Text = "Cycle-Wise"
        Me.rbtnCycleWise.UseVisualStyleBackColor = True
        '
        'rbtnDate
        '
        Me.rbtnDate.AutoSize = True
        Me.rbtnDate.Location = New System.Drawing.Point(5, 5)
        Me.rbtnDate.Name = "rbtnDate"
        Me.rbtnDate.Size = New System.Drawing.Size(49, 17)
        Me.rbtnDate.TabIndex = 438
        Me.rbtnDate.Text = "Date"
        Me.rbtnDate.UseVisualStyleBackColor = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblDCS)
        Me.RadGroupBox1.Controls.Add(Me.TxtDeduction)
        Me.RadGroupBox1.Controls.Add(Me.txtDCS)
        Me.RadGroupBox1.Controls.Add(Me.lblBMC)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtMCC)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(368, 148)
        Me.RadGroupBox1.TabIndex = 389
        '
        'lblDCS
        '
        Me.lblDCS.FieldName = Nothing
        Me.lblDCS.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCS.Location = New System.Drawing.Point(5, 59)
        Me.lblDCS.Name = "lblDCS"
        Me.lblDCS.Size = New System.Drawing.Size(58, 18)
        Me.lblDCS.TabIndex = 390
        Me.lblDCS.Text = "Deduction"
        '
        'TxtDeduction
        '
        Me.TxtDeduction.arrDispalyMember = Nothing
        Me.TxtDeduction.arrValueMember = Nothing
        Me.TxtDeduction.Location = New System.Drawing.Point(95, 58)
        Me.TxtDeduction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDeduction.MyLinkLable1 = Me.lblDCS
        Me.TxtDeduction.MyLinkLable2 = Nothing
        Me.TxtDeduction.MyNullText = "All"
        Me.TxtDeduction.Name = "TxtDeduction"
        Me.TxtDeduction.Size = New System.Drawing.Size(243, 19)
        Me.TxtDeduction.TabIndex = 389
        '
        'txtDCS
        '
        Me.txtDCS.arrDispalyMember = Nothing
        Me.txtDCS.arrValueMember = Nothing
        Me.txtDCS.Location = New System.Drawing.Point(95, 33)
        Me.txtDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCS.MyLinkLable1 = Me.lblLocation
        Me.txtDCS.MyLinkLable2 = Nothing
        Me.txtDCS.MyNullText = "All"
        Me.txtDCS.Name = "txtDCS"
        Me.txtDCS.Size = New System.Drawing.Size(243, 19)
        Me.txtDCS.TabIndex = 387
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(5, 33)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(27, 18)
        Me.lblLocation.TabIndex = 388
        Me.lblLocation.Text = "DCS"
        '
        'lblBMC
        '
        Me.lblBMC.FieldName = Nothing
        Me.lblBMC.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMC.Location = New System.Drawing.Point(5, 84)
        Me.lblBMC.Name = "lblBMC"
        Me.lblBMC.Size = New System.Drawing.Size(30, 18)
        Me.lblBMC.TabIndex = 356
        Me.lblBMC.Text = "MCC"
        Me.lblBMC.Visible = False
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(95, 83)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.lblBMC
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "All"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(243, 19)
        Me.txtMCC.TabIndex = 5
        Me.txtMCC.Visible = False
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(251, 8)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel4
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(87, 20)
        Me.txtToDate.TabIndex = 361
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "28/06/2012"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(192, 9)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel4.TabIndex = 363
        Me.MyLabel4.Text = "To Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(95, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel3
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(86, 20)
        Me.txtFromDate.TabIndex = 360
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "28/06/2012"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(5, 9)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel3.TabIndex = 364
        Me.MyLabel3.Text = "From Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 190)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel1.TabIndex = 362
        Me.MyLabel1.Text = "Report Type"
        Me.MyLabel1.Visible = False
        '
        'txtReportType
        '
        Me.txtReportType.AutoCompleteDisplayMember = Nothing
        Me.txtReportType.AutoCompleteValueMember = Nothing
        Me.txtReportType.DropDownAnimationEnabled = True
        Me.txtReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.txtReportType.Location = New System.Drawing.Point(101, 190)
        Me.txtReportType.Name = "txtReportType"
        Me.txtReportType.Size = New System.Drawing.Size(243, 20)
        Me.txtReportType.TabIndex = 365
        Me.txtReportType.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 339)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 339)
        Me.gv1.TabIndex = 2
        Me.gv1.VarID = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(800, 20)
        Me.RadMenu1.TabIndex = 24
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(695, 9)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(84, 22)
        Me.RadButton1.TabIndex = 449
        Me.RadButton1.Text = "Close"
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(165, 9)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(83, 22)
        Me.btnExp.TabIndex = 160
        Me.btnExp.Text = "Export"
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
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(16, 9)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 158
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(90, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 159
        Me.btnReset.Text = "Reset"
        '
        'FrmTotalDeductionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTotalDeductionReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmTotalDeductionReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rbtnAliasNameWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbBMCDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReportType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents lblDCS As common.Controls.MyLabel
    Friend WithEvents TxtDeduction As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblBMC As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtReportType As RadDropDownList
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnMonthly As RadioButton
    Friend WithEvents rbtnCycleWise As RadioButton
    Friend WithEvents rbtnDate As RadioButton
    Friend WithEvents btnExp As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents rbtnAliasNameWise As common.Controls.MyRadioButton
    Friend WithEvents rdbArea As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton1 As common.Controls.MyRadioButton
    Friend WithEvents rdbBMC As common.Controls.MyRadioButton
    Friend WithEvents rdbBMCDCS As common.Controls.MyRadioButton
    Friend WithEvents rdbDCS As common.Controls.MyRadioButton
End Class
