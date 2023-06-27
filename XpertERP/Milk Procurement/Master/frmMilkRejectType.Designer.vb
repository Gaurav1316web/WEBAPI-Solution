<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkRejectType
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.File = New Telerik.WinControls.UI.RadMenuItem()
        Me.Import = New Telerik.WinControls.UI.RadMenuItem()
        Me.Export = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chkExcludeHead = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIncludeInDBT = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.grpAdditionType = New System.Windows.Forms.GroupBox()
        Me.rbtnSNFKGRate = New common.Controls.MyRadioButton()
        Me.rbtnFATKGRate = New common.Controls.MyRadioButton()
        Me.rbtnPer = New common.Controls.MyRadioButton()
        Me.rbtnRate = New common.Controls.MyRadioButton()
        Me.txtSNo = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.Lbl_Type = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.txtItem = New common.UserControls.txtFinder()
        Me.lblItem = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtApplicablePer = New common.MyNumBox()
        Me.lblNoOfCans = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblAdvanceCode = New common.Controls.MyLabel()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkExcludeHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeInDBT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAdditionType.SuspendLayout()
        CType(Me.rbtnSNFKGRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnFATKGRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl_Type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApplicablePer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'File
        '
        Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Import, Me.Export, Me.RadMenuItem1})
        Me.File.Name = "File"
        Me.File.Text = "File"
        '
        'Import
        '
        Me.Import.Name = "Import"
        Me.Import.Text = "Import"
        '
        'Export
        '
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkExcludeHead)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkIncludeInDBT)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpAdditionType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Lbl_Type)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtApplicablePer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblNoOfCans)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdvanceCode)
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
        'chkExcludeHead
        '
        Me.chkExcludeHead.Location = New System.Drawing.Point(226, 205)
        Me.chkExcludeHead.Name = "chkExcludeHead"
        Me.chkExcludeHead.Size = New System.Drawing.Size(134, 18)
        Me.chkExcludeHead.TabIndex = 1032
        Me.chkExcludeHead.Text = "Exclude For Head Load"
        '
        'chkIncludeInDBT
        '
        Me.chkIncludeInDBT.Location = New System.Drawing.Point(102, 205)
        Me.chkIncludeInDBT.Name = "chkIncludeInDBT"
        Me.chkIncludeInDBT.Size = New System.Drawing.Size(93, 18)
        Me.chkIncludeInDBT.TabIndex = 1031
        Me.chkIncludeInDBT.Text = "Include In DBT"
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
        Me.txtDescription.Location = New System.Drawing.Point(102, 39)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(280, 20)
        Me.txtDescription.TabIndex = 1
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(8, 68)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel3.TabIndex = 1030
        Me.MyLabel3.Text = "Applicable Type"
        '
        'grpAdditionType
        '
        Me.grpAdditionType.Controls.Add(Me.rbtnSNFKGRate)
        Me.grpAdditionType.Controls.Add(Me.rbtnFATKGRate)
        Me.grpAdditionType.Controls.Add(Me.rbtnPer)
        Me.grpAdditionType.Controls.Add(Me.rbtnRate)
        Me.grpAdditionType.Location = New System.Drawing.Point(102, 56)
        Me.grpAdditionType.Name = "grpAdditionType"
        Me.grpAdditionType.Size = New System.Drawing.Size(280, 31)
        Me.grpAdditionType.TabIndex = 1029
        Me.grpAdditionType.TabStop = False
        '
        'rbtnSNFKGRate
        '
        Me.rbtnSNFKGRate.Location = New System.Drawing.Point(194, 9)
        Me.rbtnSNFKGRate.MyLinkLable1 = Nothing
        Me.rbtnSNFKGRate.MyLinkLable2 = Nothing
        Me.rbtnSNFKGRate.Name = "rbtnSNFKGRate"
        Me.rbtnSNFKGRate.Size = New System.Drawing.Size(83, 18)
        Me.rbtnSNFKGRate.TabIndex = 4
        Me.rbtnSNFKGRate.TabStop = False
        Me.rbtnSNFKGRate.Text = "SNF KG Rate"
        '
        'rbtnFATKGRate
        '
        Me.rbtnFATKGRate.Location = New System.Drawing.Point(108, 9)
        Me.rbtnFATKGRate.MyLinkLable1 = Nothing
        Me.rbtnFATKGRate.MyLinkLable2 = Nothing
        Me.rbtnFATKGRate.Name = "rbtnFATKGRate"
        Me.rbtnFATKGRate.Size = New System.Drawing.Size(82, 18)
        Me.rbtnFATKGRate.TabIndex = 3
        Me.rbtnFATKGRate.TabStop = False
        Me.rbtnFATKGRate.Text = "FAT KG Rate"
        '
        'rbtnPer
        '
        Me.rbtnPer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnPer.Location = New System.Drawing.Point(6, 9)
        Me.rbtnPer.MyLinkLable1 = Nothing
        Me.rbtnPer.MyLinkLable2 = Nothing
        Me.rbtnPer.Name = "rbtnPer"
        Me.rbtnPer.Size = New System.Drawing.Size(52, 18)
        Me.rbtnPer.TabIndex = 2
        Me.rbtnPer.Text = "% Age"
        Me.rbtnPer.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnRate
        '
        Me.rbtnRate.Location = New System.Drawing.Point(62, 9)
        Me.rbtnRate.MyLinkLable1 = Nothing
        Me.rbtnRate.MyLinkLable2 = Nothing
        Me.rbtnRate.Name = "rbtnRate"
        Me.rbtnRate.Size = New System.Drawing.Size(42, 18)
        Me.rbtnRate.TabIndex = 0
        Me.rbtnRate.TabStop = False
        Me.rbtnRate.Text = "Rate"
        '
        'txtSNo
        '
        Me.txtSNo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNo.CalculationExpression = Nothing
        Me.txtSNo.DecimalPlaces = 2
        Me.txtSNo.FieldCode = Nothing
        Me.txtSNo.FieldDesc = Nothing
        Me.txtSNo.FieldMaxLength = 0
        Me.txtSNo.FieldName = Nothing
        Me.txtSNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSNo.isCalculatedField = False
        Me.txtSNo.IsSourceFromTable = False
        Me.txtSNo.IsSourceFromValueList = False
        Me.txtSNo.IsUnique = False
        Me.txtSNo.Location = New System.Drawing.Point(102, 157)
        Me.txtSNo.MaxLength = 5
        Me.txtSNo.MendatroryField = True
        Me.txtSNo.MyLinkLable1 = Me.MyLabel1
        Me.txtSNo.MyLinkLable2 = Nothing
        Me.txtSNo.Name = "txtSNo"
        Me.txtSNo.ReferenceFieldDesc = Nothing
        Me.txtSNo.ReferenceFieldName = Nothing
        Me.txtSNo.ReferenceTableName = Nothing
        Me.txtSNo.Size = New System.Drawing.Size(280, 21)
        Me.txtSNo.TabIndex = 1027
        Me.txtSNo.Text = "0"
        Me.txtSNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNo.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(8, 159)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel1.TabIndex = 1028
        Me.MyLabel1.Text = "SNo"
        '
        'Lbl_Type
        '
        Me.Lbl_Type.FieldName = Nothing
        Me.Lbl_Type.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Type.Location = New System.Drawing.Point(8, 181)
        Me.Lbl_Type.Name = "Lbl_Type"
        Me.Lbl_Type.Size = New System.Drawing.Size(31, 16)
        Me.Lbl_Type.TabIndex = 1026
        Me.Lbl_Type.Text = "Type"
        Me.Lbl_Type.Visible = False
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownAnimationEnabled = True
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        Me.cboType.Location = New System.Drawing.Point(102, 180)
        Me.cboType.MendatroryField = True
        Me.cboType.MyLinkLable1 = Nothing
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(280, 18)
        Me.cboType.TabIndex = 1025
        Me.cboType.Visible = False
        '
        'txtItem
        '
        Me.txtItem.CalculationExpression = Nothing
        Me.txtItem.FieldCode = Nothing
        Me.txtItem.FieldDesc = Nothing
        Me.txtItem.FieldMaxLength = 0
        Me.txtItem.FieldName = Nothing
        Me.txtItem.isCalculatedField = False
        Me.txtItem.IsSourceFromTable = False
        Me.txtItem.IsSourceFromValueList = False
        Me.txtItem.IsUnique = False
        Me.txtItem.Location = New System.Drawing.Point(102, 117)
        Me.txtItem.MendatroryField = True
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Nothing
        Me.txtItem.MyLinkLable2 = Me.lblItem
        Me.txtItem.MyReadOnly = False
        Me.txtItem.MyShowMasterFormButton = False
        Me.txtItem.Name = "txtItem"
        Me.txtItem.ReferenceFieldDesc = Nothing
        Me.txtItem.ReferenceFieldName = Nothing
        Me.txtItem.ReferenceTableName = Nothing
        Me.txtItem.Size = New System.Drawing.Size(280, 18)
        Me.txtItem.TabIndex = 80
        Me.txtItem.Value = ""
        '
        'lblItem
        '
        Me.lblItem.AutoSize = False
        Me.lblItem.BorderVisible = True
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(102, 137)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(280, 18)
        Me.lblItem.TabIndex = 81
        Me.lblItem.TextWrap = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(8, 118)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel2.TabIndex = 79
        Me.MyLabel2.Text = "Item"
        '
        'txtApplicablePer
        '
        Me.txtApplicablePer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtApplicablePer.CalculationExpression = Nothing
        Me.txtApplicablePer.DecimalPlaces = 2
        Me.txtApplicablePer.FieldCode = Nothing
        Me.txtApplicablePer.FieldDesc = Nothing
        Me.txtApplicablePer.FieldMaxLength = 0
        Me.txtApplicablePer.FieldName = Nothing
        Me.txtApplicablePer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtApplicablePer.isCalculatedField = False
        Me.txtApplicablePer.IsSourceFromTable = False
        Me.txtApplicablePer.IsSourceFromValueList = False
        Me.txtApplicablePer.IsUnique = False
        Me.txtApplicablePer.Location = New System.Drawing.Point(102, 94)
        Me.txtApplicablePer.MaxLength = 5
        Me.txtApplicablePer.MendatroryField = True
        Me.txtApplicablePer.MyLinkLable1 = Me.lblNoOfCans
        Me.txtApplicablePer.MyLinkLable2 = Nothing
        Me.txtApplicablePer.Name = "txtApplicablePer"
        Me.txtApplicablePer.ReferenceFieldDesc = Nothing
        Me.txtApplicablePer.ReferenceFieldName = Nothing
        Me.txtApplicablePer.ReferenceTableName = Nothing
        Me.txtApplicablePer.Size = New System.Drawing.Size(280, 21)
        Me.txtApplicablePer.TabIndex = 2
        Me.txtApplicablePer.Text = "0"
        Me.txtApplicablePer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtApplicablePer.Value = 0R
        '
        'lblNoOfCans
        '
        Me.lblNoOfCans.FieldName = Nothing
        Me.lblNoOfCans.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNoOfCans.Location = New System.Drawing.Point(8, 96)
        Me.lblNoOfCans.Name = "lblNoOfCans"
        Me.lblNoOfCans.Size = New System.Drawing.Size(91, 16)
        Me.lblNoOfCans.TabIndex = 37
        Me.lblNoOfCans.Text = "Applicable Value"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(8, 40)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 6
        Me.lblDescription.Text = "Description"
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(364, 16)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(18, 21)
        Me.rdbtnreset.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(102, 16)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAdvanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(262, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblAdvanceCode
        '
        Me.lblAdvanceCode.FieldName = Nothing
        Me.lblAdvanceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblAdvanceCode.Location = New System.Drawing.Point(8, 17)
        Me.lblAdvanceCode.Name = "lblAdvanceCode"
        Me.lblAdvanceCode.Size = New System.Drawing.Size(32, 18)
        Me.lblAdvanceCode.TabIndex = 2
        Me.lblAdvanceCode.Text = "Code"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(143, 6)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(66, 27)
        Me.btnHistory.TabIndex = 108
        Me.btnHistory.Text = "&History"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 27)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(536, 6)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 27)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(74, 6)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 27)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
        '
        'frmMilkRejectType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 385)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "frmMilkRejectType"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Reject Type"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkExcludeHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeInDBT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAdditionType.ResumeLayout(False)
        Me.grpAdditionType.PerformLayout()
        CType(Me.rbtnSNFKGRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnFATKGRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl_Type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApplicablePer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtApplicablePer As common.MyNumBox
    Friend WithEvents lblNoOfCans As common.Controls.MyLabel
    Friend WithEvents btnHistory As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtItem As common.UserControls.txtFinder
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents Lbl_Type As common.Controls.MyLabel
    Friend WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents txtSNo As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents grpAdditionType As GroupBox
    Friend WithEvents rbtnPer As common.Controls.MyRadioButton
    Friend WithEvents rbtnRate As common.Controls.MyRadioButton
    Friend WithEvents rbtnSNFKGRate As common.Controls.MyRadioButton
    Friend WithEvents rbtnFATKGRate As common.Controls.MyRadioButton
    Friend WithEvents chkIncludeInDBT As RadCheckBox
    Friend WithEvents chkExcludeHead As RadCheckBox
End Class

