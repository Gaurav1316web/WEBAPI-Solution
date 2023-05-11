<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGraphicalBatchOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGraphicalBatchOrder))
        Dim Corners2 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins2 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim Corners3 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins3 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim ChartSeries1 As Telerik.Charting.ChartSeries = New Telerik.Charting.ChartSeries
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadScrollablePanel1 = New Telerik.WinControls.UI.RadScrollablePanel
        Me.RadChart1 = New Telerik.WinControls.UI.RadChart
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.cmbFigure = New common.Controls.MyComboBox
        Me.chkAutoScroll = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.cboOrientation = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.cboSkin = New common.Controls.MyComboBox
        Me.lblFromDate = New common.Controls.MyLabel
        Me.lblToDate = New common.Controls.MyLabel
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker
        Me.cboType = New common.Controls.MyComboBox
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.btnExcelChart = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel1.PanelContainer.SuspendLayout()
        Me.RadScrollablePanel1.SuspendLayout()
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(855, 347)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadScrollablePanel1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(834, 299)
        Me.RadPageViewPage1.Text = "Graph"
        '
        'RadScrollablePanel1
        '
        Me.RadScrollablePanel1.AutoScroll = True
        Me.RadScrollablePanel1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.RadScrollablePanel1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.RadScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadScrollablePanel1.Name = "RadScrollablePanel1"
        Me.RadScrollablePanel1.Padding = New System.Windows.Forms.Padding(1)
        '
        'RadScrollablePanel1.PanelContainer
        '
        Me.RadScrollablePanel1.PanelContainer.AutoScroll = True
        Me.RadScrollablePanel1.PanelContainer.Controls.Add(Me.RadChart1)
        Me.RadScrollablePanel1.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel1.PanelContainer.Location = New System.Drawing.Point(1, 1)
        Me.RadScrollablePanel1.PanelContainer.Name = "PanelContainer"
        Me.RadScrollablePanel1.PanelContainer.Size = New System.Drawing.Size(815, 280)
        Me.RadScrollablePanel1.PanelContainer.TabIndex = 0
        '
        '
        '
        Me.RadScrollablePanel1.RootElement.Padding = New System.Windows.Forms.Padding(1)
        Me.RadScrollablePanel1.Size = New System.Drawing.Size(834, 299)
        Me.RadScrollablePanel1.TabIndex = 2
        Me.RadScrollablePanel1.Text = "RadScrollablePanel1"
        '
        'RadChart1
        '
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
        Me.RadChart1.Location = New System.Drawing.Point(0, 0)
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
        Me.RadChart1.PlotArea.YAxis.MaxValue = 100
        Me.RadChart1.PlotArea.YAxis.Step = 10
        Me.RadChart1.PlotArea.YAxis2.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        ChartSeries1.Appearance.Border.Color = System.Drawing.Color.Black
        ChartSeries1.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(178, Byte), Integer))
        ChartSeries1.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(7, Byte), Integer))
        ChartSeries1.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black
        ChartSeries1.Name = "Series 1"
        ChartSeries1.Type = Telerik.Charting.ChartSeriesType.Line
        Me.RadChart1.Series.AddRange(New Telerik.Charting.ChartSeries() {ChartSeries1})
        Me.RadChart1.Size = New System.Drawing.Size(832, 307)
        Me.RadChart1.Skin = "Gradient"
        Me.RadChart1.SkinsOverrideStyles = True
        Me.RadChart1.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(750, 461)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(750, 461)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbFigure)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAutoScroll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboOrientation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboSkin)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromdate1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(855, 418)
        Me.SplitContainer1.SplitterDistance = 67
        Me.SplitContainer1.TabIndex = 2
        '
        'MyLabel7
        '
        Me.MyLabel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel7.Location = New System.Drawing.Point(559, 6)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel7.TabIndex = 655
        Me.MyLabel7.Text = "Figures In"
        '
        'cmbFigure
        '
        Me.cmbFigure.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFigure.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFigure.Location = New System.Drawing.Point(633, 4)
        Me.cmbFigure.MendatroryField = False
        Me.cmbFigure.MyLinkLable1 = Me.MyLabel7
        Me.cmbFigure.MyLinkLable2 = Nothing
        Me.cmbFigure.Name = "cmbFigure"
        Me.cmbFigure.ShowImageInEditorArea = True
        Me.cmbFigure.Size = New System.Drawing.Size(90, 20)
        Me.cmbFigure.TabIndex = 656
        '
        'chkAutoScroll
        '
        Me.chkAutoScroll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAutoScroll.Location = New System.Drawing.Point(805, 49)
        Me.chkAutoScroll.Name = "chkAutoScroll"
        Me.chkAutoScroll.Size = New System.Drawing.Size(47, 18)
        Me.chkAutoScroll.TabIndex = 36
        Me.chkAutoScroll.Text = "Scroll"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.Location = New System.Drawing.Point(557, 26)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel5.TabIndex = 34
        Me.MyLabel5.Text = "Orientation"
        '
        'cboOrientation
        '
        Me.cboOrientation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOrientation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOrientation.Location = New System.Drawing.Point(633, 26)
        Me.cboOrientation.MendatroryField = False
        Me.cboOrientation.MyLinkLable1 = Me.MyLabel5
        Me.cboOrientation.MyLinkLable2 = Nothing
        Me.cboOrientation.Name = "cboOrientation"
        Me.cboOrientation.ShowImageInEditorArea = True
        Me.cboOrientation.Size = New System.Drawing.Size(90, 20)
        Me.cboOrientation.TabIndex = 35
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.Location = New System.Drawing.Point(734, 27)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel4.TabIndex = 32
        Me.MyLabel4.Text = "Skin"
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.Location = New System.Drawing.Point(734, 4)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel3.TabIndex = 30
        Me.MyLabel3.Text = "Type"
        '
        'cboSkin
        '
        Me.cboSkin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSkin.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSkin.Location = New System.Drawing.Point(769, 26)
        Me.cboSkin.MendatroryField = False
        Me.cboSkin.MyLinkLable1 = Me.MyLabel4
        Me.cboSkin.MyLinkLable2 = Nothing
        Me.cboSkin.Name = "cboSkin"
        Me.cboSkin.ShowImageInEditorArea = True
        Me.cboSkin.Size = New System.Drawing.Size(83, 20)
        Me.cboSkin.TabIndex = 33
        '
        'lblFromDate
        '
        Me.lblFromDate.Location = New System.Drawing.Point(8, 10)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 1
        Me.lblFromDate.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(164, 12)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 2
        Me.lblToDate.Text = "To Date"
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(70, 10)
        Me.dtpFromdate1.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(88, 20)
        Me.dtpFromdate1.TabIndex = 20
        Me.dtpFromdate1.Text = "RadDateTimePicker1"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'cboType
        '
        Me.cboType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(769, 3)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.MyLabel3
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ShowImageInEditorArea = True
        Me.cboType.Size = New System.Drawing.Size(83, 20)
        Me.cboType.TabIndex = 31
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(214, 10)
        Me.dtpToDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 21
        Me.dtpToDate.Text = "RadDateTimePicker2"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(304, 10)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(59, 20)
        Me.RadButton2.TabIndex = 29
        Me.RadButton2.Text = ">>"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(769, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 21)
        Me.btnClose.TabIndex = 18
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
        Me.SplitContainer2.Size = New System.Drawing.Size(855, 447)
        Me.SplitContainer2.SplitterDistance = 418
        Me.SplitContainer2.TabIndex = 3
        '
        'btnExcelChart
        '
        Me.btnExcelChart.Location = New System.Drawing.Point(369, 10)
        Me.btnExcelChart.Name = "btnExcelChart"
        Me.btnExcelChart.Size = New System.Drawing.Size(75, 20)
        Me.btnExcelChart.TabIndex = 657
        Me.btnExcelChart.Text = "Excel Chart"
        '
        'frmGraphicalBatchOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(855, 447)
        Me.Controls.Add(Me.SplitContainer2)
        Me.KeyPreview = True
        Me.Name = "frmGraphicalBatchOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Graphical Batch Order Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadScrollablePanel1.PanelContainer.ResumeLayout(False)
        CType(Me.RadScrollablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel1.ResumeLayout(False)
        Me.RadScrollablePanel1.PerformLayout()
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadChart1 As Telerik.WinControls.UI.RadChart
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkAutoScroll As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboOrientation As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboSkin As common.Controls.MyComboBox
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadScrollablePanel1 As Telerik.WinControls.UI.RadScrollablePanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents cmbFigure As common.Controls.MyComboBox
    Friend WithEvents btnExcelChart As Telerik.WinControls.UI.RadButton
End Class

