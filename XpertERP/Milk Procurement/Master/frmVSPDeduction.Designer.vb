<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVSPDeduction
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.File = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDeductionRate = New common.MyNumBox()
        Me.MyLabel85 = New common.Controls.MyLabel()
        Me.txtDeductionNoOfPaymentCycleForNewVSP = New common.MyNumBox()
        Me.MyLabel84 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.txtDeductionMinimumSNFPer = New common.MyNumBox()
        Me.MyLabel86 = New common.Controls.MyLabel()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblAdvanceCode = New common.Controls.MyLabel()
        Me.txtDeductionMinimumFATPer = New common.MyNumBox()
        Me.MyLabel87 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnPercent = New common.Controls.MyRadioButton()
        Me.rbtnRate = New common.Controls.MyRadioButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel85, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionNoOfPaymentCycleForNewVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionMinimumSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel86, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeductionMinimumFATPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel87, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenu1
        '
        Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.File})
        Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
        Me.rdmenu1.Name = "rdmenu1"
        Me.rdmenu1.Size = New System.Drawing.Size(605, 20)
        Me.rdmenu1.TabIndex = 1
        Me.rdmenu1.Text = "rdmenu"
        '
        'File
        '
        Me.File.AccessibleDescription = "File"
        Me.File.AccessibleName = "File"
        Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export, Me.RadMenuItem1})
        Me.File.Name = "File"
        Me.File.Text = "File"
        '
        'Import
        '
        Me.Import.AccessibleDescription = "Import"
        Me.Import.AccessibleName = "Import"
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'Export
        '
        Me.Export.AccessibleDescription = "Export"
        Me.Export.AccessibleName = "Export"
        Me.Export.Name = "Export"
        Me.Export.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem1.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Exit"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Hidden
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "RadMenuItem2"
        Me.RadMenuItem2.AccessibleName = "RadMenuItem2"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "RadMenuItem2"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDeductionRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel85)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDeductionNoOfPaymentCycleForNewVSP)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel84)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDeductionMinimumSNFPer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel86)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDeductionMinimumFATPer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdvanceCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel87)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(605, 365)
        Me.SplitContainer1.SplitterDistance = 321
        Me.SplitContainer1.TabIndex = 1
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(14, 70)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(133, 16)
        Me.MyLabel1.TabIndex = 108
        Me.MyLabel1.Text = "VSP Deduction Apply On"
        '
        'txtDeductionRate
        '
        Me.txtDeductionRate.BackColor = System.Drawing.Color.White
        Me.txtDeductionRate.CalculationExpression = Nothing
        Me.txtDeductionRate.DecimalPlaces = 2
        Me.txtDeductionRate.FieldCode = Nothing
        Me.txtDeductionRate.FieldDesc = Nothing
        Me.txtDeductionRate.FieldMaxLength = 0
        Me.txtDeductionRate.FieldName = Nothing
        Me.txtDeductionRate.isCalculatedField = False
        Me.txtDeductionRate.IsSourceFromTable = False
        Me.txtDeductionRate.IsSourceFromValueList = False
        Me.txtDeductionRate.IsUnique = False
        Me.txtDeductionRate.Location = New System.Drawing.Point(269, 94)
        Me.txtDeductionRate.MendatroryField = False
        Me.txtDeductionRate.MyLinkLable1 = Me.MyLabel85
        Me.txtDeductionRate.MyLinkLable2 = Nothing
        Me.txtDeductionRate.Name = "txtDeductionRate"
        Me.txtDeductionRate.ReferenceFieldDesc = Nothing
        Me.txtDeductionRate.ReferenceFieldName = Nothing
        Me.txtDeductionRate.ReferenceTableName = Nothing
        Me.txtDeductionRate.Size = New System.Drawing.Size(60, 20)
        Me.txtDeductionRate.TabIndex = 105
        Me.txtDeductionRate.Text = "0"
        Me.txtDeductionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionRate.Value = 0R
        '
        'MyLabel85
        '
        Me.MyLabel85.FieldName = Nothing
        Me.MyLabel85.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel85.Location = New System.Drawing.Point(14, 96)
        Me.MyLabel85.Name = "MyLabel85"
        Me.MyLabel85.Size = New System.Drawing.Size(110, 16)
        Me.MyLabel85.TabIndex = 106
        Me.MyLabel85.Text = "VSP Deduction Rate"
        '
        'txtDeductionNoOfPaymentCycleForNewVSP
        '
        Me.txtDeductionNoOfPaymentCycleForNewVSP.BackColor = System.Drawing.Color.White
        Me.txtDeductionNoOfPaymentCycleForNewVSP.CalculationExpression = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.DecimalPlaces = 0
        Me.txtDeductionNoOfPaymentCycleForNewVSP.FieldCode = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.FieldDesc = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.FieldMaxLength = 0
        Me.txtDeductionNoOfPaymentCycleForNewVSP.FieldName = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.isCalculatedField = False
        Me.txtDeductionNoOfPaymentCycleForNewVSP.IsSourceFromTable = False
        Me.txtDeductionNoOfPaymentCycleForNewVSP.IsSourceFromValueList = False
        Me.txtDeductionNoOfPaymentCycleForNewVSP.IsUnique = False
        Me.txtDeductionNoOfPaymentCycleForNewVSP.Location = New System.Drawing.Point(269, 161)
        Me.txtDeductionNoOfPaymentCycleForNewVSP.MendatroryField = False
        Me.txtDeductionNoOfPaymentCycleForNewVSP.MyLinkLable1 = Me.MyLabel84
        Me.txtDeductionNoOfPaymentCycleForNewVSP.MyLinkLable2 = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.Name = "txtDeductionNoOfPaymentCycleForNewVSP"
        Me.txtDeductionNoOfPaymentCycleForNewVSP.ReferenceFieldDesc = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.ReferenceFieldName = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.ReferenceTableName = Nothing
        Me.txtDeductionNoOfPaymentCycleForNewVSP.Size = New System.Drawing.Size(60, 20)
        Me.txtDeductionNoOfPaymentCycleForNewVSP.TabIndex = 103
        Me.txtDeductionNoOfPaymentCycleForNewVSP.Text = "0"
        Me.txtDeductionNoOfPaymentCycleForNewVSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionNoOfPaymentCycleForNewVSP.Value = 0R
        '
        'MyLabel84
        '
        Me.MyLabel84.FieldName = Nothing
        Me.MyLabel84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel84.Location = New System.Drawing.Point(14, 163)
        Me.MyLabel84.Name = "MyLabel84"
        Me.MyLabel84.Size = New System.Drawing.Size(237, 16)
        Me.MyLabel84.TabIndex = 104
        Me.MyLabel84.Text = "Applicable For New VSP After Payment Cycle"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(14, 41)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 6
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(96, 40)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(233, 20)
        Me.txtDescription.TabIndex = 1
        '
        'txtDeductionMinimumSNFPer
        '
        Me.txtDeductionMinimumSNFPer.BackColor = System.Drawing.Color.White
        Me.txtDeductionMinimumSNFPer.CalculationExpression = Nothing
        Me.txtDeductionMinimumSNFPer.DecimalPlaces = 1
        Me.txtDeductionMinimumSNFPer.FieldCode = Nothing
        Me.txtDeductionMinimumSNFPer.FieldDesc = Nothing
        Me.txtDeductionMinimumSNFPer.FieldMaxLength = 0
        Me.txtDeductionMinimumSNFPer.FieldName = Nothing
        Me.txtDeductionMinimumSNFPer.isCalculatedField = False
        Me.txtDeductionMinimumSNFPer.IsSourceFromTable = False
        Me.txtDeductionMinimumSNFPer.IsSourceFromValueList = False
        Me.txtDeductionMinimumSNFPer.IsUnique = False
        Me.txtDeductionMinimumSNFPer.Location = New System.Drawing.Point(269, 139)
        Me.txtDeductionMinimumSNFPer.MendatroryField = False
        Me.txtDeductionMinimumSNFPer.MyLinkLable1 = Me.MyLabel86
        Me.txtDeductionMinimumSNFPer.MyLinkLable2 = Nothing
        Me.txtDeductionMinimumSNFPer.Name = "txtDeductionMinimumSNFPer"
        Me.txtDeductionMinimumSNFPer.ReferenceFieldDesc = Nothing
        Me.txtDeductionMinimumSNFPer.ReferenceFieldName = Nothing
        Me.txtDeductionMinimumSNFPer.ReferenceTableName = Nothing
        Me.txtDeductionMinimumSNFPer.Size = New System.Drawing.Size(60, 20)
        Me.txtDeductionMinimumSNFPer.TabIndex = 101
        Me.txtDeductionMinimumSNFPer.Text = "0"
        Me.txtDeductionMinimumSNFPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionMinimumSNFPer.Value = 0R
        '
        'MyLabel86
        '
        Me.MyLabel86.FieldName = Nothing
        Me.MyLabel86.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel86.Location = New System.Drawing.Point(14, 141)
        Me.MyLabel86.Name = "MyLabel86"
        Me.MyLabel86.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel86.TabIndex = 102
        Me.MyLabel86.Text = "Minimum SNF %"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(311, 16)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(18, 21)
        Me.rdbtnreset.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(96, 16)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAdvanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(216, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblAdvanceCode
        '
        Me.lblAdvanceCode.FieldName = Nothing
        Me.lblAdvanceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAdvanceCode.Location = New System.Drawing.Point(14, 17)
        Me.lblAdvanceCode.Name = "lblAdvanceCode"
        Me.lblAdvanceCode.Size = New System.Drawing.Size(32, 18)
        Me.lblAdvanceCode.TabIndex = 2
        Me.lblAdvanceCode.Text = "Code"
        '
        'txtDeductionMinimumFATPer
        '
        Me.txtDeductionMinimumFATPer.BackColor = System.Drawing.Color.White
        Me.txtDeductionMinimumFATPer.CalculationExpression = Nothing
        Me.txtDeductionMinimumFATPer.DecimalPlaces = 1
        Me.txtDeductionMinimumFATPer.FieldCode = Nothing
        Me.txtDeductionMinimumFATPer.FieldDesc = Nothing
        Me.txtDeductionMinimumFATPer.FieldMaxLength = 0
        Me.txtDeductionMinimumFATPer.FieldName = Nothing
        Me.txtDeductionMinimumFATPer.isCalculatedField = False
        Me.txtDeductionMinimumFATPer.IsSourceFromTable = False
        Me.txtDeductionMinimumFATPer.IsSourceFromValueList = False
        Me.txtDeductionMinimumFATPer.IsUnique = False
        Me.txtDeductionMinimumFATPer.Location = New System.Drawing.Point(269, 117)
        Me.txtDeductionMinimumFATPer.MendatroryField = False
        Me.txtDeductionMinimumFATPer.MyLinkLable1 = Me.MyLabel87
        Me.txtDeductionMinimumFATPer.MyLinkLable2 = Nothing
        Me.txtDeductionMinimumFATPer.Name = "txtDeductionMinimumFATPer"
        Me.txtDeductionMinimumFATPer.ReferenceFieldDesc = Nothing
        Me.txtDeductionMinimumFATPer.ReferenceFieldName = Nothing
        Me.txtDeductionMinimumFATPer.ReferenceTableName = Nothing
        Me.txtDeductionMinimumFATPer.Size = New System.Drawing.Size(60, 20)
        Me.txtDeductionMinimumFATPer.TabIndex = 99
        Me.txtDeductionMinimumFATPer.Text = "0"
        Me.txtDeductionMinimumFATPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeductionMinimumFATPer.Value = 0R
        '
        'MyLabel87
        '
        Me.MyLabel87.FieldName = Nothing
        Me.MyLabel87.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel87.Location = New System.Drawing.Point(14, 119)
        Me.MyLabel87.Name = "MyLabel87"
        Me.MyLabel87.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel87.TabIndex = 100
        Me.MyLabel87.Text = "Minimum FAT %"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnPercent)
        Me.GroupBox1.Controls.Add(Me.rbtnRate)
        Me.GroupBox1.Location = New System.Drawing.Point(191, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(137, 36)
        Me.GroupBox1.TabIndex = 107
        Me.GroupBox1.TabStop = False
        '
        'rbtnPercent
        '
        Me.rbtnPercent.Location = New System.Drawing.Point(57, 11)
        Me.rbtnPercent.MyLinkLable1 = Nothing
        Me.rbtnPercent.MyLinkLable2 = Nothing
        Me.rbtnPercent.Name = "rbtnPercent"
        Me.rbtnPercent.Size = New System.Drawing.Size(76, 18)
        Me.rbtnPercent.TabIndex = 1
        Me.rbtnPercent.TabStop = False
        Me.rbtnPercent.Text = "Percentage"
        '
        'rbtnRate
        '
        Me.rbtnRate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnRate.Location = New System.Drawing.Point(7, 11)
        Me.rbtnRate.MyLinkLable1 = Nothing
        Me.rbtnRate.MyLinkLable2 = Nothing
        Me.rbtnRate.Name = "rbtnRate"
        Me.rbtnRate.Size = New System.Drawing.Size(42, 18)
        Me.rbtnRate.TabIndex = 0
        Me.rbtnRate.Text = "Rate"
        Me.rbtnRate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(143, 10)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 23)
        Me.btnHistory.TabIndex = 108
        Me.btnHistory.Text = "&History"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 10)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 23)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(536, 10)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 23)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(74, 10)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 23)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'frmVSPDeduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 385)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "frmVSPDeduction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "VSP Deduction"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel85, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionNoOfPaymentCycleForNewVSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel84, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionMinimumSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel86, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeductionMinimumFATPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel87, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents File As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblAdvanceCode As common.Controls.MyLabel
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents Import As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Export As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDeductionRate As common.MyNumBox
    Friend WithEvents MyLabel85 As common.Controls.MyLabel
    Friend WithEvents txtDeductionNoOfPaymentCycleForNewVSP As common.MyNumBox
    Friend WithEvents MyLabel84 As common.Controls.MyLabel
    Friend WithEvents txtDeductionMinimumSNFPer As common.MyNumBox
    Friend WithEvents MyLabel86 As common.Controls.MyLabel
    Friend WithEvents txtDeductionMinimumFATPer As common.MyNumBox
    Friend WithEvents MyLabel87 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnPercent As common.Controls.MyRadioButton
    Friend WithEvents rbtnRate As common.Controls.MyRadioButton
End Class
