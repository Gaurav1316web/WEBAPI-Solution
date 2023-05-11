Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIncomeTaxSlab
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
        Me.cmbappliedfor = New common.Controls.MyComboBox()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.TxtDesp = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu2 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExportHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmExportDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImportHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmImportDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cmbappliedfor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbappliedfor)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel18)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxtDesp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemCategoryCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(643, 406)
        Me.SplitContainer1.SplitterDistance = 71
        Me.SplitContainer1.TabIndex = 0
        '
        'cmbappliedfor
        '
        Me.cmbappliedfor.AutoCompleteDisplayMember = Nothing
        Me.cmbappliedfor.AutoCompleteValueMember = Nothing
        Me.cmbappliedfor.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbappliedfor.Location = New System.Drawing.Point(448, 46)
        Me.cmbappliedfor.MendatroryField = True
        Me.cmbappliedfor.MyLinkLable1 = Me.MyLabel18
        Me.cmbappliedfor.MyLinkLable2 = Nothing
        Me.cmbappliedfor.Name = "cmbappliedfor"
        Me.cmbappliedfor.Size = New System.Drawing.Size(135, 20)
        Me.cmbappliedfor.TabIndex = 198
        '
        'MyLabel18
        '
        Me.MyLabel18.Location = New System.Drawing.Point(375, 46)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel18.TabIndex = 197
        Me.MyLabel18.Text = "Applied For "
        '
        'TxtDesp
        '
        Me.TxtDesp.AutoSize = False
        Me.TxtDesp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesp.Location = New System.Drawing.Point(95, 43)
        Me.TxtDesp.MaxLength = 100
        Me.TxtDesp.MendatroryField = False
        Me.TxtDesp.Multiline = True
        Me.TxtDesp.MyLinkLable1 = Me.lblDescription
        Me.TxtDesp.MyLinkLable2 = Nothing
        Me.TxtDesp.Name = "TxtDesp"
        Me.TxtDesp.Size = New System.Drawing.Size(274, 21)
        Me.TxtDesp.TabIndex = 196
        Me.TxtDesp.Text = " "
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(23, 43)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(66, 16)
        Me.lblDescription.TabIndex = 195
        Me.lblDescription.Text = "Description "
        '
        'btnnew
        '
        Me.btnnew.Image = My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(355, 16)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 21)
        Me.btnnew.TabIndex = 194
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(94, 16)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Nothing
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 32767
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(261, 21)
        Me.txtcode.TabIndex = 193
        Me.txtcode.Value = ""
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(23, 21)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(50, 16)
        Me.lblItemCategoryCode.TabIndex = 192
        Me.lblItemCategoryCode.Text = "IT Code "
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gv)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer2.Size = New System.Drawing.Size(643, 331)
        Me.SplitContainer2.SplitterDistance = 288
        Me.SplitContainer2.TabIndex = 0
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.EnableCustomFiltering = True
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AutoGenerateColumns = False
        Me.gv.MasterTemplate.EnableCustomFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.Size = New System.Drawing.Size(643, 288)
        Me.gv.TabIndex = 5
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 8
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(569, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 10
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(80, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 9
        Me.btndelete.Text = "Delete"
        '
        'RadMenu2
        '
        Me.RadMenu2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu2.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu2.Name = "RadMenu2"
        Me.RadMenu2.Size = New System.Drawing.Size(643, 20)
        Me.RadMenu2.TabIndex = 63
        Me.RadMenu2.Text = "RadMenu2"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "File"
        Me.RadMenuItem3.AccessibleName = "File"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExport, Me.rmImport})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "File"
        '
        'rmExport
        '
        Me.rmExport.AccessibleDescription = "RadMenuItem1"
        Me.rmExport.AccessibleName = "RadMenuItem1"
        Me.rmExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExportHead, Me.rmExportDetail})
        Me.rmExport.Name = "rmExport"
        Me.rmExport.Text = "Export"
        '
        'rmExportHead
        '
        Me.rmExportHead.AccessibleDescription = "Export Head"
        Me.rmExportHead.AccessibleName = "Export Head"
        Me.rmExportHead.Name = "rmExportHead"
        Me.rmExportHead.Text = "Export Head"
        '
        'rmExportDetail
        '
        Me.rmExportDetail.AccessibleDescription = "Export Detail"
        Me.rmExportDetail.AccessibleName = "Export Detail"
        Me.rmExportDetail.Name = "rmExportDetail"
        Me.rmExportDetail.Text = "Export Detail"
        '
        'rmImport
        '
        Me.rmImport.AccessibleDescription = "RadMenuItem2"
        Me.rmImport.AccessibleName = "RadMenuItem2"
        Me.rmImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmImportHead, Me.rmImportDetail})
        Me.rmImport.Name = "rmImport"
        Me.rmImport.Text = "Import"
        '
        'rmImportHead
        '
        Me.rmImportHead.AccessibleDescription = "RadMenuItem1"
        Me.rmImportHead.AccessibleName = "RadMenuItem1"
        Me.rmImportHead.Name = "rmImportHead"
        Me.rmImportHead.Text = "Import Head"
        '
        'rmImportDetail
        '
        Me.rmImportDetail.AccessibleDescription = "RadMenuItem2"
        Me.rmImportDetail.AccessibleName = "RadMenuItem2"
        Me.rmImportDetail.Name = "rmImportDetail"
        Me.rmImportDetail.Text = "Import Detail"
        '
        'frmIncomeTaxSlab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 426)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu2)
        Me.Name = "frmIncomeTaxSlab"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmIncomeTaxSlab"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cmbappliedfor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDesp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents RadMenu2 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents TxtDesp As common.Controls.MyTextBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents cmbappliedfor As common.Controls.MyComboBox
    Friend WithEvents rmExportHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExportDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImportHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmImportDetail As Telerik.WinControls.UI.RadMenuItem
End Class
