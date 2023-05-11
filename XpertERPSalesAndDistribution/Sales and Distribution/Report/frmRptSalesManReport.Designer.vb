<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptSalesManReport
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
        Me.lblEmpName = New common.Controls.MyLabel
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.fndsalesman = New common.UserControls.txtFinder
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.chkLocSelect = New common.Controls.MyRadioButton
        Me.chkLocAll = New common.Controls.MyRadioButton
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgCompany = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkCompanySelect = New common.Controls.MyRadioButton
        Me.chkCompanyAll = New common.Controls.MyRadioButton
        Me.txtToDate = New common.Controls.MyDateTimePicker
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.txtFromDate = New common.Controls.MyDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnPrint = New Telerik.WinControls.UI.RadButton
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkCompanySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCompanyAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox1.Controls.Add(Me.lblEmpName)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.fndsalesman)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox1.Controls.Add(Me.btnClose)
        Me.RadGroupBox1.Controls.Add(Me.btnReset)
        Me.RadGroupBox1.Controls.Add(Me.btnPrint)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 4)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(565, 443)
        Me.RadGroupBox1.TabIndex = 0
        '
        'lblEmpName
        '
        Me.lblEmpName.Location = New System.Drawing.Point(227, 30)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(9, 18)
        Me.lblEmpName.TabIndex = 44
        Me.lblEmpName.Text = " "
        '
        'RadLabel3
        '
        Me.RadLabel3.Location = New System.Drawing.Point(6, 30)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(55, 18)
        Me.RadLabel3.TabIndex = 9
        Me.RadLabel3.Text = "Employee"
        '
        'fndsalesman
        '
        Me.fndsalesman.Location = New System.Drawing.Point(70, 30)
        Me.fndsalesman.MendatroryField = False
        Me.fndsalesman.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndsalesman.MyLinkLable1 = Me.lblEmpName
        Me.fndsalesman.MyLinkLable2 = Me.RadLabel3
        Me.fndsalesman.MyReadOnly = False
        Me.fndsalesman.Name = "fndsalesman"
        Me.fndsalesman.Size = New System.Drawing.Size(153, 19)
        Me.fndsalesman.TabIndex = 2
        Me.fndsalesman.Value = ""
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox3.Controls.Add(Me.Panel2)
        Me.RadGroupBox3.HeaderText = "Location"
        Me.RadGroupBox3.Location = New System.Drawing.Point(5, 228)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(551, 178)
        Me.RadGroupBox3.TabIndex = 4
        Me.RadGroupBox3.Text = "Location"
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
        Me.cbgLocation.Size = New System.Drawing.Size(531, 128)
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
        Me.Panel2.Size = New System.Drawing.Size(531, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.Location = New System.Drawing.Point(256, 1)
        Me.chkLocSelect.MyLinkLable1 = Nothing
        Me.chkLocSelect.MyLinkLable2 = Nothing
        Me.chkLocSelect.Name = "chkLocSelect"
        Me.chkLocSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocSelect.TabIndex = 1
        Me.chkLocSelect.Text = "Select"
        '
        'chkLocAll
        '
        Me.chkLocAll.Location = New System.Drawing.Point(205, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgCompany)
        Me.RadGroupBox2.Controls.Add(Me.Panel1)
        Me.RadGroupBox2.HeaderText = "Company"
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 53)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(551, 172)
        Me.RadGroupBox2.TabIndex = 3
        Me.RadGroupBox2.Text = "Company"
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
        Me.cbgCompany.Size = New System.Drawing.Size(531, 122)
        Me.cbgCompany.TabIndex = 1
        Me.cbgCompany.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkCompanySelect)
        Me.Panel1.Controls.Add(Me.chkCompanyAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(531, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkCompanySelect
        '
        Me.chkCompanySelect.Location = New System.Drawing.Point(256, 1)
        Me.chkCompanySelect.MyLinkLable1 = Nothing
        Me.chkCompanySelect.MyLinkLable2 = Nothing
        Me.chkCompanySelect.Name = "chkCompanySelect"
        Me.chkCompanySelect.Size = New System.Drawing.Size(50, 18)
        Me.chkCompanySelect.TabIndex = 1
        Me.chkCompanySelect.Text = "Select"
        '
        'chkCompanyAll
        '
        Me.chkCompanyAll.Location = New System.Drawing.Point(205, 1)
        Me.chkCompanyAll.MyLinkLable1 = Nothing
        Me.chkCompanyAll.MyLinkLable2 = Nothing
        Me.chkCompanyAll.Name = "chkCompanyAll"
        Me.chkCompanyAll.Size = New System.Drawing.Size(33, 18)
        Me.chkCompanyAll.TabIndex = 0
        Me.chkCompanyAll.Text = "All"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(477, 7)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.RadLabel2
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(420, 8)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 8
        Me.RadLabel2.Text = "To Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(70, 7)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.RadLabel1
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(6, 8)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 10
        Me.RadLabel1.Text = "From Date"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(490, 417)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(15, 417)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 6
        Me.btnReset.Text = "Reset"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(89, 417)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'frmRptSalesManReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(572, 455)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Name = "frmRptSalesManReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salesman Sales Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkCompanySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCompanyAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgCompany As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkCompanySelect As common.Controls.MyRadioButton
    Friend WithEvents chkCompanyAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents fndsalesman As common.UserControls.txtFinder
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
End Class

