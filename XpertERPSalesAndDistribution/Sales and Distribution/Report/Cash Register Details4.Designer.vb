<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cash_Register_Details4
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbtnPosted = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbtnUnposted = New Telerik.WinControls.UI.RadRadioButton()
        Me.dtpreceiptTodate = New common.Controls.MyDateTimePicker()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkLocSelect = New common.Controls.MyRadioButton()
        Me.chkLocAll = New common.Controls.MyRadioButton()
        Me.chkSummary = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgsalesman = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chksalesmanselect = New common.Controls.MyRadioButton()
        Me.chksalesmanAll = New common.Controls.MyRadioButton()
        Me.dtpreceiptfromdate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.chkBankWise = New Telerik.WinControls.UI.RadCheckBox()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rdbtnPosted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnUnposted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpreceiptTodate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.chksalesmanselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chksalesmanAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpreceiptfromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBankWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.chkBankWise)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnPosted)
        Me.RadGroupBox1.Controls.Add(Me.rdbtnUnposted)
        Me.RadGroupBox1.Controls.Add(Me.dtpreceiptTodate)
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.chkSummary)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox6)
        Me.RadGroupBox1.Controls.Add(Me.dtpreceiptfromdate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(435, 405)
        Me.RadGroupBox1.TabIndex = 0
        '
        'rdbtnPosted
        '
        Me.rdbtnPosted.Location = New System.Drawing.Point(13, 41)
        Me.rdbtnPosted.Name = "rdbtnPosted"
        Me.rdbtnPosted.Size = New System.Drawing.Size(54, 18)
        Me.rdbtnPosted.TabIndex = 38
        Me.rdbtnPosted.Text = "Posted"
        '
        'rdbtnUnposted
        '
        Me.rdbtnUnposted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbtnUnposted.Location = New System.Drawing.Point(84, 41)
        Me.rdbtnUnposted.Name = "rdbtnUnposted"
        Me.rdbtnUnposted.Size = New System.Drawing.Size(69, 18)
        Me.rdbtnUnposted.TabIndex = 39
        Me.rdbtnUnposted.Text = "Unposted"
        Me.rdbtnUnposted.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'dtpreceiptTodate
        '
        Me.dtpreceiptTodate.CalculationExpression = Nothing
        Me.dtpreceiptTodate.CustomFormat = "dd-MM-yyyy"
        Me.dtpreceiptTodate.FieldCode = Nothing
        Me.dtpreceiptTodate.FieldDesc = Nothing
        Me.dtpreceiptTodate.FieldMaxLength = 0
        Me.dtpreceiptTodate.FieldName = Nothing
        Me.dtpreceiptTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpreceiptTodate.isCalculatedField = False
        Me.dtpreceiptTodate.IsSourceFromTable = False
        Me.dtpreceiptTodate.IsSourceFromValueList = False
        Me.dtpreceiptTodate.IsUnique = False
        Me.dtpreceiptTodate.Location = New System.Drawing.Point(219, 13)
        Me.dtpreceiptTodate.MendatroryField = False
        Me.dtpreceiptTodate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpreceiptTodate.MyLinkLable1 = Nothing
        Me.dtpreceiptTodate.MyLinkLable2 = Nothing
        Me.dtpreceiptTodate.Name = "dtpreceiptTodate"
        Me.dtpreceiptTodate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpreceiptTodate.ReferenceFieldDesc = Nothing
        Me.dtpreceiptTodate.ReferenceFieldName = Nothing
        Me.dtpreceiptTodate.ReferenceTableName = Nothing
        Me.dtpreceiptTodate.Size = New System.Drawing.Size(82, 20)
        Me.dtpreceiptTodate.TabIndex = 37
        Me.dtpreceiptTodate.TabStop = False
        Me.dtpreceiptTodate.Text = "17-12-2011"
        Me.dtpreceiptTodate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'gv1
        '
        Me.gv1.Location = New System.Drawing.Point(249, 17)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(10, 10)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox4.Controls.Add(Me.Panel3)
        Me.RadGroupBox4.HeaderText = "Location"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 218)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(405, 177)
        Me.RadGroupBox4.TabIndex = 36
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
        Me.cbgLocation.Size = New System.Drawing.Size(385, 127)
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
        Me.Panel3.Size = New System.Drawing.Size(385, 20)
        Me.Panel3.TabIndex = 0
        '
        'chkLocSelect
        '
        Me.chkLocSelect.AccessibleName = "chk_doc_select"
        Me.chkLocSelect.Location = New System.Drawing.Point(197, 1)
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
        Me.chkLocAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLocAll.Location = New System.Drawing.Point(139, 1)
        Me.chkLocAll.MyLinkLable1 = Nothing
        Me.chkLocAll.MyLinkLable2 = Nothing
        Me.chkLocAll.Name = "chkLocAll"
        Me.chkLocAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocAll.TabIndex = 0
        Me.chkLocAll.Text = "All"
        Me.chkLocAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkSummary
        '
        Me.chkSummary.Location = New System.Drawing.Point(324, 13)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(67, 18)
        Me.chkSummary.TabIndex = 15
        Me.chkSummary.Text = "Summary"
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.cbgsalesman)
        Me.RadGroupBox6.Controls.Add(Me.Panel4)
        Me.RadGroupBox6.HeaderText = "Select Salesman"
        Me.RadGroupBox6.Location = New System.Drawing.Point(17, 74)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox6.Size = New System.Drawing.Size(405, 147)
        Me.RadGroupBox6.TabIndex = 12
        Me.RadGroupBox6.Text = "Select Salesman"
        '
        'cbgsalesman
        '
        Me.cbgsalesman.AccessibleName = ""
        Me.cbgsalesman.CheckedValue = Nothing
        Me.cbgsalesman.DataSource = Nothing
        Me.cbgsalesman.DisplayMember = "Name"
        Me.cbgsalesman.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgsalesman.Location = New System.Drawing.Point(10, 40)
        Me.cbgsalesman.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgsalesman.MyShowHeadrText = False
        Me.cbgsalesman.Name = "cbgsalesman"
        Me.cbgsalesman.Size = New System.Drawing.Size(385, 97)
        Me.cbgsalesman.TabIndex = 1
        Me.cbgsalesman.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.chksalesmanselect)
        Me.Panel4.Controls.Add(Me.chksalesmanAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(385, 20)
        Me.Panel4.TabIndex = 0
        '
        'chksalesmanselect
        '
        Me.chksalesmanselect.Location = New System.Drawing.Point(192, 1)
        Me.chksalesmanselect.MyLinkLable1 = Nothing
        Me.chksalesmanselect.MyLinkLable2 = Nothing
        Me.chksalesmanselect.Name = "chksalesmanselect"
        Me.chksalesmanselect.Size = New System.Drawing.Size(50, 18)
        Me.chksalesmanselect.TabIndex = 1
        Me.chksalesmanselect.Text = "Select"
        '
        'chksalesmanAll
        '
        Me.chksalesmanAll.Location = New System.Drawing.Point(141, 1)
        Me.chksalesmanAll.MyLinkLable1 = Nothing
        Me.chksalesmanAll.MyLinkLable2 = Nothing
        Me.chksalesmanAll.Name = "chksalesmanAll"
        Me.chksalesmanAll.Size = New System.Drawing.Size(33, 18)
        Me.chksalesmanAll.TabIndex = 0
        Me.chksalesmanAll.Text = "All"
        '
        'dtpreceiptfromdate
        '
        Me.dtpreceiptfromdate.CalculationExpression = Nothing
        Me.dtpreceiptfromdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpreceiptfromdate.FieldCode = Nothing
        Me.dtpreceiptfromdate.FieldDesc = Nothing
        Me.dtpreceiptfromdate.FieldMaxLength = 0
        Me.dtpreceiptfromdate.FieldName = Nothing
        Me.dtpreceiptfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpreceiptfromdate.isCalculatedField = False
        Me.dtpreceiptfromdate.IsSourceFromTable = False
        Me.dtpreceiptfromdate.IsSourceFromValueList = False
        Me.dtpreceiptfromdate.IsUnique = False
        Me.dtpreceiptfromdate.Location = New System.Drawing.Point(72, 11)
        Me.dtpreceiptfromdate.MendatroryField = False
        Me.dtpreceiptfromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpreceiptfromdate.MyLinkLable1 = Nothing
        Me.dtpreceiptfromdate.MyLinkLable2 = Nothing
        Me.dtpreceiptfromdate.Name = "dtpreceiptfromdate"
        Me.dtpreceiptfromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpreceiptfromdate.ReferenceFieldDesc = Nothing
        Me.dtpreceiptfromdate.ReferenceFieldName = Nothing
        Me.dtpreceiptfromdate.ReferenceTableName = Nothing
        Me.dtpreceiptfromdate.Size = New System.Drawing.Size(82, 20)
        Me.dtpreceiptfromdate.TabIndex = 10
        Me.dtpreceiptfromdate.TabStop = False
        Me.dtpreceiptfromdate.Text = "17-12-2011"
        Me.dtpreceiptfromdate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(169, 13)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel3.TabIndex = 14
        Me.RadLabel3.Text = "To Date"
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Location = New System.Drawing.Point(9, 12)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel4.TabIndex = 13
        Me.RadLabel4.Text = "From Date"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(541, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 10
        Me.btnclose.Text = "Close"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(35, 6)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(68, 18)
        Me.btnreset.TabIndex = 9
        Me.btnreset.Text = "Reset"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(114, 6)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(68, 18)
        Me.btnprint.TabIndex = 8
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(622, 471)
        Me.SplitContainer1.SplitterDistance = 430
        Me.SplitContainer1.TabIndex = 11
        '
        'btnExp
        '
        Me.btnExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExp.Location = New System.Drawing.Point(193, 6)
        Me.btnExp.Name = "btnExp"
        Me.btnExp.Size = New System.Drawing.Size(78, 18)
        Me.btnExp.TabIndex = 313
        Me.btnExp.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.AccessibleDescription = "Excel"
        Me.rmiExcel.AccessibleName = "Excel"
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.AccessibleDescription = "PDF"
        Me.rmiPDF.AccessibleName = "PDF"
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'chkBankWise
        '
        Me.chkBankWise.Location = New System.Drawing.Point(169, 41)
        Me.chkBankWise.Name = "chkBankWise"
        Me.chkBankWise.Size = New System.Drawing.Size(72, 18)
        Me.chkBankWise.TabIndex = 40
        Me.chkBankWise.Text = "Bank Wise"
        '
        'Cash_Register_Details4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 471)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Cash_Register_Details4"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Cash Register Details"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rdbtnPosted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnUnposted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpreceiptTodate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.chkLocSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.chksalesmanselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chksalesmanAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpreceiptfromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBankWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgsalesman As common.MyCheckBoxGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chksalesmanselect As common.Controls.MyRadioButton
    Friend WithEvents chksalesmanAll As common.Controls.MyRadioButton
    Friend WithEvents dtpreceiptfromdate As common.Controls.MyDateTimePicker
    Friend WithEvents chkSummary As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkLocSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocAll As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents dtpreceiptTodate As common.Controls.MyDateTimePicker
    Friend WithEvents rdbtnPosted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbtnUnposted As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btnExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents chkBankWise As Telerik.WinControls.UI.RadCheckBox
End Class

