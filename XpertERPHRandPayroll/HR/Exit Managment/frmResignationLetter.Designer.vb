Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmResignationLetter
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtNotice = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtResponsibilityName = New common.Controls.MyLabel()
        Me.txtResponsibilityCode = New common.UserControls.txtFinder()
        Me.lblemployeetype = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDepartmentCode = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtDepartmentName = New common.Controls.MyLabel()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.txtEmployeeName = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtremarks = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtReginationDate = New common.Controls.MyDateTimePicker()
        Me.lblResignationDate = New common.Controls.MyLabel()
        Me.txtResonOfResignation = New common.Controls.MyTextBox()
        Me.lblResonOfRegination = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.Txtemployeetype = New common.UserControls.txtFinder()
        Me.btnApproval = New Telerik.WinControls.UI.RadButton()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.rbtnSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.rbtnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.BtnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.TxtNotice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtResponsibilityName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblemployeetype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepartmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReginationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblResignationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtResonOfResignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblResonOfRegination, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnApproval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnApproval)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(662, 372)
        Me.SplitContainer1.SplitterDistance = 336
        Me.SplitContainer1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TxtNotice)
        Me.Panel1.Controls.Add(Me.MyLabel17)
        Me.Panel1.Controls.Add(Me.UsLock1)
        Me.Panel1.Controls.Add(Me.txtResponsibilityName)
        Me.Panel1.Controls.Add(Me.txtResponsibilityCode)
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.txtDepartmentCode)
        Me.Panel1.Controls.Add(Me.lblCode)
        Me.Panel1.Controls.Add(Me.txtDepartmentName)
        Me.Panel1.Controls.Add(Me.txtcode)
        Me.Panel1.Controls.Add(Me.txtEmployeeName)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Controls.Add(Me.lblRemarks)
        Me.Panel1.Controls.Add(Me.lblDate)
        Me.Panel1.Controls.Add(Me.txtremarks)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Controls.Add(Me.txtReginationDate)
        Me.Panel1.Controls.Add(Me.txtResonOfResignation)
        Me.Panel1.Controls.Add(Me.lblResignationDate)
        Me.Panel1.Controls.Add(Me.lblResonOfRegination)
        Me.Panel1.Controls.Add(Me.lblDepartment)
        Me.Panel1.Controls.Add(Me.Txtemployeetype)
        Me.Panel1.Controls.Add(Me.lblemployeetype)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(662, 336)
        Me.Panel1.TabIndex = 429
        '
        'TxtNotice
        '
        Me.TxtNotice.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtNotice.DecimalPlaces = 0
        Me.TxtNotice.Location = New System.Drawing.Point(138, 178)
        Me.TxtNotice.MaxLength = 3
        Me.TxtNotice.MendatroryField = True
        Me.TxtNotice.MyLinkLable1 = Me.MyLabel17
        Me.TxtNotice.MyLinkLable2 = Nothing
        Me.TxtNotice.Name = "TxtNotice"
        Me.TxtNotice.Size = New System.Drawing.Size(213, 20)
        Me.TxtNotice.TabIndex = 435
        Me.TxtNotice.Text = "0"
        Me.TxtNotice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtNotice.Value = 0.0R
        '
        'MyLabel17
        '
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(10, 180)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(120, 16)
        Me.MyLabel17.TabIndex = 436
        Me.MyLabel17.Text = "Notice Period(In Days)"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(492, 8)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(77, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 433
        '
        'txtResponsibilityName
        '
        Me.txtResponsibilityName.AutoSize = False
        Me.txtResponsibilityName.BorderVisible = True
        Me.txtResponsibilityName.Location = New System.Drawing.Point(359, 145)
        Me.txtResponsibilityName.Name = "txtResponsibilityName"
        Me.txtResponsibilityName.Size = New System.Drawing.Size(210, 19)
        Me.txtResponsibilityName.TabIndex = 432
        Me.txtResponsibilityName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtResponsibilityCode
        '
        Me.txtResponsibilityCode.Location = New System.Drawing.Point(138, 144)
        Me.txtResponsibilityCode.MendatroryField = True
        Me.txtResponsibilityCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResponsibilityCode.MyLinkLable1 = Me.lblemployeetype
        Me.txtResponsibilityCode.MyLinkLable2 = Nothing
        Me.txtResponsibilityCode.MyReadOnly = False
        Me.txtResponsibilityCode.MyShowMasterFormButton = False
        Me.txtResponsibilityCode.Name = "txtResponsibilityCode"
        Me.txtResponsibilityCode.Size = New System.Drawing.Size(213, 19)
        Me.txtResponsibilityCode.TabIndex = 431
        Me.txtResponsibilityCode.Value = ""
        '
        'lblemployeetype
        '
        Me.lblemployeetype.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblemployeetype.Location = New System.Drawing.Point(10, 33)
        Me.lblemployeetype.Name = "lblemployeetype"
        Me.lblemployeetype.Size = New System.Drawing.Size(60, 16)
        Me.lblemployeetype.TabIndex = 420
        Me.lblemployeetype.Text = "Employee "
        '
        'MyLabel1
        '
        Me.MyLabel1.AutoSize = False
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(10, 145)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(117, 32)
        Me.MyLabel1.TabIndex = 430
        Me.MyLabel1.Text = "Handover Responsibility To"
        '
        'txtDepartmentCode
        '
        Me.txtDepartmentCode.AutoSize = False
        Me.txtDepartmentCode.BorderVisible = True
        Me.txtDepartmentCode.Location = New System.Drawing.Point(138, 54)
        Me.txtDepartmentCode.Name = "txtDepartmentCode"
        Me.txtDepartmentCode.Size = New System.Drawing.Size(213, 19)
        Me.txtDepartmentCode.TabIndex = 429
        Me.txtDepartmentCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(10, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(34, 16)
        Me.lblCode.TabIndex = 414
        Me.lblCode.Text = "Code"
        '
        'txtDepartmentName
        '
        Me.txtDepartmentName.AutoSize = False
        Me.txtDepartmentName.BorderVisible = True
        Me.txtDepartmentName.Location = New System.Drawing.Point(357, 54)
        Me.txtDepartmentName.Name = "txtDepartmentName"
        Me.txtDepartmentName.Size = New System.Drawing.Size(212, 19)
        Me.txtDepartmentName.TabIndex = 428
        Me.txtDepartmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(138, 8)
        Me.txtcode.MendatroryField = False
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(213, 20)
        Me.txtcode.TabIndex = 412
        Me.txtcode.Value = ""
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.AutoSize = False
        Me.txtEmployeeName.BorderVisible = True
        Me.txtEmployeeName.Location = New System.Drawing.Point(357, 33)
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.Size = New System.Drawing.Size(212, 19)
        Me.txtEmployeeName.TabIndex = 427
        Me.txtEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnReset
        '
        Me.btnReset.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(350, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(15, 20)
        Me.btnReset.TabIndex = 413
        '
        'lblRemarks
        '
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRemarks.Location = New System.Drawing.Point(10, 119)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 426
        Me.lblRemarks.Text = "Remarks"
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(371, 12)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 415
        Me.lblDate.Text = "Date"
        '
        'txtremarks
        '
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.Location = New System.Drawing.Point(138, 119)
        Me.txtremarks.MaxLength = 100
        Me.txtremarks.MendatroryField = False
        Me.txtremarks.MyLinkLable1 = Me.lblRemarks
        Me.txtremarks.MyLinkLable2 = Nothing
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(431, 18)
        Me.txtremarks.TabIndex = 425
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(407, 8)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(79, 20)
        Me.txtDate.TabIndex = 416
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "16/11/2011"
        Me.txtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'txtReginationDate
        '
        Me.txtReginationDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReginationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReginationDate.Location = New System.Drawing.Point(467, 77)
        Me.txtReginationDate.MendatroryField = False
        Me.txtReginationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReginationDate.MyLinkLable1 = Me.lblResignationDate
        Me.txtReginationDate.MyLinkLable2 = Nothing
        Me.txtReginationDate.Name = "txtReginationDate"
        Me.txtReginationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReginationDate.Size = New System.Drawing.Size(102, 20)
        Me.txtReginationDate.TabIndex = 424
        Me.txtReginationDate.TabStop = False
        Me.txtReginationDate.Text = "16/11/2011"
        Me.txtReginationDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblResignationDate
        '
        Me.lblResignationDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblResignationDate.Location = New System.Drawing.Point(368, 78)
        Me.lblResignationDate.Name = "lblResignationDate"
        Me.lblResignationDate.Size = New System.Drawing.Size(93, 16)
        Me.lblResignationDate.TabIndex = 423
        Me.lblResignationDate.Text = "Resignation Date"
        '
        'txtResonOfResignation
        '
        Me.txtResonOfResignation.AutoSize = False
        Me.txtResonOfResignation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResonOfResignation.Location = New System.Drawing.Point(138, 75)
        Me.txtResonOfResignation.MaxLength = 50
        Me.txtResonOfResignation.MendatroryField = True
        Me.txtResonOfResignation.Multiline = True
        Me.txtResonOfResignation.MyLinkLable1 = Me.lblResonOfRegination
        Me.txtResonOfResignation.MyLinkLable2 = Nothing
        Me.txtResonOfResignation.Name = "txtResonOfResignation"
        Me.txtResonOfResignation.Size = New System.Drawing.Size(213, 40)
        Me.txtResonOfResignation.TabIndex = 417
        '
        'lblResonOfRegination
        '
        Me.lblResonOfRegination.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblResonOfRegination.Location = New System.Drawing.Point(10, 77)
        Me.lblResonOfRegination.Name = "lblResonOfRegination"
        Me.lblResonOfRegination.Size = New System.Drawing.Size(123, 16)
        Me.lblResonOfRegination.TabIndex = 418
        Me.lblResonOfRegination.Text = "Reason Of Resignation"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDepartment.Location = New System.Drawing.Point(10, 54)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(65, 16)
        Me.lblDepartment.TabIndex = 422
        Me.lblDepartment.Text = "Department"
        '
        'Txtemployeetype
        '
        Me.Txtemployeetype.Location = New System.Drawing.Point(138, 32)
        Me.Txtemployeetype.MendatroryField = True
        Me.Txtemployeetype.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtemployeetype.MyLinkLable1 = Me.lblemployeetype
        Me.Txtemployeetype.MyLinkLable2 = Nothing
        Me.Txtemployeetype.MyReadOnly = False
        Me.Txtemployeetype.MyShowMasterFormButton = False
        Me.Txtemployeetype.Name = "Txtemployeetype"
        Me.Txtemployeetype.Size = New System.Drawing.Size(213, 19)
        Me.Txtemployeetype.TabIndex = 419
        Me.Txtemployeetype.Value = ""
        '
        'btnApproval
        '
        Me.btnApproval.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApproval.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApproval.Location = New System.Drawing.Point(349, 5)
        Me.btnApproval.Name = "btnApproval"
        Me.btnApproval.Size = New System.Drawing.Size(103, 20)
        Me.btnApproval.TabIndex = 136
        Me.btnApproval.Text = "Send For Approval"
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rbtnSetting, Me.rbtnSend})
        Me.btnsetting.Location = New System.Drawing.Point(182, 6)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(80, 18)
        Me.btnsetting.TabIndex = 135
        Me.btnsetting.Text = "E-Mail/SMS"
        '
        'rbtnSetting
        '
        Me.rbtnSetting.AccessibleDescription = "RadMenuItem1"
        Me.rbtnSetting.AccessibleName = "RadMenuItem1"
        Me.rbtnSetting.Name = "rbtnSetting"
        Me.rbtnSetting.Text = "EMail/SMS Setting"
        '
        'rbtnSend
        '
        Me.rbtnSend.AccessibleDescription = "RadMenuItem2"
        Me.rbtnSend.AccessibleName = "RadMenuItem2"
        Me.rbtnSend.Name = "rbtnSend"
        Me.rbtnSend.Text = "EMail/SMS Send"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(101, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(76, 20)
        Me.btnDelete.TabIndex = 88
        Me.btnDelete.Text = "Delete"
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.Location = New System.Drawing.Point(21, 5)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(76, 20)
        Me.BtnSave.TabIndex = 87
        Me.BtnSave.Text = "Save"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(267, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 20)
        Me.btnPrint.TabIndex = 84
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(573, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 20)
        Me.btnClose.TabIndex = 85
        Me.btnClose.Text = "Close"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(662, 20)
        Me.rdmenufile.TabIndex = 419
        Me.rdmenufile.Text = "File"
        Me.rdmenufile.Visible = False
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'FrmResignationLetter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 392)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "FrmResignationLetter"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmResignationLetter"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.TxtNotice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtResponsibilityName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblemployeetype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepartmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReginationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblResignationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtResonOfResignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblResonOfRegination, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnApproval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents lblResonOfRegination As common.Controls.MyLabel
    Friend WithEvents txtResonOfResignation As common.Controls.MyTextBox
    Friend WithEvents lblemployeetype As common.Controls.MyLabel
    Friend WithEvents Txtemployeetype As common.UserControls.txtFinder
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents txtReginationDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblResignationDate As common.Controls.MyLabel
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents txtremarks As common.Controls.MyTextBox
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtDepartmentName As common.Controls.MyLabel
    Friend WithEvents txtEmployeeName As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtDepartmentCode As common.Controls.MyLabel
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rbtnSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtnSend As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtResponsibilityName As common.Controls.MyLabel
    Friend WithEvents txtResponsibilityCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnApproval As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents TxtNotice As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
End Class

