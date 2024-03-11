Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkJobWorkTransfer
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
        Dim TableViewDefinition3 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition4 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtJobWork = New common.UserControls.txtFinder()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.fndWeighmentNo = New common.Controls.MyTextBox()
        Me.lblJobWork = New common.Controls.MyLabel()
        Me.fndTankerNo = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDocType = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.txtMilkTransferIn = New common.Controls.MyTextBox()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtManualPrice = New common.MyNumBox()
        Me.txtTolerance = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtSNFPercentage = New common.MyNumBox()
        Me.TxtSNFWeightage = New common.MyNumBox()
        Me.TxtFatWeightage = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtStanadardrate = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtfatPercentage = New common.MyNumBox()
        Me.lblPriceChart = New common.Controls.MyLabel()
        Me.fndPriceChart = New common.UserControls.txtFinder()
        Me.lblVirtual = New common.Controls.MyLabel()
        Me.txtVirtual = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtSRNNo = New common.Controls.MyTextBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblReverse = New common.Controls.MyLabel()
        Me.lblTankerTransporterName = New common.Controls.MyLabel()
        Me.txtGateEntryNo = New common.Controls.MyTextBox()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.txtVendor = New common.Controls.MyTextBox()
        Me.dtpChallanDate = New common.Controls.MyDateTimePicker()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtChallanNo = New common.Controls.MyTextBox()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.dtpWeighmentDate = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.dtpSRNDATE = New common.Controls.MyDateTimePicker()
        Me.lblSRNNo = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.QcDetails = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.txtDipValue = New common.Controls.MyTextBox()
        Me.lblDipValue = New common.Controls.MyLabel()
        Me.lblQCOutTime = New common.Controls.MyLabel()
        Me.dtpQCOutTime = New common.Controls.MyDateTimePicker()
        Me.txtQCNo = New common.Controls.MyTextBox()
        Me.lblQCInTime = New common.Controls.MyLabel()
        Me.lblQCNo = New common.Controls.MyLabel()
        Me.dtpQCInTime = New common.Controls.MyDateTimePicker()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.grpParameterDetailBulk = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvParam = New common.UserControls.MyRadGridView()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvRange = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblStandardRate = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.lblTotalQtyValue = New common.Controls.MyLabel()
        Me.lblTotalQty = New common.Controls.MyLabel()
        Me.lblSpecialDeduction = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblIncentive = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblActualAmount = New common.Controls.MyLabel()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.lblDeduction = New common.Controls.MyLabel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.lblTotalAmount = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.lblSnfAmount = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.lblFatAmount = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.lblSNFRate = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.lblFATRate = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.lblSNFKG = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblFATKg = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvReceipt = New common.UserControls.MyRadGridView()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.btnPrintPO = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkTransferIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtManualPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfatPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVirtual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVirtual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpSRNDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.QcDetails.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCOutTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCOutTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCInTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCInTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.grpParameterDetailBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpParameterDetailBulk.SuspendLayout()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvRange.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.lblStandardRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalQtyValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSpecialDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIncentive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblActualAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSnfAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFatAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSNFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFATRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSNFKG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFATKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReceipt.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintPO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(219, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintPO)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1104, 511)
        Me.SplitContainer1.SplitterDistance = 477
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer4.Size = New System.Drawing.Size(1104, 477)
        Me.SplitContainer4.SplitterDistance = 25
        Me.SplitContainer4.TabIndex = 2
        '
        'RadMenu1
        '
        Me.RadMenu1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1104, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'mnuSetting
        '
        Me.mnuSetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSaveLayout, Me.mnuDeleteLayout, Me.mnuExit})
        Me.mnuSetting.Name = "mnuSetting"
        Me.mnuSetting.Text = "Setting"
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
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.QcDetails)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage3
        Me.RadPageView1.Size = New System.Drawing.Size(1104, 448)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1083, 400)
        Me.RadPageViewPage1.Text = "General"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gvItem)
        Me.SplitContainer2.Size = New System.Drawing.Size(1083, 400)
        Me.SplitContainer2.SplitterDistance = 247
        Me.SplitContainer2.TabIndex = 3
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtJobWork)
        Me.RadGroupBox1.Controls.Add(Me.fndWeighmentNo)
        Me.RadGroupBox1.Controls.Add(Me.lblJobWork)
        Me.RadGroupBox1.Controls.Add(Me.fndTankerNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox1.Controls.Add(Me.txtDocType)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.txtMilkTransferIn)
        Me.RadGroupBox1.Controls.Add(Me.lblLocationDesc)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.lblVirtual)
        Me.RadGroupBox1.Controls.Add(Me.txtVirtual)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.txtSRNNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.lblReverse)
        Me.RadGroupBox1.Controls.Add(Me.lblTankerTransporterName)
        Me.RadGroupBox1.Controls.Add(Me.txtGateEntryNo)
        Me.RadGroupBox1.Controls.Add(Me.lblGateEntryNo)
        Me.RadGroupBox1.Controls.Add(Me.txtVendor)
        Me.RadGroupBox1.Controls.Add(Me.dtpChallanDate)
        Me.RadGroupBox1.Controls.Add(Me.lblTankerNo)
        Me.RadGroupBox1.Controls.Add(Me.txtChallanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblChallanNo)
        Me.RadGroupBox1.Controls.Add(Me.lblVendorName)
        Me.RadGroupBox1.Controls.Add(Me.lblVendor)
        Me.RadGroupBox1.Controls.Add(Me.dtpWeighmentDate)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.lblSRNDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpSRNDATE)
        Me.RadGroupBox1.Controls.Add(Me.lblWeighmentNo)
        Me.RadGroupBox1.Controls.Add(Me.lblSRNNo)
        Me.RadGroupBox1.Controls.Add(Me.lblPending)
        Me.RadGroupBox1.Controls.Add(Me.fndDocNo)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "SRN Head"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1083, 247)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "SRN Head"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(6, 41)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 327
        Me.lblLocation.Text = "Location"
        '
        'txtJobWork
        '
        Me.txtJobWork.CalculationExpression = Nothing
        Me.txtJobWork.FieldCode = Nothing
        Me.txtJobWork.FieldDesc = Nothing
        Me.txtJobWork.FieldMaxLength = 0
        Me.txtJobWork.FieldName = Nothing
        Me.txtJobWork.isCalculatedField = False
        Me.txtJobWork.IsSourceFromTable = False
        Me.txtJobWork.IsSourceFromValueList = False
        Me.txtJobWork.IsUnique = False
        Me.txtJobWork.Location = New System.Drawing.Point(106, 63)
        Me.txtJobWork.MendatroryField = True
        Me.txtJobWork.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobWork.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtJobWork.MyLinkLable2 = Nothing
        Me.txtJobWork.MyReadOnly = True
        Me.txtJobWork.MyShowMasterFormButton = False
        Me.txtJobWork.Name = "txtJobWork"
        Me.txtJobWork.ReferenceFieldDesc = Nothing
        Me.txtJobWork.ReferenceFieldName = Nothing
        Me.txtJobWork.ReferenceTableName = Nothing
        Me.txtJobWork.Size = New System.Drawing.Size(137, 20)
        Me.txtJobWork.TabIndex = 354
        Me.txtJobWork.Value = ""
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblWeighmentNo.Location = New System.Drawing.Point(6, 225)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(85, 18)
        Me.lblWeighmentNo.TabIndex = 13
        Me.lblWeighmentNo.Text = "Weighment No."
        '
        'fndWeighmentNo
        '
        Me.fndWeighmentNo.CalculationExpression = Nothing
        Me.fndWeighmentNo.Enabled = False
        Me.fndWeighmentNo.FieldCode = Nothing
        Me.fndWeighmentNo.FieldDesc = Nothing
        Me.fndWeighmentNo.FieldMaxLength = 0
        Me.fndWeighmentNo.FieldName = Nothing
        Me.fndWeighmentNo.isCalculatedField = False
        Me.fndWeighmentNo.IsSourceFromTable = False
        Me.fndWeighmentNo.IsSourceFromValueList = False
        Me.fndWeighmentNo.IsUnique = False
        Me.fndWeighmentNo.Location = New System.Drawing.Point(106, 224)
        Me.fndWeighmentNo.MaxLength = 50
        Me.fndWeighmentNo.MendatroryField = True
        Me.fndWeighmentNo.MyLinkLable1 = Nothing
        Me.fndWeighmentNo.MyLinkLable2 = Nothing
        Me.fndWeighmentNo.Name = "fndWeighmentNo"
        Me.fndWeighmentNo.ReadOnly = True
        Me.fndWeighmentNo.ReferenceFieldDesc = Nothing
        Me.fndWeighmentNo.ReferenceFieldName = Nothing
        Me.fndWeighmentNo.ReferenceTableName = Nothing
        Me.fndWeighmentNo.Size = New System.Drawing.Size(137, 20)
        Me.fndWeighmentNo.TabIndex = 355
        '
        'lblJobWork
        '
        Me.lblJobWork.AutoSize = False
        Me.lblJobWork.BorderVisible = True
        Me.lblJobWork.FieldName = Nothing
        Me.lblJobWork.Location = New System.Drawing.Point(249, 63)
        Me.lblJobWork.Name = "lblJobWork"
        Me.lblJobWork.Size = New System.Drawing.Size(516, 21)
        Me.lblJobWork.TabIndex = 348
        '
        'fndTankerNo
        '
        Me.fndTankerNo.CalculationExpression = Nothing
        Me.fndTankerNo.FieldCode = Nothing
        Me.fndTankerNo.FieldDesc = Nothing
        Me.fndTankerNo.FieldMaxLength = 0
        Me.fndTankerNo.FieldName = Nothing
        Me.fndTankerNo.isCalculatedField = False
        Me.fndTankerNo.IsSourceFromTable = False
        Me.fndTankerNo.IsSourceFromValueList = False
        Me.fndTankerNo.IsUnique = False
        Me.fndTankerNo.Location = New System.Drawing.Point(106, 132)
        Me.fndTankerNo.MaxLength = 50
        Me.fndTankerNo.MendatroryField = True
        Me.fndTankerNo.MyLinkLable1 = Nothing
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.ReferenceFieldDesc = Nothing
        Me.fndTankerNo.ReferenceFieldName = Nothing
        Me.fndTankerNo.ReferenceTableName = Nothing
        Me.fndTankerNo.Size = New System.Drawing.Size(137, 20)
        Me.fndTankerNo.TabIndex = 352
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(6, 64)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(99, 18)
        Me.MyLabel13.TabIndex = 346
        Me.MyLabel13.Text = "Job Work Location"
        '
        'txtDocType
        '
        Me.txtDocType.AutoSize = False
        Me.txtDocType.BorderVisible = True
        Me.txtDocType.FieldName = Nothing
        Me.txtDocType.Location = New System.Drawing.Point(666, 17)
        Me.txtDocType.Name = "txtDocType"
        Me.txtDocType.Size = New System.Drawing.Size(97, 21)
        Me.txtDocType.TabIndex = 351
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.Enabled = False
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(106, 40)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = True
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(137, 20)
        Me.txtLocation.TabIndex = 353
        Me.txtLocation.Value = ""
        '
        'txtMilkTransferIn
        '
        Me.txtMilkTransferIn.CalculationExpression = Nothing
        Me.txtMilkTransferIn.Enabled = False
        Me.txtMilkTransferIn.FieldCode = Nothing
        Me.txtMilkTransferIn.FieldDesc = Nothing
        Me.txtMilkTransferIn.FieldMaxLength = 0
        Me.txtMilkTransferIn.FieldName = Nothing
        Me.txtMilkTransferIn.isCalculatedField = False
        Me.txtMilkTransferIn.IsSourceFromTable = False
        Me.txtMilkTransferIn.IsSourceFromValueList = False
        Me.txtMilkTransferIn.IsUnique = False
        Me.txtMilkTransferIn.Location = New System.Drawing.Point(325, 155)
        Me.txtMilkTransferIn.MaxLength = 50
        Me.txtMilkTransferIn.MendatroryField = True
        Me.txtMilkTransferIn.MyLinkLable1 = Nothing
        Me.txtMilkTransferIn.MyLinkLable2 = Nothing
        Me.txtMilkTransferIn.Name = "txtMilkTransferIn"
        Me.txtMilkTransferIn.ReadOnly = True
        Me.txtMilkTransferIn.ReferenceFieldDesc = Nothing
        Me.txtMilkTransferIn.ReferenceFieldName = Nothing
        Me.txtMilkTransferIn.ReferenceTableName = Nothing
        Me.txtMilkTransferIn.Size = New System.Drawing.Size(146, 20)
        Me.txtMilkTransferIn.TabIndex = 349
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(249, 40)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(516, 21)
        Me.lblLocationDesc.TabIndex = 328
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel15.Location = New System.Drawing.Point(249, 157)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel15.TabIndex = 350
        Me.MyLabel15.Text = "Milk Transfer "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel17)
        Me.GroupBox1.Controls.Add(Me.txtManualPrice)
        Me.GroupBox1.Controls.Add(Me.txtTolerance)
        Me.GroupBox1.Controls.Add(Me.MyLabel8)
        Me.GroupBox1.Controls.Add(Me.MyLabel7)
        Me.GroupBox1.Controls.Add(Me.txtSNFPercentage)
        Me.GroupBox1.Controls.Add(Me.TxtSNFWeightage)
        Me.GroupBox1.Controls.Add(Me.TxtFatWeightage)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.MyLabel5)
        Me.GroupBox1.Controls.Add(Me.txtStanadardrate)
        Me.GroupBox1.Controls.Add(Me.MyLabel6)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.txtfatPercentage)
        Me.GroupBox1.Controls.Add(Me.lblPriceChart)
        Me.GroupBox1.Controls.Add(Me.fndPriceChart)
        Me.GroupBox1.Location = New System.Drawing.Point(477, 156)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(532, 88)
        Me.GroupBox1.TabIndex = 337
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Price Detail"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(7, 62)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel17.TabIndex = 348
        Me.MyLabel17.Text = "Manual Rate"
        '
        'txtManualPrice
        '
        Me.txtManualPrice.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtManualPrice.CalculationExpression = Nothing
        Me.txtManualPrice.DecimalPlaces = 2
        Me.txtManualPrice.FieldCode = Nothing
        Me.txtManualPrice.FieldDesc = Nothing
        Me.txtManualPrice.FieldMaxLength = 0
        Me.txtManualPrice.FieldName = Nothing
        Me.txtManualPrice.isCalculatedField = False
        Me.txtManualPrice.IsSourceFromTable = False
        Me.txtManualPrice.IsSourceFromValueList = False
        Me.txtManualPrice.IsUnique = False
        Me.txtManualPrice.Location = New System.Drawing.Point(97, 63)
        Me.txtManualPrice.MendatroryField = True
        Me.txtManualPrice.MyLinkLable1 = Nothing
        Me.txtManualPrice.MyLinkLable2 = Nothing
        Me.txtManualPrice.Name = "txtManualPrice"
        Me.txtManualPrice.ReferenceFieldDesc = Nothing
        Me.txtManualPrice.ReferenceFieldName = Nothing
        Me.txtManualPrice.ReferenceTableName = Nothing
        Me.txtManualPrice.Size = New System.Drawing.Size(69, 20)
        Me.txtManualPrice.TabIndex = 347
        Me.txtManualPrice.Text = "0"
        Me.txtManualPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtManualPrice.Value = 0R
        '
        'txtTolerance
        '
        Me.txtTolerance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTolerance.CalculationExpression = Nothing
        Me.txtTolerance.DecimalPlaces = 2
        Me.txtTolerance.Enabled = False
        Me.txtTolerance.FieldCode = Nothing
        Me.txtTolerance.FieldDesc = Nothing
        Me.txtTolerance.FieldMaxLength = 0
        Me.txtTolerance.FieldName = Nothing
        Me.txtTolerance.isCalculatedField = False
        Me.txtTolerance.IsSourceFromTable = False
        Me.txtTolerance.IsSourceFromValueList = False
        Me.txtTolerance.IsUnique = False
        Me.txtTolerance.Location = New System.Drawing.Point(263, 62)
        Me.txtTolerance.MendatroryField = True
        Me.txtTolerance.MyLinkLable1 = Nothing
        Me.txtTolerance.MyLinkLable2 = Nothing
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.ReferenceFieldDesc = Nothing
        Me.txtTolerance.ReferenceFieldName = Nothing
        Me.txtTolerance.ReferenceTableName = Nothing
        Me.txtTolerance.Size = New System.Drawing.Size(66, 20)
        Me.txtTolerance.TabIndex = 345
        Me.txtTolerance.Text = "0"
        Me.txtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTolerance.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(171, 63)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel8.TabIndex = 346
        Me.MyLabel8.Text = "Tolerance %"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(170, 40)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel7.TabIndex = 344
        Me.MyLabel7.Text = "SNF Ratio"
        '
        'txtSNFPercentage
        '
        Me.txtSNFPercentage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNFPercentage.CalculationExpression = Nothing
        Me.txtSNFPercentage.DecimalPlaces = 2
        Me.txtSNFPercentage.Enabled = False
        Me.txtSNFPercentage.FieldCode = Nothing
        Me.txtSNFPercentage.FieldDesc = Nothing
        Me.txtSNFPercentage.FieldMaxLength = 0
        Me.txtSNFPercentage.FieldName = Nothing
        Me.txtSNFPercentage.isCalculatedField = False
        Me.txtSNFPercentage.IsSourceFromTable = False
        Me.txtSNFPercentage.IsSourceFromValueList = False
        Me.txtSNFPercentage.IsUnique = False
        Me.txtSNFPercentage.Location = New System.Drawing.Point(263, 38)
        Me.txtSNFPercentage.MendatroryField = True
        Me.txtSNFPercentage.MyLinkLable1 = Nothing
        Me.txtSNFPercentage.MyLinkLable2 = Nothing
        Me.txtSNFPercentage.Name = "txtSNFPercentage"
        Me.txtSNFPercentage.ReferenceFieldDesc = Nothing
        Me.txtSNFPercentage.ReferenceFieldName = Nothing
        Me.txtSNFPercentage.ReferenceTableName = Nothing
        Me.txtSNFPercentage.Size = New System.Drawing.Size(66, 20)
        Me.txtSNFPercentage.TabIndex = 343
        Me.txtSNFPercentage.Text = "0"
        Me.txtSNFPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNFPercentage.Value = 0R
        '
        'TxtSNFWeightage
        '
        Me.TxtSNFWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtSNFWeightage.CalculationExpression = Nothing
        Me.TxtSNFWeightage.DecimalPlaces = 2
        Me.TxtSNFWeightage.Enabled = False
        Me.TxtSNFWeightage.FieldCode = Nothing
        Me.TxtSNFWeightage.FieldDesc = Nothing
        Me.TxtSNFWeightage.FieldMaxLength = 0
        Me.TxtSNFWeightage.FieldName = Nothing
        Me.TxtSNFWeightage.isCalculatedField = False
        Me.TxtSNFWeightage.IsSourceFromTable = False
        Me.TxtSNFWeightage.IsSourceFromValueList = False
        Me.TxtSNFWeightage.IsUnique = False
        Me.TxtSNFWeightage.Location = New System.Drawing.Point(263, 15)
        Me.TxtSNFWeightage.MendatroryField = True
        Me.TxtSNFWeightage.MyLinkLable1 = Nothing
        Me.TxtSNFWeightage.MyLinkLable2 = Nothing
        Me.TxtSNFWeightage.Name = "TxtSNFWeightage"
        Me.TxtSNFWeightage.ReferenceFieldDesc = Nothing
        Me.TxtSNFWeightage.ReferenceFieldName = Nothing
        Me.TxtSNFWeightage.ReferenceTableName = Nothing
        Me.TxtSNFWeightage.Size = New System.Drawing.Size(66, 20)
        Me.TxtSNFWeightage.TabIndex = 336
        Me.TxtSNFWeightage.Text = "0"
        Me.TxtSNFWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSNFWeightage.Value = 0R
        '
        'TxtFatWeightage
        '
        Me.TxtFatWeightage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtFatWeightage.CalculationExpression = Nothing
        Me.TxtFatWeightage.DecimalPlaces = 2
        Me.TxtFatWeightage.Enabled = False
        Me.TxtFatWeightage.FieldCode = Nothing
        Me.TxtFatWeightage.FieldDesc = Nothing
        Me.TxtFatWeightage.FieldMaxLength = 0
        Me.TxtFatWeightage.FieldName = Nothing
        Me.TxtFatWeightage.isCalculatedField = False
        Me.TxtFatWeightage.IsSourceFromTable = False
        Me.TxtFatWeightage.IsSourceFromValueList = False
        Me.TxtFatWeightage.IsUnique = False
        Me.TxtFatWeightage.Location = New System.Drawing.Point(97, 15)
        Me.TxtFatWeightage.MendatroryField = True
        Me.TxtFatWeightage.MyLinkLable1 = Nothing
        Me.TxtFatWeightage.MyLinkLable2 = Nothing
        Me.TxtFatWeightage.Name = "TxtFatWeightage"
        Me.TxtFatWeightage.ReferenceFieldDesc = Nothing
        Me.TxtFatWeightage.ReferenceFieldName = Nothing
        Me.TxtFatWeightage.ReferenceTableName = Nothing
        Me.TxtFatWeightage.Size = New System.Drawing.Size(68, 20)
        Me.TxtFatWeightage.TabIndex = 335
        Me.TxtFatWeightage.Text = "0"
        Me.TxtFatWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFatWeightage.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(168, 17)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel1.TabIndex = 342
        Me.MyLabel1.Text = "SNF Weightage"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(7, 40)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel5.TabIndex = 340
        Me.MyLabel5.Text = "FAT Ratio"
        '
        'txtStanadardrate
        '
        Me.txtStanadardrate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStanadardrate.CalculationExpression = Nothing
        Me.txtStanadardrate.DecimalPlaces = 2
        Me.txtStanadardrate.Enabled = False
        Me.txtStanadardrate.FieldCode = Nothing
        Me.txtStanadardrate.FieldDesc = Nothing
        Me.txtStanadardrate.FieldMaxLength = 0
        Me.txtStanadardrate.FieldName = Nothing
        Me.txtStanadardrate.isCalculatedField = False
        Me.txtStanadardrate.IsSourceFromTable = False
        Me.txtStanadardrate.IsSourceFromValueList = False
        Me.txtStanadardrate.IsUnique = False
        Me.txtStanadardrate.Location = New System.Drawing.Point(439, 62)
        Me.txtStanadardrate.MendatroryField = True
        Me.txtStanadardrate.MyLinkLable1 = Nothing
        Me.txtStanadardrate.MyLinkLable2 = Nothing
        Me.txtStanadardrate.Name = "txtStanadardrate"
        Me.txtStanadardrate.ReferenceFieldDesc = Nothing
        Me.txtStanadardrate.ReferenceFieldName = Nothing
        Me.txtStanadardrate.ReferenceTableName = Nothing
        Me.txtStanadardrate.Size = New System.Drawing.Size(69, 20)
        Me.txtStanadardrate.TabIndex = 338
        Me.txtStanadardrate.Text = "0"
        Me.txtStanadardrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStanadardrate.Value = 0R
        Me.txtStanadardrate.Visible = False
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(349, 63)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel6.TabIndex = 341
        Me.MyLabel6.Text = "Standard Rate"
        Me.MyLabel6.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(7, 17)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel4.TabIndex = 339
        Me.MyLabel4.Text = "FAT Weightage"
        '
        'txtfatPercentage
        '
        Me.txtfatPercentage.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtfatPercentage.CalculationExpression = Nothing
        Me.txtfatPercentage.DecimalPlaces = 2
        Me.txtfatPercentage.Enabled = False
        Me.txtfatPercentage.FieldCode = Nothing
        Me.txtfatPercentage.FieldDesc = Nothing
        Me.txtfatPercentage.FieldMaxLength = 0
        Me.txtfatPercentage.FieldName = Nothing
        Me.txtfatPercentage.isCalculatedField = False
        Me.txtfatPercentage.IsSourceFromTable = False
        Me.txtfatPercentage.IsSourceFromValueList = False
        Me.txtfatPercentage.IsUnique = False
        Me.txtfatPercentage.Location = New System.Drawing.Point(97, 38)
        Me.txtfatPercentage.MendatroryField = True
        Me.txtfatPercentage.MyLinkLable1 = Nothing
        Me.txtfatPercentage.MyLinkLable2 = Nothing
        Me.txtfatPercentage.Name = "txtfatPercentage"
        Me.txtfatPercentage.ReferenceFieldDesc = Nothing
        Me.txtfatPercentage.ReferenceFieldName = Nothing
        Me.txtfatPercentage.ReferenceTableName = Nothing
        Me.txtfatPercentage.Size = New System.Drawing.Size(69, 20)
        Me.txtfatPercentage.TabIndex = 337
        Me.txtfatPercentage.Text = "0"
        Me.txtfatPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtfatPercentage.Value = 0R
        '
        'lblPriceChart
        '
        Me.lblPriceChart.FieldName = Nothing
        Me.lblPriceChart.Location = New System.Drawing.Point(335, 15)
        Me.lblPriceChart.Name = "lblPriceChart"
        Me.lblPriceChart.Size = New System.Drawing.Size(60, 18)
        Me.lblPriceChart.TabIndex = 334
        Me.lblPriceChart.Text = "Price Chart"
        '
        'fndPriceChart
        '
        Me.fndPriceChart.CalculationExpression = Nothing
        Me.fndPriceChart.FieldCode = Nothing
        Me.fndPriceChart.FieldDesc = Nothing
        Me.fndPriceChart.FieldMaxLength = 0
        Me.fndPriceChart.FieldName = Nothing
        Me.fndPriceChart.isCalculatedField = False
        Me.fndPriceChart.IsSourceFromTable = False
        Me.fndPriceChart.IsSourceFromValueList = False
        Me.fndPriceChart.IsUnique = False
        Me.fndPriceChart.Location = New System.Drawing.Point(412, 14)
        Me.fndPriceChart.MendatroryField = True
        Me.fndPriceChart.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceChart.MyLinkLable1 = Me.lblPriceChart
        Me.fndPriceChart.MyLinkLable2 = Me.lblLocationDesc
        Me.fndPriceChart.MyReadOnly = False
        Me.fndPriceChart.MyShowMasterFormButton = False
        Me.fndPriceChart.Name = "fndPriceChart"
        Me.fndPriceChart.ReferenceFieldDesc = Nothing
        Me.fndPriceChart.ReferenceFieldName = Nothing
        Me.fndPriceChart.ReferenceTableName = Nothing
        Me.fndPriceChart.Size = New System.Drawing.Size(102, 21)
        Me.fndPriceChart.TabIndex = 9
        Me.fndPriceChart.Value = ""
        '
        'lblVirtual
        '
        Me.lblVirtual.AutoSize = False
        Me.lblVirtual.BorderVisible = True
        Me.lblVirtual.FieldName = Nothing
        Me.lblVirtual.Location = New System.Drawing.Point(249, 109)
        Me.lblVirtual.Name = "lblVirtual"
        Me.lblVirtual.Size = New System.Drawing.Size(516, 21)
        Me.lblVirtual.TabIndex = 347
        '
        'txtVirtual
        '
        Me.txtVirtual.CalculationExpression = Nothing
        Me.txtVirtual.Enabled = False
        Me.txtVirtual.FieldCode = Nothing
        Me.txtVirtual.FieldDesc = Nothing
        Me.txtVirtual.FieldMaxLength = 0
        Me.txtVirtual.FieldName = Nothing
        Me.txtVirtual.isCalculatedField = False
        Me.txtVirtual.IsSourceFromTable = False
        Me.txtVirtual.IsSourceFromValueList = False
        Me.txtVirtual.IsUnique = False
        Me.txtVirtual.Location = New System.Drawing.Point(106, 109)
        Me.txtVirtual.MaxLength = 50
        Me.txtVirtual.MendatroryField = True
        Me.txtVirtual.MyLinkLable1 = Nothing
        Me.txtVirtual.MyLinkLable2 = Nothing
        Me.txtVirtual.Name = "txtVirtual"
        Me.txtVirtual.ReadOnly = True
        Me.txtVirtual.ReferenceFieldDesc = Nothing
        Me.txtVirtual.ReferenceFieldName = Nothing
        Me.txtVirtual.ReferenceTableName = Nothing
        Me.txtVirtual.Size = New System.Drawing.Size(137, 20)
        Me.txtVirtual.TabIndex = 343
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(6, 110)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel11.TabIndex = 344
        Me.MyLabel11.Text = "Virtual Location"
        '
        'txtSRNNo
        '
        Me.txtSRNNo.CalculationExpression = Nothing
        Me.txtSRNNo.Enabled = False
        Me.txtSRNNo.FieldCode = Nothing
        Me.txtSRNNo.FieldDesc = Nothing
        Me.txtSRNNo.FieldMaxLength = 0
        Me.txtSRNNo.FieldName = Nothing
        Me.txtSRNNo.isCalculatedField = False
        Me.txtSRNNo.IsSourceFromTable = False
        Me.txtSRNNo.IsSourceFromValueList = False
        Me.txtSRNNo.IsUnique = False
        Me.txtSRNNo.Location = New System.Drawing.Point(106, 155)
        Me.txtSRNNo.MaxLength = 50
        Me.txtSRNNo.MendatroryField = True
        Me.txtSRNNo.MyLinkLable1 = Nothing
        Me.txtSRNNo.MyLinkLable2 = Nothing
        Me.txtSRNNo.Name = "txtSRNNo"
        Me.txtSRNNo.ReadOnly = True
        Me.txtSRNNo.ReferenceFieldDesc = Nothing
        Me.txtSRNNo.ReferenceFieldName = Nothing
        Me.txtSRNNo.ReferenceTableName = Nothing
        Me.txtSRNNo.Size = New System.Drawing.Size(137, 20)
        Me.txtSRNNo.TabIndex = 341
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(6, 157)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel9.TabIndex = 342
        Me.MyLabel9.Text = "SRN No"
        '
        'lblReverse
        '
        Me.lblReverse.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblReverse.FieldName = Nothing
        Me.lblReverse.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblReverse.ForeColor = System.Drawing.Color.Yellow
        Me.lblReverse.Location = New System.Drawing.Point(787, 36)
        Me.lblReverse.Name = "lblReverse"
        Me.lblReverse.Size = New System.Drawing.Size(69, 19)
        Me.lblReverse.TabIndex = 340
        Me.lblReverse.Text = "Reversed"
        Me.lblReverse.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        '
        'lblTankerTransporterName
        '
        Me.lblTankerTransporterName.AutoSize = False
        Me.lblTankerTransporterName.BorderVisible = True
        Me.lblTankerTransporterName.FieldName = Nothing
        Me.lblTankerTransporterName.Location = New System.Drawing.Point(249, 132)
        Me.lblTankerTransporterName.Name = "lblTankerTransporterName"
        Me.lblTankerTransporterName.Size = New System.Drawing.Size(516, 21)
        Me.lblTankerTransporterName.TabIndex = 339
        Me.lblTankerTransporterName.Visible = False
        '
        'txtGateEntryNo
        '
        Me.txtGateEntryNo.CalculationExpression = Nothing
        Me.txtGateEntryNo.Enabled = False
        Me.txtGateEntryNo.FieldCode = Nothing
        Me.txtGateEntryNo.FieldDesc = Nothing
        Me.txtGateEntryNo.FieldMaxLength = 0
        Me.txtGateEntryNo.FieldName = Nothing
        Me.txtGateEntryNo.isCalculatedField = False
        Me.txtGateEntryNo.IsSourceFromTable = False
        Me.txtGateEntryNo.IsSourceFromValueList = False
        Me.txtGateEntryNo.IsUnique = False
        Me.txtGateEntryNo.Location = New System.Drawing.Point(106, 201)
        Me.txtGateEntryNo.MaxLength = 50
        Me.txtGateEntryNo.MendatroryField = True
        Me.txtGateEntryNo.MyLinkLable1 = Nothing
        Me.txtGateEntryNo.MyLinkLable2 = Nothing
        Me.txtGateEntryNo.Name = "txtGateEntryNo"
        Me.txtGateEntryNo.ReadOnly = True
        Me.txtGateEntryNo.ReferenceFieldDesc = Nothing
        Me.txtGateEntryNo.ReferenceFieldName = Nothing
        Me.txtGateEntryNo.ReferenceTableName = Nothing
        Me.txtGateEntryNo.Size = New System.Drawing.Size(137, 20)
        Me.txtGateEntryNo.TabIndex = 335
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblGateEntryNo.Location = New System.Drawing.Point(6, 203)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(81, 16)
        Me.lblGateEntryNo.TabIndex = 336
        Me.lblGateEntryNo.Text = "Gate Entry No."
        '
        'txtVendor
        '
        Me.txtVendor.CalculationExpression = Nothing
        Me.txtVendor.Enabled = False
        Me.txtVendor.FieldCode = Nothing
        Me.txtVendor.FieldDesc = Nothing
        Me.txtVendor.FieldMaxLength = 0
        Me.txtVendor.FieldName = Nothing
        Me.txtVendor.isCalculatedField = False
        Me.txtVendor.IsSourceFromTable = False
        Me.txtVendor.IsSourceFromValueList = False
        Me.txtVendor.IsUnique = False
        Me.txtVendor.Location = New System.Drawing.Point(106, 86)
        Me.txtVendor.MaxLength = 50
        Me.txtVendor.MendatroryField = True
        Me.txtVendor.MyLinkLable1 = Nothing
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.ReadOnly = True
        Me.txtVendor.ReferenceFieldDesc = Nothing
        Me.txtVendor.ReferenceFieldName = Nothing
        Me.txtVendor.ReferenceTableName = Nothing
        Me.txtVendor.Size = New System.Drawing.Size(137, 20)
        Me.txtVendor.TabIndex = 4
        '
        'dtpChallanDate
        '
        Me.dtpChallanDate.CalculationExpression = Nothing
        Me.dtpChallanDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpChallanDate.FieldCode = Nothing
        Me.dtpChallanDate.FieldDesc = Nothing
        Me.dtpChallanDate.FieldMaxLength = 0
        Me.dtpChallanDate.FieldName = Nothing
        Me.dtpChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChallanDate.isCalculatedField = False
        Me.dtpChallanDate.IsSourceFromTable = False
        Me.dtpChallanDate.IsSourceFromValueList = False
        Me.dtpChallanDate.IsUnique = False
        Me.dtpChallanDate.Location = New System.Drawing.Point(247, 179)
        Me.dtpChallanDate.MendatroryField = True
        Me.dtpChallanDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.MyLinkLable1 = Nothing
        Me.dtpChallanDate.MyLinkLable2 = Nothing
        Me.dtpChallanDate.Name = "dtpChallanDate"
        Me.dtpChallanDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.ReferenceFieldDesc = Nothing
        Me.dtpChallanDate.ReferenceFieldName = Nothing
        Me.dtpChallanDate.ReferenceTableName = Nothing
        Me.dtpChallanDate.Size = New System.Drawing.Size(148, 18)
        Me.dtpChallanDate.TabIndex = 7
        Me.dtpChallanDate.TabStop = False
        Me.dtpChallanDate.Text = "03/05/2011"
        Me.dtpChallanDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTankerNo.Location = New System.Drawing.Point(6, 134)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(62, 16)
        Me.lblTankerNo.TabIndex = 329
        Me.lblTankerNo.Text = "Tanker No."
        '
        'txtChallanNo
        '
        Me.txtChallanNo.CalculationExpression = Nothing
        Me.txtChallanNo.FieldCode = Nothing
        Me.txtChallanNo.FieldDesc = Nothing
        Me.txtChallanNo.FieldMaxLength = 0
        Me.txtChallanNo.FieldName = Nothing
        Me.txtChallanNo.isCalculatedField = False
        Me.txtChallanNo.IsSourceFromTable = False
        Me.txtChallanNo.IsSourceFromValueList = False
        Me.txtChallanNo.IsUnique = False
        Me.txtChallanNo.Location = New System.Drawing.Point(106, 178)
        Me.txtChallanNo.MaxLength = 50
        Me.txtChallanNo.MendatroryField = True
        Me.txtChallanNo.MyLinkLable1 = Nothing
        Me.txtChallanNo.MyLinkLable2 = Nothing
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.ReferenceFieldDesc = Nothing
        Me.txtChallanNo.ReferenceFieldName = Nothing
        Me.txtChallanNo.ReferenceTableName = Nothing
        Me.txtChallanNo.Size = New System.Drawing.Size(137, 20)
        Me.txtChallanNo.TabIndex = 6
        '
        'lblChallanNo
        '
        Me.lblChallanNo.FieldName = Nothing
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblChallanNo.Location = New System.Drawing.Point(6, 180)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(65, 16)
        Me.lblChallanNo.TabIndex = 324
        Me.lblChallanNo.Text = "Challan No."
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(249, 86)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(516, 21)
        Me.lblVendorName.TabIndex = 323
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Location = New System.Drawing.Point(6, 87)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 18)
        Me.lblVendor.TabIndex = 322
        Me.lblVendor.Text = "Vendor"
        '
        'dtpWeighmentDate
        '
        Me.dtpWeighmentDate.CalculationExpression = Nothing
        Me.dtpWeighmentDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpWeighmentDate.Enabled = False
        Me.dtpWeighmentDate.FieldCode = Nothing
        Me.dtpWeighmentDate.FieldDesc = Nothing
        Me.dtpWeighmentDate.FieldMaxLength = 0
        Me.dtpWeighmentDate.FieldName = Nothing
        Me.dtpWeighmentDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpWeighmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDate.isCalculatedField = False
        Me.dtpWeighmentDate.IsSourceFromTable = False
        Me.dtpWeighmentDate.IsSourceFromValueList = False
        Me.dtpWeighmentDate.IsUnique = False
        Me.dtpWeighmentDate.Location = New System.Drawing.Point(247, 225)
        Me.dtpWeighmentDate.MendatroryField = True
        Me.dtpWeighmentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.MyLinkLable1 = Nothing
        Me.dtpWeighmentDate.MyLinkLable2 = Nothing
        Me.dtpWeighmentDate.Name = "dtpWeighmentDate"
        Me.dtpWeighmentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.ReadOnly = True
        Me.dtpWeighmentDate.ReferenceFieldDesc = Nothing
        Me.dtpWeighmentDate.ReferenceFieldName = Nothing
        Me.dtpWeighmentDate.ReferenceTableName = Nothing
        Me.dtpWeighmentDate.Size = New System.Drawing.Size(148, 18)
        Me.dtpWeighmentDate.TabIndex = 3
        Me.dtpWeighmentDate.TabStop = False
        Me.dtpWeighmentDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpWeighmentDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnReset
        '
        Me.btnReset.Image = Global.XpertERPJobWorkOutward.My.Resources.Resources.new1
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(420, 17)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 21)
        Me.btnReset.TabIndex = 44
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(443, 19)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(85, 16)
        Me.lblSRNDate.TabIndex = 19
        Me.lblSRNDate.Text = "Document Date"
        '
        'dtpSRNDATE
        '
        Me.dtpSRNDATE.CalculationExpression = Nothing
        Me.dtpSRNDATE.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpSRNDATE.FieldCode = Nothing
        Me.dtpSRNDATE.FieldDesc = Nothing
        Me.dtpSRNDATE.FieldMaxLength = 0
        Me.dtpSRNDATE.FieldName = Nothing
        Me.dtpSRNDATE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSRNDATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSRNDATE.isCalculatedField = False
        Me.dtpSRNDATE.IsSourceFromTable = False
        Me.dtpSRNDATE.IsSourceFromValueList = False
        Me.dtpSRNDATE.IsUnique = False
        Me.dtpSRNDATE.Location = New System.Drawing.Point(527, 18)
        Me.dtpSRNDATE.MendatroryField = True
        Me.dtpSRNDATE.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpSRNDATE.MyLinkLable1 = Me.lblSRNDate
        Me.dtpSRNDATE.MyLinkLable2 = Nothing
        Me.dtpSRNDATE.Name = "dtpSRNDATE"
        Me.dtpSRNDATE.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpSRNDATE.ReferenceFieldDesc = Nothing
        Me.dtpSRNDATE.ReferenceFieldName = Nothing
        Me.dtpSRNDATE.ReferenceTableName = Nothing
        Me.dtpSRNDATE.Size = New System.Drawing.Size(133, 18)
        Me.dtpSRNDATE.TabIndex = 1
        Me.dtpSRNDATE.TabStop = False
        Me.dtpSRNDATE.Text = "03/05/2011 12:00:00 AM"
        Me.dtpSRNDATE.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblSRNNo
        '
        Me.lblSRNNo.FieldName = Nothing
        Me.lblSRNNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSRNNo.Location = New System.Drawing.Point(6, 19)
        Me.lblSRNNo.Name = "lblSRNNo"
        Me.lblSRNNo.Size = New System.Drawing.Size(88, 16)
        Me.lblSRNNo.TabIndex = 10
        Me.lblSRNNo.Text = "Document Code"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(786, 13)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(98, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 11
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(106, 17)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblSRNNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 100
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(314, 21)
        Me.fndDocNo.TabIndex = 0
        Me.fndDocNo.Value = ""
        '
        'gvItem
        '
        Me.gvItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvItem.ForeColor = System.Drawing.Color.Black
        Me.gvItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvItem.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvItem.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gvItem.MasterTemplate.AllowDeleteRow = False
        Me.gvItem.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvItem.MyStopExport = False
        Me.gvItem.Name = "gvItem"
        Me.gvItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvItem.ShowGroupPanel = False
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(1083, 149)
        Me.gvItem.TabIndex = 0
        Me.gvItem.TabStop = False
        '
        'QcDetails
        '
        Me.QcDetails.Controls.Add(Me.SplitContainer3)
        Me.QcDetails.ItemSize = New System.Drawing.SizeF(69.0!, 28.0!)
        Me.QcDetails.Location = New System.Drawing.Point(10, 37)
        Me.QcDetails.Name = "QcDetails"
        Me.QcDetails.Size = New System.Drawing.Size(1083, 400)
        Me.QcDetails.Text = "QC Details"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDipValue)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDipValue)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblQCOutTime)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpQCOutTime)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtQCNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblQCInTime)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblQCNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpQCInTime)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer5)
        Me.SplitContainer3.Size = New System.Drawing.Size(1083, 400)
        Me.SplitContainer3.SplitterDistance = 157
        Me.SplitContainer3.TabIndex = 337
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(104, 76)
        Me.txtRemarks.MaxLength = 50
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.MyLinkLable1 = Nothing
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReadOnly = True
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(534, 76)
        Me.txtRemarks.TabIndex = 4
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(7, 79)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 341
        Me.lblRemarks.Text = "Remarks"
        '
        'txtDipValue
        '
        Me.txtDipValue.CalculationExpression = Nothing
        Me.txtDipValue.FieldCode = Nothing
        Me.txtDipValue.FieldDesc = Nothing
        Me.txtDipValue.FieldMaxLength = 0
        Me.txtDipValue.FieldName = Nothing
        Me.txtDipValue.isCalculatedField = False
        Me.txtDipValue.IsSourceFromTable = False
        Me.txtDipValue.IsSourceFromValueList = False
        Me.txtDipValue.IsUnique = False
        Me.txtDipValue.Location = New System.Drawing.Point(104, 52)
        Me.txtDipValue.MaxLength = 50
        Me.txtDipValue.MendatroryField = True
        Me.txtDipValue.MyLinkLable1 = Nothing
        Me.txtDipValue.MyLinkLable2 = Nothing
        Me.txtDipValue.Name = "txtDipValue"
        Me.txtDipValue.ReadOnly = True
        Me.txtDipValue.ReferenceFieldDesc = Nothing
        Me.txtDipValue.ReferenceFieldName = Nothing
        Me.txtDipValue.ReferenceTableName = Nothing
        Me.txtDipValue.Size = New System.Drawing.Size(212, 20)
        Me.txtDipValue.TabIndex = 3
        '
        'lblDipValue
        '
        Me.lblDipValue.FieldName = Nothing
        Me.lblDipValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDipValue.Location = New System.Drawing.Point(7, 55)
        Me.lblDipValue.Name = "lblDipValue"
        Me.lblDipValue.Size = New System.Drawing.Size(57, 16)
        Me.lblDipValue.TabIndex = 339
        Me.lblDipValue.Text = "DIP Value"
        '
        'lblQCOutTime
        '
        Me.lblQCOutTime.FieldName = Nothing
        Me.lblQCOutTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCOutTime.Location = New System.Drawing.Point(323, 32)
        Me.lblQCOutTime.Name = "lblQCOutTime"
        Me.lblQCOutTime.Size = New System.Drawing.Size(100, 16)
        Me.lblQCOutTime.TabIndex = 338
        Me.lblQCOutTime.Text = "QC Out Date Time"
        '
        'dtpQCOutTime
        '
        Me.dtpQCOutTime.CalculationExpression = Nothing
        Me.dtpQCOutTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCOutTime.FieldCode = Nothing
        Me.dtpQCOutTime.FieldDesc = Nothing
        Me.dtpQCOutTime.FieldMaxLength = 0
        Me.dtpQCOutTime.FieldName = Nothing
        Me.dtpQCOutTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpQCOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCOutTime.isCalculatedField = False
        Me.dtpQCOutTime.IsSourceFromTable = False
        Me.dtpQCOutTime.IsSourceFromValueList = False
        Me.dtpQCOutTime.IsUnique = False
        Me.dtpQCOutTime.Location = New System.Drawing.Point(426, 30)
        Me.dtpQCOutTime.MendatroryField = True
        Me.dtpQCOutTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutTime.MyLinkLable1 = Me.lblQCOutTime
        Me.dtpQCOutTime.MyLinkLable2 = Nothing
        Me.dtpQCOutTime.Name = "dtpQCOutTime"
        Me.dtpQCOutTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutTime.ReadOnly = True
        Me.dtpQCOutTime.ReferenceFieldDesc = Nothing
        Me.dtpQCOutTime.ReferenceFieldName = Nothing
        Me.dtpQCOutTime.ReferenceTableName = Nothing
        Me.dtpQCOutTime.Size = New System.Drawing.Size(212, 18)
        Me.dtpQCOutTime.TabIndex = 2
        Me.dtpQCOutTime.TabStop = False
        Me.dtpQCOutTime.Text = "03/05/2011 12:00:00 AM"
        Me.dtpQCOutTime.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtQCNo
        '
        Me.txtQCNo.CalculationExpression = Nothing
        Me.txtQCNo.FieldCode = Nothing
        Me.txtQCNo.FieldDesc = Nothing
        Me.txtQCNo.FieldMaxLength = 0
        Me.txtQCNo.FieldName = Nothing
        Me.txtQCNo.isCalculatedField = False
        Me.txtQCNo.IsSourceFromTable = False
        Me.txtQCNo.IsSourceFromValueList = False
        Me.txtQCNo.IsUnique = False
        Me.txtQCNo.Location = New System.Drawing.Point(105, 8)
        Me.txtQCNo.MaxLength = 50
        Me.txtQCNo.MendatroryField = True
        Me.txtQCNo.MyLinkLable1 = Nothing
        Me.txtQCNo.MyLinkLable2 = Nothing
        Me.txtQCNo.Name = "txtQCNo"
        Me.txtQCNo.ReadOnly = True
        Me.txtQCNo.ReferenceFieldDesc = Nothing
        Me.txtQCNo.ReferenceFieldName = Nothing
        Me.txtQCNo.ReferenceTableName = Nothing
        Me.txtQCNo.Size = New System.Drawing.Size(211, 20)
        Me.txtQCNo.TabIndex = 0
        '
        'lblQCInTime
        '
        Me.lblQCInTime.FieldName = Nothing
        Me.lblQCInTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCInTime.Location = New System.Drawing.Point(7, 33)
        Me.lblQCInTime.Name = "lblQCInTime"
        Me.lblQCInTime.Size = New System.Drawing.Size(91, 16)
        Me.lblQCInTime.TabIndex = 336
        Me.lblQCInTime.Text = "QC In Date Time"
        '
        'lblQCNo
        '
        Me.lblQCNo.FieldName = Nothing
        Me.lblQCNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblQCNo.Location = New System.Drawing.Point(6, 10)
        Me.lblQCNo.Name = "lblQCNo"
        Me.lblQCNo.Size = New System.Drawing.Size(44, 16)
        Me.lblQCNo.TabIndex = 333
        Me.lblQCNo.Text = "QC No."
        '
        'dtpQCInTime
        '
        Me.dtpQCInTime.CalculationExpression = Nothing
        Me.dtpQCInTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCInTime.FieldCode = Nothing
        Me.dtpQCInTime.FieldDesc = Nothing
        Me.dtpQCInTime.FieldMaxLength = 0
        Me.dtpQCInTime.FieldName = Nothing
        Me.dtpQCInTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpQCInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCInTime.isCalculatedField = False
        Me.dtpQCInTime.IsSourceFromTable = False
        Me.dtpQCInTime.IsSourceFromValueList = False
        Me.dtpQCInTime.IsUnique = False
        Me.dtpQCInTime.Location = New System.Drawing.Point(104, 31)
        Me.dtpQCInTime.MendatroryField = True
        Me.dtpQCInTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCInTime.MyLinkLable1 = Me.lblQCInTime
        Me.dtpQCInTime.MyLinkLable2 = Nothing
        Me.dtpQCInTime.Name = "dtpQCInTime"
        Me.dtpQCInTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCInTime.ReadOnly = True
        Me.dtpQCInTime.ReferenceFieldDesc = Nothing
        Me.dtpQCInTime.ReferenceFieldName = Nothing
        Me.dtpQCInTime.ReferenceTableName = Nothing
        Me.dtpQCInTime.Size = New System.Drawing.Size(212, 18)
        Me.dtpQCInTime.TabIndex = 1
        Me.dtpQCInTime.TabStop = False
        Me.dtpQCInTime.Text = "03/05/2011 12:00:00 AM"
        Me.dtpQCInTime.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.grpParameterDetailBulk)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer5.Size = New System.Drawing.Size(1083, 239)
        Me.SplitContainer5.SplitterDistance = 120
        Me.SplitContainer5.TabIndex = 0
        '
        'grpParameterDetailBulk
        '
        Me.grpParameterDetailBulk.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpParameterDetailBulk.Controls.Add(Me.gvParam)
        Me.grpParameterDetailBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpParameterDetailBulk.HeaderText = "QC Parameter Details"
        Me.grpParameterDetailBulk.Location = New System.Drawing.Point(0, 0)
        Me.grpParameterDetailBulk.Name = "grpParameterDetailBulk"
        Me.grpParameterDetailBulk.Size = New System.Drawing.Size(1083, 120)
        Me.grpParameterDetailBulk.TabIndex = 287
        Me.grpParameterDetailBulk.Text = "QC Parameter Details"
        '
        'gvParam
        '
        Me.gvParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvParam.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gvParam.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvParam.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvParam.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvParam.MyStopExport = False
        Me.gvParam.Name = "gvParam"
        Me.gvParam.ShowHeaderCellButtons = True
        Me.gvParam.Size = New System.Drawing.Size(1079, 100)
        Me.gvParam.TabIndex = 264
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gvRange)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "QC Parameter Range Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(1083, 115)
        Me.RadGroupBox2.TabIndex = 288
        Me.RadGroupBox2.Text = "QC Parameter Range Details"
        '
        'gvRange
        '
        Me.gvRange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvRange.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gvRange.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvRange.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvRange.MasterTemplate.ViewDefinition = TableViewDefinition3
        Me.gvRange.MyStopExport = False
        Me.gvRange.Name = "gvRange"
        Me.gvRange.ShowHeaderCellButtons = True
        Me.gvRange.Size = New System.Drawing.Size(1079, 95)
        Me.gvRange.TabIndex = 264
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.lblStandardRate)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage2.Controls.Add(Me.lblTotalQtyValue)
        Me.RadPageViewPage2.Controls.Add(Me.lblTotalQty)
        Me.RadPageViewPage2.Controls.Add(Me.lblSpecialDeduction)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage2.Controls.Add(Me.lblIncentive)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage2.Controls.Add(Me.lblActualAmount)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel28)
        Me.RadPageViewPage2.Controls.Add(Me.lblDeduction)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel24)
        Me.RadPageViewPage2.Controls.Add(Me.lblTotalAmount)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel26)
        Me.RadPageViewPage2.Controls.Add(Me.lblSnfAmount)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel20)
        Me.RadPageViewPage2.Controls.Add(Me.lblFatAmount)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel22)
        Me.RadPageViewPage2.Controls.Add(Me.lblSNFRate)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel16)
        Me.RadPageViewPage2.Controls.Add(Me.lblFATRate)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel18)
        Me.RadPageViewPage2.Controls.Add(Me.lblSNFKG)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage2.Controls.Add(Me.lblFATKg)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel12)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1083, 400)
        Me.RadPageViewPage2.Text = "Total"
        '
        'lblStandardRate
        '
        Me.lblStandardRate.AutoSize = False
        Me.lblStandardRate.BorderVisible = True
        Me.lblStandardRate.FieldName = Nothing
        Me.lblStandardRate.Location = New System.Drawing.Point(154, 173)
        Me.lblStandardRate.Name = "lblStandardRate"
        Me.lblStandardRate.Size = New System.Drawing.Size(235, 21)
        Me.lblStandardRate.TabIndex = 349
        Me.lblStandardRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Location = New System.Drawing.Point(39, 174)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel10.TabIndex = 348
        Me.MyLabel10.Text = "Standard Rate"
        '
        'lblTotalQtyValue
        '
        Me.lblTotalQtyValue.AutoSize = False
        Me.lblTotalQtyValue.BorderVisible = True
        Me.lblTotalQtyValue.FieldName = Nothing
        Me.lblTotalQtyValue.Location = New System.Drawing.Point(154, 151)
        Me.lblTotalQtyValue.Name = "lblTotalQtyValue"
        Me.lblTotalQtyValue.Size = New System.Drawing.Size(235, 21)
        Me.lblTotalQtyValue.TabIndex = 347
        Me.lblTotalQtyValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalQty
        '
        Me.lblTotalQty.FieldName = Nothing
        Me.lblTotalQty.Location = New System.Drawing.Point(39, 152)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(52, 18)
        Me.lblTotalQty.TabIndex = 346
        Me.lblTotalQty.Text = "Total Qty"
        '
        'lblSpecialDeduction
        '
        Me.lblSpecialDeduction.AutoSize = False
        Me.lblSpecialDeduction.BorderVisible = True
        Me.lblSpecialDeduction.FieldName = Nothing
        Me.lblSpecialDeduction.Location = New System.Drawing.Point(153, 262)
        Me.lblSpecialDeduction.Name = "lblSpecialDeduction"
        Me.lblSpecialDeduction.Size = New System.Drawing.Size(235, 21)
        Me.lblSpecialDeduction.TabIndex = 345
        Me.lblSpecialDeduction.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(37, 261)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(111, 18)
        Me.MyLabel3.TabIndex = 344
        Me.MyLabel3.Text = "Special Deduction (-)"
        '
        'lblIncentive
        '
        Me.lblIncentive.AutoSize = False
        Me.lblIncentive.BorderVisible = True
        Me.lblIncentive.FieldName = Nothing
        Me.lblIncentive.Location = New System.Drawing.Point(153, 239)
        Me.lblIncentive.Name = "lblIncentive"
        Me.lblIncentive.Size = New System.Drawing.Size(235, 21)
        Me.lblIncentive.TabIndex = 343
        Me.lblIncentive.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(38, 238)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(69, 18)
        Me.MyLabel2.TabIndex = 342
        Me.MyLabel2.Text = "Incentive (+)"
        '
        'lblActualAmount
        '
        Me.lblActualAmount.AutoSize = False
        Me.lblActualAmount.BorderVisible = True
        Me.lblActualAmount.FieldName = Nothing
        Me.lblActualAmount.Location = New System.Drawing.Point(153, 284)
        Me.lblActualAmount.Name = "lblActualAmount"
        Me.lblActualAmount.Size = New System.Drawing.Size(235, 21)
        Me.lblActualAmount.TabIndex = 341
        Me.lblActualAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Location = New System.Drawing.Point(38, 283)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(81, 18)
        Me.MyLabel28.TabIndex = 340
        Me.MyLabel28.Text = "Actual Amount"
        '
        'lblDeduction
        '
        Me.lblDeduction.AutoSize = False
        Me.lblDeduction.BorderVisible = True
        Me.lblDeduction.FieldName = Nothing
        Me.lblDeduction.Location = New System.Drawing.Point(153, 217)
        Me.lblDeduction.Name = "lblDeduction"
        Me.lblDeduction.Size = New System.Drawing.Size(235, 21)
        Me.lblDeduction.TabIndex = 339
        Me.lblDeduction.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Location = New System.Drawing.Point(37, 216)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(72, 18)
        Me.MyLabel24.TabIndex = 338
        Me.MyLabel24.Text = "Deduction (-)"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = False
        Me.lblTotalAmount.BorderVisible = True
        Me.lblTotalAmount.FieldName = Nothing
        Me.lblTotalAmount.Location = New System.Drawing.Point(153, 196)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(235, 21)
        Me.lblTotalAmount.TabIndex = 337
        Me.lblTotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Location = New System.Drawing.Point(38, 196)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(104, 18)
        Me.MyLabel26.TabIndex = 336
        Me.MyLabel26.Text = "Total Amount (1+2)"
        '
        'lblSnfAmount
        '
        Me.lblSnfAmount.AutoSize = False
        Me.lblSnfAmount.BorderVisible = True
        Me.lblSnfAmount.FieldName = Nothing
        Me.lblSnfAmount.Location = New System.Drawing.Point(154, 129)
        Me.lblSnfAmount.Name = "lblSnfAmount"
        Me.lblSnfAmount.Size = New System.Drawing.Size(235, 21)
        Me.lblSnfAmount.TabIndex = 335
        Me.lblSnfAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Location = New System.Drawing.Point(39, 129)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(86, 18)
        Me.MyLabel20.TabIndex = 334
        Me.MyLabel20.Text = "SNF Amount (2)"
        '
        'lblFatAmount
        '
        Me.lblFatAmount.AutoSize = False
        Me.lblFatAmount.BorderVisible = True
        Me.lblFatAmount.FieldName = Nothing
        Me.lblFatAmount.Location = New System.Drawing.Point(154, 106)
        Me.lblFatAmount.Name = "lblFatAmount"
        Me.lblFatAmount.Size = New System.Drawing.Size(235, 21)
        Me.lblFatAmount.TabIndex = 333
        Me.lblFatAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Location = New System.Drawing.Point(39, 109)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(84, 18)
        Me.MyLabel22.TabIndex = 332
        Me.MyLabel22.Text = "FAT Amount (1)"
        '
        'lblSNFRate
        '
        Me.lblSNFRate.AutoSize = False
        Me.lblSNFRate.BorderVisible = True
        Me.lblSNFRate.FieldName = Nothing
        Me.lblSNFRate.Location = New System.Drawing.Point(154, 83)
        Me.lblSNFRate.Name = "lblSNFRate"
        Me.lblSNFRate.Size = New System.Drawing.Size(235, 21)
        Me.lblSNFRate.TabIndex = 331
        Me.lblSNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Location = New System.Drawing.Point(39, 86)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(52, 18)
        Me.MyLabel16.TabIndex = 330
        Me.MyLabel16.Text = "SNF Rate"
        '
        'lblFATRate
        '
        Me.lblFATRate.AutoSize = False
        Me.lblFATRate.BorderVisible = True
        Me.lblFATRate.FieldName = Nothing
        Me.lblFATRate.Location = New System.Drawing.Point(154, 60)
        Me.lblFATRate.Name = "lblFATRate"
        Me.lblFATRate.Size = New System.Drawing.Size(235, 21)
        Me.lblFATRate.TabIndex = 329
        Me.lblFATRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(39, 63)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(50, 18)
        Me.MyLabel18.TabIndex = 328
        Me.MyLabel18.Text = "FAT Rate"
        '
        'lblSNFKG
        '
        Me.lblSNFKG.AutoSize = False
        Me.lblSNFKG.BorderVisible = True
        Me.lblSNFKG.FieldName = Nothing
        Me.lblSNFKG.Location = New System.Drawing.Point(154, 37)
        Me.lblSNFKG.Name = "lblSNFKG"
        Me.lblSNFKG.Size = New System.Drawing.Size(235, 21)
        Me.lblSNFKG.TabIndex = 327
        Me.lblSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Location = New System.Drawing.Point(39, 40)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(44, 18)
        Me.MyLabel14.TabIndex = 326
        Me.MyLabel14.Text = "SNF KG"
        '
        'lblFATKg
        '
        Me.lblFATKg.AutoSize = False
        Me.lblFATKg.BorderVisible = True
        Me.lblFATKg.FieldName = Nothing
        Me.lblFATKg.Location = New System.Drawing.Point(154, 14)
        Me.lblFATKg.Name = "lblFATKg"
        Me.lblFATKg.Size = New System.Drawing.Size(235, 21)
        Me.lblFATKg.TabIndex = 325
        Me.lblFATKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(39, 17)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel12.TabIndex = 324
        Me.MyLabel12.Text = "FAT KG"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvReceipt)
        Me.RadPageViewPage3.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1083, 400)
        Me.RadPageViewPage3.Text = "Receipt Details"
        Me.RadPageViewPage3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gvReceipt
        '
        Me.gvReceipt.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvReceipt.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvReceipt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvReceipt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvReceipt.ForeColor = System.Drawing.Color.Black
        Me.gvReceipt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvReceipt.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvReceipt.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gvReceipt.MasterTemplate.AllowDeleteRow = False
        Me.gvReceipt.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvReceipt.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvReceipt.MasterTemplate.ViewDefinition = TableViewDefinition4
        Me.gvReceipt.MyStopExport = False
        Me.gvReceipt.Name = "gvReceipt"
        Me.gvReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvReceipt.ShowGroupPanel = False
        Me.gvReceipt.ShowHeaderCellButtons = True
        Me.gvReceipt.Size = New System.Drawing.Size(1083, 367)
        Me.gvReceipt.TabIndex = 1
        Me.gvReceipt.TabStop = False
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadButton1)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel1.Location = New System.Drawing.Point(0, 367)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1083, 33)
        Me.RadPanel1.TabIndex = 0
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(3, 6)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(157, 22)
        Me.RadButton1.TabIndex = 1
        Me.RadButton1.Text = "Update Receipt Details"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(363, 4)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(96, 22)
        Me.btnShowInventory.TabIndex = 49
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'btnPrintPO
        '
        Me.btnPrintPO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintPO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintPO.Location = New System.Drawing.Point(291, 4)
        Me.btnPrintPO.Name = "btnPrintPO"
        Me.btnPrintPO.Size = New System.Drawing.Size(68, 22)
        Me.btnPrintPO.TabIndex = 6
        Me.btnPrintPO.Text = "Print PO"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(463, 4)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(247, 22)
        Me.btnReverse.TabIndex = 5
        Me.btnReverse.Text = "Reverse and Recreate with Secondary QC"
        Me.btnReverse.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1033, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 22)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(147, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 22)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(75, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'frmMilkJobWorkTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 511)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMilkJobWorkTransfer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmBulkMilkSRN"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkTransferIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtManualPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStanadardrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfatPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVirtual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVirtual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpSRNDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.QcDetails.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCOutTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCOutTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCInTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCInTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.grpParameterDetailBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpParameterDetailBulk.ResumeLayout(False)
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvRange.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvRange, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.lblStandardRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalQtyValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSpecialDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIncentive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblActualAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSnfAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFatAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSNFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFATRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSNFKG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFATKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvReceipt.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintPO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents dtpSRNDATE As common.Controls.MyDateTimePicker
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents lblSRNNo As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents QcDetails As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dtpWeighmentDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents txtChallanNo As common.Controls.MyTextBox
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents dtpChallanDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents lblPriceChart As common.Controls.MyLabel
    Friend WithEvents fndPriceChart As common.UserControls.txtFinder
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtQCNo As common.Controls.MyTextBox
    Friend WithEvents lblQCInTime As common.Controls.MyLabel
    Friend WithEvents lblQCNo As common.Controls.MyLabel
    Friend WithEvents dtpQCInTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblQCOutTime As common.Controls.MyLabel
    Friend WithEvents dtpQCOutTime As common.Controls.MyDateTimePicker
    Friend WithEvents txtDipValue As common.Controls.MyTextBox
    Friend WithEvents lblDipValue As common.Controls.MyLabel
    Friend WithEvents grpParameterDetailBulk As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvParam As common.UserControls.MyRadGridView
    Friend WithEvents txtVendor As common.Controls.MyTextBox
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblDeduction As common.Controls.MyLabel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents lblTotalAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents lblSnfAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents lblFatAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents lblSNFRate As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents lblFATRate As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents lblSNFKG As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents lblFATKg As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblActualAmount As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtGateEntryNo As common.Controls.MyTextBox
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents lblIncentive As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblSpecialDeduction As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvRange As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtSNFWeightage As common.MyNumBox
    Friend WithEvents TxtFatWeightage As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtStanadardrate As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtfatPercentage As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtSNFPercentage As common.MyNumBox
    Friend WithEvents lblTankerTransporterName As common.Controls.MyLabel
    Friend WithEvents lblTotalQtyValue As common.Controls.MyLabel
    Friend WithEvents lblTotalQty As common.Controls.MyLabel
    Friend WithEvents lblStandardRate As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents txtTolerance As common.MyNumBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents btnPrintPO As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblReverse As common.Controls.MyLabel
    Friend WithEvents lblJobWork As common.Controls.MyLabel
    Friend WithEvents lblVirtual As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtVirtual As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtSRNNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtDocType As common.Controls.MyLabel
    Friend WithEvents txtMilkTransferIn As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents fndTankerNo As common.Controls.MyTextBox
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents txtJobWork As common.UserControls.txtFinder
    Friend WithEvents fndWeighmentNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents txtManualPrice As common.MyNumBox
    Friend WithEvents btnShowInventory As RadButton
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents gvReceipt As common.UserControls.MyRadGridView
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents RadButton1 As RadButton
End Class

