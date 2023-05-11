<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankTransfer
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
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewMultiComboBoxColumn2 As Telerik.WinControls.UI.GridViewMultiComboBoxColumn = New Telerik.WinControls.UI.GridViewMultiComboBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn2 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiIMport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.lbl_transfernumber = New common.Controls.MyLabel()
        Me.lbl_desc = New common.Controls.MyLabel()
        Me.lbl_references = New common.Controls.MyLabel()
        Me.txt_description = New common.Controls.MyTextBox()
        Me.txt_references = New common.Controls.MyTextBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TxtRemittTo = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtwithdrawal = New common.UserControls.txtFinder()
        Me.LblWithdrawal = New common.Controls.MyLabel()
        Me.CmbTransType = New common.Controls.MyComboBox()
        Me.lbltranstype = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.Fnd_Transfernumber = New common.UserControls.txtNavigator()
        Me.btn_reset = New Telerik.WinControls.UI.RadButton()
        Me.MasterTemplate = New common.UserControls.MyRadGridView()
        Me.dtp_transferpostingdate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txttranbnkaccno = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.Txt_toBankCode = New common.UserControls.txtFinder()
        Me.txt_depositamount = New common.Controls.MyTextBox()
        Me.lbl_depositamount = New common.Controls.MyLabel()
        Me.txt_tobankaccount = New common.Controls.MyTextBox()
        Me.lbl_tobankaccount = New common.Controls.MyLabel()
        Me.txt_tobankname = New common.Controls.MyTextBox()
        Me.lbl_bankcode2 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtBankChargesAmt = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkCheckPrint = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtbnkaccnumber = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndPayType = New common.UserControls.txtFinder()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtchkdate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtchkno = New common.Controls.MyTextBox()
        Me.Lblchkno = New common.Controls.MyLabel()
        Me.Txt_frombankCode = New common.UserControls.txtFinder()
        Me.txt_transferamount = New common.Controls.MyTextBox()
        Me.lbl_transferamount = New common.Controls.MyLabel()
        Me.txt_frombankaccount = New common.Controls.MyTextBox()
        Me.lbl_frombankaccount = New common.Controls.MyLabel()
        Me.txt_frombankname = New common.Controls.MyTextBox()
        Me.lbl_bankcode1 = New common.Controls.MyLabel()
        Me.lbl_transferpostingdate = New common.Controls.MyLabel()
        Me.btnReCreateJE = New Telerik.WinControls.UI.RadButton()
        Me.btnBlankForReCreateJE = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btn_post = New Telerik.WinControls.UI.RadButton()
        Me.btn_save = New Telerik.WinControls.UI.RadButton()
        Me.btn_close = New Telerik.WinControls.UI.RadButton()
        Me.btn_delete = New Telerik.WinControls.UI.RadButton()
        Me.lbl_servicecharges = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnOpenBankCashBook = New Telerik.WinControls.UI.RadButton()
        Me.btnVoidCheck = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintCheck = New Telerik.WinControls.UI.RadButton()
        Me.btnReverseAndRecreate = New Telerik.WinControls.UI.RadButton()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.gbAgainstMilkBill = New Telerik.WinControls.UI.RadGroupBox()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_transfernumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_references, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_references, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.TxtRemittTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblWithdrawal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltranstype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtp_transferpostingdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.txttranbnkaccno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_depositamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_depositamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_tobankaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_tobankaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_tobankname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_bankcode2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtBankChargesAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCheckPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtbnkaccnumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtchkdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtchkno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lblchkno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_transferamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_transferamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_frombankaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_frombankaccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_frombankname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_bankcode1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_transferpostingdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReCreateJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBlankForReCreateJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_post, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_servicecharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnOpenBankCashBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnVoidCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseAndRecreate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbAgainstMilkBill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAgainstMilkBill.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(963, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiIMport, Me.rmiExport, Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiIMport
        '
        Me.rmiIMport.AccessibleDescription = "Import"
        Me.rmiIMport.AccessibleName = "Import"
        Me.rmiIMport.Name = "rmiIMport"
        Me.rmiIMport.Text = "Import"
        '
        'rmiExport
        '
        Me.rmiExport.AccessibleDescription = "Export"
        Me.rmiExport.AccessibleName = "Export"
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Exit"
        Me.RadMenuItem2.AccessibleName = "Exit"
        Me.RadMenuItem2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Exit"
        '
        'lbl_transfernumber
        '
        Me.lbl_transfernumber.FieldName = Nothing
        Me.lbl_transfernumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_transfernumber.Location = New System.Drawing.Point(14, 24)
        Me.lbl_transfernumber.Name = "lbl_transfernumber"
        Me.lbl_transfernumber.Size = New System.Drawing.Size(92, 16)
        Me.lbl_transfernumber.TabIndex = 11
        Me.lbl_transfernumber.Text = "Transfer Number"
        '
        'lbl_desc
        '
        Me.lbl_desc.FieldName = Nothing
        Me.lbl_desc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_desc.Location = New System.Drawing.Point(14, 68)
        Me.lbl_desc.Name = "lbl_desc"
        Me.lbl_desc.Size = New System.Drawing.Size(63, 16)
        Me.lbl_desc.TabIndex = 9
        Me.lbl_desc.Text = "Description"
        '
        'lbl_references
        '
        Me.lbl_references.FieldName = Nothing
        Me.lbl_references.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_references.Location = New System.Drawing.Point(14, 90)
        Me.lbl_references.Name = "lbl_references"
        Me.lbl_references.Size = New System.Drawing.Size(64, 16)
        Me.lbl_references.TabIndex = 8
        Me.lbl_references.Text = "References"
        '
        'txt_description
        '
        Me.txt_description.CalculationExpression = Nothing
        Me.txt_description.FieldCode = Nothing
        Me.txt_description.FieldDesc = Nothing
        Me.txt_description.FieldMaxLength = 0
        Me.txt_description.FieldName = Nothing
        Me.txt_description.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_description.isCalculatedField = False
        Me.txt_description.IsSourceFromTable = False
        Me.txt_description.IsSourceFromValueList = False
        Me.txt_description.IsUnique = False
        Me.txt_description.Location = New System.Drawing.Point(135, 68)
        Me.txt_description.MaxLength = 100
        Me.txt_description.MendatroryField = False
        Me.txt_description.MyLinkLable1 = Nothing
        Me.txt_description.MyLinkLable2 = Nothing
        Me.txt_description.Name = "txt_description"
        Me.txt_description.ReferenceFieldDesc = Nothing
        Me.txt_description.ReferenceFieldName = Nothing
        Me.txt_description.ReferenceTableName = Nothing
        Me.txt_description.Size = New System.Drawing.Size(527, 18)
        Me.txt_description.TabIndex = 4
        '
        'txt_references
        '
        Me.txt_references.CalculationExpression = Nothing
        Me.txt_references.FieldCode = Nothing
        Me.txt_references.FieldDesc = Nothing
        Me.txt_references.FieldMaxLength = 0
        Me.txt_references.FieldName = Nothing
        Me.txt_references.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_references.isCalculatedField = False
        Me.txt_references.IsSourceFromTable = False
        Me.txt_references.IsSourceFromValueList = False
        Me.txt_references.IsUnique = False
        Me.txt_references.Location = New System.Drawing.Point(135, 89)
        Me.txt_references.MaxLength = 100
        Me.txt_references.MendatroryField = False
        Me.txt_references.MyLinkLable1 = Nothing
        Me.txt_references.MyLinkLable2 = Nothing
        Me.txt_references.Name = "txt_references"
        Me.txt_references.ReferenceFieldDesc = Nothing
        Me.txt_references.ReferenceFieldName = Nothing
        Me.txt_references.ReferenceTableName = Nothing
        Me.txt_references.Size = New System.Drawing.Size(527, 18)
        Me.txt_references.TabIndex = 5
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gbAgainstMilkBill)
        Me.RadGroupBox1.Controls.Add(Me.TxtRemittTo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtwithdrawal)
        Me.RadGroupBox1.Controls.Add(Me.LblWithdrawal)
        Me.RadGroupBox1.Controls.Add(Me.CmbTransType)
        Me.RadGroupBox1.Controls.Add(Me.lbltranstype)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.Fnd_Transfernumber)
        Me.RadGroupBox1.Controls.Add(Me.btn_reset)
        Me.RadGroupBox1.Controls.Add(Me.MasterTemplate)
        Me.RadGroupBox1.Controls.Add(Me.dtp_transferpostingdate)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.lbl_transfernumber)
        Me.RadGroupBox1.Controls.Add(Me.txt_references)
        Me.RadGroupBox1.Controls.Add(Me.txt_description)
        Me.RadGroupBox1.Controls.Add(Me.lbl_transferpostingdate)
        Me.RadGroupBox1.Controls.Add(Me.lbl_references)
        Me.RadGroupBox1.Controls.Add(Me.lbl_desc)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(963, 369)
        Me.RadGroupBox1.TabIndex = 0
        '
        'TxtRemittTo
        '
        Me.TxtRemittTo.CalculationExpression = Nothing
        Me.TxtRemittTo.FieldCode = Nothing
        Me.TxtRemittTo.FieldDesc = Nothing
        Me.TxtRemittTo.FieldMaxLength = 0
        Me.TxtRemittTo.FieldName = Nothing
        Me.TxtRemittTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemittTo.isCalculatedField = False
        Me.TxtRemittTo.IsSourceFromTable = False
        Me.TxtRemittTo.IsSourceFromValueList = False
        Me.TxtRemittTo.IsUnique = False
        Me.TxtRemittTo.Location = New System.Drawing.Point(135, 109)
        Me.TxtRemittTo.MaxLength = 250
        Me.TxtRemittTo.MendatroryField = False
        Me.TxtRemittTo.MyLinkLable1 = Nothing
        Me.TxtRemittTo.MyLinkLable2 = Nothing
        Me.TxtRemittTo.Name = "TxtRemittTo"
        Me.TxtRemittTo.ReferenceFieldDesc = Nothing
        Me.TxtRemittTo.ReferenceFieldName = Nothing
        Me.TxtRemittTo.ReferenceTableName = Nothing
        Me.TxtRemittTo.Size = New System.Drawing.Size(527, 18)
        Me.TxtRemittTo.TabIndex = 6
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(14, 110)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel3.TabIndex = 18
        Me.MyLabel3.Text = "Remitt To"
        '
        'txtwithdrawal
        '
        Me.txtwithdrawal.AccessibleName = "fnd_frombankcode"
        Me.txtwithdrawal.CalculationExpression = Nothing
        Me.txtwithdrawal.FieldCode = Nothing
        Me.txtwithdrawal.FieldDesc = Nothing
        Me.txtwithdrawal.FieldMaxLength = 0
        Me.txtwithdrawal.FieldName = Nothing
        Me.txtwithdrawal.isCalculatedField = False
        Me.txtwithdrawal.IsSourceFromTable = False
        Me.txtwithdrawal.IsSourceFromValueList = False
        Me.txtwithdrawal.IsUnique = False
        Me.txtwithdrawal.Location = New System.Drawing.Point(469, 133)
        Me.txtwithdrawal.MendatroryField = True
        Me.txtwithdrawal.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtwithdrawal.MyLinkLable1 = Nothing
        Me.txtwithdrawal.MyLinkLable2 = Nothing
        Me.txtwithdrawal.MyReadOnly = False
        Me.txtwithdrawal.MyShowMasterFormButton = False
        Me.txtwithdrawal.Name = "txtwithdrawal"
        Me.txtwithdrawal.ReferenceFieldDesc = Nothing
        Me.txtwithdrawal.ReferenceFieldName = Nothing
        Me.txtwithdrawal.ReferenceTableName = Nothing
        Me.txtwithdrawal.Size = New System.Drawing.Size(192, 19)
        Me.txtwithdrawal.TabIndex = 7
        Me.txtwithdrawal.Value = ""
        '
        'LblWithdrawal
        '
        Me.LblWithdrawal.FieldName = Nothing
        Me.LblWithdrawal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWithdrawal.Location = New System.Drawing.Point(396, 134)
        Me.LblWithdrawal.Name = "LblWithdrawal"
        Me.LblWithdrawal.Size = New System.Drawing.Size(68, 16)
        Me.LblWithdrawal.TabIndex = 16
        Me.LblWithdrawal.Text = "Withdrawals"
        '
        'CmbTransType
        '
        Me.CmbTransType.AutoCompleteDisplayMember = Nothing
        Me.CmbTransType.AutoCompleteValueMember = Nothing
        Me.CmbTransType.CalculationExpression = Nothing
        Me.CmbTransType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbTransType.FieldCode = Nothing
        Me.CmbTransType.FieldDesc = Nothing
        Me.CmbTransType.FieldMaxLength = 0
        Me.CmbTransType.FieldName = Nothing
        Me.CmbTransType.isCalculatedField = False
        Me.CmbTransType.IsSourceFromTable = False
        Me.CmbTransType.IsSourceFromValueList = False
        Me.CmbTransType.IsUnique = False
        RadListDataItem6.Text = "Cash"
        RadListDataItem7.Text = "Bank"
        RadListDataItem8.Text = "Petty Cash"
        RadListDataItem9.Text = "Other"
        RadListDataItem10.Text = "Settlement"
        Me.CmbTransType.Items.Add(RadListDataItem6)
        Me.CmbTransType.Items.Add(RadListDataItem7)
        Me.CmbTransType.Items.Add(RadListDataItem8)
        Me.CmbTransType.Items.Add(RadListDataItem9)
        Me.CmbTransType.Items.Add(RadListDataItem10)
        Me.CmbTransType.Location = New System.Drawing.Point(135, 130)
        Me.CmbTransType.MendatroryField = False
        Me.CmbTransType.MyLinkLable1 = Me.lbltranstype
        Me.CmbTransType.MyLinkLable2 = Nothing
        Me.CmbTransType.Name = "CmbTransType"
        Me.CmbTransType.ReferenceFieldDesc = Nothing
        Me.CmbTransType.ReferenceFieldName = Nothing
        Me.CmbTransType.ReferenceTableName = Nothing
        Me.CmbTransType.Size = New System.Drawing.Size(183, 20)
        Me.CmbTransType.TabIndex = 7
        '
        'lbltranstype
        '
        Me.lbltranstype.FieldName = Nothing
        Me.lbltranstype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltranstype.Location = New System.Drawing.Point(14, 132)
        Me.lbltranstype.Name = "lbltranstype"
        Me.lbltranstype.Size = New System.Drawing.Size(88, 16)
        Me.lbltranstype.TabIndex = 14
        Me.lbltranstype.Text = "Transation Type"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(584, 18)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(74, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 12
        '
        'Fnd_Transfernumber
        '
        Me.Fnd_Transfernumber.FieldName = Nothing
        Me.Fnd_Transfernumber.Location = New System.Drawing.Point(135, 23)
        Me.Fnd_Transfernumber.MendatroryField = False
        Me.Fnd_Transfernumber.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.Fnd_Transfernumber.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Fnd_Transfernumber.MyLinkLable1 = Nothing
        Me.Fnd_Transfernumber.MyLinkLable2 = Nothing
        Me.Fnd_Transfernumber.MyMaxLength = 32767
        Me.Fnd_Transfernumber.MyReadOnly = False
        Me.Fnd_Transfernumber.Name = "Fnd_Transfernumber"
        Me.Fnd_Transfernumber.Size = New System.Drawing.Size(238, 20)
        Me.Fnd_Transfernumber.TabIndex = 0
        Me.Fnd_Transfernumber.Value = ""
        '
        'btn_reset
        '
        Me.btn_reset.Image = Global.ERP.My.Resources.Resources._new
        Me.btn_reset.Location = New System.Drawing.Point(374, 23)
        Me.btn_reset.Name = "btn_reset"
        Me.btn_reset.Size = New System.Drawing.Size(15, 20)
        Me.btn_reset.TabIndex = 1
        '
        'MasterTemplate
        '
        Me.MasterTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MasterTemplate.Cursor = System.Windows.Forms.Cursors.Default
        Me.MasterTemplate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MasterTemplate.ForeColor = System.Drawing.Color.Black
        Me.MasterTemplate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MasterTemplate.Location = New System.Drawing.Point(420, 11)
        '
        'MasterTemplate
        '
        GridViewTextBoxColumn3.HeaderText = "Bank"
        GridViewTextBoxColumn3.Name = "Bank"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.Width = 125
        GridViewMultiComboBoxColumn2.HeaderText = "GL Account"
        GridViewMultiComboBoxColumn2.Name = "GL Account"
        GridViewMultiComboBoxColumn2.Width = 125
        GridViewTextBoxColumn4.HeaderText = "Account Description"
        GridViewTextBoxColumn4.MaxLength = 100
        GridViewTextBoxColumn4.Name = "Account Description"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.Width = 250
        GridViewDecimalColumn2.HeaderText = "Amount"
        GridViewDecimalColumn2.Maximum = New Decimal(New Integer() {-1530494977, 232830, 0, 0})
        GridViewDecimalColumn2.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        GridViewDecimalColumn2.Name = "Amount"
        GridViewDecimalColumn2.Width = 135
        Me.MasterTemplate.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn3, GridViewMultiComboBoxColumn2, GridViewTextBoxColumn4, GridViewDecimalColumn2})
        Me.MasterTemplate.MasterTemplate.EnableGrouping = False
        Me.MasterTemplate.MasterTemplate.EnableSorting = False
        Me.MasterTemplate.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.Name = "MasterTemplate"
        Me.MasterTemplate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MasterTemplate.ShowHeaderCellButtons = True
        Me.MasterTemplate.Size = New System.Drawing.Size(158, 54)
        Me.MasterTemplate.TabIndex = 2
        Me.MasterTemplate.Visible = False
        '
        'dtp_transferpostingdate
        '
        Me.dtp_transferpostingdate.CalculationExpression = Nothing
        Me.dtp_transferpostingdate.CustomFormat = "dd/MM/yyyy"
        Me.dtp_transferpostingdate.FieldCode = Nothing
        Me.dtp_transferpostingdate.FieldDesc = Nothing
        Me.dtp_transferpostingdate.FieldMaxLength = 0
        Me.dtp_transferpostingdate.FieldName = Nothing
        Me.dtp_transferpostingdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_transferpostingdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_transferpostingdate.isCalculatedField = False
        Me.dtp_transferpostingdate.IsSourceFromTable = False
        Me.dtp_transferpostingdate.IsSourceFromValueList = False
        Me.dtp_transferpostingdate.IsUnique = False
        Me.dtp_transferpostingdate.Location = New System.Drawing.Point(135, 47)
        Me.dtp_transferpostingdate.MendatroryField = False
        Me.dtp_transferpostingdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtp_transferpostingdate.MyLinkLable1 = Nothing
        Me.dtp_transferpostingdate.MyLinkLable2 = Nothing
        Me.dtp_transferpostingdate.Name = "dtp_transferpostingdate"
        Me.dtp_transferpostingdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtp_transferpostingdate.ReferenceFieldDesc = Nothing
        Me.dtp_transferpostingdate.ReferenceFieldName = Nothing
        Me.dtp_transferpostingdate.ReferenceTableName = Nothing
        Me.dtp_transferpostingdate.Size = New System.Drawing.Size(144, 18)
        Me.dtp_transferpostingdate.TabIndex = 3
        Me.dtp_transferpostingdate.TabStop = False
        Me.dtp_transferpostingdate.Text = "19/03/2012"
        Me.dtp_transferpostingdate.Value = New Date(2012, 3, 19, 0, 0, 0, 0)
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.txttranbnkaccno)
        Me.RadGroupBox3.Controls.Add(Me.Txt_toBankCode)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.Controls.Add(Me.txt_depositamount)
        Me.RadGroupBox3.Controls.Add(Me.lbl_depositamount)
        Me.RadGroupBox3.Controls.Add(Me.txt_tobankaccount)
        Me.RadGroupBox3.Controls.Add(Me.lbl_tobankaccount)
        Me.RadGroupBox3.Controls.Add(Me.txt_tobankname)
        Me.RadGroupBox3.Controls.Add(Me.lbl_bankcode2)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = "Transfer To"
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 271)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(692, 95)
        Me.RadGroupBox3.TabIndex = 9
        Me.RadGroupBox3.Text = "Transfer To"
        '
        'txttranbnkaccno
        '
        Me.txttranbnkaccno.CalculationExpression = Nothing
        Me.txttranbnkaccno.Enabled = False
        Me.txttranbnkaccno.FieldCode = Nothing
        Me.txttranbnkaccno.FieldDesc = Nothing
        Me.txttranbnkaccno.FieldMaxLength = 0
        Me.txttranbnkaccno.FieldName = Nothing
        Me.txttranbnkaccno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttranbnkaccno.isCalculatedField = False
        Me.txttranbnkaccno.IsSourceFromTable = False
        Me.txttranbnkaccno.IsSourceFromValueList = False
        Me.txttranbnkaccno.IsUnique = False
        Me.txttranbnkaccno.Location = New System.Drawing.Point(433, 44)
        Me.txttranbnkaccno.MaxLength = 30
        Me.txttranbnkaccno.MendatroryField = False
        Me.txttranbnkaccno.MyLinkLable1 = Me.MyLabel2
        Me.txttranbnkaccno.MyLinkLable2 = Nothing
        Me.txttranbnkaccno.Name = "txttranbnkaccno"
        Me.txttranbnkaccno.ReferenceFieldDesc = Nothing
        Me.txttranbnkaccno.ReferenceFieldName = Nothing
        Me.txttranbnkaccno.ReferenceTableName = Nothing
        Me.txttranbnkaccno.Size = New System.Drawing.Size(203, 18)
        Me.txttranbnkaccno.TabIndex = 2
        Me.txttranbnkaccno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(310, 45)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel2.TabIndex = 7
        Me.MyLabel2.Text = "Bank Account Number"
        '
        'Txt_toBankCode
        '
        Me.Txt_toBankCode.AccessibleName = "Txt_toBankCode"
        Me.Txt_toBankCode.CalculationExpression = Nothing
        Me.Txt_toBankCode.FieldCode = Nothing
        Me.Txt_toBankCode.FieldDesc = Nothing
        Me.Txt_toBankCode.FieldMaxLength = 0
        Me.Txt_toBankCode.FieldName = Nothing
        Me.Txt_toBankCode.isCalculatedField = False
        Me.Txt_toBankCode.IsSourceFromTable = False
        Me.Txt_toBankCode.IsSourceFromValueList = False
        Me.Txt_toBankCode.IsUnique = False
        Me.Txt_toBankCode.Location = New System.Drawing.Point(110, 23)
        Me.Txt_toBankCode.MendatroryField = True
        Me.Txt_toBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_toBankCode.MyLinkLable1 = Nothing
        Me.Txt_toBankCode.MyLinkLable2 = Nothing
        Me.Txt_toBankCode.MyReadOnly = False
        Me.Txt_toBankCode.MyShowMasterFormButton = False
        Me.Txt_toBankCode.Name = "Txt_toBankCode"
        Me.Txt_toBankCode.ReferenceFieldDesc = Nothing
        Me.Txt_toBankCode.ReferenceFieldName = Nothing
        Me.Txt_toBankCode.ReferenceTableName = Nothing
        Me.Txt_toBankCode.Size = New System.Drawing.Size(193, 19)
        Me.Txt_toBankCode.TabIndex = 0
        Me.Txt_toBankCode.Value = ""
        '
        'txt_depositamount
        '
        Me.txt_depositamount.CalculationExpression = Nothing
        Me.txt_depositamount.FieldCode = Nothing
        Me.txt_depositamount.FieldDesc = Nothing
        Me.txt_depositamount.FieldMaxLength = 0
        Me.txt_depositamount.FieldName = Nothing
        Me.txt_depositamount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_depositamount.isCalculatedField = False
        Me.txt_depositamount.IsSourceFromTable = False
        Me.txt_depositamount.IsSourceFromValueList = False
        Me.txt_depositamount.IsUnique = False
        Me.txt_depositamount.Location = New System.Drawing.Point(110, 65)
        Me.txt_depositamount.MaxLength = 16
        Me.txt_depositamount.MendatroryField = False
        Me.txt_depositamount.MyLinkLable1 = Nothing
        Me.txt_depositamount.MyLinkLable2 = Nothing
        Me.txt_depositamount.Name = "txt_depositamount"
        Me.txt_depositamount.ReferenceFieldDesc = Nothing
        Me.txt_depositamount.ReferenceFieldName = Nothing
        Me.txt_depositamount.ReferenceTableName = Nothing
        Me.txt_depositamount.Size = New System.Drawing.Size(191, 18)
        Me.txt_depositamount.TabIndex = 3
        Me.txt_depositamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_depositamount
        '
        Me.lbl_depositamount.FieldName = Nothing
        Me.lbl_depositamount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_depositamount.Location = New System.Drawing.Point(13, 67)
        Me.lbl_depositamount.Name = "lbl_depositamount"
        Me.lbl_depositamount.Size = New System.Drawing.Size(87, 16)
        Me.lbl_depositamount.TabIndex = 4
        Me.lbl_depositamount.Text = "Deposit Amount"
        '
        'txt_tobankaccount
        '
        Me.txt_tobankaccount.CalculationExpression = Nothing
        Me.txt_tobankaccount.FieldCode = Nothing
        Me.txt_tobankaccount.FieldDesc = Nothing
        Me.txt_tobankaccount.FieldMaxLength = 0
        Me.txt_tobankaccount.FieldName = Nothing
        Me.txt_tobankaccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tobankaccount.isCalculatedField = False
        Me.txt_tobankaccount.IsSourceFromTable = False
        Me.txt_tobankaccount.IsSourceFromValueList = False
        Me.txt_tobankaccount.IsUnique = False
        Me.txt_tobankaccount.Location = New System.Drawing.Point(110, 45)
        Me.txt_tobankaccount.MaxLength = 30
        Me.txt_tobankaccount.MendatroryField = False
        Me.txt_tobankaccount.MyLinkLable1 = Nothing
        Me.txt_tobankaccount.MyLinkLable2 = Nothing
        Me.txt_tobankaccount.Name = "txt_tobankaccount"
        Me.txt_tobankaccount.ReferenceFieldDesc = Nothing
        Me.txt_tobankaccount.ReferenceFieldName = Nothing
        Me.txt_tobankaccount.ReferenceTableName = Nothing
        Me.txt_tobankaccount.Size = New System.Drawing.Size(191, 18)
        Me.txt_tobankaccount.TabIndex = 1
        Me.txt_tobankaccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_tobankaccount
        '
        Me.lbl_tobankaccount.FieldName = Nothing
        Me.lbl_tobankaccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_tobankaccount.Location = New System.Drawing.Point(13, 45)
        Me.lbl_tobankaccount.Name = "lbl_tobankaccount"
        Me.lbl_tobankaccount.Size = New System.Drawing.Size(76, 16)
        Me.lbl_tobankaccount.TabIndex = 5
        Me.lbl_tobankaccount.Text = "Bank Account"
        '
        'txt_tobankname
        '
        Me.txt_tobankname.CalculationExpression = Nothing
        Me.txt_tobankname.FieldCode = Nothing
        Me.txt_tobankname.FieldDesc = Nothing
        Me.txt_tobankname.FieldMaxLength = 0
        Me.txt_tobankname.FieldName = Nothing
        Me.txt_tobankname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tobankname.isCalculatedField = False
        Me.txt_tobankname.IsSourceFromTable = False
        Me.txt_tobankname.IsSourceFromValueList = False
        Me.txt_tobankname.IsUnique = False
        Me.txt_tobankname.Location = New System.Drawing.Point(310, 23)
        Me.txt_tobankname.MaxLength = 60
        Me.txt_tobankname.MendatroryField = False
        Me.txt_tobankname.MyLinkLable1 = Nothing
        Me.txt_tobankname.MyLinkLable2 = Nothing
        Me.txt_tobankname.Name = "txt_tobankname"
        Me.txt_tobankname.ReferenceFieldDesc = Nothing
        Me.txt_tobankname.ReferenceFieldName = Nothing
        Me.txt_tobankname.ReferenceTableName = Nothing
        Me.txt_tobankname.Size = New System.Drawing.Size(326, 18)
        Me.txt_tobankname.TabIndex = 8
        Me.txt_tobankname.TabStop = False
        '
        'lbl_bankcode2
        '
        Me.lbl_bankcode2.FieldName = Nothing
        Me.lbl_bankcode2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_bankcode2.Location = New System.Drawing.Point(13, 23)
        Me.lbl_bankcode2.Name = "lbl_bankcode2"
        Me.lbl_bankcode2.Size = New System.Drawing.Size(62, 16)
        Me.lbl_bankcode2.TabIndex = 6
        Me.lbl_bankcode2.Text = "Bank Code"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.txtBankChargesAmt)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox2.Controls.Add(Me.chkCheckPrint)
        Me.RadGroupBox2.Controls.Add(Me.txtbnkaccnumber)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.fndPayType)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox2.Controls.Add(Me.txtchkdate)
        Me.RadGroupBox2.Controls.Add(Me.txtchkno)
        Me.RadGroupBox2.Controls.Add(Me.Lblchkno)
        Me.RadGroupBox2.Controls.Add(Me.Txt_frombankCode)
        Me.RadGroupBox2.Controls.Add(Me.txt_transferamount)
        Me.RadGroupBox2.Controls.Add(Me.lbl_transferamount)
        Me.RadGroupBox2.Controls.Add(Me.txt_frombankaccount)
        Me.RadGroupBox2.Controls.Add(Me.lbl_frombankaccount)
        Me.RadGroupBox2.Controls.Add(Me.txt_frombankname)
        Me.RadGroupBox2.Controls.Add(Me.lbl_bankcode1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Transfer From"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 154)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(692, 111)
        Me.RadGroupBox2.TabIndex = 8
        Me.RadGroupBox2.Text = "Transfer From"
        '
        'txtBankChargesAmt
        '
        Me.txtBankChargesAmt.CalculationExpression = Nothing
        Me.txtBankChargesAmt.DecimalPlaces = 2
        Me.txtBankChargesAmt.FieldCode = Nothing
        Me.txtBankChargesAmt.FieldDesc = Nothing
        Me.txtBankChargesAmt.FieldMaxLength = 0
        Me.txtBankChargesAmt.FieldName = Nothing
        Me.txtBankChargesAmt.isCalculatedField = False
        Me.txtBankChargesAmt.IsSourceFromTable = False
        Me.txtBankChargesAmt.IsSourceFromValueList = False
        Me.txtBankChargesAmt.IsUnique = False
        Me.txtBankChargesAmt.Location = New System.Drawing.Point(433, 64)
        Me.txtBankChargesAmt.MendatroryField = False
        Me.txtBankChargesAmt.MyLinkLable1 = Nothing
        Me.txtBankChargesAmt.MyLinkLable2 = Nothing
        Me.txtBankChargesAmt.Name = "txtBankChargesAmt"
        Me.txtBankChargesAmt.ReferenceFieldDesc = Nothing
        Me.txtBankChargesAmt.ReferenceFieldName = Nothing
        Me.txtBankChargesAmt.ReferenceTableName = Nothing
        Me.txtBankChargesAmt.Size = New System.Drawing.Size(203, 20)
        Me.txtBankChargesAmt.TabIndex = 1415
        Me.txtBankChargesAmt.Text = "0"
        Me.txtBankChargesAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBankChargesAmt.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(310, 67)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(78, 16)
        Me.MyLabel4.TabIndex = 16
        Me.MyLabel4.Text = "Bank Charges"
        '
        'chkCheckPrint
        '
        Me.chkCheckPrint.CheckAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCheckPrint.Location = New System.Drawing.Point(606, 86)
        Me.chkCheckPrint.Name = "chkCheckPrint"
        Me.chkCheckPrint.Size = New System.Drawing.Size(77, 18)
        Me.chkCheckPrint.TabIndex = 14
        Me.chkCheckPrint.Text = "Check Print"
        '
        'txtbnkaccnumber
        '
        Me.txtbnkaccnumber.CalculationExpression = Nothing
        Me.txtbnkaccnumber.FieldCode = Nothing
        Me.txtbnkaccnumber.FieldDesc = Nothing
        Me.txtbnkaccnumber.FieldMaxLength = 0
        Me.txtbnkaccnumber.FieldName = Nothing
        Me.txtbnkaccnumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbnkaccnumber.isCalculatedField = False
        Me.txtbnkaccnumber.IsSourceFromTable = False
        Me.txtbnkaccnumber.IsSourceFromValueList = False
        Me.txtbnkaccnumber.IsUnique = False
        Me.txtbnkaccnumber.Location = New System.Drawing.Point(433, 45)
        Me.txtbnkaccnumber.MaxLength = 30
        Me.txtbnkaccnumber.MendatroryField = False
        Me.txtbnkaccnumber.MyLinkLable1 = Me.MyLabel1
        Me.txtbnkaccnumber.MyLinkLable2 = Nothing
        Me.txtbnkaccnumber.Name = "txtbnkaccnumber"
        Me.txtbnkaccnumber.ReferenceFieldDesc = Nothing
        Me.txtbnkaccnumber.ReferenceFieldName = Nothing
        Me.txtbnkaccnumber.ReferenceTableName = Nothing
        Me.txtbnkaccnumber.Size = New System.Drawing.Size(203, 18)
        Me.txtbnkaccnumber.TabIndex = 2
        Me.txtbnkaccnumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(310, 46)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "Bank Account Number"
        '
        'fndPayType
        '
        Me.fndPayType.CalculationExpression = Nothing
        Me.fndPayType.FieldCode = Nothing
        Me.fndPayType.FieldDesc = Nothing
        Me.fndPayType.FieldMaxLength = 0
        Me.fndPayType.FieldName = Nothing
        Me.fndPayType.isCalculatedField = False
        Me.fndPayType.IsSourceFromTable = False
        Me.fndPayType.IsSourceFromValueList = False
        Me.fndPayType.IsUnique = False
        Me.fndPayType.Location = New System.Drawing.Point(111, 86)
        Me.fndPayType.MendatroryField = True
        Me.fndPayType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPayType.MyLinkLable1 = Me.RadLabel3
        Me.fndPayType.MyLinkLable2 = Nothing
        Me.fndPayType.MyReadOnly = False
        Me.fndPayType.MyShowMasterFormButton = False
        Me.fndPayType.Name = "fndPayType"
        Me.fndPayType.ReferenceFieldDesc = Nothing
        Me.fndPayType.ReferenceFieldName = Nothing
        Me.fndPayType.ReferenceTableName = Nothing
        Me.fndPayType.Size = New System.Drawing.Size(193, 19)
        Me.fndPayType.TabIndex = 4
        Me.fndPayType.Value = ""
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(13, 87)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(82, 16)
        Me.RadLabel3.TabIndex = 10
        Me.RadLabel3.Text = "Payment Mode"
        '
        'txtchkdate
        '
        Me.txtchkdate.CustomFormat = "dd/MM/yyyy"
        Me.txtchkdate.Location = New System.Drawing.Point(513, 86)
        Me.txtchkdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtchkdate.Name = "txtchkdate"
        Me.txtchkdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtchkdate.Size = New System.Drawing.Size(87, 20)
        Me.txtchkdate.TabIndex = 6
        Me.txtchkdate.TabStop = False
        Me.txtchkdate.Text = "Thursday, 19 July 2012"
        Me.txtchkdate.Value = New Date(2012, 7, 19, 0, 0, 0, 0)
        '
        'txtchkno
        '
        Me.txtchkno.CalculationExpression = Nothing
        Me.txtchkno.FieldCode = Nothing
        Me.txtchkno.FieldDesc = Nothing
        Me.txtchkno.FieldMaxLength = 0
        Me.txtchkno.FieldName = Nothing
        Me.txtchkno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchkno.isCalculatedField = False
        Me.txtchkno.IsSourceFromTable = False
        Me.txtchkno.IsSourceFromValueList = False
        Me.txtchkno.IsUnique = False
        Me.txtchkno.Location = New System.Drawing.Point(393, 87)
        Me.txtchkno.MaxLength = 16
        Me.txtchkno.MendatroryField = True
        Me.txtchkno.MyLinkLable1 = Nothing
        Me.txtchkno.MyLinkLable2 = Nothing
        Me.txtchkno.Name = "txtchkno"
        Me.txtchkno.ReferenceFieldDesc = Nothing
        Me.txtchkno.ReferenceFieldName = Nothing
        Me.txtchkno.ReferenceTableName = Nothing
        Me.txtchkno.Size = New System.Drawing.Size(114, 18)
        Me.txtchkno.TabIndex = 5
        Me.txtchkno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Lblchkno
        '
        Me.Lblchkno.FieldName = Nothing
        Me.Lblchkno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblchkno.Location = New System.Drawing.Point(320, 87)
        Me.Lblchkno.Name = "Lblchkno"
        Me.Lblchkno.Size = New System.Drawing.Size(67, 16)
        Me.Lblchkno.TabIndex = 7
        Me.Lblchkno.Text = "Cheque No."
        '
        'Txt_frombankCode
        '
        Me.Txt_frombankCode.AccessibleName = "fnd_frombankcode"
        Me.Txt_frombankCode.CalculationExpression = Nothing
        Me.Txt_frombankCode.FieldCode = Nothing
        Me.Txt_frombankCode.FieldDesc = Nothing
        Me.Txt_frombankCode.FieldMaxLength = 0
        Me.Txt_frombankCode.FieldName = Nothing
        Me.Txt_frombankCode.isCalculatedField = False
        Me.Txt_frombankCode.IsSourceFromTable = False
        Me.Txt_frombankCode.IsSourceFromValueList = False
        Me.Txt_frombankCode.IsUnique = False
        Me.Txt_frombankCode.Location = New System.Drawing.Point(111, 23)
        Me.Txt_frombankCode.MendatroryField = True
        Me.Txt_frombankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_frombankCode.MyLinkLable1 = Nothing
        Me.Txt_frombankCode.MyLinkLable2 = Nothing
        Me.Txt_frombankCode.MyReadOnly = False
        Me.Txt_frombankCode.MyShowMasterFormButton = False
        Me.Txt_frombankCode.Name = "Txt_frombankCode"
        Me.Txt_frombankCode.ReferenceFieldDesc = Nothing
        Me.Txt_frombankCode.ReferenceFieldName = Nothing
        Me.Txt_frombankCode.ReferenceTableName = Nothing
        Me.Txt_frombankCode.Size = New System.Drawing.Size(193, 19)
        Me.Txt_frombankCode.TabIndex = 0
        Me.Txt_frombankCode.Value = ""
        '
        'txt_transferamount
        '
        Me.txt_transferamount.CalculationExpression = Nothing
        Me.txt_transferamount.FieldCode = Nothing
        Me.txt_transferamount.FieldDesc = Nothing
        Me.txt_transferamount.FieldMaxLength = 0
        Me.txt_transferamount.FieldName = Nothing
        Me.txt_transferamount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_transferamount.isCalculatedField = False
        Me.txt_transferamount.IsSourceFromTable = False
        Me.txt_transferamount.IsSourceFromValueList = False
        Me.txt_transferamount.IsUnique = False
        Me.txt_transferamount.Location = New System.Drawing.Point(111, 65)
        Me.txt_transferamount.MaxLength = 16
        Me.txt_transferamount.MendatroryField = False
        Me.txt_transferamount.MyLinkLable1 = Nothing
        Me.txt_transferamount.MyLinkLable2 = Nothing
        Me.txt_transferamount.Name = "txt_transferamount"
        Me.txt_transferamount.ReferenceFieldDesc = Nothing
        Me.txt_transferamount.ReferenceFieldName = Nothing
        Me.txt_transferamount.ReferenceTableName = Nothing
        Me.txt_transferamount.Size = New System.Drawing.Size(193, 18)
        Me.txt_transferamount.TabIndex = 3
        Me.txt_transferamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_transferamount
        '
        Me.lbl_transferamount.FieldName = Nothing
        Me.lbl_transferamount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_transferamount.Location = New System.Drawing.Point(13, 67)
        Me.lbl_transferamount.Name = "lbl_transferamount"
        Me.lbl_transferamount.Size = New System.Drawing.Size(91, 16)
        Me.lbl_transferamount.TabIndex = 11
        Me.lbl_transferamount.Text = "Transfer Amount"
        '
        'txt_frombankaccount
        '
        Me.txt_frombankaccount.CalculationExpression = Nothing
        Me.txt_frombankaccount.FieldCode = Nothing
        Me.txt_frombankaccount.FieldDesc = Nothing
        Me.txt_frombankaccount.FieldMaxLength = 0
        Me.txt_frombankaccount.FieldName = Nothing
        Me.txt_frombankaccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_frombankaccount.isCalculatedField = False
        Me.txt_frombankaccount.IsSourceFromTable = False
        Me.txt_frombankaccount.IsSourceFromValueList = False
        Me.txt_frombankaccount.IsUnique = False
        Me.txt_frombankaccount.Location = New System.Drawing.Point(111, 45)
        Me.txt_frombankaccount.MaxLength = 30
        Me.txt_frombankaccount.MendatroryField = False
        Me.txt_frombankaccount.MyLinkLable1 = Nothing
        Me.txt_frombankaccount.MyLinkLable2 = Nothing
        Me.txt_frombankaccount.Name = "txt_frombankaccount"
        Me.txt_frombankaccount.ReferenceFieldDesc = Nothing
        Me.txt_frombankaccount.ReferenceFieldName = Nothing
        Me.txt_frombankaccount.ReferenceTableName = Nothing
        Me.txt_frombankaccount.Size = New System.Drawing.Size(193, 18)
        Me.txt_frombankaccount.TabIndex = 1
        Me.txt_frombankaccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_frombankaccount
        '
        Me.lbl_frombankaccount.FieldName = Nothing
        Me.lbl_frombankaccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_frombankaccount.Location = New System.Drawing.Point(13, 45)
        Me.lbl_frombankaccount.Name = "lbl_frombankaccount"
        Me.lbl_frombankaccount.Size = New System.Drawing.Size(76, 16)
        Me.lbl_frombankaccount.TabIndex = 12
        Me.lbl_frombankaccount.Text = "Bank Account"
        '
        'txt_frombankname
        '
        Me.txt_frombankname.CalculationExpression = Nothing
        Me.txt_frombankname.FieldCode = Nothing
        Me.txt_frombankname.FieldDesc = Nothing
        Me.txt_frombankname.FieldMaxLength = 0
        Me.txt_frombankname.FieldName = Nothing
        Me.txt_frombankname.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_frombankname.isCalculatedField = False
        Me.txt_frombankname.IsSourceFromTable = False
        Me.txt_frombankname.IsSourceFromValueList = False
        Me.txt_frombankname.IsUnique = False
        Me.txt_frombankname.Location = New System.Drawing.Point(310, 24)
        Me.txt_frombankname.MaxLength = 60
        Me.txt_frombankname.MendatroryField = False
        Me.txt_frombankname.MyLinkLable1 = Nothing
        Me.txt_frombankname.MyLinkLable2 = Nothing
        Me.txt_frombankname.Name = "txt_frombankname"
        Me.txt_frombankname.ReferenceFieldDesc = Nothing
        Me.txt_frombankname.ReferenceFieldName = Nothing
        Me.txt_frombankname.ReferenceTableName = Nothing
        Me.txt_frombankname.Size = New System.Drawing.Size(326, 18)
        Me.txt_frombankname.TabIndex = 9
        Me.txt_frombankname.TabStop = False
        '
        'lbl_bankcode1
        '
        Me.lbl_bankcode1.FieldName = Nothing
        Me.lbl_bankcode1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_bankcode1.Location = New System.Drawing.Point(13, 23)
        Me.lbl_bankcode1.Name = "lbl_bankcode1"
        Me.lbl_bankcode1.Size = New System.Drawing.Size(62, 16)
        Me.lbl_bankcode1.TabIndex = 13
        Me.lbl_bankcode1.Text = "Bank Code"
        '
        'lbl_transferpostingdate
        '
        Me.lbl_transferpostingdate.FieldName = Nothing
        Me.lbl_transferpostingdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_transferpostingdate.Location = New System.Drawing.Point(14, 48)
        Me.lbl_transferpostingdate.Name = "lbl_transferpostingdate"
        Me.lbl_transferpostingdate.Size = New System.Drawing.Size(116, 16)
        Me.lbl_transferpostingdate.TabIndex = 10
        Me.lbl_transferpostingdate.Text = "Transfer Posting Date"
        '
        'btnReCreateJE
        '
        Me.btnReCreateJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReCreateJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReCreateJE.Location = New System.Drawing.Point(621, 22)
        Me.btnReCreateJE.Name = "btnReCreateJE"
        Me.btnReCreateJE.Size = New System.Drawing.Size(120, 18)
        Me.btnReCreateJE.TabIndex = 6
        Me.btnReCreateJE.Text = "Re Create JE"
        Me.btnReCreateJE.Visible = False
        '
        'btnBlankForReCreateJE
        '
        Me.btnBlankForReCreateJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBlankForReCreateJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBlankForReCreateJE.Location = New System.Drawing.Point(621, 2)
        Me.btnBlankForReCreateJE.Name = "btnBlankForReCreateJE"
        Me.btnBlankForReCreateJE.Size = New System.Drawing.Size(120, 18)
        Me.btnBlankForReCreateJE.TabIndex = 5
        Me.btnBlankForReCreateJE.Text = "Blank For ReCreateJE"
        Me.btnBlankForReCreateJE.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(234, 12)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btn_post
        '
        Me.btn_post.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_post.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_post.Location = New System.Drawing.Point(160, 12)
        Me.btn_post.Name = "btn_post"
        Me.btn_post.Size = New System.Drawing.Size(68, 18)
        Me.btn_post.TabIndex = 2
        Me.btn_post.Text = "Post"
        '
        'btn_save
        '
        Me.btn_save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_save.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.Location = New System.Drawing.Point(12, 12)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(68, 18)
        Me.btn_save.TabIndex = 0
        Me.btn_save.Text = "Save"
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_close.Location = New System.Drawing.Point(890, 12)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(68, 18)
        Me.btn_close.TabIndex = 7
        Me.btn_close.Text = "Close"
        '
        'btn_delete
        '
        Me.btn_delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_delete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delete.Location = New System.Drawing.Point(86, 12)
        Me.btn_delete.Name = "btn_delete"
        Me.btn_delete.Size = New System.Drawing.Size(68, 18)
        Me.btn_delete.TabIndex = 1
        Me.btn_delete.Text = "Delete"
        '
        'lbl_servicecharges
        '
        Me.lbl_servicecharges.FieldName = Nothing
        Me.lbl_servicecharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_servicecharges.Location = New System.Drawing.Point(782, 121)
        Me.lbl_servicecharges.Name = "lbl_servicecharges"
        Me.lbl_servicecharges.Size = New System.Drawing.Size(90, 16)
        Me.lbl_servicecharges.TabIndex = 13
        Me.lbl_servicecharges.Text = "Service Charges"
        Me.lbl_servicecharges.Visible = False
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOpenBankCashBook)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnVoidCheck)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintCheck)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseAndRecreate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReCreateJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_save)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBlankForReCreateJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_delete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_close)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_post)
        Me.SplitContainer1.Size = New System.Drawing.Size(963, 415)
        Me.SplitContainer1.SplitterDistance = 369
        Me.SplitContainer1.TabIndex = 14
        '
        'btnOpenBankCashBook
        '
        Me.btnOpenBankCashBook.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenBankCashBook.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenBankCashBook.Location = New System.Drawing.Point(747, 12)
        Me.btnOpenBankCashBook.Name = "btnOpenBankCashBook"
        Me.btnOpenBankCashBook.Size = New System.Drawing.Size(137, 18)
        Me.btnOpenBankCashBook.TabIndex = 16
        Me.btnOpenBankCashBook.Text = "Open Bank Cash Book"
        '
        'btnVoidCheck
        '
        Me.btnVoidCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnVoidCheck.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVoidCheck.Location = New System.Drawing.Point(306, 12)
        Me.btnVoidCheck.Name = "btnVoidCheck"
        Me.btnVoidCheck.Size = New System.Drawing.Size(84, 18)
        Me.btnVoidCheck.TabIndex = 12
        Me.btnVoidCheck.Text = "Void Check"
        '
        'btnPrintCheck
        '
        Me.btnPrintCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintCheck.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintCheck.Location = New System.Drawing.Point(396, 12)
        Me.btnPrintCheck.Name = "btnPrintCheck"
        Me.btnPrintCheck.Size = New System.Drawing.Size(90, 18)
        Me.btnPrintCheck.TabIndex = 11
        Me.btnPrintCheck.Text = "Print Check"
        '
        'btnReverseAndRecreate
        '
        Me.btnReverseAndRecreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseAndRecreate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseAndRecreate.Location = New System.Drawing.Point(492, 12)
        Me.btnReverseAndRecreate.Name = "btnReverseAndRecreate"
        Me.btnReverseAndRecreate.Size = New System.Drawing.Size(121, 18)
        Me.btnReverseAndRecreate.TabIndex = 4
        Me.btnReverseAndRecreate.Text = "Reverse And Recreate"
        Me.btnReverseAndRecreate.Visible = False
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(49, 22)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel16
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = ""
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(237, 19)
        Me.txtMCC.TabIndex = 391
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(12, 23)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel16.TabIndex = 392
        Me.MyLabel16.Text = "MCC"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MyLabel5)
        Me.GroupBox2.Controls.Add(Me.dtpToDate)
        Me.GroupBox2.Controls.Add(Me.MyLabel6)
        Me.GroupBox2.Controls.Add(Me.dtpFromDate)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 43)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 31)
        Me.GroupBox2.TabIndex = 393
        Me.GroupBox2.TabStop = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(147, 11)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel5.TabIndex = 266
        Me.MyLabel5.Text = "To"
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(181, 9)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel5
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReadOnly = True
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 265
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "10/06/2011"
        Me.dtpToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(6, 11)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel6.TabIndex = 264
        Me.MyLabel6.Text = "From"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(45, 9)
        Me.dtpFromDate.MendatroryField = False
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.MyLabel6
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpFromDate.TabIndex = 263
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "10/06/2011"
        Me.dtpFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'gbAgainstMilkBill
        '
        Me.gbAgainstMilkBill.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbAgainstMilkBill.Controls.Add(Me.GroupBox2)
        Me.gbAgainstMilkBill.Controls.Add(Me.MyLabel16)
        Me.gbAgainstMilkBill.Controls.Add(Me.txtMCC)
        Me.gbAgainstMilkBill.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbAgainstMilkBill.HeaderText = "Against Milk Bill"
        Me.gbAgainstMilkBill.Location = New System.Drawing.Point(663, 46)
        Me.gbAgainstMilkBill.Name = "gbAgainstMilkBill"
        Me.gbAgainstMilkBill.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbAgainstMilkBill.Size = New System.Drawing.Size(297, 81)
        Me.gbAgainstMilkBill.TabIndex = 21
        Me.gbAgainstMilkBill.Text = "Against Milk Bill"
        '
        'FrmBankTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 435)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.lbl_servicecharges)
        Me.Name = "FrmBankTransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Transfer"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_transfernumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_references, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_references, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.TxtRemittTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblWithdrawal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltranstype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtp_transferpostingdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.txttranbnkaccno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_depositamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_depositamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_tobankaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_tobankaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_tobankname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_bankcode2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtBankChargesAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCheckPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtbnkaccnumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtchkdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtchkno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lblchkno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_transferamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_transferamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_frombankaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_frombankaccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_frombankname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_bankcode1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_transferpostingdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReCreateJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBlankForReCreateJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_post, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_servicecharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnOpenBankCashBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnVoidCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseAndRecreate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbAgainstMilkBill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAgainstMilkBill.ResumeLayout(False)
        Me.gbAgainstMilkBill.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txt_description As common.Controls.MyTextBox
    Friend WithEvents txt_references As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txt_transferamount As common.Controls.MyTextBox
    Friend WithEvents txt_frombankaccount As common.Controls.MyTextBox
    Friend WithEvents txt_frombankname As common.Controls.MyTextBox
    Friend WithEvents grd_serviceCharge As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txt_depositamount As common.Controls.MyTextBox
    Friend WithEvents txt_tobankaccount As common.Controls.MyTextBox
    Friend WithEvents txt_tobankname As common.Controls.MyTextBox
    Friend WithEvents dgv_servicecharge As common.UserControls.MyRadGridView
    Friend WithEvents btn_save As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_close As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_delete As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtp_transferpostingdate As common.Controls.MyDateTimePicker
    Friend WithEvents btn_reset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_post As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents Txt_frombankCode As common.UserControls.txtFinder
    Friend WithEvents Txt_toBankCode As common.UserControls.txtFinder
    Friend WithEvents Fnd_Transfernumber As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbl_transfernumber As common.Controls.MyLabel
    Friend WithEvents lbl_desc As common.Controls.MyLabel
    Friend WithEvents lbl_references As common.Controls.MyLabel
    Friend WithEvents lbl_transferpostingdate As common.Controls.MyLabel
    Friend WithEvents lbl_bankcode1 As common.Controls.MyLabel
    Friend WithEvents lbl_transferamount As common.Controls.MyLabel
    Friend WithEvents lbl_frombankaccount As common.Controls.MyLabel
    Friend WithEvents lbl_servicecharges As common.Controls.MyLabel
    Friend WithEvents lbl_depositamount As common.Controls.MyLabel
    Friend WithEvents lbl_tobankaccount As common.Controls.MyLabel
    Friend WithEvents lbl_bankcode2 As common.Controls.MyLabel
    Friend WithEvents txtchkno As common.Controls.MyTextBox
    Friend WithEvents Lblchkno As common.Controls.MyLabel
    Friend WithEvents txtchkdate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnReCreateJE As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnBlankForReCreateJE As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndPayType As common.UserControls.txtFinder
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rmiIMport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnReverseAndRecreate As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtbnkaccnumber As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txttranbnkaccno As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents CmbTransType As common.Controls.MyComboBox
    Friend WithEvents lbltranstype As common.Controls.MyLabel
    Friend WithEvents txtwithdrawal As common.UserControls.txtFinder
    Friend WithEvents LblWithdrawal As common.Controls.MyLabel
    Friend WithEvents btnVoidCheck As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrintCheck As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkCheckPrint As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtRemittTo As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtBankChargesAmt As common.MyNumBox
    Friend WithEvents btnOpenBankCashBook As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents gbAgainstMilkBill As RadGroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
End Class

