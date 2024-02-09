<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingDetails
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
        Me.GvVendor = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.gv = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.dgvprogram = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.rbtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGO = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cboScreen = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.cboModule = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.mnuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuImportVendor = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mnuExportVendor = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenufile = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvprogram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cboScreen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GvVendor
        '
        Me.GvVendor.AllowAddNewRow = False
        Me.GvVendor.EnableFiltering = True
        Me.GvVendor.ShowHeaderCellButtons = True
        '
        'gv
        '
        Me.gv.ShowHeaderCellButtons = True
        '
        'dgvprogram
        '
        Me.dgvprogram.AllowAddNewRow = False
        Me.dgvprogram.AllowColumnReorder = False
        Me.dgvprogram.AllowColumnResize = False
        Me.dgvprogram.AllowDeleteRow = False
        Me.dgvprogram.AllowDragToGroup = False
        Me.dgvprogram.EnableFiltering = True
        Me.dgvprogram.EnableGrouping = False
        Me.dgvprogram.ShowHeaderCellButtons = True
        '
        'rbtnReset
        '
        Me.rbtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnReset.Image = Global.XpertERPAdminServices.My.Resources.Resources._new
        Me.rbtnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnReset.Location = New System.Drawing.Point(316, 7)
        Me.rbtnReset.Name = "rbtnReset"
        Me.rbtnReset.Size = New System.Drawing.Size(18, 21)
        Me.rbtnReset.TabIndex = 11
        '
        'btnGO
        '
        Me.btnGO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGO.Location = New System.Drawing.Point(319, 34)
        Me.btnGO.Name = "btnGO"
        Me.btnGO.Size = New System.Drawing.Size(73, 19)
        Me.btnGO.TabIndex = 114
        Me.btnGO.Text = ">>"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboScreen)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboModule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGO)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnReset)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer1.Size = New System.Drawing.Size(993, 497)
        Me.SplitContainer1.SplitterDistance = 81
        Me.SplitContainer1.TabIndex = 87
        '
        'cboScreen
        '
        Me.cboScreen.CalculationExpression = Nothing
        Me.cboScreen.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboScreen.FieldCode = Nothing
        Me.cboScreen.FieldDesc = Nothing
        Me.cboScreen.FieldMaxLength = 0
        Me.cboScreen.FieldName = Nothing
        Me.cboScreen.isCalculatedField = False
        Me.cboScreen.IsSourceFromTable = False
        Me.cboScreen.IsSourceFromValueList = False
        Me.cboScreen.IsUnique = False
        Me.cboScreen.Location = New System.Drawing.Point(83, 32)
        Me.cboScreen.MendatroryField = False
        Me.cboScreen.MyLinkLable1 = Me.MyLabel2
        Me.cboScreen.MyLinkLable2 = Nothing
        Me.cboScreen.Name = "cboScreen"
        Me.cboScreen.ReferenceFieldDesc = Nothing
        Me.cboScreen.ReferenceFieldName = Nothing
        Me.cboScreen.ReferenceTableName = Nothing
        Me.cboScreen.Size = New System.Drawing.Size(226, 20)
        Me.cboScreen.TabIndex = 116
        Me.cboScreen.Text = " "
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 34)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel2.TabIndex = 118
        Me.MyLabel2.Text = "Screen"
        '
        'cboModule
        '
        Me.cboModule.CalculationExpression = Nothing
        Me.cboModule.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboModule.FieldCode = Nothing
        Me.cboModule.FieldDesc = Nothing
        Me.cboModule.FieldMaxLength = 0
        Me.cboModule.FieldName = Nothing
        Me.cboModule.isCalculatedField = False
        Me.cboModule.IsSourceFromTable = False
        Me.cboModule.IsSourceFromValueList = False
        Me.cboModule.IsUnique = False
        Me.cboModule.Location = New System.Drawing.Point(84, 7)
        Me.cboModule.MendatroryField = False
        Me.cboModule.MyLinkLable1 = Me.MyLabel1
        Me.cboModule.MyLinkLable2 = Nothing
        Me.cboModule.Name = "cboModule"
        Me.cboModule.ReferenceFieldDesc = Nothing
        Me.cboModule.ReferenceFieldName = Nothing
        Me.cboModule.ReferenceTableName = Nothing
        Me.cboModule.Size = New System.Drawing.Size(226, 20)
        Me.cboModule.TabIndex = 115
        Me.cboModule.Text = " "
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 9)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel1.TabIndex = 117
        Me.MyLabel1.Text = "Module"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowColumnReorder = False
        Me.gv1.MasterTemplate.AllowColumnResize = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(993, 412)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'rdmenufile
        '
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(993, 20)
        Me.rdmenufile.TabIndex = 2
        '
        'mnuImport
        '
        Me.mnuImport.AccessibleDescription = "Import"
        Me.mnuImport.AccessibleName = "Import"
        Me.mnuImport.Name = "mnuImport"
        Me.mnuImport.Text = "Import"
        '
        'mnuImportVendor
        '
        Me.mnuImportVendor.AccessibleDescription = "Import Vendor "
        Me.mnuImportVendor.AccessibleName = "Import Vendor "
        Me.mnuImportVendor.Name = "mnuImportVendor"
        Me.mnuImportVendor.Text = "Import Vendor "
        '
        'mnuExport
        '
        Me.mnuExport.AccessibleDescription = "Export"
        Me.mnuExport.AccessibleName = "Export"
        Me.mnuExport.Name = "mnuExport"
        Me.mnuExport.Text = "Export"
        '
        'mnuExportVendor
        '
        Me.mnuExportVendor.AccessibleDescription = "Export Vendor"
        Me.mnuExportVendor.AccessibleName = "Export Vendor"
        Me.mnuExportVendor.Name = "mnuExportVendor"
        Me.mnuExportVendor.Text = "Export Vendor"
        '
        'RadMenufile
        '
        Me.RadMenufile.AccessibleDescription = "File"
        Me.RadMenufile.AccessibleName = "File"
        Me.RadMenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mnuImport, Me.mnuExport})
        Me.RadMenufile.Name = "RadMenufile"
        Me.RadMenufile.Text = "File"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(917, 59)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(73, 19)
        Me.btnClose.TabIndex = 119
        Me.btnClose.Text = "Close"
        '
        'frmSettingDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmSettingDetails"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting Details"
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvprogram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cboScreen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GvVendor As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents gv As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents dgvprogram As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents rbtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGO As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cboScreen As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cboModule As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents mnuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuImportVendor As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mnuExportVendor As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenufile As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
End Class

