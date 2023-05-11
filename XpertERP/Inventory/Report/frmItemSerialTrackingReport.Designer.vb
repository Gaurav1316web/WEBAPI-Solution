<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmItemSerialTrackingReport
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.rcbShowSerialNo = New Telerik.WinControls.UI.RadCheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbDetail = New Telerik.WinControls.UI.RadRadioButton()
        Me.rdbSummary = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgSerialNo = New common.MyCheckBoxGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkSerialselect = New common.Controls.MyRadioButton()
        Me.chkSerialAll = New common.Controls.MyRadioButton()
        Me.lblModeofTransport = New common.Controls.MyLabel()
        Me.cboType = New common.Controls.MyComboBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgLocation = New common.MyCheckBoxGrid()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rbtnLocationSelect = New common.Controls.MyRadioButton()
        Me.rbtnLocationAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgItem = New common.MyCheckBoxGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbtnItemSelet = New common.Controls.MyRadioButton()
        Me.rbtnItemAll = New common.Controls.MyRadioButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvSerial = New common.UserControls.MyRadGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.rcbShowSerialNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.chkSerialselect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSerialAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.rbtnItemSelet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnItemAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvSerial.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(887, 589)
        Me.SplitContainer1.SplitterDistance = 556
        Me.SplitContainer1.TabIndex = 25
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.DefaultPage = Me.RadPageViewPage1
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(887, 556)
        Me.RadPageView1.TabIndex = 23
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.rcbShowSerialNo)
        Me.RadPageViewPage1.Controls.Add(Me.GroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox4)
        Me.RadPageViewPage1.Controls.Add(Me.lblModeofTransport)
        Me.RadPageViewPage1.Controls.Add(Me.cboType)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(866, 508)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'rcbShowSerialNo
        '
        Me.rcbShowSerialNo.Location = New System.Drawing.Point(725, 4)
        Me.rcbShowSerialNo.Name = "rcbShowSerialNo"
        Me.rcbShowSerialNo.Size = New System.Drawing.Size(132, 18)
        Me.rcbShowSerialNo.TabIndex = 113
        Me.rcbShowSerialNo.Text = "Show Closing's Details"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbDetail)
        Me.GroupBox1.Controls.Add(Me.rdbSummary)
        Me.GroupBox1.Location = New System.Drawing.Point(588, -5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(132, 32)
        Me.GroupBox1.TabIndex = 112
        Me.GroupBox1.TabStop = False
        '
        'rdbDetail
        '
        Me.rdbDetail.Location = New System.Drawing.Point(77, 9)
        Me.rdbDetail.Name = "rdbDetail"
        Me.rdbDetail.Size = New System.Drawing.Size(49, 18)
        Me.rdbDetail.TabIndex = 1
        Me.rdbDetail.Text = "Detail"
        '
        'rdbSummary
        '
        Me.rdbSummary.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbSummary.Location = New System.Drawing.Point(5, 9)
        Me.rdbSummary.Name = "rdbSummary"
        Me.rdbSummary.Size = New System.Drawing.Size(67, 18)
        Me.rdbSummary.TabIndex = 0
        Me.rdbSummary.Text = "Summary"
        Me.rdbSummary.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.cbgSerialNo)
        Me.RadGroupBox4.Controls.Add(Me.Panel5)
        Me.RadGroupBox4.HeaderText = "Serial No"
        Me.RadGroupBox4.Location = New System.Drawing.Point(435, 29)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(422, 235)
        Me.RadGroupBox4.TabIndex = 25
        Me.RadGroupBox4.Text = "Serial No"
        '
        'cbgSerialNo
        '
        Me.cbgSerialNo.CheckedValue = Nothing
        Me.cbgSerialNo.DataSource = Nothing
        Me.cbgSerialNo.DisplayMember = "Name"
        Me.cbgSerialNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgSerialNo.Location = New System.Drawing.Point(10, 40)
        Me.cbgSerialNo.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgSerialNo.MyShowHeadrText = False
        Me.cbgSerialNo.Name = "cbgSerialNo"
        Me.cbgSerialNo.Size = New System.Drawing.Size(402, 185)
        Me.cbgSerialNo.TabIndex = 0
        Me.cbgSerialNo.ValueMember = "Code"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkSerialselect)
        Me.Panel5.Controls.Add(Me.chkSerialAll)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(10, 20)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(402, 20)
        Me.Panel5.TabIndex = 1
        '
        'chkSerialselect
        '
        Me.chkSerialselect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkSerialselect.Location = New System.Drawing.Point(198, 1)
        Me.chkSerialselect.MyLinkLable1 = Nothing
        Me.chkSerialselect.MyLinkLable2 = Nothing
        Me.chkSerialselect.Name = "chkSerialselect"
        Me.chkSerialselect.Size = New System.Drawing.Size(50, 18)
        Me.chkSerialselect.TabIndex = 1
        Me.chkSerialselect.Text = "Select"
        '
        'chkSerialAll
        '
        Me.chkSerialAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkSerialAll.Location = New System.Drawing.Point(147, 1)
        Me.chkSerialAll.MyLinkLable1 = Nothing
        Me.chkSerialAll.MyLinkLable2 = Nothing
        Me.chkSerialAll.Name = "chkSerialAll"
        Me.chkSerialAll.Size = New System.Drawing.Size(33, 18)
        Me.chkSerialAll.TabIndex = 0
        Me.chkSerialAll.Text = "All"
        '
        'lblModeofTransport
        '
        Me.lblModeofTransport.FieldName = Nothing
        Me.lblModeofTransport.Location = New System.Drawing.Point(308, 4)
        Me.lblModeofTransport.Name = "lblModeofTransport"
        Me.lblModeofTransport.Size = New System.Drawing.Size(37, 18)
        Me.lblModeofTransport.TabIndex = 23
        Me.lblModeofTransport.Text = "Status"
        '
        'cboType
        '
        Me.cboType.CalculationExpression = Nothing
        Me.cboType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboType.FieldCode = Nothing
        Me.cboType.FieldDesc = Nothing
        Me.cboType.FieldMaxLength = 0
        Me.cboType.FieldName = Nothing
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.isCalculatedField = False
        Me.cboType.IsSourceFromTable = False
        Me.cboType.IsSourceFromValueList = False
        Me.cboType.IsUnique = False
        RadListDataItem1.Text = "By Road"
        RadListDataItem2.Text = "By Air"
        RadListDataItem3.Text = "By Sea"
        Me.cboType.Items.Add(RadListDataItem1)
        Me.cboType.Items.Add(RadListDataItem2)
        Me.cboType.Items.Add(RadListDataItem3)
        Me.cboType.Location = New System.Drawing.Point(354, 4)
        Me.cboType.MendatroryField = False
        Me.cboType.MyLinkLable1 = Me.lblModeofTransport
        Me.cboType.MyLinkLable2 = Nothing
        Me.cboType.Name = "cboType"
        Me.cboType.ReferenceFieldDesc = Nothing
        Me.cboType.ReferenceFieldName = Nothing
        Me.cboType.ReferenceTableName = Nothing
        Me.cboType.Size = New System.Drawing.Size(232, 18)
        Me.cboType.TabIndex = 22
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgLocation)
        Me.RadGroupBox2.Controls.Add(Me.Panel4)
        Me.RadGroupBox2.HeaderText = "Location"
        Me.RadGroupBox2.Location = New System.Drawing.Point(7, 268)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(422, 235)
        Me.RadGroupBox2.TabIndex = 5
        Me.RadGroupBox2.Text = "Location"
        '
        'cbgLocation
        '
        Me.cbgLocation.CheckedValue = Nothing
        Me.cbgLocation.DataSource = Nothing
        Me.cbgLocation.DisplayMember = "Name"
        Me.cbgLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgLocation.Location = New System.Drawing.Point(10, 40)
        Me.cbgLocation.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgLocation.MyShowHeadrText = False
        Me.cbgLocation.Name = "cbgLocation"
        Me.cbgLocation.Size = New System.Drawing.Size(402, 185)
        Me.cbgLocation.TabIndex = 0
        Me.cbgLocation.ValueMember = "Code"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rbtnLocationSelect)
        Me.Panel4.Controls.Add(Me.rbtnLocationAll)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(10, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(402, 20)
        Me.Panel4.TabIndex = 1
        '
        'rbtnLocationSelect
        '
        Me.rbtnLocationSelect.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationSelect.Location = New System.Drawing.Point(198, 1)
        Me.rbtnLocationSelect.MyLinkLable1 = Nothing
        Me.rbtnLocationSelect.MyLinkLable2 = Nothing
        Me.rbtnLocationSelect.Name = "rbtnLocationSelect"
        Me.rbtnLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.rbtnLocationSelect.TabIndex = 1
        Me.rbtnLocationSelect.Text = "Select"
        '
        'rbtnLocationAll
        '
        Me.rbtnLocationAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnLocationAll.Location = New System.Drawing.Point(147, 1)
        Me.rbtnLocationAll.MyLinkLable1 = Nothing
        Me.rbtnLocationAll.MyLinkLable2 = Nothing
        Me.rbtnLocationAll.Name = "rbtnLocationAll"
        Me.rbtnLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnLocationAll.TabIndex = 0
        Me.rbtnLocationAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.cbgItem)
        Me.RadGroupBox1.Controls.Add(Me.Panel3)
        Me.RadGroupBox1.HeaderText = "Item"
        Me.RadGroupBox1.Location = New System.Drawing.Point(7, 29)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(422, 235)
        Me.RadGroupBox1.TabIndex = 4
        Me.RadGroupBox1.Text = "Item"
        '
        'cbgItem
        '
        Me.cbgItem.CheckedValue = Nothing
        Me.cbgItem.DataSource = Nothing
        Me.cbgItem.DisplayMember = "Name"
        Me.cbgItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgItem.Location = New System.Drawing.Point(10, 40)
        Me.cbgItem.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgItem.MyShowHeadrText = False
        Me.cbgItem.Name = "cbgItem"
        Me.cbgItem.Size = New System.Drawing.Size(402, 185)
        Me.cbgItem.TabIndex = 0
        Me.cbgItem.ValueMember = "Code"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbtnItemSelet)
        Me.Panel3.Controls.Add(Me.rbtnItemAll)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(402, 20)
        Me.Panel3.TabIndex = 1
        '
        'rbtnItemSelet
        '
        Me.rbtnItemSelet.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnItemSelet.Location = New System.Drawing.Point(198, 1)
        Me.rbtnItemSelet.MyLinkLable1 = Nothing
        Me.rbtnItemSelet.MyLinkLable2 = Nothing
        Me.rbtnItemSelet.Name = "rbtnItemSelet"
        Me.rbtnItemSelet.Size = New System.Drawing.Size(50, 18)
        Me.rbtnItemSelet.TabIndex = 1
        Me.rbtnItemSelet.Text = "Select"
        '
        'rbtnItemAll
        '
        Me.rbtnItemAll.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.rbtnItemAll.Location = New System.Drawing.Point(147, 1)
        Me.rbtnItemAll.MyLinkLable1 = Nothing
        Me.rbtnItemAll.MyLinkLable2 = Nothing
        Me.rbtnItemAll.Name = "rbtnItemAll"
        Me.rbtnItemAll.Size = New System.Drawing.Size(33, 18)
        Me.rbtnItemAll.TabIndex = 0
        Me.rbtnItemAll.Text = "All"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(7, 4)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(59, 18)
        Me.MyLabel2.TabIndex = 2
        Me.MyLabel2.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(73, 4)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel2
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(83, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "28/06/2012"
        Me.txtFromDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(217, 4)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel1
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(83, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "28/06/2012"
        Me.txtToDate.Value = New Date(2012, 6, 28, 14, 31, 57, 31)
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(166, 4)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(45, 18)
        Me.MyLabel1.TabIndex = 0
        Me.MyLabel1.Text = "To Date"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(866, 508)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.ShowGroupedColumns = True
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(866, 508)
        Me.gv1.TabIndex = 3
        Me.gv1.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvSerial)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(90.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(866, 508)
        Me.RadPageViewPage3.Text = "Closing Details"
        '
        'gvSerial
        '
        Me.gvSerial.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvSerial.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvSerial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvSerial.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvSerial.ForeColor = System.Drawing.Color.Black
        Me.gvSerial.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvSerial.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvSerial.MasterTemplate.AllowAddNewRow = False
        Me.gvSerial.MasterTemplate.AllowDeleteRow = False
        Me.gvSerial.MasterTemplate.EnableFiltering = True
        Me.gvSerial.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvSerial.Name = "gvSerial"
        Me.gvSerial.ReadOnly = True
        Me.gvSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvSerial.ShowGroupPanel = False
        Me.gvSerial.ShowHeaderCellButtons = True
        Me.gvSerial.Size = New System.Drawing.Size(866, 508)
        Me.gvSerial.TabIndex = 6
        Me.gvSerial.Text = "gv"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadButton3)
        Me.Panel1.Controls.Add(Me.btnExport)
        Me.Panel1.Controls.Add(Me.RadButton2)
        Me.Panel1.Controls.Add(Me.RadButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(887, 29)
        Me.Panel1.TabIndex = 24
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Location = New System.Drawing.Point(83, 2)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(95, 24)
        Me.RadButton3.TabIndex = 9
        Me.RadButton3.Text = "Reset"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF, Me.RadMenuItem1, Me.RadMenuItem2})
        Me.btnExport.Location = New System.Drawing.Point(178, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 24)
        Me.btnExport.TabIndex = 8
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.AccessibleDescription = "Excel"
        Me.btnExcel.AccessibleName = "Excel"
        Me.btnExcel.Image = Global.ERP.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.AccessibleDescription = "PDF"
        Me.btnPDF.AccessibleName = "PDF"
        Me.btnPDF.Image = Global.ERP.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Excel [Serial Details]"
        Me.RadMenuItem1.AccessibleName = "Excel [Serial Details]"
        Me.RadMenuItem1.Image = Global.ERP.My.Resources.Resources.MSE
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Excel [Closing Details]"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "PDF [Serial Details]"
        Me.RadMenuItem2.AccessibleName = "PDF [Serial Details]"
        Me.RadMenuItem2.Image = Global.ERP.My.Resources.Resources.pdf
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "PDF [Closing Details]"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(818, 2)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(67, 24)
        Me.RadButton2.TabIndex = 1
        Me.RadButton2.Text = "Close"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(5, 2)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(78, 24)
        Me.RadButton1.TabIndex = 1
        Me.RadButton1.Text = ">>>"
        '
        'FrmItemSerialTrackingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 589)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmItemSerialTrackingReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Item Serial Tracking Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.rcbShowSerialNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rdbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.chkSerialselect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSerialAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblModeofTransport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.rbtnLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.rbtnItemSelet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnItemAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvSerial.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvSerial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents cbgLocation As common.MyCheckBoxGrid
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblModeofTransport As common.Controls.MyLabel
    Protected WithEvents rbtnLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents rbtnLocationAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents cboType As common.Controls.MyComboBox
    Protected WithEvents rbtnItemSelet As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents cbgItem As common.MyCheckBoxGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents rbtnItemAll As common.Controls.MyRadioButton
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Protected WithEvents cbgSerialNo As common.MyCheckBoxGrid
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Protected WithEvents chkSerialselect As common.Controls.MyRadioButton
    Protected WithEvents chkSerialAll As common.Controls.MyRadioButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Protected WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Protected WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDetail As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rdbSummary As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rcbShowSerialNo As Telerik.WinControls.UI.RadCheckBox
    Friend WithEvents RadPageViewPage3 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyRadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents gvSerial As common.UserControls.MyRadGridView
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
End Class

