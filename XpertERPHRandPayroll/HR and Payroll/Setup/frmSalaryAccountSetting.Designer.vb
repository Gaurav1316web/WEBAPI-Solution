Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryAccountSetting
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
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdgpbxcustomeraccountset = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblOthrPayableDesc = New common.Controls.MyTextBox()
        Me.rdlbldescription = New common.Controls.MyLabel()
        Me.fndOthrPayable = New common.UserControls.txtFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblESIPayableAccDesc = New common.Controls.MyTextBox()
        Me.fndESIPayableAcc = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblPFPayableAccDesc = New common.Controls.MyTextBox()
        Me.fndPFPayableAcc = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fndSourceCode = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtSourceCodeName = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtBankGLAccount = New common.Controls.MyTextBox()
        Me.lblBankAccount = New common.Controls.MyLabel()
        Me.fndBankAccount = New common.UserControls.txtFinder()
        Me.txtBankAccountName = New common.Controls.MyTextBox()
        Me.txtSalaryPayableAccountDesc = New common.Controls.MyTextBox()
        Me.fndSalaryPayableAccount = New common.UserControls.txtFinder()
        Me.lblSalaryPayableAccount = New common.Controls.MyLabel()
        Me.fndaccountsetcode = New common.UserControls.txtNavigator()
        Me.rdlblAccountsetcode = New common.Controls.MyLabel()
        Me.txtAccdescription = New common.Controls.MyTextBox()
        Me.rdbtnnew = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rdgpbxcustomeraccountset.SuspendLayout()
        CType(Me.lblOthrPayableDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblESIPayableAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPFPayableAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSourceCodeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankGLAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankAccountName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalaryPayableAccountDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(648, 4)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtndelete.Location = New System.Drawing.Point(102, 4)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(80, 18)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(19, 4)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdgpbxcustomeraccountset
        '
        Me.rdgpbxcustomeraccountset.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblOthrPayableDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndOthrPayable)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblESIPayableAccDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndESIPayableAcc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel5)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel4)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblPFPayableAccDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndPFPayableAcc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel3)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndSourceCode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel2)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtSourceCodeName)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.MyLabel1)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtBankGLAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndBankAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblBankAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtBankAccountName)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtSalaryPayableAccountDesc)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndSalaryPayableAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.lblSalaryPayableAccount)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.fndaccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlblAccountsetcode)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdlbldescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.txtAccdescription)
        Me.rdgpbxcustomeraccountset.Controls.Add(Me.rdbtnnew)
        Me.rdgpbxcustomeraccountset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdgpbxcustomeraccountset.HeaderText = ""
        Me.rdgpbxcustomeraccountset.Location = New System.Drawing.Point(19, 16)
        Me.rdgpbxcustomeraccountset.Name = "rdgpbxcustomeraccountset"
        Me.rdgpbxcustomeraccountset.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.rdgpbxcustomeraccountset.Size = New System.Drawing.Size(727, 235)
        Me.rdgpbxcustomeraccountset.TabIndex = 0
        '
        'lblOthrPayableDesc
        '
        Me.lblOthrPayableDesc.CalculationExpression = Nothing
        Me.lblOthrPayableDesc.FieldCode = Nothing
        Me.lblOthrPayableDesc.FieldDesc = Nothing
        Me.lblOthrPayableDesc.FieldMaxLength = 0
        Me.lblOthrPayableDesc.FieldName = Nothing
        Me.lblOthrPayableDesc.isCalculatedField = False
        Me.lblOthrPayableDesc.IsSourceFromTable = False
        Me.lblOthrPayableDesc.IsSourceFromValueList = False
        Me.lblOthrPayableDesc.IsUnique = False
        Me.lblOthrPayableDesc.Location = New System.Drawing.Point(386, 195)
        Me.lblOthrPayableDesc.MaxLength = 50
        Me.lblOthrPayableDesc.MendatroryField = False
        Me.lblOthrPayableDesc.MyLinkLable1 = Me.rdlbldescription
        Me.lblOthrPayableDesc.MyLinkLable2 = Nothing
        Me.lblOthrPayableDesc.Name = "lblOthrPayableDesc"
        Me.lblOthrPayableDesc.ReadOnly = True
        Me.lblOthrPayableDesc.ReferenceFieldDesc = Nothing
        Me.lblOthrPayableDesc.ReferenceFieldName = Nothing
        Me.lblOthrPayableDesc.ReferenceTableName = Nothing
        Me.lblOthrPayableDesc.Size = New System.Drawing.Size(282, 20)
        Me.lblOthrPayableDesc.TabIndex = 145
        Me.lblOthrPayableDesc.TabStop = False
        '
        'rdlbldescription
        '
        Me.rdlbldescription.FieldName = Nothing
        Me.rdlbldescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbldescription.Location = New System.Drawing.Point(20, 40)
        Me.rdlbldescription.Name = "rdlbldescription"
        Me.rdlbldescription.Size = New System.Drawing.Size(63, 16)
        Me.rdlbldescription.TabIndex = 9
        Me.rdlbldescription.Text = "Description"
        '
        'fndOthrPayable
        '
        Me.fndOthrPayable.CalculationExpression = Nothing
        Me.fndOthrPayable.FieldCode = Nothing
        Me.fndOthrPayable.FieldDesc = Nothing
        Me.fndOthrPayable.FieldMaxLength = 0
        Me.fndOthrPayable.FieldName = Nothing
        Me.fndOthrPayable.isCalculatedField = False
        Me.fndOthrPayable.IsSourceFromTable = False
        Me.fndOthrPayable.IsSourceFromValueList = False
        Me.fndOthrPayable.IsUnique = False
        Me.fndOthrPayable.Location = New System.Drawing.Point(199, 196)
        Me.fndOthrPayable.MendatroryField = False
        Me.fndOthrPayable.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndOthrPayable.MyLinkLable1 = Me.MyLabel5
        Me.fndOthrPayable.MyLinkLable2 = Nothing
        Me.fndOthrPayable.MyReadOnly = False
        Me.fndOthrPayable.MyShowMasterFormButton = False
        Me.fndOthrPayable.Name = "fndOthrPayable"
        Me.fndOthrPayable.ReferenceFieldDesc = Nothing
        Me.fndOthrPayable.ReferenceFieldName = Nothing
        Me.fndOthrPayable.ReferenceTableName = Nothing
        Me.fndOthrPayable.Size = New System.Drawing.Size(185, 19)
        Me.fndOthrPayable.TabIndex = 144
        Me.fndOthrPayable.Value = ""
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(20, 198)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(130, 16)
        Me.MyLabel5.TabIndex = 146
        Me.MyLabel5.Text = "Employer Other Payable"
        '
        'lblESIPayableAccDesc
        '
        Me.lblESIPayableAccDesc.CalculationExpression = Nothing
        Me.lblESIPayableAccDesc.FieldCode = Nothing
        Me.lblESIPayableAccDesc.FieldDesc = Nothing
        Me.lblESIPayableAccDesc.FieldMaxLength = 0
        Me.lblESIPayableAccDesc.FieldName = Nothing
        Me.lblESIPayableAccDesc.isCalculatedField = False
        Me.lblESIPayableAccDesc.IsSourceFromTable = False
        Me.lblESIPayableAccDesc.IsSourceFromValueList = False
        Me.lblESIPayableAccDesc.IsUnique = False
        Me.lblESIPayableAccDesc.Location = New System.Drawing.Point(386, 172)
        Me.lblESIPayableAccDesc.MaxLength = 50
        Me.lblESIPayableAccDesc.MendatroryField = False
        Me.lblESIPayableAccDesc.MyLinkLable1 = Me.rdlbldescription
        Me.lblESIPayableAccDesc.MyLinkLable2 = Nothing
        Me.lblESIPayableAccDesc.Name = "lblESIPayableAccDesc"
        Me.lblESIPayableAccDesc.ReadOnly = True
        Me.lblESIPayableAccDesc.ReferenceFieldDesc = Nothing
        Me.lblESIPayableAccDesc.ReferenceFieldName = Nothing
        Me.lblESIPayableAccDesc.ReferenceTableName = Nothing
        Me.lblESIPayableAccDesc.Size = New System.Drawing.Size(282, 20)
        Me.lblESIPayableAccDesc.TabIndex = 142
        Me.lblESIPayableAccDesc.TabStop = False
        '
        'fndESIPayableAcc
        '
        Me.fndESIPayableAcc.CalculationExpression = Nothing
        Me.fndESIPayableAcc.FieldCode = Nothing
        Me.fndESIPayableAcc.FieldDesc = Nothing
        Me.fndESIPayableAcc.FieldMaxLength = 0
        Me.fndESIPayableAcc.FieldName = Nothing
        Me.fndESIPayableAcc.isCalculatedField = False
        Me.fndESIPayableAcc.IsSourceFromTable = False
        Me.fndESIPayableAcc.IsSourceFromValueList = False
        Me.fndESIPayableAcc.IsUnique = False
        Me.fndESIPayableAcc.Location = New System.Drawing.Point(199, 173)
        Me.fndESIPayableAcc.MendatroryField = False
        Me.fndESIPayableAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndESIPayableAcc.MyLinkLable1 = Me.MyLabel4
        Me.fndESIPayableAcc.MyLinkLable2 = Nothing
        Me.fndESIPayableAcc.MyReadOnly = False
        Me.fndESIPayableAcc.MyShowMasterFormButton = False
        Me.fndESIPayableAcc.Name = "fndESIPayableAcc"
        Me.fndESIPayableAcc.ReferenceFieldDesc = Nothing
        Me.fndESIPayableAcc.ReferenceFieldName = Nothing
        Me.fndESIPayableAcc.ReferenceTableName = Nothing
        Me.fndESIPayableAcc.Size = New System.Drawing.Size(185, 19)
        Me.fndESIPayableAcc.TabIndex = 141
        Me.fndESIPayableAcc.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(20, 175)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(164, 16)
        Me.MyLabel4.TabIndex = 143
        Me.MyLabel4.Text = "Employer ESI Payable Account"
        '
        'lblPFPayableAccDesc
        '
        Me.lblPFPayableAccDesc.CalculationExpression = Nothing
        Me.lblPFPayableAccDesc.FieldCode = Nothing
        Me.lblPFPayableAccDesc.FieldDesc = Nothing
        Me.lblPFPayableAccDesc.FieldMaxLength = 0
        Me.lblPFPayableAccDesc.FieldName = Nothing
        Me.lblPFPayableAccDesc.isCalculatedField = False
        Me.lblPFPayableAccDesc.IsSourceFromTable = False
        Me.lblPFPayableAccDesc.IsSourceFromValueList = False
        Me.lblPFPayableAccDesc.IsUnique = False
        Me.lblPFPayableAccDesc.Location = New System.Drawing.Point(386, 149)
        Me.lblPFPayableAccDesc.MaxLength = 50
        Me.lblPFPayableAccDesc.MendatroryField = False
        Me.lblPFPayableAccDesc.MyLinkLable1 = Me.rdlbldescription
        Me.lblPFPayableAccDesc.MyLinkLable2 = Nothing
        Me.lblPFPayableAccDesc.Name = "lblPFPayableAccDesc"
        Me.lblPFPayableAccDesc.ReadOnly = True
        Me.lblPFPayableAccDesc.ReferenceFieldDesc = Nothing
        Me.lblPFPayableAccDesc.ReferenceFieldName = Nothing
        Me.lblPFPayableAccDesc.ReferenceTableName = Nothing
        Me.lblPFPayableAccDesc.Size = New System.Drawing.Size(282, 20)
        Me.lblPFPayableAccDesc.TabIndex = 139
        Me.lblPFPayableAccDesc.TabStop = False
        '
        'fndPFPayableAcc
        '
        Me.fndPFPayableAcc.CalculationExpression = Nothing
        Me.fndPFPayableAcc.FieldCode = Nothing
        Me.fndPFPayableAcc.FieldDesc = Nothing
        Me.fndPFPayableAcc.FieldMaxLength = 0
        Me.fndPFPayableAcc.FieldName = Nothing
        Me.fndPFPayableAcc.isCalculatedField = False
        Me.fndPFPayableAcc.IsSourceFromTable = False
        Me.fndPFPayableAcc.IsSourceFromValueList = False
        Me.fndPFPayableAcc.IsUnique = False
        Me.fndPFPayableAcc.Location = New System.Drawing.Point(199, 150)
        Me.fndPFPayableAcc.MendatroryField = False
        Me.fndPFPayableAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPFPayableAcc.MyLinkLable1 = Me.MyLabel3
        Me.fndPFPayableAcc.MyLinkLable2 = Nothing
        Me.fndPFPayableAcc.MyReadOnly = False
        Me.fndPFPayableAcc.MyShowMasterFormButton = False
        Me.fndPFPayableAcc.Name = "fndPFPayableAcc"
        Me.fndPFPayableAcc.ReferenceFieldDesc = Nothing
        Me.fndPFPayableAcc.ReferenceFieldName = Nothing
        Me.fndPFPayableAcc.ReferenceTableName = Nothing
        Me.fndPFPayableAcc.Size = New System.Drawing.Size(185, 19)
        Me.fndPFPayableAcc.TabIndex = 138
        Me.fndPFPayableAcc.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(20, 152)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(160, 16)
        Me.MyLabel3.TabIndex = 140
        Me.MyLabel3.Text = "Employer PF Payable Account"
        '
        'fndSourceCode
        '
        Me.fndSourceCode.CalculationExpression = Nothing
        Me.fndSourceCode.FieldCode = Nothing
        Me.fndSourceCode.FieldDesc = Nothing
        Me.fndSourceCode.FieldMaxLength = 0
        Me.fndSourceCode.FieldName = Nothing
        Me.fndSourceCode.isCalculatedField = False
        Me.fndSourceCode.IsSourceFromTable = False
        Me.fndSourceCode.IsSourceFromValueList = False
        Me.fndSourceCode.IsUnique = False
        Me.fndSourceCode.Location = New System.Drawing.Point(199, 127)
        Me.fndSourceCode.MendatroryField = False
        Me.fndSourceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSourceCode.MyLinkLable1 = Me.MyLabel2
        Me.fndSourceCode.MyLinkLable2 = Nothing
        Me.fndSourceCode.MyReadOnly = False
        Me.fndSourceCode.MyShowMasterFormButton = False
        Me.fndSourceCode.Name = "fndSourceCode"
        Me.fndSourceCode.ReferenceFieldDesc = Nothing
        Me.fndSourceCode.ReferenceFieldName = Nothing
        Me.fndSourceCode.ReferenceTableName = Nothing
        Me.fndSourceCode.Size = New System.Drawing.Size(185, 19)
        Me.fndSourceCode.TabIndex = 8
        Me.fndSourceCode.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(21, 127)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel2.TabIndex = 137
        Me.MyLabel2.Text = "Source Code"
        '
        'txtSourceCodeName
        '
        Me.txtSourceCodeName.CalculationExpression = Nothing
        Me.txtSourceCodeName.FieldCode = Nothing
        Me.txtSourceCodeName.FieldDesc = Nothing
        Me.txtSourceCodeName.FieldMaxLength = 0
        Me.txtSourceCodeName.FieldName = Nothing
        Me.txtSourceCodeName.isCalculatedField = False
        Me.txtSourceCodeName.IsSourceFromTable = False
        Me.txtSourceCodeName.IsSourceFromValueList = False
        Me.txtSourceCodeName.IsUnique = False
        Me.txtSourceCodeName.Location = New System.Drawing.Point(386, 127)
        Me.txtSourceCodeName.MaxLength = 55
        Me.txtSourceCodeName.MendatroryField = False
        Me.txtSourceCodeName.MyLinkLable1 = Me.MyLabel2
        Me.txtSourceCodeName.MyLinkLable2 = Nothing
        Me.txtSourceCodeName.Name = "txtSourceCodeName"
        Me.txtSourceCodeName.ReadOnly = True
        Me.txtSourceCodeName.ReferenceFieldDesc = Nothing
        Me.txtSourceCodeName.ReferenceFieldName = Nothing
        Me.txtSourceCodeName.ReferenceTableName = Nothing
        Me.txtSourceCodeName.Size = New System.Drawing.Size(282, 20)
        Me.txtSourceCodeName.TabIndex = 9
        Me.txtSourceCodeName.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(20, 107)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(91, 18)
        Me.MyLabel1.TabIndex = 136
        Me.MyLabel1.Text = "Bank GL Account"
        '
        'txtBankGLAccount
        '
        Me.txtBankGLAccount.CalculationExpression = Nothing
        Me.txtBankGLAccount.FieldCode = Nothing
        Me.txtBankGLAccount.FieldDesc = Nothing
        Me.txtBankGLAccount.FieldMaxLength = 0
        Me.txtBankGLAccount.FieldName = Nothing
        Me.txtBankGLAccount.isCalculatedField = False
        Me.txtBankGLAccount.IsSourceFromTable = False
        Me.txtBankGLAccount.IsSourceFromValueList = False
        Me.txtBankGLAccount.IsUnique = False
        Me.txtBankGLAccount.Location = New System.Drawing.Point(199, 103)
        Me.txtBankGLAccount.MaxLength = 55
        Me.txtBankGLAccount.MendatroryField = False
        Me.txtBankGLAccount.MyLinkLable1 = Me.lblBankAccount
        Me.txtBankGLAccount.MyLinkLable2 = Nothing
        Me.txtBankGLAccount.Name = "txtBankGLAccount"
        Me.txtBankGLAccount.ReadOnly = True
        Me.txtBankGLAccount.ReferenceFieldDesc = Nothing
        Me.txtBankGLAccount.ReferenceFieldName = Nothing
        Me.txtBankGLAccount.ReferenceTableName = Nothing
        Me.txtBankGLAccount.Size = New System.Drawing.Size(185, 20)
        Me.txtBankGLAccount.TabIndex = 7
        Me.txtBankGLAccount.TabStop = False
        '
        'lblBankAccount
        '
        Me.lblBankAccount.FieldName = Nothing
        Me.lblBankAccount.Location = New System.Drawing.Point(20, 82)
        Me.lblBankAccount.Name = "lblBankAccount"
        Me.lblBankAccount.Size = New System.Drawing.Size(75, 18)
        Me.lblBankAccount.TabIndex = 132
        Me.lblBankAccount.Text = "Bank Account"
        '
        'fndBankAccount
        '
        Me.fndBankAccount.CalculationExpression = Nothing
        Me.fndBankAccount.FieldCode = Nothing
        Me.fndBankAccount.FieldDesc = Nothing
        Me.fndBankAccount.FieldMaxLength = 0
        Me.fndBankAccount.FieldName = Nothing
        Me.fndBankAccount.isCalculatedField = False
        Me.fndBankAccount.IsSourceFromTable = False
        Me.fndBankAccount.IsSourceFromValueList = False
        Me.fndBankAccount.IsUnique = False
        Me.fndBankAccount.Location = New System.Drawing.Point(199, 82)
        Me.fndBankAccount.MendatroryField = False
        Me.fndBankAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankAccount.MyLinkLable1 = Me.lblBankAccount
        Me.fndBankAccount.MyLinkLable2 = Nothing
        Me.fndBankAccount.MyReadOnly = False
        Me.fndBankAccount.MyShowMasterFormButton = False
        Me.fndBankAccount.Name = "fndBankAccount"
        Me.fndBankAccount.ReferenceFieldDesc = Nothing
        Me.fndBankAccount.ReferenceFieldName = Nothing
        Me.fndBankAccount.ReferenceTableName = Nothing
        Me.fndBankAccount.Size = New System.Drawing.Size(185, 19)
        Me.fndBankAccount.TabIndex = 5
        Me.fndBankAccount.Value = ""
        '
        'txtBankAccountName
        '
        Me.txtBankAccountName.CalculationExpression = Nothing
        Me.txtBankAccountName.FieldCode = Nothing
        Me.txtBankAccountName.FieldDesc = Nothing
        Me.txtBankAccountName.FieldMaxLength = 0
        Me.txtBankAccountName.FieldName = Nothing
        Me.txtBankAccountName.isCalculatedField = False
        Me.txtBankAccountName.IsSourceFromTable = False
        Me.txtBankAccountName.IsSourceFromValueList = False
        Me.txtBankAccountName.IsUnique = False
        Me.txtBankAccountName.Location = New System.Drawing.Point(386, 82)
        Me.txtBankAccountName.MaxLength = 55
        Me.txtBankAccountName.MendatroryField = False
        Me.txtBankAccountName.MyLinkLable1 = Me.lblBankAccount
        Me.txtBankAccountName.MyLinkLable2 = Nothing
        Me.txtBankAccountName.Name = "txtBankAccountName"
        Me.txtBankAccountName.ReadOnly = True
        Me.txtBankAccountName.ReferenceFieldDesc = Nothing
        Me.txtBankAccountName.ReferenceFieldName = Nothing
        Me.txtBankAccountName.ReferenceTableName = Nothing
        Me.txtBankAccountName.Size = New System.Drawing.Size(282, 20)
        Me.txtBankAccountName.TabIndex = 6
        Me.txtBankAccountName.TabStop = False
        '
        'txtSalaryPayableAccountDesc
        '
        Me.txtSalaryPayableAccountDesc.CalculationExpression = Nothing
        Me.txtSalaryPayableAccountDesc.FieldCode = Nothing
        Me.txtSalaryPayableAccountDesc.FieldDesc = Nothing
        Me.txtSalaryPayableAccountDesc.FieldMaxLength = 0
        Me.txtSalaryPayableAccountDesc.FieldName = Nothing
        Me.txtSalaryPayableAccountDesc.isCalculatedField = False
        Me.txtSalaryPayableAccountDesc.IsSourceFromTable = False
        Me.txtSalaryPayableAccountDesc.IsSourceFromValueList = False
        Me.txtSalaryPayableAccountDesc.IsUnique = False
        Me.txtSalaryPayableAccountDesc.Location = New System.Drawing.Point(386, 60)
        Me.txtSalaryPayableAccountDesc.MaxLength = 50
        Me.txtSalaryPayableAccountDesc.MendatroryField = False
        Me.txtSalaryPayableAccountDesc.MyLinkLable1 = Me.rdlbldescription
        Me.txtSalaryPayableAccountDesc.MyLinkLable2 = Nothing
        Me.txtSalaryPayableAccountDesc.Name = "txtSalaryPayableAccountDesc"
        Me.txtSalaryPayableAccountDesc.ReadOnly = True
        Me.txtSalaryPayableAccountDesc.ReferenceFieldDesc = Nothing
        Me.txtSalaryPayableAccountDesc.ReferenceFieldName = Nothing
        Me.txtSalaryPayableAccountDesc.ReferenceTableName = Nothing
        Me.txtSalaryPayableAccountDesc.Size = New System.Drawing.Size(282, 20)
        Me.txtSalaryPayableAccountDesc.TabIndex = 4
        Me.txtSalaryPayableAccountDesc.TabStop = False
        '
        'fndSalaryPayableAccount
        '
        Me.fndSalaryPayableAccount.CalculationExpression = Nothing
        Me.fndSalaryPayableAccount.FieldCode = Nothing
        Me.fndSalaryPayableAccount.FieldDesc = Nothing
        Me.fndSalaryPayableAccount.FieldMaxLength = 0
        Me.fndSalaryPayableAccount.FieldName = Nothing
        Me.fndSalaryPayableAccount.isCalculatedField = False
        Me.fndSalaryPayableAccount.IsSourceFromTable = False
        Me.fndSalaryPayableAccount.IsSourceFromValueList = False
        Me.fndSalaryPayableAccount.IsUnique = False
        Me.fndSalaryPayableAccount.Location = New System.Drawing.Point(199, 61)
        Me.fndSalaryPayableAccount.MendatroryField = False
        Me.fndSalaryPayableAccount.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalaryPayableAccount.MyLinkLable1 = Me.lblSalaryPayableAccount
        Me.fndSalaryPayableAccount.MyLinkLable2 = Nothing
        Me.fndSalaryPayableAccount.MyReadOnly = False
        Me.fndSalaryPayableAccount.MyShowMasterFormButton = False
        Me.fndSalaryPayableAccount.Name = "fndSalaryPayableAccount"
        Me.fndSalaryPayableAccount.ReferenceFieldDesc = Nothing
        Me.fndSalaryPayableAccount.ReferenceFieldName = Nothing
        Me.fndSalaryPayableAccount.ReferenceTableName = Nothing
        Me.fndSalaryPayableAccount.Size = New System.Drawing.Size(185, 19)
        Me.fndSalaryPayableAccount.TabIndex = 3
        Me.fndSalaryPayableAccount.Value = ""
        '
        'lblSalaryPayableAccount
        '
        Me.lblSalaryPayableAccount.FieldName = Nothing
        Me.lblSalaryPayableAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryPayableAccount.Location = New System.Drawing.Point(20, 62)
        Me.lblSalaryPayableAccount.Name = "lblSalaryPayableAccount"
        Me.lblSalaryPayableAccount.Size = New System.Drawing.Size(126, 16)
        Me.lblSalaryPayableAccount.TabIndex = 13
        Me.lblSalaryPayableAccount.Text = "Salary Payable Account"
        '
        'fndaccountsetcode
        '
        Me.fndaccountsetcode.FieldName = Nothing
        Me.fndaccountsetcode.Location = New System.Drawing.Point(199, 14)
        Me.fndaccountsetcode.MendatroryField = True
        Me.fndaccountsetcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndaccountsetcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndaccountsetcode.MyLinkLable1 = Me.rdlblAccountsetcode
        Me.fndaccountsetcode.MyLinkLable2 = Nothing
        Me.fndaccountsetcode.MyMaxLength = 30
        Me.fndaccountsetcode.MyReadOnly = False
        Me.fndaccountsetcode.Name = "fndaccountsetcode"
        Me.fndaccountsetcode.Size = New System.Drawing.Size(185, 21)
        Me.fndaccountsetcode.TabIndex = 0
        Me.fndaccountsetcode.Value = ""
        '
        'rdlblAccountsetcode
        '
        Me.rdlblAccountsetcode.FieldName = Nothing
        Me.rdlblAccountsetcode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.rdlblAccountsetcode.Location = New System.Drawing.Point(20, 20)
        Me.rdlblAccountsetcode.Name = "rdlblAccountsetcode"
        Me.rdlblAccountsetcode.Size = New System.Drawing.Size(93, 16)
        Me.rdlblAccountsetcode.TabIndex = 8
        Me.rdlblAccountsetcode.Text = "Account set code"
        '
        'txtAccdescription
        '
        Me.txtAccdescription.CalculationExpression = Nothing
        Me.txtAccdescription.FieldCode = Nothing
        Me.txtAccdescription.FieldDesc = Nothing
        Me.txtAccdescription.FieldMaxLength = 0
        Me.txtAccdescription.FieldName = Nothing
        Me.txtAccdescription.isCalculatedField = False
        Me.txtAccdescription.IsSourceFromTable = False
        Me.txtAccdescription.IsSourceFromValueList = False
        Me.txtAccdescription.IsUnique = False
        Me.txtAccdescription.Location = New System.Drawing.Point(199, 38)
        Me.txtAccdescription.MaxLength = 50
        Me.txtAccdescription.MendatroryField = True
        Me.txtAccdescription.MyLinkLable1 = Me.rdlbldescription
        Me.txtAccdescription.MyLinkLable2 = Nothing
        Me.txtAccdescription.Name = "txtAccdescription"
        Me.txtAccdescription.ReferenceFieldDesc = Nothing
        Me.txtAccdescription.ReferenceFieldName = Nothing
        Me.txtAccdescription.ReferenceTableName = Nothing
        Me.txtAccdescription.Size = New System.Drawing.Size(469, 20)
        Me.txtAccdescription.TabIndex = 2
        '
        'rdbtnnew
        '
        Me.rdbtnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.rdbtnnew.Location = New System.Drawing.Point(342, 15)
        Me.rdbtnnew.Name = "rdbtnnew"
        Me.rdbtnnew.Size = New System.Drawing.Size(18, 18)
        Me.rdbtnnew.TabIndex = 1
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdgpbxcustomeraccountset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(749, 283)
        Me.SplitContainer1.SplitterDistance = 254
        Me.SplitContainer1.TabIndex = 3
        '
        'RadMenu1
        '
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(749, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(333, 3)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(82, 18)
        Me.btnHistory.TabIndex = 3
        Me.btnHistory.Text = "History"
        '
        'frmSalaryAccountSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 303)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmSalaryAccountSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salary Account Setting"
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgpbxcustomeraccountset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rdgpbxcustomeraccountset.ResumeLayout(False)
        Me.rdgpbxcustomeraccountset.PerformLayout()
        CType(Me.lblOthrPayableDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbldescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblESIPayableAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPFPayableAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSourceCodeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankGLAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankAccountName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalaryPayableAccountDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlblAccountsetcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdgpbxcustomeraccountset As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtBankGLAccount As common.Controls.MyTextBox
    Friend WithEvents lblBankAccount As common.Controls.MyLabel
    Friend WithEvents fndBankAccount As common.UserControls.txtFinder
    Friend WithEvents txtBankAccountName As common.Controls.MyTextBox
    Friend WithEvents txtSalaryPayableAccountDesc As common.Controls.MyTextBox
    Friend WithEvents rdlbldescription As common.Controls.MyLabel
    Friend WithEvents fndSalaryPayableAccount As common.UserControls.txtFinder
    Friend WithEvents lblSalaryPayableAccount As common.Controls.MyLabel
    Friend WithEvents fndaccountsetcode As common.UserControls.txtNavigator
    Friend WithEvents rdlblAccountsetcode As common.Controls.MyLabel
    Friend WithEvents txtAccdescription As common.Controls.MyTextBox
    Friend WithEvents rdbtnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents fndSourceCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSourceCodeName As common.Controls.MyTextBox
    Friend WithEvents lblPFPayableAccDesc As common.Controls.MyTextBox
    Friend WithEvents fndPFPayableAcc As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblESIPayableAccDesc As common.Controls.MyTextBox
    Friend WithEvents fndESIPayableAcc As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblOthrPayableDesc As common.Controls.MyTextBox
    Friend WithEvents fndOthrPayable As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
End Class

