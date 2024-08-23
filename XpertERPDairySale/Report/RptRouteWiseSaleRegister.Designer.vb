<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RptRouteWiseSaleRegister
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkdemand = New System.Windows.Forms.CheckBox()
        Me.TxtMultiCustomerCategory = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtStructure = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMultItem = New common.UserControls.txtMultiSelectFinder()
        Me.txtMultiCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtfDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvData = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmenuExport = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmenuPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.SplitContainer2.Panel1Collapsed = True
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1044, 495)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 1
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(1044, 495)
        Me.SplitContainer1.SplitterDistance = 454
        Me.SplitContainer1.TabIndex = 1
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(1044, 454)
        Me.RadPageView1.TabIndex = 72
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkdemand)
        Me.RadPageViewPage1.Controls.Add(Me.TxtMultiCustomerCategory)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocation)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.txtStructure)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.txtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultItem)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1023, 406)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'chkdemand
        '
        Me.chkdemand.AutoSize = True
        Me.chkdemand.Location = New System.Drawing.Point(345, 16)
        Me.chkdemand.Name = "chkdemand"
        Me.chkdemand.Size = New System.Drawing.Size(68, 17)
        Me.chkdemand.TabIndex = 419
        Me.chkdemand.Text = "demand"
        Me.chkdemand.UseVisualStyleBackColor = True
        '
        'TxtMultiCustomerCategory
        '
        Me.TxtMultiCustomerCategory.arrDispalyMember = Nothing
        Me.TxtMultiCustomerCategory.arrValueMember = Nothing
        Me.TxtMultiCustomerCategory.Location = New System.Drawing.Point(121, 170)
        Me.TxtMultiCustomerCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiCustomerCategory.MyLinkLable1 = Me.MyLabel13
        Me.TxtMultiCustomerCategory.MyLinkLable2 = Nothing
        Me.TxtMultiCustomerCategory.MyNullText = "All"
        Me.TxtMultiCustomerCategory.Name = "TxtMultiCustomerCategory"
        Me.TxtMultiCustomerCategory.Size = New System.Drawing.Size(300, 19)
        Me.TxtMultiCustomerCategory.TabIndex = 418
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(13, 50)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel13.TabIndex = 334
        Me.MyLabel13.Text = "Customer"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(12, 171)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(103, 18)
        Me.MyLabel5.TabIndex = 417
        Me.MyLabel5.Text = "Customer Category"
        '
        'txtLocation
        '
        Me.txtLocation.arrDispalyMember = Nothing
        Me.txtLocation.arrValueMember = Nothing
        Me.txtLocation.Location = New System.Drawing.Point(122, 146)
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyNullText = "All"
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(299, 19)
        Me.txtLocation.TabIndex = 416
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 147)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel4.TabIndex = 415
        Me.MyLabel4.Text = "Location"
        '
        'txtStructure
        '
        Me.txtStructure.arrDispalyMember = Nothing
        Me.txtStructure.arrValueMember = Nothing
        Me.txtStructure.Location = New System.Drawing.Point(121, 121)
        Me.txtStructure.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStructure.MyLinkLable1 = Nothing
        Me.txtStructure.MyLinkLable2 = Nothing
        Me.txtStructure.MyNullText = "All"
        Me.txtStructure.Name = "txtStructure"
        Me.txtStructure.Size = New System.Drawing.Size(299, 19)
        Me.txtStructure.TabIndex = 414
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 122)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(77, 18)
        Me.MyLabel3.TabIndex = 413
        Me.MyLabel3.Text = "Item Structure"
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(122, 97)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Nothing
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "All"
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(299, 19)
        Me.txtRoute.TabIndex = 412
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 98)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel2.TabIndex = 411
        Me.MyLabel2.Text = "Route"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 74)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(29, 18)
        Me.MyLabel1.TabIndex = 336
        Me.MyLabel1.Text = "Item"
        '
        'txtMultItem
        '
        Me.txtMultItem.arrDispalyMember = Nothing
        Me.txtMultItem.arrValueMember = Nothing
        Me.txtMultItem.Location = New System.Drawing.Point(122, 73)
        Me.txtMultItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultItem.MyLinkLable1 = Me.MyLabel1
        Me.txtMultItem.MyLinkLable2 = Nothing
        Me.txtMultItem.MyNullText = "All"
        Me.txtMultItem.Name = "txtMultItem"
        Me.txtMultItem.Size = New System.Drawing.Size(299, 19)
        Me.txtMultItem.TabIndex = 335
        '
        'txtMultiCustomer
        '
        Me.txtMultiCustomer.arrDispalyMember = Nothing
        Me.txtMultiCustomer.arrValueMember = Nothing
        Me.txtMultiCustomer.Location = New System.Drawing.Point(122, 49)
        Me.txtMultiCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiCustomer.MyLinkLable1 = Me.MyLabel13
        Me.txtMultiCustomer.MyLinkLable2 = Nothing
        Me.txtMultiCustomer.MyNullText = "All"
        Me.txtMultiCustomer.Name = "txtMultiCustomer"
        Me.txtMultiCustomer.Size = New System.Drawing.Size(299, 19)
        Me.txtMultiCustomer.TabIndex = 333
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
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvData)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1023, 406)
        Me.RadPageViewPage2.Text = "Report"
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
        Me.gvData.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvData.MyStopExport = False
        Me.gvData.Name = "gvData"
        Me.gvData.ShowHeaderCellButtons = True
        Me.gvData.Size = New System.Drawing.Size(1023, 406)
        Me.gvData.TabIndex = 0
        Me.gvData.VarID = ""
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmenuExport, Me.rmenuPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(162, 11)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadSplitButton1.TabIndex = 20
        Me.RadSplitButton1.Text = "Export"
        '
        'rmenuExport
        '
        Me.rmenuExport.Name = "rmenuExport"
        Me.rmenuExport.Text = "Export"
        '
        'rmenuPDF
        '
        Me.rmenuPDF.Name = "rmenuPDF"
        Me.rmenuPDF.Text = "PDF"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(967, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 163
        Me.btnClose.Text = "Close"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(9, 10)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(68, 18)
        Me.btnGo.TabIndex = 160
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(84, 11)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 159
        Me.btnReset.Text = "Reset"
        '
        'RptRouteWiseSaleRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 495)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Name = "RptRouteWiseSaleRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Route Wise Sale Register"
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvData.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtMultiCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtToDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents txtfDate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvData As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtMultItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmenuExport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtStructure As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents rmenuPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtMultiCustomerCategory As common.UserControls.txtMultiSelectFinder
    Friend WithEvents chkdemand As CheckBox
End Class

