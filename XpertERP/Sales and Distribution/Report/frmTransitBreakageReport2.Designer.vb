<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTransitBreakageReport
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
        Me.lbldate = New common.Controls.MyLabel
        Me.RadLabel2 = New common.Controls.MyLabel
        Me.btnprint = New Telerik.WinControls.UI.RadButton
        Me.btncancel = New Telerik.WinControls.UI.RadButton
        Me.grpbxtransitReport = New System.Windows.Forms.GroupBox
        Me.grpCompany = New Telerik.WinControls.UI.RadGroupBox
        Me.gvDB = New common.UserControls.MyRadGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rbtnSelectCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnAllCompany = New Telerik.WinControls.UI.RadRadioButton
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgLocation = New common.MyCheckBoxGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkLocationSelect = New common.Controls.MyRadioButton
        Me.chkLocationAll = New common.Controls.MyRadioButton
        Me.groupbxItemVendor = New Telerik.WinControls.UI.RadGroupBox
        Me.cbgdoc = New common.MyCheckBoxGrid
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkDoc_select1 = New common.Controls.MyRadioButton
        Me.chkdocAll1 = New common.Controls.MyRadioButton
        Me.lblType = New common.Controls.MyLabel
        Me.drpboxType = New Telerik.WinControls.UI.RadDropDownList
        Me.rdobtndetails = New common.Controls.MyRadioButton
        Me.lblSrn_Number = New common.Controls.MyLabel
        Me.RadLabel10 = New common.Controls.MyLabel
        Me.RadLabel9 = New common.Controls.MyLabel
        Me.Totime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.rdobtnSummary = New common.Controls.MyRadioButton
        Me.Fromtime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.txtToSRNnumber = New common.UserControls.txtFinder
        Me.txtSRNnumber = New common.UserControls.txtFinder
        Me.lblToSRNnumber = New common.Controls.MyLabel
        Me.Todate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.fromdate = New Telerik.WinControls.UI.RadDateTimePicker
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.lblSRNNumber = New common.Controls.MyLabel
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox
        Me.RadLabel3 = New common.Controls.MyLabel
        Me.dtpendtime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel1 = New common.Controls.MyLabel
        Me.dtpStarttime = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel4 = New common.Controls.MyLabel
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton
        Me.RadLabel5 = New common.Controls.MyLabel
        Me.dtpend = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel7 = New common.Controls.MyLabel
        Me.dtpstart = New Telerik.WinControls.UI.RadDateTimePicker
        Me.RadLabel8 = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btncancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxtransitReport.SuspendLayout()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCompany.SuspendLayout()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupbxItemVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupbxItemVendor.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.chkDoc_select1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkdocAll1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.drpboxType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtndetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSrn_Number, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Totime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdobtnSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Fromtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToSRNnumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Todate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSRNNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lblSRNNumber.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpendtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpStarttime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbldate
        '
        Me.lbldate.Location = New System.Drawing.Point(17, 53)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(62, 18)
        Me.lbldate.TabIndex = 1
        Me.lbldate.Text = "From Date "
        '
        'RadLabel2
        '
        Me.RadLabel2.Location = New System.Drawing.Point(345, 55)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(45, 18)
        Me.RadLabel2.TabIndex = 56
        Me.RadLabel2.Text = "To Date"
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Location = New System.Drawing.Point(81, 7)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(69, 19)
        Me.btnprint.TabIndex = 59
        Me.btnprint.Text = "Print"
        '
        'btncancel
        '
        Me.btncancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncancel.Location = New System.Drawing.Point(565, 7)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(69, 19)
        Me.btncancel.TabIndex = 60
        Me.btncancel.Text = "Cancel"
        '
        'grpbxtransitReport
        '
        Me.grpbxtransitReport.Controls.Add(Me.grpCompany)
        Me.grpbxtransitReport.Controls.Add(Me.RadGroupBox1)
        Me.grpbxtransitReport.Controls.Add(Me.groupbxItemVendor)
        Me.grpbxtransitReport.Controls.Add(Me.lblType)
        Me.grpbxtransitReport.Controls.Add(Me.drpboxType)
        Me.grpbxtransitReport.Controls.Add(Me.rdobtndetails)
        Me.grpbxtransitReport.Controls.Add(Me.lblSrn_Number)
        Me.grpbxtransitReport.Controls.Add(Me.RadLabel10)
        Me.grpbxtransitReport.Controls.Add(Me.RadLabel9)
        Me.grpbxtransitReport.Controls.Add(Me.Totime)
        Me.grpbxtransitReport.Controls.Add(Me.rdobtnSummary)
        Me.grpbxtransitReport.Controls.Add(Me.Fromtime)
        Me.grpbxtransitReport.Controls.Add(Me.txtToSRNnumber)
        Me.grpbxtransitReport.Controls.Add(Me.txtSRNnumber)
        Me.grpbxtransitReport.Controls.Add(Me.lblToSRNnumber)
        Me.grpbxtransitReport.Controls.Add(Me.lbldate)
        Me.grpbxtransitReport.Controls.Add(Me.Todate)
        Me.grpbxtransitReport.Controls.Add(Me.RadLabel2)
        Me.grpbxtransitReport.Controls.Add(Me.fromdate)
        Me.grpbxtransitReport.ForeColor = System.Drawing.Color.Black
        Me.grpbxtransitReport.Location = New System.Drawing.Point(6, 12)
        Me.grpbxtransitReport.Name = "grpbxtransitReport"
        Me.grpbxtransitReport.Size = New System.Drawing.Size(628, 541)
        Me.grpbxtransitReport.TabIndex = 61
        Me.grpbxtransitReport.TabStop = False
        '
        'grpCompany
        '
        Me.grpCompany.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCompany.Controls.Add(Me.gvDB)
        Me.grpCompany.Controls.Add(Me.Panel2)
        Me.grpCompany.HeaderText = "Company"
        Me.grpCompany.Location = New System.Drawing.Point(19, 145)
        Me.grpCompany.Name = "grpCompany"
        Me.grpCompany.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCompany.Size = New System.Drawing.Size(244, 175)
        Me.grpCompany.TabIndex = 62
        Me.grpCompany.Text = "Company"
        '
        'gvDB
        '
        Me.gvDB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDB.Location = New System.Drawing.Point(10, 44)
        '
        'gvDB
        '
        Me.gvDB.MasterTemplate.AllowAddNewRow = False
        Me.gvDB.Name = "gvDB"
        Me.gvDB.ShowGroupPanel = False
        Me.gvDB.Size = New System.Drawing.Size(224, 121)
        Me.gvDB.TabIndex = 0
        Me.gvDB.Text = "RadGridView1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbtnSelectCompany)
        Me.Panel2.Controls.Add(Me.rbtnAllCompany)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(224, 24)
        Me.Panel2.TabIndex = 0
        '
        'rbtnSelectCompany
        '
        Me.rbtnSelectCompany.Location = New System.Drawing.Point(50, 4)
        Me.rbtnSelectCompany.Name = "rbtnSelectCompany"
        Me.rbtnSelectCompany.Size = New System.Drawing.Size(50, 18)
        Me.rbtnSelectCompany.TabIndex = 1
        Me.rbtnSelectCompany.Text = "Select"
        '
        'rbtnAllCompany
        '
        Me.rbtnAllCompany.Location = New System.Drawing.Point(7, 4)
        Me.rbtnAllCompany.Name = "rbtnAllCompany"
        Me.rbtnAllCompany.Size = New System.Drawing.Size(33, 18)
        Me.rbtnAllCompany.TabIndex = 0
        Me.rbtnAllCompany.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox1.Controls.Add(Me.Panel1)
        Me.RadGroupBox1.HeaderText = "Select Location"
        Me.RadGroupBox1.Location = New System.Drawing.Point(19, 328)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(603, 195)
        Me.RadGroupBox1.TabIndex = 68
        Me.RadGroupBox1.Text = "Select Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.AccessibleDescription = "cbgLocation"
        Me.cbgLocation.AccessibleName = ""
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 45)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(583, 140)
        Me.cbgLocation.TabIndex = 1
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkLocationSelect)
        Me.Panel1.Controls.Add(Me.chkLocationAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(583, 25)
        Me.Panel1.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.AccessibleDescription = "chkDoc_select"
        Me.chkLocationSelect.Location = New System.Drawing.Point(247, 3)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.AccessibleDescription = "chkdocAll"
        Me.chkLocationAll.Location = New System.Drawing.Point(158, 3)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'groupbxItemVendor
        '
        Me.groupbxItemVendor.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.groupbxItemVendor.Controls.Add(Me.cbgdoc)
        Me.groupbxItemVendor.Controls.Add(Me.Panel6)
        Me.groupbxItemVendor.HeaderText = ""
        Me.groupbxItemVendor.Location = New System.Drawing.Point(272, 147)
        Me.groupbxItemVendor.Name = "groupbxItemVendor"
        Me.groupbxItemVendor.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.groupbxItemVendor.Size = New System.Drawing.Size(350, 175)
        Me.groupbxItemVendor.TabIndex = 67
        '
        'cbgdoc
        '
        Me.cbgdoc.AccessibleDescription = "cbgdoc"
        Me.cbgdoc.AccessibleName = ""
        Me.cbgdoc.CheckedValue = Nothing
        Me.cbgdoc.DataSource = Nothing
        Me.cbgdoc.DisplayMember = "Name"
        Me.cbgdoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgdoc.Location = New System.Drawing.Point(10, 45)
        Me.cbgdoc.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgdoc.MyShowHeadrText = False
        Me.cbgdoc.Name = "cbgdoc"
        Me.cbgdoc.Size = New System.Drawing.Size(330, 120)
        Me.cbgdoc.TabIndex = 1
        Me.cbgdoc.ValueMember = "Code"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkDoc_select1)
        Me.Panel6.Controls.Add(Me.chkdocAll1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(10, 20)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(330, 25)
        Me.Panel6.TabIndex = 0
        '
        'chkDoc_select1
        '
        Me.chkDoc_select1.AccessibleDescription = "chkDoc_select"
        Me.chkDoc_select1.Location = New System.Drawing.Point(189, 3)
        Me.chkDoc_select1.MyLinkLable1 = Nothing
        Me.chkDoc_select1.MyLinkLable2 = Nothing
        Me.chkDoc_select1.Name = "chkDoc_select1"
        Me.chkDoc_select1.Size = New System.Drawing.Size(50, 18)
        Me.chkDoc_select1.TabIndex = 1
        Me.chkDoc_select1.Text = "Select"
        '
        'chkdocAll1
        '
        Me.chkdocAll1.AccessibleDescription = "chkdocAll"
        Me.chkdocAll1.Location = New System.Drawing.Point(124, 3)
        Me.chkdocAll1.MyLinkLable1 = Nothing
        Me.chkdocAll1.MyLinkLable2 = Nothing
        Me.chkdocAll1.Name = "chkdocAll1"
        Me.chkdocAll1.Size = New System.Drawing.Size(33, 18)
        Me.chkdocAll1.TabIndex = 0
        Me.chkdocAll1.Text = "All"
        '
        'lblType
        '
        Me.lblType.Location = New System.Drawing.Point(345, 23)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(30, 18)
        Me.lblType.TabIndex = 57
        Me.lblType.Text = "Type"
        '
        'drpboxType
        '
        Me.drpboxType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.drpboxType.Location = New System.Drawing.Point(446, 21)
        Me.drpboxType.Name = "drpboxType"
        Me.drpboxType.Size = New System.Drawing.Size(134, 20)
        Me.drpboxType.TabIndex = 66
        '
        'rdobtndetails
        '
        Me.rdobtndetails.Location = New System.Drawing.Point(132, 21)
        Me.rdobtndetails.MyLinkLable1 = Nothing
        Me.rdobtndetails.MyLinkLable2 = Nothing
        Me.rdobtndetails.Name = "rdobtndetails"
        Me.rdobtndetails.Size = New System.Drawing.Size(54, 18)
        Me.rdobtndetails.TabIndex = 65
        Me.rdobtndetails.Text = "Details"
        '
        'lblSrn_Number
        '
        Me.lblSrn_Number.Location = New System.Drawing.Point(17, 118)
        Me.lblSrn_Number.Name = "lblSrn_Number"
        Me.lblSrn_Number.Size = New System.Drawing.Size(101, 18)
        Me.lblSrn_Number.TabIndex = 4
        Me.lblSrn_Number.Text = "From SRN Number"
        '
        'RadLabel10
        '
        Me.RadLabel10.Location = New System.Drawing.Point(345, 85)
        Me.RadLabel10.Name = "RadLabel10"
        Me.RadLabel10.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel10.TabIndex = 26
        Me.RadLabel10.Text = "End Time"
        '
        'RadLabel9
        '
        Me.RadLabel9.Location = New System.Drawing.Point(17, 85)
        Me.RadLabel9.Name = "RadLabel9"
        Me.RadLabel9.Size = New System.Drawing.Size(57, 18)
        Me.RadLabel9.TabIndex = 25
        Me.RadLabel9.Text = "Start Time"
        '
        'Totime
        '
        Me.Totime.CustomFormat = "HH:mm tt"
        Me.Totime.Location = New System.Drawing.Point(446, 85)
        Me.Totime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Totime.Name = "Totime"
        Me.Totime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Totime.Size = New System.Drawing.Size(134, 20)
        Me.Totime.TabIndex = 28
        Me.Totime.TabStop = False
        Me.Totime.Text = "Wednesday, November 16, 2011"
        Me.Totime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'rdobtnSummary
        '
        Me.rdobtnSummary.Location = New System.Drawing.Point(21, 21)
        Me.rdobtnSummary.MyLinkLable1 = Nothing
        Me.rdobtnSummary.MyLinkLable2 = Nothing
        Me.rdobtnSummary.Name = "rdobtnSummary"
        Me.rdobtnSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdobtnSummary.TabIndex = 64
        Me.rdobtnSummary.TabStop = True
        Me.rdobtnSummary.Text = "Summary"
        Me.rdobtnSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'Fromtime
        '
        Me.Fromtime.CustomFormat = "HH:mm tt"
        Me.Fromtime.Location = New System.Drawing.Point(132, 85)
        Me.Fromtime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Fromtime.Name = "Fromtime"
        Me.Fromtime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Fromtime.Size = New System.Drawing.Size(134, 20)
        Me.Fromtime.TabIndex = 27
        Me.Fromtime.TabStop = False
        Me.Fromtime.Text = "Wednesday, November 16, 2011"
        Me.Fromtime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'txtToSRNnumber
        '
        Me.txtToSRNnumber.Location = New System.Drawing.Point(446, 117)
        Me.txtToSRNnumber.MendatroryField = True
        Me.txtToSRNnumber.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToSRNnumber.MyLinkLable1 = Nothing
        Me.txtToSRNnumber.MyLinkLable2 = Nothing
        Me.txtToSRNnumber.MyReadOnly = False
        Me.txtToSRNnumber.Name = "txtToSRNnumber"
        Me.txtToSRNnumber.Size = New System.Drawing.Size(134, 19)
        Me.txtToSRNnumber.TabIndex = 63
        Me.txtToSRNnumber.Value = ""
        '
        'txtSRNnumber
        '
        Me.txtSRNnumber.Location = New System.Drawing.Point(132, 116)
        Me.txtSRNnumber.MendatroryField = True
        Me.txtSRNnumber.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSRNnumber.MyLinkLable1 = Nothing
        Me.txtSRNnumber.MyLinkLable2 = Nothing
        Me.txtSRNnumber.MyReadOnly = False
        Me.txtSRNnumber.Name = "txtSRNnumber"
        Me.txtSRNnumber.Size = New System.Drawing.Size(134, 19)
        Me.txtSRNnumber.TabIndex = 5
        Me.txtSRNnumber.Value = ""
        '
        'lblToSRNnumber
        '
        Me.lblToSRNnumber.Location = New System.Drawing.Point(345, 117)
        Me.lblToSRNnumber.Name = "lblToSRNnumber"
        Me.lblToSRNnumber.Size = New System.Drawing.Size(87, 18)
        Me.lblToSRNnumber.TabIndex = 3
        Me.lblToSRNnumber.Text = "To SRN Number"
        '
        'Todate
        '
        Me.Todate.CustomFormat = "dd/MM/yyyy"
        Me.Todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Todate.Location = New System.Drawing.Point(446, 53)
        Me.Todate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Todate.Name = "Todate"
        Me.Todate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.Todate.Size = New System.Drawing.Size(134, 20)
        Me.Todate.TabIndex = 14
        Me.Todate.TabStop = False
        Me.Todate.Text = "03/12/2011"
        Me.Todate.Value = New Date(2011, 12, 3, 0, 0, 0, 0)
        '
        'fromdate
        '
        Me.fromdate.CustomFormat = "dd/MM/yyyy"
        Me.fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromdate.Location = New System.Drawing.Point(132, 53)
        Me.fromdate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromdate.Name = "fromdate"
        Me.fromdate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.fromdate.Size = New System.Drawing.Size(134, 20)
        Me.fromdate.TabIndex = 13
        Me.fromdate.TabStop = False
        Me.fromdate.Text = "03/12/2011"
        Me.fromdate.Value = New Date(2011, 12, 3, 0, 0, 0, 0)
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(6, 7)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(69, 19)
        Me.btnreset.TabIndex = 60
        Me.btnreset.Text = "Reset"
        '
        'lblSRNNumber
        '
        Me.lblSRNNumber.Controls.Add(Me.RadGroupBox5)
        Me.lblSRNNumber.Location = New System.Drawing.Point(1, 201)
        Me.lblSRNNumber.Name = "lblSRNNumber"
        Me.lblSRNNumber.Size = New System.Drawing.Size(101, 18)
        Me.lblSRNNumber.TabIndex = 2
        Me.lblSRNNumber.Text = "From SRN Number"
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.RadLabel3)
        Me.RadGroupBox5.Controls.Add(Me.dtpendtime)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel1)
        Me.RadGroupBox5.Controls.Add(Me.dtpStarttime)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel4)
        Me.RadGroupBox5.Controls.Add(Me.btnclose)
        Me.RadGroupBox5.Controls.Add(Me.RadButton1)
        Me.RadGroupBox5.Controls.Add(Me.RadButton2)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel5)
        Me.RadGroupBox5.Controls.Add(Me.dtpend)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel7)
        Me.RadGroupBox5.Controls.Add(Me.dtpstart)
        Me.RadGroupBox5.Controls.Add(Me.RadLabel8)
        Me.RadGroupBox5.HeaderText = ""
        Me.RadGroupBox5.Location = New System.Drawing.Point(-221, -55)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(542, 128)
        Me.RadGroupBox5.TabIndex = 3
        '
        'RadLabel3
        '
        Me.RadLabel3.BorderVisible = True
        Me.RadLabel3.Location = New System.Drawing.Point(118, 56)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(94, 18)
        Me.RadLabel3.TabIndex = 29
        Me.RadLabel3.Text = "Stock Adjustment"
        '
        'dtpendtime
        '
        Me.dtpendtime.CustomFormat = "HH:mm tt"
        Me.dtpendtime.Location = New System.Drawing.Point(364, 28)
        Me.dtpendtime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpendtime.Name = "dtpendtime"
        Me.dtpendtime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpendtime.Size = New System.Drawing.Size(134, 20)
        Me.dtpendtime.TabIndex = 28
        Me.dtpendtime.TabStop = False
        Me.dtpendtime.Text = "Wednesday, November 16, 2011"
        Me.dtpendtime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(10, 30)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(57, 18)
        Me.RadLabel1.TabIndex = 25
        Me.RadLabel1.Text = "Start Time"
        '
        'dtpStarttime
        '
        Me.dtpStarttime.CustomFormat = "HH:mm tt"
        Me.dtpStarttime.Location = New System.Drawing.Point(118, 30)
        Me.dtpStarttime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStarttime.Name = "dtpStarttime"
        Me.dtpStarttime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStarttime.Size = New System.Drawing.Size(134, 20)
        Me.dtpStarttime.TabIndex = 27
        Me.dtpStarttime.TabStop = False
        Me.dtpStarttime.Text = "Wednesday, November 16, 2011"
        Me.dtpStarttime.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(284, 30)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel4.TabIndex = 26
        Me.RadLabel4.Text = "End Time"
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(449, 96)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(68, 18)
        Me.btnclose.TabIndex = 24
        Me.btnclose.Text = "&Close"
        '
        'RadButton1
        '
        Me.RadButton1.Location = New System.Drawing.Point(95, 96)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(68, 18)
        Me.RadButton1.TabIndex = 23
        Me.RadButton1.Text = "&Reset"
        '
        'RadButton2
        '
        Me.RadButton2.Location = New System.Drawing.Point(20, 96)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(68, 18)
        Me.RadButton2.TabIndex = 22
        Me.RadButton2.Text = "&Print"
        '
        'RadLabel5
        '
        Me.RadLabel5.Location = New System.Drawing.Point(11, 54)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(94, 18)
        Me.RadLabel5.TabIndex = 20
        Me.RadLabel5.Text = "Adjustment  Type"
        '
        'dtpend
        '
        Me.dtpend.CustomFormat = "dd/MM/yyyy"
        Me.dtpend.Location = New System.Drawing.Point(364, 4)
        Me.dtpend.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Name = "dtpend"
        Me.dtpend.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpend.Size = New System.Drawing.Size(134, 20)
        Me.dtpend.TabIndex = 14
        Me.dtpend.TabStop = False
        Me.dtpend.Text = "Wednesday, November 16, 2011"
        Me.dtpend.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel7
        '
        Me.RadLabel7.Location = New System.Drawing.Point(10, 6)
        Me.RadLabel7.Name = "RadLabel7"
        Me.RadLabel7.Size = New System.Drawing.Size(56, 18)
        Me.RadLabel7.TabIndex = 11
        Me.RadLabel7.Text = "Start Date"
        '
        'dtpstart
        '
        Me.dtpstart.CustomFormat = "dd/MM/yyyy"
        Me.dtpstart.Location = New System.Drawing.Point(118, 6)
        Me.dtpstart.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpstart.Size = New System.Drawing.Size(134, 20)
        Me.dtpstart.TabIndex = 13
        Me.dtpstart.TabStop = False
        Me.dtpstart.Text = "Wednesday, November 16, 2011"
        Me.dtpstart.Value = New Date(2011, 11, 16, 11, 21, 56, 285)
        '
        'RadLabel8
        '
        Me.RadLabel8.Location = New System.Drawing.Point(284, 6)
        Me.RadLabel8.Name = "RadLabel8"
        Me.RadLabel8.Size = New System.Drawing.Size(52, 18)
        Me.RadLabel8.TabIndex = 12
        Me.RadLabel8.Text = "End Date"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpbxtransitReport)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnprint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btncancel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(654, 597)
        Me.SplitContainer1.SplitterDistance = 557
        Me.SplitContainer1.TabIndex = 62
        '
        'FrmTransitBreakageReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 597)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.lblSRNNumber)
        Me.Name = "FrmTransitBreakageReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Transit Breakage Report"
        Me.ThemeName = "ControlDefault"
        CType(Me.lbldate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnprint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btncancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxtransitReport.ResumeLayout(False)
        Me.grpbxtransitReport.PerformLayout()
        CType(Me.grpCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCompany.ResumeLayout(False)
        CType(Me.gvDB.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.rbtnSelectCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnAllCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupbxItemVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupbxItemVendor.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.chkDoc_select1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkdocAll1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.drpboxType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtndetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSrn_Number, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Totime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdobtnSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Fromtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToSRNnumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Todate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fromdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSRNNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lblSRNNumber.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpendtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpStarttime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpstart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnprint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btncancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents grpbxtransitReport As System.Windows.Forms.GroupBox
    Friend WithEvents txtSRNnumber As common.UserControls.txtFinder
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtToSRNnumber As common.UserControls.txtFinder
    Friend WithEvents rdobtnSummary As common.Controls.MyRadioButton
    Friend WithEvents rdobtndetails As common.Controls.MyRadioButton
    Friend WithEvents Totime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Fromtime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Todate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents fromdate As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents RadGroupBox5 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents dtpendtime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpStarttime As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents dtpend As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents dtpstart As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents drpboxType As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents groupbxItemVendor As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgdoc As common.MyCheckBoxGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkDoc_select1 As common.Controls.MyRadioButton
    Friend WithEvents chkdocAll1 As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents grpCompany As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gvDB As common.UserControls.MyRadGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbtnSelectCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnAllCompany As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents lbldate As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblSRNNumber As common.Controls.MyLabel
    Friend WithEvents lblToSRNnumber As common.Controls.MyLabel
    Friend WithEvents lblSrn_Number As common.Controls.MyLabel
    Friend WithEvents RadLabel10 As common.Controls.MyLabel
    Friend WithEvents RadLabel9 As common.Controls.MyLabel
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents RadLabel7 As common.Controls.MyLabel
    Friend WithEvents RadLabel8 As common.Controls.MyLabel
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

