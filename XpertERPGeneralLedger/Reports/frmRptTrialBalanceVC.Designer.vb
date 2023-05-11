<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptTrialBalanceVC
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
        Me.gvDB = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.cboAddCVAfter = New common.Controls.MyComboBox()
        Me.chkIncludeingClosingEntry = New common.Controls.MyCheckBox()
        Me.chkIncludeingAdjustmentEntry = New common.Controls.MyCheckBox()
        Me.txtMainAccount = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtSubGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkDateRange = New common.Controls.MyCheckBox()
        Me.txtACGrpType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMainGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
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
        Me.txtLocationSegmant = New common.UserControls.txtMultiSelectFinder()
        Me.txtAccount = New common.UserControls.txtMultiSelectFinder()
        Me.txtEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.txtSourceCode = New common.UserControls.txtMultiSelectFinder()
        Me.txtDepartment = New common.UserControls.txtMultiSelectFinder()
        Me.txtVISIPMX = New common.UserControls.txtMultiSelectFinder()
        Me.txtMachine = New common.UserControls.txtMultiSelectFinder()
        Me.txtVehicle = New common.UserControls.txtMultiSelectFinder()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblFromdate = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.cboReportType = New common.Controls.MyComboBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAddCVAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeingAdjustmentEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel2.SuspendLayout()
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
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadPanel1.Controls.Add(Me.MyLabel7)
        Me.RadPanel1.Controls.Add(Me.cboAddCVAfter)
        Me.RadPanel1.Controls.Add(Me.chkIncludeingClosingEntry)
        Me.RadPanel1.Controls.Add(Me.chkIncludeingAdjustmentEntry)
        Me.RadPanel1.Controls.Add(Me.txtMainAccount)
        Me.RadPanel1.Controls.Add(Me.MyLabel6)
        Me.RadPanel1.Controls.Add(Me.txtSubGroup)
        Me.RadPanel1.Controls.Add(Me.MyLabel5)
        Me.RadPanel1.Controls.Add(Me.txtGroup)
        Me.RadPanel1.Controls.Add(Me.MyLabel4)
        Me.RadPanel1.Controls.Add(Me.chkDateRange)
        Me.RadPanel1.Controls.Add(Me.txtACGrpType)
        Me.RadPanel1.Controls.Add(Me.MyLabel3)
        Me.RadPanel1.Controls.Add(Me.txtMainGroup)
        Me.RadPanel1.Controls.Add(Me.MyLabel2)
        Me.RadPanel1.Controls.Add(Me.FlowLayoutPanel2)
        Me.RadPanel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.RadPanel1.Controls.Add(Me.txtFromDate)
        Me.RadPanel1.Controls.Add(Me.txtToDate)
        Me.RadPanel1.Controls.Add(Me.lblToDate)
        Me.RadPanel1.Controls.Add(Me.lblFromdate)
        Me.RadPanel1.Controls.Add(Me.RadLabel3)
        Me.RadPanel1.Controls.Add(Me.cboReportType)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(956, 310)
        Me.RadPanel1.TabIndex = 15
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(406, 27)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(147, 18)
        Me.MyLabel7.TabIndex = 372
        Me.MyLabel7.Text = "Add Customer/Vendor After"
        '
        'cboAddCVAfter
        '
        Me.cboAddCVAfter.AutoCompleteDisplayMember = Nothing
        Me.cboAddCVAfter.AutoCompleteValueMember = Nothing
        Me.cboAddCVAfter.CalculationExpression = Nothing
        Me.cboAddCVAfter.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAddCVAfter.FieldCode = Nothing
        Me.cboAddCVAfter.FieldDesc = Nothing
        Me.cboAddCVAfter.FieldMaxLength = 0
        Me.cboAddCVAfter.FieldName = Nothing
        Me.cboAddCVAfter.isCalculatedField = False
        Me.cboAddCVAfter.IsSourceFromTable = False
        Me.cboAddCVAfter.IsSourceFromValueList = False
        Me.cboAddCVAfter.IsUnique = False
        Me.cboAddCVAfter.Location = New System.Drawing.Point(559, 26)
        Me.cboAddCVAfter.MendatroryField = False
        Me.cboAddCVAfter.MyLinkLable1 = Me.MyLabel7
        Me.cboAddCVAfter.MyLinkLable2 = Nothing
        Me.cboAddCVAfter.Name = "cboAddCVAfter"
        Me.cboAddCVAfter.ReferenceFieldDesc = Nothing
        Me.cboAddCVAfter.ReferenceFieldName = Nothing
        Me.cboAddCVAfter.ReferenceTableName = Nothing
        Me.cboAddCVAfter.Size = New System.Drawing.Size(239, 20)
        Me.cboAddCVAfter.TabIndex = 371
        '
        'chkIncludeingClosingEntry
        '
        Me.chkIncludeingClosingEntry.Location = New System.Drawing.Point(573, 5)
        Me.chkIncludeingClosingEntry.MyLinkLable1 = Nothing
        Me.chkIncludeingClosingEntry.MyLinkLable2 = Nothing
        Me.chkIncludeingClosingEntry.Name = "chkIncludeingClosingEntry"
        Me.chkIncludeingClosingEntry.Size = New System.Drawing.Size(135, 18)
        Me.chkIncludeingClosingEntry.TabIndex = 370
        Me.chkIncludeingClosingEntry.Tag1 = Nothing
        Me.chkIncludeingClosingEntry.Text = "Including Closing Entry"
        '
        'chkIncludeingAdjustmentEntry
        '
        Me.chkIncludeingAdjustmentEntry.Location = New System.Drawing.Point(408, 5)
        Me.chkIncludeingAdjustmentEntry.MyLinkLable1 = Nothing
        Me.chkIncludeingAdjustmentEntry.MyLinkLable2 = Nothing
        Me.chkIncludeingAdjustmentEntry.Name = "chkIncludeingAdjustmentEntry"
        Me.chkIncludeingAdjustmentEntry.Size = New System.Drawing.Size(156, 18)
        Me.chkIncludeingAdjustmentEntry.TabIndex = 369
        Me.chkIncludeingAdjustmentEntry.Tag1 = Nothing
        Me.chkIncludeingAdjustmentEntry.Text = "Including Adjustment Entry"
        '
        'txtMainAccount
        '
        Me.txtMainAccount.arrDispalyMember = Nothing
        Me.txtMainAccount.arrValueMember = Nothing
        Me.txtMainAccount.Location = New System.Drawing.Point(118, 138)
        Me.txtMainAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMainAccount.MyLinkLable1 = Me.MyLabel6
        Me.txtMainAccount.MyLinkLable2 = Nothing
        Me.txtMainAccount.MyNullText = "All"
        Me.txtMainAccount.Name = "txtMainAccount"
        Me.txtMainAccount.Size = New System.Drawing.Size(281, 19)
        Me.txtMainAccount.TabIndex = 8
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(7, 138)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel6.TabIndex = 367
        Me.MyLabel6.Text = "Main Account"
        '
        'txtSubGroup
        '
        Me.txtSubGroup.arrDispalyMember = Nothing
        Me.txtSubGroup.arrValueMember = Nothing
        Me.txtSubGroup.Location = New System.Drawing.Point(118, 116)
        Me.txtSubGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubGroup.MyLinkLable1 = Me.MyLabel5
        Me.txtSubGroup.MyLinkLable2 = Nothing
        Me.txtSubGroup.MyNullText = "All"
        Me.txtSubGroup.Name = "txtSubGroup"
        Me.txtSubGroup.Size = New System.Drawing.Size(281, 19)
        Me.txtSubGroup.TabIndex = 7
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(7, 116)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel5.TabIndex = 365
        Me.MyLabel5.Text = "Sub Group"
        '
        'txtGroup
        '
        Me.txtGroup.arrDispalyMember = Nothing
        Me.txtGroup.arrValueMember = Nothing
        Me.txtGroup.Location = New System.Drawing.Point(118, 94)
        Me.txtGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroup.MyLinkLable1 = Me.MyLabel4
        Me.txtGroup.MyLinkLable2 = Nothing
        Me.txtGroup.MyNullText = "All"
        Me.txtGroup.Name = "txtGroup"
        Me.txtGroup.Size = New System.Drawing.Size(281, 19)
        Me.txtGroup.TabIndex = 6
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(7, 94)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(38, 18)
        Me.MyLabel4.TabIndex = 363
        Me.MyLabel4.Text = "Group"
        '
        'chkDateRange
        '
        Me.chkDateRange.Location = New System.Drawing.Point(11, 5)
        Me.chkDateRange.MyLinkLable1 = Nothing
        Me.chkDateRange.MyLinkLable2 = Nothing
        Me.chkDateRange.Name = "chkDateRange"
        Me.chkDateRange.Size = New System.Drawing.Size(78, 18)
        Me.chkDateRange.TabIndex = 0
        Me.chkDateRange.Tag1 = Nothing
        Me.chkDateRange.Text = "Date Range"
        '
        'txtACGrpType
        '
        Me.txtACGrpType.arrDispalyMember = Nothing
        Me.txtACGrpType.arrValueMember = Nothing
        Me.txtACGrpType.Location = New System.Drawing.Point(118, 50)
        Me.txtACGrpType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtACGrpType.MyLinkLable1 = Me.MyLabel3
        Me.txtACGrpType.MyLinkLable2 = Nothing
        Me.txtACGrpType.MyNullText = "All"
        Me.txtACGrpType.Name = "txtACGrpType"
        Me.txtACGrpType.Size = New System.Drawing.Size(281, 19)
        Me.txtACGrpType.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 50)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(109, 18)
        Me.MyLabel3.TabIndex = 359
        Me.MyLabel3.Text = "Account Group Type"
        '
        'txtMainGroup
        '
        Me.txtMainGroup.arrDispalyMember = Nothing
        Me.txtMainGroup.arrValueMember = Nothing
        Me.txtMainGroup.Location = New System.Drawing.Point(118, 72)
        Me.txtMainGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMainGroup.MyLinkLable1 = Me.MyLabel2
        Me.txtMainGroup.MyLinkLable2 = Nothing
        Me.txtMainGroup.MyNullText = "All"
        Me.txtMainGroup.Name = "txtMainGroup"
        Me.txtMainGroup.Size = New System.Drawing.Size(281, 19)
        Me.txtMainGroup.TabIndex = 5
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 72)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel2.TabIndex = 356
        Me.MyLabel2.Text = "Main Group"
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.lblLocationSegment)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblAccount)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblEmployee)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblSourceCode)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblDepartment)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblVISIPMX)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblMachine)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblVehicle)
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(405, 47)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(101, 228)
        Me.FlowLayoutPanel2.TabIndex = 354
        '
        'lblLocationSegment
        '
        Me.lblLocationSegment.FieldName = Nothing
        Me.lblLocationSegment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationSegment.Location = New System.Drawing.Point(3, 3)
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
        Me.lblAccount.Location = New System.Drawing.Point(3, 27)
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
        Me.lblEmployee.Location = New System.Drawing.Point(3, 51)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(55, 18)
        Me.lblEmployee.TabIndex = 344
        Me.lblEmployee.Text = "Employee"
        '
        'lblSourceCode
        '
        Me.lblSourceCode.FieldName = Nothing
        Me.lblSourceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceCode.Location = New System.Drawing.Point(3, 75)
        Me.lblSourceCode.Name = "lblSourceCode"
        Me.lblSourceCode.Size = New System.Drawing.Size(70, 18)
        Me.lblSourceCode.TabIndex = 338
        Me.lblSourceCode.Text = "Source Code"
        '
        'lblDepartment
        '
        Me.lblDepartment.FieldName = Nothing
        Me.lblDepartment.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(3, 99)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(66, 18)
        Me.lblDepartment.TabIndex = 345
        Me.lblDepartment.Text = "Department"
        '
        'lblVISIPMX
        '
        Me.lblVISIPMX.FieldName = Nothing
        Me.lblVISIPMX.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVISIPMX.Location = New System.Drawing.Point(3, 123)
        Me.lblVISIPMX.Name = "lblVISIPMX"
        Me.lblVISIPMX.Size = New System.Drawing.Size(53, 18)
        Me.lblVISIPMX.TabIndex = 347
        Me.lblVISIPMX.Text = "VISI/PMX"
        '
        'lblMachine
        '
        Me.lblMachine.FieldName = Nothing
        Me.lblMachine.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachine.Location = New System.Drawing.Point(3, 147)
        Me.lblMachine.Name = "lblMachine"
        Me.lblMachine.Size = New System.Drawing.Size(49, 18)
        Me.lblMachine.TabIndex = 349
        Me.lblMachine.Text = "Machine"
        '
        'lblVehicle
        '
        Me.lblVehicle.FieldName = Nothing
        Me.lblVehicle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicle.Location = New System.Drawing.Point(3, 171)
        Me.lblVehicle.Name = "lblVehicle"
        Me.lblVehicle.Size = New System.Drawing.Size(42, 18)
        Me.lblVehicle.TabIndex = 351
        Me.lblVehicle.Text = "Vehicle"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.txtLocationSegmant)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtAccount)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtEmployee)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtSourceCode)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtDepartment)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtVISIPMX)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtMachine)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtVehicle)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(518, 47)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(286, 228)
        Me.FlowLayoutPanel1.TabIndex = 9
        '
        'txtLocationSegmant
        '
        Me.txtLocationSegmant.arrDispalyMember = Nothing
        Me.txtLocationSegmant.arrValueMember = Nothing
        Me.txtLocationSegmant.Location = New System.Drawing.Point(3, 3)
        Me.txtLocationSegmant.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationSegmant.MyLinkLable1 = Me.lblLocationSegment
        Me.txtLocationSegmant.MyLinkLable2 = Nothing
        Me.txtLocationSegmant.MyNullText = "All"
        Me.txtLocationSegmant.Name = "txtLocationSegmant"
        Me.txtLocationSegmant.Size = New System.Drawing.Size(277, 19)
        Me.txtLocationSegmant.TabIndex = 0
        '
        'txtAccount
        '
        Me.txtAccount.arrDispalyMember = Nothing
        Me.txtAccount.arrValueMember = Nothing
        Me.txtAccount.Location = New System.Drawing.Point(3, 28)
        Me.txtAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccount.MyLinkLable1 = Me.lblAccount
        Me.txtAccount.MyLinkLable2 = Nothing
        Me.txtAccount.MyNullText = "All"
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.Size = New System.Drawing.Size(277, 19)
        Me.txtAccount.TabIndex = 1
        '
        'txtEmployee
        '
        Me.txtEmployee.arrDispalyMember = Nothing
        Me.txtEmployee.arrValueMember = Nothing
        Me.txtEmployee.Location = New System.Drawing.Point(3, 53)
        Me.txtEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployee.MyLinkLable1 = Me.lblEmployee
        Me.txtEmployee.MyLinkLable2 = Nothing
        Me.txtEmployee.MyNullText = "All"
        Me.txtEmployee.Name = "txtEmployee"
        Me.txtEmployee.Size = New System.Drawing.Size(277, 19)
        Me.txtEmployee.TabIndex = 2
        '
        'txtSourceCode
        '
        Me.txtSourceCode.arrDispalyMember = Nothing
        Me.txtSourceCode.arrValueMember = Nothing
        Me.txtSourceCode.Location = New System.Drawing.Point(3, 78)
        Me.txtSourceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSourceCode.MyLinkLable1 = Me.lblSourceCode
        Me.txtSourceCode.MyLinkLable2 = Nothing
        Me.txtSourceCode.MyNullText = "All"
        Me.txtSourceCode.Name = "txtSourceCode"
        Me.txtSourceCode.Size = New System.Drawing.Size(277, 19)
        Me.txtSourceCode.TabIndex = 3
        '
        'txtDepartment
        '
        Me.txtDepartment.arrDispalyMember = Nothing
        Me.txtDepartment.arrValueMember = Nothing
        Me.txtDepartment.Location = New System.Drawing.Point(3, 103)
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Me.lblDepartment
        Me.txtDepartment.MyLinkLable2 = Nothing
        Me.txtDepartment.MyNullText = "All"
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(277, 19)
        Me.txtDepartment.TabIndex = 4
        '
        'txtVISIPMX
        '
        Me.txtVISIPMX.arrDispalyMember = Nothing
        Me.txtVISIPMX.arrValueMember = Nothing
        Me.txtVISIPMX.Location = New System.Drawing.Point(3, 128)
        Me.txtVISIPMX.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVISIPMX.MyLinkLable1 = Me.lblVISIPMX
        Me.txtVISIPMX.MyLinkLable2 = Nothing
        Me.txtVISIPMX.MyNullText = "All"
        Me.txtVISIPMX.Name = "txtVISIPMX"
        Me.txtVISIPMX.Size = New System.Drawing.Size(277, 19)
        Me.txtVISIPMX.TabIndex = 5
        '
        'txtMachine
        '
        Me.txtMachine.arrDispalyMember = Nothing
        Me.txtMachine.arrValueMember = Nothing
        Me.txtMachine.Location = New System.Drawing.Point(3, 153)
        Me.txtMachine.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachine.MyLinkLable1 = Me.lblMachine
        Me.txtMachine.MyLinkLable2 = Nothing
        Me.txtMachine.MyNullText = "All"
        Me.txtMachine.Name = "txtMachine"
        Me.txtMachine.Size = New System.Drawing.Size(277, 19)
        Me.txtMachine.TabIndex = 6
        '
        'txtVehicle
        '
        Me.txtVehicle.arrDispalyMember = Nothing
        Me.txtVehicle.arrValueMember = Nothing
        Me.txtVehicle.Location = New System.Drawing.Point(3, 178)
        Me.txtVehicle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.MyLinkLable1 = Me.lblVehicle
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.MyNullText = "All"
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.Size = New System.Drawing.Size(277, 19)
        Me.txtVehicle.TabIndex = 7
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
        Me.txtFromDate.Location = New System.Drawing.Point(169, 4)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(78, 20)
        Me.txtFromDate.TabIndex = 1
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
        Me.txtToDate.Location = New System.Drawing.Point(317, 4)
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
        Me.txtToDate.TabIndex = 2
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "30/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(251, 5)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 78
        Me.lblToDate.Text = "To Date"
        '
        'lblFromdate
        '
        Me.lblFromdate.FieldName = Nothing
        Me.lblFromdate.Location = New System.Drawing.Point(106, 5)
        Me.lblFromdate.Name = "lblFromdate"
        Me.lblFromdate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromdate.TabIndex = 77
        Me.lblFromdate.Text = "From Date"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(7, 28)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(67, 18)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "Report Type"
        '
        'cboReportType
        '
        Me.cboReportType.AutoCompleteDisplayMember = Nothing
        Me.cboReportType.AutoCompleteValueMember = Nothing
        Me.cboReportType.CalculationExpression = Nothing
        Me.cboReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboReportType.FieldCode = Nothing
        Me.cboReportType.FieldDesc = Nothing
        Me.cboReportType.FieldMaxLength = 0
        Me.cboReportType.FieldName = Nothing
        Me.cboReportType.isCalculatedField = False
        Me.cboReportType.IsSourceFromTable = False
        Me.cboReportType.IsSourceFromValueList = False
        Me.cboReportType.IsUnique = False
        Me.cboReportType.Location = New System.Drawing.Point(118, 27)
        Me.cboReportType.MendatroryField = False
        Me.cboReportType.MyLinkLable1 = Me.RadLabel3
        Me.cboReportType.MyLinkLable2 = Nothing
        Me.cboReportType.Name = "cboReportType"
        Me.cboReportType.ReferenceFieldDesc = Nothing
        Me.cboReportType.ReferenceFieldName = Nothing
        Me.cboReportType.ReferenceTableName = Nothing
        Me.cboReportType.Size = New System.Drawing.Size(281, 20)
        Me.cboReportType.TabIndex = 3
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(956, 310)
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
        Me.gv1.Size = New System.Drawing.Size(956, 310)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExp)
        Me.Panel1.Controls.Add(Me.RadButton1)
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
        'btnExp
        '
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(228, 3)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(79, 18)
        Me.btnExp.TabIndex = 314
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
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(154, 3)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadButton1.TabIndex = 143
        Me.RadButton1.Text = "<< Back "
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
        Me.btnPrint.Location = New System.Drawing.Point(313, 3)
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
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(977, 20)
        Me.RadMenu1.TabIndex = 23
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
        'frmRptTrialBalanceVC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(977, 401)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmRptTrialBalanceVC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Trial Balance (Customer/Vendor)"
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAddCVAfter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeingClosingEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeingAdjustmentEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDateRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
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
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboReportType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboReportType As common.Controls.MyComboBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromdate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtAccount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtLocationSegmant As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtSourceCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents gvDB As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents txtDepartment As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtVISIPMX As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMachine As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtVehicle As common.UserControls.txtMultiSelectFinder
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
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
    Friend WithEvents txtACGrpType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents chkDateRange As common.Controls.MyCheckBox
    Friend WithEvents txtMainAccount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtSubGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkIncludeingClosingEntry As common.Controls.MyCheckBox
    Friend WithEvents chkIncludeingAdjustmentEntry As common.Controls.MyCheckBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents cboAddCVAfter As common.Controls.MyComboBox
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class

