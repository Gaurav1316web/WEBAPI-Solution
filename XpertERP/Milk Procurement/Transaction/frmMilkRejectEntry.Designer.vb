<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkRejectEntry
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
        Me.components = New System.ComponentModel.Container()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.txtVehicle = New common.Controls.MyTextBox()
        Me.lblVehicle = New common.Controls.MyLabel()
        Me.LblMccName = New common.Controls.MyLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnReturnTypeCOB = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnReturnTypeDrain = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnReturnTypeReturn = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnReturnTypeNA = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtSNFPer = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtFatPer = New common.MyNumBox()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblNoOfCans = New common.Controls.MyLabel()
        Me.txtNoOfCans = New common.MyNumBox()
        Me.ChkALLVLC = New common.Controls.MyCheckBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.RadGroupBox4 = New Telerik.WinControls.UI.RadGroupBox()
        Me.TotalCansAllRoute = New common.MyNumBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.TxtTotalWeightallRoute = New common.MyNumBox()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblTotalWeight = New common.Controls.MyLabel()
        Me.txtTotalWeight = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.TxtTotalcans = New common.MyNumBox()
        Me.txtMilkWeight = New common.MyNumBox()
        Me.fndItem_Code = New common.Controls.MyLabel()
        Me.LblUom = New common.Controls.MyLabel()
        Me.fndVspCode = New common.Controls.MyLabel()
        Me.chkOther = New common.Controls.MyCheckBox()
        Me.cboRejectType = New common.Controls.MyComboBox()
        Me.lblMilkType = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.lblItemDesc = New common.Controls.MyLabel()
        Me.lblType = New common.Controls.MyLabel()
        Me.lblVehicleCode = New common.Controls.MyLabel()
        Me.fndVehicleCode = New common.Controls.MyLabel()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.fndRouteCode = New common.UserControls.txtFinder()
        Me.lblRouteDesc = New common.Controls.MyLabel()
        Me.lblVSPCode = New common.Controls.MyLabel()
        Me.lblVSPDesc = New common.Controls.MyLabel()
        Me.lblVLCCode = New common.Controls.MyLabel()
        Me.fndVLCCode = New common.UserControls.txtFinder()
        Me.lblVLCDesc = New common.Controls.MyLabel()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.dtpDocDate = New common.Controls.MyDateTimePicker()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.fndMCCCode = New common.UserControls.txtFinder()
        Me.lblCode = New common.Controls.MyLabel()
        Me.UsLock1 = New common.usLock()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdmenufile = New Telerik.WinControls.UI.RadMenu()
        Me.rdmenufile1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.BtnDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.txtVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMccName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnReturnTypeCOB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReturnTypeDrain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReturnTypeReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnReturnTypeNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNFPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFatPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkALLVLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox4.SuspendLayout()
        CType(Me.TotalCansAllRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalWeightallRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTotalcans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMilkWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndItem_Code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblUom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVspCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboRejectType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fndVehicleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVSPDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVLCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblVLCDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1005, 411)
        Me.SplitContainer1.SplitterDistance = 375
        Me.SplitContainer1.TabIndex = 42
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(1005, 375)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox2)
        Me.RadPageViewPage1.Controls.Add(Me.RadGroupBox1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(71.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(984, 327)
        Me.RadPageViewPage1.Text = "Milk Reject"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Rejected Milk Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 173)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(984, 154)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "Rejected Milk Details"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(10, 20)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AllowColumnHeaderContextMenu = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(964, 124)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.txtVehicle)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicle)
        Me.RadGroupBox1.Controls.Add(Me.LblMccName)
        Me.RadGroupBox1.Controls.Add(Me.GroupBox1)
        Me.RadGroupBox1.Controls.Add(Me.txtSNFPer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtFatPer)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel3)
        Me.RadGroupBox1.Controls.Add(Me.txtCode)
        Me.RadGroupBox1.Controls.Add(Me.lblNoOfCans)
        Me.RadGroupBox1.Controls.Add(Me.txtNoOfCans)
        Me.RadGroupBox1.Controls.Add(Me.ChkALLVLC)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox4)
        Me.RadGroupBox1.Controls.Add(Me.txtMilkWeight)
        Me.RadGroupBox1.Controls.Add(Me.fndItem_Code)
        Me.RadGroupBox1.Controls.Add(Me.LblUom)
        Me.RadGroupBox1.Controls.Add(Me.fndVspCode)
        Me.RadGroupBox1.Controls.Add(Me.chkOther)
        Me.RadGroupBox1.Controls.Add(Me.cboRejectType)
        Me.RadGroupBox1.Controls.Add(Me.lblMilkType)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.btnsave)
        Me.RadGroupBox1.Controls.Add(Me.lblItemDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblType)
        Me.RadGroupBox1.Controls.Add(Me.lblVehicleCode)
        Me.RadGroupBox1.Controls.Add(Me.fndVehicleCode)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.fndRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblVSPCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVSPDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblVLCCode)
        Me.RadGroupBox1.Controls.Add(Me.fndVLCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblVLCDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblBOMStatus)
        Me.RadGroupBox1.Controls.Add(Me.cboShift)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.dtpDocDate)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.fndMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.lblCode)
        Me.RadGroupBox1.Controls.Add(Me.UsLock1)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Add Rejected Milk "
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(984, 173)
        Me.RadGroupBox1.TabIndex = 2
        Me.RadGroupBox1.Text = "Add Rejected Milk "
        '
        'txtVehicle
        '
        Me.txtVehicle.CalculationExpression = Nothing
        Me.txtVehicle.FieldCode = Nothing
        Me.txtVehicle.FieldDesc = Nothing
        Me.txtVehicle.FieldMaxLength = 0
        Me.txtVehicle.FieldName = Nothing
        Me.txtVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.isCalculatedField = False
        Me.txtVehicle.IsSourceFromTable = False
        Me.txtVehicle.IsSourceFromValueList = False
        Me.txtVehicle.IsUnique = False
        Me.txtVehicle.Location = New System.Drawing.Point(846, 107)
        Me.txtVehicle.MaxLength = 200
        Me.txtVehicle.MendatroryField = False
        Me.txtVehicle.MyLinkLable1 = Nothing
        Me.txtVehicle.MyLinkLable2 = Nothing
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.ReadOnly = True
        Me.txtVehicle.ReferenceFieldDesc = Nothing
        Me.txtVehicle.ReferenceFieldName = Nothing
        Me.txtVehicle.ReferenceTableName = Nothing
        Me.txtVehicle.Size = New System.Drawing.Size(128, 18)
        Me.txtVehicle.TabIndex = 1446
        '
        'lblVehicle
        '
        Me.lblVehicle.FieldName = Nothing
        Me.lblVehicle.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblVehicle.Location = New System.Drawing.Point(797, 109)
        Me.lblVehicle.Name = "lblVehicle"
        Me.lblVehicle.Size = New System.Drawing.Size(43, 16)
        Me.lblVehicle.TabIndex = 1030
        Me.lblVehicle.Text = "Vehicle"
        '
        'LblMccName
        '
        Me.LblMccName.AutoSize = False
        Me.LblMccName.BorderVisible = True
        Me.LblMccName.FieldName = Nothing
        Me.LblMccName.Location = New System.Drawing.Point(197, 38)
        Me.LblMccName.Name = "LblMccName"
        Me.LblMccName.Size = New System.Drawing.Size(222, 20)
        Me.LblMccName.TabIndex = 1022
        Me.LblMccName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnReturnTypeCOB)
        Me.GroupBox1.Controls.Add(Me.rbtnReturnTypeDrain)
        Me.GroupBox1.Controls.Add(Me.rbtnReturnTypeReturn)
        Me.GroupBox1.Controls.Add(Me.rbtnReturnTypeNA)
        Me.GroupBox1.Location = New System.Drawing.Point(244, 54)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(177, 27)
        Me.GroupBox1.TabIndex = 1029
        Me.GroupBox1.TabStop = False
        '
        'rbtnReturnTypeCOB
        '
        Me.rbtnReturnTypeCOB.Location = New System.Drawing.Point(87, 7)
        Me.rbtnReturnTypeCOB.Name = "rbtnReturnTypeCOB"
        Me.rbtnReturnTypeCOB.Size = New System.Drawing.Size(42, 18)
        Me.rbtnReturnTypeCOB.TabIndex = 2
        Me.rbtnReturnTypeCOB.TabStop = False
        Me.rbtnReturnTypeCOB.Text = "COB"
        '
        'rbtnReturnTypeDrain
        '
        Me.rbtnReturnTypeDrain.Location = New System.Drawing.Point(128, 7)
        Me.rbtnReturnTypeDrain.Name = "rbtnReturnTypeDrain"
        Me.rbtnReturnTypeDrain.Size = New System.Drawing.Size(47, 18)
        Me.rbtnReturnTypeDrain.TabIndex = 2
        Me.rbtnReturnTypeDrain.TabStop = False
        Me.rbtnReturnTypeDrain.Text = "Drain"
        '
        'rbtnReturnTypeReturn
        '
        Me.rbtnReturnTypeReturn.Location = New System.Drawing.Point(34, 7)
        Me.rbtnReturnTypeReturn.Name = "rbtnReturnTypeReturn"
        Me.rbtnReturnTypeReturn.Size = New System.Drawing.Size(53, 18)
        Me.rbtnReturnTypeReturn.TabIndex = 1
        Me.rbtnReturnTypeReturn.TabStop = False
        Me.rbtnReturnTypeReturn.Text = "Return"
        '
        'rbtnReturnTypeNA
        '
        Me.rbtnReturnTypeNA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnReturnTypeNA.Location = New System.Drawing.Point(2, 7)
        Me.rbtnReturnTypeNA.Name = "rbtnReturnTypeNA"
        Me.rbtnReturnTypeNA.Size = New System.Drawing.Size(36, 18)
        Me.rbtnReturnTypeNA.TabIndex = 0
        Me.rbtnReturnTypeNA.Text = "NA"
        Me.rbtnReturnTypeNA.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtSNFPer
        '
        Me.txtSNFPer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtSNFPer.CalculationExpression = Nothing
        Me.txtSNFPer.DecimalPlaces = 2
        Me.txtSNFPer.FieldCode = Nothing
        Me.txtSNFPer.FieldDesc = Nothing
        Me.txtSNFPer.FieldMaxLength = 0
        Me.txtSNFPer.FieldName = Nothing
        Me.txtSNFPer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtSNFPer.isCalculatedField = False
        Me.txtSNFPer.IsSourceFromTable = False
        Me.txtSNFPer.IsSourceFromValueList = False
        Me.txtSNFPer.IsUnique = False
        Me.txtSNFPer.Location = New System.Drawing.Point(359, 107)
        Me.txtSNFPer.MaxLength = 5
        Me.txtSNFPer.MendatroryField = True
        Me.txtSNFPer.MyLinkLable1 = Me.MyLabel4
        Me.txtSNFPer.MyLinkLable2 = Nothing
        Me.txtSNFPer.Name = "txtSNFPer"
        Me.txtSNFPer.ReferenceFieldDesc = Nothing
        Me.txtSNFPer.ReferenceFieldName = Nothing
        Me.txtSNFPer.ReferenceTableName = Nothing
        Me.txtSNFPer.Size = New System.Drawing.Size(60, 21)
        Me.txtSNFPer.TabIndex = 1026
        Me.txtSNFPer.Text = "0"
        Me.txtSNFPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNFPer.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(311, 109)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(42, 16)
        Me.MyLabel4.TabIndex = 1027
        Me.MyLabel4.Text = "SNF %"
        '
        'txtFatPer
        '
        Me.txtFatPer.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFatPer.CalculationExpression = Nothing
        Me.txtFatPer.DecimalPlaces = 2
        Me.txtFatPer.FieldCode = Nothing
        Me.txtFatPer.FieldDesc = Nothing
        Me.txtFatPer.FieldMaxLength = 0
        Me.txtFatPer.FieldName = Nothing
        Me.txtFatPer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtFatPer.isCalculatedField = False
        Me.txtFatPer.IsSourceFromTable = False
        Me.txtFatPer.IsSourceFromValueList = False
        Me.txtFatPer.IsUnique = False
        Me.txtFatPer.Location = New System.Drawing.Point(246, 107)
        Me.txtFatPer.MaxLength = 5
        Me.txtFatPer.MendatroryField = True
        Me.txtFatPer.MyLinkLable1 = Me.MyLabel3
        Me.txtFatPer.MyLinkLable2 = Nothing
        Me.txtFatPer.Name = "txtFatPer"
        Me.txtFatPer.ReferenceFieldDesc = Nothing
        Me.txtFatPer.ReferenceFieldName = Nothing
        Me.txtFatPer.ReferenceTableName = Nothing
        Me.txtFatPer.Size = New System.Drawing.Size(60, 21)
        Me.txtFatPer.TabIndex = 3
        Me.txtFatPer.Text = "0"
        Me.txtFatPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFatPer.Value = 0R
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(201, 109)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel3.TabIndex = 10
        Me.MyLabel3.Text = "FAT %"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(78, 15)
        Me.txtCode.MendatroryField = True
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Nothing
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(341, 20)
        Me.txtCode.TabIndex = 1025
        Me.txtCode.Value = ""
        '
        'lblNoOfCans
        '
        Me.lblNoOfCans.FieldName = Nothing
        Me.lblNoOfCans.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNoOfCans.Location = New System.Drawing.Point(423, 109)
        Me.lblNoOfCans.Name = "lblNoOfCans"
        Me.lblNoOfCans.Size = New System.Drawing.Size(63, 16)
        Me.lblNoOfCans.TabIndex = 9
        Me.lblNoOfCans.Text = "No of Cans"
        '
        'txtNoOfCans
        '
        Me.txtNoOfCans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNoOfCans.CalculationExpression = Nothing
        Me.txtNoOfCans.DecimalPlaces = 0
        Me.txtNoOfCans.FieldCode = Nothing
        Me.txtNoOfCans.FieldDesc = Nothing
        Me.txtNoOfCans.FieldMaxLength = 0
        Me.txtNoOfCans.FieldName = Nothing
        Me.txtNoOfCans.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtNoOfCans.isCalculatedField = False
        Me.txtNoOfCans.IsSourceFromTable = False
        Me.txtNoOfCans.IsSourceFromValueList = False
        Me.txtNoOfCans.IsUnique = False
        Me.txtNoOfCans.Location = New System.Drawing.Point(491, 107)
        Me.txtNoOfCans.MaxLength = 5
        Me.txtNoOfCans.MendatroryField = True
        Me.txtNoOfCans.MyLinkLable1 = Me.lblNoOfCans
        Me.txtNoOfCans.MyLinkLable2 = Nothing
        Me.txtNoOfCans.Name = "txtNoOfCans"
        Me.txtNoOfCans.ReferenceFieldDesc = Nothing
        Me.txtNoOfCans.ReferenceFieldName = Nothing
        Me.txtNoOfCans.ReferenceTableName = Nothing
        Me.txtNoOfCans.Size = New System.Drawing.Size(58, 21)
        Me.txtNoOfCans.TabIndex = 4
        Me.txtNoOfCans.Text = "0"
        Me.txtNoOfCans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfCans.Value = 0R
        '
        'ChkALLVLC
        '
        Me.ChkALLVLC.Location = New System.Drawing.Point(370, 84)
        Me.ChkALLVLC.MyLinkLable1 = Nothing
        Me.ChkALLVLC.MyLinkLable2 = Nothing
        Me.ChkALLVLC.Name = "ChkALLVLC"
        Me.ChkALLVLC.Size = New System.Drawing.Size(49, 18)
        Me.ChkALLVLC.TabIndex = 2
        Me.ChkALLVLC.Tag1 = Nothing
        Me.ChkALLVLC.Text = "Other"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(558, 109)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel1.TabIndex = 8
        Me.MyLabel1.Text = "Milk Weight"
        '
        'RadGroupBox4
        '
        Me.RadGroupBox4.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox4.Controls.Add(Me.TotalCansAllRoute)
        Me.RadGroupBox4.Controls.Add(Me.TxtTotalWeightallRoute)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel7)
        Me.RadGroupBox4.Controls.Add(Me.lblTotalWeight)
        Me.RadGroupBox4.Controls.Add(Me.txtTotalWeight)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox4.Controls.Add(Me.MyLabel6)
        Me.RadGroupBox4.Controls.Add(Me.TxtTotalcans)
        Me.RadGroupBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox4.HeaderText = "Total"
        Me.RadGroupBox4.Location = New System.Drawing.Point(9, 129)
        Me.RadGroupBox4.Name = "RadGroupBox4"
        Me.RadGroupBox4.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox4.Size = New System.Drawing.Size(894, 40)
        Me.RadGroupBox4.TabIndex = 2
        Me.RadGroupBox4.Text = "Total"
        '
        'TotalCansAllRoute
        '
        Me.TotalCansAllRoute.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TotalCansAllRoute.CalculationExpression = Nothing
        Me.TotalCansAllRoute.DecimalPlaces = 0
        Me.TotalCansAllRoute.Enabled = False
        Me.TotalCansAllRoute.FieldCode = Nothing
        Me.TotalCansAllRoute.FieldDesc = Nothing
        Me.TotalCansAllRoute.FieldMaxLength = 0
        Me.TotalCansAllRoute.FieldName = Nothing
        Me.TotalCansAllRoute.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TotalCansAllRoute.isCalculatedField = False
        Me.TotalCansAllRoute.IsSourceFromTable = False
        Me.TotalCansAllRoute.IsSourceFromValueList = False
        Me.TotalCansAllRoute.IsUnique = False
        Me.TotalCansAllRoute.Location = New System.Drawing.Point(291, 12)
        Me.TotalCansAllRoute.MendatroryField = True
        Me.TotalCansAllRoute.MyLinkLable1 = Me.MyLabel7
        Me.TotalCansAllRoute.MyLinkLable2 = Nothing
        Me.TotalCansAllRoute.Name = "TotalCansAllRoute"
        Me.TotalCansAllRoute.ReadOnly = True
        Me.TotalCansAllRoute.ReferenceFieldDesc = Nothing
        Me.TotalCansAllRoute.ReferenceFieldName = Nothing
        Me.TotalCansAllRoute.ReferenceTableName = Nothing
        Me.TotalCansAllRoute.Size = New System.Drawing.Size(122, 21)
        Me.TotalCansAllRoute.TabIndex = 1027
        Me.TotalCansAllRoute.TabStop = False
        Me.TotalCansAllRoute.Text = "0"
        Me.TotalCansAllRoute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TotalCansAllRoute.Value = 0R
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(227, 14)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(61, 16)
        Me.MyLabel7.TabIndex = 1026
        Me.MyLabel7.Text = "Total Cans"
        '
        'TxtTotalWeightallRoute
        '
        Me.TxtTotalWeightallRoute.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTotalWeightallRoute.CalculationExpression = Nothing
        Me.TxtTotalWeightallRoute.DecimalPlaces = 0
        Me.TxtTotalWeightallRoute.Enabled = False
        Me.TxtTotalWeightallRoute.FieldCode = Nothing
        Me.TxtTotalWeightallRoute.FieldDesc = Nothing
        Me.TxtTotalWeightallRoute.FieldMaxLength = 0
        Me.TxtTotalWeightallRoute.FieldName = Nothing
        Me.TxtTotalWeightallRoute.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalWeightallRoute.isCalculatedField = False
        Me.TxtTotalWeightallRoute.IsSourceFromTable = False
        Me.TxtTotalWeightallRoute.IsSourceFromValueList = False
        Me.TxtTotalWeightallRoute.IsUnique = False
        Me.TxtTotalWeightallRoute.Location = New System.Drawing.Point(89, 12)
        Me.TxtTotalWeightallRoute.MendatroryField = True
        Me.TxtTotalWeightallRoute.MyLinkLable1 = Me.MyLabel9
        Me.TxtTotalWeightallRoute.MyLinkLable2 = Nothing
        Me.TxtTotalWeightallRoute.Name = "TxtTotalWeightallRoute"
        Me.TxtTotalWeightallRoute.ReadOnly = True
        Me.TxtTotalWeightallRoute.ReferenceFieldDesc = Nothing
        Me.TxtTotalWeightallRoute.ReferenceFieldName = Nothing
        Me.TxtTotalWeightallRoute.ReferenceTableName = Nothing
        Me.TxtTotalWeightallRoute.Size = New System.Drawing.Size(119, 21)
        Me.TxtTotalWeightallRoute.TabIndex = 1025
        Me.TxtTotalWeightallRoute.TabStop = False
        Me.TxtTotalWeightallRoute.Text = "0"
        Me.TxtTotalWeightallRoute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotalWeightallRoute.Value = 0R
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel9.Location = New System.Drawing.Point(13, 14)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel9.TabIndex = 1024
        Me.MyLabel9.Text = "Total Weight"
        '
        'lblTotalWeight
        '
        Me.lblTotalWeight.FieldName = Nothing
        Me.lblTotalWeight.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblTotalWeight.Location = New System.Drawing.Point(429, 14)
        Me.lblTotalWeight.Name = "lblTotalWeight"
        Me.lblTotalWeight.Size = New System.Drawing.Size(136, 16)
        Me.lblTotalWeight.TabIndex = 43
        Me.lblTotalWeight.Text = "Total Weight(Route Wise)"
        '
        'txtTotalWeight
        '
        Me.txtTotalWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTotalWeight.CalculationExpression = Nothing
        Me.txtTotalWeight.DecimalPlaces = 0
        Me.txtTotalWeight.Enabled = False
        Me.txtTotalWeight.FieldCode = Nothing
        Me.txtTotalWeight.FieldDesc = Nothing
        Me.txtTotalWeight.FieldMaxLength = 0
        Me.txtTotalWeight.FieldName = Nothing
        Me.txtTotalWeight.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalWeight.isCalculatedField = False
        Me.txtTotalWeight.IsSourceFromTable = False
        Me.txtTotalWeight.IsSourceFromValueList = False
        Me.txtTotalWeight.IsUnique = False
        Me.txtTotalWeight.Location = New System.Drawing.Point(571, 12)
        Me.txtTotalWeight.MendatroryField = True
        Me.txtTotalWeight.MyLinkLable1 = Me.lblTotalWeight
        Me.txtTotalWeight.MyLinkLable2 = Nothing
        Me.txtTotalWeight.Name = "txtTotalWeight"
        Me.txtTotalWeight.ReadOnly = True
        Me.txtTotalWeight.ReferenceFieldDesc = Nothing
        Me.txtTotalWeight.ReferenceFieldName = Nothing
        Me.txtTotalWeight.ReferenceTableName = Nothing
        Me.txtTotalWeight.Size = New System.Drawing.Size(74, 21)
        Me.txtTotalWeight.TabIndex = 100
        Me.txtTotalWeight.TabStop = False
        Me.txtTotalWeight.Text = "0"
        Me.txtTotalWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalWeight.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(657, 14)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(127, 16)
        Me.MyLabel6.TabIndex = 101
        Me.MyLabel6.Text = "Total Cans(Route Wise)"
        '
        'TxtTotalcans
        '
        Me.TxtTotalcans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtTotalcans.CalculationExpression = Nothing
        Me.TxtTotalcans.DecimalPlaces = 0
        Me.TxtTotalcans.Enabled = False
        Me.TxtTotalcans.FieldCode = Nothing
        Me.TxtTotalcans.FieldDesc = Nothing
        Me.TxtTotalcans.FieldMaxLength = 0
        Me.TxtTotalcans.FieldName = Nothing
        Me.TxtTotalcans.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalcans.isCalculatedField = False
        Me.TxtTotalcans.IsSourceFromTable = False
        Me.TxtTotalcans.IsSourceFromValueList = False
        Me.TxtTotalcans.IsUnique = False
        Me.TxtTotalcans.Location = New System.Drawing.Point(790, 12)
        Me.TxtTotalcans.MendatroryField = True
        Me.TxtTotalcans.MyLinkLable1 = Me.MyLabel6
        Me.TxtTotalcans.MyLinkLable2 = Nothing
        Me.TxtTotalcans.Name = "TxtTotalcans"
        Me.TxtTotalcans.ReadOnly = True
        Me.TxtTotalcans.ReferenceFieldDesc = Nothing
        Me.TxtTotalcans.ReferenceFieldName = Nothing
        Me.TxtTotalcans.ReferenceTableName = Nothing
        Me.TxtTotalcans.Size = New System.Drawing.Size(101, 21)
        Me.TxtTotalcans.TabIndex = 102
        Me.TxtTotalcans.TabStop = False
        Me.TxtTotalcans.Text = "0"
        Me.TxtTotalcans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtTotalcans.Value = 0R
        '
        'txtMilkWeight
        '
        Me.txtMilkWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMilkWeight.CalculationExpression = Nothing
        Me.txtMilkWeight.DecimalPlaces = 2
        Me.txtMilkWeight.FieldCode = Nothing
        Me.txtMilkWeight.FieldDesc = Nothing
        Me.txtMilkWeight.FieldMaxLength = 0
        Me.txtMilkWeight.FieldName = Nothing
        Me.txtMilkWeight.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtMilkWeight.isCalculatedField = False
        Me.txtMilkWeight.IsSourceFromTable = False
        Me.txtMilkWeight.IsSourceFromValueList = False
        Me.txtMilkWeight.IsUnique = False
        Me.txtMilkWeight.Location = New System.Drawing.Point(627, 107)
        Me.txtMilkWeight.MaxLength = 7
        Me.txtMilkWeight.MendatroryField = True
        Me.txtMilkWeight.MyLinkLable1 = Me.MyLabel1
        Me.txtMilkWeight.MyLinkLable2 = Nothing
        Me.txtMilkWeight.Name = "txtMilkWeight"
        Me.txtMilkWeight.ReferenceFieldDesc = Nothing
        Me.txtMilkWeight.ReferenceFieldName = Nothing
        Me.txtMilkWeight.ReferenceTableName = Nothing
        Me.txtMilkWeight.Size = New System.Drawing.Size(58, 21)
        Me.txtMilkWeight.TabIndex = 5
        Me.txtMilkWeight.Text = "0"
        Me.txtMilkWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMilkWeight.Value = 0R
        '
        'fndItem_Code
        '
        Me.fndItem_Code.AutoSize = False
        Me.fndItem_Code.BorderVisible = True
        Me.fndItem_Code.FieldName = Nothing
        Me.fndItem_Code.Location = New System.Drawing.Point(491, 61)
        Me.fndItem_Code.Name = "fndItem_Code"
        Me.fndItem_Code.Size = New System.Drawing.Size(148, 20)
        Me.fndItem_Code.TabIndex = 37
        Me.fndItem_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.fndItem_Code.TextWrap = False
        '
        'LblUom
        '
        Me.LblUom.FieldName = Nothing
        Me.LblUom.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.LblUom.Location = New System.Drawing.Point(692, 109)
        Me.LblUom.Name = "LblUom"
        Me.LblUom.Size = New System.Drawing.Size(33, 16)
        Me.LblUom.TabIndex = 7
        Me.LblUom.Text = "UOM"
        '
        'fndVspCode
        '
        Me.fndVspCode.AutoSize = False
        Me.fndVspCode.BorderVisible = True
        Me.fndVspCode.FieldName = Nothing
        Me.fndVspCode.Location = New System.Drawing.Point(491, 83)
        Me.fndVspCode.Name = "fndVspCode"
        Me.fndVspCode.Size = New System.Drawing.Size(148, 20)
        Me.fndVspCode.TabIndex = 1023
        Me.fndVspCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.fndVspCode.TextWrap = False
        '
        'chkOther
        '
        Me.chkOther.Location = New System.Drawing.Point(196, 62)
        Me.chkOther.MyLinkLable1 = Nothing
        Me.chkOther.MyLinkLable2 = Nothing
        Me.chkOther.Name = "chkOther"
        Me.chkOther.Size = New System.Drawing.Size(49, 18)
        Me.chkOther.TabIndex = 1
        Me.chkOther.Tag1 = Nothing
        Me.chkOther.Text = "Other"
        '
        'cboRejectType
        '
        Me.cboRejectType.AutoCompleteDisplayMember = Nothing
        Me.cboRejectType.AutoCompleteValueMember = Nothing
        Me.cboRejectType.CalculationExpression = Nothing
        Me.cboRejectType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboRejectType.FieldCode = Nothing
        Me.cboRejectType.FieldDesc = Nothing
        Me.cboRejectType.FieldMaxLength = 0
        Me.cboRejectType.FieldName = Nothing
        Me.cboRejectType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRejectType.isCalculatedField = False
        Me.cboRejectType.IsSourceFromTable = False
        Me.cboRejectType.IsSourceFromValueList = False
        Me.cboRejectType.IsUnique = False
        RadListDataItem1.Text = "Good"
        RadListDataItem2.Text = "OK"
        RadListDataItem3.Text = "Rejected"
        Me.cboRejectType.Items.Add(RadListDataItem1)
        Me.cboRejectType.Items.Add(RadListDataItem2)
        Me.cboRejectType.Items.Add(RadListDataItem3)
        Me.cboRejectType.Location = New System.Drawing.Point(78, 108)
        Me.cboRejectType.MendatroryField = True
        Me.cboRejectType.MyLinkLable1 = Me.lblMilkType
        Me.cboRejectType.MyLinkLable2 = Nothing
        Me.cboRejectType.Name = "cboRejectType"
        Me.cboRejectType.ReferenceFieldDesc = Nothing
        Me.cboRejectType.ReferenceFieldName = Nothing
        Me.cboRejectType.ReferenceTableName = Nothing
        Me.cboRejectType.Size = New System.Drawing.Size(119, 18)
        Me.cboRejectType.TabIndex = 2
        '
        'lblMilkType
        '
        Me.lblMilkType.FieldName = Nothing
        Me.lblMilkType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMilkType.Location = New System.Drawing.Point(10, 109)
        Me.lblMilkType.Name = "lblMilkType"
        Me.lblMilkType.Size = New System.Drawing.Size(67, 16)
        Me.lblMilkType.TabIndex = 11
        Me.lblMilkType.Text = "Reject Type"
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(423, 62)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel2.TabIndex = 35
        Me.MyLabel2.Text = "Item Code"
        '
        'btnsave
        '
        Me.btnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(735, 108)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(58, 18)
        Me.btnsave.TabIndex = 6
        Me.btnsave.Text = ">>"
        '
        'lblItemDesc
        '
        Me.lblItemDesc.AutoSize = False
        Me.lblItemDesc.BorderVisible = True
        Me.lblItemDesc.FieldName = Nothing
        Me.lblItemDesc.Location = New System.Drawing.Point(642, 61)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.Size = New System.Drawing.Size(262, 20)
        Me.lblItemDesc.TabIndex = 36
        Me.lblItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblItemDesc.TextWrap = False
        '
        'lblType
        '
        Me.lblType.FieldName = Nothing
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(680, 181)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 16)
        Me.lblType.TabIndex = 39
        Me.lblType.Text = "Type"
        Me.lblType.Visible = False
        '
        'lblVehicleCode
        '
        Me.lblVehicleCode.FieldName = Nothing
        Me.lblVehicleCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVehicleCode.Location = New System.Drawing.Point(10, 62)
        Me.lblVehicleCode.Name = "lblVehicleCode"
        Me.lblVehicleCode.Size = New System.Drawing.Size(60, 18)
        Me.lblVehicleCode.TabIndex = 32
        Me.lblVehicleCode.Text = "Vehicle No"
        '
        'fndVehicleCode
        '
        Me.fndVehicleCode.AutoSize = False
        Me.fndVehicleCode.BorderVisible = True
        Me.fndVehicleCode.FieldName = Nothing
        Me.fndVehicleCode.Location = New System.Drawing.Point(78, 61)
        Me.fndVehicleCode.Name = "fndVehicleCode"
        Me.fndVehicleCode.Size = New System.Drawing.Size(119, 20)
        Me.fndVehicleCode.TabIndex = 33
        Me.fndVehicleCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(423, 39)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(65, 18)
        Me.lblRouteCode.TabIndex = 29
        Me.lblRouteCode.Text = "Route Code"
        '
        'fndRouteCode
        '
        Me.fndRouteCode.CalculationExpression = Nothing
        Me.fndRouteCode.FieldCode = Nothing
        Me.fndRouteCode.FieldDesc = Nothing
        Me.fndRouteCode.FieldMaxLength = 0
        Me.fndRouteCode.FieldName = Nothing
        Me.fndRouteCode.isCalculatedField = False
        Me.fndRouteCode.IsSourceFromTable = False
        Me.fndRouteCode.IsSourceFromValueList = False
        Me.fndRouteCode.IsUnique = False
        Me.fndRouteCode.Location = New System.Drawing.Point(491, 38)
        Me.fndRouteCode.MendatroryField = True
        Me.fndRouteCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndRouteCode.MyLinkLable1 = Me.lblRouteCode
        Me.fndRouteCode.MyLinkLable2 = Nothing
        Me.fndRouteCode.MyReadOnly = False
        Me.fndRouteCode.MyShowMasterFormButton = False
        Me.fndRouteCode.Name = "fndRouteCode"
        Me.fndRouteCode.ReferenceFieldDesc = Nothing
        Me.fndRouteCode.ReferenceFieldName = Nothing
        Me.fndRouteCode.ReferenceTableName = Nothing
        Me.fndRouteCode.Size = New System.Drawing.Size(147, 20)
        Me.fndRouteCode.TabIndex = 0
        Me.fndRouteCode.Value = ""
        '
        'lblRouteDesc
        '
        Me.lblRouteDesc.AutoSize = False
        Me.lblRouteDesc.BorderVisible = True
        Me.lblRouteDesc.FieldName = Nothing
        Me.lblRouteDesc.Location = New System.Drawing.Point(642, 38)
        Me.lblRouteDesc.Name = "lblRouteDesc"
        Me.lblRouteDesc.Size = New System.Drawing.Size(262, 20)
        Me.lblRouteDesc.TabIndex = 30
        Me.lblRouteDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRouteDesc.TextWrap = False
        '
        'lblVSPCode
        '
        Me.lblVSPCode.FieldName = Nothing
        Me.lblVSPCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVSPCode.Location = New System.Drawing.Point(423, 84)
        Me.lblVSPCode.Name = "lblVSPCode"
        Me.lblVSPCode.Size = New System.Drawing.Size(55, 18)
        Me.lblVSPCode.TabIndex = 26
        Me.lblVSPCode.Text = "VSP Code"
        '
        'lblVSPDesc
        '
        Me.lblVSPDesc.AutoSize = False
        Me.lblVSPDesc.BorderVisible = True
        Me.lblVSPDesc.FieldName = Nothing
        Me.lblVSPDesc.Location = New System.Drawing.Point(642, 83)
        Me.lblVSPDesc.Name = "lblVSPDesc"
        Me.lblVSPDesc.Size = New System.Drawing.Size(262, 20)
        Me.lblVSPDesc.TabIndex = 27
        Me.lblVSPDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVSPDesc.TextWrap = False
        '
        'lblVLCCode
        '
        Me.lblVLCCode.FieldName = Nothing
        Me.lblVLCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVLCCode.Location = New System.Drawing.Point(10, 84)
        Me.lblVLCCode.Name = "lblVLCCode"
        Me.lblVLCCode.Size = New System.Drawing.Size(55, 18)
        Me.lblVLCCode.TabIndex = 23
        Me.lblVLCCode.Text = "DCS Code"
        '
        'fndVLCCode
        '
        Me.fndVLCCode.CalculationExpression = Nothing
        Me.fndVLCCode.FieldCode = Nothing
        Me.fndVLCCode.FieldDesc = Nothing
        Me.fndVLCCode.FieldMaxLength = 0
        Me.fndVLCCode.FieldName = Nothing
        Me.fndVLCCode.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVLCCode.isCalculatedField = False
        Me.fndVLCCode.IsSourceFromTable = False
        Me.fndVLCCode.IsSourceFromValueList = False
        Me.fndVLCCode.IsUnique = False
        Me.fndVLCCode.Location = New System.Drawing.Point(78, 83)
        Me.fndVLCCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.fndVLCCode.MendatroryField = True
        Me.fndVLCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndVLCCode.MyLinkLable1 = Me.lblVLCCode
        Me.fndVLCCode.MyLinkLable2 = Nothing
        Me.fndVLCCode.MyReadOnly = False
        Me.fndVLCCode.MyShowMasterFormButton = False
        Me.fndVLCCode.Name = "fndVLCCode"
        Me.fndVLCCode.ReferenceFieldDesc = Nothing
        Me.fndVLCCode.ReferenceFieldName = Nothing
        Me.fndVLCCode.ReferenceTableName = Nothing
        Me.fndVLCCode.Size = New System.Drawing.Size(119, 20)
        Me.fndVLCCode.TabIndex = 1
        Me.fndVLCCode.Value = ""
        '
        'lblVLCDesc
        '
        Me.lblVLCDesc.AutoSize = False
        Me.lblVLCDesc.BorderVisible = True
        Me.lblVLCDesc.FieldName = Nothing
        Me.lblVLCDesc.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblVLCDesc.Location = New System.Drawing.Point(196, 83)
        Me.lblVLCDesc.Name = "lblVLCDesc"
        Me.lblVLCDesc.Size = New System.Drawing.Size(173, 20)
        Me.lblVLCDesc.TabIndex = 24
        Me.lblVLCDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVLCDesc.TextWrap = False
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(642, 17)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(29, 16)
        Me.lblBOMStatus.TabIndex = 21
        Me.lblBOMStatus.Text = "Shift"
        '
        'cboShift
        '
        Me.cboShift.AutoCompleteDisplayMember = Nothing
        Me.cboShift.AutoCompleteValueMember = Nothing
        Me.cboShift.CalculationExpression = Nothing
        Me.cboShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cboShift.FieldCode = Nothing
        Me.cboShift.FieldDesc = Nothing
        Me.cboShift.FieldMaxLength = 0
        Me.cboShift.FieldName = Nothing
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.isCalculatedField = False
        Me.cboShift.IsSourceFromTable = False
        Me.cboShift.IsSourceFromValueList = False
        Me.cboShift.IsUnique = False
        RadListDataItem4.Text = "M"
        RadListDataItem5.Text = "E"
        Me.cboShift.Items.Add(RadListDataItem4)
        Me.cboShift.Items.Add(RadListDataItem5)
        Me.cboShift.Location = New System.Drawing.Point(673, 16)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(114, 18)
        Me.cboShift.TabIndex = 20
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(423, 17)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(53, 16)
        Me.lblDocDate.TabIndex = 19
        Me.lblDocDate.Text = "Doc Date"
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CalculationExpression = Nothing
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpDocDate.FieldCode = Nothing
        Me.dtpDocDate.FieldDesc = Nothing
        Me.dtpDocDate.FieldMaxLength = 0
        Me.dtpDocDate.FieldName = Nothing
        Me.dtpDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.isCalculatedField = False
        Me.dtpDocDate.IsSourceFromTable = False
        Me.dtpDocDate.IsSourceFromValueList = False
        Me.dtpDocDate.IsUnique = False
        Me.dtpDocDate.Location = New System.Drawing.Point(491, 16)
        Me.dtpDocDate.MendatroryField = True
        Me.dtpDocDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.MyLinkLable1 = Me.lblDocDate
        Me.dtpDocDate.MyLinkLable2 = Nothing
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpDocDate.ReferenceFieldDesc = Nothing
        Me.dtpDocDate.ReferenceFieldName = Nothing
        Me.dtpDocDate.ReferenceTableName = Nothing
        Me.dtpDocDate.Size = New System.Drawing.Size(148, 18)
        Me.dtpDocDate.TabIndex = 18
        Me.dtpDocDate.TabStop = False
        Me.dtpDocDate.Text = "03/05/2011 12:00:00 AM"
        Me.dtpDocDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(10, 39)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 13
        Me.lblMCCCode.Text = "MCC Code"
        '
        'fndMCCCode
        '
        Me.fndMCCCode.CalculationExpression = Nothing
        Me.fndMCCCode.FieldCode = Nothing
        Me.fndMCCCode.FieldDesc = Nothing
        Me.fndMCCCode.FieldMaxLength = 0
        Me.fndMCCCode.FieldName = Nothing
        Me.fndMCCCode.isCalculatedField = False
        Me.fndMCCCode.IsSourceFromTable = False
        Me.fndMCCCode.IsSourceFromValueList = False
        Me.fndMCCCode.IsUnique = False
        Me.fndMCCCode.Location = New System.Drawing.Point(78, 38)
        Me.fndMCCCode.MendatroryField = True
        Me.fndMCCCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndMCCCode.MyLinkLable1 = Me.lblMCCCode
        Me.fndMCCCode.MyLinkLable2 = Nothing
        Me.fndMCCCode.MyReadOnly = False
        Me.fndMCCCode.MyShowMasterFormButton = False
        Me.fndMCCCode.Name = "fndMCCCode"
        Me.fndMCCCode.ReferenceFieldDesc = Nothing
        Me.fndMCCCode.ReferenceFieldName = Nothing
        Me.fndMCCCode.ReferenceTableName = Nothing
        Me.fndMCCCode.Size = New System.Drawing.Size(119, 20)
        Me.fndMCCCode.TabIndex = 12
        Me.fndMCCCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(10, 17)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(58, 16)
        Me.lblCode.TabIndex = 10
        Me.lblCode.Text = "Document"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(805, 15)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(98, 20)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 11
        '
        'RadButton4
        '
        Me.RadButton4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton4.Location = New System.Drawing.Point(203, 5)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(70, 21)
        Me.RadButton4.TabIndex = 10
        Me.RadButton4.Text = "Recalulate"
        Me.RadButton4.Visible = False
        '
        'RadButton3
        '
        Me.RadButton3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton3.Location = New System.Drawing.Point(713, 5)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(145, 21)
        Me.RadButton3.TabIndex = 9
        Me.RadButton3.Text = "Reverse and Unpost"
        Me.RadButton3.Visible = False
        '
        'RadButton2
        '
        Me.RadButton2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton2.Location = New System.Drawing.Point(10, 5)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(43, 21)
        Me.RadButton2.TabIndex = 8
        Me.RadButton2.Text = "Shift"
        Me.RadButton2.Visible = False
        '
        'RadButton1
        '
        Me.RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadButton1.Location = New System.Drawing.Point(56, 5)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(145, 21)
        Me.RadButton1.TabIndex = 7
        Me.RadButton1.Text = "Post Only For Back Entry"
        Me.RadButton1.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(933, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'rdmenufile
        '
        Me.rdmenufile.BackColor = System.Drawing.Color.Transparent
        Me.rdmenufile.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rdmenufile1})
        Me.rdmenufile.Location = New System.Drawing.Point(0, 0)
        Me.rdmenufile.Name = "rdmenufile"
        Me.rdmenufile.Size = New System.Drawing.Size(1005, 20)
        Me.rdmenufile.TabIndex = 4
        Me.rdmenufile.Text = "File"
        '
        'rdmenufile1
        '
        Me.rdmenufile1.AccessibleDescription = "File"
        Me.rdmenufile1.AccessibleName = "File"
        Me.rdmenufile1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.BtnSaveLayout, Me.BtnDeleteLayout})
        Me.rdmenufile1.Name = "rdmenufile1"
        Me.rdmenufile1.Text = "Settings"
        '
        'BtnSaveLayout
        '
        Me.BtnSaveLayout.AccessibleDescription = "Save Layout"
        Me.BtnSaveLayout.AccessibleName = "Save Layout"
        Me.BtnSaveLayout.Name = "BtnSaveLayout"
        Me.BtnSaveLayout.Text = "Save Layout"
        '
        'BtnDeleteLayout
        '
        Me.BtnDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.BtnDeleteLayout.AccessibleName = "Delete Layout"
        Me.BtnDeleteLayout.Name = "BtnDeleteLayout"
        Me.BtnDeleteLayout.Text = "Delete Layout"
        '
        'frmMilkRejectEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 431)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenufile)
        Me.Name = "frmMilkRejectEntry"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Reject"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.txtVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMccName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnReturnTypeCOB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReturnTypeDrain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReturnTypeReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnReturnTypeNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNFPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFatPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkALLVLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox4.ResumeLayout(False)
        Me.RadGroupBox4.PerformLayout()
        CType(Me.TotalCansAllRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalWeightallRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTotalcans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMilkWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndItem_Code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblUom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVspCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboRejectType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMilkType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fndVehicleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVSPDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVLCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblVLCDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdmenufile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents fndMCCCode As common.UserControls.txtFinder
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents dtpDocDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblVLCCode As common.Controls.MyLabel
    Friend WithEvents fndVLCCode As common.UserControls.txtFinder
    Friend WithEvents lblVLCDesc As common.Controls.MyLabel
    Friend WithEvents lblVSPCode As common.Controls.MyLabel
    Friend WithEvents lblVSPDesc As common.Controls.MyLabel
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents fndRouteCode As common.UserControls.txtFinder
    Friend WithEvents lblRouteDesc As common.Controls.MyLabel
    Friend WithEvents lblVehicleCode As common.Controls.MyLabel
    Friend WithEvents fndVehicleCode As common.Controls.MyLabel
    Friend WithEvents txtNoOfCans As common.MyNumBox
    Friend WithEvents lblNoOfCans As common.Controls.MyLabel
    Friend WithEvents txtMilkWeight As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblMilkType As common.Controls.MyLabel
    Friend WithEvents cboRejectType As common.Controls.MyComboBox
    Friend WithEvents lblType As common.Controls.MyLabel
    Friend WithEvents txtTotalWeight As common.MyNumBox
    Friend WithEvents lblTotalWeight As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblItemDesc As common.Controls.MyLabel
    Friend WithEvents rdmenufile As Telerik.WinControls.UI.RadMenu
    Friend WithEvents rdmenufile1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents BtnDeleteLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents LblUom As common.Controls.MyLabel
    Friend WithEvents LblMccName As common.Controls.MyLabel
    Friend WithEvents chkOther As common.Controls.MyCheckBox
    Friend WithEvents TxtTotalcans As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents fndVspCode As common.Controls.MyLabel
    Friend WithEvents fndItem_Code As common.Controls.MyLabel
    Friend WithEvents TotalCansAllRoute As common.MyNumBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents TxtTotalWeightallRoute As common.MyNumBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents RadGroupBox4 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ChkALLVLC As common.Controls.MyCheckBox
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents txtFatPer As common.MyNumBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtSNFPer As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnReturnTypeDrain As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnReturnTypeReturn As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnReturnTypeNA As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents RadButton1 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton2 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton3 As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadButton4 As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnReturnTypeCOB As RadRadioButton
    Friend WithEvents lblVehicle As common.Controls.MyLabel
    Friend WithEvents txtVehicle As common.Controls.MyTextBox
End Class

