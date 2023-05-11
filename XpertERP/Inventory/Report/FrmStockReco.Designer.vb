<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmStockReco
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkShowTransactionData = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkNoTransaction = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkExcludeConsumptionLoc = New Telerik.WinControls.UI.RadCheckBox()
        Me.cboFATSNF = New common.Controls.MyComboBox()
        Me.cboFatSNFType = New common.Controls.MyComboBox()
        Me.chkIncludeGIT = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkProd_WIP = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.cboDisplayMethod = New common.Controls.MyComboBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtItemType = New common.UserControls.txtMultiSelectFinder()
        Me.ChkMRPWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvLocation = New common.UserControls.MyRadGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rbtnLocationSelect = New common.Controls.MyRadioButton()
        Me.rbtnLocationAll = New common.Controls.MyRadioButton()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvCategory = New common.UserControls.MyRadGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton()
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton()
        Me.RadButton6 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton7 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.cboInOutType = New common.Controls.MyComboBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtItemGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtTransaction = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.cmbUnit = New common.Controls.MyComboBox()
        Me.lblModeofTransport = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.vsb = New System.Windows.Forms.VScrollBar()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDetail = New common.UserControls.MyRadGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItemSaveeLayout3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemSett1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnQuickExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.QExpExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.QExpCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.BulkCSV = New Telerik.WinControls.UI.RadMenuItem()
        Me.ExcelGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDFGrid = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkPartiallyLoad = New Telerik.WinControls.UI.RadCheckBox()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.chkShowTransactionData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNoTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExcludeConsumptionLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFATSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFatSNFType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIncludeGIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProd_WIP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDisplayMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkMRPWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboInOutType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPartiallyLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(888, 404)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkShowTransactionData)
        Me.RadPageViewPage1.Controls.Add(Me.chkNoTransaction)
        Me.RadPageViewPage1.Controls.Add(Me.chkExcludeConsumptionLoc)
        Me.RadPageViewPage1.Controls.Add(Me.cboFATSNF)
        Me.RadPageViewPage1.Controls.Add(Me.cboFatSNFType)
        Me.RadPageViewPage1.Controls.Add(Me.chkIncludeGIT)
        Me.RadPageViewPage1.Controls.Add(Me.chkProd_WIP)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel9)
        Me.RadPageViewPage1.Controls.Add(Me.cboDisplayMethod)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel8)
        Me.RadPageViewPage1.Controls.Add(Me.txtItemType)
        Me.RadPageViewPage1.Controls.Add(Me.ChkMRPWise)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel7)
        Me.RadPageViewPage1.Controls.Add(Me.cboInOutType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtItemGroup)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtTransaction)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtItem)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.cmbUnit)
        Me.RadPageViewPage1.Controls.Add(Me.lblModeofTransport)
        Me.RadPageViewPage1.Controls.Add(Me.cboType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(867, 356)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'chkShowTransactionData
        '
        Me.chkShowTransactionData.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowTransactionData.Location = New System.Drawing.Point(69, 92)
        Me.chkShowTransactionData.Name = "chkShowTransactionData"
        Me.chkShowTransactionData.Size = New System.Drawing.Size(138, 16)
        Me.chkShowTransactionData.TabIndex = 352
        Me.chkShowTransactionData.Text = "Show Transaction Data"
        '
        'chkNoTransaction
        '
        Me.chkNoTransaction.Enabled = False
        Me.chkNoTransaction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNoTransaction.Location = New System.Drawing.Point(572, 89)
        Me.chkNoTransaction.Name = "chkNoTransaction"
        Me.chkNoTransaction.Size = New System.Drawing.Size(97, 16)
        Me.chkNoTransaction.TabIndex = 351
        Me.chkNoTransaction.Text = "No Transaction"
        '
        'chkExcludeConsumptionLoc
        '
        Me.chkExcludeConsumptionLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExcludeConsumptionLoc.Location = New System.Drawing.Point(672, 89)
        Me.chkExcludeConsumptionLoc.Name = "chkExcludeConsumptionLoc"
        Me.chkExcludeConsumptionLoc.Size = New System.Drawing.Size(176, 16)
        Me.chkExcludeConsumptionLoc.TabIndex = 350
        Me.chkExcludeConsumptionLoc.Text = "Exclude Consumption Location"
        '
        'cboFATSNF
        '
        Me.cboFATSNF.AutoCompleteDisplayMember = Nothing
        Me.cboFATSNF.AutoCompleteValueMember = Nothing
        Me.cboFATSNF.CalculationExpression = Nothing
        Me.cboFATSNF.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFATSNF.FieldCode = Nothing
        Me.cboFATSNF.FieldDesc = Nothing
        Me.cboFATSNF.FieldMaxLength = 0
        Me.cboFATSNF.FieldName = Nothing
        Me.cboFATSNF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFATSNF.isCalculatedField = False
        Me.cboFATSNF.IsSourceFromTable = False
        Me.cboFATSNF.IsSourceFromValueList = False
        Me.cboFATSNF.IsUnique = False
        Me.cboFATSNF.Location = New System.Drawing.Point(397, 67)
        Me.cboFATSNF.MendatroryField = False
        Me.cboFATSNF.MyLinkLable1 = Nothing
        Me.cboFATSNF.MyLinkLable2 = Nothing
        Me.cboFATSNF.Name = "cboFATSNF"
        Me.cboFATSNF.ReferenceFieldDesc = Nothing
        Me.cboFATSNF.ReferenceFieldName = Nothing
        Me.cboFATSNF.ReferenceTableName = Nothing
        Me.cboFATSNF.Size = New System.Drawing.Size(83, 18)
        Me.cboFATSNF.TabIndex = 349
        '
        'cboFatSNFType
        '
        Me.cboFatSNFType.AutoCompleteDisplayMember = Nothing
        Me.cboFatSNFType.AutoCompleteValueMember = Nothing
        Me.cboFatSNFType.CalculationExpression = Nothing
        Me.cboFatSNFType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboFatSNFType.FieldCode = Nothing
        Me.cboFatSNFType.FieldDesc = Nothing
        Me.cboFatSNFType.FieldMaxLength = 0
        Me.cboFatSNFType.FieldName = Nothing
        Me.cboFatSNFType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFatSNFType.isCalculatedField = False
        Me.cboFatSNFType.IsSourceFromTable = False
        Me.cboFatSNFType.IsSourceFromValueList = False
        Me.cboFatSNFType.IsUnique = False
        Me.cboFatSNFType.Location = New System.Drawing.Point(481, 67)
        Me.cboFatSNFType.MendatroryField = False
        Me.cboFatSNFType.MyLinkLable1 = Nothing
        Me.cboFatSNFType.MyLinkLable2 = Nothing
        Me.cboFatSNFType.Name = "cboFatSNFType"
        Me.cboFatSNFType.ReferenceFieldDesc = Nothing
        Me.cboFatSNFType.ReferenceFieldName = Nothing
        Me.cboFatSNFType.ReferenceTableName = Nothing
        Me.cboFatSNFType.Size = New System.Drawing.Size(141, 18)
        Me.cboFatSNFType.TabIndex = 347
        '
        'chkIncludeGIT
        '
        Me.chkIncludeGIT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeGIT.Location = New System.Drawing.Point(672, 67)
        Me.chkIncludeGIT.Name = "chkIncludeGIT"
        Me.chkIncludeGIT.Size = New System.Drawing.Size(79, 16)
        Me.chkIncludeGIT.TabIndex = 11
        Me.chkIncludeGIT.Text = "Include GIT"
        '
        'chkProd_WIP
        '
        Me.chkProd_WIP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProd_WIP.Location = New System.Drawing.Point(755, 67)
        Me.chkProd_WIP.Name = "chkProd_WIP"
        Me.chkProd_WIP.Size = New System.Drawing.Size(99, 16)
        Me.chkProd_WIP.TabIndex = 346
        Me.chkProd_WIP.Text = "Production WIP"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(624, 46)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(42, 18)
        Me.MyLabel9.TabIndex = 345
        Me.MyLabel9.Text = "Display"
        '
        'cboDisplayMethod
        '
        Me.cboDisplayMethod.AutoCompleteDisplayMember = Nothing
        Me.cboDisplayMethod.AutoCompleteValueMember = Nothing
        Me.cboDisplayMethod.CalculationExpression = Nothing
        Me.cboDisplayMethod.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboDisplayMethod.FieldCode = Nothing
        Me.cboDisplayMethod.FieldDesc = Nothing
        Me.cboDisplayMethod.FieldMaxLength = 0
        Me.cboDisplayMethod.FieldName = Nothing
        Me.cboDisplayMethod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDisplayMethod.isCalculatedField = False
        Me.cboDisplayMethod.IsSourceFromTable = False
        Me.cboDisplayMethod.IsSourceFromValueList = False
        Me.cboDisplayMethod.IsUnique = False
        Me.cboDisplayMethod.Location = New System.Drawing.Point(672, 46)
        Me.cboDisplayMethod.MendatroryField = False
        Me.cboDisplayMethod.MyLinkLable1 = Me.MyLabel9
        Me.cboDisplayMethod.MyLinkLable2 = Nothing
        Me.cboDisplayMethod.Name = "cboDisplayMethod"
        Me.cboDisplayMethod.ReferenceFieldDesc = Nothing
        Me.cboDisplayMethod.ReferenceFieldName = Nothing
        Me.cboDisplayMethod.ReferenceTableName = Nothing
        Me.cboDisplayMethod.Size = New System.Drawing.Size(182, 18)
        Me.cboDisplayMethod.TabIndex = 344
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(397, 25)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel8.TabIndex = 343
        Me.MyLabel8.Text = "Item Type"
        '
        'txtItemType
        '
        Me.txtItemType.arrDispalyMember = Nothing
        Me.txtItemType.arrValueMember = Nothing
        Me.txtItemType.Location = New System.Drawing.Point(481, 25)
        Me.txtItemType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.MyLinkLable1 = Me.MyLabel8
        Me.txtItemType.MyLinkLable2 = Nothing
        Me.txtItemType.MyNullText = "All"
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.Size = New System.Drawing.Size(373, 19)
        Me.txtItemType.TabIndex = 4
        '
        'ChkMRPWise
        '
        Me.ChkMRPWise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkMRPWise.Location = New System.Drawing.Point(624, 67)
        Me.ChkMRPWise.Name = "ChkMRPWise"
        Me.ChkMRPWise.Size = New System.Drawing.Size(45, 16)
        Me.ChkMRPWise.TabIndex = 9
        Me.ChkMRPWise.Text = "MRP"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gvLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 105)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(388, 249)
        Me.RadGroupBox2.TabIndex = 9
        Me.RadGroupBox2.Text = "Location"
        '
        'gvLocation
        '
        Me.gvLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvLocation.Location = New System.Drawing.Point(10, 40)
        '
        'gvLocation
        '
        Me.gvLocation.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvLocation.Name = "gvLocation"
        Me.gvLocation.ShowHeaderCellButtons = True
        Me.gvLocation.Size = New System.Drawing.Size(368, 199)
        Me.gvLocation.TabIndex = 3
        Me.gvLocation.Text = "RadGridView1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.SplitContainer1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(368, 20)
        Me.Panel4.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnLocationSelect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rbtnLocationAll)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton4)
        Me.SplitContainer1.Size = New System.Drawing.Size(368, 20)
        Me.SplitContainer1.SplitterDistance = 181
        Me.SplitContainer1.TabIndex = 0
        '
        'rbtnLocationSelect
        '
        Me.rbtnLocationSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationSelect.Location = New System.Drawing.Point(94, 1)
        Me.rbtnLocationSelect.MyLinkLable1 = Nothing
        Me.rbtnLocationSelect.MyLinkLable2 = Nothing
        Me.rbtnLocationSelect.Name = "rbtnLocationSelect"
        Me.rbtnLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnLocationSelect.TabIndex = 2
        Me.rbtnLocationSelect.Text = "Select"
        '
        'rbtnLocationAll
        '
        Me.rbtnLocationAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationAll.Location = New System.Drawing.Point(55, 1)
        Me.rbtnLocationAll.MyLinkLable1 = Nothing
        Me.rbtnLocationAll.MyLinkLable2 = Nothing
        Me.rbtnLocationAll.Name = "rbtnLocationAll"
        Me.rbtnLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnLocationAll.TabIndex = 1
        Me.rbtnLocationAll.Text = "All"
        '
        'RadButton5
        '
        Me.RadButton5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton5.Location = New System.Drawing.Point(93, 2)
        Me.RadButton5.Name = "RadButton5"
        Me.RadButton5.Size = New System.Drawing.Size(86, 15)
        Me.RadButton5.TabIndex = 3
        Me.RadButton5.Text = "Unselect All"
        '
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Location = New System.Drawing.Point(4, 2)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(86, 15)
        Me.RadButton4.TabIndex = 2
        Me.RadButton4.Text = "Select All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox3.Controls.Add(Me.gvCategory)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Category"
        Me.RadGroupBox3.Location = New System.Drawing.Point(402, 105)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(452, 248)
        Me.RadGroupBox3.TabIndex = 10
        Me.RadGroupBox3.Text = "Category"
        '
        'gvCategory
        '
        Me.gvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCategory.Location = New System.Drawing.Point(10, 40)
        '
        'gvCategory
        '
        Me.gvCategory.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCategory.Name = "gvCategory"
        Me.gvCategory.ShowHeaderCellButtons = True
        Me.gvCategory.Size = New System.Drawing.Size(432, 198)
        Me.gvCategory.TabIndex = 2
        Me.gvCategory.Text = "RadGridView1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.SplitContainer2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(432, 20)
        Me.Panel2.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCategorySelect)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rbtnCategoryAll)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton6)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton7)
        Me.SplitContainer2.Size = New System.Drawing.Size(432, 20)
        Me.SplitContainer2.SplitterDistance = 207
        Me.SplitContainer2.TabIndex = 2
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(102, 1)
        Me.rbtnCategorySelect.MyLinkLable1 = Nothing
        Me.rbtnCategorySelect.MyLinkLable2 = Nothing
        Me.rbtnCategorySelect.Name = "rbtnCategorySelect"
        Me.rbtnCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCategorySelect.TabIndex = 2
        Me.rbtnCategorySelect.Text = "Select"
        '
        'rbtnCategoryAll
        '
        Me.rbtnCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(63, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 1
        Me.rbtnCategoryAll.Text = "All"
        '
        'RadButton6
        '
        Me.RadButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton6.Location = New System.Drawing.Point(95, 3)
        Me.RadButton6.Name = "RadButton6"
        Me.RadButton6.Size = New System.Drawing.Size(86, 15)
        Me.RadButton6.TabIndex = 5
        Me.RadButton6.Text = "Unselect All"
        '
        'RadButton7
        '
        Me.RadButton7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton7.Location = New System.Drawing.Point(6, 3)
        Me.RadButton7.Name = "RadButton7"
        Me.RadButton7.Size = New System.Drawing.Size(86, 15)
        Me.RadButton7.TabIndex = 4
        Me.RadButton7.Text = "Select All"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(287, 4)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(39, 18)
        Me.MyLabel7.TabIndex = 340
        Me.MyLabel7.Text = "In/Out"
        '
        'cboInOutType
        '
        Me.cboInOutType.AutoCompleteDisplayMember = Nothing
        Me.cboInOutType.AutoCompleteValueMember = Nothing
        Me.cboInOutType.CalculationExpression = Nothing
        Me.cboInOutType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboInOutType.FieldCode = Nothing
        Me.cboInOutType.FieldDesc = Nothing
        Me.cboInOutType.FieldMaxLength = 0
        Me.cboInOutType.FieldName = Nothing
        Me.cboInOutType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInOutType.isCalculatedField = False
        Me.cboInOutType.IsSourceFromTable = False
        Me.cboInOutType.IsSourceFromValueList = False
        Me.cboInOutType.IsUnique = False
        RadListDataItem1.Text = "By Road"
        RadListDataItem2.Text = "By Air"
        RadListDataItem3.Text = "By Sea"
        Me.cboInOutType.Items.Add(RadListDataItem1)
        Me.cboInOutType.Items.Add(RadListDataItem2)
        Me.cboInOutType.Items.Add(RadListDataItem3)
        Me.cboInOutType.Location = New System.Drawing.Point(333, 4)
        Me.cboInOutType.MendatroryField = False
        Me.cboInOutType.MyLinkLable1 = Me.MyLabel7
        Me.cboInOutType.MyLinkLable2 = Nothing
        Me.cboInOutType.Name = "cboInOutType"
        Me.cboInOutType.ReferenceFieldDesc = Nothing
        Me.cboInOutType.ReferenceFieldName = Nothing
        Me.cboInOutType.ReferenceTableName = Nothing
        Me.cboInOutType.Size = New System.Drawing.Size(61, 18)
        Me.cboInOutType.TabIndex = 6
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(0, 46)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(63, 18)
        Me.MyLabel6.TabIndex = 338
        Me.MyLabel6.Text = "Item Group"
        '
        'txtItemGroup
        '
        Me.txtItemGroup.arrDispalyMember = Nothing
        Me.txtItemGroup.arrValueMember = Nothing
        Me.txtItemGroup.Location = New System.Drawing.Point(69, 46)
        Me.txtItemGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemGroup.MyLinkLable1 = Me.MyLabel6
        Me.txtItemGroup.MyLinkLable2 = Nothing
        Me.txtItemGroup.MyNullText = "All"
        Me.txtItemGroup.Name = "txtItemGroup"
        Me.txtItemGroup.Size = New System.Drawing.Size(325, 19)
        Me.txtItemGroup.TabIndex = 5
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(0, 25)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel5.TabIndex = 30
        Me.MyLabel5.Text = "Transaction"
        '
        'txtTransaction
        '
        Me.txtTransaction.arrDispalyMember = Nothing
        Me.txtTransaction.arrValueMember = Nothing
        Me.txtTransaction.Location = New System.Drawing.Point(69, 25)
        Me.txtTransaction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransaction.MyLinkLable1 = Me.MyLabel5
        Me.txtTransaction.MyLinkLable2 = Nothing
        Me.txtTransaction.MyNullText = "All"
        Me.txtTransaction.Name = "txtTransaction"
        Me.txtTransaction.Size = New System.Drawing.Size(325, 19)
        Me.txtTransaction.TabIndex = 3
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(0, 67)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel4.TabIndex = 28
        Me.MyLabel4.Text = "Item"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(69, 67)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Me.MyLabel4
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(325, 19)
        Me.txtItem.TabIndex = 8
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(397, 46)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel3.TabIndex = 25
        Me.MyLabel3.Text = "UOM"
        '
        'cmbUnit
        '
        Me.cmbUnit.AutoCompleteDisplayMember = Nothing
        Me.cmbUnit.AutoCompleteValueMember = Nothing
        Me.cmbUnit.CalculationExpression = Nothing
        Me.cmbUnit.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbUnit.FieldCode = Nothing
        Me.cmbUnit.FieldDesc = Nothing
        Me.cmbUnit.FieldMaxLength = 0
        Me.cmbUnit.FieldName = Nothing
        Me.cmbUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUnit.isCalculatedField = False
        Me.cmbUnit.IsSourceFromTable = False
        Me.cmbUnit.IsSourceFromValueList = False
        Me.cmbUnit.IsUnique = False
        Me.cmbUnit.Location = New System.Drawing.Point(481, 46)
        Me.cmbUnit.MendatroryField = False
        Me.cmbUnit.MyLinkLable1 = Me.MyLabel3
        Me.cmbUnit.MyLinkLable2 = Nothing
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.ReferenceFieldDesc = Nothing
        Me.cmbUnit.ReferenceFieldName = Nothing
        Me.cmbUnit.ReferenceTableName = Nothing
        Me.cmbUnit.Size = New System.Drawing.Size(141, 18)
        Me.cmbUnit.TabIndex = 7
        '
        'lblModeofTransport
        '
        Me.lblModeofTransport.FieldName = Nothing
        Me.lblModeofTransport.Location = New System.Drawing.Point(397, 4)
        Me.lblModeofTransport.Name = "lblModeofTransport"
        Me.lblModeofTransport.Size = New System.Drawing.Size(67, 18)
        Me.lblModeofTransport.TabIndex = 23
        Me.lblModeofTransport.Text = "Report Type"
        '
        'cboType
        '
        Me.cboType.AutoCompleteDisplayMember = Nothing
        Me.cboType.AutoCompleteValueMember = Nothing
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        RadListDataItem4.Text = "By Road"
        RadListDataItem5.Text = "By Air"
        RadListDataItem6.Text = "By Sea"
        Me.cboType.Items.Add(RadListDataItem4)
        Me.cboType.Items.Add(RadListDataItem5)
        Me.cboType.Items.Add(RadListDataItem6)
        Me.cboType.Location = New System.Drawing.Point(481, 4)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.lblModeofTransport
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(373, 18)
        Me.cboType.TabIndex = 2
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(0, 4)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 2
        Me.MyLabel2.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(69, 3)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(81, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "28/06/2012"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(202, 3)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel1
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(80, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "28/06/2012"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(152, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "To Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Controls.Add(Me.vsb)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(867, 356)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(850, 356)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'vsb
        '
        Me.vsb.Dock = System.Windows.Forms.DockStyle.Right
        Me.vsb.Location = New System.Drawing.Point(850, 0)
        Me.vsb.Name = "vsb"
        Me.vsb.Size = New System.Drawing.Size(17, 356)
        Me.vsb.TabIndex = 4
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvDetail)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(867, 356)
        Me.RadPageViewPage3.Text = "Detail"
        '
        'gvDetail
        '
        Me.gvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetail.Location = New System.Drawing.Point(0, 0)
        '
        'gvDetail
        '
        Me.gvDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDetail.Name = "gvDetail"
        Me.gvDetail.ShowHeaderCellButtons = True
        Me.gvDetail.Size = New System.Drawing.Size(867, 356)
        Me.gvDetail.TabIndex = 4
        Me.gvDetail.Text = "RadGridView1"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemSaveeLayout3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(888, 20)
        Me.RadMenu1.TabIndex = 22
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItemSaveeLayout3
        '
        Me.RadMenuItemSaveeLayout3.AccessibleDescription = "Setting"
        Me.RadMenuItemSaveeLayout3.AccessibleName = "Setting"
        Me.RadMenuItemSaveeLayout3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemSett1, Me.RadMenuItem2})
        Me.RadMenuItemSaveeLayout3.Name = "RadMenuItemSaveeLayout3"
        Me.RadMenuItemSaveeLayout3.Text = "Setting"
        '
        'RadMenuItemSett1
        '
        Me.RadMenuItemSett1.AccessibleDescription = "Save Layout"
        Me.RadMenuItemSett1.AccessibleName = "Save Layout"
        Me.RadMenuItemSett1.Name = "RadMenuItemSett1"
        Me.RadMenuItemSett1.Text = "Save Layout"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem2.AccessibleName = "Delete Layout"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Delete Layout"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Controls.Add(Me.btnQuickExport)
        Me.Panel1.Controls.Add(Me.chkPartiallyLoad)
        Me.Panel1.Controls.Add(Me.btnBack)
        Me.Panel1.Controls.Add(Me.RadButton3)
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 424)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(888, 30)
        Me.Panel1.TabIndex = 21
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(364, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(81, 24)
        Me.btnPrint.TabIndex = 142
        Me.btnPrint.Text = "Print"
        '
        'btnQuickExport
        '
        Me.btnQuickExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.QExpExcel, Me.QExpCSV, Me.PDF, Me.BulkExcel, Me.BulkCSV, Me.ExcelGrid, Me.PDFGrid})
        Me.btnQuickExport.Location = New System.Drawing.Point(260, 3)
        Me.btnQuickExport.Name = "btnQuickExport"
        Me.btnQuickExport.Size = New System.Drawing.Size(103, 24)
        Me.btnQuickExport.TabIndex = 23
        Me.btnQuickExport.Text = "Export"
        '
        'QExpExcel
        '
        Me.QExpExcel.AccessibleDescription = "Excel"
        Me.QExpExcel.AccessibleName = "Excel"
        Me.QExpExcel.Name = "QExpExcel"
        Me.QExpExcel.Text = "Excel"
        '
        'QExpCSV
        '
        Me.QExpCSV.AccessibleDescription = "CSV"
        Me.QExpCSV.AccessibleName = "CSV"
        Me.QExpCSV.Name = "QExpCSV"
        Me.QExpCSV.Text = "CSV"
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'BulkExcel
        '
        Me.BulkExcel.AccessibleDescription = "Bulk Excel"
        Me.BulkExcel.AccessibleName = "Bulk Excel"
        Me.BulkExcel.Name = "BulkExcel"
        Me.BulkExcel.Text = "Bulk Excel"
        '
        'BulkCSV
        '
        Me.BulkCSV.AccessibleDescription = "Bulk CSV"
        Me.BulkCSV.AccessibleName = "Bulk CSV"
        Me.BulkCSV.Name = "BulkCSV"
        Me.BulkCSV.Text = "Bulk CSV"
        '
        'ExcelGrid
        '
        Me.ExcelGrid.AccessibleDescription = "ExcelGrid"
        Me.ExcelGrid.AccessibleName = "ExcelGrid"
        Me.ExcelGrid.Name = "ExcelGrid"
        Me.ExcelGrid.Text = "Excel(Grid)"
        '
        'PDFGrid
        '
        Me.PDFGrid.AccessibleDescription = "PDFGrid"
        Me.PDFGrid.AccessibleName = "PDFGrid"
        Me.PDFGrid.Name = "PDFGrid"
        Me.PDFGrid.Text = "PDF(Grid)"
        '
        'chkPartiallyLoad
        '
        Me.chkPartiallyLoad.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPartiallyLoad.Location = New System.Drawing.Point(648, 6)
        Me.chkPartiallyLoad.Name = "chkPartiallyLoad"
        Me.chkPartiallyLoad.Size = New System.Drawing.Size(89, 16)
        Me.chkPartiallyLoad.TabIndex = 348
        Me.chkPartiallyLoad.Text = "Partially Load"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(164, 3)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(95, 24)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "<< Back "
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Location = New System.Drawing.Point(69, 3)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(95, 24)
        Me.RadButton3.TabIndex = 1
        Me.RadButton3.Text = "Reset"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(819, 3)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(67, 24)
        Me.RadButton2.TabIndex = 4
        Me.RadButton2.Text = "Close"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(5, 3)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(64, 24)
        Me.RadButton1.TabIndex = 0
        Me.RadButton1.Text = ">>>"
        '
        'FrmStockReco
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 454)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmStockReco"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Stock Reco"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.chkShowTransactionData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNoTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExcludeConsumptionLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFATSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFatSNFType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIncludeGIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProd_WIP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDisplayMethod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkMRPWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvLocation.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gvCategory.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboInOutType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnQuickExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPartiallyLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItemSaveeLayout3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemSett1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblModeofTransport As common.Controls.MyLabel
    Protected WithEvents txtToDate As common.Controls.MyDateTimePicker
    Protected WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Protected WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Protected WithEvents cmbUnit As common.Controls.MyComboBox
    Friend WithEvents ChkMRPWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents gvCategory As common.UserControls.MyRadGridView
    Friend WithEvents gvLocation As common.UserControls.MyRadGridView
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtTransaction As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtItemGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Protected WithEvents cboInOutType As common.Controls.MyComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadButton6 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton7 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadButton5 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkIncludeGIT As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtItemType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Protected WithEvents rbtnLocationSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnLocationAll As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Protected WithEvents cboDisplayMethod As common.Controls.MyComboBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDetail As common.UserControls.MyRadGridView
    Friend WithEvents chkProd_WIP As Telerik.WinControls.UI.RadCheckBox
    Protected WithEvents cboFatSNFType As common.Controls.MyComboBox
    Friend WithEvents chkPartiallyLoad As Telerik.WinControls.UI.RadCheckBox
    Private WithEvents vsb As System.Windows.Forms.VScrollBar
    Friend WithEvents btnQuickExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents QExpExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents QExpCSV As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BulkCSV As Telerik.WinControls.UI.RadMenuItem
    Protected WithEvents cboFATSNF As common.Controls.MyComboBox
    Friend WithEvents ExcelGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDFGrid As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkExcludeConsumptionLoc As RadCheckBox
    Friend WithEvents chkNoTransaction As RadCheckBox
    Friend WithEvents chkShowTransactionData As RadCheckBox
End Class

