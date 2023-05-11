<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMCCProcurementSetting
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnclose = New Telerik.WinControls.UI.RadMenuItem()
        Me.groupbox = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.dtpBlockingDBTPeriod = New common.Controls.MyDateTimePicker()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv2 = New common.UserControls.MyRadGridView()
        Me.chkStdFATSNFRate = New common.Controls.MyCheckBox()
        Me.ChkFarmerPaymentCycle = New common.Controls.MyCheckBox()
        Me.chkPriceChartGradeWise = New common.Controls.MyCheckBox()
        Me.chkItemMilkType = New common.Controls.MyCheckBox()
        Me.txtStockTolerance = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.ChkIncentiveDeductionMilksampleQC = New common.Controls.MyCheckBox()
        Me.GrpMilkTypeValidation = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.ChkValidationofMilkType = New common.Controls.MyCheckBox()
        Me.ChkDisplayTypeinMilkReceipt = New common.Controls.MyCheckBox()
        Me.ChkDisplayParameterinQualityCheck = New common.Controls.MyCheckBox()
        Me.ChkAllSampleParameter = New common.Controls.MyCheckBox()
        Me.chkAllowSkippingPrevDocOnPayProcess = New common.Controls.MyCheckBox()
        Me.chkAllowQcDateBeforeGateEntry = New common.Controls.MyCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.CmbSMSProvider = New common.Controls.MyComboBox()
        Me.TxtSendorID = New common.Controls.MyTextBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.TxtPWD = New common.Controls.MyTextBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.TxtUserName = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.chkDisAllowTankerDispatchTopalnt = New common.Controls.MyCheckBox()
        Me.DtpSendSMSTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.ChkSendSMS = New common.Controls.MyCheckBox()
        Me.txtItemDescForTankerDisp = New common.Controls.MyTextBox()
        Me.lblItemDescForTankerDisp = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.DtpTime = New common.Controls.MyDateTimePicker()
        Me.TxtInterval = New common.MyNumBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.DtpStartingDate = New common.Controls.MyDateTimePicker()
        Me.TxtMinKMDiff = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.TxttolerancePlus = New common.MyNumBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.Txttoleranceneg = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.TxtMilkWeight = New common.MyNumBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.TxtDaysForFssaiPopup = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtReceiptRange = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCorrectionFactor = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.chkControlSampleMandatory = New common.Controls.MyCheckBox()
        Me.TxtSampleRange = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblItemDesc = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.fndMCCItemCode = New common.UserControls.txtFinder()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupbox.SuspendLayout()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpBlockingDBTPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkStdFATSNFRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkFarmerPaymentCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPriceChartGradeWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStockTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkIncentiveDeductionMilksampleQC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrpMilkTypeValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpMilkTypeValidation.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkValidationofMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDisplayTypeinMilkReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDisplayParameterinQualityCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAllSampleParameter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowSkippingPrevDocOnPayProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllowQcDateBeforeGateEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbSMSProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSendorID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPWD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDisAllowTankerDispatchTopalnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpSendSMSTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkSendSMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtItemDescForTankerDisp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDescForTankerDisp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.DtpTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpStartingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMinKMDiff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxttolerancePlus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txttoleranceneg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMilkWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDaysForFssaiPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtReceiptRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorrectionFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkControlSampleMandatory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtSampleRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(7, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(68, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(922, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(60, 21)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnimport, Me.mnexport, Me.mnclose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'mnimport
        '
        Me.mnimport.Name = "mnimport"
        Me.mnimport.Text = "Import"
        '
        'mnexport
        '
        Me.mnexport.Name = "mnexport"
        Me.mnexport.Text = "Export"
        '
        'mnclose
        '
        Me.mnclose.Name = "mnclose"
        Me.mnclose.Text = "Close"
        '
        'groupbox
        '
        Me.groupbox.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.groupbox.Controls.Add(Me.MyLabel18)
        Me.groupbox.Controls.Add(Me.dtpBlockingDBTPeriod)
        Me.groupbox.Controls.Add(Me.RadGroupBox3)
        Me.groupbox.Controls.Add(Me.chkStdFATSNFRate)
        Me.groupbox.Controls.Add(Me.ChkFarmerPaymentCycle)
        Me.groupbox.Controls.Add(Me.chkPriceChartGradeWise)
        Me.groupbox.Controls.Add(Me.chkItemMilkType)
        Me.groupbox.Controls.Add(Me.txtStockTolerance)
        Me.groupbox.Controls.Add(Me.ChkIncentiveDeductionMilksampleQC)
        Me.groupbox.Controls.Add(Me.MyLabel17)
        Me.groupbox.Controls.Add(Me.GrpMilkTypeValidation)
        Me.groupbox.Controls.Add(Me.ChkDisplayTypeinMilkReceipt)
        Me.groupbox.Controls.Add(Me.ChkDisplayParameterinQualityCheck)
        Me.groupbox.Controls.Add(Me.ChkAllSampleParameter)
        Me.groupbox.Controls.Add(Me.chkAllowSkippingPrevDocOnPayProcess)
        Me.groupbox.Controls.Add(Me.chkAllowQcDateBeforeGateEntry)
        Me.groupbox.Controls.Add(Me.RadGroupBox2)
        Me.groupbox.Controls.Add(Me.chkDisAllowTankerDispatchTopalnt)
        Me.groupbox.Controls.Add(Me.DtpSendSMSTime)
        Me.groupbox.Controls.Add(Me.ChkSendSMS)
        Me.groupbox.Controls.Add(Me.txtItemDescForTankerDisp)
        Me.groupbox.Controls.Add(Me.lblItemDescForTankerDisp)
        Me.groupbox.Controls.Add(Me.RadGroupBox1)
        Me.groupbox.Controls.Add(Me.TxtMinKMDiff)
        Me.groupbox.Controls.Add(Me.MyLabel9)
        Me.groupbox.Controls.Add(Me.TxttolerancePlus)
        Me.groupbox.Controls.Add(Me.MyLabel8)
        Me.groupbox.Controls.Add(Me.Txttoleranceneg)
        Me.groupbox.Controls.Add(Me.MyLabel7)
        Me.groupbox.Controls.Add(Me.MyLabel6)
        Me.groupbox.Controls.Add(Me.TxtMilkWeight)
        Me.groupbox.Controls.Add(Me.MyLabel5)
        Me.groupbox.Controls.Add(Me.TxtDaysForFssaiPopup)
        Me.groupbox.Controls.Add(Me.MyLabel4)
        Me.groupbox.Controls.Add(Me.TxtReceiptRange)
        Me.groupbox.Controls.Add(Me.MyLabel3)
        Me.groupbox.Controls.Add(Me.txtCorrectionFactor)
        Me.groupbox.Controls.Add(Me.MyLabel2)
        Me.groupbox.Controls.Add(Me.chkControlSampleMandatory)
        Me.groupbox.Controls.Add(Me.TxtSampleRange)
        Me.groupbox.Controls.Add(Me.MyLabel1)
        Me.groupbox.Controls.Add(Me.lblItemDesc)
        Me.groupbox.Controls.Add(Me.lblMCCCode)
        Me.groupbox.Controls.Add(Me.fndMCCItemCode)
        Me.groupbox.HeaderText = ""
        Me.groupbox.Location = New System.Drawing.Point(3, 3)
        Me.groupbox.Name = "groupbox"
        Me.groupbox.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.groupbox.Size = New System.Drawing.Size(980, 578)
        Me.groupbox.TabIndex = 0
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(10, 474)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel18.TabIndex = 161
        Me.MyLabel18.Text = "Blocking DBT Period"
        '
        'dtpBlockingDBTPeriod
        '
        Me.dtpBlockingDBTPeriod.CalculationExpression = Nothing
        Me.dtpBlockingDBTPeriod.CustomFormat = "dd/MMM/yyyy"
        Me.dtpBlockingDBTPeriod.FieldCode = Nothing
        Me.dtpBlockingDBTPeriod.FieldDesc = Nothing
        Me.dtpBlockingDBTPeriod.FieldMaxLength = 0
        Me.dtpBlockingDBTPeriod.FieldName = Nothing
        Me.dtpBlockingDBTPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBlockingDBTPeriod.isCalculatedField = False
        Me.dtpBlockingDBTPeriod.IsSourceFromTable = False
        Me.dtpBlockingDBTPeriod.IsSourceFromValueList = False
        Me.dtpBlockingDBTPeriod.IsUnique = False
        Me.dtpBlockingDBTPeriod.Location = New System.Drawing.Point(127, 471)
        Me.dtpBlockingDBTPeriod.MendatroryField = True
        Me.dtpBlockingDBTPeriod.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpBlockingDBTPeriod.MyLinkLable1 = Me.MyLabel10
        Me.dtpBlockingDBTPeriod.MyLinkLable2 = Nothing
        Me.dtpBlockingDBTPeriod.Name = "dtpBlockingDBTPeriod"
        Me.dtpBlockingDBTPeriod.NullText = "01/01/1973"
        Me.dtpBlockingDBTPeriod.ReferenceFieldDesc = Nothing
        Me.dtpBlockingDBTPeriod.ReferenceFieldName = Nothing
        Me.dtpBlockingDBTPeriod.ReferenceTableName = Nothing
        Me.dtpBlockingDBTPeriod.ShowCheckBox = True
        Me.dtpBlockingDBTPeriod.Size = New System.Drawing.Size(126, 20)
        Me.dtpBlockingDBTPeriod.TabIndex = 160
        Me.dtpBlockingDBTPeriod.TabStop = False
        Me.dtpBlockingDBTPeriod.Text = "11/Jun/2014"
        Me.dtpBlockingDBTPeriod.Value = New Date(2014, 6, 11, 0, 0, 0, 0)
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(6, 22)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel10.TabIndex = 61
        Me.MyLabel10.Text = "Starting Date"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gv2)
        Me.RadGroupBox3.HeaderText = "Sub Standard Applicable % age on Incetive (Premium)"
        Me.RadGroupBox3.Location = New System.Drawing.Point(257, 433)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(704, 144)
        Me.RadGroupBox3.TabIndex = 159
        Me.RadGroupBox3.Text = "Sub Standard Applicable % age on Incetive (Premium)"
        '
        'gv2
        '
        Me.gv2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv2.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv2.ForeColor = System.Drawing.Color.Black
        Me.gv2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv2.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gv2.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv2.MasterTemplate.AllowDeleteRow = False
        Me.gv2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv2.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv2.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv2.Name = "gv2"
        Me.gv2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv2.ShowGroupPanel = False
        Me.gv2.ShowHeaderCellButtons = True
        Me.gv2.Size = New System.Drawing.Size(700, 124)
        Me.gv2.TabIndex = 156
        Me.gv2.TabStop = False
        '
        'chkStdFATSNFRate
        '
        Me.chkStdFATSNFRate.Location = New System.Drawing.Point(192, 105)
        Me.chkStdFATSNFRate.MyLinkLable1 = Nothing
        Me.chkStdFATSNFRate.MyLinkLable2 = Nothing
        Me.chkStdFATSNFRate.Name = "chkStdFATSNFRate"
        Me.chkStdFATSNFRate.Size = New System.Drawing.Size(372, 18)
        Me.chkStdFATSNFRate.TabIndex = 158
        Me.chkStdFATSNFRate.Tag1 = Nothing
        Me.chkStdFATSNFRate.Text = "Apply  FAT/SNF Rate = (Std Rate*FAT/SNFWeightage)/(FAT/SNF Ratio)"
        '
        'ChkFarmerPaymentCycle
        '
        Me.ChkFarmerPaymentCycle.Location = New System.Drawing.Point(6, 425)
        Me.ChkFarmerPaymentCycle.MyLinkLable1 = Nothing
        Me.ChkFarmerPaymentCycle.MyLinkLable2 = Nothing
        Me.ChkFarmerPaymentCycle.Name = "ChkFarmerPaymentCycle"
        Me.ChkFarmerPaymentCycle.Size = New System.Drawing.Size(131, 18)
        Me.ChkFarmerPaymentCycle.TabIndex = 157
        Me.ChkFarmerPaymentCycle.Tag1 = Nothing
        Me.ChkFarmerPaymentCycle.Text = "Farmer Payment Cycle"
        '
        'chkPriceChartGradeWise
        '
        Me.chkPriceChartGradeWise.Location = New System.Drawing.Point(6, 401)
        Me.chkPriceChartGradeWise.MyLinkLable1 = Nothing
        Me.chkPriceChartGradeWise.MyLinkLable2 = Nothing
        Me.chkPriceChartGradeWise.Name = "chkPriceChartGradeWise"
        Me.chkPriceChartGradeWise.Size = New System.Drawing.Size(146, 18)
        Me.chkPriceChartGradeWise.TabIndex = 156
        Me.chkPriceChartGradeWise.Tag1 = Nothing
        Me.chkPriceChartGradeWise.Text = "Is Price Chart Grade Wise"
        Me.chkPriceChartGradeWise.Visible = False
        '
        'chkItemMilkType
        '
        Me.chkItemMilkType.Location = New System.Drawing.Point(6, 377)
        Me.chkItemMilkType.MyLinkLable1 = Nothing
        Me.chkItemMilkType.MyLinkLable2 = Nothing
        Me.chkItemMilkType.Name = "chkItemMilkType"
        Me.chkItemMilkType.Size = New System.Drawing.Size(106, 18)
        Me.chkItemMilkType.TabIndex = 157
        Me.chkItemMilkType.Tag1 = Nothing
        Me.chkItemMilkType.Text = "IS Item Milk Type"
        '
        'txtStockTolerance
        '
        Me.txtStockTolerance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStockTolerance.CalculationExpression = Nothing
        Me.txtStockTolerance.DecimalPlaces = 2
        Me.txtStockTolerance.FieldCode = Nothing
        Me.txtStockTolerance.FieldDesc = Nothing
        Me.txtStockTolerance.FieldMaxLength = 0
        Me.txtStockTolerance.FieldName = Nothing
        Me.txtStockTolerance.isCalculatedField = False
        Me.txtStockTolerance.IsSourceFromTable = False
        Me.txtStockTolerance.IsSourceFromValueList = False
        Me.txtStockTolerance.IsUnique = False
        Me.txtStockTolerance.Location = New System.Drawing.Point(751, 104)
        Me.txtStockTolerance.MaxLength = 2
        Me.txtStockTolerance.MendatroryField = False
        Me.txtStockTolerance.MyLinkLable1 = Me.MyLabel17
        Me.txtStockTolerance.MyLinkLable2 = Nothing
        Me.txtStockTolerance.Name = "txtStockTolerance"
        Me.txtStockTolerance.ReferenceFieldDesc = Nothing
        Me.txtStockTolerance.ReferenceFieldName = Nothing
        Me.txtStockTolerance.ReferenceTableName = Nothing
        Me.txtStockTolerance.Size = New System.Drawing.Size(59, 20)
        Me.txtStockTolerance.TabIndex = 73
        Me.txtStockTolerance.Text = "0"
        Me.txtStockTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStockTolerance.Value = 0R
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel17.Location = New System.Drawing.Point(586, 105)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(143, 18)
        Me.MyLabel17.TabIndex = 72
        Me.MyLabel17.Text = "Allow Stock Tolerance(%)(-)"
        '
        'ChkIncentiveDeductionMilksampleQC
        '
        Me.ChkIncentiveDeductionMilksampleQC.Location = New System.Drawing.Point(7, 353)
        Me.ChkIncentiveDeductionMilksampleQC.MyLinkLable1 = Nothing
        Me.ChkIncentiveDeductionMilksampleQC.MyLinkLable2 = Nothing
        Me.ChkIncentiveDeductionMilksampleQC.Name = "ChkIncentiveDeductionMilksampleQC"
        Me.ChkIncentiveDeductionMilksampleQC.Size = New System.Drawing.Size(247, 18)
        Me.ChkIncentiveDeductionMilksampleQC.TabIndex = 155
        Me.ChkIncentiveDeductionMilksampleQC.Tag1 = Nothing
        Me.ChkIncentiveDeductionMilksampleQC.Text = "Add Incentive / Deduction in Milk Sample QC"
        '
        'GrpMilkTypeValidation
        '
        Me.GrpMilkTypeValidation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.GrpMilkTypeValidation.Controls.Add(Me.gv1)
        Me.GrpMilkTypeValidation.Controls.Add(Me.ChkValidationofMilkType)
        Me.GrpMilkTypeValidation.HeaderText = "Milk Type Validation"
        Me.GrpMilkTypeValidation.Location = New System.Drawing.Point(257, 283)
        Me.GrpMilkTypeValidation.Name = "GrpMilkTypeValidation"
        Me.GrpMilkTypeValidation.Size = New System.Drawing.Size(704, 144)
        Me.GrpMilkTypeValidation.TabIndex = 156
        Me.GrpMilkTypeValidation.Text = "Milk Type Validation"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(700, 124)
        Me.gv1.TabIndex = 156
        Me.gv1.TabStop = False
        '
        'ChkValidationofMilkType
        '
        Me.ChkValidationofMilkType.Location = New System.Drawing.Point(7, -2)
        Me.ChkValidationofMilkType.MyLinkLable1 = Nothing
        Me.ChkValidationofMilkType.MyLinkLable2 = Nothing
        Me.ChkValidationofMilkType.Name = "ChkValidationofMilkType"
        Me.ChkValidationofMilkType.Size = New System.Drawing.Size(212, 18)
        Me.ChkValidationofMilkType.TabIndex = 155
        Me.ChkValidationofMilkType.Tag1 = Nothing
        Me.ChkValidationofMilkType.Text = "Validation of Milk Type in Milk Sample"
        '
        'ChkDisplayTypeinMilkReceipt
        '
        Me.ChkDisplayTypeinMilkReceipt.Location = New System.Drawing.Point(7, 331)
        Me.ChkDisplayTypeinMilkReceipt.MyLinkLable1 = Nothing
        Me.ChkDisplayTypeinMilkReceipt.MyLinkLable2 = Nothing
        Me.ChkDisplayTypeinMilkReceipt.Name = "ChkDisplayTypeinMilkReceipt"
        Me.ChkDisplayTypeinMilkReceipt.Size = New System.Drawing.Size(160, 18)
        Me.ChkDisplayTypeinMilkReceipt.TabIndex = 154
        Me.ChkDisplayTypeinMilkReceipt.Tag1 = Nothing
        Me.ChkDisplayTypeinMilkReceipt.Text = "Display Type in Milk Receipt"
        '
        'ChkDisplayParameterinQualityCheck
        '
        Me.ChkDisplayParameterinQualityCheck.Location = New System.Drawing.Point(7, 307)
        Me.ChkDisplayParameterinQualityCheck.MyLinkLable1 = Nothing
        Me.ChkDisplayParameterinQualityCheck.MyLinkLable2 = Nothing
        Me.ChkDisplayParameterinQualityCheck.Name = "ChkDisplayParameterinQualityCheck"
        Me.ChkDisplayParameterinQualityCheck.Size = New System.Drawing.Size(249, 18)
        Me.ChkDisplayParameterinQualityCheck.TabIndex = 153
        Me.ChkDisplayParameterinQualityCheck.Tag1 = Nothing
        Me.ChkDisplayParameterinQualityCheck.Text = "Display All Quality Parameter in Quality Check"
        '
        'ChkAllSampleParameter
        '
        Me.ChkAllSampleParameter.Location = New System.Drawing.Point(7, 283)
        Me.ChkAllSampleParameter.MyLinkLable1 = Nothing
        Me.ChkAllSampleParameter.MyLinkLable2 = Nothing
        Me.ChkAllSampleParameter.Name = "ChkAllSampleParameter"
        Me.ChkAllSampleParameter.Size = New System.Drawing.Size(244, 18)
        Me.ChkAllSampleParameter.TabIndex = 152
        Me.ChkAllSampleParameter.Tag1 = Nothing
        Me.ChkAllSampleParameter.Text = "Display All Quality Parameter in Milk Sample."
        '
        'chkAllowSkippingPrevDocOnPayProcess
        '
        Me.chkAllowSkippingPrevDocOnPayProcess.Location = New System.Drawing.Point(611, 80)
        Me.chkAllowSkippingPrevDocOnPayProcess.MyLinkLable1 = Nothing
        Me.chkAllowSkippingPrevDocOnPayProcess.MyLinkLable2 = Nothing
        Me.chkAllowSkippingPrevDocOnPayProcess.Name = "chkAllowSkippingPrevDocOnPayProcess"
        Me.chkAllowSkippingPrevDocOnPayProcess.Size = New System.Drawing.Size(287, 18)
        Me.chkAllowSkippingPrevDocOnPayProcess.TabIndex = 152
        Me.chkAllowSkippingPrevDocOnPayProcess.Tag1 = Nothing
        Me.chkAllowSkippingPrevDocOnPayProcess.Text = "Allow Skipping Prev. Documents On Payment Process"
        '
        'chkAllowQcDateBeforeGateEntry
        '
        Me.chkAllowQcDateBeforeGateEntry.Location = New System.Drawing.Point(201, 203)
        Me.chkAllowQcDateBeforeGateEntry.MyLinkLable1 = Nothing
        Me.chkAllowQcDateBeforeGateEntry.MyLinkLable2 = Nothing
        Me.chkAllowQcDateBeforeGateEntry.Name = "chkAllowQcDateBeforeGateEntry"
        Me.chkAllowQcDateBeforeGateEntry.Size = New System.Drawing.Size(302, 18)
        Me.chkAllowQcDateBeforeGateEntry.TabIndex = 151
        Me.chkAllowQcDateBeforeGateEntry.Tag1 = Nothing
        Me.chkAllowQcDateBeforeGateEntry.Text = "Allow QC In/Out Date-Time Before Gate Entry Date Time"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox2.Controls.Add(Me.CmbSMSProvider)
        Me.RadGroupBox2.Controls.Add(Me.TxtSendorID)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox2.Controls.Add(Me.TxtPWD)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox2.Controls.Add(Me.TxtUserName)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox2.HeaderText = "Send SMS"
        Me.RadGroupBox2.Location = New System.Drawing.Point(4, 228)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(962, 49)
        Me.RadGroupBox2.TabIndex = 77
        Me.RadGroupBox2.Text = "Send SMS"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(33, 21)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel16.TabIndex = 155
        Me.MyLabel16.Text = "SMS Provider"
        '
        'CmbSMSProvider
        '
        Me.CmbSMSProvider.AutoCompleteDisplayMember = Nothing
        Me.CmbSMSProvider.AutoCompleteValueMember = Nothing
        Me.CmbSMSProvider.CalculationExpression = Nothing
        Me.CmbSMSProvider.DropDownAnimationEnabled = True
        Me.CmbSMSProvider.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbSMSProvider.FieldCode = Nothing
        Me.CmbSMSProvider.FieldDesc = Nothing
        Me.CmbSMSProvider.FieldMaxLength = 0
        Me.CmbSMSProvider.FieldName = Nothing
        Me.CmbSMSProvider.isCalculatedField = False
        Me.CmbSMSProvider.IsSourceFromTable = False
        Me.CmbSMSProvider.IsSourceFromValueList = False
        Me.CmbSMSProvider.IsUnique = False
        RadListDataItem1.Text = "BSWS"
        RadListDataItem2.Text = "Bulk SMS"
        Me.CmbSMSProvider.Items.Add(RadListDataItem1)
        Me.CmbSMSProvider.Items.Add(RadListDataItem2)
        Me.CmbSMSProvider.Location = New System.Drawing.Point(115, 19)
        Me.CmbSMSProvider.MendatroryField = True
        Me.CmbSMSProvider.MyLinkLable1 = Me.MyLabel16
        Me.CmbSMSProvider.MyLinkLable2 = Nothing
        Me.CmbSMSProvider.Name = "CmbSMSProvider"
        Me.CmbSMSProvider.ReferenceFieldDesc = Nothing
        Me.CmbSMSProvider.ReferenceFieldName = Nothing
        Me.CmbSMSProvider.ReferenceTableName = Nothing
        Me.CmbSMSProvider.Size = New System.Drawing.Size(154, 20)
        Me.CmbSMSProvider.TabIndex = 154
        '
        'TxtSendorID
        '
        Me.TxtSendorID.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtSendorID.CalculationExpression = Nothing
        Me.TxtSendorID.FieldCode = Nothing
        Me.TxtSendorID.FieldDesc = Nothing
        Me.TxtSendorID.FieldMaxLength = 0
        Me.TxtSendorID.FieldName = Nothing
        Me.TxtSendorID.isCalculatedField = False
        Me.TxtSendorID.IsSourceFromTable = False
        Me.TxtSendorID.IsSourceFromValueList = False
        Me.TxtSendorID.IsUnique = False
        Me.TxtSendorID.Location = New System.Drawing.Point(347, 19)
        Me.TxtSendorID.MendatroryField = False
        Me.TxtSendorID.MyLinkLable1 = Me.MyLabel15
        Me.TxtSendorID.MyLinkLable2 = Nothing
        Me.TxtSendorID.Name = "TxtSendorID"
        Me.TxtSendorID.ReferenceFieldDesc = Nothing
        Me.TxtSendorID.ReferenceFieldName = Nothing
        Me.TxtSendorID.ReferenceTableName = Nothing
        Me.TxtSendorID.Size = New System.Drawing.Size(144, 20)
        Me.TxtSendorID.TabIndex = 152
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(275, 21)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel15.TabIndex = 153
        Me.MyLabel15.Text = "Sendor ID"
        '
        'TxtPWD
        '
        Me.TxtPWD.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtPWD.CalculationExpression = Nothing
        Me.TxtPWD.FieldCode = Nothing
        Me.TxtPWD.FieldDesc = Nothing
        Me.TxtPWD.FieldMaxLength = 0
        Me.TxtPWD.FieldName = Nothing
        Me.TxtPWD.isCalculatedField = False
        Me.TxtPWD.IsSourceFromTable = False
        Me.TxtPWD.IsSourceFromValueList = False
        Me.TxtPWD.IsUnique = False
        Me.TxtPWD.Location = New System.Drawing.Point(824, 19)
        Me.TxtPWD.MendatroryField = False
        Me.TxtPWD.MyLinkLable1 = Me.MyLabel14
        Me.TxtPWD.MyLinkLable2 = Nothing
        Me.TxtPWD.Name = "TxtPWD"
        Me.TxtPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPWD.ReferenceFieldDesc = Nothing
        Me.TxtPWD.ReferenceFieldName = Nothing
        Me.TxtPWD.ReferenceTableName = Nothing
        Me.TxtPWD.Size = New System.Drawing.Size(133, 20)
        Me.TxtPWD.TabIndex = 152
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(776, 21)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel14.TabIndex = 153
        Me.MyLabel14.Text = "PWD"
        '
        'TxtUserName
        '
        Me.TxtUserName.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtUserName.CalculationExpression = Nothing
        Me.TxtUserName.FieldCode = Nothing
        Me.TxtUserName.FieldDesc = Nothing
        Me.TxtUserName.FieldMaxLength = 0
        Me.TxtUserName.FieldName = Nothing
        Me.TxtUserName.isCalculatedField = False
        Me.TxtUserName.IsSourceFromTable = False
        Me.TxtUserName.IsSourceFromValueList = False
        Me.TxtUserName.IsUnique = False
        Me.TxtUserName.Location = New System.Drawing.Point(614, 19)
        Me.TxtUserName.MendatroryField = False
        Me.TxtUserName.MyLinkLable1 = Me.MyLabel13
        Me.TxtUserName.MyLinkLable2 = Nothing
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.ReferenceFieldDesc = Nothing
        Me.TxtUserName.ReferenceFieldName = Nothing
        Me.TxtUserName.ReferenceTableName = Nothing
        Me.TxtUserName.Size = New System.Drawing.Size(144, 20)
        Me.TxtUserName.TabIndex = 150
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(542, 21)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel13.TabIndex = 151
        Me.MyLabel13.Text = "User Name"
        '
        'chkDisAllowTankerDispatchTopalnt
        '
        Me.chkDisAllowTankerDispatchTopalnt.Location = New System.Drawing.Point(7, 204)
        Me.chkDisAllowTankerDispatchTopalnt.MyLinkLable1 = Nothing
        Me.chkDisAllowTankerDispatchTopalnt.MyLinkLable2 = Nothing
        Me.chkDisAllowTankerDispatchTopalnt.Name = "chkDisAllowTankerDispatchTopalnt"
        Me.chkDisAllowTankerDispatchTopalnt.Size = New System.Drawing.Size(189, 18)
        Me.chkDisAllowTankerDispatchTopalnt.TabIndex = 150
        Me.chkDisAllowTankerDispatchTopalnt.Tag1 = Nothing
        Me.chkDisAllowTankerDispatchTopalnt.Text = "Disallow Tanker Dispatch To Plant"
        '
        'DtpSendSMSTime
        '
        Me.DtpSendSMSTime.CalculationExpression = Nothing
        Me.DtpSendSMSTime.CustomFormat = "hh:mm:ss tt"
        Me.DtpSendSMSTime.FieldCode = Nothing
        Me.DtpSendSMSTime.FieldDesc = Nothing
        Me.DtpSendSMSTime.FieldMaxLength = 0
        Me.DtpSendSMSTime.FieldName = Nothing
        Me.DtpSendSMSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpSendSMSTime.isCalculatedField = False
        Me.DtpSendSMSTime.IsSourceFromTable = False
        Me.DtpSendSMSTime.IsSourceFromValueList = False
        Me.DtpSendSMSTime.IsUnique = False
        Me.DtpSendSMSTime.Location = New System.Drawing.Point(753, 180)
        Me.DtpSendSMSTime.MendatroryField = True
        Me.DtpSendSMSTime.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.DtpSendSMSTime.MyLinkLable1 = Me.MyLabel12
        Me.DtpSendSMSTime.MyLinkLable2 = Nothing
        Me.DtpSendSMSTime.Name = "DtpSendSMSTime"
        Me.DtpSendSMSTime.NullText = "01/01/1973"
        Me.DtpSendSMSTime.ReferenceFieldDesc = Nothing
        Me.DtpSendSMSTime.ReferenceFieldName = Nothing
        Me.DtpSendSMSTime.ReferenceTableName = Nothing
        Me.DtpSendSMSTime.Size = New System.Drawing.Size(145, 20)
        Me.DtpSendSMSTime.TabIndex = 66
        Me.DtpSendSMSTime.TabStop = False
        Me.DtpSendSMSTime.Text = "02:13:57 PM"
        Me.DtpSendSMSTime.Value = New Date(2014, 6, 11, 14, 13, 57, 495)
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(582, 22)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel12.TabIndex = 66
        Me.MyLabel12.Text = "Starting Time"
        '
        'ChkSendSMS
        '
        Me.ChkSendSMS.Location = New System.Drawing.Point(676, 181)
        Me.ChkSendSMS.MyLinkLable1 = Nothing
        Me.ChkSendSMS.MyLinkLable2 = Nothing
        Me.ChkSendSMS.Name = "ChkSendSMS"
        Me.ChkSendSMS.Size = New System.Drawing.Size(71, 18)
        Me.ChkSendSMS.TabIndex = 61
        Me.ChkSendSMS.Tag1 = Nothing
        Me.ChkSendSMS.Text = "Send SMS"
        '
        'txtItemDescForTankerDisp
        '
        Me.txtItemDescForTankerDisp.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtItemDescForTankerDisp.CalculationExpression = Nothing
        Me.txtItemDescForTankerDisp.FieldCode = Nothing
        Me.txtItemDescForTankerDisp.FieldDesc = Nothing
        Me.txtItemDescForTankerDisp.FieldMaxLength = 0
        Me.txtItemDescForTankerDisp.FieldName = Nothing
        Me.txtItemDescForTankerDisp.isCalculatedField = False
        Me.txtItemDescForTankerDisp.IsSourceFromTable = False
        Me.txtItemDescForTankerDisp.IsSourceFromValueList = False
        Me.txtItemDescForTankerDisp.IsUnique = False
        Me.txtItemDescForTankerDisp.Location = New System.Drawing.Point(201, 180)
        Me.txtItemDescForTankerDisp.MendatroryField = False
        Me.txtItemDescForTankerDisp.MyLinkLable1 = Me.lblItemDescForTankerDisp
        Me.txtItemDescForTankerDisp.MyLinkLable2 = Nothing
        Me.txtItemDescForTankerDisp.Name = "txtItemDescForTankerDisp"
        Me.txtItemDescForTankerDisp.ReferenceFieldDesc = Nothing
        Me.txtItemDescForTankerDisp.ReferenceFieldName = Nothing
        Me.txtItemDescForTankerDisp.ReferenceTableName = Nothing
        Me.txtItemDescForTankerDisp.Size = New System.Drawing.Size(460, 20)
        Me.txtItemDescForTankerDisp.TabIndex = 148
        '
        'lblItemDescForTankerDisp
        '
        Me.lblItemDescForTankerDisp.FieldName = Nothing
        Me.lblItemDescForTankerDisp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemDescForTankerDisp.Location = New System.Drawing.Point(7, 182)
        Me.lblItemDescForTankerDisp.Name = "lblItemDescForTankerDisp"
        Me.lblItemDescForTankerDisp.Size = New System.Drawing.Size(189, 16)
        Me.lblItemDescForTankerDisp.TabIndex = 149
        Me.lblItemDescForTankerDisp.Text = "Item Desc For Tanker Dispatch Print"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.DtpTime)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.TxtInterval)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.DtpStartingDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.HeaderText = "Invoice Schedule Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(4, 118)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(962, 58)
        Me.RadGroupBox1.TabIndex = 76
        Me.RadGroupBox1.Text = "Invoice Schedule Details"
        '
        'DtpTime
        '
        Me.DtpTime.CalculationExpression = Nothing
        Me.DtpTime.CustomFormat = "hh:mm:ss tt"
        Me.DtpTime.FieldCode = Nothing
        Me.DtpTime.FieldDesc = Nothing
        Me.DtpTime.FieldMaxLength = 0
        Me.DtpTime.FieldName = Nothing
        Me.DtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpTime.isCalculatedField = False
        Me.DtpTime.IsSourceFromTable = False
        Me.DtpTime.IsSourceFromValueList = False
        Me.DtpTime.IsUnique = False
        Me.DtpTime.Location = New System.Drawing.Point(747, 22)
        Me.DtpTime.MendatroryField = True
        Me.DtpTime.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.DtpTime.MyLinkLable1 = Me.MyLabel12
        Me.DtpTime.MyLinkLable2 = Nothing
        Me.DtpTime.Name = "DtpTime"
        Me.DtpTime.NullText = "01/01/1973"
        Me.DtpTime.ReferenceFieldDesc = Nothing
        Me.DtpTime.ReferenceFieldName = Nothing
        Me.DtpTime.ReferenceTableName = Nothing
        Me.DtpTime.Size = New System.Drawing.Size(145, 20)
        Me.DtpTime.TabIndex = 65
        Me.DtpTime.TabStop = False
        Me.DtpTime.Text = "02:13:57 PM"
        Me.DtpTime.Value = New Date(2014, 6, 11, 14, 13, 57, 495)
        '
        'TxtInterval
        '
        Me.TxtInterval.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtInterval.CalculationExpression = Nothing
        Me.TxtInterval.DecimalPlaces = 2
        Me.TxtInterval.FieldCode = Nothing
        Me.TxtInterval.FieldDesc = Nothing
        Me.TxtInterval.FieldMaxLength = 0
        Me.TxtInterval.FieldName = Nothing
        Me.TxtInterval.isCalculatedField = False
        Me.TxtInterval.IsSourceFromTable = False
        Me.TxtInterval.IsSourceFromValueList = False
        Me.TxtInterval.IsUnique = False
        Me.TxtInterval.Location = New System.Drawing.Point(471, 20)
        Me.TxtInterval.MaxLength = 5
        Me.TxtInterval.MendatroryField = False
        Me.TxtInterval.MyLinkLable1 = Me.MyLabel11
        Me.TxtInterval.MyLinkLable2 = Nothing
        Me.TxtInterval.Name = "TxtInterval"
        Me.TxtInterval.ReferenceFieldDesc = Nothing
        Me.TxtInterval.ReferenceFieldName = Nothing
        Me.TxtInterval.ReferenceTableName = Nothing
        Me.TxtInterval.Size = New System.Drawing.Size(107, 20)
        Me.TxtInterval.TabIndex = 64
        Me.TxtInterval.Text = "0"
        Me.TxtInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtInterval.Value = 0R
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel11.Location = New System.Drawing.Point(348, 21)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(74, 18)
        Me.MyLabel11.TabIndex = 63
        Me.MyLabel11.Text = "Interval(Days)"
        '
        'DtpStartingDate
        '
        Me.DtpStartingDate.CalculationExpression = Nothing
        Me.DtpStartingDate.CustomFormat = "dd-MMM-yyyy"
        Me.DtpStartingDate.FieldCode = Nothing
        Me.DtpStartingDate.FieldDesc = Nothing
        Me.DtpStartingDate.FieldMaxLength = 0
        Me.DtpStartingDate.FieldName = Nothing
        Me.DtpStartingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpStartingDate.isCalculatedField = False
        Me.DtpStartingDate.IsSourceFromTable = False
        Me.DtpStartingDate.IsSourceFromValueList = False
        Me.DtpStartingDate.IsUnique = False
        Me.DtpStartingDate.Location = New System.Drawing.Point(189, 20)
        Me.DtpStartingDate.MendatroryField = True
        Me.DtpStartingDate.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.DtpStartingDate.MyLinkLable1 = Me.MyLabel10
        Me.DtpStartingDate.MyLinkLable2 = Nothing
        Me.DtpStartingDate.Name = "DtpStartingDate"
        Me.DtpStartingDate.NullText = "01/01/1973"
        Me.DtpStartingDate.ReferenceFieldDesc = Nothing
        Me.DtpStartingDate.ReferenceFieldName = Nothing
        Me.DtpStartingDate.ReferenceTableName = Nothing
        Me.DtpStartingDate.Size = New System.Drawing.Size(154, 20)
        Me.DtpStartingDate.TabIndex = 60
        Me.DtpStartingDate.TabStop = False
        Me.DtpStartingDate.Text = "11-Jun-2014"
        Me.DtpStartingDate.Value = New Date(2014, 6, 11, 14, 13, 57, 495)
        '
        'TxtMinKMDiff
        '
        Me.TxtMinKMDiff.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtMinKMDiff.CalculationExpression = Nothing
        Me.TxtMinKMDiff.DecimalPlaces = 2
        Me.TxtMinKMDiff.FieldCode = Nothing
        Me.TxtMinKMDiff.FieldDesc = Nothing
        Me.TxtMinKMDiff.FieldMaxLength = 0
        Me.TxtMinKMDiff.FieldName = Nothing
        Me.TxtMinKMDiff.isCalculatedField = False
        Me.TxtMinKMDiff.IsSourceFromTable = False
        Me.TxtMinKMDiff.IsSourceFromValueList = False
        Me.TxtMinKMDiff.IsUnique = False
        Me.TxtMinKMDiff.Location = New System.Drawing.Point(192, 80)
        Me.TxtMinKMDiff.MaxLength = 2
        Me.TxtMinKMDiff.MendatroryField = False
        Me.TxtMinKMDiff.MyLinkLable1 = Me.MyLabel9
        Me.TxtMinKMDiff.MyLinkLable2 = Nothing
        Me.TxtMinKMDiff.Name = "TxtMinKMDiff"
        Me.TxtMinKMDiff.ReferenceFieldDesc = Nothing
        Me.TxtMinKMDiff.ReferenceFieldName = Nothing
        Me.TxtMinKMDiff.ReferenceTableName = Nothing
        Me.TxtMinKMDiff.Size = New System.Drawing.Size(154, 20)
        Me.TxtMinKMDiff.TabIndex = 75
        Me.TxtMinKMDiff.Text = "0"
        Me.TxtMinKMDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMinKMDiff.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.AutoSize = False
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(7, 81)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(188, 49)
        Me.MyLabel9.TabIndex = 74
        Me.MyLabel9.Text = "Min. Kilometer(KM.) Difference For Reason in Milk Shift End"
        '
        'TxttolerancePlus
        '
        Me.TxttolerancePlus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxttolerancePlus.CalculationExpression = Nothing
        Me.TxttolerancePlus.DecimalPlaces = 2
        Me.TxttolerancePlus.FieldCode = Nothing
        Me.TxttolerancePlus.FieldDesc = Nothing
        Me.TxttolerancePlus.FieldMaxLength = 0
        Me.TxttolerancePlus.FieldName = Nothing
        Me.TxttolerancePlus.isCalculatedField = False
        Me.TxttolerancePlus.IsSourceFromTable = False
        Me.TxttolerancePlus.IsSourceFromValueList = False
        Me.TxttolerancePlus.IsUnique = False
        Me.TxttolerancePlus.Location = New System.Drawing.Point(919, 56)
        Me.TxttolerancePlus.MaxLength = 2
        Me.TxttolerancePlus.MendatroryField = False
        Me.TxttolerancePlus.MyLinkLable1 = Me.MyLabel8
        Me.TxttolerancePlus.MyLinkLable2 = Nothing
        Me.TxttolerancePlus.Name = "TxttolerancePlus"
        Me.TxttolerancePlus.ReferenceFieldDesc = Nothing
        Me.TxttolerancePlus.ReferenceFieldName = Nothing
        Me.TxttolerancePlus.ReferenceTableName = Nothing
        Me.TxttolerancePlus.Size = New System.Drawing.Size(47, 20)
        Me.TxttolerancePlus.TabIndex = 73
        Me.TxttolerancePlus.Text = "0"
        Me.TxttolerancePlus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxttolerancePlus.Value = 0R
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(816, 57)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(92, 18)
        Me.MyLabel8.TabIndex = 72
        Me.MyLabel8.Text = "Tolerance(%)( + )"
        '
        'Txttoleranceneg
        '
        Me.Txttoleranceneg.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.Txttoleranceneg.CalculationExpression = Nothing
        Me.Txttoleranceneg.DecimalPlaces = 2
        Me.Txttoleranceneg.FieldCode = Nothing
        Me.Txttoleranceneg.FieldDesc = Nothing
        Me.Txttoleranceneg.FieldMaxLength = 0
        Me.Txttoleranceneg.FieldName = Nothing
        Me.Txttoleranceneg.isCalculatedField = False
        Me.Txttoleranceneg.IsSourceFromTable = False
        Me.Txttoleranceneg.IsSourceFromValueList = False
        Me.Txttoleranceneg.IsUnique = False
        Me.Txttoleranceneg.Location = New System.Drawing.Point(751, 56)
        Me.Txttoleranceneg.MaxLength = 2
        Me.Txttoleranceneg.MendatroryField = False
        Me.Txttoleranceneg.MyLinkLable1 = Me.MyLabel7
        Me.Txttoleranceneg.MyLinkLable2 = Nothing
        Me.Txttoleranceneg.Name = "Txttoleranceneg"
        Me.Txttoleranceneg.ReferenceFieldDesc = Nothing
        Me.Txttoleranceneg.ReferenceFieldName = Nothing
        Me.Txttoleranceneg.ReferenceTableName = Nothing
        Me.Txttoleranceneg.Size = New System.Drawing.Size(59, 20)
        Me.Txttoleranceneg.TabIndex = 71
        Me.Txttoleranceneg.Text = "0"
        Me.Txttoleranceneg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Txttoleranceneg.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(586, 57)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(152, 18)
        Me.MyLabel7.TabIndex = 70
        Me.MyLabel7.Text = "Milk Weight Tolerance(%)( - )"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(816, 32)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel6.TabIndex = 68
        Me.MyLabel6.Text = "Milk Weight"
        '
        'TxtMilkWeight
        '
        Me.TxtMilkWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtMilkWeight.CalculationExpression = Nothing
        Me.TxtMilkWeight.DecimalPlaces = 2
        Me.TxtMilkWeight.FieldCode = Nothing
        Me.TxtMilkWeight.FieldDesc = Nothing
        Me.TxtMilkWeight.FieldMaxLength = 0
        Me.TxtMilkWeight.FieldName = Nothing
        Me.TxtMilkWeight.isCalculatedField = False
        Me.TxtMilkWeight.IsSourceFromTable = False
        Me.TxtMilkWeight.IsSourceFromValueList = False
        Me.TxtMilkWeight.IsUnique = False
        Me.TxtMilkWeight.Location = New System.Drawing.Point(751, 31)
        Me.TxtMilkWeight.MaxLength = 5
        Me.TxtMilkWeight.MendatroryField = False
        Me.TxtMilkWeight.MyLinkLable1 = Me.MyLabel5
        Me.TxtMilkWeight.MyLinkLable2 = Nothing
        Me.TxtMilkWeight.Name = "TxtMilkWeight"
        Me.TxtMilkWeight.ReferenceFieldDesc = Nothing
        Me.TxtMilkWeight.ReferenceFieldName = Nothing
        Me.TxtMilkWeight.ReferenceTableName = Nothing
        Me.TxtMilkWeight.Size = New System.Drawing.Size(59, 20)
        Me.TxtMilkWeight.TabIndex = 69
        Me.TxtMilkWeight.Text = "0"
        Me.TxtMilkWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMilkWeight.Value = 0R
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(703, 32)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(35, 18)
        Me.MyLabel5.TabIndex = 67
        Me.MyLabel5.Text = "1 Can"
        '
        'TxtDaysForFssaiPopup
        '
        Me.TxtDaysForFssaiPopup.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtDaysForFssaiPopup.CalculationExpression = Nothing
        Me.TxtDaysForFssaiPopup.DecimalPlaces = 2
        Me.TxtDaysForFssaiPopup.FieldCode = Nothing
        Me.TxtDaysForFssaiPopup.FieldDesc = Nothing
        Me.TxtDaysForFssaiPopup.FieldMaxLength = 0
        Me.TxtDaysForFssaiPopup.FieldName = Nothing
        Me.TxtDaysForFssaiPopup.isCalculatedField = False
        Me.TxtDaysForFssaiPopup.IsSourceFromTable = False
        Me.TxtDaysForFssaiPopup.IsSourceFromValueList = False
        Me.TxtDaysForFssaiPopup.IsUnique = False
        Me.TxtDaysForFssaiPopup.Location = New System.Drawing.Point(475, 56)
        Me.TxtDaysForFssaiPopup.MaxLength = 5
        Me.TxtDaysForFssaiPopup.MendatroryField = False
        Me.TxtDaysForFssaiPopup.MyLinkLable1 = Me.MyLabel4
        Me.TxtDaysForFssaiPopup.MyLinkLable2 = Nothing
        Me.TxtDaysForFssaiPopup.Name = "TxtDaysForFssaiPopup"
        Me.TxtDaysForFssaiPopup.ReferenceFieldDesc = Nothing
        Me.TxtDaysForFssaiPopup.ReferenceFieldName = Nothing
        Me.TxtDaysForFssaiPopup.ReferenceTableName = Nothing
        Me.TxtDaysForFssaiPopup.Size = New System.Drawing.Size(107, 20)
        Me.TxtDaysForFssaiPopup.TabIndex = 66
        Me.TxtDaysForFssaiPopup.Text = "0"
        Me.TxtDaysForFssaiPopup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtDaysForFssaiPopup.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(351, 57)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(118, 18)
        Me.MyLabel4.TabIndex = 65
        Me.MyLabel4.Text = "Days For FSSAI PopUp"
        '
        'TxtReceiptRange
        '
        Me.TxtReceiptRange.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtReceiptRange.CalculationExpression = Nothing
        Me.TxtReceiptRange.DecimalPlaces = 0
        Me.TxtReceiptRange.FieldCode = Nothing
        Me.TxtReceiptRange.FieldDesc = Nothing
        Me.TxtReceiptRange.FieldMaxLength = 0
        Me.TxtReceiptRange.FieldName = Nothing
        Me.TxtReceiptRange.isCalculatedField = False
        Me.TxtReceiptRange.IsSourceFromTable = False
        Me.TxtReceiptRange.IsSourceFromValueList = False
        Me.TxtReceiptRange.IsUnique = False
        Me.TxtReceiptRange.Location = New System.Drawing.Point(475, 31)
        Me.TxtReceiptRange.MaxLength = 5
        Me.TxtReceiptRange.MendatroryField = False
        Me.TxtReceiptRange.MyLinkLable1 = Nothing
        Me.TxtReceiptRange.MyLinkLable2 = Nothing
        Me.TxtReceiptRange.Name = "TxtReceiptRange"
        Me.TxtReceiptRange.ReferenceFieldDesc = Nothing
        Me.TxtReceiptRange.ReferenceFieldName = Nothing
        Me.TxtReceiptRange.ReferenceTableName = Nothing
        Me.TxtReceiptRange.Size = New System.Drawing.Size(107, 20)
        Me.TxtReceiptRange.TabIndex = 64
        Me.TxtReceiptRange.Text = "0"
        Me.TxtReceiptRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtReceiptRange.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(351, 32)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel3.TabIndex = 63
        Me.MyLabel3.Text = "Receipt Range"
        '
        'txtCorrectionFactor
        '
        Me.txtCorrectionFactor.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCorrectionFactor.CalculationExpression = Nothing
        Me.txtCorrectionFactor.DecimalPlaces = 2
        Me.txtCorrectionFactor.FieldCode = Nothing
        Me.txtCorrectionFactor.FieldDesc = Nothing
        Me.txtCorrectionFactor.FieldMaxLength = 0
        Me.txtCorrectionFactor.FieldName = Nothing
        Me.txtCorrectionFactor.isCalculatedField = False
        Me.txtCorrectionFactor.IsSourceFromTable = False
        Me.txtCorrectionFactor.IsSourceFromValueList = False
        Me.txtCorrectionFactor.IsUnique = False
        Me.txtCorrectionFactor.Location = New System.Drawing.Point(192, 56)
        Me.txtCorrectionFactor.MaxLength = 5
        Me.txtCorrectionFactor.MendatroryField = False
        Me.txtCorrectionFactor.MyLinkLable1 = Me.MyLabel2
        Me.txtCorrectionFactor.MyLinkLable2 = Nothing
        Me.txtCorrectionFactor.Name = "txtCorrectionFactor"
        Me.txtCorrectionFactor.ReferenceFieldDesc = Nothing
        Me.txtCorrectionFactor.ReferenceFieldName = Nothing
        Me.txtCorrectionFactor.ReferenceTableName = Nothing
        Me.txtCorrectionFactor.Size = New System.Drawing.Size(154, 20)
        Me.txtCorrectionFactor.TabIndex = 62
        Me.txtCorrectionFactor.Text = "0"
        Me.txtCorrectionFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCorrectionFactor.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(7, 57)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(132, 18)
        Me.MyLabel2.TabIndex = 61
        Me.MyLabel2.Text = "Default Correction Factor"
        '
        'chkControlSampleMandatory
        '
        Me.chkControlSampleMandatory.Location = New System.Drawing.Point(352, 81)
        Me.chkControlSampleMandatory.MyLinkLable1 = Nothing
        Me.chkControlSampleMandatory.MyLinkLable2 = Nothing
        Me.chkControlSampleMandatory.Name = "chkControlSampleMandatory"
        Me.chkControlSampleMandatory.Size = New System.Drawing.Size(253, 18)
        Me.chkControlSampleMandatory.TabIndex = 60
        Me.chkControlSampleMandatory.Tag1 = Nothing
        Me.chkControlSampleMandatory.Text = "Is Control Sample Mandatory at MCC Dispatch"
        '
        'TxtSampleRange
        '
        Me.TxtSampleRange.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtSampleRange.CalculationExpression = Nothing
        Me.TxtSampleRange.DecimalPlaces = 0
        Me.TxtSampleRange.FieldCode = Nothing
        Me.TxtSampleRange.FieldDesc = Nothing
        Me.TxtSampleRange.FieldMaxLength = 0
        Me.TxtSampleRange.FieldName = Nothing
        Me.TxtSampleRange.isCalculatedField = False
        Me.TxtSampleRange.IsSourceFromTable = False
        Me.TxtSampleRange.IsSourceFromValueList = False
        Me.TxtSampleRange.IsUnique = False
        Me.TxtSampleRange.Location = New System.Drawing.Point(192, 31)
        Me.TxtSampleRange.MaxLength = 5
        Me.TxtSampleRange.MendatroryField = False
        Me.TxtSampleRange.MyLinkLable1 = Nothing
        Me.TxtSampleRange.MyLinkLable2 = Nothing
        Me.TxtSampleRange.Name = "TxtSampleRange"
        Me.TxtSampleRange.ReferenceFieldDesc = Nothing
        Me.TxtSampleRange.ReferenceFieldName = Nothing
        Me.TxtSampleRange.ReferenceTableName = Nothing
        Me.TxtSampleRange.Size = New System.Drawing.Size(154, 20)
        Me.TxtSampleRange.TabIndex = 59
        Me.TxtSampleRange.Text = "0"
        Me.TxtSampleRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtSampleRange.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(7, 32)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel1.TabIndex = 16
        Me.MyLabel1.Text = "Sample Range"
        '
        'lblItemDesc
        '
        Me.lblItemDesc.AutoSize = False
        Me.lblItemDesc.BorderVisible = True
        Me.lblItemDesc.FieldName = Nothing
        Me.lblItemDesc.Location = New System.Drawing.Point(352, 7)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.Size = New System.Drawing.Size(614, 19)
        Me.lblItemDesc.TabIndex = 37
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMCCCode.Location = New System.Drawing.Point(7, 7)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(193, 18)
        Me.lblMCCCode.TabIndex = 15
        Me.lblMCCCode.Text = "Default Milk Item For Procurement"
        '
        'fndMCCItemCode
        '
        Me.fndMCCItemCode.CalculationExpression = Nothing
        Me.fndMCCItemCode.FieldCode = Nothing
        Me.fndMCCItemCode.FieldDesc = Nothing
        Me.fndMCCItemCode.FieldMaxLength = 0
        Me.fndMCCItemCode.FieldName = Nothing
        Me.fndMCCItemCode.isCalculatedField = False
        Me.fndMCCItemCode.IsSourceFromTable = False
        Me.fndMCCItemCode.IsSourceFromValueList = False
        Me.fndMCCItemCode.IsUnique = False
        Me.fndMCCItemCode.Location = New System.Drawing.Point(192, 7)
        Me.fndMCCItemCode.MendatroryField = False
        Me.fndMCCItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMCCItemCode.MyLinkLable1 = Me.lblMCCCode
        Me.fndMCCItemCode.MyLinkLable2 = Nothing
        Me.fndMCCItemCode.MyReadOnly = False
        Me.fndMCCItemCode.MyShowMasterFormButton = False
        Me.fndMCCItemCode.Name = "fndMCCItemCode"
        Me.fndMCCItemCode.ReferenceFieldDesc = Nothing
        Me.fndMCCItemCode.ReferenceFieldName = Nothing
        Me.fndMCCItemCode.ReferenceTableName = Nothing
        Me.fndMCCItemCode.Size = New System.Drawing.Size(154, 19)
        Me.fndMCCItemCode.TabIndex = 14
        Me.fndMCCItemCode.Value = ""
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.groupbox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(987, 633)
        Me.SplitContainer1.SplitterDistance = 596
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(987, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'frmMCCProcurementSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(987, 653)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmMCCProcurementSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MCC Procurement Setting"
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupbox.ResumeLayout(False)
        Me.groupbox.PerformLayout()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpBlockingDBTPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gv2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkStdFATSNFRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkFarmerPaymentCycle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPriceChartGradeWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStockTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkIncentiveDeductionMilksampleQC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrpMilkTypeValidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpMilkTypeValidation.ResumeLayout(False)
        Me.GrpMilkTypeValidation.PerformLayout()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkValidationofMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDisplayTypeinMilkReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDisplayParameterinQualityCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAllSampleParameter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowSkippingPrevDocOnPayProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllowQcDateBeforeGateEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbSMSProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSendorID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPWD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDisAllowTankerDispatchTopalnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpSendSMSTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkSendSMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtItemDescForTankerDisp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDescForTankerDisp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.DtpTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpStartingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMinKMDiff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxttolerancePlus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txttoleranceneg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMilkWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDaysForFssaiPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtReceiptRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorrectionFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkControlSampleMandatory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtSampleRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnclose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents groupbox As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents fndMCCItemCode As common.UserControls.txtFinder
    Friend WithEvents lblItemDesc As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtSampleRange As common.MyNumBox
    Friend WithEvents chkControlSampleMandatory As common.Controls.MyCheckBox
    Friend WithEvents txtCorrectionFactor As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtReceiptRange As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtDaysForFssaiPopup As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents TxtMilkWeight As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxttolerancePlus As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents Txttoleranceneg As common.MyNumBox
    Friend WithEvents TxtMinKMDiff As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents DtpStartingDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents TxtInterval As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents DtpTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtItemDescForTankerDisp As common.Controls.MyTextBox
    Friend WithEvents lblItemDescForTankerDisp As common.Controls.MyLabel
    Friend WithEvents ChkSendSMS As common.Controls.MyCheckBox
    Friend WithEvents DtpSendSMSTime As common.Controls.MyDateTimePicker
    Friend WithEvents chkDisAllowTankerDispatchTopalnt As common.Controls.MyCheckBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents TxtPWD As common.Controls.MyTextBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents TxtUserName As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents chkAllowQcDateBeforeGateEntry As common.Controls.MyCheckBox
    Friend WithEvents TxtSendorID As common.Controls.MyTextBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents CmbSMSProvider As common.Controls.MyComboBox
    Friend WithEvents chkAllowSkippingPrevDocOnPayProcess As common.Controls.MyCheckBox
    Friend WithEvents ChkAllSampleParameter As common.Controls.MyCheckBox
    Friend WithEvents ChkDisplayParameterinQualityCheck As common.Controls.MyCheckBox
    Friend WithEvents GrpMilkTypeValidation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkDisplayTypeinMilkReceipt As common.Controls.MyCheckBox
    Friend WithEvents ChkValidationofMilkType As common.Controls.MyCheckBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents ChkIncentiveDeductionMilksampleQC As common.Controls.MyCheckBox
    Friend WithEvents txtStockTolerance As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents chkPriceChartGradeWise As common.Controls.MyCheckBox
    Friend WithEvents chkItemMilkType As common.Controls.MyCheckBox
    Friend WithEvents ChkFarmerPaymentCycle As common.Controls.MyCheckBox
    Friend WithEvents chkStdFATSNFRate As common.Controls.MyCheckBox
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents gv2 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents dtpBlockingDBTPeriod As common.Controls.MyDateTimePicker
End Class

