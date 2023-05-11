<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateBIReport
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateBIReport))
        Dim CartesianArea1 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Dim CartesianArea2 As Telerik.WinControls.UI.CartesianArea = New Telerik.WinControls.UI.CartesianArea()
        Me.MssqlMetadataProvider1 = New ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLMetadataProvider(Me.components)
        Me.MssqlSyntaxProvider1 = New ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLSyntaxProvider(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlainTextSQLBuilder1 = New ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder(Me.components)
        Me.queryBuilder = New ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblTransactionTypeColumn = New common.Controls.MyLabel()
        Me.lblDrillDownColumn = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.rbtnDrillDownTransaction = New common.Controls.MyRadioButton()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.rbtnDrillDownReport = New common.Controls.MyRadioButton()
        Me.txtDrilldownReport = New common.UserControls.txtFinder()
        Me.rbtnDrillDownNA = New common.Controls.MyRadioButton()
        Me.txtDrilldownFilter = New common.UserControls.txtFinder()
        Me.chkDashboard = New common.Controls.MyCheckBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.cboSetting = New common.Controls.MyComboBox()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtReportModule = New common.UserControls.txtFinder()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.sqlQueryText = New ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor()
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.gvFilter = New common.UserControls.MyRadGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gvFilterInner = New common.UserControls.MyRadGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.RadButton9 = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel()
        Me.pg1 = New Telerik.WinControls.UI.RadPivotGrid()
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Me.radPivotFieldList1 = New Telerik.WinControls.UI.RadPivotFieldList()
        Me.SplitPanel3 = New Telerik.WinControls.UI.SplitPanel()
        Me.radGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.columnSubTotalNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.columnSubTotalLast = New Telerik.WinControls.UI.RadRadioButton()
        Me.columnSubTotalFirst = New Telerik.WinControls.UI.RadRadioButton()
        Me.radGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.columnGrandTotalNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.columnGrandTotalLast = New Telerik.WinControls.UI.RadRadioButton()
        Me.columnGrandTotalFirst = New Telerik.WinControls.UI.RadRadioButton()
        Me.radGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rowSubTotalNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.rowSubTotalLast = New Telerik.WinControls.UI.RadRadioButton()
        Me.rowSubTotalFirst = New Telerik.WinControls.UI.RadRadioButton()
        Me.radGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rowGrandTotalNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.rowGrandTotalLast = New Telerik.WinControls.UI.RadRadioButton()
        Me.rowGrandTotalFirst = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadChartView1 = New Telerik.WinControls.UI.RadChartView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadChartView2 = New Telerik.WinControls.UI.RadChartView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadCollapsiblePanel2 = New Telerik.WinControls.UI.RadCollapsiblePanel()
        Me.RadButton8 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton7 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadCollapsiblePanel1 = New Telerik.WinControls.UI.RadCollapsiblePanel()
        Me.chkScroll = New common.Controls.MyCheckBox()
        Me.cboChartType = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.chkShowLables = New common.Controls.MyCheckBox()
        Me.txtLableRotation = New Telerik.WinControls.UI.RadSpinEditor()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.cboCombineMode = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.cboOrientation = New common.Controls.MyComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.lblTransactionTypeColumn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDrillDownColumn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDrillDownTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDrillDownReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDrillDownNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDashboard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.RadPageViewPage6.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.gvFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvFilter.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvFilterInner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvFilterInner.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.RadButton9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.pg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.SplitPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel3.SuspendLayout()
        CType(Me.radGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.radGroupBox4.SuspendLayout()
        CType(Me.columnSubTotalNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.columnSubTotalLast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.columnSubTotalFirst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.radGroupBox3.SuspendLayout()
        CType(Me.columnGrandTotalNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.columnGrandTotalLast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.columnGrandTotalFirst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.radGroupBox2.SuspendLayout()
        CType(Me.rowSubTotalNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rowSubTotalLast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rowSubTotalFirst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.radGroupBox1.SuspendLayout()
        CType(Me.rowGrandTotalNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rowGrandTotalLast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rowGrandTotalFirst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.RadChartView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadChartView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadCollapsiblePanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadCollapsiblePanel2.PanelContainer.SuspendLayout()
        CType(Me.RadButton8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCollapsiblePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadCollapsiblePanel1.PanelContainer.SuspendLayout()
        CType(Me.chkScroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboChartType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowLables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLableRotation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCombineMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MssqlMetadataProvider1
        '
        Me.MssqlMetadataProvider1.Connection = Nothing
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(184, 70)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem1.Text = "ToolStripMenuItem1"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem2.Text = "ToolStripMenuItem2"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem3.Text = "ToolStripMenuItem3"
        '
        'PlainTextSQLBuilder1
        '
        Me.PlainTextSQLBuilder1.CTESubQueryFormat.FromClauseFormat.NewLineAfterDatasource = False
        Me.PlainTextSQLBuilder1.CTESubQueryFormat.GroupByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.CTESubQueryFormat.HavingFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.CTESubQueryFormat.OrderByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.CTESubQueryFormat.SelectListFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.CTESubQueryFormat.WhereFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.DerivedQueryFormat.FromClauseFormat.NewLineAfterDatasource = False
        Me.PlainTextSQLBuilder1.DerivedQueryFormat.GroupByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.DerivedQueryFormat.HavingFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.DerivedQueryFormat.OrderByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.DerivedQueryFormat.SelectListFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.DerivedQueryFormat.WhereFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.DynamicIndents = False
        Me.PlainTextSQLBuilder1.DynamicRightMargin = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.FromClauseFormat.NewLineAfterDatasource = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.GroupByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.HavingFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.MainPartsFromNewLine = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.OrderByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.SelectListFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.WhereFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.MainQueryFormat.FromClauseFormat.NewLineAfterDatasource = False
        Me.PlainTextSQLBuilder1.MainQueryFormat.GroupByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.MainQueryFormat.HavingFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.MainQueryFormat.MainPartsFromNewLine = False
        Me.PlainTextSQLBuilder1.MainQueryFormat.OrderByFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.MainQueryFormat.SelectListFormat.NewLineAfterItem = False
        Me.PlainTextSQLBuilder1.MainQueryFormat.WhereFormat.NewLineAfter = ActiveDatabaseSoftware.ActiveQueryBuilder.SQLBuilderConditionFormatNL.None
        Me.PlainTextSQLBuilder1.QueryBuilder = Me.queryBuilder
        Me.PlainTextSQLBuilder1.RightMargin = 0
        '
        'queryBuilder
        '
        Me.queryBuilder.AddObjectFormOptions.MinimumSize = New System.Drawing.Size(430, 430)
        Me.queryBuilder.DatabaseSchemaTreeOptions.BackColor = System.Drawing.SystemColors.Window
        Me.queryBuilder.DatabaseSchemaTreeOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.queryBuilder.DatabaseSchemaTreeOptions.TextColor = System.Drawing.SystemColors.WindowText
        Me.queryBuilder.DataSourceOptions.BackgroundColor = System.Drawing.SystemColors.Window
        Me.queryBuilder.DataSourceOptions.DescriptionColumnOptions.Color = System.Drawing.Color.LightBlue
        Me.queryBuilder.DataSourceOptions.FocusedBackgroundColor = System.Drawing.SystemColors.Window
        Me.queryBuilder.DataSourceOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.queryBuilder.DataSourceOptions.MarkColumnOptions.PrimaryKeyIcon = CType(resources.GetObject("resource.PrimaryKeyIcon"), System.Drawing.Image)
        Me.queryBuilder.DataSourceOptions.NameColumnOptions.Color = System.Drawing.SystemColors.WindowText
        Me.queryBuilder.DataSourceOptions.NameColumnOptions.PrimaryKeyColor = System.Drawing.SystemColors.WindowText
        Me.queryBuilder.DataSourceOptions.TypeColumnOptions.Color = System.Drawing.SystemColors.GrayText
        Me.queryBuilder.DesignPaneOptions.BackColor = System.Drawing.SystemColors.Window
        Me.queryBuilder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.queryBuilder.Location = New System.Drawing.Point(0, 0)
        Me.queryBuilder.MetadataLoadingOptions.DisableAutomaticMetadataLoading = False
        Me.queryBuilder.MetadataStructureOptions.ProceduresFolderText = "Procedures"
        Me.queryBuilder.MetadataStructureOptions.SynonymsFolderText = "Synonyms"
        Me.queryBuilder.MetadataStructureOptions.TablesFolderText = "Tables"
        Me.queryBuilder.MetadataStructureOptions.ViewsFolderText = "Views"
        Me.queryBuilder.Name = "queryBuilder"
        Me.queryBuilder.QueryColumnListOptions.AlternateRowColor = System.Drawing.SystemColors.Window
        Me.queryBuilder.QueryColumnListOptions.BackColor = System.Drawing.SystemColors.Window
        Me.queryBuilder.QueryColumnListOptions.EmptySpaceColor = System.Drawing.SystemColors.ControlDark
        Me.queryBuilder.QueryColumnListOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.queryBuilder.QueryColumnListOptions.TextColor = System.Drawing.SystemColors.WindowText
        Me.queryBuilder.QueryStructureTreeOptions.BackColor = System.Drawing.SystemColors.Window
        Me.queryBuilder.QueryStructureTreeOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.queryBuilder.QueryStructureTreeOptions.QueriesImageIndex = 17
        Me.queryBuilder.QueryStructureTreeOptions.TextColor = System.Drawing.SystemColors.WindowText
        Me.queryBuilder.Size = New System.Drawing.Size(808, 204)
        Me.queryBuilder.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkDashboard)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboSetting)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel23)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtReportModule)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(829, 417)
        Me.SplitContainer2.SplitterDistance = 72
        Me.SplitContainer2.TabIndex = 4
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblTransactionTypeColumn)
        Me.GroupBox3.Controls.Add(Me.lblDrillDownColumn)
        Me.GroupBox3.Controls.Add(Me.MyLabel7)
        Me.GroupBox3.Controls.Add(Me.rbtnDrillDownTransaction)
        Me.GroupBox3.Controls.Add(Me.MyLabel8)
        Me.GroupBox3.Controls.Add(Me.rbtnDrillDownReport)
        Me.GroupBox3.Controls.Add(Me.txtDrilldownReport)
        Me.GroupBox3.Controls.Add(Me.rbtnDrillDownNA)
        Me.GroupBox3.Controls.Add(Me.txtDrilldownFilter)
        Me.GroupBox3.Location = New System.Drawing.Point(307, 22)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(513, 48)
        Me.GroupBox3.TabIndex = 47
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Drilldown"
        '
        'lblTransactionTypeColumn
        '
        Me.lblTransactionTypeColumn.BorderVisible = True
        Me.lblTransactionTypeColumn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransactionTypeColumn.Location = New System.Drawing.Point(145, 29)
        Me.lblTransactionTypeColumn.Name = "lblTransactionTypeColumn"
        Me.lblTransactionTypeColumn.Size = New System.Drawing.Size(141, 16)
        Me.lblTransactionTypeColumn.TabIndex = 54
        Me.lblTransactionTypeColumn.Text = "lblTransactionTypeColumn"
        '
        'lblDrillDownColumn
        '
        Me.lblDrillDownColumn.BorderVisible = True
        Me.lblDrillDownColumn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrillDownColumn.Location = New System.Drawing.Point(331, 30)
        Me.lblDrillDownColumn.Name = "lblDrillDownColumn"
        Me.lblDrillDownColumn.Size = New System.Drawing.Size(141, 16)
        Me.lblDrillDownColumn.TabIndex = 53
        Me.lblDrillDownColumn.Text = "lblTransactionTypeColumn"
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(101, 10)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel7.TabIndex = 46
        Me.MyLabel7.Text = "Report"
        '
        'rbtnDrillDownTransaction
        '
        Me.rbtnDrillDownTransaction.Location = New System.Drawing.Point(4, 28)
        Me.rbtnDrillDownTransaction.MyLinkLable1 = Nothing
        Me.rbtnDrillDownTransaction.MyLinkLable2 = Nothing
        Me.rbtnDrillDownTransaction.Name = "rbtnDrillDownTransaction"
        Me.rbtnDrillDownTransaction.Size = New System.Drawing.Size(78, 18)
        Me.rbtnDrillDownTransaction.TabIndex = 52
        Me.rbtnDrillDownTransaction.TabStop = False
        Me.rbtnDrillDownTransaction.Text = "Transaction"
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(296, 10)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel8.TabIndex = 48
        Me.MyLabel8.Text = "Filter"
        '
        'rbtnDrillDownReport
        '
        Me.rbtnDrillDownReport.Location = New System.Drawing.Point(39, 12)
        Me.rbtnDrillDownReport.MyLinkLable1 = Nothing
        Me.rbtnDrillDownReport.MyLinkLable2 = Nothing
        Me.rbtnDrillDownReport.Name = "rbtnDrillDownReport"
        Me.rbtnDrillDownReport.Size = New System.Drawing.Size(54, 18)
        Me.rbtnDrillDownReport.TabIndex = 51
        Me.rbtnDrillDownReport.TabStop = False
        Me.rbtnDrillDownReport.Text = "Report"
        '
        'txtDrilldownReport
        '
        Me.txtDrilldownReport.Location = New System.Drawing.Point(145, 10)
        Me.txtDrilldownReport.MendatroryField = False
        Me.txtDrilldownReport.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDrilldownReport.MyLinkLable1 = Me.MyLabel7
        Me.txtDrilldownReport.MyLinkLable2 = Nothing
        Me.txtDrilldownReport.MyReadOnly = False
        Me.txtDrilldownReport.MyShowMasterFormButton = False
        Me.txtDrilldownReport.Name = "txtDrilldownReport"
        Me.txtDrilldownReport.Size = New System.Drawing.Size(145, 18)
        Me.txtDrilldownReport.TabIndex = 45
        Me.txtDrilldownReport.Value = ""
        '
        'rbtnDrillDownNA
        '
        Me.rbtnDrillDownNA.Location = New System.Drawing.Point(4, 12)
        Me.rbtnDrillDownNA.MyLinkLable1 = Nothing
        Me.rbtnDrillDownNA.MyLinkLable2 = Nothing
        Me.rbtnDrillDownNA.Name = "rbtnDrillDownNA"
        Me.rbtnDrillDownNA.Size = New System.Drawing.Size(36, 18)
        Me.rbtnDrillDownNA.TabIndex = 50
        Me.rbtnDrillDownNA.TabStop = False
        Me.rbtnDrillDownNA.Text = "NA"
        '
        'txtDrilldownFilter
        '
        Me.txtDrilldownFilter.Location = New System.Drawing.Point(331, 10)
        Me.txtDrilldownFilter.MendatroryField = False
        Me.txtDrilldownFilter.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDrilldownFilter.MyLinkLable1 = Me.MyLabel8
        Me.txtDrilldownFilter.MyLinkLable2 = Nothing
        Me.txtDrilldownFilter.MyReadOnly = False
        Me.txtDrilldownFilter.MyShowMasterFormButton = False
        Me.txtDrilldownFilter.Name = "txtDrilldownFilter"
        Me.txtDrilldownFilter.Size = New System.Drawing.Size(122, 18)
        Me.txtDrilldownFilter.TabIndex = 47
        Me.txtDrilldownFilter.Value = ""
        '
        'chkDashboard
        '
        Me.chkDashboard.Location = New System.Drawing.Point(92, 51)
        Me.chkDashboard.MyLinkLable1 = Nothing
        Me.chkDashboard.MyLinkLable2 = Nothing
        Me.chkDashboard.Name = "chkDashboard"
        Me.chkDashboard.Size = New System.Drawing.Size(94, 18)
        Me.chkDashboard.TabIndex = 44
        Me.chkDashboard.Tag1 = Nothing
        Me.chkDashboard.Text = "For Dashboard"
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(690, 4)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel6.TabIndex = 43
        Me.MyLabel6.Text = "Setting"
        '
        'cboSetting
        '
        Me.cboSetting.AutoCompleteDisplayMember = Nothing
        Me.cboSetting.AutoCompleteValueMember = Nothing
        Me.cboSetting.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboSetting.Location = New System.Drawing.Point(735, 2)
        Me.cboSetting.MendatroryField = True
        Me.cboSetting.MyLinkLable1 = Me.MyLabel6
        Me.cboSetting.MyLinkLable2 = Nothing
        Me.cboSetting.Name = "cboSetting"
        Me.cboSetting.Size = New System.Drawing.Size(88, 20)
        Me.cboSetting.TabIndex = 42
        '
        'RadLabel23
        '
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(526, 4)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(31, 16)
        Me.RadLabel23.TabIndex = 39
        Me.RadLabel23.Text = "Type"
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(560, 2)
        Me.cboType.MendatroryField = True
        Me.cboType.MyLinkLable1 = Me.RadLabel23
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(128, 20)
        Me.cboType.TabIndex = 38
        '
        'RadLabel15
        '
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(4, 31)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel15.TabIndex = 41
        Me.RadLabel15.Text = "Report Module"
        '
        'txtReportModule
        '
        Me.txtReportModule.Location = New System.Drawing.Point(92, 30)
        Me.txtReportModule.MendatroryField = True
        Me.txtReportModule.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportModule.MyLinkLable1 = Me.RadLabel15
        Me.txtReportModule.MyLinkLable2 = Nothing
        Me.txtReportModule.MyReadOnly = False
        Me.txtReportModule.MyShowMasterFormButton = False
        Me.txtReportModule.Name = "txtReportModule"
        Me.txtReportModule.Size = New System.Drawing.Size(201, 18)
        Me.txtReportModule.TabIndex = 40
        Me.txtReportModule.Value = ""
        '
        'txtDesc
        '
        Me.txtDesc.ContextMenuStrip = Me.ContextMenuStrip1
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(315, 3)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(208, 18)
        Me.txtDesc.TabIndex = 26
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(292, 2)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 2
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(90, 2)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = True
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(202, 21)
        Me.txtCode.TabIndex = 1
        Me.txtCode.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(4, 3)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "Report Code"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage4
        Me.RadPageView1.Size = New System.Drawing.Size(829, 341)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "Chart"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(808, 293)
        Me.RadPageViewPage1.Text = "Query"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.queryBuilder)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.sqlQueryText)
        Me.SplitContainer1.Size = New System.Drawing.Size(808, 293)
        Me.SplitContainer1.SplitterDistance = 204
        Me.SplitContainer1.TabIndex = 1
        '
        'sqlQueryText
        '
        Me.sqlQueryText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sqlQueryText.CommentColor = System.Drawing.Color.Green
        Me.sqlQueryText.ContextMenuStrip = Me.ContextMenuStrip1
        Me.sqlQueryText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sqlQueryText.FunctionColor = System.Drawing.Color.Purple
        Me.sqlQueryText.GutterBackColor = System.Drawing.Color.LightGray
        Me.sqlQueryText.GutterForeColor = System.Drawing.Color.Black
        Me.sqlQueryText.GutterSeparatorColor = System.Drawing.Color.LightGray
        Me.sqlQueryText.HighlightMatchingParentheses = ActiveDatabaseSoftware.ExpressionEditor.TextEditorControl.ParenthesesHighlighting.HighlightWithColor
        Me.sqlQueryText.InactiveSelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        Me.sqlQueryText.KeywordColor = System.Drawing.Color.Blue
        Me.sqlQueryText.Location = New System.Drawing.Point(0, 0)
        Me.sqlQueryText.Name = "sqlQueryText"
        Me.sqlQueryText.NumberColor = System.Drawing.Color.DarkCyan
        Me.sqlQueryText.QueryBuilder = Me.queryBuilder
        Me.sqlQueryText.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.sqlQueryText.SelectionTextColor = System.Drawing.SystemColors.HighlightText
        Me.sqlQueryText.Size = New System.Drawing.Size(808, 85)
        Me.sqlQueryText.StringColor = System.Drawing.Color.DarkRed
        Me.sqlQueryText.TabIndex = 1
        Me.sqlQueryText.Text = "SqlTextEditor"
        Me.sqlQueryText.TextColor = System.Drawing.SystemColors.WindowText
        Me.sqlQueryText.TextPadding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.SplitContainer4)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(808, 293)
        Me.RadPageViewPage6.Text = "Filters"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer4.Size = New System.Drawing.Size(808, 293)
        Me.SplitContainer4.SplitterDistance = 173
        Me.SplitContainer4.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.gvFilter)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(808, 173)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Outer Filter"
        '
        'gvFilter
        '
        Me.gvFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvFilter.Location = New System.Drawing.Point(3, 18)
        Me.gvFilter.Name = "gvFilter"
        Me.gvFilter.Size = New System.Drawing.Size(802, 152)
        Me.gvFilter.TabIndex = 1
        Me.gvFilter.Text = "RadGridView1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gvFilterInner)
        Me.GroupBox1.Controls.Add(Me.Panel3)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(808, 116)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Inner Filter"
        '
        'gvFilterInner
        '
        Me.gvFilterInner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvFilterInner.Location = New System.Drawing.Point(3, 46)
        Me.gvFilterInner.Name = "gvFilterInner"
        Me.gvFilterInner.Size = New System.Drawing.Size(802, 67)
        Me.gvFilterInner.TabIndex = 2
        Me.gvFilterInner.Text = "RadGridView1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.RadButton9)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 18)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(802, 28)
        Me.Panel3.TabIndex = 9
        '
        'RadButton9
        '
        Me.RadButton9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton9.Location = New System.Drawing.Point(5, 3)
        Me.RadButton9.Name = "RadButton9"
        Me.RadButton9.Size = New System.Drawing.Size(98, 22)
        Me.RadButton9.TabIndex = 8
        Me.RadButton9.Text = "Add Filter"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(64.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(763, 314)
        Me.RadPageViewPage2.Text = "Data Grid"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(763, 314)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadSplitContainer1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(763, 314)
        Me.RadPageViewPage3.Text = "Pivot Grid"
        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel3)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(763, 314)
        Me.RadSplitContainer1.TabIndex = 1
        Me.RadSplitContainer1.TabStop = False
        Me.RadSplitContainer1.Text = "RadSplitContainer1"
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.pg1)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel1.Size = New System.Drawing.Size(763, 314)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.1289183!, 0.0!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(97, 0)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'pg1
        '
        Me.pg1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pg1.Location = New System.Drawing.Point(0, 0)
        Me.pg1.Name = "pg1"
        Me.pg1.Size = New System.Drawing.Size(763, 314)
        Me.pg1.TabIndex = 0
        Me.pg1.Text = "RadPivotGrid1"
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Collapsed = True
        Me.SplitPanel2.Controls.Add(Me.radPivotFieldList1)
        Me.SplitPanel2.Location = New System.Drawing.Point(481, 0)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel2.Size = New System.Drawing.Size(282, 316)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(-0.0339956!, 0.0!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(-26, 0)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'radPivotFieldList1
        '
        Me.radPivotFieldList1.AssociatedPivotGrid = Me.pg1
        Me.radPivotFieldList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.radPivotFieldList1.Location = New System.Drawing.Point(0, 0)
        Me.radPivotFieldList1.MinimumSize = New System.Drawing.Size(225, 305)
        Me.radPivotFieldList1.Name = "radPivotFieldList1"
        Me.radPivotFieldList1.Size = New System.Drawing.Size(282, 316)
        Me.radPivotFieldList1.TabIndex = 1
        '
        'SplitPanel3
        '
        Me.SplitPanel3.Collapsed = True
        Me.SplitPanel3.Controls.Add(Me.radGroupBox4)
        Me.SplitPanel3.Controls.Add(Me.radGroupBox3)
        Me.SplitPanel3.Controls.Add(Me.radGroupBox2)
        Me.SplitPanel3.Controls.Add(Me.radGroupBox1)
        Me.SplitPanel3.Location = New System.Drawing.Point(583, 0)
        Me.SplitPanel3.Name = "SplitPanel3"
        '
        '
        '
        Me.SplitPanel3.RootElement.MinSize = New System.Drawing.Size(0, 0)
        Me.SplitPanel3.Size = New System.Drawing.Size(180, 316)
        Me.SplitPanel3.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(-0.09492274!, 0.0!)
        Me.SplitPanel3.SizeInfo.SplitterCorrection = New System.Drawing.Size(-71, 0)
        Me.SplitPanel3.TabIndex = 2
        Me.SplitPanel3.TabStop = False
        Me.SplitPanel3.Text = "SplitPanel3"
        '
        'radGroupBox4
        '
        Me.radGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.radGroupBox4.Controls.Add(Me.columnSubTotalNone)
        Me.radGroupBox4.Controls.Add(Me.columnSubTotalLast)
        Me.radGroupBox4.Controls.Add(Me.columnSubTotalFirst)
        Me.radGroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.radGroupBox4.HeaderText = "Columns SubTotals Position"
        Me.radGroupBox4.Location = New System.Drawing.Point(0, 120)
        Me.radGroupBox4.Name = "radGroupBox4"
        Me.radGroupBox4.Size = New System.Drawing.Size(180, 40)
        Me.radGroupBox4.TabIndex = 6
        Me.radGroupBox4.Text = "Columns SubTotals Position"
        '
        'columnSubTotalNone
        '
        Me.columnSubTotalNone.Location = New System.Drawing.Point(9, 18)
        Me.columnSubTotalNone.Name = "columnSubTotalNone"
        Me.columnSubTotalNone.Size = New System.Drawing.Size(48, 18)
        Me.columnSubTotalNone.TabIndex = 5
        Me.columnSubTotalNone.Text = "None"
        '
        'columnSubTotalLast
        '
        Me.columnSubTotalLast.Location = New System.Drawing.Point(120, 18)
        Me.columnSubTotalLast.Name = "columnSubTotalLast"
        Me.columnSubTotalLast.Size = New System.Drawing.Size(40, 18)
        Me.columnSubTotalLast.TabIndex = 4
        Me.columnSubTotalLast.Text = "Last"
        '
        'columnSubTotalFirst
        '
        Me.columnSubTotalFirst.Location = New System.Drawing.Point(68, 18)
        Me.columnSubTotalFirst.Name = "columnSubTotalFirst"
        Me.columnSubTotalFirst.Size = New System.Drawing.Size(41, 18)
        Me.columnSubTotalFirst.TabIndex = 3
        Me.columnSubTotalFirst.Text = "First"
        '
        'radGroupBox3
        '
        Me.radGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.radGroupBox3.Controls.Add(Me.columnGrandTotalNone)
        Me.radGroupBox3.Controls.Add(Me.columnGrandTotalLast)
        Me.radGroupBox3.Controls.Add(Me.columnGrandTotalFirst)
        Me.radGroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.radGroupBox3.HeaderText = "Columns GrandTotals Position"
        Me.radGroupBox3.Location = New System.Drawing.Point(0, 80)
        Me.radGroupBox3.Name = "radGroupBox3"
        Me.radGroupBox3.Size = New System.Drawing.Size(180, 40)
        Me.radGroupBox3.TabIndex = 5
        Me.radGroupBox3.Text = "Columns GrandTotals Position"
        '
        'columnGrandTotalNone
        '
        Me.columnGrandTotalNone.Location = New System.Drawing.Point(9, 17)
        Me.columnGrandTotalNone.Name = "columnGrandTotalNone"
        Me.columnGrandTotalNone.Size = New System.Drawing.Size(48, 18)
        Me.columnGrandTotalNone.TabIndex = 5
        Me.columnGrandTotalNone.Text = "None"
        '
        'columnGrandTotalLast
        '
        Me.columnGrandTotalLast.Location = New System.Drawing.Point(120, 17)
        Me.columnGrandTotalLast.Name = "columnGrandTotalLast"
        Me.columnGrandTotalLast.Size = New System.Drawing.Size(40, 18)
        Me.columnGrandTotalLast.TabIndex = 4
        Me.columnGrandTotalLast.Text = "Last"
        '
        'columnGrandTotalFirst
        '
        Me.columnGrandTotalFirst.Location = New System.Drawing.Point(68, 17)
        Me.columnGrandTotalFirst.Name = "columnGrandTotalFirst"
        Me.columnGrandTotalFirst.Size = New System.Drawing.Size(41, 18)
        Me.columnGrandTotalFirst.TabIndex = 3
        Me.columnGrandTotalFirst.Text = "First"
        '
        'radGroupBox2
        '
        Me.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.radGroupBox2.Controls.Add(Me.rowSubTotalNone)
        Me.radGroupBox2.Controls.Add(Me.rowSubTotalLast)
        Me.radGroupBox2.Controls.Add(Me.rowSubTotalFirst)
        Me.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.radGroupBox2.HeaderText = "Rows SubTotals Position"
        Me.radGroupBox2.Location = New System.Drawing.Point(0, 40)
        Me.radGroupBox2.Name = "radGroupBox2"
        Me.radGroupBox2.Size = New System.Drawing.Size(180, 40)
        Me.radGroupBox2.TabIndex = 4
        Me.radGroupBox2.Text = "Rows SubTotals Position"
        '
        'rowSubTotalNone
        '
        Me.rowSubTotalNone.Location = New System.Drawing.Point(9, 18)
        Me.rowSubTotalNone.Name = "rowSubTotalNone"
        Me.rowSubTotalNone.Size = New System.Drawing.Size(48, 18)
        Me.rowSubTotalNone.TabIndex = 5
        Me.rowSubTotalNone.Text = "None"
        '
        'rowSubTotalLast
        '
        Me.rowSubTotalLast.Location = New System.Drawing.Point(120, 18)
        Me.rowSubTotalLast.Name = "rowSubTotalLast"
        Me.rowSubTotalLast.Size = New System.Drawing.Size(40, 18)
        Me.rowSubTotalLast.TabIndex = 4
        Me.rowSubTotalLast.Text = "Last"
        '
        'rowSubTotalFirst
        '
        Me.rowSubTotalFirst.Location = New System.Drawing.Point(68, 18)
        Me.rowSubTotalFirst.Name = "rowSubTotalFirst"
        Me.rowSubTotalFirst.Size = New System.Drawing.Size(41, 18)
        Me.rowSubTotalFirst.TabIndex = 3
        Me.rowSubTotalFirst.Text = "First"
        '
        'radGroupBox1
        '
        Me.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.radGroupBox1.Controls.Add(Me.rowGrandTotalNone)
        Me.radGroupBox1.Controls.Add(Me.rowGrandTotalLast)
        Me.radGroupBox1.Controls.Add(Me.rowGrandTotalFirst)
        Me.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.radGroupBox1.HeaderText = "Rows GrandTotals Position"
        Me.radGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.radGroupBox1.Name = "radGroupBox1"
        Me.radGroupBox1.Size = New System.Drawing.Size(180, 40)
        Me.radGroupBox1.TabIndex = 3
        Me.radGroupBox1.Text = "Rows GrandTotals Position"
        '
        'rowGrandTotalNone
        '
        Me.rowGrandTotalNone.Location = New System.Drawing.Point(9, 18)
        Me.rowGrandTotalNone.Name = "rowGrandTotalNone"
        Me.rowGrandTotalNone.Size = New System.Drawing.Size(48, 18)
        Me.rowGrandTotalNone.TabIndex = 2
        Me.rowGrandTotalNone.Text = "None"
        '
        'rowGrandTotalLast
        '
        Me.rowGrandTotalLast.Location = New System.Drawing.Point(120, 18)
        Me.rowGrandTotalLast.Name = "rowGrandTotalLast"
        Me.rowGrandTotalLast.Size = New System.Drawing.Size(40, 18)
        Me.rowGrandTotalLast.TabIndex = 1
        Me.rowGrandTotalLast.Text = "Last"
        '
        'rowGrandTotalFirst
        '
        Me.rowGrandTotalFirst.Location = New System.Drawing.Point(68, 18)
        Me.rowGrandTotalFirst.Name = "rowGrandTotalFirst"
        Me.rowGrandTotalFirst.Size = New System.Drawing.Size(41, 18)
        Me.rowGrandTotalFirst.TabIndex = 0
        Me.rowGrandTotalFirst.Text = "First"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(108.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(808, 293)
        Me.RadPageViewPage4.Text = "Pivot Grid & Chart"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadChartView1)
        Me.SplitContainer3.Size = New System.Drawing.Size(808, 293)
        Me.SplitContainer3.SplitterDistance = 153
        Me.SplitContainer3.TabIndex = 0
        '
        'RadChartView1
        '
        Me.RadChartView1.AreaDesign = CartesianArea1
        Me.RadChartView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadChartView1.Location = New System.Drawing.Point(0, 0)
        Me.RadChartView1.Name = "RadChartView1"
        Me.RadChartView1.ShowGrid = False
        Me.RadChartView1.Size = New System.Drawing.Size(808, 136)
        Me.RadChartView1.TabIndex = 0
        Me.RadChartView1.Text = "RadChartView1"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadChartView2)
        Me.RadPageViewPage5.Controls.Add(Me.Panel2)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(43.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(763, 314)
        Me.RadPageViewPage5.Text = "Chart"
        '
        'RadChartView2
        '
        Me.RadChartView2.AreaDesign = CartesianArea2
        Me.RadChartView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadChartView2.Location = New System.Drawing.Point(0, 0)
        Me.RadChartView2.Name = "RadChartView2"
        Me.RadChartView2.ShowGrid = False
        Me.RadChartView2.Size = New System.Drawing.Size(567, 314)
        Me.RadChartView2.TabIndex = 1
        Me.RadChartView2.Text = "RadChartView2"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadCollapsiblePanel2)
        Me.Panel2.Controls.Add(Me.RadCollapsiblePanel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(567, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(196, 314)
        Me.Panel2.TabIndex = 2
        '
        'RadCollapsiblePanel2
        '
        Me.RadCollapsiblePanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadCollapsiblePanel2.HeaderText = "Show Me"
        Me.RadCollapsiblePanel2.Location = New System.Drawing.Point(0, 169)
        Me.RadCollapsiblePanel2.Name = "RadCollapsiblePanel2"
        Me.RadCollapsiblePanel2.OwnerBoundsCache = New System.Drawing.Rectangle(0, 21, 196, 293)
        '
        'RadCollapsiblePanel2.PanelContainer
        '
        Me.RadCollapsiblePanel2.PanelContainer.Controls.Add(Me.RadButton8)
        Me.RadCollapsiblePanel2.PanelContainer.Controls.Add(Me.RadButton7)
        Me.RadCollapsiblePanel2.PanelContainer.Controls.Add(Me.RadButton6)
        Me.RadCollapsiblePanel2.PanelContainer.Controls.Add(Me.RadButton5)
        Me.RadCollapsiblePanel2.PanelContainer.Controls.Add(Me.RadButton4)
        Me.RadCollapsiblePanel2.PanelContainer.Controls.Add(Me.RadButton3)
        Me.RadCollapsiblePanel2.PanelContainer.Size = New System.Drawing.Size(0, 0)
        Me.RadCollapsiblePanel2.Size = New System.Drawing.Size(196, 145)
        Me.RadCollapsiblePanel2.TabIndex = 58
        Me.RadCollapsiblePanel2.Text = "RadCollapsiblePanel2"
        '
        'RadButton8
        '
        Me.RadButton8.Image = Global.ERP.My.Resources.Resources.C6
        Me.RadButton8.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton8.Location = New System.Drawing.Point(56, 99)
        Me.RadButton8.Name = "RadButton8"
        Me.RadButton8.Size = New System.Drawing.Size(44, 40)
        Me.RadButton8.TabIndex = 4
        '
        'RadButton7
        '
        Me.RadButton7.Image = Global.ERP.My.Resources.Resources.C5
        Me.RadButton7.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton7.Location = New System.Drawing.Point(56, 8)
        Me.RadButton7.Name = "RadButton7"
        Me.RadButton7.Size = New System.Drawing.Size(44, 40)
        Me.RadButton7.TabIndex = 3
        '
        'RadButton6
        '
        Me.RadButton6.Image = Global.ERP.My.Resources.Resources.C4
        Me.RadButton6.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton6.Location = New System.Drawing.Point(6, 99)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(44, 40)
        Me.RadButton6.TabIndex = 1
        '
        'RadButton5
        '
        Me.RadButton5.Image = Global.ERP.My.Resources.Resources.C3
        Me.RadButton5.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton5.Location = New System.Drawing.Point(56, 53)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(44, 40)
        Me.RadButton5.TabIndex = 2
        '
        'RadButton4
        '
        Me.RadButton4.Image = Global.ERP.My.Resources.Resources.C2
        Me.RadButton4.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton4.Location = New System.Drawing.Point(6, 53)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(44, 40)
        Me.RadButton4.TabIndex = 1
        '
        'RadButton3
        '
        Me.RadButton3.Image = Global.ERP.My.Resources.Resources.C1
        Me.RadButton3.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton3.Location = New System.Drawing.Point(6, 7)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(44, 40)
        Me.RadButton3.TabIndex = 0
        '
        'RadCollapsiblePanel1
        '
        Me.RadCollapsiblePanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadCollapsiblePanel1.HeaderText = "Custom Setting"
        Me.RadCollapsiblePanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadCollapsiblePanel1.Name = "RadCollapsiblePanel1"
        Me.RadCollapsiblePanel1.OwnerBoundsCache = New System.Drawing.Rectangle(0, 0, 196, 158)
        '
        'RadCollapsiblePanel1.PanelContainer
        '
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.chkScroll)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.cboChartType)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.RadButton1)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.chkShowLables)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.txtLableRotation)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.MyLabel5)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.MyLabel2)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.cboCombineMode)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.MyLabel3)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.MyLabel4)
        Me.RadCollapsiblePanel1.PanelContainer.Controls.Add(Me.cboOrientation)
        Me.RadCollapsiblePanel1.PanelContainer.Size = New System.Drawing.Size(0, 0)
        Me.RadCollapsiblePanel1.Size = New System.Drawing.Size(196, 169)
        Me.RadCollapsiblePanel1.TabIndex = 57
        Me.RadCollapsiblePanel1.Text = "Custom Setting"
        '
        'chkScroll
        '
        Me.chkScroll.Location = New System.Drawing.Point(91, 103)
        Me.chkScroll.MyLinkLable1 = Nothing
        Me.chkScroll.MyLinkLable2 = Nothing
        Me.chkScroll.Name = "chkScroll"
        Me.chkScroll.Size = New System.Drawing.Size(78, 18)
        Me.chkScroll.TabIndex = 57
        Me.chkScroll.Tag1 = Nothing
        Me.chkScroll.Text = "Show Scroll"
        '
        'cboChartType
        '
        Me.cboChartType.AutoCompleteDisplayMember = Nothing
        Me.cboChartType.AutoCompleteValueMember = Nothing
        Me.cboChartType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboChartType.Location = New System.Drawing.Point(91, 3)
        Me.cboChartType.MendatroryField = False
        Me.cboChartType.MyLinkLable1 = Me.MyLabel2
        Me.cboChartType.MyLinkLable2 = Nothing
        Me.cboChartType.Name = "cboChartType"
        Me.cboChartType.Size = New System.Drawing.Size(94, 20)
        Me.cboChartType.TabIndex = 42
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(2, 5)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel2.TabIndex = 43
        Me.MyLabel2.Text = "Chart Type"
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(3, 124)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(188, 16)
        Me.RadButton1.TabIndex = 46
        Me.RadButton1.Text = ">>"
        '
        'chkShowLables
        '
        Me.chkShowLables.Location = New System.Drawing.Point(3, 103)
        Me.chkShowLables.MyLinkLable1 = Nothing
        Me.chkShowLables.MyLinkLable2 = Nothing
        Me.chkShowLables.Name = "chkShowLables"
        Me.chkShowLables.Size = New System.Drawing.Size(82, 18)
        Me.chkShowLables.TabIndex = 53
        Me.chkShowLables.Tag1 = Nothing
        Me.chkShowLables.Text = "Show Lables"
        '
        'txtLableRotation
        '
        Me.txtLableRotation.Location = New System.Drawing.Point(91, 79)
        Me.txtLableRotation.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.txtLableRotation.Name = "txtLableRotation"
        Me.txtLableRotation.Size = New System.Drawing.Size(46, 20)
        Me.txtLableRotation.TabIndex = 56
        Me.txtLableRotation.TabStop = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(1, 81)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel5.TabIndex = 55
        Me.MyLabel5.Text = "Label Rotaion"
        '
        'cboCombineMode
        '
        Me.cboCombineMode.AutoCompleteDisplayMember = Nothing
        Me.cboCombineMode.AutoCompleteValueMember = Nothing
        Me.cboCombineMode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCombineMode.Location = New System.Drawing.Point(91, 29)
        Me.cboCombineMode.MendatroryField = False
        Me.cboCombineMode.MyLinkLable1 = Me.MyLabel3
        Me.cboCombineMode.MyLinkLable2 = Nothing
        Me.cboCombineMode.Name = "cboCombineMode"
        Me.cboCombineMode.Size = New System.Drawing.Size(94, 20)
        Me.cboCombineMode.TabIndex = 47
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(-1, 31)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel3.TabIndex = 48
        Me.MyLabel3.Text = "Combine Mode"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(2, 55)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel4.TabIndex = 50
        Me.MyLabel4.Text = "Orientation"
        '
        'cboOrientation
        '
        Me.cboOrientation.AutoCompleteDisplayMember = Nothing
        Me.cboOrientation.AutoCompleteValueMember = Nothing
        Me.cboOrientation.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboOrientation.Location = New System.Drawing.Point(91, 53)
        Me.cboOrientation.MendatroryField = False
        Me.cboOrientation.MyLinkLable1 = Me.MyLabel4
        Me.cboOrientation.MyLinkLable2 = Nothing
        Me.cboOrientation.Name = "cboOrientation"
        Me.cboOrientation.Size = New System.Drawing.Size(94, 20)
        Me.cboOrientation.TabIndex = 49
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 417)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(829, 30)
        Me.Panel1.TabIndex = 3
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(155, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(69, 22)
        Me.btnReset.TabIndex = 9
        Me.btnReset.Text = "Reset"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(622, 4)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(128, 22)
        Me.RadButton2.TabIndex = 8
        Me.RadButton2.Text = "Get Report Code"
        Me.RadButton2.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(754, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(6, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'frmCreateBIReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(829, 447)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmCreateBIReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "BI Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.lblTransactionTypeColumn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDrillDownColumn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDrillDownTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDrillDownReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDrillDownNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDashboard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.RadPageViewPage6.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.gvFilter.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gvFilterInner.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvFilterInner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.RadButton9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        CType(Me.pg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.SplitPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel3.ResumeLayout(False)
        CType(Me.radGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.radGroupBox4.ResumeLayout(False)
        Me.radGroupBox4.PerformLayout()
        CType(Me.columnSubTotalNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.columnSubTotalLast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.columnSubTotalFirst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.radGroupBox3.ResumeLayout(False)
        Me.radGroupBox3.PerformLayout()
        CType(Me.columnGrandTotalNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.columnGrandTotalLast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.columnGrandTotalFirst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.radGroupBox2.ResumeLayout(False)
        Me.radGroupBox2.PerformLayout()
        CType(Me.rowSubTotalNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rowSubTotalLast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rowSubTotalFirst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.radGroupBox1.ResumeLayout(False)
        Me.radGroupBox1.PerformLayout()
        CType(Me.rowGrandTotalNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rowGrandTotalLast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rowGrandTotalFirst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.RadChartView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.RadChartView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.RadCollapsiblePanel2.PanelContainer.ResumeLayout(False)
        CType(Me.RadCollapsiblePanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadCollapsiblePanel1.PanelContainer.ResumeLayout(False)
        Me.RadCollapsiblePanel1.PanelContainer.PerformLayout()
        CType(Me.RadCollapsiblePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkScroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboChartType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowLables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLableRotation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCombineMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboOrientation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents queryBuilder As ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder
    Friend WithEvents MssqlMetadataProvider1 As ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLMetadataProvider
    Friend WithEvents MssqlSyntaxProvider1 As ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLSyntaxProvider
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents PlainTextSQLBuilder1 As ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents pg1 As Telerik.WinControls.UI.RadPivotGrid
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadChartView1 As Telerik.WinControls.UI.RadChartView
    Friend WithEvents RadPageViewPage6 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvFilter As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtReportModule As common.UserControls.txtFinder
    Friend WithEvents RadChartView2 As Telerik.WinControls.UI.RadChartView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cboChartType As common.Controls.MyComboBox
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents cboOrientation As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents cboCombineMode As common.Controls.MyComboBox
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Private WithEvents radPivotFieldList1 As Telerik.WinControls.UI.RadPivotFieldList
    Friend WithEvents SplitPanel3 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cboSetting As common.Controls.MyComboBox
    Friend WithEvents radGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rowSubTotalNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rowSubTotalLast As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rowSubTotalFirst As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rowGrandTotalNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rowGrandTotalLast As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rowGrandTotalFirst As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents columnSubTotalNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents columnSubTotalLast As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents columnSubTotalFirst As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents columnGrandTotalNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents columnGrandTotalLast As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents columnGrandTotalFirst As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDashboard As common.Controls.MyCheckBox
    Friend WithEvents chkShowLables As common.Controls.MyCheckBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtLableRotation As Telerik.WinControls.UI.RadSpinEditor
    Friend WithEvents RadCollapsiblePanel1 As Telerik.WinControls.UI.RadCollapsiblePanel
    Friend WithEvents RadCollapsiblePanel2 As Telerik.WinControls.UI.RadCollapsiblePanel
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton6 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton5 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton8 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton7 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvFilterInner As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadButton9 As Telerik.WinControls.UI.RadButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sqlQueryText As ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor
    Friend WithEvents chkScroll As common.Controls.MyCheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtDrilldownFilter As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtDrilldownReport As common.UserControls.txtFinder
    Friend WithEvents rbtnDrillDownTransaction As common.Controls.MyRadioButton
    Friend WithEvents rbtnDrillDownReport As common.Controls.MyRadioButton
    Friend WithEvents rbtnDrillDownNA As common.Controls.MyRadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblTransactionTypeColumn As common.Controls.MyLabel
    Friend WithEvents lblDrillDownColumn As common.Controls.MyLabel
End Class

