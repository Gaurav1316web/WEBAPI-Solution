Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTraineeFeedback
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
        Me.UsLock1 = New common.usLock
        Me.txtTraineeName = New common.Controls.MyLabel
        Me.lblCode = New common.Controls.MyLabel
        Me.txtTrainerCode = New common.Controls.MyLabel
        Me.txtCode = New common.UserControls.txtNavigator
        Me.lblTrainerCode = New common.Controls.MyLabel
        Me.lblTrainerName = New common.Controls.MyLabel
        Me.lblFeedback = New common.Controls.MyLabel
        Me.txtScheduleDate = New common.Controls.MyDateTimePicker
        Me.lblScheDate = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.lblDate = New common.Controls.MyLabel
        Me.txtFeedback = New common.Controls.MyTextBox
        Me.fndSchedule = New common.UserControls.txtFinder
        Me.lblSchedule = New common.Controls.MyLabel
        Me.txtDate = New common.Controls.MyDateTimePicker
        Me.btnPost = New Telerik.WinControls.UI.RadButton
        Me.BtnClose = New Telerik.WinControls.UI.RadButton
        Me.BtnDelete = New Telerik.WinControls.UI.RadButton
        Me.Btnsave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtTraineeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrainerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTrainerCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTrainerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFeedback, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScheduleDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblScheDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFeedback, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTraineeName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTrainerCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTrainerCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTrainerName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFeedback)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtScheduleDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblScheDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFeedback)
        Me.SplitContainer1.Panel1.Controls.Add(Me.fndSchedule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSchedule)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDate)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(771, 437)
        Me.SplitContainer1.SplitterDistance = 397
        Me.SplitContainer1.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(603, 16)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 417
        '
        'txtTraineeName
        '
        Me.txtTraineeName.AutoSize = False
        Me.txtTraineeName.BorderVisible = True
        Me.txtTraineeName.Location = New System.Drawing.Point(434, 68)
        Me.txtTraineeName.Name = "txtTraineeName"
        Me.txtTraineeName.Size = New System.Drawing.Size(246, 19)
        Me.txtTraineeName.TabIndex = 416
        Me.txtTraineeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(13, 22)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 381
        Me.lblCode.Text = "Code"
        '
        'txtTrainerCode
        '
        Me.txtTrainerCode.AutoSize = False
        Me.txtTrainerCode.BorderVisible = True
        Me.txtTrainerCode.Location = New System.Drawing.Point(99, 68)
        Me.txtTrainerCode.Name = "txtTrainerCode"
        Me.txtTrainerCode.Size = New System.Drawing.Size(246, 19)
        Me.txtTrainerCode.TabIndex = 415
        Me.txtTrainerCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(99, 16)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(231, 20)
        Me.txtCode.TabIndex = 20
        Me.txtCode.Value = ""
        '
        'lblTrainerCode
        '
        Me.lblTrainerCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTrainerCode.Location = New System.Drawing.Point(13, 66)
        Me.lblTrainerCode.Name = "lblTrainerCode"
        Me.lblTrainerCode.Size = New System.Drawing.Size(72, 16)
        Me.lblTrainerCode.TabIndex = 413
        Me.lblTrainerCode.Text = "Trainer Code"
        '
        'lblTrainerName
        '
        Me.lblTrainerName.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTrainerName.Location = New System.Drawing.Point(351, 71)
        Me.lblTrainerName.Name = "lblTrainerName"
        Me.lblTrainerName.Size = New System.Drawing.Size(75, 16)
        Me.lblTrainerName.TabIndex = 385
        Me.lblTrainerName.Text = "Trainer Name"
        '
        'lblFeedback
        '
        Me.lblFeedback.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblFeedback.Location = New System.Drawing.Point(13, 88)
        Me.lblFeedback.Name = "lblFeedback"
        Me.lblFeedback.Size = New System.Drawing.Size(56, 16)
        Me.lblFeedback.TabIndex = 386
        Me.lblFeedback.Text = "Feedback"
        '
        'txtScheduleDate
        '
        Me.txtScheduleDate.CustomFormat = "dd/MM/yyyy"
        Me.txtScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleDate.Location = New System.Drawing.Point(434, 40)
        Me.txtScheduleDate.MendatroryField = False
        Me.txtScheduleDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleDate.MyLinkLable1 = Me.lblScheDate
        Me.txtScheduleDate.MyLinkLable2 = Nothing
        Me.txtScheduleDate.Name = "txtScheduleDate"
        Me.txtScheduleDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleDate.ReadOnly = True
        Me.txtScheduleDate.Size = New System.Drawing.Size(142, 20)
        Me.txtScheduleDate.TabIndex = 412
        Me.txtScheduleDate.TabStop = False
        Me.txtScheduleDate.Text = "16/11/2011"
        Me.txtScheduleDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblScheDate
        '
        Me.lblScheDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblScheDate.Location = New System.Drawing.Point(351, 43)
        Me.lblScheDate.Name = "lblScheDate"
        Me.lblScheDate.Size = New System.Drawing.Size(80, 16)
        Me.lblScheDate.TabIndex = 384
        Me.lblScheDate.Text = "Schedule Date"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.Location = New System.Drawing.Point(330, 16)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 21
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(351, 16)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 382
        Me.lblDate.Text = "Date"
        '
        'txtFeedback
        '
        Me.txtFeedback.AutoSize = False
        Me.txtFeedback.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFeedback.Location = New System.Drawing.Point(99, 93)
        Me.txtFeedback.MaxLength = 200
        Me.txtFeedback.MendatroryField = True
        Me.txtFeedback.Multiline = True
        Me.txtFeedback.MyLinkLable1 = Me.lblFeedback
        Me.txtFeedback.MyLinkLable2 = Nothing
        Me.txtFeedback.Name = "txtFeedback"
        Me.txtFeedback.Size = New System.Drawing.Size(246, 94)
        Me.txtFeedback.TabIndex = 411
        '
        'fndSchedule
        '
        Me.fndSchedule.Location = New System.Drawing.Point(99, 44)
        Me.fndSchedule.MendatroryField = True
        Me.fndSchedule.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndSchedule.MyLinkLable1 = Nothing
        Me.fndSchedule.MyLinkLable2 = Nothing
        Me.fndSchedule.MyReadOnly = False
        Me.fndSchedule.Name = "fndSchedule"
        Me.fndSchedule.Size = New System.Drawing.Size(246, 19)
        Me.fndSchedule.TabIndex = 387
        Me.fndSchedule.Value = ""
        '
        'lblSchedule
        '
        Me.lblSchedule.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSchedule.Location = New System.Drawing.Point(13, 44)
        Me.lblSchedule.Name = "lblSchedule"
        Me.lblSchedule.Size = New System.Drawing.Size(53, 16)
        Me.lblSchedule.TabIndex = 383
        Me.lblSchedule.Text = "Schedule"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.Location = New System.Drawing.Point(434, 12)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(142, 20)
        Me.txtDate.TabIndex = 24
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "16/11/2011"
        Me.txtDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(160, 5)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(67, 20)
        Me.btnPost.TabIndex = 24
        Me.btnPost.Text = "Post"
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(686, 5)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(73, 21)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "Close"
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.Location = New System.Drawing.Point(87, 5)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(67, 20)
        Me.BtnDelete.TabIndex = 1
        Me.BtnDelete.Text = "Delete"
        '
        'Btnsave
        '
        Me.Btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btnsave.Location = New System.Drawing.Point(13, 5)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(68, 21)
        Me.Btnsave.TabIndex = 0
        Me.Btnsave.Text = "Save"
        '
        'FrmTraineeFeedback
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 437)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmTraineeFeedback"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "HR Trainee Feedback"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtTraineeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrainerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTrainerCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTrainerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFeedback, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScheduleDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblScheDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFeedback, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents Btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtTraineeName As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents txtTrainerCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblTrainerCode As common.Controls.MyLabel
    Friend WithEvents lblTrainerName As common.Controls.MyLabel
    Friend WithEvents lblFeedback As common.Controls.MyLabel
    Friend WithEvents txtScheduleDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblScheDate As common.Controls.MyLabel
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents txtFeedback As common.Controls.MyTextBox
    Friend WithEvents fndSchedule As common.UserControls.txtFinder
    Friend WithEvents lblSchedule As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
End Class

