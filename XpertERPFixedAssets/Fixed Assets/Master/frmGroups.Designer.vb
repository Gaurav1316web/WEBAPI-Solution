Imports XpertERPEngine
Imports common
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGroups
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
        Me.lblGroup = New common.Controls.MyLabel()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.lblLastMaintained = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.chkDefaultCategory = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.dtpLastMaintained = New common.Controls.MyDateTimePicker()
        Me.lblNotes = New common.Controls.MyLabel()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.txtNotes = New common.Controls.MyTextBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtcounter = New common.Controls.MyTextBox()
        Me.lblPrefix = New common.Controls.MyLabel()
        Me.lblCategoryDesc = New common.Controls.MyLabel()
        Me.txtCategory = New common.UserControls.txtFinder()
        Me.lblCategory = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.txtGroupCode = New common.UserControls.txtNavigator()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.lblGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLastMaintained, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefaultCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLastMaintained, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtcounter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrefix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategoryDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblGroup
        '
        Me.lblGroup.FieldName = Nothing
        Me.lblGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroup.Location = New System.Drawing.Point(7, 8)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.Size = New System.Drawing.Size(33, 16)
        Me.lblGroup.TabIndex = 42
        Me.lblGroup.Text = "Code"
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.XpertERPFixedAssets.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(364, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(702, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExport, Me.rmiImport, Me.rmiExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiExport
        '
        Me.rmiExport.AccessibleDescription = "Export"
        Me.rmiExport.AccessibleName = "Export"
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export"
        '
        'rmiImport
        '
        Me.rmiImport.AccessibleDescription = "Import"
        Me.rmiImport.AccessibleName = "Import"
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExit
        '
        Me.rmiExit.AccessibleDescription = "Exit"
        Me.rmiExit.AccessibleName = "Exit"
        Me.rmiExit.Name = "rmiExit"
        Me.rmiExit.Text = "Exit"
        '
        'lblLastMaintained
        '
        Me.lblLastMaintained.FieldName = Nothing
        Me.lblLastMaintained.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastMaintained.Location = New System.Drawing.Point(9, 138)
        Me.lblLastMaintained.Name = "lblLastMaintained"
        Me.lblLastMaintained.Size = New System.Drawing.Size(30, 16)
        Me.lblLastMaintained.TabIndex = 46
        Me.lblLastMaintained.Text = "Date"
        Me.lblLastMaintained.Visible = False
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
        Me.chkDefaultCategory.Location = New System.Drawing.Point(425, 11)
        Me.chkDefaultCategory.Name = "chkDefaultCategory"
        Me.chkDefaultCategory.Size = New System.Drawing.Size(130, 16)
        Me.chkDefaultCategory.TabIndex = 2
        Me.chkDefaultCategory.Text = "Use As Default Group"
        Me.chkDefaultCategory.Visible = False
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(628, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(81, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
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
        Me.dtpLastMaintained.Location = New System.Drawing.Point(99, 137)
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
        'lblNotes
        '
        Me.lblNotes.FieldName = Nothing
        Me.lblNotes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(9, 160)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(36, 16)
        Me.lblNotes.TabIndex = 49
        Me.lblNotes.Text = "Notes"
        Me.lblNotes.Visible = False
        '
        'chkInactive
        '
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(241, 138)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 16)
        Me.chkInactive.TabIndex = 5
        Me.chkInactive.Text = "Inactive"
        Me.chkInactive.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
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
        Me.txtNotes.Location = New System.Drawing.Point(99, 158)
        Me.txtNotes.MendatroryField = False
        Me.txtNotes.MyLinkLable1 = Me.lblDescription
        Me.txtNotes.MyLinkLable2 = Nothing
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ReferenceFieldDesc = Nothing
        Me.txtNotes.ReferenceFieldName = Nothing
        Me.txtNotes.ReferenceTableName = Nothing
        Me.txtNotes.Size = New System.Drawing.Size(580, 18)
        Me.txtNotes.TabIndex = 6
        Me.txtNotes.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtcounter)
        Me.RadGroupBox1.Controls.Add(Me.lblPrefix)
        Me.RadGroupBox1.Controls.Add(Me.lblCategoryDesc)
        Me.RadGroupBox1.Controls.Add(Me.txtCategory)
        Me.RadGroupBox1.Controls.Add(Me.lblCategory)
        Me.RadGroupBox1.Controls.Add(Me.lblGroup)
        Me.RadGroupBox1.Controls.Add(Me.btnAddNew)
        Me.RadGroupBox1.Controls.Add(Me.lblLastMaintained)
        Me.RadGroupBox1.Controls.Add(Me.chkDefaultCategory)
        Me.RadGroupBox1.Controls.Add(Me.dtpLastMaintained)
        Me.RadGroupBox1.Controls.Add(Me.lblNotes)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.chkInactive)
        Me.RadGroupBox1.Controls.Add(Me.txtNotes)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtGroupCode)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(7, 31)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(685, 195)
        Me.RadGroupBox1.TabIndex = 57
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
        Me.txtcounter.Location = New System.Drawing.Point(97, 75)
        Me.txtcounter.MendatroryField = True
        Me.txtcounter.MyLinkLable1 = Me.lblPrefix
        Me.txtcounter.MyLinkLable2 = Nothing
        Me.txtcounter.Name = "txtcounter"
        Me.txtcounter.ReferenceFieldDesc = Nothing
        Me.txtcounter.ReferenceFieldName = Nothing
        Me.txtcounter.ReferenceTableName = Nothing
        Me.txtcounter.Size = New System.Drawing.Size(201, 18)
        Me.txtcounter.TabIndex = 57
        '
        'lblPrefix
        '
        Me.lblPrefix.FieldName = Nothing
        Me.lblPrefix.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPrefix.Location = New System.Drawing.Point(7, 78)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(79, 16)
        Me.lblPrefix.TabIndex = 58
        Me.lblPrefix.Text = "Prefix Counter"
        '
        'lblCategoryDesc
        '
        Me.lblCategoryDesc.AutoSize = False
        Me.lblCategoryDesc.BorderVisible = True
        Me.lblCategoryDesc.FieldName = Nothing
        Me.lblCategoryDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategoryDesc.Location = New System.Drawing.Point(304, 52)
        Me.lblCategoryDesc.Name = "lblCategoryDesc"
        Me.lblCategoryDesc.Size = New System.Drawing.Size(373, 20)
        Me.lblCategoryDesc.TabIndex = 54
        Me.lblCategoryDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCategory
        '
        Me.txtCategory.CalculationExpression = Nothing
        Me.txtCategory.FieldCode = Nothing
        Me.txtCategory.FieldDesc = Nothing
        Me.txtCategory.FieldMaxLength = 0
        Me.txtCategory.FieldName = Nothing
        Me.txtCategory.isCalculatedField = False
        Me.txtCategory.IsSourceFromTable = False
        Me.txtCategory.IsSourceFromValueList = False
        Me.txtCategory.IsUnique = False
        Me.txtCategory.Location = New System.Drawing.Point(97, 54)
        Me.txtCategory.MendatroryField = True
        Me.txtCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.MyLinkLable1 = Nothing
        Me.txtCategory.MyLinkLable2 = Nothing
        Me.txtCategory.MyReadOnly = False
        Me.txtCategory.MyShowMasterFormButton = False
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.ReferenceFieldDesc = Nothing
        Me.txtCategory.ReferenceFieldName = Nothing
        Me.txtCategory.ReferenceTableName = Nothing
        Me.txtCategory.Size = New System.Drawing.Size(201, 18)
        Me.txtCategory.TabIndex = 52
        Me.txtCategory.Value = ""
        '
        'lblCategory
        '
        Me.lblCategory.FieldName = Nothing
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.Location = New System.Drawing.Point(7, 56)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(52, 16)
        Me.lblCategory.TabIndex = 53
        Me.lblCategory.Text = "Category"
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
        Me.txtDescription.Location = New System.Drawing.Point(97, 31)
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(580, 18)
        Me.txtDescription.TabIndex = 3
        '
        'txtGroupCode
        '
        Me.txtGroupCode.FieldName = Nothing
        Me.txtGroupCode.Location = New System.Drawing.Point(97, 7)
        Me.txtGroupCode.MendatroryField = False
        Me.txtGroupCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtGroupCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtGroupCode.MyLinkLable1 = Nothing
        Me.txtGroupCode.MyLinkLable2 = Nothing
        Me.txtGroupCode.MyMaxLength = 32767
        Me.txtGroupCode.MyReadOnly = False
        Me.txtGroupCode.Name = "txtGroupCode"
        Me.txtGroupCode.Size = New System.Drawing.Size(264, 20)
        Me.txtGroupCode.TabIndex = 1
        Me.txtGroupCode.Value = ""
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(702, 264)
        Me.SplitContainer1.SplitterDistance = 229
        Me.SplitContainer1.TabIndex = 4
        '
        'FrmGroups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 264)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmGroups"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Groups"
        CType(Me.lblGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLastMaintained, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefaultCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLastMaintained, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtcounter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrefix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategoryDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblGroup As common.Controls.MyLabel
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblLastMaintained As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents chkDefaultCategory As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpLastMaintained As common.Controls.MyDateTimePicker
    Friend WithEvents lblNotes As common.Controls.MyLabel
    Friend WithEvents chkInactive As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtNotes As common.Controls.MyTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtGroupCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCategory As common.UserControls.txtFinder
    Friend WithEvents lblCategory As common.Controls.MyLabel
    Friend WithEvents lblCategoryDesc As common.Controls.MyLabel
    Friend WithEvents txtcounter As common.Controls.MyTextBox
    Friend WithEvents lblPrefix As common.Controls.MyLabel
End Class

