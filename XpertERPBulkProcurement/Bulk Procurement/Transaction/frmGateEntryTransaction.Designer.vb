<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGateEntryTransaction
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
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBoxRoute = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtFinder()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadGroupBoxVendor = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.txtVendor = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblDate = New common.Controls.MyLabel()
        Me.RadGroupBoxCust = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtCustomer = New common.UserControls.txtFinder()
        Me.cmbReportType = New common.Controls.MyComboBox()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBoxRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxRoute.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBoxVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxVendor.SuspendLayout()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBoxCust, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBoxCust.SuspendLayout()
        CType(Me.cmbReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 418
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 418)
        Me.RadPageView1.TabIndex = 5
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 370)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(779, 370)
        Me.RadPanel1.TabIndex = 15
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBoxRoute)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBoxVendor)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.lblDate)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBoxCust)
        Me.RadGroupBox1.Controls.Add(Me.cmbReportType)
        Me.RadGroupBox1.Controls.Add(Me.txtDate)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(283, 89)
        Me.RadGroupBox1.TabIndex = 457
        '
        'RadGroupBoxRoute
        '
        Me.RadGroupBoxRoute.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxRoute.Controls.Add(Me.MyLabel1)
        Me.RadGroupBoxRoute.Controls.Add(Me.txtRouteNo)
        Me.RadGroupBoxRoute.HeaderText = ""
        Me.RadGroupBoxRoute.Location = New System.Drawing.Point(3, 58)
        Me.RadGroupBoxRoute.Name = "RadGroupBoxRoute"
        Me.RadGroupBoxRoute.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBoxRoute.Size = New System.Drawing.Size(273, 27)
        Me.RadGroupBoxRoute.TabIndex = 462
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(2, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel1.TabIndex = 390
        Me.MyLabel1.Text = "Route"
        '
        'txtRouteNo
        '
        Me.txtRouteNo.CalculationExpression = Nothing
        Me.txtRouteNo.FieldCode = Nothing
        Me.txtRouteNo.FieldDesc = Nothing
        Me.txtRouteNo.FieldMaxLength = 0
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.isCalculatedField = False
        Me.txtRouteNo.IsSourceFromTable = False
        Me.txtRouteNo.IsSourceFromValueList = False
        Me.txtRouteNo.IsUnique = False
        Me.txtRouteNo.Location = New System.Drawing.Point(93, 4)
        Me.txtRouteNo.MendatroryField = True
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Me.RadLabel2
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.MyShowMasterFormButton = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.ReferenceFieldDesc = Nothing
        Me.txtRouteNo.ReferenceFieldName = Nothing
        Me.txtRouteNo.ReferenceTableName = Nothing
        Me.txtRouteNo.Size = New System.Drawing.Size(171, 20)
        Me.txtRouteNo.TabIndex = 459
        Me.txtRouteNo.Value = ""
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(1, 5)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(73, 16)
        Me.RadLabel2.TabIndex = 459
        Me.RadLabel2.Text = "Customer No"
        '
        'RadGroupBoxVendor
        '
        Me.RadGroupBoxVendor.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxVendor.Controls.Add(Me.lblVendor)
        Me.RadGroupBoxVendor.Controls.Add(Me.txtVendor)
        Me.RadGroupBoxVendor.HeaderText = ""
        Me.RadGroupBoxVendor.Location = New System.Drawing.Point(3, 58)
        Me.RadGroupBoxVendor.Name = "RadGroupBoxVendor"
        Me.RadGroupBoxVendor.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBoxVendor.Size = New System.Drawing.Size(273, 27)
        Me.RadGroupBoxVendor.TabIndex = 461
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendor.Location = New System.Drawing.Point(2, 4)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(46, 18)
        Me.lblVendor.TabIndex = 390
        Me.lblVendor.Text = "Vendor "
        '
        'txtVendor
        '
        Me.txtVendor.CalculationExpression = Nothing
        Me.txtVendor.FieldCode = Nothing
        Me.txtVendor.FieldDesc = Nothing
        Me.txtVendor.FieldMaxLength = 0
        Me.txtVendor.FieldName = Nothing
        Me.txtVendor.isCalculatedField = False
        Me.txtVendor.IsSourceFromTable = False
        Me.txtVendor.IsSourceFromValueList = False
        Me.txtVendor.IsUnique = False
        Me.txtVendor.Location = New System.Drawing.Point(94, 4)
        Me.txtVendor.MendatroryField = True
        Me.txtVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendor.MyLinkLable1 = Me.RadLabel2
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.MyReadOnly = False
        Me.txtVendor.MyShowMasterFormButton = False
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.ReferenceFieldDesc = Nothing
        Me.txtVendor.ReferenceFieldName = Nothing
        Me.txtVendor.ReferenceTableName = Nothing
        Me.txtVendor.Size = New System.Drawing.Size(170, 20)
        Me.txtVendor.TabIndex = 459
        Me.txtVendor.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(4, 34)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel6.TabIndex = 456
        Me.MyLabel6.Text = "Report Type"
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Location = New System.Drawing.Point(4, 9)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 18)
        Me.lblDate.TabIndex = 77
        Me.lblDate.Text = "Date"
        '
        'RadGroupBoxCust
        '
        Me.RadGroupBoxCust.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBoxCust.Controls.Add(Me.RadLabel2)
        Me.RadGroupBoxCust.Controls.Add(Me.txtCustomer)
        Me.RadGroupBoxCust.HeaderText = ""
        Me.RadGroupBoxCust.Location = New System.Drawing.Point(3, 58)
        Me.RadGroupBoxCust.Name = "RadGroupBoxCust"
        Me.RadGroupBoxCust.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBoxCust.Size = New System.Drawing.Size(273, 27)
        Me.RadGroupBoxCust.TabIndex = 458
        '
        'txtCustomer
        '
        Me.txtCustomer.CalculationExpression = Nothing
        Me.txtCustomer.FieldCode = Nothing
        Me.txtCustomer.FieldDesc = Nothing
        Me.txtCustomer.FieldMaxLength = 0
        Me.txtCustomer.FieldName = Nothing
        Me.txtCustomer.isCalculatedField = False
        Me.txtCustomer.IsSourceFromTable = False
        Me.txtCustomer.IsSourceFromValueList = False
        Me.txtCustomer.IsUnique = False
        Me.txtCustomer.Location = New System.Drawing.Point(93, 3)
        Me.txtCustomer.MendatroryField = True
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.RadLabel2
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyReadOnly = False
        Me.txtCustomer.MyShowMasterFormButton = False
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReferenceFieldDesc = Nothing
        Me.txtCustomer.ReferenceFieldName = Nothing
        Me.txtCustomer.ReferenceTableName = Nothing
        Me.txtCustomer.Size = New System.Drawing.Size(170, 20)
        Me.txtCustomer.TabIndex = 458
        Me.txtCustomer.Value = ""
        '
        'cmbReportType
        '
        Me.cmbReportType.AutoCompleteDisplayMember = Nothing
        Me.cmbReportType.AutoCompleteValueMember = Nothing
        Me.cmbReportType.CalculationExpression = Nothing
        Me.cmbReportType.DropDownAnimationEnabled = True
        Me.cmbReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbReportType.FieldCode = Nothing
        Me.cmbReportType.FieldDesc = Nothing
        Me.cmbReportType.FieldMaxLength = 0
        Me.cmbReportType.FieldName = Nothing
        Me.cmbReportType.isCalculatedField = False
        Me.cmbReportType.IsSourceFromTable = False
        Me.cmbReportType.IsSourceFromValueList = False
        Me.cmbReportType.IsUnique = False
        RadListDataItem5.Text = "Select"
        RadListDataItem6.Text = "Route"
        RadListDataItem7.Text = "Purchase"
        RadListDataItem8.Text = "Conversion In"
        Me.cmbReportType.Items.Add(RadListDataItem5)
        Me.cmbReportType.Items.Add(RadListDataItem6)
        Me.cmbReportType.Items.Add(RadListDataItem7)
        Me.cmbReportType.Items.Add(RadListDataItem8)
        Me.cmbReportType.Location = New System.Drawing.Point(96, 34)
        Me.cmbReportType.MendatroryField = True
        Me.cmbReportType.MyLinkLable1 = Nothing
        Me.cmbReportType.MyLinkLable2 = Nothing
        Me.cmbReportType.Name = "cmbReportType"
        Me.cmbReportType.NullText = "Please Select"
        Me.cmbReportType.ReferenceFieldDesc = Nothing
        Me.cmbReportType.ReferenceFieldName = Nothing
        Me.cmbReportType.ReferenceTableName = Nothing
        Me.cmbReportType.Size = New System.Drawing.Size(180, 20)
        Me.cmbReportType.TabIndex = 455
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(96, 9)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(78, 20)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "30/05/2011"
        Me.txtDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 370)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 370)
        Me.gv1.TabIndex = 2
        Me.gv1.VarID = ""
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(250, 1)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(71, 21)
        Me.btnPost.TabIndex = 336
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(713, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 21)
        Me.btnClose.TabIndex = 79
        Me.btnClose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(89, 1)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(77, 21)
        Me.btnExport.TabIndex = 335
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseCompatibleTextRendering = False
        '
        'btnPDF
        '
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.UseCompatibleTextRendering = False
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(12, 1)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 21)
        Me.btnGo.TabIndex = 176
        Me.btnGo.Text = ">>>"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreset.Location = New System.Drawing.Point(171, 1)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(75, 21)
        Me.btnreset.TabIndex = 19
        Me.btnreset.Text = "Reset"
        '
        'frmGateEntryTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmGateEntryTransaction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Gate Entry Transaction"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBoxRoute, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxRoute.ResumeLayout(False)
        Me.RadGroupBoxRoute.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBoxVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxVendor.ResumeLayout(False)
        Me.RadGroupBoxVendor.PerformLayout()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBoxCust, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBoxCust.ResumeLayout(False)
        Me.RadGroupBoxCust.PerformLayout()
        CType(Me.cmbReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnreset As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnPost As RadButton
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents cmbReportType As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtFinder
    Friend WithEvents RadGroupBoxCust As RadGroupBox
    Friend WithEvents RadGroupBoxVendor As RadGroupBox
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents txtVendor As common.UserControls.txtFinder
    Friend WithEvents RadGroupBoxRoute As RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtRouteNo As common.UserControls.txtFinder
End Class
