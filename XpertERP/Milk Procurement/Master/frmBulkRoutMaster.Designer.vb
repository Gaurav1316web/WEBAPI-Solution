<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBulkRoutMaster
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.rdmenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.File = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtScheduleTimeE = New common.Controls.MyDateTimePicker()
        Me.txtScheduleTimeM = New common.Controls.MyDateTimePicker()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.MyLabel11 = New common.Controls.MyLabel()
        Me.CuttOffTime = New common.Controls.MyLabel()
        Me.txtcuttofftime = New common.Controls.MyDateTimePicker()
        Me.txtVehicleNo = New common.Controls.MyLabel()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtTankerNo = New common.UserControls.txtFinder()
        Me.chkDefault = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtRouteNameHindi = New common.Controls.MyTextBox()
        Me.MyLabel19 = New common.Controls.MyLabel()
        Me.txtTollAmount = New common.MyNumBox()
        Me.MyLabel6 = New common.Controls.MyLabel()
        Me.lblToLocationName = New common.Controls.MyLabel()
        Me.txtToLocationCode = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.lblToLocation = New common.Controls.MyLabel()
        Me.chkForContractor = New Telerik.WinControls.UI.RadCheckBox()
        Me.txtMCC = New common.UserControls.txtMultiSelectFinder()
        Me.MyLabel16 = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.txtWeight = New common.MyNumBox()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.lblAmout = New common.Controls.MyLabel()
        Me.txtRouteNo = New common.UserControls.txtNavigator()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.btnNew = New Telerik.WinControls.UI.RadButton()
        Me.txtRate = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtDistance = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtRouteName = New common.Controls.MyTextBox()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton()
        Me.rdbtndelete = New Telerik.WinControls.UI.RadButton()
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.txtScheduleTimeE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtScheduleTimeM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CuttOffTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcuttofftime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDefault, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteNameHindi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkForContractor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRouteName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdmenu1
        '
        Me.rdmenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.File})
        Me.rdmenu1.Location = New System.Drawing.Point(0, 0)
        Me.rdmenu1.Name = "rdmenu1"
        Me.rdmenu1.Size = New System.Drawing.Size(714, 20)
        Me.rdmenu1.TabIndex = 2
        '
        'File
        '
        Me.File.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1, Me.RadMenuItem2, Me.RadMenuItem3})
        Me.File.Name = "File"
        Me.File.Text = "File"
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "Import"
        Me.RadMenuItem1.AccessibleName = "Import"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Export"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "Export"
        Me.RadMenuItem2.AccessibleName = "Export"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Import"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Exit"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtScheduleTimeE)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtScheduleTimeM)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel11)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CuttOffTime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtcuttofftime)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtVehicleNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTankerNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkDefault)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRouteNameHindi)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtTollAmount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToLocationName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToLocationCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToLocation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkForContractor)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtMCC)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel16)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtWeight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAmout)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRouteNo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnNew)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDistance)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel19)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRouteName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtndelete)
        Me.SplitContainer1.Size = New System.Drawing.Size(714, 335)
        Me.SplitContainer1.SplitterDistance = 304
        Me.SplitContainer1.TabIndex = 3
        '
        'txtScheduleTimeE
        '
        Me.txtScheduleTimeE.CalculationExpression = Nothing
        Me.txtScheduleTimeE.CustomFormat = "hh:mm tt"
        Me.txtScheduleTimeE.FieldCode = Nothing
        Me.txtScheduleTimeE.FieldDesc = Nothing
        Me.txtScheduleTimeE.FieldMaxLength = 0
        Me.txtScheduleTimeE.FieldName = Nothing
        Me.txtScheduleTimeE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleTimeE.isCalculatedField = False
        Me.txtScheduleTimeE.IsSourceFromTable = False
        Me.txtScheduleTimeE.IsSourceFromValueList = False
        Me.txtScheduleTimeE.IsUnique = False
        Me.txtScheduleTimeE.Location = New System.Drawing.Point(150, 260)
        Me.txtScheduleTimeE.MendatroryField = False
        Me.txtScheduleTimeE.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleTimeE.MyLinkLable1 = Nothing
        Me.txtScheduleTimeE.MyLinkLable2 = Nothing
        Me.txtScheduleTimeE.Name = "txtScheduleTimeE"
        Me.txtScheduleTimeE.ReferenceFieldDesc = Nothing
        Me.txtScheduleTimeE.ReferenceFieldName = Nothing
        Me.txtScheduleTimeE.ReferenceTableName = Nothing
        Me.txtScheduleTimeE.Size = New System.Drawing.Size(153, 20)
        Me.txtScheduleTimeE.TabIndex = 12151
        Me.txtScheduleTimeE.TabStop = False
        Me.txtScheduleTimeE.Value = New Date(CType(0, Long))
        '
        'txtScheduleTimeM
        '
        Me.txtScheduleTimeM.CalculationExpression = Nothing
        Me.txtScheduleTimeM.CustomFormat = "hh:mm tt"
        Me.txtScheduleTimeM.FieldCode = Nothing
        Me.txtScheduleTimeM.FieldDesc = Nothing
        Me.txtScheduleTimeM.FieldMaxLength = 0
        Me.txtScheduleTimeM.FieldName = Nothing
        Me.txtScheduleTimeM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtScheduleTimeM.isCalculatedField = False
        Me.txtScheduleTimeM.IsSourceFromTable = False
        Me.txtScheduleTimeM.IsSourceFromValueList = False
        Me.txtScheduleTimeM.IsUnique = False
        Me.txtScheduleTimeM.Location = New System.Drawing.Point(150, 235)
        Me.txtScheduleTimeM.MendatroryField = False
        Me.txtScheduleTimeM.MinDate = New Date(1973, 1, 1, 0, 0, 0, 0)
        Me.txtScheduleTimeM.MyLinkLable1 = Nothing
        Me.txtScheduleTimeM.MyLinkLable2 = Nothing
        Me.txtScheduleTimeM.Name = "txtScheduleTimeM"
        Me.txtScheduleTimeM.ReferenceFieldDesc = Nothing
        Me.txtScheduleTimeM.ReferenceFieldName = Nothing
        Me.txtScheduleTimeM.ReferenceTableName = Nothing
        Me.txtScheduleTimeM.Size = New System.Drawing.Size(154, 20)
        Me.txtScheduleTimeM.TabIndex = 12150
        Me.txtScheduleTimeM.TabStop = False
        Me.txtScheduleTimeM.Value = New Date(CType(0, Long))
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(9, 261)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(126, 16)
        Me.MyLabel8.TabIndex = 12149
        Me.MyLabel8.Text = "Schedule Time Morning"
        '
        'MyLabel11
        '
        Me.MyLabel11.FieldName = Nothing
        Me.MyLabel11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel11.Location = New System.Drawing.Point(9, 237)
        Me.MyLabel11.Name = "MyLabel11"
        Me.MyLabel11.Size = New System.Drawing.Size(126, 16)
        Me.MyLabel11.TabIndex = 12147
        Me.MyLabel11.Text = "Schedule Time Morning"
        '
        'CuttOffTime
        '
        Me.CuttOffTime.FieldName = Nothing
        Me.CuttOffTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CuttOffTime.Location = New System.Drawing.Point(427, 52)
        Me.CuttOffTime.Name = "CuttOffTime"
        Me.CuttOffTime.Size = New System.Drawing.Size(74, 16)
        Me.CuttOffTime.TabIndex = 12145
        Me.CuttOffTime.Text = "Cutt Off Time"
        '
        'txtcuttofftime
        '
        Me.txtcuttofftime.CalculationExpression = Nothing
        Me.txtcuttofftime.CustomFormat = "dd/MM/yyyy"
        Me.txtcuttofftime.FieldCode = Nothing
        Me.txtcuttofftime.FieldDesc = Nothing
        Me.txtcuttofftime.FieldMaxLength = 0
        Me.txtcuttofftime.FieldName = Nothing
        Me.txtcuttofftime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcuttofftime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtcuttofftime.isCalculatedField = False
        Me.txtcuttofftime.IsSourceFromTable = False
        Me.txtcuttofftime.IsSourceFromValueList = False
        Me.txtcuttofftime.IsUnique = False
        Me.txtcuttofftime.Location = New System.Drawing.Point(503, 52)
        Me.txtcuttofftime.MendatroryField = False
        Me.txtcuttofftime.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcuttofftime.MyLinkLable1 = Me.CuttOffTime
        Me.txtcuttofftime.MyLinkLable2 = Nothing
        Me.txtcuttofftime.Name = "txtcuttofftime"
        Me.txtcuttofftime.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtcuttofftime.ReferenceFieldDesc = Nothing
        Me.txtcuttofftime.ReferenceFieldName = Nothing
        Me.txtcuttofftime.ReferenceTableName = Nothing
        Me.txtcuttofftime.Size = New System.Drawing.Size(81, 18)
        Me.txtcuttofftime.TabIndex = 12144
        Me.txtcuttofftime.TabStop = False
        Me.txtcuttofftime.Text = "17/05/2011"
        Me.txtcuttofftime.Value = New Date(2011, 5, 17, 15, 2, 19, 281)
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AutoSize = False
        Me.txtVehicleNo.BorderVisible = True
        Me.txtVehicleNo.FieldName = Nothing
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.Location = New System.Drawing.Point(306, 184)
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.Size = New System.Drawing.Size(300, 21)
        Me.txtVehicleNo.TabIndex = 12143
        Me.txtVehicleNo.TextWrap = False
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(9, 185)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(58, 18)
        Me.MyLabel7.TabIndex = 12142
        Me.MyLabel7.Text = "Tanker No"
        '
        'txtTankerNo
        '
        Me.txtTankerNo.CalculationExpression = Nothing
        Me.txtTankerNo.FieldCode = Nothing
        Me.txtTankerNo.FieldDesc = Nothing
        Me.txtTankerNo.FieldMaxLength = 0
        Me.txtTankerNo.FieldName = Nothing
        Me.txtTankerNo.isCalculatedField = False
        Me.txtTankerNo.IsSourceFromTable = False
        Me.txtTankerNo.IsSourceFromValueList = False
        Me.txtTankerNo.IsUnique = False
        Me.txtTankerNo.Location = New System.Drawing.Point(149, 184)
        Me.txtTankerNo.MendatroryField = True
        Me.txtTankerNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTankerNo.MyLinkLable1 = Me.MyLabel7
        Me.txtTankerNo.MyLinkLable2 = Nothing
        Me.txtTankerNo.MyReadOnly = False
        Me.txtTankerNo.MyShowMasterFormButton = False
        Me.txtTankerNo.Name = "txtTankerNo"
        Me.txtTankerNo.ReferenceFieldDesc = Nothing
        Me.txtTankerNo.ReferenceFieldName = Nothing
        Me.txtTankerNo.ReferenceTableName = Nothing
        Me.txtTankerNo.Size = New System.Drawing.Size(155, 20)
        Me.txtTankerNo.TabIndex = 12141
        Me.txtTankerNo.Value = ""
        '
        'chkDefault
        '
        Me.chkDefault.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDefault.Location = New System.Drawing.Point(425, 7)
        Me.chkDefault.Name = "chkDefault"
        Me.chkDefault.Size = New System.Drawing.Size(56, 16)
        Me.chkDefault.TabIndex = 12140
        Me.chkDefault.Text = "Default"
        '
        'txtRouteNameHindi
        '
        Me.txtRouteNameHindi.CalculationExpression = Nothing
        Me.txtRouteNameHindi.FieldCode = Nothing
        Me.txtRouteNameHindi.FieldDesc = Nothing
        Me.txtRouteNameHindi.FieldMaxLength = 0
        Me.txtRouteNameHindi.FieldName = Nothing
        Me.txtRouteNameHindi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteNameHindi.isCalculatedField = False
        Me.txtRouteNameHindi.IsSourceFromTable = False
        Me.txtRouteNameHindi.IsSourceFromValueList = False
        Me.txtRouteNameHindi.IsUnique = False
        Me.txtRouteNameHindi.Location = New System.Drawing.Point(425, 27)
        Me.txtRouteNameHindi.MaxLength = 30
        Me.txtRouteNameHindi.MendatroryField = False
        Me.txtRouteNameHindi.MyLinkLable1 = Me.MyLabel19
        Me.txtRouteNameHindi.MyLinkLable2 = Nothing
        Me.txtRouteNameHindi.Name = "txtRouteNameHindi"
        Me.txtRouteNameHindi.ReferenceFieldDesc = Nothing
        Me.txtRouteNameHindi.ReferenceFieldName = Nothing
        Me.txtRouteNameHindi.ReferenceTableName = Nothing
        Me.txtRouteNameHindi.Size = New System.Drawing.Size(279, 18)
        Me.txtRouteNameHindi.TabIndex = 12139
        '
        'MyLabel19
        '
        Me.MyLabel19.FieldName = Nothing
        Me.MyLabel19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel19.Location = New System.Drawing.Point(9, 28)
        Me.MyLabel19.Name = "MyLabel19"
        Me.MyLabel19.Size = New System.Drawing.Size(70, 16)
        Me.MyLabel19.TabIndex = 1421
        Me.MyLabel19.Text = "Route Name"
        '
        'txtTollAmount
        '
        Me.txtTollAmount.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtTollAmount.CalculationExpression = Nothing
        Me.txtTollAmount.DecimalPlaces = 0
        Me.txtTollAmount.FieldCode = Nothing
        Me.txtTollAmount.FieldDesc = Nothing
        Me.txtTollAmount.FieldMaxLength = 0
        Me.txtTollAmount.FieldName = Nothing
        Me.txtTollAmount.isCalculatedField = False
        Me.txtTollAmount.IsSourceFromTable = False
        Me.txtTollAmount.IsSourceFromValueList = False
        Me.txtTollAmount.IsUnique = False
        Me.txtTollAmount.Location = New System.Drawing.Point(149, 210)
        Me.txtTollAmount.MendatroryField = False
        Me.txtTollAmount.MyLinkLable1 = Me.MyLabel6
        Me.txtTollAmount.MyLinkLable2 = Nothing
        Me.txtTollAmount.Name = "txtTollAmount"
        Me.txtTollAmount.ReferenceFieldDesc = Nothing
        Me.txtTollAmount.ReferenceFieldName = Nothing
        Me.txtTollAmount.ReferenceTableName = Nothing
        Me.txtTollAmount.Size = New System.Drawing.Size(155, 20)
        Me.txtTollAmount.TabIndex = 12138
        Me.txtTollAmount.Text = "0"
        Me.txtTollAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTollAmount.Value = 0R
        '
        'MyLabel6
        '
        Me.MyLabel6.FieldName = Nothing
        Me.MyLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel6.Location = New System.Drawing.Point(9, 212)
        Me.MyLabel6.Name = "MyLabel6"
        Me.MyLabel6.Size = New System.Drawing.Size(67, 16)
        Me.MyLabel6.TabIndex = 12137
        Me.MyLabel6.Text = "Toll Amount"
        '
        'lblToLocationName
        '
        Me.lblToLocationName.AutoSize = False
        Me.lblToLocationName.BorderVisible = True
        Me.lblToLocationName.FieldName = Nothing
        Me.lblToLocationName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToLocationName.Location = New System.Drawing.Point(306, 159)
        Me.lblToLocationName.Name = "lblToLocationName"
        Me.lblToLocationName.Size = New System.Drawing.Size(300, 21)
        Me.lblToLocationName.TabIndex = 12136
        Me.lblToLocationName.TextWrap = False
        '
        'txtToLocationCode
        '
        Me.txtToLocationCode.CalculationExpression = Nothing
        Me.txtToLocationCode.FieldCode = Nothing
        Me.txtToLocationCode.FieldDesc = Nothing
        Me.txtToLocationCode.FieldMaxLength = 0
        Me.txtToLocationCode.FieldName = Nothing
        Me.txtToLocationCode.isCalculatedField = False
        Me.txtToLocationCode.IsSourceFromTable = False
        Me.txtToLocationCode.IsSourceFromValueList = False
        Me.txtToLocationCode.IsUnique = False
        Me.txtToLocationCode.Location = New System.Drawing.Point(149, 159)
        Me.txtToLocationCode.MendatroryField = True
        Me.txtToLocationCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToLocationCode.MyLinkLable1 = Me.MyLabel3
        Me.txtToLocationCode.MyLinkLable2 = Nothing
        Me.txtToLocationCode.MyReadOnly = False
        Me.txtToLocationCode.MyShowMasterFormButton = False
        Me.txtToLocationCode.Name = "txtToLocationCode"
        Me.txtToLocationCode.ReferenceFieldDesc = Nothing
        Me.txtToLocationCode.ReferenceFieldName = Nothing
        Me.txtToLocationCode.ReferenceTableName = Nothing
        Me.txtToLocationCode.Size = New System.Drawing.Size(155, 21)
        Me.txtToLocationCode.TabIndex = 12135
        Me.txtToLocationCode.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(9, 118)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(45, 16)
        Me.MyLabel3.TabIndex = 1424
        Me.MyLabel3.Text = "Amount"
        '
        'lblToLocation
        '
        Me.lblToLocation.FieldName = Nothing
        Me.lblToLocation.Location = New System.Drawing.Point(9, 162)
        Me.lblToLocation.Name = "lblToLocation"
        Me.lblToLocation.Size = New System.Drawing.Size(49, 18)
        Me.lblToLocation.TabIndex = 12134
        Me.lblToLocation.Text = "Location"
        '
        'chkForContractor
        '
        Me.chkForContractor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkForContractor.Location = New System.Drawing.Point(306, 52)
        Me.chkForContractor.Name = "chkForContractor"
        Me.chkForContractor.Size = New System.Drawing.Size(93, 16)
        Me.chkForContractor.TabIndex = 12133
        Me.chkForContractor.Text = "For Contractor"
        '
        'txtMCC
        '
        Me.txtMCC.arrDispalyMember = Nothing
        Me.txtMCC.arrValueMember = Nothing
        Me.txtMCC.Location = New System.Drawing.Point(149, 138)
        Me.txtMCC.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMCC.MyLinkLable1 = Me.MyLabel16
        Me.txtMCC.MyLinkLable2 = Nothing
        Me.txtMCC.MyNullText = "All"
        Me.txtMCC.Name = "txtMCC"
        Me.txtMCC.Size = New System.Drawing.Size(456, 19)
        Me.txtMCC.TabIndex = 1446
        '
        'MyLabel16
        '
        Me.MyLabel16.FieldName = Nothing
        Me.MyLabel16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel16.Location = New System.Drawing.Point(9, 138)
        Me.MyLabel16.Name = "MyLabel16"
        Me.MyLabel16.Size = New System.Drawing.Size(30, 18)
        Me.MyLabel16.TabIndex = 1447
        Me.MyLabel16.Text = "MCC"
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(306, 96)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(20, 16)
        Me.MyLabel5.TabIndex = 1444
        Me.MyLabel5.Text = "Kg"
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtWeight.CalculationExpression = Nothing
        Me.txtWeight.DecimalPlaces = 2
        Me.txtWeight.FieldCode = Nothing
        Me.txtWeight.FieldDesc = Nothing
        Me.txtWeight.FieldMaxLength = 0
        Me.txtWeight.FieldName = Nothing
        Me.txtWeight.isCalculatedField = False
        Me.txtWeight.IsSourceFromTable = False
        Me.txtWeight.IsSourceFromValueList = False
        Me.txtWeight.IsUnique = False
        Me.txtWeight.Location = New System.Drawing.Point(149, 94)
        Me.txtWeight.MendatroryField = False
        Me.txtWeight.MyLinkLable1 = Me.MyLabel4
        Me.txtWeight.MyLinkLable2 = Nothing
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReferenceFieldDesc = Nothing
        Me.txtWeight.ReferenceFieldName = Nothing
        Me.txtWeight.ReferenceTableName = Nothing
        Me.txtWeight.Size = New System.Drawing.Size(155, 20)
        Me.txtWeight.TabIndex = 1443
        Me.txtWeight.Text = "0"
        Me.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtWeight.Value = 0R
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel4.Location = New System.Drawing.Point(9, 96)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel4.TabIndex = 1441
        Me.MyLabel4.Text = "Weight"
        '
        'lblAmout
        '
        Me.lblAmout.AutoSize = False
        Me.lblAmout.BorderVisible = True
        Me.lblAmout.FieldName = Nothing
        Me.lblAmout.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmout.Location = New System.Drawing.Point(149, 117)
        Me.lblAmout.Name = "lblAmout"
        Me.lblAmout.Size = New System.Drawing.Size(155, 18)
        Me.lblAmout.TabIndex = 1440
        Me.lblAmout.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRouteNo
        '
        Me.txtRouteNo.FieldName = Nothing
        Me.txtRouteNo.Location = New System.Drawing.Point(149, 6)
        Me.txtRouteNo.MendatroryField = False
        Me.txtRouteNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtRouteNo.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtRouteNo.MyLinkLable1 = Me.RadLabel1
        Me.txtRouteNo.MyLinkLable2 = Nothing
        Me.txtRouteNo.MyMaxLength = 32767
        Me.txtRouteNo.MyReadOnly = False
        Me.txtRouteNo.Name = "txtRouteNo"
        Me.txtRouteNo.Size = New System.Drawing.Size(252, 19)
        Me.txtRouteNo.TabIndex = 1437
        Me.txtRouteNo.Value = ""
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel1.Location = New System.Drawing.Point(9, 7)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(54, 16)
        Me.RadLabel1.TabIndex = 39
        Me.RadLabel1.Text = "Route No"
        '
        'btnNew
        '
        Me.btnNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnNew.Location = New System.Drawing.Point(401, 6)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(18, 20)
        Me.btnNew.TabIndex = 1438
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRate.CalculationExpression = Nothing
        Me.txtRate.DecimalPlaces = 2
        Me.txtRate.FieldCode = Nothing
        Me.txtRate.FieldDesc = Nothing
        Me.txtRate.FieldMaxLength = 0
        Me.txtRate.FieldName = Nothing
        Me.txtRate.isCalculatedField = False
        Me.txtRate.IsSourceFromTable = False
        Me.txtRate.IsSourceFromValueList = False
        Me.txtRate.IsUnique = False
        Me.txtRate.Location = New System.Drawing.Point(149, 70)
        Me.txtRate.MendatroryField = False
        Me.txtRate.MyLinkLable1 = Me.MyLabel2
        Me.txtRate.MyLinkLable2 = Nothing
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReferenceFieldDesc = Nothing
        Me.txtRate.ReferenceFieldName = Nothing
        Me.txtRate.ReferenceTableName = Nothing
        Me.txtRate.Size = New System.Drawing.Size(155, 20)
        Me.txtRate.TabIndex = 1436
        Me.txtRate.Text = "0"
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRate.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 72)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(30, 16)
        Me.MyLabel2.TabIndex = 1423
        Me.MyLabel2.Text = "Rate"
        '
        'txtDistance
        '
        Me.txtDistance.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtDistance.CalculationExpression = Nothing
        Me.txtDistance.DecimalPlaces = 0
        Me.txtDistance.FieldCode = Nothing
        Me.txtDistance.FieldDesc = Nothing
        Me.txtDistance.FieldMaxLength = 0
        Me.txtDistance.FieldName = Nothing
        Me.txtDistance.isCalculatedField = False
        Me.txtDistance.IsSourceFromTable = False
        Me.txtDistance.IsSourceFromValueList = False
        Me.txtDistance.IsUnique = False
        Me.txtDistance.Location = New System.Drawing.Point(149, 48)
        Me.txtDistance.MendatroryField = False
        Me.txtDistance.MyLinkLable1 = Me.MyLabel1
        Me.txtDistance.MyLinkLable2 = Nothing
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.ReferenceFieldDesc = Nothing
        Me.txtDistance.ReferenceFieldName = Nothing
        Me.txtDistance.ReferenceTableName = Nothing
        Me.txtDistance.Size = New System.Drawing.Size(155, 20)
        Me.txtDistance.TabIndex = 1435
        Me.txtDistance.Text = "0"
        Me.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDistance.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 50)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(50, 16)
        Me.MyLabel1.TabIndex = 1422
        Me.MyLabel1.Text = "Distance"
        '
        'txtRouteName
        '
        Me.txtRouteName.CalculationExpression = Nothing
        Me.txtRouteName.FieldCode = Nothing
        Me.txtRouteName.FieldDesc = Nothing
        Me.txtRouteName.FieldMaxLength = 0
        Me.txtRouteName.FieldName = Nothing
        Me.txtRouteName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRouteName.isCalculatedField = False
        Me.txtRouteName.IsSourceFromTable = False
        Me.txtRouteName.IsSourceFromValueList = False
        Me.txtRouteName.IsUnique = False
        Me.txtRouteName.Location = New System.Drawing.Point(149, 27)
        Me.txtRouteName.MaxLength = 30
        Me.txtRouteName.MendatroryField = False
        Me.txtRouteName.MyLinkLable1 = Me.MyLabel19
        Me.txtRouteName.MyLinkLable2 = Nothing
        Me.txtRouteName.Name = "txtRouteName"
        Me.txtRouteName.ReferenceFieldDesc = Nothing
        Me.txtRouteName.ReferenceFieldName = Nothing
        Me.txtRouteName.ReferenceTableName = Nothing
        Me.txtRouteName.Size = New System.Drawing.Size(270, 18)
        Me.txtRouteName.TabIndex = 1420
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(645, 6)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnclose.TabIndex = 4
        Me.rdbtnclose.Text = "Close"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Location = New System.Drawing.Point(3, 6)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(66, 18)
        Me.rdbtnsave.TabIndex = 2
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtndelete
        '
        Me.rdbtndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtndelete.Location = New System.Drawing.Point(72, 6)
        Me.rdbtndelete.Name = "rdbtndelete"
        Me.rdbtndelete.Size = New System.Drawing.Size(66, 18)
        Me.rdbtndelete.TabIndex = 3
        Me.rdbtndelete.Text = "Delete"
        '
        'FrmBulkRoutMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(714, 355)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.rdmenu1)
        Me.Name = "FrmBulkRoutMaster"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Bulk Route Master"
        CType(Me.rdmenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.txtScheduleTimeE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtScheduleTimeM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CuttOffTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcuttofftime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDefault, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteNameHindi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTollAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkForContractor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRouteName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtndelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdmenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents File As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents MyLabel19 As common.Controls.MyLabel
    Friend WithEvents txtRouteName As common.Controls.MyTextBox
    Friend WithEvents txtRate As common.MyNumBox
    Friend WithEvents txtDistance As common.MyNumBox
    Friend WithEvents txtRouteNo As common.UserControls.txtNavigator
    Friend WithEvents btnNew As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblAmout As common.Controls.MyLabel
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtndelete As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents txtWeight As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtMCC As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel16 As common.Controls.MyLabel
    Friend WithEvents chkForContractor As RadCheckBox
    Friend WithEvents lblToLocationName As common.Controls.MyLabel
    Friend WithEvents txtToLocationCode As common.UserControls.txtFinder
    Friend WithEvents lblToLocation As common.Controls.MyLabel
    Friend WithEvents txtTollAmount As common.MyNumBox
    Friend WithEvents MyLabel6 As common.Controls.MyLabel
    Friend WithEvents txtRouteNameHindi As common.Controls.MyTextBox
    Friend WithEvents chkDefault As RadCheckBox
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtTankerNo As common.UserControls.txtFinder
    Friend WithEvents txtVehicleNo As common.Controls.MyLabel
    Friend WithEvents txtcuttofftime As common.Controls.MyDateTimePicker
    Friend WithEvents CuttOffTime As common.Controls.MyLabel
    Friend WithEvents MyLabel11 As common.Controls.MyLabel
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtScheduleTimeE As common.Controls.MyDateTimePicker
    Friend WithEvents txtScheduleTimeM As common.Controls.MyDateTimePicker
End Class

