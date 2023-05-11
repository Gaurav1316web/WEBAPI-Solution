Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScheduleForTraining
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
        Me.components = New System.ComponentModel.Container
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.DtpScheduleEndTime = New common.Controls.MyDateTimePicker
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.fndTrainingCourse = New common.UserControls.txtFinder
        Me.fndTraininer = New common.UserControls.txtFinder
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgResource = New common.MyCheckBoxGrid
        Me.CmbTrainingMode = New common.Controls.MyComboBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.CmbVenue = New common.Controls.MyComboBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.DtpScheduleEndDate = New common.Controls.MyDateTimePicker
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.DtpScheduleStartTime = New common.Controls.MyDateTimePicker
        Me.DtpScheduleStartDate = New common.Controls.MyDateTimePicker
        Me.lblTrainerName = New common.Controls.MyLabel
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.lblMCCCode = New common.Controls.MyLabel
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.txtRemark = New common.Controls.MyTextBox
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.lblVSPCode = New common.Controls.MyLabel
        Me.lblTrainingCourseDesc = New common.Controls.MyLabel
        Me.lblDocDate = New common.Controls.MyLabel
        Me.dtpDocDate = New common.Controls.MyDateTimePicker
        Me.lblCode = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.txtCode = New common.UserControls.txtNavigator
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.gv1 = New common.UserControls.MyRadGridView
        Me.pvpCustomFields = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcCustomFields1 = New ucCustomFields
        Me.Attachments = New Telerik.WinControls.UI.RadPageViewPage
        Me.UcAttachment1 = New XpertERPEngine.ucAttachment
        Me.BtnPost = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem
        Me.BtnsaveLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.DtpScheduleEndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.CmbTrainingMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbVenue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpScheduleEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpScheduleStartTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtpScheduleStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTrainerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTrainingCourseDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pvpCustomFields.SuspendLayout()
        Me.Attachments.SuspendLayout()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(988, 530)
        Me.SplitContainer1.SplitterDistance = 494
        Me.SplitContainer1.TabIndex = 42
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.pvpCustomFields)
        Me.RadPageView1.Controls.Add(Me.Attachments)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(988, 494)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(105.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(967, 446)
        Me.RadPageViewPage1.Text = "Schedule Training"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.DtpScheduleEndTime)
        Me.RadGroupBox1.Controls.Add(Me.fndTrainingCourse)
        Me.RadGroupBox1.Controls.Add(Me.fndTraininer)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.CmbTrainingMode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox1.Controls.Add(Me.CmbVenue)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.DtpScheduleEndDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.DtpScheduleStartTime)
        Me.RadGroupBox1.Controls.Add(Me.DtpScheduleStartDate)
        Me.RadGroupBox1.Controls.Add(Me.lblTrainerName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtRemark)
        Me.RadGroupBox1.Controls.Add(Me.btnnew)
        Me.RadGroupBox1.Controls.Add(Me.lblVSPCode)
        Me.RadGroupBox1.Controls.Add(Me.lblTrainingCourseDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpDocDate)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Schedule Training Head"
        Me.RadGroupBox1.Location = New System.Drawing.Point(2, 1)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(967, 207)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "Schedule Training Head"
        '
        'DtpScheduleEndTime
        '
        Me.DtpScheduleEndTime.CustomFormat = "hh:mm:ss tt"
        Me.DtpScheduleEndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpScheduleEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpScheduleEndTime.Location = New System.Drawing.Point(346, 112)
        Me.DtpScheduleEndTime.MendatroryField = True
        Me.DtpScheduleEndTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleEndTime.MyLinkLable1 = Me.MyLabel4
        Me.DtpScheduleEndTime.MyLinkLable2 = Nothing
        Me.DtpScheduleEndTime.Name = "DtpScheduleEndTime"
        Me.DtpScheduleEndTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleEndTime.Size = New System.Drawing.Size(113, 18)
        Me.DtpScheduleEndTime.TabIndex = 66
        Me.DtpScheduleEndTime.TabStop = False
        Me.DtpScheduleEndTime.Text = "12:00:00 AM"
        Me.DtpScheduleEndTime.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(2, 113)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(109, 16)
        Me.MyLabel4.TabIndex = 66
        Me.MyLabel4.Text = "Schedule Start Time"
        '
        'fndTrainingCourse
        '
        Me.fndTrainingCourse.Location = New System.Drawing.Point(112, 68)
        Me.fndTrainingCourse.MendatroryField = True
        Me.fndTrainingCourse.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTrainingCourse.MyLinkLable1 = Nothing
        Me.fndTrainingCourse.MyLinkLable2 = Nothing
        Me.fndTrainingCourse.MyReadOnly = False
        Me.fndTrainingCourse.Name = "fndTrainingCourse"
        Me.fndTrainingCourse.Size = New System.Drawing.Size(347, 19)
        Me.fndTrainingCourse.TabIndex = 75
        Me.fndTrainingCourse.Value = ""
        '
        'fndTraininer
        '
        Me.fndTraininer.Location = New System.Drawing.Point(112, 45)
        Me.fndTraininer.MendatroryField = True
        Me.fndTraininer.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndTraininer.MyLinkLable1 = Nothing
        Me.fndTraininer.MyLinkLable2 = Nothing
        Me.fndTraininer.MyReadOnly = False
        Me.fndTraininer.Name = "fndTraininer"
        Me.fndTraininer.Size = New System.Drawing.Size(347, 19)
        Me.fndTraininer.TabIndex = 74
        Me.fndTraininer.Value = ""
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgResource)
        Me.RadGroupBox4.HeaderText = "Resource"
        Me.RadGroupBox4.Location = New System.Drawing.Point(586, 93)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(358, 107)
        Me.RadGroupBox4.TabIndex = 73
        Me.RadGroupBox4.Text = "Resource"
        '
        'cbgResource
        '
        Me.cbgResource.CheckedValue = Nothing
        Me.cbgResource.DataSource = Nothing
        Me.cbgResource.DisplayMember = "Name"
        Me.cbgResource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgResource.Location = New System.Drawing.Point(10, 20)
        Me.cbgResource.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgResource.MyShowHeadrText = False
        Me.cbgResource.Name = "cbgResource"
        Me.cbgResource.Size = New System.Drawing.Size(338, 77)
        Me.cbgResource.TabIndex = 2
        Me.cbgResource.ValueMember = "Code"
        '
        'CmbTrainingMode
        '
        Me.CmbTrainingMode.AllowShowFocusCues = False
        Me.CmbTrainingMode.AutoCompleteDisplayMember = Nothing
        Me.CmbTrainingMode.AutoCompleteValueMember = Nothing
        Me.CmbTrainingMode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbTrainingMode.Location = New System.Drawing.Point(113, 158)
        Me.CmbTrainingMode.MendatroryField = True
        Me.CmbTrainingMode.MyLinkLable1 = Me.MyLabel6
        Me.CmbTrainingMode.MyLinkLable2 = Nothing
        Me.CmbTrainingMode.Name = "CmbTrainingMode"
        Me.CmbTrainingMode.Size = New System.Drawing.Size(345, 20)
        Me.CmbTrainingMode.TabIndex = 72
        '
        'MyLabel6
        '
        Me.MyLabel6.Location = New System.Drawing.Point(2, 159)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(79, 18)
        Me.MyLabel6.TabIndex = 71
        Me.MyLabel6.Text = "Training Mode"
        '
        'CmbVenue
        '
        Me.CmbVenue.AllowShowFocusCues = False
        Me.CmbVenue.AutoCompleteDisplayMember = Nothing
        Me.CmbVenue.AutoCompleteValueMember = Nothing
        Me.CmbVenue.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbVenue.Location = New System.Drawing.Point(112, 134)
        Me.CmbVenue.MendatroryField = True
        Me.CmbVenue.MyLinkLable1 = Me.MyLabel5
        Me.CmbVenue.MyLinkLable2 = Nothing
        Me.CmbVenue.Name = "CmbVenue"
        Me.CmbVenue.Size = New System.Drawing.Size(347, 20)
        Me.CmbVenue.TabIndex = 70
        '
        'MyLabel5
        '
        Me.MyLabel5.Location = New System.Drawing.Point(3, 135)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(38, 18)
        Me.MyLabel5.TabIndex = 69
        Me.MyLabel5.Text = "Venue"
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(237, 113)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel3.TabIndex = 68
        Me.MyLabel3.Text = "Schedule End Time"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(237, 91)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel2.TabIndex = 64
        Me.MyLabel2.Text = "Schedule End Date"
        '
        'DtpScheduleEndDate
        '
        Me.DtpScheduleEndDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpScheduleEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpScheduleEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpScheduleEndDate.Location = New System.Drawing.Point(346, 90)
        Me.DtpScheduleEndDate.MendatroryField = True
        Me.DtpScheduleEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleEndDate.MyLinkLable1 = Me.MyLabel2
        Me.DtpScheduleEndDate.MyLinkLable2 = Nothing
        Me.DtpScheduleEndDate.Name = "DtpScheduleEndDate"
        Me.DtpScheduleEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleEndDate.Size = New System.Drawing.Size(113, 18)
        Me.DtpScheduleEndDate.TabIndex = 63
        Me.DtpScheduleEndDate.TabStop = False
        Me.DtpScheduleEndDate.Text = "03/05/2011"
        Me.DtpScheduleEndDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(2, 91)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(108, 16)
        Me.MyLabel1.TabIndex = 62
        Me.MyLabel1.Text = "Schedule Start Date"
        '
        'DtpScheduleStartTime
        '
        Me.DtpScheduleStartTime.CustomFormat = "hh:mm:ss tt"
        Me.DtpScheduleStartTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpScheduleStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpScheduleStartTime.Location = New System.Drawing.Point(112, 112)
        Me.DtpScheduleStartTime.MendatroryField = True
        Me.DtpScheduleStartTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleStartTime.MyLinkLable1 = Me.MyLabel4
        Me.DtpScheduleStartTime.MyLinkLable2 = Nothing
        Me.DtpScheduleStartTime.Name = "DtpScheduleStartTime"
        Me.DtpScheduleStartTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleStartTime.Size = New System.Drawing.Size(122, 18)
        Me.DtpScheduleStartTime.TabIndex = 65
        Me.DtpScheduleStartTime.TabStop = False
        Me.DtpScheduleStartTime.Text = "12:00:00 AM"
        Me.DtpScheduleStartTime.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'DtpScheduleStartDate
        '
        Me.DtpScheduleStartDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpScheduleStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpScheduleStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpScheduleStartDate.Location = New System.Drawing.Point(112, 90)
        Me.DtpScheduleStartDate.MendatroryField = True
        Me.DtpScheduleStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleStartDate.MyLinkLable1 = Me.MyLabel1
        Me.DtpScheduleStartDate.MyLinkLable2 = Nothing
        Me.DtpScheduleStartDate.Name = "DtpScheduleStartDate"
        Me.DtpScheduleStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DtpScheduleStartDate.Size = New System.Drawing.Size(122, 18)
        Me.DtpScheduleStartDate.TabIndex = 61
        Me.DtpScheduleStartDate.TabStop = False
        Me.DtpScheduleStartDate.Text = "03/05/2011"
        Me.DtpScheduleStartDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblTrainerName
        '
        Me.lblTrainerName.AutoSize = False
        Me.lblTrainerName.BackColor = System.Drawing.Color.White
        Me.lblTrainerName.BorderVisible = True
        Me.lblTrainerName.Location = New System.Drawing.Point(586, 46)
        Me.lblTrainerName.Name = "lblTrainerName"
        Me.lblTrainerName.Size = New System.Drawing.Size(352, 19)
        Me.lblTrainerName.TabIndex = 28
        Me.lblTrainerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel10.Location = New System.Drawing.Point(464, 46)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(74, 18)
        Me.MyLabel10.TabIndex = 56
        Me.MyLabel10.Text = "Trainer Name"
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(464, 68)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(118, 18)
        Me.MyLabel7.TabIndex = 27
        Me.MyLabel7.Text = "Training Course Name"
        '
        'lblMCCCode
        '
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(2, 46)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(70, 18)
        Me.lblMCCCode.TabIndex = 55
        Me.lblMCCCode.Text = "Trainer Code"
        '
        'RadLabel3
        '
        Me.RadLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel3.Location = New System.Drawing.Point(2, 184)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(46, 16)
        Me.RadLabel3.TabIndex = 51
        Me.RadLabel3.Text = "Remark"
        '
        'txtRemark
        '
        Me.txtRemark.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemark.Location = New System.Drawing.Point(112, 182)
        Me.txtRemark.MaxLength = 200
        Me.txtRemark.MendatroryField = False
        Me.txtRemark.MyLinkLable1 = Me.RadLabel3
        Me.txtRemark.MyLinkLable2 = Nothing
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(346, 18)
        Me.txtRemark.TabIndex = 0
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(443, 21)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 44
        '
        'lblVSPCode
        '
        Me.lblVSPCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVSPCode.Location = New System.Drawing.Point(2, 68)
        Me.lblVSPCode.Name = "lblVSPCode"
        Me.lblVSPCode.Size = New System.Drawing.Size(85, 18)
        Me.lblVSPCode.TabIndex = 26
        Me.lblVSPCode.Text = "Training Course"
        '
        'lblTrainingCourseDesc
        '
        Me.lblTrainingCourseDesc.AutoSize = False
        Me.lblTrainingCourseDesc.BackColor = System.Drawing.Color.White
        Me.lblTrainingCourseDesc.BorderVisible = True
        Me.lblTrainingCourseDesc.Location = New System.Drawing.Point(586, 68)
        Me.lblTrainingCourseDesc.Name = "lblTrainingCourseDesc"
        Me.lblTrainingCourseDesc.Size = New System.Drawing.Size(352, 19)
        Me.lblTrainingCourseDesc.TabIndex = 27
        Me.lblTrainingCourseDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDocDate
        '
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(464, 23)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDocDate.TabIndex = 19
        Me.lblDocDate.Text = "Date"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.Location = New System.Drawing.Point(586, 22)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.Size = New System.Drawing.Size(163, 18)
        Me.dtpDocDate.TabIndex = 18
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCode.Location = New System.Drawing.Point(2, 23)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(80, 16)
        Me.lblCode.TabIndex = 10
        Me.lblCode.Text = "Document No"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(837, 21)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(112, 21)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(333, 21)
        Me.txtCode.TabIndex = 9
        Me.txtCode.Value = ""
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Schedule Training Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 214)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(967, 229)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "Schedule Training Details"
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.Size = New System.Drawing.Size(947, 199)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'pvpCustomFields
        '
        Me.pvpCustomFields.Controls.Add(Me.UcCustomFields1)
        Me.pvpCustomFields.ItemSize = New System.Drawing.SizeF(86.0!, 28.0!)
        Me.pvpCustomFields.Location = New System.Drawing.Point(10, 37)
        Me.pvpCustomFields.Name = "pvpCustomFields"
        Me.pvpCustomFields.Size = New System.Drawing.Size(967, 446)
        Me.pvpCustomFields.Text = "Custom Fields"
        '
        'UcCustomFields1
        '
        Me.UcCustomFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCustomFields1.Location = New System.Drawing.Point(0, 0)
        Me.UcCustomFields1.Name = "UcCustomFields1"
        Me.UcCustomFields1.Size = New System.Drawing.Size(967, 446)
        Me.UcCustomFields1.TabIndex = 1
        '
        'Attachments
        '
        Me.Attachments.Controls.Add(Me.UcAttachment1)
        Me.Attachments.ItemSize = New System.Drawing.SizeF(79.0!, 28.0!)
        Me.Attachments.Location = New System.Drawing.Point(10, 37)
        Me.Attachments.Name = "Attachments"
        Me.Attachments.Size = New System.Drawing.Size(967, 446)
        Me.Attachments.Text = "Attachments"
        '
        'UcAttachment1
        '
        Me.UcAttachment1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcAttachment1.Location = New System.Drawing.Point(0, 0)
        Me.UcAttachment1.Name = "UcAttachment1"
        Me.UcAttachment1.Size = New System.Drawing.Size(967, 446)
        Me.UcAttachment1.TabIndex = 0
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(159, 7)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 18)
        Me.BtnPost.TabIndex = 2
        Me.BtnPost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(911, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(15, 7)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(87, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(988, 20)
        Me.rdmenufile.TabIndex = 67
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnsaveLayout, Me.BtnDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        Me.rdmenufile1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'BtnsaveLayout
        '
        Me.BtnsaveLayout.AccessibleDescription = "Save Layout"
        Me.BtnsaveLayout.AccessibleName = "Save Layout"
        Me.BtnsaveLayout.Name = "BtnsaveLayout"
        Me.BtnsaveLayout.Text = "Save Layout"
        Me.BtnsaveLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'BtnDeleteLayout
        '
        Me.BtnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.BtnDeleteLayout.AccessibleName = "Delete Layout"
        Me.BtnDeleteLayout.Name = "BtnDeleteLayout"
        Me.BtnDeleteLayout.Text = "Delete Layout"
        Me.BtnDeleteLayout.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'frmScheduleForTraining
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 550)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmScheduleForTraining"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Schedule Training"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.DtpScheduleEndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        CType(Me.CmbTrainingMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbVenue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpScheduleEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpScheduleStartTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtpScheduleStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTrainerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTrainingCourseDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pvpCustomFields.ResumeLayout(False)
        Me.Attachments.ResumeLayout(False)
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pvpCustomFields As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcCustomFields1 As ucCustomFields
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblVSPCode As common.Controls.MyLabel
    Friend WithEvents lblTrainingCourseDesc As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents txtRemark As common.Controls.MyTextBox
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents lblTrainerName As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnsaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Attachments As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents UcAttachment1 As XpertERPEngine.ucAttachment
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents DtpScheduleEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents DtpScheduleStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents DtpScheduleStartTime As common.Controls.MyDateTimePicker
    Friend WithEvents CmbVenue As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents CmbTrainingMode As common.Controls.MyComboBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgResource As common.MyCheckBoxGrid
    Friend WithEvents fndTrainingCourse As common.UserControls.txtFinder
    Friend WithEvents fndTraininer As common.UserControls.txtFinder
    Friend WithEvents DtpScheduleEndTime As common.Controls.MyDateTimePicker
End Class

