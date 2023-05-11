Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExitInterview
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
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtPositionName = New common.Controls.MyLabel()
        Me.txtDepartmentName = New common.Controls.MyLabel()
        Me.txtNameCode = New common.Controls.MyLabel()
        Me.lblvandorno = New common.Controls.MyLabel()
        Me.ddlRecommend = New common.Controls.MyComboBox()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.ddlReturnToWorkHere = New common.Controls.MyComboBox()
        Me.lblName = New common.Controls.MyLabel()
        Me.ddlResonOfLeavingType = New common.Controls.MyComboBox()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.lblFriendRecom = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lbldate = New common.Controls.MyLabel()
        Me.lblReturntoWorkHere = New common.Controls.MyLabel()
        Me.txtSuggestion = New common.Controls.MyTextBox()
        Me.txtName = New common.Controls.MyLabel()
        Me.lblSuggestion = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.txtDetailReson = New common.Controls.MyTextBox()
        Me.txtDepartment = New common.Controls.MyLabel()
        Me.lblDetailReson = New common.Controls.MyLabel()
        Me.lblPosition = New common.Controls.MyLabel()
        Me.lblResonOfLeaving = New common.Controls.MyLabel()
        Me.txtPosition = New common.Controls.MyLabel()
        Me.txtSupervisorName = New common.Controls.MyLabel()
        Me.lblSuperVisor = New common.Controls.MyLabel()
        Me.txtSupervisorCode = New common.UserControls.txtFinder()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnprint = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.txtPositionName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNameCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlRecommend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlReturnToWorkHere, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddlResonOfLeavingType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFriendRecom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReturntoWorkHere, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSuggestion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSuggestion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDetailReson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDetailReson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblResonOfLeaving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSupervisorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSuperVisor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(769, 613)
        Me.SplitContainer1.SplitterDistance = 573
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(769, 573)
        Me.SplitContainer2.SplitterDistance = 242
        Me.SplitContainer2.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtPositionName)
        Me.Panel1.Controls.Add(Me.txtDepartmentName)
        Me.Panel1.Controls.Add(Me.txtNameCode)
        Me.Panel1.Controls.Add(Me.lblvandorno)
        Me.Panel1.Controls.Add(Me.ddlRecommend)
        Me.Panel1.Controls.Add(Me.btnnew)
        Me.Panel1.Controls.Add(Me.ddlReturnToWorkHere)
        Me.Panel1.Controls.Add(Me.lblName)
        Me.Panel1.Controls.Add(Me.ddlResonOfLeavingType)
        Me.Panel1.Controls.Add(Me.txtcode)
        Me.Panel1.Controls.Add(Me.lblFriendRecom)
        Me.Panel1.Controls.Add(Me.txtDate)
        Me.Panel1.Controls.Add(Me.lblReturntoWorkHere)
        Me.Panel1.Controls.Add(Me.lbldate)
        Me.Panel1.Controls.Add(Me.txtSuggestion)
        Me.Panel1.Controls.Add(Me.txtName)
        Me.Panel1.Controls.Add(Me.lblSuggestion)
        Me.Panel1.Controls.Add(Me.lblDepartment)
        Me.Panel1.Controls.Add(Me.txtDetailReson)
        Me.Panel1.Controls.Add(Me.txtDepartment)
        Me.Panel1.Controls.Add(Me.lblDetailReson)
        Me.Panel1.Controls.Add(Me.lblPosition)
        Me.Panel1.Controls.Add(Me.lblResonOfLeaving)
        Me.Panel1.Controls.Add(Me.txtPosition)
        Me.Panel1.Controls.Add(Me.txtSupervisorName)
        Me.Panel1.Controls.Add(Me.lblSuperVisor)
        Me.Panel1.Controls.Add(Me.txtSupervisorCode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(769, 242)
        Me.Panel1.TabIndex = 449
        '
        'txtPositionName
        '
        Me.txtPositionName.AutoSize = False
        Me.txtPositionName.BorderVisible = True
        Me.txtPositionName.Location = New System.Drawing.Point(311, 81)
        Me.txtPositionName.Name = "txtPositionName"
        Me.txtPositionName.Size = New System.Drawing.Size(212, 19)
        Me.txtPositionName.TabIndex = 451
        Me.txtPositionName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDepartmentName
        '
        Me.txtDepartmentName.AutoSize = False
        Me.txtDepartmentName.BorderVisible = True
        Me.txtDepartmentName.Location = New System.Drawing.Point(311, 59)
        Me.txtDepartmentName.Name = "txtDepartmentName"
        Me.txtDepartmentName.Size = New System.Drawing.Size(212, 19)
        Me.txtDepartmentName.TabIndex = 450
        Me.txtDepartmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNameCode
        '
        Me.txtNameCode.AutoSize = False
        Me.txtNameCode.BorderVisible = True
        Me.txtNameCode.Location = New System.Drawing.Point(91, 38)
        Me.txtNameCode.Name = "txtNameCode"
        Me.txtNameCode.Size = New System.Drawing.Size(212, 19)
        Me.txtNameCode.TabIndex = 449
        Me.txtNameCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblvandorno
        '
        Me.lblvandorno.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblvandorno.Location = New System.Drawing.Point(14, 12)
        Me.lblvandorno.Name = "lblvandorno"
        Me.lblvandorno.Size = New System.Drawing.Size(33, 16)
        Me.lblvandorno.TabIndex = 23
        Me.lblvandorno.Text = "Code"
        '
        'ddlRecommend
        '
        Me.ddlRecommend.AutoCompleteDisplayMember = Nothing
        Me.ddlRecommend.AutoCompleteValueMember = Nothing
        Me.ddlRecommend.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Male"
        RadListDataItem2.Text = "Female"
        RadListDataItem3.Text = "Both"
        Me.ddlRecommend.Items.Add(RadListDataItem1)
        Me.ddlRecommend.Items.Add(RadListDataItem2)
        Me.ddlRecommend.Items.Add(RadListDataItem3)
        Me.ddlRecommend.Location = New System.Drawing.Point(656, 208)
        Me.ddlRecommend.MendatroryField = False
        Me.ddlRecommend.MyLinkLable1 = Nothing
        Me.ddlRecommend.MyLinkLable2 = Nothing
        Me.ddlRecommend.Name = "ddlRecommend"
        Me.ddlRecommend.Size = New System.Drawing.Size(100, 18)
        Me.ddlRecommend.TabIndex = 448
        '
        'btnnew
        '
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(293, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(15, 20)
        Me.btnnew.TabIndex = 21
        '
        'ddlReturnToWorkHere
        '
        Me.ddlReturnToWorkHere.AutoCompleteDisplayMember = Nothing
        Me.ddlReturnToWorkHere.AutoCompleteValueMember = Nothing
        Me.ddlReturnToWorkHere.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem4.Text = "Male"
        RadListDataItem5.Text = "Female"
        RadListDataItem6.Text = "Both"
        Me.ddlReturnToWorkHere.Items.Add(RadListDataItem4)
        Me.ddlReturnToWorkHere.Items.Add(RadListDataItem5)
        Me.ddlReturnToWorkHere.Items.Add(RadListDataItem6)
        Me.ddlReturnToWorkHere.Location = New System.Drawing.Point(656, 182)
        Me.ddlReturnToWorkHere.MendatroryField = False
        Me.ddlReturnToWorkHere.MyLinkLable1 = Nothing
        Me.ddlReturnToWorkHere.MyLinkLable2 = Nothing
        Me.ddlReturnToWorkHere.Name = "ddlReturnToWorkHere"
        Me.ddlReturnToWorkHere.Size = New System.Drawing.Size(100, 18)
        Me.ddlReturnToWorkHere.TabIndex = 447
        '
        'lblName
        '
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(14, 37)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 16)
        Me.lblName.TabIndex = 24
        Me.lblName.Text = "Name"
        '
        'ddlResonOfLeavingType
        '
        Me.ddlResonOfLeavingType.AutoCompleteDisplayMember = Nothing
        Me.ddlResonOfLeavingType.AutoCompleteValueMember = Nothing
        Me.ddlResonOfLeavingType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddlResonOfLeavingType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem7.Text = "Male"
        RadListDataItem8.Text = "Female"
        RadListDataItem9.Text = "Both"
        Me.ddlResonOfLeavingType.Items.Add(RadListDataItem7)
        Me.ddlResonOfLeavingType.Items.Add(RadListDataItem8)
        Me.ddlResonOfLeavingType.Items.Add(RadListDataItem9)
        Me.ddlResonOfLeavingType.Location = New System.Drawing.Point(638, 106)
        Me.ddlResonOfLeavingType.MendatroryField = False
        Me.ddlResonOfLeavingType.MyLinkLable1 = Nothing
        Me.ddlResonOfLeavingType.MyLinkLable2 = Nothing
        Me.ddlResonOfLeavingType.Name = "ddlResonOfLeavingType"
        Me.ddlResonOfLeavingType.Size = New System.Drawing.Size(127, 18)
        Me.ddlResonOfLeavingType.TabIndex = 446
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(91, 11)
        Me.txtcode.MendatroryField = False
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblvandorno
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(202, 21)
        Me.txtcode.TabIndex = 20
        Me.txtcode.TabStop = False
        Me.txtcode.Value = ""
        '
        'lblFriendRecom
        '
        Me.lblFriendRecom.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblFriendRecom.Location = New System.Drawing.Point(527, 208)
        Me.lblFriendRecom.Name = "lblFriendRecom"
        Me.lblFriendRecom.Size = New System.Drawing.Size(106, 16)
        Me.lblFriendRecom.TabIndex = 444
        Me.lblFriendRecom.Text = "Friend Recommend"
        '
        'txtDate
        '
        Me.txtDate.CustomFormat = "dd/MM/yyyy"
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtDate.Location = New System.Drawing.Point(349, 14)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lbldate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.Size = New System.Drawing.Size(78, 18)
        Me.txtDate.TabIndex = 25
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lbldate
        '
        Me.lbldate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbldate.Location = New System.Drawing.Point(314, 15)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(33, 16)
        Me.lbldate.TabIndex = 26
        Me.lbldate.Text = " Date"
        '
        'lblReturntoWorkHere
        '
        Me.lblReturntoWorkHere.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblReturntoWorkHere.Location = New System.Drawing.Point(527, 181)
        Me.lblReturntoWorkHere.Name = "lblReturntoWorkHere"
        Me.lblReturntoWorkHere.Size = New System.Drawing.Size(114, 16)
        Me.lblReturntoWorkHere.TabIndex = 442
        Me.lblReturntoWorkHere.Text = "Return To Work Here"
        '
        'txtSuggestion
        '
        Me.txtSuggestion.AutoSize = False
        Me.txtSuggestion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuggestion.Location = New System.Drawing.Point(91, 178)
        Me.txtSuggestion.MaxLength = 150
        Me.txtSuggestion.MendatroryField = False
        Me.txtSuggestion.Multiline = True
        Me.txtSuggestion.MyLinkLable1 = Nothing
        Me.txtSuggestion.MyLinkLable2 = Nothing
        Me.txtSuggestion.Name = "txtSuggestion"
        Me.txtSuggestion.Size = New System.Drawing.Size(432, 48)
        Me.txtSuggestion.TabIndex = 441
        '
        'txtName
        '
        Me.txtName.AutoSize = False
        Me.txtName.BorderVisible = True
        Me.txtName.Location = New System.Drawing.Point(312, 38)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(212, 19)
        Me.txtName.TabIndex = 428
        Me.txtName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuggestion
        '
        Me.lblSuggestion.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSuggestion.Location = New System.Drawing.Point(14, 181)
        Me.lblSuggestion.Name = "lblSuggestion"
        Me.lblSuggestion.Size = New System.Drawing.Size(63, 16)
        Me.lblSuggestion.TabIndex = 440
        Me.lblSuggestion.Text = "Suggestion"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(14, 59)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(65, 16)
        Me.lblDepartment.TabIndex = 429
        Me.lblDepartment.Text = "Department"
        '
        'txtDetailReson
        '
        Me.txtDetailReson.AutoSize = False
        Me.txtDetailReson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDetailReson.Location = New System.Drawing.Point(91, 128)
        Me.txtDetailReson.MaxLength = 150
        Me.txtDetailReson.MendatroryField = False
        Me.txtDetailReson.Multiline = True
        Me.txtDetailReson.MyLinkLable1 = Nothing
        Me.txtDetailReson.MyLinkLable2 = Nothing
        Me.txtDetailReson.Name = "txtDetailReson"
        Me.txtDetailReson.Size = New System.Drawing.Size(674, 46)
        Me.txtDetailReson.TabIndex = 439
        '
        'txtDepartment
        '
        Me.txtDepartment.AutoSize = False
        Me.txtDepartment.BorderVisible = True
        Me.txtDepartment.Location = New System.Drawing.Point(91, 59)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(212, 19)
        Me.txtDepartment.TabIndex = 430
        Me.txtDepartment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDetailReson
        '
        Me.lblDetailReson.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDetailReson.Location = New System.Drawing.Point(14, 136)
        Me.lblDetailReson.Name = "lblDetailReson"
        Me.lblDetailReson.Size = New System.Drawing.Size(71, 16)
        Me.lblDetailReson.TabIndex = 438
        Me.lblDetailReson.Text = "Detail Reson"
        '
        'lblPosition
        '
        Me.lblPosition.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPosition.Location = New System.Drawing.Point(14, 81)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(46, 16)
        Me.lblPosition.TabIndex = 431
        Me.lblPosition.Text = "Position"
        '
        'lblResonOfLeaving
        '
        Me.lblResonOfLeaving.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblResonOfLeaving.Location = New System.Drawing.Point(538, 105)
        Me.lblResonOfLeaving.Name = "lblResonOfLeaving"
        Me.lblResonOfLeaving.Size = New System.Drawing.Size(97, 16)
        Me.lblResonOfLeaving.TabIndex = 436
        Me.lblResonOfLeaving.Text = "Reson Of Leaving"
        '
        'txtPosition
        '
        Me.txtPosition.AutoSize = False
        Me.txtPosition.BorderVisible = True
        Me.txtPosition.Location = New System.Drawing.Point(91, 81)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(212, 19)
        Me.txtPosition.TabIndex = 432
        Me.txtPosition.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSupervisorName
        '
        Me.txtSupervisorName.AutoSize = False
        Me.txtSupervisorName.BorderVisible = True
        Me.txtSupervisorName.Location = New System.Drawing.Point(311, 105)
        Me.txtSupervisorName.Name = "txtSupervisorName"
        Me.txtSupervisorName.Size = New System.Drawing.Size(212, 19)
        Me.txtSupervisorName.TabIndex = 435
        Me.txtSupervisorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperVisor
        '
        Me.lblSuperVisor.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSuperVisor.Location = New System.Drawing.Point(14, 103)
        Me.lblSuperVisor.Name = "lblSuperVisor"
        Me.lblSuperVisor.Size = New System.Drawing.Size(60, 16)
        Me.lblSuperVisor.TabIndex = 434
        Me.lblSuperVisor.Text = "Supervisor"
        '
        'txtSupervisorCode
        '
        Me.txtSupervisorCode.Location = New System.Drawing.Point(91, 103)
        Me.txtSupervisorCode.MendatroryField = True
        Me.txtSupervisorCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupervisorCode.MyLinkLable1 = Me.lblSuperVisor
        Me.txtSupervisorCode.MyLinkLable2 = Nothing
        Me.txtSupervisorCode.MyReadOnly = False
        Me.txtSupervisorCode.MyShowMasterFormButton = False
        Me.txtSupervisorCode.Name = "txtSupervisorCode"
        Me.txtSupervisorCode.Size = New System.Drawing.Size(212, 19)
        Me.txtSupervisorCode.TabIndex = 433
        Me.txtSupervisorCode.Value = ""
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.Name = "gv1"
        Me.gv1.Size = New System.Drawing.Size(769, 327)
        Me.gv1.TabIndex = 2
        Me.gv1.Text = "RadGridView1"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(10, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 18)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = ""
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(78, 7)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 18)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.AccessibleDescription = ""
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(688, 6)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "Close"
        '
        'btnprint
        '
        Me.btnprint.AccessibleDescription = ""
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(150, 7)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(66, 18)
        Me.btnprint.TabIndex = 10
        Me.btnprint.Text = "Print"
        '
        'FrmExitInterview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 613)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmExitInterview"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmExitInterview"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtPositionName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNameCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblvandorno, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlRecommend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlReturnToWorkHere, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddlResonOfLeavingType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFriendRecom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReturntoWorkHere, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSuggestion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSuggestion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDetailReson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDetailReson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblResonOfLeaving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSupervisorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSuperVisor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents lblvandorno As common.Controls.MyLabel
    Friend WithEvents lblName As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnDelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lbldate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtPosition As common.Controls.MyLabel
    Friend WithEvents lblPosition As common.Controls.MyLabel
    Friend WithEvents txtDepartment As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents txtName As common.Controls.MyLabel
    Friend WithEvents txtSupervisorCode As common.UserControls.txtFinder
    Friend WithEvents lblSuperVisor As common.Controls.MyLabel
    Friend WithEvents txtSupervisorName As common.Controls.MyLabel
    Friend WithEvents lblResonOfLeaving As common.Controls.MyLabel
    Friend WithEvents lblDetailReson As common.Controls.MyLabel
    Friend WithEvents txtDetailReson As common.Controls.MyTextBox
    Friend WithEvents lblSuggestion As common.Controls.MyLabel
    Friend WithEvents lblFriendRecom As common.Controls.MyLabel
    Friend WithEvents lblReturntoWorkHere As common.Controls.MyLabel
    Friend WithEvents txtSuggestion As common.Controls.MyTextBox
    Friend WithEvents ddlRecommend As common.Controls.MyComboBox
    Friend WithEvents ddlReturnToWorkHere As common.Controls.MyComboBox
    Friend WithEvents ddlResonOfLeavingType As common.Controls.MyComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents txtPositionName As common.Controls.MyLabel
    Friend WithEvents txtDepartmentName As common.Controls.MyLabel
    Friend WithEvents txtNameCode As common.Controls.MyLabel
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
End Class

