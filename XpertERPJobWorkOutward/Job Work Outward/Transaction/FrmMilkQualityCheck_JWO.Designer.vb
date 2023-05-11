Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkQualityCheck_JWO
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
        Me.components = New System.ComponentModel.Container()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.dtpQCInDateTime = New common.Controls.MyDateTimePicker()
        Me.lblQcInDateAndTime = New common.Controls.MyLabel()
        Me.fndGateEntryNo = New common.UserControls.txtFinder()
        Me.lblStatusValue = New common.Controls.MyLabel()
        Me.lblStatus = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.lblGateEntryNO = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblDateAndTime = New common.Controls.MyLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.grpEcoPro1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.CboMachine = New common.Controls.MyComboBox()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.BtnStart = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.cboComPort = New common.Controls.MyComboBox()
        Me.lblComPort = New common.Controls.MyLabel()
        Me.cboECOPro = New common.Controls.MyComboBox()
        Me.LblSnf = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.LblFAT = New common.Controls.MyLabel()
        Me.chkBothDoc = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblPending = New common.usLock()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.fndQcNo = New common.UserControls.txtNavigator()
        Me.lblQcNo = New common.Controls.MyLabel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.mnuSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuSeparatorItem1 = New Telerik.WinControls.UI.RadMenuSeparatorItem()
        Me.mnuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuEmailSmsSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.txtjobworklocation = New common.Controls.MyTextBox()
        Me.lblJobworkLocation = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.GrpControlSample = New System.Windows.Forms.GroupBox()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.txtRcptControlSampleSNF = New common.Controls.MyTextBox()
        Me.txtRcptControlSampleFAT = New common.Controls.MyTextBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDispControlSampleSNF = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtDispControlSampleFAT = New common.Controls.MyTextBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.fndTankerNo = New common.UserControls.txtFinder()
        Me.BtnRead = New Telerik.WinControls.UI.RadButton()
        Me.grpDocType = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkBulkMilkProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkMccProc = New Telerik.WinControls.UI.RadRadioButton()
        Me.TxtDeductionAmount = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblQcAcceptedOrRejected = New common.Controls.MyLabel()
        Me.dtpWeighmentDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtDipValue = New common.Controls.MyTextBox()
        Me.lblDipValue = New common.Controls.MyLabel()
        Me.lblChallanDate = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.lblChallanNo = New common.Controls.MyLabel()
        Me.fndVendor = New common.UserControls.txtFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtWeighmentNo = New common.Controls.MyTextBox()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.dtpChallanDate = New common.Controls.MyDateTimePicker()
        Me.txtChallanNo = New common.Controls.MyTextBox()
        Me.dtpGateEntryDateTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpQCOutDateTime = New common.Controls.MyDateTimePicker()
        Me.lblQCOutDateAndTime = New common.Controls.MyLabel()
        Me.grpParameterDetailBulk = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvParam = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvItem = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gvManualSeal = New common.UserControls.MyRadGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.gvPaperSeal = New common.UserControls.MyRadGridView()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnSendForApproval = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dtpQCInDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQcInDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatusValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.grpEcoPro1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEcoPro1.SuspendLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboComPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblComPort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboECOPro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblSnf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBothDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQcNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.txtjobworklocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblJobworkLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpControlSample.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRcptControlSampleSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRcptControlSampleFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispControlSampleSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDispControlSampleFAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnRead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDocType.SuspendLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDeductionAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQcAcceptedOrRejected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCOutDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpParameterDetailBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpParameterDetailBulk.SuspendLayout()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvManualSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvManualSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.gvPaperSeal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPaperSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendForApproval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpQCInDateTime
        '
        Me.dtpQCInDateTime.CalculationExpression = Nothing
        Me.dtpQCInDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCInDateTime.FieldCode = Nothing
        Me.dtpQCInDateTime.FieldDesc = Nothing
        Me.dtpQCInDateTime.FieldMaxLength = 0
        Me.dtpQCInDateTime.FieldName = Nothing
        Me.dtpQCInDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCInDateTime.isCalculatedField = False
        Me.dtpQCInDateTime.IsSourceFromTable = False
        Me.dtpQCInDateTime.IsSourceFromValueList = False
        Me.dtpQCInDateTime.IsUnique = False
        Me.dtpQCInDateTime.Location = New System.Drawing.Point(138, 30)
        Me.dtpQCInDateTime.MendatroryField = False
        Me.dtpQCInDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCInDateTime.MyLinkLable1 = Nothing
        Me.dtpQCInDateTime.MyLinkLable2 = Nothing
        Me.dtpQCInDateTime.Name = "dtpQCInDateTime"
        Me.dtpQCInDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCInDateTime.ReferenceFieldDesc = Nothing
        Me.dtpQCInDateTime.ReferenceFieldName = Nothing
        Me.dtpQCInDateTime.ReferenceTableName = Nothing
        Me.dtpQCInDateTime.Size = New System.Drawing.Size(190, 20)
        Me.dtpQCInDateTime.TabIndex = 288
        Me.dtpQCInDateTime.TabStop = False
        Me.dtpQCInDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpQCInDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblQcInDateAndTime
        '
        Me.lblQcInDateAndTime.FieldName = Nothing
        Me.lblQcInDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQcInDateAndTime.Location = New System.Drawing.Point(13, 30)
        Me.lblQcInDateAndTime.Name = "lblQcInDateAndTime"
        Me.lblQcInDateAndTime.Size = New System.Drawing.Size(114, 16)
        Me.lblQcInDateAndTime.TabIndex = 287
        Me.lblQcInDateAndTime.Text = "QC In Date And Time"
        '
        'fndGateEntryNo
        '
        Me.fndGateEntryNo.CalculationExpression = Nothing
        Me.fndGateEntryNo.Enabled = False
        Me.fndGateEntryNo.FieldCode = Nothing
        Me.fndGateEntryNo.FieldDesc = Nothing
        Me.fndGateEntryNo.FieldMaxLength = 0
        Me.fndGateEntryNo.FieldName = Nothing
        Me.fndGateEntryNo.isCalculatedField = False
        Me.fndGateEntryNo.IsSourceFromTable = False
        Me.fndGateEntryNo.IsSourceFromValueList = False
        Me.fndGateEntryNo.IsUnique = False
        Me.fndGateEntryNo.Location = New System.Drawing.Point(138, 53)
        Me.fndGateEntryNo.MendatroryField = True
        Me.fndGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNo.MyLinkLable1 = Nothing
        Me.fndGateEntryNo.MyLinkLable2 = Nothing
        Me.fndGateEntryNo.MyReadOnly = True
        Me.fndGateEntryNo.MyShowMasterFormButton = False
        Me.fndGateEntryNo.Name = "fndGateEntryNo"
        Me.fndGateEntryNo.ReferenceFieldDesc = Nothing
        Me.fndGateEntryNo.ReferenceFieldName = Nothing
        Me.fndGateEntryNo.ReferenceTableName = Nothing
        Me.fndGateEntryNo.Size = New System.Drawing.Size(190, 19)
        Me.fndGateEntryNo.TabIndex = 0
        Me.fndGateEntryNo.Value = ""
        '
        'lblStatusValue
        '
        Me.lblStatusValue.AutoSize = False
        Me.lblStatusValue.BorderVisible = True
        Me.lblStatusValue.FieldName = Nothing
        Me.lblStatusValue.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblStatusValue.Location = New System.Drawing.Point(138, 121)
        Me.lblStatusValue.Name = "lblStatusValue"
        Me.lblStatusValue.Size = New System.Drawing.Size(192, 19)
        Me.lblStatusValue.TabIndex = 286
        Me.lblStatusValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatus
        '
        Me.lblStatus.FieldName = Nothing
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(13, 122)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(99, 16)
        Me.lblStatus.TabIndex = 285
        Me.lblStatus.Text = "Weighment Status"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(329, 145)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(391, 19)
        Me.lblVendorName.TabIndex = 278
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGateEntryNO
        '
        Me.lblGateEntryNO.FieldName = Nothing
        Me.lblGateEntryNO.Location = New System.Drawing.Point(13, 53)
        Me.lblGateEntryNO.Name = "lblGateEntryNO"
        Me.lblGateEntryNO.Size = New System.Drawing.Size(82, 18)
        Me.lblGateEntryNO.TabIndex = 32
        Me.lblGateEntryNO.Text = "Gate Entry No. "
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(330, 166)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(390, 19)
        Me.lblLocationName.TabIndex = 275
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDateAndTime
        '
        Me.lblDateAndTime.FieldName = Nothing
        Me.lblDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateAndTime.Location = New System.Drawing.Point(330, 53)
        Me.lblDateAndTime.Name = "lblDateAndTime"
        Me.lblDateAndTime.Size = New System.Drawing.Size(126, 16)
        Me.lblDateAndTime.TabIndex = 252
        Me.lblDateAndTime.Text = "Gate Entry Date && Time"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(988, 609)
        Me.SplitContainer1.SplitterDistance = 177
        Me.SplitContainer1.TabIndex = 2
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.grpEcoPro1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.chkBothDoc)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblPending)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer3.Panel2.Controls.Add(Me.fndQcNo)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblQcNo)
        Me.SplitContainer3.Size = New System.Drawing.Size(988, 157)
        Me.SplitContainer3.SplitterDistance = 116
        Me.SplitContainer3.TabIndex = 253
        '
        'grpEcoPro1
        '
        Me.grpEcoPro1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpEcoPro1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grpEcoPro1.Controls.Add(Me.MyLabel10)
        Me.grpEcoPro1.Controls.Add(Me.CboMachine)
        Me.grpEcoPro1.Controls.Add(Me.MyLabel15)
        Me.grpEcoPro1.Controls.Add(Me.BtnStart)
        Me.grpEcoPro1.Controls.Add(Me.MyLabel5)
        Me.grpEcoPro1.Controls.Add(Me.MyLabel6)
        Me.grpEcoPro1.Controls.Add(Me.cboComPort)
        Me.grpEcoPro1.Controls.Add(Me.cboECOPro)
        Me.grpEcoPro1.Controls.Add(Me.LblSnf)
        Me.grpEcoPro1.Controls.Add(Me.MyLabel7)
        Me.grpEcoPro1.Controls.Add(Me.lblComPort)
        Me.grpEcoPro1.Controls.Add(Me.LblFAT)
        Me.grpEcoPro1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpEcoPro1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpEcoPro1.HeaderText = "Eco Pro"
        Me.grpEcoPro1.Location = New System.Drawing.Point(0, 0)
        Me.grpEcoPro1.Name = "grpEcoPro1"
        Me.grpEcoPro1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpEcoPro1.Size = New System.Drawing.Size(988, 116)
        Me.grpEcoPro1.TabIndex = 6
        Me.grpEcoPro1.Text = "Eco Pro"
        '
        'MyLabel10
        '
        Me.MyLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel10.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.MyLabel10.Location = New System.Drawing.Point(811, 95)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(166, 18)
        Me.MyLabel10.TabIndex = 66
        Me.MyLabel10.Text = "Press F2 For FAT/SNF Reading"
        '
        'CboMachine
        '
        Me.CboMachine.AutoCompleteDisplayMember = Nothing
        Me.CboMachine.AutoCompleteValueMember = Nothing
        Me.CboMachine.CalculationExpression = Nothing
        Me.CboMachine.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboMachine.FieldCode = Nothing
        Me.CboMachine.FieldDesc = Nothing
        Me.CboMachine.FieldMaxLength = 0
        Me.CboMachine.FieldName = Nothing
        Me.CboMachine.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboMachine.ForeColor = System.Drawing.Color.Lime
        Me.CboMachine.isCalculatedField = False
        Me.CboMachine.IsSourceFromTable = False
        Me.CboMachine.IsSourceFromValueList = False
        Me.CboMachine.IsUnique = False
        RadListDataItem1.Text = "COM1"
        RadListDataItem2.Text = "COM2"
        RadListDataItem3.Text = "COM3"
        RadListDataItem4.Text = "COM4"
        Me.CboMachine.Items.Add(RadListDataItem1)
        Me.CboMachine.Items.Add(RadListDataItem2)
        Me.CboMachine.Items.Add(RadListDataItem3)
        Me.CboMachine.Items.Add(RadListDataItem4)
        Me.CboMachine.Location = New System.Drawing.Point(594, 46)
        Me.CboMachine.MendatroryField = True
        Me.CboMachine.MyLinkLable1 = Me.MyLabel15
        Me.CboMachine.MyLinkLable2 = Nothing
        Me.CboMachine.Name = "CboMachine"
        Me.CboMachine.ReferenceFieldDesc = Nothing
        Me.CboMachine.ReferenceFieldName = Nothing
        Me.CboMachine.ReferenceTableName = Nothing
        '
        '
        '
        Me.CboMachine.RootElement.StretchVertically = True
        Me.CboMachine.Size = New System.Drawing.Size(109, 18)
        Me.CboMachine.TabIndex = 63
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel15.Location = New System.Drawing.Point(534, 47)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel15.TabIndex = 64
        Me.MyLabel15.Text = "Machine"
        '
        'BtnStart
        '
        Me.BtnStart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.BtnStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStart.Location = New System.Drawing.Point(708, 46)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(66, 18)
        Me.BtnStart.TabIndex = 62
        Me.BtnStart.Text = "Start"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel5.Location = New System.Drawing.Point(214, 45)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel5.TabIndex = 45
        Me.MyLabel5.Text = "Eco Pro"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel6.Location = New System.Drawing.Point(503, 11)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(66, 35)
        Me.MyLabel6.TabIndex = 59
        Me.MyLabel6.Text = "SNF"
        '
        'cboComPort
        '
        Me.cboComPort.AutoCompleteDisplayMember = Nothing
        Me.cboComPort.AutoCompleteValueMember = Nothing
        Me.cboComPort.CalculationExpression = Nothing
        Me.cboComPort.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboComPort.FieldCode = Nothing
        Me.cboComPort.FieldDesc = Nothing
        Me.cboComPort.FieldMaxLength = 0
        Me.cboComPort.FieldName = Nothing
        Me.cboComPort.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboComPort.ForeColor = System.Drawing.Color.Lime
        Me.cboComPort.isCalculatedField = False
        Me.cboComPort.IsSourceFromTable = False
        Me.cboComPort.IsSourceFromValueList = False
        Me.cboComPort.IsUnique = False
        RadListDataItem5.Text = "COM1"
        RadListDataItem6.Text = "COM2"
        RadListDataItem7.Text = "COM3"
        RadListDataItem8.Text = "COM4"
        Me.cboComPort.Items.Add(RadListDataItem5)
        Me.cboComPort.Items.Add(RadListDataItem6)
        Me.cboComPort.Items.Add(RadListDataItem7)
        Me.cboComPort.Items.Add(RadListDataItem8)
        Me.cboComPort.Location = New System.Drawing.Point(427, 45)
        Me.cboComPort.MendatroryField = True
        Me.cboComPort.MyLinkLable1 = Me.lblComPort
        Me.cboComPort.MyLinkLable2 = Nothing
        Me.cboComPort.Name = "cboComPort"
        Me.cboComPort.ReferenceFieldDesc = Nothing
        Me.cboComPort.ReferenceFieldName = Nothing
        Me.cboComPort.ReferenceTableName = Nothing
        '
        '
        '
        Me.cboComPort.RootElement.StretchVertically = True
        Me.cboComPort.Size = New System.Drawing.Size(101, 18)
        Me.cboComPort.TabIndex = 42
        '
        'lblComPort
        '
        Me.lblComPort.FieldName = Nothing
        Me.lblComPort.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComPort.ForeColor = System.Drawing.Color.Lime
        Me.lblComPort.Location = New System.Drawing.Point(367, 46)
        Me.lblComPort.Name = "lblComPort"
        Me.lblComPort.Size = New System.Drawing.Size(54, 16)
        Me.lblComPort.TabIndex = 43
        Me.lblComPort.Text = "Com Port"
        '
        'cboECOPro
        '
        Me.cboECOPro.AutoCompleteDisplayMember = Nothing
        Me.cboECOPro.AutoCompleteValueMember = Nothing
        Me.cboECOPro.CalculationExpression = Nothing
        Me.cboECOPro.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboECOPro.FieldCode = Nothing
        Me.cboECOPro.FieldDesc = Nothing
        Me.cboECOPro.FieldMaxLength = 0
        Me.cboECOPro.FieldName = Nothing
        Me.cboECOPro.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboECOPro.ForeColor = System.Drawing.Color.Lime
        Me.cboECOPro.isCalculatedField = False
        Me.cboECOPro.IsSourceFromTable = False
        Me.cboECOPro.IsSourceFromValueList = False
        Me.cboECOPro.IsUnique = False
        RadListDataItem9.Text = "COM1"
        RadListDataItem10.Text = "COM2"
        RadListDataItem11.Text = "COM3"
        RadListDataItem12.Text = "COM4"
        Me.cboECOPro.Items.Add(RadListDataItem9)
        Me.cboECOPro.Items.Add(RadListDataItem10)
        Me.cboECOPro.Items.Add(RadListDataItem11)
        Me.cboECOPro.Items.Add(RadListDataItem12)
        Me.cboECOPro.Location = New System.Drawing.Point(274, 44)
        Me.cboECOPro.MendatroryField = True
        Me.cboECOPro.MyLinkLable1 = Me.MyLabel5
        Me.cboECOPro.MyLinkLable2 = Nothing
        Me.cboECOPro.Name = "cboECOPro"
        Me.cboECOPro.ReferenceFieldDesc = Nothing
        Me.cboECOPro.ReferenceFieldName = Nothing
        Me.cboECOPro.ReferenceTableName = Nothing
        '
        '
        '
        Me.cboECOPro.RootElement.StretchVertically = True
        Me.cboECOPro.Size = New System.Drawing.Size(94, 18)
        Me.cboECOPro.TabIndex = 44
        '
        'LblSnf
        '
        Me.LblSnf.FieldName = Nothing
        Me.LblSnf.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSnf.ForeColor = System.Drawing.Color.Red
        Me.LblSnf.Location = New System.Drawing.Point(416, 11)
        Me.LblSnf.Name = "LblSnf"
        Me.LblSnf.Size = New System.Drawing.Size(81, 36)
        Me.LblSnf.TabIndex = 57
        Me.LblSnf.Text = "00.00"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.ForeColor = System.Drawing.Color.Lime
        Me.MyLabel7.Location = New System.Drawing.Point(349, 10)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(65, 35)
        Me.MyLabel7.TabIndex = 58
        Me.MyLabel7.Text = "FAT"
        '
        'LblFAT
        '
        Me.LblFAT.FieldName = Nothing
        Me.LblFAT.Font = New System.Drawing.Font("Arial", 20.0!, System.Drawing.FontStyle.Bold)
        Me.LblFAT.ForeColor = System.Drawing.Color.Red
        Me.LblFAT.Location = New System.Drawing.Point(575, 11)
        Me.LblFAT.Name = "LblFAT"
        Me.LblFAT.Size = New System.Drawing.Size(80, 35)
        Me.LblFAT.TabIndex = 56
        Me.LblFAT.Text = "00.00"
        '
        'chkBothDoc
        '
        Me.chkBothDoc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBothDoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBothDoc.Location = New System.Drawing.Point(549, 14)
        Me.chkBothDoc.Name = "chkBothDoc"
        Me.chkBothDoc.Size = New System.Drawing.Size(225, 16)
        Me.chkBothDoc.TabIndex = 346
        Me.chkBothDoc.Text = "Show Tanker and Sku In Both Document"
        Me.chkBothDoc.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        Me.chkBothDoc.Visible = False
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(878, 12)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 248
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = XpertERPJobWorkOutward.My.Resources.new1
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(448, 14)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 250
        '
        'fndQcNo
        '
        Me.fndQcNo.FieldName = Nothing
        Me.fndQcNo.Location = New System.Drawing.Point(137, 14)
        Me.fndQcNo.MendatroryField = False
        Me.fndQcNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndQcNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndQcNo.MyLinkLable1 = Me.lblQcNo
        Me.fndQcNo.MyLinkLable2 = Nothing
        Me.fndQcNo.MyMaxLength = 32767
        Me.fndQcNo.MyReadOnly = False
        Me.fndQcNo.Name = "fndQcNo"
        Me.fndQcNo.Size = New System.Drawing.Size(310, 18)
        Me.fndQcNo.TabIndex = 0
        Me.fndQcNo.Value = ""
        '
        'lblQcNo
        '
        Me.lblQcNo.FieldName = Nothing
        Me.lblQcNo.Location = New System.Drawing.Point(10, 15)
        Me.lblQcNo.Name = "lblQcNo"
        Me.lblQcNo.Size = New System.Drawing.Size(42, 18)
        Me.lblQcNo.TabIndex = 251
        Me.lblQcNo.Text = "QC No."
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSetting})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(988, 20)
        Me.RadMenu1.TabIndex = 252
        Me.RadMenu1.Text = "RadMenu1"
        '
        'mnuSetting
        '
        Me.mnuSetting.AccessibleDescription = "Setting"
        Me.mnuSetting.AccessibleName = "Setting"
        Me.mnuSetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuSaveLayout, Me.RadMenuSeparatorItem1, Me.mnuDeleteLayout, Me.mnuExit, Me.mnuEmailSmsSetting})
        Me.mnuSetting.Name = "mnuSetting"
        Me.mnuSetting.Text = "Setting"
        '
        'mnuSaveLayout
        '
        Me.mnuSaveLayout.AccessibleDescription = "Save Layout"
        Me.mnuSaveLayout.AccessibleName = "Save Layout"
        Me.mnuSaveLayout.Name = "mnuSaveLayout"
        Me.mnuSaveLayout.Text = "Save Layout"
        '
        'RadMenuSeparatorItem1
        '
        Me.RadMenuSeparatorItem1.AccessibleDescription = "RadMenuSeparatorItem1"
        Me.RadMenuSeparatorItem1.AccessibleName = "RadMenuSeparatorItem1"
        Me.RadMenuSeparatorItem1.Name = "RadMenuSeparatorItem1"
        Me.RadMenuSeparatorItem1.Text = "RadMenuSeparatorItem1"
        '
        'mnuDeleteLayout
        '
        Me.mnuDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.mnuDeleteLayout.AccessibleName = "Delete Layout"
        Me.mnuDeleteLayout.Name = "mnuDeleteLayout"
        Me.mnuDeleteLayout.Text = "Delete Layout"
        '
        'mnuExit
        '
        Me.mnuExit.AccessibleDescription = "Exit"
        Me.mnuExit.AccessibleName = "Exit"
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Text = "Exit"
        '
        'mnuEmailSmsSetting
        '
        Me.mnuEmailSmsSetting.AccessibleDescription = "Email/SMS Setting"
        Me.mnuEmailSmsSetting.AccessibleName = "Email/SMS Setting"
        Me.mnuEmailSmsSetting.Name = "mnuEmailSmsSetting"
        Me.mnuEmailSmsSetting.Text = "Email/SMS Setting"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSendForApproval)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer2.Size = New System.Drawing.Size(988, 428)
        Me.SplitContainer2.SplitterDistance = 392
        Me.SplitContainer2.TabIndex = 1
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
        Me.RadPageView1.Size = New System.Drawing.Size(988, 392)
        Me.RadPageView1.TabIndex = 305
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer4)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(967, 344)
        Me.RadPageViewPage1.Text = "General"
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
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtjobworklocation)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblJobworkLocation)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer4.Panel1.Controls.Add(Me.GrpControlSample)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndTankerNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.BtnRead)
        Me.SplitContainer4.Panel1.Controls.Add(Me.grpDocType)
        Me.SplitContainer4.Panel1.Controls.Add(Me.TxtDeductionAmount)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblQcAcceptedOrRejected)
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpWeighmentDate)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtDipValue)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblGateEntryNO)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblDipValue)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblChallanDate)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndLocation)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblChallanNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndVendor)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblVendor)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtWeighmentNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpChallanDate)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblDateAndTime)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtChallanNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblQcInDateAndTime)
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpGateEntryDateTime)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpQCInDateTime)
        Me.SplitContainer4.Panel1.Controls.Add(Me.dtpQCOutDateTime)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblQCOutDateAndTime)
        Me.SplitContainer4.Panel1.Controls.Add(Me.fndGateEntryNo)
        Me.SplitContainer4.Panel1.Controls.Add(Me.lblStatusValue)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.grpParameterDetailBulk)
        Me.SplitContainer4.Size = New System.Drawing.Size(967, 344)
        Me.SplitContainer4.SplitterDistance = 214
        Me.SplitContainer4.TabIndex = 34
        '
        'txtjobworklocation
        '
        Me.txtjobworklocation.CalculationExpression = Nothing
        Me.txtjobworklocation.Enabled = False
        Me.txtjobworklocation.FieldCode = Nothing
        Me.txtjobworklocation.FieldDesc = Nothing
        Me.txtjobworklocation.FieldMaxLength = 0
        Me.txtjobworklocation.FieldName = Nothing
        Me.txtjobworklocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobworklocation.isCalculatedField = False
        Me.txtjobworklocation.IsSourceFromTable = False
        Me.txtjobworklocation.IsSourceFromValueList = False
        Me.txtjobworklocation.IsUnique = False
        Me.txtjobworklocation.Location = New System.Drawing.Point(138, 188)
        Me.txtjobworklocation.MaxLength = 50
        Me.txtjobworklocation.MendatroryField = True
        Me.txtjobworklocation.MyLinkLable1 = Nothing
        Me.txtjobworklocation.MyLinkLable2 = Nothing
        Me.txtjobworklocation.Name = "txtjobworklocation"
        Me.txtjobworklocation.ReferenceFieldDesc = Nothing
        Me.txtjobworklocation.ReferenceFieldName = Nothing
        Me.txtjobworklocation.ReferenceTableName = Nothing
        Me.txtjobworklocation.Size = New System.Drawing.Size(190, 18)
        Me.txtjobworklocation.TabIndex = 353
        '
        'lblJobworkLocation
        '
        Me.lblJobworkLocation.AutoSize = False
        Me.lblJobworkLocation.BorderVisible = True
        Me.lblJobworkLocation.FieldName = Nothing
        Me.lblJobworkLocation.Location = New System.Drawing.Point(330, 189)
        Me.lblJobworkLocation.Name = "lblJobworkLocation"
        Me.lblJobworkLocation.Size = New System.Drawing.Size(388, 18)
        Me.lblJobworkLocation.TabIndex = 352
        Me.lblJobworkLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(13, 188)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(94, 16)
        Me.MyLabel8.TabIndex = 351
        Me.MyLabel8.Text = "Jobwork Location"
        '
        'GrpControlSample
        '
        Me.GrpControlSample.Controls.Add(Me.MyLabel12)
        Me.GrpControlSample.Controls.Add(Me.txtRcptControlSampleSNF)
        Me.GrpControlSample.Controls.Add(Me.txtRcptControlSampleFAT)
        Me.GrpControlSample.Controls.Add(Me.MyLabel13)
        Me.GrpControlSample.Controls.Add(Me.txtDispControlSampleSNF)
        Me.GrpControlSample.Controls.Add(Me.MyLabel11)
        Me.GrpControlSample.Controls.Add(Me.txtDispControlSampleFAT)
        Me.GrpControlSample.Controls.Add(Me.MyLabel4)
        Me.GrpControlSample.Location = New System.Drawing.Point(782, 96)
        Me.GrpControlSample.Name = "GrpControlSample"
        Me.GrpControlSample.Size = New System.Drawing.Size(103, 16)
        Me.GrpControlSample.TabIndex = 310
        Me.GrpControlSample.TabStop = False
        Me.GrpControlSample.Text = "Control Sample"
        Me.GrpControlSample.Visible = False
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Location = New System.Drawing.Point(243, 34)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(156, 18)
        Me.MyLabel12.TabIndex = 367
        Me.MyLabel12.Text = "Receipt Control Sample SNF%"
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
        Me.txtRcptControlSampleSNF.Location = New System.Drawing.Point(408, 35)
        Me.txtRcptControlSampleSNF.MaxLength = 50
        Me.txtRcptControlSampleSNF.MendatroryField = False
        Me.txtRcptControlSampleSNF.MyLinkLable1 = Nothing
        Me.txtRcptControlSampleSNF.MyLinkLable2 = Nothing
        Me.txtRcptControlSampleSNF.Name = "txtRcptControlSampleSNF"
        Me.txtRcptControlSampleSNF.ReferenceFieldDesc = Nothing
        Me.txtRcptControlSampleSNF.ReferenceFieldName = Nothing
        Me.txtRcptControlSampleSNF.ReferenceTableName = Nothing
        Me.txtRcptControlSampleSNF.Size = New System.Drawing.Size(63, 20)
        Me.txtRcptControlSampleSNF.TabIndex = 366
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
        Me.txtRcptControlSampleFAT.Location = New System.Drawing.Point(174, 35)
        Me.txtRcptControlSampleFAT.MaxLength = 50
        Me.txtRcptControlSampleFAT.MendatroryField = False
        Me.txtRcptControlSampleFAT.MyLinkLable1 = Nothing
        Me.txtRcptControlSampleFAT.MyLinkLable2 = Nothing
        Me.txtRcptControlSampleFAT.Name = "txtRcptControlSampleFAT"
        Me.txtRcptControlSampleFAT.ReferenceFieldDesc = Nothing
        Me.txtRcptControlSampleFAT.ReferenceFieldName = Nothing
        Me.txtRcptControlSampleFAT.ReferenceTableName = Nothing
        Me.txtRcptControlSampleFAT.Size = New System.Drawing.Size(63, 20)
        Me.txtRcptControlSampleFAT.TabIndex = 364
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Location = New System.Drawing.Point(7, 34)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(155, 18)
        Me.MyLabel13.TabIndex = 365
        Me.MyLabel13.Text = "Receipt Control Sample FAT%"
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
        Me.txtDispControlSampleSNF.Location = New System.Drawing.Point(408, 12)
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
        Me.txtDispControlSampleSNF.TabIndex = 362
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Location = New System.Drawing.Point(241, 12)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(162, 18)
        Me.MyLabel11.TabIndex = 363
        Me.MyLabel11.Text = "Dispatch Control Sample SNF%"
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
        Me.txtDispControlSampleFAT.Location = New System.Drawing.Point(174, 12)
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
        Me.txtDispControlSampleFAT.TabIndex = 360
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(7, 13)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(161, 18)
        Me.MyLabel4.TabIndex = 361
        Me.MyLabel4.Text = "Dispatch Control Sample FAT%"
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
        Me.fndTankerNo.Location = New System.Drawing.Point(138, 8)
        Me.fndTankerNo.MendatroryField = True
        Me.fndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTankerNo.MyLinkLable1 = Nothing
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.MyReadOnly = False
        Me.fndTankerNo.MyShowMasterFormButton = False
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.ReferenceFieldDesc = Nothing
        Me.fndTankerNo.ReferenceFieldName = Nothing
        Me.fndTankerNo.ReferenceTableName = Nothing
        Me.fndTankerNo.Size = New System.Drawing.Size(190, 19)
        Me.fndTankerNo.TabIndex = 309
        Me.fndTankerNo.Value = ""
        '
        'BtnRead
        '
        Me.BtnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnRead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRead.Location = New System.Drawing.Point(726, 189)
        Me.BtnRead.Name = "BtnRead"
        Me.BtnRead.Size = New System.Drawing.Size(66, 18)
        Me.BtnRead.TabIndex = 308
        Me.BtnRead.Text = "Read"
        Me.BtnRead.Visible = False
        '
        'grpDocType
        '
        Me.grpDocType.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpDocType.Controls.Add(Me.chkBulkMilkProc)
        Me.grpDocType.Controls.Add(Me.chkMccProc)
        Me.grpDocType.HeaderText = "Select Type Of Document"
        Me.grpDocType.Location = New System.Drawing.Point(801, 12)
        Me.grpDocType.Name = "grpDocType"
        Me.grpDocType.Size = New System.Drawing.Size(44, 34)
        Me.grpDocType.TabIndex = 10
        Me.grpDocType.Text = "Select Type Of Document"
        Me.grpDocType.Visible = False
        '
        'chkBulkMilkProc
        '
        Me.chkBulkMilkProc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBulkMilkProc.Location = New System.Drawing.Point(5, 13)
        Me.chkBulkMilkProc.Name = "chkBulkMilkProc"
        Me.chkBulkMilkProc.Size = New System.Drawing.Size(94, 18)
        Me.chkBulkMilkProc.TabIndex = 0
        Me.chkBulkMilkProc.Text = "Tanker Receipt"
        Me.chkBulkMilkProc.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkMccProc
        '
        Me.chkMccProc.Location = New System.Drawing.Point(229, 13)
        Me.chkMccProc.Name = "chkMccProc"
        Me.chkMccProc.Size = New System.Drawing.Size(78, 18)
        Me.chkMccProc.TabIndex = 1
        Me.chkMccProc.Text = "Sku Receipt"
        '
        'TxtDeductionAmount
        '
        Me.TxtDeductionAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtDeductionAmount.CalculationExpression = Nothing
        Me.TxtDeductionAmount.DecimalPlaces = 2
        Me.TxtDeductionAmount.Enabled = False
        Me.TxtDeductionAmount.FieldCode = Nothing
        Me.TxtDeductionAmount.FieldDesc = Nothing
        Me.TxtDeductionAmount.FieldMaxLength = 0
        Me.TxtDeductionAmount.FieldName = Nothing
        Me.TxtDeductionAmount.isCalculatedField = False
        Me.TxtDeductionAmount.IsSourceFromTable = False
        Me.TxtDeductionAmount.IsSourceFromValueList = False
        Me.TxtDeductionAmount.IsUnique = False
        Me.TxtDeductionAmount.Location = New System.Drawing.Point(458, 96)
        Me.TxtDeductionAmount.MendatroryField = True
        Me.TxtDeductionAmount.MyLinkLable1 = Nothing
        Me.TxtDeductionAmount.MyLinkLable2 = Nothing
        Me.TxtDeductionAmount.Name = "TxtDeductionAmount"
        Me.TxtDeductionAmount.ReferenceFieldDesc = Nothing
        Me.TxtDeductionAmount.ReferenceFieldName = Nothing
        Me.TxtDeductionAmount.ReferenceTableName = Nothing
        Me.TxtDeductionAmount.Size = New System.Drawing.Size(262, 20)
        Me.TxtDeductionAmount.TabIndex = 306
        Me.TxtDeductionAmount.Text = "0"
        Me.TxtDeductionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtDeductionAmount.Value = 0.0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(330, 98)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel1.TabIndex = 307
        Me.MyLabel1.Text = "Deduction Amount"
        '
        'lblQcAcceptedOrRejected
        '
        Me.lblQcAcceptedOrRejected.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblQcAcceptedOrRejected.FieldName = Nothing
        Me.lblQcAcceptedOrRejected.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblQcAcceptedOrRejected.Location = New System.Drawing.Point(725, 8)
        Me.lblQcAcceptedOrRejected.Name = "lblQcAcceptedOrRejected"
        Me.lblQcAcceptedOrRejected.Size = New System.Drawing.Size(2, 2)
        Me.lblQcAcceptedOrRejected.TabIndex = 305
        '
        'dtpWeighmentDate
        '
        Me.dtpWeighmentDate.CalculationExpression = Nothing
        Me.dtpWeighmentDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpWeighmentDate.Enabled = False
        Me.dtpWeighmentDate.FieldCode = Nothing
        Me.dtpWeighmentDate.FieldDesc = Nothing
        Me.dtpWeighmentDate.FieldMaxLength = 0
        Me.dtpWeighmentDate.FieldName = Nothing
        Me.dtpWeighmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDate.isCalculatedField = False
        Me.dtpWeighmentDate.IsSourceFromTable = False
        Me.dtpWeighmentDate.IsSourceFromValueList = False
        Me.dtpWeighmentDate.IsUnique = False
        Me.dtpWeighmentDate.Location = New System.Drawing.Point(624, 119)
        Me.dtpWeighmentDate.MendatroryField = False
        Me.dtpWeighmentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.MyLinkLable1 = Nothing
        Me.dtpWeighmentDate.MyLinkLable2 = Nothing
        Me.dtpWeighmentDate.Name = "dtpWeighmentDate"
        Me.dtpWeighmentDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDate.ReadOnly = True
        Me.dtpWeighmentDate.ReferenceFieldDesc = Nothing
        Me.dtpWeighmentDate.ReferenceFieldName = Nothing
        Me.dtpWeighmentDate.ReferenceTableName = Nothing
        Me.dtpWeighmentDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpWeighmentDate.TabIndex = 300
        Me.dtpWeighmentDate.TabStop = False
        Me.dtpWeighmentDate.Text = "10/06/2011"
        Me.dtpWeighmentDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(593, 120)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel3.TabIndex = 293
        Me.MyLabel3.Text = "Date"
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
        Me.txtDipValue.Location = New System.Drawing.Point(138, 99)
        Me.txtDipValue.MaxLength = 50
        Me.txtDipValue.MendatroryField = False
        Me.txtDipValue.MyLinkLable1 = Nothing
        Me.txtDipValue.MyLinkLable2 = Nothing
        Me.txtDipValue.Name = "txtDipValue"
        Me.txtDipValue.ReferenceFieldDesc = Nothing
        Me.txtDipValue.ReferenceFieldName = Nothing
        Me.txtDipValue.ReferenceTableName = Nothing
        Me.txtDipValue.Size = New System.Drawing.Size(191, 20)
        Me.txtDipValue.TabIndex = 304
        '
        'lblDipValue
        '
        Me.lblDipValue.FieldName = Nothing
        Me.lblDipValue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDipValue.Location = New System.Drawing.Point(13, 99)
        Me.lblDipValue.Name = "lblDipValue"
        Me.lblDipValue.Size = New System.Drawing.Size(57, 16)
        Me.lblDipValue.TabIndex = 303
        Me.lblDipValue.Text = "DIP Value"
        '
        'lblChallanDate
        '
        Me.lblChallanDate.FieldName = Nothing
        Me.lblChallanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanDate.Location = New System.Drawing.Point(330, 76)
        Me.lblChallanDate.Name = "lblChallanDate"
        Me.lblChallanDate.Size = New System.Drawing.Size(72, 16)
        Me.lblChallanDate.TabIndex = 283
        Me.lblChallanDate.Text = "Challan Date"
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.Enabled = False
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(138, 166)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = True
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(190, 19)
        Me.fndLocation.TabIndex = 302
        Me.fndLocation.Value = ""
        '
        'lblChallanNo
        '
        Me.lblChallanNo.FieldName = Nothing
        Me.lblChallanNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChallanNo.Location = New System.Drawing.Point(13, 75)
        Me.lblChallanNo.Name = "lblChallanNo"
        Me.lblChallanNo.Size = New System.Drawing.Size(62, 16)
        Me.lblChallanNo.TabIndex = 281
        Me.lblChallanNo.Text = "Challan No"
        '
        'fndVendor
        '
        Me.fndVendor.CalculationExpression = Nothing
        Me.fndVendor.Enabled = False
        Me.fndVendor.FieldCode = Nothing
        Me.fndVendor.FieldDesc = Nothing
        Me.fndVendor.FieldMaxLength = 0
        Me.fndVendor.FieldName = Nothing
        Me.fndVendor.isCalculatedField = False
        Me.fndVendor.IsSourceFromTable = False
        Me.fndVendor.IsSourceFromValueList = False
        Me.fndVendor.IsUnique = False
        Me.fndVendor.Location = New System.Drawing.Point(138, 143)
        Me.fndVendor.MendatroryField = True
        Me.fndVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendor.MyLinkLable1 = Nothing
        Me.fndVendor.MyLinkLable2 = Nothing
        Me.fndVendor.MyReadOnly = True
        Me.fndVendor.MyShowMasterFormButton = False
        Me.fndVendor.Name = "fndVendor"
        Me.fndVendor.ReferenceFieldDesc = Nothing
        Me.fndVendor.ReferenceFieldName = Nothing
        Me.fndVendor.ReferenceTableName = Nothing
        Me.fndVendor.Size = New System.Drawing.Size(190, 19)
        Me.fndVendor.TabIndex = 301
        Me.fndVendor.Value = ""
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(13, 143)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 16)
        Me.lblVendor.TabIndex = 276
        Me.lblVendor.Text = "Vendor"
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(13, 166)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 273
        Me.lblLocation.Text = "Location"
        '
        'txtWeighmentNo
        '
        Me.txtWeighmentNo.CalculationExpression = Nothing
        Me.txtWeighmentNo.Enabled = False
        Me.txtWeighmentNo.FieldCode = Nothing
        Me.txtWeighmentNo.FieldDesc = Nothing
        Me.txtWeighmentNo.FieldMaxLength = 0
        Me.txtWeighmentNo.FieldName = Nothing
        Me.txtWeighmentNo.isCalculatedField = False
        Me.txtWeighmentNo.IsSourceFromTable = False
        Me.txtWeighmentNo.IsSourceFromValueList = False
        Me.txtWeighmentNo.IsUnique = False
        Me.txtWeighmentNo.Location = New System.Drawing.Point(458, 119)
        Me.txtWeighmentNo.MaxLength = 50
        Me.txtWeighmentNo.MendatroryField = False
        Me.txtWeighmentNo.MyLinkLable1 = Nothing
        Me.txtWeighmentNo.MyLinkLable2 = Nothing
        Me.txtWeighmentNo.Name = "txtWeighmentNo"
        Me.txtWeighmentNo.ReadOnly = True
        Me.txtWeighmentNo.ReferenceFieldDesc = Nothing
        Me.txtWeighmentNo.ReferenceFieldName = Nothing
        Me.txtWeighmentNo.ReferenceTableName = Nothing
        Me.txtWeighmentNo.Size = New System.Drawing.Size(130, 20)
        Me.txtWeighmentNo.TabIndex = 299
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(13, 7)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(59, 16)
        Me.lblTankerNo.TabIndex = 279
        Me.lblTankerNo.Text = "Tanker No"
        '
        'dtpChallanDate
        '
        Me.dtpChallanDate.CalculationExpression = Nothing
        Me.dtpChallanDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpChallanDate.Enabled = False
        Me.dtpChallanDate.FieldCode = Nothing
        Me.dtpChallanDate.FieldDesc = Nothing
        Me.dtpChallanDate.FieldMaxLength = 0
        Me.dtpChallanDate.FieldName = Nothing
        Me.dtpChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChallanDate.isCalculatedField = False
        Me.dtpChallanDate.IsSourceFromTable = False
        Me.dtpChallanDate.IsSourceFromValueList = False
        Me.dtpChallanDate.IsUnique = False
        Me.dtpChallanDate.Location = New System.Drawing.Point(458, 74)
        Me.dtpChallanDate.MendatroryField = False
        Me.dtpChallanDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.MyLinkLable1 = Nothing
        Me.dtpChallanDate.MyLinkLable2 = Nothing
        Me.dtpChallanDate.Name = "dtpChallanDate"
        Me.dtpChallanDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpChallanDate.ReadOnly = True
        Me.dtpChallanDate.ReferenceFieldDesc = Nothing
        Me.dtpChallanDate.ReferenceFieldName = Nothing
        Me.dtpChallanDate.ReferenceTableName = Nothing
        Me.dtpChallanDate.Size = New System.Drawing.Size(262, 20)
        Me.dtpChallanDate.TabIndex = 298
        Me.dtpChallanDate.TabStop = False
        Me.dtpChallanDate.Text = "10/06/2011"
        Me.dtpChallanDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtChallanNo
        '
        Me.txtChallanNo.CalculationExpression = Nothing
        Me.txtChallanNo.Enabled = False
        Me.txtChallanNo.FieldCode = Nothing
        Me.txtChallanNo.FieldDesc = Nothing
        Me.txtChallanNo.FieldMaxLength = 0
        Me.txtChallanNo.FieldName = Nothing
        Me.txtChallanNo.isCalculatedField = False
        Me.txtChallanNo.IsSourceFromTable = False
        Me.txtChallanNo.IsSourceFromValueList = False
        Me.txtChallanNo.IsUnique = False
        Me.txtChallanNo.Location = New System.Drawing.Point(138, 75)
        Me.txtChallanNo.MaxLength = 50
        Me.txtChallanNo.MendatroryField = False
        Me.txtChallanNo.MyLinkLable1 = Nothing
        Me.txtChallanNo.MyLinkLable2 = Nothing
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.ReadOnly = True
        Me.txtChallanNo.ReferenceFieldDesc = Nothing
        Me.txtChallanNo.ReferenceFieldName = Nothing
        Me.txtChallanNo.ReferenceTableName = Nothing
        Me.txtChallanNo.Size = New System.Drawing.Size(191, 20)
        Me.txtChallanNo.TabIndex = 297
        '
        'dtpGateEntryDateTime
        '
        Me.dtpGateEntryDateTime.CalculationExpression = Nothing
        Me.dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpGateEntryDateTime.Enabled = False
        Me.dtpGateEntryDateTime.FieldCode = Nothing
        Me.dtpGateEntryDateTime.FieldDesc = Nothing
        Me.dtpGateEntryDateTime.FieldMaxLength = 0
        Me.dtpGateEntryDateTime.FieldName = Nothing
        Me.dtpGateEntryDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGateEntryDateTime.isCalculatedField = False
        Me.dtpGateEntryDateTime.IsSourceFromTable = False
        Me.dtpGateEntryDateTime.IsSourceFromValueList = False
        Me.dtpGateEntryDateTime.IsUnique = False
        Me.dtpGateEntryDateTime.Location = New System.Drawing.Point(458, 50)
        Me.dtpGateEntryDateTime.MendatroryField = False
        Me.dtpGateEntryDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.MyLinkLable1 = Nothing
        Me.dtpGateEntryDateTime.MyLinkLable2 = Nothing
        Me.dtpGateEntryDateTime.Name = "dtpGateEntryDateTime"
        Me.dtpGateEntryDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.ReadOnly = True
        Me.dtpGateEntryDateTime.ReferenceFieldDesc = Nothing
        Me.dtpGateEntryDateTime.ReferenceFieldName = Nothing
        Me.dtpGateEntryDateTime.ReferenceTableName = Nothing
        Me.dtpGateEntryDateTime.Size = New System.Drawing.Size(261, 20)
        Me.dtpGateEntryDateTime.TabIndex = 295
        Me.dtpGateEntryDateTime.TabStop = False
        Me.dtpGateEntryDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpGateEntryDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(330, 121)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel2.TabIndex = 291
        Me.MyLabel2.Text = "Weighment No"
        '
        'dtpQCOutDateTime
        '
        Me.dtpQCOutDateTime.CalculationExpression = Nothing
        Me.dtpQCOutDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCOutDateTime.FieldCode = Nothing
        Me.dtpQCOutDateTime.FieldDesc = Nothing
        Me.dtpQCOutDateTime.FieldMaxLength = 0
        Me.dtpQCOutDateTime.FieldName = Nothing
        Me.dtpQCOutDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCOutDateTime.isCalculatedField = False
        Me.dtpQCOutDateTime.IsSourceFromTable = False
        Me.dtpQCOutDateTime.IsSourceFromValueList = False
        Me.dtpQCOutDateTime.IsUnique = False
        Me.dtpQCOutDateTime.Location = New System.Drawing.Point(458, 26)
        Me.dtpQCOutDateTime.MendatroryField = False
        Me.dtpQCOutDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutDateTime.MyLinkLable1 = Nothing
        Me.dtpQCOutDateTime.MyLinkLable2 = Nothing
        Me.dtpQCOutDateTime.Name = "dtpQCOutDateTime"
        Me.dtpQCOutDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCOutDateTime.ReferenceFieldDesc = Nothing
        Me.dtpQCOutDateTime.ReferenceFieldName = Nothing
        Me.dtpQCOutDateTime.ReferenceTableName = Nothing
        Me.dtpQCOutDateTime.Size = New System.Drawing.Size(260, 20)
        Me.dtpQCOutDateTime.TabIndex = 290
        Me.dtpQCOutDateTime.TabStop = False
        Me.dtpQCOutDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpQCOutDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblQCOutDateAndTime
        '
        Me.lblQCOutDateAndTime.FieldName = Nothing
        Me.lblQCOutDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCOutDateAndTime.Location = New System.Drawing.Point(329, 30)
        Me.lblQCOutDateAndTime.Name = "lblQCOutDateAndTime"
        Me.lblQCOutDateAndTime.Size = New System.Drawing.Size(123, 16)
        Me.lblQCOutDateAndTime.TabIndex = 289
        Me.lblQCOutDateAndTime.Text = "QC Out Date And Time"
        '
        'grpParameterDetailBulk
        '
        Me.grpParameterDetailBulk.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpParameterDetailBulk.Controls.Add(Me.gvParam)
        Me.grpParameterDetailBulk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpParameterDetailBulk.HeaderText = "QC Parameter Details"
        Me.grpParameterDetailBulk.Location = New System.Drawing.Point(0, 0)
        Me.grpParameterDetailBulk.Name = "grpParameterDetailBulk"
        Me.grpParameterDetailBulk.Size = New System.Drawing.Size(967, 126)
        Me.grpParameterDetailBulk.TabIndex = 286
        Me.grpParameterDetailBulk.Text = "QC Parameter Details"
        '
        'gvParam
        '
        Me.gvParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvParam.Location = New System.Drawing.Point(2, 18)
        '
        'gvParam
        '
        Me.gvParam.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvParam.Name = "gvParam"
        Me.gvParam.ShowHeaderCellButtons = True
        Me.gvParam.Size = New System.Drawing.Size(963, 106)
        Me.gvParam.TabIndex = 264
        Me.gvParam.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(76.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(967, 344)
        Me.RadPageViewPage2.Text = "Item Details"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvItem)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Item Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(967, 344)
        Me.RadGroupBox1.TabIndex = 285
        Me.RadGroupBox1.Text = "Item Details"
        '
        'gvItem
        '
        Me.gvItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvItem.Location = New System.Drawing.Point(2, 18)
        '
        '
        '
        Me.gvItem.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvItem.Name = "gvItem"
        Me.gvItem.ShowHeaderCellButtons = True
        Me.gvItem.Size = New System.Drawing.Size(963, 324)
        Me.gvItem.TabIndex = 264
        Me.gvItem.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer5)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(73.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(967, 344)
        Me.RadPageViewPage3.Text = "Seal Details"
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer5.Size = New System.Drawing.Size(967, 344)
        Me.SplitContainer5.SplitterDistance = 496
        Me.SplitContainer5.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gvManualSeal)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(496, 344)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Manual Seal"
        '
        'gvManualSeal
        '
        Me.gvManualSeal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvManualSeal.Location = New System.Drawing.Point(3, 18)
        '
        'gvManualSeal
        '
        Me.gvManualSeal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvManualSeal.Name = "gvManualSeal"
        Me.gvManualSeal.ShowHeaderCellButtons = True
        Me.gvManualSeal.Size = New System.Drawing.Size(490, 323)
        Me.gvManualSeal.TabIndex = 203
        Me.gvManualSeal.Text = "RadGridView1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.gvPaperSeal)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(467, 344)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Paper Seal"
        '
        'gvPaperSeal
        '
        Me.gvPaperSeal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvPaperSeal.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gvPaperSeal.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPaperSeal.Name = "gvPaperSeal"
        Me.gvPaperSeal.ShowHeaderCellButtons = True
        Me.gvPaperSeal.Size = New System.Drawing.Size(461, 323)
        Me.gvPaperSeal.TabIndex = 203
        Me.gvPaperSeal.Text = "RadGridView1"
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(456, 7)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(60, 18)
        Me.btnReverse.TabIndex = 7
        Me.btnReverse.Text = "Reverse"
        '
        'btnSendForApproval
        '
        Me.btnSendForApproval.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSendForApproval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendForApproval.Location = New System.Drawing.Point(291, 7)
        Me.btnSendForApproval.Name = "btnSendForApproval"
        Me.btnSendForApproval.Size = New System.Drawing.Size(161, 18)
        Me.btnSendForApproval.TabIndex = 6
        Me.btnSendForApproval.Text = "Send For Special Approval"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(217, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(144, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(920, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmMilkQualityCheck_JWO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 609)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkQualityCheck_JWO"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmJobMilkQualityCheck"
        CType(Me.dtpQCInDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQcInDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatusValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.grpEcoPro1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEcoPro1.ResumeLayout(False)
        Me.grpEcoPro1.PerformLayout()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboMachine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboComPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblComPort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboECOPro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblSnf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBothDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQcNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.txtjobworklocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblJobworkLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpControlSample.ResumeLayout(False)
        Me.GrpControlSample.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRcptControlSampleSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRcptControlSampleFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispControlSampleSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDispControlSampleFAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnRead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDocType.ResumeLayout(False)
        Me.grpDocType.PerformLayout()
        CType(Me.chkBulkMilkProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMccProc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDeductionAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQcAcceptedOrRejected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDipValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChallanNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCOutDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpParameterDetailBulk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpParameterDetailBulk.ResumeLayout(False)
        CType(Me.gvParam.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvParam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gvManualSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvManualSeal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.gvPaperSeal.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPaperSeal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendForApproval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpQCInDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblQcInDateAndTime As common.Controls.MyLabel
    Friend WithEvents fndGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents lblStatusValue As common.Controls.MyLabel
    Friend WithEvents lblStatus As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents lblGateEntryNO As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblDateAndTime As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndQcNo As common.UserControls.txtNavigator
    Friend WithEvents lblQcNo As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents grpDocType As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkBulkMilkProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkMccProc As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents lblChallanNo As common.Controls.MyLabel
    Friend WithEvents lblChallanDate As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvItem As common.UserControls.MyRadGridView
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpQCOutDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblQCOutDateAndTime As common.Controls.MyLabel
    Friend WithEvents grpParameterDetailBulk As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvParam As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnSendForApproval As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpGateEntryDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents txtWeighmentNo As common.Controls.MyTextBox
    Friend WithEvents dtpChallanDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtChallanNo As common.Controls.MyTextBox
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents fndVendor As common.UserControls.txtFinder
    Friend WithEvents dtpWeighmentDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDipValue As common.Controls.MyTextBox
    Friend WithEvents lblDipValue As common.Controls.MyLabel
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents lblQcAcceptedOrRejected As common.Controls.MyLabel
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuSeparatorItem1 As Telerik.WinControls.UI.RadMenuSeparatorItem
    Friend WithEvents mnuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuEmailSmsSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents TxtDeductionAmount As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents grpEcoPro1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents CboMachine As common.Controls.MyComboBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents BtnStart As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cboComPort As common.Controls.MyComboBox
    Friend WithEvents lblComPort As common.Controls.MyLabel
    Friend WithEvents cboECOPro As common.Controls.MyComboBox
    Friend WithEvents LblSnf As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents LblFAT As common.Controls.MyLabel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BtnRead As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gvManualSeal As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents gvPaperSeal As common.UserControls.MyRadGridView
    Friend WithEvents chkBothDoc As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents GrpControlSample As System.Windows.Forms.GroupBox
    Friend WithEvents txtRcptControlSampleSNF As common.Controls.MyTextBox
    Friend WithEvents txtRcptControlSampleFAT As common.Controls.MyTextBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtDispControlSampleSNF As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtDispControlSampleFAT As common.Controls.MyTextBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtjobworklocation As common.Controls.MyTextBox
    Friend WithEvents lblJobworkLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
End Class

