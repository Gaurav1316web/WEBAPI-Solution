Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPriceComponantMapping
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
        Dim GridViewMultiComboBoxColumn1 As Telerik.WinControls.UI.GridViewMultiComboBoxColumn = New Telerik.WinControls.UI.GridViewMultiComboBoxColumn()
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim SortDescriptor1 As Telerik.WinControls.Data.SortDescriptor = New Telerik.WinControls.Data.SortDescriptor()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel9 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.fndPrice = New common.UserControls.txtNavigator()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.grdPrice = New common.UserControls.MyRadGridView()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.MenuFile = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.mbtnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.mbtnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtprinciple = New common.Controls.MyLabel()
        Me.txtpri_code = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcCustomFields1 = New ucCustomFields()
        Me.chkTransfer = New System.Windows.Forms.CheckBox()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.grdPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPrice.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MenuFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtprinciple, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(5, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(62, 16)
        Me.RadLabel1.TabIndex = 7
        Me.RadLabel1.Text = "Price Code"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(5, 23)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 6
        Me.RadLabel2.Text = "Description"
        '
        'RadLabel9
        '
        Me.RadLabel9.FieldName = Nothing
        Me.RadLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel9.Location = New System.Drawing.Point(5, 47)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(51, 16)
        Me.RadLabel9.TabIndex = 5
        Me.RadLabel9.Text = "Remarks"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(73, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(321, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(14, 18)
        Me.btnReset.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(845, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'fndPrice
        '
        Me.fndPrice.FieldName = Nothing
        Me.fndPrice.Location = New System.Drawing.Point(86, 2)
        Me.fndPrice.MendatroryField = True
        Me.fndPrice.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndPrice.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndPrice.MyLinkLable1 = Me.RadLabel1
        Me.fndPrice.MyLinkLable2 = Nothing
        Me.fndPrice.MyMaxLength = 32767
        Me.fndPrice.MyReadOnly = False
        Me.fndPrice.Name = "fndPrice"
        Me.fndPrice.Size = New System.Drawing.Size(231, 18)
        Me.fndPrice.TabIndex = 0
        Me.fndPrice.Value = ""
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.grdPrice)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 101)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(888, 309)
        Me.RadGroupBox2.TabIndex = 6
        '
        'grdPrice
        '
        Me.grdPrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdPrice.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPrice.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grdPrice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdPrice.Location = New System.Drawing.Point(10, 20)
        '
        'grdPrice
        '
        Me.grdPrice.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.grdPrice.MasterTemplate.AllowAddNewRow = False
        GridViewMultiComboBoxColumn1.FieldName = "Price Componant Code"
        GridViewMultiComboBoxColumn1.HeaderText = "Price Component Code"
        GridViewMultiComboBoxColumn1.Name = "Price Componant Code"
        GridViewMultiComboBoxColumn1.ReadOnly = True
        GridViewMultiComboBoxColumn1.Width = 150
        GridViewTextBoxColumn1.FieldName = "Description"
        GridViewTextBoxColumn1.HeaderText = "Description"
        GridViewTextBoxColumn1.Name = "Description"
        GridViewTextBoxColumn1.ReadOnly = True
        GridViewTextBoxColumn1.Width = 400
        GridViewTextBoxColumn2.HeaderText = "Price Calculation Method"
        GridViewTextBoxColumn2.Name = "Price Calculation Type"
        GridViewTextBoxColumn2.ReadOnly = True
        GridViewTextBoxColumn2.Width = 150
        GridViewTextBoxColumn3.FieldName = "Amount"
        GridViewTextBoxColumn3.HeaderText = "Amount"
        GridViewTextBoxColumn3.Name = "Amount"
        GridViewTextBoxColumn3.Width = 150
        Me.grdPrice.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewMultiComboBoxColumn1, GridViewTextBoxColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3})
        Me.grdPrice.MasterTemplate.EnableSorting = False
        Me.grdPrice.MasterTemplate.ShowHeaderCellButtons = True
        SortDescriptor1.PropertyName = "Price Component Code"
        Me.grdPrice.MasterTemplate.SortDescriptors.AddRange(New Telerik.WinControls.Data.SortDescriptor() {SortDescriptor1})
        Me.grdPrice.Name = "grdPrice"
        Me.grdPrice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdPrice.ShowGroupPanel = False
        Me.grdPrice.ShowHeaderCellButtons = True
        Me.grdPrice.Size = New System.Drawing.Size(868, 279)
        Me.grdPrice.TabIndex = 0
        Me.grdPrice.TabStop = False
        Me.grdPrice.Text = "RadGridView1"
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(86, 45)
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel9
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(583, 18)
        Me.txtRemarks.TabIndex = 3
        '
        'txtDesc
        '
        Me.txtDesc.CalculationExpression = Nothing
        Me.txtDesc.FieldCode = Nothing
        Me.txtDesc.FieldDesc = Nothing
        Me.txtDesc.FieldMaxLength = 0
        Me.txtDesc.FieldName = Nothing
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.isCalculatedField = False
        Me.txtDesc.IsSourceFromTable = False
        Me.txtDesc.IsSourceFromValueList = False
        Me.txtDesc.IsUnique = False
        Me.txtDesc.Location = New System.Drawing.Point(86, 24)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(583, 18)
        Me.txtDesc.TabIndex = 2
        '
        'MenuFile
        '
        Me.MenuFile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.MenuFile.Location = New System.Drawing.Point(0, 0)
        Me.MenuFile.Name = "MenuFile"
        Me.MenuFile.Size = New System.Drawing.Size(916, 20)
        Me.MenuFile.TabIndex = 1
        Me.MenuFile.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "FIleMenu"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.mbtnExport, Me.mbtnImport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'mbtnExport
        '
        Me.mbtnExport.AccessibleDescription = "Export"
        Me.mbtnExport.AccessibleName = "Export"
        Me.mbtnExport.Name = "mbtnExport"
        Me.mbtnExport.Text = "Export"
        '
        'mbtnImport
        '
        Me.mbtnImport.AccessibleDescription = "Import"
        Me.mbtnImport.AccessibleName = "Import"
        Me.mbtnImport.Name = "mbtnImport"
        Me.mbtnImport.Text = "Import"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(916, 492)
        Me.SplitContainer1.SplitterDistance = 459
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(916, 459)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkTransfer)
        Me.RadPageViewPage1.Controls.Add(Me.txtprinciple)
        Me.RadPageViewPage1.Controls.Add(Me.txtpri_code)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.fndPrice)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.txtDesc)
        Me.RadPageViewPage1.Controls.Add(Me.btnReset)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.txtRemarks)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(65.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(895, 411)
        Me.RadPageViewPage1.Text = "Customer"
        '
        'txtprinciple
        '
        Me.txtprinciple.AutoSize = False
        Me.txtprinciple.BorderVisible = True
        Me.txtprinciple.FieldName = Nothing
        Me.txtprinciple.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprinciple.Location = New System.Drawing.Point(230, 67)
        Me.txtprinciple.Name = "txtprinciple"
        Me.txtprinciple.Size = New System.Drawing.Size(439, 18)
        Me.txtprinciple.TabIndex = 5
        Me.txtprinciple.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtprinciple.TextWrap = False
        '
        'txtpri_code
        '
        Me.txtpri_code.CalculationExpression = Nothing
        Me.txtpri_code.FieldCode = Nothing
        Me.txtpri_code.FieldDesc = Nothing
        Me.txtpri_code.FieldMaxLength = 0
        Me.txtpri_code.FieldName = Nothing
        Me.txtpri_code.isCalculatedField = False
        Me.txtpri_code.IsSourceFromTable = False
        Me.txtpri_code.IsSourceFromValueList = False
        Me.txtpri_code.IsUnique = False
        Me.txtpri_code.Location = New System.Drawing.Point(86, 67)
        Me.txtpri_code.MendatroryField = False
        Me.txtpri_code.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpri_code.MyLinkLable1 = Me.MyLabel1
        Me.txtpri_code.MyLinkLable2 = Me.txtprinciple
        Me.txtpri_code.MyReadOnly = False
        Me.txtpri_code.MyShowMasterFormButton = False
        Me.txtpri_code.Name = "txtpri_code"
        Me.txtpri_code.ReferenceFieldDesc = Nothing
        Me.txtpri_code.ReferenceFieldName = Nothing
        Me.txtpri_code.ReferenceTableName = Nothing
        Me.txtpri_code.Size = New System.Drawing.Size(143, 19)
        Me.txtpri_code.TabIndex = 4
        Me.txtpri_code.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 69)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "Principle"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(1027, 353)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(1027, 353)
        Me.UcCustomFields1.TabIndex = 1
        '
        'chkTransfer
        '
        Me.chkTransfer.AutoSize = True
        Me.chkTransfer.Location = New System.Drawing.Point(351, 3)
        Me.chkTransfer.Name = "chkTransfer"
        Me.chkTransfer.Size = New System.Drawing.Size(67, 17)
        Me.chkTransfer.TabIndex = 9
        Me.chkTransfer.Text = "Transfer"
        Me.chkTransfer.UseVisualStyleBackColor = True
        '
        'FrmPriceComponantMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(916, 512)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuFile)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmPriceComponantMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Price Component Mapping"
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.grdPrice.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MenuFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtprinciple, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents grdPrice As common.UserControls.MyRadGridView
    Friend WithEvents MenuFile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mbtnExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents mbtnImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents fndPrice As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtprinciple As common.Controls.MyLabel
    Friend WithEvents txtpri_code As common.UserControls.txtFinder
    Friend WithEvents chkTransfer As System.Windows.Forms.CheckBox
End Class

