<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmStoresLedgerNew
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.tvCategory = New Telerik.WinControls.UI.RadTreeView
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbtnPurchase = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnSRN = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.cbgItem1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkItemAll = New common.Controls.MyRadioButton
        Me.chkDetailed = New Telerik.WinControls.UI.RadCheckBox
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbtnSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.chkWithoutVal = New Telerik.WinControls.UI.RadCheckBox
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.dtpFrmDate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gvReport = New common.UserControls.MyRadGridView
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSRN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cbgItem1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDetailed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkWithoutVal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(835, 526)
        Me.SplitContainer1.SplitterDistance = 497
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(835, 497)
        Me.RadPageView1.TabIndex = 316
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.cbgItem1)
        Me.RadPageViewPage1.Controls.Add(Me.chkDetailed)
        Me.RadPageViewPage1.Controls.Add(Me.rdbDetail)
        Me.RadPageViewPage1.Controls.Add(Me.rdbtnSummary)
        Me.RadPageViewPage1.Controls.Add(Me.chkWithoutVal)
        Me.RadPageViewPage1.Controls.Add(Me.dtpToDate)
        Me.RadPageViewPage1.Controls.Add(Me.dtpFrmDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(814, 449)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.tvCategory)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Category"
        Me.RadGroupBox5.Location = New System.Drawing.Point(410, 50)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(401, 396)
        Me.RadGroupBox5.TabIndex = 319
        Me.RadGroupBox5.Text = "Category"
        '
        'tvCategory
        '
        Me.tvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvCategory.Location = New System.Drawing.Point(10, 40)
        Me.tvCategory.Name = "tvCategory"
        Me.tvCategory.Size = New System.Drawing.Size(381, 346)
        Me.tvCategory.SpacingBetweenNodes = -1
        Me.tvCategory.TabIndex = 70
        Me.tvCategory.Text = "RadTreeView1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnCategorySelect)
        Me.Panel3.Controls.Add(Me.rbtnCategoryAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(381, 20)
        Me.Panel3.TabIndex = 1
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(187, 1)
        Me.rbtnCategorySelect.MyLinkLable1 = Nothing
        Me.rbtnCategorySelect.MyLinkLable2 = Nothing
        Me.rbtnCategorySelect.Name = "rbtnCategorySelect"
        Me.rbtnCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCategorySelect.TabIndex = 1
        Me.rbtnCategorySelect.Text = "Select"
        '
        'rbtnCategoryAll
        '
        Me.rbtnCategoryAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(136, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 0
        Me.rbtnCategoryAll.Text = "All"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnPurchase)
        Me.GroupBox1.Controls.Add(Me.rbtnSRN)
        Me.GroupBox1.Location = New System.Drawing.Point(663, -8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(151, 46)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'rbtnPurchase
        '
        Me.rbtnPurchase.Location = New System.Drawing.Point(6, 25)
        Me.rbtnPurchase.Name = "rbtnPurchase"
        Me.rbtnPurchase.Size = New System.Drawing.Size(92, 18)
        Me.rbtnPurchase.TabIndex = 27
        Me.rbtnPurchase.Text = "With Purchase"
        '
        'rbtnSRN
        '
        Me.rbtnSRN.Location = New System.Drawing.Point(6, 9)
        Me.rbtnSRN.Name = "rbtnSRN"
        Me.rbtnSRN.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSRN.TabIndex = 26
        Me.rbtnSRN.Text = "With SRN"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(2, 29)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(402, 209)
        Me.RadGroupBox3.TabIndex = 29
        Me.RadGroupBox3.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(382, 159)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocSelect)
        Me.Panel2.Controls.Add(Me.chkLocAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(382, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(187, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(136, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'cbgItem1
        '
        Me.cbgItem1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cbgItem1.Controls.Add(Me.cbgItem)
        Me.cbgItem1.Controls.Add(Me.Panel1)
        Me.cbgItem1.HeaderText = "Item"
        Me.cbgItem1.Location = New System.Drawing.Point(2, 237)
        Me.cbgItem1.Name = "cbgItem1"
        Me.cbgItem1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cbgItem1.Size = New System.Drawing.Size(402, 209)
        Me.cbgItem1.TabIndex = 28
        Me.cbgItem1.Text = "Item"
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
        Me.cbgItem.Size = New System.Drawing.Size(382, 159)
        Me.cbgItem.TabIndex = 2
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkItemSelect)
        Me.Panel1.Controls.Add(Me.chkItemAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(382, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(187, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.Location = New System.Drawing.Point(136, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'chkDetailed
        '
        Me.chkDetailed.Location = New System.Drawing.Point(595, 3)
        Me.chkDetailed.Name = "chkDetailed"
        Me.chkDetailed.Size = New System.Drawing.Size(62, 18)
        Me.chkDetailed.TabIndex = 27
        Me.chkDetailed.Text = "Detailed"
        Me.chkDetailed.Visible = False
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(425, 4)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 26
        Me.rdbDetail.Text = "Detail"
        '
        'rdbtnSummary
        '
        Me.rdbtnSummary.Location = New System.Drawing.Point(334, 4)
        Me.rdbtnSummary.Name = "rdbtnSummary"
        Me.rdbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbtnSummary.TabIndex = 25
        Me.rdbtnSummary.Text = "Summary"
        '
        'chkWithoutVal
        '
        Me.chkWithoutVal.Location = New System.Drawing.Point(492, 4)
        Me.chkWithoutVal.Name = "chkWithoutVal"
        Me.chkWithoutVal.Size = New System.Drawing.Size(92, 18)
        Me.chkWithoutVal.TabIndex = 24
        Me.chkWithoutVal.Text = "Without Value"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(229, 2)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.RadLabel2
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate.TabIndex = 21
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "13-06-2011"
        Me.dtpToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(173, 3)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 22
        Me.RadLabel2.Text = "To Date"
        '
        'dtpFrmDate
        '
        Me.dtpFrmDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrmDate.Location = New System.Drawing.Point(73, 2)
        Me.dtpFrmDate.MendatroryField = False
        Me.dtpFrmDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.MyLinkLable1 = Me.RadLabel1
        Me.dtpFrmDate.MyLinkLable2 = Nothing
        Me.dtpFrmDate.Name = "dtpFrmDate"
        Me.dtpFrmDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpFrmDate.TabIndex = 20
        Me.dtpFrmDate.TabStop = False
        Me.dtpFrmDate.Text = "13-06-2011"
        Me.dtpFrmDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(3, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 23
        Me.RadLabel1.Text = "From Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvReport)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(814, 449)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvReport
        '
        Me.gvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvReport.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvReport.MasterTemplate.AllowAddNewRow = False
        Me.gvReport.MasterTemplate.AllowEditRow = False
        Me.gvReport.MasterTemplate.EnableFiltering = True
        Me.gvReport.Name = "gvReport"
        Me.gvReport.ShowGroupPanel = False
        Me.gvReport.Size = New System.Drawing.Size(814, 449)
        Me.gvReport.TabIndex = 0
        Me.gvReport.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(756, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 32
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(67, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 31
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(3, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(58, 18)
        Me.btnReset.TabIndex = 30
        Me.btnReset.Text = "Reset"
        '
        'FrmStoresLedgerNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(835, 526)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmStoresLedgerNew"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Stores Ledger"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSRN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cbgItem1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDetailed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkWithoutVal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvReport.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvReport As common.UserControls.MyRadGridView
    Friend WithEvents chkDetailed As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkWithoutVal As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpFrmDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents cbgItem1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnPurchase As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSRN As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents tvCategory As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
End Class

