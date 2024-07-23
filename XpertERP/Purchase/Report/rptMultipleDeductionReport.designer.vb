Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptMultipleDeductionReport
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnDetail = New common.Controls.MyRadioButton()
        Me.rbtnSummary = New common.Controls.MyRadioButton()
        Me.lblArea = New common.Controls.MyLabel()
        Me.fndArea = New common.UserControls.txtFinder()
        Me.TxtItem = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.chkItemWise = New Telerik.WinControls.UI.RadCheckBox()
        Me.ddlReportType = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblType = New common.Controls.MyLabel()
        Me.TxtDeductionCode = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtMultiVSP = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(785, 20)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(785, 382)
        Me.SplitContainer1.SplitterDistance = 340
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(785, 340)
        Me.RadPageView1.TabIndex = 11
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblArea)
        Me.RadPageViewPage1.Controls.Add(Me.fndArea)
        Me.RadPageViewPage1.Controls.Add(Me.TxtItem)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.chkItemWise)
        Me.RadPageViewPage1.Controls.Add(Me.ddlReportType)
        Me.RadPageViewPage1.Controls.Add(Me.lblType)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDeductionCode)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiVSP)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(764, 292)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnDetail)
        Me.GroupBox1.Controls.Add(Me.rbtnSummary)
        Me.GroupBox1.Location = New System.Drawing.Point(443, 20)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(161, 35)
        Me.GroupBox1.TabIndex = 1514
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Visible = False
        '
        'rbtnDetail
        '
        Me.rbtnDetail.Location = New System.Drawing.Point(82, 8)
        Me.rbtnDetail.MyLinkLable1 = Nothing
        Me.rbtnDetail.MyLinkLable2 = Nothing
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 1
        Me.rbtnDetail.TabStop = False
        Me.rbtnDetail.Text = "Detail"
        '
        'rbtnSummary
        '
        Me.rbtnSummary.Location = New System.Drawing.Point(6, 9)
        Me.rbtnSummary.MyLinkLable1 = Nothing
        Me.rbtnSummary.MyLinkLable2 = Nothing
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 0
        Me.rbtnSummary.TabStop = False
        Me.rbtnSummary.Text = "Summary"
        '
        'lblArea
        '
        Me.lblArea.FieldName = Nothing
        Me.lblArea.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArea.Location = New System.Drawing.Point(21, 70)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(30, 16)
        Me.lblArea.TabIndex = 1501
        Me.lblArea.Text = "Area"
        '
        'fndArea
        '
        Me.fndArea.CalculationExpression = Nothing
        Me.fndArea.FieldCode = Nothing
        Me.fndArea.FieldDesc = Nothing
        Me.fndArea.FieldMaxLength = 0
        Me.fndArea.FieldName = Nothing
        Me.fndArea.isCalculatedField = False
        Me.fndArea.IsSourceFromTable = False
        Me.fndArea.IsSourceFromValueList = False
        Me.fndArea.IsUnique = False
        Me.fndArea.Location = New System.Drawing.Point(107, 69)
        Me.fndArea.MendatroryField = True
        Me.fndArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndArea.MyLinkLable1 = Nothing
        Me.fndArea.MyLinkLable2 = Nothing
        Me.fndArea.MyReadOnly = False
        Me.fndArea.MyShowMasterFormButton = False
        Me.fndArea.Name = "fndArea"
        Me.fndArea.ReferenceFieldDesc = Nothing
        Me.fndArea.ReferenceFieldName = Nothing
        Me.fndArea.ReferenceTableName = Nothing
        Me.fndArea.Size = New System.Drawing.Size(344, 18)
        Me.fndArea.TabIndex = 1500
        Me.fndArea.Value = ""
        '
        'TxtItem
        '
        Me.TxtItem.arrDispalyMember = Nothing
        Me.TxtItem.arrValueMember = Nothing
        Me.TxtItem.Location = New System.Drawing.Point(107, 156)
        Me.TxtItem.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.MyLinkLable1 = Me.MyLabel1
        Me.TxtItem.MyLinkLable2 = Nothing
        Me.TxtItem.MyNullText = "All"
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(344, 19)
        Me.TxtItem.TabIndex = 430
        Me.TxtItem.Visible = False
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(21, 156)
        Me.MyLabel1.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel1.TabIndex = 431
        Me.MyLabel1.Text = "Item Code"
        Me.MyLabel1.Visible = False
        '
        'chkItemWise
        '
        Me.chkItemWise.Location = New System.Drawing.Point(366, 31)
        Me.chkItemWise.Margin = New System.Windows.Forms.Padding(4)
        Me.chkItemWise.Name = "chkItemWise"
        Me.chkItemWise.Size = New System.Drawing.Size(70, 18)
        Me.chkItemWise.TabIndex = 429
        Me.chkItemWise.Text = "Item Wise"
        '
        'ddlReportType
        '
        Me.ddlReportType.AutoCompleteDisplayMember = Nothing
        Me.ddlReportType.AutoCompleteValueMember = Nothing
        Me.ddlReportType.DropDownAnimationEnabled = True
        RadListDataItem1.Text = "Both"
        RadListDataItem2.Text = "Sale Invoice"
        RadListDataItem3.Text = "Sale Return"
        Me.ddlReportType.Items.Add(RadListDataItem1)
        Me.ddlReportType.Items.Add(RadListDataItem2)
        Me.ddlReportType.Items.Add(RadListDataItem3)
        Me.ddlReportType.Location = New System.Drawing.Point(97, 179)
        Me.ddlReportType.Margin = New System.Windows.Forms.Padding(4)
        Me.ddlReportType.Name = "ddlReportType"
        Me.ddlReportType.Size = New System.Drawing.Size(240, 20)
        Me.ddlReportType.TabIndex = 427
        Me.ddlReportType.Visible = False
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Location = New System.Drawing.Point(21, 182)
        Me.lblType.Margin = New System.Windows.Forms.Padding(4)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(67, 18)
        Me.lblType.TabIndex = 428
        Me.lblType.Text = "Report Type"
        Me.lblType.Visible = False
        '
        'TxtDeductionCode
        '
        Me.TxtDeductionCode.arrDispalyMember = Nothing
        Me.TxtDeductionCode.arrValueMember = Nothing
        Me.TxtDeductionCode.Location = New System.Drawing.Point(107, 134)
        Me.TxtDeductionCode.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtDeductionCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDeductionCode.MyLinkLable1 = Me.MyLabel10
        Me.TxtDeductionCode.MyLinkLable2 = Nothing
        Me.TxtDeductionCode.MyNullText = "All"
        Me.TxtDeductionCode.Name = "TxtDeductionCode"
        Me.TxtDeductionCode.Size = New System.Drawing.Size(344, 19)
        Me.TxtDeductionCode.TabIndex = 396
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(21, 134)
        Me.MyLabel10.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(87, 18)
        Me.MyLabel10.TabIndex = 397
        Me.MyLabel10.Text = "Deduction Code"
        '
        'txtMultiVSP
        '
        Me.txtMultiVSP.arrDispalyMember = Nothing
        Me.txtMultiVSP.arrValueMember = Nothing
        Me.txtMultiVSP.Location = New System.Drawing.Point(107, 90)
        Me.txtMultiVSP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMultiVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiVSP.MyLinkLable1 = Me.MyLabel13
        Me.txtMultiVSP.MyLinkLable2 = Nothing
        Me.txtMultiVSP.MyNullText = "All"
        Me.txtMultiVSP.Name = "txtMultiVSP"
        Me.txtMultiVSP.Size = New System.Drawing.Size(344, 19)
        Me.txtMultiVSP.TabIndex = 395
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(21, 90)
        Me.MyLabel13.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel13.TabIndex = 394
        Me.MyLabel13.Text = "VSP Code"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(21, 112)
        Me.MyLabel3.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel3.TabIndex = 389
        Me.MyLabel3.Text = "Location"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(107, 112)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel3
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(344, 19)
        Me.txtLocation.TabIndex = 388
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(21, 11)
        Me.RadGroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox3.Size = New System.Drawing.Size(337, 52)
        Me.RadGroupBox3.TabIndex = 53
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(173, 20)
        Me.RadLabel2.Margin = New System.Windows.Forms.Padding(4)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(7, 20)
        Me.RadLabel1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(209, 18)
        Me.ToDate.Margin = New System.Windows.Forms.Padding(4)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(104, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(59, 18)
        Me.fromDate.Margin = New System.Windows.Forms.Padding(4)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(104, 20)
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
        Me.RadPageViewPage2.Size = New System.Drawing.Size(764, 292)
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
        Me.Gv1.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(764, 292)
        Me.Gv1.TabIndex = 0
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(203, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(95, 27)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'RadSplitExp
        '
        Me.RadSplitExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.RadSplitExp.Location = New System.Drawing.Point(301, 6)
        Me.RadSplitExp.Margin = New System.Windows.Forms.Padding(4)
        Me.RadSplitExp.Name = "RadSplitExp"
        Me.RadSplitExp.Size = New System.Drawing.Size(127, 27)
        Me.RadSplitExp.TabIndex = 155
        Me.RadSplitExp.Text = "Export"
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
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(669, 6)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(112, 27)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(7, 6)
        Me.btnGo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(95, 27)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(105, 6)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(95, 27)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'rptMultipleDeductionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 402)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "rptMultipleDeductionReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Multiple Deduction Report"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadSplitExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtMultiVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents TxtDeductionCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents ddlReportType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents chkItemWise As RadCheckBox
    Friend WithEvents TxtItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents lblArea As common.Controls.MyLabel
    Friend WithEvents fndArea As common.UserControls.txtFinder
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnDetail As common.Controls.MyRadioButton
    Friend WithEvents rbtnSummary As common.Controls.MyRadioButton
End Class

