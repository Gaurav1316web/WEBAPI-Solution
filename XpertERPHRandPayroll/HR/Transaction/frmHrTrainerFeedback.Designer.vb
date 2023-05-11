Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHrTrainerFeedback
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.schedule_date = New common.Controls.MyDateTimePicker
        Me.lblScheDate = New common.Controls.MyLabel
        Me.lblCode = New common.Controls.MyLabel
        Me.fndSchedule = New common.UserControls.txtFinder
        Me.lblScheduleCode = New common.Controls.MyLabel
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.lblDate = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.txtCode = New common.UserControls.txtNavigator
        Me.txtDescription = New common.Controls.MyTextBox
        Me.lblDescription = New common.Controls.MyLabel
        Me.gv = New common.UserControls.MyRadGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.lblScheduleDate = New common.Controls.MyDateTimePicker
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtFeedback = New common.Controls.MyTextBox
        Me.lblFeedback = New common.Controls.MyLabel
        Me.lblSchedule = New common.Controls.MyLabel
        Me.txtTraineeName = New common.Controls.MyTextBox
        Me.MyDateTimePicker1 = New common.Controls.MyDateTimePicker
        Me.txtprofile = New common.UserControls.txtFinder
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.lblTrainerName = New common.Controls.MyLabel
        Me.TxtNavigator1 = New common.UserControls.txtNavigator
        Me.BtnClose = New Telerik.WinControls.UI.RadButton
        Me.Btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.BtnDelete = New Telerik.WinControls.UI.RadButton
        Me.UsLock1 = New common.usLock
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.schedule_date, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScheDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScheduleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScheduleDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFeedback, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFeedback, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTraineeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTrainerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnDelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(804, 485)
        Me.SplitContainer1.SplitterDistance = 439
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.schedule_date)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.fndSchedule)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblScheduleCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDescription)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv)
        Me.SplitContainer2.Size = New System.Drawing.Size(804, 439)
        Me.SplitContainer2.SplitterDistance = 90
        Me.SplitContainer2.TabIndex = 415
        '
        'schedule_date
        '
        Me.schedule_date.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.schedule_date.CustomFormat = "dd/MM/yyyy"
        Me.schedule_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.schedule_date.Location = New System.Drawing.Point(243, 61)
        Me.schedule_date.MendatroryField = False
        Me.schedule_date.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.schedule_date.MyLinkLable1 = Me.lblScheDate
        Me.schedule_date.MyLinkLable2 = Nothing
        Me.schedule_date.Name = "schedule_date"
        Me.schedule_date.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.schedule_date.ReadOnly = True
        Me.schedule_date.Size = New System.Drawing.Size(134, 20)
        Me.schedule_date.TabIndex = 411
        Me.schedule_date.TabStop = False
        Me.schedule_date.Text = "16/11/2011"
        Me.schedule_date.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblScheDate
        '
        Me.lblScheDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblScheDate.Location = New System.Drawing.Point(376, 46)
        Me.lblScheDate.Name = "lblScheDate"
        Me.lblScheDate.Size = New System.Drawing.Size(80, 16)
        Me.lblScheDate.TabIndex = 384
        Me.lblScheDate.Text = "Schedule Date"
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(16, 15)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 381
        Me.lblCode.Text = "Code"
        '
        'fndSchedule
        '
        Me.fndSchedule.Location = New System.Drawing.Point(104, 62)
        Me.fndSchedule.MendatroryField = True
        Me.fndSchedule.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSchedule.MyLinkLable1 = Me.lblScheduleCode
        Me.fndSchedule.MyLinkLable2 = Nothing
        Me.fndSchedule.MyReadOnly = False
        Me.fndSchedule.Name = "fndSchedule"
        Me.fndSchedule.Size = New System.Drawing.Size(133, 19)
        Me.fndSchedule.TabIndex = 387
        Me.fndSchedule.Value = ""
        '
        'lblScheduleCode
        '
        Me.lblScheduleCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblScheduleCode.Location = New System.Drawing.Point(16, 62)
        Me.lblScheduleCode.Name = "lblScheduleCode"
        Me.lblScheduleCode.Size = New System.Drawing.Size(57, 16)
        Me.lblScheduleCode.TabIndex = 383
        Me.lblScheduleCode.Text = "Schedule "
        '
        'txtDate
        '
        Me.txtDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(457, 12)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(135, 20)
        Me.txtDate.TabIndex = 24
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "16/11/2011"
        Me.txtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(407, 13)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 382
        Me.lblDate.Text = "Date"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(362, 9)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 21
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(104, 9)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(257, 20)
        Me.txtCode.TabIndex = 20
        Me.txtCode.Value = ""
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(104, 36)
        Me.txtDescription.MaxLength = 200
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Me.lblDescription
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(273, 18)
        Me.txtDescription.TabIndex = 410
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(16, 33)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 385
        Me.lblDescription.Text = "Description"
        '
        'gv
        '
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(804, 345)
        Me.gv.TabIndex = 1
        Me.gv.Text = "gv"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Location = New System.Drawing.Point(13, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(688, 237)
        Me.Panel1.TabIndex = 387
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.MyLabel1)
        Me.GroupBox2.Controls.Add(Me.lblScheduleDate)
        Me.GroupBox2.Controls.Add(Me.MyLabel2)
        Me.GroupBox2.Controls.Add(Me.txtFeedback)
        Me.GroupBox2.Controls.Add(Me.lblSchedule)
        Me.GroupBox2.Controls.Add(Me.txtTraineeName)
        Me.GroupBox2.Controls.Add(Me.MyDateTimePicker1)
        Me.GroupBox2.Controls.Add(Me.txtprofile)
        Me.GroupBox2.Controls.Add(Me.lblScheDate)
        Me.GroupBox2.Controls.Add(Me.RadButton1)
        Me.GroupBox2.Controls.Add(Me.lblFeedback)
        Me.GroupBox2.Controls.Add(Me.lblTrainerName)
        Me.GroupBox2.Controls.Add(Me.TxtNavigator1)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(654, 201)
        Me.GroupBox2.TabIndex = 413
        Me.GroupBox2.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MyLabel1.Location = New System.Drawing.Point(16, 21)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(34, 16)
        Me.MyLabel1.TabIndex = 381
        Me.MyLabel1.Text = "Code"
        '
        'lblScheduleDate
        '
        Me.lblScheduleDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblScheduleDate.CustomFormat = "dd/MM/yyyy"
        Me.lblScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblScheduleDate.Location = New System.Drawing.Point(459, 43)
        Me.lblScheduleDate.MendatroryField = False
        Me.lblScheduleDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.lblScheduleDate.MyLinkLable1 = Me.lblScheDate
        Me.lblScheduleDate.MyLinkLable2 = Nothing
        Me.lblScheduleDate.Name = "lblScheduleDate"
        Me.lblScheduleDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.lblScheduleDate.Size = New System.Drawing.Size(142, 20)
        Me.lblScheduleDate.TabIndex = 412
        Me.lblScheduleDate.TabStop = False
        Me.lblScheduleDate.Text = "16/11/2011"
        Me.lblScheduleDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(376, 19)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 382
        Me.MyLabel2.Text = "Date"
        '
        'txtFeedback
        '
        Me.txtFeedback.AutoSize = False
        Me.txtFeedback.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFeedback.Location = New System.Drawing.Point(102, 92)
        Me.txtFeedback.MaxLength = 200
        Me.txtFeedback.MendatroryField = False
        Me.txtFeedback.Multiline = True
        Me.txtFeedback.MyLinkLable1 = Me.lblFeedback
        Me.txtFeedback.MyLinkLable2 = Nothing
        Me.txtFeedback.Name = "txtFeedback"
        Me.txtFeedback.Size = New System.Drawing.Size(246, 94)
        Me.txtFeedback.TabIndex = 411
        '
        'lblFeedback
        '
        Me.lblFeedback.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblFeedback.Location = New System.Drawing.Point(16, 87)
        Me.lblFeedback.Name = "lblFeedback"
        Me.lblFeedback.Size = New System.Drawing.Size(56, 16)
        Me.lblFeedback.TabIndex = 386
        Me.lblFeedback.Text = "Feedback"
        '
        'lblSchedule
        '
        Me.lblSchedule.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSchedule.Location = New System.Drawing.Point(16, 43)
        Me.lblSchedule.Name = "lblSchedule"
        Me.lblSchedule.Size = New System.Drawing.Size(53, 16)
        Me.lblSchedule.TabIndex = 383
        Me.lblSchedule.Text = "Schedule"
        '
        'txtTraineeName
        '
        Me.txtTraineeName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTraineeName.Location = New System.Drawing.Point(102, 68)
        Me.txtTraineeName.MaxLength = 200
        Me.txtTraineeName.MendatroryField = False
        Me.txtTraineeName.MyLinkLable1 = Nothing
        Me.txtTraineeName.MyLinkLable2 = Nothing
        Me.txtTraineeName.Name = "txtTraineeName"
        Me.txtTraineeName.Size = New System.Drawing.Size(246, 18)
        Me.txtTraineeName.TabIndex = 410
        '
        'MyDateTimePicker1
        '
        Me.MyDateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyDateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.MyDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.MyDateTimePicker1.Location = New System.Drawing.Point(459, 15)
        Me.MyDateTimePicker1.MendatroryField = False
        Me.MyDateTimePicker1.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.MyLinkLable1 = Me.MyLabel2
        Me.MyDateTimePicker1.MyLinkLable2 = Nothing
        Me.MyDateTimePicker1.Name = "MyDateTimePicker1"
        Me.MyDateTimePicker1.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.MyDateTimePicker1.Size = New System.Drawing.Size(142, 20)
        Me.MyDateTimePicker1.TabIndex = 24
        Me.MyDateTimePicker1.TabStop = False
        Me.MyDateTimePicker1.Text = "16/11/2011"
        Me.MyDateTimePicker1.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'txtprofile
        '
        Me.txtprofile.Location = New System.Drawing.Point(102, 43)
        Me.txtprofile.MendatroryField = True
        Me.txtprofile.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprofile.MyLinkLable1 = Nothing
        Me.txtprofile.MyLinkLable2 = Nothing
        Me.txtprofile.MyReadOnly = False
        Me.txtprofile.Name = "txtprofile"
        Me.txtprofile.Size = New System.Drawing.Size(246, 19)
        Me.txtprofile.TabIndex = 387
        Me.txtprofile.Value = ""
        '
        'RadButton1
        '
        Me.RadButton1.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.RadButton1.Location = New System.Drawing.Point(333, 15)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(15, 20)
        Me.RadButton1.TabIndex = 21
        '
        'lblTrainerName
        '
        Me.lblTrainerName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTrainerName.Location = New System.Drawing.Point(16, 65)
        Me.lblTrainerName.Name = "lblTrainerName"
        Me.lblTrainerName.Size = New System.Drawing.Size(75, 16)
        Me.lblTrainerName.TabIndex = 385
        Me.lblTrainerName.Text = "Trainer Name"
        '
        'TxtNavigator1
        '
        Me.TxtNavigator1.Location = New System.Drawing.Point(102, 15)
        Me.TxtNavigator1.MendatroryField = True
        Me.TxtNavigator1.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TxtNavigator1.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TxtNavigator1.MyLinkLable1 = Me.MyLabel1
        Me.TxtNavigator1.MyLinkLable2 = Nothing
        Me.TxtNavigator1.MyMaxLength = 32767
        Me.TxtNavigator1.MyReadOnly = False
        Me.TxtNavigator1.Name = "TxtNavigator1"
        Me.TxtNavigator1.Size = New System.Drawing.Size(231, 20)
        Me.TxtNavigator1.TabIndex = 20
        Me.TxtNavigator1.Value = ""
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(706, 18)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(73, 21)
        Me.BtnClose.TabIndex = 27
        Me.BtnClose.Text = "Close"
        '
        'Btnsave
        '
        Me.Btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btnsave.Location = New System.Drawing.Point(12, 18)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(68, 21)
        Me.Btnsave.TabIndex = 25
        Me.Btnsave.Text = "Save"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(159, 18)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(67, 20)
        Me.btnPost.TabIndex = 28
        Me.btnPost.Text = "Post"
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.Location = New System.Drawing.Point(86, 18)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(67, 20)
        Me.BtnDelete.TabIndex = 26
        Me.BtnDelete.Text = "Delete"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(681, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 418
        '
        'FrmHrTrainerFeedback
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 485)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmHrTrainerFeedback"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmHrTrainerFeedback"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.schedule_date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScheDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScheduleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScheduleDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFeedback, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFeedback, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTraineeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyDateTimePicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTrainerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblScheduleCode As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents fndSchedule As common.UserControls.txtFinder
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblScheduleDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblScheDate As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtFeedback As common.Controls.MyTextBox
    Friend WithEvents lblFeedback As common.Controls.MyLabel
    Friend WithEvents lblSchedule As common.Controls.MyLabel
    Friend WithEvents txtTraineeName As common.Controls.MyTextBox
    Friend WithEvents MyDateTimePicker1 As common.Controls.MyDateTimePicker
    Friend WithEvents txtprofile As common.UserControls.txtFinder
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblTrainerName As common.Controls.MyLabel
    Friend WithEvents TxtNavigator1 As common.UserControls.txtNavigator
    Friend WithEvents BtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents Btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents schedule_date As common.Controls.MyDateTimePicker
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents UsLock1 As common.usLock
End Class

