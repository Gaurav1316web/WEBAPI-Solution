Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNatureOfDeduction
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
        Me.components = New System.ComponentModel.Container()
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn1 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewDecimalColumn2 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewDecimalColumn3 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewDecimalColumn4 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewDecimalColumn5 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewDecimalColumn6 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewDecimalColumn7 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.gbdesignation = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.chkTCSTDSamountgreater50KpreviousYear = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtMinServicePer = New common.MyNumBox()
        Me.chkbuyerfilereturnlasttwoyear = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCuttoffDocument = New common.Controls.MyTextBox()
        Me.ChkNonPAN = New Telerik.WinControls.UI.RadCheckBox()
        Me.fnd_GL_Account = New common.UserControls.txtFinder()
        Me.lbltdssec = New common.Controls.MyLabel()
        Me.lblGlAccount = New common.Controls.MyLabel()
        Me.Fnd_DeductionNew = New common.UserControls.txtNavigator()
        Me.lblnDeduc = New common.Controls.MyLabel()
        Me.fndTdsNew = New common.UserControls.txtFinder()
        Me.txtremark = New common.Controls.MyTextBox()
        Me.lblreason = New common.Controls.MyLabel()
        Me.dgvdeduction = New common.UserControls.MyRadGridView()
        Me.lblcum = New common.Controls.MyLabel()
        Me.lblmodify = New common.Controls.MyLabel()
        Me.ddldeduction = New common.Controls.MyComboBox()
        Me.chkinactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txttdsdes = New common.Controls.MyTextBox()
        Me.txtcum = New common.Controls.MyTextBox()
        Me.txtmdate = New common.Controls.MyTextBox()
        Me.txtdes = New common.Controls.MyTextBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.MenuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.ToolTipNDeduction = New System.Windows.Forms.ToolTip(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbdesignation.SuspendLayout()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTCSTDSamountgreater50KpreviousYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinServicePer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkbuyerfilereturnlasttwoyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCuttoffDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkNonPAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltdssec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGlAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblnDeduc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblreason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvdeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvdeduction.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmodify, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddldeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkinactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttdsdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtmdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbdesignation
        '
        Me.gbdesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbdesignation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbdesignation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gbdesignation.Controls.Add(Me.MyLabel40)
        Me.gbdesignation.Controls.Add(Me.chkTCSTDSamountgreater50KpreviousYear)
        Me.gbdesignation.Controls.Add(Me.txtMinServicePer)
        Me.gbdesignation.Controls.Add(Me.chkbuyerfilereturnlasttwoyear)
        Me.gbdesignation.Controls.Add(Me.MyLabel1)
        Me.gbdesignation.Controls.Add(Me.txtCuttoffDocument)
        Me.gbdesignation.Controls.Add(Me.ChkNonPAN)
        Me.gbdesignation.Controls.Add(Me.fnd_GL_Account)
        Me.gbdesignation.Controls.Add(Me.lblGlAccount)
        Me.gbdesignation.Controls.Add(Me.Fnd_DeductionNew)
        Me.gbdesignation.Controls.Add(Me.fndTdsNew)
        Me.gbdesignation.Controls.Add(Me.txtremark)
        Me.gbdesignation.Controls.Add(Me.lblreason)
        Me.gbdesignation.Controls.Add(Me.dgvdeduction)
        Me.gbdesignation.Controls.Add(Me.lbltdssec)
        Me.gbdesignation.Controls.Add(Me.lblcum)
        Me.gbdesignation.Controls.Add(Me.lblmodify)
        Me.gbdesignation.Controls.Add(Me.ddldeduction)
        Me.gbdesignation.Controls.Add(Me.chkinactive)
        Me.gbdesignation.Controls.Add(Me.txttdsdes)
        Me.gbdesignation.Controls.Add(Me.txtcum)
        Me.gbdesignation.Controls.Add(Me.txtmdate)
        Me.gbdesignation.Controls.Add(Me.lblnDeduc)
        Me.gbdesignation.Controls.Add(Me.txtdes)
        Me.gbdesignation.Controls.Add(Me.btnnew)
        Me.gbdesignation.HeaderPosition = Telerik.WinControls.UI.HeaderPosition.Left
        Me.gbdesignation.HeaderText = ""
        Me.gbdesignation.Location = New System.Drawing.Point(17, 14)
        Me.gbdesignation.Name = "gbdesignation"
        Me.gbdesignation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbdesignation.Size = New System.Drawing.Size(816, 388)
        Me.gbdesignation.TabIndex = 0
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(362, 81)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(106, 16)
        Me.MyLabel40.TabIndex = 1403
        Me.MyLabel40.Text = "Minimum Service %"
        '
        'chkTCSTDSamountgreater50KpreviousYear
        '
        Me.chkTCSTDSamountgreater50KpreviousYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCSTDSamountgreater50KpreviousYear.Location = New System.Drawing.Point(519, 81)
        Me.chkTCSTDSamountgreater50KpreviousYear.Name = "chkTCSTDSamountgreater50KpreviousYear"
        Me.chkTCSTDSamountgreater50KpreviousYear.Size = New System.Drawing.Size(291, 16)
        Me.chkTCSTDSamountgreater50KpreviousYear.TabIndex = 1375
        Me.chkTCSTDSamountgreater50KpreviousYear.Text = "TCS/TDS Amount Greater Than 50K in Previous Year"
        '
        'txtMinServicePer
        '
        Me.txtMinServicePer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMinServicePer.CalculationExpression = Nothing
        Me.txtMinServicePer.DecimalPlaces = 2
        Me.txtMinServicePer.FieldCode = Nothing
        Me.txtMinServicePer.FieldDesc = Nothing
        Me.txtMinServicePer.FieldMaxLength = 0
        Me.txtMinServicePer.FieldName = Nothing
        Me.txtMinServicePer.isCalculatedField = False
        Me.txtMinServicePer.IsSourceFromTable = False
        Me.txtMinServicePer.IsSourceFromValueList = False
        Me.txtMinServicePer.IsUnique = False
        Me.txtMinServicePer.Location = New System.Drawing.Point(473, 79)
        Me.txtMinServicePer.MendatroryField = False
        Me.txtMinServicePer.MyLinkLable1 = Nothing
        Me.txtMinServicePer.MyLinkLable2 = Nothing
        Me.txtMinServicePer.Name = "txtMinServicePer"
        Me.txtMinServicePer.ReferenceFieldDesc = Nothing
        Me.txtMinServicePer.ReferenceFieldName = Nothing
        Me.txtMinServicePer.ReferenceTableName = Nothing
        Me.txtMinServicePer.Size = New System.Drawing.Size(40, 20)
        Me.txtMinServicePer.TabIndex = 1402
        Me.txtMinServicePer.Text = "0"
        Me.txtMinServicePer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinServicePer.Value = 0R
        '
        'chkbuyerfilereturnlasttwoyear
        '
        Me.chkbuyerfilereturnlasttwoyear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbuyerfilereturnlasttwoyear.Location = New System.Drawing.Point(518, 104)
        Me.chkbuyerfilereturnlasttwoyear.Name = "chkbuyerfilereturnlasttwoyear"
        Me.chkbuyerfilereturnlasttwoyear.Size = New System.Drawing.Size(209, 16)
        Me.chkbuyerfilereturnlasttwoyear.TabIndex = 1376
        Me.chkbuyerfilereturnlasttwoyear.Text = " Buyer File Return In Last Two Years "
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(4, 81)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(126, 16)
        Me.MyLabel1.TabIndex = 47
        Me.MyLabel1.Text = "Single Document Cutoff"
        '
        'txtCuttoffDocument
        '
        Me.txtCuttoffDocument.CalculationExpression = Nothing
        Me.txtCuttoffDocument.FieldCode = Nothing
        Me.txtCuttoffDocument.FieldDesc = Nothing
        Me.txtCuttoffDocument.FieldMaxLength = 0
        Me.txtCuttoffDocument.FieldName = Nothing
        Me.txtCuttoffDocument.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCuttoffDocument.isCalculatedField = False
        Me.txtCuttoffDocument.IsSourceFromTable = False
        Me.txtCuttoffDocument.IsSourceFromValueList = False
        Me.txtCuttoffDocument.IsUnique = False
        Me.txtCuttoffDocument.Location = New System.Drawing.Point(141, 79)
        Me.txtCuttoffDocument.MaxLength = 15
        Me.txtCuttoffDocument.MendatroryField = False
        Me.txtCuttoffDocument.MyLinkLable1 = Me.MyLabel1
        Me.txtCuttoffDocument.MyLinkLable2 = Nothing
        Me.txtCuttoffDocument.Name = "txtCuttoffDocument"
        Me.txtCuttoffDocument.ReferenceFieldDesc = Nothing
        Me.txtCuttoffDocument.ReferenceFieldName = Nothing
        Me.txtCuttoffDocument.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtCuttoffDocument.RootElement.StretchVertically = True
        Me.txtCuttoffDocument.Size = New System.Drawing.Size(220, 20)
        Me.txtCuttoffDocument.TabIndex = 5
        Me.txtCuttoffDocument.Text = "0.00"
        Me.txtCuttoffDocument.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ChkNonPAN
        '
        Me.ChkNonPAN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkNonPAN.Location = New System.Drawing.Point(427, 104)
        Me.ChkNonPAN.Name = "ChkNonPAN"
        Me.ChkNonPAN.Size = New System.Drawing.Size(88, 16)
        Me.ChkNonPAN.TabIndex = 45
        Me.ChkNonPAN.Text = "Non PAN No."
        '
        'fnd_GL_Account
        '
        Me.fnd_GL_Account.CalculationExpression = Nothing
        Me.fnd_GL_Account.FieldCode = Nothing
        Me.fnd_GL_Account.FieldDesc = Nothing
        Me.fnd_GL_Account.FieldMaxLength = 0
        Me.fnd_GL_Account.FieldName = Nothing
        Me.fnd_GL_Account.isCalculatedField = False
        Me.fnd_GL_Account.IsSourceFromTable = False
        Me.fnd_GL_Account.IsSourceFromValueList = False
        Me.fnd_GL_Account.IsUnique = False
        Me.fnd_GL_Account.Location = New System.Drawing.Point(590, 57)
        Me.fnd_GL_Account.MendatroryField = False
        Me.fnd_GL_Account.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_GL_Account.MyLinkLable1 = Me.lbltdssec
        Me.fnd_GL_Account.MyLinkLable2 = Nothing
        Me.fnd_GL_Account.MyReadOnly = False
        Me.fnd_GL_Account.MyShowMasterFormButton = False
        Me.fnd_GL_Account.Name = "fnd_GL_Account"
        Me.fnd_GL_Account.ReferenceFieldDesc = Nothing
        Me.fnd_GL_Account.ReferenceFieldName = Nothing
        Me.fnd_GL_Account.ReferenceTableName = Nothing
        Me.fnd_GL_Account.Size = New System.Drawing.Size(217, 18)
        Me.fnd_GL_Account.TabIndex = 4
        Me.fnd_GL_Account.Value = ""
        '
        'lbltdssec
        '
        Me.lbltdssec.FieldName = Nothing
        Me.lbltdssec.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltdssec.Location = New System.Drawing.Point(4, 35)
        Me.lbltdssec.Name = "lbltdssec"
        Me.lbltdssec.Size = New System.Drawing.Size(70, 16)
        Me.lbltdssec.TabIndex = 42
        Me.lbltdssec.Text = "TDS Section"
        '
        'lblGlAccount
        '
        Me.lblGlAccount.FieldName = Nothing
        Me.lblGlAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGlAccount.Location = New System.Drawing.Point(519, 58)
        Me.lblGlAccount.Name = "lblGlAccount"
        Me.lblGlAccount.Size = New System.Drawing.Size(65, 16)
        Me.lblGlAccount.TabIndex = 7
        Me.lblGlAccount.Text = "GL Account"
        '
        'Fnd_DeductionNew
        '
        Me.Fnd_DeductionNew.FieldName = Nothing
        Me.Fnd_DeductionNew.Location = New System.Drawing.Point(141, 10)
        Me.Fnd_DeductionNew.MendatroryField = False
        Me.Fnd_DeductionNew.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.Fnd_DeductionNew.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Fnd_DeductionNew.MyLinkLable1 = Me.lblnDeduc
        Me.Fnd_DeductionNew.MyLinkLable2 = Nothing
        Me.Fnd_DeductionNew.MyMaxLength = 30
        Me.Fnd_DeductionNew.MyReadOnly = False
        Me.Fnd_DeductionNew.Name = "Fnd_DeductionNew"
        Me.Fnd_DeductionNew.Size = New System.Drawing.Size(202, 21)
        Me.Fnd_DeductionNew.TabIndex = 0
        Me.Fnd_DeductionNew.Value = ""
        '
        'lblnDeduc
        '
        Me.lblnDeduc.FieldName = Nothing
        Me.lblnDeduc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnDeduc.Location = New System.Drawing.Point(4, 12)
        Me.lblnDeduc.Name = "lblnDeduc"
        Me.lblnDeduc.Size = New System.Drawing.Size(107, 16)
        Me.lblnDeduc.TabIndex = 37
        Me.lblnDeduc.Text = "Nature of Deduction"
        '
        'fndTdsNew
        '
        Me.fndTdsNew.CalculationExpression = Nothing
        Me.fndTdsNew.FieldCode = Nothing
        Me.fndTdsNew.FieldDesc = Nothing
        Me.fndTdsNew.FieldMaxLength = 0
        Me.fndTdsNew.FieldName = Nothing
        Me.fndTdsNew.isCalculatedField = False
        Me.fndTdsNew.IsSourceFromTable = False
        Me.fndTdsNew.IsSourceFromValueList = False
        Me.fndTdsNew.IsUnique = False
        Me.fndTdsNew.Location = New System.Drawing.Point(141, 34)
        Me.fndTdsNew.MendatroryField = False
        Me.fndTdsNew.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTdsNew.MyLinkLable1 = Me.lbltdssec
        Me.fndTdsNew.MyLinkLable2 = Nothing
        Me.fndTdsNew.MyReadOnly = False
        Me.fndTdsNew.MyShowMasterFormButton = False
        Me.fndTdsNew.Name = "fndTdsNew"
        Me.fndTdsNew.ReferenceFieldDesc = Nothing
        Me.fndTdsNew.ReferenceFieldName = Nothing
        Me.fndTdsNew.ReferenceTableName = Nothing
        Me.fndTdsNew.Size = New System.Drawing.Size(220, 19)
        Me.fndTdsNew.TabIndex = 1
        Me.fndTdsNew.Value = ""
        '
        'txtremark
        '
        Me.txtremark.CalculationExpression = Nothing
        Me.txtremark.FieldCode = Nothing
        Me.txtremark.FieldDesc = Nothing
        Me.txtremark.FieldMaxLength = 0
        Me.txtremark.FieldName = Nothing
        Me.txtremark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremark.isCalculatedField = False
        Me.txtremark.IsSourceFromTable = False
        Me.txtremark.IsSourceFromValueList = False
        Me.txtremark.IsUnique = False
        Me.txtremark.Location = New System.Drawing.Point(141, 125)
        Me.txtremark.MaxLength = 150
        Me.txtremark.MendatroryField = False
        Me.txtremark.MyLinkLable1 = Me.lblreason
        Me.txtremark.MyLinkLable2 = Nothing
        Me.txtremark.Name = "txtremark"
        Me.txtremark.ReferenceFieldDesc = Nothing
        Me.txtremark.ReferenceFieldName = Nothing
        Me.txtremark.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtremark.RootElement.StretchVertically = True
        Me.txtremark.Size = New System.Drawing.Size(666, 20)
        Me.txtremark.TabIndex = 7
        '
        'lblreason
        '
        Me.lblreason.FieldName = Nothing
        Me.lblreason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreason.Location = New System.Drawing.Point(4, 127)
        Me.lblreason.Name = "lblreason"
        Me.lblreason.Size = New System.Drawing.Size(123, 16)
        Me.lblreason.TabIndex = 44
        Me.lblreason.Text = "Reason for Lower Rate"
        '
        'dgvdeduction
        '
        Me.dgvdeduction.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvdeduction.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvdeduction.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvdeduction.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvdeduction.ForeColor = System.Drawing.Color.Black
        Me.dgvdeduction.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvdeduction.Location = New System.Drawing.Point(7, 155)
        '
        '
        '
        Me.dgvdeduction.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn1.HeaderText = "Line Number"
        GridViewTextBoxColumn1.Name = "Line Number"
        GridViewTextBoxColumn1.Width = 64
        GridViewDecimalColumn1.HeaderText = "From Range"
        GridViewDecimalColumn1.Name = "From Range"
        GridViewDecimalColumn1.Width = 120
        GridViewDecimalColumn2.HeaderText = "To Range"
        GridViewDecimalColumn2.Name = "To Range"
        GridViewDecimalColumn2.Width = 120
        GridViewDecimalColumn3.HeaderText = "Tax %"
        GridViewDecimalColumn3.Name = "Tax %"
        GridViewDecimalColumn3.Width = 120
        GridViewDecimalColumn4.HeaderText = "Surcharge %"
        GridViewDecimalColumn4.Name = "Surcharge %"
        GridViewDecimalColumn4.Width = 120
        GridViewDecimalColumn5.HeaderText = "Edu.Cess %"
        GridViewDecimalColumn5.Name = "Edu.Cess %"
        GridViewDecimalColumn5.Width = 120
        GridViewDecimalColumn6.HeaderText = "Sec.Edu.Cess %"
        GridViewDecimalColumn6.Name = "Sec.Edu.Cess %"
        GridViewDecimalColumn6.Width = 120
        GridViewDecimalColumn7.HeaderText = "TDS Non PAN"
        GridViewDecimalColumn7.Name = "column1"
        Me.dgvdeduction.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewDecimalColumn1, GridViewDecimalColumn2, GridViewDecimalColumn3, GridViewDecimalColumn4, GridViewDecimalColumn5, GridViewDecimalColumn6, GridViewDecimalColumn7})
        Me.dgvdeduction.MasterTemplate.EnableGrouping = False
        Me.dgvdeduction.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.dgvdeduction.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvdeduction.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.dgvdeduction.MyStopExport = False
        Me.dgvdeduction.Name = "dgvdeduction"
        Me.dgvdeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvdeduction.ShowHeaderCellButtons = True
        Me.dgvdeduction.Size = New System.Drawing.Size(800, 225)
        Me.dgvdeduction.TabIndex = 8
        Me.dgvdeduction.TabStop = False
        '
        'lblcum
        '
        Me.lblcum.FieldName = Nothing
        Me.lblcum.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcum.Location = New System.Drawing.Point(4, 58)
        Me.lblcum.Name = "lblcum"
        Me.lblcum.Size = New System.Drawing.Size(96, 16)
        Me.lblcum.TabIndex = 41
        Me.lblcum.Text = "Cumulative Cutoff"
        '
        'lblmodify
        '
        Me.lblmodify.FieldName = Nothing
        Me.lblmodify.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmodify.Location = New System.Drawing.Point(4, 104)
        Me.lblmodify.Name = "lblmodify"
        Me.lblmodify.Size = New System.Drawing.Size(74, 16)
        Me.lblmodify.TabIndex = 40
        Me.lblmodify.Text = "Last Modified"
        '
        'ddldeduction
        '
        Me.ddldeduction.AutoCompleteDisplayMember = Nothing
        Me.ddldeduction.AutoCompleteValueMember = Nothing
        Me.ddldeduction.CalculationExpression = Nothing
        Me.ddldeduction.DropDownAnimationEnabled = True
        Me.ddldeduction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddldeduction.FieldCode = Nothing
        Me.ddldeduction.FieldDesc = Nothing
        Me.ddldeduction.FieldMaxLength = 0
        Me.ddldeduction.FieldName = Nothing
        Me.ddldeduction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddldeduction.isCalculatedField = False
        Me.ddldeduction.IsSourceFromTable = False
        Me.ddldeduction.IsSourceFromValueList = False
        Me.ddldeduction.IsUnique = False
        RadListDataItem1.Text = "Percentage"
        RadListDataItem2.Text = "Amount"
        Me.ddldeduction.Items.Add(RadListDataItem1)
        Me.ddldeduction.Items.Add(RadListDataItem2)
        Me.ddldeduction.Location = New System.Drawing.Point(362, 57)
        Me.ddldeduction.MendatroryField = False
        Me.ddldeduction.MyLinkLable1 = Nothing
        Me.ddldeduction.MyLinkLable2 = Nothing
        Me.ddldeduction.Name = "ddldeduction"
        Me.ddldeduction.ReferenceFieldDesc = Nothing
        Me.ddldeduction.ReferenceFieldName = Nothing
        Me.ddldeduction.ReferenceTableName = Nothing
        Me.ddldeduction.Size = New System.Drawing.Size(148, 18)
        Me.ddldeduction.TabIndex = 3
        Me.ddldeduction.Text = "Percentage"
        CType(Me.ddldeduction.GetChildAt(0), Telerik.WinControls.UI.RadDropDownListElement).DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        '
        'chkinactive
        '
        Me.chkinactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkinactive.Location = New System.Drawing.Point(362, 104)
        Me.chkinactive.Name = "chkinactive"
        Me.chkinactive.Size = New System.Drawing.Size(59, 16)
        Me.chkinactive.TabIndex = 10
        Me.chkinactive.Text = "Inactive"
        '
        'txttdsdes
        '
        Me.txttdsdes.CalculationExpression = Nothing
        Me.txttdsdes.FieldCode = Nothing
        Me.txttdsdes.FieldDesc = Nothing
        Me.txttdsdes.FieldMaxLength = 0
        Me.txttdsdes.FieldName = Nothing
        Me.txttdsdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttdsdes.isCalculatedField = False
        Me.txttdsdes.IsSourceFromTable = False
        Me.txttdsdes.IsSourceFromValueList = False
        Me.txttdsdes.IsUnique = False
        Me.txttdsdes.Location = New System.Drawing.Point(362, 33)
        Me.txttdsdes.MaxLength = 49
        Me.txttdsdes.MendatroryField = False
        Me.txttdsdes.MyLinkLable1 = Nothing
        Me.txttdsdes.MyLinkLable2 = Nothing
        Me.txttdsdes.Name = "txttdsdes"
        Me.txttdsdes.ReadOnly = True
        Me.txttdsdes.ReferenceFieldDesc = Nothing
        Me.txttdsdes.ReferenceFieldName = Nothing
        Me.txttdsdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txttdsdes.RootElement.StretchVertically = True
        Me.txttdsdes.Size = New System.Drawing.Size(445, 20)
        Me.txttdsdes.TabIndex = 4
        Me.txttdsdes.TabStop = False
        '
        'txtcum
        '
        Me.txtcum.CalculationExpression = Nothing
        Me.txtcum.FieldCode = Nothing
        Me.txtcum.FieldDesc = Nothing
        Me.txtcum.FieldMaxLength = 0
        Me.txtcum.FieldName = Nothing
        Me.txtcum.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcum.isCalculatedField = False
        Me.txtcum.IsSourceFromTable = False
        Me.txtcum.IsSourceFromValueList = False
        Me.txtcum.IsUnique = False
        Me.txtcum.Location = New System.Drawing.Point(141, 56)
        Me.txtcum.MaxLength = 15
        Me.txtcum.MendatroryField = False
        Me.txtcum.MyLinkLable1 = Me.lblcum
        Me.txtcum.MyLinkLable2 = Nothing
        Me.txtcum.Name = "txtcum"
        Me.txtcum.ReferenceFieldDesc = Nothing
        Me.txtcum.ReferenceFieldName = Nothing
        Me.txtcum.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtcum.RootElement.StretchVertically = True
        Me.txtcum.Size = New System.Drawing.Size(220, 20)
        Me.txtcum.TabIndex = 2
        Me.txtcum.Text = "0.00"
        Me.txtcum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtmdate
        '
        Me.txtmdate.CalculationExpression = Nothing
        Me.txtmdate.FieldCode = Nothing
        Me.txtmdate.FieldDesc = Nothing
        Me.txtmdate.FieldMaxLength = 0
        Me.txtmdate.FieldName = Nothing
        Me.txtmdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmdate.isCalculatedField = False
        Me.txtmdate.IsSourceFromTable = False
        Me.txtmdate.IsSourceFromValueList = False
        Me.txtmdate.IsUnique = False
        Me.txtmdate.Location = New System.Drawing.Point(141, 102)
        Me.txtmdate.MaxLength = 49
        Me.txtmdate.MendatroryField = False
        Me.txtmdate.MyLinkLable1 = Me.lblmodify
        Me.txtmdate.MyLinkLable2 = Nothing
        Me.txtmdate.Name = "txtmdate"
        Me.txtmdate.ReadOnly = True
        Me.txtmdate.ReferenceFieldDesc = Nothing
        Me.txtmdate.ReferenceFieldName = Nothing
        Me.txtmdate.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtmdate.RootElement.StretchVertically = True
        Me.txtmdate.Size = New System.Drawing.Size(220, 20)
        Me.txtmdate.TabIndex = 6
        Me.txtmdate.TabStop = False
        Me.txtmdate.Text = "  /    /"
        '
        'txtdes
        '
        Me.txtdes.CalculationExpression = Nothing
        Me.txtdes.FieldCode = Nothing
        Me.txtdes.FieldDesc = Nothing
        Me.txtdes.FieldMaxLength = 0
        Me.txtdes.FieldName = Nothing
        Me.txtdes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.isCalculatedField = False
        Me.txtdes.IsSourceFromTable = False
        Me.txtdes.IsSourceFromValueList = False
        Me.txtdes.IsUnique = False
        Me.txtdes.Location = New System.Drawing.Point(362, 10)
        Me.txtdes.MaxLength = 49
        Me.txtdes.MendatroryField = False
        Me.txtdes.MyLinkLable1 = Nothing
        Me.txtdes.MyLinkLable2 = Nothing
        Me.txtdes.Name = "txtdes"
        Me.txtdes.ReferenceFieldDesc = Nothing
        Me.txtdes.ReferenceFieldName = Nothing
        Me.txtdes.ReferenceTableName = Nothing
        '
        '
        '
        Me.txtdes.RootElement.StretchVertically = True
        Me.txtdes.Size = New System.Drawing.Size(445, 20)
        Me.txtdes.TabIndex = 2
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPTDS.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(343, 10)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(18, 21)
        Me.btnnew.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(778, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(74, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(6, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(851, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuImport, Me.menuExport, Me.menuClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'MenuImport
        '
        Me.MenuImport.AccessibleDescription = "MenuImport"
        Me.MenuImport.Name = "MenuImport"
        Me.MenuImport.Text = "Import"
        '
        'menuExport
        '
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        '
        'menuClose
        '
        Me.menuClose.Name = "menuClose"
        Me.menuClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbdesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(851, 443)
        Me.SplitContainer1.SplitterDistance = 409
        Me.SplitContainer1.TabIndex = 2
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(143, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 18)
        Me.btnHistory.TabIndex = 6
        Me.btnHistory.Text = "&History"
        '
        'frmNatureOfDeduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(851, 463)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmNatureOfDeduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Nature Of Deduction"
        CType(Me.gbdesignation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbdesignation.ResumeLayout(False)
        Me.gbdesignation.PerformLayout()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTCSTDSamountgreater50KpreviousYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinServicePer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkbuyerfilereturnlasttwoyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCuttoffDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkNonPAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltdssec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGlAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblnDeduc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblreason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvdeduction.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvdeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmodify, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddldeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkinactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttdsdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtmdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbdesignation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtdes As common.Controls.MyTextBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents ToolTipNDeduction As System.Windows.Forms.ToolTip
    Friend WithEvents txttdsdes As common.Controls.MyTextBox
    Friend WithEvents txtcum As common.Controls.MyTextBox
    Friend WithEvents txtmdate As common.Controls.MyTextBox
    Friend WithEvents ddldeduction As common.Controls.MyComboBox
    Friend WithEvents chkinactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtremark As common.Controls.MyTextBox
    Friend WithEvents dgvdeduction As common.UserControls.MyRadGridView
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MenuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblnDeduc As common.Controls.MyLabel
    Friend WithEvents lbltdssec As common.Controls.MyLabel
    Friend WithEvents lblcum As common.Controls.MyLabel
    Friend WithEvents lblmodify As common.Controls.MyLabel
    Friend WithEvents lblreason As common.Controls.MyLabel
    Friend WithEvents fndTdsNew As common.UserControls.txtFinder
    Friend WithEvents Fnd_DeductionNew As common.UserControls.txtNavigator
    Friend WithEvents lblGlAccount As common.Controls.MyLabel
    Friend WithEvents fnd_GL_Account As common.UserControls.txtFinder
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ChkNonPAN As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCuttoffDocument As common.Controls.MyTextBox
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkTCSTDSamountgreater50KpreviousYear As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkbuyerfilereturnlasttwoyear As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents txtMinServicePer As common.MyNumBox
End Class

