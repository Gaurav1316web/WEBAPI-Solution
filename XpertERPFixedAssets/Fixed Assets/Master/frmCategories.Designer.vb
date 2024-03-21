Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCategories
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtSeries1 = New common.MyNumBox()
        Me.lblCategoryCOde = New common.Controls.MyLabel()
        Me.lblSeries = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtGroup = New common.UserControls.txtMultiSelectFinder()
        Me.txtcounter = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblNextAutoNo = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.txtNextAutoNo = New common.MyNumBox()
        Me.lblLastMaintained = New common.Controls.MyLabel()
        Me.txtSegmentCode = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.chkDefaultCategory = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblSegmentCode = New common.Controls.MyLabel()
        Me.dtpLastMaintained = New common.Controls.MyDateTimePicker()
        Me.lblDefaultSetDesc = New common.Controls.MyLabel()
        Me.lblNotes = New common.Controls.MyLabel()
        Me.txtDefaultAccSet = New common.UserControls.txtFinder()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtNotes = New common.Controls.MyTextBox()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.txtCategoryCode = New common.UserControls.txtNavigator()
        Me.lblDefaultAccSet = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtSeries1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategoryCOde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSeries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcounter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNextAutoNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNextAutoNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLastMaintained, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSegmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSegmentCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLastMaintained, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDefaultSetDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDefaultAccSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(801, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport, Me.rmiExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiExport
        '
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        '
        'rmiImport
        '
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExit
        '
        Me.rmiExit.Name = "rmiExit"
        Me.rmiExit.Text = "Exit"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(801, 285)
        Me.SplitContainer1.SplitterDistance = 250
        Me.SplitContainer1.TabIndex = 2
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtSeries1)
        Me.RadGroupBox1.Controls.Add(Me.lblSeries)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtGroup)
        Me.RadGroupBox1.Controls.Add(Me.txtcounter)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblCategoryCOde)
        Me.RadGroupBox1.Controls.Add(Me.lblNextAutoNo)
        Me.RadGroupBox1.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox1.Controls.Add(Me.txtNextAutoNo)
        Me.RadGroupBox1.Controls.Add(Me.lblLastMaintained)
        Me.RadGroupBox1.Controls.Add(Me.txtSegmentCode)
        Me.RadGroupBox1.Controls.Add(Me.chkDefaultCategory)
        Me.RadGroupBox1.Controls.Add(Me.lblSegmentCode)
        Me.RadGroupBox1.Controls.Add(Me.dtpLastMaintained)
        Me.RadGroupBox1.Controls.Add(Me.lblDefaultSetDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblNotes)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtDefaultAccSet)
        Me.RadGroupBox1.Controls.Add(Me.chkInactive)
        Me.RadGroupBox1.Controls.Add(Me.txtNotes)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtCategoryCode)
        Me.RadGroupBox1.Controls.Add(Me.lblDefaultAccSet)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(801, 250)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtSeries1
        '
        Me.txtSeries1.BackColor = System.Drawing.Color.White
        Me.txtSeries1.CalculationExpression = Nothing
        Me.txtSeries1.DecimalPlaces = 2
        Me.txtSeries1.FieldCode = Nothing
        Me.txtSeries1.FieldDesc = Nothing
        Me.txtSeries1.FieldMaxLength = 0
        Me.txtSeries1.FieldName = Nothing
        Me.txtSeries1.isCalculatedField = False
        Me.txtSeries1.IsSourceFromTable = False
        Me.txtSeries1.IsSourceFromValueList = False
        Me.txtSeries1.IsUnique = False
        Me.txtSeries1.Location = New System.Drawing.Point(647, 30)
        Me.txtSeries1.MendatroryField = True
        Me.txtSeries1.MyLinkLable1 = Me.lblCategoryCOde
        Me.txtSeries1.MyLinkLable2 = Nothing
        Me.txtSeries1.Name = "txtSeries1"
        Me.txtSeries1.ReferenceFieldDesc = Nothing
        Me.txtSeries1.ReferenceFieldName = Nothing
        Me.txtSeries1.ReferenceTableName = Nothing
        Me.txtSeries1.Size = New System.Drawing.Size(136, 20)
        Me.txtSeries1.TabIndex = 339
        Me.txtSeries1.Text = "0"
        Me.txtSeries1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSeries1.Value = 0R
        '
        'lblCategoryCOde
        '
        Me.lblCategoryCOde.FieldName = Nothing
        Me.lblCategoryCOde.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategoryCOde.Location = New System.Drawing.Point(7, 8)
        Me.lblCategoryCOde.Name = "lblCategoryCOde"
        Me.lblCategoryCOde.Size = New System.Drawing.Size(33, 16)
        Me.lblCategoryCOde.TabIndex = 42
        Me.lblCategoryCOde.Text = "Code"
        '
        'lblSeries
        '
        Me.lblSeries.FieldName = Nothing
        Me.lblSeries.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSeries.Location = New System.Drawing.Point(603, 32)
        Me.lblSeries.Name = "lblSeries"
        Me.lblSeries.Size = New System.Drawing.Size(38, 16)
        Me.lblSeries.TabIndex = 338
        Me.lblSeries.Text = "Series"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(8, 102)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(38, 18)
        Me.MyLabel3.TabIndex = 336
        Me.MyLabel3.Text = "Group"
        Me.MyLabel3.Visible = False
        '
        'txtGroup
        '
        Me.txtGroup.arrDispalyMember = Nothing
        Me.txtGroup.arrValueMember = Nothing
        Me.txtGroup.Location = New System.Drawing.Point(124, 101)
        Me.txtGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroup.MyLinkLable1 = Me.MyLabel3
        Me.txtGroup.MyLinkLable2 = Nothing
        Me.txtGroup.MyNullText = "All"
        Me.txtGroup.Name = "txtGroup"
        Me.txtGroup.Size = New System.Drawing.Size(309, 19)
        Me.txtGroup.TabIndex = 335
        Me.txtGroup.Visible = False
        '
        'txtcounter
        '
        Me.txtcounter.CalculationExpression = Nothing
        Me.txtcounter.FieldCode = Nothing
        Me.txtcounter.FieldDesc = Nothing
        Me.txtcounter.FieldMaxLength = 0
        Me.txtcounter.FieldName = Nothing
        Me.txtcounter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcounter.isCalculatedField = False
        Me.txtcounter.IsSourceFromTable = False
        Me.txtcounter.IsSourceFromValueList = False
        Me.txtcounter.IsUnique = False
        Me.txtcounter.Location = New System.Drawing.Point(416, 32)
        Me.txtcounter.MendatroryField = True
        Me.txtcounter.MyLinkLable1 = Me.MyLabel1
        Me.txtcounter.MyLinkLable2 = Nothing
        Me.txtcounter.Name = "txtcounter"
        Me.txtcounter.ReferenceFieldDesc = Nothing
        Me.txtcounter.ReferenceFieldName = Nothing
        Me.txtcounter.ReferenceTableName = Nothing
        Me.txtcounter.Size = New System.Drawing.Size(181, 18)
        Me.txtcounter.TabIndex = 3
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(331, 32)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel1.TabIndex = 56
        Me.MyLabel1.Text = "Prefix Counter"
        '
        'lblNextAutoNo
        '
        Me.lblNextAutoNo.FieldName = Nothing
        Me.lblNextAutoNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNextAutoNo.Location = New System.Drawing.Point(479, 184)
        Me.lblNextAutoNo.Name = "lblNextAutoNo"
        Me.lblNextAutoNo.Size = New System.Drawing.Size(77, 16)
        Me.lblNextAutoNo.TabIndex = 55
        Me.lblNextAutoNo.Text = "Next Auto  No"
        Me.lblNextAutoNo.Visible = False
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(391, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'txtNextAutoNo
        '
        Me.txtNextAutoNo.BackColor = System.Drawing.Color.White
        Me.txtNextAutoNo.CalculationExpression = Nothing
        Me.txtNextAutoNo.DecimalPlaces = 2
        Me.txtNextAutoNo.FieldCode = Nothing
        Me.txtNextAutoNo.FieldDesc = Nothing
        Me.txtNextAutoNo.FieldMaxLength = 0
        Me.txtNextAutoNo.FieldName = Nothing
        Me.txtNextAutoNo.isCalculatedField = False
        Me.txtNextAutoNo.IsSourceFromTable = False
        Me.txtNextAutoNo.IsSourceFromValueList = False
        Me.txtNextAutoNo.IsUnique = False
        Me.txtNextAutoNo.Location = New System.Drawing.Point(582, 183)
        Me.txtNextAutoNo.MendatroryField = False
        Me.txtNextAutoNo.MyLinkLable1 = Me.lblCategoryCOde
        Me.txtNextAutoNo.MyLinkLable2 = Nothing
        Me.txtNextAutoNo.Name = "txtNextAutoNo"
        Me.txtNextAutoNo.ReferenceFieldDesc = Nothing
        Me.txtNextAutoNo.ReferenceFieldName = Nothing
        Me.txtNextAutoNo.ReferenceTableName = Nothing
        Me.txtNextAutoNo.Size = New System.Drawing.Size(201, 20)
        Me.txtNextAutoNo.TabIndex = 8
        Me.txtNextAutoNo.Text = "0"
        Me.txtNextAutoNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNextAutoNo.Value = 0R
        Me.txtNextAutoNo.Visible = False
        '
        'lblLastMaintained
        '
        Me.lblLastMaintained.FieldName = Nothing
        Me.lblLastMaintained.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastMaintained.Location = New System.Drawing.Point(7, 209)
        Me.lblLastMaintained.Name = "lblLastMaintained"
        Me.lblLastMaintained.Size = New System.Drawing.Size(30, 16)
        Me.lblLastMaintained.TabIndex = 46
        Me.lblLastMaintained.Text = "Date"
        Me.lblLastMaintained.Visible = False
        '
        'txtSegmentCode
        '
        Me.txtSegmentCode.CalculationExpression = Nothing
        Me.txtSegmentCode.FieldCode = Nothing
        Me.txtSegmentCode.FieldDesc = Nothing
        Me.txtSegmentCode.FieldMaxLength = 0
        Me.txtSegmentCode.FieldName = Nothing
        Me.txtSegmentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSegmentCode.isCalculatedField = False
        Me.txtSegmentCode.IsSourceFromTable = False
        Me.txtSegmentCode.IsSourceFromValueList = False
        Me.txtSegmentCode.IsUnique = False
        Me.txtSegmentCode.Location = New System.Drawing.Point(124, 183)
        Me.txtSegmentCode.MendatroryField = False
        Me.txtSegmentCode.MyLinkLable1 = Me.lblDescription
        Me.txtSegmentCode.MyLinkLable2 = Nothing
        Me.txtSegmentCode.Name = "txtSegmentCode"
        Me.txtSegmentCode.ReferenceFieldDesc = Nothing
        Me.txtSegmentCode.ReferenceFieldName = Nothing
        Me.txtSegmentCode.ReferenceTableName = Nothing
        Me.txtSegmentCode.Size = New System.Drawing.Size(201, 18)
        Me.txtSegmentCode.TabIndex = 7
        Me.txtSegmentCode.Visible = False
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(7, 32)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 41
        Me.lblDescription.Text = "Description"
        '
        'chkDefaultCategory
        '
        Me.chkDefaultCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDefaultCategory.Location = New System.Drawing.Point(452, 11)
        Me.chkDefaultCategory.Name = "chkDefaultCategory"
        Me.chkDefaultCategory.Size = New System.Drawing.Size(145, 16)
        Me.chkDefaultCategory.TabIndex = 48
        Me.chkDefaultCategory.Text = "Use As Default Category"
        Me.chkDefaultCategory.Visible = False
        '
        'lblSegmentCode
        '
        Me.lblSegmentCode.FieldName = Nothing
        Me.lblSegmentCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSegmentCode.Location = New System.Drawing.Point(7, 184)
        Me.lblSegmentCode.Name = "lblSegmentCode"
        Me.lblSegmentCode.Size = New System.Drawing.Size(82, 16)
        Me.lblSegmentCode.TabIndex = 40
        Me.lblSegmentCode.Text = "Segment Code"
        Me.lblSegmentCode.Visible = False
        '
        'dtpLastMaintained
        '
        Me.dtpLastMaintained.CalculationExpression = Nothing
        Me.dtpLastMaintained.CustomFormat = "dd/MM/yyyy"
        Me.dtpLastMaintained.FieldCode = Nothing
        Me.dtpLastMaintained.FieldDesc = Nothing
        Me.dtpLastMaintained.FieldMaxLength = 0
        Me.dtpLastMaintained.FieldName = Nothing
        Me.dtpLastMaintained.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLastMaintained.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLastMaintained.isCalculatedField = False
        Me.dtpLastMaintained.IsSourceFromTable = False
        Me.dtpLastMaintained.IsSourceFromValueList = False
        Me.dtpLastMaintained.IsUnique = False
        Me.dtpLastMaintained.Location = New System.Drawing.Point(124, 207)
        Me.dtpLastMaintained.MendatroryField = False
        Me.dtpLastMaintained.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLastMaintained.MyLinkLable1 = Me.lblLastMaintained
        Me.dtpLastMaintained.MyLinkLable2 = Nothing
        Me.dtpLastMaintained.Name = "dtpLastMaintained"
        Me.dtpLastMaintained.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLastMaintained.ReferenceFieldDesc = Nothing
        Me.dtpLastMaintained.ReferenceFieldName = Nothing
        Me.dtpLastMaintained.ReferenceTableName = Nothing
        Me.dtpLastMaintained.Size = New System.Drawing.Size(98, 18)
        Me.dtpLastMaintained.TabIndex = 4
        Me.dtpLastMaintained.TabStop = False
        Me.dtpLastMaintained.Text = "13/06/2011"
        Me.dtpLastMaintained.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        Me.dtpLastMaintained.Visible = False
        '
        'lblDefaultSetDesc
        '
        Me.lblDefaultSetDesc.AutoSize = False
        Me.lblDefaultSetDesc.BorderVisible = True
        Me.lblDefaultSetDesc.FieldName = Nothing
        Me.lblDefaultSetDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaultSetDesc.Location = New System.Drawing.Point(331, 76)
        Me.lblDefaultSetDesc.Name = "lblDefaultSetDesc"
        Me.lblDefaultSetDesc.Size = New System.Drawing.Size(452, 20)
        Me.lblDefaultSetDesc.TabIndex = 53
        '
        'lblNotes
        '
        Me.lblNotes.FieldName = Nothing
        Me.lblNotes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(7, 56)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(36, 16)
        Me.lblNotes.TabIndex = 49
        Me.lblNotes.Text = "Notes"
        '
        'txtDefaultAccSet
        '
        Me.txtDefaultAccSet.CalculationExpression = Nothing
        Me.txtDefaultAccSet.FieldCode = Nothing
        Me.txtDefaultAccSet.FieldDesc = Nothing
        Me.txtDefaultAccSet.FieldMaxLength = 0
        Me.txtDefaultAccSet.FieldName = Nothing
        Me.txtDefaultAccSet.isCalculatedField = False
        Me.txtDefaultAccSet.IsSourceFromTable = False
        Me.txtDefaultAccSet.IsSourceFromValueList = False
        Me.txtDefaultAccSet.IsUnique = False
        Me.txtDefaultAccSet.Location = New System.Drawing.Point(124, 77)
        Me.txtDefaultAccSet.MendatroryField = True
        Me.txtDefaultAccSet.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultAccSet.MyLinkLable1 = Nothing
        Me.txtDefaultAccSet.MyLinkLable2 = Nothing
        Me.txtDefaultAccSet.MyReadOnly = False
        Me.txtDefaultAccSet.MyShowMasterFormButton = False
        Me.txtDefaultAccSet.Name = "txtDefaultAccSet"
        Me.txtDefaultAccSet.ReferenceFieldDesc = Nothing
        Me.txtDefaultAccSet.ReferenceFieldName = Nothing
        Me.txtDefaultAccSet.ReferenceTableName = Nothing
        Me.txtDefaultAccSet.Size = New System.Drawing.Size(201, 18)
        Me.txtDefaultAccSet.TabIndex = 6
        Me.txtDefaultAccSet.Value = ""
        '
        'chkInactive
        '
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(266, 209)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 16)
        Me.chkInactive.TabIndex = 52
        Me.chkInactive.Text = "Inactive"
        Me.chkInactive.Visible = False
        '
        'txtNotes
        '
        Me.txtNotes.CalculationExpression = Nothing
        Me.txtNotes.FieldCode = Nothing
        Me.txtNotes.FieldDesc = Nothing
        Me.txtNotes.FieldMaxLength = 0
        Me.txtNotes.FieldName = Nothing
        Me.txtNotes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotes.isCalculatedField = False
        Me.txtNotes.IsSourceFromTable = False
        Me.txtNotes.IsSourceFromValueList = False
        Me.txtNotes.IsUnique = False
        Me.txtNotes.Location = New System.Drawing.Point(124, 54)
        Me.txtNotes.MendatroryField = False
        Me.txtNotes.MyLinkLable1 = Me.lblDescription
        Me.txtNotes.MyLinkLable2 = Nothing
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ReferenceFieldDesc = Nothing
        Me.txtNotes.ReferenceFieldName = Nothing
        Me.txtNotes.ReferenceTableName = Nothing
        Me.txtNotes.Size = New System.Drawing.Size(659, 18)
        Me.txtNotes.TabIndex = 5
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(124, 31)
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(201, 18)
        Me.txtDescription.TabIndex = 2
        '
        'txtCategoryCode
        '
        Me.txtCategoryCode.FieldName = Nothing
        Me.txtCategoryCode.Location = New System.Drawing.Point(124, 7)
        Me.txtCategoryCode.MendatroryField = False
        Me.txtCategoryCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCategoryCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCategoryCode.MyLinkLable1 = Nothing
        Me.txtCategoryCode.MyLinkLable2 = Nothing
        Me.txtCategoryCode.MyMaxLength = 30
        Me.txtCategoryCode.MyReadOnly = False
        Me.txtCategoryCode.Name = "txtCategoryCode"
        Me.txtCategoryCode.Size = New System.Drawing.Size(264, 20)
        Me.txtCategoryCode.TabIndex = 1
        Me.txtCategoryCode.Value = ""
        '
        'lblDefaultAccSet
        '
        Me.lblDefaultAccSet.FieldName = Nothing
        Me.lblDefaultAccSet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaultAccSet.Location = New System.Drawing.Point(7, 78)
        Me.lblDefaultAccSet.Name = "lblDefaultAccSet"
        Me.lblDefaultAccSet.Size = New System.Drawing.Size(106, 16)
        Me.lblDefaultAccSet.TabIndex = 51
        Me.lblDefaultAccSet.Text = "Default Account Set"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(727, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'FrmCategories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 305)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCategories"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Categories"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtSeries1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategoryCOde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSeries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcounter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNextAutoNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNextAutoNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLastMaintained, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSegmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSegmentCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLastMaintained, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDefaultSetDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDefaultAccSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblCategoryCOde As common.Controls.MyLabel
    Friend WithEvents lblSegmentCode As common.Controls.MyLabel
    Friend WithEvents txtCategoryCode As common.UserControls.txtNavigator
    Friend WithEvents txtDefaultAccSet As common.UserControls.txtFinder
    Friend WithEvents txtNextAutoNo As common.MyNumBox
    Friend WithEvents lblLastMaintained As common.Controls.MyLabel
    Friend WithEvents dtpLastMaintained As common.Controls.MyDateTimePicker
    Friend WithEvents txtNotes As common.Controls.MyTextBox
    Friend WithEvents lblNotes As common.Controls.MyLabel
    Friend WithEvents chkDefaultCategory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDefaultAccSet As common.Controls.MyLabel
    Friend WithEvents chkInactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblDefaultSetDesc As common.Controls.MyLabel
    Friend WithEvents lblNextAutoNo As common.Controls.MyLabel
    Friend WithEvents txtSegmentCode As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtcounter As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblSeries As common.Controls.MyLabel
    Friend WithEvents txtSeries1 As common.MyNumBox
End Class

