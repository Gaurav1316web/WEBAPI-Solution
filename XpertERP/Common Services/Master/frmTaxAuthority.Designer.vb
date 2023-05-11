<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaxAuthority
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
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkmanditax = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.cbgCSA = New common.MyCheckBoxGrid()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtBaseCurrency = New common.UserControls.txtFinder()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.findTaxAuthority = New common.UserControls.txtNavigator()
        Me.lblAuthority = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.lblDesc = New common.Controls.MyLabel()
        Me.gbAccounts = New Telerik.WinControls.UI.RadGroupBox()
        Me.chk_Mandi_Tax_Cess = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblDepositControl = New common.Controls.MyLabel()
        Me.txtDepositControl = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.rdtxtpayablescontrol = New common.Controls.MyLabel()
        Me.fndpayable = New common.UserControls.txtFinder()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.ChkGSTActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndnetpay = New common.UserControls.txtFinder()
        Me.lblnetpay = New common.Controls.MyLabel()
        Me.findTaxRelAcc = New common.UserControls.txtFinder()
        Me.lblTaxRelAcc = New common.Controls.MyLabel()
        Me.findRecoverTaxAcc = New common.UserControls.txtFinder()
        Me.lblRecoverTaxAcc = New common.Controls.MyLabel()
        Me.TotalRecovRate = New common.MyNumBox()
        Me.txtRecovRate5 = New common.MyNumBox()
        Me.txtRecovRate4 = New common.MyNumBox()
        Me.txtRecovRate3 = New common.MyNumBox()
        Me.txtRecovRate2 = New common.MyNumBox()
        Me.txtRecoverRate = New common.MyNumBox()
        Me.txtRecovTaxAccDesc5 = New common.Controls.MyTextBox()
        Me.fndRecovTaxAcc5 = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtRecovTaxAccDesc4 = New common.Controls.MyTextBox()
        Me.fndRecovTaxAcc4 = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtRecovTaxAccDesc3 = New common.Controls.MyTextBox()
        Me.fndRecovTaxAcc3 = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtRecovTaxAccDesc2 = New common.Controls.MyTextBox()
        Me.fndRecovTaxAcc2 = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblRecoverRate = New common.Controls.MyLabel()
        Me.lblType = New common.Controls.MyLabel()
        Me.drpTaxtype = New common.Controls.MyComboBox()
        Me.chkExcisable = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtnetpay = New common.Controls.MyTextBox()
        Me.txtRecoverTaxAccDesc = New common.Controls.MyTextBox()
        Me.txtTaxRelAccDesc = New common.Controls.MyTextBox()
        Me.chkTaxRecover = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnAdd = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkmanditax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAuthority, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAccounts.SuspendLayout()
        CType(Me.chk_Mandi_Tax_Cess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lblDepositControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdtxtpayablescontrol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkGSTActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblnetpay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxRelAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRecoverTaxAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TotalRecovRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovRate5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovRate4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovRate3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovRate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecoverRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovTaxAccDesc5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovTaxAccDesc4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovTaxAccDesc3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecovTaxAccDesc2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRecoverRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.drpTaxtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnetpay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecoverTaxAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxRelAccDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTaxRecover, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuExport, Me.menuImport, Me.menuExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "RadMenuItem2"
        Me.menuExport.AccessibleName = "RadMenuItem2"
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "RadMenuItem3"
        Me.menuImport.AccessibleName = "RadMenuItem3"
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import"
        '
        'menuExit
        '
        Me.menuExit.AccessibleDescription = "RadMenuItem4"
        Me.menuExit.AccessibleName = "RadMenuItem4"
        Me.menuExit.Name = "menuExit"
        Me.menuExit.Text = "Exit"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(860, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkmanditax)
        Me.RadGroupBox1.Controls.Add(Me.RadPanel1)
        Me.RadGroupBox1.Controls.Add(Me.pnlCurrConv)
        Me.RadGroupBox1.Controls.Add(Me.findTaxAuthority)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblDesc)
        Me.RadGroupBox1.Controls.Add(Me.gbAccounts)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.lblAuthority)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(860, 543)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkmanditax
        '
        Me.chkmanditax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkmanditax.Location = New System.Drawing.Point(365, 500)
        Me.chkmanditax.Name = "chkmanditax"
        Me.chkmanditax.Size = New System.Drawing.Size(85, 16)
        Me.chkmanditax.TabIndex = 32
        Me.chkmanditax.Text = "Is Mandi Tax"
        Me.chkmanditax.Visible = False
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.cbgCSA)
        Me.RadPanel1.Location = New System.Drawing.Point(365, 376)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Padding = New System.Windows.Forms.Padding(3)
        Me.RadPanel1.Size = New System.Drawing.Size(268, 119)
        Me.RadPanel1.TabIndex = 7
        Me.RadPanel1.Text = "RadPanel1"
        '
        'cbgCSA
        '
        Me.cbgCSA.CheckedValue = Nothing
        Me.cbgCSA.DataSource = Nothing
        Me.cbgCSA.DisplayMember = "Name"
        Me.cbgCSA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCSA.Location = New System.Drawing.Point(3, 3)
        Me.cbgCSA.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCSA.MyShowHeadrText = False
        Me.cbgCSA.Name = "cbgCSA"
        Me.cbgCSA.Size = New System.Drawing.Size(262, 113)
        Me.cbgCSA.TabIndex = 8
        Me.cbgCSA.ValueMember = "Code"
        '
        'pnlCurrConv
        '
        Me.pnlCurrConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCurrConv.Controls.Add(Me.txtBaseCurrency)
        Me.pnlCurrConv.Controls.Add(Me.MyLabel20)
        Me.pnlCurrConv.Controls.Add(Me.txtConversionRate)
        Me.pnlCurrConv.Controls.Add(Me.txtCurrencyCode)
        Me.pnlCurrConv.Controls.Add(Me.lblEffectiveFrom)
        Me.pnlCurrConv.Controls.Add(Me.txtApplicableFrom)
        Me.pnlCurrConv.Controls.Add(Me.lblCurrency)
        Me.pnlCurrConv.Controls.Add(Me.lblConvRate)
        Me.pnlCurrConv.Location = New System.Drawing.Point(11, 378)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(269, 119)
        Me.pnlCurrConv.TabIndex = 4
        Me.pnlCurrConv.Visible = False
        '
        'txtBaseCurrency
        '
        Me.txtBaseCurrency.CalculationExpression = Nothing
        Me.txtBaseCurrency.Enabled = False
        Me.txtBaseCurrency.FieldCode = Nothing
        Me.txtBaseCurrency.FieldDesc = Nothing
        Me.txtBaseCurrency.FieldMaxLength = 0
        Me.txtBaseCurrency.FieldName = Nothing
        Me.txtBaseCurrency.isCalculatedField = False
        Me.txtBaseCurrency.IsSourceFromTable = False
        Me.txtBaseCurrency.IsSourceFromValueList = False
        Me.txtBaseCurrency.IsUnique = False
        Me.txtBaseCurrency.Location = New System.Drawing.Point(104, 33)
        Me.txtBaseCurrency.MendatroryField = False
        Me.txtBaseCurrency.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBaseCurrency.MyLinkLable1 = Nothing
        Me.txtBaseCurrency.MyLinkLable2 = Nothing
        Me.txtBaseCurrency.MyReadOnly = False
        Me.txtBaseCurrency.MyShowMasterFormButton = False
        Me.txtBaseCurrency.Name = "txtBaseCurrency"
        Me.txtBaseCurrency.ReferenceFieldDesc = Nothing
        Me.txtBaseCurrency.ReferenceFieldName = Nothing
        Me.txtBaseCurrency.ReferenceTableName = Nothing
        Me.txtBaseCurrency.Size = New System.Drawing.Size(154, 24)
        Me.txtBaseCurrency.TabIndex = 2
        Me.txtBaseCurrency.Value = ""
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(3, 36)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel20.TabIndex = 6
        Me.MyLabel20.Text = "BaseCurrency"
        '
        'txtConversionRate
        '
        Me.txtConversionRate.BackColor = System.Drawing.Color.White
        Me.txtConversionRate.CalculationExpression = Nothing
        Me.txtConversionRate.DecimalPlaces = 2
        Me.txtConversionRate.FieldCode = Nothing
        Me.txtConversionRate.FieldDesc = Nothing
        Me.txtConversionRate.FieldMaxLength = 0
        Me.txtConversionRate.FieldName = Nothing
        Me.txtConversionRate.isCalculatedField = False
        Me.txtConversionRate.IsSourceFromTable = False
        Me.txtConversionRate.IsSourceFromValueList = False
        Me.txtConversionRate.IsUnique = False
        Me.txtConversionRate.Location = New System.Drawing.Point(104, 60)
        Me.txtConversionRate.MendatroryField = False
        Me.txtConversionRate.MyLinkLable1 = Nothing
        Me.txtConversionRate.MyLinkLable2 = Nothing
        Me.txtConversionRate.Name = "txtConversionRate"
        Me.txtConversionRate.ReferenceFieldDesc = Nothing
        Me.txtConversionRate.ReferenceFieldName = Nothing
        Me.txtConversionRate.ReferenceTableName = Nothing
        Me.txtConversionRate.Size = New System.Drawing.Size(154, 20)
        Me.txtConversionRate.TabIndex = 1
        Me.txtConversionRate.Text = "1"
        Me.txtConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversionRate.Value = 1.0R
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.CalculationExpression = Nothing
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(104, 5)
        Me.txtCurrencyCode.MendatroryField = False
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Nothing
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.ReferenceFieldDesc = Nothing
        Me.txtCurrencyCode.ReferenceFieldName = Nothing
        Me.txtCurrencyCode.ReferenceTableName = Nothing
        Me.txtCurrencyCode.Size = New System.Drawing.Size(154, 24)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(3, 86)
        Me.lblEffectiveFrom.Name = "lblEffectiveFrom"
        Me.lblEffectiveFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblEffectiveFrom.TabIndex = 3
        Me.lblEffectiveFrom.Text = "Applicable From"
        '
        'txtApplicableFrom
        '
        Me.txtApplicableFrom.AutoSize = False
        Me.txtApplicableFrom.BorderVisible = True
        Me.txtApplicableFrom.FieldName = Nothing
        Me.txtApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicableFrom.Location = New System.Drawing.Point(104, 84)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(154, 22)
        Me.txtApplicableFrom.TabIndex = 4
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(3, 9)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 7
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(3, 62)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 5
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'findTaxAuthority
        '
        Me.findTaxAuthority.FieldName = Nothing
        Me.findTaxAuthority.Location = New System.Drawing.Point(84, 17)
        Me.findTaxAuthority.MendatroryField = True
        Me.findTaxAuthority.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.findTaxAuthority.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.findTaxAuthority.MyLinkLable1 = Me.lblAuthority
        Me.findTaxAuthority.MyLinkLable2 = Nothing
        Me.findTaxAuthority.MyMaxLength = 32767
        Me.findTaxAuthority.MyReadOnly = False
        Me.findTaxAuthority.Name = "findTaxAuthority"
        Me.findTaxAuthority.Size = New System.Drawing.Size(186, 19)
        Me.findTaxAuthority.TabIndex = 0
        Me.findTaxAuthority.Value = ""
        '
        'lblAuthority
        '
        Me.lblAuthority.FieldName = Nothing
        Me.lblAuthority.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblAuthority.Location = New System.Drawing.Point(7, 18)
        Me.lblAuthority.Name = "lblAuthority"
        Me.lblAuthority.Size = New System.Drawing.Size(73, 16)
        Me.lblAuthority.TabIndex = 5
        Me.lblAuthority.Text = "Tax Authority"
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(367, 17)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.lblDesc
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(297, 18)
        Me.txtDesc.TabIndex = 2
        '
        'lblDesc
        '
        Me.lblDesc.FieldName = Nothing
        Me.lblDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.Location = New System.Drawing.Point(299, 18)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(63, 16)
        Me.lblDesc.TabIndex = 6
        Me.lblDesc.Text = "Description"
        '
        'gbAccounts
        '
        Me.gbAccounts.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbAccounts.Controls.Add(Me.chk_Mandi_Tax_Cess)
        Me.gbAccounts.Controls.Add(Me.RadGroupBox2)
        Me.gbAccounts.Controls.Add(Me.ChkGSTActive)
        Me.gbAccounts.Controls.Add(Me.fndnetpay)
        Me.gbAccounts.Controls.Add(Me.findTaxRelAcc)
        Me.gbAccounts.Controls.Add(Me.findRecoverTaxAcc)
        Me.gbAccounts.Controls.Add(Me.TotalRecovRate)
        Me.gbAccounts.Controls.Add(Me.txtRecovRate5)
        Me.gbAccounts.Controls.Add(Me.txtRecovRate4)
        Me.gbAccounts.Controls.Add(Me.txtRecovRate3)
        Me.gbAccounts.Controls.Add(Me.txtRecovRate2)
        Me.gbAccounts.Controls.Add(Me.txtRecoverRate)
        Me.gbAccounts.Controls.Add(Me.txtRecovTaxAccDesc5)
        Me.gbAccounts.Controls.Add(Me.fndRecovTaxAcc5)
        Me.gbAccounts.Controls.Add(Me.txtRecovTaxAccDesc4)
        Me.gbAccounts.Controls.Add(Me.fndRecovTaxAcc4)
        Me.gbAccounts.Controls.Add(Me.txtRecovTaxAccDesc3)
        Me.gbAccounts.Controls.Add(Me.fndRecovTaxAcc3)
        Me.gbAccounts.Controls.Add(Me.txtRecovTaxAccDesc2)
        Me.gbAccounts.Controls.Add(Me.fndRecovTaxAcc2)
        Me.gbAccounts.Controls.Add(Me.MyLabel4)
        Me.gbAccounts.Controls.Add(Me.MyLabel3)
        Me.gbAccounts.Controls.Add(Me.MyLabel2)
        Me.gbAccounts.Controls.Add(Me.MyLabel1)
        Me.gbAccounts.Controls.Add(Me.lblRecoverRate)
        Me.gbAccounts.Controls.Add(Me.lblType)
        Me.gbAccounts.Controls.Add(Me.drpTaxtype)
        Me.gbAccounts.Controls.Add(Me.chkExcisable)
        Me.gbAccounts.Controls.Add(Me.lblnetpay)
        Me.gbAccounts.Controls.Add(Me.txtnetpay)
        Me.gbAccounts.Controls.Add(Me.lblTaxRelAcc)
        Me.gbAccounts.Controls.Add(Me.txtRecoverTaxAccDesc)
        Me.gbAccounts.Controls.Add(Me.txtTaxRelAccDesc)
        Me.gbAccounts.Controls.Add(Me.chkTaxRecover)
        Me.gbAccounts.Controls.Add(Me.lblRecoverTaxAcc)
        Me.gbAccounts.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbAccounts.HeaderText = ""
        Me.gbAccounts.Location = New System.Drawing.Point(11, 42)
        Me.gbAccounts.Name = "gbAccounts"
        Me.gbAccounts.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbAccounts.Size = New System.Drawing.Size(664, 328)
        Me.gbAccounts.TabIndex = 3
        '
        'chk_Mandi_Tax_Cess
        '
        Me.chk_Mandi_Tax_Cess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_Mandi_Tax_Cess.Location = New System.Drawing.Point(421, 217)
        Me.chk_Mandi_Tax_Cess.Name = "chk_Mandi_Tax_Cess"
        Me.chk_Mandi_Tax_Cess.Size = New System.Drawing.Size(46, 16)
        Me.chk_Mandi_Tax_Cess.TabIndex = 34
        Me.chk_Mandi_Tax_Cess.Text = "Cess"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.lblDepositControl)
        Me.RadGroupBox2.Controls.Add(Me.txtDepositControl)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.rdtxtpayablescontrol)
        Me.RadGroupBox2.Controls.Add(Me.fndpayable)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox2.HeaderText = "GST"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 234)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(606, 87)
        Me.RadGroupBox2.TabIndex = 33
        Me.RadGroupBox2.Text = "GST"
        '
        'lblDepositControl
        '
        Me.lblDepositControl.AutoSize = False
        Me.lblDepositControl.BorderVisible = True
        Me.lblDepositControl.FieldName = Nothing
        Me.lblDepositControl.Location = New System.Drawing.Point(324, 46)
        Me.lblDepositControl.Name = "lblDepositControl"
        Me.lblDepositControl.Size = New System.Drawing.Size(259, 19)
        Me.lblDepositControl.TabIndex = 22
        '
        'txtDepositControl
        '
        Me.txtDepositControl.CalculationExpression = Nothing
        Me.txtDepositControl.Enabled = False
        Me.txtDepositControl.FieldCode = Nothing
        Me.txtDepositControl.FieldDesc = Nothing
        Me.txtDepositControl.FieldMaxLength = 0
        Me.txtDepositControl.FieldName = Nothing
        Me.txtDepositControl.isCalculatedField = False
        Me.txtDepositControl.IsSourceFromTable = False
        Me.txtDepositControl.IsSourceFromValueList = False
        Me.txtDepositControl.IsUnique = False
        Me.txtDepositControl.Location = New System.Drawing.Point(153, 46)
        Me.txtDepositControl.MendatroryField = False
        Me.txtDepositControl.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositControl.MyLinkLable1 = Nothing
        Me.txtDepositControl.MyLinkLable2 = Nothing
        Me.txtDepositControl.MyReadOnly = False
        Me.txtDepositControl.MyShowMasterFormButton = False
        Me.txtDepositControl.Name = "txtDepositControl"
        Me.txtDepositControl.ReferenceFieldDesc = Nothing
        Me.txtDepositControl.ReferenceFieldName = Nothing
        Me.txtDepositControl.ReferenceTableName = Nothing
        Me.txtDepositControl.Size = New System.Drawing.Size(164, 19)
        Me.txtDepositControl.TabIndex = 21
        Me.txtDepositControl.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Location = New System.Drawing.Point(23, 47)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel6.TabIndex = 23
        Me.MyLabel6.Text = "Deposit Control"
        '
        'rdtxtpayablescontrol
        '
        Me.rdtxtpayablescontrol.AutoSize = False
        Me.rdtxtpayablescontrol.BorderVisible = True
        Me.rdtxtpayablescontrol.FieldName = Nothing
        Me.rdtxtpayablescontrol.Location = New System.Drawing.Point(324, 21)
        Me.rdtxtpayablescontrol.Name = "rdtxtpayablescontrol"
        Me.rdtxtpayablescontrol.Size = New System.Drawing.Size(259, 19)
        Me.rdtxtpayablescontrol.TabIndex = 19
        '
        'fndpayable
        '
        Me.fndpayable.CalculationExpression = Nothing
        Me.fndpayable.Enabled = False
        Me.fndpayable.FieldCode = Nothing
        Me.fndpayable.FieldDesc = Nothing
        Me.fndpayable.FieldMaxLength = 0
        Me.fndpayable.FieldName = Nothing
        Me.fndpayable.isCalculatedField = False
        Me.fndpayable.IsSourceFromTable = False
        Me.fndpayable.IsSourceFromValueList = False
        Me.fndpayable.IsUnique = False
        Me.fndpayable.Location = New System.Drawing.Point(153, 21)
        Me.fndpayable.MendatroryField = False
        Me.fndpayable.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndpayable.MyLinkLable1 = Nothing
        Me.fndpayable.MyLinkLable2 = Nothing
        Me.fndpayable.MyReadOnly = False
        Me.fndpayable.MyShowMasterFormButton = False
        Me.fndpayable.Name = "fndpayable"
        Me.fndpayable.ReferenceFieldDesc = Nothing
        Me.fndpayable.ReferenceFieldName = Nothing
        Me.fndpayable.ReferenceTableName = Nothing
        Me.fndpayable.Size = New System.Drawing.Size(164, 19)
        Me.fndpayable.TabIndex = 18
        Me.fndpayable.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(23, 22)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(90, 18)
        Me.RadLabel3.TabIndex = 20
        Me.RadLabel3.Text = "Payables Control"
        '
        'ChkGSTActive
        '
        Me.ChkGSTActive.Enabled = False
        Me.ChkGSTActive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkGSTActive.Location = New System.Drawing.Point(354, 217)
        Me.ChkGSTActive.Name = "ChkGSTActive"
        Me.ChkGSTActive.Size = New System.Drawing.Size(51, 16)
        Me.ChkGSTActive.TabIndex = 32
        Me.ChkGSTActive.Text = "Active"
        '
        'fndnetpay
        '
        Me.fndnetpay.CalculationExpression = Nothing
        Me.fndnetpay.FieldCode = Nothing
        Me.fndnetpay.FieldDesc = Nothing
        Me.fndnetpay.FieldMaxLength = 0
        Me.fndnetpay.FieldName = Nothing
        Me.fndnetpay.isCalculatedField = False
        Me.fndnetpay.IsSourceFromTable = False
        Me.fndnetpay.IsSourceFromValueList = False
        Me.fndnetpay.IsUnique = False
        Me.fndnetpay.Location = New System.Drawing.Point(178, 190)
        Me.fndnetpay.MendatroryField = False
        Me.fndnetpay.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndnetpay.MyLinkLable1 = Me.lblnetpay
        Me.fndnetpay.MyLinkLable2 = Nothing
        Me.fndnetpay.MyReadOnly = False
        Me.fndnetpay.MyShowMasterFormButton = False
        Me.fndnetpay.Name = "fndnetpay"
        Me.fndnetpay.ReferenceFieldDesc = Nothing
        Me.fndnetpay.ReferenceFieldName = Nothing
        Me.fndnetpay.ReferenceTableName = Nothing
        Me.fndnetpay.Size = New System.Drawing.Size(162, 19)
        Me.fndnetpay.TabIndex = 13
        Me.fndnetpay.Value = ""
        '
        'lblnetpay
        '
        Me.lblnetpay.FieldName = Nothing
        Me.lblnetpay.Location = New System.Drawing.Point(13, 188)
        Me.lblnetpay.Name = "lblnetpay"
        Me.lblnetpay.Size = New System.Drawing.Size(110, 18)
        Me.lblnetpay.TabIndex = 18
        Me.lblnetpay.Text = "Net Payable Account"
        '
        'findTaxRelAcc
        '
        Me.findTaxRelAcc.CalculationExpression = Nothing
        Me.findTaxRelAcc.FieldCode = Nothing
        Me.findTaxRelAcc.FieldDesc = Nothing
        Me.findTaxRelAcc.FieldMaxLength = 0
        Me.findTaxRelAcc.FieldName = Nothing
        Me.findTaxRelAcc.isCalculatedField = False
        Me.findTaxRelAcc.IsSourceFromTable = False
        Me.findTaxRelAcc.IsSourceFromValueList = False
        Me.findTaxRelAcc.IsUnique = False
        Me.findTaxRelAcc.Location = New System.Drawing.Point(178, 10)
        Me.findTaxRelAcc.MendatroryField = True
        Me.findTaxRelAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findTaxRelAcc.MyLinkLable1 = Me.lblTaxRelAcc
        Me.findTaxRelAcc.MyLinkLable2 = Nothing
        Me.findTaxRelAcc.MyReadOnly = False
        Me.findTaxRelAcc.MyShowMasterFormButton = False
        Me.findTaxRelAcc.Name = "findTaxRelAcc"
        Me.findTaxRelAcc.ReferenceFieldDesc = Nothing
        Me.findTaxRelAcc.ReferenceFieldName = Nothing
        Me.findTaxRelAcc.ReferenceTableName = Nothing
        Me.findTaxRelAcc.Size = New System.Drawing.Size(162, 19)
        Me.findTaxRelAcc.TabIndex = 0
        Me.findTaxRelAcc.Value = ""
        '
        'lblTaxRelAcc
        '
        Me.lblTaxRelAcc.FieldName = Nothing
        Me.lblTaxRelAcc.Location = New System.Drawing.Point(13, 11)
        Me.lblTaxRelAcc.Name = "lblTaxRelAcc"
        Me.lblTaxRelAcc.Size = New System.Drawing.Size(108, 18)
        Me.lblTaxRelAcc.TabIndex = 31
        Me.lblTaxRelAcc.Text = "Tax Liability Account"
        '
        'findRecoverTaxAcc
        '
        Me.findRecoverTaxAcc.CalculationExpression = Nothing
        Me.findRecoverTaxAcc.FieldCode = Nothing
        Me.findRecoverTaxAcc.FieldDesc = Nothing
        Me.findRecoverTaxAcc.FieldMaxLength = 0
        Me.findRecoverTaxAcc.FieldName = Nothing
        Me.findRecoverTaxAcc.isCalculatedField = False
        Me.findRecoverTaxAcc.IsSourceFromTable = False
        Me.findRecoverTaxAcc.IsSourceFromValueList = False
        Me.findRecoverTaxAcc.IsUnique = False
        Me.findRecoverTaxAcc.Location = New System.Drawing.Point(178, 67)
        Me.findRecoverTaxAcc.MendatroryField = False
        Me.findRecoverTaxAcc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findRecoverTaxAcc.MyLinkLable1 = Me.lblRecoverTaxAcc
        Me.findRecoverTaxAcc.MyLinkLable2 = Nothing
        Me.findRecoverTaxAcc.MyReadOnly = False
        Me.findRecoverTaxAcc.MyShowMasterFormButton = False
        Me.findRecoverTaxAcc.Name = "findRecoverTaxAcc"
        Me.findRecoverTaxAcc.ReferenceFieldDesc = Nothing
        Me.findRecoverTaxAcc.ReferenceFieldName = Nothing
        Me.findRecoverTaxAcc.ReferenceTableName = Nothing
        Me.findRecoverTaxAcc.Size = New System.Drawing.Size(162, 19)
        Me.findRecoverTaxAcc.TabIndex = 3
        Me.findRecoverTaxAcc.Value = ""
        '
        'lblRecoverTaxAcc
        '
        Me.lblRecoverTaxAcc.FieldName = Nothing
        Me.lblRecoverTaxAcc.Location = New System.Drawing.Point(13, 68)
        Me.lblRecoverTaxAcc.Name = "lblRecoverTaxAcc"
        Me.lblRecoverTaxAcc.Size = New System.Drawing.Size(131, 18)
        Me.lblRecoverTaxAcc.TabIndex = 23
        Me.lblRecoverTaxAcc.Text = "Recoverable Tax Account"
        '
        'TotalRecovRate
        '
        Me.TotalRecovRate.BackColor = System.Drawing.Color.White
        Me.TotalRecovRate.CalculationExpression = Nothing
        Me.TotalRecovRate.DecimalPlaces = 0
        Me.TotalRecovRate.FieldCode = Nothing
        Me.TotalRecovRate.FieldDesc = Nothing
        Me.TotalRecovRate.FieldMaxLength = 0
        Me.TotalRecovRate.FieldName = Nothing
        Me.TotalRecovRate.isCalculatedField = False
        Me.TotalRecovRate.IsSourceFromTable = False
        Me.TotalRecovRate.IsSourceFromValueList = False
        Me.TotalRecovRate.IsUnique = False
        Me.TotalRecovRate.Location = New System.Drawing.Point(554, 215)
        Me.TotalRecovRate.MendatroryField = False
        Me.TotalRecovRate.MyLinkLable1 = Nothing
        Me.TotalRecovRate.MyLinkLable2 = Nothing
        Me.TotalRecovRate.Name = "TotalRecovRate"
        Me.TotalRecovRate.ReferenceFieldDesc = Nothing
        Me.TotalRecovRate.ReferenceFieldName = Nothing
        Me.TotalRecovRate.ReferenceTableName = Nothing
        Me.TotalRecovRate.Size = New System.Drawing.Size(68, 20)
        Me.TotalRecovRate.TabIndex = 15
        Me.TotalRecovRate.Text = "0"
        Me.TotalRecovRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TotalRecovRate.Value = 0.0R
        Me.TotalRecovRate.Visible = False
        '
        'txtRecovRate5
        '
        Me.txtRecovRate5.BackColor = System.Drawing.Color.White
        Me.txtRecovRate5.CalculationExpression = Nothing
        Me.txtRecovRate5.DecimalPlaces = 2
        Me.txtRecovRate5.FieldCode = Nothing
        Me.txtRecovRate5.FieldDesc = Nothing
        Me.txtRecovRate5.FieldMaxLength = 0
        Me.txtRecovRate5.FieldName = Nothing
        Me.txtRecovRate5.isCalculatedField = False
        Me.txtRecovRate5.IsSourceFromTable = False
        Me.txtRecovRate5.IsSourceFromValueList = False
        Me.txtRecovRate5.IsUnique = False
        Me.txtRecovRate5.Location = New System.Drawing.Point(554, 166)
        Me.txtRecovRate5.MendatroryField = False
        Me.txtRecovRate5.MyLinkLable1 = Nothing
        Me.txtRecovRate5.MyLinkLable2 = Nothing
        Me.txtRecovRate5.Name = "txtRecovRate5"
        Me.txtRecovRate5.ReferenceFieldDesc = Nothing
        Me.txtRecovRate5.ReferenceFieldName = Nothing
        Me.txtRecovRate5.ReferenceTableName = Nothing
        Me.txtRecovRate5.Size = New System.Drawing.Size(68, 20)
        Me.txtRecovRate5.TabIndex = 12
        Me.txtRecovRate5.Text = "0"
        Me.txtRecovRate5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRecovRate5.Value = 0.0R
        '
        'txtRecovRate4
        '
        Me.txtRecovRate4.BackColor = System.Drawing.Color.White
        Me.txtRecovRate4.CalculationExpression = Nothing
        Me.txtRecovRate4.DecimalPlaces = 2
        Me.txtRecovRate4.FieldCode = Nothing
        Me.txtRecovRate4.FieldDesc = Nothing
        Me.txtRecovRate4.FieldMaxLength = 0
        Me.txtRecovRate4.FieldName = Nothing
        Me.txtRecovRate4.isCalculatedField = False
        Me.txtRecovRate4.IsSourceFromTable = False
        Me.txtRecovRate4.IsSourceFromValueList = False
        Me.txtRecovRate4.IsUnique = False
        Me.txtRecovRate4.Location = New System.Drawing.Point(554, 141)
        Me.txtRecovRate4.MendatroryField = False
        Me.txtRecovRate4.MyLinkLable1 = Nothing
        Me.txtRecovRate4.MyLinkLable2 = Nothing
        Me.txtRecovRate4.Name = "txtRecovRate4"
        Me.txtRecovRate4.ReferenceFieldDesc = Nothing
        Me.txtRecovRate4.ReferenceFieldName = Nothing
        Me.txtRecovRate4.ReferenceTableName = Nothing
        Me.txtRecovRate4.Size = New System.Drawing.Size(68, 20)
        Me.txtRecovRate4.TabIndex = 10
        Me.txtRecovRate4.Text = "0"
        Me.txtRecovRate4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRecovRate4.Value = 0.0R
        '
        'txtRecovRate3
        '
        Me.txtRecovRate3.BackColor = System.Drawing.Color.White
        Me.txtRecovRate3.CalculationExpression = Nothing
        Me.txtRecovRate3.DecimalPlaces = 2
        Me.txtRecovRate3.FieldCode = Nothing
        Me.txtRecovRate3.FieldDesc = Nothing
        Me.txtRecovRate3.FieldMaxLength = 0
        Me.txtRecovRate3.FieldName = Nothing
        Me.txtRecovRate3.isCalculatedField = False
        Me.txtRecovRate3.IsSourceFromTable = False
        Me.txtRecovRate3.IsSourceFromValueList = False
        Me.txtRecovRate3.IsUnique = False
        Me.txtRecovRate3.Location = New System.Drawing.Point(554, 116)
        Me.txtRecovRate3.MendatroryField = False
        Me.txtRecovRate3.MyLinkLable1 = Nothing
        Me.txtRecovRate3.MyLinkLable2 = Nothing
        Me.txtRecovRate3.Name = "txtRecovRate3"
        Me.txtRecovRate3.ReferenceFieldDesc = Nothing
        Me.txtRecovRate3.ReferenceFieldName = Nothing
        Me.txtRecovRate3.ReferenceTableName = Nothing
        Me.txtRecovRate3.Size = New System.Drawing.Size(68, 20)
        Me.txtRecovRate3.TabIndex = 8
        Me.txtRecovRate3.Text = "0"
        Me.txtRecovRate3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRecovRate3.Value = 0.0R
        '
        'txtRecovRate2
        '
        Me.txtRecovRate2.BackColor = System.Drawing.Color.White
        Me.txtRecovRate2.CalculationExpression = Nothing
        Me.txtRecovRate2.DecimalPlaces = 2
        Me.txtRecovRate2.FieldCode = Nothing
        Me.txtRecovRate2.FieldDesc = Nothing
        Me.txtRecovRate2.FieldMaxLength = 0
        Me.txtRecovRate2.FieldName = Nothing
        Me.txtRecovRate2.isCalculatedField = False
        Me.txtRecovRate2.IsSourceFromTable = False
        Me.txtRecovRate2.IsSourceFromValueList = False
        Me.txtRecovRate2.IsUnique = False
        Me.txtRecovRate2.Location = New System.Drawing.Point(554, 91)
        Me.txtRecovRate2.MendatroryField = False
        Me.txtRecovRate2.MyLinkLable1 = Nothing
        Me.txtRecovRate2.MyLinkLable2 = Nothing
        Me.txtRecovRate2.Name = "txtRecovRate2"
        Me.txtRecovRate2.ReferenceFieldDesc = Nothing
        Me.txtRecovRate2.ReferenceFieldName = Nothing
        Me.txtRecovRate2.ReferenceTableName = Nothing
        Me.txtRecovRate2.Size = New System.Drawing.Size(68, 20)
        Me.txtRecovRate2.TabIndex = 6
        Me.txtRecovRate2.Text = "0"
        Me.txtRecovRate2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRecovRate2.Value = 0.0R
        '
        'txtRecoverRate
        '
        Me.txtRecoverRate.BackColor = System.Drawing.Color.White
        Me.txtRecoverRate.CalculationExpression = Nothing
        Me.txtRecoverRate.DecimalPlaces = 2
        Me.txtRecoverRate.FieldCode = Nothing
        Me.txtRecoverRate.FieldDesc = Nothing
        Me.txtRecoverRate.FieldMaxLength = 0
        Me.txtRecoverRate.FieldName = Nothing
        Me.txtRecoverRate.isCalculatedField = False
        Me.txtRecoverRate.IsSourceFromTable = False
        Me.txtRecoverRate.IsSourceFromValueList = False
        Me.txtRecoverRate.IsUnique = False
        Me.txtRecoverRate.Location = New System.Drawing.Point(554, 65)
        Me.txtRecoverRate.MendatroryField = False
        Me.txtRecoverRate.MyLinkLable1 = Nothing
        Me.txtRecoverRate.MyLinkLable2 = Nothing
        Me.txtRecoverRate.Name = "txtRecoverRate"
        Me.txtRecoverRate.ReferenceFieldDesc = Nothing
        Me.txtRecoverRate.ReferenceFieldName = Nothing
        Me.txtRecoverRate.ReferenceTableName = Nothing
        Me.txtRecoverRate.Size = New System.Drawing.Size(68, 20)
        Me.txtRecoverRate.TabIndex = 4
        Me.txtRecoverRate.Text = "0"
        Me.txtRecoverRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRecoverRate.Value = 0.0R
        '
        'txtRecovTaxAccDesc5
        '
        Me.txtRecovTaxAccDesc5.CalculationExpression = Nothing
        Me.txtRecovTaxAccDesc5.FieldCode = Nothing
        Me.txtRecovTaxAccDesc5.FieldDesc = Nothing
        Me.txtRecovTaxAccDesc5.FieldMaxLength = 0
        Me.txtRecovTaxAccDesc5.FieldName = Nothing
        Me.txtRecovTaxAccDesc5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecovTaxAccDesc5.isCalculatedField = False
        Me.txtRecovTaxAccDesc5.IsSourceFromTable = False
        Me.txtRecovTaxAccDesc5.IsSourceFromValueList = False
        Me.txtRecovTaxAccDesc5.IsUnique = False
        Me.txtRecovTaxAccDesc5.Location = New System.Drawing.Point(354, 166)
        Me.txtRecovTaxAccDesc5.MaxLength = 50
        Me.txtRecovTaxAccDesc5.MendatroryField = False
        Me.txtRecovTaxAccDesc5.MyLinkLable1 = Nothing
        Me.txtRecovTaxAccDesc5.MyLinkLable2 = Nothing
        Me.txtRecovTaxAccDesc5.Name = "txtRecovTaxAccDesc5"
        Me.txtRecovTaxAccDesc5.ReadOnly = True
        Me.txtRecovTaxAccDesc5.ReferenceFieldDesc = Nothing
        Me.txtRecovTaxAccDesc5.ReferenceFieldName = Nothing
        Me.txtRecovTaxAccDesc5.ReferenceTableName = Nothing
        Me.txtRecovTaxAccDesc5.Size = New System.Drawing.Size(190, 18)
        Me.txtRecovTaxAccDesc5.TabIndex = 24
        Me.txtRecovTaxAccDesc5.TabStop = False
        '
        'fndRecovTaxAcc5
        '
        Me.fndRecovTaxAcc5.CalculationExpression = Nothing
        Me.fndRecovTaxAcc5.FieldCode = Nothing
        Me.fndRecovTaxAcc5.FieldDesc = Nothing
        Me.fndRecovTaxAcc5.FieldMaxLength = 0
        Me.fndRecovTaxAcc5.FieldName = Nothing
        Me.fndRecovTaxAcc5.isCalculatedField = False
        Me.fndRecovTaxAcc5.IsSourceFromTable = False
        Me.fndRecovTaxAcc5.IsSourceFromValueList = False
        Me.fndRecovTaxAcc5.IsUnique = False
        Me.fndRecovTaxAcc5.Location = New System.Drawing.Point(178, 166)
        Me.fndRecovTaxAcc5.MendatroryField = False
        Me.fndRecovTaxAcc5.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRecovTaxAcc5.MyLinkLable1 = Me.MyLabel4
        Me.fndRecovTaxAcc5.MyLinkLable2 = Nothing
        Me.fndRecovTaxAcc5.MyReadOnly = False
        Me.fndRecovTaxAcc5.MyShowMasterFormButton = False
        Me.fndRecovTaxAcc5.Name = "fndRecovTaxAcc5"
        Me.fndRecovTaxAcc5.ReferenceFieldDesc = Nothing
        Me.fndRecovTaxAcc5.ReferenceFieldName = Nothing
        Me.fndRecovTaxAcc5.ReferenceTableName = Nothing
        Me.fndRecovTaxAcc5.Size = New System.Drawing.Size(162, 19)
        Me.fndRecovTaxAcc5.TabIndex = 11
        Me.fndRecovTaxAcc5.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(13, 166)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(141, 18)
        Me.MyLabel4.TabIndex = 19
        Me.MyLabel4.Text = "Recoverable Tax Account 5"
        '
        'txtRecovTaxAccDesc4
        '
        Me.txtRecovTaxAccDesc4.CalculationExpression = Nothing
        Me.txtRecovTaxAccDesc4.FieldCode = Nothing
        Me.txtRecovTaxAccDesc4.FieldDesc = Nothing
        Me.txtRecovTaxAccDesc4.FieldMaxLength = 0
        Me.txtRecovTaxAccDesc4.FieldName = Nothing
        Me.txtRecovTaxAccDesc4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecovTaxAccDesc4.isCalculatedField = False
        Me.txtRecovTaxAccDesc4.IsSourceFromTable = False
        Me.txtRecovTaxAccDesc4.IsSourceFromValueList = False
        Me.txtRecovTaxAccDesc4.IsUnique = False
        Me.txtRecovTaxAccDesc4.Location = New System.Drawing.Point(354, 141)
        Me.txtRecovTaxAccDesc4.MaxLength = 50
        Me.txtRecovTaxAccDesc4.MendatroryField = False
        Me.txtRecovTaxAccDesc4.MyLinkLable1 = Nothing
        Me.txtRecovTaxAccDesc4.MyLinkLable2 = Nothing
        Me.txtRecovTaxAccDesc4.Name = "txtRecovTaxAccDesc4"
        Me.txtRecovTaxAccDesc4.ReadOnly = True
        Me.txtRecovTaxAccDesc4.ReferenceFieldDesc = Nothing
        Me.txtRecovTaxAccDesc4.ReferenceFieldName = Nothing
        Me.txtRecovTaxAccDesc4.ReferenceTableName = Nothing
        Me.txtRecovTaxAccDesc4.Size = New System.Drawing.Size(190, 18)
        Me.txtRecovTaxAccDesc4.TabIndex = 25
        Me.txtRecovTaxAccDesc4.TabStop = False
        '
        'fndRecovTaxAcc4
        '
        Me.fndRecovTaxAcc4.CalculationExpression = Nothing
        Me.fndRecovTaxAcc4.FieldCode = Nothing
        Me.fndRecovTaxAcc4.FieldDesc = Nothing
        Me.fndRecovTaxAcc4.FieldMaxLength = 0
        Me.fndRecovTaxAcc4.FieldName = Nothing
        Me.fndRecovTaxAcc4.isCalculatedField = False
        Me.fndRecovTaxAcc4.IsSourceFromTable = False
        Me.fndRecovTaxAcc4.IsSourceFromValueList = False
        Me.fndRecovTaxAcc4.IsUnique = False
        Me.fndRecovTaxAcc4.Location = New System.Drawing.Point(178, 141)
        Me.fndRecovTaxAcc4.MendatroryField = False
        Me.fndRecovTaxAcc4.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRecovTaxAcc4.MyLinkLable1 = Me.MyLabel3
        Me.fndRecovTaxAcc4.MyLinkLable2 = Nothing
        Me.fndRecovTaxAcc4.MyReadOnly = False
        Me.fndRecovTaxAcc4.MyShowMasterFormButton = False
        Me.fndRecovTaxAcc4.Name = "fndRecovTaxAcc4"
        Me.fndRecovTaxAcc4.ReferenceFieldDesc = Nothing
        Me.fndRecovTaxAcc4.ReferenceFieldName = Nothing
        Me.fndRecovTaxAcc4.ReferenceTableName = Nothing
        Me.fndRecovTaxAcc4.Size = New System.Drawing.Size(162, 19)
        Me.fndRecovTaxAcc4.TabIndex = 9
        Me.fndRecovTaxAcc4.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(13, 141)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(141, 18)
        Me.MyLabel3.TabIndex = 20
        Me.MyLabel3.Text = "Recoverable Tax Account 4"
        '
        'txtRecovTaxAccDesc3
        '
        Me.txtRecovTaxAccDesc3.CalculationExpression = Nothing
        Me.txtRecovTaxAccDesc3.FieldCode = Nothing
        Me.txtRecovTaxAccDesc3.FieldDesc = Nothing
        Me.txtRecovTaxAccDesc3.FieldMaxLength = 0
        Me.txtRecovTaxAccDesc3.FieldName = Nothing
        Me.txtRecovTaxAccDesc3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecovTaxAccDesc3.isCalculatedField = False
        Me.txtRecovTaxAccDesc3.IsSourceFromTable = False
        Me.txtRecovTaxAccDesc3.IsSourceFromValueList = False
        Me.txtRecovTaxAccDesc3.IsUnique = False
        Me.txtRecovTaxAccDesc3.Location = New System.Drawing.Point(354, 116)
        Me.txtRecovTaxAccDesc3.MaxLength = 50
        Me.txtRecovTaxAccDesc3.MendatroryField = False
        Me.txtRecovTaxAccDesc3.MyLinkLable1 = Nothing
        Me.txtRecovTaxAccDesc3.MyLinkLable2 = Nothing
        Me.txtRecovTaxAccDesc3.Name = "txtRecovTaxAccDesc3"
        Me.txtRecovTaxAccDesc3.ReadOnly = True
        Me.txtRecovTaxAccDesc3.ReferenceFieldDesc = Nothing
        Me.txtRecovTaxAccDesc3.ReferenceFieldName = Nothing
        Me.txtRecovTaxAccDesc3.ReferenceTableName = Nothing
        Me.txtRecovTaxAccDesc3.Size = New System.Drawing.Size(190, 18)
        Me.txtRecovTaxAccDesc3.TabIndex = 26
        Me.txtRecovTaxAccDesc3.TabStop = False
        '
        'fndRecovTaxAcc3
        '
        Me.fndRecovTaxAcc3.CalculationExpression = Nothing
        Me.fndRecovTaxAcc3.FieldCode = Nothing
        Me.fndRecovTaxAcc3.FieldDesc = Nothing
        Me.fndRecovTaxAcc3.FieldMaxLength = 0
        Me.fndRecovTaxAcc3.FieldName = Nothing
        Me.fndRecovTaxAcc3.isCalculatedField = False
        Me.fndRecovTaxAcc3.IsSourceFromTable = False
        Me.fndRecovTaxAcc3.IsSourceFromValueList = False
        Me.fndRecovTaxAcc3.IsUnique = False
        Me.fndRecovTaxAcc3.Location = New System.Drawing.Point(178, 116)
        Me.fndRecovTaxAcc3.MendatroryField = False
        Me.fndRecovTaxAcc3.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRecovTaxAcc3.MyLinkLable1 = Me.MyLabel2
        Me.fndRecovTaxAcc3.MyLinkLable2 = Nothing
        Me.fndRecovTaxAcc3.MyReadOnly = False
        Me.fndRecovTaxAcc3.MyShowMasterFormButton = False
        Me.fndRecovTaxAcc3.Name = "fndRecovTaxAcc3"
        Me.fndRecovTaxAcc3.ReferenceFieldDesc = Nothing
        Me.fndRecovTaxAcc3.ReferenceFieldName = Nothing
        Me.fndRecovTaxAcc3.ReferenceTableName = Nothing
        Me.fndRecovTaxAcc3.Size = New System.Drawing.Size(162, 19)
        Me.fndRecovTaxAcc3.TabIndex = 7
        Me.fndRecovTaxAcc3.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(13, 116)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(141, 18)
        Me.MyLabel2.TabIndex = 21
        Me.MyLabel2.Text = "Recoverable Tax Account 3"
        '
        'txtRecovTaxAccDesc2
        '
        Me.txtRecovTaxAccDesc2.CalculationExpression = Nothing
        Me.txtRecovTaxAccDesc2.FieldCode = Nothing
        Me.txtRecovTaxAccDesc2.FieldDesc = Nothing
        Me.txtRecovTaxAccDesc2.FieldMaxLength = 0
        Me.txtRecovTaxAccDesc2.FieldName = Nothing
        Me.txtRecovTaxAccDesc2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecovTaxAccDesc2.isCalculatedField = False
        Me.txtRecovTaxAccDesc2.IsSourceFromTable = False
        Me.txtRecovTaxAccDesc2.IsSourceFromValueList = False
        Me.txtRecovTaxAccDesc2.IsUnique = False
        Me.txtRecovTaxAccDesc2.Location = New System.Drawing.Point(354, 92)
        Me.txtRecovTaxAccDesc2.MaxLength = 50
        Me.txtRecovTaxAccDesc2.MendatroryField = False
        Me.txtRecovTaxAccDesc2.MyLinkLable1 = Nothing
        Me.txtRecovTaxAccDesc2.MyLinkLable2 = Nothing
        Me.txtRecovTaxAccDesc2.Name = "txtRecovTaxAccDesc2"
        Me.txtRecovTaxAccDesc2.ReadOnly = True
        Me.txtRecovTaxAccDesc2.ReferenceFieldDesc = Nothing
        Me.txtRecovTaxAccDesc2.ReferenceFieldName = Nothing
        Me.txtRecovTaxAccDesc2.ReferenceTableName = Nothing
        Me.txtRecovTaxAccDesc2.Size = New System.Drawing.Size(190, 18)
        Me.txtRecovTaxAccDesc2.TabIndex = 27
        Me.txtRecovTaxAccDesc2.TabStop = False
        '
        'fndRecovTaxAcc2
        '
        Me.fndRecovTaxAcc2.CalculationExpression = Nothing
        Me.fndRecovTaxAcc2.FieldCode = Nothing
        Me.fndRecovTaxAcc2.FieldDesc = Nothing
        Me.fndRecovTaxAcc2.FieldMaxLength = 0
        Me.fndRecovTaxAcc2.FieldName = Nothing
        Me.fndRecovTaxAcc2.isCalculatedField = False
        Me.fndRecovTaxAcc2.IsSourceFromTable = False
        Me.fndRecovTaxAcc2.IsSourceFromValueList = False
        Me.fndRecovTaxAcc2.IsUnique = False
        Me.fndRecovTaxAcc2.Location = New System.Drawing.Point(178, 92)
        Me.fndRecovTaxAcc2.MendatroryField = False
        Me.fndRecovTaxAcc2.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRecovTaxAcc2.MyLinkLable1 = Me.MyLabel1
        Me.fndRecovTaxAcc2.MyLinkLable2 = Nothing
        Me.fndRecovTaxAcc2.MyReadOnly = False
        Me.fndRecovTaxAcc2.MyShowMasterFormButton = False
        Me.fndRecovTaxAcc2.Name = "fndRecovTaxAcc2"
        Me.fndRecovTaxAcc2.ReferenceFieldDesc = Nothing
        Me.fndRecovTaxAcc2.ReferenceFieldName = Nothing
        Me.fndRecovTaxAcc2.ReferenceTableName = Nothing
        Me.fndRecovTaxAcc2.Size = New System.Drawing.Size(162, 19)
        Me.fndRecovTaxAcc2.TabIndex = 5
        Me.fndRecovTaxAcc2.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(13, 91)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(141, 18)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "Recoverable Tax Account 2"
        '
        'lblRecoverRate
        '
        Me.lblRecoverRate.FieldName = Nothing
        Me.lblRecoverRate.Location = New System.Drawing.Point(554, 44)
        Me.lblRecoverRate.Name = "lblRecoverRate"
        Me.lblRecoverRate.Size = New System.Drawing.Size(93, 18)
        Me.lblRecoverRate.TabIndex = 29
        Me.lblRecoverRate.Text = "Recoverable Rate"
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Location = New System.Drawing.Point(13, 215)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(30, 18)
        Me.lblType.TabIndex = 17
        Me.lblType.Text = "Type"
        '
        'drpTaxtype
        '
        Me.drpTaxtype.AutoCompleteDisplayMember = Nothing
        Me.drpTaxtype.AutoCompleteValueMember = Nothing
        Me.drpTaxtype.CalculationExpression = Nothing
        Me.drpTaxtype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.drpTaxtype.FieldCode = Nothing
        Me.drpTaxtype.FieldDesc = Nothing
        Me.drpTaxtype.FieldMaxLength = 0
        Me.drpTaxtype.FieldName = Nothing
        Me.drpTaxtype.isCalculatedField = False
        Me.drpTaxtype.IsSourceFromTable = False
        Me.drpTaxtype.IsSourceFromValueList = False
        Me.drpTaxtype.IsUnique = False
        Me.drpTaxtype.Location = New System.Drawing.Point(178, 215)
        Me.drpTaxtype.MendatroryField = False
        Me.drpTaxtype.MyLinkLable1 = Me.lblType
        Me.drpTaxtype.MyLinkLable2 = Nothing
        Me.drpTaxtype.Name = "drpTaxtype"
        Me.drpTaxtype.ReferenceFieldDesc = Nothing
        Me.drpTaxtype.ReferenceFieldName = Nothing
        Me.drpTaxtype.ReferenceTableName = Nothing
        Me.drpTaxtype.Size = New System.Drawing.Size(162, 20)
        Me.drpTaxtype.TabIndex = 14
        '
        'chkExcisable
        '
        Me.chkExcisable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExcisable.Location = New System.Drawing.Point(178, 44)
        Me.chkExcisable.Name = "chkExcisable"
        Me.chkExcisable.Size = New System.Drawing.Size(69, 16)
        Me.chkExcisable.TabIndex = 2
        Me.chkExcisable.Text = "Excisable"
        '
        'txtnetpay
        '
        Me.txtnetpay.CalculationExpression = Nothing
        Me.txtnetpay.FieldCode = Nothing
        Me.txtnetpay.FieldDesc = Nothing
        Me.txtnetpay.FieldMaxLength = 0
        Me.txtnetpay.FieldName = Nothing
        Me.txtnetpay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnetpay.isCalculatedField = False
        Me.txtnetpay.IsSourceFromTable = False
        Me.txtnetpay.IsSourceFromValueList = False
        Me.txtnetpay.IsUnique = False
        Me.txtnetpay.Location = New System.Drawing.Point(354, 188)
        Me.txtnetpay.MaxLength = 50
        Me.txtnetpay.MendatroryField = False
        Me.txtnetpay.MyLinkLable1 = Nothing
        Me.txtnetpay.MyLinkLable2 = Nothing
        Me.txtnetpay.Name = "txtnetpay"
        Me.txtnetpay.ReadOnly = True
        Me.txtnetpay.ReferenceFieldDesc = Nothing
        Me.txtnetpay.ReferenceFieldName = Nothing
        Me.txtnetpay.ReferenceTableName = Nothing
        Me.txtnetpay.Size = New System.Drawing.Size(268, 18)
        Me.txtnetpay.TabIndex = 16
        Me.txtnetpay.TabStop = False
        '
        'txtRecoverTaxAccDesc
        '
        Me.txtRecoverTaxAccDesc.CalculationExpression = Nothing
        Me.txtRecoverTaxAccDesc.FieldCode = Nothing
        Me.txtRecoverTaxAccDesc.FieldDesc = Nothing
        Me.txtRecoverTaxAccDesc.FieldMaxLength = 0
        Me.txtRecoverTaxAccDesc.FieldName = Nothing
        Me.txtRecoverTaxAccDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecoverTaxAccDesc.isCalculatedField = False
        Me.txtRecoverTaxAccDesc.IsSourceFromTable = False
        Me.txtRecoverTaxAccDesc.IsSourceFromValueList = False
        Me.txtRecoverTaxAccDesc.IsUnique = False
        Me.txtRecoverTaxAccDesc.Location = New System.Drawing.Point(354, 68)
        Me.txtRecoverTaxAccDesc.MaxLength = 50
        Me.txtRecoverTaxAccDesc.MendatroryField = False
        Me.txtRecoverTaxAccDesc.MyLinkLable1 = Nothing
        Me.txtRecoverTaxAccDesc.MyLinkLable2 = Nothing
        Me.txtRecoverTaxAccDesc.Name = "txtRecoverTaxAccDesc"
        Me.txtRecoverTaxAccDesc.ReadOnly = True
        Me.txtRecoverTaxAccDesc.ReferenceFieldDesc = Nothing
        Me.txtRecoverTaxAccDesc.ReferenceFieldName = Nothing
        Me.txtRecoverTaxAccDesc.ReferenceTableName = Nothing
        Me.txtRecoverTaxAccDesc.Size = New System.Drawing.Size(190, 18)
        Me.txtRecoverTaxAccDesc.TabIndex = 28
        Me.txtRecoverTaxAccDesc.TabStop = False
        '
        'txtTaxRelAccDesc
        '
        Me.txtTaxRelAccDesc.CalculationExpression = Nothing
        Me.txtTaxRelAccDesc.FieldCode = Nothing
        Me.txtTaxRelAccDesc.FieldDesc = Nothing
        Me.txtTaxRelAccDesc.FieldMaxLength = 0
        Me.txtTaxRelAccDesc.FieldName = Nothing
        Me.txtTaxRelAccDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxRelAccDesc.isCalculatedField = False
        Me.txtTaxRelAccDesc.IsSourceFromTable = False
        Me.txtTaxRelAccDesc.IsSourceFromValueList = False
        Me.txtTaxRelAccDesc.IsUnique = False
        Me.txtTaxRelAccDesc.Location = New System.Drawing.Point(354, 10)
        Me.txtTaxRelAccDesc.MendatroryField = True
        Me.txtTaxRelAccDesc.MyLinkLable1 = Nothing
        Me.txtTaxRelAccDesc.MyLinkLable2 = Nothing
        Me.txtTaxRelAccDesc.Name = "txtTaxRelAccDesc"
        Me.txtTaxRelAccDesc.ReadOnly = True
        Me.txtTaxRelAccDesc.ReferenceFieldDesc = Nothing
        Me.txtTaxRelAccDesc.ReferenceFieldName = Nothing
        Me.txtTaxRelAccDesc.ReferenceTableName = Nothing
        Me.txtTaxRelAccDesc.Size = New System.Drawing.Size(297, 18)
        Me.txtTaxRelAccDesc.TabIndex = 30
        Me.txtTaxRelAccDesc.TabStop = False
        '
        'chkTaxRecover
        '
        Me.chkTaxRecover.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTaxRecover.Location = New System.Drawing.Point(13, 44)
        Me.chkTaxRecover.Name = "chkTaxRecover"
        Me.chkTaxRecover.Size = New System.Drawing.Size(106, 16)
        Me.chkTaxRecover.TabIndex = 1
        Me.chkTaxRecover.Text = "Tax Recoverable"
        '
        'btnReset
        '
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(272, 16)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 20)
        Me.btnReset.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(789, 25)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 25)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(3, 25)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(68, 18)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(860, 597)
        Me.SplitContainer1.SplitterDistance = 543
        Me.SplitContainer1.TabIndex = 0
        '
        'frmTaxAuthority
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 617)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmTaxAuthority"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tax Authority"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkmanditax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAuthority, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAccounts.ResumeLayout(False)
        Me.gbAccounts.PerformLayout()
        CType(Me.chk_Mandi_Tax_Cess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lblDepositControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdtxtpayablescontrol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkGSTActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblnetpay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxRelAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRecoverTaxAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TotalRecovRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovRate5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovRate4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovRate3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovRate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecoverRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovTaxAccDesc5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovTaxAccDesc4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovTaxAccDesc3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecovTaxAccDesc2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRecoverRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.drpTaxtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnetpay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecoverTaxAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxRelAccDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTaxRecover, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents gbAccounts As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtRecoverTaxAccDesc As common.Controls.MyTextBox
    Friend WithEvents txtTaxRelAccDesc As common.Controls.MyTextBox
    Friend WithEvents chkTaxRecover As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtnetpay As common.Controls.MyTextBox
    Friend WithEvents chkExcisable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents drpTaxtype As common.Controls.MyComboBox
    Friend WithEvents lblRecoverRate As common.Controls.MyLabel
    Friend WithEvents lblDesc As common.Controls.MyLabel
    Friend WithEvents lblTaxRelAcc As common.Controls.MyLabel
    Friend WithEvents lblRecoverTaxAcc As common.Controls.MyLabel
    Friend WithEvents lblAuthority As common.Controls.MyLabel
    Friend WithEvents lblnetpay As common.Controls.MyLabel
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents txtRecovTaxAccDesc5 As common.Controls.MyTextBox
    Friend WithEvents fndRecovTaxAcc5 As common.UserControls.txtFinder
    Friend WithEvents txtRecovTaxAccDesc4 As common.Controls.MyTextBox
    Friend WithEvents fndRecovTaxAcc4 As common.UserControls.txtFinder
    Friend WithEvents txtRecovTaxAccDesc3 As common.Controls.MyTextBox
    Friend WithEvents fndRecovTaxAcc3 As common.UserControls.txtFinder
    Friend WithEvents txtRecovTaxAccDesc2 As common.Controls.MyTextBox
    Friend WithEvents fndRecovTaxAcc2 As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TotalRecovRate As common.MyNumBox
    Friend WithEvents txtRecovRate5 As common.MyNumBox
    Friend WithEvents txtRecovRate4 As common.MyNumBox
    Friend WithEvents txtRecovRate3 As common.MyNumBox
    Friend WithEvents txtRecovRate2 As common.MyNumBox
    Friend WithEvents txtRecoverRate As common.MyNumBox
    Friend WithEvents findRecoverTaxAcc As common.UserControls.txtFinder
    Friend WithEvents findTaxAuthority As common.UserControls.txtNavigator
    Friend WithEvents findTaxRelAcc As common.UserControls.txtFinder
    Friend WithEvents fndnetpay As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtBaseCurrency As common.UserControls.txtFinder
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents chkmanditax As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents cbgCSA As common.MyCheckBoxGrid
    Friend WithEvents ChkGSTActive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblDepositControl As common.Controls.MyLabel
    Friend WithEvents txtDepositControl As common.UserControls.txtFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents rdtxtpayablescontrol As common.Controls.MyLabel
    Friend WithEvents fndpayable As common.UserControls.txtFinder
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents chk_Mandi_Tax_Cess As Telerik.WinControls.UI.RadCheckBox
End Class

