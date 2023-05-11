<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEXSalesOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEXSalesOrder))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkIsTaxable = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.cboItemType = New common.Controls.MyComboBox()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.cboModeOfTransport = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.cmbDocType = New common.Controls.MyComboBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtOthr_Instructn = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtComm_Pay_name = New common.Controls.MyLabel()
        Me.cmbComm_Payable = New common.Controls.MyComboBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtAmt_comm = New common.MyNumBox()
        Me.fndComm_Pay_Code = New common.UserControls.txtFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.cmbComm_Amount = New common.Controls.MyComboBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtAdvance_Pers = New common.MyNumBox()
        Me.dtpPODate = New common.Controls.MyDateTimePicker()
        Me.txtDueDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel17 = New common.Controls.MyLabel()
        Me.cmbTerms_Payment = New common.Controls.MyComboBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtCRTermCode = New common.UserControls.txtFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.lblCRTermName = New common.Controls.MyLabel()
        Me.cmbTerms = New common.Controls.MyComboBox()
        Me.RadLabel16 = New common.Controls.MyLabel()
        Me.txtDate = New System.Windows.Forms.DateTimePicker()
        Me.chkclose = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtPONo = New common.Controls.MyTextBox()
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.lblSalesman = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtSalesman = New common.UserControls.txtFinder()
        Me.RadLabel23 = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.lblShipToLocation = New common.Controls.MyLabel()
        Me.lblBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel20 = New common.Controls.MyLabel()
        Me.txtDept = New common.UserControls.txtFinder()
        Me.lblDept = New common.Controls.MyLabel()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtReqNo = New common.UserControls.txtFinder()
        Me.txtVendorNo = New common.UserControls.txtFinder()
        Me.txtShipToLocation = New common.UserControls.txtFinder()
        Me.txtBillToLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtDeliveryDate = New common.Controls.MyDateTimePicker()
        Me.txtRefNo = New common.Controls.MyTextBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv_Notify_Party = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblAbandonmentNo = New common.Controls.MyLabel()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.cboPOType = New common.Controls.MyComboBox()
        Me.lblPriceCode = New common.Controls.MyLabel()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.txtPriceCode = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtPriceGroupCode = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.lblProject = New common.Controls.MyLabel()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
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
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnpreview = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnsend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendForApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.txtOthr_Instructn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtComm_Pay_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbComm_Payable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmt_comm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbComm_Amount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAdvance_Pers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPODate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTerms_Payment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCRTermName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTerms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gv_Notify_Party, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_Notify_Party.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
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
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.Attachments)
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
        Me.RadPageViewPage1.Controls.Add(Me.chkIsTaxable)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.cboItemType)
        Me.RadPageViewPage1.Controls.Add(Me.cboModeOfTransport)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.cmbDocType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.txtAdvance_Pers)
        Me.RadPageViewPage1.Controls.Add(Me.dtpPODate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDueDate)
        Me.RadPageViewPage1.Controls.Add(Me.cmbTerms_Payment)
        Me.RadPageViewPage1.Controls.Add(Me.txtCRTermCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage1.Controls.Add(Me.cmbTerms)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel17)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel16)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblCRTermName)
        Me.RadPageViewPage1.Controls.Add(Me.chkclose)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtPONo)
        Me.RadPageViewPage1.Controls.Add(Me.btnDrillDown)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.lblSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtSalesman)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel23)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.lblShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.Controls.Add(Me.txtReqNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtBillToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDeliveryDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtRefNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(77.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1079, 445)
        Me.RadPageViewPage1.Text = "Sales Order"
        '
        'chkIsTaxable
        '
        Me.chkIsTaxable.Location = New System.Drawing.Point(820, 214)
        Me.chkIsTaxable.Name = "chkIsTaxable"
        Me.chkIsTaxable.Size = New System.Drawing.Size(69, 18)
        Me.chkIsTaxable.TabIndex = 143
        Me.chkIsTaxable.Text = "Is Taxable"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(483, 218)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(330, 16)
        Me.MyLabel18.TabIndex = 138
        Me.MyLabel18.Text = "Double click for selecting Pre-Carriage By on Pre-Carriage label."
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
        Me.cboItemType.Location = New System.Drawing.Point(748, 105)
        Me.cboItemType.MendatroryField = True
        Me.cboItemType.MyLinkLable1 = Me.RadLabel21
        Me.cboItemType.MyLinkLable2 = Nothing
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.ReferenceFieldDesc = Nothing
        Me.cboItemType.ReferenceFieldName = Nothing
        Me.cboItemType.ReferenceTableName = Nothing
        Me.cboItemType.Size = New System.Drawing.Size(180, 20)
        Me.cboItemType.TabIndex = 15
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(685, 107)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(57, 16)
        Me.RadLabel21.TabIndex = 20
        Me.RadLabel21.Text = "Item Type"
        '
        'cboModeOfTransport
        '
        Me.cboModeOfTransport.AutoSize = False
        Me.cboModeOfTransport.BorderVisible = True
        Me.cboModeOfTransport.FieldName = Nothing
        Me.cboModeOfTransport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboModeOfTransport.Location = New System.Drawing.Point(600, 63)
        Me.cboModeOfTransport.Name = "cboModeOfTransport"
        Me.cboModeOfTransport.Size = New System.Drawing.Size(148, 20)
        Me.cboModeOfTransport.TabIndex = 11
        Me.cboModeOfTransport.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cboModeOfTransport.TextWrap = False
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(241, 125)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel9.TabIndex = 137
        Me.MyLabel9.Text = "Order Type"
        '
        'cmbDocType
        '
        Me.cmbDocType.AutoCompleteDisplayMember = Nothing
        Me.cmbDocType.AutoCompleteValueMember = Nothing
        Me.cmbDocType.CalculationExpression = Nothing
        Me.cmbDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbDocType.FieldCode = Nothing
        Me.cmbDocType.FieldDesc = Nothing
        Me.cmbDocType.FieldMaxLength = 0
        Me.cmbDocType.FieldName = Nothing
        Me.cmbDocType.isCalculatedField = False
        Me.cmbDocType.IsSourceFromTable = False
        Me.cmbDocType.IsSourceFromValueList = False
        Me.cmbDocType.IsUnique = False
        Me.cmbDocType.Location = New System.Drawing.Point(310, 123)
        Me.cmbDocType.MendatroryField = True
        Me.cmbDocType.MyLinkLable1 = Me.MyLabel9
        Me.cmbDocType.MyLinkLable2 = Nothing
        Me.cmbDocType.Name = "cmbDocType"
        Me.cmbDocType.ReferenceFieldDesc = Nothing
        Me.cmbDocType.ReferenceFieldName = Nothing
        Me.cmbDocType.ReferenceTableName = Nothing
        Me.cmbDocType.Size = New System.Drawing.Size(168, 20)
        Me.cmbDocType.TabIndex = 7
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Location = New System.Drawing.Point(913, 150)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(15, 18)
        Me.MyLabel16.TabIndex = 18
        Me.MyLabel16.Text = "%"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.txtOthr_Instructn)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox5.Controls.Add(Me.txtComm_Pay_name)
        Me.RadGroupBox5.Controls.Add(Me.cmbComm_Payable)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox5.Controls.Add(Me.txtAmt_comm)
        Me.RadGroupBox5.Controls.Add(Me.fndComm_Pay_Code)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox5.Controls.Add(Me.cmbComm_Amount)
        Me.RadGroupBox5.HeaderText = "Commission"
        Me.RadGroupBox5.Location = New System.Drawing.Point(-2, 143)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Size = New System.Drawing.Size(481, 92)
        Me.RadGroupBox5.TabIndex = 1
        Me.RadGroupBox5.Text = "Commission"
        '
        'txtOthr_Instructn
        '
        Me.txtOthr_Instructn.CalculationExpression = Nothing
        Me.txtOthr_Instructn.Enabled = False
        Me.txtOthr_Instructn.FieldCode = Nothing
        Me.txtOthr_Instructn.FieldDesc = Nothing
        Me.txtOthr_Instructn.FieldMaxLength = 0
        Me.txtOthr_Instructn.FieldName = Nothing
        Me.txtOthr_Instructn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthr_Instructn.isCalculatedField = False
        Me.txtOthr_Instructn.IsSourceFromTable = False
        Me.txtOthr_Instructn.IsSourceFromValueList = False
        Me.txtOthr_Instructn.IsUnique = False
        Me.txtOthr_Instructn.Location = New System.Drawing.Point(96, 70)
        Me.txtOthr_Instructn.MaxLength = 1000
        Me.txtOthr_Instructn.MendatroryField = False
        Me.txtOthr_Instructn.MyLinkLable1 = Me.MyLabel15
        Me.txtOthr_Instructn.MyLinkLable2 = Nothing
        Me.txtOthr_Instructn.Name = "txtOthr_Instructn"
        Me.txtOthr_Instructn.ReferenceFieldDesc = Nothing
        Me.txtOthr_Instructn.ReferenceFieldName = Nothing
        Me.txtOthr_Instructn.ReferenceTableName = Nothing
        Me.txtOthr_Instructn.Size = New System.Drawing.Size(383, 18)
        Me.txtOthr_Instructn.TabIndex = 4
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(1, 70)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel15.TabIndex = 24
        Me.MyLabel15.Text = "Other Instructions"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(1, 17)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel12.TabIndex = 16
        Me.MyLabel12.Text = "Commission Paid"
        '
        'txtComm_Pay_name
        '
        Me.txtComm_Pay_name.AutoSize = False
        Me.txtComm_Pay_name.BorderVisible = True
        Me.txtComm_Pay_name.FieldName = Nothing
        Me.txtComm_Pay_name.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComm_Pay_name.Location = New System.Drawing.Point(240, 37)
        Me.txtComm_Pay_name.Name = "txtComm_Pay_name"
        Me.txtComm_Pay_name.Size = New System.Drawing.Size(239, 19)
        Me.txtComm_Pay_name.TabIndex = 29
        Me.txtComm_Pay_name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtComm_Pay_name.TextWrap = False
        '
        'cmbComm_Payable
        '
        Me.cmbComm_Payable.AutoCompleteDisplayMember = Nothing
        Me.cmbComm_Payable.AutoCompleteValueMember = Nothing
        Me.cmbComm_Payable.CalculationExpression = Nothing
        Me.cmbComm_Payable.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbComm_Payable.FieldCode = Nothing
        Me.cmbComm_Payable.FieldDesc = Nothing
        Me.cmbComm_Payable.FieldMaxLength = 0
        Me.cmbComm_Payable.FieldName = Nothing
        Me.cmbComm_Payable.isCalculatedField = False
        Me.cmbComm_Payable.IsSourceFromTable = False
        Me.cmbComm_Payable.IsSourceFromValueList = False
        Me.cmbComm_Payable.IsUnique = False
        Me.cmbComm_Payable.Location = New System.Drawing.Point(96, 15)
        Me.cmbComm_Payable.MendatroryField = False
        Me.cmbComm_Payable.MyLinkLable1 = Me.MyLabel12
        Me.cmbComm_Payable.MyLinkLable2 = Nothing
        Me.cmbComm_Payable.Name = "cmbComm_Payable"
        Me.cmbComm_Payable.ReferenceFieldDesc = Nothing
        Me.cmbComm_Payable.ReferenceFieldName = Nothing
        Me.cmbComm_Payable.ReferenceTableName = Nothing
        Me.cmbComm_Payable.Size = New System.Drawing.Size(67, 20)
        Me.cmbComm_Payable.TabIndex = 0
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(163, 17)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel14.TabIndex = 22
        Me.MyLabel14.Text = "Commission Amount"
        '
        'txtAmt_comm
        '
        Me.txtAmt_comm.CalculationExpression = Nothing
        Me.txtAmt_comm.DecimalPlaces = 2
        Me.txtAmt_comm.Enabled = False
        Me.txtAmt_comm.FieldCode = Nothing
        Me.txtAmt_comm.FieldDesc = Nothing
        Me.txtAmt_comm.FieldMaxLength = 0
        Me.txtAmt_comm.FieldName = Nothing
        Me.txtAmt_comm.isCalculatedField = False
        Me.txtAmt_comm.IsSourceFromTable = False
        Me.txtAmt_comm.IsSourceFromValueList = False
        Me.txtAmt_comm.IsUnique = False
        Me.txtAmt_comm.Location = New System.Drawing.Point(277, 15)
        Me.txtAmt_comm.MendatroryField = False
        Me.txtAmt_comm.MyLinkLable1 = Me.MyLabel14
        Me.txtAmt_comm.MyLinkLable2 = Nothing
        Me.txtAmt_comm.Name = "txtAmt_comm"
        Me.txtAmt_comm.ReferenceFieldDesc = Nothing
        Me.txtAmt_comm.ReferenceFieldName = Nothing
        Me.txtAmt_comm.ReferenceTableName = Nothing
        Me.txtAmt_comm.Size = New System.Drawing.Size(111, 20)
        Me.txtAmt_comm.TabIndex = 1
        Me.txtAmt_comm.Text = "0"
        Me.txtAmt_comm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAmt_comm.Value = 0.0R
        '
        'fndComm_Pay_Code
        '
        Me.fndComm_Pay_Code.CalculationExpression = Nothing
        Me.fndComm_Pay_Code.FieldCode = Nothing
        Me.fndComm_Pay_Code.FieldDesc = Nothing
        Me.fndComm_Pay_Code.FieldMaxLength = 0
        Me.fndComm_Pay_Code.FieldName = Nothing
        Me.fndComm_Pay_Code.isCalculatedField = False
        Me.fndComm_Pay_Code.IsSourceFromTable = False
        Me.fndComm_Pay_Code.IsSourceFromValueList = False
        Me.fndComm_Pay_Code.IsUnique = False
        Me.fndComm_Pay_Code.Location = New System.Drawing.Point(96, 37)
        Me.fndComm_Pay_Code.MendatroryField = False
        Me.fndComm_Pay_Code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndComm_Pay_Code.MyLinkLable1 = Me.MyLabel13
        Me.fndComm_Pay_Code.MyLinkLable2 = Me.txtComm_Pay_name
        Me.fndComm_Pay_Code.MyReadOnly = False
        Me.fndComm_Pay_Code.MyShowMasterFormButton = False
        Me.fndComm_Pay_Code.Name = "fndComm_Pay_Code"
        Me.fndComm_Pay_Code.ReferenceFieldDesc = Nothing
        Me.fndComm_Pay_Code.ReferenceFieldName = Nothing
        Me.fndComm_Pay_Code.ReferenceTableName = Nothing
        Me.fndComm_Pay_Code.Size = New System.Drawing.Size(143, 19)
        Me.fndComm_Pay_Code.TabIndex = 3
        Me.fndComm_Pay_Code.Value = ""
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(1, 37)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(70, 27)
        Me.MyLabel13.TabIndex = 18
        Me.MyLabel13.Text = "<html><p> Commission </p><p> Payable To</p></html>"
        '
        'cmbComm_Amount
        '
        Me.cmbComm_Amount.AutoCompleteDisplayMember = Nothing
        Me.cmbComm_Amount.AutoCompleteValueMember = Nothing
        Me.cmbComm_Amount.CalculationExpression = Nothing
        Me.cmbComm_Amount.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbComm_Amount.Enabled = False
        Me.cmbComm_Amount.FieldCode = Nothing
        Me.cmbComm_Amount.FieldDesc = Nothing
        Me.cmbComm_Amount.FieldMaxLength = 0
        Me.cmbComm_Amount.FieldName = Nothing
        Me.cmbComm_Amount.isCalculatedField = False
        Me.cmbComm_Amount.IsSourceFromTable = False
        Me.cmbComm_Amount.IsSourceFromValueList = False
        Me.cmbComm_Amount.IsUnique = False
        Me.cmbComm_Amount.Location = New System.Drawing.Point(391, 15)
        Me.cmbComm_Amount.MendatroryField = False
        Me.cmbComm_Amount.MyLinkLable1 = Me.MyLabel14
        Me.cmbComm_Amount.MyLinkLable2 = Nothing
        Me.cmbComm_Amount.Name = "cmbComm_Amount"
        Me.cmbComm_Amount.ReferenceFieldDesc = Nothing
        Me.cmbComm_Amount.ReferenceFieldName = Nothing
        Me.cmbComm_Amount.ReferenceTableName = Nothing
        Me.cmbComm_Amount.Size = New System.Drawing.Size(88, 20)
        Me.cmbComm_Amount.TabIndex = 2
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(764, 44)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel17.TabIndex = 135
        Me.MyLabel17.Text = "Buyer PO Date"
        '
        'txtAdvance_Pers
        '
        Me.txtAdvance_Pers.CalculationExpression = Nothing
        Me.txtAdvance_Pers.DecimalPlaces = 0
        Me.txtAdvance_Pers.FieldCode = Nothing
        Me.txtAdvance_Pers.FieldDesc = Nothing
        Me.txtAdvance_Pers.FieldMaxLength = 0
        Me.txtAdvance_Pers.FieldName = Nothing
        Me.txtAdvance_Pers.isCalculatedField = False
        Me.txtAdvance_Pers.IsSourceFromTable = False
        Me.txtAdvance_Pers.IsSourceFromValueList = False
        Me.txtAdvance_Pers.IsUnique = False
        Me.txtAdvance_Pers.Location = New System.Drawing.Point(882, 149)
        Me.txtAdvance_Pers.MaxLength = 3
        Me.txtAdvance_Pers.MendatroryField = True
        Me.txtAdvance_Pers.MyLinkLable1 = Nothing
        Me.txtAdvance_Pers.MyLinkLable2 = Nothing
        Me.txtAdvance_Pers.Name = "txtAdvance_Pers"
        Me.txtAdvance_Pers.ReferenceFieldDesc = Nothing
        Me.txtAdvance_Pers.ReferenceFieldName = Nothing
        Me.txtAdvance_Pers.ReferenceTableName = Nothing
        Me.txtAdvance_Pers.Size = New System.Drawing.Size(31, 20)
        Me.txtAdvance_Pers.TabIndex = 16
        Me.txtAdvance_Pers.Text = "0"
        Me.txtAdvance_Pers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAdvance_Pers.Value = 0.0R
        '
        'dtpPODate
        '
        Me.dtpPODate.CalculationExpression = Nothing
        Me.dtpPODate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPODate.FieldCode = Nothing
        Me.dtpPODate.FieldDesc = Nothing
        Me.dtpPODate.FieldMaxLength = 0
        Me.dtpPODate.FieldName = Nothing
        Me.dtpPODate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPODate.isCalculatedField = False
        Me.dtpPODate.IsSourceFromTable = False
        Me.dtpPODate.IsSourceFromValueList = False
        Me.dtpPODate.IsUnique = False
        Me.dtpPODate.Location = New System.Drawing.Point(851, 43)
        Me.dtpPODate.MendatroryField = False
        Me.dtpPODate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPODate.MyLinkLable1 = Me.MyLabel17
        Me.dtpPODate.MyLinkLable2 = Nothing
        Me.dtpPODate.Name = "dtpPODate"
        Me.dtpPODate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPODate.ReferenceFieldDesc = Nothing
        Me.dtpPODate.ReferenceFieldName = Nothing
        Me.dtpPODate.ReferenceTableName = Nothing
        Me.dtpPODate.Size = New System.Drawing.Size(77, 18)
        Me.dtpPODate.TabIndex = 10
        Me.dtpPODate.TabStop = False
        Me.dtpPODate.Text = "13/06/2011"
        Me.dtpPODate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtDueDate.Location = New System.Drawing.Point(600, 106)
        Me.txtDueDate.MendatroryField = False
        Me.txtDueDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.MyLinkLable1 = Me.RadLabel17
        Me.txtDueDate.MyLinkLable2 = Nothing
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDueDate.ReferenceFieldDesc = Nothing
        Me.txtDueDate.ReferenceFieldName = Nothing
        Me.txtDueDate.ReferenceTableName = Nothing
        Me.txtDueDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDueDate.TabIndex = 14
        Me.txtDueDate.TabStop = False
        Me.txtDueDate.Text = "13-06-2011"
        Me.txtDueDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel17
        '
        Me.RadLabel17.FieldName = Nothing
        Me.RadLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel17.Location = New System.Drawing.Point(482, 107)
        Me.RadLabel17.Name = "RadLabel17"
        Me.RadLabel17.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel17.TabIndex = 21
        Me.RadLabel17.Text = "Due Date"
        '
        'cmbTerms_Payment
        '
        Me.cmbTerms_Payment.AutoCompleteDisplayMember = Nothing
        Me.cmbTerms_Payment.AutoCompleteValueMember = Nothing
        Me.cmbTerms_Payment.CalculationExpression = Nothing
        Me.cmbTerms_Payment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbTerms_Payment.FieldCode = Nothing
        Me.cmbTerms_Payment.FieldDesc = Nothing
        Me.cmbTerms_Payment.FieldMaxLength = 0
        Me.cmbTerms_Payment.FieldName = Nothing
        Me.cmbTerms_Payment.isCalculatedField = False
        Me.cmbTerms_Payment.IsSourceFromTable = False
        Me.cmbTerms_Payment.IsSourceFromValueList = False
        Me.cmbTerms_Payment.IsUnique = False
        Me.cmbTerms_Payment.Location = New System.Drawing.Point(600, 149)
        Me.cmbTerms_Payment.MendatroryField = False
        Me.cmbTerms_Payment.MyLinkLable1 = Me.MyLabel11
        Me.cmbTerms_Payment.MyLinkLable2 = Nothing
        Me.cmbTerms_Payment.Name = "cmbTerms_Payment"
        Me.cmbTerms_Payment.ReferenceFieldDesc = Nothing
        Me.cmbTerms_Payment.ReferenceFieldName = Nothing
        Me.cmbTerms_Payment.ReferenceTableName = Nothing
        Me.cmbTerms_Payment.Size = New System.Drawing.Size(282, 20)
        Me.cmbTerms_Payment.TabIndex = 17
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(482, 151)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel11.TabIndex = 16
        Me.MyLabel11.Text = "Terms of Payment"
        '
        'txtCRTermCode
        '
        Me.txtCRTermCode.CalculationExpression = Nothing
        Me.txtCRTermCode.FieldCode = Nothing
        Me.txtCRTermCode.FieldDesc = Nothing
        Me.txtCRTermCode.FieldMaxLength = 0
        Me.txtCRTermCode.FieldName = Nothing
        Me.txtCRTermCode.isCalculatedField = False
        Me.txtCRTermCode.IsSourceFromTable = False
        Me.txtCRTermCode.IsSourceFromValueList = False
        Me.txtCRTermCode.IsUnique = False
        Me.txtCRTermCode.Location = New System.Drawing.Point(600, 85)
        Me.txtCRTermCode.MendatroryField = False
        Me.txtCRTermCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCRTermCode.MyLinkLable1 = Me.MyLabel10
        Me.txtCRTermCode.MyLinkLable2 = Me.lblCRTermName
        Me.txtCRTermCode.MyReadOnly = False
        Me.txtCRTermCode.MyShowMasterFormButton = False
        Me.txtCRTermCode.Name = "txtCRTermCode"
        Me.txtCRTermCode.ReferenceFieldDesc = Nothing
        Me.txtCRTermCode.ReferenceFieldName = Nothing
        Me.txtCRTermCode.ReferenceTableName = Nothing
        Me.txtCRTermCode.Size = New System.Drawing.Size(143, 19)
        Me.txtCRTermCode.TabIndex = 13
        Me.txtCRTermCode.Value = ""
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(482, 86)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel10.TabIndex = 22
        Me.MyLabel10.Text = "Credit Terms"
        '
        'lblCRTermName
        '
        Me.lblCRTermName.AutoSize = False
        Me.lblCRTermName.BorderVisible = True
        Me.lblCRTermName.FieldName = Nothing
        Me.lblCRTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRTermName.Location = New System.Drawing.Point(745, 84)
        Me.lblCRTermName.Name = "lblCRTermName"
        Me.lblCRTermName.Size = New System.Drawing.Size(183, 20)
        Me.lblCRTermName.TabIndex = 19
        Me.lblCRTermName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCRTermName.TextWrap = False
        '
        'cmbTerms
        '
        Me.cmbTerms.AutoCompleteDisplayMember = Nothing
        Me.cmbTerms.AutoCompleteValueMember = Nothing
        Me.cmbTerms.CalculationExpression = Nothing
        Me.cmbTerms.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbTerms.FieldCode = Nothing
        Me.cmbTerms.FieldDesc = Nothing
        Me.cmbTerms.FieldMaxLength = 0
        Me.cmbTerms.FieldName = Nothing
        Me.cmbTerms.isCalculatedField = False
        Me.cmbTerms.IsSourceFromTable = False
        Me.cmbTerms.IsSourceFromValueList = False
        Me.cmbTerms.IsUnique = False
        Me.cmbTerms.Location = New System.Drawing.Point(600, 127)
        Me.cmbTerms.MendatroryField = False
        Me.cmbTerms.MyLinkLable1 = Me.RadLabel16
        Me.cmbTerms.MyLinkLable2 = Nothing
        Me.cmbTerms.Name = "cmbTerms"
        Me.cmbTerms.ReferenceFieldDesc = Nothing
        Me.cmbTerms.ReferenceFieldName = Nothing
        Me.cmbTerms.ReferenceTableName = Nothing
        Me.cmbTerms.Size = New System.Drawing.Size(328, 20)
        Me.cmbTerms.TabIndex = 16
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(482, 128)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel16.TabIndex = 13
        Me.RadLabel16.Text = "Term Code"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(399, 3)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(79, 20)
        Me.txtDate.TabIndex = 1
        '
        'chkclose
        '
        Me.chkclose.Location = New System.Drawing.Point(820, 191)
        Me.chkclose.Name = "chkclose"
        Me.chkclose.Size = New System.Drawing.Size(108, 18)
        Me.chkclose.TabIndex = 128
        Me.chkclose.Text = "Close Sales Order"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(482, 44)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel8.TabIndex = 15
        Me.MyLabel8.Text = "Buyer PO No"
        '
        'txtPONo
        '
        Me.txtPONo.CalculationExpression = Nothing
        Me.txtPONo.FieldCode = Nothing
        Me.txtPONo.FieldDesc = Nothing
        Me.txtPONo.FieldMaxLength = 0
        Me.txtPONo.FieldName = Nothing
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.isCalculatedField = False
        Me.txtPONo.IsSourceFromTable = False
        Me.txtPONo.IsSourceFromValueList = False
        Me.txtPONo.IsUnique = False
        Me.txtPONo.Location = New System.Drawing.Point(600, 43)
        Me.txtPONo.MaxLength = 200
        Me.txtPONo.MendatroryField = False
        Me.txtPONo.MyLinkLable1 = Me.MyLabel8
        Me.txtPONo.MyLinkLable2 = Nothing
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(148, 18)
        Me.txtPONo.TabIndex = 9
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDrillDown.Location = New System.Drawing.Point(761, 23)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(20, 18)
        Me.btnDrillDown.TabIndex = 4
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(482, 191)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel5.TabIndex = 126
        Me.MyLabel5.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(600, 191)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(214, 18)
        Me.lblTotRAmt1.TabIndex = 20
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
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
        'lblSalesman
        '
        Me.lblSalesman.AutoSize = False
        Me.lblSalesman.BorderVisible = True
        Me.lblSalesman.FieldName = Nothing
        Me.lblSalesman.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesman.Location = New System.Drawing.Point(240, 104)
        Me.lblSalesman.Name = "lblSalesman"
        Me.lblSalesman.Size = New System.Drawing.Size(238, 18)
        Me.lblSalesman.TabIndex = 17
        Me.lblSalesman.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSalesman.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(0, 104)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel2.TabIndex = 31
        Me.MyLabel2.Text = "Salesman"
        '
        'txtSalesman
        '
        Me.txtSalesman.CalculationExpression = Nothing
        Me.txtSalesman.FieldCode = Nothing
        Me.txtSalesman.FieldDesc = Nothing
        Me.txtSalesman.FieldMaxLength = 0
        Me.txtSalesman.FieldName = Nothing
        Me.txtSalesman.isCalculatedField = False
        Me.txtSalesman.IsSourceFromTable = False
        Me.txtSalesman.IsSourceFromValueList = False
        Me.txtSalesman.IsUnique = False
        Me.txtSalesman.Location = New System.Drawing.Point(95, 104)
        Me.txtSalesman.MendatroryField = False
        Me.txtSalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesman.MyLinkLable1 = Me.MyLabel2
        Me.txtSalesman.MyLinkLable2 = Me.lblSalesman
        Me.txtSalesman.MyReadOnly = False
        Me.txtSalesman.MyShowMasterFormButton = False
        Me.txtSalesman.Name = "txtSalesman"
        Me.txtSalesman.ReferenceFieldDesc = Nothing
        Me.txtSalesman.ReferenceFieldName = Nothing
        Me.txtSalesman.ReferenceTableName = Nothing
        Me.txtSalesman.Size = New System.Drawing.Size(143, 18)
        Me.txtSalesman.TabIndex = 5
        Me.txtSalesman.Value = ""
        '
        'RadLabel23
        '
        Me.RadLabel23.FieldName = Nothing
        Me.RadLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel23.Location = New System.Drawing.Point(482, 24)
        Me.RadLabel23.Name = "RadLabel23"
        Me.RadLabel23.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel23.TabIndex = 15
        Me.RadLabel23.Text = "Quotation No"
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(482, 65)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel5.TabIndex = 20
        Me.RadLabel5.Text = "Pre-Carriage By"
        '
        'lblShipToLocation
        '
        Me.lblShipToLocation.AutoSize = False
        Me.lblShipToLocation.BorderVisible = True
        Me.lblShipToLocation.FieldName = Nothing
        Me.lblShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShipToLocation.Location = New System.Drawing.Point(240, 84)
        Me.lblShipToLocation.Name = "lblShipToLocation"
        Me.lblShipToLocation.Size = New System.Drawing.Size(238, 18)
        Me.lblShipToLocation.TabIndex = 13
        Me.lblShipToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShipToLocation.TextWrap = False
        '
        'lblBillToLocation
        '
        Me.lblBillToLocation.AutoSize = False
        Me.lblBillToLocation.BorderVisible = True
        Me.lblBillToLocation.FieldName = Nothing
        Me.lblBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillToLocation.Location = New System.Drawing.Point(240, 44)
        Me.lblBillToLocation.Name = "lblBillToLocation"
        Me.lblBillToLocation.Size = New System.Drawing.Size(238, 18)
        Me.lblBillToLocation.TabIndex = 7
        Me.lblBillToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBillToLocation.TextWrap = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(482, 172)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel6.TabIndex = 13
        Me.RadLabel6.Text = "Remarks"
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(0, 24)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "Description"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(0, 84)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel18.TabIndex = 19
        Me.RadLabel18.Text = "Ship To Location"
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(0, 44)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(84, 16)
        Me.RadLabel15.TabIndex = 20
        Me.RadLabel15.Text = "Bill To Location"
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
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 236)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1077, 134)
        Me.RadGroupBox2.TabIndex = 20
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
        Me.gv1.Size = New System.Drawing.Size(1057, 104)
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
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(0, 124)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel7.TabIndex = 15
        Me.RadLabel7.Text = "Reference No"
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(764, 66)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel13.TabIndex = 18
        Me.RadLabel13.Text = "Delivery Date"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(367, 4)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1
        Me.RadLabel4.Text = "Date"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(241, 64)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(237, 18)
        Me.lblVendorName.TabIndex = 10
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVendorName.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(0, 65)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(69, 16)
        Me.RadLabel2.TabIndex = 21
        Me.RadLabel2.Text = "Buyer Name"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(0, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 22
        Me.RadLabel1.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(345, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(17, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(600, 171)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(328, 18)
        Me.txtRemarks.TabIndex = 19
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
        Me.txtReqNo.Location = New System.Drawing.Point(600, 23)
        Me.txtReqNo.MendatroryField = False
        Me.txtReqNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.MyLinkLable1 = Me.RadLabel23
        Me.txtReqNo.MyLinkLable2 = Nothing
        Me.txtReqNo.MyReadOnly = True
        Me.txtReqNo.MyShowMasterFormButton = False
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReferenceFieldDesc = Nothing
        Me.txtReqNo.ReferenceFieldName = Nothing
        Me.txtReqNo.ReferenceTableName = Nothing
        Me.txtReqNo.Size = New System.Drawing.Size(148, 18)
        Me.txtReqNo.TabIndex = 8
        Me.txtReqNo.Value = ""
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
        Me.txtVendorNo.Location = New System.Drawing.Point(95, 64)
        Me.txtVendorNo.MendatroryField = True
        Me.txtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.txtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.txtVendorNo.MyReadOnly = False
        Me.txtVendorNo.MyShowMasterFormButton = False
        Me.txtVendorNo.Name = "txtVendorNo"
        Me.txtVendorNo.ReferenceFieldDesc = Nothing
        Me.txtVendorNo.ReferenceFieldName = Nothing
        Me.txtVendorNo.ReferenceTableName = Nothing
        Me.txtVendorNo.Size = New System.Drawing.Size(143, 18)
        Me.txtVendorNo.TabIndex = 3
        Me.txtVendorNo.Value = ""
        '
        'txtShipToLocation
        '
        Me.txtShipToLocation.CalculationExpression = Nothing
        Me.txtShipToLocation.FieldCode = Nothing
        Me.txtShipToLocation.FieldDesc = Nothing
        Me.txtShipToLocation.FieldMaxLength = 0
        Me.txtShipToLocation.FieldName = Nothing
        Me.txtShipToLocation.isCalculatedField = False
        Me.txtShipToLocation.IsSourceFromTable = False
        Me.txtShipToLocation.IsSourceFromValueList = False
        Me.txtShipToLocation.IsUnique = False
        Me.txtShipToLocation.Location = New System.Drawing.Point(95, 84)
        Me.txtShipToLocation.MendatroryField = False
        Me.txtShipToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipToLocation.MyLinkLable1 = Me.RadLabel18
        Me.txtShipToLocation.MyLinkLable2 = Me.lblShipToLocation
        Me.txtShipToLocation.MyReadOnly = False
        Me.txtShipToLocation.MyShowMasterFormButton = False
        Me.txtShipToLocation.Name = "txtShipToLocation"
        Me.txtShipToLocation.ReferenceFieldDesc = Nothing
        Me.txtShipToLocation.ReferenceFieldName = Nothing
        Me.txtShipToLocation.ReferenceTableName = Nothing
        Me.txtShipToLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtShipToLocation.TabIndex = 4
        Me.txtShipToLocation.Value = ""
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
        Me.txtBillToLocation.Location = New System.Drawing.Point(95, 44)
        Me.txtBillToLocation.MendatroryField = True
        Me.txtBillToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillToLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtBillToLocation.MyLinkLable2 = Me.lblBillToLocation
        Me.txtBillToLocation.MyReadOnly = False
        Me.txtBillToLocation.MyShowMasterFormButton = False
        Me.txtBillToLocation.Name = "txtBillToLocation"
        Me.txtBillToLocation.ReferenceFieldDesc = Nothing
        Me.txtBillToLocation.ReferenceFieldName = Nothing
        Me.txtBillToLocation.ReferenceTableName = Nothing
        Me.txtBillToLocation.Size = New System.Drawing.Size(143, 18)
        Me.txtBillToLocation.TabIndex = 2
        Me.txtBillToLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(978, -1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(95, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(246, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'txtDeliveryDate
        '
        Me.txtDeliveryDate.CalculationExpression = Nothing
        Me.txtDeliveryDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDeliveryDate.FieldCode = Nothing
        Me.txtDeliveryDate.FieldDesc = Nothing
        Me.txtDeliveryDate.FieldMaxLength = 0
        Me.txtDeliveryDate.FieldName = Nothing
        Me.txtDeliveryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDeliveryDate.isCalculatedField = False
        Me.txtDeliveryDate.IsSourceFromTable = False
        Me.txtDeliveryDate.IsSourceFromValueList = False
        Me.txtDeliveryDate.IsUnique = False
        Me.txtDeliveryDate.Location = New System.Drawing.Point(851, 65)
        Me.txtDeliveryDate.MendatroryField = False
        Me.txtDeliveryDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.MyLinkLable1 = Me.RadLabel13
        Me.txtDeliveryDate.MyLinkLable2 = Nothing
        Me.txtDeliveryDate.Name = "txtDeliveryDate"
        Me.txtDeliveryDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDeliveryDate.ReferenceFieldDesc = Nothing
        Me.txtDeliveryDate.ReferenceFieldName = Nothing
        Me.txtDeliveryDate.ReferenceTableName = Nothing
        Me.txtDeliveryDate.Size = New System.Drawing.Size(77, 18)
        Me.txtDeliveryDate.TabIndex = 12
        Me.txtDeliveryDate.TabStop = False
        Me.txtDeliveryDate.Text = "13/06/2011"
        Me.txtDeliveryDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtRefNo
        '
        Me.txtRefNo.CalculationExpression = Nothing
        Me.txtRefNo.FieldCode = Nothing
        Me.txtRefNo.FieldDesc = Nothing
        Me.txtRefNo.FieldMaxLength = 0
        Me.txtRefNo.FieldName = Nothing
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.isCalculatedField = False
        Me.txtRefNo.IsSourceFromTable = False
        Me.txtRefNo.IsSourceFromValueList = False
        Me.txtRefNo.IsUnique = False
        Me.txtRefNo.Location = New System.Drawing.Point(95, 124)
        Me.txtRefNo.MaxLength = 50
        Me.txtRefNo.MendatroryField = False
        Me.txtRefNo.MyLinkLable1 = Me.RadLabel7
        Me.txtRefNo.MyLinkLable2 = Nothing
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.ReferenceFieldDesc = Nothing
        Me.txtRefNo.ReferenceFieldName = Nothing
        Me.txtRefNo.ReferenceTableName = Nothing
        Me.txtRefNo.Size = New System.Drawing.Size(143, 18)
        Me.txtRefNo.TabIndex = 6
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
        Me.txtDesc.Location = New System.Drawing.Point(95, 24)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(383, 18)
        Me.txtDesc.TabIndex = 1
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(1079, 445)
        Me.RadPageViewPage5.Text = "Notified Party"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gv_Notify_Party)
        Me.RadGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox4.HeaderText = "Notified Party"
        Me.RadGroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(1079, 445)
        Me.RadGroupBox4.TabIndex = 29
        Me.RadGroupBox4.Text = "Notified Party"
        '
        'gv_Notify_Party
        '
        Me.gv_Notify_Party.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv_Notify_Party.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv_Notify_Party.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv_Notify_Party.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv_Notify_Party.ForeColor = System.Drawing.Color.Black
        Me.gv_Notify_Party.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv_Notify_Party.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv_Notify_Party.MasterTemplate.AllowDeleteRow = False
        Me.gv_Notify_Party.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv_Notify_Party.Name = "gv_Notify_Party"
        Me.gv_Notify_Party.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv_Notify_Party.ShowGroupPanel = False
        Me.gv_Notify_Party.ShowHeaderCellButtons = True
        Me.gv_Notify_Party.Size = New System.Drawing.Size(1059, 415)
        Me.gv_Notify_Party.TabIndex = 17
        Me.gv_Notify_Party.TabStop = False
        Me.gv_Notify_Party.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage2.Controls.Add(Me.lblAbandonmentNo)
        Me.RadPageViewPage2.Controls.Add(Me.lblRouteNo)
        Me.RadPageViewPage2.Controls.Add(Me.txtRouteNo)
        Me.RadPageViewPage2.Controls.Add(Me.lblRouteDesc)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage2.Controls.Add(Me.cboPOType)
        Me.RadPageViewPage2.Controls.Add(Me.lblPriceCode)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.txtPriceCode)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.txtPriceGroupCode)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1079, 445)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(312, 396)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 5
        Me.chkOnHold.Text = "On Hold"
        Me.chkOnHold.Visible = False
        '
        'lblAbandonmentNo
        '
        Me.lblAbandonmentNo.FieldName = Nothing
        Me.lblAbandonmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbandonmentNo.Location = New System.Drawing.Point(613, 382)
        Me.lblAbandonmentNo.Name = "lblAbandonmentNo"
        Me.lblAbandonmentNo.Size = New System.Drawing.Size(17, 16)
        Me.lblAbandonmentNo.TabIndex = 27
        Me.lblAbandonmentNo.Text = "xx"
        Me.lblAbandonmentNo.Visible = False
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Location = New System.Drawing.Point(444, 371)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 18)
        Me.lblRouteNo.TabIndex = 119
        Me.lblRouteNo.Text = "Route No"
        Me.lblRouteNo.Visible = False
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(506, 371)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Me.lblRouteNo
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.MyShowMasterFormButton = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(41, 19)
        Me.txtRouteNo.TabIndex = 22
        Me.txtRouteNo.Value = ""
        Me.txtRouteNo.Visible = False
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Location = New System.Drawing.Point(553, 373)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(41, 17)
        Me.lblRouteDesc.TabIndex = 23
        Me.lblRouteDesc.Visible = False
        '
        'RadLabel12
        '
        Me.RadLabel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel12.Location = New System.Drawing.Point(613, 360)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(318, 16)
        Me.RadLabel12.TabIndex = 25
        Me.RadLabel12.Text = "Double click on Tax Amount Column To Set Item wise Tax"
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(203, 370)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel8.TabIndex = 24
        Me.RadLabel8.Text = "SO Type"
        Me.RadLabel8.Visible = False
        '
        'cboPOType
        '
        Me.cboPOType.AutoCompleteDisplayMember = Nothing
        Me.cboPOType.AutoCompleteValueMember = Nothing
        Me.cboPOType.CalculationExpression = Nothing
        Me.cboPOType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboPOType.FieldCode = Nothing
        Me.cboPOType.FieldDesc = Nothing
        Me.cboPOType.FieldMaxLength = 0
        Me.cboPOType.FieldName = Nothing
        Me.cboPOType.isCalculatedField = False
        Me.cboPOType.IsSourceFromTable = False
        Me.cboPOType.IsSourceFromValueList = False
        Me.cboPOType.IsUnique = False
        Me.cboPOType.Location = New System.Drawing.Point(262, 370)
        Me.cboPOType.MendatroryField = True
        Me.cboPOType.MyLinkLable1 = Me.RadLabel8
        Me.cboPOType.MyLinkLable2 = Nothing
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.ReferenceFieldDesc = Nothing
        Me.cboPOType.ReferenceFieldName = Nothing
        Me.cboPOType.ReferenceTableName = Nothing
        Me.cboPOType.Size = New System.Drawing.Size(77, 20)
        Me.cboPOType.TabIndex = 18
        Me.cboPOType.Visible = False
        '
        'lblPriceCode
        '
        Me.lblPriceCode.FieldName = Nothing
        Me.lblPriceCode.Location = New System.Drawing.Point(3, 345)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceCode.TabIndex = 56
        Me.lblPriceCode.Text = "Price Code"
        Me.lblPriceCode.Visible = False
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
        'txtPriceCode
        '
        Me.txtPriceCode.AutoSize = False
        Me.txtPriceCode.BorderVisible = True
        Me.txtPriceCode.FieldName = Nothing
        Me.txtPriceCode.Location = New System.Drawing.Point(67, 345)
        Me.txtPriceCode.Name = "txtPriceCode"
        Me.txtPriceCode.Size = New System.Drawing.Size(142, 19)
        Me.txtPriceCode.TabIndex = 25
        Me.txtPriceCode.TextWrap = False
        Me.txtPriceCode.Visible = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(776, 341)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 4
        Me.RadLabel10.Text = "Double click To Chage Rate"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(214, 346)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(94, 18)
        Me.MyLabel7.TabIndex = 121
        Me.MyLabel7.Text = "Price Group Code"
        Me.MyLabel7.Visible = False
        '
        'txtPriceGroupCode
        '
        Me.txtPriceGroupCode.AutoSize = False
        Me.txtPriceGroupCode.BorderVisible = True
        Me.txtPriceGroupCode.FieldName = Nothing
        Me.txtPriceGroupCode.Location = New System.Drawing.Point(312, 346)
        Me.txtPriceGroupCode.Name = "txtPriceGroupCode"
        Me.txtPriceGroupCode.Size = New System.Drawing.Size(142, 19)
        Me.txtPriceGroupCode.TabIndex = 26
        Me.txtPriceGroupCode.TextWrap = False
        Me.txtPriceGroupCode.Visible = False
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
        Me.gv2.Size = New System.Drawing.Size(929, 303)
        Me.gv2.TabIndex = 1
        Me.gv2.TabStop = False
        Me.gv2.Text = "RadGridView1"
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.MyLabel4)
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Location = New System.Drawing.Point(460, 343)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(364, 22)
        Me.pnlPCJ.TabIndex = 21
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(1, 3)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel4.TabIndex = 34
        Me.MyLabel4.Text = "Project"
        Me.MyLabel4.Visible = False
        '
        'fndProject
        '
        Me.fndProject.CalculationExpression = Nothing
        Me.fndProject.FieldCode = Nothing
        Me.fndProject.FieldDesc = Nothing
        Me.fndProject.FieldMaxLength = 0
        Me.fndProject.FieldName = Nothing
        Me.fndProject.isCalculatedField = False
        Me.fndProject.IsSourceFromTable = False
        Me.fndProject.IsSourceFromValueList = False
        Me.fndProject.IsUnique = False
        Me.fndProject.Location = New System.Drawing.Point(50, 2)
        Me.fndProject.MendatroryField = False
        Me.fndProject.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndProject.MyLinkLable1 = Me.MyLabel4
        Me.fndProject.MyLinkLable2 = Me.lblProject
        Me.fndProject.MyReadOnly = False
        Me.fndProject.MyShowMasterFormButton = False
        Me.fndProject.Name = "fndProject"
        Me.fndProject.ReferenceFieldDesc = Nothing
        Me.fndProject.ReferenceFieldName = Nothing
        Me.fndProject.ReferenceTableName = Nothing
        Me.fndProject.Size = New System.Drawing.Size(119, 20)
        Me.fndProject.TabIndex = 0
        Me.fndProject.Value = ""
        Me.fndProject.Visible = False
        '
        'lblProject
        '
        Me.lblProject.AutoSize = False
        Me.lblProject.BorderVisible = True
        Me.lblProject.FieldName = Nothing
        Me.lblProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProject.Location = New System.Drawing.Point(173, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(185, 20)
        Me.lblProject.TabIndex = 1
        Me.lblProject.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblProject.TextWrap = False
        Me.lblProject.Visible = False
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
        Me.RadPanel1.Controls.Add(Me.lblAddCharges)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 396)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1079, 49)
        Me.RadPanel1.TabIndex = 0
        '
        'RadLabel31
        '
        Me.RadLabel31.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(835, 10)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(130, 16)
        Me.RadLabel31.TabIndex = 130
        Me.RadLabel31.Text = "Total Additional Charges"
        '
        'lblAddCharges
        '
        Me.lblAddCharges.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddCharges.AutoSize = False
        Me.lblAddCharges.BorderVisible = True
        Me.lblAddCharges.FieldName = Nothing
        Me.lblAddCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges.Location = New System.Drawing.Point(966, 10)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 131
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1079, 445)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1079, 445)
        Me.UcCustomFields1.TabIndex = 1
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblInvoiceDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscAmt)
        Me.RadPageViewPage4.Controls.Add(Me.txtDiscPer)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage4.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage4.Controls.Add(Me.txtComment)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotRAmt)
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
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(139, 221)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 152
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(223, 221)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 151
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvoiceDiscAmt
        '
        Me.lblInvoiceDiscAmt.AutoSize = False
        Me.lblInvoiceDiscAmt.BorderVisible = True
        Me.lblInvoiceDiscAmt.FieldName = Nothing
        Me.lblInvoiceDiscAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceDiscAmt.Location = New System.Drawing.Point(223, 147)
        Me.lblInvoiceDiscAmt.Name = "lblInvoiceDiscAmt"
        Me.lblInvoiceDiscAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblInvoiceDiscAmt.TabIndex = 7
        Me.lblInvoiceDiscAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(78, 147)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel6.TabIndex = 150
        Me.MyLabel6.Text = "- Invoice Discount Amount"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(148, 99)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(68, 18)
        Me.MyLabel3.TabIndex = 3
        Me.MyLabel3.Text = "Discount On"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnAmt)
        Me.RadGroupBox3.Controls.Add(Me.chkDiscountOnRate)
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(222, 99)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(141, 21)
        Me.RadGroupBox3.TabIndex = 4
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
        Me.txtDiscAmt.Location = New System.Drawing.Point(283, 123)
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
        Me.txtDiscPer.Location = New System.Drawing.Point(222, 123)
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
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(264, 125)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(15, 18)
        Me.MyLabel1.TabIndex = 148
        Me.MyLabel1.Text = "%"
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
        Me.pnlCurrConv.Location = New System.Drawing.Point(159, 24)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(688, 38)
        Me.pnlCurrConv.TabIndex = 0
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
        Me.txtConversionRate.Location = New System.Drawing.Point(290, 10)
        Me.txtConversionRate.MendatroryField = True
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
        Me.txtCurrencyCode.MendatroryField = True
        Me.txtCurrencyCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.MyLinkLable1 = Nothing
        Me.txtCurrencyCode.MyLinkLable2 = Nothing
        Me.txtCurrencyCode.MyReadOnly = False
        Me.txtCurrencyCode.MyShowMasterFormButton = False
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.ReferenceFieldDesc = Nothing
        Me.txtCurrencyCode.ReferenceFieldName = Nothing
        Me.txtCurrencyCode.ReferenceTableName = Nothing
        Me.txtCurrencyCode.Size = New System.Drawing.Size(136, 19)
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
        Me.txtApplicableFrom.Location = New System.Drawing.Point(513, 11)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(169, 18)
        Me.txtApplicableFrom.TabIndex = 2
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(4, 11)
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
        Me.RadLabel32.Location = New System.Drawing.Point(76, 246)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel32.TabIndex = 135
        Me.RadLabel32.Text = "+ Total Additional Charges"
        '
        'lblAddCharges1
        '
        Me.lblAddCharges1.AutoSize = False
        Me.lblAddCharges1.BorderVisible = True
        Me.lblAddCharges1.FieldName = Nothing
        Me.lblAddCharges1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges1.Location = New System.Drawing.Point(223, 246)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 11
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(492, 68)
        Me.txtComment.MaxLength = 5000
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtComment.Size = New System.Drawing.Size(355, 197)
        Me.txtComment.TabIndex = 2
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(369, 72)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(117, 16)
        Me.RadLabel14.TabIndex = 127
        Me.RadLabel14.Text = "Terms and Conditions"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(96, 197)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 120
        Me.RadLabel9.Text = "Amount After Discount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(116, 273)
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
        Me.lblTotRAmt.Location = New System.Drawing.Point(223, 271)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 12
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(223, 197)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 9
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(223, 173)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 8
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(223, 72)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 1
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(117, 173)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 121
        Me.RadLabel22.Text = "- Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(30, 72)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 119
        Me.RadLabel19.Text = "Document Amount without Discount"
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1168, 445)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1168, 445)
        Me.UcAttachment1.TabIndex = 0
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnpreview, Me.btnsend, Me.btnSendForApproval})
        Me.btnsetting.Location = New System.Drawing.Point(231, 2)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(87, 22)
        Me.btnsetting.TabIndex = 3
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'btnpreview
        '
        Me.btnpreview.AccessibleDescription = "Preview"
        Me.btnpreview.AccessibleName = "Preview"
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Text = "Preview"
        Me.btnpreview.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
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
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(494, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(157, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(81, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1022, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
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
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3, Me.RadMenuItem1, Me.RadMenuItem4, Me.RadMenuItem5})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "E-Mail/SMS Setting"
        Me.RadMenuItem3.AccessibleName = "E-Mail/SMS Setting"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "E-Mail/SMS Setting"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Save Layout"
        Me.RadMenuItem1.AccessibleName = "Save Layout"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Save Layout"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "RadMenuItem4"
        Me.RadMenuItem4.AccessibleName = "RadMenuItem4"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "RadMenuItem5"
        Me.RadMenuItem5.AccessibleName = "RadMenuItem5"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Footer Setting"
        Me.RadMenuItem5.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(324, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 22)
        Me.btnCancel.TabIndex = 45
        Me.btnCancel.Text = "Cancel"
        '
        'frmEXSalesOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 548)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmEXSalesOrder"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Export Sales Order"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkIsTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModeOfTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.txtOthr_Instructn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtComm_Pay_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbComm_Payable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmt_comm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbComm_Amount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAdvance_Pers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPODate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTerms_Payment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCRTermName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTerms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesman, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gv_Notify_Party.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_Notify_Party, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAbandonmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPOType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriceGroupCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtRefNo As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtVendorNo As common.UserControls.txtFinder
    Friend WithEvents txtShipToLocation As common.UserControls.txtFinder
    Friend WithEvents txtBillToLocation As common.UserControls.txtFinder
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents cboPOType As common.Controls.MyComboBox
    Friend WithEvents txtDeliveryDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents txtDept As common.UserControls.txtFinder
    Friend WithEvents cboItemType As common.Controls.MyComboBox
    Friend WithEvents txtReqNo As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents gvAC As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents lblShipToLocation As common.Controls.MyLabel
    Friend WithEvents lblBillToLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents lblDept As common.Controls.MyLabel
    Friend WithEvents RadLabel20 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents RadLabel23 As common.Controls.MyLabel
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges1 As common.Controls.MyLabel
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSalesman As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtSalesman As common.UserControls.txtFinder
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
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents txtPriceCode As common.Controls.MyLabel
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkDiscountOnAmt As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkDiscountOnRate As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtDiscAmt As common.MyNumBox
    Friend WithEvents txtDiscPer As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblInvoiceDiscAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtPriceGroupCode As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtPONo As common.Controls.MyTextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnpreview As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnsend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkclose As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnSendForApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv_Notify_Party As common.UserControls.MyRadGridView
    Friend WithEvents cmbTerms As common.Controls.MyComboBox
    Friend WithEvents cmbTerms_Payment As common.Controls.MyComboBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents cmbComm_Payable As common.Controls.MyComboBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtOthr_Instructn As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents cmbComm_Amount As common.Controls.MyComboBox
    Friend WithEvents txtAmt_comm As common.MyNumBox
    Friend WithEvents txtCRTermCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblCRTermName As common.Controls.MyLabel
    Friend WithEvents txtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents txtAdvance_Pers As common.MyNumBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtComm_Pay_name As common.Controls.MyLabel
    Friend WithEvents fndComm_Pay_Code As common.UserControls.txtFinder
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents dtpPODate As common.Controls.MyDateTimePicker
    Friend WithEvents lblAbandonmentNo As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents cmbDocType As common.Controls.MyComboBox
    Friend WithEvents cboModeOfTransport As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents chkIsTaxable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents btnCancel As Telerik.WinControls.UI.RadButton
End Class

