<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGateOut
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
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndReferenceNo = New common.UserControls.txtFinder()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMilkTransferIn = New common.Controls.MyTextBox()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.lblMilkTransferIn = New common.Controls.MyLabel()
        Me.txtDriver = New common.Controls.MyTextBox()
        Me.lblDriver = New common.Controls.MyLabel()
        Me.fndAllocate = New common.UserControls.txtFinder()
        Me.lblAllocateTo = New common.Controls.MyLabel()
        Me.lblAllocate = New common.Controls.MyLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.chkJobWork = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.fndTankerNo = New common.UserControls.txtFinder()
        Me.dtpStartDateTime = New common.Controls.MyDateTimePicker()
        Me.lblStartDate = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtQCNo = New common.Controls.MyTextBox()
        Me.lblQCNo = New common.Controls.MyLabel()
        Me.txtWeighmentNo = New common.Controls.MyTextBox()
        Me.fndGateEntryNo = New common.UserControls.txtFinder()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnAllocatePrint = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkTransferIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkTransferIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAllocateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAllocate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAllocatePrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(760, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndReferenceNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMilkTransferIn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMilkTransferIn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDriver)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDriver)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndAllocate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAllocateTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAllocate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpStartDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtQCNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblQCNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtWeighmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWeighmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAllocatePrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(836, 296)
        Me.SplitContainer1.SplitterDistance = 253
        Me.SplitContainer1.TabIndex = 2
        '
        'fndReferenceNo
        '
        Me.fndReferenceNo.CalculationExpression = Nothing
        Me.fndReferenceNo.Enabled = False
        Me.fndReferenceNo.FieldCode = Nothing
        Me.fndReferenceNo.FieldDesc = Nothing
        Me.fndReferenceNo.FieldMaxLength = 0
        Me.fndReferenceNo.FieldName = Nothing
        Me.fndReferenceNo.isCalculatedField = False
        Me.fndReferenceNo.IsSourceFromTable = False
        Me.fndReferenceNo.IsSourceFromValueList = False
        Me.fndReferenceNo.IsUnique = False
        Me.fndReferenceNo.Location = New System.Drawing.Point(99, 116)
        Me.fndReferenceNo.MendatroryField = True
        Me.fndReferenceNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndReferenceNo.MyLinkLable1 = Me.lblGateEntryNo
        Me.fndReferenceNo.MyLinkLable2 = Nothing
        Me.fndReferenceNo.MyReadOnly = False
        Me.fndReferenceNo.MyShowMasterFormButton = False
        Me.fndReferenceNo.Name = "fndReferenceNo"
        Me.fndReferenceNo.ReferenceFieldDesc = Nothing
        Me.fndReferenceNo.ReferenceFieldName = Nothing
        Me.fndReferenceNo.ReferenceTableName = Nothing
        Me.fndReferenceNo.Size = New System.Drawing.Size(323, 19)
        Me.fndReferenceNo.TabIndex = 1447
        Me.fndReferenceNo.Value = ""
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGateEntryNo.Location = New System.Drawing.Point(428, 52)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(81, 16)
        Me.lblGateEntryNo.TabIndex = 338
        Me.lblGateEntryNo.Text = "Gate Entry No."
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 116)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel1.TabIndex = 1446
        Me.MyLabel1.Text = "Reference No"
        '
        'txtMilkTransferIn
        '
        Me.txtMilkTransferIn.CalculationExpression = Nothing
        Me.txtMilkTransferIn.FieldCode = Nothing
        Me.txtMilkTransferIn.FieldDesc = Nothing
        Me.txtMilkTransferIn.FieldMaxLength = 0
        Me.txtMilkTransferIn.FieldName = Nothing
        Me.txtMilkTransferIn.isCalculatedField = False
        Me.txtMilkTransferIn.IsSourceFromTable = False
        Me.txtMilkTransferIn.IsSourceFromValueList = False
        Me.txtMilkTransferIn.IsUnique = False
        Me.txtMilkTransferIn.Location = New System.Drawing.Point(514, 97)
        Me.txtMilkTransferIn.MaxLength = 50
        Me.txtMilkTransferIn.MendatroryField = False
        Me.txtMilkTransferIn.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtMilkTransferIn.MyLinkLable2 = Nothing
        Me.txtMilkTransferIn.Name = "txtMilkTransferIn"
        Me.txtMilkTransferIn.ReadOnly = True
        Me.txtMilkTransferIn.ReferenceFieldDesc = Nothing
        Me.txtMilkTransferIn.ReferenceFieldName = Nothing
        Me.txtMilkTransferIn.ReferenceTableName = Nothing
        Me.txtMilkTransferIn.Size = New System.Drawing.Size(310, 20)
        Me.txtMilkTransferIn.TabIndex = 1445
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentNo.Location = New System.Drawing.Point(428, 31)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(81, 16)
        Me.lblWeighmentNo.TabIndex = 339
        Me.lblWeighmentNo.Text = "Weighment No"
        '
        'lblMilkTransferIn
        '
        Me.lblMilkTransferIn.FieldName = Nothing
        Me.lblMilkTransferIn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMilkTransferIn.Location = New System.Drawing.Point(428, 97)
        Me.lblMilkTransferIn.Name = "lblMilkTransferIn"
        Me.lblMilkTransferIn.Size = New System.Drawing.Size(84, 16)
        Me.lblMilkTransferIn.TabIndex = 1444
        Me.lblMilkTransferIn.Text = "Milk Transfer In"
        '
        'txtDriver
        '
        Me.txtDriver.CalculationExpression = Nothing
        Me.txtDriver.FieldCode = Nothing
        Me.txtDriver.FieldDesc = Nothing
        Me.txtDriver.FieldMaxLength = 0
        Me.txtDriver.FieldName = Nothing
        Me.txtDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDriver.isCalculatedField = False
        Me.txtDriver.IsSourceFromTable = False
        Me.txtDriver.IsSourceFromValueList = False
        Me.txtDriver.IsUnique = False
        Me.txtDriver.Location = New System.Drawing.Point(99, 95)
        Me.txtDriver.MaxLength = 30
        Me.txtDriver.MendatroryField = False
        Me.txtDriver.MyLinkLable1 = Nothing
        Me.txtDriver.MyLinkLable2 = Nothing
        Me.txtDriver.Name = "txtDriver"
        Me.txtDriver.ReferenceFieldDesc = Nothing
        Me.txtDriver.ReferenceFieldName = Nothing
        Me.txtDriver.ReferenceTableName = Nothing
        Me.txtDriver.Size = New System.Drawing.Size(323, 18)
        Me.txtDriver.TabIndex = 1443
        '
        'lblDriver
        '
        Me.lblDriver.FieldName = Nothing
        Me.lblDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriver.Location = New System.Drawing.Point(6, 97)
        Me.lblDriver.Name = "lblDriver"
        Me.lblDriver.Size = New System.Drawing.Size(36, 16)
        Me.lblDriver.TabIndex = 362
        Me.lblDriver.Text = "Driver"
        '
        'fndAllocate
        '
        Me.fndAllocate.CalculationExpression = Nothing
        Me.fndAllocate.FieldCode = Nothing
        Me.fndAllocate.FieldDesc = Nothing
        Me.fndAllocate.FieldMaxLength = 0
        Me.fndAllocate.FieldName = Nothing
        Me.fndAllocate.isCalculatedField = False
        Me.fndAllocate.IsSourceFromTable = False
        Me.fndAllocate.IsSourceFromValueList = False
        Me.fndAllocate.IsUnique = False
        Me.fndAllocate.Location = New System.Drawing.Point(99, 74)
        Me.fndAllocate.MendatroryField = False
        Me.fndAllocate.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndAllocate.MyLinkLable1 = Me.lblGateEntryNo
        Me.fndAllocate.MyLinkLable2 = Nothing
        Me.fndAllocate.MyReadOnly = False
        Me.fndAllocate.MyShowMasterFormButton = False
        Me.fndAllocate.Name = "fndAllocate"
        Me.fndAllocate.ReferenceFieldDesc = Nothing
        Me.fndAllocate.ReferenceFieldName = Nothing
        Me.fndAllocate.ReferenceTableName = Nothing
        Me.fndAllocate.Size = New System.Drawing.Size(323, 19)
        Me.fndAllocate.TabIndex = 361
        Me.fndAllocate.Value = ""
        '
        'lblAllocateTo
        '
        Me.lblAllocateTo.AutoSize = False
        Me.lblAllocateTo.BorderVisible = True
        Me.lblAllocateTo.FieldName = Nothing
        Me.lblAllocateTo.Location = New System.Drawing.Point(428, 74)
        Me.lblAllocateTo.Name = "lblAllocateTo"
        Me.lblAllocateTo.Size = New System.Drawing.Size(396, 19)
        Me.lblAllocateTo.TabIndex = 360
        '
        'lblAllocate
        '
        Me.lblAllocate.FieldName = Nothing
        Me.lblAllocate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllocate.Location = New System.Drawing.Point(6, 75)
        Me.lblAllocate.Name = "lblAllocate"
        Me.lblAllocate.Size = New System.Drawing.Size(66, 16)
        Me.lblAllocate.TabIndex = 358
        Me.lblAllocate.Text = "Allocate To."
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblSubLocation)
        Me.Panel3.Controls.Add(Me.chkJobWork)
        Me.Panel3.Controls.Add(Me.MyLabel16)
        Me.Panel3.Controls.Add(Me.txtSubLocation)
        Me.Panel3.Location = New System.Drawing.Point(3, 142)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(780, 31)
        Me.Panel3.TabIndex = 357
        Me.Panel3.Visible = False
        '
        'lblSubLocation
        '
        Me.lblSubLocation.AutoSize = False
        Me.lblSubLocation.BorderVisible = True
        Me.lblSubLocation.FieldName = Nothing
        Me.lblSubLocation.Location = New System.Drawing.Point(228, 4)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(180, 19)
        Me.lblSubLocation.TabIndex = 276
        '
        'chkJobWork
        '
        Me.chkJobWork.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWork.Location = New System.Drawing.Point(424, 4)
        Me.chkJobWork.Name = "chkJobWork"
        Me.chkJobWork.Size = New System.Drawing.Size(80, 16)
        Me.chkJobWork.TabIndex = 346
        Me.chkJobWork.Text = "Is Job Work"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(3, 4)
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
        Me.txtSubLocation.Location = New System.Drawing.Point(96, 4)
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
        Me.fndTankerNo.Location = New System.Drawing.Point(99, 31)
        Me.fndTankerNo.MendatroryField = True
        Me.fndTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTankerNo.MyLinkLable1 = Me.lblGateEntryNo
        Me.fndTankerNo.MyLinkLable2 = Nothing
        Me.fndTankerNo.MyReadOnly = False
        Me.fndTankerNo.MyShowMasterFormButton = False
        Me.fndTankerNo.Name = "fndTankerNo"
        Me.fndTankerNo.ReferenceFieldDesc = Nothing
        Me.fndTankerNo.ReferenceFieldName = Nothing
        Me.fndTankerNo.ReferenceTableName = Nothing
        Me.fndTankerNo.Size = New System.Drawing.Size(323, 19)
        Me.fndTankerNo.TabIndex = 344
        Me.fndTankerNo.Value = ""
        '
        'dtpStartDateTime
        '
        Me.dtpStartDateTime.CalculationExpression = Nothing
        Me.dtpStartDateTime.CustomFormat = "dd/MM/yyyy  hh:mm:ss tt"
        Me.dtpStartDateTime.FieldCode = Nothing
        Me.dtpStartDateTime.FieldDesc = Nothing
        Me.dtpStartDateTime.FieldMaxLength = 0
        Me.dtpStartDateTime.FieldName = Nothing
        Me.dtpStartDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDateTime.isCalculatedField = False
        Me.dtpStartDateTime.IsSourceFromTable = False
        Me.dtpStartDateTime.IsSourceFromValueList = False
        Me.dtpStartDateTime.IsUnique = False
        Me.dtpStartDateTime.Location = New System.Drawing.Point(515, 10)
        Me.dtpStartDateTime.MendatroryField = False
        Me.dtpStartDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDateTime.MyLinkLable1 = Me.lblStartDate
        Me.dtpStartDateTime.MyLinkLable2 = Nothing
        Me.dtpStartDateTime.Name = "dtpStartDateTime"
        Me.dtpStartDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDateTime.ReferenceFieldDesc = Nothing
        Me.dtpStartDateTime.ReferenceFieldName = Nothing
        Me.dtpStartDateTime.ReferenceTableName = Nothing
        Me.dtpStartDateTime.Size = New System.Drawing.Size(309, 20)
        Me.dtpStartDateTime.TabIndex = 342
        Me.dtpStartDateTime.TabStop = False
        Me.dtpStartDateTime.Text = "10/06/2011  11:51:56 AM"
        Me.dtpStartDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblStartDate
        '
        Me.lblStartDate.FieldName = Nothing
        Me.lblStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(428, 10)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(79, 16)
        Me.lblStartDate.TabIndex = 343
        Me.lblStartDate.Text = "Gate Out Date"
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(6, 31)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(62, 16)
        Me.lblTankerNo.TabIndex = 341
        Me.lblTankerNo.Text = "Tanker No."
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
        Me.txtQCNo.Location = New System.Drawing.Point(99, 52)
        Me.txtQCNo.MaxLength = 50
        Me.txtQCNo.MendatroryField = False
        Me.txtQCNo.MyLinkLable1 = Me.lblQCNo
        Me.txtQCNo.MyLinkLable2 = Nothing
        Me.txtQCNo.Name = "txtQCNo"
        Me.txtQCNo.ReadOnly = True
        Me.txtQCNo.ReferenceFieldDesc = Nothing
        Me.txtQCNo.ReferenceFieldName = Nothing
        Me.txtQCNo.ReferenceTableName = Nothing
        Me.txtQCNo.Size = New System.Drawing.Size(323, 20)
        Me.txtQCNo.TabIndex = 334
        '
        'lblQCNo
        '
        Me.lblQCNo.FieldName = Nothing
        Me.lblQCNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCNo.Location = New System.Drawing.Point(6, 52)
        Me.lblQCNo.Name = "lblQCNo"
        Me.lblQCNo.Size = New System.Drawing.Size(44, 16)
        Me.lblQCNo.TabIndex = 340
        Me.lblQCNo.Text = "QC No."
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
        Me.txtWeighmentNo.Location = New System.Drawing.Point(515, 31)
        Me.txtWeighmentNo.MaxLength = 50
        Me.txtWeighmentNo.MendatroryField = False
        Me.txtWeighmentNo.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtWeighmentNo.MyLinkLable2 = Nothing
        Me.txtWeighmentNo.Name = "txtWeighmentNo"
        Me.txtWeighmentNo.ReadOnly = True
        Me.txtWeighmentNo.ReferenceFieldDesc = Nothing
        Me.txtWeighmentNo.ReferenceFieldName = Nothing
        Me.txtWeighmentNo.ReferenceTableName = Nothing
        Me.txtWeighmentNo.Size = New System.Drawing.Size(310, 20)
        Me.txtWeighmentNo.TabIndex = 333
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
        Me.fndGateEntryNo.Location = New System.Drawing.Point(515, 52)
        Me.fndGateEntryNo.MendatroryField = True
        Me.fndGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNo.MyLinkLable1 = Me.lblGateEntryNo
        Me.fndGateEntryNo.MyLinkLable2 = Nothing
        Me.fndGateEntryNo.MyReadOnly = False
        Me.fndGateEntryNo.MyShowMasterFormButton = False
        Me.fndGateEntryNo.Name = "fndGateEntryNo"
        Me.fndGateEntryNo.ReferenceFieldDesc = Nothing
        Me.fndGateEntryNo.ReferenceFieldName = Nothing
        Me.fndGateEntryNo.ReferenceTableName = Nothing
        Me.fndGateEntryNo.Size = New System.Drawing.Size(310, 19)
        Me.fndGateEntryNo.TabIndex = 332
        Me.fndGateEntryNo.Value = ""
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(99, 10)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 30
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(305, 18)
        Me.fndDocNo.TabIndex = 331
        Me.fndDocNo.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(6, 10)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 337
        Me.lblDocNo.Text = "Document No."
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(405, 10)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 336
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(231, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 8
        Me.btnPost.Text = "Post"
        '
        'btnAllocatePrint
        '
        Me.btnAllocatePrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAllocatePrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllocatePrint.Location = New System.Drawing.Point(305, 11)
        Me.btnAllocatePrint.Name = "btnAllocatePrint"
        Me.btnAllocatePrint.Size = New System.Drawing.Size(89, 18)
        Me.btnAllocatePrint.TabIndex = 7
        Me.btnAllocatePrint.Text = "Allocate Print"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(156, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "Print"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(12, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(84, 11)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'FrmGateOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(836, 296)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmGateOut"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Gate Out ( Bulk Milk Proc)"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkTransferIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkTransferIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAllocateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAllocate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAllocatePrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents txtQCNo As common.Controls.MyTextBox
    Friend WithEvents lblQCNo As common.Controls.MyLabel
    Friend WithEvents txtWeighmentNo As common.Controls.MyTextBox
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents fndGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpStartDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblStartDate As common.Controls.MyLabel
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents chkJobWork As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents lblAllocate As common.Controls.MyLabel
    Friend WithEvents lblAllocateTo As common.Controls.MyLabel
    Friend WithEvents fndAllocate As common.UserControls.txtFinder
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAllocatePrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDriver As common.Controls.MyLabel
    Friend WithEvents txtDriver As common.Controls.MyTextBox
    Friend WithEvents lblMilkTransferIn As common.Controls.MyLabel
    Friend WithEvents txtMilkTransferIn As common.Controls.MyTextBox
    Friend WithEvents fndReferenceNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

