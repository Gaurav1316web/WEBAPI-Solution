<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerIncetiveEntry
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.pnlSecuity = New System.Windows.Forms.Panel()
        Me.txtSecuityPart = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtADSecuity = New common.MyNumBox()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblDeliveryDate = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.chkApplyDateRange = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.txtCustomer = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.UsLock1 = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMonth = New common.Controls.MyDateTimePicker()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnExportExcel = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitExp = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmSummary = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDetails = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReverse = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.gvCustomer = New Telerik.WinControls.UI.RadGridView()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvCustomerIncentive = New Telerik.WinControls.UI.RadGridView()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvCustomerStructure = New Telerik.WinControls.UI.RadGridView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvCustomerItem = New Telerik.WinControls.UI.RadGridView()
        Me.RadPageViewPage5 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvInvoice = New Telerik.WinControls.UI.RadGridView()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TxtTotIncentiveAmt = New common.MyNumBox()
        Me.TxtTotDeductionAmount = New common.MyNumBox()
        Me.TxtTotalAmount = New common.MyNumBox()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.pnlSecuity.SuspendLayout()
        CType(Me.txtSecuityPart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtADSecuity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyDateRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage4.SuspendLayout()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvCustomerIncentive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomerIncentive.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gvCustomerStructure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomerStructure.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.gvCustomerItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCustomerItem.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage5.SuspendLayout()
        CType(Me.gvInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.TxtTotIncentiveAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotDeductionAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.pnlSecuity)
        Me.RadGroupBox3.Controls.Add(Me.Panel4)
        Me.RadGroupBox3.Controls.Add(Me.chkApplyDateRange)
        Me.RadGroupBox3.Controls.Add(Me.RadButton2)
        Me.RadGroupBox3.Controls.Add(Me.txtCustomer)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox3.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox3.Controls.Add(Me.txtDate)
        Me.RadGroupBox3.Controls.Add(Me.UsLock1)
        Me.RadGroupBox3.Controls.Add(Me.txtCode)
        Me.RadGroupBox3.Controls.Add(Me.btnnew)
        Me.RadGroupBox3.Controls.Add(Me.lblCode)
        Me.RadGroupBox3.Controls.Add(Me.RadButton1)
        Me.RadGroupBox3.Controls.Add(Me.lblLocation)
        Me.RadGroupBox3.Controls.Add(Me.txtLocation)
        Me.RadGroupBox3.Controls.Add(Me.lblTankerNo)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox3.Controls.Add(Me.txtMonth)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(818, 100)
        Me.RadGroupBox3.TabIndex = 1
        '
        'pnlSecuity
        '
        Me.pnlSecuity.Controls.Add(Me.txtSecuityPart)
        Me.pnlSecuity.Controls.Add(Me.MyLabel3)
        Me.pnlSecuity.Controls.Add(Me.txtADSecuity)
        Me.pnlSecuity.Controls.Add(Me.MyLabel42)
        Me.pnlSecuity.Location = New System.Drawing.Point(415, 27)
        Me.pnlSecuity.Name = "pnlSecuity"
        Me.pnlSecuity.Size = New System.Drawing.Size(242, 21)
        Me.pnlSecuity.TabIndex = 1075
        Me.pnlSecuity.Visible = False
        '
        'txtSecuityPart
        '
        Me.txtSecuityPart.BackColor = System.Drawing.Color.White
        Me.txtSecuityPart.CalculationExpression = Nothing
        Me.txtSecuityPart.DecimalPlaces = 0
        Me.txtSecuityPart.FieldCode = Nothing
        Me.txtSecuityPart.FieldDesc = Nothing
        Me.txtSecuityPart.FieldMaxLength = 5
        Me.txtSecuityPart.FieldName = Nothing
        Me.txtSecuityPart.isCalculatedField = False
        Me.txtSecuityPart.IsSourceFromTable = False
        Me.txtSecuityPart.IsSourceFromValueList = False
        Me.txtSecuityPart.IsUnique = False
        Me.txtSecuityPart.Location = New System.Drawing.Point(188, 0)
        Me.txtSecuityPart.MendatroryField = False
        Me.txtSecuityPart.MyLinkLable1 = Me.MyLabel3
        Me.txtSecuityPart.MyLinkLable2 = Nothing
        Me.txtSecuityPart.Name = "txtSecuityPart"
        Me.txtSecuityPart.ReferenceFieldDesc = Nothing
        Me.txtSecuityPart.ReferenceFieldName = Nothing
        Me.txtSecuityPart.ReferenceTableName = Nothing
        Me.txtSecuityPart.Size = New System.Drawing.Size(51, 20)
        Me.txtSecuityPart.TabIndex = 78
        Me.txtSecuityPart.Text = "0"
        Me.txtSecuityPart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSecuityPart.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(118, 2)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel3.TabIndex = 79
        Me.MyLabel3.Text = "Secuity Part"
        '
        'txtADSecuity
        '
        Me.txtADSecuity.BackColor = System.Drawing.Color.White
        Me.txtADSecuity.CalculationExpression = Nothing
        Me.txtADSecuity.DecimalPlaces = 0
        Me.txtADSecuity.FieldCode = Nothing
        Me.txtADSecuity.FieldDesc = Nothing
        Me.txtADSecuity.FieldMaxLength = 5
        Me.txtADSecuity.FieldName = Nothing
        Me.txtADSecuity.isCalculatedField = False
        Me.txtADSecuity.IsSourceFromTable = False
        Me.txtADSecuity.IsSourceFromValueList = False
        Me.txtADSecuity.IsUnique = False
        Me.txtADSecuity.Location = New System.Drawing.Point(66, 0)
        Me.txtADSecuity.MendatroryField = False
        Me.txtADSecuity.MyLinkLable1 = Me.MyLabel42
        Me.txtADSecuity.MyLinkLable2 = Nothing
        Me.txtADSecuity.Name = "txtADSecuity"
        Me.txtADSecuity.ReferenceFieldDesc = Nothing
        Me.txtADSecuity.ReferenceFieldName = Nothing
        Me.txtADSecuity.ReferenceTableName = Nothing
        Me.txtADSecuity.Size = New System.Drawing.Size(51, 20)
        Me.txtADSecuity.TabIndex = 76
        Me.txtADSecuity.Text = "0"
        Me.txtADSecuity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtADSecuity.Value = 0R
        '
        'MyLabel42
        '
        Me.MyLabel42.FieldName = Nothing
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel42.Location = New System.Drawing.Point(2, 2)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel42.TabIndex = 77
        Me.MyLabel42.Text = "AD Secuity"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.MyLabel2)
        Me.Panel4.Controls.Add(Me.txtToDate)
        Me.Panel4.Controls.Add(Me.lblDeliveryDate)
        Me.Panel4.Controls.Add(Me.txtFromDate)
        Me.Panel4.Location = New System.Drawing.Point(338, 51)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(242, 21)
        Me.Panel4.TabIndex = 1074
        Me.Panel4.Visible = False
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(136, 2)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel2.TabIndex = 17
        Me.MyLabel2.Text = "To"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(158, 1)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel2
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(78, 18)
        Me.txtToDate.TabIndex = 16
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "13/06/2011"
        Me.txtToDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDeliveryDate
        '
        Me.lblDeliveryDate.FieldName = Nothing
        Me.lblDeliveryDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeliveryDate.Location = New System.Drawing.Point(3, 2)
        Me.lblDeliveryDate.Name = "lblDeliveryDate"
        Me.lblDeliveryDate.Size = New System.Drawing.Size(33, 16)
        Me.lblDeliveryDate.TabIndex = 15
        Me.lblDeliveryDate.Text = "From"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(35, 1)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.lblDeliveryDate
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(78, 18)
        Me.txtFromDate.TabIndex = 14
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "13/06/2011"
        Me.txtFromDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'chkApplyDateRange
        '
        Me.chkApplyDateRange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApplyDateRange.Location = New System.Drawing.Point(220, 53)
        Me.chkApplyDateRange.Name = "chkApplyDateRange"
        Me.chkApplyDateRange.Size = New System.Drawing.Size(112, 16)
        Me.chkApplyDateRange.TabIndex = 1073
        Me.chkApplyDateRange.Text = "Apply Date Range"
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(583, 52)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(71, 19)
        Me.RadButton2.TabIndex = 14
        Me.RadButton2.Text = "Reset"
        '
        'txtCustomer
        '
        Me.txtCustomer.arrDispalyMember = Nothing
        Me.txtCustomer.arrValueMember = Nothing
        Me.txtCustomer.Location = New System.Drawing.Point(72, 72)
        Me.txtCustomer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.MyLinkLable1 = Me.MyLabel4
        Me.txtCustomer.MyLinkLable2 = Nothing
        Me.txtCustomer.MyNullText = "Please Select..."
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(508, 21)
        Me.txtCustomer.TabIndex = 1071
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(6, 73)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(55, 18)
        Me.MyLabel4.TabIndex = 1072
        Me.MyLabel4.Text = "Customer"
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(417, 5)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDocDate.TabIndex = 1045
        Me.lblDocDate.Text = "Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(450, 3)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDocDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(84, 20)
        Me.txtDate.TabIndex = 1044
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(539, 3)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(117, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1043
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(72, 3)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(320, 21)
        Me.txtCode.TabIndex = 1042
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(6, 5)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(58, 16)
        Me.lblCode.TabIndex = 1040
        Me.lblCode.Text = "Document"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPDairySale.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(392, 3)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(20, 21)
        Me.btnnew.TabIndex = 1041
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(583, 73)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(71, 19)
        Me.RadButton1.TabIndex = 13
        Me.RadButton1.Text = ">>"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLocation.Location = New System.Drawing.Point(219, 27)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(193, 21)
        Me.lblLocation.TabIndex = 6
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
        Me.txtLocation.Location = New System.Drawing.Point(72, 27)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.lblLocation
        Me.txtLocation.MyLinkLable2 = Me.lblTankerNo
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(141, 21)
        Me.txtLocation.TabIndex = 0
        Me.txtLocation.Value = ""
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTankerNo.Location = New System.Drawing.Point(6, 29)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(53, 16)
        Me.lblTankerNo.TabIndex = 10
        Me.lblTankerNo.Text = "Location"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(6, 52)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel1.TabIndex = 9
        Me.MyLabel1.Text = "Month"
        '
        'txtMonth
        '
        Me.txtMonth.CalculationExpression = Nothing
        Me.txtMonth.CustomFormat = "MMM/yyyy"
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
        Me.txtMonth.Location = New System.Drawing.Point(72, 51)
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Me.MyLabel1
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.ShowUpDown = True
        Me.txtMonth.Size = New System.Drawing.Size(141, 20)
        Me.txtMonth.TabIndex = 1
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Sep/2014"
        Me.txtMonth.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'btnsave
        '
        Me.btnsave.Location = New System.Drawing.Point(3, 5)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 23)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExportExcel)
        Me.Panel1.Controls.Add(Me.RadSplitExp)
        Me.Panel1.Controls.Add(Me.btnReverse)
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Controls.Add(Me.btndelete)
        Me.Panel1.Controls.Add(Me.btnPost)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnsave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 475)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(818, 33)
        Me.Panel1.TabIndex = 1
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportExcel.Location = New System.Drawing.Point(520, 5)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(151, 23)
        Me.btnExportExcel.TabIndex = 157
        Me.btnExportExcel.Text = "Export Excel(Selected Tab)"
        '
        'RadSplitExp
        '
        Me.RadSplitExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitExp.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSummary, Me.rmDetails})
        Me.RadSplitExp.Location = New System.Drawing.Point(400, 5)
        Me.RadSplitExp.Name = "RadSplitExp"
        Me.RadSplitExp.Size = New System.Drawing.Size(117, 23)
        Me.RadSplitExp.TabIndex = 156
        Me.RadSplitExp.Text = "Export Notepad"
        '
        'rmSummary
        '
        Me.rmSummary.AccessibleDescription = "Summary"
        Me.rmSummary.AccessibleName = "Summary"
        Me.rmSummary.Name = "rmSummary"
        Me.rmSummary.Text = "Vendor Margin Abstract"
        '
        'rmDetails
        '
        Me.rmDetails.AccessibleDescription = "Details"
        Me.rmDetails.AccessibleName = "Details"
        Me.rmDetails.Name = "rmDetails"
        Me.rmDetails.Text = "Vendor Margin Detail"
        '
        'btnReverse
        '
        Me.btnReverse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverse.Location = New System.Drawing.Point(281, 5)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(117, 23)
        Me.btnReverse.TabIndex = 6
        Me.btnReverse.Text = "Revese and Unpost"
        Me.btnReverse.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(208, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 23)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'btndelete
        '
        Me.btndelete.Location = New System.Drawing.Point(71, 5)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 23)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnPost
        '
        Me.btnPost.Location = New System.Drawing.Point(139, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 23)
        Me.btnPost.TabIndex = 4
        Me.btnPost.Text = "Post"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(745, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(70, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'gvCustomer
        '
        Me.gvCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCustomer.ForeColor = System.Drawing.Color.Black
        Me.gvCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomer.Location = New System.Drawing.Point(0, 0)
        '
        'gvCustomer
        '
        Me.gvCustomer.MasterTemplate.AllowDeleteRow = False
        Me.gvCustomer.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvCustomer.Name = "gvCustomer"
        Me.gvCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomer.ShowGroupPanel = False
        Me.gvCustomer.Size = New System.Drawing.Size(797, 292)
        Me.gvCustomer.TabIndex = 3
        Me.gvCustomer.Text = "RadGridView1"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage5)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 100)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage4
        Me.RadPageView1.Size = New System.Drawing.Size(818, 375)
        Me.RadPageView1.TabIndex = 218
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.SplitContainer1)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(45.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(797, 327)
        Me.RadPageViewPage4.Text = "Detail"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvCustomerIncentive)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(163.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(797, 327)
        Me.RadPageViewPage3.Text = "Customer And Incentive Wise"
        '
        'gvCustomerIncentive
        '
        Me.gvCustomerIncentive.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCustomerIncentive.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomerIncentive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomerIncentive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCustomerIncentive.ForeColor = System.Drawing.Color.Black
        Me.gvCustomerIncentive.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomerIncentive.Location = New System.Drawing.Point(0, 0)
        '
        'gvCustomerIncentive
        '
        Me.gvCustomerIncentive.MasterTemplate.AllowDeleteRow = False
        Me.gvCustomerIncentive.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvCustomerIncentive.Name = "gvCustomerIncentive"
        Me.gvCustomerIncentive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomerIncentive.ShowGroupPanel = False
        Me.gvCustomerIncentive.Size = New System.Drawing.Size(797, 327)
        Me.gvCustomerIncentive.TabIndex = 7
        Me.gvCustomerIncentive.Text = "RadGridView1"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gvCustomerStructure)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(164.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(797, 327)
        Me.RadPageViewPage2.Text = "Customer And Structure Wise"
        '
        'gvCustomerStructure
        '
        Me.gvCustomerStructure.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCustomerStructure.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomerStructure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomerStructure.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCustomerStructure.ForeColor = System.Drawing.Color.Black
        Me.gvCustomerStructure.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomerStructure.Location = New System.Drawing.Point(0, 0)
        '
        'gvCustomerStructure
        '
        Me.gvCustomerStructure.MasterTemplate.AllowDeleteRow = False
        Me.gvCustomerStructure.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvCustomerStructure.Name = "gvCustomerStructure"
        Me.gvCustomerStructure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomerStructure.ShowGroupPanel = False
        Me.gvCustomerStructure.Size = New System.Drawing.Size(797, 327)
        Me.gvCustomerStructure.TabIndex = 6
        Me.gvCustomerStructure.Text = "RadGridView1"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.gvCustomerItem)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(146.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(797, 327)
        Me.RadPageViewPage1.Text = "Customer And Item Detail"
        '
        'gvCustomerItem
        '
        Me.gvCustomerItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvCustomerItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCustomerItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCustomerItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCustomerItem.ForeColor = System.Drawing.Color.Black
        Me.gvCustomerItem.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCustomerItem.Location = New System.Drawing.Point(0, 0)
        '
        'gvCustomerItem
        '
        Me.gvCustomerItem.MasterTemplate.AllowDeleteRow = False
        Me.gvCustomerItem.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvCustomerItem.Name = "gvCustomerItem"
        Me.gvCustomerItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCustomerItem.ShowGroupPanel = False
        Me.gvCustomerItem.Size = New System.Drawing.Size(797, 327)
        Me.gvCustomerItem.TabIndex = 5
        Me.gvCustomerItem.Text = "RadGridView1"
        '
        'RadPageViewPage5
        '
        Me.RadPageViewPage5.Controls.Add(Me.gvInvoice)
        Me.RadPageViewPage5.ItemSize = New System.Drawing.SizeF(84.0!, 28.0!)
        Me.RadPageViewPage5.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage5.Name = "RadPageViewPage5"
        Me.RadPageViewPage5.Size = New System.Drawing.Size(797, 327)
        Me.RadPageViewPage5.Text = "Invoice Detail"
        '
        'gvInvoice
        '
        Me.gvInvoice.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvInvoice.ForeColor = System.Drawing.Color.Black
        Me.gvInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvInvoice.Location = New System.Drawing.Point(0, 0)
        '
        'gvInvoice
        '
        Me.gvInvoice.MasterTemplate.AllowDeleteRow = False
        Me.gvInvoice.MasterTemplate.EnableAlternatingRowColor = True
        Me.gvInvoice.Name = "gvInvoice"
        Me.gvInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvInvoice.ShowGroupPanel = False
        Me.gvInvoice.Size = New System.Drawing.Size(797, 327)
        Me.gvInvoice.TabIndex = 4
        Me.gvInvoice.Text = "RadGridView1"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Manual Seal"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 100)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(818, 375)
        Me.RadGroupBox2.TabIndex = 220
        Me.RadGroupBox2.Text = "Manual Seal"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox4.HeaderText = "Paper Seal"
        Me.RadGroupBox4.Location = New System.Drawing.Point(0, 100)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Size = New System.Drawing.Size(818, 375)
        Me.RadGroupBox4.TabIndex = 219
        Me.RadGroupBox4.Text = "Paper Seal"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvCustomer)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TxtTotalAmount)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TxtTotDeductionAmount)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TxtTotIncentiveAmt)
        Me.SplitContainer1.Size = New System.Drawing.Size(797, 327)
        Me.SplitContainer1.SplitterDistance = 292
        Me.SplitContainer1.TabIndex = 4
        '
        'TxtTotIncentiveAmt
        '
        Me.TxtTotIncentiveAmt.BackColor = System.Drawing.Color.White
        Me.TxtTotIncentiveAmt.CalculationExpression = Nothing
        Me.TxtTotIncentiveAmt.DecimalPlaces = 0
        Me.TxtTotIncentiveAmt.FieldCode = Nothing
        Me.TxtTotIncentiveAmt.FieldDesc = Nothing
        Me.TxtTotIncentiveAmt.FieldMaxLength = 5
        Me.TxtTotIncentiveAmt.FieldName = Nothing
        Me.TxtTotIncentiveAmt.isCalculatedField = False
        Me.TxtTotIncentiveAmt.IsSourceFromTable = False
        Me.TxtTotIncentiveAmt.IsSourceFromValueList = False
        Me.TxtTotIncentiveAmt.IsUnique = False
        Me.TxtTotIncentiveAmt.Location = New System.Drawing.Point(373, 5)
        Me.TxtTotIncentiveAmt.MendatroryField = False
        Me.TxtTotIncentiveAmt.MyLinkLable1 = Me.MyLabel42
        Me.TxtTotIncentiveAmt.MyLinkLable2 = Nothing
        Me.TxtTotIncentiveAmt.Name = "TxtTotIncentiveAmt"
        Me.TxtTotIncentiveAmt.ReferenceFieldDesc = Nothing
        Me.TxtTotIncentiveAmt.ReferenceFieldName = Nothing
        Me.TxtTotIncentiveAmt.ReferenceTableName = Nothing
        Me.TxtTotIncentiveAmt.Size = New System.Drawing.Size(110, 20)
        Me.TxtTotIncentiveAmt.TabIndex = 77
        Me.TxtTotIncentiveAmt.Text = "0"
        Me.TxtTotIncentiveAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotIncentiveAmt.Value = 0R
        '
        'TxtTotDeductionAmount
        '
        Me.TxtTotDeductionAmount.BackColor = System.Drawing.Color.White
        Me.TxtTotDeductionAmount.CalculationExpression = Nothing
        Me.TxtTotDeductionAmount.DecimalPlaces = 0
        Me.TxtTotDeductionAmount.FieldCode = Nothing
        Me.TxtTotDeductionAmount.FieldDesc = Nothing
        Me.TxtTotDeductionAmount.FieldMaxLength = 5
        Me.TxtTotDeductionAmount.FieldName = Nothing
        Me.TxtTotDeductionAmount.isCalculatedField = False
        Me.TxtTotDeductionAmount.IsSourceFromTable = False
        Me.TxtTotDeductionAmount.IsSourceFromValueList = False
        Me.TxtTotDeductionAmount.IsUnique = False
        Me.TxtTotDeductionAmount.Location = New System.Drawing.Point(486, 5)
        Me.TxtTotDeductionAmount.MendatroryField = False
        Me.TxtTotDeductionAmount.MyLinkLable1 = Me.MyLabel42
        Me.TxtTotDeductionAmount.MyLinkLable2 = Nothing
        Me.TxtTotDeductionAmount.Name = "TxtTotDeductionAmount"
        Me.TxtTotDeductionAmount.ReferenceFieldDesc = Nothing
        Me.TxtTotDeductionAmount.ReferenceFieldName = Nothing
        Me.TxtTotDeductionAmount.ReferenceTableName = Nothing
        Me.TxtTotDeductionAmount.Size = New System.Drawing.Size(110, 20)
        Me.TxtTotDeductionAmount.TabIndex = 78
        Me.TxtTotDeductionAmount.Text = "0"
        Me.TxtTotDeductionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotDeductionAmount.Value = 0R
        '
        'TxtTotalAmount
        '
        Me.TxtTotalAmount.BackColor = System.Drawing.Color.White
        Me.TxtTotalAmount.CalculationExpression = Nothing
        Me.TxtTotalAmount.DecimalPlaces = 0
        Me.TxtTotalAmount.FieldCode = Nothing
        Me.TxtTotalAmount.FieldDesc = Nothing
        Me.TxtTotalAmount.FieldMaxLength = 5
        Me.TxtTotalAmount.FieldName = Nothing
        Me.TxtTotalAmount.isCalculatedField = False
        Me.TxtTotalAmount.IsSourceFromTable = False
        Me.TxtTotalAmount.IsSourceFromValueList = False
        Me.TxtTotalAmount.IsUnique = False
        Me.TxtTotalAmount.Location = New System.Drawing.Point(601, 5)
        Me.TxtTotalAmount.MendatroryField = False
        Me.TxtTotalAmount.MyLinkLable1 = Me.MyLabel42
        Me.TxtTotalAmount.MyLinkLable2 = Nothing
        Me.TxtTotalAmount.Name = "TxtTotalAmount"
        Me.TxtTotalAmount.ReferenceFieldDesc = Nothing
        Me.TxtTotalAmount.ReferenceFieldName = Nothing
        Me.TxtTotalAmount.ReferenceTableName = Nothing
        Me.TxtTotalAmount.Size = New System.Drawing.Size(110, 20)
        Me.TxtTotalAmount.TabIndex = 78
        Me.TxtTotalAmount.Text = "0"
        Me.TxtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotalAmount.Value = 0R
        '
        'frmCustomerIncetiveEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 508)
        Me.Controls.Add(Me.RadPageView1)
        Me.Controls.Add(Me.RadGroupBox2)
        Me.Controls.Add(Me.RadGroupBox4)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmCustomerIncetiveEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Incentive Entry"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        Me.pnlSecuity.ResumeLayout(False)
        Me.pnlSecuity.PerformLayout()
        CType(Me.txtSecuityPart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtADSecuity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyDateRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReverse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage4.ResumeLayout(False)
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvCustomerIncentive.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomerIncentive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gvCustomerStructure.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomerStructure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.gvCustomerItem.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCustomerItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage5.ResumeLayout(False)
        CType(Me.gvInvoice.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.TxtTotIncentiveAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotDeductionAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents gvCustomer As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtCustomer As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage5 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvInvoice As Telerik.WinControls.UI.RadGridView
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvCustomerItem As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvCustomerStructure As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnReverse As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gvCustomerIncentive As Telerik.WinControls.UI.RadGridView
    Friend WithEvents chkApplyDateRange As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDeliveryDate As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadSplitExp As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmSummary As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDetails As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents pnlSecuity As Panel
    Friend WithEvents txtSecuityPart As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtADSecuity As common.MyNumBox
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents btnExportExcel As RadButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TxtTotalAmount As common.MyNumBox
    Friend WithEvents TxtTotDeductionAmount As common.MyNumBox
    Friend WithEvents TxtTotIncentiveAmt As common.MyNumBox
End Class

