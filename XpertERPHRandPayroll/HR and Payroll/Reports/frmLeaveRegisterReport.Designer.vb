Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveRegisterReport
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
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGenrate = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkShowResigned = New System.Windows.Forms.CheckBox()
        Me.TxtDepartment = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtMultLeaveCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.TxtMultiEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.lblEmployee = New common.Controls.MyLabel()
        Me.rbtnDetail = New common.Controls.MyRadioButton()
        Me.rbtnSummary = New common.Controls.MyRadioButton()
        Me.lblFrompp = New common.Controls.MyLabel()
        Me.txtFromPP = New common.UserControls.txtFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv3 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemSave = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemDelete = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.gv1 = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.gv2 = New Telerik.WinControls.UI.MasterGridViewTemplate()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenrate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(516, 393)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(92, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Select Pay Period"
        Me.RadLabel1.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(914, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(113, 21)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnGenrate
        '
        Me.btnGenrate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenrate.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnGenrate.Location = New System.Drawing.Point(13, 6)
        Me.btnGenrate.Name = "btnGenrate"
        Me.btnGenrate.Size = New System.Drawing.Size(65, 21)
        Me.btnGenrate.TabIndex = 0
        Me.btnGenrate.Text = ">>"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGenrate)
        Me.SplitContainer1.Size = New System.Drawing.Size(1042, 516)
        Me.SplitContainer1.SplitterDistance = 482
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
        Me.RadPageView1.Size = New System.Drawing.Size(1042, 462)
        Me.RadPageView1.TabIndex = 215
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblFrompp)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromPP)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1021, 414)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkShowResigned)
        Me.GroupBox1.Controls.Add(Me.TxtDepartment)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.txtMultLeaveCode)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.txtDivisionMult)
        Me.GroupBox1.Controls.Add(Me.lblDivision)
        Me.GroupBox1.Controls.Add(Me.lblLocationCode)
        Me.GroupBox1.Controls.Add(Me.txtLocation)
        Me.GroupBox1.Controls.Add(Me.txtToDate)
        Me.GroupBox1.Controls.Add(Me.txtFromDate)
        Me.GroupBox1.Controls.Add(Me.lblToDate)
        Me.GroupBox1.Controls.Add(Me.lblfromDate)
        Me.GroupBox1.Controls.Add(Me.TxtMultiEmployee)
        Me.GroupBox1.Controls.Add(Me.lblEmployee)
        Me.GroupBox1.Controls.Add(Me.rbtnDetail)
        Me.GroupBox1.Controls.Add(Me.rbtnSummary)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(771, 189)
        Me.GroupBox1.TabIndex = 218
        Me.GroupBox1.TabStop = False
        '
        'chkShowResigned
        '
        Me.chkShowResigned.AutoSize = True
        Me.chkShowResigned.Location = New System.Drawing.Point(235, 157)
        Me.chkShowResigned.Name = "chkShowResigned"
        Me.chkShowResigned.Size = New System.Drawing.Size(182, 17)
        Me.chkShowResigned.TabIndex = 361
        Me.chkShowResigned.Text = "Show Resigned Employee also"
        Me.chkShowResigned.UseVisualStyleBackColor = True
        '
        'TxtDepartment
        '
        Me.TxtDepartment.arrDispalyMember = Nothing
        Me.TxtDepartment.arrValueMember = Nothing
        Me.TxtDepartment.Location = New System.Drawing.Point(99, 87)
        Me.TxtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDepartment.MyLinkLable1 = Nothing
        Me.TxtDepartment.MyLinkLable2 = Nothing
        Me.TxtDepartment.MyNullText = "All"
        Me.TxtDepartment.Name = "TxtDepartment"
        Me.TxtDepartment.Size = New System.Drawing.Size(331, 19)
        Me.TxtDepartment.TabIndex = 360
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 90)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel2.TabIndex = 359
        Me.MyLabel2.Text = "Department"
        '
        'txtMultLeaveCode
        '
        Me.txtMultLeaveCode.arrDispalyMember = Nothing
        Me.txtMultLeaveCode.arrValueMember = Nothing
        Me.txtMultLeaveCode.Location = New System.Drawing.Point(99, 132)
        Me.txtMultLeaveCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultLeaveCode.MyLinkLable1 = Nothing
        Me.txtMultLeaveCode.MyLinkLable2 = Nothing
        Me.txtMultLeaveCode.MyNullText = "All"
        Me.txtMultLeaveCode.Name = "txtMultLeaveCode"
        Me.txtMultLeaveCode.Size = New System.Drawing.Size(331, 19)
        Me.txtMultLeaveCode.TabIndex = 358
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(7, 132)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel1.TabIndex = 357
        Me.MyLabel1.Text = "Leave Code"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(99, 65)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(331, 19)
        Me.txtDivisionMult.TabIndex = 356
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDivision.Location = New System.Drawing.Point(6, 68)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(50, 16)
        Me.lblDivision.TabIndex = 355
        Me.lblDivision.Text = "Division"
        '
        'lblLocationCode
        '
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(6, 44)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(49, 18)
        Me.lblLocationCode.TabIndex = 354
        Me.lblLocationCode.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(99, 43)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocationCode
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(331, 19)
        Me.txtLocation.TabIndex = 353
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
        Me.txtToDate.Location = New System.Drawing.Point(324, 17)
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
        Me.txtToDate.TabIndex = 219
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
        Me.txtFromDate.Location = New System.Drawing.Point(99, 18)
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
        Me.txtFromDate.TabIndex = 218
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(272, 18)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 221
        Me.lblToDate.Text = "To Date"
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(6, 19)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 220
        Me.lblfromDate.Text = "From Date"
        '
        'TxtMultiEmployee
        '
        Me.TxtMultiEmployee.arrDispalyMember = Nothing
        Me.TxtMultiEmployee.arrValueMember = Nothing
        Me.TxtMultiEmployee.Location = New System.Drawing.Point(99, 110)
        Me.TxtMultiEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiEmployee.MyLinkLable1 = Nothing
        Me.TxtMultiEmployee.MyLinkLable2 = Nothing
        Me.TxtMultiEmployee.MyNullText = "All"
        Me.TxtMultiEmployee.Name = "TxtMultiEmployee"
        Me.TxtMultiEmployee.Size = New System.Drawing.Size(331, 19)
        Me.TxtMultiEmployee.TabIndex = 217
        '
        'lblEmployee
        '
        Me.lblEmployee.FieldName = Nothing
        Me.lblEmployee.Location = New System.Drawing.Point(6, 111)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(84, 18)
        Me.lblEmployee.TabIndex = 216
        Me.lblEmployee.Text = "Employee Code"
        '
        'rbtnDetail
        '
        Me.rbtnDetail.Location = New System.Drawing.Point(168, 159)
        Me.rbtnDetail.MyLinkLable1 = Nothing
        Me.rbtnDetail.MyLinkLable2 = Nothing
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 215
        Me.rbtnDetail.TabStop = False
        Me.rbtnDetail.Text = "Detail"
        '
        'rbtnSummary
        '
        Me.rbtnSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnSummary.Location = New System.Drawing.Point(95, 159)
        Me.rbtnSummary.MyLinkLable1 = Nothing
        Me.rbtnSummary.MyLinkLable2 = Nothing
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 1
        Me.rbtnSummary.Text = "Summary"
        Me.rbtnSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.FieldName = Nothing
        Me.lblFrompp.Location = New System.Drawing.Point(786, 393)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(177, 18)
        Me.lblFrompp.TabIndex = 213
        Me.lblFrompp.Visible = False
        '
        'txtFromPP
        '
        Me.txtFromPP.CalculationExpression = Nothing
        Me.txtFromPP.FieldCode = Nothing
        Me.txtFromPP.FieldDesc = Nothing
        Me.txtFromPP.FieldMaxLength = 0
        Me.txtFromPP.FieldName = Nothing
        Me.txtFromPP.isCalculatedField = False
        Me.txtFromPP.IsSourceFromTable = False
        Me.txtFromPP.IsSourceFromValueList = False
        Me.txtFromPP.IsUnique = False
        Me.txtFromPP.Location = New System.Drawing.Point(610, 393)
        Me.txtFromPP.MendatroryField = True
        Me.txtFromPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromPP.MyLinkLable1 = Me.RadLabel1
        Me.txtFromPP.MyLinkLable2 = Me.lblFrompp
        Me.txtFromPP.MyReadOnly = False
        Me.txtFromPP.MyShowMasterFormButton = False
        Me.txtFromPP.Name = "txtFromPP"
        Me.txtFromPP.ReferenceFieldDesc = Nothing
        Me.txtFromPP.ReferenceFieldName = Nothing
        Me.txtFromPP.ReferenceTableName = Nothing
        Me.txtFromPP.Size = New System.Drawing.Size(177, 18)
        Me.txtFromPP.TabIndex = 212
        Me.txtFromPP.Value = ""
        Me.txtFromPP.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv3)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1021, 414)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv3
        '
        Me.gv3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv3.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv3.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv3.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv3.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv3.MyStopExport = False
        Me.gv3.Name = "gv3"
        Me.gv3.ShowHeaderCellButtons = True
        Me.gv3.Size = New System.Drawing.Size(1021, 414)
        Me.gv3.TabIndex = 1
        Me.gv3.VarID = ""
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1042, 20)
        Me.RadMenu1.TabIndex = 214
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemSave, Me.RadMenuItemDelete})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItemSave
        '
        Me.RadMenuItemSave.Name = "RadMenuItemSave"
        Me.RadMenuItemSave.Text = "Save Layout"
        '
        'RadMenuItemDelete
        '
        Me.RadMenuItemDelete.Name = "RadMenuItemDelete"
        Me.RadMenuItemDelete.Text = "Delete Layout"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnReset.Location = New System.Drawing.Point(84, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(78, 21)
        Me.btnReset.TabIndex = 338
        Me.btnReset.Text = "Reset"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnBack.Location = New System.Drawing.Point(255, 6)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(81, 21)
        Me.btnBack.TabIndex = 337
        Me.btnBack.Text = "Back"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(169, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(81, 21)
        Me.btnExport.TabIndex = 332
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Image = Global.XpertERPHRandPayroll.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.Image = Global.XpertERPHRandPayroll.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'gv1
        '
        Me.gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.AllowAddNewRow = False
        Me.gv1.AllowEditRow = False
        Me.gv1.EnableFiltering = True
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.ViewDefinition = TableViewDefinition5
        '
        'gv2
        '
        Me.gv2.AllowAddNewRow = False
        Me.gv2.ViewDefinition = TableViewDefinition6
        '
        'frmLeaveRegisterReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLeaveRegisterReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Leave Register"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGenrate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv3.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGenrate As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents txtFromPP As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemSave As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemDelete As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtnDetail As common.Controls.MyRadioButton
    Friend WithEvents rbtnSummary As common.Controls.MyRadioButton
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtMultiEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblEmployee As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gv1 As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMultLeaveCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents gv2 As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents gv3 As common.UserControls.MyRadGridView
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtDepartment As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents chkShowResigned As System.Windows.Forms.CheckBox
End Class

