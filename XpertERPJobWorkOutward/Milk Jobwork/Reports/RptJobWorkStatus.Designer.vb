<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptJobWorkStatus
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
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtMultiSelectLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtRecSnf = New System.Windows.Forms.TextBox()
        Me.txtRecFat = New System.Windows.Forms.TextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblRecoveryOfFat = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.lblModeofTransport = New common.Controls.MyLabel()
        Me.lblSRN = New common.Controls.MyLabel()
        Me.TxtMultiSRN = New common.UserControls.txtMultiSelectFinder()
        Me.lblVendor = New common.Controls.MyLabel()
        Me.TxtMultiVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblRGP = New common.Controls.MyLabel()
        Me.TxtMultiRGP = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvLocation = New common.UserControls.MyRadGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.rbtnLocationSelect = New common.Controls.MyRadioButton()
        Me.rbtnLocationAll = New common.Controls.MyRadioButton()
        Me.RadButton5 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnBack = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRecoveryOfFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRGP, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1137, 484)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiSelectLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtRecSnf)
        Me.RadPageViewPage1.Controls.Add(Me.txtRecFat)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.lblRecoveryOfFat)
        Me.RadPageViewPage1.Controls.Add(Me.cboType)
        Me.RadPageViewPage1.Controls.Add(Me.lblSRN)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiSRN)
        Me.RadPageViewPage1.Controls.Add(Me.lblVendor)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiVendor)
        Me.RadPageViewPage1.Controls.Add(Me.lblRGP)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiRGP)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.lblModeofTransport)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1116, 436)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'TxtMultiSelectLocation
        '
        Me.TxtMultiSelectLocation.arrDispalyMember = Nothing
        Me.TxtMultiSelectLocation.arrValueMember = Nothing
        Me.TxtMultiSelectLocation.Location = New System.Drawing.Point(72, 59)
        Me.TxtMultiSelectLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiSelectLocation.MyLinkLable1 = Nothing
        Me.TxtMultiSelectLocation.MyLinkLable2 = Nothing
        Me.TxtMultiSelectLocation.MyNullText = "All"
        Me.TxtMultiSelectLocation.Name = "TxtMultiSelectLocation"
        Me.TxtMultiSelectLocation.Size = New System.Drawing.Size(319, 19)
        Me.TxtMultiSelectLocation.TabIndex = 38
        Me.TxtMultiSelectLocation.Visible = False
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(3, 57)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 37
        Me.lblLocation.Text = "Location"
        Me.lblLocation.Visible = False
        '
        'txtRecSnf
        '
        Me.txtRecSnf.Location = New System.Drawing.Point(605, 8)
        Me.txtRecSnf.Name = "txtRecSnf"
        Me.txtRecSnf.Size = New System.Drawing.Size(117, 20)
        Me.txtRecSnf.TabIndex = 36
        Me.txtRecSnf.Text = "0"
        '
        'txtRecFat
        '
        Me.txtRecFat.Location = New System.Drawing.Point(404, 9)
        Me.txtRecFat.Name = "txtRecFat"
        Me.txtRecFat.Size = New System.Drawing.Size(94, 20)
        Me.txtRecFat.TabIndex = 35
        Me.txtRecFat.Text = "0"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(500, 9)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(102, 18)
        Me.MyLabel3.TabIndex = 34
        Me.MyLabel3.Text = "Recovery % Of SNF"
        '
        'lblRecoveryOfFat
        '
        Me.lblRecoveryOfFat.FieldName = Nothing
        Me.lblRecoveryOfFat.Location = New System.Drawing.Point(301, 11)
        Me.lblRecoveryOfFat.Name = "lblRecoveryOfFat"
        Me.lblRecoveryOfFat.Size = New System.Drawing.Size(97, 18)
        Me.lblRecoveryOfFat.TabIndex = 33
        Me.lblRecoveryOfFat.Text = "Recovery % Of Fat"
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
        RadListDataItem3.Text = "Vendor Wise"
        RadListDataItem4.Text = "Document and Item Wise"
        Me.cboType.Items.Add(RadListDataItem3)
        Me.cboType.Items.Add(RadListDataItem4)
        Me.cboType.Location = New System.Drawing.Point(478, 36)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.lblModeofTransport
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(244, 18)
        Me.cboType.TabIndex = 32
        Me.cboType.Visible = False
        '
        'lblModeofTransport
        '
        Me.lblModeofTransport.FieldName = Nothing
        Me.lblModeofTransport.Location = New System.Drawing.Point(400, 38)
        Me.lblModeofTransport.Name = "lblModeofTransport"
        Me.lblModeofTransport.Size = New System.Drawing.Size(67, 18)
        Me.lblModeofTransport.TabIndex = 24
        Me.lblModeofTransport.Text = "Report Type"
        Me.lblModeofTransport.Visible = False
        '
        'lblSRN
        '
        Me.lblSRN.FieldName = Nothing
        Me.lblSRN.Location = New System.Drawing.Point(4, 99)
        Me.lblSRN.Name = "lblSRN"
        Me.lblSRN.Size = New System.Drawing.Size(48, 18)
        Me.lblSRN.TabIndex = 31
        Me.lblSRN.Text = "SRN No."
        Me.lblSRN.Visible = False
        '
        'TxtMultiSRN
        '
        Me.TxtMultiSRN.arrDispalyMember = Nothing
        Me.TxtMultiSRN.arrValueMember = Nothing
        Me.TxtMultiSRN.Location = New System.Drawing.Point(73, 100)
        Me.TxtMultiSRN.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiSRN.MyLinkLable1 = Nothing
        Me.TxtMultiSRN.MyLinkLable2 = Nothing
        Me.TxtMultiSRN.MyNullText = "All"
        Me.TxtMultiSRN.Name = "TxtMultiSRN"
        Me.TxtMultiSRN.Size = New System.Drawing.Size(319, 19)
        Me.TxtMultiSRN.TabIndex = 30
        Me.TxtMultiSRN.Visible = False
        '
        'lblVendor
        '
        Me.lblVendor.FieldName = Nothing
        Me.lblVendor.Location = New System.Drawing.Point(3, 36)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(43, 18)
        Me.lblVendor.TabIndex = 29
        Me.lblVendor.Text = "Vendor"
        '
        'TxtMultiVendor
        '
        Me.TxtMultiVendor.arrDispalyMember = Nothing
        Me.TxtMultiVendor.arrValueMember = Nothing
        Me.TxtMultiVendor.Location = New System.Drawing.Point(72, 37)
        Me.TxtMultiVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiVendor.MyLinkLable1 = Nothing
        Me.TxtMultiVendor.MyLinkLable2 = Nothing
        Me.TxtMultiVendor.MyNullText = "All"
        Me.TxtMultiVendor.Name = "TxtMultiVendor"
        Me.TxtMultiVendor.Size = New System.Drawing.Size(319, 19)
        Me.TxtMultiVendor.TabIndex = 28
        '
        'lblRGP
        '
        Me.lblRGP.FieldName = Nothing
        Me.lblRGP.Location = New System.Drawing.Point(4, 81)
        Me.lblRGP.Name = "lblRGP"
        Me.lblRGP.Size = New System.Drawing.Size(48, 18)
        Me.lblRGP.TabIndex = 27
        Me.lblRGP.Text = "RGP No."
        Me.lblRGP.Visible = False
        '
        'TxtMultiRGP
        '
        Me.TxtMultiRGP.arrDispalyMember = Nothing
        Me.TxtMultiRGP.arrValueMember = Nothing
        Me.TxtMultiRGP.Location = New System.Drawing.Point(73, 81)
        Me.TxtMultiRGP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiRGP.MyLinkLable1 = Nothing
        Me.TxtMultiRGP.MyLinkLable2 = Nothing
        Me.TxtMultiRGP.MyNullText = "All"
        Me.TxtMultiRGP.Name = "TxtMultiRGP"
        Me.TxtMultiRGP.Size = New System.Drawing.Size(319, 19)
        Me.TxtMultiRGP.TabIndex = 26
        Me.TxtMultiRGP.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gvLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(8, 125)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(388, 308)
        Me.RadGroupBox2.TabIndex = 25
        Me.RadGroupBox2.Text = "Location"
        Me.RadGroupBox2.Visible = False
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
        Me.gvLocation.Size = New System.Drawing.Size(368, 258)
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
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 12)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 6
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
        Me.txtFromDate.Location = New System.Drawing.Point(72, 11)
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
        Me.txtFromDate.TabIndex = 3
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
        Me.txtToDate.Location = New System.Drawing.Point(205, 11)
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
        Me.txtToDate.TabIndex = 5
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "28/06/2012"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(155, 12)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 4
        Me.MyLabel1.Text = "To Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1116, 436)
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
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1116, 436)
        Me.gv.TabIndex = 4
        Me.gv.Text = "gv"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnBack)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer2.Size = New System.Drawing.Size(1137, 533)
        Me.SplitContainer2.SplitterDistance = 484
        Me.SplitContainer2.TabIndex = 1
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(1058, 9)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(67, 24)
        Me.RadButton2.TabIndex = 147
        Me.RadButton2.Text = "Close"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(309, 9)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(95, 24)
        Me.btnBack.TabIndex = 144
        Me.btnBack.Text = "<< Back "
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(109, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(95, 24)
        Me.btnreset.TabIndex = 143
        Me.btnreset.Text = "Reset"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(210, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 24)
        Me.btnExport.TabIndex = 145
        Me.btnExport.Text = "Export"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(12, 9)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(95, 24)
        Me.btnRefresh.TabIndex = 142
        Me.btnRefresh.Text = ">>>"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'RptJobWorkStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 533)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "RptJobWorkStatus"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptJobWorkStatus"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRecoveryOfFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRGP, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Protected WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Protected WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblModeofTransport As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvLocation As common.UserControls.MyRadGridView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Protected WithEvents rbtnLocationSelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadButton5 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblSRN As common.Controls.MyLabel
    Friend WithEvents TxtMultiSRN As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblVendor As common.Controls.MyLabel
    Friend WithEvents TxtMultiVendor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblRGP As common.Controls.MyLabel
    Friend WithEvents TxtMultiRGP As common.UserControls.txtMultiSelectFinder
    Protected WithEvents cboType As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblRecoveryOfFat As common.Controls.MyLabel
    Friend WithEvents txtRecSnf As System.Windows.Forms.TextBox
    Friend WithEvents txtRecFat As System.Windows.Forms.TextBox
    Friend WithEvents btnBack As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents TxtMultiSelectLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
End Class

