<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RM_Consumption_Detail
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
        Me.itemselect = New common.Controls.MyRadioButton
        Me.itemall = New common.Controls.MyRadioButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.grpbxSubCategory = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgSubCategroy = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkSubCategroySelect = New common.Controls.MyRadioButton
        Me.chkSubCategoryAll = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.dtptodate = New common.Controls.MyDateTimePicker
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnclose1 = New Telerik.WinControls.UI.RadButton
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton
        Me.btnprint1 = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.itemselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpbxSubCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxSubCategory.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkSubCategroySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSubCategoryAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'itemselect
        '
        Me.itemselect.AccessibleName = "chk_vendor_select"
        Me.itemselect.Location = New System.Drawing.Point(213, 1)
        Me.itemselect.MyLinkLable1 = Nothing
        Me.itemselect.MyLinkLable2 = Nothing
        Me.itemselect.Name = "itemselect"
        Me.itemselect.Size = New System.Drawing.Size(50, 18)
        Me.itemselect.TabIndex = 1
        Me.itemselect.Text = "Select"
        '
        'itemall
        '
        Me.itemall.AccessibleName = "chkvendor_All"
        Me.itemall.Location = New System.Drawing.Point(162, 1)
        Me.itemall.MyLinkLable1 = Nothing
        Me.itemall.MyLinkLable2 = Nothing
        Me.itemall.Name = "itemall"
        Me.itemall.Size = New System.Drawing.Size(33, 18)
        Me.itemall.TabIndex = 0
        Me.itemall.Text = "All"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.itemselect)
        Me.Panel3.Controls.Add(Me.itemall)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(420, 20)
        Me.Panel3.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.grpbxSubCategory)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(14, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(452, 560)
        Me.RadGroupBox1.TabIndex = 2
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox5.Controls.Add(Me.Panel2)
        Me.RadGroupBox5.HeaderText = "Location"
        Me.RadGroupBox5.Location = New System.Drawing.Point(7, 381)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(442, 165)
        Me.RadGroupBox5.TabIndex = 310
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
        Me.cbgLocation.Size = New System.Drawing.Size(422, 115)
        Me.cbgLocation.TabIndex = 2
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocSelect)
        Me.Panel2.Controls.Add(Me.chkLocAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(422, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(213, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 2
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(162, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 1
        Me.chkLocAll.Text = "All"
        '
        'grpbxSubCategory
        '
        Me.grpbxSubCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxSubCategory.Controls.Add(Me.cbgSubCategroy)
        Me.grpbxSubCategory.Controls.Add(Me.Panel1)
        Me.grpbxSubCategory.HeaderText = "Sub Category"
        Me.grpbxSubCategory.Location = New System.Drawing.Point(7, 48)
        Me.grpbxSubCategory.Name = "grpbxSubCategory"
        Me.grpbxSubCategory.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxSubCategory.Size = New System.Drawing.Size(438, 164)
        Me.grpbxSubCategory.TabIndex = 21
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
        Me.cbgSubCategroy.Size = New System.Drawing.Size(418, 114)
        Me.cbgSubCategroy.TabIndex = 1
        Me.cbgSubCategroy.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkSubCategroySelect)
        Me.Panel1.Controls.Add(Me.chkSubCategoryAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(418, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkSubCategroySelect
        '
        Me.chkSubCategroySelect.Location = New System.Drawing.Point(213, 2)
        Me.chkSubCategroySelect.MyLinkLable1 = Nothing
        Me.chkSubCategroySelect.MyLinkLable2 = Nothing
        Me.chkSubCategroySelect.Name = "chkSubCategroySelect"
        Me.chkSubCategroySelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSubCategroySelect.TabIndex = 1
        Me.chkSubCategroySelect.Text = "Select"
        '
        'chkSubCategoryAll
        '
        Me.chkSubCategoryAll.Location = New System.Drawing.Point(162, 2)
        Me.chkSubCategoryAll.MyLinkLable1 = Nothing
        Me.chkSubCategoryAll.MyLinkLable2 = Nothing
        Me.chkSubCategoryAll.Name = "chkSubCategoryAll"
        Me.chkSubCategoryAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSubCategoryAll.TabIndex = 0
        Me.chkSubCategoryAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgItem)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Item Code"
        Me.RadGroupBox4.Location = New System.Drawing.Point(7, 218)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(440, 157)
        Me.RadGroupBox4.TabIndex = 19
        Me.RadGroupBox4.Text = "Item Code"
        '
        'cbgItem
        '
        Me.cbgItem.AccessibleName = "cbgVendor"
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(420, 107)
        Me.cbgItem.TabIndex = 1
        Me.cbgItem.ValueMember = "Code"
        '
        'dtptodate
        '
        Me.dtptodate.AccessibleName = "dtptodate"
        Me.dtptodate.CustomFormat = "dd-MM-yyyy"
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.Location = New System.Drawing.Point(353, 9)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(82, 20)
        Me.dtptodate.TabIndex = 11
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "23-01-2012"
        Me.dtptodate.Value = New Date(2012, 1, 23, 0, 0, 0, 0)
        '
        'dtpfromdate
        '
        Me.dtpfromdate.AccessibleName = "dtpfromdate"
        Me.dtpfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate.Location = New System.Drawing.Point(94, 9)
        Me.dtpfromdate.MendatroryField = False
        Me.dtpfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.MyLinkLable1 = Nothing
        Me.dtpfromdate.MyLinkLable2 = Nothing
        Me.dtpfromdate.Name = "dtpfromdate"
        Me.dtpfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate.TabIndex = 10
        Me.dtpfromdate.TabStop = False
        Me.dtpfromdate.Text = "23-01-2012"
        Me.dtpfromdate.Value = New Date(2012, 1, 23, 0, 0, 0, 0)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(290, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel1.TabIndex = 18
        Me.RadLabel1.Text = "To Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(17, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel2.TabIndex = 17
        Me.RadLabel2.Text = "From Date"
        '
        'btnclose1
        '
        Me.btnclose1.AccessibleName = "btnclose"
        Me.btnclose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose1.Location = New System.Drawing.Point(398, 4)
        Me.btnclose1.Name = "btnclose1"
        Me.btnclose1.Size = New System.Drawing.Size(68, 18)
        Me.btnclose1.TabIndex = 16
        Me.btnclose1.Text = "Close"
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleName = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(14, 4)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 15
        Me.btnreset1.Text = "Reset"
        '
        'btnprint1
        '
        Me.btnprint1.AccessibleName = "btnprint"
        Me.btnprint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint1.Location = New System.Drawing.Point(88, 4)
        Me.btnprint1.Name = "btnprint1"
        Me.btnprint1.Size = New System.Drawing.Size(68, 18)
        Me.btnprint1.TabIndex = 14
        Me.btnprint1.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose1)
        Me.SplitContainer1.Size = New System.Drawing.Size(476, 614)
        Me.SplitContainer1.SplitterDistance = 585
        Me.SplitContainer1.TabIndex = 3
        '
        'RM_Consumption_Detail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 614)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RM_Consumption_Detail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RM Consumption Breakup"
        CType(Me.itemselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpbxSubCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxSubCategory.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkSubCategroySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSubCategoryAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents itemselect As common.Controls.MyRadioButton
    Friend WithEvents itemall As common.Controls.MyRadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents grpbxSubCategory As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgSubCategroy As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkSubCategroySelect As common.Controls.MyRadioButton
    Friend WithEvents chkSubCategoryAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

