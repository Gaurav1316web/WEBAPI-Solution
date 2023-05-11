<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaxRates
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
        Dim GridViewTextBoxColumn1 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewDecimalColumn1 As Telerik.WinControls.UI.GridViewDecimalColumn = New Telerik.WinControls.UI.GridViewDecimalColumn()
        Dim GridViewTextBoxColumn2 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn3 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Dim GridViewTextBoxColumn4 As Telerik.WinControls.UI.GridViewTextBoxColumn = New Telerik.WinControls.UI.GridViewTextBoxColumn()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuExit = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkGSTActive = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkIsGLReadonly = New common.Controls.MyCheckBox()
        Me.findTaxAuthority = New common.UserControls.txtNavigator()
        Me.lblTaxAuthority = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.gvTaxRates = New common.UserControls.MyRadGridView()
        Me.ddlTransType = New common.Controls.MyComboBox()
        Me.lblTransType = New common.Controls.MyLabel()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnAdd = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkGSTActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsGLReadonly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTaxAuthority, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTaxRates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTaxRates.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(586, 20)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "File"
        Me.RadMenuItem1.AccessibleName = "File"
        Me.RadMenuItem1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuExport, Me.menuImport, Me.menuExit})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "File"
        '
        'menuExport
        '
        Me.menuExport.AccessibleDescription = "Export"
        Me.menuExport.AccessibleName = "Export"
        Me.menuExport.Name = "menuExport"
        Me.menuExport.Text = "Export"
        '
        'menuImport
        '
        Me.menuImport.AccessibleDescription = "Import"
        Me.menuImport.AccessibleName = "Import"
        Me.menuImport.Name = "menuImport"
        Me.menuImport.Text = "Import"
        '
        'menuExit
        '
        Me.menuExit.AccessibleDescription = "Exit"
        Me.menuExit.AccessibleName = "Exit"
        Me.menuExit.Name = "menuExit"
        Me.menuExit.Text = "Exit"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkGSTActive)
        Me.RadGroupBox1.Controls.Add(Me.chkIsGLReadonly)
        Me.RadGroupBox1.Controls.Add(Me.findTaxAuthority)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.gvTaxRates)
        Me.RadGroupBox1.Controls.Add(Me.ddlTransType)
        Me.RadGroupBox1.Controls.Add(Me.lblTransType)
        Me.RadGroupBox1.Controls.Add(Me.lblTaxAuthority)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(586, 396)
        Me.RadGroupBox1.TabIndex = 0
        '
        'chkGSTActive
        '
        Me.chkGSTActive.Location = New System.Drawing.Point(350, 7)
        Me.chkGSTActive.Name = "chkGSTActive"
        Me.chkGSTActive.Size = New System.Drawing.Size(51, 18)
        Me.chkGSTActive.TabIndex = 6
        Me.chkGSTActive.Text = "Active"
        '
        'chkIsGLReadonly
        '
        Me.chkIsGLReadonly.Location = New System.Drawing.Point(350, 29)
        Me.chkIsGLReadonly.MyLinkLable1 = Nothing
        Me.chkIsGLReadonly.MyLinkLable2 = Nothing
        Me.chkIsGLReadonly.Name = "chkIsGLReadonly"
        Me.chkIsGLReadonly.Size = New System.Drawing.Size(96, 18)
        Me.chkIsGLReadonly.TabIndex = 5
        Me.chkIsGLReadonly.Tag1 = Nothing
        Me.chkIsGLReadonly.Text = "Is GL ReadOnly"
        Me.chkIsGLReadonly.Visible = False
        '
        'findTaxAuthority
        '
        Me.findTaxAuthority.FieldName = Nothing
        Me.findTaxAuthority.Location = New System.Drawing.Point(111, 7)
        Me.findTaxAuthority.MendatroryField = True
        Me.findTaxAuthority.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.findTaxAuthority.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.findTaxAuthority.MyLinkLable1 = Me.lblTaxAuthority
        Me.findTaxAuthority.MyLinkLable2 = Nothing
        Me.findTaxAuthority.MyMaxLength = 32767
        Me.findTaxAuthority.MyReadOnly = False
        Me.findTaxAuthority.Name = "findTaxAuthority"
        Me.findTaxAuthority.Size = New System.Drawing.Size(216, 20)
        Me.findTaxAuthority.TabIndex = 0
        Me.findTaxAuthority.Value = ""
        '
        'lblTaxAuthority
        '
        Me.lblTaxAuthority.FieldName = Nothing
        Me.lblTaxAuthority.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTaxAuthority.Location = New System.Drawing.Point(10, 8)
        Me.lblTaxAuthority.Name = "lblTaxAuthority"
        Me.lblTaxAuthority.Size = New System.Drawing.Size(73, 16)
        Me.lblTaxAuthority.TabIndex = 4
        Me.lblTaxAuthority.Text = "Tax Authority"
        '
        'btnReset
        '
        Me.btnReset.Image = Global.XpertERPCommanServices.My.Resources.Resources._new
        Me.btnReset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReset.Location = New System.Drawing.Point(327, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(20, 20)
        Me.btnReset.TabIndex = 1
        '
        'gvTaxRates
        '
        Me.gvTaxRates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvTaxRates.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvTaxRates.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvTaxRates.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvTaxRates.ForeColor = System.Drawing.Color.Black
        Me.gvTaxRates.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvTaxRates.Location = New System.Drawing.Point(10, 54)
        '
        'gvTaxRates
        '
        Me.gvTaxRates.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvTaxRates.MasterTemplate.AutoGenerateColumns = False
        GridViewTextBoxColumn1.EnableExpressionEditor = False
        GridViewTextBoxColumn1.FieldName = "Tax_Rate_Desc"
        GridViewTextBoxColumn1.HeaderText = "Rate Description"
        GridViewTextBoxColumn1.MaxLength = 50
        GridViewTextBoxColumn1.Name = "description"
        GridViewTextBoxColumn1.Width = 150
        GridViewDecimalColumn1.DecimalPlaces = 6
        GridViewDecimalColumn1.EnableExpressionEditor = False
        GridViewDecimalColumn1.FieldName = "Tax_Rate"
        GridViewDecimalColumn1.HeaderText = "Tax Rate(%)"
        GridViewDecimalColumn1.Name = "taxRate"
        GridViewDecimalColumn1.Width = 100
        GridViewTextBoxColumn2.EnableExpressionEditor = False
        GridViewTextBoxColumn2.FieldName = "Tax_Rate_Code"
        GridViewTextBoxColumn2.HeaderText = "TaxRateCode"
        GridViewTextBoxColumn2.IsVisible = False
        GridViewTextBoxColumn2.Name = "taxratecode"
        GridViewTextBoxColumn2.Width = 5
        GridViewTextBoxColumn3.EnableExpressionEditor = False
        GridViewTextBoxColumn3.HeaderText = "Account Code"
        GridViewTextBoxColumn3.Name = "colGLCode"
        GridViewTextBoxColumn3.Width = 100
        GridViewTextBoxColumn4.EnableExpressionEditor = False
        GridViewTextBoxColumn4.FieldName = "colAccountName"
        GridViewTextBoxColumn4.HeaderText = "Account Description"
        GridViewTextBoxColumn4.Name = "colGLAccountName"
        GridViewTextBoxColumn4.ReadOnly = True
        GridViewTextBoxColumn4.Width = 150
        Me.gvTaxRates.MasterTemplate.Columns.AddRange(New Telerik.WinControls.UI.GridViewDataColumn() {GridViewTextBoxColumn1, GridViewDecimalColumn1, GridViewTextBoxColumn2, GridViewTextBoxColumn3, GridViewTextBoxColumn4})
        Me.gvTaxRates.MasterTemplate.EnableGrouping = False
        Me.gvTaxRates.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTaxRates.Name = "gvTaxRates"
        Me.gvTaxRates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvTaxRates.ShowHeaderCellButtons = True
        Me.gvTaxRates.Size = New System.Drawing.Size(566, 329)
        Me.gvTaxRates.TabIndex = 3
        Me.gvTaxRates.TabStop = False
        Me.gvTaxRates.Text = "RadGridView1"
        '
        'ddlTransType
        '
        Me.ddlTransType.CalculationExpression = Nothing
        Me.ddlTransType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlTransType.FieldCode = Nothing
        Me.ddlTransType.FieldDesc = Nothing
        Me.ddlTransType.FieldMaxLength = 0
        Me.ddlTransType.FieldName = Nothing
        Me.ddlTransType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlTransType.isCalculatedField = False
        Me.ddlTransType.IsSourceFromTable = False
        Me.ddlTransType.IsSourceFromValueList = False
        Me.ddlTransType.IsUnique = False
        Me.ddlTransType.Location = New System.Drawing.Point(111, 29)
        Me.ddlTransType.MendatroryField = False
        Me.ddlTransType.MyLinkLable1 = Me.lblTransType
        Me.ddlTransType.MyLinkLable2 = Nothing
        Me.ddlTransType.Name = "ddlTransType"
        Me.ddlTransType.ReferenceFieldDesc = Nothing
        Me.ddlTransType.ReferenceFieldName = Nothing
        Me.ddlTransType.ReferenceTableName = Nothing
        Me.ddlTransType.Size = New System.Drawing.Size(235, 18)
        Me.ddlTransType.TabIndex = 2
        '
        'lblTransType
        '
        Me.lblTransType.FieldName = Nothing
        Me.lblTransType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransType.Location = New System.Drawing.Point(10, 30)
        Me.lblTransType.Name = "lblTransType"
        Me.lblTransType.Size = New System.Drawing.Size(94, 16)
        Me.lblTransType.TabIndex = 3
        Me.lblTransType.Text = "Transaction Type"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(515, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 21)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(74, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 21)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(3, 7)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(68, 21)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Save"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(586, 433)
        Me.SplitContainer1.SplitterDistance = 396
        Me.SplitContainer1.TabIndex = 0
        '
        'FrmTaxRates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 453)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmTaxRates"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tax Rates"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkGSTActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsGLReadonly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTaxAuthority, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTaxRates.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTaxRates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuExit As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnAdd As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvTaxRates As common.UserControls.MyRadGridView
    Friend WithEvents ddlTransType As common.Controls.MyComboBox
    Friend WithEvents lblTransType As common.Controls.MyLabel
    Friend WithEvents lblTaxAuthority As common.Controls.MyLabel
    Friend WithEvents findTaxAuthority As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkIsGLReadonly As common.Controls.MyCheckBox
    Friend WithEvents chkGSTActive As Telerik.WinControls.UI.RadCheckBox
End Class

