<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBITopExpence
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
        Dim Corners4 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins4 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBITopExpence))
        Dim Corners5 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins5 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim Corners6 As Telerik.Charting.Styles.Corners = New Telerik.Charting.Styles.Corners
        Dim ChartMargins6 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim ChartSeries2 As Telerik.Charting.ChartSeries = New Telerik.Charting.ChartSeries
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.chkAutoScroll = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.cboFigure = New common.Controls.MyComboBox
        Me.cboOrientation = New common.Controls.MyComboBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.cboTop = New common.Controls.MyComboBox
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExcelChart = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFigure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTop, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAutoScroll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboFigure)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboOrientation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboTop)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(810, 347)
        Me.SplitContainer1.SplitterDistance = 67
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel8
        '
        Me.MyLabel8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel8.Location = New System.Drawing.Point(484, 6)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel8.TabIndex = 649
        Me.MyLabel8.Text = "Parameters In"
        '
        'chkAutoScroll
        '
        Me.chkAutoScroll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAutoScroll.Location = New System.Drawing.Point(757, 50)
        Me.chkAutoScroll.Name = "chkAutoScroll"
        Me.chkAutoScroll.Size = New System.Drawing.Size(47, 18)
        Me.chkAutoScroll.TabIndex = 18
        Me.chkAutoScroll.Text = "Scroll"
        '
        'MyLabel6
        '
        Me.MyLabel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel6.Location = New System.Drawing.Point(644, 6)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel6.TabIndex = 16
        Me.MyLabel6.Text = "Orientation"
        '
        'cboFigure
        '
        Me.cboFigure.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboFigure.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFigure.Location = New System.Drawing.Point(558, 5)
        Me.cboFigure.MendatroryField = False
        Me.cboFigure.MyLinkLable1 = Me.MyLabel8
        Me.cboFigure.MyLinkLable2 = Nothing
        Me.cboFigure.Name = "cboFigure"
        Me.cboFigure.ShowImageInEditorArea = True
        Me.cboFigure.Size = New System.Drawing.Size(83, 20)
        Me.cboFigure.TabIndex = 650
        '
        'cboOrientation
        '
        Me.cboOrientation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOrientation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOrientation.Location = New System.Drawing.Point(721, 5)
        Me.cboOrientation.MendatroryField = False
        Me.cboOrientation.MyLinkLable1 = Me.MyLabel6
        Me.cboOrientation.MyLinkLable2 = Nothing
        Me.cboOrientation.Name = "cboOrientation"
        Me.cboOrientation.ShowImageInEditorArea = True
        Me.cboOrientation.Size = New System.Drawing.Size(83, 20)
        Me.cboOrientation.TabIndex = 17
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(301, 6)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(25, 18)
        Me.MyLabel5.TabIndex = 13
        Me.MyLabel5.Text = "Top"
        '
        'cboTop
        '
        Me.cboTop.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboTop.Location = New System.Drawing.Point(331, 5)
        Me.cboTop.MendatroryField = False
        Me.cboTop.MyLinkLable1 = Me.MyLabel5
        Me.cboTop.MyLinkLable2 = Nothing
        Me.cboTop.Name = "cboTop"
        Me.cboTop.ShowImageInEditorArea = True
        Me.cboTop.Size = New System.Drawing.Size(53, 20)
        Me.cboTop.TabIndex = 14
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(683, 29)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel4.TabIndex = 11
        Me.MyLabel4.Text = "Skin"
        '
        'cboSkin
        '
        Me.cboSkin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSkin.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSkin.Location = New System.Drawing.Point(721, 28)
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
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(525, 29)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel3.TabIndex = 9
        Me.MyLabel3.Text = "Type"
        '
        'cboType
        '
        Me.cboType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(558, 28)
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
        Me.RadButton1.Location = New System.Drawing.Point(206, 31)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(76, 20)
        Me.RadButton1.TabIndex = 7
        Me.RadButton1.Text = ">>"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(4, 6)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "From Month"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(75, 5)
        Me.txtFromDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(79, 20)
        Me.txtFromDate.TabIndex = 5
        Me.txtFromDate.Text = "MyDateTimePicker1"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(159, 6)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 3
        Me.MyLabel1.Text = "To Month"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(217, 5)
        Me.txtToDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel1
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(79, 20)
        Me.txtToDate.TabIndex = 4
        Me.txtToDate.Text = "MyDateTimePicker1"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
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
        Me.RadScrollablePanel1.PanelContainer.Size = New System.Drawing.Size(808, 274)
        Me.RadScrollablePanel1.PanelContainer.TabIndex = 0
        '
        '
        '
        Me.RadScrollablePanel1.RootElement.Padding = New System.Windows.Forms.Padding(1)
        Me.RadScrollablePanel1.Size = New System.Drawing.Size(810, 276)
        Me.RadScrollablePanel1.TabIndex = 2
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
        Corners4.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners4.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners4.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners4.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.ChartTitle.Appearance.Corners = Corners4
        ChartMargins4.Bottom = CType(resources.GetObject("ChartMargins4.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins4.Left = CType(resources.GetObject("ChartMargins4.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins4.Right = CType(resources.GetObject("ChartMargins4.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins4.Top = CType(resources.GetObject("ChartMargins4.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.ChartTitle.Appearance.Dimensions.Margins = ChartMargins4
        Me.RadChart1.ChartTitle.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.RadChart1.ChartTitle.Appearance.Position.AlignedPosition = Telerik.Charting.Styles.AlignedPositions.Top
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.White
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.DefaultType = Telerik.Charting.ChartSeriesType.Line
        Me.RadChart1.Legend.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Corners5.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners5.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners5.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners5.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.Legend.Appearance.Corners = Corners5
        ChartMargins5.Right = CType(resources.GetObject("ChartMargins5.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins5.Top = CType(resources.GetObject("ChartMargins5.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.Legend.Appearance.Dimensions.Margins = ChartMargins5
        Me.RadChart1.Legend.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.RadChart1.Location = New System.Drawing.Point(1, 3)
        Me.RadChart1.Name = "RadChart1"
        Me.RadChart1.PlotArea.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(93, Byte), Integer))
        Corners6.BottomLeft = Telerik.Charting.Styles.CornerType.Round
        Corners6.BottomRight = Telerik.Charting.Styles.CornerType.Round
        Corners6.RoundSize = 6
        Corners6.TopLeft = Telerik.Charting.Styles.CornerType.Round
        Corners6.TopRight = Telerik.Charting.Styles.CornerType.Round
        Me.RadChart1.PlotArea.Appearance.Corners = Corners6
        ChartMargins6.Bottom = CType(resources.GetObject("ChartMargins6.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins6.Left = CType(resources.GetObject("ChartMargins6.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins6.Right = CType(resources.GetObject("ChartMargins6.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins6.Top = CType(resources.GetObject("ChartMargins6.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.PlotArea.Appearance.Dimensions.Margins = ChartMargins6
        Me.RadChart1.PlotArea.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.RadChart1.PlotArea.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.DimGray
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorTick.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.XAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.XAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.PlotArea.XAxis.MinValue = 1
        Me.RadChart1.PlotArea.YAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.Black
        Me.RadChart1.PlotArea.YAxis.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RadChart1.PlotArea.YAxis.MaxValue = 70
        Me.RadChart1.PlotArea.YAxis.Step = 10
        Me.RadChart1.PlotArea.YAxis2.AxisLabel.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        ChartSeries2.Appearance.Border.Color = System.Drawing.Color.Black
        ChartSeries2.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(178, Byte), Integer))
        ChartSeries2.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(7, Byte), Integer))
        ChartSeries2.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black
        ChartSeries2.Name = "Series 1"
        ChartSeries2.Type = Telerik.Charting.ChartSeriesType.Line
        Me.RadChart1.Series.AddRange(New Telerik.Charting.ChartSeries() {ChartSeries2})
        Me.RadChart1.Size = New System.Drawing.Size(807, 267)
        Me.RadChart1.Skin = "Gradient"
        Me.RadChart1.SkinsOverrideStyles = True
        Me.RadChart1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(695, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(112, 20)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 347)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(810, 26)
        Me.Panel1.TabIndex = 1
        '
        'btnExcelChart
        '
        Me.btnExcelChart.Location = New System.Drawing.Point(288, 31)
        Me.btnExcelChart.Name = "btnExcelChart"
        Me.btnExcelChart.Size = New System.Drawing.Size(96, 20)
        Me.btnExcelChart.TabIndex = 653
        Me.btnExcelChart.Text = "Excel Chart"
        '
        'frmBITopExpence
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 373)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmBITopExpence"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Top Expenses"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFigure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTop, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
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
    Friend WithEvents cboTop As common.Controls.MyComboBox
    Friend WithEvents RadScrollablePanel1 As Telerik.WinControls.UI.RadScrollablePanel
    Friend WithEvents RadChart1 As Telerik.WinControls.UI.RadChart
    Friend WithEvents chkAutoScroll As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cboOrientation As common.Controls.MyComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents cboFigure As common.Controls.MyComboBox
    Friend WithEvents btnExcelChart As Telerik.WinControls.UI.RadButton
End Class

