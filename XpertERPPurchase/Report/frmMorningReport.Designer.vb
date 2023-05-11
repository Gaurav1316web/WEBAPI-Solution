<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMorningReport
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
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
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.drpItemType = New Telerik.WinControls.UI.RadDropDownList
        Me.grpbxItemWise = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkItemSelect = New common.Controls.MyRadioButton
        Me.chkItemAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.lblFromDate = New common.Controls.MyLabel
        Me.lblToDate = New common.Controls.MyLabel
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
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
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.drpItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxItemWise.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.grpbxSubCategory)
        Me.RadGroupBox1.Controls.Add(Me.grpbxItemCategory)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.drpItemType)
        Me.RadGroupBox1.Controls.Add(Me.grpbxItemWise)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblFromDate)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(641, 397)
        Me.RadGroupBox1.TabIndex = 0
        '
        'grpbxSubCategory
        '
        Me.grpbxSubCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxSubCategory.Controls.Add(Me.cbgSubCategroy)
        Me.grpbxSubCategory.Controls.Add(Me.Panel3)
        Me.grpbxSubCategory.HeaderText = "Sub Category"
        Me.grpbxSubCategory.Location = New System.Drawing.Point(329, 59)
        Me.grpbxSubCategory.Name = "grpbxSubCategory"
        Me.grpbxSubCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxSubCategory.Size = New System.Drawing.Size(297, 164)
        Me.grpbxSubCategory.TabIndex = 74
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
        Me.cbgSubCategroy.Size = New System.Drawing.Size(277, 114)
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
        Me.Panel3.Size = New System.Drawing.Size(277, 20)
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
        Me.grpbxItemCategory.Location = New System.Drawing.Point(13, 59)
        Me.grpbxItemCategory.Name = "grpbxItemCategory"
        Me.grpbxItemCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemCategory.Size = New System.Drawing.Size(297, 164)
        Me.grpbxItemCategory.TabIndex = 73
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
        Me.cbgCategory.Size = New System.Drawing.Size(277, 114)
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
        Me.Panel1.Size = New System.Drawing.Size(277, 20)
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
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(385, 23)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel1.TabIndex = 27
        Me.MyLabel1.Text = "Item Type"
        '
        'drpItemType
        '
        Me.drpItemType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Select"
        RadListDataItem1.TextWrap = True
        RadListDataItem2.Text = "Finished Goods"
        RadListDataItem2.TextWrap = True
        RadListDataItem3.Text = "Other"
        RadListDataItem3.TextWrap = True
        Me.drpItemType.Items.Add(RadListDataItem1)
        Me.drpItemType.Items.Add(RadListDataItem2)
        Me.drpItemType.Items.Add(RadListDataItem3)
        Me.drpItemType.Location = New System.Drawing.Point(485, 21)
        Me.drpItemType.Name = "drpItemType"
        Me.drpItemType.Size = New System.Drawing.Size(133, 20)
        Me.drpItemType.TabIndex = 72
        '
        'grpbxItemWise
        '
        Me.grpbxItemWise.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxItemWise.Controls.Add(Me.cbgItem)
        Me.grpbxItemWise.Controls.Add(Me.Panel2)
        Me.grpbxItemWise.HeaderText = "Item"
        Me.grpbxItemWise.Location = New System.Drawing.Point(11, 229)
        Me.grpbxItemWise.Name = "grpbxItemWise"
        Me.grpbxItemWise.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxItemWise.Size = New System.Drawing.Size(299, 155)
        Me.grpbxItemWise.TabIndex = 71
        Me.grpbxItemWise.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.AccessibleName = "cbgItem"
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(279, 105)
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
        Me.Panel2.Size = New System.Drawing.Size(279, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkItemSelect
        '
        Me.chkItemSelect.AccessibleName = "chkItemSelect"
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
        Me.chkItemAll.AccessibleName = "chkItemAll"
        Me.chkItemAll.Location = New System.Drawing.Point(46, 1)
        Me.chkItemAll.MyLinkLable1 = Nothing
        Me.chkItemAll.MyLinkLable2 = Nothing
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.Size = New System.Drawing.Size(33, 18)
        Me.chkItemAll.TabIndex = 0
        Me.chkItemAll.Text = "All"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel5)
        Me.RadGroupBox3.HeaderText = " Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(329, 229)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(299, 155)
        Me.RadGroupBox3.TabIndex = 70
        Me.RadGroupBox3.Text = " Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 51)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(279, 94)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkLocationSelect)
        Me.Panel5.Controls.Add(Me.chkLocationAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(279, 31)
        Me.Panel5.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkLoc_select"
        Me.chkLocationSelect.AccessibleName = "chkLocationSelect"
        Me.chkLocationSelect.Location = New System.Drawing.Point(106, 7)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkLocAll"
        Me.chkLocationAll.AccessibleName = "chkLocationAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(46, 7)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(270, 23)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(88, 20)
        Me.dtpToDate.TabIndex = 29
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "01/02/2012"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'lblFromDate
        '
        Me.lblFromDate.Location = New System.Drawing.Point(11, 23)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 26
        Me.lblFromDate.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(199, 23)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 27
        Me.lblToDate.Text = "To Date"
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(76, 23)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(89, 20)
        Me.dtpFromdate1.TabIndex = 28
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01/02/2012"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(74, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(59, 24)
        Me.btnPrint.TabIndex = 33
        Me.btnPrint.Text = "Print"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(55, 24)
        Me.btnreset.TabIndex = 35
        Me.btnreset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(590, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(57, 24)
        Me.btnClose.TabIndex = 34
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(661, 443)
        Me.SplitContainer1.SplitterDistance = 403
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmMorningReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 443)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMorningReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Morning Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
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
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.drpItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxItemWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxItemWise.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkItemSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents grpbxItemWise As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkItemSelect As common.Controls.MyRadioButton
    Friend WithEvents chkItemAll As common.Controls.MyRadioButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents drpItemType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents grpbxItemCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCategory As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkCategorySelect As common.Controls.MyRadioButton
    Friend WithEvents chkCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents grpbxSubCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSubCategroy As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSubCategroySelect As common.Controls.MyRadioButton
    Friend WithEvents chkSubCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

