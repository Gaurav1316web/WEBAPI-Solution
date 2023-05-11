<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptChartOfAccount
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MultChartOfAccount = New common.UserControls.txtMultiSelectFinder()
        Me.lblChartOfAccount = New common.Controls.MyLabel()
        Me.MultGLMainAccount = New common.UserControls.txtMultiSelectFinder()
        Me.lblGLMainAccount = New common.Controls.MyLabel()
        Me.MultAccountSubGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblAccountSubGroup = New common.Controls.MyLabel()
        Me.MultAccountGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblAccountGroup = New common.Controls.MyLabel()
        Me.MultAccountMainGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblAccountMainGroup = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ChkInActive = New common.Controls.MyRadioButton()
        Me.ChkActive = New common.Controls.MyRadioButton()
        Me.ChkAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblChartOfAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGLMainAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountSubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountMainGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.ChkInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(600, 431)
        Me.SplitContainer1.SplitterDistance = 381
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(600, 361)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Report"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Report"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MultChartOfAccount)
        Me.RadPageViewPage1.Controls.Add(Me.lblChartOfAccount)
        Me.RadPageViewPage1.Controls.Add(Me.MultGLMainAccount)
        Me.RadPageViewPage1.Controls.Add(Me.lblGLMainAccount)
        Me.RadPageViewPage1.Controls.Add(Me.MultAccountSubGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblAccountSubGroup)
        Me.RadPageViewPage1.Controls.Add(Me.MultAccountGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblAccountGroup)
        Me.RadPageViewPage1.Controls.Add(Me.MultAccountMainGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblAccountMainGroup)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(579, 313)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'MultChartOfAccount
        '
        Me.MultChartOfAccount.arrDispalyMember = Nothing
        Me.MultChartOfAccount.arrValueMember = Nothing
        Me.MultChartOfAccount.Location = New System.Drawing.Point(168, 141)
        Me.MultChartOfAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultChartOfAccount.MyLinkLable1 = Nothing
        Me.MultChartOfAccount.MyLinkLable2 = Nothing
        Me.MultChartOfAccount.MyNullText = "All"
        Me.MultChartOfAccount.Name = "MultChartOfAccount"
        Me.MultChartOfAccount.Size = New System.Drawing.Size(335, 19)
        Me.MultChartOfAccount.TabIndex = 359
        '
        'lblChartOfAccount
        '
        Me.lblChartOfAccount.FieldName = Nothing
        Me.lblChartOfAccount.Location = New System.Drawing.Point(12, 142)
        Me.lblChartOfAccount.Name = "lblChartOfAccount"
        Me.lblChartOfAccount.Size = New System.Drawing.Size(93, 18)
        Me.lblChartOfAccount.TabIndex = 358
        Me.lblChartOfAccount.Text = "Chart Of Account"
        '
        'MultGLMainAccount
        '
        Me.MultGLMainAccount.arrDispalyMember = Nothing
        Me.MultGLMainAccount.arrValueMember = Nothing
        Me.MultGLMainAccount.Location = New System.Drawing.Point(168, 117)
        Me.MultGLMainAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultGLMainAccount.MyLinkLable1 = Nothing
        Me.MultGLMainAccount.MyLinkLable2 = Nothing
        Me.MultGLMainAccount.MyNullText = "All"
        Me.MultGLMainAccount.Name = "MultGLMainAccount"
        Me.MultGLMainAccount.Size = New System.Drawing.Size(335, 19)
        Me.MultGLMainAccount.TabIndex = 357
        '
        'lblGLMainAccount
        '
        Me.lblGLMainAccount.FieldName = Nothing
        Me.lblGLMainAccount.Location = New System.Drawing.Point(12, 118)
        Me.lblGLMainAccount.Name = "lblGLMainAccount"
        Me.lblGLMainAccount.Size = New System.Drawing.Size(92, 18)
        Me.lblGLMainAccount.TabIndex = 356
        Me.lblGLMainAccount.Text = "GL Main Account"
        '
        'MultAccountSubGroup
        '
        Me.MultAccountSubGroup.arrDispalyMember = Nothing
        Me.MultAccountSubGroup.arrValueMember = Nothing
        Me.MultAccountSubGroup.Location = New System.Drawing.Point(168, 93)
        Me.MultAccountSubGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultAccountSubGroup.MyLinkLable1 = Nothing
        Me.MultAccountSubGroup.MyLinkLable2 = Nothing
        Me.MultAccountSubGroup.MyNullText = "All"
        Me.MultAccountSubGroup.Name = "MultAccountSubGroup"
        Me.MultAccountSubGroup.Size = New System.Drawing.Size(335, 19)
        Me.MultAccountSubGroup.TabIndex = 355
        '
        'lblAccountSubGroup
        '
        Me.lblAccountSubGroup.FieldName = Nothing
        Me.lblAccountSubGroup.Location = New System.Drawing.Point(12, 94)
        Me.lblAccountSubGroup.Name = "lblAccountSubGroup"
        Me.lblAccountSubGroup.Size = New System.Drawing.Size(104, 18)
        Me.lblAccountSubGroup.TabIndex = 354
        Me.lblAccountSubGroup.Text = "Account Sub Group"
        '
        'MultAccountGroup
        '
        Me.MultAccountGroup.arrDispalyMember = Nothing
        Me.MultAccountGroup.arrValueMember = Nothing
        Me.MultAccountGroup.Location = New System.Drawing.Point(168, 69)
        Me.MultAccountGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultAccountGroup.MyLinkLable1 = Nothing
        Me.MultAccountGroup.MyLinkLable2 = Nothing
        Me.MultAccountGroup.MyNullText = "All"
        Me.MultAccountGroup.Name = "MultAccountGroup"
        Me.MultAccountGroup.Size = New System.Drawing.Size(335, 19)
        Me.MultAccountGroup.TabIndex = 353
        '
        'lblAccountGroup
        '
        Me.lblAccountGroup.FieldName = Nothing
        Me.lblAccountGroup.Location = New System.Drawing.Point(12, 70)
        Me.lblAccountGroup.Name = "lblAccountGroup"
        Me.lblAccountGroup.Size = New System.Drawing.Size(82, 18)
        Me.lblAccountGroup.TabIndex = 352
        Me.lblAccountGroup.Text = "Account Group"
        '
        'MultAccountMainGroup
        '
        Me.MultAccountMainGroup.arrDispalyMember = Nothing
        Me.MultAccountMainGroup.arrValueMember = Nothing
        Me.MultAccountMainGroup.Location = New System.Drawing.Point(168, 45)
        Me.MultAccountMainGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultAccountMainGroup.MyLinkLable1 = Nothing
        Me.MultAccountMainGroup.MyLinkLable2 = Nothing
        Me.MultAccountMainGroup.MyNullText = "All"
        Me.MultAccountMainGroup.Name = "MultAccountMainGroup"
        Me.MultAccountMainGroup.Size = New System.Drawing.Size(335, 19)
        Me.MultAccountMainGroup.TabIndex = 351
        '
        'lblAccountMainGroup
        '
        Me.lblAccountMainGroup.FieldName = Nothing
        Me.lblAccountMainGroup.Location = New System.Drawing.Point(12, 46)
        Me.lblAccountMainGroup.Name = "lblAccountMainGroup"
        Me.lblAccountMainGroup.Size = New System.Drawing.Size(110, 18)
        Me.lblAccountMainGroup.TabIndex = 350
        Me.lblAccountMainGroup.Text = "Account Main Group"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.ChkInActive)
        Me.RadGroupBox2.Controls.Add(Me.ChkActive)
        Me.RadGroupBox2.Controls.Add(Me.ChkAll)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(168, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(335, 36)
        Me.RadGroupBox2.TabIndex = 1
        '
        'ChkInActive
        '
        Me.ChkInActive.Location = New System.Drawing.Point(75, 10)
        Me.ChkInActive.MyLinkLable1 = Nothing
        Me.ChkInActive.MyLinkLable2 = Nothing
        Me.ChkInActive.Name = "ChkInActive"
        Me.ChkInActive.Size = New System.Drawing.Size(60, 18)
        Me.ChkInActive.TabIndex = 1
        Me.ChkInActive.TabStop = False
        Me.ChkInActive.Text = "InActive"
        '
        'ChkActive
        '
        Me.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkActive.Location = New System.Drawing.Point(23, 10)
        Me.ChkActive.MyLinkLable1 = Nothing
        Me.ChkActive.MyLinkLable2 = Nothing
        Me.ChkActive.Name = "ChkActive"
        Me.ChkActive.Size = New System.Drawing.Size(51, 18)
        Me.ChkActive.TabIndex = 0
        Me.ChkActive.Text = "Active"
        Me.ChkActive.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'ChkAll
        '
        Me.ChkAll.Location = New System.Drawing.Point(141, 10)
        Me.ChkAll.MyLinkLable1 = Nothing
        Me.ChkAll.MyLinkLable2 = Nothing
        Me.ChkAll.Name = "ChkAll"
        Me.ChkAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkAll.TabIndex = 2
        Me.ChkAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblfromDate)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(488, 189)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(65, 37)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Visible = False
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(217, 11)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(75, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(165, 12)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 14
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(9, 12)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 13
        Me.lblfromDate.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(579, 313)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(579, 313)
        Me.gv.TabIndex = 4
        Me.gv.Text = "gv"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(600, 20)
        Me.RadMenu1.TabIndex = 2
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'btnExp
        '
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(205, 13)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(93, 22)
        Me.btnExp.TabIndex = 313
        Me.btnExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(108, 13)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(95, 22)
        Me.BtnReset.TabIndex = 16
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(506, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 17
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(11, 13)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(95, 22)
        Me.btnGo.TabIndex = 15
        Me.btnGo.Text = ">>>"
        '
        'RptChartOfAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 431)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptChartOfAccount"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptChartOfAccount"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblChartOfAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGLMainAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountSubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountMainGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.ChkInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkInActive As common.Controls.MyRadioButton
    Friend WithEvents ChkActive As common.Controls.MyRadioButton
    Friend WithEvents MultChartOfAccount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblChartOfAccount As common.Controls.MyLabel
    Friend WithEvents MultGLMainAccount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblGLMainAccount As common.Controls.MyLabel
    Friend WithEvents MultAccountSubGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAccountSubGroup As common.Controls.MyLabel
    Friend WithEvents MultAccountGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAccountGroup As common.Controls.MyLabel
    Friend WithEvents MultAccountMainGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAccountMainGroup As common.Controls.MyLabel
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class

