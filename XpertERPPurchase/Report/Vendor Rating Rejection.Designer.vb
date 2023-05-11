<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Vendor_Rating_Rejection
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
        Me.chk_vendor_select = New common.Controls.MyRadioButton
        Me.dtptodate = New common.Controls.MyDateTimePicker
        Me.dtpfromdate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnclose1 = New Telerik.WinControls.UI.RadButton
        Me.btnprint1 = New Telerik.WinControls.UI.RadButton
        Me.cbgItem = New common.MyCheckBoxGrid
        Me.chkvendor_All1 = New common.Controls.MyRadioButton
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.itemselect = New common.Controls.MyRadioButton
        Me.itemall = New common.Controls.MyRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox13 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor1 = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnreset1 = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.chk_vendor_select, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkvendor_All1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.itemselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox13.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chk_vendor_select
        '
        Me.chk_vendor_select.AccessibleName = "chk_vendor_select"
        Me.chk_vendor_select.Location = New System.Drawing.Point(207, 1)
        Me.chk_vendor_select.MyLinkLable1 = Nothing
        Me.chk_vendor_select.MyLinkLable2 = Nothing
        Me.chk_vendor_select.Name = "chk_vendor_select"
        Me.chk_vendor_select.Size = New System.Drawing.Size(50, 18)
        Me.chk_vendor_select.TabIndex = 1
        Me.chk_vendor_select.Text = "Select"
        '
        'dtptodate
        '
        Me.dtptodate.AccessibleName = "dtptodate"
        Me.dtptodate.CustomFormat = "dd-MM-yyyy"
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptodate.Location = New System.Drawing.Point(363, 10)
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
        Me.dtpfromdate.Location = New System.Drawing.Point(67, 9)
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
        Me.RadLabel1.Location = New System.Drawing.Point(312, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel1.TabIndex = 18
        Me.RadLabel1.Text = "To Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(4, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel2.TabIndex = 17
        Me.RadLabel2.Text = "From Date"
        '
        'btnclose1
        '
        Me.btnclose1.AccessibleName = "btnclose"
        Me.btnclose1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose1.Location = New System.Drawing.Point(388, 8)
        Me.btnclose1.Name = "btnclose1"
        Me.btnclose1.Size = New System.Drawing.Size(68, 18)
        Me.btnclose1.TabIndex = 16
        Me.btnclose1.Text = "Close"
        '
        'btnprint1
        '
        Me.btnprint1.AccessibleName = "btnprint"
        Me.btnprint1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint1.Location = New System.Drawing.Point(81, 8)
        Me.btnprint1.Name = "btnprint1"
        Me.btnprint1.Size = New System.Drawing.Size(68, 18)
        Me.btnprint1.TabIndex = 14
        Me.btnprint1.Text = "Print"
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
        'chkvendor_All1
        '
        Me.chkvendor_All1.AccessibleName = "chkvendor_All"
        Me.chkvendor_All1.Location = New System.Drawing.Point(156, 1)
        Me.chkvendor_All1.MyLinkLable1 = Nothing
        Me.chkvendor_All1.MyLinkLable2 = Nothing
        Me.chkvendor_All1.Name = "chkvendor_All1"
        Me.chkvendor_All1.Size = New System.Drawing.Size(33, 18)
        Me.chkvendor_All1.TabIndex = 0
        Me.chkvendor_All1.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgItem)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Item Code"
        Me.RadGroupBox4.Location = New System.Drawing.Point(3, 206)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(440, 157)
        Me.RadGroupBox4.TabIndex = 19
        Me.RadGroupBox4.Text = "Item Code"
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
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox13)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpfromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(449, 532)
        Me.RadGroupBox1.TabIndex = 1
        '
        'RadGroupBox13
        '
        Me.RadGroupBox13.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox13.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox13.Controls.Add(Me.Panel9)
        Me.RadGroupBox13.HeaderText = " Location"
        Me.RadGroupBox13.Location = New System.Drawing.Point(3, 369)
        Me.RadGroupBox13.Name = "RadGroupBox13"
        Me.RadGroupBox13.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox13.Size = New System.Drawing.Size(440, 152)
        Me.RadGroupBox13.TabIndex = 76
        Me.RadGroupBox13.Text = " Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(420, 97)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.chkLocationSelect)
        Me.Panel9.Controls.Add(Me.chkLocationAll)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 20)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(420, 25)
        Me.Panel9.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(193, 3)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(141, 4)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgVendor1)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Vendor"
        Me.RadGroupBox2.Location = New System.Drawing.Point(4, 31)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(440, 169)
        Me.RadGroupBox2.TabIndex = 13
        Me.RadGroupBox2.Text = "Vendor"
        '
        'cbgVendor1
        '
        Me.cbgVendor1.AccessibleName = "cbgVendor"
        Me.cbgVendor1.CheckedValue = Nothing
        Me.cbgVendor1.DataSource = Nothing
        Me.cbgVendor1.DisplayMember = "Name"
        Me.cbgVendor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor1.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor1.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor1.MyShowHeadrText = False
        Me.cbgVendor1.Name = "cbgVendor1"
        Me.cbgVendor1.Size = New System.Drawing.Size(420, 119)
        Me.cbgVendor1.TabIndex = 1
        Me.cbgVendor1.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chk_vendor_select)
        Me.Panel1.Controls.Add(Me.chkvendor_All1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(420, 20)
        Me.Panel1.TabIndex = 0
        '
        'btnreset1
        '
        Me.btnreset1.AccessibleName = "btnreset"
        Me.btnreset1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset1.Location = New System.Drawing.Point(7, 8)
        Me.btnreset1.Name = "btnreset1"
        Me.btnreset1.Size = New System.Drawing.Size(68, 18)
        Me.btnreset1.TabIndex = 15
        Me.btnreset1.Text = "Reset"
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
        Me.SplitContainer1.Size = New System.Drawing.Size(473, 577)
        Me.SplitContainer1.SplitterDistance = 544
        Me.SplitContainer1.TabIndex = 2
        '
        'Vendor_Rating_Rejection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(473, 577)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Vendor_Rating_Rejection"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Rating Rejection"
        CType(Me.chk_vendor_select, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkvendor_All1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.itemselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.RadGroupBox13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox13.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnreset1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chk_vendor_select As common.Controls.MyRadioButton
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents chkvendor_All1 As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents itemselect As common.Controls.MyRadioButton
    Friend WithEvents itemall As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor1 As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnreset1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox13 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

