<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMilkVSPPayment
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
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.fndArea = New common.UserControls.txtFinder()
        Me.Area = New common.Controls.MyLabel()
        Me.txtFiscalYear = New common.Controls.MyTextBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtPaymentCycleNo = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.fndDocNo = New common.UserControls.txtNavigator()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.lblMCC = New common.Controls.MyLabel()
        Me.lblTankerNo = New common.Controls.MyLabel()
        Me.txtMCCMultiple = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblPaymentType = New common.Controls.MyLabel()
        Me.txtVSP = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtMonth = New common.Controls.MyDateTimePicker()
        Me.btnGenerateBill = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnJE = New Telerik.WinControls.UI.RadButton()
        Me.btnProvisionBill = New Telerik.WinControls.UI.RadButton()
        Me.btnDeleteVSPBill = New Telerik.WinControls.UI.RadButton()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.BtnIncentive = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.Area, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFiscalYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaymentCycleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGenerateBill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnJE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnProvisionBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDeleteVSPBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnIncentive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.fndArea)
        Me.RadGroupBox3.Controls.Add(Me.Area)
        Me.RadGroupBox3.Controls.Add(Me.txtFiscalYear)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox3.Controls.Add(Me.txtPaymentCycleNo)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox3.Controls.Add(Me.fndDocNo)
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox3.Controls.Add(Me.lblPaymentType)
        Me.RadGroupBox3.Controls.Add(Me.txtVSP)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox3.Controls.Add(Me.lblTankerNo)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.Controls.Add(Me.txtToDate)
        Me.RadGroupBox3.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox3.Controls.Add(Me.txtMonth)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = "VSP Payment"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(962, 279)
        Me.RadGroupBox3.TabIndex = 1
        Me.RadGroupBox3.Text = "VSP Payment"
        '
        'fndArea
        '
        Me.fndArea.CalculationExpression = Nothing
        Me.fndArea.FieldCode = Nothing
        Me.fndArea.FieldDesc = Nothing
        Me.fndArea.FieldMaxLength = 0
        Me.fndArea.FieldName = Nothing
        Me.fndArea.isCalculatedField = False
        Me.fndArea.IsSourceFromTable = False
        Me.fndArea.IsSourceFromValueList = False
        Me.fndArea.IsUnique = False
        Me.fndArea.Location = New System.Drawing.Point(81, 23)
        Me.fndArea.MendatroryField = True
        Me.fndArea.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndArea.MyLinkLable1 = Nothing
        Me.fndArea.MyLinkLable2 = Nothing
        Me.fndArea.MyReadOnly = False
        Me.fndArea.MyShowMasterFormButton = False
        Me.fndArea.Name = "fndArea"
        Me.fndArea.ReferenceFieldDesc = Nothing
        Me.fndArea.ReferenceFieldName = Nothing
        Me.fndArea.ReferenceTableName = Nothing
        Me.fndArea.Size = New System.Drawing.Size(169, 19)
        Me.fndArea.TabIndex = 1075
        Me.fndArea.Value = ""
        '
        'Area
        '
        Me.Area.FieldName = Nothing
        Me.Area.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Area.Location = New System.Drawing.Point(13, 24)
        Me.Area.Name = "Area"
        Me.Area.Size = New System.Drawing.Size(30, 16)
        Me.Area.TabIndex = 626
        Me.Area.Text = "Area"
        '
        'txtFiscalYear
        '
        Me.txtFiscalYear.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtFiscalYear.CalculationExpression = Nothing
        Me.txtFiscalYear.Enabled = False
        Me.txtFiscalYear.FieldCode = Nothing
        Me.txtFiscalYear.FieldDesc = Nothing
        Me.txtFiscalYear.FieldMaxLength = 0
        Me.txtFiscalYear.FieldName = Nothing
        Me.txtFiscalYear.isCalculatedField = False
        Me.txtFiscalYear.IsSourceFromTable = False
        Me.txtFiscalYear.IsSourceFromValueList = False
        Me.txtFiscalYear.IsUnique = False
        Me.txtFiscalYear.Location = New System.Drawing.Point(289, 134)
        Me.txtFiscalYear.MendatroryField = False
        Me.txtFiscalYear.MyLinkLable1 = Nothing
        Me.txtFiscalYear.MyLinkLable2 = Nothing
        Me.txtFiscalYear.Name = "txtFiscalYear"
        Me.txtFiscalYear.ReadOnly = True
        Me.txtFiscalYear.ReferenceFieldDesc = Nothing
        Me.txtFiscalYear.ReferenceFieldName = Nothing
        Me.txtFiscalYear.ReferenceTableName = Nothing
        Me.txtFiscalYear.Size = New System.Drawing.Size(143, 20)
        Me.txtFiscalYear.TabIndex = 624
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(222, 136)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel6.TabIndex = 623
        Me.MyLabel6.Text = "Fiscal Year"
        '
        'txtPaymentCycleNo
        '
        Me.txtPaymentCycleNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtPaymentCycleNo.CalculationExpression = Nothing
        Me.txtPaymentCycleNo.Enabled = False
        Me.txtPaymentCycleNo.FieldCode = Nothing
        Me.txtPaymentCycleNo.FieldDesc = Nothing
        Me.txtPaymentCycleNo.FieldMaxLength = 0
        Me.txtPaymentCycleNo.FieldName = Nothing
        Me.txtPaymentCycleNo.isCalculatedField = False
        Me.txtPaymentCycleNo.IsSourceFromTable = False
        Me.txtPaymentCycleNo.IsSourceFromValueList = False
        Me.txtPaymentCycleNo.IsUnique = False
        Me.txtPaymentCycleNo.Location = New System.Drawing.Point(81, 134)
        Me.txtPaymentCycleNo.MendatroryField = False
        Me.txtPaymentCycleNo.MyLinkLable1 = Nothing
        Me.txtPaymentCycleNo.MyLinkLable2 = Nothing
        Me.txtPaymentCycleNo.Name = "txtPaymentCycleNo"
        Me.txtPaymentCycleNo.ReadOnly = True
        Me.txtPaymentCycleNo.ReferenceFieldDesc = Nothing
        Me.txtPaymentCycleNo.ReferenceFieldName = Nothing
        Me.txtPaymentCycleNo.ReferenceTableName = Nothing
        Me.txtPaymentCycleNo.Size = New System.Drawing.Size(135, 20)
        Me.txtPaymentCycleNo.TabIndex = 622
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(13, 136)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel7.TabIndex = 621
        Me.MyLabel7.Text = "Cycle"
        '
        'fndDocNo
        '
        Me.fndDocNo.FieldName = Nothing
        Me.fndDocNo.Location = New System.Drawing.Point(12, 245)
        Me.fndDocNo.MendatroryField = False
        Me.fndDocNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.fndDocNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndDocNo.MyLinkLable1 = Nothing
        Me.fndDocNo.MyLinkLable2 = Nothing
        Me.fndDocNo.MyMaxLength = 32767
        Me.fndDocNo.MyReadOnly = False
        Me.fndDocNo.Name = "fndDocNo"
        Me.fndDocNo.Size = New System.Drawing.Size(265, 21)
        Me.fndDocNo.TabIndex = 259
        Me.fndDocNo.Value = ""
        Me.fndDocNo.Visible = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(81, 47)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMCC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblMCC)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtMCCMultiple)
        Me.SplitContainer1.Size = New System.Drawing.Size(351, 19)
        Me.SplitContainer1.SplitterDistance = 174
        Me.SplitContainer1.TabIndex = 13
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(1, 0)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.lblMCC
        Me.txtMCC.MyLinkLable2 = Me.lblTankerNo
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(169, 19)
        Me.txtMCC.TabIndex = 0
        Me.txtMCC.Value = ""
        '
        'lblMCC
        '
        Me.lblMCC.AutoSize = False
        Me.lblMCC.BorderVisible = True
        Me.lblMCC.FieldName = Nothing
        Me.lblMCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMCC.Location = New System.Drawing.Point(171, 0)
        Me.lblMCC.Name = "lblMCC"
        Me.lblMCC.Size = New System.Drawing.Size(180, 19)
        Me.lblMCC.TabIndex = 6
        '
        'lblTankerNo
        '
        Me.lblTankerNo.FieldName = Nothing
        Me.lblTankerNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTankerNo.Location = New System.Drawing.Point(13, 48)
        Me.lblTankerNo.Name = "lblTankerNo"
        Me.lblTankerNo.Size = New System.Drawing.Size(32, 16)
        Me.lblTankerNo.TabIndex = 10
        Me.lblTankerNo.Text = "BMC"
        '
        'txtMCCMultiple
        '
        Me.txtMCCMultiple.arrDispalyMember = Nothing
        Me.txtMCCMultiple.arrValueMember = Nothing
        Me.txtMCCMultiple.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtMCCMultiple.Location = New System.Drawing.Point(0, 0)
        Me.txtMCCMultiple.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCCMultiple.MyLinkLable1 = Me.MyLabel4
        Me.txtMCCMultiple.MyLinkLable2 = Nothing
        Me.txtMCCMultiple.MyNullText = "Please Select..."
        Me.txtMCCMultiple.Name = "txtMCCMultiple"
        Me.txtMCCMultiple.Size = New System.Drawing.Size(173, 19)
        Me.txtMCCMultiple.TabIndex = 5
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 112)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(26, 18)
        Me.MyLabel4.TabIndex = 7
        Me.MyLabel4.Text = "VLC"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(204, 71)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(31, 16)
        Me.MyLabel5.TabIndex = 12
        Me.MyLabel5.Text = "Type"
        '
        'lblPaymentType
        '
        Me.lblPaymentType.AutoSize = False
        Me.lblPaymentType.BorderVisible = True
        Me.lblPaymentType.FieldName = Nothing
        Me.lblPaymentType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentType.Location = New System.Drawing.Point(252, 68)
        Me.lblPaymentType.Name = "lblPaymentType"
        Me.lblPaymentType.Size = New System.Drawing.Size(180, 19)
        Me.lblPaymentType.TabIndex = 11
        '
        'txtVSP
        '
        Me.txtVSP.arrDispalyMember = Nothing
        Me.txtVSP.arrValueMember = Nothing
        Me.txtVSP.Location = New System.Drawing.Point(81, 112)
        Me.txtVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSP.MyLinkLable1 = Me.MyLabel4
        Me.txtVSP.MyLinkLable2 = Nothing
        Me.txtVSP.MyNullText = "Please Select..."
        Me.txtVSP.Name = "txtVSP"
        Me.txtVSP.Size = New System.Drawing.Size(351, 19)
        Me.txtVSP.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(204, 92)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel3.TabIndex = 5
        Me.MyLabel3.Text = "To Date"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 92)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(60, 16)
        Me.MyLabel2.TabIndex = 8
        Me.MyLabel2.Text = "From Date"
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd - MMM - yyyy"
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
        Me.txtToDate.Location = New System.Drawing.Point(252, 91)
        Me.txtToDate.MendatroryField = True
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel3
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReadOnly = True
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(103, 18)
        Me.txtToDate.TabIndex = 3
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "29 - Sep - 2014"
        Me.txtToDate.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd - MMM - yyyy"
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
        Me.txtFromDate.Location = New System.Drawing.Point(81, 91)
        Me.txtFromDate.MendatroryField = True
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(101, 18)
        Me.txtFromDate.TabIndex = 2
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "29 - Sep - 2014"
        Me.txtFromDate.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(13, 71)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel1.TabIndex = 9
        Me.MyLabel1.Text = "Month"
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
        Me.txtMonth.Location = New System.Drawing.Point(81, 70)
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.MyLinkLable1 = Me.MyLabel1
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.Size = New System.Drawing.Size(101, 18)
        Me.txtMonth.TabIndex = 1
        Me.txtMonth.TabStop = False
        Me.txtMonth.Text = "Sep - 2014"
        Me.txtMonth.Value = New Date(2014, 9, 29, 0, 0, 0, 0)
        '
        'btnGenerateBill
        '
        Me.btnGenerateBill.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateBill.Location = New System.Drawing.Point(3, 6)
        Me.btnGenerateBill.Name = "btnGenerateBill"
        Me.btnGenerateBill.Size = New System.Drawing.Size(111, 23)
        Me.btnGenerateBill.TabIndex = 0
        Me.btnGenerateBill.Text = "Generate Bill"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnJE)
        Me.Panel1.Controls.Add(Me.btnProvisionBill)
        Me.Panel1.Controls.Add(Me.btnDeleteVSPBill)
        Me.Panel1.Controls.Add(Me.BtnReset)
        Me.Panel1.Controls.Add(Me.BtnIncentive)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnGenerateBill)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 279)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(962, 33)
        Me.Panel1.TabIndex = 1
        '
        'btnJE
        '
        Me.btnJE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnJE.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnJE.Location = New System.Drawing.Point(231, 6)
        Me.btnJE.Name = "btnJE"
        Me.btnJE.Size = New System.Drawing.Size(70, 22)
        Me.btnJE.TabIndex = 284
        Me.btnJE.Text = "Show JE"
        '
        'btnProvisionBill
        '
        Me.btnProvisionBill.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnProvisionBill.Location = New System.Drawing.Point(117, 6)
        Me.btnProvisionBill.Name = "btnProvisionBill"
        Me.btnProvisionBill.Size = New System.Drawing.Size(111, 23)
        Me.btnProvisionBill.TabIndex = 2
        Me.btnProvisionBill.Text = "Provision Bill"
        Me.btnProvisionBill.Visible = False
        '
        'btnDeleteVSPBill
        '
        Me.btnDeleteVSPBill.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteVSPBill.Location = New System.Drawing.Point(532, 6)
        Me.btnDeleteVSPBill.Name = "btnDeleteVSPBill"
        Me.btnDeleteVSPBill.Size = New System.Drawing.Size(254, 23)
        Me.btnDeleteVSPBill.TabIndex = 283
        Me.btnDeleteVSPBill.Text = "Delete VSP Bill Not Used in Payment Process"
        Me.btnDeleteVSPBill.Visible = False
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Location = New System.Drawing.Point(418, 6)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(111, 23)
        Me.BtnReset.TabIndex = 2
        Me.BtnReset.Text = "Reset"
        Me.BtnReset.Visible = False
        '
        'BtnIncentive
        '
        Me.BtnIncentive.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnIncentive.Location = New System.Drawing.Point(304, 6)
        Me.BtnIncentive.Name = "BtnIncentive"
        Me.BtnIncentive.Size = New System.Drawing.Size(111, 23)
        Me.BtnIncentive.TabIndex = 1
        Me.BtnIncentive.Text = "Generate Incentive"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(884, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'FrmMilkVSPPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(962, 312)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmMilkVSPPayment"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "VSP Payment"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.Area, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFiscalYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaymentCycleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblMCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTankerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGenerateBill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.btnJE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnProvisionBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDeleteVSPBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnIncentive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerateBill As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtMonth As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents BtnIncentive As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents lblMCC As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblTankerNo As common.Controls.MyLabel
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblPaymentType As common.Controls.MyLabel
    Friend WithEvents btnDeleteVSPBill As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtMCCMultiple As common.UserControls.txtMultiSelectFinder
    Friend WithEvents btnProvisionBill As RadButton
    Friend WithEvents fndDocNo As common.UserControls.txtNavigator
    Friend WithEvents btnJE As RadButton
    Friend WithEvents txtFiscalYear As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtPaymentCycleNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents Area As common.Controls.MyLabel
    Friend WithEvents fndArea As common.UserControls.txtFinder
End Class

