Imports XpertERPEngine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRequesitionEntry
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
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.LblVertical = New common.Controls.MyLabel()
        Me.LblIndustry = New common.Controls.MyLabel()
        Me.TxtVertical = New common.UserControls.txtFinder()
        Me.TxtIndustry = New common.UserControls.txtFinder()
        Me.txtReqDateDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.txtinitiated = New common.Controls.MyTextBox()
        Me.lblinitiated = New common.Controls.MyLabel()
        Me.lblnoofpost = New common.Controls.MyLabel()
        Me.ddgender = New common.Controls.MyComboBox()
        Me.lblgender = New common.Controls.MyLabel()
        Me.lblageMonth = New common.Controls.MyLabel()
        Me.lblAgeYear = New common.Controls.MyLabel()
        Me.lblagerange = New common.Controls.MyLabel()
        Me.lblmaxMonth = New common.Controls.MyLabel()
        Me.lblMaxYear = New common.Controls.MyLabel()
        Me.lblmaximumexperience = New common.Controls.MyLabel()
        Me.lblmonth = New common.Controls.MyLabel()
        Me.lblyear = New common.Controls.MyLabel()
        Me.lblminexp = New common.Controls.MyLabel()
        Me.ddctcrange = New common.Controls.MyComboBox()
        Me.lblctcrange = New common.Controls.MyLabel()
        Me.ddhiringtype = New common.Controls.MyComboBox()
        Me.lblhiringtype = New common.Controls.MyLabel()
        Me.lbljobtitle = New common.Controls.MyLabel()
        Me.lblemployeetype = New common.Controls.MyLabel()
        Me.lblSubLocation = New common.Controls.MyLabel()
        Me.lbllocation = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.lblrecommendedby = New common.Controls.MyLabel()
        Me.lblprofile = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.lblCode = New common.Controls.MyLabel()
        Me.txtdescription = New common.Controls.MyTextBox()
        Me.txtjobTitleCode = New common.Controls.MyLabel()
        Me.txtEmployeeName = New common.Controls.MyLabel()
        Me.txtSubLocationName = New common.Controls.MyLabel()
        Me.txtLocationName = New common.Controls.MyLabel()
        Me.txtDepartmentName = New common.Controls.MyLabel()
        Me.txtRecommendedName = New common.Controls.MyLabel()
        Me.txtProfileName = New common.Controls.MyLabel()
        Me.ddagemonth = New common.MyNumBox()
        Me.ddageyr = New common.MyNumBox()
        Me.ddmaxexpmonth = New common.MyNumBox()
        Me.ddmaxexp = New common.MyNumBox()
        Me.ddminexpmonth = New common.MyNumBox()
        Me.ddminexp = New common.MyNumBox()
        Me.txtNoPost = New common.MyNumBox()
        Me.butnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.txtprofile = New common.UserControls.txtFinder()
        Me.Txtrecommendedby = New common.UserControls.txtFinder()
        Me.txtDepartmentCode = New common.UserControls.txtFinder()
        Me.Txtlocation = New common.UserControls.txtFinder()
        Me.txtsublocationCode = New common.UserControls.txtFinder()
        Me.Txtjobtitle = New common.UserControls.txtFinder()
        Me.txtnoOfPost = New common.MyNumBox()
        Me.Txtemployeetype = New common.UserControls.txtFinder()
        Me.cbgQualification = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgQual = New common.MyCheckBoxGrid()
        Me.butnclose = New Telerik.WinControls.UI.RadButton()
        Me.butnsave = New Telerik.WinControls.UI.RadButton()
        Me.butndelete = New Telerik.WinControls.UI.RadButton()
        Me.lbldesignation = New common.Controls.MyLabel()
        Me.Txtdesignation = New common.UserControls.txtFinder()
        Me.txtDesignationName = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblVertical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblIndustry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReqDateDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtinitiated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblinitiated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblnoofpost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddgender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblgender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblageMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAgeYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblagerange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmaxMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMaxYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmaximumexperience, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblmonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblyear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblminexp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddctcrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblctcrange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddhiringtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblhiringtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbljobtitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblemployeetype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblrecommendedby, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblprofile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtjobTitleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSubLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRecommendedName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProfileName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddagemonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddageyr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddmaxexpmonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddmaxexp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddminexpmonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddminexp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.butnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtnoOfPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbgQualification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cbgQualification.SuspendLayout()
        CType(Me.butnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.butnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.butndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDesignationName, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cbgQualification)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.butnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.butnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.butndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbldesignation)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Txtdesignation)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtDesignationName)
        Me.SplitContainer1.Size = New System.Drawing.Size(966, 612)
        Me.SplitContainer1.SplitterDistance = 572
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MyLabel1)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.LblVertical)
        Me.GroupBox1.Controls.Add(Me.LblIndustry)
        Me.GroupBox1.Controls.Add(Me.TxtVertical)
        Me.GroupBox1.Controls.Add(Me.TxtIndustry)
        Me.GroupBox1.Controls.Add(Me.txtReqDateDate)
        Me.GroupBox1.Controls.Add(Me.UsLock1)
        Me.GroupBox1.Controls.Add(Me.txtinitiated)
        Me.GroupBox1.Controls.Add(Me.lblnoofpost)
        Me.GroupBox1.Controls.Add(Me.ddgender)
        Me.GroupBox1.Controls.Add(Me.lblgender)
        Me.GroupBox1.Controls.Add(Me.lblageMonth)
        Me.GroupBox1.Controls.Add(Me.lblAgeYear)
        Me.GroupBox1.Controls.Add(Me.lblagerange)
        Me.GroupBox1.Controls.Add(Me.lblmaxMonth)
        Me.GroupBox1.Controls.Add(Me.lblMaxYear)
        Me.GroupBox1.Controls.Add(Me.lblmaximumexperience)
        Me.GroupBox1.Controls.Add(Me.lblmonth)
        Me.GroupBox1.Controls.Add(Me.lblyear)
        Me.GroupBox1.Controls.Add(Me.lblminexp)
        Me.GroupBox1.Controls.Add(Me.ddctcrange)
        Me.GroupBox1.Controls.Add(Me.lblctcrange)
        Me.GroupBox1.Controls.Add(Me.ddhiringtype)
        Me.GroupBox1.Controls.Add(Me.lblhiringtype)
        Me.GroupBox1.Controls.Add(Me.lblinitiated)
        Me.GroupBox1.Controls.Add(Me.lblDate)
        Me.GroupBox1.Controls.Add(Me.lbljobtitle)
        Me.GroupBox1.Controls.Add(Me.lblemployeetype)
        Me.GroupBox1.Controls.Add(Me.lblSubLocation)
        Me.GroupBox1.Controls.Add(Me.lbllocation)
        Me.GroupBox1.Controls.Add(Me.lblDepartment)
        Me.GroupBox1.Controls.Add(Me.lblrecommendedby)
        Me.GroupBox1.Controls.Add(Me.lblprofile)
        Me.GroupBox1.Controls.Add(Me.lblDescription)
        Me.GroupBox1.Controls.Add(Me.lblCode)
        Me.GroupBox1.Controls.Add(Me.txtdescription)
        Me.GroupBox1.Controls.Add(Me.txtjobTitleCode)
        Me.GroupBox1.Controls.Add(Me.txtEmployeeName)
        Me.GroupBox1.Controls.Add(Me.txtSubLocationName)
        Me.GroupBox1.Controls.Add(Me.txtLocationName)
        Me.GroupBox1.Controls.Add(Me.txtDepartmentName)
        Me.GroupBox1.Controls.Add(Me.txtRecommendedName)
        Me.GroupBox1.Controls.Add(Me.txtProfileName)
        Me.GroupBox1.Controls.Add(Me.ddagemonth)
        Me.GroupBox1.Controls.Add(Me.ddageyr)
        Me.GroupBox1.Controls.Add(Me.ddmaxexpmonth)
        Me.GroupBox1.Controls.Add(Me.ddmaxexp)
        Me.GroupBox1.Controls.Add(Me.ddminexpmonth)
        Me.GroupBox1.Controls.Add(Me.ddminexp)
        Me.GroupBox1.Controls.Add(Me.txtNoPost)
        Me.GroupBox1.Controls.Add(Me.butnreset)
        Me.GroupBox1.Controls.Add(Me.txtcode)
        Me.GroupBox1.Controls.Add(Me.txtprofile)
        Me.GroupBox1.Controls.Add(Me.Txtrecommendedby)
        Me.GroupBox1.Controls.Add(Me.txtDepartmentCode)
        Me.GroupBox1.Controls.Add(Me.Txtlocation)
        Me.GroupBox1.Controls.Add(Me.txtsublocationCode)
        Me.GroupBox1.Controls.Add(Me.Txtjobtitle)
        Me.GroupBox1.Controls.Add(Me.txtnoOfPost)
        Me.GroupBox1.Controls.Add(Me.Txtemployeetype)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(954, 232)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(15, 208)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(44, 16)
        Me.MyLabel1.TabIndex = 417
        Me.MyLabel1.Text = "Vertical"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(15, 187)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(46, 16)
        Me.MyLabel2.TabIndex = 416
        Me.MyLabel2.Text = "Industry"
        '
        'LblVertical
        '
        Me.LblVertical.AutoSize = False
        Me.LblVertical.BorderVisible = True
        Me.LblVertical.Location = New System.Drawing.Point(294, 207)
        Me.LblVertical.Name = "LblVertical"
        Me.LblVertical.Size = New System.Drawing.Size(212, 19)
        Me.LblVertical.TabIndex = 415
        Me.LblVertical.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblIndustry
        '
        Me.LblIndustry.AutoSize = False
        Me.LblIndustry.BorderVisible = True
        Me.LblIndustry.Location = New System.Drawing.Point(294, 186)
        Me.LblIndustry.Name = "LblIndustry"
        Me.LblIndustry.Size = New System.Drawing.Size(212, 19)
        Me.LblIndustry.TabIndex = 414
        Me.LblIndustry.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtVertical
        '
        Me.TxtVertical.Location = New System.Drawing.Point(141, 207)
        Me.TxtVertical.MendatroryField = True
        Me.TxtVertical.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVertical.MyLinkLable1 = Me.MyLabel1
        Me.TxtVertical.MyLinkLable2 = Nothing
        Me.TxtVertical.MyReadOnly = False
        Me.TxtVertical.MyShowMasterFormButton = False
        Me.TxtVertical.Name = "TxtVertical"
        Me.TxtVertical.Size = New System.Drawing.Size(146, 19)
        Me.TxtVertical.TabIndex = 413
        Me.TxtVertical.Value = ""
        '
        'TxtIndustry
        '
        Me.TxtIndustry.Location = New System.Drawing.Point(141, 186)
        Me.TxtIndustry.MendatroryField = True
        Me.TxtIndustry.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIndustry.MyLinkLable1 = Me.MyLabel2
        Me.TxtIndustry.MyLinkLable2 = Nothing
        Me.TxtIndustry.MyReadOnly = False
        Me.TxtIndustry.MyShowMasterFormButton = False
        Me.TxtIndustry.Name = "TxtIndustry"
        Me.TxtIndustry.Size = New System.Drawing.Size(146, 19)
        Me.TxtIndustry.TabIndex = 412
        Me.TxtIndustry.Value = ""
        '
        'txtReqDateDate
        '
        Me.txtReqDateDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtReqDateDate.CustomFormat = "dd/MM/yyyy"
        Me.txtReqDateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtReqDateDate.Location = New System.Drawing.Point(427, 16)
        Me.txtReqDateDate.MendatroryField = False
        Me.txtReqDateDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReqDateDate.MyLinkLable1 = Me.lblDate
        Me.txtReqDateDate.MyLinkLable2 = Nothing
        Me.txtReqDateDate.Name = "txtReqDateDate"
        Me.txtReqDateDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtReqDateDate.Size = New System.Drawing.Size(79, 20)
        Me.txtReqDateDate.TabIndex = 2
        Me.txtReqDateDate.TabStop = False
        Me.txtReqDateDate.Text = "16/11/2011"
        Me.txtReqDateDate.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDate.Location = New System.Drawing.Point(397, 18)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 391
        Me.lblDate.Text = "Date"
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(850, 16)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 2
        '
        'txtinitiated
        '
        Me.txtinitiated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinitiated.Location = New System.Drawing.Point(636, 39)
        Me.txtinitiated.MaxLength = 200
        Me.txtinitiated.MendatroryField = False
        Me.txtinitiated.MyLinkLable1 = Me.lblinitiated
        Me.txtinitiated.MyLinkLable2 = Nothing
        Me.txtinitiated.Name = "txtinitiated"
        Me.txtinitiated.ReadOnly = True
        Me.txtinitiated.Size = New System.Drawing.Size(182, 18)
        Me.txtinitiated.TabIndex = 5
        Me.txtinitiated.TabStop = False
        '
        'lblinitiated
        '
        Me.lblinitiated.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblinitiated.Location = New System.Drawing.Point(519, 40)
        Me.lblinitiated.Name = "lblinitiated"
        Me.lblinitiated.Size = New System.Drawing.Size(62, 16)
        Me.lblinitiated.TabIndex = 392
        Me.lblinitiated.Text = "Initiated By"
        '
        'lblnoofpost
        '
        Me.lblnoofpost.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblnoofpost.Location = New System.Drawing.Point(519, 187)
        Me.lblnoofpost.Name = "lblnoofpost"
        Me.lblnoofpost.Size = New System.Drawing.Size(61, 16)
        Me.lblnoofpost.TabIndex = 407
        Me.lblnoofpost.Text = "No. of post"
        '
        'ddgender
        '
        Me.ddgender.AutoCompleteDisplayMember = Nothing
        Me.ddgender.AutoCompleteValueMember = Nothing
        Me.ddgender.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddgender.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "Male"
        RadListDataItem2.Text = "Female"
        RadListDataItem3.Text = "Both"
        Me.ddgender.Items.Add(RadListDataItem1)
        Me.ddgender.Items.Add(RadListDataItem2)
        Me.ddgender.Items.Add(RadListDataItem3)
        Me.ddgender.Location = New System.Drawing.Point(636, 165)
        Me.ddgender.MendatroryField = True
        Me.ddgender.MyLinkLable1 = Me.lblgender
        Me.ddgender.MyLinkLable2 = Nothing
        Me.ddgender.Name = "ddgender"
        Me.ddgender.Size = New System.Drawing.Size(68, 18)
        Me.ddgender.TabIndex = 19
        '
        'lblgender
        '
        Me.lblgender.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblgender.Location = New System.Drawing.Point(519, 166)
        Me.lblgender.Name = "lblgender"
        Me.lblgender.Size = New System.Drawing.Size(44, 16)
        Me.lblgender.TabIndex = 405
        Me.lblgender.Text = "Gender"
        '
        'lblageMonth
        '
        Me.lblageMonth.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblageMonth.Location = New System.Drawing.Point(820, 144)
        Me.lblageMonth.Name = "lblageMonth"
        Me.lblageMonth.Size = New System.Drawing.Size(19, 16)
        Me.lblageMonth.TabIndex = 404
        Me.lblageMonth.Text = "To"
        '
        'lblAgeYear
        '
        Me.lblAgeYear.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblAgeYear.Location = New System.Drawing.Point(706, 145)
        Me.lblAgeYear.Name = "lblAgeYear"
        Me.lblAgeYear.Size = New System.Drawing.Size(33, 16)
        Me.lblAgeYear.TabIndex = 403
        Me.lblAgeYear.Text = "From"
        '
        'lblagerange
        '
        Me.lblagerange.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblagerange.Location = New System.Drawing.Point(519, 145)
        Me.lblagerange.Name = "lblagerange"
        Me.lblagerange.Size = New System.Drawing.Size(63, 16)
        Me.lblagerange.TabIndex = 402
        Me.lblagerange.Text = "Age Range"
        '
        'lblmaxMonth
        '
        Me.lblmaxMonth.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblmaxMonth.Location = New System.Drawing.Point(820, 123)
        Me.lblmaxMonth.Name = "lblmaxMonth"
        Me.lblmaxMonth.Size = New System.Drawing.Size(38, 16)
        Me.lblmaxMonth.TabIndex = 401
        Me.lblmaxMonth.Text = "Month"
        '
        'lblMaxYear
        '
        Me.lblMaxYear.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblMaxYear.Location = New System.Drawing.Point(706, 124)
        Me.lblMaxYear.Name = "lblMaxYear"
        Me.lblMaxYear.Size = New System.Drawing.Size(30, 16)
        Me.lblMaxYear.TabIndex = 400
        Me.lblMaxYear.Text = "Year"
        '
        'lblmaximumexperience
        '
        Me.lblmaximumexperience.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblmaximumexperience.Location = New System.Drawing.Point(519, 124)
        Me.lblmaximumexperience.Name = "lblmaximumexperience"
        Me.lblmaximumexperience.Size = New System.Drawing.Size(112, 16)
        Me.lblmaximumexperience.TabIndex = 399
        Me.lblmaximumexperience.Text = "Maximun Experience"
        '
        'lblmonth
        '
        Me.lblmonth.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblmonth.Location = New System.Drawing.Point(820, 102)
        Me.lblmonth.Name = "lblmonth"
        Me.lblmonth.Size = New System.Drawing.Size(38, 16)
        Me.lblmonth.TabIndex = 398
        Me.lblmonth.Text = "Month"
        '
        'lblyear
        '
        Me.lblyear.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblyear.Location = New System.Drawing.Point(706, 103)
        Me.lblyear.Name = "lblyear"
        Me.lblyear.Size = New System.Drawing.Size(30, 16)
        Me.lblyear.TabIndex = 397
        Me.lblyear.Text = "Year"
        '
        'lblminexp
        '
        Me.lblminexp.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblminexp.Location = New System.Drawing.Point(519, 103)
        Me.lblminexp.Name = "lblminexp"
        Me.lblminexp.Size = New System.Drawing.Size(112, 16)
        Me.lblminexp.TabIndex = 396
        Me.lblminexp.Text = "Minimum Experience"
        '
        'ddctcrange
        '
        Me.ddctcrange.AutoCompleteDisplayMember = Nothing
        Me.ddctcrange.AutoCompleteValueMember = Nothing
        Me.ddctcrange.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddctcrange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem4.Text = "0-5000"
        RadListDataItem5.Text = "5000-20000"
        RadListDataItem6.Text = "20000-50000"
        RadListDataItem7.Text = "Above 50000"
        Me.ddctcrange.Items.Add(RadListDataItem4)
        Me.ddctcrange.Items.Add(RadListDataItem5)
        Me.ddctcrange.Items.Add(RadListDataItem6)
        Me.ddctcrange.Items.Add(RadListDataItem7)
        Me.ddctcrange.Location = New System.Drawing.Point(636, 81)
        Me.ddctcrange.MendatroryField = True
        Me.ddctcrange.MyLinkLable1 = Me.lblctcrange
        Me.ddctcrange.MyLinkLable2 = Nothing
        Me.ddctcrange.Name = "ddctcrange"
        Me.ddctcrange.Size = New System.Drawing.Size(182, 18)
        Me.ddctcrange.TabIndex = 8
        '
        'lblctcrange
        '
        Me.lblctcrange.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblctcrange.Location = New System.Drawing.Point(519, 82)
        Me.lblctcrange.Name = "lblctcrange"
        Me.lblctcrange.Size = New System.Drawing.Size(66, 16)
        Me.lblctcrange.TabIndex = 394
        Me.lblctcrange.Text = "CTC Range"
        '
        'ddhiringtype
        '
        Me.ddhiringtype.AutoCompleteDisplayMember = Nothing
        Me.ddhiringtype.AutoCompleteValueMember = Nothing
        Me.ddhiringtype.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.ddhiringtype.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem8.Text = "New"
        RadListDataItem9.Text = "Replacement"
        RadListDataItem10.Text = "Rehire"
        RadListDataItem11.Text = "Temporary"
        Me.ddhiringtype.Items.Add(RadListDataItem8)
        Me.ddhiringtype.Items.Add(RadListDataItem9)
        Me.ddhiringtype.Items.Add(RadListDataItem10)
        Me.ddhiringtype.Items.Add(RadListDataItem11)
        Me.ddhiringtype.Location = New System.Drawing.Point(636, 60)
        Me.ddhiringtype.MendatroryField = True
        Me.ddhiringtype.MyLinkLable1 = Me.lblhiringtype
        Me.ddhiringtype.MyLinkLable2 = Nothing
        Me.ddhiringtype.Name = "ddhiringtype"
        Me.ddhiringtype.Size = New System.Drawing.Size(182, 18)
        Me.ddhiringtype.TabIndex = 6
        '
        'lblhiringtype
        '
        Me.lblhiringtype.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblhiringtype.Location = New System.Drawing.Point(519, 61)
        Me.lblhiringtype.Name = "lblhiringtype"
        Me.lblhiringtype.Size = New System.Drawing.Size(64, 16)
        Me.lblhiringtype.TabIndex = 393
        Me.lblhiringtype.Text = "Hiring Type"
        '
        'lbljobtitle
        '
        Me.lbljobtitle.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbljobtitle.Location = New System.Drawing.Point(15, 166)
        Me.lbljobtitle.Name = "lbljobtitle"
        Me.lbljobtitle.Size = New System.Drawing.Size(116, 16)
        Me.lbljobtitle.TabIndex = 389
        Me.lbljobtitle.Text = "Job Title(Designation)"
        '
        'lblemployeetype
        '
        Me.lblemployeetype.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblemployeetype.Location = New System.Drawing.Point(15, 145)
        Me.lblemployeetype.Name = "lblemployeetype"
        Me.lblemployeetype.Size = New System.Drawing.Size(85, 16)
        Me.lblemployeetype.TabIndex = 387
        Me.lblemployeetype.Text = "Employee Type"
        '
        'lblSubLocation
        '
        Me.lblSubLocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblSubLocation.Location = New System.Drawing.Point(800, 187)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(75, 16)
        Me.lblSubLocation.TabIndex = 386
        Me.lblSubLocation.Text = "Sub Location "
        Me.lblSubLocation.Visible = False
        '
        'lbllocation
        '
        Me.lbllocation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbllocation.Location = New System.Drawing.Point(15, 124)
        Me.lbllocation.Name = "lbllocation"
        Me.lbllocation.Size = New System.Drawing.Size(52, 16)
        Me.lbllocation.TabIndex = 385
        Me.lbllocation.Text = "Location "
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDepartment.Location = New System.Drawing.Point(15, 103)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(65, 16)
        Me.lblDepartment.TabIndex = 384
        Me.lblDepartment.Text = "Department"
        '
        'lblrecommendedby
        '
        Me.lblrecommendedby.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblrecommendedby.Location = New System.Drawing.Point(15, 82)
        Me.lblrecommendedby.Name = "lblrecommendedby"
        Me.lblrecommendedby.Size = New System.Drawing.Size(99, 16)
        Me.lblrecommendedby.TabIndex = 383
        Me.lblrecommendedby.Text = "Recommended By"
        '
        'lblprofile
        '
        Me.lblprofile.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblprofile.Location = New System.Drawing.Point(15, 61)
        Me.lblprofile.Name = "lblprofile"
        Me.lblprofile.Size = New System.Drawing.Size(38, 16)
        Me.lblprofile.TabIndex = 382
        Me.lblprofile.Text = "Profile"
        '
        'lblDescription
        '
        Me.lblDescription.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(15, 40)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 16)
        Me.lblDescription.TabIndex = 381
        Me.lblDescription.Text = "Description"
        '
        'lblCode
        '
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(15, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(33, 16)
        Me.lblCode.TabIndex = 380
        Me.lblCode.Text = "Code"
        '
        'txtdescription
        '
        Me.txtdescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescription.Location = New System.Drawing.Point(141, 39)
        Me.txtdescription.MaxLength = 100
        Me.txtdescription.MendatroryField = True
        Me.txtdescription.MyLinkLable1 = Me.lblDescription
        Me.txtdescription.MyLinkLable2 = Nothing
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.Size = New System.Drawing.Size(365, 18)
        Me.txtdescription.TabIndex = 3
        '
        'txtjobTitleCode
        '
        Me.txtjobTitleCode.AutoSize = False
        Me.txtjobTitleCode.BorderVisible = True
        Me.txtjobTitleCode.Location = New System.Drawing.Point(294, 165)
        Me.txtjobTitleCode.Name = "txtjobTitleCode"
        Me.txtjobTitleCode.Size = New System.Drawing.Size(212, 19)
        Me.txtjobTitleCode.TabIndex = 378
        Me.txtjobTitleCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.AutoSize = False
        Me.txtEmployeeName.BorderVisible = True
        Me.txtEmployeeName.Location = New System.Drawing.Point(294, 144)
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.Size = New System.Drawing.Size(212, 19)
        Me.txtEmployeeName.TabIndex = 376
        Me.txtEmployeeName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSubLocationName
        '
        Me.txtSubLocationName.AutoSize = False
        Me.txtSubLocationName.BorderVisible = True
        Me.txtSubLocationName.Location = New System.Drawing.Point(927, 183)
        Me.txtSubLocationName.Name = "txtSubLocationName"
        Me.txtSubLocationName.Size = New System.Drawing.Size(19, 19)
        Me.txtSubLocationName.TabIndex = 375
        Me.txtSubLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSubLocationName.Visible = False
        '
        'txtLocationName
        '
        Me.txtLocationName.AutoSize = False
        Me.txtLocationName.BorderVisible = True
        Me.txtLocationName.Location = New System.Drawing.Point(294, 123)
        Me.txtLocationName.Name = "txtLocationName"
        Me.txtLocationName.Size = New System.Drawing.Size(212, 19)
        Me.txtLocationName.TabIndex = 374
        Me.txtLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDepartmentName
        '
        Me.txtDepartmentName.AutoSize = False
        Me.txtDepartmentName.BorderVisible = True
        Me.txtDepartmentName.Location = New System.Drawing.Point(294, 102)
        Me.txtDepartmentName.Name = "txtDepartmentName"
        Me.txtDepartmentName.Size = New System.Drawing.Size(212, 19)
        Me.txtDepartmentName.TabIndex = 373
        Me.txtDepartmentName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRecommendedName
        '
        Me.txtRecommendedName.AutoSize = False
        Me.txtRecommendedName.BorderVisible = True
        Me.txtRecommendedName.Location = New System.Drawing.Point(294, 81)
        Me.txtRecommendedName.Name = "txtRecommendedName"
        Me.txtRecommendedName.Size = New System.Drawing.Size(212, 19)
        Me.txtRecommendedName.TabIndex = 372
        Me.txtRecommendedName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtProfileName
        '
        Me.txtProfileName.AutoSize = False
        Me.txtProfileName.BorderVisible = True
        Me.txtProfileName.Location = New System.Drawing.Point(294, 60)
        Me.txtProfileName.Name = "txtProfileName"
        Me.txtProfileName.Size = New System.Drawing.Size(212, 19)
        Me.txtProfileName.TabIndex = 371
        Me.txtProfileName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ddagemonth
        '
        Me.ddagemonth.BackColor = System.Drawing.Color.White
        Me.ddagemonth.DecimalPlaces = 2
        Me.ddagemonth.Location = New System.Drawing.Point(750, 143)
        Me.ddagemonth.MaxLength = 2
        Me.ddagemonth.MendatroryField = False
        Me.ddagemonth.MyLinkLable1 = Me.lblagerange
        Me.ddagemonth.MyLinkLable2 = Me.lblageMonth
        Me.ddagemonth.Name = "ddagemonth"
        Me.ddagemonth.Size = New System.Drawing.Size(68, 20)
        Me.ddagemonth.TabIndex = 17
        Me.ddagemonth.Text = "0"
        Me.ddagemonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ddagemonth.Value = 0.0R
        '
        'ddageyr
        '
        Me.ddageyr.BackColor = System.Drawing.Color.White
        Me.ddageyr.DecimalPlaces = 2
        Me.ddageyr.Location = New System.Drawing.Point(636, 143)
        Me.ddageyr.MaxLength = 2
        Me.ddageyr.MendatroryField = False
        Me.ddageyr.MyLinkLable1 = Me.lblagerange
        Me.ddageyr.MyLinkLable2 = Me.lblAgeYear
        Me.ddageyr.Name = "ddageyr"
        Me.ddageyr.Size = New System.Drawing.Size(69, 20)
        Me.ddageyr.TabIndex = 16
        Me.ddageyr.Text = "0"
        Me.ddageyr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ddageyr.Value = 0.0R
        '
        'ddmaxexpmonth
        '
        Me.ddmaxexpmonth.BackColor = System.Drawing.Color.White
        Me.ddmaxexpmonth.DecimalPlaces = 2
        Me.ddmaxexpmonth.Location = New System.Drawing.Point(751, 122)
        Me.ddmaxexpmonth.MaxLength = 2
        Me.ddmaxexpmonth.MendatroryField = False
        Me.ddmaxexpmonth.MyLinkLable1 = Me.lblmaximumexperience
        Me.ddmaxexpmonth.MyLinkLable2 = Me.lblmaxMonth
        Me.ddmaxexpmonth.Name = "ddmaxexpmonth"
        Me.ddmaxexpmonth.Size = New System.Drawing.Size(68, 20)
        Me.ddmaxexpmonth.TabIndex = 14
        Me.ddmaxexpmonth.Text = "0"
        Me.ddmaxexpmonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ddmaxexpmonth.Value = 0.0R
        '
        'ddmaxexp
        '
        Me.ddmaxexp.BackColor = System.Drawing.Color.White
        Me.ddmaxexp.DecimalPlaces = 2
        Me.ddmaxexp.Location = New System.Drawing.Point(636, 122)
        Me.ddmaxexp.MaxLength = 2
        Me.ddmaxexp.MendatroryField = False
        Me.ddmaxexp.MyLinkLable1 = Me.lblmaximumexperience
        Me.ddmaxexp.MyLinkLable2 = Me.lblMaxYear
        Me.ddmaxexp.Name = "ddmaxexp"
        Me.ddmaxexp.Size = New System.Drawing.Size(68, 20)
        Me.ddmaxexp.TabIndex = 13
        Me.ddmaxexp.Text = "0"
        Me.ddmaxexp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ddmaxexp.Value = 0.0R
        '
        'ddminexpmonth
        '
        Me.ddminexpmonth.BackColor = System.Drawing.Color.White
        Me.ddminexpmonth.DecimalPlaces = 2
        Me.ddminexpmonth.Location = New System.Drawing.Point(750, 101)
        Me.ddminexpmonth.MaxLength = 2
        Me.ddminexpmonth.MendatroryField = False
        Me.ddminexpmonth.MyLinkLable1 = Me.lblminexp
        Me.ddminexpmonth.MyLinkLable2 = Me.lblmonth
        Me.ddminexpmonth.Name = "ddminexpmonth"
        Me.ddminexpmonth.Size = New System.Drawing.Size(68, 20)
        Me.ddminexpmonth.TabIndex = 11
        Me.ddminexpmonth.Text = "0"
        Me.ddminexpmonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ddminexpmonth.Value = 0.0R
        '
        'ddminexp
        '
        Me.ddminexp.BackColor = System.Drawing.Color.White
        Me.ddminexp.DecimalPlaces = 2
        Me.ddminexp.Location = New System.Drawing.Point(636, 101)
        Me.ddminexp.MaxLength = 2
        Me.ddminexp.MendatroryField = False
        Me.ddminexp.MyLinkLable1 = Me.lblminexp
        Me.ddminexp.MyLinkLable2 = Me.lblyear
        Me.ddminexp.Name = "ddminexp"
        Me.ddminexp.Size = New System.Drawing.Size(68, 20)
        Me.ddminexp.TabIndex = 10
        Me.ddminexp.Text = "0"
        Me.ddminexp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ddminexp.Value = 0.0R
        '
        'txtNoPost
        '
        Me.txtNoPost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNoPost.DecimalPlaces = 2
        Me.txtNoPost.Location = New System.Drawing.Point(636, 185)
        Me.txtNoPost.MendatroryField = True
        Me.txtNoPost.MyLinkLable1 = Me.lblnoofpost
        Me.txtNoPost.MyLinkLable2 = Nothing
        Me.txtNoPost.Name = "txtNoPost"
        Me.txtNoPost.Size = New System.Drawing.Size(68, 20)
        Me.txtNoPost.TabIndex = 20
        Me.txtNoPost.Text = "0"
        Me.txtNoPost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoPost.Value = 0.0R
        '
        'butnreset
        '
        Me.butnreset.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.butnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.butnreset.Location = New System.Drawing.Point(355, 16)
        Me.butnreset.Name = "butnreset"
        Me.butnreset.Size = New System.Drawing.Size(15, 20)
        Me.butnreset.TabIndex = 1
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(141, 16)
        Me.txtcode.MendatroryField = False
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(213, 20)
        Me.txtcode.TabIndex = 0
        Me.txtcode.Value = ""
        '
        'txtprofile
        '
        Me.txtprofile.Location = New System.Drawing.Point(141, 60)
        Me.txtprofile.MendatroryField = True
        Me.txtprofile.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprofile.MyLinkLable1 = Me.lblprofile
        Me.txtprofile.MyLinkLable2 = Nothing
        Me.txtprofile.MyReadOnly = False
        Me.txtprofile.MyShowMasterFormButton = False
        Me.txtprofile.Name = "txtprofile"
        Me.txtprofile.Size = New System.Drawing.Size(146, 19)
        Me.txtprofile.TabIndex = 4
        Me.txtprofile.Value = ""
        '
        'Txtrecommendedby
        '
        Me.Txtrecommendedby.Location = New System.Drawing.Point(141, 81)
        Me.Txtrecommendedby.MendatroryField = False
        Me.Txtrecommendedby.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtrecommendedby.MyLinkLable1 = Me.lblrecommendedby
        Me.Txtrecommendedby.MyLinkLable2 = Nothing
        Me.Txtrecommendedby.MyReadOnly = False
        Me.Txtrecommendedby.MyShowMasterFormButton = False
        Me.Txtrecommendedby.Name = "Txtrecommendedby"
        Me.Txtrecommendedby.Size = New System.Drawing.Size(146, 19)
        Me.Txtrecommendedby.TabIndex = 7
        Me.Txtrecommendedby.Value = ""
        '
        'txtDepartmentCode
        '
        Me.txtDepartmentCode.Location = New System.Drawing.Point(141, 102)
        Me.txtDepartmentCode.MendatroryField = True
        Me.txtDepartmentCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartmentCode.MyLinkLable1 = Me.lblDepartment
        Me.txtDepartmentCode.MyLinkLable2 = Nothing
        Me.txtDepartmentCode.MyReadOnly = False
        Me.txtDepartmentCode.MyShowMasterFormButton = False
        Me.txtDepartmentCode.Name = "txtDepartmentCode"
        Me.txtDepartmentCode.Size = New System.Drawing.Size(147, 19)
        Me.txtDepartmentCode.TabIndex = 9
        Me.txtDepartmentCode.Value = ""
        '
        'Txtlocation
        '
        Me.Txtlocation.Location = New System.Drawing.Point(141, 123)
        Me.Txtlocation.MendatroryField = True
        Me.Txtlocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtlocation.MyLinkLable1 = Me.lbllocation
        Me.Txtlocation.MyLinkLable2 = Nothing
        Me.Txtlocation.MyReadOnly = False
        Me.Txtlocation.MyShowMasterFormButton = False
        Me.Txtlocation.Name = "Txtlocation"
        Me.Txtlocation.Size = New System.Drawing.Size(147, 19)
        Me.Txtlocation.TabIndex = 12
        Me.Txtlocation.Value = ""
        '
        'txtsublocationCode
        '
        Me.txtsublocationCode.Location = New System.Drawing.Point(881, 183)
        Me.txtsublocationCode.MendatroryField = False
        Me.txtsublocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsublocationCode.MyLinkLable1 = Me.lblSubLocation
        Me.txtsublocationCode.MyLinkLable2 = Nothing
        Me.txtsublocationCode.MyReadOnly = False
        Me.txtsublocationCode.MyShowMasterFormButton = False
        Me.txtsublocationCode.Name = "txtsublocationCode"
        Me.txtsublocationCode.Size = New System.Drawing.Size(40, 19)
        Me.txtsublocationCode.TabIndex = 15
        Me.txtsublocationCode.Value = ""
        Me.txtsublocationCode.Visible = False
        '
        'Txtjobtitle
        '
        Me.Txtjobtitle.Location = New System.Drawing.Point(141, 165)
        Me.Txtjobtitle.MendatroryField = True
        Me.Txtjobtitle.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtjobtitle.MyLinkLable1 = Me.lbljobtitle
        Me.Txtjobtitle.MyLinkLable2 = Nothing
        Me.Txtjobtitle.MyReadOnly = False
        Me.Txtjobtitle.MyShowMasterFormButton = False
        Me.Txtjobtitle.Name = "Txtjobtitle"
        Me.Txtjobtitle.Size = New System.Drawing.Size(146, 19)
        Me.Txtjobtitle.TabIndex = 22
        Me.Txtjobtitle.Value = ""
        '
        'txtnoOfPost
        '
        Me.txtnoOfPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtnoOfPost.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtnoOfPost.DecimalPlaces = 2
        Me.txtnoOfPost.Location = New System.Drawing.Point(897, 424)
        Me.txtnoOfPost.MendatroryField = True
        Me.txtnoOfPost.MyLinkLable1 = Nothing
        Me.txtnoOfPost.MyLinkLable2 = Nothing
        Me.txtnoOfPost.Name = "txtnoOfPost"
        Me.txtnoOfPost.Size = New System.Drawing.Size(144, 20)
        Me.txtnoOfPost.TabIndex = 320
        Me.txtnoOfPost.Text = "0"
        Me.txtnoOfPost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtnoOfPost.Value = 0.0R
        '
        'Txtemployeetype
        '
        Me.Txtemployeetype.Location = New System.Drawing.Point(141, 144)
        Me.Txtemployeetype.MendatroryField = True
        Me.Txtemployeetype.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtemployeetype.MyLinkLable1 = Me.lblemployeetype
        Me.Txtemployeetype.MyLinkLable2 = Nothing
        Me.Txtemployeetype.MyReadOnly = False
        Me.Txtemployeetype.MyShowMasterFormButton = False
        Me.Txtemployeetype.Name = "Txtemployeetype"
        Me.Txtemployeetype.Size = New System.Drawing.Size(146, 19)
        Me.Txtemployeetype.TabIndex = 18
        Me.Txtemployeetype.Value = ""
        '
        'cbgQualification
        '
        Me.cbgQualification.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.cbgQualification.Controls.Add(Me.cbgQual)
        Me.cbgQualification.HeaderText = "Qualification"
        Me.cbgQualification.Location = New System.Drawing.Point(6, 242)
        Me.cbgQualification.Name = "cbgQualification"
        Me.cbgQualification.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.cbgQualification.Size = New System.Drawing.Size(954, 323)
        Me.cbgQualification.TabIndex = 23
        Me.cbgQualification.Text = "Qualification"
        '
        'cbgQual
        '
        Me.cbgQual.CheckedValue = Nothing
        Me.cbgQual.DataSource = Nothing
        Me.cbgQual.DisplayMember = "Name"
        Me.cbgQual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgQual.Location = New System.Drawing.Point(10, 20)
        Me.cbgQual.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgQual.MyShowHeadrText = False
        Me.cbgQual.Name = "cbgQual"
        Me.cbgQual.Size = New System.Drawing.Size(934, 293)
        Me.cbgQual.TabIndex = 0
        Me.cbgQual.TabStop = False
        Me.cbgQual.ValueMember = "Code"
        '
        'butnclose
        '
        Me.butnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.butnclose.Location = New System.Drawing.Point(886, 11)
        Me.butnclose.Name = "butnclose"
        Me.butnclose.Size = New System.Drawing.Size(66, 18)
        Me.butnclose.TabIndex = 2
        Me.butnclose.Text = "Close"
        '
        'butnsave
        '
        Me.butnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.butnsave.Location = New System.Drawing.Point(8, 11)
        Me.butnsave.Name = "butnsave"
        Me.butnsave.Size = New System.Drawing.Size(66, 18)
        Me.butnsave.TabIndex = 0
        Me.butnsave.Text = "Save"
        '
        'butndelete
        '
        Me.butndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.butndelete.Location = New System.Drawing.Point(79, 11)
        Me.butndelete.Name = "butndelete"
        Me.butndelete.Size = New System.Drawing.Size(66, 18)
        Me.butndelete.TabIndex = 1
        Me.butndelete.Text = "Delete"
        '
        'lbldesignation
        '
        Me.lbldesignation.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lbldesignation.Location = New System.Drawing.Point(723, 8)
        Me.lbldesignation.Name = "lbldesignation"
        Me.lbldesignation.Size = New System.Drawing.Size(66, 16)
        Me.lbldesignation.TabIndex = 388
        Me.lbldesignation.Text = "Designation"
        Me.lbldesignation.Visible = False
        '
        'Txtdesignation
        '
        Me.Txtdesignation.Location = New System.Drawing.Point(795, 5)
        Me.Txtdesignation.MendatroryField = True
        Me.Txtdesignation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtdesignation.MyLinkLable1 = Me.lbldesignation
        Me.Txtdesignation.MyLinkLable2 = Nothing
        Me.Txtdesignation.MyReadOnly = False
        Me.Txtdesignation.MyShowMasterFormButton = False
        Me.Txtdesignation.Name = "Txtdesignation"
        Me.Txtdesignation.Size = New System.Drawing.Size(45, 19)
        Me.Txtdesignation.TabIndex = 21
        Me.Txtdesignation.Value = ""
        Me.Txtdesignation.Visible = False
        '
        'txtDesignationName
        '
        Me.txtDesignationName.AutoSize = False
        Me.txtDesignationName.BorderVisible = True
        Me.txtDesignationName.Location = New System.Drawing.Point(846, 5)
        Me.txtDesignationName.Name = "txtDesignationName"
        Me.txtDesignationName.Size = New System.Drawing.Size(24, 19)
        Me.txtDesignationName.TabIndex = 377
        Me.txtDesignationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDesignationName.Visible = False
        '
        'FrmRequesitionEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(966, 612)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRequesitionEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Requisition Entry"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblVertical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblIndustry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReqDateDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtinitiated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblinitiated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblnoofpost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddgender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblgender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblageMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAgeYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblagerange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmaxMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMaxYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmaximumexperience, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblmonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblyear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblminexp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddctcrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblctcrange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddhiringtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblhiringtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbljobtitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblemployeetype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSubLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbllocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblrecommendedby, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblprofile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtdescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtjobTitleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSubLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDepartmentName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRecommendedName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProfileName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddagemonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddageyr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddmaxexpmonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddmaxexp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddminexpmonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddminexp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.butnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtnoOfPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbgQualification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cbgQualification.ResumeLayout(False)
        CType(Me.butnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.butnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.butndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDesignationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pageCus As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents txtprofile As common.UserControls.txtFinder
    Friend WithEvents Txtrecommendedby As common.UserControls.txtFinder
    Friend WithEvents txtDepartmentCode As common.UserControls.txtFinder
    Friend WithEvents Txtlocation As common.UserControls.txtFinder
    Friend WithEvents Txtdesignation As common.UserControls.txtFinder
    Friend WithEvents txtsublocationCode As common.UserControls.txtFinder
    Friend WithEvents Txtjobtitle As common.UserControls.txtFinder
    Friend WithEvents txtnoOfPost As common.MyNumBox
    Friend WithEvents Txtemployeetype As common.UserControls.txtFinder
    Friend WithEvents butnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents butnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents butndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents butnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents cbgQualification As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgQual As common.MyCheckBoxGrid
    Friend WithEvents txtNoPost As common.MyNumBox
    Friend WithEvents ddmaxexp As common.MyNumBox
    Friend WithEvents ddminexpmonth As common.MyNumBox
    Friend WithEvents ddminexp As common.MyNumBox
    Friend WithEvents ddagemonth As common.MyNumBox
    Friend WithEvents ddageyr As common.MyNumBox
    Friend WithEvents ddmaxexpmonth As common.MyNumBox
    Friend WithEvents txtRecommendedName As common.Controls.MyLabel
    Friend WithEvents txtProfileName As common.Controls.MyLabel
    Friend WithEvents txtjobTitleCode As common.Controls.MyLabel
    Friend WithEvents txtDesignationName As common.Controls.MyLabel
    Friend WithEvents txtEmployeeName As common.Controls.MyLabel
    Friend WithEvents txtSubLocationName As common.Controls.MyLabel
    Friend WithEvents txtLocationName As common.Controls.MyLabel
    Friend WithEvents txtDepartmentName As common.Controls.MyLabel
    Friend WithEvents txtdescription As common.Controls.MyTextBox
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblrecommendedby As common.Controls.MyLabel
    Friend WithEvents lblprofile As common.Controls.MyLabel
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents lblSubLocation As common.Controls.MyLabel
    Friend WithEvents lbllocation As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents lblemployeetype As common.Controls.MyLabel
    Friend WithEvents lbldesignation As common.Controls.MyLabel
    Friend WithEvents lbljobtitle As common.Controls.MyLabel
    Friend WithEvents lblinitiated As common.Controls.MyLabel
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblhiringtype As common.Controls.MyLabel
    Friend WithEvents ddhiringtype As common.Controls.MyComboBox
    Friend WithEvents ddctcrange As common.Controls.MyComboBox
    Friend WithEvents lblctcrange As common.Controls.MyLabel
    Friend WithEvents lblminexp As common.Controls.MyLabel
    Friend WithEvents lblmaxMonth As common.Controls.MyLabel
    Friend WithEvents lblMaxYear As common.Controls.MyLabel
    Friend WithEvents lblmaximumexperience As common.Controls.MyLabel
    Friend WithEvents lblmonth As common.Controls.MyLabel
    Friend WithEvents lblyear As common.Controls.MyLabel
    Friend WithEvents lblgender As common.Controls.MyLabel
    Friend WithEvents lblageMonth As common.Controls.MyLabel
    Friend WithEvents lblAgeYear As common.Controls.MyLabel
    Friend WithEvents lblagerange As common.Controls.MyLabel
    Friend WithEvents lblnoofpost As common.Controls.MyLabel
    Friend WithEvents ddgender As common.Controls.MyComboBox
    Friend WithEvents txtinitiated As common.Controls.MyTextBox
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents txtReqDateDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents LblVertical As common.Controls.MyLabel
    Friend WithEvents LblIndustry As common.Controls.MyLabel
    Friend WithEvents TxtVertical As common.UserControls.txtFinder
    Friend WithEvents TxtIndustry As common.UserControls.txtFinder
End Class

