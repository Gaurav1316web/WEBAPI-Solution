<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdditionalCharges
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
        Me.lblCode = New common.Controls.MyLabel()
        Me.grpbxAdditional = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkRoundoff = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkInsurance = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtabtmnt = New common.MyNumBox()
        Me.txtReverseChargePer = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtspecification = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkFreight = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtGLAccount = New common.UserControls.txtFinder()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.txtGlAccDesc = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.lbldesc = New common.Controls.MyLabel()
        Me.lblSacCodeDesc = New common.Controls.MyLabel()
        Me.fndSACcode = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.chkServiceType = New common.Controls.MyCheckBox()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.MenuRD = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuAll = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuCriteria = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RdbGST = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkNoGSTCredit = New common.Controls.MyCheckBox()
        Me.chkRCM = New common.Controls.MyCheckBox()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxAdditional, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxAdditional.SuspendLayout()
        CType(Me.chkRoundoff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInsurance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtabtmnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReverseChargePer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtspecification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGlAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSacCodeDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkServiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MenuRD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RdbGST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RdbGST.SuspendLayout()
        CType(Me.chkNoGSTCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRCM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(9, 20)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 18)
        Me.lblCode.TabIndex = 13
        Me.lblCode.Text = "Code"
        '
        'grpbxAdditional
        '
        Me.grpbxAdditional.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxAdditional.Controls.Add(Me.chkRoundoff)
        Me.grpbxAdditional.Controls.Add(Me.chkInsurance)
        Me.grpbxAdditional.Controls.Add(Me.txtabtmnt)
        Me.grpbxAdditional.Controls.Add(Me.txtReverseChargePer)
        Me.grpbxAdditional.Controls.Add(Me.MyLabel3)
        Me.grpbxAdditional.Controls.Add(Me.txtspecification)
        Me.grpbxAdditional.Controls.Add(Me.MyLabel2)
        Me.grpbxAdditional.Controls.Add(Me.MyLabel1)
        Me.grpbxAdditional.Controls.Add(Me.chkFreight)
        Me.grpbxAdditional.Controls.Add(Me.txtGLAccount)
        Me.grpbxAdditional.Controls.Add(Me.RadLabel5)
        Me.grpbxAdditional.Controls.Add(Me.txtGlAccDesc)
        Me.grpbxAdditional.Controls.Add(Me.RadLabel3)
        Me.grpbxAdditional.Controls.Add(Me.fndCode)
        Me.grpbxAdditional.Controls.Add(Me.btnreset)
        Me.grpbxAdditional.Controls.Add(Me.txtdesc)
        Me.grpbxAdditional.Controls.Add(Me.lbldesc)
        Me.grpbxAdditional.Controls.Add(Me.lblCode)
        Me.grpbxAdditional.HeaderText = ""
        Me.grpbxAdditional.Location = New System.Drawing.Point(20, 15)
        Me.grpbxAdditional.Name = "grpbxAdditional"
        Me.grpbxAdditional.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxAdditional.Size = New System.Drawing.Size(454, 207)
        Me.grpbxAdditional.TabIndex = 0
        '
        'chkRoundoff
        '
        Me.chkRoundoff.Location = New System.Drawing.Point(373, 68)
        Me.chkRoundoff.Name = "chkRoundoff"
        Me.chkRoundoff.Size = New System.Drawing.Size(70, 18)
        Me.chkRoundoff.TabIndex = 19
        Me.chkRoundoff.Text = "Round off"
        '
        'chkInsurance
        '
        Me.chkInsurance.Location = New System.Drawing.Point(300, 68)
        Me.chkInsurance.Name = "chkInsurance"
        Me.chkInsurance.Size = New System.Drawing.Size(68, 18)
        Me.chkInsurance.TabIndex = 18
        Me.chkInsurance.Text = "Insurance"
        '
        'txtabtmnt
        '
        Me.txtabtmnt.CalculationExpression = Nothing
        Me.txtabtmnt.DecimalPlaces = 0
        Me.txtabtmnt.FieldCode = Nothing
        Me.txtabtmnt.FieldDesc = Nothing
        Me.txtabtmnt.FieldMaxLength = 0
        Me.txtabtmnt.FieldName = Nothing
        Me.txtabtmnt.isCalculatedField = False
        Me.txtabtmnt.IsSourceFromTable = False
        Me.txtabtmnt.IsSourceFromValueList = False
        Me.txtabtmnt.IsUnique = False
        Me.txtabtmnt.Location = New System.Drawing.Point(93, 121)
        Me.txtabtmnt.MendatroryField = False
        Me.txtabtmnt.MyLinkLable1 = Nothing
        Me.txtabtmnt.MyLinkLable2 = Nothing
        Me.txtabtmnt.Name = "txtabtmnt"
        Me.txtabtmnt.ReferenceFieldDesc = Nothing
        Me.txtabtmnt.ReferenceFieldName = Nothing
        Me.txtabtmnt.ReferenceTableName = Nothing
        Me.txtabtmnt.Size = New System.Drawing.Size(80, 20)
        Me.txtabtmnt.TabIndex = 17
        Me.txtabtmnt.Text = "0"
        Me.txtabtmnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtabtmnt.Value = 0.0R
        '
        'txtReverseChargePer
        '
        Me.txtReverseChargePer.CalculationExpression = Nothing
        Me.txtReverseChargePer.DecimalPlaces = 0
        Me.txtReverseChargePer.FieldCode = Nothing
        Me.txtReverseChargePer.FieldDesc = Nothing
        Me.txtReverseChargePer.FieldMaxLength = 0
        Me.txtReverseChargePer.FieldName = Nothing
        Me.txtReverseChargePer.isCalculatedField = False
        Me.txtReverseChargePer.IsSourceFromTable = False
        Me.txtReverseChargePer.IsSourceFromValueList = False
        Me.txtReverseChargePer.IsUnique = False
        Me.txtReverseChargePer.Location = New System.Drawing.Point(280, 121)
        Me.txtReverseChargePer.MendatroryField = False
        Me.txtReverseChargePer.MyLinkLable1 = Nothing
        Me.txtReverseChargePer.MyLinkLable2 = Nothing
        Me.txtReverseChargePer.Name = "txtReverseChargePer"
        Me.txtReverseChargePer.ReferenceFieldDesc = Nothing
        Me.txtReverseChargePer.ReferenceFieldName = Nothing
        Me.txtReverseChargePer.ReferenceTableName = Nothing
        Me.txtReverseChargePer.Size = New System.Drawing.Size(80, 20)
        Me.txtReverseChargePer.TabIndex = 16
        Me.txtReverseChargePer.Text = "0"
        Me.txtReverseChargePer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtReverseChargePer.Value = 0.0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(178, 122)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(96, 18)
        Me.MyLabel3.TabIndex = 15
        Me.MyLabel3.Text = "Reverse Charge %"
        '
        'txtspecification
        '
        Me.txtspecification.AutoSize = False
        Me.txtspecification.CalculationExpression = Nothing
        Me.txtspecification.FieldCode = Nothing
        Me.txtspecification.FieldDesc = Nothing
        Me.txtspecification.FieldMaxLength = 0
        Me.txtspecification.FieldName = Nothing
        Me.txtspecification.isCalculatedField = False
        Me.txtspecification.IsSourceFromTable = False
        Me.txtspecification.IsSourceFromValueList = False
        Me.txtspecification.IsUnique = False
        Me.txtspecification.Location = New System.Drawing.Point(93, 145)
        Me.txtspecification.MaxLength = 1000
        Me.txtspecification.MendatroryField = False
        Me.txtspecification.Multiline = True
        Me.txtspecification.MyLinkLable1 = Me.MyLabel2
        Me.txtspecification.MyLinkLable2 = Nothing
        Me.txtspecification.Name = "txtspecification"
        Me.txtspecification.ReferenceFieldDesc = Nothing
        Me.txtspecification.ReferenceFieldName = Nothing
        Me.txtspecification.ReferenceTableName = Nothing
        Me.txtspecification.Size = New System.Drawing.Size(351, 53)
        Me.txtspecification.TabIndex = 7
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(9, 146)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel2.TabIndex = 8
        Me.MyLabel2.Text = "Specification"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(9, 122)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(74, 18)
        Me.MyLabel1.TabIndex = 9
        Me.MyLabel1.Text = "Abatement %"
        '
        'chkFreight
        '
        Me.chkFreight.Location = New System.Drawing.Point(240, 68)
        Me.chkFreight.Name = "chkFreight"
        Me.chkFreight.Size = New System.Drawing.Size(55, 18)
        Me.chkFreight.TabIndex = 4
        Me.chkFreight.Text = "Freight"
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
        Me.txtGLAccount.Location = New System.Drawing.Point(93, 68)
        Me.txtGLAccount.MendatroryField = True
        Me.txtGLAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGLAccount.MyLinkLable1 = Me.RadLabel5
        Me.txtGLAccount.MyLinkLable2 = Nothing
        Me.txtGLAccount.MyReadOnly = False
        Me.txtGLAccount.MyShowMasterFormButton = False
        Me.txtGLAccount.Name = "txtGLAccount"
        Me.txtGLAccount.ReferenceFieldDesc = Nothing
        Me.txtGLAccount.ReferenceFieldName = Nothing
        Me.txtGLAccount.ReferenceTableName = Nothing
        Me.txtGLAccount.Size = New System.Drawing.Size(143, 18)
        Me.txtGLAccount.TabIndex = 3
        Me.txtGLAccount.Value = ""
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Location = New System.Drawing.Point(9, 68)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(65, 18)
        Me.RadLabel5.TabIndex = 11
        Me.RadLabel5.Text = "GL/Account"
        '
        'txtGlAccDesc
        '
        Me.txtGlAccDesc.CalculationExpression = Nothing
        Me.txtGlAccDesc.FieldCode = Nothing
        Me.txtGlAccDesc.FieldDesc = Nothing
        Me.txtGlAccDesc.FieldMaxLength = 0
        Me.txtGlAccDesc.FieldName = Nothing
        Me.txtGlAccDesc.isCalculatedField = False
        Me.txtGlAccDesc.IsSourceFromTable = False
        Me.txtGlAccDesc.IsSourceFromValueList = False
        Me.txtGlAccDesc.IsUnique = False
        Me.txtGlAccDesc.Location = New System.Drawing.Point(93, 93)
        Me.txtGlAccDesc.MaxLength = 50
        Me.txtGlAccDesc.MendatroryField = False
        Me.txtGlAccDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtGlAccDesc.MyLinkLable2 = Nothing
        Me.txtGlAccDesc.Name = "txtGlAccDesc"
        Me.txtGlAccDesc.ReadOnly = True
        Me.txtGlAccDesc.ReferenceFieldDesc = Nothing
        Me.txtGlAccDesc.ReferenceFieldName = Nothing
        Me.txtGlAccDesc.ReferenceTableName = Nothing
        Me.txtGlAccDesc.Size = New System.Drawing.Size(351, 20)
        Me.txtGlAccDesc.TabIndex = 4
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(9, 94)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 18)
        Me.RadLabel3.TabIndex = 10
        Me.RadLabel3.Text = "Description"
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(93, 19)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.lblCode
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 32767
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(268, 21)
        Me.fndCode.TabIndex = 0
        Me.fndCode.Value = ""
        '
        'btnreset
        '
        Me.btnreset.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnreset.Location = New System.Drawing.Point(361, 19)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(20, 21)
        Me.btnreset.TabIndex = 1
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(93, 43)
        Me.txtdesc.MendatroryField = False
        Me.txtdesc.MyLinkLable1 = Me.lbldesc
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(351, 20)
        Me.txtdesc.TabIndex = 2
        '
        'lbldesc
        '
        Me.lbldesc.FieldName = Nothing
        Me.lbldesc.Location = New System.Drawing.Point(9, 44)
        Me.lbldesc.Name = "lbldesc"
        Me.lbldesc.Size = New System.Drawing.Size(63, 18)
        Me.lbldesc.TabIndex = 12
        Me.lbldesc.Text = "Description"
        '
        'lblSacCodeDesc
        '
        Me.lblSacCodeDesc.AutoSize = False
        Me.lblSacCodeDesc.BorderVisible = True
        Me.lblSacCodeDesc.FieldName = Nothing
        Me.lblSacCodeDesc.Location = New System.Drawing.Point(240, 40)
        Me.lblSacCodeDesc.Name = "lblSacCodeDesc"
        Me.lblSacCodeDesc.Size = New System.Drawing.Size(202, 19)
        Me.lblSacCodeDesc.TabIndex = 277
        Me.lblSacCodeDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fndSACcode
        '
        Me.fndSACcode.CalculationExpression = Nothing
        Me.fndSACcode.FieldCode = Nothing
        Me.fndSACcode.FieldDesc = Nothing
        Me.fndSACcode.FieldMaxLength = 0
        Me.fndSACcode.FieldName = Nothing
        Me.fndSACcode.isCalculatedField = False
        Me.fndSACcode.IsSourceFromTable = False
        Me.fndSACcode.IsSourceFromValueList = False
        Me.fndSACcode.IsUnique = False
        Me.fndSACcode.Location = New System.Drawing.Point(91, 40)
        Me.fndSACcode.MendatroryField = False
        Me.fndSACcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSACcode.MyLinkLable1 = Me.lblSacCodeDesc
        Me.fndSACcode.MyLinkLable2 = Nothing
        Me.fndSACcode.MyReadOnly = False
        Me.fndSACcode.MyShowMasterFormButton = False
        Me.fndSACcode.Name = "fndSACcode"
        Me.fndSACcode.ReferenceFieldDesc = Nothing
        Me.fndSACcode.ReferenceFieldName = Nothing
        Me.fndSACcode.ReferenceTableName = Nothing
        Me.fndSACcode.Size = New System.Drawing.Size(143, 19)
        Me.fndSACcode.TabIndex = 276
        Me.fndSACcode.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(7, 40)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel2.TabIndex = 67
        Me.RadLabel2.Text = "SAC Code"
        '
        'chkServiceType
        '
        Me.chkServiceType.Location = New System.Drawing.Point(91, 16)
        Me.chkServiceType.MyLinkLable1 = Nothing
        Me.chkServiceType.MyLinkLable2 = Nothing
        Me.chkServiceType.Name = "chkServiceType"
        Me.chkServiceType.Size = New System.Drawing.Size(82, 18)
        Me.chkServiceType.TabIndex = 66
        Me.chkServiceType.Tag1 = Nothing
        Me.chkServiceType.Text = "Service Type"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(408, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 19)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(92, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 19)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(20, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 19)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'MenuRD
        '
        Me.MenuRD.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.MenuRD.Location = New System.Drawing.Point(0, 0)
        Me.MenuRD.Name = "MenuRD"
        Me.MenuRD.Size = New System.Drawing.Size(492, 20)
        Me.MenuRD.TabIndex = 1
        Me.MenuRD.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "FIle"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Import"
        Me.RadMenuItem2.AccessibleName = "Import"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Export"
        Me.RadMenuItem3.AccessibleName = "Export"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuAll, Me.MenuCriteria})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Export"
        '
        'menuAll
        '
        Me.menuAll.AccessibleDescription = "All"
        Me.menuAll.AccessibleName = "menuAll"
        Me.menuAll.Name = "menuAll"
        Me.menuAll.Text = "All"
        '
        'MenuCriteria
        '
        Me.MenuCriteria.AccessibleDescription = "Set Criteria"
        Me.MenuCriteria.AccessibleName = "MenuCriteria"
        Me.MenuCriteria.Name = "MenuCriteria"
        Me.MenuCriteria.Text = "Set Criteria"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Exit"
        Me.RadMenuItem4.AccessibleName = "Exit"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Exit"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RdbGST)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpbxAdditional)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(492, 339)
        Me.SplitContainer1.SplitterDistance = 305
        Me.SplitContainer1.TabIndex = 0
        '
        'RdbGST
        '
        Me.RdbGST.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RdbGST.Controls.Add(Me.chkNoGSTCredit)
        Me.RdbGST.Controls.Add(Me.chkRCM)
        Me.RdbGST.Controls.Add(Me.lblSacCodeDesc)
        Me.RdbGST.Controls.Add(Me.chkServiceType)
        Me.RdbGST.Controls.Add(Me.fndSACcode)
        Me.RdbGST.Controls.Add(Me.RadLabel2)
        Me.RdbGST.HeaderText = "GST"
        Me.RdbGST.Location = New System.Drawing.Point(20, 223)
        Me.RdbGST.Name = "RdbGST"
        Me.RdbGST.Size = New System.Drawing.Size(454, 70)
        Me.RdbGST.TabIndex = 1
        Me.RdbGST.Text = "GST"
        '
        'chkNoGSTCredit
        '
        Me.chkNoGSTCredit.Location = New System.Drawing.Point(298, 17)
        Me.chkNoGSTCredit.MyLinkLable1 = Nothing
        Me.chkNoGSTCredit.MyLinkLable2 = Nothing
        Me.chkNoGSTCredit.Name = "chkNoGSTCredit"
        Me.chkNoGSTCredit.Size = New System.Drawing.Size(93, 18)
        Me.chkNoGSTCredit.TabIndex = 279
        Me.chkNoGSTCredit.Tag1 = Nothing
        Me.chkNoGSTCredit.Text = "NO GST Credit"
        '
        'chkRCM
        '
        Me.chkRCM.Location = New System.Drawing.Point(240, 17)
        Me.chkRCM.MyLinkLable1 = Nothing
        Me.chkRCM.MyLinkLable2 = Nothing
        Me.chkRCM.Name = "chkRCM"
        Me.chkRCM.Size = New System.Drawing.Size(44, 18)
        Me.chkRCM.TabIndex = 278
        Me.chkRCM.Tag1 = Nothing
        Me.chkRCM.Text = "RCM"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(161, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 19)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "&History"
        '
        'FrmAdditionalCharges
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 359)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuRD)
        Me.Name = "FrmAdditionalCharges"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Additional Charges"
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxAdditional, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxAdditional.ResumeLayout(False)
        Me.grpbxAdditional.PerformLayout()
        CType(Me.chkRoundoff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInsurance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtabtmnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReverseChargePer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtspecification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGlAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSacCodeDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkServiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MenuRD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RdbGST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RdbGST.ResumeLayout(False)
        Me.RdbGST.PerformLayout()
        CType(Me.chkNoGSTCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRCM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpbxAdditional As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents MenuRD As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuAll As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuCriteria As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtGLAccount As common.UserControls.txtFinder
    Friend WithEvents txtGlAccDesc As common.Controls.MyTextBox
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lbldesc As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents chkFreight As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtspecification As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtabtmnt As common.MyNumBox
    Friend WithEvents txtReverseChargePer As common.MyNumBox
    Friend WithEvents chkServiceType As common.Controls.MyCheckBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents fndSACcode As common.UserControls.txtFinder
    Friend WithEvents lblSacCodeDesc As common.Controls.MyLabel
    Friend WithEvents RdbGST As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkRCM As common.Controls.MyCheckBox
    Friend WithEvents chkNoGSTCredit As common.Controls.MyCheckBox
    Friend WithEvents chkInsurance As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRoundoff As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

