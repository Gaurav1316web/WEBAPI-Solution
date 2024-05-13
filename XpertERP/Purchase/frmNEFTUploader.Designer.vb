<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEFTUploader
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.pnlrtgsamt = New Telerik.WinControls.UI.RadPanel()
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel()
        Me.btnupdate = New Telerik.WinControls.UI.RadButton()
        Me.txtAmt = New common.MyNumBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtVendorCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblVendorCode = New common.Controls.MyLabel()
        Me.txtInvoiceDate = New common.Controls.MyDateTimePicker()
        Me.lblInvDate = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.txtBankCode = New common.Controls.MyLabel()
        Me.pnlCreateNeftuploaderPlantWise = New System.Windows.Forms.Panel()
        Me.btnGoForPlantwise = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtToDatePlant = New common.Controls.MyDateTimePicker()
        Me.txtFromDatePlant = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtMonth = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndPlantCode = New common.UserControls.txtFinder()
        Me.lblBankCaption = New common.Controls.MyLabel()
        Me.fndBank = New common.UserControls.txtFinder()
        Me.lblbankcode = New common.Controls.MyLabel()
        Me.lblFarmerType = New common.Controls.MyLabel()
        Me.CboTypePaymentFarmer = New common.Controls.MyComboBox()
        Me.btnUpdateRefNo = New Telerik.WinControls.UI.RadButton()
        Me.txtNEFTUploaderREFNo = New common.Controls.MyTextBox()
        Me.lblneftUploader = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtFinder()
        Me.gvTemp = New common.UserControls.MyRadGridView()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.radbtnBulkExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemVSP = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemBank = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnOtherBranch = New Telerik.WinControls.UI.RadButton()
        Me.BtnSameBranch = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnUploader = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItemMP = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlrtgsamt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlrtgsamt.SuspendLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnupdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvoiceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCreateNeftuploaderPlantWise.SuspendLayout()
        CType(Me.btnGoForPlantwise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDatePlant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDatePlant, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankCaption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFarmerType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboTypePaymentFarmer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNEFTUploaderREFNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblneftUploader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTemp.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOtherBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSameBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUploader, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.radbtnBulkExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnOtherBranch)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSameBranch)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUploader)
        Me.SplitContainer1.Size = New System.Drawing.Size(979, 698)
        Me.SplitContainer1.SplitterDistance = 658
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvTemp)
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Size = New System.Drawing.Size(979, 658)
        Me.SplitContainer2.SplitterDistance = 134
        Me.SplitContainer2.TabIndex = 222
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.pnlrtgsamt)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtVendorCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblToDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtInvoiceDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblInvDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer3.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtBankCode)
        Me.SplitContainer3.Panel2.Controls.Add(Me.pnlCreateNeftuploaderPlantWise)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblBankCaption)
        Me.SplitContainer3.Panel2.Controls.Add(Me.fndBank)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblFarmerType)
        Me.SplitContainer3.Panel2.Controls.Add(Me.CboTypePaymentFarmer)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnUpdateRefNo)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtNEFTUploaderREFNo)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblbankcode)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblneftUploader)
        Me.SplitContainer3.Panel2.Controls.Add(Me.fndDocNo)
        Me.SplitContainer3.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer3.Size = New System.Drawing.Size(979, 134)
        Me.SplitContainer3.SplitterDistance = 77
        Me.SplitContainer3.TabIndex = 374
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(636, 28)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(65, 20)
        Me.btnGo.TabIndex = 378
        Me.btnGo.Text = ">>>>"
        '
        'pnlrtgsamt
        '
        Me.pnlrtgsamt.Controls.Add(Me.RadLabel1)
        Me.pnlrtgsamt.Controls.Add(Me.btnupdate)
        Me.pnlrtgsamt.Controls.Add(Me.txtAmt)
        Me.pnlrtgsamt.Location = New System.Drawing.Point(7, 52)
        Me.pnlrtgsamt.Name = "pnlrtgsamt"
        Me.pnlrtgsamt.Size = New System.Drawing.Size(964, 24)
        Me.pnlrtgsamt.TabIndex = 377
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(163, 18)
        Me.RadLabel1.TabIndex = 348
        Me.RadLabel1.Text = "Apply RTGS Amount More than"
        '
        'btnupdate
        '
        Me.btnupdate.Location = New System.Drawing.Point(253, 3)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(65, 20)
        Me.btnupdate.TabIndex = 347
        Me.btnupdate.Text = ">>>>"
        '
        'txtAmt
        '
        Me.txtAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAmt.CalculationExpression = Nothing
        Me.txtAmt.DecimalPlaces = 0
        Me.txtAmt.FieldCode = Nothing
        Me.txtAmt.FieldDesc = Nothing
        Me.txtAmt.FieldMaxLength = 0
        Me.txtAmt.FieldName = Nothing
        Me.txtAmt.isCalculatedField = False
        Me.txtAmt.IsSourceFromTable = False
        Me.txtAmt.IsSourceFromValueList = False
        Me.txtAmt.IsUnique = False
        Me.txtAmt.Location = New System.Drawing.Point(175, 3)
        Me.txtAmt.MendatroryField = False
        Me.txtAmt.MyLinkLable1 = Nothing
        Me.txtAmt.MyLinkLable2 = Nothing
        Me.txtAmt.Name = "txtAmt"
        Me.txtAmt.ReferenceFieldDesc = Nothing
        Me.txtAmt.ReferenceFieldName = Nothing
        Me.txtAmt.ReferenceTableName = Nothing
        Me.txtAmt.Size = New System.Drawing.Size(72, 20)
        Me.txtAmt.TabIndex = 346
        Me.txtAmt.Text = "0"
        Me.txtAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmt.Value = 0R
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(734, 7)
        Me.txtToDate.MendatroryField = True
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.lblToDate
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 18)
        Me.txtToDate.TabIndex = 372
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "03/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(636, 8)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(86, 16)
        Me.lblToDate.TabIndex = 373
        Me.lblToDate.Text = "Invoice To Date"
        '
        'txtVendorCode
        '
        Me.txtVendorCode.arrDispalyMember = Nothing
        Me.txtVendorCode.arrValueMember = Nothing
        Me.txtVendorCode.Location = New System.Drawing.Point(158, 29)
        Me.txtVendorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.MyLinkLable1 = Me.lblVendorCode
        Me.txtVendorCode.MyLinkLable2 = Nothing
        Me.txtVendorCode.MyNullText = "Please Select..."
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.Size = New System.Drawing.Size(470, 19)
        Me.txtVendorCode.TabIndex = 375
        '
        'lblVendorCode
        '
        Me.lblVendorCode.FieldName = Nothing
        Me.lblVendorCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorCode.Location = New System.Drawing.Point(6, 27)
        Me.lblVendorCode.Name = "lblVendorCode"
        Me.lblVendorCode.Size = New System.Drawing.Size(72, 18)
        Me.lblVendorCode.TabIndex = 376
        Me.lblVendorCode.Text = "Vendor Code"
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.CalculationExpression = Nothing
        Me.txtInvoiceDate.CustomFormat = "dd/MM/yyyy"
        Me.txtInvoiceDate.FieldCode = Nothing
        Me.txtInvoiceDate.FieldDesc = Nothing
        Me.txtInvoiceDate.FieldMaxLength = 0
        Me.txtInvoiceDate.FieldName = Nothing
        Me.txtInvoiceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInvoiceDate.isCalculatedField = False
        Me.txtInvoiceDate.IsSourceFromTable = False
        Me.txtInvoiceDate.IsSourceFromValueList = False
        Me.txtInvoiceDate.IsUnique = False
        Me.txtInvoiceDate.Location = New System.Drawing.Point(547, 7)
        Me.txtInvoiceDate.MendatroryField = True
        Me.txtInvoiceDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvoiceDate.MyLinkLable1 = Me.lblInvDate
        Me.txtInvoiceDate.MyLinkLable2 = Nothing
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvoiceDate.ReferenceFieldDesc = Nothing
        Me.txtInvoiceDate.ReferenceFieldName = Nothing
        Me.txtInvoiceDate.ReferenceTableName = Nothing
        Me.txtInvoiceDate.Size = New System.Drawing.Size(82, 18)
        Me.txtInvoiceDate.TabIndex = 369
        Me.txtInvoiceDate.TabStop = False
        Me.txtInvoiceDate.Text = "03/05/2011"
        Me.txtInvoiceDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblInvDate
        '
        Me.lblInvDate.FieldName = Nothing
        Me.lblInvDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvDate.Location = New System.Drawing.Point(445, 8)
        Me.lblInvDate.Name = "lblInvDate"
        Me.lblInvDate.Size = New System.Drawing.Size(95, 16)
        Me.lblInvDate.TabIndex = 370
        Me.lblInvDate.Text = "Invoice from Date"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 5)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel1.TabIndex = 286
        Me.MyLabel1.Text = "Type"
        '
        'cboType
        '
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        Me.cboType.Location = New System.Drawing.Point(158, 6)
        Me.cboType.MendatroryField = True
        Me.cboType.MyLinkLable1 = Me.MyLabel1
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(281, 20)
        Me.cboType.TabIndex = 371
        '
        'txtBankCode
        '
        Me.txtBankCode.AutoSize = False
        Me.txtBankCode.BorderVisible = True
        Me.txtBankCode.FieldName = Nothing
        Me.txtBankCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.Location = New System.Drawing.Point(319, 27)
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(225, 18)
        Me.txtBankCode.TabIndex = 376
        Me.txtBankCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtBankCode.TextWrap = False
        '
        'pnlCreateNeftuploaderPlantWise
        '
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.btnGoForPlantwise)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.MyLabel4)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.MyLabel5)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.txtToDatePlant)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.txtFromDatePlant)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.MyLabel3)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.txtMonth)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.MyLabel2)
        Me.pnlCreateNeftuploaderPlantWise.Controls.Add(Me.fndPlantCode)
        Me.pnlCreateNeftuploaderPlantWise.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCreateNeftuploaderPlantWise.Location = New System.Drawing.Point(2, 2)
        Me.pnlCreateNeftuploaderPlantWise.Name = "pnlCreateNeftuploaderPlantWise"
        Me.pnlCreateNeftuploaderPlantWise.Size = New System.Drawing.Size(975, 49)
        Me.pnlCreateNeftuploaderPlantWise.TabIndex = 1
        '
        'btnGoForPlantwise
        '
        Me.btnGoForPlantwise.Location = New System.Drawing.Point(357, 41)
        Me.btnGoForPlantwise.Name = "btnGoForPlantwise"
        Me.btnGoForPlantwise.Size = New System.Drawing.Size(82, 20)
        Me.btnGoForPlantwise.TabIndex = 379
        Me.btnGoForPlantwise.Text = ">>>>"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(200, 43)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel4.TabIndex = 294
        Me.MyLabel4.Text = "To Date"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(9, 43)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel5.TabIndex = 295
        Me.MyLabel5.Text = "From Date"
        '
        'txtToDatePlant
        '
        Me.txtToDatePlant.CalculationExpression = Nothing
        Me.txtToDatePlant.CustomFormat = "dd - MMM - yyyy"
        Me.txtToDatePlant.FieldCode = Nothing
        Me.txtToDatePlant.FieldDesc = Nothing
        Me.txtToDatePlant.FieldMaxLength = 0
        Me.txtToDatePlant.FieldName = Nothing
        Me.txtToDatePlant.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDatePlant.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDatePlant.isCalculatedField = False
        Me.txtToDatePlant.IsSourceFromTable = False
        Me.txtToDatePlant.IsSourceFromValueList = False
        Me.txtToDatePlant.IsUnique = False
        Me.txtToDatePlant.Location = New System.Drawing.Point(248, 42)
        Me.txtToDatePlant.MendatroryField = True
        Me.txtToDatePlant.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDatePlant.MyLinkLable1 = Me.MyLabel4
        Me.txtToDatePlant.MyLinkLable2 = Nothing
        Me.txtToDatePlant.Name = "txtToDatePlant"
        Me.txtToDatePlant.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDatePlant.ReadOnly = True
        Me.txtToDatePlant.ReferenceFieldDesc = Nothing
        Me.txtToDatePlant.ReferenceFieldName = Nothing
        Me.txtToDatePlant.ReferenceTableName = Nothing
        Me.txtToDatePlant.Size = New System.Drawing.Size(103, 18)
        Me.txtToDatePlant.TabIndex = 293
        Me.txtToDatePlant.TabStop = False
        Me.txtToDatePlant.Text = "29 - Sep - 2014"
        Me.txtToDatePlant.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'txtFromDatePlant
        '
        Me.txtFromDatePlant.CalculationExpression = Nothing
        Me.txtFromDatePlant.CustomFormat = "dd - MMM - yyyy"
        Me.txtFromDatePlant.FieldCode = Nothing
        Me.txtFromDatePlant.FieldDesc = Nothing
        Me.txtFromDatePlant.FieldMaxLength = 0
        Me.txtFromDatePlant.FieldName = Nothing
        Me.txtFromDatePlant.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDatePlant.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDatePlant.isCalculatedField = False
        Me.txtFromDatePlant.IsSourceFromTable = False
        Me.txtFromDatePlant.IsSourceFromValueList = False
        Me.txtFromDatePlant.IsUnique = False
        Me.txtFromDatePlant.Location = New System.Drawing.Point(77, 42)
        Me.txtFromDatePlant.MendatroryField = True
        Me.txtFromDatePlant.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDatePlant.MyLinkLable1 = Me.MyLabel5
        Me.txtFromDatePlant.MyLinkLable2 = Nothing
        Me.txtFromDatePlant.Name = "txtFromDatePlant"
        Me.txtFromDatePlant.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDatePlant.ReferenceFieldDesc = Nothing
        Me.txtFromDatePlant.ReferenceFieldName = Nothing
        Me.txtFromDatePlant.ReferenceTableName = Nothing
        Me.txtFromDatePlant.Size = New System.Drawing.Size(101, 18)
        Me.txtFromDatePlant.TabIndex = 292
        Me.txtFromDatePlant.TabStop = False
        Me.txtFromDatePlant.Text = "29 - Sep - 2014"
        Me.txtFromDatePlant.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 23)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel3.TabIndex = 291
        Me.MyLabel3.Text = "Month"
        '
        'txtMonth
        '
        Me.txtMonth.CalculationExpression = Nothing
        Me.txtMonth.CustomFormat = "MMM - yyyy"
        Me.txtMonth.FieldCode = Nothing
        Me.txtMonth.FieldDesc = Nothing
        Me.txtMonth.FieldMaxLength = 0
        Me.txtMonth.FieldName = Nothing
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMonth.isCalculatedField = False
        Me.txtMonth.IsSourceFromTable = False
        Me.txtMonth.IsSourceFromValueList = False
        Me.txtMonth.IsUnique = False
        Me.txtMonth.Location = New System.Drawing.Point(77, 22)
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Me.MyLabel3
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.Size = New System.Drawing.Size(101, 18)
        Me.txtMonth.TabIndex = 290
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Sep - 2014"
        Me.txtMonth.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 3)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel2.TabIndex = 289
        Me.MyLabel2.Text = "Plant Code"
        '
        'fndPlantCode
        '
        Me.fndPlantCode.CalculationExpression = Nothing
        Me.fndPlantCode.FieldCode = Nothing
        Me.fndPlantCode.FieldDesc = Nothing
        Me.fndPlantCode.FieldMaxLength = 0
        Me.fndPlantCode.FieldName = Nothing
        Me.fndPlantCode.isCalculatedField = False
        Me.fndPlantCode.IsSourceFromTable = False
        Me.fndPlantCode.IsSourceFromValueList = False
        Me.fndPlantCode.IsUnique = False
        Me.fndPlantCode.Location = New System.Drawing.Point(77, 2)
        Me.fndPlantCode.MendatroryField = True
        Me.fndPlantCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPlantCode.MyLinkLable1 = Me.MyLabel2
        Me.fndPlantCode.MyLinkLable2 = Nothing
        Me.fndPlantCode.MyReadOnly = False
        Me.fndPlantCode.MyShowMasterFormButton = False
        Me.fndPlantCode.Name = "fndPlantCode"
        Me.fndPlantCode.ReferenceFieldDesc = Nothing
        Me.fndPlantCode.ReferenceFieldName = Nothing
        Me.fndPlantCode.ReferenceTableName = Nothing
        Me.fndPlantCode.Size = New System.Drawing.Size(362, 19)
        Me.fndPlantCode.TabIndex = 288
        Me.fndPlantCode.Value = ""
        '
        'lblBankCaption
        '
        Me.lblBankCaption.FieldName = Nothing
        Me.lblBankCaption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankCaption.Location = New System.Drawing.Point(12, 28)
        Me.lblBankCaption.Name = "lblBankCaption"
        Me.lblBankCaption.Size = New System.Drawing.Size(35, 16)
        Me.lblBankCaption.TabIndex = 375
        Me.lblBankCaption.Text = "Bank "
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
        Me.fndBank.Location = New System.Drawing.Point(182, 27)
        Me.fndBank.MendatroryField = True
        Me.fndBank.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBank.MyLinkLable1 = Me.lblbankcode
        Me.fndBank.MyLinkLable2 = Nothing
        Me.fndBank.MyReadOnly = False
        Me.fndBank.MyShowMasterFormButton = False
        Me.fndBank.Name = "fndBank"
        Me.fndBank.ReferenceFieldDesc = Nothing
        Me.fndBank.ReferenceFieldName = Nothing
        Me.fndBank.ReferenceTableName = Nothing
        Me.fndBank.Size = New System.Drawing.Size(131, 19)
        Me.fndBank.TabIndex = 374
        Me.fndBank.Value = ""
        '
        'lblbankcode
        '
        Me.lblbankcode.FieldName = Nothing
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.Location = New System.Drawing.Point(12, 5)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.Size = New System.Drawing.Size(139, 16)
        Me.lblbankcode.TabIndex = 287
        Me.lblbankcode.Text = "Payment Process Doc No."
        '
        'lblFarmerType
        '
        Me.lblFarmerType.FieldName = Nothing
        Me.lblFarmerType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFarmerType.Location = New System.Drawing.Point(550, 28)
        Me.lblFarmerType.Name = "lblFarmerType"
        Me.lblFarmerType.Size = New System.Drawing.Size(31, 16)
        Me.lblFarmerType.TabIndex = 372
        Me.lblFarmerType.Text = "Type"
        Me.lblFarmerType.Visible = False
        '
        'CboTypePaymentFarmer
        '
        Me.CboTypePaymentFarmer.CalculationExpression = Nothing
        Me.CboTypePaymentFarmer.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboTypePaymentFarmer.FieldCode = Nothing
        Me.CboTypePaymentFarmer.FieldDesc = Nothing
        Me.CboTypePaymentFarmer.FieldMaxLength = 0
        Me.CboTypePaymentFarmer.FieldName = Nothing
        Me.CboTypePaymentFarmer.isCalculatedField = False
        Me.CboTypePaymentFarmer.IsSourceFromTable = False
        Me.CboTypePaymentFarmer.IsSourceFromValueList = False
        Me.CboTypePaymentFarmer.IsUnique = False
        Me.CboTypePaymentFarmer.Location = New System.Drawing.Point(680, 26)
        Me.CboTypePaymentFarmer.MendatroryField = True
        Me.CboTypePaymentFarmer.MyLinkLable1 = Me.lblFarmerType
        Me.CboTypePaymentFarmer.MyLinkLable2 = Nothing
        Me.CboTypePaymentFarmer.Name = "CboTypePaymentFarmer"
        Me.CboTypePaymentFarmer.ReferenceFieldDesc = Nothing
        Me.CboTypePaymentFarmer.ReferenceFieldName = Nothing
        Me.CboTypePaymentFarmer.ReferenceTableName = Nothing
        Me.CboTypePaymentFarmer.Size = New System.Drawing.Size(135, 20)
        Me.CboTypePaymentFarmer.TabIndex = 373
        Me.CboTypePaymentFarmer.Visible = False
        '
        'btnUpdateRefNo
        '
        Me.btnUpdateRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateRefNo.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnUpdateRefNo.Location = New System.Drawing.Point(892, 2)
        Me.btnUpdateRefNo.Name = "btnUpdateRefNo"
        Me.btnUpdateRefNo.Size = New System.Drawing.Size(85, 21)
        Me.btnUpdateRefNo.TabIndex = 290
        Me.btnUpdateRefNo.Text = "Update Ref No."
        '
        'txtNEFTUploaderREFNo
        '
        Me.txtNEFTUploaderREFNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtNEFTUploaderREFNo.CalculationExpression = Nothing
        Me.txtNEFTUploaderREFNo.FieldCode = Nothing
        Me.txtNEFTUploaderREFNo.FieldDesc = Nothing
        Me.txtNEFTUploaderREFNo.FieldMaxLength = 0
        Me.txtNEFTUploaderREFNo.FieldName = Nothing
        Me.txtNEFTUploaderREFNo.isCalculatedField = False
        Me.txtNEFTUploaderREFNo.IsSourceFromTable = False
        Me.txtNEFTUploaderREFNo.IsSourceFromValueList = False
        Me.txtNEFTUploaderREFNo.IsUnique = False
        Me.txtNEFTUploaderREFNo.Location = New System.Drawing.Point(680, 3)
        Me.txtNEFTUploaderREFNo.MendatroryField = False
        Me.txtNEFTUploaderREFNo.MyLinkLable1 = Me.lblneftUploader
        Me.txtNEFTUploaderREFNo.MyLinkLable2 = Nothing
        Me.txtNEFTUploaderREFNo.Name = "txtNEFTUploaderREFNo"
        Me.txtNEFTUploaderREFNo.ReferenceFieldDesc = Nothing
        Me.txtNEFTUploaderREFNo.ReferenceFieldName = Nothing
        Me.txtNEFTUploaderREFNo.ReferenceTableName = Nothing
        Me.txtNEFTUploaderREFNo.Size = New System.Drawing.Size(210, 20)
        Me.txtNEFTUploaderREFNo.TabIndex = 288
        '
        'lblneftUploader
        '
        Me.lblneftUploader.FieldName = Nothing
        Me.lblneftUploader.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblneftUploader.Location = New System.Drawing.Point(550, 5)
        Me.lblneftUploader.Name = "lblneftUploader"
        Me.lblneftUploader.Size = New System.Drawing.Size(125, 16)
        Me.lblneftUploader.TabIndex = 289
        Me.lblneftUploader.Text = "NEFT Uploder REF. No"
        '
        'fndDocNo
        '
        Me.fndDocNo.CalculationExpression = Nothing
        Me.fndDocNo.FieldCode = Nothing
        Me.fndDocNo.FieldDesc = Nothing
        Me.fndDocNo.FieldMaxLength = 0
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.isCalculatedField = False
        Me.fndDocNo.IsSourceFromTable = False
        Me.fndDocNo.IsSourceFromValueList = False
        Me.fndDocNo.IsUnique = False
        Me.fndDocNo.Location = New System.Drawing.Point(182, 6)
        Me.fndDocNo.MendatroryField = True
        Me.fndDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDocNo.MyLinkLable1 = Me.lblbankcode
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.MyShowMasterFormButton = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.ReferenceFieldDesc = Nothing
        Me.fndDocNo.ReferenceFieldName = Nothing
        Me.fndDocNo.ReferenceTableName = Nothing
        Me.fndDocNo.Size = New System.Drawing.Size(362, 19)
        Me.fndDocNo.TabIndex = 286
        Me.fndDocNo.Value = ""
        '
        'gvTemp
        '
        Me.gvTemp.Location = New System.Drawing.Point(182, 111)
        '
        'gvTemp
        '
        Me.gvTemp.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTemp.Name = "gvTemp"
        Me.gvTemp.ShowHeaderCellButtons = True
        Me.gvTemp.Size = New System.Drawing.Size(411, 180)
        Me.gvTemp.TabIndex = 2
        Me.gvTemp.Text = "RadGridView1"
        Me.gvTemp.Visible = False
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(2, 2)
        '
        'gv
        '
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(975, 516)
        Me.gv.TabIndex = 1
        Me.gv.Text = "RadGridView1"
        '
        'radbtnBulkExp
        '
        Me.radbtnBulkExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.radbtnBulkExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItemVSP, Me.RadMenuItemMP, Me.RadMenuItemBank})
        Me.radbtnBulkExp.Location = New System.Drawing.Point(445, 7)
        Me.radbtnBulkExp.Name = "radbtnBulkExp"
        Me.radbtnBulkExp.Size = New System.Drawing.Size(100, 22)
        Me.radbtnBulkExp.TabIndex = 161
        Me.radbtnBulkExp.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "PDF"
        Me.RadMenuItem1.AccessibleName = "PDF"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "PDF"
        '
        'RadMenuItemVSP
        '
        Me.RadMenuItemVSP.AccessibleDescription = "VSP Wise"
        Me.RadMenuItemVSP.AccessibleName = "VSP Wise"
        Me.RadMenuItemVSP.Name = "RadMenuItemVSP"
        Me.RadMenuItemVSP.Text = "Secretary Wise"
        '
        'RadMenuItemBank
        '
        Me.RadMenuItemBank.AccessibleDescription = "Bank Wise"
        Me.RadMenuItemBank.AccessibleName = "Bank Wise"
        Me.RadMenuItemBank.Name = "RadMenuItemBank"
        Me.RadMenuItemBank.Text = "Bank Wise"
        '
        'btnOtherBranch
        '
        Me.btnOtherBranch.Location = New System.Drawing.Point(249, 8)
        Me.btnOtherBranch.Name = "btnOtherBranch"
        Me.btnOtherBranch.Size = New System.Drawing.Size(113, 21)
        Me.btnOtherBranch.TabIndex = 7
        Me.btnOtherBranch.Text = "Other Branch"
        '
        'BtnSameBranch
        '
        Me.BtnSameBranch.Location = New System.Drawing.Point(129, 8)
        Me.BtnSameBranch.Name = "BtnSameBranch"
        Me.BtnSameBranch.Size = New System.Drawing.Size(113, 21)
        Me.BtnSameBranch.TabIndex = 6
        Me.BtnSameBranch.Text = "Same Branch"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnReset.Location = New System.Drawing.Point(368, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 21)
        Me.btnReset.TabIndex = 5
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(896, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(71, 21)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        '
        'btnUploader
        '
        Me.btnUploader.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUploader.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUploader.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnUploader.Location = New System.Drawing.Point(10, 8)
        Me.btnUploader.Name = "btnUploader"
        Me.btnUploader.Size = New System.Drawing.Size(113, 21)
        Me.btnUploader.TabIndex = 4
        Me.btnUploader.Text = "Create Uploader"
        '
        'RadMenuItemMP
        '
        Me.RadMenuItemMP.AccessibleDescription = "MP Wise"
        Me.RadMenuItemMP.AccessibleName = "MP Wise"
        Me.RadMenuItemMP.Name = "RadMenuItemMP"
        Me.RadMenuItemMP.Text = "MP Wise"
        '
        'FrmNEFTUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(979, 698)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmNEFTUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmNEFTUploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlrtgsamt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlrtgsamt.ResumeLayout(False)
        Me.pnlrtgsamt.PerformLayout()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnupdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvoiceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCreateNeftuploaderPlantWise.ResumeLayout(False)
        Me.pnlCreateNeftuploaderPlantWise.PerformLayout()
        CType(Me.btnGoForPlantwise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDatePlant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDatePlant, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankCaption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFarmerType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboTypePaymentFarmer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNEFTUploaderREFNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblneftUploader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTemp.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radbtnBulkExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOtherBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSameBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUploader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUploader As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtInvoiceDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblInvDate As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtVendorCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblVendorCode As common.Controls.MyLabel
    Friend WithEvents btnUpdateRefNo As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtNEFTUploaderREFNo As common.Controls.MyTextBox
    Friend WithEvents lblneftUploader As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtFinder
    Friend WithEvents lblbankcode As common.Controls.MyLabel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnupdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAmt As common.MyNumBox
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents pnlrtgsamt As Telerik.WinControls.UI.RadPanel
    Friend WithEvents btnOtherBranch As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnSameBranch As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvTemp As common.UserControls.MyRadGridView
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblFarmerType As common.Controls.MyLabel
    Friend WithEvents CboTypePaymentFarmer As common.Controls.MyComboBox
    Friend WithEvents lblBankCaption As common.Controls.MyLabel
    Friend WithEvents fndBank As common.UserControls.txtFinder
    Friend WithEvents txtBankCode As common.Controls.MyLabel
    Friend WithEvents radbtnBulkExp As RadSplitButton
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents pnlCreateNeftuploaderPlantWise As Panel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents fndPlantCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents btnGoForPlantwise As RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtToDatePlant As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDatePlant As common.Controls.MyDateTimePicker
    Friend WithEvents RadMenuItemVSP As RadMenuItem
    Friend WithEvents RadMenuItemBank As RadMenuItem
    Friend WithEvents RadMenuItemMP As RadMenuItem
End Class

