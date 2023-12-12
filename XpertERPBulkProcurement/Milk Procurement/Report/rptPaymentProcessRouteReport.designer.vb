<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptPaymentProcessRouteReport
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gbLedger = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox16 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox17 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnPRBoth = New System.Windows.Forms.RadioButton()
        Me.rbtnRegistered = New System.Windows.Forms.RadioButton()
        Me.rbtnPDCS = New System.Windows.Forms.RadioButton()
        Me.fndMultDCS = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.fndMultMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.fndFinacialYear = New common.UserControls.txtFinder()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.txtDateTo = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtDateFrom = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.btnYearlySummary = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox14 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnDCSWiseAvgFatSnfPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox15 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.dtpDCSWiseAvgFatSnfToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpDCSWiseAvgFatSnfFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox12 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDFGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox13 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.dtpGainLossToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpGainLossFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox10 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtRouteName = New common.UserControls.txtMultiSelectFinder()
        Me.txtMultiMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.btn_DCS_Ledger_Report = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox11 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.dtpToDCS_Ledger = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpFromDCS_Ledger = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnPrintDailySummary = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.dtpDailySummaryToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpDailySummaryFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblMonth = New common.Controls.MyLabel()
        Me.txtMonth = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.btnDCSSummaryPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnLedgerPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.dtpLedgerToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpLedgerFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.gbSummaryReportType = New System.Windows.Forms.GroupBox()
        Me.chkOther = New System.Windows.Forms.RadioButton()
        Me.chkAddition = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkDetailReportType = New System.Windows.Forms.RadioButton()
        Me.chkSummaryReportType = New System.Windows.Forms.RadioButton()
        Me.txtMcc = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtPaymentCycleCode = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gbDateRangeApply = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbBothFromToDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbToDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbFromDate = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbDetails = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtVSP = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkBoth = New Telerik.WinControls.UI.RadRadioButton()
        Me.ChkPosted = New Telerik.WinControls.UI.RadRadioButton()
        Me.ChkUnPosted = New Telerik.WinControls.UI.RadRadioButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtMultiSelectFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gbLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbLedger.SuspendLayout()
        CType(Me.RadGroupBox16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox16.SuspendLayout()
        CType(Me.RadGroupBox17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox17.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnYearlySummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox14.SuspendLayout()
        CType(Me.btnDCSWiseAvgFatSnfPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox15.SuspendLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDCSWiseAvgFatSnfToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDCSWiseAvgFatSnfFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox12.SuspendLayout()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox13.SuspendLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGainLossToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGainLossFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox10.SuspendLayout()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_DCS_Ledger_Report, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox11.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDCS_Ledger, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDCS_Ledger, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        CType(Me.btnPrintDailySummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDailySummaryToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDailySummaryFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDCSSummaryPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.btnLedgerPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLedgerToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLedgerFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSummaryReportType.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.gbDateRangeApply, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDateRangeApply.SuspendLayout()
        CType(Me.rbNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbBothFromToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkUnPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(878, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(878, 493)
        Me.SplitContainer1.SplitterDistance = 464
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(878, 464)
        Me.RadPageView1.TabIndex = 11
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gbLedger)
        Me.RadPageViewPage1.Controls.Add(Me.gbSummaryReportType)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCC)
        Me.RadPageViewPage1.Controls.Add(Me.txtMcc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtPaymentCycleCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(857, 416)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'gbLedger
        '
        Me.gbLedger.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbLedger.Controls.Add(Me.RadGroupBox16)
        Me.gbLedger.Controls.Add(Me.RadGroupBox14)
        Me.gbLedger.Controls.Add(Me.RadGroupBox12)
        Me.gbLedger.Controls.Add(Me.RadGroupBox10)
        Me.gbLedger.Controls.Add(Me.RadGroupBox8)
        Me.gbLedger.Controls.Add(Me.RadGroupBox6)
        Me.gbLedger.Controls.Add(Me.RadGroupBox5)
        Me.gbLedger.HeaderText = ""
        Me.gbLedger.Location = New System.Drawing.Point(0, 0)
        Me.gbLedger.Name = "gbLedger"
        Me.gbLedger.Size = New System.Drawing.Size(856, 416)
        Me.gbLedger.TabIndex = 406
        '
        'RadGroupBox16
        '
        Me.RadGroupBox16.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox16.Controls.Add(Me.RadGroupBox17)
        Me.RadGroupBox16.Controls.Add(Me.fndMultDCS)
        Me.RadGroupBox16.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox16.Controls.Add(Me.fndMultMCC)
        Me.RadGroupBox16.Controls.Add(Me.MyLabel23)
        Me.RadGroupBox16.Controls.Add(Me.fndFinacialYear)
        Me.RadGroupBox16.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox16.Controls.Add(Me.MyLabel22)
        Me.RadGroupBox16.Controls.Add(Me.txtDateTo)
        Me.RadGroupBox16.Controls.Add(Me.txtDateFrom)
        Me.RadGroupBox16.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox16.Controls.Add(Me.btnYearlySummary)
        Me.RadGroupBox16.HeaderText = "Yearly DCS Summary"
        Me.RadGroupBox16.Location = New System.Drawing.Point(283, 137)
        Me.RadGroupBox16.Name = "RadGroupBox16"
        Me.RadGroupBox16.Size = New System.Drawing.Size(286, 151)
        Me.RadGroupBox16.TabIndex = 10
        Me.RadGroupBox16.Text = "Yearly DCS Summary"
        '
        'RadGroupBox17
        '
        Me.RadGroupBox17.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox17.Controls.Add(Me.rbtnPRBoth)
        Me.RadGroupBox17.Controls.Add(Me.rbtnRegistered)
        Me.RadGroupBox17.Controls.Add(Me.rbtnPDCS)
        Me.RadGroupBox17.HeaderText = ""
        Me.RadGroupBox17.Location = New System.Drawing.Point(72, 100)
        Me.RadGroupBox17.Name = "RadGroupBox17"
        Me.RadGroupBox17.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox17.Size = New System.Drawing.Size(189, 25)
        Me.RadGroupBox17.TabIndex = 1524
        '
        'rbtnPRBoth
        '
        Me.rbtnPRBoth.AutoSize = True
        Me.rbtnPRBoth.Checked = True
        Me.rbtnPRBoth.Location = New System.Drawing.Point(137, 4)
        Me.rbtnPRBoth.Name = "rbtnPRBoth"
        Me.rbtnPRBoth.Size = New System.Drawing.Size(38, 17)
        Me.rbtnPRBoth.TabIndex = 2
        Me.rbtnPRBoth.TabStop = True
        Me.rbtnPRBoth.Text = "All"
        Me.rbtnPRBoth.UseVisualStyleBackColor = True
        '
        'rbtnRegistered
        '
        Me.rbtnRegistered.AutoSize = True
        Me.rbtnRegistered.Location = New System.Drawing.Point(57, 4)
        Me.rbtnRegistered.Name = "rbtnRegistered"
        Me.rbtnRegistered.Size = New System.Drawing.Size(80, 17)
        Me.rbtnRegistered.TabIndex = 1
        Me.rbtnRegistered.Text = "Registered"
        Me.rbtnRegistered.UseVisualStyleBackColor = True
        '
        'rbtnPDCS
        '
        Me.rbtnPDCS.AutoSize = True
        Me.rbtnPDCS.Location = New System.Drawing.Point(3, 4)
        Me.rbtnPDCS.Name = "rbtnPDCS"
        Me.rbtnPDCS.Size = New System.Drawing.Size(52, 17)
        Me.rbtnPDCS.TabIndex = 0
        Me.rbtnPDCS.Text = "PDCS"
        Me.rbtnPDCS.UseVisualStyleBackColor = True
        '
        'fndMultDCS
        '
        Me.fndMultDCS.arrDispalyMember = Nothing
        Me.fndMultDCS.arrValueMember = Nothing
        Me.fndMultDCS.Location = New System.Drawing.Point(72, 80)
        Me.fndMultDCS.Margin = New System.Windows.Forms.Padding(4)
        Me.fndMultDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMultDCS.MyLinkLable1 = Me.MyLabel13
        Me.fndMultDCS.MyLinkLable2 = Nothing
        Me.fndMultDCS.MyNullText = "All"
        Me.fndMultDCS.Name = "fndMultDCS"
        Me.fndMultDCS.Size = New System.Drawing.Size(189, 19)
        Me.fndMultDCS.TabIndex = 1521
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel13.TabIndex = 2
        Me.MyLabel13.Text = "From"
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(5, 80)
        Me.MyLabel24.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel24.TabIndex = 1520
        Me.MyLabel24.Text = "DCS"
        '
        'fndMultMCC
        '
        Me.fndMultMCC.arrDispalyMember = Nothing
        Me.fndMultMCC.arrValueMember = Nothing
        Me.fndMultMCC.Location = New System.Drawing.Point(72, 60)
        Me.fndMultMCC.Margin = New System.Windows.Forms.Padding(4)
        Me.fndMultMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMultMCC.MyLinkLable1 = Me.MyLabel13
        Me.fndMultMCC.MyLinkLable2 = Nothing
        Me.fndMultMCC.MyNullText = "All"
        Me.fndMultMCC.Name = "fndMultMCC"
        Me.fndMultMCC.Size = New System.Drawing.Size(189, 19)
        Me.fndMultMCC.TabIndex = 1519
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(5, 60)
        Me.MyLabel23.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel23.TabIndex = 1518
        Me.MyLabel23.Text = "MCC"
        '
        'fndFinacialYear
        '
        Me.fndFinacialYear.CalculationExpression = Nothing
        Me.fndFinacialYear.FieldCode = Nothing
        Me.fndFinacialYear.FieldDesc = Nothing
        Me.fndFinacialYear.FieldMaxLength = 0
        Me.fndFinacialYear.FieldName = Nothing
        Me.fndFinacialYear.isCalculatedField = False
        Me.fndFinacialYear.IsSourceFromTable = False
        Me.fndFinacialYear.IsSourceFromValueList = False
        Me.fndFinacialYear.IsUnique = False
        Me.fndFinacialYear.Location = New System.Drawing.Point(72, 18)
        Me.fndFinacialYear.MendatroryField = True
        Me.fndFinacialYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFinacialYear.MyLinkLable1 = Me.lblMCC
        Me.fndFinacialYear.MyLinkLable2 = Nothing
        Me.fndFinacialYear.MyReadOnly = False
        Me.fndFinacialYear.MyShowMasterFormButton = False
        Me.fndFinacialYear.Name = "fndFinacialYear"
        Me.fndFinacialYear.ReferenceFieldDesc = Nothing
        Me.fndFinacialYear.ReferenceFieldName = Nothing
        Me.fndFinacialYear.ReferenceTableName = Nothing
        Me.fndFinacialYear.Size = New System.Drawing.Size(189, 19)
        Me.fndFinacialYear.TabIndex = 159
        Me.fndFinacialYear.Value = ""
        '
        'lblMCC
        '
        Me.lblMCC.AutoSize = False
        Me.lblMCC.BorderVisible = True
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC.Location = New System.Drawing.Point(293, 14)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(237, 18)
        Me.lblMCC.TabIndex = 403
        Me.lblMCC.TextWrap = False
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Location = New System.Drawing.Point(158, 41)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel21.TabIndex = 158
        Me.MyLabel21.Text = "To"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Location = New System.Drawing.Point(5, 40)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel22.TabIndex = 157
        Me.MyLabel22.Text = "From"
        '
        'txtDateTo
        '
        Me.txtDateTo.CustomFormat = "dd/MM/yyyy"
        Me.txtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDateTo.Location = New System.Drawing.Point(178, 39)
        Me.txtDateTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateTo.Size = New System.Drawing.Size(83, 20)
        Me.txtDateTo.TabIndex = 156
        Me.txtDateTo.TabStop = False
        Me.txtDateTo.Text = "24/10/2011"
        Me.txtDateTo.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtDateFrom
        '
        Me.txtDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.txtDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDateFrom.Location = New System.Drawing.Point(72, 39)
        Me.txtDateFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDateFrom.Size = New System.Drawing.Size(85, 20)
        Me.txtDateFrom.TabIndex = 155
        Me.txtDateFrom.TabStop = False
        Me.txtDateFrom.Text = "24/10/2011"
        Me.txtDateFrom.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Location = New System.Drawing.Point(5, 18)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel20.TabIndex = 154
        Me.MyLabel20.Text = "Finacial Year"
        '
        'btnYearlySummary
        '
        Me.btnYearlySummary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnYearlySummary.Location = New System.Drawing.Point(5, 127)
        Me.btnYearlySummary.Name = "btnYearlySummary"
        Me.btnYearlySummary.Size = New System.Drawing.Size(71, 22)
        Me.btnYearlySummary.TabIndex = 153
        Me.btnYearlySummary.Text = "Print"
        '
        'RadGroupBox14
        '
        Me.RadGroupBox14.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox14.Controls.Add(Me.btnDCSWiseAvgFatSnfPrint)
        Me.RadGroupBox14.Controls.Add(Me.RadGroupBox15)
        Me.RadGroupBox14.HeaderText = "DCS Wise Avg FAT SNF"
        Me.RadGroupBox14.Location = New System.Drawing.Point(578, 137)
        Me.RadGroupBox14.Name = "RadGroupBox14"
        Me.RadGroupBox14.Size = New System.Drawing.Size(269, 104)
        Me.RadGroupBox14.TabIndex = 9
        Me.RadGroupBox14.Text = "DCS Wise Avg FAT SNF"
        '
        'btnDCSWiseAvgFatSnfPrint
        '
        Me.btnDCSWiseAvgFatSnfPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDCSWiseAvgFatSnfPrint.Location = New System.Drawing.Point(5, 67)
        Me.btnDCSWiseAvgFatSnfPrint.Name = "btnDCSWiseAvgFatSnfPrint"
        Me.btnDCSWiseAvgFatSnfPrint.Size = New System.Drawing.Size(71, 22)
        Me.btnDCSWiseAvgFatSnfPrint.TabIndex = 153
        Me.btnDCSWiseAvgFatSnfPrint.Text = "Print"
        '
        'RadGroupBox15
        '
        Me.RadGroupBox15.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox15.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox15.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox15.Controls.Add(Me.dtpDCSWiseAvgFatSnfToDate)
        Me.RadGroupBox15.Controls.Add(Me.dtpDCSWiseAvgFatSnfFromDate)
        Me.RadGroupBox15.HeaderText = "Date Range"
        Me.RadGroupBox15.Location = New System.Drawing.Point(5, 19)
        Me.RadGroupBox15.Name = "RadGroupBox15"
        Me.RadGroupBox15.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox15.Size = New System.Drawing.Size(255, 42)
        Me.RadGroupBox15.TabIndex = 54
        Me.RadGroupBox15.Text = "Date Range"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Location = New System.Drawing.Point(135, 16)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel16.TabIndex = 3
        Me.MyLabel16.Text = "To"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel17.TabIndex = 2
        Me.MyLabel17.Text = "From"
        '
        'dtpDCSWiseAvgFatSnfToDate
        '
        Me.dtpDCSWiseAvgFatSnfToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDCSWiseAvgFatSnfToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDCSWiseAvgFatSnfToDate.Location = New System.Drawing.Point(162, 15)
        Me.dtpDCSWiseAvgFatSnfToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDCSWiseAvgFatSnfToDate.Name = "dtpDCSWiseAvgFatSnfToDate"
        Me.dtpDCSWiseAvgFatSnfToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDCSWiseAvgFatSnfToDate.Size = New System.Drawing.Size(83, 20)
        Me.dtpDCSWiseAvgFatSnfToDate.TabIndex = 1
        Me.dtpDCSWiseAvgFatSnfToDate.TabStop = False
        Me.dtpDCSWiseAvgFatSnfToDate.Text = "24/10/2011"
        Me.dtpDCSWiseAvgFatSnfToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'dtpDCSWiseAvgFatSnfFromDate
        '
        Me.dtpDCSWiseAvgFatSnfFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDCSWiseAvgFatSnfFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDCSWiseAvgFatSnfFromDate.Location = New System.Drawing.Point(44, 15)
        Me.dtpDCSWiseAvgFatSnfFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDCSWiseAvgFatSnfFromDate.Name = "dtpDCSWiseAvgFatSnfFromDate"
        Me.dtpDCSWiseAvgFatSnfFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDCSWiseAvgFatSnfFromDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpDCSWiseAvgFatSnfFromDate.TabIndex = 0
        Me.dtpDCSWiseAvgFatSnfFromDate.TabStop = False
        Me.dtpDCSWiseAvgFatSnfFromDate.Text = "24/10/2011"
        Me.dtpDCSWiseAvgFatSnfFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox12
        '
        Me.RadGroupBox12.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox12.Controls.Add(Me.RadSplitButton1)
        Me.RadGroupBox12.Controls.Add(Me.RadGroupBox13)
        Me.RadGroupBox12.HeaderText = "Daily Gain Loss Report"
        Me.RadGroupBox12.Location = New System.Drawing.Point(8, 293)
        Me.RadGroupBox12.Name = "RadGroupBox12"
        Me.RadGroupBox12.Size = New System.Drawing.Size(269, 104)
        Me.RadGroupBox12.TabIndex = 8
        Me.RadGroupBox12.Text = "Daily Gain Loss Report"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.rmiPDFGrid})
        Me.RadSplitButton1.Location = New System.Drawing.Point(5, 67)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(71, 22)
        Me.RadSplitButton1.TabIndex = 154
        Me.RadSplitButton1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Excel"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "PDF"
        Me.RadMenuItem3.UseCompatibleTextRendering = False
        '
        'rmiPDFGrid
        '
        Me.rmiPDFGrid.Name = "rmiPDFGrid"
        Me.rmiPDFGrid.Text = "PDF Grid"
        Me.rmiPDFGrid.UseCompatibleTextRendering = False
        '
        'RadGroupBox13
        '
        Me.RadGroupBox13.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox13.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox13.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox13.Controls.Add(Me.dtpGainLossToDate)
        Me.RadGroupBox13.Controls.Add(Me.dtpGainLossFromDate)
        Me.RadGroupBox13.HeaderText = "Date Range"
        Me.RadGroupBox13.Location = New System.Drawing.Point(5, 19)
        Me.RadGroupBox13.Name = "RadGroupBox13"
        Me.RadGroupBox13.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox13.Size = New System.Drawing.Size(255, 42)
        Me.RadGroupBox13.TabIndex = 54
        Me.RadGroupBox13.Text = "Date Range"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(135, 16)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel14.TabIndex = 3
        Me.MyLabel14.Text = "To"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel15.TabIndex = 2
        Me.MyLabel15.Text = "From"
        '
        'dtpGainLossToDate
        '
        Me.dtpGainLossToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpGainLossToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGainLossToDate.Location = New System.Drawing.Point(162, 15)
        Me.dtpGainLossToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGainLossToDate.Name = "dtpGainLossToDate"
        Me.dtpGainLossToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGainLossToDate.Size = New System.Drawing.Size(83, 20)
        Me.dtpGainLossToDate.TabIndex = 1
        Me.dtpGainLossToDate.TabStop = False
        Me.dtpGainLossToDate.Text = "24/10/2011"
        Me.dtpGainLossToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'dtpGainLossFromDate
        '
        Me.dtpGainLossFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpGainLossFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGainLossFromDate.Location = New System.Drawing.Point(44, 15)
        Me.dtpGainLossFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGainLossFromDate.Name = "dtpGainLossFromDate"
        Me.dtpGainLossFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGainLossFromDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpGainLossFromDate.TabIndex = 0
        Me.dtpGainLossFromDate.TabStop = False
        Me.dtpGainLossFromDate.Text = "24/10/2011"
        Me.dtpGainLossFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox10
        '
        Me.RadGroupBox10.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox10.Controls.Add(Me.txtRouteName)
        Me.RadGroupBox10.Controls.Add(Me.txtMultiMCC)
        Me.RadGroupBox10.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox10.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox10.Controls.Add(Me.btn_DCS_Ledger_Report)
        Me.RadGroupBox10.Controls.Add(Me.RadGroupBox11)
        Me.RadGroupBox10.HeaderText = "DCS Ledger Report"
        Me.RadGroupBox10.Location = New System.Drawing.Point(8, 137)
        Me.RadGroupBox10.Name = "RadGroupBox10"
        Me.RadGroupBox10.Size = New System.Drawing.Size(269, 150)
        Me.RadGroupBox10.TabIndex = 7
        Me.RadGroupBox10.Text = "DCS Ledger Report"
        '
        'txtRouteName
        '
        Me.txtRouteName.arrDispalyMember = Nothing
        Me.txtRouteName.arrValueMember = Nothing
        Me.txtRouteName.Location = New System.Drawing.Point(74, 96)
        Me.txtRouteName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRouteName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteName.MyLinkLable1 = Me.MyLabel13
        Me.txtRouteName.MyLinkLable2 = Nothing
        Me.txtRouteName.MyNullText = "All"
        Me.txtRouteName.Name = "txtRouteName"
        Me.txtRouteName.Size = New System.Drawing.Size(186, 19)
        Me.txtRouteName.TabIndex = 1518
        '
        'txtMultiMCC
        '
        Me.txtMultiMCC.arrDispalyMember = Nothing
        Me.txtMultiMCC.arrValueMember = Nothing
        Me.txtMultiMCC.Location = New System.Drawing.Point(74, 71)
        Me.txtMultiMCC.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMultiMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiMCC.MyLinkLable1 = Me.MyLabel13
        Me.txtMultiMCC.MyLinkLable2 = Nothing
        Me.txtMultiMCC.MyNullText = "All"
        Me.txtMultiMCC.Name = "txtMultiMCC"
        Me.txtMultiMCC.Size = New System.Drawing.Size(186, 19)
        Me.txtMultiMCC.TabIndex = 1517
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(6, 97)
        Me.MyLabel19.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel19.TabIndex = 1516
        Me.MyLabel19.Text = "Route Name"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(6, 71)
        Me.MyLabel18.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel18.TabIndex = 1515
        Me.MyLabel18.Text = "MCC Code"
        '
        'btn_DCS_Ledger_Report
        '
        Me.btn_DCS_Ledger_Report.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_DCS_Ledger_Report.Location = New System.Drawing.Point(5, 122)
        Me.btn_DCS_Ledger_Report.Name = "btn_DCS_Ledger_Report"
        Me.btn_DCS_Ledger_Report.Size = New System.Drawing.Size(71, 22)
        Me.btn_DCS_Ledger_Report.TabIndex = 153
        Me.btn_DCS_Ledger_Report.Text = "Print"
        '
        'RadGroupBox11
        '
        Me.RadGroupBox11.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox11.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox11.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox11.Controls.Add(Me.dtpToDCS_Ledger)
        Me.RadGroupBox11.Controls.Add(Me.dtpFromDCS_Ledger)
        Me.RadGroupBox11.HeaderText = "Date Range"
        Me.RadGroupBox11.Location = New System.Drawing.Point(5, 19)
        Me.RadGroupBox11.Name = "RadGroupBox11"
        Me.RadGroupBox11.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox11.Size = New System.Drawing.Size(255, 42)
        Me.RadGroupBox11.TabIndex = 54
        Me.RadGroupBox11.Text = "Date Range"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(135, 16)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel12.TabIndex = 3
        Me.MyLabel12.Text = "To"
        '
        'dtpToDCS_Ledger
        '
        Me.dtpToDCS_Ledger.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDCS_Ledger.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDCS_Ledger.Location = New System.Drawing.Point(162, 15)
        Me.dtpToDCS_Ledger.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDCS_Ledger.Name = "dtpToDCS_Ledger"
        Me.dtpToDCS_Ledger.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDCS_Ledger.Size = New System.Drawing.Size(83, 20)
        Me.dtpToDCS_Ledger.TabIndex = 1
        Me.dtpToDCS_Ledger.TabStop = False
        Me.dtpToDCS_Ledger.Text = "24/10/2011"
        Me.dtpToDCS_Ledger.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'dtpFromDCS_Ledger
        '
        Me.dtpFromDCS_Ledger.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDCS_Ledger.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDCS_Ledger.Location = New System.Drawing.Point(44, 15)
        Me.dtpFromDCS_Ledger.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDCS_Ledger.Name = "dtpFromDCS_Ledger"
        Me.dtpFromDCS_Ledger.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDCS_Ledger.Size = New System.Drawing.Size(85, 20)
        Me.dtpFromDCS_Ledger.TabIndex = 0
        Me.dtpFromDCS_Ledger.TabStop = False
        Me.dtpFromDCS_Ledger.Text = "24/10/2011"
        Me.dtpFromDCS_Ledger.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.btnPrintDailySummary)
        Me.RadGroupBox8.Controls.Add(Me.RadGroupBox9)
        Me.RadGroupBox8.HeaderText = "Daily Summary"
        Me.RadGroupBox8.Location = New System.Drawing.Point(578, 9)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Size = New System.Drawing.Size(269, 104)
        Me.RadGroupBox8.TabIndex = 6
        Me.RadGroupBox8.Text = "Daily Summary"
        '
        'btnPrintDailySummary
        '
        Me.btnPrintDailySummary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintDailySummary.Location = New System.Drawing.Point(5, 67)
        Me.btnPrintDailySummary.Name = "btnPrintDailySummary"
        Me.btnPrintDailySummary.Size = New System.Drawing.Size(71, 22)
        Me.btnPrintDailySummary.TabIndex = 153
        Me.btnPrintDailySummary.Text = "Print"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox9.Controls.Add(Me.dtpDailySummaryToDate)
        Me.RadGroupBox9.Controls.Add(Me.dtpDailySummaryFromDate)
        Me.RadGroupBox9.HeaderText = "Date Range"
        Me.RadGroupBox9.Location = New System.Drawing.Point(5, 19)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox9.Size = New System.Drawing.Size(255, 42)
        Me.RadGroupBox9.TabIndex = 54
        Me.RadGroupBox9.Text = "Date Range"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(135, 16)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel10.TabIndex = 3
        Me.MyLabel10.Text = "To"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel11.TabIndex = 2
        Me.MyLabel11.Text = "From"
        '
        'dtpDailySummaryToDate
        '
        Me.dtpDailySummaryToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDailySummaryToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDailySummaryToDate.Location = New System.Drawing.Point(162, 15)
        Me.dtpDailySummaryToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDailySummaryToDate.Name = "dtpDailySummaryToDate"
        Me.dtpDailySummaryToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDailySummaryToDate.Size = New System.Drawing.Size(83, 20)
        Me.dtpDailySummaryToDate.TabIndex = 1
        Me.dtpDailySummaryToDate.TabStop = False
        Me.dtpDailySummaryToDate.Text = "24/10/2011"
        Me.dtpDailySummaryToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'dtpDailySummaryFromDate
        '
        Me.dtpDailySummaryFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDailySummaryFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDailySummaryFromDate.Location = New System.Drawing.Point(44, 15)
        Me.dtpDailySummaryFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDailySummaryFromDate.Name = "dtpDailySummaryFromDate"
        Me.dtpDailySummaryFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDailySummaryFromDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpDailySummaryFromDate.TabIndex = 0
        Me.dtpDailySummaryFromDate.TabStop = False
        Me.dtpDailySummaryFromDate.Text = "24/10/2011"
        Me.dtpDailySummaryFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.lblMonth)
        Me.RadGroupBox6.Controls.Add(Me.txtMonth)
        Me.RadGroupBox6.Controls.Add(Me.RadGroupBox7)
        Me.RadGroupBox6.Controls.Add(Me.btnDCSSummaryPrint)
        Me.RadGroupBox6.HeaderText = "DCS Summary"
        Me.RadGroupBox6.Location = New System.Drawing.Point(283, 9)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(286, 104)
        Me.RadGroupBox6.TabIndex = 5
        Me.RadGroupBox6.Text = "DCS Summary"
        '
        'lblMonth
        '
        Me.lblMonth.FieldName = Nothing
        Me.lblMonth.Location = New System.Drawing.Point(5, 28)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(40, 18)
        Me.lblMonth.TabIndex = 154
        Me.lblMonth.Text = "Month"
        '
        'txtMonth
        '
        Me.txtMonth.CalculationExpression = Nothing
        Me.txtMonth.CustomFormat = "MMM/yyyy"
        Me.txtMonth.FieldCode = Nothing
        Me.txtMonth.FieldDesc = Nothing
        Me.txtMonth.FieldMaxLength = 0
        Me.txtMonth.FieldName = Nothing
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMonth.isCalculatedField = False
        Me.txtMonth.IsSourceFromTable = False
        Me.txtMonth.IsSourceFromValueList = False
        Me.txtMonth.IsUnique = False
        Me.txtMonth.Location = New System.Drawing.Point(61, 28)
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Me.MyLabel1
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.ShowUpDown = True
        Me.txtMonth.Size = New System.Drawing.Size(141, 19)
        Me.txtMonth.TabIndex = 6
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Sep/2014"
        Me.txtMonth.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 99)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(26, 18)
        Me.MyLabel1.TabIndex = 385
        Me.MyLabel1.Text = "VSP"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox7.Controls.Add(Me.txtToDate)
        Me.RadGroupBox7.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox7.HeaderText = "Date Range"
        Me.RadGroupBox7.Location = New System.Drawing.Point(200, 60)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(69, 42)
        Me.RadGroupBox7.TabIndex = 54
        Me.RadGroupBox7.Text = "Date Range"
        Me.RadGroupBox7.Visible = False
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(135, 16)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel8.TabIndex = 3
        Me.MyLabel8.Text = "To"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(6, 16)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel9.TabIndex = 2
        Me.MyLabel9.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(162, 15)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(83, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(44, 15)
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(85, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "24/10/2011"
        Me.txtFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'btnDCSSummaryPrint
        '
        Me.btnDCSSummaryPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDCSSummaryPrint.Location = New System.Drawing.Point(5, 67)
        Me.btnDCSSummaryPrint.Name = "btnDCSSummaryPrint"
        Me.btnDCSSummaryPrint.Size = New System.Drawing.Size(71, 22)
        Me.btnDCSSummaryPrint.TabIndex = 153
        Me.btnDCSSummaryPrint.Text = "Print"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.btnLedgerPrint)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox5.HeaderText = "Ledger Print"
        Me.RadGroupBox5.Location = New System.Drawing.Point(8, 7)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(269, 104)
        Me.RadGroupBox5.TabIndex = 4
        Me.RadGroupBox5.Text = "Ledger Print"
        '
        'btnLedgerPrint
        '
        Me.btnLedgerPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLedgerPrint.Location = New System.Drawing.Point(5, 67)
        Me.btnLedgerPrint.Name = "btnLedgerPrint"
        Me.btnLedgerPrint.Size = New System.Drawing.Size(71, 22)
        Me.btnLedgerPrint.TabIndex = 153
        Me.btnLedgerPrint.Text = "Print"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox4.Controls.Add(Me.dtpLedgerToDate)
        Me.RadGroupBox4.Controls.Add(Me.dtpLedgerFromDate)
        Me.RadGroupBox4.HeaderText = "Date Range"
        Me.RadGroupBox4.Location = New System.Drawing.Point(5, 19)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(255, 42)
        Me.RadGroupBox4.TabIndex = 54
        Me.RadGroupBox4.Text = "Date Range"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(135, 16)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel5.TabIndex = 3
        Me.MyLabel5.Text = "To"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel7.TabIndex = 2
        Me.MyLabel7.Text = "From"
        '
        'dtpLedgerToDate
        '
        Me.dtpLedgerToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpLedgerToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLedgerToDate.Location = New System.Drawing.Point(162, 15)
        Me.dtpLedgerToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLedgerToDate.Name = "dtpLedgerToDate"
        Me.dtpLedgerToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLedgerToDate.Size = New System.Drawing.Size(83, 20)
        Me.dtpLedgerToDate.TabIndex = 1
        Me.dtpLedgerToDate.TabStop = False
        Me.dtpLedgerToDate.Text = "24/10/2011"
        Me.dtpLedgerToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'dtpLedgerFromDate
        '
        Me.dtpLedgerFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpLedgerFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLedgerFromDate.Location = New System.Drawing.Point(44, 15)
        Me.dtpLedgerFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLedgerFromDate.Name = "dtpLedgerFromDate"
        Me.dtpLedgerFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLedgerFromDate.Size = New System.Drawing.Size(85, 20)
        Me.dtpLedgerFromDate.TabIndex = 0
        Me.dtpLedgerFromDate.TabStop = False
        Me.dtpLedgerFromDate.Text = "24/10/2011"
        Me.dtpLedgerFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'gbSummaryReportType
        '
        Me.gbSummaryReportType.Controls.Add(Me.chkOther)
        Me.gbSummaryReportType.Controls.Add(Me.chkAddition)
        Me.gbSummaryReportType.Location = New System.Drawing.Point(5, 107)
        Me.gbSummaryReportType.Name = "gbSummaryReportType"
        Me.gbSummaryReportType.Size = New System.Drawing.Size(175, 44)
        Me.gbSummaryReportType.TabIndex = 405
        Me.gbSummaryReportType.TabStop = False
        Me.gbSummaryReportType.Text = "Summary Report Type"
        '
        'chkOther
        '
        Me.chkOther.AutoSize = True
        Me.chkOther.Location = New System.Drawing.Point(110, 20)
        Me.chkOther.Name = "chkOther"
        Me.chkOther.Size = New System.Drawing.Size(55, 17)
        Me.chkOther.TabIndex = 1
        Me.chkOther.Text = "Other"
        Me.chkOther.UseVisualStyleBackColor = True
        '
        'chkAddition
        '
        Me.chkAddition.AutoSize = True
        Me.chkAddition.Checked = True
        Me.chkAddition.Location = New System.Drawing.Point(8, 21)
        Me.chkAddition.Name = "chkAddition"
        Me.chkAddition.Size = New System.Drawing.Size(70, 17)
        Me.chkAddition.TabIndex = 0
        Me.chkAddition.TabStop = True
        Me.chkAddition.Text = "Addition"
        Me.chkAddition.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkDetailReportType)
        Me.GroupBox1.Controls.Add(Me.chkSummaryReportType)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 60)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(175, 44)
        Me.GroupBox1.TabIndex = 404
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report Type"
        '
        'chkDetailReportType
        '
        Me.chkDetailReportType.AutoSize = True
        Me.chkDetailReportType.Checked = True
        Me.chkDetailReportType.Location = New System.Drawing.Point(110, 20)
        Me.chkDetailReportType.Name = "chkDetailReportType"
        Me.chkDetailReportType.Size = New System.Drawing.Size(60, 17)
        Me.chkDetailReportType.TabIndex = 1
        Me.chkDetailReportType.TabStop = True
        Me.chkDetailReportType.Text = "Details"
        Me.chkDetailReportType.UseVisualStyleBackColor = True
        '
        'chkSummaryReportType
        '
        Me.chkSummaryReportType.AutoSize = True
        Me.chkSummaryReportType.Location = New System.Drawing.Point(8, 21)
        Me.chkSummaryReportType.Name = "chkSummaryReportType"
        Me.chkSummaryReportType.Size = New System.Drawing.Size(71, 17)
        Me.chkSummaryReportType.TabIndex = 0
        Me.chkSummaryReportType.Text = "Summary"
        Me.chkSummaryReportType.UseVisualStyleBackColor = True
        '
        'txtMcc
        '
        Me.txtMcc.CalculationExpression = Nothing
        Me.txtMcc.FieldCode = Nothing
        Me.txtMcc.FieldDesc = Nothing
        Me.txtMcc.FieldMaxLength = 0
        Me.txtMcc.FieldName = Nothing
        Me.txtMcc.isCalculatedField = False
        Me.txtMcc.IsSourceFromTable = False
        Me.txtMcc.IsSourceFromValueList = False
        Me.txtMcc.IsUnique = False
        Me.txtMcc.Location = New System.Drawing.Point(120, 13)
        Me.txtMcc.MendatroryField = True
        Me.txtMcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMcc.MyLinkLable1 = Nothing
        Me.txtMcc.MyLinkLable2 = Nothing
        Me.txtMcc.MyReadOnly = False
        Me.txtMcc.MyShowMasterFormButton = False
        Me.txtMcc.Name = "txtMcc"
        Me.txtMcc.ReferenceFieldDesc = Nothing
        Me.txtMcc.ReferenceFieldName = Nothing
        Me.txtMcc.ReferenceTableName = Nothing
        Me.txtMcc.Size = New System.Drawing.Size(167, 19)
        Me.txtMcc.TabIndex = 401
        Me.txtMcc.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(5, 14)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel4.TabIndex = 402
        Me.MyLabel4.Text = "MCC"
        '
        'txtPaymentCycleCode
        '
        Me.txtPaymentCycleCode.CalculationExpression = Nothing
        Me.txtPaymentCycleCode.FieldCode = Nothing
        Me.txtPaymentCycleCode.FieldDesc = Nothing
        Me.txtPaymentCycleCode.FieldMaxLength = 0
        Me.txtPaymentCycleCode.FieldName = Nothing
        Me.txtPaymentCycleCode.isCalculatedField = False
        Me.txtPaymentCycleCode.IsSourceFromTable = False
        Me.txtPaymentCycleCode.IsSourceFromValueList = False
        Me.txtPaymentCycleCode.IsUnique = False
        Me.txtPaymentCycleCode.Location = New System.Drawing.Point(120, 35)
        Me.txtPaymentCycleCode.MendatroryField = True
        Me.txtPaymentCycleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentCycleCode.MyLinkLable1 = Nothing
        Me.txtPaymentCycleCode.MyLinkLable2 = Nothing
        Me.txtPaymentCycleCode.MyReadOnly = False
        Me.txtPaymentCycleCode.MyShowMasterFormButton = False
        Me.txtPaymentCycleCode.Name = "txtPaymentCycleCode"
        Me.txtPaymentCycleCode.ReferenceFieldDesc = Nothing
        Me.txtPaymentCycleCode.ReferenceFieldName = Nothing
        Me.txtPaymentCycleCode.ReferenceTableName = Nothing
        Me.txtPaymentCycleCode.Size = New System.Drawing.Size(167, 19)
        Me.txtPaymentCycleCode.TabIndex = 399
        Me.txtPaymentCycleCode.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(5, 36)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel6.TabIndex = 400
        Me.MyLabel6.Text = "Pyment Cycle Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.gbDateRangeApply)
        Me.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.Panel1.Controls.Add(Me.txtVSP)
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.txtLocation)
        Me.Panel1.Controls.Add(Me.txtDocumentNo)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Location = New System.Drawing.Point(614, 303)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(111, 72)
        Me.Panel1.TabIndex = 398
        Me.Panel1.Visible = False
        '
        'gbDateRangeApply
        '
        Me.gbDateRangeApply.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbDateRangeApply.Controls.Add(Me.rbNone)
        Me.gbDateRangeApply.Controls.Add(Me.rbBothFromToDate)
        Me.gbDateRangeApply.Controls.Add(Me.rbToDate)
        Me.gbDateRangeApply.Controls.Add(Me.rbFromDate)
        Me.gbDateRangeApply.HeaderText = "Payment Cycle Date Range Apply"
        Me.gbDateRangeApply.Location = New System.Drawing.Point(12, 28)
        Me.gbDateRangeApply.Name = "gbDateRangeApply"
        Me.gbDateRangeApply.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbDateRangeApply.Size = New System.Drawing.Size(259, 42)
        Me.gbDateRangeApply.TabIndex = 396
        Me.gbDateRangeApply.Text = "Payment Cycle Date Range Apply"
        '
        'rbNone
        '
        Me.rbNone.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbNone.Location = New System.Drawing.Point(200, 17)
        Me.rbNone.Name = "rbNone"
        Me.rbNone.Size = New System.Drawing.Size(48, 18)
        Me.rbNone.TabIndex = 397
        Me.rbNone.Text = "None"
        Me.rbNone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbBothFromToDate
        '
        Me.rbBothFromToDate.Location = New System.Drawing.Point(150, 17)
        Me.rbBothFromToDate.Name = "rbBothFromToDate"
        Me.rbBothFromToDate.Size = New System.Drawing.Size(44, 18)
        Me.rbBothFromToDate.TabIndex = 397
        Me.rbBothFromToDate.TabStop = False
        Me.rbBothFromToDate.Text = "Both"
        '
        'rbToDate
        '
        Me.rbToDate.Location = New System.Drawing.Point(85, 17)
        Me.rbToDate.Name = "rbToDate"
        Me.rbToDate.Size = New System.Drawing.Size(59, 18)
        Me.rbToDate.TabIndex = 397
        Me.rbToDate.TabStop = False
        Me.rbToDate.Text = "To Date"
        '
        'rbFromDate
        '
        Me.rbFromDate.Location = New System.Drawing.Point(6, 17)
        Me.rbFromDate.Name = "rbFromDate"
        Me.rbFromDate.Size = New System.Drawing.Size(73, 18)
        Me.rbFromDate.TabIndex = 397
        Me.rbFromDate.TabStop = False
        Me.rbFromDate.Text = "From Date"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox2.Controls.Add(Me.rdbDetails)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(528, 35)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(149, 35)
        Me.RadGroupBox2.TabIndex = 397
        '
        'rdbSummary
        '
        Me.rdbSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbSummary.Location = New System.Drawing.Point(8, 7)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 310
        Me.rdbSummary.Text = "Summary"
        Me.rdbSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbDetails
        '
        Me.rdbDetails.Location = New System.Drawing.Point(81, 7)
        Me.rdbDetails.Name = "rdbDetails"
        Me.rdbDetails.Size = New System.Drawing.Size(54, 18)
        Me.rdbDetails.TabIndex = 308
        Me.rdbDetails.TabStop = False
        Me.rdbDetails.Text = "Details"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Payment Cycle Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(277, 28)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(251, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Payment Cycle Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(135, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(162, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(83, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(85, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtVSP
        '
        Me.txtVSP.arrDispalyMember = Nothing
        Me.txtVSP.arrValueMember = Nothing
        Me.txtVSP.Location = New System.Drawing.Point(103, 99)
        Me.txtVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSP.MyLinkLable1 = Me.MyLabel1
        Me.txtVSP.MyLinkLable2 = Nothing
        Me.txtVSP.MyNullText = "All"
        Me.txtVSP.Name = "txtVSP"
        Me.txtVSP.Size = New System.Drawing.Size(425, 19)
        Me.txtVSP.TabIndex = 384
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkBoth)
        Me.RadGroupBox1.Controls.Add(Me.ChkPosted)
        Me.RadGroupBox1.Controls.Add(Me.ChkUnPosted)
        Me.RadGroupBox1.HeaderText = "Document Status"
        Me.RadGroupBox1.Location = New System.Drawing.Point(536, 67)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(141, 74)
        Me.RadGroupBox1.TabIndex = 393
        Me.RadGroupBox1.Text = "Document Status"
        '
        'chkBoth
        '
        Me.chkBoth.Location = New System.Drawing.Point(8, 19)
        Me.chkBoth.Name = "chkBoth"
        Me.chkBoth.Size = New System.Drawing.Size(44, 18)
        Me.chkBoth.TabIndex = 310
        Me.chkBoth.TabStop = False
        Me.chkBoth.Text = "Both"
        '
        'ChkPosted
        '
        Me.ChkPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPosted.Location = New System.Drawing.Point(8, 35)
        Me.ChkPosted.Name = "ChkPosted"
        Me.ChkPosted.Size = New System.Drawing.Size(54, 18)
        Me.ChkPosted.TabIndex = 308
        Me.ChkPosted.Text = "Posted"
        Me.ChkPosted.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'ChkUnPosted
        '
        Me.ChkUnPosted.Location = New System.Drawing.Point(8, 52)
        Me.ChkUnPosted.Name = "ChkUnPosted"
        Me.ChkUnPosted.Size = New System.Drawing.Size(69, 18)
        Me.ChkUnPosted.TabIndex = 309
        Me.ChkUnPosted.TabStop = False
        Me.ChkUnPosted.Text = "UnPosted"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 122)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel2.TabIndex = 392
        Me.MyLabel2.Text = "Document No"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(103, 77)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel3
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(425, 19)
        Me.txtLocation.TabIndex = 388
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 77)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel3.TabIndex = 389
        Me.MyLabel3.Text = "Location"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.arrDispalyMember = Nothing
        Me.txtDocumentNo.arrValueMember = Nothing
        Me.txtDocumentNo.Location = New System.Drawing.Point(103, 122)
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentNo.MyLinkLable1 = Me.MyLabel2
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyNullText = "All"
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(425, 19)
        Me.txtDocumentNo.TabIndex = 391
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(857, 416)
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
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(857, 416)
        Me.Gv1.TabIndex = 0
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(701, 1)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(84, 22)
        Me.btnBack.TabIndex = 156
        Me.btnBack.Text = "<< Back"
        '
        'RadSplitExp
        '
        Me.RadSplitExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitExp.Location = New System.Drawing.Point(155, 1)
        Me.RadSplitExp.Name = "RadSplitExp"
        Me.RadSplitExp.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitExp.TabIndex = 155
        Me.RadSplitExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(791, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(3, 1)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(79, 1)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'rptPaymentProcessRouteReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(878, 513)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptPaymentProcessRouteReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Route Payment Process Report"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.gbLedger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbLedger.ResumeLayout(False)
        CType(Me.RadGroupBox16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox16.ResumeLayout(False)
        Me.RadGroupBox16.PerformLayout()
        CType(Me.RadGroupBox17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox17.ResumeLayout(False)
        Me.RadGroupBox17.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnYearlySummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox14.ResumeLayout(False)
        CType(Me.btnDCSWiseAvgFatSnfPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox15.ResumeLayout(False)
        Me.RadGroupBox15.PerformLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDCSWiseAvgFatSnfToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDCSWiseAvgFatSnfFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox12.ResumeLayout(False)
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox13.ResumeLayout(False)
        Me.RadGroupBox13.PerformLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGainLossToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGainLossFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox10.ResumeLayout(False)
        Me.RadGroupBox10.PerformLayout()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_DCS_Ledger_Report, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox11.ResumeLayout(False)
        Me.RadGroupBox11.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDCS_Ledger, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDCS_Ledger, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        CType(Me.btnPrintDailySummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.RadGroupBox9.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDailySummaryToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDailySummaryFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDCSSummaryPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.btnLedgerPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLedgerToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLedgerFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSummaryReportType.ResumeLayout(False)
        Me.gbSummaryReportType.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.gbDateRangeApply, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDateRangeApply.ResumeLayout(False)
        Me.gbDateRangeApply.PerformLayout()
        CType(Me.rbNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbBothFromToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkBoth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkUnPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadSplitExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gbDateRangeApply As RadGroupBox
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents chkBoth As RadRadioButton
    Friend WithEvents ChkPosted As RadRadioButton
    Friend WithEvents ChkUnPosted As RadRadioButton
    Friend WithEvents rbNone As RadRadioButton
    Friend WithEvents rbBothFromToDate As RadRadioButton
    Friend WithEvents rbToDate As RadRadioButton
    Friend WithEvents rbFromDate As RadRadioButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rdbSummary As RadRadioButton
    Friend WithEvents rdbDetails As RadRadioButton
    Friend WithEvents btnBack As RadButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtPaymentCycleCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtMcc As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents gbSummaryReportType As GroupBox
    Friend WithEvents chkOther As RadioButton
    Friend WithEvents chkAddition As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkDetailReportType As RadioButton
    Friend WithEvents chkSummaryReportType As RadioButton
    Friend WithEvents gbLedger As RadGroupBox
    Friend WithEvents btnLedgerPrint As RadButton
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpLedgerToDate As RadDateTimePicker
    Friend WithEvents dtpLedgerFromDate As RadDateTimePicker
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents btnDCSSummaryPrint As RadButton
    Friend WithEvents RadGroupBox7 As RadGroupBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtToDate As RadDateTimePicker
    Friend WithEvents txtFromDate As RadDateTimePicker
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents lblMonth As common.Controls.MyLabel
    Friend WithEvents RadGroupBox8 As RadGroupBox
    Friend WithEvents btnPrintDailySummary As RadButton
    Friend WithEvents RadGroupBox9 As RadGroupBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents dtpDailySummaryToDate As RadDateTimePicker
    Friend WithEvents dtpDailySummaryFromDate As RadDateTimePicker
    Friend WithEvents RadGroupBox10 As RadGroupBox
    Friend WithEvents btn_DCS_Ledger_Report As RadButton
    Friend WithEvents RadGroupBox11 As RadGroupBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpToDCS_Ledger As RadDateTimePicker
    Friend WithEvents dtpFromDCS_Ledger As RadDateTimePicker
    Friend WithEvents RadGroupBox12 As RadGroupBox
    Friend WithEvents RadGroupBox13 As RadGroupBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents dtpGainLossToDate As RadDateTimePicker
    Friend WithEvents dtpGainLossFromDate As RadDateTimePicker
    Friend WithEvents RadGroupBox14 As RadGroupBox
    Friend WithEvents btnDCSWiseAvgFatSnfPrint As RadButton
    Friend WithEvents RadGroupBox15 As RadGroupBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents dtpDCSWiseAvgFatSnfToDate As RadDateTimePicker
    Friend WithEvents dtpDCSWiseAvgFatSnfFromDate As RadDateTimePicker
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtRouteName As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMultiMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents rmiPDFGrid As RadMenuItem
    Friend WithEvents RadGroupBox16 As RadGroupBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents btnYearlySummary As RadButton
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents txtDateTo As RadDateTimePicker
    Friend WithEvents txtDateFrom As RadDateTimePicker
    Friend WithEvents fndFinacialYear As common.UserControls.txtFinder
    Friend WithEvents fndMultMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox17 As RadGroupBox
    Friend WithEvents fndMultDCS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents rbtnPDCS As RadioButton
    Friend WithEvents rbtnRegistered As RadioButton
    Friend WithEvents rbtnPRBoth As RadioButton
End Class

