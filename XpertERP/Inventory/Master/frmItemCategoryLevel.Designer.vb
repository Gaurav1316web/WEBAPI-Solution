<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemCategoryLevel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemCategoryLevel))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkBinMapping = New common.Controls.MyCheckBox()
        Me.chkMasterPack = New common.Controls.MyCheckBox()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexportItemHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnExportItemDetails = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnImpItemHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnImportItemDetails = New Telerik.WinControls.UI.RadMenuItem()
        Me.txtCategoryLevel = New common.MyNumBox()
        Me.lblCategoryLavel = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.gvCategoryValues = New common.UserControls.MyRadGridView()
        Me.lblItemCategoryCode = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkBinMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMasterPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCategoryLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCategoryLavel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategoryValues, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategoryValues.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(769, 540)
        Me.SplitContainer1.SplitterDistance = 488
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkBinMapping)
        Me.RadGroupBox1.Controls.Add(Me.chkMasterPack)
        Me.RadGroupBox1.Controls.Add(Me.RadMenu1)
        Me.RadGroupBox1.Controls.Add(Me.txtCategoryLevel)
        Me.RadGroupBox1.Controls.Add(Me.lblCategoryLavel)
        Me.RadGroupBox1.Controls.Add(Me.lblDescription)
        Me.RadGroupBox1.Controls.Add(Me.txtDescription)
        Me.RadGroupBox1.Controls.Add(Me.gvCategoryValues)
        Me.RadGroupBox1.Controls.Add(Me.lblItemCategoryCode)
        Me.RadGroupBox1.Controls.Add(Me.btnNew)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 8)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(720, 456)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkBinMapping
        '
        Me.chkBinMapping.Location = New System.Drawing.Point(535, 103)
        Me.chkBinMapping.MyLinkLable1 = Nothing
        Me.chkBinMapping.MyLinkLable2 = Nothing
        Me.chkBinMapping.Name = "chkBinMapping"
        Me.chkBinMapping.Size = New System.Drawing.Size(84, 18)
        Me.chkBinMapping.TabIndex = 181
        Me.chkBinMapping.Tag1 = Nothing
        Me.chkBinMapping.Text = "Bin Mapping"
        '
        'chkMasterPack
        '
        Me.chkMasterPack.Location = New System.Drawing.Point(432, 101)
        Me.chkMasterPack.MyLinkLable1 = Nothing
        Me.chkMasterPack.MyLinkLable2 = Nothing
        Me.chkMasterPack.Name = "chkMasterPack"
        Me.chkMasterPack.Size = New System.Drawing.Size(97, 18)
        Me.chkMasterPack.TabIndex = 180
        Me.chkMasterPack.Tag1 = Nothing
        Me.chkMasterPack.Text = "Master Packing"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(10, 20)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(700, 20)
        Me.RadMenu1.TabIndex = 179
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export"
        Me.RadMenuItem2.AccessibleName = "Export"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexportItemHead, Me.BtnExportItemDetails})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Export"
        '
        'btnexportItemHead
        '
        Me.btnexportItemHead.AccessibleDescription = "Item Category Head"
        Me.btnexportItemHead.AccessibleName = "Item Category Head"
        Me.btnexportItemHead.Name = "btnexportItemHead"
        Me.btnexportItemHead.Text = "Item Category Head"
        '
        'BtnExportItemDetails
        '
        Me.BtnExportItemDetails.AccessibleDescription = "Item Category Details"
        Me.BtnExportItemDetails.AccessibleName = "Item Category Details"
        Me.BtnExportItemDetails.Name = "BtnExportItemDetails"
        Me.BtnExportItemDetails.Text = "Item Category Details"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Import"
        Me.RadMenuItem3.AccessibleName = "Import"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnImpItemHead, Me.BtnImportItemDetails})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Import"
        '
        'BtnImpItemHead
        '
        Me.BtnImpItemHead.AccessibleDescription = "Item Category Head"
        Me.BtnImpItemHead.AccessibleName = "Item Category Head"
        Me.BtnImpItemHead.Name = "BtnImpItemHead"
        Me.BtnImpItemHead.Text = "Item Category Head"
        '
        'BtnImportItemDetails
        '
        Me.BtnImportItemDetails.AccessibleDescription = "Item Category Details"
        Me.BtnImportItemDetails.AccessibleName = "Item Category Details"
        Me.BtnImportItemDetails.Name = "BtnImportItemDetails"
        Me.BtnImportItemDetails.Text = "Item Category Details"
        '
        'txtCategoryLevel
        '
        Me.txtCategoryLevel.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCategoryLevel.CalculationExpression = Nothing
        Me.txtCategoryLevel.DecimalPlaces = 0
        Me.txtCategoryLevel.FieldCode = Nothing
        Me.txtCategoryLevel.FieldDesc = Nothing
        Me.txtCategoryLevel.FieldMaxLength = 0
        Me.txtCategoryLevel.FieldName = Nothing
        Me.txtCategoryLevel.isCalculatedField = False
        Me.txtCategoryLevel.IsSourceFromTable = False
        Me.txtCategoryLevel.IsSourceFromValueList = False
        Me.txtCategoryLevel.IsUnique = False
        Me.txtCategoryLevel.Location = New System.Drawing.Point(152, 101)
        Me.txtCategoryLevel.MendatroryField = True
        Me.txtCategoryLevel.MyLinkLable1 = Me.lblCategoryLavel
        Me.txtCategoryLevel.MyLinkLable2 = Nothing
        Me.txtCategoryLevel.Name = "txtCategoryLevel"
        Me.txtCategoryLevel.ReferenceFieldDesc = Nothing
        Me.txtCategoryLevel.ReferenceFieldName = Nothing
        Me.txtCategoryLevel.ReferenceTableName = Nothing
        Me.txtCategoryLevel.Size = New System.Drawing.Size(274, 20)
        Me.txtCategoryLevel.TabIndex = 3
        Me.txtCategoryLevel.Text = "0"
        Me.txtCategoryLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCategoryLevel.Value = 0.0R
        '
        'lblCategoryLavel
        '
        Me.lblCategoryLavel.FieldName = Nothing
        Me.lblCategoryLavel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategoryLavel.Location = New System.Drawing.Point(18, 101)
        Me.lblCategoryLavel.Name = "lblCategoryLavel"
        Me.lblCategoryLavel.Size = New System.Drawing.Size(82, 16)
        Me.lblCategoryLavel.TabIndex = 178
        Me.lblCategoryLavel.Text = "Category Level"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(18, 77)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 176
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
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
        Me.txtDescription.Location = New System.Drawing.Point(152, 77)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(274, 21)
        Me.txtDescription.TabIndex = 2
        Me.txtDescription.Text = " "
        Me.txtDescription.UseWaitCursor = True
        '
        'gvCategoryValues
        '
        Me.gvCategoryValues.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvCategoryValues.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvCategoryValues.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCategoryValues.EnableCustomFiltering = True
        Me.gvCategoryValues.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvCategoryValues.ForeColor = System.Drawing.Color.Black
        Me.gvCategoryValues.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCategoryValues.Location = New System.Drawing.Point(18, 137)
        '
        'gvCategoryValues
        '
        Me.gvCategoryValues.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvCategoryValues.MasterTemplate.AllowAddNewRow = False
        Me.gvCategoryValues.MasterTemplate.AutoGenerateColumns = False
        Me.gvCategoryValues.MasterTemplate.EnableCustomFiltering = True
        Me.gvCategoryValues.MasterTemplate.EnableGrouping = False
        Me.gvCategoryValues.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCategoryValues.Name = "gvCategoryValues"
        Me.gvCategoryValues.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCategoryValues.ShowHeaderCellButtons = True
        Me.gvCategoryValues.Size = New System.Drawing.Size(689, 294)
        Me.gvCategoryValues.TabIndex = 4
        Me.gvCategoryValues.TabStop = False
        '
        'lblItemCategoryCode
        '
        Me.lblItemCategoryCode.FieldName = Nothing
        Me.lblItemCategoryCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCategoryCode.Location = New System.Drawing.Point(18, 54)
        Me.lblItemCategoryCode.Name = "lblItemCategoryCode"
        Me.lblItemCategoryCode.Size = New System.Drawing.Size(108, 16)
        Me.lblItemCategoryCode.TabIndex = 158
        Me.lblItemCategoryCode.Text = "Item Category Code"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(376, 55)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(152, 54)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblItemCategoryCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 17)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(694, 17)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(81, 17)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'frmItemCategoryLevel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 540)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmItemCategoryLevel"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Category Level"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkBinMapping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMasterPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCategoryLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCategoryLavel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategoryValues.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategoryValues, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCategoryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblItemCategoryCode As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvCategoryValues As common.UserControls.MyRadGridView
    Friend WithEvents lblCategoryLavel As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtCategoryLevel As common.MyNumBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexportItemHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnExportItemDetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnImpItemHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnImportItemDetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkMasterPack As common.Controls.MyCheckBox
    Friend WithEvents chkBinMapping As common.Controls.MyCheckBox
End Class
