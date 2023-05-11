<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDetailOfForm2A
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.Todate = New common.Controls.MyDateTimePicker
        Me.fromdate = New common.Controls.MyDateTimePicker
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgVendor = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkVendorSelect = New common.Controls.MyRadioButton
        Me.chkVendorAll = New common.Controls.MyRadioButton
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Todate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox2.Controls.Add(Me.Todate)
        Me.RadGroupBox2.Controls.Add(Me.fromdate)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(6, 12)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(419, 405)
        Me.RadGroupBox2.TabIndex = 1
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(10, 235)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(397, 158)
        Me.RadGroupBox1.TabIndex = 311
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
        Me.cbgLocation.Size = New System.Drawing.Size(377, 108)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocSelect)
        Me.Panel1.Controls.Add(Me.chkLocAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(377, 20)
        Me.Panel1.TabIndex = 0
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
        'Todate
        '
        Me.Todate.CustomFormat = "dd/MM/yyyy"
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
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgVendor)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Vendor"
        Me.RadGroupBox3.Location = New System.Drawing.Point(9, 45)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(398, 184)
        Me.RadGroupBox3.TabIndex = 30
        Me.RadGroupBox3.Text = "Vendor"
        '
        'cbgVendor
        '
        Me.cbgVendor.CheckedValue = Nothing
        Me.cbgVendor.DataSource = Nothing
        Me.cbgVendor.DisplayMember = "Name"
        Me.cbgVendor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgVendor.Location = New System.Drawing.Point(10, 40)
        Me.cbgVendor.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgVendor.MyShowHeadrText = False
        Me.cbgVendor.Name = "cbgVendor"
        Me.cbgVendor.Size = New System.Drawing.Size(378, 134)
        Me.cbgVendor.TabIndex = 2
        Me.cbgVendor.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkVendorSelect)
        Me.Panel2.Controls.Add(Me.chkVendorAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(378, 20)
        Me.Panel2.TabIndex = 1
        '
        'chkVendorSelect
        '
        Me.chkVendorSelect.Location = New System.Drawing.Point(192, 1)
        Me.chkVendorSelect.MyLinkLable1 = Nothing
        Me.chkVendorSelect.MyLinkLable2 = Nothing
        Me.chkVendorSelect.Name = "chkVendorSelect"
        Me.chkVendorSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkVendorSelect.TabIndex = 2
        Me.chkVendorSelect.Text = "Select"
        '
        'chkVendorAll
        '
        Me.chkVendorAll.Location = New System.Drawing.Point(141, 1)
        Me.chkVendorAll.MyLinkLable1 = Nothing
        Me.chkVendorAll.MyLinkLable2 = Nothing
        Me.chkVendorAll.Name = "chkVendorAll"
        Me.chkVendorAll.Size = New System.Drawing.Size(33, 18)
        Me.chkVendorAll.TabIndex = 1
        Me.chkVendorAll.Text = "All"
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
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(357, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 31
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(6, 8)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 25
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(78, 8)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 24
        Me.btnprint.Text = "Print"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(438, 464)
        Me.SplitContainer1.SplitterDistance = 423
        Me.SplitContainer1.TabIndex = 2
        '
        'FrmDetailOfForm2A
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 464)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmDetailOfForm2A"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Detail Of Form2A"
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Todate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkVendorSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkVendorAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents Todate As common.Controls.MyDateTimePicker
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents fromdate As common.Controls.MyDateTimePicker
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgVendor As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkVendorSelect As common.Controls.MyRadioButton
    Friend WithEvents chkVendorAll As common.Controls.MyRadioButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

