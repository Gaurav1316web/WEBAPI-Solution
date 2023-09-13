<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScrapSale
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScrapSale))
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition5 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn2 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkBuyBack = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblTransporter = New common.Controls.MyLabel()
        Me.txtTransporter_Code = New common.UserControls.txtFinder()
        Me.txtTransporter_desc = New common.Controls.MyLabel()
        Me.txtFreightDistance = New common.MyNumBox()
        Me.MyLabel59 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtGWeighmentNo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.chkTaxable = New common.Controls.MyCheckBox()
        Me.chkCashSale = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkScrapSale = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkOnHold = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkinvoice = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblSecondryInvNo = New common.Controls.MyLabel()
        Me.UcItemBalance1 = New XpertERPEngine.ucItemBalance()
        Me.dtppost = New common.Controls.MyDateTimePicker()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.txtcustdesc = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel14 = New common.Controls.MyLabel()
        Me.fndcustNo = New common.UserControls.txtFinder()
        Me.dtpshipment = New common.Controls.MyDateTimePicker()
        Me.lblInvoiceType = New common.Controls.MyLabel()
        Me.ddlInvoiceType = New common.Controls.MyComboBox()
        Me.txtVatInvNo = New common.Controls.MyLabel()
        Me.lblDocAmount = New common.Controls.MyLabel()
        Me.txtVehicleDesc = New common.Controls.MyTextBox()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtVehicleCode = New common.UserControls.txtFinder()
        Me.txtnrg = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.lblInvoiceNo = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkInterBranch = New common.Controls.MyCheckBox()
        Me.chkExcisable = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtscrapinvoice = New common.Controls.MyTextBox()
        Me.RadLabel12 = New common.Controls.MyLabel()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.dtpexp = New common.Controls.MyDateTimePicker()
        Me.RadLabel13 = New common.Controls.MyLabel()
        Me.txtlocation = New common.Controls.MyTextBox()
        Me.RadLabel21 = New common.Controls.MyLabel()
        Me.txtref = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel18 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.fndShipToLocation = New common.UserControls.txtFinder()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.UsLock1 = New common.usLock()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.txtponumber = New common.Controls.MyTextBox()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvadd = New common.UserControls.MyRadGridView()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.txtaddamt = New common.Controls.MyTextBox()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ERP.ucCustomFields()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtTCSTaxRate = New common.MyNumBox()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.lblActualTCSTaxBaseAmt = New common.Controls.MyLabel()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.txttcstaxbaseamount = New common.MyNumBox()
        Me.btnCancel = New Telerik.WinControls.UI.RadButton()
        Me.lblRound_Off = New common.Controls.MyLabel()
        Me.txtRoundOff = New common.Controls.MyLabel()
        Me.lblNetWeight = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblGrossWeight = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lbldocamt = New common.Controls.MyLabel()
        Me.lbladdc = New common.Controls.MyLabel()
        Me.lbladdcharges = New common.Controls.MyLabel()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.RadLabel27 = New common.Controls.MyLabel()
        Me.lblTotRAmt = New common.Controls.MyLabel()
        Me.lblTaxAmt = New common.Controls.MyLabel()
        Me.lblAmtAfterDiscount = New common.Controls.MyLabel()
        Me.lblDiscountAmt = New common.Controls.MyLabel()
        Me.lblAmtWithDiscount = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.RadLabel25 = New common.Controls.MyLabel()
        Me.RadLabel22 = New common.Controls.MyLabel()
        Me.RadLabel19 = New common.Controls.MyLabel()
        '<<<<<<< HEAD
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtEWayBillDate = New common.Controls.MyDateTimePicker()
        Me.txtEwayValidDate = New common.Controls.MyDateTimePicker()
        Me.txtEWayBillRemarks = New common.Controls.MyTextBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel38 = New common.Controls.MyLabel()
        Me.txtEWayBillNo = New common.Controls.MyTextBox()
        Me.btnEWaybillUpdate = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtAckDate = New common.Controls.MyDateTimePicker()
        Me.EinvoiceBtnUpdate = New Telerik.WinControls.UI.RadButton()
        Me.EInvoiceQrCode = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.EinvoiceAckNo = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.EInvoiceIRNNo = New common.Controls.MyTextBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        '=======
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
'>>>>>>> d1ec586fa3c5d6f2c6903ff02c7d5301aea8f361
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnInvoiceJE = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.Setting = New Telerik.WinControls.UI.RadMenuItem()
        Me.layoutrbtn = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkBuyBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporter_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFreightDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCashSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkScrapSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkinvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSecondryInvNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtppost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcustdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpshipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVatInvNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInterBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtscrapinvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpexp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtref, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtponumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvadd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvadd.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtaddamt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRound_Off, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoundOff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldocamt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbladdc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbladdcharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        '<<<<<<< HEAD
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEWayBillDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEwayValidDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEWayBillRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEWayBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEWaybillUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtAckDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EinvoiceBtnUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EInvoiceQrCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EinvoiceAckNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EInvoiceIRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        '=======
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
'>>>>>>> d1ec586fa3c5d6f2c6903ff02c7d5301aea8f361
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnInvoiceJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1092, 517)
        Me.SplitContainer1.SplitterDistance = 485
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
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage5
        Me.RadPageView1.Size = New System.Drawing.Size(1092, 485)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.TabStop = False
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkBuyBack)
        Me.RadPageViewPage1.Controls.Add(Me.lblTransporter)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporter_Code)
        Me.RadPageViewPage1.Controls.Add(Me.txtFreightDistance)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransporter_desc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel59)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtGWeighmentNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.chkTaxable)
        Me.RadPageViewPage1.Controls.Add(Me.chkCashSale)
        Me.RadPageViewPage1.Controls.Add(Me.chkScrapSale)
        Me.RadPageViewPage1.Controls.Add(Me.chkOnHold)
        Me.RadPageViewPage1.Controls.Add(Me.chkinvoice)
        Me.RadPageViewPage1.Controls.Add(Me.lblSecondryInvNo)
        Me.RadPageViewPage1.Controls.Add(Me.UcItemBalance1)
        Me.RadPageViewPage1.Controls.Add(Me.dtppost)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtcustdesc)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.fndcustNo)
        Me.RadPageViewPage1.Controls.Add(Me.dtpshipment)
        Me.RadPageViewPage1.Controls.Add(Me.lblInvoiceType)
        Me.RadPageViewPage1.Controls.Add(Me.ddlInvoiceType)
        Me.RadPageViewPage1.Controls.Add(Me.txtVatInvNo)
        Me.RadPageViewPage1.Controls.Add(Me.lblDocAmount)
        Me.RadPageViewPage1.Controls.Add(Me.txtVehicleDesc)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.lblVehicleNo)
        Me.RadPageViewPage1.Controls.Add(Me.TxtVehicleCode)
        Me.RadPageViewPage1.Controls.Add(Me.txtnrg)
        Me.RadPageViewPage1.Controls.Add(Me.lblInvoiceNo)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.chkInterBranch)
        Me.RadPageViewPage1.Controls.Add(Me.chkExcisable)
        Me.RadPageViewPage1.Controls.Add(Me.txtscrapinvoice)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel12)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.dtpexp)
        Me.RadPageViewPage1.Controls.Add(Me.txtlocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel21)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtref)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel18)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.fndShipToLocation)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.UsLock1)
        Me.RadPageViewPage1.Controls.Add(Me.txtDocNo)
        Me.RadPageViewPage1.Controls.Add(Me.txtponumber)
        Me.RadPageViewPage1.Controls.Add(Me.btnAddNew)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(64.0!, 26.0!)
        '<<<<<<< HEAD
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        '=======
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 31)
'>>>>>>> d1ec586fa3c5d6f2c6903ff02c7d5301aea8f361
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1071, 439)
        Me.RadPageViewPage1.Text = "Shipment"
        '
        'chkBuyBack
        '
        Me.chkBuyBack.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBuyBack.Location = New System.Drawing.Point(416, 156)
        Me.chkBuyBack.Name = "chkBuyBack"
        Me.chkBuyBack.Size = New System.Drawing.Size(65, 16)
        Me.chkBuyBack.TabIndex = 1432
        Me.chkBuyBack.Text = "BuyBack"
        Me.chkBuyBack.Visible = False
        '
        'lblTransporter
        '
        Me.lblTransporter.FieldName = Nothing
        Me.lblTransporter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransporter.Location = New System.Drawing.Point(6, 94)
        Me.lblTransporter.Name = "lblTransporter"
        Me.lblTransporter.Size = New System.Drawing.Size(65, 16)
        Me.lblTransporter.TabIndex = 1428
        Me.lblTransporter.Text = "Transporter"
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
        Me.txtTransporter_Code.Location = New System.Drawing.Point(81, 92)
        Me.txtTransporter_Code.MendatroryField = False
        Me.txtTransporter_Code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter_Code.MyLinkLable1 = Me.lblTransporter
        Me.txtTransporter_Code.MyLinkLable2 = Me.txtTransporter_desc
        Me.txtTransporter_Code.MyReadOnly = False
        Me.txtTransporter_Code.MyShowMasterFormButton = False
        Me.txtTransporter_Code.Name = "txtTransporter_Code"
        Me.txtTransporter_Code.ReferenceFieldDesc = Nothing
        Me.txtTransporter_Code.ReferenceFieldName = Nothing
        Me.txtTransporter_Code.ReferenceTableName = Nothing
        Me.txtTransporter_Code.Size = New System.Drawing.Size(131, 20)
        Me.txtTransporter_Code.TabIndex = 1426
        Me.txtTransporter_Code.Value = ""
        '
        'txtTransporter_desc
        '
        Me.txtTransporter_desc.AutoSize = False
        Me.txtTransporter_desc.BorderVisible = True
        Me.txtTransporter_desc.FieldName = Nothing
        Me.txtTransporter_desc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter_desc.Location = New System.Drawing.Point(216, 93)
        Me.txtTransporter_desc.Name = "txtTransporter_desc"
        Me.txtTransporter_desc.Size = New System.Drawing.Size(387, 18)
        Me.txtTransporter_desc.TabIndex = 1427
        Me.txtTransporter_desc.TextWrap = False
        '
        'txtFreightDistance
        '
        Me.txtFreightDistance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFreightDistance.CalculationExpression = Nothing
        Me.txtFreightDistance.DecimalPlaces = 0
        Me.txtFreightDistance.FieldCode = Nothing
        Me.txtFreightDistance.FieldDesc = Nothing
        Me.txtFreightDistance.FieldMaxLength = 0
        Me.txtFreightDistance.FieldName = Nothing
        Me.txtFreightDistance.isCalculatedField = False
        Me.txtFreightDistance.IsSourceFromTable = False
        Me.txtFreightDistance.IsSourceFromValueList = False
        Me.txtFreightDistance.IsUnique = False
        Me.txtFreightDistance.Location = New System.Drawing.Point(962, 152)
        Me.txtFreightDistance.MendatroryField = False
        Me.txtFreightDistance.MyLinkLable1 = Nothing
        Me.txtFreightDistance.MyLinkLable2 = Nothing
        Me.txtFreightDistance.Name = "txtFreightDistance"
        Me.txtFreightDistance.ReferenceFieldDesc = Nothing
        Me.txtFreightDistance.ReferenceFieldName = Nothing
        Me.txtFreightDistance.ReferenceTableName = Nothing
        Me.txtFreightDistance.Size = New System.Drawing.Size(58, 20)
        Me.txtFreightDistance.TabIndex = 1473
        Me.txtFreightDistance.Text = "0"
        Me.txtFreightDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFreightDistance.Value = 0R
        '
        'MyLabel59
        '
        Me.MyLabel59.FieldName = Nothing
        Me.MyLabel59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel59.Location = New System.Drawing.Point(868, 154)
        Me.MyLabel59.Name = "MyLabel59"
        Me.MyLabel59.Size = New System.Drawing.Size(89, 16)
        Me.MyLabel59.TabIndex = 1472
        Me.MyLabel59.Text = "Freight Distance"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(606, 5)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel6.TabIndex = 1435
        Me.MyLabel6.Text = "Weighment No"
        '
        'txtGWeighmentNo
        '
        Me.txtGWeighmentNo.CalculationExpression = Nothing
        Me.txtGWeighmentNo.FieldCode = Nothing
        Me.txtGWeighmentNo.FieldDesc = Nothing
        Me.txtGWeighmentNo.FieldMaxLength = 0
        Me.txtGWeighmentNo.FieldName = Nothing
        Me.txtGWeighmentNo.isCalculatedField = False
        Me.txtGWeighmentNo.IsSourceFromTable = False
        Me.txtGWeighmentNo.IsSourceFromValueList = False
        Me.txtGWeighmentNo.IsUnique = False
        Me.txtGWeighmentNo.Location = New System.Drawing.Point(716, 2)
        Me.txtGWeighmentNo.MendatroryField = False
        Me.txtGWeighmentNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGWeighmentNo.MyLinkLable1 = Me.MyLabel6
        Me.txtGWeighmentNo.MyLinkLable2 = Nothing
        Me.txtGWeighmentNo.MyReadOnly = False
        Me.txtGWeighmentNo.MyShowMasterFormButton = False
        Me.txtGWeighmentNo.Name = "txtGWeighmentNo"
        Me.txtGWeighmentNo.ReferenceFieldDesc = Nothing
        Me.txtGWeighmentNo.ReferenceFieldName = Nothing
        Me.txtGWeighmentNo.ReferenceTableName = Nothing
        Me.txtGWeighmentNo.Size = New System.Drawing.Size(146, 22)
        Me.txtGWeighmentNo.TabIndex = 1434
        Me.txtGWeighmentNo.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MyLabel3.Location = New System.Drawing.Point(965, 353)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel3.TabIndex = 1
        Me.MyLabel3.Text = "F5 For Batch Item"
        '
        'chkTaxable
        '
        Me.chkTaxable.Enabled = False
        Me.chkTaxable.Location = New System.Drawing.Point(357, 154)
        Me.chkTaxable.MyLinkLable1 = Nothing
        Me.chkTaxable.MyLinkLable2 = Nothing
        Me.chkTaxable.Name = "chkTaxable"
        Me.chkTaxable.Size = New System.Drawing.Size(58, 18)
        Me.chkTaxable.TabIndex = 1432
        Me.chkTaxable.Tag1 = Nothing
        Me.chkTaxable.Text = "Taxable"
        '
        'chkCashSale
        '
        Me.chkCashSale.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCashSale.Location = New System.Drawing.Point(273, 141)
        Me.chkCashSale.Name = "chkCashSale"
        Me.chkCashSale.Size = New System.Drawing.Size(72, 16)
        Me.chkCashSale.TabIndex = 1431
        Me.chkCashSale.Text = "Cash Sale"
        Me.chkCashSale.Visible = False
        '
        'chkScrapSale
        '
        Me.chkScrapSale.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapSale.Location = New System.Drawing.Point(174, 155)
        Me.chkScrapSale.Name = "chkScrapSale"
        Me.chkScrapSale.Size = New System.Drawing.Size(76, 16)
        Me.chkScrapSale.TabIndex = 17
        Me.chkScrapSale.Text = "Scrap Sale"
        Me.chkScrapSale.Visible = False
        '
        'chkOnHold
        '
        Me.chkOnHold.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnHold.Location = New System.Drawing.Point(82, 155)
        Me.chkOnHold.Name = "chkOnHold"
        Me.chkOnHold.Size = New System.Drawing.Size(62, 16)
        Me.chkOnHold.TabIndex = 16
        Me.chkOnHold.Text = "On Hold"
        '
        'chkinvoice
        '
        Me.chkinvoice.Enabled = False
        Me.chkinvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkinvoice.Location = New System.Drawing.Point(174, 141)
        Me.chkinvoice.Name = "chkinvoice"
        Me.chkinvoice.Size = New System.Drawing.Size(93, 16)
        Me.chkinvoice.TabIndex = 15
        Me.chkinvoice.Text = "Create Invoice"
        '
        'lblSecondryInvNo
        '
        Me.lblSecondryInvNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSecondryInvNo.FieldName = Nothing
        Me.lblSecondryInvNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecondryInvNo.Location = New System.Drawing.Point(713, 428)
        Me.lblSecondryInvNo.Name = "lblSecondryInvNo"
        Me.lblSecondryInvNo.Size = New System.Drawing.Size(62, 16)
        Me.lblSecondryInvNo.TabIndex = 16
        Me.lblSecondryInvNo.Text = "Vat Invoice"
        Me.lblSecondryInvNo.Visible = False
        '
        'UcItemBalance1
        '
        Me.UcItemBalance1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.UcItemBalance1.CommitedQty = False
        Me.UcItemBalance1.CommitedQtyLbl = False
        Me.UcItemBalance1.ItemCode = ""
        Me.UcItemBalance1.ItemMRP = 0R
        Me.UcItemBalance1.ItemName = ""
        Me.UcItemBalance1.Location = New System.Drawing.Point(0, 364)
        Me.UcItemBalance1.LocationCode = ""
        Me.UcItemBalance1.LocationName = ""
        Me.UcItemBalance1.MaximumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.MinimumSize = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.Name = "UcItemBalance1"
        Me.UcItemBalance1.ShowCSADOQty = False
        Me.UcItemBalance1.ShowPOQty = False
        Me.UcItemBalance1.ShowSOQty = False
        Me.UcItemBalance1.Size = New System.Drawing.Size(710, 75)
        Me.UcItemBalance1.TabIndex = 1430
        Me.UcItemBalance1.TabStop = False
        Me.UcItemBalance1.TransDate = New Date(CType(0, Long))
        Me.UcItemBalance1.TransNo = ""
        Me.UcItemBalance1.UOM = ""
        '
        'dtppost
        '
        Me.dtppost.CalculationExpression = Nothing
        Me.dtppost.CustomFormat = "dd/MMM/yyyy  hh:mm tt"
        Me.dtppost.FieldCode = Nothing
        Me.dtppost.FieldDesc = Nothing
        Me.dtppost.FieldMaxLength = 0
        Me.dtppost.FieldName = Nothing
        Me.dtppost.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtppost.isCalculatedField = False
        Me.dtppost.IsSourceFromTable = False
        Me.dtppost.IsSourceFromValueList = False
        Me.dtppost.IsUnique = False
        Me.dtppost.Location = New System.Drawing.Point(716, 47)
        Me.dtppost.MendatroryField = False
        Me.dtppost.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtppost.MyLinkLable1 = Me.RadLabel6
        Me.dtppost.MyLinkLable2 = Nothing
        Me.dtppost.Name = "dtppost"
        Me.dtppost.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtppost.ReferenceFieldDesc = Nothing
        Me.dtppost.ReferenceFieldName = Nothing
        Me.dtppost.ReferenceTableName = Nothing
        Me.dtppost.Size = New System.Drawing.Size(146, 20)
        Me.dtppost.TabIndex = 9
        Me.dtppost.TabStop = False
        Me.dtppost.Text = "05/Aug/2011  05:38 PM"
        Me.dtppost.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        Me.dtppost.Visible = False
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(606, 49)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(85, 16)
        Me.RadLabel6.TabIndex = 24
        Me.RadLabel6.Text = "Document Date"
        Me.RadLabel6.Visible = False
        '
        'txtcustdesc
        '
        Me.txtcustdesc.CalculationExpression = Nothing
        Me.txtcustdesc.FieldCode = Nothing
        Me.txtcustdesc.FieldDesc = Nothing
        Me.txtcustdesc.FieldMaxLength = 0
        Me.txtcustdesc.FieldName = Nothing
        Me.txtcustdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustdesc.isCalculatedField = False
        Me.txtcustdesc.IsSourceFromTable = False
        Me.txtcustdesc.IsSourceFromValueList = False
        Me.txtcustdesc.IsUnique = False
        Me.txtcustdesc.Location = New System.Drawing.Point(210, 48)
        Me.txtcustdesc.MaxLength = 200
        Me.txtcustdesc.MendatroryField = False
        Me.txtcustdesc.MyLinkLable1 = Me.RadLabel2
        Me.txtcustdesc.MyLinkLable2 = Nothing
        Me.txtcustdesc.Name = "txtcustdesc"
        Me.txtcustdesc.ReadOnly = True
        Me.txtcustdesc.ReferenceFieldDesc = Nothing
        Me.txtcustdesc.ReferenceFieldName = Nothing
        Me.txtcustdesc.ReferenceTableName = Nothing
        Me.txtcustdesc.Size = New System.Drawing.Size(394, 18)
        Me.txtcustdesc.TabIndex = 4
        Me.txtcustdesc.TabStop = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(7, 49)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "Customer No"
        '
        'RadLabel14
        '
        Me.RadLabel14.FieldName = Nothing
        Me.RadLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel14.Location = New System.Drawing.Point(436, 5)
        Me.RadLabel14.Name = "RadLabel14"
        Me.RadLabel14.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel14.TabIndex = 18
        Me.RadLabel14.Text = "Date"
        '
        'fndcustNo
        '
        Me.fndcustNo.CalculationExpression = Nothing
        Me.fndcustNo.FieldCode = Nothing
        Me.fndcustNo.FieldDesc = Nothing
        Me.fndcustNo.FieldMaxLength = 0
        Me.fndcustNo.FieldName = Nothing
        Me.fndcustNo.isCalculatedField = False
        Me.fndcustNo.IsSourceFromTable = False
        Me.fndcustNo.IsSourceFromValueList = False
        Me.fndcustNo.IsUnique = False
        Me.fndcustNo.Location = New System.Drawing.Point(82, 48)
        Me.fndcustNo.MendatroryField = True
        Me.fndcustNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndcustNo.MyLinkLable1 = Me.RadLabel2
        Me.fndcustNo.MyLinkLable2 = Nothing
        Me.fndcustNo.MyReadOnly = False
        Me.fndcustNo.MyShowMasterFormButton = False
        Me.fndcustNo.Name = "fndcustNo"
        Me.fndcustNo.ReferenceFieldDesc = Nothing
        Me.fndcustNo.ReferenceFieldName = Nothing
        Me.fndcustNo.ReferenceTableName = Nothing
        Me.fndcustNo.Size = New System.Drawing.Size(126, 19)
        Me.fndcustNo.TabIndex = 4
        Me.fndcustNo.Value = ""
        '
        'dtpshipment
        '
        Me.dtpshipment.CalculationExpression = Nothing
        Me.dtpshipment.CustomFormat = "dd/MM/yyyy  hh:mm tt"
        Me.dtpshipment.FieldCode = Nothing
        Me.dtpshipment.FieldDesc = Nothing
        Me.dtpshipment.FieldMaxLength = 0
        Me.dtpshipment.FieldName = Nothing
        Me.dtpshipment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpshipment.isCalculatedField = False
        Me.dtpshipment.IsSourceFromTable = False
        Me.dtpshipment.IsSourceFromValueList = False
        Me.dtpshipment.IsUnique = False
        Me.dtpshipment.Location = New System.Drawing.Point(472, 3)
        Me.dtpshipment.MendatroryField = False
        Me.dtpshipment.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpshipment.MyLinkLable1 = Me.RadLabel14
        Me.dtpshipment.MyLinkLable2 = Nothing
        Me.dtpshipment.Name = "dtpshipment"
        Me.dtpshipment.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpshipment.ReferenceFieldDesc = Nothing
        Me.dtpshipment.ReferenceFieldName = Nothing
        Me.dtpshipment.ReferenceTableName = Nothing
        Me.dtpshipment.Size = New System.Drawing.Size(132, 20)
        Me.dtpshipment.TabIndex = 3
        Me.dtpshipment.TabStop = False
        Me.dtpshipment.Text = "05/08/2011  05:38 PM"
        Me.dtpshipment.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'lblInvoiceType
        '
        Me.lblInvoiceType.FieldName = Nothing
        Me.lblInvoiceType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceType.Location = New System.Drawing.Point(606, 154)
        Me.lblInvoiceType.Name = "lblInvoiceType"
        Me.lblInvoiceType.Size = New System.Drawing.Size(70, 16)
        Me.lblInvoiceType.TabIndex = 35
        Me.lblInvoiceType.Text = "Invoice Type"
        '
        'ddlInvoiceType
        '
        Me.ddlInvoiceType.AutoCompleteDisplayMember = Nothing
        Me.ddlInvoiceType.AutoCompleteValueMember = Nothing
        Me.ddlInvoiceType.CalculationExpression = Nothing
        Me.ddlInvoiceType.DropDownAnimationEnabled = True
        Me.ddlInvoiceType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlInvoiceType.FieldCode = Nothing
        Me.ddlInvoiceType.FieldDesc = Nothing
        Me.ddlInvoiceType.FieldMaxLength = 0
        Me.ddlInvoiceType.FieldName = Nothing
        Me.ddlInvoiceType.isCalculatedField = False
        Me.ddlInvoiceType.IsSourceFromTable = False
        Me.ddlInvoiceType.IsSourceFromValueList = False
        Me.ddlInvoiceType.IsUnique = False
        Me.ddlInvoiceType.Location = New System.Drawing.Point(716, 152)
        Me.ddlInvoiceType.MendatroryField = True
        Me.ddlInvoiceType.MyLinkLable1 = Nothing
        Me.ddlInvoiceType.MyLinkLable2 = Nothing
        Me.ddlInvoiceType.Name = "ddlInvoiceType"
        Me.ddlInvoiceType.ReferenceFieldDesc = Nothing
        Me.ddlInvoiceType.ReferenceFieldName = Nothing
        Me.ddlInvoiceType.ReferenceTableName = Nothing
        Me.ddlInvoiceType.Size = New System.Drawing.Size(146, 20)
        Me.ddlInvoiceType.TabIndex = 36
        '
        'txtVatInvNo
        '
        Me.txtVatInvNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVatInvNo.AutoSize = False
        Me.txtVatInvNo.BorderVisible = True
        Me.txtVatInvNo.FieldName = Nothing
        Me.txtVatInvNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVatInvNo.Location = New System.Drawing.Point(781, 422)
        Me.txtVatInvNo.Name = "txtVatInvNo"
        Me.txtVatInvNo.Size = New System.Drawing.Size(133, 18)
        Me.txtVatInvNo.TabIndex = 14
        Me.txtVatInvNo.Visible = False
        '
        'lblDocAmount
        '
        Me.lblDocAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDocAmount.AutoSize = False
        Me.lblDocAmount.BorderVisible = True
        Me.lblDocAmount.FieldName = Nothing
        Me.lblDocAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocAmount.Location = New System.Drawing.Point(957, 422)
        Me.lblDocAmount.Name = "lblDocAmount"
        Me.lblDocAmount.Size = New System.Drawing.Size(113, 18)
        Me.lblDocAmount.TabIndex = 14
        Me.lblDocAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVehicleDesc
        '
        Me.txtVehicleDesc.CalculationExpression = Nothing
        Me.txtVehicleDesc.FieldCode = Nothing
        Me.txtVehicleDesc.FieldDesc = Nothing
        Me.txtVehicleDesc.FieldMaxLength = 0
        Me.txtVehicleDesc.FieldName = Nothing
        Me.txtVehicleDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleDesc.isCalculatedField = False
        Me.txtVehicleDesc.IsSourceFromTable = False
        Me.txtVehicleDesc.IsSourceFromValueList = False
        Me.txtVehicleDesc.IsUnique = False
        Me.txtVehicleDesc.Location = New System.Drawing.Point(210, 70)
        Me.txtVehicleDesc.MaxLength = 200
        Me.txtVehicleDesc.MendatroryField = False
        Me.txtVehicleDesc.MyLinkLable1 = Me.lblVehicleNo
        Me.txtVehicleDesc.MyLinkLable2 = Nothing
        Me.txtVehicleDesc.Name = "txtVehicleDesc"
        Me.txtVehicleDesc.ReadOnly = True
        Me.txtVehicleDesc.ReferenceFieldDesc = Nothing
        Me.txtVehicleDesc.ReferenceFieldName = Nothing
        Me.txtVehicleDesc.ReferenceTableName = Nothing
        Me.txtVehicleDesc.Size = New System.Drawing.Size(393, 18)
        Me.txtVehicleDesc.TabIndex = 33
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.Location = New System.Drawing.Point(7, 71)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(43, 16)
        Me.lblVehicleNo.TabIndex = 34
        Me.lblVehicleNo.Text = "Vehicle"
        '
        'MyLabel4
        '
        Me.MyLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(912, 424)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel4.TabIndex = 15
        Me.MyLabel4.Text = "Amount"
        '
        'TxtVehicleCode
        '
        Me.TxtVehicleCode.CalculationExpression = Nothing
        Me.TxtVehicleCode.FieldCode = Nothing
        Me.TxtVehicleCode.FieldDesc = Nothing
        Me.TxtVehicleCode.FieldMaxLength = 0
        Me.TxtVehicleCode.FieldName = Nothing
        Me.TxtVehicleCode.isCalculatedField = False
        Me.TxtVehicleCode.IsSourceFromTable = False
        Me.TxtVehicleCode.IsSourceFromValueList = False
        Me.TxtVehicleCode.IsUnique = False
        Me.TxtVehicleCode.Location = New System.Drawing.Point(82, 70)
        Me.TxtVehicleCode.MendatroryField = True
        Me.TxtVehicleCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleCode.MyLinkLable1 = Me.lblVehicleNo
        Me.TxtVehicleCode.MyLinkLable2 = Nothing
        Me.TxtVehicleCode.MyReadOnly = False
        Me.TxtVehicleCode.MyShowMasterFormButton = False
        Me.TxtVehicleCode.Name = "TxtVehicleCode"
        Me.TxtVehicleCode.ReferenceFieldDesc = Nothing
        Me.TxtVehicleCode.ReferenceFieldName = Nothing
        Me.TxtVehicleCode.ReferenceTableName = Nothing
        Me.TxtVehicleCode.Size = New System.Drawing.Size(126, 19)
        Me.TxtVehicleCode.TabIndex = 12
        Me.TxtVehicleCode.Value = ""
        '
        'txtnrg
        '
        Me.txtnrg.CalculationExpression = Nothing
        Me.txtnrg.FieldCode = Nothing
        Me.txtnrg.FieldDesc = Nothing
        Me.txtnrg.FieldMaxLength = 0
        Me.txtnrg.FieldName = Nothing
        Me.txtnrg.isCalculatedField = False
        Me.txtnrg.IsSourceFromTable = False
        Me.txtnrg.IsSourceFromValueList = False
        Me.txtnrg.IsUnique = False
        Me.txtnrg.Location = New System.Drawing.Point(716, 112)
        Me.txtnrg.MendatroryField = False
        Me.txtnrg.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnrg.MyLinkLable1 = Me.RadLabel15
        Me.txtnrg.MyLinkLable2 = Nothing
        Me.txtnrg.MyReadOnly = False
        Me.txtnrg.MyShowMasterFormButton = False
        Me.txtnrg.Name = "txtnrg"
        Me.txtnrg.ReferenceFieldDesc = Nothing
        Me.txtnrg.ReferenceFieldName = Nothing
        Me.txtnrg.ReferenceTableName = Nothing
        Me.txtnrg.Size = New System.Drawing.Size(146, 20)
        Me.txtnrg.TabIndex = 13
        Me.txtnrg.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(7, 27)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 21
        Me.RadLabel15.Text = "Location"
        '
        'lblInvoiceNo
        '
        Me.lblInvoiceNo.AutoSize = False
        Me.lblInvoiceNo.BorderVisible = True
        Me.lblInvoiceNo.FieldName = Nothing
        Me.lblInvoiceNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceNo.Location = New System.Drawing.Point(716, 133)
        Me.lblInvoiceNo.Name = "lblInvoiceNo"
        Me.lblInvoiceNo.Size = New System.Drawing.Size(146, 20)
        Me.lblInvoiceNo.TabIndex = 18
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(606, 134)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel1.TabIndex = 26
        Me.MyLabel1.Text = "Invoice No"
        '
        'chkInterBranch
        '
        Me.chkInterBranch.Enabled = False
        Me.chkInterBranch.Location = New System.Drawing.Point(273, 154)
        Me.chkInterBranch.MyLinkLable1 = Nothing
        Me.chkInterBranch.MyLinkLable2 = Nothing
        Me.chkInterBranch.Name = "chkInterBranch"
        Me.chkInterBranch.Size = New System.Drawing.Size(81, 18)
        Me.chkInterBranch.TabIndex = 2
        Me.chkInterBranch.Tag1 = Nothing
        Me.chkInterBranch.Text = "Inter Branch"
        '
        'chkExcisable
        '
        Me.chkExcisable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExcisable.Location = New System.Drawing.Point(82, 141)
        Me.chkExcisable.Name = "chkExcisable"
        Me.chkExcisable.Size = New System.Drawing.Size(69, 16)
        Me.chkExcisable.TabIndex = 14
        Me.chkExcisable.Text = "Excisable"
        Me.chkExcisable.Visible = False
        '
        'txtscrapinvoice
        '
        Me.txtscrapinvoice.CalculationExpression = Nothing
        Me.txtscrapinvoice.FieldCode = Nothing
        Me.txtscrapinvoice.FieldDesc = Nothing
        Me.txtscrapinvoice.FieldMaxLength = 0
        Me.txtscrapinvoice.FieldName = Nothing
        Me.txtscrapinvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtscrapinvoice.isCalculatedField = False
        Me.txtscrapinvoice.IsSourceFromTable = False
        Me.txtscrapinvoice.IsSourceFromValueList = False
        Me.txtscrapinvoice.IsUnique = False
        Me.txtscrapinvoice.Location = New System.Drawing.Point(459, 138)
        Me.txtscrapinvoice.MaxLength = 30
        Me.txtscrapinvoice.MendatroryField = False
        Me.txtscrapinvoice.MyLinkLable1 = Me.RadLabel12
        Me.txtscrapinvoice.MyLinkLable2 = Nothing
        Me.txtscrapinvoice.Name = "txtscrapinvoice"
        Me.txtscrapinvoice.ReferenceFieldDesc = Nothing
        Me.txtscrapinvoice.ReferenceFieldName = Nothing
        Me.txtscrapinvoice.ReferenceTableName = Nothing
        Me.txtscrapinvoice.Size = New System.Drawing.Size(145, 18)
        Me.txtscrapinvoice.TabIndex = 17
        '
        'RadLabel12
        '
        Me.RadLabel12.FieldName = Nothing
        Me.RadLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel12.Location = New System.Drawing.Point(361, 139)
        Me.RadLabel12.Name = "RadLabel12"
        Me.RadLabel12.Size = New System.Drawing.Size(92, 16)
        Me.RadLabel12.TabIndex = 28
        Me.RadLabel12.Text = "Scrap Invoice No"
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel7.Location = New System.Drawing.Point(606, 113)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel7.TabIndex = 29
        Me.RadLabel7.Text = "NRGP No."
        '
        'dtpexp
        '
        Me.dtpexp.CalculationExpression = Nothing
        Me.dtpexp.CustomFormat = "dd/MM/yyyy  hh:mm tt"
        Me.dtpexp.FieldCode = Nothing
        Me.dtpexp.FieldDesc = Nothing
        Me.dtpexp.FieldMaxLength = 0
        Me.dtpexp.FieldName = Nothing
        Me.dtpexp.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpexp.isCalculatedField = False
        Me.dtpexp.IsSourceFromTable = False
        Me.dtpexp.IsSourceFromValueList = False
        Me.dtpexp.IsUnique = False
        Me.dtpexp.Location = New System.Drawing.Point(716, 25)
        Me.dtpexp.MendatroryField = False
        Me.dtpexp.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpexp.MyLinkLable1 = Me.RadLabel13
        Me.dtpexp.MyLinkLable2 = Nothing
        Me.dtpexp.Name = "dtpexp"
        Me.dtpexp.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpexp.ReferenceFieldDesc = Nothing
        Me.dtpexp.ReferenceFieldName = Nothing
        Me.dtpexp.ReferenceTableName = Nothing
        Me.dtpexp.Size = New System.Drawing.Size(146, 20)
        Me.dtpexp.TabIndex = 5
        Me.dtpexp.TabStop = False
        Me.dtpexp.Text = "05/08/2011  05:38 PM"
        Me.dtpexp.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        Me.dtpexp.Visible = False
        '
        'RadLabel13
        '
        Me.RadLabel13.FieldName = Nothing
        Me.RadLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel13.Location = New System.Drawing.Point(606, 27)
        Me.RadLabel13.Name = "RadLabel13"
        Me.RadLabel13.Size = New System.Drawing.Size(107, 16)
        Me.RadLabel13.TabIndex = 20
        Me.RadLabel13.Text = "EXP Shipment Date"
        Me.RadLabel13.Visible = False
        '
        'txtlocation
        '
        Me.txtlocation.CalculationExpression = Nothing
        Me.txtlocation.FieldCode = Nothing
        Me.txtlocation.FieldDesc = Nothing
        Me.txtlocation.FieldMaxLength = 0
        Me.txtlocation.FieldName = Nothing
        Me.txtlocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlocation.isCalculatedField = False
        Me.txtlocation.IsSourceFromTable = False
        Me.txtlocation.IsSourceFromValueList = False
        Me.txtlocation.IsUnique = False
        Me.txtlocation.Location = New System.Drawing.Point(211, 26)
        Me.txtlocation.MaxLength = 200
        Me.txtlocation.MendatroryField = False
        Me.txtlocation.MyLinkLable1 = Me.RadLabel15
        Me.txtlocation.MyLinkLable2 = Nothing
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.ReadOnly = True
        Me.txtlocation.ReferenceFieldDesc = Nothing
        Me.txtlocation.ReferenceFieldName = Nothing
        Me.txtlocation.ReferenceTableName = Nothing
        Me.txtlocation.Size = New System.Drawing.Size(393, 18)
        Me.txtlocation.TabIndex = 7
        Me.txtlocation.TabStop = False
        '
        'RadLabel21
        '
        Me.RadLabel21.FieldName = Nothing
        Me.RadLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel21.Location = New System.Drawing.Point(7, 115)
        Me.RadLabel21.Name = "RadLabel21"
        Me.RadLabel21.Size = New System.Drawing.Size(58, 16)
        Me.RadLabel21.TabIndex = 25
        Me.RadLabel21.Text = "Reference"
        '
        'txtref
        '
        Me.txtref.CalculationExpression = Nothing
        Me.txtref.FieldCode = Nothing
        Me.txtref.FieldDesc = Nothing
        Me.txtref.FieldMaxLength = 0
        Me.txtref.FieldName = Nothing
        Me.txtref.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtref.isCalculatedField = False
        Me.txtref.IsSourceFromTable = False
        Me.txtref.IsSourceFromValueList = False
        Me.txtref.IsUnique = False
        Me.txtref.Location = New System.Drawing.Point(82, 115)
        Me.txtref.MaxLength = 50
        Me.txtref.MendatroryField = False
        Me.txtref.MyLinkLable1 = Me.RadLabel21
        Me.txtref.MyLinkLable2 = Nothing
        Me.txtref.Name = "txtref"
        Me.txtref.ReferenceFieldDesc = Nothing
        Me.txtref.ReferenceFieldName = Nothing
        Me.txtref.ReferenceTableName = Nothing
        Me.txtref.Size = New System.Drawing.Size(521, 18)
        Me.txtref.TabIndex = 10
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(606, 92)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(66, 16)
        Me.RadLabel3.TabIndex = 26
        Me.RadLabel3.Text = "PO Number"
        '
        'RadLabel18
        '
        Me.RadLabel18.FieldName = Nothing
        Me.RadLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel18.Location = New System.Drawing.Point(606, 71)
        Me.RadLabel18.Name = "RadLabel18"
        Me.RadLabel18.Size = New System.Drawing.Size(91, 16)
        Me.RadLabel18.TabIndex = 22
        Me.RadLabel18.Text = "Ship To Location"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(7, 5)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(72, 16)
        Me.RadLabel1.TabIndex = 17
        Me.RadLabel1.Text = "Shipment No"
        '
        'fndShipToLocation
        '
        Me.fndShipToLocation.CalculationExpression = Nothing
        Me.fndShipToLocation.FieldCode = Nothing
        Me.fndShipToLocation.FieldDesc = Nothing
        Me.fndShipToLocation.FieldMaxLength = 0
        Me.fndShipToLocation.FieldName = Nothing
        Me.fndShipToLocation.isCalculatedField = False
        Me.fndShipToLocation.IsSourceFromTable = False
        Me.fndShipToLocation.IsSourceFromValueList = False
        Me.fndShipToLocation.IsUnique = False
        Me.fndShipToLocation.Location = New System.Drawing.Point(716, 70)
        Me.fndShipToLocation.MendatroryField = False
        Me.fndShipToLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndShipToLocation.MyLinkLable1 = Me.RadLabel18
        Me.fndShipToLocation.MyLinkLable2 = Nothing
        Me.fndShipToLocation.MyReadOnly = False
        Me.fndShipToLocation.MyShowMasterFormButton = False
        Me.fndShipToLocation.Name = "fndShipToLocation"
        Me.fndShipToLocation.ReferenceFieldDesc = Nothing
        Me.fndShipToLocation.ReferenceFieldName = Nothing
        Me.fndShipToLocation.ReferenceTableName = Nothing
        Me.fndShipToLocation.Size = New System.Drawing.Size(146, 20)
        Me.fndShipToLocation.TabIndex = 7
        Me.fndShipToLocation.Value = ""
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(82, 26)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.RadLabel15
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(126, 19)
        Me.fndLocation.TabIndex = 6
        Me.fndLocation.Value = ""
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(357, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(76, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 30
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(82, 4)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 32767
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 19)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'txtponumber
        '
        Me.txtponumber.CalculationExpression = Nothing
        Me.txtponumber.FieldCode = Nothing
        Me.txtponumber.FieldDesc = Nothing
        Me.txtponumber.FieldMaxLength = 0
        Me.txtponumber.FieldName = Nothing
        Me.txtponumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtponumber.isCalculatedField = False
        Me.txtponumber.IsSourceFromTable = False
        Me.txtponumber.IsSourceFromValueList = False
        Me.txtponumber.IsUnique = False
        Me.txtponumber.Location = New System.Drawing.Point(716, 91)
        Me.txtponumber.MaxLength = 30
        Me.txtponumber.MendatroryField = False
        Me.txtponumber.MyLinkLable1 = Me.RadLabel3
        Me.txtponumber.MyLinkLable2 = Nothing
        Me.txtponumber.Name = "txtponumber"
        Me.txtponumber.ReferenceFieldDesc = Nothing
        Me.txtponumber.ReferenceFieldName = Nothing
        Me.txtponumber.ReferenceTableName = Nothing
        Me.txtponumber.Size = New System.Drawing.Size(146, 18)
        Me.txtponumber.TabIndex = 11
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = CType(resources.GetObject("btnAddNew.Image"), System.Drawing.Image)
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(334, 3)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 179)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(1071, 176)
        Me.RadGroupBox2.TabIndex = 19
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
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1051, 146)
        Me.gv1.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
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
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1071, 439)
        Me.RadPageViewPage2.Text = "Taxes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(562, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(162, 35)
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
        Me.RadLabel11.Location = New System.Drawing.Point(3, 6)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 6
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
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 20)
        Me.lblTaxGrpName.TabIndex = 5
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(913, 335)
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
        Me.RadGroupBox1.Location = New System.Drawing.Point(1, 348)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1070, 87)
        Me.RadGroupBox1.TabIndex = 3
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
        Me.RadLabel16.Location = New System.Drawing.Point(6, 26)
        Me.RadLabel16.Name = "RadLabel16"
        Me.RadLabel16.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel16.TabIndex = 4
        Me.RadLabel16.Text = "Term Code"
        '
        'lblTermName
        '
        Me.lblTermName.AutoSize = False
        Me.lblTermName.BorderVisible = True
        Me.lblTermName.FieldName = Nothing
        Me.lblTermName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTermName.Location = New System.Drawing.Point(220, 23)
        Me.lblTermName.Name = "lblTermName"
        Me.lblTermName.Size = New System.Drawing.Size(321, 20)
        Me.lblTermName.TabIndex = 2
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
        Me.RadLabel17.TabIndex = 3
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
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition5
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1068, 297)
        Me.gv2.TabIndex = 2
        Me.gv2.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(112.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1071, 439)
        Me.RadPageViewPage3.Text = "Additional Charges "
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gvadd)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox3.Controls.Add(Me.txtaddamt)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(855, 348)
        Me.RadGroupBox3.TabIndex = 0
        '
        'gvadd
        '
        Me.gvadd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvadd.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvadd.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvadd.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvadd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvadd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvadd.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvadd.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GridViewTextBoxColumn3.HeaderText = "Additional Charges"
        GridViewTextBoxColumn3.Name = "coladdcode"
        GridViewTextBoxColumn3.Width = 125
        GridViewTextBoxColumn4.HeaderText = "Description"
        GridViewTextBoxColumn4.Name = "coladddesc"
        GridViewTextBoxColumn4.Width = 300
        GridViewDecimalColumn2.HeaderText = "Amount"
        GridViewDecimalColumn2.Name = "coladdamt"
        GridViewDecimalColumn2.Width = 78
        Me.gvadd.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn3, GridViewTextBoxColumn4, GridViewDecimalColumn2})
        Me.gvadd.MasterTemplate.EnableGrouping = False
        Me.gvadd.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvadd.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvadd.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gvadd.Name = "gvadd"
        Me.gvadd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvadd.ShowHeaderCellButtons = True
        Me.gvadd.Size = New System.Drawing.Size(855, 324)
        Me.gvadd.TabIndex = 0
        Me.gvadd.TabStop = False
        '
        'RadLabel4
        '
        Me.RadLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(648, 329)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(98, 16)
        Me.RadLabel4.TabIndex = 2
        Me.RadLabel4.Text = "Additional Amount"
        '
        'txtaddamt
        '
        Me.txtaddamt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtaddamt.CalculationExpression = Nothing
        Me.txtaddamt.FieldCode = Nothing
        Me.txtaddamt.FieldDesc = Nothing
        Me.txtaddamt.FieldMaxLength = 0
        Me.txtaddamt.FieldName = Nothing
        Me.txtaddamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddamt.isCalculatedField = False
        Me.txtaddamt.IsSourceFromTable = False
        Me.txtaddamt.IsSourceFromValueList = False
        Me.txtaddamt.IsUnique = False
        Me.txtaddamt.Location = New System.Drawing.Point(750, 328)
        Me.txtaddamt.MaxLength = 200
        Me.txtaddamt.MendatroryField = False
        Me.txtaddamt.MyLinkLable1 = Me.RadLabel4
        Me.txtaddamt.MyLinkLable2 = Nothing
        Me.txtaddamt.Name = "txtaddamt"
        Me.txtaddamt.ReferenceFieldDesc = Nothing
        Me.txtaddamt.ReferenceFieldName = Nothing
        Me.txtaddamt.ReferenceTableName = Nothing
        Me.txtaddamt.Size = New System.Drawing.Size(101, 18)
        Me.txtaddamt.TabIndex = 1
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(89.0!, 26.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 35)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1071, 439)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1071, 439)
        Me.UcCustomFields1.TabIndex = 2
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(1071, 439)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(1071, 439)
        Me.UcAttachment1.TabIndex = 0
        Me.UcAttachment1.TabStop = False
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.txtTCSTaxRate)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel39)
        Me.RadPageViewPage4.Controls.Add(Me.lblActualTCSTaxBaseAmt)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel40)
        Me.RadPageViewPage4.Controls.Add(Me.txttcstaxbaseamount)
        Me.RadPageViewPage4.Controls.Add(Me.btnCancel)
        Me.RadPageViewPage4.Controls.Add(Me.lblRound_Off)
        Me.RadPageViewPage4.Controls.Add(Me.txtRoundOff)
        Me.RadPageViewPage4.Controls.Add(Me.lblNetWeight)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage4.Controls.Add(Me.lblGrossWeight)
        Me.RadPageViewPage4.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage4.Controls.Add(Me.lbldocamt)
        Me.RadPageViewPage4.Controls.Add(Me.lbladdc)
        Me.RadPageViewPage4.Controls.Add(Me.lbladdcharges)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel8)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel27)
        Me.RadPageViewPage4.Controls.Add(Me.lblTotRAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblTaxAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtAfterDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.lblDiscountAmt)
        Me.RadPageViewPage4.Controls.Add(Me.lblAmtWithDiscount)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel25)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel22)
        Me.RadPageViewPage4.Controls.Add(Me.RadLabel19)
        Me.RadPageViewPage4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(41.0!, 26.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(1071, 439)
        Me.RadPageViewPage4.Text = "Total"
        '
        'txtTCSTaxRate
        '
        Me.txtTCSTaxRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTCSTaxRate.CalculationExpression = Nothing
        Me.txtTCSTaxRate.DecimalPlaces = 0
        Me.txtTCSTaxRate.FieldCode = Nothing
        Me.txtTCSTaxRate.FieldDesc = Nothing
        Me.txtTCSTaxRate.FieldMaxLength = 0
        Me.txtTCSTaxRate.FieldName = Nothing
        Me.txtTCSTaxRate.isCalculatedField = False
        Me.txtTCSTaxRate.IsSourceFromTable = False
        Me.txtTCSTaxRate.IsSourceFromValueList = False
        Me.txtTCSTaxRate.IsUnique = False
        Me.txtTCSTaxRate.Location = New System.Drawing.Point(221, 302)
        Me.txtTCSTaxRate.MendatroryField = False
        Me.txtTCSTaxRate.MyLinkLable1 = Nothing
        Me.txtTCSTaxRate.MyLinkLable2 = Nothing
        Me.txtTCSTaxRate.Name = "txtTCSTaxRate"
        Me.txtTCSTaxRate.ReferenceFieldDesc = Nothing
        Me.txtTCSTaxRate.ReferenceFieldName = Nothing
        Me.txtTCSTaxRate.ReferenceTableName = Nothing
        Me.txtTCSTaxRate.Size = New System.Drawing.Size(115, 20)
        Me.txtTCSTaxRate.TabIndex = 1416
        Me.txtTCSTaxRate.Text = "0"
        Me.txtTCSTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTCSTaxRate.Value = 0R
        Me.txtTCSTaxRate.Visible = False
        '
        'MyLabel39
        '
        Me.MyLabel39.FieldName = Nothing
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(81, 256)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel39.TabIndex = 1415
        Me.MyLabel39.Text = "Actual TCS Tax Base Amt"
        '
        'lblActualTCSTaxBaseAmt
        '
        Me.lblActualTCSTaxBaseAmt.AutoSize = False
        Me.lblActualTCSTaxBaseAmt.BorderVisible = True
        Me.lblActualTCSTaxBaseAmt.FieldName = Nothing
        Me.lblActualTCSTaxBaseAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualTCSTaxBaseAmt.Location = New System.Drawing.Point(221, 254)
        Me.lblActualTCSTaxBaseAmt.Name = "lblActualTCSTaxBaseAmt"
        Me.lblActualTCSTaxBaseAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblActualTCSTaxBaseAmt.TabIndex = 1414
        Me.lblActualTCSTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel40
        '
        Me.MyLabel40.FieldName = Nothing
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel40.Location = New System.Drawing.Point(93, 280)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel40.TabIndex = 1413
        Me.MyLabel40.Text = "TCS Tax Base Amount"
        '
        'txttcstaxbaseamount
        '
        Me.txttcstaxbaseamount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txttcstaxbaseamount.CalculationExpression = Nothing
        Me.txttcstaxbaseamount.DecimalPlaces = 0
        Me.txttcstaxbaseamount.FieldCode = Nothing
        Me.txttcstaxbaseamount.FieldDesc = Nothing
        Me.txttcstaxbaseamount.FieldMaxLength = 0
        Me.txttcstaxbaseamount.FieldName = Nothing
        Me.txttcstaxbaseamount.isCalculatedField = False
        Me.txttcstaxbaseamount.IsSourceFromTable = False
        Me.txttcstaxbaseamount.IsSourceFromValueList = False
        Me.txttcstaxbaseamount.IsUnique = False
        Me.txttcstaxbaseamount.Location = New System.Drawing.Point(222, 276)
        Me.txttcstaxbaseamount.MendatroryField = False
        Me.txttcstaxbaseamount.MyLinkLable1 = Nothing
        Me.txttcstaxbaseamount.MyLinkLable2 = Nothing
        Me.txttcstaxbaseamount.Name = "txttcstaxbaseamount"
        Me.txttcstaxbaseamount.ReferenceFieldDesc = Nothing
        Me.txttcstaxbaseamount.ReferenceFieldName = Nothing
        Me.txttcstaxbaseamount.ReferenceTableName = Nothing
        Me.txttcstaxbaseamount.Size = New System.Drawing.Size(115, 20)
        Me.txttcstaxbaseamount.TabIndex = 1412
        Me.txttcstaxbaseamount.Text = "0"
        Me.txttcstaxbaseamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttcstaxbaseamount.Value = 0R
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(395, 228)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 22)
        Me.btnCancel.TabIndex = 1411
        Me.btnCancel.Text = "Cancel"
        '
        'lblRound_Off
        '
        Me.lblRound_Off.FieldName = Nothing
        Me.lblRound_Off.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRound_Off.Location = New System.Drawing.Point(118, 209)
        Me.lblRound_Off.Name = "lblRound_Off"
        Me.lblRound_Off.Size = New System.Drawing.Size(100, 16)
        Me.lblRound_Off.TabIndex = 1410
        Me.lblRound_Off.Text = "Round Off Amount"
        '
        'txtRoundOff
        '
        Me.txtRoundOff.AutoSize = False
        Me.txtRoundOff.BorderVisible = True
        Me.txtRoundOff.FieldName = Nothing
        Me.txtRoundOff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoundOff.Location = New System.Drawing.Point(221, 208)
        Me.txtRoundOff.Name = "txtRoundOff"
        Me.txtRoundOff.Size = New System.Drawing.Size(110, 18)
        Me.txtRoundOff.TabIndex = 1409
        Me.txtRoundOff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNetWeight
        '
        Me.lblNetWeight.AutoSize = False
        Me.lblNetWeight.BorderVisible = True
        Me.lblNetWeight.FieldName = Nothing
        Me.lblNetWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetWeight.Location = New System.Drawing.Point(499, 68)
        Me.lblNetWeight.Name = "lblNetWeight"
        Me.lblNetWeight.Size = New System.Drawing.Size(110, 18)
        Me.lblNetWeight.TabIndex = 16
        Me.lblNetWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(395, 69)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel7.TabIndex = 17
        Me.MyLabel7.Text = "Total Net Weight"
        '
        'lblGrossWeight
        '
        Me.lblGrossWeight.AutoSize = False
        Me.lblGrossWeight.BorderVisible = True
        Me.lblGrossWeight.FieldName = Nothing
        Me.lblGrossWeight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrossWeight.Location = New System.Drawing.Point(499, 39)
        Me.lblGrossWeight.Name = "lblGrossWeight"
        Me.lblGrossWeight.Size = New System.Drawing.Size(110, 18)
        Me.lblGrossWeight.TabIndex = 14
        Me.lblGrossWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(395, 40)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(103, 16)
        Me.MyLabel5.TabIndex = 15
        Me.MyLabel5.Text = "Total Gross Weight"
        '
        'lbldocamt
        '
        Me.lbldocamt.AutoSize = False
        Me.lbldocamt.BorderVisible = True
        Me.lbldocamt.FieldName = Nothing
        Me.lbldocamt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldocamt.Location = New System.Drawing.Point(221, 233)
        Me.lbldocamt.Name = "lbldocamt"
        Me.lbldocamt.Size = New System.Drawing.Size(110, 18)
        Me.lbldocamt.TabIndex = 6
        Me.lbldocamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbladdc
        '
        Me.lbladdc.FieldName = Nothing
        Me.lbladdc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladdc.Location = New System.Drawing.Point(117, 234)
        Me.lbladdc.Name = "lbladdc"
        Me.lbladdc.Size = New System.Drawing.Size(100, 16)
        Me.lbladdc.TabIndex = 13
        Me.lbladdc.Text = "Document Amount"
        '
        'lbladdcharges
        '
        Me.lbladdcharges.AutoSize = False
        Me.lbladdcharges.BorderVisible = True
        Me.lbladdcharges.FieldName = Nothing
        Me.lbladdcharges.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladdcharges.Location = New System.Drawing.Point(221, 184)
        Me.lbladdcharges.Name = "lbladdcharges"
        Me.lbladdcharges.Size = New System.Drawing.Size(110, 18)
        Me.lbladdcharges.TabIndex = 5
        Me.lbladdcharges.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(115, 185)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(102, 16)
        Me.RadLabel8.TabIndex = 12
        Me.RadLabel8.Text = "Additional Charges"
        '
        'RadLabel27
        '
        Me.RadLabel27.FieldName = Nothing
        Me.RadLabel27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel27.Location = New System.Drawing.Point(143, 156)
        Me.RadLabel27.Name = "RadLabel27"
        Me.RadLabel27.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel27.TabIndex = 11
        Me.RadLabel27.Text = "Total Amount"
        '
        'lblTotRAmt
        '
        Me.lblTotRAmt.AutoSize = False
        Me.lblTotRAmt.BorderVisible = True
        Me.lblTotRAmt.FieldName = Nothing
        Me.lblTotRAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotRAmt.Location = New System.Drawing.Point(221, 155)
        Me.lblTotRAmt.Name = "lblTotRAmt"
        Me.lblTotRAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTotRAmt.TabIndex = 4
        Me.lblTotRAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = False
        Me.lblTaxAmt.BorderVisible = True
        Me.lblTaxAmt.FieldName = Nothing
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(221, 126)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblTaxAmt.TabIndex = 3
        Me.lblTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtAfterDiscount
        '
        Me.lblAmtAfterDiscount.AutoSize = False
        Me.lblAmtAfterDiscount.BorderVisible = True
        Me.lblAmtAfterDiscount.FieldName = Nothing
        Me.lblAmtAfterDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtAfterDiscount.Location = New System.Drawing.Point(221, 97)
        Me.lblAmtAfterDiscount.Name = "lblAmtAfterDiscount"
        Me.lblAmtAfterDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtAfterDiscount.TabIndex = 2
        Me.lblAmtAfterDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiscountAmt
        '
        Me.lblDiscountAmt.AutoSize = False
        Me.lblDiscountAmt.BorderVisible = True
        Me.lblDiscountAmt.FieldName = Nothing
        Me.lblDiscountAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscountAmt.Location = New System.Drawing.Point(221, 68)
        Me.lblDiscountAmt.Name = "lblDiscountAmt"
        Me.lblDiscountAmt.Size = New System.Drawing.Size(110, 18)
        Me.lblDiscountAmt.TabIndex = 1
        Me.lblDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAmtWithDiscount
        '
        Me.lblAmtWithDiscount.AutoSize = False
        Me.lblAmtWithDiscount.BorderVisible = True
        Me.lblAmtWithDiscount.FieldName = Nothing
        Me.lblAmtWithDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtWithDiscount.Location = New System.Drawing.Point(221, 39)
        Me.lblAmtWithDiscount.Name = "lblAmtWithDiscount"
        Me.lblAmtWithDiscount.Size = New System.Drawing.Size(110, 18)
        Me.lblAmtWithDiscount.TabIndex = 0
        Me.lblAmtWithDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(97, 98)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(120, 16)
        Me.RadLabel9.TabIndex = 9
        Me.RadLabel9.Text = "Amount After Discount"
        '
        'RadLabel25
        '
        Me.RadLabel25.FieldName = Nothing
        Me.RadLabel25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel25.Location = New System.Drawing.Point(140, 127)
        Me.RadLabel25.Name = "RadLabel25"
        Me.RadLabel25.Size = New System.Drawing.Size(77, 16)
        Me.RadLabel25.TabIndex = 10
        Me.RadLabel25.Text = "+ Tax Amount"
        '
        'RadLabel22
        '
        Me.RadLabel22.FieldName = Nothing
        Me.RadLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel22.Location = New System.Drawing.Point(118, 69)
        Me.RadLabel22.Name = "RadLabel22"
        Me.RadLabel22.Size = New System.Drawing.Size(99, 16)
        Me.RadLabel22.TabIndex = 8
        Me.RadLabel22.Text = "- Discount Amount"
        '
        'RadLabel19
        '
        Me.RadLabel19.FieldName = Nothing
        Me.RadLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel19.Location = New System.Drawing.Point(31, 40)
        Me.RadLabel19.Name = "RadLabel19"
        Me.RadLabel19.Size = New System.Drawing.Size(186, 16)
        Me.RadLabel19.TabIndex = 7
        Me.RadLabel19.Text = "Document Amount without Discount"
        '
        '<'<<<<<< HEAD
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox6)
        Me.RadPageViewPage5.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(121.0!, 26.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(1071, 439)
        Me.RadPageViewPage5.Text = "E-Invoice / E-Waybill"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox6.Controls.Add(Me.txtEWayBillDate)
        Me.RadGroupBox6.Controls.Add(Me.txtEwayValidDate)
        Me.RadGroupBox6.Controls.Add(Me.txtEWayBillRemarks)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel38)
        Me.RadGroupBox6.Controls.Add(Me.txtEWayBillNo)
        Me.RadGroupBox6.Controls.Add(Me.btnEWaybillUpdate)
        Me.RadGroupBox6.HeaderText = "E-Waybill Detail"
        Me.RadGroupBox6.Location = New System.Drawing.Point(0, 218)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(683, 138)
        Me.RadGroupBox6.TabIndex = 1421
        Me.RadGroupBox6.Text = "E-Waybill Detail"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(11, 22)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel12.TabIndex = 1413
        Me.MyLabel12.Text = "E Way Bill No."
        '
        'txtEWayBillDate
        '
        Me.txtEWayBillDate.CalculationExpression = Nothing
        Me.txtEWayBillDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtEWayBillDate.FieldCode = Nothing
        Me.txtEWayBillDate.FieldDesc = Nothing
        Me.txtEWayBillDate.FieldMaxLength = 0
        Me.txtEWayBillDate.FieldName = Nothing
        Me.txtEWayBillDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWayBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEWayBillDate.isCalculatedField = False
        Me.txtEWayBillDate.IsSourceFromTable = False
        Me.txtEWayBillDate.IsSourceFromValueList = False
        Me.txtEWayBillDate.IsUnique = False
        Me.txtEWayBillDate.Location = New System.Drawing.Point(136, 39)
        Me.txtEWayBillDate.MendatroryField = False
        Me.txtEWayBillDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEWayBillDate.MyLinkLable1 = Me.RadLabel4
        Me.txtEWayBillDate.MyLinkLable2 = Nothing
        Me.txtEWayBillDate.Name = "txtEWayBillDate"
        Me.txtEWayBillDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEWayBillDate.ReferenceFieldDesc = Nothing
        Me.txtEWayBillDate.ReferenceFieldName = Nothing
        Me.txtEWayBillDate.ReferenceTableName = Nothing
        Me.txtEWayBillDate.Size = New System.Drawing.Size(126, 18)
        Me.txtEWayBillDate.TabIndex = 1412
        Me.txtEWayBillDate.TabStop = False
        Me.txtEWayBillDate.Text = "13/06/2011 11:29 AM"
        Me.txtEWayBillDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtEwayValidDate
        '
        Me.txtEwayValidDate.CalculationExpression = Nothing
        Me.txtEwayValidDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtEwayValidDate.FieldCode = Nothing
        Me.txtEwayValidDate.FieldDesc = Nothing
        Me.txtEwayValidDate.FieldMaxLength = 0
        Me.txtEwayValidDate.FieldName = Nothing
        Me.txtEwayValidDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEwayValidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEwayValidDate.isCalculatedField = False
        Me.txtEwayValidDate.IsSourceFromTable = False
        Me.txtEwayValidDate.IsSourceFromValueList = False
        Me.txtEwayValidDate.IsUnique = False
        Me.txtEwayValidDate.Location = New System.Drawing.Point(136, 59)
        Me.txtEwayValidDate.MendatroryField = False
        Me.txtEwayValidDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEwayValidDate.MyLinkLable1 = Me.RadLabel4
        Me.txtEwayValidDate.MyLinkLable2 = Nothing
        Me.txtEwayValidDate.Name = "txtEwayValidDate"
        Me.txtEwayValidDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEwayValidDate.ReferenceFieldDesc = Nothing
        Me.txtEwayValidDate.ReferenceFieldName = Nothing
        Me.txtEwayValidDate.ReferenceTableName = Nothing
        Me.txtEwayValidDate.Size = New System.Drawing.Size(126, 18)
        Me.txtEwayValidDate.TabIndex = 1411
        Me.txtEwayValidDate.TabStop = False
        Me.txtEwayValidDate.Text = "13/06/2011 11:29 AM"
        Me.txtEwayValidDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'txtEWayBillRemarks
        '
        Me.txtEWayBillRemarks.CalculationExpression = Nothing
        Me.txtEWayBillRemarks.FieldCode = Nothing
        Me.txtEWayBillRemarks.FieldDesc = Nothing
        Me.txtEWayBillRemarks.FieldMaxLength = 0
        Me.txtEWayBillRemarks.FieldName = Nothing
        Me.txtEWayBillRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWayBillRemarks.isCalculatedField = False
        Me.txtEWayBillRemarks.IsSourceFromTable = False
        Me.txtEWayBillRemarks.IsSourceFromValueList = False
        Me.txtEWayBillRemarks.IsUnique = False
        Me.txtEWayBillRemarks.Location = New System.Drawing.Point(136, 79)
        Me.txtEWayBillRemarks.MaxLength = 30
        Me.txtEWayBillRemarks.MendatroryField = False
        Me.txtEWayBillRemarks.MyLinkLable1 = Nothing
        Me.txtEWayBillRemarks.MyLinkLable2 = Nothing
        Me.txtEWayBillRemarks.Name = "txtEWayBillRemarks"
        Me.txtEWayBillRemarks.ReferenceFieldDesc = Nothing
        Me.txtEWayBillRemarks.ReferenceFieldName = Nothing
        Me.txtEWayBillRemarks.ReferenceTableName = Nothing
        Me.txtEWayBillRemarks.Size = New System.Drawing.Size(291, 18)
        Me.txtEWayBillRemarks.TabIndex = 1410
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(11, 81)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel19.TabIndex = 1409
        Me.MyLabel19.Text = "E Waybill Bill Remarks"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(11, 61)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(113, 16)
        Me.MyLabel18.TabIndex = 1408
        Me.MyLabel18.Text = "E Way Bill Valid Date"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(11, 41)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel8.TabIndex = 1405
        Me.MyLabel8.Text = "E Way Bill Date"
        '
        'MyLabel38
        '
        Me.MyLabel38.FieldName = Nothing
        Me.MyLabel38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel38.Location = New System.Drawing.Point(20, 119)
        Me.MyLabel38.Name = "MyLabel38"
        Me.MyLabel38.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel38.TabIndex = 1407
        '
        'txtEWayBillNo
        '
        Me.txtEWayBillNo.CalculationExpression = Nothing
        Me.txtEWayBillNo.FieldCode = Nothing
        Me.txtEWayBillNo.FieldDesc = Nothing
        Me.txtEWayBillNo.FieldMaxLength = 0
        Me.txtEWayBillNo.FieldName = Nothing
        Me.txtEWayBillNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWayBillNo.isCalculatedField = False
        Me.txtEWayBillNo.IsSourceFromTable = False
        Me.txtEWayBillNo.IsSourceFromValueList = False
        Me.txtEWayBillNo.IsUnique = False
        Me.txtEWayBillNo.Location = New System.Drawing.Point(136, 19)
        Me.txtEWayBillNo.MaxLength = 30
        Me.txtEWayBillNo.MendatroryField = False
        Me.txtEWayBillNo.MyLinkLable1 = Nothing
        Me.txtEWayBillNo.MyLinkLable2 = Nothing
        Me.txtEWayBillNo.Name = "txtEWayBillNo"
        Me.txtEWayBillNo.ReferenceFieldDesc = Nothing
        Me.txtEWayBillNo.ReferenceFieldName = Nothing
        Me.txtEWayBillNo.ReferenceTableName = Nothing
        Me.txtEWayBillNo.Size = New System.Drawing.Size(291, 18)
        Me.txtEWayBillNo.TabIndex = 1402
        '
        'btnEWaybillUpdate
        '
        Me.btnEWaybillUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEWaybillUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEWaybillUpdate.Location = New System.Drawing.Point(136, 107)
        Me.btnEWaybillUpdate.Name = "btnEWaybillUpdate"
        Me.btnEWaybillUpdate.Size = New System.Drawing.Size(141, 20)
        Me.btnEWaybillUpdate.TabIndex = 41
        Me.btnEWaybillUpdate.Text = "Update"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtAckDate)
        Me.RadGroupBox4.Controls.Add(Me.EinvoiceBtnUpdate)
        Me.RadGroupBox4.Controls.Add(Me.EInvoiceQrCode)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox4.Controls.Add(Me.EinvoiceAckNo)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox4.Controls.Add(Me.EInvoiceIRNNo)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox4.HeaderText = "E-Invoice Detail"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(680, 209)
        Me.RadGroupBox4.TabIndex = 1420
        Me.RadGroupBox4.Text = "E-Invoice Detail"
        '
        'txtAckDate
        '
        Me.txtAckDate.CalculationExpression = Nothing
        Me.txtAckDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtAckDate.FieldCode = Nothing
        Me.txtAckDate.FieldDesc = Nothing
        Me.txtAckDate.FieldMaxLength = 0
        Me.txtAckDate.FieldName = Nothing
        Me.txtAckDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAckDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtAckDate.isCalculatedField = False
        Me.txtAckDate.IsSourceFromTable = False
        Me.txtAckDate.IsSourceFromValueList = False
        Me.txtAckDate.IsUnique = False
        Me.txtAckDate.Location = New System.Drawing.Point(84, 60)
        Me.txtAckDate.MendatroryField = False
        Me.txtAckDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtAckDate.MyLinkLable1 = Me.RadLabel4
        Me.txtAckDate.MyLinkLable2 = Nothing
        Me.txtAckDate.Name = "txtAckDate"
        Me.txtAckDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtAckDate.ReferenceFieldDesc = Nothing
        Me.txtAckDate.ReferenceFieldName = Nothing
        Me.txtAckDate.ReferenceTableName = Nothing
        Me.txtAckDate.Size = New System.Drawing.Size(126, 18)
        Me.txtAckDate.TabIndex = 1410
        Me.txtAckDate.TabStop = False
        Me.txtAckDate.Text = "13/06/2011 11:29 AM"
        Me.txtAckDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'EinvoiceBtnUpdate
        '
        Me.EinvoiceBtnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EinvoiceBtnUpdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EinvoiceBtnUpdate.Location = New System.Drawing.Point(84, 176)
        Me.EinvoiceBtnUpdate.Name = "EinvoiceBtnUpdate"
        Me.EinvoiceBtnUpdate.Size = New System.Drawing.Size(141, 20)
        Me.EinvoiceBtnUpdate.TabIndex = 41
        Me.EinvoiceBtnUpdate.Text = "Update"
        '
        'EInvoiceQrCode
        '
        Me.EInvoiceQrCode.AutoSize = False
        Me.EInvoiceQrCode.CalculationExpression = Nothing
        Me.EInvoiceQrCode.FieldCode = Nothing
        Me.EInvoiceQrCode.FieldDesc = Nothing
        Me.EInvoiceQrCode.FieldMaxLength = 0
        Me.EInvoiceQrCode.FieldName = Nothing
        Me.EInvoiceQrCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EInvoiceQrCode.isCalculatedField = False
        Me.EInvoiceQrCode.IsSourceFromTable = False
        Me.EInvoiceQrCode.IsSourceFromValueList = False
        Me.EInvoiceQrCode.IsUnique = False
        Me.EInvoiceQrCode.Location = New System.Drawing.Point(84, 80)
        Me.EInvoiceQrCode.MaxLength = 500
        Me.EInvoiceQrCode.MendatroryField = False
        Me.EInvoiceQrCode.Multiline = True
        Me.EInvoiceQrCode.MyLinkLable1 = Me.MyLabel15
        Me.EInvoiceQrCode.MyLinkLable2 = Nothing
        Me.EInvoiceQrCode.Name = "EInvoiceQrCode"
        Me.EInvoiceQrCode.ReferenceFieldDesc = Nothing
        Me.EInvoiceQrCode.ReferenceFieldName = Nothing
        Me.EInvoiceQrCode.ReferenceTableName = Nothing
        Me.EInvoiceQrCode.Size = New System.Drawing.Size(588, 93)
        Me.EInvoiceQrCode.TabIndex = 1409
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(11, 63)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel15.TabIndex = 1407
        Me.MyLabel15.Text = "Ack Date"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(11, 85)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel17.TabIndex = 1408
        Me.MyLabel17.Text = "QR Code"
        '
        'EinvoiceAckNo
        '
        Me.EinvoiceAckNo.CalculationExpression = Nothing
        Me.EinvoiceAckNo.FieldCode = Nothing
        Me.EinvoiceAckNo.FieldDesc = Nothing
        Me.EinvoiceAckNo.FieldMaxLength = 0
        Me.EinvoiceAckNo.FieldName = Nothing
        Me.EinvoiceAckNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EinvoiceAckNo.isCalculatedField = False
        Me.EinvoiceAckNo.IsSourceFromTable = False
        Me.EinvoiceAckNo.IsSourceFromValueList = False
        Me.EinvoiceAckNo.IsUnique = False
        Me.EinvoiceAckNo.Location = New System.Drawing.Point(84, 41)
        Me.EinvoiceAckNo.MaxLength = 30
        Me.EinvoiceAckNo.MendatroryField = False
        Me.EinvoiceAckNo.MyLinkLable1 = Me.MyLabel15
        Me.EinvoiceAckNo.MyLinkLable2 = Nothing
        Me.EinvoiceAckNo.Name = "EinvoiceAckNo"
        Me.EinvoiceAckNo.ReferenceFieldDesc = Nothing
        Me.EinvoiceAckNo.ReferenceFieldName = Nothing
        Me.EinvoiceAckNo.ReferenceTableName = Nothing
        Me.EinvoiceAckNo.Size = New System.Drawing.Size(300, 18)
        Me.EinvoiceAckNo.TabIndex = 1406
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(20, 42)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel14.TabIndex = 1405
        Me.MyLabel14.Text = "Ack No"
        '
        'EInvoiceIRNNo
        '
        Me.EInvoiceIRNNo.CalculationExpression = Nothing
        Me.EInvoiceIRNNo.FieldCode = Nothing
        Me.EInvoiceIRNNo.FieldDesc = Nothing
        Me.EInvoiceIRNNo.FieldMaxLength = 0
        Me.EInvoiceIRNNo.FieldName = Nothing
        Me.EInvoiceIRNNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EInvoiceIRNNo.isCalculatedField = False
        Me.EInvoiceIRNNo.IsSourceFromTable = False
        Me.EInvoiceIRNNo.IsSourceFromValueList = False
        Me.EInvoiceIRNNo.IsUnique = False
        Me.EInvoiceIRNNo.Location = New System.Drawing.Point(84, 22)
        Me.EInvoiceIRNNo.MaxLength = 30
        Me.EInvoiceIRNNo.MendatroryField = False
        Me.EInvoiceIRNNo.MyLinkLable1 = Me.MyLabel16
        Me.EInvoiceIRNNo.MyLinkLable2 = Nothing
        Me.EInvoiceIRNNo.Name = "EInvoiceIRNNo"
        Me.EInvoiceIRNNo.ReferenceFieldDesc = Nothing
        Me.EInvoiceIRNNo.ReferenceFieldName = Nothing
        Me.EInvoiceIRNNo.ReferenceTableName = Nothing
        Me.EInvoiceIRNNo.Size = New System.Drawing.Size(300, 18)
        Me.EInvoiceIRNNo.TabIndex = 1402
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(18, 23)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel16.TabIndex = 1403
        Me.MyLabel16.Text = "IRN No"
        '=======
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(225, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(77, 22)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
'>>>>>>> d1ec586fa3c5d6f2c6903ff02c7d5301aea8f361
        '
        'btnHistory
        '
        Me.btnHistory.Location = New System.Drawing.Point(638, 2)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(71, 22)
        Me.btnHistory.TabIndex = 37
        Me.btnHistory.Text = "&History"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(514, 3)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(118, 22)
        Me.btnShowInventory.TabIndex = 7
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnInvoiceJE
        '
        Me.btnInvoiceJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvoiceJE.Location = New System.Drawing.Point(390, 3)
        Me.btnInvoiceJE.Name = "btnInvoiceJE"
        Me.btnInvoiceJE.Size = New System.Drawing.Size(118, 22)
        Me.btnInvoiceJE.TabIndex = 6
        Me.btnInvoiceJE.Text = "Show Invoice JE"
        Me.btnInvoiceJE.Visible = False
        '
        'btnReverse
        '
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(308, 3)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(77, 22)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(77, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1018, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(149, 3)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Setting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1092, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'Setting
        '
        Me.Setting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.layoutrbtn, Me.RadMenuItem1, Me.rmiImport, Me.rmiExport})
        Me.Setting.Name = "Setting"
        Me.Setting.Text = "Setting"
        '
        'layoutrbtn
        '
        Me.layoutrbtn.Name = "layoutrbtn"
        Me.layoutrbtn.Text = "Save Layout"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Delete Layout"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export Blank sheet"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1092, 517)
        Me.Panel1.TabIndex = 3
        '
        'frmScrapSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1092, 537)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.MinimumSize = New System.Drawing.Size(890, 467)
        Me.Name = "frmScrapSale"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Material Sale"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkBuyBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporter_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFreightDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCashSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkScrapSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOnHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkinvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSecondryInvNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtppost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcustdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpshipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlInvoiceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVatInvNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInterBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcisable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtscrapinvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpexp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtlocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtref, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtponumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.gvadd.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvadd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtaddamt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage4.PerformLayout()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRound_Off, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoundOff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldocamt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbladdc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbladdcharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotRAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtAfterDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscountAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtWithDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        '<<<<<<< HEAD
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEWayBillDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEwayValidDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEWayBillRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEWayBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEWaybillUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtAckDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EinvoiceBtnUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EInvoiceQrCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EinvoiceAckNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EInvoiceIRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        '=======
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
'>>>>>>> d1ec586fa3c5d6f2c6903ff02c7d5301aea8f361
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtponumber As common.Controls.MyTextBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDueDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents fndShipToLocation As common.UserControls.txtFinder
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents txtTermCode As common.UserControls.txtFinder
    Friend WithEvents txtref As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtcustdesc As common.Controls.MyTextBox
    Friend WithEvents txtlocation As common.Controls.MyTextBox
    Friend WithEvents dtpexp As common.Controls.MyDateTimePicker
    Friend WithEvents dtpshipment As common.Controls.MyDateTimePicker
    Friend WithEvents dtppost As common.Controls.MyDateTimePicker
    Friend WithEvents chkOnHold As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkinvoice As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtaddamt As common.Controls.MyTextBox
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvadd As common.UserControls.MyRadGridView
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents fndcustNo As common.UserControls.txtFinder
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Setting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents layoutrbtn As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtscrapinvoice As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents RadLabel17 As common.Controls.MyLabel
    Friend WithEvents lblTermName As common.Controls.MyLabel
    Friend WithEvents RadLabel16 As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel18 As common.Controls.MyLabel
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents RadLabel14 As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel21 As common.Controls.MyLabel
    Friend WithEvents RadLabel13 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel25 As common.Controls.MyLabel
    Friend WithEvents RadLabel22 As common.Controls.MyLabel
    Friend WithEvents RadLabel19 As common.Controls.MyLabel
    Friend WithEvents lblTotRAmt As common.Controls.MyLabel
    Friend WithEvents lblTaxAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtAfterDiscount As common.Controls.MyLabel
    Friend WithEvents lblDiscountAmt As common.Controls.MyLabel
    Friend WithEvents lblAmtWithDiscount As common.Controls.MyLabel
    Friend WithEvents RadLabel27 As common.Controls.MyLabel
    Friend WithEvents lbladdcharges As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lbldocamt As common.Controls.MyLabel
    Friend WithEvents lbladdc As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel12 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents chkExcisable As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkInterBranch As common.Controls.MyCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblInvoiceNo As common.Controls.MyLabel
    Friend WithEvents txtnrg As common.UserControls.txtFinder
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVehicleDesc As common.Controls.MyTextBox
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents TxtVehicleCode As common.UserControls.txtFinder
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ERP.ucCustomFields
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblDocAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblInvoiceType As common.Controls.MyLabel
    Friend WithEvents ddlInvoiceType As common.Controls.MyComboBox
    Friend WithEvents lblTransporter As common.Controls.MyLabel
    Friend WithEvents txtTransporter_desc As common.Controls.MyLabel
    Friend WithEvents txtTransporter_Code As common.UserControls.txtFinder
    Friend WithEvents UcItemBalance1 As XpertERPEngine.ucItemBalance
    Friend WithEvents lblSecondryInvNo As common.Controls.MyLabel
    Friend WithEvents txtVatInvNo As common.Controls.MyLabel
    Friend WithEvents chkScrapSale As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblNetWeight As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblGrossWeight As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents chkCashSale As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblRound_Off As common.Controls.MyLabel
    Friend WithEvents txtRoundOff As common.Controls.MyLabel
    Friend WithEvents chkTaxable As common.Controls.MyCheckBox
    Friend WithEvents btnInvoiceJE As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtGWeighmentNo As common.UserControls.txtFinder
    Friend WithEvents btnCancel As RadButton
    Friend WithEvents MyLabel59 As common.Controls.MyLabel
    Friend WithEvents txtFreightDistance As common.MyNumBox
    Friend WithEvents txtTCSTaxRate As common.MyNumBox
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents lblActualTCSTaxBaseAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents txttcstaxbaseamount As common.MyNumBox
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents chkBuyBack As RadCheckBox
    '<<<<<<< HEAD
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RadPageViewPage5 As RadPageViewPage
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents txtAckDate As common.Controls.MyDateTimePicker
    Friend WithEvents EinvoiceBtnUpdate As RadButton
    Friend WithEvents EInvoiceQrCode As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents EinvoiceAckNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents EInvoiceIRNNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents txtEwayValidDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtEWayBillRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel38 As common.Controls.MyLabel
    Friend WithEvents txtEWayBillNo As common.Controls.MyTextBox
    Friend WithEvents btnEWaybillUpdate As RadButton
    Friend WithEvents txtEWayBillDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    '=======
    Friend WithEvents btnPrint As RadButton
'>>>>>>> d1ec586fa3c5d6f2c6903ff02c7d5301aea8f361
End Class

