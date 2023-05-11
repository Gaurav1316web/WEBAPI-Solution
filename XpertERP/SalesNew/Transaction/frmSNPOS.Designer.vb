<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSNPOS
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
        Me.txtBarCode = New common.Controls.MyTextBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.lblShiping = New common.Controls.MyLabel()
        Me.txtShipping = New common.UserControls.txtFinder()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.btnCustomerMasterOpen = New Telerik.WinControls.UI.RadButton()
        Me.UsLock1 = New common.usLock()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.pnlCreditCard = New System.Windows.Forms.Panel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtApprovalCode = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtBatchNo = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtBankName = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtCreditCardNo = New common.Controls.MyTextBox()
        Me.pnlDebit = New System.Windows.Forms.Panel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.cboCardType = New common.Controls.MyComboBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtDebitCardNo = New common.Controls.MyTextBox()
        Me.pnlCheque = New System.Windows.Forms.Panel()
        Me.txtChequeDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtChequeNo = New common.Controls.MyTextBox()
        Me.lblAmtAfterTax = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.lblKMReading = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtfreight = New common.MyNumBox()
        Me.cboPaymentMode = New common.Controls.MyComboBox()
        Me.txtOtherCharges = New common.MyNumBox()
        Me.txtAmtPaid = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtAdvancePaid = New common.MyNumBox()
        Me.lblBalance = New common.Controls.MyLabel()
        Me.lblBalancePayment = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDeliveryDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.txtMessage = New common.Controls.MyTextBox()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel29 = New common.Controls.MyLabel()
        Me.cboDeliveryType = New common.Controls.MyComboBox()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel24 = New common.Controls.MyLabel()
        Me.txtReqNo = New common.UserControls.txtFinder()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadDropDownMenu()
        Me.RadThemeManager1 = New Telerik.WinControls.RadThemeManager()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShiping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCustomerMasterOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCreditCard.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApprovalCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDebit.SuspendLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCardType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDebitCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCheque.SuspendLayout()
        CType(Me.txtChequeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPaymentMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOtherCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmtPaid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdvancePaid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalancePayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDeliveryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMessage)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBarCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtBillToLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblShiping)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtShipping)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnCustomerMasterOpen)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel16)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDeliveryDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVendorNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel29)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboDeliveryType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblBillToLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel14)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadLabel24)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtReqNo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1061, 488)
        Me.SplitContainer1.SplitterDistance = 456
        Me.SplitContainer1.TabIndex = 0
        '
        'txtBarCode
        '
        Me.txtBarCode.CalculationExpression = Nothing
        Me.txtBarCode.FieldCode = Nothing
        Me.txtBarCode.FieldDesc = Nothing
        Me.txtBarCode.FieldMaxLength = 0
        Me.txtBarCode.FieldName = Nothing
        Me.txtBarCode.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCode.isCalculatedField = False
        Me.txtBarCode.IsSourceFromTable = False
        Me.txtBarCode.IsSourceFromValueList = False
        Me.txtBarCode.IsUnique = False
        Me.txtBarCode.Location = New System.Drawing.Point(72, 73)
        Me.txtBarCode.MaxLength = 200
        Me.txtBarCode.MendatroryField = False
        Me.txtBarCode.MyLinkLable1 = Me.MyLabel16
        Me.txtBarCode.MyLinkLable2 = Nothing
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.ReferenceFieldDesc = Nothing
        Me.txtBarCode.ReferenceFieldName = Nothing
        Me.txtBarCode.ReferenceTableName = Nothing
        Me.txtBarCode.Size = New System.Drawing.Size(497, 21)
        Me.txtBarCode.TabIndex = 13
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(3, 73)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(65, 19)
        Me.MyLabel16.TabIndex = 34
        Me.MyLabel16.Text = "Bar Code"
        '
        'txtBillToLocation
        '
        Me.txtBillToLocation.CalculationExpression = Nothing
        Me.txtBillToLocation.FieldCode = Nothing
        Me.txtBillToLocation.FieldDesc = Nothing
        Me.txtBillToLocation.FieldMaxLength = 0
        Me.txtBillToLocation.FieldName = Nothing
        Me.txtBillToLocation.isCalculatedField = False
        Me.txtBillToLocation.IsSourceFromTable = False
        Me.txtBillToLocation.IsSourceFromValueList = False
        Me.txtBillToLocation.IsUnique = False
        Me.txtBillToLocation.Location = New System.Drawing.Point(72, 51)
        Me.txtBillToLocation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBillToLocation.MendatroryField = False
        Me.txtBillToLocation.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillToLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtBillToLocation.MyLinkLable2 = Me.lblBillToLocation
        Me.txtBillToLocation.MyReadOnly = False
        Me.txtBillToLocation.MyShowMasterFormButton = False
        Me.txtBillToLocation.Name = "txtBillToLocation"
        Me.txtBillToLocation.ReferenceFieldDesc = Nothing
        Me.txtBillToLocation.ReferenceFieldName = Nothing
        Me.txtBillToLocation.ReferenceTableName = Nothing
        Me.txtBillToLocation.Size = New System.Drawing.Size(185, 21)
        Me.txtBillToLocation.TabIndex = 11
        Me.txtBillToLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(3, 52)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(50, 19)
        Me.RadLabel15.TabIndex = 29
        Me.RadLabel15.Text = "Branch"
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(259, 50)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(310, 22)
        Me.lblBillToLocation.TabIndex = 12
        Me.lblBillToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBillToLocation.TextWrap = False
        '
        'lblShiping
        '
        Me.lblShiping.AutoSize = False
        Me.lblShiping.BorderVisible = True
        Me.lblShiping.FieldName = Nothing
        Me.lblShiping.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShiping.Location = New System.Drawing.Point(672, 75)
        Me.lblShiping.Name = "lblShiping"
        Me.lblShiping.Size = New System.Drawing.Size(129, 22)
        Me.lblShiping.TabIndex = 10
        Me.lblShiping.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShiping.TextWrap = False
        Me.lblShiping.Visible = False
        '
        'txtShipping
        '
        Me.txtShipping.CalculationExpression = Nothing
        Me.txtShipping.FieldCode = Nothing
        Me.txtShipping.FieldDesc = Nothing
        Me.txtShipping.FieldMaxLength = 0
        Me.txtShipping.FieldName = Nothing
        Me.txtShipping.isCalculatedField = False
        Me.txtShipping.IsSourceFromTable = False
        Me.txtShipping.IsSourceFromValueList = False
        Me.txtShipping.IsUnique = False
        Me.txtShipping.Location = New System.Drawing.Point(745, 72)
        Me.txtShipping.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtShipping.MendatroryField = False
        Me.txtShipping.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipping.MyLinkLable1 = Me.MyLabel18
        Me.txtShipping.MyLinkLable2 = Me.lblShiping
        Me.txtShipping.MyReadOnly = False
        Me.txtShipping.MyShowMasterFormButton = False
        Me.txtShipping.Name = "txtShipping"
        Me.txtShipping.ReferenceFieldDesc = Nothing
        Me.txtShipping.ReferenceFieldName = Nothing
        Me.txtShipping.ReferenceTableName = Nothing
        Me.txtShipping.Size = New System.Drawing.Size(185, 20)
        Me.txtShipping.TabIndex = 9
        Me.txtShipping.Value = ""
        Me.txtShipping.Visible = False
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(673, 77)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(60, 19)
        Me.MyLabel18.TabIndex = 54
        Me.MyLabel18.Text = "Shipping"
        Me.MyLabel18.Visible = False
        '
        'btnCustomerMasterOpen
        '
        Me.btnCustomerMasterOpen.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerMasterOpen.Image = Global.ERP.My.Resources.Resources.Detail
        Me.btnCustomerMasterOpen.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCustomerMasterOpen.Location = New System.Drawing.Point(259, 28)
        Me.btnCustomerMasterOpen.Name = "btnCustomerMasterOpen"
        Me.btnCustomerMasterOpen.Size = New System.Drawing.Size(21, 23)
        Me.btnCustomerMasterOpen.TabIndex = 6
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(978, 6)
        Me.UsLock1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(76, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 26
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadGroupBox2.Controls.Add(Me.lblTaxAmt)
        Me.RadGroupBox2.Controls.Add(Me.lblTotRAmt)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel25)
        Me.RadGroupBox2.Controls.Add(Me.lblDiscountAmt)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel27)
        Me.RadGroupBox2.Controls.Add(Me.pnlCreditCard)
        Me.RadGroupBox2.Controls.Add(Me.pnlDebit)
        Me.RadGroupBox2.Controls.Add(Me.pnlCheque)
        Me.RadGroupBox2.Controls.Add(Me.lblAmtAfterTax)
        Me.RadGroupBox2.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel9)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel32)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel22)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel19)
        Me.RadGroupBox2.Controls.Add(Me.lblKMReading)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.txtfreight)
        Me.RadGroupBox2.Controls.Add(Me.cboPaymentMode)
        Me.RadGroupBox2.Controls.Add(Me.txtOtherCharges)
        Me.RadGroupBox2.Controls.Add(Me.txtAmtPaid)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox2.Controls.Add(Me.txtAdvancePaid)
        Me.RadGroupBox2.Controls.Add(Me.lblBalance)
        Me.RadGroupBox2.Controls.Add(Me.lblBalancePayment)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(487, 236)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(570, 212)
        Me.RadGroupBox2.TabIndex = 17
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(12, 5)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(99, 19)
        Me.MyLabel17.TabIndex = 0
        Me.MyLabel17.Text = "Payment Mode"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 5)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(99, 19)
        Me.MyLabel5.TabIndex = 157
        Me.MyLabel5.Text = "Payment Mode"
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(136, 72)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(156, 23)
        Me.lblAmtAfterDiscount.TabIndex = 3
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(136, 95)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(156, 23)
        Me.lblTaxAmt.TabIndex = 4
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(135, 187)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(156, 23)
        Me.lblTotRAmt.TabIndex = 8
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(12, 97)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(102, 19)
        Me.RadLabel25.TabIndex = 24
        Me.RadLabel25.Text = "Tax Amount (+)"
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(136, 49)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(156, 23)
        Me.lblDiscountAmt.TabIndex = 2
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(12, 187)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(121, 19)
        Me.RadLabel27.TabIndex = 19
        Me.RadLabel27.Text = "Document Amount"
        '
        'pnlCreditCard
        '
        Me.pnlCreditCard.Controls.Add(Me.MyLabel13)
        Me.pnlCreditCard.Controls.Add(Me.txtApprovalCode)
        Me.pnlCreditCard.Controls.Add(Me.MyLabel12)
        Me.pnlCreditCard.Controls.Add(Me.txtBatchNo)
        Me.pnlCreditCard.Controls.Add(Me.MyLabel11)
        Me.pnlCreditCard.Controls.Add(Me.txtBankName)
        Me.pnlCreditCard.Controls.Add(Me.MyLabel10)
        Me.pnlCreditCard.Controls.Add(Me.txtCreditCardNo)
        Me.pnlCreditCard.Location = New System.Drawing.Point(293, 3)
        Me.pnlCreditCard.Name = "pnlCreditCard"
        Me.pnlCreditCard.Size = New System.Drawing.Size(273, 115)
        Me.pnlCreditCard.TabIndex = 9
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(3, 72)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(98, 19)
        Me.MyLabel13.TabIndex = 40
        Me.MyLabel13.Text = "Approval Code"
        '
        'txtApprovalCode
        '
        Me.txtApprovalCode.CalculationExpression = Nothing
        Me.txtApprovalCode.FieldCode = Nothing
        Me.txtApprovalCode.FieldDesc = Nothing
        Me.txtApprovalCode.FieldMaxLength = 0
        Me.txtApprovalCode.FieldName = Nothing
        Me.txtApprovalCode.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApprovalCode.isCalculatedField = False
        Me.txtApprovalCode.IsSourceFromTable = False
        Me.txtApprovalCode.IsSourceFromValueList = False
        Me.txtApprovalCode.IsUnique = False
        Me.txtApprovalCode.Location = New System.Drawing.Point(111, 71)
        Me.txtApprovalCode.MaxLength = 200
        Me.txtApprovalCode.MendatroryField = False
        Me.txtApprovalCode.MyLinkLable1 = Me.MyLabel13
        Me.txtApprovalCode.MyLinkLable2 = Nothing
        Me.txtApprovalCode.Name = "txtApprovalCode"
        Me.txtApprovalCode.ReferenceFieldDesc = Nothing
        Me.txtApprovalCode.ReferenceFieldName = Nothing
        Me.txtApprovalCode.ReferenceTableName = Nothing
        Me.txtApprovalCode.Size = New System.Drawing.Size(156, 21)
        Me.txtApprovalCode.TabIndex = 3
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(3, 49)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(63, 19)
        Me.MyLabel12.TabIndex = 40
        Me.MyLabel12.Text = "Batch No"
        '
        'txtBatchNo
        '
        Me.txtBatchNo.CalculationExpression = Nothing
        Me.txtBatchNo.FieldCode = Nothing
        Me.txtBatchNo.FieldDesc = Nothing
        Me.txtBatchNo.FieldMaxLength = 0
        Me.txtBatchNo.FieldName = Nothing
        Me.txtBatchNo.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.isCalculatedField = False
        Me.txtBatchNo.IsSourceFromTable = False
        Me.txtBatchNo.IsSourceFromValueList = False
        Me.txtBatchNo.IsUnique = False
        Me.txtBatchNo.Location = New System.Drawing.Point(111, 48)
        Me.txtBatchNo.MaxLength = 200
        Me.txtBatchNo.MendatroryField = False
        Me.txtBatchNo.MyLinkLable1 = Me.MyLabel12
        Me.txtBatchNo.MyLinkLable2 = Nothing
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.ReferenceFieldDesc = Nothing
        Me.txtBatchNo.ReferenceFieldName = Nothing
        Me.txtBatchNo.ReferenceTableName = Nothing
        Me.txtBatchNo.Size = New System.Drawing.Size(156, 21)
        Me.txtBatchNo.TabIndex = 2
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(3, 26)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(79, 19)
        Me.MyLabel11.TabIndex = 38
        Me.MyLabel11.Text = "Bank Name"
        '
        'txtBankName
        '
        Me.txtBankName.CalculationExpression = Nothing
        Me.txtBankName.FieldCode = Nothing
        Me.txtBankName.FieldDesc = Nothing
        Me.txtBankName.FieldMaxLength = 0
        Me.txtBankName.FieldName = Nothing
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.isCalculatedField = False
        Me.txtBankName.IsSourceFromTable = False
        Me.txtBankName.IsSourceFromValueList = False
        Me.txtBankName.IsUnique = False
        Me.txtBankName.Location = New System.Drawing.Point(111, 25)
        Me.txtBankName.MaxLength = 200
        Me.txtBankName.MendatroryField = False
        Me.txtBankName.MyLinkLable1 = Me.MyLabel11
        Me.txtBankName.MyLinkLable2 = Nothing
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReferenceFieldDesc = Nothing
        Me.txtBankName.ReferenceFieldName = Nothing
        Me.txtBankName.ReferenceTableName = Nothing
        Me.txtBankName.Size = New System.Drawing.Size(156, 21)
        Me.txtBankName.TabIndex = 1
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(3, 4)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(99, 19)
        Me.MyLabel10.TabIndex = 0
        Me.MyLabel10.Text = "Credit Card No"
        '
        'txtCreditCardNo
        '
        Me.txtCreditCardNo.CalculationExpression = Nothing
        Me.txtCreditCardNo.FieldCode = Nothing
        Me.txtCreditCardNo.FieldDesc = Nothing
        Me.txtCreditCardNo.FieldMaxLength = 0
        Me.txtCreditCardNo.FieldName = Nothing
        Me.txtCreditCardNo.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditCardNo.isCalculatedField = False
        Me.txtCreditCardNo.IsSourceFromTable = False
        Me.txtCreditCardNo.IsSourceFromValueList = False
        Me.txtCreditCardNo.IsUnique = False
        Me.txtCreditCardNo.Location = New System.Drawing.Point(111, 3)
        Me.txtCreditCardNo.MaxLength = 200
        Me.txtCreditCardNo.MendatroryField = False
        Me.txtCreditCardNo.MyLinkLable1 = Me.MyLabel10
        Me.txtCreditCardNo.MyLinkLable2 = Nothing
        Me.txtCreditCardNo.Name = "txtCreditCardNo"
        Me.txtCreditCardNo.ReferenceFieldDesc = Nothing
        Me.txtCreditCardNo.ReferenceFieldName = Nothing
        Me.txtCreditCardNo.ReferenceTableName = Nothing
        Me.txtCreditCardNo.Size = New System.Drawing.Size(156, 21)
        Me.txtCreditCardNo.TabIndex = 0
        '
        'pnlDebit
        '
        Me.pnlDebit.Controls.Add(Me.MyLabel14)
        Me.pnlDebit.Controls.Add(Me.cboCardType)
        Me.pnlDebit.Controls.Add(Me.MyLabel15)
        Me.pnlDebit.Controls.Add(Me.txtDebitCardNo)
        Me.pnlDebit.Location = New System.Drawing.Point(293, 6)
        Me.pnlDebit.Name = "pnlDebit"
        Me.pnlDebit.Size = New System.Drawing.Size(273, 115)
        Me.pnlDebit.TabIndex = 3
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(5, 28)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(71, 19)
        Me.MyLabel14.TabIndex = 159
        Me.MyLabel14.Text = "Card Type"
        '
        'cboCardType
        '
        Me.cboCardType.CalculationExpression = Nothing
        Me.cboCardType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboCardType.FieldCode = Nothing
        Me.cboCardType.FieldDesc = Nothing
        Me.cboCardType.FieldMaxLength = 0
        Me.cboCardType.FieldName = Nothing
        Me.cboCardType.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCardType.isCalculatedField = False
        Me.cboCardType.IsSourceFromTable = False
        Me.cboCardType.IsSourceFromValueList = False
        Me.cboCardType.IsUnique = False
        Me.cboCardType.Location = New System.Drawing.Point(111, 26)
        Me.cboCardType.MendatroryField = True
        Me.cboCardType.MyLinkLable1 = Me.MyLabel14
        Me.cboCardType.MyLinkLable2 = Nothing
        Me.cboCardType.Name = "cboCardType"
        Me.cboCardType.ReferenceFieldDesc = Nothing
        Me.cboCardType.ReferenceFieldName = Nothing
        Me.cboCardType.ReferenceTableName = Nothing
        Me.cboCardType.Size = New System.Drawing.Size(156, 23)
        Me.cboCardType.TabIndex = 0
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(5, 4)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(94, 19)
        Me.MyLabel15.TabIndex = 40
        Me.MyLabel15.Text = "Debit Card No"
        '
        'txtDebitCardNo
        '
        Me.txtDebitCardNo.CalculationExpression = Nothing
        Me.txtDebitCardNo.FieldCode = Nothing
        Me.txtDebitCardNo.FieldDesc = Nothing
        Me.txtDebitCardNo.FieldMaxLength = 0
        Me.txtDebitCardNo.FieldName = Nothing
        Me.txtDebitCardNo.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebitCardNo.isCalculatedField = False
        Me.txtDebitCardNo.IsSourceFromTable = False
        Me.txtDebitCardNo.IsSourceFromValueList = False
        Me.txtDebitCardNo.IsUnique = False
        Me.txtDebitCardNo.Location = New System.Drawing.Point(111, 1)
        Me.txtDebitCardNo.MaxLength = 200
        Me.txtDebitCardNo.MendatroryField = False
        Me.txtDebitCardNo.MyLinkLable1 = Me.MyLabel15
        Me.txtDebitCardNo.MyLinkLable2 = Nothing
        Me.txtDebitCardNo.Name = "txtDebitCardNo"
        Me.txtDebitCardNo.ReferenceFieldDesc = Nothing
        Me.txtDebitCardNo.ReferenceFieldName = Nothing
        Me.txtDebitCardNo.ReferenceTableName = Nothing
        Me.txtDebitCardNo.Size = New System.Drawing.Size(156, 21)
        Me.txtDebitCardNo.TabIndex = 0
        '
        'pnlCheque
        '
        Me.pnlCheque.Controls.Add(Me.txtChequeDate)
        Me.pnlCheque.Controls.Add(Me.MyLabel8)
        Me.pnlCheque.Controls.Add(Me.MyLabel7)
        Me.pnlCheque.Controls.Add(Me.txtChequeNo)
        Me.pnlCheque.Location = New System.Drawing.Point(293, 3)
        Me.pnlCheque.Name = "pnlCheque"
        Me.pnlCheque.Size = New System.Drawing.Size(273, 115)
        Me.pnlCheque.TabIndex = 3
        '
        'txtChequeDate
        '
        Me.txtChequeDate.CalculationExpression = Nothing
        Me.txtChequeDate.CustomFormat = "dd/MM/yyyy"
        Me.txtChequeDate.FieldCode = Nothing
        Me.txtChequeDate.FieldDesc = Nothing
        Me.txtChequeDate.FieldMaxLength = 0
        Me.txtChequeDate.FieldName = Nothing
        Me.txtChequeDate.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtChequeDate.isCalculatedField = False
        Me.txtChequeDate.IsSourceFromTable = False
        Me.txtChequeDate.IsSourceFromValueList = False
        Me.txtChequeDate.IsUnique = False
        Me.txtChequeDate.Location = New System.Drawing.Point(114, 24)
        Me.txtChequeDate.MendatroryField = False
        Me.txtChequeDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtChequeDate.MyLinkLable1 = Me.MyLabel8
        Me.txtChequeDate.MyLinkLable2 = Nothing
        Me.txtChequeDate.Name = "txtChequeDate"
        Me.txtChequeDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtChequeDate.ReferenceFieldDesc = Nothing
        Me.txtChequeDate.ReferenceFieldName = Nothing
        Me.txtChequeDate.ReferenceTableName = Nothing
        Me.txtChequeDate.Size = New System.Drawing.Size(156, 21)
        Me.txtChequeDate.TabIndex = 0
        Me.txtChequeDate.TabStop = False
        Me.txtChequeDate.Text = "13/06/2011"
        Me.txtChequeDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(3, 25)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(88, 19)
        Me.MyLabel8.TabIndex = 52
        Me.MyLabel8.Text = "Cheque Date"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 3)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(76, 19)
        Me.MyLabel7.TabIndex = 34
        Me.MyLabel7.Text = "Cheque No"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CalculationExpression = Nothing
        Me.txtChequeNo.FieldCode = Nothing
        Me.txtChequeNo.FieldDesc = Nothing
        Me.txtChequeNo.FieldMaxLength = 0
        Me.txtChequeNo.FieldName = Nothing
        Me.txtChequeNo.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChequeNo.isCalculatedField = False
        Me.txtChequeNo.IsSourceFromTable = False
        Me.txtChequeNo.IsSourceFromValueList = False
        Me.txtChequeNo.IsUnique = False
        Me.txtChequeNo.Location = New System.Drawing.Point(113, 2)
        Me.txtChequeNo.MaxLength = 200
        Me.txtChequeNo.MendatroryField = False
        Me.txtChequeNo.MyLinkLable1 = Me.MyLabel7
        Me.txtChequeNo.MyLinkLable2 = Nothing
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.ReferenceFieldDesc = Nothing
        Me.txtChequeNo.ReferenceFieldName = Nothing
        Me.txtChequeNo.ReferenceTableName = Nothing
        Me.txtChequeNo.Size = New System.Drawing.Size(156, 21)
        Me.txtChequeNo.TabIndex = 0
        '
        'lblAmtAfterTax
        '
        Me.lblAmtAfterTax.AutoSize = False
        Me.lblAmtAfterTax.BorderVisible = True
        Me.lblAmtAfterTax.FieldName = Nothing
        Me.lblAmtAfterTax.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterTax.Location = New System.Drawing.Point(136, 118)
        Me.lblAmtAfterTax.Name = "lblAmtAfterTax"
        Me.lblAmtAfterTax.Size = New System.Drawing.Size(156, 23)
        Me.lblAmtAfterTax.TabIndex = 5
        Me.lblAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(136, 26)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(156, 23)
        Me.lblAmtWithDiscount.TabIndex = 1
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(12, 74)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(121, 19)
        Me.RadLabel9.TabIndex = 31
        Me.RadLabel9.Text = "Amt After Discount"
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(12, 120)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(91, 19)
        Me.RadLabel32.TabIndex = 25
        Me.RadLabel32.Text = "Amt After Tax"
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(12, 51)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(106, 19)
        Me.RadLabel22.TabIndex = 30
        Me.RadLabel22.Text = "Discount Amt (-)"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(12, 28)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(92, 19)
        Me.RadLabel19.TabIndex = 29
        Me.RadLabel19.Text = "Basic Amount"
        '
        'lblKMReading
        '
        Me.lblKMReading.FieldName = Nothing
        Me.lblKMReading.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKMReading.Location = New System.Drawing.Point(12, 143)
        Me.lblKMReading.Name = "lblKMReading"
        Me.lblKMReading.Size = New System.Drawing.Size(69, 18)
        Me.lblKMReading.TabIndex = 21
        Me.lblKMReading.Text = "Freight (+)"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 165)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(115, 18)
        Me.MyLabel2.TabIndex = 20
        Me.MyLabel2.Text = "Other Charges (+)"
        '
        'txtfreight
        '
        Me.txtfreight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtfreight.CalculationExpression = Nothing
        Me.txtfreight.DecimalPlaces = 2
        Me.txtfreight.FieldCode = Nothing
        Me.txtfreight.FieldDesc = Nothing
        Me.txtfreight.FieldMaxLength = 0
        Me.txtfreight.FieldName = Nothing
        Me.txtfreight.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfreight.isCalculatedField = False
        Me.txtfreight.IsSourceFromTable = False
        Me.txtfreight.IsSourceFromValueList = False
        Me.txtfreight.IsUnique = False
        Me.txtfreight.Location = New System.Drawing.Point(135, 141)
        Me.txtfreight.MendatroryField = True
        Me.txtfreight.MyLinkLable1 = Me.lblKMReading
        Me.txtfreight.MyLinkLable2 = Nothing
        Me.txtfreight.Name = "txtfreight"
        Me.txtfreight.ReferenceFieldDesc = Nothing
        Me.txtfreight.ReferenceFieldName = Nothing
        Me.txtfreight.ReferenceTableName = Nothing
        Me.txtfreight.Size = New System.Drawing.Size(156, 23)
        Me.txtfreight.TabIndex = 6
        Me.txtfreight.Text = "0"
        Me.txtfreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtfreight.Value = 0R
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.CalculationExpression = Nothing
        Me.cboPaymentMode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPaymentMode.FieldCode = Nothing
        Me.cboPaymentMode.FieldDesc = Nothing
        Me.cboPaymentMode.FieldMaxLength = 0
        Me.cboPaymentMode.FieldName = Nothing
        Me.cboPaymentMode.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.isCalculatedField = False
        Me.cboPaymentMode.IsSourceFromTable = False
        Me.cboPaymentMode.IsSourceFromValueList = False
        Me.cboPaymentMode.IsUnique = False
        Me.cboPaymentMode.Location = New System.Drawing.Point(136, 3)
        Me.cboPaymentMode.MendatroryField = True
        Me.cboPaymentMode.MyLinkLable1 = Me.MyLabel5
        Me.cboPaymentMode.MyLinkLable2 = Nothing
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.ReferenceFieldDesc = Nothing
        Me.cboPaymentMode.ReferenceFieldName = Nothing
        Me.cboPaymentMode.ReferenceTableName = Nothing
        Me.cboPaymentMode.Size = New System.Drawing.Size(156, 23)
        Me.cboPaymentMode.TabIndex = 0
        '
        'txtOtherCharges
        '
        Me.txtOtherCharges.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtOtherCharges.CalculationExpression = Nothing
        Me.txtOtherCharges.DecimalPlaces = 2
        Me.txtOtherCharges.FieldCode = Nothing
        Me.txtOtherCharges.FieldDesc = Nothing
        Me.txtOtherCharges.FieldMaxLength = 0
        Me.txtOtherCharges.FieldName = Nothing
        Me.txtOtherCharges.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherCharges.isCalculatedField = False
        Me.txtOtherCharges.IsSourceFromTable = False
        Me.txtOtherCharges.IsSourceFromValueList = False
        Me.txtOtherCharges.IsUnique = False
        Me.txtOtherCharges.Location = New System.Drawing.Point(135, 164)
        Me.txtOtherCharges.MendatroryField = True
        Me.txtOtherCharges.MyLinkLable1 = Me.MyLabel2
        Me.txtOtherCharges.MyLinkLable2 = Nothing
        Me.txtOtherCharges.Name = "txtOtherCharges"
        Me.txtOtherCharges.ReferenceFieldDesc = Nothing
        Me.txtOtherCharges.ReferenceFieldName = Nothing
        Me.txtOtherCharges.ReferenceTableName = Nothing
        Me.txtOtherCharges.Size = New System.Drawing.Size(156, 23)
        Me.txtOtherCharges.TabIndex = 7
        Me.txtOtherCharges.Text = "0"
        Me.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOtherCharges.Value = 0R
        '
        'txtAmtPaid
        '
        Me.txtAmtPaid.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAmtPaid.CalculationExpression = Nothing
        Me.txtAmtPaid.DecimalPlaces = 2
        Me.txtAmtPaid.FieldCode = Nothing
        Me.txtAmtPaid.FieldDesc = Nothing
        Me.txtAmtPaid.FieldMaxLength = 0
        Me.txtAmtPaid.FieldName = Nothing
        Me.txtAmtPaid.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmtPaid.isCalculatedField = False
        Me.txtAmtPaid.IsSourceFromTable = False
        Me.txtAmtPaid.IsSourceFromValueList = False
        Me.txtAmtPaid.IsUnique = False
        Me.txtAmtPaid.Location = New System.Drawing.Point(412, 164)
        Me.txtAmtPaid.MendatroryField = True
        Me.txtAmtPaid.MyLinkLable1 = Me.MyLabel3
        Me.txtAmtPaid.MyLinkLable2 = Nothing
        Me.txtAmtPaid.Name = "txtAmtPaid"
        Me.txtAmtPaid.ReferenceFieldDesc = Nothing
        Me.txtAmtPaid.ReferenceFieldName = Nothing
        Me.txtAmtPaid.ReferenceTableName = Nothing
        Me.txtAmtPaid.Size = New System.Drawing.Size(156, 23)
        Me.txtAmtPaid.TabIndex = 11
        Me.txtAmtPaid.Text = "0"
        Me.txtAmtPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmtPaid.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(296, 120)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(107, 18)
        Me.MyLabel3.TabIndex = 15
        Me.MyLabel3.Text = "Advance Paid (-)"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(296, 189)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(57, 19)
        Me.MyLabel9.TabIndex = 16
        Me.MyLabel9.Text = "Balance"
        '
        'txtAdvancePaid
        '
        Me.txtAdvancePaid.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtAdvancePaid.CalculationExpression = Nothing
        Me.txtAdvancePaid.DecimalPlaces = 2
        Me.txtAdvancePaid.FieldCode = Nothing
        Me.txtAdvancePaid.FieldDesc = Nothing
        Me.txtAdvancePaid.FieldMaxLength = 0
        Me.txtAdvancePaid.FieldName = Nothing
        Me.txtAdvancePaid.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvancePaid.isCalculatedField = False
        Me.txtAdvancePaid.IsSourceFromTable = False
        Me.txtAdvancePaid.IsSourceFromValueList = False
        Me.txtAdvancePaid.IsUnique = False
        Me.txtAdvancePaid.Location = New System.Drawing.Point(412, 118)
        Me.txtAdvancePaid.MendatroryField = True
        Me.txtAdvancePaid.MyLinkLable1 = Me.MyLabel3
        Me.txtAdvancePaid.MyLinkLable2 = Nothing
        Me.txtAdvancePaid.Name = "txtAdvancePaid"
        Me.txtAdvancePaid.ReferenceFieldDesc = Nothing
        Me.txtAdvancePaid.ReferenceFieldName = Nothing
        Me.txtAdvancePaid.ReferenceTableName = Nothing
        Me.txtAdvancePaid.Size = New System.Drawing.Size(156, 23)
        Me.txtAdvancePaid.TabIndex = 9
        Me.txtAdvancePaid.Text = "0"
        Me.txtAdvancePaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvancePaid.Value = 0R
        '
        'lblBalance
        '
        Me.lblBalance.AutoSize = False
        Me.lblBalance.BorderVisible = True
        Me.lblBalance.FieldName = Nothing
        Me.lblBalance.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.Location = New System.Drawing.Point(412, 187)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(156, 23)
        Me.lblBalance.TabIndex = 12
        Me.lblBalance.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBalancePayment
        '
        Me.lblBalancePayment.AutoSize = False
        Me.lblBalancePayment.BorderVisible = True
        Me.lblBalancePayment.FieldName = Nothing
        Me.lblBalancePayment.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalancePayment.Location = New System.Drawing.Point(412, 141)
        Me.lblBalancePayment.Name = "lblBalancePayment"
        Me.lblBalancePayment.Size = New System.Drawing.Size(156, 23)
        Me.lblBalancePayment.TabIndex = 10
        Me.lblBalancePayment.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(296, 166)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(86, 19)
        Me.MyLabel6.TabIndex = 13
        Me.MyLabel6.Text = "Amount Paid"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(296, 143)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(115, 19)
        Me.MyLabel4.TabIndex = 14
        Me.MyLabel4.Text = "Balance Payment"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(4, 100)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1053, 130)
        Me.gv1.TabIndex = 15
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(3, 6)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(50, 19)
        Me.RadLabel1.TabIndex = 45
        Me.RadLabel1.Text = "Invoice"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel11)
        Me.RadGroupBox1.Controls.Add(Me.gv2)
        Me.RadGroupBox1.Controls.Add(Me.txtTaxGroup)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.lblTaxGrpName)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(9, 236)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(474, 212)
        Me.RadGroupBox1.TabIndex = 16
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(5, 12)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(72, 19)
        Me.RadLabel11.TabIndex = 5
        Me.RadLabel11.Text = "Tax Group"
        '
        'gv2
        '
        Me.gv2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(5, 63)
        '
        'gv2
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(462, 146)
        Me.gv2.TabIndex = 3
        Me.gv2.TabStop = False
        Me.gv2.Text = "RadGridView1"
        '
        'txtTaxGroup
        '
        Me.txtTaxGroup.CalculationExpression = Nothing
        Me.txtTaxGroup.FieldCode = Nothing
        Me.txtTaxGroup.FieldDesc = Nothing
        Me.txtTaxGroup.FieldMaxLength = 0
        Me.txtTaxGroup.FieldName = Nothing
        Me.txtTaxGroup.isCalculatedField = False
        Me.txtTaxGroup.IsSourceFromTable = False
        Me.txtTaxGroup.IsSourceFromValueList = False
        Me.txtTaxGroup.IsUnique = False
        Me.txtTaxGroup.Location = New System.Drawing.Point(80, 11)
        Me.txtTaxGroup.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtTaxGroup.MendatroryField = True
        Me.txtTaxGroup.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.MyLinkLable1 = Me.RadLabel11
        Me.txtTaxGroup.MyLinkLable2 = Me.lblTaxGrpName
        Me.txtTaxGroup.MyReadOnly = False
        Me.txtTaxGroup.MyShowMasterFormButton = False
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReferenceFieldDesc = Nothing
        Me.txtTaxGroup.ReferenceFieldName = Nothing
        Me.txtTaxGroup.ReferenceTableName = Nothing
        Me.txtTaxGroup.Size = New System.Drawing.Size(217, 21)
        Me.txtTaxGroup.TabIndex = 0
        Me.txtTaxGroup.Value = ""
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(5, 38)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(460, 23)
        Me.lblTaxGrpName.TabIndex = 2
        Me.lblTaxGrpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTaxGrpName.TextWrap = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(302, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(161, 36)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tax Calculation Type"
        '
        'rbtnTaxCalManual
        '
        Me.rbtnTaxCalManual.Location = New System.Drawing.Point(90, 13)
        Me.rbtnTaxCalManual.MyLinkLable1 = Nothing
        Me.rbtnTaxCalManual.MyLinkLable2 = Nothing
        Me.rbtnTaxCalManual.Name = "rbtnTaxCalManual"
        Me.rbtnTaxCalManual.Size = New System.Drawing.Size(57, 18)
        Me.rbtnTaxCalManual.TabIndex = 1
        Me.rbtnTaxCalManual.Text = "Manual"
        '
        'rbtnTaxCalAutomatic
        '
        Me.rbtnTaxCalAutomatic.Location = New System.Drawing.Point(7, 13)
        Me.rbtnTaxCalAutomatic.MyLinkLable1 = Nothing
        Me.rbtnTaxCalAutomatic.MyLinkLable2 = Nothing
        Me.rbtnTaxCalAutomatic.Name = "rbtnTaxCalAutomatic"
        Me.rbtnTaxCalAutomatic.Size = New System.Drawing.Size(72, 18)
        Me.rbtnTaxCalAutomatic.TabIndex = 0
        Me.rbtnTaxCalAutomatic.Text = "Automatic"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(652, 29)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel3
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(403, 21)
        Me.txtRemarks.TabIndex = 8
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(571, 30)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(62, 19)
        Me.RadLabel3.TabIndex = 28
        Me.RadLabel3.Text = "Remarks"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(72, 4)
        Me.txtDocNo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(278, 23)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'txtDeliveryDate
        '
        Me.txtDeliveryDate.CalculationExpression = Nothing
        Me.txtDeliveryDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDeliveryDate.FieldCode = Nothing
        Me.txtDeliveryDate.FieldDesc = Nothing
        Me.txtDeliveryDate.FieldMaxLength = 0
        Me.txtDeliveryDate.FieldName = Nothing
        Me.txtDeliveryDate.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDeliveryDate.isCalculatedField = False
        Me.txtDeliveryDate.IsSourceFromTable = False
        Me.txtDeliveryDate.IsSourceFromValueList = False
        Me.txtDeliveryDate.IsUnique = False
        Me.txtDeliveryDate.Location = New System.Drawing.Point(825, 5)
        Me.txtDeliveryDate.MendatroryField = False
        Me.txtDeliveryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.MyLinkLable1 = Me.MyLabel1
        Me.txtDeliveryDate.MyLinkLable2 = Nothing
        Me.txtDeliveryDate.Name = "txtDeliveryDate"
        Me.txtDeliveryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.ReferenceFieldDesc = Nothing
        Me.txtDeliveryDate.ReferenceFieldName = Nothing
        Me.txtDeliveryDate.ReferenceTableName = Nothing
        Me.txtDeliveryDate.Size = New System.Drawing.Size(148, 21)
        Me.txtDeliveryDate.TabIndex = 4
        Me.txtDeliveryDate.TabStop = False
        Me.txtDeliveryDate.Text = "13/06/2011 11:29 AM"
        Me.txtDeliveryDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(745, 6)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(74, 19)
        Me.MyLabel1.TabIndex = 50
        Me.MyLabel1.Text = "Deliv. Date"
        '
        'txtVendorNo
        '
        Me.txtVendorNo.CalculationExpression = Nothing
        Me.txtVendorNo.FieldCode = Nothing
        Me.txtVendorNo.FieldDesc = Nothing
        Me.txtVendorNo.FieldMaxLength = 0
        Me.txtVendorNo.FieldName = Nothing
        Me.txtVendorNo.isCalculatedField = False
        Me.txtVendorNo.IsSourceFromTable = False
        Me.txtVendorNo.IsSourceFromValueList = False
        Me.txtVendorNo.IsUnique = False
        Me.txtVendorNo.Location = New System.Drawing.Point(72, 28)
        Me.txtVendorNo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(185, 22)
        Me.txtVendorNo.TabIndex = 5
        Me.txtVendorNo.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 30)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(66, 19)
        Me.RadLabel2.TabIndex = 27
        Me.RadLabel2.Text = "Customer"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(281, 28)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(288, 22)
        Me.lblVendorName.TabIndex = 7
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.TextWrap = False
        '
        'txtMessage
        '
        Me.txtMessage.AutoSize = False
        Me.txtMessage.CalculationExpression = Nothing
        Me.txtMessage.FieldCode = Nothing
        Me.txtMessage.FieldDesc = Nothing
        Me.txtMessage.FieldMaxLength = 0
        Me.txtMessage.FieldName = Nothing
        Me.txtMessage.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage.isCalculatedField = False
        Me.txtMessage.IsSourceFromTable = False
        Me.txtMessage.IsSourceFromValueList = False
        Me.txtMessage.IsUnique = False
        Me.txtMessage.Location = New System.Drawing.Point(652, 52)
        Me.txtMessage.MaxLength = 200
        Me.txtMessage.MendatroryField = False
        Me.txtMessage.Multiline = True
        Me.txtMessage.MyLinkLable1 = Me.RadLabel14
        Me.txtMessage.MyLinkLable2 = Nothing
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ReferenceFieldDesc = Nothing
        Me.txtMessage.ReferenceFieldName = Nothing
        Me.txtMessage.ReferenceTableName = Nothing
        Me.txtMessage.Size = New System.Drawing.Size(403, 45)
        Me.txtMessage.TabIndex = 14
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(571, 52)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(63, 19)
        Me.RadLabel14.TabIndex = 32
        Me.RadLabel14.Text = "Message"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(421, 5)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(148, 21)
        Me.txtDate.TabIndex = 2
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(380, 6)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(36, 19)
        Me.RadLabel4.TabIndex = 2
        Me.RadLabel4.Text = "Date"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(351, 4)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(21, 23)
        Me.btnAddNew.TabIndex = 1
        '
        'RadLabel29
        '
        Me.RadLabel29.FieldName = Nothing
        Me.RadLabel29.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel29.Location = New System.Drawing.Point(571, 6)
        Me.RadLabel29.Name = "RadLabel29"
        Me.RadLabel29.Size = New System.Drawing.Size(76, 19)
        Me.RadLabel29.TabIndex = 40
        Me.RadLabel29.Text = "Deliv. Type"
        '
        'cboDeliveryType
        '
        Me.cboDeliveryType.CalculationExpression = Nothing
        Me.cboDeliveryType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDeliveryType.FieldCode = Nothing
        Me.cboDeliveryType.FieldDesc = Nothing
        Me.cboDeliveryType.FieldMaxLength = 0
        Me.cboDeliveryType.FieldName = Nothing
        Me.cboDeliveryType.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDeliveryType.isCalculatedField = False
        Me.cboDeliveryType.IsSourceFromTable = False
        Me.cboDeliveryType.IsSourceFromValueList = False
        Me.cboDeliveryType.IsUnique = False
        Me.cboDeliveryType.Location = New System.Drawing.Point(652, 4)
        Me.cboDeliveryType.MendatroryField = True
        Me.cboDeliveryType.MyLinkLable1 = Me.RadLabel29
        Me.cboDeliveryType.MyLinkLable2 = Nothing
        Me.cboDeliveryType.Name = "cboDeliveryType"
        Me.cboDeliveryType.ReferenceFieldDesc = Nothing
        Me.cboDeliveryType.ReferenceFieldName = Nothing
        Me.cboDeliveryType.ReferenceTableName = Nothing
        Me.cboDeliveryType.Size = New System.Drawing.Size(93, 23)
        Me.cboDeliveryType.TabIndex = 3
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(218, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(147, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(76, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 0
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(988, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadLabel24
        '
        Me.RadLabel24.FieldName = Nothing
        Me.RadLabel24.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel24.Location = New System.Drawing.Point(747, 7)
        Me.RadLabel24.Name = "RadLabel24"
        Me.RadLabel24.Size = New System.Drawing.Size(63, 19)
        Me.RadLabel24.TabIndex = 3
        Me.RadLabel24.Text = "Order No"
        Me.RadLabel24.Visible = False
        '
        'txtReqNo
        '
        Me.txtReqNo.CalculationExpression = Nothing
        Me.txtReqNo.FieldCode = Nothing
        Me.txtReqNo.FieldDesc = Nothing
        Me.txtReqNo.FieldMaxLength = 0
        Me.txtReqNo.FieldName = Nothing
        Me.txtReqNo.isCalculatedField = False
        Me.txtReqNo.IsSourceFromTable = False
        Me.txtReqNo.IsSourceFromValueList = False
        Me.txtReqNo.IsUnique = False
        Me.txtReqNo.Location = New System.Drawing.Point(813, 6)
        Me.txtReqNo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyFont = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel24
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyReadOnly = True
        Me.txtReqNo.MyShowMasterFormButton = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReferenceFieldDesc = Nothing
        Me.txtReqNo.ReferenceFieldName = Nothing
        Me.txtReqNo.ReferenceTableName = Nothing
        Me.txtReqNo.Size = New System.Drawing.Size(166, 18)
        Me.txtReqNo.TabIndex = 4
        Me.txtReqNo.Value = ""
        Me.txtReqNo.Visible = False
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1061, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4, Me.RadMenuItem5, Me.RadMenuItem6})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem4.AccessibleName = "Delete Layout"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "E-Mail/SMS Setting"
        Me.RadMenuItem5.AccessibleName = "E-Mail/SMS Setting"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "E-Mail/SMS Setting"
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AccessibleDescription = "Email And SMS Reipients"
        Me.RadMenuItem6.AccessibleName = "Email And SMS Reipients"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Email And SMS Reipients"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1061, 488)
        Me.Panel1.TabIndex = 4
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AnimationEnabled = False
        Me.RadMenuItem2.AnimationFrames = 1
        Me.RadMenuItem2.AnimationType = Telerik.WinControls.UI.PopupAnimationTypes.None
        Me.RadMenuItem2.AutoSize = True
        Me.RadMenuItem2.DropDownAnimationDirection = Telerik.WinControls.UI.RadDirection.Down
        Me.RadMenuItem2.DropShadow = True
        Me.RadMenuItem2.EasingType = Telerik.WinControls.RadEasingType.InQuad
        Me.RadMenuItem2.EnableAeroEffects = False
        Me.RadMenuItem2.FadeAnimationFrames = 10
        Me.RadMenuItem2.FadeAnimationSpeed = 10
        Me.RadMenuItem2.FadeAnimationType = Telerik.WinControls.UI.FadeAnimationType.FadeIn
        Me.RadMenuItem2.FitToScreenMode = CType((Telerik.WinControls.UI.FitToScreenModes.FitWidth Or Telerik.WinControls.UI.FitToScreenModes.FitHeight), Telerik.WinControls.UI.FitToScreenModes)
        Me.RadMenuItem2.HorizontalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenuItem2.Maximum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Minimum = New System.Drawing.Size(0, 0)
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Opacity = 1.0!
        Me.RadMenuItem2.ProcessKeyboard = False
        Me.RadMenuItem2.RollOverItemSelection = True
        Me.RadMenuItem2.Size = New System.Drawing.Size(44, 2)
        Me.RadMenuItem2.TabIndex = 0
        Me.RadMenuItem2.VerticalAlignmentCorrectionMode = Telerik.WinControls.UI.AlignmentCorrectionMode.SnapToOuterEdges
        Me.RadMenuItem2.Visible = False
        '
        'frmSNPOS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1061, 508)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(1022, 538)
        Me.Name = "frmSNPOS"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "POS"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShiping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCustomerMasterOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCreditCard.ResumeLayout(False)
        Me.pnlCreditCard.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApprovalCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDebit.ResumeLayout(False)
        Me.pnlDebit.PerformLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCardType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDebitCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCheque.ResumeLayout(False)
        Me.pnlCheque.PerformLayout()
        CType(Me.txtChequeDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPaymentMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOtherCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmtPaid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdvancePaid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalancePayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDeliveryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadMenuItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents txtMessage As common.Controls.MyTextBox
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtReqNo As common.UserControls.txtFinder
    Friend WithEvents cboDeliveryType As common.Controls.MyComboBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadDropDownMenu
    Friend WithEvents RadThemeManager1 As Telerik.WinControls.RadThemeManager
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblBillToLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel24 As common.Controls.MyLabel
    Friend WithEvents RadLabel29 As common.Controls.MyLabel
    Friend WithEvents txtDeliveryDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterTax As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblBalancePayment As common.Controls.MyLabel
    Friend WithEvents txtAdvancePaid As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtOtherCharges As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtfreight As common.MyNumBox
    Friend WithEvents lblKMReading As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblBalance As common.Controls.MyLabel
    Friend WithEvents txtAmtPaid As common.MyNumBox
    Friend WithEvents pnlCheque As System.Windows.Forms.Panel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents cboPaymentMode As common.Controls.MyComboBox
    Friend WithEvents pnlCreditCard As System.Windows.Forms.Panel
    Friend WithEvents pnlDebit As System.Windows.Forms.Panel
    Friend WithEvents txtChequeDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtChequeNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtCreditCardNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtApprovalCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtBatchNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtBankName As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents cboCardType As common.Controls.MyComboBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtDebitCardNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtBarCode As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents btnCustomerMasterOpen As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblShiping As common.Controls.MyLabel
    Friend WithEvents txtShipping As common.UserControls.txtFinder
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
End Class

