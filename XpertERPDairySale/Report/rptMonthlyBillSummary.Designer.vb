<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptMonthlyBillSummary
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.chkExcludeShift = New System.Windows.Forms.CheckBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnCustomerWise = New common.Controls.MyRadioButton()
        Me.rbtnrouteWise = New common.Controls.MyRadioButton()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDateWise = New common.Controls.MyRadioButton()
        Me.rbtnDocumentdate = New common.Controls.MyRadioButton()
        Me.rbtnSupplydate = New common.Controls.MyRadioButton()
        Me.TxtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtMultiCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtfDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.btnprintDetail = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnCustomerWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnrouteWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.rbtnDateWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDocumentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSupplydate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprintDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprintDetail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Size = New System.Drawing.Size(901, 450)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(901, 413)
        Me.RadPageView1.TabIndex = 74
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.chkExcludeShift)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox5)
        Me.RadPageViewPage1.Controls.Add(Me.TxtRoute)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel10)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel13)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(880, 365)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'chkExcludeShift
        '
        Me.chkExcludeShift.AutoSize = True
        Me.chkExcludeShift.Location = New System.Drawing.Point(785, 17)
        Me.chkExcludeShift.Name = "chkExcludeShift"
        Me.chkExcludeShift.Size = New System.Drawing.Size(92, 17)
        Me.chkExcludeShift.TabIndex = 448
        Me.chkExcludeShift.Text = "Exclude Shift"
        Me.chkExcludeShift.UseVisualStyleBackColor = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnCustomerWise)
        Me.RadGroupBox1.Controls.Add(Me.rbtnrouteWise)
        Me.RadGroupBox1.HeaderText = "Report Type"
        Me.RadGroupBox1.Location = New System.Drawing.Point(557, 2)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(222, 43)
        Me.RadGroupBox1.TabIndex = 447
        Me.RadGroupBox1.Text = "Report Type"
        '
        'rbtnCustomerWise
        '
        Me.rbtnCustomerWise.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnCustomerWise.Location = New System.Drawing.Point(13, 17)
        Me.rbtnCustomerWise.MyLinkLable1 = Nothing
        Me.rbtnCustomerWise.MyLinkLable2 = Nothing
        Me.rbtnCustomerWise.Name = "rbtnCustomerWise"
        Me.rbtnCustomerWise.Size = New System.Drawing.Size(96, 18)
        Me.rbtnCustomerWise.TabIndex = 393
        Me.rbtnCustomerWise.Text = "Customer Wise"
        Me.rbtnCustomerWise.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnrouteWise
        '
        Me.rbtnrouteWise.Location = New System.Drawing.Point(132, 16)
        Me.rbtnrouteWise.MyLinkLable1 = Nothing
        Me.rbtnrouteWise.MyLinkLable2 = Nothing
        Me.rbtnrouteWise.Name = "rbtnrouteWise"
        Me.rbtnrouteWise.Size = New System.Drawing.Size(77, 18)
        Me.rbtnrouteWise.TabIndex = 393
        Me.rbtnrouteWise.TabStop = False
        Me.rbtnrouteWise.Text = "Route Wise"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.rbtnDateWise)
        Me.RadGroupBox5.Controls.Add(Me.rbtnDocumentdate)
        Me.RadGroupBox5.Controls.Add(Me.rbtnSupplydate)
        Me.RadGroupBox5.HeaderText = "Date Type"
        Me.RadGroupBox5.Location = New System.Drawing.Point(276, 3)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(275, 40)
        Me.RadGroupBox5.TabIndex = 446
        Me.RadGroupBox5.Text = "Date Type"
        '
        'rbtnDateWise
        '
        Me.rbtnDateWise.Location = New System.Drawing.Point(199, 15)
        Me.rbtnDateWise.MyLinkLable1 = Nothing
        Me.rbtnDateWise.MyLinkLable2 = Nothing
        Me.rbtnDateWise.Name = "rbtnDateWise"
        Me.rbtnDateWise.Size = New System.Drawing.Size(71, 18)
        Me.rbtnDateWise.TabIndex = 393
        Me.rbtnDateWise.TabStop = False
        Me.rbtnDateWise.Text = "Date Wise"
        '
        'rbtnDocumentdate
        '
        Me.rbtnDocumentdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnDocumentdate.Location = New System.Drawing.Point(9, 15)
        Me.rbtnDocumentdate.MyLinkLable1 = Nothing
        Me.rbtnDocumentdate.MyLinkLable2 = Nothing
        Me.rbtnDocumentdate.Name = "rbtnDocumentdate"
        Me.rbtnDocumentdate.Size = New System.Drawing.Size(99, 18)
        Me.rbtnDocumentdate.TabIndex = 393
        Me.rbtnDocumentdate.Text = "Document Date"
        Me.rbtnDocumentdate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnSupplydate
        '
        Me.rbtnSupplydate.Location = New System.Drawing.Point(111, 15)
        Me.rbtnSupplydate.MyLinkLable1 = Nothing
        Me.rbtnSupplydate.MyLinkLable2 = Nothing
        Me.rbtnSupplydate.Name = "rbtnSupplydate"
        Me.rbtnSupplydate.Size = New System.Drawing.Size(81, 18)
        Me.rbtnSupplydate.TabIndex = 393
        Me.rbtnSupplydate.TabStop = False
        Me.rbtnSupplydate.Text = "Supply Date"
        '
        'TxtRoute
        '
        Me.TxtRoute.arrDispalyMember = Nothing
        Me.TxtRoute.arrValueMember = Nothing
        Me.TxtRoute.Location = New System.Drawing.Point(73, 74)
        Me.TxtRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoute.MyLinkLable1 = Me.MyLabel10
        Me.TxtRoute.MyLinkLable2 = Nothing
        Me.TxtRoute.MyNullText = "All"
        Me.TxtRoute.Name = "TxtRoute"
        Me.TxtRoute.Size = New System.Drawing.Size(299, 19)
        Me.TxtRoute.TabIndex = 443
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(13, 75)
        Me.MyLabel10.Margin = New System.Windows.Forms.Padding(4)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(36, 18)
        Me.MyLabel10.TabIndex = 442
        Me.MyLabel10.Text = "Route"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(12, 49)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel13.TabIndex = 334
        Me.MyLabel13.Text = "Customer"
        '
        'txtMultiCustomer
        '
        Me.txtMultiCustomer.arrDispalyMember = Nothing
        Me.txtMultiCustomer.arrValueMember = Nothing
        Me.txtMultiCustomer.Location = New System.Drawing.Point(73, 50)
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
        Me.RadGroupBox4.Location = New System.Drawing.Point(16, 3)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(254, 40)
        Me.RadGroupBox4.TabIndex = 53
        Me.RadGroupBox4.Text = "Date Range"
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Location = New System.Drawing.Point(135, 16)
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
        Me.txtToDate.Location = New System.Drawing.Point(159, 15)
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
        'btnprintDetail
        '
        Me.btnprintDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprintDetail.Location = New System.Drawing.Point(111, 7)
        Me.btnprintDetail.Name = "btnprintDetail"
        Me.btnprintDetail.Size = New System.Drawing.Size(80, 18)
        Me.btnprintDetail.TabIndex = 166
        Me.btnprintDetail.Text = "Detail Print"
        Me.btnprintDetail.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(822, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 168
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(194, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 166
        Me.btnReset.Text = "Reset"
        Me.btnReset.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(12, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(96, 18)
        Me.btnPrint.TabIndex = 165
        Me.btnPrint.Text = "Print"
        '
        'rptMonthlyBillSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptMonthlyBillSummary"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptMonthlyBillSummary"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnCustomerWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnrouteWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.rbtnDateWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDocumentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSupplydate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtfDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprintDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents rbtnDocumentdate As common.Controls.MyRadioButton
    Friend WithEvents rbtnSupplydate As common.Controls.MyRadioButton
    Friend WithEvents TxtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtMultiCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadGroupBox4 As RadGroupBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtToDate As RadDateTimePicker
    Friend WithEvents txtfDate As RadDateTimePicker
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbtnCustomerWise As common.Controls.MyRadioButton
    Friend WithEvents rbtnrouteWise As common.Controls.MyRadioButton
    Friend WithEvents btnprintDetail As RadButton
    Friend WithEvents chkExcludeShift As CheckBox
    Friend WithEvents rbtnDateWise As common.Controls.MyRadioButton
End Class
