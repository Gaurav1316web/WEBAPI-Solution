<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateBIFilter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreateBIFilter))
        Me.queryBuilder = New ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder()
        Me.MssqlMetadataProvider1 = New ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLMetadataProvider(Me.components)
        Me.MssqlSyntaxProvider1 = New ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLSyntaxProvider(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.sqlQueryText = New ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor()
        Me.PlainTextSQLBuilder1 = New ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder(Me.components)
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage6 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvFilter = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.cbt = New common.MyCheckBoxTreeView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboLevel = New common.Controls.MyComboBox()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkSecurityLocation = New common.Controls.MyCheckBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage6.SuspendLayout()
        CType(Me.gvFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvFilter.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSecurityLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.queryBuilder.Size = New System.Drawing.Size(856, 213)
        Me.queryBuilder.TabIndex = 0
        '
        'MssqlMetadataProvider1
        '
        Me.MssqlMetadataProvider1.Connection = Nothing
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
        Me.SplitContainer1.Size = New System.Drawing.Size(856, 305)
        Me.SplitContainer1.SplitterDistance = 213
        Me.SplitContainer1.TabIndex = 1
        '
        'sqlQueryText
        '
        Me.sqlQueryText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sqlQueryText.CommentColor = System.Drawing.Color.Green
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
        Me.sqlQueryText.Size = New System.Drawing.Size(856, 88)
        Me.sqlQueryText.StringColor = System.Drawing.Color.DarkRed
        Me.sqlQueryText.TabIndex = 0
        Me.sqlQueryText.Text = "SqlTextEditor"
        Me.sqlQueryText.TextColor = System.Drawing.SystemColors.WindowText
        Me.sqlQueryText.TextPadding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        '
        'PlainTextSQLBuilder1
        '
        Me.PlainTextSQLBuilder1.DynamicIndents = False
        Me.PlainTextSQLBuilder1.DynamicRightMargin = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.FromClauseFormat.NewLineAfterDatasource = False
        Me.PlainTextSQLBuilder1.ExpressionSubqueryFormat.MainPartsFromNewLine = False
        Me.PlainTextSQLBuilder1.QueryBuilder = Me.queryBuilder
        Me.PlainTextSQLBuilder1.RightMargin = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage6)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(877, 353)
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
        Me.RadPageViewPage1.Size = New System.Drawing.Size(856, 305)
        Me.RadPageViewPage1.Text = "Query"
        '
        'RadPageViewPage6
        '
        Me.RadPageViewPage6.Controls.Add(Me.gvFilter)
        Me.RadPageViewPage6.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage6.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage6.Name = "RadPageViewPage6"
        Me.RadPageViewPage6.Size = New System.Drawing.Size(856, 335)
        Me.RadPageViewPage6.Text = "Filters"
        '
        'gvFilter
        '
        Me.gvFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvFilter.Location = New System.Drawing.Point(0, 0)
        Me.gvFilter.Name = "gvFilter"
        Me.gvFilter.Size = New System.Drawing.Size(856, 335)
        Me.gvFilter.TabIndex = 1
        Me.gvFilter.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(64.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(763, 335)
        Me.RadPageViewPage2.Text = "Data Grid"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(763, 335)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.cbt)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(63.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(856, 335)
        Me.RadPageViewPage3.Text = "Tree view"
        '
        'cbt
        '
        Me.cbt.DataSource = Nothing
        Me.cbt.DisplayMember = "Name"
        Me.cbt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbt.Location = New System.Drawing.Point(0, 0)
        Me.cbt.Name = "cbt"
        Me.cbt.ParentValue = "ParentCode"
        Me.cbt.Size = New System.Drawing.Size(856, 335)
        Me.cbt.TabIndex = 0
        Me.cbt.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 420)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(877, 30)
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
        Me.RadButton2.Location = New System.Drawing.Point(670, 4)
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
        Me.btnClose.Location = New System.Drawing.Point(802, 4)
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
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkSecurityLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboLevel)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel21)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(877, 420)
        Me.SplitContainer2.SplitterDistance = 63
        Me.SplitContainer2.TabIndex = 4
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(784, 11)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel2.TabIndex = 45
        Me.MyLabel2.Text = "Level"
        '
        'cboLevel
        '
        Me.cboLevel.AutoCompleteDisplayMember = Nothing
        Me.cboLevel.AutoCompleteValueMember = Nothing
        Me.cboLevel.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLevel.Location = New System.Drawing.Point(824, 9)
        Me.cboLevel.MendatroryField = True
        Me.cboLevel.MyLinkLable1 = Me.MyLabel2
        Me.cboLevel.MyLinkLable2 = Nothing
        Me.cboLevel.Name = "cboLevel"
        Me.cboLevel.Size = New System.Drawing.Size(49, 20)
        Me.cboLevel.TabIndex = 44
        '
        'RadLabel21
        '
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(614, 10)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(31, 16)
        Me.RadLabel21.TabIndex = 43
        Me.RadLabel21.Text = "Type"
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(654, 8)
        Me.cboType.MendatroryField = True
        Me.cboType.MyLinkLable1 = Me.RadLabel21
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(124, 20)
        Me.cboType.TabIndex = 42
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(310, 10)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 27
        Me.RadLabel3.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(385, 9)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(222, 18)
        Me.txtDesc.TabIndex = 26
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(284, 6)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 2
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(82, 6)
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
        Me.MyLabel1.Location = New System.Drawing.Point(6, 7)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "Report Code"
        '
        'chkSecurityLocation
        '
        Me.chkSecurityLocation.Location = New System.Drawing.Point(310, 32)
        Me.chkSecurityLocation.MyLinkLable1 = Nothing
        Me.chkSecurityLocation.MyLinkLable2 = Nothing
        Me.chkSecurityLocation.Name = "chkSecurityLocation"
        Me.chkSecurityLocation.Size = New System.Drawing.Size(116, 18)
        Me.chkSecurityLocation.TabIndex = 46
        Me.chkSecurityLocation.Tag1 = Nothing
        Me.chkSecurityLocation.Text = "Is Security Location"
        '
        'frmCreateBIFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 450)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmCreateBIFilter"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "BI Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage6.ResumeLayout(False)
        CType(Me.gvFilter.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSecurityLocation, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage6 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvFilter As common.UserControls.MyRadGridView
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents cbt As common.MyCheckBoxTreeView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cboLevel As common.Controls.MyComboBox
    Friend WithEvents sqlQueryText As ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor
    Friend WithEvents chkSecurityLocation As common.Controls.MyCheckBox
End Class

