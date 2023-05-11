Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayrollSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayrollSetting))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblempcode = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.pageGeneral = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtGratuityPeriod = New common.MyNumBox()
        Me.lblGratuityPeriod = New common.Controls.MyLabel()
        Me.txtStatutoryDLWorkingHours = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.MyLabel18 = New common.Controls.MyLabel()
        Me.RadCheckBox1 = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.chkRemoveAttdList = New Telerik.WinControls.UI.RadCheckBox()
        Me.RadGroupBox7 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbLAYearly = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbLAMonthly = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtStatutoryWK_WH = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.chkTreatWOffLeaveContinuous = New Telerik.WinControls.UI.RadCheckBox()
        Me.fndLocationCode = New common.UserControls.txtFinder()
        Me.lblLocationDesc = New common.Controls.MyLabel()
        Me.chkAutoAttendance = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtEarlyArrivalMintOT = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.txtMinHoursHFDay = New common.MyNumBox()
        Me.txtIntervalMinutes = New common.MyNumBox()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtMinHoursOT = New common.MyNumBox()
        Me.txtMinimumPdaysWKOff = New common.MyNumBox()
        Me.MyLabel20 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.txtMaximumHoursOT = New common.MyNumBox()
        Me.txtWorkingHours = New common.MyNumBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.txtMaximumMintLateComming = New common.MyNumBox()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.txtNoSLForHFDay = New common.MyNumBox()
        Me.MyLabel17 = New common.Controls.MyLabel()
        Me.txtNoLcommingSL = New common.MyNumBox()
        Me.pageCTC = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvCTC = New common.UserControls.MyRadGridView()
        Me.pageGross = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvGross = New common.UserControls.MyRadGridView()
        Me.pageInHand = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gvInHand = New common.UserControls.MyRadGridView()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.rdbSetDefault = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.RtimeFrom = New Telerik.WinControls.UI.RadTimePicker()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.RTimeTo = New Telerik.WinControls.UI.RadTimePicker()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        CType(Me.txtGratuityPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGratuityPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStatutoryDLWorkingHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRemoveAttdList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox7.SuspendLayout()
        CType(Me.rdbLAYearly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbLAMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStatutoryWK_WH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTreatWOffLeaveContinuous, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAutoAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEarlyArrivalMintOT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinHoursHFDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIntervalMinutes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinHoursOT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinimumPdaysWKOff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaximumHoursOT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWorkingHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaximumMintLateComming, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoSLForHFDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoLcommingSL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageCTC.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.gvCTC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCTC.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageGross.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gvGross, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvGross.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageInHand.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.gvInHand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvInHand.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSetDefault, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RtimeFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RTimeTo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblempcode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbSetDefault)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(986, 594)
        Me.SplitContainer1.SplitterDistance = 549
        Me.SplitContainer1.TabIndex = 0
        '
        'lblempcode
        '
        Me.lblempcode.FieldName = Nothing
        Me.lblempcode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblempcode.Location = New System.Drawing.Point(37, 17)
        Me.lblempcode.Name = "lblempcode"
        Me.lblempcode.Size = New System.Drawing.Size(33, 16)
        Me.lblempcode.TabIndex = 119
        Me.lblempcode.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(82, 14)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblempcode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(344, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'RadPageView1
        '
        Me.RadPageView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadPageView1.Controls.Add(Me.pageGeneral)
        Me.RadPageView1.Controls.Add(Me.pageCTC)
        Me.RadPageView1.Controls.Add(Me.pageGross)
        Me.RadPageView1.Controls.Add(Me.pageInHand)
        Me.RadPageView1.Location = New System.Drawing.Point(10, 41)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.pageGeneral
        Me.RadPageView1.Size = New System.Drawing.Size(970, 505)
        Me.RadPageView1.TabIndex = 232
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.MyLabel6)
        Me.pageGeneral.Controls.Add(Me.MyLabel5)
        Me.pageGeneral.Controls.Add(Me.RTimeTo)
        Me.pageGeneral.Controls.Add(Me.RtimeFrom)
        Me.pageGeneral.Controls.Add(Me.txtGratuityPeriod)
        Me.pageGeneral.Controls.Add(Me.lblGratuityPeriod)
        Me.pageGeneral.Controls.Add(Me.txtStatutoryDLWorkingHours)
        Me.pageGeneral.Controls.Add(Me.MyLabel18)
        Me.pageGeneral.Controls.Add(Me.MyLabel3)
        Me.pageGeneral.Controls.Add(Me.RadCheckBox1)
        Me.pageGeneral.Controls.Add(Me.MyLabel4)
        Me.pageGeneral.Controls.Add(Me.chkRemoveAttdList)
        Me.pageGeneral.Controls.Add(Me.RadGroupBox7)
        Me.pageGeneral.Controls.Add(Me.txtStatutoryWK_WH)
        Me.pageGeneral.Controls.Add(Me.chkTreatWOffLeaveContinuous)
        Me.pageGeneral.Controls.Add(Me.fndLocationCode)
        Me.pageGeneral.Controls.Add(Me.MyLabel2)
        Me.pageGeneral.Controls.Add(Me.lblLocationDesc)
        Me.pageGeneral.Controls.Add(Me.chkAutoAttendance)
        Me.pageGeneral.Controls.Add(Me.txtEarlyArrivalMintOT)
        Me.pageGeneral.Controls.Add(Me.MyLabel11)
        Me.pageGeneral.Controls.Add(Me.MyLabel1)
        Me.pageGeneral.Controls.Add(Me.txtMinHoursHFDay)
        Me.pageGeneral.Controls.Add(Me.txtIntervalMinutes)
        Me.pageGeneral.Controls.Add(Me.MyLabel13)
        Me.pageGeneral.Controls.Add(Me.MyLabel21)
        Me.pageGeneral.Controls.Add(Me.txtMinHoursOT)
        Me.pageGeneral.Controls.Add(Me.txtMinimumPdaysWKOff)
        Me.pageGeneral.Controls.Add(Me.MyLabel14)
        Me.pageGeneral.Controls.Add(Me.MyLabel20)
        Me.pageGeneral.Controls.Add(Me.txtMaximumHoursOT)
        Me.pageGeneral.Controls.Add(Me.txtWorkingHours)
        Me.pageGeneral.Controls.Add(Me.MyLabel15)
        Me.pageGeneral.Controls.Add(Me.MyLabel19)
        Me.pageGeneral.Controls.Add(Me.txtMaximumMintLateComming)
        Me.pageGeneral.Controls.Add(Me.MyLabel16)
        Me.pageGeneral.Controls.Add(Me.txtNoSLForHFDay)
        Me.pageGeneral.Controls.Add(Me.txtNoLcommingSL)
        Me.pageGeneral.Controls.Add(Me.MyLabel17)
        Me.pageGeneral.ItemSize = New System.Drawing.SizeF(55.0!, 28.0!)
        Me.pageGeneral.Location = New System.Drawing.Point(10, 37)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Size = New System.Drawing.Size(949, 457)
        Me.pageGeneral.Text = "General"
        '
        'txtGratuityPeriod
        '
        Me.txtGratuityPeriod.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtGratuityPeriod.CalculationExpression = Nothing
        Me.txtGratuityPeriod.DecimalPlaces = 2
        Me.txtGratuityPeriod.FieldCode = Nothing
        Me.txtGratuityPeriod.FieldDesc = Nothing
        Me.txtGratuityPeriod.FieldMaxLength = 0
        Me.txtGratuityPeriod.FieldName = Nothing
        Me.txtGratuityPeriod.isCalculatedField = False
        Me.txtGratuityPeriod.IsSourceFromTable = False
        Me.txtGratuityPeriod.IsSourceFromValueList = False
        Me.txtGratuityPeriod.IsUnique = False
        Me.txtGratuityPeriod.Location = New System.Drawing.Point(231, 359)
        Me.txtGratuityPeriod.MaxLength = 6
        Me.txtGratuityPeriod.MendatroryField = True
        Me.txtGratuityPeriod.MyLinkLable1 = Me.lblGratuityPeriod
        Me.txtGratuityPeriod.MyLinkLable2 = Nothing
        Me.txtGratuityPeriod.Name = "txtGratuityPeriod"
        Me.txtGratuityPeriod.ReferenceFieldDesc = Nothing
        Me.txtGratuityPeriod.ReferenceFieldName = Nothing
        Me.txtGratuityPeriod.ReferenceTableName = Nothing
        Me.txtGratuityPeriod.Size = New System.Drawing.Size(122, 20)
        Me.txtGratuityPeriod.TabIndex = 231
        Me.txtGratuityPeriod.Text = "0"
        Me.txtGratuityPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGratuityPeriod.Value = 0.0R
        '
        'lblGratuityPeriod
        '
        Me.lblGratuityPeriod.FieldName = Nothing
        Me.lblGratuityPeriod.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGratuityPeriod.Location = New System.Drawing.Point(17, 360)
        Me.lblGratuityPeriod.Name = "lblGratuityPeriod"
        Me.lblGratuityPeriod.Size = New System.Drawing.Size(82, 16)
        Me.lblGratuityPeriod.TabIndex = 232
        Me.lblGratuityPeriod.Text = "Gratuity Period"
        '
        'txtStatutoryDLWorkingHours
        '
        Me.txtStatutoryDLWorkingHours.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStatutoryDLWorkingHours.CalculationExpression = Nothing
        Me.txtStatutoryDLWorkingHours.DecimalPlaces = 2
        Me.txtStatutoryDLWorkingHours.FieldCode = Nothing
        Me.txtStatutoryDLWorkingHours.FieldDesc = Nothing
        Me.txtStatutoryDLWorkingHours.FieldMaxLength = 0
        Me.txtStatutoryDLWorkingHours.FieldName = Nothing
        Me.txtStatutoryDLWorkingHours.isCalculatedField = False
        Me.txtStatutoryDLWorkingHours.IsSourceFromTable = False
        Me.txtStatutoryDLWorkingHours.IsSourceFromValueList = False
        Me.txtStatutoryDLWorkingHours.IsUnique = False
        Me.txtStatutoryDLWorkingHours.Location = New System.Drawing.Point(231, 335)
        Me.txtStatutoryDLWorkingHours.MaxLength = 6
        Me.txtStatutoryDLWorkingHours.MendatroryField = True
        Me.txtStatutoryDLWorkingHours.MyLinkLable1 = Me.MyLabel3
        Me.txtStatutoryDLWorkingHours.MyLinkLable2 = Nothing
        Me.txtStatutoryDLWorkingHours.Name = "txtStatutoryDLWorkingHours"
        Me.txtStatutoryDLWorkingHours.ReferenceFieldDesc = Nothing
        Me.txtStatutoryDLWorkingHours.ReferenceFieldName = Nothing
        Me.txtStatutoryDLWorkingHours.ReferenceTableName = Nothing
        Me.txtStatutoryDLWorkingHours.Size = New System.Drawing.Size(122, 20)
        Me.txtStatutoryDLWorkingHours.TabIndex = 229
        Me.txtStatutoryDLWorkingHours.Text = "0"
        Me.txtStatutoryDLWorkingHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStatutoryDLWorkingHours.Value = 0.0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(17, 338)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(152, 16)
        Me.MyLabel3.TabIndex = 230
        Me.MyLabel3.Text = "Statutory daily working hours"
        '
        'MyLabel18
        '
        Me.MyLabel18.FieldName = Nothing
        Me.MyLabel18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel18.Location = New System.Drawing.Point(17, 27)
        Me.MyLabel18.Name = "MyLabel18"
        Me.MyLabel18.Size = New System.Drawing.Size(49, 16)
        Me.MyLabel18.TabIndex = 55
        Me.MyLabel18.Text = "Location"
        '
        'RadCheckBox1
        '
        Me.RadCheckBox1.Location = New System.Drawing.Point(231, 3)
        Me.RadCheckBox1.Name = "RadCheckBox1"
        Me.RadCheckBox1.Size = New System.Drawing.Size(215, 18)
        Me.RadCheckBox1.TabIndex = 120
        Me.RadCheckBox1.Text = "Apply Common setting for all locations"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(373, 137)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(116, 16)
        Me.MyLabel4.TabIndex = 204
        Me.MyLabel4.Text = "Leave Allotment Time"
        '
        'chkRemoveAttdList
        '
        Me.chkRemoveAttdList.Location = New System.Drawing.Point(373, 96)
        Me.chkRemoveAttdList.Name = "chkRemoveAttdList"
        Me.chkRemoveAttdList.Size = New System.Drawing.Size(386, 18)
        Me.chkRemoveAttdList.TabIndex = 218
        Me.chkRemoveAttdList.Text = "Remove contractor from attendance list if present day izero in any month"
        '
        'RadGroupBox7
        '
        Me.RadGroupBox7.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox7.Controls.Add(Me.rdbLAYearly)
        Me.RadGroupBox7.Controls.Add(Me.rdbLAMonthly)
        Me.RadGroupBox7.HeaderText = ""
        Me.RadGroupBox7.Location = New System.Drawing.Point(501, 120)
        Me.RadGroupBox7.Name = "RadGroupBox7"
        Me.RadGroupBox7.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox7.Size = New System.Drawing.Size(225, 53)
        Me.RadGroupBox7.TabIndex = 203
        '
        'rdbLAYearly
        '
        Me.rdbLAYearly.Location = New System.Drawing.Point(152, 15)
        Me.rdbLAYearly.Name = "rdbLAYearly"
        Me.rdbLAYearly.Size = New System.Drawing.Size(50, 18)
        Me.rdbLAYearly.TabIndex = 1
        Me.rdbLAYearly.Text = "Yearly"
        '
        'rdbLAMonthly
        '
        Me.rdbLAMonthly.Location = New System.Drawing.Point(18, 15)
        Me.rdbLAMonthly.Name = "rdbLAMonthly"
        Me.rdbLAMonthly.Size = New System.Drawing.Size(62, 18)
        Me.rdbLAMonthly.TabIndex = 0
        Me.rdbLAMonthly.Text = "Monthly"
        '
        'txtStatutoryWK_WH
        '
        Me.txtStatutoryWK_WH.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStatutoryWK_WH.CalculationExpression = Nothing
        Me.txtStatutoryWK_WH.DecimalPlaces = 2
        Me.txtStatutoryWK_WH.FieldCode = Nothing
        Me.txtStatutoryWK_WH.FieldDesc = Nothing
        Me.txtStatutoryWK_WH.FieldMaxLength = 0
        Me.txtStatutoryWK_WH.FieldName = Nothing
        Me.txtStatutoryWK_WH.isCalculatedField = False
        Me.txtStatutoryWK_WH.IsSourceFromTable = False
        Me.txtStatutoryWK_WH.IsSourceFromValueList = False
        Me.txtStatutoryWK_WH.IsUnique = False
        Me.txtStatutoryWK_WH.Location = New System.Drawing.Point(231, 309)
        Me.txtStatutoryWK_WH.MaxLength = 6
        Me.txtStatutoryWK_WH.MendatroryField = True
        Me.txtStatutoryWK_WH.MyLinkLable1 = Me.MyLabel2
        Me.txtStatutoryWK_WH.MyLinkLable2 = Nothing
        Me.txtStatutoryWK_WH.Name = "txtStatutoryWK_WH"
        Me.txtStatutoryWK_WH.ReferenceFieldDesc = Nothing
        Me.txtStatutoryWK_WH.ReferenceFieldName = Nothing
        Me.txtStatutoryWK_WH.ReferenceTableName = Nothing
        Me.txtStatutoryWK_WH.Size = New System.Drawing.Size(122, 20)
        Me.txtStatutoryWK_WH.TabIndex = 227
        Me.txtStatutoryWK_WH.Text = "0"
        Me.txtStatutoryWK_WH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStatutoryWK_WH.Value = 0.0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(17, 312)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(163, 16)
        Me.MyLabel2.TabIndex = 228
        Me.MyLabel2.Text = "Statutory weekly working hours"
        '
        'chkTreatWOffLeaveContinuous
        '
        Me.chkTreatWOffLeaveContinuous.Location = New System.Drawing.Point(373, 75)
        Me.chkTreatWOffLeaveContinuous.Name = "chkTreatWOffLeaveContinuous"
        Me.chkTreatWOffLeaveContinuous.Size = New System.Drawing.Size(353, 18)
        Me.chkTreatWOffLeaveContinuous.TabIndex = 211
        Me.chkTreatWOffLeaveContinuous.Text = "Treat weekly of as leave if it is in continuous with any leave applied"
        '
        'fndLocationCode
        '
        Me.fndLocationCode.CalculationExpression = Nothing
        Me.fndLocationCode.FieldCode = Nothing
        Me.fndLocationCode.FieldDesc = Nothing
        Me.fndLocationCode.FieldMaxLength = 0
        Me.fndLocationCode.FieldName = Nothing
        Me.fndLocationCode.isCalculatedField = False
        Me.fndLocationCode.IsSourceFromTable = False
        Me.fndLocationCode.IsSourceFromValueList = False
        Me.fndLocationCode.IsUnique = False
        Me.fndLocationCode.Location = New System.Drawing.Point(231, 24)
        Me.fndLocationCode.MendatroryField = True
        Me.fndLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocationCode.MyLinkLable1 = Me.lblempcode
        Me.fndLocationCode.MyLinkLable2 = Nothing
        Me.fndLocationCode.MyReadOnly = False
        Me.fndLocationCode.MyShowMasterFormButton = False
        Me.fndLocationCode.Name = "fndLocationCode"
        Me.fndLocationCode.ReferenceFieldDesc = Nothing
        Me.fndLocationCode.ReferenceFieldName = Nothing
        Me.fndLocationCode.ReferenceTableName = Nothing
        Me.fndLocationCode.Size = New System.Drawing.Size(122, 19)
        Me.fndLocationCode.TabIndex = 121
        Me.fndLocationCode.Value = ""
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.AutoSize = False
        Me.lblLocationDesc.BorderVisible = True
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.Location = New System.Drawing.Point(359, 24)
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.Size = New System.Drawing.Size(354, 19)
        Me.lblLocationDesc.TabIndex = 201
        Me.lblLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkAutoAttendance
        '
        Me.chkAutoAttendance.Location = New System.Drawing.Point(373, 51)
        Me.chkAutoAttendance.Name = "chkAutoAttendance"
        Me.chkAutoAttendance.Size = New System.Drawing.Size(185, 18)
        Me.chkAutoAttendance.TabIndex = 202
        Me.chkAutoAttendance.Text = "Apply Auto Generate Attendance"
        '
        'txtEarlyArrivalMintOT
        '
        Me.txtEarlyArrivalMintOT.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtEarlyArrivalMintOT.CalculationExpression = Nothing
        Me.txtEarlyArrivalMintOT.DecimalPlaces = 2
        Me.txtEarlyArrivalMintOT.FieldCode = Nothing
        Me.txtEarlyArrivalMintOT.FieldDesc = Nothing
        Me.txtEarlyArrivalMintOT.FieldMaxLength = 0
        Me.txtEarlyArrivalMintOT.FieldName = Nothing
        Me.txtEarlyArrivalMintOT.isCalculatedField = False
        Me.txtEarlyArrivalMintOT.IsSourceFromTable = False
        Me.txtEarlyArrivalMintOT.IsSourceFromValueList = False
        Me.txtEarlyArrivalMintOT.IsUnique = False
        Me.txtEarlyArrivalMintOT.Location = New System.Drawing.Point(231, 283)
        Me.txtEarlyArrivalMintOT.MaxLength = 6
        Me.txtEarlyArrivalMintOT.MendatroryField = True
        Me.txtEarlyArrivalMintOT.MyLinkLable1 = Me.MyLabel1
        Me.txtEarlyArrivalMintOT.MyLinkLable2 = Nothing
        Me.txtEarlyArrivalMintOT.Name = "txtEarlyArrivalMintOT"
        Me.txtEarlyArrivalMintOT.ReferenceFieldDesc = Nothing
        Me.txtEarlyArrivalMintOT.ReferenceFieldName = Nothing
        Me.txtEarlyArrivalMintOT.ReferenceTableName = Nothing
        Me.txtEarlyArrivalMintOT.Size = New System.Drawing.Size(122, 20)
        Me.txtEarlyArrivalMintOT.TabIndex = 225
        Me.txtEarlyArrivalMintOT.Text = "0"
        Me.txtEarlyArrivalMintOT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEarlyArrivalMintOT.Value = 0.0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(17, 286)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(172, 16)
        Me.MyLabel1.TabIndex = 226
        Me.MyLabel1.Text = "Early arrival minutes for overtime"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(17, 51)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(143, 16)
        Me.MyLabel11.TabIndex = 206
        Me.MyLabel11.Text = "Minimum hours for half day"
        '
        'txtMinHoursHFDay
        '
        Me.txtMinHoursHFDay.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMinHoursHFDay.CalculationExpression = Nothing
        Me.txtMinHoursHFDay.DecimalPlaces = 2
        Me.txtMinHoursHFDay.FieldCode = Nothing
        Me.txtMinHoursHFDay.FieldDesc = Nothing
        Me.txtMinHoursHFDay.FieldMaxLength = 0
        Me.txtMinHoursHFDay.FieldName = Nothing
        Me.txtMinHoursHFDay.isCalculatedField = False
        Me.txtMinHoursHFDay.IsSourceFromTable = False
        Me.txtMinHoursHFDay.IsSourceFromValueList = False
        Me.txtMinHoursHFDay.IsUnique = False
        Me.txtMinHoursHFDay.Location = New System.Drawing.Point(231, 49)
        Me.txtMinHoursHFDay.MaxLength = 6
        Me.txtMinHoursHFDay.MendatroryField = True
        Me.txtMinHoursHFDay.MyLinkLable1 = Me.MyLabel11
        Me.txtMinHoursHFDay.MyLinkLable2 = Nothing
        Me.txtMinHoursHFDay.Name = "txtMinHoursHFDay"
        Me.txtMinHoursHFDay.ReferenceFieldDesc = Nothing
        Me.txtMinHoursHFDay.ReferenceFieldName = Nothing
        Me.txtMinHoursHFDay.ReferenceTableName = Nothing
        Me.txtMinHoursHFDay.Size = New System.Drawing.Size(122, 20)
        Me.txtMinHoursHFDay.TabIndex = 205
        Me.txtMinHoursHFDay.Text = "0"
        Me.txtMinHoursHFDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinHoursHFDay.Value = 0.0R
        '
        'txtIntervalMinutes
        '
        Me.txtIntervalMinutes.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtIntervalMinutes.CalculationExpression = Nothing
        Me.txtIntervalMinutes.DecimalPlaces = 2
        Me.txtIntervalMinutes.FieldCode = Nothing
        Me.txtIntervalMinutes.FieldDesc = Nothing
        Me.txtIntervalMinutes.FieldMaxLength = 0
        Me.txtIntervalMinutes.FieldName = Nothing
        Me.txtIntervalMinutes.isCalculatedField = False
        Me.txtIntervalMinutes.IsSourceFromTable = False
        Me.txtIntervalMinutes.IsSourceFromValueList = False
        Me.txtIntervalMinutes.IsUnique = False
        Me.txtIntervalMinutes.Location = New System.Drawing.Point(231, 257)
        Me.txtIntervalMinutes.MaxLength = 6
        Me.txtIntervalMinutes.MendatroryField = True
        Me.txtIntervalMinutes.MyLinkLable1 = Me.MyLabel21
        Me.txtIntervalMinutes.MyLinkLable2 = Nothing
        Me.txtIntervalMinutes.Name = "txtIntervalMinutes"
        Me.txtIntervalMinutes.ReferenceFieldDesc = Nothing
        Me.txtIntervalMinutes.ReferenceFieldName = Nothing
        Me.txtIntervalMinutes.ReferenceTableName = Nothing
        Me.txtIntervalMinutes.Size = New System.Drawing.Size(122, 20)
        Me.txtIntervalMinutes.TabIndex = 223
        Me.txtIntervalMinutes.Text = "0"
        Me.txtIntervalMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtIntervalMinutes.Value = 0.0R
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel21.Location = New System.Drawing.Point(17, 260)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(86, 16)
        Me.MyLabel21.TabIndex = 224
        Me.MyLabel21.Text = "Interval Minutes"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(17, 77)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(147, 16)
        Me.MyLabel13.TabIndex = 208
        Me.MyLabel13.Text = "Minimum hours for overtime"
        '
        'txtMinHoursOT
        '
        Me.txtMinHoursOT.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMinHoursOT.CalculationExpression = Nothing
        Me.txtMinHoursOT.DecimalPlaces = 2
        Me.txtMinHoursOT.FieldCode = Nothing
        Me.txtMinHoursOT.FieldDesc = Nothing
        Me.txtMinHoursOT.FieldMaxLength = 0
        Me.txtMinHoursOT.FieldName = Nothing
        Me.txtMinHoursOT.isCalculatedField = False
        Me.txtMinHoursOT.IsSourceFromTable = False
        Me.txtMinHoursOT.IsSourceFromValueList = False
        Me.txtMinHoursOT.IsUnique = False
        Me.txtMinHoursOT.Location = New System.Drawing.Point(231, 75)
        Me.txtMinHoursOT.MaxLength = 6
        Me.txtMinHoursOT.MendatroryField = True
        Me.txtMinHoursOT.MyLinkLable1 = Me.MyLabel13
        Me.txtMinHoursOT.MyLinkLable2 = Nothing
        Me.txtMinHoursOT.Name = "txtMinHoursOT"
        Me.txtMinHoursOT.ReferenceFieldDesc = Nothing
        Me.txtMinHoursOT.ReferenceFieldName = Nothing
        Me.txtMinHoursOT.ReferenceTableName = Nothing
        Me.txtMinHoursOT.Size = New System.Drawing.Size(122, 20)
        Me.txtMinHoursOT.TabIndex = 207
        Me.txtMinHoursOT.Text = "0"
        Me.txtMinHoursOT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinHoursOT.Value = 0.0R
        '
        'txtMinimumPdaysWKOff
        '
        Me.txtMinimumPdaysWKOff.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMinimumPdaysWKOff.CalculationExpression = Nothing
        Me.txtMinimumPdaysWKOff.DecimalPlaces = 2
        Me.txtMinimumPdaysWKOff.FieldCode = Nothing
        Me.txtMinimumPdaysWKOff.FieldDesc = Nothing
        Me.txtMinimumPdaysWKOff.FieldMaxLength = 0
        Me.txtMinimumPdaysWKOff.FieldName = Nothing
        Me.txtMinimumPdaysWKOff.isCalculatedField = False
        Me.txtMinimumPdaysWKOff.IsSourceFromTable = False
        Me.txtMinimumPdaysWKOff.IsSourceFromValueList = False
        Me.txtMinimumPdaysWKOff.IsUnique = False
        Me.txtMinimumPdaysWKOff.Location = New System.Drawing.Point(231, 231)
        Me.txtMinimumPdaysWKOff.MaxLength = 6
        Me.txtMinimumPdaysWKOff.MendatroryField = True
        Me.txtMinimumPdaysWKOff.MyLinkLable1 = Me.MyLabel20
        Me.txtMinimumPdaysWKOff.MyLinkLable2 = Nothing
        Me.txtMinimumPdaysWKOff.Name = "txtMinimumPdaysWKOff"
        Me.txtMinimumPdaysWKOff.ReferenceFieldDesc = Nothing
        Me.txtMinimumPdaysWKOff.ReferenceFieldName = Nothing
        Me.txtMinimumPdaysWKOff.ReferenceTableName = Nothing
        Me.txtMinimumPdaysWKOff.Size = New System.Drawing.Size(122, 20)
        Me.txtMinimumPdaysWKOff.TabIndex = 221
        Me.txtMinimumPdaysWKOff.Text = "0"
        Me.txtMinimumPdaysWKOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinimumPdaysWKOff.Value = 0.0R
        '
        'MyLabel20
        '
        Me.MyLabel20.FieldName = Nothing
        Me.MyLabel20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel20.Location = New System.Drawing.Point(17, 234)
        Me.MyLabel20.Name = "MyLabel20"
        Me.MyLabel20.Size = New System.Drawing.Size(190, 16)
        Me.MyLabel20.TabIndex = 222
        Me.MyLabel20.Text = "Minimum present days for weekly off"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(17, 103)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(150, 16)
        Me.MyLabel14.TabIndex = 210
        Me.MyLabel14.Text = "Maximum hours for overtime"
        '
        'txtMaximumHoursOT
        '
        Me.txtMaximumHoursOT.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMaximumHoursOT.CalculationExpression = Nothing
        Me.txtMaximumHoursOT.DecimalPlaces = 2
        Me.txtMaximumHoursOT.FieldCode = Nothing
        Me.txtMaximumHoursOT.FieldDesc = Nothing
        Me.txtMaximumHoursOT.FieldMaxLength = 0
        Me.txtMaximumHoursOT.FieldName = Nothing
        Me.txtMaximumHoursOT.isCalculatedField = False
        Me.txtMaximumHoursOT.IsSourceFromTable = False
        Me.txtMaximumHoursOT.IsSourceFromValueList = False
        Me.txtMaximumHoursOT.IsUnique = False
        Me.txtMaximumHoursOT.Location = New System.Drawing.Point(231, 101)
        Me.txtMaximumHoursOT.MaxLength = 6
        Me.txtMaximumHoursOT.MendatroryField = True
        Me.txtMaximumHoursOT.MyLinkLable1 = Me.MyLabel14
        Me.txtMaximumHoursOT.MyLinkLable2 = Nothing
        Me.txtMaximumHoursOT.Name = "txtMaximumHoursOT"
        Me.txtMaximumHoursOT.ReferenceFieldDesc = Nothing
        Me.txtMaximumHoursOT.ReferenceFieldName = Nothing
        Me.txtMaximumHoursOT.ReferenceTableName = Nothing
        Me.txtMaximumHoursOT.Size = New System.Drawing.Size(122, 20)
        Me.txtMaximumHoursOT.TabIndex = 209
        Me.txtMaximumHoursOT.Text = "0"
        Me.txtMaximumHoursOT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaximumHoursOT.Value = 0.0R
        '
        'txtWorkingHours
        '
        Me.txtWorkingHours.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtWorkingHours.CalculationExpression = Nothing
        Me.txtWorkingHours.DecimalPlaces = 2
        Me.txtWorkingHours.FieldCode = Nothing
        Me.txtWorkingHours.FieldDesc = Nothing
        Me.txtWorkingHours.FieldMaxLength = 0
        Me.txtWorkingHours.FieldName = Nothing
        Me.txtWorkingHours.isCalculatedField = False
        Me.txtWorkingHours.IsSourceFromTable = False
        Me.txtWorkingHours.IsSourceFromValueList = False
        Me.txtWorkingHours.IsUnique = False
        Me.txtWorkingHours.Location = New System.Drawing.Point(231, 205)
        Me.txtWorkingHours.MaxLength = 6
        Me.txtWorkingHours.MendatroryField = True
        Me.txtWorkingHours.MyLinkLable1 = Me.MyLabel19
        Me.txtWorkingHours.MyLinkLable2 = Nothing
        Me.txtWorkingHours.Name = "txtWorkingHours"
        Me.txtWorkingHours.ReferenceFieldDesc = Nothing
        Me.txtWorkingHours.ReferenceFieldName = Nothing
        Me.txtWorkingHours.ReferenceTableName = Nothing
        Me.txtWorkingHours.Size = New System.Drawing.Size(122, 20)
        Me.txtWorkingHours.TabIndex = 219
        Me.txtWorkingHours.Text = "0"
        Me.txtWorkingHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtWorkingHours.Value = 0.0R
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(17, 208)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(81, 16)
        Me.MyLabel19.TabIndex = 220
        Me.MyLabel19.Text = "Working Hours"
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(17, 129)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(185, 16)
        Me.MyLabel15.TabIndex = 213
        Me.MyLabel15.Text = "Maximum minutes for late comming"
        '
        'txtMaximumMintLateComming
        '
        Me.txtMaximumMintLateComming.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMaximumMintLateComming.CalculationExpression = Nothing
        Me.txtMaximumMintLateComming.DecimalPlaces = 2
        Me.txtMaximumMintLateComming.FieldCode = Nothing
        Me.txtMaximumMintLateComming.FieldDesc = Nothing
        Me.txtMaximumMintLateComming.FieldMaxLength = 0
        Me.txtMaximumMintLateComming.FieldName = Nothing
        Me.txtMaximumMintLateComming.isCalculatedField = False
        Me.txtMaximumMintLateComming.IsSourceFromTable = False
        Me.txtMaximumMintLateComming.IsSourceFromValueList = False
        Me.txtMaximumMintLateComming.IsUnique = False
        Me.txtMaximumMintLateComming.Location = New System.Drawing.Point(231, 127)
        Me.txtMaximumMintLateComming.MaxLength = 6
        Me.txtMaximumMintLateComming.MendatroryField = True
        Me.txtMaximumMintLateComming.MyLinkLable1 = Me.MyLabel15
        Me.txtMaximumMintLateComming.MyLinkLable2 = Nothing
        Me.txtMaximumMintLateComming.Name = "txtMaximumMintLateComming"
        Me.txtMaximumMintLateComming.ReferenceFieldDesc = Nothing
        Me.txtMaximumMintLateComming.ReferenceFieldName = Nothing
        Me.txtMaximumMintLateComming.ReferenceTableName = Nothing
        Me.txtMaximumMintLateComming.Size = New System.Drawing.Size(122, 20)
        Me.txtMaximumMintLateComming.TabIndex = 212
        Me.txtMaximumMintLateComming.Text = "0"
        Me.txtMaximumMintLateComming.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaximumMintLateComming.Value = 0.0R
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(17, 155)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(179, 16)
        Me.MyLabel16.TabIndex = 215
        Me.MyLabel16.Text = "No of late comming for short leave"
        '
        'txtNoSLForHFDay
        '
        Me.txtNoSLForHFDay.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNoSLForHFDay.CalculationExpression = Nothing
        Me.txtNoSLForHFDay.DecimalPlaces = 2
        Me.txtNoSLForHFDay.FieldCode = Nothing
        Me.txtNoSLForHFDay.FieldDesc = Nothing
        Me.txtNoSLForHFDay.FieldMaxLength = 0
        Me.txtNoSLForHFDay.FieldName = Nothing
        Me.txtNoSLForHFDay.isCalculatedField = False
        Me.txtNoSLForHFDay.IsSourceFromTable = False
        Me.txtNoSLForHFDay.IsSourceFromValueList = False
        Me.txtNoSLForHFDay.IsUnique = False
        Me.txtNoSLForHFDay.Location = New System.Drawing.Point(231, 179)
        Me.txtNoSLForHFDay.MaxLength = 6
        Me.txtNoSLForHFDay.MendatroryField = True
        Me.txtNoSLForHFDay.MyLinkLable1 = Me.MyLabel17
        Me.txtNoSLForHFDay.MyLinkLable2 = Nothing
        Me.txtNoSLForHFDay.Name = "txtNoSLForHFDay"
        Me.txtNoSLForHFDay.ReferenceFieldDesc = Nothing
        Me.txtNoSLForHFDay.ReferenceFieldName = Nothing
        Me.txtNoSLForHFDay.ReferenceTableName = Nothing
        Me.txtNoSLForHFDay.Size = New System.Drawing.Size(122, 20)
        Me.txtNoSLForHFDay.TabIndex = 216
        Me.txtNoSLForHFDay.Text = "0"
        Me.txtNoSLForHFDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoSLForHFDay.Value = 0.0R
        '
        'MyLabel17
        '
        Me.MyLabel17.FieldName = Nothing
        Me.MyLabel17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel17.Location = New System.Drawing.Point(17, 181)
        Me.MyLabel17.Name = "MyLabel17"
        Me.MyLabel17.Size = New System.Drawing.Size(151, 16)
        Me.MyLabel17.TabIndex = 217
        Me.MyLabel17.Text = "No of short leave for half day"
        '
        'txtNoLcommingSL
        '
        Me.txtNoLcommingSL.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNoLcommingSL.CalculationExpression = Nothing
        Me.txtNoLcommingSL.DecimalPlaces = 2
        Me.txtNoLcommingSL.FieldCode = Nothing
        Me.txtNoLcommingSL.FieldDesc = Nothing
        Me.txtNoLcommingSL.FieldMaxLength = 0
        Me.txtNoLcommingSL.FieldName = Nothing
        Me.txtNoLcommingSL.isCalculatedField = False
        Me.txtNoLcommingSL.IsSourceFromTable = False
        Me.txtNoLcommingSL.IsSourceFromValueList = False
        Me.txtNoLcommingSL.IsUnique = False
        Me.txtNoLcommingSL.Location = New System.Drawing.Point(231, 153)
        Me.txtNoLcommingSL.MaxLength = 6
        Me.txtNoLcommingSL.MendatroryField = True
        Me.txtNoLcommingSL.MyLinkLable1 = Me.MyLabel16
        Me.txtNoLcommingSL.MyLinkLable2 = Nothing
        Me.txtNoLcommingSL.Name = "txtNoLcommingSL"
        Me.txtNoLcommingSL.ReferenceFieldDesc = Nothing
        Me.txtNoLcommingSL.ReferenceFieldName = Nothing
        Me.txtNoLcommingSL.ReferenceTableName = Nothing
        Me.txtNoLcommingSL.Size = New System.Drawing.Size(122, 20)
        Me.txtNoLcommingSL.TabIndex = 214
        Me.txtNoLcommingSL.Text = "0"
        Me.txtNoLcommingSL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoLcommingSL.Value = 0.0R
        '
        'pageCTC
        '
        Me.pageCTC.Controls.Add(Me.RadGroupBox1)
        Me.pageCTC.ItemSize = New System.Drawing.SizeF(36.0!, 28.0!)
        Me.pageCTC.Location = New System.Drawing.Point(10, 37)
        Me.pageCTC.Name = "pageCTC"
        Me.pageCTC.Size = New System.Drawing.Size(852, 373)
        Me.pageCTC.Text = "CTC"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.gvCTC)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.HeaderText = "Select Pay Heads for CTC"
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(852, 373)
        Me.RadGroupBox1.TabIndex = 204
        Me.RadGroupBox1.Text = "Select Pay Heads for CTC"
        '
        'gvCTC
        '
        Me.gvCTC.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvCTC.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvCTC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvCTC.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvCTC.ForeColor = System.Drawing.Color.Black
        Me.gvCTC.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvCTC.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvCTC.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvCTC.MasterTemplate.AllowAddNewRow = False
        Me.gvCTC.MasterTemplate.AllowDeleteRow = False
        Me.gvCTC.MasterTemplate.AutoGenerateColumns = False
        Me.gvCTC.MasterTemplate.EnableGrouping = False
        Me.gvCTC.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvCTC.Name = "gvCTC"
        Me.gvCTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvCTC.ShowHeaderCellButtons = True
        Me.gvCTC.Size = New System.Drawing.Size(832, 343)
        Me.gvCTC.TabIndex = 5
        Me.gvCTC.Text = "RadGridView1"
        '
        'pageGross
        '
        Me.pageGross.Controls.Add(Me.RadGroupBox2)
        Me.pageGross.ItemSize = New System.Drawing.SizeF(44.0!, 28.0!)
        Me.pageGross.Location = New System.Drawing.Point(10, 37)
        Me.pageGross.Name = "pageGross"
        Me.pageGross.Size = New System.Drawing.Size(852, 373)
        Me.pageGross.Text = "Gross"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gvGross)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.HeaderText = "Select Pay Heads for Gross"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(852, 373)
        Me.RadGroupBox2.TabIndex = 205
        Me.RadGroupBox2.Text = "Select Pay Heads for Gross"
        '
        'gvGross
        '
        Me.gvGross.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvGross.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvGross.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvGross.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvGross.ForeColor = System.Drawing.Color.Black
        Me.gvGross.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvGross.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvGross.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvGross.MasterTemplate.AllowAddNewRow = False
        Me.gvGross.MasterTemplate.AllowDeleteRow = False
        Me.gvGross.MasterTemplate.AutoGenerateColumns = False
        Me.gvGross.MasterTemplate.EnableGrouping = False
        Me.gvGross.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvGross.Name = "gvGross"
        Me.gvGross.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvGross.ShowHeaderCellButtons = True
        Me.gvGross.Size = New System.Drawing.Size(832, 343)
        Me.gvGross.TabIndex = 5
        Me.gvGross.Text = "RadGridView1"
        '
        'pageInHand
        '
        Me.pageInHand.Controls.Add(Me.RadGroupBox3)
        Me.pageInHand.ItemSize = New System.Drawing.SizeF(56.0!, 28.0!)
        Me.pageInHand.Location = New System.Drawing.Point(10, 37)
        Me.pageInHand.Name = "pageInHand"
        Me.pageInHand.Size = New System.Drawing.Size(852, 373)
        Me.pageInHand.Text = "In Hand"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.gvInHand)
        Me.RadGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox3.HeaderText = "Select Pay Heads for In Hand"
        Me.RadGroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(852, 373)
        Me.RadGroupBox3.TabIndex = 205
        Me.RadGroupBox3.Text = "Select Pay Heads for In Hand"
        '
        'gvInHand
        '
        Me.gvInHand.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvInHand.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvInHand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvInHand.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gvInHand.ForeColor = System.Drawing.Color.Black
        Me.gvInHand.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvInHand.Location = New System.Drawing.Point(10, 20)
        '
        '
        '
        Me.gvInHand.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvInHand.MasterTemplate.AllowAddNewRow = False
        Me.gvInHand.MasterTemplate.AllowDeleteRow = False
        Me.gvInHand.MasterTemplate.AutoGenerateColumns = False
        Me.gvInHand.MasterTemplate.EnableGrouping = False
        Me.gvInHand.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvInHand.Name = "gvInHand"
        Me.gvInHand.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvInHand.ShowHeaderCellButtons = True
        Me.gvInHand.Size = New System.Drawing.Size(832, 343)
        Me.gvInHand.TabIndex = 5
        Me.gvInHand.Text = "RadGridView1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(434, 14)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(15, 20)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = " "
        '
        'rdbSetDefault
        '
        Me.rdbSetDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbSetDefault.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSetDefault.Location = New System.Drawing.Point(12, 13)
        Me.rdbSetDefault.Name = "rdbSetDefault"
        Me.rdbSetDefault.Size = New System.Drawing.Size(74, 18)
        Me.rdbSetDefault.TabIndex = 3
        Me.rdbSetDefault.Text = "Set Default"
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(171, 13)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 18)
        Me.btndelete.TabIndex = 1
        Me.btndelete.Text = "Delete"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(912, 13)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "Close"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(96, 13)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 18)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'RtimeFrom
        '
        Me.RtimeFrom.Location = New System.Drawing.Point(581, 181)
        Me.RtimeFrom.MaxValue = New Date(9999, 12, 31, 23, 59, 59, 0)
        Me.RtimeFrom.MinValue = New Date(CType(0, Long))
        Me.RtimeFrom.Name = "RtimeFrom"
        Me.RtimeFrom.Size = New System.Drawing.Size(122, 20)
        Me.RtimeFrom.TabIndex = 233
        Me.RtimeFrom.TabStop = False
        Me.RtimeFrom.Text = "RadTimePicker1"
        Me.RtimeFrom.Value = New Date(2018, 7, 27, 10, 52, 3, 714)
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(372, 184)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(117, 16)
        Me.MyLabel5.TabIndex = 234
        Me.MyLabel5.Text = "First Half (From Time)"
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(372, 210)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(104, 16)
        Me.MyLabel6.TabIndex = 236
        Me.MyLabel6.Text = "First Half (To Time)"
        '
        'RTimeTo
        '
        Me.RTimeTo.Location = New System.Drawing.Point(581, 207)
        Me.RTimeTo.MaxValue = New Date(9999, 12, 31, 23, 59, 59, 0)
        Me.RTimeTo.MinValue = New Date(CType(0, Long))
        Me.RTimeTo.Name = "RTimeTo"
        Me.RTimeTo.Size = New System.Drawing.Size(122, 20)
        Me.RTimeTo.TabIndex = 235
        Me.RTimeTo.TabStop = False
        Me.RTimeTo.Text = "RadTimePicker2"
        Me.RTimeTo.Value = New Date(2018, 7, 27, 10, 52, 3, 714)
        '
        'frmPayrollSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(986, 594)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmPayrollSetting"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Payroll Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblempcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        Me.pageGeneral.PerformLayout()
        CType(Me.txtGratuityPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGratuityPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStatutoryDLWorkingHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadCheckBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRemoveAttdList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox7.ResumeLayout(False)
        Me.RadGroupBox7.PerformLayout()
        CType(Me.rdbLAYearly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbLAMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStatutoryWK_WH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTreatWOffLeaveContinuous, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAutoAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEarlyArrivalMintOT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinHoursHFDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIntervalMinutes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinHoursOT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinimumPdaysWKOff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaximumHoursOT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWorkingHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaximumMintLateComming, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoSLForHFDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoLcommingSL, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageCTC.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.gvCTC.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCTC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageGross.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gvGross.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvGross, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageInHand.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        CType(Me.gvInHand.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvInHand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSetDefault, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RtimeFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RTimeTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel18 As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblempcode As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadCheckBox1 As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents fndLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblLocationDesc As common.Controls.MyLabel
    Friend WithEvents chkAutoAttendance As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadGroupBox7 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbLAYearly As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbLAMonthly As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtMinHoursHFDay As common.MyNumBox
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents txtMinHoursOT As common.MyNumBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents txtMaximumHoursOT As common.MyNumBox
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents chkTreatWOffLeaveContinuous As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtMaximumMintLateComming As common.MyNumBox
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents txtNoLcommingSL As common.MyNumBox
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents txtNoSLForHFDay As common.MyNumBox
    Friend WithEvents MyLabel17 As common.Controls.MyLabel
    Friend WithEvents chkRemoveAttdList As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents txtWorkingHours As common.MyNumBox
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtMinimumPdaysWKOff As common.MyNumBox
    Friend WithEvents MyLabel20 As common.Controls.MyLabel
    Friend WithEvents txtIntervalMinutes As common.MyNumBox
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents txtEarlyArrivalMintOT As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtStatutoryDLWorkingHours As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtStatutoryWK_WH As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents pageGeneral As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageCTC As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageGross As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents pageInHand As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvCTC As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvGross As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvInHand As common.UserControls.MyRadGridView
    Friend WithEvents rdbSetDefault As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtGratuityPeriod As common.MyNumBox
    Friend WithEvents lblGratuityPeriod As common.Controls.MyLabel
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents RTimeTo As Telerik.WinControls.UI.RadTimePicker
    Friend WithEvents RtimeFrom As Telerik.WinControls.UI.RadTimePicker
End Class
