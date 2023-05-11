Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeductionRegister
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
        Me.btnGenrate = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MulttxtFromPP = New common.UserControls.txtMultiSelectFinder()
        Me.MultfndDivisionCode = New common.UserControls.txtMultiSelectFinder()
        Me.MultfndEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.MultfndLocationCode = New common.UserControls.txtMultiSelectFinder()
        Me.chkPivot = New common.Controls.MyCheckBox()
        Me.txtEmployeeName = New common.Controls.MyLabel()
        Me.fndEmployee = New common.UserControls.txtFinder()
        Me.lblemployee = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblDivisionName = New common.Controls.MyLabel()
        Me.lblTopp = New common.Controls.MyLabel()
        Me.fndDivisionCode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtFromPP = New common.UserControls.txtFinder()
        Me.lblFrompp = New common.Controls.MyLabel()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.fndLocationCode = New common.UserControls.txtFinder()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.txtTopp = New common.UserControls.txtFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemSave = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemDelete = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenrate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkPivot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTopp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.btnGenrate.Location = New System.Drawing.Point(6, 6)
        Me.btnGenrate.Name = "btnGenrate"
        Me.btnGenrate.Size = New System.Drawing.Size(81, 21)
        Me.btnGenrate.TabIndex = 0
        Me.btnGenrate.Text = "Refresh"
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
        Me.RadPageView1.Text = "RadPageView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MulttxtFromPP)
        Me.RadPageViewPage1.Controls.Add(Me.MultfndDivisionCode)
        Me.RadPageViewPage1.Controls.Add(Me.MultfndEmployee)
        Me.RadPageViewPage1.Controls.Add(Me.MultfndLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.chkPivot)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmployeeName)
        Me.RadPageViewPage1.Controls.Add(Me.fndEmployee)
        Me.RadPageViewPage1.Controls.Add(Me.lblemployee)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblDivisionName)
        Me.RadPageViewPage1.Controls.Add(Me.lblTopp)
        Me.RadPageViewPage1.Controls.Add(Me.fndDivisionCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromPP)
        Me.RadPageViewPage1.Controls.Add(Me.lblDivision)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblFrompp)
        Me.RadPageViewPage1.Controls.Add(Me.txtTopp)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1021, 414)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'MulttxtFromPP
        '
        Me.MulttxtFromPP.arrDispalyMember = Nothing
        Me.MulttxtFromPP.arrValueMember = Nothing
        Me.MulttxtFromPP.Location = New System.Drawing.Point(81, 12)
        Me.MulttxtFromPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MulttxtFromPP.MyLinkLable1 = Nothing
        Me.MulttxtFromPP.MyLinkLable2 = Nothing
        Me.MulttxtFromPP.MyNullText = "All"
        Me.MulttxtFromPP.Name = "MulttxtFromPP"
        Me.MulttxtFromPP.Size = New System.Drawing.Size(367, 19)
        Me.MulttxtFromPP.TabIndex = 234
        '
        'MultfndDivisionCode
        '
        Me.MultfndDivisionCode.arrDispalyMember = Nothing
        Me.MultfndDivisionCode.arrValueMember = Nothing
        Me.MultfndDivisionCode.Location = New System.Drawing.Point(81, 56)
        Me.MultfndDivisionCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultfndDivisionCode.MyLinkLable1 = Nothing
        Me.MultfndDivisionCode.MyLinkLable2 = Nothing
        Me.MultfndDivisionCode.MyNullText = "All"
        Me.MultfndDivisionCode.Name = "MultfndDivisionCode"
        Me.MultfndDivisionCode.Size = New System.Drawing.Size(367, 19)
        Me.MultfndDivisionCode.TabIndex = 233
        '
        'MultfndEmployee
        '
        Me.MultfndEmployee.arrDispalyMember = Nothing
        Me.MultfndEmployee.arrValueMember = Nothing
        Me.MultfndEmployee.Location = New System.Drawing.Point(80, 78)
        Me.MultfndEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultfndEmployee.MyLinkLable1 = Nothing
        Me.MultfndEmployee.MyLinkLable2 = Nothing
        Me.MultfndEmployee.MyNullText = "All"
        Me.MultfndEmployee.Name = "MultfndEmployee"
        Me.MultfndEmployee.Size = New System.Drawing.Size(367, 19)
        Me.MultfndEmployee.TabIndex = 232
        '
        'MultfndLocationCode
        '
        Me.MultfndLocationCode.arrDispalyMember = Nothing
        Me.MultfndLocationCode.arrValueMember = Nothing
        Me.MultfndLocationCode.Location = New System.Drawing.Point(81, 34)
        Me.MultfndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultfndLocationCode.MyLinkLable1 = Nothing
        Me.MultfndLocationCode.MyLinkLable2 = Nothing
        Me.MultfndLocationCode.MyNullText = "All"
        Me.MultfndLocationCode.Name = "MultfndLocationCode"
        Me.MultfndLocationCode.Size = New System.Drawing.Size(367, 19)
        Me.MultfndLocationCode.TabIndex = 231
        '
        'chkPivot
        '
        Me.chkPivot.Location = New System.Drawing.Point(456, 11)
        Me.chkPivot.MyLinkLable1 = Nothing
        Me.chkPivot.MyLinkLable2 = Nothing
        Me.chkPivot.Name = "chkPivot"
        Me.chkPivot.Size = New System.Drawing.Size(76, 18)
        Me.chkPivot.TabIndex = 230
        Me.chkPivot.Tag1 = Nothing
        Me.chkPivot.Text = "Show Pivot"
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.AutoSize = False
        Me.txtEmployeeName.BorderVisible = True
        Me.txtEmployeeName.FieldName = Nothing
        Me.txtEmployeeName.Location = New System.Drawing.Point(314, 329)
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.Size = New System.Drawing.Size(177, 18)
        Me.txtEmployeeName.TabIndex = 229
        Me.txtEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEmployeeName.Visible = False
        '
        'fndEmployee
        '
        Me.fndEmployee.CalculationExpression = Nothing
        Me.fndEmployee.FieldCode = Nothing
        Me.fndEmployee.FieldDesc = Nothing
        Me.fndEmployee.FieldMaxLength = 0
        Me.fndEmployee.FieldName = Nothing
        Me.fndEmployee.isCalculatedField = False
        Me.fndEmployee.IsSourceFromTable = False
        Me.fndEmployee.IsSourceFromValueList = False
        Me.fndEmployee.IsUnique = False
        Me.fndEmployee.Location = New System.Drawing.Point(124, 329)
        Me.fndEmployee.MendatroryField = False
        Me.fndEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmployee.MyLinkLable1 = Nothing
        Me.fndEmployee.MyLinkLable2 = Nothing
        Me.fndEmployee.MyReadOnly = False
        Me.fndEmployee.MyShowMasterFormButton = False
        Me.fndEmployee.Name = "fndEmployee"
        Me.fndEmployee.ReferenceFieldDesc = Nothing
        Me.fndEmployee.ReferenceFieldName = Nothing
        Me.fndEmployee.ReferenceTableName = Nothing
        Me.fndEmployee.Size = New System.Drawing.Size(177, 19)
        Me.fndEmployee.TabIndex = 228
        Me.fndEmployee.Value = ""
        Me.fndEmployee.Visible = False
        '
        'lblemployee
        '
        Me.lblemployee.FieldName = Nothing
        Me.lblemployee.Location = New System.Drawing.Point(18, 76)
        Me.lblemployee.Name = "lblemployee"
        Me.lblemployee.Size = New System.Drawing.Size(55, 18)
        Me.lblemployee.TabIndex = 227
        Me.lblemployee.Text = "Employee"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(18, 12)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Pay Period"
        '
        'lblDivisionName
        '
        Me.lblDivisionName.AutoSize = False
        Me.lblDivisionName.BorderVisible = True
        Me.lblDivisionName.FieldName = Nothing
        Me.lblDivisionName.Location = New System.Drawing.Point(755, 308)
        Me.lblDivisionName.Name = "lblDivisionName"
        Me.lblDivisionName.Size = New System.Drawing.Size(177, 21)
        Me.lblDivisionName.TabIndex = 226
        Me.lblDivisionName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDivisionName.Visible = False
        '
        'lblTopp
        '
        Me.lblTopp.AutoSize = False
        Me.lblTopp.BorderVisible = True
        Me.lblTopp.FieldName = Nothing
        Me.lblTopp.Location = New System.Drawing.Point(756, 284)
        Me.lblTopp.Name = "lblTopp"
        Me.lblTopp.Size = New System.Drawing.Size(177, 18)
        Me.lblTopp.TabIndex = 217
        Me.lblTopp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTopp.Visible = False
        '
        'fndDivisionCode
        '
        Me.fndDivisionCode.CalculationExpression = Nothing
        Me.fndDivisionCode.FieldCode = Nothing
        Me.fndDivisionCode.FieldDesc = Nothing
        Me.fndDivisionCode.FieldMaxLength = 0
        Me.fndDivisionCode.FieldName = Nothing
        Me.fndDivisionCode.isCalculatedField = False
        Me.fndDivisionCode.IsSourceFromTable = False
        Me.fndDivisionCode.IsSourceFromValueList = False
        Me.fndDivisionCode.IsUnique = False
        Me.fndDivisionCode.Location = New System.Drawing.Point(578, 309)
        Me.fndDivisionCode.MendatroryField = False
        Me.fndDivisionCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDivisionCode.MyLinkLable1 = Nothing
        Me.fndDivisionCode.MyLinkLable2 = Me.lblDivisionName
        Me.fndDivisionCode.MyReadOnly = False
        Me.fndDivisionCode.MyShowMasterFormButton = False
        Me.fndDivisionCode.Name = "fndDivisionCode"
        Me.fndDivisionCode.ReferenceFieldDesc = Nothing
        Me.fndDivisionCode.ReferenceFieldName = Nothing
        Me.fndDivisionCode.ReferenceTableName = Nothing
        Me.fndDivisionCode.Size = New System.Drawing.Size(177, 18)
        Me.fndDivisionCode.TabIndex = 225
        Me.fndDivisionCode.Value = ""
        Me.fndDivisionCode.Visible = False
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(18, 34)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 221
        Me.lblLocation.Text = "Location"
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
        Me.txtFromPP.Location = New System.Drawing.Point(125, 284)
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
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.FieldName = Nothing
        Me.lblFrompp.Location = New System.Drawing.Point(315, 284)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(177, 18)
        Me.lblFrompp.TabIndex = 213
        Me.lblFrompp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFrompp.Visible = False
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Location = New System.Drawing.Point(18, 58)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(46, 18)
        Me.lblDivision.TabIndex = 224
        Me.lblDivision.Text = "Division"
        '
        'fndLocationCode
        '
        Me.fndLocationCode.CalculationExpression = Nothing
        Me.fndLocationCode.FieldCode = Nothing
        Me.fndLocationCode.FieldDesc = Nothing
        Me.fndLocationCode.FieldMaxLength = 0
        Me.fndLocationCode.FieldName = Nothing
        Me.fndLocationCode.isCalculatedField = False
        Me.fndLocationCode.IsSourceFromTable = False
        Me.fndLocationCode.IsSourceFromValueList = False
        Me.fndLocationCode.IsUnique = False
        Me.fndLocationCode.Location = New System.Drawing.Point(124, 308)
        Me.fndLocationCode.MendatroryField = False
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Nothing
        Me.fndLocationCode.MyLinkLable2 = Me.lblLocationName
        Me.fndLocationCode.MyReadOnly = False
        Me.fndLocationCode.MyShowMasterFormButton = False
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.ReferenceFieldDesc = Nothing
        Me.fndLocationCode.ReferenceFieldName = Nothing
        Me.fndLocationCode.ReferenceTableName = Nothing
        Me.fndLocationCode.Size = New System.Drawing.Size(177, 18)
        Me.fndLocationCode.TabIndex = 222
        Me.fndLocationCode.Value = ""
        Me.fndLocationCode.Visible = False
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(314, 308)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(177, 18)
        Me.lblLocationName.TabIndex = 223
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.Visible = False
        '
        'txtTopp
        '
        Me.txtTopp.CalculationExpression = Nothing
        Me.txtTopp.FieldCode = Nothing
        Me.txtTopp.FieldDesc = Nothing
        Me.txtTopp.FieldMaxLength = 0
        Me.txtTopp.FieldName = Nothing
        Me.txtTopp.isCalculatedField = False
        Me.txtTopp.IsSourceFromTable = False
        Me.txtTopp.IsSourceFromValueList = False
        Me.txtTopp.IsUnique = False
        Me.txtTopp.Location = New System.Drawing.Point(579, 284)
        Me.txtTopp.MendatroryField = True
        Me.txtTopp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTopp.MyLinkLable1 = Nothing
        Me.txtTopp.MyLinkLable2 = Me.lblFrompp
        Me.txtTopp.MyReadOnly = False
        Me.txtTopp.MyShowMasterFormButton = False
        Me.txtTopp.Name = "txtTopp"
        Me.txtTopp.ReferenceFieldDesc = Nothing
        Me.txtTopp.ReferenceFieldName = Nothing
        Me.txtTopp.ReferenceTableName = Nothing
        Me.txtTopp.Size = New System.Drawing.Size(177, 18)
        Me.txtTopp.TabIndex = 215
        Me.txtTopp.Value = ""
        Me.txtTopp.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1021, 414)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1021, 414)
        Me.gv1.TabIndex = 147
        Me.gv1.Text = "RadGridView4"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1042, 20)
        Me.RadMenu1.TabIndex = 214
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemSave, Me.RadMenuItemDelete})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItemSave
        '
        Me.RadMenuItemSave.AccessibleDescription = "Save Layout"
        Me.RadMenuItemSave.AccessibleName = "Save Layout"
        Me.RadMenuItemSave.Name = "RadMenuItemSave"
        Me.RadMenuItemSave.Text = "Save Layout"
        '
        'RadMenuItemDelete
        '
        Me.RadMenuItemDelete.AccessibleDescription = "Delete Layout"
        Me.RadMenuItemDelete.AccessibleName = "Delete Layout"
        Me.RadMenuItemDelete.Name = "RadMenuItemDelete"
        Me.RadMenuItemDelete.Text = "Delete Layout"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnReset.Location = New System.Drawing.Point(176, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(81, 21)
        Me.btnReset.TabIndex = 335
        Me.btnReset.Text = "Reset"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(89, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(81, 21)
        Me.btnExport.TabIndex = 333
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.XpertERPHRandPayroll.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.XpertERPHRandPayroll.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'frmDeductionRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDeductionRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Deduction Register"
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
        CType(Me.chkPivot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblemployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTopp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGenrate As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemSave As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemDelete As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents fndEmployee As common.UserControls.txtFinder
    Friend WithEvents lblemployee As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents lblTopp As common.Controls.MyLabel
    Friend WithEvents fndDivisionCode As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtFromPP As common.UserControls.txtFinder
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents fndLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents txtTopp As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtEmployeeName As common.Controls.MyLabel
    Friend WithEvents chkPivot As common.Controls.MyCheckBox
    Friend WithEvents MultfndDivisionCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MultfndEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MultfndLocationCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents MulttxtFromPP As common.UserControls.txtMultiSelectFinder
End Class

