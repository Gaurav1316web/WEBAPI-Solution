<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YearlyBillReport
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
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDeduction = New common.Controls.MyRadioButton()
        Me.rbtnAddition = New common.Controls.MyRadioButton()
        Me.rbtnAll = New common.Controls.MyRadioButton()
        Me.txtMultBMC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMultArea = New common.UserControls.txtMultiSelectFinder()
        Me.lblArea = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnAliasNameWise = New common.Controls.MyRadioButton()
        Me.rdbArea = New common.Controls.MyRadioButton()
        Me.MyRadioButton1 = New common.Controls.MyRadioButton()
        Me.rdbBMC = New common.Controls.MyRadioButton()
        Me.rdbBMCDCS = New common.Controls.MyRadioButton()
        Me.rdbDCS = New common.Controls.MyRadioButton()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbMonthCycle = New common.Controls.MyRadioButton()
        Me.rdbCycleW = New common.Controls.MyRadioButton()
        Me.rdbMonth = New common.Controls.MyRadioButton()
        Me.rdbSummary = New common.Controls.MyRadioButton()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDCS = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox11 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lbltoDate = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnMccWiseExcle = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAddition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnAliasNameWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbBMCDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.rdbMonthCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbCycleW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox11.SuspendLayout()
        CType(Me.lbltoDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnMccWiseExcle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnMccWiseExcle)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 409
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView2
        '
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView2.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView2.Name = "RadPageView2"
        Me.RadPageView2.SelectedPage = Me.RadPageViewPage4
        Me.RadPageView2.Size = New System.Drawing.Size(800, 409)
        Me.RadPageView2.TabIndex = 18
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage4.Controls.Add(Me.txtMultBMC)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.Controls.Add(Me.txtMultArea)
        Me.RadPageViewPage4.Controls.Add(Me.lblArea)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage4.Controls.Add(Me.txtRoute)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage4.Controls.Add(Me.txtDCS)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox11)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(779, 361)
        Me.RadPageViewPage4.Text = "Filters"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnDeduction)
        Me.RadGroupBox2.Controls.Add(Me.rbtnAddition)
        Me.RadGroupBox2.Controls.Add(Me.rbtnAll)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(372, 99)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(404, 35)
        Me.RadGroupBox2.TabIndex = 1509
        '
        'rbtnDeduction
        '
        Me.rbtnDeduction.Location = New System.Drawing.Point(183, 8)
        Me.rbtnDeduction.MyLinkLable1 = Nothing
        Me.rbtnDeduction.MyLinkLable2 = Nothing
        Me.rbtnDeduction.Name = "rbtnDeduction"
        Me.rbtnDeduction.Size = New System.Drawing.Size(72, 18)
        Me.rbtnDeduction.TabIndex = 4
        Me.rbtnDeduction.TabStop = False
        Me.rbtnDeduction.Text = "Deduction"
        '
        'rbtnAddition
        '
        Me.rbtnAddition.Location = New System.Drawing.Point(102, 8)
        Me.rbtnAddition.MyLinkLable1 = Nothing
        Me.rbtnAddition.MyLinkLable2 = Nothing
        Me.rbtnAddition.Name = "rbtnAddition"
        Me.rbtnAddition.Size = New System.Drawing.Size(63, 18)
        Me.rbtnAddition.TabIndex = 3
        Me.rbtnAddition.TabStop = False
        Me.rbtnAddition.Text = "Addition"
        '
        'rbtnAll
        '
        Me.rbtnAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnAll.Location = New System.Drawing.Point(15, 8)
        Me.rbtnAll.MyLinkLable1 = Nothing
        Me.rbtnAll.MyLinkLable2 = Nothing
        Me.rbtnAll.Name = "rbtnAll"
        Me.rbtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAll.TabIndex = 2
        Me.rbtnAll.TabStop = False
        Me.rbtnAll.Text = "All"
        Me.rbtnAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtMultBMC
        '
        Me.txtMultBMC.arrDispalyMember = Nothing
        Me.txtMultBMC.arrValueMember = Nothing
        Me.txtMultBMC.Location = New System.Drawing.Point(107, 78)
        Me.txtMultBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultBMC.MyLinkLable1 = Me.MyLabel2
        Me.txtMultBMC.MyLinkLable2 = Nothing
        Me.txtMultBMC.MyNullText = "All"
        Me.txtMultBMC.Name = "txtMultBMC"
        Me.txtMultBMC.Size = New System.Drawing.Size(259, 19)
        Me.txtMultBMC.TabIndex = 1508
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(16, 99)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel2.TabIndex = 415
        Me.MyLabel2.Text = "DCS Code"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(16, 79)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 1507
        Me.MyLabel3.Text = "BMC"
        '
        'txtMultArea
        '
        Me.txtMultArea.arrDispalyMember = Nothing
        Me.txtMultArea.arrValueMember = Nothing
        Me.txtMultArea.Location = New System.Drawing.Point(107, 57)
        Me.txtMultArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultArea.MyLinkLable1 = Me.MyLabel2
        Me.txtMultArea.MyLinkLable2 = Nothing
        Me.txtMultArea.MyNullText = "All"
        Me.txtMultArea.Name = "txtMultArea"
        Me.txtMultArea.Size = New System.Drawing.Size(259, 19)
        Me.txtMultArea.TabIndex = 1506
        '
        'lblArea
        '
        Me.lblArea.FieldName = Nothing
        Me.lblArea.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArea.Location = New System.Drawing.Point(16, 58)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(30, 16)
        Me.lblArea.TabIndex = 1505
        Me.lblArea.Text = "Area"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnAliasNameWise)
        Me.RadGroupBox1.Controls.Add(Me.rdbArea)
        Me.RadGroupBox1.Controls.Add(Me.MyRadioButton1)
        Me.RadGroupBox1.Controls.Add(Me.rdbBMC)
        Me.RadGroupBox1.Controls.Add(Me.rdbBMCDCS)
        Me.RadGroupBox1.Controls.Add(Me.rdbDCS)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(372, 47)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(404, 49)
        Me.RadGroupBox1.TabIndex = 419
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
        '
        'rdbArea
        '
        Me.rdbArea.Location = New System.Drawing.Point(283, 5)
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
        Me.rdbBMC.Location = New System.Drawing.Point(183, 5)
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
        Me.rdbBMCDCS.Location = New System.Drawing.Point(102, 5)
        Me.rdbBMCDCS.MyLinkLable1 = Nothing
        Me.rdbBMCDCS.MyLinkLable2 = Nothing
        Me.rdbBMCDCS.Name = "rdbBMCDCS"
        Me.rdbBMCDCS.Size = New System.Drawing.Size(69, 18)
        Me.rdbBMCDCS.TabIndex = 2
        Me.rdbBMCDCS.TabStop = False
        Me.rdbBMCDCS.Text = "BMC/DCS"
        '
        'rdbDCS
        '
        Me.rdbDCS.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbDCS.Location = New System.Drawing.Point(15, 5)
        Me.rdbDCS.MyLinkLable1 = Nothing
        Me.rdbDCS.MyLinkLable2 = Nothing
        Me.rdbDCS.Name = "rdbDCS"
        Me.rdbDCS.Size = New System.Drawing.Size(41, 18)
        Me.rdbDCS.TabIndex = 1
        Me.rdbDCS.Text = "DCS"
        Me.rdbDCS.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rdbMonthCycle)
        Me.RadGroupBox6.Controls.Add(Me.rdbCycleW)
        Me.RadGroupBox6.Controls.Add(Me.rdbMonth)
        Me.RadGroupBox6.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(372, 9)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(404, 35)
        Me.RadGroupBox6.TabIndex = 418
        '
        'rdbMonthCycle
        '
        Me.rdbMonthCycle.Location = New System.Drawing.Point(283, 8)
        Me.rdbMonthCycle.MyLinkLable1 = Nothing
        Me.rdbMonthCycle.MyLinkLable2 = Nothing
        Me.rdbMonthCycle.Name = "rdbMonthCycle"
        Me.rdbMonthCycle.Size = New System.Drawing.Size(110, 18)
        Me.rdbMonthCycle.TabIndex = 4
        Me.rdbMonthCycle.TabStop = False
        Me.rdbMonthCycle.Text = "Month Cycle Wise"
        '
        'rdbCycleW
        '
        Me.rdbCycleW.Location = New System.Drawing.Point(183, 8)
        Me.rdbCycleW.MyLinkLable1 = Nothing
        Me.rdbCycleW.MyLinkLable2 = Nothing
        Me.rdbCycleW.Name = "rdbCycleW"
        Me.rdbCycleW.Size = New System.Drawing.Size(74, 18)
        Me.rdbCycleW.TabIndex = 3
        Me.rdbCycleW.TabStop = False
        Me.rdbCycleW.Text = "Cycle Wise"
        '
        'rdbMonth
        '
        Me.rdbMonth.Location = New System.Drawing.Point(102, 8)
        Me.rdbMonth.MyLinkLable1 = Nothing
        Me.rdbMonth.MyLinkLable2 = Nothing
        Me.rdbMonth.Name = "rdbMonth"
        Me.rdbMonth.Size = New System.Drawing.Size(54, 18)
        Me.rdbMonth.TabIndex = 2
        Me.rdbMonth.TabStop = False
        Me.rdbMonth.Text = "Month"
        '
        'rdbSummary
        '
        Me.rdbSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbSummary.Location = New System.Drawing.Point(15, 8)
        Me.rdbSummary.MyLinkLable1 = Nothing
        Me.rdbSummary.MyLinkLable2 = Nothing
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 1
        Me.rdbSummary.Text = "Summary"
        Me.rdbSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(107, 120)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(259, 19)
        Me.txtRoute.TabIndex = 417
        Me.txtRoute.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(16, 119)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel1.TabIndex = 416
        Me.MyLabel1.Text = "Route"
        Me.MyLabel1.Visible = False
        '
        'txtDCS
        '
        Me.txtDCS.arrDispalyMember = Nothing
        Me.txtDCS.arrValueMember = Nothing
        Me.txtDCS.Location = New System.Drawing.Point(107, 99)
        Me.txtDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCS.MyLinkLable1 = Me.MyLabel2
        Me.txtDCS.MyLinkLable2 = Nothing
        Me.txtDCS.MyNullText = "All"
        Me.txtDCS.Name = "txtDCS"
        Me.txtDCS.Size = New System.Drawing.Size(259, 19)
        Me.txtDCS.TabIndex = 414
        '
        'RadGroupBox11
        '
        Me.RadGroupBox11.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox11.Controls.Add(Me.lbltoDate)
        Me.RadGroupBox11.Controls.Add(Me.ToDate)
        Me.RadGroupBox11.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox11.Controls.Add(Me.fromDate)
        Me.RadGroupBox11.HeaderText = ""
        Me.RadGroupBox11.Location = New System.Drawing.Point(16, 9)
        Me.RadGroupBox11.Name = "RadGroupBox11"
        Me.RadGroupBox11.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox11.Size = New System.Drawing.Size(350, 42)
        Me.RadGroupBox11.TabIndex = 53
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
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gv1)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(779, 361)
        Me.RadPageViewPage5.Text = "Detail"
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
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 361)
        Me.gv1.TabIndex = 0
        Me.gv1.VarID = ""
        '
        'btnMccWiseExcle
        '
        Me.btnMccWiseExcle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMccWiseExcle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMccWiseExcle.Location = New System.Drawing.Point(261, 8)
        Me.btnMccWiseExcle.Name = "btnMccWiseExcle"
        Me.btnMccWiseExcle.Size = New System.Drawing.Size(125, 22)
        Me.btnMccWiseExcle.TabIndex = 163
        Me.btnMccWiseExcle.Text = "Mcc Wise Excle"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(160, 8)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 22)
        Me.btnExport.TabIndex = 162
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseCompatibleTextRendering = False
        '
        'btnPDF
        '
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.UseCompatibleTextRendering = False
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(12, 8)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 159
        Me.btnGo.Text = ">>>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(707, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 161
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(86, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 160
        Me.btnReset.Text = "Reset"
        '
        'YearlyBillReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "YearlyBillReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "YearlyBillReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAddition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnAliasNameWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbBMCDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.rdbMonthCycle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbCycleW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox11.ResumeLayout(False)
        Me.RadGroupBox11.PerformLayout()
        CType(Me.lbltoDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnMccWiseExcle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadPageView2 As RadPageView
    Friend WithEvents RadPageViewPage4 As RadPageViewPage
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox11 As RadGroupBox
    Friend WithEvents lbltoDate As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents rdbMonthCycle As common.Controls.MyRadioButton
    Friend WithEvents rdbCycleW As common.Controls.MyRadioButton
    Friend WithEvents rdbMonth As common.Controls.MyRadioButton
    Friend WithEvents rdbSummary As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents MyRadioButton1 As common.Controls.MyRadioButton
    Friend WithEvents rdbBMC As common.Controls.MyRadioButton
    Friend WithEvents rdbBMCDCS As common.Controls.MyRadioButton
    Friend WithEvents rdbDCS As common.Controls.MyRadioButton
    Friend WithEvents rdbArea As common.Controls.MyRadioButton
    Friend WithEvents rbtnAliasNameWise As common.Controls.MyRadioButton
    Friend WithEvents txtMultBMC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtMultArea As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblArea As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnDeduction As common.Controls.MyRadioButton
    Friend WithEvents rbtnAddition As common.Controls.MyRadioButton
    Friend WithEvents rbtnAll As common.Controls.MyRadioButton
    Friend WithEvents btnMccWiseExcle As RadButton
End Class
