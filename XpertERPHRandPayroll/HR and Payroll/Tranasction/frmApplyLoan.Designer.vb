Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApplyLoan
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
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmApplyLoan))
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.dtpEndMonth = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.lblGrossSalary = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblDevision = New common.Controls.MyLabel()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.cboLoanStatus = New common.Controls.MyComboBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.lblInterestPeriodicity = New common.Controls.MyLabel()
        Me.cboInterestPeriodicity = New common.Controls.MyComboBox()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblRemarks = New common.Controls.MyLabel()
        Me.lblLoanBy = New common.Controls.MyLabel()
        Me.lblEmpName = New common.Controls.MyLabel()
        Me.txtInterestRate = New common.MyNumBox()
        Me.lblInterestRate = New common.Controls.MyLabel()
        Me.txtNoofEmi = New common.MyNumBox()
        Me.lblNoOfEMI = New common.Controls.MyLabel()
        Me.txtInterestAmt = New common.MyNumBox()
        Me.lblInterestType = New common.Controls.MyLabel()
        Me.txtTotalPayableAmount = New common.Controls.MyTextBox()
        Me.lblTotPayable = New common.Controls.MyLabel()
        Me.lblInterestAmount = New common.Controls.MyLabel()
        Me.cboInterestType = New common.Controls.MyComboBox()
        Me.chkApplyInterest = New Telerik.WinControls.UI.RadCheckBox()
        Me.lblPaymDate = New common.Controls.MyLabel()
        Me.dtpPaymentStartDate = New common.Controls.MyDateTimePicker()
        Me.txtDays = New common.Controls.MyTextBox()
        Me.lblLoanPeriod = New common.Controls.MyLabel()
        Me.txtMonth = New common.Controls.MyTextBox()
        Me.txtLoanAmount = New common.Controls.MyTextBox()
        Me.lblLoanAmount = New common.Controls.MyLabel()
        Me.lblGivenBy = New common.Controls.MyLabel()
        Me.findLoanBy = New common.UserControls.txtFinder()
        Me.lblLoanDate = New common.Controls.MyLabel()
        Me.dtpLoanDate = New common.Controls.MyDateTimePicker()
        Me.lblEmpCode = New common.Controls.MyLabel()
        Me.txtEmpCode = New common.UserControls.txtFinder()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblLoanCode = New common.Controls.MyLabel()
        Me.gvEMI = New common.UserControls.MyRadGridView()
        Me.cboLoanType = New common.Controls.MyComboBox()
        Me.lblLoanType = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrossSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLoanStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInterestPeriodicity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboInterestPeriodicity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInterestRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInterestRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoofEmi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfEMI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInterestAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInterestType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPayableAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInterestAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboInterestType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkApplyInterest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPaymDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPaymentStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLoanAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGivenBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpLoanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEMI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvEMI.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLoanType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLoanType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.SplitContainer1)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(813, 571)
        Me.RadGroupBox3.TabIndex = 64
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpEndMonth)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGrossSalary)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDevision)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboLoanStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblInterestPeriodicity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboInterestPeriodicity)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInterestRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNoofEmi)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInterestAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblNoOfEMI)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTotalPayableAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTotPayable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblInterestAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblInterestRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblInterestType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboInterestType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkApplyInterest)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPaymDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpPaymentStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDays)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMonth)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanPeriod)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtLoanAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblGivenBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.findLoanBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpLoanDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmpCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvEMI)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboLoanType)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblLoanType)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(793, 541)
        Me.SplitContainer1.SplitterDistance = 499
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(14, 287)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel2.TabIndex = 214
        Me.MyLabel2.Text = "EMI End Month"
        '
        'dtpEndMonth
        '
        Me.dtpEndMonth.CalculationExpression = Nothing
        Me.dtpEndMonth.CustomFormat = "MM/yyyy"
        Me.dtpEndMonth.FieldCode = Nothing
        Me.dtpEndMonth.FieldDesc = Nothing
        Me.dtpEndMonth.FieldMaxLength = 0
        Me.dtpEndMonth.FieldName = Nothing
        Me.dtpEndMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndMonth.isCalculatedField = False
        Me.dtpEndMonth.IsSourceFromTable = False
        Me.dtpEndMonth.IsSourceFromValueList = False
        Me.dtpEndMonth.IsUnique = False
        Me.dtpEndMonth.Location = New System.Drawing.Point(147, 287)
        Me.dtpEndMonth.MendatroryField = True
        Me.dtpEndMonth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndMonth.MyLinkLable1 = Me.MyLabel2
        Me.dtpEndMonth.MyLinkLable2 = Nothing
        Me.dtpEndMonth.Name = "dtpEndMonth"
        Me.dtpEndMonth.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndMonth.ReferenceFieldDesc = Nothing
        Me.dtpEndMonth.ReferenceFieldName = Nothing
        Me.dtpEndMonth.ReferenceTableName = Nothing
        Me.dtpEndMonth.Size = New System.Drawing.Size(221, 18)
        Me.dtpEndMonth.TabIndex = 213
        Me.dtpEndMonth.TabStop = False
        Me.dtpEndMonth.Text = "05/2011"
        Me.dtpEndMonth.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(12, 178)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(72, 16)
        Me.MyLabel7.TabIndex = 212
        Me.MyLabel7.Text = "Gross Salary"
        '
        'lblGrossSalary
        '
        Me.lblGrossSalary.AutoSize = False
        Me.lblGrossSalary.BorderVisible = True
        Me.lblGrossSalary.FieldName = Nothing
        Me.lblGrossSalary.Location = New System.Drawing.Point(147, 178)
        Me.lblGrossSalary.Name = "lblGrossSalary"
        Me.lblGrossSalary.Size = New System.Drawing.Size(301, 19)
        Me.lblGrossSalary.TabIndex = 211
        Me.lblGrossSalary.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(454, 156)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel5.TabIndex = 210
        Me.MyLabel5.Text = "Devision"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 156)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel4.TabIndex = 209
        Me.MyLabel4.Text = "Location"
        '
        'lblDevision
        '
        Me.lblDevision.AutoSize = False
        Me.lblDevision.BorderVisible = True
        Me.lblDevision.FieldName = Nothing
        Me.lblDevision.Location = New System.Drawing.Point(510, 156)
        Me.lblDevision.Name = "lblDevision"
        Me.lblDevision.Size = New System.Drawing.Size(274, 19)
        Me.lblDevision.TabIndex = 208
        Me.lblDevision.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLocationCode
        '
        Me.lblLocationCode.AutoSize = False
        Me.lblLocationCode.BorderVisible = True
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Location = New System.Drawing.Point(147, 156)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(301, 19)
        Me.lblLocationCode.TabIndex = 207
        Me.lblLocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboLoanStatus
        '
        Me.cboLoanStatus.CalculationExpression = Nothing
        Me.cboLoanStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLoanStatus.FieldCode = Nothing
        Me.cboLoanStatus.FieldDesc = Nothing
        Me.cboLoanStatus.FieldMaxLength = 0
        Me.cboLoanStatus.FieldName = Nothing
        Me.cboLoanStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanStatus.isCalculatedField = False
        Me.cboLoanStatus.IsSourceFromTable = False
        Me.cboLoanStatus.IsSourceFromValueList = False
        Me.cboLoanStatus.IsUnique = False
        RadListDataItem1.Text = "Approve"
        RadListDataItem2.Text = "Hold"
        RadListDataItem3.Text = "Cancelled"
        RadListDataItem4.Text = "Open"
        Me.cboLoanStatus.Items.Add(RadListDataItem1)
        Me.cboLoanStatus.Items.Add(RadListDataItem2)
        Me.cboLoanStatus.Items.Add(RadListDataItem3)
        Me.cboLoanStatus.Items.Add(RadListDataItem4)
        Me.cboLoanStatus.Location = New System.Drawing.Point(147, 112)
        Me.cboLoanStatus.MendatroryField = True
        Me.cboLoanStatus.MyLinkLable1 = Me.MyLabel1
        Me.cboLoanStatus.MyLinkLable2 = Nothing
        Me.cboLoanStatus.Name = "cboLoanStatus"
        Me.cboLoanStatus.ReferenceFieldDesc = Nothing
        Me.cboLoanStatus.ReferenceFieldName = Nothing
        Me.cboLoanStatus.ReferenceTableName = Nothing
        Me.cboLoanStatus.Size = New System.Drawing.Size(221, 18)
        Me.cboLoanStatus.TabIndex = 205
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(14, 114)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel1.TabIndex = 206
        Me.MyLabel1.Text = "Loan Status"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(498, 18)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 204
        '
        'lblInterestPeriodicity
        '
        Me.lblInterestPeriodicity.FieldName = Nothing
        Me.lblInterestPeriodicity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInterestPeriodicity.Location = New System.Drawing.Point(15, 384)
        Me.lblInterestPeriodicity.Name = "lblInterestPeriodicity"
        Me.lblInterestPeriodicity.Size = New System.Drawing.Size(99, 16)
        Me.lblInterestPeriodicity.TabIndex = 203
        Me.lblInterestPeriodicity.Text = "Interest Periodicity"
        '
        'cboInterestPeriodicity
        '
        Me.cboInterestPeriodicity.CalculationExpression = Nothing
        Me.cboInterestPeriodicity.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboInterestPeriodicity.FieldCode = Nothing
        Me.cboInterestPeriodicity.FieldDesc = Nothing
        Me.cboInterestPeriodicity.FieldMaxLength = 0
        Me.cboInterestPeriodicity.FieldName = Nothing
        Me.cboInterestPeriodicity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInterestPeriodicity.isCalculatedField = False
        Me.cboInterestPeriodicity.IsSourceFromTable = False
        Me.cboInterestPeriodicity.IsSourceFromValueList = False
        Me.cboInterestPeriodicity.IsUnique = False
        RadListDataItem5.Text = "Monthly"
        RadListDataItem6.Text = "Quarterly"
        RadListDataItem7.Text = "Yearly"
        Me.cboInterestPeriodicity.Items.Add(RadListDataItem5)
        Me.cboInterestPeriodicity.Items.Add(RadListDataItem6)
        Me.cboInterestPeriodicity.Items.Add(RadListDataItem7)
        Me.cboInterestPeriodicity.Location = New System.Drawing.Point(147, 380)
        Me.cboInterestPeriodicity.MendatroryField = False
        Me.cboInterestPeriodicity.MyLinkLable1 = Me.lblInterestPeriodicity
        Me.cboInterestPeriodicity.MyLinkLable2 = Nothing
        Me.cboInterestPeriodicity.Name = "cboInterestPeriodicity"
        Me.cboInterestPeriodicity.ReferenceFieldDesc = Nothing
        Me.cboInterestPeriodicity.ReferenceFieldName = Nothing
        Me.cboInterestPeriodicity.ReferenceTableName = Nothing
        Me.cboInterestPeriodicity.Size = New System.Drawing.Size(221, 18)
        Me.cboInterestPeriodicity.TabIndex = 17
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(374, 18)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(14, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'txtDescription
        '
        Me.txtDescription.AutoSize = False
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(147, 64)
        Me.txtDescription.MaxLength = 49
        Me.txtDescription.MendatroryField = True
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.lblRemarks
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(637, 45)
        Me.txtDescription.TabIndex = 20
        '
        'lblRemarks
        '
        Me.lblRemarks.FieldName = Nothing
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(15, 65)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(63, 16)
        Me.lblRemarks.TabIndex = 201
        Me.lblRemarks.Text = "Description"
        '
        'lblLoanBy
        '
        Me.lblLoanBy.AutoSize = False
        Me.lblLoanBy.BorderVisible = True
        Me.lblLoanBy.FieldName = Nothing
        Me.lblLoanBy.Location = New System.Drawing.Point(374, 469)
        Me.lblLoanBy.Name = "lblLoanBy"
        Me.lblLoanBy.Size = New System.Drawing.Size(222, 19)
        Me.lblLoanBy.TabIndex = 6
        Me.lblLoanBy.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLoanBy.Visible = False
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.FieldName = Nothing
        Me.lblEmpName.Location = New System.Drawing.Point(374, 134)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(410, 19)
        Me.lblEmpName.TabIndex = 3
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInterestRate
        '
        Me.txtInterestRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtInterestRate.CalculationExpression = Nothing
        Me.txtInterestRate.DecimalPlaces = 2
        Me.txtInterestRate.FieldCode = Nothing
        Me.txtInterestRate.FieldDesc = Nothing
        Me.txtInterestRate.FieldMaxLength = 0
        Me.txtInterestRate.FieldName = Nothing
        Me.txtInterestRate.isCalculatedField = False
        Me.txtInterestRate.IsSourceFromTable = False
        Me.txtInterestRate.IsSourceFromValueList = False
        Me.txtInterestRate.IsUnique = False
        Me.txtInterestRate.Location = New System.Drawing.Point(147, 356)
        Me.txtInterestRate.MaxLength = 6
        Me.txtInterestRate.MendatroryField = True
        Me.txtInterestRate.MyLinkLable1 = Me.lblInterestRate
        Me.txtInterestRate.MyLinkLable2 = Nothing
        Me.txtInterestRate.Name = "txtInterestRate"
        Me.txtInterestRate.ReferenceFieldDesc = Nothing
        Me.txtInterestRate.ReferenceFieldName = Nothing
        Me.txtInterestRate.ReferenceTableName = Nothing
        Me.txtInterestRate.Size = New System.Drawing.Size(221, 20)
        Me.txtInterestRate.TabIndex = 16
        Me.txtInterestRate.Text = "0"
        Me.txtInterestRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtInterestRate.Value = 0.0R
        '
        'lblInterestRate
        '
        Me.lblInterestRate.FieldName = Nothing
        Me.lblInterestRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInterestRate.Location = New System.Drawing.Point(14, 360)
        Me.lblInterestRate.Name = "lblInterestRate"
        Me.lblInterestRate.Size = New System.Drawing.Size(71, 16)
        Me.lblInterestRate.TabIndex = 187
        Me.lblInterestRate.Text = "Interest Rate"
        '
        'txtNoofEmi
        '
        Me.txtNoofEmi.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNoofEmi.CalculationExpression = Nothing
        Me.txtNoofEmi.DecimalPlaces = 2
        Me.txtNoofEmi.FieldCode = Nothing
        Me.txtNoofEmi.FieldDesc = Nothing
        Me.txtNoofEmi.FieldMaxLength = 0
        Me.txtNoofEmi.FieldName = Nothing
        Me.txtNoofEmi.isCalculatedField = False
        Me.txtNoofEmi.IsSourceFromTable = False
        Me.txtNoofEmi.IsSourceFromValueList = False
        Me.txtNoofEmi.IsUnique = False
        Me.txtNoofEmi.Location = New System.Drawing.Point(147, 243)
        Me.txtNoofEmi.MaxLength = 6
        Me.txtNoofEmi.MendatroryField = True
        Me.txtNoofEmi.MyLinkLable1 = Me.lblNoOfEMI
        Me.txtNoofEmi.MyLinkLable2 = Nothing
        Me.txtNoofEmi.Name = "txtNoofEmi"
        Me.txtNoofEmi.ReferenceFieldDesc = Nothing
        Me.txtNoofEmi.ReferenceFieldName = Nothing
        Me.txtNoofEmi.ReferenceTableName = Nothing
        Me.txtNoofEmi.Size = New System.Drawing.Size(221, 20)
        Me.txtNoofEmi.TabIndex = 13
        Me.txtNoofEmi.Text = "0"
        Me.txtNoofEmi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoofEmi.Value = 0.0R
        '
        'lblNoOfEMI
        '
        Me.lblNoOfEMI.FieldName = Nothing
        Me.lblNoOfEMI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOfEMI.Location = New System.Drawing.Point(14, 247)
        Me.lblNoOfEMI.Name = "lblNoOfEMI"
        Me.lblNoOfEMI.Size = New System.Drawing.Size(120, 16)
        Me.lblNoOfEMI.TabIndex = 193
        Me.lblNoOfEMI.Text = "Loan Period In Months"
        '
        'txtInterestAmt
        '
        Me.txtInterestAmt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtInterestAmt.CalculationExpression = Nothing
        Me.txtInterestAmt.DecimalPlaces = 2
        Me.txtInterestAmt.FieldCode = Nothing
        Me.txtInterestAmt.FieldDesc = Nothing
        Me.txtInterestAmt.FieldMaxLength = 0
        Me.txtInterestAmt.FieldName = Nothing
        Me.txtInterestAmt.isCalculatedField = False
        Me.txtInterestAmt.IsSourceFromTable = False
        Me.txtInterestAmt.IsSourceFromValueList = False
        Me.txtInterestAmt.IsUnique = False
        Me.txtInterestAmt.Location = New System.Drawing.Point(147, 402)
        Me.txtInterestAmt.MaxLength = 6
        Me.txtInterestAmt.MendatroryField = True
        Me.txtInterestAmt.MyLinkLable1 = Me.lblInterestType
        Me.txtInterestAmt.MyLinkLable2 = Nothing
        Me.txtInterestAmt.Name = "txtInterestAmt"
        Me.txtInterestAmt.ReferenceFieldDesc = Nothing
        Me.txtInterestAmt.ReferenceFieldName = Nothing
        Me.txtInterestAmt.ReferenceTableName = Nothing
        Me.txtInterestAmt.Size = New System.Drawing.Size(221, 20)
        Me.txtInterestAmt.TabIndex = 18
        Me.txtInterestAmt.Text = "0"
        Me.txtInterestAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtInterestAmt.Value = 0.0R
        '
        'lblInterestType
        '
        Me.lblInterestType.FieldName = Nothing
        Me.lblInterestType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInterestType.Location = New System.Drawing.Point(14, 339)
        Me.lblInterestType.Name = "lblInterestType"
        Me.lblInterestType.Size = New System.Drawing.Size(72, 16)
        Me.lblInterestType.TabIndex = 186
        Me.lblInterestType.Text = "Interest Type"
        '
        'txtTotalPayableAmount
        '
        Me.txtTotalPayableAmount.CalculationExpression = Nothing
        Me.txtTotalPayableAmount.FieldCode = Nothing
        Me.txtTotalPayableAmount.FieldDesc = Nothing
        Me.txtTotalPayableAmount.FieldMaxLength = 0
        Me.txtTotalPayableAmount.FieldName = Nothing
        Me.txtTotalPayableAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPayableAmount.isCalculatedField = False
        Me.txtTotalPayableAmount.IsSourceFromTable = False
        Me.txtTotalPayableAmount.IsSourceFromValueList = False
        Me.txtTotalPayableAmount.IsUnique = False
        Me.txtTotalPayableAmount.Location = New System.Drawing.Point(147, 426)
        Me.txtTotalPayableAmount.MaxLength = 49
        Me.txtTotalPayableAmount.MendatroryField = True
        Me.txtTotalPayableAmount.MyLinkLable1 = Me.lblTotPayable
        Me.txtTotalPayableAmount.MyLinkLable2 = Nothing
        Me.txtTotalPayableAmount.Name = "txtTotalPayableAmount"
        Me.txtTotalPayableAmount.ReferenceFieldDesc = Nothing
        Me.txtTotalPayableAmount.ReferenceFieldName = Nothing
        Me.txtTotalPayableAmount.ReferenceTableName = Nothing
        Me.txtTotalPayableAmount.Size = New System.Drawing.Size(221, 18)
        Me.txtTotalPayableAmount.TabIndex = 19
        '
        'lblTotPayable
        '
        Me.lblTotPayable.FieldName = Nothing
        Me.lblTotPayable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPayable.Location = New System.Drawing.Point(14, 428)
        Me.lblTotPayable.Name = "lblTotPayable"
        Me.lblTotPayable.Size = New System.Drawing.Size(118, 16)
        Me.lblTotPayable.TabIndex = 191
        Me.lblTotPayable.Text = "Total Payable Amount"
        '
        'lblInterestAmount
        '
        Me.lblInterestAmount.FieldName = Nothing
        Me.lblInterestAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInterestAmount.Location = New System.Drawing.Point(15, 407)
        Me.lblInterestAmount.Name = "lblInterestAmount"
        Me.lblInterestAmount.Size = New System.Drawing.Size(86, 16)
        Me.lblInterestAmount.TabIndex = 189
        Me.lblInterestAmount.Text = "Interest Amount"
        '
        'cboInterestType
        '
        Me.cboInterestType.CalculationExpression = Nothing
        Me.cboInterestType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboInterestType.FieldCode = Nothing
        Me.cboInterestType.FieldDesc = Nothing
        Me.cboInterestType.FieldMaxLength = 0
        Me.cboInterestType.FieldName = Nothing
        Me.cboInterestType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInterestType.isCalculatedField = False
        Me.cboInterestType.IsSourceFromTable = False
        Me.cboInterestType.IsSourceFromValueList = False
        Me.cboInterestType.IsUnique = False
        RadListDataItem8.Text = "Simple"
        RadListDataItem9.Text = "Compound"
        RadListDataItem10.Text = "None"
        Me.cboInterestType.Items.Add(RadListDataItem8)
        Me.cboInterestType.Items.Add(RadListDataItem9)
        Me.cboInterestType.Items.Add(RadListDataItem10)
        Me.cboInterestType.Location = New System.Drawing.Point(147, 335)
        Me.cboInterestType.MendatroryField = False
        Me.cboInterestType.MyLinkLable1 = Me.lblInterestType
        Me.cboInterestType.MyLinkLable2 = Nothing
        Me.cboInterestType.Name = "cboInterestType"
        Me.cboInterestType.ReferenceFieldDesc = Nothing
        Me.cboInterestType.ReferenceFieldName = Nothing
        Me.cboInterestType.ReferenceTableName = Nothing
        Me.cboInterestType.Size = New System.Drawing.Size(221, 18)
        Me.cboInterestType.TabIndex = 15
        '
        'chkApplyInterest
        '
        Me.chkApplyInterest.Location = New System.Drawing.Point(147, 311)
        Me.chkApplyInterest.Name = "chkApplyInterest"
        Me.chkApplyInterest.Size = New System.Drawing.Size(90, 18)
        Me.chkApplyInterest.TabIndex = 14
        Me.chkApplyInterest.Text = "Apply Interest"
        '
        'lblPaymDate
        '
        Me.lblPaymDate.FieldName = Nothing
        Me.lblPaymDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymDate.Location = New System.Drawing.Point(14, 268)
        Me.lblPaymDate.Name = "lblPaymDate"
        Me.lblPaymDate.Size = New System.Drawing.Size(88, 16)
        Me.lblPaymDate.TabIndex = 183
        Me.lblPaymDate.Text = "EMI Start Month"
        '
        'dtpPaymentStartDate
        '
        Me.dtpPaymentStartDate.CalculationExpression = Nothing
        Me.dtpPaymentStartDate.CustomFormat = "MM/yyyy"
        Me.dtpPaymentStartDate.FieldCode = Nothing
        Me.dtpPaymentStartDate.FieldDesc = Nothing
        Me.dtpPaymentStartDate.FieldMaxLength = 0
        Me.dtpPaymentStartDate.FieldName = Nothing
        Me.dtpPaymentStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPaymentStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPaymentStartDate.isCalculatedField = False
        Me.dtpPaymentStartDate.IsSourceFromTable = False
        Me.dtpPaymentStartDate.IsSourceFromValueList = False
        Me.dtpPaymentStartDate.IsUnique = False
        Me.dtpPaymentStartDate.Location = New System.Drawing.Point(147, 266)
        Me.dtpPaymentStartDate.MendatroryField = True
        Me.dtpPaymentStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPaymentStartDate.MyLinkLable1 = Me.lblPaymDate
        Me.dtpPaymentStartDate.MyLinkLable2 = Nothing
        Me.dtpPaymentStartDate.Name = "dtpPaymentStartDate"
        Me.dtpPaymentStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPaymentStartDate.ReferenceFieldDesc = Nothing
        Me.dtpPaymentStartDate.ReferenceFieldName = Nothing
        Me.dtpPaymentStartDate.ReferenceTableName = Nothing
        Me.dtpPaymentStartDate.Size = New System.Drawing.Size(221, 18)
        Me.dtpPaymentStartDate.TabIndex = 12
        Me.dtpPaymentStartDate.TabStop = False
        Me.dtpPaymentStartDate.Text = "05/2011"
        Me.dtpPaymentStartDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtDays
        '
        Me.txtDays.CalculationExpression = Nothing
        Me.txtDays.FieldCode = Nothing
        Me.txtDays.FieldDesc = Nothing
        Me.txtDays.FieldMaxLength = 0
        Me.txtDays.FieldName = Nothing
        Me.txtDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays.isCalculatedField = False
        Me.txtDays.IsSourceFromTable = False
        Me.txtDays.IsSourceFromValueList = False
        Me.txtDays.IsUnique = False
        Me.txtDays.Location = New System.Drawing.Point(253, 450)
        Me.txtDays.MaxLength = 49
        Me.txtDays.MendatroryField = True
        Me.txtDays.MyLinkLable1 = Me.lblLoanPeriod
        Me.txtDays.MyLinkLable2 = Nothing
        Me.txtDays.Name = "txtDays"
        Me.txtDays.NullText = "Days"
        Me.txtDays.ReferenceFieldDesc = Nothing
        Me.txtDays.ReferenceFieldName = Nothing
        Me.txtDays.ReferenceTableName = Nothing
        Me.txtDays.Size = New System.Drawing.Size(115, 18)
        Me.txtDays.TabIndex = 11
        Me.txtDays.Visible = False
        '
        'lblLoanPeriod
        '
        Me.lblLoanPeriod.FieldName = Nothing
        Me.lblLoanPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanPeriod.Location = New System.Drawing.Point(14, 452)
        Me.lblLoanPeriod.Name = "lblLoanPeriod"
        Me.lblLoanPeriod.Size = New System.Drawing.Size(67, 16)
        Me.lblLoanPeriod.TabIndex = 179
        Me.lblLoanPeriod.Text = "Loan Period"
        Me.lblLoanPeriod.Visible = False
        '
        'txtMonth
        '
        Me.txtMonth.CalculationExpression = Nothing
        Me.txtMonth.FieldCode = Nothing
        Me.txtMonth.FieldDesc = Nothing
        Me.txtMonth.FieldMaxLength = 0
        Me.txtMonth.FieldName = Nothing
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.isCalculatedField = False
        Me.txtMonth.IsSourceFromTable = False
        Me.txtMonth.IsSourceFromValueList = False
        Me.txtMonth.IsUnique = False
        Me.txtMonth.Location = New System.Drawing.Point(147, 450)
        Me.txtMonth.MaxLength = 49
        Me.txtMonth.MendatroryField = True
        Me.txtMonth.MyLinkLable1 = Me.lblLoanPeriod
        Me.txtMonth.MyLinkLable2 = Nothing
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.NullText = "Months"
        Me.txtMonth.ReferenceFieldDesc = Nothing
        Me.txtMonth.ReferenceFieldName = Nothing
        Me.txtMonth.ReferenceTableName = Nothing
        Me.txtMonth.Size = New System.Drawing.Size(100, 18)
        Me.txtMonth.TabIndex = 10
        Me.txtMonth.Visible = False
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.CalculationExpression = Nothing
        Me.txtLoanAmount.FieldCode = Nothing
        Me.txtLoanAmount.FieldDesc = Nothing
        Me.txtLoanAmount.FieldMaxLength = 0
        Me.txtLoanAmount.FieldName = Nothing
        Me.txtLoanAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanAmount.isCalculatedField = False
        Me.txtLoanAmount.IsSourceFromTable = False
        Me.txtLoanAmount.IsSourceFromValueList = False
        Me.txtLoanAmount.IsUnique = False
        Me.txtLoanAmount.Location = New System.Drawing.Point(147, 221)
        Me.txtLoanAmount.MaxLength = 49
        Me.txtLoanAmount.MendatroryField = True
        Me.txtLoanAmount.MyLinkLable1 = Me.lblLoanAmount
        Me.txtLoanAmount.MyLinkLable2 = Nothing
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.ReferenceFieldDesc = Nothing
        Me.txtLoanAmount.ReferenceFieldName = Nothing
        Me.txtLoanAmount.ReferenceTableName = Nothing
        Me.txtLoanAmount.Size = New System.Drawing.Size(221, 18)
        Me.txtLoanAmount.TabIndex = 9
        '
        'lblLoanAmount
        '
        Me.lblLoanAmount.FieldName = Nothing
        Me.lblLoanAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanAmount.Location = New System.Drawing.Point(14, 223)
        Me.lblLoanAmount.Name = "lblLoanAmount"
        Me.lblLoanAmount.Size = New System.Drawing.Size(74, 16)
        Me.lblLoanAmount.TabIndex = 177
        Me.lblLoanAmount.Text = "Loan Amount"
        '
        'lblGivenBy
        '
        Me.lblGivenBy.FieldName = Nothing
        Me.lblGivenBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGivenBy.Location = New System.Drawing.Point(14, 472)
        Me.lblGivenBy.Name = "lblGivenBy"
        Me.lblGivenBy.Size = New System.Drawing.Size(78, 16)
        Me.lblGivenBy.TabIndex = 165
        Me.lblGivenBy.Text = "Loan given By"
        Me.lblGivenBy.Visible = False
        '
        'findLoanBy
        '
        Me.findLoanBy.CalculationExpression = Nothing
        Me.findLoanBy.FieldCode = Nothing
        Me.findLoanBy.FieldDesc = Nothing
        Me.findLoanBy.FieldMaxLength = 0
        Me.findLoanBy.FieldName = Nothing
        Me.findLoanBy.isCalculatedField = False
        Me.findLoanBy.IsSourceFromTable = False
        Me.findLoanBy.IsSourceFromValueList = False
        Me.findLoanBy.IsUnique = False
        Me.findLoanBy.Location = New System.Drawing.Point(147, 470)
        Me.findLoanBy.MendatroryField = False
        Me.findLoanBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.findLoanBy.MyLinkLable1 = Me.lblGivenBy
        Me.findLoanBy.MyLinkLable2 = Nothing
        Me.findLoanBy.MyReadOnly = False
        Me.findLoanBy.MyShowMasterFormButton = False
        Me.findLoanBy.Name = "findLoanBy"
        Me.findLoanBy.ReferenceFieldDesc = Nothing
        Me.findLoanBy.ReferenceFieldName = Nothing
        Me.findLoanBy.ReferenceTableName = Nothing
        Me.findLoanBy.Size = New System.Drawing.Size(221, 19)
        Me.findLoanBy.TabIndex = 5
        Me.findLoanBy.Value = ""
        Me.findLoanBy.Visible = False
        '
        'lblLoanDate
        '
        Me.lblLoanDate.FieldName = Nothing
        Me.lblLoanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanDate.Location = New System.Drawing.Point(14, 43)
        Me.lblLoanDate.Name = "lblLoanDate"
        Me.lblLoanDate.Size = New System.Drawing.Size(58, 16)
        Me.lblLoanDate.TabIndex = 164
        Me.lblLoanDate.Text = "Loan Date"
        '
        'dtpLoanDate
        '
        Me.dtpLoanDate.CalculationExpression = Nothing
        Me.dtpLoanDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpLoanDate.FieldCode = Nothing
        Me.dtpLoanDate.FieldDesc = Nothing
        Me.dtpLoanDate.FieldMaxLength = 0
        Me.dtpLoanDate.FieldName = Nothing
        Me.dtpLoanDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpLoanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLoanDate.isCalculatedField = False
        Me.dtpLoanDate.IsSourceFromTable = False
        Me.dtpLoanDate.IsSourceFromValueList = False
        Me.dtpLoanDate.IsUnique = False
        Me.dtpLoanDate.Location = New System.Drawing.Point(147, 42)
        Me.dtpLoanDate.MendatroryField = True
        Me.dtpLoanDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLoanDate.MyLinkLable1 = Me.lblLoanDate
        Me.dtpLoanDate.MyLinkLable2 = Nothing
        Me.dtpLoanDate.Name = "dtpLoanDate"
        Me.dtpLoanDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpLoanDate.ReferenceFieldDesc = Nothing
        Me.dtpLoanDate.ReferenceFieldName = Nothing
        Me.dtpLoanDate.ReferenceTableName = Nothing
        Me.dtpLoanDate.Size = New System.Drawing.Size(130, 18)
        Me.dtpLoanDate.TabIndex = 4
        Me.dtpLoanDate.TabStop = False
        Me.dtpLoanDate.Text = "03/05/2011"
        Me.dtpLoanDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblEmpCode
        '
        Me.lblEmpCode.FieldName = Nothing
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.Location = New System.Drawing.Point(13, 137)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.Size = New System.Drawing.Size(87, 16)
        Me.lblEmpCode.TabIndex = 154
        Me.lblEmpCode.Text = "Employee Code"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.CalculationExpression = Nothing
        Me.txtEmpCode.FieldCode = Nothing
        Me.txtEmpCode.FieldDesc = Nothing
        Me.txtEmpCode.FieldMaxLength = 0
        Me.txtEmpCode.FieldName = Nothing
        Me.txtEmpCode.isCalculatedField = False
        Me.txtEmpCode.IsSourceFromTable = False
        Me.txtEmpCode.IsSourceFromValueList = False
        Me.txtEmpCode.IsUnique = False
        Me.txtEmpCode.Location = New System.Drawing.Point(147, 134)
        Me.txtEmpCode.MendatroryField = True
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.lblEmpCode
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = False
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.ReferenceFieldDesc = Nothing
        Me.txtEmpCode.ReferenceFieldName = Nothing
        Me.txtEmpCode.ReferenceTableName = Nothing
        Me.txtEmpCode.Size = New System.Drawing.Size(221, 19)
        Me.txtEmpCode.TabIndex = 2
        Me.txtEmpCode.Value = ""
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(147, 17)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblLoanCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(221, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblLoanCode
        '
        Me.lblLoanCode.FieldName = Nothing
        Me.lblLoanCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanCode.Location = New System.Drawing.Point(14, 24)
        Me.lblLoanCode.Name = "lblLoanCode"
        Me.lblLoanCode.Size = New System.Drawing.Size(62, 16)
        Me.lblLoanCode.TabIndex = 161
        Me.lblLoanCode.Text = "Loan Code"
        '
        'gvEMI
        '
        Me.gvEMI.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvEMI.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvEMI.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvEMI.ForeColor = System.Drawing.Color.Black
        Me.gvEMI.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvEMI.Location = New System.Drawing.Point(374, 200)
        '
        'gvEMI
        '
        Me.gvEMI.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvEMI.MasterTemplate.AllowAddNewRow = False
        Me.gvEMI.MasterTemplate.AllowEditRow = False
        Me.gvEMI.MasterTemplate.AutoGenerateColumns = False
        Me.gvEMI.MasterTemplate.EnableGrouping = False
        Me.gvEMI.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvEMI.Name = "gvEMI"
        Me.gvEMI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvEMI.ShowHeaderCellButtons = True
        Me.gvEMI.Size = New System.Drawing.Size(410, 244)
        Me.gvEMI.TabIndex = 8
        Me.gvEMI.TabStop = False
        Me.gvEMI.Text = "RadGridView4"
        '
        'cboLoanType
        '
        Me.cboLoanType.CalculationExpression = Nothing
        Me.cboLoanType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboLoanType.FieldCode = Nothing
        Me.cboLoanType.FieldDesc = Nothing
        Me.cboLoanType.FieldMaxLength = 0
        Me.cboLoanType.FieldName = Nothing
        Me.cboLoanType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanType.isCalculatedField = False
        Me.cboLoanType.IsSourceFromTable = False
        Me.cboLoanType.IsSourceFromValueList = False
        Me.cboLoanType.IsUnique = False
        RadListDataItem11.Text = "Loan"
        RadListDataItem12.Text = "Advance"
        Me.cboLoanType.Items.Add(RadListDataItem11)
        Me.cboLoanType.Items.Add(RadListDataItem12)
        Me.cboLoanType.Location = New System.Drawing.Point(147, 200)
        Me.cboLoanType.MendatroryField = True
        Me.cboLoanType.MyLinkLable1 = Me.lblLoanType
        Me.cboLoanType.MyLinkLable2 = Nothing
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.ReferenceFieldDesc = Nothing
        Me.cboLoanType.ReferenceFieldName = Nothing
        Me.cboLoanType.ReferenceTableName = Nothing
        Me.cboLoanType.Size = New System.Drawing.Size(221, 18)
        Me.cboLoanType.TabIndex = 7
        '
        'lblLoanType
        '
        Me.lblLoanType.FieldName = Nothing
        Me.lblLoanType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanType.Location = New System.Drawing.Point(14, 199)
        Me.lblLoanType.Name = "lblLoanType"
        Me.lblLoanType.Size = New System.Drawing.Size(60, 16)
        Me.lblLoanType.TabIndex = 153
        Me.lblLoanType.Text = "Loan Type"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Enabled = False
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(149, 11)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 1
        Me.btnPost.Text = "Post"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(9, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(718, 11)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(80, 11)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 2
        Me.btndelete.Text = "Delete"
        '
        'frmApplyLoan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 571)
        Me.Controls.Add(Me.RadGroupBox3)
        Me.Name = "frmApplyLoan"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Apply for Loan/Advance"
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrossSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDevision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLoanStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInterestPeriodicity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboInterestPeriodicity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInterestRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInterestRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoofEmi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfEMI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInterestAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInterestType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPayableAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInterestAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboInterestType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkApplyInterest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPaymDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPaymentStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLoanAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGivenBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpLoanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEMI.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvEMI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLoanType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLoanType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtLoanAmount As common.Controls.MyTextBox
    Friend WithEvents lblLoanAmount As common.Controls.MyLabel
    Friend WithEvents lblGivenBy As common.Controls.MyLabel
    Friend WithEvents findLoanBy As common.UserControls.txtFinder
    Friend WithEvents lblLoanDate As common.Controls.MyLabel
    Friend WithEvents dtpLoanDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblEmpCode As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblLoanCode As common.Controls.MyLabel
    Friend WithEvents gvEMI As common.UserControls.MyRadGridView
    Friend WithEvents cboLoanType As common.Controls.MyComboBox
    Friend WithEvents lblLoanType As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtMonth As common.Controls.MyTextBox
    Friend WithEvents lblLoanPeriod As common.Controls.MyLabel
    Friend WithEvents txtDays As common.Controls.MyTextBox
    Friend WithEvents lblPaymDate As common.Controls.MyLabel
    Friend WithEvents dtpPaymentStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents chkApplyInterest As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents lblInterestType As common.Controls.MyLabel
    Friend WithEvents cboInterestType As common.Controls.MyComboBox
    Friend WithEvents lblInterestRate As common.Controls.MyLabel
    Friend WithEvents lblInterestAmount As common.Controls.MyLabel
    Friend WithEvents txtTotalPayableAmount As common.Controls.MyTextBox
    Friend WithEvents lblTotPayable As common.Controls.MyLabel
    Friend WithEvents lblNoOfEMI As common.Controls.MyLabel
    Friend WithEvents txtInterestAmt As common.MyNumBox
    Friend WithEvents txtInterestRate As common.MyNumBox
    Friend WithEvents txtNoofEmi As common.MyNumBox
    Friend WithEvents lblLoanBy As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents lblRemarks As common.Controls.MyLabel
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblInterestPeriodicity As common.Controls.MyLabel
    Friend WithEvents cboInterestPeriodicity As common.Controls.MyComboBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboLoanStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblDevision As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents lblGrossSalary As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpEndMonth As common.Controls.MyDateTimePicker
End Class
