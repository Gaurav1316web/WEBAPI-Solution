<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVSPMapping
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UsLock1 = New common.usLock()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.txtRoute = New common.UserControls.txtMultiSelectFinder()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.dtpEndDate = New common.Controls.MyDateTimePicker()
        Me.dtStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblDayWiseIncentive = New common.Controls.MyTextBox()
        Me.txtDayWiseIncentive = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.lblDeduction = New common.Controls.MyTextBox()
        Me.txtDeduction = New common.UserControls.txtFinder()
        Me.lblCommission = New common.Controls.MyTextBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtCommission = New common.UserControls.txtFinder()
        Me.lblUOM = New common.Controls.MyLabel()
        Me.txtVSP = New common.UserControls.txtMultiSelectFinder()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel15 = New common.Controls.MyLabel()
        Me.MyLabel14 = New common.Controls.MyLabel()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblAdvanceCode = New common.Controls.MyLabel()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnUpdates = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDayWiseIncentive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCommission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpdates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpdates)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(789, 432)
        Me.SplitContainer1.SplitterDistance = 398
        Me.SplitContainer1.TabIndex = 1
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(447, 2)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(64, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1081
        '
        'chkInactive
        '
        Me.chkInactive.Enabled = False
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(513, 4)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 16)
        Me.chkInactive.TabIndex = 1080
        Me.chkInactive.Text = "Inactive"
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(193, 50)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel13.TabIndex = 1079
        Me.MyLabel13.Text = "End Date"
        '
        'txtRoute
        '
        Me.txtRoute.arrDispalyMember = Nothing
        Me.txtRoute.arrValueMember = Nothing
        Me.txtRoute.Location = New System.Drawing.Point(109, 159)
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.lblLocation
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyNullText = "Select Route..."
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(463, 19)
        Me.txtRoute.TabIndex = 381
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(3, 159)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(36, 18)
        Me.lblLocation.TabIndex = 382
        Me.lblLocation.Text = "Route"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalculationExpression = Nothing
        Me.dtpEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpEndDate.FieldCode = Nothing
        Me.dtpEndDate.FieldDesc = Nothing
        Me.dtpEndDate.FieldMaxLength = 0
        Me.dtpEndDate.FieldName = Nothing
        Me.dtpEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.isCalculatedField = False
        Me.dtpEndDate.IsSourceFromTable = False
        Me.dtpEndDate.IsSourceFromValueList = False
        Me.dtpEndDate.IsUnique = False
        Me.dtpEndDate.Location = New System.Drawing.Point(248, 49)
        Me.dtpEndDate.MendatroryField = False
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.MyLinkLable1 = Me.MyLabel13
        Me.dtpEndDate.MyLinkLable2 = Nothing
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.ReferenceFieldDesc = Nothing
        Me.dtpEndDate.ReferenceFieldName = Nothing
        Me.dtpEndDate.ReferenceTableName = Nothing
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(91, 18)
        Me.dtpEndDate.TabIndex = 1078
        Me.dtpEndDate.TabStop = False
        Me.dtpEndDate.Text = "13/06/2011"
        Me.dtpEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'dtStartDate
        '
        Me.dtStartDate.CalculationExpression = Nothing
        Me.dtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtStartDate.FieldCode = Nothing
        Me.dtStartDate.FieldDesc = Nothing
        Me.dtStartDate.FieldMaxLength = 0
        Me.dtStartDate.FieldName = Nothing
        Me.dtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.isCalculatedField = False
        Me.dtStartDate.IsSourceFromTable = False
        Me.dtStartDate.IsSourceFromValueList = False
        Me.dtStartDate.IsUnique = False
        Me.dtStartDate.Location = New System.Drawing.Point(109, 49)
        Me.dtStartDate.MendatroryField = False
        Me.dtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.MyLinkLable1 = Me.MyLabel3
        Me.dtStartDate.MyLinkLable2 = Nothing
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.ReferenceFieldDesc = Nothing
        Me.dtStartDate.ReferenceFieldName = Nothing
        Me.dtStartDate.ReferenceTableName = Nothing
        Me.dtStartDate.Size = New System.Drawing.Size(80, 18)
        Me.dtStartDate.TabIndex = 1076
        Me.dtStartDate.TabStop = False
        Me.dtStartDate.Text = "13/06/2011"
        Me.dtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(3, 50)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 1077
        Me.MyLabel3.Text = "Start Date"
        '
        'lblDayWiseIncentive
        '
        Me.lblDayWiseIncentive.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblDayWiseIncentive.CalculationExpression = Nothing
        Me.lblDayWiseIncentive.Enabled = False
        Me.lblDayWiseIncentive.FieldCode = Nothing
        Me.lblDayWiseIncentive.FieldDesc = Nothing
        Me.lblDayWiseIncentive.FieldMaxLength = 0
        Me.lblDayWiseIncentive.FieldName = Nothing
        Me.lblDayWiseIncentive.isCalculatedField = False
        Me.lblDayWiseIncentive.IsSourceFromTable = False
        Me.lblDayWiseIncentive.IsSourceFromValueList = False
        Me.lblDayWiseIncentive.IsUnique = False
        Me.lblDayWiseIncentive.Location = New System.Drawing.Point(248, 113)
        Me.lblDayWiseIncentive.MendatroryField = False
        Me.lblDayWiseIncentive.MyLinkLable1 = Nothing
        Me.lblDayWiseIncentive.MyLinkLable2 = Nothing
        Me.lblDayWiseIncentive.Name = "lblDayWiseIncentive"
        Me.lblDayWiseIncentive.ReferenceFieldDesc = Nothing
        Me.lblDayWiseIncentive.ReferenceFieldName = Nothing
        Me.lblDayWiseIncentive.ReferenceTableName = Nothing
        Me.lblDayWiseIncentive.Size = New System.Drawing.Size(325, 20)
        Me.lblDayWiseIncentive.TabIndex = 376
        '
        'txtDayWiseIncentive
        '
        Me.txtDayWiseIncentive.CalculationExpression = Nothing
        Me.txtDayWiseIncentive.FieldCode = Nothing
        Me.txtDayWiseIncentive.FieldDesc = Nothing
        Me.txtDayWiseIncentive.FieldMaxLength = 0
        Me.txtDayWiseIncentive.FieldName = Nothing
        Me.txtDayWiseIncentive.isCalculatedField = False
        Me.txtDayWiseIncentive.IsSourceFromTable = False
        Me.txtDayWiseIncentive.IsSourceFromValueList = False
        Me.txtDayWiseIncentive.IsUnique = False
        Me.txtDayWiseIncentive.Location = New System.Drawing.Point(109, 114)
        Me.txtDayWiseIncentive.MendatroryField = True
        Me.txtDayWiseIncentive.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDayWiseIncentive.MyLinkLable1 = Nothing
        Me.txtDayWiseIncentive.MyLinkLable2 = Nothing
        Me.txtDayWiseIncentive.MyReadOnly = False
        Me.txtDayWiseIncentive.MyShowMasterFormButton = False
        Me.txtDayWiseIncentive.Name = "txtDayWiseIncentive"
        Me.txtDayWiseIncentive.ReferenceFieldDesc = Nothing
        Me.txtDayWiseIncentive.ReferenceFieldName = Nothing
        Me.txtDayWiseIncentive.ReferenceTableName = Nothing
        Me.txtDayWiseIncentive.Size = New System.Drawing.Size(135, 19)
        Me.txtDayWiseIncentive.TabIndex = 374
        Me.txtDayWiseIncentive.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(3, 115)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(101, 16)
        Me.MyLabel2.TabIndex = 375
        Me.MyLabel2.Text = "Day wise Incentive"
        '
        'lblDeduction
        '
        Me.lblDeduction.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblDeduction.CalculationExpression = Nothing
        Me.lblDeduction.Enabled = False
        Me.lblDeduction.FieldCode = Nothing
        Me.lblDeduction.FieldDesc = Nothing
        Me.lblDeduction.FieldMaxLength = 0
        Me.lblDeduction.FieldName = Nothing
        Me.lblDeduction.isCalculatedField = False
        Me.lblDeduction.IsSourceFromTable = False
        Me.lblDeduction.IsSourceFromValueList = False
        Me.lblDeduction.IsUnique = False
        Me.lblDeduction.Location = New System.Drawing.Point(248, 91)
        Me.lblDeduction.MendatroryField = False
        Me.lblDeduction.MyLinkLable1 = Nothing
        Me.lblDeduction.MyLinkLable2 = Nothing
        Me.lblDeduction.Name = "lblDeduction"
        Me.lblDeduction.ReferenceFieldDesc = Nothing
        Me.lblDeduction.ReferenceFieldName = Nothing
        Me.lblDeduction.ReferenceTableName = Nothing
        Me.lblDeduction.Size = New System.Drawing.Size(325, 20)
        Me.lblDeduction.TabIndex = 373
        '
        'txtDeduction
        '
        Me.txtDeduction.CalculationExpression = Nothing
        Me.txtDeduction.FieldCode = Nothing
        Me.txtDeduction.FieldDesc = Nothing
        Me.txtDeduction.FieldMaxLength = 0
        Me.txtDeduction.FieldName = Nothing
        Me.txtDeduction.isCalculatedField = False
        Me.txtDeduction.IsSourceFromTable = False
        Me.txtDeduction.IsSourceFromValueList = False
        Me.txtDeduction.IsUnique = False
        Me.txtDeduction.Location = New System.Drawing.Point(109, 92)
        Me.txtDeduction.MendatroryField = True
        Me.txtDeduction.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeduction.MyLinkLable1 = Nothing
        Me.txtDeduction.MyLinkLable2 = Nothing
        Me.txtDeduction.MyReadOnly = False
        Me.txtDeduction.MyShowMasterFormButton = False
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.ReferenceFieldDesc = Nothing
        Me.txtDeduction.ReferenceFieldName = Nothing
        Me.txtDeduction.ReferenceTableName = Nothing
        Me.txtDeduction.Size = New System.Drawing.Size(135, 19)
        Me.txtDeduction.TabIndex = 371
        Me.txtDeduction.Value = ""
        '
        'lblCommission
        '
        Me.lblCommission.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(187, Byte), Integer))
        Me.lblCommission.CalculationExpression = Nothing
        Me.lblCommission.Enabled = False
        Me.lblCommission.FieldCode = Nothing
        Me.lblCommission.FieldDesc = Nothing
        Me.lblCommission.FieldMaxLength = 0
        Me.lblCommission.FieldName = Nothing
        Me.lblCommission.isCalculatedField = False
        Me.lblCommission.IsSourceFromTable = False
        Me.lblCommission.IsSourceFromValueList = False
        Me.lblCommission.IsUnique = False
        Me.lblCommission.Location = New System.Drawing.Point(248, 69)
        Me.lblCommission.MendatroryField = False
        Me.lblCommission.MyLinkLable1 = Nothing
        Me.lblCommission.MyLinkLable2 = Nothing
        Me.lblCommission.Name = "lblCommission"
        Me.lblCommission.ReferenceFieldDesc = Nothing
        Me.lblCommission.ReferenceFieldName = Nothing
        Me.lblCommission.ReferenceTableName = Nothing
        Me.lblCommission.Size = New System.Drawing.Size(325, 20)
        Me.lblCommission.TabIndex = 370
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(3, 93)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel1.TabIndex = 372
        Me.MyLabel1.Text = "Deduction"
        '
        'txtCommission
        '
        Me.txtCommission.CalculationExpression = Nothing
        Me.txtCommission.FieldCode = Nothing
        Me.txtCommission.FieldDesc = Nothing
        Me.txtCommission.FieldMaxLength = 0
        Me.txtCommission.FieldName = Nothing
        Me.txtCommission.isCalculatedField = False
        Me.txtCommission.IsSourceFromTable = False
        Me.txtCommission.IsSourceFromValueList = False
        Me.txtCommission.IsUnique = False
        Me.txtCommission.Location = New System.Drawing.Point(109, 70)
        Me.txtCommission.MendatroryField = True
        Me.txtCommission.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommission.MyLinkLable1 = Nothing
        Me.txtCommission.MyLinkLable2 = Nothing
        Me.txtCommission.MyReadOnly = False
        Me.txtCommission.MyShowMasterFormButton = False
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.ReferenceFieldDesc = Nothing
        Me.txtCommission.ReferenceFieldName = Nothing
        Me.txtCommission.ReferenceTableName = Nothing
        Me.txtCommission.Size = New System.Drawing.Size(135, 19)
        Me.txtCommission.TabIndex = 368
        Me.txtCommission.Value = ""
        '
        'lblUOM
        '
        Me.lblUOM.FieldName = Nothing
        Me.lblUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUOM.Location = New System.Drawing.Point(3, 71)
        Me.lblUOM.Name = "lblUOM"
        Me.lblUOM.Size = New System.Drawing.Size(68, 16)
        Me.lblUOM.TabIndex = 369
        Me.lblUOM.Text = "Commission"
        '
        'txtVSP
        '
        Me.txtVSP.arrDispalyMember = Nothing
        Me.txtVSP.arrValueMember = Nothing
        Me.txtVSP.Location = New System.Drawing.Point(109, 181)
        Me.txtVSP.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVSP.MyLinkLable1 = Nothing
        Me.txtVSP.MyLinkLable2 = Nothing
        Me.txtVSP.MyNullText = "Please Select"
        Me.txtVSP.Name = "txtVSP"
        Me.txtVSP.Size = New System.Drawing.Size(463, 20)
        Me.txtVSP.TabIndex = 366
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(109, 136)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Nothing
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "Please Select"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(463, 20)
        Me.txtMCC.TabIndex = 364
        '
        'MyLabel15
        '
        Me.MyLabel15.FieldName = Nothing
        Me.MyLabel15.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel15.Location = New System.Drawing.Point(3, 182)
        Me.MyLabel15.Name = "MyLabel15"
        Me.MyLabel15.Size = New System.Drawing.Size(26, 18)
        Me.MyLabel15.TabIndex = 367
        Me.MyLabel15.Text = "VSP"
        '
        'MyLabel14
        '
        Me.MyLabel14.FieldName = Nothing
        Me.MyLabel14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel14.Location = New System.Drawing.Point(3, 137)
        Me.MyLabel14.Name = "MyLabel14"
        Me.MyLabel14.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel14.TabIndex = 365
        Me.MyLabel14.Text = "MCC"
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(3, 27)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 6
        Me.lblDescription.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(109, 26)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(463, 20)
        Me.txtDescription.TabIndex = 1
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(422, 2)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(22, 21)
        Me.rdbtnreset.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(109, 2)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAdvanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 12
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(313, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.Value = ""
        '
        'lblAdvanceCode
        '
        Me.lblAdvanceCode.FieldName = Nothing
        Me.lblAdvanceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAdvanceCode.Location = New System.Drawing.Point(3, 3)
        Me.lblAdvanceCode.Name = "lblAdvanceCode"
        Me.lblAdvanceCode.Size = New System.Drawing.Size(33, 18)
        Me.lblAdvanceCode.TabIndex = 2
        Me.lblAdvanceCode.Text = "Code"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Location = New System.Drawing.Point(141, 3)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 21)
        Me.btnPost.TabIndex = 110
        Me.btnPost.Text = "Post"
        '
        'btnUpdates
        '
        Me.btnUpdates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdates.Location = New System.Drawing.Point(209, 3)
        Me.btnUpdates.Name = "btnUpdates"
        Me.btnUpdates.Size = New System.Drawing.Size(128, 21)
        Me.btnUpdates.TabIndex = 109
        Me.btnUpdates.Text = "Add More VSP"
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(5, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 0
        Me.btnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(720, 3)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 21)
        Me.rdbtnclose.TabIndex = 2
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(73, 3)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 21)
        Me.rdbtndelete.TabIndex = 1
        Me.rdbtndelete.Text = "Delete"
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblAdvanceCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkInactive)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtRoute)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpEndDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel14)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtStartDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtMCC)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtVSP)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDayWiseIncentive)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDayWiseIncentive)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCommission)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDeduction)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCommission)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDeduction)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(789, 398)
        Me.SplitContainer2.SplitterDistance = 206
        Me.SplitContainer2.TabIndex = 1082
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "VSP Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(789, 188)
        Me.RadGroupBox2.TabIndex = 1
        Me.RadGroupBox2.Text = "VSP Details"
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
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowGroupPanel = False
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(769, 158)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'frmVSPMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 432)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmVSPMapping"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "VSP Mapping"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDayWiseIncentive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCommission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpdates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents lblAdvanceCode As common.Controls.MyLabel
    Friend WithEvents rdbtnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtVSP As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel15 As common.Controls.MyLabel
    Friend WithEvents MyLabel14 As common.Controls.MyLabel
    Friend WithEvents lblDayWiseIncentive As common.Controls.MyTextBox
    Friend WithEvents txtDayWiseIncentive As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblDeduction As common.Controls.MyTextBox
    Friend WithEvents txtDeduction As common.UserControls.txtFinder
    Friend WithEvents lblCommission As common.Controls.MyTextBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtCommission As common.UserControls.txtFinder
    Friend WithEvents lblUOM As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents btnUpdates As RadButton
    Friend WithEvents chkInactive As RadCheckBox
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents btnPost As RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents RadGroupBox2 As RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class
