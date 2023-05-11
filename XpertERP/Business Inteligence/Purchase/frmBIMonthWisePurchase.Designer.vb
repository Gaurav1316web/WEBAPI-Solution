<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBIMonthWisePurchase
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
        Dim Corners1 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins1 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBIMonthWisePurchase))
        Dim Corners2 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins2 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim Corners3 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins3 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim ChartSeries1 As Telerik.Charting.ChartSeries = New Telerik.Charting.ChartSeries
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.cmbFigure = New common.Controls.MyComboBox
        Me.chkAutoScroll = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.cboOrientation = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.cboSkin = New common.Controls.MyComboBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.cboType = New common.Controls.MyComboBox
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.RadScrollablePanel1 = New Telerik.WinControls.UI.RadScrollablePanel
        Me.RadChart1 = New Telerik.WinControls.UI.RadChart
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.btnExcelChart = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel1.PanelContainer.SuspendLayout()
        Me.RadScrollablePanel1.SuspendLayout()
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnExcelChart)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbFigure)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAutoScroll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboOrientation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboSkin)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadScrollablePanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(805, 317)
        Me.SplitContainer1.SplitterDistance = 69
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel6
        '
        Me.MyLabel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel6.Location = New System.Drawing.Point(508, 6)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel6.TabIndex = 647
        Me.MyLabel6.Text = "Figures In"
        '
        'cmbFigure
        '
        Me.cmbFigure.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFigure.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFigure.Location = New System.Drawing.Point(584, 4)
        Me.cmbFigure.MendatroryField = False
        Me.cmbFigure.MyLinkLable1 = Me.MyLabel6
        Me.cmbFigure.MyLinkLable2 = Nothing
        Me.cmbFigure.Name = "cmbFigure"
        Me.cmbFigure.ShowImageInEditorArea = True
        Me.cmbFigure.Size = New System.Drawing.Size(90, 20)
        Me.cmbFigure.TabIndex = 648
        '
        'chkAutoScroll
        '
        Me.chkAutoScroll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAutoScroll.Location = New System.Drawing.Point(752, 51)
        Me.chkAutoScroll.Name = "chkAutoScroll"
        Me.chkAutoScroll.Size = New System.Drawing.Size(47, 18)
        Me.chkAutoScroll.TabIndex = 15
        Me.chkAutoScroll.Text = "Scroll"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.Location = New System.Drawing.Point(505, 28)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel5.TabIndex = 13
        Me.MyLabel5.Text = "Orientation"
        '
        'cboOrientation
        '
        Me.cboOrientation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOrientation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOrientation.Location = New System.Drawing.Point(584, 27)
        Me.cboOrientation.MendatroryField = False
        Me.cboOrientation.MyLinkLable1 = Me.MyLabel5
        Me.cboOrientation.MyLinkLable2 = Nothing
        Me.cboOrientation.Name = "cboOrientation"
        Me.cboOrientation.ShowImageInEditorArea = True
        Me.cboOrientation.Size = New System.Drawing.Size(90, 20)
        Me.cboOrientation.TabIndex = 14
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.Location = New System.Drawing.Point(685, 29)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel4.TabIndex = 11
        Me.MyLabel4.Text = "Skin"
        '
        'cboSkin
        '
        Me.cboSkin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSkin.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSkin.Location = New System.Drawing.Point(716, 28)
        Me.cboSkin.MendatroryField = False
        Me.cboSkin.MyLinkLable1 = Me.MyLabel4
        Me.cboSkin.MyLinkLable2 = Nothing
        Me.cboSkin.Name = "cboSkin"
        Me.cboSkin.ShowImageInEditorArea = True
        Me.cboSkin.Size = New System.Drawing.Size(83, 20)
        Me.cboSkin.TabIndex = 12
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.Location = New System.Drawing.Point(682, 5)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel3.TabIndex = 9
        Me.MyLabel3.Text = "Type"
        '
        'cboType
        '
        Me.cboType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(716, 4)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.MyLabel3
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ShowImageInEditorArea = True
        Me.cboType.Size = New System.Drawing.Size(83, 20)
        Me.cboType.TabIndex = 10
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(290, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(78, 20)
        Me.RadButton1.TabIndex = 7
        Me.RadButton1.Text = ">>"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "From Month"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(81, 5)
        Me.txtFromDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(67, 20)
        Me.txtFromDate.TabIndex = 5
        Me.txtFromDate.Text = "MyDateTimePicker1"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(155, 6)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 3
        Me.MyLabel1.Text = "To Month"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(217, 5)
        Me.txtToDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel1
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(67, 20)
        Me.txtToDate.TabIndex = 4
        Me.txtToDate.Text = "MyDateTimePicker1"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'RadScrollablePanel1
        '
        Me.RadScrollablePanel1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.RadScrollablePanel1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.RadScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadScrollablePanel1.Name = "RadScrollablePanel1"
        Me.RadScrollablePanel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'RadScrollablePanel1.PanelContainer
        '
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.RadChart1)
        Me.RadScrollablePanel1.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.PanelContainer.Location = New System.Drawing.Point(1, 1)
        Me.RadScrollablePanel1.PanelContainer.Name = "PanelContainer"
        Me.RadScrollablePanel1.PanelContainer.Size = New System.Drawing.Size(803, 242)
        Me.RadScrollablePanel1.PanelContainer.TabIndex = 0
        '
        '
        '
        Me.RadScrollablePanel1.RootElement.Padding = New System.Windows.Forms.Padding(1)
        Me.RadScrollablePanel1.Size = New System.Drawing.Size(805, 244)
        Me.RadScrollablePanel1.TabIndex = 1
        Me.RadScrollablePanel1.Text = "RadScrollablePanel1"
        '
        'RadChart1
        '
        Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadChart1.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(117, Byte), Integer))
        Me.RadChart1.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Gradient
        Me.RadChart1.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.RadChart1.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.RadChart1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.RadChart1.ChartTitle.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Corners1.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners1.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners1.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners1.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.ChartTitle.Appearance.Corners = Corners1
        ChartMargins1.Bottom = CType(resources.GetObject("ChartMargins1.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins1.Left = CType(resources.GetObject("ChartMargins1.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins1.Right = CType(resources.GetObject("ChartMargins1.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins1.Top = CType(resources.GetObject("ChartMargins1.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.ChartTitle.Appearance.Dimensions.Margins = ChartMargins1
        Me.RadChart1.ChartTitle.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.RadChart1.ChartTitle.Appearance.Position.AlignedPosition = Telerik.Charting.Styles.AlignedPositions.Top
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.White
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.DefaultType = Telerik.Charting.ChartSeriesType.Line
        Me.RadChart1.Legend.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Corners2.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners2.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners2.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners2.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.Legend.Appearance.Corners = Corners2
        ChartMargins2.Right = CType(resources.GetObject("ChartMargins2.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins2.Top = CType(resources.GetObject("ChartMargins2.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.Legend.Appearance.Dimensions.Margins = ChartMargins2
        Me.RadChart1.Legend.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.RadChart1.Location = New System.Drawing.Point(54, 3)
        Me.RadChart1.Name = "RadChart1"
        Me.RadChart1.PlotArea.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(93, Byte), Integer))
        Corners3.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners3.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners3.RoundSize = 6
        Corners3.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners3.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.PlotArea.Appearance.Corners = Corners3
        ChartMargins3.Bottom = CType(resources.GetObject("ChartMargins3.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins3.Left = CType(resources.GetObject("ChartMargins3.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins3.Right = CType(resources.GetObject("ChartMargins3.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins3.Top = CType(resources.GetObject("ChartMargins3.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.PlotArea.Appearance.Dimensions.Margins = ChartMargins3
        Me.RadChart1.PlotArea.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.RadChart1.PlotArea.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.DimGray
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorTick.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.XAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.XAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.PlotArea.XAxis.MinValue = 1
        Me.RadChart1.PlotArea.YAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.YAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.PlotArea.YAxis.MaxValue = 90
        Me.RadChart1.PlotArea.YAxis.Step = 10
        Me.RadChart1.PlotArea.YAxis2.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        ChartSeries1.Appearance.Border.Color = System.Drawing.Color.Black
        ChartSeries1.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(178, Byte), Integer))
        ChartSeries1.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(7, Byte), Integer))
        ChartSeries1.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black
        ChartSeries1.Name = "Series 1"
        ChartSeries1.Type = Telerik.Charting.ChartSeriesType.Line
        Me.RadChart1.Series.AddRange(New Telerik.Charting.ChartSeries() {ChartSeries1})
        Me.RadChart1.Size = New System.Drawing.Size(456, 236)
        Me.RadChart1.Skin = "Gradient"
        Me.RadChart1.SkinsOverrideStyles = True
        Me.RadChart1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(716, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 20)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Size = New System.Drawing.Size(805, 346)
        Me.SplitContainer2.SplitterDistance = 317
        Me.SplitContainer2.TabIndex = 1
        '
        'btnExcelChart
        '
        Me.btnExcelChart.Location = New System.Drawing.Point(374, 5)
        Me.btnExcelChart.Name = "btnExcelChart"
        Me.btnExcelChart.Size = New System.Drawing.Size(96, 20)
        Me.btnExcelChart.TabIndex = 653
        Me.btnExcelChart.Text = "Excel Chart"
        '
        'FrmBIMonthWisePurchase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 346)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "FrmBIMonthWisePurchase"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Month Wise Purchase"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.PanelContainer.ResumeLayout(False)
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.ResumeLayout(False)
        Me.RadScrollablePanel1.PerformLayout()
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadChart1 As Telerik.WinControls.UI.RadChart
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents cboSkin As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboOrientation As common.Controls.MyComboBox
    Friend WithEvents chkAutoScroll As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadScrollablePanel1 As Telerik.WinControls.UI.RadScrollablePanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cmbFigure As common.Controls.MyComboBox
    Friend WithEvents btnExcelChart As Telerik.WinControls.UI.RadButton
End Class

