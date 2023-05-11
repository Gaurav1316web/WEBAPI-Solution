<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMCCTankerGateOutSecurity
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtGateOutDate = New common.Controls.MyLabel()
        Me.txtRemarks = New common.Controls.MyLabel()
        Me.txt_Phone_No = New common.Controls.MyLabel()
        Me.txtDriver = New common.Controls.MyLabel()
        Me.txtLocationCode = New common.Controls.MyLabel()
        Me.txtMccCode = New common.Controls.MyLabel()
        Me.txtTanker = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblPhoneNo = New common.Controls.MyLabel()
        Me.lblDriver = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.lblMccCode = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.dtpStartDateTime = New common.Controls.MyDateTimePicker()
        Me.lblStartDate = New common.Controls.MyLabel()
        Me.fndGateOutNo = New common.UserControls.txtFinder()
        Me.lblGateEntryNo = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.lblDocNo = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtGateOutDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Phone_No, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPhoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(667, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 23)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGateOutDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_Phone_No)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDriver)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMccCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTanker)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPhoneNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDriver)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWeighmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMccCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpStartDateTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndGateOutNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(739, 214)
        Me.SplitContainer1.SplitterDistance = 171
        Me.SplitContainer1.TabIndex = 2
        '
        'txtGateOutDate
        '
        Me.txtGateOutDate.AutoSize = False
        Me.txtGateOutDate.BorderVisible = True
        Me.txtGateOutDate.FieldName = Nothing
        Me.txtGateOutDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateOutDate.Location = New System.Drawing.Point(515, 29)
        Me.txtGateOutDate.Name = "txtGateOutDate"
        Me.txtGateOutDate.Size = New System.Drawing.Size(220, 21)
        Me.txtGateOutDate.TabIndex = 1471
        Me.txtGateOutDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGateOutDate.TextWrap = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.BorderVisible = True
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(99, 138)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(636, 21)
        Me.txtRemarks.TabIndex = 1470
        Me.txtRemarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemarks.TextWrap = False
        '
        'txt_Phone_No
        '
        Me.txt_Phone_No.AutoSize = False
        Me.txt_Phone_No.BorderVisible = True
        Me.txt_Phone_No.FieldName = Nothing
        Me.txt_Phone_No.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Phone_No.Location = New System.Drawing.Point(99, 116)
        Me.txt_Phone_No.Name = "txt_Phone_No"
        Me.txt_Phone_No.Size = New System.Drawing.Size(139, 21)
        Me.txt_Phone_No.TabIndex = 1469
        Me.txt_Phone_No.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txt_Phone_No.TextWrap = False
        '
        'txtDriver
        '
        Me.txtDriver.AutoSize = False
        Me.txtDriver.BorderVisible = True
        Me.txtDriver.FieldName = Nothing
        Me.txtDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDriver.Location = New System.Drawing.Point(281, 116)
        Me.txtDriver.Name = "txtDriver"
        Me.txtDriver.Size = New System.Drawing.Size(454, 21)
        Me.txtDriver.TabIndex = 1468
        Me.txtDriver.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDriver.TextWrap = False
        '
        'txtLocationCode
        '
        Me.txtLocationCode.AutoSize = False
        Me.txtLocationCode.BorderVisible = True
        Me.txtLocationCode.FieldName = Nothing
        Me.txtLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationCode.Location = New System.Drawing.Point(99, 94)
        Me.txtLocationCode.Name = "txtLocationCode"
        Me.txtLocationCode.Size = New System.Drawing.Size(139, 21)
        Me.txtLocationCode.TabIndex = 1468
        Me.txtLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLocationCode.TextWrap = False
        '
        'txtMccCode
        '
        Me.txtMccCode.AutoSize = False
        Me.txtMccCode.BorderVisible = True
        Me.txtMccCode.FieldName = Nothing
        Me.txtMccCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMccCode.Location = New System.Drawing.Point(99, 72)
        Me.txtMccCode.Name = "txtMccCode"
        Me.txtMccCode.Size = New System.Drawing.Size(139, 21)
        Me.txtMccCode.TabIndex = 1468
        Me.txtMccCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtMccCode.TextWrap = False
        '
        'txtTanker
        '
        Me.txtTanker.AutoSize = False
        Me.txtTanker.BorderVisible = True
        Me.txtTanker.FieldName = Nothing
        Me.txtTanker.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTanker.Location = New System.Drawing.Point(99, 50)
        Me.txtTanker.Name = "txtTanker"
        Me.txtTanker.Size = New System.Drawing.Size(139, 21)
        Me.txtTanker.TabIndex = 1467
        Me.txtTanker.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTanker.TextWrap = False
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(667, 9)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(68, 19)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 1466
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(9, 140)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 1464
        Me.lblRemarks.Text = "Remarks"
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.FieldName = Nothing
        Me.lblPhoneNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneNo.Location = New System.Drawing.Point(9, 118)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(57, 16)
        Me.lblPhoneNo.TabIndex = 1462
        Me.lblPhoneNo.Text = "Phone No"
        '
        'lblDriver
        '
        Me.lblDriver.FieldName = Nothing
        Me.lblDriver.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriver.Location = New System.Drawing.Point(239, 118)
        Me.lblDriver.Name = "lblDriver"
        Me.lblDriver.Size = New System.Drawing.Size(36, 16)
        Me.lblDriver.TabIndex = 1460
        Me.lblDriver.Text = "Driver"
        '
        'lblTankerNo
        '
        Me.lblTankerNo.AutoSize = False
        Me.lblTankerNo.BorderVisible = True
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(239, 50)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(496, 21)
        Me.lblTankerNo.TabIndex = 1459
        Me.lblTankerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTankerNo.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Location = New System.Drawing.Point(9, 51)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel4.TabIndex = 1453
        Me.MyLabel4.Text = "Tanker No"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(9, 95)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel3.TabIndex = 1455
        Me.MyLabel3.Text = "From Location"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationName.Location = New System.Drawing.Point(239, 94)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(496, 21)
        Me.lblLocationName.TabIndex = 1456
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocationName.TextWrap = False
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeighmentNo.Location = New System.Drawing.Point(428, 31)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(82, 16)
        Me.lblWeighmentNo.TabIndex = 1458
        Me.lblWeighmentNo.Text = "Gate Out Date "
        '
        'lblMccCode
        '
        Me.lblMccCode.AutoSize = False
        Me.lblMccCode.BorderVisible = True
        Me.lblMccCode.FieldName = Nothing
        Me.lblMccCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMccCode.Location = New System.Drawing.Point(239, 72)
        Me.lblMccCode.Name = "lblMccCode"
        Me.lblMccCode.Size = New System.Drawing.Size(496, 21)
        Me.lblMccCode.TabIndex = 1457
        Me.lblMccCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMccCode.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(9, 73)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel1.TabIndex = 1454
        Me.MyLabel1.Text = "MCC Code"
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
        Me.dtpStartDateTime.Location = New System.Drawing.Point(515, 8)
        Me.dtpStartDateTime.MendatroryField = False
        Me.dtpStartDateTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDateTime.MyLinkLable1 = Me.lblStartDate
        Me.dtpStartDateTime.MyLinkLable2 = Nothing
        Me.dtpStartDateTime.Name = "dtpStartDateTime"
        Me.dtpStartDateTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDateTime.ReferenceFieldDesc = Nothing
        Me.dtpStartDateTime.ReferenceFieldName = Nothing
        Me.dtpStartDateTime.ReferenceTableName = Nothing
        Me.dtpStartDateTime.Size = New System.Drawing.Size(150, 20)
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
        Me.lblStartDate.Size = New System.Drawing.Size(85, 16)
        Me.lblStartDate.TabIndex = 343
        Me.lblStartDate.Text = "Doucment Date"
        '
        'fndGateOutNo
        '
        Me.fndGateOutNo.CalculationExpression = Nothing
        Me.fndGateOutNo.Enabled = False
        Me.fndGateOutNo.FieldCode = Nothing
        Me.fndGateOutNo.FieldDesc = Nothing
        Me.fndGateOutNo.FieldMaxLength = 0
        Me.fndGateOutNo.FieldName = Nothing
        Me.fndGateOutNo.isCalculatedField = False
        Me.fndGateOutNo.IsSourceFromTable = False
        Me.fndGateOutNo.IsSourceFromValueList = False
        Me.fndGateOutNo.IsUnique = False
        Me.fndGateOutNo.Location = New System.Drawing.Point(99, 30)
        Me.fndGateOutNo.MendatroryField = True
        Me.fndGateOutNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndGateOutNo.MyLinkLable1 = Me.lblGateEntryNo
        Me.fndGateOutNo.MyLinkLable2 = Nothing
        Me.fndGateOutNo.MyReadOnly = False
        Me.fndGateOutNo.MyShowMasterFormButton = False
        Me.fndGateOutNo.Name = "fndGateOutNo"
        Me.fndGateOutNo.ReferenceFieldDesc = Nothing
        Me.fndGateOutNo.ReferenceFieldName = Nothing
        Me.fndGateOutNo.ReferenceTableName = Nothing
        Me.fndGateOutNo.Size = New System.Drawing.Size(327, 19)
        Me.fndGateOutNo.TabIndex = 332
        Me.fndGateOutNo.Value = ""
        '
        'lblGateEntryNo
        '
        Me.lblGateEntryNo.FieldName = Nothing
        Me.lblGateEntryNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGateEntryNo.Location = New System.Drawing.Point(9, 31)
        Me.lblGateEntryNo.Name = "lblGateEntryNo"
        Me.lblGateEntryNo.Size = New System.Drawing.Size(73, 16)
        Me.lblGateEntryNo.TabIndex = 338
        Me.lblGateEntryNo.Text = "Gate Out No."
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(99, 8)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Me.lblDocNo
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(305, 21)
        Me.fndDocNo.TabIndex = 331
        Me.fndDocNo.Value = ""
        '
        'lblDocNo
        '
        Me.lblDocNo.FieldName = Nothing
        Me.lblDocNo.Location = New System.Drawing.Point(9, 9)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(77, 18)
        Me.lblDocNo.TabIndex = 337
        Me.lblDocNo.Text = "Document No"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(404, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(22, 21)
        Me.btnReset.TabIndex = 336
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(148, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 23)
        Me.btnPost.TabIndex = 8
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(4, 9)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 23)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(76, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 23)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'FrmMCCTankerGateOutSecurity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 214)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMCCTankerGateOutSecurity"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sercurity Tanker Gate Out"
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtGateOutDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Phone_No, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPhoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDriver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGateEntryNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents fndGateOutNo As common.UserControls.txtFinder
    Friend WithEvents lblGateEntryNo As common.Controls.MyLabel
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents lblDocNo As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpStartDateTime As common.Controls.MyDateTimePicker
    Friend WithEvents lblStartDate As common.Controls.MyLabel
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblPhoneNo As common.Controls.MyLabel
    Friend WithEvents lblDriver As common.Controls.MyLabel
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents lblMccCode As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents txtGateOutDate As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyLabel
    Friend WithEvents txt_Phone_No As common.Controls.MyLabel
    Friend WithEvents txtDriver As common.Controls.MyLabel
    Friend WithEvents txtLocationCode As common.Controls.MyLabel
    Friend WithEvents txtMccCode As common.Controls.MyLabel
    Friend WithEvents txtTanker As common.Controls.MyLabel
End Class

