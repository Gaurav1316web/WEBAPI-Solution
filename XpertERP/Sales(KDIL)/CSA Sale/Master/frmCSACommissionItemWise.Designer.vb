<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCSACommissionItemWise
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCSACommissionItemWise))
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnCombinedExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImportHead = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImportDetail = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnImportCombined = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCSAName = New common.Controls.MyLabel()
        Me.txtCSACode = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.dtpdate = New common.Controls.MyDateTimePicker()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.txtVendorName = New common.Controls.MyLabel()
        Me.txtVendorCode = New common.UserControls.txtFinder()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCSAName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(739, 20)
        Me.RadMenu1.TabIndex = 1408
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem3, Me.btnExport, Me.btnImport})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Save Layout"
        Me.RadMenuItem2.AccessibleName = "Save Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem3.AccessibleName = "Delete Layout"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Delete Layout"
        '
        'btnExport
        '
        Me.btnExport.AccessibleDescription = "RadMenuItem4"
        Me.btnExport.AccessibleName = "RadMenuItem4"
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExportHead, Me.btnExportDetail, Me.btnCombinedExport})
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export"
        '
        'btnExportHead
        '
        Me.btnExportHead.AccessibleDescription = "Export Head"
        Me.btnExportHead.AccessibleName = "Export Head"
        Me.btnExportHead.Name = "btnExportHead"
        Me.btnExportHead.Text = "Export Head"
        '
        'btnExportDetail
        '
        Me.btnExportDetail.AccessibleDescription = "Export Detail"
        Me.btnExportDetail.AccessibleName = "Export Detail"
        Me.btnExportDetail.Name = "btnExportDetail"
        Me.btnExportDetail.Text = "Export Detail"
        '
        'btnCombinedExport
        '
        Me.btnCombinedExport.AccessibleDescription = "Combined Export"
        Me.btnCombinedExport.AccessibleName = "Combined Export"
        Me.btnCombinedExport.Name = "btnCombinedExport"
        Me.btnCombinedExport.Text = "Combined Export"
        '
        'btnImport
        '
        Me.btnImport.AccessibleDescription = "Import"
        Me.btnImport.AccessibleName = "Import"
        Me.btnImport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnImportHead, Me.btnImportDetail, Me.btnImportCombined})
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Text = "Import"
        '
        'btnImportHead
        '
        Me.btnImportHead.AccessibleDescription = "Import Head"
        Me.btnImportHead.AccessibleName = "Import Head"
        Me.btnImportHead.Name = "btnImportHead"
        Me.btnImportHead.Text = "Import Head"
        '
        'btnImportDetail
        '
        Me.btnImportDetail.AccessibleDescription = "Import Detail"
        Me.btnImportDetail.AccessibleName = "Import Detail"
        Me.btnImportDetail.Name = "btnImportDetail"
        Me.btnImportDetail.Text = "Import Detail"
        '
        'btnImportCombined
        '
        Me.btnImportCombined.AccessibleDescription = "Combined Import"
        Me.btnImportCombined.AccessibleName = "Combined Import"
        Me.btnImportCombined.Name = "btnImportCombined"
        Me.btnImportCombined.Text = "Combined Import"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(739, 496)
        Me.SplitContainer1.SplitterDistance = 464
        Me.SplitContainer1.TabIndex = 1409
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(2, 2)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(735, 460)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(165.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(714, 412)
        Me.RadPageViewPage1.Text = "Commission/Freight Mapping"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCSAName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCSACode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVendorName)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVendorCode)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox4)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Size = New System.Drawing.Size(714, 412)
        Me.SplitContainer2.SplitterDistance = 91
        Me.SplitContainer2.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 44)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel1.TabIndex = 1423
        Me.MyLabel1.Text = "CSA Name"
        '
        'txtCSAName
        '
        Me.txtCSAName.AutoSize = False
        Me.txtCSAName.BorderVisible = True
        Me.txtCSAName.FieldName = Nothing
        Me.txtCSAName.Location = New System.Drawing.Point(272, 42)
        Me.txtCSAName.Name = "txtCSAName"
        Me.txtCSAName.Size = New System.Drawing.Size(316, 19)
        Me.txtCSAName.TabIndex = 1424
        Me.txtCSAName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCSACode
        '
        Me.txtCSACode.CalculationExpression = Nothing
        Me.txtCSACode.FieldCode = Nothing
        Me.txtCSACode.FieldDesc = Nothing
        Me.txtCSACode.FieldMaxLength = 0
        Me.txtCSACode.FieldName = Nothing
        Me.txtCSACode.isCalculatedField = False
        Me.txtCSACode.IsSourceFromTable = False
        Me.txtCSACode.IsSourceFromValueList = False
        Me.txtCSACode.IsUnique = False
        Me.txtCSACode.Location = New System.Drawing.Point(129, 42)
        Me.txtCSACode.MendatroryField = True
        Me.txtCSACode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSACode.MyLinkLable1 = Me.MyLabel1
        Me.txtCSACode.MyLinkLable2 = Me.txtCSAName
        Me.txtCSACode.MyReadOnly = False
        Me.txtCSACode.MyShowMasterFormButton = False
        Me.txtCSACode.Name = "txtCSACode"
        Me.txtCSACode.ReferenceFieldDesc = Nothing
        Me.txtCSACode.ReferenceFieldName = Nothing
        Me.txtCSACode.ReferenceTableName = Nothing
        Me.txtCSACode.Size = New System.Drawing.Size(143, 19)
        Me.txtCSACode.TabIndex = 2
        Me.txtCSACode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(128, 16)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(309, 21)
        Me.txtCode.TabIndex = 1420
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(13, 20)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(88, 16)
        Me.lblCode.TabIndex = 1421
        Me.lblCode.Text = "Document Code"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(466, 18)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel7.TabIndex = 1418
        Me.MyLabel7.Text = "Date"
        '
        'dtpdate
        '
        Me.dtpdate.CalculationExpression = Nothing
        Me.dtpdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpdate.FieldCode = Nothing
        Me.dtpdate.FieldDesc = Nothing
        Me.dtpdate.FieldMaxLength = 0
        Me.dtpdate.FieldName = Nothing
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdate.isCalculatedField = False
        Me.dtpdate.IsSourceFromTable = False
        Me.dtpdate.IsSourceFromValueList = False
        Me.dtpdate.IsUnique = False
        Me.dtpdate.Location = New System.Drawing.Point(503, 16)
        Me.dtpdate.MendatroryField = True
        Me.dtpdate.MyLinkLable1 = Me.MyLabel7
        Me.dtpdate.MyLinkLable2 = Nothing
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.NullDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.dtpdate.ReferenceFieldDesc = Nothing
        Me.dtpdate.ReferenceFieldName = Nothing
        Me.dtpdate.ReferenceTableName = Nothing
        Me.dtpdate.Size = New System.Drawing.Size(84, 20)
        Me.dtpdate.TabIndex = 1
        Me.dtpdate.TabStop = False
        Me.dtpdate.Text = "11/09/2014"
        Me.dtpdate.Value = New Date(2014, 9, 11, 16, 2, 0, 928)
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(440, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 21)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = " "
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel5.Location = New System.Drawing.Point(13, 68)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(76, 16)
        Me.RadLabel5.TabIndex = 1413
        Me.RadLabel5.Text = "Vendor Name"
        '
        'txtVendorName
        '
        Me.txtVendorName.AutoSize = False
        Me.txtVendorName.BorderVisible = True
        Me.txtVendorName.FieldName = Nothing
        Me.txtVendorName.Location = New System.Drawing.Point(272, 66)
        Me.txtVendorName.Name = "txtVendorName"
        Me.txtVendorName.Size = New System.Drawing.Size(316, 19)
        Me.txtVendorName.TabIndex = 1414
        Me.txtVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVendorCode
        '
        Me.txtVendorCode.CalculationExpression = Nothing
        Me.txtVendorCode.Enabled = False
        Me.txtVendorCode.FieldCode = Nothing
        Me.txtVendorCode.FieldDesc = Nothing
        Me.txtVendorCode.FieldMaxLength = 0
        Me.txtVendorCode.FieldName = Nothing
        Me.txtVendorCode.isCalculatedField = False
        Me.txtVendorCode.IsSourceFromTable = False
        Me.txtVendorCode.IsSourceFromValueList = False
        Me.txtVendorCode.IsUnique = False
        Me.txtVendorCode.Location = New System.Drawing.Point(129, 66)
        Me.txtVendorCode.MendatroryField = True
        Me.txtVendorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.MyLinkLable1 = Me.RadLabel5
        Me.txtVendorCode.MyLinkLable2 = Me.txtVendorName
        Me.txtVendorCode.MyReadOnly = False
        Me.txtVendorCode.MyShowMasterFormButton = False
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.ReferenceFieldDesc = Nothing
        Me.txtVendorCode.ReferenceFieldName = Nothing
        Me.txtVendorCode.ReferenceTableName = Nothing
        Me.txtVendorCode.Size = New System.Drawing.Size(143, 19)
        Me.txtVendorCode.TabIndex = 3
        Me.txtVendorCode.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.gv)
        Me.RadGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox4.HeaderText = "Detail"
        Me.RadGroupBox4.Location = New System.Drawing.Point(2, 2)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(710, 313)
        Me.RadGroupBox4.TabIndex = 0
        Me.RadGroupBox4.Text = "Detail"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(2, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(706, 293)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(659, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(73, 20)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(83, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(4, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'FrmCSACommissionItemWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCSACommissionItemWise"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "CSA Commission/Freight Mapping"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCSAName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents txtVendorName As common.Controls.MyLabel
    Friend WithEvents txtVendorCode As common.UserControls.txtFinder
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpdate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCSAName As common.Controls.MyLabel
    Friend WithEvents txtCSACode As common.UserControls.txtFinder
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImportHead As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImportDetail As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnCombinedExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnImportCombined As Telerik.WinControls.UI.RadMenuItem
End Class

