<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptBookingVsDispatchReport
    ' Inherits System.Windows.Forms.Form
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
        Dim TableViewDefinition6 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnBatchDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnDocumentDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnDocument = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnBooking = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblItem = New common.Controls.MyLabel()
        Me.txtMultiItem = New common.UserControls.txtMultiSelectFinder()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.lblCustomer = New common.Controls.MyLabel()
        Me.txtMultiRoute = New common.UserControls.txtMultiSelectFinder()
        Me.txtMultiCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtToDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.txtFromDate = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.rbtnBatchDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDocumentDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnBooking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 409
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(800, 409)
        Me.RadPageView1.TabIndex = 11
        Me.RadPageView1.TabStop = False
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.lblItem)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiItem)
        Me.RadPageViewPage1.Controls.Add(Me.lblRoute)
        Me.RadPageViewPage1.Controls.Add(Me.lblCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiRoute)
        Me.RadPageViewPage1.Controls.Add(Me.txtMultiCustomer)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(779, 361)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rbtnBatchDetail)
        Me.RadGroupBox2.Controls.Add(Me.rbtnDocumentDetail)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(505, 21)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(222, 35)
        Me.RadGroupBox2.TabIndex = 424
        '
        'rbtnBatchDetail
        '
        Me.rbtnBatchDetail.Location = New System.Drawing.Point(128, 10)
        Me.rbtnBatchDetail.Name = "rbtnBatchDetail"
        Me.rbtnBatchDetail.Size = New System.Drawing.Size(80, 18)
        Me.rbtnBatchDetail.TabIndex = 312
        Me.rbtnBatchDetail.TabStop = False
        Me.rbtnBatchDetail.Text = "Batch Detail"
        '
        'rbtnDocumentDetail
        '
        Me.rbtnDocumentDetail.Location = New System.Drawing.Point(8, 10)
        Me.rbtnDocumentDetail.Name = "rbtnDocumentDetail"
        Me.rbtnDocumentDetail.Size = New System.Drawing.Size(104, 18)
        Me.rbtnDocumentDetail.TabIndex = 311
        Me.rbtnDocumentDetail.TabStop = False
        Me.rbtnDocumentDetail.Text = "Document Detail"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnDocument)
        Me.RadGroupBox1.Controls.Add(Me.rbtnBooking)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(342, 21)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(157, 35)
        Me.RadGroupBox1.TabIndex = 423
        '
        'rbtnDocument
        '
        Me.rbtnDocument.Location = New System.Drawing.Point(8, 10)
        Me.rbtnDocument.Name = "rbtnDocument"
        Me.rbtnDocument.Size = New System.Drawing.Size(63, 18)
        Me.rbtnDocument.TabIndex = 311
        Me.rbtnDocument.TabStop = False
        Me.rbtnDocument.Text = "Demand"
        '
        'rbtnBooking
        '
        Me.rbtnBooking.Location = New System.Drawing.Point(82, 10)
        Me.rbtnBooking.Name = "rbtnBooking"
        Me.rbtnBooking.Size = New System.Drawing.Size(61, 18)
        Me.rbtnBooking.TabIndex = 310
        Me.rbtnBooking.TabStop = False
        Me.rbtnBooking.Text = "Booking"
        '
        'lblItem
        '
        Me.lblItem.FieldName = Nothing
        Me.lblItem.Location = New System.Drawing.Point(22, 114)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(29, 18)
        Me.lblItem.TabIndex = 420
        Me.lblItem.Text = "Item"
        '
        'txtMultiItem
        '
        Me.txtMultiItem.arrDispalyMember = Nothing
        Me.txtMultiItem.arrValueMember = Nothing
        Me.txtMultiItem.Location = New System.Drawing.Point(95, 113)
        Me.txtMultiItem.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiItem.MyLinkLable1 = Nothing
        Me.txtMultiItem.MyLinkLable2 = Nothing
        Me.txtMultiItem.MyNullText = "All"
        Me.txtMultiItem.Name = "txtMultiItem"
        Me.txtMultiItem.Size = New System.Drawing.Size(382, 19)
        Me.txtMultiItem.TabIndex = 419
        '
        'lblRoute
        '
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Location = New System.Drawing.Point(22, 66)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(36, 18)
        Me.lblRoute.TabIndex = 418
        Me.lblRoute.Text = "Route"
        '
        'lblCustomer
        '
        Me.lblCustomer.FieldName = Nothing
        Me.lblCustomer.Location = New System.Drawing.Point(22, 90)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(55, 18)
        Me.lblCustomer.TabIndex = 417
        Me.lblCustomer.Text = "Customer"
        '
        'txtMultiRoute
        '
        Me.txtMultiRoute.arrDispalyMember = Nothing
        Me.txtMultiRoute.arrValueMember = Nothing
        Me.txtMultiRoute.Location = New System.Drawing.Point(95, 63)
        Me.txtMultiRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiRoute.MyLinkLable1 = Nothing
        Me.txtMultiRoute.MyLinkLable2 = Nothing
        Me.txtMultiRoute.MyNullText = "All"
        Me.txtMultiRoute.Name = "txtMultiRoute"
        Me.txtMultiRoute.Size = New System.Drawing.Size(382, 19)
        Me.txtMultiRoute.TabIndex = 416
        '
        'txtMultiCustomer
        '
        Me.txtMultiCustomer.arrDispalyMember = Nothing
        Me.txtMultiCustomer.arrValueMember = Nothing
        Me.txtMultiCustomer.Location = New System.Drawing.Point(95, 88)
        Me.txtMultiCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiCustomer.MyLinkLable1 = Nothing
        Me.txtMultiCustomer.MyLinkLable2 = Nothing
        Me.txtMultiCustomer.MyNullText = "All"
        Me.txtMultiCustomer.Name = "txtMultiCustomer"
        Me.txtMultiCustomer.Size = New System.Drawing.Size(382, 19)
        Me.txtMultiCustomer.TabIndex = 415
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox3.Controls.Add(Me.txtToDate)
        Me.RadGroupBox3.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox3.HeaderText = "Date Range"
        Me.RadGroupBox3.Location = New System.Drawing.Point(95, 16)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(241, 42)
        Me.RadGroupBox3.TabIndex = 55
        Me.RadGroupBox3.Text = "Date Range"
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(128, 16)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(19, 18)
        Me.RadLabel2.TabIndex = 3
        Me.RadLabel2.Text = "To"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(5, 16)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(32, 18)
        Me.MyLabel1.TabIndex = 2
        Me.MyLabel1.Text = "From"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(151, 15)
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Size = New System.Drawing.Size(78, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "24/10/2011"
        Me.txtToDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(44, 15)
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Size = New System.Drawing.Size(78, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "24/10/2011"
        Me.txtFromDate.Value = New Date(2011, 10, 24, 2, 29, 0, 265)
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(22, 40)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(30, 18)
        Me.RadLabel1.TabIndex = 24
        Me.RadLabel1.Text = "Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(779, 361)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition6
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(779, 361)
        Me.gv1.TabIndex = 3
        Me.gv1.VarID = ""
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(10, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 165
        Me.btnGo.Text = ">>>"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(92, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(71, 22)
        Me.btnReset.TabIndex = 164
        Me.btnReset.Text = "Reset"
        '
        'rptBookingVsDispatchReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "rptBookingVsDispatchReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "rptBookingVsDispatchReport"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.rbtnBatchDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDocumentDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnBooking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents RadGroupBox3 As RadGroupBox
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtToDate As RadDateTimePicker
    Friend WithEvents txtFromDate As RadDateTimePicker
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents lblCustomer As common.Controls.MyLabel
    Friend WithEvents txtMultiRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMultiCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents txtMultiItem As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblItem As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbtnDocument As RadRadioButton
    Friend WithEvents rbtnBooking As RadRadioButton
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents rbtnDocumentDetail As RadRadioButton
    Friend WithEvents rbtnBatchDetail As RadRadioButton
End Class
