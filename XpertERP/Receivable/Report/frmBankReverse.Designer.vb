<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBankReverse
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.rdobtnReceipts = New common.Controls.MyRadioButton()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.rdobtnPayments = New common.Controls.MyRadioButton()
        Me.dtpFrmDate = New common.Controls.MyDateTimePicker()
        Me.rdobtnAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgBankCode = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkBankSelect = New common.Controls.MyRadioButton()
        Me.chkbankAll = New common.Controls.MyRadioButton()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReferesh = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnReceipts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkBankSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkbankAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReferesh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 20)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(528, 481)
        Me.RadGroupBox1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(10, 20)
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReferesh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(508, 451)
        Me.SplitContainer1.SplitterDistance = 413
        Me.SplitContainer1.TabIndex = 28
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
        Me.RadPageView1.RootElement.Text = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(508, 413)
        Me.RadPageView1.TabIndex = 1
        Me.RadPageView1.TabStop = False
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(487, 365)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox2.Controls.Add(Me.rdobtnReceipts)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox2.Controls.Add(Me.rdobtnPayments)
        Me.RadGroupBox2.Controls.Add(Me.dtpFrmDate)
        Me.RadGroupBox2.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox2.Controls.Add(Me.rdobtnAll)
        Me.RadGroupBox2.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(394, 352)
        Me.RadGroupBox2.TabIndex = 27
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(280, 0)
        Me.dtpToDate.MendatroryField = False
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.RadLabel2
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpToDate.TabIndex = 24
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "13-06-2011"
        Me.dtpToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(229, 0)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 25
        Me.RadLabel2.Text = "To Date"
        '
        'rdobtnReceipts
        '
        Me.rdobtnReceipts.Location = New System.Drawing.Point(236, 41)
        Me.rdobtnReceipts.MyLinkLable1 = Nothing
        Me.rdobtnReceipts.MyLinkLable2 = Nothing
        Me.rdobtnReceipts.Name = "rdobtnReceipts"
        Me.rdobtnReceipts.Size = New System.Drawing.Size(62, 18)
        Me.rdobtnReceipts.TabIndex = 1
        Me.rdobtnReceipts.Text = "Receipts"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(13, 2)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 26
        Me.RadLabel1.Text = "From Date"
        '
        'rdobtnPayments
        '
        Me.rdobtnPayments.Location = New System.Drawing.Point(140, 41)
        Me.rdobtnPayments.MyLinkLable1 = Nothing
        Me.rdobtnPayments.MyLinkLable2 = Nothing
        Me.rdobtnPayments.Name = "rdobtnPayments"
        Me.rdobtnPayments.Size = New System.Drawing.Size(69, 18)
        Me.rdobtnPayments.TabIndex = 1
        Me.rdobtnPayments.Text = "Payments"
        '
        'dtpFrmDate
        '
        Me.dtpFrmDate.CalculationExpression = Nothing
        Me.dtpFrmDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrmDate.FieldCode = Nothing
        Me.dtpFrmDate.FieldDesc = Nothing
        Me.dtpFrmDate.FieldMaxLength = 0
        Me.dtpFrmDate.FieldName = Nothing
        Me.dtpFrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrmDate.isCalculatedField = False
        Me.dtpFrmDate.IsSourceFromTable = False
        Me.dtpFrmDate.IsSourceFromValueList = False
        Me.dtpFrmDate.IsUnique = False
        Me.dtpFrmDate.Location = New System.Drawing.Point(78, 0)
        Me.dtpFrmDate.MendatroryField = False
        Me.dtpFrmDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.MyLinkLable1 = Me.RadLabel1
        Me.dtpFrmDate.MyLinkLable2 = Nothing
        Me.dtpFrmDate.Name = "dtpFrmDate"
        Me.dtpFrmDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFrmDate.ReferenceFieldDesc = Nothing
        Me.dtpFrmDate.ReferenceFieldName = Nothing
        Me.dtpFrmDate.ReferenceTableName = Nothing
        Me.dtpFrmDate.Size = New System.Drawing.Size(82, 20)
        Me.dtpFrmDate.TabIndex = 23
        Me.dtpFrmDate.TabStop = False
        Me.dtpFrmDate.Text = "13-06-2011"
        Me.dtpFrmDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'rdobtnAll
        '
        Me.rdobtnAll.Location = New System.Drawing.Point(74, 41)
        Me.rdobtnAll.MyLinkLable1 = Nothing
        Me.rdobtnAll.MyLinkLable2 = Nothing
        Me.rdobtnAll.Name = "rdobtnAll"
        Me.rdobtnAll.Size = New System.Drawing.Size(33, 18)
        Me.rdobtnAll.TabIndex = 0
        Me.rdobtnAll.Text = "All"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgBankCode)
        Me.RadGroupBox4.Controls.Add(Me.Panel1)
        Me.RadGroupBox4.HeaderText = "Bank"
        Me.RadGroupBox4.Location = New System.Drawing.Point(13, 65)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(373, 259)
        Me.RadGroupBox4.TabIndex = 25
        Me.RadGroupBox4.Text = "Bank"
        '
        'cbgBankCode
        '
        Me.cbgBankCode.CheckedValue = Nothing
        Me.cbgBankCode.DataSource = Nothing
        Me.cbgBankCode.DisplayMember = "Name"
        Me.cbgBankCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgBankCode.Location = New System.Drawing.Point(10, 45)
        Me.cbgBankCode.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgBankCode.MyShowHeadrText = False
        Me.cbgBankCode.Name = "cbgBankCode"
        Me.cbgBankCode.Size = New System.Drawing.Size(353, 204)
        Me.cbgBankCode.TabIndex = 1
        Me.cbgBankCode.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkBankSelect)
        Me.Panel1.Controls.Add(Me.chkbankAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(353, 25)
        Me.Panel1.TabIndex = 0
        '
        'chkBankSelect
        '
        Me.chkBankSelect.Location = New System.Drawing.Point(180, 3)
        Me.chkBankSelect.MyLinkLable1 = Nothing
        Me.chkBankSelect.MyLinkLable2 = Nothing
        Me.chkBankSelect.Name = "chkBankSelect"
        Me.chkBankSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkBankSelect.TabIndex = 1
        Me.chkBankSelect.Text = "Select"
        '
        'chkbankAll
        '
        Me.chkbankAll.Location = New System.Drawing.Point(97, 3)
        Me.chkbankAll.MyLinkLable1 = Nothing
        Me.chkbankAll.MyLinkLable2 = Nothing
        Me.chkbankAll.Name = "chkbankAll"
        Me.chkbankAll.Size = New System.Drawing.Size(33, 18)
        Me.chkbankAll.TabIndex = 0
        Me.chkbankAll.Text = "All"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(487, 385)
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
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(487, 385)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.btnExport.Location = New System.Drawing.Point(217, 7)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 18)
        Me.btnExport.TabIndex = 331
        Me.btnExport.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.AccessibleDescription = "Excel"
        Me.rmExcel.AccessibleName = "Excel"
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'rmPDF
        '
        Me.rmPDF.AccessibleDescription = "PDF"
        Me.rmPDF.AccessibleName = "PDF"
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'btnReferesh
        '
        Me.btnReferesh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReferesh.Location = New System.Drawing.Point(3, 7)
        Me.btnReferesh.Name = "btnReferesh"
        Me.btnReferesh.Size = New System.Drawing.Size(66, 19)
        Me.btnReferesh.TabIndex = 29
        Me.btnReferesh.Text = ">>>"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(145, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 19)
        Me.btnPrint.TabIndex = 26
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(73, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(66, 19)
        Me.btnReset.TabIndex = 28
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(430, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 19)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(528, 20)
        Me.RadMenu1.TabIndex = 16
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'FrmBankReverse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 501)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmBankReverse"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bank Reverse"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnReceipts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFrmDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkBankSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkbankAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReferesh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpFrmDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgBankCode As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkBankSelect As common.Controls.MyRadioButton
    Friend WithEvents chkbankAll As common.Controls.MyRadioButton
    Friend WithEvents rdobtnAll As common.Controls.MyRadioButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdobtnReceipts As common.Controls.MyRadioButton
    Friend WithEvents rdobtnPayments As common.Controls.MyRadioButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnReferesh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
End Class

