<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptCustItemWiseSaleReport
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
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate1 = New common.Controls.MyDateTimePicker()
        Me.txtToShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.LblToDate = New common.Controls.MyLabel()
        Me.txtFromShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.txtFromDate1 = New common.Controls.MyDateTimePicker()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.ddlDefaultReportUOM = New common.Controls.MyComboBox()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblQtyConv = New common.Controls.MyLabel()
        Me.ddlQtyConversionType = New common.Controls.MyComboBox()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDocumentDate = New common.Controls.MyRadioButton()
        Me.rbtnSupplyDate = New common.Controls.MyRadioButton()
        Me.BKNGroupBox = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBoothSaleItemWise = New common.Controls.MyRadioButton()
        Me.rbtnBoothSaleDateShiftWise = New common.Controls.MyRadioButton()
        Me.rbtnMilkSale = New common.Controls.MyRadioButton()
        Me.rbtnStockStatement = New common.Controls.MyRadioButton()
        Me.rbtnDistributorCollStatement = New common.Controls.MyRadioButton()
        Me.BtnCreditPartyWiseSaleAmount = New common.Controls.MyRadioButton()
        Me.BtnRouteWiseSale = New common.Controls.MyRadioButton()
        Me.BtnGheeReport = New common.Controls.MyRadioButton()
        Me.BtnTcsSummary = New common.Controls.MyRadioButton()
        Me.BtnTransportationCharges = New common.Controls.MyRadioButton()
        Me.BtnBillWiseSaleOfMilkSummary = New common.Controls.MyRadioButton()
        Me.BtnProductSalesSummaryTaxable = New common.Controls.MyRadioButton()
        Me.BtnMilkStcSummary = New common.Controls.MyRadioButton()
        Me.BtnStcRegisterItemWiseSummary = New common.Controls.MyRadioButton()
        Me.BtnBillWiseSaleOfMilk = New common.Controls.MyRadioButton()
        Me.BtnPartySaleMilkProductDispatch = New common.Controls.MyRadioButton()
        Me.BtnProductWiseSaleQuantity = New common.Controls.MyRadioButton()
        Me.BtnStcRegisterPartyandItemWiseSummary = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDetail = New common.Controls.MyRadioButton()
        Me.rbtnSummary = New common.Controls.MyRadioButton()
        Me.txtTransaction = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtItem = New common.UserControls.txtMultiSelectFinder()
        Me.lblItem = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.MyDateTimePicker1 = New common.Controls.MyDateTimePicker()
        Me.BtnPartySaleMilkProductInvoice = New common.Controls.MyRadioButton()
        Me.BtnProductSalesSummaryNonTaxable = New common.Controls.MyRadioButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.txtToDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlDefaultReportUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblQtyConv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlQtyConversionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rbtnDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSupplyDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BKNGroupBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BKNGroupBox.SuspendLayout()
        CType(Me.rbtnBoothSaleItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBoothSaleDateShiftWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMilkSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnStockStatement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDistributorCollStatement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnCreditPartyWiseSaleAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnRouteWiseSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnGheeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnTcsSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnTransportationCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnBillWiseSaleOfMilkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnProductSalesSummaryTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnMilkStcSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnStcRegisterItemWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnBillWiseSaleOfMilk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPartySaleMilkProductDispatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnProductWiseSaleQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnStcRegisterPartyandItemWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPartySaleMilkProductInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnProductSalesSummaryNonTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(855, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
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
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(855, 474)
        Me.SplitContainer1.SplitterDistance = 424
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(855, 424)
        Me.RadPageView1.TabIndex = 4
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(834, 376)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(834, 376)
        Me.RadPanel1.TabIndex = 15
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.ddlDefaultReportUOM)
        Me.RadGroupBox1.Controls.Add(Me.txtRoute)
        Me.RadGroupBox1.Controls.Add(Me.lblRoute)
        Me.RadGroupBox1.Controls.Add(Me.txtLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblQtyConv)
        Me.RadGroupBox1.Controls.Add(Me.ddlQtyConversionType)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.BKNGroupBox)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtTransaction)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtItem)
        Me.RadGroupBox1.Controls.Add(Me.lblItem)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(801, 305)
        Me.RadGroupBox1.TabIndex = 389
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.txtToDate1)
        Me.RadGroupBox4.Controls.Add(Me.txtToShift)
        Me.RadGroupBox4.Controls.Add(Me.LblToDate)
        Me.RadGroupBox4.Controls.Add(Me.txtFromShift)
        Me.RadGroupBox4.Controls.Add(Me.txtFromDate1)
        Me.RadGroupBox4.Controls.Add(Me.lblfromDate)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(404, 29)
        Me.RadGroupBox4.TabIndex = 456
        '
        'txtToDate1
        '
        Me.txtToDate1.CalculationExpression = Nothing
        Me.txtToDate1.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate1.FieldCode = Nothing
        Me.txtToDate1.FieldDesc = Nothing
        Me.txtToDate1.FieldMaxLength = 0
        Me.txtToDate1.FieldName = Nothing
        Me.txtToDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate1.isCalculatedField = False
        Me.txtToDate1.IsSourceFromTable = False
        Me.txtToDate1.IsSourceFromValueList = False
        Me.txtToDate1.IsUnique = False
        Me.txtToDate1.Location = New System.Drawing.Point(263, 7)
        Me.txtToDate1.MendatroryField = False
        Me.txtToDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate1.MyLinkLable1 = Nothing
        Me.txtToDate1.MyLinkLable2 = Nothing
        Me.txtToDate1.Name = "txtToDate1"
        Me.txtToDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate1.ReferenceFieldDesc = Nothing
        Me.txtToDate1.ReferenceFieldName = Nothing
        Me.txtToDate1.ReferenceTableName = Nothing
        Me.txtToDate1.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate1.TabIndex = 337
        Me.txtToDate1.TabStop = False
        Me.txtToDate1.Text = "17-12-2011"
        Me.txtToDate1.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'txtToShift
        '
        Me.txtToShift.AutoCompleteDisplayMember = Nothing
        Me.txtToShift.AutoCompleteValueMember = Nothing
        Me.txtToShift.DropDownAnimationEnabled = True
        Me.txtToShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.txtToShift.Location = New System.Drawing.Point(347, 7)
        Me.txtToShift.Name = "txtToShift"
        Me.txtToShift.Size = New System.Drawing.Size(52, 20)
        Me.txtToShift.TabIndex = 336
        '
        'LblToDate
        '
        Me.LblToDate.FieldName = Nothing
        Me.LblToDate.Location = New System.Drawing.Point(212, 8)
        Me.LblToDate.Name = "LblToDate"
        Me.LblToDate.Size = New System.Drawing.Size(45, 18)
        Me.LblToDate.TabIndex = 335
        Me.LblToDate.Text = "To Date"
        '
        'txtFromShift
        '
        Me.txtFromShift.AutoCompleteDisplayMember = Nothing
        Me.txtFromShift.AutoCompleteValueMember = Nothing
        Me.txtFromShift.DropDownAnimationEnabled = True
        Me.txtFromShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.txtFromShift.Location = New System.Drawing.Point(154, 5)
        Me.txtFromShift.Name = "txtFromShift"
        Me.txtFromShift.Size = New System.Drawing.Size(52, 20)
        Me.txtFromShift.TabIndex = 330
        '
        'txtFromDate1
        '
        Me.txtFromDate1.CalculationExpression = Nothing
        Me.txtFromDate1.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate1.FieldCode = Nothing
        Me.txtFromDate1.FieldDesc = Nothing
        Me.txtFromDate1.FieldMaxLength = 0
        Me.txtFromDate1.FieldName = Nothing
        Me.txtFromDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate1.isCalculatedField = False
        Me.txtFromDate1.IsSourceFromTable = False
        Me.txtFromDate1.IsSourceFromValueList = False
        Me.txtFromDate1.IsUnique = False
        Me.txtFromDate1.Location = New System.Drawing.Point(67, 5)
        Me.txtFromDate1.MendatroryField = False
        Me.txtFromDate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate1.MyLinkLable1 = Nothing
        Me.txtFromDate1.MyLinkLable2 = Nothing
        Me.txtFromDate1.Name = "txtFromDate1"
        Me.txtFromDate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate1.ReferenceFieldDesc = Nothing
        Me.txtFromDate1.ReferenceFieldName = Nothing
        Me.txtFromDate1.ReferenceTableName = Nothing
        Me.txtFromDate1.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate1.TabIndex = 328
        Me.txtFromDate1.TabStop = False
        Me.txtFromDate1.Text = "17-12-2011"
        Me.txtFromDate1.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(1, 6)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 329
        Me.lblfromDate.Text = "From Date"
        '
        'ddlDefaultReportUOM
        '
        Me.ddlDefaultReportUOM.AutoCompleteDisplayMember = Nothing
        Me.ddlDefaultReportUOM.AutoCompleteValueMember = Nothing
        Me.ddlDefaultReportUOM.CalculationExpression = Nothing
        Me.ddlDefaultReportUOM.DropDownAnimationEnabled = True
        Me.ddlDefaultReportUOM.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlDefaultReportUOM.FieldCode = Nothing
        Me.ddlDefaultReportUOM.FieldDesc = Nothing
        Me.ddlDefaultReportUOM.FieldMaxLength = 0
        Me.ddlDefaultReportUOM.FieldName = Nothing
        Me.ddlDefaultReportUOM.isCalculatedField = False
        Me.ddlDefaultReportUOM.IsSourceFromTable = False
        Me.ddlDefaultReportUOM.IsSourceFromValueList = False
        Me.ddlDefaultReportUOM.IsUnique = False
        Me.ddlDefaultReportUOM.Location = New System.Drawing.Point(529, 9)
        Me.ddlDefaultReportUOM.MendatroryField = True
        Me.ddlDefaultReportUOM.MyLinkLable1 = Nothing
        Me.ddlDefaultReportUOM.MyLinkLable2 = Nothing
        Me.ddlDefaultReportUOM.Name = "ddlDefaultReportUOM"
        Me.ddlDefaultReportUOM.ReferenceFieldDesc = Nothing
        Me.ddlDefaultReportUOM.ReferenceFieldName = Nothing
        Me.ddlDefaultReportUOM.ReferenceTableName = Nothing
        Me.ddlDefaultReportUOM.Size = New System.Drawing.Size(107, 20)
        Me.ddlDefaultReportUOM.TabIndex = 455
        Me.ddlDefaultReportUOM.Visible = False
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(94, 132)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(243, 19)
        Me.txtRoute.TabIndex = 454
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(5, 132)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(36, 18)
        Me.lblRoute.TabIndex = 453
        Me.lblRoute.Text = "Route"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(94, 109)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(243, 19)
        Me.txtLocation.TabIndex = 452
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(5, 109)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 451
        Me.lblLocation.Text = "Location"
        '
        'lblQtyConv
        '
        Me.lblQtyConv.FieldName = Nothing
        Me.lblQtyConv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQtyConv.Location = New System.Drawing.Point(441, 10)
        Me.lblQtyConv.Name = "lblQtyConv"
        Me.lblQtyConv.Size = New System.Drawing.Size(83, 18)
        Me.lblQtyConv.TabIndex = 450
        Me.lblQtyConv.Text = "Qty Conversion"
        Me.lblQtyConv.Visible = False
        '
        'ddlQtyConversionType
        '
        Me.ddlQtyConversionType.AutoCompleteDisplayMember = Nothing
        Me.ddlQtyConversionType.AutoCompleteValueMember = Nothing
        Me.ddlQtyConversionType.CalculationExpression = Nothing
        Me.ddlQtyConversionType.DropDownAnimationEnabled = True
        Me.ddlQtyConversionType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlQtyConversionType.FieldCode = Nothing
        Me.ddlQtyConversionType.FieldDesc = Nothing
        Me.ddlQtyConversionType.FieldMaxLength = 0
        Me.ddlQtyConversionType.FieldName = Nothing
        Me.ddlQtyConversionType.isCalculatedField = False
        Me.ddlQtyConversionType.IsSourceFromTable = False
        Me.ddlQtyConversionType.IsSourceFromValueList = False
        Me.ddlQtyConversionType.IsUnique = False
        Me.ddlQtyConversionType.Location = New System.Drawing.Point(529, 9)
        Me.ddlQtyConversionType.MendatroryField = True
        Me.ddlQtyConversionType.MyLinkLable1 = Nothing
        Me.ddlQtyConversionType.MyLinkLable2 = Nothing
        Me.ddlQtyConversionType.Name = "ddlQtyConversionType"
        Me.ddlQtyConversionType.ReferenceFieldDesc = Nothing
        Me.ddlQtyConversionType.ReferenceFieldName = Nothing
        Me.ddlQtyConversionType.ReferenceTableName = Nothing
        Me.ddlQtyConversionType.Size = New System.Drawing.Size(107, 20)
        Me.ddlQtyConversionType.TabIndex = 449
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rbtnDocumentDate)
        Me.RadGroupBox3.Controls.Add(Me.rbtnSupplyDate)
        Me.RadGroupBox3.HeaderText = "Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(8, 198)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(331, 37)
        Me.RadGroupBox3.TabIndex = 448
        Me.RadGroupBox3.Text = "Date"
        '
        'rbtnDocumentDate
        '
        Me.rbtnDocumentDate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnDocumentDate.Location = New System.Drawing.Point(5, 9)
        Me.rbtnDocumentDate.MyLinkLable1 = Nothing
        Me.rbtnDocumentDate.MyLinkLable2 = Nothing
        Me.rbtnDocumentDate.Name = "rbtnDocumentDate"
        Me.rbtnDocumentDate.Size = New System.Drawing.Size(94, 18)
        Me.rbtnDocumentDate.TabIndex = 396
        Me.rbtnDocumentDate.Text = "Doument Date"
        Me.rbtnDocumentDate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnSupplyDate
        '
        Me.rbtnSupplyDate.Location = New System.Drawing.Point(118, 9)
        Me.rbtnSupplyDate.MyLinkLable1 = Nothing
        Me.rbtnSupplyDate.MyLinkLable2 = Nothing
        Me.rbtnSupplyDate.Name = "rbtnSupplyDate"
        Me.rbtnSupplyDate.Size = New System.Drawing.Size(81, 18)
        Me.rbtnSupplyDate.TabIndex = 391
        Me.rbtnSupplyDate.TabStop = False
        Me.rbtnSupplyDate.Text = "Supply Date"
        '
        'BKNGroupBox
        '
        Me.BKNGroupBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.BKNGroupBox.Controls.Add(Me.BtnProductSalesSummaryNonTaxable)
        Me.BKNGroupBox.Controls.Add(Me.BtnPartySaleMilkProductInvoice)
        Me.BKNGroupBox.Controls.Add(Me.rbtnBoothSaleItemWise)
        Me.BKNGroupBox.Controls.Add(Me.rbtnBoothSaleDateShiftWise)
        Me.BKNGroupBox.Controls.Add(Me.rbtnMilkSale)
        Me.BKNGroupBox.Controls.Add(Me.rbtnStockStatement)
        Me.BKNGroupBox.Controls.Add(Me.rbtnDistributorCollStatement)
        Me.BKNGroupBox.Controls.Add(Me.BtnCreditPartyWiseSaleAmount)
        Me.BKNGroupBox.Controls.Add(Me.BtnRouteWiseSale)
        Me.BKNGroupBox.Controls.Add(Me.BtnGheeReport)
        Me.BKNGroupBox.Controls.Add(Me.BtnTcsSummary)
        Me.BKNGroupBox.Controls.Add(Me.BtnTransportationCharges)
        Me.BKNGroupBox.Controls.Add(Me.BtnBillWiseSaleOfMilkSummary)
        Me.BKNGroupBox.Controls.Add(Me.BtnProductSalesSummaryTaxable)
        Me.BKNGroupBox.Controls.Add(Me.BtnMilkStcSummary)
        Me.BKNGroupBox.Controls.Add(Me.BtnStcRegisterItemWiseSummary)
        Me.BKNGroupBox.Controls.Add(Me.BtnBillWiseSaleOfMilk)
        Me.BKNGroupBox.Controls.Add(Me.BtnPartySaleMilkProductDispatch)
        Me.BKNGroupBox.Controls.Add(Me.BtnProductWiseSaleQuantity)
        Me.BKNGroupBox.Controls.Add(Me.BtnStcRegisterPartyandItemWiseSummary)
        Me.BKNGroupBox.HeaderText = ""
        Me.BKNGroupBox.Location = New System.Drawing.Point(344, 33)
        Me.BKNGroupBox.Name = "BKNGroupBox"
        Me.BKNGroupBox.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.BKNGroupBox.Size = New System.Drawing.Size(442, 267)
        Me.BKNGroupBox.TabIndex = 447
        Me.BKNGroupBox.Visible = False
        '
        'rbtnBoothSaleItemWise
        '
        Me.rbtnBoothSaleItemWise.Location = New System.Drawing.Point(5, 220)
        Me.rbtnBoothSaleItemWise.MyLinkLable1 = Nothing
        Me.rbtnBoothSaleItemWise.MyLinkLable2 = Nothing
        Me.rbtnBoothSaleItemWise.Name = "rbtnBoothSaleItemWise"
        Me.rbtnBoothSaleItemWise.Size = New System.Drawing.Size(127, 18)
        Me.rbtnBoothSaleItemWise.TabIndex = 417
        Me.rbtnBoothSaleItemWise.TabStop = False
        Me.rbtnBoothSaleItemWise.Text = "Booth Sale Item Wise"
        '
        'rbtnBoothSaleDateShiftWise
        '
        Me.rbtnBoothSaleDateShiftWise.Location = New System.Drawing.Point(5, 197)
        Me.rbtnBoothSaleDateShiftWise.MyLinkLable1 = Nothing
        Me.rbtnBoothSaleDateShiftWise.MyLinkLable2 = Nothing
        Me.rbtnBoothSaleDateShiftWise.Name = "rbtnBoothSaleDateShiftWise"
        Me.rbtnBoothSaleDateShiftWise.Size = New System.Drawing.Size(165, 18)
        Me.rbtnBoothSaleDateShiftWise.TabIndex = 416
        Me.rbtnBoothSaleDateShiftWise.TabStop = False
        Me.rbtnBoothSaleDateShiftWise.Text = "Booth Sale Date && Shift Wise"
        '
        'rbtnMilkSale
        '
        Me.rbtnMilkSale.Location = New System.Drawing.Point(233, 219)
        Me.rbtnMilkSale.MyLinkLable1 = Nothing
        Me.rbtnMilkSale.MyLinkLable2 = Nothing
        Me.rbtnMilkSale.Name = "rbtnMilkSale"
        Me.rbtnMilkSale.Size = New System.Drawing.Size(151, 18)
        Me.rbtnMilkSale.TabIndex = 413
        Me.rbtnMilkSale.TabStop = False
        Me.rbtnMilkSale.Text = "Milk Sale As Per Gate Pass"
        '
        'rbtnStockStatement
        '
        Me.rbtnStockStatement.Location = New System.Drawing.Point(5, 174)
        Me.rbtnStockStatement.MyLinkLable1 = Nothing
        Me.rbtnStockStatement.MyLinkLable2 = Nothing
        Me.rbtnStockStatement.Name = "rbtnStockStatement"
        Me.rbtnStockStatement.Size = New System.Drawing.Size(102, 18)
        Me.rbtnStockStatement.TabIndex = 411
        Me.rbtnStockStatement.TabStop = False
        Me.rbtnStockStatement.Text = "Stock Statement"
        '
        'rbtnDistributorCollStatement
        '
        Me.rbtnDistributorCollStatement.Location = New System.Drawing.Point(233, 196)
        Me.rbtnDistributorCollStatement.MyLinkLable1 = Nothing
        Me.rbtnDistributorCollStatement.MyLinkLable2 = Nothing
        Me.rbtnDistributorCollStatement.Name = "rbtnDistributorCollStatement"
        Me.rbtnDistributorCollStatement.Size = New System.Drawing.Size(184, 18)
        Me.rbtnDistributorCollStatement.TabIndex = 410
        Me.rbtnDistributorCollStatement.TabStop = False
        Me.rbtnDistributorCollStatement.Text = " Distributor Collection Statement"
        '
        'BtnCreditPartyWiseSaleAmount
        '
        Me.BtnCreditPartyWiseSaleAmount.Location = New System.Drawing.Point(233, 173)
        Me.BtnCreditPartyWiseSaleAmount.MyLinkLable1 = Nothing
        Me.BtnCreditPartyWiseSaleAmount.MyLinkLable2 = Nothing
        Me.BtnCreditPartyWiseSaleAmount.Name = "BtnCreditPartyWiseSaleAmount"
        Me.BtnCreditPartyWiseSaleAmount.Size = New System.Drawing.Size(173, 18)
        Me.BtnCreditPartyWiseSaleAmount.TabIndex = 409
        Me.BtnCreditPartyWiseSaleAmount.TabStop = False
        Me.BtnCreditPartyWiseSaleAmount.Text = "Credit Party Wise Sale Amount"
        '
        'BtnRouteWiseSale
        '
        Me.BtnRouteWiseSale.Location = New System.Drawing.Point(5, 126)
        Me.BtnRouteWiseSale.MyLinkLable1 = Nothing
        Me.BtnRouteWiseSale.MyLinkLable2 = Nothing
        Me.BtnRouteWiseSale.Name = "BtnRouteWiseSale"
        Me.BtnRouteWiseSale.Size = New System.Drawing.Size(100, 18)
        Me.BtnRouteWiseSale.TabIndex = 408
        Me.BtnRouteWiseSale.TabStop = False
        Me.BtnRouteWiseSale.Text = "Route Wise Sale"
        '
        'BtnGheeReport
        '
        Me.BtnGheeReport.Location = New System.Drawing.Point(5, 151)
        Me.BtnGheeReport.MyLinkLable1 = Nothing
        Me.BtnGheeReport.MyLinkLable2 = Nothing
        Me.BtnGheeReport.Name = "BtnGheeReport"
        Me.BtnGheeReport.Size = New System.Drawing.Size(83, 18)
        Me.BtnGheeReport.TabIndex = 407
        Me.BtnGheeReport.TabStop = False
        Me.BtnGheeReport.Text = "Ghee Report"
        '
        'BtnTcsSummary
        '
        Me.BtnTcsSummary.Location = New System.Drawing.Point(233, 148)
        Me.BtnTcsSummary.MyLinkLable1 = Nothing
        Me.BtnTcsSummary.MyLinkLable2 = Nothing
        Me.BtnTcsSummary.Name = "BtnTcsSummary"
        Me.BtnTcsSummary.Size = New System.Drawing.Size(89, 18)
        Me.BtnTcsSummary.TabIndex = 406
        Me.BtnTcsSummary.TabStop = False
        Me.BtnTcsSummary.Text = "Tcs Summary "
        '
        'BtnTransportationCharges
        '
        Me.BtnTransportationCharges.Location = New System.Drawing.Point(5, 101)
        Me.BtnTransportationCharges.MyLinkLable1 = Nothing
        Me.BtnTransportationCharges.MyLinkLable2 = Nothing
        Me.BtnTransportationCharges.Name = "BtnTransportationCharges"
        Me.BtnTransportationCharges.Size = New System.Drawing.Size(137, 18)
        Me.BtnTransportationCharges.TabIndex = 405
        Me.BtnTransportationCharges.TabStop = False
        Me.BtnTransportationCharges.Text = "Transportation Charges"
        '
        'BtnBillWiseSaleOfMilkSummary
        '
        Me.BtnBillWiseSaleOfMilkSummary.Location = New System.Drawing.Point(233, 123)
        Me.BtnBillWiseSaleOfMilkSummary.MyLinkLable1 = Nothing
        Me.BtnBillWiseSaleOfMilkSummary.MyLinkLable2 = Nothing
        Me.BtnBillWiseSaleOfMilkSummary.Name = "BtnBillWiseSaleOfMilkSummary"
        Me.BtnBillWiseSaleOfMilkSummary.Size = New System.Drawing.Size(175, 18)
        Me.BtnBillWiseSaleOfMilkSummary.TabIndex = 403
        Me.BtnBillWiseSaleOfMilkSummary.TabStop = False
        Me.BtnBillWiseSaleOfMilkSummary.Text = "Bill Wise Sale Of Milk Summary"
        '
        'BtnProductSalesSummaryTaxable
        '
        Me.BtnProductSalesSummaryTaxable.Location = New System.Drawing.Point(5, 76)
        Me.BtnProductSalesSummaryTaxable.MyLinkLable1 = Nothing
        Me.BtnProductSalesSummaryTaxable.MyLinkLable2 = Nothing
        Me.BtnProductSalesSummaryTaxable.Name = "BtnProductSalesSummaryTaxable"
        Me.BtnProductSalesSummaryTaxable.Size = New System.Drawing.Size(186, 18)
        Me.BtnProductSalesSummaryTaxable.TabIndex = 404
        Me.BtnProductSalesSummaryTaxable.TabStop = False
        Me.BtnProductSalesSummaryTaxable.Text = "Product Sales Summary (Taxable)"
        '
        'BtnMilkStcSummary
        '
        Me.BtnMilkStcSummary.Location = New System.Drawing.Point(233, 28)
        Me.BtnMilkStcSummary.MyLinkLable1 = Nothing
        Me.BtnMilkStcSummary.MyLinkLable2 = Nothing
        Me.BtnMilkStcSummary.Name = "BtnMilkStcSummary"
        Me.BtnMilkStcSummary.Size = New System.Drawing.Size(110, 18)
        Me.BtnMilkStcSummary.TabIndex = 402
        Me.BtnMilkStcSummary.TabStop = False
        Me.BtnMilkStcSummary.Text = "Milk Stc Summary"
        '
        'BtnStcRegisterItemWiseSummary
        '
        Me.BtnStcRegisterItemWiseSummary.Location = New System.Drawing.Point(233, 4)
        Me.BtnStcRegisterItemWiseSummary.MyLinkLable1 = Nothing
        Me.BtnStcRegisterItemWiseSummary.MyLinkLable2 = Nothing
        Me.BtnStcRegisterItemWiseSummary.Name = "BtnStcRegisterItemWiseSummary"
        Me.BtnStcRegisterItemWiseSummary.Size = New System.Drawing.Size(182, 18)
        Me.BtnStcRegisterItemWiseSummary.TabIndex = 401
        Me.BtnStcRegisterItemWiseSummary.TabStop = False
        Me.BtnStcRegisterItemWiseSummary.Text = "Stc Register Item Wise Summary"
        '
        'BtnBillWiseSaleOfMilk
        '
        Me.BtnBillWiseSaleOfMilk.Location = New System.Drawing.Point(233, 98)
        Me.BtnBillWiseSaleOfMilk.MyLinkLable1 = Nothing
        Me.BtnBillWiseSaleOfMilk.MyLinkLable2 = Nothing
        Me.BtnBillWiseSaleOfMilk.Name = "BtnBillWiseSaleOfMilk"
        Me.BtnBillWiseSaleOfMilk.Size = New System.Drawing.Size(125, 18)
        Me.BtnBillWiseSaleOfMilk.TabIndex = 399
        Me.BtnBillWiseSaleOfMilk.TabStop = False
        Me.BtnBillWiseSaleOfMilk.Text = "Bill Wise Sale Of Milk"
        '
        'BtnPartySaleMilkProductDispatch
        '
        Me.BtnPartySaleMilkProductDispatch.Location = New System.Drawing.Point(5, 52)
        Me.BtnPartySaleMilkProductDispatch.MyLinkLable1 = Nothing
        Me.BtnPartySaleMilkProductDispatch.MyLinkLable2 = Nothing
        Me.BtnPartySaleMilkProductDispatch.Name = "BtnPartySaleMilkProductDispatch"
        Me.BtnPartySaleMilkProductDispatch.Size = New System.Drawing.Size(211, 18)
        Me.BtnPartySaleMilkProductDispatch.TabIndex = 398
        Me.BtnPartySaleMilkProductDispatch.TabStop = False
        Me.BtnPartySaleMilkProductDispatch.Text = "Party Sale Milk and Product (Dispatch)"
        '
        'BtnProductWiseSaleQuantity
        '
        Me.BtnProductWiseSaleQuantity.Location = New System.Drawing.Point(5, 28)
        Me.BtnProductWiseSaleQuantity.MyLinkLable1 = Nothing
        Me.BtnProductWiseSaleQuantity.MyLinkLable2 = Nothing
        Me.BtnProductWiseSaleQuantity.Name = "BtnProductWiseSaleQuantity"
        Me.BtnProductWiseSaleQuantity.Size = New System.Drawing.Size(156, 18)
        Me.BtnProductWiseSaleQuantity.TabIndex = 397
        Me.BtnProductWiseSaleQuantity.TabStop = False
        Me.BtnProductWiseSaleQuantity.Text = "Product Wise Sale Quantity"
        '
        'BtnStcRegisterPartyandItemWiseSummary
        '
        Me.BtnStcRegisterPartyandItemWiseSummary.Location = New System.Drawing.Point(5, 4)
        Me.BtnStcRegisterPartyandItemWiseSummary.MyLinkLable1 = Nothing
        Me.BtnStcRegisterPartyandItemWiseSummary.MyLinkLable2 = Nothing
        Me.BtnStcRegisterPartyandItemWiseSummary.Name = "BtnStcRegisterPartyandItemWiseSummary"
        Me.BtnStcRegisterPartyandItemWiseSummary.Size = New System.Drawing.Size(223, 18)
        Me.BtnStcRegisterPartyandItemWiseSummary.TabIndex = 396
        Me.BtnStcRegisterPartyandItemWiseSummary.TabStop = False
        Me.BtnStcRegisterPartyandItemWiseSummary.Text = "Stc Register Party && Item Wise Summary"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnDetail)
        Me.RadGroupBox2.Controls.Add(Me.rbtnSummary)
        Me.RadGroupBox2.HeaderText = "Report Type"
        Me.RadGroupBox2.Location = New System.Drawing.Point(8, 158)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(331, 37)
        Me.RadGroupBox2.TabIndex = 441
        Me.RadGroupBox2.Text = "Report Type"
        '
        'rbtnDetail
        '
        Me.rbtnDetail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnDetail.Location = New System.Drawing.Point(5, 9)
        Me.rbtnDetail.MyLinkLable1 = Nothing
        Me.rbtnDetail.MyLinkLable2 = Nothing
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 396
        Me.rbtnDetail.Text = "Detail"
        Me.rbtnDetail.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnSummary
        '
        Me.rbtnSummary.Location = New System.Drawing.Point(119, 9)
        Me.rbtnSummary.MyLinkLable1 = Nothing
        Me.rbtnSummary.MyLinkLable2 = Nothing
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 391
        Me.rbtnSummary.TabStop = False
        Me.rbtnSummary.Text = "Summary"
        '
        'txtTransaction
        '
        Me.txtTransaction.arrDispalyMember = Nothing
        Me.txtTransaction.arrValueMember = Nothing
        Me.txtTransaction.Location = New System.Drawing.Point(95, 85)
        Me.txtTransaction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransaction.MyLinkLable1 = Nothing
        Me.txtTransaction.MyLinkLable2 = Nothing
        Me.txtTransaction.MyNullText = "All"
        Me.txtTransaction.Name = "txtTransaction"
        Me.txtTransaction.Size = New System.Drawing.Size(243, 19)
        Me.txtTransaction.TabIndex = 394
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 85)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(64, 18)
        Me.MyLabel2.TabIndex = 393
        Me.MyLabel2.Text = "Transaction"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(95, 61)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Nothing
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(243, 19)
        Me.txtCustomer.TabIndex = 392
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 61)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 391
        Me.MyLabel1.Text = "Customer"
        '
        'txtItem
        '
        Me.txtItem.arrDispalyMember = Nothing
        Me.txtItem.arrValueMember = Nothing
        Me.txtItem.Location = New System.Drawing.Point(95, 38)
        Me.txtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.MyLinkLable1 = Nothing
        Me.txtItem.MyLinkLable2 = Nothing
        Me.txtItem.MyNullText = "All"
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(243, 19)
        Me.txtItem.TabIndex = 390
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(5, 38)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 389
        Me.lblItem.Text = "Item"
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
        Me.txtToDate.Location = New System.Drawing.Point(251, 11)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel4
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(87, 20)
        Me.txtToDate.TabIndex = 361
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "28/06/2012"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(192, 12)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel4.TabIndex = 363
        Me.MyLabel4.Text = "To Date"
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
        Me.txtFromDate.Location = New System.Drawing.Point(95, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel3
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(86, 20)
        Me.txtFromDate.TabIndex = 360
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "28/06/2012"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(5, 12)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel3.TabIndex = 364
        Me.MyLabel3.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(764, 376)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(764, 376)
        Me.gv1.TabIndex = 2
        Me.gv1.VarID = ""
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(265, 15)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(71, 22)
        Me.btnPrint.TabIndex = 155
        Me.btnPrint.Text = "Print"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(162, 15)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 154
        Me.RadSplitButton1.Text = "Export"
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
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(724, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(14, 15)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(88, 15)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'MyDateTimePicker1
        '
        Me.MyDateTimePicker1.CalculationExpression = Nothing
        Me.MyDateTimePicker1.CustomFormat = "dd-MM-yyyy"
        Me.MyDateTimePicker1.FieldCode = Nothing
        Me.MyDateTimePicker1.FieldDesc = Nothing
        Me.MyDateTimePicker1.FieldMaxLength = 0
        Me.MyDateTimePicker1.FieldName = Nothing
        Me.MyDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker1.isCalculatedField = False
        Me.MyDateTimePicker1.IsSourceFromTable = False
        Me.MyDateTimePicker1.IsSourceFromValueList = False
        Me.MyDateTimePicker1.IsUnique = False
        Me.MyDateTimePicker1.Location = New System.Drawing.Point(53, 6)
        Me.MyDateTimePicker1.MendatroryField = False
        Me.MyDateTimePicker1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.MyLinkLable1 = Nothing
        Me.MyDateTimePicker1.MyLinkLable2 = Nothing
        Me.MyDateTimePicker1.Name = "MyDateTimePicker1"
        Me.MyDateTimePicker1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.ReferenceFieldDesc = Nothing
        Me.MyDateTimePicker1.ReferenceFieldName = Nothing
        Me.MyDateTimePicker1.ReferenceTableName = Nothing
        Me.MyDateTimePicker1.Size = New System.Drawing.Size(82, 20)
        Me.MyDateTimePicker1.TabIndex = 331
        Me.MyDateTimePicker1.TabStop = False
        Me.MyDateTimePicker1.Text = "17-12-2011"
        Me.MyDateTimePicker1.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'BtnPartySaleMilkProductInvoice
        '
        Me.BtnPartySaleMilkProductInvoice.Location = New System.Drawing.Point(233, 52)
        Me.BtnPartySaleMilkProductInvoice.MyLinkLable1 = Nothing
        Me.BtnPartySaleMilkProductInvoice.MyLinkLable2 = Nothing
        Me.BtnPartySaleMilkProductInvoice.Name = "BtnPartySaleMilkProductInvoice"
        Me.BtnPartySaleMilkProductInvoice.Size = New System.Drawing.Size(203, 18)
        Me.BtnPartySaleMilkProductInvoice.TabIndex = 418
        Me.BtnPartySaleMilkProductInvoice.TabStop = False
        Me.BtnPartySaleMilkProductInvoice.Text = "Party Sale Milk and Product (Invoice)"
        '
        'BtnProductSalesSummaryNonTaxable
        '
        Me.BtnProductSalesSummaryNonTaxable.Location = New System.Drawing.Point(233, 74)
        Me.BtnProductSalesSummaryNonTaxable.MyLinkLable1 = Nothing
        Me.BtnProductSalesSummaryNonTaxable.MyLinkLable2 = Nothing
        Me.BtnProductSalesSummaryNonTaxable.Name = "BtnProductSalesSummaryNonTaxable"
        Me.BtnProductSalesSummaryNonTaxable.Size = New System.Drawing.Size(210, 18)
        Me.BtnProductSalesSummaryNonTaxable.TabIndex = 419
        Me.BtnProductSalesSummaryNonTaxable.TabStop = False
        Me.BtnProductSalesSummaryNonTaxable.Text = "Product Sales Summary (Non Taxable)"
        '
        'rptCustItemWiseSaleReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(855, 494)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptCustItemWiseSaleReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sales Ledger Report"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.txtToDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlDefaultReportUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblQtyConv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlQtyConversionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rbtnDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSupplyDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BKNGroupBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BKNGroupBox.ResumeLayout(False)
        Me.BKNGroupBox.PerformLayout()
        CType(Me.rbtnBoothSaleItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBoothSaleDateShiftWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMilkSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnStockStatement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDistributorCollStatement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnCreditPartyWiseSaleAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnRouteWiseSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnGheeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnTcsSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnTransportationCharges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnBillWiseSaleOfMilkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnProductSalesSummaryTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnMilkStcSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnStcRegisterItemWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnBillWiseSaleOfMilk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPartySaleMilkProductDispatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnProductWiseSaleQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnStcRegisterPartyandItemWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPartySaleMilkProductInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnProductSalesSummaryNonTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents txtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents txtTransaction As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnDetail As common.Controls.MyRadioButton
    Friend WithEvents rbtnSummary As common.Controls.MyRadioButton
    Friend WithEvents BKNGroupBox As RadGroupBox
    Friend WithEvents BtnBillWiseSaleOfMilkSummary As common.Controls.MyRadioButton
    Friend WithEvents BtnProductSalesSummaryTaxable As common.Controls.MyRadioButton
    Friend WithEvents BtnMilkStcSummary As common.Controls.MyRadioButton
    Friend WithEvents BtnStcRegisterItemWiseSummary As common.Controls.MyRadioButton
    Friend WithEvents BtnBillWiseSaleOfMilk As common.Controls.MyRadioButton
    Friend WithEvents BtnPartySaleMilkProductDispatch As common.Controls.MyRadioButton
    Friend WithEvents BtnProductWiseSaleQuantity As common.Controls.MyRadioButton
    Friend WithEvents BtnStcRegisterPartyandItemWiseSummary As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents rbtnDocumentDate As common.Controls.MyRadioButton
    Friend WithEvents rbtnSupplyDate As common.Controls.MyRadioButton
    Friend WithEvents BtnTransportationCharges As common.Controls.MyRadioButton
    Friend WithEvents BtnTcsSummary As common.Controls.MyRadioButton
    Friend WithEvents BtnGheeReport As common.Controls.MyRadioButton
    Friend WithEvents BtnRouteWiseSale As common.Controls.MyRadioButton
    Friend WithEvents BtnCreditPartyWiseSaleAmount As common.Controls.MyRadioButton
    Friend WithEvents rbtnStockStatement As common.Controls.MyRadioButton
    Friend WithEvents rbtnDistributorCollStatement As common.Controls.MyRadioButton
    Friend WithEvents ddlQtyConversionType As common.Controls.MyComboBox
    Friend WithEvents lblQtyConv As common.Controls.MyLabel
    Friend WithEvents rbtnMilkSale As common.Controls.MyRadioButton
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents ddlDefaultReportUOM As common.Controls.MyComboBox
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents txtFromShift As RadDropDownList
    Friend WithEvents txtFromDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents MyDateTimePicker1 As common.Controls.MyDateTimePicker
    Friend WithEvents txtToDate1 As common.Controls.MyDateTimePicker
    Friend WithEvents txtToShift As RadDropDownList
    Friend WithEvents LblToDate As common.Controls.MyLabel
    Friend WithEvents rbtnBoothSaleItemWise As common.Controls.MyRadioButton
    Friend WithEvents rbtnBoothSaleDateShiftWise As common.Controls.MyRadioButton
    Friend WithEvents BtnPartySaleMilkProductInvoice As common.Controls.MyRadioButton
    Friend WithEvents BtnProductSalesSummaryNonTaxable As common.Controls.MyRadioButton
End Class

