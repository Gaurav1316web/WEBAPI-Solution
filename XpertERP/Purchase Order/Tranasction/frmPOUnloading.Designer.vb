<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOUnloading
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lblCode = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblGDShipToLocationName = New common.Controls.MyLabel()
        Me.MyLabel12 = New common.Controls.MyLabel()
        Me.lblGDShipToLocation = New common.Controls.MyLabel()
        Me.lblGDGRNDate = New common.Controls.MyLabel()
        Me.MyLabel9 = New common.Controls.MyLabel()
        Me.lblGDBillToLocationName = New common.Controls.MyLabel()
        Me.lblGDVendorName = New common.Controls.MyLabel()
        Me.MyLabel5 = New common.Controls.MyLabel()
        Me.lblGDCarrier = New common.Controls.MyLabel()
        Me.lblGDBillToLocation = New common.Controls.MyLabel()
        Me.RadLabel2 = New common.Controls.MyLabel()
        Me.lblGDVendorCode = New common.Controls.MyLabel()
        Me.lblGDVehicleNo = New common.Controls.MyLabel()
        Me.lblDocDate = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.btnnew = New Telerik.WinControls.UI.RadButton()
        Me.MyLabel8 = New common.Controls.MyLabel()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.UsGrossWeight = New common.usLock()
        Me.MyLabel7 = New common.Controls.MyLabel()
        Me.txtGateEntryNo = New common.UserControls.txtFinder()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtGrossWeight = New common.MyNumBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblGDShipToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDShipToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDGRNDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDBillToLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDVendorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDCarrier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDBillToLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDVendorCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGDVehicleNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnnew)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.UsGrossWeight)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtGateEntryNo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtGrossWeight)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(922, 398)
        Me.SplitContainer2.SplitterDistance = 135
        Me.SplitContainer2.TabIndex = 1067
        '
        'lblCode
        '
        Me.lblCode.FieldName = Nothing
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblCode.Location = New System.Drawing.Point(5, 5)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(81, 16)
        Me.lblCode.TabIndex = 1046
        Me.lblCode.Text = "Weighment No"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.lblGDShipToLocationName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel12)
        Me.RadGroupBox1.Controls.Add(Me.lblGDShipToLocation)
        Me.RadGroupBox1.Controls.Add(Me.lblGDGRNDate)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel9)
        Me.RadGroupBox1.Controls.Add(Me.lblGDBillToLocationName)
        Me.RadGroupBox1.Controls.Add(Me.lblGDVendorName)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel5)
        Me.RadGroupBox1.Controls.Add(Me.lblGDCarrier)
        Me.RadGroupBox1.Controls.Add(Me.lblGDBillToLocation)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel2)
        Me.RadGroupBox1.Controls.Add(Me.lblGDVendorCode)
        Me.RadGroupBox1.Controls.Add(Me.lblGDVehicleNo)
        Me.RadGroupBox1.Controls.Add(Me.lblDocDate)
        Me.RadGroupBox1.Controls.Add(Me.RadLabel6)
        Me.RadGroupBox1.HeaderText = "GRN Details"
        Me.RadGroupBox1.Location = New System.Drawing.Point(352, 4)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(546, 127)
        Me.RadGroupBox1.TabIndex = 1045
        Me.RadGroupBox1.Text = "GRN Details"
        '
        'lblGDShipToLocationName
        '
        Me.lblGDShipToLocationName.AutoSize = False
        Me.lblGDShipToLocationName.BorderVisible = True
        Me.lblGDShipToLocationName.FieldName = Nothing
        Me.lblGDShipToLocationName.Location = New System.Drawing.Point(248, 106)
        Me.lblGDShipToLocationName.Name = "lblGDShipToLocationName"
        Me.lblGDShipToLocationName.Size = New System.Drawing.Size(296, 18)
        Me.lblGDShipToLocationName.TabIndex = 1049
        Me.lblGDShipToLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel12
        '
        Me.MyLabel12.FieldName = Nothing
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel12.Location = New System.Drawing.Point(5, 107)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel12.TabIndex = 1051
        Me.MyLabel12.Text = "Ship To Location"
        '
        'lblGDShipToLocation
        '
        Me.lblGDShipToLocation.AutoSize = False
        Me.lblGDShipToLocation.BorderVisible = True
        Me.lblGDShipToLocation.FieldName = Nothing
        Me.lblGDShipToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGDShipToLocation.Location = New System.Drawing.Point(100, 106)
        Me.lblGDShipToLocation.Name = "lblGDShipToLocation"
        Me.lblGDShipToLocation.Size = New System.Drawing.Size(143, 18)
        Me.lblGDShipToLocation.TabIndex = 1050
        Me.lblGDShipToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGDShipToLocation.TextWrap = False
        '
        'lblGDGRNDate
        '
        Me.lblGDGRNDate.AutoSize = False
        Me.lblGDGRNDate.BorderVisible = True
        Me.lblGDGRNDate.FieldName = Nothing
        Me.lblGDGRNDate.Location = New System.Drawing.Point(100, 18)
        Me.lblGDGRNDate.Name = "lblGDGRNDate"
        Me.lblGDGRNDate.Size = New System.Drawing.Size(143, 18)
        Me.lblGDGRNDate.TabIndex = 1048
        Me.lblGDGRNDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel9
        '
        Me.MyLabel9.FieldName = Nothing
        Me.MyLabel9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel9.Location = New System.Drawing.Point(248, 41)
        Me.MyLabel9.Name = "MyLabel9"
        Me.MyLabel9.Size = New System.Drawing.Size(41, 16)
        Me.MyLabel9.TabIndex = 1047
        Me.MyLabel9.Text = "Carrier"
        '
        'lblGDBillToLocationName
        '
        Me.lblGDBillToLocationName.AutoSize = False
        Me.lblGDBillToLocationName.BorderVisible = True
        Me.lblGDBillToLocationName.FieldName = Nothing
        Me.lblGDBillToLocationName.Location = New System.Drawing.Point(248, 84)
        Me.lblGDBillToLocationName.Name = "lblGDBillToLocationName"
        Me.lblGDBillToLocationName.Size = New System.Drawing.Size(296, 18)
        Me.lblGDBillToLocationName.TabIndex = 1044
        Me.lblGDBillToLocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGDVendorName
        '
        Me.lblGDVendorName.AutoSize = False
        Me.lblGDVendorName.BorderVisible = True
        Me.lblGDVendorName.FieldName = Nothing
        Me.lblGDVendorName.Location = New System.Drawing.Point(248, 62)
        Me.lblGDVendorName.Name = "lblGDVendorName"
        Me.lblGDVendorName.Size = New System.Drawing.Size(296, 18)
        Me.lblGDVendorName.TabIndex = 35
        Me.lblGDVendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel5
        '
        Me.MyLabel5.FieldName = Nothing
        Me.MyLabel5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel5.Location = New System.Drawing.Point(5, 85)
        Me.MyLabel5.Name = "MyLabel5"
        Me.MyLabel5.Size = New System.Drawing.Size(84, 16)
        Me.MyLabel5.TabIndex = 1046
        Me.MyLabel5.Text = "Bill To Location"
        '
        'lblGDCarrier
        '
        Me.lblGDCarrier.AutoSize = False
        Me.lblGDCarrier.BorderVisible = True
        Me.lblGDCarrier.FieldName = Nothing
        Me.lblGDCarrier.Location = New System.Drawing.Point(298, 40)
        Me.lblGDCarrier.Name = "lblGDCarrier"
        Me.lblGDCarrier.Size = New System.Drawing.Size(246, 18)
        Me.lblGDCarrier.TabIndex = 1044
        Me.lblGDCarrier.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGDBillToLocation
        '
        Me.lblGDBillToLocation.AutoSize = False
        Me.lblGDBillToLocation.BorderVisible = True
        Me.lblGDBillToLocation.FieldName = Nothing
        Me.lblGDBillToLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGDBillToLocation.Location = New System.Drawing.Point(100, 84)
        Me.lblGDBillToLocation.Name = "lblGDBillToLocation"
        Me.lblGDBillToLocation.Size = New System.Drawing.Size(143, 18)
        Me.lblGDBillToLocation.TabIndex = 1045
        Me.lblGDBillToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGDBillToLocation.TextWrap = False
        '
        'RadLabel2
        '
        Me.RadLabel2.FieldName = Nothing
        Me.RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel2.Location = New System.Drawing.Point(5, 63)
        Me.RadLabel2.Name = "RadLabel2"
        Me.RadLabel2.Size = New System.Drawing.Size(60, 16)
        Me.RadLabel2.TabIndex = 1043
        Me.RadLabel2.Text = "Vendor No"
        '
        'lblGDVendorCode
        '
        Me.lblGDVendorCode.AutoSize = False
        Me.lblGDVendorCode.BorderVisible = True
        Me.lblGDVendorCode.FieldName = Nothing
        Me.lblGDVendorCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGDVendorCode.Location = New System.Drawing.Point(100, 62)
        Me.lblGDVendorCode.Name = "lblGDVendorCode"
        Me.lblGDVendorCode.Size = New System.Drawing.Size(143, 18)
        Me.lblGDVendorCode.TabIndex = 1042
        Me.lblGDVendorCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGDVendorCode.TextWrap = False
        '
        'lblGDVehicleNo
        '
        Me.lblGDVehicleNo.AutoSize = False
        Me.lblGDVehicleNo.BorderVisible = True
        Me.lblGDVehicleNo.FieldName = Nothing
        Me.lblGDVehicleNo.Location = New System.Drawing.Point(100, 40)
        Me.lblGDVehicleNo.Name = "lblGDVehicleNo"
        Me.lblGDVehicleNo.Size = New System.Drawing.Size(143, 18)
        Me.lblGDVehicleNo.TabIndex = 34
        Me.lblGDVehicleNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDocDate
        '
        Me.lblDocDate.FieldName = Nothing
        Me.lblDocDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocDate.Location = New System.Drawing.Point(5, 19)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(58, 16)
        Me.lblDocDate.TabIndex = 1028
        Me.lblDocDate.Text = "GRN Date"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel6.Location = New System.Drawing.Point(5, 41)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(61, 16)
        Me.RadLabel6.TabIndex = 1041
        Me.RadLabel6.Text = "Vehicle No"
        '
        'btnnew
        '
        Me.btnnew.Image = Global.ERP.My.Resources.Resources._new
        Me.btnnew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnnew.Location = New System.Drawing.Point(330, 3)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(19, 21)
        Me.btnnew.TabIndex = 1040
        '
        'MyLabel8
        '
        Me.MyLabel8.FieldName = Nothing
        Me.MyLabel8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel8.Location = New System.Drawing.Point(5, 29)
        Me.MyLabel8.Name = "MyLabel8"
        Me.MyLabel8.Size = New System.Drawing.Size(91, 16)
        Me.MyLabel8.TabIndex = 1047
        Me.MyLabel8.Text = "Weighment Date"
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(96, 3)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 32767
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(234, 21)
        Me.txtCode.TabIndex = 1044
        Me.txtCode.Value = ""
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
        Me.txtDate.Location = New System.Drawing.Point(96, 28)
        Me.txtDate.MendatroryField = True
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.MyLabel8
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(146, 18)
        Me.txtDate.TabIndex = 1041
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "03/05/2011 12:00:00 AM"
        Me.txtDate.Value = New Date(2011, 5, 3, 0, 0, 0, 0)
        '
        'UsGrossWeight
        '
        Me.UsGrossWeight.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsGrossWeight.Location = New System.Drawing.Point(249, 27)
        Me.UsGrossWeight.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsGrossWeight.Name = "UsGrossWeight"
        Me.UsGrossWeight.Size = New System.Drawing.Size(98, 21)
        Me.UsGrossWeight.Status = common.ERPTransactionStatus.Pending
        Me.UsGrossWeight.TabIndex = 1050
        '
        'MyLabel7
        '
        Me.MyLabel7.FieldName = Nothing
        Me.MyLabel7.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel7.Location = New System.Drawing.Point(5, 76)
        Me.MyLabel7.Name = "MyLabel7"
        Me.MyLabel7.Size = New System.Drawing.Size(75, 16)
        Me.MyLabel7.TabIndex = 1049
        Me.MyLabel7.Text = "Gross Weight"
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
        Me.txtGateEntryNo.Location = New System.Drawing.Point(96, 50)
        Me.txtGateEntryNo.MendatroryField = True
        Me.txtGateEntryNo.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateEntryNo.MyLinkLable1 = Me.MyLabel3
        Me.txtGateEntryNo.MyLinkLable2 = Nothing
        Me.txtGateEntryNo.MyReadOnly = False
        Me.txtGateEntryNo.MyShowMasterFormButton = False
        Me.txtGateEntryNo.Name = "txtGateEntryNo"
        Me.txtGateEntryNo.ReferenceFieldDesc = Nothing
        Me.txtGateEntryNo.ReferenceFieldName = Nothing
        Me.txtGateEntryNo.ReferenceTableName = Nothing
        Me.txtGateEntryNo.Size = New System.Drawing.Size(146, 20)
        Me.txtGateEntryNo.TabIndex = 1042
        Me.txtGateEntryNo.Value = ""
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(5, 51)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(47, 18)
        Me.MyLabel3.TabIndex = 1048
        Me.MyLabel3.Text = "GRN No"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtGrossWeight.CalculationExpression = Nothing
        Me.txtGrossWeight.DecimalPlaces = 3
        Me.txtGrossWeight.FieldCode = Nothing
        Me.txtGrossWeight.FieldDesc = Nothing
        Me.txtGrossWeight.FieldMaxLength = 0
        Me.txtGrossWeight.FieldName = Nothing
        Me.txtGrossWeight.isCalculatedField = False
        Me.txtGrossWeight.IsSourceFromTable = False
        Me.txtGrossWeight.IsSourceFromValueList = False
        Me.txtGrossWeight.IsUnique = False
        Me.txtGrossWeight.Location = New System.Drawing.Point(96, 74)
        Me.txtGrossWeight.MendatroryField = True
        Me.txtGrossWeight.MyLinkLable1 = Nothing
        Me.txtGrossWeight.MyLinkLable2 = Nothing
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.ReadOnly = True
        Me.txtGrossWeight.ReferenceFieldDesc = Nothing
        Me.txtGrossWeight.ReferenceFieldName = Nothing
        Me.txtGrossWeight.ReferenceTableName = Nothing
        Me.txtGrossWeight.Size = New System.Drawing.Size(146, 20)
        Me.txtGrossWeight.TabIndex = 1043
        Me.txtGrossWeight.Text = "0"
        Me.txtGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGrossWeight.Value = 0.0R
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.gv1)
        Me.RadGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox2.HeaderText = "Item Details"
        Me.RadGroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(922, 259)
        Me.RadGroupBox2.TabIndex = 25
        Me.RadGroupBox2.Text = "Item Details"
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
        Me.gv1.Size = New System.Drawing.Size(902, 229)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        Me.gv1.Text = "RadGridView1"
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.ForeColor = System.Drawing.Color.Blue
        Me.MyLabel1.Location = New System.Drawing.Point(5, 7)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(233, 16)
        Me.MyLabel1.TabIndex = 1052
        Me.MyLabel1.Text = "Double click on item to Unload particular item"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.MyLabel1)
        Me.Panel1.Controls.Add(Me.btnclose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 398)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(922, 28)
        Me.Panel1.TabIndex = 1068
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(851, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 21)
        Me.btnclose.TabIndex = 4
        Me.btnclose.Text = "Close"
        '
        'frmPOUnloading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 426)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmPOUnloading"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "PO Unload"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.lblCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblGDShipToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDShipToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDGRNDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDBillToLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDVendorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDCarrier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDBillToLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDVendorCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGDVehicleNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrossWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents lblCode As common.Controls.MyLabel
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblGDShipToLocationName As common.Controls.MyLabel
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents lblGDShipToLocation As common.Controls.MyLabel
    Friend WithEvents lblGDGRNDate As common.Controls.MyLabel
    Friend WithEvents MyLabel9 As common.Controls.MyLabel
    Friend WithEvents lblGDBillToLocationName As common.Controls.MyLabel
    Friend WithEvents lblGDVendorName As common.Controls.MyLabel
    Friend WithEvents MyLabel5 As common.Controls.MyLabel
    Friend WithEvents lblGDCarrier As common.Controls.MyLabel
    Friend WithEvents lblGDBillToLocation As common.Controls.MyLabel
    Friend WithEvents RadLabel2 As common.Controls.MyLabel
    Friend WithEvents lblGDVendorCode As common.Controls.MyLabel
    Friend WithEvents lblGDVehicleNo As common.Controls.MyLabel
    Friend WithEvents lblDocDate As common.Controls.MyLabel
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents btnnew As Telerik.WinControls.UI.RadButton
    Friend WithEvents MyLabel8 As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents UsGrossWeight As common.usLock
    Friend WithEvents MyLabel7 As common.Controls.MyLabel
    Friend WithEvents txtGateEntryNo As common.UserControls.txtFinder
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents txtGrossWeight As common.MyNumBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
End Class
