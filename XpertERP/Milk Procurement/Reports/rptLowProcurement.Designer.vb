<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptLowProcurement
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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgMCC = New common.MyCheckBoxGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkMCCSelect = New common.Controls.MyRadioButton()
        Me.chkMCCAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgState = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkStateSelect = New common.Controls.MyRadioButton()
        Me.chkStateAll = New common.Controls.MyRadioButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.lblUnit = New common.Controls.MyLabel()
        Me.cboUnit = New common.Controls.MyComboBox()
        Me.txtToKg = New common.MyNumBox()
        Me.txtFromKg = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblMilkProcurementBetwwen = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblToDate = New common.Controls.MyLabel()
        Me.lblfromDate = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.BtnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkMCCSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMCCAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkStateAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkProcurementBetwwen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1043, 20)
        Me.RadMenu1.TabIndex = 5
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1043, 472)
        Me.SplitContainer1.SplitterDistance = 433
        Me.SplitContainer1.TabIndex = 6
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        '
        '
        '
        Me.RadPageView1.RootElement.AccessibleDescription = "Report1"
        Me.RadPageView1.RootElement.AccessibleName = "Report1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1043, 433)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.TabStop = False
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(2), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        CType(Me.RadPageView1.GetChildAt(0).GetChildAt(3), Telerik.WinControls.UI.RadPageViewLabelElement).Text = "Filter"
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1022, 385)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.cbgMCC)
        Me.RadGroupBox3.Controls.Add(Me.Panel1)
        Me.RadGroupBox3.HeaderText = "MCC"
        Me.RadGroupBox3.Location = New System.Drawing.Point(376, 82)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(358, 210)
        Me.RadGroupBox3.TabIndex = 2
        Me.RadGroupBox3.Text = "MCC"
        '
        'cbgMCC
        '
        Me.cbgMCC.CheckedValue = Nothing
        Me.cbgMCC.DataSource = Nothing
        Me.cbgMCC.DisplayMember = "Name"
        Me.cbgMCC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgMCC.Location = New System.Drawing.Point(10, 40)
        Me.cbgMCC.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgMCC.MyShowHeadrText = False
        Me.cbgMCC.Name = "cbgMCC"
        Me.cbgMCC.Size = New System.Drawing.Size(338, 160)
        Me.cbgMCC.TabIndex = 1
        Me.cbgMCC.TabStop = False
        Me.cbgMCC.ValueMember = "Code"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkMCCSelect)
        Me.Panel1.Controls.Add(Me.chkMCCAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(10, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(338, 20)
        Me.Panel1.TabIndex = 0
        '
        'chkMCCSelect
        '
        Me.chkMCCSelect.Location = New System.Drawing.Point(176, 1)
        Me.chkMCCSelect.MyLinkLable1 = Nothing
        Me.chkMCCSelect.MyLinkLable2 = Nothing
        Me.chkMCCSelect.Name = "chkMCCSelect"
        Me.chkMCCSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkMCCSelect.TabIndex = 1
        Me.chkMCCSelect.Text = "Select"
        '
        'chkMCCAll
        '
        Me.chkMCCAll.Location = New System.Drawing.Point(122, 1)
        Me.chkMCCAll.MyLinkLable1 = Nothing
        Me.chkMCCAll.MyLinkLable2 = Nothing
        Me.chkMCCAll.Name = "chkMCCAll"
        Me.chkMCCAll.Size = New System.Drawing.Size(33, 18)
        Me.chkMCCAll.TabIndex = 0
        Me.chkMCCAll.Text = "All"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.cbgState)
        Me.RadGroupBox2.Controls.Add(Me.Panel2)
        Me.RadGroupBox2.HeaderText = "State"
        Me.RadGroupBox2.Location = New System.Drawing.Point(11, 82)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(356, 210)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "State"
        '
        'cbgState
        '
        Me.cbgState.CheckedValue = Nothing
        Me.cbgState.DataSource = Nothing
        Me.cbgState.DisplayMember = "Name"
        Me.cbgState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgState.Location = New System.Drawing.Point(10, 40)
        Me.cbgState.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgState.MyShowHeadrText = False
        Me.cbgState.Name = "cbgState"
        Me.cbgState.Size = New System.Drawing.Size(336, 160)
        Me.cbgState.TabIndex = 1
        Me.cbgState.TabStop = False
        Me.cbgState.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkStateSelect)
        Me.Panel2.Controls.Add(Me.chkStateAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(336, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkStateSelect
        '
        Me.chkStateSelect.Location = New System.Drawing.Point(172, 1)
        Me.chkStateSelect.MyLinkLable1 = Nothing
        Me.chkStateSelect.MyLinkLable2 = Nothing
        Me.chkStateSelect.Name = "chkStateSelect"
        Me.chkStateSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkStateSelect.TabIndex = 1
        Me.chkStateSelect.Text = "Select"
        '
        'chkStateAll
        '
        Me.chkStateAll.Location = New System.Drawing.Point(114, 1)
        Me.chkStateAll.MyLinkLable1 = Nothing
        Me.chkStateAll.MyLinkLable2 = Nothing
        Me.chkStateAll.Name = "chkStateAll"
        Me.chkStateAll.Size = New System.Drawing.Size(33, 18)
        Me.chkStateAll.TabIndex = 0
        Me.chkStateAll.Text = "All"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblUnit)
        Me.RadGroupBox1.Controls.Add(Me.cboUnit)
        Me.RadGroupBox1.Controls.Add(Me.txtToKg)
        Me.RadGroupBox1.Controls.Add(Me.txtFromKg)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lblMilkProcurementBetwwen)
        Me.RadGroupBox1.Controls.Add(Me.txtFromDate)
        Me.RadGroupBox1.Controls.Add(Me.lblToDate)
        Me.RadGroupBox1.Controls.Add(Me.lblfromDate)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(11, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(723, 73)
        Me.RadGroupBox1.TabIndex = 0
        '
        'txtToDate
        '
        Me.txtToDate.CalculationExpression = Nothing
        Me.txtToDate.CustomFormat = "dd-MM-yyyy"
        Me.txtToDate.FieldCode = Nothing
        Me.txtToDate.FieldDesc = Nothing
        Me.txtToDate.FieldMaxLength = 0
        Me.txtToDate.FieldName = Nothing
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.isCalculatedField = False
        Me.txtToDate.IsSourceFromTable = False
        Me.txtToDate.IsSourceFromValueList = False
        Me.txtToDate.IsUnique = False
        Me.txtToDate.Location = New System.Drawing.Point(195, 12)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Nothing
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(82, 20)
        Me.txtToDate.TabIndex = 1
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "17-12-2011"
        Me.txtToDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblUnit
        '
        Me.lblUnit.FieldName = Nothing
        Me.lblUnit.Location = New System.Drawing.Point(400, 42)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(27, 18)
        Me.lblUnit.TabIndex = 6
        Me.lblUnit.Text = "Unit"
        '
        'cboUnit
        '
        Me.cboUnit.AutoCompleteDisplayMember = Nothing
        Me.cboUnit.AutoCompleteValueMember = Nothing
        Me.cboUnit.BackColor = System.Drawing.Color.Transparent
        Me.cboUnit.CalculationExpression = Nothing
        Me.cboUnit.DropDownAnimationEnabled = True
        Me.cboUnit.FieldCode = Nothing
        Me.cboUnit.FieldDesc = Nothing
        Me.cboUnit.FieldMaxLength = 0
        Me.cboUnit.FieldName = Nothing
        Me.cboUnit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUnit.isCalculatedField = False
        Me.cboUnit.IsSourceFromTable = False
        Me.cboUnit.IsSourceFromValueList = False
        Me.cboUnit.IsUnique = False
        RadListDataItem1.Text = "Kg"
        RadListDataItem2.Text = "ltr"
        Me.cboUnit.Items.Add(RadListDataItem1)
        Me.cboUnit.Items.Add(RadListDataItem2)
        Me.cboUnit.Location = New System.Drawing.Point(432, 42)
        Me.cboUnit.MendatroryField = True
        Me.cboUnit.MyLinkLable1 = Nothing
        Me.cboUnit.MyLinkLable2 = Nothing
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.ReferenceFieldDesc = Nothing
        Me.cboUnit.ReferenceFieldName = Nothing
        Me.cboUnit.ReferenceTableName = Nothing
        Me.cboUnit.Size = New System.Drawing.Size(98, 18)
        Me.cboUnit.TabIndex = 4
        '
        'txtToKg
        '
        Me.txtToKg.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtToKg.CalculationExpression = Nothing
        Me.txtToKg.DecimalPlaces = 2
        Me.txtToKg.FieldCode = Nothing
        Me.txtToKg.FieldDesc = Nothing
        Me.txtToKg.FieldMaxLength = 0
        Me.txtToKg.FieldName = Nothing
        Me.txtToKg.isCalculatedField = False
        Me.txtToKg.IsSourceFromTable = False
        Me.txtToKg.IsSourceFromValueList = False
        Me.txtToKg.IsUnique = False
        Me.txtToKg.Location = New System.Drawing.Point(312, 41)
        Me.txtToKg.MendatroryField = True
        Me.txtToKg.MyLinkLable1 = Nothing
        Me.txtToKg.MyLinkLable2 = Nothing
        Me.txtToKg.Name = "txtToKg"
        Me.txtToKg.ReferenceFieldDesc = Nothing
        Me.txtToKg.ReferenceFieldName = Nothing
        Me.txtToKg.ReferenceTableName = Nothing
        Me.txtToKg.Size = New System.Drawing.Size(82, 20)
        Me.txtToKg.TabIndex = 3
        Me.txtToKg.Text = "0"
        Me.txtToKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtToKg.Value = 0R
        '
        'txtFromKg
        '
        Me.txtFromKg.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFromKg.CalculationExpression = Nothing
        Me.txtFromKg.DecimalPlaces = 2
        Me.txtFromKg.FieldCode = Nothing
        Me.txtFromKg.FieldDesc = Nothing
        Me.txtFromKg.FieldMaxLength = 0
        Me.txtFromKg.FieldName = Nothing
        Me.txtFromKg.isCalculatedField = False
        Me.txtFromKg.IsSourceFromTable = False
        Me.txtFromKg.IsSourceFromValueList = False
        Me.txtFromKg.IsUnique = False
        Me.txtFromKg.Location = New System.Drawing.Point(195, 39)
        Me.txtFromKg.MendatroryField = True
        Me.txtFromKg.MyLinkLable1 = Nothing
        Me.txtFromKg.MyLinkLable2 = Nothing
        Me.txtFromKg.Name = "txtFromKg"
        Me.txtFromKg.ReferenceFieldDesc = Nothing
        Me.txtFromKg.ReferenceFieldName = Nothing
        Me.txtFromKg.ReferenceTableName = Nothing
        Me.txtFromKg.Size = New System.Drawing.Size(82, 20)
        Me.txtFromKg.TabIndex = 2
        Me.txtFromKg.Text = "0"
        Me.txtFromKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFromKg.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(284, 42)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(19, 18)
        Me.MyLabel2.TabIndex = 4
        Me.MyLabel2.Text = "To"
        '
        'lblMilkProcurementBetwwen
        '
        Me.lblMilkProcurementBetwwen.FieldName = Nothing
        Me.lblMilkProcurementBetwwen.Location = New System.Drawing.Point(9, 42)
        Me.lblMilkProcurementBetwwen.Name = "lblMilkProcurementBetwwen"
        Me.lblMilkProcurementBetwwen.Size = New System.Drawing.Size(184, 18)
        Me.lblMilkProcurementBetwwen.TabIndex = 4
        Me.lblMilkProcurementBetwwen.Text = "Milk Procurement Between(Weight)"
        '
        'txtFromDate
        '
        Me.txtFromDate.CalculationExpression = Nothing
        Me.txtFromDate.CustomFormat = "dd-MM-yyyy"
        Me.txtFromDate.FieldCode = Nothing
        Me.txtFromDate.FieldDesc = Nothing
        Me.txtFromDate.FieldMaxLength = 0
        Me.txtFromDate.FieldName = Nothing
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.isCalculatedField = False
        Me.txtFromDate.IsSourceFromTable = False
        Me.txtFromDate.IsSourceFromValueList = False
        Me.txtFromDate.IsUnique = False
        Me.txtFromDate.Location = New System.Drawing.Point(82, 11)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Nothing
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(82, 20)
        Me.txtFromDate.TabIndex = 0
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "17-12-2011"
        Me.txtFromDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.FieldName = Nothing
        Me.lblToDate.Location = New System.Drawing.Point(167, 13)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(22, 18)
        Me.lblToDate.TabIndex = 1
        Me.lblToDate.Text = "To "
        '
        'lblfromDate
        '
        Me.lblfromDate.FieldName = Nothing
        Me.lblfromDate.Location = New System.Drawing.Point(9, 12)
        Me.lblfromDate.Name = "lblfromDate"
        Me.lblfromDate.Size = New System.Drawing.Size(38, 18)
        Me.lblfromDate.TabIndex = 0
        Me.lblfromDate.Text = "Period"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1022, 385)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.AllowDeleteRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowGroupPanel = False
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(1022, 385)
        Me.gv.TabIndex = 3
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmExcel, Me.rmPDF})
        Me.RadSplitButton1.Location = New System.Drawing.Point(164, 10)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 21)
        Me.RadSplitButton1.TabIndex = 2
        Me.RadSplitButton1.Text = "Export"
        '
        'rmExcel
        '
        Me.rmExcel.Name = "rmExcel"
        Me.rmExcel.Text = "Excel"
        '
        'rmPDF
        '
        Me.rmPDF.Name = "rmPDF"
        Me.rmPDF.Text = "PDF"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo.Location = New System.Drawing.Point(10, 10)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(71, 22)
        Me.btnGo.TabIndex = 0
        Me.btnGo.Text = ">>>"
        '
        'BtnReset
        '
        Me.BtnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReset.Location = New System.Drawing.Point(87, 10)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(71, 22)
        Me.BtnReset.TabIndex = 1
        Me.BtnReset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(947, 10)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "Close"
        '
        'RptLowProcurement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1043, 492)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "RptLowProcurement"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "RptLowProcurement"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkMCCSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMCCAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkStateAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkProcurementBetwwen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblfromDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgMCC As common.MyCheckBoxGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkMCCSelect As common.Controls.MyRadioButton
    Friend WithEvents chkMCCAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgState As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkStateSelect As common.Controls.MyRadioButton
    Friend WithEvents chkStateAll As common.Controls.MyRadioButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblToDate As common.Controls.MyLabel
    Friend WithEvents lblfromDate As common.Controls.MyLabel
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblMilkProcurementBetwwen As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents txtToKg As common.MyNumBox
    Friend WithEvents txtFromKg As common.MyNumBox
    Friend WithEvents lblUnit As common.Controls.MyLabel
    Friend WithEvents cboUnit As common.Controls.MyComboBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents rmPDF As Telerik.WinControls.UI.RadMenuItem
End Class

