<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExciseSummaryReport
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
        Me.dtEnd = New Telerik.WinControls.UI.RadDateTimePicker
        Me.dtStart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.chkIQtywise = New Telerik.WinControls.UI.RadCheckBox
        Me.chkAllItems = New Telerik.WinControls.UI.RadCheckBox
        Me.rbtnSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.chkImrpWise = New Telerik.WinControls.UI.RadCheckBox
        Me.chkCHapterWise = New Telerik.WinControls.UI.RadCheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbothers = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbFinishGoods = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadButton
        Me.btnReferesh = New Telerik.WinControls.UI.RadButton
        CType(Me.dtEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIQtywise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkImrpWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCHapterWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rdbothers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbFinishGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReferesh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtEnd
        '
        Me.dtEnd.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEnd.Location = New System.Drawing.Point(51, 44)
        Me.dtEnd.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtEnd.Size = New System.Drawing.Size(135, 20)
        Me.dtEnd.TabIndex = 1
        Me.dtEnd.TabStop = False
        Me.dtEnd.Text = "19/11/2011 03:21 PM"
        Me.dtEnd.Value = New Date(2011, 11, 19, 15, 21, 23, 359)
        '
        'dtStart
        '
        Me.dtStart.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStart.Location = New System.Drawing.Point(51, 20)
        Me.dtStart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStart.Size = New System.Drawing.Size(135, 20)
        Me.dtStart.TabIndex = 2
        Me.dtStart.TabStop = False
        Me.dtStart.Text = "19/11/2011 03:21 PM"
        Me.dtStart.Value = New Date(2011, 11, 19, 15, 21, 23, 359)
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.chkIQtywise)
        Me.RadGroupBox1.Controls.Add(Me.chkAllItems)
        Me.RadGroupBox1.Controls.Add(Me.rbtnSummary)
        Me.RadGroupBox1.Controls.Add(Me.rbtnDetail)
        Me.RadGroupBox1.Controls.Add(Me.chkImrpWise)
        Me.RadGroupBox1.Controls.Add(Me.chkCHapterWise)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.dtEnd)
        Me.RadGroupBox1.Controls.Add(Me.dtStart)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = "Date & Time"
        Me.RadGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(526, 354)
        Me.RadGroupBox1.TabIndex = 3
        Me.RadGroupBox1.Text = "Date & Time"
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(198, 28)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(2, 2)
        Me.MyLabel1.TabIndex = 316
        '
        'chkIQtywise
        '
        Me.chkIQtywise.Location = New System.Drawing.Point(405, 23)
        Me.chkIQtywise.Name = "chkIQtywise"
        Me.chkIQtywise.Size = New System.Drawing.Size(117, 18)
        Me.chkIQtywise.TabIndex = 315
        Me.chkIQtywise.Text = "Item(Quantity) wise"
        Me.chkIQtywise.Visible = False
        '
        'chkAllItems
        '
        Me.chkAllItems.Location = New System.Drawing.Point(278, 48)
        Me.chkAllItems.Name = "chkAllItems"
        Me.chkAllItems.Size = New System.Drawing.Size(63, 18)
        Me.chkAllItems.TabIndex = 314
        Me.chkAllItems.Text = "All Items"
        '
        'rbtnSummary
        '
        Me.rbtnSummary.Location = New System.Drawing.Point(202, 47)
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 313
        Me.rbtnSummary.Text = "Summary"
        '
        'rbtnDetail
        '
        Me.rbtnDetail.Location = New System.Drawing.Point(202, 23)
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 312
        Me.rbtnDetail.TabStop = True
        Me.rbtnDetail.Text = "Detail"
        Me.rbtnDetail.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkImrpWise
        '
        Me.chkImrpWise.Location = New System.Drawing.Point(278, 22)
        Me.chkImrpWise.Name = "chkImrpWise"
        Me.chkImrpWise.Size = New System.Drawing.Size(98, 18)
        Me.chkImrpWise.TabIndex = 311
        Me.chkImrpWise.Text = "Item(MRP) wise"
        '
        'chkCHapterWise
        '
        Me.chkCHapterWise.Location = New System.Drawing.Point(405, 48)
        Me.chkCHapterWise.Name = "chkCHapterWise"
        Me.chkCHapterWise.Size = New System.Drawing.Size(116, 18)
        Me.chkCHapterWise.TabIndex = 310
        Me.chkCHapterWise.Text = "Chapter Head Wise"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbothers)
        Me.GroupBox1.Controls.Add(Me.rdbFinishGoods)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 92)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(497, 47)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item Types"
        '
        'rdbothers
        '
        Me.rdbothers.Location = New System.Drawing.Point(229, 21)
        Me.rdbothers.Name = "rdbothers"
        Me.rdbothers.Size = New System.Drawing.Size(54, 18)
        Me.rdbothers.TabIndex = 1
        Me.rdbothers.Text = "Others"
        '
        'rdbFinishGoods
        '
        Me.rdbFinishGoods.Location = New System.Drawing.Point(31, 21)
        Me.rdbFinishGoods.Name = "rdbFinishGoods"
        Me.rdbFinishGoods.Size = New System.Drawing.Size(84, 18)
        Me.rdbFinishGoods.TabIndex = 0
        Me.rdbFinishGoods.TabStop = True
        Me.rdbFinishGoods.Text = "Finish Goods"
        Me.rdbFinishGoods.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(10, 146)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(500, 193)
        Me.RadGroupBox5.TabIndex = 309
        Me.RadGroupBox5.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(480, 143)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocSelect)
        Me.Panel3.Controls.Add(Me.chkLocAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(480, 20)
        Me.Panel3.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(229, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(180, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(14, 44)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(27, 18)
        Me.RadLabel2.TabIndex = 1
        Me.RadLabel2.Text = "End:"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 20)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 0
        Me.RadLabel1.Text = "Start:"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(88, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 22)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(466, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 22)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReferesh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(551, 449)
        Me.SplitContainer1.SplitterDistance = 405
        Me.SplitContainer1.TabIndex = 4
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(551, 405)
        Me.RadPageView1.TabIndex = 120
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(530, 357)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(530, 305)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(530, 305)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(163, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(92, 22)
        Me.btnExport.TabIndex = 7
        Me.btnExport.Text = "Export To Excel"
        '
        'btnReferesh
        '
        Me.btnReferesh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReferesh.Location = New System.Drawing.Point(13, 6)
        Me.btnReferesh.Name = "btnReferesh"
        Me.btnReferesh.Size = New System.Drawing.Size(69, 22)
        Me.btnReferesh.TabIndex = 6
        Me.btnReferesh.Text = ">>>"
        '
        'FrmExciseSummaryReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(551, 449)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmExciseSummaryReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Excise Summary"
        CType(Me.dtEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIQtywise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkImrpWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCHapterWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rdbothers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbFinishGoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReferesh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtEnd As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtStart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbothers As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbFinishGoods As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents chkCHapterWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnReferesh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents MasterTemplate As common.UserControls.MyRadGridView
    Friend WithEvents chkImrpWise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents chkAllItems As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents rbtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkIQtywise As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

