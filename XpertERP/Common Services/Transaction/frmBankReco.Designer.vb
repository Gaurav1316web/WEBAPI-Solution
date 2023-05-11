<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankReco
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.UsLock1 = New common.usLock()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnWithdrawal = New Telerik.WinControls.UI.RadButton()
        Me.ddlTransType = New Telerik.WinControls.UI.RadDropDownList()
        Me.btnDeposit = New Telerik.WinControls.UI.RadButton()
        Me.btnSelectAll = New Telerik.WinControls.UI.RadButton()
        Me.btnClear = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.gvImport = New common.UserControls.MyRadGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cboDocNo = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel29 = New common.Controls.MyLabel()
        Me.rbtnDocumentNo = New common.Controls.MyRadioButton()
        Me.cboDate = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.rbtnAmtAntType = New common.Controls.MyRadioButton()
        Me.rbtnChequeNoAmtAntType = New common.Controls.MyRadioButton()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.cboType = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.cboAmount = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.cboColumnChequeNo = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.txtOutOfBalanceGrid = New common.Controls.MyTextBox()
        Me.txtWithdrawalTotal = New common.Controls.MyTextBox()
        Me.txtDepositTotal = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblWithdrawalbankErrorAmt = New common.Controls.MyLabel()
        Me.lblExchangeRateLossAmt = New common.Controls.MyLabel()
        Me.txtOutOfBalanceAmt = New common.Controls.MyTextBox()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.btnCalculate = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblBookBalanceAmt = New common.Controls.MyTextBox()
        Me.lblAdjustedBookBalance = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel31 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblDepositBankErrorAmt = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblExchangeRateGainAmt = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.fndRecoId = New common.UserControls.txtNavigator()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.fndBank = New common.UserControls.txtFinder()
        Me.lblCreditCardCharges = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblAdjustedStatementBal = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblDepositOutStandingAmt = New common.Controls.MyTextBox()
        Me.lblWithdrawalAmtOutstanding = New common.Controls.MyTextBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtStatementBalance = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblWriteoffsAmt = New common.Controls.MyLabel()
        Me.txtBankAccntNo = New common.Controls.MyTextBox()
        Me.lblBankEntryNotPosted = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.rdlbltrnasfertype = New common.Controls.MyLabel()
        Me.txtBankName = New common.Controls.MyTextBox()
        Me.dtRecoDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.dtStatementDate = New common.Controls.MyDateTimePicker()
        Me.rdlbltransferdate = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintOutstandingDoc = New Telerik.WinControls.UI.RadButton()
        Me.BtnQuickExport = New Telerik.WinControls.UI.RadButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadMenu1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnWithdrawal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvImport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.cboDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDocumentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAmtAntType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnChequeNoAmtAntType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboColumnChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOutOfBalanceGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWithdrawalTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepositTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWithdrawalbankErrorAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExchangeRateLossAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOutOfBalanceAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCalculate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lblBookBalanceAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdjustedBookBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepositBankErrorAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExchangeRateGainAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditCardCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblAdjustedStatementBal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepositOutStandingAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWithdrawalAmtOutstanding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStatementBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWriteoffsAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankAccntNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankEntryNotPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltrnasfertype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRecoDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStatementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintOutstandingDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Controls.Add(Me.UsLock1)
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(847, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(697, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(128, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 35
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Exit"
        Me.RadMenuItem2.AccessibleName = "Exit"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Exit"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintOutstandingDoc)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnQuickExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(847, 493)
        Me.SplitContainer1.SplitterDistance = 459
        Me.SplitContainer1.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 139)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvImport)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel4)
        Me.SplitContainer2.Size = New System.Drawing.Size(847, 294)
        Me.SplitContainer2.SplitterDistance = 216
        Me.SplitContainer2.TabIndex = 14
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 27)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.ShowFilteringRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(216, 267)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.RadButton1)
        Me.Panel3.Controls.Add(Me.btnWithdrawal)
        Me.Panel3.Controls.Add(Me.ddlTransType)
        Me.Panel3.Controls.Add(Me.btnDeposit)
        Me.Panel3.Controls.Add(Me.btnSelectAll)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.MyLabel20)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(216, 27)
        Me.Panel3.TabIndex = 13
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(81, 2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(76, 22)
        Me.RadButton1.TabIndex = 12
        Me.RadButton1.Text = "Unselect All"
        '
        'btnWithdrawal
        '
        Me.btnWithdrawal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnWithdrawal.Location = New System.Drawing.Point(159, 2)
        Me.btnWithdrawal.Name = "btnWithdrawal"
        Me.btnWithdrawal.Size = New System.Drawing.Size(76, 22)
        Me.btnWithdrawal.TabIndex = 10
        Me.btnWithdrawal.Text = "WithDrawal"
        '
        'ddlTransType
        '
        Me.ddlTransType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlTransType.Location = New System.Drawing.Point(510, 4)
        Me.ddlTransType.Name = "ddlTransType"
        Me.ddlTransType.Size = New System.Drawing.Size(137, 20)
        Me.ddlTransType.TabIndex = 0
        Me.ddlTransType.Visible = False
        '
        'btnDeposit
        '
        Me.btnDeposit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeposit.Location = New System.Drawing.Point(237, 2)
        Me.btnDeposit.Name = "btnDeposit"
        Me.btnDeposit.Size = New System.Drawing.Size(76, 22)
        Me.btnDeposit.TabIndex = 9
        Me.btnDeposit.Text = "Deposit"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 2)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(76, 22)
        Me.btnSelectAll.TabIndex = 11
        Me.btnSelectAll.Text = "Select All"
        '
        'btnClear
        '
        Me.btnClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(315, 2)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(76, 22)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "Clear"
        Me.btnClear.Visible = False
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(397, 6)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel20.TabIndex = 5
        Me.MyLabel20.Text = "Transaction Type"
        Me.MyLabel20.Visible = False
        '
        'gvImport
        '
        Me.gvImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvImport.Location = New System.Drawing.Point(0, 46)
        '
        'gvImport
        '
        Me.gvImport.MasterTemplate.ShowFilteringRow = False
        Me.gvImport.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvImport.Name = "gvImport"
        Me.gvImport.ShowHeaderCellButtons = True
        Me.gvImport.Size = New System.Drawing.Size(627, 248)
        Me.gvImport.TabIndex = 15
        Me.gvImport.TabStop = False
        Me.gvImport.Text = "RadGridView1"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.cboDocNo)
        Me.Panel4.Controls.Add(Me.MyLabel29)
        Me.Panel4.Controls.Add(Me.rbtnDocumentNo)
        Me.Panel4.Controls.Add(Me.cboDate)
        Me.Panel4.Controls.Add(Me.MyLabel27)
        Me.Panel4.Controls.Add(Me.rbtnAmtAntType)
        Me.Panel4.Controls.Add(Me.rbtnChequeNoAmtAntType)
        Me.Panel4.Controls.Add(Me.RadButton5)
        Me.Panel4.Controls.Add(Me.RadButton3)
        Me.Panel4.Controls.Add(Me.RadButton2)
        Me.Panel4.Controls.Add(Me.cboType)
        Me.Panel4.Controls.Add(Me.MyLabel25)
        Me.Panel4.Controls.Add(Me.cboAmount)
        Me.Panel4.Controls.Add(Me.MyLabel23)
        Me.Panel4.Controls.Add(Me.cboColumnChequeNo)
        Me.Panel4.Controls.Add(Me.MyLabel21)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(627, 46)
        Me.Panel4.TabIndex = 14
        '
        'cboDocNo
        '
        Me.cboDocNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDocNo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDocNo.Location = New System.Drawing.Point(554, 1)
        Me.cboDocNo.Name = "cboDocNo"
        Me.cboDocNo.Size = New System.Drawing.Size(69, 20)
        Me.cboDocNo.TabIndex = 17
        '
        'MyLabel29
        '
        Me.MyLabel29.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel29.FieldName = Nothing
        Me.MyLabel29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel29.Location = New System.Drawing.Point(523, 3)
        Me.MyLabel29.Name = "MyLabel29"
        Me.MyLabel29.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel29.TabIndex = 18
        Me.MyLabel29.Text = "Doc."
        '
        'rbtnDocumentNo
        '
        Me.rbtnDocumentNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnDocumentNo.Location = New System.Drawing.Point(380, 25)
        Me.rbtnDocumentNo.MyLinkLable1 = Nothing
        Me.rbtnDocumentNo.MyLinkLable2 = Nothing
        Me.rbtnDocumentNo.Name = "rbtnDocumentNo"
        Me.rbtnDocumentNo.Size = New System.Drawing.Size(91, 18)
        Me.rbtnDocumentNo.TabIndex = 17
        Me.rbtnDocumentNo.TabStop = False
        Me.rbtnDocumentNo.Text = "Document No"
        '
        'cboDate
        '
        Me.cboDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDate.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDate.Location = New System.Drawing.Point(452, 2)
        Me.cboDate.Name = "cboDate"
        Me.cboDate.Size = New System.Drawing.Size(69, 20)
        Me.cboDate.TabIndex = 15
        '
        'MyLabel27
        '
        Me.MyLabel27.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel27.Location = New System.Drawing.Point(414, 4)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel27.TabIndex = 16
        Me.MyLabel27.Text = "Date"
        '
        'rbtnAmtAntType
        '
        Me.rbtnAmtAntType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnAmtAntType.Location = New System.Drawing.Point(269, 25)
        Me.rbtnAmtAntType.MyLinkLable1 = Nothing
        Me.rbtnAmtAntType.MyLinkLable2 = Nothing
        Me.rbtnAmtAntType.Name = "rbtnAmtAntType"
        Me.rbtnAmtAntType.Size = New System.Drawing.Size(113, 18)
        Me.rbtnAmtAntType.TabIndex = 14
        Me.rbtnAmtAntType.TabStop = False
        Me.rbtnAmtAntType.Text = "Amount  and Type"
        '
        'rbtnChequeNoAmtAntType
        '
        Me.rbtnChequeNoAmtAntType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnChequeNoAmtAntType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnChequeNoAmtAntType.Location = New System.Drawing.Point(55, 25)
        Me.rbtnChequeNoAmtAntType.MyLinkLable1 = Nothing
        Me.rbtnChequeNoAmtAntType.MyLinkLable2 = Nothing
        Me.rbtnChequeNoAmtAntType.Name = "rbtnChequeNoAmtAntType"
        Me.rbtnChequeNoAmtAntType.Size = New System.Drawing.Size(223, 18)
        Me.rbtnChequeNoAmtAntType.TabIndex = 13
        Me.rbtnChequeNoAmtAntType.Text = "Search On Cheque No,Amount and Type"
        Me.rbtnChequeNoAmtAntType.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadButton5
        '
        Me.RadButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton5.Location = New System.Drawing.Point(484, 23)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(66, 22)
        Me.RadButton5.TabIndex = 12
        Me.RadButton5.Text = ">>>"
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton3.Location = New System.Drawing.Point(555, 23)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(66, 22)
        Me.RadButton3.TabIndex = 11
        Me.RadButton3.Text = "Export"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(0, 2)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(52, 40)
        Me.RadButton2.TabIndex = 10
        Me.RadButton2.Text = "Browse Excel Sheet"
        Me.RadButton2.TextWrap = True
        '
        'cboType
        '
        Me.cboType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(344, 2)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(69, 20)
        Me.cboType.TabIndex = 8
        '
        'MyLabel25
        '
        Me.MyLabel25.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(309, 4)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel25.TabIndex = 9
        Me.MyLabel25.Text = "Type"
        '
        'cboAmount
        '
        Me.cboAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboAmount.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboAmount.Location = New System.Drawing.Point(238, 2)
        Me.cboAmount.Name = "cboAmount"
        Me.cboAmount.Size = New System.Drawing.Size(69, 20)
        Me.cboAmount.TabIndex = 6
        '
        'MyLabel23
        '
        Me.MyLabel23.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(191, 4)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel23.TabIndex = 7
        Me.MyLabel23.Text = "Amount"
        '
        'cboColumnChequeNo
        '
        Me.cboColumnChequeNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboColumnChequeNo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboColumnChequeNo.Location = New System.Drawing.Point(121, 2)
        Me.cboColumnChequeNo.Name = "cboColumnChequeNo"
        Me.cboColumnChequeNo.Size = New System.Drawing.Size(69, 20)
        Me.cboColumnChequeNo.TabIndex = 0
        '
        'MyLabel21
        '
        Me.MyLabel21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(53, 4)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel21.TabIndex = 5
        Me.MyLabel21.Text = "Cheque No"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadLabel12)
        Me.Panel2.Controls.Add(Me.txtOutOfBalanceGrid)
        Me.Panel2.Controls.Add(Me.txtWithdrawalTotal)
        Me.Panel2.Controls.Add(Me.txtDepositTotal)
        Me.Panel2.Controls.Add(Me.MyLabel13)
        Me.Panel2.Controls.Add(Me.MyLabel17)
        Me.Panel2.Controls.Add(Me.MyLabel16)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 433)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(847, 26)
        Me.Panel2.TabIndex = 12
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(564, 5)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(280, 16)
        Me.RadLabel12.TabIndex = 6
        Me.RadLabel12.Text = "Double click on Document No. to Open transaction"
        '
        'txtOutOfBalanceGrid
        '
        Me.txtOutOfBalanceGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtOutOfBalanceGrid.CalculationExpression = Nothing
        Me.txtOutOfBalanceGrid.FieldCode = Nothing
        Me.txtOutOfBalanceGrid.FieldDesc = Nothing
        Me.txtOutOfBalanceGrid.FieldMaxLength = 0
        Me.txtOutOfBalanceGrid.FieldName = Nothing
        Me.txtOutOfBalanceGrid.isCalculatedField = False
        Me.txtOutOfBalanceGrid.IsSourceFromTable = False
        Me.txtOutOfBalanceGrid.IsSourceFromValueList = False
        Me.txtOutOfBalanceGrid.IsUnique = False
        Me.txtOutOfBalanceGrid.Location = New System.Drawing.Point(490, 3)
        Me.txtOutOfBalanceGrid.MaxLength = 50
        Me.txtOutOfBalanceGrid.MendatroryField = False
        Me.txtOutOfBalanceGrid.MyLinkLable1 = Nothing
        Me.txtOutOfBalanceGrid.MyLinkLable2 = Nothing
        Me.txtOutOfBalanceGrid.Name = "txtOutOfBalanceGrid"
        Me.txtOutOfBalanceGrid.ReadOnly = True
        Me.txtOutOfBalanceGrid.ReferenceFieldDesc = Nothing
        Me.txtOutOfBalanceGrid.ReferenceFieldName = Nothing
        Me.txtOutOfBalanceGrid.ReferenceTableName = Nothing
        Me.txtOutOfBalanceGrid.Size = New System.Drawing.Size(125, 20)
        Me.txtOutOfBalanceGrid.TabIndex = 3
        Me.txtOutOfBalanceGrid.Text = "0.00"
        Me.txtOutOfBalanceGrid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOutOfBalanceGrid.Visible = False
        '
        'txtWithdrawalTotal
        '
        Me.txtWithdrawalTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtWithdrawalTotal.CalculationExpression = Nothing
        Me.txtWithdrawalTotal.FieldCode = Nothing
        Me.txtWithdrawalTotal.FieldDesc = Nothing
        Me.txtWithdrawalTotal.FieldMaxLength = 0
        Me.txtWithdrawalTotal.FieldName = Nothing
        Me.txtWithdrawalTotal.isCalculatedField = False
        Me.txtWithdrawalTotal.IsSourceFromTable = False
        Me.txtWithdrawalTotal.IsSourceFromValueList = False
        Me.txtWithdrawalTotal.IsUnique = False
        Me.txtWithdrawalTotal.Location = New System.Drawing.Point(69, 3)
        Me.txtWithdrawalTotal.MaxLength = 50
        Me.txtWithdrawalTotal.MendatroryField = False
        Me.txtWithdrawalTotal.MyLinkLable1 = Nothing
        Me.txtWithdrawalTotal.MyLinkLable2 = Nothing
        Me.txtWithdrawalTotal.Name = "txtWithdrawalTotal"
        Me.txtWithdrawalTotal.ReadOnly = True
        Me.txtWithdrawalTotal.ReferenceFieldDesc = Nothing
        Me.txtWithdrawalTotal.ReferenceFieldName = Nothing
        Me.txtWithdrawalTotal.ReferenceTableName = Nothing
        Me.txtWithdrawalTotal.Size = New System.Drawing.Size(125, 20)
        Me.txtWithdrawalTotal.TabIndex = 1
        Me.txtWithdrawalTotal.Text = "0.00"
        Me.txtWithdrawalTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDepositTotal
        '
        Me.txtDepositTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDepositTotal.CalculationExpression = Nothing
        Me.txtDepositTotal.FieldCode = Nothing
        Me.txtDepositTotal.FieldDesc = Nothing
        Me.txtDepositTotal.FieldMaxLength = 0
        Me.txtDepositTotal.FieldName = Nothing
        Me.txtDepositTotal.isCalculatedField = False
        Me.txtDepositTotal.IsSourceFromTable = False
        Me.txtDepositTotal.IsSourceFromValueList = False
        Me.txtDepositTotal.IsUnique = False
        Me.txtDepositTotal.Location = New System.Drawing.Point(253, 3)
        Me.txtDepositTotal.MaxLength = 50
        Me.txtDepositTotal.MendatroryField = False
        Me.txtDepositTotal.MyLinkLable1 = Nothing
        Me.txtDepositTotal.MyLinkLable2 = Nothing
        Me.txtDepositTotal.Name = "txtDepositTotal"
        Me.txtDepositTotal.ReadOnly = True
        Me.txtDepositTotal.ReferenceFieldDesc = Nothing
        Me.txtDepositTotal.ReferenceFieldName = Nothing
        Me.txtDepositTotal.ReferenceTableName = Nothing
        Me.txtDepositTotal.Size = New System.Drawing.Size(125, 20)
        Me.txtDepositTotal.TabIndex = 2
        Me.txtDepositTotal.Text = "0.00"
        Me.txtDepositTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel13
        '
        Me.MyLabel13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(1, 5)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel13.TabIndex = 7
        Me.MyLabel13.Text = "Withdrawal"
        '
        'MyLabel17
        '
        Me.MyLabel17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(384, 5)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel17.TabIndex = 4
        Me.MyLabel17.Text = "Out Of Balance By"
        Me.MyLabel17.Visible = False
        '
        'MyLabel16
        '
        Me.MyLabel16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(202, 5)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel16.TabIndex = 8
        Me.MyLabel16.Text = "Deposit"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton6)
        Me.Panel1.Controls.Add(Me.RadButton4)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.lblWithdrawalbankErrorAmt)
        Me.Panel1.Controls.Add(Me.lblExchangeRateLossAmt)
        Me.Panel1.Controls.Add(Me.txtOutOfBalanceAmt)
        Me.Panel1.Controls.Add(Me.MyLabel33)
        Me.Panel1.Controls.Add(Me.btnCalculate)
        Me.Panel1.Controls.Add(Me.MyLabel6)
        Me.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.Panel1.Controls.Add(Me.lblDepositBankErrorAmt)
        Me.Panel1.Controls.Add(Me.MyLabel14)
        Me.Panel1.Controls.Add(Me.lblExchangeRateGainAmt)
        Me.Panel1.Controls.Add(Me.MyLabel12)
        Me.Panel1.Controls.Add(Me.fndRecoId)
        Me.Panel1.Controls.Add(Me.MyLabel28)
        Me.Panel1.Controls.Add(Me.fndBank)
        Me.Panel1.Controls.Add(Me.lblCreditCardCharges)
        Me.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.Panel1.Controls.Add(Me.MyLabel26)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.MyLabel24)
        Me.Panel1.Controls.Add(Me.MyLabel2)
        Me.Panel1.Controls.Add(Me.lblWriteoffsAmt)
        Me.Panel1.Controls.Add(Me.txtBankAccntNo)
        Me.Panel1.Controls.Add(Me.lblBankEntryNotPosted)
        Me.Panel1.Controls.Add(Me.MyLabel22)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.MyLabel19)
        Me.Panel1.Controls.Add(Me.rdlbltrnasfertype)
        Me.Panel1.Controls.Add(Me.txtBankName)
        Me.Panel1.Controls.Add(Me.dtRecoDate)
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.dtStatementDate)
        Me.Panel1.Controls.Add(Me.rdlbltransferdate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(847, 139)
        Me.Panel1.TabIndex = 0
        '
        'RadButton6
        '
        Me.RadButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton6.Location = New System.Drawing.Point(323, 114)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(246, 22)
        Me.RadButton6.TabIndex = 19
        Me.RadButton6.Text = "View Hide Enteries"
        Me.RadButton6.Visible = False
        '
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton4.Location = New System.Drawing.Point(587, 114)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(232, 22)
        Me.RadButton4.TabIndex = 18
        Me.RadButton4.Text = "Show/Hide Import && Identify panel"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(300, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(21, 20)
        Me.btnReset.TabIndex = 9
        '
        'lblWithdrawalbankErrorAmt
        '
        Me.lblWithdrawalbankErrorAmt.AutoSize = False
        Me.lblWithdrawalbankErrorAmt.BorderVisible = True
        Me.lblWithdrawalbankErrorAmt.FieldName = Nothing
        Me.lblWithdrawalbankErrorAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWithdrawalbankErrorAmt.Location = New System.Drawing.Point(956, 146)
        Me.lblWithdrawalbankErrorAmt.Name = "lblWithdrawalbankErrorAmt"
        Me.lblWithdrawalbankErrorAmt.Size = New System.Drawing.Size(110, 20)
        Me.lblWithdrawalbankErrorAmt.TabIndex = 8
        Me.lblWithdrawalbankErrorAmt.Text = "0.00"
        Me.lblWithdrawalbankErrorAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblWithdrawalbankErrorAmt.Visible = False
        '
        'lblExchangeRateLossAmt
        '
        Me.lblExchangeRateLossAmt.AutoSize = False
        Me.lblExchangeRateLossAmt.BorderVisible = True
        Me.lblExchangeRateLossAmt.FieldName = Nothing
        Me.lblExchangeRateLossAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExchangeRateLossAmt.Location = New System.Drawing.Point(1028, 93)
        Me.lblExchangeRateLossAmt.Name = "lblExchangeRateLossAmt"
        Me.lblExchangeRateLossAmt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblExchangeRateLossAmt.Size = New System.Drawing.Size(110, 20)
        Me.lblExchangeRateLossAmt.TabIndex = 6
        Me.lblExchangeRateLossAmt.Text = "0.00"
        Me.lblExchangeRateLossAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblExchangeRateLossAmt.Visible = False
        '
        'txtOutOfBalanceAmt
        '
        Me.txtOutOfBalanceAmt.CalculationExpression = Nothing
        Me.txtOutOfBalanceAmt.FieldCode = Nothing
        Me.txtOutOfBalanceAmt.FieldDesc = Nothing
        Me.txtOutOfBalanceAmt.FieldMaxLength = 0
        Me.txtOutOfBalanceAmt.FieldName = Nothing
        Me.txtOutOfBalanceAmt.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtOutOfBalanceAmt.isCalculatedField = False
        Me.txtOutOfBalanceAmt.IsSourceFromTable = False
        Me.txtOutOfBalanceAmt.IsSourceFromValueList = False
        Me.txtOutOfBalanceAmt.IsUnique = False
        Me.txtOutOfBalanceAmt.Location = New System.Drawing.Point(680, 90)
        Me.txtOutOfBalanceAmt.MaxLength = 50
        Me.txtOutOfBalanceAmt.MendatroryField = False
        Me.txtOutOfBalanceAmt.MyLinkLable1 = Nothing
        Me.txtOutOfBalanceAmt.MyLinkLable2 = Nothing
        Me.txtOutOfBalanceAmt.Name = "txtOutOfBalanceAmt"
        Me.txtOutOfBalanceAmt.ReadOnly = True
        Me.txtOutOfBalanceAmt.ReferenceFieldDesc = Nothing
        Me.txtOutOfBalanceAmt.ReferenceFieldName = Nothing
        Me.txtOutOfBalanceAmt.ReferenceTableName = Nothing
        Me.txtOutOfBalanceAmt.Size = New System.Drawing.Size(139, 21)
        Me.txtOutOfBalanceAmt.TabIndex = 3
        Me.txtOutOfBalanceAmt.Text = "0.00"
        Me.txtOutOfBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel33.Location = New System.Drawing.Point(586, 92)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel33.TabIndex = 4
        Me.MyLabel33.Text = "Out Of Balance"
        '
        'btnCalculate
        '
        Me.btnCalculate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalculate.Location = New System.Drawing.Point(587, 67)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(232, 22)
        Me.btnCalculate.TabIndex = 2
        Me.btnCalculate.Text = "Calculate"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(2, 5)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel6.TabIndex = 7
        Me.MyLabel6.Text = "Reco ID"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.lblBookBalanceAmt)
        Me.RadGroupBox2.Controls.Add(Me.lblAdjustedBookBalance)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel31)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.HeaderText = "General Ledger"
        Me.RadGroupBox2.Location = New System.Drawing.Point(579, 1)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(246, 65)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "General Ledger"
        '
        'lblBookBalanceAmt
        '
        Me.lblBookBalanceAmt.CalculationExpression = Nothing
        Me.lblBookBalanceAmt.FieldCode = Nothing
        Me.lblBookBalanceAmt.FieldDesc = Nothing
        Me.lblBookBalanceAmt.FieldMaxLength = 0
        Me.lblBookBalanceAmt.FieldName = Nothing
        Me.lblBookBalanceAmt.isCalculatedField = False
        Me.lblBookBalanceAmt.IsSourceFromTable = False
        Me.lblBookBalanceAmt.IsSourceFromValueList = False
        Me.lblBookBalanceAmt.IsUnique = False
        Me.lblBookBalanceAmt.Location = New System.Drawing.Point(130, 11)
        Me.lblBookBalanceAmt.MaxLength = 50
        Me.lblBookBalanceAmt.MendatroryField = False
        Me.lblBookBalanceAmt.MyLinkLable1 = Nothing
        Me.lblBookBalanceAmt.MyLinkLable2 = Nothing
        Me.lblBookBalanceAmt.Name = "lblBookBalanceAmt"
        Me.lblBookBalanceAmt.ReadOnly = True
        Me.lblBookBalanceAmt.ReferenceFieldDesc = Nothing
        Me.lblBookBalanceAmt.ReferenceFieldName = Nothing
        Me.lblBookBalanceAmt.ReferenceTableName = Nothing
        Me.lblBookBalanceAmt.Size = New System.Drawing.Size(110, 20)
        Me.lblBookBalanceAmt.TabIndex = 1
        Me.lblBookBalanceAmt.Text = "0.00"
        Me.lblBookBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAdjustedBookBalance
        '
        Me.lblAdjustedBookBalance.CalculationExpression = Nothing
        Me.lblAdjustedBookBalance.FieldCode = Nothing
        Me.lblAdjustedBookBalance.FieldDesc = Nothing
        Me.lblAdjustedBookBalance.FieldMaxLength = 0
        Me.lblAdjustedBookBalance.FieldName = Nothing
        Me.lblAdjustedBookBalance.isCalculatedField = False
        Me.lblAdjustedBookBalance.IsSourceFromTable = False
        Me.lblAdjustedBookBalance.IsSourceFromValueList = False
        Me.lblAdjustedBookBalance.IsUnique = False
        Me.lblAdjustedBookBalance.Location = New System.Drawing.Point(130, 34)
        Me.lblAdjustedBookBalance.MaxLength = 50
        Me.lblAdjustedBookBalance.MendatroryField = False
        Me.lblAdjustedBookBalance.MyLinkLable1 = Nothing
        Me.lblAdjustedBookBalance.MyLinkLable2 = Nothing
        Me.lblAdjustedBookBalance.Name = "lblAdjustedBookBalance"
        Me.lblAdjustedBookBalance.ReadOnly = True
        Me.lblAdjustedBookBalance.ReferenceFieldDesc = Nothing
        Me.lblAdjustedBookBalance.ReferenceFieldName = Nothing
        Me.lblAdjustedBookBalance.ReferenceTableName = Nothing
        Me.lblAdjustedBookBalance.Size = New System.Drawing.Size(110, 20)
        Me.lblAdjustedBookBalance.TabIndex = 2
        Me.lblAdjustedBookBalance.Text = "0.00"
        Me.lblAdjustedBookBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(126, 47)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(119, 16)
        Me.MyLabel11.TabIndex = 7
        Me.MyLabel11.Text = "------------------------------"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(126, 23)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(119, 16)
        Me.MyLabel9.TabIndex = 17
        Me.MyLabel9.Text = "------------------------------"
        '
        'MyLabel31
        '
        Me.MyLabel31.FieldName = Nothing
        Me.MyLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel31.Location = New System.Drawing.Point(3, 39)
        Me.MyLabel31.Name = "MyLabel31"
        Me.MyLabel31.Size = New System.Drawing.Size(123, 16)
        Me.MyLabel31.TabIndex = 4
        Me.MyLabel31.Text = "Adjusted Book Balance"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(3, 16)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel5.TabIndex = 16
        Me.MyLabel5.Text = "Book Balance"
        '
        'lblDepositBankErrorAmt
        '
        Me.lblDepositBankErrorAmt.AutoSize = False
        Me.lblDepositBankErrorAmt.BorderVisible = True
        Me.lblDepositBankErrorAmt.FieldName = Nothing
        Me.lblDepositBankErrorAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositBankErrorAmt.Location = New System.Drawing.Point(956, 122)
        Me.lblDepositBankErrorAmt.Name = "lblDepositBankErrorAmt"
        Me.lblDepositBankErrorAmt.Size = New System.Drawing.Size(110, 20)
        Me.lblDepositBankErrorAmt.TabIndex = 10
        Me.lblDepositBankErrorAmt.Text = "0.00"
        Me.lblDepositBankErrorAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDepositBankErrorAmt.Visible = False
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(830, 148)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel14.TabIndex = 9
        Me.MyLabel14.Text = "-Withdraw Bank Errors"
        Me.MyLabel14.Visible = False
        '
        'lblExchangeRateGainAmt
        '
        Me.lblExchangeRateGainAmt.AutoSize = False
        Me.lblExchangeRateGainAmt.BorderVisible = True
        Me.lblExchangeRateGainAmt.FieldName = Nothing
        Me.lblExchangeRateGainAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExchangeRateGainAmt.Location = New System.Drawing.Point(1028, 71)
        Me.lblExchangeRateGainAmt.Name = "lblExchangeRateGainAmt"
        Me.lblExchangeRateGainAmt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblExchangeRateGainAmt.Size = New System.Drawing.Size(110, 20)
        Me.lblExchangeRateGainAmt.TabIndex = 8
        Me.lblExchangeRateGainAmt.Text = "0.00"
        Me.lblExchangeRateGainAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblExchangeRateGainAmt.Visible = False
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(784, 120)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel12.TabIndex = 11
        Me.MyLabel12.Text = "+Deposits Bank Errors"
        Me.MyLabel12.Visible = False
        '
        'fndRecoId
        '
        Me.fndRecoId.FieldName = Nothing
        Me.fndRecoId.Location = New System.Drawing.Point(69, 3)
        Me.fndRecoId.MendatroryField = False
        Me.fndRecoId.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndRecoId.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndRecoId.MyLinkLable1 = Nothing
        Me.fndRecoId.MyLinkLable2 = Nothing
        Me.fndRecoId.MyMaxLength = 32767
        Me.fndRecoId.MyReadOnly = False
        Me.fndRecoId.Name = "fndRecoId"
        Me.fndRecoId.Size = New System.Drawing.Size(231, 20)
        Me.fndRecoId.TabIndex = 0
        Me.fndRecoId.Value = ""
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel28.Location = New System.Drawing.Point(901, 98)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(114, 16)
        Me.MyLabel28.TabIndex = 7
        Me.MyLabel28.Text = "-Exchange Rate Loss"
        Me.MyLabel28.Visible = False
        '
        'fndBank
        '
        Me.fndBank.CalculationExpression = Nothing
        Me.fndBank.FieldCode = Nothing
        Me.fndBank.FieldDesc = Nothing
        Me.fndBank.FieldMaxLength = 0
        Me.fndBank.FieldName = Nothing
        Me.fndBank.isCalculatedField = False
        Me.fndBank.IsSourceFromTable = False
        Me.fndBank.IsSourceFromValueList = False
        Me.fndBank.IsUnique = False
        Me.fndBank.Location = New System.Drawing.Point(68, 69)
        Me.fndBank.MendatroryField = True
        Me.fndBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBank.MyLinkLable1 = Nothing
        Me.fndBank.MyLinkLable2 = Nothing
        Me.fndBank.MyReadOnly = False
        Me.fndBank.MyShowMasterFormButton = False
        Me.fndBank.Name = "fndBank"
        Me.fndBank.ReferenceFieldDesc = Nothing
        Me.fndBank.ReferenceFieldName = Nothing
        Me.fndBank.ReferenceTableName = Nothing
        Me.fndBank.Size = New System.Drawing.Size(252, 20)
        Me.fndBank.TabIndex = 2
        Me.fndBank.Value = ""
        '
        'lblCreditCardCharges
        '
        Me.lblCreditCardCharges.AutoSize = False
        Me.lblCreditCardCharges.BorderVisible = True
        Me.lblCreditCardCharges.FieldName = Nothing
        Me.lblCreditCardCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditCardCharges.Location = New System.Drawing.Point(1028, 49)
        Me.lblCreditCardCharges.Name = "lblCreditCardCharges"
        Me.lblCreditCardCharges.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCreditCardCharges.Size = New System.Drawing.Size(110, 20)
        Me.lblCreditCardCharges.TabIndex = 10
        Me.lblCreditCardCharges.Text = "0.00"
        Me.lblCreditCardCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCreditCardCharges.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblAdjustedStatementBal)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.lblDepositOutStandingAmt)
        Me.RadGroupBox1.Controls.Add(Me.lblWithdrawalAmtOutstanding)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.txtStatementBalance)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.HeaderText = "Bank Statement"
        Me.RadGroupBox1.Location = New System.Drawing.Point(323, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(246, 113)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Bank Statement"
        '
        'lblAdjustedStatementBal
        '
        Me.lblAdjustedStatementBal.CalculationExpression = Nothing
        Me.lblAdjustedStatementBal.FieldCode = Nothing
        Me.lblAdjustedStatementBal.FieldDesc = Nothing
        Me.lblAdjustedStatementBal.FieldMaxLength = 0
        Me.lblAdjustedStatementBal.FieldName = Nothing
        Me.lblAdjustedStatementBal.isCalculatedField = False
        Me.lblAdjustedStatementBal.IsSourceFromTable = False
        Me.lblAdjustedStatementBal.IsSourceFromValueList = False
        Me.lblAdjustedStatementBal.IsUnique = False
        Me.lblAdjustedStatementBal.Location = New System.Drawing.Point(130, 88)
        Me.lblAdjustedStatementBal.MaxLength = 50
        Me.lblAdjustedStatementBal.MendatroryField = False
        Me.lblAdjustedStatementBal.MyLinkLable1 = Nothing
        Me.lblAdjustedStatementBal.MyLinkLable2 = Nothing
        Me.lblAdjustedStatementBal.Name = "lblAdjustedStatementBal"
        Me.lblAdjustedStatementBal.ReadOnly = True
        Me.lblAdjustedStatementBal.ReferenceFieldDesc = Nothing
        Me.lblAdjustedStatementBal.ReferenceFieldName = Nothing
        Me.lblAdjustedStatementBal.ReferenceTableName = Nothing
        Me.lblAdjustedStatementBal.Size = New System.Drawing.Size(110, 20)
        Me.lblAdjustedStatementBal.TabIndex = 4
        Me.lblAdjustedStatementBal.Text = "0.00"
        Me.lblAdjustedStatementBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(126, 100)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(119, 16)
        Me.MyLabel7.TabIndex = 16
        Me.MyLabel7.Text = "------------------------------"
        '
        'lblDepositOutStandingAmt
        '
        Me.lblDepositOutStandingAmt.CalculationExpression = Nothing
        Me.lblDepositOutStandingAmt.FieldCode = Nothing
        Me.lblDepositOutStandingAmt.FieldDesc = Nothing
        Me.lblDepositOutStandingAmt.FieldMaxLength = 0
        Me.lblDepositOutStandingAmt.FieldName = Nothing
        Me.lblDepositOutStandingAmt.isCalculatedField = False
        Me.lblDepositOutStandingAmt.IsSourceFromTable = False
        Me.lblDepositOutStandingAmt.IsSourceFromValueList = False
        Me.lblDepositOutStandingAmt.IsUnique = False
        Me.lblDepositOutStandingAmt.Location = New System.Drawing.Point(130, 61)
        Me.lblDepositOutStandingAmt.MaxLength = 50
        Me.lblDepositOutStandingAmt.MendatroryField = False
        Me.lblDepositOutStandingAmt.MyLinkLable1 = Nothing
        Me.lblDepositOutStandingAmt.MyLinkLable2 = Nothing
        Me.lblDepositOutStandingAmt.Name = "lblDepositOutStandingAmt"
        Me.lblDepositOutStandingAmt.ReadOnly = True
        Me.lblDepositOutStandingAmt.ReferenceFieldDesc = Nothing
        Me.lblDepositOutStandingAmt.ReferenceFieldName = Nothing
        Me.lblDepositOutStandingAmt.ReferenceTableName = Nothing
        Me.lblDepositOutStandingAmt.Size = New System.Drawing.Size(110, 20)
        Me.lblDepositOutStandingAmt.TabIndex = 2
        Me.lblDepositOutStandingAmt.Text = "0.00"
        Me.lblDepositOutStandingAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblWithdrawalAmtOutstanding
        '
        Me.lblWithdrawalAmtOutstanding.CalculationExpression = Nothing
        Me.lblWithdrawalAmtOutstanding.FieldCode = Nothing
        Me.lblWithdrawalAmtOutstanding.FieldDesc = Nothing
        Me.lblWithdrawalAmtOutstanding.FieldMaxLength = 0
        Me.lblWithdrawalAmtOutstanding.FieldName = Nothing
        Me.lblWithdrawalAmtOutstanding.isCalculatedField = False
        Me.lblWithdrawalAmtOutstanding.IsSourceFromTable = False
        Me.lblWithdrawalAmtOutstanding.IsSourceFromValueList = False
        Me.lblWithdrawalAmtOutstanding.IsUnique = False
        Me.lblWithdrawalAmtOutstanding.Location = New System.Drawing.Point(130, 38)
        Me.lblWithdrawalAmtOutstanding.MaxLength = 50
        Me.lblWithdrawalAmtOutstanding.MendatroryField = False
        Me.lblWithdrawalAmtOutstanding.MyLinkLable1 = Nothing
        Me.lblWithdrawalAmtOutstanding.MyLinkLable2 = Nothing
        Me.lblWithdrawalAmtOutstanding.Name = "lblWithdrawalAmtOutstanding"
        Me.lblWithdrawalAmtOutstanding.ReadOnly = True
        Me.lblWithdrawalAmtOutstanding.ReferenceFieldDesc = Nothing
        Me.lblWithdrawalAmtOutstanding.ReferenceFieldName = Nothing
        Me.lblWithdrawalAmtOutstanding.ReferenceTableName = Nothing
        Me.lblWithdrawalAmtOutstanding.Size = New System.Drawing.Size(110, 20)
        Me.lblWithdrawalAmtOutstanding.TabIndex = 3
        Me.lblWithdrawalAmtOutstanding.Text = "0.00"
        Me.lblWithdrawalAmtOutstanding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(4, 90)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(124, 16)
        Me.MyLabel18.TabIndex = 7
        Me.MyLabel18.Text = "Adjusted Stmt. Balance"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(126, 76)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(119, 16)
        Me.MyLabel15.TabIndex = 6
        Me.MyLabel15.Text = "------------------------------"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(4, 40)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(124, 16)
        Me.MyLabel10.TabIndex = 12
        Me.MyLabel10.Text = "-Withdrawl Outstanding"
        '
        'txtStatementBalance
        '
        Me.txtStatementBalance.CalculationExpression = Nothing
        Me.txtStatementBalance.FieldCode = Nothing
        Me.txtStatementBalance.FieldDesc = Nothing
        Me.txtStatementBalance.FieldMaxLength = 0
        Me.txtStatementBalance.FieldName = Nothing
        Me.txtStatementBalance.isCalculatedField = False
        Me.txtStatementBalance.IsSourceFromTable = False
        Me.txtStatementBalance.IsSourceFromValueList = False
        Me.txtStatementBalance.IsUnique = False
        Me.txtStatementBalance.Location = New System.Drawing.Point(130, 15)
        Me.txtStatementBalance.MaxLength = 50
        Me.txtStatementBalance.MendatroryField = False
        Me.txtStatementBalance.MyLinkLable1 = Nothing
        Me.txtStatementBalance.MyLinkLable2 = Nothing
        Me.txtStatementBalance.Name = "txtStatementBalance"
        Me.txtStatementBalance.ReferenceFieldDesc = Nothing
        Me.txtStatementBalance.ReferenceFieldName = Nothing
        Me.txtStatementBalance.ReferenceTableName = Nothing
        Me.txtStatementBalance.Size = New System.Drawing.Size(110, 20)
        Me.txtStatementBalance.TabIndex = 1
        Me.txtStatementBalance.Text = "0.00"
        Me.txtStatementBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(4, 63)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(115, 16)
        Me.MyLabel8.TabIndex = 13
        Me.MyLabel8.Text = "+Deposit Outstanding"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(4, 17)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel4.TabIndex = 14
        Me.MyLabel4.Text = "Statement Balance"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(901, 76)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(114, 16)
        Me.MyLabel26.TabIndex = 9
        Me.MyLabel26.Text = "-Exchange Rate Gain"
        Me.MyLabel26.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(68, 48)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(252, 20)
        Me.txtDescription.TabIndex = 1
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(901, 54)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(114, 16)
        Me.MyLabel24.TabIndex = 11
        Me.MyLabel24.Text = "-Credit Card Charges"
        Me.MyLabel24.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(1, 50)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "Description"
        '
        'lblWriteoffsAmt
        '
        Me.lblWriteoffsAmt.AutoSize = False
        Me.lblWriteoffsAmt.BorderVisible = True
        Me.lblWriteoffsAmt.FieldName = Nothing
        Me.lblWriteoffsAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWriteoffsAmt.Location = New System.Drawing.Point(1028, 27)
        Me.lblWriteoffsAmt.Name = "lblWriteoffsAmt"
        Me.lblWriteoffsAmt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblWriteoffsAmt.Size = New System.Drawing.Size(110, 20)
        Me.lblWriteoffsAmt.TabIndex = 12
        Me.lblWriteoffsAmt.Text = "0.00"
        Me.lblWriteoffsAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblWriteoffsAmt.Visible = False
        '
        'txtBankAccntNo
        '
        Me.txtBankAccntNo.CalculationExpression = Nothing
        Me.txtBankAccntNo.FieldCode = Nothing
        Me.txtBankAccntNo.FieldDesc = Nothing
        Me.txtBankAccntNo.FieldMaxLength = 0
        Me.txtBankAccntNo.FieldName = Nothing
        Me.txtBankAccntNo.isCalculatedField = False
        Me.txtBankAccntNo.IsSourceFromTable = False
        Me.txtBankAccntNo.IsSourceFromValueList = False
        Me.txtBankAccntNo.IsUnique = False
        Me.txtBankAccntNo.Location = New System.Drawing.Point(68, 112)
        Me.txtBankAccntNo.MaxLength = 50
        Me.txtBankAccntNo.MendatroryField = False
        Me.txtBankAccntNo.MyLinkLable1 = Nothing
        Me.txtBankAccntNo.MyLinkLable2 = Nothing
        Me.txtBankAccntNo.Name = "txtBankAccntNo"
        Me.txtBankAccntNo.ReadOnly = True
        Me.txtBankAccntNo.ReferenceFieldDesc = Nothing
        Me.txtBankAccntNo.ReferenceFieldName = Nothing
        Me.txtBankAccntNo.ReferenceTableName = Nothing
        Me.txtBankAccntNo.Size = New System.Drawing.Size(252, 20)
        Me.txtBankAccntNo.TabIndex = 3
        '
        'lblBankEntryNotPosted
        '
        Me.lblBankEntryNotPosted.AutoSize = False
        Me.lblBankEntryNotPosted.BorderVisible = True
        Me.lblBankEntryNotPosted.FieldName = Nothing
        Me.lblBankEntryNotPosted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankEntryNotPosted.Location = New System.Drawing.Point(1028, 5)
        Me.lblBankEntryNotPosted.Name = "lblBankEntryNotPosted"
        Me.lblBankEntryNotPosted.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblBankEntryNotPosted.Size = New System.Drawing.Size(110, 20)
        Me.lblBankEntryNotPosted.TabIndex = 14
        Me.lblBankEntryNotPosted.Text = "0.00"
        Me.lblBankEntryNotPosted.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBankEntryNotPosted.Visible = False
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(901, 32)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel22.TabIndex = 13
        Me.MyLabel22.Text = "±Write-Offs"
        Me.MyLabel22.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(1, 114)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel1.TabIndex = 4
        Me.MyLabel1.Text = "Account No."
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(901, 10)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(127, 16)
        Me.MyLabel19.TabIndex = 15
        Me.MyLabel19.Text = "±Bank Entry Not Posted"
        Me.MyLabel19.Visible = False
        '
        'rdlbltrnasfertype
        '
        Me.rdlbltrnasfertype.FieldName = Nothing
        Me.rdlbltrnasfertype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbltrnasfertype.Location = New System.Drawing.Point(1, 71)
        Me.rdlbltrnasfertype.Name = "rdlbltrnasfertype"
        Me.rdlbltrnasfertype.Size = New System.Drawing.Size(62, 16)
        Me.rdlbltrnasfertype.TabIndex = 5
        Me.rdlbltrnasfertype.Text = "Bank Code"
        '
        'txtBankName
        '
        Me.txtBankName.CalculationExpression = Nothing
        Me.txtBankName.FieldCode = Nothing
        Me.txtBankName.FieldDesc = Nothing
        Me.txtBankName.FieldMaxLength = 0
        Me.txtBankName.FieldName = Nothing
        Me.txtBankName.isCalculatedField = False
        Me.txtBankName.IsSourceFromTable = False
        Me.txtBankName.IsSourceFromValueList = False
        Me.txtBankName.IsUnique = False
        Me.txtBankName.Location = New System.Drawing.Point(68, 91)
        Me.txtBankName.MaxLength = 50
        Me.txtBankName.MendatroryField = False
        Me.txtBankName.MyLinkLable1 = Nothing
        Me.txtBankName.MyLinkLable2 = Nothing
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReadOnly = True
        Me.txtBankName.ReferenceFieldDesc = Nothing
        Me.txtBankName.ReferenceFieldName = Nothing
        Me.txtBankName.ReferenceTableName = Nothing
        Me.txtBankName.Size = New System.Drawing.Size(252, 20)
        Me.txtBankName.TabIndex = 8
        '
        'dtRecoDate
        '
        Me.dtRecoDate.CalculationExpression = Nothing
        Me.dtRecoDate.CustomFormat = "dd/MM/yyyy"
        Me.dtRecoDate.FieldCode = Nothing
        Me.dtRecoDate.FieldDesc = Nothing
        Me.dtRecoDate.FieldMaxLength = 0
        Me.dtRecoDate.FieldName = Nothing
        Me.dtRecoDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRecoDate.isCalculatedField = False
        Me.dtRecoDate.IsSourceFromTable = False
        Me.dtRecoDate.IsSourceFromValueList = False
        Me.dtRecoDate.IsUnique = False
        Me.dtRecoDate.Location = New System.Drawing.Point(69, 26)
        Me.dtRecoDate.MendatroryField = False
        Me.dtRecoDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtRecoDate.MyLinkLable1 = Nothing
        Me.dtRecoDate.MyLinkLable2 = Nothing
        Me.dtRecoDate.Name = "dtRecoDate"
        Me.dtRecoDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtRecoDate.ReferenceFieldDesc = Nothing
        Me.dtRecoDate.ReferenceFieldName = Nothing
        Me.dtRecoDate.ReferenceTableName = Nothing
        Me.dtRecoDate.Size = New System.Drawing.Size(79, 20)
        Me.dtRecoDate.TabIndex = 0
        Me.dtRecoDate.TabStop = False
        Me.dtRecoDate.Text = "27/06/2011"
        Me.dtRecoDate.Value = New Date(2011, 6, 27, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(2, 28)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel3.TabIndex = 17
        Me.MyLabel3.Text = "Reco Date"
        '
        'dtStatementDate
        '
        Me.dtStatementDate.CalculationExpression = Nothing
        Me.dtStatementDate.CustomFormat = "dd/MM/yyyy"
        Me.dtStatementDate.FieldCode = Nothing
        Me.dtStatementDate.FieldDesc = Nothing
        Me.dtStatementDate.FieldMaxLength = 0
        Me.dtStatementDate.FieldName = Nothing
        Me.dtStatementDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStatementDate.isCalculatedField = False
        Me.dtStatementDate.IsSourceFromTable = False
        Me.dtStatementDate.IsSourceFromValueList = False
        Me.dtStatementDate.IsUnique = False
        Me.dtStatementDate.Location = New System.Drawing.Point(241, 26)
        Me.dtStatementDate.MendatroryField = False
        Me.dtStatementDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStatementDate.MyLinkLable1 = Nothing
        Me.dtStatementDate.MyLinkLable2 = Nothing
        Me.dtStatementDate.Name = "dtStatementDate"
        Me.dtStatementDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStatementDate.ReferenceFieldDesc = Nothing
        Me.dtStatementDate.ReferenceFieldName = Nothing
        Me.dtStatementDate.ReferenceTableName = Nothing
        Me.dtStatementDate.Size = New System.Drawing.Size(79, 20)
        Me.dtStatementDate.TabIndex = 0
        Me.dtStatementDate.TabStop = False
        Me.dtStatementDate.Text = "27/06/2011"
        Me.dtStatementDate.Value = New Date(2011, 6, 27, 0, 0, 0, 0)
        '
        'rdlbltransferdate
        '
        Me.rdlbltransferdate.FieldName = Nothing
        Me.rdlbltransferdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdlbltransferdate.Location = New System.Drawing.Point(155, 28)
        Me.rdlbltransferdate.Name = "rdlbltransferdate"
        Me.rdlbltransferdate.Size = New System.Drawing.Size(85, 16)
        Me.rdlbltransferdate.TabIndex = 15
        Me.rdlbltransferdate.Text = "Statement Date"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(648, 5)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(112, 22)
        Me.btnReverse.TabIndex = 14
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnPrintOutstandingDoc
        '
        Me.btnPrintOutstandingDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintOutstandingDoc.Location = New System.Drawing.Point(477, 5)
        Me.btnPrintOutstandingDoc.Name = "btnPrintOutstandingDoc"
        Me.btnPrintOutstandingDoc.Size = New System.Drawing.Size(169, 22)
        Me.btnPrintOutstandingDoc.TabIndex = 13
        Me.btnPrintOutstandingDoc.Text = "Print Outstanding Documents"
        '
        'BtnQuickExport
        '
        Me.BtnQuickExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnQuickExport.Location = New System.Drawing.Point(382, 5)
        Me.BtnQuickExport.Name = "BtnQuickExport"
        Me.BtnQuickExport.Size = New System.Drawing.Size(93, 22)
        Me.BtnQuickExport.TabIndex = 12
        Me.BtnQuickExport.Text = "Quick Export"
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Location = New System.Drawing.Point(287, 5)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(93, 22)
        Me.btnExcel.TabIndex = 11
        Me.btnExcel.Text = "Export To Excel"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(193, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(93, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(98, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(93, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(762, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(82, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmBankReco
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(847, 513)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmBankReco"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Reconciliation"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadMenu1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnWithdrawal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvImport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvImport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.cboDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDocumentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAmtAntType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnChequeNoAmtAntType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboColumnChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOutOfBalanceGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWithdrawalTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepositTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWithdrawalbankErrorAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExchangeRateLossAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOutOfBalanceAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCalculate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lblBookBalanceAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdjustedBookBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepositBankErrorAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExchangeRateGainAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditCardCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblAdjustedStatementBal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepositOutStandingAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWithdrawalAmtOutstanding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStatementBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWriteoffsAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankAccntNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankEntryNotPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltrnasfertype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRecoDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStatementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdlbltransferdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintOutstandingDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdlbltrnasfertype As common.Controls.MyLabel
    Friend WithEvents txtBankName As common.Controls.MyTextBox
    Friend WithEvents txtBankAccntNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents dtRecoDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtStatementBalance As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents dtStatementDate As common.Controls.MyDateTimePicker
    Friend WithEvents rdlbltransferdate As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents lblWithdrawalbankErrorAmt As common.Controls.MyLabel
    Friend WithEvents lblDepositBankErrorAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents lblCreditCardCharges As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents lblWriteoffsAmt As common.Controls.MyLabel
    Friend WithEvents lblBankEntryNotPosted As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtOutOfBalanceAmt As common.Controls.MyTextBox
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents MyLabel31 As common.Controls.MyLabel
    Friend WithEvents lblExchangeRateLossAmt As common.Controls.MyLabel
    Friend WithEvents lblExchangeRateGainAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents btnCalculate As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndRecoId As common.UserControls.txtNavigator
    Friend WithEvents fndBank As common.UserControls.txtFinder
    Friend WithEvents btnClear As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectAll As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBookBalanceAmt As common.Controls.MyTextBox
    Friend WithEvents lblAdjustedStatementBal As common.Controls.MyTextBox
    Friend WithEvents lblDepositOutStandingAmt As common.Controls.MyTextBox
    Friend WithEvents lblWithdrawalAmtOutstanding As common.Controls.MyTextBox
    Friend WithEvents lblAdjustedBookBalance As common.Controls.MyTextBox
    Friend WithEvents btnDeposit As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnWithdrawal As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtDepositTotal As common.Controls.MyTextBox
    Friend WithEvents txtWithdrawalTotal As common.Controls.MyTextBox
    Friend WithEvents txtOutOfBalanceGrid As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents ddlTransType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents BtnQuickExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvImport As common.UserControls.MyRadGridView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents RadButton5 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents cboAmount As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents cboColumnChequeNo As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnAmtAntType As common.Controls.MyRadioButton
    Friend WithEvents rbtnChequeNoAmtAntType As common.Controls.MyRadioButton
    Friend WithEvents cboDate As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents btnPrintOutstandingDoc As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents cboDocNo As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents MyLabel29 As common.Controls.MyLabel
    Friend WithEvents rbtnDocumentNo As common.Controls.MyRadioButton
    Friend WithEvents RadButton6 As Telerik.WinControls.UI.RadButton
End Class

