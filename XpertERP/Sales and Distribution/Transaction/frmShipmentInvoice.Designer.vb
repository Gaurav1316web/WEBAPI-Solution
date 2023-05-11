<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShipmentInvoice
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewComboBoxColumn1 As Telerik.WinControls.UI.GridViewComboBoxColumn = New Telerik.WinControls.UI.GridViewComboBoxColumn
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn5 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn6 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn7 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn8 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn9 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Dim GridViewTextBoxColumn10 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn
        Me.pvLoadOut = New Telerik.WinControls.UI.RadPageView
        Me.pageLoadOut = New Telerik.WinControls.UI.RadPageViewPage
        Me.lblSalesMan1 = New common.Controls.MyLabel
        Me.txtRemovalTime = New common.Controls.MyDateTimePicker
        Me.lblShipDate = New common.Controls.MyLabel
        Me.txtPriceCode = New common.Controls.MyLabel
        Me.lblVhicleNo = New common.Controls.MyTextBox
        Me.lblDescription = New common.Controls.MyLabel
        Me.txtSalesman = New common.UserControls.txtFinder
        Me.lblSalesman = New common.Controls.MyLabel
        Me.txtOrderNo = New common.UserControls.txtFinder
        Me.lblOrderNo = New common.Controls.MyLabel
        Me.chkCreateEmpty = New Telerik.WinControls.UI.RadCheckBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.chkAgainstCForm = New Telerik.WinControls.UI.RadCheckBox
        Me.chkSample = New Telerik.WinControls.UI.RadCheckBox
        Me.chlcreditinvoice = New Telerik.WinControls.UI.RadCheckBox
        Me.rbAll = New Telerik.WinControls.UI.RadRadioButton
        Me.rbFC = New Telerik.WinControls.UI.RadRadioButton
        Me.rbFB = New Telerik.WinControls.UI.RadRadioButton
        Me.chkInvoice = New Telerik.WinControls.UI.RadCheckBox
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox
        Me.chkMultipleOrder = New Telerik.WinControls.UI.RadCheckBox
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.txtMannualQty = New common.MyNumBox
        Me.lblManualQty = New common.Controls.MyLabel
        Me.txtEmployeeCode = New common.UserControls.txtFinder
        Me.lblemployeecode = New common.Controls.MyLabel
        Me.lblEmpName = New common.Controls.MyLabel
        Me.txtVehicleCode = New common.UserControls.txtFinder
        Me.lblVehicleCode = New common.Controls.MyLabel
        Me.txtMannualAmt = New common.MyNumBox
        Me.lblManualAmt = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.CmbTransaction = New common.Controls.MyComboBox
        Me.lblTransaction = New common.Controls.MyLabel
        Me.txtDocNo = New common.UserControls.txtNavigator
        Me.lblLoadOut = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.lblSaleInvoiceNo = New common.Controls.MyLabel
        Me.lblShipTo = New common.Controls.MyLabel
        Me.txtShipTo = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtTransferNo = New common.UserControls.txtFinder
        Me.lblTransferNo = New common.Controls.MyLabel
        Me.txtKMReading = New common.MyNumBox
        Me.lblKMReading = New common.Controls.MyLabel
        Me.txtLocation = New common.UserControls.txtFinder
        Me.lblLocation = New common.Controls.MyLabel
        Me.txtCustomer = New common.UserControls.txtFinder
        Me.lblCustomeNo = New common.Controls.MyLabel
        Me.txtCustomerName = New common.Controls.MyLabel
        Me.lblLoadType = New common.Controls.MyLabel
        Me.txtshellqty = New common.Controls.MyTextBox
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.cboLoadOutType = New common.Controls.MyComboBox
        Me.txtcustomerinvoiceno = New common.Controls.MyTextBox
        Me.lblcustomerinvoiceno = New common.Controls.MyLabel
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.txtTransferDate = New common.Controls.MyDateTimePicker
        Me.lblTransferDate = New common.Controls.MyLabel
        Me.cboPriceDate = New common.Controls.MyComboBox
        Me.lblPriceDate = New common.Controls.MyLabel
        Me.lblRemark = New common.Controls.MyLabel
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.lblTripNo = New common.Controls.MyLabel
        Me.txtTripNo = New common.Controls.MyTextBox
        Me.lblPriceCode = New common.Controls.MyLabel
        Me.txtDesc = New common.Controls.MyTextBox
        Me.lblExpectedShipDate = New common.Controls.MyLabel
        Me.txtExpectedShipDate = New common.Controls.MyDateTimePicker
        Me.lblCustomerPONo = New common.Controls.MyLabel
        Me.txtCustomerPONO = New common.Controls.MyTextBox
        Me.lblOrderDate = New common.Controls.MyLabel
        Me.txtOrderDate = New common.Controls.MyDateTimePicker
        Me.lblModeofTransport = New common.Controls.MyLabel
        Me.cboModeOfTransport = New common.Controls.MyComboBox
        Me.pageTaxDetails = New Telerik.WinControls.UI.RadPageViewPage
        Me.lblTaxDesc = New common.Controls.MyLabel
        Me.lblRouteDesc = New common.Controls.MyLabel
        Me.txtPaymentTerm = New common.UserControls.txtFinder
        Me.lblRouteNo = New common.Controls.MyLabel
        Me.txtRouteNo = New common.UserControls.txtFinder
        Me.lblRefNo = New common.Controls.MyLabel
        Me.txtRef = New common.Controls.MyTextBox
        Me.lblPaymentTerms = New common.Controls.MyLabel
        Me.lblTaxGroup = New common.Controls.MyLabel
        Me.fndTaxGroup = New finder.finder
        Me.gbTaxDetails = New Telerik.WinControls.UI.RadGroupBox
        Me.gvTax = New common.UserControls.MyRadGridView
        Me.txtSchemeSample = New finder.finder
        Me.lblSchemeSample = New common.Controls.MyLabel
        Me.pageTotal = New Telerik.WinControls.UI.RadPageViewPage
        Me.btnRecalTransAndReCreateJE = New Telerik.WinControls.UI.RadButton
        Me.pnlMannualInvoiceNo = New System.Windows.Forms.Panel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.txtMannaulInvoiceNo = New common.Controls.MyTextBox
        Me.btnCreateRSSaleType = New Telerik.WinControls.UI.RadButton
        Me.btnReverse = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadRadioButton1 = New Telerik.WinControls.UI.RadRadioButton
        Me.RadRadioButton2 = New Telerik.WinControls.UI.RadRadioButton
        Me.lblAddCharges = New common.Controls.MyLabel
        Me.lblOtherCharges = New common.Controls.MyLabel
        Me.txtAdditionalCharges = New common.Controls.MyTextBox
        Me.txtOtherCharges = New common.Controls.MyTextBox
        Me.txtFreight = New common.Controls.MyTextBox
        Me.lblFreight = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.chkDiscountOnAmt = New Telerik.WinControls.UI.RadRadioButton
        Me.chkDiscountOnRate = New Telerik.WinControls.UI.RadRadioButton
        Me.txtDiscAmt = New common.MyNumBox
        Me.txtDiscPer = New common.MyNumBox
        Me.RadLabel12 = New common.Controls.MyLabel
        Me.RadLabel11 = New common.Controls.MyLabel
        Me.txtTotalShipmentAmt = New common.Controls.MyTextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.lblShipmentAmt = New common.Controls.MyLabel
        Me.txtContainerDeposit = New common.Controls.MyTextBox
        Me.txtShipmentAmt = New common.Controls.MyTextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtTotalTPT = New common.Controls.MyTextBox
        Me.rdbFix = New Telerik.WinControls.UI.RadRadioButton
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.txtNetShipAmt = New common.Controls.MyTextBox
        Me.rdbPer = New Telerik.WinControls.UI.RadRadioButton
        Me.RadLabel9 = New common.Controls.MyLabel
        Me.lblTotalTaxAmt = New common.Controls.MyLabel
        Me.txtTotalTaxAmount = New common.Controls.MyTextBox
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.lblShipmentTotal = New common.Controls.MyLabel
        Me.txtShipmentTotal = New common.Controls.MyTextBox
        Me.lblDiscountPer = New common.Controls.MyLabel
        Me.txtCustDisc = New common.Controls.MyTextBox
        Me.btnDeleteRS = New Telerik.WinControls.UI.RadButton
        Me.btnDeleteRSSaleType = New Telerik.WinControls.UI.RadButton
        Me.btnCreateRS = New Telerik.WinControls.UI.RadButton
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton
        Me.btnReverseTransaction = New Telerik.WinControls.UI.RadButton
        Me.lbltotalfcfb = New common.Controls.MyLabel
        Me.btnUpdateWithCustomerNewPrice = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSkipExciseInvoice = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnSettlement = New Telerik.WinControls.UI.RadButton
        Me.btnAdd = New Telerik.WinControls.UI.RadButton
        Me.btnSaveAndPrint = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.lblfb = New common.Controls.MyLabel
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.lblfc = New common.Controls.MyLabel
        Me.RadLabel5 = New common.Controls.MyLabel
        CType(Me.pvLoadOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvLoadOut.SuspendLayout()
        Me.pageLoadOut.SuspendLayout()
        CType(Me.lblSalesMan1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemovalTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVhicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateEmpty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAgainstCForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSample, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chlcreditinvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbFC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbFB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMultipleOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMannualQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblManualQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblemployeecode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMannualAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblManualAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoadOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSaleInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKMReading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoadType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtshellqty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLoadOutType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustomerinvoiceno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblcustomerinvoiceno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransferDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransferDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPriceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTripNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTripNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpectedShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpectedShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerPONO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageTaxDetails.SuspendLayout()
        CType(Me.lblTaxDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPaymentTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbTaxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTaxDetails.SuspendLayout()
        CType(Me.gvTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTax.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSchemeSample, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageTotal.SuspendLayout()
        CType(Me.btnRecalTransAndReCreateJE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMannualInvoiceNo.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMannaulInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreateRSSaleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadRadioButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadRadioButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOtherCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdditionalCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOtherCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalShipmentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipmentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtContainerDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipmentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalTPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetShipAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalTaxAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipmentTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShipmentTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeleteRS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeleteRSSaleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreateRS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverseTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltotalfcfb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateWithCustomerNewPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSkipExciseInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSettlement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveAndPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pvLoadOut
        '
        Me.pvLoadOut.Controls.Add(Me.pageLoadOut)
        Me.pvLoadOut.Controls.Add(Me.pageTaxDetails)
        Me.pvLoadOut.Controls.Add(Me.pageTotal)
        Me.pvLoadOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pvLoadOut.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pvLoadOut.Location = New System.Drawing.Point(0, 0)
        Me.pvLoadOut.Name = "pvLoadOut"
        Me.pvLoadOut.SelectedPage = Me.pageLoadOut
        Me.pvLoadOut.Size = New System.Drawing.Size(1012, 460)
        Me.pvLoadOut.TabIndex = 0
        Me.pvLoadOut.Text = "Load Out"
        CType(Me.pvLoadOut.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageLoadOut
        '
        Me.pageLoadOut.Controls.Add(Me.lblSalesMan1)
        Me.pageLoadOut.Controls.Add(Me.txtRemovalTime)
        Me.pageLoadOut.Controls.Add(Me.txtPriceCode)
        Me.pageLoadOut.Controls.Add(Me.lblVhicleNo)
        Me.pageLoadOut.Controls.Add(Me.txtSalesman)
        Me.pageLoadOut.Controls.Add(Me.txtOrderNo)
        Me.pageLoadOut.Controls.Add(Me.chkCreateEmpty)
        Me.pageLoadOut.Controls.Add(Me.gv1)
        Me.pageLoadOut.Controls.Add(Me.chkAgainstCForm)
        Me.pageLoadOut.Controls.Add(Me.chkSample)
        Me.pageLoadOut.Controls.Add(Me.chlcreditinvoice)
        Me.pageLoadOut.Controls.Add(Me.rbAll)
        Me.pageLoadOut.Controls.Add(Me.rbFC)
        Me.pageLoadOut.Controls.Add(Me.rbFB)
        Me.pageLoadOut.Controls.Add(Me.chkInvoice)
        Me.pageLoadOut.Controls.Add(Me.chkOnHold)
        Me.pageLoadOut.Controls.Add(Me.chkMultipleOrder)
        Me.pageLoadOut.Controls.Add(Me.btnReset)
        Me.pageLoadOut.Controls.Add(Me.txtMannualQty)
        Me.pageLoadOut.Controls.Add(Me.lblManualQty)
        Me.pageLoadOut.Controls.Add(Me.txtEmployeeCode)
        Me.pageLoadOut.Controls.Add(Me.txtVehicleCode)
        Me.pageLoadOut.Controls.Add(Me.txtMannualAmt)
        Me.pageLoadOut.Controls.Add(Me.lblSalesman)
        Me.pageLoadOut.Controls.Add(Me.MyLabel1)
        Me.pageLoadOut.Controls.Add(Me.CmbTransaction)
        Me.pageLoadOut.Controls.Add(Me.lblManualAmt)
        Me.pageLoadOut.Controls.Add(Me.txtDocNo)
        Me.pageLoadOut.Controls.Add(Me.lblTransaction)
        Me.pageLoadOut.Controls.Add(Me.UsLock1)
        Me.pageLoadOut.Controls.Add(Me.lblEmpName)
        Me.pageLoadOut.Controls.Add(Me.lblSaleInvoiceNo)
        Me.pageLoadOut.Controls.Add(Me.lblShipTo)
        Me.pageLoadOut.Controls.Add(Me.txtShipTo)
        Me.pageLoadOut.Controls.Add(Me.MyLabel2)
        Me.pageLoadOut.Controls.Add(Me.txtTransferNo)
        Me.pageLoadOut.Controls.Add(Me.txtKMReading)
        Me.pageLoadOut.Controls.Add(Me.txtLocation)
        Me.pageLoadOut.Controls.Add(Me.txtCustomer)
        Me.pageLoadOut.Controls.Add(Me.lblLoadType)
        Me.pageLoadOut.Controls.Add(Me.lblemployeecode)
        Me.pageLoadOut.Controls.Add(Me.txtshellqty)
        Me.pageLoadOut.Controls.Add(Me.cboLoadOutType)
        Me.pageLoadOut.Controls.Add(Me.RadLabel1)
        Me.pageLoadOut.Controls.Add(Me.txtcustomerinvoiceno)
        Me.pageLoadOut.Controls.Add(Me.lblLoadOut)
        Me.pageLoadOut.Controls.Add(Me.txtDate)
        Me.pageLoadOut.Controls.Add(Me.lblcustomerinvoiceno)
        Me.pageLoadOut.Controls.Add(Me.lblShipDate)
        Me.pageLoadOut.Controls.Add(Me.txtTransferDate)
        Me.pageLoadOut.Controls.Add(Me.cboPriceDate)
        Me.pageLoadOut.Controls.Add(Me.lblTransferNo)
        Me.pageLoadOut.Controls.Add(Me.lblPriceDate)
        Me.pageLoadOut.Controls.Add(Me.lblRemark)
        Me.pageLoadOut.Controls.Add(Me.txtRemarks)
        Me.pageLoadOut.Controls.Add(Me.lblTripNo)
        Me.pageLoadOut.Controls.Add(Me.txtTripNo)
        Me.pageLoadOut.Controls.Add(Me.lblTransferDate)
        Me.pageLoadOut.Controls.Add(Me.lblPriceCode)
        Me.pageLoadOut.Controls.Add(Me.lblKMReading)
        Me.pageLoadOut.Controls.Add(Me.lblVehicleCode)
        Me.pageLoadOut.Controls.Add(Me.lblDescription)
        Me.pageLoadOut.Controls.Add(Me.txtDesc)
        Me.pageLoadOut.Controls.Add(Me.lblExpectedShipDate)
        Me.pageLoadOut.Controls.Add(Me.txtExpectedShipDate)
        Me.pageLoadOut.Controls.Add(Me.lblCustomerPONo)
        Me.pageLoadOut.Controls.Add(Me.txtCustomerPONO)
        Me.pageLoadOut.Controls.Add(Me.txtCustomerName)
        Me.pageLoadOut.Controls.Add(Me.lblCustomeNo)
        Me.pageLoadOut.Controls.Add(Me.lblOrderDate)
        Me.pageLoadOut.Controls.Add(Me.txtOrderDate)
        Me.pageLoadOut.Controls.Add(Me.lblOrderNo)
        Me.pageLoadOut.Controls.Add(Me.lblLocation)
        Me.pageLoadOut.Controls.Add(Me.lblModeofTransport)
        Me.pageLoadOut.Controls.Add(Me.cboModeOfTransport)
        Me.pageLoadOut.ItemSize = New System.Drawing.SizeF(63.0!, 26.0!)
        Me.pageLoadOut.Location = New System.Drawing.Point(10, 35)
        Me.pageLoadOut.Name = "pageLoadOut"
        Me.pageLoadOut.Size = New System.Drawing.Size(991, 414)
        Me.pageLoadOut.Text = "Load Out"
        '
        'lblSalesMan1
        '
        Me.lblSalesMan1.AutoSize = False
        Me.lblSalesMan1.BorderVisible = True
        Me.lblSalesMan1.Location = New System.Drawing.Point(215, 110)
        Me.lblSalesMan1.Name = "lblSalesMan1"
        Me.lblSalesMan1.Size = New System.Drawing.Size(225, 18)
        Me.lblSalesMan1.TabIndex = 119
        '
        'txtRemovalTime
        '
        Me.txtRemovalTime.CustomFormat = "hh:mm tt"
        Me.txtRemovalTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemovalTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRemovalTime.Location = New System.Drawing.Point(515, 2)
        Me.txtRemovalTime.MendatroryField = False
        Me.txtRemovalTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRemovalTime.MyLinkLable1 = Me.lblShipDate
        Me.txtRemovalTime.MyLinkLable2 = Nothing
        Me.txtRemovalTime.Name = "txtRemovalTime"
        Me.txtRemovalTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRemovalTime.Size = New System.Drawing.Size(70, 18)
        Me.txtRemovalTime.TabIndex = 1
        Me.txtRemovalTime.TabStop = False
        Me.txtRemovalTime.Text = "02:11 PM"
        Me.txtRemovalTime.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'lblShipDate
        '
        Me.lblShipDate.Location = New System.Drawing.Point(376, 3)
        Me.lblShipDate.Name = "lblShipDate"
        Me.lblShipDate.Size = New System.Drawing.Size(30, 18)
        Me.lblShipDate.TabIndex = 15
        Me.lblShipDate.Text = "Date"
        '
        'txtPriceCode
        '
        Me.txtPriceCode.AutoSize = False
        Me.txtPriceCode.BorderVisible = True
        Me.txtPriceCode.Location = New System.Drawing.Point(769, 68)
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(217, 19)
        Me.txtPriceCode.TabIndex = 18
        Me.txtPriceCode.TextWrap = False
        '
        'lblVhicleNo
        '
        Me.lblVhicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVhicleNo.Location = New System.Drawing.Point(215, 68)
        Me.lblVhicleNo.MaxLength = 200
        Me.lblVhicleNo.MendatroryField = False
        Me.lblVhicleNo.MyLinkLable1 = Me.lblDescription
        Me.lblVhicleNo.MyLinkLable2 = Nothing
        Me.lblVhicleNo.Name = "lblVhicleNo"
        Me.lblVhicleNo.Size = New System.Drawing.Size(225, 18)
        Me.lblVhicleNo.TabIndex = 10
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(0, 132)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 36
        Me.lblDescription.Text = "Description"
        '
        'txtSalesman
        '
        Me.txtSalesman.Location = New System.Drawing.Point(80, 110)
        Me.txtSalesman.MendatroryField = False
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Me.lblSalesman
        Me.txtSalesman.MyLinkLable2 = Nothing
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.Size = New System.Drawing.Size(133, 18)
        Me.txtSalesman.TabIndex = 14
        Me.txtSalesman.Value = ""
        '
        'lblSalesman
        '
        Me.lblSalesman.Location = New System.Drawing.Point(0, 111)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(53, 18)
        Me.lblSalesman.TabIndex = 104
        Me.lblSalesman.Text = "Salesman"
        '
        'txtOrderNo
        '
        Me.txtOrderNo.Location = New System.Drawing.Point(80, 25)
        Me.txtOrderNo.MendatroryField = False
        Me.txtOrderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.MyLinkLable1 = Me.lblOrderNo
        Me.txtOrderNo.MyLinkLable2 = Nothing
        Me.txtOrderNo.MyReadOnly = False
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(133, 19)
        Me.txtOrderNo.TabIndex = 3
        Me.txtOrderNo.Value = ""
        '
        'lblOrderNo
        '
        Me.lblOrderNo.Location = New System.Drawing.Point(0, 26)
        Me.lblOrderNo.Name = "lblOrderNo"
        Me.lblOrderNo.Size = New System.Drawing.Size(53, 18)
        Me.lblOrderNo.TabIndex = 61
        Me.lblOrderNo.Text = "Order No"
        '
        'chkCreateEmpty
        '
        Me.chkCreateEmpty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreateEmpty.Location = New System.Drawing.Point(621, 132)
        Me.chkCreateEmpty.Name = "chkCreateEmpty"
        '
        '
        '
        Me.chkCreateEmpty.RootElement.StretchHorizontally = True
        Me.chkCreateEmpty.RootElement.StretchVertically = True
        Me.chkCreateEmpty.Size = New System.Drawing.Size(89, 16)
        Me.chkCreateEmpty.TabIndex = 31
        Me.chkCreateEmpty.Text = "Create Empty"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.White
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 177)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.ShowRowHeaderColumn = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.Size = New System.Drawing.Size(987, 237)
        Me.gv1.TabIndex = 24
        Me.gv1.Text = "GV Load Out"
        '
        'chkAgainstCForm
        '
        Me.chkAgainstCForm.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgainstCForm.Location = New System.Drawing.Point(444, 153)
        Me.chkAgainstCForm.Name = "chkAgainstCForm"
        '
        '
        '
        Me.chkAgainstCForm.RootElement.StretchHorizontally = True
        Me.chkAgainstCForm.RootElement.StretchVertically = True
        Me.chkAgainstCForm.Size = New System.Drawing.Size(98, 16)
        Me.chkAgainstCForm.TabIndex = 36
        Me.chkAgainstCForm.Text = "Against C Form"
        '
        'chkSample
        '
        Me.chkSample.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSample.Location = New System.Drawing.Point(715, 132)
        Me.chkSample.Name = "chkSample"
        '
        '
        '
        Me.chkSample.RootElement.StretchHorizontally = True
        Me.chkSample.RootElement.StretchVertically = True
        Me.chkSample.Size = New System.Drawing.Size(57, 16)
        Me.chkSample.TabIndex = 32
        Me.chkSample.Text = "Sample"
        Me.chkSample.Visible = False
        '
        'chlcreditinvoice
        '
        Me.chlcreditinvoice.Enabled = False
        Me.chlcreditinvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chlcreditinvoice.Location = New System.Drawing.Point(785, 130)
        Me.chlcreditinvoice.Name = "chlcreditinvoice"
        '
        '
        '
        Me.chlcreditinvoice.RootElement.StretchHorizontally = True
        Me.chlcreditinvoice.RootElement.StretchVertically = True
        Me.chlcreditinvoice.Size = New System.Drawing.Size(88, 20)
        Me.chlcreditinvoice.TabIndex = 33
        Me.chlcreditinvoice.Text = "Credit Invoice"
        Me.chlcreditinvoice.Visible = False
        '
        'rbAll
        '
        Me.rbAll.Location = New System.Drawing.Point(953, 110)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(33, 18)
        Me.rbAll.TabIndex = 1003
        Me.rbAll.Text = "All"
        '
        'rbFC
        '
        Me.rbFC.Location = New System.Drawing.Point(917, 110)
        Me.rbFC.Name = "rbFC"
        Me.rbFC.Size = New System.Drawing.Size(33, 18)
        Me.rbFC.TabIndex = 1002
        Me.rbFC.Text = "FC"
        '
        'rbFB
        '
        Me.rbFB.Location = New System.Drawing.Point(882, 110)
        Me.rbFB.Name = "rbFB"
        Me.rbFB.Size = New System.Drawing.Size(32, 18)
        Me.rbFB.TabIndex = 1001
        Me.rbFB.Text = "FB"
        '
        'chkInvoice
        '
        Me.chkInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInvoice.Location = New System.Drawing.Point(890, 132)
        Me.chkInvoice.Name = "chkInvoice"
        '
        '
        '
        Me.chkInvoice.RootElement.StretchHorizontally = True
        Me.chkInvoice.RootElement.StretchVertically = True
        Me.chkInvoice.Size = New System.Drawing.Size(95, 16)
        Me.chkInvoice.TabIndex = 25
        Me.chkInvoice.Text = "Create Invoice"
        Me.chkInvoice.Visible = False
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(696, 26)
        Me.chkOnHold.Name = "chkOnHold"
        '
        '
        '
        Me.chkOnHold.RootElement.StretchHorizontally = True
        Me.chkOnHold.RootElement.StretchVertically = True
        Me.chkOnHold.Size = New System.Drawing.Size(60, 16)
        Me.chkOnHold.TabIndex = 7
        Me.chkOnHold.Text = "On Hold"
        Me.chkOnHold.Visible = False
        '
        'chkMultipleOrder
        '
        Me.chkMultipleOrder.Enabled = False
        Me.chkMultipleOrder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMultipleOrder.Location = New System.Drawing.Point(760, 26)
        Me.chkMultipleOrder.Name = "chkMultipleOrder"
        '
        '
        '
        Me.chkMultipleOrder.RootElement.StretchHorizontally = True
        Me.chkMultipleOrder.RootElement.StretchVertically = True
        Me.chkMultipleOrder.Size = New System.Drawing.Size(119, 16)
        Me.chkMultipleOrder.TabIndex = 8
        Me.chkMultipleOrder.Text = "From Multiple Order"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(353, 1)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 20)
        Me.btnReset.TabIndex = 56
        '
        'txtMannualQty
        '
        Me.txtMannualQty.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMannualQty.DecimalPlaces = 2
        Me.txtMannualQty.Location = New System.Drawing.Point(829, 109)
        Me.txtMannualQty.MendatroryField = True
        Me.txtMannualQty.MyLinkLable1 = Me.lblManualQty
        Me.txtMannualQty.MyLinkLable2 = Nothing
        Me.txtMannualQty.Name = "txtMannualQty"
        Me.txtMannualQty.Size = New System.Drawing.Size(48, 20)
        Me.txtMannualQty.TabIndex = 17
        Me.txtMannualQty.Text = "0"
        Me.txtMannualQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMannualQty.Value = 0
        '
        'lblManualQty
        '
        Me.lblManualQty.Location = New System.Drawing.Point(777, 111)
        Me.lblManualQty.Name = "lblManualQty"
        Me.lblManualQty.Size = New System.Drawing.Size(52, 18)
        Me.lblManualQty.TabIndex = 28
        Me.lblManualQty.Text = "Man. Qty"
        '
        'txtEmployeeCode
        '
        Me.txtEmployeeCode.Location = New System.Drawing.Point(80, 89)
        Me.txtEmployeeCode.MendatroryField = False
        Me.txtEmployeeCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployeeCode.MyLinkLable1 = Me.lblemployeecode
        Me.txtEmployeeCode.MyLinkLable2 = Me.lblEmpName
        Me.txtEmployeeCode.MyReadOnly = False
        Me.txtEmployeeCode.Name = "txtEmployeeCode"
        Me.txtEmployeeCode.Size = New System.Drawing.Size(133, 18)
        Me.txtEmployeeCode.TabIndex = 12
        Me.txtEmployeeCode.Value = ""
        '
        'lblemployeecode
        '
        Me.lblemployeecode.Location = New System.Drawing.Point(0, 90)
        Me.lblemployeecode.Name = "lblemployeecode"
        Me.lblemployeecode.Size = New System.Drawing.Size(58, 18)
        Me.lblemployeecode.TabIndex = 23
        Me.lblemployeecode.Text = "Emp Code"
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Location = New System.Drawing.Point(215, 89)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(225, 18)
        Me.lblEmpName.TabIndex = 51
        '
        'txtVehicleCode
        '
        Me.txtVehicleCode.Location = New System.Drawing.Point(80, 68)
        Me.txtVehicleCode.MendatroryField = False
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.lblVehicleCode
        Me.txtVehicleCode.MyLinkLable2 = Nothing
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.Size = New System.Drawing.Size(133, 18)
        Me.txtVehicleCode.TabIndex = 9
        Me.txtVehicleCode.Value = ""
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.Location = New System.Drawing.Point(0, 69)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(72, 18)
        Me.lblVehicleCode.TabIndex = 22
        Me.lblVehicleCode.Text = "Vehicle Code"
        '
        'txtMannualAmt
        '
        Me.txtMannualAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMannualAmt.DecimalPlaces = 2
        Me.txtMannualAmt.Location = New System.Drawing.Point(683, 109)
        Me.txtMannualAmt.MendatroryField = True
        Me.txtMannualAmt.MyLinkLable1 = Me.lblManualAmt
        Me.txtMannualAmt.MyLinkLable2 = Nothing
        Me.txtMannualAmt.Name = "txtMannualAmt"
        Me.txtMannualAmt.Size = New System.Drawing.Size(90, 20)
        Me.txtMannualAmt.TabIndex = 16
        Me.txtMannualAmt.Text = "0"
        Me.txtMannualAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMannualAmt.Value = 0
        '
        'lblManualAmt
        '
        Me.lblManualAmt.Location = New System.Drawing.Point(613, 111)
        Me.lblManualAmt.Name = "lblManualAmt"
        Me.lblManualAmt.Size = New System.Drawing.Size(67, 18)
        Me.lblManualAmt.TabIndex = 27
        Me.lblManualAmt.Text = "Manual Amt"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(696, 2)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel1.TabIndex = 22
        Me.MyLabel1.Text = "Invoice No"
        '
        'CmbTransaction
        '
        Me.CmbTransaction.AllowShowFocusCues = False
        Me.CmbTransaction.AutoCompleteDisplayMember = Nothing
        Me.CmbTransaction.AutoCompleteValueMember = Nothing
        Me.CmbTransaction.CaseSensitive = True
        Me.CmbTransaction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Retail"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Tax"
        RadListDataItem2.TextWrap = True
        Me.CmbTransaction.Items.Add(RadListDataItem1)
        Me.CmbTransaction.Items.Add(RadListDataItem2)
        Me.CmbTransaction.Location = New System.Drawing.Point(805, 151)
        Me.CmbTransaction.MendatroryField = True
        Me.CmbTransaction.MyLinkLable1 = Me.lblTransaction
        Me.CmbTransaction.MyLinkLable2 = Nothing
        Me.CmbTransaction.Name = "CmbTransaction"
        Me.CmbTransaction.Size = New System.Drawing.Size(106, 20)
        Me.CmbTransaction.TabIndex = 23
        Me.CmbTransaction.Text = "Select"
        Me.CmbTransaction.Visible = False
        '
        'lblTransaction
        '
        Me.lblTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransaction.Location = New System.Drawing.Point(705, 153)
        Me.lblTransaction.Name = "lblTransaction"
        Me.lblTransaction.Size = New System.Drawing.Size(94, 16)
        Me.lblTransaction.TabIndex = 39
        Me.lblTransaction.Text = "Transaction Type"
        Me.lblTransaction.Visible = False
        '
        'txtDocNo
        '
        Me.txtDocNo.Location = New System.Drawing.Point(80, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.lblLoadOut
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(273, 20)
        Me.txtDocNo.TabIndex = 25
        Me.txtDocNo.Value = ""
        '
        'lblLoadOut
        '
        Me.lblLoadOut.Location = New System.Drawing.Point(0, 3)
        Me.lblLoadOut.Name = "lblLoadOut"
        Me.lblLoadOut.Size = New System.Drawing.Size(74, 18)
        Me.lblLoadOut.TabIndex = 12
        Me.lblLoadOut.Text = "Load Out No "
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(925, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(61, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 17
        '
        'lblSaleInvoiceNo
        '
        Me.lblSaleInvoiceNo.AutoSize = False
        Me.lblSaleInvoiceNo.BorderVisible = True
        Me.lblSaleInvoiceNo.Location = New System.Drawing.Point(760, 1)
        Me.lblSaleInvoiceNo.Name = "lblSaleInvoiceNo"
        Me.lblSaleInvoiceNo.Size = New System.Drawing.Size(165, 18)
        Me.lblSaleInvoiceNo.TabIndex = 23
        Me.lblSaleInvoiceNo.Text = " "
        '
        'lblShipTo
        '
        Me.lblShipTo.AutoSize = False
        Me.lblShipTo.BorderVisible = True
        Me.lblShipTo.Location = New System.Drawing.Point(215, 153)
        Me.lblShipTo.Name = "lblShipTo"
        Me.lblShipTo.Size = New System.Drawing.Size(225, 17)
        Me.lblShipTo.TabIndex = 35
        '
        'txtShipTo
        '
        Me.txtShipTo.Location = New System.Drawing.Point(80, 152)
        Me.txtShipTo.MendatroryField = False
        Me.txtShipTo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipTo.MyLinkLable1 = Me.MyLabel2
        Me.txtShipTo.MyLinkLable2 = Me.lblShipTo
        Me.txtShipTo.MyReadOnly = False
        Me.txtShipTo.Name = "txtShipTo"
        Me.txtShipTo.Size = New System.Drawing.Size(133, 18)
        Me.txtShipTo.TabIndex = 21
        Me.txtShipTo.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(0, 153)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(44, 18)
        Me.MyLabel2.TabIndex = 53
        Me.MyLabel2.Text = "Ship To"
        '
        'txtTransferNo
        '
        Me.txtTransferNo.Location = New System.Drawing.Point(80, 25)
        Me.txtTransferNo.MendatroryField = True
        Me.txtTransferNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferNo.MyLinkLable1 = Me.lblTransferNo
        Me.txtTransferNo.MyLinkLable2 = Nothing
        Me.txtTransferNo.MyReadOnly = False
        Me.txtTransferNo.Name = "txtTransferNo"
        Me.txtTransferNo.Size = New System.Drawing.Size(133, 19)
        Me.txtTransferNo.TabIndex = 4
        Me.txtTransferNo.Value = ""
        '
        'lblTransferNo
        '
        Me.lblTransferNo.Location = New System.Drawing.Point(0, 26)
        Me.lblTransferNo.Name = "lblTransferNo"
        Me.lblTransferNo.Size = New System.Drawing.Size(65, 18)
        Me.lblTransferNo.TabIndex = 27
        Me.lblTransferNo.Text = "Transfer No"
        '
        'txtKMReading
        '
        Me.txtKMReading.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtKMReading.DecimalPlaces = 0
        Me.txtKMReading.Location = New System.Drawing.Point(576, 130)
        Me.txtKMReading.MendatroryField = True
        Me.txtKMReading.MyLinkLable1 = Me.lblKMReading
        Me.txtKMReading.MyLinkLable2 = Nothing
        Me.txtKMReading.Name = "txtKMReading"
        Me.txtKMReading.Size = New System.Drawing.Size(39, 20)
        Me.txtKMReading.TabIndex = 20
        Me.txtKMReading.Text = "0"
        Me.txtKMReading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtKMReading.Value = 0
        '
        'lblKMReading
        '
        Me.lblKMReading.Location = New System.Drawing.Point(549, 132)
        Me.lblKMReading.Name = "lblKMReading"
        Me.lblKMReading.Size = New System.Drawing.Size(26, 18)
        Me.lblKMReading.TabIndex = 29
        Me.lblKMReading.Text = "KM "
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(80, 46)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(133, 19)
        Me.txtLocation.TabIndex = 6
        Me.txtLocation.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.Location = New System.Drawing.Point(0, 47)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 31
        Me.lblLocation.Text = "Location"
        '
        'txtCustomer
        '
        Me.txtCustomer.Location = New System.Drawing.Point(294, 46)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.lblCustomeNo
        Me.txtCustomer.MyLinkLable2 = Me.txtCustomerName
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(146, 19)
        Me.txtCustomer.TabIndex = 7
        Me.txtCustomer.Value = ""
        '
        'lblCustomeNo
        '
        Me.lblCustomeNo.Location = New System.Drawing.Point(215, 47)
        Me.lblCustomeNo.Name = "lblCustomeNo"
        Me.lblCustomeNo.Size = New System.Drawing.Size(73, 18)
        Me.lblCustomeNo.TabIndex = 10
        Me.lblCustomeNo.Text = "Customer No"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AutoSize = False
        Me.txtCustomerName.BorderVisible = True
        Me.txtCustomerName.Location = New System.Drawing.Point(444, 46)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(251, 19)
        Me.txtCustomerName.TabIndex = 11
        Me.txtCustomerName.TextWrap = False
        '
        'lblLoadType
        '
        Me.lblLoadType.Location = New System.Drawing.Point(587, 3)
        Me.lblLoadType.Name = "lblLoadType"
        Me.lblLoadType.Size = New System.Drawing.Size(30, 18)
        Me.lblLoadType.TabIndex = 17
        Me.lblLoadType.Text = "Type"
        '
        'txtshellqty
        '
        Me.txtshellqty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtshellqty.Location = New System.Drawing.Point(508, 131)
        Me.txtshellqty.MaxLength = 10
        Me.txtshellqty.MendatroryField = False
        Me.txtshellqty.MyLinkLable1 = Me.RadLabel1
        Me.txtshellqty.MyLinkLable2 = Nothing
        Me.txtshellqty.Name = "txtshellqty"
        Me.txtshellqty.Size = New System.Drawing.Size(41, 18)
        Me.txtshellqty.TabIndex = 19
        Me.txtshellqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(444, 132)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(51, 18)
        Me.RadLabel1.TabIndex = 27
        Me.RadLabel1.Text = "Shell Qty"
        '
        'cboLoadOutType
        '
        Me.cboLoadOutType.AllowShowFocusCues = False
        Me.cboLoadOutType.AutoCompleteDisplayMember = Nothing
        Me.cboLoadOutType.AutoCompleteValueMember = Nothing
        Me.cboLoadOutType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem3.Text = "Sale"
        RadListDataItem3.TextWrap = True
        RadListDataItem4.Text = "Transfer"
        RadListDataItem4.TextWrap = True
        Me.cboLoadOutType.Items.Add(RadListDataItem3)
        Me.cboLoadOutType.Items.Add(RadListDataItem4)
        Me.cboLoadOutType.Location = New System.Drawing.Point(624, 2)
        Me.cboLoadOutType.MendatroryField = False
        Me.cboLoadOutType.MyLinkLable1 = Me.lblLoadType
        Me.cboLoadOutType.MyLinkLable2 = Nothing
        Me.cboLoadOutType.Name = "cboLoadOutType"
        Me.cboLoadOutType.Size = New System.Drawing.Size(71, 18)
        Me.cboLoadOutType.TabIndex = 2
        '
        'txtcustomerinvoiceno
        '
        Me.txtcustomerinvoiceno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomerinvoiceno.Location = New System.Drawing.Point(508, 110)
        Me.txtcustomerinvoiceno.MaxLength = 10
        Me.txtcustomerinvoiceno.MendatroryField = False
        Me.txtcustomerinvoiceno.MyLinkLable1 = Me.lblcustomerinvoiceno
        Me.txtcustomerinvoiceno.MyLinkLable2 = Nothing
        Me.txtcustomerinvoiceno.Name = "txtcustomerinvoiceno"
        Me.txtcustomerinvoiceno.Size = New System.Drawing.Size(102, 18)
        Me.txtcustomerinvoiceno.TabIndex = 15
        '
        'lblcustomerinvoiceno
        '
        Me.lblcustomerinvoiceno.Location = New System.Drawing.Point(444, 111)
        Me.lblcustomerinvoiceno.Name = "lblcustomerinvoiceno"
        Me.lblcustomerinvoiceno.Size = New System.Drawing.Size(64, 18)
        Me.lblcustomerinvoiceno.TabIndex = 26
        Me.lblcustomerinvoiceno.Text = "Cust Inv No"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(411, 2)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblShipDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(100, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "18/05/2011"
        Me.txtDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'txtTransferDate
        '
        Me.txtTransferDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtTransferDate.Enabled = False
        Me.txtTransferDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransferDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTransferDate.Location = New System.Drawing.Point(294, 25)
        Me.txtTransferDate.MendatroryField = False
        Me.txtTransferDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTransferDate.MyLinkLable1 = Me.lblTransferDate
        Me.txtTransferDate.MyLinkLable2 = Nothing
        Me.txtTransferDate.Name = "txtTransferDate"
        Me.txtTransferDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTransferDate.Size = New System.Drawing.Size(146, 18)
        Me.txtTransferDate.TabIndex = 4
        Me.txtTransferDate.TabStop = False
        Me.txtTransferDate.Text = "18/05/2011 02:11 PM"
        Me.txtTransferDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'lblTransferDate
        '
        Me.lblTransferDate.Location = New System.Drawing.Point(215, 26)
        Me.lblTransferDate.Name = "lblTransferDate"
        Me.lblTransferDate.Size = New System.Drawing.Size(73, 18)
        Me.lblTransferDate.TabIndex = 28
        Me.lblTransferDate.Text = "Transfer Date"
        '
        'cboPriceDate
        '
        Me.cboPriceDate.AllowShowFocusCues = False
        Me.cboPriceDate.AutoCompleteDisplayMember = Nothing
        Me.cboPriceDate.AutoCompleteValueMember = Nothing
        Me.cboPriceDate.BackColor = System.Drawing.Color.Transparent
        Me.cboPriceDate.Enabled = False
        Me.cboPriceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPriceDate.Location = New System.Drawing.Point(937, 25)
        Me.cboPriceDate.MendatroryField = False
        Me.cboPriceDate.MyLinkLable1 = Me.lblPriceDate
        Me.cboPriceDate.MyLinkLable2 = Nothing
        Me.cboPriceDate.Name = "cboPriceDate"
        Me.cboPriceDate.Size = New System.Drawing.Size(49, 18)
        Me.cboPriceDate.TabIndex = 8
        Me.cboPriceDate.Visible = False
        '
        'lblPriceDate
        '
        Me.lblPriceDate.Location = New System.Drawing.Point(875, 26)
        Me.lblPriceDate.Name = "lblPriceDate"
        Me.lblPriceDate.Size = New System.Drawing.Size(57, 18)
        Me.lblPriceDate.TabIndex = 5
        Me.lblPriceDate.Text = "Price Date"
        Me.lblPriceDate.Visible = False
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(444, 90)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(49, 18)
        Me.lblRemark.TabIndex = 35
        Me.lblRemark.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(508, 89)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.lblRemark
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(477, 18)
        Me.txtRemarks.TabIndex = 13
        '
        'lblTripNo
        '
        Me.lblTripNo.Location = New System.Drawing.Point(549, 153)
        Me.lblTripNo.Name = "lblTripNo"
        Me.lblTripNo.Size = New System.Drawing.Size(44, 18)
        Me.lblTripNo.TabIndex = 37
        Me.lblTripNo.Text = "Trip No"
        '
        'txtTripNo
        '
        Me.txtTripNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripNo.Location = New System.Drawing.Point(597, 152)
        Me.txtTripNo.MaxLength = 10
        Me.txtTripNo.MendatroryField = True
        Me.txtTripNo.MyLinkLable1 = Me.lblTripNo
        Me.txtTripNo.MyLinkLable2 = Nothing
        Me.txtTripNo.Name = "txtTripNo"
        Me.txtTripNo.Size = New System.Drawing.Size(98, 18)
        Me.txtTripNo.TabIndex = 22
        Me.txtTripNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPriceCode
        '
        Me.lblPriceCode.Location = New System.Drawing.Point(696, 69)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceCode.TabIndex = 16
        Me.lblPriceCode.Text = "Price Code"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(80, 131)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.lblDescription
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(360, 18)
        Me.txtDesc.TabIndex = 18
        '
        'lblExpectedShipDate
        '
        Me.lblExpectedShipDate.Location = New System.Drawing.Point(444, 26)
        Me.lblExpectedShipDate.Name = "lblExpectedShipDate"
        Me.lblExpectedShipDate.Size = New System.Drawing.Size(103, 18)
        Me.lblExpectedShipDate.TabIndex = 6
        Me.lblExpectedShipDate.Text = "Expected Ship Date"
        '
        'txtExpectedShipDate
        '
        Me.txtExpectedShipDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtExpectedShipDate.Enabled = False
        Me.txtExpectedShipDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedShipDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtExpectedShipDate.Location = New System.Drawing.Point(558, 25)
        Me.txtExpectedShipDate.MendatroryField = False
        Me.txtExpectedShipDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpectedShipDate.MyLinkLable1 = Me.lblExpectedShipDate
        Me.txtExpectedShipDate.MyLinkLable2 = Nothing
        Me.txtExpectedShipDate.Name = "txtExpectedShipDate"
        Me.txtExpectedShipDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpectedShipDate.Size = New System.Drawing.Size(137, 18)
        Me.txtExpectedShipDate.TabIndex = 5
        Me.txtExpectedShipDate.TabStop = False
        Me.txtExpectedShipDate.Text = "18/05/2011 02:11 PM"
        Me.txtExpectedShipDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'lblCustomerPONo
        '
        Me.lblCustomerPONo.Location = New System.Drawing.Point(696, 47)
        Me.lblCustomerPONo.Name = "lblCustomerPONo"
        Me.lblCustomerPONo.Size = New System.Drawing.Size(67, 18)
        Me.lblCustomerPONo.TabIndex = 13
        Me.lblCustomerPONo.Text = "Cust. PO No"
        '
        'txtCustomerPONO
        '
        Me.txtCustomerPONO.BackColor = System.Drawing.Color.Transparent
        Me.txtCustomerPONO.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txtCustomerPONO.Location = New System.Drawing.Point(769, 46)
        Me.txtCustomerPONO.MaxLength = 50
        Me.txtCustomerPONO.MendatroryField = False
        Me.txtCustomerPONO.MyLinkLable1 = Me.lblCustomerPONo
        Me.txtCustomerPONO.MyLinkLable2 = Nothing
        Me.txtCustomerPONO.Name = "txtCustomerPONO"
        Me.txtCustomerPONO.Size = New System.Drawing.Size(217, 18)
        Me.txtCustomerPONO.TabIndex = 8
        CType(Me.txtCustomerPONO.GetChildAt(0), Telerik.WinControls.UI.RadTextBoxElement).BackColor = System.Drawing.Color.Transparent
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).BackColor2 = System.Drawing.Color.Transparent
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).BackColor3 = System.Drawing.Color.Transparent
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).BackColor4 = System.Drawing.Color.Transparent
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).GradientStyle = Telerik.WinControls.GradientStyles.Solid
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).GradientAngle = 90.0!
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).GradientPercentage = 0.5!
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).GradientPercentage2 = 0.666!
        CType(Me.txtCustomerPONO.GetChildAt(0).GetChildAt(1), Telerik.WinControls.Primitives.FillPrimitive).BackColor = System.Drawing.Color.Transparent
        '
        'lblOrderDate
        '
        Me.lblOrderDate.Location = New System.Drawing.Point(223, 26)
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Size = New System.Drawing.Size(62, 18)
        Me.lblOrderDate.TabIndex = 63
        Me.lblOrderDate.Text = "Order Date"
        '
        'txtOrderDate
        '
        Me.txtOrderDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtOrderDate.Enabled = False
        Me.txtOrderDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtOrderDate.Location = New System.Drawing.Point(294, 25)
        Me.txtOrderDate.MendatroryField = False
        Me.txtOrderDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtOrderDate.MyLinkLable1 = Nothing
        Me.txtOrderDate.MyLinkLable2 = Nothing
        Me.txtOrderDate.Name = "txtOrderDate"
        Me.txtOrderDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtOrderDate.Size = New System.Drawing.Size(146, 18)
        Me.txtOrderDate.TabIndex = 7
        Me.txtOrderDate.TabStop = False
        Me.txtOrderDate.Text = "18/05/2011 02:11 PM"
        Me.txtOrderDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'lblModeofTransport
        '
        Me.lblModeofTransport.Location = New System.Drawing.Point(444, 69)
        Me.lblModeofTransport.Name = "lblModeofTransport"
        Me.lblModeofTransport.Size = New System.Drawing.Size(58, 18)
        Me.lblModeofTransport.TabIndex = 18
        Me.lblModeofTransport.Text = "Tpt. Mode"
        '
        'cboModeOfTransport
        '
        Me.cboModeOfTransport.AllowShowFocusCues = False
        Me.cboModeOfTransport.AutoCompleteDisplayMember = Nothing
        Me.cboModeOfTransport.AutoCompleteValueMember = Nothing
        Me.cboModeOfTransport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem5.Text = "By Road"
        RadListDataItem5.TextWrap = True
        RadListDataItem6.Text = "By Air"
        RadListDataItem6.TextWrap = True
        RadListDataItem7.Text = "By Sea"
        RadListDataItem7.TextWrap = True
        Me.cboModeOfTransport.Items.Add(RadListDataItem5)
        Me.cboModeOfTransport.Items.Add(RadListDataItem6)
        Me.cboModeOfTransport.Items.Add(RadListDataItem7)
        Me.cboModeOfTransport.Location = New System.Drawing.Point(508, 68)
        Me.cboModeOfTransport.MendatroryField = False
        Me.cboModeOfTransport.MyLinkLable1 = Me.lblModeofTransport
        Me.cboModeOfTransport.MyLinkLable2 = Nothing
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.Size = New System.Drawing.Size(187, 18)
        Me.cboModeOfTransport.TabIndex = 11
        Me.cboModeOfTransport.Text = "Select"
        '
        'pageTaxDetails
        '
        Me.pageTaxDetails.Controls.Add(Me.lblTaxDesc)
        Me.pageTaxDetails.Controls.Add(Me.lblRouteDesc)
        Me.pageTaxDetails.Controls.Add(Me.txtPaymentTerm)
        Me.pageTaxDetails.Controls.Add(Me.txtRouteNo)
        Me.pageTaxDetails.Controls.Add(Me.lblRefNo)
        Me.pageTaxDetails.Controls.Add(Me.txtRef)
        Me.pageTaxDetails.Controls.Add(Me.lblPaymentTerms)
        Me.pageTaxDetails.Controls.Add(Me.lblTaxGroup)
        Me.pageTaxDetails.Controls.Add(Me.fndTaxGroup)
        Me.pageTaxDetails.Controls.Add(Me.lblRouteNo)
        Me.pageTaxDetails.Controls.Add(Me.gbTaxDetails)
        Me.pageTaxDetails.Controls.Add(Me.txtSchemeSample)
        Me.pageTaxDetails.Controls.Add(Me.lblSchemeSample)
        Me.pageTaxDetails.ItemSize = New System.Drawing.SizeF(73.0!, 26.0!)
        Me.pageTaxDetails.Location = New System.Drawing.Point(10, 35)
        Me.pageTaxDetails.Name = "pageTaxDetails"
        Me.pageTaxDetails.Size = New System.Drawing.Size(991, 414)
        Me.pageTaxDetails.Text = "Tax Details"
        '
        'lblTaxDesc
        '
        Me.lblTaxDesc.AutoSize = False
        Me.lblTaxDesc.BorderVisible = True
        Me.lblTaxDesc.Location = New System.Drawing.Point(234, 47)
        Me.lblTaxDesc.Name = "lblTaxDesc"
        Me.lblTaxDesc.Size = New System.Drawing.Size(225, 17)
        Me.lblTaxDesc.TabIndex = 118
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.Location = New System.Drawing.Point(234, 16)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(225, 17)
        Me.lblRouteDesc.TabIndex = 117
        '
        'txtPaymentTerm
        '
        Me.txtPaymentTerm.Location = New System.Drawing.Point(557, 39)
        Me.txtPaymentTerm.MendatroryField = False
        Me.txtPaymentTerm.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentTerm.MyLinkLable1 = Me.lblRouteNo
        Me.txtPaymentTerm.MyLinkLable2 = Nothing
        Me.txtPaymentTerm.MyReadOnly = False
        Me.txtPaymentTerm.Name = "txtPaymentTerm"
        Me.txtPaymentTerm.Size = New System.Drawing.Size(133, 18)
        Me.txtPaymentTerm.TabIndex = 3
        Me.txtPaymentTerm.Value = ""
        '
        'lblRouteNo
        '
        Me.lblRouteNo.Location = New System.Drawing.Point(9, 16)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 102
        Me.lblRouteNo.Text = "Route No"
        '
        'txtRouteNo
        '
        Me.txtRouteNo.Location = New System.Drawing.Point(95, 15)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Me.lblRouteNo
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.Size = New System.Drawing.Size(133, 18)
        Me.txtRouteNo.TabIndex = 0
        Me.txtRouteNo.Value = ""
        '
        'lblRefNo
        '
        Me.lblRefNo.Location = New System.Drawing.Point(9, 76)
        Me.lblRefNo.Name = "lblRefNo"
        Me.lblRefNo.Size = New System.Drawing.Size(74, 18)
        Me.lblRefNo.TabIndex = 113
        Me.lblRefNo.Text = "Reference No"
        '
        'txtRef
        '
        Me.txtRef.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRef.Location = New System.Drawing.Point(95, 76)
        Me.txtRef.MaxLength = 50
        Me.txtRef.MendatroryField = False
        Me.txtRef.MyLinkLable1 = Nothing
        Me.txtRef.MyLinkLable2 = Nothing
        Me.txtRef.Name = "txtRef"
        Me.txtRef.Size = New System.Drawing.Size(360, 18)
        Me.txtRef.TabIndex = 4
        '
        'lblPaymentTerms
        '
        Me.lblPaymentTerms.Location = New System.Drawing.Point(467, 40)
        Me.lblPaymentTerms.Name = "lblPaymentTerms"
        Me.lblPaymentTerms.Size = New System.Drawing.Size(83, 18)
        Me.lblPaymentTerms.TabIndex = 108
        Me.lblPaymentTerms.Text = "Payment Terms"
        '
        'lblTaxGroup
        '
        Me.lblTaxGroup.Location = New System.Drawing.Point(9, 48)
        Me.lblTaxGroup.Name = "lblTaxGroup"
        Me.lblTaxGroup.Size = New System.Drawing.Size(58, 18)
        Me.lblTaxGroup.TabIndex = 107
        Me.lblTaxGroup.Text = "Tax Group"
        '
        'fndTaxGroup
        '
        Me.fndTaxGroup.BackColor = System.Drawing.Color.Transparent
        Me.fndTaxGroup.Caption = Nothing
        Me.fndTaxGroup.ConnectionString = Nothing
        Me.fndTaxGroup.Enabled = False
        Me.fndTaxGroup.Icon = Nothing
        Me.fndTaxGroup.Location = New System.Drawing.Point(95, 44)
        Me.fndTaxGroup.Margin = New System.Windows.Forms.Padding(0)
        Me.fndTaxGroup.MinimumSize = New System.Drawing.Size(117, 20)
        Me.fndTaxGroup.Name = "fndTaxGroup"
        Me.fndTaxGroup.NewTimer = Nothing
        Me.fndTaxGroup.Query = Nothing
        Me.fndTaxGroup.ResultDT = Nothing
        Me.fndTaxGroup.SelectedRowDR = Nothing
        Me.fndTaxGroup.SelectedValue = Nothing
        Me.fndTaxGroup.SelectedValue1 = Nothing
        Me.fndTaxGroup.Size = New System.Drawing.Size(134, 20)
        Me.fndTaxGroup.TabIndex = 2
        Me.fndTaxGroup.ValueToSelect = Nothing
        Me.fndTaxGroup.ValueToSelect1 = Nothing
        '
        'gbTaxDetails
        '
        Me.gbTaxDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbTaxDetails.Controls.Add(Me.gvTax)
        Me.gbTaxDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTaxDetails.HeaderText = "Tax Details"
        Me.gbTaxDetails.Location = New System.Drawing.Point(3, 112)
        Me.gbTaxDetails.Name = "gbTaxDetails"
        Me.gbTaxDetails.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbTaxDetails.Size = New System.Drawing.Size(940, 297)
        Me.gbTaxDetails.TabIndex = 8
        Me.gbTaxDetails.Text = "Tax Details"
        '
        'gvTax
        '
        Me.gvTax.BackColor = System.Drawing.Color.White
        Me.gvTax.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTax.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvTax.ForeColor = System.Drawing.Color.Black
        Me.gvTax.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTax.Location = New System.Drawing.Point(13, 23)
        '
        '
        '
        Me.gvTax.MasterTemplate.AllowAddNewRow = False
        Me.gvTax.MasterTemplate.AllowColumnReorder = False
        Me.gvTax.MasterTemplate.AllowDeleteRow = False
        Me.gvTax.MasterTemplate.AutoGenerateColumns = False
        GridViewTextBoxColumn1.FieldName = "Tax_Code"
        GridViewTextBoxColumn1.HeaderText = "Tax Authority"
        GridViewTextBoxColumn1.Name = "taxAuthority"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 140
        GridViewTextBoxColumn2.FieldName = "Tax_Code_Desc"
        GridViewTextBoxColumn2.HeaderText = "Description"
        GridViewTextBoxColumn2.Name = "description"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 265
        GridViewComboBoxColumn1.FieldName = "Tax_Rate"
        GridViewComboBoxColumn1.HeaderText = "Rate"
        GridViewComboBoxColumn1.Name = "taxRate"
        GridViewComboBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewComboBoxColumn1.Width = 100
        GridViewTextBoxColumn3.HeaderText = "Basic Amount"
        GridViewTextBoxColumn3.Name = "basicAmount"
        GridViewTextBoxColumn3.ReadOnly = True
        GridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn3.Width = 140
        GridViewTextBoxColumn4.HeaderText = "Assessible Amount"
        GridViewTextBoxColumn4.Name = "assessibleAmount"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn4.Width = 140
        GridViewTextBoxColumn5.HeaderText = "Tax Amount"
        GridViewTextBoxColumn5.Name = "taxAmount"
        GridViewTextBoxColumn5.ReadOnly = True
        GridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        GridViewTextBoxColumn5.Width = 115
        GridViewTextBoxColumn6.FieldName = "Taxable"
        GridViewTextBoxColumn6.HeaderText = "Taxable"
        GridViewTextBoxColumn6.IsVisible = False
        GridViewTextBoxColumn6.Name = "taxable"
        GridViewTextBoxColumn7.FieldName = "Surtax"
        GridViewTextBoxColumn7.HeaderText = "Surtax"
        GridViewTextBoxColumn7.IsVisible = False
        GridViewTextBoxColumn7.Name = "surtax"
        GridViewTextBoxColumn8.FieldName = "Surtax_Tax_Code"
        GridViewTextBoxColumn8.HeaderText = "Surtax_Code"
        GridViewTextBoxColumn8.IsVisible = False
        GridViewTextBoxColumn8.Name = "surtaxCode"
        GridViewTextBoxColumn9.HeaderText = "Item Assess"
        GridViewTextBoxColumn9.IsVisible = False
        GridViewTextBoxColumn9.Name = "itemAssess"
        GridViewTextBoxColumn10.HeaderText = "Item Tax Amt"
        GridViewTextBoxColumn10.IsVisible = False
        GridViewTextBoxColumn10.Name = "itemTaxAmt"
        Me.gvTax.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewComboBoxColumn1, GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewTextBoxColumn5, GridViewTextBoxColumn6, GridViewTextBoxColumn7, GridViewTextBoxColumn8, GridViewTextBoxColumn9, GridViewTextBoxColumn10})
        Me.gvTax.MasterTemplate.EnableGrouping = False
        Me.gvTax.Name = "gvTax"
        Me.gvTax.ReadOnly = True
        Me.gvTax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTax.ShowGroupPanel = False
        Me.gvTax.Size = New System.Drawing.Size(918, 261)
        Me.gvTax.TabIndex = 0
        Me.gvTax.Text = "GV Tax Details"
        '
        'txtSchemeSample
        '
        Me.txtSchemeSample.BackColor = System.Drawing.Color.Transparent
        Me.txtSchemeSample.Caption = Nothing
        Me.txtSchemeSample.ConnectionString = Nothing
        Me.txtSchemeSample.Icon = Nothing
        Me.txtSchemeSample.Location = New System.Drawing.Point(557, 60)
        Me.txtSchemeSample.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSchemeSample.MinimumSize = New System.Drawing.Size(117, 20)
        Me.txtSchemeSample.Name = "txtSchemeSample"
        Me.txtSchemeSample.NewTimer = Nothing
        Me.txtSchemeSample.Query = Nothing
        Me.txtSchemeSample.ResultDT = Nothing
        Me.txtSchemeSample.SelectedRowDR = Nothing
        Me.txtSchemeSample.SelectedValue = Nothing
        Me.txtSchemeSample.SelectedValue1 = Nothing
        Me.txtSchemeSample.Size = New System.Drawing.Size(136, 20)
        Me.txtSchemeSample.TabIndex = 24
        Me.txtSchemeSample.ValueToSelect = Nothing
        Me.txtSchemeSample.ValueToSelect1 = Nothing
        Me.txtSchemeSample.Visible = False
        '
        'lblSchemeSample
        '
        Me.lblSchemeSample.Location = New System.Drawing.Point(466, 62)
        Me.lblSchemeSample.Name = "lblSchemeSample"
        Me.lblSchemeSample.Size = New System.Drawing.Size(85, 18)
        Me.lblSchemeSample.TabIndex = 30
        Me.lblSchemeSample.Text = "Scheme Sample"
        Me.lblSchemeSample.Visible = False
        '
        'pageTotal
        '
        Me.pageTotal.Controls.Add(Me.btnRecalTransAndReCreateJE)
        Me.pageTotal.Controls.Add(Me.pnlMannualInvoiceNo)
        Me.pageTotal.Controls.Add(Me.btnCreateRSSaleType)
        Me.pageTotal.Controls.Add(Me.btnReverse)
        Me.pageTotal.Controls.Add(Me.RadGroupBox3)
        Me.pageTotal.Controls.Add(Me.RadGroupBox2)
        Me.pageTotal.Controls.Add(Me.btnDeleteRS)
        Me.pageTotal.Controls.Add(Me.btnDeleteRSSaleType)
        Me.pageTotal.Controls.Add(Me.btnCreateRS)
        Me.pageTotal.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.pageTotal.Location = New System.Drawing.Point(10, 35)
        Me.pageTotal.Name = "pageTotal"
        Me.pageTotal.Size = New System.Drawing.Size(991, 414)
        Me.pageTotal.Text = "Total"
        '
        'btnRecalTransAndReCreateJE
        '
        Me.btnRecalTransAndReCreateJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRecalTransAndReCreateJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecalTransAndReCreateJE.Location = New System.Drawing.Point(673, 118)
        Me.btnRecalTransAndReCreateJE.Name = "btnRecalTransAndReCreateJE"
        Me.btnRecalTransAndReCreateJE.Size = New System.Drawing.Size(268, 18)
        Me.btnRecalTransAndReCreateJE.TabIndex = 88
        Me.btnRecalTransAndReCreateJE.Text = "Recalculate Transaction And Recreate JE"
        Me.btnRecalTransAndReCreateJE.Visible = False
        '
        'pnlMannualInvoiceNo
        '
        Me.pnlMannualInvoiceNo.Controls.Add(Me.MyLabel4)
        Me.pnlMannualInvoiceNo.Controls.Add(Me.txtMannaulInvoiceNo)
        Me.pnlMannualInvoiceNo.Location = New System.Drawing.Point(8, 296)
        Me.pnlMannualInvoiceNo.Name = "pnlMannualInvoiceNo"
        Me.pnlMannualInvoiceNo.Size = New System.Drawing.Size(350, 26)
        Me.pnlMannualInvoiceNo.TabIndex = 87
        Me.pnlMannualInvoiceNo.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.Location = New System.Drawing.Point(3, 5)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(107, 18)
        Me.MyLabel4.TabIndex = 86
        Me.MyLabel4.Text = "Mannual Invoice No"
        '
        'txtMannaulInvoiceNo
        '
        Me.txtMannaulInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMannaulInvoiceNo.Location = New System.Drawing.Point(115, 4)
        Me.txtMannaulInvoiceNo.MaxLength = 50
        Me.txtMannaulInvoiceNo.MendatroryField = False
        Me.txtMannaulInvoiceNo.MyLinkLable1 = Me.MyLabel4
        Me.txtMannaulInvoiceNo.MyLinkLable2 = Nothing
        Me.txtMannaulInvoiceNo.Name = "txtMannaulInvoiceNo"
        Me.txtMannaulInvoiceNo.Size = New System.Drawing.Size(229, 18)
        Me.txtMannaulInvoiceNo.TabIndex = 85
        '
        'btnCreateRSSaleType
        '
        Me.btnCreateRSSaleType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreateRSSaleType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateRSSaleType.Location = New System.Drawing.Point(673, 183)
        Me.btnCreateRSSaleType.Name = "btnCreateRSSaleType"
        Me.btnCreateRSSaleType.Size = New System.Drawing.Size(268, 18)
        Me.btnCreateRSSaleType.TabIndex = 3
        Me.btnCreateRSSaleType.Text = "For Create (Tecxpert Only Sale Type)"
        Me.btnCreateRSSaleType.Visible = False
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(673, 246)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(268, 18)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse and Recreate"
        Me.btnReverse.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadRadioButton1)
        Me.RadGroupBox3.Controls.Add(Me.RadRadioButton2)
        Me.RadGroupBox3.Controls.Add(Me.lblAddCharges)
        Me.RadGroupBox3.Controls.Add(Me.lblOtherCharges)
        Me.RadGroupBox3.Controls.Add(Me.txtAdditionalCharges)
        Me.RadGroupBox3.Controls.Add(Me.txtOtherCharges)
        Me.RadGroupBox3.Controls.Add(Me.txtFreight)
        Me.RadGroupBox3.Controls.Add(Me.lblFreight)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(355, 16)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(299, 82)
        Me.RadGroupBox3.TabIndex = 84
        Me.RadGroupBox3.Visible = False
        '
        'RadRadioButton1
        '
        Me.RadRadioButton1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RadRadioButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadRadioButton1.Location = New System.Drawing.Point(82, 53)
        Me.RadRadioButton1.Name = "RadRadioButton1"
        Me.RadRadioButton1.Size = New System.Drawing.Size(35, 16)
        Me.RadRadioButton1.TabIndex = 3
        Me.RadRadioButton1.TabStop = True
        Me.RadRadioButton1.Text = "Fix"
        Me.RadRadioButton1.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.RadRadioButton1.Visible = False
        '
        'RadRadioButton2
        '
        Me.RadRadioButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadRadioButton2.Location = New System.Drawing.Point(51, 53)
        Me.RadRadioButton2.Name = "RadRadioButton2"
        Me.RadRadioButton2.Size = New System.Drawing.Size(30, 16)
        Me.RadRadioButton2.TabIndex = 2
        Me.RadRadioButton2.Text = "%"
        Me.RadRadioButton2.Visible = False
        '
        'lblAddCharges
        '
        Me.lblAddCharges.Location = New System.Drawing.Point(13, 9)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(101, 18)
        Me.lblAddCharges.TabIndex = 80
        Me.lblAddCharges.Text = "Additional Charges"
        '
        'lblOtherCharges
        '
        Me.lblOtherCharges.Location = New System.Drawing.Point(15, 33)
        Me.lblOtherCharges.Name = "lblOtherCharges"
        Me.lblOtherCharges.Size = New System.Drawing.Size(78, 18)
        Me.lblOtherCharges.TabIndex = 79
        Me.lblOtherCharges.Text = "Other Charges"
        '
        'txtAdditionalCharges
        '
        Me.txtAdditionalCharges.AutoSize = False
        Me.txtAdditionalCharges.Location = New System.Drawing.Point(121, 9)
        Me.txtAdditionalCharges.MaxLength = 10
        Me.txtAdditionalCharges.MendatroryField = False
        Me.txtAdditionalCharges.Multiline = True
        Me.txtAdditionalCharges.MyLinkLable1 = Nothing
        Me.txtAdditionalCharges.MyLinkLable2 = Nothing
        Me.txtAdditionalCharges.Name = "txtAdditionalCharges"
        Me.txtAdditionalCharges.Size = New System.Drawing.Size(170, 18)
        Me.txtAdditionalCharges.TabIndex = 0
        Me.txtAdditionalCharges.Text = "0.0"
        Me.txtAdditionalCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOtherCharges
        '
        Me.txtOtherCharges.AutoSize = False
        Me.txtOtherCharges.Location = New System.Drawing.Point(121, 33)
        Me.txtOtherCharges.MaxLength = 10
        Me.txtOtherCharges.MendatroryField = False
        Me.txtOtherCharges.Multiline = True
        Me.txtOtherCharges.MyLinkLable1 = Nothing
        Me.txtOtherCharges.MyLinkLable2 = Nothing
        Me.txtOtherCharges.Name = "txtOtherCharges"
        Me.txtOtherCharges.Size = New System.Drawing.Size(170, 18)
        Me.txtOtherCharges.TabIndex = 1
        Me.txtOtherCharges.Text = "0.0"
        Me.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFreight
        '
        Me.txtFreight.AutoSize = False
        Me.txtFreight.Location = New System.Drawing.Point(121, 55)
        Me.txtFreight.MaxLength = 10
        Me.txtFreight.MendatroryField = False
        Me.txtFreight.Multiline = True
        Me.txtFreight.MyLinkLable1 = Nothing
        Me.txtFreight.MyLinkLable2 = Nothing
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.Size = New System.Drawing.Size(170, 18)
        Me.txtFreight.TabIndex = 4
        Me.txtFreight.Text = "0.0"
        Me.txtFreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblFreight
        '
        Me.lblFreight.Location = New System.Drawing.Point(13, 55)
        Me.lblFreight.Name = "lblFreight"
        Me.lblFreight.Size = New System.Drawing.Size(41, 18)
        Me.lblFreight.TabIndex = 77
        Me.lblFreight.Text = "Freight"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.txtDiscAmt)
        Me.RadGroupBox2.Controls.Add(Me.txtDiscPer)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel12)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel11)
        Me.RadGroupBox2.Controls.Add(Me.txtTotalShipmentAmt)
        Me.RadGroupBox2.Controls.Add(Me.TextBox3)
        Me.RadGroupBox2.Controls.Add(Me.lblShipmentAmt)
        Me.RadGroupBox2.Controls.Add(Me.txtContainerDeposit)
        Me.RadGroupBox2.Controls.Add(Me.txtShipmentAmt)
        Me.RadGroupBox2.Controls.Add(Me.TextBox2)
        Me.RadGroupBox2.Controls.Add(Me.TextBox1)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox2.Controls.Add(Me.txtTotalTPT)
        Me.RadGroupBox2.Controls.Add(Me.rdbFix)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel10)
        Me.RadGroupBox2.Controls.Add(Me.txtNetShipAmt)
        Me.RadGroupBox2.Controls.Add(Me.rdbPer)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel9)
        Me.RadGroupBox2.Controls.Add(Me.lblTotalTaxAmt)
        Me.RadGroupBox2.Controls.Add(Me.txtTotalTaxAmount)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox2.Controls.Add(Me.lblShipmentTotal)
        Me.RadGroupBox2.Controls.Add(Me.txtShipmentTotal)
        Me.RadGroupBox2.Controls.Add(Me.lblDiscountPer)
        Me.RadGroupBox2.Controls.Add(Me.txtCustDisc)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(8, 16)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(341, 274)
        Me.RadGroupBox2.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Location = New System.Drawing.Point(10, 71)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel3.TabIndex = 87
        Me.MyLabel3.Text = "Discount On"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkDiscountOnAmt)
        Me.RadGroupBox1.Controls.Add(Me.chkDiscountOnRate)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(181, 67)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(141, 21)
        Me.RadGroupBox1.TabIndex = 4
        '
        'chkDiscountOnAmt
        '
        Me.chkDiscountOnAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiscountOnAmt.Location = New System.Drawing.Point(66, 2)
        Me.chkDiscountOnAmt.Name = "chkDiscountOnAmt"
        Me.chkDiscountOnAmt.Size = New System.Drawing.Size(59, 16)
        Me.chkDiscountOnAmt.TabIndex = 1
        Me.chkDiscountOnAmt.Text = "Amount"
        '
        'chkDiscountOnRate
        '
        Me.chkDiscountOnRate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDiscountOnRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiscountOnRate.Location = New System.Drawing.Point(15, 2)
        Me.chkDiscountOnRate.Name = "chkDiscountOnRate"
        Me.chkDiscountOnRate.Size = New System.Drawing.Size(44, 16)
        Me.chkDiscountOnRate.TabIndex = 0
        Me.chkDiscountOnRate.TabStop = True
        Me.chkDiscountOnRate.Text = "Rate"
        Me.chkDiscountOnRate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtDiscAmt
        '
        Me.txtDiscAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDiscAmt.DecimalPlaces = 5
        Me.txtDiscAmt.Location = New System.Drawing.Point(242, 91)
        Me.txtDiscAmt.MendatroryField = True
        Me.txtDiscAmt.MyLinkLable1 = Me.lblKMReading
        Me.txtDiscAmt.MyLinkLable2 = Nothing
        Me.txtDiscAmt.Name = "txtDiscAmt"
        Me.txtDiscAmt.Size = New System.Drawing.Size(80, 20)
        Me.txtDiscAmt.TabIndex = 6
        Me.txtDiscAmt.Text = "0"
        Me.txtDiscAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscAmt.Value = 0
        '
        'txtDiscPer
        '
        Me.txtDiscPer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDiscPer.DecimalPlaces = 5
        Me.txtDiscPer.Location = New System.Drawing.Point(181, 91)
        Me.txtDiscPer.MendatroryField = True
        Me.txtDiscPer.MyLinkLable1 = Me.lblKMReading
        Me.txtDiscPer.MyLinkLable2 = Nothing
        Me.txtDiscPer.Name = "txtDiscPer"
        Me.txtDiscPer.Size = New System.Drawing.Size(39, 20)
        Me.txtDiscPer.TabIndex = 5
        Me.txtDiscPer.Text = "0"
        Me.txtDiscPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscPer.Value = 0
        '
        'RadLabel12
        '
        Me.RadLabel12.Location = New System.Drawing.Point(10, 242)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(125, 18)
        Me.RadLabel12.TabIndex = 21
        Me.RadLabel12.Text = "Total Shipment Amount"
        '
        'RadLabel11
        '
        Me.RadLabel11.Location = New System.Drawing.Point(10, 218)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(120, 18)
        Me.RadLabel11.TabIndex = 20
        Me.RadLabel11.Text = "Plus Container Deposit"
        '
        'txtTotalShipmentAmt
        '
        Me.txtTotalShipmentAmt.AutoSize = False
        Me.txtTotalShipmentAmt.Location = New System.Drawing.Point(181, 242)
        Me.txtTotalShipmentAmt.MendatroryField = False
        Me.txtTotalShipmentAmt.Multiline = True
        Me.txtTotalShipmentAmt.MyLinkLable1 = Me.RadLabel12
        Me.txtTotalShipmentAmt.MyLinkLable2 = Nothing
        Me.txtTotalShipmentAmt.Name = "txtTotalShipmentAmt"
        Me.txtTotalShipmentAmt.ReadOnly = True
        Me.txtTotalShipmentAmt.ShowItemToolTips = False
        Me.txtTotalShipmentAmt.Size = New System.Drawing.Size(142, 18)
        Me.txtTotalShipmentAmt.TabIndex = 12
        Me.txtTotalShipmentAmt.Text = "0.0"
        Me.txtTotalShipmentAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.WindowText
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Location = New System.Drawing.Point(180, 239)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(142, 1)
        Me.TextBox3.TabIndex = 11
        '
        'lblShipmentAmt
        '
        Me.lblShipmentAmt.Location = New System.Drawing.Point(10, 194)
        Me.lblShipmentAmt.Name = "lblShipmentAmt"
        Me.lblShipmentAmt.Size = New System.Drawing.Size(97, 18)
        Me.lblShipmentAmt.TabIndex = 19
        Me.lblShipmentAmt.Text = "Shipment Amount"
        '
        'txtContainerDeposit
        '
        Me.txtContainerDeposit.AutoSize = False
        Me.txtContainerDeposit.Location = New System.Drawing.Point(181, 218)
        Me.txtContainerDeposit.MaxLength = 10
        Me.txtContainerDeposit.MendatroryField = False
        Me.txtContainerDeposit.Multiline = True
        Me.txtContainerDeposit.MyLinkLable1 = Me.RadLabel11
        Me.txtContainerDeposit.MyLinkLable2 = Nothing
        Me.txtContainerDeposit.Name = "txtContainerDeposit"
        Me.txtContainerDeposit.ReadOnly = True
        Me.txtContainerDeposit.Size = New System.Drawing.Size(141, 18)
        Me.txtContainerDeposit.TabIndex = 11
        Me.txtContainerDeposit.Text = "0.0"
        Me.txtContainerDeposit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtShipmentAmt
        '
        Me.txtShipmentAmt.AutoSize = False
        Me.txtShipmentAmt.Location = New System.Drawing.Point(180, 194)
        Me.txtShipmentAmt.MendatroryField = False
        Me.txtShipmentAmt.Multiline = True
        Me.txtShipmentAmt.MyLinkLable1 = Me.lblShipmentAmt
        Me.txtShipmentAmt.MyLinkLable2 = Nothing
        Me.txtShipmentAmt.Name = "txtShipmentAmt"
        Me.txtShipmentAmt.ReadOnly = True
        Me.txtShipmentAmt.ShowItemToolTips = False
        Me.txtShipmentAmt.Size = New System.Drawing.Size(142, 18)
        Me.txtShipmentAmt.TabIndex = 10
        Me.txtShipmentAmt.Text = "0.0"
        Me.txtShipmentAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.WindowText
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Location = New System.Drawing.Point(180, 189)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(142, 1)
        Me.TextBox2.TabIndex = 8
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.WindowText
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(180, 115)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(142, 1)
        Me.TextBox1.TabIndex = 7
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(10, 167)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(48, 18)
        Me.RadLabel2.TabIndex = 18
        Me.RadLabel2.Text = "Plus TPT"
        '
        'txtTotalTPT
        '
        Me.txtTotalTPT.AutoSize = False
        Me.txtTotalTPT.Location = New System.Drawing.Point(181, 167)
        Me.txtTotalTPT.MaxLength = 10
        Me.txtTotalTPT.MendatroryField = False
        Me.txtTotalTPT.Multiline = True
        Me.txtTotalTPT.MyLinkLable1 = Me.RadLabel2
        Me.txtTotalTPT.MyLinkLable2 = Nothing
        Me.txtTotalTPT.Name = "txtTotalTPT"
        Me.txtTotalTPT.ReadOnly = True
        Me.txtTotalTPT.Size = New System.Drawing.Size(141, 18)
        Me.txtTotalTPT.TabIndex = 9
        Me.txtTotalTPT.Text = "0.0"
        Me.txtTotalTPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdbFix
        '
        Me.rdbFix.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbFix.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbFix.Location = New System.Drawing.Point(273, 3)
        Me.rdbFix.Name = "rdbFix"
        Me.rdbFix.Size = New System.Drawing.Size(35, 16)
        Me.rdbFix.TabIndex = 1
        Me.rdbFix.TabStop = True
        Me.rdbFix.Text = "Fix"
        Me.rdbFix.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.rdbFix.Visible = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Location = New System.Drawing.Point(10, 119)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(95, 18)
        Me.RadLabel10.TabIndex = 16
        Me.RadLabel10.Text = "Net Ship. Amount"
        '
        'txtNetShipAmt
        '
        Me.txtNetShipAmt.AutoSize = False
        Me.txtNetShipAmt.Location = New System.Drawing.Point(181, 119)
        Me.txtNetShipAmt.MendatroryField = False
        Me.txtNetShipAmt.Multiline = True
        Me.txtNetShipAmt.MyLinkLable1 = Me.RadLabel10
        Me.txtNetShipAmt.MyLinkLable2 = Nothing
        Me.txtNetShipAmt.Name = "txtNetShipAmt"
        Me.txtNetShipAmt.ReadOnly = True
        Me.txtNetShipAmt.Size = New System.Drawing.Size(141, 18)
        Me.txtNetShipAmt.TabIndex = 5
        Me.txtNetShipAmt.Text = "0.0"
        Me.txtNetShipAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdbPer
        '
        Me.rdbPer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbPer.Location = New System.Drawing.Point(237, 3)
        Me.rdbPer.Name = "rdbPer"
        Me.rdbPer.Size = New System.Drawing.Size(30, 16)
        Me.rdbPer.TabIndex = 0
        Me.rdbPer.Text = "%"
        Me.rdbPer.Visible = False
        '
        'RadLabel9
        '
        Me.RadLabel9.Location = New System.Drawing.Point(223, 93)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(15, 18)
        Me.RadLabel9.TabIndex = 79
        Me.RadLabel9.Text = "%"
        '
        'lblTotalTaxAmt
        '
        Me.lblTotalTaxAmt.Location = New System.Drawing.Point(10, 143)
        Me.lblTotalTaxAmt.Name = "lblTotalTaxAmt"
        Me.lblTotalTaxAmt.Size = New System.Drawing.Size(90, 18)
        Me.lblTotalTaxAmt.TabIndex = 17
        Me.lblTotalTaxAmt.Text = "Plus Tax Amount"
        '
        'txtTotalTaxAmount
        '
        Me.txtTotalTaxAmount.AutoSize = False
        Me.txtTotalTaxAmount.Location = New System.Drawing.Point(181, 143)
        Me.txtTotalTaxAmount.MendatroryField = False
        Me.txtTotalTaxAmount.Multiline = True
        Me.txtTotalTaxAmount.MyLinkLable1 = Me.lblTotalTaxAmt
        Me.txtTotalTaxAmount.MyLinkLable2 = Nothing
        Me.txtTotalTaxAmount.Name = "txtTotalTaxAmount"
        Me.txtTotalTaxAmount.ReadOnly = True
        Me.txtTotalTaxAmount.Size = New System.Drawing.Size(141, 18)
        Me.txtTotalTaxAmount.TabIndex = 8
        Me.txtTotalTaxAmount.Text = "0.0"
        Me.txtTotalTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(10, 93)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(102, 18)
        Me.RadLabel8.TabIndex = 15
        Me.RadLabel8.Text = "Less Ship. Discount"
        '
        'lblShipmentTotal
        '
        Me.lblShipmentTotal.Location = New System.Drawing.Point(10, 22)
        Me.lblShipmentTotal.Name = "lblShipmentTotal"
        Me.lblShipmentTotal.Size = New System.Drawing.Size(82, 18)
        Me.lblShipmentTotal.TabIndex = 13
        Me.lblShipmentTotal.Text = "Shipment Total"
        '
        'txtShipmentTotal
        '
        Me.txtShipmentTotal.AutoSize = False
        Me.txtShipmentTotal.Location = New System.Drawing.Point(181, 22)
        Me.txtShipmentTotal.MendatroryField = False
        Me.txtShipmentTotal.Multiline = True
        Me.txtShipmentTotal.MyLinkLable1 = Me.lblShipmentTotal
        Me.txtShipmentTotal.MyLinkLable2 = Nothing
        Me.txtShipmentTotal.Name = "txtShipmentTotal"
        Me.txtShipmentTotal.ReadOnly = True
        Me.txtShipmentTotal.Size = New System.Drawing.Size(141, 18)
        Me.txtShipmentTotal.TabIndex = 2
        Me.txtShipmentTotal.Text = "0.0"
        Me.txtShipmentTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDiscountPer
        '
        Me.lblDiscountPer.Location = New System.Drawing.Point(10, 46)
        Me.lblDiscountPer.Name = "lblDiscountPer"
        Me.lblDiscountPer.Size = New System.Drawing.Size(105, 18)
        Me.lblDiscountPer.TabIndex = 14
        Me.lblDiscountPer.Text = "Customer Discount "
        '
        'txtCustDisc
        '
        Me.txtCustDisc.AutoSize = False
        Me.txtCustDisc.Location = New System.Drawing.Point(181, 45)
        Me.txtCustDisc.MaxLength = 10
        Me.txtCustDisc.MendatroryField = False
        Me.txtCustDisc.Multiline = True
        Me.txtCustDisc.MyLinkLable1 = Me.lblDiscountPer
        Me.txtCustDisc.MyLinkLable2 = Nothing
        Me.txtCustDisc.Name = "txtCustDisc"
        Me.txtCustDisc.ReadOnly = True
        Me.txtCustDisc.Size = New System.Drawing.Size(141, 18)
        Me.txtCustDisc.TabIndex = 3
        Me.txtCustDisc.Text = "0.0"
        Me.txtCustDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnDeleteRS
        '
        Me.btnDeleteRS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteRS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteRS.Location = New System.Drawing.Point(673, 58)
        Me.btnDeleteRS.Name = "btnDeleteRS"
        Me.btnDeleteRS.Size = New System.Drawing.Size(268, 18)
        Me.btnDeleteRS.TabIndex = 0
        Me.btnDeleteRS.Text = "For Delete   (Tecxpert Only Transfer Type)"
        Me.btnDeleteRS.Visible = False
        '
        'btnDeleteRSSaleType
        '
        Me.btnDeleteRSSaleType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteRSSaleType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteRSSaleType.Location = New System.Drawing.Point(673, 161)
        Me.btnDeleteRSSaleType.Name = "btnDeleteRSSaleType"
        Me.btnDeleteRSSaleType.Size = New System.Drawing.Size(268, 18)
        Me.btnDeleteRSSaleType.TabIndex = 2
        Me.btnDeleteRSSaleType.Text = "For Delete   (Tecxpert Only Sale Type)"
        Me.btnDeleteRSSaleType.Visible = False
        '
        'btnCreateRS
        '
        Me.btnCreateRS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreateRS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateRS.Location = New System.Drawing.Point(673, 79)
        Me.btnCreateRS.Name = "btnCreateRS"
        Me.btnCreateRS.Size = New System.Drawing.Size(268, 18)
        Me.btnCreateRS.TabIndex = 1
        Me.btnCreateRS.Text = "For Create (Tecxpert Only Transfer Type)"
        Me.btnCreateRS.Visible = False
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Save Layout"
        Me.RadMenuItem4.AccessibleName = "Save Layout"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Save Layout"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem5.AccessibleName = "Delete Layout"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Delete Layout"
        Me.RadMenuItem5.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1012, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.pvLoadOut)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverseTransaction)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbltotalfcfb)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdateWithCustomerNewPrice)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSkipExciseInvoice)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSettlement)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveAndPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblfb)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblfc)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadLabel5)
        Me.SplitContainer1.Size = New System.Drawing.Size(1012, 511)
        Me.SplitContainer1.SplitterDistance = 460
        Me.SplitContainer1.TabIndex = 1
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(819, 2)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(68, 18)
        Me.RadButton2.TabIndex = 0
        Me.RadButton2.Text = "Old"
        Me.RadButton2.Visible = False
        '
        'btnReverseTransaction
        '
        Me.btnReverseTransaction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverseTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseTransaction.Location = New System.Drawing.Point(890, 1)
        Me.btnReverseTransaction.Name = "btnReverseTransaction"
        Me.btnReverseTransaction.Size = New System.Drawing.Size(117, 19)
        Me.btnReverseTransaction.TabIndex = 1
        Me.btnReverseTransaction.Text = "Reverse Transaction"
        Me.btnReverseTransaction.Visible = False
        '
        'lbltotalfcfb
        '
        Me.lbltotalfcfb.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalfcfb.Location = New System.Drawing.Point(12, 4)
        Me.lbltotalfcfb.Name = "lbltotalfcfb"
        Me.lbltotalfcfb.Size = New System.Drawing.Size(73, 19)
        Me.lbltotalfcfb.TabIndex = 16
        Me.lbltotalfcfb.Text = "Full Case :"
        '
        'btnUpdateWithCustomerNewPrice
        '
        Me.btnUpdateWithCustomerNewPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateWithCustomerNewPrice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateWithCustomerNewPrice.Location = New System.Drawing.Point(629, 25)
        Me.btnUpdateWithCustomerNewPrice.Name = "btnUpdateWithCustomerNewPrice"
        Me.btnUpdateWithCustomerNewPrice.Size = New System.Drawing.Size(176, 18)
        Me.btnUpdateWithCustomerNewPrice.TabIndex = 11
        Me.btnUpdateWithCustomerNewPrice.Text = "Update with Customer New Price"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(939, 25)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        '
        'btnSkipExciseInvoice
        '
        Me.btnSkipExciseInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSkipExciseInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSkipExciseInvoice.Location = New System.Drawing.Point(523, 25)
        Me.btnSkipExciseInvoice.Name = "btnSkipExciseInvoice"
        Me.btnSkipExciseInvoice.Size = New System.Drawing.Size(105, 18)
        Me.btnSkipExciseInvoice.TabIndex = 10
        Me.btnSkipExciseInvoice.Text = "Skip Excise Invoice"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(148, 25)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Cancel"
        '
        'btnSettlement
        '
        Me.btnSettlement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSettlement.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSettlement.Location = New System.Drawing.Point(361, 25)
        Me.btnSettlement.Name = "btnSettlement"
        Me.btnSettlement.Size = New System.Drawing.Size(80, 18)
        Me.btnSettlement.TabIndex = 7
        Me.btnSettlement.Text = "Settlement"
        Me.btnSettlement.Visible = False
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(10, 25)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(68, 18)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "Save"
        '
        'btnSaveAndPrint
        '
        Me.btnSaveAndPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAndPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAndPrint.Location = New System.Drawing.Point(77, 25)
        Me.btnSaveAndPrint.Name = "btnSaveAndPrint"
        Me.btnSaveAndPrint.Size = New System.Drawing.Size(70, 18)
        Me.btnSaveAndPrint.TabIndex = 3
        Me.btnSaveAndPrint.Text = "Save && Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(217, 25)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2})
        Me.RadSplitButton1.Location = New System.Drawing.Point(442, 25)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton1.TabIndex = 9
        Me.RadSplitButton1.Text = "Invoice"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Pre-Print"
        Me.RadMenuItem1.AccessibleName = "Pre-Print"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Pre-Print"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Print"
        Me.RadMenuItem2.AccessibleName = "Print"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Print"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(867, 25)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 19)
        Me.btnPrint.TabIndex = 12
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'lblfb
        '
        Me.lblfb.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfb.Location = New System.Drawing.Point(229, 4)
        Me.lblfb.Name = "lblfb"
        Me.lblfb.Size = New System.Drawing.Size(15, 19)
        Me.lblfb.TabIndex = 5
        Me.lblfb.Text = "0"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(286, 25)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(74, 18)
        Me.RadButton1.TabIndex = 6
        Me.RadButton1.Text = "Adjustment"
        '
        'lblfc
        '
        Me.lblfc.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfc.Location = New System.Drawing.Point(84, 4)
        Me.lblfc.Name = "lblfc"
        Me.lblfc.Size = New System.Drawing.Size(15, 19)
        Me.lblfc.TabIndex = 4
        Me.lblfc.Text = "0"
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(154, 4)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(76, 19)
        Me.RadLabel5.TabIndex = 17
        Me.RadLabel5.Text = "Full Bottle :"
        '
        'frmShipmentInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1012, 531)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(1020, 561)
        Me.Name = "frmShipmentInvoice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Load Out"
        CType(Me.pvLoadOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvLoadOut.ResumeLayout(False)
        Me.pageLoadOut.ResumeLayout(False)
        Me.pageLoadOut.PerformLayout()
        CType(Me.lblSalesMan1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemovalTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVhicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateEmpty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAgainstCForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSample, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chlcreditinvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbFC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbFB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMultipleOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMannualQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblManualQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblemployeecode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMannualAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblManualAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoadOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSaleInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKMReading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblKMReading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoadType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtshellqty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLoadOutType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustomerinvoiceno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblcustomerinvoiceno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransferDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransferDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPriceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTripNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTripNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpectedShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpectedShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerPONO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageTaxDetails.ResumeLayout(False)
        Me.pageTaxDetails.PerformLayout()
        CType(Me.lblTaxDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPaymentTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbTaxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTaxDetails.ResumeLayout(False)
        CType(Me.gvTax.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSchemeSample, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageTotal.ResumeLayout(False)
        CType(Me.btnRecalTransAndReCreateJE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMannualInvoiceNo.ResumeLayout(False)
        Me.pnlMannualInvoiceNo.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMannaulInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreateRSSaleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadRadioButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadRadioButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOtherCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdditionalCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOtherCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalShipmentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipmentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtContainerDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipmentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalTPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetShipAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalTaxAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipmentTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShipmentTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustDisc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeleteRS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeleteRSSaleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreateRS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverseTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltotalfcfb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateWithCustomerNewPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSkipExciseInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSettlement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveAndPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents pvLoadOut As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageLoadOut As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkMultipleOrder As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtTripNo As common.Controls.MyTextBox
    Friend WithEvents txtSchemeSample As finder.finder
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtExpectedShipDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCustomerPONO As common.Controls.MyTextBox
    Friend WithEvents txtOrderDate As common.Controls.MyDateTimePicker
    Friend WithEvents cboModeOfTransport As common.Controls.MyComboBox
    Friend WithEvents pageTaxDetails As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gbTaxDetails As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvTax As common.UserControls.MyRadGridView
    Friend WithEvents pageTotal As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbFix As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbPer As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtAdditionalCharges As common.Controls.MyTextBox
    Friend WithEvents txtOtherCharges As common.Controls.MyTextBox
    Friend WithEvents txtTotalTaxAmount As common.Controls.MyTextBox
    Friend WithEvents txtFreight As common.Controls.MyTextBox
    Friend WithEvents txtShipmentTotal As common.Controls.MyTextBox
    Friend WithEvents txtCustDisc As common.Controls.MyTextBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndTaxGroup As finder.finder
    Friend WithEvents cboLoadOutType As common.Controls.MyComboBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents chkInvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbFC As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbFB As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtTransferDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtshellqty As common.Controls.MyTextBox
    Friend WithEvents txtTotalTPT As common.Controls.MyTextBox
    Friend WithEvents lblremovaltime As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadDateTimePicker1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtcustomerinvoiceno As common.Controls.MyTextBox
    Friend WithEvents txtRef As common.Controls.MyTextBox
    Friend WithEvents chlcreditinvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtNetShipAmt As common.Controls.MyTextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadRadioButton1 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadRadioButton2 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtShipmentAmt As common.Controls.MyTextBox
    Friend WithEvents txtContainerDeposit As common.Controls.MyTextBox
    Friend WithEvents txtTotalShipmentAmt As common.Controls.MyTextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents chkSample As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtKMReading As common.MyNumBox
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents txtTransferNo As common.UserControls.txtFinder
    Friend WithEvents lblShipDate As common.Controls.MyLabel
    Friend WithEvents lblSchemeSample As common.Controls.MyLabel
    Friend WithEvents lblTripNo As common.Controls.MyLabel
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents lblPriceDate As common.Controls.MyLabel
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblExpectedShipDate As common.Controls.MyLabel
    Friend WithEvents lblCustomerPONo As common.Controls.MyLabel
    Friend WithEvents txtCustomerName As common.Controls.MyLabel
    Friend WithEvents lblCustomeNo As common.Controls.MyLabel
    Friend WithEvents lblOrderDate As common.Controls.MyLabel
    Friend WithEvents lblOrderNo As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblModeofTransport As common.Controls.MyLabel
    Friend WithEvents lblLoadOut As common.Controls.MyLabel
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents lblShipmentAmt As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents lblOtherCharges As common.Controls.MyLabel
    Friend WithEvents lblTotalTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblFreight As common.Controls.MyLabel
    Friend WithEvents lblShipmentTotal As common.Controls.MyLabel
    Friend WithEvents lblDiscountPer As common.Controls.MyLabel
    Friend WithEvents lblKMReading As common.Controls.MyLabel
    Friend WithEvents lblPaymentTerms As common.Controls.MyLabel
    Friend WithEvents lblTaxGroup As common.Controls.MyLabel
    Friend WithEvents lblLoadType As common.Controls.MyLabel
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents lblTransferDate As common.Controls.MyLabel
    Friend WithEvents lblTransferNo As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblcustomerinvoiceno As common.Controls.MyLabel
    Friend WithEvents lblemployeecode As common.Controls.MyLabel
    Friend WithEvents lblRefNo As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents lblSaleInvoiceNo As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtShipTo As common.UserControls.txtFinder
    Friend WithEvents lblShipTo As common.Controls.MyLabel
    Friend WithEvents cboPriceDate As common.Controls.MyComboBox
    Friend WithEvents chkAgainstCForm As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents CmbTransaction As common.Controls.MyComboBox
    Friend WithEvents lblTransaction As common.Controls.MyLabel
    Friend WithEvents lblManualAmt As common.Controls.MyLabel
    Friend WithEvents txtMannualAmt As common.MyNumBox
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents txtEmployeeCode As common.UserControls.txtFinder
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents txtPaymentTerm As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents lblManualQty As common.Controls.MyLabel
    Friend WithEvents txtMannualQty As common.MyNumBox
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDeleteRS As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCreateRS As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDeleteRSSaleType As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnCreateRSSaleType As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReverseTransaction As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbltotalfcfb As common.Controls.MyLabel
    Friend WithEvents btnUpdateWithCustomerNewPrice As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSkipExciseInvoice As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSettlement As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveAndPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblfb As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblfc As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblSalesMan1 As common.Controls.MyLabel
    Friend WithEvents lblTaxDesc As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkCreateEmpty As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDiscPer As common.MyNumBox
    Friend WithEvents txtDiscAmt As common.MyNumBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkDiscountOnAmt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDiscountOnRate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtOrderNo As common.UserControls.txtFinder
    Friend WithEvents lblVhicleNo As common.Controls.MyTextBox
    Friend WithEvents txtPriceCode As common.Controls.MyLabel
    Friend WithEvents pnlMannualInvoiceNo As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtMannaulInvoiceNo As common.Controls.MyTextBox
    Friend WithEvents txtRemovalTime As common.Controls.MyDateTimePicker
    Friend WithEvents btnRecalTransAndReCreateJE As Telerik.WinControls.UI.RadButton
End Class

