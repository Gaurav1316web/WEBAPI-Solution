<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaleOrder
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
        Me.txtDocNo = New common.UserControls.txtNavigator
        Me.lblLoadOut = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.pvLoadOut = New Telerik.WinControls.UI.RadPageView
        Me.pageLoadOut = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtPaymentAmt = New common.MyNumBox
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.lblSalesMan1 = New common.Controls.MyLabel
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txtPaymentDate = New common.Controls.MyDateTimePicker
        Me.txtCustomerName = New common.Controls.MyLabel
        Me.lblLocation = New common.Controls.MyLabel
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.txtSalesman = New common.UserControls.txtFinder
        Me.lblSalesman = New common.Controls.MyLabel
        Me.txtEmployeeCode = New common.UserControls.txtFinder
        Me.lblemployeecode = New common.Controls.MyLabel
        Me.lblEmpName = New common.Controls.MyLabel
        Me.txtVehicleCode = New common.UserControls.txtFinder
        Me.lblVehicleCode = New common.Controls.MyLabel
        Me.lblVhicleNo = New common.Controls.MyLabel
        Me.lblShipTo = New common.Controls.MyLabel
        Me.txtShipTo = New common.UserControls.txtFinder
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtLocation = New common.UserControls.txtFinder
        Me.lblNew = New common.Controls.MyLabel
        Me.txtCustomer = New common.UserControls.txtFinder
        Me.lblCustomeNo = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.lblShipDate = New common.Controls.MyLabel
        Me.cboPriceDate = New common.Controls.MyComboBox
        Me.lblPriceDate = New common.Controls.MyLabel
        Me.rbAll = New Telerik.WinControls.UI.RadRadioButton
        Me.rbFC = New Telerik.WinControls.UI.RadRadioButton
        Me.rbFB = New Telerik.WinControls.UI.RadRadioButton
        Me.lblRemark = New common.Controls.MyLabel
        Me.txtRemarks = New common.Controls.MyTextBox
        Me.txtPriceCode = New common.Controls.MyTextBox
        Me.lblPriceCode = New common.Controls.MyLabel
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox
        Me.lblDescription = New common.Controls.MyLabel
        Me.txtDesc = New common.Controls.MyTextBox
        Me.lblExpectedShipDate = New common.Controls.MyLabel
        Me.txtExpectedShipDate = New common.Controls.MyDateTimePicker
        Me.lblCustomerPONo = New common.Controls.MyLabel
        Me.txtCustomerPONO = New common.Controls.MyTextBox
        Me.lblModeofTransport = New common.Controls.MyLabel
        Me.cboModeOfTransport = New common.Controls.MyComboBox
        Me.btnReset = New Telerik.WinControls.UI.RadButton
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
        Me.pageTotal = New Telerik.WinControls.UI.RadPageViewPage
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
        Me.lblTotalTonnage = New common.Controls.MyLabel
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.lblCustomerQty = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.lblSalesmanQty = New common.Controls.MyLabel
        Me.btnUnpost = New Telerik.WinControls.UI.RadButton
        Me.btnCheckSlip = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.lbltotalfcfb = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnAdd = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.lblfb = New common.Controls.MyLabel
        Me.lblfc = New common.Controls.MyLabel
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.txtshellqty = New common.MyNumBox
        CType(Me.lblLoadOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pvLoadOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvLoadOut.SuspendLayout()
        Me.pageLoadOut.SuspendLayout()
        CType(Me.txtPaymentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesMan1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaymentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblemployeecode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVhicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPriceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbFC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbFB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpectedShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExpectedShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustomerPONO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pageTotal.SuspendLayout()
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
        CType(Me.lblTotalTonnage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesmanQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCheckSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltotalfcfb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtshellqty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.txtDocNo.Size = New System.Drawing.Size(273, 18)
        Me.txtDocNo.TabIndex = 28
        Me.txtDocNo.Value = ""
        '
        'lblLoadOut
        '
        Me.lblLoadOut.Location = New System.Drawing.Point(3, 2)
        Me.lblLoadOut.Name = "lblLoadOut"
        Me.lblLoadOut.Size = New System.Drawing.Size(53, 18)
        Me.lblLoadOut.TabIndex = 29
        Me.lblLoadOut.Text = "Order No"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(911, 0)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(76, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 17
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
        Me.pageLoadOut.Controls.Add(Me.txtshellqty)
        Me.pageLoadOut.Controls.Add(Me.txtPaymentAmt)
        Me.pageLoadOut.Controls.Add(Me.lblSalesMan1)
        Me.pageLoadOut.Controls.Add(Me.MyLabel7)
        Me.pageLoadOut.Controls.Add(Me.MyLabel6)
        Me.pageLoadOut.Controls.Add(Me.txtPaymentDate)
        Me.pageLoadOut.Controls.Add(Me.txtCustomerName)
        Me.pageLoadOut.Controls.Add(Me.lblLocation)
        Me.pageLoadOut.Controls.Add(Me.gv1)
        Me.pageLoadOut.Controls.Add(Me.txtSalesman)
        Me.pageLoadOut.Controls.Add(Me.txtEmployeeCode)
        Me.pageLoadOut.Controls.Add(Me.txtVehicleCode)
        Me.pageLoadOut.Controls.Add(Me.txtDocNo)
        Me.pageLoadOut.Controls.Add(Me.UsLock1)
        Me.pageLoadOut.Controls.Add(Me.lblEmpName)
        Me.pageLoadOut.Controls.Add(Me.lblVhicleNo)
        Me.pageLoadOut.Controls.Add(Me.lblShipTo)
        Me.pageLoadOut.Controls.Add(Me.txtShipTo)
        Me.pageLoadOut.Controls.Add(Me.lblSalesman)
        Me.pageLoadOut.Controls.Add(Me.MyLabel2)
        Me.pageLoadOut.Controls.Add(Me.txtLocation)
        Me.pageLoadOut.Controls.Add(Me.txtCustomer)
        Me.pageLoadOut.Controls.Add(Me.lblemployeecode)
        Me.pageLoadOut.Controls.Add(Me.RadLabel1)
        Me.pageLoadOut.Controls.Add(Me.lblLoadOut)
        Me.pageLoadOut.Controls.Add(Me.txtDate)
        Me.pageLoadOut.Controls.Add(Me.lblShipDate)
        Me.pageLoadOut.Controls.Add(Me.cboPriceDate)
        Me.pageLoadOut.Controls.Add(Me.lblPriceDate)
        Me.pageLoadOut.Controls.Add(Me.rbAll)
        Me.pageLoadOut.Controls.Add(Me.rbFC)
        Me.pageLoadOut.Controls.Add(Me.rbFB)
        Me.pageLoadOut.Controls.Add(Me.lblRemark)
        Me.pageLoadOut.Controls.Add(Me.txtRemarks)
        Me.pageLoadOut.Controls.Add(Me.txtPriceCode)
        Me.pageLoadOut.Controls.Add(Me.lblPriceCode)
        Me.pageLoadOut.Controls.Add(Me.chkOnHold)
        Me.pageLoadOut.Controls.Add(Me.lblVehicleCode)
        Me.pageLoadOut.Controls.Add(Me.lblDescription)
        Me.pageLoadOut.Controls.Add(Me.txtDesc)
        Me.pageLoadOut.Controls.Add(Me.lblExpectedShipDate)
        Me.pageLoadOut.Controls.Add(Me.txtExpectedShipDate)
        Me.pageLoadOut.Controls.Add(Me.lblCustomerPONo)
        Me.pageLoadOut.Controls.Add(Me.txtCustomerPONO)
        Me.pageLoadOut.Controls.Add(Me.lblCustomeNo)
        Me.pageLoadOut.Controls.Add(Me.lblNew)
        Me.pageLoadOut.Controls.Add(Me.lblModeofTransport)
        Me.pageLoadOut.Controls.Add(Me.cboModeOfTransport)
        Me.pageLoadOut.Controls.Add(Me.btnReset)
        Me.pageLoadOut.Location = New System.Drawing.Point(10, 35)
        Me.pageLoadOut.Name = "pageLoadOut"
        Me.pageLoadOut.Size = New System.Drawing.Size(991, 414)
        Me.pageLoadOut.Text = "Load Out"
        '
        'txtPaymentAmt
        '
        Me.txtPaymentAmt.BackColor = System.Drawing.Color.White
        Me.txtPaymentAmt.DecimalPlaces = 0
        Me.txtPaymentAmt.Location = New System.Drawing.Point(865, 105)
        Me.txtPaymentAmt.MendatroryField = False
        Me.txtPaymentAmt.MyLinkLable1 = Me.MyLabel7
        Me.txtPaymentAmt.MyLinkLable2 = Nothing
        Me.txtPaymentAmt.Name = "txtPaymentAmt"
        Me.txtPaymentAmt.Size = New System.Drawing.Size(121, 20)
        Me.txtPaymentAmt.TabIndex = 42
        Me.txtPaymentAmt.Text = "0"
        Me.txtPaymentAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPaymentAmt.Value = 0
        '
        'MyLabel7
        '
        Me.MyLabel7.Location = New System.Drawing.Point(766, 106)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(93, 18)
        Me.MyLabel7.TabIndex = 21
        Me.MyLabel7.Text = "Payment Amount"
        '
        'lblSalesMan1
        '
        Me.lblSalesMan1.AutoSize = False
        Me.lblSalesMan1.BorderVisible = True
        Me.lblSalesMan1.Location = New System.Drawing.Point(219, 127)
        Me.lblSalesMan1.Name = "lblSalesMan1"
        Me.lblSalesMan1.Size = New System.Drawing.Size(225, 17)
        Me.lblSalesMan1.TabIndex = 36
        '
        'MyLabel6
        '
        Me.MyLabel6.Location = New System.Drawing.Point(582, 107)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(76, 18)
        Me.MyLabel6.TabIndex = 38
        Me.MyLabel6.Text = "Payment Date"
        '
        'txtPaymentDate
        '
        Me.txtPaymentDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtPaymentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPaymentDate.Location = New System.Drawing.Point(667, 106)
        Me.txtPaymentDate.MendatroryField = False
        Me.txtPaymentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPaymentDate.MyLinkLable1 = Me.MyLabel6
        Me.txtPaymentDate.MyLinkLable2 = Nothing
        Me.txtPaymentDate.Name = "txtPaymentDate"
        Me.txtPaymentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPaymentDate.ShowCheckBox = True
        Me.txtPaymentDate.Size = New System.Drawing.Size(93, 18)
        Me.txtPaymentDate.TabIndex = 16
        Me.txtPaymentDate.TabStop = False
        Me.txtPaymentDate.Text = "18/05/2011 02:11 PM"
        Me.txtPaymentDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AutoSize = False
        Me.txtCustomerName.BorderVisible = True
        Me.txtCustomerName.Location = New System.Drawing.Point(220, 43)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(225, 18)
        Me.txtCustomerName.TabIndex = 40
        Me.txtCustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustomerName.TextWrap = False
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.Location = New System.Drawing.Point(219, 22)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(225, 18)
        Me.lblLocation.TabIndex = 41
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
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
        Me.gv1.Location = New System.Drawing.Point(0, 147)
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
        Me.gv1.Size = New System.Drawing.Size(987, 200)
        Me.gv1.TabIndex = 18
        Me.gv1.Text = "GV Load Out"
        '
        'txtSalesman
        '
        Me.txtSalesman.Location = New System.Drawing.Point(80, 126)
        Me.txtSalesman.MendatroryField = False
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Me.lblSalesman
        Me.txtSalesman.MyLinkLable2 = Nothing
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.Size = New System.Drawing.Size(133, 18)
        Me.txtSalesman.TabIndex = 17
        Me.txtSalesman.Value = ""
        '
        'lblSalesman
        '
        Me.lblSalesman.Location = New System.Drawing.Point(3, 126)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(53, 18)
        Me.lblSalesman.TabIndex = 35
        Me.lblSalesman.Text = "Salesman"
        '
        'txtEmployeeCode
        '
        Me.txtEmployeeCode.Location = New System.Drawing.Point(80, 85)
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
        Me.lblemployeecode.Location = New System.Drawing.Point(3, 86)
        Me.lblemployeecode.Name = "lblemployeecode"
        Me.lblemployeecode.Size = New System.Drawing.Size(58, 18)
        Me.lblemployeecode.TabIndex = 33
        Me.lblemployeecode.Text = "Emp Code"
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Location = New System.Drawing.Point(219, 85)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(225, 18)
        Me.lblEmpName.TabIndex = 38
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEmpName.TextWrap = False
        '
        'txtVehicleCode
        '
        Me.txtVehicleCode.Location = New System.Drawing.Point(80, 64)
        Me.txtVehicleCode.MendatroryField = False
        Me.txtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleCode.MyLinkLable1 = Me.lblVehicleCode
        Me.txtVehicleCode.MyLinkLable2 = Me.lblVhicleNo
        Me.txtVehicleCode.MyReadOnly = False
        Me.txtVehicleCode.Name = "txtVehicleCode"
        Me.txtVehicleCode.Size = New System.Drawing.Size(133, 18)
        Me.txtVehicleCode.TabIndex = 10
        Me.txtVehicleCode.Value = ""
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.Location = New System.Drawing.Point(3, 65)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(72, 18)
        Me.lblVehicleCode.TabIndex = 32
        Me.lblVehicleCode.Text = "Vehicle Code"
        '
        'lblVhicleNo
        '
        Me.lblVhicleNo.AutoSize = False
        Me.lblVhicleNo.BorderVisible = True
        Me.lblVhicleNo.Location = New System.Drawing.Point(219, 64)
        Me.lblVhicleNo.Name = "lblVhicleNo"
        Me.lblVhicleNo.Size = New System.Drawing.Size(225, 18)
        Me.lblVhicleNo.TabIndex = 39
        Me.lblVhicleNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVhicleNo.TextWrap = False
        '
        'lblShipTo
        '
        Me.lblShipTo.AutoSize = False
        Me.lblShipTo.BorderVisible = True
        Me.lblShipTo.Location = New System.Drawing.Point(219, 106)
        Me.lblShipTo.Name = "lblShipTo"
        Me.lblShipTo.Size = New System.Drawing.Size(225, 18)
        Me.lblShipTo.TabIndex = 37
        Me.lblShipTo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShipTo.TextWrap = False
        '
        'txtShipTo
        '
        Me.txtShipTo.Location = New System.Drawing.Point(80, 106)
        Me.txtShipTo.MendatroryField = False
        Me.txtShipTo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipTo.MyLinkLable1 = Me.MyLabel2
        Me.txtShipTo.MyLinkLable2 = Me.lblShipTo
        Me.txtShipTo.MyReadOnly = False
        Me.txtShipTo.Name = "txtShipTo"
        Me.txtShipTo.Size = New System.Drawing.Size(133, 18)
        Me.txtShipTo.TabIndex = 14
        Me.txtShipTo.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(3, 107)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(44, 18)
        Me.MyLabel2.TabIndex = 34
        Me.MyLabel2.Text = "Ship To"
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(80, 22)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblNew
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(133, 18)
        Me.txtLocation.TabIndex = 2
        Me.txtLocation.Value = ""
        '
        'lblNew
        '
        Me.lblNew.Location = New System.Drawing.Point(3, 23)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(49, 18)
        Me.lblNew.TabIndex = 30
        Me.lblNew.Text = "Location"
        '
        'txtCustomer
        '
        Me.txtCustomer.Location = New System.Drawing.Point(80, 43)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.lblCustomeNo
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(133, 18)
        Me.txtCustomer.TabIndex = 5
        Me.txtCustomer.Value = ""
        '
        'lblCustomeNo
        '
        Me.lblCustomeNo.Location = New System.Drawing.Point(3, 44)
        Me.lblCustomeNo.Name = "lblCustomeNo"
        Me.lblCustomeNo.Size = New System.Drawing.Size(73, 18)
        Me.lblCustomeNo.TabIndex = 31
        Me.lblCustomeNo.Text = "Customer No"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(445, 107)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(51, 18)
        Me.RadLabel1.TabIndex = 19
        Me.RadLabel1.Text = "Shell Qty"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(411, 1)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblShipDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(126, 18)
        Me.txtDate.TabIndex = 0
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "18/05/2011 02:11 PM"
        Me.txtDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'lblShipDate
        '
        Me.lblShipDate.Location = New System.Drawing.Point(376, 2)
        Me.lblShipDate.Name = "lblShipDate"
        Me.lblShipDate.Size = New System.Drawing.Size(30, 18)
        Me.lblShipDate.TabIndex = 26
        Me.lblShipDate.Text = "Date"
        '
        'cboPriceDate
        '
        Me.cboPriceDate.BackColor = System.Drawing.Color.Transparent
        Me.cboPriceDate.Enabled = False
        Me.cboPriceDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPriceDate.Location = New System.Drawing.Point(856, 1)
        Me.cboPriceDate.MendatroryField = False
        Me.cboPriceDate.MyLinkLable1 = Me.lblPriceDate
        Me.cboPriceDate.MyLinkLable2 = Nothing
        Me.cboPriceDate.Name = "cboPriceDate"
        Me.cboPriceDate.Size = New System.Drawing.Size(49, 18)
        Me.cboPriceDate.TabIndex = 21
        Me.cboPriceDate.Visible = False
        '
        'lblPriceDate
        '
        Me.lblPriceDate.Location = New System.Drawing.Point(802, 2)
        Me.lblPriceDate.Name = "lblPriceDate"
        Me.lblPriceDate.Size = New System.Drawing.Size(57, 18)
        Me.lblPriceDate.TabIndex = 5
        Me.lblPriceDate.Text = "Price Date"
        Me.lblPriceDate.Visible = False
        '
        'rbAll
        '
        Me.rbAll.Location = New System.Drawing.Point(605, 43)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(33, 18)
        Me.rbAll.TabIndex = 8
        Me.rbAll.Text = "All"
        '
        'rbFC
        '
        Me.rbFC.Location = New System.Drawing.Point(571, 43)
        Me.rbFC.Name = "rbFC"
        Me.rbFC.Size = New System.Drawing.Size(33, 18)
        Me.rbFC.TabIndex = 7
        Me.rbFC.Text = "FC"
        '
        'rbFB
        '
        Me.rbFB.Location = New System.Drawing.Point(537, 43)
        Me.rbFB.Name = "rbFB"
        Me.rbFB.Size = New System.Drawing.Size(32, 18)
        Me.rbFB.TabIndex = 6
        Me.rbFB.Text = "FB"
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(445, 65)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(49, 18)
        Me.lblRemark.TabIndex = 21
        Me.lblRemark.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(525, 64)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.lblRemark
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(462, 18)
        Me.txtRemarks.TabIndex = 11
        '
        'txtPriceCode
        '
        Me.txtPriceCode.AutoSize = False
        Me.txtPriceCode.Location = New System.Drawing.Point(739, 43)
        Me.txtPriceCode.MendatroryField = False
        Me.txtPriceCode.Multiline = True
        Me.txtPriceCode.MyLinkLable1 = Me.lblPriceCode
        Me.txtPriceCode.MyLinkLable2 = Nothing
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.ReadOnly = True
        Me.txtPriceCode.Size = New System.Drawing.Size(247, 18)
        Me.txtPriceCode.TabIndex = 9
        '
        'lblPriceCode
        '
        Me.lblPriceCode.Location = New System.Drawing.Point(664, 44)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceCode.TabIndex = 22
        Me.lblPriceCode.Text = "Price Code"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(739, 2)
        Me.chkOnHold.Name = "chkOnHold"
        '
        '
        '
        Me.chkOnHold.RootElement.StretchHorizontally = True
        Me.chkOnHold.RootElement.StretchVertically = True
        Me.chkOnHold.Size = New System.Drawing.Size(60, 16)
        Me.chkOnHold.TabIndex = 24
        Me.chkOnHold.Text = "On Hold"
        Me.chkOnHold.Visible = False
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(445, 86)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 20
        Me.lblDescription.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(525, 85)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.lblDescription
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(462, 18)
        Me.txtDesc.TabIndex = 13
        '
        'lblExpectedShipDate
        '
        Me.lblExpectedShipDate.Location = New System.Drawing.Point(542, 2)
        Me.lblExpectedShipDate.Name = "lblExpectedShipDate"
        Me.lblExpectedShipDate.Size = New System.Drawing.Size(103, 18)
        Me.lblExpectedShipDate.TabIndex = 25
        Me.lblExpectedShipDate.Text = "Expected Ship Date"
        '
        'txtExpectedShipDate
        '
        Me.txtExpectedShipDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtExpectedShipDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedShipDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtExpectedShipDate.Location = New System.Drawing.Point(655, 1)
        Me.txtExpectedShipDate.MendatroryField = False
        Me.txtExpectedShipDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpectedShipDate.MyLinkLable1 = Me.lblExpectedShipDate
        Me.txtExpectedShipDate.MyLinkLable2 = Nothing
        Me.txtExpectedShipDate.Name = "txtExpectedShipDate"
        Me.txtExpectedShipDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtExpectedShipDate.Size = New System.Drawing.Size(79, 18)
        Me.txtExpectedShipDate.TabIndex = 1
        Me.txtExpectedShipDate.TabStop = False
        Me.txtExpectedShipDate.Text = "18/05/2011 02:11 PM"
        Me.txtExpectedShipDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'lblCustomerPONo
        '
        Me.lblCustomerPONo.Location = New System.Drawing.Point(664, 23)
        Me.lblCustomerPONo.Name = "lblCustomerPONo"
        Me.lblCustomerPONo.Size = New System.Drawing.Size(67, 18)
        Me.lblCustomerPONo.TabIndex = 23
        Me.lblCustomerPONo.Text = "Cust. PO No"
        '
        'txtCustomerPONO
        '
        Me.txtCustomerPONO.BackColor = System.Drawing.Color.Transparent
        Me.txtCustomerPONO.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txtCustomerPONO.Location = New System.Drawing.Point(739, 22)
        Me.txtCustomerPONO.MaxLength = 50
        Me.txtCustomerPONO.MendatroryField = False
        Me.txtCustomerPONO.MyLinkLable1 = Me.lblCustomerPONo
        Me.txtCustomerPONO.MyLinkLable2 = Nothing
        Me.txtCustomerPONO.Name = "txtCustomerPONO"
        Me.txtCustomerPONO.Size = New System.Drawing.Size(247, 18)
        Me.txtCustomerPONO.TabIndex = 4
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
        'lblModeofTransport
        '
        Me.lblModeofTransport.Location = New System.Drawing.Point(445, 23)
        Me.lblModeofTransport.Name = "lblModeofTransport"
        Me.lblModeofTransport.Size = New System.Drawing.Size(55, 18)
        Me.lblModeofTransport.TabIndex = 19
        Me.lblModeofTransport.Text = "Tpt Mode"
        '
        'cboModeOfTransport
        '
        Me.cboModeOfTransport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "By Road"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "By Air"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "By Sea"
        RadListDataItem3.TextWrap = True
        Me.cboModeOfTransport.Items.Add(RadListDataItem1)
        Me.cboModeOfTransport.Items.Add(RadListDataItem2)
        Me.cboModeOfTransport.Items.Add(RadListDataItem3)
        Me.cboModeOfTransport.Location = New System.Drawing.Point(525, 22)
        Me.cboModeOfTransport.MendatroryField = False
        Me.cboModeOfTransport.MyLinkLable1 = Me.lblModeofTransport
        Me.cboModeOfTransport.MyLinkLable2 = Nothing
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.Size = New System.Drawing.Size(133, 18)
        Me.cboModeOfTransport.TabIndex = 3
        Me.cboModeOfTransport.Text = "Select"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(353, 1)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 27
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
        Me.txtPaymentTerm.TabIndex = 116
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
        Me.txtRouteNo.TabIndex = 114
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
        Me.txtRef.TabIndex = 7
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
        Me.fndTaxGroup.TabIndex = 4
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
        'pageTotal
        '
        Me.pageTotal.Controls.Add(Me.RadGroupBox2)
        Me.pageTotal.Location = New System.Drawing.Point(10, 35)
        Me.pageTotal.Name = "pageTotal"
        Me.pageTotal.Size = New System.Drawing.Size(991, 410)
        Me.pageTotal.Text = "Total"
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
        Me.RadGroupBox2.Size = New System.Drawing.Size(341, 309)
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
        Me.RadGroupBox1.TabIndex = 2
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
        Me.txtDiscAmt.MyLinkLable1 = Nothing
        Me.txtDiscAmt.MyLinkLable2 = Nothing
        Me.txtDiscAmt.Name = "txtDiscAmt"
        Me.txtDiscAmt.Size = New System.Drawing.Size(80, 20)
        Me.txtDiscAmt.TabIndex = 4
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
        Me.txtDiscPer.MyLinkLable1 = Nothing
        Me.txtDiscPer.MyLinkLable2 = Nothing
        Me.txtDiscPer.Name = "txtDiscPer"
        Me.txtDiscPer.Size = New System.Drawing.Size(39, 20)
        Me.txtDiscPer.TabIndex = 3
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
        Me.txtTotalShipmentAmt.TabIndex = 10
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
        Me.txtContainerDeposit.TabIndex = 9
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
        Me.txtShipmentAmt.TabIndex = 8
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
        Me.TextBox1.TabIndex = 5
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
        Me.txtTotalTPT.TabIndex = 7
        Me.txtTotalTPT.Text = "0.0"
        Me.txtTotalTPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdbFix
        '
        Me.rdbFix.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbFix.Location = New System.Drawing.Point(273, 3)
        Me.rdbFix.Name = "rdbFix"
        Me.rdbFix.Size = New System.Drawing.Size(35, 16)
        Me.rdbFix.TabIndex = 43
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
        Me.rdbPer.TabIndex = 42
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
        Me.txtTotalTaxAmount.TabIndex = 6
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
        Me.txtShipmentTotal.TabIndex = 0
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
        Me.txtCustDisc.TabIndex = 1
        Me.txtCustDisc.Text = "0.0"
        Me.txtCustDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotalTonnage
        '
        Me.lblTotalTonnage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalTonnage.AutoSize = False
        Me.lblTotalTonnage.BorderVisible = True
        Me.lblTotalTonnage.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTonnage.Location = New System.Drawing.Point(867, 4)
        Me.lblTotalTonnage.Name = "lblTotalTonnage"
        Me.lblTotalTonnage.Size = New System.Drawing.Size(142, 18)
        Me.lblTotalTonnage.TabIndex = 89
        Me.lblTotalTonnage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel1
        '
        Me.MyLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(769, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(96, 19)
        Me.MyLabel1.TabIndex = 88
        Me.MyLabel1.Text = "Total Tonnage"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblCustomerQty)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblSalesmanQty)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblTotalTonnage)
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCheckSlip)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbltotalfcfb)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblfb)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblfc)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadLabel5)
        Me.SplitContainer1.Size = New System.Drawing.Size(1012, 511)
        Me.SplitContainer1.SplitterDistance = 460
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(547, 4)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(97, 18)
        Me.MyLabel5.TabIndex = 93
        Me.MyLabel5.Text = "Customer Qty :"
        '
        'lblCustomerQty
        '
        Me.lblCustomerQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerQty.Location = New System.Drawing.Point(642, 4)
        Me.lblCustomerQty.Name = "lblCustomerQty"
        Me.lblCustomerQty.Size = New System.Drawing.Size(14, 18)
        Me.lblCustomerQty.TabIndex = 92
        Me.lblCustomerQty.Text = "0"
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(346, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(98, 18)
        Me.MyLabel4.TabIndex = 91
        Me.MyLabel4.Text = "Salesman Qty :"
        '
        'lblSalesmanQty
        '
        Me.lblSalesmanQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesmanQty.Location = New System.Drawing.Point(441, 3)
        Me.lblSalesmanQty.Name = "lblSalesmanQty"
        Me.lblSalesmanQty.Size = New System.Drawing.Size(14, 18)
        Me.lblSalesmanQty.TabIndex = 90
        Me.lblSalesmanQty.Text = "0"
        '
        'btnUnpost
        '
        Me.btnUnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUnpost.Location = New System.Drawing.Point(284, 25)
        Me.btnUnpost.Name = "btnUnpost"
        Me.btnUnpost.Size = New System.Drawing.Size(68, 18)
        Me.btnUnpost.TabIndex = 4
        Me.btnUnpost.Text = "Unpost"
        '
        'btnCheckSlip
        '
        Me.btnCheckSlip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCheckSlip.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheckSlip.Location = New System.Drawing.Point(358, 26)
        Me.btnCheckSlip.Name = "btnCheckSlip"
        Me.btnCheckSlip.Size = New System.Drawing.Size(68, 18)
        Me.btnCheckSlip.TabIndex = 2
        Me.btnCheckSlip.Text = "Check Slip"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(144, 25)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        '
        'lbltotalfcfb
        '
        Me.lbltotalfcfb.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalfcfb.Location = New System.Drawing.Point(12, 4)
        Me.lbltotalfcfb.Name = "lbltotalfcfb"
        Me.lbltotalfcfb.Size = New System.Drawing.Size(71, 18)
        Me.lbltotalfcfb.TabIndex = 8
        Me.lbltotalfcfb.Text = "Full Case :"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(939, 25)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 25)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(4, 25)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(68, 18)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Save"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(214, 25)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        '
        'lblfb
        '
        Me.lblfb.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfb.Location = New System.Drawing.Point(229, 4)
        Me.lblfb.Name = "lblfb"
        Me.lblfb.Size = New System.Drawing.Size(14, 18)
        Me.lblfb.TabIndex = 5
        Me.lblfb.Text = "0"
        '
        'lblfc
        '
        Me.lblfc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfc.Location = New System.Drawing.Point(82, 4)
        Me.lblfc.Name = "lblfc"
        Me.lblfc.Size = New System.Drawing.Size(14, 18)
        Me.lblfc.TabIndex = 7
        Me.lblfc.Text = "0"
        '
        'RadLabel5
        '
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(154, 4)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(74, 18)
        Me.RadLabel5.TabIndex = 6
        Me.RadLabel5.Text = "Full Bottle :"
        '
        'txtshellqty
        '
        Me.txtshellqty.BackColor = System.Drawing.Color.White
        Me.txtshellqty.DecimalPlaces = 0
        Me.txtshellqty.Location = New System.Drawing.Point(524, 106)
        Me.txtshellqty.MendatroryField = False
        Me.txtshellqty.MyLinkLable1 = Me.MyLabel7
        Me.txtshellqty.MyLinkLable2 = Nothing
        Me.txtshellqty.Name = "txtshellqty"
        Me.txtshellqty.Size = New System.Drawing.Size(52, 20)
        Me.txtshellqty.TabIndex = 43
        Me.txtshellqty.Text = "0"
        Me.txtshellqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtshellqty.Value = 0
        '
        'frmSaleOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1012, 531)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(1020, 561)
        Me.Name = "frmSaleOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Order"
        CType(Me.lblLoadOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pvLoadOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvLoadOut.ResumeLayout(False)
        Me.pageLoadOut.ResumeLayout(False)
        Me.pageLoadOut.PerformLayout()
        CType(Me.txtPaymentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesMan1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaymentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblemployeecode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVhicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPriceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbFC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbFB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpectedShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExpectedShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustomerPONO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.pageTotal.ResumeLayout(False)
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
        CType(Me.lblTotalTonnage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesmanQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCheckSlip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltotalfcfb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtshellqty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents pvLoadOut As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageLoadOut As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtExpectedShipDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCustomerPONO As common.Controls.MyTextBox
    Friend WithEvents cboModeOfTransport As common.Controls.MyComboBox
    Friend WithEvents pageTaxDetails As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gbTaxDetails As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvTax As common.UserControls.MyRadGridView
    Friend WithEvents pageTotal As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbFix As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbPer As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtTotalTaxAmount As common.Controls.MyTextBox
    Friend WithEvents txtShipmentTotal As common.Controls.MyTextBox
    Friend WithEvents txtCustDisc As common.Controls.MyTextBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndTaxGroup As finder.finder
    Friend WithEvents txtPriceCode As common.Controls.MyTextBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents rbAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbFC As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbFB As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtTotalTPT As common.Controls.MyTextBox
    Friend WithEvents lblremovaltime As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadDateTimePicker1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtRef As common.Controls.MyTextBox
    Friend WithEvents txtNetShipAmt As common.Controls.MyTextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents txtShipmentAmt As common.Controls.MyTextBox
    Friend WithEvents txtContainerDeposit As common.Controls.MyTextBox
    Friend WithEvents txtTotalShipmentAmt As common.Controls.MyTextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblShipDate As common.Controls.MyLabel
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents lblPriceDate As common.Controls.MyLabel
    Friend WithEvents lblRemark As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblExpectedShipDate As common.Controls.MyLabel
    Friend WithEvents lblCustomerPONo As common.Controls.MyLabel
    Friend WithEvents lblCustomeNo As common.Controls.MyLabel
    Friend WithEvents lblNew As common.Controls.MyLabel
    Friend WithEvents lblModeofTransport As common.Controls.MyLabel
    Friend WithEvents lblLoadOut As common.Controls.MyLabel
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents lblShipmentAmt As common.Controls.MyLabel
    Friend WithEvents lblTotalTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblShipmentTotal As common.Controls.MyLabel
    Friend WithEvents lblDiscountPer As common.Controls.MyLabel
    Friend WithEvents lblPaymentTerms As common.Controls.MyLabel
    Friend WithEvents lblTaxGroup As common.Controls.MyLabel
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblemployeecode As common.Controls.MyLabel
    Friend WithEvents lblRefNo As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtShipTo As common.UserControls.txtFinder
    Friend WithEvents lblShipTo As common.Controls.MyLabel
    Friend WithEvents cboPriceDate As common.Controls.MyComboBox
    Friend WithEvents txtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents txtEmployeeCode As common.UserControls.txtFinder
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
    Friend WithEvents txtPaymentTerm As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lbltotalfcfb As common.Controls.MyLabel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblfb As common.Controls.MyLabel
    Friend WithEvents lblfc As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents lblVhicleNo As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents lblSalesMan1 As common.Controls.MyLabel
    Friend WithEvents lblTaxDesc As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents txtDiscPer As common.MyNumBox
    Friend WithEvents txtDiscAmt As common.MyNumBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkDiscountOnAmt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDiscountOnRate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtCustomerName As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnUnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTotalTonnage As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnCheckSlip As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblSalesmanQty As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblCustomerQty As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtPaymentDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtPaymentAmt As common.MyNumBox
    Friend WithEvents txtshellqty As common.MyNumBox
End Class

