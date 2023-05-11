<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVSPCommission
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
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblAdvanceCode = New common.Controls.MyLabel()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtCommissionNoOfPaymentCycleForNewVSP = New common.MyNumBox()
        Me.MyLabel83 = New common.Controls.MyLabel()
        Me.txtCommissionMinimumQtyInShift = New common.MyNumBox()
        Me.MyLabel82 = New common.Controls.MyLabel()
        Me.txtCommissionMinimumShiftInPaymentCycle = New common.MyNumBox()
        Me.MyLabel81 = New common.Controls.MyLabel()
        Me.txtCommissionRate = New common.MyNumBox()
        Me.MyLabel80 = New common.Controls.MyLabel()
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommissionNoOfPaymentCycleForNewVSP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel83, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommissionMinimumQtyInShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel82, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommissionMinimumShiftInPaymentCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel81, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCommissionRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel80, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCommissionNoOfPaymentCycleForNewVSP)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel83)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCommissionMinimumQtyInShift)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel82)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCommissionMinimumShiftInPaymentCycle)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel81)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdvanceCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCommissionRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel80)
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
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(311, 16)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(18, 21)
        Me.rdbtnreset.TabIndex = 1
        '
        'txtCommissionNoOfPaymentCycleForNewVSP
        '
        Me.txtCommissionNoOfPaymentCycleForNewVSP.BackColor = System.Drawing.Color.White
        Me.txtCommissionNoOfPaymentCycleForNewVSP.CalculationExpression = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.DecimalPlaces = 0
        Me.txtCommissionNoOfPaymentCycleForNewVSP.FieldCode = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.FieldDesc = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.FieldMaxLength = 0
        Me.txtCommissionNoOfPaymentCycleForNewVSP.FieldName = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.isCalculatedField = False
        Me.txtCommissionNoOfPaymentCycleForNewVSP.IsSourceFromTable = False
        Me.txtCommissionNoOfPaymentCycleForNewVSP.IsSourceFromValueList = False
        Me.txtCommissionNoOfPaymentCycleForNewVSP.IsUnique = False
        Me.txtCommissionNoOfPaymentCycleForNewVSP.Location = New System.Drawing.Point(269, 131)
        Me.txtCommissionNoOfPaymentCycleForNewVSP.MendatroryField = False
        Me.txtCommissionNoOfPaymentCycleForNewVSP.MyLinkLable1 = Me.MyLabel83
        Me.txtCommissionNoOfPaymentCycleForNewVSP.MyLinkLable2 = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.Name = "txtCommissionNoOfPaymentCycleForNewVSP"
        Me.txtCommissionNoOfPaymentCycleForNewVSP.ReferenceFieldDesc = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.ReferenceFieldName = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.ReferenceTableName = Nothing
        Me.txtCommissionNoOfPaymentCycleForNewVSP.Size = New System.Drawing.Size(60, 20)
        Me.txtCommissionNoOfPaymentCycleForNewVSP.TabIndex = 103
        Me.txtCommissionNoOfPaymentCycleForNewVSP.Text = "0"
        Me.txtCommissionNoOfPaymentCycleForNewVSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCommissionNoOfPaymentCycleForNewVSP.Value = 0R
        '
        'MyLabel83
        '
        Me.MyLabel83.FieldName = Nothing
        Me.MyLabel83.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel83.Location = New System.Drawing.Point(14, 133)
        Me.MyLabel83.Name = "MyLabel83"
        Me.MyLabel83.Size = New System.Drawing.Size(237, 16)
        Me.MyLabel83.TabIndex = 104
        Me.MyLabel83.Text = "Applicable For New VSP After Payment Cycle"
        '
        'txtCommissionMinimumQtyInShift
        '
        Me.txtCommissionMinimumQtyInShift.BackColor = System.Drawing.Color.White
        Me.txtCommissionMinimumQtyInShift.CalculationExpression = Nothing
        Me.txtCommissionMinimumQtyInShift.DecimalPlaces = 0
        Me.txtCommissionMinimumQtyInShift.FieldCode = Nothing
        Me.txtCommissionMinimumQtyInShift.FieldDesc = Nothing
        Me.txtCommissionMinimumQtyInShift.FieldMaxLength = 0
        Me.txtCommissionMinimumQtyInShift.FieldName = Nothing
        Me.txtCommissionMinimumQtyInShift.isCalculatedField = False
        Me.txtCommissionMinimumQtyInShift.IsSourceFromTable = False
        Me.txtCommissionMinimumQtyInShift.IsSourceFromValueList = False
        Me.txtCommissionMinimumQtyInShift.IsUnique = False
        Me.txtCommissionMinimumQtyInShift.Location = New System.Drawing.Point(269, 108)
        Me.txtCommissionMinimumQtyInShift.MendatroryField = False
        Me.txtCommissionMinimumQtyInShift.MyLinkLable1 = Me.MyLabel82
        Me.txtCommissionMinimumQtyInShift.MyLinkLable2 = Nothing
        Me.txtCommissionMinimumQtyInShift.Name = "txtCommissionMinimumQtyInShift"
        Me.txtCommissionMinimumQtyInShift.ReferenceFieldDesc = Nothing
        Me.txtCommissionMinimumQtyInShift.ReferenceFieldName = Nothing
        Me.txtCommissionMinimumQtyInShift.ReferenceTableName = Nothing
        Me.txtCommissionMinimumQtyInShift.Size = New System.Drawing.Size(60, 20)
        Me.txtCommissionMinimumQtyInShift.TabIndex = 101
        Me.txtCommissionMinimumQtyInShift.Text = "0"
        Me.txtCommissionMinimumQtyInShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCommissionMinimumQtyInShift.Value = 0R
        '
        'MyLabel82
        '
        Me.MyLabel82.FieldName = Nothing
        Me.MyLabel82.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel82.Location = New System.Drawing.Point(14, 110)
        Me.MyLabel82.Name = "MyLabel82"
        Me.MyLabel82.Size = New System.Drawing.Size(159, 16)
        Me.MyLabel82.TabIndex = 102
        Me.MyLabel82.Text = "Minimum Milk Qty in One Shift"
        '
        'txtCommissionMinimumShiftInPaymentCycle
        '
        Me.txtCommissionMinimumShiftInPaymentCycle.BackColor = System.Drawing.Color.White
        Me.txtCommissionMinimumShiftInPaymentCycle.CalculationExpression = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.DecimalPlaces = 0
        Me.txtCommissionMinimumShiftInPaymentCycle.FieldCode = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.FieldDesc = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.FieldMaxLength = 0
        Me.txtCommissionMinimumShiftInPaymentCycle.FieldName = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.isCalculatedField = False
        Me.txtCommissionMinimumShiftInPaymentCycle.IsSourceFromTable = False
        Me.txtCommissionMinimumShiftInPaymentCycle.IsSourceFromValueList = False
        Me.txtCommissionMinimumShiftInPaymentCycle.IsUnique = False
        Me.txtCommissionMinimumShiftInPaymentCycle.Location = New System.Drawing.Point(269, 85)
        Me.txtCommissionMinimumShiftInPaymentCycle.MendatroryField = False
        Me.txtCommissionMinimumShiftInPaymentCycle.MyLinkLable1 = Me.MyLabel81
        Me.txtCommissionMinimumShiftInPaymentCycle.MyLinkLable2 = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.Name = "txtCommissionMinimumShiftInPaymentCycle"
        Me.txtCommissionMinimumShiftInPaymentCycle.ReferenceFieldDesc = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.ReferenceFieldName = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.ReferenceTableName = Nothing
        Me.txtCommissionMinimumShiftInPaymentCycle.Size = New System.Drawing.Size(60, 20)
        Me.txtCommissionMinimumShiftInPaymentCycle.TabIndex = 101
        Me.txtCommissionMinimumShiftInPaymentCycle.Text = "0"
        Me.txtCommissionMinimumShiftInPaymentCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCommissionMinimumShiftInPaymentCycle.Value = 0R
        '
        'MyLabel81
        '
        Me.MyLabel81.FieldName = Nothing
        Me.MyLabel81.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel81.Location = New System.Drawing.Point(14, 87)
        Me.MyLabel81.Name = "MyLabel81"
        Me.MyLabel81.Size = New System.Drawing.Size(169, 16)
        Me.MyLabel81.TabIndex = 102
        Me.MyLabel81.Text = "Minimum Shift in Payment Cycle"
        '
        'txtCommissionRate
        '
        Me.txtCommissionRate.BackColor = System.Drawing.Color.White
        Me.txtCommissionRate.CalculationExpression = Nothing
        Me.txtCommissionRate.DecimalPlaces = 2
        Me.txtCommissionRate.FieldCode = Nothing
        Me.txtCommissionRate.FieldDesc = Nothing
        Me.txtCommissionRate.FieldMaxLength = 0
        Me.txtCommissionRate.FieldName = Nothing
        Me.txtCommissionRate.isCalculatedField = False
        Me.txtCommissionRate.IsSourceFromTable = False
        Me.txtCommissionRate.IsSourceFromValueList = False
        Me.txtCommissionRate.IsUnique = False
        Me.txtCommissionRate.Location = New System.Drawing.Point(269, 63)
        Me.txtCommissionRate.MendatroryField = False
        Me.txtCommissionRate.MyLinkLable1 = Me.MyLabel80
        Me.txtCommissionRate.MyLinkLable2 = Nothing
        Me.txtCommissionRate.Name = "txtCommissionRate"
        Me.txtCommissionRate.ReferenceFieldDesc = Nothing
        Me.txtCommissionRate.ReferenceFieldName = Nothing
        Me.txtCommissionRate.ReferenceTableName = Nothing
        Me.txtCommissionRate.Size = New System.Drawing.Size(60, 20)
        Me.txtCommissionRate.TabIndex = 99
        Me.txtCommissionRate.Text = "0"
        Me.txtCommissionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCommissionRate.Value = 0R
        '
        'MyLabel80
        '
        Me.MyLabel80.FieldName = Nothing
        Me.MyLabel80.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel80.Location = New System.Drawing.Point(14, 65)
        Me.MyLabel80.Name = "MyLabel80"
        Me.MyLabel80.Size = New System.Drawing.Size(121, 16)
        Me.MyLabel80.TabIndex = 100
        Me.MyLabel80.Text = "VSP Commission Rate"
        '
        'frmVSPCommission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 385)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "frmVSPCommission"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "VSP Commission"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommissionNoOfPaymentCycleForNewVSP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel83, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommissionMinimumQtyInShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel82, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommissionMinimumShiftInPaymentCycle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel81, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCommissionRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel80, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtCommissionNoOfPaymentCycleForNewVSP As common.MyNumBox
    Friend WithEvents MyLabel83 As common.Controls.MyLabel
    Friend WithEvents txtCommissionMinimumQtyInShift As common.MyNumBox
    Friend WithEvents MyLabel82 As common.Controls.MyLabel
    Friend WithEvents txtCommissionMinimumShiftInPaymentCycle As common.MyNumBox
    Friend WithEvents MyLabel81 As common.Controls.MyLabel
    Friend WithEvents txtCommissionRate As common.MyNumBox
    Friend WithEvents MyLabel80 As common.Controls.MyLabel
End Class
