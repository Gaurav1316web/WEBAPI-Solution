<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmStockAnalysis1
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.grpbxItemWise = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkItemAll = New common.Controls.MyRadioButton
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker
        Me.lblToDate = New common.Controls.MyLabel
        Me.lblFromDate = New common.Controls.MyLabel
        Me.grpbxSubCategory = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgSubCategroy = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkSubCategroySelect = New common.Controls.MyRadioButton
        Me.chkSubCategoryAll = New common.Controls.MyRadioButton
        Me.grpbxItemCategory = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCategory = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkCategorySelect = New common.Controls.MyRadioButton
        Me.chkCategoryAll = New common.Controls.MyRadioButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxItemWise.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxSubCategory.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkSubCategroySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSubCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxItemCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxItemCategory.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkCategorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.grpbxItemWise)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblFromDate)
        Me.RadGroupBox1.Controls.Add(Me.grpbxSubCategory)
        Me.RadGroupBox1.Controls.Add(Me.grpbxItemCategory)
        Me.RadGroupBox1.HeaderText = "Stock Analysis"
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(530, 416)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Stock Analysis"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(258, 237)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(259, 164)
        Me.RadGroupBox2.TabIndex = 30
        Me.RadGroupBox2.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(239, 114)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkLocSelect)
        Me.Panel4.Controls.Add(Me.chkLocAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(239, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(106, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(46, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'grpbxItemWise
        '
        Me.grpbxItemWise.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxItemWise.Controls.Add(Me.cbgItem)
        Me.grpbxItemWise.Controls.Add(Me.Panel2)
        Me.grpbxItemWise.HeaderText = "Item"
        Me.grpbxItemWise.Location = New System.Drawing.Point(10, 237)
        Me.grpbxItemWise.Name = "grpbxItemWise"
        Me.grpbxItemWise.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemWise.Size = New System.Drawing.Size(242, 164)
        Me.grpbxItemWise.TabIndex = 16
        Me.grpbxItemWise.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.AccessibleName = ""
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(222, 114)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkItemSelect)
        Me.Panel2.Controls.Add(Me.chkItemAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(222, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.Location = New System.Drawing.Point(106, 1)
        Me.chkItemSelect.MyLinkLable1 = Nothing
        Me.chkItemSelect.MyLinkLable2 = Nothing
        Me.chkItemSelect.Name = "chkItemSelect"
        Me.chkItemSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkItemSelect.TabIndex = 1
        Me.chkItemSelect.Text = "Select"
        '
        'chkItemAll
        '
        Me.chkItemAll.Location = New System.Drawing.Point(46, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(333, 27)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 25
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "01/02/2012"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(87, 25)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(81, 20)
        Me.dtpFromdate1.TabIndex = 24
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01/02/2012"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(258, 27)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 23
        Me.lblToDate.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.Location = New System.Drawing.Point(13, 23)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 22
        Me.lblFromDate.Text = "From Date"
        '
        'grpbxSubCategory
        '
        Me.grpbxSubCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxSubCategory.Controls.Add(Me.cbgSubCategroy)
        Me.grpbxSubCategory.Controls.Add(Me.Panel3)
        Me.grpbxSubCategory.HeaderText = "Sub Category"
        Me.grpbxSubCategory.Location = New System.Drawing.Point(258, 67)
        Me.grpbxSubCategory.Name = "grpbxSubCategory"
        Me.grpbxSubCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxSubCategory.Size = New System.Drawing.Size(263, 164)
        Me.grpbxSubCategory.TabIndex = 18
        Me.grpbxSubCategory.Text = "Sub Category"
        '
        'cbgSubCategroy
        '
        Me.cbgSubCategroy.AccessibleName = ""
        Me.cbgSubCategroy.CheckedValue = Nothing
        Me.cbgSubCategroy.DataSource = Nothing
        Me.cbgSubCategroy.DisplayMember = "Name"
        Me.cbgSubCategroy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSubCategroy.Location = New System.Drawing.Point(10, 40)
        Me.cbgSubCategroy.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSubCategroy.MyShowHeadrText = False
        Me.cbgSubCategroy.Name = "cbgSubCategroy"
        Me.cbgSubCategroy.Size = New System.Drawing.Size(243, 114)
        Me.cbgSubCategroy.TabIndex = 1
        Me.cbgSubCategroy.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkSubCategroySelect)
        Me.Panel3.Controls.Add(Me.chkSubCategoryAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(243, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkSubCategroySelect
        '
        Me.chkSubCategroySelect.Location = New System.Drawing.Point(106, 1)
        Me.chkSubCategroySelect.MyLinkLable1 = Nothing
        Me.chkSubCategroySelect.MyLinkLable2 = Nothing
        Me.chkSubCategroySelect.Name = "chkSubCategroySelect"
        Me.chkSubCategroySelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSubCategroySelect.TabIndex = 1
        Me.chkSubCategroySelect.Text = "Select"
        '
        'chkSubCategoryAll
        '
        Me.chkSubCategoryAll.Location = New System.Drawing.Point(46, 1)
        Me.chkSubCategoryAll.MyLinkLable1 = Nothing
        Me.chkSubCategoryAll.MyLinkLable2 = Nothing
        Me.chkSubCategoryAll.Name = "chkSubCategoryAll"
        Me.chkSubCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSubCategoryAll.TabIndex = 0
        Me.chkSubCategoryAll.Text = "All"
        '
        'grpbxItemCategory
        '
        Me.grpbxItemCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxItemCategory.Controls.Add(Me.cbgCategory)
        Me.grpbxItemCategory.Controls.Add(Me.Panel1)
        Me.grpbxItemCategory.HeaderText = "Category"
        Me.grpbxItemCategory.Location = New System.Drawing.Point(10, 67)
        Me.grpbxItemCategory.Name = "grpbxItemCategory"
        Me.grpbxItemCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemCategory.Size = New System.Drawing.Size(242, 164)
        Me.grpbxItemCategory.TabIndex = 17
        Me.grpbxItemCategory.Text = "Category"
        '
        'cbgCategory
        '
        Me.cbgCategory.AccessibleName = ""
        Me.cbgCategory.CheckedValue = Nothing
        Me.cbgCategory.DataSource = Nothing
        Me.cbgCategory.DisplayMember = "Name"
        Me.cbgCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCategory.Location = New System.Drawing.Point(10, 40)
        Me.cbgCategory.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCategory.MyShowHeadrText = False
        Me.cbgCategory.Name = "cbgCategory"
        Me.cbgCategory.Size = New System.Drawing.Size(222, 114)
        Me.cbgCategory.TabIndex = 1
        Me.cbgCategory.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCategorySelect)
        Me.Panel1.Controls.Add(Me.chkCategoryAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(222, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkCategorySelect
        '
        Me.chkCategorySelect.Location = New System.Drawing.Point(115, 1)
        Me.chkCategorySelect.MyLinkLable1 = Nothing
        Me.chkCategorySelect.MyLinkLable2 = Nothing
        Me.chkCategorySelect.Name = "chkCategorySelect"
        Me.chkCategorySelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCategorySelect.TabIndex = 1
        Me.chkCategorySelect.Text = "Select"
        '
        'chkCategoryAll
        '
        Me.chkCategoryAll.Location = New System.Drawing.Point(52, 1)
        Me.chkCategoryAll.MyLinkLable1 = Nothing
        Me.chkCategoryAll.MyLinkLable2 = Nothing
        Me.chkCategoryAll.Name = "chkCategoryAll"
        Me.chkCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCategoryAll.TabIndex = 0
        Me.chkCategoryAll.Text = "All"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(76, 24)
        Me.btnreset.TabIndex = 28
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(460, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(94, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 24)
        Me.btnPrint.TabIndex = 26
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(547, 462)
        Me.SplitContainer1.SplitterDistance = 428
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmStockAnalysis1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 462)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmStockAnalysis1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Stock Analysis"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxItemWise.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxSubCategory.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkSubCategroySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSubCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxItemCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxItemCategory.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkCategorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents grpbxSubCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSubCategroy As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSubCategroySelect As common.Controls.MyRadioButton
    Friend WithEvents chkSubCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents grpbxItemCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCategory As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkCategorySelect As common.Controls.MyRadioButton
    Friend WithEvents chkCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents grpbxItemWise As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

