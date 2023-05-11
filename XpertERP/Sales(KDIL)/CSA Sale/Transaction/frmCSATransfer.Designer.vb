<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSATransfer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCSATransfer))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtElectronicRefNo = New common.Controls.MyTextBox()
        Me.TxtEWayBillDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.btnEwaybillnoupdate = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel27 = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.TxtEWayBillNo = New common.Controls.MyTextBox()
        Me.txtRemovalDate = New common.Controls.MyDateTimePicker()
        Me.lblRemovalDate = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.txtSecondary_Doc_Code = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.TxtTransportorMName = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.cmbEXType = New common.Controls.MyComboBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.chkownvehicle = New common.Controls.MyCheckBox()
        Me.txtship_to_loc_name = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.txtship_to_loc_code = New common.UserControls.txtFinder()
        Me.chk_F_Form = New common.Controls.MyCheckBox()
        Me.txtExcisable = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.txtvehicle_Charge = New common.Controls.MyLabel()
        Me.ttxway_bill_date = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.dtpGR = New common.Controls.MyDateTimePicker()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtGR_No = New common.Controls.MyTextBox()
        Me.txtGross_Wt = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txttotal_Wt = New common.Controls.MyLabel()
        Me.txtVehicle_Capacity = New common.MyNumBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtvehicle_code = New common.Controls.MyTextBox()
        Me.txtTransporter_desc = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtTransporter_Code = New common.UserControls.txtFinder()
        Me.btnupdate = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtWayBill_No = New common.Controls.MyTextBox()
        Me.cmbTax = New common.Controls.MyComboBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtCSARate = New common.Controls.MyTextBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.fndDONo = New common.UserControls.txtFinder()
        Me.txtStateDesc = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtState = New common.UserControls.txtFinder()
        Me.txtDate = New System.Windows.Forms.DateTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtDocumentTotal = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.txtToLocationDesc = New common.Controls.MyLabel()
        Me.txtFromLocationDesc = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.lblDept = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtCustDesc = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCustCode = New common.UserControls.txtFinder()
        Me.txtToLocation = New common.UserControls.txtFinder()
        Me.txtFromLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtTermCode = New common.UserControls.txtFinder()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.lblTermName = New common.Controls.MyLabel()
        Me.txtDueDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.lblCommissionCharges = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblTotalAmount = New common.Controls.MyLabel()
        Me.lblInvoiceDiscAmt = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkDiscountOnAmt = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkDiscountOnRate = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDiscAmt = New common.MyNumBox()
        Me.txtDiscPer = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.lblTotalOtherCharges = New common.Controls.MyLabel()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTotalTaxAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.BtnPrintChallan = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintMandi = New Telerik.WinControls.UI.RadButton()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnpreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnsend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendForApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.chkRateUserCustomer = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRateDefaultSetting = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnAmendment = New Telerik.WinControls.UI.RadButton()
        Me.lblAmbendmentNoCaption = New common.Controls.MyLabel()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtElectronicRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEWayBillDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEwaybillnoupdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemovalDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemovalDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSecondary_Doc_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTransportorMName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEXType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkownvehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtship_to_loc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chk_F_Form, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvehicle_Charge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ttxway_bill_date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGR_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGross_Wt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotal_Wt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicle_Capacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtvehicle_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporter_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnupdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWayBill_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCSARate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStateDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocumentTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCustDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCommissionCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalOtherCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPrintChallan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintMandi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmbendmentNoCaption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPrintChallan)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintMandi)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkRateUserCustomer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkRateDefaultSetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAmendment)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblAmbendmentNoCaption)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(1100, 523)
        Me.SplitContainer1.SplitterDistance = 491
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1100, 491)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtElectronicRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.TxtEWayBillDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel25)
        Me.RadPageViewPage1.Controls.Add(Me.btnEwaybillnoupdate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel27)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage1.Controls.Add(Me.TxtEWayBillNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemovalDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblRemovalDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage1.Controls.Add(Me.txtSecondary_Doc_Code)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.TxtTransportorMName)
        Me.RadPageViewPage1.Controls.Add(Me.cmbEXType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.chkownvehicle)
        Me.RadPageViewPage1.Controls.Add(Me.txtship_to_loc_name)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage1.Controls.Add(Me.txtship_to_loc_code)
        Me.RadPageViewPage1.Controls.Add(Me.chk_F_Form)
        Me.RadPageViewPage1.Controls.Add(Me.txtExcisable)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage1.Controls.Add(Me.txtvehicle_Charge)
        Me.RadPageViewPage1.Controls.Add(Me.ttxway_bill_date)
        Me.RadPageViewPage1.Controls.Add(Me.dtpGR)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.Controls.Add(Me.txtGR_No)
        Me.RadPageViewPage1.Controls.Add(Me.txtGross_Wt)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.txttotal_Wt)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicle_Capacity)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.txtvehicle_code)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporter_desc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporter_Code)
        Me.RadPageViewPage1.Controls.Add(Me.btnupdate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.txtWayBill_No)
        Me.RadPageViewPage1.Controls.Add(Me.cmbTax)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.txtCSARate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.fndDONo)
        Me.RadPageViewPage1.Controls.Add(Me.txtStateDesc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtState)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocumentTotal)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtToLocationDesc)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromLocationDesc)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustDesc)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(85.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1079, 445)
        Me.RadPageViewPage1.Text = "CSA Transfer"
        '
        'txtElectronicRefNo
        '
        Me.txtElectronicRefNo.CalculationExpression = Nothing
        Me.txtElectronicRefNo.FieldCode = Nothing
        Me.txtElectronicRefNo.FieldDesc = Nothing
        Me.txtElectronicRefNo.FieldMaxLength = 0
        Me.txtElectronicRefNo.FieldName = Nothing
        Me.txtElectronicRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtElectronicRefNo.isCalculatedField = False
        Me.txtElectronicRefNo.IsSourceFromTable = False
        Me.txtElectronicRefNo.IsSourceFromValueList = False
        Me.txtElectronicRefNo.IsUnique = False
        Me.txtElectronicRefNo.Location = New System.Drawing.Point(595, 197)
        Me.txtElectronicRefNo.MaxLength = 10
        Me.txtElectronicRefNo.MendatroryField = False
        Me.txtElectronicRefNo.MyLinkLable1 = Nothing
        Me.txtElectronicRefNo.MyLinkLable2 = Nothing
        Me.txtElectronicRefNo.Name = "txtElectronicRefNo"
        Me.txtElectronicRefNo.ReferenceFieldDesc = Nothing
        Me.txtElectronicRefNo.ReferenceFieldName = Nothing
        Me.txtElectronicRefNo.ReferenceTableName = Nothing
        Me.txtElectronicRefNo.Size = New System.Drawing.Size(151, 18)
        Me.txtElectronicRefNo.TabIndex = 1444
        '
        'TxtEWayBillDate
        '
        Me.TxtEWayBillDate.CalculationExpression = Nothing
        Me.TxtEWayBillDate.CustomFormat = "dd/MM/yyyy"
        Me.TxtEWayBillDate.FieldCode = Nothing
        Me.TxtEWayBillDate.FieldDesc = Nothing
        Me.TxtEWayBillDate.FieldMaxLength = 0
        Me.TxtEWayBillDate.FieldName = Nothing
        Me.TxtEWayBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtEWayBillDate.isCalculatedField = False
        Me.TxtEWayBillDate.IsSourceFromTable = False
        Me.TxtEWayBillDate.IsSourceFromValueList = False
        Me.TxtEWayBillDate.IsUnique = False
        Me.TxtEWayBillDate.Location = New System.Drawing.Point(835, 175)
        Me.TxtEWayBillDate.MendatroryField = False
        Me.TxtEWayBillDate.MyLinkLable1 = Me.MyLabel25
        Me.TxtEWayBillDate.MyLinkLable2 = Nothing
        Me.TxtEWayBillDate.Name = "TxtEWayBillDate"
        Me.TxtEWayBillDate.ReferenceFieldDesc = Nothing
        Me.TxtEWayBillDate.ReferenceFieldName = Nothing
        Me.TxtEWayBillDate.ReferenceTableName = Nothing
        Me.TxtEWayBillDate.Size = New System.Drawing.Size(79, 20)
        Me.TxtEWayBillDate.TabIndex = 1440
        Me.TxtEWayBillDate.TabStop = False
        Me.TxtEWayBillDate.Text = "17/12/2014"
        Me.TxtEWayBillDate.Value = New Date(2014, 12, 17, 11, 28, 4, 99)
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel25.Location = New System.Drawing.Point(750, 176)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel25.TabIndex = 1443
        Me.MyLabel25.Text = "E-WayBill Date"
        '
        'btnEwaybillnoupdate
        '
        Me.btnEwaybillnoupdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEwaybillnoupdate.Location = New System.Drawing.Point(920, 177)
        Me.btnEwaybillnoupdate.Name = "btnEwaybillnoupdate"
        Me.btnEwaybillnoupdate.Size = New System.Drawing.Size(145, 18)
        Me.btnEwaybillnoupdate.TabIndex = 1441
        Me.btnEwaybillnoupdate.Text = "Update E-WayBill"
        '
        'MyLabel27
        '
        Me.MyLabel27.FieldName = Nothing
        Me.MyLabel27.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel27.Location = New System.Drawing.Point(491, 199)
        Me.MyLabel27.Name = "MyLabel27"
        Me.MyLabel27.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel27.TabIndex = 1445
        Me.MyLabel27.Text = "Electronic Ref. No"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(491, 177)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel26.TabIndex = 1442
        Me.MyLabel26.Text = "E-WayBill No."
        '
        'TxtEWayBillNo
        '
        Me.TxtEWayBillNo.CalculationExpression = Nothing
        Me.TxtEWayBillNo.FieldCode = Nothing
        Me.TxtEWayBillNo.FieldDesc = Nothing
        Me.TxtEWayBillNo.FieldMaxLength = 0
        Me.TxtEWayBillNo.FieldName = Nothing
        Me.TxtEWayBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEWayBillNo.isCalculatedField = False
        Me.TxtEWayBillNo.IsSourceFromTable = False
        Me.TxtEWayBillNo.IsSourceFromValueList = False
        Me.TxtEWayBillNo.IsUnique = False
        Me.TxtEWayBillNo.Location = New System.Drawing.Point(595, 176)
        Me.TxtEWayBillNo.MaxLength = 30
        Me.TxtEWayBillNo.MendatroryField = False
        Me.TxtEWayBillNo.MyLinkLable1 = Me.MyLabel26
        Me.TxtEWayBillNo.MyLinkLable2 = Nothing
        Me.TxtEWayBillNo.Name = "TxtEWayBillNo"
        Me.TxtEWayBillNo.ReferenceFieldDesc = Nothing
        Me.TxtEWayBillNo.ReferenceFieldName = Nothing
        Me.TxtEWayBillNo.ReferenceTableName = Nothing
        Me.TxtEWayBillNo.Size = New System.Drawing.Size(151, 18)
        Me.TxtEWayBillNo.TabIndex = 1439
        '
        'txtRemovalDate
        '
        Me.txtRemovalDate.CalculationExpression = Nothing
        Me.txtRemovalDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtRemovalDate.FieldCode = Nothing
        Me.txtRemovalDate.FieldDesc = Nothing
        Me.txtRemovalDate.FieldMaxLength = 0
        Me.txtRemovalDate.FieldName = Nothing
        Me.txtRemovalDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemovalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtRemovalDate.isCalculatedField = False
        Me.txtRemovalDate.IsSourceFromTable = False
        Me.txtRemovalDate.IsSourceFromValueList = False
        Me.txtRemovalDate.IsUnique = False
        Me.txtRemovalDate.Location = New System.Drawing.Point(347, 176)
        Me.txtRemovalDate.MendatroryField = False
        Me.txtRemovalDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRemovalDate.MyLinkLable1 = Me.lblRemovalDate
        Me.txtRemovalDate.MyLinkLable2 = Nothing
        Me.txtRemovalDate.Name = "txtRemovalDate"
        Me.txtRemovalDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtRemovalDate.ReferenceFieldDesc = Nothing
        Me.txtRemovalDate.ReferenceFieldName = Nothing
        Me.txtRemovalDate.ReferenceTableName = Nothing
        Me.txtRemovalDate.ShowCheckBox = True
        Me.txtRemovalDate.Size = New System.Drawing.Size(143, 18)
        Me.txtRemovalDate.TabIndex = 1438
        Me.txtRemovalDate.TabStop = False
        Me.txtRemovalDate.Text = "13/06/2011 11:29 AM"
        Me.txtRemovalDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblRemovalDate
        '
        Me.lblRemovalDate.FieldName = Nothing
        Me.lblRemovalDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemovalDate.Location = New System.Drawing.Point(245, 177)
        Me.lblRemovalDate.Name = "lblRemovalDate"
        Me.lblRemovalDate.Size = New System.Drawing.Size(78, 16)
        Me.lblRemovalDate.TabIndex = 1437
        Me.lblRemovalDate.Text = "Removal Date"
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(0, 175)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel24.TabIndex = 1436
        Me.MyLabel24.Text = "Secondary Code"
        '
        'txtSecondary_Doc_Code
        '
        Me.txtSecondary_Doc_Code.AutoSize = False
        Me.txtSecondary_Doc_Code.BorderVisible = True
        Me.txtSecondary_Doc_Code.FieldName = Nothing
        Me.txtSecondary_Doc_Code.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecondary_Doc_Code.Location = New System.Drawing.Point(100, 175)
        Me.txtSecondary_Doc_Code.Name = "txtSecondary_Doc_Code"
        Me.txtSecondary_Doc_Code.Size = New System.Drawing.Size(143, 18)
        Me.txtSecondary_Doc_Code.TabIndex = 1435
        Me.txtSecondary_Doc_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel23
        '
        Me.MyLabel23.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel23.Location = New System.Drawing.Point(757, 404)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(232, 16)
        Me.MyLabel23.TabIndex = 1434
        Me.MyLabel23.Text = "Press F5 on grid cell for Batch item detail."
        '
        'TxtTransportorMName
        '
        Me.TxtTransportorMName.CalculationExpression = Nothing
        Me.TxtTransportorMName.FieldCode = Nothing
        Me.TxtTransportorMName.FieldDesc = Nothing
        Me.TxtTransportorMName.FieldMaxLength = 0
        Me.TxtTransportorMName.FieldName = Nothing
        Me.TxtTransportorMName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransportorMName.isCalculatedField = False
        Me.TxtTransportorMName.IsSourceFromTable = False
        Me.TxtTransportorMName.IsSourceFromValueList = False
        Me.TxtTransportorMName.IsUnique = False
        Me.TxtTransportorMName.Location = New System.Drawing.Point(834, 132)
        Me.TxtTransportorMName.MaxLength = 200
        Me.TxtTransportorMName.MendatroryField = False
        Me.TxtTransportorMName.MyLinkLable1 = Me.MyLabel14
        Me.TxtTransportorMName.MyLinkLable2 = Nothing
        Me.TxtTransportorMName.Name = "TxtTransportorMName"
        Me.TxtTransportorMName.ReferenceFieldDesc = Nothing
        Me.TxtTransportorMName.ReferenceFieldName = Nothing
        Me.TxtTransportorMName.ReferenceTableName = Nothing
        Me.TxtTransportorMName.Size = New System.Drawing.Size(212, 18)
        Me.TxtTransportorMName.TabIndex = 1433
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(2, 134)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel14.TabIndex = 1397
        Me.MyLabel14.Text = "Vehicle No."
        '
        'cmbEXType
        '
        Me.cmbEXType.AutoCompleteDisplayMember = Nothing
        Me.cmbEXType.AutoCompleteValueMember = Nothing
        Me.cmbEXType.CalculationExpression = Nothing
        Me.cmbEXType.FieldCode = Nothing
        Me.cmbEXType.FieldDesc = Nothing
        Me.cmbEXType.FieldMaxLength = 0
        Me.cmbEXType.FieldName = Nothing
        Me.cmbEXType.isCalculatedField = False
        Me.cmbEXType.IsSourceFromTable = False
        Me.cmbEXType.IsSourceFromValueList = False
        Me.cmbEXType.IsUnique = False
        Me.cmbEXType.Location = New System.Drawing.Point(834, 151)
        Me.cmbEXType.MendatroryField = True
        Me.cmbEXType.MyLinkLable1 = Me.MyLabel21
        Me.cmbEXType.MyLinkLable2 = Nothing
        Me.cmbEXType.Name = "cmbEXType"
        Me.cmbEXType.ReferenceFieldDesc = Nothing
        Me.cmbEXType.ReferenceFieldName = Nothing
        Me.cmbEXType.ReferenceTableName = Nothing
        Me.cmbEXType.Size = New System.Drawing.Size(231, 20)
        Me.cmbEXType.TabIndex = 21
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(742, 153)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(77, 16)
        Me.MyLabel21.TabIndex = 1416
        Me.MyLabel21.Text = "Transfer Type"
        '
        'chkownvehicle
        '
        Me.chkownvehicle.Location = New System.Drawing.Point(744, 136)
        Me.chkownvehicle.MyLinkLable1 = Nothing
        Me.chkownvehicle.MyLinkLable2 = Nothing
        Me.chkownvehicle.Name = "chkownvehicle"
        Me.chkownvehicle.Size = New System.Drawing.Size(82, 18)
        Me.chkownvehicle.TabIndex = 19
        Me.chkownvehicle.Tag1 = Nothing
        Me.chkownvehicle.Text = "Own Vehicle"
        '
        'txtship_to_loc_name
        '
        Me.txtship_to_loc_name.AutoSize = False
        Me.txtship_to_loc_name.BorderVisible = True
        Me.txtship_to_loc_name.FieldName = Nothing
        Me.txtship_to_loc_name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtship_to_loc_name.Location = New System.Drawing.Point(742, 25)
        Me.txtship_to_loc_name.Name = "txtship_to_loc_name"
        Me.txtship_to_loc_name.Size = New System.Drawing.Size(324, 18)
        Me.txtship_to_loc_name.TabIndex = 1412
        Me.txtship_to_loc_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtship_to_loc_name.TextWrap = False
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(491, 24)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel22.TabIndex = 1413
        Me.MyLabel22.Text = "Ship To Location"
        '
        'txtship_to_loc_code
        '
        Me.txtship_to_loc_code.CalculationExpression = Nothing
        Me.txtship_to_loc_code.FieldCode = Nothing
        Me.txtship_to_loc_code.FieldDesc = Nothing
        Me.txtship_to_loc_code.FieldMaxLength = 0
        Me.txtship_to_loc_code.FieldName = Nothing
        Me.txtship_to_loc_code.isCalculatedField = False
        Me.txtship_to_loc_code.IsSourceFromTable = False
        Me.txtship_to_loc_code.IsSourceFromValueList = False
        Me.txtship_to_loc_code.IsUnique = False
        Me.txtship_to_loc_code.Location = New System.Drawing.Point(595, 24)
        Me.txtship_to_loc_code.MendatroryField = False
        Me.txtship_to_loc_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtship_to_loc_code.MyLinkLable1 = Me.MyLabel22
        Me.txtship_to_loc_code.MyLinkLable2 = Me.txtship_to_loc_name
        Me.txtship_to_loc_code.MyReadOnly = False
        Me.txtship_to_loc_code.MyShowMasterFormButton = False
        Me.txtship_to_loc_code.Name = "txtship_to_loc_code"
        Me.txtship_to_loc_code.ReferenceFieldDesc = Nothing
        Me.txtship_to_loc_code.ReferenceFieldName = Nothing
        Me.txtship_to_loc_code.ReferenceTableName = Nothing
        Me.txtship_to_loc_code.Size = New System.Drawing.Size(143, 19)
        Me.txtship_to_loc_code.TabIndex = 10
        Me.txtship_to_loc_code.Value = ""
        '
        'chk_F_Form
        '
        Me.chk_F_Form.Location = New System.Drawing.Point(742, 1)
        Me.chk_F_Form.MyLinkLable1 = Nothing
        Me.chk_F_Form.MyLinkLable2 = Nothing
        Me.chk_F_Form.Name = "chk_F_Form"
        Me.chk_F_Form.Size = New System.Drawing.Size(97, 18)
        Me.chk_F_Form.TabIndex = 3
        Me.chk_F_Form.Tag1 = Nothing
        Me.chk_F_Form.Text = "Against F-Form"
        '
        'txtExcisable
        '
        Me.txtExcisable.AutoSize = False
        Me.txtExcisable.BorderVisible = True
        Me.txtExcisable.FieldName = Nothing
        Me.txtExcisable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExcisable.Location = New System.Drawing.Point(83, 47)
        Me.txtExcisable.Name = "txtExcisable"
        Me.txtExcisable.Size = New System.Drawing.Size(19, 18)
        Me.txtExcisable.TabIndex = 4
        Me.txtExcisable.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtExcisable.TextWrap = False
        Me.txtExcisable.Visible = False
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(491, 136)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel20.TabIndex = 1410
        Me.MyLabel20.Text = "Vehicle Charge"
        '
        'txtvehicle_Charge
        '
        Me.txtvehicle_Charge.AutoSize = False
        Me.txtvehicle_Charge.BorderVisible = True
        Me.txtvehicle_Charge.FieldName = Nothing
        Me.txtvehicle_Charge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvehicle_Charge.Location = New System.Drawing.Point(595, 134)
        Me.txtvehicle_Charge.Name = "txtvehicle_Charge"
        Me.txtvehicle_Charge.Size = New System.Drawing.Size(143, 18)
        Me.txtvehicle_Charge.TabIndex = 18
        Me.txtvehicle_Charge.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'ttxway_bill_date
        '
        Me.ttxway_bill_date.CalculationExpression = Nothing
        Me.ttxway_bill_date.CustomFormat = "dd/MM/yyyy"
        Me.ttxway_bill_date.FieldCode = Nothing
        Me.ttxway_bill_date.FieldDesc = Nothing
        Me.ttxway_bill_date.FieldMaxLength = 0
        Me.ttxway_bill_date.FieldName = Nothing
        Me.ttxway_bill_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ttxway_bill_date.isCalculatedField = False
        Me.ttxway_bill_date.IsSourceFromTable = False
        Me.ttxway_bill_date.IsSourceFromValueList = False
        Me.ttxway_bill_date.IsUnique = False
        Me.ttxway_bill_date.Location = New System.Drawing.Point(820, 91)
        Me.ttxway_bill_date.MendatroryField = False
        Me.ttxway_bill_date.MyLinkLable1 = Me.MyLabel12
        Me.ttxway_bill_date.MyLinkLable2 = Nothing
        Me.ttxway_bill_date.Name = "ttxway_bill_date"
        Me.ttxway_bill_date.ReferenceFieldDesc = Nothing
        Me.ttxway_bill_date.ReferenceFieldName = Nothing
        Me.ttxway_bill_date.ReferenceTableName = Nothing
        Me.ttxway_bill_date.Size = New System.Drawing.Size(79, 20)
        Me.ttxway_bill_date.TabIndex = 14
        Me.ttxway_bill_date.TabStop = False
        Me.ttxway_bill_date.Text = "17/12/2014"
        Me.ttxway_bill_date.Value = New Date(2014, 12, 17, 11, 28, 4, 99)
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(743, 93)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel12.TabIndex = 1394
        Me.MyLabel12.Text = "WayBill Date"
        '
        'dtpGR
        '
        Me.dtpGR.CalculationExpression = Nothing
        Me.dtpGR.CustomFormat = "dd/MM/yyyy"
        Me.dtpGR.FieldCode = Nothing
        Me.dtpGR.FieldDesc = Nothing
        Me.dtpGR.FieldMaxLength = 0
        Me.dtpGR.FieldName = Nothing
        Me.dtpGR.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGR.isCalculatedField = False
        Me.dtpGR.IsSourceFromTable = False
        Me.dtpGR.IsSourceFromValueList = False
        Me.dtpGR.IsUnique = False
        Me.dtpGR.Location = New System.Drawing.Point(941, 114)
        Me.dtpGR.MendatroryField = False
        Me.dtpGR.MyLinkLable1 = Me.MyLabel18
        Me.dtpGR.MyLinkLable2 = Nothing
        Me.dtpGR.Name = "dtpGR"
        Me.dtpGR.ReferenceFieldDesc = Nothing
        Me.dtpGR.ReferenceFieldName = Nothing
        Me.dtpGR.ReferenceTableName = Nothing
        Me.dtpGR.Size = New System.Drawing.Size(79, 20)
        Me.dtpGR.TabIndex = 17
        Me.dtpGR.TabStop = False
        Me.dtpGR.Text = "17/12/2014"
        Me.dtpGR.Value = New Date(2014, 12, 17, 11, 28, 4, 99)
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(864, 115)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel18.TabIndex = 1408
        Me.MyLabel18.Text = "GR Date"
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(491, 115)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel19.TabIndex = 1407
        Me.MyLabel19.Text = "GR No."
        '
        'txtGR_No
        '
        Me.txtGR_No.CalculationExpression = Nothing
        Me.txtGR_No.FieldCode = Nothing
        Me.txtGR_No.FieldDesc = Nothing
        Me.txtGR_No.FieldMaxLength = 0
        Me.txtGR_No.FieldName = Nothing
        Me.txtGR_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGR_No.isCalculatedField = False
        Me.txtGR_No.IsSourceFromTable = False
        Me.txtGR_No.IsSourceFromValueList = False
        Me.txtGR_No.IsUnique = False
        Me.txtGR_No.Location = New System.Drawing.Point(595, 114)
        Me.txtGR_No.MaxLength = 30
        Me.txtGR_No.MendatroryField = False
        Me.txtGR_No.MyLinkLable1 = Me.MyLabel19
        Me.txtGR_No.MyLinkLable2 = Nothing
        Me.txtGR_No.Name = "txtGR_No"
        Me.txtGR_No.ReferenceFieldDesc = Nothing
        Me.txtGR_No.ReferenceFieldName = Nothing
        Me.txtGR_No.ReferenceTableName = Nothing
        Me.txtGR_No.Size = New System.Drawing.Size(143, 18)
        Me.txtGR_No.TabIndex = 16
        '
        'txtGross_Wt
        '
        Me.txtGross_Wt.CalculationExpression = Nothing
        Me.txtGross_Wt.DecimalPlaces = 0
        Me.txtGross_Wt.FieldCode = Nothing
        Me.txtGross_Wt.FieldDesc = Nothing
        Me.txtGross_Wt.FieldMaxLength = 0
        Me.txtGross_Wt.FieldName = Nothing
        Me.txtGross_Wt.isCalculatedField = False
        Me.txtGross_Wt.IsSourceFromTable = False
        Me.txtGross_Wt.IsSourceFromValueList = False
        Me.txtGross_Wt.IsUnique = False
        Me.txtGross_Wt.Location = New System.Drawing.Point(365, 153)
        Me.txtGross_Wt.MendatroryField = False
        Me.txtGross_Wt.MyLinkLable1 = Me.MyLabel17
        Me.txtGross_Wt.MyLinkLable2 = Nothing
        Me.txtGross_Wt.Name = "txtGross_Wt"
        Me.txtGross_Wt.ReferenceFieldDesc = Nothing
        Me.txtGross_Wt.ReferenceFieldName = Nothing
        Me.txtGross_Wt.ReferenceTableName = Nothing
        Me.txtGross_Wt.Size = New System.Drawing.Size(120, 20)
        Me.txtGross_Wt.TabIndex = 9
        Me.txtGross_Wt.Text = "0"
        Me.txtGross_Wt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGross_Wt.Value = 0.0R
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(245, 154)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel17.TabIndex = 1403
        Me.MyLabel17.Text = "Gross Weight"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(0, 156)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel16.TabIndex = 1402
        Me.MyLabel16.Text = "Total Weight"
        '
        'txttotal_Wt
        '
        Me.txttotal_Wt.AutoSize = False
        Me.txttotal_Wt.BorderVisible = True
        Me.txttotal_Wt.FieldName = Nothing
        Me.txttotal_Wt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotal_Wt.Location = New System.Drawing.Point(100, 154)
        Me.txttotal_Wt.Name = "txttotal_Wt"
        Me.txttotal_Wt.Size = New System.Drawing.Size(143, 18)
        Me.txttotal_Wt.TabIndex = 1401
        Me.txttotal_Wt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVehicle_Capacity
        '
        Me.txtVehicle_Capacity.CalculationExpression = Nothing
        Me.txtVehicle_Capacity.DecimalPlaces = 0
        Me.txtVehicle_Capacity.FieldCode = Nothing
        Me.txtVehicle_Capacity.FieldDesc = Nothing
        Me.txtVehicle_Capacity.FieldMaxLength = 0
        Me.txtVehicle_Capacity.FieldName = Nothing
        Me.txtVehicle_Capacity.isCalculatedField = False
        Me.txtVehicle_Capacity.IsSourceFromTable = False
        Me.txtVehicle_Capacity.IsSourceFromValueList = False
        Me.txtVehicle_Capacity.IsUnique = False
        Me.txtVehicle_Capacity.Location = New System.Drawing.Point(365, 133)
        Me.txtVehicle_Capacity.MendatroryField = False
        Me.txtVehicle_Capacity.MyLinkLable1 = Me.MyLabel15
        Me.txtVehicle_Capacity.MyLinkLable2 = Nothing
        Me.txtVehicle_Capacity.Name = "txtVehicle_Capacity"
        Me.txtVehicle_Capacity.ReferenceFieldDesc = Nothing
        Me.txtVehicle_Capacity.ReferenceFieldName = Nothing
        Me.txtVehicle_Capacity.ReferenceTableName = Nothing
        Me.txtVehicle_Capacity.Size = New System.Drawing.Size(120, 20)
        Me.txtVehicle_Capacity.TabIndex = 8
        Me.txtVehicle_Capacity.Text = "0"
        Me.txtVehicle_Capacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtVehicle_Capacity.Value = 0.0R
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(245, 134)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel15.TabIndex = 1399
        Me.MyLabel15.Text = "Vehicle Capacity"
        '
        'txtvehicle_code
        '
        Me.txtvehicle_code.CalculationExpression = Nothing
        Me.txtvehicle_code.FieldCode = Nothing
        Me.txtvehicle_code.FieldDesc = Nothing
        Me.txtvehicle_code.FieldMaxLength = 0
        Me.txtvehicle_code.FieldName = Nothing
        Me.txtvehicle_code.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvehicle_code.isCalculatedField = False
        Me.txtvehicle_code.IsSourceFromTable = False
        Me.txtvehicle_code.IsSourceFromValueList = False
        Me.txtvehicle_code.IsUnique = False
        Me.txtvehicle_code.Location = New System.Drawing.Point(100, 134)
        Me.txtvehicle_code.MaxLength = 30
        Me.txtvehicle_code.MendatroryField = False
        Me.txtvehicle_code.MyLinkLable1 = Me.MyLabel14
        Me.txtvehicle_code.MyLinkLable2 = Nothing
        Me.txtvehicle_code.Name = "txtvehicle_code"
        Me.txtvehicle_code.ReferenceFieldDesc = Nothing
        Me.txtvehicle_code.ReferenceFieldName = Nothing
        Me.txtvehicle_code.ReferenceTableName = Nothing
        Me.txtvehicle_code.Size = New System.Drawing.Size(143, 18)
        Me.txtvehicle_code.TabIndex = 7
        '
        'txtTransporter_desc
        '
        Me.txtTransporter_desc.AutoSize = False
        Me.txtTransporter_desc.BorderVisible = True
        Me.txtTransporter_desc.FieldName = Nothing
        Me.txtTransporter_desc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter_desc.Location = New System.Drawing.Point(245, 111)
        Me.txtTransporter_desc.Name = "txtTransporter_desc"
        Me.txtTransporter_desc.Size = New System.Drawing.Size(241, 18)
        Me.txtTransporter_desc.TabIndex = 1391
        Me.txtTransporter_desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTransporter_desc.TextWrap = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(2, 112)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel13.TabIndex = 1392
        Me.MyLabel13.Text = "Transporter"
        '
        'txtTransporter_Code
        '
        Me.txtTransporter_Code.CalculationExpression = Nothing
        Me.txtTransporter_Code.FieldCode = Nothing
        Me.txtTransporter_Code.FieldDesc = Nothing
        Me.txtTransporter_Code.FieldMaxLength = 0
        Me.txtTransporter_Code.FieldName = Nothing
        Me.txtTransporter_Code.isCalculatedField = False
        Me.txtTransporter_Code.IsSourceFromTable = False
        Me.txtTransporter_Code.IsSourceFromValueList = False
        Me.txtTransporter_Code.IsUnique = False
        Me.txtTransporter_Code.Location = New System.Drawing.Point(100, 111)
        Me.txtTransporter_Code.MendatroryField = False
        Me.txtTransporter_Code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter_Code.MyLinkLable1 = Me.MyLabel13
        Me.txtTransporter_Code.MyLinkLable2 = Me.txtTransporter_desc
        Me.txtTransporter_Code.MyReadOnly = False
        Me.txtTransporter_Code.MyShowMasterFormButton = False
        Me.txtTransporter_Code.Name = "txtTransporter_Code"
        Me.txtTransporter_Code.ReferenceFieldDesc = Nothing
        Me.txtTransporter_Code.ReferenceFieldName = Nothing
        Me.txtTransporter_Code.ReferenceTableName = Nothing
        Me.txtTransporter_Code.Size = New System.Drawing.Size(143, 19)
        Me.txtTransporter_Code.TabIndex = 6
        Me.txtTransporter_Code.Value = ""
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Location = New System.Drawing.Point(1022, 92)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(43, 18)
        Me.btnupdate.TabIndex = 15
        Me.btnupdate.Text = "Update"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(491, 93)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel11.TabIndex = 1388
        Me.MyLabel11.Text = "WayBill No."
        '
        'txtWayBill_No
        '
        Me.txtWayBill_No.CalculationExpression = Nothing
        Me.txtWayBill_No.FieldCode = Nothing
        Me.txtWayBill_No.FieldDesc = Nothing
        Me.txtWayBill_No.FieldMaxLength = 0
        Me.txtWayBill_No.FieldName = Nothing
        Me.txtWayBill_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWayBill_No.isCalculatedField = False
        Me.txtWayBill_No.IsSourceFromTable = False
        Me.txtWayBill_No.IsSourceFromValueList = False
        Me.txtWayBill_No.IsUnique = False
        Me.txtWayBill_No.Location = New System.Drawing.Point(595, 92)
        Me.txtWayBill_No.MaxLength = 30
        Me.txtWayBill_No.MendatroryField = False
        Me.txtWayBill_No.MyLinkLable1 = Me.MyLabel11
        Me.txtWayBill_No.MyLinkLable2 = Nothing
        Me.txtWayBill_No.Name = "txtWayBill_No"
        Me.txtWayBill_No.ReferenceFieldDesc = Nothing
        Me.txtWayBill_No.ReferenceFieldName = Nothing
        Me.txtWayBill_No.ReferenceTableName = Nothing
        Me.txtWayBill_No.Size = New System.Drawing.Size(143, 18)
        Me.txtWayBill_No.TabIndex = 13
        '
        'cmbTax
        '
        Me.cmbTax.AutoCompleteDisplayMember = Nothing
        Me.cmbTax.AutoCompleteValueMember = Nothing
        Me.cmbTax.CalculationExpression = Nothing
        Me.cmbTax.FieldCode = Nothing
        Me.cmbTax.FieldDesc = Nothing
        Me.cmbTax.FieldMaxLength = 0
        Me.cmbTax.FieldName = Nothing
        Me.cmbTax.isCalculatedField = False
        Me.cmbTax.IsSourceFromTable = False
        Me.cmbTax.IsSourceFromValueList = False
        Me.cmbTax.IsUnique = False
        Me.cmbTax.Location = New System.Drawing.Point(342, 65)
        Me.cmbTax.MendatroryField = True
        Me.cmbTax.MyLinkLable1 = Me.MyLabel9
        Me.cmbTax.MyLinkLable2 = Nothing
        Me.cmbTax.Name = "cmbTax"
        Me.cmbTax.ReferenceFieldDesc = Nothing
        Me.cmbTax.ReferenceFieldName = Nothing
        Me.cmbTax.ReferenceTableName = Nothing
        Me.cmbTax.Size = New System.Drawing.Size(143, 20)
        Me.cmbTax.TabIndex = 4
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(250, 68)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel9.TabIndex = 1386
        Me.MyLabel9.Text = "Including Tax"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(2, 69)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel7.TabIndex = 137
        Me.MyLabel7.Text = "CSA Rate"
        '
        'txtCSARate
        '
        Me.txtCSARate.CalculationExpression = Nothing
        Me.txtCSARate.FieldCode = Nothing
        Me.txtCSARate.FieldDesc = Nothing
        Me.txtCSARate.FieldMaxLength = 0
        Me.txtCSARate.FieldName = Nothing
        Me.txtCSARate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSARate.isCalculatedField = False
        Me.txtCSARate.IsSourceFromTable = False
        Me.txtCSARate.IsSourceFromValueList = False
        Me.txtCSARate.IsUnique = False
        Me.txtCSARate.Location = New System.Drawing.Point(100, 68)
        Me.txtCSARate.MaxLength = 200
        Me.txtCSARate.MendatroryField = False
        Me.txtCSARate.MyLinkLable1 = Me.MyLabel7
        Me.txtCSARate.MyLinkLable2 = Nothing
        Me.txtCSARate.Name = "txtCSARate"
        Me.txtCSARate.ReferenceFieldDesc = Nothing
        Me.txtCSARate.ReferenceFieldName = Nothing
        Me.txtCSARate.ReferenceTableName = Nothing
        Me.txtCSARate.Size = New System.Drawing.Size(144, 18)
        Me.txtCSARate.TabIndex = 3
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(491, 2)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel8.TabIndex = 135
        Me.MyLabel8.Text = "DO No"
        '
        'fndDONo
        '
        Me.fndDONo.CalculationExpression = Nothing
        Me.fndDONo.FieldCode = Nothing
        Me.fndDONo.FieldDesc = Nothing
        Me.fndDONo.FieldMaxLength = 0
        Me.fndDONo.FieldName = Nothing
        Me.fndDONo.isCalculatedField = False
        Me.fndDONo.IsSourceFromTable = False
        Me.fndDONo.IsSourceFromValueList = False
        Me.fndDONo.IsUnique = False
        Me.fndDONo.Location = New System.Drawing.Point(595, 2)
        Me.fndDONo.MendatroryField = True
        Me.fndDONo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDONo.MyLinkLable1 = Me.MyLabel8
        Me.fndDONo.MyLinkLable2 = Nothing
        Me.fndDONo.MyReadOnly = False
        Me.fndDONo.MyShowMasterFormButton = False
        Me.fndDONo.Name = "fndDONo"
        Me.fndDONo.ReferenceFieldDesc = Nothing
        Me.fndDONo.ReferenceFieldName = Nothing
        Me.fndDONo.ReferenceTableName = Nothing
        Me.fndDONo.Size = New System.Drawing.Size(143, 19)
        Me.fndDONo.TabIndex = 2
        Me.fndDONo.Value = ""
        '
        'txtStateDesc
        '
        Me.txtStateDesc.AutoSize = False
        Me.txtStateDesc.BorderVisible = True
        Me.txtStateDesc.FieldName = Nothing
        Me.txtStateDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStateDesc.Location = New System.Drawing.Point(742, 69)
        Me.txtStateDesc.Name = "txtStateDesc"
        Me.txtStateDesc.Size = New System.Drawing.Size(324, 18)
        Me.txtStateDesc.TabIndex = 131
        Me.txtStateDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtStateDesc.TextWrap = False
        Me.txtStateDesc.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(491, 71)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel4.TabIndex = 132
        Me.MyLabel4.Text = "State"
        Me.MyLabel4.Visible = False
        '
        'txtState
        '
        Me.txtState.CalculationExpression = Nothing
        Me.txtState.Enabled = False
        Me.txtState.FieldCode = Nothing
        Me.txtState.FieldDesc = Nothing
        Me.txtState.FieldMaxLength = 0
        Me.txtState.FieldName = Nothing
        Me.txtState.isCalculatedField = False
        Me.txtState.IsSourceFromTable = False
        Me.txtState.IsSourceFromValueList = False
        Me.txtState.IsUnique = False
        Me.txtState.Location = New System.Drawing.Point(595, 69)
        Me.txtState.MendatroryField = True
        Me.txtState.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.MyLinkLable1 = Me.MyLabel4
        Me.txtState.MyLinkLable2 = Me.txtStateDesc
        Me.txtState.MyReadOnly = False
        Me.txtState.MyShowMasterFormButton = False
        Me.txtState.Name = "txtState"
        Me.txtState.ReferenceFieldDesc = Nothing
        Me.txtState.ReferenceFieldName = Nothing
        Me.txtState.ReferenceTableName = Nothing
        Me.txtState.Size = New System.Drawing.Size(143, 19)
        Me.txtState.TabIndex = 12
        Me.txtState.Value = ""
        Me.txtState.Visible = False
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(407, 2)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(79, 20)
        Me.txtDate.TabIndex = 1
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(491, 154)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel5.TabIndex = 126
        Me.MyLabel5.Text = "Document Amount"
        '
        'txtDocumentTotal
        '
        Me.txtDocumentTotal.AutoSize = False
        Me.txtDocumentTotal.BorderVisible = True
        Me.txtDocumentTotal.FieldName = Nothing
        Me.txtDocumentTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentTotal.Location = New System.Drawing.Point(595, 153)
        Me.txtDocumentTotal.Name = "txtDocumentTotal"
        Me.txtDocumentTotal.Size = New System.Drawing.Size(143, 18)
        Me.txtDocumentTotal.TabIndex = 20
        Me.txtDocumentTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(757, 426)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(318, 16)
        Me.RadLabel12.TabIndex = 25
        Me.RadLabel12.Text = "Double click on Tax Amount Column To Set Item wise Tax"
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0.0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(2, 373)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 70)
        Me.UcItemBalance1.TabIndex = 29
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(1111, 185)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel21.TabIndex = 20
        Me.RadLabel21.Text = "Item Type"
        Me.RadLabel21.Visible = False
        '
        'cboItemType
        '
        Me.cboItemType.AutoCompleteDisplayMember = Nothing
        Me.cboItemType.AutoCompleteValueMember = Nothing
        Me.cboItemType.CalculationExpression = Nothing
        Me.cboItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboItemType.FieldCode = Nothing
        Me.cboItemType.FieldDesc = Nothing
        Me.cboItemType.FieldMaxLength = 0
        Me.cboItemType.FieldName = Nothing
        Me.cboItemType.isCalculatedField = False
        Me.cboItemType.IsSourceFromTable = False
        Me.cboItemType.IsSourceFromValueList = False
        Me.cboItemType.IsUnique = False
        Me.cboItemType.Location = New System.Drawing.Point(1174, 183)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel21
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(135, 20)
        Me.cboItemType.TabIndex = 19
        Me.cboItemType.Visible = False
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(744, 85)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(2, 2)
        Me.lblAbandonmentNo.TabIndex = 27
        '
        'txtToLocationDesc
        '
        Me.txtToLocationDesc.AutoSize = False
        Me.txtToLocationDesc.BorderVisible = True
        Me.txtToLocationDesc.FieldName = Nothing
        Me.txtToLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToLocationDesc.Location = New System.Drawing.Point(742, 47)
        Me.txtToLocationDesc.Name = "txtToLocationDesc"
        Me.txtToLocationDesc.Size = New System.Drawing.Size(324, 18)
        Me.txtToLocationDesc.TabIndex = 13
        Me.txtToLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtToLocationDesc.TextWrap = False
        Me.txtToLocationDesc.Visible = False
        '
        'txtFromLocationDesc
        '
        Me.txtFromLocationDesc.AutoSize = False
        Me.txtFromLocationDesc.BorderVisible = True
        Me.txtFromLocationDesc.FieldName = Nothing
        Me.txtFromLocationDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLocationDesc.Location = New System.Drawing.Point(244, 47)
        Me.txtFromLocationDesc.Name = "txtFromLocationDesc"
        Me.txtFromLocationDesc.Size = New System.Drawing.Size(242, 18)
        Me.txtFromLocationDesc.TabIndex = 7
        Me.txtFromLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFromLocationDesc.TextWrap = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(2, 24)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(491, 47)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel18.TabIndex = 19
        Me.RadLabel18.Text = "To Location"
        Me.RadLabel18.Visible = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(2, 48)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(79, 16)
        Me.RadLabel15.TabIndex = 20
        Me.RadLabel15.Text = "From Location"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel20)
        Me.RadGroupBox2.Controls.Add(Me.txtDept)
        Me.RadGroupBox2.Controls.Add(Me.lblDept)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 216)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1074, 156)
        Me.RadGroupBox2.TabIndex = 18
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1054, 126)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RadLabel20
        '
        Me.RadLabel20.FieldName = Nothing
        Me.RadLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel20.Location = New System.Drawing.Point(69, 38)
        Me.RadLabel20.Name = "RadLabel20"
        Me.RadLabel20.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel20.TabIndex = 0
        Me.RadLabel20.Text = "Department"
        Me.RadLabel20.Visible = False
        '
        'txtDept
        '
        Me.txtDept.CalculationExpression = Nothing
        Me.txtDept.FieldCode = Nothing
        Me.txtDept.FieldDesc = Nothing
        Me.txtDept.FieldMaxLength = 0
        Me.txtDept.FieldName = Nothing
        Me.txtDept.isCalculatedField = False
        Me.txtDept.IsSourceFromTable = False
        Me.txtDept.IsSourceFromValueList = False
        Me.txtDept.IsUnique = False
        Me.txtDept.Location = New System.Drawing.Point(168, 37)
        Me.txtDept.MendatroryField = False
        Me.txtDept.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.MyLinkLable1 = Me.RadLabel20
        Me.txtDept.MyLinkLable2 = Me.lblDept
        Me.txtDept.MyReadOnly = False
        Me.txtDept.MyShowMasterFormButton = False
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReferenceFieldDesc = Nothing
        Me.txtDept.ReferenceFieldName = Nothing
        Me.txtDept.ReferenceTableName = Nothing
        Me.txtDept.Size = New System.Drawing.Size(143, 18)
        Me.txtDept.TabIndex = 10
        Me.txtDept.Value = ""
        Me.txtDept.Visible = False
        '
        'lblDept
        '
        Me.lblDept.AutoSize = False
        Me.lblDept.BorderVisible = True
        Me.lblDept.FieldName = Nothing
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.Location = New System.Drawing.Point(312, 37)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(242, 18)
        Me.lblDept.TabIndex = 16
        Me.lblDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDept.TextWrap = False
        Me.lblDept.Visible = False
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(375, 3)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'txtCustDesc
        '
        Me.txtCustDesc.AutoSize = False
        Me.txtCustDesc.BorderVisible = True
        Me.txtCustDesc.FieldName = Nothing
        Me.txtCustDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustDesc.Location = New System.Drawing.Point(245, 89)
        Me.txtCustDesc.Name = "txtCustDesc"
        Me.txtCustDesc.Size = New System.Drawing.Size(241, 18)
        Me.txtCustDesc.TabIndex = 10
        Me.txtCustDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustDesc.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(2, 90)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel2.TabIndex = 21
        Me.RadLabel2.Text = "Customer"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(2, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = CType(resources.GetObject("btnAddNew.Image"), System.Drawing.Image)
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(349, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'txtCustCode
        '
        Me.txtCustCode.CalculationExpression = Nothing
        Me.txtCustCode.Enabled = False
        Me.txtCustCode.FieldCode = Nothing
        Me.txtCustCode.FieldDesc = Nothing
        Me.txtCustCode.FieldMaxLength = 0
        Me.txtCustCode.FieldName = Nothing
        Me.txtCustCode.isCalculatedField = False
        Me.txtCustCode.IsSourceFromTable = False
        Me.txtCustCode.IsSourceFromValueList = False
        Me.txtCustCode.IsUnique = False
        Me.txtCustCode.Location = New System.Drawing.Point(100, 89)
        Me.txtCustCode.MendatroryField = True
        Me.txtCustCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustCode.MyLinkLable1 = Me.RadLabel2
        Me.txtCustCode.MyLinkLable2 = Me.txtCustDesc
        Me.txtCustCode.MyReadOnly = False
        Me.txtCustCode.MyShowMasterFormButton = False
        Me.txtCustCode.Name = "txtCustCode"
        Me.txtCustCode.ReferenceFieldDesc = Nothing
        Me.txtCustCode.ReferenceFieldName = Nothing
        Me.txtCustCode.ReferenceTableName = Nothing
        Me.txtCustCode.Size = New System.Drawing.Size(143, 18)
        Me.txtCustCode.TabIndex = 5
        Me.txtCustCode.Value = ""
        '
        'txtToLocation
        '
        Me.txtToLocation.CalculationExpression = Nothing
        Me.txtToLocation.Enabled = False
        Me.txtToLocation.FieldCode = Nothing
        Me.txtToLocation.FieldDesc = Nothing
        Me.txtToLocation.FieldMaxLength = 0
        Me.txtToLocation.FieldName = Nothing
        Me.txtToLocation.isCalculatedField = False
        Me.txtToLocation.IsSourceFromTable = False
        Me.txtToLocation.IsSourceFromValueList = False
        Me.txtToLocation.IsUnique = False
        Me.txtToLocation.Location = New System.Drawing.Point(595, 47)
        Me.txtToLocation.MendatroryField = True
        Me.txtToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToLocation.MyLinkLable1 = Me.RadLabel18
        Me.txtToLocation.MyLinkLable2 = Me.txtToLocationDesc
        Me.txtToLocation.MyReadOnly = False
        Me.txtToLocation.MyShowMasterFormButton = False
        Me.txtToLocation.Name = "txtToLocation"
        Me.txtToLocation.ReferenceFieldDesc = Nothing
        Me.txtToLocation.ReferenceFieldName = Nothing
        Me.txtToLocation.ReferenceTableName = Nothing
        Me.txtToLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtToLocation.TabIndex = 11
        Me.txtToLocation.Value = ""
        Me.txtToLocation.Visible = False
        '
        'txtFromLocation
        '
        Me.txtFromLocation.CalculationExpression = Nothing
        Me.txtFromLocation.Enabled = False
        Me.txtFromLocation.FieldCode = Nothing
        Me.txtFromLocation.FieldDesc = Nothing
        Me.txtFromLocation.FieldMaxLength = 0
        Me.txtFromLocation.FieldName = Nothing
        Me.txtFromLocation.isCalculatedField = False
        Me.txtFromLocation.IsSourceFromTable = False
        Me.txtFromLocation.IsSourceFromValueList = False
        Me.txtFromLocation.IsUnique = False
        Me.txtFromLocation.Location = New System.Drawing.Point(100, 47)
        Me.txtFromLocation.MendatroryField = True
        Me.txtFromLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtFromLocation.MyLinkLable2 = Me.txtFromLocationDesc
        Me.txtFromLocation.MyReadOnly = False
        Me.txtFromLocation.MyShowMasterFormButton = False
        Me.txtFromLocation.Name = "txtFromLocation"
        Me.txtFromLocation.ReferenceFieldDesc = Nothing
        Me.txtFromLocation.ReferenceFieldName = Nothing
        Me.txtFromLocation.ReferenceTableName = Nothing
        Me.txtFromLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtFromLocation.TabIndex = 4
        Me.txtFromLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(967, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(98, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
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
        Me.txtDesc.Location = New System.Drawing.Point(99, 24)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(386, 18)
        Me.txtDesc.TabIndex = 3
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1079, 445)
        Me.RadPageViewPage2.Text = "Taxes"
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
        Me.txtTaxGroup.Location = New System.Drawing.Point(69, 3)
        Me.txtTaxGroup.MendatroryField = True
        Me.txtTaxGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxGroup.MyLinkLable1 = Me.RadLabel11
        Me.txtTaxGroup.MyLinkLable2 = Me.lblTaxGrpName
        Me.txtTaxGroup.MyReadOnly = False
        Me.txtTaxGroup.MyShowMasterFormButton = False
        Me.txtTaxGroup.Name = "txtTaxGroup"
        Me.txtTaxGroup.ReferenceFieldDesc = Nothing
        Me.txtTaxGroup.ReferenceFieldName = Nothing
        Me.txtTaxGroup.ReferenceTableName = Nothing
        Me.txtTaxGroup.Size = New System.Drawing.Size(143, 19)
        Me.txtTaxGroup.TabIndex = 0
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(3, 4)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 2
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(220, 3)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 1
        Me.lblTaxGrpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(921, 341)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 4
        Me.RadLabel10.Text = "Double click To Chage Rate"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.txtTermCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel16)
        Me.RadGroupBox1.Controls.Add(Me.txtDueDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel17)
        Me.RadGroupBox1.Controls.Add(Me.lblTermName)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Terms"
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 354)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1079, 87)
        Me.RadGroupBox1.TabIndex = 14
        Me.RadGroupBox1.Text = "Terms"
        Me.RadGroupBox1.Visible = False
        '
        'txtTermCode
        '
        Me.txtTermCode.CalculationExpression = Nothing
        Me.txtTermCode.FieldCode = Nothing
        Me.txtTermCode.FieldDesc = Nothing
        Me.txtTermCode.FieldMaxLength = 0
        Me.txtTermCode.FieldName = Nothing
        Me.txtTermCode.isCalculatedField = False
        Me.txtTermCode.IsSourceFromTable = False
        Me.txtTermCode.IsSourceFromValueList = False
        Me.txtTermCode.IsUnique = False
        Me.txtTermCode.Location = New System.Drawing.Point(68, 23)
        Me.txtTermCode.MendatroryField = False
        Me.txtTermCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTermCode.MyLinkLable1 = Me.RadLabel16
        Me.txtTermCode.MyLinkLable2 = Me.lblTermName
        Me.txtTermCode.MyReadOnly = False
        Me.txtTermCode.MyShowMasterFormButton = False
        Me.txtTermCode.Name = "txtTermCode"
        Me.txtTermCode.ReferenceFieldDesc = Nothing
        Me.txtTermCode.ReferenceFieldName = Nothing
        Me.txtTermCode.ReferenceTableName = Nothing
        Me.txtTermCode.Size = New System.Drawing.Size(143, 19)
        Me.txtTermCode.TabIndex = 0
        Me.txtTermCode.Value = ""
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(6, 24)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel16.TabIndex = 13
        Me.RadLabel16.Text = "Term Code"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(220, 22)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(321, 20)
        Me.lblTermName.TabIndex = 1
        Me.lblTermName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTermName.TextWrap = False
        '
        'txtDueDate
        '
        Me.txtDueDate.CalculationExpression = Nothing
        Me.txtDueDate.CustomFormat = "dd-MM-yyyy"
        Me.txtDueDate.FieldCode = Nothing
        Me.txtDueDate.FieldDesc = Nothing
        Me.txtDueDate.FieldMaxLength = 0
        Me.txtDueDate.FieldName = Nothing
        Me.txtDueDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDueDate.isCalculatedField = False
        Me.txtDueDate.IsSourceFromTable = False
        Me.txtDueDate.IsSourceFromValueList = False
        Me.txtDueDate.IsUnique = False
        Me.txtDueDate.Location = New System.Drawing.Point(70, 57)
        Me.txtDueDate.MendatroryField = False
        Me.txtDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.MyLinkLable1 = Me.RadLabel17
        Me.txtDueDate.MyLinkLable2 = Nothing
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.ReferenceFieldDesc = Nothing
        Me.txtDueDate.ReferenceFieldName = Nothing
        Me.txtDueDate.ReferenceTableName = Nothing
        Me.txtDueDate.Size = New System.Drawing.Size(81, 18)
        Me.txtDueDate.TabIndex = 3
        Me.txtDueDate.TabStop = False
        Me.txtDueDate.Text = "13-06-2011"
        Me.txtDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel17.Location = New System.Drawing.Point(6, 58)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel17.TabIndex = 12
        Me.RadLabel17.Text = "Due Date"
        '
        'gv2
        '
        Me.gv2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(2, 34)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1074, 303)
        Me.gv2.TabIndex = 1
        Me.gv2.TabStop = False
        Me.gv2.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvAC)
        Me.RadPageViewPage3.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1079, 445)
        Me.RadPageViewPage3.Text = "Additional Charges"
        '
        'gvAC
        '
        Me.gvAC.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvAC.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvAC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvAC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvAC.ForeColor = System.Drawing.Color.Black
        Me.gvAC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvAC.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvAC.MasterTemplate.AllowDeleteRow = False
        Me.gvAC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAC.Name = "gvAC"
        Me.gvAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAC.ShowGroupPanel = False
        Me.gvAC.ShowHeaderCellButtons = True
        Me.gvAC.Size = New System.Drawing.Size(1079, 396)
        Me.gvAC.TabIndex = 3
        Me.gvAC.TabStop = False
        Me.gvAC.Text = "RadGridView1"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadLabel31)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 396)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1079, 49)
        Me.RadPanel1.TabIndex = 0
        '
        'RadLabel31
        '
        Me.RadLabel31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(834, 16)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(130, 16)
        Me.RadLabel31.TabIndex = 130
        Me.RadLabel31.Text = "Total Additional Charges"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(902, 445)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(902, 445)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1079, 445)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1079, 445)
        Me.UcAttachment1.TabIndex = 0
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges)
        Me.RadPageViewPage4.Controls.Add(Me.lblCommissionCharges)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotalAmount)
        Me.RadPageViewPage4.Controls.Add(Me.lblInvoiceDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscPer)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage4.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotalOtherCharges)
        Me.RadPageViewPage4.Controls.Add(Me.txtComment)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotalTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1079, 445)
        Me.RadPageViewPage4.Text = "Total"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(32, 140)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(114, 16)
        Me.MyLabel10.TabIndex = 154
        Me.MyLabel10.Text = "Commission Charges"
        '
        'lblAddCharges
        '
        Me.lblAddCharges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddCharges.AutoSize = False
        Me.lblAddCharges.BorderVisible = True
        Me.lblAddCharges.FieldName = Nothing
        Me.lblAddCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges.Location = New System.Drawing.Point(674, 402)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 131
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAddCharges.Visible = False
        '
        'lblCommissionCharges
        '
        Me.lblCommissionCharges.AutoSize = False
        Me.lblCommissionCharges.BorderVisible = True
        Me.lblCommissionCharges.FieldName = Nothing
        Me.lblCommissionCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommissionCharges.Location = New System.Drawing.Point(156, 140)
        Me.lblCommissionCharges.Name = "lblCommissionCharges"
        Me.lblCommissionCharges.Size = New System.Drawing.Size(165, 18)
        Me.lblCommissionCharges.TabIndex = 153
        Me.lblCommissionCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(72, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel2.TabIndex = 152
        Me.MyLabel2.Text = "Total Amount"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = False
        Me.lblTotalAmount.BorderVisible = True
        Me.lblTotalAmount.FieldName = Nothing
        Me.lblTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.Location = New System.Drawing.Point(156, 72)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(165, 18)
        Me.lblTotalAmount.TabIndex = 151
        Me.lblTotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvoiceDiscAmt
        '
        Me.lblInvoiceDiscAmt.AutoSize = False
        Me.lblInvoiceDiscAmt.BorderVisible = True
        Me.lblInvoiceDiscAmt.FieldName = Nothing
        Me.lblInvoiceDiscAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceDiscAmt.Location = New System.Drawing.Point(674, 322)
        Me.lblInvoiceDiscAmt.Name = "lblInvoiceDiscAmt"
        Me.lblInvoiceDiscAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblInvoiceDiscAmt.TabIndex = 7
        Me.lblInvoiceDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblInvoiceDiscAmt.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(529, 322)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel6.TabIndex = 150
        Me.MyLabel6.Text = "- Invoice Discount Amount"
        Me.MyLabel6.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(598, 274)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel3.TabIndex = 3
        Me.MyLabel3.Text = "Discount On"
        Me.MyLabel3.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnAmt)
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnRate)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(674, 271)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(141, 21)
        Me.RadGroupBox3.TabIndex = 4
        Me.RadGroupBox3.Visible = False
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
        Me.chkDiscountOnRate.Text = "Rate"
        Me.chkDiscountOnRate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkDiscountOnRate.Visible = False
        '
        'txtDiscAmt
        '
        Me.txtDiscAmt.BackColor = System.Drawing.Color.White
        Me.txtDiscAmt.CalculationExpression = Nothing
        Me.txtDiscAmt.DecimalPlaces = 5
        Me.txtDiscAmt.FieldCode = Nothing
        Me.txtDiscAmt.FieldDesc = Nothing
        Me.txtDiscAmt.FieldMaxLength = 0
        Me.txtDiscAmt.FieldName = Nothing
        Me.txtDiscAmt.isCalculatedField = False
        Me.txtDiscAmt.IsSourceFromTable = False
        Me.txtDiscAmt.IsSourceFromValueList = False
        Me.txtDiscAmt.IsUnique = False
        Me.txtDiscAmt.Location = New System.Drawing.Point(734, 298)
        Me.txtDiscAmt.MendatroryField = False
        Me.txtDiscAmt.MyLinkLable1 = Nothing
        Me.txtDiscAmt.MyLinkLable2 = Nothing
        Me.txtDiscAmt.Name = "txtDiscAmt"
        Me.txtDiscAmt.ReferenceFieldDesc = Nothing
        Me.txtDiscAmt.ReferenceFieldName = Nothing
        Me.txtDiscAmt.ReferenceTableName = Nothing
        Me.txtDiscAmt.Size = New System.Drawing.Size(80, 20)
        Me.txtDiscAmt.TabIndex = 6
        Me.txtDiscAmt.Text = "0"
        Me.txtDiscAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscAmt.Value = 0.0R
        Me.txtDiscAmt.Visible = False
        '
        'txtDiscPer
        '
        Me.txtDiscPer.BackColor = System.Drawing.Color.White
        Me.txtDiscPer.CalculationExpression = Nothing
        Me.txtDiscPer.DecimalPlaces = 5
        Me.txtDiscPer.FieldCode = Nothing
        Me.txtDiscPer.FieldDesc = Nothing
        Me.txtDiscPer.FieldMaxLength = 0
        Me.txtDiscPer.FieldName = Nothing
        Me.txtDiscPer.isCalculatedField = False
        Me.txtDiscPer.IsSourceFromTable = False
        Me.txtDiscPer.IsSourceFromValueList = False
        Me.txtDiscPer.IsUnique = False
        Me.txtDiscPer.Location = New System.Drawing.Point(673, 298)
        Me.txtDiscPer.MendatroryField = False
        Me.txtDiscPer.MyLinkLable1 = Nothing
        Me.txtDiscPer.MyLinkLable2 = Nothing
        Me.txtDiscPer.Name = "txtDiscPer"
        Me.txtDiscPer.ReferenceFieldDesc = Nothing
        Me.txtDiscPer.ReferenceFieldName = Nothing
        Me.txtDiscPer.ReferenceTableName = Nothing
        Me.txtDiscPer.Size = New System.Drawing.Size(39, 20)
        Me.txtDiscPer.TabIndex = 5
        Me.txtDiscPer.Text = "0"
        Me.txtDiscPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDiscPer.Value = 0.0R
        Me.txtDiscPer.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(715, 300)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(15, 18)
        Me.MyLabel1.TabIndex = 148
        Me.MyLabel1.Text = "%"
        Me.MyLabel1.Visible = False
        '
        'pnlCurrConv
        '
        Me.pnlCurrConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCurrConv.Controls.Add(Me.txtConversionRate)
        Me.pnlCurrConv.Controls.Add(Me.txtCurrencyCode)
        Me.pnlCurrConv.Controls.Add(Me.lblEffectiveFrom)
        Me.pnlCurrConv.Controls.Add(Me.txtApplicableFrom)
        Me.pnlCurrConv.Controls.Add(Me.lblCurrency)
        Me.pnlCurrConv.Controls.Add(Me.lblConvRate)
        Me.pnlCurrConv.Location = New System.Drawing.Point(96, 28)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(688, 38)
        Me.pnlCurrConv.TabIndex = 0
        Me.pnlCurrConv.Visible = False
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
        Me.txtConversionRate.Location = New System.Drawing.Point(290, 8)
        Me.txtConversionRate.MendatroryField = False
        Me.txtConversionRate.MyLinkLable1 = Nothing
        Me.txtConversionRate.MyLinkLable2 = Nothing
        Me.txtConversionRate.Name = "txtConversionRate"
        Me.txtConversionRate.ReferenceFieldDesc = Nothing
        Me.txtConversionRate.ReferenceFieldName = Nothing
        Me.txtConversionRate.ReferenceTableName = Nothing
        Me.txtConversionRate.Size = New System.Drawing.Size(124, 20)
        Me.txtConversionRate.TabIndex = 1
        Me.txtConversionRate.Text = "1"
        Me.txtConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtConversionRate.Value = 1.0R
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCurrencyCode.CalculationExpression = Nothing
        Me.txtCurrencyCode.Enabled = False
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(59, 10)
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
        Me.txtCurrencyCode.Size = New System.Drawing.Size(136, 22)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(420, 12)
        Me.lblEffectiveFrom.Name = "lblEffectiveFrom"
        Me.lblEffectiveFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblEffectiveFrom.TabIndex = 141
        Me.lblEffectiveFrom.Text = "Applicable From"
        '
        'txtApplicableFrom
        '
        Me.txtApplicableFrom.AutoSize = False
        Me.txtApplicableFrom.BorderVisible = True
        Me.txtApplicableFrom.FieldName = Nothing
        Me.txtApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicableFrom.Location = New System.Drawing.Point(516, 10)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(119, 18)
        Me.txtApplicableFrom.TabIndex = 2
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(4, 12)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 137
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(193, 12)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 139
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(66, 96)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(80, 16)
        Me.RadLabel32.TabIndex = 135
        Me.RadLabel32.Text = "Other Charges"
        '
        'lblTotalOtherCharges
        '
        Me.lblTotalOtherCharges.AutoSize = False
        Me.lblTotalOtherCharges.BorderVisible = True
        Me.lblTotalOtherCharges.FieldName = Nothing
        Me.lblTotalOtherCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalOtherCharges.Location = New System.Drawing.Point(156, 94)
        Me.lblTotalOtherCharges.Name = "lblTotalOtherCharges"
        Me.lblTotalOtherCharges.Size = New System.Drawing.Size(165, 18)
        Me.lblTotalOtherCharges.TabIndex = 11
        Me.lblTotalOtherCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(429, 68)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(355, 164)
        Me.txtComment.TabIndex = 2
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(369, 72)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel14.TabIndex = 127
        Me.RadLabel14.Text = "Comment"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(547, 378)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 120
        Me.RadLabel9.Text = "Amount After Discount"
        Me.RadLabel9.Visible = False
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(46, 165)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 123
        Me.RadLabel27.Text = "Document Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(156, 164)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(165, 18)
        Me.lblTotRAmt.TabIndex = 12
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(79, 117)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel25.TabIndex = 122
        Me.RadLabel25.Text = "Tax Amount"
        '
        'lblTotalTaxAmt
        '
        Me.lblTotalTaxAmt.AutoSize = False
        Me.lblTotalTaxAmt.BorderVisible = True
        Me.lblTotalTaxAmt.FieldName = Nothing
        Me.lblTotalTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTaxAmt.Location = New System.Drawing.Point(156, 117)
        Me.lblTotalTaxAmt.Name = "lblTotalTaxAmt"
        Me.lblTotalTaxAmt.Size = New System.Drawing.Size(165, 18)
        Me.lblTotalTaxAmt.TabIndex = 10
        Me.lblTotalTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(674, 378)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 9
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmtAfterDiscount.Visible = False
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(674, 348)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 8
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDiscountAmt.Visible = False
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(674, 247)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 1
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmtWithDiscount.Visible = False
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(567, 348)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 121
        Me.RadLabel22.Text = "- Discount Amount"
        Me.RadLabel22.Visible = False
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(483, 247)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 119
        Me.RadLabel19.Text = "Document Amount without Discount"
        Me.RadLabel19.Visible = False
        '
        'BtnPrintChallan
        '
        Me.BtnPrintChallan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintChallan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrintChallan.Location = New System.Drawing.Point(290, 3)
        Me.BtnPrintChallan.Name = "BtnPrintChallan"
        Me.BtnPrintChallan.Size = New System.Drawing.Size(88, 22)
        Me.BtnPrintChallan.TabIndex = 9
        Me.BtnPrintChallan.Text = "Print Challan"
        '
        'btnPrintMandi
        '
        Me.btnPrintMandi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintMandi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintMandi.Location = New System.Drawing.Point(208, 3)
        Me.btnPrintMandi.Name = "btnPrintMandi"
        Me.btnPrintMandi.Size = New System.Drawing.Size(76, 22)
        Me.btnPrintMandi.TabIndex = 4
        Me.btnPrintMandi.Text = "Print"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(447, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(106, 22)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Document Cancel"
        Me.btnCancel.Visible = False
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Location = New System.Drawing.Point(600, 3)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(104, 22)
        Me.RadSplitButton1.TabIndex = 44
        Me.RadSplitButton1.Text = "Print Excisable"
        Me.RadSplitButton1.Visible = False
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.Location = New System.Drawing.Point(386, 2)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(59, 22)
        Me.btnHistory.TabIndex = 5
        Me.btnHistory.Text = "History"
        Me.btnHistory.Visible = False
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnpreview, Me.btnsend, Me.btnSendForApproval})
        Me.btnsetting.Location = New System.Drawing.Point(950, 2)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(87, 22)
        Me.btnsetting.TabIndex = 7
        Me.btnsetting.Text = "E-Mail/SMS"
        Me.btnsetting.Visible = False
        '
        'btnpreview
        '
        Me.btnpreview.AccessibleDescription = "Preview"
        Me.btnpreview.AccessibleName = "Preview"
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Text = "Preview"
        '
        'btnsend
        '
        Me.btnsend.AccessibleDescription = "Send E-Mail/SMS"
        Me.btnsend.AccessibleName = "Send E-Mail/SMS"
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Text = "Send E-Mail/SMS"
        '
        'btnSendForApproval
        '
        Me.btnSendForApproval.AccessibleDescription = "Send For Approval"
        Me.btnSendForApproval.AccessibleName = "Send For Approval"
        Me.btnSendForApproval.Name = "btnSendForApproval"
        Me.btnSendForApproval.Text = "Send For Approval"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(710, 5)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(59, 22)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'chkRateUserCustomer
        '
        Me.chkRateUserCustomer.Enabled = False
        Me.chkRateUserCustomer.IsThreeState = True
        Me.chkRateUserCustomer.Location = New System.Drawing.Point(508, 4)
        Me.chkRateUserCustomer.Name = "chkRateUserCustomer"
        Me.chkRateUserCustomer.Size = New System.Drawing.Size(158, 18)
        Me.chkRateUserCustomer.TabIndex = 6
        Me.chkRateUserCustomer.Text = "User Customer Rate Setting"
        Me.chkRateUserCustomer.Visible = False
        '
        'chkRateDefaultSetting
        '
        Me.chkRateDefaultSetting.Enabled = False
        Me.chkRateDefaultSetting.IsThreeState = True
        Me.chkRateDefaultSetting.Location = New System.Drawing.Point(349, 4)
        Me.chkRateDefaultSetting.Name = "chkRateDefaultSetting"
        Me.chkRateDefaultSetting.Size = New System.Drawing.Size(120, 18)
        Me.chkRateDefaultSetting.TabIndex = 5
        Me.chkRateDefaultSetting.Text = "Rate Default Setting"
        Me.chkRateDefaultSetting.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(817, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(59, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnAmendment
        '
        Me.btnAmendment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAmendment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAmendment.Location = New System.Drawing.Point(878, 2)
        Me.btnAmendment.Name = "btnAmendment"
        Me.btnAmendment.Size = New System.Drawing.Size(69, 22)
        Me.btnAmendment.TabIndex = 6
        Me.btnAmendment.Text = "Amendment"
        Me.btnAmendment.Visible = False
        '
        'lblAmbendmentNoCaption
        '
        Me.lblAmbendmentNoCaption.FieldName = Nothing
        Me.lblAmbendmentNoCaption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmbendmentNoCaption.Location = New System.Drawing.Point(794, 5)
        Me.lblAmbendmentNoCaption.Name = "lblAmbendmentNoCaption"
        Me.lblAmbendmentNoCaption.Size = New System.Drawing.Size(85, 16)
        Me.lblAmbendmentNoCaption.TabIndex = 8
        Me.lblAmbendmentNoCaption.Text = "Amendment No"
        Me.lblAmbendmentNoCaption.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(142, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(63, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(73, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(65, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1039, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(57, 22)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RadMenu1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1100, 25)
        Me.Panel2.TabIndex = 5
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1100, 20)
        Me.RadMenu1.TabIndex = 0
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem4})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
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
        Me.RadMenuItem4.AccessibleDescription = "RadMenuItem4"
        Me.RadMenuItem4.AccessibleName = "RadMenuItem4"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'frmCSATransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 548)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmCSATransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "CSA Transfer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtElectronicRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEWayBillDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEwaybillnoupdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemovalDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemovalDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSecondary_Doc_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTransportorMName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEXType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkownvehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtship_to_loc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chk_F_Form, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvehicle_Charge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ttxway_bill_date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGR_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGross_Wt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotal_Wt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicle_Capacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtvehicle_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporter_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnupdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWayBill_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCSARate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStateDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocumentTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCustDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTermName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCommissionCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceDiscAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.chkDiscountOnAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDiscountOnRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiscPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalOtherCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPrintChallan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintMandi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRateUserCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRateDefaultSetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAmendment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmbendmentNoCaption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtCustCode As common.UserControls.txtFinder
    Friend WithEvents txtToLocation As common.UserControls.txtFinder
    Friend WithEvents txtFromLocation As common.UserControls.txtFinder
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents txtDept As common.UserControls.txtFinder
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents btnAmendment As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents gvAC As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCustDesc As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTotalTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents lblTermName As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents txtToLocationDesc As common.Controls.MyLabel
    Friend WithEvents txtFromLocationDesc As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents lblAmbendmentNoCaption As common.Controls.MyLabel
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblTotalOtherCharges As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkRateUserCustomer As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRateDefaultSetting As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkDiscountOnAmt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDiscountOnRate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtDiscAmt As common.MyNumBox
    Friend WithEvents txtDiscPer As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblInvoiceDiscAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtDocumentTotal As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnpreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSendForApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtStateDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtState As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents fndDONo As common.UserControls.txtFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtCSARate As common.Controls.MyTextBox
    Friend WithEvents cmbTax As common.Controls.MyComboBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblTotalAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblCommissionCharges As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtWayBill_No As common.Controls.MyTextBox
    Friend WithEvents btnupdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtTransporter_desc As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtTransporter_Code As common.UserControls.txtFinder
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents chk_F_Form As common.Controls.MyCheckBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtvehicle_code As common.Controls.MyTextBox
    Friend WithEvents txtVehicle_Capacity As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txttotal_Wt As common.Controls.MyLabel
    Friend WithEvents txtGross_Wt As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtGR_No As common.Controls.MyTextBox
    Friend WithEvents dtpGR As common.Controls.MyDateTimePicker
    Friend WithEvents ttxway_bill_date As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtvehicle_Charge As common.Controls.MyLabel
    Friend WithEvents txtExcisable As common.Controls.MyLabel
    Friend WithEvents txtship_to_loc_name As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents txtship_to_loc_code As common.UserControls.txtFinder
    Friend WithEvents chkownvehicle As common.Controls.MyCheckBox
    Friend WithEvents BtnPrintChallan As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbEXType As common.Controls.MyComboBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents TxtTransportorMName As common.Controls.MyTextBox
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents txtSecondary_Doc_Code As common.Controls.MyLabel
    Friend WithEvents txtRemovalDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblRemovalDate As common.Controls.MyLabel
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtEWayBillDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents btnEwaybillnoupdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents TxtEWayBillNo As common.Controls.MyTextBox
    Friend WithEvents txtElectronicRefNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel27 As common.Controls.MyLabel
    Friend WithEvents btnPrintMandi As Telerik.WinControls.UI.RadButton
End Class

