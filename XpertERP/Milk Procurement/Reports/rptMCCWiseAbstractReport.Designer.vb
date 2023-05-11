<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptMCCWiseAbstractReport
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pnlMilkReceipts = New System.Windows.Forms.Panel()
        Me.pnlSingleMCCCode = New System.Windows.Forms.Panel()
        Me.lblSingleMCCName = New common.Controls.MyTextBox()
        Me.fndSingleMCCCode = New common.UserControls.txtFinder()
        Me.rdbOther = New System.Windows.Forms.RadioButton()
        Me.rdbMainPlant = New System.Windows.Forms.RadioButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtReciptMCC = New common.UserControls.txtMultiSelectFinder()
        Me.txtMilkReciptToDate = New common.Controls.MyDateTimePicker()
        Me.txtMilkReceiptFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkRejection = New System.Windows.Forms.CheckBox()
        Me.gbReportType = New System.Windows.Forms.GroupBox()
        Me.rdbYearlyConsolidatedReportofMilkProcurement = New System.Windows.Forms.RadioButton()
        Me.rdbYearlyConsolidatedofMilkPayment = New System.Windows.Forms.RadioButton()
        Me.rdbUnitWiseBillSummary = New System.Windows.Forms.RadioButton()
        Me.rdbTIPsummaryReportMCCandVLCwise = New System.Windows.Forms.RadioButton()
        Me.rdbUnitWiseDeduction = New System.Windows.Forms.RadioButton()
        Me.rdbRouteBillsAbstract = New System.Windows.Forms.RadioButton()
        Me.rdbUnitWiseAnalysis = New System.Windows.Forms.RadioButton()
        Me.rdbUnitWiseTotal = New System.Windows.Forms.RadioButton()
        Me.rdbCheckList = New System.Windows.Forms.RadioButton()
        Me.rdbMilkReceipts = New System.Windows.Forms.RadioButton()
        Me.rdbProcurementAbstract = New System.Windows.Forms.RadioButton()
        Me.rdbDetails = New System.Windows.Forms.RadioButton()
        Me.rdbSummary = New System.Windows.Forms.RadioButton()
        Me.txtLocName = New common.Controls.MyTextBox()
        Me.fndLoc = New common.UserControls.txtFinder()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.txtPlant = New common.UserControls.txtMultiSelectFinder()
        Me.lblPlant = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDotMatrixPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
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
        Me.pnlMilkReceipts.SuspendLayout()
        Me.pnlSingleMCCCode.SuspendLayout()
        CType(Me.lblSingleMCCName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkReciptToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkReceiptFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbReportType.SuspendLayout()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDotMatrixPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDotMatrixPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1029, 516)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1029, 445)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.pnlMilkReceipts)
        Me.RadPageViewPage1.Controls.Add(Me.chkRejection)
        Me.RadPageViewPage1.Controls.Add(Me.gbReportType)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocName)
        Me.RadPageViewPage1.Controls.Add(Me.fndLoc)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblMCCCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.txtPlant)
        Me.RadPageViewPage1.Controls.Add(Me.lblPlant)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1008, 397)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'pnlMilkReceipts
        '
        Me.pnlMilkReceipts.Controls.Add(Me.pnlSingleMCCCode)
        Me.pnlMilkReceipts.Controls.Add(Me.rdbOther)
        Me.pnlMilkReceipts.Controls.Add(Me.rdbMainPlant)
        Me.pnlMilkReceipts.Controls.Add(Me.MyLabel5)
        Me.pnlMilkReceipts.Controls.Add(Me.txtReciptMCC)
        Me.pnlMilkReceipts.Controls.Add(Me.txtMilkReciptToDate)
        Me.pnlMilkReceipts.Controls.Add(Me.txtMilkReceiptFromDate)
        Me.pnlMilkReceipts.Controls.Add(Me.MyLabel3)
        Me.pnlMilkReceipts.Controls.Add(Me.MyLabel4)
        Me.pnlMilkReceipts.Location = New System.Drawing.Point(10, 43)
        Me.pnlMilkReceipts.Name = "pnlMilkReceipts"
        Me.pnlMilkReceipts.Size = New System.Drawing.Size(335, 97)
        Me.pnlMilkReceipts.TabIndex = 405
        '
        'pnlSingleMCCCode
        '
        Me.pnlSingleMCCCode.Controls.Add(Me.lblSingleMCCName)
        Me.pnlSingleMCCCode.Controls.Add(Me.fndSingleMCCCode)
        Me.pnlSingleMCCCode.Location = New System.Drawing.Point(40, 25)
        Me.pnlSingleMCCCode.Name = "pnlSingleMCCCode"
        Me.pnlSingleMCCCode.Size = New System.Drawing.Size(292, 25)
        Me.pnlSingleMCCCode.TabIndex = 406
        '
        'lblSingleMCCName
        '
        Me.lblSingleMCCName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblSingleMCCName.CalculationExpression = Nothing
        Me.lblSingleMCCName.Enabled = False
        Me.lblSingleMCCName.FieldCode = Nothing
        Me.lblSingleMCCName.FieldDesc = Nothing
        Me.lblSingleMCCName.FieldMaxLength = 0
        Me.lblSingleMCCName.FieldName = Nothing
        Me.lblSingleMCCName.isCalculatedField = False
        Me.lblSingleMCCName.IsSourceFromTable = False
        Me.lblSingleMCCName.IsSourceFromValueList = False
        Me.lblSingleMCCName.IsUnique = False
        Me.lblSingleMCCName.Location = New System.Drawing.Point(152, 3)
        Me.lblSingleMCCName.MendatroryField = False
        Me.lblSingleMCCName.MyLinkLable1 = Nothing
        Me.lblSingleMCCName.MyLinkLable2 = Nothing
        Me.lblSingleMCCName.Name = "lblSingleMCCName"
        Me.lblSingleMCCName.ReferenceFieldDesc = Nothing
        Me.lblSingleMCCName.ReferenceFieldName = Nothing
        Me.lblSingleMCCName.ReferenceTableName = Nothing
        Me.lblSingleMCCName.Size = New System.Drawing.Size(137, 20)
        Me.lblSingleMCCName.TabIndex = 407
        '
        'fndSingleMCCCode
        '
        Me.fndSingleMCCCode.CalculationExpression = Nothing
        Me.fndSingleMCCCode.FieldCode = Nothing
        Me.fndSingleMCCCode.FieldDesc = Nothing
        Me.fndSingleMCCCode.FieldMaxLength = 0
        Me.fndSingleMCCCode.FieldName = Nothing
        Me.fndSingleMCCCode.isCalculatedField = False
        Me.fndSingleMCCCode.IsSourceFromTable = False
        Me.fndSingleMCCCode.IsSourceFromValueList = False
        Me.fndSingleMCCCode.IsUnique = False
        Me.fndSingleMCCCode.Location = New System.Drawing.Point(2, 3)
        Me.fndSingleMCCCode.MendatroryField = True
        Me.fndSingleMCCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSingleMCCCode.MyLinkLable1 = Nothing
        Me.fndSingleMCCCode.MyLinkLable2 = Nothing
        Me.fndSingleMCCCode.MyReadOnly = False
        Me.fndSingleMCCCode.MyShowMasterFormButton = False
        Me.fndSingleMCCCode.Name = "fndSingleMCCCode"
        Me.fndSingleMCCCode.ReferenceFieldDesc = Nothing
        Me.fndSingleMCCCode.ReferenceFieldName = Nothing
        Me.fndSingleMCCCode.ReferenceTableName = Nothing
        Me.fndSingleMCCCode.Size = New System.Drawing.Size(144, 19)
        Me.fndSingleMCCCode.TabIndex = 407
        Me.fndSingleMCCCode.Value = ""
        '
        'rdbOther
        '
        Me.rdbOther.AutoSize = True
        Me.rdbOther.Checked = True
        Me.rdbOther.Location = New System.Drawing.Point(156, 52)
        Me.rdbOther.Name = "rdbOther"
        Me.rdbOther.Size = New System.Drawing.Size(55, 17)
        Me.rdbOther.TabIndex = 411
        Me.rdbOther.TabStop = True
        Me.rdbOther.Text = "Other"
        Me.rdbOther.UseVisualStyleBackColor = True
        '
        'rdbMainPlant
        '
        Me.rdbMainPlant.AutoSize = True
        Me.rdbMainPlant.Location = New System.Drawing.Point(39, 52)
        Me.rdbMainPlant.Name = "rdbMainPlant"
        Me.rdbMainPlant.Size = New System.Drawing.Size(80, 17)
        Me.rdbMainPlant.TabIndex = 410
        Me.rdbMainPlant.Text = "Main Plant"
        Me.rdbMainPlant.UseVisualStyleBackColor = True
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(3, 27)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel5.TabIndex = 409
        Me.MyLabel5.Text = "MCC"
        '
        'txtReciptMCC
        '
        Me.txtReciptMCC.arrDispalyMember = Nothing
        Me.txtReciptMCC.arrValueMember = Nothing
        Me.txtReciptMCC.Location = New System.Drawing.Point(39, 27)
        Me.txtReciptMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReciptMCC.MyLinkLable1 = Nothing
        Me.txtReciptMCC.MyLinkLable2 = Nothing
        Me.txtReciptMCC.MyNullText = "All"
        Me.txtReciptMCC.Name = "txtReciptMCC"
        Me.txtReciptMCC.Size = New System.Drawing.Size(247, 19)
        Me.txtReciptMCC.TabIndex = 408
        '
        'txtMilkReciptToDate
        '
        Me.txtMilkReciptToDate.CalculationExpression = Nothing
        Me.txtMilkReciptToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtMilkReciptToDate.FieldCode = Nothing
        Me.txtMilkReciptToDate.FieldDesc = Nothing
        Me.txtMilkReciptToDate.FieldMaxLength = 0
        Me.txtMilkReciptToDate.FieldName = Nothing
        Me.txtMilkReciptToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMilkReciptToDate.isCalculatedField = False
        Me.txtMilkReciptToDate.IsSourceFromTable = False
        Me.txtMilkReciptToDate.IsSourceFromValueList = False
        Me.txtMilkReciptToDate.IsUnique = False
        Me.txtMilkReciptToDate.Location = New System.Drawing.Point(157, 2)
        Me.txtMilkReciptToDate.MendatroryField = False
        Me.txtMilkReciptToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMilkReciptToDate.MyLinkLable1 = Nothing
        Me.txtMilkReciptToDate.MyLinkLable2 = Nothing
        Me.txtMilkReciptToDate.Name = "txtMilkReciptToDate"
        Me.txtMilkReciptToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMilkReciptToDate.ReferenceFieldDesc = Nothing
        Me.txtMilkReciptToDate.ReferenceFieldName = Nothing
        Me.txtMilkReciptToDate.ReferenceTableName = Nothing
        Me.txtMilkReciptToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtMilkReciptToDate.TabIndex = 20
        Me.txtMilkReciptToDate.TabStop = False
        Me.txtMilkReciptToDate.Text = "17-12-2011"
        Me.txtMilkReciptToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtMilkReceiptFromDate
        '
        Me.txtMilkReceiptFromDate.CalculationExpression = Nothing
        Me.txtMilkReceiptFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtMilkReceiptFromDate.FieldCode = Nothing
        Me.txtMilkReceiptFromDate.FieldDesc = Nothing
        Me.txtMilkReceiptFromDate.FieldMaxLength = 0
        Me.txtMilkReceiptFromDate.FieldName = Nothing
        Me.txtMilkReceiptFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMilkReceiptFromDate.isCalculatedField = False
        Me.txtMilkReceiptFromDate.IsSourceFromTable = False
        Me.txtMilkReceiptFromDate.IsSourceFromValueList = False
        Me.txtMilkReceiptFromDate.IsUnique = False
        Me.txtMilkReceiptFromDate.Location = New System.Drawing.Point(40, 2)
        Me.txtMilkReceiptFromDate.MendatroryField = False
        Me.txtMilkReceiptFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMilkReceiptFromDate.MyLinkLable1 = Nothing
        Me.txtMilkReceiptFromDate.MyLinkLable2 = Nothing
        Me.txtMilkReceiptFromDate.Name = "txtMilkReceiptFromDate"
        Me.txtMilkReceiptFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMilkReceiptFromDate.ReferenceFieldDesc = Nothing
        Me.txtMilkReceiptFromDate.ReferenceFieldName = Nothing
        Me.txtMilkReceiptFromDate.ReferenceTableName = Nothing
        Me.txtMilkReceiptFromDate.Size = New System.Drawing.Size(86, 20)
        Me.txtMilkReceiptFromDate.TabIndex = 19
        Me.txtMilkReceiptFromDate.TabStop = False
        Me.txtMilkReceiptFromDate.Text = "17-12-2011"
        Me.txtMilkReceiptFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(134, 4)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel3.TabIndex = 22
        Me.MyLabel3.Text = "To"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel4.TabIndex = 21
        Me.MyLabel4.Text = "From"
        '
        'chkRejection
        '
        Me.chkRejection.AutoSize = True
        Me.chkRejection.Location = New System.Drawing.Point(880, 11)
        Me.chkRejection.Name = "chkRejection"
        Me.chkRejection.Size = New System.Drawing.Size(115, 17)
        Me.chkRejection.TabIndex = 404
        Me.chkRejection.Text = "Include Rejection"
        Me.chkRejection.UseVisualStyleBackColor = True
        Me.chkRejection.Visible = False
        '
        'gbReportType
        '
        Me.gbReportType.Controls.Add(Me.rdbYearlyConsolidatedReportofMilkProcurement)
        Me.gbReportType.Controls.Add(Me.rdbYearlyConsolidatedofMilkPayment)
        Me.gbReportType.Controls.Add(Me.rdbUnitWiseBillSummary)
        Me.gbReportType.Controls.Add(Me.rdbTIPsummaryReportMCCandVLCwise)
        Me.gbReportType.Controls.Add(Me.rdbUnitWiseDeduction)
        Me.gbReportType.Controls.Add(Me.rdbRouteBillsAbstract)
        Me.gbReportType.Controls.Add(Me.rdbUnitWiseAnalysis)
        Me.gbReportType.Controls.Add(Me.rdbUnitWiseTotal)
        Me.gbReportType.Controls.Add(Me.rdbCheckList)
        Me.gbReportType.Controls.Add(Me.rdbMilkReceipts)
        Me.gbReportType.Controls.Add(Me.rdbProcurementAbstract)
        Me.gbReportType.Controls.Add(Me.rdbDetails)
        Me.gbReportType.Controls.Add(Me.rdbSummary)
        Me.gbReportType.Location = New System.Drawing.Point(351, 35)
        Me.gbReportType.Name = "gbReportType"
        Me.gbReportType.Size = New System.Drawing.Size(288, 314)
        Me.gbReportType.TabIndex = 403
        Me.gbReportType.TabStop = False
        '
        'rdbYearlyConsolidatedReportofMilkProcurement
        '
        Me.rdbYearlyConsolidatedReportofMilkProcurement.AutoSize = True
        Me.rdbYearlyConsolidatedReportofMilkProcurement.Location = New System.Drawing.Point(6, 283)
        Me.rdbYearlyConsolidatedReportofMilkProcurement.Name = "rdbYearlyConsolidatedReportofMilkProcurement"
        Me.rdbYearlyConsolidatedReportofMilkProcurement.Size = New System.Drawing.Size(270, 17)
        Me.rdbYearlyConsolidatedReportofMilkProcurement.TabIndex = 12
        Me.rdbYearlyConsolidatedReportofMilkProcurement.Text = "Yearly Consolidated Report of Milk Procurement"
        Me.rdbYearlyConsolidatedReportofMilkProcurement.UseVisualStyleBackColor = True
        '
        'rdbYearlyConsolidatedofMilkPayment
        '
        Me.rdbYearlyConsolidatedofMilkPayment.AutoSize = True
        Me.rdbYearlyConsolidatedofMilkPayment.Location = New System.Drawing.Point(6, 260)
        Me.rdbYearlyConsolidatedofMilkPayment.Name = "rdbYearlyConsolidatedofMilkPayment"
        Me.rdbYearlyConsolidatedofMilkPayment.Size = New System.Drawing.Size(210, 17)
        Me.rdbYearlyConsolidatedofMilkPayment.TabIndex = 11
        Me.rdbYearlyConsolidatedofMilkPayment.Text = "Yearly Consolidated of Milk Payment"
        Me.rdbYearlyConsolidatedofMilkPayment.UseVisualStyleBackColor = True
        '
        'rdbUnitWiseBillSummary
        '
        Me.rdbUnitWiseBillSummary.AutoSize = True
        Me.rdbUnitWiseBillSummary.Location = New System.Drawing.Point(6, 237)
        Me.rdbUnitWiseBillSummary.Name = "rdbUnitWiseBillSummary"
        Me.rdbUnitWiseBillSummary.Size = New System.Drawing.Size(139, 17)
        Me.rdbUnitWiseBillSummary.TabIndex = 10
        Me.rdbUnitWiseBillSummary.Text = "Unit wise Bill summary"
        Me.rdbUnitWiseBillSummary.UseVisualStyleBackColor = True
        '
        'rdbTIPsummaryReportMCCandVLCwise
        '
        Me.rdbTIPsummaryReportMCCandVLCwise.AutoSize = True
        Me.rdbTIPsummaryReportMCCandVLCwise.Location = New System.Drawing.Point(6, 214)
        Me.rdbTIPsummaryReportMCCandVLCwise.Name = "rdbTIPsummaryReportMCCandVLCwise"
        Me.rdbTIPsummaryReportMCCandVLCwise.Size = New System.Drawing.Size(202, 17)
        Me.rdbTIPsummaryReportMCCandVLCwise.TabIndex = 9
        Me.rdbTIPsummaryReportMCCandVLCwise.Text = "Premium report MCC and VLC wise"
        Me.rdbTIPsummaryReportMCCandVLCwise.UseVisualStyleBackColor = True
        '
        'rdbUnitWiseDeduction
        '
        Me.rdbUnitWiseDeduction.AutoSize = True
        Me.rdbUnitWiseDeduction.Location = New System.Drawing.Point(6, 191)
        Me.rdbUnitWiseDeduction.Name = "rdbUnitWiseDeduction"
        Me.rdbUnitWiseDeduction.Size = New System.Drawing.Size(130, 17)
        Me.rdbUnitWiseDeduction.TabIndex = 8
        Me.rdbUnitWiseDeduction.Text = "Unit wise Deduction"
        Me.rdbUnitWiseDeduction.UseVisualStyleBackColor = True
        '
        'rdbRouteBillsAbstract
        '
        Me.rdbRouteBillsAbstract.AutoSize = True
        Me.rdbRouteBillsAbstract.Location = New System.Drawing.Point(6, 172)
        Me.rdbRouteBillsAbstract.Name = "rdbRouteBillsAbstract"
        Me.rdbRouteBillsAbstract.Size = New System.Drawing.Size(124, 17)
        Me.rdbRouteBillsAbstract.TabIndex = 7
        Me.rdbRouteBillsAbstract.Text = "Route Bills Abstract"
        Me.rdbRouteBillsAbstract.UseVisualStyleBackColor = True
        '
        'rdbUnitWiseAnalysis
        '
        Me.rdbUnitWiseAnalysis.AutoSize = True
        Me.rdbUnitWiseAnalysis.Location = New System.Drawing.Point(6, 151)
        Me.rdbUnitWiseAnalysis.Name = "rdbUnitWiseAnalysis"
        Me.rdbUnitWiseAnalysis.Size = New System.Drawing.Size(117, 17)
        Me.rdbUnitWiseAnalysis.TabIndex = 6
        Me.rdbUnitWiseAnalysis.Text = "Unit wise Analysis"
        Me.rdbUnitWiseAnalysis.UseVisualStyleBackColor = True
        '
        'rdbUnitWiseTotal
        '
        Me.rdbUnitWiseTotal.AutoSize = True
        Me.rdbUnitWiseTotal.Location = New System.Drawing.Point(6, 128)
        Me.rdbUnitWiseTotal.Name = "rdbUnitWiseTotal"
        Me.rdbUnitWiseTotal.Size = New System.Drawing.Size(101, 17)
        Me.rdbUnitWiseTotal.TabIndex = 5
        Me.rdbUnitWiseTotal.Text = "Unit wise Total"
        Me.rdbUnitWiseTotal.UseVisualStyleBackColor = True
        '
        'rdbCheckList
        '
        Me.rdbCheckList.AutoSize = True
        Me.rdbCheckList.Location = New System.Drawing.Point(6, 105)
        Me.rdbCheckList.Name = "rdbCheckList"
        Me.rdbCheckList.Size = New System.Drawing.Size(76, 17)
        Me.rdbCheckList.TabIndex = 4
        Me.rdbCheckList.Text = "Check List"
        Me.rdbCheckList.UseVisualStyleBackColor = True
        '
        'rdbMilkReceipts
        '
        Me.rdbMilkReceipts.AutoSize = True
        Me.rdbMilkReceipts.Location = New System.Drawing.Point(6, 82)
        Me.rdbMilkReceipts.Name = "rdbMilkReceipts"
        Me.rdbMilkReceipts.Size = New System.Drawing.Size(93, 17)
        Me.rdbMilkReceipts.TabIndex = 3
        Me.rdbMilkReceipts.Text = "Milk Receipts"
        Me.rdbMilkReceipts.UseVisualStyleBackColor = True
        '
        'rdbProcurementAbstract
        '
        Me.rdbProcurementAbstract.AutoSize = True
        Me.rdbProcurementAbstract.Location = New System.Drawing.Point(6, 59)
        Me.rdbProcurementAbstract.Name = "rdbProcurementAbstract"
        Me.rdbProcurementAbstract.Size = New System.Drawing.Size(135, 17)
        Me.rdbProcurementAbstract.TabIndex = 2
        Me.rdbProcurementAbstract.Text = "Procurement Abstract"
        Me.rdbProcurementAbstract.UseVisualStyleBackColor = True
        '
        'rdbDetails
        '
        Me.rdbDetails.AutoSize = True
        Me.rdbDetails.Checked = True
        Me.rdbDetails.Location = New System.Drawing.Point(6, 13)
        Me.rdbDetails.Name = "rdbDetails"
        Me.rdbDetails.Size = New System.Drawing.Size(115, 17)
        Me.rdbDetails.TabIndex = 1
        Me.rdbDetails.TabStop = True
        Me.rdbDetails.Text = "Details (Mcc wise)"
        Me.rdbDetails.UseVisualStyleBackColor = True
        '
        'rdbSummary
        '
        Me.rdbSummary.AutoSize = True
        Me.rdbSummary.Location = New System.Drawing.Point(6, 36)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(132, 17)
        Me.rdbSummary.TabIndex = 0
        Me.rdbSummary.Text = "Summary (Plant wise)"
        Me.rdbSummary.UseVisualStyleBackColor = True
        '
        'txtLocName
        '
        Me.txtLocName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtLocName.CalculationExpression = Nothing
        Me.txtLocName.Enabled = False
        Me.txtLocName.FieldCode = Nothing
        Me.txtLocName.FieldDesc = Nothing
        Me.txtLocName.FieldMaxLength = 0
        Me.txtLocName.FieldName = Nothing
        Me.txtLocName.isCalculatedField = False
        Me.txtLocName.IsSourceFromTable = False
        Me.txtLocName.IsSourceFromValueList = False
        Me.txtLocName.IsUnique = False
        Me.txtLocName.Location = New System.Drawing.Point(281, 8)
        Me.txtLocName.MendatroryField = False
        Me.txtLocName.MyLinkLable1 = Nothing
        Me.txtLocName.MyLinkLable2 = Nothing
        Me.txtLocName.Name = "txtLocName"
        Me.txtLocName.ReferenceFieldDesc = Nothing
        Me.txtLocName.ReferenceFieldName = Nothing
        Me.txtLocName.ReferenceTableName = Nothing
        Me.txtLocName.Size = New System.Drawing.Size(146, 20)
        Me.txtLocName.TabIndex = 402
        '
        'fndLoc
        '
        Me.fndLoc.CalculationExpression = Nothing
        Me.fndLoc.FieldCode = Nothing
        Me.fndLoc.FieldDesc = Nothing
        Me.fndLoc.FieldMaxLength = 0
        Me.fndLoc.FieldName = Nothing
        Me.fndLoc.isCalculatedField = False
        Me.fndLoc.IsSourceFromTable = False
        Me.fndLoc.IsSourceFromValueList = False
        Me.fndLoc.IsUnique = False
        Me.fndLoc.Location = New System.Drawing.Point(77, 9)
        Me.fndLoc.MendatroryField = True
        Me.fndLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLoc.MyLinkLable1 = Nothing
        Me.fndLoc.MyLinkLable2 = Nothing
        Me.fndLoc.MyReadOnly = False
        Me.fndLoc.MyShowMasterFormButton = False
        Me.fndLoc.Name = "fndLoc"
        Me.fndLoc.ReferenceFieldDesc = Nothing
        Me.fndLoc.ReferenceFieldName = Nothing
        Me.fndLoc.ReferenceTableName = Nothing
        Me.fndLoc.Size = New System.Drawing.Size(198, 19)
        Me.fndLoc.TabIndex = 401
        Me.fndLoc.Value = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.dtpToDate)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.dtpFromDate)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 35)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(272, 31)
        Me.GroupBox1.TabIndex = 400
        Me.GroupBox1.TabStop = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(147, 11)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel2.TabIndex = 266
        Me.MyLabel2.Text = "To"
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(181, 9)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel2
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReadOnly = True
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 265
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "10/06/2011"
        Me.dtpToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel1.TabIndex = 264
        Me.MyLabel1.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(45, 9)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.MyLabel1
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpFromDate.TabIndex = 263
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "10/06/2011"
        Me.dtpFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(10, 146)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(30, 18)
        Me.lblMCCCode.TabIndex = 37
        Me.lblMCCCode.Text = "MCC"
        Me.lblMCCCode.Visible = False
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(54, 145)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "All"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(247, 19)
        Me.txtMCC.TabIndex = 36
        Me.txtMCC.Visible = False
        '
        'txtPlant
        '
        Me.txtPlant.arrDispalyMember = Nothing
        Me.txtPlant.arrValueMember = Nothing
        Me.txtPlant.Location = New System.Drawing.Point(54, 170)
        Me.txtPlant.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlant.MyLinkLable1 = Nothing
        Me.txtPlant.MyLinkLable2 = Nothing
        Me.txtPlant.MyNullText = "All"
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.Size = New System.Drawing.Size(247, 19)
        Me.txtPlant.TabIndex = 31
        Me.txtPlant.Visible = False
        '
        'lblPlant
        '
        Me.lblPlant.FieldName = Nothing
        Me.lblPlant.Location = New System.Drawing.Point(10, 10)
        Me.lblPlant.Name = "lblPlant"
        Me.lblPlant.Size = New System.Drawing.Size(31, 18)
        Me.lblPlant.TabIndex = 27
        Me.lblPlant.Text = "Shed"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1008, 397)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1008, 397)
        Me.gv1.TabIndex = 5
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1029, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'btnDotMatrixPrint
        '
        Me.btnDotMatrixPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDotMatrixPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDotMatrixPrint.Location = New System.Drawing.Point(268, 14)
        Me.btnDotMatrixPrint.Name = "btnDotMatrixPrint"
        Me.btnDotMatrixPrint.Size = New System.Drawing.Size(145, 22)
        Me.btnDotMatrixPrint.TabIndex = 48
        Me.btnDotMatrixPrint.Text = "Print By Dot Matrix Printer"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(167, 14)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 47
        Me.RadSplitButton1.Text = "Export"
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
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(90, 14)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(71, 22)
        Me.BtnReset.TabIndex = 43
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(934, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 44
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(13, 13)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 42
        Me.btnGo.Text = ">>>"
        '
        'rptMCCWiseAbstractReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptMCCWiseAbstractReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Wise Abstract Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.pnlMilkReceipts.ResumeLayout(False)
        Me.pnlMilkReceipts.PerformLayout()
        Me.pnlSingleMCCCode.ResumeLayout(False)
        Me.pnlSingleMCCCode.PerformLayout()
        CType(Me.lblSingleMCCName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkReciptToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkReceiptFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbReportType.ResumeLayout(False)
        Me.gbReportType.PerformLayout()
        CType(Me.txtLocName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDotMatrixPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblPlant As common.Controls.MyLabel
    Friend WithEvents txtPlant As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents fndLoc As common.UserControls.txtFinder
    Friend WithEvents txtLocName As common.Controls.MyTextBox
    Friend WithEvents btnDotMatrixPrint As RadButton
    Friend WithEvents gbReportType As GroupBox
    Friend WithEvents rdbDetails As RadioButton
    Friend WithEvents rdbSummary As RadioButton
    Friend WithEvents rdbProcurementAbstract As RadioButton
    Friend WithEvents chkRejection As CheckBox
    Friend WithEvents rdbMilkReceipts As RadioButton
    Friend WithEvents pnlMilkReceipts As Panel
    Friend WithEvents txtMilkReciptToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtMilkReceiptFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents rdbOther As RadioButton
    Friend WithEvents rdbMainPlant As RadioButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtReciptMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents rdbCheckList As RadioButton
    Friend WithEvents rdbUnitWiseTotal As RadioButton
    Friend WithEvents rdbUnitWiseAnalysis As RadioButton
    Friend WithEvents pnlSingleMCCCode As Panel
    Friend WithEvents lblSingleMCCName As common.Controls.MyTextBox
    Friend WithEvents fndSingleMCCCode As common.UserControls.txtFinder
    Friend WithEvents rdbRouteBillsAbstract As RadioButton
    Friend WithEvents rdbUnitWiseDeduction As RadioButton
    Friend WithEvents rdbTIPsummaryReportMCCandVLCwise As RadioButton
    Friend WithEvents rdbUnitWiseBillSummary As RadioButton
    Friend WithEvents rdbYearlyConsolidatedReportofMilkProcurement As RadioButton
    Friend WithEvents rdbYearlyConsolidatedofMilkPayment As RadioButton
End Class

