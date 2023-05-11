<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkPurchaseReturn
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndInvoiceNo = New common.UserControls.txtFinder()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.txtVendor = New common.Controls.MyTextBox()
        Me.txtLocation = New common.Controls.MyTextBox()
        Me.txtSRNNo = New common.Controls.MyTextBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.dtpDocReturnDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.fndDocNoReturn = New common.UserControls.txtNavigator()
        Me.lblMonth = New common.Controls.MyLabel()
        Me.txtMonth = New common.Controls.MyDateTimePicker()
        Me.chkSRNTrade = New common.Controls.MyCheckBox()
        Me.txtTotalAmt = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtVendorInvoiceNo = New common.Controls.MyTextBox()
        Me.lblVendorInvoiceNo = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.txtRoundOffAmt = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtTotalQty = New common.Controls.MyTextBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.txtTotalSNFKg = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtTotalFatKg = New common.Controls.MyTextBox()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblVendorName = New common.Controls.MyLabel()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.lblSRNDate = New common.Controls.MyLabel()
        Me.dtpDocDate = New common.Controls.MyDateTimePicker()
        Me.lblWeighmentNo = New common.Controls.MyLabel()
        Me.lblSRNNo = New common.Controls.MyLabel()
        Me.lblPending = New common.usLock()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.gvItem = New Telerik.WinControls.UI.MasterGridViewTemplate()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.chkJobWork = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtSubLocation = New common.UserControls.txtFinder()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocReturnDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSRNTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRoundOffAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalSNFKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalFatKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndInvoiceNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVendor)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSRNNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpDocReturnDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndDocNoReturn)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMonth)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMonth)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkSRNTrade)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTotalAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVendorInvoiceNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVendorInvoiceNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpToDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFromDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRoundOffAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTotalQty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTotalSNFKg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTotalFatKg)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblVendorName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSRNDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpDocDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblWeighmentNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSRNNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPending)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(1250, 487)
        Me.SplitContainer1.SplitterDistance = 444
        Me.SplitContainer1.TabIndex = 5
        '
        'fndInvoiceNo
        '
        Me.fndInvoiceNo.CalculationExpression = Nothing
        Me.fndInvoiceNo.FieldCode = Nothing
        Me.fndInvoiceNo.FieldDesc = Nothing
        Me.fndInvoiceNo.FieldMaxLength = 0
        Me.fndInvoiceNo.FieldName = Nothing
        Me.fndInvoiceNo.isCalculatedField = False
        Me.fndInvoiceNo.IsSourceFromTable = False
        Me.fndInvoiceNo.IsSourceFromValueList = False
        Me.fndInvoiceNo.IsUnique = False
        Me.fndInvoiceNo.Location = New System.Drawing.Point(102, 25)
        Me.fndInvoiceNo.MendatroryField = True
        Me.fndInvoiceNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndInvoiceNo.MyLinkLable1 = Me.MyLabel8
        Me.fndInvoiceNo.MyLinkLable2 = Nothing
        Me.fndInvoiceNo.MyReadOnly = False
        Me.fndInvoiceNo.MyShowMasterFormButton = False
        Me.fndInvoiceNo.Name = "fndInvoiceNo"
        Me.fndInvoiceNo.ReferenceFieldDesc = Nothing
        Me.fndInvoiceNo.ReferenceFieldName = Nothing
        Me.fndInvoiceNo.ReferenceTableName = Nothing
        Me.fndInvoiceNo.Size = New System.Drawing.Size(355, 19)
        Me.fndInvoiceNo.TabIndex = 433
        Me.fndInvoiceNo.Value = ""
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel8.Location = New System.Drawing.Point(6, 65)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel8.TabIndex = 415
        Me.MyLabel8.Text = "Location"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox1.Controls.Add(Me.gv)
        Me.RadGroupBox1.HeaderText = "Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 177)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1240, 264)
        Me.RadGroupBox1.TabIndex = 432
        Me.RadGroupBox1.Text = "Details"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(2, 18)
        '
        'gv
        '
        Me.gv.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1236, 244)
        Me.gv.TabIndex = 2
        Me.gv.TabStop = False
        Me.gv.Text = "RadGridView1"
        '
        'txtVendor
        '
        Me.txtVendor.CalculationExpression = Nothing
        Me.txtVendor.FieldCode = Nothing
        Me.txtVendor.FieldDesc = Nothing
        Me.txtVendor.FieldMaxLength = 0
        Me.txtVendor.FieldName = Nothing
        Me.txtVendor.isCalculatedField = False
        Me.txtVendor.IsSourceFromTable = False
        Me.txtVendor.IsSourceFromValueList = False
        Me.txtVendor.IsUnique = False
        Me.txtVendor.Location = New System.Drawing.Point(102, 44)
        Me.txtVendor.MaxLength = 50
        Me.txtVendor.MendatroryField = True
        Me.txtVendor.MyLinkLable1 = Nothing
        Me.txtVendor.MyLinkLable2 = Nothing
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.ReferenceFieldDesc = Nothing
        Me.txtVendor.ReferenceFieldName = Nothing
        Me.txtVendor.ReferenceTableName = Nothing
        Me.txtVendor.Size = New System.Drawing.Size(355, 20)
        Me.txtVendor.TabIndex = 431
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(102, 65)
        Me.txtLocation.MaxLength = 50
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(355, 20)
        Me.txtLocation.TabIndex = 430
        '
        'txtSRNNo
        '
        Me.txtSRNNo.CalculationExpression = Nothing
        Me.txtSRNNo.FieldCode = Nothing
        Me.txtSRNNo.FieldDesc = Nothing
        Me.txtSRNNo.FieldMaxLength = 0
        Me.txtSRNNo.FieldName = Nothing
        Me.txtSRNNo.isCalculatedField = False
        Me.txtSRNNo.IsSourceFromTable = False
        Me.txtSRNNo.IsSourceFromValueList = False
        Me.txtSRNNo.IsUnique = False
        Me.txtSRNNo.Location = New System.Drawing.Point(102, 106)
        Me.txtSRNNo.MaxLength = 50
        Me.txtSRNNo.MendatroryField = True
        Me.txtSRNNo.MyLinkLable1 = Nothing
        Me.txtSRNNo.MyLinkLable2 = Nothing
        Me.txtSRNNo.Name = "txtSRNNo"
        Me.txtSRNNo.ReadOnly = True
        Me.txtSRNNo.ReferenceFieldDesc = Nothing
        Me.txtSRNNo.ReferenceFieldName = Nothing
        Me.txtSRNNo.ReferenceTableName = Nothing
        Me.txtSRNNo.Size = New System.Drawing.Size(355, 20)
        Me.txtSRNNo.TabIndex = 429
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(476, 5)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel10.TabIndex = 428
        Me.MyLabel10.Text = "Doc Date"
        '
        'dtpDocReturnDate
        '
        Me.dtpDocReturnDate.CalculationExpression = Nothing
        Me.dtpDocReturnDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocReturnDate.FieldCode = Nothing
        Me.dtpDocReturnDate.FieldDesc = Nothing
        Me.dtpDocReturnDate.FieldMaxLength = 0
        Me.dtpDocReturnDate.FieldName = Nothing
        Me.dtpDocReturnDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocReturnDate.isCalculatedField = False
        Me.dtpDocReturnDate.IsSourceFromTable = False
        Me.dtpDocReturnDate.IsSourceFromValueList = False
        Me.dtpDocReturnDate.IsUnique = False
        Me.dtpDocReturnDate.Location = New System.Drawing.Point(560, 3)
        Me.dtpDocReturnDate.MendatroryField = True
        Me.dtpDocReturnDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocReturnDate.MyLinkLable1 = Me.MyLabel10
        Me.dtpDocReturnDate.MyLinkLable2 = Nothing
        Me.dtpDocReturnDate.Name = "dtpDocReturnDate"
        Me.dtpDocReturnDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocReturnDate.ReferenceFieldDesc = Nothing
        Me.dtpDocReturnDate.ReferenceFieldName = Nothing
        Me.dtpDocReturnDate.ReferenceTableName = Nothing
        Me.dtpDocReturnDate.Size = New System.Drawing.Size(94, 18)
        Me.dtpDocReturnDate.TabIndex = 427
        Me.dtpDocReturnDate.TabStop = False
        Me.dtpDocReturnDate.Text = "03/05/2011"
        Me.dtpDocReturnDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(6, 4)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(47, 16)
        Me.MyLabel9.TabIndex = 425
        Me.MyLabel9.Text = "Doc No."
        '
        'fndDocNoReturn
        '
        Me.fndDocNoReturn.FieldName = Nothing
        Me.fndDocNoReturn.Location = New System.Drawing.Point(102, 2)
        Me.fndDocNoReturn.MendatroryField = False
        Me.fndDocNoReturn.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndDocNoReturn.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNoReturn.MyLinkLable1 = Me.MyLabel9
        Me.fndDocNoReturn.MyLinkLable2 = Nothing
        Me.fndDocNoReturn.MyMaxLength = 50
        Me.fndDocNoReturn.MyReadOnly = False
        Me.fndDocNoReturn.Name = "fndDocNoReturn"
        Me.fndDocNoReturn.Size = New System.Drawing.Size(342, 21)
        Me.fndDocNoReturn.TabIndex = 424
        Me.fndDocNoReturn.Value = ""
        '
        'lblMonth
        '
        Me.lblMonth.FieldName = Nothing
        Me.lblMonth.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMonth.Location = New System.Drawing.Point(457, 88)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(38, 16)
        Me.lblMonth.TabIndex = 423
        Me.lblMonth.Text = "Month"
        Me.lblMonth.Visible = False
        '
        'txtMonth
        '
        Me.txtMonth.CalculationExpression = Nothing
        Me.txtMonth.CustomFormat = "MMM - yyyy"
        Me.txtMonth.FieldCode = Nothing
        Me.txtMonth.FieldDesc = Nothing
        Me.txtMonth.FieldMaxLength = 0
        Me.txtMonth.FieldName = Nothing
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtMonth.isCalculatedField = False
        Me.txtMonth.IsSourceFromTable = False
        Me.txtMonth.IsSourceFromValueList = False
        Me.txtMonth.IsUnique = False
        Me.txtMonth.Location = New System.Drawing.Point(552, 86)
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Me.lblMonth
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.ReadOnly = True
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.Size = New System.Drawing.Size(108, 18)
        Me.txtMonth.TabIndex = 422
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Sep - 2014"
        Me.txtMonth.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        Me.txtMonth.Visible = False
        '
        'chkSRNTrade
        '
        Me.chkSRNTrade.Location = New System.Drawing.Point(732, 25)
        Me.chkSRNTrade.MyLinkLable1 = Nothing
        Me.chkSRNTrade.MyLinkLable2 = Nothing
        Me.chkSRNTrade.Name = "chkSRNTrade"
        Me.chkSRNTrade.ReadOnly = True
        Me.chkSRNTrade.Size = New System.Drawing.Size(73, 18)
        Me.chkSRNTrade.TabIndex = 421
        Me.chkSRNTrade.Tag1 = Nothing
        Me.chkSRNTrade.Text = "SRN Trade"
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.CalculationExpression = Nothing
        Me.txtTotalAmt.FieldCode = Nothing
        Me.txtTotalAmt.FieldDesc = Nothing
        Me.txtTotalAmt.FieldMaxLength = 0
        Me.txtTotalAmt.FieldName = Nothing
        Me.txtTotalAmt.isCalculatedField = False
        Me.txtTotalAmt.IsSourceFromTable = False
        Me.txtTotalAmt.IsSourceFromValueList = False
        Me.txtTotalAmt.IsUnique = False
        Me.txtTotalAmt.Location = New System.Drawing.Point(552, 152)
        Me.txtTotalAmt.MaxLength = 50
        Me.txtTotalAmt.MendatroryField = True
        Me.txtTotalAmt.MyLinkLable1 = Nothing
        Me.txtTotalAmt.MyLinkLable2 = Nothing
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.ReadOnly = True
        Me.txtTotalAmt.ReferenceFieldDesc = Nothing
        Me.txtTotalAmt.ReferenceFieldName = Nothing
        Me.txtTotalAmt.ReferenceTableName = Nothing
        Me.txtTotalAmt.Size = New System.Drawing.Size(259, 20)
        Me.txtTotalAmt.TabIndex = 419
        Me.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Location = New System.Drawing.Point(459, 154)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel7.TabIndex = 420
        Me.MyLabel7.Text = "Total Amount"
        '
        'txtVendorInvoiceNo
        '
        Me.txtVendorInvoiceNo.CalculationExpression = Nothing
        Me.txtVendorInvoiceNo.FieldCode = Nothing
        Me.txtVendorInvoiceNo.FieldDesc = Nothing
        Me.txtVendorInvoiceNo.FieldMaxLength = 0
        Me.txtVendorInvoiceNo.FieldName = Nothing
        Me.txtVendorInvoiceNo.isCalculatedField = False
        Me.txtVendorInvoiceNo.IsSourceFromTable = False
        Me.txtVendorInvoiceNo.IsSourceFromValueList = False
        Me.txtVendorInvoiceNo.IsUnique = False
        Me.txtVendorInvoiceNo.Location = New System.Drawing.Point(552, 106)
        Me.txtVendorInvoiceNo.MaxLength = 50
        Me.txtVendorInvoiceNo.MendatroryField = True
        Me.txtVendorInvoiceNo.MyLinkLable1 = Nothing
        Me.txtVendorInvoiceNo.MyLinkLable2 = Nothing
        Me.txtVendorInvoiceNo.Name = "txtVendorInvoiceNo"
        Me.txtVendorInvoiceNo.ReadOnly = True
        Me.txtVendorInvoiceNo.ReferenceFieldDesc = Nothing
        Me.txtVendorInvoiceNo.ReferenceFieldName = Nothing
        Me.txtVendorInvoiceNo.ReferenceTableName = Nothing
        Me.txtVendorInvoiceNo.Size = New System.Drawing.Size(259, 20)
        Me.txtVendorInvoiceNo.TabIndex = 417
        '
        'lblVendorInvoiceNo
        '
        Me.lblVendorInvoiceNo.FieldName = Nothing
        Me.lblVendorInvoiceNo.Location = New System.Drawing.Point(457, 107)
        Me.lblVendorInvoiceNo.Name = "lblVendorInvoiceNo"
        Me.lblVendorInvoiceNo.Size = New System.Drawing.Size(100, 18)
        Me.lblVendorInvoiceNo.TabIndex = 418
        Me.lblVendorInvoiceNo.Text = "Vendor Invoice No"
        '
        'dtpToDate
        '
        Me.dtpToDate.CalculationExpression = Nothing
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpToDate.FieldCode = Nothing
        Me.dtpToDate.FieldDesc = Nothing
        Me.dtpToDate.FieldMaxLength = 0
        Me.dtpToDate.FieldName = Nothing
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.isCalculatedField = False
        Me.dtpToDate.IsSourceFromTable = False
        Me.dtpToDate.IsSourceFromValueList = False
        Me.dtpToDate.IsUnique = False
        Me.dtpToDate.Location = New System.Drawing.Point(317, 86)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel5
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.ReadOnly = True
        Me.dtpToDate.ReferenceFieldDesc = Nothing
        Me.dtpToDate.ReferenceFieldName = Nothing
        Me.dtpToDate.ReferenceTableName = Nothing
        Me.dtpToDate.Size = New System.Drawing.Size(140, 18)
        Me.dtpToDate.TabIndex = 397
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(269, 87)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel5.TabIndex = 413
        Me.MyLabel5.Text = "To Date"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 88)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel4.TabIndex = 412
        Me.MyLabel4.Text = "From Date"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(6, 108)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(27, 18)
        Me.MyLabel6.TabIndex = 414
        Me.MyLabel6.Text = "SRN"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalculationExpression = Nothing
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpFromDate.FieldCode = Nothing
        Me.dtpFromDate.FieldDesc = Nothing
        Me.dtpFromDate.FieldMaxLength = 0
        Me.dtpFromDate.FieldName = Nothing
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.isCalculatedField = False
        Me.dtpFromDate.IsSourceFromTable = False
        Me.dtpFromDate.IsSourceFromValueList = False
        Me.dtpFromDate.IsUnique = False
        Me.dtpFromDate.Location = New System.Drawing.Point(102, 86)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.MyLabel4
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.ReadOnly = True
        Me.dtpFromDate.ReferenceFieldDesc = Nothing
        Me.dtpFromDate.ReferenceFieldName = Nothing
        Me.dtpFromDate.ReferenceTableName = Nothing
        Me.dtpFromDate.Size = New System.Drawing.Size(145, 18)
        Me.dtpFromDate.TabIndex = 396
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtRoundOffAmt
        '
        Me.txtRoundOffAmt.CalculationExpression = Nothing
        Me.txtRoundOffAmt.FieldCode = Nothing
        Me.txtRoundOffAmt.FieldDesc = Nothing
        Me.txtRoundOffAmt.FieldMaxLength = 0
        Me.txtRoundOffAmt.FieldName = Nothing
        Me.txtRoundOffAmt.isCalculatedField = False
        Me.txtRoundOffAmt.IsSourceFromTable = False
        Me.txtRoundOffAmt.IsSourceFromValueList = False
        Me.txtRoundOffAmt.IsUnique = False
        Me.txtRoundOffAmt.Location = New System.Drawing.Point(317, 153)
        Me.txtRoundOffAmt.MaxLength = 50
        Me.txtRoundOffAmt.MendatroryField = True
        Me.txtRoundOffAmt.MyLinkLable1 = Nothing
        Me.txtRoundOffAmt.MyLinkLable2 = Nothing
        Me.txtRoundOffAmt.Name = "txtRoundOffAmt"
        Me.txtRoundOffAmt.ReferenceFieldDesc = Nothing
        Me.txtRoundOffAmt.ReferenceFieldName = Nothing
        Me.txtRoundOffAmt.ReferenceTableName = Nothing
        Me.txtRoundOffAmt.Size = New System.Drawing.Size(139, 20)
        Me.txtRoundOffAmt.TabIndex = 401
        Me.txtRoundOffAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(233, 156)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel2.TabIndex = 411
        Me.MyLabel2.Text = "Round Off Amt"
        '
        'txtTotalQty
        '
        Me.txtTotalQty.CalculationExpression = Nothing
        Me.txtTotalQty.FieldCode = Nothing
        Me.txtTotalQty.FieldDesc = Nothing
        Me.txtTotalQty.FieldMaxLength = 0
        Me.txtTotalQty.FieldName = Nothing
        Me.txtTotalQty.isCalculatedField = False
        Me.txtTotalQty.IsSourceFromTable = False
        Me.txtTotalQty.IsSourceFromValueList = False
        Me.txtTotalQty.IsUnique = False
        Me.txtTotalQty.Location = New System.Drawing.Point(102, 152)
        Me.txtTotalQty.MaxLength = 50
        Me.txtTotalQty.MendatroryField = True
        Me.txtTotalQty.MyLinkLable1 = Nothing
        Me.txtTotalQty.MyLinkLable2 = Nothing
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.ReferenceFieldDesc = Nothing
        Me.txtTotalQty.ReferenceFieldName = Nothing
        Me.txtTotalQty.ReferenceTableName = Nothing
        Me.txtTotalQty.Size = New System.Drawing.Size(125, 20)
        Me.txtTotalQty.TabIndex = 400
        Me.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(6, 153)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel3.TabIndex = 410
        Me.MyLabel3.Text = "Total QTY"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(457, 64)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(354, 21)
        Me.lblLocationName.TabIndex = 416
        Me.lblLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalSNFKg
        '
        Me.txtTotalSNFKg.CalculationExpression = Nothing
        Me.txtTotalSNFKg.FieldCode = Nothing
        Me.txtTotalSNFKg.FieldDesc = Nothing
        Me.txtTotalSNFKg.FieldMaxLength = 0
        Me.txtTotalSNFKg.FieldName = Nothing
        Me.txtTotalSNFKg.isCalculatedField = False
        Me.txtTotalSNFKg.IsSourceFromTable = False
        Me.txtTotalSNFKg.IsSourceFromValueList = False
        Me.txtTotalSNFKg.IsUnique = False
        Me.txtTotalSNFKg.Location = New System.Drawing.Point(552, 129)
        Me.txtTotalSNFKg.MaxLength = 50
        Me.txtTotalSNFKg.MendatroryField = True
        Me.txtTotalSNFKg.MyLinkLable1 = Nothing
        Me.txtTotalSNFKg.MyLinkLable2 = Nothing
        Me.txtTotalSNFKg.Name = "txtTotalSNFKg"
        Me.txtTotalSNFKg.ReadOnly = True
        Me.txtTotalSNFKg.ReferenceFieldDesc = Nothing
        Me.txtTotalSNFKg.ReferenceFieldName = Nothing
        Me.txtTotalSNFKg.ReferenceTableName = Nothing
        Me.txtTotalSNFKg.Size = New System.Drawing.Size(259, 20)
        Me.txtTotalSNFKg.TabIndex = 399
        Me.txtTotalSNFKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(459, 130)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(72, 18)
        Me.MyLabel1.TabIndex = 409
        Me.MyLabel1.Text = "Total SNF KG"
        '
        'txtTotalFatKg
        '
        Me.txtTotalFatKg.CalculationExpression = Nothing
        Me.txtTotalFatKg.FieldCode = Nothing
        Me.txtTotalFatKg.FieldDesc = Nothing
        Me.txtTotalFatKg.FieldMaxLength = 0
        Me.txtTotalFatKg.FieldName = Nothing
        Me.txtTotalFatKg.isCalculatedField = False
        Me.txtTotalFatKg.IsSourceFromTable = False
        Me.txtTotalFatKg.IsSourceFromValueList = False
        Me.txtTotalFatKg.IsUnique = False
        Me.txtTotalFatKg.Location = New System.Drawing.Point(102, 129)
        Me.txtTotalFatKg.MaxLength = 50
        Me.txtTotalFatKg.MendatroryField = True
        Me.txtTotalFatKg.MyLinkLable1 = Nothing
        Me.txtTotalFatKg.MyLinkLable2 = Nothing
        Me.txtTotalFatKg.Name = "txtTotalFatKg"
        Me.txtTotalFatKg.ReferenceFieldDesc = Nothing
        Me.txtTotalFatKg.ReferenceFieldName = Nothing
        Me.txtTotalFatKg.ReferenceTableName = Nothing
        Me.txtTotalFatKg.Size = New System.Drawing.Size(355, 20)
        Me.txtTotalFatKg.TabIndex = 398
        Me.txtTotalFatKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Location = New System.Drawing.Point(6, 130)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(70, 18)
        Me.lblLocation.TabIndex = 408
        Me.lblLocation.Text = "Total FAT KG"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = False
        Me.lblVendorName.BorderVisible = True
        Me.lblVendorName.FieldName = Nothing
        Me.lblVendorName.Location = New System.Drawing.Point(457, 43)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(354, 21)
        Me.lblVendorName.TabIndex = 407
        Me.lblVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnReset
        '
        Me.btnReset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnReset.Location = New System.Drawing.Point(444, 3)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(13, 20)
        Me.btnReset.TabIndex = 406
        '
        'lblSRNDate
        '
        Me.lblSRNDate.FieldName = Nothing
        Me.lblSRNDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSRNDate.Location = New System.Drawing.Point(478, 25)
        Me.lblSRNDate.Name = "lblSRNDate"
        Me.lblSRNDate.Size = New System.Drawing.Size(73, 16)
        Me.lblSRNDate.TabIndex = 405
        Me.lblSRNDate.Text = "Invoice Date"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CalculationExpression = Nothing
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocDate.FieldCode = Nothing
        Me.dtpDocDate.FieldDesc = Nothing
        Me.dtpDocDate.FieldMaxLength = 0
        Me.dtpDocDate.FieldName = Nothing
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.isCalculatedField = False
        Me.dtpDocDate.IsSourceFromTable = False
        Me.dtpDocDate.IsSourceFromValueList = False
        Me.dtpDocDate.IsUnique = False
        Me.dtpDocDate.Location = New System.Drawing.Point(561, 23)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblSRNDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.ReadOnly = True
        Me.dtpDocDate.ReferenceFieldDesc = Nothing
        Me.dtpDocDate.ReferenceFieldName = Nothing
        Me.dtpDocDate.ReferenceTableName = Nothing
        Me.dtpDocDate.Size = New System.Drawing.Size(94, 18)
        Me.dtpDocDate.TabIndex = 395
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblWeighmentNo
        '
        Me.lblWeighmentNo.FieldName = Nothing
        Me.lblWeighmentNo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblWeighmentNo.Location = New System.Drawing.Point(6, 44)
        Me.lblWeighmentNo.Name = "lblWeighmentNo"
        Me.lblWeighmentNo.Size = New System.Drawing.Size(43, 18)
        Me.lblWeighmentNo.TabIndex = 404
        Me.lblWeighmentNo.Text = "Vendor"
        '
        'lblSRNNo
        '
        Me.lblSRNNo.FieldName = Nothing
        Me.lblSRNNo.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSRNNo.Location = New System.Drawing.Point(6, 25)
        Me.lblSRNNo.Name = "lblSRNNo"
        Me.lblSRNNo.Size = New System.Drawing.Size(63, 16)
        Me.lblSRNNo.TabIndex = 402
        Me.lblSRNNo.Text = "Invoice No."
        '
        'lblPending
        '
        Me.lblPending.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblPending.Location = New System.Drawing.Point(707, 5)
        Me.lblPending.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(98, 20)
        Me.lblPending.Status = common.ERPTransactionStatus.Pending
        Me.lblPending.TabIndex = 403
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(218, 14)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(1178, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(148, 14)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(68, 18)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "Post"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(7, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(77, 14)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(68, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'gvItem
        '
        Me.gvItem.AllowColumnHeaderContextMenu = False
        Me.gvItem.AllowDeleteRow = False
        Me.gvItem.ShowHeaderCellButtons = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblSubLocation)
        Me.Panel3.Controls.Add(Me.chkJobWork)
        Me.Panel3.Controls.Add(Me.MyLabel16)
        Me.Panel3.Controls.Add(Me.txtSubLocation)
        Me.Panel3.Location = New System.Drawing.Point(817, 42)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(396, 44)
        Me.Panel3.TabIndex = 434
        Me.Panel3.Visible = False
        '
        'lblSubLocation
        '
        Me.lblSubLocation.AutoSize = False
        Me.lblSubLocation.BorderVisible = True
        Me.lblSubLocation.FieldName = Nothing
        Me.lblSubLocation.Location = New System.Drawing.Point(213, 21)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(180, 19)
        Me.lblSubLocation.TabIndex = 276
        Me.lblSubLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkJobWork
        '
        Me.chkJobWork.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJobWork.Location = New System.Drawing.Point(3, 4)
        Me.chkJobWork.Name = "chkJobWork"
        Me.chkJobWork.Size = New System.Drawing.Size(80, 16)
        Me.chkJobWork.TabIndex = 346
        Me.chkJobWork.Text = "Is Job Work"
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(3, 21)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel16.TabIndex = 274
        Me.MyLabel16.Text = "Sub Location"
        '
        'txtSubLocation
        '
        Me.txtSubLocation.CalculationExpression = Nothing
        Me.txtSubLocation.FieldCode = Nothing
        Me.txtSubLocation.FieldDesc = Nothing
        Me.txtSubLocation.FieldMaxLength = 0
        Me.txtSubLocation.FieldName = Nothing
        Me.txtSubLocation.isCalculatedField = False
        Me.txtSubLocation.IsSourceFromTable = False
        Me.txtSubLocation.IsSourceFromValueList = False
        Me.txtSubLocation.IsUnique = False
        Me.txtSubLocation.Location = New System.Drawing.Point(81, 21)
        Me.txtSubLocation.MendatroryField = True
        Me.txtSubLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubLocation.MyLinkLable1 = Nothing
        Me.txtSubLocation.MyLinkLable2 = Nothing
        Me.txtSubLocation.MyReadOnly = False
        Me.txtSubLocation.MyShowMasterFormButton = False
        Me.txtSubLocation.Name = "txtSubLocation"
        Me.txtSubLocation.ReferenceFieldDesc = Nothing
        Me.txtSubLocation.ReferenceFieldName = Nothing
        Me.txtSubLocation.ReferenceTableName = Nothing
        Me.txtSubLocation.Size = New System.Drawing.Size(124, 20)
        Me.txtSubLocation.TabIndex = 275
        Me.txtSubLocation.Value = ""
        '
        'FrmMilkPurchaseReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1250, 487)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkPurchaseReturn"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bulk Milk Purchase Return"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocReturnDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSRNTrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVendorInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRoundOffAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalSNFKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalFatKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWeighmentNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkJobWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvItem As Telerik.WinControls.UI.MasterGridViewTemplate
    Friend WithEvents txtVendor As common.Controls.MyTextBox
    Friend WithEvents txtLocation As common.Controls.MyTextBox
    Friend WithEvents txtSRNNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents dtpDocReturnDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents fndDocNoReturn As common.UserControls.txtNavigator
    Friend WithEvents lblMonth As common.Controls.MyLabel
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents chkSRNTrade As common.Controls.MyCheckBox
    Friend WithEvents txtTotalAmt As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtVendorInvoiceNo As common.Controls.MyTextBox
    Friend WithEvents lblVendorInvoiceNo As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtRoundOffAmt As common.Controls.MyTextBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtTotalQty As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents txtTotalSNFKg As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtTotalFatKg As common.Controls.MyTextBox
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblVendorName As common.Controls.MyLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblSRNDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblWeighmentNo As common.Controls.MyLabel
    Friend WithEvents lblSRNNo As common.Controls.MyLabel
    Friend WithEvents lblPending As common.usLock
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndInvoiceNo As common.UserControls.txtFinder
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents chkJobWork As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtSubLocation As common.UserControls.txtFinder
End Class

