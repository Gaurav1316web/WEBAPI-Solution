<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmStockDispatchReport
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.grpSE = New System.Windows.Forms.GroupBox
        Me.rdbSku = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbFlavour = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbPack = New Telerik.WinControls.UI.RadRadioButton
        Me.grpSelect = New System.Windows.Forms.GroupBox
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.ddlUnitType = New common.Controls.MyComboBox
        Me.Item = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkItemAll = New common.Controls.MyRadioButton
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.txtFromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.btnExport = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnGo = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.grpSE.SuspendLayout()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSelect.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlUnitType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Item, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Item.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(773, 494)
        Me.RadPageView1.TabIndex = 26
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(752, 446)
        Me.RadPageViewPage1.Text = "Filter"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpSE)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpSelect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ddlUnitType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Item)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFromDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Locationgb)
        Me.SplitContainer1.Size = New System.Drawing.Size(752, 446)
        Me.SplitContainer1.SplitterDistance = 245
        Me.SplitContainer1.TabIndex = 22
        '
        'grpSE
        '
        Me.grpSE.Controls.Add(Me.rdbSku)
        Me.grpSE.Controls.Add(Me.rdbFlavour)
        Me.grpSE.Controls.Add(Me.rdbPack)
        Me.grpSE.Location = New System.Drawing.Point(490, 3)
        Me.grpSE.Name = "grpSE"
        Me.grpSE.Size = New System.Drawing.Size(247, 42)
        Me.grpSE.TabIndex = 109
        Me.grpSE.TabStop = False
        Me.grpSE.Text = "Select"
        '
        'rdbSku
        '
        Me.rdbSku.Location = New System.Drawing.Point(3, 18)
        Me.rdbSku.Name = "rdbSku"
        Me.rdbSku.Size = New System.Drawing.Size(68, 18)
        Me.rdbSku.TabIndex = 109
        Me.rdbSku.Text = "SKU Wise"
        '
        'rdbFlavour
        '
        Me.rdbFlavour.Location = New System.Drawing.Point(160, 18)
        Me.rdbFlavour.Name = "rdbFlavour"
        Me.rdbFlavour.Size = New System.Drawing.Size(84, 18)
        Me.rdbFlavour.TabIndex = 111
        Me.rdbFlavour.Text = "Flavour Wise"
        '
        'rdbPack
        '
        Me.rdbPack.Location = New System.Drawing.Point(80, 18)
        Me.rdbPack.Name = "rdbPack"
        Me.rdbPack.Size = New System.Drawing.Size(70, 18)
        Me.rdbPack.TabIndex = 110
        Me.rdbPack.Text = "Pack Wise"
        '
        'grpSelect
        '
        Me.grpSelect.Controls.Add(Me.rdbDetail)
        Me.grpSelect.Controls.Add(Me.rdbSummary)
        Me.grpSelect.Location = New System.Drawing.Point(345, 3)
        Me.grpSelect.Name = "grpSelect"
        Me.grpSelect.Size = New System.Drawing.Size(139, 42)
        Me.grpSelect.TabIndex = 108
        Me.grpSelect.TabStop = False
        Me.grpSelect.Text = "Select"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(83, 18)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 0
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(6, 18)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 103
        Me.rdbSummary.Text = "Summary"
        '
        'ddlUnitType
        '
        Me.ddlUnitType.AllowShowFocusCues = False
        Me.ddlUnitType.AutoCompleteDisplayMember = Nothing
        Me.ddlUnitType.AutoCompleteValueMember = Nothing
        Me.ddlUnitType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlUnitType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Tag = "F"
        RadListDataItem1.Text = "Filled"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Empty"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Both"
        RadListDataItem3.TextWrap = True
        Me.ddlUnitType.Items.Add(RadListDataItem1)
        Me.ddlUnitType.Items.Add(RadListDataItem2)
        Me.ddlUnitType.Items.Add(RadListDataItem3)
        Me.ddlUnitType.Location = New System.Drawing.Point(81, 38)
        Me.ddlUnitType.MendatroryField = False
        Me.ddlUnitType.MyLinkLable1 = Nothing
        Me.ddlUnitType.MyLinkLable2 = Nothing
        Me.ddlUnitType.Name = "ddlUnitType"
        Me.ddlUnitType.Size = New System.Drawing.Size(130, 18)
        Me.ddlUnitType.TabIndex = 75
        '
        'Item
        '
        Me.Item.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Item.Controls.Add(Me.cbgItem)
        Me.Item.Controls.Add(Me.Panel3)
        Me.Item.HeaderText = "Item"
        Me.Item.Location = New System.Drawing.Point(8, 60)
        Me.Item.Name = "Item"
        Me.Item.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Item.Size = New System.Drawing.Size(444, 177)
        Me.Item.TabIndex = 48
        Me.Item.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(424, 127)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkItemSelect)
        Me.Panel3.Controls.Add(Me.chkItemAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(424, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(202, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.Location = New System.Drawing.Point(151, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(8, 36)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(54, 18)
        Me.RadLabel3.TabIndex = 74
        Me.RadLabel3.Text = "Unit Type"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(8, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(209, 8)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(130, 20)
        Me.txtToDate.TabIndex = 73
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011 02:29 AM"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(183, 8)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(43, 8)
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(130, 20)
        Me.txtFromDate.TabIndex = 72
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "24/10/2011 02:29 AM"
        Me.txtFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLocation)
        Me.Locationgb.Controls.Add(Me.Panel2)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(8, 12)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(444, 182)
        Me.Locationgb.TabIndex = 47
        Me.Locationgb.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(424, 132)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(424, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(202, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(151, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(752, 446)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(752, 446)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnExport)
        Me.Panel6.Controls.Add(Me.btnClose)
        Me.Panel6.Controls.Add(Me.btnReset)
        Me.Panel6.Controls.Add(Me.btnGo)
        Me.Panel6.Controls.Add(Me.btnPrint)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 494)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(773, 31)
        Me.Panel6.TabIndex = 27
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(294, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "Export To Excel"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(702, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 24)
        Me.btnClose.TabIndex = 44
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(105, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(84, 24)
        Me.btnReset.TabIndex = 45
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(4, 3)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(95, 24)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = ">>>"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(195, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(93, 24)
        Me.btnPrint.TabIndex = 43
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'FrmStockDispatchReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 525)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel6)
        Me.Name = "FrmStockDispatchReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Stock Dispatch Report"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.grpSE.ResumeLayout(False)
        Me.grpSE.PerformLayout()
        CType(Me.rdbSku, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFlavour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSelect.ResumeLayout(False)
        Me.grpSelect.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlUnitType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Item, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Item.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents grpSelect As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents ddlUnitType As common.Controls.MyComboBox
    Friend WithEvents Item As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents txtFromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbPack As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbFlavour As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSku As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents grpSE As System.Windows.Forms.GroupBox
End Class

