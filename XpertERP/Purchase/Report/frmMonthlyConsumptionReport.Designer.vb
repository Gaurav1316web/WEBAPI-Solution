<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonthlyConsumptionReport
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkBoth = New System.Windows.Forms.RadioButton()
        Me.chkValueWise = New System.Windows.Forms.RadioButton()
        Me.ChkQtyWise = New System.Windows.Forms.RadioButton()
        Me.fnd_Months = New common.UserControls.txtMultiSelectFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtHighValue = New Telerik.WinControls.UI.RadTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.fnd_Dept = New common.UserControls.txtMultiSelectFinder()
        Me.lblItem = New common.Controls.MyLabel()
        Me.fnd_ItemCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.fnd_Category = New common.UserControls.txtMultiSelectFinder()
        Me.lblPoNo = New common.Controls.MyLabel()
        Me.fnd_DocNo = New common.UserControls.txtMultiSelectFinder()
        Me.cboFiscalYear = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnExportSplit = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MyRadGridView1 = New common.UserControls.MyRadGridView()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.menuDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.object_011f7fba_380c_42d0_8ed6_95e278817401 = New Telerik.WinControls.UI.RadLabelRootElement()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHighValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPoNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadMenu1.SuspendLayout()
        CType(Me.MyRadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportSplit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(690, 407)
        Me.SplitContainer1.SplitterDistance = 371
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(690, 371)
        Me.RadPageView1.TabIndex = 159
        Me.RadPageView1.Text = "Report"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.fnd_Months)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtHighValue)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendor)
        Me.RadPageViewPage1.Controls.Add(Me.fnd_Dept)
        Me.RadPageViewPage1.Controls.Add(Me.lblItem)
        Me.RadPageViewPage1.Controls.Add(Me.fnd_ItemCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.fnd_Category)
        Me.RadPageViewPage1.Controls.Add(Me.lblPoNo)
        Me.RadPageViewPage1.Controls.Add(Me.fnd_DocNo)
        Me.RadPageViewPage1.Controls.Add(Me.cboFiscalYear)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(669, 323)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.chkBoth)
        Me.RadGroupBox3.Controls.Add(Me.chkValueWise)
        Me.RadGroupBox3.Controls.Add(Me.ChkQtyWise)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(228, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(217, 30)
        Me.RadGroupBox3.TabIndex = 372
        '
        'chkBoth
        '
        Me.chkBoth.AutoSize = True
        Me.chkBoth.Location = New System.Drawing.Point(160, 5)
        Me.chkBoth.Name = "chkBoth"
        Me.chkBoth.Size = New System.Drawing.Size(50, 17)
        Me.chkBoth.TabIndex = 4
        Me.chkBoth.TabStop = True
        Me.chkBoth.Text = "Both"
        Me.chkBoth.UseVisualStyleBackColor = True
        '
        'chkValueWise
        '
        Me.chkValueWise.AutoSize = True
        Me.chkValueWise.Location = New System.Drawing.Point(78, 5)
        Me.chkValueWise.Name = "chkValueWise"
        Me.chkValueWise.Size = New System.Drawing.Size(81, 17)
        Me.chkValueWise.TabIndex = 3
        Me.chkValueWise.TabStop = True
        Me.chkValueWise.Text = "Value Wise"
        Me.chkValueWise.UseVisualStyleBackColor = True
        '
        'ChkQtyWise
        '
        Me.ChkQtyWise.AutoSize = True
        Me.ChkQtyWise.Location = New System.Drawing.Point(7, 5)
        Me.ChkQtyWise.Name = "ChkQtyWise"
        Me.ChkQtyWise.Size = New System.Drawing.Size(70, 17)
        Me.ChkQtyWise.TabIndex = 0
        Me.ChkQtyWise.TabStop = True
        Me.ChkQtyWise.Text = "Qty Wise"
        Me.ChkQtyWise.UseVisualStyleBackColor = True
        '
        'fnd_Months
        '
        Me.fnd_Months.arrDispalyMember = Nothing
        Me.fnd_Months.arrValueMember = Nothing
        Me.fnd_Months.Location = New System.Drawing.Point(88, 159)
        Me.fnd_Months.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_Months.MyLinkLable1 = Me.lblVendor
        Me.fnd_Months.MyLinkLable2 = Nothing
        Me.fnd_Months.MyNullText = "All"
        Me.fnd_Months.Name = "fnd_Months"
        Me.fnd_Months.Size = New System.Drawing.Size(344, 19)
        Me.fnd_Months.TabIndex = 371
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(3, 111)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(66, 18)
        Me.lblVendor.TabIndex = 360
        Me.lblVendor.Text = "Department"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 135)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(61, 18)
        Me.MyLabel2.TabIndex = 370
        Me.MyLabel2.Text = "High Value"
        '
        'txtHighValue
        '
        Me.txtHighValue.Location = New System.Drawing.Point(88, 134)
        Me.txtHighValue.MaxLength = 20
        Me.txtHighValue.Name = "txtHighValue"
        Me.txtHighValue.NullText = "0.00"
        Me.txtHighValue.Size = New System.Drawing.Size(134, 20)
        Me.txtHighValue.TabIndex = 369
        Me.txtHighValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(2, 159)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(44, 18)
        Me.MyLabel3.TabIndex = 364
        Me.MyLabel3.Text = "Months"
        '
        'fnd_Dept
        '
        Me.fnd_Dept.arrDispalyMember = Nothing
        Me.fnd_Dept.arrValueMember = Nothing
        Me.fnd_Dept.Location = New System.Drawing.Point(88, 110)
        Me.fnd_Dept.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_Dept.MyLinkLable1 = Me.lblVendor
        Me.fnd_Dept.MyLinkLable2 = Nothing
        Me.fnd_Dept.MyNullText = "All"
        Me.fnd_Dept.Name = "fnd_Dept"
        Me.fnd_Dept.Size = New System.Drawing.Size(344, 19)
        Me.fnd_Dept.TabIndex = 359
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(3, 87)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 358
        Me.lblItem.Text = "Item"
        '
        'fnd_ItemCode
        '
        Me.fnd_ItemCode.arrDispalyMember = Nothing
        Me.fnd_ItemCode.arrValueMember = Nothing
        Me.fnd_ItemCode.Location = New System.Drawing.Point(88, 86)
        Me.fnd_ItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_ItemCode.MyLinkLable1 = Me.lblItem
        Me.fnd_ItemCode.MyLinkLable2 = Nothing
        Me.fnd_ItemCode.MyNullText = "All"
        Me.fnd_ItemCode.Name = "fnd_ItemCode"
        Me.fnd_ItemCode.Size = New System.Drawing.Size(344, 19)
        Me.fnd_ItemCode.TabIndex = 357
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(3, 63)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(51, 18)
        Me.lblLocation.TabIndex = 356
        Me.lblLocation.Text = "Category"
        '
        'fnd_Category
        '
        Me.fnd_Category.arrDispalyMember = Nothing
        Me.fnd_Category.arrValueMember = Nothing
        Me.fnd_Category.Location = New System.Drawing.Point(88, 62)
        Me.fnd_Category.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_Category.MyLinkLable1 = Me.lblLocation
        Me.fnd_Category.MyLinkLable2 = Nothing
        Me.fnd_Category.MyNullText = "All"
        Me.fnd_Category.Name = "fnd_Category"
        Me.fnd_Category.Size = New System.Drawing.Size(344, 19)
        Me.fnd_Category.TabIndex = 355
        '
        'lblPoNo
        '
        Me.lblPoNo.FieldName = Nothing
        Me.lblPoNo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPoNo.Location = New System.Drawing.Point(3, 39)
        Me.lblPoNo.Name = "lblPoNo"
        Me.lblPoNo.Size = New System.Drawing.Size(58, 18)
        Me.lblPoNo.TabIndex = 354
        Me.lblPoNo.Text = "Document"
        '
        'fnd_DocNo
        '
        Me.fnd_DocNo.arrDispalyMember = Nothing
        Me.fnd_DocNo.arrValueMember = Nothing
        Me.fnd_DocNo.Location = New System.Drawing.Point(88, 38)
        Me.fnd_DocNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fnd_DocNo.MyLinkLable1 = Me.lblPoNo
        Me.fnd_DocNo.MyLinkLable2 = Nothing
        Me.fnd_DocNo.MyNullText = "All"
        Me.fnd_DocNo.Name = "fnd_DocNo"
        Me.fnd_DocNo.Size = New System.Drawing.Size(344, 19)
        Me.fnd_DocNo.TabIndex = 353
        '
        'cboFiscalYear
        '
        Me.cboFiscalYear.AutoCompleteDisplayMember = Nothing
        Me.cboFiscalYear.AutoCompleteValueMember = Nothing
        Me.cboFiscalYear.CalculationExpression = Nothing
        Me.cboFiscalYear.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFiscalYear.FieldCode = Nothing
        Me.cboFiscalYear.FieldDesc = Nothing
        Me.cboFiscalYear.FieldMaxLength = 0
        Me.cboFiscalYear.FieldName = Nothing
        Me.cboFiscalYear.isCalculatedField = False
        Me.cboFiscalYear.IsSourceFromTable = False
        Me.cboFiscalYear.IsSourceFromValueList = False
        Me.cboFiscalYear.IsUnique = False
        Me.cboFiscalYear.Location = New System.Drawing.Point(88, 13)
        Me.cboFiscalYear.MendatroryField = True
        Me.cboFiscalYear.MyLinkLable1 = Nothing
        Me.cboFiscalYear.MyLinkLable2 = Nothing
        Me.cboFiscalYear.Name = "cboFiscalYear"
        Me.cboFiscalYear.ReferenceFieldDesc = Nothing
        Me.cboFiscalYear.ReferenceFieldName = Nothing
        Me.cboFiscalYear.ReferenceTableName = Nothing
        Me.cboFiscalYear.Size = New System.Drawing.Size(134, 20)
        Me.cboFiscalYear.TabIndex = 352
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 15)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel1.TabIndex = 351
        Me.MyLabel1.Text = "Fiscal Year"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(669, 323)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(669, 323)
        Me.gv.TabIndex = 5
        Me.gv.Text = "gv"
        '
        'btnExportSplit
        '
        Me.btnExportSplit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportSplit.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExport, Me.btnPDF})
        Me.btnExportSplit.Location = New System.Drawing.Point(144, 6)
        Me.btnExportSplit.Name = "btnExportSplit"
        Me.btnExportSplit.Size = New System.Drawing.Size(75, 20)
        Me.btnExportSplit.TabIndex = 158
        Me.btnExportSplit.Text = "Export"
        '
        'btnExport
        '
        Me.btnExport.AccessibleDescription = "Excel"
        Me.btnExport.AccessibleName = "Excel"
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(609, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 20)
        Me.btnClose.TabIndex = 157
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(5, 6)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(64, 20)
        Me.btnGo.TabIndex = 154
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(69, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(75, 20)
        Me.btnReset.TabIndex = 153
        Me.btnReset.Text = "Reset"
        '
        'gv1
        '
        Me.gv1.AllowAddNewRow = False
        Me.gv1.AllowDeleteRow = False
        Me.gv1.AllowEditRow = False
        Me.gv1.EnableFiltering = True
        Me.gv1.ShowHeaderCellButtons = True
        '
        'RadMenu1
        '
        Me.RadMenu1.Controls.Add(Me.MyRadGridView1)
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(690, 20)
        Me.RadMenu1.TabIndex = 37
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MyRadGridView1
        '
        Me.MyRadGridView1.Location = New System.Drawing.Point(828, 0)
        '
        'MyRadGridView1
        '
        Me.MyRadGridView1.MasterTemplate.ShowHeaderCellButtons = True
        Me.MyRadGridView1.Name = "MyRadGridView1"
        Me.MyRadGridView1.ShowHeaderCellButtons = True
        Me.MyRadGridView1.Size = New System.Drawing.Size(10, 10)
        Me.MyRadGridView1.TabIndex = 2
        Me.MyRadGridView1.Text = "RadGridView1"
        Me.MyRadGridView1.Visible = False
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.menuSaveLayout, Me.menuDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'menuSaveLayout
        '
        Me.menuSaveLayout.AccessibleDescription = "Save Layout"
        Me.menuSaveLayout.AccessibleName = "Save Layout"
        Me.menuSaveLayout.Name = "menuSaveLayout"
        Me.menuSaveLayout.Text = "Save Layout"
        '
        'menuDeleteLayout
        '
        Me.menuDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.menuDeleteLayout.AccessibleName = "Delete Layout"
        Me.menuDeleteLayout.Name = "menuDeleteLayout"
        Me.menuDeleteLayout.Text = "Delete Layout"
        '
        'object_011f7fba_380c_42d0_8ed6_95e278817401
        '
        Me.object_011f7fba_380c_42d0_8ed6_95e278817401.Name = "object_011f7fba_380c_42d0_8ed6_95e278817401"
        '
        'frmMonthlyConsumptionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 427)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmMonthlyConsumptionReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Monthly Consumption Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHighValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPoNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportSplit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadMenu1.ResumeLayout(False)
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportSplit As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv1 As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MyRadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents menuDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents cboFiscalYear As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents fnd_Dept As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents fnd_ItemCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents fnd_Category As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblPoNo As common.Controls.MyLabel
    Friend WithEvents fnd_DocNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtHighValue As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents object_011f7fba_380c_42d0_8ed6_95e278817401 As Telerik.WinControls.UI.RadLabelRootElement
    Friend WithEvents fnd_Months As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkBoth As System.Windows.Forms.RadioButton
    Friend WithEvents chkValueWise As System.Windows.Forms.RadioButton
    Friend WithEvents ChkQtyWise As System.Windows.Forms.RadioButton
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
End Class
