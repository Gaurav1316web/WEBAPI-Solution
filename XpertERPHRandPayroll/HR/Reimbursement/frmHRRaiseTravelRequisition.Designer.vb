Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHRRaiseTravelRequisition
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox6 = New Telerik.WinControls.UI.RadGroupBox()
        Me.LblDesgName = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.LblDeptName = New common.Controls.MyLabel()
        Me.LblCompName = New common.Controls.MyLabel()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.txtcode = New common.UserControls.txtNavigator()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.dtpDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblChillingVendor = New common.Controls.MyLabel()
        Me.lblDesgCode = New common.Controls.MyLabel()
        Me.lblDeptCode = New common.Controls.MyLabel()
        Me.MyLabel43 = New common.Controls.MyLabel()
        Me.lblCompCode = New common.Controls.MyLabel()
        Me.MyLabel44 = New common.Controls.MyLabel()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.LblTravelPur = New common.Controls.MyLabel()
        Me.TxtTravelPur = New common.UserControls.txtFinder()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.LblBookingFor = New common.Controls.MyLabel()
        Me.TxtBookingFor = New common.UserControls.txtFinder()
        Me.LblTCategory = New common.Controls.MyLabel()
        Me.TxtTCategory = New common.UserControls.txtFinder()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.rbtnInternational = New common.Controls.MyRadioButton()
        Me.rbtnDomestic = New common.Controls.MyRadioButton()
        Me.dtpToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.cmbTravelRqst = New common.Controls.MyComboBox()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.dtpFromDate = New common.Controls.MyDateTimePicker()
        Me.lblPPDate = New common.Controls.MyLabel()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.LblBookedByName = New common.Controls.MyLabel()
        Me.TxtBookedByName = New common.UserControls.txtFinder()
        Me.TxtTRemarks = New common.Controls.MyTextBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.CmbTBookedBy = New common.Controls.MyComboBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.TxtTCouponNo = New common.Controls.MyTextBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.TxtTFlightNo = New common.Controls.MyTextBox()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.LblTClass = New common.Controls.MyLabel()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.TxtTClass = New common.UserControls.txtFinder()
        Me.LblTravelMode = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.TxtTravelMode = New common.UserControls.txtFinder()
        Me.LblTNoOfDays = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.dtpArrivalDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.dtpDepartureDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.LblToLoc = New common.Controls.MyLabel()
        Me.MyLabel10 = New common.Controls.MyLabel()
        Me.TxtToLoc = New common.UserControls.txtFinder()
        Me.LblFromLoc = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.TxtFromLoc = New common.UserControls.txtFinder()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.LblNight = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.CmbBookByHotel = New common.Controls.MyComboBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.LblRoomType = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.TxtRoomType = New common.UserControls.txtFinder()
        Me.LblHotelRating = New common.Controls.MyLabel()
        Me.MyLabel37 = New common.Controls.MyLabel()
        Me.TxtHHotelRating = New common.UserControls.txtFinder()
        Me.LblDays = New common.Controls.MyLabel()
        Me.MyLabel39 = New common.Controls.MyLabel()
        Me.dtpStayTo = New common.Controls.MyDateTimePicker()
        Me.MyLabel40 = New common.Controls.MyLabel()
        Me.dtpHStayFrom = New common.Controls.MyDateTimePicker()
        Me.MyLabel42 = New common.Controls.MyLabel()
        Me.RadPageViewPage4 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox9 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TxtAmount = New common.MyNumBox()
        Me.TxtCRemarks = New common.Controls.MyTextBox()
        Me.MyLabel34 = New common.Controls.MyLabel()
        Me.LblLocTo = New common.Controls.MyLabel()
        Me.MyLabel59 = New common.Controls.MyLabel()
        Me.TxtLocTo = New common.UserControls.txtFinder()
        Me.LblTypesOfCar = New common.Controls.MyLabel()
        Me.MyLabel57 = New common.Controls.MyLabel()
        Me.TxtTypesCar = New common.UserControls.txtFinder()
        Me.MyLabel61 = New common.Controls.MyLabel()
        Me.LblLocFrom = New common.Controls.MyLabel()
        Me.MyLabel66 = New common.Controls.MyLabel()
        Me.TxtLocFrom = New common.UserControls.txtFinder()
        Me.dtpPeriodTo = New common.Controls.MyDateTimePicker()
        Me.MyLabel69 = New common.Controls.MyLabel()
        Me.dtpPeriodFrom = New common.Controls.MyDateTimePicker()
        Me.MyLabel70 = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox6.SuspendLayout()
        CType(Me.LblDesgName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblDeptName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCompName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChillingVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesgCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeptCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.LblTravelPur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblBookingFor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnInternational, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnDomestic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTravelRqst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.LblBookedByName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbTBookedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTCouponNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTFlightNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTravelMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTNoOfDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpArrivalDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDepartureDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblToLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblFromLoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.LblNight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbBookByHotel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblRoomType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblHotelRating, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStayTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpHStayFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage4.SuspendLayout()
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox9.SuspendLayout()
        CType(Me.TxtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblTypesOfCar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel61, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblLocFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel66, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPeriodTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel69, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpPeriodFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel70, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(970, 451)
        Me.SplitContainer1.SplitterDistance = 394
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage4)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage4
        Me.RadPageView1.Size = New System.Drawing.Size(970, 394)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "Compressor"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer3)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(126.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(949, 346)
        Me.RadPageViewPage1.Text = "Organizational Details"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadGroupBox6)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer3.Size = New System.Drawing.Size(949, 346)
        Me.SplitContainer3.SplitterDistance = 152
        Me.SplitContainer3.TabIndex = 115
        '
        'RadGroupBox6
        '
        Me.RadGroupBox6.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox6.Controls.Add(Me.LblDesgName)
        Me.RadGroupBox6.Controls.Add(Me.UsLock1)
        Me.RadGroupBox6.Controls.Add(Me.LblDeptName)
        Me.RadGroupBox6.Controls.Add(Me.LblCompName)
        Me.RadGroupBox6.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox6.Controls.Add(Me.txtcode)
        Me.RadGroupBox6.Controls.Add(Me.btnnew)
        Me.RadGroupBox6.Controls.Add(Me.dtpDate)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox6.Controls.Add(Me.lblChillingVendor)
        Me.RadGroupBox6.Controls.Add(Me.lblDesgCode)
        Me.RadGroupBox6.Controls.Add(Me.lblDeptCode)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel43)
        Me.RadGroupBox6.Controls.Add(Me.lblCompCode)
        Me.RadGroupBox6.Controls.Add(Me.MyLabel44)
        Me.RadGroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox6.HeaderText = "Organizational Details"
        Me.RadGroupBox6.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox6.Name = "RadGroupBox6"
        Me.RadGroupBox6.Size = New System.Drawing.Size(949, 152)
        Me.RadGroupBox6.TabIndex = 19
        Me.RadGroupBox6.Text = "Organizational Details"
        '
        'LblDesgName
        '
        Me.LblDesgName.AutoSize = False
        Me.LblDesgName.BorderVisible = True
        Me.LblDesgName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDesgName.Location = New System.Drawing.Point(223, 98)
        Me.LblDesgName.Name = "LblDesgName"
        Me.LblDesgName.Size = New System.Drawing.Size(367, 18)
        Me.LblDesgName.TabIndex = 131
        Me.LblDesgName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblDesgName.TextWrap = False
        '
        'UsLock1
        '
        Me.UsLock1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(832, 31)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 110
        '
        'LblDeptName
        '
        Me.LblDeptName.AutoSize = False
        Me.LblDeptName.BorderVisible = True
        Me.LblDeptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDeptName.Location = New System.Drawing.Point(223, 77)
        Me.LblDeptName.Name = "LblDeptName"
        Me.LblDeptName.Size = New System.Drawing.Size(367, 18)
        Me.LblDeptName.TabIndex = 130
        Me.LblDeptName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblDeptName.TextWrap = False
        '
        'LblCompName
        '
        Me.LblCompName.AutoSize = False
        Me.LblCompName.BorderVisible = True
        Me.LblCompName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCompName.Location = New System.Drawing.Point(223, 56)
        Me.LblCompName.Name = "LblCompName"
        Me.LblCompName.Size = New System.Drawing.Size(367, 18)
        Me.LblCompName.TabIndex = 129
        Me.LblCompName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblCompName.TextWrap = False
        '
        'lblMCCCode
        '
        Me.lblMCCCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMCCCode.Location = New System.Drawing.Point(9, 33)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(34, 16)
        Me.lblMCCCode.TabIndex = 121
        Me.lblMCCCode.Text = "Code"
        '
        'txtcode
        '
        Me.txtcode.Location = New System.Drawing.Point(117, 31)
        Me.txtcode.MendatroryField = True
        Me.txtcode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtcode.MyLinkLable1 = Me.lblMCCCode
        Me.txtcode.MyLinkLable2 = Nothing
        Me.txtcode.MyMaxLength = 30
        Me.txtcode.MyReadOnly = False
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(199, 21)
        Me.txtcode.TabIndex = 1
        Me.txtcode.Value = ""
        '
        'btnnew
        '
        Me.btnnew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.Image = Global.XpertERPHRandPayroll.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(317, 31)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(14, 21)
        Me.btnnew.TabIndex = 0
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(505, 32)
        Me.dtpDate.MendatroryField = True
        Me.dtpDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.MyLinkLable1 = Me.MyLabel12
        Me.dtpDate.MyLinkLable2 = Nothing
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDate.Size = New System.Drawing.Size(85, 18)
        Me.dtpDate.TabIndex = 2
        Me.dtpDate.TabStop = False
        Me.dtpDate.Text = "03/05/2011 "
        Me.dtpDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(462, 33)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel12.TabIndex = 128
        Me.MyLabel12.Text = "Date"
        '
        'lblChillingVendor
        '
        Me.lblChillingVendor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChillingVendor.Location = New System.Drawing.Point(9, 78)
        Me.lblChillingVendor.Name = "lblChillingVendor"
        Me.lblChillingVendor.Size = New System.Drawing.Size(99, 16)
        Me.lblChillingVendor.TabIndex = 122
        Me.lblChillingVendor.Text = "Department Name"
        '
        'lblDesgCode
        '
        Me.lblDesgCode.AutoSize = False
        Me.lblDesgCode.BorderVisible = True
        Me.lblDesgCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesgCode.Location = New System.Drawing.Point(117, 98)
        Me.lblDesgCode.Name = "lblDesgCode"
        Me.lblDesgCode.Size = New System.Drawing.Size(100, 18)
        Me.lblDesgCode.TabIndex = 5
        Me.lblDesgCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDesgCode.TextWrap = False
        '
        'lblDeptCode
        '
        Me.lblDeptCode.AutoSize = False
        Me.lblDeptCode.BorderVisible = True
        Me.lblDeptCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptCode.Location = New System.Drawing.Point(117, 77)
        Me.lblDeptCode.Name = "lblDeptCode"
        Me.lblDeptCode.Size = New System.Drawing.Size(100, 18)
        Me.lblDeptCode.TabIndex = 4
        Me.lblDeptCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDeptCode.TextWrap = False
        '
        'MyLabel43
        '
        Me.MyLabel43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel43.Location = New System.Drawing.Point(9, 99)
        Me.MyLabel43.Name = "MyLabel43"
        Me.MyLabel43.Size = New System.Drawing.Size(99, 16)
        Me.MyLabel43.TabIndex = 123
        Me.MyLabel43.Text = "Designation Name"
        '
        'lblCompCode
        '
        Me.lblCompCode.AutoSize = False
        Me.lblCompCode.BorderVisible = True
        Me.lblCompCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompCode.Location = New System.Drawing.Point(117, 56)
        Me.lblCompCode.Name = "lblCompCode"
        Me.lblCompCode.Size = New System.Drawing.Size(100, 18)
        Me.lblCompCode.TabIndex = 3
        Me.lblCompCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCompCode.TextWrap = False
        '
        'MyLabel44
        '
        Me.MyLabel44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel44.Location = New System.Drawing.Point(9, 57)
        Me.MyLabel44.Name = "MyLabel44"
        Me.MyLabel44.Size = New System.Drawing.Size(88, 16)
        Me.MyLabel44.TabIndex = 124
        Me.MyLabel44.Text = "Company Name"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.LblTravelPur)
        Me.RadGroupBox2.Controls.Add(Me.TxtTravelPur)
        Me.RadGroupBox2.Controls.Add(Me.LblBookingFor)
        Me.RadGroupBox2.Controls.Add(Me.TxtBookingFor)
        Me.RadGroupBox2.Controls.Add(Me.LblTCategory)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox2.Controls.Add(Me.TxtTCategory)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox2.Controls.Add(Me.rbtnInternational)
        Me.RadGroupBox2.Controls.Add(Me.rbtnDomestic)
        Me.RadGroupBox2.Controls.Add(Me.dtpToDate)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel13)
        Me.RadGroupBox2.Controls.Add(Me.cmbTravelRqst)
        Me.RadGroupBox2.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox2.Controls.Add(Me.dtpFromDate)
        Me.RadGroupBox2.Controls.Add(Me.lblPPDate)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Travel Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(949, 190)
        Me.RadGroupBox2.TabIndex = 7
        Me.RadGroupBox2.Text = "Travel Details"
        '
        'LblTravelPur
        '
        Me.LblTravelPur.AutoSize = False
        Me.LblTravelPur.BorderVisible = True
        Me.LblTravelPur.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTravelPur.Location = New System.Drawing.Point(241, 47)
        Me.LblTravelPur.Name = "LblTravelPur"
        Me.LblTravelPur.Size = New System.Drawing.Size(344, 18)
        Me.LblTravelPur.TabIndex = 139
        Me.LblTravelPur.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTravelPur.TextWrap = False
        '
        'TxtTravelPur
        '
        Me.TxtTravelPur.Location = New System.Drawing.Point(117, 47)
        Me.TxtTravelPur.MendatroryField = True
        Me.TxtTravelPur.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTravelPur.MyLinkLable1 = Me.MyLabel6
        Me.TxtTravelPur.MyLinkLable2 = Nothing
        Me.TxtTravelPur.MyReadOnly = False
        Me.TxtTravelPur.MyShowMasterFormButton = False
        Me.TxtTravelPur.Name = "TxtTravelPur"
        Me.TxtTravelPur.Size = New System.Drawing.Size(120, 19)
        Me.TxtTravelPur.TabIndex = 138
        Me.TxtTravelPur.Value = ""
        '
        'MyLabel6
        '
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(9, 138)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(87, 16)
        Me.MyLabel6.TabIndex = 134
        Me.MyLabel6.Text = "Travel Category"
        '
        'LblBookingFor
        '
        Me.LblBookingFor.AutoSize = False
        Me.LblBookingFor.BorderVisible = True
        Me.LblBookingFor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookingFor.Location = New System.Drawing.Point(241, 115)
        Me.LblBookingFor.Name = "LblBookingFor"
        Me.LblBookingFor.Size = New System.Drawing.Size(345, 18)
        Me.LblBookingFor.TabIndex = 137
        Me.LblBookingFor.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblBookingFor.TextWrap = False
        '
        'TxtBookingFor
        '
        Me.TxtBookingFor.Location = New System.Drawing.Point(117, 115)
        Me.TxtBookingFor.MendatroryField = True
        Me.TxtBookingFor.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBookingFor.MyLinkLable1 = Me.MyLabel6
        Me.TxtBookingFor.MyLinkLable2 = Nothing
        Me.TxtBookingFor.MyReadOnly = False
        Me.TxtBookingFor.MyShowMasterFormButton = False
        Me.TxtBookingFor.Name = "TxtBookingFor"
        Me.TxtBookingFor.Size = New System.Drawing.Size(120, 19)
        Me.TxtBookingFor.TabIndex = 136
        Me.TxtBookingFor.Value = ""
        '
        'LblTCategory
        '
        Me.LblTCategory.AutoSize = False
        Me.LblTCategory.BorderVisible = True
        Me.LblTCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTCategory.Location = New System.Drawing.Point(241, 137)
        Me.LblTCategory.Name = "LblTCategory"
        Me.LblTCategory.Size = New System.Drawing.Size(345, 18)
        Me.LblTCategory.TabIndex = 135
        Me.LblTCategory.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTCategory.TextWrap = False
        '
        'TxtTCategory
        '
        Me.TxtTCategory.Location = New System.Drawing.Point(117, 137)
        Me.TxtTCategory.MendatroryField = True
        Me.TxtTCategory.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTCategory.MyLinkLable1 = Me.MyLabel6
        Me.TxtTCategory.MyLinkLable2 = Nothing
        Me.TxtTCategory.MyReadOnly = False
        Me.TxtTCategory.MyShowMasterFormButton = False
        Me.TxtTCategory.Name = "TxtTCategory"
        Me.TxtTCategory.Size = New System.Drawing.Size(120, 19)
        Me.TxtTCategory.TabIndex = 6
        Me.TxtTCategory.Value = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(8, 116)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(102, 16)
        Me.MyLabel4.TabIndex = 128
        Me.MyLabel4.Text = "Travel Booking For"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(8, 27)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel2.TabIndex = 120
        Me.MyLabel2.Text = "Travel Type"
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(8, 49)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel1.TabIndex = 119
        Me.MyLabel1.Text = "Travel Purpose"
        '
        'rbtnInternational
        '
        Me.rbtnInternational.Location = New System.Drawing.Point(230, 25)
        Me.rbtnInternational.MyLinkLable1 = Nothing
        Me.rbtnInternational.MyLinkLable2 = Nothing
        Me.rbtnInternational.Name = "rbtnInternational"
        Me.rbtnInternational.Size = New System.Drawing.Size(84, 18)
        Me.rbtnInternational.TabIndex = 1
        Me.rbtnInternational.Text = "International"
        '
        'rbtnDomestic
        '
        Me.rbtnDomestic.Location = New System.Drawing.Point(117, 25)
        Me.rbtnDomestic.MyLinkLable1 = Nothing
        Me.rbtnDomestic.MyLinkLable2 = Nothing
        Me.rbtnDomestic.Name = "rbtnDomestic"
        Me.rbtnDomestic.Size = New System.Drawing.Size(67, 18)
        Me.rbtnDomestic.TabIndex = 0
        Me.rbtnDomestic.Text = "Domestic"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpToDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(477, 94)
        Me.dtpToDate.MendatroryField = True
        Me.dtpToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.MyLinkLable1 = Me.MyLabel13
        Me.dtpToDate.MyLinkLable2 = Nothing
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToDate.Size = New System.Drawing.Size(108, 18)
        Me.dtpToDate.TabIndex = 14
        Me.dtpToDate.TabStop = False
        Me.dtpToDate.Text = "03/05/2011 "
        Me.dtpToDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(378, 95)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(90, 16)
        Me.MyLabel13.TabIndex = 115
        Me.MyLabel13.Text = "Travel Period To"
        '
        'cmbTravelRqst
        '
        Me.cmbTravelRqst.AutoCompleteDisplayMember = Nothing
        Me.cmbTravelRqst.AutoCompleteValueMember = Nothing
        Me.cmbTravelRqst.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbTravelRqst.Location = New System.Drawing.Point(117, 70)
        Me.cmbTravelRqst.MendatroryField = True
        Me.cmbTravelRqst.MyLinkLable1 = Me.MyLabel5
        Me.cmbTravelRqst.MyLinkLable2 = Nothing
        Me.cmbTravelRqst.Name = "cmbTravelRqst"
        Me.cmbTravelRqst.Size = New System.Drawing.Size(107, 20)
        Me.cmbTravelRqst.TabIndex = 3
        '
        'MyLabel5
        '
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(8, 72)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(83, 16)
        Me.MyLabel5.TabIndex = 60
        Me.MyLabel5.Text = "Travel Request"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpFromDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(117, 94)
        Me.dtpFromDate.MendatroryField = True
        Me.dtpFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.MyLinkLable1 = Me.lblPPDate
        Me.dtpFromDate.MyLinkLable2 = Nothing
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromDate.Size = New System.Drawing.Size(107, 18)
        Me.dtpFromDate.TabIndex = 4
        Me.dtpFromDate.TabStop = False
        Me.dtpFromDate.Text = "03/05/2011 "
        Me.dtpFromDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblPPDate
        '
        Me.lblPPDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblPPDate.Location = New System.Drawing.Point(8, 95)
        Me.lblPPDate.Name = "lblPPDate"
        Me.lblPPDate.Size = New System.Drawing.Size(103, 16)
        Me.lblPPDate.TabIndex = 56
        Me.lblPPDate.Text = "Travel Period From"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(114.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(949, 346)
        Me.RadPageViewPage3.Text = "Travel/Hotel Details"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox7)
        Me.SplitContainer2.Size = New System.Drawing.Size(949, 346)
        Me.SplitContainer2.SplitterDistance = 180
        Me.SplitContainer2.TabIndex = 131
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.LblBookedByName)
        Me.RadGroupBox1.Controls.Add(Me.TxtBookedByName)
        Me.RadGroupBox1.Controls.Add(Me.TxtTRemarks)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel21)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel20)
        Me.RadGroupBox1.Controls.Add(Me.CmbTBookedBy)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel19)
        Me.RadGroupBox1.Controls.Add(Me.TxtTCouponNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel17)
        Me.RadGroupBox1.Controls.Add(Me.TxtTFlightNo)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel11)
        Me.RadGroupBox1.Controls.Add(Me.LblTClass)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel16)
        Me.RadGroupBox1.Controls.Add(Me.TxtTClass)
        Me.RadGroupBox1.Controls.Add(Me.LblTravelMode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel15)
        Me.RadGroupBox1.Controls.Add(Me.TxtTravelMode)
        Me.RadGroupBox1.Controls.Add(Me.LblTNoOfDays)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel14)
        Me.RadGroupBox1.Controls.Add(Me.dtpArrivalDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox1.Controls.Add(Me.dtpDepartureDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.LblToLoc)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel10)
        Me.RadGroupBox1.Controls.Add(Me.TxtToLoc)
        Me.RadGroupBox1.Controls.Add(Me.LblFromLoc)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel8)
        Me.RadGroupBox1.Controls.Add(Me.TxtFromLoc)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Ticket/Travelling Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(949, 180)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Ticket/Travelling Details"
        '
        'LblBookedByName
        '
        Me.LblBookedByName.AutoSize = False
        Me.LblBookedByName.BorderVisible = True
        Me.LblBookedByName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookedByName.Location = New System.Drawing.Point(730, 70)
        Me.LblBookedByName.Name = "LblBookedByName"
        Me.LblBookedByName.Size = New System.Drawing.Size(212, 18)
        Me.LblBookedByName.TabIndex = 165
        Me.LblBookedByName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblBookedByName.TextWrap = False
        '
        'TxtBookedByName
        '
        Me.TxtBookedByName.Location = New System.Drawing.Point(606, 70)
        Me.TxtBookedByName.MendatroryField = True
        Me.TxtBookedByName.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBookedByName.MyLinkLable1 = Me.MyLabel6
        Me.TxtBookedByName.MyLinkLable2 = Nothing
        Me.TxtBookedByName.MyReadOnly = False
        Me.TxtBookedByName.MyShowMasterFormButton = False
        Me.TxtBookedByName.Name = "TxtBookedByName"
        Me.TxtBookedByName.Size = New System.Drawing.Size(120, 19)
        Me.TxtBookedByName.TabIndex = 164
        Me.TxtBookedByName.Value = ""
        '
        'TxtTRemarks
        '
        Me.TxtTRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtTRemarks.Location = New System.Drawing.Point(605, 94)
        Me.TxtTRemarks.MaxLength = 150
        Me.TxtTRemarks.MendatroryField = True
        Me.TxtTRemarks.MyLinkLable1 = Me.MyLabel21
        Me.TxtTRemarks.MyLinkLable2 = Nothing
        Me.TxtTRemarks.Name = "TxtTRemarks"
        Me.TxtTRemarks.Size = New System.Drawing.Size(338, 20)
        Me.TxtTRemarks.TabIndex = 11
        '
        'MyLabel21
        '
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(508, 96)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel21.TabIndex = 163
        Me.MyLabel21.Text = "Remarks"
        '
        'MyLabel20
        '
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(508, 72)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(97, 16)
        Me.MyLabel20.TabIndex = 161
        Me.MyLabel20.Text = "Booked By Name "
        '
        'CmbTBookedBy
        '
        Me.CmbTBookedBy.AutoCompleteDisplayMember = Nothing
        Me.CmbTBookedBy.AutoCompleteValueMember = Nothing
        Me.CmbTBookedBy.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbTBookedBy.Location = New System.Drawing.Point(605, 45)
        Me.CmbTBookedBy.MendatroryField = True
        Me.CmbTBookedBy.MyLinkLable1 = Me.MyLabel19
        Me.CmbTBookedBy.MyLinkLable2 = Nothing
        Me.CmbTBookedBy.Name = "CmbTBookedBy"
        Me.CmbTBookedBy.Size = New System.Drawing.Size(120, 20)
        Me.CmbTBookedBy.TabIndex = 9
        '
        'MyLabel19
        '
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(508, 47)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel19.TabIndex = 159
        Me.MyLabel19.Text = "Booked By "
        '
        'TxtTCouponNo
        '
        Me.TxtTCouponNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtTCouponNo.Location = New System.Drawing.Point(822, 21)
        Me.TxtTCouponNo.MaxLength = 150
        Me.TxtTCouponNo.MendatroryField = True
        Me.TxtTCouponNo.MyLinkLable1 = Me.MyLabel17
        Me.TxtTCouponNo.MyLinkLable2 = Nothing
        Me.TxtTCouponNo.Name = "TxtTCouponNo"
        Me.TxtTCouponNo.Size = New System.Drawing.Size(120, 20)
        Me.TxtTCouponNo.TabIndex = 8
        '
        'MyLabel17
        '
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(750, 23)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel17.TabIndex = 157
        Me.MyLabel17.Text = "Coupon No."
        '
        'TxtTFlightNo
        '
        Me.TxtTFlightNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtTFlightNo.Location = New System.Drawing.Point(605, 21)
        Me.TxtTFlightNo.MaxLength = 150
        Me.TxtTFlightNo.MendatroryField = True
        Me.TxtTFlightNo.MyLinkLable1 = Me.MyLabel11
        Me.TxtTFlightNo.MyLinkLable2 = Nothing
        Me.TxtTFlightNo.Name = "TxtTFlightNo"
        Me.TxtTFlightNo.Size = New System.Drawing.Size(120, 20)
        Me.TxtTFlightNo.TabIndex = 7
        '
        'MyLabel11
        '
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(508, 23)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel11.TabIndex = 155
        Me.MyLabel11.Text = "Flight/Train No."
        '
        'LblTClass
        '
        Me.LblTClass.AutoSize = False
        Me.LblTClass.BorderVisible = True
        Me.LblTClass.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTClass.Location = New System.Drawing.Point(247, 135)
        Me.LblTClass.Name = "LblTClass"
        Me.LblTClass.Size = New System.Drawing.Size(235, 18)
        Me.LblTClass.TabIndex = 153
        Me.LblTClass.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTClass.TextWrap = False
        '
        'MyLabel16
        '
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(24, 136)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel16.TabIndex = 152
        Me.MyLabel16.Text = "Travel Class"
        '
        'TxtTClass
        '
        Me.TxtTClass.Location = New System.Drawing.Point(119, 134)
        Me.TxtTClass.MendatroryField = True
        Me.TxtTClass.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTClass.MyLinkLable1 = Me.MyLabel16
        Me.TxtTClass.MyLinkLable2 = Nothing
        Me.TxtTClass.MyReadOnly = False
        Me.TxtTClass.MyShowMasterFormButton = False
        Me.TxtTClass.Name = "TxtTClass"
        Me.TxtTClass.Size = New System.Drawing.Size(120, 19)
        Me.TxtTClass.TabIndex = 6
        Me.TxtTClass.Value = ""
        '
        'LblTravelMode
        '
        Me.LblTravelMode.AutoSize = False
        Me.LblTravelMode.BorderVisible = True
        Me.LblTravelMode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTravelMode.Location = New System.Drawing.Point(247, 112)
        Me.LblTravelMode.Name = "LblTravelMode"
        Me.LblTravelMode.Size = New System.Drawing.Size(235, 18)
        Me.LblTravelMode.TabIndex = 150
        Me.LblTravelMode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTravelMode.TextWrap = False
        '
        'MyLabel15
        '
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(24, 113)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel15.TabIndex = 149
        Me.MyLabel15.Text = "Travel Mode"
        '
        'TxtTravelMode
        '
        Me.TxtTravelMode.Location = New System.Drawing.Point(119, 111)
        Me.TxtTravelMode.MendatroryField = True
        Me.TxtTravelMode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTravelMode.MyLinkLable1 = Me.MyLabel15
        Me.TxtTravelMode.MyLinkLable2 = Nothing
        Me.TxtTravelMode.MyReadOnly = False
        Me.TxtTravelMode.MyShowMasterFormButton = False
        Me.TxtTravelMode.Name = "TxtTravelMode"
        Me.TxtTravelMode.Size = New System.Drawing.Size(120, 19)
        Me.TxtTravelMode.TabIndex = 5
        Me.TxtTravelMode.Value = ""
        '
        'LblTNoOfDays
        '
        Me.LblTNoOfDays.AutoSize = False
        Me.LblTNoOfDays.BorderVisible = True
        Me.LblTNoOfDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTNoOfDays.Location = New System.Drawing.Point(119, 90)
        Me.LblTNoOfDays.Name = "LblTNoOfDays"
        Me.LblTNoOfDays.Size = New System.Drawing.Size(120, 18)
        Me.LblTNoOfDays.TabIndex = 4
        Me.LblTNoOfDays.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTNoOfDays.TextWrap = False
        '
        'MyLabel14
        '
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(24, 91)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(62, 16)
        Me.MyLabel14.TabIndex = 146
        Me.MyLabel14.Text = "No of Days"
        '
        'dtpArrivalDate
        '
        Me.dtpArrivalDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpArrivalDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpArrivalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpArrivalDate.Location = New System.Drawing.Point(375, 70)
        Me.dtpArrivalDate.MendatroryField = True
        Me.dtpArrivalDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpArrivalDate.MyLinkLable1 = Me.MyLabel7
        Me.dtpArrivalDate.MyLinkLable2 = Nothing
        Me.dtpArrivalDate.Name = "dtpArrivalDate"
        Me.dtpArrivalDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpArrivalDate.Size = New System.Drawing.Size(108, 18)
        Me.dtpArrivalDate.TabIndex = 3
        Me.dtpArrivalDate.TabStop = False
        Me.dtpArrivalDate.Text = "03/05/2011 "
        Me.dtpArrivalDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel7
        '
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(299, 71)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel7.TabIndex = 145
        Me.MyLabel7.Text = "Arrival Date"
        '
        'dtpDepartureDate
        '
        Me.dtpDepartureDate.CustomFormat = "dd/MM/yyyy "
        Me.dtpDepartureDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDepartureDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDepartureDate.Location = New System.Drawing.Point(119, 69)
        Me.dtpDepartureDate.MendatroryField = True
        Me.dtpDepartureDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDepartureDate.MyLinkLable1 = Me.MyLabel9
        Me.dtpDepartureDate.MyLinkLable2 = Nothing
        Me.dtpDepartureDate.Name = "dtpDepartureDate"
        Me.dtpDepartureDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDepartureDate.Size = New System.Drawing.Size(120, 18)
        Me.dtpDepartureDate.TabIndex = 2
        Me.dtpDepartureDate.TabStop = False
        Me.dtpDepartureDate.Text = "03/05/2011 "
        Me.dtpDepartureDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel9
        '
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(24, 70)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel9.TabIndex = 144
        Me.MyLabel9.Text = "Departure Date"
        '
        'LblToLoc
        '
        Me.LblToLoc.AutoSize = False
        Me.LblToLoc.BorderVisible = True
        Me.LblToLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToLoc.Location = New System.Drawing.Point(243, 46)
        Me.LblToLoc.Name = "LblToLoc"
        Me.LblToLoc.Size = New System.Drawing.Size(240, 18)
        Me.LblToLoc.TabIndex = 3
        Me.LblToLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblToLoc.TextWrap = False
        '
        'MyLabel10
        '
        Me.MyLabel10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel10.Location = New System.Drawing.Point(24, 47)
        Me.MyLabel10.Name = "MyLabel10"
        Me.MyLabel10.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel10.TabIndex = 140
        Me.MyLabel10.Text = "To Location"
        '
        'TxtToLoc
        '
        Me.TxtToLoc.Location = New System.Drawing.Point(119, 46)
        Me.TxtToLoc.MendatroryField = True
        Me.TxtToLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToLoc.MyLinkLable1 = Me.MyLabel10
        Me.TxtToLoc.MyLinkLable2 = Nothing
        Me.TxtToLoc.MyReadOnly = False
        Me.TxtToLoc.MyShowMasterFormButton = False
        Me.TxtToLoc.Name = "TxtToLoc"
        Me.TxtToLoc.Size = New System.Drawing.Size(120, 19)
        Me.TxtToLoc.TabIndex = 1
        Me.TxtToLoc.Value = ""
        '
        'LblFromLoc
        '
        Me.LblFromLoc.AutoSize = False
        Me.LblFromLoc.BorderVisible = True
        Me.LblFromLoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromLoc.Location = New System.Drawing.Point(243, 23)
        Me.LblFromLoc.Name = "LblFromLoc"
        Me.LblFromLoc.Size = New System.Drawing.Size(240, 18)
        Me.LblFromLoc.TabIndex = 1
        Me.LblFromLoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblFromLoc.TextWrap = False
        '
        'MyLabel8
        '
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(24, 24)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel8.TabIndex = 137
        Me.MyLabel8.Text = "From Location"
        '
        'TxtFromLoc
        '
        Me.TxtFromLoc.Location = New System.Drawing.Point(119, 23)
        Me.TxtFromLoc.MendatroryField = True
        Me.TxtFromLoc.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromLoc.MyLinkLable1 = Me.MyLabel8
        Me.TxtFromLoc.MyLinkLable2 = Nothing
        Me.TxtFromLoc.MyReadOnly = False
        Me.TxtFromLoc.MyShowMasterFormButton = False
        Me.TxtFromLoc.Name = "TxtFromLoc"
        Me.TxtFromLoc.Size = New System.Drawing.Size(120, 19)
        Me.TxtFromLoc.TabIndex = 0
        Me.TxtFromLoc.Value = ""
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.LblNight)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel23)
        Me.RadGroupBox7.Controls.Add(Me.CmbBookByHotel)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox7.Controls.Add(Me.LblRoomType)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel18)
        Me.RadGroupBox7.Controls.Add(Me.TxtRoomType)
        Me.RadGroupBox7.Controls.Add(Me.LblHotelRating)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel37)
        Me.RadGroupBox7.Controls.Add(Me.TxtHHotelRating)
        Me.RadGroupBox7.Controls.Add(Me.LblDays)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel39)
        Me.RadGroupBox7.Controls.Add(Me.dtpStayTo)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel40)
        Me.RadGroupBox7.Controls.Add(Me.dtpHStayFrom)
        Me.RadGroupBox7.Controls.Add(Me.MyLabel42)
        Me.RadGroupBox7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox7.HeaderText = "Hotel Details"
        Me.RadGroupBox7.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Size = New System.Drawing.Size(949, 162)
        Me.RadGroupBox7.TabIndex = 0
        Me.RadGroupBox7.Text = "Hotel Details"
        '
        'LblNight
        '
        Me.LblNight.AutoSize = False
        Me.LblNight.BorderVisible = True
        Me.LblNight.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNight.Location = New System.Drawing.Point(214, 54)
        Me.LblNight.Name = "LblNight"
        Me.LblNight.Size = New System.Drawing.Size(23, 18)
        Me.LblNight.TabIndex = 3
        Me.LblNight.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblNight.TextWrap = False
        '
        'MyLabel23
        '
        Me.MyLabel23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel23.Location = New System.Drawing.Point(175, 54)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel23.TabIndex = 163
        Me.MyLabel23.Text = "Night"
        '
        'CmbBookByHotel
        '
        Me.CmbBookByHotel.AutoCompleteDisplayMember = Nothing
        Me.CmbBookByHotel.AutoCompleteValueMember = Nothing
        Me.CmbBookByHotel.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CmbBookByHotel.Location = New System.Drawing.Point(117, 76)
        Me.CmbBookByHotel.MendatroryField = True
        Me.CmbBookByHotel.MyLinkLable1 = Me.MyLabel3
        Me.CmbBookByHotel.MyLinkLable2 = Nothing
        Me.CmbBookByHotel.Name = "CmbBookByHotel"
        Me.CmbBookByHotel.Size = New System.Drawing.Size(120, 20)
        Me.CmbBookByHotel.TabIndex = 4
        '
        'MyLabel3
        '
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(15, 78)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(64, 16)
        Me.MyLabel3.TabIndex = 161
        Me.MyLabel3.Text = "Booked By "
        '
        'LblRoomType
        '
        Me.LblRoomType.AutoSize = False
        Me.LblRoomType.BorderVisible = True
        Me.LblRoomType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRoomType.Location = New System.Drawing.Point(708, 32)
        Me.LblRoomType.Name = "LblRoomType"
        Me.LblRoomType.Size = New System.Drawing.Size(235, 18)
        Me.LblRoomType.TabIndex = 156
        Me.LblRoomType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblRoomType.TextWrap = False
        '
        'MyLabel18
        '
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(508, 33)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel18.TabIndex = 155
        Me.MyLabel18.Text = "Room Type"
        '
        'TxtRoomType
        '
        Me.TxtRoomType.Location = New System.Drawing.Point(605, 32)
        Me.TxtRoomType.MendatroryField = True
        Me.TxtRoomType.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoomType.MyLinkLable1 = Me.MyLabel18
        Me.TxtRoomType.MyLinkLable2 = Nothing
        Me.TxtRoomType.MyReadOnly = False
        Me.TxtRoomType.MyShowMasterFormButton = False
        Me.TxtRoomType.Name = "TxtRoomType"
        Me.TxtRoomType.Size = New System.Drawing.Size(97, 19)
        Me.TxtRoomType.TabIndex = 6
        Me.TxtRoomType.Value = ""
        '
        'LblHotelRating
        '
        Me.LblHotelRating.AutoSize = False
        Me.LblHotelRating.BorderVisible = True
        Me.LblHotelRating.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblHotelRating.Location = New System.Drawing.Point(245, 103)
        Me.LblHotelRating.Name = "LblHotelRating"
        Me.LblHotelRating.Size = New System.Drawing.Size(235, 18)
        Me.LblHotelRating.TabIndex = 150
        Me.LblHotelRating.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblHotelRating.TextWrap = False
        '
        'MyLabel37
        '
        Me.MyLabel37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel37.Location = New System.Drawing.Point(15, 104)
        Me.MyLabel37.Name = "MyLabel37"
        Me.MyLabel37.Size = New System.Drawing.Size(69, 16)
        Me.MyLabel37.TabIndex = 149
        Me.MyLabel37.Text = "Hotel Rating"
        '
        'TxtHHotelRating
        '
        Me.TxtHHotelRating.Location = New System.Drawing.Point(117, 102)
        Me.TxtHHotelRating.MendatroryField = True
        Me.TxtHHotelRating.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHHotelRating.MyLinkLable1 = Me.MyLabel37
        Me.TxtHHotelRating.MyLinkLable2 = Nothing
        Me.TxtHHotelRating.MyReadOnly = False
        Me.TxtHHotelRating.MyShowMasterFormButton = False
        Me.TxtHHotelRating.Name = "TxtHHotelRating"
        Me.TxtHHotelRating.Size = New System.Drawing.Size(120, 19)
        Me.TxtHHotelRating.TabIndex = 5
        Me.TxtHHotelRating.Value = ""
        '
        'LblDays
        '
        Me.LblDays.AutoSize = False
        Me.LblDays.BorderVisible = True
        Me.LblDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDays.Location = New System.Drawing.Point(117, 53)
        Me.LblDays.Name = "LblDays"
        Me.LblDays.Size = New System.Drawing.Size(28, 18)
        Me.LblDays.TabIndex = 2
        Me.LblDays.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblDays.TextWrap = False
        '
        'MyLabel39
        '
        Me.MyLabel39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel39.Location = New System.Drawing.Point(15, 54)
        Me.MyLabel39.Name = "MyLabel39"
        Me.MyLabel39.Size = New System.Drawing.Size(32, 16)
        Me.MyLabel39.TabIndex = 146
        Me.MyLabel39.Text = "Days"
        '
        'dtpStayTo
        '
        Me.dtpStayTo.CustomFormat = "dd/MM/yyyy "
        Me.dtpStayTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStayTo.Location = New System.Drawing.Point(375, 32)
        Me.dtpStayTo.MendatroryField = True
        Me.dtpStayTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStayTo.MyLinkLable1 = Me.MyLabel40
        Me.dtpStayTo.MyLinkLable2 = Nothing
        Me.dtpStayTo.Name = "dtpStayTo"
        Me.dtpStayTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStayTo.Size = New System.Drawing.Size(108, 18)
        Me.dtpStayTo.TabIndex = 1
        Me.dtpStayTo.TabStop = False
        Me.dtpStayTo.Text = "03/05/2011 "
        Me.dtpStayTo.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel40
        '
        Me.MyLabel40.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel40.Location = New System.Drawing.Point(286, 33)
        Me.MyLabel40.Name = "MyLabel40"
        Me.MyLabel40.Size = New System.Drawing.Size(85, 16)
        Me.MyLabel40.TabIndex = 145
        Me.MyLabel40.Text = "Date of Stay To"
        '
        'dtpHStayFrom
        '
        Me.dtpHStayFrom.CustomFormat = "dd/MM/yyyy "
        Me.dtpHStayFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHStayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHStayFrom.Location = New System.Drawing.Point(117, 32)
        Me.dtpHStayFrom.MendatroryField = True
        Me.dtpHStayFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpHStayFrom.MyLinkLable1 = Me.MyLabel42
        Me.dtpHStayFrom.MyLinkLable2 = Nothing
        Me.dtpHStayFrom.Name = "dtpHStayFrom"
        Me.dtpHStayFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpHStayFrom.Size = New System.Drawing.Size(120, 18)
        Me.dtpHStayFrom.TabIndex = 0
        Me.dtpHStayFrom.TabStop = False
        Me.dtpHStayFrom.Text = "03/05/2011 "
        Me.dtpHStayFrom.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel42
        '
        Me.MyLabel42.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel42.Location = New System.Drawing.Point(15, 33)
        Me.MyLabel42.Name = "MyLabel42"
        Me.MyLabel42.Size = New System.Drawing.Size(98, 16)
        Me.MyLabel42.TabIndex = 144
        Me.MyLabel42.Text = "Date of Stay From"
        '
        'RadPageViewPage4
        '
        Me.RadPageViewPage4.Controls.Add(Me.RadGroupBox9)
        Me.RadPageViewPage4.ItemSize = New System.Drawing.SizeF(70.0!, 28.0!)
        Me.RadPageViewPage4.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage4.Name = "RadPageViewPage4"
        Me.RadPageViewPage4.Size = New System.Drawing.Size(949, 346)
        Me.RadPageViewPage4.Text = "Car Details"
        '
        'RadGroupBox9
        '
        Me.RadGroupBox9.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox9.Controls.Add(Me.TxtAmount)
        Me.RadGroupBox9.Controls.Add(Me.TxtCRemarks)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel34)
        Me.RadGroupBox9.Controls.Add(Me.LblLocTo)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel59)
        Me.RadGroupBox9.Controls.Add(Me.TxtLocTo)
        Me.RadGroupBox9.Controls.Add(Me.LblTypesOfCar)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel57)
        Me.RadGroupBox9.Controls.Add(Me.TxtTypesCar)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel61)
        Me.RadGroupBox9.Controls.Add(Me.LblLocFrom)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel66)
        Me.RadGroupBox9.Controls.Add(Me.TxtLocFrom)
        Me.RadGroupBox9.Controls.Add(Me.dtpPeriodTo)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel69)
        Me.RadGroupBox9.Controls.Add(Me.dtpPeriodFrom)
        Me.RadGroupBox9.Controls.Add(Me.MyLabel70)
        Me.RadGroupBox9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox9.HeaderText = "Car Details"
        Me.RadGroupBox9.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox9.Name = "RadGroupBox9"
        Me.RadGroupBox9.Size = New System.Drawing.Size(949, 346)
        Me.RadGroupBox9.TabIndex = 0
        Me.RadGroupBox9.Text = "Car Details"
        '
        'TxtAmount
        '
        Me.TxtAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtAmount.DecimalPlaces = 3
        Me.TxtAmount.Location = New System.Drawing.Point(118, 123)
        Me.TxtAmount.MaxLength = 10
        Me.TxtAmount.MendatroryField = True
        Me.TxtAmount.MyLinkLable1 = Nothing
        Me.TxtAmount.MyLinkLable2 = Nothing
        Me.TxtAmount.Name = "TxtAmount"
        Me.TxtAmount.Size = New System.Drawing.Size(118, 20)
        Me.TxtAmount.TabIndex = 5
        Me.TxtAmount.Text = "0"
        Me.TxtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAmount.Value = 0.0R
        '
        'TxtCRemarks
        '
        Me.TxtCRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.TxtCRemarks.Location = New System.Drawing.Point(607, 26)
        Me.TxtCRemarks.MaxLength = 150
        Me.TxtCRemarks.MendatroryField = True
        Me.TxtCRemarks.MyLinkLable1 = Me.MyLabel34
        Me.TxtCRemarks.MyLinkLable2 = Nothing
        Me.TxtCRemarks.Name = "TxtCRemarks"
        Me.TxtCRemarks.Size = New System.Drawing.Size(338, 20)
        Me.TxtCRemarks.TabIndex = 6
        '
        'MyLabel34
        '
        Me.MyLabel34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel34.Location = New System.Drawing.Point(547, 29)
        Me.MyLabel34.Name = "MyLabel34"
        Me.MyLabel34.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel34.TabIndex = 176
        Me.MyLabel34.Text = "Remarks"
        '
        'LblLocTo
        '
        Me.LblLocTo.AutoSize = False
        Me.LblLocTo.BorderVisible = True
        Me.LblLocTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLocTo.Location = New System.Drawing.Point(245, 76)
        Me.LblLocTo.Name = "LblLocTo"
        Me.LblLocTo.Size = New System.Drawing.Size(257, 18)
        Me.LblLocTo.TabIndex = 174
        Me.LblLocTo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblLocTo.TextWrap = False
        '
        'MyLabel59
        '
        Me.MyLabel59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel59.Location = New System.Drawing.Point(15, 76)
        Me.MyLabel59.Name = "MyLabel59"
        Me.MyLabel59.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel59.TabIndex = 173
        Me.MyLabel59.Text = "Location To"
        '
        'TxtLocTo
        '
        Me.TxtLocTo.Location = New System.Drawing.Point(118, 75)
        Me.TxtLocTo.MendatroryField = True
        Me.TxtLocTo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLocTo.MyLinkLable1 = Me.MyLabel59
        Me.TxtLocTo.MyLinkLable2 = Nothing
        Me.TxtLocTo.MyReadOnly = False
        Me.TxtLocTo.MyShowMasterFormButton = False
        Me.TxtLocTo.Name = "TxtLocTo"
        Me.TxtLocTo.Size = New System.Drawing.Size(120, 19)
        Me.TxtLocTo.TabIndex = 3
        Me.TxtLocTo.Value = ""
        '
        'LblTypesOfCar
        '
        Me.LblTypesOfCar.AutoSize = False
        Me.LblTypesOfCar.BorderVisible = True
        Me.LblTypesOfCar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTypesOfCar.Location = New System.Drawing.Point(245, 99)
        Me.LblTypesOfCar.Name = "LblTypesOfCar"
        Me.LblTypesOfCar.Size = New System.Drawing.Size(257, 18)
        Me.LblTypesOfCar.TabIndex = 171
        Me.LblTypesOfCar.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTypesOfCar.TextWrap = False
        '
        'MyLabel57
        '
        Me.MyLabel57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel57.Location = New System.Drawing.Point(15, 100)
        Me.MyLabel57.Name = "MyLabel57"
        Me.MyLabel57.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel57.TabIndex = 170
        Me.MyLabel57.Text = "Types Of Car"
        '
        'TxtTypesCar
        '
        Me.TxtTypesCar.Location = New System.Drawing.Point(118, 99)
        Me.TxtTypesCar.MendatroryField = True
        Me.TxtTypesCar.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTypesCar.MyLinkLable1 = Me.MyLabel57
        Me.TxtTypesCar.MyLinkLable2 = Nothing
        Me.TxtTypesCar.MyReadOnly = False
        Me.TxtTypesCar.MyShowMasterFormButton = False
        Me.TxtTypesCar.Name = "TxtTypesCar"
        Me.TxtTypesCar.Size = New System.Drawing.Size(120, 19)
        Me.TxtTypesCar.TabIndex = 4
        Me.TxtTypesCar.Value = ""
        '
        'MyLabel61
        '
        Me.MyLabel61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel61.Location = New System.Drawing.Point(15, 126)
        Me.MyLabel61.Name = "MyLabel61"
        Me.MyLabel61.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel61.TabIndex = 165
        Me.MyLabel61.Text = "Amount"
        '
        'LblLocFrom
        '
        Me.LblLocFrom.AutoSize = False
        Me.LblLocFrom.BorderVisible = True
        Me.LblLocFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLocFrom.Location = New System.Drawing.Point(245, 52)
        Me.LblLocFrom.Name = "LblLocFrom"
        Me.LblLocFrom.Size = New System.Drawing.Size(257, 18)
        Me.LblLocFrom.TabIndex = 150
        Me.LblLocFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblLocFrom.TextWrap = False
        '
        'MyLabel66
        '
        Me.MyLabel66.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel66.Location = New System.Drawing.Point(15, 54)
        Me.MyLabel66.Name = "MyLabel66"
        Me.MyLabel66.Size = New System.Drawing.Size(79, 16)
        Me.MyLabel66.TabIndex = 149
        Me.MyLabel66.Text = "Location From"
        '
        'TxtLocFrom
        '
        Me.TxtLocFrom.Location = New System.Drawing.Point(118, 51)
        Me.TxtLocFrom.MendatroryField = True
        Me.TxtLocFrom.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLocFrom.MyLinkLable1 = Me.MyLabel66
        Me.TxtLocFrom.MyLinkLable2 = Nothing
        Me.TxtLocFrom.MyReadOnly = False
        Me.TxtLocFrom.MyShowMasterFormButton = False
        Me.TxtLocFrom.Name = "TxtLocFrom"
        Me.TxtLocFrom.Size = New System.Drawing.Size(120, 19)
        Me.TxtLocFrom.TabIndex = 2
        Me.TxtLocFrom.Value = ""
        '
        'dtpPeriodTo
        '
        Me.dtpPeriodTo.CustomFormat = "dd/MM/yyyy "
        Me.dtpPeriodTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPeriodTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPeriodTo.Location = New System.Drawing.Point(394, 30)
        Me.dtpPeriodTo.MendatroryField = True
        Me.dtpPeriodTo.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPeriodTo.MyLinkLable1 = Me.MyLabel69
        Me.dtpPeriodTo.MyLinkLable2 = Nothing
        Me.dtpPeriodTo.Name = "dtpPeriodTo"
        Me.dtpPeriodTo.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPeriodTo.Size = New System.Drawing.Size(108, 18)
        Me.dtpPeriodTo.TabIndex = 1
        Me.dtpPeriodTo.TabStop = False
        Me.dtpPeriodTo.Text = "03/05/2011 "
        Me.dtpPeriodTo.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel69
        '
        Me.MyLabel69.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel69.Location = New System.Drawing.Point(332, 31)
        Me.MyLabel69.Name = "MyLabel69"
        Me.MyLabel69.Size = New System.Drawing.Size(55, 16)
        Me.MyLabel69.TabIndex = 145
        Me.MyLabel69.Text = "Period To"
        '
        'dtpPeriodFrom
        '
        Me.dtpPeriodFrom.CustomFormat = "dd/MM/yyyy "
        Me.dtpPeriodFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPeriodFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPeriodFrom.Location = New System.Drawing.Point(118, 29)
        Me.dtpPeriodFrom.MendatroryField = True
        Me.dtpPeriodFrom.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPeriodFrom.MyLinkLable1 = Me.MyLabel70
        Me.dtpPeriodFrom.MyLinkLable2 = Nothing
        Me.dtpPeriodFrom.Name = "dtpPeriodFrom"
        Me.dtpPeriodFrom.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpPeriodFrom.Size = New System.Drawing.Size(120, 18)
        Me.dtpPeriodFrom.TabIndex = 0
        Me.dtpPeriodFrom.TabStop = False
        Me.dtpPeriodFrom.Text = "03/05/2011 "
        Me.dtpPeriodFrom.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel70
        '
        Me.MyLabel70.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel70.Location = New System.Drawing.Point(15, 30)
        Me.MyLabel70.Name = "MyLabel70"
        Me.MyLabel70.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel70.TabIndex = 144
        Me.MyLabel70.Text = "Period From"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(10, 17)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(89, 17)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(894, 17)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'FrmHRRaiseTravelRequisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(970, 451)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmHRRaiseTravelRequisition"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Raise Travel Requisition"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.RadGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox6.ResumeLayout(False)
        Me.RadGroupBox6.PerformLayout()
        CType(Me.LblDesgName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblDeptName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCompName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChillingVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesgCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeptCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.LblTravelPur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblBookingFor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnInternational, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnDomestic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTravelRqst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPPDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.LblBookedByName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbTBookedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTCouponNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTFlightNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTravelMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTNoOfDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpArrivalDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDepartureDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblToLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblFromLoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.LblNight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbBookByHotel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblRoomType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblHotelRating, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStayTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpHStayFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel42, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage4.ResumeLayout(False)
        CType(Me.RadGroupBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox9.ResumeLayout(False)
        Me.RadGroupBox9.PerformLayout()
        CType(Me.TxtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel59, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblTypesOfCar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel57, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel61, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblLocFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel66, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPeriodTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel69, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpPeriodFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel70, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents cmbTravelRqst As common.Controls.MyComboBox
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents dtpFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPPDate As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage4 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox6 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents LblDesgName As common.Controls.MyLabel
    Friend WithEvents LblDeptName As common.Controls.MyLabel
    Friend WithEvents LblCompName As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents txtcode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblChillingVendor As common.Controls.MyLabel
    Friend WithEvents lblDesgCode As common.Controls.MyLabel
    Friend WithEvents lblDeptCode As common.Controls.MyLabel
    Friend WithEvents MyLabel43 As common.Controls.MyLabel
    Friend WithEvents lblCompCode As common.Controls.MyLabel
    Friend WithEvents MyLabel44 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents rbtnInternational As common.Controls.MyRadioButton
    Friend WithEvents rbtnDomestic As common.Controls.MyRadioButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents LblTCategory As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents TxtTCategory As common.UserControls.txtFinder
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents TxtTCouponNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents TxtTFlightNo As common.Controls.MyTextBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents LblTClass As common.Controls.MyLabel
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents TxtTClass As common.UserControls.txtFinder
    Friend WithEvents LblTravelMode As common.Controls.MyLabel
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents TxtTravelMode As common.UserControls.txtFinder
    Friend WithEvents LblTNoOfDays As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents dtpArrivalDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents dtpDepartureDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents LblToLoc As common.Controls.MyLabel
    Friend WithEvents MyLabel10 As common.Controls.MyLabel
    Friend WithEvents TxtToLoc As common.UserControls.txtFinder
    Friend WithEvents LblFromLoc As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents TxtFromLoc As common.UserControls.txtFinder
    Friend WithEvents TxtTRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents CmbTBookedBy As common.Controls.MyComboBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents LblHotelRating As common.Controls.MyLabel
    Friend WithEvents MyLabel37 As common.Controls.MyLabel
    Friend WithEvents TxtHHotelRating As common.UserControls.txtFinder
    Friend WithEvents LblDays As common.Controls.MyLabel
    Friend WithEvents MyLabel39 As common.Controls.MyLabel
    Friend WithEvents dtpStayTo As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel40 As common.Controls.MyLabel
    Friend WithEvents dtpHStayFrom As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel42 As common.Controls.MyLabel
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox9 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents TxtAmount As common.MyNumBox
    Friend WithEvents TxtCRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel34 As common.Controls.MyLabel
    Friend WithEvents LblLocTo As common.Controls.MyLabel
    Friend WithEvents MyLabel59 As common.Controls.MyLabel
    Friend WithEvents TxtLocTo As common.UserControls.txtFinder
    Friend WithEvents LblTypesOfCar As common.Controls.MyLabel
    Friend WithEvents MyLabel57 As common.Controls.MyLabel
    Friend WithEvents TxtTypesCar As common.UserControls.txtFinder
    Friend WithEvents MyLabel61 As common.Controls.MyLabel
    Friend WithEvents LblLocFrom As common.Controls.MyLabel
    Friend WithEvents MyLabel66 As common.Controls.MyLabel
    Friend WithEvents TxtLocFrom As common.UserControls.txtFinder
    Friend WithEvents dtpPeriodTo As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel69 As common.Controls.MyLabel
    Friend WithEvents dtpPeriodFrom As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel70 As common.Controls.MyLabel
    Friend WithEvents LblRoomType As common.Controls.MyLabel
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents TxtRoomType As common.UserControls.txtFinder
    Friend WithEvents CmbBookByHotel As common.Controls.MyComboBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents LblNight As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents LblBookingFor As common.Controls.MyLabel
    Friend WithEvents TxtBookingFor As common.UserControls.txtFinder
    Friend WithEvents LblTravelPur As common.Controls.MyLabel
    Friend WithEvents TxtTravelPur As common.UserControls.txtFinder
    Friend WithEvents LblBookedByName As common.Controls.MyLabel
    Friend WithEvents TxtBookedByName As common.UserControls.txtFinder
End Class

