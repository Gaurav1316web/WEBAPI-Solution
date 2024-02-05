<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptAvgSaleDetailReport
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmsaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtYear = New common.UserControls.txtFinder()
        Me.txtRoute = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.ddMonth = New Telerik.WinControls.UI.RadDropDownList()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(692, 20)
        Me.RadMenu1.TabIndex = 2
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmsaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmsaveLayout
        '
        Me.rmsaveLayout.Name = "rmsaveLayout"
        Me.rmsaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(692, 376)
        Me.SplitContainer1.SplitterDistance = 326
        Me.SplitContainer1.TabIndex = 3
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(692, 326)
        Me.RadPageView1.TabIndex = 4
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadPanel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(671, 278)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(671, 278)
        Me.RadPanel1.TabIndex = 15
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtYear)
        Me.RadGroupBox1.Controls.Add(Me.txtRoute)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.ddMonth)
        Me.RadGroupBox1.Controls.Add(Me.lblLocation)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(241, 101)
        Me.RadGroupBox1.TabIndex = 389
        '
        'txtYear
        '
        Me.txtYear.CalculationExpression = Nothing
        Me.txtYear.FieldCode = Nothing
        Me.txtYear.FieldDesc = Nothing
        Me.txtYear.FieldMaxLength = 0
        Me.txtYear.FieldName = Nothing
        Me.txtYear.isCalculatedField = False
        Me.txtYear.IsSourceFromTable = False
        Me.txtYear.IsSourceFromValueList = False
        Me.txtYear.IsUnique = False
        Me.txtYear.Location = New System.Drawing.Point(96, 12)
        Me.txtYear.MendatroryField = True
        Me.txtYear.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.MyLinkLable1 = Nothing
        Me.txtYear.MyLinkLable2 = Nothing
        Me.txtYear.MyReadOnly = False
        Me.txtYear.MyShowMasterFormButton = False
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ReferenceFieldDesc = Nothing
        Me.txtYear.ReferenceFieldName = Nothing
        Me.txtYear.ReferenceTableName = Nothing
        Me.txtYear.Size = New System.Drawing.Size(139, 19)
        Me.txtYear.TabIndex = 392
        Me.txtYear.Value = ""
        '
        'txtRoute
        '
        Me.txtRoute.CalculationExpression = Nothing
        Me.txtRoute.FieldCode = Nothing
        Me.txtRoute.FieldDesc = Nothing
        Me.txtRoute.FieldMaxLength = 0
        Me.txtRoute.FieldName = Nothing
        Me.txtRoute.isCalculatedField = False
        Me.txtRoute.IsSourceFromTable = False
        Me.txtRoute.IsSourceFromValueList = False
        Me.txtRoute.IsUnique = False
        Me.txtRoute.Location = New System.Drawing.Point(94, 64)
        Me.txtRoute.MendatroryField = True
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyReadOnly = False
        Me.txtRoute.MyShowMasterFormButton = False
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(139, 19)
        Me.txtRoute.TabIndex = 391
        Me.txtRoute.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(5, 35)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(40, 18)
        Me.MyLabel1.TabIndex = 390
        Me.MyLabel1.Text = "Month"
        '
        'ddMonth
        '
        Me.ddMonth.AutoCompleteDisplayMember = Nothing
        Me.ddMonth.AutoCompleteValueMember = Nothing
        Me.ddMonth.DropDownAnimationEnabled = True
        Me.ddMonth.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        RadListDataItem1.Text = "Credit"
        RadListDataItem2.Text = "Cash"
        RadListDataItem3.Text = "Both"
        Me.ddMonth.Items.Add(RadListDataItem1)
        Me.ddMonth.Items.Add(RadListDataItem2)
        Me.ddMonth.Items.Add(RadListDataItem3)
        Me.ddMonth.Location = New System.Drawing.Point(95, 36)
        Me.ddMonth.Name = "ddMonth"
        Me.ddMonth.Size = New System.Drawing.Size(139, 20)
        Me.ddMonth.TabIndex = 389
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(5, 62)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(36, 18)
        Me.lblLocation.TabIndex = 388
        Me.lblLocation.Text = "Route"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(2, 12)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel3.TabIndex = 364
        Me.MyLabel3.Text = " Financial Year"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(671, 278)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(671, 278)
        Me.gv1.TabIndex = 2
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(162, 15)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 22)
        Me.RadSplitButton1.TabIndex = 154
        Me.RadSplitButton1.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseCompatibleTextRendering = False
        '
        'btnPDF
        '
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        Me.btnPDF.UseCompatibleTextRendering = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(599, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 22)
        Me.btnClose.TabIndex = 153
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(14, 15)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 151
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(88, 15)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 152
        Me.btnReset.Text = "Reset"
        '
        'rptAvgSaleDetailReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 396)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "rptAvgSaleDetailReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Average Sale Detail Report"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rmsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadPanel1 As RadPanel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitButton1 As RadSplitButton
    Friend WithEvents btnExcel As RadMenuItem
    Friend WithEvents btnPDF As RadMenuItem
    Friend WithEvents ddMonth As RadDropDownList
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtYear As common.UserControls.txtFinder
    Friend WithEvents txtRoute As common.UserControls.txtFinder
End Class

