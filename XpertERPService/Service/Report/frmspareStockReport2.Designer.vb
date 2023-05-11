Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmspareStockReport2
    Inherits Telerik.WinControls.UI.RadForm

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
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem6 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem7 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.tvCategory = New Telerik.WinControls.UI.RadTreeView
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.rbtnCategorySelect = New common.Controls.MyRadioButton
        Me.rbtnCategoryAll = New common.Controls.MyRadioButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItemMaster = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkItemAll = New common.Controls.MyRadioButton
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.dtpFdate = New common.Controls.MyDateTimePicker
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem
        Me.RadMenuItem5 = New Telerik.WinControls.UI.RadMenuItem
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnGo = New Telerik.WinControls.UI.RadButton
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        Me.RadMenuItem3.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Setting"
        Me.RadMenuItem1.AccessibleName = "Setting"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(826, 20)
        Me.RadMenu1.TabIndex = 25
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Setting"
        Me.RadMenuItem2.AccessibleName = "Setting"
        Me.RadMenuItem2.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem6, Me.RadMenuItem7})
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Setting"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem6
        '
        Me.RadMenuItem6.AccessibleDescription = "save Layout"
        Me.RadMenuItem6.AccessibleName = "save Layout"
        Me.RadMenuItem6.Name = "RadMenuItem6"
        Me.RadMenuItem6.Text = "save Layout"
        Me.RadMenuItem6.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem7
        '
        Me.RadMenuItem7.AccessibleDescription = "Delete Layout"
        Me.RadMenuItem7.AccessibleName = "Delete Layout"
        Me.RadMenuItem7.Name = "RadMenuItem7"
        Me.RadMenuItem7.Text = "Delete Layout"
        Me.RadMenuItem7.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(826, 437)
        Me.RadPageView1.TabIndex = 26
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(805, 389)
        Me.RadPageViewPage1.Text = "Filter"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFdate)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(805, 389)
        Me.SplitContainer1.SplitterDistance = 347
        Me.SplitContainer1.TabIndex = 22
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.tvCategory)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Model"
        Me.RadGroupBox2.Location = New System.Drawing.Point(392, 28)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(339, 186)
        Me.RadGroupBox2.TabIndex = 319
        Me.RadGroupBox2.Text = "Model"
        '
        'tvCategory
        '
        Me.tvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvCategory.Location = New System.Drawing.Point(10, 40)
        Me.tvCategory.Name = "tvCategory"
        Me.tvCategory.Size = New System.Drawing.Size(319, 136)
        Me.tvCategory.SpacingBetweenNodes = -1
        Me.tvCategory.TabIndex = 70
        Me.tvCategory.Text = "RadTreeView1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbtnCategorySelect)
        Me.Panel4.Controls.Add(Me.rbtnCategoryAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(319, 20)
        Me.Panel4.TabIndex = 1
        '
        'rbtnCategorySelect
        '
        Me.rbtnCategorySelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnCategorySelect.Location = New System.Drawing.Point(156, 1)
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
        Me.rbtnCategoryAll.Location = New System.Drawing.Point(105, 1)
        Me.rbtnCategoryAll.MyLinkLable1 = Nothing
        Me.rbtnCategoryAll.MyLinkLable2 = Nothing
        Me.rbtnCategoryAll.Name = "rbtnCategoryAll"
        Me.rbtnCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCategoryAll.TabIndex = 0
        Me.rbtnCategoryAll.Text = "All"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(169, 3)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 105
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(8, 3)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 106
        Me.RadLabel1.Text = "From Date"
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = My.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(308, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(17, 19)
        Me.btnReset.TabIndex = 102
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgItemMaster)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Item Master"
        Me.RadGroupBox3.Location = New System.Drawing.Point(8, 28)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(362, 186)
        Me.RadGroupBox3.TabIndex = 2
        Me.RadGroupBox3.Text = "Item Master"
        '
        'cbgItemMaster
        '
        Me.cbgItemMaster.AccessibleDescription = "cbgItemMaster"
        Me.cbgItemMaster.CheckedValue = Nothing
        Me.cbgItemMaster.DataSource = Nothing
        Me.cbgItemMaster.DisplayMember = "Name"
        Me.cbgItemMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItemMaster.Location = New System.Drawing.Point(10, 40)
        Me.cbgItemMaster.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItemMaster.MyShowHeadrText = False
        Me.cbgItemMaster.Name = "cbgItemMaster"
        Me.cbgItemMaster.Size = New System.Drawing.Size(342, 136)
        Me.cbgItemMaster.TabIndex = 0
        Me.cbgItemMaster.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkItemSelect)
        Me.Panel2.Controls.Add(Me.chkItemAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(342, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkItemSelect.Location = New System.Drawing.Point(168, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkItemAll.Location = New System.Drawing.Point(117, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.TabStop = True
        Me.chkItemAll.Text = "All"
        Me.chkItemAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(216, 2)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Nothing
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpToDate.TabIndex = 104
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "18/05/2011"
        Me.dtpToDate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'dtpFdate
        '
        Me.dtpFdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFdate.Location = New System.Drawing.Point(76, 4)
        Me.dtpFdate.MendatroryField = False
        Me.dtpFdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.MyLinkLable1 = Nothing
        Me.dtpFdate.MyLinkLable2 = Nothing
        Me.dtpFdate.Name = "dtpFdate"
        Me.dtpFdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFdate.Size = New System.Drawing.Size(84, 18)
        Me.dtpFdate.TabIndex = 103
        Me.dtpFdate.TabStop = False
        Me.dtpFdate.Text = "18/05/2011"
        Me.dtpFdate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(805, 389)
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
        Me.gv1.ReadOnly = True
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(805, 389)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExport)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnGo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 457)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(826, 34)
        Me.Panel1.TabIndex = 320
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem4, Me.RadMenuItem5})
        Me.btnExport.Location = New System.Drawing.Point(110, 5)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(92, 26)
        Me.btnExport.TabIndex = 119
        Me.btnExport.Text = "Export"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.AccessibleDescription = "Export"
        Me.RadMenuItem4.AccessibleName = "Export"
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Export"
        Me.RadMenuItem4.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem5
        '
        Me.RadMenuItem5.AccessibleDescription = "PDF"
        Me.RadMenuItem5.AccessibleName = "PDF"
        Me.RadMenuItem5.Name = "RadMenuItem5"
        Me.RadMenuItem5.Text = "PDF"
        Me.RadMenuItem5.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(757, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(5, 5)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(95, 24)
        Me.btnGo.TabIndex = 1
        Me.btnGo.Text = ">>>"
        '
        'FrmspareStockReport2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 491)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmspareStockReport2"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmspareStockReport2"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.tvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents tvCategory As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents rbtnCategorySelect As common.Controls.MyRadioButton
    Protected WithEvents rbtnCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItemMaster As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFdate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem5 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem6 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem7 As Telerik.WinControls.UI.RadMenuItem
End Class

