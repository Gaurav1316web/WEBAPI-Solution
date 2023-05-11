Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHREXTerminationLetter
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
        Me.i = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtResonOfResignation = New System.Windows.Forms.RichTextBox()
        Me.btnsetting = New Telerik.WinControls.UI.RadSplitButton()
        Me.rbtnSetting = New Telerik.WinControls.UI.RadMenuItem()
        Me.rbtnSend = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.BtnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.lblType = New common.Controls.MyLabel()
        Me.ddlType = New common.Controls.MyComboBox()
        Me.txtDepartmentCode = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtDepartmentName = New common.Controls.MyLabel()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.txtEmployeeName = New common.Controls.MyLabel()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblDate = New common.Controls.MyLabel()
        Me.txtremarks = New common.Controls.MyTextBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.txtTerminationDate = New common.Controls.MyDateTimePicker()
        Me.lblTerminationDate = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.Txtemployeetype = New common.UserControls.txtFinder()
        Me.lblemployeetype = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.i, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.i.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepartmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTerminationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTerminationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblemployeetype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.i)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsetting)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(668, 411)
        Me.SplitContainer1.SplitterDistance = 363
        Me.SplitContainer1.TabIndex = 0
        '
        'i
        '
        Me.i.Controls.Add(Me.RadPageViewPage1)
        Me.i.Controls.Add(Me.RadPageViewPage2)
        Me.i.Dock = System.Windows.Forms.DockStyle.Fill
        Me.i.Location = New System.Drawing.Point(0, 0)
        Me.i.Name = "i"
        Me.i.SelectedPage = Me.RadPageViewPage1
        Me.i.Size = New System.Drawing.Size(668, 363)
        Me.i.TabIndex = 418
        Me.i.Text = "RadPageView1"
        CType(Me.i.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(109.0!, 24.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 33)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(647, 319)
        Me.RadPageViewPage1.Text = "Termination Details"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblType)
        Me.Panel1.Controls.Add(Me.ddlType)
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
        Me.Panel1.Controls.Add(Me.txtTerminationDate)
        Me.Panel1.Controls.Add(Me.lblTerminationDate)
        Me.Panel1.Controls.Add(Me.lblDepartment)
        Me.Panel1.Controls.Add(Me.Txtemployeetype)
        Me.Panel1.Controls.Add(Me.lblemployeetype)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(647, 319)
        Me.Panel1.TabIndex = 0
        '
        'btnReset
        '
        Me.btnReset.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(295, 33)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(15, 20)
        Me.btnReset.TabIndex = 434
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.txtResonOfResignation)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(121.0!, 24.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(647, 315)
        Me.RadPageViewPage2.Text = "Reson Of Termination"
        '
        'txtResonOfResignation
        '
        Me.txtResonOfResignation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtResonOfResignation.Location = New System.Drawing.Point(0, 0)
        Me.txtResonOfResignation.Name = "txtResonOfResignation"
        Me.txtResonOfResignation.Size = New System.Drawing.Size(647, 315)
        Me.txtResonOfResignation.TabIndex = 113
        Me.txtResonOfResignation.Text = ""
        '
        'btnsetting
        '
        Me.btnsetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsetting.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rbtnSetting, Me.rbtnSend})
        Me.btnsetting.Location = New System.Drawing.Point(173, 13)
        Me.btnsetting.Name = "btnsetting"
        Me.btnsetting.Size = New System.Drawing.Size(80, 18)
        Me.btnsetting.TabIndex = 140
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
        Me.btnDelete.Location = New System.Drawing.Point(92, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(76, 20)
        Me.btnDelete.TabIndex = 139
        Me.btnDelete.Text = "Delete"
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.Location = New System.Drawing.Point(12, 12)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(76, 20)
        Me.BtnSave.TabIndex = 138
        Me.BtnSave.Text = "Save"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(258, 12)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 20)
        Me.btnPrint.TabIndex = 136
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(581, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 20)
        Me.btnClose.TabIndex = 137
        Me.btnClose.Text = "Close"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(668, 20)
        Me.rdmenufile.TabIndex = 420
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
        'lblType
        '
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblType.Location = New System.Drawing.Point(12, 105)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 16)
        Me.lblType.TabIndex = 451
        Me.lblType.Text = "Type"
        '
        'ddlType
        '
        Me.ddlType.AutoCompleteDisplayMember = Nothing
        Me.ddlType.AutoCompleteValueMember = Nothing
        Me.ddlType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlType.Location = New System.Drawing.Point(82, 103)
        Me.ddlType.MendatroryField = True
        Me.ddlType.MyLinkLable1 = Nothing
        Me.ddlType.MyLinkLable2 = Nothing
        Me.ddlType.Name = "ddlType"
        Me.ddlType.Size = New System.Drawing.Size(226, 18)
        Me.ddlType.TabIndex = 450
        '
        'txtDepartmentCode
        '
        Me.txtDepartmentCode.AutoSize = False
        Me.txtDepartmentCode.BorderVisible = True
        Me.txtDepartmentCode.Location = New System.Drawing.Point(82, 79)
        Me.txtDepartmentCode.Name = "txtDepartmentCode"
        Me.txtDepartmentCode.Size = New System.Drawing.Size(226, 19)
        Me.txtDepartmentCode.TabIndex = 449
        Me.txtDepartmentCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(12, 37)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 435
        Me.lblCode.Text = "Code"
        '
        'txtDepartmentName
        '
        Me.txtDepartmentName.AutoSize = False
        Me.txtDepartmentName.BorderVisible = True
        Me.txtDepartmentName.Location = New System.Drawing.Point(316, 79)
        Me.txtDepartmentName.Name = "txtDepartmentName"
        Me.txtDepartmentName.Size = New System.Drawing.Size(212, 19)
        Me.txtDepartmentName.TabIndex = 448
        Me.txtDepartmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(81, 33)
        Me.txtcode.MendatroryField = False
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(213, 20)
        Me.txtcode.TabIndex = 433
        Me.txtcode.Value = ""
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.AutoSize = False
        Me.txtEmployeeName.BorderVisible = True
        Me.txtEmployeeName.Location = New System.Drawing.Point(316, 58)
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.Size = New System.Drawing.Size(212, 19)
        Me.txtEmployeeName.TabIndex = 447
        Me.txtEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRemarks
        '
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRemarks.Location = New System.Drawing.Point(12, 129)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(51, 16)
        Me.lblRemarks.TabIndex = 446
        Me.lblRemarks.Text = "Remarks"
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(314, 37)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 436
        Me.lblDate.Text = "Date"
        '
        'txtremarks
        '
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.Location = New System.Drawing.Point(81, 129)
        Me.txtremarks.MaxLength = 100
        Me.txtremarks.MendatroryField = False
        Me.txtremarks.MyLinkLable1 = Me.lblRemarks
        Me.txtremarks.MyLinkLable2 = Nothing
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(447, 18)
        Me.txtremarks.TabIndex = 445
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(350, 33)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(98, 20)
        Me.txtDate.TabIndex = 437
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "16/11/2011"
        Me.txtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'txtTerminationDate
        '
        Me.txtTerminationDate.CustomFormat = "dd/MM/yyyy"
        Me.txtTerminationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTerminationDate.Location = New System.Drawing.Point(410, 102)
        Me.txtTerminationDate.MendatroryField = False
        Me.txtTerminationDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTerminationDate.MyLinkLable1 = Me.lblTerminationDate
        Me.txtTerminationDate.MyLinkLable2 = Nothing
        Me.txtTerminationDate.Name = "txtTerminationDate"
        Me.txtTerminationDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTerminationDate.Size = New System.Drawing.Size(118, 20)
        Me.txtTerminationDate.TabIndex = 444
        Me.txtTerminationDate.TabStop = False
        Me.txtTerminationDate.Text = "16/11/2011"
        Me.txtTerminationDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblTerminationDate
        '
        Me.lblTerminationDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTerminationDate.Location = New System.Drawing.Point(311, 103)
        Me.lblTerminationDate.Name = "lblTerminationDate"
        Me.lblTerminationDate.Size = New System.Drawing.Size(93, 16)
        Me.lblTerminationDate.TabIndex = 443
        Me.lblTerminationDate.Text = "Termination Date"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDepartment.Location = New System.Drawing.Point(12, 79)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(65, 16)
        Me.lblDepartment.TabIndex = 442
        Me.lblDepartment.Text = "Department"
        '
        'Txtemployeetype
        '
        Me.Txtemployeetype.Location = New System.Drawing.Point(81, 57)
        Me.Txtemployeetype.MendatroryField = True
        Me.Txtemployeetype.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtemployeetype.MyLinkLable1 = Me.lblemployeetype
        Me.Txtemployeetype.MyLinkLable2 = Nothing
        Me.Txtemployeetype.MyReadOnly = False
        Me.Txtemployeetype.MyShowMasterFormButton = False
        Me.Txtemployeetype.Name = "Txtemployeetype"
        Me.Txtemployeetype.Size = New System.Drawing.Size(227, 19)
        Me.Txtemployeetype.TabIndex = 440
        Me.Txtemployeetype.Value = ""
        '
        'lblemployeetype
        '
        Me.lblemployeetype.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblemployeetype.Location = New System.Drawing.Point(12, 58)
        Me.lblemployeetype.Name = "lblemployeetype"
        Me.lblemployeetype.Size = New System.Drawing.Size(60, 16)
        Me.lblemployeetype.TabIndex = 441
        Me.lblemployeetype.Text = "Employee "
        '
        'FrmHREXTerminationLetter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 431)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "FrmHREXTerminationLetter"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmHREXTerminationLetter"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.i, System.ComponentModel.ISupportInitialize).EndInit()
        Me.i.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnsetting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepartmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTerminationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTerminationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblemployeetype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtDepartmentCode As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtDepartmentName As common.Controls.MyLabel
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents txtEmployeeName As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtremarks As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtTerminationDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblTerminationDate As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents Txtemployeetype As common.UserControls.txtFinder
    Friend WithEvents lblemployeetype As common.Controls.MyLabel
    Friend WithEvents i As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtResonOfResignation As System.Windows.Forms.RichTextBox
    Friend WithEvents btnsetting As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents ddlType As common.Controls.MyComboBox
    Friend WithEvents rbtnSetting As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rbtnSend As Telerik.WinControls.UI.RadMenuItem
End Class

