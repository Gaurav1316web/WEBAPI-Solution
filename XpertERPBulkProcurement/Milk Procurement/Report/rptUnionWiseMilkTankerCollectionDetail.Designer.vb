<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptUnionWiseMilkTankerCollectionDetail
    'Inherits System.Windows.Forms.Form
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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TxtMultiTanker = New common.UserControls.txtMultiSelectFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.lblTanker = New common.Controls.MyLabel()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.txtUnion = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblFromdate = New common.Controls.MyLabel()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnSplitExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btngo = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTanker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSplitExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btngo)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 400
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 400)
        Me.RadPageView1.TabIndex = 7
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 352)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.TxtMultiTanker)
        Me.RadPanel1.Controls.Add(Me.lblTanker)
        Me.RadPanel1.Controls.Add(Me.txtRoute)
        Me.RadPanel1.Controls.Add(Me.lblRoute)
        Me.RadPanel1.Controls.Add(Me.txtUnion)
        Me.RadPanel1.Controls.Add(Me.lblLocation)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox3)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(779, 352)
        Me.RadPanel1.TabIndex = 15
        '
        'TxtMultiTanker
        '
        Me.TxtMultiTanker.arrDispalyMember = Nothing
        Me.TxtMultiTanker.arrValueMember = Nothing
        Me.TxtMultiTanker.Location = New System.Drawing.Point(87, 135)
        Me.TxtMultiTanker.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiTanker.MyLinkLable1 = Me.lblRoute
        Me.TxtMultiTanker.MyLinkLable2 = Nothing
        Me.TxtMultiTanker.MyNullText = "All"
        Me.TxtMultiTanker.Name = "TxtMultiTanker"
        Me.TxtMultiTanker.Size = New System.Drawing.Size(232, 19)
        Me.TxtMultiTanker.TabIndex = 452
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoute.Location = New System.Drawing.Point(17, 112)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(36, 18)
        Me.lblRoute.TabIndex = 450
        Me.lblRoute.Text = "Route"
        '
        'lblTanker
        '
        Me.lblTanker.FieldName = Nothing
        Me.lblTanker.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTanker.Location = New System.Drawing.Point(17, 136)
        Me.lblTanker.Name = "lblTanker"
        Me.lblTanker.Size = New System.Drawing.Size(40, 18)
        Me.lblTanker.TabIndex = 451
        Me.lblTanker.Text = "Tanker"
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(87, 112)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.lblRoute
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(232, 19)
        Me.txtRoute.TabIndex = 449
        '
        'txtUnion
        '
        Me.txtUnion.arrDispalyMember = Nothing
        Me.txtUnion.arrValueMember = Nothing
        Me.txtUnion.Location = New System.Drawing.Point(89, 88)
        Me.txtUnion.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnion.MyLinkLable1 = Me.lblLocation
        Me.txtUnion.MyLinkLable2 = Nothing
        Me.txtUnion.MyNullText = "All"
        Me.txtUnion.Name = "txtUnion"
        Me.txtUnion.Size = New System.Drawing.Size(230, 19)
        Me.txtUnion.TabIndex = 447
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(17, 88)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(36, 18)
        Me.lblLocation.TabIndex = 448
        Me.lblLocation.Text = "Union"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.lblFromdate)
        Me.RadGroupBox3.Controls.Add(Me.lblToDate)
        Me.RadGroupBox3.Controls.Add(Me.txtToDate)
        Me.RadGroupBox3.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(17, 16)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(304, 57)
        Me.RadGroupBox3.TabIndex = 446
        Me.RadGroupBox3.Text = "Date Range"
        '
        'lblFromdate
        '
        Me.lblFromdate.FieldName = Nothing
        Me.lblFromdate.Location = New System.Drawing.Point(5, 23)
        Me.lblFromdate.Name = "lblFromdate"
        Me.lblFromdate.Size = New System.Drawing.Size(59, 18)
        Me.lblFromdate.TabIndex = 77
        Me.lblFromdate.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(154, 25)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 78
        Me.lblToDate.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(205, 23)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 2
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "30/05/2011"
        Me.txtToDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(70, 23)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(78, 20)
        Me.txtFromDate.TabIndex = 1
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "30/05/2011"
        Me.txtFromDate.Value = New Date(2011, 5, 30, 12, 41, 54, 500)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 364)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.MyExportAPI = False
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 364)
        Me.gv1.TabIndex = 2
        Me.gv1.VarID = ""
        '
        'btnSplitExport
        '
        Me.btnSplitExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSplitExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel})
        Me.btnSplitExport.Location = New System.Drawing.Point(405, 16)
        Me.btnSplitExport.Name = "btnSplitExport"
        Me.btnSplitExport.Size = New System.Drawing.Size(81, 18)
        Me.btnSplitExport.TabIndex = 458
        Me.btnSplitExport.Text = "Export"
        Me.btnSplitExport.Visible = False
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(707, 12)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(81, 18)
        Me.btnclose.TabIndex = 457
        Me.btnclose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(100, 13)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(81, 18)
        Me.btnReset.TabIndex = 456
        Me.btnReset.Text = "Reset"
        '
        'btngo
        '
        Me.btngo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngo.Location = New System.Drawing.Point(13, 12)
        Me.btngo.Name = "btngo"
        Me.btngo.Size = New System.Drawing.Size(81, 18)
        Me.btngo.TabIndex = 455
        Me.btngo.Text = ">>>"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(187, 14)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(81, 18)
        Me.btnPrint.TabIndex = 459
        Me.btnPrint.Text = "Print"
        '
        'rptUnionWiseMilkTankerCollectionDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptUnionWiseMilkTankerCollectionDetail"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptUnionWiseMilkTankerCollectionDetail"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTanker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.lblFromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSplitExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btngo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents lblFromdate As common.Controls.MyLabel
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnSplitExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents btnclose As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btngo As RadButton
    Friend WithEvents txtUnion As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents TxtMultiTanker As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblTanker As common.Controls.MyLabel
    Friend WithEvents btnPrint As RadButton
End Class
