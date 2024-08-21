<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptEmployeeAdvanceLedger
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btn_savelayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.btn_deletelayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtEmployee = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtDepartment = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.rbtndetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.dtpToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.dtpFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtLocationMult = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btn_excel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btn_pdf = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtndetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1042, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btn_savelayout, Me.btn_deletelayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'btn_savelayout
        '
        Me.btn_savelayout.Name = "btn_savelayout"
        Me.btn_savelayout.Text = "Save Layout"
        '
        'btn_deletelayout
        '
        Me.btn_deletelayout.Name = "btn_deletelayout"
        Me.btn_deletelayout.Text = "Delete Layout"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(1042, 496)
        Me.SplitContainer1.SplitterDistance = 457
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1042, 457)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtEmployee)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDepartment)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.rbtndetail)
        Me.RadPageViewPage1.Controls.Add(Me.rbtnSummary)
        Me.RadPageViewPage1.Controls.Add(Me.dtpToDate)
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtDivisionMult)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocationMult)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1021, 409)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'TxtEmployee
        '
        Me.TxtEmployee.arrDispalyMember = Nothing
        Me.TxtEmployee.arrValueMember = Nothing
        Me.TxtEmployee.Location = New System.Drawing.Point(101, 95)
        Me.TxtEmployee.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmployee.MyLinkLable1 = Nothing
        Me.TxtEmployee.MyLinkLable2 = Nothing
        Me.TxtEmployee.MyNullText = "All"
        Me.TxtEmployee.Name = "TxtEmployee"
        Me.TxtEmployee.Size = New System.Drawing.Size(466, 19)
        Me.TxtEmployee.TabIndex = 355
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(3, 96)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 354
        Me.MyLabel1.Text = "Employee"
        '
        'TxtDepartment
        '
        Me.TxtDepartment.arrDispalyMember = Nothing
        Me.TxtDepartment.arrValueMember = Nothing
        Me.TxtDepartment.Location = New System.Drawing.Point(101, 74)
        Me.TxtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDepartment.MyLinkLable1 = Nothing
        Me.TxtDepartment.MyLinkLable2 = Nothing
        Me.TxtDepartment.MyNullText = "All"
        Me.TxtDepartment.Name = "TxtDepartment"
        Me.TxtDepartment.Size = New System.Drawing.Size(466, 19)
        Me.TxtDepartment.TabIndex = 353
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(3, 75)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel3.TabIndex = 352
        Me.MyLabel3.Text = "Department"
        '
        'rbtndetail
        '
        Me.rbtndetail.Location = New System.Drawing.Point(188, 124)
        Me.rbtndetail.Name = "rbtndetail"
        Me.rbtndetail.Size = New System.Drawing.Size(49, 18)
        Me.rbtndetail.TabIndex = 1
        Me.rbtndetail.TabStop = False
        Me.rbtndetail.Text = "Detail"
        '
        'rbtnSummary
        '
        Me.rbtnSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnSummary.Location = New System.Drawing.Point(101, 124)
        Me.rbtnSummary.Name = "rbtnSummary"
        Me.rbtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rbtnSummary.TabIndex = 0
        Me.rbtnSummary.Text = "Summary"
        Me.rbtnSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Location = New System.Drawing.Point(249, 10)
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(86, 20)
        Me.dtpToDate.TabIndex = 349
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "22 July 2011"
        Me.dtpToDate.Value = New Date(2011, 7, 22, 13, 55, 15, 703)
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Location = New System.Drawing.Point(101, 10)
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(90, 20)
        Me.dtpFromDate.TabIndex = 348
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "22 July 2011"
        Me.dtpFromDate.Value = New Date(2011, 7, 22, 13, 55, 15, 703)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(197, 12)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel3.TabIndex = 351
        Me.RadLabel3.Text = "To Date"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(3, 10)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 350
        Me.RadLabel2.Text = "From Date"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(101, 53)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(466, 19)
        Me.txtDivisionMult.TabIndex = 347
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(3, 54)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(46, 18)
        Me.MyLabel2.TabIndex = 346
        Me.MyLabel2.Text = "Division"
        '
        'txtLocationMult
        '
        Me.txtLocationMult.arrDispalyMember = Nothing
        Me.txtLocationMult.arrValueMember = Nothing
        Me.txtLocationMult.Location = New System.Drawing.Point(101, 32)
        Me.txtLocationMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationMult.MyLinkLable1 = Nothing
        Me.txtLocationMult.MyLinkLable2 = Nothing
        Me.txtLocationMult.MyNullText = "All"
        Me.txtLocationMult.Name = "txtLocationMult"
        Me.txtLocationMult.Size = New System.Drawing.Size(466, 19)
        Me.txtLocationMult.TabIndex = 345
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(3, 33)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblLocation.TabIndex = 344
        Me.lblLocation.Text = "Location"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1021, 409)
        Me.RadPageViewPage2.Text = "Report"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gv1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = " "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(1021, 409)
        Me.RadGroupBox1.TabIndex = 1
        Me.RadGroupBox1.Text = " "
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1001, 379)
        Me.gv1.TabIndex = 146
        Me.gv1.VarID = ""
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btn_excel, Me.btn_pdf})
        Me.btnExport.Location = New System.Drawing.Point(91, 10)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(77, 21)
        Me.btnExport.TabIndex = 341
        Me.btnExport.Text = "Export"
        '
        'btn_excel
        '
        Me.btn_excel.Name = "btn_excel"
        Me.btn_excel.Text = "To Excel"
        '
        'btn_pdf
        '
        Me.btn_pdf.Name = "btn_pdf"
        Me.btn_pdf.Text = "To PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(922, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(113, 21)
        Me.btnClose.TabIndex = 340
        Me.btnClose.Text = "Close"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnRefresh.Location = New System.Drawing.Point(8, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(77, 21)
        Me.btnRefresh.TabIndex = 339
        Me.btnRefresh.Text = "Refresh"
        '
        'rptEmployeeAdvanceLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptEmployeeAdvanceLedger"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptEmployeeAdvanceLedger"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtndetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btn_savelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btn_deletelayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLocationMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents dtpToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpFromDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtEmployee As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtDepartment As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents rbtndetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents btn_excel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btn_pdf As Telerik.WinControls.UI.RadMenuItem
End Class

