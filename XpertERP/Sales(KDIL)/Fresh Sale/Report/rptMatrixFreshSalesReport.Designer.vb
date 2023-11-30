<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RptMatrixFreshSalesReport
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim RadListDataItem15 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem16 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem17 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem18 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem13 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem14 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBoths = New System.Windows.Forms.RadioButton()
        Me.rbtnMrng = New System.Windows.Forms.RadioButton()
        Me.rbtnEvng = New System.Windows.Forms.RadioButton()
        Me.ddlReportType = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndCustomer = New common.UserControls.txtFinder()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnTaxable = New System.Windows.Forms.RadioButton()
        Me.rbtnNonTaxable = New System.Windows.Forms.RadioButton()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.btnCreditPrint = New Telerik.WinControls.UI.RadButton()
        Me.txtCreditDateTo = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtCreditDateFrom = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBoth = New System.Windows.Forms.RadioButton()
        Me.rbtnMilk = New System.Windows.Forms.RadioButton()
        Me.rbtnProduct = New System.Windows.Forms.RadioButton()
        Me.txtCustMultFnd = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.btnTrkShtSummaryRW = New Telerik.WinControls.UI.RadButton()
        Me.txtMultPTSRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.ddlPTSShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.btnPrintTrkSht = New Telerik.WinControls.UI.RadButton()
        Me.txtPTSDateTo = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtPTSDateFrom = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkGatePass = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRouteBoothWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.ChkDayWiseSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFirstAndSecondSpellAbstract = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkProduct = New Telerik.WinControls.UI.RadCheckBox()
        Me.pnlMilkPouch = New System.Windows.Forms.Panel()
        Me.rbtnAsPerBooking = New System.Windows.Forms.RadioButton()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.cboShift = New Telerik.WinControls.UI.RadDropDownList()
        Me.rdbCreate = New System.Windows.Forms.RadioButton()
        Me.rdbLtr = New System.Windows.Forms.RadioButton()
        Me.chkMilkPouch = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkRouteSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFirstAndSecondSpell = New Telerik.WinControls.UI.RadCheckBox()
        Me.chkFilterByCreatedDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.TxtMultiCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtBookingType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.chkSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.ddlInvocieType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblSubCategory = New common.Controls.MyLabel()
        Me.chkSaleInvoiceWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.TxtUOM = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.chkBookingWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtLorry = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtItemCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtZone = New common.UserControls.txtMultiSelectFinder()
        Me.lblCustomerGroup = New common.Controls.MyLabel()
        Me.txtCustomerGroup = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnRouteSummaryPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCreditPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditDateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnTrkShtSummaryRW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlPTSShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrintTrkSht, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPTSDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPTSDateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.chkGatePass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteBoothWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDayWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFirstAndSecondSpellAbstract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMilkPouch.SuspendLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMilkPouch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRouteSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFirstAndSecondSpell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkFilterByCreatedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlInvocieType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSaleInvoiceWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBookingWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRouteSummaryPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRouteSummaryPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(976, 539)
        Me.SplitContainer1.SplitterDistance = 498
        Me.SplitContainer1.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(976, 20)
        Me.RadMenu1.TabIndex = 16
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout For Summary"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout For Summary"
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Location = New System.Drawing.Point(0, 26)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(976, 472)
        Me.RadPageView1.TabIndex = 15
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox7)
        Me.RadPageViewPage1.Controls.Add(Me.ddlReportType)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel15)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.chkProduct)
        Me.RadPageViewPage1.Controls.Add(Me.pnlMilkPouch)
        Me.RadPageViewPage1.Controls.Add(Me.chkRouteSummary)
        Me.RadPageViewPage1.Controls.Add(Me.chkFirstAndSecondSpell)
        Me.RadPageViewPage1.Controls.Add(Me.chkFilterByCreatedDate)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiCustomerCategory)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.txtBookingType)
        Me.RadPageViewPage1.Controls.Add(Me.chkSummary)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.ddlInvocieType)
        Me.RadPageViewPage1.Controls.Add(Me.lblSubCategory)
        Me.RadPageViewPage1.Controls.Add(Me.chkSaleInvoiceWise)
        Me.RadPageViewPage1.Controls.Add(Me.TxtUOM)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.chkBookingWise)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtLorry)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtItemCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtZone)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomerGroup)
        Me.RadPageViewPage1.Controls.Add(Me.txtCustomerGroup)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(955, 424)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.rbtnBoths)
        Me.RadGroupBox7.Controls.Add(Me.rbtnMrng)
        Me.RadGroupBox7.Controls.Add(Me.rbtnEvng)
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(292, 6)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Size = New System.Drawing.Size(250, 26)
        Me.RadGroupBox7.TabIndex = 439
        '
        'rbtnBoths
        '
        Me.rbtnBoths.AutoSize = True
        Me.rbtnBoths.Location = New System.Drawing.Point(186, 4)
        Me.rbtnBoths.Name = "rbtnBoths"
        Me.rbtnBoths.Size = New System.Drawing.Size(49, 17)
        Me.rbtnBoths.TabIndex = 442
        Me.rbtnBoths.Text = "Both"
        Me.rbtnBoths.UseVisualStyleBackColor = True
        '
        'rbtnMrng
        '
        Me.rbtnMrng.AutoSize = True
        Me.rbtnMrng.Checked = True
        Me.rbtnMrng.Location = New System.Drawing.Point(5, 4)
        Me.rbtnMrng.Name = "rbtnMrng"
        Me.rbtnMrng.Size = New System.Drawing.Size(70, 17)
        Me.rbtnMrng.TabIndex = 440
        Me.rbtnMrng.TabStop = True
        Me.rbtnMrng.Text = "Morning"
        Me.rbtnMrng.UseVisualStyleBackColor = True
        '
        'rbtnEvng
        '
        Me.rbtnEvng.AutoSize = True
        Me.rbtnEvng.Location = New System.Drawing.Point(98, 4)
        Me.rbtnEvng.Name = "rbtnEvng"
        Me.rbtnEvng.Size = New System.Drawing.Size(66, 17)
        Me.rbtnEvng.TabIndex = 441
        Me.rbtnEvng.Text = "Evening"
        Me.rbtnEvng.UseVisualStyleBackColor = True
        '
        'ddlReportType
        '
        Me.ddlReportType.AutoCompleteDisplayMember = Nothing
        Me.ddlReportType.AutoCompleteValueMember = Nothing
        Me.ddlReportType.DropDownAnimationEnabled = True
        RadListDataItem15.Text = "Both"
        RadListDataItem16.Text = "Sale Invoice"
        RadListDataItem17.Text = "Sale Return"
        Me.ddlReportType.Items.Add(RadListDataItem15)
        Me.ddlReportType.Items.Add(RadListDataItem16)
        Me.ddlReportType.Items.Add(RadListDataItem17)
        Me.ddlReportType.Location = New System.Drawing.Point(128, 6)
        Me.ddlReportType.Name = "ddlReportType"
        Me.ddlReportType.Size = New System.Drawing.Size(121, 20)
        Me.ddlReportType.TabIndex = 438
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Location = New System.Drawing.Point(16, 7)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(67, 18)
        Me.MyLabel15.TabIndex = 437
        Me.MyLabel15.Text = "Report Type"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.fndCustomer)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox5.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox5.Controls.Add(Me.btnCreditPrint)
        Me.RadGroupBox5.Controls.Add(Me.txtCreditDateTo)
        Me.RadGroupBox5.Controls.Add(Me.txtCreditDateFrom)
        Me.RadGroupBox5.HeaderText = "Credit Sale Report"
        Me.RadGroupBox5.Location = New System.Drawing.Point(607, 270)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(301, 125)
        Me.RadGroupBox5.TabIndex = 436
        Me.RadGroupBox5.Text = "Credit Sale Report"
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
        Me.fndCustomer.Location = New System.Drawing.Point(64, 40)
        Me.fndCustomer.MendatroryField = True
        Me.fndCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndCustomer.MyLinkLable1 = Me.MyLabel9
        Me.fndCustomer.MyLinkLable2 = Nothing
        Me.fndCustomer.MyReadOnly = False
        Me.fndCustomer.MyShowMasterFormButton = False
        Me.fndCustomer.Name = "fndCustomer"
        Me.fndCustomer.ReferenceFieldDesc = Nothing
        Me.fndCustomer.ReferenceFieldName = Nothing
        Me.fndCustomer.ReferenceTableName = Nothing
        Me.fndCustomer.Size = New System.Drawing.Size(194, 19)
        Me.fndCustomer.TabIndex = 441
        Me.fndCustomer.Value = ""
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Location = New System.Drawing.Point(5, 19)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel9.TabIndex = 2
        Me.MyLabel9.Text = "From"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.rbtnTaxable)
        Me.RadGroupBox6.Controls.Add(Me.rbtnNonTaxable)
        Me.RadGroupBox6.HeaderText = ""
        Me.RadGroupBox6.Location = New System.Drawing.Point(64, 61)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(194, 26)
        Me.RadGroupBox6.TabIndex = 440
        '
        'rbtnTaxable
        '
        Me.rbtnTaxable.AutoSize = True
        Me.rbtnTaxable.Checked = True
        Me.rbtnTaxable.Location = New System.Drawing.Point(6, 4)
        Me.rbtnTaxable.Name = "rbtnTaxable"
        Me.rbtnTaxable.Size = New System.Drawing.Size(63, 17)
        Me.rbtnTaxable.TabIndex = 440
        Me.rbtnTaxable.TabStop = True
        Me.rbtnTaxable.Text = "Taxable"
        Me.rbtnTaxable.UseVisualStyleBackColor = True
        '
        'rbtnNonTaxable
        '
        Me.rbtnNonTaxable.AutoSize = True
        Me.rbtnNonTaxable.Location = New System.Drawing.Point(75, 4)
        Me.rbtnNonTaxable.Name = "rbtnNonTaxable"
        Me.rbtnNonTaxable.Size = New System.Drawing.Size(89, 17)
        Me.rbtnNonTaxable.TabIndex = 441
        Me.rbtnNonTaxable.Text = "Non-Taxable"
        Me.rbtnNonTaxable.UseVisualStyleBackColor = True
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(5, 41)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel14.TabIndex = 439
        Me.MyLabel14.Text = "Customer"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(150, 19)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel17.TabIndex = 3
        Me.MyLabel17.Text = "To"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(5, 19)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel18.TabIndex = 2
        Me.MyLabel18.Text = "From"
        '
        'btnCreditPrint
        '
        Me.btnCreditPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreditPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreditPrint.Location = New System.Drawing.Point(157, 92)
        Me.btnCreditPrint.Name = "btnCreditPrint"
        Me.btnCreditPrint.Size = New System.Drawing.Size(133, 22)
        Me.btnCreditPrint.TabIndex = 433
        Me.btnCreditPrint.Text = "Print"
        '
        'txtCreditDateTo
        '
        Me.txtCreditDateTo.CustomFormat = "dd/MM/yyyy"
        Me.txtCreditDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCreditDateTo.Location = New System.Drawing.Point(177, 18)
        Me.txtCreditDateTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCreditDateTo.Name = "txtCreditDateTo"
        Me.txtCreditDateTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCreditDateTo.Size = New System.Drawing.Size(81, 20)
        Me.txtCreditDateTo.TabIndex = 1
        Me.txtCreditDateTo.TabStop = False
        Me.txtCreditDateTo.Text = "24/10/2011"
        Me.txtCreditDateTo.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtCreditDateFrom
        '
        Me.txtCreditDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.txtCreditDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtCreditDateFrom.Location = New System.Drawing.Point(64, 18)
        Me.txtCreditDateFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCreditDateFrom.Name = "txtCreditDateFrom"
        Me.txtCreditDateFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtCreditDateFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtCreditDateFrom.TabIndex = 0
        Me.txtCreditDateFrom.TabStop = False
        Me.txtCreditDateFrom.Text = "24/10/2011"
        Me.txtCreditDateFrom.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox2.Controls.Add(Me.txtCustMultFnd)
        Me.RadGroupBox2.Controls.Add(Me.btnTrkShtSummaryRW)
        Me.RadGroupBox2.Controls.Add(Me.txtMultPTSRoute)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox2.Controls.Add(Me.ddlPTSShift)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox2.Controls.Add(Me.btnPrintTrkSht)
        Me.RadGroupBox2.Controls.Add(Me.txtPTSDateTo)
        Me.RadGroupBox2.Controls.Add(Me.txtPTSDateFrom)
        Me.RadGroupBox2.HeaderText = "Print Truck Sheet"
        Me.RadGroupBox2.Location = New System.Drawing.Point(607, 80)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(301, 186)
        Me.RadGroupBox2.TabIndex = 435
        Me.RadGroupBox2.Text = "Print Truck Sheet"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.rbtnBoth)
        Me.RadGroupBox4.Controls.Add(Me.rbtnMilk)
        Me.RadGroupBox4.Controls.Add(Me.rbtnProduct)
        Me.RadGroupBox4.HeaderText = ""
        Me.RadGroupBox4.Location = New System.Drawing.Point(64, 104)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(194, 26)
        Me.RadGroupBox4.TabIndex = 436
        '
        'rbtnBoth
        '
        Me.rbtnBoth.AutoSize = True
        Me.rbtnBoth.Location = New System.Drawing.Point(141, 4)
        Me.rbtnBoth.Name = "rbtnBoth"
        Me.rbtnBoth.Size = New System.Drawing.Size(49, 17)
        Me.rbtnBoth.TabIndex = 442
        Me.rbtnBoth.Text = "Both"
        Me.rbtnBoth.UseVisualStyleBackColor = True
        '
        'rbtnMilk
        '
        Me.rbtnMilk.AutoSize = True
        Me.rbtnMilk.Checked = True
        Me.rbtnMilk.Location = New System.Drawing.Point(5, 4)
        Me.rbtnMilk.Name = "rbtnMilk"
        Me.rbtnMilk.Size = New System.Drawing.Size(47, 17)
        Me.rbtnMilk.TabIndex = 440
        Me.rbtnMilk.TabStop = True
        Me.rbtnMilk.Text = "Milk"
        Me.rbtnMilk.UseVisualStyleBackColor = True
        '
        'rbtnProduct
        '
        Me.rbtnProduct.AutoSize = True
        Me.rbtnProduct.Location = New System.Drawing.Point(65, 4)
        Me.rbtnProduct.Name = "rbtnProduct"
        Me.rbtnProduct.Size = New System.Drawing.Size(65, 17)
        Me.rbtnProduct.TabIndex = 441
        Me.rbtnProduct.Text = "Product"
        Me.rbtnProduct.UseVisualStyleBackColor = True
        '
        'txtCustMultFnd
        '
        Me.txtCustMultFnd.arrDispalyMember = Nothing
        Me.txtCustMultFnd.arrValueMember = Nothing
        Me.txtCustMultFnd.Location = New System.Drawing.Point(64, 62)
        Me.txtCustMultFnd.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustMultFnd.MyLinkLable1 = Me.MyLabel13
        Me.txtCustMultFnd.MyLinkLable2 = Nothing
        Me.txtCustMultFnd.MyNullText = "All"
        Me.txtCustMultFnd.Name = "txtCustMultFnd"
        Me.txtCustMultFnd.Size = New System.Drawing.Size(194, 19)
        Me.txtCustMultFnd.TabIndex = 438
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(5, 62)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel13.TabIndex = 439
        Me.MyLabel13.Text = "Customer"
        '
        'btnTrkShtSummaryRW
        '
        Me.btnTrkShtSummaryRW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTrkShtSummaryRW.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTrkShtSummaryRW.Location = New System.Drawing.Point(102, 160)
        Me.btnTrkShtSummaryRW.Name = "btnTrkShtSummaryRW"
        Me.btnTrkShtSummaryRW.Size = New System.Drawing.Size(188, 22)
        Me.btnTrkShtSummaryRW.TabIndex = 434
        Me.btnTrkShtSummaryRW.Text = "Truck Sheet Summary Route Wise"
        '
        'txtMultPTSRoute
        '
        Me.txtMultPTSRoute.arrDispalyMember = Nothing
        Me.txtMultPTSRoute.arrValueMember = Nothing
        Me.txtMultPTSRoute.Location = New System.Drawing.Point(64, 83)
        Me.txtMultPTSRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultPTSRoute.MyLinkLable1 = Me.MyLabel12
        Me.txtMultPTSRoute.MyLinkLable2 = Nothing
        Me.txtMultPTSRoute.MyNullText = "All"
        Me.txtMultPTSRoute.Name = "txtMultPTSRoute"
        Me.txtMultPTSRoute.Size = New System.Drawing.Size(194, 19)
        Me.txtMultPTSRoute.TabIndex = 436
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 83)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel12.TabIndex = 437
        Me.MyLabel12.Text = "Route"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(5, 41)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel11.TabIndex = 434
        Me.MyLabel11.Text = "Shift"
        '
        'ddlPTSShift
        '
        Me.ddlPTSShift.AutoCompleteDisplayMember = Nothing
        Me.ddlPTSShift.AutoCompleteValueMember = Nothing
        Me.ddlPTSShift.DropDownAnimationEnabled = True
        Me.ddlPTSShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Both"
        RadListDataItem2.Text = "Morning"
        RadListDataItem3.Text = "Evening"
        RadListDataItem18.Text = "Shift Wise"
        Me.ddlPTSShift.Items.Add(RadListDataItem1)
        Me.ddlPTSShift.Items.Add(RadListDataItem2)
        Me.ddlPTSShift.Items.Add(RadListDataItem3)
        Me.ddlPTSShift.Items.Add(RadListDataItem18)
        Me.ddlPTSShift.Location = New System.Drawing.Point(64, 40)
        Me.ddlPTSShift.Name = "ddlPTSShift"
        Me.ddlPTSShift.Size = New System.Drawing.Size(194, 20)
        Me.ddlPTSShift.TabIndex = 435
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Location = New System.Drawing.Point(150, 19)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel8.TabIndex = 3
        Me.MyLabel8.Text = "To"
        '
        'btnPrintTrkSht
        '
        Me.btnPrintTrkSht.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintTrkSht.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintTrkSht.Location = New System.Drawing.Point(157, 134)
        Me.btnPrintTrkSht.Name = "btnPrintTrkSht"
        Me.btnPrintTrkSht.Size = New System.Drawing.Size(133, 22)
        Me.btnPrintTrkSht.TabIndex = 433
        Me.btnPrintTrkSht.Text = "Print Truck Sheet"
        '
        'txtPTSDateTo
        '
        Me.txtPTSDateTo.CustomFormat = "dd/MM/yyyy"
        Me.txtPTSDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPTSDateTo.Location = New System.Drawing.Point(177, 18)
        Me.txtPTSDateTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPTSDateTo.Name = "txtPTSDateTo"
        Me.txtPTSDateTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPTSDateTo.Size = New System.Drawing.Size(81, 20)
        Me.txtPTSDateTo.TabIndex = 1
        Me.txtPTSDateTo.TabStop = False
        Me.txtPTSDateTo.Text = "24/10/2011"
        Me.txtPTSDateTo.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtPTSDateFrom
        '
        Me.txtPTSDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.txtPTSDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPTSDateFrom.Location = New System.Drawing.Point(64, 18)
        Me.txtPTSDateFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPTSDateFrom.Name = "txtPTSDateFrom"
        Me.txtPTSDateFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtPTSDateFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtPTSDateFrom.TabIndex = 0
        Me.txtPTSDateFrom.TabStop = False
        Me.txtPTSDateFrom.Text = "24/10/2011"
        Me.txtPTSDateFrom.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkGatePass)
        Me.RadGroupBox1.Controls.Add(Me.chkRouteBoothWise)
        Me.RadGroupBox1.Controls.Add(Me.ChkDayWiseSummary)
        Me.RadGroupBox1.Controls.Add(Me.chkFirstAndSecondSpellAbstract)
        Me.RadGroupBox1.HeaderText = "RadGroupBox1"
        Me.RadGroupBox1.Location = New System.Drawing.Point(605, 382)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(345, 86)
        Me.RadGroupBox1.TabIndex = 432
        Me.RadGroupBox1.Text = "RadGroupBox1"
        Me.RadGroupBox1.Visible = False
        '
        'chkGatePass
        '
        Me.chkGatePass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkGatePass.Enabled = False
        Me.chkGatePass.Location = New System.Drawing.Point(5, 21)
        Me.chkGatePass.Name = "chkGatePass"
        Me.chkGatePass.Size = New System.Drawing.Size(95, 18)
        Me.chkGatePass.TabIndex = 425
        Me.chkGatePass.Text = "Only Gate Pass"
        Me.chkGatePass.Visible = False
        '
        'chkRouteBoothWise
        '
        Me.chkRouteBoothWise.Location = New System.Drawing.Point(5, 36)
        Me.chkRouteBoothWise.Name = "chkRouteBoothWise"
        Me.chkRouteBoothWise.Size = New System.Drawing.Size(114, 18)
        Me.chkRouteBoothWise.TabIndex = 422
        Me.chkRouteBoothWise.Text = "Route/Booth Wise "
        Me.chkRouteBoothWise.Visible = False
        '
        'ChkDayWiseSummary
        '
        Me.ChkDayWiseSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChkDayWiseSummary.Location = New System.Drawing.Point(5, 51)
        Me.ChkDayWiseSummary.Name = "ChkDayWiseSummary"
        Me.ChkDayWiseSummary.Size = New System.Drawing.Size(117, 18)
        Me.ChkDayWiseSummary.TabIndex = 423
        Me.ChkDayWiseSummary.Text = "Day Wise Summary"
        Me.ChkDayWiseSummary.Visible = False
        '
        'chkFirstAndSecondSpellAbstract
        '
        Me.chkFirstAndSecondSpellAbstract.Location = New System.Drawing.Point(5, 66)
        Me.chkFirstAndSecondSpellAbstract.Name = "chkFirstAndSecondSpellAbstract"
        Me.chkFirstAndSecondSpellAbstract.Size = New System.Drawing.Size(226, 18)
        Me.chkFirstAndSecondSpellAbstract.TabIndex = 424
        Me.chkFirstAndSecondSpellAbstract.Text = "First And Second Spell Card Sale Abstract"
        Me.chkFirstAndSecondSpellAbstract.Visible = False
        '
        'chkProduct
        '
        Me.chkProduct.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkProduct.Location = New System.Drawing.Point(608, 30)
        Me.chkProduct.Name = "chkProduct"
        Me.chkProduct.Size = New System.Drawing.Size(59, 18)
        Me.chkProduct.TabIndex = 431
        Me.chkProduct.Text = "Product"
        '
        'pnlMilkPouch
        '
        Me.pnlMilkPouch.Controls.Add(Me.rbtnAsPerBooking)
        Me.pnlMilkPouch.Controls.Add(Me.MyLabel7)
        Me.pnlMilkPouch.Controls.Add(Me.cboShift)
        Me.pnlMilkPouch.Controls.Add(Me.rdbCreate)
        Me.pnlMilkPouch.Controls.Add(Me.rdbLtr)
        Me.pnlMilkPouch.Controls.Add(Me.chkMilkPouch)
        Me.pnlMilkPouch.Location = New System.Drawing.Point(607, 3)
        Me.pnlMilkPouch.Name = "pnlMilkPouch"
        Me.pnlMilkPouch.Size = New System.Drawing.Size(301, 50)
        Me.pnlMilkPouch.TabIndex = 428
        '
        'rbtnAsPerBooking
        '
        Me.rbtnAsPerBooking.AutoSize = True
        Me.rbtnAsPerBooking.Location = New System.Drawing.Point(185, 6)
        Me.rbtnAsPerBooking.Name = "rbtnAsPerBooking"
        Me.rbtnAsPerBooking.Size = New System.Drawing.Size(102, 17)
        Me.rbtnAsPerBooking.TabIndex = 430
        Me.rbtnAsPerBooking.Text = "As Per Booking"
        Me.rbtnAsPerBooking.UseVisualStyleBackColor = True
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(87, 26)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel7.TabIndex = 429
        Me.MyLabel7.Text = "Shift"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.DropDownAnimationEnabled = True
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem7.Text = "Both"
        RadListDataItem12.Text = "Morning"
        RadListDataItem13.Text = "Evening"
        RadListDataItem14.Text = "Shift Wise"
        Me.cboShift.Items.Add(RadListDataItem7)
        Me.cboShift.Items.Add(RadListDataItem12)
        Me.cboShift.Items.Add(RadListDataItem13)
        Me.cboShift.Items.Add(RadListDataItem14)
        Me.cboShift.Location = New System.Drawing.Point(122, 25)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.Size = New System.Drawing.Size(91, 20)
        Me.cboShift.TabIndex = 429
        '
        'rdbCreate
        '
        Me.rdbCreate.AutoSize = True
        Me.rdbCreate.Checked = True
        Me.rdbCreate.Location = New System.Drawing.Point(127, 6)
        Me.rdbCreate.Name = "rdbCreate"
        Me.rdbCreate.Size = New System.Drawing.Size(52, 17)
        Me.rdbCreate.TabIndex = 1
        Me.rdbCreate.TabStop = True
        Me.rdbCreate.Text = "Crate"
        Me.rdbCreate.UseVisualStyleBackColor = True
        '
        'rdbLtr
        '
        Me.rdbLtr.AutoSize = True
        Me.rdbLtr.Location = New System.Drawing.Point(85, 6)
        Me.rdbLtr.Name = "rdbLtr"
        Me.rdbLtr.Size = New System.Drawing.Size(38, 17)
        Me.rdbLtr.TabIndex = 0
        Me.rdbLtr.Text = "Ltr"
        Me.rdbLtr.UseVisualStyleBackColor = True
        '
        'chkMilkPouch
        '
        Me.chkMilkPouch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkMilkPouch.Location = New System.Drawing.Point(1, 4)
        Me.chkMilkPouch.Name = "chkMilkPouch"
        Me.chkMilkPouch.Size = New System.Drawing.Size(76, 18)
        Me.chkMilkPouch.TabIndex = 426
        Me.chkMilkPouch.Text = "Milk Pouch"
        '
        'chkRouteSummary
        '
        Me.chkRouteSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRouteSummary.Location = New System.Drawing.Point(607, 56)
        Me.chkRouteSummary.Name = "chkRouteSummary"
        Me.chkRouteSummary.Size = New System.Drawing.Size(126, 18)
        Me.chkRouteSummary.TabIndex = 427
        Me.chkRouteSummary.Text = "Route Summary Print"
        '
        'chkFirstAndSecondSpell
        '
        Me.chkFirstAndSecondSpell.Location = New System.Drawing.Point(282, 111)
        Me.chkFirstAndSecondSpell.Name = "chkFirstAndSecondSpell"
        Me.chkFirstAndSecondSpell.Size = New System.Drawing.Size(232, 18)
        Me.chkFirstAndSecondSpell.TabIndex = 421
        Me.chkFirstAndSecondSpell.Text = "First And Second Spell Card Sale Summary"
        Me.chkFirstAndSecondSpell.Visible = False
        '
        'chkFilterByCreatedDate
        '
        Me.chkFilterByCreatedDate.Location = New System.Drawing.Point(268, 56)
        Me.chkFilterByCreatedDate.Name = "chkFilterByCreatedDate"
        Me.chkFilterByCreatedDate.Size = New System.Drawing.Size(129, 18)
        Me.chkFilterByCreatedDate.TabIndex = 420
        Me.chkFilterByCreatedDate.Text = "Filter By Created Date"
        '
        'TxtMultiCustomerCategory
        '
        Me.TxtMultiCustomerCategory.arrDispalyMember = Nothing
        Me.TxtMultiCustomerCategory.arrValueMember = Nothing
        Me.TxtMultiCustomerCategory.Location = New System.Drawing.Point(133, 324)
        Me.TxtMultiCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCustomerCategory.MyLinkLable1 = Nothing
        Me.TxtMultiCustomerCategory.MyLinkLable2 = Nothing
        Me.TxtMultiCustomerCategory.MyNullText = "All"
        Me.TxtMultiCustomerCategory.Name = "TxtMultiCustomerCategory"
        Me.TxtMultiCustomerCategory.Size = New System.Drawing.Size(331, 19)
        Me.TxtMultiCustomerCategory.TabIndex = 419
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(21, 324)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel6.TabIndex = 418
        Me.MyLabel6.Text = "Customer Category"
        '
        'txtBookingType
        '
        Me.txtBookingType.arrDispalyMember = Nothing
        Me.txtBookingType.arrValueMember = Nothing
        Me.txtBookingType.Location = New System.Drawing.Point(133, 303)
        Me.txtBookingType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBookingType.MyLinkLable1 = Me.MyLabel5
        Me.txtBookingType.MyLinkLable2 = Nothing
        Me.txtBookingType.MyNullText = "All"
        Me.txtBookingType.Name = "txtBookingType"
        Me.txtBookingType.Size = New System.Drawing.Size(332, 19)
        Me.txtBookingType.TabIndex = 399
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(21, 303)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel5.TabIndex = 400
        Me.MyLabel5.Text = "Booking Type"
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(282, 87)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 398
        Me.chkSummary.Text = "Summary"
        Me.chkSummary.Visible = False
        '
        'ddlInvocieType
        '
        Me.ddlInvocieType.AutoCompleteDisplayMember = Nothing
        Me.ddlInvocieType.AutoCompleteValueMember = Nothing
        Me.ddlInvocieType.DropDownAnimationEnabled = True
        RadListDataItem4.Text = "Both"
        RadListDataItem5.Text = "Sale Invoice"
        RadListDataItem6.Text = "Sale Return"
        Me.ddlInvocieType.Items.Add(RadListDataItem4)
        Me.ddlInvocieType.Items.Add(RadListDataItem5)
        Me.ddlInvocieType.Items.Add(RadListDataItem6)
        Me.ddlInvocieType.Location = New System.Drawing.Point(133, 346)
        Me.ddlInvocieType.Name = "ddlInvocieType"
        Me.ddlInvocieType.Size = New System.Drawing.Size(121, 20)
        Me.ddlInvocieType.TabIndex = 397
        '
        'lblSubCategory
        '
        Me.lblSubCategory.FieldName = Nothing
        Me.lblSubCategory.Location = New System.Drawing.Point(21, 347)
        Me.lblSubCategory.Name = "lblSubCategory"
        Me.lblSubCategory.Size = New System.Drawing.Size(69, 18)
        Me.lblSubCategory.TabIndex = 396
        Me.lblSubCategory.Text = "Invoice Type"
        '
        'chkSaleInvoiceWise
        '
        Me.chkSaleInvoiceWise.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSaleInvoiceWise.Location = New System.Drawing.Point(21, 111)
        Me.chkSaleInvoiceWise.Name = "chkSaleInvoiceWise"
        Me.chkSaleInvoiceWise.Size = New System.Drawing.Size(106, 18)
        Me.chkSaleInvoiceWise.TabIndex = 395
        Me.chkSaleInvoiceWise.Text = "Sale Invoice Wise"
        '
        'TxtUOM
        '
        Me.TxtUOM.arrDispalyMember = Nothing
        Me.TxtUOM.arrValueMember = Nothing
        Me.TxtUOM.Location = New System.Drawing.Point(133, 282)
        Me.TxtUOM.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUOM.MyLinkLable1 = Me.MyLabel4
        Me.TxtUOM.MyLinkLable2 = Nothing
        Me.TxtUOM.MyNullText = "All"
        Me.TxtUOM.Name = "TxtUOM"
        Me.TxtUOM.Size = New System.Drawing.Size(332, 19)
        Me.TxtUOM.TabIndex = 393
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(21, 282)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(33, 18)
        Me.MyLabel4.TabIndex = 394
        Me.MyLabel4.Text = "UOM"
        '
        'TxtRoute
        '
        Me.TxtRoute.arrDispalyMember = Nothing
        Me.TxtRoute.arrValueMember = Nothing
        Me.TxtRoute.Location = New System.Drawing.Point(133, 261)
        Me.TxtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoute.MyLinkLable1 = Me.MyLabel10
        Me.TxtRoute.MyLinkLable2 = Nothing
        Me.TxtRoute.MyNullText = "All"
        Me.TxtRoute.Name = "TxtRoute"
        Me.TxtRoute.Size = New System.Drawing.Size(332, 19)
        Me.TxtRoute.TabIndex = 391
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(21, 261)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel10.TabIndex = 392
        Me.MyLabel10.Text = "Route"
        '
        'chkBookingWise
        '
        Me.chkBookingWise.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBookingWise.Location = New System.Drawing.Point(21, 87)
        Me.chkBookingWise.Name = "chkBookingWise"
        Me.chkBookingWise.Size = New System.Drawing.Size(89, 18)
        Me.chkBookingWise.TabIndex = 390
        Me.chkBookingWise.Text = "Booking Wise"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(21, 199)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(31, 18)
        Me.MyLabel3.TabIndex = 389
        Me.MyLabel3.Text = "Lorry"
        '
        'txtLorry
        '
        Me.txtLorry.arrDispalyMember = Nothing
        Me.txtLorry.arrValueMember = Nothing
        Me.txtLorry.Location = New System.Drawing.Point(133, 199)
        Me.txtLorry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLorry.MyLinkLable1 = Me.MyLabel3
        Me.txtLorry.MyLinkLable2 = Nothing
        Me.txtLorry.MyNullText = "All"
        Me.txtLorry.Name = "txtLorry"
        Me.txtLorry.Size = New System.Drawing.Size(332, 19)
        Me.txtLorry.TabIndex = 388
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(21, 158)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 387
        Me.lblCustomer.Text = "Customer"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(133, 158)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Nothing
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(332, 19)
        Me.txtCustomer.TabIndex = 386
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(21, 178)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel2.TabIndex = 385
        Me.MyLabel2.Text = "Item Code"
        '
        'txtItemCode
        '
        Me.txtItemCode.arrDispalyMember = Nothing
        Me.txtItemCode.arrValueMember = Nothing
        Me.txtItemCode.Location = New System.Drawing.Point(133, 178)
        Me.txtItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.MyLinkLable1 = Me.MyLabel2
        Me.txtItemCode.MyLinkLable2 = Nothing
        Me.txtItemCode.MyNullText = "All"
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(332, 19)
        Me.txtItemCode.TabIndex = 384
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(21, 240)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel1.TabIndex = 383
        Me.MyLabel1.Text = "Zone"
        '
        'txtZone
        '
        Me.txtZone.arrDispalyMember = Nothing
        Me.txtZone.arrValueMember = Nothing
        Me.txtZone.Location = New System.Drawing.Point(133, 240)
        Me.txtZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZone.MyLinkLable1 = Me.MyLabel1
        Me.txtZone.MyLinkLable2 = Nothing
        Me.txtZone.MyNullText = "All"
        Me.txtZone.Name = "txtZone"
        Me.txtZone.Size = New System.Drawing.Size(332, 19)
        Me.txtZone.TabIndex = 382
        '
        'lblCustomerGroup
        '
        Me.lblCustomerGroup.FieldName = Nothing
        Me.lblCustomerGroup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerGroup.Location = New System.Drawing.Point(21, 138)
        Me.lblCustomerGroup.Name = "lblCustomerGroup"
        Me.lblCustomerGroup.Size = New System.Drawing.Size(89, 18)
        Me.lblCustomerGroup.TabIndex = 381
        Me.lblCustomerGroup.Text = "Customer Group"
        '
        'txtCustomerGroup
        '
        Me.txtCustomerGroup.arrDispalyMember = Nothing
        Me.txtCustomerGroup.arrValueMember = Nothing
        Me.txtCustomerGroup.Location = New System.Drawing.Point(133, 138)
        Me.txtCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerGroup.MyLinkLable1 = Me.lblCustomerGroup
        Me.txtCustomerGroup.MyLinkLable2 = Nothing
        Me.txtCustomerGroup.MyNullText = "All"
        Me.txtCustomerGroup.Name = "txtCustomerGroup"
        Me.txtCustomerGroup.Size = New System.Drawing.Size(332, 19)
        Me.txtCustomerGroup.TabIndex = 380
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(21, 220)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 379
        Me.lblLocation.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(133, 220)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(332, 19)
        Me.txtLocation.TabIndex = 378
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(16, 39)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(246, 42)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(130, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(5, 16)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(157, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(81, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(81, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(684, 312)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.EnableFiltering = True
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ReadOnly = True
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(684, 312)
        Me.Gv1.TabIndex = 1
        '
        'btnRouteSummaryPrint
        '
        Me.btnRouteSummaryPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRouteSummaryPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRouteSummaryPrint.Location = New System.Drawing.Point(251, 8)
        Me.btnRouteSummaryPrint.Name = "btnRouteSummaryPrint"
        Me.btnRouteSummaryPrint.Size = New System.Drawing.Size(133, 22)
        Me.btnRouteSummaryPrint.TabIndex = 151
        Me.btnRouteSummaryPrint.Text = "Route Summary Print"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.PDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(150, 8)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 150
        Me.RadSplitButton1.Text = "Export"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel"
        '
        'PDF
        '
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(890, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 148
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(2, 8)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 146
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(76, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 147
        Me.btnReset.Text = "Reset"
        '
        'RptMatrixFreshSalesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 539)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptMatrixFreshSalesReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Matrix Fresh Sales Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCreditPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditDateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnTrkShtSummaryRW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlPTSShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrintTrkSht, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPTSDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPTSDateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.chkGatePass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteBoothWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDayWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFirstAndSecondSpellAbstract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMilkPouch.ResumeLayout(False)
        Me.pnlMilkPouch.PerformLayout()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMilkPouch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRouteSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFirstAndSecondSpell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkFilterByCreatedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlInvocieType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSaleInvoiceWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBookingWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRouteSummaryPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtLorry As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtItemCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomerGroup As common.Controls.MyLabel
    Friend WithEvents txtCustomerGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkBookingWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents TxtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents TxtUOM As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents chkSaleInvoiceWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ddlInvocieType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblSubCategory As common.Controls.MyLabel
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkSummary As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtBookingType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents TxtMultiCustomerCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkFilterByCreatedDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkFirstAndSecondSpell As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkRouteBoothWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents ChkDayWiseSummary As RadCheckBox
    Friend WithEvents chkFirstAndSecondSpellAbstract As RadCheckBox
    Friend WithEvents chkGatePass As RadCheckBox
    Friend WithEvents chkMilkPouch As RadCheckBox
    Friend WithEvents chkRouteSummary As RadCheckBox
    Friend WithEvents btnRouteSummaryPrint As RadButton
    Friend WithEvents pnlMilkPouch As Panel
    Friend WithEvents rdbCreate As RadioButton
    Friend WithEvents rdbLtr As RadioButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents cboShift As RadDropDownList
    Friend WithEvents rbtnAsPerBooking As RadioButton
    Friend WithEvents chkProduct As RadCheckBox
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents btnTrkShtSummaryRW As RadButton
    Friend WithEvents btnPrintTrkSht As RadButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtPTSDateTo As RadDateTimePicker
    Friend WithEvents txtPTSDateFrom As RadDateTimePicker
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents ddlPTSShift As RadDropDownList
    Friend WithEvents txtMultPTSRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtCustMultFnd As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents rbtnBoth As RadioButton
    Friend WithEvents rbtnProduct As RadioButton
    Friend WithEvents rbtnMilk As RadioButton
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents btnCreditPrint As RadButton
    Friend WithEvents txtCreditDateTo As RadDateTimePicker
    Friend WithEvents txtCreditDateFrom As RadDateTimePicker
    Friend WithEvents RadGroupBox6 As RadGroupBox
    Friend WithEvents rbtnTaxable As RadioButton
    Friend WithEvents rbtnNonTaxable As RadioButton
    Friend WithEvents fndCustomer As common.UserControls.txtFinder
    Friend WithEvents RadGroupBox7 As RadGroupBox
    Friend WithEvents rbtnBoths As RadioButton
    Friend WithEvents rbtnMrng As RadioButton
    Friend WithEvents rbtnEvng As RadioButton
    Friend WithEvents ddlReportType As RadDropDownList
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
End Class

