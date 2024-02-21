<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RptComplainfeedback
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox14 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox10 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel8 = New common.Controls.MyLabel()
        Me.MyDateTimePicker1 = New common.Controls.MyDateTimePicker()
        Me.dtpfromdate1 = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.RadLabel7 = New common.Controls.MyLabel()
        Me.cmbReportType = New common.Controls.MyComboBox()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.fromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.ToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox14.SuspendLayout()
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox10.SuspendLayout()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpfromdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(831, 364)
        Me.SplitContainer1.SplitterDistance = 334
        Me.SplitContainer1.TabIndex = 4
        '
        'RadPageView1
        '
        Me.RadPageView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(831, 334)
        Me.RadPageView1.TabIndex = 12
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox14)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(810, 286)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'RadGroupBox14
        '
        Me.RadGroupBox14.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox14.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox14.Controls.Add(Me.RadGroupBox10)
        Me.RadGroupBox14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox14.HeaderText = ""
        Me.RadGroupBox14.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox14.Name = "RadGroupBox14"
        Me.RadGroupBox14.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox14.Size = New System.Drawing.Size(810, 286)
        Me.RadGroupBox14.TabIndex = 1
        '
        'RadGroupBox10
        '
        Me.RadGroupBox10.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox10.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RadGroupBox10.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox10.Controls.Add(Me.MyDateTimePicker1)
        Me.RadGroupBox10.Controls.Add(Me.dtpfromdate1)
        Me.RadGroupBox10.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox10.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox10.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox10.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox10.Controls.Add(Me.cmbReportType)
        Me.RadGroupBox10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox10.HeaderText = ""
        Me.RadGroupBox10.Location = New System.Drawing.Point(10, 20)
        Me.RadGroupBox10.Name = "RadGroupBox10"
        Me.RadGroupBox10.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox10.Size = New System.Drawing.Size(790, 256)
        Me.RadGroupBox10.TabIndex = 10
        '
        'RadLabel8
        '
        Me.RadLabel8.FieldName = Nothing
        Me.RadLabel8.Location = New System.Drawing.Point(28, 23)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel8.TabIndex = 1471
        Me.RadLabel8.Text = "From Date"
        '
        'MyDateTimePicker1
        '
        Me.MyDateTimePicker1.AccessibleName = " dtpTodate"
        Me.MyDateTimePicker1.CalculationExpression = Nothing
        Me.MyDateTimePicker1.CustomFormat = "dd-MM-yyyy"
        Me.MyDateTimePicker1.FieldCode = Nothing
        Me.MyDateTimePicker1.FieldDesc = Nothing
        Me.MyDateTimePicker1.FieldMaxLength = 0
        Me.MyDateTimePicker1.FieldName = Nothing
        Me.MyDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker1.isCalculatedField = False
        Me.MyDateTimePicker1.IsSourceFromTable = False
        Me.MyDateTimePicker1.IsSourceFromValueList = False
        Me.MyDateTimePicker1.IsUnique = False
        Me.MyDateTimePicker1.Location = New System.Drawing.Point(383, 23)
        Me.MyDateTimePicker1.MendatroryField = False
        Me.MyDateTimePicker1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.MyLinkLable1 = Nothing
        Me.MyDateTimePicker1.MyLinkLable2 = Nothing
        Me.MyDateTimePicker1.Name = "MyDateTimePicker1"
        Me.MyDateTimePicker1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.ReferenceFieldDesc = Nothing
        Me.MyDateTimePicker1.ReferenceFieldName = Nothing
        Me.MyDateTimePicker1.ReferenceTableName = Nothing
        Me.MyDateTimePicker1.Size = New System.Drawing.Size(82, 20)
        Me.MyDateTimePicker1.TabIndex = 1470
        Me.MyDateTimePicker1.TabStop = False
        Me.MyDateTimePicker1.Text = "14-09-2011"
        Me.MyDateTimePicker1.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'dtpfromdate1
        '
        Me.dtpfromdate1.AccessibleName = "dtpfromdate1"
        Me.dtpfromdate1.CalculationExpression = Nothing
        Me.dtpfromdate1.CustomFormat = "dd-MM-yyyy"
        Me.dtpfromdate1.FieldCode = Nothing
        Me.dtpfromdate1.FieldDesc = Nothing
        Me.dtpfromdate1.FieldMaxLength = 0
        Me.dtpfromdate1.FieldName = Nothing
        Me.dtpfromdate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromdate1.isCalculatedField = False
        Me.dtpfromdate1.IsSourceFromTable = False
        Me.dtpfromdate1.IsSourceFromValueList = False
        Me.dtpfromdate1.IsUnique = False
        Me.dtpfromdate1.Location = New System.Drawing.Point(127, 21)
        Me.dtpfromdate1.MendatroryField = False
        Me.dtpfromdate1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate1.MyLinkLable1 = Nothing
        Me.dtpfromdate1.MyLinkLable2 = Nothing
        Me.dtpfromdate1.Name = "dtpfromdate1"
        Me.dtpfromdate1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpfromdate1.ReferenceFieldDesc = Nothing
        Me.dtpfromdate1.ReferenceFieldName = Nothing
        Me.dtpfromdate1.ReferenceTableName = Nothing
        Me.dtpfromdate1.Size = New System.Drawing.Size(82, 20)
        Me.dtpfromdate1.TabIndex = 1469
        Me.dtpfromdate1.TabStop = False
        Me.dtpfromdate1.Text = "14-09-2011"
        Me.dtpfromdate1.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(28, 79)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel3.TabIndex = 400
        Me.MyLabel3.Text = "Customer"
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(28, 53)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel21.TabIndex = 1467
        Me.MyLabel21.Text = " Type"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(127, 78)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.MyLabel3
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "All"
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(338, 19)
        Me.txtCustomer.TabIndex = 402
        '
        'RadLabel7
        '
        Me.RadLabel7.FieldName = Nothing
        Me.RadLabel7.Location = New System.Drawing.Point(272, 24)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel7.TabIndex = 1472
        Me.RadLabel7.Text = "To Date"
        '
        'cmbReportType
        '
        Me.cmbReportType.AutoCompleteDisplayMember = Nothing
        Me.cmbReportType.AutoCompleteValueMember = Nothing
        Me.cmbReportType.CalculationExpression = Nothing
        Me.cmbReportType.DropDownAnimationEnabled = True
        Me.cmbReportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbReportType.FieldCode = Nothing
        Me.cmbReportType.FieldDesc = Nothing
        Me.cmbReportType.FieldMaxLength = 0
        Me.cmbReportType.FieldName = Nothing
        Me.cmbReportType.isCalculatedField = False
        Me.cmbReportType.IsSourceFromTable = False
        Me.cmbReportType.IsSourceFromValueList = False
        Me.cmbReportType.IsUnique = False
        RadListDataItem4.Text = "All"
        RadListDataItem5.Text = "Complain"
        RadListDataItem6.Text = "Feedback"
        Me.cmbReportType.Items.Add(RadListDataItem4)
        Me.cmbReportType.Items.Add(RadListDataItem5)
        Me.cmbReportType.Items.Add(RadListDataItem6)
        Me.cmbReportType.Location = New System.Drawing.Point(127, 49)
        Me.cmbReportType.MendatroryField = False
        Me.cmbReportType.MyLinkLable1 = Nothing
        Me.cmbReportType.MyLinkLable2 = Nothing
        Me.cmbReportType.Name = "cmbReportType"
        Me.cmbReportType.ReferenceFieldDesc = Nothing
        Me.cmbReportType.ReferenceFieldName = Nothing
        Me.cmbReportType.ReferenceTableName = Nothing
        Me.cmbReportType.Size = New System.Drawing.Size(338, 20)
        Me.cmbReportType.TabIndex = 1468
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(810, 286)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.EnableFiltering = True
        Me.Gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.Gv1.Name = "Gv1"
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(810, 286)
        Me.Gv1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(735, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 158
        Me.btnClose.Text = "Close"
        '
        'btnSplitExport
        '
        Me.btnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnSplitExport.Location = New System.Drawing.Point(166, 0)
        Me.btnSplitExport.Name = "btnSplitExport"
        Me.btnSplitExport.Size = New System.Drawing.Size(95, 22)
        Me.btnSplitExport.TabIndex = 157
        Me.btnSplitExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        Me.rmiPDF.UseCompatibleTextRendering = False
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(89, 0)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 153
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(12, 0)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 152
        Me.btnGo.Text = ">>>"
        '
        'fromDate
        '
        Me.fromDate.CustomFormat = "dd/MM/yyyy"
        Me.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDate.Location = New System.Drawing.Point(44, 15)
        Me.fromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Name = "fromDate"
        Me.fromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromDate.Size = New System.Drawing.Size(89, 20)
        Me.fromDate.TabIndex = 0
        Me.fromDate.TabStop = False
        Me.fromDate.Text = "24/10/2011"
        Me.fromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'ToDate
        '
        Me.ToDate.CustomFormat = "dd/MM/yyyy"
        Me.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.ToDate.Location = New System.Drawing.Point(164, 15)
        Me.ToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ToDate.Size = New System.Drawing.Size(83, 20)
        Me.ToDate.TabIndex = 1
        Me.ToDate.TabStop = False
        Me.ToDate.Text = "24/10/2011"
        Me.ToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RptComplainfeedback
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 364)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "RptComplainfeedback"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptComplainfeedback"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox14.ResumeLayout(False)
        CType(Me.RadGroupBox10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox10.ResumeLayout(False)
        Me.RadGroupBox10.PerformLayout()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpfromdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbReportType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents fromDate As RadDateTimePicker
    Friend WithEvents ToDate As RadDateTimePicker
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadGroupBox14 As RadGroupBox
    Friend WithEvents RadGroupBox10 As RadGroupBox
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents MyDateTimePicker1 As common.Controls.MyDateTimePicker
    Friend WithEvents dtpfromdate1 As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents cmbReportType As common.Controls.MyComboBox
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnSplitExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnGo As RadButton
End Class

