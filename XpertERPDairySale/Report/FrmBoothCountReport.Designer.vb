<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBoothCountReport
    Inherits FrmMainTranScreen
    'Inherits Telerik.WinControls.UI.RadForm

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
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.TxtMultiRoute = New common.UserControls.txtMultiSelectFinder()
        Me.LblRoute = New common.Controls.MyLabel()
        Me.gbDocStatus = New Telerik.WinControls.UI.RadGroupBox()
        Me.BtnRouteGroupWise = New common.Controls.MyRadioButton()
        Me.BtnRouteWise = New common.Controls.MyRadioButton()
        Me.BtnBoothDetail = New common.Controls.MyRadioButton()
        Me.BtnDateGroupWise = New common.Controls.MyRadioButton()
        Me.BtnDateWise = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtfDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmenuPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.gvData = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.LblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbDocStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDocStatus.SuspendLayout()
        CType(Me.BtnRouteGroupWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnRouteWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnBoothDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDateGroupWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDateWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(693, 390)
        Me.SplitContainer1.SplitterDistance = 353
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
        Me.RadPageView1.Size = New System.Drawing.Size(693, 353)
        Me.RadPageView1.TabIndex = 74
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiRoute)
        Me.RadPageViewPage1.Controls.Add(Me.LblRoute)
        Me.RadPageViewPage1.Controls.Add(Me.gbDocStatus)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(672, 305)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'TxtMultiRoute
        '
        Me.TxtMultiRoute.arrDispalyMember = Nothing
        Me.TxtMultiRoute.arrValueMember = Nothing
        Me.TxtMultiRoute.Location = New System.Drawing.Point(60, 80)
        Me.TxtMultiRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtMultiRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiRoute.MyLinkLable1 = Nothing
        Me.TxtMultiRoute.MyLinkLable2 = Nothing
        Me.TxtMultiRoute.MyNullText = "All"
        Me.TxtMultiRoute.Name = "TxtMultiRoute"
        Me.TxtMultiRoute.Size = New System.Drawing.Size(266, 19)
        Me.TxtMultiRoute.TabIndex = 447
        '
        'LblRoute
        '
        Me.LblRoute.FieldName = Nothing
        Me.LblRoute.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRoute.Location = New System.Drawing.Point(16, 80)
        Me.LblRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.LblRoute.Name = "LblRoute"
        Me.LblRoute.Size = New System.Drawing.Size(36, 18)
        Me.LblRoute.TabIndex = 446
        Me.LblRoute.Text = "Route"
        '
        'gbDocStatus
        '
        Me.gbDocStatus.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbDocStatus.Controls.Add(Me.BtnRouteGroupWise)
        Me.gbDocStatus.Controls.Add(Me.BtnRouteWise)
        Me.gbDocStatus.Controls.Add(Me.BtnBoothDetail)
        Me.gbDocStatus.Controls.Add(Me.BtnDateGroupWise)
        Me.gbDocStatus.Controls.Add(Me.BtnDateWise)
        Me.gbDocStatus.HeaderText = "Date"
        Me.gbDocStatus.Location = New System.Drawing.Point(16, 118)
        Me.gbDocStatus.Name = "gbDocStatus"
        Me.gbDocStatus.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbDocStatus.Size = New System.Drawing.Size(310, 95)
        Me.gbDocStatus.TabIndex = 443
        Me.gbDocStatus.Text = "Date"
        '
        'BtnRouteGroupWise
        '
        Me.BtnRouteGroupWise.Location = New System.Drawing.Point(107, 64)
        Me.BtnRouteGroupWise.MyLinkLable1 = Nothing
        Me.BtnRouteGroupWise.MyLinkLable2 = Nothing
        Me.BtnRouteGroupWise.Name = "BtnRouteGroupWise"
        Me.BtnRouteGroupWise.Size = New System.Drawing.Size(111, 18)
        Me.BtnRouteGroupWise.TabIndex = 399
        Me.BtnRouteGroupWise.TabStop = False
        Me.BtnRouteGroupWise.Text = "Route Group Wise"
        '
        'BtnRouteWise
        '
        Me.BtnRouteWise.Location = New System.Drawing.Point(23, 64)
        Me.BtnRouteWise.MyLinkLable1 = Nothing
        Me.BtnRouteWise.MyLinkLable2 = Nothing
        Me.BtnRouteWise.Name = "BtnRouteWise"
        Me.BtnRouteWise.Size = New System.Drawing.Size(78, 18)
        Me.BtnRouteWise.TabIndex = 398
        Me.BtnRouteWise.TabStop = False
        Me.BtnRouteWise.Text = "Route-Wise"
        '
        'BtnBoothDetail
        '
        Me.BtnBoothDetail.Location = New System.Drawing.Point(218, 23)
        Me.BtnBoothDetail.MyLinkLable1 = Nothing
        Me.BtnBoothDetail.MyLinkLable2 = Nothing
        Me.BtnBoothDetail.Name = "BtnBoothDetail"
        Me.BtnBoothDetail.Size = New System.Drawing.Size(82, 18)
        Me.BtnBoothDetail.TabIndex = 397
        Me.BtnBoothDetail.TabStop = False
        Me.BtnBoothDetail.Text = "Booth Detail"
        '
        'BtnDateGroupWise
        '
        Me.BtnDateGroupWise.Location = New System.Drawing.Point(103, 23)
        Me.BtnDateGroupWise.MyLinkLable1 = Nothing
        Me.BtnDateGroupWise.MyLinkLable2 = Nothing
        Me.BtnDateGroupWise.Name = "BtnDateGroupWise"
        Me.BtnDateGroupWise.Size = New System.Drawing.Size(113, 18)
        Me.BtnDateGroupWise.TabIndex = 391
        Me.BtnDateGroupWise.TabStop = False
        Me.BtnDateGroupWise.Text = "Booth Group-Wise"
        '
        'BtnDateWise
        '
        Me.BtnDateWise.CheckState = System.Windows.Forms.CheckState.Checked
        Me.BtnDateWise.Location = New System.Drawing.Point(23, 23)
        Me.BtnDateWise.MyLinkLable1 = Nothing
        Me.BtnDateWise.MyLinkLable2 = Nothing
        Me.BtnDateWise.Name = "BtnDateWise"
        Me.BtnDateWise.Size = New System.Drawing.Size(72, 18)
        Me.BtnDateWise.TabIndex = 396
        Me.BtnDateWise.Text = "Date-Wise"
        Me.BtnDateWise.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox4.Controls.Add(Me.txtToDate)
        Me.RadGroupBox4.Controls.Add(Me.txtfDate)
        Me.RadGroupBox4.HeaderText = "Date Range"
        Me.RadGroupBox4.Location = New System.Drawing.Point(16, 20)
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
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvData)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(672, 305)
        Me.RadPageViewPage2.Text = "Report"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(622, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 426
        Me.btnClose.Text = "Close"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmenuExport, Me.rmenuPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(148, 8)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadSplitButton1.TabIndex = 425
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
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(75, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 422
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(3, 8)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 162
        Me.btnGo.Text = ">>>"
        '
        'gvData
        '
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvData.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvData.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvData.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gvData.MyStopExport = False
        Me.gvData.Name = "gvData"
        Me.gvData.ShowHeaderCellButtons = True
        Me.gvData.Size = New System.Drawing.Size(672, 305)
        Me.gvData.TabIndex = 1
        Me.gvData.VarID = ""
        '
        'FrmBoothCountReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 390)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBoothCountReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmBoothCountReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.LblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbDocStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDocStatus.ResumeLayout(False)
        Me.gbDocStatus.PerformLayout()
        CType(Me.BtnRouteGroupWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnRouteWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnBoothDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDateGroupWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDateWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents rmenuExport As RadMenuItem
    Friend WithEvents rmenuPDF As RadMenuItem
    Friend WithEvents btnClose As RadButton
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents gbDocStatus As RadGroupBox
    Friend WithEvents BtnDateWise As common.Controls.MyRadioButton
    Friend WithEvents BtnDateGroupWise As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtToDate As RadDateTimePicker
    Friend WithEvents txtfDate As RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents BtnBoothDetail As common.Controls.MyRadioButton
    Friend WithEvents BtnRouteGroupWise As common.Controls.MyRadioButton
    Friend WithEvents BtnRouteWise As common.Controls.MyRadioButton
    Friend WithEvents LblRoute As common.Controls.MyLabel
    Friend WithEvents TxtMultiRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents gvData As common.UserControls.MyRadGridView
End Class

