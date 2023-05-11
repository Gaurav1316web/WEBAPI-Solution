<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMilkGateEntryOut
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtGateOutWithoutMilkReceipt = New common.Controls.MyTextBox()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.chkGateOutWithoutMilkReceipt = New common.Controls.MyCheckBox()
        Me.txtRemarksOut = New common.Controls.MyTextBox()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtFilledCansOut = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.txtEmptyCansOut = New common.MyNumBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblNoOfCans = New common.Controls.MyLabel()
        Me.cboShift = New common.Controls.MyComboBox()
        Me.lblBOMStatus = New common.Controls.MyLabel()
        Me.lblTransporterName = New common.Controls.MyLabel()
        Me.txtFilledCans = New common.MyNumBox()
        Me.txtMCC = New common.UserControls.txtFinder()
        Me.lblMCCCode = New common.Controls.MyLabel()
        Me.lblMcc = New common.Controls.MyLabel()
        Me.Txtdds = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.lblTransporterCode = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtEmpryCans = New common.MyNumBox()
        Me.lblRoute = New common.Controls.MyLabel()
        Me.chkOther = New common.Controls.MyCheckBox()
        Me.txtShiftDate = New common.Controls.MyDateTimePicker()
        Me.txtRemarks = New common.Controls.MyTextBox()
        Me.txtRoute = New common.UserControls.txtFinder()
        Me.lblRouteCode = New common.Controls.MyLabel()
        Me.spltVehicle = New System.Windows.Forms.SplitContainer()
        Me.lblVehicleNo = New common.Controls.MyLabel()
        Me.txtVehicleNo = New common.Controls.MyTextBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtGWDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtGateEntryNo = New common.UserControls.txtFinder()
        Me.UsGrossWeight = New common.usLock()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblCode = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.BtnPost = New Telerik.WinControls.UI.RadButton()
        Me.btndelete = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtGateOutWithoutMilkReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGateOutWithoutMilkReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarksOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilledCansOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmptyCansOut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilledCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txtdds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransporterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmpryCans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtShiftDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spltVehicle.Panel1.SuspendLayout()
        Me.spltVehicle.Panel2.SuspendLayout()
        Me.spltVehicle.SuspendLayout()
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGWDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGateOutWithoutMilkReceipt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel9)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkGateOutWithoutMilkReceipt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRemarksOut)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFilledCansOut)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtEmptyCansOut)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGWDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtGateEntryNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsGrossWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(922, 332)
        Me.SplitContainer1.SplitterDistance = 293
        Me.SplitContainer1.TabIndex = 0
        '
        'txtGateOutWithoutMilkReceipt
        '
        Me.txtGateOutWithoutMilkReceipt.CalculationExpression = Nothing
        Me.txtGateOutWithoutMilkReceipt.FieldCode = Nothing
        Me.txtGateOutWithoutMilkReceipt.FieldDesc = Nothing
        Me.txtGateOutWithoutMilkReceipt.FieldMaxLength = 0
        Me.txtGateOutWithoutMilkReceipt.FieldName = Nothing
        Me.txtGateOutWithoutMilkReceipt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateOutWithoutMilkReceipt.isCalculatedField = False
        Me.txtGateOutWithoutMilkReceipt.IsSourceFromTable = False
        Me.txtGateOutWithoutMilkReceipt.IsSourceFromValueList = False
        Me.txtGateOutWithoutMilkReceipt.IsUnique = False
        Me.txtGateOutWithoutMilkReceipt.Location = New System.Drawing.Point(98, 162)
        Me.txtGateOutWithoutMilkReceipt.MaxLength = 200
        Me.txtGateOutWithoutMilkReceipt.MendatroryField = False
        Me.txtGateOutWithoutMilkReceipt.MyLinkLable1 = Me.RadLabel6
        Me.txtGateOutWithoutMilkReceipt.MyLinkLable2 = Nothing
        Me.txtGateOutWithoutMilkReceipt.Name = "txtGateOutWithoutMilkReceipt"
        Me.txtGateOutWithoutMilkReceipt.ReferenceFieldDesc = Nothing
        Me.txtGateOutWithoutMilkReceipt.ReferenceFieldName = Nothing
        Me.txtGateOutWithoutMilkReceipt.ReferenceTableName = Nothing
        Me.txtGateOutWithoutMilkReceipt.Size = New System.Drawing.Size(253, 18)
        Me.txtGateOutWithoutMilkReceipt.TabIndex = 1079
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(5, 88)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel6.TabIndex = 1041
        Me.RadLabel6.Text = "Vehicle No"
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(7, 163)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(48, 16)
        Me.MyLabel9.TabIndex = 1080
        Me.MyLabel9.Text = "Reason "
        '
        'chkGateOutWithoutMilkReceipt
        '
        Me.chkGateOutWithoutMilkReceipt.Location = New System.Drawing.Point(98, 143)
        Me.chkGateOutWithoutMilkReceipt.MyLinkLable1 = Nothing
        Me.chkGateOutWithoutMilkReceipt.MyLinkLable2 = Nothing
        Me.chkGateOutWithoutMilkReceipt.Name = "chkGateOutWithoutMilkReceipt"
        Me.chkGateOutWithoutMilkReceipt.Size = New System.Drawing.Size(169, 18)
        Me.chkGateOutWithoutMilkReceipt.TabIndex = 1078
        Me.chkGateOutWithoutMilkReceipt.Tag1 = Nothing
        Me.chkGateOutWithoutMilkReceipt.Text = "Gateout Without Milk Receipt"
        '
        'txtRemarksOut
        '
        Me.txtRemarksOut.CalculationExpression = Nothing
        Me.txtRemarksOut.FieldCode = Nothing
        Me.txtRemarksOut.FieldDesc = Nothing
        Me.txtRemarksOut.FieldMaxLength = 0
        Me.txtRemarksOut.FieldName = Nothing
        Me.txtRemarksOut.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarksOut.isCalculatedField = False
        Me.txtRemarksOut.IsSourceFromTable = False
        Me.txtRemarksOut.IsSourceFromValueList = False
        Me.txtRemarksOut.IsUnique = False
        Me.txtRemarksOut.Location = New System.Drawing.Point(98, 123)
        Me.txtRemarksOut.MaxLength = 200
        Me.txtRemarksOut.MendatroryField = False
        Me.txtRemarksOut.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarksOut.MyLinkLable2 = Nothing
        Me.txtRemarksOut.Name = "txtRemarksOut"
        Me.txtRemarksOut.ReferenceFieldDesc = Nothing
        Me.txtRemarksOut.ReferenceFieldName = Nothing
        Me.txtRemarksOut.ReferenceTableName = Nothing
        Me.txtRemarksOut.Size = New System.Drawing.Size(253, 18)
        Me.txtRemarksOut.TabIndex = 1076
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel7.Location = New System.Drawing.Point(7, 124)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel7.TabIndex = 1077
        Me.MyLabel7.Text = "Remarks"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel5.Location = New System.Drawing.Point(7, 101)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(63, 16)
        Me.MyLabel5.TabIndex = 1073
        Me.MyLabel5.Text = "Filled Cans"
        '
        'txtFilledCansOut
        '
        Me.txtFilledCansOut.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFilledCansOut.CalculationExpression = Nothing
        Me.txtFilledCansOut.DecimalPlaces = 0
        Me.txtFilledCansOut.FieldCode = Nothing
        Me.txtFilledCansOut.FieldDesc = Nothing
        Me.txtFilledCansOut.FieldMaxLength = 0
        Me.txtFilledCansOut.FieldName = Nothing
        Me.txtFilledCansOut.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtFilledCansOut.isCalculatedField = False
        Me.txtFilledCansOut.IsSourceFromTable = False
        Me.txtFilledCansOut.IsSourceFromValueList = False
        Me.txtFilledCansOut.IsUnique = False
        Me.txtFilledCansOut.Location = New System.Drawing.Point(98, 99)
        Me.txtFilledCansOut.MaxLength = 5
        Me.txtFilledCansOut.MendatroryField = False
        Me.txtFilledCansOut.MyLinkLable1 = Me.MyLabel5
        Me.txtFilledCansOut.MyLinkLable2 = Nothing
        Me.txtFilledCansOut.Name = "txtFilledCansOut"
        Me.txtFilledCansOut.ReferenceFieldDesc = Nothing
        Me.txtFilledCansOut.ReferenceFieldName = Nothing
        Me.txtFilledCansOut.ReferenceTableName = Nothing
        Me.txtFilledCansOut.Size = New System.Drawing.Size(159, 21)
        Me.txtFilledCansOut.TabIndex = 1072
        Me.txtFilledCansOut.Text = "0"
        Me.txtFilledCansOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFilledCansOut.Value = 0.0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel6.Location = New System.Drawing.Point(7, 77)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel6.TabIndex = 1075
        Me.MyLabel6.Text = "Empty Cans"
        '
        'txtEmptyCansOut
        '
        Me.txtEmptyCansOut.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtEmptyCansOut.CalculationExpression = Nothing
        Me.txtEmptyCansOut.DecimalPlaces = 0
        Me.txtEmptyCansOut.FieldCode = Nothing
        Me.txtEmptyCansOut.FieldDesc = Nothing
        Me.txtEmptyCansOut.FieldMaxLength = 0
        Me.txtEmptyCansOut.FieldName = Nothing
        Me.txtEmptyCansOut.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtEmptyCansOut.isCalculatedField = False
        Me.txtEmptyCansOut.IsSourceFromTable = False
        Me.txtEmptyCansOut.IsSourceFromValueList = False
        Me.txtEmptyCansOut.IsUnique = False
        Me.txtEmptyCansOut.Location = New System.Drawing.Point(98, 75)
        Me.txtEmptyCansOut.MaxLength = 5
        Me.txtEmptyCansOut.MendatroryField = True
        Me.txtEmptyCansOut.MyLinkLable1 = Me.MyLabel6
        Me.txtEmptyCansOut.MyLinkLable2 = Nothing
        Me.txtEmptyCansOut.Name = "txtEmptyCansOut"
        Me.txtEmptyCansOut.ReferenceFieldDesc = Nothing
        Me.txtEmptyCansOut.ReferenceFieldName = Nothing
        Me.txtEmptyCansOut.ReferenceTableName = Nothing
        Me.txtEmptyCansOut.Size = New System.Drawing.Size(159, 21)
        Me.txtEmptyCansOut.TabIndex = 1074
        Me.txtEmptyCansOut.Text = "0"
        Me.txtEmptyCansOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEmptyCansOut.Value = 0.0R
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.txtDate)
        Me.RadGroupBox1.Controls.Add(Me.lblNoOfCans)
        Me.RadGroupBox1.Controls.Add(Me.cboShift)
        Me.RadGroupBox1.Controls.Add(Me.lblTransporterName)
        Me.RadGroupBox1.Controls.Add(Me.lblBOMStatus)
        Me.RadGroupBox1.Controls.Add(Me.txtFilledCans)
        Me.RadGroupBox1.Controls.Add(Me.txtMCC)
        Me.RadGroupBox1.Controls.Add(Me.Txtdds)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.lblMcc)
        Me.RadGroupBox1.Controls.Add(Me.lblTransporterCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.txtEmpryCans)
        Me.RadGroupBox1.Controls.Add(Me.lblRoute)
        Me.RadGroupBox1.Controls.Add(Me.chkOther)
        Me.RadGroupBox1.Controls.Add(Me.txtShiftDate)
        Me.RadGroupBox1.Controls.Add(Me.txtRemarks)
        Me.RadGroupBox1.Controls.Add(Me.txtRoute)
        Me.RadGroupBox1.Controls.Add(Me.spltVehicle)
        Me.RadGroupBox1.Controls.Add(Me.lblRouteCode)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.HeaderText = "Gate Entry Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(357, 9)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(546, 201)
        Me.RadGroupBox1.TabIndex = 1071
        Me.RadGroupBox1.Text = "Gate Entry Details"
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(5, 18)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(87, 16)
        Me.lblDocDate.TabIndex = 1028
        Me.lblDocDate.Text = "Gate Entry Date"
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(96, 17)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDocDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(159, 18)
        Me.txtDate.TabIndex = 1027
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'lblNoOfCans
        '
        Me.lblNoOfCans.FieldName = Nothing
        Me.lblNoOfCans.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblNoOfCans.Location = New System.Drawing.Point(5, 131)
        Me.lblNoOfCans.Name = "lblNoOfCans"
        Me.lblNoOfCans.Size = New System.Drawing.Size(63, 16)
        Me.lblNoOfCans.TabIndex = 1048
        Me.lblNoOfCans.Text = "Filled Cans"
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
        RadListDataItem1.Text = "M"
        RadListDataItem2.Text = "E"
        Me.cboShift.Items.Add(RadListDataItem1)
        Me.cboShift.Items.Add(RadListDataItem2)
        Me.cboShift.Location = New System.Drawing.Point(447, 17)
        Me.cboShift.MendatroryField = True
        Me.cboShift.MyLinkLable1 = Me.lblBOMStatus
        Me.cboShift.MyLinkLable2 = Nothing
        Me.cboShift.Name = "cboShift"
        Me.cboShift.ReferenceFieldDesc = Nothing
        Me.cboShift.ReferenceFieldName = Nothing
        Me.cboShift.ReferenceTableName = Nothing
        Me.cboShift.Size = New System.Drawing.Size(83, 18)
        Me.cboShift.TabIndex = 1029
        '
        'lblBOMStatus
        '
        Me.lblBOMStatus.FieldName = Nothing
        Me.lblBOMStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBOMStatus.Location = New System.Drawing.Point(405, 18)
        Me.lblBOMStatus.Name = "lblBOMStatus"
        Me.lblBOMStatus.Size = New System.Drawing.Size(29, 16)
        Me.lblBOMStatus.TabIndex = 1030
        Me.lblBOMStatus.Text = "Shift"
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = False
        Me.lblTransporterName.BorderVisible = True
        Me.lblTransporterName.FieldName = Nothing
        Me.lblTransporterName.Location = New System.Drawing.Point(260, 107)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(270, 21)
        Me.lblTransporterName.TabIndex = 1046
        Me.lblTransporterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFilledCans
        '
        Me.txtFilledCans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFilledCans.CalculationExpression = Nothing
        Me.txtFilledCans.DecimalPlaces = 0
        Me.txtFilledCans.FieldCode = Nothing
        Me.txtFilledCans.FieldDesc = Nothing
        Me.txtFilledCans.FieldMaxLength = 0
        Me.txtFilledCans.FieldName = Nothing
        Me.txtFilledCans.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtFilledCans.isCalculatedField = False
        Me.txtFilledCans.IsSourceFromTable = False
        Me.txtFilledCans.IsSourceFromValueList = False
        Me.txtFilledCans.IsUnique = False
        Me.txtFilledCans.Location = New System.Drawing.Point(96, 129)
        Me.txtFilledCans.MaxLength = 5
        Me.txtFilledCans.MendatroryField = True
        Me.txtFilledCans.MyLinkLable1 = Me.lblNoOfCans
        Me.txtFilledCans.MyLinkLable2 = Nothing
        Me.txtFilledCans.Name = "txtFilledCans"
        Me.txtFilledCans.ReferenceFieldDesc = Nothing
        Me.txtFilledCans.ReferenceFieldName = Nothing
        Me.txtFilledCans.ReferenceTableName = Nothing
        Me.txtFilledCans.Size = New System.Drawing.Size(159, 21)
        Me.txtFilledCans.TabIndex = 1047
        Me.txtFilledCans.Text = "0"
        Me.txtFilledCans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFilledCans.Value = 0.0R
        '
        'txtMCC
        '
        Me.txtMCC.CalculationExpression = Nothing
        Me.txtMCC.FieldCode = Nothing
        Me.txtMCC.FieldDesc = Nothing
        Me.txtMCC.FieldMaxLength = 0
        Me.txtMCC.FieldName = Nothing
        Me.txtMCC.isCalculatedField = False
        Me.txtMCC.IsSourceFromTable = False
        Me.txtMCC.IsSourceFromValueList = False
        Me.txtMCC.IsUnique = False
        Me.txtMCC.Location = New System.Drawing.Point(96, 38)
        Me.txtMCC.MendatroryField = True
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.lblMCCCode
        Me.txtMCC.MyLinkLable2 = Me.lblMcc
        Me.txtMCC.MyReadOnly = False
        Me.txtMCC.MyShowMasterFormButton = False
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.ReferenceFieldDesc = Nothing
        Me.txtMCC.ReferenceFieldName = Nothing
        Me.txtMCC.ReferenceTableName = Nothing
        Me.txtMCC.Size = New System.Drawing.Size(159, 20)
        Me.txtMCC.TabIndex = 1033
        Me.txtMCC.Value = ""
        '
        'lblMCCCode
        '
        Me.lblMCCCode.FieldName = Nothing
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(5, 39)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(60, 18)
        Me.lblMCCCode.TabIndex = 1034
        Me.lblMCCCode.Text = "MCC Code"
        '
        'lblMcc
        '
        Me.lblMcc.AutoSize = False
        Me.lblMcc.BorderVisible = True
        Me.lblMcc.FieldName = Nothing
        Me.lblMcc.Location = New System.Drawing.Point(260, 38)
        Me.lblMcc.Name = "lblMcc"
        Me.lblMcc.Size = New System.Drawing.Size(270, 21)
        Me.lblMcc.TabIndex = 1035
        Me.lblMcc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Txtdds
        '
        Me.Txtdds.FieldName = Nothing
        Me.Txtdds.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Txtdds.Location = New System.Drawing.Point(5, 108)
        Me.Txtdds.Name = "Txtdds"
        Me.Txtdds.Size = New System.Drawing.Size(64, 18)
        Me.Txtdds.TabIndex = 1044
        Me.Txtdds.Text = "Transporter"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(5, 154)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(68, 16)
        Me.MyLabel1.TabIndex = 1050
        Me.MyLabel1.Text = "Empty Cans"
        '
        'lblTransporterCode
        '
        Me.lblTransporterCode.AutoSize = False
        Me.lblTransporterCode.BorderVisible = True
        Me.lblTransporterCode.FieldName = Nothing
        Me.lblTransporterCode.Location = New System.Drawing.Point(96, 107)
        Me.lblTransporterCode.Name = "lblTransporterCode"
        Me.lblTransporterCode.Size = New System.Drawing.Size(159, 21)
        Me.lblTransporterCode.TabIndex = 1045
        Me.lblTransporterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(259, 18)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(56, 16)
        Me.MyLabel4.TabIndex = 1056
        Me.MyLabel4.Text = "Shift Date"
        '
        'txtEmpryCans
        '
        Me.txtEmpryCans.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtEmpryCans.CalculationExpression = Nothing
        Me.txtEmpryCans.DecimalPlaces = 0
        Me.txtEmpryCans.FieldCode = Nothing
        Me.txtEmpryCans.FieldDesc = Nothing
        Me.txtEmpryCans.FieldMaxLength = 0
        Me.txtEmpryCans.FieldName = Nothing
        Me.txtEmpryCans.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtEmpryCans.isCalculatedField = False
        Me.txtEmpryCans.IsSourceFromTable = False
        Me.txtEmpryCans.IsSourceFromValueList = False
        Me.txtEmpryCans.IsUnique = False
        Me.txtEmpryCans.Location = New System.Drawing.Point(96, 152)
        Me.txtEmpryCans.MaxLength = 5
        Me.txtEmpryCans.MendatroryField = False
        Me.txtEmpryCans.MyLinkLable1 = Me.MyLabel1
        Me.txtEmpryCans.MyLinkLable2 = Nothing
        Me.txtEmpryCans.Name = "txtEmpryCans"
        Me.txtEmpryCans.ReferenceFieldDesc = Nothing
        Me.txtEmpryCans.ReferenceFieldName = Nothing
        Me.txtEmpryCans.ReferenceTableName = Nothing
        Me.txtEmpryCans.Size = New System.Drawing.Size(159, 21)
        Me.txtEmpryCans.TabIndex = 1049
        Me.txtEmpryCans.Text = "0"
        Me.txtEmpryCans.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtEmpryCans.Value = 0.0R
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = False
        Me.lblRoute.BorderVisible = True
        Me.lblRoute.FieldName = Nothing
        Me.lblRoute.Location = New System.Drawing.Point(260, 60)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(270, 21)
        Me.lblRoute.TabIndex = 1038
        Me.lblRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkOther
        '
        Me.chkOther.Location = New System.Drawing.Point(260, 87)
        Me.chkOther.MyLinkLable1 = Nothing
        Me.chkOther.MyLinkLable2 = Nothing
        Me.chkOther.Name = "chkOther"
        Me.chkOther.Size = New System.Drawing.Size(88, 18)
        Me.chkOther.TabIndex = 1043
        Me.chkOther.Tag1 = Nothing
        Me.chkOther.Text = "Other Vehicle"
        '
        'txtShiftDate
        '
        Me.txtShiftDate.CalculationExpression = Nothing
        Me.txtShiftDate.CustomFormat = "dd/MM/yyyy"
        Me.txtShiftDate.FieldCode = Nothing
        Me.txtShiftDate.FieldDesc = Nothing
        Me.txtShiftDate.FieldMaxLength = 0
        Me.txtShiftDate.FieldName = Nothing
        Me.txtShiftDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtShiftDate.isCalculatedField = False
        Me.txtShiftDate.IsSourceFromTable = False
        Me.txtShiftDate.IsSourceFromValueList = False
        Me.txtShiftDate.IsUnique = False
        Me.txtShiftDate.Location = New System.Drawing.Point(319, 17)
        Me.txtShiftDate.MendatroryField = True
        Me.txtShiftDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftDate.MyLinkLable1 = Me.MyLabel4
        Me.txtShiftDate.MyLinkLable2 = Nothing
        Me.txtShiftDate.Name = "txtShiftDate"
        Me.txtShiftDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtShiftDate.ReferenceFieldDesc = Nothing
        Me.txtShiftDate.ReferenceFieldName = Nothing
        Me.txtShiftDate.ReferenceTableName = Nothing
        Me.txtShiftDate.Size = New System.Drawing.Size(82, 18)
        Me.txtShiftDate.TabIndex = 1055
        Me.txtShiftDate.TabStop = False
        Me.txtShiftDate.Text = "03/05/2011"
        Me.txtShiftDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'txtRemarks
        '
        Me.txtRemarks.CalculationExpression = Nothing
        Me.txtRemarks.FieldCode = Nothing
        Me.txtRemarks.FieldDesc = Nothing
        Me.txtRemarks.FieldMaxLength = 0
        Me.txtRemarks.FieldName = Nothing
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.isCalculatedField = False
        Me.txtRemarks.IsSourceFromTable = False
        Me.txtRemarks.IsSourceFromValueList = False
        Me.txtRemarks.IsUnique = False
        Me.txtRemarks.Location = New System.Drawing.Point(96, 174)
        Me.txtRemarks.MaxLength = 200
        Me.txtRemarks.MendatroryField = False
        Me.txtRemarks.MyLinkLable1 = Me.RadLabel6
        Me.txtRemarks.MyLinkLable2 = Nothing
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReferenceFieldDesc = Nothing
        Me.txtRemarks.ReferenceFieldName = Nothing
        Me.txtRemarks.ReferenceTableName = Nothing
        Me.txtRemarks.Size = New System.Drawing.Size(434, 18)
        Me.txtRemarks.TabIndex = 1051
        '
        'txtRoute
        '
        Me.txtRoute.CalculationExpression = Nothing
        Me.txtRoute.FieldCode = Nothing
        Me.txtRoute.FieldDesc = Nothing
        Me.txtRoute.FieldMaxLength = 0
        Me.txtRoute.FieldName = Nothing
        Me.txtRoute.isCalculatedField = False
        Me.txtRoute.IsSourceFromTable = False
        Me.txtRoute.IsSourceFromValueList = False
        Me.txtRoute.IsUnique = False
        Me.txtRoute.Location = New System.Drawing.Point(96, 60)
        Me.txtRoute.MendatroryField = True
        Me.txtRoute.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.MyLinkLable1 = Me.lblRouteCode
        Me.txtRoute.MyLinkLable2 = Nothing
        Me.txtRoute.MyReadOnly = False
        Me.txtRoute.MyShowMasterFormButton = False
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.ReferenceFieldDesc = Nothing
        Me.txtRoute.ReferenceFieldName = Nothing
        Me.txtRoute.ReferenceTableName = Nothing
        Me.txtRoute.Size = New System.Drawing.Size(159, 21)
        Me.txtRoute.TabIndex = 1036
        Me.txtRoute.Value = ""
        '
        'lblRouteCode
        '
        Me.lblRouteCode.FieldName = Nothing
        Me.lblRouteCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblRouteCode.Location = New System.Drawing.Point(5, 61)
        Me.lblRouteCode.Name = "lblRouteCode"
        Me.lblRouteCode.Size = New System.Drawing.Size(65, 18)
        Me.lblRouteCode.TabIndex = 1037
        Me.lblRouteCode.Text = "Route Code"
        '
        'spltVehicle
        '
        Me.spltVehicle.Location = New System.Drawing.Point(96, 85)
        Me.spltVehicle.Name = "spltVehicle"
        '
        'spltVehicle.Panel1
        '
        Me.spltVehicle.Panel1.Controls.Add(Me.lblVehicleNo)
        '
        'spltVehicle.Panel2
        '
        Me.spltVehicle.Panel2.Controls.Add(Me.txtVehicleNo)
        Me.spltVehicle.Size = New System.Drawing.Size(159, 22)
        Me.spltVehicle.SplitterDistance = 53
        Me.spltVehicle.TabIndex = 1042
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = False
        Me.lblVehicleNo.BorderVisible = True
        Me.lblVehicleNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVehicleNo.FieldName = Nothing
        Me.lblVehicleNo.Location = New System.Drawing.Point(0, 0)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(53, 22)
        Me.lblVehicleNo.TabIndex = 34
        Me.lblVehicleNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.CalculationExpression = Nothing
        Me.txtVehicleNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtVehicleNo.FieldCode = Nothing
        Me.txtVehicleNo.FieldDesc = Nothing
        Me.txtVehicleNo.FieldMaxLength = 0
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.isCalculatedField = False
        Me.txtVehicleNo.IsSourceFromTable = False
        Me.txtVehicleNo.IsSourceFromValueList = False
        Me.txtVehicleNo.IsUnique = False
        Me.txtVehicleNo.Location = New System.Drawing.Point(0, 0)
        Me.txtVehicleNo.MaxLength = 200
        Me.txtVehicleNo.MendatroryField = True
        Me.txtVehicleNo.MyLinkLable1 = Me.RadLabel6
        Me.txtVehicleNo.MyLinkLable2 = Nothing
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.ReferenceFieldDesc = Nothing
        Me.txtVehicleNo.ReferenceFieldName = Nothing
        Me.txtVehicleNo.ReferenceTableName = Nothing
        Me.txtVehicleNo.Size = New System.Drawing.Size(102, 22)
        Me.txtVehicleNo.TabIndex = 1040
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(5, 175)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(51, 16)
        Me.MyLabel2.TabIndex = 1052
        Me.MyLabel2.Text = "Remarks"
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(7, 32)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(74, 16)
        Me.MyLabel8.TabIndex = 1065
        Me.MyLabel8.Text = "Gateout Date"
        '
        'txtGWDate
        '
        Me.txtGWDate.CalculationExpression = Nothing
        Me.txtGWDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.txtGWDate.FieldCode = Nothing
        Me.txtGWDate.FieldDesc = Nothing
        Me.txtGWDate.FieldMaxLength = 0
        Me.txtGWDate.FieldName = Nothing
        Me.txtGWDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGWDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtGWDate.isCalculatedField = False
        Me.txtGWDate.IsSourceFromTable = False
        Me.txtGWDate.IsSourceFromValueList = False
        Me.txtGWDate.IsUnique = False
        Me.txtGWDate.Location = New System.Drawing.Point(98, 31)
        Me.txtGWDate.MendatroryField = True
        Me.txtGWDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGWDate.MyLinkLable1 = Me.MyLabel8
        Me.txtGWDate.MyLinkLable2 = Nothing
        Me.txtGWDate.Name = "txtGWDate"
        Me.txtGWDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtGWDate.ReferenceFieldDesc = Nothing
        Me.txtGWDate.ReferenceFieldName = Nothing
        Me.txtGWDate.ReferenceTableName = Nothing
        Me.txtGWDate.Size = New System.Drawing.Size(146, 18)
        Me.txtGWDate.TabIndex = 1064
        Me.txtGWDate.TabStop = False
        Me.txtGWDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtGWDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(7, 53)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(76, 18)
        Me.MyLabel3.TabIndex = 1054
        Me.MyLabel3.Text = "Gate Entry No"
        '
        'txtGateEntryNo
        '
        Me.txtGateEntryNo.CalculationExpression = Nothing
        Me.txtGateEntryNo.FieldCode = Nothing
        Me.txtGateEntryNo.FieldDesc = Nothing
        Me.txtGateEntryNo.FieldMaxLength = 0
        Me.txtGateEntryNo.FieldName = Nothing
        Me.txtGateEntryNo.isCalculatedField = False
        Me.txtGateEntryNo.IsSourceFromTable = False
        Me.txtGateEntryNo.IsSourceFromValueList = False
        Me.txtGateEntryNo.IsUnique = False
        Me.txtGateEntryNo.Location = New System.Drawing.Point(98, 52)
        Me.txtGateEntryNo.MendatroryField = True
        Me.txtGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateEntryNo.MyLinkLable1 = Me.MyLabel3
        Me.txtGateEntryNo.MyLinkLable2 = Me.lblMcc
        Me.txtGateEntryNo.MyReadOnly = False
        Me.txtGateEntryNo.MyShowMasterFormButton = False
        Me.txtGateEntryNo.Name = "txtGateEntryNo"
        Me.txtGateEntryNo.ReferenceFieldDesc = Nothing
        Me.txtGateEntryNo.ReferenceFieldName = Nothing
        Me.txtGateEntryNo.ReferenceTableName = Nothing
        Me.txtGateEntryNo.Size = New System.Drawing.Size(251, 20)
        Me.txtGateEntryNo.TabIndex = 1053
        Me.txtGateEntryNo.Value = ""
        '
        'UsGrossWeight
        '
        Me.UsGrossWeight.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsGrossWeight.Location = New System.Drawing.Point(251, 30)
        Me.UsGrossWeight.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsGrossWeight.Name = "UsGrossWeight"
        Me.UsGrossWeight.Size = New System.Drawing.Size(98, 21)
        Me.UsGrossWeight.Status = common.ERPTransactionStatus.Pending
        Me.UsGrossWeight.TabIndex = 1039
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(98, 7)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(234, 21)
        Me.txtCode.TabIndex = 1032
        Me.txtCode.Value = ""
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(7, 9)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(64, 16)
        Me.lblCode.TabIndex = 1026
        Me.lblCode.Text = "Gateout No"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(332, 7)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 21)
        Me.btnnew.TabIndex = 1031
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(215, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(66, 21)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "Print"
        Me.btnPrint.Visible = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(851, 7)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 6
        Me.btnclose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 21)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.Visible = False
        '
        'BtnPost
        '
        Me.BtnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPost.Location = New System.Drawing.Point(143, 7)
        Me.BtnPost.Name = "BtnPost"
        Me.BtnPost.Size = New System.Drawing.Size(66, 21)
        Me.BtnPost.TabIndex = 4
        Me.BtnPost.Text = "Post"
        Me.BtnPost.Visible = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(74, 7)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(66, 21)
        Me.btndelete.TabIndex = 3
        Me.btndelete.Text = "Delete"
        '
        'frmMilkGateEntryOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 332)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMilkGateEntryOut"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Milk Gate Entry Out"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtGateOutWithoutMilkReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGateOutWithoutMilkReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarksOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilledCansOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmptyCansOut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBOMStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilledCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txtdds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransporterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmpryCans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkOther, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtShiftDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRouteCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spltVehicle.Panel1.ResumeLayout(False)
        Me.spltVehicle.Panel2.ResumeLayout(False)
        Me.spltVehicle.Panel2.PerformLayout()
        Me.spltVehicle.ResumeLayout(False)
        CType(Me.lblVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGWDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents BtnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblBOMStatus As common.Controls.MyLabel
    Friend WithEvents cboShift As common.Controls.MyComboBox
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents lblMcc As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtFinder
    Friend WithEvents lblRouteCode As common.Controls.MyLabel
    Friend WithEvents txtRoute As common.UserControls.txtFinder
    Friend WithEvents lblRoute As common.Controls.MyLabel
    Friend WithEvents UsGrossWeight As common.usLock
    Friend WithEvents spltVehicle As System.Windows.Forms.SplitContainer
    Friend WithEvents txtVehicleNo As common.Controls.MyTextBox
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents lblVehicleNo As common.Controls.MyLabel
    Friend WithEvents chkOther As common.Controls.MyCheckBox
    Friend WithEvents lblTransporterName As common.Controls.MyLabel
    Friend WithEvents Txtdds As common.Controls.MyLabel
    Friend WithEvents lblTransporterCode As common.Controls.MyLabel
    Friend WithEvents txtEmpryCans As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFilledCans As common.MyNumBox
    Friend WithEvents lblNoOfCans As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRemarks As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtShiftDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtGWDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtRemarksOut As common.Controls.MyTextBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtFilledCansOut As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtEmptyCansOut As common.MyNumBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtGateOutWithoutMilkReceipt As common.Controls.MyTextBox
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents chkGateOutWithoutMilkReceipt As common.Controls.MyCheckBox
End Class
