<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptGatePassVSActual
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel2 = New Telerik.WinControls.UI.RadLabel
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkcustomerAll = New common.Controls.MyRadioButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.Locationgb = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLoaction = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgGatePass = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdbtnGateSelect = New common.Controls.MyRadioButton
        Me.rdbtnGateAll = New common.Controls.MyRadioButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.rdbtnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkcustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Locationgb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.rdbtnGateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnGateAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox3.Controls.Add(Me.ToDate)
        Me.RadGroupBox3.Controls.Add(Me.fromDate)
        Me.RadGroupBox3.HeaderText = "Select Date"
        Me.RadGroupBox3.Location = New System.Drawing.Point(13, 9)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(387, 42)
        Me.RadGroupBox3.TabIndex = 55
        Me.RadGroupBox3.Text = "Select Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(141, 14)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 14)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(32, 18)
        Me.RadLabel1.TabIndex = 2
        Me.RadLabel1.Text = "From"
        '
        'ToDate
        '
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(166, 14)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(84, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "7/26/2013 12:00 AM"
        Me.ToDate.Value = New Date(2013, 7, 26, 0, 0, 0, 0)
        '
        'fromDate
        '
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(51, 12)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(84, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "7/26/2013 12:00 AM"
        Me.fromDate.Value = New Date(2013, 7, 26, 0, 0, 0, 0)
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox4.Controls.Add(Me.Panel4)
        Me.RadGroupBox4.HeaderText = "Customer"
        Me.RadGroupBox4.HeaderTextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 52)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(387, 137)
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
        Me.cbgCustomer.Size = New System.Drawing.Size(367, 87)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkCustomerSelect)
        Me.Panel4.Controls.Add(Me.chkcustomerAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(367, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(164, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkcustomerAll
        '
        Me.chkcustomerAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkcustomerAll.Location = New System.Drawing.Point(113, 1)
        Me.chkcustomerAll.MyLinkLable1 = Nothing
        Me.chkcustomerAll.MyLinkLable2 = Nothing
        Me.chkcustomerAll.Name = "chkcustomerAll"
        Me.chkcustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkcustomerAll.TabIndex = 0
        Me.chkcustomerAll.TabStop = True
        Me.chkcustomerAll.Text = "All"
        Me.chkcustomerAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(319, 4)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(70, 18)
        Me.rdbtnclose.TabIndex = 15
        Me.rdbtnclose.Text = "Close"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Locationgb)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(417, 492)
        Me.RadGroupBox1.TabIndex = 122
        '
        'Locationgb
        '
        Me.Locationgb.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Locationgb.Controls.Add(Me.cbgLoaction)
        Me.Locationgb.Controls.Add(Me.Panel2)
        Me.Locationgb.HeaderText = "Location"
        Me.Locationgb.Location = New System.Drawing.Point(16, 343)
        Me.Locationgb.Name = "Locationgb"
        Me.Locationgb.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Locationgb.Size = New System.Drawing.Size(385, 136)
        Me.Locationgb.TabIndex = 113
        Me.Locationgb.Text = "Location"
        '
        'cbgLoaction
        '
        Me.cbgLoaction.CheckedValue = Nothing
        Me.cbgLoaction.DataSource = Nothing
        Me.cbgLoaction.DisplayMember = "Name"
        Me.cbgLoaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLoaction.Location = New System.Drawing.Point(10, 40)
        Me.cbgLoaction.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLoaction.MyShowHeadrText = False
        Me.cbgLoaction.Name = "cbgLoaction"
        Me.cbgLoaction.Size = New System.Drawing.Size(365, 86)
        Me.cbgLoaction.TabIndex = 1
        Me.cbgLoaction.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(365, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(168, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(101, 1)
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
        Me.RadGroupBox2.Controls.Add(Me.cbgGatePass)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "GatePass"
        Me.RadGroupBox2.HeaderTextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.RadGroupBox2.Location = New System.Drawing.Point(14, 195)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(387, 147)
        Me.RadGroupBox2.TabIndex = 112
        Me.RadGroupBox2.Text = "GatePass"
        '
        'cbgGatePass
        '
        Me.cbgGatePass.CheckedValue = Nothing
        Me.cbgGatePass.DataSource = Nothing
        Me.cbgGatePass.DisplayMember = "Name"
        Me.cbgGatePass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgGatePass.Location = New System.Drawing.Point(10, 40)
        Me.cbgGatePass.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgGatePass.MyShowHeadrText = False
        Me.cbgGatePass.Name = "cbgGatePass"
        Me.cbgGatePass.Size = New System.Drawing.Size(367, 97)
        Me.cbgGatePass.TabIndex = 1
        Me.cbgGatePass.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdbtnGateSelect)
        Me.Panel1.Controls.Add(Me.rdbtnGateAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(367, 20)
        Me.Panel1.TabIndex = 0
        '
        'rdbtnGateSelect
        '
        Me.rdbtnGateSelect.Location = New System.Drawing.Point(164, 1)
        Me.rdbtnGateSelect.MyLinkLable1 = Nothing
        Me.rdbtnGateSelect.MyLinkLable2 = Nothing
        Me.rdbtnGateSelect.Name = "rdbtnGateSelect"
        Me.rdbtnGateSelect.Size = New System.Drawing.Size(50, 18)
        Me.rdbtnGateSelect.TabIndex = 1
        Me.rdbtnGateSelect.Text = "Select"
        '
        'rdbtnGateAll
        '
        Me.rdbtnGateAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbtnGateAll.Location = New System.Drawing.Point(113, 1)
        Me.rdbtnGateAll.MyLinkLable1 = Nothing
        Me.rdbtnGateAll.MyLinkLable2 = Nothing
        Me.rdbtnGateAll.Name = "rdbtnGateAll"
        Me.rdbtnGateAll.Size = New System.Drawing.Size(33, 18)
        Me.rdbtnGateAll.TabIndex = 0
        Me.rdbtnGateAll.TabStop = True
        Me.rdbtnGateAll.Text = "All"
        Me.rdbtnGateAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(23, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(72, 18)
        Me.btnReset.TabIndex = 16
        Me.btnReset.Text = "Reset"
        '
        'rdbtnprint
        '
        Me.rdbtnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnprint.Location = New System.Drawing.Point(101, 4)
        Me.rdbtnprint.Name = "rdbtnprint"
        Me.rdbtnprint.Size = New System.Drawing.Size(72, 18)
        Me.rdbtnprint.TabIndex = 14
        Me.rdbtnprint.Text = "Print"
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
        Me.SplitContainer1.Size = New System.Drawing.Size(417, 527)
        Me.SplitContainer1.SplitterDistance = 492
        Me.SplitContainer1.TabIndex = 4
        '
        'RptGatePassVSActual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 527)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptGatePassVSActual"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "GatePass V/S Actual"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkcustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.Locationgb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Locationgb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.rdbtnGateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnGateAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel2 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents ToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkcustomerAll As common.Controls.MyRadioButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgGatePass As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdbtnGateSelect As common.Controls.MyRadioButton
    Friend WithEvents rdbtnGateAll As common.Controls.MyRadioButton
    Friend WithEvents Locationgb As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLoaction As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
End Class

