<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSecurityDeposit1
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
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.lblFromDate = New common.Controls.MyLabel
        Me.lblToDate = New common.Controls.MyLabel
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.dtpFromdate1 = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkCustomerSelect = New common.Controls.MyRadioButton
        Me.chkCustomerAll = New common.Controls.MyRadioButton
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(15, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(576, 450)
        Me.RadGroupBox1.TabIndex = 0
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Location"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 260)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(557, 182)
        Me.RadGroupBox4.TabIndex = 38
        Me.RadGroupBox4.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleName = "cbgDoc"
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(537, 132)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLocSelect)
        Me.Panel3.Controls.Add(Me.chkLocAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(537, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.AccessibleName = "chk_doc_select"
        Me.chkLocSelect.Location = New System.Drawing.Point(241, 2)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.AccessibleName = "chk_All"
        Me.chkLocAll.Location = New System.Drawing.Point(183, 3)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.TabStop = True
        Me.chkLocAll.Text = "All"
        Me.chkLocAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.lblFromDate)
        Me.RadGroupBox2.Controls.Add(Me.lblToDate)
        Me.RadGroupBox2.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox2.Controls.Add(Me.dtpFromdate1)
        Me.RadGroupBox2.HeaderText = "Date"
        Me.RadGroupBox2.Location = New System.Drawing.Point(9, 8)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(557, 53)
        Me.RadGroupBox2.TabIndex = 27
        Me.RadGroupBox2.Text = "Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.Location = New System.Drawing.Point(13, 23)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromDate.TabIndex = 22
        Me.lblFromDate.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.Location = New System.Drawing.Point(201, 24)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 23
        Me.lblToDate.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.AccessibleName = "dtpToDate"
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(262, 22)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(84, 20)
        Me.dtpToDate.TabIndex = 25
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "01/02/2012"
        Me.dtpToDate.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'dtpFromdate1
        '
        Me.dtpFromdate1.AccessibleName = "dtpFromDate"
        Me.dtpFromdate1.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromdate1.Location = New System.Drawing.Point(82, 21)
        Me.dtpFromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Name = "dtpFromdate1"
        Me.dtpFromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromdate1.Size = New System.Drawing.Size(88, 20)
        Me.dtpFromdate1.TabIndex = 24
        Me.dtpFromdate1.TabStop = False
        Me.dtpFromdate1.Text = "01/02/2012"
        Me.dtpFromdate1.Value = New Date(2012, 2, 1, 0, 0, 0, 0)
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox3.HeaderText = "Customer"
        Me.RadGroupBox3.Location = New System.Drawing.Point(9, 67)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(557, 182)
        Me.RadGroupBox3.TabIndex = 37
        Me.RadGroupBox3.Text = "Customer"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkCustomerSelect)
        Me.Panel2.Controls.Add(Me.chkCustomerAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(537, 20)
        Me.Panel2.TabIndex = 26
        '
        'chkCustomerSelect
        '
        Me.chkCustomerSelect.Location = New System.Drawing.Point(254, 1)
        Me.chkCustomerSelect.MyLinkLable1 = Nothing
        Me.chkCustomerSelect.MyLinkLable2 = Nothing
        Me.chkCustomerSelect.Name = "chkCustomerSelect"
        Me.chkCustomerSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCustomerSelect.TabIndex = 1
        Me.chkCustomerSelect.Text = "Select"
        '
        'chkCustomerAll
        '
        Me.chkCustomerAll.Location = New System.Drawing.Point(187, 1)
        Me.chkCustomerAll.MyLinkLable1 = Nothing
        Me.chkCustomerAll.MyLinkLable2 = Nothing
        Me.chkCustomerAll.Name = "chkCustomerAll"
        Me.chkCustomerAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustomerAll.TabIndex = 0
        Me.chkCustomerAll.Text = "All"
        '
        'cbgCustomer
        '
        Me.cbgCustomer.CheckedValue = Nothing
        Me.cbgCustomer.DataSource = Nothing
        Me.cbgCustomer.DisplayMember = "Name"
        Me.cbgCustomer.Location = New System.Drawing.Point(10, 43)
        Me.cbgCustomer.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCustomer.MyShowHeadrText = False
        Me.cbgCustomer.Name = "cbgCustomer"
        Me.cbgCustomer.Size = New System.Drawing.Size(537, 131)
        Me.cbgCustomer.TabIndex = 0
        Me.cbgCustomer.ValueMember = "Code"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(515, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 24)
        Me.btnClose.TabIndex = 19
        Me.btnClose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(76, 24)
        Me.btnreset.TabIndex = 21
        Me.btnreset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(94, 10)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 24)
        Me.btnPrint.TabIndex = 20
        Me.btnPrint.Text = "Print"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(608, 516)
        Me.SplitContainer1.SplitterDistance = 465
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmSecurityDeposit1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSecurityDeposit1"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Security Deposit"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lblFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkCustomerSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustomerAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromdate1 As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblFromDate As common.Controls.MyLabel
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkCustomerSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustomerAll As common.Controls.MyRadioButton
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

