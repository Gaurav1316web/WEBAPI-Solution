<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDCSAdditionDeduction
    Inherits FrmMainTranScreen
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DescHindi = New common.Controls.MyLabel()
        Me.txtDescNameHindi = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.rbtnACNotExists = New common.Controls.MyRadioButton()
        Me.rbtnACExists = New common.Controls.MyRadioButton()
        Me.chkSavingAC = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludeShortageOwnBMC = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkSubtract = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkApplyTDS = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtMilkType = New common.UserControls.txtMultiSelectFinder()
        Me.grpQtyUOM = New System.Windows.Forms.GroupBox()
        Me.rbtnQtyUOMRec = New common.Controls.MyRadioButton()
        Me.rbtnQtyUOMKG = New common.Controls.MyRadioButton()
        Me.rbtnQtyUOMLtr = New common.Controls.MyRadioButton()
        Me.cboROIncreaseAfter = New common.Controls.MyComboBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.cboRoundOFFDecimalPlaces = New common.Controls.MyComboBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtAddAmount = New common.UserControls.txtMultiSelectFinder()
        Me.txtDescName = New common.Controls.MyTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtConvertsion = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.rbtnDCSTypeBMCTruckSheet = New common.Controls.MyRadioButton()
        Me.rbtnDCSTypeCluster = New common.Controls.MyRadioButton()
        Me.rbtnDCSTypeBMC = New common.Controls.MyRadioButton()
        Me.rbtnDCSTypePDCS = New common.Controls.MyRadioButton()
        Me.rbtnDCSTypeDCS = New common.Controls.MyRadioButton()
        Me.rbtnDCSTypeBoth = New common.Controls.MyRadioButton()
        Me.grpAdditionType = New System.Windows.Forms.GroupBox()
        Me.rbtnAdditionTypeNormal = New common.Controls.MyRadioButton()
        Me.rbtnAdditionTypeCompulsory = New common.Controls.MyRadioButton()
        Me.rbtnAdditionTypeSaving = New common.Controls.MyRadioButton()
        Me.lblMappingCodeDesc = New common.Controls.MyLabel()
        Me.txtMappingCode = New common.UserControls.txtFinder()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblGLAcctName = New common.Controls.MyLabel()
        Me.cboApplyType = New common.Controls.MyComboBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.cboApplyOn = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtGLAccount = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtApplyValue = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtnNatureTypeDeduction = New common.Controls.MyRadioButton()
        Me.rbtnNatureTypeAddition = New common.Controls.MyRadioButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblAdvanceCode = New common.Controls.MyLabel()
        Me.txtSNo = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.dtStartDate = New common.Controls.MyDateTimePicker()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.dtpEndDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.btnEndDate = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtExcludeDCS = New common.UserControls.txtMultiSelectFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DescHindi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescNameHindi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnACNotExists, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnACExists, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSavingAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeShortageOwnBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSubtract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyTDS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpQtyUOM.SuspendLayout()
        CType(Me.rbtnQtyUOMRec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnQtyUOMKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnQtyUOMLtr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboROIncreaseAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboRoundOFFDecimalPlaces, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtConvertsion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDCSTypeBMCTruckSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDCSTypeCluster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDCSTypeBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDCSTypePDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDCSTypeDCS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDCSTypeBoth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAdditionType.SuspendLayout()
        CType(Me.rbtnAdditionTypeNormal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAdditionTypeCompulsory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAdditionTypeSaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMappingCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGLAcctName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboApplyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboApplyOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplyValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.rbtnNatureTypeDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnNatureTypeAddition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtExcludeDCS)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DescHindi)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescNameHindi)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnACNotExists)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnACExists)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkSavingAC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkIncludeShortageOwnBMC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkSubtract)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkApplyTDS)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMilkType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpQtyUOM)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboROIncreaseAfter)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboRoundOFFDecimalPlaces)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAddAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpAdditionType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMappingCodeDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMappingCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboApplyType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboApplyOn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGLAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGLAcctName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtApplyValue)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdvanceCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkInactive)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpEndDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnEndDate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(681, 449)
        Me.SplitContainer1.SplitterDistance = 415
        Me.SplitContainer1.TabIndex = 0
        '
        'DescHindi
        '
        Me.DescHindi.FieldName = Nothing
        Me.DescHindi.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.DescHindi.Location = New System.Drawing.Point(5, 60)
        Me.DescHindi.Name = "DescHindi"
        Me.DescHindi.Size = New System.Drawing.Size(93, 18)
        Me.DescHindi.TabIndex = 384
        Me.DescHindi.Text = "Description Hindi"
        '
        'txtDescNameHindi
        '
        Me.txtDescNameHindi.CalculationExpression = Nothing
        Me.txtDescNameHindi.FieldCode = Nothing
        Me.txtDescNameHindi.FieldDesc = Nothing
        Me.txtDescNameHindi.FieldMaxLength = 0
        Me.txtDescNameHindi.FieldName = Nothing
        Me.txtDescNameHindi.isCalculatedField = False
        Me.txtDescNameHindi.IsSourceFromTable = False
        Me.txtDescNameHindi.IsSourceFromValueList = False
        Me.txtDescNameHindi.IsUnique = False
        Me.txtDescNameHindi.Location = New System.Drawing.Point(101, 59)
        Me.txtDescNameHindi.MaxLength = 100
        Me.txtDescNameHindi.MendatroryField = True
        Me.txtDescNameHindi.MyLinkLable1 = Me.lblDescription
        Me.txtDescNameHindi.MyLinkLable2 = Nothing
        Me.txtDescNameHindi.Name = "txtDescNameHindi"
        Me.txtDescNameHindi.ReferenceFieldDesc = Nothing
        Me.txtDescNameHindi.ReferenceFieldName = Nothing
        Me.txtDescNameHindi.ReferenceTableName = Nothing
        Me.txtDescNameHindi.Size = New System.Drawing.Size(569, 20)
        Me.txtDescNameHindi.TabIndex = 383
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(5, 34)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 17
        Me.lblDescription.Text = "Description"
        '
        'rbtnACNotExists
        '
        Me.rbtnACNotExists.Location = New System.Drawing.Point(307, 340)
        Me.rbtnACNotExists.MyLinkLable1 = Nothing
        Me.rbtnACNotExists.MyLinkLable2 = Nothing
        Me.rbtnACNotExists.Name = "rbtnACNotExists"
        Me.rbtnACNotExists.Size = New System.Drawing.Size(114, 18)
        Me.rbtnACNotExists.TabIndex = 382
        Me.rbtnACNotExists.Text = "Account Not Exists"
        '
        'rbtnACExists
        '
        Me.rbtnACExists.Location = New System.Drawing.Point(201, 340)
        Me.rbtnACExists.MyLinkLable1 = Nothing
        Me.rbtnACExists.MyLinkLable2 = Nothing
        Me.rbtnACExists.Name = "rbtnACExists"
        Me.rbtnACExists.Size = New System.Drawing.Size(91, 18)
        Me.rbtnACExists.TabIndex = 381
        Me.rbtnACExists.Text = "Account Exists"
        '
        'chkSavingAC
        '
        Me.chkSavingAC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSavingAC.Location = New System.Drawing.Point(105, 341)
        Me.chkSavingAC.Name = "chkSavingAC"
        Me.chkSavingAC.Size = New System.Drawing.Size(77, 16)
        Me.chkSavingAC.TabIndex = 377
        Me.chkSavingAC.Text = "Saving A/C"
        '
        'chkIncludeShortageOwnBMC
        '
        Me.chkIncludeShortageOwnBMC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeShortageOwnBMC.Location = New System.Drawing.Point(442, 213)
        Me.chkIncludeShortageOwnBMC.Name = "chkIncludeShortageOwnBMC"
        Me.chkIncludeShortageOwnBMC.Size = New System.Drawing.Size(232, 16)
        Me.chkIncludeShortageOwnBMC.TabIndex = 376
        Me.chkIncludeShortageOwnBMC.Text = "Include FAT/SNF Shortage for OWN BMC"
        '
        'chkSubtract
        '
        Me.chkSubtract.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSubtract.Location = New System.Drawing.Point(612, 238)
        Me.chkSubtract.Name = "chkSubtract"
        Me.chkSubtract.Size = New System.Drawing.Size(62, 16)
        Me.chkSubtract.TabIndex = 376
        Me.chkSubtract.Text = "Subtract"
        '
        'chkApplyTDS
        '
        Me.chkApplyTDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApplyTDS.Location = New System.Drawing.Point(536, 93)
        Me.chkApplyTDS.Name = "chkApplyTDS"
        Me.chkApplyTDS.Size = New System.Drawing.Size(74, 16)
        Me.chkApplyTDS.TabIndex = 375
        Me.chkApplyTDS.Text = "Apply TDS"
        Me.chkApplyTDS.Visible = False
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(5, 289)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel10.TabIndex = 374
        Me.MyLabel10.Text = "Milk Type"
        '
        'txtMilkType
        '
        Me.txtMilkType.arrDispalyMember = Nothing
        Me.txtMilkType.arrValueMember = Nothing
        Me.txtMilkType.Location = New System.Drawing.Point(101, 288)
        Me.txtMilkType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMilkType.MyLinkLable1 = Nothing
        Me.txtMilkType.MyLinkLable2 = Nothing
        Me.txtMilkType.MyNullText = "All"
        Me.txtMilkType.Name = "txtMilkType"
        Me.txtMilkType.Size = New System.Drawing.Size(570, 20)
        Me.txtMilkType.TabIndex = 373
        '
        'grpQtyUOM
        '
        Me.grpQtyUOM.Controls.Add(Me.rbtnQtyUOMRec)
        Me.grpQtyUOM.Controls.Add(Me.rbtnQtyUOMKG)
        Me.grpQtyUOM.Controls.Add(Me.rbtnQtyUOMLtr)
        Me.grpQtyUOM.Location = New System.Drawing.Point(326, 116)
        Me.grpQtyUOM.Name = "grpQtyUOM"
        Me.grpQtyUOM.Size = New System.Drawing.Size(201, 29)
        Me.grpQtyUOM.TabIndex = 372
        Me.grpQtyUOM.TabStop = False
        Me.grpQtyUOM.Text = "UOM"
        Me.grpQtyUOM.Visible = False
        '
        'rbtnQtyUOMRec
        '
        Me.rbtnQtyUOMRec.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnQtyUOMRec.Location = New System.Drawing.Point(6, 13)
        Me.rbtnQtyUOMRec.MyLinkLable1 = Nothing
        Me.rbtnQtyUOMRec.MyLinkLable2 = Nothing
        Me.rbtnQtyUOMRec.Name = "rbtnQtyUOMRec"
        Me.rbtnQtyUOMRec.Size = New System.Drawing.Size(98, 18)
        Me.rbtnQtyUOMRec.TabIndex = 2
        Me.rbtnQtyUOMRec.Text = "Receiving UOM"
        Me.rbtnQtyUOMRec.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnQtyUOMKG
        '
        Me.rbtnQtyUOMKG.Location = New System.Drawing.Point(146, 13)
        Me.rbtnQtyUOMKG.MyLinkLable1 = Nothing
        Me.rbtnQtyUOMKG.MyLinkLable2 = Nothing
        Me.rbtnQtyUOMKG.Name = "rbtnQtyUOMKG"
        Me.rbtnQtyUOMKG.Size = New System.Drawing.Size(33, 18)
        Me.rbtnQtyUOMKG.TabIndex = 1
        Me.rbtnQtyUOMKG.TabStop = False
        Me.rbtnQtyUOMKG.Text = "Kg"
        '
        'rbtnQtyUOMLtr
        '
        Me.rbtnQtyUOMLtr.Location = New System.Drawing.Point(107, 13)
        Me.rbtnQtyUOMLtr.MyLinkLable1 = Nothing
        Me.rbtnQtyUOMLtr.MyLinkLable2 = Nothing
        Me.rbtnQtyUOMLtr.Name = "rbtnQtyUOMLtr"
        Me.rbtnQtyUOMLtr.Size = New System.Drawing.Size(33, 18)
        Me.rbtnQtyUOMLtr.TabIndex = 0
        Me.rbtnQtyUOMLtr.TabStop = False
        Me.rbtnQtyUOMLtr.Text = "Ltr"
        '
        'cboROIncreaseAfter
        '
        Me.cboROIncreaseAfter.CalculationExpression = Nothing
        Me.cboROIncreaseAfter.DropDownAnimationEnabled = True
        Me.cboROIncreaseAfter.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboROIncreaseAfter.FieldCode = Nothing
        Me.cboROIncreaseAfter.FieldDesc = Nothing
        Me.cboROIncreaseAfter.FieldMaxLength = 0
        Me.cboROIncreaseAfter.FieldName = Nothing
        Me.cboROIncreaseAfter.isCalculatedField = False
        Me.cboROIncreaseAfter.IsSourceFromTable = False
        Me.cboROIncreaseAfter.IsSourceFromValueList = False
        Me.cboROIncreaseAfter.IsUnique = False
        Me.cboROIncreaseAfter.Location = New System.Drawing.Point(392, 262)
        Me.cboROIncreaseAfter.MendatroryField = True
        Me.cboROIncreaseAfter.MyLinkLable1 = Me.MyLabel9
        Me.cboROIncreaseAfter.MyLinkLable2 = Nothing
        Me.cboROIncreaseAfter.Name = "cboROIncreaseAfter"
        Me.cboROIncreaseAfter.ReferenceFieldDesc = Nothing
        Me.cboROIncreaseAfter.ReferenceFieldName = Nothing
        Me.cboROIncreaseAfter.ReferenceTableName = Nothing
        Me.cboROIncreaseAfter.Size = New System.Drawing.Size(279, 20)
        Me.cboROIncreaseAfter.TabIndex = 370
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(310, 264)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel9.TabIndex = 371
        Me.MyLabel9.Text = "Increase After"
        '
        'cboRoundOFFDecimalPlaces
        '
        Me.cboRoundOFFDecimalPlaces.CalculationExpression = Nothing
        Me.cboRoundOFFDecimalPlaces.DropDownAnimationEnabled = True
        Me.cboRoundOFFDecimalPlaces.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboRoundOFFDecimalPlaces.FieldCode = Nothing
        Me.cboRoundOFFDecimalPlaces.FieldDesc = Nothing
        Me.cboRoundOFFDecimalPlaces.FieldMaxLength = 0
        Me.cboRoundOFFDecimalPlaces.FieldName = Nothing
        Me.cboRoundOFFDecimalPlaces.isCalculatedField = False
        Me.cboRoundOFFDecimalPlaces.IsSourceFromTable = False
        Me.cboRoundOFFDecimalPlaces.IsSourceFromValueList = False
        Me.cboRoundOFFDecimalPlaces.IsUnique = False
        Me.cboRoundOFFDecimalPlaces.Location = New System.Drawing.Point(101, 262)
        Me.cboRoundOFFDecimalPlaces.MendatroryField = True
        Me.cboRoundOFFDecimalPlaces.MyLinkLable1 = Me.MyLabel8
        Me.cboRoundOFFDecimalPlaces.MyLinkLable2 = Nothing
        Me.cboRoundOFFDecimalPlaces.Name = "cboRoundOFFDecimalPlaces"
        Me.cboRoundOFFDecimalPlaces.ReferenceFieldDesc = Nothing
        Me.cboRoundOFFDecimalPlaces.ReferenceFieldName = Nothing
        Me.cboRoundOFFDecimalPlaces.ReferenceTableName = Nothing
        Me.cboRoundOFFDecimalPlaces.Size = New System.Drawing.Size(204, 20)
        Me.cboRoundOFFDecimalPlaces.TabIndex = 368
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(5, 264)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel8.TabIndex = 369
        Me.MyLabel8.Text = "Round On"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(5, 237)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel14.TabIndex = 367
        Me.MyLabel14.Text = "Add Amount"
        '
        'txtAddAmount
        '
        Me.txtAddAmount.arrDispalyMember = Nothing
        Me.txtAddAmount.arrValueMember = Nothing
        Me.txtAddAmount.Location = New System.Drawing.Point(101, 236)
        Me.txtAddAmount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddAmount.MyLinkLable1 = Nothing
        Me.txtAddAmount.MyLinkLable2 = Nothing
        Me.txtAddAmount.MyNullText = "Amount of DCS Addition/Deduction Will be added"
        Me.txtAddAmount.Name = "txtAddAmount"
        Me.txtAddAmount.Size = New System.Drawing.Size(506, 20)
        Me.txtAddAmount.TabIndex = 366
        '
        'txtDescName
        '
        Me.txtDescName.CalculationExpression = Nothing
        Me.txtDescName.FieldCode = Nothing
        Me.txtDescName.FieldDesc = Nothing
        Me.txtDescName.FieldMaxLength = 0
        Me.txtDescName.FieldName = Nothing
        Me.txtDescName.isCalculatedField = False
        Me.txtDescName.IsSourceFromTable = False
        Me.txtDescName.IsSourceFromValueList = False
        Me.txtDescName.IsUnique = False
        Me.txtDescName.Location = New System.Drawing.Point(101, 33)
        Me.txtDescName.MaxLength = 100
        Me.txtDescName.MendatroryField = True
        Me.txtDescName.MyLinkLable1 = Me.lblDescription
        Me.txtDescName.MyLinkLable2 = Nothing
        Me.txtDescName.Name = "txtDescName"
        Me.txtDescName.ReferenceFieldDesc = Nothing
        Me.txtDescName.ReferenceFieldName = Nothing
        Me.txtDescName.ReferenceTableName = Nothing
        Me.txtDescName.Size = New System.Drawing.Size(570, 20)
        Me.txtDescName.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtConvertsion)
        Me.GroupBox1.Controls.Add(Me.MyLabel11)
        Me.GroupBox1.Controls.Add(Me.rbtnDCSTypeBMCTruckSheet)
        Me.GroupBox1.Controls.Add(Me.rbtnDCSTypeCluster)
        Me.GroupBox1.Controls.Add(Me.rbtnDCSTypeBMC)
        Me.GroupBox1.Controls.Add(Me.rbtnDCSTypePDCS)
        Me.GroupBox1.Controls.Add(Me.rbtnDCSTypeDCS)
        Me.GroupBox1.Controls.Add(Me.rbtnDCSTypeBoth)
        Me.GroupBox1.Location = New System.Drawing.Point(526, 116)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(145, 101)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Applicable For DCS Type"
        '
        'txtConvertsion
        '
        Me.txtConvertsion.BackColor = System.Drawing.Color.Transparent
        Me.txtConvertsion.CalculationExpression = Nothing
        Me.txtConvertsion.DecimalPlaces = 3
        Me.txtConvertsion.FieldCode = Nothing
        Me.txtConvertsion.FieldDesc = Nothing
        Me.txtConvertsion.FieldMaxLength = 0
        Me.txtConvertsion.FieldName = Nothing
        Me.txtConvertsion.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtConvertsion.isCalculatedField = False
        Me.txtConvertsion.IsSourceFromTable = False
        Me.txtConvertsion.IsSourceFromValueList = False
        Me.txtConvertsion.IsUnique = False
        Me.txtConvertsion.Location = New System.Drawing.Point(77, 72)
        Me.txtConvertsion.MaxLength = 5
        Me.txtConvertsion.MendatroryField = False
        Me.txtConvertsion.MyLinkLable1 = Me.MyLabel11
        Me.txtConvertsion.MyLinkLable2 = Nothing
        Me.txtConvertsion.Name = "txtConvertsion"
        Me.txtConvertsion.ReferenceFieldDesc = Nothing
        Me.txtConvertsion.ReferenceFieldName = Nothing
        Me.txtConvertsion.ReferenceTableName = Nothing
        Me.txtConvertsion.Size = New System.Drawing.Size(60, 21)
        Me.txtConvertsion.TabIndex = 383
        Me.txtConvertsion.Text = "0"
        Me.txtConvertsion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConvertsion.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(6, 74)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel11.TabIndex = 384
        Me.MyLabel11.Text = "Conversion"
        '
        'rbtnDCSTypeBMCTruckSheet
        '
        Me.rbtnDCSTypeBMCTruckSheet.Location = New System.Drawing.Point(6, 51)
        Me.rbtnDCSTypeBMCTruckSheet.MyLinkLable1 = Nothing
        Me.rbtnDCSTypeBMCTruckSheet.MyLinkLable2 = Nothing
        Me.rbtnDCSTypeBMCTruckSheet.Name = "rbtnDCSTypeBMCTruckSheet"
        Me.rbtnDCSTypeBMCTruckSheet.Size = New System.Drawing.Size(105, 18)
        Me.rbtnDCSTypeBMCTruckSheet.TabIndex = 5
        Me.rbtnDCSTypeBMCTruckSheet.Text = "BMC Truck Sheet"
        '
        'rbtnDCSTypeCluster
        '
        Me.rbtnDCSTypeCluster.Location = New System.Drawing.Point(84, 32)
        Me.rbtnDCSTypeCluster.MyLinkLable1 = Nothing
        Me.rbtnDCSTypeCluster.MyLinkLable2 = Nothing
        Me.rbtnDCSTypeCluster.Name = "rbtnDCSTypeCluster"
        Me.rbtnDCSTypeCluster.Size = New System.Drawing.Size(55, 18)
        Me.rbtnDCSTypeCluster.TabIndex = 4
        Me.rbtnDCSTypeCluster.Text = "Cluster"
        '
        'rbtnDCSTypeBMC
        '
        Me.rbtnDCSTypeBMC.Location = New System.Drawing.Point(6, 32)
        Me.rbtnDCSTypeBMC.MyLinkLable1 = Nothing
        Me.rbtnDCSTypeBMC.MyLinkLable2 = Nothing
        Me.rbtnDCSTypeBMC.Name = "rbtnDCSTypeBMC"
        Me.rbtnDCSTypeBMC.Size = New System.Drawing.Size(70, 18)
        Me.rbtnDCSTypeBMC.TabIndex = 3
        Me.rbtnDCSTypeBMC.Text = "Own BMC"
        '
        'rbtnDCSTypePDCS
        '
        Me.rbtnDCSTypePDCS.Location = New System.Drawing.Point(90, 13)
        Me.rbtnDCSTypePDCS.MyLinkLable1 = Nothing
        Me.rbtnDCSTypePDCS.MyLinkLable2 = Nothing
        Me.rbtnDCSTypePDCS.Name = "rbtnDCSTypePDCS"
        Me.rbtnDCSTypePDCS.Size = New System.Drawing.Size(47, 18)
        Me.rbtnDCSTypePDCS.TabIndex = 2
        Me.rbtnDCSTypePDCS.Text = "PDCS"
        '
        'rbtnDCSTypeDCS
        '
        Me.rbtnDCSTypeDCS.Location = New System.Drawing.Point(44, 13)
        Me.rbtnDCSTypeDCS.MyLinkLable1 = Nothing
        Me.rbtnDCSTypeDCS.MyLinkLable2 = Nothing
        Me.rbtnDCSTypeDCS.Name = "rbtnDCSTypeDCS"
        Me.rbtnDCSTypeDCS.Size = New System.Drawing.Size(41, 18)
        Me.rbtnDCSTypeDCS.TabIndex = 1
        Me.rbtnDCSTypeDCS.Text = "DCS"
        '
        'rbtnDCSTypeBoth
        '
        Me.rbtnDCSTypeBoth.Location = New System.Drawing.Point(6, 13)
        Me.rbtnDCSTypeBoth.MyLinkLable1 = Nothing
        Me.rbtnDCSTypeBoth.MyLinkLable2 = Nothing
        Me.rbtnDCSTypeBoth.Name = "rbtnDCSTypeBoth"
        Me.rbtnDCSTypeBoth.Size = New System.Drawing.Size(33, 18)
        Me.rbtnDCSTypeBoth.TabIndex = 0
        Me.rbtnDCSTypeBoth.Text = "All"
        '
        'grpAdditionType
        '
        Me.grpAdditionType.Controls.Add(Me.rbtnAdditionTypeNormal)
        Me.grpAdditionType.Controls.Add(Me.rbtnAdditionTypeCompulsory)
        Me.grpAdditionType.Controls.Add(Me.rbtnAdditionTypeSaving)
        Me.grpAdditionType.Location = New System.Drawing.Point(326, 147)
        Me.grpAdditionType.Name = "grpAdditionType"
        Me.grpAdditionType.Size = New System.Drawing.Size(203, 37)
        Me.grpAdditionType.TabIndex = 29
        Me.grpAdditionType.TabStop = False
        Me.grpAdditionType.Text = "Addition Type"
        Me.grpAdditionType.Visible = False
        '
        'rbtnAdditionTypeNormal
        '
        Me.rbtnAdditionTypeNormal.Location = New System.Drawing.Point(6, 19)
        Me.rbtnAdditionTypeNormal.MyLinkLable1 = Nothing
        Me.rbtnAdditionTypeNormal.MyLinkLable2 = Nothing
        Me.rbtnAdditionTypeNormal.Name = "rbtnAdditionTypeNormal"
        Me.rbtnAdditionTypeNormal.Size = New System.Drawing.Size(57, 18)
        Me.rbtnAdditionTypeNormal.TabIndex = 2
        Me.rbtnAdditionTypeNormal.Text = "Normal"
        '
        'rbtnAdditionTypeCompulsory
        '
        Me.rbtnAdditionTypeCompulsory.Location = New System.Drawing.Point(121, 19)
        Me.rbtnAdditionTypeCompulsory.MyLinkLable1 = Nothing
        Me.rbtnAdditionTypeCompulsory.MyLinkLable2 = Nothing
        Me.rbtnAdditionTypeCompulsory.Name = "rbtnAdditionTypeCompulsory"
        Me.rbtnAdditionTypeCompulsory.Size = New System.Drawing.Size(80, 18)
        Me.rbtnAdditionTypeCompulsory.TabIndex = 1
        Me.rbtnAdditionTypeCompulsory.Text = "Compulsory"
        '
        'rbtnAdditionTypeSaving
        '
        Me.rbtnAdditionTypeSaving.Location = New System.Drawing.Point(68, 19)
        Me.rbtnAdditionTypeSaving.MyLinkLable1 = Nothing
        Me.rbtnAdditionTypeSaving.MyLinkLable2 = Nothing
        Me.rbtnAdditionTypeSaving.Name = "rbtnAdditionTypeSaving"
        Me.rbtnAdditionTypeSaving.Size = New System.Drawing.Size(53, 18)
        Me.rbtnAdditionTypeSaving.TabIndex = 0
        Me.rbtnAdditionTypeSaving.Text = "Saving"
        '
        'lblMappingCodeDesc
        '
        Me.lblMappingCodeDesc.AutoSize = False
        Me.lblMappingCodeDesc.BorderVisible = True
        Me.lblMappingCodeDesc.FieldName = Nothing
        Me.lblMappingCodeDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMappingCodeDesc.Location = New System.Drawing.Point(310, 212)
        Me.lblMappingCodeDesc.Name = "lblMappingCodeDesc"
        Me.lblMappingCodeDesc.Size = New System.Drawing.Size(128, 18)
        Me.lblMappingCodeDesc.TabIndex = 28
        Me.lblMappingCodeDesc.TextWrap = False
        '
        'txtMappingCode
        '
        Me.txtMappingCode.CalculationExpression = Nothing
        Me.txtMappingCode.FieldCode = Nothing
        Me.txtMappingCode.FieldDesc = Nothing
        Me.txtMappingCode.FieldMaxLength = 0
        Me.txtMappingCode.FieldName = Nothing
        Me.txtMappingCode.isCalculatedField = False
        Me.txtMappingCode.IsSourceFromTable = False
        Me.txtMappingCode.IsSourceFromValueList = False
        Me.txtMappingCode.IsUnique = False
        Me.txtMappingCode.Location = New System.Drawing.Point(101, 212)
        Me.txtMappingCode.MendatroryField = True
        Me.txtMappingCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMappingCode.MyLinkLable1 = Me.MyLabel7
        Me.txtMappingCode.MyLinkLable2 = Me.lblGLAcctName
        Me.txtMappingCode.MyReadOnly = False
        Me.txtMappingCode.MyShowMasterFormButton = False
        Me.txtMappingCode.Name = "txtMappingCode"
        Me.txtMappingCode.ReferenceFieldDesc = Nothing
        Me.txtMappingCode.ReferenceFieldName = Nothing
        Me.txtMappingCode.ReferenceTableName = Nothing
        Me.txtMappingCode.Size = New System.Drawing.Size(204, 18)
        Me.txtMappingCode.TabIndex = 27
        Me.txtMappingCode.Value = ""
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 213)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(80, 16)
        Me.MyLabel7.TabIndex = 26
        Me.MyLabel7.Text = "Mapping Code"
        '
        'lblGLAcctName
        '
        Me.lblGLAcctName.AutoSize = False
        Me.lblGLAcctName.BorderVisible = True
        Me.lblGLAcctName.FieldName = Nothing
        Me.lblGLAcctName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGLAcctName.Location = New System.Drawing.Point(310, 188)
        Me.lblGLAcctName.Name = "lblGLAcctName"
        Me.lblGLAcctName.Size = New System.Drawing.Size(212, 18)
        Me.lblGLAcctName.TabIndex = 10
        Me.lblGLAcctName.TextWrap = False
        '
        'cboApplyType
        '
        Me.cboApplyType.CalculationExpression = Nothing
        Me.cboApplyType.DropDownAnimationEnabled = True
        Me.cboApplyType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboApplyType.FieldCode = Nothing
        Me.cboApplyType.FieldDesc = Nothing
        Me.cboApplyType.FieldMaxLength = 0
        Me.cboApplyType.FieldName = Nothing
        Me.cboApplyType.isCalculatedField = False
        Me.cboApplyType.IsSourceFromTable = False
        Me.cboApplyType.IsSourceFromValueList = False
        Me.cboApplyType.IsUnique = False
        Me.cboApplyType.Location = New System.Drawing.Point(101, 135)
        Me.cboApplyType.MendatroryField = True
        Me.cboApplyType.MyLinkLable1 = Me.MyLabel4
        Me.cboApplyType.MyLinkLable2 = Nothing
        Me.cboApplyType.Name = "cboApplyType"
        Me.cboApplyType.ReferenceFieldDesc = Nothing
        Me.cboApplyType.ReferenceFieldName = Nothing
        Me.cboApplyType.ReferenceTableName = Nothing
        Me.cboApplyType.Size = New System.Drawing.Size(220, 20)
        Me.cboApplyType.TabIndex = 5
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 137)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel4.TabIndex = 14
        Me.MyLabel4.Text = "Apply Type"
        '
        'cboApplyOn
        '
        Me.cboApplyOn.CalculationExpression = Nothing
        Me.cboApplyOn.DropDownAnimationEnabled = True
        Me.cboApplyOn.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboApplyOn.FieldCode = Nothing
        Me.cboApplyOn.FieldDesc = Nothing
        Me.cboApplyOn.FieldMaxLength = 0
        Me.cboApplyOn.FieldName = Nothing
        Me.cboApplyOn.isCalculatedField = False
        Me.cboApplyOn.IsSourceFromTable = False
        Me.cboApplyOn.IsSourceFromValueList = False
        Me.cboApplyOn.IsUnique = False
        Me.cboApplyOn.Location = New System.Drawing.Point(101, 109)
        Me.cboApplyOn.MendatroryField = True
        Me.cboApplyOn.MyLinkLable1 = Me.MyLabel2
        Me.cboApplyOn.MyLinkLable2 = Nothing
        Me.cboApplyOn.Name = "cboApplyOn"
        Me.cboApplyOn.ReferenceFieldDesc = Nothing
        Me.cboApplyOn.ReferenceFieldName = Nothing
        Me.cboApplyOn.ReferenceTableName = Nothing
        Me.cboApplyOn.Size = New System.Drawing.Size(220, 20)
        Me.cboApplyOn.TabIndex = 4
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 111)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel2.TabIndex = 15
        Me.MyLabel2.Text = "Apply On"
        '
        'txtGLAccount
        '
        Me.txtGLAccount.CalculationExpression = Nothing
        Me.txtGLAccount.FieldCode = Nothing
        Me.txtGLAccount.FieldDesc = Nothing
        Me.txtGLAccount.FieldMaxLength = 0
        Me.txtGLAccount.FieldName = Nothing
        Me.txtGLAccount.isCalculatedField = False
        Me.txtGLAccount.IsSourceFromTable = False
        Me.txtGLAccount.IsSourceFromValueList = False
        Me.txtGLAccount.IsUnique = False
        Me.txtGLAccount.Location = New System.Drawing.Point(101, 188)
        Me.txtGLAccount.MendatroryField = True
        Me.txtGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccount.MyLinkLable1 = Me.MyLabel6
        Me.txtGLAccount.MyLinkLable2 = Me.lblGLAcctName
        Me.txtGLAccount.MyReadOnly = False
        Me.txtGLAccount.MyShowMasterFormButton = False
        Me.txtGLAccount.Name = "txtGLAccount"
        Me.txtGLAccount.ReferenceFieldDesc = Nothing
        Me.txtGLAccount.ReferenceFieldName = Nothing
        Me.txtGLAccount.ReferenceTableName = Nothing
        Me.txtGLAccount.Size = New System.Drawing.Size(203, 18)
        Me.txtGLAccount.TabIndex = 9
        Me.txtGLAccount.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(5, 189)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel6.TabIndex = 12
        Me.MyLabel6.Text = "GL Account"
        '
        'txtApplyValue
        '
        Me.txtApplyValue.BackColor = System.Drawing.Color.Transparent
        Me.txtApplyValue.CalculationExpression = Nothing
        Me.txtApplyValue.DecimalPlaces = 3
        Me.txtApplyValue.FieldCode = Nothing
        Me.txtApplyValue.FieldDesc = Nothing
        Me.txtApplyValue.FieldMaxLength = 0
        Me.txtApplyValue.FieldName = Nothing
        Me.txtApplyValue.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtApplyValue.isCalculatedField = False
        Me.txtApplyValue.IsSourceFromTable = False
        Me.txtApplyValue.IsSourceFromValueList = False
        Me.txtApplyValue.IsUnique = False
        Me.txtApplyValue.Location = New System.Drawing.Point(101, 161)
        Me.txtApplyValue.MaxLength = 5
        Me.txtApplyValue.MendatroryField = True
        Me.txtApplyValue.MyLinkLable1 = Me.MyLabel5
        Me.txtApplyValue.MyLinkLable2 = Nothing
        Me.txtApplyValue.Name = "txtApplyValue"
        Me.txtApplyValue.ReferenceFieldDesc = Nothing
        Me.txtApplyValue.ReferenceFieldName = Nothing
        Me.txtApplyValue.ReferenceTableName = Nothing
        Me.txtApplyValue.Size = New System.Drawing.Size(72, 21)
        Me.txtApplyValue.TabIndex = 7
        Me.txtApplyValue.Text = "0"
        Me.txtApplyValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtApplyValue.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(5, 163)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel5.TabIndex = 13
        Me.MyLabel5.Text = "Apply Value"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtnNatureTypeDeduction)
        Me.GroupBox2.Controls.Add(Me.rbtnNatureTypeAddition)
        Me.GroupBox2.Location = New System.Drawing.Point(326, 83)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(198, 36)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Nature"
        '
        'rbtnNatureTypeDeduction
        '
        Me.rbtnNatureTypeDeduction.Location = New System.Drawing.Point(88, 12)
        Me.rbtnNatureTypeDeduction.MyLinkLable1 = Nothing
        Me.rbtnNatureTypeDeduction.MyLinkLable2 = Nothing
        Me.rbtnNatureTypeDeduction.Name = "rbtnNatureTypeDeduction"
        Me.rbtnNatureTypeDeduction.Size = New System.Drawing.Size(72, 18)
        Me.rbtnNatureTypeDeduction.TabIndex = 1
        Me.rbtnNatureTypeDeduction.Text = "Deduction"
        '
        'rbtnNatureTypeAddition
        '
        Me.rbtnNatureTypeAddition.Location = New System.Drawing.Point(6, 12)
        Me.rbtnNatureTypeAddition.MyLinkLable1 = Nothing
        Me.rbtnNatureTypeAddition.MyLinkLable2 = Nothing
        Me.rbtnNatureTypeAddition.Name = "rbtnNatureTypeAddition"
        Me.rbtnNatureTypeAddition.Size = New System.Drawing.Size(63, 18)
        Me.rbtnNatureTypeAddition.TabIndex = 0
        Me.rbtnNatureTypeAddition.Text = "Addition"
        '
        'RadButton1
        '
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(471, 6)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(22, 21)
        Me.RadButton1.TabIndex = 20
        Me.RadButton1.Text = "CC"
        '
        'lblAdvanceCode
        '
        Me.lblAdvanceCode.FieldName = Nothing
        Me.lblAdvanceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAdvanceCode.Location = New System.Drawing.Point(5, 7)
        Me.lblAdvanceCode.Name = "lblAdvanceCode"
        Me.lblAdvanceCode.Size = New System.Drawing.Size(32, 18)
        Me.lblAdvanceCode.TabIndex = 22
        Me.lblAdvanceCode.Text = "Code"
        '
        'txtSNo
        '
        Me.txtSNo.BackColor = System.Drawing.Color.Transparent
        Me.txtSNo.CalculationExpression = Nothing
        Me.txtSNo.DecimalPlaces = 1
        Me.txtSNo.FieldCode = Nothing
        Me.txtSNo.FieldDesc = Nothing
        Me.txtSNo.FieldMaxLength = 0
        Me.txtSNo.FieldName = Nothing
        Me.txtSNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSNo.isCalculatedField = False
        Me.txtSNo.IsSourceFromTable = False
        Me.txtSNo.IsSourceFromValueList = False
        Me.txtSNo.IsUnique = False
        Me.txtSNo.Location = New System.Drawing.Point(230, 161)
        Me.txtSNo.MaxLength = 5
        Me.txtSNo.MendatroryField = False
        Me.txtSNo.MyLinkLable1 = Me.MyLabel1
        Me.txtSNo.MyLinkLable2 = Nothing
        Me.txtSNo.Name = "txtSNo"
        Me.txtSNo.ReferenceFieldDesc = Nothing
        Me.txtSNo.ReferenceFieldName = Nothing
        Me.txtSNo.ReferenceTableName = Nothing
        Me.txtSNo.Size = New System.Drawing.Size(81, 21)
        Me.txtSNo.TabIndex = 8
        Me.txtSNo.Text = "0"
        Me.txtSNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNo.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(196, 163)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel1.TabIndex = 24
        Me.MyLabel1.Text = "SNo"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(5, 86)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 16
        Me.MyLabel3.Text = "Start Date"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(498, 6)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(88, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 21
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(101, 6)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAdvanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(349, 21)
        Me.txtCode.TabIndex = 18
        Me.txtCode.Value = ""
        '
        'dtStartDate
        '
        Me.dtStartDate.CalculationExpression = Nothing
        Me.dtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtStartDate.FieldCode = Nothing
        Me.dtStartDate.FieldDesc = Nothing
        Me.dtStartDate.FieldMaxLength = 0
        Me.dtStartDate.FieldName = Nothing
        Me.dtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.isCalculatedField = False
        Me.dtStartDate.IsSourceFromTable = False
        Me.dtStartDate.IsSourceFromValueList = False
        Me.dtStartDate.IsUnique = False
        Me.dtStartDate.Location = New System.Drawing.Point(101, 85)
        Me.dtStartDate.MendatroryField = True
        Me.dtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.MyLinkLable1 = Me.MyLabel3
        Me.dtStartDate.MyLinkLable2 = Nothing
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.ReferenceFieldDesc = Nothing
        Me.dtStartDate.ReferenceFieldName = Nothing
        Me.dtStartDate.ReferenceTableName = Nothing
        Me.dtStartDate.Size = New System.Drawing.Size(79, 18)
        Me.dtStartDate.TabIndex = 1
        Me.dtStartDate.TabStop = False
        Me.dtStartDate.Text = "13/06/2011"
        Me.dtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkInactive
        '
        Me.chkInactive.Enabled = False
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(592, 8)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 16)
        Me.chkInactive.TabIndex = 22
        Me.chkInactive.Text = "Inactive"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(449, 6)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(22, 21)
        Me.rdbtnreset.TabIndex = 19
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalculationExpression = Nothing
        Me.dtpEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpEndDate.FieldCode = Nothing
        Me.dtpEndDate.FieldDesc = Nothing
        Me.dtpEndDate.FieldMaxLength = 0
        Me.dtpEndDate.FieldName = Nothing
        Me.dtpEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.isCalculatedField = False
        Me.dtpEndDate.IsSourceFromTable = False
        Me.dtpEndDate.IsSourceFromValueList = False
        Me.dtpEndDate.IsUnique = False
        Me.dtpEndDate.Location = New System.Drawing.Point(230, 85)
        Me.dtpEndDate.MendatroryField = False
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.MyLinkLable1 = Me.MyLabel13
        Me.dtpEndDate.MyLinkLable2 = Nothing
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.ReferenceFieldDesc = Nothing
        Me.dtpEndDate.ReferenceFieldName = Nothing
        Me.dtpEndDate.ReferenceTableName = Nothing
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(91, 18)
        Me.dtpEndDate.TabIndex = 2
        Me.dtpEndDate.TabStop = False
        Me.dtpEndDate.Text = "13/06/2011"
        Me.dtpEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(179, 86)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel13.TabIndex = 11
        Me.MyLabel13.Text = "End Date"
        '
        'btnEndDate
        '
        Me.btnEndDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEndDate.Location = New System.Drawing.Point(212, 3)
        Me.btnEndDate.Name = "btnEndDate"
        Me.btnEndDate.Size = New System.Drawing.Size(109, 21)
        Me.btnEndDate.TabIndex = 3
        Me.btnEndDate.Text = "Update End Date"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(143, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 21)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(612, 3)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 21)
        Me.rdbtnclose.TabIndex = 4
        Me.rdbtnclose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(74, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 21)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 314)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel12.TabIndex = 386
        Me.MyLabel12.Text = "Exclude DCS"
        '
        'txtExcludeDCS
        '
        Me.txtExcludeDCS.arrDispalyMember = Nothing
        Me.txtExcludeDCS.arrValueMember = Nothing
        Me.txtExcludeDCS.Location = New System.Drawing.Point(101, 313)
        Me.txtExcludeDCS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExcludeDCS.MyLinkLable1 = Nothing
        Me.txtExcludeDCS.MyLinkLable2 = Nothing
        Me.txtExcludeDCS.MyNullText = "All"
        Me.txtExcludeDCS.Name = "txtExcludeDCS"
        Me.txtExcludeDCS.Size = New System.Drawing.Size(570, 20)
        Me.txtExcludeDCS.TabIndex = 385
        '
        'frmDCSAdditionDeduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(681, 449)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDCSAdditionDeduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "DCS Addition Deduction"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DescHindi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescNameHindi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnACNotExists, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnACExists, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSavingAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeShortageOwnBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSubtract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyTDS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpQtyUOM.ResumeLayout(False)
        Me.grpQtyUOM.PerformLayout()
        CType(Me.rbtnQtyUOMRec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnQtyUOMKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnQtyUOMLtr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboROIncreaseAfter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboRoundOFFDecimalPlaces, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtConvertsion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDCSTypeBMCTruckSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDCSTypeCluster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDCSTypeBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDCSTypePDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDCSTypeDCS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDCSTypeBoth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAdditionType.ResumeLayout(False)
        Me.grpAdditionType.PerformLayout()
        CType(Me.rbtnAdditionTypeNormal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAdditionTypeCompulsory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAdditionTypeSaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMappingCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGLAcctName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboApplyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboApplyOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplyValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.rbtnNatureTypeDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnNatureTypeAddition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblAdvanceCode As common.Controls.MyLabel
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescName As common.Controls.MyTextBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkInactive As RadCheckBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnPost As RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtSNo As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnEndDate As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents rbtnNatureTypeDeduction As common.Controls.MyRadioButton
    Friend WithEvents rbtnNatureTypeAddition As common.Controls.MyRadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnDCSTypePDCS As common.Controls.MyRadioButton
    Friend WithEvents rbtnDCSTypeDCS As common.Controls.MyRadioButton
    Friend WithEvents rbtnDCSTypeBoth As common.Controls.MyRadioButton
    Friend WithEvents txtApplyValue As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboApplyType As common.Controls.MyComboBox
    Friend WithEvents cboApplyOn As common.Controls.MyComboBox
    Friend WithEvents txtGLAccount As common.UserControls.txtFinder
    Friend WithEvents lblGLAcctName As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents rbtnDCSTypeCluster As common.Controls.MyRadioButton
    Friend WithEvents rbtnDCSTypeBMC As common.Controls.MyRadioButton
    Friend WithEvents rbtnDCSTypeBMCTruckSheet As common.Controls.MyRadioButton
    Friend WithEvents lblMappingCodeDesc As common.Controls.MyLabel
    Friend WithEvents txtMappingCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents grpAdditionType As GroupBox
    Friend WithEvents rbtnAdditionTypeNormal As common.Controls.MyRadioButton
    Friend WithEvents rbtnAdditionTypeCompulsory As common.Controls.MyRadioButton
    Friend WithEvents rbtnAdditionTypeSaving As common.Controls.MyRadioButton
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtAddAmount As common.UserControls.txtMultiSelectFinder
    Friend WithEvents cboROIncreaseAfter As common.Controls.MyComboBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents cboRoundOFFDecimalPlaces As common.Controls.MyComboBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents grpQtyUOM As GroupBox
    Friend WithEvents rbtnQtyUOMRec As common.Controls.MyRadioButton
    Friend WithEvents rbtnQtyUOMKG As common.Controls.MyRadioButton
    Friend WithEvents rbtnQtyUOMLtr As common.Controls.MyRadioButton
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtMilkType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkApplyTDS As RadCheckBox
    Friend WithEvents chkIncludeShortageOwnBMC As RadCheckBox
    Friend WithEvents chkSubtract As RadCheckBox
    Friend WithEvents chkSavingAC As RadCheckBox
    Friend WithEvents rbtnACNotExists As common.Controls.MyRadioButton
    Friend WithEvents rbtnACExists As common.Controls.MyRadioButton
    Friend WithEvents txtConvertsion As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents DescHindi As common.Controls.MyLabel
    Friend WithEvents txtDescNameHindi As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtExcludeDCS As common.UserControls.txtMultiSelectFinder
End Class
