<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDVAT31
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
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.cg = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkselectcustomer = New common.Controls.MyRadioButton
        Me.chkallcustomer = New common.Controls.MyRadioButton
        Me.Todate = New common.Controls.MyDateTimePicker
        Me.fromdate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnrefresh = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.chkexcel = New common.Controls.MyCheckBox
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.gv = New common.UserControls.MyRadGridView
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cg.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Todate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkexcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.cg)
        Me.RadGroupBox2.Controls.Add(Me.Todate)
        Me.RadGroupBox2.Controls.Add(Me.fromdate)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(417, 497)
        Me.RadGroupBox2.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel2)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(9, 262)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(397, 222)
        Me.RadGroupBox1.TabIndex = 312
        Me.RadGroupBox1.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(377, 172)
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
        Me.Panel2.Size = New System.Drawing.Size(377, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(177, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(126, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.TabStop = True
        Me.chkLocAll.Text = "All"
        Me.chkLocAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'cg
        '
        Me.cg.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cg.Controls.Add(Me.cbgCustomer)
        Me.cg.Controls.Add(Me.Panel1)
        Me.cg.HeaderText = "Customer"
        Me.cg.Location = New System.Drawing.Point(9, 47)
        Me.cg.Name = "cg"
        Me.cg.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cg.Size = New System.Drawing.Size(397, 209)
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
        Me.cbgCustomer.Size = New System.Drawing.Size(377, 159)
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
        Me.Panel1.Size = New System.Drawing.Size(377, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkselectcustomer
        '
        Me.chkselectcustomer.Location = New System.Drawing.Point(199, 1)
        Me.chkselectcustomer.MyLinkLable1 = Nothing
        Me.chkselectcustomer.MyLinkLable2 = Nothing
        Me.chkselectcustomer.Name = "chkselectcustomer"
        Me.chkselectcustomer.Size = New System.Drawing.Size(50, 18)
        Me.chkselectcustomer.TabIndex = 2
        Me.chkselectcustomer.Text = "Select"
        '
        'chkallcustomer
        '
        Me.chkallcustomer.Location = New System.Drawing.Point(147, 1)
        Me.chkallcustomer.MyLinkLable1 = Nothing
        Me.chkallcustomer.MyLinkLable2 = Nothing
        Me.chkallcustomer.Name = "chkallcustomer"
        Me.chkallcustomer.Size = New System.Drawing.Size(33, 18)
        Me.chkallcustomer.TabIndex = 1
        Me.chkallcustomer.Text = "All"
        '
        'Todate
        '
        Me.Todate.CustomFormat = "dd/MM/yyyy"
        Me.Todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Todate.Location = New System.Drawing.Point(268, 11)
        Me.Todate.MendatroryField = False
        Me.Todate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Todate.MyLinkLable1 = Nothing
        Me.Todate.MyLinkLable2 = Nothing
        Me.Todate.Name = "Todate"
        Me.Todate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Todate.Size = New System.Drawing.Size(129, 20)
        Me.Todate.TabIndex = 27
        Me.Todate.TabStop = False
        Me.Todate.Value = New Date(1753, 1, 1, 0, 0, 0, 0)
        '
        'fromdate
        '
        Me.fromdate.CustomFormat = "dd/MM/yyyy"
        Me.fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromdate.Location = New System.Drawing.Point(74, 11)
        Me.fromdate.MendatroryField = False
        Me.fromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromdate.MyLinkLable1 = Nothing
        Me.fromdate.MyLinkLable2 = Nothing
        Me.fromdate.Name = "fromdate"
        Me.fromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromdate.Size = New System.Drawing.Size(121, 20)
        Me.fromdate.TabIndex = 26
        Me.fromdate.TabStop = False
        Me.fromdate.Value = New Date(1753, 1, 1, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.Location = New System.Drawing.Point(211, 11)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 28
        Me.MyLabel1.Text = "To Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.Location = New System.Drawing.Point(9, 11)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 29
        Me.MyLabel2.Text = "From Date"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(444, 617)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Panel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.chkexcel)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(423, 569)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnExport)
        Me.Panel3.Controls.Add(Me.btnclose)
        Me.Panel3.Controls.Add(Me.btnreset)
        Me.Panel3.Controls.Add(Me.btnrefresh)
        Me.Panel3.Controls.Add(Me.btnprint)
        Me.Panel3.Location = New System.Drawing.Point(2, 532)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(418, 36)
        Me.Panel3.TabIndex = 4
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(232, 7)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 18)
        Me.btnExport.TabIndex = 330
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.XpertERPCommanServices.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.XpertERPCommanServices.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(338, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 32
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(84, 7)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 28
        Me.btnreset.Text = "Reset"
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(10, 7)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(68, 18)
        Me.btnrefresh.TabIndex = 27
        Me.btnrefresh.Text = ">>>"
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(158, 7)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 25
        Me.btnprint.Text = "Print"
        '
        'chkexcel
        '
        Me.chkexcel.Location = New System.Drawing.Point(77, 508)
        Me.chkexcel.MyLinkLable1 = Nothing
        Me.chkexcel.MyLinkLable2 = Nothing
        Me.chkexcel.Name = "chkexcel"
        Me.chkexcel.Size = New System.Drawing.Size(96, 18)
        Me.chkexcel.TabIndex = 26
        Me.chkexcel.Tag1 = Nothing
        Me.chkexcel.Text = "Export To Excel"
        Me.chkexcel.Visible = False
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(423, 543)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.EnableGrouping = False
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.Size = New System.Drawing.Size(423, 543)
        Me.gv.TabIndex = 1
        Me.gv.Text = "RadGridView1"
        '
        'FrmDVAT31
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 617)
        Me.Controls.Add(Me.RadPageView1)
        Me.Name = "FrmDVAT31"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmDVAT31"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cg.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Todate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnrefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkexcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Todate As common.Controls.MyDateTimePicker
    Friend WithEvents fromdate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cg As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkselectcustomer As common.Controls.MyRadioButton
    Friend WithEvents chkallcustomer As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnrefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkexcel As common.Controls.MyCheckBox
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
End Class

