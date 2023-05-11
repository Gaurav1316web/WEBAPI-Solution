<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSamplingReportSummary1
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.grpbxLocation = New Telerik.WinControls.UI.RadGroupBox
        Me.CbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.ChkLocationSelect = New common.Controls.MyRadioButton
        Me.ChklocationAll = New common.Controls.MyRadioButton
        Me.rdosummary = New common.Controls.MyRadioButton
        Me.rdodetail = New common.Controls.MyRadioButton
        Me.cg = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkselectcustomer = New common.Controls.MyRadioButton
        Me.chkallcustomer = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgroute = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkSelect = New common.Controls.MyRadioButton
        Me.chkAll = New common.Controls.MyRadioButton
        Me.dtptodate = New common.Controls.MyDateTimePicker
        Me.dtpFromdate = New common.Controls.MyDateTimePicker
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.RadLabel1 = New common.Controls.MyLabel
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.grpbxLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxLocation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.ChkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChklocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdosummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdodetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cg.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.grpbxLocation)
        Me.RadGroupBox1.Controls.Add(Me.rdosummary)
        Me.RadGroupBox1.Controls.Add(Me.rdodetail)
        Me.RadGroupBox1.Controls.Add(Me.cg)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.dtptodate)
        Me.RadGroupBox1.Controls.Add(Me.dtpFromdate)
        Me.RadGroupBox1.Controls.Add(Me.btnclose)
        Me.RadGroupBox1.Controls.Add(Me.btnreset)
        Me.RadGroupBox1.Controls.Add(Me.btnprint)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(545, 586)
        Me.RadGroupBox1.TabIndex = 0
        '
        'grpbxLocation
        '
        Me.grpbxLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxLocation.Controls.Add(Me.CbgLocation)
        Me.grpbxLocation.Controls.Add(Me.Panel3)
        Me.grpbxLocation.HeaderText = "Location"
        Me.grpbxLocation.Location = New System.Drawing.Point(9, 377)
        Me.grpbxLocation.Name = "grpbxLocation"
        Me.grpbxLocation.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxLocation.Size = New System.Drawing.Size(520, 162)
        Me.grpbxLocation.TabIndex = 36
        Me.grpbxLocation.Text = "Location"
        '
        'CbgLocation
        '
        Me.CbgLocation.CheckedValue = Nothing
        Me.CbgLocation.DataSource = Nothing
        Me.CbgLocation.DisplayMember = "Name"
        Me.CbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.CbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.CbgLocation.MyShowHeadrText = False
        Me.CbgLocation.Name = "CbgLocation"
        Me.CbgLocation.Size = New System.Drawing.Size(500, 112)
        Me.CbgLocation.TabIndex = 2
        Me.CbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ChkLocationSelect)
        Me.Panel3.Controls.Add(Me.ChklocationAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(500, 20)
        Me.Panel3.TabIndex = 1
        '
        'ChkLocationSelect
        '
        Me.ChkLocationSelect.Location = New System.Drawing.Point(231, 1)
        Me.ChkLocationSelect.MyLinkLable1 = Nothing
        Me.ChkLocationSelect.MyLinkLable2 = Nothing
        Me.ChkLocationSelect.Name = "ChkLocationSelect"
        Me.ChkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.ChkLocationSelect.TabIndex = 2
        Me.ChkLocationSelect.Text = "Select"
        '
        'ChklocationAll
        '
        Me.ChklocationAll.Location = New System.Drawing.Point(179, 1)
        Me.ChklocationAll.MyLinkLable1 = Nothing
        Me.ChklocationAll.MyLinkLable2 = Nothing
        Me.ChklocationAll.Name = "ChklocationAll"
        Me.ChklocationAll.Size = New System.Drawing.Size(33, 18)
        Me.ChklocationAll.TabIndex = 1
        Me.ChklocationAll.Text = "All"
        '
        'rdosummary
        '
        Me.rdosummary.Location = New System.Drawing.Point(433, 9)
        Me.rdosummary.MyLinkLable1 = Nothing
        Me.rdosummary.MyLinkLable2 = Nothing
        Me.rdosummary.Name = "rdosummary"
        Me.rdosummary.Size = New System.Drawing.Size(67, 18)
        Me.rdosummary.TabIndex = 36
        Me.rdosummary.Text = "Summary"
        '
        'rdodetail
        '
        Me.rdodetail.Location = New System.Drawing.Point(358, 9)
        Me.rdodetail.MyLinkLable1 = Nothing
        Me.rdodetail.MyLinkLable2 = Nothing
        Me.rdodetail.Name = "rdodetail"
        Me.rdodetail.Size = New System.Drawing.Size(49, 18)
        Me.rdodetail.TabIndex = 37
        Me.rdodetail.Text = "Detail"
        '
        'cg
        '
        Me.cg.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cg.Controls.Add(Me.cbgCustomer)
        Me.cg.Controls.Add(Me.Panel1)
        Me.cg.HeaderText = "Customer"
        Me.cg.Location = New System.Drawing.Point(9, 209)
        Me.cg.Name = "cg"
        Me.cg.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cg.Size = New System.Drawing.Size(520, 162)
        Me.cg.TabIndex = 35
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
        Me.cbgCustomer.Size = New System.Drawing.Size(500, 112)
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
        Me.Panel1.Size = New System.Drawing.Size(500, 20)
        Me.Panel1.TabIndex = 1
        '
        'chkselectcustomer
        '
        Me.chkselectcustomer.Location = New System.Drawing.Point(231, 1)
        Me.chkselectcustomer.MyLinkLable1 = Nothing
        Me.chkselectcustomer.MyLinkLable2 = Nothing
        Me.chkselectcustomer.Name = "chkselectcustomer"
        Me.chkselectcustomer.Size = New System.Drawing.Size(50, 18)
        Me.chkselectcustomer.TabIndex = 2
        Me.chkselectcustomer.Text = "Select"
        '
        'chkallcustomer
        '
        Me.chkallcustomer.Location = New System.Drawing.Point(179, 1)
        Me.chkallcustomer.MyLinkLable1 = Nothing
        Me.chkallcustomer.MyLinkLable2 = Nothing
        Me.chkallcustomer.Name = "chkallcustomer"
        Me.chkallcustomer.Size = New System.Drawing.Size(33, 18)
        Me.chkallcustomer.TabIndex = 1
        Me.chkallcustomer.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgroute)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "Route"
        Me.RadGroupBox2.Location = New System.Drawing.Point(12, 38)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(520, 165)
        Me.RadGroupBox2.TabIndex = 34
        Me.RadGroupBox2.Text = "Route"
        '
        'cbgroute
        '
        Me.cbgroute.CheckedValue = Nothing
        Me.cbgroute.DataSource = Nothing
        Me.cbgroute.DisplayMember = "Name"
        Me.cbgroute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgroute.Location = New System.Drawing.Point(10, 40)
        Me.cbgroute.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgroute.MyShowHeadrText = False
        Me.cbgroute.Name = "cbgroute"
        Me.cbgroute.Size = New System.Drawing.Size(500, 115)
        Me.cbgroute.TabIndex = 2
        Me.cbgroute.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkSelect)
        Me.Panel2.Controls.Add(Me.chkAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(500, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(231, 1)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 2
        Me.chkSelect.Text = "Select"
        '
        'chkAll
        '
        Me.chkAll.Location = New System.Drawing.Point(179, 1)
        Me.chkAll.MyLinkLable1 = Nothing
        Me.chkAll.MyLinkLable2 = Nothing
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(33, 18)
        Me.chkAll.TabIndex = 1
        Me.chkAll.Text = "All"
        '
        'dtptodate
        '
        Me.dtptodate.CustomFormat = "dd/MM/yyyy"
        Me.dtptodate.Location = New System.Drawing.Point(228, 9)
        Me.dtptodate.MendatroryField = False
        Me.dtptodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.MyLinkLable1 = Nothing
        Me.dtptodate.MyLinkLable2 = Nothing
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtptodate.Size = New System.Drawing.Size(87, 20)
        Me.dtptodate.TabIndex = 33
        Me.dtptodate.TabStop = False
        Me.dtptodate.Text = "Friday, August 05, 2011"
        Me.dtptodate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'dtpFromdate
        '
        Me.dtpFromdate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate.Location = New System.Drawing.Point(78, 9)
        Me.dtpFromdate.MendatroryField = False
        Me.dtpFromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.MyLinkLable1 = Nothing
        Me.dtpFromdate.MyLinkLable2 = Nothing
        Me.dtpFromdate.Name = "dtpFromdate"
        Me.dtpFromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate.Size = New System.Drawing.Size(87, 20)
        Me.dtpFromdate.TabIndex = 32
        Me.dtpFromdate.TabStop = False
        Me.dtpFromdate.Text = "Friday, August 05, 2011"
        Me.dtpFromdate.Value = New Date(2011, 8, 5, 17, 38, 42, 656)
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(451, 553)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 31
        Me.btnclose.Text = "&Close"
        '
        'btnreset
        '
        Me.btnreset.Location = New System.Drawing.Point(13, 553)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 30
        Me.btnreset.Text = "&Reset"
        '
        'btnprint
        '
        Me.btnprint.Location = New System.Drawing.Point(91, 553)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 29
        Me.btnprint.Text = "&Print"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(175, 9)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 27
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 9)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 28
        Me.RadLabel1.Text = "From Date"
        '
        'FrmSamplingReportSummary1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(569, 596)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "FrmSamplingReportSummary1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Sampling Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.grpbxLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxLocation.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.ChkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChklocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdosummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdodetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cg.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkselectcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkallcustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtptodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgroute As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkAll As common.Controls.MyRadioButton
    Friend WithEvents dtptodate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromdate As common.Controls.MyDateTimePicker
    Friend WithEvents cg As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkselectcustomer As common.Controls.MyRadioButton
    Friend WithEvents chkallcustomer As common.Controls.MyRadioButton
    Friend WithEvents rdodetail As common.Controls.MyRadioButton
    Friend WithEvents rdosummary As common.Controls.MyRadioButton
    Friend WithEvents grpbxLocation As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents CbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ChkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents ChklocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
End Class

