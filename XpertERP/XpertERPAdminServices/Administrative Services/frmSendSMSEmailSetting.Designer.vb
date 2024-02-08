<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSendSMSEmailSetting
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
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.rlblDescription = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtSchedulerCode = New common.UserControls.txtNavigator()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cboScreenName = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkSMSLastDayOfMonth = New System.Windows.Forms.CheckBox()
        Me.cmbWeekDaysForSMS = New common.Controls.MyComboBox()
        Me.txtSMSMonthly = New common.Controls.MyDateTimePicker()
        Me.rdbSMSMonthly = New System.Windows.Forms.RadioButton()
        Me.rdbSMSWeekly = New System.Windows.Forms.RadioButton()
        Me.rdbSMSEveryDays = New System.Windows.Forms.RadioButton()
        Me.dtpSMSSchedulTime = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.chkEmailLastDayOfMonth = New System.Windows.Forms.CheckBox()
        Me.txtEmailMonthly = New common.Controls.MyDateTimePicker()
        Me.cmbWeekDaysForEmail = New common.Controls.MyComboBox()
        Me.rdbEmailMonthly = New System.Windows.Forms.RadioButton()
        Me.rdbEmailWeekly = New System.Windows.Forms.RadioButton()
        Me.rdbEmailEveryDays = New System.Windows.Forms.RadioButton()
        Me.dtpEmailSchedulTime = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.rbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.rbtnSave = New Telerik.WinControls.UI.RadButton()
        Me.rbtnDelete = New Telerik.WinControls.UI.RadButton()
        Me.rdbEmailNone = New System.Windows.Forms.RadioButton()
        Me.rdbSMSNone = New System.Windows.Forms.RadioButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.cboScreenName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.cmbWeekDaysForSMS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSMSMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpSMSSchedulTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.txtEmailMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbWeekDaysForEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEmailSchedulTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rlblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSchedulerCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rbtnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(946, 446)
        Me.SplitContainer1.SplitterDistance = 417
        Me.SplitContainer1.TabIndex = 0
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
        Me.txtDescription.Location = New System.Drawing.Point(101, 37)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.Multiline = True
        Me.txtDescription.MyLinkLable1 = Me.rlblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(263, 18)
        Me.txtDescription.TabIndex = 13
        '
        'rlblDescription
        '
        Me.rlblDescription.FieldName = Nothing
        Me.rlblDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rlblDescription.Location = New System.Drawing.Point(8, 38)
        Me.rlblDescription.Name = "rlblDescription"
        Me.rlblDescription.Size = New System.Drawing.Size(66, 16)
        Me.rlblDescription.TabIndex = 12
        Me.rlblDescription.Text = "Description "
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(8, 17)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel4.TabIndex = 5
        Me.MyLabel4.Text = "Scheduler Code"
        '
        'txtSchedulerCode
        '
        Me.txtSchedulerCode.FieldName = Nothing
        Me.txtSchedulerCode.Location = New System.Drawing.Point(101, 16)
        Me.txtSchedulerCode.MendatroryField = True
        Me.txtSchedulerCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSchedulerCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtSchedulerCode.MyLinkLable1 = Me.MyLabel4
        Me.txtSchedulerCode.MyLinkLable2 = Nothing
        Me.txtSchedulerCode.MyMaxLength = 32767
        Me.txtSchedulerCode.MyReadOnly = False
        Me.txtSchedulerCode.Name = "txtSchedulerCode"
        Me.txtSchedulerCode.Size = New System.Drawing.Size(245, 18)
        Me.txtSchedulerCode.TabIndex = 6
        Me.txtSchedulerCode.Value = ""
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.XpertERPAdminServices.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(346, 16)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 19)
        Me.btnNew.TabIndex = 7
        Me.btnNew.Text = "&"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cboScreenName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.RadButton1)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox3)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.HeaderText = "Scheduler Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 60)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(810, 183)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Scheduler Details"
        '
        'cboScreenName
        '
        Me.cboScreenName.AutoCompleteDisplayMember = Nothing
        Me.cboScreenName.AutoCompleteValueMember = Nothing
        Me.cboScreenName.CalculationExpression = Nothing
        Me.cboScreenName.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboScreenName.FieldCode = Nothing
        Me.cboScreenName.FieldDesc = Nothing
        Me.cboScreenName.FieldMaxLength = 0
        Me.cboScreenName.FieldName = Nothing
        Me.cboScreenName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboScreenName.isCalculatedField = False
        Me.cboScreenName.IsSourceFromTable = False
        Me.cboScreenName.IsSourceFromValueList = False
        Me.cboScreenName.IsUnique = False
        Me.cboScreenName.Location = New System.Drawing.Point(60, 20)
        Me.cboScreenName.MendatroryField = False
        Me.cboScreenName.MyLinkLable1 = Nothing
        Me.cboScreenName.MyLinkLable2 = Nothing
        Me.cboScreenName.Name = "cboScreenName"
        Me.cboScreenName.ReferenceFieldDesc = Nothing
        Me.cboScreenName.ReferenceFieldName = Nothing
        Me.cboScreenName.ReferenceTableName = Nothing
        Me.cboScreenName.Size = New System.Drawing.Size(200, 18)
        Me.cboScreenName.TabIndex = 9
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(14, 21)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(40, 18)
        Me.MyLabel3.TabIndex = 4
        Me.MyLabel3.Text = "Screen"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(716, 153)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(89, 18)
        Me.RadButton1.TabIndex = 3
        Me.RadButton1.Text = "Set Text"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbSMSNone)
        Me.RadGroupBox3.Controls.Add(Me.chkSMSLastDayOfMonth)
        Me.RadGroupBox3.Controls.Add(Me.cmbWeekDaysForSMS)
        Me.RadGroupBox3.Controls.Add(Me.txtSMSMonthly)
        Me.RadGroupBox3.Controls.Add(Me.rdbSMSMonthly)
        Me.RadGroupBox3.Controls.Add(Me.rdbSMSWeekly)
        Me.RadGroupBox3.Controls.Add(Me.rdbSMSEveryDays)
        Me.RadGroupBox3.Controls.Add(Me.dtpSMSSchedulTime)
        Me.RadGroupBox3.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox3.HeaderText = "For SMS"
        Me.RadGroupBox3.Location = New System.Drawing.Point(369, 44)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Size = New System.Drawing.Size(343, 134)
        Me.RadGroupBox3.TabIndex = 1
        Me.RadGroupBox3.Text = "For SMS"
        '
        'chkSMSLastDayOfMonth
        '
        Me.chkSMSLastDayOfMonth.AutoSize = True
        Me.chkSMSLastDayOfMonth.Location = New System.Drawing.Point(217, 81)
        Me.chkSMSLastDayOfMonth.Name = "chkSMSLastDayOfMonth"
        Me.chkSMSLastDayOfMonth.Size = New System.Drawing.Size(120, 17)
        Me.chkSMSLastDayOfMonth.TabIndex = 14
        Me.chkSMSLastDayOfMonth.Text = "Last Day of Month"
        Me.chkSMSLastDayOfMonth.UseVisualStyleBackColor = True
        '
        'cmbWeekDaysForSMS
        '
        Me.cmbWeekDaysForSMS.AutoCompleteDisplayMember = Nothing
        Me.cmbWeekDaysForSMS.AutoCompleteValueMember = Nothing
        Me.cmbWeekDaysForSMS.CalculationExpression = Nothing
        Me.cmbWeekDaysForSMS.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbWeekDaysForSMS.FieldCode = Nothing
        Me.cmbWeekDaysForSMS.FieldDesc = Nothing
        Me.cmbWeekDaysForSMS.FieldMaxLength = 0
        Me.cmbWeekDaysForSMS.FieldName = Nothing
        Me.cmbWeekDaysForSMS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbWeekDaysForSMS.isCalculatedField = False
        Me.cmbWeekDaysForSMS.IsSourceFromTable = False
        Me.cmbWeekDaysForSMS.IsSourceFromValueList = False
        Me.cmbWeekDaysForSMS.IsUnique = False
        Me.cmbWeekDaysForSMS.Location = New System.Drawing.Point(102, 60)
        Me.cmbWeekDaysForSMS.MendatroryField = False
        Me.cmbWeekDaysForSMS.MyLinkLable1 = Nothing
        Me.cmbWeekDaysForSMS.MyLinkLable2 = Nothing
        Me.cmbWeekDaysForSMS.Name = "cmbWeekDaysForSMS"
        Me.cmbWeekDaysForSMS.ReferenceFieldDesc = Nothing
        Me.cmbWeekDaysForSMS.ReferenceFieldName = Nothing
        Me.cmbWeekDaysForSMS.ReferenceTableName = Nothing
        Me.cmbWeekDaysForSMS.Size = New System.Drawing.Size(109, 18)
        Me.cmbWeekDaysForSMS.TabIndex = 366
        '
        'txtSMSMonthly
        '
        Me.txtSMSMonthly.CalculationExpression = Nothing
        Me.txtSMSMonthly.CustomFormat = "dd"
        Me.txtSMSMonthly.FieldCode = Nothing
        Me.txtSMSMonthly.FieldDesc = Nothing
        Me.txtSMSMonthly.FieldMaxLength = 0
        Me.txtSMSMonthly.FieldName = Nothing
        Me.txtSMSMonthly.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtSMSMonthly.isCalculatedField = False
        Me.txtSMSMonthly.IsSourceFromTable = False
        Me.txtSMSMonthly.IsSourceFromValueList = False
        Me.txtSMSMonthly.IsUnique = False
        Me.txtSMSMonthly.Location = New System.Drawing.Point(102, 79)
        Me.txtSMSMonthly.MaxDate = New Date(9998, 12, 28, 0, 0, 0, 0)
        Me.txtSMSMonthly.MendatroryField = False
        Me.txtSMSMonthly.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtSMSMonthly.MyLinkLable1 = Nothing
        Me.txtSMSMonthly.MyLinkLable2 = Nothing
        Me.txtSMSMonthly.Name = "txtSMSMonthly"
        Me.txtSMSMonthly.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtSMSMonthly.ReferenceFieldDesc = Nothing
        Me.txtSMSMonthly.ReferenceFieldName = Nothing
        Me.txtSMSMonthly.ReferenceTableName = Nothing
        Me.txtSMSMonthly.ShowCheckBox = True
        Me.txtSMSMonthly.ShowUpDown = True
        Me.txtSMSMonthly.Size = New System.Drawing.Size(109, 20)
        Me.txtSMSMonthly.TabIndex = 365
        Me.txtSMSMonthly.TabStop = False
        Me.txtSMSMonthly.Text = "04"
        Me.txtSMSMonthly.Value = New Date(2011, 8, 4, 11, 41, 7, 406)
        '
        'rdbSMSMonthly
        '
        Me.rdbSMSMonthly.AutoSize = True
        Me.rdbSMSMonthly.Location = New System.Drawing.Point(14, 82)
        Me.rdbSMSMonthly.Name = "rdbSMSMonthly"
        Me.rdbSMSMonthly.Size = New System.Drawing.Size(68, 17)
        Me.rdbSMSMonthly.TabIndex = 364
        Me.rdbSMSMonthly.TabStop = True
        Me.rdbSMSMonthly.Text = "Monthly"
        Me.rdbSMSMonthly.UseVisualStyleBackColor = True
        '
        'rdbSMSWeekly
        '
        Me.rdbSMSWeekly.AutoSize = True
        Me.rdbSMSWeekly.Location = New System.Drawing.Point(14, 61)
        Me.rdbSMSWeekly.Name = "rdbSMSWeekly"
        Me.rdbSMSWeekly.Size = New System.Drawing.Size(62, 17)
        Me.rdbSMSWeekly.TabIndex = 363
        Me.rdbSMSWeekly.TabStop = True
        Me.rdbSMSWeekly.Text = "Weekly"
        Me.rdbSMSWeekly.UseVisualStyleBackColor = True
        '
        'rdbSMSEveryDays
        '
        Me.rdbSMSEveryDays.AutoSize = True
        Me.rdbSMSEveryDays.Location = New System.Drawing.Point(14, 38)
        Me.rdbSMSEveryDays.Name = "rdbSMSEveryDays"
        Me.rdbSMSEveryDays.Size = New System.Drawing.Size(73, 17)
        Me.rdbSMSEveryDays.TabIndex = 362
        Me.rdbSMSEveryDays.TabStop = True
        Me.rdbSMSEveryDays.Text = "Every Day"
        Me.rdbSMSEveryDays.UseVisualStyleBackColor = True
        '
        'dtpSMSSchedulTime
        '
        Me.dtpSMSSchedulTime.CustomFormat = "hh:mm tt"
        Me.dtpSMSSchedulTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSMSSchedulTime.Location = New System.Drawing.Point(102, 103)
        Me.dtpSMSSchedulTime.Name = "dtpSMSSchedulTime"
        Me.dtpSMSSchedulTime.ShowCheckBox = True
        Me.dtpSMSSchedulTime.ShowUpDown = True
        Me.dtpSMSSchedulTime.Size = New System.Drawing.Size(109, 20)
        Me.dtpSMSSchedulTime.TabIndex = 361
        Me.dtpSMSSchedulTime.TabStop = False
        Me.dtpSMSSchedulTime.Text = "02:08 PM"
        Me.dtpSMSSchedulTime.Value = New Date(2018, 12, 11, 14, 8, 55, 115)
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(13, 104)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel2.TabIndex = 360
        Me.MyLabel2.Text = "Schedule Time "
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.rdbEmailNone)
        Me.RadGroupBox2.Controls.Add(Me.chkEmailLastDayOfMonth)
        Me.RadGroupBox2.Controls.Add(Me.txtEmailMonthly)
        Me.RadGroupBox2.Controls.Add(Me.cmbWeekDaysForEmail)
        Me.RadGroupBox2.Controls.Add(Me.rdbEmailMonthly)
        Me.RadGroupBox2.Controls.Add(Me.rdbEmailWeekly)
        Me.RadGroupBox2.Controls.Add(Me.rdbEmailEveryDays)
        Me.RadGroupBox2.Controls.Add(Me.dtpEmailSchedulTime)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.HeaderText = "For Email"
        Me.RadGroupBox2.Location = New System.Drawing.Point(10, 44)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(353, 134)
        Me.RadGroupBox2.TabIndex = 0
        Me.RadGroupBox2.Text = "For Email"
        '
        'chkEmailLastDayOfMonth
        '
        Me.chkEmailLastDayOfMonth.AutoSize = True
        Me.chkEmailLastDayOfMonth.Location = New System.Drawing.Point(225, 83)
        Me.chkEmailLastDayOfMonth.Name = "chkEmailLastDayOfMonth"
        Me.chkEmailLastDayOfMonth.Size = New System.Drawing.Size(120, 17)
        Me.chkEmailLastDayOfMonth.TabIndex = 365
        Me.chkEmailLastDayOfMonth.Text = "Last Day of Month"
        Me.chkEmailLastDayOfMonth.UseVisualStyleBackColor = True
        '
        'txtEmailMonthly
        '
        Me.txtEmailMonthly.CalculationExpression = Nothing
        Me.txtEmailMonthly.CustomFormat = "dd"
        Me.txtEmailMonthly.FieldCode = Nothing
        Me.txtEmailMonthly.FieldDesc = Nothing
        Me.txtEmailMonthly.FieldMaxLength = 0
        Me.txtEmailMonthly.FieldName = Nothing
        Me.txtEmailMonthly.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtEmailMonthly.isCalculatedField = False
        Me.txtEmailMonthly.IsSourceFromTable = False
        Me.txtEmailMonthly.IsSourceFromValueList = False
        Me.txtEmailMonthly.IsUnique = False
        Me.txtEmailMonthly.Location = New System.Drawing.Point(105, 81)
        Me.txtEmailMonthly.MaxDate = New Date(9998, 12, 28, 0, 0, 0, 0)
        Me.txtEmailMonthly.MendatroryField = False
        Me.txtEmailMonthly.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEmailMonthly.MyLinkLable1 = Nothing
        Me.txtEmailMonthly.MyLinkLable2 = Nothing
        Me.txtEmailMonthly.Name = "txtEmailMonthly"
        Me.txtEmailMonthly.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtEmailMonthly.ReferenceFieldDesc = Nothing
        Me.txtEmailMonthly.ReferenceFieldName = Nothing
        Me.txtEmailMonthly.ReferenceTableName = Nothing
        Me.txtEmailMonthly.ShowCheckBox = True
        Me.txtEmailMonthly.ShowUpDown = True
        Me.txtEmailMonthly.Size = New System.Drawing.Size(114, 20)
        Me.txtEmailMonthly.TabIndex = 364
        Me.txtEmailMonthly.TabStop = False
        Me.txtEmailMonthly.Text = "04"
        Me.txtEmailMonthly.Value = New Date(2011, 8, 4, 11, 41, 7, 406)
        '
        'cmbWeekDaysForEmail
        '
        Me.cmbWeekDaysForEmail.AutoCompleteDisplayMember = Nothing
        Me.cmbWeekDaysForEmail.AutoCompleteValueMember = Nothing
        Me.cmbWeekDaysForEmail.CalculationExpression = Nothing
        Me.cmbWeekDaysForEmail.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbWeekDaysForEmail.FieldCode = Nothing
        Me.cmbWeekDaysForEmail.FieldDesc = Nothing
        Me.cmbWeekDaysForEmail.FieldMaxLength = 0
        Me.cmbWeekDaysForEmail.FieldName = Nothing
        Me.cmbWeekDaysForEmail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbWeekDaysForEmail.isCalculatedField = False
        Me.cmbWeekDaysForEmail.IsSourceFromTable = False
        Me.cmbWeekDaysForEmail.IsSourceFromValueList = False
        Me.cmbWeekDaysForEmail.IsUnique = False
        Me.cmbWeekDaysForEmail.Location = New System.Drawing.Point(105, 60)
        Me.cmbWeekDaysForEmail.MendatroryField = False
        Me.cmbWeekDaysForEmail.MyLinkLable1 = Nothing
        Me.cmbWeekDaysForEmail.MyLinkLable2 = Nothing
        Me.cmbWeekDaysForEmail.Name = "cmbWeekDaysForEmail"
        Me.cmbWeekDaysForEmail.ReferenceFieldDesc = Nothing
        Me.cmbWeekDaysForEmail.ReferenceFieldName = Nothing
        Me.cmbWeekDaysForEmail.ReferenceTableName = Nothing
        Me.cmbWeekDaysForEmail.Size = New System.Drawing.Size(114, 18)
        Me.cmbWeekDaysForEmail.TabIndex = 364
        '
        'rdbEmailMonthly
        '
        Me.rdbEmailMonthly.AutoSize = True
        Me.rdbEmailMonthly.Location = New System.Drawing.Point(22, 84)
        Me.rdbEmailMonthly.Name = "rdbEmailMonthly"
        Me.rdbEmailMonthly.Size = New System.Drawing.Size(68, 17)
        Me.rdbEmailMonthly.TabIndex = 361
        Me.rdbEmailMonthly.TabStop = True
        Me.rdbEmailMonthly.Text = "Monthly"
        Me.rdbEmailMonthly.UseVisualStyleBackColor = True
        '
        'rdbEmailWeekly
        '
        Me.rdbEmailWeekly.AutoSize = True
        Me.rdbEmailWeekly.Location = New System.Drawing.Point(22, 63)
        Me.rdbEmailWeekly.Name = "rdbEmailWeekly"
        Me.rdbEmailWeekly.Size = New System.Drawing.Size(62, 17)
        Me.rdbEmailWeekly.TabIndex = 360
        Me.rdbEmailWeekly.TabStop = True
        Me.rdbEmailWeekly.Text = "Weekly"
        Me.rdbEmailWeekly.UseVisualStyleBackColor = True
        '
        'rdbEmailEveryDays
        '
        Me.rdbEmailEveryDays.AutoSize = True
        Me.rdbEmailEveryDays.Location = New System.Drawing.Point(22, 40)
        Me.rdbEmailEveryDays.Name = "rdbEmailEveryDays"
        Me.rdbEmailEveryDays.Size = New System.Drawing.Size(73, 17)
        Me.rdbEmailEveryDays.TabIndex = 1
        Me.rdbEmailEveryDays.TabStop = True
        Me.rdbEmailEveryDays.Text = "Every Day"
        Me.rdbEmailEveryDays.UseVisualStyleBackColor = True
        '
        'dtpEmailSchedulTime
        '
        Me.dtpEmailSchedulTime.CustomFormat = "hh:mm tt"
        Me.dtpEmailSchedulTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEmailSchedulTime.Location = New System.Drawing.Point(105, 104)
        Me.dtpEmailSchedulTime.Name = "dtpEmailSchedulTime"
        Me.dtpEmailSchedulTime.ShowCheckBox = True
        Me.dtpEmailSchedulTime.ShowUpDown = True
        Me.dtpEmailSchedulTime.Size = New System.Drawing.Size(114, 20)
        Me.dtpEmailSchedulTime.TabIndex = 359
        Me.dtpEmailSchedulTime.TabStop = False
        Me.dtpEmailSchedulTime.Text = "02:08 PM"
        Me.dtpEmailSchedulTime.Value = New Date(2018, 12, 11, 14, 8, 55, 115)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(20, 105)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(82, 18)
        Me.MyLabel1.TabIndex = 2
        Me.MyLabel1.Text = "Schedule Time "
        '
        'rbtnClose
        '
        Me.rbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnClose.Location = New System.Drawing.Point(875, 4)
        Me.rbtnClose.Name = "rbtnClose"
        Me.rbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rbtnClose.TabIndex = 4
        Me.rbtnClose.Text = "Close"
        '
        'rbtnSave
        '
        Me.rbtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSave.Location = New System.Drawing.Point(3, 4)
        Me.rbtnSave.Name = "rbtnSave"
        Me.rbtnSave.Size = New System.Drawing.Size(68, 18)
        Me.rbtnSave.TabIndex = 2
        Me.rbtnSave.Text = "Save"
        '
        'rbtnDelete
        '
        Me.rbtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnDelete.Location = New System.Drawing.Point(77, 4)
        Me.rbtnDelete.Name = "rbtnDelete"
        Me.rbtnDelete.Size = New System.Drawing.Size(68, 18)
        Me.rbtnDelete.TabIndex = 3
        Me.rbtnDelete.Text = "Delete"
        '
        'rdbEmailNone
        '
        Me.rdbEmailNone.AutoSize = True
        Me.rdbEmailNone.Location = New System.Drawing.Point(22, 17)
        Me.rdbEmailNone.Name = "rdbEmailNone"
        Me.rdbEmailNone.Size = New System.Drawing.Size(53, 17)
        Me.rdbEmailNone.TabIndex = 366
        Me.rdbEmailNone.TabStop = True
        Me.rdbEmailNone.Text = "None"
        Me.rdbEmailNone.UseVisualStyleBackColor = True
        '
        'rdbSMSNone
        '
        Me.rdbSMSNone.AutoSize = True
        Me.rdbSMSNone.Location = New System.Drawing.Point(14, 17)
        Me.rdbSMSNone.Name = "rdbSMSNone"
        Me.rdbSMSNone.Size = New System.Drawing.Size(53, 17)
        Me.rdbSMSNone.TabIndex = 367
        Me.rdbSMSNone.TabStop = True
        Me.rdbSMSNone.Text = "None"
        Me.rdbSMSNone.UseVisualStyleBackColor = True
        '
        'FrmSendSMSEmailSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 446)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSendSMSEmailSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Send SMS/Email Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rlblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.cboScreenName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.cmbWeekDaysForSMS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSMSMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpSMSSchedulTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.txtEmailMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbWeekDaysForEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEmailSchedulTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents dtpSMSSchedulTime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpEmailSchedulTime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents cboScreenName As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents rdbSMSMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSMSWeekly As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSMSEveryDays As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEmailMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEmailWeekly As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEmailEveryDays As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtSchedulerCode As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents rlblDescription As common.Controls.MyLabel
    Friend WithEvents cmbWeekDaysForEmail As common.Controls.MyComboBox
    Friend WithEvents cmbWeekDaysForSMS As common.Controls.MyComboBox
    Friend WithEvents txtSMSMonthly As common.Controls.MyDateTimePicker
    Friend WithEvents txtEmailMonthly As common.Controls.MyDateTimePicker
    Friend WithEvents chkSMSLastDayOfMonth As System.Windows.Forms.CheckBox
    Friend WithEvents chkEmailLastDayOfMonth As System.Windows.Forms.CheckBox
    Friend WithEvents rdbSMSNone As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEmailNone As System.Windows.Forms.RadioButton
End Class

