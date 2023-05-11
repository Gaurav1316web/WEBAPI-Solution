<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptCashDiscountReport
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
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel
        Me.RadGroupBox8 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbglocation = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chklocSelect = New common.Controls.MyRadioButton
        Me.chklocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCompany = New common.MyCheckBoxGrid
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.rbtnCompanySelect = New common.Controls.MyRadioButton
        Me.rbtnCompanyAll = New common.Controls.MyRadioButton
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCustomer = New common.MyCheckBoxGrid
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.chkChkSelect = New common.Controls.MyRadioButton
        Me.chkCustAll = New common.Controls.MyRadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbtnAll = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnPosted = New Telerik.WinControls.UI.RadRadioButton
        Me.txtTodate = New common.Controls.MyDateTimePicker
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox8.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chklocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chklocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chkChkSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        Me.RadSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(681, 391)
        Me.RadSplitContainer1.SplitterWidth = 4
        Me.RadSplitContainer1.TabIndex = 0
        Me.RadSplitContainer1.TabStop = False
        Me.RadSplitContainer1.Text = "RadSplitContainer1"
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.RadGroupBox8)
        Me.SplitPanel1.Controls.Add(Me.RadGroupBox3)
        Me.SplitPanel1.Controls.Add(Me.RadGroupBox6)
        Me.SplitPanel1.Controls.Add(Me.GroupBox1)
        Me.SplitPanel1.Controls.Add(Me.txtTodate)
        Me.SplitPanel1.Controls.Add(Me.RadLabel7)
        Me.SplitPanel1.Controls.Add(Me.txtFromDate)
        Me.SplitPanel1.Controls.Add(Me.RadLabel8)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.SplitPanel1.Size = New System.Drawing.Size(681, 362)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.0!, 0.435567!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, 169)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.Text = "SplitPanel1"
        '
        'RadGroupBox8
        '
        Me.RadGroupBox8.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox8.Controls.Add(Me.cbglocation)
        Me.RadGroupBox8.Controls.Add(Me.Panel6)
        Me.RadGroupBox8.HeaderText = "Location"
        Me.RadGroupBox8.Location = New System.Drawing.Point(12, 208)
        Me.RadGroupBox8.Name = "RadGroupBox8"
        Me.RadGroupBox8.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox8.Size = New System.Drawing.Size(321, 139)
        Me.RadGroupBox8.TabIndex = 112
        Me.RadGroupBox8.Text = "Location"
        '
        'cbglocation
        '
        Me.cbglocation.CheckedValue = Nothing
        Me.cbglocation.DataSource = Nothing
        Me.cbglocation.DisplayMember = "Name"
        Me.cbglocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbglocation.Location = New System.Drawing.Point(10, 40)
        Me.cbglocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbglocation.MyShowHeadrText = False
        Me.cbglocation.Name = "cbglocation"
        Me.cbglocation.Size = New System.Drawing.Size(301, 89)
        Me.cbglocation.TabIndex = 1
        Me.cbglocation.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chklocSelect)
        Me.Panel6.Controls.Add(Me.chklocAll)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(301, 20)
        Me.Panel6.TabIndex = 0
        '
        'chklocSelect
        '
        Me.chklocSelect.Location = New System.Drawing.Point(192, 1)
        Me.chklocSelect.MyLinkLable1 = Nothing
        Me.chklocSelect.MyLinkLable2 = Nothing
        Me.chklocSelect.Name = "chklocSelect"
        Me.chklocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chklocSelect.TabIndex = 1
        Me.chklocSelect.Text = "Select"
        '
        'chklocAll
        '
        Me.chklocAll.Location = New System.Drawing.Point(141, 1)
        Me.chklocAll.MyLinkLable1 = Nothing
        Me.chklocAll.MyLinkLable2 = Nothing
        Me.chklocAll.Name = "chklocAll"
        Me.chklocAll.Size = New System.Drawing.Size(33, 18)
        Me.chklocAll.TabIndex = 0
        Me.chklocAll.TabStop = True
        Me.chklocAll.Text = "All"
        Me.chklocAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgCompany)
        Me.RadGroupBox3.Controls.Add(Me.Panel7)
        Me.RadGroupBox3.HeaderText = "Company"
        Me.RadGroupBox3.Location = New System.Drawing.Point(344, 61)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(321, 141)
        Me.RadGroupBox3.TabIndex = 111
        Me.RadGroupBox3.Text = "Company"
        '
        'cbgCompany
        '
        Me.cbgCompany.CheckedValue = Nothing
        Me.cbgCompany.DataSource = Nothing
        Me.cbgCompany.DisplayMember = "Name"
        Me.cbgCompany.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgCompany.Location = New System.Drawing.Point(10, 40)
        Me.cbgCompany.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgCompany.MyShowHeadrText = False
        Me.cbgCompany.Name = "cbgCompany"
        Me.cbgCompany.Size = New System.Drawing.Size(301, 91)
        Me.cbgCompany.TabIndex = 1
        Me.cbgCompany.ValueMember = "Code"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbtnCompanySelect)
        Me.Panel7.Controls.Add(Me.rbtnCompanyAll)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(10, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(301, 20)
        Me.Panel7.TabIndex = 0
        '
        'rbtnCompanySelect
        '
        Me.rbtnCompanySelect.Location = New System.Drawing.Point(161, 1)
        Me.rbtnCompanySelect.MyLinkLable1 = Nothing
        Me.rbtnCompanySelect.MyLinkLable2 = Nothing
        Me.rbtnCompanySelect.Name = "rbtnCompanySelect"
        Me.rbtnCompanySelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnCompanySelect.TabIndex = 1
        Me.rbtnCompanySelect.Text = "Select"
        '
        'rbtnCompanyAll
        '
        Me.rbtnCompanyAll.Location = New System.Drawing.Point(120, 1)
        Me.rbtnCompanyAll.MyLinkLable1 = Nothing
        Me.rbtnCompanyAll.MyLinkLable2 = Nothing
        Me.rbtnCompanyAll.Name = "rbtnCompanyAll"
        Me.rbtnCompanyAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnCompanyAll.TabIndex = 0
        Me.rbtnCompanyAll.Text = "All"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgCustomer)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Customer"
        Me.RadGroupBox6.Location = New System.Drawing.Point(12, 61)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(321, 141)
        Me.RadGroupBox6.TabIndex = 110
        Me.RadGroupBox6.Text = "Customer"
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
        Me.cbgCustomer.Size = New System.Drawing.Size(301, 91)
        Me.cbgCustomer.TabIndex = 1
        Me.cbgCustomer.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chkChkSelect)
        Me.Panel4.Controls.Add(Me.chkCustAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(301, 20)
        Me.Panel4.TabIndex = 0
        '
        'chkChkSelect
        '
        Me.chkChkSelect.Location = New System.Drawing.Point(161, 1)
        Me.chkChkSelect.MyLinkLable1 = Nothing
        Me.chkChkSelect.MyLinkLable2 = Nothing
        Me.chkChkSelect.Name = "chkChkSelect"
        Me.chkChkSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkChkSelect.TabIndex = 1
        Me.chkChkSelect.Text = "Select"
        '
        'chkCustAll
        '
        Me.chkCustAll.Location = New System.Drawing.Point(120, 1)
        Me.chkCustAll.MyLinkLable1 = Nothing
        Me.chkCustAll.MyLinkLable2 = Nothing
        Me.chkCustAll.Name = "chkCustAll"
        Me.chkCustAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCustAll.TabIndex = 0
        Me.chkCustAll.Text = "All"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnAll)
        Me.GroupBox1.Controls.Add(Me.rbtnPosted)
        Me.GroupBox1.Location = New System.Drawing.Point(346, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(149, 36)
        Me.GroupBox1.TabIndex = 109
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Status"
        '
        'rbtnAll
        '
        Me.rbtnAll.Location = New System.Drawing.Point(92, 13)
        Me.rbtnAll.Name = "rbtnAll"
        Me.rbtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAll.TabIndex = 103
        Me.rbtnAll.Text = "All"
        '
        'rbtnPosted
        '
        Me.rbtnPosted.Location = New System.Drawing.Point(29, 13)
        Me.rbtnPosted.Name = "rbtnPosted"
        Me.rbtnPosted.Size = New System.Drawing.Size(54, 18)
        Me.rbtnPosted.TabIndex = 0
        Me.rbtnPosted.TabStop = True
        Me.rbtnPosted.Text = "Posted"
        Me.rbtnPosted.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtTodate
        '
        Me.txtTodate.CustomFormat = "dd/MM/yyyy"
        Me.txtTodate.Location = New System.Drawing.Point(235, 11)
        Me.txtTodate.MendatroryField = False
        Me.txtTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.MyLinkLable1 = Me.RadLabel8
        Me.txtTodate.MyLinkLable2 = Nothing
        Me.txtTodate.Name = "txtTodate"
        Me.txtTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtTodate.Size = New System.Drawing.Size(83, 20)
        Me.txtTodate.TabIndex = 18
        Me.txtTodate.TabStop = False
        Me.txtTodate.Text = "Wednesday, November 16, 2011"
        Me.txtTodate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(181, 12)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel8.TabIndex = 16
        Me.RadLabel8.Text = "To Date"
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(12, 12)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel7.TabIndex = 15
        Me.RadLabel7.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Location = New System.Drawing.Point(80, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.RadLabel7
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(83, 20)
        Me.txtFromDate.TabIndex = 17
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "Wednesday, November 16, 2011"
        Me.txtFromDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.btnPrint)
        Me.SplitPanel2.Controls.Add(Me.btnclose)
        Me.SplitPanel2.Controls.Add(Me.btnreset)
        Me.SplitPanel2.Location = New System.Drawing.Point(0, 366)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.SplitPanel2.Size = New System.Drawing.Size(681, 25)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.0!, -0.435567!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(0, -169)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
        Me.SplitPanel2.Text = "SplitPanel2"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(86, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 120
        Me.btnPrint.Text = "Print"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(610, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 121
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(12, 4)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 118
        Me.btnreset.Text = "Reset"
        '
        'rptCashDiscountReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(681, 391)
        Me.Controls.Add(Me.RadSplitContainer1)
        Me.Name = "rptCashDiscountReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cash Discount"
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        Me.SplitPanel1.PerformLayout()
        CType(Me.RadGroupBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox8.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chklocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chklocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.rbtnCompanySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCompanyAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chkChkSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCustAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents txtTodate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnAll As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnPosted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCompany As common.MyCheckBoxGrid
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents rbtnCompanySelect As common.Controls.MyRadioButton
    Friend WithEvents rbtnCompanyAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCustomer As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkChkSelect As common.Controls.MyRadioButton
    Friend WithEvents chkCustAll As common.Controls.MyRadioButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox8 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbglocation As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chklocSelect As common.Controls.MyRadioButton
    Friend WithEvents chklocAll As common.Controls.MyRadioButton
End Class

