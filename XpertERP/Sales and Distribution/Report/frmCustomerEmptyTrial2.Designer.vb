<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomerEmptyTrial2
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
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.CbZero = New System.Windows.Forms.CheckBox
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLOcSelect = New common.Controls.MyRadioButton
        Me.chkLOcAll = New common.Controls.MyRadioButton
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Customer = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkSelect = New common.Controls.MyRadioButton
        Me.chkSelectAll = New common.Controls.MyRadioButton
        Me.lblToRouteDesc = New common.Controls.MyLabel
        Me.lblfrmRouteDesc = New common.Controls.MyLabel
        Me.dtpend = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.dtpstart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLOcSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLOcAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Customer.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfrmRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.CbZero)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox5.Controls.Add(Me.Customer)
        Me.RadGroupBox5.Controls.Add(Me.lblToRouteDesc)
        Me.RadGroupBox5.Controls.Add(Me.lblfrmRouteDesc)
        Me.RadGroupBox5.Controls.Add(Me.dtpend)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox5.Controls.Add(Me.dtpstart)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(14, 13)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(518, 416)
        Me.RadGroupBox5.TabIndex = 5
        '
        'CbZero
        '
        Me.CbZero.AutoSize = True
        Me.CbZero.Location = New System.Drawing.Point(370, 9)
        Me.CbZero.Name = "CbZero"
        Me.CbZero.Size = New System.Drawing.Size(120, 17)
        Me.CbZero.TabIndex = 51
        Me.CbZero.Text = "With Zero Balance"
        Me.CbZero.UseVisualStyleBackColor = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(9, 223)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(501, 181)
        Me.RadGroupBox1.TabIndex = 50
        Me.RadGroupBox1.Text = "Location"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLOcSelect)
        Me.Panel1.Controls.Add(Me.chkLOcAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(481, 20)
        Me.Panel1.TabIndex = 26
        '
        'chkLOcSelect
        '
        Me.chkLOcSelect.Location = New System.Drawing.Point(254, 1)
        Me.chkLOcSelect.MyLinkLable1 = Nothing
        Me.chkLOcSelect.MyLinkLable2 = Nothing
        Me.chkLOcSelect.Name = "chkLOcSelect"
        Me.chkLOcSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLOcSelect.TabIndex = 1
        Me.chkLOcSelect.Text = "Select"
        '
        'chkLOcAll
        '
        Me.chkLOcAll.Location = New System.Drawing.Point(187, 1)
        Me.chkLOcAll.MyLinkLable1 = Nothing
        Me.chkLOcAll.MyLinkLable2 = Nothing
        Me.chkLOcAll.Name = "chkLOcAll"
        Me.chkLOcAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLOcAll.TabIndex = 0
        Me.chkLOcAll.Text = "All"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Location = New System.Drawing.Point(10, 37)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(484, 135)
        Me.cbgLocation.TabIndex = 0
        Me.cbgLocation.ValueMember = "Code"
        '
        'Customer
        '
        Me.Customer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Customer.Controls.Add(Me.cbgCustomer)
        Me.Customer.Controls.Add(Me.Panel3)
        Me.Customer.HeaderText = "Customer"
        Me.Customer.Location = New System.Drawing.Point(9, 36)
        Me.Customer.Name = "Customer"
        Me.Customer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Customer.Size = New System.Drawing.Size(501, 181)
        Me.Customer.TabIndex = 49
        Me.Customer.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(481, 131)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkSelect)
        Me.Panel3.Controls.Add(Me.chkSelectAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(481, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkSelect
        '
        Me.chkSelect.Location = New System.Drawing.Point(255, 1)
        Me.chkSelect.MyLinkLable1 = Nothing
        Me.chkSelect.MyLinkLable2 = Nothing
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkSelect.TabIndex = 1
        Me.chkSelect.Text = "Select"
        '
        'chkSelectAll
        '
        Me.chkSelectAll.Location = New System.Drawing.Point(188, 1)
        Me.chkSelectAll.MyLinkLable1 = Nothing
        Me.chkSelectAll.MyLinkLable2 = Nothing
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSelectAll.TabIndex = 0
        Me.chkSelectAll.Text = "All"
        '
        'lblToRouteDesc
        '
        Me.lblToRouteDesc.BorderVisible = True
        Me.lblToRouteDesc.Location = New System.Drawing.Point(182, 82)
        Me.lblToRouteDesc.Name = "lblToRouteDesc"
        Me.lblToRouteDesc.Size = New System.Drawing.Size(2, 2)
        Me.lblToRouteDesc.TabIndex = 0
        '
        'lblfrmRouteDesc
        '
        Me.lblfrmRouteDesc.BorderVisible = True
        Me.lblfrmRouteDesc.Location = New System.Drawing.Point(182, 25)
        Me.lblfrmRouteDesc.Name = "lblfrmRouteDesc"
        Me.lblfrmRouteDesc.Size = New System.Drawing.Size(2, 2)
        Me.lblfrmRouteDesc.TabIndex = 25
        '
        'dtpend
        '
        Me.dtpend.CustomFormat = "dd/MM/yyyy"
        Me.dtpend.Location = New System.Drawing.Point(256, 6)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(96, 20)
        Me.dtpend.TabIndex = 1
        Me.dtpend.TabStop = False
        Me.dtpend.Text = "Wednesday, November 16, 2011"
        Me.dtpend.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(10, 8)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel7.TabIndex = 11
        Me.RadLabel7.Text = "Start Date"
        '
        'dtpstart
        '
        Me.dtpstart.CustomFormat = "dd/MM/yyyy"
        Me.dtpstart.Location = New System.Drawing.Point(80, 8)
        Me.dtpstart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Size = New System.Drawing.Size(96, 20)
        Me.dtpstart.TabIndex = 0
        Me.dtpstart.TabStop = False
        Me.dtpstart.Text = "Wednesday, November 16, 2011"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(190, 8)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel8.TabIndex = 12
        Me.RadLabel8.Text = "End Date"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(464, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(14, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 5
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(92, 3)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 4
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox5)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(546, 468)
        Me.SplitContainer1.SplitterDistance = 436
        Me.SplitContainer1.TabIndex = 6
        '
        'FrmCustomerEmptyTrial2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 468)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCustomerEmptyTrial2"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Empty Trial"
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLOcSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLOcAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Customer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Customer.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfrmRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Customer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkSelectAll As common.Controls.MyRadioButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLOcSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLOcAll As common.Controls.MyRadioButton
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents CbZero As System.Windows.Forms.CheckBox
    Friend WithEvents lblToRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblfrmRouteDesc As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

