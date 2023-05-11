<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptTrialBalanceNew
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.gvDB = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.chkIncludeYearEndEntry = New common.Controls.MyCheckBox()
        Me.chkIndAS = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtFiscalYear = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.chkShowNetBalance = New common.Controls.MyCheckBox()
        Me.chkCusVendWiseSummary = New common.Controls.MyCheckBox()
        Me.txtACGrpType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.chkIncludeingClosingEntry = New common.Controls.MyCheckBox()
        Me.chkIncludeingAdjustmentEntry = New common.Controls.MyCheckBox()
        Me.txtMainGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblCompany = New common.Controls.MyLabel()
        Me.lblLocationSegment = New common.Controls.MyLabel()
        Me.lblAccount = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblEmployee = New common.Controls.MyLabel()
        Me.lblSourceCode = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.lblVISIPMX = New common.Controls.MyLabel()
        Me.lblMachine = New common.Controls.MyLabel()
        Me.lblVehicle = New common.Controls.MyLabel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtCompany = New common.UserControls.txtMultiSelectFinder()
        Me.txtLocationSegmant = New common.UserControls.txtMultiSelectFinder()
        Me.txtAccount = New common.UserControls.txtMultiSelectFinder()
        Me.txtEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.txtSourceCode = New common.UserControls.txtMultiSelectFinder()
        Me.txtDepartment = New common.UserControls.txtMultiSelectFinder()
        Me.txtVISIPMX = New common.UserControls.txtMultiSelectFinder()
        Me.txtMachine = New common.UserControls.txtMultiSelectFinder()
        Me.txtVehicle = New common.UserControls.txtMultiSelectFinder()
        Me.gbAcc = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgAccount = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkAccSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkAccAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.gbDept = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgDept = New common.MyCheckBoxGrid()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.chkDeptSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkDeptAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.gbEmployee = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgEmployee = New common.MyCheckBoxGrid()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.chkEmpSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkEmpAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.grpLocaSegment = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocSeg = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbtnLocSegSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnLocSegAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.gbVisi = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVisi = New common.MyCheckBoxGrid()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.chkVisiSelct = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVisiAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.grpCompany = New Telerik.WinControls.UI.RadGroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbtnSelectCompany = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnAllCompany = New Telerik.WinControls.UI.RadRadioButton()
        Me.gbMachines = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgmachine = New common.MyCheckBoxGrid()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkMachineSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkMachineAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.gbVehicle = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgVehicle = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkVehicleSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkVehicleAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.gbSourceCode = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgSourceCode = New common.MyCheckBoxGrid()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.chkSrcCodeSelect = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkSrcCodeAll = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkExcludeTemplete = New common.Controls.MyCheckBox()
        Me.chkMultipleRollup = New common.Controls.MyCheckBox()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromdate = New common.Controls.MyLabel()
        Me.chkRollupWise = New common.Controls.MyCheckBox()
        Me.chkShowOPBal = New common.Controls.MyCheckBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.cbgSrcCode = New common.Controls.MyComboBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnQExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.QExpExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.QExpCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnChangeOrder = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkIncludeUnusedAccount = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.chkIncludeYearEndEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowNetBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCusVendWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeingAdjustmentEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel2.SuspendLayout()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblAccount.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSourceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVISIPMX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.gbAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAcc.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkAccSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAccAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbDept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDept.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.chkDeptSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDeptAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEmployee.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkEmpSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkEmpAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpLocaSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLocaSegment.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnLocSegSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLocSegAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbVisi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVisi.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.chkVisiSelct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVisiAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCompany.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbMachines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMachines.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkMachineSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMachineAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVehicle.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkVehicleSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVehicleAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbSourceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSourceCode.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.chkSrcCodeSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSrcCodeAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcludeTemplete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMultipleRollup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRollupWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowOPBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgSrcCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnQExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnChangeOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeUnusedAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gvDB
        '
        Me.gvDB.AllowAddNewRow = False
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(977, 358)
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
        Me.RadPageViewPage1.Size = New System.Drawing.Size(956, 310)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.chkIncludeUnusedAccount)
        Me.RadPanel1.Controls.Add(Me.chkIncludeYearEndEntry)
        Me.RadPanel1.Controls.Add(Me.chkIndAS)
        Me.RadPanel1.Controls.Add(Me.txtFiscalYear)
        Me.RadPanel1.Controls.Add(Me.RadLabel1)
        Me.RadPanel1.Controls.Add(Me.chkShowNetBalance)
        Me.RadPanel1.Controls.Add(Me.chkCusVendWiseSummary)
        Me.RadPanel1.Controls.Add(Me.txtACGrpType)
        Me.RadPanel1.Controls.Add(Me.MyLabel3)
        Me.RadPanel1.Controls.Add(Me.chkIncludeingClosingEntry)
        Me.RadPanel1.Controls.Add(Me.chkIncludeingAdjustmentEntry)
        Me.RadPanel1.Controls.Add(Me.txtMainGroup)
        Me.RadPanel1.Controls.Add(Me.MyLabel2)
        Me.RadPanel1.Controls.Add(Me.FlowLayoutPanel2)
        Me.RadPanel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.RadPanel1.Controls.Add(Me.chkExcludeTemplete)
        Me.RadPanel1.Controls.Add(Me.chkMultipleRollup)
        Me.RadPanel1.Controls.Add(Me.txtFromDate)
        Me.RadPanel1.Controls.Add(Me.txtToDate)
        Me.RadPanel1.Controls.Add(Me.lblToDate)
        Me.RadPanel1.Controls.Add(Me.lblFromdate)
        Me.RadPanel1.Controls.Add(Me.chkRollupWise)
        Me.RadPanel1.Controls.Add(Me.chkShowOPBal)
        Me.RadPanel1.Controls.Add(Me.RadLabel3)
        Me.RadPanel1.Controls.Add(Me.cbgSrcCode)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(956, 310)
        Me.RadPanel1.TabIndex = 15
        '
        'chkIncludeYearEndEntry
        '
        Me.chkIncludeYearEndEntry.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeYearEndEntry.Location = New System.Drawing.Point(667, 72)
        Me.chkIncludeYearEndEntry.MyLinkLable1 = Nothing
        Me.chkIncludeYearEndEntry.MyLinkLable2 = Nothing
        Me.chkIncludeYearEndEntry.Name = "chkIncludeYearEndEntry"
        Me.chkIncludeYearEndEntry.Size = New System.Drawing.Size(142, 18)
        Me.chkIncludeYearEndEntry.TabIndex = 366
        Me.chkIncludeYearEndEntry.Tag1 = Nothing
        Me.chkIncludeYearEndEntry.Text = "Including Year End Entry"
        Me.chkIncludeYearEndEntry.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkIndAS
        '
        Me.chkIndAS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIndAS.Location = New System.Drawing.Point(359, 138)
        Me.chkIndAS.Name = "chkIndAS"
        '
        '
        '
        Me.chkIndAS.RootElement.StretchHorizontally = True
        Me.chkIndAS.RootElement.StretchVertically = True
        Me.chkIndAS.Size = New System.Drawing.Size(89, 16)
        Me.chkIndAS.TabIndex = 365
        Me.chkIndAS.Text = "Ind As"
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
        Me.txtFiscalYear.Location = New System.Drawing.Point(118, 2)
        Me.txtFiscalYear.MendatroryField = False
        Me.txtFiscalYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFiscalYear.MyLinkLable1 = Me.RadLabel1
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.MyReadOnly = False
        Me.txtFiscalYear.MyShowMasterFormButton = False
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(231, 20)
        Me.txtFiscalYear.TabIndex = 0
        Me.txtFiscalYear.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(9, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(58, 18)
        Me.RadLabel1.TabIndex = 364
        Me.RadLabel1.Text = "Fiscal Year"
        '
        'chkShowNetBalance
        '
        Me.chkShowNetBalance.Location = New System.Drawing.Point(359, 114)
        Me.chkShowNetBalance.MyLinkLable1 = Nothing
        Me.chkShowNetBalance.MyLinkLable2 = Nothing
        Me.chkShowNetBalance.Name = "chkShowNetBalance"
        Me.chkShowNetBalance.Size = New System.Drawing.Size(110, 18)
        Me.chkShowNetBalance.TabIndex = 13
        Me.chkShowNetBalance.Tag1 = Nothing
        Me.chkShowNetBalance.Text = "Show Net Balance"
        '
        'chkCusVendWiseSummary
        '
        Me.chkCusVendWiseSummary.Location = New System.Drawing.Point(359, 93)
        Me.chkCusVendWiseSummary.MyLinkLable1 = Nothing
        Me.chkCusVendWiseSummary.MyLinkLable2 = Nothing
        Me.chkCusVendWiseSummary.Name = "chkCusVendWiseSummary"
        Me.chkCusVendWiseSummary.Size = New System.Drawing.Size(282, 18)
        Me.chkCusVendWiseSummary.TabIndex = 12
        Me.chkCusVendWiseSummary.Tag1 = Nothing
        Me.chkCusVendWiseSummary.Text = "Show Customer/Vendor wise Summary on Drilldown"
        '
        'txtACGrpType
        '
        Me.txtACGrpType.arrDispalyMember = Nothing
        Me.txtACGrpType.arrValueMember = Nothing
        Me.txtACGrpType.Location = New System.Drawing.Point(118, 48)
        Me.txtACGrpType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtACGrpType.MyLinkLable1 = Nothing
        Me.txtACGrpType.MyLinkLable2 = Nothing
        Me.txtACGrpType.MyNullText = "All"
        Me.txtACGrpType.Name = "txtACGrpType"
        Me.txtACGrpType.Size = New System.Drawing.Size(231, 19)
        Me.txtACGrpType.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 49)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(109, 18)
        Me.MyLabel3.TabIndex = 359
        Me.MyLabel3.Text = "Account Group Type"
        '
        'chkIncludeingClosingEntry
        '
        Me.chkIncludeingClosingEntry.Location = New System.Drawing.Point(525, 72)
        Me.chkIncludeingClosingEntry.MyLinkLable1 = Nothing
        Me.chkIncludeingClosingEntry.MyLinkLable2 = Nothing
        Me.chkIncludeingClosingEntry.Name = "chkIncludeingClosingEntry"
        Me.chkIncludeingClosingEntry.Size = New System.Drawing.Size(135, 18)
        Me.chkIncludeingClosingEntry.TabIndex = 11
        Me.chkIncludeingClosingEntry.Tag1 = Nothing
        Me.chkIncludeingClosingEntry.Text = "Including Closing Entry"
        '
        'chkIncludeingAdjustmentEntry
        '
        Me.chkIncludeingAdjustmentEntry.Location = New System.Drawing.Point(359, 72)
        Me.chkIncludeingAdjustmentEntry.MyLinkLable1 = Nothing
        Me.chkIncludeingAdjustmentEntry.MyLinkLable2 = Nothing
        Me.chkIncludeingAdjustmentEntry.Name = "chkIncludeingAdjustmentEntry"
        Me.chkIncludeingAdjustmentEntry.Size = New System.Drawing.Size(156, 18)
        Me.chkIncludeingAdjustmentEntry.TabIndex = 10
        Me.chkIncludeingAdjustmentEntry.Tag1 = Nothing
        Me.chkIncludeingAdjustmentEntry.Text = "Including Adjustment Entry"
        '
        'txtMainGroup
        '
        Me.txtMainGroup.arrDispalyMember = Nothing
        Me.txtMainGroup.arrValueMember = Nothing
        Me.txtMainGroup.Location = New System.Drawing.Point(118, 71)
        Me.txtMainGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMainGroup.MyLinkLable1 = Nothing
        Me.txtMainGroup.MyLinkLable2 = Nothing
        Me.txtMainGroup.MyNullText = "All"
        Me.txtMainGroup.Name = "txtMainGroup"
        Me.txtMainGroup.Size = New System.Drawing.Size(231, 19)
        Me.txtMainGroup.TabIndex = 9
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 72)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel2.TabIndex = 356
        Me.MyLabel2.Text = "Account Group"
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.lblCompany)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblLocationSegment)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblAccount)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblEmployee)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblSourceCode)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblDepartment)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblVISIPMX)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblMachine)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblVehicle)
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(3, 94)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(101, 228)
        Me.FlowLayoutPanel2.TabIndex = 354
        '
        'lblCompany
        '
        Me.lblCompany.FieldName = Nothing
        Me.lblCompany.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(3, 3)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(54, 18)
        Me.lblCompany.TabIndex = 342
        Me.lblCompany.Text = "Company"
        '
        'lblLocationSegment
        '
        Me.lblLocationSegment.FieldName = Nothing
        Me.lblLocationSegment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationSegment.Location = New System.Drawing.Point(3, 27)
        Me.lblLocationSegment.Name = "lblLocationSegment"
        Me.lblLocationSegment.Size = New System.Drawing.Size(96, 18)
        Me.lblLocationSegment.TabIndex = 336
        Me.lblLocationSegment.Text = "Location Segment"
        '
        'lblAccount
        '
        Me.lblAccount.Controls.Add(Me.MyLabel1)
        Me.lblAccount.FieldName = Nothing
        Me.lblAccount.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.Location = New System.Drawing.Point(3, 51)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(47, 18)
        Me.lblAccount.TabIndex = 340
        Me.lblAccount.Text = "Account"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 24)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel1.TabIndex = 338
        Me.MyLabel1.Text = "Source Code"
        '
        'lblEmployee
        '
        Me.lblEmployee.FieldName = Nothing
        Me.lblEmployee.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployee.Location = New System.Drawing.Point(3, 75)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(55, 18)
        Me.lblEmployee.TabIndex = 344
        Me.lblEmployee.Text = "Employee"
        '
        'lblSourceCode
        '
        Me.lblSourceCode.FieldName = Nothing
        Me.lblSourceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceCode.Location = New System.Drawing.Point(3, 99)
        Me.lblSourceCode.Name = "lblSourceCode"
        Me.lblSourceCode.Size = New System.Drawing.Size(70, 18)
        Me.lblSourceCode.TabIndex = 338
        Me.lblSourceCode.Text = "Source Code"
        '
        'lblDepartment
        '
        Me.lblDepartment.FieldName = Nothing
        Me.lblDepartment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(3, 123)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(66, 18)
        Me.lblDepartment.TabIndex = 345
        Me.lblDepartment.Text = "Department"
        '
        'lblVISIPMX
        '
        Me.lblVISIPMX.FieldName = Nothing
        Me.lblVISIPMX.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVISIPMX.Location = New System.Drawing.Point(3, 147)
        Me.lblVISIPMX.Name = "lblVISIPMX"
        Me.lblVISIPMX.Size = New System.Drawing.Size(53, 18)
        Me.lblVISIPMX.TabIndex = 347
        Me.lblVISIPMX.Text = "VISI/PMX"
        '
        'lblMachine
        '
        Me.lblMachine.FieldName = Nothing
        Me.lblMachine.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachine.Location = New System.Drawing.Point(3, 171)
        Me.lblMachine.Name = "lblMachine"
        Me.lblMachine.Size = New System.Drawing.Size(49, 18)
        Me.lblMachine.TabIndex = 349
        Me.lblMachine.Text = "Machine"
        '
        'lblVehicle
        '
        Me.lblVehicle.FieldName = Nothing
        Me.lblVehicle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicle.Location = New System.Drawing.Point(3, 195)
        Me.lblVehicle.Name = "lblVehicle"
        Me.lblVehicle.Size = New System.Drawing.Size(42, 18)
        Me.lblVehicle.TabIndex = 351
        Me.lblVehicle.Text = "Vehicle"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.txtCompany)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtLocationSegmant)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtAccount)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtEmployee)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtSourceCode)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtDepartment)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtVISIPMX)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtMachine)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtVehicle)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbAcc)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbDept)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbEmployee)
        Me.FlowLayoutPanel1.Controls.Add(Me.grpLocaSegment)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbVisi)
        Me.FlowLayoutPanel1.Controls.Add(Me.grpCompany)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbMachines)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbVehicle)
        Me.FlowLayoutPanel1.Controls.Add(Me.gbSourceCode)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(116, 94)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(237, 228)
        Me.FlowLayoutPanel1.TabIndex = 14
        '
        'txtCompany
        '
        Me.txtCompany.arrDispalyMember = Nothing
        Me.txtCompany.arrValueMember = Nothing
        Me.txtCompany.Location = New System.Drawing.Point(3, 3)
        Me.txtCompany.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.MyLinkLable1 = Nothing
        Me.txtCompany.MyLinkLable2 = Nothing
        Me.txtCompany.MyNullText = "All"
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(231, 19)
        Me.txtCompany.TabIndex = 0
        '
        'txtLocationSegmant
        '
        Me.txtLocationSegmant.arrDispalyMember = Nothing
        Me.txtLocationSegmant.arrValueMember = Nothing
        Me.txtLocationSegmant.Location = New System.Drawing.Point(3, 28)
        Me.txtLocationSegmant.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationSegmant.MyLinkLable1 = Nothing
        Me.txtLocationSegmant.MyLinkLable2 = Nothing
        Me.txtLocationSegmant.MyNullText = "All"
        Me.txtLocationSegmant.Name = "txtLocationSegmant"
        Me.txtLocationSegmant.Size = New System.Drawing.Size(231, 19)
        Me.txtLocationSegmant.TabIndex = 1
        '
        'txtAccount
        '
        Me.txtAccount.arrDispalyMember = Nothing
        Me.txtAccount.arrValueMember = Nothing
        Me.txtAccount.Location = New System.Drawing.Point(3, 53)
        Me.txtAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccount.MyLinkLable1 = Nothing
        Me.txtAccount.MyLinkLable2 = Nothing
        Me.txtAccount.MyNullText = "All"
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(231, 19)
        Me.txtAccount.TabIndex = 2
        '
        'txtEmployee
        '
        Me.txtEmployee.arrDispalyMember = Nothing
        Me.txtEmployee.arrValueMember = Nothing
        Me.txtEmployee.Location = New System.Drawing.Point(3, 78)
        Me.txtEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployee.MyLinkLable1 = Me.lblSourceCode
        Me.txtEmployee.MyLinkLable2 = Nothing
        Me.txtEmployee.MyNullText = "All"
        Me.txtEmployee.Name = "txtEmployee"
        Me.txtEmployee.Size = New System.Drawing.Size(231, 19)
        Me.txtEmployee.TabIndex = 3
        '
        'txtSourceCode
        '
        Me.txtSourceCode.arrDispalyMember = Nothing
        Me.txtSourceCode.arrValueMember = Nothing
        Me.txtSourceCode.Location = New System.Drawing.Point(3, 103)
        Me.txtSourceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSourceCode.MyLinkLable1 = Nothing
        Me.txtSourceCode.MyLinkLable2 = Nothing
        Me.txtSourceCode.MyNullText = "All"
        Me.txtSourceCode.Name = "txtSourceCode"
        Me.txtSourceCode.Size = New System.Drawing.Size(231, 19)
        Me.txtSourceCode.TabIndex = 4
        '
        'txtDepartment
        '
        Me.txtDepartment.arrDispalyMember = Nothing
        Me.txtDepartment.arrValueMember = Nothing
        Me.txtDepartment.Location = New System.Drawing.Point(3, 128)
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Nothing
        Me.txtDepartment.MyLinkLable2 = Nothing
        Me.txtDepartment.MyNullText = "All"
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(231, 19)
        Me.txtDepartment.TabIndex = 5
        '
        'txtVISIPMX
        '
        Me.txtVISIPMX.arrDispalyMember = Nothing
        Me.txtVISIPMX.arrValueMember = Nothing
        Me.txtVISIPMX.Location = New System.Drawing.Point(3, 153)
        Me.txtVISIPMX.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVISIPMX.MyLinkLable1 = Nothing
        Me.txtVISIPMX.MyLinkLable2 = Nothing
        Me.txtVISIPMX.MyNullText = "All"
        Me.txtVISIPMX.Name = "txtVISIPMX"
        Me.txtVISIPMX.Size = New System.Drawing.Size(231, 19)
        Me.txtVISIPMX.TabIndex = 6
        '
        'txtMachine
        '
        Me.txtMachine.arrDispalyMember = Nothing
        Me.txtMachine.arrValueMember = Nothing
        Me.txtMachine.Location = New System.Drawing.Point(3, 178)
        Me.txtMachine.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachine.MyLinkLable1 = Nothing
        Me.txtMachine.MyLinkLable2 = Nothing
        Me.txtMachine.MyNullText = "All"
        Me.txtMachine.Name = "txtMachine"
        Me.txtMachine.Size = New System.Drawing.Size(231, 19)
        Me.txtMachine.TabIndex = 7
        '
        'txtVehicle
        '
        Me.txtVehicle.arrDispalyMember = Nothing
        Me.txtVehicle.arrValueMember = Nothing
        Me.txtVehicle.Location = New System.Drawing.Point(3, 203)
        Me.txtVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.MyLinkLable1 = Nothing
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.MyNullText = "All"
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.Size = New System.Drawing.Size(231, 19)
        Me.txtVehicle.TabIndex = 8
        '
        'gbAcc
        '
        Me.gbAcc.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbAcc.Controls.Add(Me.cbgAccount)
        Me.gbAcc.Controls.Add(Me.Panel4)
        Me.gbAcc.HeaderText = "Account"
        Me.gbAcc.Location = New System.Drawing.Point(3, 228)
        Me.gbAcc.Name = "gbAcc"
        Me.gbAcc.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbAcc.Size = New System.Drawing.Size(68, 58)
        Me.gbAcc.TabIndex = 69
        Me.gbAcc.Text = "Account"
        Me.gbAcc.Visible = False
        '
        'cbgAccount
        '
        Me.cbgAccount.CheckedValue = Nothing
        Me.cbgAccount.DataSource = Nothing
        Me.cbgAccount.DisplayMember = "Name"
        Me.cbgAccount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgAccount.Location = New System.Drawing.Point(10, 40)
        Me.cbgAccount.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgAccount.MyShowHeadrText = False
        Me.cbgAccount.Name = "cbgAccount"
        Me.cbgAccount.Size = New System.Drawing.Size(48, 8)
        Me.cbgAccount.TabIndex = 1
        Me.cbgAccount.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkAccSelect)
        Me.Panel4.Controls.Add(Me.chkAccAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(48, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkAccSelect
        '
        Me.chkAccSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkAccSelect.Location = New System.Drawing.Point(17, 1)
        Me.chkAccSelect.Name = "chkAccSelect"
        Me.chkAccSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkAccSelect.TabIndex = 1
        Me.chkAccSelect.Text = "Select"
        '
        'chkAccAll
        '
        Me.chkAccAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkAccAll.Location = New System.Drawing.Point(-26, 1)
        Me.chkAccAll.Name = "chkAccAll"
        Me.chkAccAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAccAll.TabIndex = 0
        Me.chkAccAll.Text = "All"
        '
        'gbDept
        '
        Me.gbDept.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbDept.Controls.Add(Me.cbgDept)
        Me.gbDept.Controls.Add(Me.Panel7)
        Me.gbDept.HeaderText = "Department"
        Me.gbDept.Location = New System.Drawing.Point(3, 292)
        Me.gbDept.Name = "gbDept"
        Me.gbDept.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbDept.Size = New System.Drawing.Size(165, 57)
        Me.gbDept.TabIndex = 74
        Me.gbDept.Text = "Department"
        Me.gbDept.Visible = False
        '
        'cbgDept
        '
        Me.cbgDept.CheckedValue = Nothing
        Me.cbgDept.DataSource = Nothing
        Me.cbgDept.DisplayMember = "Name"
        Me.cbgDept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgDept.Location = New System.Drawing.Point(10, 40)
        Me.cbgDept.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgDept.MyShowHeadrText = False
        Me.cbgDept.Name = "cbgDept"
        Me.cbgDept.Size = New System.Drawing.Size(145, 7)
        Me.cbgDept.TabIndex = 1
        Me.cbgDept.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.chkDeptSelect)
        Me.Panel7.Controls.Add(Me.chkDeptAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(145, 20)
        Me.Panel7.TabIndex = 0
        '
        'chkDeptSelect
        '
        Me.chkDeptSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkDeptSelect.Location = New System.Drawing.Point(65, 1)
        Me.chkDeptSelect.Name = "chkDeptSelect"
        Me.chkDeptSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkDeptSelect.TabIndex = 1
        Me.chkDeptSelect.Text = "Select"
        '
        'chkDeptAll
        '
        Me.chkDeptAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkDeptAll.Location = New System.Drawing.Point(22, 1)
        Me.chkDeptAll.Name = "chkDeptAll"
        Me.chkDeptAll.Size = New System.Drawing.Size(33, 18)
        Me.chkDeptAll.TabIndex = 0
        Me.chkDeptAll.Text = "All"
        '
        'gbEmployee
        '
        Me.gbEmployee.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbEmployee.Controls.Add(Me.cbgEmployee)
        Me.gbEmployee.Controls.Add(Me.Panel9)
        Me.gbEmployee.HeaderText = "Employee"
        Me.gbEmployee.Location = New System.Drawing.Point(3, 355)
        Me.gbEmployee.Name = "gbEmployee"
        Me.gbEmployee.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbEmployee.Size = New System.Drawing.Size(154, 58)
        Me.gbEmployee.TabIndex = 75
        Me.gbEmployee.Text = "Employee"
        Me.gbEmployee.Visible = False
        '
        'cbgEmployee
        '
        Me.cbgEmployee.CheckedValue = Nothing
        Me.cbgEmployee.DataSource = Nothing
        Me.cbgEmployee.DisplayMember = "Name"
        Me.cbgEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgEmployee.Location = New System.Drawing.Point(10, 40)
        Me.cbgEmployee.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgEmployee.MyShowHeadrText = False
        Me.cbgEmployee.Name = "cbgEmployee"
        Me.cbgEmployee.Size = New System.Drawing.Size(134, 8)
        Me.cbgEmployee.TabIndex = 1
        Me.cbgEmployee.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkEmpSelect)
        Me.Panel9.Controls.Add(Me.chkEmpAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(134, 20)
        Me.Panel9.TabIndex = 0
        '
        'chkEmpSelect
        '
        Me.chkEmpSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkEmpSelect.Location = New System.Drawing.Point(60, 1)
        Me.chkEmpSelect.Name = "chkEmpSelect"
        Me.chkEmpSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkEmpSelect.TabIndex = 1
        Me.chkEmpSelect.Text = "Select"
        '
        'chkEmpAll
        '
        Me.chkEmpAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkEmpAll.Location = New System.Drawing.Point(17, 1)
        Me.chkEmpAll.Name = "chkEmpAll"
        Me.chkEmpAll.Size = New System.Drawing.Size(33, 18)
        Me.chkEmpAll.TabIndex = 0
        Me.chkEmpAll.Text = "All"
        '
        'grpLocaSegment
        '
        Me.grpLocaSegment.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpLocaSegment.Controls.Add(Me.cbgLocSeg)
        Me.grpLocaSegment.Controls.Add(Me.Panel3)
        Me.grpLocaSegment.HeaderText = "Location Segment"
        Me.grpLocaSegment.Location = New System.Drawing.Point(3, 419)
        Me.grpLocaSegment.Name = "grpLocaSegment"
        Me.grpLocaSegment.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpLocaSegment.Size = New System.Drawing.Size(177, 58)
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
        Me.cbgLocSeg.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocSeg.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocSeg.MyShowHeadrText = False
        Me.cbgLocSeg.Name = "cbgLocSeg"
        Me.cbgLocSeg.Size = New System.Drawing.Size(157, 8)
        Me.cbgLocSeg.TabIndex = 1
        Me.cbgLocSeg.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnLocSegSelect)
        Me.Panel3.Controls.Add(Me.rbtnLocSegAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(157, 20)
        Me.Panel3.TabIndex = 0
        '
        'rbtnLocSegSelect
        '
        Me.rbtnLocSegSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocSegSelect.Location = New System.Drawing.Point(71, 1)
        Me.rbtnLocSegSelect.Name = "rbtnLocSegSelect"
        Me.rbtnLocSegSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnLocSegSelect.TabIndex = 1
        Me.rbtnLocSegSelect.Text = "Select"
        '
        'rbtnLocSegAll
        '
        Me.rbtnLocSegAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocSegAll.Location = New System.Drawing.Point(28, 1)
        Me.rbtnLocSegAll.Name = "rbtnLocSegAll"
        Me.rbtnLocSegAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnLocSegAll.TabIndex = 0
        Me.rbtnLocSegAll.Text = "All"
        '
        'gbVisi
        '
        Me.gbVisi.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVisi.Controls.Add(Me.cbgVisi)
        Me.gbVisi.Controls.Add(Me.Panel8)
        Me.gbVisi.HeaderText = "Visi/PMX"
        Me.gbVisi.Location = New System.Drawing.Point(3, 483)
        Me.gbVisi.Name = "gbVisi"
        Me.gbVisi.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbVisi.Size = New System.Drawing.Size(140, 50)
        Me.gbVisi.TabIndex = 76
        Me.gbVisi.Text = "Visi/PMX"
        Me.gbVisi.Visible = False
        '
        'cbgVisi
        '
        Me.cbgVisi.CheckedValue = Nothing
        Me.cbgVisi.DataSource = Nothing
        Me.cbgVisi.DisplayMember = "Name"
        Me.cbgVisi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVisi.Location = New System.Drawing.Point(10, 40)
        Me.cbgVisi.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVisi.MyShowHeadrText = False
        Me.cbgVisi.Name = "cbgVisi"
        Me.cbgVisi.Size = New System.Drawing.Size(120, 0)
        Me.cbgVisi.TabIndex = 1
        Me.cbgVisi.ValueMember = "Code"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chkVisiSelct)
        Me.Panel8.Controls.Add(Me.chkVisiAll)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 20)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(120, 20)
        Me.Panel8.TabIndex = 0
        '
        'chkVisiSelct
        '
        Me.chkVisiSelct.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkVisiSelct.Location = New System.Drawing.Point(53, 1)
        Me.chkVisiSelct.Name = "chkVisiSelct"
        Me.chkVisiSelct.Size = New System.Drawing.Size(50, 18)
        Me.chkVisiSelct.TabIndex = 1
        Me.chkVisiSelct.Text = "Select"
        '
        'chkVisiAll
        '
        Me.chkVisiAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkVisiAll.Location = New System.Drawing.Point(10, 1)
        Me.chkVisiAll.Name = "chkVisiAll"
        Me.chkVisiAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVisiAll.TabIndex = 0
        Me.chkVisiAll.Text = "All"
        '
        'grpCompany
        '
        Me.grpCompany.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCompany.Controls.Add(Me.Panel2)
        Me.grpCompany.HeaderText = "Company"
        Me.grpCompany.Location = New System.Drawing.Point(3, 539)
        Me.grpCompany.Name = "grpCompany"
        Me.grpCompany.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCompany.Size = New System.Drawing.Size(358, 51)
        Me.grpCompany.TabIndex = 5
        Me.grpCompany.Text = "Company"
        Me.grpCompany.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbtnSelectCompany)
        Me.Panel2.Controls.Add(Me.rbtnAllCompany)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(338, 18)
        Me.Panel2.TabIndex = 0
        '
        'rbtnSelectCompany
        '
        Me.rbtnSelectCompany.Location = New System.Drawing.Point(146, 1)
        Me.rbtnSelectCompany.Name = "rbtnSelectCompany"
        Me.rbtnSelectCompany.Size = New System.Drawing.Size(50, 18)
        Me.rbtnSelectCompany.TabIndex = 1
        Me.rbtnSelectCompany.Text = "Select"
        '
        'rbtnAllCompany
        '
        Me.rbtnAllCompany.Location = New System.Drawing.Point(103, 1)
        Me.rbtnAllCompany.Name = "rbtnAllCompany"
        Me.rbtnAllCompany.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAllCompany.TabIndex = 0
        Me.rbtnAllCompany.Text = "All"
        '
        'gbMachines
        '
        Me.gbMachines.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbMachines.Controls.Add(Me.cbgmachine)
        Me.gbMachines.Controls.Add(Me.Panel6)
        Me.gbMachines.HeaderText = "Machine"
        Me.gbMachines.Location = New System.Drawing.Point(3, 596)
        Me.gbMachines.Name = "gbMachines"
        Me.gbMachines.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbMachines.Size = New System.Drawing.Size(99, 61)
        Me.gbMachines.TabIndex = 73
        Me.gbMachines.Text = "Machine"
        Me.gbMachines.Visible = False
        '
        'cbgmachine
        '
        Me.cbgmachine.CheckedValue = Nothing
        Me.cbgmachine.DataSource = Nothing
        Me.cbgmachine.DisplayMember = "Name"
        Me.cbgmachine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgmachine.Location = New System.Drawing.Point(10, 40)
        Me.cbgmachine.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgmachine.MyShowHeadrText = False
        Me.cbgmachine.Name = "cbgmachine"
        Me.cbgmachine.Size = New System.Drawing.Size(79, 11)
        Me.cbgmachine.TabIndex = 1
        Me.cbgmachine.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkMachineSelect)
        Me.Panel6.Controls.Add(Me.chkMachineAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(79, 20)
        Me.Panel6.TabIndex = 0
        '
        'chkMachineSelect
        '
        Me.chkMachineSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkMachineSelect.Location = New System.Drawing.Point(32, 1)
        Me.chkMachineSelect.Name = "chkMachineSelect"
        Me.chkMachineSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkMachineSelect.TabIndex = 1
        Me.chkMachineSelect.Text = "Select"
        '
        'chkMachineAll
        '
        Me.chkMachineAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkMachineAll.Location = New System.Drawing.Point(-11, 1)
        Me.chkMachineAll.Name = "chkMachineAll"
        Me.chkMachineAll.Size = New System.Drawing.Size(33, 18)
        Me.chkMachineAll.TabIndex = 0
        Me.chkMachineAll.Text = "All"
        '
        'gbVehicle
        '
        Me.gbVehicle.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbVehicle.Controls.Add(Me.cbgVehicle)
        Me.gbVehicle.Controls.Add(Me.Panel5)
        Me.gbVehicle.HeaderText = "Vehicle"
        Me.gbVehicle.Location = New System.Drawing.Point(3, 663)
        Me.gbVehicle.Name = "gbVehicle"
        Me.gbVehicle.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbVehicle.Size = New System.Drawing.Size(182, 68)
        Me.gbVehicle.TabIndex = 72
        Me.gbVehicle.Text = "Vehicle"
        Me.gbVehicle.Visible = False
        '
        'cbgVehicle
        '
        Me.cbgVehicle.CheckedValue = Nothing
        Me.cbgVehicle.DataSource = Nothing
        Me.cbgVehicle.DisplayMember = "Name"
        Me.cbgVehicle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVehicle.Location = New System.Drawing.Point(10, 40)
        Me.cbgVehicle.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVehicle.MyShowHeadrText = False
        Me.cbgVehicle.Name = "cbgVehicle"
        Me.cbgVehicle.Size = New System.Drawing.Size(162, 18)
        Me.cbgVehicle.TabIndex = 1
        Me.cbgVehicle.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkVehicleSelect)
        Me.Panel5.Controls.Add(Me.chkVehicleAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(162, 20)
        Me.Panel5.TabIndex = 0
        '
        'chkVehicleSelect
        '
        Me.chkVehicleSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkVehicleSelect.Location = New System.Drawing.Point(74, 1)
        Me.chkVehicleSelect.Name = "chkVehicleSelect"
        Me.chkVehicleSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVehicleSelect.TabIndex = 1
        Me.chkVehicleSelect.Text = "Select"
        '
        'chkVehicleAll
        '
        Me.chkVehicleAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkVehicleAll.Location = New System.Drawing.Point(31, 1)
        Me.chkVehicleAll.Name = "chkVehicleAll"
        Me.chkVehicleAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVehicleAll.TabIndex = 0
        Me.chkVehicleAll.Text = "All"
        '
        'gbSourceCode
        '
        Me.gbSourceCode.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbSourceCode.Controls.Add(Me.cbgSourceCode)
        Me.gbSourceCode.Controls.Add(Me.Panel10)
        Me.gbSourceCode.HeaderText = "Source Code"
        Me.gbSourceCode.Location = New System.Drawing.Point(3, 737)
        Me.gbSourceCode.Name = "gbSourceCode"
        Me.gbSourceCode.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbSourceCode.Size = New System.Drawing.Size(51, 68)
        Me.gbSourceCode.TabIndex = 84
        Me.gbSourceCode.Text = "Source Code"
        Me.gbSourceCode.Visible = False
        '
        'cbgSourceCode
        '
        Me.cbgSourceCode.CheckedValue = Nothing
        Me.cbgSourceCode.DataSource = Nothing
        Me.cbgSourceCode.DisplayMember = "Name"
        Me.cbgSourceCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSourceCode.Location = New System.Drawing.Point(10, 40)
        Me.cbgSourceCode.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSourceCode.MyShowHeadrText = False
        Me.cbgSourceCode.Name = "cbgSourceCode"
        Me.cbgSourceCode.Size = New System.Drawing.Size(31, 18)
        Me.cbgSourceCode.TabIndex = 1
        Me.cbgSourceCode.ValueMember = "Code"
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.chkSrcCodeSelect)
        Me.Panel10.Controls.Add(Me.chkSrcCodeAll)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(10, 20)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(31, 20)
        Me.Panel10.TabIndex = 0
        '
        'chkSrcCodeSelect
        '
        Me.chkSrcCodeSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkSrcCodeSelect.Location = New System.Drawing.Point(8, 1)
        Me.chkSrcCodeSelect.Name = "chkSrcCodeSelect"
        Me.chkSrcCodeSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSrcCodeSelect.TabIndex = 1
        Me.chkSrcCodeSelect.Text = "Select"
        '
        'chkSrcCodeAll
        '
        Me.chkSrcCodeAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkSrcCodeAll.Location = New System.Drawing.Point(-35, 1)
        Me.chkSrcCodeAll.Name = "chkSrcCodeAll"
        Me.chkSrcCodeAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSrcCodeAll.TabIndex = 0
        Me.chkSrcCodeAll.Text = "All"
        '
        'chkExcludeTemplete
        '
        Me.chkExcludeTemplete.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExcludeTemplete.Location = New System.Drawing.Point(784, 51)
        Me.chkExcludeTemplete.MyLinkLable1 = Nothing
        Me.chkExcludeTemplete.MyLinkLable2 = Nothing
        Me.chkExcludeTemplete.Name = "chkExcludeTemplete"
        Me.chkExcludeTemplete.Size = New System.Drawing.Size(108, 18)
        Me.chkExcludeTemplete.TabIndex = 8
        Me.chkExcludeTemplete.Tag1 = Nothing
        Me.chkExcludeTemplete.Text = "Exclude Templete"
        Me.chkExcludeTemplete.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkExcludeTemplete.Visible = False
        '
        'chkMultipleRollup
        '
        Me.chkMultipleRollup.Location = New System.Drawing.Point(359, 51)
        Me.chkMultipleRollup.MyLinkLable1 = Nothing
        Me.chkMultipleRollup.MyLinkLable2 = Nothing
        Me.chkMultipleRollup.Name = "chkMultipleRollup"
        Me.chkMultipleRollup.Size = New System.Drawing.Size(96, 18)
        Me.chkMultipleRollup.TabIndex = 5
        Me.chkMultipleRollup.Tag1 = Nothing
        Me.chkMultipleRollup.Text = "Multiple Rollup"
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
        Me.txtFromDate.Location = New System.Drawing.Point(422, 27)
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
        Me.txtFromDate.TabIndex = 2
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "30/05/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
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
        Me.txtToDate.Location = New System.Drawing.Point(571, 27)
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
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "30/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(508, 28)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 78
        Me.lblToDate.Text = "To Date"
        '
        'lblFromdate
        '
        Me.lblFromdate.FieldName = Nothing
        Me.lblFromdate.Location = New System.Drawing.Point(359, 28)
        Me.lblFromdate.Name = "lblFromdate"
        Me.lblFromdate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromdate.TabIndex = 77
        Me.lblFromdate.Text = "From Date"
        '
        'chkRollupWise
        '
        Me.chkRollupWise.Location = New System.Drawing.Point(667, 51)
        Me.chkRollupWise.MyLinkLable1 = Nothing
        Me.chkRollupWise.MyLinkLable2 = Nothing
        Me.chkRollupWise.Name = "chkRollupWise"
        Me.chkRollupWise.Size = New System.Drawing.Size(111, 18)
        Me.chkRollupWise.TabIndex = 7
        Me.chkRollupWise.Tag1 = Nothing
        Me.chkRollupWise.Text = "Rollup Summarize"
        Me.chkRollupWise.Visible = False
        '
        'chkShowOPBal
        '
        Me.chkShowOPBal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowOPBal.Location = New System.Drawing.Point(525, 51)
        Me.chkShowOPBal.MyLinkLable1 = Nothing
        Me.chkShowOPBal.MyLinkLable2 = Nothing
        Me.chkShowOPBal.Name = "chkShowOPBal"
        Me.chkShowOPBal.Size = New System.Drawing.Size(135, 18)
        Me.chkShowOPBal.TabIndex = 6
        Me.chkShowOPBal.Tag1 = Nothing
        Me.chkShowOPBal.Text = "Show Opening Balance"
        Me.chkShowOPBal.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkShowOPBal.Visible = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(9, 27)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(67, 18)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "Report Type"
        '
        'cbgSrcCode
        '
        Me.cbgSrcCode.AutoCompleteDisplayMember = Nothing
        Me.cbgSrcCode.AutoCompleteValueMember = Nothing
        Me.cbgSrcCode.CalculationExpression = Nothing
        Me.cbgSrcCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cbgSrcCode.FieldCode = Nothing
        Me.cbgSrcCode.FieldDesc = Nothing
        Me.cbgSrcCode.FieldMaxLength = 0
        Me.cbgSrcCode.FieldName = Nothing
        Me.cbgSrcCode.isCalculatedField = False
        Me.cbgSrcCode.IsSourceFromTable = False
        Me.cbgSrcCode.IsSourceFromValueList = False
        Me.cbgSrcCode.IsUnique = False
        RadListDataItem1.Text = "Trial Balance"
        RadListDataItem2.Text = "Subledger Trial Balance"
        RadListDataItem3.Text = "Period Trial Balance"
        RadListDataItem4.Text = "Basic Trial Balance"
        RadListDataItem5.Text = "Location wise"
        RadListDataItem6.Text = "Account Wise"
        Me.cbgSrcCode.Items.Add(RadListDataItem1)
        Me.cbgSrcCode.Items.Add(RadListDataItem2)
        Me.cbgSrcCode.Items.Add(RadListDataItem3)
        Me.cbgSrcCode.Items.Add(RadListDataItem4)
        Me.cbgSrcCode.Items.Add(RadListDataItem5)
        Me.cbgSrcCode.Items.Add(RadListDataItem6)
        Me.cbgSrcCode.Location = New System.Drawing.Point(118, 25)
        Me.cbgSrcCode.MendatroryField = False
        Me.cbgSrcCode.MyLinkLable1 = Me.RadLabel3
        Me.cbgSrcCode.MyLinkLable2 = Nothing
        Me.cbgSrcCode.Name = "cbgSrcCode"
        Me.cbgSrcCode.ReferenceFieldDesc = Nothing
        Me.cbgSrcCode.ReferenceFieldName = Nothing
        Me.cbgSrcCode.ReferenceTableName = Nothing
        Me.cbgSrcCode.Size = New System.Drawing.Size(231, 20)
        Me.cbgSrcCode.TabIndex = 1
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(956, 330)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.gv1.Size = New System.Drawing.Size(956, 330)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnQExport)
        Me.Panel1.Controls.Add(Me.btnChangeOrder)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 378)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(977, 23)
        Me.Panel1.TabIndex = 1
        '
        'btnQExport
        '
        Me.btnQExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.QExpExcel, Me.QExpCSV, Me.PDF})
        Me.btnQExport.Location = New System.Drawing.Point(338, 3)
        Me.btnQExport.Name = "btnQExport"
        Me.btnQExport.Size = New System.Drawing.Size(103, 18)
        Me.btnQExport.TabIndex = 160
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
        'btnChangeOrder
        '
        Me.btnChangeOrder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeOrder.Location = New System.Drawing.Point(228, 3)
        Me.btnChangeOrder.Name = "btnChangeOrder"
        Me.btnChangeOrder.Size = New System.Drawing.Size(105, 18)
        Me.btnChangeOrder.TabIndex = 39
        Me.btnChangeOrder.Text = "Change Print Order"
        Me.btnChangeOrder.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(6, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = ">>>"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(154, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(80, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(903, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = " Close"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(977, 20)
        Me.rdmenufile.TabIndex = 72
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
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
        'chkIncludeUnusedAccount
        '
        Me.chkIncludeUnusedAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeUnusedAccount.Location = New System.Drawing.Point(359, 160)
        Me.chkIncludeUnusedAccount.Name = "chkIncludeUnusedAccount"
        '
        '
        '
        Me.chkIncludeUnusedAccount.RootElement.StretchHorizontally = True
        Me.chkIncludeUnusedAccount.RootElement.StretchVertically = True
        Me.chkIncludeUnusedAccount.Size = New System.Drawing.Size(240, 16)
        Me.chkIncludeUnusedAccount.TabIndex = 367
        Me.chkIncludeUnusedAccount.Text = "Include Unused Account"
        '
        'frmRptTrialBalanceNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(977, 401)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmRptTrialBalanceNew"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Trial Balance"
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.chkIncludeYearEndEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowNetBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCusVendWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeingAdjustmentEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblAccount.ResumeLayout(False)
        Me.lblAccount.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSourceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVISIPMX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        CType(Me.gbAcc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAcc.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkAccSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAccAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbDept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDept.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.chkDeptSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDeptAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEmployee.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.chkEmpSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkEmpAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpLocaSegment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLocaSegment.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnLocSegSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLocSegAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbVisi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVisi.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.chkVisiSelct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVisiAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCompany.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbMachines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMachines.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkMachineSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMachineAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVehicle.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkVehicleSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVehicleAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbSourceCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSourceCode.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.chkSrcCodeSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSrcCodeAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcludeTemplete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMultipleRollup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRollupWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowOPBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgSrcCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnQExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnChangeOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeUnusedAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkShowOPBal As common.Controls.MyCheckBox
    Friend WithEvents cbgSrcCode As common.Controls.MyComboBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents chkRollupWise As common.Controls.MyCheckBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnChangeOrder As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromdate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkMultipleRollup As common.Controls.MyCheckBox
    Friend WithEvents chkExcludeTemplete As common.Controls.MyCheckBox
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents gbAcc As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgAccount As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkAccSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkAccAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbDept As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgDept As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents chkDeptSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDeptAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbVisi As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVisi As common.MyCheckBoxGrid
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkVisiSelct As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkVisiAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents grpCompany As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbtnSelectCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnAllCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbMachines As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgmachine As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkMachineSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkMachineAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbEmployee As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgEmployee As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkEmpSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkEmpAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents grpLocaSegment As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocSeg As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbtnLocSegSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnLocSegAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbVehicle As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVehicle As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkVehicleSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkVehicleAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents gbSourceCode As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSourceCode As common.MyCheckBoxGrid
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents chkSrcCodeSelect As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkSrcCodeAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtAccount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtLocationSegmant As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtCompany As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtSourceCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents gvDB As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents txtDepartment As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtVISIPMX As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMachine As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtVehicle As common.UserControls.txtMultiSelectFinder
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblCompany As common.Controls.MyLabel
    Friend WithEvents lblLocationSegment As common.Controls.MyLabel
    Friend WithEvents lblAccount As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblEmployee As common.Controls.MyLabel
    Friend WithEvents lblSourceCode As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents lblVISIPMX As common.Controls.MyLabel
    Friend WithEvents lblMachine As common.Controls.MyLabel
    Friend WithEvents lblVehicle As common.Controls.MyLabel
    Friend WithEvents txtMainGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkIncludeingClosingEntry As common.Controls.MyCheckBox
    Friend WithEvents chkIncludeingAdjustmentEntry As common.Controls.MyCheckBox
    Friend WithEvents txtACGrpType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkCusVendWiseSummary As common.Controls.MyCheckBox
    Friend WithEvents chkShowNetBalance As common.Controls.MyCheckBox
    Friend WithEvents txtFiscalYear As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents chkIndAS As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnQExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents QExpExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents QExpCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkIncludeYearEndEntry As common.Controls.MyCheckBox
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkIncludeUnusedAccount As Telerik.WinControls.UI.RadCheckBox
End Class

