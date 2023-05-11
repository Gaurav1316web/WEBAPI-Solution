<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerAdvanceAgainstBooking
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.FndPaymentMode = New common.UserControls.txtFinder()
        Me.chkread = New common.Controls.MyCheckBox()
        Me.txtCustomerNo = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblpaymentdate = New common.Controls.MyLabel()
        Me.txtReceiptDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.fndBankCode = New common.UserControls.txtFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.btnSelectSheet = New Telerik.WinControls.UI.RadButton()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.Gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnExport = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSaveAndPost = New Telerik.WinControls.UI.RadButton()
        Me.lblTotalAmount = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkread, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReceiptDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblTotalAmount)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveAndPost)
        Me.SplitContainer1.Size = New System.Drawing.Size(739, 472)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.FndPaymentMode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkread)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCustomerNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblpaymentdate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtReceiptDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndBankCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnSelectSheet)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtToDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFromDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(739, 435)
        Me.SplitContainer2.SplitterDistance = 148
        Me.SplitContainer2.TabIndex = 3
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(229, 98)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel3.TabIndex = 1470
        Me.MyLabel3.Text = "Payment Mode"
        '
        'FndPaymentMode
        '
        Me.FndPaymentMode.CalculationExpression = Nothing
        Me.FndPaymentMode.FieldCode = Nothing
        Me.FndPaymentMode.FieldDesc = Nothing
        Me.FndPaymentMode.FieldMaxLength = 0
        Me.FndPaymentMode.FieldName = Nothing
        Me.FndPaymentMode.isCalculatedField = False
        Me.FndPaymentMode.IsSourceFromTable = False
        Me.FndPaymentMode.IsSourceFromValueList = False
        Me.FndPaymentMode.IsUnique = False
        Me.FndPaymentMode.Location = New System.Drawing.Point(312, 94)
        Me.FndPaymentMode.MendatroryField = True
        Me.FndPaymentMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndPaymentMode.MyLinkLable1 = Nothing
        Me.FndPaymentMode.MyLinkLable2 = Nothing
        Me.FndPaymentMode.MyReadOnly = False
        Me.FndPaymentMode.MyShowMasterFormButton = False
        Me.FndPaymentMode.Name = "FndPaymentMode"
        Me.FndPaymentMode.ReferenceFieldDesc = Nothing
        Me.FndPaymentMode.ReferenceFieldName = Nothing
        Me.FndPaymentMode.ReferenceTableName = Nothing
        Me.FndPaymentMode.Size = New System.Drawing.Size(155, 19)
        Me.FndPaymentMode.TabIndex = 1471
        Me.FndPaymentMode.Value = ""
        '
        'chkread
        '
        Me.chkread.Location = New System.Drawing.Point(247, 118)
        Me.chkread.MyLinkLable1 = Nothing
        Me.chkread.MyLinkLable2 = Nothing
        Me.chkread.Name = "chkread"
        Me.chkread.Size = New System.Drawing.Size(114, 18)
        Me.chkread.TabIndex = 1469
        Me.chkread.Tag1 = Nothing
        Me.chkread.Text = "Select/UnSelect All"
        '
        'txtCustomerNo
        '
        Me.txtCustomerNo.arrDispalyMember = Nothing
        Me.txtCustomerNo.arrValueMember = Nothing
        Me.txtCustomerNo.Location = New System.Drawing.Point(84, 73)
        Me.txtCustomerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerNo.MyLinkLable1 = Me.MyLabel2
        Me.txtCustomerNo.MyLinkLable2 = Nothing
        Me.txtCustomerNo.MyNullText = "All"
        Me.txtCustomerNo.Name = "txtCustomerNo"
        Me.txtCustomerNo.Size = New System.Drawing.Size(383, 19)
        Me.txtCustomerNo.TabIndex = 1468
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(12, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel2.TabIndex = 1467
        Me.MyLabel2.Text = "Customer"
        '
        'lblpaymentdate
        '
        Me.lblpaymentdate.FieldName = Nothing
        Me.lblpaymentdate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaymentdate.Location = New System.Drawing.Point(12, 121)
        Me.lblpaymentdate.Name = "lblpaymentdate"
        Me.lblpaymentdate.Size = New System.Drawing.Size(72, 16)
        Me.lblpaymentdate.TabIndex = 159
        Me.lblpaymentdate.Text = "Receipt Date"
        '
        'txtReceiptDate
        '
        Me.txtReceiptDate.CalculationExpression = Nothing
        Me.txtReceiptDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReceiptDate.FieldCode = Nothing
        Me.txtReceiptDate.FieldDesc = Nothing
        Me.txtReceiptDate.FieldMaxLength = 0
        Me.txtReceiptDate.FieldName = Nothing
        Me.txtReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReceiptDate.isCalculatedField = False
        Me.txtReceiptDate.IsSourceFromTable = False
        Me.txtReceiptDate.IsSourceFromValueList = False
        Me.txtReceiptDate.IsUnique = False
        Me.txtReceiptDate.Location = New System.Drawing.Point(84, 118)
        Me.txtReceiptDate.MendatroryField = False
        Me.txtReceiptDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReceiptDate.MyLinkLable1 = Me.lblpaymentdate
        Me.txtReceiptDate.MyLinkLable2 = Nothing
        Me.txtReceiptDate.Name = "txtReceiptDate"
        Me.txtReceiptDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReceiptDate.ReferenceFieldDesc = Nothing
        Me.txtReceiptDate.ReferenceFieldName = Nothing
        Me.txtReceiptDate.ReferenceTableName = Nothing
        Me.txtReceiptDate.Size = New System.Drawing.Size(79, 20)
        Me.txtReceiptDate.TabIndex = 160
        Me.txtReceiptDate.TabStop = False
        Me.txtReceiptDate.Text = "10/06/2011"
        Me.txtReceiptDate.Value = New Date(2011, 6, 10, 11, 47, 26, 250)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(12, 99)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel1.TabIndex = 156
        Me.MyLabel1.Text = "Bank Code"
        '
        'fndBankCode
        '
        Me.fndBankCode.CalculationExpression = Nothing
        Me.fndBankCode.FieldCode = Nothing
        Me.fndBankCode.FieldDesc = Nothing
        Me.fndBankCode.FieldMaxLength = 0
        Me.fndBankCode.FieldName = Nothing
        Me.fndBankCode.isCalculatedField = False
        Me.fndBankCode.IsSourceFromTable = False
        Me.fndBankCode.IsSourceFromValueList = False
        Me.fndBankCode.IsUnique = False
        Me.fndBankCode.Location = New System.Drawing.Point(84, 95)
        Me.fndBankCode.MendatroryField = True
        Me.fndBankCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndBankCode.MyLinkLable1 = Nothing
        Me.fndBankCode.MyLinkLable2 = Nothing
        Me.fndBankCode.MyReadOnly = False
        Me.fndBankCode.MyShowMasterFormButton = False
        Me.fndBankCode.Name = "fndBankCode"
        Me.fndBankCode.ReferenceFieldDesc = Nothing
        Me.fndBankCode.ReferenceFieldName = Nothing
        Me.fndBankCode.ReferenceTableName = Nothing
        Me.fndBankCode.Size = New System.Drawing.Size(142, 19)
        Me.fndBankCode.TabIndex = 158
        Me.fndBankCode.Value = ""
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(232, 52)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(235, 18)
        Me.lblLocation.TabIndex = 157
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
        '
        'btnSelectSheet
        '
        Me.btnSelectSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectSheet.Location = New System.Drawing.Point(169, 119)
        Me.btnSelectSheet.Name = "btnSelectSheet"
        Me.btnSelectSheet.Size = New System.Drawing.Size(72, 18)
        Me.btnSelectSheet.TabIndex = 8
        Me.btnSelectSheet.Text = ">>>>>"
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
        Me.txtLocation.Location = New System.Drawing.Point(84, 51)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(142, 19)
        Me.txtLocation.TabIndex = 156
        Me.txtLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(12, 55)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 155
        Me.RadLabel15.Text = "Location"
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
        Me.txtToDate.Location = New System.Drawing.Point(84, 29)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToDate.TabIndex = 11
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13-06-2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
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
        Me.txtFromDate.Location = New System.Drawing.Point(84, 7)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(85, 20)
        Me.txtFromDate.TabIndex = 10
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13-06-2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Location = New System.Drawing.Point(12, 31)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 12
        Me.RadLabel2.Text = "To Date"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(12, 10)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(59, 18)
        Me.RadLabel1.TabIndex = 13
        Me.RadLabel1.Text = "From Date"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        Me.Gv1.Name = "Gv1"
        Me.Gv1.Size = New System.Drawing.Size(739, 283)
        Me.Gv1.TabIndex = 2
        Me.Gv1.Text = "RadGridView1"
        Me.Gv1.Visible = False
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExport.Location = New System.Drawing.Point(210, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(62, 18)
        Me.btnExport.TabIndex = 15
        Me.btnExport.Text = "Export"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(142, 9)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(62, 18)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(670, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        '
        'btnSaveAndPost
        '
        Me.btnSaveAndPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAndPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAndPost.Location = New System.Drawing.Point(3, 9)
        Me.btnSaveAndPost.Name = "btnSaveAndPost"
        Me.btnSaveAndPost.Size = New System.Drawing.Size(133, 18)
        Me.btnSaveAndPost.TabIndex = 12
        Me.btnSaveAndPost.Text = "Create Receipt"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = False
        Me.lblTotalAmount.BorderVisible = True
        Me.lblTotalAmount.FieldName = Nothing
        Me.lblTotalAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.Location = New System.Drawing.Point(561, 9)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(103, 18)
        Me.lblTotalAmount.TabIndex = 1472
        Me.lblTotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalAmount.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(484, 11)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel5.TabIndex = 1473
        Me.MyLabel5.Text = "Total Amount"
        '
        'frmCustomerAdvanceAgainstBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 472)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmCustomerAdvanceAgainstBooking"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Advance For CD"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkread, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblpaymentdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReceiptDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Gv1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveAndPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectSheet As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents fndBankCode As common.UserControls.txtFinder
    Friend WithEvents lblpaymentdate As common.Controls.MyLabel
    Friend WithEvents txtReceiptDate As common.Controls.MyDateTimePicker
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCustomerNo As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkread As common.Controls.MyCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents FndPaymentMode As common.UserControls.txtFinder
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblTotalAmount As common.Controls.MyLabel
End Class
