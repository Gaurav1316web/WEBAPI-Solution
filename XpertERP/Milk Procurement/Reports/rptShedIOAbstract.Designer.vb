<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class rptShedIOAbstract
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.txtBMSNFDedRate = New common.MyNumBox()
        Me.txtCMSNFDedRate = New common.MyNumBox()
        Me.lblCMSNFDedRate = New common.Controls.MyLabel()
        Me.lbBMSNFDedRate = New common.Controls.MyLabel()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnMillBill = New common.Controls.MyRadioButton()
        Me.rbtnShedWiseMilkCost = New common.Controls.MyRadioButton()
        Me.rbtnUnitWise = New common.Controls.MyRadioButton()
        Me.rbtnShortageRecovery = New common.Controls.MyRadioButton()
        Me.rbtnShed = New common.Controls.MyRadioButton()
        Me.rbtnShortageExcel = New common.Controls.MyRadioButton()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblShed = New common.Controls.MyTextBox()
        Me.txtToDate = New common.Controls.MyDateTimePicker()
        Me.txtShed = New common.UserControls.txtFinder()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFromDate = New common.Controls.MyDateTimePicker()
        Me.lblPlant = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New common.UserControls.MyRadGridView()
        Me.RadPageViewPage3 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gvDetail = New common.UserControls.MyRadGridView()
        Me.btnExcel = New Telerik.WinControls.UI.RadSplitButton()
        Me.ExpExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.PDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.txtBMSNFDedRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCMSNFDedRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCMSNFDedRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbBMSNFDedRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnMillBill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnShedWiseMilkCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnUnitWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnShortageRecovery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnShed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnShortageExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage3.SuspendLayout()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(700, 20)
        Me.rdmenufile.TabIndex = 71
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnSaveLayout, Me.rmDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.AccessibleDescription = "Save Layout"
        Me.btnSaveLayout.AccessibleName = "Save Layout"
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnGo)
        Me.SplitContainer1.Size = New System.Drawing.Size(700, 508)
        Me.SplitContainer1.SplitterDistance = 469
        Me.SplitContainer1.TabIndex = 72
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage3)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(700, 469)
        Me.RadPageView1.TabIndex = 72
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.txtBMSNFDedRate)
        Me.RadPageViewPage1.Controls.Add(Me.txtCMSNFDedRate)
        Me.RadPageViewPage1.Controls.Add(Me.lblCMSNFDedRate)
        Me.RadPageViewPage1.Controls.Add(Me.lbBMSNFDedRate)
        Me.RadPageViewPage1.Controls.Add(Me.txtMCC)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel3)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.lblShed)
        Me.RadPageViewPage1.Controls.Add(Me.txtToDate)
        Me.RadPageViewPage1.Controls.Add(Me.txtShed)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtFromDate)
        Me.RadPageViewPage1.Controls.Add(Me.lblPlant)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(46.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(679, 421)
        Me.RadPageViewPage1.Text = "Filters"
        '
        'txtBMSNFDedRate
        '
        Me.txtBMSNFDedRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtBMSNFDedRate.CalculationExpression = Nothing
        Me.txtBMSNFDedRate.DecimalPlaces = 2
        Me.txtBMSNFDedRate.FieldCode = Nothing
        Me.txtBMSNFDedRate.FieldDesc = Nothing
        Me.txtBMSNFDedRate.FieldMaxLength = 0
        Me.txtBMSNFDedRate.FieldName = Nothing
        Me.txtBMSNFDedRate.isCalculatedField = False
        Me.txtBMSNFDedRate.IsSourceFromTable = False
        Me.txtBMSNFDedRate.IsSourceFromValueList = False
        Me.txtBMSNFDedRate.IsUnique = False
        Me.txtBMSNFDedRate.Location = New System.Drawing.Point(137, 134)
        Me.txtBMSNFDedRate.MendatroryField = False
        Me.txtBMSNFDedRate.MyLinkLable1 = Nothing
        Me.txtBMSNFDedRate.MyLinkLable2 = Nothing
        Me.txtBMSNFDedRate.Name = "txtBMSNFDedRate"
        Me.txtBMSNFDedRate.ReferenceFieldDesc = Nothing
        Me.txtBMSNFDedRate.ReferenceFieldName = Nothing
        Me.txtBMSNFDedRate.ReferenceTableName = Nothing
        Me.txtBMSNFDedRate.Size = New System.Drawing.Size(60, 20)
        Me.txtBMSNFDedRate.TabIndex = 1405
        Me.txtBMSNFDedRate.Text = "0"
        Me.txtBMSNFDedRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBMSNFDedRate.Value = 0R
        Me.txtBMSNFDedRate.Visible = False
        '
        'txtCMSNFDedRate
        '
        Me.txtCMSNFDedRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCMSNFDedRate.CalculationExpression = Nothing
        Me.txtCMSNFDedRate.DecimalPlaces = 2
        Me.txtCMSNFDedRate.FieldCode = Nothing
        Me.txtCMSNFDedRate.FieldDesc = Nothing
        Me.txtCMSNFDedRate.FieldMaxLength = 0
        Me.txtCMSNFDedRate.FieldName = Nothing
        Me.txtCMSNFDedRate.isCalculatedField = False
        Me.txtCMSNFDedRate.IsSourceFromTable = False
        Me.txtCMSNFDedRate.IsSourceFromValueList = False
        Me.txtCMSNFDedRate.IsUnique = False
        Me.txtCMSNFDedRate.Location = New System.Drawing.Point(329, 134)
        Me.txtCMSNFDedRate.MendatroryField = False
        Me.txtCMSNFDedRate.MyLinkLable1 = Nothing
        Me.txtCMSNFDedRate.MyLinkLable2 = Nothing
        Me.txtCMSNFDedRate.Name = "txtCMSNFDedRate"
        Me.txtCMSNFDedRate.ReferenceFieldDesc = Nothing
        Me.txtCMSNFDedRate.ReferenceFieldName = Nothing
        Me.txtCMSNFDedRate.ReferenceTableName = Nothing
        Me.txtCMSNFDedRate.Size = New System.Drawing.Size(60, 20)
        Me.txtCMSNFDedRate.TabIndex = 1404
        Me.txtCMSNFDedRate.Text = "0"
        Me.txtCMSNFDedRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCMSNFDedRate.Value = 0R
        Me.txtCMSNFDedRate.Visible = False
        '
        'lblCMSNFDedRate
        '
        Me.lblCMSNFDedRate.FieldName = Nothing
        Me.lblCMSNFDedRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCMSNFDedRate.Location = New System.Drawing.Point(198, 136)
        Me.lblCMSNFDedRate.Name = "lblCMSNFDedRate"
        Me.lblCMSNFDedRate.Size = New System.Drawing.Size(131, 16)
        Me.lblCMSNFDedRate.TabIndex = 1402
        Me.lblCMSNFDedRate.Text = "CM SNF Deduction Rate"
        Me.lblCMSNFDedRate.Visible = False
        '
        'lbBMSNFDedRate
        '
        Me.lbBMSNFDedRate.FieldName = Nothing
        Me.lbBMSNFDedRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbBMSNFDedRate.Location = New System.Drawing.Point(4, 136)
        Me.lbBMSNFDedRate.Name = "lbBMSNFDedRate"
        Me.lbBMSNFDedRate.Size = New System.Drawing.Size(130, 16)
        Me.lbBMSNFDedRate.TabIndex = 1403
        Me.lbBMSNFDedRate.Text = "BM SNF Deduction Rate"
        Me.lbBMSNFDedRate.Visible = False
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(43, 112)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel3
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "Please Select ..."
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(346, 19)
        Me.txtMCC.TabIndex = 1387
        Me.txtMCC.Visible = False
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(4, 112)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel3.TabIndex = 1386
        Me.MyLabel3.Text = "MCC"
        Me.MyLabel3.Visible = False
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnMillBill)
        Me.RadGroupBox1.Controls.Add(Me.rbtnShedWiseMilkCost)
        Me.RadGroupBox1.Controls.Add(Me.rbtnUnitWise)
        Me.RadGroupBox1.Controls.Add(Me.rbtnShortageRecovery)
        Me.RadGroupBox1.Controls.Add(Me.rbtnShed)
        Me.RadGroupBox1.Controls.Add(Me.rbtnShortageExcel)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(383, 56)
        Me.RadGroupBox1.TabIndex = 407
        '
        'rbtnMillBill
        '
        Me.rbtnMillBill.Location = New System.Drawing.Point(260, 29)
        Me.rbtnMillBill.MyLinkLable1 = Nothing
        Me.rbtnMillBill.MyLinkLable2 = Nothing
        Me.rbtnMillBill.Name = "rbtnMillBill"
        Me.rbtnMillBill.Size = New System.Drawing.Size(59, 18)
        Me.rbtnMillBill.TabIndex = 5
        Me.rbtnMillBill.TabStop = False
        Me.rbtnMillBill.Text = "Milk Bill"
        '
        'rbtnShedWiseMilkCost
        '
        Me.rbtnShedWiseMilkCost.Location = New System.Drawing.Point(135, 29)
        Me.rbtnShedWiseMilkCost.MyLinkLable1 = Nothing
        Me.rbtnShedWiseMilkCost.MyLinkLable2 = Nothing
        Me.rbtnShedWiseMilkCost.Name = "rbtnShedWiseMilkCost"
        Me.rbtnShedWiseMilkCost.Size = New System.Drawing.Size(122, 18)
        Me.rbtnShedWiseMilkCost.TabIndex = 4
        Me.rbtnShedWiseMilkCost.TabStop = False
        Me.rbtnShedWiseMilkCost.Text = "Shed Wise Milk Cost"
        '
        'rbtnUnitWise
        '
        Me.rbtnUnitWise.Location = New System.Drawing.Point(6, 29)
        Me.rbtnUnitWise.MyLinkLable1 = Nothing
        Me.rbtnUnitWise.MyLinkLable2 = Nothing
        Me.rbtnUnitWise.Name = "rbtnUnitWise"
        Me.rbtnUnitWise.Size = New System.Drawing.Size(113, 18)
        Me.rbtnUnitWise.TabIndex = 3
        Me.rbtnUnitWise.TabStop = False
        Me.rbtnUnitWise.Text = "Unit Wise Abstract"
        '
        'rbtnShortageRecovery
        '
        Me.rbtnShortageRecovery.Location = New System.Drawing.Point(260, 6)
        Me.rbtnShortageRecovery.MyLinkLable1 = Nothing
        Me.rbtnShortageRecovery.MyLinkLable2 = Nothing
        Me.rbtnShortageRecovery.Name = "rbtnShortageRecovery"
        Me.rbtnShortageRecovery.Size = New System.Drawing.Size(114, 18)
        Me.rbtnShortageRecovery.TabIndex = 2
        Me.rbtnShortageRecovery.TabStop = False
        Me.rbtnShortageRecovery.Text = "Shortage Recovery"
        '
        'rbtnShed
        '
        Me.rbtnShed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnShed.Location = New System.Drawing.Point(6, 6)
        Me.rbtnShed.MyLinkLable1 = Nothing
        Me.rbtnShed.MyLinkLable2 = Nothing
        Me.rbtnShed.Name = "rbtnShed"
        Me.rbtnShed.Size = New System.Drawing.Size(123, 18)
        Me.rbtnShed.TabIndex = 0
        Me.rbtnShed.Text = "Shed Wise Summary"
        Me.rbtnShed.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'rbtnShortageExcel
        '
        Me.rbtnShortageExcel.Location = New System.Drawing.Point(135, 6)
        Me.rbtnShortageExcel.MyLinkLable1 = Nothing
        Me.rbtnShortageExcel.MyLinkLable2 = Nothing
        Me.rbtnShortageExcel.Name = "rbtnShortageExcel"
        Me.rbtnShortageExcel.Size = New System.Drawing.Size(93, 18)
        Me.rbtnShortageExcel.TabIndex = 1
        Me.rbtnShortageExcel.TabStop = False
        Me.rbtnShortageExcel.Text = "Shortage Excel"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(148, 89)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(19, 16)
        Me.MyLabel2.TabIndex = 266
        Me.MyLabel2.Text = "To"
        '
        'lblShed
        '
        Me.lblShed.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblShed.CalculationExpression = Nothing
        Me.lblShed.Enabled = False
        Me.lblShed.FieldCode = Nothing
        Me.lblShed.FieldDesc = Nothing
        Me.lblShed.FieldMaxLength = 0
        Me.lblShed.FieldName = Nothing
        Me.lblShed.isCalculatedField = False
        Me.lblShed.IsSourceFromTable = False
        Me.lblShed.IsSourceFromValueList = False
        Me.lblShed.IsUnique = False
        Me.lblShed.Location = New System.Drawing.Point(148, 63)
        Me.lblShed.MendatroryField = False
        Me.lblShed.MyLinkLable1 = Nothing
        Me.lblShed.MyLinkLable2 = Nothing
        Me.lblShed.Name = "lblShed"
        Me.lblShed.ReferenceFieldDesc = Nothing
        Me.lblShed.ReferenceFieldName = Nothing
        Me.lblShed.ReferenceTableName = Nothing
        Me.lblShed.Size = New System.Drawing.Size(241, 20)
        Me.lblShed.TabIndex = 406
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
        Me.txtToDate.Location = New System.Drawing.Point(173, 87)
        Me.txtToDate.MendatroryField = False
        Me.txtToDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.MyLinkLable1 = Me.MyLabel2
        Me.txtToDate.MyLinkLable2 = Nothing
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.ReadOnly = True
        Me.txtToDate.ReferenceFieldDesc = Nothing
        Me.txtToDate.ReferenceFieldName = Nothing
        Me.txtToDate.ReferenceTableName = Nothing
        Me.txtToDate.Size = New System.Drawing.Size(98, 20)
        Me.txtToDate.TabIndex = 265
        Me.txtToDate.TabStop = False
        Me.txtToDate.Text = "10/06/2011"
        Me.txtToDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'txtShed
        '
        Me.txtShed.CalculationExpression = Nothing
        Me.txtShed.FieldCode = Nothing
        Me.txtShed.FieldDesc = Nothing
        Me.txtShed.FieldMaxLength = 0
        Me.txtShed.FieldName = Nothing
        Me.txtShed.isCalculatedField = False
        Me.txtShed.IsSourceFromTable = False
        Me.txtShed.IsSourceFromValueList = False
        Me.txtShed.IsUnique = False
        Me.txtShed.Location = New System.Drawing.Point(43, 64)
        Me.txtShed.MendatroryField = True
        Me.txtShed.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShed.MyLinkLable1 = Nothing
        Me.txtShed.MyLinkLable2 = Nothing
        Me.txtShed.MyReadOnly = False
        Me.txtShed.MyShowMasterFormButton = False
        Me.txtShed.Name = "txtShed"
        Me.txtShed.ReferenceFieldDesc = Nothing
        Me.txtShed.ReferenceFieldName = Nothing
        Me.txtShed.ReferenceTableName = Nothing
        Me.txtShed.Size = New System.Drawing.Size(98, 20)
        Me.txtShed.TabIndex = 405
        Me.txtShed.Value = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(4, 89)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(33, 16)
        Me.MyLabel1.TabIndex = 264
        Me.MyLabel1.Text = "From"
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
        Me.txtFromDate.Location = New System.Drawing.Point(43, 87)
        Me.txtFromDate.MendatroryField = False
        Me.txtFromDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.MyLinkLable1 = Me.MyLabel1
        Me.txtFromDate.MyLinkLable2 = Nothing
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.ReferenceFieldDesc = Nothing
        Me.txtFromDate.ReferenceFieldName = Nothing
        Me.txtFromDate.ReferenceTableName = Nothing
        Me.txtFromDate.Size = New System.Drawing.Size(98, 20)
        Me.txtFromDate.TabIndex = 263
        Me.txtFromDate.TabStop = False
        Me.txtFromDate.Text = "10/06/2011"
        Me.txtFromDate.Value = New Date(2011, 6, 10, 11, 51, 56, 953)
        '
        'lblPlant
        '
        Me.lblPlant.FieldName = Nothing
        Me.lblPlant.Location = New System.Drawing.Point(4, 65)
        Me.lblPlant.Name = "lblPlant"
        Me.lblPlant.Size = New System.Drawing.Size(31, 18)
        Me.lblPlant.TabIndex = 403
        Me.lblPlant.Text = "Shed"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.Gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(679, 421)
        Me.RadPageViewPage2.Text = "Report"
        '
        'Gv1
        '
        Me.Gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Gv1.ForeColor = System.Drawing.Color.Black
        Me.Gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.Gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.Gv1.MasterTemplate.AllowAddNewRow = False
        Me.Gv1.MasterTemplate.AllowDeleteRow = False
        Me.Gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.Gv1.Name = "Gv1"
        Me.Gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Gv1.ShowHeaderCellButtons = True
        Me.Gv1.Size = New System.Drawing.Size(679, 421)
        Me.Gv1.TabIndex = 0
        Me.Gv1.Text = "RadGridView1"
        '
        'RadPageViewPage3
        '
        Me.RadPageViewPage3.Controls.Add(Me.gvDetail)
        Me.RadPageViewPage3.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage3.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage3.Name = "RadPageViewPage3"
        Me.RadPageViewPage3.Size = New System.Drawing.Size(679, 421)
        Me.RadPageViewPage3.Text = "Details"
        '
        'gvDetail
        '
        Me.gvDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gvDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.gvDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvDetail.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gvDetail.ForeColor = System.Drawing.Color.Black
        Me.gvDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gvDetail.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gvDetail.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gvDetail.MasterTemplate.AllowAddNewRow = False
        Me.gvDetail.MasterTemplate.AllowDeleteRow = False
        Me.gvDetail.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvDetail.Name = "gvDetail"
        Me.gvDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gvDetail.ShowHeaderCellButtons = True
        Me.gvDetail.Size = New System.Drawing.Size(679, 421)
        Me.gvDetail.TabIndex = 1
        Me.gvDetail.Text = "RadGridView1"
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Items.AddRange(New Telerik.WinControls.RadItem() {Me.ExpExcel, Me.PDF, Me.RadMenuItem1})
        Me.btnExcel.Location = New System.Drawing.Point(124, 7)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(80, 20)
        Me.btnExcel.TabIndex = 333
        Me.btnExcel.Text = "Export"
        '
        'ExpExcel
        '
        Me.ExpExcel.AccessibleDescription = "Excel"
        Me.ExpExcel.AccessibleName = "Excel"
        Me.ExpExcel.Name = "ExpExcel"
        Me.ExpExcel.Text = "Excel"
        '
        'PDF
        '
        Me.PDF.AccessibleDescription = "PDF"
        Me.PDF.AccessibleName = "PDF"
        Me.PDF.Name = "PDF"
        Me.PDF.Text = "PDF"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Notepad"
        Me.RadMenuItem1.AccessibleName = "Notepad"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Notepad"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(638, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(57, 20)
        Me.btnClose.TabIndex = 331
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(60, 7)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(63, 20)
        Me.btnReset.TabIndex = 329
        Me.btnReset.Text = "Reset"
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(6, 7)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(53, 20)
        Me.btnGo.TabIndex = 330
        Me.btnGo.Text = ">>>"
        '
        'rptShedIOAbstract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 528)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "rptShedIOAbstract"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Shed IO Abstract "
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.txtBMSNFDedRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCMSNFDedRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCMSNFDedRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbBMSNFDedRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnMillBill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnShedWiseMilkCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnUnitWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnShortageRecovery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnShed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnShortageExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPlant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage3.ResumeLayout(False)
        CType(Me.gvDetail.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents Gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnGo As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents ExpExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents PDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageViewPage3 As RadPageViewPage
    Friend WithEvents gvDetail As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblShed As common.Controls.MyTextBox
    Friend WithEvents txtToDate As common.Controls.MyDateTimePicker
    Friend WithEvents txtShed As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFromDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblPlant As common.Controls.MyLabel
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbtnShed As common.Controls.MyRadioButton
    Friend WithEvents rbtnShortageExcel As common.Controls.MyRadioButton
    Friend WithEvents rbtnShortageRecovery As common.Controls.MyRadioButton
    Friend WithEvents rbtnUnitWise As common.Controls.MyRadioButton
    Friend WithEvents rbtnShedWiseMilkCost As common.Controls.MyRadioButton
    Friend WithEvents rbtnMillBill As common.Controls.MyRadioButton
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtBMSNFDedRate As common.MyNumBox
    Friend WithEvents txtCMSNFDedRate As common.MyNumBox
    Friend WithEvents lblCMSNFDedRate As common.Controls.MyLabel
    Friend WithEvents lbBMSNFDedRate As common.Controls.MyLabel
End Class

