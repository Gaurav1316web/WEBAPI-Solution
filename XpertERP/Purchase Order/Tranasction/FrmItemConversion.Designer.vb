<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemConversion
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CmbConversion = New common.Controls.MyComboBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblItemDesc = New common.Controls.MyTextBox()
        Me.frnItemCode = New common.UserControls.txtFinder()
        Me.lblItemCode = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtDocNo = New common.UserControls.txtNavigator()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnAddNew = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RmiExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.CmbConversion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.CmbConversion)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.frnItemCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblItemCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDocNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnAddNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(924, 439)
        Me.SplitContainer1.SplitterDistance = 409
        Me.SplitContainer1.TabIndex = 0
        '
        'CmbConversion
        '
        Me.CmbConversion.AutoCompleteDisplayMember = Nothing
        Me.CmbConversion.AutoCompleteValueMember = Nothing
        Me.CmbConversion.CalculationExpression = Nothing
        Me.CmbConversion.DropDownAnimationEnabled = True
        Me.CmbConversion.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbConversion.FieldCode = Nothing
        Me.CmbConversion.FieldDesc = Nothing
        Me.CmbConversion.FieldMaxLength = 0
        Me.CmbConversion.FieldName = Nothing
        Me.CmbConversion.isCalculatedField = False
        Me.CmbConversion.IsSourceFromTable = False
        Me.CmbConversion.IsSourceFromValueList = False
        Me.CmbConversion.IsUnique = False
        Me.CmbConversion.Location = New System.Drawing.Point(138, 82)
        Me.CmbConversion.MendatroryField = True
        Me.CmbConversion.MyLinkLable1 = Me.MyLabel5
        Me.CmbConversion.MyLinkLable2 = Nothing
        Me.CmbConversion.Name = "CmbConversion"
        Me.CmbConversion.ReferenceFieldDesc = Nothing
        Me.CmbConversion.ReferenceFieldName = Nothing
        Me.CmbConversion.ReferenceTableName = Nothing
        Me.CmbConversion.Size = New System.Drawing.Size(347, 20)
        Me.CmbConversion.TabIndex = 72
        Me.CmbConversion.Text = "MyComboBox1"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Location = New System.Drawing.Point(14, 83)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(89, 18)
        Me.MyLabel5.TabIndex = 71
        Me.MyLabel5.Text = "Conversion Type"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(570, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 22)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 4
        '
        'lblItemDesc
        '
        Me.lblItemDesc.CalculationExpression = Nothing
        Me.lblItemDesc.FieldCode = Nothing
        Me.lblItemDesc.FieldDesc = Nothing
        Me.lblItemDesc.FieldMaxLength = 0
        Me.lblItemDesc.FieldName = Nothing
        Me.lblItemDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemDesc.isCalculatedField = False
        Me.lblItemDesc.IsSourceFromTable = False
        Me.lblItemDesc.IsSourceFromValueList = False
        Me.lblItemDesc.IsUnique = False
        Me.lblItemDesc.Location = New System.Drawing.Point(391, 34)
        Me.lblItemDesc.MaxLength = 200
        Me.lblItemDesc.MendatroryField = False
        Me.lblItemDesc.MyLinkLable1 = Nothing
        Me.lblItemDesc.MyLinkLable2 = Nothing
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.ReferenceFieldDesc = Nothing
        Me.lblItemDesc.ReferenceFieldName = Nothing
        Me.lblItemDesc.ReferenceTableName = Nothing
        Me.lblItemDesc.Size = New System.Drawing.Size(439, 18)
        Me.lblItemDesc.TabIndex = 11
        Me.lblItemDesc.TabStop = False
        '
        'frnItemCode
        '
        Me.frnItemCode.CalculationExpression = Nothing
        Me.frnItemCode.FieldCode = Nothing
        Me.frnItemCode.FieldDesc = Nothing
        Me.frnItemCode.FieldMaxLength = 0
        Me.frnItemCode.FieldName = Nothing
        Me.frnItemCode.isCalculatedField = False
        Me.frnItemCode.IsSourceFromTable = False
        Me.frnItemCode.IsSourceFromValueList = False
        Me.frnItemCode.IsUnique = False
        Me.frnItemCode.Location = New System.Drawing.Point(138, 34)
        Me.frnItemCode.MendatroryField = True
        Me.frnItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frnItemCode.MyLinkLable1 = Me.lblItemCode
        Me.frnItemCode.MyLinkLable2 = Nothing
        Me.frnItemCode.MyReadOnly = False
        Me.frnItemCode.MyShowMasterFormButton = False
        Me.frnItemCode.Name = "frnItemCode"
        Me.frnItemCode.ReferenceFieldDesc = Nothing
        Me.frnItemCode.ReferenceFieldName = Nothing
        Me.frnItemCode.ReferenceTableName = Nothing
        Me.frnItemCode.Size = New System.Drawing.Size(236, 19)
        Me.frnItemCode.TabIndex = 10
        Me.frnItemCode.Value = ""
        '
        'lblItemCode
        '
        Me.lblItemCode.FieldName = Nothing
        Me.lblItemCode.Location = New System.Drawing.Point(14, 34)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(58, 18)
        Me.lblItemCode.TabIndex = 14
        Me.lblItemCode.Text = "Item Code"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(14, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(74, 16)
        Me.RadLabel1.TabIndex = 20
        Me.RadLabel1.Text = "Document no"
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
        Me.txtDesc.Location = New System.Drawing.Point(138, 58)
        Me.txtDesc.MaxLength = 200
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel3
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(692, 18)
        Me.txtDesc.TabIndex = 13
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(14, 59)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel3.TabIndex = 13
        Me.RadLabel3.Text = "Description"
        '
        'txtDocNo
        '
        Me.txtDocNo.FieldName = Nothing
        Me.txtDocNo.Location = New System.Drawing.Point(139, 8)
        Me.txtDocNo.MendatroryField = False
        Me.txtDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDocNo.MyLinkLable1 = Me.RadLabel1
        Me.txtDocNo.MyLinkLable2 = Nothing
        Me.txtDocNo.MyMaxLength = 30
        Me.txtDocNo.MyReadOnly = False
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(252, 20)
        Me.txtDocNo.TabIndex = 0
        Me.txtDocNo.Value = ""
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 115)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(914, 291)
        Me.RadGroupBox2.TabIndex = 15
        Me.RadGroupBox2.Text = "Item Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(894, 261)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'btnAddNew
        '
        Me.btnAddNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnAddNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNew.Location = New System.Drawing.Point(391, 8)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(20, 21)
        Me.btnAddNew.TabIndex = 1
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(152, 1)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(80, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 22)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(227, 1)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(69, 22)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        Me.btnPost.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(847, 2)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(69, 22)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RmiExport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RmiExport
        '
        Me.RmiExport.AccessibleDescription = "RadMenuItem2"
        Me.RmiExport.AccessibleName = "RadMenuItem2"
        Me.RmiExport.Name = "RmiExport"
        Me.RmiExport.Text = "Export excel"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(924, 439)
        Me.Panel1.TabIndex = 4
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(924, 20)
        Me.RadMenu1.TabIndex = 3
        '
        'FrmItemConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 459)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmItemConversion"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Conversion"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.CmbConversion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAddNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtDocNo As common.UserControls.txtNavigator
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnAddNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RmiExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblItemDesc As common.Controls.MyTextBox
    Friend WithEvents frnItemCode As common.UserControls.txtFinder
    Friend WithEvents lblItemCode As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents CmbConversion As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
End Class

