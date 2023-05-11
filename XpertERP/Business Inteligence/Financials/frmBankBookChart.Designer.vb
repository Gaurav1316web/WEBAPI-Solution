<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankBookChart
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
        Dim ChartMargins4 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBankBookChart))
        Dim ChartMargins5 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim ChartMargins6 As Telerik.Charting.Styles.ChartMargins = New Telerik.Charting.Styles.ChartMargins
        Dim ChartSeries2 As Telerik.Charting.ChartSeries = New Telerik.Charting.ChartSeries
        Dim GradientElement4 As Telerik.Charting.GradientElement = New Telerik.Charting.GradientElement
        Dim GradientElement5 As Telerik.Charting.GradientElement = New Telerik.Charting.GradientElement
        Dim GradientElement6 As Telerik.Charting.GradientElement = New Telerik.Charting.GradientElement
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.cmbFigure = New common.Controls.MyComboBox
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        Me.chkAutoScroll = New Telerik.WinControls.UI.RadCheckBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.cboOrientation = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.cboSkin = New common.Controls.MyComboBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.cboType = New common.Controls.MyComboBox
        Me.lblBankDesc = New common.Controls.MyLabel
        Me.txtBankCode = New common.UserControls.txtFinder
        Me.lblbankcode = New common.Controls.MyLabel
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadScrollablePanel2 = New Telerik.WinControls.UI.RadScrollablePanel
        Me.RadChart1 = New Telerik.WinControls.UI.RadChart
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvDetails = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnExcelChart = New Telerik.WinControls.UI.RadButton
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadScrollablePanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadScrollablePanel2.PanelContainer.SuspendLayout()
        Me.RadScrollablePanel2.SuspendLayout()
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(4, 5)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "From"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(81, 4)
        Me.txtFromDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(90, 20)
        Me.txtFromDate.TabIndex = 5
        Me.txtFromDate.Text = "MyDateTimePicker1"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(190, 5)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel1.TabIndex = 3
        Me.MyLabel1.Text = "To"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkAutoScroll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboOrientation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboSkin)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBankDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBankCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblbankcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1007, 474)
        Me.SplitContainer1.SplitterDistance = 67
        Me.SplitContainer1.TabIndex = 1
        '
        'MyLabel6
        '
        Me.MyLabel6.Location = New System.Drawing.Point(702, 2)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel6.TabIndex = 650
        Me.MyLabel6.Text = "Parameters In"
        '
        'cmbFigure
        '
        Me.cmbFigure.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFigure.Location = New System.Drawing.Point(785, 2)
        Me.cmbFigure.MendatroryField = False
        Me.cmbFigure.MyLinkLable1 = Me.MyLabel1
        Me.cmbFigure.MyLinkLable2 = Nothing
        Me.cmbFigure.Name = "cmbFigure"
        Me.cmbFigure.ShowImageInEditorArea = True
        Me.cmbFigure.Size = New System.Drawing.Size(90, 20)
        Me.cmbFigure.TabIndex = 649
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(319, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(96, 20)
        Me.btnRefresh.TabIndex = 613
        Me.btnRefresh.Text = ">>"
        '
        'chkAutoScroll
        '
        Me.chkAutoScroll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAutoScroll.Location = New System.Drawing.Point(957, 47)
        Me.chkAutoScroll.Name = "chkAutoScroll"
        Me.chkAutoScroll.Size = New System.Drawing.Size(47, 18)
        Me.chkAutoScroll.TabIndex = 612
        Me.chkAutoScroll.Text = "Scroll"
        '
        'MyLabel5
        '
        Me.MyLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel5.Location = New System.Drawing.Point(700, 26)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel5.TabIndex = 610
        Me.MyLabel5.Text = "Orientation"
        '
        'cboOrientation
        '
        Me.cboOrientation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOrientation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOrientation.Location = New System.Drawing.Point(785, 25)
        Me.cboOrientation.MendatroryField = False
        Me.cboOrientation.MyLinkLable1 = Me.MyLabel5
        Me.cboOrientation.MyLinkLable2 = Nothing
        Me.cboOrientation.Name = "cboOrientation"
        Me.cboOrientation.ShowImageInEditorArea = True
        Me.cboOrientation.Size = New System.Drawing.Size(90, 20)
        Me.cboOrientation.TabIndex = 611
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.Location = New System.Drawing.Point(893, 26)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel4.TabIndex = 608
        Me.MyLabel4.Text = "Skin"
        '
        'cboSkin
        '
        Me.cboSkin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSkin.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSkin.Location = New System.Drawing.Point(921, 25)
        Me.cboSkin.MendatroryField = False
        Me.cboSkin.MyLinkLable1 = Me.MyLabel4
        Me.cboSkin.MyLinkLable2 = Nothing
        Me.cboSkin.Name = "cboSkin"
        Me.cboSkin.ShowImageInEditorArea = True
        Me.cboSkin.Size = New System.Drawing.Size(83, 20)
        Me.cboSkin.TabIndex = 609
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.Location = New System.Drawing.Point(893, 3)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel3.TabIndex = 606
        Me.MyLabel3.Text = "Type"
        '
        'cboType
        '
        Me.cboType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(921, 2)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.MyLabel3
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ShowImageInEditorArea = True
        Me.cboType.Size = New System.Drawing.Size(83, 20)
        Me.cboType.TabIndex = 607
        '
        'lblBankDesc
        '
        Me.lblBankDesc.AutoSize = False
        Me.lblBankDesc.BorderVisible = True
        Me.lblBankDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankDesc.Location = New System.Drawing.Point(232, 27)
        Me.lblBankDesc.Name = "lblBankDesc"
        Me.lblBankDesc.Size = New System.Drawing.Size(381, 19)
        Me.lblBankDesc.TabIndex = 604
        Me.lblBankDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(81, 27)
        Me.txtBankCode.MendatroryField = True
        Me.txtBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.MyLinkLable1 = Nothing
        Me.txtBankCode.MyLinkLable2 = Nothing
        Me.txtBankCode.MyReadOnly = False
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(145, 19)
        Me.txtBankCode.TabIndex = 602
        Me.txtBankCode.Value = ""
        '
        'lblbankcode
        '
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(4, 27)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(62, 16)
        Me.lblbankcode.TabIndex = 603
        Me.lblbankcode.Text = "Bank Code"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(223, 4)
        Me.txtToDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel1
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(90, 20)
        Me.txtToDate.TabIndex = 4
        Me.txtToDate.Text = "MyDateTimePicker1"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
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
        Me.SplitContainer2.Size = New System.Drawing.Size(1007, 403)
        Me.SplitContainer2.SplitterDistance = 374
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
        Me.RadPageView2.Size = New System.Drawing.Size(1007, 374)
        Me.RadPageView2.TabIndex = 319
        Me.RadPageView2.Text = "RadPageView2"
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadScrollablePanel2)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(986, 326)
        Me.RadPageViewPage4.Text = "Filter"
        '
        'RadScrollablePanel2
        '
        Me.RadScrollablePanel2.AutoScroll = True
        Me.RadScrollablePanel2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.RadScrollablePanel2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.RadScrollablePanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel2.Location = New System.Drawing.Point(0, 0)
        Me.RadScrollablePanel2.Name = "RadScrollablePanel2"
        Me.RadScrollablePanel2.Padding = New System.Windows.Forms.Padding(1)
        '
        'RadScrollablePanel2.PanelContainer
        '
        Me.RadScrollablePanel2.PanelContainer.AutoScroll = True
        Me.RadScrollablePanel2.PanelContainer.Controls.Add(Me.RadChart1)
        Me.RadScrollablePanel2.PanelContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadScrollablePanel2.PanelContainer.Location = New System.Drawing.Point(1, 1)
        Me.RadScrollablePanel2.PanelContainer.Name = "PanelContainer"
        Me.RadScrollablePanel2.PanelContainer.Size = New System.Drawing.Size(967, 324)
        Me.RadScrollablePanel2.PanelContainer.TabIndex = 0
        '
        '
        '
        Me.RadScrollablePanel2.RootElement.Padding = New System.Windows.Forms.Padding(1)
        Me.RadScrollablePanel2.Size = New System.Drawing.Size(986, 326)
        Me.RadScrollablePanel2.TabIndex = 624
        Me.RadScrollablePanel2.Text = "RadScrollablePanel2"
        '
        'RadChart1
        '
        Me.RadChart1.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.RadChart1.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.Hatch
        Me.RadChart1.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.RadChart1.Appearance.FillStyle.SecondColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer))
        ChartMargins4.Bottom = CType(resources.GetObject("ChartMargins4.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins4.Left = CType(resources.GetObject("ChartMargins4.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins4.Right = CType(resources.GetObject("ChartMargins4.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins4.Top = CType(resources.GetObject("ChartMargins4.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.ChartTitle.Appearance.Dimensions.Margins = ChartMargins4
        Me.RadChart1.ChartTitle.Appearance.FillStyle.MainColor = System.Drawing.Color.Empty
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.RadChart1.ChartTitle.TextBlock.Appearance.TextProperties.Font = New System.Drawing.Font("Verdana", 18.0!)
        Me.RadChart1.DefaultType = Telerik.Charting.ChartSeriesType.Pie
        Me.RadChart1.Legend.Appearance.Border.Color = System.Drawing.Color.Empty
        ChartMargins5.Bottom = CType(resources.GetObject("ChartMargins5.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins5.Right = CType(resources.GetObject("ChartMargins5.Right"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.Legend.Appearance.Dimensions.Margins = ChartMargins5
        Me.RadChart1.Legend.Appearance.FillStyle.MainColor = System.Drawing.Color.Empty
        Me.RadChart1.Legend.Appearance.ItemTextAppearance.TextProperties.Color = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.RadChart1.Location = New System.Drawing.Point(0, 0)
        Me.RadChart1.Name = "RadChart1"
        Me.RadChart1.PlotArea.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(130, Byte), Integer))
        ChartMargins6.Bottom = CType(resources.GetObject("ChartMargins6.Bottom"), Telerik.Charting.Styles.Unit)
        ChartMargins6.Left = CType(resources.GetObject("ChartMargins6.Left"), Telerik.Charting.Styles.Unit)
        ChartMargins6.Right = CType(resources.GetObject("ChartMargins6.Right"), Telerik.Charting.Styles.Unit)
        ChartMargins6.Top = CType(resources.GetObject("ChartMargins6.Top"), Telerik.Charting.Styles.Unit)
        Me.RadChart1.PlotArea.Appearance.Dimensions.Margins = ChartMargins6
        Me.RadChart1.PlotArea.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.RadChart1.PlotArea.Appearance.FillStyle.SecondColor = System.Drawing.Color.Transparent
        Me.RadChart1.PlotArea.XAxis.Appearance.Color = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorGridLines.Width = 0.0!
        Me.RadChart1.PlotArea.XAxis.Appearance.MajorTick.Color = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.AxisLabel.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.RadChart1.PlotArea.XAxis.MinValue = 1
        Me.RadChart1.PlotArea.YAxis.Appearance.Color = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.RadChart1.PlotArea.YAxis.Appearance.MajorGridLines.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.RadChart1.PlotArea.YAxis.Appearance.MajorTick.Color = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.RadChart1.PlotArea.YAxis.Appearance.MinorGridLines.Color = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.RadChart1.PlotArea.YAxis.Appearance.MinorTick.Color = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.RadChart1.PlotArea.YAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.RadChart1.PlotArea.YAxis.AxisLabel.TextBlock.Appearance.TextProperties.Color = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(85, Byte), Integer))
        ChartSeries2.Appearance.Border.Color = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(58, Byte), Integer))
        GradientElement4.Color = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(152, Byte), Integer))
        GradientElement5.Color = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(123, Byte), Integer))
        GradientElement5.Position = 0.5!
        GradientElement6.Color = System.Drawing.Color.FromArgb(CType(CType(183, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(84, Byte), Integer))
        GradientElement6.Position = 1.0!
        ChartSeries2.Appearance.FillStyle.FillSettings.ComplexGradient.AddRange(New Telerik.Charting.GradientElement() {GradientElement4, GradientElement5, GradientElement6})
        ChartSeries2.Appearance.FillStyle.FillType = Telerik.Charting.Styles.FillType.ComplexGradient
        ChartSeries2.Appearance.FillStyle.MainColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(155, Byte), Integer))
        ChartSeries2.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(159, Byte), Integer))
        ChartSeries2.Name = "Series 1"
        ChartSeries2.Type = Telerik.Charting.ChartSeriesType.Pie
        Me.RadChart1.Series.AddRange(New Telerik.Charting.ChartSeries() {ChartSeries2})
        Me.RadChart1.Size = New System.Drawing.Size(921, 341)
        Me.RadChart1.Skin = "GrayStripes"
        Me.RadChart1.SkinsOverrideStyles = True
        Me.RadChart1.TabIndex = 622
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gvDetails)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(984, 373)
        Me.RadPageViewPage5.Text = "Details"
        '
        'gvDetails
        '
        Me.gvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetails.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvDetails.MasterTemplate.AllowEditRow = False
        Me.gvDetails.MasterTemplate.EnableFiltering = True
        Me.gvDetails.Name = "gvDetails"
        Me.gvDetails.ShowGroupPanel = False
        Me.gvDetails.Size = New System.Drawing.Size(984, 373)
        Me.gvDetails.TabIndex = 0
        Me.gvDetails.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(921, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 20)
        Me.btnClose.TabIndex = 605
        Me.btnClose.Text = "Close"
        '
        'btnExcelChart
        '
        Me.btnExcelChart.Location = New System.Drawing.Point(421, 5)
        Me.btnExcelChart.Name = "btnExcelChart"
        Me.btnExcelChart.Size = New System.Drawing.Size(96, 20)
        Me.btnExcelChart.TabIndex = 651
        Me.btnExcelChart.Text = "Excel Chart"
        '
        'FrmBankBookChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1007, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBankBookChart"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Summary"
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFigure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoScroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSkin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadScrollablePanel2.PanelContainer.ResumeLayout(False)
        CType(Me.RadScrollablePanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadScrollablePanel2.ResumeLayout(False)
        Me.RadScrollablePanel2.PerformLayout()
        CType(Me.RadChart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExcelChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblBankDesc As common.Controls.MyLabel
    Friend WithEvents txtBankCode As common.UserControls.txtFinder
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents chkAutoScroll As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboOrientation As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents cboSkin As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView2 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadChart1 As Telerik.WinControls.UI.RadChart
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDetails As common.UserControls.MyRadGridView
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbFigure As common.Controls.MyComboBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents RadScrollablePanel2 As Telerik.WinControls.UI.RadScrollablePanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnExcelChart As Telerik.WinControls.UI.RadButton
End Class

