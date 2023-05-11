<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorGroupWiseSaleReport
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
        Me.dtpToDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.dtpFrmDate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.chkChapterSelect = New common.Controls.MyRadioButton
        Me.chkChapterAll = New common.Controls.MyRadioButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.MyRadioButton3 = New common.Controls.MyRadioButton
        Me.MyRadioButton4 = New common.Controls.MyRadioButton
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgBills = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.MyRadioButton1 = New common.Controls.MyRadioButton
        Me.MyRadioButton2 = New common.Controls.MyRadioButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.btnreferesh = New Telerik.WinControls.UI.RadButton
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkChapterSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkChapterAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.MyRadioButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadioButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreferesh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(257, 15)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.RadLabel2
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(119, 20)
        Me.dtpToDate.TabIndex = 88
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "13-06-2011"
        Me.dtpToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.RadLabel2.Location = New System.Drawing.Point(206, 15)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 89
        Me.RadLabel2.Text = "To Date"
        '
        'dtpFrmDate
        '
        Me.dtpFrmDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrmDate.Location = New System.Drawing.Point(79, 13)
        Me.dtpFrmDate.MendatroryField = False
        Me.dtpFrmDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.MyLinkLable1 = Me.RadLabel1
        Me.dtpFrmDate.MyLinkLable2 = Nothing
        Me.dtpFrmDate.Name = "dtpFrmDate"
        Me.dtpFrmDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.Size = New System.Drawing.Size(121, 20)
        Me.dtpFrmDate.TabIndex = 87
        Me.dtpFrmDate.TabStop = False
        Me.dtpFrmDate.Text = "13-06-2011"
        Me.dtpFrmDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(3, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 90
        Me.RadLabel1.Text = "From Date"
        '
        'chkChapterSelect
        '
        Me.chkChapterSelect.Location = New System.Drawing.Point(158, 1)
        Me.chkChapterSelect.MyLinkLable1 = Nothing
        Me.chkChapterSelect.MyLinkLable2 = Nothing
        Me.chkChapterSelect.Name = "chkChapterSelect"
        '
        '
        '
        Me.chkChapterSelect.RootElement.StretchHorizontally = True
        Me.chkChapterSelect.RootElement.StretchVertically = True
        Me.chkChapterSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkChapterSelect.TabIndex = 2
        Me.chkChapterSelect.Text = "Select"
        '
        'chkChapterAll
        '
        Me.chkChapterAll.Location = New System.Drawing.Point(109, 1)
        Me.chkChapterAll.MyLinkLable1 = Nothing
        Me.chkChapterAll.MyLinkLable2 = Nothing
        Me.chkChapterAll.Name = "chkChapterAll"
        '
        '
        '
        Me.chkChapterAll.RootElement.StretchHorizontally = True
        Me.chkChapterAll.RootElement.StretchVertically = True
        Me.chkChapterAll.Size = New System.Drawing.Size(33, 18)
        Me.chkChapterAll.TabIndex = 1
        Me.chkChapterAll.Text = "All"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(179, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 24)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "Print"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 20)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(318, 173)
        Me.cbgCustomer.TabIndex = 2
        Me.cbgCustomer.ValueMember = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(602, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Select Vendors"
        Me.RadGroupBox2.Location = New System.Drawing.Point(345, 40)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(346, 202)
        Me.RadGroupBox2.TabIndex = 313
        Me.RadGroupBox2.Text = "Select Vendors"
        '
        'cbgVendor
        '
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 20)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(326, 172)
        Me.cbgVendor.TabIndex = 2
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.MyRadioButton3)
        Me.Panel2.Controls.Add(Me.MyRadioButton4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(326, 172)
        Me.Panel2.TabIndex = 1
        '
        'MyRadioButton3
        '
        Me.MyRadioButton3.Location = New System.Drawing.Point(159, 1)
        Me.MyRadioButton3.MyLinkLable1 = Nothing
        Me.MyRadioButton3.MyLinkLable2 = Nothing
        Me.MyRadioButton3.Name = "MyRadioButton3"
        '
        '
        '
        Me.MyRadioButton3.RootElement.StretchHorizontally = True
        Me.MyRadioButton3.RootElement.StretchVertically = True
        Me.MyRadioButton3.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton3.TabIndex = 2
        Me.MyRadioButton3.Text = "Select"
        '
        'MyRadioButton4
        '
        Me.MyRadioButton4.Location = New System.Drawing.Point(110, 1)
        Me.MyRadioButton4.MyLinkLable1 = Nothing
        Me.MyRadioButton4.MyLinkLable2 = Nothing
        Me.MyRadioButton4.Name = "MyRadioButton4"
        '
        '
        '
        Me.MyRadioButton4.RootElement.StretchHorizontally = True
        Me.MyRadioButton4.RootElement.StretchVertically = True
        Me.MyRadioButton4.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton4.TabIndex = 1
        Me.MyRadioButton4.Text = "All"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox5.Controls.Add(Me.Panel3)
        Me.RadGroupBox5.HeaderText = "Select Customers"
        Me.RadGroupBox5.Location = New System.Drawing.Point(6, 39)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(338, 203)
        Me.RadGroupBox5.TabIndex = 312
        Me.RadGroupBox5.Text = "Select Customers"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkChapterSelect)
        Me.Panel3.Controls.Add(Me.chkChapterAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(318, 173)
        Me.Panel3.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgBills)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Select Bills"
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 8)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(685, 202)
        Me.RadGroupBox1.TabIndex = 314
        Me.RadGroupBox1.Text = "Select Bills"
        '
        'cbgBills
        '
        Me.cbgBills.CheckedValue = Nothing
        Me.cbgBills.DataSource = Nothing
        Me.cbgBills.DisplayMember = "Name"
        Me.cbgBills.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgBills.Location = New System.Drawing.Point(10, 20)
        Me.cbgBills.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgBills.MyShowHeadrText = False
        Me.cbgBills.Name = "cbgBills"
        Me.cbgBills.Size = New System.Drawing.Size(665, 172)
        Me.cbgBills.TabIndex = 2
        Me.cbgBills.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyRadioButton1)
        Me.Panel1.Controls.Add(Me.MyRadioButton2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(665, 172)
        Me.Panel1.TabIndex = 1
        '
        'MyRadioButton1
        '
        Me.MyRadioButton1.Location = New System.Drawing.Point(317, 1)
        Me.MyRadioButton1.MyLinkLable1 = Nothing
        Me.MyRadioButton1.MyLinkLable2 = Nothing
        Me.MyRadioButton1.Name = "MyRadioButton1"
        '
        '
        '
        Me.MyRadioButton1.RootElement.StretchHorizontally = True
        Me.MyRadioButton1.RootElement.StretchVertically = True
        Me.MyRadioButton1.Size = New System.Drawing.Size(50, 18)
        Me.MyRadioButton1.TabIndex = 2
        Me.MyRadioButton1.Text = "Select"
        '
        'MyRadioButton2
        '
        Me.MyRadioButton2.Location = New System.Drawing.Point(268, 1)
        Me.MyRadioButton2.MyLinkLable1 = Nothing
        Me.MyRadioButton2.MyLinkLable2 = Nothing
        Me.MyRadioButton2.Name = "MyRadioButton2"
        '
        '
        '
        Me.MyRadioButton2.RootElement.StretchHorizontally = True
        Me.MyRadioButton2.RootElement.StretchVertically = True
        Me.MyRadioButton2.Size = New System.Drawing.Size(33, 18)
        Me.MyRadioButton2.TabIndex = 1
        Me.MyRadioButton2.Text = "All"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(95, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(76, 24)
        Me.btnReset.TabIndex = 11
        Me.btnReset.Text = "Reset"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreferesh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(690, 471)
        Me.SplitContainer1.SplitterDistance = 429
        Me.SplitContainer1.TabIndex = 315
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(690, 429)
        Me.RadPageView1.TabIndex = 317
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(669, 381)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpFrmDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Size = New System.Drawing.Size(669, 381)
        Me.SplitContainer2.SplitterDistance = 218
        Me.SplitContainer2.TabIndex = 0
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(669, 381)
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
        Me.gv1.Size = New System.Drawing.Size(669, 381)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'btnreferesh
        '
        Me.btnreferesh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreferesh.Location = New System.Drawing.Point(10, 3)
        Me.btnreferesh.Name = "btnreferesh"
        Me.btnreferesh.Size = New System.Drawing.Size(76, 24)
        Me.btnreferesh.TabIndex = 13
        Me.btnreferesh.Text = ">>>"
        '
        'frmVendorGroupWiseSaleReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 471)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmVendorGroupWiseSaleReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Group Wise Sale Report"
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkChapterSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkChapterAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.MyRadioButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.MyRadioButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadioButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreferesh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpFrmDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents chkChapterSelect As common.Controls.MyRadioButton
    Friend WithEvents chkChapterAll As common.Controls.MyRadioButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents MyRadioButton3 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton4 As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgBills As common.MyCheckBoxGrid
    Friend WithEvents MyRadioButton1 As common.Controls.MyRadioButton
    Friend WithEvents MyRadioButton2 As common.Controls.MyRadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnreferesh As Telerik.WinControls.UI.RadButton
End Class

