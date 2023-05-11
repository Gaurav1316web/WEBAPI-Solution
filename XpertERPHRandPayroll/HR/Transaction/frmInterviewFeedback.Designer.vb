Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInterviewFeedback
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
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.UsLock1 = New common.usLock
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.txtFeedbackCode = New common.Controls.MyTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.MyLabel11 = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.txtcode = New common.UserControls.txtNavigator
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.UcRequisitionDetail1 = New XpertERPHRandPayroll.ucRequisitionDetail
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.gvround = New common.UserControls.MyRadGridView
        Me.GrpBoxPar = New System.Windows.Forms.GroupBox
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer
        Me.LblResch = New common.Controls.MyLabel
        Me.txtremark1 = New common.Controls.MyTextBox
        Me.MyLabel19 = New common.Controls.MyLabel
        Me.lblround = New common.Controls.MyLabel
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.txtcomments = New common.Controls.MyTextBox
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.LblFinalAction = New common.Controls.MyLabel
        Me.cmbAction = New common.Controls.MyComboBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.CmbFinalAction = New common.Controls.MyComboBox
        Me.LblStartDate = New common.Controls.MyLabel
        Me.LblEndTime = New common.Controls.MyLabel
        Me.txtroundcode = New common.Controls.MyTextBox
        Me.dtpEndTime = New common.Controls.MyDateTimePicker
        Me.dtpStartTime = New common.Controls.MyDateTimePicker
        Me.gvparameter = New common.UserControls.MyRadGridView
        Me.txtclearingscore = New common.MyNumBox
        Me.MyLabel7 = New common.Controls.MyLabel
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.txtscore = New common.MyNumBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.btndelete = New Telerik.WinControls.UI.RadButton
        Me.txtpercentage = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txttotalscore = New common.MyNumBox
        Me.btnpost = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.LblDec = New common.Controls.MyLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFeedbackCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gvround, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvround.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBoxPar.SuspendLayout()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.LblResch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremark1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblround, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcomments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFinalAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbFinalAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblEndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtroundcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStartTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvparameter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvparameter.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtclearingscore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtscore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotalscore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblDec, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(896, 680)
        Me.SplitContainer1.SplitterDistance = 639
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(896, 639)
        Me.SplitContainer2.SplitterDistance = 152
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.IsSplitterFixed = True
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.LblDec)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer4.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel10)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtFeedbackCode)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer4.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer4.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtcode)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.UcRequisitionDetail1)
        Me.SplitContainer4.Size = New System.Drawing.Size(896, 152)
        Me.SplitContainer4.SplitterDistance = 44
        Me.SplitContainer4.TabIndex = 0
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(6, 11)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel3.TabIndex = 129
        Me.MyLabel3.Text = "Applicant Code"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(511, 9)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(36, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 110
        Me.UsLock1.Visible = False
        '
        'MyLabel10
        '
        Me.MyLabel10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(628, 10)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel10.TabIndex = 132
        Me.MyLabel10.Text = "Next Round"
        '
        'MyLabel9
        '
        Me.MyLabel9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(718, 10)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel9.TabIndex = 133
        Me.MyLabel9.Text = "Reschedule"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.Aquamarine
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(795, 12)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(14, 13)
        Me.Panel3.TabIndex = 139
        '
        'txtFeedbackCode
        '
        Me.txtFeedbackCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtFeedbackCode.Location = New System.Drawing.Point(313, 8)
        Me.txtFeedbackCode.MaxLength = 100
        Me.txtFeedbackCode.MendatroryField = False
        Me.txtFeedbackCode.MyLinkLable1 = Nothing
        Me.txtFeedbackCode.MyLinkLable2 = Nothing
        Me.txtFeedbackCode.Name = "txtFeedbackCode"
        Me.txtFeedbackCode.Size = New System.Drawing.Size(62, 20)
        Me.txtFeedbackCode.TabIndex = 131
        Me.txtFeedbackCode.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.LightPink
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(613, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(14, 13)
        Me.Panel1.TabIndex = 134
        '
        'MyLabel11
        '
        Me.MyLabel11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(809, 10)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel11.TabIndex = 138
        Me.MyLabel11.Text = "Final Opinion"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(293, 8)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 1
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(95, 8)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.MyLabel3
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(199, 21)
        Me.txtcode.TabIndex = 0
        Me.txtcode.Value = ""
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.Thistle
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(703, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(14, 13)
        Me.Panel2.TabIndex = 135
        '
        'UcRequisitionDetail1
        '
        Me.UcRequisitionDetail1.AppCode = ""
        Me.UcRequisitionDetail1.AppDate = ""
        Me.UcRequisitionDetail1.AppName = ""
        Me.UcRequisitionDetail1.DateofBirth = ""
        Me.UcRequisitionDetail1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcRequisitionDetail1.Email = ""
        Me.UcRequisitionDetail1.Location = New System.Drawing.Point(0, 0)
        Me.UcRequisitionDetail1.Name = "UcRequisitionDetail1"
        Me.UcRequisitionDetail1.ReqCode = ""
        Me.UcRequisitionDetail1.Size = New System.Drawing.Size(896, 104)
        Me.UcRequisitionDetail1.TabIndex = 130
        Me.UcRequisitionDetail1.TelephoneNo = ""
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GrpBoxPar)
        Me.SplitContainer3.Size = New System.Drawing.Size(896, 483)
        Me.SplitContainer3.SplitterDistance = 155
        Me.SplitContainer3.TabIndex = 140
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gvround)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(896, 155)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Round Details"
        '
        'gvround
        '
        Me.gvround.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvround.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvround.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvround.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvround.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvround.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvround.Location = New System.Drawing.Point(3, 16)
        '
        'gvround
        '
        Me.gvround.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvround.MasterTemplate.AllowAddNewRow = False
        Me.gvround.MasterTemplate.EnableGrouping = False
        Me.gvround.Name = "gvround"
        Me.gvround.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvround.Size = New System.Drawing.Size(890, 136)
        Me.gvround.TabIndex = 3
        Me.gvround.TabStop = False
        Me.gvround.Text = "RadGridView1"
        '
        'GrpBoxPar
        '
        Me.GrpBoxPar.Controls.Add(Me.SplitContainer6)
        Me.GrpBoxPar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpBoxPar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBoxPar.Location = New System.Drawing.Point(0, 0)
        Me.GrpBoxPar.Name = "GrpBoxPar"
        Me.GrpBoxPar.Size = New System.Drawing.Size(896, 324)
        Me.GrpBoxPar.TabIndex = 3
        Me.GrpBoxPar.TabStop = False
        Me.GrpBoxPar.Text = "Parameter Details"
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer6.IsSplitterFixed = True
        Me.SplitContainer6.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.SplitContainer5)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.txtclearingscore)
        Me.SplitContainer6.Panel2.Controls.Add(Me.MyLabel5)
        Me.SplitContainer6.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer6.Panel2.Controls.Add(Me.txtscore)
        Me.SplitContainer6.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer6.Panel2.Controls.Add(Me.txtpercentage)
        Me.SplitContainer6.Panel2.Controls.Add(Me.MyLabel2)
        Me.SplitContainer6.Panel2.Controls.Add(Me.MyLabel4)
        Me.SplitContainer6.Panel2.Controls.Add(Me.MyLabel7)
        Me.SplitContainer6.Panel2.Controls.Add(Me.txttotalscore)
        Me.SplitContainer6.Size = New System.Drawing.Size(890, 305)
        Me.SplitContainer6.SplitterDistance = 249
        Me.SplitContainer6.TabIndex = 153
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer5.IsSplitterFixed = True
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.LblResch)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtremark1)
        Me.SplitContainer5.Panel1.Controls.Add(Me.lblround)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtcomments)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel19)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer5.Panel1.Controls.Add(Me.LblFinalAction)
        Me.SplitContainer5.Panel1.Controls.Add(Me.cmbAction)
        Me.SplitContainer5.Panel1.Controls.Add(Me.CmbFinalAction)
        Me.SplitContainer5.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer5.Panel1.Controls.Add(Me.LblStartDate)
        Me.SplitContainer5.Panel1.Controls.Add(Me.LblEndTime)
        Me.SplitContainer5.Panel1.Controls.Add(Me.txtroundcode)
        Me.SplitContainer5.Panel1.Controls.Add(Me.dtpEndTime)
        Me.SplitContainer5.Panel1.Controls.Add(Me.dtpStartTime)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.gvparameter)
        Me.SplitContainer5.Size = New System.Drawing.Size(890, 249)
        Me.SplitContainer5.SplitterDistance = 111
        Me.SplitContainer5.TabIndex = 152
        '
        'LblResch
        '
        Me.LblResch.AutoSize = False
        Me.LblResch.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblResch.Location = New System.Drawing.Point(495, 12)
        Me.LblResch.Name = "LblResch"
        Me.LblResch.Size = New System.Drawing.Size(392, 16)
        Me.LblResch.TabIndex = 152
        Me.LblResch.Text = "Final Action"
        '
        'txtremark1
        '
        Me.txtremark1.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtremark1.Location = New System.Drawing.Point(81, 57)
        Me.txtremark1.MaxLength = 200
        Me.txtremark1.MendatroryField = True
        Me.txtremark1.MyLinkLable1 = Me.MyLabel19
        Me.txtremark1.MyLinkLable2 = Nothing
        Me.txtremark1.Name = "txtremark1"
        Me.txtremark1.Size = New System.Drawing.Size(475, 20)
        Me.txtremark1.TabIndex = 9
        '
        'MyLabel19
        '
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(7, 57)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel19.TabIndex = 132
        Me.MyLabel19.Text = "Remarks "
        '
        'lblround
        '
        Me.lblround.AutoSize = False
        Me.lblround.BorderVisible = True
        Me.lblround.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblround.Location = New System.Drawing.Point(199, 12)
        Me.lblround.Name = "lblround"
        Me.lblround.Size = New System.Drawing.Size(289, 18)
        Me.lblround.TabIndex = 151
        Me.lblround.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblround.TextWrap = False
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(7, 81)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel6.TabIndex = 133
        Me.MyLabel6.Text = "Comments"
        '
        'txtcomments
        '
        Me.txtcomments.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtcomments.Location = New System.Drawing.Point(81, 80)
        Me.txtcomments.MaxLength = 200
        Me.txtcomments.MendatroryField = False
        Me.txtcomments.MyLinkLable1 = Me.MyLabel6
        Me.txtcomments.MyLinkLable2 = Nothing
        Me.txtcomments.Name = "txtcomments"
        Me.txtcomments.Size = New System.Drawing.Size(475, 20)
        Me.txtcomments.TabIndex = 10
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(7, 12)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel8.TabIndex = 139
        Me.MyLabel8.Text = "Round Code"
        '
        'LblFinalAction
        '
        Me.LblFinalAction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFinalAction.Location = New System.Drawing.Point(212, 35)
        Me.LblFinalAction.Name = "LblFinalAction"
        Me.LblFinalAction.Size = New System.Drawing.Size(65, 16)
        Me.LblFinalAction.TabIndex = 127
        Me.LblFinalAction.Text = "Final Action"
        '
        'cmbAction
        '
        Me.cmbAction.AllowShowFocusCues = False
        Me.cmbAction.AutoCompleteDisplayMember = Nothing
        Me.cmbAction.AutoCompleteValueMember = Nothing
        Me.cmbAction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbAction.Location = New System.Drawing.Point(81, 33)
        Me.cmbAction.MendatroryField = True
        Me.cmbAction.MyLinkLable1 = Me.MyLabel1
        Me.cmbAction.MyLinkLable2 = Nothing
        Me.cmbAction.Name = "cmbAction"
        Me.cmbAction.Size = New System.Drawing.Size(113, 20)
        Me.cmbAction.TabIndex = 5
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 33)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel1.TabIndex = 124
        Me.MyLabel1.Text = "Action"
        '
        'CmbFinalAction
        '
        Me.CmbFinalAction.AllowShowFocusCues = False
        Me.CmbFinalAction.AutoCompleteDisplayMember = Nothing
        Me.CmbFinalAction.AutoCompleteValueMember = Nothing
        Me.CmbFinalAction.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbFinalAction.Location = New System.Drawing.Point(281, 33)
        Me.CmbFinalAction.MendatroryField = True
        Me.CmbFinalAction.MyLinkLable1 = Me.LblFinalAction
        Me.CmbFinalAction.MyLinkLable2 = Nothing
        Me.CmbFinalAction.Name = "CmbFinalAction"
        Me.CmbFinalAction.Size = New System.Drawing.Size(98, 20)
        Me.CmbFinalAction.TabIndex = 6
        '
        'LblStartDate
        '
        Me.LblStartDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.LblStartDate.Location = New System.Drawing.Point(393, 35)
        Me.LblStartDate.Name = "LblStartDate"
        Me.LblStartDate.Size = New System.Drawing.Size(58, 16)
        Me.LblStartDate.TabIndex = 135
        Me.LblStartDate.Text = "Start Time"
        '
        'LblEndTime
        '
        Me.LblEndTime.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.LblEndTime.Location = New System.Drawing.Point(586, 35)
        Me.LblEndTime.Name = "LblEndTime"
        Me.LblEndTime.Size = New System.Drawing.Size(55, 16)
        Me.LblEndTime.TabIndex = 137
        Me.LblEndTime.Text = "End Time"
        '
        'txtroundcode
        '
        Me.txtroundcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtroundcode.Location = New System.Drawing.Point(81, 10)
        Me.txtroundcode.MaxLength = 200
        Me.txtroundcode.MendatroryField = False
        Me.txtroundcode.MyLinkLable1 = Me.MyLabel8
        Me.txtroundcode.MyLinkLable2 = Nothing
        Me.txtroundcode.Name = "txtroundcode"
        Me.txtroundcode.ReadOnly = True
        Me.txtroundcode.Size = New System.Drawing.Size(112, 20)
        Me.txtroundcode.TabIndex = 4
        '
        'dtpEndTime
        '
        Me.dtpEndTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpEndTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndTime.Location = New System.Drawing.Point(644, 33)
        Me.dtpEndTime.MendatroryField = True
        Me.dtpEndTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndTime.MyLinkLable1 = Me.LblEndTime
        Me.dtpEndTime.MyLinkLable2 = Nothing
        Me.dtpEndTime.Name = "dtpEndTime"
        Me.dtpEndTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndTime.Size = New System.Drawing.Size(124, 18)
        Me.dtpEndTime.TabIndex = 8
        Me.dtpEndTime.TabStop = False
        Me.dtpEndTime.Text = "03/05/2011 12:00 AM"
        Me.dtpEndTime.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'dtpStartTime
        '
        Me.dtpStartTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.dtpStartTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartTime.Location = New System.Drawing.Point(454, 34)
        Me.dtpStartTime.MendatroryField = True
        Me.dtpStartTime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartTime.MyLinkLable1 = Me.LblStartDate
        Me.dtpStartTime.MyLinkLable2 = Nothing
        Me.dtpStartTime.Name = "dtpStartTime"
        Me.dtpStartTime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartTime.Size = New System.Drawing.Size(124, 18)
        Me.dtpStartTime.TabIndex = 7
        Me.dtpStartTime.TabStop = False
        Me.dtpStartTime.Text = "03/05/2011 12:00 AM"
        Me.dtpStartTime.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'gvparameter
        '
        Me.gvparameter.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvparameter.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvparameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvparameter.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvparameter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gvparameter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvparameter.Location = New System.Drawing.Point(0, 0)
        '
        'gvparameter
        '
        Me.gvparameter.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvparameter.MasterTemplate.AllowAddNewRow = False
        Me.gvparameter.MasterTemplate.EnableGrouping = False
        Me.gvparameter.Name = "gvparameter"
        Me.gvparameter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvparameter.Size = New System.Drawing.Size(890, 134)
        Me.gvparameter.TabIndex = 11
        Me.gvparameter.TabStop = False
        Me.gvparameter.Text = "RadGridView1"
        '
        'txtclearingscore
        '
        Me.txtclearingscore.BackColor = System.Drawing.Color.White
        Me.txtclearingscore.DecimalPlaces = 3
        Me.txtclearingscore.Location = New System.Drawing.Point(341, 6)
        Me.txtclearingscore.MendatroryField = False
        Me.txtclearingscore.MyLinkLable1 = Me.MyLabel7
        Me.txtclearingscore.MyLinkLable2 = Nothing
        Me.txtclearingscore.Name = "txtclearingscore"
        Me.txtclearingscore.ReadOnly = True
        Me.txtclearingscore.Size = New System.Drawing.Size(75, 20)
        Me.txtclearingscore.TabIndex = 13
        Me.txtclearingscore.Text = "0"
        Me.txtclearingscore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtclearingscore.Value = 0
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(253, 8)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel7.TabIndex = 149
        Me.MyLabel7.Text = "Clearing Score"
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(742, 8)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel5.TabIndex = 147
        Me.MyLabel5.Text = "Percentage"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 28)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 16
        Me.btnsave.Text = "Save"
        '
        'txtscore
        '
        Me.txtscore.BackColor = System.Drawing.Color.White
        Me.txtscore.DecimalPlaces = 3
        Me.txtscore.Location = New System.Drawing.Point(564, 6)
        Me.txtscore.MendatroryField = False
        Me.txtscore.MyLinkLable1 = Me.MyLabel4
        Me.txtscore.MyLinkLable2 = Nothing
        Me.txtscore.Name = "txtscore"
        Me.txtscore.ReadOnly = True
        Me.txtscore.Size = New System.Drawing.Size(75, 20)
        Me.txtscore.TabIndex = 14
        Me.txtscore.Text = "0"
        Me.txtscore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtscore.Value = 0
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(524, 8)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel4.TabIndex = 145
        Me.MyLabel4.Text = "Score"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(78, 28)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 17
        Me.btndelete.Text = "Delete"
        '
        'txtpercentage
        '
        Me.txtpercentage.BackColor = System.Drawing.Color.White
        Me.txtpercentage.DecimalPlaces = 3
        Me.txtpercentage.Location = New System.Drawing.Point(810, 6)
        Me.txtpercentage.MendatroryField = False
        Me.txtpercentage.MyLinkLable1 = Me.MyLabel5
        Me.txtpercentage.MyLinkLable2 = Nothing
        Me.txtpercentage.Name = "txtpercentage"
        Me.txtpercentage.ReadOnly = True
        Me.txtpercentage.Size = New System.Drawing.Size(75, 20)
        Me.txtpercentage.TabIndex = 15
        Me.txtpercentage.Text = "0"
        Me.txtpercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtpercentage.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 8)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel2.TabIndex = 143
        Me.MyLabel2.Text = "Total Score"
        '
        'txttotalscore
        '
        Me.txttotalscore.BackColor = System.Drawing.Color.White
        Me.txttotalscore.DecimalPlaces = 3
        Me.txttotalscore.Location = New System.Drawing.Point(75, 6)
        Me.txttotalscore.MendatroryField = False
        Me.txttotalscore.MyLinkLable1 = Me.MyLabel2
        Me.txttotalscore.MyLinkLable2 = Nothing
        Me.txttotalscore.Name = "txttotalscore"
        Me.txttotalscore.ReadOnly = True
        Me.txttotalscore.Size = New System.Drawing.Size(75, 20)
        Me.txttotalscore.TabIndex = 12
        Me.txttotalscore.Text = "0"
        Me.txttotalscore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txttotalscore.Value = 0
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpost.Location = New System.Drawing.Point(10, 10)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(66, 18)
        Me.btnpost.TabIndex = 18
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(822, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 19
        Me.btnclose.Text = "Close"
        '
        'LblDec
        '
        Me.LblDec.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LblDec.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDec.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblDec.Location = New System.Drawing.Point(513, 12)
        Me.LblDec.Name = "LblDec"
        Me.LblDec.Size = New System.Drawing.Size(96, 16)
        Me.LblDec.TabIndex = 133
        Me.LblDec.Text = "NOT DECLARED"
        '
        'FrmInterviewFeedback
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 680)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmInterviewFeedback"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Interview Feedback"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFeedbackCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gvround.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvround, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBoxPar.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.Panel2.PerformLayout()
        Me.SplitContainer6.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.LblResch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremark1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblround, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcomments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFinalAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbFinalAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblEndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtroundcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStartTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvparameter.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvparameter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtclearingscore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtscore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotalscore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblDec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents CmbFinalAction As common.Controls.MyComboBox
    Friend WithEvents LblFinalAction As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents cmbAction As common.Controls.MyComboBox
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtcomments As common.Controls.MyTextBox
    Friend WithEvents txtremark1 As common.Controls.MyTextBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gvround As common.UserControls.MyRadGridView
    Friend WithEvents GrpBoxPar As System.Windows.Forms.GroupBox
    Friend WithEvents gvparameter As common.UserControls.MyRadGridView
    Friend WithEvents UcRequisitionDetail1 As XpertERPHRandPayroll.ucRequisitionDetail
    Friend WithEvents txtFeedbackCode As common.Controls.MyTextBox
    Friend WithEvents LblEndTime As common.Controls.MyLabel
    Friend WithEvents LblStartDate As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtroundcode As common.Controls.MyTextBox
    Friend WithEvents dtpStartTime As common.Controls.MyDateTimePicker
    Friend WithEvents dtpEndTime As common.Controls.MyDateTimePicker
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtclearingscore As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txttotalscore As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtpercentage As common.MyNumBox
    Friend WithEvents txtscore As common.MyNumBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblround As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents LblResch As common.Controls.MyLabel
    Friend WithEvents LblDec As common.Controls.MyLabel
End Class

