Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryGeneration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalaryGeneration))
        Me.txtOthrPayableDesc = New common.Controls.MyTextBox()
        Me.lblBankAccount = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtOthrPayableAcc = New common.Controls.MyTextBox()
        Me.txtESIPayableDesc = New common.Controls.MyTextBox()
        Me.txtPFPayableAccDesc = New common.Controls.MyTextBox()
        Me.txtSalaryPayableAccDesc = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtESIPayableAcc = New common.Controls.MyTextBox()
        Me.txtPFPayableAcc = New common.Controls.MyTextBox()
        Me.txtSalaryPayableAccount = New common.Controls.MyTextBox()
        Me.lblSalaryPayableAccount = New common.Controls.MyLabel()
        Me.fndSalaryAccountSett = New common.UserControls.txtFinder()
        Me.lblSalaryAccountSett = New common.Controls.MyLabel()
        Me.lblPayPeriodName = New common.Controls.MyLabel()
        Me.txtGeneratedBy = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRemark = New common.Controls.MyTextBox()
        Me.UsLock1 = New common.usLock()
        Me.lblPayPeriodDays = New common.Controls.MyLabel()
        Me.txtPayPeriodDays = New common.Controls.MyTextBox()
        Me.lblTo = New common.Controls.MyLabel()
        Me.lblDateFrom = New common.Controls.MyLabel()
        Me.dtpTo = New common.Controls.MyDateTimePicker()
        Me.dtpFrom = New common.Controls.MyDateTimePicker()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.findPayperiod = New common.UserControls.txtFinder()
        Me.lblPayPeriodCode = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.lblDivisionDesc = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.fndDivision = New common.UserControls.txtFinder()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtBranch = New common.UserControls.txtFinder()
        Me.chkCreateFE = New Telerik.WinControls.UI.RadCheckBox()
        Me.dtpChequeDated = New common.Controls.MyDateTimePicker()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtChequeNo = New common.Controls.MyTextBox()
        Me.btnCreateFE = New Telerik.WinControls.UI.RadButton()
        Me.btnSendMail = New Telerik.WinControls.UI.RadButton()
        Me.btnViewFE = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dtpGenerateDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblCompany = New common.Controls.MyLabel()
        Me.txtEmp = New common.UserControls.txtMultiSelectFinder()
        Me.txtpaymentDate = New common.Controls.MyDateTimePicker()
        Me.lblPaymentDate = New common.Controls.MyLabel()
        Me.fndBankCode = New common.UserControls.txtFinder()
        Me.lblBankCode = New common.Controls.MyLabel()
        CType(Me.txtOthrPayableDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOthrPayableAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESIPayableDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPFPayableAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalaryPayableAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESIPayableAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPFPayableAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalaryPayableAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalaryAccountSett, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivisionDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateFE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChequeDated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreateFE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendMail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewFE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dtpGenerateDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpaymentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPaymentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtOthrPayableDesc
        '
        Me.txtOthrPayableDesc.CalculationExpression = Nothing
        Me.txtOthrPayableDesc.FieldCode = Nothing
        Me.txtOthrPayableDesc.FieldDesc = Nothing
        Me.txtOthrPayableDesc.FieldMaxLength = 0
        Me.txtOthrPayableDesc.FieldName = Nothing
        Me.txtOthrPayableDesc.isCalculatedField = False
        Me.txtOthrPayableDesc.IsSourceFromTable = False
        Me.txtOthrPayableDesc.IsSourceFromValueList = False
        Me.txtOthrPayableDesc.IsUnique = False
        Me.txtOthrPayableDesc.Location = New System.Drawing.Point(448, 273)
        Me.txtOthrPayableDesc.MaxLength = 55
        Me.txtOthrPayableDesc.MendatroryField = False
        Me.txtOthrPayableDesc.MyLinkLable1 = Me.lblBankAccount
        Me.txtOthrPayableDesc.MyLinkLable2 = Nothing
        Me.txtOthrPayableDesc.Name = "txtOthrPayableDesc"
        Me.txtOthrPayableDesc.ReadOnly = True
        Me.txtOthrPayableDesc.ReferenceFieldDesc = Nothing
        Me.txtOthrPayableDesc.ReferenceFieldName = Nothing
        Me.txtOthrPayableDesc.ReferenceTableName = Nothing
        Me.txtOthrPayableDesc.Size = New System.Drawing.Size(259, 20)
        Me.txtOthrPayableDesc.TabIndex = 241
        '
        'lblBankAccount
        '
        Me.lblBankAccount.FieldName = Nothing
        Me.lblBankAccount.Location = New System.Drawing.Point(6, 228)
        Me.lblBankAccount.Name = "lblBankAccount"
        Me.lblBankAccount.Size = New System.Drawing.Size(131, 18)
        Me.lblBankAccount.TabIndex = 226
        Me.lblBankAccount.Text = "Employer PF Payable Acc"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(6, 274)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(127, 18)
        Me.MyLabel4.TabIndex = 240
        Me.MyLabel4.Text = "Employer Other Payable"
        '
        'txtOthrPayableAcc
        '
        Me.txtOthrPayableAcc.CalculationExpression = Nothing
        Me.txtOthrPayableAcc.FieldCode = Nothing
        Me.txtOthrPayableAcc.FieldDesc = Nothing
        Me.txtOthrPayableAcc.FieldMaxLength = 0
        Me.txtOthrPayableAcc.FieldName = Nothing
        Me.txtOthrPayableAcc.isCalculatedField = False
        Me.txtOthrPayableAcc.IsSourceFromTable = False
        Me.txtOthrPayableAcc.IsSourceFromValueList = False
        Me.txtOthrPayableAcc.IsUnique = False
        Me.txtOthrPayableAcc.Location = New System.Drawing.Point(141, 273)
        Me.txtOthrPayableAcc.MaxLength = 55
        Me.txtOthrPayableAcc.MendatroryField = False
        Me.txtOthrPayableAcc.MyLinkLable1 = Me.lblBankAccount
        Me.txtOthrPayableAcc.MyLinkLable2 = Nothing
        Me.txtOthrPayableAcc.Name = "txtOthrPayableAcc"
        Me.txtOthrPayableAcc.ReadOnly = True
        Me.txtOthrPayableAcc.ReferenceFieldDesc = Nothing
        Me.txtOthrPayableAcc.ReferenceFieldName = Nothing
        Me.txtOthrPayableAcc.ReferenceTableName = Nothing
        Me.txtOthrPayableAcc.Size = New System.Drawing.Size(301, 20)
        Me.txtOthrPayableAcc.TabIndex = 239
        '
        'txtESIPayableDesc
        '
        Me.txtESIPayableDesc.CalculationExpression = Nothing
        Me.txtESIPayableDesc.FieldCode = Nothing
        Me.txtESIPayableDesc.FieldDesc = Nothing
        Me.txtESIPayableDesc.FieldMaxLength = 0
        Me.txtESIPayableDesc.FieldName = Nothing
        Me.txtESIPayableDesc.isCalculatedField = False
        Me.txtESIPayableDesc.IsSourceFromTable = False
        Me.txtESIPayableDesc.IsSourceFromValueList = False
        Me.txtESIPayableDesc.IsUnique = False
        Me.txtESIPayableDesc.Location = New System.Drawing.Point(448, 250)
        Me.txtESIPayableDesc.MaxLength = 55
        Me.txtESIPayableDesc.MendatroryField = False
        Me.txtESIPayableDesc.MyLinkLable1 = Me.lblBankAccount
        Me.txtESIPayableDesc.MyLinkLable2 = Nothing
        Me.txtESIPayableDesc.Name = "txtESIPayableDesc"
        Me.txtESIPayableDesc.ReadOnly = True
        Me.txtESIPayableDesc.ReferenceFieldDesc = Nothing
        Me.txtESIPayableDesc.ReferenceFieldName = Nothing
        Me.txtESIPayableDesc.ReferenceTableName = Nothing
        Me.txtESIPayableDesc.Size = New System.Drawing.Size(259, 20)
        Me.txtESIPayableDesc.TabIndex = 238
        '
        'txtPFPayableAccDesc
        '
        Me.txtPFPayableAccDesc.CalculationExpression = Nothing
        Me.txtPFPayableAccDesc.FieldCode = Nothing
        Me.txtPFPayableAccDesc.FieldDesc = Nothing
        Me.txtPFPayableAccDesc.FieldMaxLength = 0
        Me.txtPFPayableAccDesc.FieldName = Nothing
        Me.txtPFPayableAccDesc.isCalculatedField = False
        Me.txtPFPayableAccDesc.IsSourceFromTable = False
        Me.txtPFPayableAccDesc.IsSourceFromValueList = False
        Me.txtPFPayableAccDesc.IsUnique = False
        Me.txtPFPayableAccDesc.Location = New System.Drawing.Point(448, 227)
        Me.txtPFPayableAccDesc.MaxLength = 55
        Me.txtPFPayableAccDesc.MendatroryField = False
        Me.txtPFPayableAccDesc.MyLinkLable1 = Me.lblBankAccount
        Me.txtPFPayableAccDesc.MyLinkLable2 = Nothing
        Me.txtPFPayableAccDesc.Name = "txtPFPayableAccDesc"
        Me.txtPFPayableAccDesc.ReadOnly = True
        Me.txtPFPayableAccDesc.ReferenceFieldDesc = Nothing
        Me.txtPFPayableAccDesc.ReferenceFieldName = Nothing
        Me.txtPFPayableAccDesc.ReferenceTableName = Nothing
        Me.txtPFPayableAccDesc.Size = New System.Drawing.Size(259, 20)
        Me.txtPFPayableAccDesc.TabIndex = 237
        '
        'txtSalaryPayableAccDesc
        '
        Me.txtSalaryPayableAccDesc.CalculationExpression = Nothing
        Me.txtSalaryPayableAccDesc.FieldCode = Nothing
        Me.txtSalaryPayableAccDesc.FieldDesc = Nothing
        Me.txtSalaryPayableAccDesc.FieldMaxLength = 0
        Me.txtSalaryPayableAccDesc.FieldName = Nothing
        Me.txtSalaryPayableAccDesc.isCalculatedField = False
        Me.txtSalaryPayableAccDesc.IsSourceFromTable = False
        Me.txtSalaryPayableAccDesc.IsSourceFromValueList = False
        Me.txtSalaryPayableAccDesc.IsUnique = False
        Me.txtSalaryPayableAccDesc.Location = New System.Drawing.Point(448, 204)
        Me.txtSalaryPayableAccDesc.MaxLength = 50
        Me.txtSalaryPayableAccDesc.MendatroryField = False
        Me.txtSalaryPayableAccDesc.MyLinkLable1 = Nothing
        Me.txtSalaryPayableAccDesc.MyLinkLable2 = Nothing
        Me.txtSalaryPayableAccDesc.Name = "txtSalaryPayableAccDesc"
        Me.txtSalaryPayableAccDesc.ReadOnly = True
        Me.txtSalaryPayableAccDesc.ReferenceFieldDesc = Nothing
        Me.txtSalaryPayableAccDesc.ReferenceFieldName = Nothing
        Me.txtSalaryPayableAccDesc.ReferenceTableName = Nothing
        Me.txtSalaryPayableAccDesc.Size = New System.Drawing.Size(259, 20)
        Me.txtSalaryPayableAccDesc.TabIndex = 236
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(6, 251)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(134, 18)
        Me.MyLabel3.TabIndex = 229
        Me.MyLabel3.Text = "Employer ESI Payable Acc"
        '
        'txtESIPayableAcc
        '
        Me.txtESIPayableAcc.CalculationExpression = Nothing
        Me.txtESIPayableAcc.FieldCode = Nothing
        Me.txtESIPayableAcc.FieldDesc = Nothing
        Me.txtESIPayableAcc.FieldMaxLength = 0
        Me.txtESIPayableAcc.FieldName = Nothing
        Me.txtESIPayableAcc.isCalculatedField = False
        Me.txtESIPayableAcc.IsSourceFromTable = False
        Me.txtESIPayableAcc.IsSourceFromValueList = False
        Me.txtESIPayableAcc.IsUnique = False
        Me.txtESIPayableAcc.Location = New System.Drawing.Point(141, 250)
        Me.txtESIPayableAcc.MaxLength = 55
        Me.txtESIPayableAcc.MendatroryField = False
        Me.txtESIPayableAcc.MyLinkLable1 = Me.lblBankAccount
        Me.txtESIPayableAcc.MyLinkLable2 = Nothing
        Me.txtESIPayableAcc.Name = "txtESIPayableAcc"
        Me.txtESIPayableAcc.ReadOnly = True
        Me.txtESIPayableAcc.ReferenceFieldDesc = Nothing
        Me.txtESIPayableAcc.ReferenceFieldName = Nothing
        Me.txtESIPayableAcc.ReferenceTableName = Nothing
        Me.txtESIPayableAcc.Size = New System.Drawing.Size(301, 20)
        Me.txtESIPayableAcc.TabIndex = 228
        '
        'txtPFPayableAcc
        '
        Me.txtPFPayableAcc.CalculationExpression = Nothing
        Me.txtPFPayableAcc.FieldCode = Nothing
        Me.txtPFPayableAcc.FieldDesc = Nothing
        Me.txtPFPayableAcc.FieldMaxLength = 0
        Me.txtPFPayableAcc.FieldName = Nothing
        Me.txtPFPayableAcc.isCalculatedField = False
        Me.txtPFPayableAcc.IsSourceFromTable = False
        Me.txtPFPayableAcc.IsSourceFromValueList = False
        Me.txtPFPayableAcc.IsUnique = False
        Me.txtPFPayableAcc.Location = New System.Drawing.Point(141, 227)
        Me.txtPFPayableAcc.MaxLength = 55
        Me.txtPFPayableAcc.MendatroryField = False
        Me.txtPFPayableAcc.MyLinkLable1 = Me.lblBankAccount
        Me.txtPFPayableAcc.MyLinkLable2 = Nothing
        Me.txtPFPayableAcc.Name = "txtPFPayableAcc"
        Me.txtPFPayableAcc.ReadOnly = True
        Me.txtPFPayableAcc.ReferenceFieldDesc = Nothing
        Me.txtPFPayableAcc.ReferenceFieldName = Nothing
        Me.txtPFPayableAcc.ReferenceTableName = Nothing
        Me.txtPFPayableAcc.Size = New System.Drawing.Size(301, 20)
        Me.txtPFPayableAcc.TabIndex = 227
        '
        'txtSalaryPayableAccount
        '
        Me.txtSalaryPayableAccount.CalculationExpression = Nothing
        Me.txtSalaryPayableAccount.FieldCode = Nothing
        Me.txtSalaryPayableAccount.FieldDesc = Nothing
        Me.txtSalaryPayableAccount.FieldMaxLength = 0
        Me.txtSalaryPayableAccount.FieldName = Nothing
        Me.txtSalaryPayableAccount.isCalculatedField = False
        Me.txtSalaryPayableAccount.IsSourceFromTable = False
        Me.txtSalaryPayableAccount.IsSourceFromValueList = False
        Me.txtSalaryPayableAccount.IsUnique = False
        Me.txtSalaryPayableAccount.Location = New System.Drawing.Point(141, 204)
        Me.txtSalaryPayableAccount.MaxLength = 50
        Me.txtSalaryPayableAccount.MendatroryField = False
        Me.txtSalaryPayableAccount.MyLinkLable1 = Nothing
        Me.txtSalaryPayableAccount.MyLinkLable2 = Nothing
        Me.txtSalaryPayableAccount.Name = "txtSalaryPayableAccount"
        Me.txtSalaryPayableAccount.ReadOnly = True
        Me.txtSalaryPayableAccount.ReferenceFieldDesc = Nothing
        Me.txtSalaryPayableAccount.ReferenceFieldName = Nothing
        Me.txtSalaryPayableAccount.ReferenceTableName = Nothing
        Me.txtSalaryPayableAccount.Size = New System.Drawing.Size(301, 20)
        Me.txtSalaryPayableAccount.TabIndex = 224
        '
        'lblSalaryPayableAccount
        '
        Me.lblSalaryPayableAccount.FieldName = Nothing
        Me.lblSalaryPayableAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryPayableAccount.Location = New System.Drawing.Point(6, 206)
        Me.lblSalaryPayableAccount.Name = "lblSalaryPayableAccount"
        Me.lblSalaryPayableAccount.Size = New System.Drawing.Size(126, 16)
        Me.lblSalaryPayableAccount.TabIndex = 225
        Me.lblSalaryPayableAccount.Text = "Salary Payable Account"
        '
        'fndSalaryAccountSett
        '
        Me.fndSalaryAccountSett.CalculationExpression = Nothing
        Me.fndSalaryAccountSett.FieldCode = Nothing
        Me.fndSalaryAccountSett.FieldDesc = Nothing
        Me.fndSalaryAccountSett.FieldMaxLength = 0
        Me.fndSalaryAccountSett.FieldName = Nothing
        Me.fndSalaryAccountSett.isCalculatedField = False
        Me.fndSalaryAccountSett.IsSourceFromTable = False
        Me.fndSalaryAccountSett.IsSourceFromValueList = False
        Me.fndSalaryAccountSett.IsUnique = False
        Me.fndSalaryAccountSett.Location = New System.Drawing.Point(141, 182)
        Me.fndSalaryAccountSett.MendatroryField = True
        Me.fndSalaryAccountSett.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSalaryAccountSett.MyLinkLable1 = Me.lblSalaryAccountSett
        Me.fndSalaryAccountSett.MyLinkLable2 = Me.lblPayPeriodName
        Me.fndSalaryAccountSett.MyReadOnly = False
        Me.fndSalaryAccountSett.MyShowMasterFormButton = False
        Me.fndSalaryAccountSett.Name = "fndSalaryAccountSett"
        Me.fndSalaryAccountSett.ReferenceFieldDesc = Nothing
        Me.fndSalaryAccountSett.ReferenceFieldName = Nothing
        Me.fndSalaryAccountSett.ReferenceTableName = Nothing
        Me.fndSalaryAccountSett.Size = New System.Drawing.Size(301, 19)
        Me.fndSalaryAccountSett.TabIndex = 223
        Me.fndSalaryAccountSett.Value = ""
        '
        'lblSalaryAccountSett
        '
        Me.lblSalaryAccountSett.FieldName = Nothing
        Me.lblSalaryAccountSett.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryAccountSett.Location = New System.Drawing.Point(6, 183)
        Me.lblSalaryAccountSett.Name = "lblSalaryAccountSett"
        Me.lblSalaryAccountSett.Size = New System.Drawing.Size(109, 16)
        Me.lblSalaryAccountSett.TabIndex = 222
        Me.lblSalaryAccountSett.Text = "Salary Account Sett."
        '
        'lblPayPeriodName
        '
        Me.lblPayPeriodName.AutoSize = False
        Me.lblPayPeriodName.BorderVisible = True
        Me.lblPayPeriodName.FieldName = Nothing
        Me.lblPayPeriodName.Location = New System.Drawing.Point(448, 75)
        Me.lblPayPeriodName.Name = "lblPayPeriodName"
        Me.lblPayPeriodName.Size = New System.Drawing.Size(259, 19)
        Me.lblPayPeriodName.TabIndex = 211
        Me.lblPayPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtGeneratedBy
        '
        Me.txtGeneratedBy.CalculationExpression = Nothing
        Me.txtGeneratedBy.FieldCode = Nothing
        Me.txtGeneratedBy.FieldDesc = Nothing
        Me.txtGeneratedBy.FieldMaxLength = 0
        Me.txtGeneratedBy.FieldName = Nothing
        Me.txtGeneratedBy.isCalculatedField = False
        Me.txtGeneratedBy.IsSourceFromTable = False
        Me.txtGeneratedBy.IsSourceFromValueList = False
        Me.txtGeneratedBy.IsUnique = False
        Me.txtGeneratedBy.Location = New System.Drawing.Point(141, 160)
        Me.txtGeneratedBy.MendatroryField = True
        Me.txtGeneratedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGeneratedBy.MyLinkLable1 = Me.MyLabel2
        Me.txtGeneratedBy.MyLinkLable2 = Me.lblPayPeriodName
        Me.txtGeneratedBy.MyReadOnly = False
        Me.txtGeneratedBy.MyShowMasterFormButton = False
        Me.txtGeneratedBy.Name = "txtGeneratedBy"
        Me.txtGeneratedBy.ReferenceFieldDesc = Nothing
        Me.txtGeneratedBy.ReferenceFieldName = Nothing
        Me.txtGeneratedBy.ReferenceTableName = Nothing
        Me.txtGeneratedBy.Size = New System.Drawing.Size(301, 19)
        Me.txtGeneratedBy.TabIndex = 221
        Me.txtGeneratedBy.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 161)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel2.TabIndex = 220
        Me.MyLabel2.Text = "Generated by"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 140)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel1.TabIndex = 219
        Me.MyLabel1.Text = "Remark"
        '
        'txtRemark
        '
        Me.txtRemark.CalculationExpression = Nothing
        Me.txtRemark.FieldCode = Nothing
        Me.txtRemark.FieldDesc = Nothing
        Me.txtRemark.FieldMaxLength = 0
        Me.txtRemark.FieldName = Nothing
        Me.txtRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.isCalculatedField = False
        Me.txtRemark.IsSourceFromTable = False
        Me.txtRemark.IsSourceFromValueList = False
        Me.txtRemark.IsUnique = False
        Me.txtRemark.Location = New System.Drawing.Point(141, 139)
        Me.txtRemark.MaxLength = 49
        Me.txtRemark.MendatroryField = True
        Me.txtRemark.MyLinkLable1 = Me.MyLabel1
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReferenceFieldDesc = Nothing
        Me.txtRemark.ReferenceFieldName = Nothing
        Me.txtRemark.ReferenceTableName = Nothing
        Me.txtRemark.Size = New System.Drawing.Size(301, 18)
        Me.txtRemark.TabIndex = 218
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(710, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(128, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 217
        '
        'lblPayPeriodDays
        '
        Me.lblPayPeriodDays.FieldName = Nothing
        Me.lblPayPeriodDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriodDays.Location = New System.Drawing.Point(6, 119)
        Me.lblPayPeriodDays.Name = "lblPayPeriodDays"
        Me.lblPayPeriodDays.Size = New System.Drawing.Size(91, 16)
        Me.lblPayPeriodDays.TabIndex = 216
        Me.lblPayPeriodDays.Text = "Pay Period Days"
        '
        'txtPayPeriodDays
        '
        Me.txtPayPeriodDays.CalculationExpression = Nothing
        Me.txtPayPeriodDays.FieldCode = Nothing
        Me.txtPayPeriodDays.FieldDesc = Nothing
        Me.txtPayPeriodDays.FieldMaxLength = 0
        Me.txtPayPeriodDays.FieldName = Nothing
        Me.txtPayPeriodDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayPeriodDays.isCalculatedField = False
        Me.txtPayPeriodDays.IsSourceFromTable = False
        Me.txtPayPeriodDays.IsSourceFromValueList = False
        Me.txtPayPeriodDays.IsUnique = False
        Me.txtPayPeriodDays.Location = New System.Drawing.Point(141, 118)
        Me.txtPayPeriodDays.MaxLength = 49
        Me.txtPayPeriodDays.MendatroryField = True
        Me.txtPayPeriodDays.MyLinkLable1 = Me.lblPayPeriodDays
        Me.txtPayPeriodDays.MyLinkLable2 = Nothing
        Me.txtPayPeriodDays.Name = "txtPayPeriodDays"
        Me.txtPayPeriodDays.ReadOnly = True
        Me.txtPayPeriodDays.ReferenceFieldDesc = Nothing
        Me.txtPayPeriodDays.ReferenceFieldName = Nothing
        Me.txtPayPeriodDays.ReferenceTableName = Nothing
        Me.txtPayPeriodDays.Size = New System.Drawing.Size(301, 18)
        Me.txtPayPeriodDays.TabIndex = 215
        '
        'lblTo
        '
        Me.lblTo.FieldName = Nothing
        Me.lblTo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTo.Location = New System.Drawing.Point(238, 98)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(110, 16)
        Me.lblTo.TabIndex = 59
        Me.lblTo.Text = "Salary Applicable To"
        '
        'lblDateFrom
        '
        Me.lblDateFrom.FieldName = Nothing
        Me.lblDateFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateFrom.Location = New System.Drawing.Point(6, 98)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(126, 16)
        Me.lblDateFrom.TabIndex = 214
        Me.lblDateFrom.Text = "Salary Applicable From "
        '
        'dtpTo
        '
        Me.dtpTo.CalculationExpression = Nothing
        Me.dtpTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpTo.FieldCode = Nothing
        Me.dtpTo.FieldDesc = Nothing
        Me.dtpTo.FieldMaxLength = 0
        Me.dtpTo.FieldName = Nothing
        Me.dtpTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.isCalculatedField = False
        Me.dtpTo.IsSourceFromTable = False
        Me.dtpTo.IsSourceFromValueList = False
        Me.dtpTo.IsUnique = False
        Me.dtpTo.Location = New System.Drawing.Point(353, 97)
        Me.dtpTo.MendatroryField = True
        Me.dtpTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTo.MyLinkLable1 = Me.lblTo
        Me.dtpTo.MyLinkLable2 = Nothing
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTo.ReadOnly = True
        Me.dtpTo.ReferenceFieldDesc = Nothing
        Me.dtpTo.ReferenceFieldName = Nothing
        Me.dtpTo.ReferenceTableName = Nothing
        Me.dtpTo.Size = New System.Drawing.Size(89, 18)
        Me.dtpTo.TabIndex = 58
        Me.dtpTo.TabStop = False
        Me.dtpTo.Text = "06/07/2013"
        Me.dtpTo.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        '
        'dtpFrom
        '
        Me.dtpFrom.CalculationExpression = Nothing
        Me.dtpFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpFrom.FieldCode = Nothing
        Me.dtpFrom.FieldDesc = Nothing
        Me.dtpFrom.FieldMaxLength = 0
        Me.dtpFrom.FieldName = Nothing
        Me.dtpFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.isCalculatedField = False
        Me.dtpFrom.IsSourceFromTable = False
        Me.dtpFrom.IsSourceFromValueList = False
        Me.dtpFrom.IsUnique = False
        Me.dtpFrom.Location = New System.Drawing.Point(141, 97)
        Me.dtpFrom.MendatroryField = True
        Me.dtpFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrom.MyLinkLable1 = Me.lblDateFrom
        Me.dtpFrom.MyLinkLable2 = Nothing
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrom.ReadOnly = True
        Me.dtpFrom.ReferenceFieldDesc = Nothing
        Me.dtpFrom.ReferenceFieldName = Nothing
        Me.dtpFrom.ReferenceTableName = Nothing
        Me.dtpFrom.Size = New System.Drawing.Size(89, 18)
        Me.dtpFrom.TabIndex = 213
        Me.dtpFrom.TabStop = False
        Me.dtpFrom.Text = "06/07/2013"
        Me.dtpFrom.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(422, 7)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 21)
        Me.btnNew.TabIndex = 212
        Me.btnNew.Text = " "
        '
        'findPayperiod
        '
        Me.findPayperiod.CalculationExpression = Nothing
        Me.findPayperiod.FieldCode = Nothing
        Me.findPayperiod.FieldDesc = Nothing
        Me.findPayperiod.FieldMaxLength = 0
        Me.findPayperiod.FieldName = Nothing
        Me.findPayperiod.isCalculatedField = False
        Me.findPayperiod.IsSourceFromTable = False
        Me.findPayperiod.IsSourceFromValueList = False
        Me.findPayperiod.IsUnique = False
        Me.findPayperiod.Location = New System.Drawing.Point(141, 75)
        Me.findPayperiod.MendatroryField = True
        Me.findPayperiod.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findPayperiod.MyLinkLable1 = Me.lblPayPeriodCode
        Me.findPayperiod.MyLinkLable2 = Me.lblPayPeriodName
        Me.findPayperiod.MyReadOnly = False
        Me.findPayperiod.MyShowMasterFormButton = False
        Me.findPayperiod.Name = "findPayperiod"
        Me.findPayperiod.ReferenceFieldDesc = Nothing
        Me.findPayperiod.ReferenceFieldName = Nothing
        Me.findPayperiod.ReferenceTableName = Nothing
        Me.findPayperiod.Size = New System.Drawing.Size(301, 19)
        Me.findPayperiod.TabIndex = 210
        Me.findPayperiod.Value = ""
        '
        'lblPayPeriodCode
        '
        Me.lblPayPeriodCode.FieldName = Nothing
        Me.lblPayPeriodCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayPeriodCode.Location = New System.Drawing.Point(6, 76)
        Me.lblPayPeriodCode.Name = "lblPayPeriodCode"
        Me.lblPayPeriodCode.Size = New System.Drawing.Size(92, 16)
        Me.lblPayPeriodCode.TabIndex = 209
        Me.lblPayPeriodCode.Text = "Pay Period Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(141, 7)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(281, 21)
        Me.txtCode.TabIndex = 207
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(6, 9)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(92, 16)
        Me.lblCode.TabIndex = 208
        Me.lblCode.Text = "Generation Code"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(847, 17)
        Me.ProgressBar1.TabIndex = 0
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(78, 18)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(58, 21)
        Me.btnPost.TabIndex = 132
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(5, 18)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(71, 21)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Generate"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(138, 18)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(62, 21)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(772, 18)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(71, 21)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'lblDivisionDesc
        '
        Me.lblDivisionDesc.AutoSize = False
        Me.lblDivisionDesc.BorderVisible = True
        Me.lblDivisionDesc.FieldName = Nothing
        Me.lblDivisionDesc.Location = New System.Drawing.Point(448, 53)
        Me.lblDivisionDesc.Name = "lblDivisionDesc"
        Me.lblDivisionDesc.Size = New System.Drawing.Size(259, 19)
        Me.lblDivisionDesc.TabIndex = 253
        Me.lblDivisionDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(6, 54)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel8.TabIndex = 252
        Me.MyLabel8.Text = "Division"
        '
        'fndDivision
        '
        Me.fndDivision.CalculationExpression = Nothing
        Me.fndDivision.FieldCode = Nothing
        Me.fndDivision.FieldDesc = Nothing
        Me.fndDivision.FieldMaxLength = 0
        Me.fndDivision.FieldName = Nothing
        Me.fndDivision.isCalculatedField = False
        Me.fndDivision.IsSourceFromTable = False
        Me.fndDivision.IsSourceFromValueList = False
        Me.fndDivision.IsUnique = False
        Me.fndDivision.Location = New System.Drawing.Point(141, 53)
        Me.fndDivision.MendatroryField = True
        Me.fndDivision.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDivision.MyLinkLable1 = Me.MyLabel8
        Me.fndDivision.MyLinkLable2 = Nothing
        Me.fndDivision.MyReadOnly = False
        Me.fndDivision.MyShowMasterFormButton = False
        Me.fndDivision.Name = "fndDivision"
        Me.fndDivision.ReferenceFieldDesc = Nothing
        Me.fndDivision.ReferenceFieldName = Nothing
        Me.fndDivision.ReferenceTableName = Nothing
        Me.fndDivision.Size = New System.Drawing.Size(301, 19)
        Me.fndDivision.TabIndex = 251
        Me.fndDivision.Value = ""
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(448, 31)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(259, 19)
        Me.lblLocationDesc.TabIndex = 250
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(6, 32)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel9.TabIndex = 249
        Me.MyLabel9.Text = "Location"
        '
        'txtBranch
        '
        Me.txtBranch.CalculationExpression = Nothing
        Me.txtBranch.FieldCode = Nothing
        Me.txtBranch.FieldDesc = Nothing
        Me.txtBranch.FieldMaxLength = 0
        Me.txtBranch.FieldName = Nothing
        Me.txtBranch.isCalculatedField = False
        Me.txtBranch.IsSourceFromTable = False
        Me.txtBranch.IsSourceFromValueList = False
        Me.txtBranch.IsUnique = False
        Me.txtBranch.Location = New System.Drawing.Point(141, 31)
        Me.txtBranch.MendatroryField = True
        Me.txtBranch.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.MyLinkLable1 = Me.MyLabel9
        Me.txtBranch.MyLinkLable2 = Nothing
        Me.txtBranch.MyReadOnly = False
        Me.txtBranch.MyShowMasterFormButton = False
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.ReferenceFieldDesc = Nothing
        Me.txtBranch.ReferenceFieldName = Nothing
        Me.txtBranch.ReferenceTableName = Nothing
        Me.txtBranch.Size = New System.Drawing.Size(301, 19)
        Me.txtBranch.TabIndex = 248
        Me.txtBranch.Value = ""
        '
        'chkCreateFE
        '
        Me.chkCreateFE.Location = New System.Drawing.Point(448, 182)
        Me.chkCreateFE.Name = "chkCreateFE"
        '
        '
        '
        Me.chkCreateFE.RootElement.StretchHorizontally = True
        Me.chkCreateFE.RootElement.StretchVertically = True
        Me.chkCreateFE.Size = New System.Drawing.Size(148, 18)
        Me.chkCreateFE.TabIndex = 247
        Me.chkCreateFE.Text = "Create Financial Entry"
        '
        'dtpChequeDated
        '
        Me.dtpChequeDated.CalculationExpression = Nothing
        Me.dtpChequeDated.CustomFormat = "dd/MM/yyyy"
        Me.dtpChequeDated.FieldCode = Nothing
        Me.dtpChequeDated.FieldDesc = Nothing
        Me.dtpChequeDated.FieldMaxLength = 0
        Me.dtpChequeDated.FieldName = Nothing
        Me.dtpChequeDated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpChequeDated.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDated.isCalculatedField = False
        Me.dtpChequeDated.IsSourceFromTable = False
        Me.dtpChequeDated.IsSourceFromValueList = False
        Me.dtpChequeDated.IsUnique = False
        Me.dtpChequeDated.Location = New System.Drawing.Point(353, 321)
        Me.dtpChequeDated.MendatroryField = True
        Me.dtpChequeDated.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDated.MyLinkLable1 = Me.lblTo
        Me.dtpChequeDated.MyLinkLable2 = Nothing
        Me.dtpChequeDated.Name = "dtpChequeDated"
        Me.dtpChequeDated.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChequeDated.ReferenceFieldDesc = Nothing
        Me.dtpChequeDated.ReferenceFieldName = Nothing
        Me.dtpChequeDated.ReferenceTableName = Nothing
        Me.dtpChequeDated.Size = New System.Drawing.Size(89, 18)
        Me.dtpChequeDated.TabIndex = 246
        Me.dtpChequeDated.TabStop = False
        Me.dtpChequeDated.Text = "06/07/2013"
        Me.dtpChequeDated.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        Me.dtpChequeDated.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(273, 321)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel6.TabIndex = 245
        Me.MyLabel6.Text = "Cheque Dated"
        Me.MyLabel6.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(6, 321)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel5.TabIndex = 243
        Me.MyLabel5.Text = "Cheque No"
        Me.MyLabel5.Visible = False
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CalculationExpression = Nothing
        Me.txtChequeNo.FieldCode = Nothing
        Me.txtChequeNo.FieldDesc = Nothing
        Me.txtChequeNo.FieldMaxLength = 0
        Me.txtChequeNo.FieldName = Nothing
        Me.txtChequeNo.isCalculatedField = False
        Me.txtChequeNo.IsSourceFromTable = False
        Me.txtChequeNo.IsSourceFromValueList = False
        Me.txtChequeNo.IsUnique = False
        Me.txtChequeNo.Location = New System.Drawing.Point(141, 320)
        Me.txtChequeNo.MaxLength = 55
        Me.txtChequeNo.MendatroryField = False
        Me.txtChequeNo.MyLinkLable1 = Me.lblBankAccount
        Me.txtChequeNo.MyLinkLable2 = Nothing
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.ReferenceFieldDesc = Nothing
        Me.txtChequeNo.ReferenceFieldName = Nothing
        Me.txtChequeNo.ReferenceTableName = Nothing
        Me.txtChequeNo.Size = New System.Drawing.Size(126, 20)
        Me.txtChequeNo.TabIndex = 242
        Me.txtChequeNo.Visible = False
        '
        'btnCreateFE
        '
        Me.btnCreateFE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreateFE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateFE.Location = New System.Drawing.Point(202, 18)
        Me.btnCreateFE.Name = "btnCreateFE"
        Me.btnCreateFE.Size = New System.Drawing.Size(129, 21)
        Me.btnCreateFE.TabIndex = 219
        Me.btnCreateFE.Text = "Create Financial Entry"
        '
        'btnSendMail
        '
        Me.btnSendMail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSendMail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendMail.Location = New System.Drawing.Point(498, 18)
        Me.btnSendMail.Name = "btnSendMail"
        Me.btnSendMail.Size = New System.Drawing.Size(68, 21)
        Me.btnSendMail.TabIndex = 220
        Me.btnSendMail.Text = "Send Mail"
        Me.btnSendMail.Visible = False
        '
        'btnViewFE
        '
        Me.btnViewFE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnViewFE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewFE.Location = New System.Drawing.Point(333, 18)
        Me.btnViewFE.Name = "btnViewFE"
        Me.btnViewFE.Size = New System.Drawing.Size(110, 21)
        Me.btnViewFE.TabIndex = 221
        Me.btnViewFE.Text = "View Financial Entry"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(569, 18)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(112, 21)
        Me.RadButton1.TabIndex = 222
        Me.RadButton1.Text = "View Payment Entry"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(445, 18)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(51, 21)
        Me.btnReverse.TabIndex = 220
        Me.btnReverse.Text = "Reverse"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(683, 18)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(86, 21)
        Me.RadButton2.TabIndex = 223
        Me.RadButton2.Text = "Salary Register"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBankCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndBankCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtpaymentDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPaymentDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpGenerateDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCompany)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDivisionDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDivision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBranch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkCreateFE)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpChequeDated)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findPayperiod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBankAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtChequeNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPFPayableAcc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemark)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOthrPayableDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtESIPayableAcc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSalaryPayableAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPayPeriodDays)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalaryPayableAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOthrPayableAcc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSalaryPayableAccDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPayPeriodDays)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndSalaryAccountSett)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGeneratedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtESIPayableDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPFPayableAccDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDateFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalaryAccountSett)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ProgressBar1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewFE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCreateFE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSendMail)
        Me.SplitContainer1.Size = New System.Drawing.Size(847, 427)
        Me.SplitContainer1.SplitterDistance = 381
        Me.SplitContainer1.TabIndex = 224
        '
        'dtpGenerateDate
        '
        Me.dtpGenerateDate.CalculationExpression = Nothing
        Me.dtpGenerateDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpGenerateDate.FieldCode = Nothing
        Me.dtpGenerateDate.FieldDesc = Nothing
        Me.dtpGenerateDate.FieldMaxLength = 0
        Me.dtpGenerateDate.FieldName = Nothing
        Me.dtpGenerateDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpGenerateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGenerateDate.isCalculatedField = False
        Me.dtpGenerateDate.IsSourceFromTable = False
        Me.dtpGenerateDate.IsSourceFromValueList = False
        Me.dtpGenerateDate.IsUnique = False
        Me.dtpGenerateDate.Location = New System.Drawing.Point(490, 8)
        Me.dtpGenerateDate.MendatroryField = True
        Me.dtpGenerateDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGenerateDate.MyLinkLable1 = Me.lblTo
        Me.dtpGenerateDate.MyLinkLable2 = Nothing
        Me.dtpGenerateDate.Name = "dtpGenerateDate"
        Me.dtpGenerateDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGenerateDate.ReferenceFieldDesc = Nothing
        Me.dtpGenerateDate.ReferenceFieldName = Nothing
        Me.dtpGenerateDate.ReferenceTableName = Nothing
        Me.dtpGenerateDate.Size = New System.Drawing.Size(89, 18)
        Me.dtpGenerateDate.TabIndex = 388
        Me.dtpGenerateDate.TabStop = False
        Me.dtpGenerateDate.Text = "06/07/2013"
        Me.dtpGenerateDate.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(448, 7)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel7.TabIndex = 387
        Me.MyLabel7.Text = "Dated"
        '
        'lblCompany
        '
        Me.lblCompany.FieldName = Nothing
        Me.lblCompany.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(6, 297)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(60, 18)
        Me.lblCompany.TabIndex = 386
        Me.lblCompany.Text = "Employees"
        '
        'txtEmp
        '
        Me.txtEmp.arrDispalyMember = Nothing
        Me.txtEmp.arrValueMember = Nothing
        Me.txtEmp.Location = New System.Drawing.Point(141, 297)
        Me.txtEmp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.MyLinkLable1 = Me.lblCompany
        Me.txtEmp.MyLinkLable2 = Nothing
        Me.txtEmp.MyNullText = "Please select..."
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.Size = New System.Drawing.Size(566, 19)
        Me.txtEmp.TabIndex = 385
        '
        'txtpaymentDate
        '
        Me.txtpaymentDate.CalculationExpression = Nothing
        Me.txtpaymentDate.CustomFormat = "dd/MM/yyyy"
        Me.txtpaymentDate.FieldCode = Nothing
        Me.txtpaymentDate.FieldDesc = Nothing
        Me.txtpaymentDate.FieldMaxLength = 0
        Me.txtpaymentDate.FieldName = Nothing
        Me.txtpaymentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtpaymentDate.isCalculatedField = False
        Me.txtpaymentDate.IsSourceFromTable = False
        Me.txtpaymentDate.IsSourceFromValueList = False
        Me.txtpaymentDate.IsUnique = False
        Me.txtpaymentDate.Location = New System.Drawing.Point(525, 96)
        Me.txtpaymentDate.MendatroryField = True
        Me.txtpaymentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtpaymentDate.MyLinkLable1 = Me.lblTo
        Me.txtpaymentDate.MyLinkLable2 = Nothing
        Me.txtpaymentDate.Name = "txtpaymentDate"
        Me.txtpaymentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtpaymentDate.ReferenceFieldDesc = Nothing
        Me.txtpaymentDate.ReferenceFieldName = Nothing
        Me.txtpaymentDate.ReferenceTableName = Nothing
        Me.txtpaymentDate.Size = New System.Drawing.Size(89, 18)
        Me.txtpaymentDate.TabIndex = 390
        Me.txtpaymentDate.TabStop = False
        Me.txtpaymentDate.Text = "06/07/2013"
        Me.txtpaymentDate.Value = New Date(2013, 7, 6, 0, 0, 0, 0)
        Me.txtpaymentDate.Visible = False
        '
        'lblPaymentDate
        '
        Me.lblPaymentDate.FieldName = Nothing
        Me.lblPaymentDate.Location = New System.Drawing.Point(445, 96)
        Me.lblPaymentDate.Name = "lblPaymentDate"
        Me.lblPaymentDate.Size = New System.Drawing.Size(76, 18)
        Me.lblPaymentDate.TabIndex = 389
        Me.lblPaymentDate.Text = "Payment Date"
        Me.lblPaymentDate.Visible = False
        '
        'fndBankCode
        '
        Me.fndBankCode.CalculationExpression = Nothing
        Me.fndBankCode.FieldCode = Nothing
        Me.fndBankCode.FieldDesc = Nothing
        Me.fndBankCode.FieldMaxLength = 0
        Me.fndBankCode.FieldName = Nothing
        Me.fndBankCode.isCalculatedField = False
        Me.fndBankCode.IsSourceFromTable = False
        Me.fndBankCode.IsSourceFromValueList = False
        Me.fndBankCode.IsUnique = False
        Me.fndBankCode.Location = New System.Drawing.Point(518, 118)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.MyShowMasterFormButton = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.ReferenceFieldDesc = Nothing
        Me.fndBankCode.ReferenceFieldName = Nothing
        Me.fndBankCode.ReferenceTableName = Nothing
        Me.fndBankCode.Size = New System.Drawing.Size(127, 19)
        Me.fndBankCode.TabIndex = 392
        Me.fndBankCode.Value = ""
        '
        'lblBankCode
        '
        Me.lblBankCode.FieldName = Nothing
        Me.lblBankCode.Location = New System.Drawing.Point(445, 119)
        Me.lblBankCode.Name = "lblBankCode"
        Me.lblBankCode.Size = New System.Drawing.Size(60, 18)
        Me.lblBankCode.TabIndex = 390
        Me.lblBankCode.Text = "Bank Code"
        Me.lblBankCode.Visible = False
        '
        'frmSalaryGeneration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(847, 427)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSalaryGeneration"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salary Generation"
        CType(Me.txtOthrPayableDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOthrPayableAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESIPayableDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPFPayableAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalaryPayableAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESIPayableAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPFPayableAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalaryPayableAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalaryAccountSett, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPayPeriodDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPayPeriodCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivisionDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateFE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChequeDated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreateFE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendMail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewFE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dtpGenerateDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpaymentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPaymentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblPayPeriodName As common.Controls.MyLabel
    Friend WithEvents findPayperiod As common.UserControls.txtFinder
    Friend WithEvents lblPayPeriodCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblTo As common.Controls.MyLabel
    Friend WithEvents lblDateFrom As common.Controls.MyLabel
    Friend WithEvents dtpTo As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFrom As common.Controls.MyDateTimePicker
    Friend WithEvents lblPayPeriodDays As common.Controls.MyLabel
    Friend WithEvents txtPayPeriodDays As common.Controls.MyTextBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents txtGeneratedBy As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndSalaryAccountSett As common.UserControls.txtFinder
    Friend WithEvents lblSalaryAccountSett As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtESIPayableAcc As common.Controls.MyTextBox
    Friend WithEvents lblBankAccount As common.Controls.MyLabel
    Friend WithEvents txtPFPayableAcc As common.Controls.MyTextBox
    Friend WithEvents txtSalaryPayableAccount As common.Controls.MyTextBox
    Friend WithEvents lblSalaryPayableAccount As common.Controls.MyLabel
    Friend WithEvents txtESIPayableDesc As common.Controls.MyTextBox
    Friend WithEvents txtPFPayableAccDesc As common.Controls.MyTextBox
    Friend WithEvents txtSalaryPayableAccDesc As common.Controls.MyTextBox
    Friend WithEvents txtOthrPayableDesc As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtOthrPayableAcc As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtChequeNo As common.Controls.MyTextBox
    Friend WithEvents dtpChequeDated As common.Controls.MyDateTimePicker
    Friend WithEvents chkCreateFE As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnCreateFE As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtBranch As common.UserControls.txtFinder
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblDivisionDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents fndDivision As common.UserControls.txtFinder
    Friend WithEvents btnSendMail As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnViewFE As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCompany As common.Controls.MyLabel
    Friend WithEvents txtEmp As common.UserControls.txtMultiSelectFinder
    Friend WithEvents dtpGenerateDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtpaymentDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPaymentDate As common.Controls.MyLabel
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents lblBankCode As common.Controls.MyLabel
End Class
