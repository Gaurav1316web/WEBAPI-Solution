Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayHeadDefinitions
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
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TxtMaxHRA = New common.MyNumBox()
        Me.lblMaxHRA = New common.Controls.MyLabel()
        Me.chkCreateAPInvoice = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndNatureOfDeduction = New common.UserControls.txtFinder()
        Me.lblNatureOfDeduction = New common.Controls.MyTextBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.chkDoNotIncludeInGrossSalaryForOverTime = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkTDSExempted = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFullnFinal = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndEmpGLAcc = New common.UserControls.txtFinder()
        Me.txtEmpGLAcc = New common.Controls.MyTextBox()
        Me.lblEmpGLACC = New common.Controls.MyLabel()
        Me.cboArrearType = New common.Controls.MyComboBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtPrintSerialNo = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblPrintSerialNo = New common.Controls.MyLabel()
        Me.txtSerialNo = New common.Controls.MyTextBox()
        Me.lblSerailNo = New common.Controls.MyLabel()
        Me.chkIsEarning = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndGLAccount = New common.UserControls.txtFinder()
        Me.txtGLAccountDesc = New common.Controls.MyTextBox()
        Me.ChkHiddenComponents = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.txtPrintName = New common.Controls.MyTextBox()
        Me.cboRoundOffType = New common.Controls.MyComboBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboPayHeadCategory = New common.Controls.MyComboBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.cboCalcBasis = New common.Controls.MyComboBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtPayHeadName = New common.Controls.MyTextBox()
        Me.CboPayHeadType = New common.Controls.MyComboBox()
        Me.cboPeriodicity = New common.Controls.MyComboBox()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.TxtMaxHRA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMaxHRA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateAPInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNatureOfDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDoNotIncludeInGrossSalaryForOverTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTDSExempted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFullnFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpGLAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpGLACC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboArrearType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrintSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSerailNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsEarning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGLAccountDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkHiddenComponents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrintName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboRoundOffType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPayHeadCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCalcBasis, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayHeadName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboPayHeadType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPeriodicity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(327, 12)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 18)
        Me.btnnew.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(866, 480)
        Me.SplitContainer1.SplitterDistance = 431
        Me.SplitContainer1.TabIndex = 1
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(866, 20)
        Me.RadMenu2.TabIndex = 1
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuItemImport, Me.MenuItemExport, Me.MenuItemClose})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'MenuItemImport
        '
        Me.MenuItemImport.Name = "MenuItemImport"
        Me.MenuItemImport.Text = "Import"
        '
        'MenuItemExport
        '
        Me.MenuItemExport.Name = "MenuItemExport"
        Me.MenuItemExport.Text = "Export"
        '
        'MenuItemClose
        '
        Me.MenuItemClose.Name = "MenuItemClose"
        Me.MenuItemClose.Text = "Close"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.TxtMaxHRA)
        Me.RadGroupBox1.Controls.Add(Me.lblMaxHRA)
        Me.RadGroupBox1.Controls.Add(Me.chkCreateAPInvoice)
        Me.RadGroupBox1.Controls.Add(Me.fndNatureOfDeduction)
        Me.RadGroupBox1.Controls.Add(Me.lblNatureOfDeduction)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.chkDoNotIncludeInGrossSalaryForOverTime)
        Me.RadGroupBox1.Controls.Add(Me.chkTDSExempted)
        Me.RadGroupBox1.Controls.Add(Me.chkFullnFinal)
        Me.RadGroupBox1.Controls.Add(Me.fndEmpGLAcc)
        Me.RadGroupBox1.Controls.Add(Me.txtEmpGLAcc)
        Me.RadGroupBox1.Controls.Add(Me.lblEmpGLACC)
        Me.RadGroupBox1.Controls.Add(Me.cboArrearType)
        Me.RadGroupBox1.Controls.Add(Me.txtPrintSerialNo)
        Me.RadGroupBox1.Controls.Add(Me.lblPrintSerialNo)
        Me.RadGroupBox1.Controls.Add(Me.txtSerialNo)
        Me.RadGroupBox1.Controls.Add(Me.lblSerailNo)
        Me.RadGroupBox1.Controls.Add(Me.chkIsEarning)
        Me.RadGroupBox1.Controls.Add(Me.fndGLAccount)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtGLAccountDesc)
        Me.RadGroupBox1.Controls.Add(Me.ChkHiddenComponents)
        Me.RadGroupBox1.Controls.Add(Me.lblempcode)
        Me.RadGroupBox1.Controls.Add(Me.txtPrintName)
        Me.RadGroupBox1.Controls.Add(Me.cboRoundOffType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.cboPayHeadCategory)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.cboCalcBasis)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtPayHeadName)
        Me.RadGroupBox1.Controls.Add(Me.CboPayHeadType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.cboPeriodicity)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 21)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(860, 407)
        Me.RadGroupBox1.TabIndex = 0
        '
        'TxtMaxHRA
        '
        Me.TxtMaxHRA.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtMaxHRA.CalculationExpression = Nothing
        Me.TxtMaxHRA.DecimalPlaces = 2
        Me.TxtMaxHRA.FieldCode = Nothing
        Me.TxtMaxHRA.FieldDesc = Nothing
        Me.TxtMaxHRA.FieldMaxLength = 0
        Me.TxtMaxHRA.FieldName = Nothing
        Me.TxtMaxHRA.isCalculatedField = False
        Me.TxtMaxHRA.IsSourceFromTable = False
        Me.TxtMaxHRA.IsSourceFromValueList = False
        Me.TxtMaxHRA.IsUnique = False
        Me.TxtMaxHRA.Location = New System.Drawing.Point(651, 108)
        Me.TxtMaxHRA.MendatroryField = True
        Me.TxtMaxHRA.MyLinkLable1 = Nothing
        Me.TxtMaxHRA.MyLinkLable2 = Nothing
        Me.TxtMaxHRA.Name = "TxtMaxHRA"
        Me.TxtMaxHRA.ReferenceFieldDesc = Nothing
        Me.TxtMaxHRA.ReferenceFieldName = Nothing
        Me.TxtMaxHRA.ReferenceTableName = Nothing
        Me.TxtMaxHRA.Size = New System.Drawing.Size(117, 20)
        Me.TxtMaxHRA.TabIndex = 307
        Me.TxtMaxHRA.Text = "0"
        Me.TxtMaxHRA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMaxHRA.Value = 0R
        '
        'lblMaxHRA
        '
        Me.lblMaxHRA.FieldName = Nothing
        Me.lblMaxHRA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxHRA.Location = New System.Drawing.Point(563, 109)
        Me.lblMaxHRA.Name = "lblMaxHRA"
        Me.lblMaxHRA.Size = New System.Drawing.Size(82, 16)
        Me.lblMaxHRA.TabIndex = 146
        Me.lblMaxHRA.Text = "Maximum HRA"
        '
        'chkCreateAPInvoice
        '
        Me.chkCreateAPInvoice.Location = New System.Drawing.Point(337, 356)
        Me.chkCreateAPInvoice.Name = "chkCreateAPInvoice"
        Me.chkCreateAPInvoice.Size = New System.Drawing.Size(108, 18)
        Me.chkCreateAPInvoice.TabIndex = 145
        Me.chkCreateAPInvoice.Text = "Create Ap Invoice"
        '
        'fndNatureOfDeduction
        '
        Me.fndNatureOfDeduction.CalculationExpression = Nothing
        Me.fndNatureOfDeduction.FieldCode = Nothing
        Me.fndNatureOfDeduction.FieldDesc = Nothing
        Me.fndNatureOfDeduction.FieldMaxLength = 0
        Me.fndNatureOfDeduction.FieldName = Nothing
        Me.fndNatureOfDeduction.isCalculatedField = False
        Me.fndNatureOfDeduction.IsSourceFromTable = False
        Me.fndNatureOfDeduction.IsSourceFromValueList = False
        Me.fndNatureOfDeduction.IsUnique = False
        Me.fndNatureOfDeduction.Location = New System.Drawing.Point(135, 322)
        Me.fndNatureOfDeduction.MendatroryField = False
        Me.fndNatureOfDeduction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndNatureOfDeduction.MyLinkLable1 = Nothing
        Me.fndNatureOfDeduction.MyLinkLable2 = Nothing
        Me.fndNatureOfDeduction.MyReadOnly = False
        Me.fndNatureOfDeduction.MyShowMasterFormButton = False
        Me.fndNatureOfDeduction.Name = "fndNatureOfDeduction"
        Me.fndNatureOfDeduction.ReferenceFieldDesc = Nothing
        Me.fndNatureOfDeduction.ReferenceFieldName = Nothing
        Me.fndNatureOfDeduction.ReferenceTableName = Nothing
        Me.fndNatureOfDeduction.Size = New System.Drawing.Size(211, 18)
        Me.fndNatureOfDeduction.TabIndex = 144
        Me.fndNatureOfDeduction.Value = ""
        '
        'lblNatureOfDeduction
        '
        Me.lblNatureOfDeduction.CalculationExpression = Nothing
        Me.lblNatureOfDeduction.FieldCode = Nothing
        Me.lblNatureOfDeduction.FieldDesc = Nothing
        Me.lblNatureOfDeduction.FieldMaxLength = 0
        Me.lblNatureOfDeduction.FieldName = Nothing
        Me.lblNatureOfDeduction.isCalculatedField = False
        Me.lblNatureOfDeduction.IsSourceFromTable = False
        Me.lblNatureOfDeduction.IsSourceFromValueList = False
        Me.lblNatureOfDeduction.IsUnique = False
        Me.lblNatureOfDeduction.Location = New System.Drawing.Point(349, 321)
        Me.lblNatureOfDeduction.MaxLength = 55
        Me.lblNatureOfDeduction.MendatroryField = False
        Me.lblNatureOfDeduction.MyLinkLable1 = Me.RadLabel1
        Me.lblNatureOfDeduction.MyLinkLable2 = Nothing
        Me.lblNatureOfDeduction.Name = "lblNatureOfDeduction"
        Me.lblNatureOfDeduction.ReadOnly = True
        Me.lblNatureOfDeduction.ReferenceFieldDesc = Nothing
        Me.lblNatureOfDeduction.ReferenceFieldName = Nothing
        Me.lblNatureOfDeduction.ReferenceTableName = Nothing
        Me.lblNatureOfDeduction.Size = New System.Drawing.Size(216, 20)
        Me.lblNatureOfDeduction.TabIndex = 143
        Me.lblNatureOfDeduction.TabStop = False
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 229)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel1.TabIndex = 129
        Me.RadLabel1.Text = "GL Account"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(13, 325)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(111, 18)
        Me.MyLabel6.TabIndex = 142
        Me.MyLabel6.Text = "Nature Of Deduction"
        '
        'chkDoNotIncludeInGrossSalaryForOverTime
        '
        Me.chkDoNotIncludeInGrossSalaryForOverTime.Location = New System.Drawing.Point(135, 380)
        Me.chkDoNotIncludeInGrossSalaryForOverTime.Name = "chkDoNotIncludeInGrossSalaryForOverTime"
        Me.chkDoNotIncludeInGrossSalaryForOverTime.Size = New System.Drawing.Size(246, 18)
        Me.chkDoNotIncludeInGrossSalaryForOverTime.TabIndex = 141
        Me.chkDoNotIncludeInGrossSalaryForOverTime.Text = "Do Not Include in Gross Salary For Over Time"
        '
        'chkTDSExempted
        '
        Me.chkTDSExempted.Location = New System.Drawing.Point(238, 356)
        Me.chkTDSExempted.Name = "chkTDSExempted"
        Me.chkTDSExempted.Size = New System.Drawing.Size(93, 18)
        Me.chkTDSExempted.TabIndex = 140
        Me.chkTDSExempted.Text = "TDS Exempted"
        '
        'chkFullnFinal
        '
        Me.chkFullnFinal.Location = New System.Drawing.Point(135, 356)
        Me.chkFullnFinal.Name = "chkFullnFinal"
        Me.chkFullnFinal.Size = New System.Drawing.Size(86, 18)
        Me.chkFullnFinal.TabIndex = 139
        Me.chkFullnFinal.Text = "Full and Final"
        '
        'fndEmpGLAcc
        '
        Me.fndEmpGLAcc.CalculationExpression = Nothing
        Me.fndEmpGLAcc.FieldCode = Nothing
        Me.fndEmpGLAcc.FieldDesc = Nothing
        Me.fndEmpGLAcc.FieldMaxLength = 0
        Me.fndEmpGLAcc.FieldName = Nothing
        Me.fndEmpGLAcc.isCalculatedField = False
        Me.fndEmpGLAcc.IsSourceFromTable = False
        Me.fndEmpGLAcc.IsSourceFromValueList = False
        Me.fndEmpGLAcc.IsUnique = False
        Me.fndEmpGLAcc.Location = New System.Drawing.Point(135, 298)
        Me.fndEmpGLAcc.MendatroryField = False
        Me.fndEmpGLAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndEmpGLAcc.MyLinkLable1 = Nothing
        Me.fndEmpGLAcc.MyLinkLable2 = Nothing
        Me.fndEmpGLAcc.MyReadOnly = False
        Me.fndEmpGLAcc.MyShowMasterFormButton = False
        Me.fndEmpGLAcc.Name = "fndEmpGLAcc"
        Me.fndEmpGLAcc.ReferenceFieldDesc = Nothing
        Me.fndEmpGLAcc.ReferenceFieldName = Nothing
        Me.fndEmpGLAcc.ReferenceTableName = Nothing
        Me.fndEmpGLAcc.Size = New System.Drawing.Size(211, 18)
        Me.fndEmpGLAcc.TabIndex = 138
        Me.fndEmpGLAcc.Value = ""
        '
        'txtEmpGLAcc
        '
        Me.txtEmpGLAcc.CalculationExpression = Nothing
        Me.txtEmpGLAcc.FieldCode = Nothing
        Me.txtEmpGLAcc.FieldDesc = Nothing
        Me.txtEmpGLAcc.FieldMaxLength = 0
        Me.txtEmpGLAcc.FieldName = Nothing
        Me.txtEmpGLAcc.isCalculatedField = False
        Me.txtEmpGLAcc.IsSourceFromTable = False
        Me.txtEmpGLAcc.IsSourceFromValueList = False
        Me.txtEmpGLAcc.IsUnique = False
        Me.txtEmpGLAcc.Location = New System.Drawing.Point(349, 297)
        Me.txtEmpGLAcc.MaxLength = 55
        Me.txtEmpGLAcc.MendatroryField = False
        Me.txtEmpGLAcc.MyLinkLable1 = Me.RadLabel1
        Me.txtEmpGLAcc.MyLinkLable2 = Nothing
        Me.txtEmpGLAcc.Name = "txtEmpGLAcc"
        Me.txtEmpGLAcc.ReadOnly = True
        Me.txtEmpGLAcc.ReferenceFieldDesc = Nothing
        Me.txtEmpGLAcc.ReferenceFieldName = Nothing
        Me.txtEmpGLAcc.ReferenceTableName = Nothing
        Me.txtEmpGLAcc.Size = New System.Drawing.Size(216, 20)
        Me.txtEmpGLAcc.TabIndex = 137
        Me.txtEmpGLAcc.TabStop = False
        '
        'lblEmpGLACC
        '
        Me.lblEmpGLACC.FieldName = Nothing
        Me.lblEmpGLACC.Location = New System.Drawing.Point(13, 301)
        Me.lblEmpGLACC.Name = "lblEmpGLACC"
        Me.lblEmpGLACC.Size = New System.Drawing.Size(113, 18)
        Me.lblEmpGLACC.TabIndex = 136
        Me.lblEmpGLACC.Text = "Employer GL Account"
        '
        'cboArrearType
        '
        Me.cboArrearType.CalculationExpression = Nothing
        Me.cboArrearType.DropDownAnimationEnabled = True
        Me.cboArrearType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboArrearType.FieldCode = Nothing
        Me.cboArrearType.FieldDesc = Nothing
        Me.cboArrearType.FieldMaxLength = 0
        Me.cboArrearType.FieldName = Nothing
        Me.cboArrearType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArrearType.isCalculatedField = False
        Me.cboArrearType.IsSourceFromTable = False
        Me.cboArrearType.IsSourceFromValueList = False
        Me.cboArrearType.IsUnique = False
        Me.cboArrearType.Location = New System.Drawing.Point(349, 108)
        Me.cboArrearType.MendatroryField = False
        Me.cboArrearType.MyLinkLable1 = Me.MyLabel19
        Me.cboArrearType.MyLinkLable2 = Nothing
        Me.cboArrearType.Name = "cboArrearType"
        Me.cboArrearType.ReferenceFieldDesc = Nothing
        Me.cboArrearType.ReferenceFieldName = Nothing
        Me.cboArrearType.ReferenceTableName = Nothing
        Me.cboArrearType.Size = New System.Drawing.Size(208, 18)
        Me.cboArrearType.TabIndex = 135
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(13, 85)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel19.TabIndex = 107
        Me.MyLabel19.Text = "Pay Head Type"
        '
        'txtPrintSerialNo
        '
        Me.txtPrintSerialNo.CalculationExpression = Nothing
        Me.txtPrintSerialNo.FieldCode = Nothing
        Me.txtPrintSerialNo.FieldDesc = Nothing
        Me.txtPrintSerialNo.FieldMaxLength = 0
        Me.txtPrintSerialNo.FieldName = Nothing
        Me.txtPrintSerialNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrintSerialNo.isCalculatedField = False
        Me.txtPrintSerialNo.IsSourceFromTable = False
        Me.txtPrintSerialNo.IsSourceFromValueList = False
        Me.txtPrintSerialNo.IsUnique = False
        Me.txtPrintSerialNo.Location = New System.Drawing.Point(135, 276)
        Me.txtPrintSerialNo.MaxLength = 49
        Me.txtPrintSerialNo.MendatroryField = False
        Me.txtPrintSerialNo.MyLinkLable1 = Me.MyLabel1
        Me.txtPrintSerialNo.MyLinkLable2 = Nothing
        Me.txtPrintSerialNo.Name = "txtPrintSerialNo"
        Me.txtPrintSerialNo.ReferenceFieldDesc = Nothing
        Me.txtPrintSerialNo.ReferenceFieldName = Nothing
        Me.txtPrintSerialNo.ReferenceTableName = Nothing
        Me.txtPrintSerialNo.Size = New System.Drawing.Size(211, 18)
        Me.txtPrintSerialNo.TabIndex = 134
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 61)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 119
        Me.MyLabel1.Text = "Print Name"
        '
        'lblPrintSerialNo
        '
        Me.lblPrintSerialNo.FieldName = Nothing
        Me.lblPrintSerialNo.Location = New System.Drawing.Point(13, 277)
        Me.lblPrintSerialNo.Name = "lblPrintSerialNo"
        Me.lblPrintSerialNo.Size = New System.Drawing.Size(80, 18)
        Me.lblPrintSerialNo.TabIndex = 133
        Me.lblPrintSerialNo.Text = "Print Serial No."
        '
        'txtSerialNo
        '
        Me.txtSerialNo.CalculationExpression = Nothing
        Me.txtSerialNo.FieldCode = Nothing
        Me.txtSerialNo.FieldDesc = Nothing
        Me.txtSerialNo.FieldMaxLength = 0
        Me.txtSerialNo.FieldName = Nothing
        Me.txtSerialNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialNo.isCalculatedField = False
        Me.txtSerialNo.IsSourceFromTable = False
        Me.txtSerialNo.IsSourceFromValueList = False
        Me.txtSerialNo.IsUnique = False
        Me.txtSerialNo.Location = New System.Drawing.Point(135, 252)
        Me.txtSerialNo.MaxLength = 49
        Me.txtSerialNo.MendatroryField = False
        Me.txtSerialNo.MyLinkLable1 = Me.MyLabel1
        Me.txtSerialNo.MyLinkLable2 = Nothing
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.ReferenceFieldDesc = Nothing
        Me.txtSerialNo.ReferenceFieldName = Nothing
        Me.txtSerialNo.ReferenceTableName = Nothing
        Me.txtSerialNo.Size = New System.Drawing.Size(211, 18)
        Me.txtSerialNo.TabIndex = 132
        '
        'lblSerailNo
        '
        Me.lblSerailNo.FieldName = Nothing
        Me.lblSerailNo.Location = New System.Drawing.Point(13, 253)
        Me.lblSerailNo.Name = "lblSerailNo"
        Me.lblSerailNo.Size = New System.Drawing.Size(54, 18)
        Me.lblSerailNo.TabIndex = 131
        Me.lblSerailNo.Text = "Serial No."
        '
        'chkIsEarning
        '
        Me.chkIsEarning.Location = New System.Drawing.Point(135, 205)
        Me.chkIsEarning.Name = "chkIsEarning"
        Me.chkIsEarning.Size = New System.Drawing.Size(69, 18)
        Me.chkIsEarning.TabIndex = 9
        Me.chkIsEarning.Text = "Is Earning"
        '
        'fndGLAccount
        '
        Me.fndGLAccount.CalculationExpression = Nothing
        Me.fndGLAccount.FieldCode = Nothing
        Me.fndGLAccount.FieldDesc = Nothing
        Me.fndGLAccount.FieldMaxLength = 0
        Me.fndGLAccount.FieldName = Nothing
        Me.fndGLAccount.isCalculatedField = False
        Me.fndGLAccount.IsSourceFromTable = False
        Me.fndGLAccount.IsSourceFromValueList = False
        Me.fndGLAccount.IsUnique = False
        Me.fndGLAccount.Location = New System.Drawing.Point(135, 229)
        Me.fndGLAccount.MendatroryField = False
        Me.fndGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGLAccount.MyLinkLable1 = Me.RadLabel1
        Me.fndGLAccount.MyLinkLable2 = Nothing
        Me.fndGLAccount.MyReadOnly = False
        Me.fndGLAccount.MyShowMasterFormButton = False
        Me.fndGLAccount.Name = "fndGLAccount"
        Me.fndGLAccount.ReferenceFieldDesc = Nothing
        Me.fndGLAccount.ReferenceFieldName = Nothing
        Me.fndGLAccount.ReferenceTableName = Nothing
        Me.fndGLAccount.Size = New System.Drawing.Size(211, 18)
        Me.fndGLAccount.TabIndex = 11
        Me.fndGLAccount.Value = ""
        '
        'txtGLAccountDesc
        '
        Me.txtGLAccountDesc.CalculationExpression = Nothing
        Me.txtGLAccountDesc.FieldCode = Nothing
        Me.txtGLAccountDesc.FieldDesc = Nothing
        Me.txtGLAccountDesc.FieldMaxLength = 0
        Me.txtGLAccountDesc.FieldName = Nothing
        Me.txtGLAccountDesc.isCalculatedField = False
        Me.txtGLAccountDesc.IsSourceFromTable = False
        Me.txtGLAccountDesc.IsSourceFromValueList = False
        Me.txtGLAccountDesc.IsUnique = False
        Me.txtGLAccountDesc.Location = New System.Drawing.Point(349, 228)
        Me.txtGLAccountDesc.MaxLength = 55
        Me.txtGLAccountDesc.MendatroryField = False
        Me.txtGLAccountDesc.MyLinkLable1 = Me.RadLabel1
        Me.txtGLAccountDesc.MyLinkLable2 = Nothing
        Me.txtGLAccountDesc.Name = "txtGLAccountDesc"
        Me.txtGLAccountDesc.ReadOnly = True
        Me.txtGLAccountDesc.ReferenceFieldDesc = Nothing
        Me.txtGLAccountDesc.ReferenceFieldName = Nothing
        Me.txtGLAccountDesc.ReferenceTableName = Nothing
        Me.txtGLAccountDesc.Size = New System.Drawing.Size(216, 20)
        Me.txtGLAccountDesc.TabIndex = 12
        Me.txtGLAccountDesc.TabStop = False
        '
        'ChkHiddenComponents
        '
        Me.ChkHiddenComponents.Location = New System.Drawing.Point(213, 205)
        Me.ChkHiddenComponents.Name = "ChkHiddenComponents"
        Me.ChkHiddenComponents.Size = New System.Drawing.Size(124, 18)
        Me.ChkHiddenComponents.TabIndex = 10
        Me.ChkHiddenComponents.Text = "Hidden Components"
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempcode.Location = New System.Drawing.Point(13, 13)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(86, 16)
        Me.lblempcode.TabIndex = 56
        Me.lblempcode.Text = "Pay Head Code"
        '
        'txtPrintName
        '
        Me.txtPrintName.CalculationExpression = Nothing
        Me.txtPrintName.FieldCode = Nothing
        Me.txtPrintName.FieldDesc = Nothing
        Me.txtPrintName.FieldMaxLength = 0
        Me.txtPrintName.FieldName = Nothing
        Me.txtPrintName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrintName.isCalculatedField = False
        Me.txtPrintName.IsSourceFromTable = False
        Me.txtPrintName.IsSourceFromValueList = False
        Me.txtPrintName.IsUnique = False
        Me.txtPrintName.Location = New System.Drawing.Point(135, 60)
        Me.txtPrintName.MaxLength = 49
        Me.txtPrintName.MendatroryField = False
        Me.txtPrintName.MyLinkLable1 = Me.MyLabel1
        Me.txtPrintName.MyLinkLable2 = Nothing
        Me.txtPrintName.Name = "txtPrintName"
        Me.txtPrintName.ReferenceFieldDesc = Nothing
        Me.txtPrintName.ReferenceFieldName = Nothing
        Me.txtPrintName.ReferenceTableName = Nothing
        Me.txtPrintName.Size = New System.Drawing.Size(211, 18)
        Me.txtPrintName.TabIndex = 3
        '
        'cboRoundOffType
        '
        Me.cboRoundOffType.CalculationExpression = Nothing
        Me.cboRoundOffType.DropDownAnimationEnabled = True
        Me.cboRoundOffType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboRoundOffType.FieldCode = Nothing
        Me.cboRoundOffType.FieldDesc = Nothing
        Me.cboRoundOffType.FieldMaxLength = 0
        Me.cboRoundOffType.FieldName = Nothing
        Me.cboRoundOffType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRoundOffType.isCalculatedField = False
        Me.cboRoundOffType.IsSourceFromTable = False
        Me.cboRoundOffType.IsSourceFromValueList = False
        Me.cboRoundOffType.IsUnique = False
        Me.cboRoundOffType.Location = New System.Drawing.Point(135, 180)
        Me.cboRoundOffType.MendatroryField = False
        Me.cboRoundOffType.MyLinkLable1 = Me.MyLabel5
        Me.cboRoundOffType.MyLinkLable2 = Nothing
        Me.cboRoundOffType.Name = "cboRoundOffType"
        Me.cboRoundOffType.ReferenceFieldDesc = Nothing
        Me.cboRoundOffType.ReferenceFieldName = Nothing
        Me.cboRoundOffType.ReferenceTableName = Nothing
        Me.cboRoundOffType.Size = New System.Drawing.Size(211, 18)
        Me.cboRoundOffType.TabIndex = 8
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(13, 181)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel5.TabIndex = 127
        Me.MyLabel5.Text = "Round off Type"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 109)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel2.TabIndex = 121
        Me.MyLabel2.Text = "Sub Head Type"
        '
        'cboPayHeadCategory
        '
        Me.cboPayHeadCategory.CalculationExpression = Nothing
        Me.cboPayHeadCategory.DropDownAnimationEnabled = True
        Me.cboPayHeadCategory.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPayHeadCategory.FieldCode = Nothing
        Me.cboPayHeadCategory.FieldDesc = Nothing
        Me.cboPayHeadCategory.FieldMaxLength = 0
        Me.cboPayHeadCategory.FieldName = Nothing
        Me.cboPayHeadCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPayHeadCategory.isCalculatedField = False
        Me.cboPayHeadCategory.IsSourceFromTable = False
        Me.cboPayHeadCategory.IsSourceFromValueList = False
        Me.cboPayHeadCategory.IsUnique = False
        Me.cboPayHeadCategory.Location = New System.Drawing.Point(135, 108)
        Me.cboPayHeadCategory.MendatroryField = False
        Me.cboPayHeadCategory.MyLinkLable1 = Me.MyLabel2
        Me.cboPayHeadCategory.MyLinkLable2 = Nothing
        Me.cboPayHeadCategory.Name = "cboPayHeadCategory"
        Me.cboPayHeadCategory.ReferenceFieldDesc = Nothing
        Me.cboPayHeadCategory.ReferenceFieldName = Nothing
        Me.cboPayHeadCategory.ReferenceTableName = Nothing
        Me.cboPayHeadCategory.Size = New System.Drawing.Size(211, 18)
        Me.cboPayHeadCategory.TabIndex = 5
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(13, 37)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel18.TabIndex = 55
        Me.MyLabel18.Text = "Pay Head Name"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(135, 12)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblempcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(192, 18)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'cboCalcBasis
        '
        Me.cboCalcBasis.CalculationExpression = Nothing
        Me.cboCalcBasis.DropDownAnimationEnabled = True
        Me.cboCalcBasis.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCalcBasis.FieldCode = Nothing
        Me.cboCalcBasis.FieldDesc = Nothing
        Me.cboCalcBasis.FieldMaxLength = 0
        Me.cboCalcBasis.FieldName = Nothing
        Me.cboCalcBasis.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCalcBasis.isCalculatedField = False
        Me.cboCalcBasis.IsSourceFromTable = False
        Me.cboCalcBasis.IsSourceFromValueList = False
        Me.cboCalcBasis.IsUnique = False
        Me.cboCalcBasis.Location = New System.Drawing.Point(135, 156)
        Me.cboCalcBasis.MendatroryField = False
        Me.cboCalcBasis.MyLinkLable1 = Me.MyLabel4
        Me.cboCalcBasis.MyLinkLable2 = Nothing
        Me.cboCalcBasis.Name = "cboCalcBasis"
        Me.cboCalcBasis.ReferenceFieldDesc = Nothing
        Me.cboCalcBasis.ReferenceFieldName = Nothing
        Me.cboCalcBasis.ReferenceTableName = Nothing
        Me.cboCalcBasis.Size = New System.Drawing.Size(211, 18)
        Me.cboCalcBasis.TabIndex = 7
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 157)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel4.TabIndex = 125
        Me.MyLabel4.Text = "Calculation Basis"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(13, 133)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel3.TabIndex = 123
        Me.MyLabel3.Text = "Periodicity"
        '
        'txtPayHeadName
        '
        Me.txtPayHeadName.CalculationExpression = Nothing
        Me.txtPayHeadName.FieldCode = Nothing
        Me.txtPayHeadName.FieldDesc = Nothing
        Me.txtPayHeadName.FieldMaxLength = 0
        Me.txtPayHeadName.FieldName = Nothing
        Me.txtPayHeadName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayHeadName.isCalculatedField = False
        Me.txtPayHeadName.IsSourceFromTable = False
        Me.txtPayHeadName.IsSourceFromValueList = False
        Me.txtPayHeadName.IsUnique = False
        Me.txtPayHeadName.Location = New System.Drawing.Point(135, 36)
        Me.txtPayHeadName.MaxLength = 49
        Me.txtPayHeadName.MendatroryField = False
        Me.txtPayHeadName.MyLinkLable1 = Me.MyLabel18
        Me.txtPayHeadName.MyLinkLable2 = Nothing
        Me.txtPayHeadName.Name = "txtPayHeadName"
        Me.txtPayHeadName.ReferenceFieldDesc = Nothing
        Me.txtPayHeadName.ReferenceFieldName = Nothing
        Me.txtPayHeadName.ReferenceTableName = Nothing
        Me.txtPayHeadName.Size = New System.Drawing.Size(211, 18)
        Me.txtPayHeadName.TabIndex = 2
        '
        'CboPayHeadType
        '
        Me.CboPayHeadType.CalculationExpression = Nothing
        Me.CboPayHeadType.DropDownAnimationEnabled = True
        Me.CboPayHeadType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboPayHeadType.FieldCode = Nothing
        Me.CboPayHeadType.FieldDesc = Nothing
        Me.CboPayHeadType.FieldMaxLength = 0
        Me.CboPayHeadType.FieldName = Nothing
        Me.CboPayHeadType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboPayHeadType.isCalculatedField = False
        Me.CboPayHeadType.IsSourceFromTable = False
        Me.CboPayHeadType.IsSourceFromValueList = False
        Me.CboPayHeadType.IsUnique = False
        Me.CboPayHeadType.Location = New System.Drawing.Point(135, 84)
        Me.CboPayHeadType.MendatroryField = False
        Me.CboPayHeadType.MyLinkLable1 = Me.MyLabel19
        Me.CboPayHeadType.MyLinkLable2 = Nothing
        Me.CboPayHeadType.Name = "CboPayHeadType"
        Me.CboPayHeadType.ReferenceFieldDesc = Nothing
        Me.CboPayHeadType.ReferenceFieldName = Nothing
        Me.CboPayHeadType.ReferenceTableName = Nothing
        Me.CboPayHeadType.Size = New System.Drawing.Size(211, 18)
        Me.CboPayHeadType.TabIndex = 4
        '
        'cboPeriodicity
        '
        Me.cboPeriodicity.CalculationExpression = Nothing
        Me.cboPeriodicity.DropDownAnimationEnabled = True
        Me.cboPeriodicity.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPeriodicity.FieldCode = Nothing
        Me.cboPeriodicity.FieldDesc = Nothing
        Me.cboPeriodicity.FieldMaxLength = 0
        Me.cboPeriodicity.FieldName = Nothing
        Me.cboPeriodicity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPeriodicity.isCalculatedField = False
        Me.cboPeriodicity.IsSourceFromTable = False
        Me.cboPeriodicity.IsSourceFromValueList = False
        Me.cboPeriodicity.IsUnique = False
        Me.cboPeriodicity.Location = New System.Drawing.Point(135, 132)
        Me.cboPeriodicity.MendatroryField = False
        Me.cboPeriodicity.MyLinkLable1 = Me.MyLabel3
        Me.cboPeriodicity.MyLinkLable2 = Nothing
        Me.cboPeriodicity.Name = "cboPeriodicity"
        Me.cboPeriodicity.ReferenceFieldDesc = Nothing
        Me.cboPeriodicity.ReferenceFieldName = Nothing
        Me.cboPeriodicity.ReferenceTableName = Nothing
        Me.cboPeriodicity.Size = New System.Drawing.Size(211, 18)
        Me.cboPeriodicity.TabIndex = 6
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(10, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(82, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(788, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'frmPayHeadDefinitions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(866, 480)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPayHeadDefinitions"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Pay Head Definitions"
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.TxtMaxHRA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMaxHRA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateAPInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNatureOfDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDoNotIncludeInGrossSalaryForOverTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTDSExempted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFullnFinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpGLAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpGLACC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboArrearType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrintSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSerailNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsEarning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGLAccountDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkHiddenComponents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrintName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboRoundOffType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPayHeadCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCalcBasis, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayHeadName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboPayHeadType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPeriodicity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents txtPrintName As common.Controls.MyTextBox
    Friend WithEvents cboRoundOffType As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboPayHeadCategory As common.Controls.MyComboBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents cboCalcBasis As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtPayHeadName As common.Controls.MyTextBox
    Friend WithEvents CboPayHeadType As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents cboPeriodicity As common.Controls.MyComboBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkIsEarning As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkHiddenComponents As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndGLAccount As common.UserControls.txtFinder
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtGLAccountDesc As common.Controls.MyTextBox
    Friend WithEvents txtSerialNo As common.Controls.MyTextBox
    Friend WithEvents lblSerailNo As common.Controls.MyLabel
    Friend WithEvents txtPrintSerialNo As common.Controls.MyTextBox
    Friend WithEvents lblPrintSerialNo As common.Controls.MyLabel
    Friend WithEvents cboArrearType As common.Controls.MyComboBox
    Friend WithEvents fndEmpGLAcc As common.UserControls.txtFinder
    Friend WithEvents txtEmpGLAcc As common.Controls.MyTextBox
    Friend WithEvents lblEmpGLACC As common.Controls.MyLabel
    Friend WithEvents chkFullnFinal As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkTDSExempted As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkDoNotIncludeInGrossSalaryForOverTime As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndNatureOfDeduction As common.UserControls.txtFinder
    Friend WithEvents lblNatureOfDeduction As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents chkCreateAPInvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblMaxHRA As common.Controls.MyLabel
    Friend WithEvents TxtMaxHRA As common.MyNumBox
End Class
