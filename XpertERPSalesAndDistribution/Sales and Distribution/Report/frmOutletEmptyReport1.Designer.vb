Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOutletEmptyReport1
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdobtndetails = New common.Controls.MyRadioButton
        Me.rdobtnSummary = New common.Controls.MyRadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdbtnPosted = New common.Controls.MyRadioButton
        Me.rdbtnAll = New common.Controls.MyRadioButton
        Me.ChkZero = New System.Windows.Forms.CheckBox
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cgvtemplate = New common.MyCheckBoxGrid
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chktempselect = New common.Controls.MyRadioButton
        Me.chktempall = New common.Controls.MyRadioButton
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
        Me.GroupBox2.SuspendLayout()
        CType(Me.rdobtndetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rdbtnPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RadGroupBox5.Controls.Add(Me.GroupBox2)
        Me.RadGroupBox5.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox5.Controls.Add(Me.ChkZero)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox5.Controls.Add(Me.RadGroupBox1)
        Me.RadGroupBox5.Controls.Add(Me.Customer)
        Me.RadGroupBox5.Controls.Add(Me.lblToRouteDesc)
        Me.RadGroupBox5.Controls.Add(Me.lblfrmRouteDesc)
        Me.RadGroupBox5.Controls.Add(Me.dtpend)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox5.Controls.Add(Me.dtpstart)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(22, 18)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(579, 610)
        Me.RadGroupBox5.TabIndex = 6
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdobtndetails)
        Me.GroupBox2.Controls.Add(Me.rdobtnSummary)
        Me.GroupBox2.Location = New System.Drawing.Point(304, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(140, 36)
        Me.GroupBox2.TabIndex = 115
        Me.GroupBox2.TabStop = False
        '
        'rdobtndetails
        '
        Me.rdobtndetails.Location = New System.Drawing.Point(76, 10)
        Me.rdobtndetails.MyLinkLable1 = Nothing
        Me.rdobtndetails.MyLinkLable2 = Nothing
        Me.rdobtndetails.Name = "rdobtndetails"
        Me.rdobtndetails.Size = New System.Drawing.Size(54, 18)
        Me.rdobtndetails.TabIndex = 113
        Me.rdobtndetails.Text = "Details"
        '
        'rdobtnSummary
        '
        Me.rdobtnSummary.Location = New System.Drawing.Point(3, 9)
        Me.rdobtnSummary.MyLinkLable1 = Nothing
        Me.rdobtnSummary.MyLinkLable2 = Nothing
        Me.rdobtnSummary.Name = "rdobtnSummary"
        Me.rdobtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdobtnSummary.TabIndex = 112
        Me.rdobtnSummary.TabStop = True
        Me.rdobtnSummary.Text = "Summary"
        Me.rdobtnSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbtnPosted)
        Me.GroupBox1.Controls.Add(Me.rdbtnAll)
        Me.GroupBox1.Location = New System.Drawing.Point(304, 34)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(140, 32)
        Me.GroupBox1.TabIndex = 114
        Me.GroupBox1.TabStop = False
        '
        'rdbtnPosted
        '
        Me.rdbtnPosted.Location = New System.Drawing.Point(4, 11)
        Me.rdbtnPosted.MyLinkLable1 = Nothing
        Me.rdbtnPosted.MyLinkLable2 = Nothing
        Me.rdbtnPosted.Name = "rdbtnPosted"
        Me.rdbtnPosted.Size = New System.Drawing.Size(54, 18)
        Me.rdbtnPosted.TabIndex = 112
        Me.rdbtnPosted.TabStop = True
        Me.rdbtnPosted.Text = "Posted"
        Me.rdbtnPosted.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rdbtnAll
        '
        Me.rdbtnAll.Location = New System.Drawing.Point(86, 11)
        Me.rdbtnAll.MyLinkLable1 = Nothing
        Me.rdbtnAll.MyLinkLable2 = Nothing
        Me.rdbtnAll.Name = "rdbtnAll"
        Me.rdbtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rdbtnAll.TabIndex = 113
        Me.rdbtnAll.Text = "All"
        '
        'ChkZero
        '
        Me.ChkZero.AutoSize = True
        Me.ChkZero.Location = New System.Drawing.Point(446, 13)
        Me.ChkZero.Name = "ChkZero"
        Me.ChkZero.Size = New System.Drawing.Size(120, 17)
        Me.ChkZero.TabIndex = 51
        Me.ChkZero.Text = "With Zero Balance"
        Me.ChkZero.UseVisualStyleBackColor = True
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cgvtemplate)
        Me.RadGroupBox2.Controls.Add(Me.Panel5)
        Me.RadGroupBox2.HeaderText = "Template"
        Me.RadGroupBox2.Location = New System.Drawing.Point(9, 434)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(551, 169)
        Me.RadGroupBox2.TabIndex = 111
        Me.RadGroupBox2.Text = "Template"
        '
        'cgvtemplate
        '
        Me.cgvtemplate.CheckedValue = Nothing
        Me.cgvtemplate.DataSource = Nothing
        Me.cgvtemplate.DisplayMember = "Name"
        Me.cgvtemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cgvtemplate.Location = New System.Drawing.Point(10, 40)
        Me.cgvtemplate.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cgvtemplate.MyShowHeadrText = False
        Me.cgvtemplate.Name = "cgvtemplate"
        Me.cgvtemplate.Size = New System.Drawing.Size(531, 119)
        Me.cgvtemplate.TabIndex = 2
        Me.cgvtemplate.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chktempselect)
        Me.Panel5.Controls.Add(Me.chktempall)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(531, 20)
        Me.Panel5.TabIndex = 1
        '
        'chktempselect
        '
        Me.chktempselect.Location = New System.Drawing.Point(240, 1)
        Me.chktempselect.MyLinkLable1 = Nothing
        Me.chktempselect.MyLinkLable2 = Nothing
        Me.chktempselect.Name = "chktempselect"
        Me.chktempselect.Size = New System.Drawing.Size(50, 18)
        Me.chktempselect.TabIndex = 2
        Me.chktempselect.Text = "Select"
        '
        'chktempall
        '
        Me.chktempall.Location = New System.Drawing.Point(188, 1)
        Me.chktempall.MyLinkLable1 = Nothing
        Me.chktempall.MyLinkLable2 = Nothing
        Me.chktempall.Name = "chktempall"
        Me.chktempall.Size = New System.Drawing.Size(33, 18)
        Me.chktempall.TabIndex = 1
        Me.chktempall.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.HeaderText = "Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(9, 248)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(543, 181)
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
        Me.Panel1.Size = New System.Drawing.Size(523, 20)
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
        Me.cbgLocation.Size = New System.Drawing.Size(523, 135)
        Me.cbgLocation.TabIndex = 0
        Me.cbgLocation.ValueMember = "Code"
        '
        'Customer
        '
        Me.Customer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.Customer.Controls.Add(Me.cbgCustomer)
        Me.Customer.Controls.Add(Me.Panel3)
        Me.Customer.HeaderText = "Customer"
        Me.Customer.Location = New System.Drawing.Point(9, 61)
        Me.Customer.Name = "Customer"
        Me.Customer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.Customer.Size = New System.Drawing.Size(543, 181)
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
        Me.cbgCustomer.Size = New System.Drawing.Size(523, 131)
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
        Me.Panel3.Size = New System.Drawing.Size(523, 20)
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
        Me.dtpend.Location = New System.Drawing.Point(221, 6)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(81, 20)
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
        Me.dtpstart.Size = New System.Drawing.Size(80, 20)
        Me.dtpstart.TabIndex = 0
        Me.dtpstart.TabStop = False
        Me.dtpstart.Text = "Wednesday, November 16, 2011"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(164, 8)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel8.TabIndex = 12
        Me.RadLabel8.Text = "End Date"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(533, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(22, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 5
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(96, 6)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(617, 669)
        Me.SplitContainer1.SplitterDistance = 631
        Me.SplitContainer1.TabIndex = 7
        '
        'FrmOutletEmptyReport1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(617, 669)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmOutletEmptyReport1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Outlet Empty Report"
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.rdobtndetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rdbtnPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chktempselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chktempall, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ChkZero As System.Windows.Forms.CheckBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLOcSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLOcAll As common.Controls.MyRadioButton
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Customer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkSelectAll As common.Controls.MyRadioButton
    Friend WithEvents lblToRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblfrmRouteDesc As common.Controls.MyLabel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cgvtemplate As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chktempselect As common.Controls.MyRadioButton
    Friend WithEvents chktempall As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents rdobtndetails As common.Controls.MyRadioButton
    Friend WithEvents rdobtnSummary As common.Controls.MyRadioButton
    Friend WithEvents rdbtnAll As common.Controls.MyRadioButton
    Friend WithEvents rdbtnPosted As common.Controls.MyRadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class

