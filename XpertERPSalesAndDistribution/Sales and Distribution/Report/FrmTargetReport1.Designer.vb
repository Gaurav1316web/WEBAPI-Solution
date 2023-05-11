Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTargetReport1
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
        Me.dgvdiscount = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.discountselect = New common.Controls.MyRadioButton
        Me.discountall = New common.Controls.MyRadioButton
        Me.cg = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkselectcustomer = New common.Controls.MyRadioButton
        Me.chkallcustomer = New common.Controls.MyRadioButton
        Me.lblMonthYear = New common.Controls.MyLabel
        Me.dtpFromdate = New common.Controls.MyDateTimePicker
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.rdbPendingSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton
        Me.dtpTodate = New common.Controls.MyDateTimePicker
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.GV1 = New common.UserControls.MyRadGridView
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.discountselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.discountall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cg.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.rdbPendingSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.dgvdiscount)
        Me.RadGroupBox1.Controls.Add(Me.Panel2)
        Me.RadGroupBox1.HeaderText = "Discount Type"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 235)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(484, 192)
        Me.RadGroupBox1.TabIndex = 40
        Me.RadGroupBox1.Text = "Discount Type"
        '
        'dgvdiscount
        '
        Me.dgvdiscount.CheckedValue = Nothing
        Me.dgvdiscount.DataSource = Nothing
        Me.dgvdiscount.DisplayMember = "Name"
        Me.dgvdiscount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvdiscount.Location = New System.Drawing.Point(10, 40)
        Me.dgvdiscount.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.dgvdiscount.MyShowHeadrText = False
        Me.dgvdiscount.Name = "dgvdiscount"
        Me.dgvdiscount.Size = New System.Drawing.Size(464, 142)
        Me.dgvdiscount.TabIndex = 2
        Me.dgvdiscount.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.discountselect)
        Me.Panel2.Controls.Add(Me.discountall)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(464, 20)
        Me.Panel2.TabIndex = 1
        '
        'discountselect
        '
        Me.discountselect.Location = New System.Drawing.Point(198, 1)
        Me.discountselect.MyLinkLable1 = Nothing
        Me.discountselect.MyLinkLable2 = Nothing
        Me.discountselect.Name = "discountselect"
        Me.discountselect.Size = New System.Drawing.Size(50, 18)
        Me.discountselect.TabIndex = 2
        Me.discountselect.Text = "Select"
        '
        'discountall
        '
        Me.discountall.Location = New System.Drawing.Point(146, 1)
        Me.discountall.MyLinkLable1 = Nothing
        Me.discountall.MyLinkLable2 = Nothing
        Me.discountall.Name = "discountall"
        Me.discountall.Size = New System.Drawing.Size(33, 18)
        Me.discountall.TabIndex = 1
        Me.discountall.Text = "All"
        '
        'cg
        '
        Me.cg.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cg.Controls.Add(Me.cbgCustomer)
        Me.cg.Controls.Add(Me.Panel1)
        Me.cg.HeaderText = "Customer"
        Me.cg.Location = New System.Drawing.Point(12, 33)
        Me.cg.Name = "cg"
        Me.cg.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cg.Size = New System.Drawing.Size(484, 191)
        Me.cg.TabIndex = 39
        Me.cg.Text = "Customer"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 40)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(464, 141)
        Me.cbgCustomer.TabIndex = 2
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkselectcustomer)
        Me.Panel1.Controls.Add(Me.chkallcustomer)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(464, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkselectcustomer
        '
        Me.chkselectcustomer.Location = New System.Drawing.Point(198, 1)
        Me.chkselectcustomer.MyLinkLable1 = Nothing
        Me.chkselectcustomer.MyLinkLable2 = Nothing
        Me.chkselectcustomer.Name = "chkselectcustomer"
        Me.chkselectcustomer.Size = New System.Drawing.Size(50, 18)
        Me.chkselectcustomer.TabIndex = 2
        Me.chkselectcustomer.Text = "Select"
        '
        'chkallcustomer
        '
        Me.chkallcustomer.Location = New System.Drawing.Point(146, 1)
        Me.chkallcustomer.MyLinkLable1 = Nothing
        Me.chkallcustomer.MyLinkLable2 = Nothing
        Me.chkallcustomer.Name = "chkallcustomer"
        Me.chkallcustomer.Size = New System.Drawing.Size(33, 18)
        Me.chkallcustomer.TabIndex = 1
        Me.chkallcustomer.Text = "All"
        '
        'lblMonthYear
        '
        Me.lblMonthYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMonthYear.Location = New System.Drawing.Point(13, 7)
        Me.lblMonthYear.Name = "lblMonthYear"
        Me.lblMonthYear.Size = New System.Drawing.Size(62, 16)
        Me.lblMonthYear.TabIndex = 34
        Me.lblMonthYear.Text = "From Date"
        '
        'dtpFromdate
        '
        Me.dtpFromdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpFromdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate.Location = New System.Drawing.Point(81, 7)
        Me.dtpFromdate.MendatroryField = False
        Me.dtpFromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.MyLinkLable1 = Me.lblMonthYear
        Me.dtpFromdate.MyLinkLable2 = Nothing
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.Size = New System.Drawing.Size(84, 18)
        Me.dtpFromdate.TabIndex = 33
        Me.dtpFromdate.TabStop = False
        Me.dtpFromdate.Text = "18/05/2011 02:11 PM"
        Me.dtpFromdate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(888, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 43
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(80, 8)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 42
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(161, 8)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 41
        Me.btnprint.Text = "Print"
        Me.btnprint.Visible = False
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(972, 530)
        Me.SplitContainer1.SplitterDistance = 491
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
        Me.RadPageView1.Size = New System.Drawing.Size(972, 491)
        Me.RadPageView1.TabIndex = 115
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.dtpTodate)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblMonthYear)
        Me.RadPageViewPage1.Controls.Add(Me.cg)
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromdate)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(951, 443)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel1.Location = New System.Drawing.Point(171, 9)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel1.TabIndex = 36
        Me.MyLabel1.Text = "To Date"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox4.Controls.Add(Me.rdbPendingSummary)
        Me.RadGroupBox4.Controls.Add(Me.rdbDetail)
        Me.RadGroupBox4.Controls.Add(Me.rdbSummary)
        Me.RadGroupBox4.HeaderText = "Select"
        Me.RadGroupBox4.Location = New System.Drawing.Point(329, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(303, 37)
        Me.RadGroupBox4.TabIndex = 111
        Me.RadGroupBox4.Text = "Select"
        '
        'rdbPendingSummary
        '
        Me.rdbPendingSummary.Location = New System.Drawing.Point(176, 11)
        Me.rdbPendingSummary.Name = "rdbPendingSummary"
        Me.rdbPendingSummary.Size = New System.Drawing.Size(112, 18)
        Me.rdbPendingSummary.TabIndex = 106
        Me.rdbPendingSummary.Text = "Pending Summary"
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(105, 11)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 105
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.Location = New System.Drawing.Point(11, 11)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 105
        Me.rdbSummary.Text = "Summary"
        '
        'dtpTodate
        '
        Me.dtpTodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpTodate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTodate.Location = New System.Drawing.Point(239, 9)
        Me.dtpTodate.MendatroryField = False
        Me.dtpTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.MyLinkLable1 = Me.MyLabel1
        Me.dtpTodate.MyLinkLable2 = Nothing
        Me.dtpTodate.Name = "dtpTodate"
        Me.dtpTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpTodate.Size = New System.Drawing.Size(84, 18)
        Me.dtpTodate.TabIndex = 35
        Me.dtpTodate.TabStop = False
        Me.dtpTodate.Text = "18/05/2011 02:11 PM"
        Me.dtpTodate.Value = New Date(2011, 5, 18, 14, 11, 58, 609)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.GV1)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(951, 443)
        Me.RadPageViewPage2.Text = "Report"
        '
        'GV1
        '
        Me.GV1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GV1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.GV1.MasterTemplate.AllowAddNewRow = False
        Me.GV1.MasterTemplate.AllowEditRow = False
        Me.GV1.Name = "GV1"
        Me.GV1.ShowGroupPanel = False
        Me.GV1.Size = New System.Drawing.Size(951, 443)
        Me.GV1.TabIndex = 0
        Me.GV1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(235, 8)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 127
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.XpertERPSalesAndDistribution.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(3, 7)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnRefresh.TabIndex = 126
        Me.btnRefresh.Text = ">>>"
        '
        'FrmTargetReport1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(972, 530)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTargetReport1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Target Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.discountselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.discountall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cg.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMonthYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.rdbPendingSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpTodate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.GV1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblMonthYear As common.Controls.MyLabel
    Friend WithEvents dtpFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents cg As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents chkselectcustomer As common.Controls.MyRadioButton
    Friend WithEvents chkallcustomer As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dgvdiscount As common.MyCheckBoxGrid
    Friend WithEvents discountselect As common.Controls.MyRadioButton
    Friend WithEvents discountall As common.Controls.MyRadioButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GV1 As common.UserControls.MyRadGridView
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpTodate As common.Controls.MyDateTimePicker
    Friend WithEvents rdbPendingSummary As Telerik.WinControls.UI.RadRadioButton
End Class

