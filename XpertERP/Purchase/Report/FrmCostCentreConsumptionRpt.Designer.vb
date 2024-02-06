<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostCentreConsumptionRpt
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
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtItemType = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtDepartment = New common.UserControls.txtMultiSelectFinder()
        Me.txtplantUnit = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtfDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvData = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDetail = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcelExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDFExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(1044, 475)
        Me.SplitContainer1.SplitterDistance = 439
        Me.SplitContainer1.TabIndex = 20
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1044, 439)
        Me.RadPageView1.TabIndex = 73
        Me.RadPageView1.Text = "RadPageView2"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtItemType)
        Me.RadPageViewPage1.Controls.Add(Me.txtDepartment)
        Me.RadPageViewPage1.Controls.Add(Me.txtplantUnit)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel14)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel19)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1023, 391)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'txtItemType
        '
        Me.txtItemType.arrDispalyMember = Nothing
        Me.txtItemType.arrValueMember = Nothing
        Me.txtItemType.Location = New System.Drawing.Point(112, 121)
        Me.txtItemType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.MyLinkLable1 = Me.MyLabel13
        Me.txtItemType.MyLinkLable2 = Nothing
        Me.txtItemType.MyNullText = "All"
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.Size = New System.Drawing.Size(309, 19)
        Me.txtItemType.TabIndex = 343
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(13, 50)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel13.TabIndex = 334
        Me.MyLabel13.Text = "Location"
        '
        'txtDepartment
        '
        Me.txtDepartment.arrDispalyMember = Nothing
        Me.txtDepartment.arrValueMember = Nothing
        Me.txtDepartment.Location = New System.Drawing.Point(112, 97)
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Me.MyLabel13
        Me.txtDepartment.MyLinkLable2 = Nothing
        Me.txtDepartment.MyNullText = "All"
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(309, 19)
        Me.txtDepartment.TabIndex = 342
        '
        'txtplantUnit
        '
        Me.txtplantUnit.arrDispalyMember = Nothing
        Me.txtplantUnit.arrValueMember = Nothing
        Me.txtplantUnit.Location = New System.Drawing.Point(112, 73)
        Me.txtplantUnit.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtplantUnit.MyLinkLable1 = Me.MyLabel13
        Me.txtplantUnit.MyLinkLable2 = Nothing
        Me.txtplantUnit.MyNullText = "All"
        Me.txtplantUnit.Name = "txtplantUnit"
        Me.txtplantUnit.Size = New System.Drawing.Size(309, 19)
        Me.txtplantUnit.TabIndex = 341
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(13, 98)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(66, 18)
        Me.MyLabel10.TabIndex = 340
        Me.MyLabel10.Text = "Department"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(112, 49)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.MyLabel13
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(309, 19)
        Me.txtLocation.TabIndex = 333
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(13, 122)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(56, 18)
        Me.MyLabel14.TabIndex = 332
        Me.MyLabel14.Text = "Item Type"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox4.Controls.Add(Me.txtToDate)
        Me.RadGroupBox4.Controls.Add(Me.txtfDate)
        Me.RadGroupBox4.HeaderText = "Date Range"
        Me.RadGroupBox4.Location = New System.Drawing.Point(16, 1)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(310, 42)
        Me.RadGroupBox4.TabIndex = 53
        Me.RadGroupBox4.Text = "Date Range"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(142, 16)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel17.TabIndex = 3
        Me.MyLabel17.Text = "To"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel18.TabIndex = 2
        Me.MyLabel18.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(169, 15)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtfDate
        '
        Me.txtfDate.CustomFormat = "dd/MM/yyyy"
        Me.txtfDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtfDate.Location = New System.Drawing.Point(44, 15)
        Me.txtfDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfDate.Name = "txtfDate"
        Me.txtfDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtfDate.Size = New System.Drawing.Size(88, 20)
        Me.txtfDate.TabIndex = 0
        Me.txtfDate.TabStop = False
        Me.txtfDate.Text = "24/10/2011"
        Me.txtfDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Location = New System.Drawing.Point(13, 76)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(72, 18)
        Me.MyLabel19.TabIndex = 323
        Me.MyLabel19.Text = "Plant/Section"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvData)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1023, 391)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gvData
        '
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvData.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvData.Name = "gvData"
        Me.gvData.ShowHeaderCellButtons = True
        Me.gvData.Size = New System.Drawing.Size(1023, 391)
        Me.gvData.TabIndex = 0
        Me.gvData.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvDetail)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(1023, 391)
        Me.RadPageViewPage3.Text = "Detail"
        '
        'gvDetail
        '
        Me.gvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetail.Location = New System.Drawing.Point(0, 0)
        Me.gvDetail.Name = "gvDetail"
        Me.gvDetail.Size = New System.Drawing.Size(1023, 391)
        Me.gvDetail.TabIndex = 0
        Me.gvDetail.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(967, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 169
        Me.btnClose.Text = "Close"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcelExport, Me.btnPDFExport})
        Me.RadSplitButton1.Location = New System.Drawing.Point(158, 7)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(80, 18)
        Me.RadSplitButton1.TabIndex = 168
        Me.RadSplitButton1.Text = "Export"
        '
        'btnExcelExport
        '
        Me.btnExcelExport.AccessibleDescription = "Excel"
        Me.btnExcelExport.AccessibleName = "Excel"
        Me.btnExcelExport.Name = "btnExcelExport"
        Me.btnExcelExport.Text = "Excel"
        '
        'btnPDFExport
        '
        Me.btnPDFExport.AccessibleDescription = "PDF"
        Me.btnPDFExport.AccessibleName = "PDF"
        Me.btnPDFExport.Name = "btnPDFExport"
        Me.btnPDFExport.Text = "PDF"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(9, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 167
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(84, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 166
        Me.btnReset.Text = "Reset"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1044, 20)
        Me.RadMenu1.TabIndex = 19
        Me.RadMenu1.Text = "RadMenu1"
        '
        'FrmCostCentreConsumptionRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 495)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmCostCentreConsumptionRpt"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmCostCentreConsumptionRpt"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtfDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvData As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtItemType As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtDepartment As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtplantUnit As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnExcelExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvDetail As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents btnPDFExport As Telerik.WinControls.UI.RadMenuItem
End Class

