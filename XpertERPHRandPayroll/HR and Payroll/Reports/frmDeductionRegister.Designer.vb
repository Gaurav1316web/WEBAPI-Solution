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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
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
        Me.lbldeduction = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblDivisionName = New common.Controls.MyLabel()
        Me.lblTopp = New common.Controls.MyLabel()
        Me.fndDeductioncode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtFromPP = New common.UserControls.txtFinder()
        Me.lblpayperiod = New common.Controls.MyLabel()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.fndPayperiod = New common.UserControls.txtFinder()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.txtTopp = New common.UserControls.txtFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemSave = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemDelete = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.brngo = New Telerik.WinControls.UI.RadButton()
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
        CType(Me.lbldeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTopp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpayperiod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.brngo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btnGenrate.Location = New System.Drawing.Point(566, 6)
        Me.btnGenrate.Name = "btnGenrate"
        Me.btnGenrate.Size = New System.Drawing.Size(81, 21)
        Me.btnGenrate.TabIndex = 0
        Me.btnGenrate.Text = "Refresh"
        Me.btnGenrate.Visible = False
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.brngo)
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
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.MulttxtFromPP)
        Me.RadPageViewPage1.Controls.Add(Me.MultfndDivisionCode)
        Me.RadPageViewPage1.Controls.Add(Me.MultfndEmployee)
        Me.RadPageViewPage1.Controls.Add(Me.MultfndLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.chkPivot)
        Me.RadPageViewPage1.Controls.Add(Me.lbldeduction)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.lblDivisionName)
        Me.RadPageViewPage1.Controls.Add(Me.lblTopp)
        Me.RadPageViewPage1.Controls.Add(Me.fndDeductioncode)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromPP)
        Me.RadPageViewPage1.Controls.Add(Me.lblDivision)
        Me.RadPageViewPage1.Controls.Add(Me.fndPayperiod)
        Me.RadPageViewPage1.Controls.Add(Me.lblpayperiod)
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
        Me.MulttxtFromPP.Location = New System.Drawing.Point(101, 244)
        Me.MulttxtFromPP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MulttxtFromPP.MyLinkLable1 = Nothing
        Me.MulttxtFromPP.MyLinkLable2 = Nothing
        Me.MulttxtFromPP.MyNullText = "All"
        Me.MulttxtFromPP.Name = "MulttxtFromPP"
        Me.MulttxtFromPP.Size = New System.Drawing.Size(367, 19)
        Me.MulttxtFromPP.TabIndex = 234
        Me.MulttxtFromPP.Visible = False
        '
        'MultfndDivisionCode
        '
        Me.MultfndDivisionCode.arrDispalyMember = Nothing
        Me.MultfndDivisionCode.arrValueMember = Nothing
        Me.MultfndDivisionCode.Location = New System.Drawing.Point(101, 194)
        Me.MultfndDivisionCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultfndDivisionCode.MyLinkLable1 = Nothing
        Me.MultfndDivisionCode.MyLinkLable2 = Nothing
        Me.MultfndDivisionCode.MyNullText = "All"
        Me.MultfndDivisionCode.Name = "MultfndDivisionCode"
        Me.MultfndDivisionCode.Size = New System.Drawing.Size(367, 19)
        Me.MultfndDivisionCode.TabIndex = 233
        Me.MultfndDivisionCode.Visible = False
        '
        'MultfndEmployee
        '
        Me.MultfndEmployee.arrDispalyMember = Nothing
        Me.MultfndEmployee.arrValueMember = Nothing
        Me.MultfndEmployee.Location = New System.Drawing.Point(101, 219)
        Me.MultfndEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultfndEmployee.MyLinkLable1 = Nothing
        Me.MultfndEmployee.MyLinkLable2 = Nothing
        Me.MultfndEmployee.MyNullText = "All"
        Me.MultfndEmployee.Name = "MultfndEmployee"
        Me.MultfndEmployee.Size = New System.Drawing.Size(367, 19)
        Me.MultfndEmployee.TabIndex = 232
        Me.MultfndEmployee.Visible = False
        '
        'MultfndLocationCode
        '
        Me.MultfndLocationCode.arrDispalyMember = Nothing
        Me.MultfndLocationCode.arrValueMember = Nothing
        Me.MultfndLocationCode.Location = New System.Drawing.Point(101, 169)
        Me.MultfndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultfndLocationCode.MyLinkLable1 = Nothing
        Me.MultfndLocationCode.MyLinkLable2 = Nothing
        Me.MultfndLocationCode.MyNullText = "All"
        Me.MultfndLocationCode.Name = "MultfndLocationCode"
        Me.MultfndLocationCode.Size = New System.Drawing.Size(367, 19)
        Me.MultfndLocationCode.TabIndex = 231
        Me.MultfndLocationCode.Visible = False
        '
        'chkPivot
        '
        Me.chkPivot.Location = New System.Drawing.Point(467, 12)
        Me.chkPivot.MyLinkLable1 = Nothing
        Me.chkPivot.MyLinkLable2 = Nothing
        Me.chkPivot.Name = "chkPivot"
        Me.chkPivot.Size = New System.Drawing.Size(76, 18)
        Me.chkPivot.TabIndex = 230
        Me.chkPivot.Tag1 = Nothing
        Me.chkPivot.Text = "Show Pivot"
        Me.chkPivot.Visible = False
        '
        'lbldeduction
        '
        Me.lbldeduction.AutoSize = False
        Me.lbldeduction.BorderVisible = True
        Me.lbldeduction.FieldName = Nothing
        Me.lbldeduction.Location = New System.Drawing.Point(266, 54)
        Me.lbldeduction.Name = "lbldeduction"
        Me.lbldeduction.Size = New System.Drawing.Size(195, 18)
        Me.lbldeduction.TabIndex = 229
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(83, 33)
        Me.fndLocation.MendatroryField = False
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(177, 19)
        Me.fndLocation.TabIndex = 228
        Me.fndLocation.Value = ""
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
        Me.lblTopp.Visible = False
        '
        'fndDeductioncode
        '
        Me.fndDeductioncode.CalculationExpression = Nothing
        Me.fndDeductioncode.FieldCode = Nothing
        Me.fndDeductioncode.FieldDesc = Nothing
        Me.fndDeductioncode.FieldMaxLength = 0
        Me.fndDeductioncode.FieldName = Nothing
        Me.fndDeductioncode.isCalculatedField = False
        Me.fndDeductioncode.IsSourceFromTable = False
        Me.fndDeductioncode.IsSourceFromValueList = False
        Me.fndDeductioncode.IsUnique = False
        Me.fndDeductioncode.Location = New System.Drawing.Point(83, 54)
        Me.fndDeductioncode.MendatroryField = False
        Me.fndDeductioncode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDeductioncode.MyLinkLable1 = Nothing
        Me.fndDeductioncode.MyLinkLable2 = Me.lblDivisionName
        Me.fndDeductioncode.MyReadOnly = False
        Me.fndDeductioncode.MyShowMasterFormButton = False
        Me.fndDeductioncode.Name = "fndDeductioncode"
        Me.fndDeductioncode.ReferenceFieldDesc = Nothing
        Me.fndDeductioncode.ReferenceFieldName = Nothing
        Me.fndDeductioncode.ReferenceTableName = Nothing
        Me.fndDeductioncode.Size = New System.Drawing.Size(177, 18)
        Me.fndDeductioncode.TabIndex = 225
        Me.fndDeductioncode.Value = ""
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
        Me.txtFromPP.MyLinkLable2 = Me.lblpayperiod
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
        'lblpayperiod
        '
        Me.lblpayperiod.AutoSize = False
        Me.lblpayperiod.BorderVisible = True
        Me.lblpayperiod.FieldName = Nothing
        Me.lblpayperiod.Location = New System.Drawing.Point(266, 12)
        Me.lblpayperiod.Name = "lblpayperiod"
        Me.lblpayperiod.Size = New System.Drawing.Size(195, 18)
        Me.lblpayperiod.TabIndex = 213
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Location = New System.Drawing.Point(18, 58)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(58, 18)
        Me.lblDivision.TabIndex = 224
        Me.lblDivision.Text = "Deduction"
        '
        'fndPayperiod
        '
        Me.fndPayperiod.CalculationExpression = Nothing
        Me.fndPayperiod.FieldCode = Nothing
        Me.fndPayperiod.FieldDesc = Nothing
        Me.fndPayperiod.FieldMaxLength = 0
        Me.fndPayperiod.FieldName = Nothing
        Me.fndPayperiod.isCalculatedField = False
        Me.fndPayperiod.IsSourceFromTable = False
        Me.fndPayperiod.IsSourceFromValueList = False
        Me.fndPayperiod.IsUnique = False
        Me.fndPayperiod.Location = New System.Drawing.Point(83, 12)
        Me.fndPayperiod.MendatroryField = False
        Me.fndPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayperiod.MyLinkLable1 = Nothing
        Me.fndPayperiod.MyLinkLable2 = Me.lblLocationName
        Me.fndPayperiod.MyReadOnly = False
        Me.fndPayperiod.MyShowMasterFormButton = False
        Me.fndPayperiod.Name = "fndPayperiod"
        Me.fndPayperiod.ReferenceFieldDesc = Nothing
        Me.fndPayperiod.ReferenceFieldName = Nothing
        Me.fndPayperiod.ReferenceTableName = Nothing
        Me.fndPayperiod.Size = New System.Drawing.Size(177, 18)
        Me.fndPayperiod.TabIndex = 222
        Me.fndPayperiod.Value = ""
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(266, 33)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(195, 18)
        Me.lblLocationName.TabIndex = 223
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
        Me.txtTopp.MyLinkLable2 = Me.lblpayperiod
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
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1021, 414)
        Me.gv1.TabIndex = 147
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
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnprint.Location = New System.Drawing.Point(173, 6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(81, 21)
        Me.btnprint.TabIndex = 337
        Me.btnprint.Text = "Print"
        '
        'brngo
        '
        Me.brngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.brngo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.brngo.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.brngo.Location = New System.Drawing.Point(5, 6)
        Me.brngo.Name = "brngo"
        Me.brngo.Size = New System.Drawing.Size(81, 21)
        Me.brngo.TabIndex = 336
        Me.brngo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnReset.Location = New System.Drawing.Point(257, 6)
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
        CType(Me.lbldeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTopp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpayperiod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.brngo, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblDivisionName As common.Controls.MyLabel
    Friend WithEvents lblTopp As common.Controls.MyLabel
    Friend WithEvents fndDeductioncode As common.UserControls.txtFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtFromPP As common.UserControls.txtFinder
    Friend WithEvents lblpayperiod As common.Controls.MyLabel
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents fndPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents txtTopp As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lbldeduction As common.Controls.MyLabel
    Friend WithEvents chkPivot As common.Controls.MyCheckBox
    Friend WithEvents MultfndDivisionCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MultfndEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MultfndLocationCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents MulttxtFromPP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents brngo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
End Class

