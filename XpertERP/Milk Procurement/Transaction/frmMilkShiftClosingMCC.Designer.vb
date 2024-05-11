<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkShiftClosingMCC
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkAskSiloatShiftEnd = New common.Controls.MyCheckBox()
        Me.fndAutoInLoc = New common.UserControls.txtFinder()
        Me.MyLabel70 = New common.Controls.MyLabel()
        Me.txtSiloInLoc = New common.Controls.MyTextBox()
        Me.fndSiloInLoc = New common.UserControls.txtFinder()
        Me.lblSiloInLocation = New common.Controls.MyLabel()
        Me.fndMcc = New common.UserControls.txtFinder()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.BtnMilkTruckSheet = New Telerik.WinControls.UI.RadButton()
        Me.fndMccCode = New common.Controls.MyTextBox()
        Me.UsLock1 = New common.usLock()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.DtpMCCDate = New common.Controls.MyDateTimePicker()
        Me.DtpTime = New common.Controls.MyDateTimePicker()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.dtpDocDate = New common.Controls.MyDateTimePicker()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadPageView2 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GvRoute = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Pg_StockDetail = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.txtCLR = New common.MyNumBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.TxtManualSNF_Per = New common.MyNumBox()
        Me.LblManualSNF_Per = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.LblFirstWeightmentSample = New common.Controls.MyLabel()
        Me.LblLastsampleFATTime = New common.Controls.MyLabel()
        Me.LblShiftOpeningTime = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtBookSNF_per = New common.MyNumBox()
        Me.LblBookSNF_Per = New common.Controls.MyLabel()
        Me.TxtManualSNF = New common.MyNumBox()
        Me.LblManualSNF = New common.Controls.MyLabel()
        Me.TxtManualFat_Per = New common.MyNumBox()
        Me.LblManualFAT_Per = New common.Controls.MyLabel()
        Me.TxtBookFat_Per = New common.MyNumBox()
        Me.LblBookFat_Per = New common.Controls.MyLabel()
        Me.TxtManualFAT = New common.MyNumBox()
        Me.LblManualFAT = New common.Controls.MyLabel()
        Me.TxtManualStock = New common.MyNumBox()
        Me.LblManualStock = New common.Controls.MyLabel()
        Me.TxtActualSNF = New common.MyNumBox()
        Me.LblBookSNF = New common.Controls.MyLabel()
        Me.TxtActualFat = New common.MyNumBox()
        Me.LblBookFAT = New common.Controls.MyLabel()
        Me.TxtActualStock = New common.MyNumBox()
        Me.LblBookStock = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnemailsetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnemailsmssettingforvsp = New Telerik.WinControls.UI.RadMenuItem()
        Me.gvUOM = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkAskSiloatShiftEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel70, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSiloInLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSiloInLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnMilkTruckSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndMccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpMCCDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView2.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GvRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvRoute.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pg_StockDetail.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.txtCLR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.LblFirstWeightmentSample, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLastsampleFATTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblShiftOpeningTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBookSNF_per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookSNF_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualFat_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtBookFat_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookFat_Per, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtManualStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblManualStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtActualSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtActualFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtActualStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(988, 449)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 42
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(988, 413)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadPageView2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(85.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(967, 365)
        Me.RadPageViewPage1.Text = "Milk Shift End"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkAskSiloatShiftEnd)
        Me.RadGroupBox1.Controls.Add(Me.fndAutoInLoc)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel70)
        Me.RadGroupBox1.Controls.Add(Me.txtSiloInLoc)
        Me.RadGroupBox1.Controls.Add(Me.fndSiloInLoc)
        Me.RadGroupBox1.Controls.Add(Me.lblSiloInLocation)
        Me.RadGroupBox1.Controls.Add(Me.fndMcc)
        Me.RadGroupBox1.Controls.Add(Me.BtnMilkTruckSheet)
        Me.RadGroupBox1.Controls.Add(Me.fndMccCode)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.DtpMCCDate)
        Me.RadGroupBox1.Controls.Add(Me.DtpTime)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblBOMStatus)
        Me.RadGroupBox1.Controls.Add(Me.cboShift)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpDocDate)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Add Shift"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(967, 88)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "Add Shift"
        '
        'chkAskSiloatShiftEnd
        '
        Me.chkAskSiloatShiftEnd.Location = New System.Drawing.Point(688, 65)
        Me.chkAskSiloatShiftEnd.MyLinkLable1 = Nothing
        Me.chkAskSiloatShiftEnd.MyLinkLable2 = Nothing
        Me.chkAskSiloatShiftEnd.Name = "chkAskSiloatShiftEnd"
        Me.chkAskSiloatShiftEnd.Size = New System.Drawing.Size(119, 18)
        Me.chkAskSiloatShiftEnd.TabIndex = 1068
        Me.chkAskSiloatShiftEnd.Tag1 = Nothing
        Me.chkAskSiloatShiftEnd.Text = "Ask Silo at Shift End"
        '
        'fndAutoInLoc
        '
        Me.fndAutoInLoc.CalculationExpression = Nothing
        Me.fndAutoInLoc.FieldCode = Nothing
        Me.fndAutoInLoc.FieldDesc = Nothing
        Me.fndAutoInLoc.FieldMaxLength = 0
        Me.fndAutoInLoc.FieldName = Nothing
        Me.fndAutoInLoc.isCalculatedField = False
        Me.fndAutoInLoc.IsSourceFromTable = False
        Me.fndAutoInLoc.IsSourceFromValueList = False
        Me.fndAutoInLoc.IsUnique = False
        Me.fndAutoInLoc.Location = New System.Drawing.Point(545, 64)
        Me.fndAutoInLoc.MendatroryField = True
        Me.fndAutoInLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAutoInLoc.MyLinkLable1 = Nothing
        Me.fndAutoInLoc.MyLinkLable2 = Nothing
        Me.fndAutoInLoc.MyReadOnly = False
        Me.fndAutoInLoc.MyShowMasterFormButton = False
        Me.fndAutoInLoc.Name = "fndAutoInLoc"
        Me.fndAutoInLoc.ReferenceFieldDesc = Nothing
        Me.fndAutoInLoc.ReferenceFieldName = Nothing
        Me.fndAutoInLoc.ReferenceTableName = Nothing
        Me.fndAutoInLoc.Size = New System.Drawing.Size(135, 19)
        Me.fndAutoInLoc.TabIndex = 1066
        Me.fndAutoInLoc.Value = ""
        '
        'MyLabel70
        '
        Me.MyLabel70.FieldName = Nothing
        Me.MyLabel70.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel70.Location = New System.Drawing.Point(441, 65)
        Me.MyLabel70.Name = "MyLabel70"
        Me.MyLabel70.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel70.TabIndex = 1067
        Me.MyLabel70.Text = "AutoIn Location"
        '
        'txtSiloInLoc
        '
        Me.txtSiloInLoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtSiloInLoc.CalculationExpression = Nothing
        Me.txtSiloInLoc.Enabled = False
        Me.txtSiloInLoc.FieldCode = Nothing
        Me.txtSiloInLoc.FieldDesc = Nothing
        Me.txtSiloInLoc.FieldMaxLength = 0
        Me.txtSiloInLoc.FieldName = Nothing
        Me.txtSiloInLoc.isCalculatedField = False
        Me.txtSiloInLoc.IsSourceFromTable = False
        Me.txtSiloInLoc.IsSourceFromValueList = False
        Me.txtSiloInLoc.IsUnique = False
        Me.txtSiloInLoc.Location = New System.Drawing.Point(260, 64)
        Me.txtSiloInLoc.MendatroryField = False
        Me.txtSiloInLoc.MyLinkLable1 = Nothing
        Me.txtSiloInLoc.MyLinkLable2 = Nothing
        Me.txtSiloInLoc.Name = "txtSiloInLoc"
        Me.txtSiloInLoc.ReferenceFieldDesc = Nothing
        Me.txtSiloInLoc.ReferenceFieldName = Nothing
        Me.txtSiloInLoc.ReferenceTableName = Nothing
        Me.txtSiloInLoc.Size = New System.Drawing.Size(175, 20)
        Me.txtSiloInLoc.TabIndex = 1065
        '
        'fndSiloInLoc
        '
        Me.fndSiloInLoc.CalculationExpression = Nothing
        Me.fndSiloInLoc.FieldCode = Nothing
        Me.fndSiloInLoc.FieldDesc = Nothing
        Me.fndSiloInLoc.FieldMaxLength = 0
        Me.fndSiloInLoc.FieldName = Nothing
        Me.fndSiloInLoc.isCalculatedField = False
        Me.fndSiloInLoc.IsSourceFromTable = False
        Me.fndSiloInLoc.IsSourceFromValueList = False
        Me.fndSiloInLoc.IsUnique = False
        Me.fndSiloInLoc.Location = New System.Drawing.Point(119, 65)
        Me.fndSiloInLoc.MendatroryField = True
        Me.fndSiloInLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSiloInLoc.MyLinkLable1 = Nothing
        Me.fndSiloInLoc.MyLinkLable2 = Nothing
        Me.fndSiloInLoc.MyReadOnly = False
        Me.fndSiloInLoc.MyShowMasterFormButton = False
        Me.fndSiloInLoc.Name = "fndSiloInLoc"
        Me.fndSiloInLoc.ReferenceFieldDesc = Nothing
        Me.fndSiloInLoc.ReferenceFieldName = Nothing
        Me.fndSiloInLoc.ReferenceTableName = Nothing
        Me.fndSiloInLoc.Size = New System.Drawing.Size(135, 19)
        Me.fndSiloInLoc.TabIndex = 1063
        Me.fndSiloInLoc.Value = ""
        '
        'lblSiloInLocation
        '
        Me.lblSiloInLocation.FieldName = Nothing
        Me.lblSiloInLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSiloInLocation.Location = New System.Drawing.Point(14, 67)
        Me.lblSiloInLocation.Name = "lblSiloInLocation"
        Me.lblSiloInLocation.Size = New System.Drawing.Size(84, 16)
        Me.lblSiloInLocation.TabIndex = 1064
        Me.lblSiloInLocation.Text = "Silo In Location"
        '
        'fndMcc
        '
        Me.fndMcc.CalculationExpression = Nothing
        Me.fndMcc.FieldCode = Nothing
        Me.fndMcc.FieldDesc = Nothing
        Me.fndMcc.FieldMaxLength = 0
        Me.fndMcc.FieldName = Nothing
        Me.fndMcc.isCalculatedField = False
        Me.fndMcc.IsSourceFromTable = False
        Me.fndMcc.IsSourceFromValueList = False
        Me.fndMcc.IsUnique = False
        Me.fndMcc.Location = New System.Drawing.Point(118, 41)
        Me.fndMcc.MendatroryField = True
        Me.fndMcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMcc.MyLinkLable1 = Me.lblMCCCode
        Me.fndMcc.MyLinkLable2 = Nothing
        Me.fndMcc.MyReadOnly = False
        Me.fndMcc.MyShowMasterFormButton = False
        Me.fndMcc.Name = "fndMcc"
        Me.fndMcc.ReferenceFieldDesc = Nothing
        Me.fndMcc.ReferenceFieldName = Nothing
        Me.fndMcc.ReferenceTableName = Nothing
        Me.fndMcc.Size = New System.Drawing.Size(119, 19)
        Me.fndMcc.TabIndex = 54
        Me.fndMcc.Value = ""
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(14, 41)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 13
        Me.lblMCCCode.Text = "MCC Code"
        '
        'BtnMilkTruckSheet
        '
        Me.BtnMilkTruckSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnMilkTruckSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMilkTruckSheet.Location = New System.Drawing.Point(813, 61)
        Me.BtnMilkTruckSheet.Name = "BtnMilkTruckSheet"
        Me.BtnMilkTruckSheet.Size = New System.Drawing.Size(141, 18)
        Me.BtnMilkTruckSheet.TabIndex = 53
        Me.BtnMilkTruckSheet.Text = "Milk Truck Sheet"
        '
        'fndMccCode
        '
        Me.fndMccCode.CalculationExpression = Nothing
        Me.fndMccCode.FieldCode = Nothing
        Me.fndMccCode.FieldDesc = Nothing
        Me.fndMccCode.FieldMaxLength = 0
        Me.fndMccCode.FieldName = Nothing
        Me.fndMccCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMccCode.isCalculatedField = False
        Me.fndMccCode.IsSourceFromTable = False
        Me.fndMccCode.IsSourceFromValueList = False
        Me.fndMccCode.IsUnique = False
        Me.fndMccCode.Location = New System.Drawing.Point(239, 41)
        Me.fndMccCode.MaxLength = 200
        Me.fndMccCode.MendatroryField = False
        Me.fndMccCode.MyLinkLable1 = Nothing
        Me.fndMccCode.MyLinkLable2 = Nothing
        Me.fndMccCode.Name = "fndMccCode"
        Me.fndMccCode.ReferenceFieldDesc = Nothing
        Me.fndMccCode.ReferenceFieldName = Nothing
        Me.fndMccCode.ReferenceTableName = Nothing
        Me.fndMccCode.Size = New System.Drawing.Size(192, 18)
        Me.fndMccCode.TabIndex = 52
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(834, 16)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(101, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 47
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(438, 41)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel4.TabIndex = 21
        Me.MyLabel4.Text = "MCC Date"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(613, 17)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel3.TabIndex = 21
        Me.MyLabel3.Text = "Time"
        '
        'DtpMCCDate
        '
        Me.DtpMCCDate.CalculationExpression = Nothing
        Me.DtpMCCDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpMCCDate.FieldCode = Nothing
        Me.DtpMCCDate.FieldDesc = Nothing
        Me.DtpMCCDate.FieldMaxLength = 0
        Me.DtpMCCDate.FieldName = Nothing
        Me.DtpMCCDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpMCCDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpMCCDate.isCalculatedField = False
        Me.DtpMCCDate.IsSourceFromTable = False
        Me.DtpMCCDate.IsSourceFromValueList = False
        Me.DtpMCCDate.IsUnique = False
        Me.DtpMCCDate.Location = New System.Drawing.Point(504, 40)
        Me.DtpMCCDate.MendatroryField = True
        Me.DtpMCCDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpMCCDate.MyLinkLable1 = Me.MyLabel4
        Me.DtpMCCDate.MyLinkLable2 = Nothing
        Me.DtpMCCDate.Name = "DtpMCCDate"
        Me.DtpMCCDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpMCCDate.ReferenceFieldDesc = Nothing
        Me.DtpMCCDate.ReferenceFieldName = Nothing
        Me.DtpMCCDate.ReferenceTableName = Nothing
        Me.DtpMCCDate.Size = New System.Drawing.Size(100, 18)
        Me.DtpMCCDate.TabIndex = 20
        Me.DtpMCCDate.TabStop = False
        Me.DtpMCCDate.Text = "03/05/2011"
        Me.DtpMCCDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'DtpTime
        '
        Me.DtpTime.CalculationExpression = Nothing
        Me.DtpTime.CustomFormat = "HH:MM:ss"
        Me.DtpTime.FieldCode = Nothing
        Me.DtpTime.FieldDesc = Nothing
        Me.DtpTime.FieldMaxLength = 0
        Me.DtpTime.FieldName = Nothing
        Me.DtpTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTime.isCalculatedField = False
        Me.DtpTime.IsSourceFromTable = False
        Me.DtpTime.IsSourceFromValueList = False
        Me.DtpTime.IsUnique = False
        Me.DtpTime.Location = New System.Drawing.Point(678, 16)
        Me.DtpTime.MendatroryField = True
        Me.DtpTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTime.MyLinkLable1 = Me.MyLabel3
        Me.DtpTime.MyLinkLable2 = Nothing
        Me.DtpTime.Name = "DtpTime"
        Me.DtpTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpTime.ReferenceFieldDesc = Nothing
        Me.DtpTime.ReferenceFieldName = Nothing
        Me.DtpTime.ReferenceTableName = Nothing
        Me.DtpTime.Size = New System.Drawing.Size(119, 18)
        Me.DtpTime.TabIndex = 20
        Me.DtpTime.TabStop = False
        Me.DtpTime.Text = "11:08:32"
        Me.DtpTime.Value = New Date(2014, 8, 5, 11, 5, 32, 0)
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(412, 15)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 21)
        Me.btnnew.TabIndex = 44
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(613, 41)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(29, 16)
        Me.lblBOMStatus.TabIndex = 21
        Me.lblBOMStatus.Text = "Shift"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Location = New System.Drawing.Point(678, 40)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(119, 18)
        Me.cboShift.TabIndex = 20
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(438, 17)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDocDate.TabIndex = 19
        Me.lblDocDate.Text = "Date"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CalculationExpression = Nothing
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocDate.FieldCode = Nothing
        Me.dtpDocDate.FieldDesc = Nothing
        Me.dtpDocDate.FieldMaxLength = 0
        Me.dtpDocDate.FieldName = Nothing
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.isCalculatedField = False
        Me.dtpDocDate.IsSourceFromTable = False
        Me.dtpDocDate.IsSourceFromValueList = False
        Me.dtpDocDate.IsUnique = False
        Me.dtpDocDate.Location = New System.Drawing.Point(504, 16)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.ReferenceFieldDesc = Nothing
        Me.dtpDocDate.ReferenceFieldName = Nothing
        Me.dtpDocDate.ReferenceTableName = Nothing
        Me.dtpDocDate.Size = New System.Drawing.Size(100, 18)
        Me.dtpDocDate.TabIndex = 18
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(14, 17)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(99, 16)
        Me.lblCode.TabIndex = 10
        Me.lblCode.Text = "Milk Shift End No"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(118, 15)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(294, 21)
        Me.txtCode.TabIndex = 9
        Me.txtCode.Value = ""
        '
        'RadPageView2
        '
        Me.RadPageView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView2.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView2.Controls.Add(Me.Pg_StockDetail)
        Me.RadPageView2.Location = New System.Drawing.Point(0, 94)
        Me.RadPageView2.Name = "RadPageView2"
        Me.RadPageView2.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView2.Size = New System.Drawing.Size(965, 269)
        Me.RadPageView2.TabIndex = 3
        Me.RadPageView2.Text = "RadPageView2"
        CType(Me.RadPageView2.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GvRoute)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(78.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(944, 221)
        Me.RadPageViewPage2.Text = "Route Detail"
        '
        'GvRoute
        '
        Me.GvRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.GvRoute.Cursor = System.Windows.Forms.Cursors.Default
        Me.GvRoute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GvRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GvRoute.ForeColor = System.Drawing.Color.Black
        Me.GvRoute.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GvRoute.Location = New System.Drawing.Point(0, 0)
        '
        'GvRoute
        '
        Me.GvRoute.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.GvRoute.MasterTemplate.AllowDeleteRow = False
        Me.GvRoute.MasterTemplate.ShowHeaderCellButtons = True
        Me.GvRoute.Name = "GvRoute"
        Me.GvRoute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GvRoute.ShowGroupPanel = False
        Me.GvRoute.ShowHeaderCellButtons = True
        Me.GvRoute.Size = New System.Drawing.Size(944, 221)
        Me.GvRoute.TabIndex = 1
        Me.GvRoute.TabStop = False
        Me.GvRoute.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gv1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(155.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(944, 221)
        Me.RadPageViewPage3.Text = "Documents and Deductions"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(944, 221)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'Pg_StockDetail
        '
        Me.Pg_StockDetail.Controls.Add(Me.SplitContainer3)
        Me.Pg_StockDetail.Controls.Add(Me.RadGroupBox2)
        Me.Pg_StockDetail.Controls.Add(Me.TxtBookSNF_per)
        Me.Pg_StockDetail.Controls.Add(Me.LblManualSNF_Per)
        Me.Pg_StockDetail.Controls.Add(Me.TxtManualSNF)
        Me.Pg_StockDetail.Controls.Add(Me.TxtManualFat_Per)
        Me.Pg_StockDetail.Controls.Add(Me.LblManualFAT_Per)
        Me.Pg_StockDetail.Controls.Add(Me.LblBookSNF_Per)
        Me.Pg_StockDetail.Controls.Add(Me.LblManualSNF)
        Me.Pg_StockDetail.Controls.Add(Me.TxtBookFat_Per)
        Me.Pg_StockDetail.Controls.Add(Me.LblBookFat_Per)
        Me.Pg_StockDetail.Controls.Add(Me.TxtManualFAT)
        Me.Pg_StockDetail.Controls.Add(Me.LblManualFAT)
        Me.Pg_StockDetail.Controls.Add(Me.TxtManualStock)
        Me.Pg_StockDetail.Controls.Add(Me.LblManualStock)
        Me.Pg_StockDetail.Controls.Add(Me.TxtActualSNF)
        Me.Pg_StockDetail.Controls.Add(Me.LblBookSNF)
        Me.Pg_StockDetail.Controls.Add(Me.TxtActualFat)
        Me.Pg_StockDetail.Controls.Add(Me.LblBookFAT)
        Me.Pg_StockDetail.Controls.Add(Me.TxtActualStock)
        Me.Pg_StockDetail.Controls.Add(Me.LblBookStock)
        Me.Pg_StockDetail.ItemSize = New System.Drawing.SizeF(76.0!, 28.0!)
        Me.Pg_StockDetail.Location = New System.Drawing.Point(10, 37)
        Me.Pg_StockDetail.Name = "Pg_StockDetail"
        Me.Pg_StockDetail.Size = New System.Drawing.Size(944, 221)
        Me.Pg_StockDetail.Text = "Stock Detail"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Location = New System.Drawing.Point(128, 63)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtCLR)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel13)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.TxtManualSNF_Per)
        Me.SplitContainer3.Size = New System.Drawing.Size(165, 20)
        Me.SplitContainer3.SplitterDistance = 77
        Me.SplitContainer3.TabIndex = 1062
        '
        'txtCLR
        '
        Me.txtCLR.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCLR.CalculationExpression = Nothing
        Me.txtCLR.DecimalPlaces = 1
        Me.txtCLR.FieldCode = Nothing
        Me.txtCLR.FieldDesc = Nothing
        Me.txtCLR.FieldMaxLength = 0
        Me.txtCLR.FieldName = Nothing
        Me.txtCLR.isCalculatedField = False
        Me.txtCLR.IsSourceFromTable = False
        Me.txtCLR.IsSourceFromValueList = False
        Me.txtCLR.IsUnique = False
        Me.txtCLR.Location = New System.Drawing.Point(33, 1)
        Me.txtCLR.MendatroryField = True
        Me.txtCLR.MyLinkLable1 = Me.MyLabel13
        Me.txtCLR.MyLinkLable2 = Nothing
        Me.txtCLR.Name = "txtCLR"
        Me.txtCLR.ReferenceFieldDesc = Nothing
        Me.txtCLR.ReferenceFieldName = Nothing
        Me.txtCLR.ReferenceTableName = Nothing
        Me.txtCLR.Size = New System.Drawing.Size(42, 20)
        Me.txtCLR.TabIndex = 19
        Me.txtCLR.Text = "0"
        Me.txtCLR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCLR.Value = 0R
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel13.TabIndex = 18
        Me.MyLabel13.Text = "CLR"
        '
        'TxtManualSNF_Per
        '
        Me.TxtManualSNF_Per.BackColor = System.Drawing.Color.White
        Me.TxtManualSNF_Per.CalculationExpression = Nothing
        Me.TxtManualSNF_Per.DecimalPlaces = 3
        Me.TxtManualSNF_Per.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtManualSNF_Per.FieldCode = Nothing
        Me.TxtManualSNF_Per.FieldDesc = Nothing
        Me.TxtManualSNF_Per.FieldMaxLength = 0
        Me.TxtManualSNF_Per.FieldName = Nothing
        Me.TxtManualSNF_Per.isCalculatedField = False
        Me.TxtManualSNF_Per.IsSourceFromTable = False
        Me.TxtManualSNF_Per.IsSourceFromValueList = False
        Me.TxtManualSNF_Per.IsUnique = False
        Me.TxtManualSNF_Per.Location = New System.Drawing.Point(0, 0)
        Me.TxtManualSNF_Per.MendatroryField = True
        Me.TxtManualSNF_Per.MyLinkLable1 = Me.LblManualSNF_Per
        Me.TxtManualSNF_Per.MyLinkLable2 = Nothing
        Me.TxtManualSNF_Per.Name = "TxtManualSNF_Per"
        Me.TxtManualSNF_Per.ReferenceFieldDesc = Nothing
        Me.TxtManualSNF_Per.ReferenceFieldName = Nothing
        Me.TxtManualSNF_Per.ReferenceTableName = Nothing
        Me.TxtManualSNF_Per.Size = New System.Drawing.Size(84, 20)
        Me.TxtManualSNF_Per.TabIndex = 1059
        Me.TxtManualSNF_Per.Text = "0"
        Me.TxtManualSNF_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualSNF_Per.Value = 0R
        '
        'LblManualSNF_Per
        '
        Me.LblManualSNF_Per.FieldName = Nothing
        Me.LblManualSNF_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualSNF_Per.Location = New System.Drawing.Point(27, 67)
        Me.LblManualSNF_Per.Name = "LblManualSNF_Per"
        Me.LblManualSNF_Per.Size = New System.Drawing.Size(87, 16)
        Me.LblManualSNF_Per.TabIndex = 1060
        Me.LblManualSNF_Per.Text = "Closing SNF(%)"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.LblFirstWeightmentSample)
        Me.RadGroupBox2.Controls.Add(Me.LblLastsampleFATTime)
        Me.RadGroupBox2.Controls.Add(Me.LblShiftOpeningTime)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Shift Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(4, 126)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(937, 113)
        Me.RadGroupBox2.TabIndex = 1061
        Me.RadGroupBox2.Text = "Shift Details"
        '
        'LblFirstWeightmentSample
        '
        Me.LblFirstWeightmentSample.AutoSize = False
        Me.LblFirstWeightmentSample.BorderVisible = True
        Me.LblFirstWeightmentSample.FieldName = Nothing
        Me.LblFirstWeightmentSample.Location = New System.Drawing.Point(218, 51)
        Me.LblFirstWeightmentSample.Name = "LblFirstWeightmentSample"
        Me.LblFirstWeightmentSample.Size = New System.Drawing.Size(344, 19)
        Me.LblFirstWeightmentSample.TabIndex = 1067
        Me.LblFirstWeightmentSample.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblLastsampleFATTime
        '
        Me.LblLastsampleFATTime.AutoSize = False
        Me.LblLastsampleFATTime.BorderVisible = True
        Me.LblLastsampleFATTime.FieldName = Nothing
        Me.LblLastsampleFATTime.Location = New System.Drawing.Point(218, 78)
        Me.LblLastsampleFATTime.Name = "LblLastsampleFATTime"
        Me.LblLastsampleFATTime.Size = New System.Drawing.Size(344, 19)
        Me.LblLastsampleFATTime.TabIndex = 1067
        Me.LblLastsampleFATTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblShiftOpeningTime
        '
        Me.LblShiftOpeningTime.AutoSize = False
        Me.LblShiftOpeningTime.BorderVisible = True
        Me.LblShiftOpeningTime.FieldName = Nothing
        Me.LblShiftOpeningTime.Location = New System.Drawing.Point(218, 24)
        Me.LblShiftOpeningTime.Name = "LblShiftOpeningTime"
        Me.LblShiftOpeningTime.Size = New System.Drawing.Size(344, 19)
        Me.LblShiftOpeningTime.TabIndex = 1066
        Me.LblShiftOpeningTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(23, 79)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(148, 16)
        Me.MyLabel5.TabIndex = 1065
        Me.MyLabel5.Text = "Last Sample FAT/SNF Time"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(23, 52)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(159, 16)
        Me.MyLabel2.TabIndex = 1063
        Me.MyLabel2.Text = "First Sample Weighment Time"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(23, 25)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel1.TabIndex = 1061
        Me.MyLabel1.Text = "Shift Opening Time"
        '
        'TxtBookSNF_per
        '
        Me.TxtBookSNF_per.BackColor = System.Drawing.Color.White
        Me.TxtBookSNF_per.CalculationExpression = Nothing
        Me.TxtBookSNF_per.DecimalPlaces = 3
        Me.TxtBookSNF_per.FieldCode = Nothing
        Me.TxtBookSNF_per.FieldDesc = Nothing
        Me.TxtBookSNF_per.FieldMaxLength = 0
        Me.TxtBookSNF_per.FieldName = Nothing
        Me.TxtBookSNF_per.isCalculatedField = False
        Me.TxtBookSNF_per.IsSourceFromTable = False
        Me.TxtBookSNF_per.IsSourceFromValueList = False
        Me.TxtBookSNF_per.IsUnique = False
        Me.TxtBookSNF_per.Location = New System.Drawing.Point(128, 377)
        Me.TxtBookSNF_per.MendatroryField = False
        Me.TxtBookSNF_per.MyLinkLable1 = Me.LblBookSNF_Per
        Me.TxtBookSNF_per.MyLinkLable2 = Nothing
        Me.TxtBookSNF_per.Name = "TxtBookSNF_per"
        Me.TxtBookSNF_per.ReadOnly = True
        Me.TxtBookSNF_per.ReferenceFieldDesc = Nothing
        Me.TxtBookSNF_per.ReferenceFieldName = Nothing
        Me.TxtBookSNF_per.ReferenceTableName = Nothing
        Me.TxtBookSNF_per.Size = New System.Drawing.Size(165, 20)
        Me.TxtBookSNF_per.TabIndex = 1051
        Me.TxtBookSNF_per.Text = "0"
        Me.TxtBookSNF_per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtBookSNF_per.Value = 0R
        Me.TxtBookSNF_per.Visible = False
        '
        'LblBookSNF_Per
        '
        Me.LblBookSNF_Per.FieldName = Nothing
        Me.LblBookSNF_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookSNF_Per.Location = New System.Drawing.Point(27, 379)
        Me.LblBookSNF_Per.Name = "LblBookSNF_Per"
        Me.LblBookSNF_Per.Size = New System.Drawing.Size(75, 16)
        Me.LblBookSNF_Per.TabIndex = 1053
        Me.LblBookSNF_Per.Text = "Book SNF(%)"
        Me.LblBookSNF_Per.Visible = False
        '
        'TxtManualSNF
        '
        Me.TxtManualSNF.BackColor = System.Drawing.Color.White
        Me.TxtManualSNF.CalculationExpression = Nothing
        Me.TxtManualSNF.DecimalPlaces = 3
        Me.TxtManualSNF.Enabled = False
        Me.TxtManualSNF.FieldCode = Nothing
        Me.TxtManualSNF.FieldDesc = Nothing
        Me.TxtManualSNF.FieldMaxLength = 0
        Me.TxtManualSNF.FieldName = Nothing
        Me.TxtManualSNF.isCalculatedField = False
        Me.TxtManualSNF.IsSourceFromTable = False
        Me.TxtManualSNF.IsSourceFromValueList = False
        Me.TxtManualSNF.IsUnique = False
        Me.TxtManualSNF.Location = New System.Drawing.Point(409, 65)
        Me.TxtManualSNF.MendatroryField = True
        Me.TxtManualSNF.MyLinkLable1 = Me.LblManualSNF
        Me.TxtManualSNF.MyLinkLable2 = Nothing
        Me.TxtManualSNF.Name = "TxtManualSNF"
        Me.TxtManualSNF.ReferenceFieldDesc = Nothing
        Me.TxtManualSNF.ReferenceFieldName = Nothing
        Me.TxtManualSNF.ReferenceTableName = Nothing
        Me.TxtManualSNF.Size = New System.Drawing.Size(157, 20)
        Me.TxtManualSNF.TabIndex = 1055
        Me.TxtManualSNF.Text = "0"
        Me.TxtManualSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualSNF.Value = 0R
        '
        'LblManualSNF
        '
        Me.LblManualSNF.FieldName = Nothing
        Me.LblManualSNF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualSNF.Location = New System.Drawing.Point(303, 67)
        Me.LblManualSNF.Name = "LblManualSNF"
        Me.LblManualSNF.Size = New System.Drawing.Size(94, 16)
        Me.LblManualSNF.TabIndex = 1056
        Me.LblManualSNF.Text = "Closing SNF(KG)"
        '
        'TxtManualFat_Per
        '
        Me.TxtManualFat_Per.BackColor = System.Drawing.Color.White
        Me.TxtManualFat_Per.CalculationExpression = Nothing
        Me.TxtManualFat_Per.DecimalPlaces = 3
        Me.TxtManualFat_Per.FieldCode = Nothing
        Me.TxtManualFat_Per.FieldDesc = Nothing
        Me.TxtManualFat_Per.FieldMaxLength = 0
        Me.TxtManualFat_Per.FieldName = Nothing
        Me.TxtManualFat_Per.isCalculatedField = False
        Me.TxtManualFat_Per.IsSourceFromTable = False
        Me.TxtManualFat_Per.IsSourceFromValueList = False
        Me.TxtManualFat_Per.IsUnique = False
        Me.TxtManualFat_Per.Location = New System.Drawing.Point(128, 40)
        Me.TxtManualFat_Per.MendatroryField = True
        Me.TxtManualFat_Per.MyLinkLable1 = Me.LblManualFAT_Per
        Me.TxtManualFat_Per.MyLinkLable2 = Nothing
        Me.TxtManualFat_Per.Name = "TxtManualFat_Per"
        Me.TxtManualFat_Per.ReferenceFieldDesc = Nothing
        Me.TxtManualFat_Per.ReferenceFieldName = Nothing
        Me.TxtManualFat_Per.ReferenceTableName = Nothing
        Me.TxtManualFat_Per.Size = New System.Drawing.Size(165, 20)
        Me.TxtManualFat_Per.TabIndex = 1057
        Me.TxtManualFat_Per.Text = "0"
        Me.TxtManualFat_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualFat_Per.Value = 0R
        '
        'LblManualFAT_Per
        '
        Me.LblManualFAT_Per.FieldName = Nothing
        Me.LblManualFAT_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualFAT_Per.Location = New System.Drawing.Point(27, 42)
        Me.LblManualFAT_Per.Name = "LblManualFAT_Per"
        Me.LblManualFAT_Per.Size = New System.Drawing.Size(86, 16)
        Me.LblManualFAT_Per.TabIndex = 1058
        Me.LblManualFAT_Per.Text = "Closing FAT(%)"
        '
        'TxtBookFat_Per
        '
        Me.TxtBookFat_Per.BackColor = System.Drawing.Color.White
        Me.TxtBookFat_Per.CalculationExpression = Nothing
        Me.TxtBookFat_Per.DecimalPlaces = 3
        Me.TxtBookFat_Per.FieldCode = Nothing
        Me.TxtBookFat_Per.FieldDesc = Nothing
        Me.TxtBookFat_Per.FieldMaxLength = 0
        Me.TxtBookFat_Per.FieldName = Nothing
        Me.TxtBookFat_Per.isCalculatedField = False
        Me.TxtBookFat_Per.IsSourceFromTable = False
        Me.TxtBookFat_Per.IsSourceFromValueList = False
        Me.TxtBookFat_Per.IsUnique = False
        Me.TxtBookFat_Per.Location = New System.Drawing.Point(128, 354)
        Me.TxtBookFat_Per.MendatroryField = False
        Me.TxtBookFat_Per.MyLinkLable1 = Me.LblBookFat_Per
        Me.TxtBookFat_Per.MyLinkLable2 = Nothing
        Me.TxtBookFat_Per.Name = "TxtBookFat_Per"
        Me.TxtBookFat_Per.ReadOnly = True
        Me.TxtBookFat_Per.ReferenceFieldDesc = Nothing
        Me.TxtBookFat_Per.ReferenceFieldName = Nothing
        Me.TxtBookFat_Per.ReferenceTableName = Nothing
        Me.TxtBookFat_Per.Size = New System.Drawing.Size(165, 20)
        Me.TxtBookFat_Per.TabIndex = 1048
        Me.TxtBookFat_Per.Text = "0"
        Me.TxtBookFat_Per.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtBookFat_Per.Value = 0R
        Me.TxtBookFat_Per.Visible = False
        '
        'LblBookFat_Per
        '
        Me.LblBookFat_Per.FieldName = Nothing
        Me.LblBookFat_Per.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookFat_Per.Location = New System.Drawing.Point(27, 356)
        Me.LblBookFat_Per.Name = "LblBookFat_Per"
        Me.LblBookFat_Per.Size = New System.Drawing.Size(74, 16)
        Me.LblBookFat_Per.TabIndex = 1050
        Me.LblBookFat_Per.Text = "Book FAT(%)"
        Me.LblBookFat_Per.Visible = False
        '
        'TxtManualFAT
        '
        Me.TxtManualFAT.BackColor = System.Drawing.Color.White
        Me.TxtManualFAT.CalculationExpression = Nothing
        Me.TxtManualFAT.DecimalPlaces = 3
        Me.TxtManualFAT.Enabled = False
        Me.TxtManualFAT.FieldCode = Nothing
        Me.TxtManualFAT.FieldDesc = Nothing
        Me.TxtManualFAT.FieldMaxLength = 0
        Me.TxtManualFAT.FieldName = Nothing
        Me.TxtManualFAT.isCalculatedField = False
        Me.TxtManualFAT.IsSourceFromTable = False
        Me.TxtManualFAT.IsSourceFromValueList = False
        Me.TxtManualFAT.IsUnique = False
        Me.TxtManualFAT.Location = New System.Drawing.Point(409, 40)
        Me.TxtManualFAT.MendatroryField = True
        Me.TxtManualFAT.MyLinkLable1 = Me.LblManualFAT
        Me.TxtManualFAT.MyLinkLable2 = Nothing
        Me.TxtManualFAT.Name = "TxtManualFAT"
        Me.TxtManualFAT.ReferenceFieldDesc = Nothing
        Me.TxtManualFAT.ReferenceFieldName = Nothing
        Me.TxtManualFAT.ReferenceTableName = Nothing
        Me.TxtManualFAT.Size = New System.Drawing.Size(157, 20)
        Me.TxtManualFAT.TabIndex = 1052
        Me.TxtManualFAT.Text = "0"
        Me.TxtManualFAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualFAT.Value = 0R
        '
        'LblManualFAT
        '
        Me.LblManualFAT.FieldName = Nothing
        Me.LblManualFAT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualFAT.Location = New System.Drawing.Point(303, 42)
        Me.LblManualFAT.Name = "LblManualFAT"
        Me.LblManualFAT.Size = New System.Drawing.Size(92, 16)
        Me.LblManualFAT.TabIndex = 1054
        Me.LblManualFAT.Text = "Closing FAT(KG)"
        '
        'TxtManualStock
        '
        Me.TxtManualStock.BackColor = System.Drawing.Color.White
        Me.TxtManualStock.CalculationExpression = Nothing
        Me.TxtManualStock.DecimalPlaces = 3
        Me.TxtManualStock.FieldCode = Nothing
        Me.TxtManualStock.FieldDesc = Nothing
        Me.TxtManualStock.FieldMaxLength = 0
        Me.TxtManualStock.FieldName = Nothing
        Me.TxtManualStock.isCalculatedField = False
        Me.TxtManualStock.IsSourceFromTable = False
        Me.TxtManualStock.IsSourceFromValueList = False
        Me.TxtManualStock.IsUnique = False
        Me.TxtManualStock.Location = New System.Drawing.Point(128, 16)
        Me.TxtManualStock.MendatroryField = True
        Me.TxtManualStock.MyLinkLable1 = Me.LblManualStock
        Me.TxtManualStock.MyLinkLable2 = Nothing
        Me.TxtManualStock.Name = "TxtManualStock"
        Me.TxtManualStock.ReferenceFieldDesc = Nothing
        Me.TxtManualStock.ReferenceFieldName = Nothing
        Me.TxtManualStock.ReferenceTableName = Nothing
        Me.TxtManualStock.Size = New System.Drawing.Size(165, 20)
        Me.TxtManualStock.TabIndex = 1047
        Me.TxtManualStock.Text = "0"
        Me.TxtManualStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtManualStock.Value = 0R
        '
        'LblManualStock
        '
        Me.LblManualStock.FieldName = Nothing
        Me.LblManualStock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualStock.Location = New System.Drawing.Point(27, 18)
        Me.LblManualStock.Name = "LblManualStock"
        Me.LblManualStock.Size = New System.Drawing.Size(99, 16)
        Me.LblManualStock.TabIndex = 1049
        Me.LblManualStock.Text = "Closing Stock(KG)"
        '
        'TxtActualSNF
        '
        Me.TxtActualSNF.BackColor = System.Drawing.Color.White
        Me.TxtActualSNF.CalculationExpression = Nothing
        Me.TxtActualSNF.DecimalPlaces = 3
        Me.TxtActualSNF.FieldCode = Nothing
        Me.TxtActualSNF.FieldDesc = Nothing
        Me.TxtActualSNF.FieldMaxLength = 0
        Me.TxtActualSNF.FieldName = Nothing
        Me.TxtActualSNF.isCalculatedField = False
        Me.TxtActualSNF.IsSourceFromTable = False
        Me.TxtActualSNF.IsSourceFromValueList = False
        Me.TxtActualSNF.IsUnique = False
        Me.TxtActualSNF.Location = New System.Drawing.Point(409, 377)
        Me.TxtActualSNF.MendatroryField = False
        Me.TxtActualSNF.MyLinkLable1 = Me.LblBookSNF
        Me.TxtActualSNF.MyLinkLable2 = Nothing
        Me.TxtActualSNF.Name = "TxtActualSNF"
        Me.TxtActualSNF.ReadOnly = True
        Me.TxtActualSNF.ReferenceFieldDesc = Nothing
        Me.TxtActualSNF.ReferenceFieldName = Nothing
        Me.TxtActualSNF.ReferenceTableName = Nothing
        Me.TxtActualSNF.Size = New System.Drawing.Size(157, 20)
        Me.TxtActualSNF.TabIndex = 1045
        Me.TxtActualSNF.Text = "0"
        Me.TxtActualSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtActualSNF.Value = 0R
        Me.TxtActualSNF.Visible = False
        '
        'LblBookSNF
        '
        Me.LblBookSNF.FieldName = Nothing
        Me.LblBookSNF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookSNF.Location = New System.Drawing.Point(303, 379)
        Me.LblBookSNF.Name = "LblBookSNF"
        Me.LblBookSNF.Size = New System.Drawing.Size(82, 16)
        Me.LblBookSNF.TabIndex = 1046
        Me.LblBookSNF.Text = "Book SNF(KG)"
        Me.LblBookSNF.Visible = False
        '
        'TxtActualFat
        '
        Me.TxtActualFat.BackColor = System.Drawing.Color.White
        Me.TxtActualFat.CalculationExpression = Nothing
        Me.TxtActualFat.DecimalPlaces = 3
        Me.TxtActualFat.FieldCode = Nothing
        Me.TxtActualFat.FieldDesc = Nothing
        Me.TxtActualFat.FieldMaxLength = 0
        Me.TxtActualFat.FieldName = Nothing
        Me.TxtActualFat.isCalculatedField = False
        Me.TxtActualFat.IsSourceFromTable = False
        Me.TxtActualFat.IsSourceFromValueList = False
        Me.TxtActualFat.IsUnique = False
        Me.TxtActualFat.Location = New System.Drawing.Point(409, 354)
        Me.TxtActualFat.MendatroryField = False
        Me.TxtActualFat.MyLinkLable1 = Me.LblBookFAT
        Me.TxtActualFat.MyLinkLable2 = Nothing
        Me.TxtActualFat.Name = "TxtActualFat"
        Me.TxtActualFat.ReadOnly = True
        Me.TxtActualFat.ReferenceFieldDesc = Nothing
        Me.TxtActualFat.ReferenceFieldName = Nothing
        Me.TxtActualFat.ReferenceTableName = Nothing
        Me.TxtActualFat.Size = New System.Drawing.Size(157, 20)
        Me.TxtActualFat.TabIndex = 1043
        Me.TxtActualFat.Text = "0"
        Me.TxtActualFat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtActualFat.Value = 0R
        Me.TxtActualFat.Visible = False
        '
        'LblBookFAT
        '
        Me.LblBookFAT.FieldName = Nothing
        Me.LblBookFAT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookFAT.Location = New System.Drawing.Point(303, 356)
        Me.LblBookFAT.Name = "LblBookFAT"
        Me.LblBookFAT.Size = New System.Drawing.Size(80, 16)
        Me.LblBookFAT.TabIndex = 1044
        Me.LblBookFAT.Text = "Book FAT(KG)"
        Me.LblBookFAT.Visible = False
        '
        'TxtActualStock
        '
        Me.TxtActualStock.BackColor = System.Drawing.Color.White
        Me.TxtActualStock.CalculationExpression = Nothing
        Me.TxtActualStock.DecimalPlaces = 3
        Me.TxtActualStock.FieldCode = Nothing
        Me.TxtActualStock.FieldDesc = Nothing
        Me.TxtActualStock.FieldMaxLength = 0
        Me.TxtActualStock.FieldName = Nothing
        Me.TxtActualStock.isCalculatedField = False
        Me.TxtActualStock.IsSourceFromTable = False
        Me.TxtActualStock.IsSourceFromValueList = False
        Me.TxtActualStock.IsUnique = False
        Me.TxtActualStock.Location = New System.Drawing.Point(128, 331)
        Me.TxtActualStock.MendatroryField = False
        Me.TxtActualStock.MyLinkLable1 = Me.LblBookStock
        Me.TxtActualStock.MyLinkLable2 = Nothing
        Me.TxtActualStock.Name = "TxtActualStock"
        Me.TxtActualStock.ReadOnly = True
        Me.TxtActualStock.ReferenceFieldDesc = Nothing
        Me.TxtActualStock.ReferenceFieldName = Nothing
        Me.TxtActualStock.ReferenceTableName = Nothing
        Me.TxtActualStock.Size = New System.Drawing.Size(165, 20)
        Me.TxtActualStock.TabIndex = 1041
        Me.TxtActualStock.Text = "0"
        Me.TxtActualStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtActualStock.Value = 0R
        Me.TxtActualStock.Visible = False
        '
        'LblBookStock
        '
        Me.LblBookStock.FieldName = Nothing
        Me.LblBookStock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookStock.Location = New System.Drawing.Point(27, 333)
        Me.LblBookStock.Name = "LblBookStock"
        Me.LblBookStock.Size = New System.Drawing.Size(87, 16)
        Me.LblBookStock.TabIndex = 1042
        Me.LblBookStock.Text = "Book Stock(KG)"
        Me.LblBookStock.Visible = False
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(967, 365)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(967, 365)
        Me.UcCustomFields1.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(911, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(15, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(87, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        Me.btndelete.Visible = False
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(988, 20)
        Me.rdmenufile.TabIndex = 67
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnSaveLayout, Me.BtnDeleteLayout, Me.btnemailsetting, Me.btnemailsmssettingforvsp})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'BtnSaveLayout
        '
        Me.BtnSaveLayout.AccessibleDescription = "Save Layout"
        Me.BtnSaveLayout.AccessibleName = "Save Layout"
        Me.BtnSaveLayout.Name = "BtnSaveLayout"
        Me.BtnSaveLayout.Text = "Save Layout"
        '
        'BtnDeleteLayout
        '
        Me.BtnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.BtnDeleteLayout.AccessibleName = "Delete Layout"
        Me.BtnDeleteLayout.Name = "BtnDeleteLayout"
        Me.BtnDeleteLayout.Text = "Delete Layout"
        '
        'btnemailsetting
        '
        Me.btnemailsetting.AccessibleDescription = "RadMenuItem1"
        Me.btnemailsetting.AccessibleName = "RadMenuItem1"
        Me.btnemailsetting.Name = "btnemailsetting"
        Me.btnemailsetting.Text = "Email & SMS Setting"
        '
        'btnemailsmssettingforvsp
        '
        Me.btnemailsmssettingforvsp.AccessibleDescription = "Email & SMS Setting For VSP"
        Me.btnemailsmssettingforvsp.AccessibleName = "Email & SMS Setting For VSP"
        Me.btnemailsmssettingforvsp.Name = "btnemailsmssettingforvsp"
        Me.btnemailsmssettingforvsp.Text = "Email & SMS Setting For Secretary"
        '
        'frmMilkShiftClosingMCC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 469)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmMilkShiftClosingMCC"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Shift End"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkAskSiloatShiftEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel70, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSiloInLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSiloInLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnMilkTruckSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndMccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpMCCDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView2.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GvRoute.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvRoute, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pg_StockDetail.ResumeLayout(False)
        Me.Pg_StockDetail.PerformLayout()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.txtCLR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.LblFirstWeightmentSample, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLastsampleFATTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblShiftOpeningTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBookSNF_per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookSNF_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualFat_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualFAT_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtBookFat_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookFat_Per, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtManualStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblManualStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtActualSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtActualFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtActualStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents DtpTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents DtpMCCDate As common.Controls.MyDateTimePicker
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents fndMccCode As common.Controls.MyTextBox
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView2 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvUOM As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents GvRoute As common.UserControls.MyRadGridView
    Friend WithEvents BtnMilkTruckSheet As Telerik.WinControls.UI.RadButton
    Friend WithEvents Pg_StockDetail As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents TxtManualSNF_Per As common.MyNumBox
    Friend WithEvents LblManualSNF_Per As common.Controls.MyLabel
    Friend WithEvents TxtBookSNF_per As common.MyNumBox
    Friend WithEvents LblBookSNF_Per As common.Controls.MyLabel
    Friend WithEvents TxtManualSNF As common.MyNumBox
    Friend WithEvents LblManualSNF As common.Controls.MyLabel
    Friend WithEvents TxtManualFat_Per As common.MyNumBox
    Friend WithEvents LblManualFAT_Per As common.Controls.MyLabel
    Friend WithEvents TxtBookFat_Per As common.MyNumBox
    Friend WithEvents LblBookFat_Per As common.Controls.MyLabel
    Friend WithEvents TxtManualFAT As common.MyNumBox
    Friend WithEvents LblManualFAT As common.Controls.MyLabel
    Friend WithEvents TxtManualStock As common.MyNumBox
    Friend WithEvents LblManualStock As common.Controls.MyLabel
    Friend WithEvents TxtActualSNF As common.MyNumBox
    Friend WithEvents LblBookSNF As common.Controls.MyLabel
    Friend WithEvents TxtActualFat As common.MyNumBox
    Friend WithEvents LblBookFAT As common.Controls.MyLabel
    Friend WithEvents TxtActualStock As common.MyNumBox
    Friend WithEvents LblBookStock As common.Controls.MyLabel
    Friend WithEvents fndMcc As common.UserControls.txtFinder
    Friend WithEvents btnemailsetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents LblShiftOpeningTime As common.Controls.MyLabel
    Friend WithEvents LblFirstWeightmentSample As common.Controls.MyLabel
    Friend WithEvents LblLastsampleFATTime As common.Controls.MyLabel
    Friend WithEvents btnemailsmssettingforvsp As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCLR As common.MyNumBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtSiloInLoc As common.Controls.MyTextBox
    Friend WithEvents fndSiloInLoc As common.UserControls.txtFinder
    Friend WithEvents lblSiloInLocation As common.Controls.MyLabel
    Friend WithEvents fndAutoInLoc As common.UserControls.txtFinder
    Friend WithEvents MyLabel70 As common.Controls.MyLabel
    Friend WithEvents chkAskSiloatShiftEnd As common.Controls.MyCheckBox
End Class

