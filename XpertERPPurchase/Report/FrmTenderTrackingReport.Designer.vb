<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTenderTrackingReport
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtItem = New common.Controls.MyTextBox()
        Me.txtTenderNo = New common.UserControls.txtFinder()
        Me.lblTenderNo = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.multiLocationFinder = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.lbltype = New common.Controls.MyLabel()
        Me.txtSrcDesc = New common.Controls.MyTextBox()
        Me.fndSrcCode = New common.UserControls.txtFinder()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtQty = New common.MyNumBox()
        Me.txtAccQty = New common.MyNumBox()
        Me.txtRejQty = New common.MyNumBox()
        Me.txtPenQty = New common.MyNumBox()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTenderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSrcDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAccQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRejQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPenQty, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtItem)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTenderNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTenderNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.multiLocationFinder)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbltype)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSrcDesc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndSrcCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1078, 483)
        Me.SplitContainer1.SplitterDistance = 85
        Me.SplitContainer1.TabIndex = 0
        '
        'txtItem
        '
        Me.txtItem.CalculationExpression = Nothing
        Me.txtItem.FieldCode = Nothing
        Me.txtItem.FieldDesc = Nothing
        Me.txtItem.FieldMaxLength = 0
        Me.txtItem.FieldName = Nothing
        Me.txtItem.isCalculatedField = False
        Me.txtItem.IsSourceFromTable = False
        Me.txtItem.IsSourceFromValueList = False
        Me.txtItem.IsUnique = False
        Me.txtItem.Location = New System.Drawing.Point(203, 32)
        Me.txtItem.MaxLength = 150
        Me.txtItem.MendatroryField = False
        Me.txtItem.MyLinkLable1 = Nothing
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.Name = "txtItem"
        Me.txtItem.ReadOnly = True
        Me.txtItem.ReferenceFieldDesc = Nothing
        Me.txtItem.ReferenceFieldName = Nothing
        Me.txtItem.ReferenceTableName = Nothing
        Me.txtItem.Size = New System.Drawing.Size(79, 20)
        Me.txtItem.TabIndex = 1522
        Me.txtItem.TabStop = False
        '
        'txtTenderNo
        '
        Me.txtTenderNo.CalculationExpression = Nothing
        Me.txtTenderNo.FieldCode = Nothing
        Me.txtTenderNo.FieldDesc = Nothing
        Me.txtTenderNo.FieldMaxLength = 0
        Me.txtTenderNo.FieldName = Nothing
        Me.txtTenderNo.isCalculatedField = False
        Me.txtTenderNo.IsSourceFromTable = False
        Me.txtTenderNo.IsSourceFromValueList = False
        Me.txtTenderNo.IsUnique = False
        Me.txtTenderNo.Location = New System.Drawing.Point(69, 9)
        Me.txtTenderNo.MendatroryField = True
        Me.txtTenderNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTenderNo.MyLinkLable1 = Me.lblTenderNo
        Me.txtTenderNo.MyLinkLable2 = Nothing
        Me.txtTenderNo.MyReadOnly = False
        Me.txtTenderNo.MyShowMasterFormButton = False
        Me.txtTenderNo.Name = "txtTenderNo"
        Me.txtTenderNo.ReferenceFieldDesc = Nothing
        Me.txtTenderNo.ReferenceFieldName = Nothing
        Me.txtTenderNo.ReferenceTableName = Nothing
        Me.txtTenderNo.Size = New System.Drawing.Size(128, 19)
        Me.txtTenderNo.TabIndex = 1521
        Me.txtTenderNo.Value = ""
        '
        'lblTenderNo
        '
        Me.lblTenderNo.FieldName = Nothing
        Me.lblTenderNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenderNo.Location = New System.Drawing.Point(12, 12)
        Me.lblTenderNo.Name = "lblTenderNo"
        Me.lblTenderNo.Size = New System.Drawing.Size(46, 16)
        Me.lblTenderNo.TabIndex = 1520
        Me.lblTenderNo.Text = "RAL No"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(203, 9)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(79, 18)
        Me.txtDate.TabIndex = 1519
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'multiLocationFinder
        '
        Me.multiLocationFinder.arrDispalyMember = Nothing
        Me.multiLocationFinder.arrValueMember = Nothing
        Me.multiLocationFinder.Location = New System.Drawing.Point(69, 56)
        Me.multiLocationFinder.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.multiLocationFinder.MyLinkLable1 = Nothing
        Me.multiLocationFinder.MyLinkLable2 = Nothing
        Me.multiLocationFinder.MyNullText = "All"
        Me.multiLocationFinder.Name = "multiLocationFinder"
        Me.multiLocationFinder.Size = New System.Drawing.Size(362, 19)
        Me.multiLocationFinder.TabIndex = 60
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(12, 56)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 59
        Me.lblLocation.Text = "Location"
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(440, 56)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 19)
        Me.btnReport.TabIndex = 4
        Me.btnReport.Text = ">>>"
        '
        'lbltype
        '
        Me.lbltype.FieldName = Nothing
        Me.lbltype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltype.Location = New System.Drawing.Point(12, 35)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(28, 16)
        Me.lbltype.TabIndex = 52
        Me.lbltype.Text = "Item"
        '
        'txtSrcDesc
        '
        Me.txtSrcDesc.CalculationExpression = Nothing
        Me.txtSrcDesc.FieldCode = Nothing
        Me.txtSrcDesc.FieldDesc = Nothing
        Me.txtSrcDesc.FieldMaxLength = 0
        Me.txtSrcDesc.FieldName = Nothing
        Me.txtSrcDesc.isCalculatedField = False
        Me.txtSrcDesc.IsSourceFromTable = False
        Me.txtSrcDesc.IsSourceFromValueList = False
        Me.txtSrcDesc.IsUnique = False
        Me.txtSrcDesc.Location = New System.Drawing.Point(287, 32)
        Me.txtSrcDesc.MaxLength = 150
        Me.txtSrcDesc.MendatroryField = False
        Me.txtSrcDesc.MyLinkLable1 = Nothing
        Me.txtSrcDesc.MyLinkLable2 = Nothing
        Me.txtSrcDesc.Name = "txtSrcDesc"
        Me.txtSrcDesc.ReadOnly = True
        Me.txtSrcDesc.ReferenceFieldDesc = Nothing
        Me.txtSrcDesc.ReferenceFieldName = Nothing
        Me.txtSrcDesc.ReferenceTableName = Nothing
        Me.txtSrcDesc.Size = New System.Drawing.Size(223, 20)
        Me.txtSrcDesc.TabIndex = 50
        Me.txtSrcDesc.TabStop = False
        '
        'fndSrcCode
        '
        Me.fndSrcCode.CalculationExpression = Nothing
        Me.fndSrcCode.FieldCode = Nothing
        Me.fndSrcCode.FieldDesc = Nothing
        Me.fndSrcCode.FieldMaxLength = 0
        Me.fndSrcCode.FieldName = Nothing
        Me.fndSrcCode.isCalculatedField = False
        Me.fndSrcCode.IsSourceFromTable = False
        Me.fndSrcCode.IsSourceFromValueList = False
        Me.fndSrcCode.IsUnique = False
        Me.fndSrcCode.Location = New System.Drawing.Point(69, 32)
        Me.fndSrcCode.MendatroryField = True
        Me.fndSrcCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSrcCode.MyLinkLable1 = Nothing
        Me.fndSrcCode.MyLinkLable2 = Nothing
        Me.fndSrcCode.MyReadOnly = False
        Me.fndSrcCode.MyShowMasterFormButton = False
        Me.fndSrcCode.Name = "fndSrcCode"
        Me.fndSrcCode.ReferenceFieldDesc = Nothing
        Me.fndSrcCode.ReferenceFieldName = Nothing
        Me.fndSrcCode.ReferenceTableName = Nothing
        Me.fndSrcCode.Size = New System.Drawing.Size(128, 19)
        Me.fndSrcCode.TabIndex = 3
        Me.fndSrcCode.Value = ""
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Size = New System.Drawing.Size(1078, 394)
        Me.SplitContainer2.SplitterDistance = 355
        Me.SplitContainer2.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1078, 355)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.ThemeName = "ControlDefault"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gv1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1057, 307)
        Me.RadPageViewPage1.Text = "Detail"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1057, 307)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(88, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(79, 19)
        Me.btnExport.TabIndex = 83
        Me.btnExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(70, 19)
        Me.btnreset.TabIndex = 0
        Me.btnreset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(996, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 19)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer3.Size = New System.Drawing.Size(1078, 512)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 1
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1078, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPenQty)
        Me.GroupBox1.Controls.Add(Me.txtRejQty)
        Me.GroupBox1.Controls.Add(Me.txtAccQty)
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.MyLabel4)
        Me.GroupBox1.Controls.Add(Me.MyLabel3)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Location = New System.Drawing.Point(670, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(396, 68)
        Me.GroupBox1.TabIndex = 1523
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "RAL Detail"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 23)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel1.TabIndex = 1521
        Me.MyLabel1.Text = "RAL Quantity"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 46)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel2.TabIndex = 1522
        Me.MyLabel2.Text = "Accepted Qty"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(204, 46)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel3.TabIndex = 1523
        Me.MyLabel3.Text = "Pending Qty"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(204, 23)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel4.TabIndex = 1524
        Me.MyLabel4.Text = "Rejected Qty"
        '
        'txtQty
        '
        Me.txtQty.BackColor = System.Drawing.Color.White
        Me.txtQty.CalculationExpression = Nothing
        Me.txtQty.DecimalPlaces = 2
        Me.txtQty.FieldCode = Nothing
        Me.txtQty.FieldDesc = Nothing
        Me.txtQty.FieldMaxLength = 0
        Me.txtQty.FieldName = Nothing
        Me.txtQty.isCalculatedField = False
        Me.txtQty.IsSourceFromTable = False
        Me.txtQty.IsSourceFromValueList = False
        Me.txtQty.IsUnique = False
        Me.txtQty.Location = New System.Drawing.Point(86, 20)
        Me.txtQty.MaxLength = 2
        Me.txtQty.MendatroryField = False
        Me.txtQty.MyLinkLable1 = Nothing
        Me.txtQty.MyLinkLable2 = Nothing
        Me.txtQty.Name = "txtQty"
        Me.txtQty.ReadOnly = True
        Me.txtQty.ReferenceFieldDesc = Nothing
        Me.txtQty.ReferenceFieldName = Nothing
        Me.txtQty.ReferenceTableName = Nothing
        Me.txtQty.Size = New System.Drawing.Size(109, 20)
        Me.txtQty.TabIndex = 1525
        Me.txtQty.Text = "0"
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQty.Value = 0R
        '
        'txtAccQty
        '
        Me.txtAccQty.BackColor = System.Drawing.Color.White
        Me.txtAccQty.CalculationExpression = Nothing
        Me.txtAccQty.DecimalPlaces = 2
        Me.txtAccQty.FieldCode = Nothing
        Me.txtAccQty.FieldDesc = Nothing
        Me.txtAccQty.FieldMaxLength = 0
        Me.txtAccQty.FieldName = Nothing
        Me.txtAccQty.isCalculatedField = False
        Me.txtAccQty.IsSourceFromTable = False
        Me.txtAccQty.IsSourceFromValueList = False
        Me.txtAccQty.IsUnique = False
        Me.txtAccQty.Location = New System.Drawing.Point(86, 43)
        Me.txtAccQty.MaxLength = 2
        Me.txtAccQty.MendatroryField = False
        Me.txtAccQty.MyLinkLable1 = Nothing
        Me.txtAccQty.MyLinkLable2 = Nothing
        Me.txtAccQty.Name = "txtAccQty"
        Me.txtAccQty.ReadOnly = True
        Me.txtAccQty.ReferenceFieldDesc = Nothing
        Me.txtAccQty.ReferenceFieldName = Nothing
        Me.txtAccQty.ReferenceTableName = Nothing
        Me.txtAccQty.Size = New System.Drawing.Size(109, 20)
        Me.txtAccQty.TabIndex = 1526
        Me.txtAccQty.Text = "0"
        Me.txtAccQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAccQty.Value = 0R
        '
        'txtRejQty
        '
        Me.txtRejQty.BackColor = System.Drawing.Color.White
        Me.txtRejQty.CalculationExpression = Nothing
        Me.txtRejQty.DecimalPlaces = 2
        Me.txtRejQty.FieldCode = Nothing
        Me.txtRejQty.FieldDesc = Nothing
        Me.txtRejQty.FieldMaxLength = 0
        Me.txtRejQty.FieldName = Nothing
        Me.txtRejQty.isCalculatedField = False
        Me.txtRejQty.IsSourceFromTable = False
        Me.txtRejQty.IsSourceFromValueList = False
        Me.txtRejQty.IsUnique = False
        Me.txtRejQty.Location = New System.Drawing.Point(282, 20)
        Me.txtRejQty.MaxLength = 2
        Me.txtRejQty.MendatroryField = False
        Me.txtRejQty.MyLinkLable1 = Nothing
        Me.txtRejQty.MyLinkLable2 = Nothing
        Me.txtRejQty.Name = "txtRejQty"
        Me.txtRejQty.ReadOnly = True
        Me.txtRejQty.ReferenceFieldDesc = Nothing
        Me.txtRejQty.ReferenceFieldName = Nothing
        Me.txtRejQty.ReferenceTableName = Nothing
        Me.txtRejQty.Size = New System.Drawing.Size(108, 20)
        Me.txtRejQty.TabIndex = 1527
        Me.txtRejQty.Text = "0"
        Me.txtRejQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRejQty.Value = 0R
        '
        'txtPenQty
        '
        Me.txtPenQty.BackColor = System.Drawing.Color.White
        Me.txtPenQty.CalculationExpression = Nothing
        Me.txtPenQty.DecimalPlaces = 2
        Me.txtPenQty.FieldCode = Nothing
        Me.txtPenQty.FieldDesc = Nothing
        Me.txtPenQty.FieldMaxLength = 0
        Me.txtPenQty.FieldName = Nothing
        Me.txtPenQty.isCalculatedField = False
        Me.txtPenQty.IsSourceFromTable = False
        Me.txtPenQty.IsSourceFromValueList = False
        Me.txtPenQty.IsUnique = False
        Me.txtPenQty.Location = New System.Drawing.Point(282, 43)
        Me.txtPenQty.MaxLength = 2
        Me.txtPenQty.MendatroryField = False
        Me.txtPenQty.MyLinkLable1 = Nothing
        Me.txtPenQty.MyLinkLable2 = Nothing
        Me.txtPenQty.Name = "txtPenQty"
        Me.txtPenQty.ReadOnly = True
        Me.txtPenQty.ReferenceFieldDesc = Nothing
        Me.txtPenQty.ReferenceFieldName = Nothing
        Me.txtPenQty.ReferenceTableName = Nothing
        Me.txtPenQty.Size = New System.Drawing.Size(108, 20)
        Me.txtPenQty.TabIndex = 1528
        Me.txtPenQty.Text = "0"
        Me.txtPenQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPenQty.Value = 0R
        '
        'FrmTenderTrackingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1078, 512)
        Me.Controls.Add(Me.SplitContainer3)
        Me.Name = "FrmTenderTrackingReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Tender Tracking Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTenderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSrcDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAccQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRejQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPenQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents fndSrcCode As common.UserControls.txtFinder
    Friend WithEvents lbltype As common.Controls.MyLabel
    Friend WithEvents txtSrcDesc As common.Controls.MyTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReport As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents multiLocationFinder As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblTenderNo As common.Controls.MyLabel
    Friend WithEvents txtTenderNo As common.UserControls.txtFinder
    Friend WithEvents txtItem As common.Controls.MyTextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtPenQty As common.MyNumBox
    Friend WithEvents txtRejQty As common.MyNumBox
    Friend WithEvents txtAccQty As common.MyNumBox
    Friend WithEvents txtQty As common.MyNumBox
End Class

