<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkPurchaseInvoice
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.chkHighClassVendor = New Telerik.WinControls.UI.RadCheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.chkJobWork = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.lblSRNNo = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lblMonth = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtMonth = New common.Controls.MyDateTimePicker()
        Me.fndVendor = New common.UserControls.txtFinder()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.TxtVendorUpdate = New common.UserControls.txtFinder()
        Me.chkSRNTrade = New common.Controls.MyCheckBox()
        Me.dtpDocDate = New common.Controls.MyDateTimePicker()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtVendorInvoiceNo = New common.Controls.MyTextBox()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.lblVendorInvoiceNo = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.fndSRNNo = New common.UserControls.txtFinder()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtElectronicRefNo = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.txtewaybilldate = New common.Controls.MyDateTimePicker()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.btnEwaybillupdate = New Telerik.WinControls.UI.RadButton()
        Me.TxtEWayBillNo = New common.Controls.MyTextBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTaxCalManual = New common.Controls.MyRadioButton()
        Me.rbtnTaxCalAutomatic = New common.Controls.MyRadioButton()
        Me.txtTaxGroup = New common.UserControls.txtFinder()
        Me.RadLabel11 = New common.Controls.MyLabel()
        Me.lblTaxGrpName = New common.Controls.MyLabel()
        Me.RadLabel10 = New common.Controls.MyLabel()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtAmtAfterTDS = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtTDSAmt = New common.Controls.MyTextBox()
        Me.txtTCSTaxRate = New common.MyNumBox()
        Me.MyLabel57 = New common.Controls.MyLabel()
        Me.lblActualTCSTaxBaseAmt = New common.Controls.MyLabel()
        Me.MyLabel58 = New common.Controls.MyLabel()
        Me.txttcstaxbaseamount = New common.MyNumBox()
        Me.txtTransporterCharge = New common.Controls.MyTextBox()
        Me.lblTransporterCharge = New common.Controls.MyLabel()
        Me.txtTaxableAmt = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtTaxAmt = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtRoundOffAmt = New common.Controls.MyTextBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtTotalSNFKg = New common.Controls.MyTextBox()
        Me.txtTotalQty = New common.Controls.MyTextBox()
        Me.txtTotalFatKg = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtTotalAmt = New common.Controls.MyTextBox()
        Me.btnViewTDSDetails = New Telerik.WinControls.UI.RadButton()
        Me.btnInvoiceJE = New Telerik.WinControls.UI.RadButton()
        Me.btnBillOfSupply = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdateVendor = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.chkHighClassVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSRNTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtElectronicRefNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnEwaybillupdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnTaxCalManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnTaxCalAutomatic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxGrpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmtAfterTDS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTDSAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransporterCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxableAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoundOffAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalSNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalFatKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBillOfSupply, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdateVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnViewTDSDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnInvoiceJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBillOfSupply)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdateVendor)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1135, 429)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView1.Size = New System.Drawing.Size(1135, 399)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1114, 353)
        Me.RadPageViewPage1.Text = "Invoice Detail"
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
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkHighClassVendor)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblSRNNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndDocNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblMonth)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtMonth)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndVendor)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TxtVendorUpdate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblWeighmentNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkSRNTrade)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpDocDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblSRNDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtVendorInvoiceNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblVendorInvoiceNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpFromDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndSRNNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer3.Size = New System.Drawing.Size(1114, 353)
        Me.SplitContainer3.SplitterDistance = 137
        Me.SplitContainer3.TabIndex = 0
        '
        'chkHighClassVendor
        '
        Me.chkHighClassVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHighClassVendor.Location = New System.Drawing.Point(746, 26)
        Me.chkHighClassVendor.Name = "chkHighClassVendor"
        Me.chkHighClassVendor.ReadOnly = True
        Me.chkHighClassVendor.Size = New System.Drawing.Size(115, 16)
        Me.chkHighClassVendor.TabIndex = 361
        Me.chkHighClassVendor.Text = "High Class Vendor"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblSubLocation)
        Me.Panel3.Controls.Add(Me.chkJobWork)
        Me.Panel3.Controls.Add(Me.MyLabel16)
        Me.Panel3.Controls.Add(Me.txtSubLocation)
        Me.Panel3.Location = New System.Drawing.Point(744, 44)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(365, 44)
        Me.Panel3.TabIndex = 360
        Me.Panel3.Visible = False
        '
        'lblSubLocation
        '
        Me.lblSubLocation.AutoSize = False
        Me.lblSubLocation.BorderVisible = True
        Me.lblSubLocation.FieldName = Nothing
        Me.lblSubLocation.Location = New System.Drawing.Point(213, 21)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(140, 19)
        Me.lblSubLocation.TabIndex = 276
        '
        'chkJobWork
        '
        Me.chkJobWork.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWork.Location = New System.Drawing.Point(3, 4)
        Me.chkJobWork.Name = "chkJobWork"
        Me.chkJobWork.Size = New System.Drawing.Size(68, 16)
        Me.chkJobWork.TabIndex = 346
        Me.chkJobWork.Text = "Job Work"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(3, 21)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel16.TabIndex = 274
        Me.MyLabel16.Text = "Sub Location"
        '
        'txtSubLocation
        '
        Me.txtSubLocation.CalculationExpression = Nothing
        Me.txtSubLocation.FieldCode = Nothing
        Me.txtSubLocation.FieldDesc = Nothing
        Me.txtSubLocation.FieldMaxLength = 0
        Me.txtSubLocation.FieldName = Nothing
        Me.txtSubLocation.isCalculatedField = False
        Me.txtSubLocation.IsSourceFromTable = False
        Me.txtSubLocation.IsSourceFromValueList = False
        Me.txtSubLocation.IsUnique = False
        Me.txtSubLocation.Location = New System.Drawing.Point(81, 20)
        Me.txtSubLocation.MendatroryField = True
        Me.txtSubLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubLocation.MyLinkLable1 = Nothing
        Me.txtSubLocation.MyLinkLable2 = Nothing
        Me.txtSubLocation.MyReadOnly = False
        Me.txtSubLocation.MyShowMasterFormButton = False
        Me.txtSubLocation.Name = "txtSubLocation"
        Me.txtSubLocation.ReferenceFieldDesc = Nothing
        Me.txtSubLocation.ReferenceFieldName = Nothing
        Me.txtSubLocation.ReferenceTableName = Nothing
        Me.txtSubLocation.Size = New System.Drawing.Size(124, 20)
        Me.txtSubLocation.TabIndex = 275
        Me.txtSubLocation.Value = ""
        '
        'lblSRNNo
        '
        Me.lblSRNNo.FieldName = Nothing
        Me.lblSRNNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSRNNo.Location = New System.Drawing.Point(3, 4)
        Me.lblSRNNo.Name = "lblSRNNo"
        Me.lblSRNNo.Size = New System.Drawing.Size(47, 16)
        Me.lblSRNNo.TabIndex = 52
        Me.lblSRNNo.Text = "Doc No."
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(98, 2)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblSRNNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 50
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(355, 20)
        Me.fndDocNo.TabIndex = 0
        Me.fndDocNo.Value = ""
        '
        'lblMonth
        '
        Me.lblMonth.FieldName = Nothing
        Me.lblMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(478, 68)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(38, 16)
        Me.lblMonth.TabIndex = 352
        Me.lblMonth.Text = "Month"
        Me.lblMonth.Visible = False
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(646, 2)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(98, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 53
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
        Me.txtMonth.Location = New System.Drawing.Point(547, 66)
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Me.lblMonth
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.Size = New System.Drawing.Size(108, 18)
        Me.txtMonth.TabIndex = 351
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Sep - 2014"
        Me.txtMonth.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        Me.txtMonth.Visible = False
        '
        'fndVendor
        '
        Me.fndVendor.CalculationExpression = Nothing
        Me.fndVendor.FieldCode = Nothing
        Me.fndVendor.FieldDesc = Nothing
        Me.fndVendor.FieldMaxLength = 0
        Me.fndVendor.FieldName = Nothing
        Me.fndVendor.isCalculatedField = False
        Me.fndVendor.IsSourceFromTable = False
        Me.fndVendor.IsSourceFromValueList = False
        Me.fndVendor.IsUnique = False
        Me.fndVendor.Location = New System.Drawing.Point(97, 24)
        Me.fndVendor.MendatroryField = True
        Me.fndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor.MyLinkLable1 = Me.lblWeighmentNo
        Me.fndVendor.MyLinkLable2 = Nothing
        Me.fndVendor.MyReadOnly = False
        Me.fndVendor.MyShowMasterFormButton = False
        Me.fndVendor.Name = "fndVendor"
        Me.fndVendor.ReferenceFieldDesc = Nothing
        Me.fndVendor.ReferenceFieldName = Nothing
        Me.fndVendor.ReferenceTableName = Nothing
        Me.fndVendor.Size = New System.Drawing.Size(377, 19)
        Me.fndVendor.TabIndex = 2
        Me.fndVendor.Value = ""
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblWeighmentNo.Location = New System.Drawing.Point(3, 24)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(43, 18)
        Me.lblWeighmentNo.TabIndex = 54
        Me.lblWeighmentNo.Text = "Vendor"
        '
        'TxtVendorUpdate
        '
        Me.TxtVendorUpdate.CalculationExpression = Nothing
        Me.TxtVendorUpdate.FieldCode = Nothing
        Me.TxtVendorUpdate.FieldDesc = Nothing
        Me.TxtVendorUpdate.FieldMaxLength = 0
        Me.TxtVendorUpdate.FieldName = Nothing
        Me.TxtVendorUpdate.isCalculatedField = False
        Me.TxtVendorUpdate.IsSourceFromTable = False
        Me.TxtVendorUpdate.IsSourceFromValueList = False
        Me.TxtVendorUpdate.IsUnique = False
        Me.TxtVendorUpdate.Location = New System.Drawing.Point(98, 23)
        Me.TxtVendorUpdate.MendatroryField = False
        Me.TxtVendorUpdate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorUpdate.MyLinkLable1 = Nothing
        Me.TxtVendorUpdate.MyLinkLable2 = Nothing
        Me.TxtVendorUpdate.MyReadOnly = False
        Me.TxtVendorUpdate.MyShowMasterFormButton = False
        Me.TxtVendorUpdate.Name = "TxtVendorUpdate"
        Me.TxtVendorUpdate.ReferenceFieldDesc = Nothing
        Me.TxtVendorUpdate.ReferenceFieldName = Nothing
        Me.TxtVendorUpdate.ReferenceTableName = Nothing
        Me.TxtVendorUpdate.Size = New System.Drawing.Size(354, 19)
        Me.TxtVendorUpdate.TabIndex = 350
        Me.TxtVendorUpdate.Value = ""
        Me.TxtVendorUpdate.Visible = False
        '
        'chkSRNTrade
        '
        Me.chkSRNTrade.Location = New System.Drawing.Point(661, 65)
        Me.chkSRNTrade.MyLinkLable1 = Nothing
        Me.chkSRNTrade.MyLinkLable2 = Nothing
        Me.chkSRNTrade.Name = "chkSRNTrade"
        Me.chkSRNTrade.Size = New System.Drawing.Size(73, 18)
        Me.chkSRNTrade.TabIndex = 349
        Me.chkSRNTrade.Tag1 = Nothing
        Me.chkSRNTrade.Text = "SRN Trade"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CalculationExpression = Nothing
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocDate.FieldCode = Nothing
        Me.dtpDocDate.FieldDesc = Nothing
        Me.dtpDocDate.FieldMaxLength = 0
        Me.dtpDocDate.FieldName = Nothing
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.isCalculatedField = False
        Me.dtpDocDate.IsSourceFromTable = False
        Me.dtpDocDate.IsSourceFromValueList = False
        Me.dtpDocDate.IsUnique = False
        Me.dtpDocDate.Location = New System.Drawing.Point(543, 3)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblSRNDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.ReferenceFieldDesc = Nothing
        Me.dtpDocDate.ReferenceFieldName = Nothing
        Me.dtpDocDate.ReferenceTableName = Nothing
        Me.dtpDocDate.Size = New System.Drawing.Size(87, 18)
        Me.dtpDocDate.TabIndex = 1
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(478, 4)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(53, 16)
        Me.lblSRNDate.TabIndex = 55
        Me.lblSRNDate.Text = "Doc Date"
        '
        'btnReset
        '
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(453, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 20)
        Me.btnReset.TabIndex = 56
        '
        'txtVendorInvoiceNo
        '
        Me.txtVendorInvoiceNo.CalculationExpression = Nothing
        Me.txtVendorInvoiceNo.FieldCode = Nothing
        Me.txtVendorInvoiceNo.FieldDesc = Nothing
        Me.txtVendorInvoiceNo.FieldMaxLength = 0
        Me.txtVendorInvoiceNo.FieldName = Nothing
        Me.txtVendorInvoiceNo.isCalculatedField = False
        Me.txtVendorInvoiceNo.IsSourceFromTable = False
        Me.txtVendorInvoiceNo.IsSourceFromValueList = False
        Me.txtVendorInvoiceNo.IsUnique = False
        Me.txtVendorInvoiceNo.Location = New System.Drawing.Point(558, 86)
        Me.txtVendorInvoiceNo.MaxLength = 50
        Me.txtVendorInvoiceNo.MendatroryField = True
        Me.txtVendorInvoiceNo.MyLinkLable1 = Nothing
        Me.txtVendorInvoiceNo.MyLinkLable2 = Nothing
        Me.txtVendorInvoiceNo.Name = "txtVendorInvoiceNo"
        Me.txtVendorInvoiceNo.ReferenceFieldDesc = Nothing
        Me.txtVendorInvoiceNo.ReferenceFieldName = Nothing
        Me.txtVendorInvoiceNo.ReferenceTableName = Nothing
        Me.txtVendorInvoiceNo.Size = New System.Drawing.Size(187, 20)
        Me.txtVendorInvoiceNo.TabIndex = 345
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(475, 23)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(269, 21)
        Me.lblVendorName.TabIndex = 324
        '
        'lblVendorInvoiceNo
        '
        Me.lblVendorInvoiceNo.FieldName = Nothing
        Me.lblVendorInvoiceNo.Location = New System.Drawing.Point(478, 87)
        Me.lblVendorInvoiceNo.Name = "lblVendorInvoiceNo"
        Me.lblVendorInvoiceNo.Size = New System.Drawing.Size(79, 18)
        Me.lblVendorInvoiceNo.TabIndex = 346
        Me.lblVendorInvoiceNo.Text = "Vendor Inv No"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(475, 44)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(269, 21)
        Me.lblLocationName.TabIndex = 344
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(3, 45)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel8.TabIndex = 343
        Me.MyLabel8.Text = "Location"
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
        Me.fndLocation.Location = New System.Drawing.Point(97, 45)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Me.MyLabel8
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(377, 19)
        Me.fndLocation.TabIndex = 3
        Me.fndLocation.Value = ""
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(334, 66)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel5
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(140, 18)
        Me.dtpToDate.TabIndex = 5
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(264, 67)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel5.TabIndex = 339
        Me.MyLabel5.Text = "To Date"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(5, 68)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel4.TabIndex = 337
        Me.MyLabel4.Text = "From Date"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(4, 88)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel6.TabIndex = 341
        Me.MyLabel6.Text = "SRN"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(97, 66)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.MyLabel4
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(145, 18)
        Me.dtpFromDate.TabIndex = 4
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'fndSRNNo
        '
        Me.fndSRNNo.CalculationExpression = Nothing
        Me.fndSRNNo.FieldCode = Nothing
        Me.fndSRNNo.FieldDesc = Nothing
        Me.fndSRNNo.FieldMaxLength = 0
        Me.fndSRNNo.FieldName = Nothing
        Me.fndSRNNo.isCalculatedField = False
        Me.fndSRNNo.IsSourceFromTable = False
        Me.fndSRNNo.IsSourceFromValueList = False
        Me.fndSRNNo.IsUnique = False
        Me.fndSRNNo.Location = New System.Drawing.Point(97, 87)
        Me.fndSRNNo.MendatroryField = True
        Me.fndSRNNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSRNNo.MyLinkLable1 = Me.MyLabel6
        Me.fndSRNNo.MyLinkLable2 = Nothing
        Me.fndSRNNo.MyReadOnly = False
        Me.fndSRNNo.MyShowMasterFormButton = False
        Me.fndSRNNo.Name = "fndSRNNo"
        Me.fndSRNNo.ReferenceFieldDesc = Nothing
        Me.fndSRNNo.ReferenceFieldName = Nothing
        Me.fndSRNNo.ReferenceTableName = Nothing
        Me.fndSRNNo.Size = New System.Drawing.Size(377, 19)
        Me.fndSRNNo.TabIndex = 6
        Me.fndSRNNo.Value = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtElectronicRefNo)
        Me.GroupBox2.Controls.Add(Me.MyLabel9)
        Me.GroupBox2.Controls.Add(Me.txtewaybilldate)
        Me.GroupBox2.Controls.Add(Me.MyLabel10)
        Me.GroupBox2.Controls.Add(Me.btnEwaybillupdate)
        Me.GroupBox2.Controls.Add(Me.TxtEWayBillNo)
        Me.GroupBox2.Controls.Add(Me.MyLabel20)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 104)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(740, 30)
        Me.GroupBox2.TabIndex = 344
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GST"
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
        Me.txtElectronicRefNo.Location = New System.Drawing.Point(532, 9)
        Me.txtElectronicRefNo.MaxLength = 10
        Me.txtElectronicRefNo.MendatroryField = False
        Me.txtElectronicRefNo.MyLinkLable1 = Nothing
        Me.txtElectronicRefNo.MyLinkLable2 = Nothing
        Me.txtElectronicRefNo.Name = "txtElectronicRefNo"
        Me.txtElectronicRefNo.ReferenceFieldDesc = Nothing
        Me.txtElectronicRefNo.ReferenceFieldName = Nothing
        Me.txtElectronicRefNo.ReferenceTableName = Nothing
        Me.txtElectronicRefNo.Size = New System.Drawing.Size(130, 18)
        Me.txtElectronicRefNo.TabIndex = 336
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(473, 10)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel9.TabIndex = 337
        Me.MyLabel9.Text = "E-Ref. No"
        '
        'txtewaybilldate
        '
        Me.txtewaybilldate.CalculationExpression = Nothing
        Me.txtewaybilldate.CustomFormat = "dd/MM/yyyy"
        Me.txtewaybilldate.FieldCode = Nothing
        Me.txtewaybilldate.FieldDesc = Nothing
        Me.txtewaybilldate.FieldMaxLength = 0
        Me.txtewaybilldate.FieldName = Nothing
        Me.txtewaybilldate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtewaybilldate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtewaybilldate.isCalculatedField = False
        Me.txtewaybilldate.IsSourceFromTable = False
        Me.txtewaybilldate.IsSourceFromValueList = False
        Me.txtewaybilldate.IsUnique = False
        Me.txtewaybilldate.Location = New System.Drawing.Point(372, 9)
        Me.txtewaybilldate.MendatroryField = False
        Me.txtewaybilldate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtewaybilldate.MyLinkLable1 = Nothing
        Me.txtewaybilldate.MyLinkLable2 = Nothing
        Me.txtewaybilldate.Name = "txtewaybilldate"
        Me.txtewaybilldate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtewaybilldate.ReferenceFieldDesc = Nothing
        Me.txtewaybilldate.ReferenceFieldName = Nothing
        Me.txtewaybilldate.ReferenceTableName = Nothing
        Me.txtewaybilldate.ShowCheckBox = True
        Me.txtewaybilldate.Size = New System.Drawing.Size(97, 18)
        Me.txtewaybilldate.TabIndex = 335
        Me.txtewaybilldate.TabStop = False
        Me.txtewaybilldate.Text = "13/06/2011"
        Me.txtewaybilldate.Value = New Date(2011, 6, 13, 0, 0, 0, 0)
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(287, 10)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel10.TabIndex = 334
        Me.MyLabel10.Text = "E-WayBill Date"
        '
        'btnEwaybillupdate
        '
        Me.btnEwaybillupdate.Location = New System.Drawing.Point(666, 8)
        Me.btnEwaybillupdate.Name = "btnEwaybillupdate"
        Me.btnEwaybillupdate.Size = New System.Drawing.Size(73, 20)
        Me.btnEwaybillupdate.TabIndex = 333
        Me.btnEwaybillupdate.Text = "Update"
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
        Me.TxtEWayBillNo.Location = New System.Drawing.Point(93, 9)
        Me.TxtEWayBillNo.MaxLength = 30
        Me.TxtEWayBillNo.MendatroryField = False
        Me.TxtEWayBillNo.MyLinkLable1 = Nothing
        Me.TxtEWayBillNo.MyLinkLable2 = Nothing
        Me.TxtEWayBillNo.Name = "TxtEWayBillNo"
        Me.TxtEWayBillNo.ReferenceFieldDesc = Nothing
        Me.TxtEWayBillNo.ReferenceFieldName = Nothing
        Me.TxtEWayBillNo.ReferenceTableName = Nothing
        Me.TxtEWayBillNo.Size = New System.Drawing.Size(190, 18)
        Me.TxtEWayBillNo.TabIndex = 331
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(4, 10)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(73, 16)
        Me.MyLabel20.TabIndex = 332
        Me.MyLabel20.Text = "E-WayBill No"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1114, 212)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage2.Controls.Add(Me.txtTaxGroup)
        Me.RadPageViewPage2.Controls.Add(Me.lblTaxGrpName)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.gv2)
        Me.RadPageViewPage2.Controls.Add(Me.RadLabel11)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(35.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1114, 353)
        Me.RadPageViewPage2.Text = "Tax"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalManual)
        Me.GroupBox1.Controls.Add(Me.rbtnTaxCalAutomatic)
        Me.GroupBox1.Location = New System.Drawing.Point(547, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 36)
        Me.GroupBox1.TabIndex = 8
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
        Me.txtTaxGroup.Location = New System.Drawing.Point(69, 7)
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
        Me.txtTaxGroup.TabIndex = 7
        Me.txtTaxGroup.Value = ""
        '
        'RadLabel11
        '
        Me.RadLabel11.FieldName = Nothing
        Me.RadLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel11.Location = New System.Drawing.Point(3, 8)
        Me.RadLabel11.Name = "RadLabel11"
        Me.RadLabel11.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel11.TabIndex = 11
        Me.RadLabel11.Text = "Tax Group"
        '
        'lblTaxGrpName
        '
        Me.lblTaxGrpName.AutoSize = False
        Me.lblTaxGrpName.BorderVisible = True
        Me.lblTaxGrpName.FieldName = Nothing
        Me.lblTaxGrpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxGrpName.Location = New System.Drawing.Point(220, 7)
        Me.lblTaxGrpName.Name = "lblTaxGrpName"
        Me.lblTaxGrpName.Size = New System.Drawing.Size(321, 19)
        Me.lblTaxGrpName.TabIndex = 12
        Me.lblTaxGrpName.TextWrap = False
        '
        'RadLabel10
        '
        Me.RadLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadLabel10.FieldName = Nothing
        Me.RadLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel10.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RadLabel10.Location = New System.Drawing.Point(958, 336)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(155, 16)
        Me.RadLabel10.TabIndex = 9
        Me.RadLabel10.Text = "Double click To Chage Rate"
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
        Me.gv2.Location = New System.Drawing.Point(2, 38)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowAddNewRow = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(1109, 292)
        Me.gv2.TabIndex = 10
        Me.gv2.TabStop = False
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage3.Controls.Add(Me.txtAmtAfterTDS)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage3.Controls.Add(Me.txtTDSAmt)
        Me.RadPageViewPage3.Controls.Add(Me.txtTCSTaxRate)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel57)
        Me.RadPageViewPage3.Controls.Add(Me.lblActualTCSTaxBaseAmt)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel58)
        Me.RadPageViewPage3.Controls.Add(Me.txttcstaxbaseamount)
        Me.RadPageViewPage3.Controls.Add(Me.txtTransporterCharge)
        Me.RadPageViewPage3.Controls.Add(Me.lblTransporterCharge)
        Me.RadPageViewPage3.Controls.Add(Me.txtTaxableAmt)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel11)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage3.Controls.Add(Me.txtTaxAmt)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage3.Controls.Add(Me.txtRoundOffAmt)
        Me.RadPageViewPage3.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage3.Controls.Add(Me.txtTotalSNFKg)
        Me.RadPageViewPage3.Controls.Add(Me.txtTotalQty)
        Me.RadPageViewPage3.Controls.Add(Me.txtTotalFatKg)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage3.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage3.Controls.Add(Me.txtTotalAmt)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(47.0!, 26.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1114, 353)
        Me.RadPageViewPage3.Text = "Totals"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(27, 214)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(97, 18)
        Me.MyLabel14.TabIndex = 1409
        Me.MyLabel14.Text = "Amount After TDS"
        '
        'txtAmtAfterTDS
        '
        Me.txtAmtAfterTDS.CalculationExpression = Nothing
        Me.txtAmtAfterTDS.FieldCode = Nothing
        Me.txtAmtAfterTDS.FieldDesc = Nothing
        Me.txtAmtAfterTDS.FieldMaxLength = 0
        Me.txtAmtAfterTDS.FieldName = Nothing
        Me.txtAmtAfterTDS.isCalculatedField = False
        Me.txtAmtAfterTDS.IsSourceFromTable = False
        Me.txtAmtAfterTDS.IsSourceFromValueList = False
        Me.txtAmtAfterTDS.IsUnique = False
        Me.txtAmtAfterTDS.Location = New System.Drawing.Point(130, 213)
        Me.txtAmtAfterTDS.MaxLength = 50
        Me.txtAmtAfterTDS.MendatroryField = True
        Me.txtAmtAfterTDS.MyLinkLable1 = Nothing
        Me.txtAmtAfterTDS.MyLinkLable2 = Nothing
        Me.txtAmtAfterTDS.Name = "txtAmtAfterTDS"
        Me.txtAmtAfterTDS.ReadOnly = True
        Me.txtAmtAfterTDS.ReferenceFieldDesc = Nothing
        Me.txtAmtAfterTDS.ReferenceFieldName = Nothing
        Me.txtAmtAfterTDS.ReferenceTableName = Nothing
        Me.txtAmtAfterTDS.Size = New System.Drawing.Size(154, 20)
        Me.txtAmtAfterTDS.TabIndex = 1408
        Me.txtAmtAfterTDS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(54, 190)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(70, 18)
        Me.MyLabel13.TabIndex = 1407
        Me.MyLabel13.Text = "TDS Amount"
        '
        'txtTDSAmt
        '
        Me.txtTDSAmt.CalculationExpression = Nothing
        Me.txtTDSAmt.FieldCode = Nothing
        Me.txtTDSAmt.FieldDesc = Nothing
        Me.txtTDSAmt.FieldMaxLength = 0
        Me.txtTDSAmt.FieldName = Nothing
        Me.txtTDSAmt.isCalculatedField = False
        Me.txtTDSAmt.IsSourceFromTable = False
        Me.txtTDSAmt.IsSourceFromValueList = False
        Me.txtTDSAmt.IsUnique = False
        Me.txtTDSAmt.Location = New System.Drawing.Point(130, 189)
        Me.txtTDSAmt.MaxLength = 50
        Me.txtTDSAmt.MendatroryField = True
        Me.txtTDSAmt.MyLinkLable1 = Nothing
        Me.txtTDSAmt.MyLinkLable2 = Nothing
        Me.txtTDSAmt.Name = "txtTDSAmt"
        Me.txtTDSAmt.ReadOnly = True
        Me.txtTDSAmt.ReferenceFieldDesc = Nothing
        Me.txtTDSAmt.ReferenceFieldName = Nothing
        Me.txtTDSAmt.ReferenceTableName = Nothing
        Me.txtTDSAmt.Size = New System.Drawing.Size(154, 20)
        Me.txtTDSAmt.TabIndex = 1406
        Me.txtTDSAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtTCSTaxRate.Location = New System.Drawing.Point(426, 69)
        Me.txtTCSTaxRate.MendatroryField = False
        Me.txtTCSTaxRate.MyLinkLable1 = Nothing
        Me.txtTCSTaxRate.MyLinkLable2 = Nothing
        Me.txtTCSTaxRate.Name = "txtTCSTaxRate"
        Me.txtTCSTaxRate.ReadOnly = True
        Me.txtTCSTaxRate.ReferenceFieldDesc = Nothing
        Me.txtTCSTaxRate.ReferenceFieldName = Nothing
        Me.txtTCSTaxRate.ReferenceTableName = Nothing
        Me.txtTCSTaxRate.Size = New System.Drawing.Size(115, 20)
        Me.txtTCSTaxRate.TabIndex = 1405
        Me.txtTCSTaxRate.Text = "0"
        Me.txtTCSTaxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTCSTaxRate.Value = 0R
        Me.txtTCSTaxRate.Visible = False
        '
        'MyLabel57
        '
        Me.MyLabel57.FieldName = Nothing
        Me.MyLabel57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel57.Location = New System.Drawing.Point(287, 23)
        Me.MyLabel57.Name = "MyLabel57"
        Me.MyLabel57.Size = New System.Drawing.Size(138, 16)
        Me.MyLabel57.TabIndex = 1404
        Me.MyLabel57.Text = "Actual TCS Tax Base Amt"
        '
        'lblActualTCSTaxBaseAmt
        '
        Me.lblActualTCSTaxBaseAmt.AutoSize = False
        Me.lblActualTCSTaxBaseAmt.BorderVisible = True
        Me.lblActualTCSTaxBaseAmt.FieldName = Nothing
        Me.lblActualTCSTaxBaseAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualTCSTaxBaseAmt.Location = New System.Drawing.Point(426, 22)
        Me.lblActualTCSTaxBaseAmt.Name = "lblActualTCSTaxBaseAmt"
        Me.lblActualTCSTaxBaseAmt.Size = New System.Drawing.Size(115, 18)
        Me.lblActualTCSTaxBaseAmt.TabIndex = 1403
        Me.lblActualTCSTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel58
        '
        Me.MyLabel58.FieldName = Nothing
        Me.MyLabel58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel58.Location = New System.Drawing.Point(287, 47)
        Me.MyLabel58.Name = "MyLabel58"
        Me.MyLabel58.Size = New System.Drawing.Size(122, 16)
        Me.MyLabel58.TabIndex = 1402
        Me.MyLabel58.Text = "TCS Tax Base Amount"
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
        Me.txttcstaxbaseamount.Location = New System.Drawing.Point(426, 45)
        Me.txttcstaxbaseamount.MendatroryField = False
        Me.txttcstaxbaseamount.MyLinkLable1 = Nothing
        Me.txttcstaxbaseamount.MyLinkLable2 = Nothing
        Me.txttcstaxbaseamount.Name = "txttcstaxbaseamount"
        Me.txttcstaxbaseamount.ReadOnly = True
        Me.txttcstaxbaseamount.ReferenceFieldDesc = Nothing
        Me.txttcstaxbaseamount.ReferenceFieldName = Nothing
        Me.txttcstaxbaseamount.ReferenceTableName = Nothing
        Me.txttcstaxbaseamount.Size = New System.Drawing.Size(115, 20)
        Me.txttcstaxbaseamount.TabIndex = 1401
        Me.txttcstaxbaseamount.Text = "0"
        Me.txttcstaxbaseamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttcstaxbaseamount.Value = 0R
        '
        'txtTransporterCharge
        '
        Me.txtTransporterCharge.CalculationExpression = Nothing
        Me.txtTransporterCharge.FieldCode = Nothing
        Me.txtTransporterCharge.FieldDesc = Nothing
        Me.txtTransporterCharge.FieldMaxLength = 0
        Me.txtTransporterCharge.FieldName = Nothing
        Me.txtTransporterCharge.isCalculatedField = False
        Me.txtTransporterCharge.IsSourceFromTable = False
        Me.txtTransporterCharge.IsSourceFromValueList = False
        Me.txtTransporterCharge.IsUnique = False
        Me.txtTransporterCharge.Location = New System.Drawing.Point(426, 143)
        Me.txtTransporterCharge.MaxLength = 50
        Me.txtTransporterCharge.MendatroryField = True
        Me.txtTransporterCharge.MyLinkLable1 = Nothing
        Me.txtTransporterCharge.MyLinkLable2 = Nothing
        Me.txtTransporterCharge.Name = "txtTransporterCharge"
        Me.txtTransporterCharge.ReadOnly = True
        Me.txtTransporterCharge.ReferenceFieldDesc = Nothing
        Me.txtTransporterCharge.ReferenceFieldName = Nothing
        Me.txtTransporterCharge.ReferenceTableName = Nothing
        Me.txtTransporterCharge.Size = New System.Drawing.Size(115, 20)
        Me.txtTransporterCharge.TabIndex = 354
        Me.txtTransporterCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTransporterCharge
        '
        Me.lblTransporterCharge.FieldName = Nothing
        Me.lblTransporterCharge.Location = New System.Drawing.Point(287, 143)
        Me.lblTransporterCharge.Name = "lblTransporterCharge"
        Me.lblTransporterCharge.Size = New System.Drawing.Size(103, 18)
        Me.lblTransporterCharge.TabIndex = 353
        Me.lblTransporterCharge.Text = "Transporter Charge"
        '
        'txtTaxableAmt
        '
        Me.txtTaxableAmt.CalculationExpression = Nothing
        Me.txtTaxableAmt.FieldCode = Nothing
        Me.txtTaxableAmt.FieldDesc = Nothing
        Me.txtTaxableAmt.FieldMaxLength = 0
        Me.txtTaxableAmt.FieldName = Nothing
        Me.txtTaxableAmt.isCalculatedField = False
        Me.txtTaxableAmt.IsSourceFromTable = False
        Me.txtTaxableAmt.IsSourceFromValueList = False
        Me.txtTaxableAmt.IsUnique = False
        Me.txtTaxableAmt.Location = New System.Drawing.Point(130, 93)
        Me.txtTaxableAmt.MaxLength = 50
        Me.txtTaxableAmt.MendatroryField = True
        Me.txtTaxableAmt.MyLinkLable1 = Nothing
        Me.txtTaxableAmt.MyLinkLable2 = Nothing
        Me.txtTaxableAmt.Name = "txtTaxableAmt"
        Me.txtTaxableAmt.ReadOnly = True
        Me.txtTaxableAmt.ReferenceFieldDesc = Nothing
        Me.txtTaxableAmt.ReferenceFieldName = Nothing
        Me.txtTaxableAmt.ReferenceTableName = Nothing
        Me.txtTaxableAmt.Size = New System.Drawing.Size(154, 20)
        Me.txtTaxableAmt.TabIndex = 349
        Me.txtTaxableAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(36, 94)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(88, 18)
        Me.MyLabel11.TabIndex = 350
        Me.MyLabel11.Text = "Taxable Amount"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(57, 118)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel12.TabIndex = 352
        Me.MyLabel12.Text = "Tax Amount"
        '
        'txtTaxAmt
        '
        Me.txtTaxAmt.CalculationExpression = Nothing
        Me.txtTaxAmt.FieldCode = Nothing
        Me.txtTaxAmt.FieldDesc = Nothing
        Me.txtTaxAmt.FieldMaxLength = 0
        Me.txtTaxAmt.FieldName = Nothing
        Me.txtTaxAmt.isCalculatedField = False
        Me.txtTaxAmt.IsSourceFromTable = False
        Me.txtTaxAmt.IsSourceFromValueList = False
        Me.txtTaxAmt.IsUnique = False
        Me.txtTaxAmt.Location = New System.Drawing.Point(130, 117)
        Me.txtTaxAmt.MaxLength = 50
        Me.txtTaxAmt.MendatroryField = True
        Me.txtTaxAmt.MyLinkLable1 = Nothing
        Me.txtTaxAmt.MyLinkLable2 = Nothing
        Me.txtTaxAmt.Name = "txtTaxAmt"
        Me.txtTaxAmt.ReadOnly = True
        Me.txtTaxAmt.ReferenceFieldDesc = Nothing
        Me.txtTaxAmt.ReferenceFieldName = Nothing
        Me.txtTaxAmt.ReferenceTableName = Nothing
        Me.txtTaxAmt.Size = New System.Drawing.Size(154, 20)
        Me.txtTaxAmt.TabIndex = 351
        Me.txtTaxAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(69, 70)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel3.TabIndex = 333
        Me.MyLabel3.Text = "Total QTY"
        '
        'txtRoundOffAmt
        '
        Me.txtRoundOffAmt.CalculationExpression = Nothing
        Me.txtRoundOffAmt.FieldCode = Nothing
        Me.txtRoundOffAmt.FieldDesc = Nothing
        Me.txtRoundOffAmt.FieldMaxLength = 0
        Me.txtRoundOffAmt.FieldName = Nothing
        Me.txtRoundOffAmt.isCalculatedField = False
        Me.txtRoundOffAmt.IsSourceFromTable = False
        Me.txtRoundOffAmt.IsSourceFromValueList = False
        Me.txtRoundOffAmt.IsUnique = False
        Me.txtRoundOffAmt.Location = New System.Drawing.Point(130, 141)
        Me.txtRoundOffAmt.MaxLength = 50
        Me.txtRoundOffAmt.MendatroryField = True
        Me.txtRoundOffAmt.MyLinkLable1 = Nothing
        Me.txtRoundOffAmt.MyLinkLable2 = Nothing
        Me.txtRoundOffAmt.Name = "txtRoundOffAmt"
        Me.txtRoundOffAmt.ReadOnly = True
        Me.txtRoundOffAmt.ReferenceFieldDesc = Nothing
        Me.txtRoundOffAmt.ReferenceFieldName = Nothing
        Me.txtRoundOffAmt.ReferenceTableName = Nothing
        Me.txtRoundOffAmt.Size = New System.Drawing.Size(154, 20)
        Me.txtRoundOffAmt.TabIndex = 10
        Me.txtRoundOffAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(54, 22)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(70, 18)
        Me.lblLocation.TabIndex = 329
        Me.lblLocation.Text = "Total FAT KG"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(42, 142)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel2.TabIndex = 335
        Me.MyLabel2.Text = "Round Off Amt"
        '
        'txtTotalSNFKg
        '
        Me.txtTotalSNFKg.CalculationExpression = Nothing
        Me.txtTotalSNFKg.FieldCode = Nothing
        Me.txtTotalSNFKg.FieldDesc = Nothing
        Me.txtTotalSNFKg.FieldMaxLength = 0
        Me.txtTotalSNFKg.FieldName = Nothing
        Me.txtTotalSNFKg.isCalculatedField = False
        Me.txtTotalSNFKg.IsSourceFromTable = False
        Me.txtTotalSNFKg.IsSourceFromValueList = False
        Me.txtTotalSNFKg.IsUnique = False
        Me.txtTotalSNFKg.Location = New System.Drawing.Point(130, 45)
        Me.txtTotalSNFKg.MaxLength = 50
        Me.txtTotalSNFKg.MendatroryField = True
        Me.txtTotalSNFKg.MyLinkLable1 = Nothing
        Me.txtTotalSNFKg.MyLinkLable2 = Nothing
        Me.txtTotalSNFKg.Name = "txtTotalSNFKg"
        Me.txtTotalSNFKg.ReadOnly = True
        Me.txtTotalSNFKg.ReferenceFieldDesc = Nothing
        Me.txtTotalSNFKg.ReferenceFieldName = Nothing
        Me.txtTotalSNFKg.ReferenceTableName = Nothing
        Me.txtTotalSNFKg.Size = New System.Drawing.Size(154, 20)
        Me.txtTotalSNFKg.TabIndex = 8
        Me.txtTotalSNFKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalQty
        '
        Me.txtTotalQty.CalculationExpression = Nothing
        Me.txtTotalQty.FieldCode = Nothing
        Me.txtTotalQty.FieldDesc = Nothing
        Me.txtTotalQty.FieldMaxLength = 0
        Me.txtTotalQty.FieldName = Nothing
        Me.txtTotalQty.isCalculatedField = False
        Me.txtTotalQty.IsSourceFromTable = False
        Me.txtTotalQty.IsSourceFromValueList = False
        Me.txtTotalQty.IsUnique = False
        Me.txtTotalQty.Location = New System.Drawing.Point(130, 69)
        Me.txtTotalQty.MaxLength = 50
        Me.txtTotalQty.MendatroryField = True
        Me.txtTotalQty.MyLinkLable1 = Nothing
        Me.txtTotalQty.MyLinkLable2 = Nothing
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.ReadOnly = True
        Me.txtTotalQty.ReferenceFieldDesc = Nothing
        Me.txtTotalQty.ReferenceFieldName = Nothing
        Me.txtTotalQty.ReferenceTableName = Nothing
        Me.txtTotalQty.Size = New System.Drawing.Size(154, 20)
        Me.txtTotalQty.TabIndex = 9
        Me.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalFatKg
        '
        Me.txtTotalFatKg.CalculationExpression = Nothing
        Me.txtTotalFatKg.FieldCode = Nothing
        Me.txtTotalFatKg.FieldDesc = Nothing
        Me.txtTotalFatKg.FieldMaxLength = 0
        Me.txtTotalFatKg.FieldName = Nothing
        Me.txtTotalFatKg.isCalculatedField = False
        Me.txtTotalFatKg.IsSourceFromTable = False
        Me.txtTotalFatKg.IsSourceFromValueList = False
        Me.txtTotalFatKg.IsUnique = False
        Me.txtTotalFatKg.Location = New System.Drawing.Point(130, 21)
        Me.txtTotalFatKg.MaxLength = 50
        Me.txtTotalFatKg.MendatroryField = True
        Me.txtTotalFatKg.MyLinkLable1 = Nothing
        Me.txtTotalFatKg.MyLinkLable2 = Nothing
        Me.txtTotalFatKg.Name = "txtTotalFatKg"
        Me.txtTotalFatKg.ReadOnly = True
        Me.txtTotalFatKg.ReferenceFieldDesc = Nothing
        Me.txtTotalFatKg.ReferenceFieldName = Nothing
        Me.txtTotalFatKg.ReferenceTableName = Nothing
        Me.txtTotalFatKg.Size = New System.Drawing.Size(154, 20)
        Me.txtTotalFatKg.TabIndex = 7
        Me.txtTotalFatKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(49, 166)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel7.TabIndex = 348
        Me.MyLabel7.Text = "Total Amount"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(52, 46)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(72, 18)
        Me.MyLabel1.TabIndex = 331
        Me.MyLabel1.Text = "Total SNF KG"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.CalculationExpression = Nothing
        Me.txtTotalAmt.FieldCode = Nothing
        Me.txtTotalAmt.FieldDesc = Nothing
        Me.txtTotalAmt.FieldMaxLength = 0
        Me.txtTotalAmt.FieldName = Nothing
        Me.txtTotalAmt.isCalculatedField = False
        Me.txtTotalAmt.IsSourceFromTable = False
        Me.txtTotalAmt.IsSourceFromValueList = False
        Me.txtTotalAmt.IsUnique = False
        Me.txtTotalAmt.Location = New System.Drawing.Point(130, 165)
        Me.txtTotalAmt.MaxLength = 50
        Me.txtTotalAmt.MendatroryField = True
        Me.txtTotalAmt.MyLinkLable1 = Nothing
        Me.txtTotalAmt.MyLinkLable2 = Nothing
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.ReferenceFieldDesc = Nothing
        Me.txtTotalAmt.ReferenceFieldName = Nothing
        Me.txtTotalAmt.ReferenceTableName = Nothing
        Me.txtTotalAmt.Size = New System.Drawing.Size(154, 20)
        Me.txtTotalAmt.TabIndex = 347
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnViewTDSDetails
        '
        Me.btnViewTDSDetails.Enabled = False
        Me.btnViewTDSDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewTDSDetails.Location = New System.Drawing.Point(207, 2)
        Me.btnViewTDSDetails.Name = "btnViewTDSDetails"
        Me.btnViewTDSDetails.Size = New System.Drawing.Size(69, 21)
        Me.btnViewTDSDetails.TabIndex = 46
        Me.btnViewTDSDetails.Text = "View TDS"
        '
        'btnInvoiceJE
        '
        Me.btnInvoiceJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnInvoiceJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvoiceJE.Location = New System.Drawing.Point(679, 2)
        Me.btnInvoiceJE.Name = "btnInvoiceJE"
        Me.btnInvoiceJE.Size = New System.Drawing.Size(96, 21)
        Me.btnInvoiceJE.TabIndex = 45
        Me.btnInvoiceJE.Text = "Show Invoice JE"
        '
        'btnBillOfSupply
        '
        Me.btnBillOfSupply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBillOfSupply.Enabled = False
        Me.btnBillOfSupply.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBillOfSupply.Location = New System.Drawing.Point(540, 2)
        Me.btnBillOfSupply.Name = "btnBillOfSupply"
        Me.btnBillOfSupply.Size = New System.Drawing.Size(139, 21)
        Me.btnBillOfSupply.TabIndex = 8
        Me.btnBillOfSupply.Text = "Bill of Supply Print"
        '
        'btnUpdateVendor
        '
        Me.btnUpdateVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateVendor.Location = New System.Drawing.Point(412, 2)
        Me.btnUpdateVendor.Name = "btnUpdateVendor"
        Me.btnUpdateVendor.Size = New System.Drawing.Size(128, 21)
        Me.btnUpdateVendor.TabIndex = 7
        Me.btnUpdateVendor.Text = "Update Vendor"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(344, 2)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(68, 21)
        Me.btnReverse.TabIndex = 4
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(276, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 21)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1056, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 21)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(139, 2)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 21)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 21)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(71, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 21)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1135, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSaveLayout, Me.mnuDeleteLayout, Me.mnuExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'mnuSaveLayout
        '
        Me.mnuSaveLayout.Name = "mnuSaveLayout"
        Me.mnuSaveLayout.Text = "Save Layout"
        '
        'mnuDeleteLayout
        '
        Me.mnuDeleteLayout.Name = "mnuDeleteLayout"
        Me.mnuDeleteLayout.Text = "Delete Layout"
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Text = "Exit"
        '
        'mnuSetting
        '
        Me.mnuSetting.Name = "mnuSetting"
        Me.mnuSetting.Text = "Setting"
        '
        'FrmMilkPurchaseInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1135, 449)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmMilkPurchaseInvoice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkPurchaseInvoice"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.chkHighClassVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSRNTrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtElectronicRefNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtewaybilldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnEwaybillupdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEWayBillNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.RadPageViewPage3.PerformLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmtAfterTDS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTDSAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTCSTaxRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualTCSTaxBaseAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel58, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttcstaxbaseamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransporterCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxableAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoundOffAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalSNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalFatKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnViewTDSDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnInvoiceJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBillOfSupply, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdateVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents fndVendor As common.UserControls.txtFinder
    Friend WithEvents lblSRNNo As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents txtTotalSNFKg As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtTotalFatKg As common.Controls.MyTextBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtRoundOffAmt As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtTotalQty As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndSRNNo As common.UserControls.txtFinder
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents txtVendorInvoiceNo As common.Controls.MyTextBox
    Friend WithEvents lblVendorInvoiceNo As common.Controls.MyLabel
    Friend WithEvents txtTotalAmt As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents chkSRNTrade As common.Controls.MyCheckBox
    Friend WithEvents btnUpdateVendor As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtVendorUpdate As common.UserControls.txtFinder
    Friend WithEvents lblMonth As common.Controls.MyLabel
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtElectronicRefNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtewaybilldate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents btnEwaybillupdate As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtEWayBillNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents btnBillOfSupply As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents chkJobWork As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents btnInvoiceJE As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents txtTaxableAmt As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtTaxAmt As common.Controls.MyTextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnTaxCalManual As common.Controls.MyRadioButton
    Friend WithEvents rbtnTaxCalAutomatic As common.Controls.MyRadioButton
    Friend WithEvents txtTaxGroup As common.UserControls.txtFinder
    Friend WithEvents RadLabel11 As common.Controls.MyLabel
    Friend WithEvents lblTaxGrpName As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents chkHighClassVendor As RadCheckBox
    Friend WithEvents txtTransporterCharge As common.Controls.MyTextBox
    Friend WithEvents lblTransporterCharge As common.Controls.MyLabel
    Friend WithEvents txtTCSTaxRate As common.MyNumBox
    Friend WithEvents MyLabel57 As common.Controls.MyLabel
    Friend WithEvents lblActualTCSTaxBaseAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel58 As common.Controls.MyLabel
    Friend WithEvents txttcstaxbaseamount As common.MyNumBox
    Friend WithEvents btnViewTDSDetails As RadButton
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents txtAmtAfterTDS As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtTDSAmt As common.Controls.MyTextBox
End Class

