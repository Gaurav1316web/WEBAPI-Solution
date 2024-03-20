<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStandardRateItem
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.radioIsVendor = New Telerik.WinControls.UI.RadRadioButton()
        Me.radioIsCust = New Telerik.WinControls.UI.RadRadioButton()
        Me.fndCustomer = New common.UserControls.txtFinder()
        Me.fndCode = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.dtpEnd = New common.Controls.MyDateTimePicker()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.dtpStart = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.txtDesc = New common.Controls.MyTextBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.Filemenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.importmenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimprtHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnimportDetails = New Telerik.WinControls.UI.RadMenuItem()
        Me.exportmenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportStdhead = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnExportstdDetails = New Telerik.WinControls.UI.RadMenuItem()
        Me.Exitmenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.radioIsVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.radioIsCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.fndCustomer)
        Me.RadGroupBox1.Controls.Add(Me.fndCode)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.dtpEnd)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox1.Controls.Add(Me.dtpStart)
        Me.RadGroupBox1.Controls.Add(Me.txtDesc)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(810, 500)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.radioIsVendor)
        Me.RadGroupBox2.Controls.Add(Me.radioIsCust)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 6)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(221, 25)
        Me.RadGroupBox2.TabIndex = 0
        '
        'radioIsVendor
        '
        Me.radioIsVendor.Location = New System.Drawing.Point(112, 3)
        Me.radioIsVendor.Name = "radioIsVendor"
        Me.radioIsVendor.Size = New System.Drawing.Size(57, 18)
        Me.radioIsVendor.TabIndex = 1
        Me.radioIsVendor.Text = "Vendor"
        '
        'radioIsCust
        '
        Me.radioIsCust.CheckState = System.Windows.Forms.CheckState.Checked
        Me.radioIsCust.Location = New System.Drawing.Point(5, 3)
        Me.radioIsCust.Name = "radioIsCust"
        Me.radioIsCust.Size = New System.Drawing.Size(69, 18)
        Me.radioIsCust.TabIndex = 0
        Me.radioIsCust.Text = "Customer"
        Me.radioIsCust.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'fndCustomer
        '
        Me.fndCustomer.CalculationExpression = Nothing
        Me.fndCustomer.FieldCode = Nothing
        Me.fndCustomer.FieldDesc = Nothing
        Me.fndCustomer.FieldMaxLength = 0
        Me.fndCustomer.FieldName = Nothing
        Me.fndCustomer.isCalculatedField = False
        Me.fndCustomer.IsSourceFromTable = False
        Me.fndCustomer.IsSourceFromValueList = False
        Me.fndCustomer.IsUnique = False
        Me.fndCustomer.Location = New System.Drawing.Point(117, 62)
        Me.fndCustomer.MendatroryField = False
        Me.fndCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomer.MyLinkLable1 = Nothing
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.MyShowMasterFormButton = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.ReferenceFieldDesc = Nothing
        Me.fndCustomer.ReferenceFieldName = Nothing
        Me.fndCustomer.ReferenceTableName = Nothing
        Me.fndCustomer.Size = New System.Drawing.Size(238, 18)
        Me.fndCustomer.TabIndex = 4
        Me.fndCustomer.Value = ""
        '
        'fndCode
        '
        Me.fndCode.FieldName = Nothing
        Me.fndCode.Location = New System.Drawing.Point(117, 36)
        Me.fndCode.MendatroryField = True
        Me.fndCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndCode.MyLinkLable1 = Me.RadLabel1
        Me.fndCode.MyLinkLable2 = Nothing
        Me.fndCode.MyMaxLength = 30
        Me.fndCode.MyReadOnly = False
        Me.fndCode.Name = "fndCode"
        Me.fndCode.Size = New System.Drawing.Size(218, 18)
        Me.fndCode.TabIndex = 1
        Me.fndCode.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.RadLabel1.Location = New System.Drawing.Point(8, 37)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(33, 16)
        Me.RadLabel1.TabIndex = 10
        Me.RadLabel1.Text = "Code"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(338, 36)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 17)
        Me.btnReset.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gv1)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 82)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(797, 411)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(12, 21)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(768, 384)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'dtpEnd
        '
        Me.dtpEnd.CalculationExpression = Nothing
        Me.dtpEnd.CustomFormat = "dd/MM/yyyy"
        Me.dtpEnd.FieldCode = Nothing
        Me.dtpEnd.FieldDesc = Nothing
        Me.dtpEnd.FieldMaxLength = 0
        Me.dtpEnd.FieldName = Nothing
        Me.dtpEnd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEnd.isCalculatedField = False
        Me.dtpEnd.IsSourceFromTable = False
        Me.dtpEnd.IsSourceFromValueList = False
        Me.dtpEnd.IsUnique = False
        Me.dtpEnd.Location = New System.Drawing.Point(699, 58)
        Me.dtpEnd.MendatroryField = False
        Me.dtpEnd.MinDate = New Date(2011, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.MyLinkLable1 = Me.RadLabel5
        Me.dtpEnd.MyLinkLable2 = Nothing
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.NullDate = New Date(2011, 1, 1, 0, 0, 0, 0)
        Me.dtpEnd.ReferenceFieldDesc = Nothing
        Me.dtpEnd.ReferenceFieldName = Nothing
        Me.dtpEnd.ReferenceTableName = Nothing
        Me.dtpEnd.ShowCheckBox = True
        Me.dtpEnd.Size = New System.Drawing.Size(103, 18)
        Me.dtpEnd.TabIndex = 6
        Me.dtpEnd.TabStop = False
        Me.dtpEnd.Text = "17/05/2011"
        Me.dtpEnd.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(624, 59)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel5.TabIndex = 7
        Me.RadLabel5.Text = "Valid Till Date"
        '
        'dtpStart
        '
        Me.dtpStart.CalculationExpression = Nothing
        Me.dtpStart.CustomFormat = "dd/MM/yyyy"
        Me.dtpStart.FieldCode = Nothing
        Me.dtpStart.FieldDesc = Nothing
        Me.dtpStart.FieldMaxLength = 0
        Me.dtpStart.FieldName = Nothing
        Me.dtpStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStart.isCalculatedField = False
        Me.dtpStart.IsSourceFromTable = False
        Me.dtpStart.IsSourceFromValueList = False
        Me.dtpStart.IsUnique = False
        Me.dtpStart.Location = New System.Drawing.Point(456, 59)
        Me.dtpStart.MendatroryField = False
        Me.dtpStart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.MyLinkLable1 = Me.RadLabel3
        Me.dtpStart.MyLinkLable2 = Nothing
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStart.ReferenceFieldDesc = Nothing
        Me.dtpStart.ReferenceFieldName = Nothing
        Me.dtpStart.ReferenceTableName = Nothing
        Me.dtpStart.Size = New System.Drawing.Size(130, 18)
        Me.dtpStart.TabIndex = 5
        Me.dtpStart.TabStop = False
        Me.dtpStart.Text = "17/05/2011"
        Me.dtpStart.Value = New Date(2011, 5, 17, 21, 26, 29, 812)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(370, 60)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel3.TabIndex = 8
        Me.RadLabel3.Text = "Date"
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
        Me.txtDesc.Location = New System.Drawing.Point(453, 36)
        Me.txtDesc.MendatroryField = False
        Me.txtDesc.MyLinkLable1 = Me.RadLabel2
        Me.txtDesc.MyLinkLable2 = Nothing
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReferenceFieldDesc = Nothing
        Me.txtDesc.ReferenceFieldName = Nothing
        Me.txtDesc.ReferenceTableName = Nothing
        Me.txtDesc.Size = New System.Drawing.Size(346, 18)
        Me.txtDesc.TabIndex = 3
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(370, 37)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(63, 16)
        Me.RadLabel2.TabIndex = 11
        Me.RadLabel2.Text = "Description"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(8, 60)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(55, 16)
        Me.RadLabel4.TabIndex = 9
        Me.RadLabel4.Text = "Customer"
        '
        'btnclose
        '
        Me.btnclose.AccessibleDescription = ""
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(751, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(70, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(3, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.Filemenu})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(820, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'Filemenu
        '
        Me.Filemenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.importmenu, Me.exportmenu, Me.Exitmenu})
        Me.Filemenu.Name = "Filemenu"
        Me.Filemenu.Text = "File"
        '
        'importmenu
        '
        Me.importmenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnimprtHead, Me.BtnimportDetails})
        Me.importmenu.Name = "importmenu"
        Me.importmenu.Text = "Import"
        '
        'btnimprtHead
        '
        Me.btnimprtHead.AccessibleDescription = "Import"
        Me.btnimprtHead.AccessibleName = "Import"
        Me.btnimprtHead.Name = "btnimprtHead"
        Me.btnimprtHead.Text = "Standard ROI Head"
        '
        'BtnimportDetails
        '
        Me.BtnimportDetails.Name = "BtnimportDetails"
        Me.BtnimportDetails.Text = "Standard ROI Details"
        '
        'exportmenu
        '
        Me.exportmenu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExportStdhead, Me.BtnExportstdDetails})
        Me.exportmenu.Name = "exportmenu"
        Me.exportmenu.Text = "Export"
        '
        'btnExportStdhead
        '
        Me.btnExportStdhead.Name = "btnExportStdhead"
        Me.btnExportStdhead.Text = "Std ROI Head"
        '
        'BtnExportstdDetails
        '
        Me.BtnExportstdDetails.Name = "BtnExportstdDetails"
        Me.BtnExportstdDetails.Text = "Std ROI Details"
        '
        'Exitmenu
        '
        Me.Exitmenu.Name = "Exitmenu"
        Me.Exitmenu.Text = "Exit"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(820, 536)
        Me.SplitContainer1.SplitterDistance = 505
        Me.SplitContainer1.TabIndex = 0
        '
        'frmStandardRateItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(820, 556)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmStandardRateItem"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Standard Rate of Item"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.radioIsVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.radioIsCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    'Friend WithEvents grdfndSch As finder.gridFinder
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpEnd As common.Controls.MyDateTimePicker
    Friend WithEvents dtpStart As common.Controls.MyDateTimePicker
    Friend WithEvents txtDesc As common.Controls.MyTextBox
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents Filemenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents importmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents exportmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Exitmenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents fndCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents fndCustomer As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents radioIsVendor As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents radioIsCust As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnimprtHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnimportDetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportStdhead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnExportstdDetails As Telerik.WinControls.UI.RadMenuItem
End Class

