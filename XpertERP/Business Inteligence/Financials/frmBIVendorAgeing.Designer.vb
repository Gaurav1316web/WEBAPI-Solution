<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBIVendorAgeing
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
        Dim Corners7 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins7 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBIVendorAgeing))
        Dim Corners8 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins8 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim Corners9 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins9 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim ChartSeries3 As Telerik.Charting.ChartSeries = New Telerik.Charting.ChartSeries
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnExcelChart = New Telerik.WinControls.UI.RadButton
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.cmbFigure = New common.Controls.MyComboBox
        Me.chkAutoScroll = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.cboOrientation = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.cboSkin = New common.Controls.MyComboBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.cboType = New common.Controls.MyComboBox
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadScrollablePanel1 = New Telerik.WinControls.UI.RadScrollablePanel
        Me.RadChart1 = New Telerik.WinControls.UI.RadChart
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvDetails = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.txtCurrencyCode = New common.UserControls.txtFinder
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel1.PanelContainer.SuspendLayout()
        Me.RadScrollablePanel1.SuspendLayout()
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCurrencyCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnExcelChart)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbFigure)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAutoScroll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboOrientation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboSkin)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(887, 466)
        Me.SplitContainer1.SplitterDistance = 126
        Me.SplitContainer1.TabIndex = 4
        '
        'btnExcelChart
        '
        Me.btnExcelChart.Location = New System.Drawing.Point(513, 100)
        Me.btnExcelChart.Name = "btnExcelChart"
        Me.btnExcelChart.Size = New System.Drawing.Size(96, 20)
        Me.btnExcelChart.TabIndex = 653
        Me.btnExcelChart.Text = "Excel Chart"
        '
        'MyLabel1
        '
        Me.MyLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel1.Location = New System.Drawing.Point(587, 6)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel1.TabIndex = 647
        Me.MyLabel1.Text = "Figures in "
        '
        'cmbFigure
        '
        Me.cmbFigure.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFigure.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFigure.Location = New System.Drawing.Point(666, 4)
        Me.cmbFigure.MendatroryField = False
        Me.cmbFigure.MyLinkLable1 = Me.MyLabel1
        Me.cmbFigure.MyLinkLable2 = Nothing
        Me.cmbFigure.Name = "cmbFigure"
        Me.cmbFigure.Size = New System.Drawing.Size(90, 20)
        Me.cmbFigure.TabIndex = 648
        '
        'chkAutoScroll
        '
        Me.chkAutoScroll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAutoScroll.Location = New System.Drawing.Point(837, 51)
        Me.chkAutoScroll.Name = "chkAutoScroll"
        Me.chkAutoScroll.Size = New System.Drawing.Size(47, 18)
        Me.chkAutoScroll.TabIndex = 633
        Me.chkAutoScroll.Text = "Scroll"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.Location = New System.Drawing.Point(585, 26)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel5.TabIndex = 631
        Me.MyLabel5.Text = "Orientation"
        '
        'cboOrientation
        '
        Me.cboOrientation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOrientation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOrientation.Location = New System.Drawing.Point(666, 26)
        Me.cboOrientation.MendatroryField = False
        Me.cboOrientation.MyLinkLable1 = Me.MyLabel5
        Me.cboOrientation.MyLinkLable2 = Nothing
        Me.cboOrientation.Name = "cboOrientation"
        Me.cboOrientation.Size = New System.Drawing.Size(90, 20)
        Me.cboOrientation.TabIndex = 632
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.Location = New System.Drawing.Point(768, 26)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel4.TabIndex = 629
        Me.MyLabel4.Text = "Skin"
        '
        'cboSkin
        '
        Me.cboSkin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSkin.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSkin.Location = New System.Drawing.Point(801, 26)
        Me.cboSkin.MendatroryField = False
        Me.cboSkin.MyLinkLable1 = Me.MyLabel4
        Me.cboSkin.MyLinkLable2 = Nothing
        Me.cboSkin.Name = "cboSkin"
        Me.cboSkin.Size = New System.Drawing.Size(83, 20)
        Me.cboSkin.TabIndex = 630
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.Location = New System.Drawing.Point(768, 5)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel3.TabIndex = 627
        Me.MyLabel3.Text = "Type"
        '
        'cboType
        '
        Me.cboType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(801, 4)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.MyLabel3
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(83, 20)
        Me.cboType.TabIndex = 628
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(411, 100)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(96, 20)
        Me.btnRefresh.TabIndex = 625
        Me.btnRefresh.Text = ">>"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel2.Location = New System.Drawing.Point(411, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel2.TabIndex = 624
        Me.MyLabel2.Text = "As on date"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(491, 74)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.MyLabel2
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(90, 20)
        Me.txtDate.TabIndex = 623
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "28/06/2012"
        Me.txtDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox1.HeaderText = "Vendors"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(400, 122)
        Me.RadGroupBox1.TabIndex = 610
        Me.RadGroupBox1.Text = "Vendors"
        '
        'cbgVendor
        '
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 20)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(380, 92)
        Me.cbgVendor.TabIndex = 2
        Me.cbgVendor.ValueMember = "Code"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Size = New System.Drawing.Size(887, 336)
        Me.SplitContainer2.SplitterDistance = 307
        Me.SplitContainer2.TabIndex = 320
        '
        'RadPageView2
        '
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView2.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView2.Name = "RadPageView2"
        Me.RadPageView2.SelectedPage = Me.RadPageViewPage4
        Me.RadPageView2.Size = New System.Drawing.Size(887, 307)
        Me.RadPageView2.TabIndex = 319
        Me.RadPageView2.Text = "RadPageView2"
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadScrollablePanel1)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(866, 259)
        Me.RadPageViewPage4.Text = "Filter"
        '
        'RadScrollablePanel1
        '
        Me.RadScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadScrollablePanel1.Name = "RadScrollablePanel1"
        '
        'RadScrollablePanel1.PanelContainer
        '
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.RadChart1)
        Me.RadScrollablePanel1.PanelContainer.Size = New System.Drawing.Size(864, 257)
        Me.RadScrollablePanel1.Size = New System.Drawing.Size(866, 259)
        Me.RadScrollablePanel1.TabIndex = 623
        Me.RadScrollablePanel1.Text = "RadScrollablePanel1"
        '
        'RadChart1
        '
        Me.RadChart1.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.RadChart1.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        Me.RadChart1.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.RadChart1.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.RadChart1.ChartTitle.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Corners7.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners7.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners7.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners7.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.ChartTitle.Appearance.Corners = Corners7
        ChartMargins7.Bottom = CType(resources.GetObject("ChartMargins7.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins7.Left = CType(resources.GetObject("ChartMargins7.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins7.Right = CType(resources.GetObject("ChartMargins7.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins7.Top = CType(resources.GetObject("ChartMargins7.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.ChartTitle.Appearance.Dimensions.Margins = ChartMargins7
        Me.RadChart1.ChartTitle.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.RadChart1.ChartTitle.Appearance.Position.AlignedPosition = Telerik.Charting.Styles.AlignedPositions.Top
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.White
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.Legend.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Corners8.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners8.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners8.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners8.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.Legend.Appearance.Corners = Corners8
        ChartMargins8.Right = CType(resources.GetObject("ChartMargins8.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins8.Top = CType(resources.GetObject("ChartMargins8.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.Legend.Appearance.Dimensions.Margins = ChartMargins8
        Me.RadChart1.Legend.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.RadChart1.Location = New System.Drawing.Point(0, 0)
        Me.RadChart1.Name = "RadChart1"
        Me.RadChart1.PlotArea.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(93, Byte), Integer))
        Corners9.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners9.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners9.RoundSize = 6
        Corners9.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners9.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.PlotArea.Appearance.Corners = Corners9
        ChartMargins9.Bottom = CType(resources.GetObject("ChartMargins9.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins9.Left = CType(resources.GetObject("ChartMargins9.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins9.Right = CType(resources.GetObject("ChartMargins9.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins9.Top = CType(resources.GetObject("ChartMargins9.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.PlotArea.Appearance.Dimensions.Margins = ChartMargins9
        Me.RadChart1.PlotArea.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.RadChart1.PlotArea.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.DimGray
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorTick.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.XAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.XAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.PlotArea.XAxis.MinValue = 1
        Me.RadChart1.PlotArea.YAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.YAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.PlotArea.YAxis.MaxValue = 100
        Me.RadChart1.PlotArea.YAxis.Step = 10
        Me.RadChart1.PlotArea.YAxis2.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        ChartSeries3.Appearance.Border.Color = System.Drawing.Color.Black
        ChartSeries3.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(178, Byte), Integer))
        ChartSeries3.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(7, Byte), Integer))
        ChartSeries3.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black
        ChartSeries3.Name = "Series 1"
        Me.RadChart1.Series.AddRange(New Telerik.Charting.ChartSeries() {ChartSeries3})
        Me.RadChart1.Size = New System.Drawing.Size(864, 257)
        Me.RadChart1.Skin = "Gradient"
        Me.RadChart1.SkinsOverrideStyles = True
        Me.RadChart1.TabIndex = 622
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gvDetails)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(866, 259)
        Me.RadPageViewPage5.Text = "Details"
        '
        'gvDetails
        '
        Me.gvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetails.Location = New System.Drawing.Point(0, 0)
        '
        'gvDetails
        '
        Me.gvDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvDetails.MasterTemplate.AllowEditRow = False
        Me.gvDetails.MasterTemplate.EnableFiltering = True
        Me.gvDetails.Name = "gvDetails"
        Me.gvDetails.ShowGroupPanel = False
        Me.gvDetails.Size = New System.Drawing.Size(866, 259)
        Me.gvDetails.TabIndex = 0
        Me.gvDetails.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(801, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 20)
        Me.btnClose.TabIndex = 626
        Me.btnClose.Text = "Close"
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.Location = New System.Drawing.Point(666, 52)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Me.MyLabel6
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.Size = New System.Drawing.Size(135, 19)
        Me.txtCurrencyCode.TabIndex = 654
        Me.txtCurrencyCode.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(587, 53)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel6.TabIndex = 655
        Me.MyLabel6.Text = "Currency"
        '
        'FrmBIVendorAgeing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 466)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBIVendorAgeing"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Ageing"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadScrollablePanel1.PanelContainer.ResumeLayout(False)
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.ResumeLayout(False)
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents RadPageView2 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadChart1 As Telerik.WinControls.UI.RadChart
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDetails As common.UserControls.MyRadGridView
    Friend WithEvents chkAutoScroll As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboOrientation As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents cboSkin As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cmbFigure As common.Controls.MyComboBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadScrollablePanel1 As Telerik.WinControls.UI.RadScrollablePanel
    Friend WithEvents btnExcelChart As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
End Class

