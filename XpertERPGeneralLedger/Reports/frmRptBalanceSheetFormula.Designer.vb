<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptBalanceSheetFormula
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnQuickExp = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.grpLocaSegment = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocSeg = New common.MyCheckBoxGrid()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.chkShowTotalRow = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludeingClosingEntry = New common.Controls.MyCheckBox()
        Me.chkIndAS = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblGLMainAccountGroup = New common.Controls.MyLabel()
        Me.txtMultGLMainAmountGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblAccountSubGroup = New common.Controls.MyLabel()
        Me.txtmultAccountSubGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblAccountMainGroup = New common.Controls.MyLabel()
        Me.txtmultAccountMainGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblAccountGroup = New common.Controls.MyLabel()
        Me.txtmultAccountGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblCompany = New common.Controls.MyLabel()
        Me.txtmultLocationSegment = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgGLMainAccount = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rbtnGlMainAccountSelect = New common.Controls.MyRadioButton()
        Me.rbtnGlMainAccountAll = New common.Controls.MyRadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtnTreeView = New common.Controls.MyRadioButton()
        Me.ChkGLMainAccount = New common.Controls.MyRadioButton()
        Me.ChkGLAccount = New common.Controls.MyRadioButton()
        Me.rbtnNA = New common.Controls.MyRadioButton()
        Me.rbtnGLRollupLevel = New common.Controls.MyRadioButton()
        Me.rbtnGLGroupLevel = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.CbgAccountmainGrp = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.ChkAccountMainGrpSelect = New common.Controls.MyRadioButton()
        Me.ChkAccountMainGrpAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgGLACGrp = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbtnGLAccountGroupSelect = New common.Controls.MyRadioButton()
        Me.rbtnGLAccountGrpAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgRollupAC = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbtnRollupSelect = New common.Controls.MyRadioButton()
        Me.rbtnRollupAll = New common.Controls.MyRadioButton()
        Me.chkLocationWise = New common.Controls.MyCheckBox()
        Me.pnlFiscalYear = New System.Windows.Forms.Panel()
        Me.lblFiscalYear = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtFiscalYear = New common.UserControls.txtFinder()
        Me.pnlDateRange = New System.Windows.Forms.Panel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnDateRange = New common.Controls.MyRadioButton()
        Me.rbtnMonthly = New common.Controls.MyRadioButton()
        Me.rbtnQuarterly = New common.Controls.MyRadioButton()
        Me.rbtnHalfYearly = New common.Controls.MyRadioButton()
        Me.rbtnYearly = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromdate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnRestoreLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLocaSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLocaSegment.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.chkShowTotalRow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGLMainAccountGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountSubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountMainGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccountGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.rbtnGlMainAccountSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnGlMainAccountAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.rbtnTreeView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkGLMainAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkGLAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnGLRollupLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnGLGroupLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.ChkAccountMainGrpSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAccountMainGrpAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnGLAccountGroupSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnGLAccountGrpAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnRollupSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnRollupAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFiscalYear.SuspendLayout()
        CType(Me.lblFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDateRange.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnDateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnQuarterly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnHalfYearly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnYearly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(741, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = " Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(152, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnBack)
        Me.Panel1.Controls.Add(Me.btnExport)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 453)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(815, 26)
        Me.Panel1.TabIndex = 1
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(79, 4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(68, 18)
        Me.btnBack.TabIndex = 332
        Me.btnBack.Text = "<< Back "
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF, Me.btnQuickExp})
        Me.btnExport.Location = New System.Drawing.Point(225, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 18)
        Me.btnExport.TabIndex = 330
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'btnQuickExp
        '
        Me.btnQuickExp.AccessibleDescription = "Quick Export"
        Me.btnQuickExp.AccessibleName = "Quick Export"
        Me.btnQuickExp.Name = "btnQuickExp"
        Me.btnQuickExp.Text = "Quick Export"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(6, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = ">>>"
        '
        'grpLocaSegment
        '
        Me.grpLocaSegment.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpLocaSegment.Controls.Add(Me.cbgLocSeg)
        Me.grpLocaSegment.HeaderText = "Location Segment"
        Me.grpLocaSegment.Location = New System.Drawing.Point(8, 205)
        Me.grpLocaSegment.Name = "grpLocaSegment"
        Me.grpLocaSegment.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpLocaSegment.Size = New System.Drawing.Size(204, 75)
        Me.grpLocaSegment.TabIndex = 6
        Me.grpLocaSegment.Text = "Location Segment"
        Me.grpLocaSegment.Visible = False
        '
        'cbgLocSeg
        '
        Me.cbgLocSeg.CheckedValue = Nothing
        Me.cbgLocSeg.DataSource = Nothing
        Me.cbgLocSeg.DisplayMember = "Name"
        Me.cbgLocSeg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocSeg.Location = New System.Drawing.Point(10, 20)
        Me.cbgLocSeg.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocSeg.MyShowHeadrText = False
        Me.cbgLocSeg.Name = "cbgLocSeg"
        Me.cbgLocSeg.Size = New System.Drawing.Size(184, 45)
        Me.cbgLocSeg.TabIndex = 1
        Me.cbgLocSeg.ValueMember = "Code"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(886, 599)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 25)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(815, 428)
        Me.RadPageView1.TabIndex = 3
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(794, 380)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadPanel2)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.lblToDate)
        Me.RadPanel1.Controls.Add(Me.lblFromdate)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(794, 380)
        Me.RadPanel1.TabIndex = 15
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.chkShowTotalRow)
        Me.RadPanel2.Controls.Add(Me.chkIncludeingClosingEntry)
        Me.RadPanel2.Controls.Add(Me.chkIndAS)
        Me.RadPanel2.Controls.Add(Me.lblGLMainAccountGroup)
        Me.RadPanel2.Controls.Add(Me.txtMultGLMainAmountGroup)
        Me.RadPanel2.Controls.Add(Me.lblAccountSubGroup)
        Me.RadPanel2.Controls.Add(Me.txtmultAccountSubGroup)
        Me.RadPanel2.Controls.Add(Me.lblAccountMainGroup)
        Me.RadPanel2.Controls.Add(Me.txtmultAccountMainGroup)
        Me.RadPanel2.Controls.Add(Me.lblAccountGroup)
        Me.RadPanel2.Controls.Add(Me.txtmultAccountGroup)
        Me.RadPanel2.Controls.Add(Me.lblCompany)
        Me.RadPanel2.Controls.Add(Me.txtmultLocationSegment)
        Me.RadPanel2.Controls.Add(Me.RadGroupBox5)
        Me.RadPanel2.Controls.Add(Me.GroupBox2)
        Me.RadPanel2.Controls.Add(Me.RadGroupBox4)
        Me.RadPanel2.Controls.Add(Me.RadGroupBox3)
        Me.RadPanel2.Controls.Add(Me.RadGroupBox2)
        Me.RadPanel2.Controls.Add(Me.chkLocationWise)
        Me.RadPanel2.Controls.Add(Me.pnlFiscalYear)
        Me.RadPanel2.Controls.Add(Me.pnlDateRange)
        Me.RadPanel2.Controls.Add(Me.GroupBox1)
        Me.RadPanel2.Controls.Add(Me.grpLocaSegment)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel2.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(794, 380)
        Me.RadPanel2.TabIndex = 82
        '
        'chkShowTotalRow
        '
        Me.chkShowTotalRow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowTotalRow.Location = New System.Drawing.Point(482, 176)
        Me.chkShowTotalRow.Name = "chkShowTotalRow"
        '
        '
        '
        Me.chkShowTotalRow.RootElement.StretchHorizontally = True
        Me.chkShowTotalRow.RootElement.StretchVertically = True
        Me.chkShowTotalRow.Size = New System.Drawing.Size(169, 16)
        Me.chkShowTotalRow.TabIndex = 405
        Me.chkShowTotalRow.Text = "Show Total Summary Row"
        '
        'chkIncludeingClosingEntry
        '
        Me.chkIncludeingClosingEntry.Location = New System.Drawing.Point(482, 110)
        Me.chkIncludeingClosingEntry.MyLinkLable1 = Nothing
        Me.chkIncludeingClosingEntry.MyLinkLable2 = Nothing
        Me.chkIncludeingClosingEntry.Name = "chkIncludeingClosingEntry"
        Me.chkIncludeingClosingEntry.Size = New System.Drawing.Size(135, 18)
        Me.chkIncludeingClosingEntry.TabIndex = 404
        Me.chkIncludeingClosingEntry.Tag1 = Nothing
        Me.chkIncludeingClosingEntry.Text = "Including Closing Entry"
        '
        'chkIndAS
        '
        Me.chkIndAS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIndAS.Location = New System.Drawing.Point(482, 90)
        Me.chkIndAS.Name = "chkIndAS"
        '
        '
        '
        Me.chkIndAS.RootElement.StretchHorizontally = True
        Me.chkIndAS.RootElement.StretchVertically = True
        Me.chkIndAS.Size = New System.Drawing.Size(89, 16)
        Me.chkIndAS.TabIndex = 403
        Me.chkIndAS.Text = "Ind As"
        '
        'lblGLMainAccountGroup
        '
        Me.lblGLMainAccountGroup.FieldName = Nothing
        Me.lblGLMainAccountGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGLMainAccountGroup.Location = New System.Drawing.Point(7, 174)
        Me.lblGLMainAccountGroup.Name = "lblGLMainAccountGroup"
        Me.lblGLMainAccountGroup.Size = New System.Drawing.Size(126, 18)
        Me.lblGLMainAccountGroup.TabIndex = 402
        Me.lblGLMainAccountGroup.Text = "GL Main Account Group"
        '
        'txtMultGLMainAmountGroup
        '
        Me.txtMultGLMainAmountGroup.arrDispalyMember = Nothing
        Me.txtMultGLMainAmountGroup.arrValueMember = Nothing
        Me.txtMultGLMainAmountGroup.Location = New System.Drawing.Point(136, 173)
        Me.txtMultGLMainAmountGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultGLMainAmountGroup.MyLinkLable1 = Me.lblGLMainAccountGroup
        Me.txtMultGLMainAmountGroup.MyLinkLable2 = Nothing
        Me.txtMultGLMainAmountGroup.MyNullText = "All"
        Me.txtMultGLMainAmountGroup.Name = "txtMultGLMainAmountGroup"
        Me.txtMultGLMainAmountGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtMultGLMainAmountGroup.TabIndex = 401
        '
        'lblAccountSubGroup
        '
        Me.lblAccountSubGroup.FieldName = Nothing
        Me.lblAccountSubGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountSubGroup.Location = New System.Drawing.Point(7, 153)
        Me.lblAccountSubGroup.Name = "lblAccountSubGroup"
        Me.lblAccountSubGroup.Size = New System.Drawing.Size(104, 18)
        Me.lblAccountSubGroup.TabIndex = 400
        Me.lblAccountSubGroup.Text = "Account Sub Group"
        '
        'txtmultAccountSubGroup
        '
        Me.txtmultAccountSubGroup.arrDispalyMember = Nothing
        Me.txtmultAccountSubGroup.arrValueMember = Nothing
        Me.txtmultAccountSubGroup.Location = New System.Drawing.Point(136, 152)
        Me.txtmultAccountSubGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultAccountSubGroup.MyLinkLable1 = Me.lblAccountSubGroup
        Me.txtmultAccountSubGroup.MyLinkLable2 = Nothing
        Me.txtmultAccountSubGroup.MyNullText = "All"
        Me.txtmultAccountSubGroup.Name = "txtmultAccountSubGroup"
        Me.txtmultAccountSubGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtmultAccountSubGroup.TabIndex = 399
        '
        'lblAccountMainGroup
        '
        Me.lblAccountMainGroup.FieldName = Nothing
        Me.lblAccountMainGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountMainGroup.Location = New System.Drawing.Point(8, 111)
        Me.lblAccountMainGroup.Name = "lblAccountMainGroup"
        Me.lblAccountMainGroup.Size = New System.Drawing.Size(110, 18)
        Me.lblAccountMainGroup.TabIndex = 398
        Me.lblAccountMainGroup.Text = "Account Main Group"
        '
        'txtmultAccountMainGroup
        '
        Me.txtmultAccountMainGroup.arrDispalyMember = Nothing
        Me.txtmultAccountMainGroup.arrValueMember = Nothing
        Me.txtmultAccountMainGroup.Location = New System.Drawing.Point(136, 110)
        Me.txtmultAccountMainGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultAccountMainGroup.MyLinkLable1 = Me.lblAccountMainGroup
        Me.txtmultAccountMainGroup.MyLinkLable2 = Nothing
        Me.txtmultAccountMainGroup.MyNullText = "All"
        Me.txtmultAccountMainGroup.Name = "txtmultAccountMainGroup"
        Me.txtmultAccountMainGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtmultAccountMainGroup.TabIndex = 397
        '
        'lblAccountGroup
        '
        Me.lblAccountGroup.FieldName = Nothing
        Me.lblAccountGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountGroup.Location = New System.Drawing.Point(7, 130)
        Me.lblAccountGroup.Name = "lblAccountGroup"
        Me.lblAccountGroup.Size = New System.Drawing.Size(82, 18)
        Me.lblAccountGroup.TabIndex = 396
        Me.lblAccountGroup.Text = "Account Group"
        '
        'txtmultAccountGroup
        '
        Me.txtmultAccountGroup.arrDispalyMember = Nothing
        Me.txtmultAccountGroup.arrValueMember = Nothing
        Me.txtmultAccountGroup.Location = New System.Drawing.Point(136, 131)
        Me.txtmultAccountGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultAccountGroup.MyLinkLable1 = Me.lblAccountGroup
        Me.txtmultAccountGroup.MyLinkLable2 = Nothing
        Me.txtmultAccountGroup.MyNullText = "All"
        Me.txtmultAccountGroup.Name = "txtmultAccountGroup"
        Me.txtmultAccountGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtmultAccountGroup.TabIndex = 395
        '
        'lblCompany
        '
        Me.lblCompany.FieldName = Nothing
        Me.lblCompany.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(7, 90)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(96, 18)
        Me.lblCompany.TabIndex = 394
        Me.lblCompany.Text = "Location Segment"
        '
        'txtmultLocationSegment
        '
        Me.txtmultLocationSegment.arrDispalyMember = Nothing
        Me.txtmultLocationSegment.arrValueMember = Nothing
        Me.txtmultLocationSegment.Location = New System.Drawing.Point(136, 89)
        Me.txtmultLocationSegment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmultLocationSegment.MyLinkLable1 = Me.lblCompany
        Me.txtmultLocationSegment.MyLinkLable2 = Nothing
        Me.txtmultLocationSegment.MyNullText = "All"
        Me.txtmultLocationSegment.Name = "txtmultLocationSegment"
        Me.txtmultLocationSegment.Size = New System.Drawing.Size(344, 19)
        Me.txtmultLocationSegment.TabIndex = 393
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgGLMainAccount)
        Me.RadGroupBox5.Controls.Add(Me.Panel6)
        Me.RadGroupBox5.HeaderText = "GL Main Account Group"
        Me.RadGroupBox5.Location = New System.Drawing.Point(8, 296)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(204, 73)
        Me.RadGroupBox5.TabIndex = 92
        Me.RadGroupBox5.Text = "GL Main Account Group"
        Me.RadGroupBox5.Visible = False
        '
        'cbgGLMainAccount
        '
        Me.cbgGLMainAccount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbgGLMainAccount.CheckedValue = Nothing
        Me.cbgGLMainAccount.DataSource = Nothing
        Me.cbgGLMainAccount.DisplayMember = "Name"
        Me.cbgGLMainAccount.Location = New System.Drawing.Point(10, 43)
        Me.cbgGLMainAccount.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgGLMainAccount.MyShowHeadrText = False
        Me.cbgGLMainAccount.Name = "cbgGLMainAccount"
        Me.cbgGLMainAccount.Size = New System.Drawing.Size(184, 20)
        Me.cbgGLMainAccount.TabIndex = 1
        Me.cbgGLMainAccount.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rbtnGlMainAccountSelect)
        Me.Panel6.Controls.Add(Me.rbtnGlMainAccountAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(184, 23)
        Me.Panel6.TabIndex = 0
        '
        'rbtnGlMainAccountSelect
        '
        Me.rbtnGlMainAccountSelect.Location = New System.Drawing.Point(129, 2)
        Me.rbtnGlMainAccountSelect.MyLinkLable1 = Nothing
        Me.rbtnGlMainAccountSelect.MyLinkLable2 = Nothing
        Me.rbtnGlMainAccountSelect.Name = "rbtnGlMainAccountSelect"
        Me.rbtnGlMainAccountSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnGlMainAccountSelect.TabIndex = 2
        Me.rbtnGlMainAccountSelect.Text = "Select"
        '
        'rbtnGlMainAccountAll
        '
        Me.rbtnGlMainAccountAll.Location = New System.Drawing.Point(87, 2)
        Me.rbtnGlMainAccountAll.MyLinkLable1 = Nothing
        Me.rbtnGlMainAccountAll.MyLinkLable2 = Nothing
        Me.rbtnGlMainAccountAll.Name = "rbtnGlMainAccountAll"
        Me.rbtnGlMainAccountAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnGlMainAccountAll.TabIndex = 2
        Me.rbtnGlMainAccountAll.Text = "All"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtnTreeView)
        Me.GroupBox2.Controls.Add(Me.ChkGLMainAccount)
        Me.GroupBox2.Controls.Add(Me.ChkGLAccount)
        Me.GroupBox2.Controls.Add(Me.rbtnNA)
        Me.GroupBox2.Controls.Add(Me.rbtnGLRollupLevel)
        Me.GroupBox2.Controls.Add(Me.rbtnGLGroupLevel)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 41)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(731, 43)
        Me.GroupBox2.TabIndex = 90
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Report Level"
        '
        'rbtnTreeView
        '
        Me.rbtnTreeView.Location = New System.Drawing.Point(599, 17)
        Me.rbtnTreeView.MyLinkLable1 = Nothing
        Me.rbtnTreeView.MyLinkLable2 = Nothing
        Me.rbtnTreeView.Name = "rbtnTreeView"
        Me.rbtnTreeView.Size = New System.Drawing.Size(69, 18)
        Me.rbtnTreeView.TabIndex = 3
        Me.rbtnTreeView.TabStop = False
        Me.rbtnTreeView.Text = "Tree View"
        '
        'ChkGLMainAccount
        '
        Me.ChkGLMainAccount.Location = New System.Drawing.Point(349, 17)
        Me.ChkGLMainAccount.MyLinkLable1 = Nothing
        Me.ChkGLMainAccount.MyLinkLable2 = Nothing
        Me.ChkGLMainAccount.Name = "ChkGLMainAccount"
        Me.ChkGLMainAccount.Size = New System.Drawing.Size(134, 18)
        Me.ChkGLMainAccount.TabIndex = 2
        Me.ChkGLMainAccount.TabStop = False
        Me.ChkGLMainAccount.Text = "GL Main Account Level"
        '
        'ChkGLAccount
        '
        Me.ChkGLAccount.Location = New System.Drawing.Point(489, 17)
        Me.ChkGLAccount.MyLinkLable1 = Nothing
        Me.ChkGLAccount.MyLinkLable2 = Nothing
        Me.ChkGLAccount.Name = "ChkGLAccount"
        Me.ChkGLAccount.Size = New System.Drawing.Size(106, 18)
        Me.ChkGLAccount.TabIndex = 1
        Me.ChkGLAccount.TabStop = False
        Me.ChkGLAccount.Text = "GL Account Level"
        '
        'rbtnNA
        '
        Me.rbtnNA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnNA.Location = New System.Drawing.Point(50, 17)
        Me.rbtnNA.MyLinkLable1 = Nothing
        Me.rbtnNA.MyLinkLable2 = Nothing
        Me.rbtnNA.Name = "rbtnNA"
        Me.rbtnNA.Size = New System.Drawing.Size(40, 18)
        Me.rbtnNA.TabIndex = 1
        Me.rbtnNA.TabStop = False
        Me.rbtnNA.Text = "N/A"
        Me.rbtnNA.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnGLRollupLevel
        '
        Me.rbtnGLRollupLevel.Location = New System.Drawing.Point(214, 17)
        Me.rbtnGLRollupLevel.MyLinkLable1 = Nothing
        Me.rbtnGLRollupLevel.MyLinkLable2 = Nothing
        Me.rbtnGLRollupLevel.Name = "rbtnGLRollupLevel"
        Me.rbtnGLRollupLevel.Size = New System.Drawing.Size(118, 18)
        Me.rbtnGLRollupLevel.TabIndex = 1
        Me.rbtnGLRollupLevel.TabStop = False
        Me.rbtnGLRollupLevel.Text = "GL Sub Group Level"
        '
        'rbtnGLGroupLevel
        '
        Me.rbtnGLGroupLevel.Location = New System.Drawing.Point(101, 17)
        Me.rbtnGLGroupLevel.MyLinkLable1 = Nothing
        Me.rbtnGLGroupLevel.MyLinkLable2 = Nothing
        Me.rbtnGLGroupLevel.Name = "rbtnGLGroupLevel"
        Me.rbtnGLGroupLevel.Size = New System.Drawing.Size(96, 18)
        Me.rbtnGLGroupLevel.TabIndex = 0
        Me.rbtnGLGroupLevel.TabStop = False
        Me.rbtnGLGroupLevel.Text = "GL Group Level"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.CbgAccountmainGrp)
        Me.RadGroupBox4.Controls.Add(Me.Panel5)
        Me.RadGroupBox4.HeaderText = "Account Main Group"
        Me.RadGroupBox4.Location = New System.Drawing.Point(218, 205)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(204, 75)
        Me.RadGroupBox4.TabIndex = 89
        Me.RadGroupBox4.Text = "Account Main Group"
        Me.RadGroupBox4.Visible = False
        '
        'CbgAccountmainGrp
        '
        Me.CbgAccountmainGrp.CheckedValue = Nothing
        Me.CbgAccountmainGrp.DataSource = Nothing
        Me.CbgAccountmainGrp.DisplayMember = "Name"
        Me.CbgAccountmainGrp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CbgAccountmainGrp.Location = New System.Drawing.Point(10, 43)
        Me.CbgAccountmainGrp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.CbgAccountmainGrp.MyShowHeadrText = False
        Me.CbgAccountmainGrp.Name = "CbgAccountmainGrp"
        Me.CbgAccountmainGrp.Size = New System.Drawing.Size(184, 22)
        Me.CbgAccountmainGrp.TabIndex = 1
        Me.CbgAccountmainGrp.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.ChkAccountMainGrpSelect)
        Me.Panel5.Controls.Add(Me.ChkAccountMainGrpAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(184, 23)
        Me.Panel5.TabIndex = 0
        '
        'ChkAccountMainGrpSelect
        '
        Me.ChkAccountMainGrpSelect.Location = New System.Drawing.Point(129, 2)
        Me.ChkAccountMainGrpSelect.MyLinkLable1 = Nothing
        Me.ChkAccountMainGrpSelect.MyLinkLable2 = Nothing
        Me.ChkAccountMainGrpSelect.Name = "ChkAccountMainGrpSelect"
        Me.ChkAccountMainGrpSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkAccountMainGrpSelect.TabIndex = 2
        Me.ChkAccountMainGrpSelect.Text = "Select"
        '
        'ChkAccountMainGrpAll
        '
        Me.ChkAccountMainGrpAll.Location = New System.Drawing.Point(87, 2)
        Me.ChkAccountMainGrpAll.MyLinkLable1 = Nothing
        Me.ChkAccountMainGrpAll.MyLinkLable2 = Nothing
        Me.ChkAccountMainGrpAll.Name = "ChkAccountMainGrpAll"
        Me.ChkAccountMainGrpAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkAccountMainGrpAll.TabIndex = 2
        Me.ChkAccountMainGrpAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgGLACGrp)
        Me.RadGroupBox3.Controls.Add(Me.Panel4)
        Me.RadGroupBox3.HeaderText = "Account Group"
        Me.RadGroupBox3.Location = New System.Drawing.Point(418, 205)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(204, 75)
        Me.RadGroupBox3.TabIndex = 87
        Me.RadGroupBox3.Text = "Account Group"
        Me.RadGroupBox3.Visible = False
        '
        'cbgGLACGrp
        '
        Me.cbgGLACGrp.CheckedValue = Nothing
        Me.cbgGLACGrp.DataSource = Nothing
        Me.cbgGLACGrp.DisplayMember = "Name"
        Me.cbgGLACGrp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgGLACGrp.Location = New System.Drawing.Point(10, 43)
        Me.cbgGLACGrp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgGLACGrp.MyShowHeadrText = False
        Me.cbgGLACGrp.Name = "cbgGLACGrp"
        Me.cbgGLACGrp.Size = New System.Drawing.Size(184, 22)
        Me.cbgGLACGrp.TabIndex = 1
        Me.cbgGLACGrp.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbtnGLAccountGroupSelect)
        Me.Panel4.Controls.Add(Me.rbtnGLAccountGrpAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(184, 23)
        Me.Panel4.TabIndex = 0
        '
        'rbtnGLAccountGroupSelect
        '
        Me.rbtnGLAccountGroupSelect.Location = New System.Drawing.Point(129, 2)
        Me.rbtnGLAccountGroupSelect.MyLinkLable1 = Nothing
        Me.rbtnGLAccountGroupSelect.MyLinkLable2 = Nothing
        Me.rbtnGLAccountGroupSelect.Name = "rbtnGLAccountGroupSelect"
        Me.rbtnGLAccountGroupSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnGLAccountGroupSelect.TabIndex = 2
        Me.rbtnGLAccountGroupSelect.Text = "Select"
        '
        'rbtnGLAccountGrpAll
        '
        Me.rbtnGLAccountGrpAll.Location = New System.Drawing.Point(87, 2)
        Me.rbtnGLAccountGrpAll.MyLinkLable1 = Nothing
        Me.rbtnGLAccountGrpAll.MyLinkLable2 = Nothing
        Me.rbtnGLAccountGrpAll.Name = "rbtnGLAccountGrpAll"
        Me.rbtnGLAccountGrpAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnGLAccountGrpAll.TabIndex = 2
        Me.rbtnGLAccountGrpAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgRollupAC)
        Me.RadGroupBox2.Controls.Add(Me.Panel3)
        Me.RadGroupBox2.HeaderText = "Account Sub Group"
        Me.RadGroupBox2.Location = New System.Drawing.Point(428, 286)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(203, 68)
        Me.RadGroupBox2.TabIndex = 86
        Me.RadGroupBox2.Text = "Account Sub Group"
        Me.RadGroupBox2.Visible = False
        '
        'cbgRollupAC
        '
        Me.cbgRollupAC.CheckedValue = Nothing
        Me.cbgRollupAC.DataSource = Nothing
        Me.cbgRollupAC.DisplayMember = "Name"
        Me.cbgRollupAC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgRollupAC.Location = New System.Drawing.Point(10, 43)
        Me.cbgRollupAC.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgRollupAC.MyShowHeadrText = False
        Me.cbgRollupAC.Name = "cbgRollupAC"
        Me.cbgRollupAC.Size = New System.Drawing.Size(183, 15)
        Me.cbgRollupAC.TabIndex = 1
        Me.cbgRollupAC.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnRollupSelect)
        Me.Panel3.Controls.Add(Me.rbtnRollupAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(183, 23)
        Me.Panel3.TabIndex = 0
        '
        'rbtnRollupSelect
        '
        Me.rbtnRollupSelect.Location = New System.Drawing.Point(129, 2)
        Me.rbtnRollupSelect.MyLinkLable1 = Nothing
        Me.rbtnRollupSelect.MyLinkLable2 = Nothing
        Me.rbtnRollupSelect.Name = "rbtnRollupSelect"
        Me.rbtnRollupSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnRollupSelect.TabIndex = 2
        Me.rbtnRollupSelect.Text = "Select"
        '
        'rbtnRollupAll
        '
        Me.rbtnRollupAll.Location = New System.Drawing.Point(87, 2)
        Me.rbtnRollupAll.MyLinkLable1 = Nothing
        Me.rbtnRollupAll.MyLinkLable2 = Nothing
        Me.rbtnRollupAll.Name = "rbtnRollupAll"
        Me.rbtnRollupAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnRollupAll.TabIndex = 2
        Me.rbtnRollupAll.Text = "All"
        '
        'chkLocationWise
        '
        Me.chkLocationWise.Location = New System.Drawing.Point(482, 131)
        Me.chkLocationWise.MyLinkLable1 = Nothing
        Me.chkLocationWise.MyLinkLable2 = Nothing
        Me.chkLocationWise.Name = "chkLocationWise"
        Me.chkLocationWise.Size = New System.Drawing.Size(164, 18)
        Me.chkLocationWise.TabIndex = 84
        Me.chkLocationWise.Tag1 = Nothing
        Me.chkLocationWise.Text = "Show Location Wise Amount"
        '
        'pnlFiscalYear
        '
        Me.pnlFiscalYear.Controls.Add(Me.lblFiscalYear)
        Me.pnlFiscalYear.Controls.Add(Me.MyLabel3)
        Me.pnlFiscalYear.Controls.Add(Me.txtFiscalYear)
        Me.pnlFiscalYear.Location = New System.Drawing.Point(385, 8)
        Me.pnlFiscalYear.Name = "pnlFiscalYear"
        Me.pnlFiscalYear.Size = New System.Drawing.Size(353, 25)
        Me.pnlFiscalYear.TabIndex = 83
        '
        'lblFiscalYear
        '
        Me.lblFiscalYear.AutoSize = False
        Me.lblFiscalYear.BorderVisible = True
        Me.lblFiscalYear.FieldName = Nothing
        Me.lblFiscalYear.Location = New System.Drawing.Point(191, 3)
        Me.lblFiscalYear.Name = "lblFiscalYear"
        Me.lblFiscalYear.Size = New System.Drawing.Size(158, 18)
        Me.lblFiscalYear.TabIndex = 79
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel3.TabIndex = 78
        Me.MyLabel3.Text = "Fiscal Year"
        '
        'txtFiscalYear
        '
        Me.txtFiscalYear.CalculationExpression = Nothing
        Me.txtFiscalYear.FieldCode = Nothing
        Me.txtFiscalYear.FieldDesc = Nothing
        Me.txtFiscalYear.FieldMaxLength = 0
        Me.txtFiscalYear.FieldName = Nothing
        Me.txtFiscalYear.isCalculatedField = False
        Me.txtFiscalYear.IsSourceFromTable = False
        Me.txtFiscalYear.IsSourceFromValueList = False
        Me.txtFiscalYear.IsUnique = False
        Me.txtFiscalYear.Location = New System.Drawing.Point(66, 3)
        Me.txtFiscalYear.MendatroryField = False
        Me.txtFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiscalYear.MyLinkLable1 = Nothing
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.MyReadOnly = False
        Me.txtFiscalYear.MyShowMasterFormButton = False
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(120, 19)
        Me.txtFiscalYear.TabIndex = 0
        Me.txtFiscalYear.Value = ""
        '
        'pnlDateRange
        '
        Me.pnlDateRange.Controls.Add(Me.MyLabel2)
        Me.pnlDateRange.Controls.Add(Me.MyLabel1)
        Me.pnlDateRange.Controls.Add(Me.txtToDate)
        Me.pnlDateRange.Controls.Add(Me.txtFromDate)
        Me.pnlDateRange.Location = New System.Drawing.Point(382, 7)
        Me.pnlDateRange.Name = "pnlDateRange"
        Me.pnlDateRange.Size = New System.Drawing.Size(353, 25)
        Me.pnlDateRange.TabIndex = 82
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 77
        Me.MyLabel2.Text = "From Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(166, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 78
        Me.MyLabel1.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(217, 3)
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
        Me.txtToDate.TabIndex = 80
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "30/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(68, 3)
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
        Me.txtFromDate.TabIndex = 79
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "30/05/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnDateRange)
        Me.GroupBox1.Controls.Add(Me.rbtnMonthly)
        Me.GroupBox1.Controls.Add(Me.rbtnQuarterly)
        Me.GroupBox1.Controls.Add(Me.rbtnHalfYearly)
        Me.GroupBox1.Controls.Add(Me.rbtnYearly)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(371, 34)
        Me.GroupBox1.TabIndex = 81
        Me.GroupBox1.TabStop = False
        '
        'rbtnDateRange
        '
        Me.rbtnDateRange.Location = New System.Drawing.Point(282, 10)
        Me.rbtnDateRange.MyLinkLable1 = Nothing
        Me.rbtnDateRange.MyLinkLable2 = Nothing
        Me.rbtnDateRange.Name = "rbtnDateRange"
        Me.rbtnDateRange.Size = New System.Drawing.Size(78, 18)
        Me.rbtnDateRange.TabIndex = 2
        Me.rbtnDateRange.Text = "Date Range"
        '
        'rbtnMonthly
        '
        Me.rbtnMonthly.Location = New System.Drawing.Point(214, 10)
        Me.rbtnMonthly.MyLinkLable1 = Nothing
        Me.rbtnMonthly.MyLinkLable2 = Nothing
        Me.rbtnMonthly.Name = "rbtnMonthly"
        Me.rbtnMonthly.Size = New System.Drawing.Size(62, 18)
        Me.rbtnMonthly.TabIndex = 1
        Me.rbtnMonthly.Text = "Monthly"
        '
        'rbtnQuarterly
        '
        Me.rbtnQuarterly.Location = New System.Drawing.Point(141, 10)
        Me.rbtnQuarterly.MyLinkLable1 = Nothing
        Me.rbtnQuarterly.MyLinkLable2 = Nothing
        Me.rbtnQuarterly.Name = "rbtnQuarterly"
        Me.rbtnQuarterly.Size = New System.Drawing.Size(67, 18)
        Me.rbtnQuarterly.TabIndex = 1
        Me.rbtnQuarterly.Text = "Quarterly"
        '
        'rbtnHalfYearly
        '
        Me.rbtnHalfYearly.Location = New System.Drawing.Point(62, 10)
        Me.rbtnHalfYearly.MyLinkLable1 = Nothing
        Me.rbtnHalfYearly.MyLinkLable2 = Nothing
        Me.rbtnHalfYearly.Name = "rbtnHalfYearly"
        Me.rbtnHalfYearly.Size = New System.Drawing.Size(73, 18)
        Me.rbtnHalfYearly.TabIndex = 1
        Me.rbtnHalfYearly.Text = "Half yearly"
        '
        'rbtnYearly
        '
        Me.rbtnYearly.Location = New System.Drawing.Point(6, 10)
        Me.rbtnYearly.MyLinkLable1 = Nothing
        Me.rbtnYearly.MyLinkLable2 = Nothing
        Me.rbtnYearly.Name = "rbtnYearly"
        Me.rbtnYearly.Size = New System.Drawing.Size(50, 18)
        Me.rbtnYearly.TabIndex = 0
        Me.rbtnYearly.Text = "Yearly"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.HeaderText = "Company"
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 37)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(361, 263)
        Me.RadGroupBox1.TabIndex = 7
        Me.RadGroupBox1.Text = "Company"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(192, 8)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 78
        Me.lblToDate.Text = "To Date"
        '
        'lblFromdate
        '
        Me.lblFromdate.FieldName = Nothing
        Me.lblFromdate.Location = New System.Drawing.Point(4, 8)
        Me.lblFromdate.Name = "lblFromdate"
        Me.lblFromdate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromdate.TabIndex = 77
        Me.lblFromdate.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(886, 599)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadMenu1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(815, 25)
        Me.Panel2.TabIndex = 4
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(815, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Layout"
        Me.RadMenuItem1.AccessibleName = "Layout"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSaveLayout, Me.btnRestoreLayout, Me.btnDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Layout"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.AccessibleDescription = "Save Layout"
        Me.btnSaveLayout.AccessibleName = "Save Layout"
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        '
        'btnRestoreLayout
        '
        Me.btnRestoreLayout.AccessibleDescription = "Restore Layout"
        Me.btnRestoreLayout.AccessibleName = "Restore Layout"
        Me.btnRestoreLayout.Name = "btnRestoreLayout"
        Me.btnRestoreLayout.Text = "Restore Layout"
        '
        'btnDeleteLayout
        '
        Me.btnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.btnDeleteLayout.AccessibleName = "Delete Layout"
        Me.btnDeleteLayout.Name = "btnDeleteLayout"
        Me.btnDeleteLayout.Text = "Delete Layout"
        '
        'frmRptBalanceSheetFormula
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(815, 479)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmRptBalanceSheetFormula"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Balance Sheet"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLocaSegment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLocaSegment.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.RadPanel2.PerformLayout()
        CType(Me.chkShowTotalRow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGLMainAccountGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountSubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountMainGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccountGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.rbtnGlMainAccountSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnGlMainAccountAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.rbtnTreeView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkGLMainAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkGLAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnGLRollupLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnGLGroupLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.ChkAccountMainGrpSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAccountMainGrpAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnGLAccountGroupSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnGLAccountGrpAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnRollupSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnRollupAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFiscalYear.ResumeLayout(False)
        Me.pnlFiscalYear.PerformLayout()
        CType(Me.lblFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDateRange.ResumeLayout(False)
        Me.pnlDateRange.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnDateRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnQuarterly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnHalfYearly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnYearly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents grpLocaSegment As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocSeg As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromdate As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnRestoreLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnDateRange As common.Controls.MyRadioButton
    Friend WithEvents rbtnMonthly As common.Controls.MyRadioButton
    Friend WithEvents rbtnQuarterly As common.Controls.MyRadioButton
    Friend WithEvents rbtnHalfYearly As common.Controls.MyRadioButton
    Friend WithEvents rbtnYearly As common.Controls.MyRadioButton
    Friend WithEvents pnlFiscalYear As System.Windows.Forms.Panel
    Friend WithEvents pnlDateRange As System.Windows.Forms.Panel
    Friend WithEvents txtFiscalYear As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblFiscalYear As common.Controls.MyLabel
    Friend WithEvents chkLocationWise As common.Controls.MyCheckBox
    Friend WithEvents cbgRollupAC As common.MyCheckBoxGrid
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbtnRollupSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnRollupAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgGLACGrp As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbtnGLAccountGroupSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnGLAccountGrpAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents CbgAccountmainGrp As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents ChkAccountMainGrpSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkAccountMainGrpAll As common.Controls.MyRadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkGLMainAccount As common.Controls.MyRadioButton
    Friend WithEvents ChkGLAccount As common.Controls.MyRadioButton
    Friend WithEvents rbtnNA As common.Controls.MyRadioButton
    Friend WithEvents rbtnGLRollupLevel As common.Controls.MyRadioButton
    Friend WithEvents rbtnGLGroupLevel As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgGLMainAccount As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents rbtnGlMainAccountSelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnGlMainAccountAll As common.Controls.MyRadioButton
    Friend WithEvents lblGLMainAccountGroup As common.Controls.MyLabel
    Friend WithEvents txtMultGLMainAmountGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAccountSubGroup As common.Controls.MyLabel
    Friend WithEvents txtmultAccountSubGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAccountMainGroup As common.Controls.MyLabel
    Friend WithEvents txtmultAccountMainGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblAccountGroup As common.Controls.MyLabel
    Friend WithEvents txtmultAccountGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCompany As common.Controls.MyLabel
    Friend WithEvents txtmultLocationSegment As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIndAS As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIncludeingClosingEntry As common.Controls.MyCheckBox
    Friend WithEvents chkShowTotalRow As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnTreeView As common.Controls.MyRadioButton
    Friend WithEvents btnQuickExp As Telerik.WinControls.UI.RadMenuItem
End Class

