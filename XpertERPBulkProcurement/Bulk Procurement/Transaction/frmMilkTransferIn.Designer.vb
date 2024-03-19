<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkTransferIn
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
        Dim TableViewDefinition7 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition8 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition9 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition10 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition11 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition12 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblDocumentAmt = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtSNFPercentage = New common.MyNumBox()
        Me.TxtSNFWeightage = New common.MyNumBox()
        Me.TxtFatWeightage = New common.MyNumBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.txtfatPercentage = New common.MyNumBox()
        Me.lblPriceChart = New common.Controls.MyLabel()
        Me.fndPriceChart = New common.UserControls.txtFinder()
        Me.txtMccPlantCode = New common.UserControls.txtFinder()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpDispatchDateAndTime = New common.Controls.MyDateTimePicker()
        Me.fndDispatchChallanNo = New common.UserControls.txtFinder()
        Me.lblMccPlantName = New common.Controls.MyLabel()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.lblDateAndTime = New common.Controls.MyLabel()
        Me.dtpRcptDateAndTime = New common.Controls.MyDateTimePicker()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.fndRcptChalanNo = New common.UserControls.txtNavigator()
        Me.lChalanNo = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.fndReferenceNo = New common.Controls.MyTextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.chkJobWork = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.txtTransferPrice = New common.MyNumBox()
        Me.txtDispatchFrom = New common.Controls.MyTextBox()
        Me.lblDispatchFromDesc = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtKmReadingRecpt = New common.Controls.MyTextBox()
        Me.txtDip = New common.Controls.MyTextBox()
        Me.lblTankerFull = New common.Controls.MyLabel()
        Me.txtKMReadingDisp = New common.Controls.MyTextBox()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtTankerNo = New common.Controls.MyTextBox()
        Me.lblDripMarking = New common.Controls.MyLabel()
        Me.lblTankerKmReading = New common.Controls.MyLabel()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gvOldSealPaper = New common.UserControls.MyRadGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.gvOldSeal = New common.UserControls.MyRadGridView()
        Me.chkNewSealNo = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.txtRcptControlSampleSNF = New common.Controls.MyTextBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtRcptControlSampleFAT = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDispControlSampleSNF = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtDispControlSampleFAT = New common.Controls.MyTextBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtgateEntryNo = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.dtpWeighmentDate = New common.Controls.MyDateTimePicker()
        Me.txtWeighmentNo = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtDipW = New common.Controls.MyTextBox()
        Me.gvWeighment = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.dtpQCOutDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.dtpQcInDate = New common.Controls.MyDateTimePicker()
        Me.txtQCNo = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.gvParam = New common.UserControls.MyRadGridView()
        Me.btnJE = New Telerik.WinControls.UI.RadButton()
        Me.btnShowInventory = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer8 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.gvNewSealPaper = New common.UserControls.MyRadGridView()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.gvNewSeal = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblDocumentAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfatPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPriceChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDispatchDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMccPlantName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpRcptDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lChalanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndReferenceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTransferPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispatchFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispatchFromDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKmReadingRecpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerFull, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKMReadingDisp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDripMarking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerKmReading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvOldSealPaper, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOldSealPaper.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.gvOldSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvOldSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gvOldSeal.SuspendLayout()
        CType(Me.chkNewSealNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.txtRcptControlSampleSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRcptControlSampleFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispControlSampleSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispControlSampleFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtgateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDipW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvWeighment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvWeighment.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCOutDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQcInDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer8.Panel1.SuspendLayout()
        Me.SplitContainer8.Panel2.SuspendLayout()
        Me.SplitContainer8.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.gvNewSealPaper, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvNewSealPaper.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.gvNewSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvNewSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(284, 19)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(68, 18)
        Me.btnReverse.TabIndex = 7
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(214, 19)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 6
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnJE)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnShowInventory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1122, 487)
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 1
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDocumentAmt)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel17)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMccPlantCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpDispatchDateAndTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblWeighmentNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndDispatchChallanNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblMccPlantName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblVendor)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDateAndTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpRcptDateAndTime)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndRcptChalanNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lChalanNo)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1122, 444)
        Me.SplitContainer2.SplitterDistance = 112
        Me.SplitContainer2.TabIndex = 0
        '
        'lblDocumentAmt
        '
        Me.lblDocumentAmt.AutoSize = False
        Me.lblDocumentAmt.BorderVisible = True
        Me.lblDocumentAmt.FieldName = Nothing
        Me.lblDocumentAmt.Location = New System.Drawing.Point(137, 75)
        Me.lblDocumentAmt.Name = "lblDocumentAmt"
        Me.lblDocumentAmt.Size = New System.Drawing.Size(156, 21)
        Me.lblDocumentAmt.TabIndex = 341
        Me.lblDocumentAmt.Visible = False
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(5, 75)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(102, 18)
        Me.MyLabel17.TabIndex = 340
        Me.MyLabel17.Text = "Document Amount"
        Me.MyLabel17.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.MyLabel18)
        Me.GroupBox5.Controls.Add(Me.txtSNFPercentage)
        Me.GroupBox5.Controls.Add(Me.TxtSNFWeightage)
        Me.GroupBox5.Controls.Add(Me.TxtFatWeightage)
        Me.GroupBox5.Controls.Add(Me.MyLabel19)
        Me.GroupBox5.Controls.Add(Me.MyLabel20)
        Me.GroupBox5.Controls.Add(Me.MyLabel22)
        Me.GroupBox5.Controls.Add(Me.txtfatPercentage)
        Me.GroupBox5.Controls.Add(Me.lblPriceChart)
        Me.GroupBox5.Controls.Add(Me.fndPriceChart)
        Me.GroupBox5.Location = New System.Drawing.Point(589, 33)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(521, 59)
        Me.GroupBox5.TabIndex = 339
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Price Detail"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel18.Location = New System.Drawing.Point(384, 13)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(58, 16)
        Me.MyLabel18.TabIndex = 344
        Me.MyLabel18.Text = "SNF Ratio"
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
        Me.txtSNFPercentage.Location = New System.Drawing.Point(455, 12)
        Me.txtSNFPercentage.MendatroryField = True
        Me.txtSNFPercentage.MyLinkLable1 = Nothing
        Me.txtSNFPercentage.MyLinkLable2 = Nothing
        Me.txtSNFPercentage.Name = "txtSNFPercentage"
        Me.txtSNFPercentage.ReferenceFieldDesc = Nothing
        Me.txtSNFPercentage.ReferenceFieldName = Nothing
        Me.txtSNFPercentage.ReferenceTableName = Nothing
        Me.txtSNFPercentage.Size = New System.Drawing.Size(61, 20)
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
        Me.TxtSNFWeightage.Location = New System.Drawing.Point(309, 33)
        Me.TxtSNFWeightage.MendatroryField = True
        Me.TxtSNFWeightage.MyLinkLable1 = Nothing
        Me.TxtSNFWeightage.MyLinkLable2 = Nothing
        Me.TxtSNFWeightage.Name = "TxtSNFWeightage"
        Me.TxtSNFWeightage.ReferenceFieldDesc = Nothing
        Me.TxtSNFWeightage.ReferenceFieldName = Nothing
        Me.TxtSNFWeightage.ReferenceTableName = Nothing
        Me.TxtSNFWeightage.Size = New System.Drawing.Size(69, 20)
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
        Me.TxtFatWeightage.Location = New System.Drawing.Point(309, 9)
        Me.TxtFatWeightage.MendatroryField = True
        Me.TxtFatWeightage.MyLinkLable1 = Nothing
        Me.TxtFatWeightage.MyLinkLable2 = Nothing
        Me.TxtFatWeightage.Name = "TxtFatWeightage"
        Me.TxtFatWeightage.ReferenceFieldDesc = Nothing
        Me.TxtFatWeightage.ReferenceFieldName = Nothing
        Me.TxtFatWeightage.ReferenceTableName = Nothing
        Me.TxtFatWeightage.Size = New System.Drawing.Size(70, 20)
        Me.TxtFatWeightage.TabIndex = 335
        Me.TxtFatWeightage.Text = "0"
        Me.TxtFatWeightage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtFatWeightage.Value = 0R
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel19.Location = New System.Drawing.Point(224, 35)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel19.TabIndex = 342
        Me.MyLabel19.Text = "SNF Weightage"
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel20.Location = New System.Drawing.Point(384, 35)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel20.TabIndex = 340
        Me.MyLabel20.Text = "FAT Ratio"
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel22.Location = New System.Drawing.Point(224, 11)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel22.TabIndex = 339
        Me.MyLabel22.Text = "FAT Weightage"
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
        Me.txtfatPercentage.Location = New System.Drawing.Point(455, 35)
        Me.txtfatPercentage.MendatroryField = True
        Me.txtfatPercentage.MyLinkLable1 = Nothing
        Me.txtfatPercentage.MyLinkLable2 = Nothing
        Me.txtfatPercentage.Name = "txtfatPercentage"
        Me.txtfatPercentage.ReferenceFieldDesc = Nothing
        Me.txtfatPercentage.ReferenceFieldName = Nothing
        Me.txtfatPercentage.ReferenceTableName = Nothing
        Me.txtfatPercentage.Size = New System.Drawing.Size(61, 20)
        Me.txtfatPercentage.TabIndex = 337
        Me.txtfatPercentage.Text = "0"
        Me.txtfatPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtfatPercentage.Value = 0R
        '
        'lblPriceChart
        '
        Me.lblPriceChart.FieldName = Nothing
        Me.lblPriceChart.Location = New System.Drawing.Point(7, 15)
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
        Me.fndPriceChart.Location = New System.Drawing.Point(101, 13)
        Me.fndPriceChart.MendatroryField = True
        Me.fndPriceChart.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceChart.MyLinkLable1 = Me.lblPriceChart
        Me.fndPriceChart.MyLinkLable2 = Nothing
        Me.fndPriceChart.MyReadOnly = False
        Me.fndPriceChart.MyShowMasterFormButton = False
        Me.fndPriceChart.Name = "fndPriceChart"
        Me.fndPriceChart.ReferenceFieldDesc = Nothing
        Me.fndPriceChart.ReferenceFieldName = Nothing
        Me.fndPriceChart.ReferenceTableName = Nothing
        Me.fndPriceChart.Size = New System.Drawing.Size(117, 21)
        Me.fndPriceChart.TabIndex = 9
        Me.fndPriceChart.Value = ""
        '
        'txtMccPlantCode
        '
        Me.txtMccPlantCode.CalculationExpression = Nothing
        Me.txtMccPlantCode.FieldCode = Nothing
        Me.txtMccPlantCode.FieldDesc = Nothing
        Me.txtMccPlantCode.FieldMaxLength = 0
        Me.txtMccPlantCode.FieldName = Nothing
        Me.txtMccPlantCode.isCalculatedField = False
        Me.txtMccPlantCode.IsSourceFromTable = False
        Me.txtMccPlantCode.IsSourceFromValueList = False
        Me.txtMccPlantCode.IsUnique = False
        Me.txtMccPlantCode.Location = New System.Drawing.Point(137, 32)
        Me.txtMccPlantCode.MendatroryField = True
        Me.txtMccPlantCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMccPlantCode.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtMccPlantCode.MyLinkLable2 = Nothing
        Me.txtMccPlantCode.MyReadOnly = False
        Me.txtMccPlantCode.MyShowMasterFormButton = False
        Me.txtMccPlantCode.Name = "txtMccPlantCode"
        Me.txtMccPlantCode.ReferenceFieldDesc = Nothing
        Me.txtMccPlantCode.ReferenceFieldName = Nothing
        Me.txtMccPlantCode.ReferenceTableName = Nothing
        Me.txtMccPlantCode.Size = New System.Drawing.Size(156, 19)
        Me.txtMccPlantCode.TabIndex = 331
        Me.txtMccPlantCode.Value = ""
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblWeighmentNo.Location = New System.Drawing.Point(5, 53)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(110, 18)
        Me.lblWeighmentNo.TabIndex = 328
        Me.lblWeighmentNo.Text = "Dispatch Challan No."
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(299, 58)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(119, 16)
        Me.MyLabel1.TabIndex = 330
        Me.MyLabel1.Text = "Dispatch Challan Date"
        '
        'dtpDispatchDateAndTime
        '
        Me.dtpDispatchDateAndTime.CalculationExpression = Nothing
        Me.dtpDispatchDateAndTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpDispatchDateAndTime.FieldCode = Nothing
        Me.dtpDispatchDateAndTime.FieldDesc = Nothing
        Me.dtpDispatchDateAndTime.FieldMaxLength = 0
        Me.dtpDispatchDateAndTime.FieldName = Nothing
        Me.dtpDispatchDateAndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDispatchDateAndTime.isCalculatedField = False
        Me.dtpDispatchDateAndTime.IsSourceFromTable = False
        Me.dtpDispatchDateAndTime.IsSourceFromValueList = False
        Me.dtpDispatchDateAndTime.IsUnique = False
        Me.dtpDispatchDateAndTime.Location = New System.Drawing.Point(455, 55)
        Me.dtpDispatchDateAndTime.MendatroryField = False
        Me.dtpDispatchDateAndTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDispatchDateAndTime.MyLinkLable1 = Me.MyLabel1
        Me.dtpDispatchDateAndTime.MyLinkLable2 = Nothing
        Me.dtpDispatchDateAndTime.Name = "dtpDispatchDateAndTime"
        Me.dtpDispatchDateAndTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDispatchDateAndTime.ReadOnly = True
        Me.dtpDispatchDateAndTime.ReferenceFieldDesc = Nothing
        Me.dtpDispatchDateAndTime.ReferenceFieldName = Nothing
        Me.dtpDispatchDateAndTime.ReferenceTableName = Nothing
        Me.dtpDispatchDateAndTime.Size = New System.Drawing.Size(131, 20)
        Me.dtpDispatchDateAndTime.TabIndex = 329
        Me.dtpDispatchDateAndTime.TabStop = False
        Me.dtpDispatchDateAndTime.Text = "10/06/2011 11:51 AM"
        Me.dtpDispatchDateAndTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'fndDispatchChallanNo
        '
        Me.fndDispatchChallanNo.CalculationExpression = Nothing
        Me.fndDispatchChallanNo.FieldCode = Nothing
        Me.fndDispatchChallanNo.FieldDesc = Nothing
        Me.fndDispatchChallanNo.FieldMaxLength = 0
        Me.fndDispatchChallanNo.FieldName = Nothing
        Me.fndDispatchChallanNo.isCalculatedField = False
        Me.fndDispatchChallanNo.IsSourceFromTable = False
        Me.fndDispatchChallanNo.IsSourceFromValueList = False
        Me.fndDispatchChallanNo.IsUnique = False
        Me.fndDispatchChallanNo.Location = New System.Drawing.Point(137, 54)
        Me.fndDispatchChallanNo.MendatroryField = True
        Me.fndDispatchChallanNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndDispatchChallanNo.MyLinkLable1 = Me.lblWeighmentNo
        Me.fndDispatchChallanNo.MyLinkLable2 = Nothing
        Me.fndDispatchChallanNo.MyReadOnly = False
        Me.fndDispatchChallanNo.MyShowMasterFormButton = False
        Me.fndDispatchChallanNo.Name = "fndDispatchChallanNo"
        Me.fndDispatchChallanNo.ReferenceFieldDesc = Nothing
        Me.fndDispatchChallanNo.ReferenceFieldName = Nothing
        Me.fndDispatchChallanNo.ReferenceTableName = Nothing
        Me.fndDispatchChallanNo.Size = New System.Drawing.Size(156, 19)
        Me.fndDispatchChallanNo.TabIndex = 327
        Me.fndDispatchChallanNo.Value = ""
        '
        'lblMccPlantName
        '
        Me.lblMccPlantName.AutoSize = False
        Me.lblMccPlantName.BorderVisible = True
        Me.lblMccPlantName.FieldName = Nothing
        Me.lblMccPlantName.Location = New System.Drawing.Point(299, 33)
        Me.lblMccPlantName.Name = "lblMccPlantName"
        Me.lblMccPlantName.Size = New System.Drawing.Size(285, 21)
        Me.lblMccPlantName.TabIndex = 326
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Location = New System.Drawing.Point(6, 32)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(60, 18)
        Me.lblVendor.TabIndex = 325
        Me.lblVendor.Text = "MCC/Plant"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(725, 8)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(118, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 210
        '
        'lblDateAndTime
        '
        Me.lblDateAndTime.FieldName = Nothing
        Me.lblDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTime.Location = New System.Drawing.Point(463, 10)
        Me.lblDateAndTime.Name = "lblDateAndTime"
        Me.lblDateAndTime.Size = New System.Drawing.Size(113, 16)
        Me.lblDateAndTime.TabIndex = 136
        Me.lblDateAndTime.Text = "Receipt Challan Date"
        '
        'dtpRcptDateAndTime
        '
        Me.dtpRcptDateAndTime.CalculationExpression = Nothing
        Me.dtpRcptDateAndTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpRcptDateAndTime.FieldCode = Nothing
        Me.dtpRcptDateAndTime.FieldDesc = Nothing
        Me.dtpRcptDateAndTime.FieldMaxLength = 0
        Me.dtpRcptDateAndTime.FieldName = Nothing
        Me.dtpRcptDateAndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRcptDateAndTime.isCalculatedField = False
        Me.dtpRcptDateAndTime.IsSourceFromTable = False
        Me.dtpRcptDateAndTime.IsSourceFromValueList = False
        Me.dtpRcptDateAndTime.IsUnique = False
        Me.dtpRcptDateAndTime.Location = New System.Drawing.Point(587, 8)
        Me.dtpRcptDateAndTime.MendatroryField = False
        Me.dtpRcptDateAndTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpRcptDateAndTime.MyLinkLable1 = Me.lblDateAndTime
        Me.dtpRcptDateAndTime.MyLinkLable2 = Nothing
        Me.dtpRcptDateAndTime.Name = "dtpRcptDateAndTime"
        Me.dtpRcptDateAndTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpRcptDateAndTime.ReferenceFieldDesc = Nothing
        Me.dtpRcptDateAndTime.ReferenceFieldName = Nothing
        Me.dtpRcptDateAndTime.ReferenceTableName = Nothing
        Me.dtpRcptDateAndTime.Size = New System.Drawing.Size(132, 20)
        Me.dtpRcptDateAndTime.TabIndex = 135
        Me.dtpRcptDateAndTime.TabStop = False
        Me.dtpRcptDateAndTime.Text = "10/06/2011 11:51 AM"
        Me.dtpRcptDateAndTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(442, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 21)
        Me.btnReset.TabIndex = 133
        '
        'fndRcptChalanNo
        '
        Me.fndRcptChalanNo.FieldName = Nothing
        Me.fndRcptChalanNo.Location = New System.Drawing.Point(138, 8)
        Me.fndRcptChalanNo.MendatroryField = False
        Me.fndRcptChalanNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndRcptChalanNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndRcptChalanNo.MyLinkLable1 = Me.lChalanNo
        Me.fndRcptChalanNo.MyLinkLable2 = Nothing
        Me.fndRcptChalanNo.MyMaxLength = 32767
        Me.fndRcptChalanNo.MyReadOnly = False
        Me.fndRcptChalanNo.Name = "fndRcptChalanNo"
        Me.fndRcptChalanNo.Size = New System.Drawing.Size(304, 21)
        Me.fndRcptChalanNo.TabIndex = 132
        Me.fndRcptChalanNo.Value = ""
        '
        'lChalanNo
        '
        Me.lChalanNo.FieldName = Nothing
        Me.lChalanNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lChalanNo.Location = New System.Drawing.Point(5, 10)
        Me.lChalanNo.Name = "lChalanNo"
        Me.lChalanNo.Size = New System.Drawing.Size(107, 16)
        Me.lChalanNo.TabIndex = 134
        Me.lChalanNo.Text = "Receipt Challan No."
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1122, 328)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1101, 280)
        Me.RadPageViewPage1.Tag = ""
        Me.RadPageViewPage1.Text = "Challan Details"
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
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel15)
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndReferenceNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtTransferPrice)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDispatchFrom)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDispatchFromDesc)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtKmReadingRecpt)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDip)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerFull)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtKMReadingDisp)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtTankerNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDripMarking)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTankerKmReading)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer3.Size = New System.Drawing.Size(1101, 280)
        Me.SplitContainer3.SplitterDistance = 94
        Me.SplitContainer3.TabIndex = 343
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(12, 72)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel15.TabIndex = 363
        Me.MyLabel15.Text = "Reference No"
        '
        'fndReferenceNo
        '
        Me.fndReferenceNo.CalculationExpression = Nothing
        Me.fndReferenceNo.FieldCode = Nothing
        Me.fndReferenceNo.FieldDesc = Nothing
        Me.fndReferenceNo.FieldMaxLength = 0
        Me.fndReferenceNo.FieldName = Nothing
        Me.fndReferenceNo.isCalculatedField = False
        Me.fndReferenceNo.IsSourceFromTable = False
        Me.fndReferenceNo.IsSourceFromValueList = False
        Me.fndReferenceNo.IsUnique = False
        Me.fndReferenceNo.Location = New System.Drawing.Point(120, 71)
        Me.fndReferenceNo.MaxLength = 50
        Me.fndReferenceNo.MendatroryField = False
        Me.fndReferenceNo.MyLinkLable1 = Nothing
        Me.fndReferenceNo.MyLinkLable2 = Nothing
        Me.fndReferenceNo.Name = "fndReferenceNo"
        Me.fndReferenceNo.ReadOnly = True
        Me.fndReferenceNo.ReferenceFieldDesc = Nothing
        Me.fndReferenceNo.ReferenceFieldName = Nothing
        Me.fndReferenceNo.ReferenceTableName = Nothing
        Me.fndReferenceNo.Size = New System.Drawing.Size(229, 20)
        Me.fndReferenceNo.TabIndex = 364
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblSubLocation)
        Me.Panel3.Controls.Add(Me.chkJobWork)
        Me.Panel3.Controls.Add(Me.MyLabel16)
        Me.Panel3.Controls.Add(Me.txtSubLocation)
        Me.Panel3.Location = New System.Drawing.Point(710, 25)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(389, 44)
        Me.Panel3.TabIndex = 362
        Me.Panel3.Visible = False
        '
        'lblSubLocation
        '
        Me.lblSubLocation.AutoSize = False
        Me.lblSubLocation.BorderVisible = True
        Me.lblSubLocation.FieldName = Nothing
        Me.lblSubLocation.Location = New System.Drawing.Point(213, 21)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(174, 19)
        Me.lblSubLocation.TabIndex = 276
        '
        'chkJobWork
        '
        Me.chkJobWork.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWork.Location = New System.Drawing.Point(3, 4)
        Me.chkJobWork.Name = "chkJobWork"
        Me.chkJobWork.Size = New System.Drawing.Size(80, 16)
        Me.chkJobWork.TabIndex = 346
        Me.chkJobWork.Text = "Is Job Work"
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
        Me.txtSubLocation.Location = New System.Drawing.Point(81, 21)
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
        'txtTransferPrice
        '
        Me.txtTransferPrice.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTransferPrice.CalculationExpression = Nothing
        Me.txtTransferPrice.DecimalPlaces = 2
        Me.txtTransferPrice.FieldCode = Nothing
        Me.txtTransferPrice.FieldDesc = Nothing
        Me.txtTransferPrice.FieldMaxLength = 0
        Me.txtTransferPrice.FieldName = Nothing
        Me.txtTransferPrice.isCalculatedField = False
        Me.txtTransferPrice.IsSourceFromTable = False
        Me.txtTransferPrice.IsSourceFromValueList = False
        Me.txtTransferPrice.IsUnique = False
        Me.txtTransferPrice.Location = New System.Drawing.Point(797, 7)
        Me.txtTransferPrice.MendatroryField = False
        Me.txtTransferPrice.MyLinkLable1 = Nothing
        Me.txtTransferPrice.MyLinkLable2 = Nothing
        Me.txtTransferPrice.Name = "txtTransferPrice"
        Me.txtTransferPrice.ReferenceFieldDesc = Nothing
        Me.txtTransferPrice.ReferenceFieldName = Nothing
        Me.txtTransferPrice.ReferenceTableName = Nothing
        Me.txtTransferPrice.Size = New System.Drawing.Size(125, 20)
        Me.txtTransferPrice.TabIndex = 361
        Me.txtTransferPrice.Text = "0"
        Me.txtTransferPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTransferPrice.Value = 0R
        '
        'txtDispatchFrom
        '
        Me.txtDispatchFrom.CalculationExpression = Nothing
        Me.txtDispatchFrom.FieldCode = Nothing
        Me.txtDispatchFrom.FieldDesc = Nothing
        Me.txtDispatchFrom.FieldMaxLength = 0
        Me.txtDispatchFrom.FieldName = Nothing
        Me.txtDispatchFrom.isCalculatedField = False
        Me.txtDispatchFrom.IsSourceFromTable = False
        Me.txtDispatchFrom.IsSourceFromValueList = False
        Me.txtDispatchFrom.IsUnique = False
        Me.txtDispatchFrom.Location = New System.Drawing.Point(121, 3)
        Me.txtDispatchFrom.MaxLength = 50
        Me.txtDispatchFrom.MendatroryField = False
        Me.txtDispatchFrom.MyLinkLable1 = Nothing
        Me.txtDispatchFrom.MyLinkLable2 = Nothing
        Me.txtDispatchFrom.Name = "txtDispatchFrom"
        Me.txtDispatchFrom.ReadOnly = True
        Me.txtDispatchFrom.ReferenceFieldDesc = Nothing
        Me.txtDispatchFrom.ReferenceFieldName = Nothing
        Me.txtDispatchFrom.ReferenceTableName = Nothing
        Me.txtDispatchFrom.Size = New System.Drawing.Size(229, 20)
        Me.txtDispatchFrom.TabIndex = 327
        '
        'lblDispatchFromDesc
        '
        Me.lblDispatchFromDesc.AutoSize = False
        Me.lblDispatchFromDesc.BorderVisible = True
        Me.lblDispatchFromDesc.FieldName = Nothing
        Me.lblDispatchFromDesc.Location = New System.Drawing.Point(351, 3)
        Me.lblDispatchFromDesc.Name = "lblDispatchFromDesc"
        Me.lblDispatchFromDesc.Size = New System.Drawing.Size(357, 21)
        Me.lblDispatchFromDesc.TabIndex = 329
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(715, 7)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel14.TabIndex = 347
        Me.MyLabel14.Text = "Transfer price"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 3)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(91, 18)
        Me.MyLabel3.TabIndex = 328
        Me.MyLabel3.Text = "Dispatched From"
        '
        'txtKmReadingRecpt
        '
        Me.txtKmReadingRecpt.CalculationExpression = Nothing
        Me.txtKmReadingRecpt.FieldCode = Nothing
        Me.txtKmReadingRecpt.FieldDesc = Nothing
        Me.txtKmReadingRecpt.FieldMaxLength = 0
        Me.txtKmReadingRecpt.FieldName = Nothing
        Me.txtKmReadingRecpt.isCalculatedField = False
        Me.txtKmReadingRecpt.IsSourceFromTable = False
        Me.txtKmReadingRecpt.IsSourceFromValueList = False
        Me.txtKmReadingRecpt.IsUnique = False
        Me.txtKmReadingRecpt.Location = New System.Drawing.Point(479, 50)
        Me.txtKmReadingRecpt.MaxLength = 50
        Me.txtKmReadingRecpt.MendatroryField = True
        Me.txtKmReadingRecpt.MyLinkLable1 = Nothing
        Me.txtKmReadingRecpt.MyLinkLable2 = Nothing
        Me.txtKmReadingRecpt.Name = "txtKmReadingRecpt"
        Me.txtKmReadingRecpt.ReferenceFieldDesc = Nothing
        Me.txtKmReadingRecpt.ReferenceFieldName = Nothing
        Me.txtKmReadingRecpt.ReferenceTableName = Nothing
        Me.txtKmReadingRecpt.Size = New System.Drawing.Size(229, 20)
        Me.txtKmReadingRecpt.TabIndex = 339
        '
        'txtDip
        '
        Me.txtDip.CalculationExpression = Nothing
        Me.txtDip.FieldCode = Nothing
        Me.txtDip.FieldDesc = Nothing
        Me.txtDip.FieldMaxLength = 0
        Me.txtDip.FieldName = Nothing
        Me.txtDip.isCalculatedField = False
        Me.txtDip.IsSourceFromTable = False
        Me.txtDip.IsSourceFromValueList = False
        Me.txtDip.IsUnique = False
        Me.txtDip.Location = New System.Drawing.Point(121, 49)
        Me.txtDip.MaxLength = 50
        Me.txtDip.MendatroryField = False
        Me.txtDip.MyLinkLable1 = Nothing
        Me.txtDip.MyLinkLable2 = Nothing
        Me.txtDip.Name = "txtDip"
        Me.txtDip.ReferenceFieldDesc = Nothing
        Me.txtDip.ReferenceFieldName = Nothing
        Me.txtDip.ReferenceTableName = Nothing
        Me.txtDip.Size = New System.Drawing.Size(229, 20)
        Me.txtDip.TabIndex = 338
        '
        'lblTankerFull
        '
        Me.lblTankerFull.FieldName = Nothing
        Me.lblTankerFull.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerFull.Location = New System.Drawing.Point(356, 52)
        Me.lblTankerFull.Name = "lblTankerFull"
        Me.lblTankerFull.Size = New System.Drawing.Size(114, 16)
        Me.lblTankerFull.TabIndex = 332
        Me.lblTankerFull.Text = "KM Reading(Receipt)"
        '
        'txtKMReadingDisp
        '
        Me.txtKMReadingDisp.CalculationExpression = Nothing
        Me.txtKMReadingDisp.FieldCode = Nothing
        Me.txtKMReadingDisp.FieldDesc = Nothing
        Me.txtKMReadingDisp.FieldMaxLength = 0
        Me.txtKMReadingDisp.FieldName = Nothing
        Me.txtKMReadingDisp.isCalculatedField = False
        Me.txtKMReadingDisp.IsSourceFromTable = False
        Me.txtKMReadingDisp.IsSourceFromValueList = False
        Me.txtKMReadingDisp.IsUnique = False
        Me.txtKMReadingDisp.Location = New System.Drawing.Point(479, 27)
        Me.txtKMReadingDisp.MaxLength = 50
        Me.txtKMReadingDisp.MendatroryField = False
        Me.txtKMReadingDisp.MyLinkLable1 = Nothing
        Me.txtKMReadingDisp.MyLinkLable2 = Nothing
        Me.txtKMReadingDisp.Name = "txtKMReadingDisp"
        Me.txtKMReadingDisp.ReadOnly = True
        Me.txtKMReadingDisp.ReferenceFieldDesc = Nothing
        Me.txtKMReadingDisp.ReferenceFieldName = Nothing
        Me.txtKMReadingDisp.ReferenceTableName = Nothing
        Me.txtKMReadingDisp.Size = New System.Drawing.Size(229, 20)
        Me.txtKMReadingDisp.TabIndex = 337
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(13, 27)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(62, 16)
        Me.lblTankerNo.TabIndex = 330
        Me.lblTankerNo.Text = "Tanker No."
        '
        'txtTankerNo
        '
        Me.txtTankerNo.CalculationExpression = Nothing
        Me.txtTankerNo.FieldCode = Nothing
        Me.txtTankerNo.FieldDesc = Nothing
        Me.txtTankerNo.FieldMaxLength = 0
        Me.txtTankerNo.FieldName = Nothing
        Me.txtTankerNo.isCalculatedField = False
        Me.txtTankerNo.IsSourceFromTable = False
        Me.txtTankerNo.IsSourceFromValueList = False
        Me.txtTankerNo.IsUnique = False
        Me.txtTankerNo.Location = New System.Drawing.Point(121, 26)
        Me.txtTankerNo.MaxLength = 50
        Me.txtTankerNo.MendatroryField = False
        Me.txtTankerNo.MyLinkLable1 = Nothing
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReadOnly = True
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(229, 20)
        Me.txtTankerNo.TabIndex = 336
        '
        'lblDripMarking
        '
        Me.lblDripMarking.FieldName = Nothing
        Me.lblDripMarking.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDripMarking.Location = New System.Drawing.Point(13, 50)
        Me.lblDripMarking.Name = "lblDripMarking"
        Me.lblDripMarking.Size = New System.Drawing.Size(55, 16)
        Me.lblDripMarking.TabIndex = 331
        Me.lblDripMarking.Text = "Dip Value"
        '
        'lblTankerKmReading
        '
        Me.lblTankerKmReading.FieldName = Nothing
        Me.lblTankerKmReading.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerKmReading.Location = New System.Drawing.Point(356, 28)
        Me.lblTankerKmReading.Name = "lblTankerKmReading"
        Me.lblTankerKmReading.Size = New System.Drawing.Size(120, 16)
        Me.lblTankerKmReading.TabIndex = 335
        Me.lblTankerKmReading.Text = "KM Reading(Dispatch)"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer4.Size = New System.Drawing.Size(1101, 182)
        Me.SplitContainer4.SplitterDistance = 550
        Me.SplitContainer4.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gvOldSealPaper)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(550, 182)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Paper Seal"
        '
        'gvOldSealPaper
        '
        Me.gvOldSealPaper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvOldSealPaper.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gvOldSealPaper.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvOldSealPaper.MasterTemplate.ShowFilteringRow = False
        Me.gvOldSealPaper.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvOldSealPaper.MasterTemplate.ViewDefinition = TableViewDefinition7
        Me.gvOldSealPaper.MyStopExport = False
        Me.gvOldSealPaper.Name = "gvOldSealPaper"
        Me.gvOldSealPaper.ShowHeaderCellButtons = True
        Me.gvOldSealPaper.Size = New System.Drawing.Size(544, 161)
        Me.gvOldSealPaper.TabIndex = 203
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.gvOldSeal)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(547, 182)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Manual Seal"
        '
        'gvOldSeal
        '
        Me.gvOldSeal.Controls.Add(Me.chkNewSealNo)
        Me.gvOldSeal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvOldSeal.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gvOldSeal.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvOldSeal.MasterTemplate.ShowFilteringRow = False
        Me.gvOldSeal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvOldSeal.MasterTemplate.ViewDefinition = TableViewDefinition8
        Me.gvOldSeal.MyStopExport = False
        Me.gvOldSeal.Name = "gvOldSeal"
        Me.gvOldSeal.ShowHeaderCellButtons = True
        Me.gvOldSeal.Size = New System.Drawing.Size(541, 161)
        Me.gvOldSeal.TabIndex = 202
        '
        'chkNewSealNo
        '
        Me.chkNewSealNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNewSealNo.Location = New System.Drawing.Point(384, 33)
        Me.chkNewSealNo.Name = "chkNewSealNo"
        Me.chkNewSealNo.Size = New System.Drawing.Size(89, 16)
        Me.chkNewSealNo.TabIndex = 342
        Me.chkNewSealNo.Text = "New Seal No."
        Me.chkNewSealNo.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.SplitContainer5)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(111.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1101, 280)
        Me.RadPageViewPage2.Text = "Weighment Details"
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
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtRcptControlSampleSNF)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel12)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtRcptControlSampleFAT)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtDispControlSampleSNF)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtDispControlSampleFAT)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtgateEntryNo)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer5.Panel1.Controls.Add(Me.dtpWeighmentDate)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtWeighmentNo)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtDipW)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.gvWeighment)
        Me.SplitContainer5.Size = New System.Drawing.Size(1101, 280)
        Me.SplitContainer5.SplitterDistance = 68
        Me.SplitContainer5.TabIndex = 0
        '
        'txtRcptControlSampleSNF
        '
        Me.txtRcptControlSampleSNF.CalculationExpression = Nothing
        Me.txtRcptControlSampleSNF.FieldCode = Nothing
        Me.txtRcptControlSampleSNF.FieldDesc = Nothing
        Me.txtRcptControlSampleSNF.FieldMaxLength = 0
        Me.txtRcptControlSampleSNF.FieldName = Nothing
        Me.txtRcptControlSampleSNF.isCalculatedField = False
        Me.txtRcptControlSampleSNF.IsSourceFromTable = False
        Me.txtRcptControlSampleSNF.IsSourceFromValueList = False
        Me.txtRcptControlSampleSNF.IsUnique = False
        Me.txtRcptControlSampleSNF.Location = New System.Drawing.Point(408, 52)
        Me.txtRcptControlSampleSNF.MaxLength = 50
        Me.txtRcptControlSampleSNF.MendatroryField = False
        Me.txtRcptControlSampleSNF.MyLinkLable1 = Nothing
        Me.txtRcptControlSampleSNF.MyLinkLable2 = Nothing
        Me.txtRcptControlSampleSNF.Name = "txtRcptControlSampleSNF"
        Me.txtRcptControlSampleSNF.ReferenceFieldDesc = Nothing
        Me.txtRcptControlSampleSNF.ReferenceFieldName = Nothing
        Me.txtRcptControlSampleSNF.ReferenceTableName = Nothing
        Me.txtRcptControlSampleSNF.Size = New System.Drawing.Size(63, 20)
        Me.txtRcptControlSampleSNF.TabIndex = 359
        Me.txtRcptControlSampleSNF.Visible = False
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(241, 51)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(156, 18)
        Me.MyLabel12.TabIndex = 360
        Me.MyLabel12.Text = "Receipt Control Sample SNF%"
        Me.MyLabel12.Visible = False
        '
        'txtRcptControlSampleFAT
        '
        Me.txtRcptControlSampleFAT.CalculationExpression = Nothing
        Me.txtRcptControlSampleFAT.FieldCode = Nothing
        Me.txtRcptControlSampleFAT.FieldDesc = Nothing
        Me.txtRcptControlSampleFAT.FieldMaxLength = 0
        Me.txtRcptControlSampleFAT.FieldName = Nothing
        Me.txtRcptControlSampleFAT.isCalculatedField = False
        Me.txtRcptControlSampleFAT.IsSourceFromTable = False
        Me.txtRcptControlSampleFAT.IsSourceFromValueList = False
        Me.txtRcptControlSampleFAT.IsUnique = False
        Me.txtRcptControlSampleFAT.Location = New System.Drawing.Point(174, 50)
        Me.txtRcptControlSampleFAT.MaxLength = 50
        Me.txtRcptControlSampleFAT.MendatroryField = False
        Me.txtRcptControlSampleFAT.MyLinkLable1 = Nothing
        Me.txtRcptControlSampleFAT.MyLinkLable2 = Nothing
        Me.txtRcptControlSampleFAT.Name = "txtRcptControlSampleFAT"
        Me.txtRcptControlSampleFAT.ReferenceFieldDesc = Nothing
        Me.txtRcptControlSampleFAT.ReferenceFieldName = Nothing
        Me.txtRcptControlSampleFAT.ReferenceTableName = Nothing
        Me.txtRcptControlSampleFAT.Size = New System.Drawing.Size(63, 20)
        Me.txtRcptControlSampleFAT.TabIndex = 357
        Me.txtRcptControlSampleFAT.Visible = False
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(7, 49)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(155, 18)
        Me.MyLabel13.TabIndex = 358
        Me.MyLabel13.Text = "Receipt Control Sample FAT%"
        Me.MyLabel13.Visible = False
        '
        'txtDispControlSampleSNF
        '
        Me.txtDispControlSampleSNF.CalculationExpression = Nothing
        Me.txtDispControlSampleSNF.FieldCode = Nothing
        Me.txtDispControlSampleSNF.FieldDesc = Nothing
        Me.txtDispControlSampleSNF.FieldMaxLength = 0
        Me.txtDispControlSampleSNF.FieldName = Nothing
        Me.txtDispControlSampleSNF.isCalculatedField = False
        Me.txtDispControlSampleSNF.IsSourceFromTable = False
        Me.txtDispControlSampleSNF.IsSourceFromValueList = False
        Me.txtDispControlSampleSNF.IsUnique = False
        Me.txtDispControlSampleSNF.Location = New System.Drawing.Point(408, 29)
        Me.txtDispControlSampleSNF.MaxLength = 50
        Me.txtDispControlSampleSNF.MendatroryField = False
        Me.txtDispControlSampleSNF.MyLinkLable1 = Nothing
        Me.txtDispControlSampleSNF.MyLinkLable2 = Nothing
        Me.txtDispControlSampleSNF.Name = "txtDispControlSampleSNF"
        Me.txtDispControlSampleSNF.ReadOnly = True
        Me.txtDispControlSampleSNF.ReferenceFieldDesc = Nothing
        Me.txtDispControlSampleSNF.ReferenceFieldName = Nothing
        Me.txtDispControlSampleSNF.ReferenceTableName = Nothing
        Me.txtDispControlSampleSNF.Size = New System.Drawing.Size(63, 20)
        Me.txtDispControlSampleSNF.TabIndex = 355
        Me.txtDispControlSampleSNF.Visible = False
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(241, 29)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(162, 18)
        Me.MyLabel11.TabIndex = 356
        Me.MyLabel11.Text = "Dispatch Control Sample SNF%"
        Me.MyLabel11.Visible = False
        '
        'txtDispControlSampleFAT
        '
        Me.txtDispControlSampleFAT.CalculationExpression = Nothing
        Me.txtDispControlSampleFAT.FieldCode = Nothing
        Me.txtDispControlSampleFAT.FieldDesc = Nothing
        Me.txtDispControlSampleFAT.FieldMaxLength = 0
        Me.txtDispControlSampleFAT.FieldName = Nothing
        Me.txtDispControlSampleFAT.isCalculatedField = False
        Me.txtDispControlSampleFAT.IsSourceFromTable = False
        Me.txtDispControlSampleFAT.IsSourceFromValueList = False
        Me.txtDispControlSampleFAT.IsUnique = False
        Me.txtDispControlSampleFAT.Location = New System.Drawing.Point(174, 27)
        Me.txtDispControlSampleFAT.MaxLength = 50
        Me.txtDispControlSampleFAT.MendatroryField = False
        Me.txtDispControlSampleFAT.MyLinkLable1 = Nothing
        Me.txtDispControlSampleFAT.MyLinkLable2 = Nothing
        Me.txtDispControlSampleFAT.Name = "txtDispControlSampleFAT"
        Me.txtDispControlSampleFAT.ReadOnly = True
        Me.txtDispControlSampleFAT.ReferenceFieldDesc = Nothing
        Me.txtDispControlSampleFAT.ReferenceFieldName = Nothing
        Me.txtDispControlSampleFAT.ReferenceTableName = Nothing
        Me.txtDispControlSampleFAT.Size = New System.Drawing.Size(63, 20)
        Me.txtDispControlSampleFAT.TabIndex = 353
        Me.txtDispControlSampleFAT.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(7, 28)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(161, 18)
        Me.MyLabel5.TabIndex = 354
        Me.MyLabel5.Text = "Dispatch Control Sample FAT%"
        Me.MyLabel5.Visible = False
        '
        'txtgateEntryNo
        '
        Me.txtgateEntryNo.CalculationExpression = Nothing
        Me.txtgateEntryNo.FieldCode = Nothing
        Me.txtgateEntryNo.FieldDesc = Nothing
        Me.txtgateEntryNo.FieldMaxLength = 0
        Me.txtgateEntryNo.FieldName = Nothing
        Me.txtgateEntryNo.isCalculatedField = False
        Me.txtgateEntryNo.IsSourceFromTable = False
        Me.txtgateEntryNo.IsSourceFromValueList = False
        Me.txtgateEntryNo.IsUnique = False
        Me.txtgateEntryNo.Location = New System.Drawing.Point(690, 8)
        Me.txtgateEntryNo.MaxLength = 50
        Me.txtgateEntryNo.MendatroryField = False
        Me.txtgateEntryNo.MyLinkLable1 = Nothing
        Me.txtgateEntryNo.MyLinkLable2 = Nothing
        Me.txtgateEntryNo.Name = "txtgateEntryNo"
        Me.txtgateEntryNo.ReadOnly = True
        Me.txtgateEntryNo.ReferenceFieldDesc = Nothing
        Me.txtgateEntryNo.ReferenceFieldName = Nothing
        Me.txtgateEntryNo.ReferenceTableName = Nothing
        Me.txtgateEntryNo.Size = New System.Drawing.Size(203, 20)
        Me.txtgateEntryNo.TabIndex = 351
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(601, 7)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(79, 18)
        Me.MyLabel2.TabIndex = 352
        Me.MyLabel2.Text = "Gate Entry No."
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(241, 8)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel8.TabIndex = 350
        Me.MyLabel8.Text = "Weighment Date"
        '
        'dtpWeighmentDate
        '
        Me.dtpWeighmentDate.CalculationExpression = Nothing
        Me.dtpWeighmentDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpWeighmentDate.FieldCode = Nothing
        Me.dtpWeighmentDate.FieldDesc = Nothing
        Me.dtpWeighmentDate.FieldMaxLength = 0
        Me.dtpWeighmentDate.FieldName = Nothing
        Me.dtpWeighmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDate.isCalculatedField = False
        Me.dtpWeighmentDate.IsSourceFromTable = False
        Me.dtpWeighmentDate.IsSourceFromValueList = False
        Me.dtpWeighmentDate.IsUnique = False
        Me.dtpWeighmentDate.Location = New System.Drawing.Point(339, 6)
        Me.dtpWeighmentDate.MendatroryField = False
        Me.dtpWeighmentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.MyLinkLable1 = Me.MyLabel8
        Me.dtpWeighmentDate.MyLinkLable2 = Nothing
        Me.dtpWeighmentDate.Name = "dtpWeighmentDate"
        Me.dtpWeighmentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.ReadOnly = True
        Me.dtpWeighmentDate.ReferenceFieldDesc = Nothing
        Me.dtpWeighmentDate.ReferenceFieldName = Nothing
        Me.dtpWeighmentDate.ReferenceTableName = Nothing
        Me.dtpWeighmentDate.Size = New System.Drawing.Size(132, 20)
        Me.dtpWeighmentDate.TabIndex = 349
        Me.dtpWeighmentDate.TabStop = False
        Me.dtpWeighmentDate.Text = "10/06/2011 11:51 AM"
        Me.dtpWeighmentDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtWeighmentNo
        '
        Me.txtWeighmentNo.CalculationExpression = Nothing
        Me.txtWeighmentNo.FieldCode = Nothing
        Me.txtWeighmentNo.FieldDesc = Nothing
        Me.txtWeighmentNo.FieldMaxLength = 0
        Me.txtWeighmentNo.FieldName = Nothing
        Me.txtWeighmentNo.isCalculatedField = False
        Me.txtWeighmentNo.IsSourceFromTable = False
        Me.txtWeighmentNo.IsSourceFromValueList = False
        Me.txtWeighmentNo.IsUnique = False
        Me.txtWeighmentNo.Location = New System.Drawing.Point(97, 5)
        Me.txtWeighmentNo.MaxLength = 50
        Me.txtWeighmentNo.MendatroryField = False
        Me.txtWeighmentNo.MyLinkLable1 = Nothing
        Me.txtWeighmentNo.MyLinkLable2 = Nothing
        Me.txtWeighmentNo.Name = "txtWeighmentNo"
        Me.txtWeighmentNo.ReadOnly = True
        Me.txtWeighmentNo.ReferenceFieldDesc = Nothing
        Me.txtWeighmentNo.ReferenceFieldName = Nothing
        Me.txtWeighmentNo.ReferenceTableName = Nothing
        Me.txtWeighmentNo.Size = New System.Drawing.Size(140, 20)
        Me.txtWeighmentNo.TabIndex = 341
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(8, 6)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(85, 18)
        Me.MyLabel4.TabIndex = 342
        Me.MyLabel4.Text = "Weighment No."
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(474, 8)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel6.TabIndex = 343
        Me.MyLabel6.Text = "DIP Value"
        '
        'txtDipW
        '
        Me.txtDipW.CalculationExpression = Nothing
        Me.txtDipW.FieldCode = Nothing
        Me.txtDipW.FieldDesc = Nothing
        Me.txtDipW.FieldMaxLength = 0
        Me.txtDipW.FieldName = Nothing
        Me.txtDipW.isCalculatedField = False
        Me.txtDipW.IsSourceFromTable = False
        Me.txtDipW.IsSourceFromValueList = False
        Me.txtDipW.IsUnique = False
        Me.txtDipW.Location = New System.Drawing.Point(537, 7)
        Me.txtDipW.MaxLength = 50
        Me.txtDipW.MendatroryField = False
        Me.txtDipW.MyLinkLable1 = Nothing
        Me.txtDipW.MyLinkLable2 = Nothing
        Me.txtDipW.Name = "txtDipW"
        Me.txtDipW.ReadOnly = True
        Me.txtDipW.ReferenceFieldDesc = Nothing
        Me.txtDipW.ReferenceFieldName = Nothing
        Me.txtDipW.ReferenceTableName = Nothing
        Me.txtDipW.Size = New System.Drawing.Size(60, 20)
        Me.txtDipW.TabIndex = 346
        '
        'gvWeighment
        '
        Me.gvWeighment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvWeighment.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvWeighment.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvWeighment.MasterTemplate.ShowFilteringRow = False
        Me.gvWeighment.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvWeighment.MasterTemplate.ViewDefinition = TableViewDefinition9
        Me.gvWeighment.MyStopExport = False
        Me.gvWeighment.Name = "gvWeighment"
        Me.gvWeighment.ShowHeaderCellButtons = True
        Me.gvWeighment.Size = New System.Drawing.Size(1101, 208)
        Me.gvWeighment.TabIndex = 203
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer6)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(69.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1101, 280)
        Me.RadPageViewPage3.Text = "QC Details"
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer6.Panel1.Controls.Add(Me.dtpQCOutDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer6.Panel1.Controls.Add(Me.dtpQcInDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtQCNo)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel7)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.gvParam)
        Me.SplitContainer6.Size = New System.Drawing.Size(1101, 280)
        Me.SplitContainer6.SplitterDistance = 26
        Me.SplitContainer6.TabIndex = 0
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(518, 7)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(100, 16)
        Me.MyLabel10.TabIndex = 360
        Me.MyLabel10.Text = "QC Out Date/Time"
        '
        'dtpQCOutDate
        '
        Me.dtpQCOutDate.CalculationExpression = Nothing
        Me.dtpQCOutDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpQCOutDate.FieldCode = Nothing
        Me.dtpQCOutDate.FieldDesc = Nothing
        Me.dtpQCOutDate.FieldMaxLength = 0
        Me.dtpQCOutDate.FieldName = Nothing
        Me.dtpQCOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCOutDate.isCalculatedField = False
        Me.dtpQCOutDate.IsSourceFromTable = False
        Me.dtpQCOutDate.IsSourceFromValueList = False
        Me.dtpQCOutDate.IsUnique = False
        Me.dtpQCOutDate.Location = New System.Drawing.Point(619, 6)
        Me.dtpQCOutDate.MendatroryField = False
        Me.dtpQCOutDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutDate.MyLinkLable1 = Me.MyLabel10
        Me.dtpQCOutDate.MyLinkLable2 = Nothing
        Me.dtpQCOutDate.Name = "dtpQCOutDate"
        Me.dtpQCOutDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutDate.ReadOnly = True
        Me.dtpQCOutDate.ReferenceFieldDesc = Nothing
        Me.dtpQCOutDate.ReferenceFieldName = Nothing
        Me.dtpQCOutDate.ReferenceTableName = Nothing
        Me.dtpQCOutDate.Size = New System.Drawing.Size(132, 20)
        Me.dtpQCOutDate.TabIndex = 359
        Me.dtpQCOutDate.TabStop = False
        Me.dtpQCOutDate.Text = "10/06/2011 11:51 AM"
        Me.dtpQCOutDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(289, 7)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel9.TabIndex = 358
        Me.MyLabel9.Text = "QC In Date/Time"
        '
        'dtpQcInDate
        '
        Me.dtpQcInDate.CalculationExpression = Nothing
        Me.dtpQcInDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpQcInDate.FieldCode = Nothing
        Me.dtpQcInDate.FieldDesc = Nothing
        Me.dtpQcInDate.FieldMaxLength = 0
        Me.dtpQcInDate.FieldName = Nothing
        Me.dtpQcInDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQcInDate.isCalculatedField = False
        Me.dtpQcInDate.IsSourceFromTable = False
        Me.dtpQcInDate.IsSourceFromValueList = False
        Me.dtpQcInDate.IsUnique = False
        Me.dtpQcInDate.Location = New System.Drawing.Point(386, 5)
        Me.dtpQcInDate.MendatroryField = False
        Me.dtpQcInDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQcInDate.MyLinkLable1 = Me.MyLabel9
        Me.dtpQcInDate.MyLinkLable2 = Nothing
        Me.dtpQcInDate.Name = "dtpQcInDate"
        Me.dtpQcInDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQcInDate.ReadOnly = True
        Me.dtpQcInDate.ReferenceFieldDesc = Nothing
        Me.dtpQcInDate.ReferenceFieldName = Nothing
        Me.dtpQcInDate.ReferenceTableName = Nothing
        Me.dtpQcInDate.Size = New System.Drawing.Size(132, 20)
        Me.dtpQcInDate.TabIndex = 357
        Me.dtpQcInDate.TabStop = False
        Me.dtpQcInDate.Text = "10/06/2011 11:51 AM"
        Me.dtpQcInDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
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
        Me.txtQCNo.Location = New System.Drawing.Point(57, 5)
        Me.txtQCNo.MaxLength = 50
        Me.txtQCNo.MendatroryField = False
        Me.txtQCNo.MyLinkLable1 = Nothing
        Me.txtQCNo.MyLinkLable2 = Nothing
        Me.txtQCNo.Name = "txtQCNo"
        Me.txtQCNo.ReadOnly = True
        Me.txtQCNo.ReferenceFieldDesc = Nothing
        Me.txtQCNo.ReferenceFieldName = Nothing
        Me.txtQCNo.ReferenceTableName = Nothing
        Me.txtQCNo.Size = New System.Drawing.Size(229, 20)
        Me.txtQCNo.TabIndex = 356
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(7, 7)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel7.TabIndex = 355
        Me.MyLabel7.Text = "QC No."
        '
        'gvParam
        '
        Me.gvParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvParam.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvParam.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvParam.MasterTemplate.ShowFilteringRow = False
        Me.gvParam.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvParam.MasterTemplate.ViewDefinition = TableViewDefinition10
        Me.gvParam.MyStopExport = False
        Me.gvParam.Name = "gvParam"
        Me.gvParam.ShowHeaderCellButtons = True
        Me.gvParam.Size = New System.Drawing.Size(1101, 250)
        Me.gvParam.TabIndex = 203
        '
        'btnJE
        '
        Me.btnJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJE.Location = New System.Drawing.Point(976, 19)
        Me.btnJE.Name = "btnJE"
        Me.btnJE.Size = New System.Drawing.Size(71, 18)
        Me.btnJE.TabIndex = 345
        Me.btnJE.Text = "Show JE"
        '
        'btnShowInventory
        '
        Me.btnShowInventory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowInventory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowInventory.Location = New System.Drawing.Point(878, 19)
        Me.btnShowInventory.Name = "btnShowInventory"
        Me.btnShowInventory.Size = New System.Drawing.Size(96, 18)
        Me.btnShowInventory.TabIndex = 344
        Me.btnShowInventory.Text = "Show Inventory"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer8)
        Me.RadGroupBox1.HeaderText = "New Seal No"
        Me.RadGroupBox1.Location = New System.Drawing.Point(592, 17)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(50, 10)
        Me.RadGroupBox1.TabIndex = 343
        Me.RadGroupBox1.Text = "New Seal No"
        Me.RadGroupBox1.Visible = False
        '
        'SplitContainer8
        '
        Me.SplitContainer8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer8.Location = New System.Drawing.Point(2, 18)
        Me.SplitContainer8.Name = "SplitContainer8"
        '
        'SplitContainer8.Panel1
        '
        Me.SplitContainer8.Panel1.Controls.Add(Me.GroupBox3)
        '
        'SplitContainer8.Panel2
        '
        Me.SplitContainer8.Panel2.Controls.Add(Me.GroupBox4)
        Me.SplitContainer8.Size = New System.Drawing.Size(46, 0)
        Me.SplitContainer8.SplitterDistance = 25
        Me.SplitContainer8.TabIndex = 203
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.gvNewSealPaper)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(25, 0)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Paper Seal"
        '
        'gvNewSealPaper
        '
        Me.gvNewSealPaper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvNewSealPaper.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gvNewSealPaper.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvNewSealPaper.MasterTemplate.ShowFilteringRow = False
        Me.gvNewSealPaper.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvNewSealPaper.MasterTemplate.ViewDefinition = TableViewDefinition11
        Me.gvNewSealPaper.MyStopExport = False
        Me.gvNewSealPaper.Name = "gvNewSealPaper"
        Me.gvNewSealPaper.ShowHeaderCellButtons = True
        Me.gvNewSealPaper.Size = New System.Drawing.Size(19, 0)
        Me.gvNewSealPaper.TabIndex = 203
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.gvNewSeal)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(25, 0)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Manual Seal"
        '
        'gvNewSeal
        '
        Me.gvNewSeal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvNewSeal.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gvNewSeal.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvNewSeal.MasterTemplate.ShowFilteringRow = False
        Me.gvNewSeal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvNewSeal.MasterTemplate.ViewDefinition = TableViewDefinition12
        Me.gvNewSeal.MyStopExport = False
        Me.gvNewSeal.Name = "gvNewSeal"
        Me.gvNewSeal.ShowHeaderCellButtons = True
        Me.gvNewSeal.Size = New System.Drawing.Size(19, 0)
        Me.gvNewSeal.TabIndex = 202
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1051, 19)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(143, 19)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 5
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 18)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 19)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'FrmMilkTransferIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1122, 487)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkTransferIn"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkTransferIn"
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblDocumentAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSNFWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFatWeightage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfatPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPriceChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDispatchDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMccPlantName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpRcptDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lChalanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndReferenceNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTransferPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispatchFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispatchFromDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKmReadingRecpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerFull, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKMReadingDisp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDripMarking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerKmReading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gvOldSealPaper.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOldSealPaper, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.gvOldSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvOldSeal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gvOldSeal.ResumeLayout(False)
        Me.gvOldSeal.PerformLayout()
        CType(Me.chkNewSealNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.txtRcptControlSampleSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRcptControlSampleFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispControlSampleSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispControlSampleFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtgateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDipW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvWeighment.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvWeighment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCOutDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQcInDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnShowInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.SplitContainer8.Panel1.ResumeLayout(False)
        Me.SplitContainer8.Panel2.ResumeLayout(False)
        Me.SplitContainer8.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.gvNewSealPaper.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvNewSealPaper, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.gvNewSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvNewSeal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndRcptChalanNo As common.UserControls.txtNavigator
    Friend WithEvents lChalanNo As common.Controls.MyLabel
    Friend WithEvents lblDateAndTime As common.Controls.MyLabel
    Friend WithEvents dtpRcptDateAndTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents lblMccPlantName As common.Controls.MyLabel
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents fndDispatchChallanNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpDispatchDateAndTime As common.Controls.MyDateTimePicker
    Friend WithEvents txtDispatchFrom As common.Controls.MyTextBox
    Friend WithEvents lblDispatchFromDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtKMReadingDisp As common.Controls.MyTextBox
    Friend WithEvents txtTankerNo As common.Controls.MyTextBox
    Friend WithEvents lblTankerKmReading As common.Controls.MyLabel
    Friend WithEvents lblDripMarking As common.Controls.MyLabel
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents lblTankerFull As common.Controls.MyLabel
    Friend WithEvents txtKmReadingRecpt As common.Controls.MyTextBox
    Friend WithEvents txtDip As common.Controls.MyTextBox
    Friend WithEvents gvOldSeal As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvNewSeal As common.UserControls.MyRadGridView
    Friend WithEvents chkNewSealNo As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtWeighmentNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtDipW As common.Controls.MyTextBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents dtpWeighmentDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents dtpQCOutDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents dtpQcInDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtQCNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents gvWeighment As common.UserControls.MyRadGridView
    Friend WithEvents gvParam As common.UserControls.MyRadGridView
    Friend WithEvents txtgateEntryNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents gvOldSealPaper As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer8 As System.Windows.Forms.SplitContainer
    Friend WithEvents gvNewSealPaper As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDispControlSampleFAT As common.Controls.MyTextBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtRcptControlSampleSNF As common.Controls.MyTextBox
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtRcptControlSampleFAT As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtDispControlSampleSNF As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtMccPlantCode As common.UserControls.txtFinder
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtSNFPercentage As common.MyNumBox
    Friend WithEvents TxtSNFWeightage As common.MyNumBox
    Friend WithEvents TxtFatWeightage As common.MyNumBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents txtfatPercentage As common.MyNumBox
    Friend WithEvents lblPriceChart As common.Controls.MyLabel
    Friend WithEvents fndPriceChart As common.UserControls.txtFinder
    Friend WithEvents txtTransferPrice As common.MyNumBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents chkJobWork As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents lblDocumentAmt As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents btnShowInventory As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents fndReferenceNo As common.Controls.MyTextBox
    Friend WithEvents btnJE As RadButton
End Class

