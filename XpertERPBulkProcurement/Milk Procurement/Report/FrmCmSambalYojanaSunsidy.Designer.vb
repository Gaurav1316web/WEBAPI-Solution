<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCmSambalYojanaSunsidy
    'Inherits System.Windows.Forms.Form
    Inherits XpertERPEngine.FrmMainTranScreen
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
        Dim TableViewDefinition7 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.LBLVidhanSabha = New common.Controls.MyLabel()
        Me.LBLdistrict = New common.Controls.MyLabel()
        Me.TxtVidhanSabha = New common.UserControls.txtMultiSelectFinder()
        Me.TxtDistrict = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Details = New common.Controls.MyRadioButton()
        Me.VidhanSabhaWise = New common.Controls.MyRadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmenuPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.LBLVidhanSabha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBLdistrict, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.Details, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VidhanSabhaWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Size = New System.Drawing.Size(731, 358)
        Me.SplitContainer1.SplitterDistance = 312
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(731, 312)
        Me.RadPageView1.TabIndex = 2
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.LBLVidhanSabha)
        Me.RadPageViewPage1.Controls.Add(Me.LBLdistrict)
        Me.RadPageViewPage1.Controls.Add(Me.TxtVidhanSabha)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDistrict)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(710, 264)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'LBLVidhanSabha
        '
        Me.LBLVidhanSabha.FieldName = Nothing
        Me.LBLVidhanSabha.Location = New System.Drawing.Point(21, 173)
        Me.LBLVidhanSabha.Name = "LBLVidhanSabha"
        Me.LBLVidhanSabha.Size = New System.Drawing.Size(75, 18)
        Me.LBLVidhanSabha.TabIndex = 447
        Me.LBLVidhanSabha.Text = "Vidhan Sabha"
        '
        'LBLdistrict
        '
        Me.LBLdistrict.FieldName = Nothing
        Me.LBLdistrict.Location = New System.Drawing.Point(21, 146)
        Me.LBLdistrict.Name = "LBLdistrict"
        Me.LBLdistrict.Size = New System.Drawing.Size(41, 18)
        Me.LBLdistrict.TabIndex = 446
        Me.LBLdistrict.Text = "District"
        '
        'TxtVidhanSabha
        '
        Me.TxtVidhanSabha.arrDispalyMember = Nothing
        Me.TxtVidhanSabha.arrValueMember = Nothing
        Me.TxtVidhanSabha.Location = New System.Drawing.Point(98, 173)
        Me.TxtVidhanSabha.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtVidhanSabha.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVidhanSabha.MyLinkLable1 = Nothing
        Me.TxtVidhanSabha.MyLinkLable2 = Nothing
        Me.TxtVidhanSabha.MyNullText = "All"
        Me.TxtVidhanSabha.Name = "TxtVidhanSabha"
        Me.TxtVidhanSabha.Size = New System.Drawing.Size(257, 19)
        Me.TxtVidhanSabha.TabIndex = 445
        '
        'TxtDistrict
        '
        Me.TxtDistrict.arrDispalyMember = Nothing
        Me.TxtDistrict.arrValueMember = Nothing
        Me.TxtDistrict.Location = New System.Drawing.Point(98, 146)
        Me.TxtDistrict.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtDistrict.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDistrict.MyLinkLable1 = Nothing
        Me.TxtDistrict.MyLinkLable2 = Nothing
        Me.TxtDistrict.MyNullText = "All"
        Me.TxtDistrict.Name = "TxtDistrict"
        Me.TxtDistrict.Size = New System.Drawing.Size(257, 19)
        Me.TxtDistrict.TabIndex = 444
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(21, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(334, 113)
        Me.RadGroupBox1.TabIndex = 443
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "MMM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(226, 9)
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
        Me.txtToDate.TabIndex = 443
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "Dec-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.Details)
        Me.RadGroupBox3.Controls.Add(Me.VidhanSabhaWise)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(6, 50)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(214, 37)
        Me.RadGroupBox3.TabIndex = 442
        '
        'Details
        '
        Me.Details.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Details.Location = New System.Drawing.Point(5, 9)
        Me.Details.MyLinkLable1 = Nothing
        Me.Details.MyLinkLable2 = Nothing
        Me.Details.Name = "Details"
        Me.Details.Size = New System.Drawing.Size(54, 18)
        Me.Details.TabIndex = 396
        Me.Details.Text = "Details"
        Me.Details.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'VidhanSabhaWise
        '
        Me.VidhanSabhaWise.Location = New System.Drawing.Point(87, 9)
        Me.VidhanSabhaWise.MyLinkLable1 = Nothing
        Me.VidhanSabhaWise.MyLinkLable2 = Nothing
        Me.VidhanSabhaWise.Name = "VidhanSabhaWise"
        Me.VidhanSabhaWise.Size = New System.Drawing.Size(116, 18)
        Me.VidhanSabhaWise.TabIndex = 391
        Me.VidhanSabhaWise.TabStop = False
        Me.VidhanSabhaWise.Text = "Vidhan Sabha Wise"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(165, 9)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel1.TabIndex = 14
        Me.MyLabel1.Text = "To Month"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "MMM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(77, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 11
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "Dec-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(5, 9)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(69, 18)
        Me.RadLabel3.TabIndex = 12
        Me.RadLabel3.Text = "From Month"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(710, 271)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowGroupedColumns = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition7
        Me.gv.MyExportFilePath = ""
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(710, 271)
        Me.gv.TabIndex = 3
        Me.gv.VarID = ""
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(649, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 22)
        Me.btnclose.TabIndex = 160
        Me.btnclose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(86, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(70, 22)
        Me.btnReset.TabIndex = 9
        Me.btnReset.Text = "Reset"
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReport.Location = New System.Drawing.Point(10, 8)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 22)
        Me.btnReport.TabIndex = 8
        Me.btnReport.Text = ">>>"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmenuExport, Me.rmenuPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(162, 8)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(78, 22)
        Me.RadSplitButton1.TabIndex = 426
        Me.RadSplitButton1.Text = "Export"
        '
        'rmenuExport
        '
        Me.rmenuExport.Name = "rmenuExport"
        Me.rmenuExport.Text = "Export"
        Me.rmenuExport.UseCompatibleTextRendering = False
        '
        'rmenuPDF
        '
        Me.rmenuPDF.Name = "rmenuPDF"
        Me.rmenuPDF.Text = "PDF"
        Me.rmenuPDF.UseCompatibleTextRendering = False
        '
        'FrmCmSambalYojanaSunsidy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 358)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCmSambalYojanaSunsidy"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCmSambalYojanaSunsidy"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.LBLVidhanSabha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LBLdistrict, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.Details, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VidhanSabhaWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents LBLVidhanSabha As common.Controls.MyLabel
    Friend WithEvents LBLdistrict As common.Controls.MyLabel
    Friend WithEvents TxtVidhanSabha As common.UserControls.txtMultiSelectFinder
    Friend WithEvents TxtDistrict As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents Details As common.Controls.MyRadioButton
    Friend WithEvents VidhanSabhaWise As common.Controls.MyRadioButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents btnReport As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents rmenuExport As RadMenuItem
    Friend WithEvents rmenuPDF As RadMenuItem
End Class
