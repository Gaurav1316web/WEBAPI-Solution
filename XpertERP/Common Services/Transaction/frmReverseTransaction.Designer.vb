<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReverseTransaction
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadTextBox1 = New common.Controls.MyTextBox()
        Me.RadTextBox2 = New common.Controls.MyTextBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.UsLock1 = New common.usLock()
        Me.fndbankcode = New common.UserControls.txtFinder()
        Me.fndreversecode = New common.UserControls.txtNavigator()
        Me.btn_reset = New Telerik.WinControls.UI.RadButton()
        Me.txt_PaymentReverseDocument = New common.Controls.MyTextBox()
        Me.txt_ReceiptReversedocument = New common.Controls.MyTextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lbl_checkpaymentno = New common.Controls.MyLabel()
        Me.lbl_CheckReceiptNo = New common.Controls.MyLabel()
        Me.chkIsChequeBounce = New common.Controls.MyCheckBox()
        Me.fndcustomerNo = New common.UserControls.txtFinder()
        Me.txt_checkreceiptno = New common.Controls.MyTextBox()
        Me.fndVendorNo = New common.UserControls.txtFinder()
        Me.fndcheckReceiptNo = New common.UserControls.txtFinder()
        Me.fndCheckPaymentNo = New common.UserControls.txtFinder()
        Me.lbl_Receiptdate = New common.Controls.MyLabel()
        Me.txt_paymentAmount = New common.Controls.MyTextBox()
        Me.txt_receiptAmount = New common.Controls.MyTextBox()
        Me.lbl_Paymentdate = New common.Controls.MyLabel()
        Me.lbl_CustomerNumber = New common.Controls.MyLabel()
        Me.lbl_Vendornumber = New common.Controls.MyLabel()
        Me.lbl_ReceiptAmount = New common.Controls.MyLabel()
        Me.lbl_Paymentamount = New common.Controls.MyLabel()
        Me.txt_checkpaymentno = New common.Controls.MyTextBox()
        Me.txt_CustomerNo = New common.Controls.MyTextBox()
        Me.txt_VendorNo = New common.Controls.MyTextBox()
        Me.dtp_PayRecDate = New common.Controls.MyDateTimePicker()
        Me.dtp_reversaldate = New common.Controls.MyDateTimePicker()
        Me.txt_reason = New common.Controls.MyTextBox()
        Me.lbl_ReversalDate = New common.Controls.MyLabel()
        Me.lbl_ReasonForReversal = New common.Controls.MyLabel()
        Me.lbl_ReverseDocument = New common.Controls.MyLabel()
        Me.ddl_SourceApplication = New common.Controls.MyComboBox()
        Me.lbl_SourceApplication = New common.Controls.MyLabel()
        Me.txt_BankaccountNo = New common.Controls.MyTextBox()
        Me.lbl_Bankcode = New common.Controls.MyLabel()
        Me.txt_Bankcode = New common.Controls.MyTextBox()
        Me.lbl_Reversecode = New common.Controls.MyLabel()
        Me.lbl_BankaccNo = New common.Controls.MyLabel()
        Me.btn_delete = New Telerik.WinControls.UI.RadButton()
        Me.btn_close = New Telerik.WinControls.UI.RadButton()
        Me.RadButton10 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton7 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton8 = New Telerik.WinControls.UI.RadButton()
        Me.btn_save = New Telerik.WinControls.UI.RadButton()
        Me.btn_post = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnReverseTransaction = New Telerik.WinControls.UI.RadButton()
        Me.btnOpenBankCashBook = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadTextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PaymentReverseDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ReceiptReversedocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lbl_checkpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_CheckReceiptNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsChequeBounce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_checkreceiptno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Receiptdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_paymentAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_receiptAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Paymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_CustomerNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Vendornumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_ReceiptAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Paymentamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_checkpaymentno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CustomerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_VendorNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtp_PayRecDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtp_reversaldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_reason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_ReversalDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_ReasonForReversal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_ReverseDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddl_SourceApplication, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_SourceApplication, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_BankaccountNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Bankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Bankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Reversecode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_BankaccNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.btn_close.SuspendLayout()
        CType(Me.RadButton10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_post, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnReverseTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOpenBankCashBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadTextBox1
        '
        Me.RadTextBox1.CalculationExpression = Nothing
        Me.RadTextBox1.FieldCode = Nothing
        Me.RadTextBox1.FieldDesc = Nothing
        Me.RadTextBox1.FieldMaxLength = 0
        Me.RadTextBox1.FieldName = Nothing
        Me.RadTextBox1.isCalculatedField = False
        Me.RadTextBox1.IsSourceFromTable = False
        Me.RadTextBox1.IsSourceFromValueList = False
        Me.RadTextBox1.IsUnique = False
        Me.RadTextBox1.Location = New System.Drawing.Point(306, 27)
        Me.RadTextBox1.MendatroryField = False
        Me.RadTextBox1.MyLinkLable1 = Nothing
        Me.RadTextBox1.MyLinkLable2 = Nothing
        Me.RadTextBox1.Name = "RadTextBox1"
        Me.RadTextBox1.ReferenceFieldDesc = Nothing
        Me.RadTextBox1.ReferenceFieldName = Nothing
        Me.RadTextBox1.ReferenceTableName = Nothing
        Me.RadTextBox1.Size = New System.Drawing.Size(334, 20)
        Me.RadTextBox1.TabIndex = 4
        '
        'RadTextBox2
        '
        Me.RadTextBox2.CalculationExpression = Nothing
        Me.RadTextBox2.FieldCode = Nothing
        Me.RadTextBox2.FieldDesc = Nothing
        Me.RadTextBox2.FieldMaxLength = 0
        Me.RadTextBox2.FieldName = Nothing
        Me.RadTextBox2.isCalculatedField = False
        Me.RadTextBox2.IsSourceFromTable = False
        Me.RadTextBox2.IsSourceFromValueList = False
        Me.RadTextBox2.IsUnique = False
        Me.RadTextBox2.Location = New System.Drawing.Point(306, 49)
        Me.RadTextBox2.MendatroryField = False
        Me.RadTextBox2.MyLinkLable1 = Nothing
        Me.RadTextBox2.MyLinkLable2 = Nothing
        Me.RadTextBox2.Name = "RadTextBox2"
        Me.RadTextBox2.ReferenceFieldDesc = Nothing
        Me.RadTextBox2.ReferenceFieldName = Nothing
        Me.RadTextBox2.ReferenceTableName = Nothing
        Me.RadTextBox2.Size = New System.Drawing.Size(334, 20)
        Me.RadTextBox2.TabIndex = 5
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(751, 20)
        Me.RadMenu1.TabIndex = 5
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Exit.."
        Me.RadMenuItem2.AccessibleName = "Exit.."
        Me.RadMenuItem2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Exit.."
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.fndbankcode)
        Me.RadGroupBox1.Controls.Add(Me.fndreversecode)
        Me.RadGroupBox1.Controls.Add(Me.btn_reset)
        Me.RadGroupBox1.Controls.Add(Me.txt_PaymentReverseDocument)
        Me.RadGroupBox1.Controls.Add(Me.txt_ReceiptReversedocument)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.lbl_ReverseDocument)
        Me.RadGroupBox1.Controls.Add(Me.ddl_SourceApplication)
        Me.RadGroupBox1.Controls.Add(Me.lbl_SourceApplication)
        Me.RadGroupBox1.Controls.Add(Me.txt_BankaccountNo)
        Me.RadGroupBox1.Controls.Add(Me.lbl_Bankcode)
        Me.RadGroupBox1.Controls.Add(Me.txt_Bankcode)
        Me.RadGroupBox1.Controls.Add(Me.lbl_Reversecode)
        Me.RadGroupBox1.Controls.Add(Me.lbl_BankaccNo)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(751, 337)
        Me.RadGroupBox1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(615, 10)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(97, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 28
        '
        'fndbankcode
        '
        Me.fndbankcode.AccessibleName = "fndbankcode"
        Me.fndbankcode.CalculationExpression = Nothing
        Me.fndbankcode.FieldCode = Nothing
        Me.fndbankcode.FieldDesc = Nothing
        Me.fndbankcode.FieldMaxLength = 0
        Me.fndbankcode.FieldName = Nothing
        Me.fndbankcode.isCalculatedField = False
        Me.fndbankcode.IsSourceFromTable = False
        Me.fndbankcode.IsSourceFromValueList = False
        Me.fndbankcode.IsUnique = False
        Me.fndbankcode.Location = New System.Drawing.Point(148, 37)
        Me.fndbankcode.MendatroryField = True
        Me.fndbankcode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndbankcode.MyLinkLable1 = Nothing
        Me.fndbankcode.MyLinkLable2 = Nothing
        Me.fndbankcode.MyReadOnly = False
        Me.fndbankcode.MyShowMasterFormButton = False
        Me.fndbankcode.Name = "fndbankcode"
        Me.fndbankcode.ReferenceFieldDesc = Nothing
        Me.fndbankcode.ReferenceFieldName = Nothing
        Me.fndbankcode.ReferenceTableName = Nothing
        Me.fndbankcode.Size = New System.Drawing.Size(170, 19)
        Me.fndbankcode.TabIndex = 2
        Me.fndbankcode.Value = ""
        '
        'fndreversecode
        '
        Me.fndreversecode.FieldName = Nothing
        Me.fndreversecode.Location = New System.Drawing.Point(148, 10)
        Me.fndreversecode.MendatroryField = True
        Me.fndreversecode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndreversecode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndreversecode.MyLinkLable1 = Nothing
        Me.fndreversecode.MyLinkLable2 = Nothing
        Me.fndreversecode.MyMaxLength = 32767
        Me.fndreversecode.MyReadOnly = False
        Me.fndreversecode.Name = "fndreversecode"
        Me.fndreversecode.Size = New System.Drawing.Size(269, 21)
        Me.fndreversecode.TabIndex = 0
        Me.fndreversecode.Value = ""
        '
        'btn_reset
        '
        Me.btn_reset.Image = Global.ERP.My.Resources.Resources._new
        Me.btn_reset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btn_reset.Location = New System.Drawing.Point(418, 10)
        Me.btn_reset.Name = "btn_reset"
        Me.btn_reset.Size = New System.Drawing.Size(19, 21)
        Me.btn_reset.TabIndex = 1
        '
        'txt_PaymentReverseDocument
        '
        Me.txt_PaymentReverseDocument.CalculationExpression = Nothing
        Me.txt_PaymentReverseDocument.FieldCode = Nothing
        Me.txt_PaymentReverseDocument.FieldDesc = Nothing
        Me.txt_PaymentReverseDocument.FieldMaxLength = 0
        Me.txt_PaymentReverseDocument.FieldName = Nothing
        Me.txt_PaymentReverseDocument.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PaymentReverseDocument.isCalculatedField = False
        Me.txt_PaymentReverseDocument.IsSourceFromTable = False
        Me.txt_PaymentReverseDocument.IsSourceFromValueList = False
        Me.txt_PaymentReverseDocument.IsUnique = False
        Me.txt_PaymentReverseDocument.Location = New System.Drawing.Point(148, 105)
        Me.txt_PaymentReverseDocument.MendatroryField = False
        Me.txt_PaymentReverseDocument.MyLinkLable1 = Nothing
        Me.txt_PaymentReverseDocument.MyLinkLable2 = Nothing
        Me.txt_PaymentReverseDocument.Name = "txt_PaymentReverseDocument"
        Me.txt_PaymentReverseDocument.ReadOnly = True
        Me.txt_PaymentReverseDocument.ReferenceFieldDesc = Nothing
        Me.txt_PaymentReverseDocument.ReferenceFieldName = Nothing
        Me.txt_PaymentReverseDocument.ReferenceTableName = Nothing
        Me.txt_PaymentReverseDocument.Size = New System.Drawing.Size(170, 18)
        Me.txt_PaymentReverseDocument.TabIndex = 5
        Me.txt_PaymentReverseDocument.Text = "Payments"
        '
        'txt_ReceiptReversedocument
        '
        Me.txt_ReceiptReversedocument.CalculationExpression = Nothing
        Me.txt_ReceiptReversedocument.FieldCode = Nothing
        Me.txt_ReceiptReversedocument.FieldDesc = Nothing
        Me.txt_ReceiptReversedocument.FieldMaxLength = 0
        Me.txt_ReceiptReversedocument.FieldName = Nothing
        Me.txt_ReceiptReversedocument.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ReceiptReversedocument.isCalculatedField = False
        Me.txt_ReceiptReversedocument.IsSourceFromTable = False
        Me.txt_ReceiptReversedocument.IsSourceFromValueList = False
        Me.txt_ReceiptReversedocument.IsUnique = False
        Me.txt_ReceiptReversedocument.Location = New System.Drawing.Point(148, 105)
        Me.txt_ReceiptReversedocument.MendatroryField = False
        Me.txt_ReceiptReversedocument.MyLinkLable1 = Nothing
        Me.txt_ReceiptReversedocument.MyLinkLable2 = Nothing
        Me.txt_ReceiptReversedocument.Name = "txt_ReceiptReversedocument"
        Me.txt_ReceiptReversedocument.ReferenceFieldDesc = Nothing
        Me.txt_ReceiptReversedocument.ReferenceFieldName = Nothing
        Me.txt_ReceiptReversedocument.ReferenceTableName = Nothing
        Me.txt_ReceiptReversedocument.Size = New System.Drawing.Size(170, 18)
        Me.txt_ReceiptReversedocument.TabIndex = 5
        Me.txt_ReceiptReversedocument.Text = "Receipts"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.lbl_checkpaymentno)
        Me.RadGroupBox2.Controls.Add(Me.lbl_CheckReceiptNo)
        Me.RadGroupBox2.Controls.Add(Me.chkIsChequeBounce)
        Me.RadGroupBox2.Controls.Add(Me.fndcustomerNo)
        Me.RadGroupBox2.Controls.Add(Me.txt_checkreceiptno)
        Me.RadGroupBox2.Controls.Add(Me.fndVendorNo)
        Me.RadGroupBox2.Controls.Add(Me.fndcheckReceiptNo)
        Me.RadGroupBox2.Controls.Add(Me.fndCheckPaymentNo)
        Me.RadGroupBox2.Controls.Add(Me.lbl_Receiptdate)
        Me.RadGroupBox2.Controls.Add(Me.txt_paymentAmount)
        Me.RadGroupBox2.Controls.Add(Me.txt_receiptAmount)
        Me.RadGroupBox2.Controls.Add(Me.lbl_Paymentdate)
        Me.RadGroupBox2.Controls.Add(Me.lbl_CustomerNumber)
        Me.RadGroupBox2.Controls.Add(Me.lbl_Vendornumber)
        Me.RadGroupBox2.Controls.Add(Me.lbl_ReceiptAmount)
        Me.RadGroupBox2.Controls.Add(Me.lbl_Paymentamount)
        Me.RadGroupBox2.Controls.Add(Me.txt_checkpaymentno)
        Me.RadGroupBox2.Controls.Add(Me.txt_CustomerNo)
        Me.RadGroupBox2.Controls.Add(Me.txt_VendorNo)
        Me.RadGroupBox2.Controls.Add(Me.dtp_PayRecDate)
        Me.RadGroupBox2.Controls.Add(Me.dtp_reversaldate)
        Me.RadGroupBox2.Controls.Add(Me.txt_reason)
        Me.RadGroupBox2.Controls.Add(Me.lbl_ReversalDate)
        Me.RadGroupBox2.Controls.Add(Me.lbl_ReasonForReversal)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Reverse Single Transaction"
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 130)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(712, 194)
        Me.RadGroupBox2.TabIndex = 6
        Me.RadGroupBox2.Text = "Reverse Single Transaction"
        '
        'lbl_checkpaymentno
        '
        Me.lbl_checkpaymentno.FieldName = Nothing
        Me.lbl_checkpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_checkpaymentno.Location = New System.Drawing.Point(17, 113)
        Me.lbl_checkpaymentno.Name = "lbl_checkpaymentno"
        Me.lbl_checkpaymentno.Size = New System.Drawing.Size(107, 16)
        Me.lbl_checkpaymentno.TabIndex = 0
        Me.lbl_checkpaymentno.Text = "Check/Payment No."
        '
        'lbl_CheckReceiptNo
        '
        Me.lbl_CheckReceiptNo.FieldName = Nothing
        Me.lbl_CheckReceiptNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CheckReceiptNo.Location = New System.Drawing.Point(15, 113)
        Me.lbl_CheckReceiptNo.Name = "lbl_CheckReceiptNo"
        Me.lbl_CheckReceiptNo.Size = New System.Drawing.Size(97, 16)
        Me.lbl_CheckReceiptNo.TabIndex = 10
        Me.lbl_CheckReceiptNo.Text = "Check/Receipt No"
        '
        'chkIsChequeBounce
        '
        Me.chkIsChequeBounce.Location = New System.Drawing.Point(343, 60)
        Me.chkIsChequeBounce.MyLinkLable1 = Nothing
        Me.chkIsChequeBounce.MyLinkLable2 = Nothing
        Me.chkIsChequeBounce.Name = "chkIsChequeBounce"
        Me.chkIsChequeBounce.Size = New System.Drawing.Size(110, 18)
        Me.chkIsChequeBounce.TabIndex = 2
        Me.chkIsChequeBounce.Tag1 = Nothing
        Me.chkIsChequeBounce.Text = "Is Cheque Bounce"
        '
        'fndcustomerNo
        '
        Me.fndcustomerNo.AccessibleName = "fndcustomerNo"
        Me.fndcustomerNo.CalculationExpression = Nothing
        Me.fndcustomerNo.FieldCode = Nothing
        Me.fndcustomerNo.FieldDesc = Nothing
        Me.fndcustomerNo.FieldMaxLength = 0
        Me.fndcustomerNo.FieldName = Nothing
        Me.fndcustomerNo.isCalculatedField = False
        Me.fndcustomerNo.IsSourceFromTable = False
        Me.fndcustomerNo.IsSourceFromValueList = False
        Me.fndcustomerNo.IsUnique = False
        Me.fndcustomerNo.Location = New System.Drawing.Point(135, 84)
        Me.fndcustomerNo.MendatroryField = True
        Me.fndcustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcustomerNo.MyLinkLable1 = Nothing
        Me.fndcustomerNo.MyLinkLable2 = Nothing
        Me.fndcustomerNo.MyReadOnly = False
        Me.fndcustomerNo.MyShowMasterFormButton = False
        Me.fndcustomerNo.Name = "fndcustomerNo"
        Me.fndcustomerNo.ReferenceFieldDesc = Nothing
        Me.fndcustomerNo.ReferenceFieldName = Nothing
        Me.fndcustomerNo.ReferenceTableName = Nothing
        Me.fndcustomerNo.Size = New System.Drawing.Size(170, 19)
        Me.fndcustomerNo.TabIndex = 3
        Me.fndcustomerNo.Value = ""
        '
        'txt_checkreceiptno
        '
        Me.txt_checkreceiptno.CalculationExpression = Nothing
        Me.txt_checkreceiptno.FieldCode = Nothing
        Me.txt_checkreceiptno.FieldDesc = Nothing
        Me.txt_checkreceiptno.FieldMaxLength = 0
        Me.txt_checkreceiptno.FieldName = Nothing
        Me.txt_checkreceiptno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_checkreceiptno.isCalculatedField = False
        Me.txt_checkreceiptno.IsSourceFromTable = False
        Me.txt_checkreceiptno.IsSourceFromValueList = False
        Me.txt_checkreceiptno.IsUnique = False
        Me.txt_checkreceiptno.Location = New System.Drawing.Point(322, 113)
        Me.txt_checkreceiptno.MaxLength = 30
        Me.txt_checkreceiptno.MendatroryField = False
        Me.txt_checkreceiptno.MyLinkLable1 = Nothing
        Me.txt_checkreceiptno.MyLinkLable2 = Nothing
        Me.txt_checkreceiptno.Name = "txt_checkreceiptno"
        Me.txt_checkreceiptno.ReadOnly = True
        Me.txt_checkreceiptno.ReferenceFieldDesc = Nothing
        Me.txt_checkreceiptno.ReferenceFieldName = Nothing
        Me.txt_checkreceiptno.ReferenceTableName = Nothing
        Me.txt_checkreceiptno.Size = New System.Drawing.Size(377, 18)
        Me.txt_checkreceiptno.TabIndex = 28
        Me.txt_checkreceiptno.TabStop = False
        '
        'fndVendorNo
        '
        Me.fndVendorNo.AccessibleName = "fndVendorNo"
        Me.fndVendorNo.CalculationExpression = Nothing
        Me.fndVendorNo.FieldCode = Nothing
        Me.fndVendorNo.FieldDesc = Nothing
        Me.fndVendorNo.FieldMaxLength = 0
        Me.fndVendorNo.FieldName = Nothing
        Me.fndVendorNo.isCalculatedField = False
        Me.fndVendorNo.IsSourceFromTable = False
        Me.fndVendorNo.IsSourceFromValueList = False
        Me.fndVendorNo.IsUnique = False
        Me.fndVendorNo.Location = New System.Drawing.Point(135, 83)
        Me.fndVendorNo.MendatroryField = False
        Me.fndVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendorNo.MyLinkLable1 = Nothing
        Me.fndVendorNo.MyLinkLable2 = Nothing
        Me.fndVendorNo.MyReadOnly = False
        Me.fndVendorNo.MyShowMasterFormButton = False
        Me.fndVendorNo.Name = "fndVendorNo"
        Me.fndVendorNo.ReferenceFieldDesc = Nothing
        Me.fndVendorNo.ReferenceFieldName = Nothing
        Me.fndVendorNo.ReferenceTableName = Nothing
        Me.fndVendorNo.Size = New System.Drawing.Size(170, 19)
        Me.fndVendorNo.TabIndex = 2
        Me.fndVendorNo.Value = ""
        '
        'fndcheckReceiptNo
        '
        Me.fndcheckReceiptNo.AccessibleName = "fndcheckReceiptNo"
        Me.fndcheckReceiptNo.CalculationExpression = Nothing
        Me.fndcheckReceiptNo.FieldCode = Nothing
        Me.fndcheckReceiptNo.FieldDesc = Nothing
        Me.fndcheckReceiptNo.FieldMaxLength = 0
        Me.fndcheckReceiptNo.FieldName = Nothing
        Me.fndcheckReceiptNo.isCalculatedField = False
        Me.fndcheckReceiptNo.IsSourceFromTable = False
        Me.fndcheckReceiptNo.IsSourceFromValueList = False
        Me.fndcheckReceiptNo.IsUnique = False
        Me.fndcheckReceiptNo.Location = New System.Drawing.Point(136, 111)
        Me.fndcheckReceiptNo.MendatroryField = True
        Me.fndcheckReceiptNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcheckReceiptNo.MyLinkLable1 = Nothing
        Me.fndcheckReceiptNo.MyLinkLable2 = Nothing
        Me.fndcheckReceiptNo.MyReadOnly = False
        Me.fndcheckReceiptNo.MyShowMasterFormButton = False
        Me.fndcheckReceiptNo.Name = "fndcheckReceiptNo"
        Me.fndcheckReceiptNo.ReferenceFieldDesc = Nothing
        Me.fndcheckReceiptNo.ReferenceFieldName = Nothing
        Me.fndcheckReceiptNo.ReferenceTableName = Nothing
        Me.fndcheckReceiptNo.Size = New System.Drawing.Size(170, 19)
        Me.fndcheckReceiptNo.TabIndex = 4
        Me.fndcheckReceiptNo.Value = ""
        '
        'fndCheckPaymentNo
        '
        Me.fndCheckPaymentNo.AccessibleName = "fndCheckPaymentNo"
        Me.fndCheckPaymentNo.CalculationExpression = Nothing
        Me.fndCheckPaymentNo.FieldCode = Nothing
        Me.fndCheckPaymentNo.FieldDesc = Nothing
        Me.fndCheckPaymentNo.FieldMaxLength = 0
        Me.fndCheckPaymentNo.FieldName = Nothing
        Me.fndCheckPaymentNo.isCalculatedField = False
        Me.fndCheckPaymentNo.IsSourceFromTable = False
        Me.fndCheckPaymentNo.IsSourceFromValueList = False
        Me.fndCheckPaymentNo.IsUnique = False
        Me.fndCheckPaymentNo.Location = New System.Drawing.Point(135, 110)
        Me.fndCheckPaymentNo.MendatroryField = False
        Me.fndCheckPaymentNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCheckPaymentNo.MyLinkLable1 = Nothing
        Me.fndCheckPaymentNo.MyLinkLable2 = Nothing
        Me.fndCheckPaymentNo.MyReadOnly = False
        Me.fndCheckPaymentNo.MyShowMasterFormButton = False
        Me.fndCheckPaymentNo.Name = "fndCheckPaymentNo"
        Me.fndCheckPaymentNo.ReferenceFieldDesc = Nothing
        Me.fndCheckPaymentNo.ReferenceFieldName = Nothing
        Me.fndCheckPaymentNo.ReferenceTableName = Nothing
        Me.fndCheckPaymentNo.Size = New System.Drawing.Size(170, 19)
        Me.fndCheckPaymentNo.TabIndex = 24
        Me.fndCheckPaymentNo.Value = ""
        '
        'lbl_Receiptdate
        '
        Me.lbl_Receiptdate.FieldName = Nothing
        Me.lbl_Receiptdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Receiptdate.Location = New System.Drawing.Point(15, 160)
        Me.lbl_Receiptdate.Name = "lbl_Receiptdate"
        Me.lbl_Receiptdate.Size = New System.Drawing.Size(72, 16)
        Me.lbl_Receiptdate.TabIndex = 7
        Me.lbl_Receiptdate.Text = "Receipt Date"
        '
        'txt_paymentAmount
        '
        Me.txt_paymentAmount.CalculationExpression = Nothing
        Me.txt_paymentAmount.FieldCode = Nothing
        Me.txt_paymentAmount.FieldDesc = Nothing
        Me.txt_paymentAmount.FieldMaxLength = 0
        Me.txt_paymentAmount.FieldName = Nothing
        Me.txt_paymentAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_paymentAmount.isCalculatedField = False
        Me.txt_paymentAmount.IsSourceFromTable = False
        Me.txt_paymentAmount.IsSourceFromValueList = False
        Me.txt_paymentAmount.IsUnique = False
        Me.txt_paymentAmount.Location = New System.Drawing.Point(135, 136)
        Me.txt_paymentAmount.MaxLength = 16
        Me.txt_paymentAmount.MendatroryField = False
        Me.txt_paymentAmount.MyLinkLable1 = Nothing
        Me.txt_paymentAmount.MyLinkLable2 = Nothing
        Me.txt_paymentAmount.Name = "txt_paymentAmount"
        Me.txt_paymentAmount.ReferenceFieldDesc = Nothing
        Me.txt_paymentAmount.ReferenceFieldName = Nothing
        Me.txt_paymentAmount.ReferenceTableName = Nothing
        Me.txt_paymentAmount.Size = New System.Drawing.Size(170, 18)
        Me.txt_paymentAmount.TabIndex = 5
        Me.txt_paymentAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_receiptAmount
        '
        Me.txt_receiptAmount.CalculationExpression = Nothing
        Me.txt_receiptAmount.FieldCode = Nothing
        Me.txt_receiptAmount.FieldDesc = Nothing
        Me.txt_receiptAmount.FieldMaxLength = 0
        Me.txt_receiptAmount.FieldName = Nothing
        Me.txt_receiptAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_receiptAmount.isCalculatedField = False
        Me.txt_receiptAmount.IsSourceFromTable = False
        Me.txt_receiptAmount.IsSourceFromValueList = False
        Me.txt_receiptAmount.IsUnique = False
        Me.txt_receiptAmount.Location = New System.Drawing.Point(135, 136)
        Me.txt_receiptAmount.MaxLength = 16
        Me.txt_receiptAmount.MendatroryField = False
        Me.txt_receiptAmount.MyLinkLable1 = Nothing
        Me.txt_receiptAmount.MyLinkLable2 = Nothing
        Me.txt_receiptAmount.Name = "txt_receiptAmount"
        Me.txt_receiptAmount.ReferenceFieldDesc = Nothing
        Me.txt_receiptAmount.ReferenceFieldName = Nothing
        Me.txt_receiptAmount.ReferenceTableName = Nothing
        Me.txt_receiptAmount.Size = New System.Drawing.Size(170, 18)
        Me.txt_receiptAmount.TabIndex = 15
        Me.txt_receiptAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Paymentdate
        '
        Me.lbl_Paymentdate.FieldName = Nothing
        Me.lbl_Paymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Paymentdate.Location = New System.Drawing.Point(14, 162)
        Me.lbl_Paymentdate.Name = "lbl_Paymentdate"
        Me.lbl_Paymentdate.Size = New System.Drawing.Size(78, 16)
        Me.lbl_Paymentdate.TabIndex = 26
        Me.lbl_Paymentdate.Text = "Payment Date"
        '
        'lbl_CustomerNumber
        '
        Me.lbl_CustomerNumber.FieldName = Nothing
        Me.lbl_CustomerNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CustomerNumber.Location = New System.Drawing.Point(12, 88)
        Me.lbl_CustomerNumber.Name = "lbl_CustomerNumber"
        Me.lbl_CustomerNumber.Size = New System.Drawing.Size(99, 16)
        Me.lbl_CustomerNumber.TabIndex = 9
        Me.lbl_CustomerNumber.Text = "Customer Number"
        '
        'lbl_Vendornumber
        '
        Me.lbl_Vendornumber.FieldName = Nothing
        Me.lbl_Vendornumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Vendornumber.Location = New System.Drawing.Point(13, 86)
        Me.lbl_Vendornumber.Name = "lbl_Vendornumber"
        Me.lbl_Vendornumber.Size = New System.Drawing.Size(86, 16)
        Me.lbl_Vendornumber.TabIndex = 21
        Me.lbl_Vendornumber.Text = "Vendor Number"
        '
        'lbl_ReceiptAmount
        '
        Me.lbl_ReceiptAmount.FieldName = Nothing
        Me.lbl_ReceiptAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ReceiptAmount.Location = New System.Drawing.Point(13, 138)
        Me.lbl_ReceiptAmount.Name = "lbl_ReceiptAmount"
        Me.lbl_ReceiptAmount.Size = New System.Drawing.Size(87, 16)
        Me.lbl_ReceiptAmount.TabIndex = 8
        Me.lbl_ReceiptAmount.Text = "Receipt Amount"
        '
        'lbl_Paymentamount
        '
        Me.lbl_Paymentamount.FieldName = Nothing
        Me.lbl_Paymentamount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Paymentamount.Location = New System.Drawing.Point(14, 140)
        Me.lbl_Paymentamount.Name = "lbl_Paymentamount"
        Me.lbl_Paymentamount.Size = New System.Drawing.Size(93, 16)
        Me.lbl_Paymentamount.TabIndex = 8
        Me.lbl_Paymentamount.Text = "Payment Amount"
        '
        'txt_checkpaymentno
        '
        Me.txt_checkpaymentno.CalculationExpression = Nothing
        Me.txt_checkpaymentno.FieldCode = Nothing
        Me.txt_checkpaymentno.FieldDesc = Nothing
        Me.txt_checkpaymentno.FieldMaxLength = 0
        Me.txt_checkpaymentno.FieldName = Nothing
        Me.txt_checkpaymentno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_checkpaymentno.isCalculatedField = False
        Me.txt_checkpaymentno.IsSourceFromTable = False
        Me.txt_checkpaymentno.IsSourceFromValueList = False
        Me.txt_checkpaymentno.IsUnique = False
        Me.txt_checkpaymentno.Location = New System.Drawing.Point(322, 113)
        Me.txt_checkpaymentno.MaxLength = 30
        Me.txt_checkpaymentno.MendatroryField = False
        Me.txt_checkpaymentno.MyLinkLable1 = Nothing
        Me.txt_checkpaymentno.MyLinkLable2 = Nothing
        Me.txt_checkpaymentno.Name = "txt_checkpaymentno"
        Me.txt_checkpaymentno.ReferenceFieldDesc = Nothing
        Me.txt_checkpaymentno.ReferenceFieldName = Nothing
        Me.txt_checkpaymentno.ReferenceTableName = Nothing
        Me.txt_checkpaymentno.Size = New System.Drawing.Size(377, 18)
        Me.txt_checkpaymentno.TabIndex = 27
        '
        'txt_CustomerNo
        '
        Me.txt_CustomerNo.CalculationExpression = Nothing
        Me.txt_CustomerNo.FieldCode = Nothing
        Me.txt_CustomerNo.FieldDesc = Nothing
        Me.txt_CustomerNo.FieldMaxLength = 0
        Me.txt_CustomerNo.FieldName = Nothing
        Me.txt_CustomerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_CustomerNo.isCalculatedField = False
        Me.txt_CustomerNo.IsSourceFromTable = False
        Me.txt_CustomerNo.IsSourceFromValueList = False
        Me.txt_CustomerNo.IsUnique = False
        Me.txt_CustomerNo.Location = New System.Drawing.Point(322, 88)
        Me.txt_CustomerNo.MaxLength = 30
        Me.txt_CustomerNo.MendatroryField = False
        Me.txt_CustomerNo.MyLinkLable1 = Nothing
        Me.txt_CustomerNo.MyLinkLable2 = Nothing
        Me.txt_CustomerNo.Name = "txt_CustomerNo"
        Me.txt_CustomerNo.ReadOnly = True
        Me.txt_CustomerNo.ReferenceFieldDesc = Nothing
        Me.txt_CustomerNo.ReferenceFieldName = Nothing
        Me.txt_CustomerNo.ReferenceTableName = Nothing
        Me.txt_CustomerNo.Size = New System.Drawing.Size(377, 18)
        Me.txt_CustomerNo.TabIndex = 12
        Me.txt_CustomerNo.TabStop = False
        '
        'txt_VendorNo
        '
        Me.txt_VendorNo.CalculationExpression = Nothing
        Me.txt_VendorNo.FieldCode = Nothing
        Me.txt_VendorNo.FieldDesc = Nothing
        Me.txt_VendorNo.FieldMaxLength = 0
        Me.txt_VendorNo.FieldName = Nothing
        Me.txt_VendorNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_VendorNo.isCalculatedField = False
        Me.txt_VendorNo.IsSourceFromTable = False
        Me.txt_VendorNo.IsSourceFromValueList = False
        Me.txt_VendorNo.IsUnique = False
        Me.txt_VendorNo.Location = New System.Drawing.Point(322, 88)
        Me.txt_VendorNo.MaxLength = 30
        Me.txt_VendorNo.MendatroryField = False
        Me.txt_VendorNo.MyLinkLable1 = Nothing
        Me.txt_VendorNo.MyLinkLable2 = Nothing
        Me.txt_VendorNo.Name = "txt_VendorNo"
        Me.txt_VendorNo.ReferenceFieldDesc = Nothing
        Me.txt_VendorNo.ReferenceFieldName = Nothing
        Me.txt_VendorNo.ReferenceTableName = Nothing
        Me.txt_VendorNo.Size = New System.Drawing.Size(377, 18)
        Me.txt_VendorNo.TabIndex = 11
        '
        'dtp_PayRecDate
        '
        Me.dtp_PayRecDate.CalculationExpression = Nothing
        Me.dtp_PayRecDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtp_PayRecDate.FieldCode = Nothing
        Me.dtp_PayRecDate.FieldDesc = Nothing
        Me.dtp_PayRecDate.FieldMaxLength = 0
        Me.dtp_PayRecDate.FieldName = Nothing
        Me.dtp_PayRecDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_PayRecDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_PayRecDate.isCalculatedField = False
        Me.dtp_PayRecDate.IsSourceFromTable = False
        Me.dtp_PayRecDate.IsSourceFromValueList = False
        Me.dtp_PayRecDate.IsUnique = False
        Me.dtp_PayRecDate.Location = New System.Drawing.Point(135, 160)
        Me.dtp_PayRecDate.MendatroryField = False
        Me.dtp_PayRecDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtp_PayRecDate.MyLinkLable1 = Nothing
        Me.dtp_PayRecDate.MyLinkLable2 = Nothing
        Me.dtp_PayRecDate.Name = "dtp_PayRecDate"
        Me.dtp_PayRecDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtp_PayRecDate.ReferenceFieldDesc = Nothing
        Me.dtp_PayRecDate.ReferenceFieldName = Nothing
        Me.dtp_PayRecDate.ReferenceTableName = Nothing
        Me.dtp_PayRecDate.Size = New System.Drawing.Size(170, 18)
        Me.dtp_PayRecDate.TabIndex = 6
        Me.dtp_PayRecDate.TabStop = False
        Me.dtp_PayRecDate.Text = "14/06/2011 07:13 PM"
        Me.dtp_PayRecDate.Value = New Date(2011, 6, 14, 19, 13, 12, 750)
        '
        'dtp_reversaldate
        '
        Me.dtp_reversaldate.CalculationExpression = Nothing
        Me.dtp_reversaldate.CustomFormat = "dd/MM/yyyy"
        Me.dtp_reversaldate.FieldCode = Nothing
        Me.dtp_reversaldate.FieldDesc = Nothing
        Me.dtp_reversaldate.FieldMaxLength = 0
        Me.dtp_reversaldate.FieldName = Nothing
        Me.dtp_reversaldate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_reversaldate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_reversaldate.isCalculatedField = False
        Me.dtp_reversaldate.IsSourceFromTable = False
        Me.dtp_reversaldate.IsSourceFromValueList = False
        Me.dtp_reversaldate.IsUnique = False
        Me.dtp_reversaldate.Location = New System.Drawing.Point(135, 60)
        Me.dtp_reversaldate.MendatroryField = False
        Me.dtp_reversaldate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtp_reversaldate.MyLinkLable1 = Nothing
        Me.dtp_reversaldate.MyLinkLable2 = Nothing
        Me.dtp_reversaldate.Name = "dtp_reversaldate"
        Me.dtp_reversaldate.ReferenceFieldDesc = Nothing
        Me.dtp_reversaldate.ReferenceFieldName = Nothing
        Me.dtp_reversaldate.ReferenceTableName = Nothing
        Me.dtp_reversaldate.Size = New System.Drawing.Size(170, 18)
        Me.dtp_reversaldate.TabIndex = 1
        Me.dtp_reversaldate.TabStop = False
        Me.dtp_reversaldate.Text = "01/01/1753"
        Me.dtp_reversaldate.Value = New Date(1753, 1, 1, 0, 0, 0, 0)
        '
        'txt_reason
        '
        Me.txt_reason.CalculationExpression = Nothing
        Me.txt_reason.FieldCode = Nothing
        Me.txt_reason.FieldDesc = Nothing
        Me.txt_reason.FieldMaxLength = 0
        Me.txt_reason.FieldName = Nothing
        Me.txt_reason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_reason.isCalculatedField = False
        Me.txt_reason.IsSourceFromTable = False
        Me.txt_reason.IsSourceFromValueList = False
        Me.txt_reason.IsUnique = False
        Me.txt_reason.Location = New System.Drawing.Point(135, 36)
        Me.txt_reason.MaxLength = 100
        Me.txt_reason.MendatroryField = False
        Me.txt_reason.MyLinkLable1 = Nothing
        Me.txt_reason.MyLinkLable2 = Nothing
        Me.txt_reason.Name = "txt_reason"
        Me.txt_reason.ReferenceFieldDesc = Nothing
        Me.txt_reason.ReferenceFieldName = Nothing
        Me.txt_reason.ReferenceTableName = Nothing
        Me.txt_reason.Size = New System.Drawing.Size(564, 18)
        Me.txt_reason.TabIndex = 0
        '
        'lbl_ReversalDate
        '
        Me.lbl_ReversalDate.FieldName = Nothing
        Me.lbl_ReversalDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ReversalDate.Location = New System.Drawing.Point(13, 60)
        Me.lbl_ReversalDate.Name = "lbl_ReversalDate"
        Me.lbl_ReversalDate.Size = New System.Drawing.Size(76, 16)
        Me.lbl_ReversalDate.TabIndex = 11
        Me.lbl_ReversalDate.Text = "Reversal date"
        '
        'lbl_ReasonForReversal
        '
        Me.lbl_ReasonForReversal.FieldName = Nothing
        Me.lbl_ReasonForReversal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ReasonForReversal.Location = New System.Drawing.Point(13, 36)
        Me.lbl_ReasonForReversal.Name = "lbl_ReasonForReversal"
        Me.lbl_ReasonForReversal.Size = New System.Drawing.Size(113, 16)
        Me.lbl_ReasonForReversal.TabIndex = 12
        Me.lbl_ReasonForReversal.Text = "Reason For Reversal"
        '
        'lbl_ReverseDocument
        '
        Me.lbl_ReverseDocument.FieldName = Nothing
        Me.lbl_ReverseDocument.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ReverseDocument.Location = New System.Drawing.Point(13, 105)
        Me.lbl_ReverseDocument.Name = "lbl_ReverseDocument"
        Me.lbl_ReverseDocument.Size = New System.Drawing.Size(103, 16)
        Me.lbl_ReverseDocument.TabIndex = 7
        Me.lbl_ReverseDocument.Text = "Reverse Document"
        '
        'ddl_SourceApplication
        '
        Me.ddl_SourceApplication.AutoCompleteDisplayMember = Nothing
        Me.ddl_SourceApplication.AutoCompleteValueMember = Nothing
        Me.ddl_SourceApplication.CalculationExpression = Nothing
        Me.ddl_SourceApplication.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddl_SourceApplication.FieldCode = Nothing
        Me.ddl_SourceApplication.FieldDesc = Nothing
        Me.ddl_SourceApplication.FieldMaxLength = 0
        Me.ddl_SourceApplication.FieldName = Nothing
        Me.ddl_SourceApplication.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddl_SourceApplication.isCalculatedField = False
        Me.ddl_SourceApplication.IsSourceFromTable = False
        Me.ddl_SourceApplication.IsSourceFromValueList = False
        Me.ddl_SourceApplication.IsUnique = False
        RadListDataItem3.Selected = True
        RadListDataItem3.Text = "Account Payable"
        RadListDataItem4.Text = "Account Receivable"
        Me.ddl_SourceApplication.Items.Add(RadListDataItem3)
        Me.ddl_SourceApplication.Items.Add(RadListDataItem4)
        Me.ddl_SourceApplication.Location = New System.Drawing.Point(148, 82)
        Me.ddl_SourceApplication.MendatroryField = False
        Me.ddl_SourceApplication.MyLinkLable1 = Nothing
        Me.ddl_SourceApplication.MyLinkLable2 = Nothing
        Me.ddl_SourceApplication.Name = "ddl_SourceApplication"
        Me.ddl_SourceApplication.ReferenceFieldDesc = Nothing
        Me.ddl_SourceApplication.ReferenceFieldName = Nothing
        Me.ddl_SourceApplication.ReferenceTableName = Nothing
        Me.ddl_SourceApplication.Size = New System.Drawing.Size(170, 18)
        Me.ddl_SourceApplication.TabIndex = 4
        Me.ddl_SourceApplication.Text = "Account Payable"
        '
        'lbl_SourceApplication
        '
        Me.lbl_SourceApplication.FieldName = Nothing
        Me.lbl_SourceApplication.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SourceApplication.Location = New System.Drawing.Point(13, 82)
        Me.lbl_SourceApplication.Name = "lbl_SourceApplication"
        Me.lbl_SourceApplication.Size = New System.Drawing.Size(101, 16)
        Me.lbl_SourceApplication.TabIndex = 8
        Me.lbl_SourceApplication.Text = "Source Application"
        '
        'txt_BankaccountNo
        '
        Me.txt_BankaccountNo.CalculationExpression = Nothing
        Me.txt_BankaccountNo.FieldCode = Nothing
        Me.txt_BankaccountNo.FieldDesc = Nothing
        Me.txt_BankaccountNo.FieldMaxLength = 0
        Me.txt_BankaccountNo.FieldName = Nothing
        Me.txt_BankaccountNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BankaccountNo.isCalculatedField = False
        Me.txt_BankaccountNo.IsSourceFromTable = False
        Me.txt_BankaccountNo.IsSourceFromValueList = False
        Me.txt_BankaccountNo.IsUnique = False
        Me.txt_BankaccountNo.Location = New System.Drawing.Point(148, 59)
        Me.txt_BankaccountNo.MendatroryField = False
        Me.txt_BankaccountNo.MyLinkLable1 = Nothing
        Me.txt_BankaccountNo.MyLinkLable2 = Nothing
        Me.txt_BankaccountNo.Name = "txt_BankaccountNo"
        Me.txt_BankaccountNo.ReferenceFieldDesc = Nothing
        Me.txt_BankaccountNo.ReferenceFieldName = Nothing
        Me.txt_BankaccountNo.ReferenceTableName = Nothing
        Me.txt_BankaccountNo.Size = New System.Drawing.Size(170, 18)
        Me.txt_BankaccountNo.TabIndex = 3
        '
        'lbl_Bankcode
        '
        Me.lbl_Bankcode.FieldName = Nothing
        Me.lbl_Bankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Bankcode.Location = New System.Drawing.Point(13, 35)
        Me.lbl_Bankcode.Name = "lbl_Bankcode"
        Me.lbl_Bankcode.Size = New System.Drawing.Size(62, 16)
        Me.lbl_Bankcode.TabIndex = 10
        Me.lbl_Bankcode.Text = "Bank Code"
        '
        'txt_Bankcode
        '
        Me.txt_Bankcode.CalculationExpression = Nothing
        Me.txt_Bankcode.FieldCode = Nothing
        Me.txt_Bankcode.FieldDesc = Nothing
        Me.txt_Bankcode.FieldMaxLength = 0
        Me.txt_Bankcode.FieldName = Nothing
        Me.txt_Bankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Bankcode.isCalculatedField = False
        Me.txt_Bankcode.IsSourceFromTable = False
        Me.txt_Bankcode.IsSourceFromValueList = False
        Me.txt_Bankcode.IsUnique = False
        Me.txt_Bankcode.Location = New System.Drawing.Point(335, 38)
        Me.txt_Bankcode.MendatroryField = False
        Me.txt_Bankcode.MyLinkLable1 = Nothing
        Me.txt_Bankcode.MyLinkLable2 = Nothing
        Me.txt_Bankcode.Name = "txt_Bankcode"
        Me.txt_Bankcode.ReadOnly = True
        Me.txt_Bankcode.ReferenceFieldDesc = Nothing
        Me.txt_Bankcode.ReferenceFieldName = Nothing
        Me.txt_Bankcode.ReferenceTableName = Nothing
        Me.txt_Bankcode.Size = New System.Drawing.Size(377, 18)
        Me.txt_Bankcode.TabIndex = 2
        Me.txt_Bankcode.TabStop = False
        '
        'lbl_Reversecode
        '
        Me.lbl_Reversecode.FieldName = Nothing
        Me.lbl_Reversecode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Reversecode.Location = New System.Drawing.Point(13, 11)
        Me.lbl_Reversecode.Name = "lbl_Reversecode"
        Me.lbl_Reversecode.Size = New System.Drawing.Size(79, 16)
        Me.lbl_Reversecode.TabIndex = 11
        Me.lbl_Reversecode.Text = "Reverse Code"
        '
        'lbl_BankaccNo
        '
        Me.lbl_BankaccNo.FieldName = Nothing
        Me.lbl_BankaccNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BankaccNo.Location = New System.Drawing.Point(13, 59)
        Me.lbl_BankaccNo.Name = "lbl_BankaccNo"
        Me.lbl_BankaccNo.Size = New System.Drawing.Size(120, 16)
        Me.lbl_BankaccNo.TabIndex = 9
        Me.lbl_BankaccNo.Text = "Bank Account Number"
        '
        'btn_delete
        '
        Me.btn_delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_delete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_delete.Location = New System.Drawing.Point(83, 12)
        Me.btn_delete.Name = "btn_delete"
        Me.btn_delete.Size = New System.Drawing.Size(68, 18)
        Me.btn_delete.TabIndex = 1
        Me.btn_delete.Text = "Delete"
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Controls.Add(Me.RadButton10)
        Me.btn_close.Controls.Add(Me.RadButton6)
        Me.btn_close.Controls.Add(Me.RadButton7)
        Me.btn_close.Controls.Add(Me.RadButton8)
        Me.btn_close.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_close.Location = New System.Drawing.Point(671, 12)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(68, 18)
        Me.btn_close.TabIndex = 5
        Me.btn_close.Text = "Close"
        '
        'RadButton10
        '
        Me.RadButton10.Location = New System.Drawing.Point(-429, 0)
        Me.RadButton10.Name = "RadButton10"
        Me.RadButton10.Size = New System.Drawing.Size(68, 18)
        Me.RadButton10.TabIndex = 3
        Me.RadButton10.Text = "Print"
        '
        'RadButton6
        '
        Me.RadButton6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton6.Location = New System.Drawing.Point(-501, 0)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(68, 18)
        Me.RadButton6.TabIndex = 2
        Me.RadButton6.Text = "Post"
        '
        'RadButton7
        '
        Me.RadButton7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton7.Location = New System.Drawing.Point(-644, 0)
        Me.RadButton7.Name = "RadButton7"
        Me.RadButton7.Size = New System.Drawing.Size(68, 18)
        Me.RadButton7.TabIndex = 0
        Me.RadButton7.Text = "Save"
        '
        'RadButton8
        '
        Me.RadButton8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton8.Location = New System.Drawing.Point(-574, 0)
        Me.RadButton8.Name = "RadButton8"
        Me.RadButton8.Size = New System.Drawing.Size(68, 18)
        Me.RadButton8.TabIndex = 1
        Me.RadButton8.Text = "Delete"
        '
        'btn_save
        '
        Me.btn_save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_save.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.Location = New System.Drawing.Point(13, 12)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(68, 18)
        Me.btn_save.TabIndex = 0
        Me.btn_save.Text = "Save"
        '
        'btn_post
        '
        Me.btn_post.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_post.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_post.Location = New System.Drawing.Point(156, 12)
        Me.btn_post.Name = "btn_post"
        Me.btn_post.Size = New System.Drawing.Size(68, 18)
        Me.btn_post.TabIndex = 2
        Me.btn_post.Text = "Post"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(228, 12)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 3
        Me.btnprint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOpenBankCashBook)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseTransaction)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_close)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_delete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_save)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_post)
        Me.SplitContainer1.Size = New System.Drawing.Size(751, 379)
        Me.SplitContainer1.SplitterDistance = 337
        Me.SplitContainer1.TabIndex = 6
        '
        'btnReverseTransaction
        '
        Me.btnReverseTransaction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverseTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseTransaction.Location = New System.Drawing.Point(302, 12)
        Me.btnReverseTransaction.Name = "btnReverseTransaction"
        Me.btnReverseTransaction.Size = New System.Drawing.Size(220, 18)
        Me.btnReverseTransaction.TabIndex = 4
        Me.btnReverseTransaction.Text = "Reverse And Unpost Transaction"
        Me.btnReverseTransaction.Visible = False
        '
        'btnOpenBankCashBook
        '
        Me.btnOpenBankCashBook.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenBankCashBook.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenBankCashBook.Location = New System.Drawing.Point(544, 12)
        Me.btnOpenBankCashBook.Name = "btnOpenBankCashBook"
        Me.btnOpenBankCashBook.Size = New System.Drawing.Size(124, 18)
        Me.btnOpenBankCashBook.TabIndex = 15
        Me.btnOpenBankCashBook.Text = "Open Bank Cash Book"
        '
        'frmReverseTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(751, 399)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmReverseTransaction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Reverse Entry"
        CType(Me.RadTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadTextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.btn_reset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PaymentReverseDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ReceiptReversedocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lbl_checkpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_CheckReceiptNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsChequeBounce, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_checkreceiptno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Receiptdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_paymentAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_receiptAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Paymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_CustomerNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Vendornumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_ReceiptAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Paymentamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_checkpaymentno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CustomerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_VendorNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtp_PayRecDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtp_reversaldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_reason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_ReversalDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_ReasonForReversal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_ReverseDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddl_SourceApplication, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_SourceApplication, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_BankaccountNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Bankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Bankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Reversecode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_BankaccNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_delete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_close, System.ComponentModel.ISupportInitialize).EndInit()
        Me.btn_close.ResumeLayout(False)
        CType(Me.RadButton10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_save, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_post, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnReverseTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOpenBankCashBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadTextBox1 As common.Controls.MyTextBox
    Friend WithEvents RadTextBox2 As common.Controls.MyTextBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndbankcode As common.UserControls.txtFinder
    Friend WithEvents fndreversecode As common.UserControls.txtNavigator
    Friend WithEvents btn_reset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txt_PaymentReverseDocument As common.Controls.MyTextBox
    Friend WithEvents txt_ReceiptReversedocument As common.Controls.MyTextBox
    Friend WithEvents btn_post As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_save As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_close As Telerik.WinControls.UI.RadButton
    Friend WithEvents btn_delete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndcustomerNo As common.UserControls.txtFinder
    Friend WithEvents txt_checkreceiptno As common.Controls.MyTextBox
    Friend WithEvents fndVendorNo As common.UserControls.txtFinder
    Friend WithEvents fndcheckReceiptNo As common.UserControls.txtFinder
    Friend WithEvents fndCheckPaymentNo As common.UserControls.txtFinder
    Friend WithEvents lbl_Receiptdate As common.Controls.MyLabel
    Friend WithEvents txt_paymentAmount As common.Controls.MyTextBox
    Friend WithEvents txt_receiptAmount As common.Controls.MyTextBox
    Friend WithEvents lbl_Paymentdate As common.Controls.MyLabel
    Friend WithEvents lbl_CustomerNumber As common.Controls.MyLabel
    Friend WithEvents lbl_Vendornumber As common.Controls.MyLabel
    Friend WithEvents lbl_ReceiptAmount As common.Controls.MyLabel
    Friend WithEvents lbl_Paymentamount As common.Controls.MyLabel
    Friend WithEvents txt_checkpaymentno As common.Controls.MyTextBox
    Friend WithEvents txt_CustomerNo As common.Controls.MyTextBox
    Friend WithEvents txt_VendorNo As common.Controls.MyTextBox
    Friend WithEvents lbl_CheckReceiptNo As common.Controls.MyLabel
    Friend WithEvents lbl_checkpaymentno As common.Controls.MyLabel
    Friend WithEvents dtp_PayRecDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtp_reversaldate As common.Controls.MyDateTimePicker
    Friend WithEvents txt_reason As common.Controls.MyTextBox
    Friend WithEvents lbl_ReversalDate As common.Controls.MyLabel
    Friend WithEvents lbl_ReasonForReversal As common.Controls.MyLabel
    Friend WithEvents lbl_ReverseDocument As common.Controls.MyLabel
    Friend WithEvents ddl_SourceApplication As common.Controls.MyComboBox
    Friend WithEvents lbl_SourceApplication As common.Controls.MyLabel
    Friend WithEvents txt_BankaccountNo As common.Controls.MyTextBox
    Friend WithEvents lbl_Bankcode As common.Controls.MyLabel
    Friend WithEvents txt_Bankcode As common.Controls.MyTextBox
    Friend WithEvents lbl_Reversecode As common.Controls.MyLabel
    Friend WithEvents lbl_BankaccNo As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadButton10 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton6 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton7 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton8 As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnReverseTransaction As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIsChequeBounce As common.Controls.MyCheckBox
    Friend WithEvents btnOpenBankCashBook As Telerik.WinControls.UI.RadButton
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReverseTransaction_vb2
    Inherits Telerik.WinControls.UI.RadForm

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
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReverseTransaction_vb2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 292)
        Me.Name = "ReverseTransaction_vb2"
        Me.Text = "ReverseTransaction_vb2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class

