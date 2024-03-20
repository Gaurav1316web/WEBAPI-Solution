<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCleaning
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
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndRefrenceNo = New common.UserControls.txtFinder()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.chkJobWork = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtInTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtOutTime = New common.Controls.MyDateTimePicker()
        Me.dtpGateEntryDateTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.dtpQCDateTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.dtpWeighmentDateTime = New common.Controls.MyDateTimePicker()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpUnloadingDateTime = New common.Controls.MyDateTimePicker()
        Me.lblQCOutDateAndTime = New common.Controls.MyLabel()
        Me.txtUnloadingNo = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.fndTankerNo = New common.UserControls.txtFinder()
        Me.TxtRemarks = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.lblStatus = New common.Controls.MyLabel()
        Me.ddlStatus = New common.Controls.MyComboBox()
        Me.lblCheckedByName = New common.Controls.MyLabel()
        Me.lblCheckedBy = New common.Controls.MyLabel()
        Me.fndCheckedBy = New common.UserControls.txtFinder()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtQCNo = New common.Controls.MyTextBox()
        Me.lblQCNo = New common.Controls.MyLabel()
        Me.txtWeighmentNo = New common.Controls.MyTextBox()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.fndGateEntryNo = New common.UserControls.txtFinder()
        Me.dtpEndDateTime = New common.Controls.MyDateTimePicker()
        Me.lblEndDate = New common.Controls.MyLabel()
        Me.lblDoneByName = New common.Controls.MyLabel()
        Me.lblCleaningDoneBy = New common.Controls.MyLabel()
        Me.fndCleaningDoneBy = New common.UserControls.txtFinder()
        Me.dtpStartDateTime = New common.Controls.MyDateTimePicker()
        Me.lblStartDate = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOutTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpQCDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpWeighmentDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpUnloadingDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUnloadingNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDoneByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCleaningDoneBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(216, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 24)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1036, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(145, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 24)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndRefrenceNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpGateEntryDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpQCDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpWeighmentDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpUnloadingDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblQCOutDateAndTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtUnloadingNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCheckedByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCheckedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCheckedBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtQCNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblQCNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtWeighmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWeighmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpEndDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEndDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDoneByName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCleaningDoneBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndCleaningDoneBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpStartDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReverse)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1107, 487)
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 2
        '
        'fndRefrenceNo
        '
        Me.fndRefrenceNo.CalculationExpression = Nothing
        Me.fndRefrenceNo.Enabled = False
        Me.fndRefrenceNo.FieldCode = Nothing
        Me.fndRefrenceNo.FieldDesc = Nothing
        Me.fndRefrenceNo.FieldMaxLength = 0
        Me.fndRefrenceNo.FieldName = Nothing
        Me.fndRefrenceNo.isCalculatedField = False
        Me.fndRefrenceNo.IsSourceFromTable = False
        Me.fndRefrenceNo.IsSourceFromValueList = False
        Me.fndRefrenceNo.IsUnique = False
        Me.fndRefrenceNo.Location = New System.Drawing.Point(151, 335)
        Me.fndRefrenceNo.MendatroryField = True
        Me.fndRefrenceNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRefrenceNo.MyLinkLable1 = Me.lblGateEntryNo
        Me.fndRefrenceNo.MyLinkLable2 = Nothing
        Me.fndRefrenceNo.MyReadOnly = False
        Me.fndRefrenceNo.MyShowMasterFormButton = False
        Me.fndRefrenceNo.Name = "fndRefrenceNo"
        Me.fndRefrenceNo.ReferenceFieldDesc = Nothing
        Me.fndRefrenceNo.ReferenceFieldName = Nothing
        Me.fndRefrenceNo.ReferenceTableName = Nothing
        Me.fndRefrenceNo.Size = New System.Drawing.Size(234, 19)
        Me.fndRefrenceNo.TabIndex = 363
        Me.fndRefrenceNo.Value = ""
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGateEntryNo.Location = New System.Drawing.Point(12, 106)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(81, 16)
        Me.lblGateEntryNo.TabIndex = 324
        Me.lblGateEntryNo.Text = "Gate Entry No."
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 336)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel5.TabIndex = 362
        Me.MyLabel5.Text = "Reference No"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(508, 58)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(180, 19)
        Me.lblVendorName.TabIndex = 361
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(399, 61)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(76, 16)
        Me.MyLabel9.TabIndex = 360
        Me.MyLabel9.Text = "Vendor Name"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblSubLocation)
        Me.Panel3.Controls.Add(Me.chkJobWork)
        Me.Panel3.Controls.Add(Me.MyLabel16)
        Me.Panel3.Controls.Add(Me.txtSubLocation)
        Me.Panel3.Location = New System.Drawing.Point(693, 89)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(396, 44)
        Me.Panel3.TabIndex = 359
        Me.Panel3.Visible = False
        '
        'lblSubLocation
        '
        Me.lblSubLocation.AutoSize = False
        Me.lblSubLocation.BorderVisible = True
        Me.lblSubLocation.FieldName = Nothing
        Me.lblSubLocation.Location = New System.Drawing.Point(213, 21)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(180, 19)
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel3)
        Me.Panel1.Controls.Add(Me.txtInTime)
        Me.Panel1.Controls.Add(Me.MyLabel4)
        Me.Panel1.Controls.Add(Me.txtOutTime)
        Me.Panel1.Location = New System.Drawing.Point(694, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(218, 50)
        Me.Panel1.TabIndex = 358
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 8)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel3.TabIndex = 357
        Me.MyLabel3.Text = "In Time"
        '
        'txtInTime
        '
        Me.txtInTime.CalculationExpression = Nothing
        Me.txtInTime.CustomFormat = " hh:mm:ss tt"
        Me.txtInTime.FieldCode = Nothing
        Me.txtInTime.FieldDesc = Nothing
        Me.txtInTime.FieldMaxLength = 0
        Me.txtInTime.FieldName = Nothing
        Me.txtInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInTime.isCalculatedField = False
        Me.txtInTime.IsSourceFromTable = False
        Me.txtInTime.IsSourceFromValueList = False
        Me.txtInTime.IsUnique = False
        Me.txtInTime.Location = New System.Drawing.Point(67, 4)
        Me.txtInTime.MendatroryField = False
        Me.txtInTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInTime.MyLinkLable1 = Nothing
        Me.txtInTime.MyLinkLable2 = Nothing
        Me.txtInTime.Name = "txtInTime"
        Me.txtInTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInTime.ReferenceFieldDesc = Nothing
        Me.txtInTime.ReferenceFieldName = Nothing
        Me.txtInTime.ReferenceTableName = Nothing
        Me.txtInTime.Size = New System.Drawing.Size(135, 20)
        Me.txtInTime.TabIndex = 356
        Me.txtInTime.TabStop = False
        Me.txtInTime.Text = " 11:51:56 AM"
        Me.txtInTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(3, 30)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel4.TabIndex = 355
        Me.MyLabel4.Text = "Out time"
        '
        'txtOutTime
        '
        Me.txtOutTime.CalculationExpression = Nothing
        Me.txtOutTime.CustomFormat = " hh:mm:ss tt"
        Me.txtOutTime.FieldCode = Nothing
        Me.txtOutTime.FieldDesc = Nothing
        Me.txtOutTime.FieldMaxLength = 0
        Me.txtOutTime.FieldName = Nothing
        Me.txtOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtOutTime.isCalculatedField = False
        Me.txtOutTime.IsSourceFromTable = False
        Me.txtOutTime.IsSourceFromValueList = False
        Me.txtOutTime.IsUnique = False
        Me.txtOutTime.Location = New System.Drawing.Point(67, 26)
        Me.txtOutTime.MendatroryField = False
        Me.txtOutTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtOutTime.MyLinkLable1 = Nothing
        Me.txtOutTime.MyLinkLable2 = Nothing
        Me.txtOutTime.Name = "txtOutTime"
        Me.txtOutTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtOutTime.ReferenceFieldDesc = Nothing
        Me.txtOutTime.ReferenceFieldName = Nothing
        Me.txtOutTime.ReferenceTableName = Nothing
        Me.txtOutTime.Size = New System.Drawing.Size(135, 20)
        Me.txtOutTime.TabIndex = 354
        Me.txtOutTime.TabStop = False
        Me.txtOutTime.Text = " 11:51:56 AM"
        Me.txtOutTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'dtpGateEntryDateTime
        '
        Me.dtpGateEntryDateTime.CalculationExpression = Nothing
        Me.dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpGateEntryDateTime.FieldCode = Nothing
        Me.dtpGateEntryDateTime.FieldDesc = Nothing
        Me.dtpGateEntryDateTime.FieldMaxLength = 0
        Me.dtpGateEntryDateTime.FieldName = Nothing
        Me.dtpGateEntryDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpGateEntryDateTime.isCalculatedField = False
        Me.dtpGateEntryDateTime.IsSourceFromTable = False
        Me.dtpGateEntryDateTime.IsSourceFromValueList = False
        Me.dtpGateEntryDateTime.IsUnique = False
        Me.dtpGateEntryDateTime.Location = New System.Drawing.Point(510, 101)
        Me.dtpGateEntryDateTime.MendatroryField = False
        Me.dtpGateEntryDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.MyLinkLable1 = Nothing
        Me.dtpGateEntryDateTime.MyLinkLable2 = Nothing
        Me.dtpGateEntryDateTime.Name = "dtpGateEntryDateTime"
        Me.dtpGateEntryDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpGateEntryDateTime.ReferenceFieldDesc = Nothing
        Me.dtpGateEntryDateTime.ReferenceFieldName = Nothing
        Me.dtpGateEntryDateTime.ReferenceTableName = Nothing
        Me.dtpGateEntryDateTime.Size = New System.Drawing.Size(150, 20)
        Me.dtpGateEntryDateTime.TabIndex = 352
        Me.dtpGateEntryDateTime.TabStop = False
        Me.dtpGateEntryDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpGateEntryDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(399, 104)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel8.TabIndex = 353
        Me.MyLabel8.Text = "Gate Entry Date "
        '
        'dtpQCDateTime
        '
        Me.dtpQCDateTime.CalculationExpression = Nothing
        Me.dtpQCDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpQCDateTime.FieldCode = Nothing
        Me.dtpQCDateTime.FieldDesc = Nothing
        Me.dtpQCDateTime.FieldMaxLength = 0
        Me.dtpQCDateTime.FieldName = Nothing
        Me.dtpQCDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQCDateTime.isCalculatedField = False
        Me.dtpQCDateTime.IsSourceFromTable = False
        Me.dtpQCDateTime.IsSourceFromValueList = False
        Me.dtpQCDateTime.IsUnique = False
        Me.dtpQCDateTime.Location = New System.Drawing.Point(510, 123)
        Me.dtpQCDateTime.MendatroryField = False
        Me.dtpQCDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCDateTime.MyLinkLable1 = Nothing
        Me.dtpQCDateTime.MyLinkLable2 = Nothing
        Me.dtpQCDateTime.Name = "dtpQCDateTime"
        Me.dtpQCDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpQCDateTime.ReferenceFieldDesc = Nothing
        Me.dtpQCDateTime.ReferenceFieldName = Nothing
        Me.dtpQCDateTime.ReferenceTableName = Nothing
        Me.dtpQCDateTime.Size = New System.Drawing.Size(150, 20)
        Me.dtpQCDateTime.TabIndex = 350
        Me.dtpQCDateTime.TabStop = False
        Me.dtpQCDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpQCDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(399, 126)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel7.TabIndex = 351
        Me.MyLabel7.Text = "QC Date "
        '
        'dtpWeighmentDateTime
        '
        Me.dtpWeighmentDateTime.CalculationExpression = Nothing
        Me.dtpWeighmentDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpWeighmentDateTime.FieldCode = Nothing
        Me.dtpWeighmentDateTime.FieldDesc = Nothing
        Me.dtpWeighmentDateTime.FieldMaxLength = 0
        Me.dtpWeighmentDateTime.FieldName = Nothing
        Me.dtpWeighmentDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWeighmentDateTime.isCalculatedField = False
        Me.dtpWeighmentDateTime.IsSourceFromTable = False
        Me.dtpWeighmentDateTime.IsSourceFromValueList = False
        Me.dtpWeighmentDateTime.IsUnique = False
        Me.dtpWeighmentDateTime.Location = New System.Drawing.Point(510, 79)
        Me.dtpWeighmentDateTime.MendatroryField = False
        Me.dtpWeighmentDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateTime.MyLinkLable1 = Nothing
        Me.dtpWeighmentDateTime.MyLinkLable2 = Nothing
        Me.dtpWeighmentDateTime.Name = "dtpWeighmentDateTime"
        Me.dtpWeighmentDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpWeighmentDateTime.ReferenceFieldDesc = Nothing
        Me.dtpWeighmentDateTime.ReferenceFieldName = Nothing
        Me.dtpWeighmentDateTime.ReferenceTableName = Nothing
        Me.dtpWeighmentDateTime.Size = New System.Drawing.Size(149, 20)
        Me.dtpWeighmentDateTime.TabIndex = 348
        Me.dtpWeighmentDateTime.TabStop = False
        Me.dtpWeighmentDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpWeighmentDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(399, 82)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel6.TabIndex = 349
        Me.MyLabel6.Text = "Weighment Date"
        '
        'dtpUnloadingDateTime
        '
        Me.dtpUnloadingDateTime.CalculationExpression = Nothing
        Me.dtpUnloadingDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpUnloadingDateTime.FieldCode = Nothing
        Me.dtpUnloadingDateTime.FieldDesc = Nothing
        Me.dtpUnloadingDateTime.FieldMaxLength = 0
        Me.dtpUnloadingDateTime.FieldName = Nothing
        Me.dtpUnloadingDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUnloadingDateTime.isCalculatedField = False
        Me.dtpUnloadingDateTime.IsSourceFromTable = False
        Me.dtpUnloadingDateTime.IsSourceFromValueList = False
        Me.dtpUnloadingDateTime.IsUnique = False
        Me.dtpUnloadingDateTime.Location = New System.Drawing.Point(510, 145)
        Me.dtpUnloadingDateTime.MendatroryField = False
        Me.dtpUnloadingDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpUnloadingDateTime.MyLinkLable1 = Nothing
        Me.dtpUnloadingDateTime.MyLinkLable2 = Nothing
        Me.dtpUnloadingDateTime.Name = "dtpUnloadingDateTime"
        Me.dtpUnloadingDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpUnloadingDateTime.ReferenceFieldDesc = Nothing
        Me.dtpUnloadingDateTime.ReferenceFieldName = Nothing
        Me.dtpUnloadingDateTime.ReferenceTableName = Nothing
        Me.dtpUnloadingDateTime.Size = New System.Drawing.Size(150, 20)
        Me.dtpUnloadingDateTime.TabIndex = 346
        Me.dtpUnloadingDateTime.TabStop = False
        Me.dtpUnloadingDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpUnloadingDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblQCOutDateAndTime
        '
        Me.lblQCOutDateAndTime.FieldName = Nothing
        Me.lblQCOutDateAndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCOutDateAndTime.Location = New System.Drawing.Point(399, 148)
        Me.lblQCOutDateAndTime.Name = "lblQCOutDateAndTime"
        Me.lblQCOutDateAndTime.Size = New System.Drawing.Size(87, 16)
        Me.lblQCOutDateAndTime.TabIndex = 347
        Me.lblQCOutDateAndTime.Text = "Unloading Date "
        '
        'txtUnloadingNo
        '
        Me.txtUnloadingNo.CalculationExpression = Nothing
        Me.txtUnloadingNo.FieldCode = Nothing
        Me.txtUnloadingNo.FieldDesc = Nothing
        Me.txtUnloadingNo.FieldMaxLength = 0
        Me.txtUnloadingNo.FieldName = Nothing
        Me.txtUnloadingNo.isCalculatedField = False
        Me.txtUnloadingNo.IsSourceFromTable = False
        Me.txtUnloadingNo.IsSourceFromValueList = False
        Me.txtUnloadingNo.IsUnique = False
        Me.txtUnloadingNo.Location = New System.Drawing.Point(151, 146)
        Me.txtUnloadingNo.MaxLength = 50
        Me.txtUnloadingNo.MendatroryField = False
        Me.txtUnloadingNo.MyLinkLable1 = Me.MyLabel2
        Me.txtUnloadingNo.MyLinkLable2 = Nothing
        Me.txtUnloadingNo.Name = "txtUnloadingNo"
        Me.txtUnloadingNo.ReadOnly = True
        Me.txtUnloadingNo.ReferenceFieldDesc = Nothing
        Me.txtUnloadingNo.ReferenceFieldName = Nothing
        Me.txtUnloadingNo.ReferenceTableName = Nothing
        Me.txtUnloadingNo.Size = New System.Drawing.Size(233, 20)
        Me.txtUnloadingNo.TabIndex = 344
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 149)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel2.TabIndex = 345
        Me.MyLabel2.Text = "Unloading No"
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
        Me.fndTankerNo.Location = New System.Drawing.Point(151, 58)
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
        Me.fndTankerNo.Size = New System.Drawing.Size(231, 19)
        Me.fndTankerNo.TabIndex = 343
        Me.fndTankerNo.Value = ""
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AutoSize = False
        Me.TxtRemarks.CalculationExpression = Nothing
        Me.TxtRemarks.FieldCode = Nothing
        Me.TxtRemarks.FieldDesc = Nothing
        Me.TxtRemarks.FieldMaxLength = 0
        Me.TxtRemarks.FieldName = Nothing
        Me.TxtRemarks.isCalculatedField = False
        Me.TxtRemarks.IsSourceFromTable = False
        Me.TxtRemarks.IsSourceFromValueList = False
        Me.TxtRemarks.IsUnique = False
        Me.TxtRemarks.Location = New System.Drawing.Point(151, 235)
        Me.TxtRemarks.MaxLength = 50
        Me.TxtRemarks.MendatroryField = False
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.MyLinkLable1 = Me.MyLabel1
        Me.TxtRemarks.MyLinkLable2 = Nothing
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.ReferenceFieldDesc = Nothing
        Me.TxtRemarks.ReferenceFieldName = Nothing
        Me.TxtRemarks.ReferenceTableName = Nothing
        Me.TxtRemarks.Size = New System.Drawing.Size(618, 96)
        Me.TxtRemarks.TabIndex = 341
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 238)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel1.TabIndex = 342
        Me.MyLabel1.Text = "Remarks"
        '
        'lblPending
        '
        Me.lblPending.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(694, 7)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(97, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 340
        '
        'lblStatus
        '
        Me.lblStatus.FieldName = Nothing
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(12, 235)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(38, 16)
        Me.lblStatus.TabIndex = 335
        Me.lblStatus.Text = "Status"
        '
        'ddlStatus
        '
        Me.ddlStatus.AutoCompleteDisplayMember = Nothing
        Me.ddlStatus.AutoCompleteValueMember = Nothing
        Me.ddlStatus.CalculationExpression = Nothing
        Me.ddlStatus.DropDownAnimationEnabled = True
        Me.ddlStatus.FieldCode = Nothing
        Me.ddlStatus.FieldDesc = Nothing
        Me.ddlStatus.FieldMaxLength = 0
        Me.ddlStatus.FieldName = Nothing
        Me.ddlStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlStatus.isCalculatedField = False
        Me.ddlStatus.IsSourceFromTable = False
        Me.ddlStatus.IsSourceFromValueList = False
        Me.ddlStatus.IsUnique = False
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.ddlStatus.Items.Add(RadListDataItem1)
        Me.ddlStatus.Items.Add(RadListDataItem2)
        Me.ddlStatus.Location = New System.Drawing.Point(151, 213)
        Me.ddlStatus.MendatroryField = True
        Me.ddlStatus.MyLinkLable1 = Me.lblStatus
        Me.ddlStatus.MyLinkLable2 = Nothing
        Me.ddlStatus.Name = "ddlStatus"
        Me.ddlStatus.ReferenceFieldDesc = Nothing
        Me.ddlStatus.ReferenceFieldName = Nothing
        Me.ddlStatus.ReferenceTableName = Nothing
        Me.ddlStatus.Size = New System.Drawing.Size(234, 18)
        Me.ddlStatus.TabIndex = 11
        '
        'lblCheckedByName
        '
        Me.lblCheckedByName.AutoSize = False
        Me.lblCheckedByName.BorderVisible = True
        Me.lblCheckedByName.FieldName = Nothing
        Me.lblCheckedByName.Location = New System.Drawing.Point(403, 192)
        Me.lblCheckedByName.Name = "lblCheckedByName"
        Me.lblCheckedByName.Size = New System.Drawing.Size(290, 19)
        Me.lblCheckedByName.TabIndex = 333
        '
        'lblCheckedBy
        '
        Me.lblCheckedBy.FieldName = Nothing
        Me.lblCheckedBy.Location = New System.Drawing.Point(12, 192)
        Me.lblCheckedBy.Name = "lblCheckedBy"
        Me.lblCheckedBy.Size = New System.Drawing.Size(64, 18)
        Me.lblCheckedBy.TabIndex = 332
        Me.lblCheckedBy.Text = "Checked By"
        '
        'fndCheckedBy
        '
        Me.fndCheckedBy.CalculationExpression = Nothing
        Me.fndCheckedBy.FieldCode = Nothing
        Me.fndCheckedBy.FieldDesc = Nothing
        Me.fndCheckedBy.FieldMaxLength = 0
        Me.fndCheckedBy.FieldName = Nothing
        Me.fndCheckedBy.isCalculatedField = False
        Me.fndCheckedBy.IsSourceFromTable = False
        Me.fndCheckedBy.IsSourceFromValueList = False
        Me.fndCheckedBy.IsUnique = False
        Me.fndCheckedBy.Location = New System.Drawing.Point(151, 191)
        Me.fndCheckedBy.MendatroryField = True
        Me.fndCheckedBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCheckedBy.MyLinkLable1 = Me.lblCheckedBy
        Me.fndCheckedBy.MyLinkLable2 = Me.lblCheckedByName
        Me.fndCheckedBy.MyReadOnly = False
        Me.fndCheckedBy.MyShowMasterFormButton = False
        Me.fndCheckedBy.Name = "fndCheckedBy"
        Me.fndCheckedBy.ReferenceFieldDesc = Nothing
        Me.fndCheckedBy.ReferenceFieldName = Nothing
        Me.fndCheckedBy.ReferenceTableName = Nothing
        Me.fndCheckedBy.Size = New System.Drawing.Size(234, 19)
        Me.fndCheckedBy.TabIndex = 10
        Me.fndCheckedBy.Value = ""
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(12, 61)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(62, 16)
        Me.lblTankerNo.TabIndex = 330
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
        Me.txtQCNo.Location = New System.Drawing.Point(151, 124)
        Me.txtQCNo.MaxLength = 50
        Me.txtQCNo.MendatroryField = False
        Me.txtQCNo.MyLinkLable1 = Me.lblQCNo
        Me.txtQCNo.MyLinkLable2 = Nothing
        Me.txtQCNo.Name = "txtQCNo"
        Me.txtQCNo.ReadOnly = True
        Me.txtQCNo.ReferenceFieldDesc = Nothing
        Me.txtQCNo.ReferenceFieldName = Nothing
        Me.txtQCNo.ReferenceTableName = Nothing
        Me.txtQCNo.Size = New System.Drawing.Size(233, 20)
        Me.txtQCNo.TabIndex = 5
        '
        'lblQCNo
        '
        Me.lblQCNo.FieldName = Nothing
        Me.lblQCNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCNo.Location = New System.Drawing.Point(12, 127)
        Me.lblQCNo.Name = "lblQCNo"
        Me.lblQCNo.Size = New System.Drawing.Size(44, 16)
        Me.lblQCNo.TabIndex = 329
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
        Me.txtWeighmentNo.Location = New System.Drawing.Point(151, 80)
        Me.txtWeighmentNo.MaxLength = 50
        Me.txtWeighmentNo.MendatroryField = False
        Me.txtWeighmentNo.MyLinkLable1 = Me.lblWeighmentNo
        Me.txtWeighmentNo.MyLinkLable2 = Nothing
        Me.txtWeighmentNo.Name = "txtWeighmentNo"
        Me.txtWeighmentNo.ReadOnly = True
        Me.txtWeighmentNo.ReferenceFieldDesc = Nothing
        Me.txtWeighmentNo.ReferenceFieldName = Nothing
        Me.txtWeighmentNo.ReferenceTableName = Nothing
        Me.txtWeighmentNo.Size = New System.Drawing.Size(232, 20)
        Me.txtWeighmentNo.TabIndex = 4
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentNo.Location = New System.Drawing.Point(12, 84)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(81, 16)
        Me.lblWeighmentNo.TabIndex = 328
        Me.lblWeighmentNo.Text = "Weighment No"
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
        Me.fndGateEntryNo.Location = New System.Drawing.Point(151, 103)
        Me.fndGateEntryNo.MendatroryField = True
        Me.fndGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateEntryNo.MyLinkLable1 = Me.lblGateEntryNo
        Me.fndGateEntryNo.MyLinkLable2 = Nothing
        Me.fndGateEntryNo.MyReadOnly = True
        Me.fndGateEntryNo.MyShowMasterFormButton = False
        Me.fndGateEntryNo.Name = "fndGateEntryNo"
        Me.fndGateEntryNo.ReferenceFieldDesc = Nothing
        Me.fndGateEntryNo.ReferenceFieldName = Nothing
        Me.fndGateEntryNo.ReferenceTableName = Nothing
        Me.fndGateEntryNo.Size = New System.Drawing.Size(232, 19)
        Me.fndGateEntryNo.TabIndex = 3
        Me.fndGateEntryNo.Value = ""
        '
        'dtpEndDateTime
        '
        Me.dtpEndDateTime.CalculationExpression = Nothing
        Me.dtpEndDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpEndDateTime.FieldCode = Nothing
        Me.dtpEndDateTime.FieldDesc = Nothing
        Me.dtpEndDateTime.FieldMaxLength = 0
        Me.dtpEndDateTime.FieldName = Nothing
        Me.dtpEndDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDateTime.isCalculatedField = False
        Me.dtpEndDateTime.IsSourceFromTable = False
        Me.dtpEndDateTime.IsSourceFromValueList = False
        Me.dtpEndDateTime.IsUnique = False
        Me.dtpEndDateTime.Location = New System.Drawing.Point(508, 33)
        Me.dtpEndDateTime.MendatroryField = False
        Me.dtpEndDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDateTime.MyLinkLable1 = Me.lblEndDate
        Me.dtpEndDateTime.MyLinkLable2 = Nothing
        Me.dtpEndDateTime.Name = "dtpEndDateTime"
        Me.dtpEndDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDateTime.ReferenceFieldDesc = Nothing
        Me.dtpEndDateTime.ReferenceFieldName = Nothing
        Me.dtpEndDateTime.ReferenceTableName = Nothing
        Me.dtpEndDateTime.Size = New System.Drawing.Size(153, 20)
        Me.dtpEndDateTime.TabIndex = 2
        Me.dtpEndDateTime.TabStop = False
        Me.dtpEndDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpEndDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblEndDate
        '
        Me.lblEndDate.FieldName = Nothing
        Me.lblEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.Location = New System.Drawing.Point(399, 37)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(104, 16)
        Me.lblEndDate.TabIndex = 322
        Me.lblEndDate.Text = "Cleaning End Date "
        '
        'lblDoneByName
        '
        Me.lblDoneByName.AutoSize = False
        Me.lblDoneByName.BorderVisible = True
        Me.lblDoneByName.FieldName = Nothing
        Me.lblDoneByName.Location = New System.Drawing.Point(403, 170)
        Me.lblDoneByName.Name = "lblDoneByName"
        Me.lblDoneByName.Size = New System.Drawing.Size(290, 19)
        Me.lblDoneByName.TabIndex = 320
        '
        'lblCleaningDoneBy
        '
        Me.lblCleaningDoneBy.FieldName = Nothing
        Me.lblCleaningDoneBy.Location = New System.Drawing.Point(12, 170)
        Me.lblCleaningDoneBy.Name = "lblCleaningDoneBy"
        Me.lblCleaningDoneBy.Size = New System.Drawing.Size(95, 18)
        Me.lblCleaningDoneBy.TabIndex = 319
        Me.lblCleaningDoneBy.Text = "Cleaning Done By"
        '
        'fndCleaningDoneBy
        '
        Me.fndCleaningDoneBy.CalculationExpression = Nothing
        Me.fndCleaningDoneBy.FieldCode = Nothing
        Me.fndCleaningDoneBy.FieldDesc = Nothing
        Me.fndCleaningDoneBy.FieldMaxLength = 0
        Me.fndCleaningDoneBy.FieldName = Nothing
        Me.fndCleaningDoneBy.isCalculatedField = False
        Me.fndCleaningDoneBy.IsSourceFromTable = False
        Me.fndCleaningDoneBy.IsSourceFromValueList = False
        Me.fndCleaningDoneBy.IsUnique = False
        Me.fndCleaningDoneBy.Location = New System.Drawing.Point(151, 169)
        Me.fndCleaningDoneBy.MendatroryField = True
        Me.fndCleaningDoneBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCleaningDoneBy.MyLinkLable1 = Me.lblCleaningDoneBy
        Me.fndCleaningDoneBy.MyLinkLable2 = Me.lblDoneByName
        Me.fndCleaningDoneBy.MyReadOnly = False
        Me.fndCleaningDoneBy.MyShowMasterFormButton = False
        Me.fndCleaningDoneBy.Name = "fndCleaningDoneBy"
        Me.fndCleaningDoneBy.ReferenceFieldDesc = Nothing
        Me.fndCleaningDoneBy.ReferenceFieldName = Nothing
        Me.fndCleaningDoneBy.ReferenceTableName = Nothing
        Me.fndCleaningDoneBy.Size = New System.Drawing.Size(234, 19)
        Me.fndCleaningDoneBy.TabIndex = 9
        Me.fndCleaningDoneBy.Value = ""
        '
        'dtpStartDateTime
        '
        Me.dtpStartDateTime.CalculationExpression = Nothing
        Me.dtpStartDateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpStartDateTime.FieldCode = Nothing
        Me.dtpStartDateTime.FieldDesc = Nothing
        Me.dtpStartDateTime.FieldMaxLength = 0
        Me.dtpStartDateTime.FieldName = Nothing
        Me.dtpStartDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDateTime.isCalculatedField = False
        Me.dtpStartDateTime.IsSourceFromTable = False
        Me.dtpStartDateTime.IsSourceFromValueList = False
        Me.dtpStartDateTime.IsUnique = False
        Me.dtpStartDateTime.Location = New System.Drawing.Point(151, 34)
        Me.dtpStartDateTime.MendatroryField = False
        Me.dtpStartDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDateTime.MyLinkLable1 = Me.lblStartDate
        Me.dtpStartDateTime.MyLinkLable2 = Nothing
        Me.dtpStartDateTime.Name = "dtpStartDateTime"
        Me.dtpStartDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDateTime.ReferenceFieldDesc = Nothing
        Me.dtpStartDateTime.ReferenceFieldName = Nothing
        Me.dtpStartDateTime.ReferenceTableName = Nothing
        Me.dtpStartDateTime.Size = New System.Drawing.Size(231, 20)
        Me.dtpStartDateTime.TabIndex = 1
        Me.dtpStartDateTime.TabStop = False
        Me.dtpStartDateTime.Text = "10/06/2011 11:51:56 AM"
        Me.dtpStartDateTime.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblStartDate
        '
        Me.lblStartDate.FieldName = Nothing
        Me.lblStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(12, 38)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(108, 16)
        Me.lblStartDate.TabIndex = 318
        Me.lblStartDate.Text = "Cleaning Start Date "
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(151, 12)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 30
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(279, 18)
        Me.fndDocNo.TabIndex = 0
        Me.fndDocNo.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(12, 16)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(79, 18)
        Me.lblDocNo.TabIndex = 317
        Me.lblDocNo.Text = "Document No."
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(432, 12)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 316
        '
        'btnReverse
        '
        Me.btnReverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(287, 7)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(77, 24)
        Me.btnReverse.TabIndex = 8
        Me.btnReverse.Text = "Reverse"
        Me.btnReverse.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 24)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'FrmCleaning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1107, 487)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCleaning"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCleaning"
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOutTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpGateEntryDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpQCDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpWeighmentDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpUnloadingDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCOutDateAndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUnloadingNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQCNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDoneByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCleaningDoneBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDoneByName As common.Controls.MyLabel
    Friend WithEvents lblCleaningDoneBy As common.Controls.MyLabel
    Friend WithEvents fndCleaningDoneBy As common.UserControls.txtFinder
    Friend WithEvents dtpStartDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblStartDate As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpEndDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblEndDate As common.Controls.MyLabel
    Friend WithEvents fndGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents txtQCNo As common.Controls.MyTextBox
    Friend WithEvents lblQCNo As common.Controls.MyLabel
    Friend WithEvents txtWeighmentNo As common.Controls.MyTextBox
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents lblCheckedByName As common.Controls.MyLabel
    Friend WithEvents lblCheckedBy As common.Controls.MyLabel
    Friend WithEvents fndCheckedBy As common.UserControls.txtFinder
    Friend WithEvents lblStatus As common.Controls.MyLabel
    Friend WithEvents ddlStatus As common.Controls.MyComboBox
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents TxtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndTankerNo As common.UserControls.txtFinder
    Friend WithEvents txtUnloadingNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpGateEntryDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents dtpQCDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpWeighmentDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents dtpUnloadingDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblQCOutDateAndTime As common.Controls.MyLabel
    Friend WithEvents txtInTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtOutTime As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents chkJobWork As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents fndRefrenceNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
End Class

