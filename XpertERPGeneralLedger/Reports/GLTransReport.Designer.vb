<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GLTransReport
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
        Me.txtAccountGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.chkIncludeYearEndEntry = New common.Controls.MyCheckBox()
        Me.chkIndAS = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludeingClosingEntry = New common.Controls.MyCheckBox()
        Me.chkIncludeingAdjustmentEntry = New common.Controls.MyCheckBox()
        Me.chkCusVendWiseSummary = New common.Controls.MyCheckBox()
        Me.chkWithoutOpening = New Telerik.WinControls.UI.RadCheckBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlLocSeg = New System.Windows.Forms.Panel()
        Me.txtLocationSeg = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.pnlAccount = New System.Windows.Forms.Panel()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtAccount = New common.UserControls.txtMultiSelectFinder()
        Me.pnlVehicle = New System.Windows.Forms.Panel()
        Me.txtVehicle = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.pnlMachine = New System.Windows.Forms.Panel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMachine = New common.UserControls.txtMultiSelectFinder()
        Me.pnlDepartment = New System.Windows.Forms.Panel()
        Me.txtDepartment = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.pnlEmployee = New System.Windows.Forms.Panel()
        Me.txtEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.pnlVisiPMX = New System.Windows.Forms.Panel()
        Me.txtVisiPMX = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.pnlSourceCode = New System.Windows.Forms.Panel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtSourceCode = New common.UserControls.txtMultiSelectFinder()
        Me.pnlAccountgrp = New System.Windows.Forms.Panel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtACGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.chkSettlementSheetClearance = New common.Controls.MyCheckBox()
        Me.chkExcludeTemplete = New common.Controls.MyCheckBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.ddlGroupingType = New Telerik.WinControls.UI.RadDropDownList()
        Me.chkMonthWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.radbtnBulkExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkExportCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkExportXls = New Telerik.WinControls.UI.RadMenuItem()
        Me.pnlAdminSetting = New System.Windows.Forms.Panel()
        Me.pnlSaleRegister = New System.Windows.Forms.Panel()
        Me.rbtnVAT = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnCST = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkMismatched = New common.Controls.MyCheckBox()
        Me.pnlPurchaseBook = New System.Windows.Forms.Panel()
        Me.rbtnFinishGoods = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnOtherGoods = New Telerik.WinControls.UI.RadRadioButton()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbtnTPT = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnSaleAccount = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnHCessAccount = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnExciseAccount = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnECessAccount = New Telerik.WinControls.UI.RadRadioButton()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.rbtnSaleRegister = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnPurchaseBook = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnSaleRecoChart = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnNone = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnBank = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnVendor = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnCustomer = New Telerik.WinControls.UI.RadRadioButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.chkIncludeYearEndEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeingAdjustmentEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCusVendWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkWithoutOpening, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.pnlLocSeg.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAccount.SuspendLayout()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlVehicle.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMachine.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDepartment.SuspendLayout()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEmployee.SuspendLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlVisiPMX.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSourceCode.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAccountgrp.SuspendLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSettlementSheetClearance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcludeTemplete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlGroupingType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMonthWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdminSetting.SuspendLayout()
        Me.pnlSaleRegister.SuspendLayout()
        CType(Me.rbtnVAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMismatched, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPurchaseBook.SuspendLayout()
        CType(Me.rbtnFinishGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnOtherGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnTPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSaleAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnHCessAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnExciseAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnECessAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        CType(Me.rbtnSaleRegister, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnPurchaseBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSaleRecoChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnNone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtAccountGroup
        '
        Me.txtAccountGroup.arrDispalyMember = Nothing
        Me.txtAccountGroup.arrValueMember = Nothing
        Me.txtAccountGroup.Location = New System.Drawing.Point(112, 2)
        Me.txtAccountGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountGroup.MyLinkLable1 = Me.MyLabel7
        Me.txtAccountGroup.MyLinkLable2 = Nothing
        Me.txtAccountGroup.MyNullText = "All"
        Me.txtAccountGroup.Name = "txtAccountGroup"
        Me.txtAccountGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtAccountGroup.TabIndex = 393
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel7.TabIndex = 394
        Me.MyLabel7.Text = "Account Group"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.radbtnBulkExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlAdminSetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(1035, 494)
        Me.SplitContainer1.SplitterDistance = 454
        Me.SplitContainer1.TabIndex = 61
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1035, 434)
        Me.RadPageView1.TabIndex = 5
        Me.RadPageView1.Text = "RadPageView1"
        Me.RadPageView1.ThemeName = "ControlDefault"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1014, 386)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.chkIncludeYearEndEntry)
        Me.RadPanel1.Controls.Add(Me.chkIndAS)
        Me.RadPanel1.Controls.Add(Me.chkIncludeingClosingEntry)
        Me.RadPanel1.Controls.Add(Me.chkIncludeingAdjustmentEntry)
        Me.RadPanel1.Controls.Add(Me.chkCusVendWiseSummary)
        Me.RadPanel1.Controls.Add(Me.chkWithoutOpening)
        Me.RadPanel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.RadPanel1.Controls.Add(Me.MyLabel2)
        Me.RadPanel1.Controls.Add(Me.txtToDate)
        Me.RadPanel1.Controls.Add(Me.chkSettlementSheetClearance)
        Me.RadPanel1.Controls.Add(Me.chkExcludeTemplete)
        Me.RadPanel1.Controls.Add(Me.MyLabel1)
        Me.RadPanel1.Controls.Add(Me.ddlGroupingType)
        Me.RadPanel1.Controls.Add(Me.chkMonthWise)
        Me.RadPanel1.Controls.Add(Me.RadLabel7)
        Me.RadPanel1.Controls.Add(Me.txtFromDate)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1014, 386)
        Me.RadPanel1.TabIndex = 68
        '
        'chkIncludeYearEndEntry
        '
        Me.chkIncludeYearEndEntry.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeYearEndEntry.Location = New System.Drawing.Point(792, 76)
        Me.chkIncludeYearEndEntry.MyLinkLable1 = Nothing
        Me.chkIncludeYearEndEntry.MyLinkLable2 = Nothing
        Me.chkIncludeYearEndEntry.Name = "chkIncludeYearEndEntry"
        Me.chkIncludeYearEndEntry.Size = New System.Drawing.Size(142, 18)
        Me.chkIncludeYearEndEntry.TabIndex = 405
        Me.chkIncludeYearEndEntry.Tag1 = Nothing
        Me.chkIncludeYearEndEntry.Text = "Including Year End Entry"
        Me.chkIncludeYearEndEntry.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkIndAS
        '
        Me.chkIndAS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIndAS.Location = New System.Drawing.Point(495, 100)
        Me.chkIndAS.Name = "chkIndAS"
        '
        '
        '
        Me.chkIndAS.RootElement.StretchHorizontally = True
        Me.chkIndAS.RootElement.StretchVertically = True
        Me.chkIndAS.Size = New System.Drawing.Size(89, 16)
        Me.chkIndAS.TabIndex = 404
        Me.chkIndAS.Text = "Ind As"
        '
        'chkIncludeingClosingEntry
        '
        Me.chkIncludeingClosingEntry.Location = New System.Drawing.Point(651, 76)
        Me.chkIncludeingClosingEntry.MyLinkLable1 = Nothing
        Me.chkIncludeingClosingEntry.MyLinkLable2 = Nothing
        Me.chkIncludeingClosingEntry.Name = "chkIncludeingClosingEntry"
        Me.chkIncludeingClosingEntry.Size = New System.Drawing.Size(135, 18)
        Me.chkIncludeingClosingEntry.TabIndex = 363
        Me.chkIncludeingClosingEntry.Tag1 = Nothing
        Me.chkIncludeingClosingEntry.Text = "Including Closing Entry"
        '
        'chkIncludeingAdjustmentEntry
        '
        Me.chkIncludeingAdjustmentEntry.Location = New System.Drawing.Point(495, 76)
        Me.chkIncludeingAdjustmentEntry.MyLinkLable1 = Nothing
        Me.chkIncludeingAdjustmentEntry.MyLinkLable2 = Nothing
        Me.chkIncludeingAdjustmentEntry.Name = "chkIncludeingAdjustmentEntry"
        Me.chkIncludeingAdjustmentEntry.Size = New System.Drawing.Size(156, 18)
        Me.chkIncludeingAdjustmentEntry.TabIndex = 362
        Me.chkIncludeingAdjustmentEntry.Tag1 = Nothing
        Me.chkIncludeingAdjustmentEntry.Text = "Including Adjustment Entry"
        '
        'chkCusVendWiseSummary
        '
        Me.chkCusVendWiseSummary.Location = New System.Drawing.Point(495, 52)
        Me.chkCusVendWiseSummary.MyLinkLable1 = Nothing
        Me.chkCusVendWiseSummary.MyLinkLable2 = Nothing
        Me.chkCusVendWiseSummary.Name = "chkCusVendWiseSummary"
        Me.chkCusVendWiseSummary.Size = New System.Drawing.Size(282, 18)
        Me.chkCusVendWiseSummary.TabIndex = 361
        Me.chkCusVendWiseSummary.Tag1 = Nothing
        Me.chkCusVendWiseSummary.Text = "Show Customer/Vendor wise Summary on Drilldown"
        Me.chkCusVendWiseSummary.Visible = False
        '
        'chkWithoutOpening
        '
        Me.chkWithoutOpening.Location = New System.Drawing.Point(495, 28)
        Me.chkWithoutOpening.Name = "chkWithoutOpening"
        Me.chkWithoutOpening.Size = New System.Drawing.Size(107, 18)
        Me.chkWithoutOpening.TabIndex = 312
        Me.chkWithoutOpening.Text = "Without Opening"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlLocSeg)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlAccount)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlVehicle)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlMachine)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlDepartment)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlEmployee)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlVisiPMX)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlSourceCode)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlAccountgrp)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(13, 27)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(471, 266)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'pnlLocSeg
        '
        Me.pnlLocSeg.Controls.Add(Me.txtLocationSeg)
        Me.pnlLocSeg.Controls.Add(Me.lblLocation)
        Me.pnlLocSeg.Location = New System.Drawing.Point(3, 3)
        Me.pnlLocSeg.Name = "pnlLocSeg"
        Me.pnlLocSeg.Size = New System.Drawing.Size(464, 25)
        Me.pnlLocSeg.TabIndex = 399
        '
        'txtLocationSeg
        '
        Me.txtLocationSeg.arrDispalyMember = Nothing
        Me.txtLocationSeg.arrValueMember = Nothing
        Me.txtLocationSeg.Location = New System.Drawing.Point(112, 4)
        Me.txtLocationSeg.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationSeg.MyLinkLable1 = Me.lblLocation
        Me.txtLocationSeg.MyLinkLable2 = Nothing
        Me.txtLocationSeg.MyNullText = "All"
        Me.txtLocationSeg.Name = "txtLocationSeg"
        Me.txtLocationSeg.Size = New System.Drawing.Size(344, 19)
        Me.txtLocationSeg.TabIndex = 373
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(2, 4)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(96, 18)
        Me.lblLocation.TabIndex = 374
        Me.lblLocation.Text = "Location Segment"
        '
        'pnlAccount
        '
        Me.pnlAccount.Controls.Add(Me.lblCustomerGroup)
        Me.pnlAccount.Controls.Add(Me.txtAccount)
        Me.pnlAccount.Location = New System.Drawing.Point(3, 34)
        Me.pnlAccount.Name = "pnlAccount"
        Me.pnlAccount.Size = New System.Drawing.Size(464, 23)
        Me.pnlAccount.TabIndex = 0
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(2, 3)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(47, 18)
        Me.lblCustomerGroup.TabIndex = 384
        Me.lblCustomerGroup.Text = "Account"
        '
        'txtAccount
        '
        Me.txtAccount.arrDispalyMember = Nothing
        Me.txtAccount.arrValueMember = Nothing
        Me.txtAccount.Location = New System.Drawing.Point(112, 2)
        Me.txtAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccount.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtAccount.MyLinkLable2 = Nothing
        Me.txtAccount.MyNullText = "All"
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(344, 19)
        Me.txtAccount.TabIndex = 383
        '
        'pnlVehicle
        '
        Me.pnlVehicle.Controls.Add(Me.txtVehicle)
        Me.pnlVehicle.Controls.Add(Me.MyLabel4)
        Me.pnlVehicle.Location = New System.Drawing.Point(3, 63)
        Me.pnlVehicle.Name = "pnlVehicle"
        Me.pnlVehicle.Size = New System.Drawing.Size(464, 23)
        Me.pnlVehicle.TabIndex = 400
        '
        'txtVehicle
        '
        Me.txtVehicle.arrDispalyMember = Nothing
        Me.txtVehicle.arrValueMember = Nothing
        Me.txtVehicle.Location = New System.Drawing.Point(112, 2)
        Me.txtVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.MyLinkLable1 = Me.MyLabel4
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.MyNullText = "All"
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.Size = New System.Drawing.Size(344, 19)
        Me.txtVehicle.TabIndex = 387
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel4.TabIndex = 388
        Me.MyLabel4.Text = "Vehicle"
        '
        'pnlMachine
        '
        Me.pnlMachine.Controls.Add(Me.MyLabel3)
        Me.pnlMachine.Controls.Add(Me.txtMachine)
        Me.pnlMachine.Location = New System.Drawing.Point(3, 92)
        Me.pnlMachine.Name = "pnlMachine"
        Me.pnlMachine.Size = New System.Drawing.Size(464, 23)
        Me.pnlMachine.TabIndex = 401
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel3.TabIndex = 390
        Me.MyLabel3.Text = "Machine"
        '
        'txtMachine
        '
        Me.txtMachine.arrDispalyMember = Nothing
        Me.txtMachine.arrValueMember = Nothing
        Me.txtMachine.Location = New System.Drawing.Point(112, 2)
        Me.txtMachine.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachine.MyLinkLable1 = Me.MyLabel3
        Me.txtMachine.MyLinkLable2 = Nothing
        Me.txtMachine.MyNullText = "All"
        Me.txtMachine.Name = "txtMachine"
        Me.txtMachine.Size = New System.Drawing.Size(344, 19)
        Me.txtMachine.TabIndex = 389
        '
        'pnlDepartment
        '
        Me.pnlDepartment.Controls.Add(Me.txtDepartment)
        Me.pnlDepartment.Controls.Add(Me.lblCustomer)
        Me.pnlDepartment.Location = New System.Drawing.Point(3, 121)
        Me.pnlDepartment.Name = "pnlDepartment"
        Me.pnlDepartment.Size = New System.Drawing.Size(464, 23)
        Me.pnlDepartment.TabIndex = 402
        '
        'txtDepartment
        '
        Me.txtDepartment.arrDispalyMember = Nothing
        Me.txtDepartment.arrValueMember = Nothing
        Me.txtDepartment.Location = New System.Drawing.Point(112, 2)
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Me.lblCustomer
        Me.txtDepartment.MyLinkLable2 = Nothing
        Me.txtDepartment.MyNullText = "All"
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(344, 19)
        Me.txtDepartment.TabIndex = 385
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(2, 3)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(66, 18)
        Me.lblCustomer.TabIndex = 386
        Me.lblCustomer.Text = "Department"
        '
        'pnlEmployee
        '
        Me.pnlEmployee.Controls.Add(Me.txtEmployee)
        Me.pnlEmployee.Controls.Add(Me.MyLabel8)
        Me.pnlEmployee.Location = New System.Drawing.Point(3, 150)
        Me.pnlEmployee.Name = "pnlEmployee"
        Me.pnlEmployee.Size = New System.Drawing.Size(464, 23)
        Me.pnlEmployee.TabIndex = 403
        '
        'txtEmployee
        '
        Me.txtEmployee.arrDispalyMember = Nothing
        Me.txtEmployee.arrValueMember = Nothing
        Me.txtEmployee.Location = New System.Drawing.Point(112, 2)
        Me.txtEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployee.MyLinkLable1 = Me.MyLabel8
        Me.txtEmployee.MyLinkLable2 = Nothing
        Me.txtEmployee.MyNullText = "All"
        Me.txtEmployee.Name = "txtEmployee"
        Me.txtEmployee.Size = New System.Drawing.Size(344, 19)
        Me.txtEmployee.TabIndex = 391
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel8.TabIndex = 392
        Me.MyLabel8.Text = "Employee"
        '
        'pnlVisiPMX
        '
        Me.pnlVisiPMX.Controls.Add(Me.txtVisiPMX)
        Me.pnlVisiPMX.Controls.Add(Me.MyLabel6)
        Me.pnlVisiPMX.Location = New System.Drawing.Point(3, 179)
        Me.pnlVisiPMX.Name = "pnlVisiPMX"
        Me.pnlVisiPMX.Size = New System.Drawing.Size(464, 23)
        Me.pnlVisiPMX.TabIndex = 404
        '
        'txtVisiPMX
        '
        Me.txtVisiPMX.arrDispalyMember = Nothing
        Me.txtVisiPMX.arrValueMember = Nothing
        Me.txtVisiPMX.Location = New System.Drawing.Point(112, 2)
        Me.txtVisiPMX.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVisiPMX.MyLinkLable1 = Me.MyLabel6
        Me.txtVisiPMX.MyLinkLable2 = Nothing
        Me.txtVisiPMX.MyNullText = "All"
        Me.txtVisiPMX.Name = "txtVisiPMX"
        Me.txtVisiPMX.Size = New System.Drawing.Size(344, 19)
        Me.txtVisiPMX.TabIndex = 395
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(51, 18)
        Me.MyLabel6.TabIndex = 396
        Me.MyLabel6.Text = "Visi/PMX"
        '
        'pnlSourceCode
        '
        Me.pnlSourceCode.Controls.Add(Me.MyLabel5)
        Me.pnlSourceCode.Controls.Add(Me.txtSourceCode)
        Me.pnlSourceCode.Location = New System.Drawing.Point(3, 208)
        Me.pnlSourceCode.Name = "pnlSourceCode"
        Me.pnlSourceCode.Size = New System.Drawing.Size(464, 23)
        Me.pnlSourceCode.TabIndex = 405
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel5.TabIndex = 398
        Me.MyLabel5.Text = "Source Code"
        '
        'txtSourceCode
        '
        Me.txtSourceCode.arrDispalyMember = Nothing
        Me.txtSourceCode.arrValueMember = Nothing
        Me.txtSourceCode.Location = New System.Drawing.Point(112, 2)
        Me.txtSourceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSourceCode.MyLinkLable1 = Me.MyLabel5
        Me.txtSourceCode.MyLinkLable2 = Nothing
        Me.txtSourceCode.MyNullText = "All"
        Me.txtSourceCode.Name = "txtSourceCode"
        Me.txtSourceCode.Size = New System.Drawing.Size(344, 19)
        Me.txtSourceCode.TabIndex = 397
        '
        'pnlAccountgrp
        '
        Me.pnlAccountgrp.Controls.Add(Me.MyLabel9)
        Me.pnlAccountgrp.Controls.Add(Me.txtACGroup)
        Me.pnlAccountgrp.Location = New System.Drawing.Point(3, 237)
        Me.pnlAccountgrp.Name = "pnlAccountgrp"
        Me.pnlAccountgrp.Size = New System.Drawing.Size(464, 23)
        Me.pnlAccountgrp.TabIndex = 406
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(2, 3)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel9.TabIndex = 398
        Me.MyLabel9.Text = "Account Group"
        '
        'txtACGroup
        '
        Me.txtACGroup.arrDispalyMember = Nothing
        Me.txtACGroup.arrValueMember = Nothing
        Me.txtACGroup.Location = New System.Drawing.Point(112, 2)
        Me.txtACGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtACGroup.MyLinkLable1 = Me.MyLabel9
        Me.txtACGroup.MyLinkLable2 = Nothing
        Me.txtACGroup.MyNullText = "All"
        Me.txtACGroup.Name = "txtACGroup"
        Me.txtACGroup.Size = New System.Drawing.Size(344, 19)
        Me.txtACGroup.TabIndex = 397
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(299, 4)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel2.TabIndex = 310
        Me.MyLabel2.Text = "To Date"
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
        Me.txtToDate.Location = New System.Drawing.Point(357, 4)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(115, 20)
        Me.txtToDate.TabIndex = 311
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "30/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'chkSettlementSheetClearance
        '
        Me.chkSettlementSheetClearance.Location = New System.Drawing.Point(536, 187)
        Me.chkSettlementSheetClearance.MyLinkLable1 = Nothing
        Me.chkSettlementSheetClearance.MyLinkLable2 = Nothing
        Me.chkSettlementSheetClearance.Name = "chkSettlementSheetClearance"
        Me.chkSettlementSheetClearance.Size = New System.Drawing.Size(179, 18)
        Me.chkSettlementSheetClearance.TabIndex = 309
        Me.chkSettlementSheetClearance.Tag1 = Nothing
        Me.chkSettlementSheetClearance.Text = "Settlement Sheet Clearance A/C"
        Me.chkSettlementSheetClearance.Visible = False
        '
        'chkExcludeTemplete
        '
        Me.chkExcludeTemplete.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExcludeTemplete.Location = New System.Drawing.Point(536, 163)
        Me.chkExcludeTemplete.MyLinkLable1 = Nothing
        Me.chkExcludeTemplete.MyLinkLable2 = Nothing
        Me.chkExcludeTemplete.Name = "chkExcludeTemplete"
        Me.chkExcludeTemplete.Size = New System.Drawing.Size(108, 18)
        Me.chkExcludeTemplete.TabIndex = 86
        Me.chkExcludeTemplete.Tag1 = Nothing
        Me.chkExcludeTemplete.Text = "Exclude Templete"
        Me.chkExcludeTemplete.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkExcludeTemplete.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(495, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(81, 18)
        Me.MyLabel1.TabIndex = 63
        Me.MyLabel1.Text = "Grouping Type"
        '
        'ddlGroupingType
        '
        Me.ddlGroupingType.AutoCompleteDisplayMember = Nothing
        Me.ddlGroupingType.AutoCompleteValueMember = Nothing
        Me.ddlGroupingType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlGroupingType.Location = New System.Drawing.Point(582, 4)
        Me.ddlGroupingType.Name = "ddlGroupingType"
        Me.ddlGroupingType.Size = New System.Drawing.Size(227, 20)
        Me.ddlGroupingType.TabIndex = 308
        '
        'chkMonthWise
        '
        Me.chkMonthWise.Location = New System.Drawing.Point(536, 139)
        Me.chkMonthWise.Name = "chkMonthWise"
        Me.chkMonthWise.Size = New System.Drawing.Size(118, 18)
        Me.chkMonthWise.TabIndex = 75
        Me.chkMonthWise.Text = "Month Wise Report"
        Me.chkMonthWise.Visible = False
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Location = New System.Drawing.Point(17, 4)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel7.TabIndex = 62
        Me.RadLabel7.Text = "From Date"
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
        Me.txtFromDate.Location = New System.Drawing.Point(127, 4)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(118, 20)
        Me.txtFromDate.TabIndex = 63
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "30/05/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1014, 386)
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
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1014, 386)
        Me.gv1.TabIndex = 5
        Me.gv1.Text = "gv"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1035, 20)
        Me.RadMenu1.TabIndex = 16
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'radbtnBulkExp
        '
        Me.radbtnBulkExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.radbtnBulkExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiCSV, Me.rmiPDF, Me.BulkExportCSV, Me.BulkExportXls})
        Me.radbtnBulkExp.Location = New System.Drawing.Point(217, 4)
        Me.radbtnBulkExp.Name = "radbtnBulkExp"
        Me.radbtnBulkExp.Size = New System.Drawing.Size(100, 27)
        Me.radbtnBulkExp.TabIndex = 312
        Me.radbtnBulkExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiCSV
        '
        Me.rmiCSV.AccessibleDescription = "CSV"
        Me.rmiCSV.AccessibleName = "CSV"
        Me.rmiCSV.Name = "rmiCSV"
        Me.rmiCSV.Text = "CSV"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'BulkExportCSV
        '
        Me.BulkExportCSV.AccessibleDescription = "CSV"
        Me.BulkExportCSV.AccessibleName = "CSV"
        Me.BulkExportCSV.Name = "BulkExportCSV"
        Me.BulkExportCSV.Text = "Bulk CSV"
        '
        'BulkExportXls
        '
        Me.BulkExportXls.AccessibleDescription = "Excel"
        Me.BulkExportXls.AccessibleName = "Excel"
        Me.BulkExportXls.Name = "BulkExportXls"
        Me.BulkExportXls.Text = "Bulk Excel"
        '
        'pnlAdminSetting
        '
        Me.pnlAdminSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAdminSetting.Controls.Add(Me.pnlSaleRegister)
        Me.pnlAdminSetting.Controls.Add(Me.chkMismatched)
        Me.pnlAdminSetting.Controls.Add(Me.pnlPurchaseBook)
        Me.pnlAdminSetting.Controls.Add(Me.Panel4)
        Me.pnlAdminSetting.Controls.Add(Me.Panel12)
        Me.pnlAdminSetting.Location = New System.Drawing.Point(509, 4)
        Me.pnlAdminSetting.Name = "pnlAdminSetting"
        Me.pnlAdminSetting.Size = New System.Drawing.Size(435, 47)
        Me.pnlAdminSetting.TabIndex = 310
        Me.pnlAdminSetting.Visible = False
        '
        'pnlSaleRegister
        '
        Me.pnlSaleRegister.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSaleRegister.Controls.Add(Me.rbtnVAT)
        Me.pnlSaleRegister.Controls.Add(Me.rbtnCST)
        Me.pnlSaleRegister.Location = New System.Drawing.Point(4, 23)
        Me.pnlSaleRegister.Name = "pnlSaleRegister"
        Me.pnlSaleRegister.Size = New System.Drawing.Size(103, 21)
        Me.pnlSaleRegister.TabIndex = 312
        Me.pnlSaleRegister.Visible = False
        '
        'rbtnVAT
        '
        Me.rbtnVAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnVAT.Location = New System.Drawing.Point(6, 0)
        Me.rbtnVAT.Name = "rbtnVAT"
        Me.rbtnVAT.Size = New System.Drawing.Size(40, 18)
        Me.rbtnVAT.TabIndex = 0
        Me.rbtnVAT.Text = "VAT"
        Me.rbtnVAT.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnCST
        '
        Me.rbtnCST.Location = New System.Drawing.Point(55, 0)
        Me.rbtnCST.Name = "rbtnCST"
        Me.rbtnCST.Size = New System.Drawing.Size(39, 18)
        Me.rbtnCST.TabIndex = 1
        Me.rbtnCST.Text = "CST"
        '
        'chkMismatched
        '
        Me.chkMismatched.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMismatched.Location = New System.Drawing.Point(480, 1)
        Me.chkMismatched.MyLinkLable1 = Nothing
        Me.chkMismatched.MyLinkLable2 = Nothing
        Me.chkMismatched.Name = "chkMismatched"
        Me.chkMismatched.Size = New System.Drawing.Size(81, 18)
        Me.chkMismatched.TabIndex = 312
        Me.chkMismatched.Tag1 = Nothing
        Me.chkMismatched.Text = "Mismatched"
        Me.chkMismatched.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'pnlPurchaseBook
        '
        Me.pnlPurchaseBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPurchaseBook.Controls.Add(Me.rbtnFinishGoods)
        Me.pnlPurchaseBook.Controls.Add(Me.rbtnOtherGoods)
        Me.pnlPurchaseBook.Location = New System.Drawing.Point(4, 23)
        Me.pnlPurchaseBook.Name = "pnlPurchaseBook"
        Me.pnlPurchaseBook.Size = New System.Drawing.Size(206, 21)
        Me.pnlPurchaseBook.TabIndex = 311
        Me.pnlPurchaseBook.Visible = False
        '
        'rbtnFinishGoods
        '
        Me.rbtnFinishGoods.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnFinishGoods.Location = New System.Drawing.Point(6, 0)
        Me.rbtnFinishGoods.Name = "rbtnFinishGoods"
        Me.rbtnFinishGoods.Size = New System.Drawing.Size(84, 18)
        Me.rbtnFinishGoods.TabIndex = 0
        Me.rbtnFinishGoods.Text = "Finish Goods"
        Me.rbtnFinishGoods.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnOtherGoods
        '
        Me.rbtnOtherGoods.Location = New System.Drawing.Point(104, 0)
        Me.rbtnOtherGoods.Name = "rbtnOtherGoods"
        Me.rbtnOtherGoods.Size = New System.Drawing.Size(84, 18)
        Me.rbtnOtherGoods.TabIndex = 1
        Me.rbtnOtherGoods.Text = "Other Goods"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.rbtnTPT)
        Me.Panel4.Controls.Add(Me.rbtnSaleAccount)
        Me.Panel4.Controls.Add(Me.rbtnHCessAccount)
        Me.Panel4.Controls.Add(Me.rbtnExciseAccount)
        Me.Panel4.Controls.Add(Me.rbtnECessAccount)
        Me.Panel4.Location = New System.Drawing.Point(3, 24)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(521, 21)
        Me.Panel4.TabIndex = 4
        Me.Panel4.Visible = False
        '
        'rbtnTPT
        '
        Me.rbtnTPT.Location = New System.Drawing.Point(428, 0)
        Me.rbtnTPT.Name = "rbtnTPT"
        Me.rbtnTPT.Size = New System.Drawing.Size(83, 18)
        Me.rbtnTPT.TabIndex = 3
        Me.rbtnTPT.Text = "TPT Account"
        '
        'rbtnSaleAccount
        '
        Me.rbtnSaleAccount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnSaleAccount.Location = New System.Drawing.Point(6, 0)
        Me.rbtnSaleAccount.Name = "rbtnSaleAccount"
        Me.rbtnSaleAccount.Size = New System.Drawing.Size(85, 18)
        Me.rbtnSaleAccount.TabIndex = 0
        Me.rbtnSaleAccount.Text = "Sale Account"
        Me.rbtnSaleAccount.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnHCessAccount
        '
        Me.rbtnHCessAccount.Location = New System.Drawing.Point(320, 0)
        Me.rbtnHCessAccount.Name = "rbtnHCessAccount"
        Me.rbtnHCessAccount.Size = New System.Drawing.Size(95, 18)
        Me.rbtnHCessAccount.TabIndex = 2
        Me.rbtnHCessAccount.Text = "HCess Account"
        '
        'rbtnExciseAccount
        '
        Me.rbtnExciseAccount.Location = New System.Drawing.Point(104, 0)
        Me.rbtnExciseAccount.Name = "rbtnExciseAccount"
        Me.rbtnExciseAccount.Size = New System.Drawing.Size(94, 18)
        Me.rbtnExciseAccount.TabIndex = 1
        Me.rbtnExciseAccount.Text = "Excise Account"
        '
        'rbtnECessAccount
        '
        Me.rbtnECessAccount.Location = New System.Drawing.Point(212, 0)
        Me.rbtnECessAccount.Name = "rbtnECessAccount"
        Me.rbtnECessAccount.Size = New System.Drawing.Size(93, 18)
        Me.rbtnECessAccount.TabIndex = 2
        Me.rbtnECessAccount.Text = "ECess Account"
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.rbtnSaleRegister)
        Me.Panel12.Controls.Add(Me.rbtnPurchaseBook)
        Me.Panel12.Controls.Add(Me.rbtnSaleRecoChart)
        Me.Panel12.Controls.Add(Me.rbtnNone)
        Me.Panel12.Controls.Add(Me.rbtnBank)
        Me.Panel12.Controls.Add(Me.rbtnVendor)
        Me.Panel12.Controls.Add(Me.rbtnCustomer)
        Me.Panel12.Location = New System.Drawing.Point(3, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(474, 21)
        Me.Panel12.TabIndex = 3
        '
        'rbtnSaleRegister
        '
        Me.rbtnSaleRegister.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSaleRegister.Location = New System.Drawing.Point(392, 0)
        Me.rbtnSaleRegister.Name = "rbtnSaleRegister"
        Me.rbtnSaleRegister.Size = New System.Drawing.Size(72, 15)
        Me.rbtnSaleRegister.TabIndex = 5
        Me.rbtnSaleRegister.Text = "Sale Register"
        '
        'rbtnPurchaseBook
        '
        Me.rbtnPurchaseBook.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnPurchaseBook.Location = New System.Drawing.Point(308, 0)
        Me.rbtnPurchaseBook.Name = "rbtnPurchaseBook"
        Me.rbtnPurchaseBook.Size = New System.Drawing.Size(80, 15)
        Me.rbtnPurchaseBook.TabIndex = 4
        Me.rbtnPurchaseBook.Text = "Purchase Book"
        '
        'rbtnSaleRecoChart
        '
        Me.rbtnSaleRecoChart.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSaleRecoChart.Location = New System.Drawing.Point(220, 0)
        Me.rbtnSaleRecoChart.Name = "rbtnSaleRecoChart"
        Me.rbtnSaleRecoChart.Size = New System.Drawing.Size(83, 15)
        Me.rbtnSaleRecoChart.TabIndex = 3
        Me.rbtnSaleRecoChart.Text = "Sale Reco Chart"
        '
        'rbtnNone
        '
        Me.rbtnNone.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnNone.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnNone.Location = New System.Drawing.Point(4, 0)
        Me.rbtnNone.Name = "rbtnNone"
        Me.rbtnNone.Size = New System.Drawing.Size(42, 15)
        Me.rbtnNone.TabIndex = 0
        Me.rbtnNone.Text = "None"
        Me.rbtnNone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnBank
        '
        Me.rbtnBank.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnBank.Location = New System.Drawing.Point(175, 0)
        Me.rbtnBank.Name = "rbtnBank"
        Me.rbtnBank.Size = New System.Drawing.Size(39, 15)
        Me.rbtnBank.TabIndex = 2
        Me.rbtnBank.Text = "Bank"
        '
        'rbtnVendor
        '
        Me.rbtnVendor.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnVendor.Location = New System.Drawing.Point(53, 0)
        Me.rbtnVendor.Name = "rbtnVendor"
        Me.rbtnVendor.Size = New System.Drawing.Size(49, 15)
        Me.rbtnVendor.TabIndex = 1
        Me.rbtnVendor.Text = "Vendor"
        '
        'rbtnCustomer
        '
        Me.rbtnCustomer.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnCustomer.Location = New System.Drawing.Point(110, 0)
        Me.rbtnCustomer.Name = "rbtnCustomer"
        Me.rbtnCustomer.Size = New System.Drawing.Size(59, 15)
        Me.rbtnCustomer.TabIndex = 2
        Me.rbtnCustomer.Text = "Customer"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(961, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 27)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(885, -19)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(144, 18)
        Me.RadButton1.TabIndex = 5
        Me.RadButton1.Text = "Relationship Between Table"
        Me.RadButton1.Visible = False
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(75, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 27)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(146, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 27)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "Print"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 27)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = ">>>"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(318, 5)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(68, 27)
        Me.RadButton2.TabIndex = 313
        Me.RadButton2.Text = "Show Trial"
        Me.RadButton2.Visible = False
        '
        'GLTransReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 494)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(737, 524)
        Me.Name = "GLTransReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "General Ledger"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.chkIncludeYearEndEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIndAS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeingAdjustmentEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCusVendWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkWithoutOpening, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.pnlLocSeg.ResumeLayout(False)
        Me.pnlLocSeg.PerformLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAccount.ResumeLayout(False)
        Me.pnlAccount.PerformLayout()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlVehicle.ResumeLayout(False)
        Me.pnlVehicle.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMachine.ResumeLayout(False)
        Me.pnlMachine.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDepartment.ResumeLayout(False)
        Me.pnlDepartment.PerformLayout()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEmployee.ResumeLayout(False)
        Me.pnlEmployee.PerformLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlVisiPMX.ResumeLayout(False)
        Me.pnlVisiPMX.PerformLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSourceCode.ResumeLayout(False)
        Me.pnlSourceCode.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAccountgrp.ResumeLayout(False)
        Me.pnlAccountgrp.PerformLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSettlementSheetClearance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcludeTemplete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlGroupingType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMonthWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdminSetting.ResumeLayout(False)
        Me.pnlAdminSetting.PerformLayout()
        Me.pnlSaleRegister.ResumeLayout(False)
        Me.pnlSaleRegister.PerformLayout()
        CType(Me.rbtnVAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMismatched, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPurchaseBook.ResumeLayout(False)
        Me.pnlPurchaseBook.PerformLayout()
        CType(Me.rbtnFinishGoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnOtherGoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnTPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSaleAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnHCessAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnExciseAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnECessAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        CType(Me.rbtnSaleRegister, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnPurchaseBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSaleRecoChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnNone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkMonthWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents ddlGroupingType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents chkExcludeTemplete As common.Controls.MyCheckBox
    Friend WithEvents chkSettlementSheetClearance As common.Controls.MyCheckBox
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlAdminSetting As System.Windows.Forms.Panel
    Friend WithEvents rbtnVendor As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnNone As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnBank As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnCustomer As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rbtnTPT As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSaleAccount As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnHCessAccount As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnExciseAccount As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnECessAccount As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSaleRecoChart As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnPurchaseBook As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents pnlPurchaseBook As System.Windows.Forms.Panel
    Friend WithEvents rbtnFinishGoods As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnOtherGoods As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkMismatched As common.Controls.MyCheckBox
    Friend WithEvents pnlSaleRegister As System.Windows.Forms.Panel
    Friend WithEvents rbtnVAT As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnCST As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSaleRegister As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocationSeg As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtMachine As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtVehicle As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtDepartment As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtAccount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtSourceCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtVisiPMX As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtAccountGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents pnlAccount As System.Windows.Forms.Panel
    Friend WithEvents pnlAccountGroup As System.Windows.Forms.Panel
    Friend WithEvents pnlSourceCode As System.Windows.Forms.Panel
    Friend WithEvents pnlVisiPMX As System.Windows.Forms.Panel
    Friend WithEvents pnlEmployee As System.Windows.Forms.Panel
    Friend WithEvents pnlDepartment As System.Windows.Forms.Panel
    Friend WithEvents pnlMachine As System.Windows.Forms.Panel
    Friend WithEvents pnlVehicle As System.Windows.Forms.Panel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlAccountgrp As System.Windows.Forms.Panel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtACGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents chkCusVendWiseSummary As common.Controls.MyCheckBox
    Public WithEvents chkWithoutOpening As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkIncludeingClosingEntry As common.Controls.MyCheckBox
    Friend WithEvents chkIncludeingAdjustmentEntry As common.Controls.MyCheckBox
    Friend WithEvents radbtnBulkExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents BulkExportCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkExportXls As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkIndAS As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkIncludeYearEndEntry As common.Controls.MyCheckBox
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadButton2 As RadButton
End Class

