<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptDairyTruckSheetReport
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnPrintInvoice = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.txtInvMultiCust = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.MyLabel25 = New common.Controls.MyLabel()
        Me.txtInvToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtInvFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.chkShowEarlyRoute = New Telerik.WinControls.UI.RadCheckBox()
        Me.gbInstitutionDailySalesReport = New Telerik.WinControls.UI.RadGroupBox()
        Me.btnIDSPrintABS = New Telerik.WinControls.UI.RadButton()
        Me.dtpIDStodate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.dtpIDSfromdate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.cmbCustomerCategory = New common.Controls.MyComboBox()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.lblIDSCustomer = New common.Controls.MyLabel()
        Me.txtIDSCustomer = New common.UserControls.txtFinder()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.lblIDSItem = New common.Controls.MyLabel()
        Me.txtIDSItem = New common.UserControls.txtFinder()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.btnIDSPrint = New Telerik.WinControls.UI.RadButton()
        Me.gbNetSalesReport = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkConsiderShowEarlyRoute = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtRouteNetSales = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.dtpNSRToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtNSRZone = New common.UserControls.txtMultiSelectFinder()
        Me.txtNSRLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.cboNSRType = New common.Controls.MyComboBox()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.dtpNSRFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.btnNSRPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtRouteABS = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtABSZone = New common.UserControls.txtMultiSelectFinder()
        Me.txtABSLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.cboABSReportShift = New common.Controls.MyComboBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.cboABSReportType = New common.Controls.MyComboBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.txtABSDateToTime = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtABSDateFromTime = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtABSDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RGBTSPrint = New Telerik.WinControls.UI.RadGroupBox()
        Me.cmbGatePassType = New common.Controls.MyComboBox()
        Me.lblGatePassType = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtMultiSelectFinder()
        Me.btn_CancelTruckSheet = New Telerik.WinControls.UI.RadButton()
        Me.TxtMultiDistributor = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.btn_DistributorTS = New Telerik.WinControls.UI.RadButton()
        Me.btn_PrintGatePass = New Telerik.WinControls.UI.RadButton()
        Me.btn_TruckSheetGenerated = New Telerik.WinControls.UI.RadButton()
        Me.TxtMultiCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.btnDotMatrixPrinter = New Telerik.WinControls.UI.RadButton()
        Me.dtpToTime = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpFromTime = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnordersheetprint = New Telerik.WinControls.UI.RadButton()
        Me.lblRouteNo = New common.Controls.MyLabel()
        Me.TSP_Reset = New Telerik.WinControls.UI.RadButton()
        Me.lblVehicleNo = New common.Controls.MyTextBox()
        Me.lbl_tsp_Date = New common.Controls.MyLabel()
        Me.TSP_Date = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtLorryNo = New common.UserControls.txtFinder()
        Me.lbl_Vehicle = New common.Controls.MyLabel()
        Me.btn_TSPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMultLocation = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtMultCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btnPrintInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInvFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShowEarlyRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbInstitutionDailySalesReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbInstitutionDailySalesReport.SuspendLayout()
        CType(Me.btnIDSPrintABS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpIDStodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpIDSfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCustomerCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIDSCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblIDSItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnIDSPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbNetSalesReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbNetSalesReport.SuspendLayout()
        CType(Me.chkConsiderShowEarlyRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpNSRToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNSRType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpNSRFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNSRPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboABSReportShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboABSReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtABSDateToTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtABSDateFromTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtABSDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RGBTSPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RGBTSPrint.SuspendLayout()
        CType(Me.cmbGatePassType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGatePassType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_CancelTruckSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_DistributorTS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_PrintGatePass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_TruckSheetGenerated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDotMatrixPrinter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnordersheetprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TSP_Reset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_tsp_Date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TSP_Date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl_Vehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_TSPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(888, 601)
        Me.SplitContainer1.SplitterDistance = 557
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
        Me.RadPageView1.Size = New System.Drawing.Size(888, 557)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.chkShowEarlyRoute)
        Me.RadPageViewPage1.Controls.Add(Me.gbInstitutionDailySalesReport)
        Me.RadPageViewPage1.Controls.Add(Me.gbNetSalesReport)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RGBTSPrint)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.txtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultCustomer)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(867, 509)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.btnPrintInvoice)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel26)
        Me.RadGroupBox2.Controls.Add(Me.txtInvMultiCust)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel24)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel25)
        Me.RadGroupBox2.Controls.Add(Me.txtInvToDate)
        Me.RadGroupBox2.Controls.Add(Me.txtInvFromDate)
        Me.RadGroupBox2.HeaderText = "Print Invoice"
        Me.RadGroupBox2.Location = New System.Drawing.Point(441, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(426, 98)
        Me.RadGroupBox2.TabIndex = 1396
        Me.RadGroupBox2.Text = "Print Invoice"
        Me.RadGroupBox2.Visible = False
        '
        'btnPrintInvoice
        '
        Me.btnPrintInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrintInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintInvoice.Location = New System.Drawing.Point(312, 70)
        Me.btnPrintInvoice.Name = "btnPrintInvoice"
        Me.btnPrintInvoice.Size = New System.Drawing.Size(109, 22)
        Me.btnPrintInvoice.TabIndex = 409
        Me.btnPrintInvoice.Text = "Print Invoice"
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel26.Location = New System.Drawing.Point(5, 38)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel26.TabIndex = 407
        Me.MyLabel26.Text = "Customer"
        '
        'txtInvMultiCust
        '
        Me.txtInvMultiCust.arrDispalyMember = Nothing
        Me.txtInvMultiCust.arrValueMember = Nothing
        Me.txtInvMultiCust.Location = New System.Drawing.Point(63, 38)
        Me.txtInvMultiCust.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvMultiCust.MyLinkLable1 = Nothing
        Me.txtInvMultiCust.MyLinkLable2 = Nothing
        Me.txtInvMultiCust.MyNullText = "All"
        Me.txtInvMultiCust.Name = "txtInvMultiCust"
        Me.txtInvMultiCust.Size = New System.Drawing.Size(358, 19)
        Me.txtInvMultiCust.TabIndex = 408
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Location = New System.Drawing.Point(205, 16)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel24.TabIndex = 3
        Me.MyLabel24.Text = "To"
        '
        'MyLabel25
        '
        Me.MyLabel25.FieldName = Nothing
        Me.MyLabel25.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel25.Name = "MyLabel25"
        Me.MyLabel25.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel25.TabIndex = 2
        Me.MyLabel25.Text = "From"
        '
        'txtInvToDate
        '
        Me.txtInvToDate.CustomFormat = "dd/MM/yyyy hh:mmtt"
        Me.txtInvToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInvToDate.Location = New System.Drawing.Point(233, 15)
        Me.txtInvToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvToDate.Name = "txtInvToDate"
        Me.txtInvToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvToDate.Size = New System.Drawing.Size(135, 20)
        Me.txtInvToDate.TabIndex = 1
        Me.txtInvToDate.TabStop = False
        Me.txtInvToDate.Text = "24/10/2011 11:59PM"
        Me.txtInvToDate.Value = New Date(2011, 10, 24, 23, 59, 0, 0)
        '
        'txtInvFromDate
        '
        Me.txtInvFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtInvFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtInvFromDate.Location = New System.Drawing.Point(63, 15)
        Me.txtInvFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvFromDate.Name = "txtInvFromDate"
        Me.txtInvFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtInvFromDate.Size = New System.Drawing.Size(132, 20)
        Me.txtInvFromDate.TabIndex = 0
        Me.txtInvFromDate.TabStop = False
        Me.txtInvFromDate.Text = "03/05/2011 12:00 AM"
        Me.txtInvFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'chkShowEarlyRoute
        '
        Me.chkShowEarlyRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowEarlyRoute.Location = New System.Drawing.Point(443, 464)
        Me.chkShowEarlyRoute.Name = "chkShowEarlyRoute"
        Me.chkShowEarlyRoute.Size = New System.Drawing.Size(128, 16)
        Me.chkShowEarlyRoute.TabIndex = 1395
        Me.chkShowEarlyRoute.Text = "Show Early Route No"
        '
        'gbInstitutionDailySalesReport
        '
        Me.gbInstitutionDailySalesReport.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.btnIDSPrintABS)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.dtpIDStodate)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.MyLabel23)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.dtpIDSfromdate)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.MyLabel15)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.cmbCustomerCategory)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.MyLabel22)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.lblIDSCustomer)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.txtIDSCustomer)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.MyLabel21)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.lblIDSItem)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.txtIDSItem)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.MyLabel19)
        Me.gbInstitutionDailySalesReport.Controls.Add(Me.btnIDSPrint)
        Me.gbInstitutionDailySalesReport.HeaderText = "Credit Institution Report [DOT MATRIX]"
        Me.gbInstitutionDailySalesReport.Location = New System.Drawing.Point(441, 304)
        Me.gbInstitutionDailySalesReport.Name = "gbInstitutionDailySalesReport"
        Me.gbInstitutionDailySalesReport.Size = New System.Drawing.Size(426, 153)
        Me.gbInstitutionDailySalesReport.TabIndex = 414
        Me.gbInstitutionDailySalesReport.Text = "Credit Institution Report [DOT MATRIX]"
        Me.gbInstitutionDailySalesReport.Visible = False
        '
        'btnIDSPrintABS
        '
        Me.btnIDSPrintABS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIDSPrintABS.Enabled = False
        Me.btnIDSPrintABS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIDSPrintABS.Location = New System.Drawing.Point(312, 125)
        Me.btnIDSPrintABS.Name = "btnIDSPrintABS"
        Me.btnIDSPrintABS.Size = New System.Drawing.Size(110, 22)
        Me.btnIDSPrintABS.TabIndex = 432
        Me.btnIDSPrintABS.Text = "Abstract Print"
        '
        'dtpIDStodate
        '
        Me.dtpIDStodate.CustomFormat = "dd/MM/yyyy"
        Me.dtpIDStodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIDStodate.Location = New System.Drawing.Point(229, 48)
        Me.dtpIDStodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpIDStodate.Name = "dtpIDStodate"
        Me.dtpIDStodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpIDStodate.Size = New System.Drawing.Size(100, 20)
        Me.dtpIDStodate.TabIndex = 431
        Me.dtpIDStodate.TabStop = False
        Me.dtpIDStodate.Text = "03/05/2011"
        Me.dtpIDStodate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(177, 51)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel23.TabIndex = 430
        Me.MyLabel23.Text = "To Date"
        '
        'dtpIDSfromdate
        '
        Me.dtpIDSfromdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpIDSfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIDSfromdate.Location = New System.Drawing.Point(71, 48)
        Me.dtpIDSfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpIDSfromdate.Name = "dtpIDSfromdate"
        Me.dtpIDSfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpIDSfromdate.Size = New System.Drawing.Size(100, 20)
        Me.dtpIDSfromdate.TabIndex = 429
        Me.dtpIDSfromdate.TabStop = False
        Me.dtpIDSfromdate.Text = "03/05/2011"
        Me.dtpIDSfromdate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(7, 51)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel15.TabIndex = 428
        Me.MyLabel15.Text = "From Date"
        '
        'cmbCustomerCategory
        '
        Me.cmbCustomerCategory.AutoCompleteDisplayMember = Nothing
        Me.cmbCustomerCategory.AutoCompleteValueMember = Nothing
        Me.cmbCustomerCategory.CalculationExpression = Nothing
        Me.cmbCustomerCategory.DropDownAnimationEnabled = True
        Me.cmbCustomerCategory.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbCustomerCategory.FieldCode = Nothing
        Me.cmbCustomerCategory.FieldDesc = Nothing
        Me.cmbCustomerCategory.FieldMaxLength = 0
        Me.cmbCustomerCategory.FieldName = Nothing
        Me.cmbCustomerCategory.isCalculatedField = False
        Me.cmbCustomerCategory.IsSourceFromTable = False
        Me.cmbCustomerCategory.IsSourceFromValueList = False
        Me.cmbCustomerCategory.IsUnique = False
        RadListDataItem1.Text = "Select"
        RadListDataItem2.Text = "Vendor"
        RadListDataItem3.Text = "Institution CR"
        RadListDataItem4.Text = "Institution SO"
        RadListDataItem5.Text = "Distributor"
        RadListDataItem6.Text = "Others"
        RadListDataItem7.Text = "UP COUNTRY"
        RadListDataItem8.Text = "FORENOON"
        RadListDataItem9.Text = "PARLOR SALES"
        Me.cmbCustomerCategory.Items.Add(RadListDataItem1)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem2)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem3)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem4)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem5)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem6)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem7)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem8)
        Me.cmbCustomerCategory.Items.Add(RadListDataItem9)
        Me.cmbCustomerCategory.Location = New System.Drawing.Point(71, 26)
        Me.cmbCustomerCategory.MendatroryField = False
        Me.cmbCustomerCategory.MyLinkLable1 = Nothing
        Me.cmbCustomerCategory.MyLinkLable2 = Nothing
        Me.cmbCustomerCategory.Name = "cmbCustomerCategory"
        Me.cmbCustomerCategory.ReferenceFieldDesc = Nothing
        Me.cmbCustomerCategory.ReferenceFieldName = Nothing
        Me.cmbCustomerCategory.ReferenceTableName = Nothing
        Me.cmbCustomerCategory.Size = New System.Drawing.Size(193, 20)
        Me.cmbCustomerCategory.TabIndex = 427
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel22.Location = New System.Drawing.Point(7, 28)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel22.TabIndex = 426
        Me.MyLabel22.Text = "Category"
        '
        'lblIDSCustomer
        '
        Me.lblIDSCustomer.AutoSize = False
        Me.lblIDSCustomer.BorderVisible = True
        Me.lblIDSCustomer.FieldName = Nothing
        Me.lblIDSCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIDSCustomer.Location = New System.Drawing.Point(218, 95)
        Me.lblIDSCustomer.Name = "lblIDSCustomer"
        Me.lblIDSCustomer.Size = New System.Drawing.Size(204, 21)
        Me.lblIDSCustomer.TabIndex = 422
        '
        'txtIDSCustomer
        '
        Me.txtIDSCustomer.CalculationExpression = Nothing
        Me.txtIDSCustomer.FieldCode = Nothing
        Me.txtIDSCustomer.FieldDesc = Nothing
        Me.txtIDSCustomer.FieldMaxLength = 0
        Me.txtIDSCustomer.FieldName = Nothing
        Me.txtIDSCustomer.isCalculatedField = False
        Me.txtIDSCustomer.IsSourceFromTable = False
        Me.txtIDSCustomer.IsSourceFromValueList = False
        Me.txtIDSCustomer.IsUnique = False
        Me.txtIDSCustomer.Location = New System.Drawing.Point(71, 95)
        Me.txtIDSCustomer.MendatroryField = True
        Me.txtIDSCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDSCustomer.MyLinkLable1 = Me.lblIDSCustomer
        Me.txtIDSCustomer.MyLinkLable2 = Me.MyLabel21
        Me.txtIDSCustomer.MyReadOnly = False
        Me.txtIDSCustomer.MyShowMasterFormButton = False
        Me.txtIDSCustomer.Name = "txtIDSCustomer"
        Me.txtIDSCustomer.ReferenceFieldDesc = Nothing
        Me.txtIDSCustomer.ReferenceFieldName = Nothing
        Me.txtIDSCustomer.ReferenceTableName = Nothing
        Me.txtIDSCustomer.Size = New System.Drawing.Size(141, 21)
        Me.txtIDSCustomer.TabIndex = 421
        Me.txtIDSCustomer.Value = ""
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(7, 97)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel21.TabIndex = 423
        Me.MyLabel21.Text = "Customer"
        '
        'lblIDSItem
        '
        Me.lblIDSItem.AutoSize = False
        Me.lblIDSItem.BorderVisible = True
        Me.lblIDSItem.FieldName = Nothing
        Me.lblIDSItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIDSItem.Location = New System.Drawing.Point(218, 71)
        Me.lblIDSItem.Name = "lblIDSItem"
        Me.lblIDSItem.Size = New System.Drawing.Size(204, 21)
        Me.lblIDSItem.TabIndex = 419
        '
        'txtIDSItem
        '
        Me.txtIDSItem.CalculationExpression = Nothing
        Me.txtIDSItem.FieldCode = Nothing
        Me.txtIDSItem.FieldDesc = Nothing
        Me.txtIDSItem.FieldMaxLength = 0
        Me.txtIDSItem.FieldName = Nothing
        Me.txtIDSItem.isCalculatedField = False
        Me.txtIDSItem.IsSourceFromTable = False
        Me.txtIDSItem.IsSourceFromValueList = False
        Me.txtIDSItem.IsUnique = False
        Me.txtIDSItem.Location = New System.Drawing.Point(71, 71)
        Me.txtIDSItem.MendatroryField = True
        Me.txtIDSItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDSItem.MyLinkLable1 = Me.lblIDSItem
        Me.txtIDSItem.MyLinkLable2 = Me.MyLabel19
        Me.txtIDSItem.MyReadOnly = False
        Me.txtIDSItem.MyShowMasterFormButton = False
        Me.txtIDSItem.Name = "txtIDSItem"
        Me.txtIDSItem.ReferenceFieldDesc = Nothing
        Me.txtIDSItem.ReferenceFieldName = Nothing
        Me.txtIDSItem.ReferenceTableName = Nothing
        Me.txtIDSItem.Size = New System.Drawing.Size(141, 21)
        Me.txtIDSItem.TabIndex = 418
        Me.txtIDSItem.Value = ""
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(7, 73)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel19.TabIndex = 420
        Me.MyLabel19.Text = "Item"
        '
        'btnIDSPrint
        '
        Me.btnIDSPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIDSPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIDSPrint.Location = New System.Drawing.Point(229, 125)
        Me.btnIDSPrint.Name = "btnIDSPrint"
        Me.btnIDSPrint.Size = New System.Drawing.Size(77, 22)
        Me.btnIDSPrint.TabIndex = 156
        Me.btnIDSPrint.Text = "Print"
        '
        'gbNetSalesReport
        '
        Me.gbNetSalesReport.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbNetSalesReport.Controls.Add(Me.chkConsiderShowEarlyRoute)
        Me.gbNetSalesReport.Controls.Add(Me.txtRouteNetSales)
        Me.gbNetSalesReport.Controls.Add(Me.MyLabel20)
        Me.gbNetSalesReport.Controls.Add(Me.MyLabel13)
        Me.gbNetSalesReport.Controls.Add(Me.dtpNSRToDate)
        Me.gbNetSalesReport.Controls.Add(Me.txtNSRZone)
        Me.gbNetSalesReport.Controls.Add(Me.txtNSRLocation)
        Me.gbNetSalesReport.Controls.Add(Me.MyLabel11)
        Me.gbNetSalesReport.Controls.Add(Me.MyLabel12)
        Me.gbNetSalesReport.Controls.Add(Me.cboNSRType)
        Me.gbNetSalesReport.Controls.Add(Me.MyLabel14)
        Me.gbNetSalesReport.Controls.Add(Me.MyLabel17)
        Me.gbNetSalesReport.Controls.Add(Me.dtpNSRFromDate)
        Me.gbNetSalesReport.Controls.Add(Me.btnNSRPrint)
        Me.gbNetSalesReport.HeaderText = "Net Sales Report [DOT MATRIX]"
        Me.gbNetSalesReport.Location = New System.Drawing.Point(441, 138)
        Me.gbNetSalesReport.Name = "gbNetSalesReport"
        Me.gbNetSalesReport.Size = New System.Drawing.Size(426, 161)
        Me.gbNetSalesReport.TabIndex = 413
        Me.gbNetSalesReport.Text = "Net Sales Report [DOT MATRIX]"
        Me.gbNetSalesReport.Visible = False
        '
        'chkConsiderShowEarlyRoute
        '
        Me.chkConsiderShowEarlyRoute.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsiderShowEarlyRoute.Location = New System.Drawing.Point(5, 136)
        Me.chkConsiderShowEarlyRoute.Name = "chkConsiderShowEarlyRoute"
        Me.chkConsiderShowEarlyRoute.Size = New System.Drawing.Size(145, 16)
        Me.chkConsiderShowEarlyRoute.TabIndex = 1396
        Me.chkConsiderShowEarlyRoute.Text = "Consider Early Route No"
        '
        'txtRouteNetSales
        '
        Me.txtRouteNetSales.arrDispalyMember = Nothing
        Me.txtRouteNetSales.arrValueMember = Nothing
        Me.txtRouteNetSales.Location = New System.Drawing.Point(68, 104)
        Me.txtRouteNetSales.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNetSales.MyLinkLable1 = Nothing
        Me.txtRouteNetSales.MyLinkLable2 = Nothing
        Me.txtRouteNetSales.MyNullText = "All"
        Me.txtRouteNetSales.Name = "txtRouteNetSales"
        Me.txtRouteNetSales.Size = New System.Drawing.Size(353, 19)
        Me.txtRouteNetSales.TabIndex = 416
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(5, 107)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel20.TabIndex = 415
        Me.MyLabel20.Text = "Route No"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(174, 42)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel13.TabIndex = 414
        Me.MyLabel13.Text = "To Date"
        '
        'dtpNSRToDate
        '
        Me.dtpNSRToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpNSRToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNSRToDate.Location = New System.Drawing.Point(237, 40)
        Me.dtpNSRToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpNSRToDate.Name = "dtpNSRToDate"
        Me.dtpNSRToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpNSRToDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpNSRToDate.TabIndex = 413
        Me.dtpNSRToDate.TabStop = False
        Me.dtpNSRToDate.Text = "03/05/2011"
        Me.dtpNSRToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtNSRZone
        '
        Me.txtNSRZone.arrDispalyMember = Nothing
        Me.txtNSRZone.arrValueMember = Nothing
        Me.txtNSRZone.Location = New System.Drawing.Point(68, 83)
        Me.txtNSRZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNSRZone.MyLinkLable1 = Nothing
        Me.txtNSRZone.MyLinkLable2 = Nothing
        Me.txtNSRZone.MyNullText = "All"
        Me.txtNSRZone.Name = "txtNSRZone"
        Me.txtNSRZone.Size = New System.Drawing.Size(353, 19)
        Me.txtNSRZone.TabIndex = 412
        '
        'txtNSRLocation
        '
        Me.txtNSRLocation.arrDispalyMember = Nothing
        Me.txtNSRLocation.arrValueMember = Nothing
        Me.txtNSRLocation.Location = New System.Drawing.Point(68, 62)
        Me.txtNSRLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNSRLocation.MyLinkLable1 = Nothing
        Me.txtNSRLocation.MyLinkLable2 = Nothing
        Me.txtNSRLocation.MyNullText = "All"
        Me.txtNSRLocation.Name = "txtNSRLocation"
        Me.txtNSRLocation.Size = New System.Drawing.Size(353, 19)
        Me.txtNSRLocation.TabIndex = 410
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(5, 83)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel11.TabIndex = 411
        Me.MyLabel11.Text = "Zone"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 62)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel12.TabIndex = 409
        Me.MyLabel12.Text = "Location"
        '
        'cboNSRType
        '
        Me.cboNSRType.AutoCompleteDisplayMember = Nothing
        Me.cboNSRType.AutoCompleteValueMember = Nothing
        Me.cboNSRType.CalculationExpression = Nothing
        Me.cboNSRType.DropDownAnimationEnabled = True
        Me.cboNSRType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboNSRType.FieldCode = Nothing
        Me.cboNSRType.FieldDesc = Nothing
        Me.cboNSRType.FieldMaxLength = 0
        Me.cboNSRType.FieldName = Nothing
        Me.cboNSRType.isCalculatedField = False
        Me.cboNSRType.IsSourceFromTable = False
        Me.cboNSRType.IsSourceFromValueList = False
        Me.cboNSRType.IsUnique = False
        Me.cboNSRType.Location = New System.Drawing.Point(68, 18)
        Me.cboNSRType.MendatroryField = True
        Me.cboNSRType.MyLinkLable1 = Me.MyLabel14
        Me.cboNSRType.MyLinkLable2 = Nothing
        Me.cboNSRType.Name = "cboNSRType"
        Me.cboNSRType.ReferenceFieldDesc = Nothing
        Me.cboNSRType.ReferenceFieldName = Nothing
        Me.cboNSRType.ReferenceTableName = Nothing
        Me.cboNSRType.Size = New System.Drawing.Size(194, 20)
        Me.cboNSRType.TabIndex = 171
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(5, 20)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel14.TabIndex = 172
        Me.MyLabel14.Text = "Type"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(5, 42)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel17.TabIndex = 160
        Me.MyLabel17.Text = "From Date"
        '
        'dtpNSRFromDate
        '
        Me.dtpNSRFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpNSRFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNSRFromDate.Location = New System.Drawing.Point(68, 40)
        Me.dtpNSRFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpNSRFromDate.Name = "dtpNSRFromDate"
        Me.dtpNSRFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpNSRFromDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpNSRFromDate.TabIndex = 159
        Me.dtpNSRFromDate.TabStop = False
        Me.dtpNSRFromDate.Text = "03/05/2011"
        Me.dtpNSRFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'btnNSRPrint
        '
        Me.btnNSRPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNSRPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNSRPrint.Location = New System.Drawing.Point(344, 133)
        Me.btnNSRPrint.Name = "btnNSRPrint"
        Me.btnNSRPrint.Size = New System.Drawing.Size(77, 22)
        Me.btnNSRPrint.TabIndex = 156
        Me.btnNSRPrint.Text = "Print"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtRouteABS)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox1.Controls.Add(Me.txtABSZone)
        Me.RadGroupBox1.Controls.Add(Me.txtABSLocation)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.cboABSReportShift)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.cboABSReportType)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox1.Controls.Add(Me.txtABSDateToTime)
        Me.RadGroupBox1.Controls.Add(Me.txtABSDateFromTime)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.txtABSDate)
        Me.RadGroupBox1.Controls.Add(Me.RadButton4)
        Me.RadGroupBox1.HeaderText = "Abstract Reports [DOT MATRIX]"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 357)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(421, 152)
        Me.RadGroupBox1.TabIndex = 412
        Me.RadGroupBox1.Text = "Abstract Reports [DOT MATRIX]"
        Me.RadGroupBox1.Visible = False
        '
        'txtRouteABS
        '
        Me.txtRouteABS.arrDispalyMember = Nothing
        Me.txtRouteABS.arrValueMember = Nothing
        Me.txtRouteABS.Location = New System.Drawing.Point(61, 105)
        Me.txtRouteABS.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteABS.MyLinkLable1 = Nothing
        Me.txtRouteABS.MyLinkLable2 = Nothing
        Me.txtRouteABS.MyNullText = "All"
        Me.txtRouteABS.Name = "txtRouteABS"
        Me.txtRouteABS.Size = New System.Drawing.Size(354, 19)
        Me.txtRouteABS.TabIndex = 414
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(5, 108)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(54, 16)
        Me.MyLabel18.TabIndex = 413
        Me.MyLabel18.Text = "Route No"
        '
        'txtABSZone
        '
        Me.txtABSZone.arrDispalyMember = Nothing
        Me.txtABSZone.arrValueMember = Nothing
        Me.txtABSZone.Location = New System.Drawing.Point(62, 84)
        Me.txtABSZone.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtABSZone.MyLinkLable1 = Nothing
        Me.txtABSZone.MyLinkLable2 = Nothing
        Me.txtABSZone.MyNullText = "All"
        Me.txtABSZone.Name = "txtABSZone"
        Me.txtABSZone.Size = New System.Drawing.Size(353, 19)
        Me.txtABSZone.TabIndex = 412
        '
        'txtABSLocation
        '
        Me.txtABSLocation.arrDispalyMember = Nothing
        Me.txtABSLocation.arrValueMember = Nothing
        Me.txtABSLocation.Location = New System.Drawing.Point(62, 62)
        Me.txtABSLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtABSLocation.MyLinkLable1 = Nothing
        Me.txtABSLocation.MyLinkLable2 = Nothing
        Me.txtABSLocation.MyNullText = "All"
        Me.txtABSLocation.Name = "txtABSLocation"
        Me.txtABSLocation.Size = New System.Drawing.Size(353, 19)
        Me.txtABSLocation.TabIndex = 410
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(5, 84)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel8.TabIndex = 411
        Me.MyLabel8.Text = "Zone"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(5, 62)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel6.TabIndex = 409
        Me.MyLabel6.Text = "Location"
        '
        'cboABSReportShift
        '
        Me.cboABSReportShift.AutoCompleteDisplayMember = Nothing
        Me.cboABSReportShift.AutoCompleteValueMember = Nothing
        Me.cboABSReportShift.CalculationExpression = Nothing
        Me.cboABSReportShift.DropDownAnimationEnabled = True
        Me.cboABSReportShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboABSReportShift.FieldCode = Nothing
        Me.cboABSReportShift.FieldDesc = Nothing
        Me.cboABSReportShift.FieldMaxLength = 0
        Me.cboABSReportShift.FieldName = Nothing
        Me.cboABSReportShift.isCalculatedField = False
        Me.cboABSReportShift.IsSourceFromTable = False
        Me.cboABSReportShift.IsSourceFromValueList = False
        Me.cboABSReportShift.IsUnique = False
        Me.cboABSReportShift.Location = New System.Drawing.Point(305, 18)
        Me.cboABSReportShift.MendatroryField = True
        Me.cboABSReportShift.MyLinkLable1 = Me.MyLabel9
        Me.cboABSReportShift.MyLinkLable2 = Nothing
        Me.cboABSReportShift.Name = "cboABSReportShift"
        Me.cboABSReportShift.ReferenceFieldDesc = Nothing
        Me.cboABSReportShift.ReferenceFieldName = Nothing
        Me.cboABSReportShift.ReferenceTableName = Nothing
        Me.cboABSReportShift.Size = New System.Drawing.Size(111, 20)
        Me.cboABSReportShift.TabIndex = 173
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(262, 20)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel9.TabIndex = 174
        Me.MyLabel9.Text = "Shift"
        '
        'cboABSReportType
        '
        Me.cboABSReportType.AutoCompleteDisplayMember = Nothing
        Me.cboABSReportType.AutoCompleteValueMember = Nothing
        Me.cboABSReportType.CalculationExpression = Nothing
        Me.cboABSReportType.DropDownAnimationEnabled = True
        Me.cboABSReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboABSReportType.FieldCode = Nothing
        Me.cboABSReportType.FieldDesc = Nothing
        Me.cboABSReportType.FieldMaxLength = 0
        Me.cboABSReportType.FieldName = Nothing
        Me.cboABSReportType.isCalculatedField = False
        Me.cboABSReportType.IsSourceFromTable = False
        Me.cboABSReportType.IsSourceFromValueList = False
        Me.cboABSReportType.IsUnique = False
        Me.cboABSReportType.Location = New System.Drawing.Point(62, 18)
        Me.cboABSReportType.MendatroryField = True
        Me.cboABSReportType.MyLinkLable1 = Me.RadLabel8
        Me.cboABSReportType.MyLinkLable2 = Nothing
        Me.cboABSReportType.Name = "cboABSReportType"
        Me.cboABSReportType.ReferenceFieldDesc = Nothing
        Me.cboABSReportType.ReferenceFieldName = Nothing
        Me.cboABSReportType.ReferenceTableName = Nothing
        Me.cboABSReportType.Size = New System.Drawing.Size(194, 20)
        Me.cboABSReportType.TabIndex = 171
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel8.Location = New System.Drawing.Point(5, 20)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(31, 16)
        Me.RadLabel8.TabIndex = 172
        Me.RadLabel8.Text = "Type"
        '
        'txtABSDateToTime
        '
        Me.txtABSDateToTime.CustomFormat = "HH:mm"
        Me.txtABSDateToTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtABSDateToTime.Location = New System.Drawing.Point(351, 40)
        Me.txtABSDateToTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtABSDateToTime.Name = "txtABSDateToTime"
        Me.txtABSDateToTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtABSDateToTime.ShowCheckBox = True
        Me.txtABSDateToTime.ShowUpDown = True
        Me.txtABSDateToTime.Size = New System.Drawing.Size(65, 20)
        Me.txtABSDateToTime.TabIndex = 170
        Me.txtABSDateToTime.TabStop = False
        Me.txtABSDateToTime.Text = "09:30"
        Me.txtABSDateToTime.Value = New Date(2011, 5, 3, 9, 30, 0, 0)
        '
        'txtABSDateFromTime
        '
        Me.txtABSDateFromTime.CustomFormat = "HH:mm"
        Me.txtABSDateFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtABSDateFromTime.Location = New System.Drawing.Point(229, 40)
        Me.txtABSDateFromTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtABSDateFromTime.Name = "txtABSDateFromTime"
        Me.txtABSDateFromTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtABSDateFromTime.ShowCheckBox = True
        Me.txtABSDateFromTime.ShowUpDown = True
        Me.txtABSDateFromTime.Size = New System.Drawing.Size(66, 20)
        Me.txtABSDateFromTime.TabIndex = 169
        Me.txtABSDateFromTime.TabStop = False
        Me.txtABSDateFromTime.Text = "06:00"
        Me.txtABSDateFromTime.Value = New Date(2011, 5, 3, 6, 0, 0, 0)
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(293, 42)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel4.TabIndex = 168
        Me.MyLabel4.Text = "To Time "
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(167, 42)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel5.TabIndex = 167
        Me.MyLabel5.Text = "From Time "
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(5, 42)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel7.TabIndex = 160
        Me.MyLabel7.Text = "Date"
        '
        'txtABSDate
        '
        Me.txtABSDate.CustomFormat = "dd/MM/yyyy"
        Me.txtABSDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtABSDate.Location = New System.Drawing.Point(62, 40)
        Me.txtABSDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtABSDate.Name = "txtABSDate"
        Me.txtABSDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtABSDate.Size = New System.Drawing.Size(100, 20)
        Me.txtABSDate.TabIndex = 159
        Me.txtABSDate.TabStop = False
        Me.txtABSDate.Text = "03/05/2011"
        Me.txtABSDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton4.Location = New System.Drawing.Point(344, 127)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(71, 22)
        Me.RadButton4.TabIndex = 156
        Me.RadButton4.Text = "Print"
        '
        'RGBTSPrint
        '
        Me.RGBTSPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RGBTSPrint.Controls.Add(Me.cmbGatePassType)
        Me.RGBTSPrint.Controls.Add(Me.lblGatePassType)
        Me.RGBTSPrint.Controls.Add(Me.txtRouteNo)
        Me.RGBTSPrint.Controls.Add(Me.btn_CancelTruckSheet)
        Me.RGBTSPrint.Controls.Add(Me.TxtMultiDistributor)
        Me.RGBTSPrint.Controls.Add(Me.MyLabel16)
        Me.RGBTSPrint.Controls.Add(Me.btn_DistributorTS)
        Me.RGBTSPrint.Controls.Add(Me.btn_PrintGatePass)
        Me.RGBTSPrint.Controls.Add(Me.btn_TruckSheetGenerated)
        Me.RGBTSPrint.Controls.Add(Me.TxtMultiCustomerCategory)
        Me.RGBTSPrint.Controls.Add(Me.MyLabel10)
        Me.RGBTSPrint.Controls.Add(Me.btnDotMatrixPrinter)
        Me.RGBTSPrint.Controls.Add(Me.dtpToTime)
        Me.RGBTSPrint.Controls.Add(Me.dtpFromTime)
        Me.RGBTSPrint.Controls.Add(Me.MyLabel3)
        Me.RGBTSPrint.Controls.Add(Me.MyLabel2)
        Me.RGBTSPrint.Controls.Add(Me.btnordersheetprint)
        Me.RGBTSPrint.Controls.Add(Me.lblRouteNo)
        Me.RGBTSPrint.Controls.Add(Me.TSP_Reset)
        Me.RGBTSPrint.Controls.Add(Me.lblVehicleNo)
        Me.RGBTSPrint.Controls.Add(Me.lbl_tsp_Date)
        Me.RGBTSPrint.Controls.Add(Me.TSP_Date)
        Me.RGBTSPrint.Controls.Add(Me.txtLorryNo)
        Me.RGBTSPrint.Controls.Add(Me.lbl_Vehicle)
        Me.RGBTSPrint.Controls.Add(Me.btn_TSPrint)
        Me.RGBTSPrint.HeaderText = "Truck Sheet Print"
        Me.RGBTSPrint.Location = New System.Drawing.Point(12, 138)
        Me.RGBTSPrint.Name = "RGBTSPrint"
        Me.RGBTSPrint.Size = New System.Drawing.Size(421, 213)
        Me.RGBTSPrint.TabIndex = 409
        Me.RGBTSPrint.Text = "Truck Sheet Print"
        '
        'cmbGatePassType
        '
        Me.cmbGatePassType.AutoCompleteDisplayMember = Nothing
        Me.cmbGatePassType.AutoCompleteValueMember = Nothing
        Me.cmbGatePassType.CalculationExpression = Nothing
        Me.cmbGatePassType.DropDownAnimationEnabled = True
        Me.cmbGatePassType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbGatePassType.FieldCode = Nothing
        Me.cmbGatePassType.FieldDesc = Nothing
        Me.cmbGatePassType.FieldMaxLength = 0
        Me.cmbGatePassType.FieldName = Nothing
        Me.cmbGatePassType.isCalculatedField = False
        Me.cmbGatePassType.IsSourceFromTable = False
        Me.cmbGatePassType.IsSourceFromValueList = False
        Me.cmbGatePassType.IsUnique = False
        RadListDataItem10.Text = "Select"
        RadListDataItem11.Text = "AM"
        RadListDataItem12.Text = "PM"
        Me.cmbGatePassType.Items.Add(RadListDataItem10)
        Me.cmbGatePassType.Items.Add(RadListDataItem11)
        Me.cmbGatePassType.Items.Add(RadListDataItem12)
        Me.cmbGatePassType.Location = New System.Drawing.Point(121, 115)
        Me.cmbGatePassType.MendatroryField = False
        Me.cmbGatePassType.MyLinkLable1 = Nothing
        Me.cmbGatePassType.MyLinkLable2 = Nothing
        Me.cmbGatePassType.Name = "cmbGatePassType"
        Me.cmbGatePassType.ReferenceFieldDesc = Nothing
        Me.cmbGatePassType.ReferenceFieldName = Nothing
        Me.cmbGatePassType.ReferenceTableName = Nothing
        Me.cmbGatePassType.Size = New System.Drawing.Size(59, 20)
        Me.cmbGatePassType.TabIndex = 1500
        '
        'lblGatePassType
        '
        Me.lblGatePassType.FieldName = Nothing
        Me.lblGatePassType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGatePassType.Location = New System.Drawing.Point(17, 117)
        Me.lblGatePassType.Name = "lblGatePassType"
        Me.lblGatePassType.Size = New System.Drawing.Size(87, 16)
        Me.lblGatePassType.TabIndex = 1499
        Me.lblGatePassType.Text = "Gate Pass Type"
        '
        'txtRouteNo
        '
        Me.txtRouteNo.arrDispalyMember = Nothing
        Me.txtRouteNo.arrValueMember = Nothing
        Me.txtRouteNo.Location = New System.Drawing.Point(83, 66)
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNo.MyLinkLable1 = Nothing
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyNullText = "All"
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.Size = New System.Drawing.Size(332, 19)
        Me.txtRouteNo.TabIndex = 412
        '
        'btn_CancelTruckSheet
        '
        Me.btn_CancelTruckSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_CancelTruckSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_CancelTruckSheet.Location = New System.Drawing.Point(293, 163)
        Me.btn_CancelTruckSheet.Name = "btn_CancelTruckSheet"
        Me.btn_CancelTruckSheet.Size = New System.Drawing.Size(123, 22)
        Me.btn_CancelTruckSheet.TabIndex = 349
        Me.btn_CancelTruckSheet.Text = "Cancel Truck Sheet"
        '
        'TxtMultiDistributor
        '
        Me.TxtMultiDistributor.arrDispalyMember = Nothing
        Me.TxtMultiDistributor.arrValueMember = Nothing
        Me.TxtMultiDistributor.Location = New System.Drawing.Point(83, 188)
        Me.TxtMultiDistributor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiDistributor.MyLinkLable1 = Nothing
        Me.TxtMultiDistributor.MyLinkLable2 = Nothing
        Me.TxtMultiDistributor.MyNullText = "All"
        Me.TxtMultiDistributor.Name = "TxtMultiDistributor"
        Me.TxtMultiDistributor.Size = New System.Drawing.Size(204, 19)
        Me.TxtMultiDistributor.TabIndex = 348
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(17, 188)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(60, 18)
        Me.MyLabel16.TabIndex = 347
        Me.MyLabel16.Text = "Distributor"
        '
        'btn_DistributorTS
        '
        Me.btn_DistributorTS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_DistributorTS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_DistributorTS.Location = New System.Drawing.Point(293, 187)
        Me.btn_DistributorTS.Name = "btn_DistributorTS"
        Me.btn_DistributorTS.Size = New System.Drawing.Size(123, 22)
        Me.btn_DistributorTS.TabIndex = 346
        Me.btn_DistributorTS.Text = "Distributor Truck Sheet"
        '
        'btn_PrintGatePass
        '
        Me.btn_PrintGatePass.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_PrintGatePass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_PrintGatePass.Location = New System.Drawing.Point(160, 140)
        Me.btn_PrintGatePass.Name = "btn_PrintGatePass"
        Me.btn_PrintGatePass.Size = New System.Drawing.Size(127, 22)
        Me.btn_PrintGatePass.TabIndex = 345
        Me.btn_PrintGatePass.Text = "Print Gate Pass"
        '
        'btn_TruckSheetGenerated
        '
        Me.btn_TruckSheetGenerated.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_TruckSheetGenerated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_TruckSheetGenerated.Location = New System.Drawing.Point(293, 140)
        Me.btn_TruckSheetGenerated.Name = "btn_TruckSheetGenerated"
        Me.btn_TruckSheetGenerated.Size = New System.Drawing.Size(123, 22)
        Me.btn_TruckSheetGenerated.TabIndex = 344
        Me.btn_TruckSheetGenerated.Text = "Generate Truck Sheet"
        '
        'TxtMultiCustomerCategory
        '
        Me.TxtMultiCustomerCategory.arrDispalyMember = Nothing
        Me.TxtMultiCustomerCategory.arrValueMember = Nothing
        Me.TxtMultiCustomerCategory.Location = New System.Drawing.Point(121, 90)
        Me.TxtMultiCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCustomerCategory.MyLinkLable1 = Nothing
        Me.TxtMultiCustomerCategory.MyLinkLable2 = Nothing
        Me.TxtMultiCustomerCategory.MyNullText = "All"
        Me.TxtMultiCustomerCategory.Name = "TxtMultiCustomerCategory"
        Me.TxtMultiCustomerCategory.Size = New System.Drawing.Size(295, 19)
        Me.TxtMultiCustomerCategory.TabIndex = 343
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(17, 91)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel10.TabIndex = 342
        Me.MyLabel10.Text = "Customer Category"
        '
        'btnDotMatrixPrinter
        '
        Me.btnDotMatrixPrinter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDotMatrixPrinter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDotMatrixPrinter.Location = New System.Drawing.Point(17, 140)
        Me.btnDotMatrixPrinter.Name = "btnDotMatrixPrinter"
        Me.btnDotMatrixPrinter.Size = New System.Drawing.Size(137, 22)
        Me.btnDotMatrixPrinter.TabIndex = 171
        Me.btnDotMatrixPrinter.Text = "Print By Dot Matrix Printer"
        '
        'dtpToTime
        '
        Me.dtpToTime.CustomFormat = "HH:mm"
        Me.dtpToTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToTime.Location = New System.Drawing.Point(351, 22)
        Me.dtpToTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToTime.Name = "dtpToTime"
        Me.dtpToTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToTime.ShowCheckBox = True
        Me.dtpToTime.ShowUpDown = True
        Me.dtpToTime.Size = New System.Drawing.Size(65, 20)
        Me.dtpToTime.TabIndex = 170
        Me.dtpToTime.TabStop = False
        Me.dtpToTime.Text = "09:30"
        Me.dtpToTime.Value = New Date(2011, 5, 3, 9, 30, 0, 0)
        '
        'dtpFromTime
        '
        Me.dtpFromTime.CustomFormat = "HH:mm"
        Me.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromTime.Location = New System.Drawing.Point(240, 22)
        Me.dtpFromTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromTime.Name = "dtpFromTime"
        Me.dtpFromTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromTime.ShowCheckBox = True
        Me.dtpFromTime.ShowUpDown = True
        Me.dtpFromTime.Size = New System.Drawing.Size(66, 20)
        Me.dtpFromTime.TabIndex = 169
        Me.dtpFromTime.TabStop = False
        Me.dtpFromTime.Text = "06:00"
        Me.dtpFromTime.Value = New Date(2011, 5, 3, 6, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(305, 25)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel3.TabIndex = 168
        Me.MyLabel3.Text = "To Time "
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(178, 25)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel2.TabIndex = 167
        Me.MyLabel2.Text = "From Time "
        '
        'btnordersheetprint
        '
        Me.btnordersheetprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnordersheetprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnordersheetprint.Location = New System.Drawing.Point(301, 114)
        Me.btnordersheetprint.Name = "btnordersheetprint"
        Me.btnordersheetprint.Size = New System.Drawing.Size(115, 22)
        Me.btnordersheetprint.TabIndex = 166
        Me.btnordersheetprint.Text = "Order Sheet Print"
        '
        'lblRouteNo
        '
        Me.lblRouteNo.FieldName = Nothing
        Me.lblRouteNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteNo.Location = New System.Drawing.Point(17, 69)
        Me.lblRouteNo.Name = "lblRouteNo"
        Me.lblRouteNo.Size = New System.Drawing.Size(54, 16)
        Me.lblRouteNo.TabIndex = 163
        Me.lblRouteNo.Text = "Route No"
        '
        'TSP_Reset
        '
        Me.TSP_Reset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TSP_Reset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TSP_Reset.Location = New System.Drawing.Point(184, 114)
        Me.TSP_Reset.Name = "TSP_Reset"
        Me.TSP_Reset.Size = New System.Drawing.Size(52, 22)
        Me.TSP_Reset.TabIndex = 162
        Me.TSP_Reset.Text = "Reset"
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.CalculationExpression = Nothing
        Me.lblVehicleNo.FieldCode = Nothing
        Me.lblVehicleNo.FieldDesc = Nothing
        Me.lblVehicleNo.FieldMaxLength = 0
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVehicleNo.isCalculatedField = False
        Me.lblVehicleNo.IsSourceFromTable = False
        Me.lblVehicleNo.IsSourceFromValueList = False
        Me.lblVehicleNo.IsUnique = False
        Me.lblVehicleNo.Location = New System.Drawing.Point(178, 45)
        Me.lblVehicleNo.MaxLength = 200
        Me.lblVehicleNo.MendatroryField = False
        Me.lblVehicleNo.MyLinkLable1 = Nothing
        Me.lblVehicleNo.MyLinkLable2 = Nothing
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.ReferenceFieldDesc = Nothing
        Me.lblVehicleNo.ReferenceFieldName = Nothing
        Me.lblVehicleNo.ReferenceTableName = Nothing
        Me.lblVehicleNo.Size = New System.Drawing.Size(238, 18)
        Me.lblVehicleNo.TabIndex = 161
        '
        'lbl_tsp_Date
        '
        Me.lbl_tsp_Date.FieldName = Nothing
        Me.lbl_tsp_Date.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_tsp_Date.Location = New System.Drawing.Point(17, 25)
        Me.lbl_tsp_Date.Name = "lbl_tsp_Date"
        Me.lbl_tsp_Date.Size = New System.Drawing.Size(30, 16)
        Me.lbl_tsp_Date.TabIndex = 160
        Me.lbl_tsp_Date.Text = "Date"
        '
        'TSP_Date
        '
        Me.TSP_Date.CustomFormat = "dd/MM/yyyy"
        Me.TSP_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TSP_Date.Location = New System.Drawing.Point(84, 21)
        Me.TSP_Date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TSP_Date.Name = "TSP_Date"
        Me.TSP_Date.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.TSP_Date.Size = New System.Drawing.Size(92, 20)
        Me.TSP_Date.TabIndex = 159
        Me.TSP_Date.TabStop = False
        Me.TSP_Date.Text = "03/05/2011"
        Me.TSP_Date.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtLorryNo
        '
        Me.txtLorryNo.CalculationExpression = Nothing
        Me.txtLorryNo.FieldCode = Nothing
        Me.txtLorryNo.FieldDesc = Nothing
        Me.txtLorryNo.FieldMaxLength = 0
        Me.txtLorryNo.FieldName = Nothing
        Me.txtLorryNo.isCalculatedField = False
        Me.txtLorryNo.IsSourceFromTable = False
        Me.txtLorryNo.IsSourceFromValueList = False
        Me.txtLorryNo.IsUnique = False
        Me.txtLorryNo.Location = New System.Drawing.Point(84, 44)
        Me.txtLorryNo.MendatroryField = False
        Me.txtLorryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLorryNo.MyLinkLable1 = Me.lbl_Vehicle
        Me.txtLorryNo.MyLinkLable2 = Nothing
        Me.txtLorryNo.MyReadOnly = False
        Me.txtLorryNo.MyShowMasterFormButton = False
        Me.txtLorryNo.Name = "txtLorryNo"
        Me.txtLorryNo.ReferenceFieldDesc = Nothing
        Me.txtLorryNo.ReferenceFieldName = Nothing
        Me.txtLorryNo.ReferenceTableName = Nothing
        Me.txtLorryNo.Size = New System.Drawing.Size(92, 19)
        Me.txtLorryNo.TabIndex = 158
        Me.txtLorryNo.Value = ""
        '
        'lbl_Vehicle
        '
        Me.lbl_Vehicle.FieldName = Nothing
        Me.lbl_Vehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Vehicle.Location = New System.Drawing.Point(17, 47)
        Me.lbl_Vehicle.Name = "lbl_Vehicle"
        Me.lbl_Vehicle.Size = New System.Drawing.Size(61, 16)
        Me.lbl_Vehicle.TabIndex = 157
        Me.lbl_Vehicle.Text = "Vehicle No"
        '
        'btn_TSPrint
        '
        Me.btn_TSPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_TSPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_TSPrint.Location = New System.Drawing.Point(240, 114)
        Me.btn_TSPrint.Name = "btn_TSPrint"
        Me.btn_TSPrint.Size = New System.Drawing.Size(55, 22)
        Me.btn_TSPrint.TabIndex = 156
        Me.btn_TSPrint.Text = "Print"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 3)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(406, 42)
        Me.RadGroupBox3.TabIndex = 54
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(186, 16)
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
        Me.ToDate.CustomFormat = "dd/MM/yyyy hh:mmtt"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(214, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(135, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011 11:59PM"
        Me.ToDate.Value = New Date(2011, 10, 24, 23, 59, 0, 0)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(132, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "03/05/2011 12:00 AM"
        Me.fromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(73, 98)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(345, 19)
        Me.txtRoute.TabIndex = 411
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(17, 99)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel1.TabIndex = 410
        Me.MyLabel1.Text = "Route"
        '
        'txtMultLocation
        '
        Me.txtMultLocation.arrDispalyMember = Nothing
        Me.txtMultLocation.arrValueMember = Nothing
        Me.txtMultLocation.Location = New System.Drawing.Point(73, 74)
        Me.txtMultLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultLocation.MyLinkLable1 = Nothing
        Me.txtMultLocation.MyLinkLable2 = Nothing
        Me.txtMultLocation.MyNullText = "All"
        Me.txtMultLocation.Name = "txtMultLocation"
        Me.txtMultLocation.Size = New System.Drawing.Size(345, 19)
        Me.txtMultLocation.TabIndex = 408
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(17, 75)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 407
        Me.lblLocation.Text = "Location"
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(17, 51)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 405
        Me.lblCustomer.Text = "Customer"
        '
        'txtMultCustomer
        '
        Me.txtMultCustomer.arrDispalyMember = Nothing
        Me.txtMultCustomer.arrValueMember = Nothing
        Me.txtMultCustomer.Location = New System.Drawing.Point(73, 50)
        Me.txtMultCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultCustomer.MyLinkLable1 = Nothing
        Me.txtMultCustomer.MyLinkLable2 = Nothing
        Me.txtMultCustomer.MyNullText = "All"
        Me.txtMultCustomer.Name = "txtMultCustomer"
        Me.txtMultCustomer.Size = New System.Drawing.Size(345, 19)
        Me.txtMultCustomer.TabIndex = 406
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(849, 502)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Gv1.ForeColor = System.Drawing.Color.Black
        Me.Gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.EnableFiltering = True
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ReadOnly = True
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(849, 502)
        Me.Gv1.TabIndex = 1
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.PDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(151, 9)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 158
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
        Me.btnClose.Location = New System.Drawing.Point(795, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 157
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(3, 9)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 155
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(80, 9)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 156
        Me.btnReset.Text = "Reset"
        '
        'rptDairyTruckSheetReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 601)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptDairyTruckSheetReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Dairy Booking Distributor Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.btnPrintInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInvFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShowEarlyRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbInstitutionDailySalesReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbInstitutionDailySalesReport.ResumeLayout(False)
        Me.gbInstitutionDailySalesReport.PerformLayout()
        CType(Me.btnIDSPrintABS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpIDStodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpIDSfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCustomerCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIDSCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblIDSItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnIDSPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbNetSalesReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbNetSalesReport.ResumeLayout(False)
        Me.gbNetSalesReport.PerformLayout()
        CType(Me.chkConsiderShowEarlyRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpNSRToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNSRType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpNSRFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNSRPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboABSReportShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboABSReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtABSDateToTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtABSDateFromTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtABSDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RGBTSPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RGBTSPrint.ResumeLayout(False)
        Me.RGBTSPrint.PerformLayout()
        CType(Me.cmbGatePassType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGatePassType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_CancelTruckSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_DistributorTS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_PrintGatePass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_TruckSheetGenerated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDotMatrixPrinter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnordersheetprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TSP_Reset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_tsp_Date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TSP_Date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl_Vehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_TSPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtMultLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMultCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents RGBTSPrint As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btn_TSPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbl_Vehicle As common.Controls.MyLabel
    Friend WithEvents txtLorryNo As common.UserControls.txtFinder
    Friend WithEvents lbl_tsp_Date As common.Controls.MyLabel
    Friend WithEvents TSP_Date As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents lblVehicleNo As common.Controls.MyTextBox
    Friend WithEvents TSP_Reset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblRouteNo As common.Controls.MyLabel
    Friend WithEvents btnordersheetprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents dtpToTime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromTime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnDotMatrixPrinter As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtABSDateToTime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtABSDateFromTime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtABSDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboABSReportShift As common.Controls.MyComboBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents cboABSReportType As common.Controls.MyComboBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents txtABSZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtABSLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents btn_TruckSheetGenerated As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtMultiCustomerCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btn_PrintGatePass As Telerik.WinControls.UI.RadButton
    Friend WithEvents gbNetSalesReport As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpNSRToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtNSRZone As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtNSRLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents cboNSRType As common.Controls.MyComboBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents dtpNSRFromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnNSRPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents gbInstitutionDailySalesReport As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnIDSPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblIDSCustomer As common.Controls.MyLabel
    Friend WithEvents txtIDSCustomer As common.UserControls.txtFinder
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents lblIDSItem As common.Controls.MyLabel
    Friend WithEvents txtIDSItem As common.UserControls.txtFinder
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents btn_DistributorTS As Telerik.WinControls.UI.RadButton
    Friend WithEvents TxtMultiDistributor As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents btn_CancelTruckSheet As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtRouteNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkShowEarlyRoute As RadCheckBox
    Friend WithEvents txtRouteABS As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtRouteNetSales As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents chkConsiderShowEarlyRoute As RadCheckBox
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents cmbCustomerCategory As common.Controls.MyComboBox
    Friend WithEvents dtpIDStodate As RadDateTimePicker
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents dtpIDSfromdate As RadDateTimePicker
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents btnIDSPrintABS As RadButton
    Friend WithEvents cmbGatePassType As common.Controls.MyComboBox
    Friend WithEvents lblGatePassType As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents txtInvMultiCust As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents MyLabel25 As common.Controls.MyLabel
    Friend WithEvents txtInvToDate As RadDateTimePicker
    Friend WithEvents txtInvFromDate As RadDateTimePicker
    Friend WithEvents btnPrintInvoice As RadButton
End Class

