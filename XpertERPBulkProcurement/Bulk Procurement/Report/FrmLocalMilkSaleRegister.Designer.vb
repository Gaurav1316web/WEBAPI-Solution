<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLocalMilkSaleRegister
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnCow = New common.Controls.MyRadioButton()
        Me.rbtnbuffalo = New common.Controls.MyRadioButton()
        Me.rbtnMix = New common.Controls.MyRadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnMorning = New common.Controls.MyRadioButton()
        Me.rbtnEvening = New common.Controls.MyRadioButton()
        Me.rbtnBothShift = New common.Controls.MyRadioButton()
        Me.lblMilkType = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.rbtnDateShiftType = New common.Controls.MyRadioButton()
        Me.rbtnDateMilkType = New common.Controls.MyRadioButton()
        Me.rbtnDCSWise = New common.Controls.MyRadioButton()
        Me.rbtnBMCWise = New common.Controls.MyRadioButton()
        Me.rbtnDetails = New common.Controls.MyRadioButton()
        Me.TxtMultiDCS = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMultiBMC = New common.UserControls.txtMultiSelectFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.rddlShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.rddlMilk = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnCow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnbuffalo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBothShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDateShiftType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDateMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDCSWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBMCWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rddlShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rddlMilk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 412
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 412)
        Me.RadPageView1.TabIndex = 12
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.rddlMilk)
        Me.RadPageViewPage1.Controls.Add(Me.rddlShift)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.lblMilkType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnDateShiftType)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnDateMilkType)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnDCSWise)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnBMCWise)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnDetails)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiDCS)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiBMC)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 364)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnCow)
        Me.RadGroupBox1.Controls.Add(Me.rbtnbuffalo)
        Me.RadGroupBox1.Controls.Add(Me.rbtnMix)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(103, 323)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(303, 38)
        Me.RadGroupBox1.TabIndex = 1530
        Me.RadGroupBox1.Visible = False
        '
        'rbtnCow
        '
        Me.rbtnCow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnCow.Location = New System.Drawing.Point(5, 9)
        Me.rbtnCow.MyLinkLable1 = Nothing
        Me.rbtnCow.MyLinkLable2 = Nothing
        Me.rbtnCow.Name = "rbtnCow"
        Me.rbtnCow.Size = New System.Drawing.Size(42, 18)
        Me.rbtnCow.TabIndex = 393
        Me.rbtnCow.Text = "Cow"
        Me.rbtnCow.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnbuffalo
        '
        Me.rbtnbuffalo.Location = New System.Drawing.Point(95, 9)
        Me.rbtnbuffalo.MyLinkLable1 = Nothing
        Me.rbtnbuffalo.MyLinkLable2 = Nothing
        Me.rbtnbuffalo.Name = "rbtnbuffalo"
        Me.rbtnbuffalo.Size = New System.Drawing.Size(55, 18)
        Me.rbtnbuffalo.TabIndex = 393
        Me.rbtnbuffalo.TabStop = False
        Me.rbtnbuffalo.Text = "Buffalo"
        '
        'rbtnMix
        '
        Me.rbtnMix.Location = New System.Drawing.Point(212, 9)
        Me.rbtnMix.MyLinkLable1 = Nothing
        Me.rbtnMix.MyLinkLable2 = Nothing
        Me.rbtnMix.Name = "rbtnMix"
        Me.rbtnMix.Size = New System.Drawing.Size(38, 18)
        Me.rbtnMix.TabIndex = 395
        Me.rbtnMix.TabStop = False
        Me.rbtnMix.Text = "Mix"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.rbtnMorning)
        Me.RadGroupBox5.Controls.Add(Me.rbtnEvening)
        Me.RadGroupBox5.Controls.Add(Me.rbtnBothShift)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(103, 285)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(303, 32)
        Me.RadGroupBox5.TabIndex = 1529
        Me.RadGroupBox5.Visible = False
        '
        'rbtnMorning
        '
        Me.rbtnMorning.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnMorning.Location = New System.Drawing.Point(5, 9)
        Me.rbtnMorning.MyLinkLable1 = Nothing
        Me.rbtnMorning.MyLinkLable2 = Nothing
        Me.rbtnMorning.Name = "rbtnMorning"
        Me.rbtnMorning.Size = New System.Drawing.Size(63, 18)
        Me.rbtnMorning.TabIndex = 393
        Me.rbtnMorning.Text = "Morning"
        Me.rbtnMorning.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnEvening
        '
        Me.rbtnEvening.Location = New System.Drawing.Point(95, 9)
        Me.rbtnEvening.MyLinkLable1 = Nothing
        Me.rbtnEvening.MyLinkLable2 = Nothing
        Me.rbtnEvening.Name = "rbtnEvening"
        Me.rbtnEvening.Size = New System.Drawing.Size(59, 18)
        Me.rbtnEvening.TabIndex = 393
        Me.rbtnEvening.TabStop = False
        Me.rbtnEvening.Text = "Evening"
        '
        'rbtnBothShift
        '
        Me.rbtnBothShift.Location = New System.Drawing.Point(212, 9)
        Me.rbtnBothShift.MyLinkLable1 = Nothing
        Me.rbtnBothShift.MyLinkLable2 = Nothing
        Me.rbtnBothShift.Name = "rbtnBothShift"
        Me.rbtnBothShift.Size = New System.Drawing.Size(44, 18)
        Me.rbtnBothShift.TabIndex = 395
        Me.rbtnBothShift.TabStop = False
        Me.rbtnBothShift.Text = "Both"
        '
        'lblMilkType
        '
        Me.lblMilkType.FieldName = Nothing
        Me.lblMilkType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMilkType.Location = New System.Drawing.Point(21, 120)
        Me.lblMilkType.Margin = New System.Windows.Forms.Padding(4)
        Me.lblMilkType.Name = "lblMilkType"
        Me.lblMilkType.Size = New System.Drawing.Size(55, 18)
        Me.lblMilkType.TabIndex = 1526
        Me.lblMilkType.Text = "Milk Type"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(21, 96)
        Me.MyLabel2.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel2.TabIndex = 1525
        Me.MyLabel2.Text = "Shift Type"
        '
        'rbtnDateShiftType
        '
        Me.rbtnDateShiftType.Location = New System.Drawing.Point(259, 172)
        Me.rbtnDateShiftType.MyLinkLable1 = Nothing
        Me.rbtnDateShiftType.MyLinkLable2 = Nothing
        Me.rbtnDateShiftType.Name = "rbtnDateShiftType"
        Me.rbtnDateShiftType.Size = New System.Drawing.Size(146, 18)
        Me.rbtnDateShiftType.TabIndex = 1522
        Me.rbtnDateShiftType.TabStop = False
        Me.rbtnDateShiftType.Text = "Date and Shift Type Wise"
        '
        'rbtnDateMilkType
        '
        Me.rbtnDateMilkType.Location = New System.Drawing.Point(108, 172)
        Me.rbtnDateMilkType.MyLinkLable1 = Nothing
        Me.rbtnDateMilkType.MyLinkLable2 = Nothing
        Me.rbtnDateMilkType.Name = "rbtnDateMilkType"
        Me.rbtnDateMilkType.Size = New System.Drawing.Size(144, 18)
        Me.rbtnDateMilkType.TabIndex = 1521
        Me.rbtnDateMilkType.TabStop = False
        Me.rbtnDateMilkType.Text = "Date and Milk Type Wise"
        '
        'rbtnDCSWise
        '
        Me.rbtnDCSWise.Location = New System.Drawing.Point(317, 144)
        Me.rbtnDCSWise.MyLinkLable1 = Nothing
        Me.rbtnDCSWise.MyLinkLable2 = Nothing
        Me.rbtnDCSWise.Name = "rbtnDCSWise"
        Me.rbtnDCSWise.Size = New System.Drawing.Size(65, 18)
        Me.rbtnDCSWise.TabIndex = 1520
        Me.rbtnDCSWise.TabStop = False
        Me.rbtnDCSWise.Text = "Dcs Wise"
        '
        'rbtnBMCWise
        '
        Me.rbtnBMCWise.Location = New System.Drawing.Point(203, 144)
        Me.rbtnBMCWise.MyLinkLable1 = Nothing
        Me.rbtnBMCWise.MyLinkLable2 = Nothing
        Me.rbtnBMCWise.Name = "rbtnBMCWise"
        Me.rbtnBMCWise.Size = New System.Drawing.Size(71, 18)
        Me.rbtnBMCWise.TabIndex = 1519
        Me.rbtnBMCWise.TabStop = False
        Me.rbtnBMCWise.Text = "BMC Wise"
        '
        'rbtnDetails
        '
        Me.rbtnDetails.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnDetails.Location = New System.Drawing.Point(108, 144)
        Me.rbtnDetails.MyLinkLable1 = Nothing
        Me.rbtnDetails.MyLinkLable2 = Nothing
        Me.rbtnDetails.Name = "rbtnDetails"
        Me.rbtnDetails.Size = New System.Drawing.Size(54, 18)
        Me.rbtnDetails.TabIndex = 1518
        Me.rbtnDetails.Text = "Details"
        Me.rbtnDetails.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'TxtMultiDCS
        '
        Me.TxtMultiDCS.arrDispalyMember = Nothing
        Me.TxtMultiDCS.arrValueMember = Nothing
        Me.TxtMultiDCS.Location = New System.Drawing.Point(103, 65)
        Me.TxtMultiDCS.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtMultiDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiDCS.MyLinkLable1 = Me.MyLabel13
        Me.TxtMultiDCS.MyLinkLable2 = Nothing
        Me.TxtMultiDCS.MyNullText = "All"
        Me.TxtMultiDCS.Name = "TxtMultiDCS"
        Me.TxtMultiDCS.Size = New System.Drawing.Size(306, 20)
        Me.TxtMultiDCS.TabIndex = 1517
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(21, 42)
        Me.MyLabel13.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel13.TabIndex = 1514
        Me.MyLabel13.Text = "MCC Code"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(21, 70)
        Me.MyLabel1.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel1.TabIndex = 1516
        Me.MyLabel1.Text = "DCS Code"
        '
        'txtMultiBMC
        '
        Me.txtMultiBMC.arrDispalyMember = Nothing
        Me.txtMultiBMC.arrValueMember = Nothing
        Me.txtMultiBMC.Location = New System.Drawing.Point(103, 40)
        Me.txtMultiBMC.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMultiBMC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiBMC.MyLinkLable1 = Me.MyLabel13
        Me.txtMultiBMC.MyLinkLable2 = Nothing
        Me.txtMultiBMC.MyNullText = "All"
        Me.txtMultiBMC.Name = "txtMultiBMC"
        Me.txtMultiBMC.Size = New System.Drawing.Size(306, 20)
        Me.txtMultiBMC.TabIndex = 1515
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(194, 13)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(21, 19)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(221, 13)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(78, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(104, 13)
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(78, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "24/10/2011"
        Me.txtFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(794, 278)
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
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(794, 278)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(704, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 159
        Me.btnClose.Text = "Close"
        '
        'btnExp
        '
        Me.btnExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(157, 9)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(83, 22)
        Me.btnExp.TabIndex = 158
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
        Me.btnGo.Location = New System.Drawing.Point(5, 9)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 153
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(80, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 154
        Me.btnReset.Text = "Reset"
        '
        'rddlShift
        '
        Me.rddlShift.AutoCompleteDisplayMember = Nothing
        Me.rddlShift.AutoCompleteValueMember = Nothing
        Me.rddlShift.DropDownAnimationEnabled = True
        Me.rddlShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem4.Text = "Morning"
        RadListDataItem5.Text = "Evening"
        RadListDataItem6.Text = "Both"
        Me.rddlShift.Items.Add(RadListDataItem4)
        Me.rddlShift.Items.Add(RadListDataItem5)
        Me.rddlShift.Items.Add(RadListDataItem6)
        Me.rddlShift.Location = New System.Drawing.Point(103, 92)
        Me.rddlShift.Name = "rddlShift"
        Me.rddlShift.Size = New System.Drawing.Size(194, 20)
        Me.rddlShift.TabIndex = 1531
        '
        'rddlMilk
        '
        Me.rddlMilk.AutoCompleteDisplayMember = Nothing
        Me.rddlMilk.AutoCompleteValueMember = Nothing
        Me.rddlMilk.DropDownAnimationEnabled = True
        Me.rddlMilk.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Cow"
        RadListDataItem2.Text = "Buffalo"
        RadListDataItem3.Text = "Mix"
        Me.rddlMilk.Items.Add(RadListDataItem1)
        Me.rddlMilk.Items.Add(RadListDataItem2)
        Me.rddlMilk.Items.Add(RadListDataItem3)
        Me.rddlMilk.Location = New System.Drawing.Point(103, 118)
        Me.rddlMilk.Name = "rddlMilk"
        Me.rddlMilk.Size = New System.Drawing.Size(194, 20)
        Me.rddlMilk.TabIndex = 1532
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(21, 146)
        Me.MyLabel3.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel3.TabIndex = 1533
        Me.MyLabel3.Text = "Report Type"
        '
        'FrmLocalMilkSaleRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmLocalMilkSaleRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmLocalMilkSaleRegister"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnCow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnbuffalo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBothShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDateShiftType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDateMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDCSWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBMCWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rddlShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rddlMilk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents TxtMultiDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMultiBMC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtToDate As RadDateTimePicker
    Friend WithEvents txtFromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents rbtnBMCWise As common.Controls.MyRadioButton
    Friend WithEvents rbtnDetails As common.Controls.MyRadioButton
    Friend WithEvents rbtnDateMilkType As common.Controls.MyRadioButton
    Friend WithEvents rbtnDCSWise As common.Controls.MyRadioButton
    Friend WithEvents rbtnDateShiftType As common.Controls.MyRadioButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblMilkType As common.Controls.MyLabel
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents rbtnMorning As common.Controls.MyRadioButton
    Friend WithEvents rbtnEvening As common.Controls.MyRadioButton
    Friend WithEvents rbtnBothShift As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbtnCow As common.Controls.MyRadioButton
    Friend WithEvents rbtnbuffalo As common.Controls.MyRadioButton
    Friend WithEvents rbtnMix As common.Controls.MyRadioButton
    Friend WithEvents btnExp As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnClose As RadButton
    Friend WithEvents rddlShift As RadDropDownList
    Friend WithEvents rddlMilk As RadDropDownList
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class
