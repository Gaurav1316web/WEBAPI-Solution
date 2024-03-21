Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBranchDetails
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
        Me.gboxBranchDetails = New Telerik.WinControls.UI.RadGroupBox()
        Me.FndBranchCodeNew = New common.UserControls.txtNavigator()
        Me.lblBranchCode = New common.Controls.MyLabel()
        Me.fndStateCodeNew = New common.UserControls.txtFinder()
        Me.lblStateCode = New common.Controls.MyLabel()
        Me.fndResponsiblePersonNew = New common.UserControls.txtFinder()
        Me.lblResponsiblePerson = New common.Controls.MyLabel()
        Me.fndBankNew = New common.UserControls.txtFinder()
        Me.lblBank = New common.Controls.MyLabel()
        Me.fndPenaltyAccountNew = New common.UserControls.txtFinder()
        Me.lblPenaltyAccount = New common.Controls.MyLabel()
        Me.fndOtherAccountNew = New common.UserControls.txtFinder()
        Me.lblOtherAccount = New common.Controls.MyLabel()
        Me.FndInterestAccountNew = New common.UserControls.txtFinder()
        Me.lblInterestAccount = New common.Controls.MyLabel()
        Me.fndTaxAccountNew = New common.UserControls.txtFinder()
        Me.lbltaxAccount = New common.Controls.MyLabel()
        Me.rtxtdate = New common.Controls.MyTextBox()
        Me.rchkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblInactive = New common.Controls.MyLabel()
        Me.rtxtStateCode = New common.Controls.MyTextBox()
        Me.rtxtResponsiblePerson = New common.Controls.MyTextBox()
        Me.rtxtRemitTo = New common.Controls.MyTextBox()
        Me.lblRemitTo = New common.Controls.MyLabel()
        Me.rtxtBank = New common.Controls.MyTextBox()
        Me.rtxtCircleCode = New common.Controls.MyTextBox()
        Me.lblCircleCode = New common.Controls.MyLabel()
        Me.rtxtPenaltyAccount = New common.Controls.MyTextBox()
        Me.rtxtOtherAccount = New common.Controls.MyTextBox()
        Me.rtxtInterestAccount = New common.Controls.MyTextBox()
        Me.rtxtTaxAccount = New common.Controls.MyTextBox()
        Me.txtBranchName = New common.Controls.MyTextBox()
        Me.lblBranchName = New common.Controls.MyLabel()
        Me.rdbtnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.radmenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.gboxBranchDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxBranchDetails.SuspendLayout()
        CType(Me.lblBranchCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStateCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblResponsiblePerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPenaltyAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOtherAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInterestAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltaxAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rchkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtStateCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtResponsiblePerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtRemitTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemitTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtCircleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCircleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtPenaltyAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtOtherAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtInterestAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtTaxAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gboxBranchDetails
        '
        Me.gboxBranchDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gboxBranchDetails.Controls.Add(Me.FndBranchCodeNew)
        Me.gboxBranchDetails.Controls.Add(Me.fndStateCodeNew)
        Me.gboxBranchDetails.Controls.Add(Me.fndResponsiblePersonNew)
        Me.gboxBranchDetails.Controls.Add(Me.fndBankNew)
        Me.gboxBranchDetails.Controls.Add(Me.fndPenaltyAccountNew)
        Me.gboxBranchDetails.Controls.Add(Me.fndOtherAccountNew)
        Me.gboxBranchDetails.Controls.Add(Me.FndInterestAccountNew)
        Me.gboxBranchDetails.Controls.Add(Me.fndTaxAccountNew)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtdate)
        Me.gboxBranchDetails.Controls.Add(Me.rchkInactive)
        Me.gboxBranchDetails.Controls.Add(Me.lblInactive)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtStateCode)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtResponsiblePerson)
        Me.gboxBranchDetails.Controls.Add(Me.lblStateCode)
        Me.gboxBranchDetails.Controls.Add(Me.lblResponsiblePerson)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtRemitTo)
        Me.gboxBranchDetails.Controls.Add(Me.lblRemitTo)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtBank)
        Me.gboxBranchDetails.Controls.Add(Me.lblBank)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtCircleCode)
        Me.gboxBranchDetails.Controls.Add(Me.lblCircleCode)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtPenaltyAccount)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtOtherAccount)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtInterestAccount)
        Me.gboxBranchDetails.Controls.Add(Me.rtxtTaxAccount)
        Me.gboxBranchDetails.Controls.Add(Me.lblPenaltyAccount)
        Me.gboxBranchDetails.Controls.Add(Me.lblOtherAccount)
        Me.gboxBranchDetails.Controls.Add(Me.lblInterestAccount)
        Me.gboxBranchDetails.Controls.Add(Me.lbltaxAccount)
        Me.gboxBranchDetails.Controls.Add(Me.txtBranchName)
        Me.gboxBranchDetails.Controls.Add(Me.lblBranchName)
        Me.gboxBranchDetails.Controls.Add(Me.rdbtnRefresh)
        Me.gboxBranchDetails.Controls.Add(Me.lblBranchCode)
        Me.gboxBranchDetails.HeaderText = ""
        Me.gboxBranchDetails.Location = New System.Drawing.Point(36, 13)
        Me.gboxBranchDetails.Name = "gboxBranchDetails"
        Me.gboxBranchDetails.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gboxBranchDetails.Size = New System.Drawing.Size(657, 373)
        Me.gboxBranchDetails.TabIndex = 0
        '
        'FndBranchCodeNew
        '
        Me.FndBranchCodeNew.FieldName = Nothing
        Me.FndBranchCodeNew.Location = New System.Drawing.Point(139, 18)
        Me.FndBranchCodeNew.MendatroryField = False
        Me.FndBranchCodeNew.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.FndBranchCodeNew.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.FndBranchCodeNew.MyLinkLable1 = Me.lblBranchCode
        Me.FndBranchCodeNew.MyLinkLable2 = Nothing
        Me.FndBranchCodeNew.MyMaxLength = 30
        Me.FndBranchCodeNew.MyReadOnly = False
        Me.FndBranchCodeNew.Name = "FndBranchCodeNew"
        Me.FndBranchCodeNew.Size = New System.Drawing.Size(233, 21)
        Me.FndBranchCodeNew.TabIndex = 0
        Me.FndBranchCodeNew.Value = ""
        '
        'lblBranchCode
        '
        Me.lblBranchCode.FieldName = Nothing
        Me.lblBranchCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranchCode.Location = New System.Drawing.Point(13, 23)
        Me.lblBranchCode.Name = "lblBranchCode"
        Me.lblBranchCode.Size = New System.Drawing.Size(72, 16)
        Me.lblBranchCode.TabIndex = 20
        Me.lblBranchCode.Text = "Branch Code"
        '
        'fndStateCodeNew
        '
        Me.fndStateCodeNew.CalculationExpression = Nothing
        Me.fndStateCodeNew.FieldCode = Nothing
        Me.fndStateCodeNew.FieldDesc = Nothing
        Me.fndStateCodeNew.FieldMaxLength = 0
        Me.fndStateCodeNew.FieldName = Nothing
        Me.fndStateCodeNew.isCalculatedField = False
        Me.fndStateCodeNew.IsSourceFromTable = False
        Me.fndStateCodeNew.IsSourceFromValueList = False
        Me.fndStateCodeNew.IsUnique = False
        Me.fndStateCodeNew.Location = New System.Drawing.Point(139, 263)
        Me.fndStateCodeNew.MendatroryField = False
        Me.fndStateCodeNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndStateCodeNew.MyLinkLable1 = Me.lblStateCode
        Me.fndStateCodeNew.MyLinkLable2 = Nothing
        Me.fndStateCodeNew.MyReadOnly = False
        Me.fndStateCodeNew.MyShowMasterFormButton = False
        Me.fndStateCodeNew.Name = "fndStateCodeNew"
        Me.fndStateCodeNew.ReferenceFieldDesc = Nothing
        Me.fndStateCodeNew.ReferenceFieldName = Nothing
        Me.fndStateCodeNew.ReferenceTableName = Nothing
        Me.fndStateCodeNew.Size = New System.Drawing.Size(222, 19)
        Me.fndStateCodeNew.TabIndex = 19
        Me.fndStateCodeNew.Value = ""
        '
        'lblStateCode
        '
        Me.lblStateCode.FieldName = Nothing
        Me.lblStateCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateCode.Location = New System.Drawing.Point(13, 266)
        Me.lblStateCode.Name = "lblStateCode"
        Me.lblStateCode.Size = New System.Drawing.Size(63, 16)
        Me.lblStateCode.TabIndex = 43
        Me.lblStateCode.Text = "State Code"
        '
        'fndResponsiblePersonNew
        '
        Me.fndResponsiblePersonNew.CalculationExpression = Nothing
        Me.fndResponsiblePersonNew.FieldCode = Nothing
        Me.fndResponsiblePersonNew.FieldDesc = Nothing
        Me.fndResponsiblePersonNew.FieldMaxLength = 0
        Me.fndResponsiblePersonNew.FieldName = Nothing
        Me.fndResponsiblePersonNew.isCalculatedField = False
        Me.fndResponsiblePersonNew.IsSourceFromTable = False
        Me.fndResponsiblePersonNew.IsSourceFromValueList = False
        Me.fndResponsiblePersonNew.IsUnique = False
        Me.fndResponsiblePersonNew.Location = New System.Drawing.Point(139, 240)
        Me.fndResponsiblePersonNew.MendatroryField = False
        Me.fndResponsiblePersonNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndResponsiblePersonNew.MyLinkLable1 = Me.lblResponsiblePerson
        Me.fndResponsiblePersonNew.MyLinkLable2 = Nothing
        Me.fndResponsiblePersonNew.MyReadOnly = False
        Me.fndResponsiblePersonNew.MyShowMasterFormButton = False
        Me.fndResponsiblePersonNew.Name = "fndResponsiblePersonNew"
        Me.fndResponsiblePersonNew.ReferenceFieldDesc = Nothing
        Me.fndResponsiblePersonNew.ReferenceFieldName = Nothing
        Me.fndResponsiblePersonNew.ReferenceTableName = Nothing
        Me.fndResponsiblePersonNew.Size = New System.Drawing.Size(222, 19)
        Me.fndResponsiblePersonNew.TabIndex = 17
        Me.fndResponsiblePersonNew.Value = ""
        '
        'lblResponsiblePerson
        '
        Me.lblResponsiblePerson.FieldName = Nothing
        Me.lblResponsiblePerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResponsiblePerson.Location = New System.Drawing.Point(13, 241)
        Me.lblResponsiblePerson.Name = "lblResponsiblePerson"
        Me.lblResponsiblePerson.Size = New System.Drawing.Size(108, 16)
        Me.lblResponsiblePerson.TabIndex = 42
        Me.lblResponsiblePerson.Text = "Responsible Person"
        '
        'fndBankNew
        '
        Me.fndBankNew.CalculationExpression = Nothing
        Me.fndBankNew.FieldCode = Nothing
        Me.fndBankNew.FieldDesc = Nothing
        Me.fndBankNew.FieldMaxLength = 0
        Me.fndBankNew.FieldName = Nothing
        Me.fndBankNew.isCalculatedField = False
        Me.fndBankNew.IsSourceFromTable = False
        Me.fndBankNew.IsSourceFromValueList = False
        Me.fndBankNew.IsUnique = False
        Me.fndBankNew.Location = New System.Drawing.Point(139, 193)
        Me.fndBankNew.MendatroryField = False
        Me.fndBankNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankNew.MyLinkLable1 = Me.lblBank
        Me.fndBankNew.MyLinkLable2 = Nothing
        Me.fndBankNew.MyReadOnly = False
        Me.fndBankNew.MyShowMasterFormButton = False
        Me.fndBankNew.Name = "fndBankNew"
        Me.fndBankNew.ReferenceFieldDesc = Nothing
        Me.fndBankNew.ReferenceFieldName = Nothing
        Me.fndBankNew.ReferenceTableName = Nothing
        Me.fndBankNew.Size = New System.Drawing.Size(222, 19)
        Me.fndBankNew.TabIndex = 14
        Me.fndBankNew.Value = ""
        '
        'lblBank
        '
        Me.lblBank.FieldName = Nothing
        Me.lblBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBank.Location = New System.Drawing.Point(13, 193)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(32, 16)
        Me.lblBank.TabIndex = 37
        Me.lblBank.Text = "Bank"
        '
        'fndPenaltyAccountNew
        '
        Me.fndPenaltyAccountNew.CalculationExpression = Nothing
        Me.fndPenaltyAccountNew.FieldCode = Nothing
        Me.fndPenaltyAccountNew.FieldDesc = Nothing
        Me.fndPenaltyAccountNew.FieldMaxLength = 0
        Me.fndPenaltyAccountNew.FieldName = Nothing
        Me.fndPenaltyAccountNew.isCalculatedField = False
        Me.fndPenaltyAccountNew.IsSourceFromTable = False
        Me.fndPenaltyAccountNew.IsSourceFromValueList = False
        Me.fndPenaltyAccountNew.IsUnique = False
        Me.fndPenaltyAccountNew.Location = New System.Drawing.Point(139, 142)
        Me.fndPenaltyAccountNew.MendatroryField = False
        Me.fndPenaltyAccountNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPenaltyAccountNew.MyLinkLable1 = Me.lblPenaltyAccount
        Me.fndPenaltyAccountNew.MyLinkLable2 = Nothing
        Me.fndPenaltyAccountNew.MyReadOnly = False
        Me.fndPenaltyAccountNew.MyShowMasterFormButton = False
        Me.fndPenaltyAccountNew.Name = "fndPenaltyAccountNew"
        Me.fndPenaltyAccountNew.ReferenceFieldDesc = Nothing
        Me.fndPenaltyAccountNew.ReferenceFieldName = Nothing
        Me.fndPenaltyAccountNew.ReferenceTableName = Nothing
        Me.fndPenaltyAccountNew.Size = New System.Drawing.Size(222, 19)
        Me.fndPenaltyAccountNew.TabIndex = 11
        Me.fndPenaltyAccountNew.Value = ""
        '
        'lblPenaltyAccount
        '
        Me.lblPenaltyAccount.FieldName = Nothing
        Me.lblPenaltyAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPenaltyAccount.Location = New System.Drawing.Point(13, 145)
        Me.lblPenaltyAccount.Name = "lblPenaltyAccount"
        Me.lblPenaltyAccount.Size = New System.Drawing.Size(88, 16)
        Me.lblPenaltyAccount.TabIndex = 24
        Me.lblPenaltyAccount.Text = "Penalty Account"
        '
        'fndOtherAccountNew
        '
        Me.fndOtherAccountNew.CalculationExpression = Nothing
        Me.fndOtherAccountNew.FieldCode = Nothing
        Me.fndOtherAccountNew.FieldDesc = Nothing
        Me.fndOtherAccountNew.FieldMaxLength = 0
        Me.fndOtherAccountNew.FieldName = Nothing
        Me.fndOtherAccountNew.isCalculatedField = False
        Me.fndOtherAccountNew.IsSourceFromTable = False
        Me.fndOtherAccountNew.IsSourceFromValueList = False
        Me.fndOtherAccountNew.IsUnique = False
        Me.fndOtherAccountNew.Location = New System.Drawing.Point(139, 118)
        Me.fndOtherAccountNew.MendatroryField = False
        Me.fndOtherAccountNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOtherAccountNew.MyLinkLable1 = Me.lblOtherAccount
        Me.fndOtherAccountNew.MyLinkLable2 = Nothing
        Me.fndOtherAccountNew.MyReadOnly = False
        Me.fndOtherAccountNew.MyShowMasterFormButton = False
        Me.fndOtherAccountNew.Name = "fndOtherAccountNew"
        Me.fndOtherAccountNew.ReferenceFieldDesc = Nothing
        Me.fndOtherAccountNew.ReferenceFieldName = Nothing
        Me.fndOtherAccountNew.ReferenceTableName = Nothing
        Me.fndOtherAccountNew.Size = New System.Drawing.Size(222, 19)
        Me.fndOtherAccountNew.TabIndex = 9
        Me.fndOtherAccountNew.Value = ""
        '
        'lblOtherAccount
        '
        Me.lblOtherAccount.FieldName = Nothing
        Me.lblOtherAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOtherAccount.Location = New System.Drawing.Point(13, 121)
        Me.lblOtherAccount.Name = "lblOtherAccount"
        Me.lblOtherAccount.Size = New System.Drawing.Size(79, 16)
        Me.lblOtherAccount.TabIndex = 25
        Me.lblOtherAccount.Text = "Other Account"
        '
        'FndInterestAccountNew
        '
        Me.FndInterestAccountNew.CalculationExpression = Nothing
        Me.FndInterestAccountNew.FieldCode = Nothing
        Me.FndInterestAccountNew.FieldDesc = Nothing
        Me.FndInterestAccountNew.FieldMaxLength = 0
        Me.FndInterestAccountNew.FieldName = Nothing
        Me.FndInterestAccountNew.isCalculatedField = False
        Me.FndInterestAccountNew.IsSourceFromTable = False
        Me.FndInterestAccountNew.IsSourceFromValueList = False
        Me.FndInterestAccountNew.IsUnique = False
        Me.FndInterestAccountNew.Location = New System.Drawing.Point(139, 93)
        Me.FndInterestAccountNew.MendatroryField = False
        Me.FndInterestAccountNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndInterestAccountNew.MyLinkLable1 = Me.lblInterestAccount
        Me.FndInterestAccountNew.MyLinkLable2 = Nothing
        Me.FndInterestAccountNew.MyReadOnly = False
        Me.FndInterestAccountNew.MyShowMasterFormButton = False
        Me.FndInterestAccountNew.Name = "FndInterestAccountNew"
        Me.FndInterestAccountNew.ReferenceFieldDesc = Nothing
        Me.FndInterestAccountNew.ReferenceFieldName = Nothing
        Me.FndInterestAccountNew.ReferenceTableName = Nothing
        Me.FndInterestAccountNew.Size = New System.Drawing.Size(222, 19)
        Me.FndInterestAccountNew.TabIndex = 7
        Me.FndInterestAccountNew.Value = ""
        '
        'lblInterestAccount
        '
        Me.lblInterestAccount.FieldName = Nothing
        Me.lblInterestAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInterestAccount.Location = New System.Drawing.Point(13, 96)
        Me.lblInterestAccount.Name = "lblInterestAccount"
        Me.lblInterestAccount.Size = New System.Drawing.Size(88, 16)
        Me.lblInterestAccount.TabIndex = 26
        Me.lblInterestAccount.Text = "Interest Account"
        '
        'fndTaxAccountNew
        '
        Me.fndTaxAccountNew.CalculationExpression = Nothing
        Me.fndTaxAccountNew.FieldCode = Nothing
        Me.fndTaxAccountNew.FieldDesc = Nothing
        Me.fndTaxAccountNew.FieldMaxLength = 0
        Me.fndTaxAccountNew.FieldName = Nothing
        Me.fndTaxAccountNew.isCalculatedField = False
        Me.fndTaxAccountNew.IsSourceFromTable = False
        Me.fndTaxAccountNew.IsSourceFromValueList = False
        Me.fndTaxAccountNew.IsUnique = False
        Me.fndTaxAccountNew.Location = New System.Drawing.Point(139, 70)
        Me.fndTaxAccountNew.MendatroryField = False
        Me.fndTaxAccountNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTaxAccountNew.MyLinkLable1 = Me.lbltaxAccount
        Me.fndTaxAccountNew.MyLinkLable2 = Nothing
        Me.fndTaxAccountNew.MyReadOnly = False
        Me.fndTaxAccountNew.MyShowMasterFormButton = False
        Me.fndTaxAccountNew.Name = "fndTaxAccountNew"
        Me.fndTaxAccountNew.ReferenceFieldDesc = Nothing
        Me.fndTaxAccountNew.ReferenceFieldName = Nothing
        Me.fndTaxAccountNew.ReferenceTableName = Nothing
        Me.fndTaxAccountNew.Size = New System.Drawing.Size(222, 19)
        Me.fndTaxAccountNew.TabIndex = 5
        Me.fndTaxAccountNew.Value = ""
        '
        'lbltaxAccount
        '
        Me.lbltaxAccount.FieldName = Nothing
        Me.lbltaxAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltaxAccount.Location = New System.Drawing.Point(13, 71)
        Me.lbltaxAccount.Name = "lbltaxAccount"
        Me.lbltaxAccount.Size = New System.Drawing.Size(72, 16)
        Me.lbltaxAccount.TabIndex = 23
        Me.lbltaxAccount.Text = "Tax Account "
        '
        'rtxtdate
        '
        Me.rtxtdate.CalculationExpression = Nothing
        Me.rtxtdate.FieldCode = Nothing
        Me.rtxtdate.FieldDesc = Nothing
        Me.rtxtdate.FieldMaxLength = 0
        Me.rtxtdate.FieldName = Nothing
        Me.rtxtdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtdate.isCalculatedField = False
        Me.rtxtdate.IsSourceFromTable = False
        Me.rtxtdate.IsSourceFromValueList = False
        Me.rtxtdate.IsUnique = False
        Me.rtxtdate.Location = New System.Drawing.Point(167, 292)
        Me.rtxtdate.MaxLength = 50
        Me.rtxtdate.MendatroryField = False
        Me.rtxtdate.MyLinkLable1 = Nothing
        Me.rtxtdate.MyLinkLable2 = Nothing
        Me.rtxtdate.Name = "rtxtdate"
        Me.rtxtdate.ReadOnly = True
        Me.rtxtdate.ReferenceFieldDesc = Nothing
        Me.rtxtdate.ReferenceFieldName = Nothing
        Me.rtxtdate.ReferenceTableName = Nothing
        Me.rtxtdate.Size = New System.Drawing.Size(79, 18)
        Me.rtxtdate.TabIndex = 21
        Me.rtxtdate.TabStop = False
        '
        'rchkInactive
        '
        Me.rchkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rchkInactive.Location = New System.Drawing.Point(139, 293)
        Me.rchkInactive.Name = "rchkInactive"
        Me.rchkInactive.Size = New System.Drawing.Size(15, 15)
        Me.rchkInactive.TabIndex = 19
        '
        'lblInactive
        '
        Me.lblInactive.FieldName = Nothing
        Me.lblInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInactive.Location = New System.Drawing.Point(13, 291)
        Me.lblInactive.Name = "lblInactive"
        Me.lblInactive.Size = New System.Drawing.Size(45, 16)
        Me.lblInactive.TabIndex = 48
        Me.lblInactive.Text = "Inactive"
        '
        'rtxtStateCode
        '
        Me.rtxtStateCode.CalculationExpression = Nothing
        Me.rtxtStateCode.FieldCode = Nothing
        Me.rtxtStateCode.FieldDesc = Nothing
        Me.rtxtStateCode.FieldMaxLength = 0
        Me.rtxtStateCode.FieldName = Nothing
        Me.rtxtStateCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtStateCode.isCalculatedField = False
        Me.rtxtStateCode.IsSourceFromTable = False
        Me.rtxtStateCode.IsSourceFromValueList = False
        Me.rtxtStateCode.IsUnique = False
        Me.rtxtStateCode.Location = New System.Drawing.Point(367, 266)
        Me.rtxtStateCode.MendatroryField = False
        Me.rtxtStateCode.MyLinkLable1 = Nothing
        Me.rtxtStateCode.MyLinkLable2 = Nothing
        Me.rtxtStateCode.Name = "rtxtStateCode"
        Me.rtxtStateCode.ReadOnly = True
        Me.rtxtStateCode.ReferenceFieldDesc = Nothing
        Me.rtxtStateCode.ReferenceFieldName = Nothing
        Me.rtxtStateCode.ReferenceTableName = Nothing
        Me.rtxtStateCode.Size = New System.Drawing.Size(268, 18)
        Me.rtxtStateCode.TabIndex = 20
        Me.rtxtStateCode.TabStop = False
        '
        'rtxtResponsiblePerson
        '
        Me.rtxtResponsiblePerson.CalculationExpression = Nothing
        Me.rtxtResponsiblePerson.FieldCode = Nothing
        Me.rtxtResponsiblePerson.FieldDesc = Nothing
        Me.rtxtResponsiblePerson.FieldMaxLength = 0
        Me.rtxtResponsiblePerson.FieldName = Nothing
        Me.rtxtResponsiblePerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtResponsiblePerson.isCalculatedField = False
        Me.rtxtResponsiblePerson.IsSourceFromTable = False
        Me.rtxtResponsiblePerson.IsSourceFromValueList = False
        Me.rtxtResponsiblePerson.IsUnique = False
        Me.rtxtResponsiblePerson.Location = New System.Drawing.Point(367, 241)
        Me.rtxtResponsiblePerson.MendatroryField = False
        Me.rtxtResponsiblePerson.MyLinkLable1 = Nothing
        Me.rtxtResponsiblePerson.MyLinkLable2 = Nothing
        Me.rtxtResponsiblePerson.Name = "rtxtResponsiblePerson"
        Me.rtxtResponsiblePerson.ReadOnly = True
        Me.rtxtResponsiblePerson.ReferenceFieldDesc = Nothing
        Me.rtxtResponsiblePerson.ReferenceFieldName = Nothing
        Me.rtxtResponsiblePerson.ReferenceTableName = Nothing
        Me.rtxtResponsiblePerson.Size = New System.Drawing.Size(268, 18)
        Me.rtxtResponsiblePerson.TabIndex = 18
        Me.rtxtResponsiblePerson.TabStop = False
        '
        'rtxtRemitTo
        '
        Me.rtxtRemitTo.CalculationExpression = Nothing
        Me.rtxtRemitTo.FieldCode = Nothing
        Me.rtxtRemitTo.FieldDesc = Nothing
        Me.rtxtRemitTo.FieldMaxLength = 0
        Me.rtxtRemitTo.FieldName = Nothing
        Me.rtxtRemitTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtRemitTo.isCalculatedField = False
        Me.rtxtRemitTo.IsSourceFromTable = False
        Me.rtxtRemitTo.IsSourceFromValueList = False
        Me.rtxtRemitTo.IsUnique = False
        Me.rtxtRemitTo.Location = New System.Drawing.Point(139, 218)
        Me.rtxtRemitTo.MaxLength = 50
        Me.rtxtRemitTo.MendatroryField = False
        Me.rtxtRemitTo.MyLinkLable1 = Me.lblRemitTo
        Me.rtxtRemitTo.MyLinkLable2 = Nothing
        Me.rtxtRemitTo.Name = "rtxtRemitTo"
        Me.rtxtRemitTo.ReferenceFieldDesc = Nothing
        Me.rtxtRemitTo.ReferenceFieldName = Nothing
        Me.rtxtRemitTo.ReferenceTableName = Nothing
        Me.rtxtRemitTo.Size = New System.Drawing.Size(496, 18)
        Me.rtxtRemitTo.TabIndex = 16
        '
        'lblRemitTo
        '
        Me.lblRemitTo.FieldName = Nothing
        Me.lblRemitTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemitTo.Location = New System.Drawing.Point(13, 217)
        Me.lblRemitTo.Name = "lblRemitTo"
        Me.lblRemitTo.Size = New System.Drawing.Size(52, 16)
        Me.lblRemitTo.TabIndex = 41
        Me.lblRemitTo.Text = "Remit To"
        '
        'rtxtBank
        '
        Me.rtxtBank.CalculationExpression = Nothing
        Me.rtxtBank.FieldCode = Nothing
        Me.rtxtBank.FieldDesc = Nothing
        Me.rtxtBank.FieldMaxLength = 0
        Me.rtxtBank.FieldName = Nothing
        Me.rtxtBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtBank.isCalculatedField = False
        Me.rtxtBank.IsSourceFromTable = False
        Me.rtxtBank.IsSourceFromValueList = False
        Me.rtxtBank.IsUnique = False
        Me.rtxtBank.Location = New System.Drawing.Point(367, 193)
        Me.rtxtBank.MendatroryField = False
        Me.rtxtBank.MyLinkLable1 = Nothing
        Me.rtxtBank.MyLinkLable2 = Nothing
        Me.rtxtBank.Name = "rtxtBank"
        Me.rtxtBank.ReadOnly = True
        Me.rtxtBank.ReferenceFieldDesc = Nothing
        Me.rtxtBank.ReferenceFieldName = Nothing
        Me.rtxtBank.ReferenceTableName = Nothing
        Me.rtxtBank.Size = New System.Drawing.Size(268, 18)
        Me.rtxtBank.TabIndex = 15
        Me.rtxtBank.TabStop = False
        '
        'rtxtCircleCode
        '
        Me.rtxtCircleCode.CalculationExpression = Nothing
        Me.rtxtCircleCode.FieldCode = Nothing
        Me.rtxtCircleCode.FieldDesc = Nothing
        Me.rtxtCircleCode.FieldMaxLength = 0
        Me.rtxtCircleCode.FieldName = Nothing
        Me.rtxtCircleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtCircleCode.isCalculatedField = False
        Me.rtxtCircleCode.IsSourceFromTable = False
        Me.rtxtCircleCode.IsSourceFromValueList = False
        Me.rtxtCircleCode.IsUnique = False
        Me.rtxtCircleCode.Location = New System.Drawing.Point(139, 169)
        Me.rtxtCircleCode.MaxLength = 50
        Me.rtxtCircleCode.MendatroryField = False
        Me.rtxtCircleCode.MyLinkLable1 = Me.lblCircleCode
        Me.rtxtCircleCode.MyLinkLable2 = Nothing
        Me.rtxtCircleCode.Name = "rtxtCircleCode"
        Me.rtxtCircleCode.ReferenceFieldDesc = Nothing
        Me.rtxtCircleCode.ReferenceFieldName = Nothing
        Me.rtxtCircleCode.ReferenceTableName = Nothing
        Me.rtxtCircleCode.Size = New System.Drawing.Size(496, 18)
        Me.rtxtCircleCode.TabIndex = 13
        '
        'lblCircleCode
        '
        Me.lblCircleCode.FieldName = Nothing
        Me.lblCircleCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCircleCode.Location = New System.Drawing.Point(13, 169)
        Me.lblCircleCode.Name = "lblCircleCode"
        Me.lblCircleCode.Size = New System.Drawing.Size(65, 16)
        Me.lblCircleCode.TabIndex = 36
        Me.lblCircleCode.Text = "Circle Code"
        '
        'rtxtPenaltyAccount
        '
        Me.rtxtPenaltyAccount.CalculationExpression = Nothing
        Me.rtxtPenaltyAccount.FieldCode = Nothing
        Me.rtxtPenaltyAccount.FieldDesc = Nothing
        Me.rtxtPenaltyAccount.FieldMaxLength = 0
        Me.rtxtPenaltyAccount.FieldName = Nothing
        Me.rtxtPenaltyAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtPenaltyAccount.isCalculatedField = False
        Me.rtxtPenaltyAccount.IsSourceFromTable = False
        Me.rtxtPenaltyAccount.IsSourceFromValueList = False
        Me.rtxtPenaltyAccount.IsUnique = False
        Me.rtxtPenaltyAccount.Location = New System.Drawing.Point(367, 145)
        Me.rtxtPenaltyAccount.MendatroryField = False
        Me.rtxtPenaltyAccount.MyLinkLable1 = Nothing
        Me.rtxtPenaltyAccount.MyLinkLable2 = Nothing
        Me.rtxtPenaltyAccount.Name = "rtxtPenaltyAccount"
        Me.rtxtPenaltyAccount.ReadOnly = True
        Me.rtxtPenaltyAccount.ReferenceFieldDesc = Nothing
        Me.rtxtPenaltyAccount.ReferenceFieldName = Nothing
        Me.rtxtPenaltyAccount.ReferenceTableName = Nothing
        Me.rtxtPenaltyAccount.Size = New System.Drawing.Size(268, 18)
        Me.rtxtPenaltyAccount.TabIndex = 12
        Me.rtxtPenaltyAccount.TabStop = False
        '
        'rtxtOtherAccount
        '
        Me.rtxtOtherAccount.CalculationExpression = Nothing
        Me.rtxtOtherAccount.FieldCode = Nothing
        Me.rtxtOtherAccount.FieldDesc = Nothing
        Me.rtxtOtherAccount.FieldMaxLength = 0
        Me.rtxtOtherAccount.FieldName = Nothing
        Me.rtxtOtherAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtOtherAccount.isCalculatedField = False
        Me.rtxtOtherAccount.IsSourceFromTable = False
        Me.rtxtOtherAccount.IsSourceFromValueList = False
        Me.rtxtOtherAccount.IsUnique = False
        Me.rtxtOtherAccount.Location = New System.Drawing.Point(367, 121)
        Me.rtxtOtherAccount.MendatroryField = False
        Me.rtxtOtherAccount.MyLinkLable1 = Nothing
        Me.rtxtOtherAccount.MyLinkLable2 = Nothing
        Me.rtxtOtherAccount.Name = "rtxtOtherAccount"
        Me.rtxtOtherAccount.ReadOnly = True
        Me.rtxtOtherAccount.ReferenceFieldDesc = Nothing
        Me.rtxtOtherAccount.ReferenceFieldName = Nothing
        Me.rtxtOtherAccount.ReferenceTableName = Nothing
        Me.rtxtOtherAccount.Size = New System.Drawing.Size(268, 18)
        Me.rtxtOtherAccount.TabIndex = 10
        Me.rtxtOtherAccount.TabStop = False
        '
        'rtxtInterestAccount
        '
        Me.rtxtInterestAccount.CalculationExpression = Nothing
        Me.rtxtInterestAccount.FieldCode = Nothing
        Me.rtxtInterestAccount.FieldDesc = Nothing
        Me.rtxtInterestAccount.FieldMaxLength = 0
        Me.rtxtInterestAccount.FieldName = Nothing
        Me.rtxtInterestAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtInterestAccount.isCalculatedField = False
        Me.rtxtInterestAccount.IsSourceFromTable = False
        Me.rtxtInterestAccount.IsSourceFromValueList = False
        Me.rtxtInterestAccount.IsUnique = False
        Me.rtxtInterestAccount.Location = New System.Drawing.Point(367, 96)
        Me.rtxtInterestAccount.MendatroryField = False
        Me.rtxtInterestAccount.MyLinkLable1 = Nothing
        Me.rtxtInterestAccount.MyLinkLable2 = Nothing
        Me.rtxtInterestAccount.Name = "rtxtInterestAccount"
        Me.rtxtInterestAccount.ReadOnly = True
        Me.rtxtInterestAccount.ReferenceFieldDesc = Nothing
        Me.rtxtInterestAccount.ReferenceFieldName = Nothing
        Me.rtxtInterestAccount.ReferenceTableName = Nothing
        Me.rtxtInterestAccount.Size = New System.Drawing.Size(268, 18)
        Me.rtxtInterestAccount.TabIndex = 8
        Me.rtxtInterestAccount.TabStop = False
        '
        'rtxtTaxAccount
        '
        Me.rtxtTaxAccount.CalculationExpression = Nothing
        Me.rtxtTaxAccount.FieldCode = Nothing
        Me.rtxtTaxAccount.FieldDesc = Nothing
        Me.rtxtTaxAccount.FieldMaxLength = 0
        Me.rtxtTaxAccount.FieldName = Nothing
        Me.rtxtTaxAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtTaxAccount.isCalculatedField = False
        Me.rtxtTaxAccount.IsSourceFromTable = False
        Me.rtxtTaxAccount.IsSourceFromValueList = False
        Me.rtxtTaxAccount.IsUnique = False
        Me.rtxtTaxAccount.Location = New System.Drawing.Point(367, 71)
        Me.rtxtTaxAccount.MendatroryField = False
        Me.rtxtTaxAccount.MyLinkLable1 = Nothing
        Me.rtxtTaxAccount.MyLinkLable2 = Nothing
        Me.rtxtTaxAccount.Name = "rtxtTaxAccount"
        Me.rtxtTaxAccount.ReadOnly = True
        Me.rtxtTaxAccount.ReferenceFieldDesc = Nothing
        Me.rtxtTaxAccount.ReferenceFieldName = Nothing
        Me.rtxtTaxAccount.ReferenceTableName = Nothing
        Me.rtxtTaxAccount.Size = New System.Drawing.Size(268, 18)
        Me.rtxtTaxAccount.TabIndex = 6
        Me.rtxtTaxAccount.TabStop = False
        '
        'txtBranchName
        '
        Me.txtBranchName.CalculationExpression = Nothing
        Me.txtBranchName.FieldCode = Nothing
        Me.txtBranchName.FieldDesc = Nothing
        Me.txtBranchName.FieldMaxLength = 0
        Me.txtBranchName.FieldName = Nothing
        Me.txtBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranchName.isCalculatedField = False
        Me.txtBranchName.IsSourceFromTable = False
        Me.txtBranchName.IsSourceFromValueList = False
        Me.txtBranchName.IsUnique = False
        Me.txtBranchName.Location = New System.Drawing.Point(139, 48)
        Me.txtBranchName.MaxLength = 50
        Me.txtBranchName.MendatroryField = False
        Me.txtBranchName.MyLinkLable1 = Me.lblBranchName
        Me.txtBranchName.MyLinkLable2 = Nothing
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.ReferenceFieldDesc = Nothing
        Me.txtBranchName.ReferenceFieldName = Nothing
        Me.txtBranchName.ReferenceTableName = Nothing
        Me.txtBranchName.Size = New System.Drawing.Size(496, 18)
        Me.txtBranchName.TabIndex = 4
        '
        'lblBranchName
        '
        Me.lblBranchName.FieldName = Nothing
        Me.lblBranchName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranchName.Location = New System.Drawing.Point(13, 46)
        Me.lblBranchName.Name = "lblBranchName"
        Me.lblBranchName.Size = New System.Drawing.Size(75, 16)
        Me.lblBranchName.TabIndex = 21
        Me.lblBranchName.Text = "Branch Name"
        '
        'rdbtnRefresh
        '
        Me.rdbtnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnRefresh.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.rdbtnRefresh.Location = New System.Drawing.Point(378, 17)
        Me.rdbtnRefresh.Name = "rdbtnRefresh"
        Me.rdbtnRefresh.Size = New System.Drawing.Size(16, 22)
        Me.rdbtnRefresh.TabIndex = 1
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(1237, 6)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 26
        Me.rbtnClose.Text = "Close"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(107, 6)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 23
        Me.rbtnDelete.Text = "Delete"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(35, 6)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 22
        Me.rbtnSave.Text = "Save"
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.radmenu})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(720, 20)
        Me.rdmenufile.TabIndex = 13
        '
        'radmenu
        '
        Me.radmenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemImport, Me.RadMenuItemExport, Me.RadMenuItemClose})
        Me.radmenu.Name = "radmenu"
        Me.radmenu.Text = "File"
        '
        'RadMenuItemImport
        '
        Me.RadMenuItemImport.Name = "RadMenuItemImport"
        Me.RadMenuItemImport.Text = "Import"
        '
        'RadMenuItemExport
        '
        Me.RadMenuItemExport.Name = "RadMenuItemExport"
        Me.RadMenuItemExport.Text = "Export"
        '
        'RadMenuItemClose
        '
        Me.RadMenuItemClose.Name = "RadMenuItemClose"
        Me.RadMenuItemClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gboxBranchDetails)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(720, 426)
        Me.SplitContainer1.SplitterDistance = 395
        Me.SplitContainer1.TabIndex = 14
        '
        'frmBranchDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 446)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmBranchDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Branch Details"
        CType(Me.gboxBranchDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxBranchDetails.ResumeLayout(False)
        Me.gboxBranchDetails.PerformLayout()
        CType(Me.lblBranchCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStateCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblResponsiblePerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPenaltyAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOtherAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInterestAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltaxAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rchkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtStateCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtResponsiblePerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtRemitTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemitTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtCircleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCircleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtPenaltyAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtOtherAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtInterestAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtTaxAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBranchName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gboxBranchDetails As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents radmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtBranchName As common.Controls.MyTextBox
    Friend WithEvents rdbtnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents rtxtPenaltyAccount As common.Controls.MyTextBox
    Friend WithEvents rtxtOtherAccount As common.Controls.MyTextBox
    Friend WithEvents rtxtInterestAccount As common.Controls.MyTextBox
    Friend WithEvents rtxtTaxAccount As common.Controls.MyTextBox
    Friend WithEvents rtxtCircleCode As common.Controls.MyTextBox
    Friend WithEvents rtxtBank As common.Controls.MyTextBox
    Friend WithEvents rtxtRemitTo As common.Controls.MyTextBox
    Friend WithEvents rtxtStateCode As common.Controls.MyTextBox
    Friend WithEvents rtxtResponsiblePerson As common.Controls.MyTextBox
    Friend WithEvents rchkInactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rtxtdate As common.Controls.MyTextBox
    Friend WithEvents RadMenuItemImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblBranchName As common.Controls.MyLabel
    Friend WithEvents lblBranchCode As common.Controls.MyLabel
    Friend WithEvents lblPenaltyAccount As common.Controls.MyLabel
    Friend WithEvents lblOtherAccount As common.Controls.MyLabel
    Friend WithEvents lblInterestAccount As common.Controls.MyLabel
    Friend WithEvents lbltaxAccount As common.Controls.MyLabel
    Friend WithEvents lblCircleCode As common.Controls.MyLabel
    Friend WithEvents lblBank As common.Controls.MyLabel
    Friend WithEvents lblRemitTo As common.Controls.MyLabel
    Friend WithEvents lblStateCode As common.Controls.MyLabel
    Friend WithEvents lblResponsiblePerson As common.Controls.MyLabel
    Friend WithEvents lblInactive As common.Controls.MyLabel
    Friend WithEvents fndTaxAccountNew As common.UserControls.txtFinder
    Friend WithEvents fndResponsiblePersonNew As common.UserControls.txtFinder
    Friend WithEvents fndBankNew As common.UserControls.txtFinder
    Friend WithEvents fndPenaltyAccountNew As common.UserControls.txtFinder
    Friend WithEvents fndOtherAccountNew As common.UserControls.txtFinder
    Friend WithEvents FndInterestAccountNew As common.UserControls.txtFinder
    Friend WithEvents fndStateCodeNew As common.UserControls.txtFinder
    Friend WithEvents FndBranchCodeNew As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

