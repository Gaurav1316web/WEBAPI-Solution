<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptInvoiceAgainstInward
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
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkInvoiceSelect = New common.Controls.MyRadioButton
        Me.chkInvoiceAll = New common.Controls.MyRadioButton
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel3 = New Telerik.WinControls.UI.RadLabel
        Me.ddlType = New Telerik.WinControls.UI.RadDropDownList
        Me.chkTransporter = New Telerik.WinControls.UI.RadCheckBox
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgroute = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkSelectRoute = New common.Controls.MyRadioButton
        Me.chkAllRoute = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLoc = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdbtnSelect = New common.Controls.MyRadioButton
        Me.rdbtnAll = New common.Controls.MyRadioButton
        Me.rdbtnprint = New Telerik.WinControls.UI.RadButton
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkInvoiceSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInvoiceAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkSelectRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAllRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rdbtnSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(468, 3)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(70, 18)
        Me.rdbtnclose.TabIndex = 15
        Me.rdbtnclose.Text = "Close"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(6, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox4.Controls.Add(Me.Panel4)
        Me.RadGroupBox4.HeaderText = "Customer"
        Me.RadGroupBox4.HeaderTextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 57)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(525, 168)
        Me.RadGroupBox4.TabIndex = 112
        Me.RadGroupBox4.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(505, 118)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkInvoiceSelect)
        Me.Panel4.Controls.Add(Me.chkInvoiceAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(505, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkInvoiceSelect
        '
        Me.chkInvoiceSelect.Location = New System.Drawing.Point(164, 1)
        Me.chkInvoiceSelect.MyLinkLable1 = Nothing
        Me.chkInvoiceSelect.MyLinkLable2 = Nothing
        Me.chkInvoiceSelect.Name = "chkInvoiceSelect"
        Me.chkInvoiceSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkInvoiceSelect.TabIndex = 1
        Me.chkInvoiceSelect.Text = "Select"
        '
        'chkInvoiceAll
        '
        Me.chkInvoiceAll.Location = New System.Drawing.Point(113, 1)
        Me.chkInvoiceAll.MyLinkLable1 = Nothing
        Me.chkInvoiceAll.MyLinkLable2 = Nothing
        Me.chkInvoiceAll.Name = "chkInvoiceAll"
        Me.chkInvoiceAll.Size = New System.Drawing.Size(33, 18)
        Me.chkInvoiceAll.TabIndex = 0
        Me.chkInvoiceAll.TabStop = True
        Me.chkInvoiceAll.Text = "All"
        Me.chkInvoiceAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(134, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(12, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(72, 18)
        Me.btnReset.TabIndex = 16
        Me.btnReset.Text = "Reset"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox3.Controls.Add(Me.ddlType)
        Me.RadGroupBox3.Controls.Add(Me.chkTransporter)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(525, 42)
        Me.RadGroupBox3.TabIndex = 55
        Me.RadGroupBox3.Text = "Select Date"
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(261, 14)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 18)
        Me.RadLabel3.TabIndex = 5
        Me.RadLabel3.Text = "Type"
        '
        'ddlType
        '
        Me.ddlType.Location = New System.Drawing.Point(300, 12)
        Me.ddlType.Name = "ddlType"
        Me.ddlType.Size = New System.Drawing.Size(135, 20)
        Me.ddlType.TabIndex = 0
        '
        'chkTransporter
        '
        Me.chkTransporter.Location = New System.Drawing.Point(441, 13)
        Me.chkTransporter.Name = "chkTransporter"
        Me.chkTransporter.Size = New System.Drawing.Size(78, 18)
        Me.chkTransporter.TabIndex = 4
        Me.chkTransporter.Text = "Transporter"
        '
        'ToDate
        '
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(159, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "31/07/2013 12:00 AM"
        Me.ToDate.Value = New Date(2013, 7, 31, 0, 0, 0, 0)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "31/07/2013 12:00 AM"
        Me.fromDate.Value = New Date(2013, 7, 31, 0, 0, 0, 0)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnprint)
        Me.SplitContainer1.Size = New System.Drawing.Size(553, 625)
        Me.SplitContainer1.SplitterDistance = 596
        Me.SplitContainer1.TabIndex = 4
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox5)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(553, 596)
        Me.RadGroupBox1.TabIndex = 122
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.cbgroute)
        Me.RadGroupBox5.Controls.Add(Me.Panel6)
        Me.RadGroupBox5.HeaderText = "Route"
        Me.RadGroupBox5.Location = New System.Drawing.Point(16, 405)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(522, 147)
        Me.RadGroupBox5.TabIndex = 21
        Me.RadGroupBox5.Text = "Route"
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
        Me.cbgroute.Size = New System.Drawing.Size(502, 97)
        Me.cbgroute.TabIndex = 2
        Me.cbgroute.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkSelectRoute)
        Me.Panel6.Controls.Add(Me.chkAllRoute)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(502, 20)
        Me.Panel6.TabIndex = 1
        '
        'chkSelectRoute
        '
        Me.chkSelectRoute.Location = New System.Drawing.Point(154, 1)
        Me.chkSelectRoute.MyLinkLable1 = Nothing
        Me.chkSelectRoute.MyLinkLable2 = Nothing
        Me.chkSelectRoute.Name = "chkSelectRoute"
        Me.chkSelectRoute.Size = New System.Drawing.Size(50, 18)
        Me.chkSelectRoute.TabIndex = 2
        Me.chkSelectRoute.Text = "Select"
        '
        'chkAllRoute
        '
        Me.chkAllRoute.Location = New System.Drawing.Point(99, 1)
        Me.chkAllRoute.MyLinkLable1 = Nothing
        Me.chkAllRoute.MyLinkLable2 = Nothing
        Me.chkAllRoute.Name = "chkAllRoute"
        Me.chkAllRoute.Size = New System.Drawing.Size(33, 18)
        Me.chkAllRoute.TabIndex = 1
        Me.chkAllRoute.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgLoc)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.HeaderTextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 231)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(526, 168)
        Me.RadGroupBox2.TabIndex = 112
        Me.RadGroupBox2.Text = "Location"
        '
        'cbgLoc
        '
        Me.cbgLoc.CheckedValue = Nothing
        Me.cbgLoc.DataSource = Nothing
        Me.cbgLoc.DisplayMember = "Name"
        Me.cbgLoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLoc.Location = New System.Drawing.Point(10, 40)
        Me.cbgLoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLoc.MyShowHeadrText = False
        Me.cbgLoc.Name = "cbgLoc"
        Me.cbgLoc.Size = New System.Drawing.Size(506, 118)
        Me.cbgLoc.TabIndex = 1
        Me.cbgLoc.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdbtnSelect)
        Me.Panel1.Controls.Add(Me.rdbtnAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(506, 20)
        Me.Panel1.TabIndex = 0
        '
        'rdbtnSelect
        '
        Me.rdbtnSelect.Location = New System.Drawing.Point(164, 1)
        Me.rdbtnSelect.MyLinkLable1 = Nothing
        Me.rdbtnSelect.MyLinkLable2 = Nothing
        Me.rdbtnSelect.Name = "rdbtnSelect"
        Me.rdbtnSelect.Size = New System.Drawing.Size(50, 18)
        Me.rdbtnSelect.TabIndex = 1
        Me.rdbtnSelect.Text = "Select"
        '
        'rdbtnAll
        '
        Me.rdbtnAll.Location = New System.Drawing.Point(113, 1)
        Me.rdbtnAll.MyLinkLable1 = Nothing
        Me.rdbtnAll.MyLinkLable2 = Nothing
        Me.rdbtnAll.Name = "rdbtnAll"
        Me.rdbtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rdbtnAll.TabIndex = 0
        Me.rdbtnAll.TabStop = True
        Me.rdbtnAll.Text = "All"
        Me.rdbtnAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbtnprint
        '
        Me.rdbtnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnprint.Location = New System.Drawing.Point(88, 2)
        Me.rdbtnprint.Name = "rdbtnprint"
        Me.rdbtnprint.Size = New System.Drawing.Size(72, 18)
        Me.rdbtnprint.TabIndex = 14
        Me.rdbtnprint.Text = "Print"
        '
        'RptInvoiceAgainstInward
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(553, 625)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptInvoiceAgainstInward"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Invoice Against Inward"
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkInvoiceSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInvoiceAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkSelectRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAllRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rdbtnSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkInvoiceSelect As common.Controls.MyRadioButton
    Friend WithEvents chkInvoiceAll As common.Controls.MyRadioButton
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoc As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdbtnSelect As common.Controls.MyRadioButton
    Friend WithEvents rdbtnAll As common.Controls.MyRadioButton
    Friend WithEvents chkTransporter As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgroute As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkSelectRoute As common.Controls.MyRadioButton
    Friend WithEvents chkAllRoute As common.Controls.MyRadioButton
    Friend WithEvents RadLabel3 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents ddlType As Telerik.WinControls.UI.RadDropDownList
End Class

