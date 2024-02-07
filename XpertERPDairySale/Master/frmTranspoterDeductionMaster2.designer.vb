<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTranspoterDeductionMaster2
    Inherits FrmMainTranScreen

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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.txtCatgory = New common.UserControls.txtFinder()
        Me.lblCategory = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.txtTranspoterDeductionDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtDeductionCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTranspoterDeductionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadMenu1.Size = New System.Drawing.Size(795, 20)
        Me.RadMenu1.TabIndex = 3
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiImport, Me.rmiExport, Me.rmiClose})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'rmiImport
        '
        Me.rmiImport.AccessibleDescription = "Import"
        Me.rmiImport.AccessibleName = "Import"
        Me.rmiImport.Name = "rmiImport"
        Me.rmiImport.Text = "Import"
        '
        'rmiExport
        '
        Me.rmiExport.AccessibleDescription = "Export"
        Me.rmiExport.AccessibleName = "Export"
        Me.rmiExport.Name = "rmiExport"
        Me.rmiExport.Text = "Export Blank Sheet"
        '
        'rmiClose
        '
        Me.rmiClose.AccessibleDescription = "Close"
        Me.rmiClose.AccessibleName = "Close"
        Me.rmiClose.Name = "rmiClose"
        Me.rmiClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(795, 364)
        Me.SplitContainer1.SplitterDistance = 334
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer6.IsSplitterFixed = True
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtCatgory)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblCategory)
        Me.SplitContainer6.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer6.Panel1.Controls.Add(Me.lblPending)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtTranspoterDeductionDate)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtDeductionCode)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer6.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer6.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer6.Panel1.Controls.Add(Me.txtDesc)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer6.Size = New System.Drawing.Size(795, 334)
        Me.SplitContainer6.SplitterDistance = 80
        Me.SplitContainer6.TabIndex = 0
        '
        'txtCatgory
        '
        Me.txtCatgory.CalculationExpression = Nothing
        Me.txtCatgory.FieldCode = Nothing
        Me.txtCatgory.FieldDesc = Nothing
        Me.txtCatgory.FieldMaxLength = 0
        Me.txtCatgory.FieldName = Nothing
        Me.txtCatgory.isCalculatedField = False
        Me.txtCatgory.IsSourceFromTable = False
        Me.txtCatgory.IsSourceFromValueList = False
        Me.txtCatgory.IsUnique = False
        Me.txtCatgory.Location = New System.Drawing.Point(103, 47)
        Me.txtCatgory.MendatroryField = True
        Me.txtCatgory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatgory.MyLinkLable1 = Nothing
        Me.txtCatgory.MyLinkLable2 = Nothing
        Me.txtCatgory.MyReadOnly = False
        Me.txtCatgory.MyShowMasterFormButton = False
        Me.txtCatgory.Name = "txtCatgory"
        Me.txtCatgory.ReferenceFieldDesc = Nothing
        Me.txtCatgory.ReferenceFieldName = Nothing
        Me.txtCatgory.ReferenceTableName = Nothing
        Me.txtCatgory.Size = New System.Drawing.Size(416, 19)
        Me.txtCatgory.TabIndex = 51
        Me.txtCatgory.Value = ""
        '
        'lblCategory
        '
        Me.lblCategory.CalculationExpression = Nothing
        Me.lblCategory.FieldCode = Nothing
        Me.lblCategory.FieldDesc = Nothing
        Me.lblCategory.FieldMaxLength = 0
        Me.lblCategory.FieldName = Nothing
        Me.lblCategory.isCalculatedField = False
        Me.lblCategory.IsSourceFromTable = False
        Me.lblCategory.IsSourceFromValueList = False
        Me.lblCategory.IsUnique = False
        Me.lblCategory.Location = New System.Drawing.Point(525, 46)
        Me.lblCategory.MendatroryField = False
        Me.lblCategory.MyLinkLable1 = Nothing
        Me.lblCategory.MyLinkLable2 = Nothing
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.ReadOnly = True
        Me.lblCategory.ReferenceFieldDesc = Nothing
        Me.lblCategory.ReferenceFieldName = Nothing
        Me.lblCategory.ReferenceTableName = Nothing
        Me.lblCategory.Size = New System.Drawing.Size(265, 20)
        Me.lblCategory.TabIndex = 52
        Me.lblCategory.TabStop = False
        Me.lblCategory.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(12, 47)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(51, 18)
        Me.MyLabel3.TabIndex = 49
        Me.MyLabel3.Text = "Category"
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(525, 5)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(98, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 44
        Me.lblPending.Visible = False
        '
        'txtTranspoterDeductionDate
        '
        Me.txtTranspoterDeductionDate.CalculationExpression = Nothing
        Me.txtTranspoterDeductionDate.CustomFormat = "dd/MM/yyyy"
        Me.txtTranspoterDeductionDate.FieldCode = Nothing
        Me.txtTranspoterDeductionDate.FieldDesc = Nothing
        Me.txtTranspoterDeductionDate.FieldMaxLength = 0
        Me.txtTranspoterDeductionDate.FieldName = Nothing
        Me.txtTranspoterDeductionDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTranspoterDeductionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTranspoterDeductionDate.isCalculatedField = False
        Me.txtTranspoterDeductionDate.IsSourceFromTable = False
        Me.txtTranspoterDeductionDate.IsSourceFromValueList = False
        Me.txtTranspoterDeductionDate.IsUnique = False
        Me.txtTranspoterDeductionDate.Location = New System.Drawing.Point(413, 6)
        Me.txtTranspoterDeductionDate.MendatroryField = False
        Me.txtTranspoterDeductionDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTranspoterDeductionDate.MyLinkLable1 = Me.RadLabel3
        Me.txtTranspoterDeductionDate.MyLinkLable2 = Nothing
        Me.txtTranspoterDeductionDate.Name = "txtTranspoterDeductionDate"
        Me.txtTranspoterDeductionDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTranspoterDeductionDate.ReferenceFieldDesc = Nothing
        Me.txtTranspoterDeductionDate.ReferenceFieldName = Nothing
        Me.txtTranspoterDeductionDate.ReferenceTableName = Nothing
        Me.txtTranspoterDeductionDate.Size = New System.Drawing.Size(106, 18)
        Me.txtTranspoterDeductionDate.TabIndex = 42
        Me.txtTranspoterDeductionDate.TabStop = False
        Me.txtTranspoterDeductionDate.Text = "17/05/2011"
        Me.txtTranspoterDeductionDate.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(377, 7)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 21
        Me.RadLabel3.Text = "Date"
        '
        'txtDeductionCode
        '
        Me.txtDeductionCode.FieldName = Nothing
        Me.txtDeductionCode.Location = New System.Drawing.Point(103, 6)
        Me.txtDeductionCode.MendatroryField = True
        Me.txtDeductionCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDeductionCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDeductionCode.MyLinkLable1 = Me.RadLabel1
        Me.txtDeductionCode.MyLinkLable2 = Nothing
        Me.txtDeductionCode.MyMaxLength = 32767
        Me.txtDeductionCode.MyReadOnly = False
        Me.txtDeductionCode.Name = "txtDeductionCode"
        Me.txtDeductionCode.Size = New System.Drawing.Size(241, 18)
        Me.txtDeductionCode.TabIndex = 1
        Me.txtDeductionCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(12, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(87, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Deduction Code"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(344, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(19, 18)
        Me.btnReset.TabIndex = 1
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(12, 27)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 19
        Me.RadLabel2.Text = "Description"
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
        Me.txtDesc.Location = New System.Drawing.Point(103, 26)
        Me.txtDesc.MendatroryField = True
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(416, 18)
        Me.txtDesc.TabIndex = 5
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(795, 250)
        Me.gv.TabIndex = 2
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(163, 4)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(81, 18)
        Me.btnPost.TabIndex = 3
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(712, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(83, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'frmTranspoterDeductionMaster2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 384)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmTranspoterDeductionMaster2"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Transporter Deduction Master"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.lblCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTranspoterDeductionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    ' Friend WithEvents UcCustomFields1 As EnumCustomFieldType
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents grdScheme As common.UserControls.MyRadGridView
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtTranspoterDeductionDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtDeductionCode As common.UserControls.txtNavigator
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCatgory As common.UserControls.txtFinder
    Friend WithEvents lblCategory As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
End Class

