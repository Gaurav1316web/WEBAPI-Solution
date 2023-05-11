Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReferenceCheck
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
        Me.grpmain = New Telerik.WinControls.UI.RadGroupBox
        Me.ChkPastEmp = New Telerik.WinControls.UI.RadCheckBox
        Me.grpPastEmp = New Telerik.WinControls.UI.RadGroupBox
        Me.cmbMOCPast = New common.Controls.MyComboBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.cmbFBPast = New common.Controls.MyComboBox
        Me.MyLabel5 = New common.Controls.MyLabel
        Me.txtRemarksPast = New common.Controls.MyTextBox
        Me.MyLabel6 = New common.Controls.MyLabel
        Me.chkDetailsCand = New Telerik.WinControls.UI.RadCheckBox
        Me.dtpDate = New common.Controls.MyDateTimePicker
        Me.MyLabel12 = New common.Controls.MyLabel
        Me.cmbFinalFeedback = New common.Controls.MyComboBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.grpCand = New Telerik.WinControls.UI.RadGroupBox
        Me.cmbMOCCand = New common.Controls.MyComboBox
        Me.MyLabel8 = New common.Controls.MyLabel
        Me.cmbFBCand = New common.Controls.MyComboBox
        Me.MyLabel9 = New common.Controls.MyLabel
        Me.txtRemarksCand = New common.Controls.MyTextBox
        Me.MyLabel10 = New common.Controls.MyLabel
        Me.LblInitiateBy = New common.Controls.MyLabel
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.TxtInitiateBy = New common.UserControls.txtFinder
        Me.lblRelation = New common.Controls.MyLabel
        Me.lblEmpName = New common.Controls.MyLabel
        Me.LblRefBy = New common.Controls.MyLabel
        Me.txtEmpCode = New common.UserControls.txtFinder
        Me.MyLabel18 = New common.Controls.MyLabel
        Me.txtRelation = New common.UserControls.txtFinder
        Me.rbnrefbyAge = New common.Controls.MyRadioButton
        Me.rbnRefbyEmp = New common.Controls.MyRadioButton
        Me.ChkOverride = New Telerik.WinControls.UI.RadCheckBox
        Me.grpOvride = New Telerik.WinControls.UI.RadGroupBox
        Me.MyLabel24 = New common.Controls.MyLabel
        Me.txtremarks = New common.Controls.MyTextBox
        Me.UsLock1 = New common.usLock
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.btnnew = New Telerik.WinControls.UI.RadButton
        Me.txtAppcode = New common.UserControls.txtNavigator
        Me.UcRequisitionDetail1 = New XpertERPHRandPayroll.ucRequisitionDetail
        Me.btnDelete = New Telerik.WinControls.UI.RadButton
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnpost = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.LblStatus = New common.Controls.MyLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grpmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpmain.SuspendLayout()
        CType(Me.ChkPastEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPastEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPastEmp.SuspendLayout()
        CType(Me.cmbMOCPast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFBPast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarksPast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDetailsCand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFinalFeedback, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpCand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCand.SuspendLayout()
        CType(Me.cmbMOCCand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFBCand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarksCand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblInitiateBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRelation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblRefBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbnrefbyAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbnRefbyEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkOverride, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpOvride, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOvride.SuspendLayout()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.LblStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpmain)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ChkOverride)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpOvride)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtAppcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UcRequisitionDetail1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnpost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(754, 498)
        Me.SplitContainer1.SplitterDistance = 458
        Me.SplitContainer1.TabIndex = 0
        '
        'grpmain
        '
        Me.grpmain.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpmain.Controls.Add(Me.ChkPastEmp)
        Me.grpmain.Controls.Add(Me.grpPastEmp)
        Me.grpmain.Controls.Add(Me.chkDetailsCand)
        Me.grpmain.Controls.Add(Me.dtpDate)
        Me.grpmain.Controls.Add(Me.cmbFinalFeedback)
        Me.grpmain.Controls.Add(Me.MyLabel12)
        Me.grpmain.Controls.Add(Me.MyLabel4)
        Me.grpmain.Controls.Add(Me.grpCand)
        Me.grpmain.Controls.Add(Me.LblInitiateBy)
        Me.grpmain.Controls.Add(Me.MyLabel3)
        Me.grpmain.Controls.Add(Me.TxtInitiateBy)
        Me.grpmain.Controls.Add(Me.lblRelation)
        Me.grpmain.Controls.Add(Me.lblEmpName)
        Me.grpmain.Controls.Add(Me.LblRefBy)
        Me.grpmain.Controls.Add(Me.txtEmpCode)
        Me.grpmain.Controls.Add(Me.MyLabel18)
        Me.grpmain.Controls.Add(Me.txtRelation)
        Me.grpmain.Controls.Add(Me.rbnrefbyAge)
        Me.grpmain.Controls.Add(Me.rbnRefbyEmp)
        Me.grpmain.HeaderText = ""
        Me.grpmain.Location = New System.Drawing.Point(6, 176)
        Me.grpmain.Name = "grpmain"
        Me.grpmain.Size = New System.Drawing.Size(744, 276)
        Me.grpmain.TabIndex = 5
        '
        'ChkPastEmp
        '
        Me.ChkPastEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPastEmp.Location = New System.Drawing.Point(13, 103)
        Me.ChkPastEmp.Name = "ChkPastEmp"
        Me.ChkPastEmp.Size = New System.Drawing.Size(178, 16)
        Me.ChkPastEmp.TabIndex = 7
        Me.ChkPastEmp.Text = "Details Of Past Employement"
        '
        'grpPastEmp
        '
        Me.grpPastEmp.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpPastEmp.Controls.Add(Me.cmbMOCPast)
        Me.grpPastEmp.Controls.Add(Me.MyLabel2)
        Me.grpPastEmp.Controls.Add(Me.cmbFBPast)
        Me.grpPastEmp.Controls.Add(Me.MyLabel5)
        Me.grpPastEmp.Controls.Add(Me.txtRemarksPast)
        Me.grpPastEmp.Controls.Add(Me.MyLabel6)
        Me.grpPastEmp.HeaderText = ""
        Me.grpPastEmp.Location = New System.Drawing.Point(4, 111)
        Me.grpPastEmp.Name = "grpPastEmp"
        Me.grpPastEmp.Size = New System.Drawing.Size(560, 65)
        Me.grpPastEmp.TabIndex = 6
        '
        'cmbMOCPast
        '
        Me.cmbMOCPast.AllowShowFocusCues = False
        Me.cmbMOCPast.AutoCompleteDisplayMember = Nothing
        Me.cmbMOCPast.AutoCompleteValueMember = Nothing
        Me.cmbMOCPast.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbMOCPast.Location = New System.Drawing.Point(115, 15)
        Me.cmbMOCPast.MendatroryField = True
        Me.cmbMOCPast.MyLinkLable1 = Me.MyLabel2
        Me.cmbMOCPast.MyLinkLable2 = Nothing
        Me.cmbMOCPast.Name = "cmbMOCPast"
        Me.cmbMOCPast.Size = New System.Drawing.Size(141, 20)
        Me.cmbMOCPast.TabIndex = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(6, 17)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel2.TabIndex = 76
        Me.MyLabel2.Text = "Mode of Check"
        '
        'cmbFBPast
        '
        Me.cmbFBPast.AllowShowFocusCues = False
        Me.cmbFBPast.AutoCompleteDisplayMember = Nothing
        Me.cmbFBPast.AutoCompleteValueMember = Nothing
        Me.cmbFBPast.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFBPast.Location = New System.Drawing.Point(432, 15)
        Me.cmbFBPast.MendatroryField = True
        Me.cmbFBPast.MyLinkLable1 = Me.MyLabel5
        Me.cmbFBPast.MyLinkLable2 = Nothing
        Me.cmbFBPast.Name = "cmbFBPast"
        Me.cmbFBPast.Size = New System.Drawing.Size(122, 20)
        Me.cmbFBPast.TabIndex = 1
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(323, 17)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel5.TabIndex = 75
        Me.MyLabel5.Text = "Category Feedback"
        '
        'txtRemarksPast
        '
        Me.txtRemarksPast.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtRemarksPast.Location = New System.Drawing.Point(115, 38)
        Me.txtRemarksPast.MaxLength = 150
        Me.txtRemarksPast.MendatroryField = False
        Me.txtRemarksPast.MyLinkLable1 = Me.MyLabel6
        Me.txtRemarksPast.MyLinkLable2 = Nothing
        Me.txtRemarksPast.Name = "txtRemarksPast"
        Me.txtRemarksPast.Size = New System.Drawing.Size(438, 20)
        Me.txtRemarksPast.TabIndex = 2
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(6, 40)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel6.TabIndex = 74
        Me.MyLabel6.Text = "Feedback Remarks"
        '
        'chkDetailsCand
        '
        Me.chkDetailsCand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDetailsCand.Location = New System.Drawing.Point(13, 180)
        Me.chkDetailsCand.Name = "chkDetailsCand"
        Me.chkDetailsCand.Size = New System.Drawing.Size(131, 16)
        Me.chkDetailsCand.TabIndex = 9
        Me.chkDetailsCand.Text = "Details Of Candidate"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(645, 34)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.MyLabel12
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(86, 18)
        Me.dtpDate.TabIndex = 3
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011 "
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(609, 36)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel12.TabIndex = 130
        Me.MyLabel12.Text = "Date"
        '
        'cmbFinalFeedback
        '
        Me.cmbFinalFeedback.AllowShowFocusCues = False
        Me.cmbFinalFeedback.AutoCompleteDisplayMember = Nothing
        Me.cmbFinalFeedback.AutoCompleteValueMember = Nothing
        Me.cmbFinalFeedback.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFinalFeedback.Location = New System.Drawing.Point(119, 251)
        Me.cmbFinalFeedback.MendatroryField = True
        Me.cmbFinalFeedback.MyLinkLable1 = Me.MyLabel4
        Me.cmbFinalFeedback.MyLinkLable2 = Nothing
        Me.cmbFinalFeedback.Name = "cmbFinalFeedback"
        Me.cmbFinalFeedback.Size = New System.Drawing.Size(141, 20)
        Me.cmbFinalFeedback.TabIndex = 10
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(13, 253)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel4.TabIndex = 64
        Me.MyLabel4.Text = "Final Feedback"
        '
        'grpCand
        '
        Me.grpCand.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCand.Controls.Add(Me.cmbMOCCand)
        Me.grpCand.Controls.Add(Me.MyLabel8)
        Me.grpCand.Controls.Add(Me.cmbFBCand)
        Me.grpCand.Controls.Add(Me.MyLabel9)
        Me.grpCand.Controls.Add(Me.txtRemarksCand)
        Me.grpCand.Controls.Add(Me.MyLabel10)
        Me.grpCand.HeaderText = ""
        Me.grpCand.Location = New System.Drawing.Point(4, 188)
        Me.grpCand.Name = "grpCand"
        Me.grpCand.Size = New System.Drawing.Size(560, 58)
        Me.grpCand.TabIndex = 8
        '
        'cmbMOCCand
        '
        Me.cmbMOCCand.AllowShowFocusCues = False
        Me.cmbMOCCand.AutoCompleteDisplayMember = Nothing
        Me.cmbMOCCand.AutoCompleteValueMember = Nothing
        Me.cmbMOCCand.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbMOCCand.Location = New System.Drawing.Point(115, 11)
        Me.cmbMOCCand.MendatroryField = True
        Me.cmbMOCCand.MyLinkLable1 = Me.MyLabel8
        Me.cmbMOCCand.MyLinkLable2 = Nothing
        Me.cmbMOCCand.Name = "cmbMOCCand"
        Me.cmbMOCCand.Size = New System.Drawing.Size(141, 20)
        Me.cmbMOCCand.TabIndex = 0
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(6, 13)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(82, 16)
        Me.MyLabel8.TabIndex = 70
        Me.MyLabel8.Text = "Mode of Check"
        '
        'cmbFBCand
        '
        Me.cmbFBCand.AllowShowFocusCues = False
        Me.cmbFBCand.AutoCompleteDisplayMember = Nothing
        Me.cmbFBCand.AutoCompleteValueMember = Nothing
        Me.cmbFBCand.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbFBCand.Location = New System.Drawing.Point(431, 11)
        Me.cmbFBCand.MendatroryField = True
        Me.cmbFBCand.MyLinkLable1 = Me.MyLabel9
        Me.cmbFBCand.MyLinkLable2 = Nothing
        Me.cmbFBCand.Name = "cmbFBCand"
        Me.cmbFBCand.Size = New System.Drawing.Size(122, 20)
        Me.cmbFBCand.TabIndex = 1
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(322, 13)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(105, 16)
        Me.MyLabel9.TabIndex = 69
        Me.MyLabel9.Text = "Category Feedback"
        '
        'txtRemarksCand
        '
        Me.txtRemarksCand.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtRemarksCand.Location = New System.Drawing.Point(115, 34)
        Me.txtRemarksCand.MaxLength = 150
        Me.txtRemarksCand.MendatroryField = False
        Me.txtRemarksCand.MyLinkLable1 = Me.MyLabel10
        Me.txtRemarksCand.MyLinkLable2 = Nothing
        Me.txtRemarksCand.Name = "txtRemarksCand"
        Me.txtRemarksCand.Size = New System.Drawing.Size(438, 20)
        Me.txtRemarksCand.TabIndex = 2
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(6, 36)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel10.TabIndex = 68
        Me.MyLabel10.Text = "Feedback Remarks"
        '
        'LblInitiateBy
        '
        Me.LblInitiateBy.AutoSize = False
        Me.LblInitiateBy.BorderVisible = True
        Me.LblInitiateBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInitiateBy.Location = New System.Drawing.Point(275, 79)
        Me.LblInitiateBy.Name = "LblInitiateBy"
        Me.LblInitiateBy.Size = New System.Drawing.Size(289, 18)
        Me.LblInitiateBy.TabIndex = 147
        Me.LblInitiateBy.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblInitiateBy.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 80)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel3.TabIndex = 146
        Me.MyLabel3.Text = "Initiate By"
        '
        'TxtInitiateBy
        '
        Me.TxtInitiateBy.Location = New System.Drawing.Point(88, 79)
        Me.TxtInitiateBy.MendatroryField = True
        Me.TxtInitiateBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInitiateBy.MyLinkLable1 = Me.MyLabel3
        Me.TxtInitiateBy.MyLinkLable2 = Nothing
        Me.TxtInitiateBy.MyReadOnly = False
        Me.TxtInitiateBy.MyShowMasterFormButton = False
        Me.TxtInitiateBy.Name = "TxtInitiateBy"
        Me.TxtInitiateBy.Size = New System.Drawing.Size(182, 19)
        Me.TxtInitiateBy.TabIndex = 5
        Me.TxtInitiateBy.Value = ""
        '
        'lblRelation
        '
        Me.lblRelation.AutoSize = False
        Me.lblRelation.BorderVisible = True
        Me.lblRelation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelation.Location = New System.Drawing.Point(275, 57)
        Me.lblRelation.Name = "lblRelation"
        Me.lblRelation.Size = New System.Drawing.Size(289, 18)
        Me.lblRelation.TabIndex = 145
        Me.lblRelation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRelation.TextWrap = False
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = False
        Me.lblEmpName.BorderVisible = True
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpName.Location = New System.Drawing.Point(275, 35)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(289, 18)
        Me.lblEmpName.TabIndex = 144
        Me.lblEmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblEmpName.TextWrap = False
        '
        'LblRefBy
        '
        Me.LblRefBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRefBy.Location = New System.Drawing.Point(7, 36)
        Me.LblRefBy.Name = "LblRefBy"
        Me.LblRefBy.Size = New System.Drawing.Size(60, 16)
        Me.LblRefBy.TabIndex = 143
        Me.LblRefBy.Text = "Emp Code"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.Location = New System.Drawing.Point(88, 35)
        Me.txtEmpCode.MendatroryField = False
        Me.txtEmpCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.MyLinkLable1 = Me.LblRefBy
        Me.txtEmpCode.MyLinkLable2 = Nothing
        Me.txtEmpCode.MyReadOnly = True
        Me.txtEmpCode.MyShowMasterFormButton = False
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.Size = New System.Drawing.Size(182, 19)
        Me.txtEmpCode.TabIndex = 2
        Me.txtEmpCode.Value = ""
        '
        'MyLabel18
        '
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(7, 59)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel18.TabIndex = 142
        Me.MyLabel18.Text = "Relation"
        '
        'txtRelation
        '
        Me.txtRelation.Location = New System.Drawing.Point(88, 57)
        Me.txtRelation.MendatroryField = False
        Me.txtRelation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRelation.MyLinkLable1 = Me.MyLabel18
        Me.txtRelation.MyLinkLable2 = Nothing
        Me.txtRelation.MyReadOnly = True
        Me.txtRelation.MyShowMasterFormButton = False
        Me.txtRelation.Name = "txtRelation"
        Me.txtRelation.Size = New System.Drawing.Size(182, 19)
        Me.txtRelation.TabIndex = 4
        Me.txtRelation.Value = ""
        '
        'rbnrefbyAge
        '
        Me.rbnrefbyAge.Location = New System.Drawing.Point(190, 10)
        Me.rbnrefbyAge.MyLinkLable1 = Nothing
        Me.rbnrefbyAge.MyLinkLable2 = Nothing
        Me.rbnrefbyAge.Name = "rbnrefbyAge"
        Me.rbnrefbyAge.Size = New System.Drawing.Size(119, 18)
        Me.rbnrefbyAge.TabIndex = 1
        Me.rbnrefbyAge.Text = "Refrence By Agency"
        '
        'rbnRefbyEmp
        '
        Me.rbnRefbyEmp.Location = New System.Drawing.Point(13, 10)
        Me.rbnRefbyEmp.MyLinkLable1 = Nothing
        Me.rbnRefbyEmp.MyLinkLable2 = Nothing
        Me.rbnRefbyEmp.Name = "rbnRefbyEmp"
        Me.rbnRefbyEmp.Size = New System.Drawing.Size(131, 18)
        Me.rbnRefbyEmp.TabIndex = 0
        Me.rbnRefbyEmp.Text = "Refrence by Employee"
        '
        'ChkOverride
        '
        Me.ChkOverride.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkOverride.Location = New System.Drawing.Point(17, 129)
        Me.ChkOverride.Name = "ChkOverride"
        Me.ChkOverride.Size = New System.Drawing.Size(67, 16)
        Me.ChkOverride.TabIndex = 3
        Me.ChkOverride.Text = "Override"
        '
        'grpOvride
        '
        Me.grpOvride.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpOvride.Controls.Add(Me.MyLabel24)
        Me.grpOvride.Controls.Add(Me.txtremarks)
        Me.grpOvride.HeaderText = ""
        Me.grpOvride.Location = New System.Drawing.Point(8, 137)
        Me.grpOvride.Name = "grpOvride"
        Me.grpOvride.Size = New System.Drawing.Size(542, 37)
        Me.grpOvride.TabIndex = 2
        '
        'MyLabel24
        '
        Me.MyLabel24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel24.Location = New System.Drawing.Point(10, 12)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel24.TabIndex = 114
        Me.MyLabel24.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.txtremarks.Location = New System.Drawing.Point(69, 10)
        Me.txtremarks.MaxLength = 150
        Me.txtremarks.MendatroryField = False
        Me.txtremarks.MyLinkLable1 = Me.MyLabel24
        Me.txtremarks.MyLinkLable2 = Nothing
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(465, 20)
        Me.txtremarks.TabIndex = 0
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(651, 12)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 117
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(15, 13)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel1.TabIndex = 116
        Me.MyLabel1.Text = "Applicant Code"
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(311, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 1
        '
        'txtAppcode
        '
        Me.txtAppcode.Location = New System.Drawing.Point(109, 11)
        Me.txtAppcode.MendatroryField = True
        Me.txtAppcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAppcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtAppcode.MyLinkLable1 = Me.MyLabel1
        Me.txtAppcode.MyLinkLable2 = Nothing
        Me.txtAppcode.MyMaxLength = 30
        Me.txtAppcode.MyReadOnly = False
        Me.txtAppcode.Name = "txtAppcode"
        Me.txtAppcode.Size = New System.Drawing.Size(202, 21)
        Me.txtAppcode.TabIndex = 0
        Me.txtAppcode.TabStop = False
        Me.txtAppcode.Value = ""
        '
        'UcRequisitionDetail1
        '
        Me.UcRequisitionDetail1.AppCode = ""
        Me.UcRequisitionDetail1.AppDate = ""
        Me.UcRequisitionDetail1.AppName = ""
        Me.UcRequisitionDetail1.DateofBirth = ""
        Me.UcRequisitionDetail1.Email = ""
        Me.UcRequisitionDetail1.Location = New System.Drawing.Point(8, 33)
        Me.UcRequisitionDetail1.Name = "UcRequisitionDetail1"
        Me.UcRequisitionDetail1.ReqCode = ""
        Me.UcRequisitionDetail1.Size = New System.Drawing.Size(742, 93)
        Me.UcRequisitionDetail1.TabIndex = 115
        Me.UcRequisitionDetail1.TelephoneNo = ""
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(78, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(8, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btnpost
        '
        Me.btnpost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpost.Location = New System.Drawing.Point(149, 9)
        Me.btnpost.Name = "btnpost"
        Me.btnpost.Size = New System.Drawing.Size(66, 18)
        Me.btnpost.TabIndex = 2
        Me.btnpost.Text = "Post"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(683, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'LblStatus
        '
        Me.LblStatus.BackColor = System.Drawing.Color.Wheat
        Me.LblStatus.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblStatus.Location = New System.Drawing.Point(673, 13)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(76, 18)
        Me.LblStatus.TabIndex = 118
        Me.LblStatus.Text = "REJECTED"
        Me.LblStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmReferenceCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 498)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmReferenceCheck"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Reference Check"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grpmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpmain.ResumeLayout(False)
        Me.grpmain.PerformLayout()
        CType(Me.ChkPastEmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPastEmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPastEmp.ResumeLayout(False)
        Me.grpPastEmp.PerformLayout()
        CType(Me.cmbMOCPast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFBPast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarksPast, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDetailsCand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFinalFeedback, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpCand, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCand.ResumeLayout(False)
        Me.grpCand.PerformLayout()
        CType(Me.cmbMOCCand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFBCand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarksCand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblInitiateBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRelation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmpName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblRefBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbnrefbyAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbnRefbyEmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkOverride, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpOvride, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOvride.ResumeLayout(False)
        Me.grpOvride.PerformLayout()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtremarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtAppcode As common.UserControls.txtNavigator
    Friend WithEvents UcRequisitionDetail1 As XpertERPHRandPayroll.ucRequisitionDetail
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents cmbFinalFeedback As common.Controls.MyComboBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnpost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkOverride As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents grpOvride As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents txtremarks As common.Controls.MyTextBox
    Friend WithEvents grpmain As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkPastEmp As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents grpPastEmp As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cmbMOCPast As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents cmbFBPast As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtRemarksPast As common.Controls.MyTextBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents chkDetailsCand As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents grpCand As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cmbMOCCand As common.Controls.MyComboBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents cmbFBCand As common.Controls.MyComboBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents txtRemarksCand As common.Controls.MyTextBox
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents LblInitiateBy As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtInitiateBy As common.UserControls.txtFinder
    Friend WithEvents lblRelation As common.Controls.MyLabel
    Friend WithEvents lblEmpName As common.Controls.MyLabel
    Friend WithEvents LblRefBy As common.Controls.MyLabel
    Friend WithEvents txtEmpCode As common.UserControls.txtFinder
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtRelation As common.UserControls.txtFinder
    Friend WithEvents rbnrefbyAge As common.Controls.MyRadioButton
    Friend WithEvents rbnRefbyEmp As common.Controls.MyRadioButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents LblStatus As common.Controls.MyLabel
End Class

