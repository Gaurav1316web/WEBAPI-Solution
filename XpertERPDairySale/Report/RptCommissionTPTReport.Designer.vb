<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptcommissionTPTReport
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.rbtnDetail = New common.Controls.MyRadioButton()
        Me.rbtnSummary = New common.Controls.MyRadioButton()
        Me.txtMultDistr = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.ddlSubCategory = New Telerik.WinControls.UI.RadDropDownList()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGenrate = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmenuPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGenrate)
        Me.SplitContainer1.Size = New System.Drawing.Size(799, 415)
        Me.SplitContainer1.SplitterDistance = 378
        Me.SplitContainer1.TabIndex = 0
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
        Me.RadPageView1.Size = New System.Drawing.Size(799, 378)
        Me.RadPageView1.TabIndex = 73
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.rbtnDetail)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnSummary)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultDistr)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.ddlSubCategory)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(778, 330)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'rbtnDetail
        '
        Me.rbtnDetail.Location = New System.Drawing.Point(607, 22)
        Me.rbtnDetail.MyLinkLable1 = Nothing
        Me.rbtnDetail.MyLinkLable2 = Nothing
        Me.rbtnDetail.Name = "rbtnDetail"
        Me.rbtnDetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtnDetail.TabIndex = 363
        Me.rbtnDetail.TabStop = False
        Me.rbtnDetail.Text = "Detail"
        '
        'rbtnSummary
        '
        Me.rbtnSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnSummary.Location = New System.Drawing.Point(510, 21)
        Me.rbtnSummary.MyLinkLable1 = Nothing
        Me.rbtnSummary.MyLinkLable2 = Nothing
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 362
        Me.rbtnSummary.Text = "Summary"
        Me.rbtnSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtMultDistr
        '
        Me.txtMultDistr.arrDispalyMember = Nothing
        Me.txtMultDistr.arrValueMember = Nothing
        Me.txtMultDistr.Location = New System.Drawing.Point(133, 64)
        Me.txtMultDistr.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultDistr.MyLinkLable1 = Nothing
        Me.txtMultDistr.MyLinkLable2 = Nothing
        Me.txtMultDistr.MyNullText = "All"
        Me.txtMultDistr.Name = "txtMultDistr"
        Me.txtMultDistr.Size = New System.Drawing.Size(301, 19)
        Me.txtMultDistr.TabIndex = 361
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(22, 64)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel1.TabIndex = 360
        Me.MyLabel1.Text = "Distributer"
        '
        'TxtRoute
        '
        Me.TxtRoute.arrDispalyMember = Nothing
        Me.TxtRoute.arrValueMember = Nothing
        Me.TxtRoute.Location = New System.Drawing.Point(133, 92)
        Me.TxtRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoute.MyLinkLable1 = Me.MyLabel10
        Me.TxtRoute.MyLinkLable2 = Nothing
        Me.TxtRoute.MyNullText = "All"
        Me.TxtRoute.Name = "TxtRoute"
        Me.TxtRoute.Size = New System.Drawing.Size(301, 19)
        Me.TxtRoute.TabIndex = 358
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(21, 94)
        Me.MyLabel10.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel10.TabIndex = 359
        Me.MyLabel10.Text = "Route"
        '
        'ddlSubCategory
        '
        Me.ddlSubCategory.AutoCompleteDisplayMember = Nothing
        Me.ddlSubCategory.AutoCompleteValueMember = Nothing
        Me.ddlSubCategory.DropDownAnimationEnabled = True
        RadListDataItem4.Text = "Both"
        RadListDataItem5.Text = "Sale Invoice"
        RadListDataItem6.Text = "Sale Return"
        Me.ddlSubCategory.Items.Add(RadListDataItem4)
        Me.ddlSubCategory.Items.Add(RadListDataItem5)
        Me.ddlSubCategory.Items.Add(RadListDataItem6)
        Me.ddlSubCategory.Location = New System.Drawing.Point(1098, 94)
        Me.ddlSubCategory.Margin = New System.Windows.Forms.Padding(4)
        Me.ddlSubCategory.Name = "ddlSubCategory"
        Me.ddlSubCategory.Size = New System.Drawing.Size(161, 20)
        Me.ddlSubCategory.TabIndex = 344
        Me.ddlSubCategory.Visible = False
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(21, 1)
        Me.RadGroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(13, 25, 13, 12)
        Me.RadGroupBox3.Size = New System.Drawing.Size(413, 52)
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
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1013, 429)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        Me.Gv1.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.MyStopExport = False
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(1013, 429)
        Me.Gv1.TabIndex = 0
        Me.Gv1.VarID = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(704, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 21)
        Me.btnClose.TabIndex = 363
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnReset.Location = New System.Drawing.Point(79, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(78, 21)
        Me.btnReset.TabIndex = 361
        Me.btnReset.Text = "Reset"
        '
        'btnGenrate
        '
        Me.btnGenrate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenrate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenrate.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnGenrate.Location = New System.Drawing.Point(11, 7)
        Me.btnGenrate.Name = "btnGenrate"
        Me.btnGenrate.Size = New System.Drawing.Size(65, 21)
        Me.btnGenrate.TabIndex = 360
        Me.btnGenrate.Text = ">>"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmenuExport, Me.rmenuPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(160, 7)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(71, 21)
        Me.RadSplitButton1.TabIndex = 364
        Me.RadSplitButton1.Text = "Export"
        '
        'rmenuExport
        '
        Me.rmenuExport.Name = "rmenuExport"
        Me.rmenuExport.Text = "Export"
        Me.rmenuExport.UseCompatibleTextRendering = False
        '
        'rmenuPDF
        '
        Me.rmenuPDF.Name = "rmenuPDF"
        Me.rmenuPDF.Text = "PDF"
        Me.rmenuPDF.UseCompatibleTextRendering = False
        '
        'RptcommissionTPTReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 415)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptcommissionTPTReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptcommissionTPTReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.rbtnDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGenrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents TxtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents ddlSubCategory As RadDropDownList
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnGenrate As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMultDistr As common.UserControls.txtMultiSelectFinder
    Friend WithEvents rbtnSummary As common.Controls.MyRadioButton
    Friend WithEvents rbtnDetail As common.Controls.MyRadioButton
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents rmenuExport As RadMenuItem
    Friend WithEvents rmenuPDF As RadMenuItem
End Class

