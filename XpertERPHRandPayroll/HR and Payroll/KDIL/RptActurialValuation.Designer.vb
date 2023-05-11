<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptActurialValuation
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDetail = New common.Controls.MyRadioButton()
        Me.rbtnSummary = New common.Controls.MyRadioButton()
        Me.fndLeaveCode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLeaveName = New common.Controls.MyLabel()
        Me.todate = New common.Controls.MyDateTimePicker()
        Me.dtpfromdate1 = New common.Controls.MyDateTimePicker()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.TxtMultiEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.lblEmployee = New common.Controls.MyLabel()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.txtFromYear = New common.Controls.MyDateTimePicker()
        Me.lblYear = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLeaveName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.todate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1082, 492)
        Me.SplitContainer1.SplitterDistance = 447
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1082, 447)
        Me.RadPageView1.TabIndex = 4
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromYear)
        Me.RadPageViewPage1.Controls.Add(Me.lblYear)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1061, 399)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnDetail)
        Me.RadGroupBox1.Controls.Add(Me.rbtnSummary)
        Me.RadGroupBox1.Controls.Add(Me.fndLeaveCode)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblLeaveName)
        Me.RadGroupBox1.Controls.Add(Me.todate)
        Me.RadGroupBox1.Controls.Add(Me.dtpfromdate1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox1.Controls.Add(Me.TxtMultiEmployee)
        Me.RadGroupBox1.Controls.Add(Me.lblEmployee)
        Me.RadGroupBox1.Controls.Add(Me.txtDivisionMult)
        Me.RadGroupBox1.Controls.Add(Me.lblDivision)
        Me.RadGroupBox1.Controls.Add(Me.lblLocationCode)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(455, 147)
        Me.RadGroupBox1.TabIndex = 0
        '
        'rbtnDetail
        '
        Me.rbtnDetail.Location = New System.Drawing.Point(145, 122)
        Me.rbtnDetail.MyLinkLable1 = Nothing
        Me.rbtnDetail.MyLinkLable2 = Nothing
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 363
        Me.rbtnDetail.TabStop = False
        Me.rbtnDetail.Text = "Detail"
        '
        'rbtnSummary
        '
        Me.rbtnSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnSummary.Location = New System.Drawing.Point(72, 122)
        Me.rbtnSummary.MyLinkLable1 = Nothing
        Me.rbtnSummary.MyLinkLable2 = Nothing
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 362
        Me.rbtnSummary.Text = "Summary"
        Me.rbtnSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'fndLeaveCode
        '
        Me.fndLeaveCode.CalculationExpression = Nothing
        Me.fndLeaveCode.FieldCode = Nothing
        Me.fndLeaveCode.FieldDesc = Nothing
        Me.fndLeaveCode.FieldMaxLength = 0
        Me.fndLeaveCode.FieldName = Nothing
        Me.fndLeaveCode.isCalculatedField = False
        Me.fndLeaveCode.IsSourceFromTable = False
        Me.fndLeaveCode.IsSourceFromValueList = False
        Me.fndLeaveCode.IsUnique = False
        Me.fndLeaveCode.Location = New System.Drawing.Point(75, 98)
        Me.fndLeaveCode.MendatroryField = True
        Me.fndLeaveCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLeaveCode.MyLinkLable1 = Me.lblLocation
        Me.fndLeaveCode.MyLinkLable2 = Me.lblLeaveName
        Me.fndLeaveCode.MyReadOnly = False
        Me.fndLeaveCode.MyShowMasterFormButton = False
        Me.fndLeaveCode.Name = "fndLeaveCode"
        Me.fndLeaveCode.ReferenceFieldDesc = Nothing
        Me.fndLeaveCode.ReferenceFieldName = Nothing
        Me.fndLeaveCode.ReferenceTableName = Nothing
        Me.fndLeaveCode.Size = New System.Drawing.Size(145, 18)
        Me.fndLeaveCode.TabIndex = 359
        Me.fndLeaveCode.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(13, 100)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(37, 16)
        Me.lblLocation.TabIndex = 360
        Me.lblLocation.Text = "Leave"
        '
        'lblLeaveName
        '
        Me.lblLeaveName.AutoSize = False
        Me.lblLeaveName.BorderVisible = True
        Me.lblLeaveName.FieldName = Nothing
        Me.lblLeaveName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLeaveName.Location = New System.Drawing.Point(226, 98)
        Me.lblLeaveName.Name = "lblLeaveName"
        Me.lblLeaveName.Size = New System.Drawing.Size(210, 18)
        Me.lblLeaveName.TabIndex = 361
        Me.lblLeaveName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLeaveName.TextWrap = False
        '
        'todate
        '
        Me.todate.AccessibleName = " dtpTodate"
        Me.todate.CalculationExpression = Nothing
        Me.todate.CustomFormat = "dd-MM-yyyy"
        Me.todate.FieldCode = Nothing
        Me.todate.FieldDesc = Nothing
        Me.todate.FieldMaxLength = 0
        Me.todate.FieldName = Nothing
        Me.todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.todate.isCalculatedField = False
        Me.todate.IsSourceFromTable = False
        Me.todate.IsSourceFromValueList = False
        Me.todate.IsUnique = False
        Me.todate.Location = New System.Drawing.Point(279, 8)
        Me.todate.MendatroryField = False
        Me.todate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.todate.MyLinkLable1 = Nothing
        Me.todate.MyLinkLable2 = Nothing
        Me.todate.Name = "todate"
        Me.todate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.todate.ReferenceFieldDesc = Nothing
        Me.todate.ReferenceFieldName = Nothing
        Me.todate.ReferenceTableName = Nothing
        Me.todate.Size = New System.Drawing.Size(82, 20)
        Me.todate.TabIndex = 356
        Me.todate.TabStop = False
        Me.todate.Text = "14-09-2011"
        Me.todate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'dtpfromdate1
        '
        Me.dtpfromdate1.AccessibleName = "dtpfromdate1"
        Me.dtpfromdate1.CalculationExpression = Nothing
        Me.dtpfromdate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate1.FieldCode = Nothing
        Me.dtpfromdate1.FieldDesc = Nothing
        Me.dtpfromdate1.FieldMaxLength = 0
        Me.dtpfromdate1.FieldName = Nothing
        Me.dtpfromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate1.isCalculatedField = False
        Me.dtpfromdate1.IsSourceFromTable = False
        Me.dtpfromdate1.IsSourceFromValueList = False
        Me.dtpfromdate1.IsUnique = False
        Me.dtpfromdate1.Location = New System.Drawing.Point(75, 8)
        Me.dtpfromdate1.MendatroryField = False
        Me.dtpfromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate1.MyLinkLable1 = Nothing
        Me.dtpfromdate1.MyLinkLable2 = Nothing
        Me.dtpfromdate1.Name = "dtpfromdate1"
        Me.dtpfromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate1.ReferenceFieldDesc = Nothing
        Me.dtpfromdate1.ReferenceFieldName = Nothing
        Me.dtpfromdate1.ReferenceTableName = Nothing
        Me.dtpfromdate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate1.TabIndex = 355
        Me.dtpfromdate1.TabStop = False
        Me.dtpfromdate1.Text = "14-09-2011"
        Me.dtpfromdate1.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Location = New System.Drawing.Point(228, 9)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel7.TabIndex = 358
        Me.RadLabel7.Text = "To Date"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Location = New System.Drawing.Point(13, 9)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel8.TabIndex = 357
        Me.RadLabel8.Text = "From Date"
        '
        'TxtMultiEmployee
        '
        Me.TxtMultiEmployee.arrDispalyMember = Nothing
        Me.TxtMultiEmployee.arrValueMember = Nothing
        Me.TxtMultiEmployee.Location = New System.Drawing.Point(74, 76)
        Me.TxtMultiEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiEmployee.MyLinkLable1 = Nothing
        Me.TxtMultiEmployee.MyLinkLable2 = Nothing
        Me.TxtMultiEmployee.MyNullText = "All"
        Me.TxtMultiEmployee.Name = "TxtMultiEmployee"
        Me.TxtMultiEmployee.Size = New System.Drawing.Size(362, 19)
        Me.TxtMultiEmployee.TabIndex = 354
        '
        'lblEmployee
        '
        Me.lblEmployee.FieldName = Nothing
        Me.lblEmployee.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblEmployee.Location = New System.Drawing.Point(13, 79)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(60, 16)
        Me.lblEmployee.TabIndex = 353
        Me.lblEmployee.Text = "Employee"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(74, 54)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(362, 19)
        Me.txtDivisionMult.TabIndex = 352
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDivision.Location = New System.Drawing.Point(13, 57)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(50, 16)
        Me.lblDivision.TabIndex = 351
        Me.lblDivision.Text = "Division"
        '
        'lblLocationCode
        '
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(13, 33)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(49, 18)
        Me.lblLocationCode.TabIndex = 350
        Me.lblLocationCode.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(74, 32)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocationCode
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(362, 19)
        Me.txtLocation.TabIndex = 349
        '
        'txtFromYear
        '
        Me.txtFromYear.CalculationExpression = Nothing
        Me.txtFromYear.CustomFormat = "yyyy"
        Me.txtFromYear.FieldCode = Nothing
        Me.txtFromYear.FieldDesc = Nothing
        Me.txtFromYear.FieldMaxLength = 0
        Me.txtFromYear.FieldName = Nothing
        Me.txtFromYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromYear.isCalculatedField = False
        Me.txtFromYear.IsSourceFromTable = False
        Me.txtFromYear.IsSourceFromValueList = False
        Me.txtFromYear.IsUnique = False
        Me.txtFromYear.Location = New System.Drawing.Point(464, 171)
        Me.txtFromYear.MendatroryField = False
        Me.txtFromYear.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromYear.MyLinkLable1 = Nothing
        Me.txtFromYear.MyLinkLable2 = Nothing
        Me.txtFromYear.Name = "txtFromYear"
        Me.txtFromYear.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromYear.ReferenceFieldDesc = Nothing
        Me.txtFromYear.ReferenceFieldName = Nothing
        Me.txtFromYear.ReferenceTableName = Nothing
        Me.txtFromYear.Size = New System.Drawing.Size(103, 20)
        Me.txtFromYear.TabIndex = 30
        Me.txtFromYear.TabStop = False
        Me.txtFromYear.Text = "2011"
        Me.txtFromYear.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        Me.txtFromYear.Visible = False
        '
        'lblYear
        '
        Me.lblYear.FieldName = Nothing
        Me.lblYear.Location = New System.Drawing.Point(430, 171)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(28, 18)
        Me.lblYear.TabIndex = 13
        Me.lblYear.Text = "Year"
        Me.lblYear.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1061, 419)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1061, 419)
        Me.gv1.TabIndex = 0
        Me.gv1.Text = "gv1"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(216, 11)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 18)
        Me.RadSplitButton1.TabIndex = 25
        Me.RadSplitButton1.Text = "Export"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(10, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 18)
        Me.btnRefresh.TabIndex = 17
        Me.btnRefresh.Text = ">>"
        Me.btnRefresh.Visible = False
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(78, 11)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(66, 18)
        Me.BtnReset.TabIndex = 16
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1000, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 19)
        Me.btnclose.TabIndex = 17
        Me.btnclose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(146, 11)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(66, 18)
        Me.btnGo.TabIndex = 15
        Me.btnGo.Text = "Print"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1082, 20)
        Me.RadMenu1.TabIndex = 4
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.Visible = False
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Setting"
        Me.rmSaveLayout.AccessibleName = "Setting"
        Me.rmSaveLayout.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.rmDeleteLayout})
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Setting"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Save Layout"
        Me.RadMenuItem2.AccessibleName = "Save Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
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
        'RptActurialValuation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1082, 512)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "RptActurialValuation"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptActurialValuation"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLeaveName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.todate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtFromYear As common.Controls.MyDateTimePicker
    Friend WithEvents lblYear As common.Controls.MyLabel
    Friend WithEvents TxtMultiEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblEmployee As common.Controls.MyLabel
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents todate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate1 As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents fndLeaveCode As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLeaveName As common.Controls.MyLabel
    Friend WithEvents rbtnDetail As common.Controls.MyRadioButton
    Friend WithEvents rbtnSummary As common.Controls.MyRadioButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class

