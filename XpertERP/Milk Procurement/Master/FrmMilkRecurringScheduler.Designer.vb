<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMilkRecurringScheduler
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
        Me.components = New System.ComponentModel.Container()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.MenuClose = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnexport = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnimport = New Telerik.WinControls.UI.RadMenuItem()
        Me.GrpPeriods = New System.Windows.Forms.GroupBox()
        Me.RdbYearly = New System.Windows.Forms.RadioButton()
        Me.RdbMonthly = New System.Windows.Forms.RadioButton()
        Me.RdbSemimonthly = New System.Windows.Forms.RadioButton()
        Me.RdbWeekly = New System.Windows.Forms.RadioButton()
        Me.RdbDaily = New System.Windows.Forms.RadioButton()
        Me.GrpReminder = New System.Windows.Forms.GroupBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.TxtRemindinAdvance = New common.Controls.MyTextBox()
        Me.DtpStartDate = New common.Controls.MyDateTimePicker()
        Me.cboUser = New common.Controls.MyComboBox()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.PgDaily = New Telerik.WinControls.UI.RadPageViewPage()
        Me.GrpDailyWorkDays = New System.Windows.Forms.GroupBox()
        Me.ChkDailySaturday = New common.Controls.MyCheckBox()
        Me.ChkDailyFriday = New common.Controls.MyCheckBox()
        Me.ChkDailyThursday = New common.Controls.MyCheckBox()
        Me.ChkDailyWednesday = New common.Controls.MyCheckBox()
        Me.ChkDailyTuesday = New common.Controls.MyCheckBox()
        Me.ChkDailyMonday = New common.Controls.MyCheckBox()
        Me.ChkDailySunday = New common.Controls.MyCheckBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtEveryDay = New common.MyNumBox()
        Me.RdbDailyTheseWorkDays = New System.Windows.Forms.RadioButton()
        Me.rdbDailyEvery = New System.Windows.Forms.RadioButton()
        Me.PgWeekly = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RdbWeeklyThursday = New System.Windows.Forms.RadioButton()
        Me.rdbWeeklyFriday = New System.Windows.Forms.RadioButton()
        Me.RdbweeklySaturday = New System.Windows.Forms.RadioButton()
        Me.RdbTuesday = New System.Windows.Forms.RadioButton()
        Me.RdbWeeklyWednesday = New System.Windows.Forms.RadioButton()
        Me.RdbWeeklyMonday = New System.Windows.Forms.RadioButton()
        Me.RdbWeeklySunday = New System.Windows.Forms.RadioButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtWeeklyEvery = New common.MyNumBox()
        Me.PgSemi = New Telerik.WinControls.UI.RadPageViewPage()
        Me.CboSemiMonthCmb1 = New common.Controls.MyComboBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.CboSemiMonthTheCombo = New common.Controls.MyComboBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.RdbSemiThe = New System.Windows.Forms.RadioButton()
        Me.RdbSemiFirst = New System.Windows.Forms.RadioButton()
        Me.PgMonthly = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.CboMonthlyDays = New common.Controls.MyComboBox()
        Me.CboMonthlyFirst = New common.Controls.MyComboBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.RdbMonthlyOnThe2 = New System.Windows.Forms.RadioButton()
        Me.TxtMonthlyOnthe = New common.MyNumBox()
        Me.RdbMonthlyOnTheDay1 = New System.Windows.Forms.RadioButton()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtMonthlyEvery = New common.MyNumBox()
        Me.pgYearly = New Telerik.WinControls.UI.RadPageViewPage()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.CboYearEvery = New common.Controls.MyComboBox()
        Me.CboYearDays = New common.Controls.MyComboBox()
        Me.CboYearFirst = New common.Controls.MyComboBox()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.RdbYearOnthe2 = New System.Windows.Forms.RadioButton()
        Me.TxtYearOnthe = New common.MyNumBox()
        Me.RdbYearOnThe1 = New System.Windows.Forms.RadioButton()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.PgAttachment = New Telerik.WinControls.UI.RadPageViewPage()
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment()
        Me.Pg_Users = New Telerik.WinControls.UI.RadPageViewPage()
        Me.cbguser = New common.UserControls.MyRadGridView()
        Me.lblScheduleCode = New common.Controls.MyLabel()
        Me.fndcode = New common.UserControls.txtNavigator()
        Me.LblScheduleName = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.txtdesc = New common.Controls.MyTextBox()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpPeriods.SuspendLayout()
        Me.GrpReminder.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRemindinAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.PgDaily.SuspendLayout()
        Me.GrpDailyWorkDays.SuspendLayout()
        CType(Me.ChkDailySaturday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDailyFriday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDailyThursday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDailyWednesday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDailyTuesday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDailyMonday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkDailySunday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEveryDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PgWeekly.SuspendLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtWeeklyEvery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PgSemi.SuspendLayout()
        CType(Me.CboSemiMonthCmb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboSemiMonthTheCombo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PgMonthly.SuspendLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboMonthlyDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboMonthlyFirst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMonthlyOnthe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtMonthlyEvery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pgYearly.SuspendLayout()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboYearEvery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboYearDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboYearFirst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtYearOnthe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PgAttachment.SuspendLayout()
        Me.Pg_Users.SuspendLayout()
        CType(Me.cbguser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbguser.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScheduleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblScheduleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(691, 525)
        Me.SplitContainer1.SplitterDistance = 486
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GrpPeriods)
        Me.SplitContainer2.Panel2.Controls.Add(Me.GrpReminder)
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblScheduleCode)
        Me.SplitContainer2.Panel2.Controls.Add(Me.fndcode)
        Me.SplitContainer2.Panel2.Controls.Add(Me.LblScheduleName)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtdesc)
        Me.SplitContainer2.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(685, 480)
        Me.SplitContainer2.SplitterDistance = 25
        Me.SplitContainer2.TabIndex = 0
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.MenuClose})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(685, 20)
        Me.RadMenu1.TabIndex = 12
        '
        'MenuClose
        '
        Me.MenuClose.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnexport, Me.btnimport})
        Me.MenuClose.Name = "MenuClose"
        Me.MenuClose.Text = "File"
        '
        'btnexport
        '
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Text = "Export"
        '
        'btnimport
        '
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Text = "Import"
        '
        'GrpPeriods
        '
        Me.GrpPeriods.Controls.Add(Me.RdbYearly)
        Me.GrpPeriods.Controls.Add(Me.RdbMonthly)
        Me.GrpPeriods.Controls.Add(Me.RdbSemimonthly)
        Me.GrpPeriods.Controls.Add(Me.RdbWeekly)
        Me.GrpPeriods.Controls.Add(Me.RdbDaily)
        Me.GrpPeriods.Location = New System.Drawing.Point(12, 172)
        Me.GrpPeriods.Name = "GrpPeriods"
        Me.GrpPeriods.Size = New System.Drawing.Size(154, 264)
        Me.GrpPeriods.TabIndex = 70
        Me.GrpPeriods.TabStop = False
        Me.GrpPeriods.Text = "Recurring Periods"
        '
        'RdbYearly
        '
        Me.RdbYearly.AutoSize = True
        Me.RdbYearly.Location = New System.Drawing.Point(11, 113)
        Me.RdbYearly.Name = "RdbYearly"
        Me.RdbYearly.Size = New System.Drawing.Size(53, 17)
        Me.RdbYearly.TabIndex = 4
        Me.RdbYearly.TabStop = True
        Me.RdbYearly.Text = "Yearly"
        Me.RdbYearly.UseVisualStyleBackColor = True
        '
        'RdbMonthly
        '
        Me.RdbMonthly.AutoSize = True
        Me.RdbMonthly.Location = New System.Drawing.Point(11, 90)
        Me.RdbMonthly.Name = "RdbMonthly"
        Me.RdbMonthly.Size = New System.Drawing.Size(68, 17)
        Me.RdbMonthly.TabIndex = 3
        Me.RdbMonthly.TabStop = True
        Me.RdbMonthly.Text = "Monthly"
        Me.RdbMonthly.UseVisualStyleBackColor = True
        '
        'RdbSemimonthly
        '
        Me.RdbSemimonthly.AutoSize = True
        Me.RdbSemimonthly.Location = New System.Drawing.Point(11, 67)
        Me.RdbSemimonthly.Name = "RdbSemimonthly"
        Me.RdbSemimonthly.Size = New System.Drawing.Size(96, 17)
        Me.RdbSemimonthly.TabIndex = 2
        Me.RdbSemimonthly.TabStop = True
        Me.RdbSemimonthly.Text = "Semi-Monthly"
        Me.RdbSemimonthly.UseVisualStyleBackColor = True
        '
        'RdbWeekly
        '
        Me.RdbWeekly.AutoSize = True
        Me.RdbWeekly.Location = New System.Drawing.Point(11, 44)
        Me.RdbWeekly.Name = "RdbWeekly"
        Me.RdbWeekly.Size = New System.Drawing.Size(62, 17)
        Me.RdbWeekly.TabIndex = 1
        Me.RdbWeekly.TabStop = True
        Me.RdbWeekly.Text = "Weekly"
        Me.RdbWeekly.UseVisualStyleBackColor = True
        '
        'RdbDaily
        '
        Me.RdbDaily.AutoSize = True
        Me.RdbDaily.Location = New System.Drawing.Point(11, 21)
        Me.RdbDaily.Name = "RdbDaily"
        Me.RdbDaily.Size = New System.Drawing.Size(50, 17)
        Me.RdbDaily.TabIndex = 0
        Me.RdbDaily.TabStop = True
        Me.RdbDaily.Text = "Daily"
        Me.RdbDaily.UseVisualStyleBackColor = True
        '
        'GrpReminder
        '
        Me.GrpReminder.Controls.Add(Me.MyLabel13)
        Me.GrpReminder.Controls.Add(Me.MyLabel12)
        Me.GrpReminder.Controls.Add(Me.TxtRemindinAdvance)
        Me.GrpReminder.Controls.Add(Me.DtpStartDate)
        Me.GrpReminder.Controls.Add(Me.cboUser)
        Me.GrpReminder.Controls.Add(Me.RadLabel1)
        Me.GrpReminder.Location = New System.Drawing.Point(5, 65)
        Me.GrpReminder.Name = "GrpReminder"
        Me.GrpReminder.Size = New System.Drawing.Size(667, 72)
        Me.GrpReminder.TabIndex = 69
        Me.GrpReminder.TabStop = False
        Me.GrpReminder.Text = "Reminder"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(7, 42)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel13.TabIndex = 41
        Me.MyLabel13.Text = "Start Date"
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(235, 42)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel12.TabIndex = 41
        Me.MyLabel12.Text = "Remind in Advance"
        '
        'TxtRemindinAdvance
        '
        Me.TxtRemindinAdvance.CalculationExpression = Nothing
        Me.TxtRemindinAdvance.FieldCode = Nothing
        Me.TxtRemindinAdvance.FieldDesc = Nothing
        Me.TxtRemindinAdvance.FieldMaxLength = 0
        Me.TxtRemindinAdvance.FieldName = Nothing
        Me.TxtRemindinAdvance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemindinAdvance.isCalculatedField = False
        Me.TxtRemindinAdvance.IsSourceFromTable = False
        Me.TxtRemindinAdvance.IsSourceFromValueList = False
        Me.TxtRemindinAdvance.IsUnique = False
        Me.TxtRemindinAdvance.Location = New System.Drawing.Point(357, 41)
        Me.TxtRemindinAdvance.MaxLength = 50
        Me.TxtRemindinAdvance.MendatroryField = False
        Me.TxtRemindinAdvance.MyLinkLable1 = Nothing
        Me.TxtRemindinAdvance.MyLinkLable2 = Nothing
        Me.TxtRemindinAdvance.Name = "TxtRemindinAdvance"
        Me.TxtRemindinAdvance.ReferenceFieldDesc = Nothing
        Me.TxtRemindinAdvance.ReferenceFieldName = Nothing
        Me.TxtRemindinAdvance.ReferenceTableName = Nothing
        Me.TxtRemindinAdvance.Size = New System.Drawing.Size(59, 18)
        Me.TxtRemindinAdvance.TabIndex = 127
        '
        'DtpStartDate
        '
        Me.DtpStartDate.AccessibleName = "InvDate"
        Me.DtpStartDate.CalculationExpression = Nothing
        Me.DtpStartDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpStartDate.FieldCode = Nothing
        Me.DtpStartDate.FieldDesc = Nothing
        Me.DtpStartDate.FieldMaxLength = 0
        Me.DtpStartDate.FieldName = Nothing
        Me.DtpStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpStartDate.isCalculatedField = False
        Me.DtpStartDate.IsSourceFromTable = False
        Me.DtpStartDate.IsSourceFromValueList = False
        Me.DtpStartDate.IsUnique = False
        Me.DtpStartDate.Location = New System.Drawing.Point(95, 41)
        Me.DtpStartDate.MendatroryField = False
        Me.DtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpStartDate.MyLinkLable1 = Nothing
        Me.DtpStartDate.MyLinkLable2 = Nothing
        Me.DtpStartDate.Name = "DtpStartDate"
        Me.DtpStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpStartDate.ReferenceFieldDesc = Nothing
        Me.DtpStartDate.ReferenceFieldName = Nothing
        Me.DtpStartDate.ReferenceTableName = Nothing
        Me.DtpStartDate.Size = New System.Drawing.Size(134, 18)
        Me.DtpStartDate.TabIndex = 126
        Me.DtpStartDate.TabStop = False
        Me.DtpStartDate.Text = "31/03/2012"
        Me.DtpStartDate.Value = New Date(2012, 3, 31, 0, 0, 0, 0)
        '
        'cboUser
        '
        Me.cboUser.AutoCompleteDisplayMember = Nothing
        Me.cboUser.AutoCompleteValueMember = Nothing
        Me.cboUser.CalculationExpression = Nothing
        Me.cboUser.DropDownAnimationEnabled = True
        Me.cboUser.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboUser.FieldCode = Nothing
        Me.cboUser.FieldDesc = Nothing
        Me.cboUser.FieldMaxLength = 0
        Me.cboUser.FieldName = Nothing
        Me.cboUser.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUser.isCalculatedField = False
        Me.cboUser.IsSourceFromTable = False
        Me.cboUser.IsSourceFromValueList = False
        Me.cboUser.IsUnique = False
        Me.cboUser.Location = New System.Drawing.Point(95, 18)
        Me.cboUser.MendatroryField = False
        Me.cboUser.MyLinkLable1 = Me.RadLabel1
        Me.cboUser.MyLinkLable2 = Nothing
        Me.cboUser.Name = "cboUser"
        Me.cboUser.ReferenceFieldDesc = Nothing
        Me.cboUser.ReferenceFieldName = Nothing
        Me.cboUser.ReferenceTableName = Nothing
        Me.cboUser.Size = New System.Drawing.Size(321, 18)
        Me.cboUser.TabIndex = 39
        Me.cboUser.Text = "RadDropDownList1"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(7, 20)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(62, 16)
        Me.RadLabel1.TabIndex = 40
        Me.RadLabel1.Text = "User Mode"
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.PgDaily)
        Me.RadPageView1.Controls.Add(Me.PgWeekly)
        Me.RadPageView1.Controls.Add(Me.PgSemi)
        Me.RadPageView1.Controls.Add(Me.PgMonthly)
        Me.RadPageView1.Controls.Add(Me.pgYearly)
        Me.RadPageView1.Controls.Add(Me.PgAttachment)
        Me.RadPageView1.Controls.Add(Me.Pg_Users)
        Me.RadPageView1.Location = New System.Drawing.Point(172, 147)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.PgWeekly
        Me.RadPageView1.Size = New System.Drawing.Size(510, 300)
        Me.RadPageView1.TabIndex = 0
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'PgDaily
        '
        Me.PgDaily.Controls.Add(Me.GrpDailyWorkDays)
        Me.PgDaily.Controls.Add(Me.MyLabel1)
        Me.PgDaily.Controls.Add(Me.TxtEveryDay)
        Me.PgDaily.Controls.Add(Me.RdbDailyTheseWorkDays)
        Me.PgDaily.Controls.Add(Me.rdbDailyEvery)
        Me.PgDaily.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.PgDaily.Location = New System.Drawing.Point(10, 37)
        Me.PgDaily.Name = "PgDaily"
        Me.PgDaily.Size = New System.Drawing.Size(489, 252)
        Me.PgDaily.Text = "Daily"
        '
        'GrpDailyWorkDays
        '
        Me.GrpDailyWorkDays.Controls.Add(Me.ChkDailySaturday)
        Me.GrpDailyWorkDays.Controls.Add(Me.ChkDailyFriday)
        Me.GrpDailyWorkDays.Controls.Add(Me.ChkDailyThursday)
        Me.GrpDailyWorkDays.Controls.Add(Me.ChkDailyWednesday)
        Me.GrpDailyWorkDays.Controls.Add(Me.ChkDailyTuesday)
        Me.GrpDailyWorkDays.Controls.Add(Me.ChkDailyMonday)
        Me.GrpDailyWorkDays.Controls.Add(Me.ChkDailySunday)
        Me.GrpDailyWorkDays.Location = New System.Drawing.Point(37, 75)
        Me.GrpDailyWorkDays.Name = "GrpDailyWorkDays"
        Me.GrpDailyWorkDays.Size = New System.Drawing.Size(380, 169)
        Me.GrpDailyWorkDays.TabIndex = 71
        Me.GrpDailyWorkDays.TabStop = False
        '
        'ChkDailySaturday
        '
        Me.ChkDailySaturday.Location = New System.Drawing.Point(156, 69)
        Me.ChkDailySaturday.MyLinkLable1 = Nothing
        Me.ChkDailySaturday.MyLinkLable2 = Nothing
        Me.ChkDailySaturday.Name = "ChkDailySaturday"
        Me.ChkDailySaturday.Size = New System.Drawing.Size(64, 18)
        Me.ChkDailySaturday.TabIndex = 3
        Me.ChkDailySaturday.Tag1 = Nothing
        Me.ChkDailySaturday.Text = "Saturday"
        '
        'ChkDailyFriday
        '
        Me.ChkDailyFriday.Location = New System.Drawing.Point(156, 45)
        Me.ChkDailyFriday.MyLinkLable1 = Nothing
        Me.ChkDailyFriday.MyLinkLable2 = Nothing
        Me.ChkDailyFriday.Name = "ChkDailyFriday"
        Me.ChkDailyFriday.Size = New System.Drawing.Size(50, 18)
        Me.ChkDailyFriday.TabIndex = 2
        Me.ChkDailyFriday.Tag1 = Nothing
        Me.ChkDailyFriday.Text = "Friday"
        '
        'ChkDailyThursday
        '
        Me.ChkDailyThursday.Location = New System.Drawing.Point(156, 21)
        Me.ChkDailyThursday.MyLinkLable1 = Nothing
        Me.ChkDailyThursday.MyLinkLable2 = Nothing
        Me.ChkDailyThursday.Name = "ChkDailyThursday"
        Me.ChkDailyThursday.Size = New System.Drawing.Size(66, 18)
        Me.ChkDailyThursday.TabIndex = 2
        Me.ChkDailyThursday.Tag1 = Nothing
        Me.ChkDailyThursday.Text = "Thursday"
        '
        'ChkDailyWednesday
        '
        Me.ChkDailyWednesday.Location = New System.Drawing.Point(22, 93)
        Me.ChkDailyWednesday.MyLinkLable1 = Nothing
        Me.ChkDailyWednesday.MyLinkLable2 = Nothing
        Me.ChkDailyWednesday.Name = "ChkDailyWednesday"
        Me.ChkDailyWednesday.Size = New System.Drawing.Size(78, 18)
        Me.ChkDailyWednesday.TabIndex = 2
        Me.ChkDailyWednesday.Tag1 = Nothing
        Me.ChkDailyWednesday.Text = "Wednesday"
        '
        'ChkDailyTuesday
        '
        Me.ChkDailyTuesday.Location = New System.Drawing.Point(22, 69)
        Me.ChkDailyTuesday.MyLinkLable1 = Nothing
        Me.ChkDailyTuesday.MyLinkLable2 = Nothing
        Me.ChkDailyTuesday.Name = "ChkDailyTuesday"
        Me.ChkDailyTuesday.Size = New System.Drawing.Size(61, 18)
        Me.ChkDailyTuesday.TabIndex = 2
        Me.ChkDailyTuesday.Tag1 = Nothing
        Me.ChkDailyTuesday.Text = "Tuesday"
        '
        'ChkDailyMonday
        '
        Me.ChkDailyMonday.Location = New System.Drawing.Point(22, 45)
        Me.ChkDailyMonday.MyLinkLable1 = Nothing
        Me.ChkDailyMonday.MyLinkLable2 = Nothing
        Me.ChkDailyMonday.Name = "ChkDailyMonday"
        Me.ChkDailyMonday.Size = New System.Drawing.Size(61, 18)
        Me.ChkDailyMonday.TabIndex = 2
        Me.ChkDailyMonday.Tag1 = Nothing
        Me.ChkDailyMonday.Text = "Monday"
        '
        'ChkDailySunday
        '
        Me.ChkDailySunday.Location = New System.Drawing.Point(22, 21)
        Me.ChkDailySunday.MyLinkLable1 = Nothing
        Me.ChkDailySunday.MyLinkLable2 = Nothing
        Me.ChkDailySunday.Name = "ChkDailySunday"
        Me.ChkDailySunday.Size = New System.Drawing.Size(57, 18)
        Me.ChkDailySunday.TabIndex = 1
        Me.ChkDailySunday.Tag1 = Nothing
        Me.ChkDailySunday.Text = "Sunday"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(193, 33)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(40, 16)
        Me.MyLabel1.TabIndex = 41
        Me.MyLabel1.Text = "Day(s)"
        '
        'TxtEveryDay
        '
        Me.TxtEveryDay.BackColor = System.Drawing.Color.White
        Me.TxtEveryDay.CalculationExpression = Nothing
        Me.TxtEveryDay.DecimalPlaces = 0
        Me.TxtEveryDay.FieldCode = Nothing
        Me.TxtEveryDay.FieldDesc = Nothing
        Me.TxtEveryDay.FieldMaxLength = 0
        Me.TxtEveryDay.FieldName = Nothing
        Me.TxtEveryDay.isCalculatedField = False
        Me.TxtEveryDay.IsSourceFromTable = False
        Me.TxtEveryDay.IsSourceFromValueList = False
        Me.TxtEveryDay.IsUnique = False
        Me.TxtEveryDay.Location = New System.Drawing.Point(102, 30)
        Me.TxtEveryDay.MendatroryField = False
        Me.TxtEveryDay.MyLinkLable1 = Nothing
        Me.TxtEveryDay.MyLinkLable2 = Nothing
        Me.TxtEveryDay.Name = "TxtEveryDay"
        Me.TxtEveryDay.ReferenceFieldDesc = Nothing
        Me.TxtEveryDay.ReferenceFieldName = Nothing
        Me.TxtEveryDay.ReferenceTableName = Nothing
        Me.TxtEveryDay.Size = New System.Drawing.Size(85, 20)
        Me.TxtEveryDay.TabIndex = 6
        Me.TxtEveryDay.Text = "0"
        Me.TxtEveryDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtEveryDay.Value = 0R
        '
        'RdbDailyTheseWorkDays
        '
        Me.RdbDailyTheseWorkDays.AutoSize = True
        Me.RdbDailyTheseWorkDays.Location = New System.Drawing.Point(36, 59)
        Me.RdbDailyTheseWorkDays.Name = "RdbDailyTheseWorkDays"
        Me.RdbDailyTheseWorkDays.Size = New System.Drawing.Size(113, 17)
        Me.RdbDailyTheseWorkDays.TabIndex = 3
        Me.RdbDailyTheseWorkDays.TabStop = True
        Me.RdbDailyTheseWorkDays.Text = "These Work Days"
        Me.RdbDailyTheseWorkDays.UseVisualStyleBackColor = True
        '
        'rdbDailyEvery
        '
        Me.rdbDailyEvery.AutoSize = True
        Me.rdbDailyEvery.Location = New System.Drawing.Point(37, 30)
        Me.rdbDailyEvery.Name = "rdbDailyEvery"
        Me.rdbDailyEvery.Size = New System.Drawing.Size(51, 17)
        Me.rdbDailyEvery.TabIndex = 2
        Me.rdbDailyEvery.TabStop = True
        Me.rdbDailyEvery.Text = "Every"
        Me.rdbDailyEvery.UseVisualStyleBackColor = True
        '
        'PgWeekly
        '
        Me.PgWeekly.Controls.Add(Me.MyLabel6)
        Me.PgWeekly.Controls.Add(Me.MyLabel3)
        Me.PgWeekly.Controls.Add(Me.GroupBox1)
        Me.PgWeekly.Controls.Add(Me.MyLabel2)
        Me.PgWeekly.Controls.Add(Me.TxtWeeklyEvery)
        Me.PgWeekly.ItemSize = New System.Drawing.SizeF(52.0!, 28.0!)
        Me.PgWeekly.Location = New System.Drawing.Point(10, 37)
        Me.PgWeekly.Name = "PgWeekly"
        Me.PgWeekly.Size = New System.Drawing.Size(489, 252)
        Me.PgWeekly.Text = "Weekly"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(55, 22)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel6.TabIndex = 80
        Me.MyLabel6.Text = "Every"
        Me.MyLabel6.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(55, 46)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(71, 16)
        Me.MyLabel3.TabIndex = 76
        Me.MyLabel3.Text = "Day of Week"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RdbWeeklyThursday)
        Me.GroupBox1.Controls.Add(Me.rdbWeeklyFriday)
        Me.GroupBox1.Controls.Add(Me.RdbweeklySaturday)
        Me.GroupBox1.Controls.Add(Me.RdbTuesday)
        Me.GroupBox1.Controls.Add(Me.RdbWeeklyWednesday)
        Me.GroupBox1.Controls.Add(Me.RdbWeeklyMonday)
        Me.GroupBox1.Controls.Add(Me.RdbWeeklySunday)
        Me.GroupBox1.Location = New System.Drawing.Point(55, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(380, 169)
        Me.GroupBox1.TabIndex = 76
        Me.GroupBox1.TabStop = False
        '
        'RdbWeeklyThursday
        '
        Me.RdbWeeklyThursday.AutoSize = True
        Me.RdbWeeklyThursday.Location = New System.Drawing.Point(180, 21)
        Me.RdbWeeklyThursday.Name = "RdbWeeklyThursday"
        Me.RdbWeeklyThursday.Size = New System.Drawing.Size(72, 17)
        Me.RdbWeeklyThursday.TabIndex = 79
        Me.RdbWeeklyThursday.TabStop = True
        Me.RdbWeeklyThursday.Text = "Thursday"
        Me.RdbWeeklyThursday.UseVisualStyleBackColor = True
        '
        'rdbWeeklyFriday
        '
        Me.rdbWeeklyFriday.AutoSize = True
        Me.rdbWeeklyFriday.Location = New System.Drawing.Point(180, 43)
        Me.rdbWeeklyFriday.Name = "rdbWeeklyFriday"
        Me.rdbWeeklyFriday.Size = New System.Drawing.Size(56, 17)
        Me.rdbWeeklyFriday.TabIndex = 78
        Me.rdbWeeklyFriday.TabStop = True
        Me.rdbWeeklyFriday.Text = "Friday"
        Me.rdbWeeklyFriday.UseVisualStyleBackColor = True
        '
        'RdbweeklySaturday
        '
        Me.RdbweeklySaturday.AutoSize = True
        Me.RdbweeklySaturday.Location = New System.Drawing.Point(180, 66)
        Me.RdbweeklySaturday.Name = "RdbweeklySaturday"
        Me.RdbweeklySaturday.Size = New System.Drawing.Size(70, 17)
        Me.RdbweeklySaturday.TabIndex = 77
        Me.RdbweeklySaturday.TabStop = True
        Me.RdbweeklySaturday.Text = "Saturday"
        Me.RdbweeklySaturday.UseVisualStyleBackColor = True
        '
        'RdbTuesday
        '
        Me.RdbTuesday.AutoSize = True
        Me.RdbTuesday.Location = New System.Drawing.Point(21, 66)
        Me.RdbTuesday.Name = "RdbTuesday"
        Me.RdbTuesday.Size = New System.Drawing.Size(67, 17)
        Me.RdbTuesday.TabIndex = 76
        Me.RdbTuesday.TabStop = True
        Me.RdbTuesday.Text = "Tuesday"
        Me.RdbTuesday.UseVisualStyleBackColor = True
        '
        'RdbWeeklyWednesday
        '
        Me.RdbWeeklyWednesday.AutoSize = True
        Me.RdbWeeklyWednesday.Location = New System.Drawing.Point(21, 89)
        Me.RdbWeeklyWednesday.Name = "RdbWeeklyWednesday"
        Me.RdbWeeklyWednesday.Size = New System.Drawing.Size(85, 17)
        Me.RdbWeeklyWednesday.TabIndex = 75
        Me.RdbWeeklyWednesday.TabStop = True
        Me.RdbWeeklyWednesday.Text = "Wednesday"
        Me.RdbWeeklyWednesday.UseVisualStyleBackColor = True
        '
        'RdbWeeklyMonday
        '
        Me.RdbWeeklyMonday.AutoSize = True
        Me.RdbWeeklyMonday.Location = New System.Drawing.Point(21, 43)
        Me.RdbWeeklyMonday.Name = "RdbWeeklyMonday"
        Me.RdbWeeklyMonday.Size = New System.Drawing.Size(67, 17)
        Me.RdbWeeklyMonday.TabIndex = 74
        Me.RdbWeeklyMonday.TabStop = True
        Me.RdbWeeklyMonday.Text = "Monday"
        Me.RdbWeeklyMonday.UseVisualStyleBackColor = True
        '
        'RdbWeeklySunday
        '
        Me.RdbWeeklySunday.AutoSize = True
        Me.RdbWeeklySunday.Location = New System.Drawing.Point(21, 20)
        Me.RdbWeeklySunday.Name = "RdbWeeklySunday"
        Me.RdbWeeklySunday.Size = New System.Drawing.Size(63, 17)
        Me.RdbWeeklySunday.TabIndex = 73
        Me.RdbWeeklySunday.TabStop = True
        Me.RdbWeeklySunday.Text = "Sunday"
        Me.RdbWeeklySunday.UseVisualStyleBackColor = True
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(211, 22)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel2.TabIndex = 75
        Me.MyLabel2.Text = "Week(s)"
        Me.MyLabel2.Visible = False
        '
        'TxtWeeklyEvery
        '
        Me.TxtWeeklyEvery.BackColor = System.Drawing.Color.White
        Me.TxtWeeklyEvery.CalculationExpression = Nothing
        Me.TxtWeeklyEvery.DecimalPlaces = 0
        Me.TxtWeeklyEvery.FieldCode = Nothing
        Me.TxtWeeklyEvery.FieldDesc = Nothing
        Me.TxtWeeklyEvery.FieldMaxLength = 0
        Me.TxtWeeklyEvery.FieldName = Nothing
        Me.TxtWeeklyEvery.isCalculatedField = False
        Me.TxtWeeklyEvery.IsSourceFromTable = False
        Me.TxtWeeklyEvery.IsSourceFromValueList = False
        Me.TxtWeeklyEvery.IsUnique = False
        Me.TxtWeeklyEvery.Location = New System.Drawing.Point(120, 19)
        Me.TxtWeeklyEvery.MendatroryField = False
        Me.TxtWeeklyEvery.MyLinkLable1 = Nothing
        Me.TxtWeeklyEvery.MyLinkLable2 = Nothing
        Me.TxtWeeklyEvery.Name = "TxtWeeklyEvery"
        Me.TxtWeeklyEvery.ReferenceFieldDesc = Nothing
        Me.TxtWeeklyEvery.ReferenceFieldName = Nothing
        Me.TxtWeeklyEvery.ReferenceTableName = Nothing
        Me.TxtWeeklyEvery.Size = New System.Drawing.Size(85, 20)
        Me.TxtWeeklyEvery.TabIndex = 74
        Me.TxtWeeklyEvery.Text = "0"
        Me.TxtWeeklyEvery.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtWeeklyEvery.Value = 0R
        Me.TxtWeeklyEvery.Visible = False
        '
        'PgSemi
        '
        Me.PgSemi.Controls.Add(Me.CboSemiMonthCmb1)
        Me.PgSemi.Controls.Add(Me.MyLabel9)
        Me.PgSemi.Controls.Add(Me.CboSemiMonthTheCombo)
        Me.PgSemi.Controls.Add(Me.MyLabel8)
        Me.PgSemi.Controls.Add(Me.RdbSemiThe)
        Me.PgSemi.Controls.Add(Me.RdbSemiFirst)
        Me.PgSemi.ItemSize = New System.Drawing.SizeF(85.0!, 28.0!)
        Me.PgSemi.Location = New System.Drawing.Point(10, 37)
        Me.PgSemi.Name = "PgSemi"
        Me.PgSemi.Size = New System.Drawing.Size(489, 252)
        Me.PgSemi.Text = "Semi Monthly"
        '
        'CboSemiMonthCmb1
        '
        Me.CboSemiMonthCmb1.AutoCompleteDisplayMember = Nothing
        Me.CboSemiMonthCmb1.AutoCompleteValueMember = Nothing
        Me.CboSemiMonthCmb1.CalculationExpression = Nothing
        Me.CboSemiMonthCmb1.DropDownAnimationEnabled = True
        Me.CboSemiMonthCmb1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboSemiMonthCmb1.FieldCode = Nothing
        Me.CboSemiMonthCmb1.FieldDesc = Nothing
        Me.CboSemiMonthCmb1.FieldMaxLength = 0
        Me.CboSemiMonthCmb1.FieldName = Nothing
        Me.CboSemiMonthCmb1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSemiMonthCmb1.isCalculatedField = False
        Me.CboSemiMonthCmb1.IsSourceFromTable = False
        Me.CboSemiMonthCmb1.IsSourceFromValueList = False
        Me.CboSemiMonthCmb1.IsUnique = False
        Me.CboSemiMonthCmb1.Location = New System.Drawing.Point(140, 34)
        Me.CboSemiMonthCmb1.MendatroryField = False
        Me.CboSemiMonthCmb1.MyLinkLable1 = Me.RadLabel1
        Me.CboSemiMonthCmb1.MyLinkLable2 = Nothing
        Me.CboSemiMonthCmb1.Name = "CboSemiMonthCmb1"
        Me.CboSemiMonthCmb1.ReferenceFieldDesc = Nothing
        Me.CboSemiMonthCmb1.ReferenceFieldName = Nothing
        Me.CboSemiMonthCmb1.ReferenceTableName = Nothing
        Me.CboSemiMonthCmb1.Size = New System.Drawing.Size(85, 18)
        Me.CboSemiMonthCmb1.TabIndex = 88
        Me.CboSemiMonthCmb1.Text = "RadDropDownList1"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(235, 60)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(140, 16)
        Me.MyLabel9.TabIndex = 86
        Me.MyLabel9.Text = "And Last Day of the Month"
        '
        'CboSemiMonthTheCombo
        '
        Me.CboSemiMonthTheCombo.AutoCompleteDisplayMember = Nothing
        Me.CboSemiMonthTheCombo.AutoCompleteValueMember = Nothing
        Me.CboSemiMonthTheCombo.CalculationExpression = Nothing
        Me.CboSemiMonthTheCombo.DropDownAnimationEnabled = True
        Me.CboSemiMonthTheCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboSemiMonthTheCombo.FieldCode = Nothing
        Me.CboSemiMonthTheCombo.FieldDesc = Nothing
        Me.CboSemiMonthTheCombo.FieldMaxLength = 0
        Me.CboSemiMonthTheCombo.FieldName = Nothing
        Me.CboSemiMonthTheCombo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSemiMonthTheCombo.isCalculatedField = False
        Me.CboSemiMonthTheCombo.IsSourceFromTable = False
        Me.CboSemiMonthTheCombo.IsSourceFromValueList = False
        Me.CboSemiMonthTheCombo.IsUnique = False
        Me.CboSemiMonthTheCombo.Location = New System.Drawing.Point(140, 59)
        Me.CboSemiMonthTheCombo.MendatroryField = False
        Me.CboSemiMonthTheCombo.MyLinkLable1 = Me.RadLabel1
        Me.CboSemiMonthTheCombo.MyLinkLable2 = Nothing
        Me.CboSemiMonthTheCombo.Name = "CboSemiMonthTheCombo"
        Me.CboSemiMonthTheCombo.ReferenceFieldDesc = Nothing
        Me.CboSemiMonthTheCombo.ReferenceFieldName = Nothing
        Me.CboSemiMonthTheCombo.ReferenceTableName = Nothing
        Me.CboSemiMonthTheCombo.Size = New System.Drawing.Size(85, 18)
        Me.CboSemiMonthTheCombo.TabIndex = 87
        Me.CboSemiMonthTheCombo.Text = "RadDropDownList1"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(235, 35)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel8.TabIndex = 85
        Me.MyLabel8.Text = "Day of the Month"
        '
        'RdbSemiThe
        '
        Me.RdbSemiThe.AutoSize = True
        Me.RdbSemiThe.Location = New System.Drawing.Point(50, 60)
        Me.RdbSemiThe.Name = "RdbSemiThe"
        Me.RdbSemiThe.Size = New System.Drawing.Size(44, 17)
        Me.RdbSemiThe.TabIndex = 86
        Me.RdbSemiThe.TabStop = True
        Me.RdbSemiThe.Text = "The"
        Me.RdbSemiThe.UseVisualStyleBackColor = True
        '
        'RdbSemiFirst
        '
        Me.RdbSemiFirst.AutoSize = True
        Me.RdbSemiFirst.Location = New System.Drawing.Point(50, 35)
        Me.RdbSemiFirst.Name = "RdbSemiFirst"
        Me.RdbSemiFirst.Size = New System.Drawing.Size(70, 17)
        Me.RdbSemiFirst.TabIndex = 84
        Me.RdbSemiFirst.TabStop = True
        Me.RdbSemiFirst.Text = "First and"
        Me.RdbSemiFirst.UseVisualStyleBackColor = True
        '
        'PgMonthly
        '
        Me.PgMonthly.Controls.Add(Me.MyLabel14)
        Me.PgMonthly.Controls.Add(Me.CboMonthlyDays)
        Me.PgMonthly.Controls.Add(Me.CboMonthlyFirst)
        Me.PgMonthly.Controls.Add(Me.MyLabel7)
        Me.PgMonthly.Controls.Add(Me.RdbMonthlyOnThe2)
        Me.PgMonthly.Controls.Add(Me.TxtMonthlyOnthe)
        Me.PgMonthly.Controls.Add(Me.RdbMonthlyOnTheDay1)
        Me.PgMonthly.Controls.Add(Me.MyLabel5)
        Me.PgMonthly.Controls.Add(Me.MyLabel4)
        Me.PgMonthly.Controls.Add(Me.TxtMonthlyEvery)
        Me.PgMonthly.ItemSize = New System.Drawing.SizeF(58.0!, 28.0!)
        Me.PgMonthly.Location = New System.Drawing.Point(10, 37)
        Me.PgMonthly.Name = "PgMonthly"
        Me.PgMonthly.Size = New System.Drawing.Size(489, 252)
        Me.PgMonthly.Text = "Monthly"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(217, 24)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(92, 16)
        Me.MyLabel14.TabIndex = 86
        Me.MyLabel14.Text = "Day of the Month"
        '
        'CboMonthlyDays
        '
        Me.CboMonthlyDays.AutoCompleteDisplayMember = Nothing
        Me.CboMonthlyDays.AutoCompleteValueMember = Nothing
        Me.CboMonthlyDays.CalculationExpression = Nothing
        Me.CboMonthlyDays.DropDownAnimationEnabled = True
        Me.CboMonthlyDays.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboMonthlyDays.FieldCode = Nothing
        Me.CboMonthlyDays.FieldDesc = Nothing
        Me.CboMonthlyDays.FieldMaxLength = 0
        Me.CboMonthlyDays.FieldName = Nothing
        Me.CboMonthlyDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboMonthlyDays.isCalculatedField = False
        Me.CboMonthlyDays.IsSourceFromTable = False
        Me.CboMonthlyDays.IsSourceFromValueList = False
        Me.CboMonthlyDays.IsUnique = False
        Me.CboMonthlyDays.Location = New System.Drawing.Point(126, 130)
        Me.CboMonthlyDays.MendatroryField = False
        Me.CboMonthlyDays.MyLinkLable1 = Me.RadLabel1
        Me.CboMonthlyDays.MyLinkLable2 = Nothing
        Me.CboMonthlyDays.Name = "CboMonthlyDays"
        Me.CboMonthlyDays.ReferenceFieldDesc = Nothing
        Me.CboMonthlyDays.ReferenceFieldName = Nothing
        Me.CboMonthlyDays.ReferenceTableName = Nothing
        Me.CboMonthlyDays.Size = New System.Drawing.Size(152, 18)
        Me.CboMonthlyDays.TabIndex = 83
        Me.CboMonthlyDays.Text = "RadDropDownList1"
        Me.CboMonthlyDays.Visible = False
        '
        'CboMonthlyFirst
        '
        Me.CboMonthlyFirst.AutoCompleteDisplayMember = Nothing
        Me.CboMonthlyFirst.AutoCompleteValueMember = Nothing
        Me.CboMonthlyFirst.CalculationExpression = Nothing
        Me.CboMonthlyFirst.DropDownAnimationEnabled = True
        Me.CboMonthlyFirst.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboMonthlyFirst.FieldCode = Nothing
        Me.CboMonthlyFirst.FieldDesc = Nothing
        Me.CboMonthlyFirst.FieldMaxLength = 0
        Me.CboMonthlyFirst.FieldName = Nothing
        Me.CboMonthlyFirst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboMonthlyFirst.isCalculatedField = False
        Me.CboMonthlyFirst.IsSourceFromTable = False
        Me.CboMonthlyFirst.IsSourceFromValueList = False
        Me.CboMonthlyFirst.IsUnique = False
        Me.CboMonthlyFirst.Location = New System.Drawing.Point(94, 23)
        Me.CboMonthlyFirst.MendatroryField = False
        Me.CboMonthlyFirst.MyLinkLable1 = Me.RadLabel1
        Me.CboMonthlyFirst.MyLinkLable2 = Nothing
        Me.CboMonthlyFirst.Name = "CboMonthlyFirst"
        Me.CboMonthlyFirst.ReferenceFieldDesc = Nothing
        Me.CboMonthlyFirst.ReferenceFieldName = Nothing
        Me.CboMonthlyFirst.ReferenceTableName = Nothing
        Me.CboMonthlyFirst.Size = New System.Drawing.Size(117, 18)
        Me.CboMonthlyFirst.TabIndex = 82
        Me.CboMonthlyFirst.Text = "RadDropDownList1"
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(228, 190)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel7.TabIndex = 80
        Me.MyLabel7.Text = "Day"
        Me.MyLabel7.Visible = False
        '
        'RdbMonthlyOnThe2
        '
        Me.RdbMonthlyOnThe2.AutoSize = True
        Me.RdbMonthlyOnThe2.Location = New System.Drawing.Point(26, 24)
        Me.RdbMonthlyOnThe2.Name = "RdbMonthlyOnThe2"
        Me.RdbMonthlyOnThe2.Size = New System.Drawing.Size(63, 17)
        Me.RdbMonthlyOnThe2.TabIndex = 81
        Me.RdbMonthlyOnThe2.TabStop = True
        Me.RdbMonthlyOnThe2.Text = "On The"
        Me.RdbMonthlyOnThe2.UseVisualStyleBackColor = True
        '
        'TxtMonthlyOnthe
        '
        Me.TxtMonthlyOnthe.BackColor = System.Drawing.Color.White
        Me.TxtMonthlyOnthe.CalculationExpression = Nothing
        Me.TxtMonthlyOnthe.DecimalPlaces = 0
        Me.TxtMonthlyOnthe.FieldCode = Nothing
        Me.TxtMonthlyOnthe.FieldDesc = Nothing
        Me.TxtMonthlyOnthe.FieldMaxLength = 0
        Me.TxtMonthlyOnthe.FieldName = Nothing
        Me.TxtMonthlyOnthe.isCalculatedField = False
        Me.TxtMonthlyOnthe.IsSourceFromTable = False
        Me.TxtMonthlyOnthe.IsSourceFromValueList = False
        Me.TxtMonthlyOnthe.IsUnique = False
        Me.TxtMonthlyOnthe.Location = New System.Drawing.Point(137, 188)
        Me.TxtMonthlyOnthe.MendatroryField = False
        Me.TxtMonthlyOnthe.MyLinkLable1 = Nothing
        Me.TxtMonthlyOnthe.MyLinkLable2 = Nothing
        Me.TxtMonthlyOnthe.Name = "TxtMonthlyOnthe"
        Me.TxtMonthlyOnthe.ReferenceFieldDesc = Nothing
        Me.TxtMonthlyOnthe.ReferenceFieldName = Nothing
        Me.TxtMonthlyOnthe.ReferenceTableName = Nothing
        Me.TxtMonthlyOnthe.Size = New System.Drawing.Size(85, 20)
        Me.TxtMonthlyOnthe.TabIndex = 79
        Me.TxtMonthlyOnthe.Text = "0"
        Me.TxtMonthlyOnthe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMonthlyOnthe.Value = 0R
        Me.TxtMonthlyOnthe.Visible = False
        '
        'RdbMonthlyOnTheDay1
        '
        Me.RdbMonthlyOnTheDay1.AutoSize = True
        Me.RdbMonthlyOnTheDay1.Location = New System.Drawing.Point(69, 190)
        Me.RdbMonthlyOnTheDay1.Name = "RdbMonthlyOnTheDay1"
        Me.RdbMonthlyOnTheDay1.Size = New System.Drawing.Size(63, 17)
        Me.RdbMonthlyOnTheDay1.TabIndex = 80
        Me.RdbMonthlyOnTheDay1.TabStop = True
        Me.RdbMonthlyOnTheDay1.Text = "On The"
        Me.RdbMonthlyOnTheDay1.UseVisualStyleBackColor = True
        Me.RdbMonthlyOnTheDay1.Visible = False
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(69, 156)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel5.TabIndex = 79
        Me.MyLabel5.Text = "Every"
        Me.MyLabel5.Visible = False
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(217, 156)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel4.TabIndex = 78
        Me.MyLabel4.Text = "Month(s)"
        Me.MyLabel4.Visible = False
        '
        'TxtMonthlyEvery
        '
        Me.TxtMonthlyEvery.BackColor = System.Drawing.Color.White
        Me.TxtMonthlyEvery.CalculationExpression = Nothing
        Me.TxtMonthlyEvery.DecimalPlaces = 0
        Me.TxtMonthlyEvery.FieldCode = Nothing
        Me.TxtMonthlyEvery.FieldDesc = Nothing
        Me.TxtMonthlyEvery.FieldMaxLength = 0
        Me.TxtMonthlyEvery.FieldName = Nothing
        Me.TxtMonthlyEvery.isCalculatedField = False
        Me.TxtMonthlyEvery.IsSourceFromTable = False
        Me.TxtMonthlyEvery.IsSourceFromValueList = False
        Me.TxtMonthlyEvery.IsUnique = False
        Me.TxtMonthlyEvery.Location = New System.Drawing.Point(126, 154)
        Me.TxtMonthlyEvery.MendatroryField = False
        Me.TxtMonthlyEvery.MyLinkLable1 = Nothing
        Me.TxtMonthlyEvery.MyLinkLable2 = Nothing
        Me.TxtMonthlyEvery.Name = "TxtMonthlyEvery"
        Me.TxtMonthlyEvery.ReferenceFieldDesc = Nothing
        Me.TxtMonthlyEvery.ReferenceFieldName = Nothing
        Me.TxtMonthlyEvery.ReferenceTableName = Nothing
        Me.TxtMonthlyEvery.Size = New System.Drawing.Size(85, 20)
        Me.TxtMonthlyEvery.TabIndex = 77
        Me.TxtMonthlyEvery.Text = "0"
        Me.TxtMonthlyEvery.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtMonthlyEvery.Value = 0R
        Me.TxtMonthlyEvery.Visible = False
        '
        'pgYearly
        '
        Me.pgYearly.Controls.Add(Me.MyLabel15)
        Me.pgYearly.Controls.Add(Me.CboYearEvery)
        Me.pgYearly.Controls.Add(Me.CboYearDays)
        Me.pgYearly.Controls.Add(Me.CboYearFirst)
        Me.pgYearly.Controls.Add(Me.MyLabel10)
        Me.pgYearly.Controls.Add(Me.RdbYearOnthe2)
        Me.pgYearly.Controls.Add(Me.TxtYearOnthe)
        Me.pgYearly.Controls.Add(Me.RdbYearOnThe1)
        Me.pgYearly.Controls.Add(Me.MyLabel11)
        Me.pgYearly.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.pgYearly.Location = New System.Drawing.Point(10, 37)
        Me.pgYearly.Name = "pgYearly"
        Me.pgYearly.Size = New System.Drawing.Size(489, 252)
        Me.pgYearly.Text = "Yearly"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(209, 23)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(151, 16)
        Me.MyLabel15.TabIndex = 94
        Me.MyLabel15.Text = "Day of the Month of the Year"
        '
        'CboYearEvery
        '
        Me.CboYearEvery.AutoCompleteDisplayMember = Nothing
        Me.CboYearEvery.AutoCompleteValueMember = Nothing
        Me.CboYearEvery.CalculationExpression = Nothing
        Me.CboYearEvery.DropDownAnimationEnabled = True
        Me.CboYearEvery.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboYearEvery.FieldCode = Nothing
        Me.CboYearEvery.FieldDesc = Nothing
        Me.CboYearEvery.FieldMaxLength = 0
        Me.CboYearEvery.FieldName = Nothing
        Me.CboYearEvery.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboYearEvery.isCalculatedField = False
        Me.CboYearEvery.IsSourceFromTable = False
        Me.CboYearEvery.IsSourceFromValueList = False
        Me.CboYearEvery.IsUnique = False
        Me.CboYearEvery.Location = New System.Drawing.Point(118, 150)
        Me.CboYearEvery.MendatroryField = False
        Me.CboYearEvery.MyLinkLable1 = Me.RadLabel1
        Me.CboYearEvery.MyLinkLable2 = Nothing
        Me.CboYearEvery.Name = "CboYearEvery"
        Me.CboYearEvery.ReferenceFieldDesc = Nothing
        Me.CboYearEvery.ReferenceFieldName = Nothing
        Me.CboYearEvery.ReferenceTableName = Nothing
        Me.CboYearEvery.Size = New System.Drawing.Size(117, 18)
        Me.CboYearEvery.TabIndex = 93
        Me.CboYearEvery.Text = "RadDropDownList1"
        Me.CboYearEvery.Visible = False
        '
        'CboYearDays
        '
        Me.CboYearDays.AutoCompleteDisplayMember = Nothing
        Me.CboYearDays.AutoCompleteValueMember = Nothing
        Me.CboYearDays.CalculationExpression = Nothing
        Me.CboYearDays.DropDownAnimationEnabled = True
        Me.CboYearDays.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboYearDays.FieldCode = Nothing
        Me.CboYearDays.FieldDesc = Nothing
        Me.CboYearDays.FieldMaxLength = 0
        Me.CboYearDays.FieldName = Nothing
        Me.CboYearDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboYearDays.isCalculatedField = False
        Me.CboYearDays.IsSourceFromTable = False
        Me.CboYearDays.IsSourceFromValueList = False
        Me.CboYearDays.IsUnique = False
        Me.CboYearDays.Location = New System.Drawing.Point(118, 126)
        Me.CboYearDays.MendatroryField = False
        Me.CboYearDays.MyLinkLable1 = Me.RadLabel1
        Me.CboYearDays.MyLinkLable2 = Nothing
        Me.CboYearDays.Name = "CboYearDays"
        Me.CboYearDays.ReferenceFieldDesc = Nothing
        Me.CboYearDays.ReferenceFieldName = Nothing
        Me.CboYearDays.ReferenceTableName = Nothing
        Me.CboYearDays.Size = New System.Drawing.Size(152, 18)
        Me.CboYearDays.TabIndex = 92
        Me.CboYearDays.Text = "RadDropDownList1"
        Me.CboYearDays.Visible = False
        '
        'CboYearFirst
        '
        Me.CboYearFirst.AutoCompleteDisplayMember = Nothing
        Me.CboYearFirst.AutoCompleteValueMember = Nothing
        Me.CboYearFirst.CalculationExpression = Nothing
        Me.CboYearFirst.DropDownAnimationEnabled = True
        Me.CboYearFirst.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboYearFirst.FieldCode = Nothing
        Me.CboYearFirst.FieldDesc = Nothing
        Me.CboYearFirst.FieldMaxLength = 0
        Me.CboYearFirst.FieldName = Nothing
        Me.CboYearFirst.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboYearFirst.isCalculatedField = False
        Me.CboYearFirst.IsSourceFromTable = False
        Me.CboYearFirst.IsSourceFromValueList = False
        Me.CboYearFirst.IsUnique = False
        Me.CboYearFirst.Location = New System.Drawing.Point(86, 23)
        Me.CboYearFirst.MendatroryField = False
        Me.CboYearFirst.MyLinkLable1 = Me.RadLabel1
        Me.CboYearFirst.MyLinkLable2 = Nothing
        Me.CboYearFirst.Name = "CboYearFirst"
        Me.CboYearFirst.ReferenceFieldDesc = Nothing
        Me.CboYearFirst.ReferenceFieldName = Nothing
        Me.CboYearFirst.ReferenceTableName = Nothing
        Me.CboYearFirst.Size = New System.Drawing.Size(117, 18)
        Me.CboYearFirst.TabIndex = 91
        Me.CboYearFirst.Text = "RadDropDownList1"
        '
        'MyLabel10
        '
        Me.MyLabel10.FieldName = Nothing
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(209, 184)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(26, 16)
        Me.MyLabel10.TabIndex = 89
        Me.MyLabel10.Text = "Day"
        Me.MyLabel10.Visible = False
        '
        'RdbYearOnthe2
        '
        Me.RdbYearOnthe2.AutoSize = True
        Me.RdbYearOnthe2.Location = New System.Drawing.Point(18, 23)
        Me.RdbYearOnthe2.Name = "RdbYearOnthe2"
        Me.RdbYearOnthe2.Size = New System.Drawing.Size(63, 17)
        Me.RdbYearOnthe2.TabIndex = 90
        Me.RdbYearOnthe2.TabStop = True
        Me.RdbYearOnthe2.Text = "On The"
        Me.RdbYearOnthe2.UseVisualStyleBackColor = True
        '
        'TxtYearOnthe
        '
        Me.TxtYearOnthe.BackColor = System.Drawing.Color.White
        Me.TxtYearOnthe.CalculationExpression = Nothing
        Me.TxtYearOnthe.DecimalPlaces = 0
        Me.TxtYearOnthe.FieldCode = Nothing
        Me.TxtYearOnthe.FieldDesc = Nothing
        Me.TxtYearOnthe.FieldMaxLength = 0
        Me.TxtYearOnthe.FieldName = Nothing
        Me.TxtYearOnthe.isCalculatedField = False
        Me.TxtYearOnthe.IsSourceFromTable = False
        Me.TxtYearOnthe.IsSourceFromValueList = False
        Me.TxtYearOnthe.IsUnique = False
        Me.TxtYearOnthe.Location = New System.Drawing.Point(118, 182)
        Me.TxtYearOnthe.MendatroryField = False
        Me.TxtYearOnthe.MyLinkLable1 = Nothing
        Me.TxtYearOnthe.MyLinkLable2 = Nothing
        Me.TxtYearOnthe.Name = "TxtYearOnthe"
        Me.TxtYearOnthe.ReferenceFieldDesc = Nothing
        Me.TxtYearOnthe.ReferenceFieldName = Nothing
        Me.TxtYearOnthe.ReferenceTableName = Nothing
        Me.TxtYearOnthe.Size = New System.Drawing.Size(85, 20)
        Me.TxtYearOnthe.TabIndex = 86
        Me.TxtYearOnthe.Text = "0"
        Me.TxtYearOnthe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtYearOnthe.Value = 0R
        Me.TxtYearOnthe.Visible = False
        '
        'RdbYearOnThe1
        '
        Me.RdbYearOnThe1.AutoSize = True
        Me.RdbYearOnThe1.Location = New System.Drawing.Point(50, 184)
        Me.RdbYearOnThe1.Name = "RdbYearOnThe1"
        Me.RdbYearOnThe1.Size = New System.Drawing.Size(63, 17)
        Me.RdbYearOnThe1.TabIndex = 88
        Me.RdbYearOnThe1.TabStop = True
        Me.RdbYearOnThe1.Text = "On The"
        Me.RdbYearOnThe1.UseVisualStyleBackColor = True
        Me.RdbYearOnThe1.Visible = False
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(50, 152)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(35, 16)
        Me.MyLabel11.TabIndex = 87
        Me.MyLabel11.Text = "Every"
        Me.MyLabel11.Visible = False
        '
        'PgAttachment
        '
        Me.PgAttachment.Controls.Add(Me.UcAttachment1)
        Me.PgAttachment.ItemSize = New System.Drawing.SizeF(75.0!, 28.0!)
        Me.PgAttachment.Location = New System.Drawing.Point(10, 37)
        Me.PgAttachment.Name = "PgAttachment"
        Me.PgAttachment.Size = New System.Drawing.Size(489, 252)
        Me.PgAttachment.Text = "Attachment"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(489, 252)
        Me.UcAttachment1.TabIndex = 4
        '
        'Pg_Users
        '
        Me.Pg_Users.Controls.Add(Me.cbguser)
        Me.Pg_Users.ItemSize = New System.Drawing.SizeF(39.0!, 28.0!)
        Me.Pg_Users.Location = New System.Drawing.Point(10, 37)
        Me.Pg_Users.Name = "Pg_Users"
        Me.Pg_Users.Size = New System.Drawing.Size(489, 252)
        Me.Pg_Users.Text = "User"
        '
        'cbguser
        '
        Me.cbguser.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.cbguser.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.cbguser.MasterTemplate.ShowHeaderCellButtons = True
        Me.cbguser.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.cbguser.MyStopExport = False
        Me.cbguser.Name = "cbguser"
        Me.cbguser.ShowHeaderCellButtons = True
        Me.cbguser.Size = New System.Drawing.Size(489, 252)
        Me.cbguser.TabIndex = 2
        '
        'lblScheduleCode
        '
        Me.lblScheduleCode.FieldName = Nothing
        Me.lblScheduleCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblScheduleCode.Location = New System.Drawing.Point(10, 16)
        Me.lblScheduleCode.Name = "lblScheduleCode"
        Me.lblScheduleCode.Size = New System.Drawing.Size(84, 16)
        Me.lblScheduleCode.TabIndex = 67
        Me.lblScheduleCode.Text = "Schedule Code"
        '
        'fndcode
        '
        Me.fndcode.FieldName = Nothing
        Me.fndcode.Location = New System.Drawing.Point(110, 13)
        Me.fndcode.MendatroryField = True
        Me.fndcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.fndcode.MyLinkLable1 = Me.lblScheduleCode
        Me.fndcode.MyLinkLable2 = Nothing
        Me.fndcode.MyMaxLength = 30
        Me.fndcode.MyReadOnly = False
        Me.fndcode.Name = "fndcode"
        Me.fndcode.Size = New System.Drawing.Size(307, 21)
        Me.fndcode.TabIndex = 4
        Me.fndcode.TabStop = False
        Me.fndcode.Value = ""
        '
        'LblScheduleName
        '
        Me.LblScheduleName.FieldName = Nothing
        Me.LblScheduleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblScheduleName.Location = New System.Drawing.Point(10, 42)
        Me.LblScheduleName.Name = "LblScheduleName"
        Me.LblScheduleName.Size = New System.Drawing.Size(63, 16)
        Me.LblScheduleName.TabIndex = 68
        Me.LblScheduleName.Text = "Description"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(417, 14)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 0
        '
        'txtdesc
        '
        Me.txtdesc.CalculationExpression = Nothing
        Me.txtdesc.FieldCode = Nothing
        Me.txtdesc.FieldDesc = Nothing
        Me.txtdesc.FieldMaxLength = 0
        Me.txtdesc.FieldName = Nothing
        Me.txtdesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.isCalculatedField = False
        Me.txtdesc.IsSourceFromTable = False
        Me.txtdesc.IsSourceFromValueList = False
        Me.txtdesc.IsUnique = False
        Me.txtdesc.Location = New System.Drawing.Point(111, 41)
        Me.txtdesc.MaxLength = 150
        Me.txtdesc.MendatroryField = True
        Me.txtdesc.MyLinkLable1 = Me.LblScheduleName
        Me.txtdesc.MyLinkLable2 = Nothing
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReferenceFieldDesc = Nothing
        Me.txtdesc.ReferenceFieldName = Nothing
        Me.txtdesc.ReferenceTableName = Nothing
        Me.txtdesc.Size = New System.Drawing.Size(321, 18)
        Me.txtdesc.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(607, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Location = New System.Drawing.Point(88, 9)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(73, 20)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(7, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning
        Me.NotifyIcon1.BalloonTipText = "See This"
        Me.NotifyIcon1.BalloonTipTitle = "r u seeing"
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'FrmMilkRecurringScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 525)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMilkRecurringScheduler"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmMilkRouteMaster"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpPeriods.ResumeLayout(False)
        Me.GrpPeriods.PerformLayout()
        Me.GrpReminder.ResumeLayout(False)
        Me.GrpReminder.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRemindinAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.PgDaily.ResumeLayout(False)
        Me.PgDaily.PerformLayout()
        Me.GrpDailyWorkDays.ResumeLayout(False)
        Me.GrpDailyWorkDays.PerformLayout()
        CType(Me.ChkDailySaturday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDailyFriday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDailyThursday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDailyWednesday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDailyTuesday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDailyMonday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkDailySunday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEveryDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PgWeekly.ResumeLayout(False)
        Me.PgWeekly.PerformLayout()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtWeeklyEvery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PgSemi.ResumeLayout(False)
        Me.PgSemi.PerformLayout()
        CType(Me.CboSemiMonthCmb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboSemiMonthTheCombo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PgMonthly.ResumeLayout(False)
        Me.PgMonthly.PerformLayout()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboMonthlyDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboMonthlyFirst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMonthlyOnthe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtMonthlyEvery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pgYearly.ResumeLayout(False)
        Me.pgYearly.PerformLayout()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboYearEvery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboYearDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboYearFirst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtYearOnthe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PgAttachment.ResumeLayout(False)
        Me.Pg_Users.ResumeLayout(False)
        CType(Me.cbguser.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbguser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScheduleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblScheduleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents MenuClose As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents PgDaily As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents PgAttachment As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents lblScheduleCode As common.Controls.MyLabel
    Friend WithEvents txtdesc As common.Controls.MyTextBox
    Friend WithEvents LblScheduleName As common.Controls.MyLabel
    Friend WithEvents fndcode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnexport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnimport As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PgWeekly As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GrpReminder As System.Windows.Forms.GroupBox
    Friend WithEvents cboUser As common.Controls.MyComboBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents PgSemi As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents PgMonthly As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pgYearly As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents GrpPeriods As System.Windows.Forms.GroupBox
    Friend WithEvents RdbDaily As System.Windows.Forms.RadioButton
    Friend WithEvents RdbYearly As System.Windows.Forms.RadioButton
    Friend WithEvents RdbMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents RdbSemimonthly As System.Windows.Forms.RadioButton
    Friend WithEvents RdbWeekly As System.Windows.Forms.RadioButton
    Friend WithEvents RdbDailyTheseWorkDays As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDailyEvery As System.Windows.Forms.RadioButton
    Friend WithEvents TxtEveryDay As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents GrpDailyWorkDays As System.Windows.Forms.GroupBox
    Friend WithEvents ChkDailySaturday As common.Controls.MyCheckBox
    Friend WithEvents ChkDailyFriday As common.Controls.MyCheckBox
    Friend WithEvents ChkDailyThursday As common.Controls.MyCheckBox
    Friend WithEvents ChkDailyWednesday As common.Controls.MyCheckBox
    Friend WithEvents ChkDailyTuesday As common.Controls.MyCheckBox
    Friend WithEvents ChkDailyMonday As common.Controls.MyCheckBox
    Friend WithEvents ChkDailySunday As common.Controls.MyCheckBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RdbWeeklyMonday As System.Windows.Forms.RadioButton
    Friend WithEvents RdbWeeklySunday As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtWeeklyEvery As common.MyNumBox
    Friend WithEvents RdbWeeklyThursday As System.Windows.Forms.RadioButton
    Friend WithEvents rdbWeeklyFriday As System.Windows.Forms.RadioButton
    Friend WithEvents RdbweeklySaturday As System.Windows.Forms.RadioButton
    Friend WithEvents RdbTuesday As System.Windows.Forms.RadioButton
    Friend WithEvents RdbWeeklyWednesday As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtMonthlyEvery As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RdbMonthlyOnTheDay1 As System.Windows.Forms.RadioButton
    Friend WithEvents RdbMonthlyOnThe2 As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtMonthlyOnthe As common.MyNumBox
    Friend WithEvents CboMonthlyDays As common.Controls.MyComboBox
    Friend WithEvents CboMonthlyFirst As common.Controls.MyComboBox
    Friend WithEvents CboSemiMonthTheCombo As common.Controls.MyComboBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents RdbSemiThe As System.Windows.Forms.RadioButton
    Friend WithEvents RdbSemiFirst As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents CboYearDays As common.Controls.MyComboBox
    Friend WithEvents CboYearFirst As common.Controls.MyComboBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents RdbYearOnthe2 As System.Windows.Forms.RadioButton
    Friend WithEvents TxtYearOnthe As common.MyNumBox
    Friend WithEvents RdbYearOnThe1 As System.Windows.Forms.RadioButton
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents CboYearEvery As common.Controls.MyComboBox
    Friend WithEvents Pg_Users As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents cbguser As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents TxtRemindinAdvance As common.Controls.MyTextBox
    Friend WithEvents DtpStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents CboSemiMonthCmb1 As common.Controls.MyComboBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
End Class

