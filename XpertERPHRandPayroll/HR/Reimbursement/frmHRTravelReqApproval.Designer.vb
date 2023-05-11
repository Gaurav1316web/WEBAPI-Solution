Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHRTravelReqApproval
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ChkSearchDate = New Telerik.WinControls.UI.RadCheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.GrpSearchDate = New System.Windows.Forms.GroupBox()
        Me.lblPPDate = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.BtnShow = New Telerik.WinControls.UI.RadButton()
        Me.cmbTravelType = New common.Controls.MyComboBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.LblTravelCat = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.TxtTravelCat = New common.UserControls.txtFinder()
        Me.LblTravelPurpose = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.TxtTravelPurpose = New common.UserControls.txtFinder()
        Me.LblTravelBookingFor = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.TxtTravelBookingFor = New common.UserControls.txtFinder()
        Me.LblTravelBookingBy = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.TxtTravelBookingBy = New common.UserControls.txtFinder()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.CmbStatus = New common.Controls.MyComboBox()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ChkSearchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpSearchDate.SuspendLayout()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTravelType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTravelCat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTravelPurpose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTravelBookingFor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTravelBookingBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1063, 477)
        Me.SplitContainer1.SplitterDistance = 424
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1063, 424)
        Me.SplitContainer2.SplitterDistance = 146
        Me.SplitContainer2.TabIndex = 6
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CmbStatus)
        Me.GroupBox2.Controls.Add(Me.MyLabel8)
        Me.GroupBox2.Controls.Add(Me.ChkSearchDate)
        Me.GroupBox2.Controls.Add(Me.Panel3)
        Me.GroupBox2.Controls.Add(Me.MyLabel6)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.MyLabel7)
        Me.GroupBox2.Controls.Add(Me.GrpSearchDate)
        Me.GroupBox2.Controls.Add(Me.BtnShow)
        Me.GroupBox2.Controls.Add(Me.cmbTravelType)
        Me.GroupBox2.Controls.Add(Me.MyLabel2)
        Me.GroupBox2.Controls.Add(Me.LblTravelCat)
        Me.GroupBox2.Controls.Add(Me.MyLabel5)
        Me.GroupBox2.Controls.Add(Me.TxtTravelCat)
        Me.GroupBox2.Controls.Add(Me.LblTravelPurpose)
        Me.GroupBox2.Controls.Add(Me.MyLabel4)
        Me.GroupBox2.Controls.Add(Me.TxtTravelPurpose)
        Me.GroupBox2.Controls.Add(Me.LblTravelBookingFor)
        Me.GroupBox2.Controls.Add(Me.MyLabel3)
        Me.GroupBox2.Controls.Add(Me.TxtTravelBookingFor)
        Me.GroupBox2.Controls.Add(Me.LblTravelBookingBy)
        Me.GroupBox2.Controls.Add(Me.MyLabel1)
        Me.GroupBox2.Controls.Add(Me.TxtTravelBookingBy)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1063, 146)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search Criteria"
        '
        'ChkSearchDate
        '
        Me.ChkSearchDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSearchDate.Location = New System.Drawing.Point(9, 105)
        Me.ChkSearchDate.Name = "ChkSearchDate"
        Me.ChkSearchDate.Size = New System.Drawing.Size(103, 16)
        Me.ChkSearchDate.TabIndex = 4
        Me.ChkSearchDate.Text = "Search By Date"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.LightGreen
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(900, 15)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(14, 13)
        Me.Panel3.TabIndex = 162
        '
        'MyLabel6
        '
        Me.MyLabel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(919, 13)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel6.TabIndex = 161
        Me.MyLabel6.Text = "Approved"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.LightSalmon
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(987, 15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(14, 13)
        Me.Panel1.TabIndex = 160
        '
        'MyLabel7
        '
        Me.MyLabel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(1003, 13)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel7.TabIndex = 159
        Me.MyLabel7.Text = "Rejected"
        '
        'GrpSearchDate
        '
        Me.GrpSearchDate.Controls.Add(Me.lblPPDate)
        Me.GrpSearchDate.Controls.Add(Me.dtpFromDate)
        Me.GrpSearchDate.Controls.Add(Me.MyLabel13)
        Me.GrpSearchDate.Controls.Add(Me.dtpToDate)
        Me.GrpSearchDate.Location = New System.Drawing.Point(5, 106)
        Me.GrpSearchDate.Name = "GrpSearchDate"
        Me.GrpSearchDate.Size = New System.Drawing.Size(577, 38)
        Me.GrpSearchDate.TabIndex = 158
        Me.GrpSearchDate.TabStop = False
        '
        'lblPPDate
        '
        Me.lblPPDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPPDate.Location = New System.Drawing.Point(7, 17)
        Me.lblPPDate.Name = "lblPPDate"
        Me.lblPPDate.Size = New System.Drawing.Size(60, 16)
        Me.lblPPDate.TabIndex = 156
        Me.lblPPDate.Text = "From Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(106, 16)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.lblPPDate
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(120, 18)
        Me.dtpFromDate.TabIndex = 0
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "03/05/2011 "
        Me.dtpFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(408, 17)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel13.TabIndex = 157
        Me.MyLabel13.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(462, 16)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel13
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(108, 18)
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "03/05/2011 "
        Me.dtpToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'BtnShow
        '
        Me.BtnShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnShow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShow.Location = New System.Drawing.Point(989, 122)
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.Size = New System.Drawing.Size(68, 18)
        Me.BtnShow.TabIndex = 7
        Me.BtnShow.Text = ">>>"
        '
        'cmbTravelType
        '
        Me.cmbTravelType.AutoCompleteDisplayMember = Nothing
        Me.cmbTravelType.AutoCompleteValueMember = Nothing
        Me.cmbTravelType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbTravelType.Location = New System.Drawing.Point(701, 40)
        Me.cmbTravelType.MendatroryField = True
        Me.cmbTravelType.MyLinkLable1 = Me.MyLabel2
        Me.cmbTravelType.MyLinkLable2 = Nothing
        Me.cmbTravelType.Name = "cmbTravelType"
        Me.cmbTravelType.Size = New System.Drawing.Size(107, 20)
        Me.cmbTravelType.TabIndex = 5
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(620, 42)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel2.TabIndex = 153
        Me.MyLabel2.Text = "Travel Type"
        '
        'LblTravelCat
        '
        Me.LblTravelCat.AutoSize = False
        Me.LblTravelCat.BorderVisible = True
        Me.LblTravelCat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTravelCat.Location = New System.Drawing.Point(239, 86)
        Me.LblTravelCat.Name = "LblTravelCat"
        Me.LblTravelCat.Size = New System.Drawing.Size(344, 18)
        Me.LblTravelCat.TabIndex = 151
        Me.LblTravelCat.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTravelCat.TextWrap = False
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(7, 88)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel5.TabIndex = 149
        Me.MyLabel5.Text = "Travel Category"
        '
        'TxtTravelCat
        '
        Me.TxtTravelCat.Location = New System.Drawing.Point(115, 86)
        Me.TxtTravelCat.MendatroryField = True
        Me.TxtTravelCat.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTravelCat.MyLinkLable1 = Nothing
        Me.TxtTravelCat.MyLinkLable2 = Nothing
        Me.TxtTravelCat.MyReadOnly = False
        Me.TxtTravelCat.MyShowMasterFormButton = False
        Me.TxtTravelCat.Name = "TxtTravelCat"
        Me.TxtTravelCat.Size = New System.Drawing.Size(120, 19)
        Me.TxtTravelCat.TabIndex = 3
        Me.TxtTravelCat.Value = ""
        '
        'LblTravelPurpose
        '
        Me.LblTravelPurpose.AutoSize = False
        Me.LblTravelPurpose.BorderVisible = True
        Me.LblTravelPurpose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTravelPurpose.Location = New System.Drawing.Point(239, 64)
        Me.LblTravelPurpose.Name = "LblTravelPurpose"
        Me.LblTravelPurpose.Size = New System.Drawing.Size(344, 18)
        Me.LblTravelPurpose.TabIndex = 148
        Me.LblTravelPurpose.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTravelPurpose.TextWrap = False
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(7, 66)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel4.TabIndex = 146
        Me.MyLabel4.Text = "Travel Purpose"
        '
        'TxtTravelPurpose
        '
        Me.TxtTravelPurpose.Location = New System.Drawing.Point(115, 64)
        Me.TxtTravelPurpose.MendatroryField = True
        Me.TxtTravelPurpose.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTravelPurpose.MyLinkLable1 = Nothing
        Me.TxtTravelPurpose.MyLinkLable2 = Nothing
        Me.TxtTravelPurpose.MyReadOnly = False
        Me.TxtTravelPurpose.MyShowMasterFormButton = False
        Me.TxtTravelPurpose.Name = "TxtTravelPurpose"
        Me.TxtTravelPurpose.Size = New System.Drawing.Size(120, 19)
        Me.TxtTravelPurpose.TabIndex = 2
        Me.TxtTravelPurpose.Value = ""
        '
        'LblTravelBookingFor
        '
        Me.LblTravelBookingFor.AutoSize = False
        Me.LblTravelBookingFor.BorderVisible = True
        Me.LblTravelBookingFor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTravelBookingFor.Location = New System.Drawing.Point(239, 42)
        Me.LblTravelBookingFor.Name = "LblTravelBookingFor"
        Me.LblTravelBookingFor.Size = New System.Drawing.Size(344, 18)
        Me.LblTravelBookingFor.TabIndex = 145
        Me.LblTravelBookingFor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTravelBookingFor.TextWrap = False
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(7, 44)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel3.TabIndex = 143
        Me.MyLabel3.Text = "Travel Booking For"
        '
        'TxtTravelBookingFor
        '
        Me.TxtTravelBookingFor.Location = New System.Drawing.Point(115, 42)
        Me.TxtTravelBookingFor.MendatroryField = True
        Me.TxtTravelBookingFor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTravelBookingFor.MyLinkLable1 = Nothing
        Me.TxtTravelBookingFor.MyLinkLable2 = Nothing
        Me.TxtTravelBookingFor.MyReadOnly = False
        Me.TxtTravelBookingFor.MyShowMasterFormButton = False
        Me.TxtTravelBookingFor.Name = "TxtTravelBookingFor"
        Me.TxtTravelBookingFor.Size = New System.Drawing.Size(120, 19)
        Me.TxtTravelBookingFor.TabIndex = 1
        Me.TxtTravelBookingFor.Value = ""
        '
        'LblTravelBookingBy
        '
        Me.LblTravelBookingBy.AutoSize = False
        Me.LblTravelBookingBy.BorderVisible = True
        Me.LblTravelBookingBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTravelBookingBy.Location = New System.Drawing.Point(239, 20)
        Me.LblTravelBookingBy.Name = "LblTravelBookingBy"
        Me.LblTravelBookingBy.Size = New System.Drawing.Size(344, 18)
        Me.LblTravelBookingBy.TabIndex = 142
        Me.LblTravelBookingBy.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTravelBookingBy.TextWrap = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(7, 22)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(98, 16)
        Me.MyLabel1.TabIndex = 140
        Me.MyLabel1.Text = "Travel Booking By"
        '
        'TxtTravelBookingBy
        '
        Me.TxtTravelBookingBy.Location = New System.Drawing.Point(115, 20)
        Me.TxtTravelBookingBy.MendatroryField = True
        Me.TxtTravelBookingBy.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTravelBookingBy.MyLinkLable1 = Nothing
        Me.TxtTravelBookingBy.MyLinkLable2 = Nothing
        Me.TxtTravelBookingBy.MyReadOnly = False
        Me.TxtTravelBookingBy.MyShowMasterFormButton = False
        Me.TxtTravelBookingBy.Name = "TxtTravelBookingBy"
        Me.TxtTravelBookingBy.Size = New System.Drawing.Size(120, 19)
        Me.TxtTravelBookingBy.TabIndex = 0
        Me.TxtTravelBookingBy.Value = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.gv1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1063, 274)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Travel Requistions"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(3, 16)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.Size = New System.Drawing.Size(1057, 255)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(9, 15)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(987, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'CmbStatus
        '
        Me.CmbStatus.AutoCompleteDisplayMember = Nothing
        Me.CmbStatus.AutoCompleteValueMember = Nothing
        Me.CmbStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbStatus.Location = New System.Drawing.Point(946, 40)
        Me.CmbStatus.MendatroryField = True
        Me.CmbStatus.MyLinkLable1 = Me.MyLabel8
        Me.CmbStatus.MyLinkLable2 = Nothing
        Me.CmbStatus.Name = "CmbStatus"
        Me.CmbStatus.Size = New System.Drawing.Size(107, 20)
        Me.CmbStatus.TabIndex = 6
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(889, 42)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(38, 16)
        Me.MyLabel8.TabIndex = 165
        Me.MyLabel8.Text = "Status"
        '
        'frmHRTravelReqApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1063, 477)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmHRTravelReqApproval"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Travel Requisition Approval"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ChkSearchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpSearchDate.ResumeLayout(False)
        Me.GrpSearchDate.PerformLayout()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTravelType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTravelCat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTravelPurpose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTravelBookingFor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTravelBookingBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents LblTravelBookingBy As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtTravelBookingBy As common.UserControls.txtFinder
    Friend WithEvents LblTravelCat As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents TxtTravelCat As common.UserControls.txtFinder
    Friend WithEvents LblTravelPurpose As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtTravelPurpose As common.UserControls.txtFinder
    Friend WithEvents LblTravelBookingFor As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtTravelBookingFor As common.UserControls.txtFinder
    Friend WithEvents cmbTravelType As common.Controls.MyComboBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPPDate As common.Controls.MyLabel
    Friend WithEvents BtnShow As Telerik.WinControls.UI.RadButton
    Friend WithEvents GrpSearchDate As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents ChkSearchDate As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents CmbStatus As common.Controls.MyComboBox
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
End Class

