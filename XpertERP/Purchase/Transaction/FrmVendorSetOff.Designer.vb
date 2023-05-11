<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVendorSetOff
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
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.txtMulVendor = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.txtVendorGroup = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lbldocinvoicedate = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblreceiptamount = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lbldocumentdate = New common.Controls.MyLabel()
        Me.lblDocumentType = New common.Controls.MyLabel()
        Me.lblBalAmt = New common.Controls.MyLabel()
        Me.lblDocumentNo = New common.Controls.MyLabel()
        Me.txtDocumentNo = New common.UserControls.txtFinder()
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.dgvReceipt = New common.UserControls.MyRadGridView()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldocinvoicedate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblreceiptamount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldocumentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Attachments.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.dgvReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvReceipt.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(606, 205)
        Me.SplitContainer1.SplitterDistance = 176
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(606, 176)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblToDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtMulVendor)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocation)
        Me.RadPageViewPage1.Controls.Add(Me.lblfromDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtVendorGroup)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel5)
        Me.RadPageViewPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(91.0!, 26.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(585, 130)
        Me.RadPageViewPage1.Text = "Vendor Set Off"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(229, 8)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(87, 20)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(179, 9)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(45, 18)
        Me.lblToDate.TabIndex = 2
        Me.lblToDate.Text = "To Date"
        '
        'txtMulVendor
        '
        Me.txtMulVendor.arrDispalyMember = Nothing
        Me.txtMulVendor.arrValueMember = Nothing
        Me.txtMulVendor.Location = New System.Drawing.Point(85, 60)
        Me.txtMulVendor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMulVendor.MyLinkLable1 = Me.lblLocation
        Me.txtMulVendor.MyLinkLable2 = Nothing
        Me.txtMulVendor.MyNullText = "Select..."
        Me.txtMulVendor.Name = "txtMulVendor"
        Me.txtMulVendor.Size = New System.Drawing.Size(494, 22)
        Me.txtMulVendor.TabIndex = 387
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(5, 35)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(77, 18)
        Me.lblLocation.TabIndex = 386
        Me.lblLocation.Text = "Vendor Group"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(86, 8)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(87, 20)
        Me.txtFromDate.TabIndex = 1
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(5, 9)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(59, 18)
        Me.lblfromDate.TabIndex = 0
        Me.lblfromDate.Text = "From Date"
        '
        'txtVendorGroup
        '
        Me.txtVendorGroup.arrDispalyMember = Nothing
        Me.txtVendorGroup.arrValueMember = Nothing
        Me.txtVendorGroup.Location = New System.Drawing.Point(86, 34)
        Me.txtVendorGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorGroup.MyLinkLable1 = Me.lblLocation
        Me.txtVendorGroup.MyLinkLable2 = Nothing
        Me.txtVendorGroup.MyNullText = "All"
        Me.txtVendorGroup.Name = "txtVendorGroup"
        Me.txtVendorGroup.Size = New System.Drawing.Size(493, 20)
        Me.txtVendorGroup.TabIndex = 385
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(5, 63)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(43, 16)
        Me.MyLabel5.TabIndex = 0
        Me.MyLabel5.Text = "Vendor"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage2.Controls.Add(Me.lbldocinvoicedate)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage2.Controls.Add(Me.lblreceiptamount)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel4)
        Me.RadPageViewPage2.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage2.Controls.Add(Me.lbldocumentdate)
        Me.RadPageViewPage2.Controls.Add(Me.lblDocumentType)
        Me.RadPageViewPage2.Controls.Add(Me.lblBalAmt)
        Me.RadPageViewPage2.Controls.Add(Me.lblDocumentNo)
        Me.RadPageViewPage2.Controls.Add(Me.txtDocumentNo)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(149.0!, 26.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 35)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(585, 130)
        Me.RadPageViewPage2.Text = "On Account/Advance Doc."
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(15, 108)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel1.TabIndex = 28
        Me.MyLabel1.Text = "Invoice Date"
        '
        'lbldocinvoicedate
        '
        Me.lbldocinvoicedate.AutoSize = False
        Me.lbldocinvoicedate.BorderVisible = True
        Me.lbldocinvoicedate.FieldName = Nothing
        Me.lbldocinvoicedate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldocinvoicedate.Location = New System.Drawing.Point(105, 106)
        Me.lbldocinvoicedate.Name = "lbldocinvoicedate"
        Me.lbldocinvoicedate.Size = New System.Drawing.Size(181, 18)
        Me.lbldocinvoicedate.TabIndex = 27
        Me.lbldocinvoicedate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(15, 84)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(93, 16)
        Me.MyLabel2.TabIndex = 26
        Me.MyLabel2.Text = "Payment Amount"
        '
        'lblreceiptamount
        '
        Me.lblreceiptamount.AutoSize = False
        Me.lblreceiptamount.BorderVisible = True
        Me.lblreceiptamount.FieldName = Nothing
        Me.lblreceiptamount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreceiptamount.Location = New System.Drawing.Point(104, 84)
        Me.lblreceiptamount.Name = "lblreceiptamount"
        Me.lblreceiptamount.Size = New System.Drawing.Size(181, 18)
        Me.lblreceiptamount.TabIndex = 26
        Me.lblreceiptamount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(15, 62)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel4.TabIndex = 25
        Me.MyLabel4.Text = "Document Date"
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(15, 36)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel3.TabIndex = 22
        Me.MyLabel3.Text = "Document Type"
        '
        'lbldocumentdate
        '
        Me.lbldocumentdate.AutoSize = False
        Me.lbldocumentdate.BorderVisible = True
        Me.lbldocumentdate.FieldName = Nothing
        Me.lbldocumentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldocumentdate.Location = New System.Drawing.Point(104, 60)
        Me.lbldocumentdate.Name = "lbldocumentdate"
        Me.lbldocumentdate.Size = New System.Drawing.Size(181, 18)
        Me.lbldocumentdate.TabIndex = 24
        Me.lbldocumentdate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocumentType
        '
        Me.lblDocumentType.AutoSize = False
        Me.lblDocumentType.BorderVisible = True
        Me.lblDocumentType.FieldName = Nothing
        Me.lblDocumentType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentType.Location = New System.Drawing.Point(104, 36)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(181, 18)
        Me.lblDocumentType.TabIndex = 24
        Me.lblDocumentType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBalAmt
        '
        Me.lblBalAmt.AutoSize = False
        Me.lblBalAmt.BorderVisible = True
        Me.lblBalAmt.FieldName = Nothing
        Me.lblBalAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalAmt.Location = New System.Drawing.Point(329, 10)
        Me.lblBalAmt.Name = "lblBalAmt"
        Me.lblBalAmt.Size = New System.Drawing.Size(181, 18)
        Me.lblBalAmt.TabIndex = 23
        Me.lblBalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.FieldName = Nothing
        Me.lblDocumentNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentNo.Location = New System.Drawing.Point(15, 12)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(75, 16)
        Me.lblDocumentNo.TabIndex = 21
        Me.lblDocumentNo.Text = "Document No"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.CalculationExpression = Nothing
        Me.txtDocumentNo.FieldCode = Nothing
        Me.txtDocumentNo.FieldDesc = Nothing
        Me.txtDocumentNo.FieldMaxLength = 0
        Me.txtDocumentNo.FieldName = Nothing
        Me.txtDocumentNo.isCalculatedField = False
        Me.txtDocumentNo.IsSourceFromTable = False
        Me.txtDocumentNo.IsSourceFromValueList = False
        Me.txtDocumentNo.IsUnique = False
        Me.txtDocumentNo.Location = New System.Drawing.Point(104, 10)
        Me.txtDocumentNo.MendatroryField = True
        Me.txtDocumentNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentNo.MyLinkLable1 = Nothing
        Me.txtDocumentNo.MyLinkLable2 = Nothing
        Me.txtDocumentNo.MyReadOnly = False
        Me.txtDocumentNo.MyShowMasterFormButton = False
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.ReferenceFieldDesc = Nothing
        Me.txtDocumentNo.ReferenceFieldName = Nothing
        Me.txtDocumentNo.ReferenceTableName = Nothing
        Me.txtDocumentNo.Size = New System.Drawing.Size(220, 19)
        Me.txtDocumentNo.TabIndex = 22
        Me.txtDocumentNo.Value = ""
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.RadGroupBox3)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(139.0!, 26.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 35)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(585, 130)
        Me.Attachments.Text = "Invoice Document Detail"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.dgvReceipt)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox3.HeaderText = "Invoice Document Details"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(585, 130)
        Me.RadGroupBox3.TabIndex = 8
        Me.RadGroupBox3.Text = "Invoice Document Details"
        '
        'dgvReceipt
        '
        Me.dgvReceipt.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvReceipt.Cursor = System.Windows.Forms.Cursors.Default
        Me.dgvReceipt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvReceipt.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.dgvReceipt.ForeColor = System.Drawing.Color.Black
        Me.dgvReceipt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgvReceipt.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.dgvReceipt.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.dgvReceipt.MasterTemplate.EnableFiltering = True
        Me.dgvReceipt.MasterTemplate.ShowHeaderCellButtons = True
        Me.dgvReceipt.Name = "dgvReceipt"
        Me.dgvReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.dgvReceipt.ShowGroupPanel = False
        Me.dgvReceipt.ShowHeaderCellButtons = True
        Me.dgvReceipt.Size = New System.Drawing.Size(565, 100)
        Me.dgvReceipt.TabIndex = 3
        Me.dgvReceipt.TabStop = False
        Me.dgvReceipt.Text = "RadGridView1"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(94, 2)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(88, 20)
        Me.btnReset.TabIndex = 5
        Me.btnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(530, 2)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(4, 2)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(88, 20)
        Me.btnsave.TabIndex = 3
        Me.btnsave.Text = "Proceed"
        '
        'FrmVendorSetOff
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 205)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmVendorSetOff"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Vendor Set Off"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        Me.RadPageViewPage2.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldocinvoicedate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblreceiptamount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldocumentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocumentNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Attachments.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.dgvReceipt.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lbldocinvoicedate As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblreceiptamount As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lbldocumentdate As common.Controls.MyLabel
    Friend WithEvents lblDocumentType As common.Controls.MyLabel
    Friend WithEvents lblBalAmt As common.Controls.MyLabel
    Friend WithEvents lblDocumentNo As common.Controls.MyLabel
    Friend WithEvents txtDocumentNo As common.UserControls.txtFinder
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dgvReceipt As common.UserControls.MyRadGridView
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtVendorGroup As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMulVendor As common.UserControls.txtMultiSelectFinder
End Class
