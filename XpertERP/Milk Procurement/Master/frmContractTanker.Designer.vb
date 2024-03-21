<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContractTanker
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkVendorSelect = New common.Controls.MyRadioButton()
        Me.chkVendorAll = New common.Controls.MyRadioButton()
        Me.fndVendorNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.txtTankerCode = New common.UserControls.txtNavigator()
        Me.btnGO = New Telerik.WinControls.UI.RadButton()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.txtTankerNo = New common.Controls.MyTextBox()
        Me.lblpan = New common.Controls.MyLabel()
        Me.txtChamborNo = New common.MyNumBox()
        Me.MyLabel49 = New common.Controls.MyLabel()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.GvVendor = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuImportVendor = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExportVendor = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChamborNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel49, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(3, 18)
        '
        '
        '
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(482, 362)
        Me.gv.TabIndex = 63
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gv)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(488, 383)
        Me.GroupBox1.TabIndex = 64
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Chamber Desc"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndVendorNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMCCCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTankerCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtChamborNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblpan)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel49)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(993, 497)
        Me.SplitContainer1.SplitterDistance = 81
        Me.SplitContainer1.TabIndex = 87
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(745, 88)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(291, 20)
        Me.lblVendorName.TabIndex = 338
        Me.lblVendorName.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkVendorSelect)
        Me.Panel1.Controls.Add(Me.chkVendorAll)
        Me.Panel1.Location = New System.Drawing.Point(502, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(480, 29)
        Me.Panel1.TabIndex = 0
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(243, 11)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 1
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(194, 11)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 0
        Me.chkVendorAll.Text = "All"
        '
        'fndVendorNo
        '
        Me.fndVendorNo.CalculationExpression = Nothing
        Me.fndVendorNo.FieldCode = Nothing
        Me.fndVendorNo.FieldDesc = Nothing
        Me.fndVendorNo.FieldMaxLength = 0
        Me.fndVendorNo.FieldName = Nothing
        Me.fndVendorNo.isCalculatedField = False
        Me.fndVendorNo.IsSourceFromTable = False
        Me.fndVendorNo.IsSourceFromValueList = False
        Me.fndVendorNo.IsUnique = False
        Me.fndVendorNo.Location = New System.Drawing.Point(521, 88)
        Me.fndVendorNo.MendatroryField = False
        Me.fndVendorNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVendorNo.MyLinkLable1 = Me.RadLabel2
        Me.fndVendorNo.MyLinkLable2 = Nothing
        Me.fndVendorNo.MyReadOnly = False
        Me.fndVendorNo.MyShowMasterFormButton = False
        Me.fndVendorNo.Name = "fndVendorNo"
        Me.fndVendorNo.ReferenceFieldDesc = Nothing
        Me.fndVendorNo.ReferenceFieldName = Nothing
        Me.fndVendorNo.ReferenceTableName = Nothing
        Me.fndVendorNo.Size = New System.Drawing.Size(217, 20)
        Me.fndVendorNo.TabIndex = 336
        Me.fndVendorNo.Value = ""
        Me.fndVendorNo.Visible = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(425, 90)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 337
        Me.RadLabel2.Text = "Vendor No"
        Me.RadLabel2.Visible = False
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMCCCode.Location = New System.Drawing.Point(12, 12)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(75, 16)
        Me.lblMCCCode.TabIndex = 12
        Me.lblMCCCode.Text = "Tanker Code"
        '
        'txtTankerCode
        '
        Me.txtTankerCode.FieldName = Nothing
        Me.txtTankerCode.Location = New System.Drawing.Point(108, 7)
        Me.txtTankerCode.MendatroryField = True
        Me.txtTankerCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTankerCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtTankerCode.MyLinkLable1 = Me.lblMCCCode
        Me.txtTankerCode.MyLinkLable2 = Nothing
        Me.txtTankerCode.MyMaxLength = 30
        Me.txtTankerCode.MyReadOnly = False
        Me.txtTankerCode.Name = "txtTankerCode"
        Me.txtTankerCode.Size = New System.Drawing.Size(199, 21)
        Me.txtTankerCode.TabIndex = 10
        Me.txtTankerCode.Value = ""
        '
        'btnGO
        '
        Me.btnGO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGO.Location = New System.Drawing.Point(352, 52)
        Me.btnGO.Name = "btnGO"
        Me.btnGO.Size = New System.Drawing.Size(73, 19)
        Me.btnGO.TabIndex = 114
        Me.btnGO.Text = ">>"
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.rbtnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnReset.Location = New System.Drawing.Point(307, 7)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(18, 21)
        Me.rbtnReset.TabIndex = 11
        '
        'txtTankerNo
        '
        Me.txtTankerNo.CalculationExpression = Nothing
        Me.txtTankerNo.FieldCode = Nothing
        Me.txtTankerNo.FieldDesc = Nothing
        Me.txtTankerNo.FieldMaxLength = 0
        Me.txtTankerNo.FieldName = Nothing
        Me.txtTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerNo.isCalculatedField = False
        Me.txtTankerNo.IsSourceFromTable = False
        Me.txtTankerNo.IsSourceFromValueList = False
        Me.txtTankerNo.IsUnique = False
        Me.txtTankerNo.Location = New System.Drawing.Point(108, 31)
        Me.txtTankerNo.MaxLength = 15
        Me.txtTankerNo.MendatroryField = False
        Me.txtTankerNo.MyLinkLable1 = Me.lblpan
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(217, 18)
        Me.txtTankerNo.TabIndex = 112
        '
        'lblpan
        '
        Me.lblpan.FieldName = Nothing
        Me.lblpan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpan.Location = New System.Drawing.Point(12, 33)
        Me.lblpan.Name = "lblpan"
        Me.lblpan.Size = New System.Drawing.Size(59, 16)
        Me.lblpan.TabIndex = 113
        Me.lblpan.Text = "Tanker No"
        '
        'txtChamborNo
        '
        Me.txtChamborNo.BackColor = System.Drawing.Color.White
        Me.txtChamborNo.CalculationExpression = Nothing
        Me.txtChamborNo.DecimalPlaces = 0
        Me.txtChamborNo.FieldCode = Nothing
        Me.txtChamborNo.FieldDesc = Nothing
        Me.txtChamborNo.FieldMaxLength = 0
        Me.txtChamborNo.FieldName = Nothing
        Me.txtChamborNo.isCalculatedField = False
        Me.txtChamborNo.IsSourceFromTable = False
        Me.txtChamborNo.IsSourceFromValueList = False
        Me.txtChamborNo.IsUnique = False
        Me.txtChamborNo.Location = New System.Drawing.Point(108, 51)
        Me.txtChamborNo.MendatroryField = False
        Me.txtChamborNo.MyLinkLable1 = Nothing
        Me.txtChamborNo.MyLinkLable2 = Nothing
        Me.txtChamborNo.Name = "txtChamborNo"
        Me.txtChamborNo.ReferenceFieldDesc = Nothing
        Me.txtChamborNo.ReferenceFieldName = Nothing
        Me.txtChamborNo.ReferenceTableName = Nothing
        Me.txtChamborNo.Size = New System.Drawing.Size(217, 20)
        Me.txtChamborNo.TabIndex = 109
        Me.txtChamborNo.Text = "0"
        Me.txtChamborNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtChamborNo.Value = 0R
        '
        'MyLabel49
        '
        Me.MyLabel49.FieldName = Nothing
        Me.MyLabel49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel49.Location = New System.Drawing.Point(12, 51)
        Me.MyLabel49.Name = "MyLabel49"
        Me.MyLabel49.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel49.TabIndex = 110
        Me.MyLabel49.Text = "No Of Chamber"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer3.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer3.Size = New System.Drawing.Size(993, 412)
        Me.SplitContainer3.SplitterDistance = 383
        Me.SplitContainer3.TabIndex = 65
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(993, 383)
        Me.SplitContainer2.SplitterDistance = 488
        Me.SplitContainer2.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.GvVendor)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Vendor"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(501, 383)
        Me.RadGroupBox2.TabIndex = 115
        Me.RadGroupBox2.Text = "Vendor"
        '
        'GvVendor
        '
        Me.GvVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GvVendor.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.GvVendor.MasterTemplate.AllowAddNewRow = False
        Me.GvVendor.MasterTemplate.EnableFiltering = True
        Me.GvVendor.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.GvVendor.MasterTemplate.ShowHeaderCellButtons = True
        Me.GvVendor.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.GvVendor.MyStopExport = False
        Me.GvVendor.Name = "GvVendor"
        Me.GvVendor.ShowGroupPanel = False
        Me.GvVendor.ShowHeaderCellButtons = True
        Me.GvVendor.Size = New System.Drawing.Size(481, 353)
        Me.GvVendor.TabIndex = 2
        Me.GvVendor.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(905, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 19)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(77, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(70, 19)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(4, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(73, 19)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'rdmenufile
        '
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenufile})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(993, 20)
        Me.rdmenufile.TabIndex = 2
        '
        'RadMenufile
        '
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuImport, Me.mnuExport})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        '
        'mnuImport
        '
        Me.mnuImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuImportVendor})
        Me.mnuImport.Name = "mnuImport"
        Me.mnuImport.Text = "Import"
        '
        'mnuImportVendor
        '
        Me.mnuImportVendor.Name = "mnuImportVendor"
        Me.mnuImportVendor.Text = "Import Vendor "
        '
        'mnuExport
        '
        Me.mnuExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuExportVendor})
        Me.mnuExport.Name = "mnuExport"
        Me.mnuExport.Text = "Export"
        '
        'mnuExportVendor
        '
        Me.mnuExportVendor.Name = "mnuExportVendor"
        Me.mnuExportVendor.Text = "Export Vendor"
        '
        'frmContractTanker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmContractTanker"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Contractor Tanker"
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChamborNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel49, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.GvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtTankerCode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel49 As common.Controls.MyLabel
    Friend WithEvents txtChamborNo As common.MyNumBox
    Friend WithEvents txtTankerNo As common.Controls.MyTextBox
    Friend WithEvents lblpan As common.Controls.MyLabel
    Friend WithEvents btnGO As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents fndVendorNo As common.UserControls.txtFinder
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents GvVendor As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents mnuImportVendor As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExportVendor As Telerik.WinControls.UI.RadMenuItem
End Class

