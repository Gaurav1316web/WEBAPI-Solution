<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorPriceChartMappingDocNoWise
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.fndPriceCode = New common.UserControls.txtFinder()
        Me.lblPriceCode = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.fndcode = New common.UserControls.txtNavigator()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.txtDocDate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.GvVendor = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkVendorSelect = New common.Controls.MyRadioButton()
        Me.chkVendorAll = New common.Controls.MyRadioButton()
        Me.gbPriceCode = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvPriceCode = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPriceCode.SuspendLayout()
        CType(Me.gvPriceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvPriceCode.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(911, 580)
        Me.SplitContainer1.SplitterDistance = 543
        Me.SplitContainer1.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer2.Panel1.Padding = New System.Windows.Forms.Padding(5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(6)
        Me.SplitContainer2.Size = New System.Drawing.Size(905, 537)
        Me.SplitContainer2.SplitterDistance = 28
        Me.SplitContainer2.TabIndex = 19
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(5, 5)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(895, 20)
        Me.RadMenu1.TabIndex = 17
        Me.RadMenu1.Text = "RadMenu1"
        '
        'MenuClose
        '
        Me.MenuClose.AccessibleDescription = "File"
        Me.MenuClose.AccessibleName = "File"
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.AccessibleDescription = "Export"
        Me.btnexport.AccessibleName = "Export"
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.AccessibleDescription = "Import"
        Me.btnimport.AccessibleName = "Import"
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(6, 6)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.fndPriceCode)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPriceCode)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer3.Size = New System.Drawing.Size(893, 493)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 1
        '
        'fndPriceCode
        '
        Me.fndPriceCode.CalculationExpression = Nothing
        Me.fndPriceCode.FieldCode = Nothing
        Me.fndPriceCode.FieldDesc = Nothing
        Me.fndPriceCode.FieldMaxLength = 0
        Me.fndPriceCode.FieldName = Nothing
        Me.fndPriceCode.isCalculatedField = False
        Me.fndPriceCode.IsSourceFromTable = False
        Me.fndPriceCode.IsSourceFromValueList = False
        Me.fndPriceCode.IsUnique = False
        Me.fndPriceCode.Location = New System.Drawing.Point(73, 4)
        Me.fndPriceCode.MendatroryField = True
        Me.fndPriceCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndPriceCode.MyLinkLable1 = Me.lblPriceCode
        Me.fndPriceCode.MyLinkLable2 = Nothing
        Me.fndPriceCode.MyReadOnly = False
        Me.fndPriceCode.MyShowMasterFormButton = False
        Me.fndPriceCode.Name = "fndPriceCode"
        Me.fndPriceCode.ReferenceFieldDesc = Nothing
        Me.fndPriceCode.ReferenceFieldName = Nothing
        Me.fndPriceCode.ReferenceTableName = Nothing
        Me.fndPriceCode.Size = New System.Drawing.Size(294, 18)
        Me.fndPriceCode.TabIndex = 8
        Me.fndPriceCode.Value = ""
        '
        'lblPriceCode
        '
        Me.lblPriceCode.FieldName = Nothing
        Me.lblPriceCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPriceCode.Location = New System.Drawing.Point(5, 6)
        Me.lblPriceCode.Name = "lblPriceCode"
        Me.lblPriceCode.Size = New System.Drawing.Size(62, 16)
        Me.lblPriceCode.TabIndex = 7
        Me.lblPriceCode.Text = "Price Code"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(893, 464)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(872, 416)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Vendor Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(3, 18, 3, 3)
        Me.RadGroupBox1.Size = New System.Drawing.Size(872, 416)
        Me.RadGroupBox1.TabIndex = 18
        Me.RadGroupBox1.Text = "Vendor Details"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(3, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowDragToGroup = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(866, 395)
        Me.gv.TabIndex = 0
        Me.gv.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.btnnew)
        Me.RadPageViewPage2.Controls.Add(Me.fndcode)
        Me.RadPageViewPage2.Controls.Add(Me.lblvandorno)
        Me.RadPageViewPage2.Controls.Add(Me.lblSRNDate)
        Me.RadPageViewPage2.Controls.Add(Me.txtDocDate)
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage2.Controls.Add(Me.gbPriceCode)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(872, 416)
        Me.RadPageViewPage2.Text = "Details"
        '
        'fndcode
        '
        Me.fndcode.FieldName = Nothing
        Me.fndcode.Location = New System.Drawing.Point(96, 3)
        Me.fndcode.MendatroryField = True
        Me.fndcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcode.MyLinkLable1 = Me.lblvandorno
        Me.fndcode.MyLinkLable2 = Nothing
        Me.fndcode.MyMaxLength = 32767
        Me.fndcode.MyReadOnly = False
        Me.fndcode.Name = "fndcode"
        Me.fndcode.Size = New System.Drawing.Size(300, 21)
        Me.fndcode.TabIndex = 119
        Me.fndcode.Value = ""
        '
        'lblvandorno
        '
        Me.lblvandorno.FieldName = Nothing
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(7, 5)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(75, 16)
        Me.lblvandorno.TabIndex = 118
        Me.lblvandorno.Text = "Document No"
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSRNDate.Location = New System.Drawing.Point(441, 5)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(85, 16)
        Me.lblSRNDate.TabIndex = 116
        Me.lblSRNDate.Text = "Document Date"
        '
        'txtDocDate
        '
        Me.txtDocDate.CalculationExpression = Nothing
        Me.txtDocDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.txtDocDate.FieldCode = Nothing
        Me.txtDocDate.FieldDesc = Nothing
        Me.txtDocDate.FieldMaxLength = 0
        Me.txtDocDate.FieldName = Nothing
        Me.txtDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDocDate.isCalculatedField = False
        Me.txtDocDate.IsSourceFromTable = False
        Me.txtDocDate.IsSourceFromValueList = False
        Me.txtDocDate.IsUnique = False
        Me.txtDocDate.Location = New System.Drawing.Point(528, 2)
        Me.txtDocDate.MendatroryField = True
        Me.txtDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.MyLinkLable1 = Me.lblSRNDate
        Me.txtDocDate.MyLinkLable2 = Nothing
        Me.txtDocDate.Name = "txtDocDate"
        Me.txtDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDocDate.ReferenceFieldDesc = Nothing
        Me.txtDocDate.ReferenceFieldName = Nothing
        Me.txtDocDate.ReferenceTableName = Nothing
        Me.txtDocDate.Size = New System.Drawing.Size(151, 18)
        Me.txtDocDate.TabIndex = 117
        Me.txtDocDate.TabStop = False
        Me.txtDocDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.GvVendor)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Vendor"
        Me.RadGroupBox2.Location = New System.Drawing.Point(437, 30)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(427, 381)
        Me.RadGroupBox2.TabIndex = 114
        Me.RadGroupBox2.Text = "Vendor"
        '
        'GvVendor
        '
        Me.GvVendor.Location = New System.Drawing.Point(13, 52)
        '
        'GvVendor
        '
        Me.GvVendor.MasterTemplate.AllowAddNewRow = False
        Me.GvVendor.MasterTemplate.EnableFiltering = True
        Me.GvVendor.MasterTemplate.ShowHeaderCellButtons = True
        Me.GvVendor.Name = "GvVendor"
        Me.GvVendor.ShowGroupPanel = False
        Me.GvVendor.ShowHeaderCellButtons = True
        Me.GvVendor.Size = New System.Drawing.Size(401, 347)
        Me.GvVendor.TabIndex = 2
        Me.GvVendor.TabStop = False
        Me.GvVendor.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkVendorSelect)
        Me.Panel1.Controls.Add(Me.chkVendorAll)
        Me.Panel1.Location = New System.Drawing.Point(13, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(401, 29)
        Me.Panel1.TabIndex = 0
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(218, 8)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 1
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(169, 8)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 0
        Me.chkVendorAll.Text = "All"
        '
        'gbPriceCode
        '
        Me.gbPriceCode.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbPriceCode.Controls.Add(Me.gvPriceCode)
        Me.gbPriceCode.HeaderText = "Price Code"
        Me.gbPriceCode.Location = New System.Drawing.Point(4, 30)
        Me.gbPriceCode.Name = "gbPriceCode"
        Me.gbPriceCode.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbPriceCode.Size = New System.Drawing.Size(427, 381)
        Me.gbPriceCode.TabIndex = 113
        Me.gbPriceCode.Text = "Price Code"
        '
        'gvPriceCode
        '
        Me.gvPriceCode.Location = New System.Drawing.Point(3, 18)
        '
        'gvPriceCode
        '
        Me.gvPriceCode.MasterTemplate.AllowAddNewRow = False
        Me.gvPriceCode.MasterTemplate.EnableFiltering = True
        Me.gvPriceCode.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvPriceCode.Name = "gvPriceCode"
        Me.gvPriceCode.ShowGroupPanel = False
        Me.gvPriceCode.ShowHeaderCellButtons = True
        Me.gvPriceCode.Size = New System.Drawing.Size(411, 386)
        Me.gvPriceCode.TabIndex = 3
        Me.gvPriceCode.TabStop = False
        Me.gvPriceCode.Text = "RadGridView1"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(830, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(78, 21)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(94, 6)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(78, 21)
        Me.btndelete.TabIndex = 4
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(10, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(78, 21)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Save"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(399, 4)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 120
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(178, 6)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(78, 21)
        Me.btnPost.TabIndex = 6
        Me.btnPost.Text = "Post"
        '
        'frmVendorPriceChartMappingDocNoWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(911, 580)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmVendorPriceChartMappingDocNoWise"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RadForm2"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.lblPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.GvVendor.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GvVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPriceCode.ResumeLayout(False)
        CType(Me.gvPriceCode.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvPriceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndPriceCode As common.UserControls.txtFinder
    Friend WithEvents lblPriceCode As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gbPriceCode As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvPriceCode As common.UserControls.MyRadGridView
    Friend WithEvents GvVendor As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents txtDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents fndcode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
End Class

