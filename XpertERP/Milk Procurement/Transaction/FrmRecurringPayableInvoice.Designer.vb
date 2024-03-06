<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRecurringPayableInvoice
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRecurringPayableInvoice))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtExpirationAmount = New common.Controls.MyTextBox()
        Me.LblScheduler = New common.Controls.MyLabel()
        Me.FndScheduler = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.CboExpiration = New common.Controls.MyComboBox()
        Me.DtpExpirationDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.grpProvision = New System.Windows.Forms.GroupBox()
        Me.btlShowProvision = New System.Windows.Forms.LinkLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtProvAmt = New common.Controls.MyTextBox()
        Me.btnProvSelect = New System.Windows.Forms.LinkLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpToProv = New common.Controls.MyDateTimePicker()
        Me.dtpFromProv = New common.Controls.MyDateTimePicker()
        Me.LblLocDesp = New common.Controls.MyLabel()
        Me.InvDate = New common.Controls.MyLabel()
        Me.pnlPCJ = New System.Windows.Forms.Panel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndProject = New common.UserControls.txtFinder()
        Me.lblProject = New common.Controls.MyLabel()
        Me.chkProRated = New common.Controls.MyCheckBox()
        Me.cmbRefType = New common.Controls.MyComboBox()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.chkDeduction = New common.Controls.MyCheckBox()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.ChkSecurity = New common.Controls.MyCheckBox()
        Me.txtRefDocNo = New common.UserControls.txtFinder()
        Me.txtACSet = New common.UserControls.txtFinder()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.chkQuickMode = New common.Controls.MyCheckBox()
        Me.chkProvision = New common.Controls.MyCheckBox()
        Me.txtlocation = New common.UserControls.txtFinder()
        Me.RadLabel18 = New Telerik.WinControls.UI.RadLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.btnDrillDown = New Telerik.WinControls.UI.RadButton()
        Me.TxtVendorNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.lblTotRAmt1 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtVendorInvDatre = New common.Controls.MyDateTimePicker()
        Me.txtVendorInvoiceNo = New common.Controls.MyTextBox()
        Me.txtOrderNo = New common.Controls.MyTextBox()
        Me.txtPONo = New common.Controls.MyTextBox()
        Me.cboDocType = New common.Controls.MyComboBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gvAC = New common.UserControls.MyRadGridView()
        Me.RadLabel31 = New common.Controls.MyLabel()
        Me.lblAddCharges = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.pnlCurrConv = New System.Windows.Forms.Panel()
        Me.txtConversionRate = New common.MyNumBox()
        Me.txtCurrencyCode = New common.UserControls.txtFinder()
        Me.lblEffectiveFrom = New common.Controls.MyLabel()
        Me.txtApplicableFrom = New common.Controls.MyLabel()
        Me.lblCurrency = New common.Controls.MyLabel()
        Me.lblConvRate = New common.Controls.MyLabel()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblLandedAmt = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblTotEmptyAmt = New common.Controls.MyLabel()
        Me.RadLabel32 = New common.Controls.MyLabel()
        Me.lblAddCharges1 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        Me.BtnProcess = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSendForApproval = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnViewTDSDetails = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mbtnExportApTransaction = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExportDrNote = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem7 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mbtnImportApTransaction = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImportDrNote = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem8 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem9 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.TxtExpirationAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblScheduler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboExpiration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpExpirationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProvision.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProvAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromProv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InvDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPCJ.SuspendLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProRated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRefType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkQuickMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProvision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorInvDatre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        Me.pnlCurrConv.SuspendLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLandedAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotEmptyAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnProcess)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewTDSDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(945, 456)
        Me.SplitContainer1.SplitterDistance = 418
        Me.SplitContainer1.TabIndex = 0
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
        Me.RadPageView1.Size = New System.Drawing.Size(945, 418)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtExpirationAmount)
        Me.RadPageViewPage1.Controls.Add(Me.LblScheduler)
        Me.RadPageViewPage1.Controls.Add(Me.FndScheduler)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.CboExpiration)
        Me.RadPageViewPage1.Controls.Add(Me.DtpExpirationDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.grpProvision)
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromProv)
        Me.RadPageViewPage1.Controls.Add(Me.LblLocDesp)
        Me.RadPageViewPage1.Controls.Add(Me.InvDate)
        Me.RadPageViewPage1.Controls.Add(Me.pnlPCJ)
        Me.RadPageViewPage1.Controls.Add(Me.txtlocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnDrillDown)
        Me.RadPageViewPage1.Controls.Add(Me.TxtVendorNo)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.lblTotRAmt1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendorName)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorInvDatre)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtOrderNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtPONo)
        Me.RadPageViewPage1.Controls.Add(Me.cboDocType)
        Me.RadPageViewPage1.Controls.Add(Me.txtDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(68.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(924, 372)
        Me.RadPageViewPage1.Text = "Document"
        '
        'TxtExpirationAmount
        '
        Me.TxtExpirationAmount.CalculationExpression = Nothing
        Me.TxtExpirationAmount.FieldCode = Nothing
        Me.TxtExpirationAmount.FieldDesc = Nothing
        Me.TxtExpirationAmount.FieldMaxLength = 0
        Me.TxtExpirationAmount.FieldName = Nothing
        Me.TxtExpirationAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExpirationAmount.isCalculatedField = False
        Me.TxtExpirationAmount.IsSourceFromTable = False
        Me.TxtExpirationAmount.IsSourceFromValueList = False
        Me.TxtExpirationAmount.IsUnique = False
        Me.TxtExpirationAmount.Location = New System.Drawing.Point(236, 139)
        Me.TxtExpirationAmount.MaxLength = 50
        Me.TxtExpirationAmount.MendatroryField = False
        Me.TxtExpirationAmount.MyLinkLable1 = Nothing
        Me.TxtExpirationAmount.MyLinkLable2 = Nothing
        Me.TxtExpirationAmount.Name = "TxtExpirationAmount"
        Me.TxtExpirationAmount.ReferenceFieldDesc = Nothing
        Me.TxtExpirationAmount.ReferenceFieldName = Nothing
        Me.TxtExpirationAmount.ReferenceTableName = Nothing
        Me.TxtExpirationAmount.Size = New System.Drawing.Size(134, 18)
        Me.TxtExpirationAmount.TabIndex = 125
        Me.TxtExpirationAmount.Visible = False
        '
        'LblScheduler
        '
        Me.LblScheduler.AutoSize = False
        Me.LblScheduler.BorderVisible = True
        Me.LblScheduler.FieldName = Nothing
        Me.LblScheduler.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblScheduler.Location = New System.Drawing.Point(239, 114)
        Me.LblScheduler.Name = "LblScheduler"
        Me.LblScheduler.Size = New System.Drawing.Size(198, 18)
        Me.LblScheduler.TabIndex = 124
        Me.LblScheduler.TextWrap = False
        '
        'FndScheduler
        '
        Me.FndScheduler.CalculationExpression = Nothing
        Me.FndScheduler.FieldCode = Nothing
        Me.FndScheduler.FieldDesc = Nothing
        Me.FndScheduler.FieldMaxLength = 0
        Me.FndScheduler.FieldName = Nothing
        Me.FndScheduler.isCalculatedField = False
        Me.FndScheduler.IsSourceFromTable = False
        Me.FndScheduler.IsSourceFromValueList = False
        Me.FndScheduler.IsUnique = False
        Me.FndScheduler.Location = New System.Drawing.Point(90, 112)
        Me.FndScheduler.MendatroryField = True
        Me.FndScheduler.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndScheduler.MyLinkLable1 = Nothing
        Me.FndScheduler.MyLinkLable2 = Me.LblScheduler
        Me.FndScheduler.MyReadOnly = False
        Me.FndScheduler.MyShowMasterFormButton = False
        Me.FndScheduler.Name = "FndScheduler"
        Me.FndScheduler.ReferenceFieldDesc = Nothing
        Me.FndScheduler.ReferenceFieldName = Nothing
        Me.FndScheduler.ReferenceTableName = Nothing
        Me.FndScheduler.Size = New System.Drawing.Size(143, 22)
        Me.FndScheduler.TabIndex = 123
        Me.FndScheduler.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(0, 115)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel8.TabIndex = 120
        Me.MyLabel8.Text = "Scheduler Code"
        '
        'CboExpiration
        '
        Me.CboExpiration.AutoCompleteDisplayMember = Nothing
        Me.CboExpiration.AutoCompleteValueMember = Nothing
        Me.CboExpiration.CalculationExpression = Nothing
        Me.CboExpiration.DropDownAnimationEnabled = True
        Me.CboExpiration.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboExpiration.FieldCode = Nothing
        Me.CboExpiration.FieldDesc = Nothing
        Me.CboExpiration.FieldMaxLength = 0
        Me.CboExpiration.FieldName = Nothing
        Me.CboExpiration.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboExpiration.ForeColor = System.Drawing.Color.Lime
        Me.CboExpiration.isCalculatedField = False
        Me.CboExpiration.IsSourceFromTable = False
        Me.CboExpiration.IsSourceFromValueList = False
        Me.CboExpiration.IsUnique = False
        RadListDataItem1.Text = "No Expiration"
        RadListDataItem2.Text = "Specific Date"
        RadListDataItem3.Text = "Specific Amount"
        Me.CboExpiration.Items.Add(RadListDataItem1)
        Me.CboExpiration.Items.Add(RadListDataItem2)
        Me.CboExpiration.Items.Add(RadListDataItem3)
        Me.CboExpiration.Location = New System.Drawing.Point(90, 139)
        Me.CboExpiration.MendatroryField = True
        Me.CboExpiration.MyLinkLable1 = Nothing
        Me.CboExpiration.MyLinkLable2 = Nothing
        Me.CboExpiration.Name = "CboExpiration"
        Me.CboExpiration.ReferenceFieldDesc = Nothing
        Me.CboExpiration.ReferenceFieldName = Nothing
        Me.CboExpiration.ReferenceTableName = Nothing
        '
        '
        '
        Me.CboExpiration.RootElement.StretchVertically = True
        Me.CboExpiration.Size = New System.Drawing.Size(143, 18)
        Me.CboExpiration.TabIndex = 122
        '
        'DtpExpirationDate
        '
        Me.DtpExpirationDate.AccessibleName = "InvDate"
        Me.DtpExpirationDate.CalculationExpression = Nothing
        Me.DtpExpirationDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpExpirationDate.FieldCode = Nothing
        Me.DtpExpirationDate.FieldDesc = Nothing
        Me.DtpExpirationDate.FieldMaxLength = 0
        Me.DtpExpirationDate.FieldName = Nothing
        Me.DtpExpirationDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpExpirationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpExpirationDate.isCalculatedField = False
        Me.DtpExpirationDate.IsSourceFromTable = False
        Me.DtpExpirationDate.IsSourceFromValueList = False
        Me.DtpExpirationDate.IsUnique = False
        Me.DtpExpirationDate.Location = New System.Drawing.Point(239, 139)
        Me.DtpExpirationDate.MendatroryField = False
        Me.DtpExpirationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpExpirationDate.MyLinkLable1 = Me.RadLabel4
        Me.DtpExpirationDate.MyLinkLable2 = Nothing
        Me.DtpExpirationDate.Name = "DtpExpirationDate"
        Me.DtpExpirationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpExpirationDate.ReferenceFieldDesc = Nothing
        Me.DtpExpirationDate.ReferenceFieldName = Nothing
        Me.DtpExpirationDate.ReferenceTableName = Nothing
        Me.DtpExpirationDate.ShowCheckBox = True
        Me.DtpExpirationDate.Size = New System.Drawing.Size(134, 18)
        Me.DtpExpirationDate.TabIndex = 117
        Me.DtpExpirationDate.TabStop = False
        Me.DtpExpirationDate.Text = "31/03/2012"
        Me.DtpExpirationDate.Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        Me.DtpExpirationDate.Visible = False
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(0, 70)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(85, 16)
        Me.RadLabel4.TabIndex = 25
        Me.RadLabel4.Text = "Document Date"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(441, 115)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel5.TabIndex = 25
        Me.MyLabel5.Text = "Start Date"
        '
        'grpProvision
        '
        Me.grpProvision.Controls.Add(Me.btlShowProvision)
        Me.grpProvision.Controls.Add(Me.MyLabel7)
        Me.grpProvision.Controls.Add(Me.txtProvAmt)
        Me.grpProvision.Controls.Add(Me.btnProvSelect)
        Me.grpProvision.Controls.Add(Me.MyLabel6)
        Me.grpProvision.Controls.Add(Me.dtpToProv)
        Me.grpProvision.Location = New System.Drawing.Point(5, 162)
        Me.grpProvision.Name = "grpProvision"
        Me.grpProvision.Size = New System.Drawing.Size(82, 15)
        Me.grpProvision.TabIndex = 42
        Me.grpProvision.TabStop = False
        '
        'btlShowProvision
        '
        Me.btlShowProvision.AutoSize = True
        Me.btlShowProvision.Location = New System.Drawing.Point(145, 52)
        Me.btlShowProvision.Name = "btlShowProvision"
        Me.btlShowProvision.Size = New System.Drawing.Size(116, 14)
        Me.btlShowProvision.TabIndex = 47
        Me.btlShowProvision.TabStop = True
        Me.btlShowProvision.Text = "List Selected Provision"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(6, 50)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel7.TabIndex = 46
        Me.MyLabel7.Text = "Prov Amt."
        '
        'txtProvAmt
        '
        Me.txtProvAmt.CalculationExpression = Nothing
        Me.txtProvAmt.FieldCode = Nothing
        Me.txtProvAmt.FieldDesc = Nothing
        Me.txtProvAmt.FieldMaxLength = 0
        Me.txtProvAmt.FieldName = Nothing
        Me.txtProvAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProvAmt.isCalculatedField = False
        Me.txtProvAmt.IsSourceFromTable = False
        Me.txtProvAmt.IsSourceFromValueList = False
        Me.txtProvAmt.IsUnique = False
        Me.txtProvAmt.Location = New System.Drawing.Point(62, 49)
        Me.txtProvAmt.MaxLength = 30
        Me.txtProvAmt.MendatroryField = False
        Me.txtProvAmt.MyLinkLable1 = Me.MyLabel7
        Me.txtProvAmt.MyLinkLable2 = Nothing
        Me.txtProvAmt.Name = "txtProvAmt"
        Me.txtProvAmt.ReadOnly = True
        Me.txtProvAmt.ReferenceFieldDesc = Nothing
        Me.txtProvAmt.ReferenceFieldName = Nothing
        Me.txtProvAmt.ReferenceTableName = Nothing
        Me.txtProvAmt.Size = New System.Drawing.Size(78, 18)
        Me.txtProvAmt.TabIndex = 45
        '
        'btnProvSelect
        '
        Me.btnProvSelect.AutoSize = True
        Me.btnProvSelect.Location = New System.Drawing.Point(137, 9)
        Me.btnProvSelect.Name = "btnProvSelect"
        Me.btnProvSelect.Size = New System.Drawing.Size(123, 14)
        Me.btnProvSelect.TabIndex = 44
        Me.btnProvSelect.TabStop = True
        Me.btnProvSelect.Text = "Click To Select Provision"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(140, 27)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel6.TabIndex = 27
        Me.MyLabel6.Text = "To Date"
        '
        'dtpToProv
        '
        Me.dtpToProv.CalculationExpression = Nothing
        Me.dtpToProv.CustomFormat = "dd/MM/yyyy"
        Me.dtpToProv.FieldCode = Nothing
        Me.dtpToProv.FieldDesc = Nothing
        Me.dtpToProv.FieldMaxLength = 0
        Me.dtpToProv.FieldName = Nothing
        Me.dtpToProv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToProv.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToProv.isCalculatedField = False
        Me.dtpToProv.IsSourceFromTable = False
        Me.dtpToProv.IsSourceFromValueList = False
        Me.dtpToProv.IsUnique = False
        Me.dtpToProv.Location = New System.Drawing.Point(186, 26)
        Me.dtpToProv.MendatroryField = False
        Me.dtpToProv.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToProv.MyLinkLable1 = Me.MyLabel6
        Me.dtpToProv.MyLinkLable2 = Nothing
        Me.dtpToProv.Name = "dtpToProv"
        Me.dtpToProv.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToProv.ReferenceFieldDesc = Nothing
        Me.dtpToProv.ReferenceFieldName = Nothing
        Me.dtpToProv.ReferenceTableName = Nothing
        Me.dtpToProv.Size = New System.Drawing.Size(76, 18)
        Me.dtpToProv.TabIndex = 26
        Me.dtpToProv.TabStop = False
        Me.dtpToProv.Text = "13/06/2011"
        Me.dtpToProv.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'dtpFromProv
        '
        Me.dtpFromProv.CalculationExpression = Nothing
        Me.dtpFromProv.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromProv.FieldCode = Nothing
        Me.dtpFromProv.FieldDesc = Nothing
        Me.dtpFromProv.FieldMaxLength = 0
        Me.dtpFromProv.FieldName = Nothing
        Me.dtpFromProv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromProv.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromProv.isCalculatedField = False
        Me.dtpFromProv.IsSourceFromTable = False
        Me.dtpFromProv.IsSourceFromValueList = False
        Me.dtpFromProv.IsUnique = False
        Me.dtpFromProv.Location = New System.Drawing.Point(541, 114)
        Me.dtpFromProv.MendatroryField = False
        Me.dtpFromProv.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromProv.MyLinkLable1 = Me.MyLabel5
        Me.dtpFromProv.MyLinkLable2 = Nothing
        Me.dtpFromProv.Name = "dtpFromProv"
        Me.dtpFromProv.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromProv.ReferenceFieldDesc = Nothing
        Me.dtpFromProv.ReferenceFieldName = Nothing
        Me.dtpFromProv.ReferenceTableName = Nothing
        Me.dtpFromProv.Size = New System.Drawing.Size(108, 18)
        Me.dtpFromProv.TabIndex = 24
        Me.dtpFromProv.TabStop = False
        Me.dtpFromProv.Text = "13/06/2011"
        Me.dtpFromProv.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'LblLocDesp
        '
        Me.LblLocDesp.AutoSize = False
        Me.LblLocDesp.BorderVisible = True
        Me.LblLocDesp.FieldName = Nothing
        Me.LblLocDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLocDesp.Location = New System.Drawing.Point(239, 25)
        Me.LblLocDesp.Name = "LblLocDesp"
        Me.LblLocDesp.Size = New System.Drawing.Size(410, 18)
        Me.LblLocDesp.TabIndex = 41
        Me.LblLocDesp.TextWrap = False
        '
        'InvDate
        '
        Me.InvDate.FieldName = Nothing
        Me.InvDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InvDate.Location = New System.Drawing.Point(0, 140)
        Me.InvDate.Name = "InvDate"
        Me.InvDate.Size = New System.Drawing.Size(85, 16)
        Me.InvDate.TabIndex = 119
        Me.InvDate.Text = "Expiration Type"
        '
        'pnlPCJ
        '
        Me.pnlPCJ.Controls.Add(Me.MyLabel4)
        Me.pnlPCJ.Controls.Add(Me.fndProject)
        Me.pnlPCJ.Controls.Add(Me.chkProRated)
        Me.pnlPCJ.Controls.Add(Me.lblProject)
        Me.pnlPCJ.Controls.Add(Me.cmbRefType)
        Me.pnlPCJ.Controls.Add(Me.chkDeduction)
        Me.pnlPCJ.Controls.Add(Me.RadLabel14)
        Me.pnlPCJ.Controls.Add(Me.RadLabel15)
        Me.pnlPCJ.Controls.Add(Me.ChkSecurity)
        Me.pnlPCJ.Controls.Add(Me.txtRefDocNo)
        Me.pnlPCJ.Controls.Add(Me.txtACSet)
        Me.pnlPCJ.Controls.Add(Me.RadLabel6)
        Me.pnlPCJ.Controls.Add(Me.chkQuickMode)
        Me.pnlPCJ.Controls.Add(Me.chkProvision)
        Me.pnlPCJ.Location = New System.Drawing.Point(862, 2)
        Me.pnlPCJ.Name = "pnlPCJ"
        Me.pnlPCJ.Size = New System.Drawing.Size(61, 20)
        Me.pnlPCJ.TabIndex = 18
        Me.pnlPCJ.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 0)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel4.TabIndex = 0
        Me.MyLabel4.Text = "Project"
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
        Me.fndProject.Location = New System.Drawing.Point(88, 1)
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
        Me.fndProject.Size = New System.Drawing.Size(143, 20)
        Me.fndProject.TabIndex = 0
        Me.fndProject.Value = ""
        '
        'lblProject
        '
        Me.lblProject.AutoSize = False
        Me.lblProject.BorderVisible = True
        Me.lblProject.FieldName = Nothing
        Me.lblProject.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProject.Location = New System.Drawing.Point(237, 1)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(407, 20)
        Me.lblProject.TabIndex = 1
        Me.lblProject.TextWrap = False
        '
        'chkProRated
        '
        Me.chkProRated.Location = New System.Drawing.Point(499, 49)
        Me.chkProRated.MyLinkLable1 = Nothing
        Me.chkProRated.MyLinkLable2 = Nothing
        Me.chkProRated.Name = "chkProRated"
        Me.chkProRated.Size = New System.Drawing.Size(69, 18)
        Me.chkProRated.TabIndex = 17
        Me.chkProRated.Tag1 = Nothing
        Me.chkProRated.Text = "Pro Rated"
        Me.chkProRated.Visible = False
        '
        'cmbRefType
        '
        Me.cmbRefType.AllowDrop = True
        Me.cmbRefType.AutoCompleteDisplayMember = Nothing
        Me.cmbRefType.AutoCompleteValueMember = Nothing
        Me.cmbRefType.CalculationExpression = Nothing
        Me.cmbRefType.DropDownAnimationEnabled = True
        Me.cmbRefType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbRefType.FieldCode = Nothing
        Me.cmbRefType.FieldDesc = Nothing
        Me.cmbRefType.FieldMaxLength = 0
        Me.cmbRefType.FieldName = Nothing
        Me.cmbRefType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRefType.isCalculatedField = False
        Me.cmbRefType.IsSourceFromTable = False
        Me.cmbRefType.IsSourceFromValueList = False
        Me.cmbRefType.IsUnique = False
        Me.cmbRefType.Location = New System.Drawing.Point(88, 25)
        Me.cmbRefType.MendatroryField = False
        Me.cmbRefType.MyLinkLable1 = Me.RadLabel14
        Me.cmbRefType.MyLinkLable2 = Nothing
        Me.cmbRefType.Name = "cmbRefType"
        Me.cmbRefType.ReferenceFieldDesc = Nothing
        Me.cmbRefType.ReferenceFieldName = Nothing
        Me.cmbRefType.ReferenceTableName = Nothing
        Me.cmbRefType.Size = New System.Drawing.Size(114, 18)
        Me.cmbRefType.TabIndex = 16
        Me.cmbRefType.Text = "RadDropDownList1"
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(0, 26)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(72, 16)
        Me.RadLabel14.TabIndex = 22
        Me.RadLabel14.Text = "Ref DocType"
        '
        'chkDeduction
        '
        Me.chkDeduction.Location = New System.Drawing.Point(332, 65)
        Me.chkDeduction.MyLinkLable1 = Nothing
        Me.chkDeduction.MyLinkLable2 = Nothing
        Me.chkDeduction.Name = "chkDeduction"
        Me.chkDeduction.Size = New System.Drawing.Size(72, 18)
        Me.chkDeduction.TabIndex = 44
        Me.chkDeduction.Tag1 = Nothing
        Me.chkDeduction.Text = "Deduction"
        Me.chkDeduction.Visible = False
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(248, 26)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(65, 16)
        Me.RadLabel15.TabIndex = 21
        Me.RadLabel15.Text = "Ref Doc No"
        '
        'ChkSecurity
        '
        Me.ChkSecurity.Location = New System.Drawing.Point(415, 64)
        Me.ChkSecurity.MyLinkLable1 = Nothing
        Me.ChkSecurity.MyLinkLable2 = Nothing
        Me.ChkSecurity.Name = "ChkSecurity"
        Me.ChkSecurity.Size = New System.Drawing.Size(60, 18)
        Me.ChkSecurity.TabIndex = 43
        Me.ChkSecurity.Tag1 = Nothing
        Me.ChkSecurity.Text = "Security"
        Me.ChkSecurity.Visible = False
        '
        'txtRefDocNo
        '
        Me.txtRefDocNo.CalculationExpression = Nothing
        Me.txtRefDocNo.FieldCode = Nothing
        Me.txtRefDocNo.FieldDesc = Nothing
        Me.txtRefDocNo.FieldMaxLength = 0
        Me.txtRefDocNo.FieldName = Nothing
        Me.txtRefDocNo.isCalculatedField = False
        Me.txtRefDocNo.IsSourceFromTable = False
        Me.txtRefDocNo.IsSourceFromValueList = False
        Me.txtRefDocNo.IsUnique = False
        Me.txtRefDocNo.Location = New System.Drawing.Point(321, 25)
        Me.txtRefDocNo.MendatroryField = False
        Me.txtRefDocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDocNo.MyLinkLable1 = Me.RadLabel15
        Me.txtRefDocNo.MyLinkLable2 = Nothing
        Me.txtRefDocNo.MyReadOnly = False
        Me.txtRefDocNo.MyShowMasterFormButton = False
        Me.txtRefDocNo.Name = "txtRefDocNo"
        Me.txtRefDocNo.ReferenceFieldDesc = Nothing
        Me.txtRefDocNo.ReferenceFieldName = Nothing
        Me.txtRefDocNo.ReferenceTableName = Nothing
        Me.txtRefDocNo.Size = New System.Drawing.Size(326, 18)
        Me.txtRefDocNo.TabIndex = 17
        Me.txtRefDocNo.Value = ""
        '
        'txtACSet
        '
        Me.txtACSet.CalculationExpression = Nothing
        Me.txtACSet.Enabled = False
        Me.txtACSet.FieldCode = Nothing
        Me.txtACSet.FieldDesc = Nothing
        Me.txtACSet.FieldMaxLength = 0
        Me.txtACSet.FieldName = Nothing
        Me.txtACSet.isCalculatedField = False
        Me.txtACSet.IsSourceFromTable = False
        Me.txtACSet.IsSourceFromValueList = False
        Me.txtACSet.IsUnique = False
        Me.txtACSet.Location = New System.Drawing.Point(140, 58)
        Me.txtACSet.MendatroryField = False
        Me.txtACSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtACSet.MyLinkLable1 = Me.RadLabel6
        Me.txtACSet.MyLinkLable2 = Nothing
        Me.txtACSet.MyReadOnly = False
        Me.txtACSet.MyShowMasterFormButton = False
        Me.txtACSet.Name = "txtACSet"
        Me.txtACSet.ReferenceFieldDesc = Nothing
        Me.txtACSet.ReferenceFieldName = Nothing
        Me.txtACSet.ReferenceTableName = Nothing
        Me.txtACSet.Size = New System.Drawing.Size(121, 20)
        Me.txtACSet.TabIndex = 5
        Me.txtACSet.Value = ""
        Me.txtACSet.Visible = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(68, 58)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(67, 16)
        Me.RadLabel6.TabIndex = 30
        Me.RadLabel6.Text = "Account Set"
        Me.RadLabel6.Visible = False
        '
        'chkQuickMode
        '
        Me.chkQuickMode.Location = New System.Drawing.Point(332, 49)
        Me.chkQuickMode.MyLinkLable1 = Nothing
        Me.chkQuickMode.MyLinkLable2 = Nothing
        Me.chkQuickMode.Name = "chkQuickMode"
        Me.chkQuickMode.Size = New System.Drawing.Size(81, 18)
        Me.chkQuickMode.TabIndex = 15
        Me.chkQuickMode.Tag1 = Nothing
        Me.chkQuickMode.Text = "Quick Mode"
        Me.chkQuickMode.Visible = False
        '
        'chkProvision
        '
        Me.chkProvision.Location = New System.Drawing.Point(415, 48)
        Me.chkProvision.MyLinkLable1 = Nothing
        Me.chkProvision.MyLinkLable2 = Nothing
        Me.chkProvision.Name = "chkProvision"
        Me.chkProvision.Size = New System.Drawing.Size(66, 18)
        Me.chkProvision.TabIndex = 16
        Me.chkProvision.Tag1 = Nothing
        Me.chkProvision.Text = "Provision"
        Me.chkProvision.Visible = False
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(90, 24)
        Me.txtlocation.MendatroryField = True
        Me.txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.MyLinkLable1 = Nothing
        Me.txtlocation.MyLinkLable2 = Nothing
        Me.txtlocation.MyReadOnly = False
        Me.txtlocation.MyShowMasterFormButton = False
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(143, 20)
        Me.txtlocation.TabIndex = 4
        Me.txtlocation.Value = ""
        '
        'RadLabel18
        '
        Me.RadLabel18.Location = New System.Drawing.Point(0, 25)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(49, 18)
        Me.RadLabel18.TabIndex = 31
        Me.RadLabel18.Text = "Location"
        '
        'MyLabel1
        '
        Me.MyLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel1.Location = New System.Drawing.Point(608, 358)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(318, 16)
        Me.MyLabel1.TabIndex = 23
        Me.MyLabel1.Text = "Double click on Tax Amount Column To Set Item wise Tax"
        Me.MyLabel1.Visible = False
        '
        'btnDrillDown
        '
        Me.btnDrillDown.Image = CType(resources.GetObject("btnDrillDown.Image"), System.Drawing.Image)
        Me.btnDrillDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDrillDown.Location = New System.Drawing.Point(357, 1)
        Me.btnDrillDown.Name = "btnDrillDown"
        Me.btnDrillDown.Size = New System.Drawing.Size(20, 20)
        Me.btnDrillDown.TabIndex = 2
        '
        'TxtVendorNo
        '
        Me.TxtVendorNo.CalculationExpression = Nothing
        Me.TxtVendorNo.FieldCode = Nothing
        Me.TxtVendorNo.FieldDesc = Nothing
        Me.TxtVendorNo.FieldMaxLength = 0
        Me.TxtVendorNo.FieldName = Nothing
        Me.TxtVendorNo.isCalculatedField = False
        Me.TxtVendorNo.IsSourceFromTable = False
        Me.TxtVendorNo.IsSourceFromValueList = False
        Me.TxtVendorNo.IsUnique = False
        Me.TxtVendorNo.Location = New System.Drawing.Point(90, 47)
        Me.TxtVendorNo.MendatroryField = True
        Me.TxtVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.TxtVendorNo.MyLinkLable2 = Me.lblVendorName
        Me.TxtVendorNo.MyReadOnly = False
        Me.TxtVendorNo.MyShowMasterFormButton = False
        Me.TxtVendorNo.Name = "TxtVendorNo"
        Me.TxtVendorNo.ReferenceFieldDesc = Nothing
        Me.TxtVendorNo.ReferenceFieldName = Nothing
        Me.TxtVendorNo.ReferenceTableName = Nothing
        Me.TxtVendorNo.Size = New System.Drawing.Size(143, 18)
        Me.TxtVendorNo.TabIndex = 6
        Me.TxtVendorNo.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(0, 48)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 27
        Me.RadLabel2.Text = "Vendor No"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendorName.Location = New System.Drawing.Point(239, 47)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(410, 18)
        Me.lblVendorName.TabIndex = 7
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(727, 1)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(120, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 34
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(661, 79)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(109, 16)
        Me.RadLabel13.TabIndex = 23
        Me.RadLabel13.Text = "Vendor Invoice Date"
        Me.RadLabel13.Visible = False
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(235, 91)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel12.TabIndex = 19
        Me.RadLabel12.Text = "Document Amount"
        '
        'lblTotRAmt1
        '
        Me.lblTotRAmt1.AutoSize = False
        Me.lblTotRAmt1.BackColor = System.Drawing.Color.Transparent
        Me.lblTotRAmt1.BorderVisible = True
        Me.lblTotRAmt1.FieldName = Nothing
        Me.lblTotRAmt1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt1.Location = New System.Drawing.Point(335, 90)
        Me.lblTotRAmt1.Name = "lblTotRAmt1"
        Me.lblTotRAmt1.Size = New System.Drawing.Size(102, 18)
        Me.lblTotRAmt1.TabIndex = 14
        Me.lblTotRAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(2, 163)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(919, 194)
        Me.RadGroupBox2.TabIndex = 19
        Me.RadGroupBox2.Text = "Details"
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
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(899, 164)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(661, 58)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel3.TabIndex = 26
        Me.RadLabel3.Text = "Vendor Invoice No"
        Me.RadLabel3.Visible = False
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(441, 140)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(53, 16)
        Me.RadLabel8.TabIndex = 29
        Me.RadLabel8.Text = "Order No"
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(0, 91)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(40, 16)
        Me.RadLabel7.TabIndex = 24
        Me.RadLabel7.Text = "PO No"
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(235, 70)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(86, 16)
        Me.RadLabel5.TabIndex = 20
        Me.RadLabel5.Text = "Document Type"
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(655, 3)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 3
        Me.chkOnHold.Text = "On Hold"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(0, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(75, 16)
        Me.RadLabel1.TabIndex = 32
        Me.RadLabel1.Text = "Document No"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(335, 1)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(91, 1)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(242, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.TabStop = False
        Me.txtDocNo.Value = ""
        '
        'txtVendorInvDatre
        '
        Me.txtVendorInvDatre.CalculationExpression = Nothing
        Me.txtVendorInvDatre.CustomFormat = "dd/MM/yyyy"
        Me.txtVendorInvDatre.FieldCode = Nothing
        Me.txtVendorInvDatre.FieldDesc = Nothing
        Me.txtVendorInvDatre.FieldMaxLength = 0
        Me.txtVendorInvDatre.FieldName = Nothing
        Me.txtVendorInvDatre.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorInvDatre.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtVendorInvDatre.isCalculatedField = False
        Me.txtVendorInvDatre.IsSourceFromTable = False
        Me.txtVendorInvDatre.IsSourceFromValueList = False
        Me.txtVendorInvDatre.IsUnique = False
        Me.txtVendorInvDatre.Location = New System.Drawing.Point(778, 78)
        Me.txtVendorInvDatre.MendatroryField = False
        Me.txtVendorInvDatre.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVendorInvDatre.MyLinkLable1 = Me.RadLabel13
        Me.txtVendorInvDatre.MyLinkLable2 = Nothing
        Me.txtVendorInvDatre.Name = "txtVendorInvDatre"
        Me.txtVendorInvDatre.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtVendorInvDatre.ReferenceFieldDesc = Nothing
        Me.txtVendorInvDatre.ReferenceFieldName = Nothing
        Me.txtVendorInvDatre.ReferenceTableName = Nothing
        Me.txtVendorInvDatre.Size = New System.Drawing.Size(114, 18)
        Me.txtVendorInvDatre.TabIndex = 13
        Me.txtVendorInvDatre.TabStop = False
        Me.txtVendorInvDatre.Text = "13/06/2011"
        Me.txtVendorInvDatre.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.txtVendorInvDatre.Visible = False
        '
        'txtVendorInvoiceNo
        '
        Me.txtVendorInvoiceNo.CalculationExpression = Nothing
        Me.txtVendorInvoiceNo.FieldCode = Nothing
        Me.txtVendorInvoiceNo.FieldDesc = Nothing
        Me.txtVendorInvoiceNo.FieldMaxLength = 0
        Me.txtVendorInvoiceNo.FieldName = Nothing
        Me.txtVendorInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorInvoiceNo.isCalculatedField = False
        Me.txtVendorInvoiceNo.IsSourceFromTable = False
        Me.txtVendorInvoiceNo.IsSourceFromValueList = False
        Me.txtVendorInvoiceNo.IsUnique = False
        Me.txtVendorInvoiceNo.Location = New System.Drawing.Point(778, 57)
        Me.txtVendorInvoiceNo.MaxLength = 30
        Me.txtVendorInvoiceNo.MendatroryField = True
        Me.txtVendorInvoiceNo.MyLinkLable1 = Me.RadLabel3
        Me.txtVendorInvoiceNo.MyLinkLable2 = Nothing
        Me.txtVendorInvoiceNo.Name = "txtVendorInvoiceNo"
        Me.txtVendorInvoiceNo.ReferenceFieldDesc = Nothing
        Me.txtVendorInvoiceNo.ReferenceFieldName = Nothing
        Me.txtVendorInvoiceNo.ReferenceTableName = Nothing
        Me.txtVendorInvoiceNo.Size = New System.Drawing.Size(114, 18)
        Me.txtVendorInvoiceNo.TabIndex = 10
        Me.txtVendorInvoiceNo.Visible = False
        '
        'txtOrderNo
        '
        Me.txtOrderNo.CalculationExpression = Nothing
        Me.txtOrderNo.FieldCode = Nothing
        Me.txtOrderNo.FieldDesc = Nothing
        Me.txtOrderNo.FieldMaxLength = 0
        Me.txtOrderNo.FieldName = Nothing
        Me.txtOrderNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNo.isCalculatedField = False
        Me.txtOrderNo.IsSourceFromTable = False
        Me.txtOrderNo.IsSourceFromValueList = False
        Me.txtOrderNo.IsUnique = False
        Me.txtOrderNo.Location = New System.Drawing.Point(541, 139)
        Me.txtOrderNo.MaxLength = 30
        Me.txtOrderNo.MendatroryField = False
        Me.txtOrderNo.MyLinkLable1 = Me.RadLabel8
        Me.txtOrderNo.MyLinkLable2 = Nothing
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.ReferenceFieldDesc = Nothing
        Me.txtOrderNo.ReferenceFieldName = Nothing
        Me.txtOrderNo.ReferenceTableName = Nothing
        Me.txtOrderNo.Size = New System.Drawing.Size(108, 18)
        Me.txtOrderNo.TabIndex = 8
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
        Me.txtPONo.Location = New System.Drawing.Point(90, 90)
        Me.txtPONo.MaxLength = 30
        Me.txtPONo.MendatroryField = False
        Me.txtPONo.MyLinkLable1 = Me.RadLabel7
        Me.txtPONo.MyLinkLable2 = Nothing
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReferenceFieldDesc = Nothing
        Me.txtPONo.ReferenceFieldName = Nothing
        Me.txtPONo.ReferenceTableName = Nothing
        Me.txtPONo.Size = New System.Drawing.Size(143, 18)
        Me.txtPONo.TabIndex = 12
        '
        'cboDocType
        '
        Me.cboDocType.AllowDrop = True
        Me.cboDocType.AutoCompleteDisplayMember = Nothing
        Me.cboDocType.AutoCompleteValueMember = Nothing
        Me.cboDocType.CalculationExpression = Nothing
        Me.cboDocType.DropDownAnimationEnabled = True
        Me.cboDocType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDocType.FieldCode = Nothing
        Me.cboDocType.FieldDesc = Nothing
        Me.cboDocType.FieldMaxLength = 0
        Me.cboDocType.FieldName = Nothing
        Me.cboDocType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocType.isCalculatedField = False
        Me.cboDocType.IsSourceFromTable = False
        Me.cboDocType.IsSourceFromValueList = False
        Me.cboDocType.IsUnique = False
        Me.cboDocType.Location = New System.Drawing.Point(335, 69)
        Me.cboDocType.MendatroryField = False
        Me.cboDocType.MyLinkLable1 = Me.RadLabel5
        Me.cboDocType.MyLinkLable2 = Nothing
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.ReferenceFieldDesc = Nothing
        Me.cboDocType.ReferenceFieldName = Nothing
        Me.cboDocType.ReferenceTableName = Nothing
        Me.cboDocType.Size = New System.Drawing.Size(102, 18)
        Me.cboDocType.TabIndex = 11
        Me.cboDocType.Text = "RadDropDownList1"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(90, 69)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(143, 18)
        Me.txtDate.TabIndex = 9
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtDesc.Location = New System.Drawing.Point(383, 2)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Nothing
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(266, 18)
        Me.txtDesc.TabIndex = 2
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(924, 372)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(553, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tax Calculation Type"
        '
        'rbtnTaxCalManual
        '
        Me.rbtnTaxCalManual.Location = New System.Drawing.Point(89, 13)
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
        Me.txtTaxGroup.Location = New System.Drawing.Point(69, 5)
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
        Me.RadLabel11.Location = New System.Drawing.Point(3, 7)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 3
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(225, 5)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 4
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(755, 268)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(162, 16)
        Me.RadLabel10.TabIndex = 4
        Me.RadLabel10.Text = "Double click To Change Rate"
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
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 281)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(924, 87)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "Terms"
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
        Me.txtTermCode.Location = New System.Drawing.Point(70, 24)
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
        Me.txtTermCode.Size = New System.Drawing.Size(143, 18)
        Me.txtTermCode.TabIndex = 0
        Me.txtTermCode.Value = ""
        '
        'RadLabel16
        '
        Me.RadLabel16.FieldName = Nothing
        Me.RadLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel16.Location = New System.Drawing.Point(6, 26)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel16.TabIndex = 3
        Me.RadLabel16.Text = "Term Code"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(225, 24)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(321, 20)
        Me.lblTermName.TabIndex = 4
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
        Me.txtDueDate.Size = New System.Drawing.Size(116, 18)
        Me.txtDueDate.TabIndex = 1
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
        Me.RadLabel17.TabIndex = 2
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
        Me.gv2.Location = New System.Drawing.Point(2, 39)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.MyStopExport = False
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(919, 226)
        Me.gv2.TabIndex = 2
        Me.gv2.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(116.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(924, 372)
        Me.RadPageViewPage3.Text = "Additional Chrarges"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gvAC)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadLabel31)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblAddCharges)
        Me.SplitContainer2.Size = New System.Drawing.Size(924, 372)
        Me.SplitContainer2.SplitterDistance = 336
        Me.SplitContainer2.TabIndex = 1
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
        Me.gvAC.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvAC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvAC.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvAC.MyStopExport = False
        Me.gvAC.Name = "gvAC"
        Me.gvAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvAC.ShowGroupPanel = False
        Me.gvAC.ShowHeaderCellButtons = True
        Me.gvAC.Size = New System.Drawing.Size(924, 336)
        Me.gvAC.TabIndex = 0
        Me.gvAC.TabStop = False
        '
        'RadLabel31
        '
        Me.RadLabel31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel31.FieldName = Nothing
        Me.RadLabel31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel31.Location = New System.Drawing.Point(679, 3)
        Me.RadLabel31.Name = "RadLabel31"
        Me.RadLabel31.Size = New System.Drawing.Size(130, 16)
        Me.RadLabel31.TabIndex = 1
        Me.RadLabel31.Text = "Total Additional Charges"
        '
        'lblAddCharges
        '
        Me.lblAddCharges.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddCharges.AutoSize = False
        Me.lblAddCharges.BorderVisible = True
        Me.lblAddCharges.FieldName = Nothing
        Me.lblAddCharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges.Location = New System.Drawing.Point(811, 3)
        Me.lblAddCharges.Name = "lblAddCharges"
        Me.lblAddCharges.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges.TabIndex = 0
        Me.lblAddCharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(924, 372)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(924, 372)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(924, 372)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(924, 372)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.pnlCurrConv)
        Me.RadPageViewPage4.Controls.Add(Me.btnReverse)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage4.Controls.Add(Me.lblLandedAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotEmptyAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel32)
        Me.RadPageViewPage4.Controls.Add(Me.lblAddCharges1)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(924, 372)
        Me.RadPageViewPage4.Text = "Total"
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
        Me.pnlCurrConv.Location = New System.Drawing.Point(31, 6)
        Me.pnlCurrConv.Name = "pnlCurrConv"
        Me.pnlCurrConv.Size = New System.Drawing.Size(736, 38)
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
        Me.txtConversionRate.Location = New System.Drawing.Point(353, 8)
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
        Me.txtCurrencyCode.FieldCode = Nothing
        Me.txtCurrencyCode.FieldDesc = Nothing
        Me.txtCurrencyCode.FieldMaxLength = 0
        Me.txtCurrencyCode.FieldName = Nothing
        Me.txtCurrencyCode.isCalculatedField = False
        Me.txtCurrencyCode.IsSourceFromTable = False
        Me.txtCurrencyCode.IsSourceFromValueList = False
        Me.txtCurrencyCode.IsUnique = False
        Me.txtCurrencyCode.Location = New System.Drawing.Point(80, 9)
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
        Me.txtCurrencyCode.Size = New System.Drawing.Size(170, 24)
        Me.txtCurrencyCode.TabIndex = 0
        Me.txtCurrencyCode.Value = ""
        '
        'lblEffectiveFrom
        '
        Me.lblEffectiveFrom.FieldName = Nothing
        Me.lblEffectiveFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEffectiveFrom.Location = New System.Drawing.Point(483, 12)
        Me.lblEffectiveFrom.Name = "lblEffectiveFrom"
        Me.lblEffectiveFrom.Size = New System.Drawing.Size(88, 16)
        Me.lblEffectiveFrom.TabIndex = 3
        Me.lblEffectiveFrom.Text = "Applicable From"
        '
        'txtApplicableFrom
        '
        Me.txtApplicableFrom.AutoSize = False
        Me.txtApplicableFrom.BorderVisible = True
        Me.txtApplicableFrom.FieldName = Nothing
        Me.txtApplicableFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicableFrom.Location = New System.Drawing.Point(579, 10)
        Me.txtApplicableFrom.Name = "txtApplicableFrom"
        Me.txtApplicableFrom.Size = New System.Drawing.Size(152, 18)
        Me.txtApplicableFrom.TabIndex = 2
        Me.txtApplicableFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency
        '
        Me.lblCurrency.FieldName = Nothing
        Me.lblCurrency.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(22, 12)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 16)
        Me.lblCurrency.TabIndex = 5
        Me.lblCurrency.Text = "Currency"
        '
        'lblConvRate
        '
        Me.lblConvRate.FieldName = Nothing
        Me.lblConvRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvRate.Location = New System.Drawing.Point(256, 12)
        Me.lblConvRate.Name = "lblConvRate"
        Me.lblConvRate.Size = New System.Drawing.Size(91, 16)
        Me.lblConvRate.TabIndex = 4
        Me.lblConvRate.Text = "Conversion Rate"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(563, 51)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(267, 22)
        Me.btnReverse.TabIndex = 1
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(121, 207)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(96, 16)
        Me.MyLabel3.TabIndex = 14
        Me.MyLabel3.Text = "+ Landed Amount"
        '
        'lblLandedAmt
        '
        Me.lblLandedAmt.AutoSize = False
        Me.lblLandedAmt.BorderVisible = True
        Me.lblLandedAmt.FieldName = Nothing
        Me.lblLandedAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLandedAmt.Location = New System.Drawing.Point(220, 206)
        Me.lblLandedAmt.Name = "lblLandedAmt"
        Me.lblLandedAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblLandedAmt.TabIndex = 15
        Me.lblLandedAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(127, 181)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel2.TabIndex = 12
        Me.MyLabel2.Text = "+ Empty Amount"
        '
        'lblTotEmptyAmt
        '
        Me.lblTotEmptyAmt.AutoSize = False
        Me.lblTotEmptyAmt.BorderVisible = True
        Me.lblTotEmptyAmt.FieldName = Nothing
        Me.lblTotEmptyAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotEmptyAmt.Location = New System.Drawing.Point(220, 180)
        Me.lblTotEmptyAmt.Name = "lblTotEmptyAmt"
        Me.lblTotEmptyAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotEmptyAmt.TabIndex = 13
        Me.lblTotEmptyAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel32
        '
        Me.RadLabel32.FieldName = Nothing
        Me.RadLabel32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel32.Location = New System.Drawing.Point(77, 155)
        Me.RadLabel32.Name = "RadLabel32"
        Me.RadLabel32.Size = New System.Drawing.Size(140, 16)
        Me.RadLabel32.TabIndex = 10
        Me.RadLabel32.Text = "+ Total Additional Charges"
        '
        'lblAddCharges1
        '
        Me.lblAddCharges1.AutoSize = False
        Me.lblAddCharges1.BorderVisible = True
        Me.lblAddCharges1.FieldName = Nothing
        Me.lblAddCharges1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddCharges1.Location = New System.Drawing.Point(220, 154)
        Me.lblAddCharges1.Name = "lblAddCharges1"
        Me.lblAddCharges1.Size = New System.Drawing.Size(110, 18)
        Me.lblAddCharges1.TabIndex = 11
        Me.lblAddCharges1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(97, 103)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 6
        Me.RadLabel9.Text = "Amount After Discount"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(117, 233)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(100, 16)
        Me.RadLabel27.TabIndex = 16
        Me.RadLabel27.Text = "Document Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(220, 232)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 17
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(140, 129)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 8
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(220, 128)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 9
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(220, 102)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 7
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(220, 76)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 5
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(220, 50)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 3
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(118, 77)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 4
        Me.RadLabel22.Text = "- Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(31, 51)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 2
        Me.RadLabel19.Text = "Document Amount without Discount"
        '
        'BtnProcess
        '
        Me.BtnProcess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnProcess.Location = New System.Drawing.Point(473, 6)
        Me.BtnProcess.Name = "BtnProcess"
        Me.BtnProcess.Size = New System.Drawing.Size(69, 22)
        Me.BtnProcess.TabIndex = 8
        Me.BtnProcess.Text = "Process"
        '
        'btnsetting
        '
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSend, Me.btnSendForApproval})
        Me.btnsetting.Location = New System.Drawing.Point(380, 6)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(87, 22)
        Me.btnsetting.TabIndex = 7
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'btnSend
        '
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Text = "Send Email/SMS"
        '
        'btnSendForApproval
        '
        Me.btnSendForApproval.Name = "btnSendForApproval"
        Me.btnSendForApproval.Text = "Send For Approval"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(305, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(155, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(80, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnViewTDSDetails
        '
        Me.btnViewTDSDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewTDSDetails.Location = New System.Drawing.Point(230, 6)
        Me.btnViewTDSDetails.Name = "btnViewTDSDetails"
        Me.btnViewTDSDetails.Size = New System.Drawing.Size(69, 22)
        Me.btnViewTDSDetails.TabIndex = 3
        Me.btnViewTDSDetails.Text = " TDS Detail"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(871, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(945, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.RadMenuItem8, Me.RadMenuItem9})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem4, Me.mbtnExportApTransaction, Me.rmiExportDrNote})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Opening Balance"
        Me.RadMenuItem4.AccessibleName = "Opening Balance"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Opening Balance (Blank Sheet)"
        '
        'mbtnExportApTransaction
        '
        Me.mbtnExportApTransaction.AccessibleDescription = "Export AP Transaction Blank"
        Me.mbtnExportApTransaction.AccessibleName = "Export AP Transaction Blank"
        Me.mbtnExportApTransaction.Name = "mbtnExportApTransaction"
        Me.mbtnExportApTransaction.Text = "AP Transactions Blank"
        '
        'rmiExportDrNote
        '
        Me.rmiExportDrNote.Name = "rmiExportDrNote"
        Me.rmiExportDrNote.Text = "Export [Debit Note]"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem5, Me.RadMenuItem6, Me.RadMenuItem7, Me.mbtnImportApTransaction, Me.rmiImportDrNote})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "Opening Balance"
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "Credit Note"
        '
        'RadMenuItem7
        '
        Me.RadMenuItem7.Name = "RadMenuItem7"
        Me.RadMenuItem7.Text = "Debit Note"
        '
        'mbtnImportApTransaction
        '
        Me.mbtnImportApTransaction.Name = "mbtnImportApTransaction"
        Me.mbtnImportApTransaction.Text = "AP Transactions"
        '
        'rmiImportDrNote
        '
        Me.rmiImportDrNote.Name = "rmiImportDrNote"
        Me.rmiImportDrNote.Text = "Import [Debit Note]"
        '
        'RadMenuItem8
        '
        Me.RadMenuItem8.Name = "RadMenuItem8"
        Me.RadMenuItem8.Text = "Save Layout"
        '
        'RadMenuItem9
        '
        Me.RadMenuItem9.Name = "RadMenuItem9"
        Me.RadMenuItem9.Text = "Delete Layout"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(945, 456)
        Me.Panel1.TabIndex = 3
        '
        'FrmRecurringPayableInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 476)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "FrmRecurringPayableInvoice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "AP Invoice Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.TxtExpirationAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblScheduler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboExpiration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpExpirationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProvision.ResumeLayout(False)
        Me.grpProvision.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProvAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromProv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InvDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPCJ.ResumeLayout(False)
        Me.pnlPCJ.PerformLayout()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProRated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRefType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkQuickMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProvision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDrillDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorInvDatre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPONo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gvAC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        Me.pnlCurrConv.ResumeLayout(False)
        Me.pnlCurrConv.PerformLayout()
        CType(Me.txtConversionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEffectiveFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicableFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblConvRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLandedAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotEmptyAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddCharges1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtOrderNo As common.Controls.MyTextBox
    Friend WithEvents txtPONo As common.Controls.MyTextBox
    Friend WithEvents cboDocType As common.Controls.MyComboBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVendorInvoiceNo As common.Controls.MyTextBox
    Friend WithEvents txtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnViewTDSDetails As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVendorInvDatre As common.Controls.MyDateTimePicker
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmbRefType As common.Controls.MyComboBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtRefDocNo As common.UserControls.txtFinder
    Friend WithEvents TxtVendorNo As common.UserControls.txtFinder
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents txtACSet As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvAC As common.UserControls.MyRadGridView
    Friend WithEvents btnDrillDown As Telerik.WinControls.UI.RadButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents lblTermName As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt1 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents RadLabel31 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges As common.Controls.MyLabel
    Friend WithEvents RadLabel32 As common.Controls.MyLabel
    Friend WithEvents lblAddCharges1 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents chkQuickMode As common.Controls.MyCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem7 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtlocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel18 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblTotEmptyAmt As common.Controls.MyLabel
    Friend WithEvents mbtnExportApTransaction As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mbtnImportApTransaction As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem8 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem9 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblLandedAmt As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents pnlCurrConv As System.Windows.Forms.Panel
    Friend WithEvents txtCurrencyCode As common.UserControls.txtFinder
    Friend WithEvents lblEffectiveFrom As common.Controls.MyLabel
    Friend WithEvents txtApplicableFrom As common.Controls.MyLabel
    Friend WithEvents lblCurrency As common.Controls.MyLabel
    Friend WithEvents lblConvRate As common.Controls.MyLabel
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents txtConversionRate As common.MyNumBox
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents pnlPCJ As System.Windows.Forms.Panel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents fndProject As common.UserControls.txtFinder
    Friend WithEvents lblProject As common.Controls.MyLabel
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnSendForApproval As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents LblLocDesp As common.Controls.MyLabel
    Friend WithEvents grpProvision As System.Windows.Forms.GroupBox
    Friend WithEvents chkProvision As common.Controls.MyCheckBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents dtpToProv As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents dtpFromProv As common.Controls.MyDateTimePicker
    Friend WithEvents btnProvSelect As System.Windows.Forms.LinkLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtProvAmt As common.Controls.MyTextBox
    Friend WithEvents btlShowProvision As System.Windows.Forms.LinkLabel
    Friend WithEvents ChkSecurity As common.Controls.MyCheckBox
    Friend WithEvents chkDeduction As common.Controls.MyCheckBox
    Friend WithEvents chkProRated As common.Controls.MyCheckBox
    Friend WithEvents rmiExportDrNote As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImportDrNote As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents LblScheduler As common.Controls.MyLabel
    Friend WithEvents FndScheduler As common.UserControls.txtFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents CboExpiration As common.Controls.MyComboBox
    Friend WithEvents DtpExpirationDate As common.Controls.MyDateTimePicker
    Friend WithEvents InvDate As common.Controls.MyLabel
    Friend WithEvents TxtExpirationAmount As common.Controls.MyTextBox
    Friend WithEvents BtnProcess As Telerik.WinControls.UI.RadButton
End Class

