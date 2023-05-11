Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRptCustomerLedgerDemo
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
        Me.chkCompanySelect = New common.Controls.MyRadioButton()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkIncludeCardIndent = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTurnOver = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtZone = New common.UserControls.txtMultiSelectFinder()
        Me.chkExcludeOpening = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ChkUnapplied = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkreceipt = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkrefund = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkBankReverse = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAdvance = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkOnAccount = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkDebitNote = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkInvoice = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkApplyDocument = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkCreditNote = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkAdjustment = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludeApplyDocument = New Telerik.WinControls.UI.RadCheckBox()
        Me.LblSecurity = New common.Controls.MyLabel()
        Me.lblParentCustomer = New common.Controls.MyLabel()
        Me.txtParentCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.TxtSecurity = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.ddlCurrencyType = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCustomerType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtCustomerGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblCompany = New common.Controls.MyLabel()
        Me.txtCompany = New common.UserControls.txtMultiSelectFinder()
        Me.GrpCustType = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgcusttype = New common.MyCheckBoxGrid()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.ChkCustTypeSelect = New common.Controls.MyRadioButton()
        Me.ChkCustTypeAll = New common.Controls.MyRadioButton()
        Me.GrpCustCategory = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgcustcat = New common.MyCheckBoxGrid()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.ChkCustCatSelect = New common.Controls.MyRadioButton()
        Me.ChkCustCatAll = New common.Controls.MyRadioButton()
        Me.ChkSecurity = New Telerik.WinControls.UI.RadCheckBox()
        Me.GrpDocWise = New System.Windows.Forms.GroupBox()
        Me.ChkDocWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkDocSumm = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkISParentCust = New Telerik.WinControls.UI.RadCheckBox()
        Me.GrpIsParent = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgParentCust = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.ChkParentCustSelect = New common.Controls.MyRadioButton()
        Me.ChkParentCustAll = New common.Controls.MyRadioButton()
        Me.chkItemWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkCumulativeClosing = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnAreaWise = New System.Windows.Forms.RadioButton()
        Me.rdbtnZoneWise = New System.Windows.Forms.RadioButton()
        Me.rbtnCustomerType = New System.Windows.Forms.RadioButton()
        Me.rbtnCustomerCategory = New System.Windows.Forms.RadioButton()
        Me.chkMonthWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkDateWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.rbtnCustWiseDrCr = New System.Windows.Forms.RadioButton()
        Me.rbtnCustGroupWise = New System.Windows.Forms.RadioButton()
        Me.rbtnCustGroupWiseDrCr = New System.Windows.Forms.RadioButton()
        Me.rbtnDocWise = New System.Windows.Forms.RadioButton()
        Me.rbtnNone = New System.Windows.Forms.RadioButton()
        Me.rbtnCustWise = New System.Windows.Forms.RadioButton()
        Me.pnlActiveInActiveCustomer = New System.Windows.Forms.Panel()
        Me.chkActive = New System.Windows.Forms.RadioButton()
        Me.chkAll = New System.Windows.Forms.RadioButton()
        Me.chkInactive = New System.Windows.Forms.RadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustGrp = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkCustGrpSelect = New common.Controls.MyRadioButton()
        Me.chkCustGrpAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkLOcSelect = New common.Controls.MyRadioButton()
        Me.chkLOcALL = New common.Controls.MyRadioButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCustomer = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkCustomerSelect = New common.Controls.MyRadioButton()
        Me.chkCustomerAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgCompany = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkCompanyAll = New common.Controls.MyRadioButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDetails = New common.UserControls.MyRadGridView()
        Me.gvCustomer = New common.UserControls.MyRadGridView()
        Me.gvCustomerGroup = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.refreshbtn = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.pnlAdminSetting = New System.Windows.Forms.Panel()
        Me.chkMismatch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkReconcile = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.btnPrintConfirmation = New Telerik.WinControls.UI.RadButton()
        Me.btnQExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.QExpExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.QExpCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExcelGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDFGrid = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.chkCompanySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkIncludeCardIndent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTurnOver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcludeOpening, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ChkUnapplied, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkreceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkrefund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBankReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDebitNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreditNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdjustment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeApplyDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblParentCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpCustType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCustType.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.ChkCustTypeSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCustTypeAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpCustCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCustCategory.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.ChkCustCatSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkCustCatAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpDocWise.SuspendLayout()
        CType(Me.ChkDocWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDocSumm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpIsParent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpIsParent.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.ChkParentCustSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkParentCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCumulativeClosing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.chkMonthWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDateWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlActiveInActiveCustomer.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCustGrpSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustGrpAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLOcSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLOcALL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkCompanyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomerGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.refreshbtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdminSetting.SuspendLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.btnPrintConfirmation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnQExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkCompanySelect
        '
        Me.chkCompanySelect.Location = New System.Drawing.Point(180, 1)
        Me.chkCompanySelect.MyLinkLable1 = Nothing
        Me.chkCompanySelect.MyLinkLable2 = Nothing
        Me.chkCompanySelect.Name = "chkCompanySelect"
        Me.chkCompanySelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCompanySelect.TabIndex = 1
        Me.chkCompanySelect.Text = "Select"
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
        Me.txtToDate.Location = New System.Drawing.Point(130, 37)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 4
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtFromDate.Location = New System.Drawing.Point(130, 15)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(85, 20)
        Me.txtFromDate.TabIndex = 3
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1037, 436)
        Me.RadPageView1.TabIndex = 13
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1016, 388)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.chkIncludeCardIndent)
        Me.RadGroupBox1.Controls.Add(Me.chkTurnOver)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.TxtZone)
        Me.RadGroupBox1.Controls.Add(Me.chkExcludeOpening)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.chkIncludeApplyDocument)
        Me.RadGroupBox1.Controls.Add(Me.LblSecurity)
        Me.RadGroupBox1.Controls.Add(Me.lblParentCustomer)
        Me.RadGroupBox1.Controls.Add(Me.txtParentCustomer)
        Me.RadGroupBox1.Controls.Add(Me.TxtSecurity)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.ddlCurrencyType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerCategory)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomer)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox1.Controls.Add(Me.lblCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomerGroup)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblCompany)
        Me.RadGroupBox1.Controls.Add(Me.txtCompany)
        Me.RadGroupBox1.Controls.Add(Me.GrpCustType)
        Me.RadGroupBox1.Controls.Add(Me.GrpCustCategory)
        Me.RadGroupBox1.Controls.Add(Me.ChkSecurity)
        Me.RadGroupBox1.Controls.Add(Me.GrpDocWise)
        Me.RadGroupBox1.Controls.Add(Me.ChkISParentCust)
        Me.RadGroupBox1.Controls.Add(Me.GrpIsParent)
        Me.RadGroupBox1.Controls.Add(Me.chkItemWise)
        Me.RadGroupBox1.Controls.Add(Me.chkCumulativeClosing)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.pnlActiveInActiveCustomer)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1016, 388)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkIncludeCardIndent
        '
        Me.chkIncludeCardIndent.Location = New System.Drawing.Point(249, 237)
        Me.chkIncludeCardIndent.Name = "chkIncludeCardIndent"
        '
        '
        '
        Me.chkIncludeCardIndent.RootElement.StretchHorizontally = True
        Me.chkIncludeCardIndent.RootElement.StretchVertically = True
        Me.chkIncludeCardIndent.Size = New System.Drawing.Size(181, 18)
        Me.chkIncludeCardIndent.TabIndex = 404
        Me.chkIncludeCardIndent.Text = "Include Card Indent"
        '
        'chkTurnOver
        '
        Me.chkTurnOver.Location = New System.Drawing.Point(576, 171)
        Me.chkTurnOver.Name = "chkTurnOver"
        Me.chkTurnOver.Size = New System.Drawing.Size(70, 18)
        Me.chkTurnOver.TabIndex = 403
        Me.chkTurnOver.Text = "Turn Over"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 186)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel4.TabIndex = 402
        Me.MyLabel4.Text = "Zone"
        '
        'TxtZone
        '
        Me.TxtZone.arrDispalyMember = Nothing
        Me.TxtZone.arrValueMember = Nothing
        Me.TxtZone.Location = New System.Drawing.Point(130, 187)
        Me.TxtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtZone.MyLinkLable1 = Me.MyLabel4
        Me.TxtZone.MyLinkLable2 = Nothing
        Me.TxtZone.MyNullText = "All"
        Me.TxtZone.Name = "TxtZone"
        Me.TxtZone.Size = New System.Drawing.Size(344, 19)
        Me.TxtZone.TabIndex = 401
        '
        'chkExcludeOpening
        '
        Me.chkExcludeOpening.Location = New System.Drawing.Point(130, 236)
        Me.chkExcludeOpening.Name = "chkExcludeOpening"
        '
        '
        '
        Me.chkExcludeOpening.RootElement.StretchHorizontally = True
        Me.chkExcludeOpening.RootElement.StretchVertically = True
        Me.chkExcludeOpening.Size = New System.Drawing.Size(181, 18)
        Me.chkExcludeOpening.TabIndex = 400
        Me.chkExcludeOpening.Text = "Exclude Opening"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ChkUnapplied)
        Me.GroupBox2.Controls.Add(Me.chkreceipt)
        Me.GroupBox2.Controls.Add(Me.chkrefund)
        Me.GroupBox2.Controls.Add(Me.chkBankReverse)
        Me.GroupBox2.Controls.Add(Me.chkAdvance)
        Me.GroupBox2.Controls.Add(Me.chkOnAccount)
        Me.GroupBox2.Controls.Add(Me.chkDebitNote)
        Me.GroupBox2.Controls.Add(Me.ChkInvoice)
        Me.GroupBox2.Controls.Add(Me.chkApplyDocument)
        Me.GroupBox2.Controls.Add(Me.chkCreditNote)
        Me.GroupBox2.Controls.Add(Me.chkAdjustment)
        Me.GroupBox2.Location = New System.Drawing.Point(693, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(116, 216)
        Me.GroupBox2.TabIndex = 399
        Me.GroupBox2.TabStop = False
        '
        'ChkUnapplied
        '
        Me.ChkUnapplied.Location = New System.Drawing.Point(8, 192)
        Me.ChkUnapplied.Name = "ChkUnapplied"
        Me.ChkUnapplied.Size = New System.Drawing.Size(72, 18)
        Me.ChkUnapplied.TabIndex = 65
        Me.ChkUnapplied.Text = "Unapplied"
        '
        'chkreceipt
        '
        Me.chkreceipt.Location = New System.Drawing.Point(8, 138)
        Me.chkreceipt.Name = "chkreceipt"
        Me.chkreceipt.Size = New System.Drawing.Size(57, 18)
        Me.chkreceipt.TabIndex = 59
        Me.chkreceipt.Text = "Receipt"
        '
        'chkrefund
        '
        Me.chkrefund.Location = New System.Drawing.Point(8, 156)
        Me.chkrefund.Name = "chkrefund"
        Me.chkrefund.Size = New System.Drawing.Size(56, 18)
        Me.chkrefund.TabIndex = 59
        Me.chkrefund.Text = "Refund"
        '
        'chkBankReverse
        '
        Me.chkBankReverse.Location = New System.Drawing.Point(8, 174)
        Me.chkBankReverse.Name = "chkBankReverse"
        Me.chkBankReverse.Size = New System.Drawing.Size(86, 18)
        Me.chkBankReverse.TabIndex = 64
        Me.chkBankReverse.Text = "Bank Reverse"
        '
        'chkAdvance
        '
        Me.chkAdvance.Location = New System.Drawing.Point(8, 120)
        Me.chkAdvance.Name = "chkAdvance"
        Me.chkAdvance.Size = New System.Drawing.Size(63, 18)
        Me.chkAdvance.TabIndex = 63
        Me.chkAdvance.Text = "Advance"
        '
        'chkOnAccount
        '
        Me.chkOnAccount.Location = New System.Drawing.Point(8, 102)
        Me.chkOnAccount.Name = "chkOnAccount"
        Me.chkOnAccount.Size = New System.Drawing.Size(79, 18)
        Me.chkOnAccount.TabIndex = 62
        Me.chkOnAccount.Text = "On Account"
        '
        'chkDebitNote
        '
        Me.chkDebitNote.Location = New System.Drawing.Point(8, 48)
        Me.chkDebitNote.Name = "chkDebitNote"
        Me.chkDebitNote.Size = New System.Drawing.Size(75, 18)
        Me.chkDebitNote.TabIndex = 61
        Me.chkDebitNote.Text = "Debit Note"
        '
        'ChkInvoice
        '
        Me.ChkInvoice.Location = New System.Drawing.Point(8, 84)
        Me.ChkInvoice.Name = "ChkInvoice"
        Me.ChkInvoice.Size = New System.Drawing.Size(56, 18)
        Me.ChkInvoice.TabIndex = 59
        Me.ChkInvoice.Text = "Invoice"
        '
        'chkApplyDocument
        '
        Me.chkApplyDocument.Location = New System.Drawing.Point(8, 66)
        Me.chkApplyDocument.Name = "chkApplyDocument"
        Me.chkApplyDocument.Size = New System.Drawing.Size(104, 18)
        Me.chkApplyDocument.TabIndex = 60
        Me.chkApplyDocument.Text = "Apply Document"
        '
        'chkCreditNote
        '
        Me.chkCreditNote.Location = New System.Drawing.Point(8, 30)
        Me.chkCreditNote.Name = "chkCreditNote"
        Me.chkCreditNote.Size = New System.Drawing.Size(78, 18)
        Me.chkCreditNote.TabIndex = 59
        Me.chkCreditNote.Text = "Credit Note"
        '
        'chkAdjustment
        '
        Me.chkAdjustment.Location = New System.Drawing.Point(8, 12)
        Me.chkAdjustment.Name = "chkAdjustment"
        Me.chkAdjustment.Size = New System.Drawing.Size(74, 18)
        Me.chkAdjustment.TabIndex = 58
        Me.chkAdjustment.Text = "Adjusment"
        '
        'chkIncludeApplyDocument
        '
        Me.chkIncludeApplyDocument.Location = New System.Drawing.Point(480, 150)
        Me.chkIncludeApplyDocument.Name = "chkIncludeApplyDocument"
        '
        '
        '
        Me.chkIncludeApplyDocument.RootElement.StretchHorizontally = True
        Me.chkIncludeApplyDocument.RootElement.StretchVertically = True
        Me.chkIncludeApplyDocument.Size = New System.Drawing.Size(181, 18)
        Me.chkIncludeApplyDocument.TabIndex = 398
        Me.chkIncludeApplyDocument.Text = "Include Apply Document"
        '
        'LblSecurity
        '
        Me.LblSecurity.FieldName = Nothing
        Me.LblSecurity.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSecurity.Location = New System.Drawing.Point(11, 100)
        Me.LblSecurity.Name = "LblSecurity"
        Me.LblSecurity.Size = New System.Drawing.Size(46, 18)
        Me.LblSecurity.TabIndex = 385
        Me.LblSecurity.Text = "Security"
        '
        'lblParentCustomer
        '
        Me.lblParentCustomer.FieldName = Nothing
        Me.lblParentCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParentCustomer.Location = New System.Drawing.Point(340, 288)
        Me.lblParentCustomer.Name = "lblParentCustomer"
        Me.lblParentCustomer.Size = New System.Drawing.Size(90, 18)
        Me.lblParentCustomer.TabIndex = 378
        Me.lblParentCustomer.Text = "Parent Customer"
        Me.lblParentCustomer.Visible = False
        '
        'txtParentCustomer
        '
        Me.txtParentCustomer.arrDispalyMember = Nothing
        Me.txtParentCustomer.arrValueMember = Nothing
        Me.txtParentCustomer.Location = New System.Drawing.Point(462, 289)
        Me.txtParentCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParentCustomer.MyLinkLable1 = Me.lblParentCustomer
        Me.txtParentCustomer.MyLinkLable2 = Nothing
        Me.txtParentCustomer.MyNullText = "All"
        Me.txtParentCustomer.Name = "txtParentCustomer"
        Me.txtParentCustomer.Size = New System.Drawing.Size(344, 19)
        Me.txtParentCustomer.TabIndex = 377
        Me.txtParentCustomer.Visible = False
        '
        'TxtSecurity
        '
        Me.TxtSecurity.arrDispalyMember = Nothing
        Me.TxtSecurity.arrValueMember = Nothing
        Me.TxtSecurity.Location = New System.Drawing.Point(130, 101)
        Me.TxtSecurity.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSecurity.MyLinkLable1 = Nothing
        Me.TxtSecurity.MyLinkLable2 = Nothing
        Me.TxtSecurity.MyNullText = "All"
        Me.TxtSecurity.Name = "TxtSecurity"
        Me.TxtSecurity.Size = New System.Drawing.Size(344, 19)
        Me.TxtSecurity.TabIndex = 384
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(480, 130)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(54, 18)
        Me.MyLabel3.TabIndex = 371
        Me.MyLabel3.Text = "Currency "
        '
        'ddlCurrencyType
        '
        Me.ddlCurrencyType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlCurrencyType.Location = New System.Drawing.Point(536, 130)
        Me.ddlCurrencyType.Name = "ddlCurrencyType"
        Me.ddlCurrencyType.Size = New System.Drawing.Size(141, 20)
        Me.ddlCurrencyType.TabIndex = 383
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 164)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel2.TabIndex = 382
        Me.MyLabel2.Text = "Customer Type"
        '
        'txtCustomerType
        '
        Me.txtCustomerType.arrDispalyMember = Nothing
        Me.txtCustomerType.arrValueMember = Nothing
        Me.txtCustomerType.Location = New System.Drawing.Point(130, 165)
        Me.txtCustomerType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerType.MyLinkLable1 = Me.MyLabel2
        Me.txtCustomerType.MyLinkLable2 = Nothing
        Me.txtCustomerType.MyNullText = "All"
        Me.txtCustomerType.Name = "txtCustomerType"
        Me.txtCustomerType.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerType.TabIndex = 381
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 143)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel1.TabIndex = 380
        Me.MyLabel1.Text = "Customer Category"
        '
        'txtCustomerCategory
        '
        Me.txtCustomerCategory.arrDispalyMember = Nothing
        Me.txtCustomerCategory.arrValueMember = Nothing
        Me.txtCustomerCategory.Location = New System.Drawing.Point(130, 144)
        Me.txtCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCategory.MyLinkLable1 = Me.MyLabel1
        Me.txtCustomerCategory.MyLinkLable2 = Nothing
        Me.txtCustomerCategory.MyNullText = "All"
        Me.txtCustomerCategory.Name = "txtCustomerCategory"
        Me.txtCustomerCategory.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerCategory.TabIndex = 379
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(9, 208)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 376
        Me.lblCustomer.Text = "Customer"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(130, 209)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.lblCustomer
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomer.TabIndex = 375
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(9, 122)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(89, 18)
        Me.lblCustomerGroup.TabIndex = 374
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroup
        '
        Me.txtCustomerGroup.arrDispalyMember = Nothing
        Me.txtCustomerGroup.arrValueMember = Nothing
        Me.txtCustomerGroup.Location = New System.Drawing.Point(130, 123)
        Me.txtCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroup.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroup.MyLinkLable2 = Nothing
        Me.txtCustomerGroup.MyNullText = "All"
        Me.txtCustomerGroup.Name = "txtCustomerGroup"
        Me.txtCustomerGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtCustomerGroup.TabIndex = 373
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(9, 79)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 372
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(130, 80)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 371
        '
        'lblCompany
        '
        Me.lblCompany.FieldName = Nothing
        Me.lblCompany.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(9, 58)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(54, 18)
        Me.lblCompany.TabIndex = 370
        Me.lblCompany.Text = "Company"
        '
        'txtCompany
        '
        Me.txtCompany.arrDispalyMember = Nothing
        Me.txtCompany.arrValueMember = Nothing
        Me.txtCompany.Location = New System.Drawing.Point(130, 59)
        Me.txtCompany.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.MyLinkLable1 = Me.lblCompany
        Me.txtCompany.MyLinkLable2 = Nothing
        Me.txtCompany.MyNullText = "All"
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(344, 19)
        Me.txtCompany.TabIndex = 369
        '
        'GrpCustType
        '
        Me.GrpCustType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpCustType.Controls.Add(Me.cbgcusttype)
        Me.GrpCustType.Controls.Add(Me.Panel8)
        Me.GrpCustType.HeaderText = "Customer Type"
        Me.GrpCustType.Location = New System.Drawing.Point(8, 429)
        Me.GrpCustType.Name = "GrpCustType"
        Me.GrpCustType.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpCustType.Size = New System.Drawing.Size(270, 101)
        Me.GrpCustType.TabIndex = 60
        Me.GrpCustType.Text = "Customer Type"
        Me.GrpCustType.Visible = False
        '
        'cbgcusttype
        '
        Me.cbgcusttype.CheckedValue = Nothing
        Me.cbgcusttype.DataSource = Nothing
        Me.cbgcusttype.DisplayMember = "Name"
        Me.cbgcusttype.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgcusttype.Location = New System.Drawing.Point(10, 40)
        Me.cbgcusttype.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgcusttype.MyShowHeadrText = False
        Me.cbgcusttype.Name = "cbgcusttype"
        Me.cbgcusttype.Size = New System.Drawing.Size(250, 51)
        Me.cbgcusttype.TabIndex = 1
        Me.cbgcusttype.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.ChkCustTypeSelect)
        Me.Panel8.Controls.Add(Me.ChkCustTypeAll)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(250, 20)
        Me.Panel8.TabIndex = 0
        '
        'ChkCustTypeSelect
        '
        Me.ChkCustTypeSelect.Location = New System.Drawing.Point(181, 1)
        Me.ChkCustTypeSelect.MyLinkLable1 = Nothing
        Me.ChkCustTypeSelect.MyLinkLable2 = Nothing
        Me.ChkCustTypeSelect.Name = "ChkCustTypeSelect"
        Me.ChkCustTypeSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkCustTypeSelect.TabIndex = 1
        Me.ChkCustTypeSelect.Text = "Select"
        '
        'ChkCustTypeAll
        '
        Me.ChkCustTypeAll.Location = New System.Drawing.Point(130, 1)
        Me.ChkCustTypeAll.MyLinkLable1 = Nothing
        Me.ChkCustTypeAll.MyLinkLable2 = Nothing
        Me.ChkCustTypeAll.Name = "ChkCustTypeAll"
        Me.ChkCustTypeAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkCustTypeAll.TabIndex = 0
        Me.ChkCustTypeAll.Text = "All"
        '
        'GrpCustCategory
        '
        Me.GrpCustCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpCustCategory.Controls.Add(Me.cbgcustcat)
        Me.GrpCustCategory.Controls.Add(Me.Panel7)
        Me.GrpCustCategory.HeaderText = "Customer Category"
        Me.GrpCustCategory.Location = New System.Drawing.Point(284, 733)
        Me.GrpCustCategory.Name = "GrpCustCategory"
        Me.GrpCustCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpCustCategory.Size = New System.Drawing.Size(265, 103)
        Me.GrpCustCategory.TabIndex = 59
        Me.GrpCustCategory.Text = "Customer Category"
        Me.GrpCustCategory.Visible = False
        '
        'cbgcustcat
        '
        Me.cbgcustcat.CheckedValue = Nothing
        Me.cbgcustcat.DataSource = Nothing
        Me.cbgcustcat.DisplayMember = "Name"
        Me.cbgcustcat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgcustcat.Location = New System.Drawing.Point(10, 40)
        Me.cbgcustcat.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgcustcat.MyShowHeadrText = False
        Me.cbgcustcat.Name = "cbgcustcat"
        Me.cbgcustcat.Size = New System.Drawing.Size(245, 53)
        Me.cbgcustcat.TabIndex = 1
        Me.cbgcustcat.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.ChkCustCatSelect)
        Me.Panel7.Controls.Add(Me.ChkCustCatAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(245, 20)
        Me.Panel7.TabIndex = 0
        '
        'ChkCustCatSelect
        '
        Me.ChkCustCatSelect.Location = New System.Drawing.Point(181, 1)
        Me.ChkCustCatSelect.MyLinkLable1 = Nothing
        Me.ChkCustCatSelect.MyLinkLable2 = Nothing
        Me.ChkCustCatSelect.Name = "ChkCustCatSelect"
        Me.ChkCustCatSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkCustCatSelect.TabIndex = 1
        Me.ChkCustCatSelect.Text = "Select"
        '
        'ChkCustCatAll
        '
        Me.ChkCustCatAll.Location = New System.Drawing.Point(130, 1)
        Me.ChkCustCatAll.MyLinkLable1 = Nothing
        Me.ChkCustCatAll.MyLinkLable2 = Nothing
        Me.ChkCustCatAll.Name = "ChkCustCatAll"
        Me.ChkCustCatAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkCustCatAll.TabIndex = 0
        Me.ChkCustCatAll.Text = "All"
        '
        'ChkSecurity
        '
        Me.ChkSecurity.Location = New System.Drawing.Point(480, 171)
        Me.ChkSecurity.Name = "ChkSecurity"
        Me.ChkSecurity.Size = New System.Drawing.Size(90, 18)
        Me.ChkSecurity.TabIndex = 57
        Me.ChkSecurity.Text = "Show Security"
        Me.ChkSecurity.Visible = False
        '
        'GrpDocWise
        '
        Me.GrpDocWise.Controls.Add(Me.ChkDocWise)
        Me.GrpDocWise.Controls.Add(Me.ChkDocSumm)
        Me.GrpDocWise.Location = New System.Drawing.Point(284, 429)
        Me.GrpDocWise.Name = "GrpDocWise"
        Me.GrpDocWise.Size = New System.Drawing.Size(186, 40)
        Me.GrpDocWise.TabIndex = 56
        Me.GrpDocWise.TabStop = False
        Me.GrpDocWise.Visible = False
        '
        'ChkDocWise
        '
        Me.ChkDocWise.Location = New System.Drawing.Point(9, -1)
        Me.ChkDocWise.Name = "ChkDocWise"
        Me.ChkDocWise.Size = New System.Drawing.Size(144, 18)
        Me.ChkDocWise.TabIndex = 54
        Me.ChkDocWise.Text = "Document Pending Wise"
        Me.ChkDocWise.Visible = False
        '
        'ChkDocSumm
        '
        Me.ChkDocSumm.Location = New System.Drawing.Point(29, 19)
        Me.ChkDocSumm.Name = "ChkDocSumm"
        Me.ChkDocSumm.Size = New System.Drawing.Size(124, 18)
        Me.ChkDocSumm.TabIndex = 55
        Me.ChkDocSumm.Text = "Summary (Doc Wise)"
        '
        'ChkISParentCust
        '
        Me.ChkISParentCust.Location = New System.Drawing.Point(480, 234)
        Me.ChkISParentCust.Name = "ChkISParentCust"
        Me.ChkISParentCust.Size = New System.Drawing.Size(115, 18)
        Me.ChkISParentCust.TabIndex = 53
        Me.ChkISParentCust.Text = "Is Parent Customer"
        Me.ChkISParentCust.Visible = False
        '
        'GrpIsParent
        '
        Me.GrpIsParent.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpIsParent.Controls.Add(Me.cbgParentCust)
        Me.GrpIsParent.Controls.Add(Me.Panel6)
        Me.GrpIsParent.HeaderText = "Parent Customer"
        Me.GrpIsParent.Location = New System.Drawing.Point(555, 408)
        Me.GrpIsParent.Name = "GrpIsParent"
        Me.GrpIsParent.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.GrpIsParent.Size = New System.Drawing.Size(261, 92)
        Me.GrpIsParent.TabIndex = 52
        Me.GrpIsParent.Text = "Parent Customer"
        Me.GrpIsParent.Visible = False
        '
        'cbgParentCust
        '
        Me.cbgParentCust.CheckedValue = Nothing
        Me.cbgParentCust.DataSource = Nothing
        Me.cbgParentCust.DisplayMember = "Name"
        Me.cbgParentCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgParentCust.Location = New System.Drawing.Point(10, 40)
        Me.cbgParentCust.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgParentCust.MyShowHeadrText = False
        Me.cbgParentCust.Name = "cbgParentCust"
        Me.cbgParentCust.Size = New System.Drawing.Size(241, 42)
        Me.cbgParentCust.TabIndex = 1
        Me.cbgParentCust.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.ChkParentCustSelect)
        Me.Panel6.Controls.Add(Me.ChkParentCustAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(241, 20)
        Me.Panel6.TabIndex = 0
        '
        'ChkParentCustSelect
        '
        Me.ChkParentCustSelect.Location = New System.Drawing.Point(179, 1)
        Me.ChkParentCustSelect.MyLinkLable1 = Nothing
        Me.ChkParentCustSelect.MyLinkLable2 = Nothing
        Me.ChkParentCustSelect.Name = "ChkParentCustSelect"
        Me.ChkParentCustSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkParentCustSelect.TabIndex = 1
        Me.ChkParentCustSelect.Text = "Select"
        '
        'ChkParentCustAll
        '
        Me.ChkParentCustAll.Location = New System.Drawing.Point(128, 1)
        Me.ChkParentCustAll.MyLinkLable1 = Nothing
        Me.ChkParentCustAll.MyLinkLable2 = Nothing
        Me.ChkParentCustAll.Name = "ChkParentCustAll"
        Me.ChkParentCustAll.Size = New System.Drawing.Size(33, 18)
        Me.ChkParentCustAll.TabIndex = 0
        Me.ChkParentCustAll.Text = "All"
        '
        'chkItemWise
        '
        Me.chkItemWise.Location = New System.Drawing.Point(480, 192)
        Me.chkItemWise.Name = "chkItemWise"
        Me.chkItemWise.Size = New System.Drawing.Size(70, 18)
        Me.chkItemWise.TabIndex = 30
        Me.chkItemWise.Text = "Item Wise"
        Me.chkItemWise.Visible = False
        '
        'chkCumulativeClosing
        '
        Me.chkCumulativeClosing.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCumulativeClosing.Location = New System.Drawing.Point(480, 213)
        Me.chkCumulativeClosing.Name = "chkCumulativeClosing"
        Me.chkCumulativeClosing.Size = New System.Drawing.Size(147, 18)
        Me.chkCumulativeClosing.TabIndex = 29
        Me.chkCumulativeClosing.Text = "Show Cumulative Closing"
        Me.chkCumulativeClosing.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkCumulativeClosing.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnAreaWise)
        Me.GroupBox1.Controls.Add(Me.rdbtnZoneWise)
        Me.GroupBox1.Controls.Add(Me.rbtnCustomerType)
        Me.GroupBox1.Controls.Add(Me.rbtnCustomerCategory)
        Me.GroupBox1.Controls.Add(Me.chkMonthWise)
        Me.GroupBox1.Controls.Add(Me.chkDateWise)
        Me.GroupBox1.Controls.Add(Me.rbtnCustWiseDrCr)
        Me.GroupBox1.Controls.Add(Me.rbtnCustGroupWise)
        Me.GroupBox1.Controls.Add(Me.rbtnCustGroupWiseDrCr)
        Me.GroupBox1.Controls.Add(Me.rbtnDocWise)
        Me.GroupBox1.Controls.Add(Me.rbtnNone)
        Me.GroupBox1.Controls.Add(Me.rbtnCustWise)
        Me.GroupBox1.Location = New System.Drawing.Point(221, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(714, 54)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Summary"
        '
        'rbtnAreaWise
        '
        Me.rbtnAreaWise.AutoSize = True
        Me.rbtnAreaWise.Location = New System.Drawing.Point(527, 33)
        Me.rbtnAreaWise.Name = "rbtnAreaWise"
        Me.rbtnAreaWise.Size = New System.Drawing.Size(76, 17)
        Me.rbtnAreaWise.TabIndex = 409
        Me.rbtnAreaWise.Text = "Area Wise"
        Me.rbtnAreaWise.UseVisualStyleBackColor = True
        Me.rbtnAreaWise.Visible = False
        '
        'rdbtnZoneWise
        '
        Me.rdbtnZoneWise.AutoSize = True
        Me.rdbtnZoneWise.Location = New System.Drawing.Point(527, 12)
        Me.rdbtnZoneWise.Name = "rdbtnZoneWise"
        Me.rdbtnZoneWise.Size = New System.Drawing.Size(79, 17)
        Me.rdbtnZoneWise.TabIndex = 408
        Me.rdbtnZoneWise.Text = "Zone Wise"
        Me.rdbtnZoneWise.UseVisualStyleBackColor = True
        Me.rdbtnZoneWise.Visible = False
        '
        'rbtnCustomerType
        '
        Me.rbtnCustomerType.AutoSize = True
        Me.rbtnCustomerType.Location = New System.Drawing.Point(400, 31)
        Me.rbtnCustomerType.Name = "rbtnCustomerType"
        Me.rbtnCustomerType.Size = New System.Drawing.Size(100, 17)
        Me.rbtnCustomerType.TabIndex = 407
        Me.rbtnCustomerType.Text = "Customer Type"
        Me.rbtnCustomerType.UseVisualStyleBackColor = True
        '
        'rbtnCustomerCategory
        '
        Me.rbtnCustomerCategory.AutoSize = True
        Me.rbtnCustomerCategory.Location = New System.Drawing.Point(399, 12)
        Me.rbtnCustomerCategory.Name = "rbtnCustomerCategory"
        Me.rbtnCustomerCategory.Size = New System.Drawing.Size(123, 17)
        Me.rbtnCustomerCategory.TabIndex = 406
        Me.rbtnCustomerCategory.Text = "Customer Category"
        Me.rbtnCustomerCategory.UseVisualStyleBackColor = True
        '
        'chkMonthWise
        '
        Me.chkMonthWise.Location = New System.Drawing.Point(609, 31)
        Me.chkMonthWise.Name = "chkMonthWise"
        '
        '
        '
        Me.chkMonthWise.RootElement.StretchHorizontally = True
        Me.chkMonthWise.RootElement.StretchVertically = True
        Me.chkMonthWise.Size = New System.Drawing.Size(91, 18)
        Me.chkMonthWise.TabIndex = 400
        Me.chkMonthWise.Text = "MonthWise"
        '
        'chkDateWise
        '
        Me.chkDateWise.Location = New System.Drawing.Point(609, 10)
        Me.chkDateWise.Name = "chkDateWise"
        '
        '
        '
        Me.chkDateWise.RootElement.StretchHorizontally = True
        Me.chkDateWise.RootElement.StretchVertically = True
        Me.chkDateWise.Size = New System.Drawing.Size(91, 18)
        Me.chkDateWise.TabIndex = 399
        Me.chkDateWise.Text = "DateWise"
        '
        'rbtnCustWiseDrCr
        '
        Me.rbtnCustWiseDrCr.AutoSize = True
        Me.rbtnCustWiseDrCr.Location = New System.Drawing.Point(151, 33)
        Me.rbtnCustWiseDrCr.Name = "rbtnCustWiseDrCr"
        Me.rbtnCustWiseDrCr.Size = New System.Drawing.Size(132, 17)
        Me.rbtnCustWiseDrCr.TabIndex = 32
        Me.rbtnCustWiseDrCr.Text = "Customer Wise Dr/Cr"
        Me.rbtnCustWiseDrCr.UseVisualStyleBackColor = True
        '
        'rbtnCustGroupWise
        '
        Me.rbtnCustGroupWise.AutoSize = True
        Me.rbtnCustGroupWise.Location = New System.Drawing.Point(5, 13)
        Me.rbtnCustGroupWise.Name = "rbtnCustGroupWise"
        Me.rbtnCustGroupWise.Size = New System.Drawing.Size(138, 17)
        Me.rbtnCustGroupWise.TabIndex = 27
        Me.rbtnCustGroupWise.Text = "Customer Group Wise"
        Me.rbtnCustGroupWise.UseVisualStyleBackColor = True
        '
        'rbtnCustGroupWiseDrCr
        '
        Me.rbtnCustGroupWiseDrCr.AutoSize = True
        Me.rbtnCustGroupWiseDrCr.Location = New System.Drawing.Point(5, 33)
        Me.rbtnCustGroupWiseDrCr.Name = "rbtnCustGroupWiseDrCr"
        Me.rbtnCustGroupWiseDrCr.Size = New System.Drawing.Size(143, 17)
        Me.rbtnCustGroupWiseDrCr.TabIndex = 31
        Me.rbtnCustGroupWiseDrCr.Text = "Customer Group Dr/Cr "
        Me.rbtnCustGroupWiseDrCr.UseVisualStyleBackColor = True
        '
        'rbtnDocWise
        '
        Me.rbtnDocWise.AutoSize = True
        Me.rbtnDocWise.Location = New System.Drawing.Point(286, 33)
        Me.rbtnDocWise.Name = "rbtnDocWise"
        Me.rbtnDocWise.Size = New System.Drawing.Size(73, 17)
        Me.rbtnDocWise.TabIndex = 30
        Me.rbtnDocWise.Text = "Doc Wise"
        Me.rbtnDocWise.UseVisualStyleBackColor = True
        '
        'rbtnNone
        '
        Me.rbtnNone.AutoSize = True
        Me.rbtnNone.Location = New System.Drawing.Point(286, 13)
        Me.rbtnNone.Name = "rbtnNone"
        Me.rbtnNone.Size = New System.Drawing.Size(106, 17)
        Me.rbtnNone.TabIndex = 29
        Me.rbtnNone.Text = "Document Wise"
        Me.rbtnNone.UseVisualStyleBackColor = True
        '
        'rbtnCustWise
        '
        Me.rbtnCustWise.AutoSize = True
        Me.rbtnCustWise.Location = New System.Drawing.Point(151, 13)
        Me.rbtnCustWise.Name = "rbtnCustWise"
        Me.rbtnCustWise.Size = New System.Drawing.Size(102, 17)
        Me.rbtnCustWise.TabIndex = 28
        Me.rbtnCustWise.Text = "Customer Wise"
        Me.rbtnCustWise.UseVisualStyleBackColor = True
        '
        'pnlActiveInActiveCustomer
        '
        Me.pnlActiveInActiveCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkActive)
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkAll)
        Me.pnlActiveInActiveCustomer.Controls.Add(Me.chkInactive)
        Me.pnlActiveInActiveCustomer.Location = New System.Drawing.Point(480, 57)
        Me.pnlActiveInActiveCustomer.Name = "pnlActiveInActiveCustomer"
        Me.pnlActiveInActiveCustomer.Size = New System.Drawing.Size(198, 67)
        Me.pnlActiveInActiveCustomer.TabIndex = 27
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Location = New System.Drawing.Point(3, 23)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(107, 17)
        Me.chkActive.TabIndex = 27
        Me.chkActive.Text = "Active Customer"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(3, 3)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(95, 17)
        Me.chkAll.TabIndex = 26
        Me.chkAll.Text = "All Customers"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'chkInactive
        '
        Me.chkInactive.AutoSize = True
        Me.chkInactive.Location = New System.Drawing.Point(3, 43)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(116, 17)
        Me.chkInactive.TabIndex = 25
        Me.chkInactive.Text = "Inactive Customer"
        Me.chkInactive.UseVisualStyleBackColor = True
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgCustGrp)
        Me.RadGroupBox5.Controls.Add(Me.Panel4)
        Me.RadGroupBox5.HeaderText = "Customer Group"
        Me.RadGroupBox5.Location = New System.Drawing.Point(8, 408)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(270, 92)
        Me.RadGroupBox5.TabIndex = 18
        Me.RadGroupBox5.Text = "Customer Group"
        Me.RadGroupBox5.Visible = False
        '
        'cbgCustGrp
        '
        Me.cbgCustGrp.CheckedValue = Nothing
        Me.cbgCustGrp.DataSource = Nothing
        Me.cbgCustGrp.DisplayMember = "Name"
        Me.cbgCustGrp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustGrp.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustGrp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustGrp.MyShowHeadrText = False
        Me.cbgCustGrp.Name = "cbgCustGrp"
        Me.cbgCustGrp.Size = New System.Drawing.Size(250, 42)
        Me.cbgCustGrp.TabIndex = 1
        Me.cbgCustGrp.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkCustGrpSelect)
        Me.Panel4.Controls.Add(Me.chkCustGrpAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(250, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkCustGrpSelect
        '
        Me.chkCustGrpSelect.Location = New System.Drawing.Point(156, 1)
        Me.chkCustGrpSelect.MyLinkLable1 = Nothing
        Me.chkCustGrpSelect.MyLinkLable2 = Nothing
        Me.chkCustGrpSelect.Name = "chkCustGrpSelect"
        Me.chkCustGrpSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustGrpSelect.TabIndex = 1
        Me.chkCustGrpSelect.Text = "Select"
        '
        'chkCustGrpAll
        '
        Me.chkCustGrpAll.Location = New System.Drawing.Point(105, 1)
        Me.chkCustGrpAll.MyLinkLable1 = Nothing
        Me.chkCustGrpAll.MyLinkLable2 = Nothing
        Me.chkCustGrpAll.Name = "chkCustGrpAll"
        Me.chkCustGrpAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustGrpAll.TabIndex = 0
        Me.chkCustGrpAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Location"
        Me.RadGroupBox4.Location = New System.Drawing.Point(284, 408)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(265, 92)
        Me.RadGroupBox4.TabIndex = 15
        Me.RadGroupBox4.Text = "Location"
        Me.RadGroupBox4.Visible = False
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(245, 42)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLOcSelect)
        Me.Panel3.Controls.Add(Me.chkLOcALL)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(245, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLOcSelect
        '
        Me.chkLOcSelect.Location = New System.Drawing.Point(179, 1)
        Me.chkLOcSelect.MyLinkLable1 = Nothing
        Me.chkLOcSelect.MyLinkLable2 = Nothing
        Me.chkLOcSelect.Name = "chkLOcSelect"
        Me.chkLOcSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLOcSelect.TabIndex = 1
        Me.chkLOcSelect.Text = "Select"
        '
        'chkLOcALL
        '
        Me.chkLOcALL.Location = New System.Drawing.Point(128, 1)
        Me.chkLOcALL.MyLinkLable1 = Nothing
        Me.chkLOcALL.MyLinkLable2 = Nothing
        Me.chkLOcALL.Name = "chkLOcALL"
        Me.chkLOcALL.Size = New System.Drawing.Size(33, 18)
        Me.chkLOcALL.TabIndex = 0
        Me.chkLOcALL.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Customer"
        Me.RadGroupBox3.Location = New System.Drawing.Point(8, 733)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(270, 103)
        Me.RadGroupBox3.TabIndex = 15
        Me.RadGroupBox3.Text = "Customer"
        Me.RadGroupBox3.Visible = False
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(250, 53)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkCustomerSelect)
        Me.Panel2.Controls.Add(Me.chkCustomerAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(250, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(179, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(128, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgCompany)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Company"
        Me.RadGroupBox2.Location = New System.Drawing.Point(555, 733)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(261, 103)
        Me.RadGroupBox2.TabIndex = 14
        Me.RadGroupBox2.Text = "Company"
        Me.RadGroupBox2.Visible = False
        '
        'cbgCompany
        '
        Me.cbgCompany.CheckedValue = Nothing
        Me.cbgCompany.DataSource = Nothing
        Me.cbgCompany.DisplayMember = "Name"
        Me.cbgCompany.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCompany.Location = New System.Drawing.Point(10, 40)
        Me.cbgCompany.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCompany.MyShowHeadrText = False
        Me.cbgCompany.Name = "cbgCompany"
        Me.cbgCompany.Size = New System.Drawing.Size(241, 53)
        Me.cbgCompany.TabIndex = 1
        Me.cbgCompany.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCompanySelect)
        Me.Panel1.Controls.Add(Me.chkCompanyAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(241, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkCompanyAll
        '
        Me.chkCompanyAll.Location = New System.Drawing.Point(129, 1)
        Me.chkCompanyAll.MyLinkLable1 = Nothing
        Me.chkCompanyAll.MyLinkLable2 = Nothing
        Me.chkCompanyAll.Name = "chkCompanyAll"
        Me.chkCompanyAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCompanyAll.TabIndex = 0
        Me.chkCompanyAll.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(10, 37)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(10, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvDetails)
        Me.RadPageViewPage2.Controls.Add(Me.gvCustomer)
        Me.RadPageViewPage2.Controls.Add(Me.gvCustomerGroup)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1016, 388)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvDetails
        '
        Me.gvDetails.Location = New System.Drawing.Point(3, 296)
        '
        '
        '
        Me.gvDetails.MasterTemplate.AllowAddNewRow = False
        Me.gvDetails.MasterTemplate.AllowEditRow = False
        Me.gvDetails.MasterTemplate.EnableFiltering = True
        Me.gvDetails.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDetails.Name = "gvDetails"
        Me.gvDetails.ShowGroupPanel = False
        Me.gvDetails.ShowHeaderCellButtons = True
        Me.gvDetails.Size = New System.Drawing.Size(845, 153)
        Me.gvDetails.TabIndex = 0
        Me.gvDetails.Text = "RadGridView1"
        '
        'gvCustomer
        '
        Me.gvCustomer.Location = New System.Drawing.Point(3, 141)
        '
        '
        '
        Me.gvCustomer.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomer.MasterTemplate.AllowEditRow = False
        Me.gvCustomer.MasterTemplate.EnableFiltering = True
        Me.gvCustomer.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.ShowGroupPanel = False
        Me.gvCustomer.ShowHeaderCellButtons = True
        Me.gvCustomer.Size = New System.Drawing.Size(845, 149)
        Me.gvCustomer.TabIndex = 2
        Me.gvCustomer.Text = "RadGridView1"
        '
        'gvCustomerGroup
        '
        Me.gvCustomerGroup.Location = New System.Drawing.Point(3, 3)
        '
        '
        '
        Me.gvCustomerGroup.MasterTemplate.AllowAddNewRow = False
        Me.gvCustomerGroup.MasterTemplate.AllowEditRow = False
        Me.gvCustomerGroup.MasterTemplate.EnableFiltering = True
        Me.gvCustomerGroup.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCustomerGroup.Name = "gvCustomerGroup"
        Me.gvCustomerGroup.ShowGroupPanel = False
        Me.gvCustomerGroup.ShowHeaderCellButtons = True
        Me.gvCustomerGroup.Size = New System.Drawing.Size(845, 134)
        Me.gvCustomerGroup.TabIndex = 1
        Me.gvCustomerGroup.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(815, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(70, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'refreshbtn
        '
        Me.refreshbtn.Location = New System.Drawing.Point(5, 6)
        Me.refreshbtn.Name = "refreshbtn"
        Me.refreshbtn.Size = New System.Drawing.Size(70, 18)
        Me.refreshbtn.TabIndex = 8
        Me.refreshbtn.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(239, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 330
        Me.btnReset.Text = "Reset"
        '
        'pnlAdminSetting
        '
        Me.pnlAdminSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminSetting.Controls.Add(Me.chkMismatch)
        Me.pnlAdminSetting.Controls.Add(Me.chkReconcile)
        Me.pnlAdminSetting.Location = New System.Drawing.Point(565, 5)
        Me.pnlAdminSetting.Name = "pnlAdminSetting"
        Me.pnlAdminSetting.Size = New System.Drawing.Size(170, 19)
        Me.pnlAdminSetting.TabIndex = 328
        Me.pnlAdminSetting.Visible = False
        '
        'chkMismatch
        '
        Me.chkMismatch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMismatch.Location = New System.Drawing.Point(81, -1)
        Me.chkMismatch.Name = "chkMismatch"
        Me.chkMismatch.Size = New System.Drawing.Size(81, 18)
        Me.chkMismatch.TabIndex = 19
        Me.chkMismatch.Text = "Mismatched"
        Me.chkMismatch.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkReconcile
        '
        Me.chkReconcile.Location = New System.Drawing.Point(3, -1)
        Me.chkReconcile.Name = "chkReconcile"
        Me.chkReconcile.Size = New System.Drawing.Size(68, 18)
        Me.chkReconcile.TabIndex = 18
        Me.chkReconcile.Text = "Reconcile"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(78, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(739, 6)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(70, 18)
        Me.btnBack.TabIndex = 329
        Me.btnBack.Text = "<< Back"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1037, 20)
        Me.RadMenu1.TabIndex = 15
        Me.RadMenu1.Text = "RadMenu1"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnPrintConfirmation)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnQExport)
        Me.SplitContainer3.Panel2.Controls.Add(Me.refreshbtn)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer3.Panel2.Controls.Add(Me.pnlAdminSetting)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer3.Size = New System.Drawing.Size(1037, 470)
        Me.SplitContainer3.SplitterDistance = 436
        Me.SplitContainer3.TabIndex = 17
        '
        'btnPrintConfirmation
        '
        Me.btnPrintConfirmation.Location = New System.Drawing.Point(313, 6)
        Me.btnPrintConfirmation.Name = "btnPrintConfirmation"
        Me.btnPrintConfirmation.Size = New System.Drawing.Size(127, 18)
        Me.btnPrintConfirmation.TabIndex = 334
        Me.btnPrintConfirmation.Text = "Print Confirmation"
        '
        'btnQExport
        '
        Me.btnQExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.QExpExcel, Me.QExpCSV, Me.PDF, Me.BulkExcel, Me.BulkCSV, Me.ExcelGrid, Me.PDFGrid})
        Me.btnQExport.Location = New System.Drawing.Point(152, 6)
        Me.btnQExport.Name = "btnQExport"
        Me.btnQExport.Size = New System.Drawing.Size(82, 18)
        Me.btnQExport.TabIndex = 333
        Me.btnQExport.Text = "Export"
        '
        'QExpExcel
        '
        Me.QExpExcel.AccessibleDescription = "Excel"
        Me.QExpExcel.AccessibleName = "Excel"
        Me.QExpExcel.Name = "QExpExcel"
        Me.QExpExcel.Text = "Excel"
        '
        'QExpCSV
        '
        Me.QExpCSV.AccessibleDescription = "CSV"
        Me.QExpCSV.AccessibleName = "CSV"
        Me.QExpCSV.Name = "QExpCSV"
        Me.QExpCSV.Text = "CSV"
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'BulkExcel
        '
        Me.BulkExcel.AccessibleDescription = "Bulk Excel"
        Me.BulkExcel.AccessibleName = "Bulk Excel"
        Me.BulkExcel.Name = "BulkExcel"
        Me.BulkExcel.Text = "Bulk Excel"
        '
        'BulkCSV
        '
        Me.BulkCSV.AccessibleDescription = "Bulk CSV"
        Me.BulkCSV.AccessibleName = "Bulk CSV"
        Me.BulkCSV.Name = "BulkCSV"
        Me.BulkCSV.Text = "Bulk CSV"
        '
        'ExcelGrid
        '
        Me.ExcelGrid.AccessibleDescription = "ExcelGrid"
        Me.ExcelGrid.AccessibleName = "ExcelGrid"
        Me.ExcelGrid.Name = "ExcelGrid"
        Me.ExcelGrid.Text = "Excel(Grid)"
        '
        'PDFGrid
        '
        Me.PDFGrid.AccessibleDescription = "PDFGrid"
        Me.PDFGrid.AccessibleName = "PDFGrid"
        Me.PDFGrid.Name = "PDFGrid"
        Me.PDFGrid.Text = "PDF(Grid)"
        '
        'FrmRptCustomerLedgerDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1037, 490)
        Me.Controls.Add(Me.SplitContainer3)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmRptCustomerLedgerDemo"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Ledger"
        CType(Me.chkCompanySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkIncludeCardIndent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTurnOver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcludeOpening, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ChkUnapplied, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkreceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkrefund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBankReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDebitNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreditNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdjustment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeApplyDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblParentCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlCurrencyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpCustType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpCustType.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.ChkCustTypeSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCustTypeAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpCustCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpCustCategory.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.ChkCustCatSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkCustCatAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpDocWise.ResumeLayout(False)
        Me.GrpDocWise.PerformLayout()
        CType(Me.ChkDocWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDocSumm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkISParentCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpIsParent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpIsParent.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.ChkParentCustSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkParentCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCumulativeClosing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.chkMonthWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDateWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlActiveInActiveCustomer.ResumeLayout(False)
        Me.pnlActiveInActiveCustomer.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCustGrpSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustGrpAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLOcSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLOcALL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkCompanyAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvDetails.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomerGroup.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.refreshbtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdminSetting.ResumeLayout(False)
        Me.pnlAdminSetting.PerformLayout()
        CType(Me.chkMismatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkReconcile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.btnPrintConfirmation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnQExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkCompanySelect As common.Controls.MyRadioButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkAll As System.Windows.Forms.RadioButton
    Friend WithEvents chkInactive As System.Windows.Forms.RadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustGrp As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkCustGrpSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustGrpAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLOcSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLOcALL As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCompany As common.MyCheckBoxGrid
    Friend WithEvents chkCompanyAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDetails As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlAdminSetting As System.Windows.Forms.Panel
    Friend WithEvents chkMismatch As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkReconcile As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents refreshbtn As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCustGroupWise As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCustWise As System.Windows.Forms.RadioButton
    Friend WithEvents pnlActiveInActiveCustomer As System.Windows.Forms.Panel
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvCustomer As common.UserControls.MyRadGridView
    Friend WithEvents gvCustomerGroup As common.UserControls.MyRadGridView
    Friend WithEvents chkActive As System.Windows.Forms.RadioButton
    Friend WithEvents chkCumulativeClosing As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkItemWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GrpIsParent As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgParentCust As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents ChkParentCustSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkParentCustAll As common.Controls.MyRadioButton
    Friend WithEvents ChkISParentCust As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkDocWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkDocSumm As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GrpDocWise As System.Windows.Forms.GroupBox
    Friend WithEvents ChkSecurity As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GrpCustCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgcustcat As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents ChkCustCatSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkCustCatAll As common.Controls.MyRadioButton
    Friend WithEvents GrpCustType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgcusttype As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents ChkCustTypeSelect As common.Controls.MyRadioButton
    Friend WithEvents ChkCustTypeAll As common.Controls.MyRadioButton
    Friend WithEvents lblParentCustomer As common.Controls.MyLabel
    Friend WithEvents txtParentCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtCustomerGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCompany As common.Controls.MyLabel
    Friend WithEvents txtCompany As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustomerType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustomerCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents ddlCurrencyType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents LblSecurity As common.Controls.MyLabel
    Friend WithEvents TxtSecurity As common.UserControls.txtMultiSelectFinder
    Friend WithEvents rbtnDocWise As System.Windows.Forms.RadioButton
    Friend WithEvents chkIncludeApplyDocument As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkreceipt As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkrefund As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkBankReverse As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAdvance As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOnAccount As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkDebitNote As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkInvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkApplyDocument As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkCreditNote As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAdjustment As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkUnapplied As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnQExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents QExpExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents QExpCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtnCustGroupWiseDrCr As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCustWiseDrCr As System.Windows.Forms.RadioButton
    Friend WithEvents chkExcludeOpening As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkDateWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents ExcelGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDFGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkMonthWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnCustomerType As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnCustomerCategory As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAreaWise As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtnZoneWise As System.Windows.Forms.RadioButton
    Friend WithEvents chkTurnOver As RadCheckBox
    Friend WithEvents chkIncludeCardIndent As RadCheckBox
    Friend WithEvents btnPrintConfirmation As RadButton
End Class

